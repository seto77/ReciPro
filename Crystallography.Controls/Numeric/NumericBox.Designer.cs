namespace Crystallography.Controls
{
    partial class NumericBox
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

        #region コンポーネント デザイナで生成されたコード

        /// <summary> 
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NumericBox));
            this.textBox = new System.Windows.Forms.TextBox();
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
            this.labelHeader = new System.Windows.Forms.Label();
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.contextMenuStripUpDown = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.incrementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smartIncrementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxIncrement = new System.Windows.Forms.ToolStripComboBox();
            this.labelFooter = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.contextMenuStripBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            this.contextMenuStripUpDown.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.ContextMenuStrip = this.contextMenuStripBody;
            resources.ApplyResources(this.textBox, "textBox");
            this.textBox.Name = "textBox";
            this.textBox.Click += new System.EventHandler(this.textBox_Click);
            this.textBox.ReadOnlyChanged += new System.EventHandler(this.textBox_ReadOnlyChanged);
            this.textBox.FontChanged += new System.EventHandler(this.textBox_FontChanged);
            this.textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBox.Enter += new System.EventHandler(this.textBox_Enter);
            this.textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            this.textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            this.textBox.Leave += new System.EventHandler(this.textBox_Leave);
            this.textBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textBox_MouseDown);
            this.textBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.textBox_MouseMove);
            this.textBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.textBox_MouseUp);
            // 
            // contextMenuStripBody
            // 
            this.contextMenuStripBody.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.contextMenuStripBody, "contextMenuStripBody");
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
            this.contextMenuStripBody.Name = "contextMenuStripBody";
            this.contextMenuStripBody.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(this.contextMenuStripBody_Closing);
            // 
            // decimalPlacesToolStripMenuItem
            // 
            this.decimalPlacesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxDecimalPlaces});
            this.decimalPlacesToolStripMenuItem.Name = "decimalPlacesToolStripMenuItem";
            resources.ApplyResources(this.decimalPlacesToolStripMenuItem, "decimalPlacesToolStripMenuItem");
            // 
            // toolStripComboBoxDecimalPlaces
            // 
            this.toolStripComboBoxDecimalPlaces.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxDecimalPlaces.Items.AddRange(new object[] {
            resources.GetString("toolStripComboBoxDecimalPlaces.Items"),
            resources.GetString("toolStripComboBoxDecimalPlaces.Items1"),
            resources.GetString("toolStripComboBoxDecimalPlaces.Items2"),
            resources.GetString("toolStripComboBoxDecimalPlaces.Items3"),
            resources.GetString("toolStripComboBoxDecimalPlaces.Items4"),
            resources.GetString("toolStripComboBoxDecimalPlaces.Items5"),
            resources.GetString("toolStripComboBoxDecimalPlaces.Items6"),
            resources.GetString("toolStripComboBoxDecimalPlaces.Items7"),
            resources.GetString("toolStripComboBoxDecimalPlaces.Items8"),
            resources.GetString("toolStripComboBoxDecimalPlaces.Items9"),
            resources.GetString("toolStripComboBoxDecimalPlaces.Items10"),
            resources.GetString("toolStripComboBoxDecimalPlaces.Items11")});
            this.toolStripComboBoxDecimalPlaces.Name = "toolStripComboBoxDecimalPlaces";
            resources.ApplyResources(this.toolStripComboBoxDecimalPlaces, "toolStripComboBoxDecimalPlaces");
            this.toolStripComboBoxDecimalPlaces.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxDecimalPlaces_SelectedIndexChanged);
            // 
            // thousandsSeparatorToolStripMenuItem
            // 
            this.thousandsSeparatorToolStripMenuItem.Checked = true;
            this.thousandsSeparatorToolStripMenuItem.CheckOnClick = true;
            this.thousandsSeparatorToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.thousandsSeparatorToolStripMenuItem.Name = "thousandsSeparatorToolStripMenuItem";
            resources.ApplyResources(this.thousandsSeparatorToolStripMenuItem, "thousandsSeparatorToolStripMenuItem");
            this.thousandsSeparatorToolStripMenuItem.CheckedChanged += new System.EventHandler(this.thousandsSeparatorToolStripMenuItem_CheckedChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripMenuItemRestrictLimit
            // 
            this.toolStripMenuItemRestrictLimit.Checked = true;
            this.toolStripMenuItemRestrictLimit.CheckOnClick = true;
            this.toolStripMenuItemRestrictLimit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItemRestrictLimit.Name = "toolStripMenuItemRestrictLimit";
            resources.ApplyResources(this.toolStripMenuItemRestrictLimit, "toolStripMenuItemRestrictLimit");
            this.toolStripMenuItemRestrictLimit.CheckedChanged += new System.EventHandler(this.toolStripMenuItemRestrictLimit_CheckedChanged);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBoxMaximum});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // toolStripTextBoxMaximum
            // 
            this.toolStripTextBoxMaximum.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.toolStripTextBoxMaximum, "toolStripTextBoxMaximum");
            this.toolStripTextBoxMaximum.Name = "toolStripTextBoxMaximum";
            this.toolStripTextBoxMaximum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolStripTextBoxMaximum_KeyPress);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBoxMimimum});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // toolStripTextBoxMimimum
            // 
            this.toolStripTextBoxMimimum.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.toolStripTextBoxMimimum, "toolStripTextBoxMimimum");
            this.toolStripTextBoxMimimum.Name = "toolStripTextBoxMimimum";
            this.toolStripTextBoxMimimum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolStripTextBoxMaximum_KeyPress);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // allowMouseContlolToolStripMenuItem
            // 
            this.allowMouseContlolToolStripMenuItem.CheckOnClick = true;
            this.allowMouseContlolToolStripMenuItem.Name = "allowMouseContlolToolStripMenuItem";
            resources.ApplyResources(this.allowMouseContlolToolStripMenuItem, "allowMouseContlolToolStripMenuItem");
            this.allowMouseContlolToolStripMenuItem.CheckedChanged += new System.EventHandler(this.allowMouseContlolToolStripMenuItem_CheckedChanged);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBoxMouseSpeed});
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            // 
            // toolStripTextBoxMouseSpeed
            // 
            resources.ApplyResources(this.toolStripTextBoxMouseSpeed, "toolStripTextBoxMouseSpeed");
            this.toolStripTextBoxMouseSpeed.Name = "toolStripTextBoxMouseSpeed";
            this.toolStripTextBoxMouseSpeed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolStripTextBoxMouseSpeed_KeyPress);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxMouseDirection});
            resources.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            // 
            // toolStripComboBoxMouseDirection
            // 
            this.toolStripComboBoxMouseDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxMouseDirection.Items.AddRange(new object[] {
            resources.GetString("toolStripComboBoxMouseDirection.Items"),
            resources.GetString("toolStripComboBoxMouseDirection.Items1")});
            this.toolStripComboBoxMouseDirection.Name = "toolStripComboBoxMouseDirection";
            resources.ApplyResources(this.toolStripComboBoxMouseDirection, "toolStripComboBoxMouseDirection");
            // 
            // labelHeader
            // 
            resources.ApplyResources(this.labelHeader, "labelHeader");
            this.labelHeader.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.labelHeader.Name = "labelHeader";
            // 
            // numericUpDown
            // 
            this.numericUpDown.ContextMenuStrip = this.contextMenuStripUpDown;
            resources.ApplyResources(this.numericUpDown, "numericUpDown");
            this.numericUpDown.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.TabStop = false;
            this.numericUpDown.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // contextMenuStripUpDown
            // 
            resources.ApplyResources(this.contextMenuStripUpDown, "contextMenuStripUpDown");
            this.contextMenuStripUpDown.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.incrementToolStripMenuItem});
            this.contextMenuStripUpDown.Name = "contextMenuStrip1";
            // 
            // incrementToolStripMenuItem
            // 
            this.incrementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smartIncrementToolStripMenuItem,
            this.toolStripComboBoxIncrement});
            this.incrementToolStripMenuItem.Name = "incrementToolStripMenuItem";
            resources.ApplyResources(this.incrementToolStripMenuItem, "incrementToolStripMenuItem");
            // 
            // smartIncrementToolStripMenuItem
            // 
            this.smartIncrementToolStripMenuItem.Checked = true;
            this.smartIncrementToolStripMenuItem.CheckOnClick = true;
            this.smartIncrementToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.smartIncrementToolStripMenuItem.Name = "smartIncrementToolStripMenuItem";
            resources.ApplyResources(this.smartIncrementToolStripMenuItem, "smartIncrementToolStripMenuItem");
            this.smartIncrementToolStripMenuItem.CheckedChanged += new System.EventHandler(this.smartIncrementToolStripMenuItem_CheckedChanged);
            // 
            // toolStripComboBoxIncrement
            // 
            this.toolStripComboBoxIncrement.AutoCompleteCustomSource.AddRange(new string[] {
            resources.GetString("toolStripComboBoxIncrement.AutoCompleteCustomSource"),
            resources.GetString("toolStripComboBoxIncrement.AutoCompleteCustomSource1"),
            resources.GetString("toolStripComboBoxIncrement.AutoCompleteCustomSource2"),
            resources.GetString("toolStripComboBoxIncrement.AutoCompleteCustomSource3"),
            resources.GetString("toolStripComboBoxIncrement.AutoCompleteCustomSource4"),
            resources.GetString("toolStripComboBoxIncrement.AutoCompleteCustomSource5"),
            resources.GetString("toolStripComboBoxIncrement.AutoCompleteCustomSource6"),
            resources.GetString("toolStripComboBoxIncrement.AutoCompleteCustomSource7"),
            resources.GetString("toolStripComboBoxIncrement.AutoCompleteCustomSource8"),
            resources.GetString("toolStripComboBoxIncrement.AutoCompleteCustomSource9")});
            this.toolStripComboBoxIncrement.DropDownHeight = 200;
            resources.ApplyResources(this.toolStripComboBoxIncrement, "toolStripComboBoxIncrement");
            this.toolStripComboBoxIncrement.Items.AddRange(new object[] {
            resources.GetString("toolStripComboBoxIncrement.Items"),
            resources.GetString("toolStripComboBoxIncrement.Items1"),
            resources.GetString("toolStripComboBoxIncrement.Items2"),
            resources.GetString("toolStripComboBoxIncrement.Items3"),
            resources.GetString("toolStripComboBoxIncrement.Items4"),
            resources.GetString("toolStripComboBoxIncrement.Items5"),
            resources.GetString("toolStripComboBoxIncrement.Items6"),
            resources.GetString("toolStripComboBoxIncrement.Items7"),
            resources.GetString("toolStripComboBoxIncrement.Items8"),
            resources.GetString("toolStripComboBoxIncrement.Items9")});
            this.toolStripComboBoxIncrement.Name = "toolStripComboBoxIncrement";
            this.toolStripComboBoxIncrement.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxIncrement_SelectedIndexChanged);
            this.toolStripComboBoxIncrement.TextUpdate += new System.EventHandler(this.toolStripComboBoxIncrement_TextUpdate);
            // 
            // labelFooter
            // 
            resources.ApplyResources(this.labelFooter, "labelFooter");
            this.labelFooter.Name = "labelFooter";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            resources.ApplyResources(this.toolStripComboBox1, "toolStripComboBox1");
            // 
            // NumericBox
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.numericUpDown);
            this.Controls.Add(this.labelHeader);
            this.Controls.Add(this.labelFooter);
            this.DoubleBuffered = true;
            this.Name = "NumericBox";
            this.SizeChanged += new System.EventHandler(this.NumericalTextBox_SizeChanged);
            this.contextMenuStripBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
            this.contextMenuStripUpDown.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.Label labelFooter;
        public System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.NumericUpDown numericUpDown;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripUpDown;
        private System.Windows.Forms.ToolStripMenuItem incrementToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxIncrement;
        private System.Windows.Forms.ToolStripMenuItem smartIncrementToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripBody;
        private System.Windows.Forms.ToolStripMenuItem decimalPlacesToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxDecimalPlaces;
        private System.Windows.Forms.ToolStripMenuItem thousandsSeparatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRestrictLimit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxMaximum;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxMimimum;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem allowMouseContlolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxMouseSpeed;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxMouseDirection;
    }
}
