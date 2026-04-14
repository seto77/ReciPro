namespace Crystallography.Controls
{
    partial class FormMacro
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>Clean up any resources being used.</summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            readToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            buttonRunMacro = new System.Windows.Forms.Button();
            dataGridView = new System.Windows.Forms.DataGridView();
            Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            buttonCancelStep = new System.Windows.Forms.Button();
            buttonNextStep = new System.Windows.Forms.Button();
            buttonStepByStep = new System.Windows.Forms.Button();
            splitContainer2 = new System.Windows.Forms.SplitContainer();
            splitContainer3 = new System.Windows.Forms.SplitContainer();
            listBoxMacro = new System.Windows.Forms.ListBox();
            flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            buttonAdd = new System.Windows.Forms.Button();
            buttonChange = new System.Windows.Forms.Button();
            flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            buttonUpper = new System.Windows.Forms.Button();
            buttonLower = new System.Windows.Forms.Button();
            buttonDeleteProfile = new System.Windows.Forms.Button();
            pyRichTextBox = new PyRichTextBox();
            panelGutter = new System.Windows.Forms.Panel();
            flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            label1 = new System.Windows.Forms.Label();
            textBoxMacroName = new System.Windows.Forms.TextBox();
            flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            label2 = new System.Windows.Forms.Label();
            dataGridViewDebug = new System.Windows.Forms.DataGridView();
            ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            statusLabelPos = new System.Windows.Forms.ToolStripStatusLabel();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDebug).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(784, 24);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { readToolStripMenuItem, saveToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // readToolStripMenuItem
            // 
            readToolStripMenuItem.Name = "readToolStripMenuItem";
            readToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            readToolStripMenuItem.Text = "Read file";
            readToolStripMenuItem.Click += readToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            saveToolStripMenuItem.Text = "Save file";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // buttonRunMacro
            // 
            buttonRunMacro.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            buttonRunMacro.AutoSize = true;
            buttonRunMacro.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonRunMacro.Font = new System.Drawing.Font("Segoe UI Symbol", 11.25F);
            buttonRunMacro.Location = new System.Drawing.Point(235, 0);
            buttonRunMacro.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            buttonRunMacro.Name = "buttonRunMacro";
            buttonRunMacro.Size = new System.Drawing.Size(90, 30);
            buttonRunMacro.TabIndex = 1;
            buttonRunMacro.Text = "Run macro";
            buttonRunMacro.UseVisualStyleBackColor = true;
            buttonRunMacro.Click += buttonRunMacro_Click;
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(220, 220, 255);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(20, 20, 255);
            dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { Column1, Column2 });
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(255, 220, 255);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Purple;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView.Location = new System.Drawing.Point(0, 0);
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridView.Size = new System.Drawing.Size(784, 188);
            dataGridView.TabIndex = 7;
            dataGridView.CellContentDoubleClick += dataGridView_CellContentDoubleClick;
            // 
            // Column1
            // 
            Column1.HeaderText = "Built-in functions or propeties";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 250;
            // 
            // Column2
            // 
            Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            Column2.HeaderText = "Help";
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 1);
            tableLayoutPanel1.Controls.Add(splitContainer2, 0, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new System.Drawing.Size(784, 323);
            tableLayoutPanel1.TabIndex = 8;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(buttonCancelStep);
            flowLayoutPanel1.Controls.Add(buttonRunMacro);
            flowLayoutPanel1.Controls.Add(buttonNextStep);
            flowLayoutPanel1.Controls.Add(buttonStepByStep);
            flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            flowLayoutPanel1.Location = new System.Drawing.Point(390, 293);
            flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new System.Drawing.Size(391, 30);
            flowLayoutPanel1.TabIndex = 9;
            // 
            // buttonCancelStep
            // 
            buttonCancelStep.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            buttonCancelStep.AutoSize = true;
            buttonCancelStep.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonCancelStep.BackColor = System.Drawing.Color.IndianRed;
            buttonCancelStep.Font = new System.Drawing.Font("Segoe UI Symbol", 11.25F);
            buttonCancelStep.ForeColor = System.Drawing.Color.White;
            buttonCancelStep.Location = new System.Drawing.Point(331, 0);
            buttonCancelStep.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            buttonCancelStep.Name = "buttonCancelStep";
            buttonCancelStep.Size = new System.Drawing.Size(57, 30);
            buttonCancelStep.TabIndex = 1;
            buttonCancelStep.Text = "Abort";
            buttonCancelStep.UseVisualStyleBackColor = false;
            buttonCancelStep.Visible = false;
            buttonCancelStep.Click += buttonCancelStep_Click;
            // 
            // buttonNextStep
            // 
            buttonNextStep.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            buttonNextStep.AutoSize = true;
            buttonNextStep.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonNextStep.Font = new System.Drawing.Font("Segoe UI Symbol", 11.25F);
            buttonNextStep.Location = new System.Drawing.Point(110, 0);
            buttonNextStep.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            buttonNextStep.Name = "buttonNextStep";
            buttonNextStep.Size = new System.Drawing.Size(119, 30);
            buttonNextStep.TabIndex = 1;
            buttonNextStep.Text = "Next step (F10)";
            buttonNextStep.UseVisualStyleBackColor = true;
            buttonNextStep.Visible = false;
            buttonNextStep.Click += buttonNextStep_Click;
            // 
            // buttonStepByStep
            // 
            buttonStepByStep.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            buttonStepByStep.AutoSize = true;
            buttonStepByStep.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonStepByStep.Font = new System.Drawing.Font("Segoe UI Symbol", 11.25F);
            buttonStepByStep.Location = new System.Drawing.Point(3, 0);
            buttonStepByStep.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            buttonStepByStep.Name = "buttonStepByStep";
            buttonStepByStep.Size = new System.Drawing.Size(101, 30);
            buttonStepByStep.TabIndex = 1;
            buttonStepByStep.Text = "Step by step";
            buttonStepByStep.UseVisualStyleBackColor = true;
            buttonStepByStep.Click += buttonStepByStep_Click;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer2.Location = new System.Drawing.Point(3, 3);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(dataGridViewDebug);
            splitContainer2.Panel2MinSize = 0;
            splitContainer2.Size = new System.Drawing.Size(778, 287);
            splitContainer2.SplitterDistance = 563;
            splitContainer2.SplitterWidth = 1;
            splitContainer2.TabIndex = 10;
            // 
            // splitContainer3
            // 
            splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            splitContainer3.Location = new System.Drawing.Point(0, 0);
            splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.Controls.Add(listBoxMacro);
            splitContainer3.Panel1.Controls.Add(flowLayoutPanel3);
            splitContainer3.Panel1.Controls.Add(flowLayoutPanel2);
            splitContainer3.Panel1.Font = new System.Drawing.Font("Segoe UI Symbol", 11.25F);
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.Controls.Add(pyRichTextBox);
            splitContainer3.Panel2.Controls.Add(panelGutter);
            splitContainer3.Panel2.Controls.Add(flowLayoutPanel4);
            splitContainer3.Panel2.Controls.Add(flowLayoutPanel5);
            splitContainer3.Size = new System.Drawing.Size(563, 287);
            splitContainer3.SplitterDistance = 162;
            splitContainer3.TabIndex = 7;
            // 
            // listBoxMacro
            // 
            listBoxMacro.Dock = System.Windows.Forms.DockStyle.Fill;
            listBoxMacro.FormattingEnabled = true;
            listBoxMacro.IntegralHeight = false;
            listBoxMacro.Location = new System.Drawing.Point(0, 30);
            listBoxMacro.Name = "listBoxMacro";
            listBoxMacro.Size = new System.Drawing.Size(162, 227);
            listBoxMacro.TabIndex = 0;
            listBoxMacro.SelectedIndexChanged += listBox_SelectedIndexChanged;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.AutoSize = true;
            flowLayoutPanel3.Controls.Add(buttonAdd);
            flowLayoutPanel3.Controls.Add(buttonChange);
            flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            flowLayoutPanel3.Location = new System.Drawing.Point(0, 257);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new System.Drawing.Size(162, 30);
            flowLayoutPanel3.TabIndex = 82;
            // 
            // buttonAdd
            // 
            buttonAdd.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonAdd.AutoSize = true;
            buttonAdd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonAdd.BackColor = System.Drawing.Color.SteelBlue;
            buttonAdd.Font = new System.Drawing.Font("Segoe UI Symbol", 11.25F);
            buttonAdd.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            buttonAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonAdd.Location = new System.Drawing.Point(0, 0);
            buttonAdd.Margin = new System.Windows.Forms.Padding(0);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            buttonAdd.RightToLeft = System.Windows.Forms.RightToLeft.No;
            buttonAdd.Size = new System.Drawing.Size(67, 30);
            buttonAdd.TabIndex = 80;
            buttonAdd.Text = "Add";
            buttonAdd.UseVisualStyleBackColor = false;
            buttonAdd.Click += buttonAddMacro_Click;
            // 
            // buttonChange
            // 
            buttonChange.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonChange.AutoSize = true;
            buttonChange.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonChange.BackColor = System.Drawing.Color.SteelBlue;
            buttonChange.Enabled = false;
            buttonChange.Font = new System.Drawing.Font("Segoe UI Symbol", 11.25F);
            buttonChange.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            buttonChange.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonChange.Location = new System.Drawing.Point(67, 0);
            buttonChange.Margin = new System.Windows.Forms.Padding(0);
            buttonChange.Name = "buttonChange";
            buttonChange.Size = new System.Drawing.Size(72, 30);
            buttonChange.TabIndex = 79;
            buttonChange.Text = "Replace";
            buttonChange.UseVisualStyleBackColor = false;
            buttonChange.Click += buttonChangeMacro_Click;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.AutoSize = true;
            flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanel2.Controls.Add(buttonUpper);
            flowLayoutPanel2.Controls.Add(buttonLower);
            flowLayoutPanel2.Controls.Add(buttonDeleteProfile);
            flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new System.Drawing.Size(162, 30);
            flowLayoutPanel2.TabIndex = 81;
            // 
            // buttonUpper
            // 
            buttonUpper.AutoSize = true;
            buttonUpper.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonUpper.Enabled = false;
            buttonUpper.Font = new System.Drawing.Font("Segoe UI Symbol", 11.25F);
            buttonUpper.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonUpper.Location = new System.Drawing.Point(0, 0);
            buttonUpper.Margin = new System.Windows.Forms.Padding(0);
            buttonUpper.Name = "buttonUpper";
            buttonUpper.Size = new System.Drawing.Size(26, 30);
            buttonUpper.TabIndex = 76;
            buttonUpper.Text = "↑";
            buttonUpper.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            buttonUpper.Click += buttonUpper_Click;
            // 
            // buttonLower
            // 
            buttonLower.AutoSize = true;
            buttonLower.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonLower.Enabled = false;
            buttonLower.Font = new System.Drawing.Font("Segoe UI Symbol", 11.25F);
            buttonLower.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonLower.Location = new System.Drawing.Point(26, 0);
            buttonLower.Margin = new System.Windows.Forms.Padding(0);
            buttonLower.Name = "buttonLower";
            buttonLower.Size = new System.Drawing.Size(26, 30);
            buttonLower.TabIndex = 78;
            buttonLower.Text = "↓";
            buttonLower.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            buttonLower.Click += buttonLower_Click;
            // 
            // buttonDeleteProfile
            // 
            buttonDeleteProfile.AutoSize = true;
            buttonDeleteProfile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonDeleteProfile.BackColor = System.Drawing.Color.IndianRed;
            buttonDeleteProfile.Enabled = false;
            buttonDeleteProfile.Font = new System.Drawing.Font("Segoe UI Symbol", 11.25F);
            buttonDeleteProfile.ForeColor = System.Drawing.Color.White;
            buttonDeleteProfile.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonDeleteProfile.Location = new System.Drawing.Point(72, 0);
            buttonDeleteProfile.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            buttonDeleteProfile.Name = "buttonDeleteProfile";
            buttonDeleteProfile.Size = new System.Drawing.Size(63, 30);
            buttonDeleteProfile.TabIndex = 77;
            buttonDeleteProfile.Text = "Delete";
            buttonDeleteProfile.UseVisualStyleBackColor = false;
            buttonDeleteProfile.Click += buttonDeleteMacro_Click;
            // 
            // pyRichTextBox
            // 
            pyRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            pyRichTextBox.Font = new System.Drawing.Font("メイリオ", 11.25F);
            pyRichTextBox.Location = new System.Drawing.Point(30, 28);
            pyRichTextBox.Name = "pyRichTextBox";
            pyRichTextBox.Size = new System.Drawing.Size(367, 243);
            pyRichTextBox.TabIndex = 6;
            pyRichTextBox.Text = "";
            pyRichTextBox.WordWrap = false;
            pyRichTextBox.TextChanged += markDirtyFromEdit;
            // 
            // panelGutter
            // 
            panelGutter.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            panelGutter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            panelGutter.Dock = System.Windows.Forms.DockStyle.Left;
            panelGutter.Location = new System.Drawing.Point(0, 28);
            panelGutter.Name = "panelGutter";
            panelGutter.Size = new System.Drawing.Size(30, 243);
            panelGutter.TabIndex = 7;
            // 
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.AutoSize = true;
            flowLayoutPanel4.Controls.Add(label1);
            flowLayoutPanel4.Controls.Add(textBoxMacroName);
            flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            flowLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            flowLayoutPanel4.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            flowLayoutPanel4.Size = new System.Drawing.Size(397, 28);
            flowLayoutPanel4.TabIndex = 9;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(3, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(82, 17);
            label1.TabIndex = 8;
            label1.Text = "Macro name";
            // 
            // textBoxMacroName
            // 
            textBoxMacroName.Location = new System.Drawing.Point(88, 0);
            textBoxMacroName.Margin = new System.Windows.Forms.Padding(0);
            textBoxMacroName.Name = "textBoxMacroName";
            textBoxMacroName.Size = new System.Drawing.Size(299, 25);
            textBoxMacroName.TabIndex = 7;
            textBoxMacroName.TextChanged += markDirtyFromEdit;
            // 
            // flowLayoutPanel5
            // 
            flowLayoutPanel5.AutoSize = true;
            flowLayoutPanel5.Controls.Add(label2);
            flowLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            flowLayoutPanel5.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            flowLayoutPanel5.Location = new System.Drawing.Point(0, 271);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            flowLayoutPanel5.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            flowLayoutPanel5.Size = new System.Drawing.Size(397, 16);
            flowLayoutPanel5.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label2.Location = new System.Drawing.Point(24, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(370, 13);
            label2.TabIndex = 8;
            label2.Text = "Use basic Python syntax and the built-in functions provided by ReciPro";
            // 
            // dataGridViewDebug
            // 
            dataGridViewDebug.AllowUserToAddRows = false;
            dataGridViewDebug.AllowUserToDeleteRows = false;
            dataGridViewDebug.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(255, 225, 220);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(255, 225, 220);
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewDebug.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewDebug.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewDebug.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { ColumnName, Column4 });
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(255, 192, 192);
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dataGridViewDebug.DefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridViewDebug.Location = new System.Drawing.Point(0, 0);
            dataGridViewDebug.MultiSelect = false;
            dataGridViewDebug.Name = "dataGridViewDebug";
            dataGridViewDebug.ReadOnly = true;
            dataGridViewDebug.RowHeadersVisible = false;
            dataGridViewDebug.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            dataGridViewDebug.Size = new System.Drawing.Size(214, 287);
            dataGridViewDebug.TabIndex = 7;
            // 
            // ColumnName
            // 
            ColumnName.HeaderText = "Name";
            ColumnName.Name = "ColumnName";
            ColumnName.ReadOnly = true;
            // 
            // Column4
            // 
            Column4.HeaderText = "Value";
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 24);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(dataGridView);
            splitContainer1.Size = new System.Drawing.Size(784, 515);
            splitContainer1.SplitterDistance = 323;
            splitContainer1.TabIndex = 9;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { statusLabelPos });
            statusStrip1.Location = new System.Drawing.Point(0, 539);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new System.Drawing.Size(784, 22);
            statusStrip1.TabIndex = 10;
            // 
            // statusLabelPos
            // 
            statusLabelPos.Name = "statusLabelPos";
            statusLabelPos.Size = new System.Drawing.Size(70, 17);
            statusLabelPos.Text = "Line 1, Col 1";
            // 
            // FormMacro
            // 
            AllowDrop = true;
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            ClientSize = new System.Drawing.Size(784, 561);
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            Name = "FormMacro";
            Text = "Macro";
            FormClosing += FormMacro_FormClosing;
            DragDrop += FormMacro_DragDrop;
            DragEnter += FormMacro_DragEnter;
            KeyDown += FormMacro_KeyDown;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel1.PerformLayout();
            splitContainer3.Panel2.ResumeLayout(false);
            splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
            splitContainer3.ResumeLayout(false);
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            flowLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel4.PerformLayout();
            flowLayoutPanel5.ResumeLayout(false);
            flowLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDebug).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.Button buttonRunMacro;
        private PyRichTextBox pyRichTextBox;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button buttonStepByStep;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dataGridViewDebug;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.SplitContainer splitContainer3;
        public System.Windows.Forms.Button buttonAdd;
        public System.Windows.Forms.Button buttonChange;
        private System.Windows.Forms.Button buttonDeleteProfile;
        private System.Windows.Forms.Button buttonUpper;
        private System.Windows.Forms.Button buttonLower;
        private System.Windows.Forms.ListBox listBoxMacro;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxMacroName;
        private System.Windows.Forms.Button buttonNextStep;
        private System.Windows.Forms.Button buttonCancelStep;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        // 260414Cl 追加
        private System.Windows.Forms.Panel panelGutter;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelPos;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.Label label2;
    }
}