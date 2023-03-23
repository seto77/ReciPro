namespace ReciPro
{
    partial class FormPresets
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            components = new System.ComponentModel.Container();
            listBox = new System.Windows.Forms.ListBox();
            textBoxPresetName = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            buttonDelete = new System.Windows.Forms.Button();
            buttonAdd = new System.Windows.Forms.Button();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            buttonReplace = new System.Windows.Forms.Button();
            buttonRename = new System.Windows.Forms.Button();
            flowLayoutPanelOkCancel = new System.Windows.Forms.FlowLayoutPanel();
            buttonOK = new System.Windows.Forms.Button();
            buttonCancel = new System.Windows.Forms.Button();
            panelManageList = new System.Windows.Forms.Panel();
            panel2 = new System.Windows.Forms.Panel();
            checkBoxManageList = new System.Windows.Forms.CheckBox();
            toolTip = new System.Windows.Forms.ToolTip(components);
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanelOkCancel.SuspendLayout();
            panelManageList.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // listBox
            // 
            listBox.Dock = System.Windows.Forms.DockStyle.Fill;
            listBox.FormattingEnabled = true;
            listBox.IntegralHeight = false;
            listBox.ItemHeight = 17;
            listBox.Location = new System.Drawing.Point(4, 5);
            listBox.Name = "listBox";
            listBox.Size = new System.Drawing.Size(233, 208);
            listBox.TabIndex = 1;
            listBox.SelectedIndexChanged += listBox_SelectedIndexChanged;
            // 
            // textBoxPresetName
            // 
            textBoxPresetName.Dock = System.Windows.Forms.DockStyle.Fill;
            textBoxPresetName.Location = new System.Drawing.Point(43, 0);
            textBoxPresetName.Name = "textBoxPresetName";
            textBoxPresetName.Size = new System.Drawing.Size(190, 25);
            textBoxPresetName.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = System.Windows.Forms.DockStyle.Left;
            label1.Location = new System.Drawing.Point(0, 0);
            label1.Name = "label1";
            label1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            label1.Size = new System.Drawing.Size(43, 20);
            label1.TabIndex = 3;
            label1.Text = "Name";
            // 
            // buttonDelete
            // 
            buttonDelete.AutoSize = true;
            buttonDelete.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonDelete.BackColor = System.Drawing.Color.IndianRed;
            buttonDelete.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            buttonDelete.ForeColor = System.Drawing.Color.White;
            buttonDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonDelete.Location = new System.Drawing.Point(171, 0);
            buttonDelete.Margin = new System.Windows.Forms.Padding(0);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new System.Drawing.Size(55, 27);
            buttonDelete.TabIndex = 8;
            buttonDelete.Text = "Delete";
            toolTip.SetToolTip(buttonDelete, "Delete the seletcted preset");
            buttonDelete.UseVisualStyleBackColor = false;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // buttonAdd
            // 
            buttonAdd.AutoSize = true;
            buttonAdd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonAdd.BackColor = System.Drawing.Color.SteelBlue;
            buttonAdd.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            buttonAdd.ForeColor = System.Drawing.SystemColors.HighlightText;
            buttonAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            buttonAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonAdd.Location = new System.Drawing.Point(0, 0);
            buttonAdd.Margin = new System.Windows.Forms.Padding(0);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new System.Drawing.Size(42, 27);
            buttonAdd.TabIndex = 6;
            buttonAdd.Text = "Add";
            toolTip.SetToolTip(buttonAdd, "Add the current setting as a new preset");
            buttonAdd.UseVisualStyleBackColor = false;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(buttonAdd);
            flowLayoutPanel1.Controls.Add(buttonReplace);
            flowLayoutPanel1.Controls.Add(buttonRename);
            flowLayoutPanel1.Controls.Add(buttonDelete);
            flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new System.Drawing.Size(233, 27);
            flowLayoutPanel1.TabIndex = 9;
            // 
            // buttonReplace
            // 
            buttonReplace.AutoSize = true;
            buttonReplace.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonReplace.BackColor = System.Drawing.Color.SteelBlue;
            buttonReplace.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            buttonReplace.ForeColor = System.Drawing.SystemColors.HighlightText;
            buttonReplace.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            buttonReplace.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonReplace.Location = new System.Drawing.Point(42, 0);
            buttonReplace.Margin = new System.Windows.Forms.Padding(0);
            buttonReplace.Name = "buttonReplace";
            buttonReplace.Size = new System.Drawing.Size(64, 27);
            buttonReplace.TabIndex = 6;
            buttonReplace.Text = "Replace";
            toolTip.SetToolTip(buttonReplace, "Replace the selected preset with the current setting");
            buttonReplace.UseVisualStyleBackColor = false;
            buttonReplace.Click += buttonAdd_Click;
            // 
            // buttonRename
            // 
            buttonRename.AutoSize = true;
            buttonRename.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonRename.BackColor = System.Drawing.Color.SteelBlue;
            buttonRename.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            buttonRename.ForeColor = System.Drawing.SystemColors.HighlightText;
            buttonRename.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            buttonRename.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonRename.Location = new System.Drawing.Point(106, 0);
            buttonRename.Margin = new System.Windows.Forms.Padding(0);
            buttonRename.Name = "buttonRename";
            buttonRename.Size = new System.Drawing.Size(65, 27);
            buttonRename.TabIndex = 6;
            buttonRename.Text = "Rename";
            toolTip.SetToolTip(buttonRename, "Rename the selected preset");
            buttonRename.UseVisualStyleBackColor = false;
            buttonRename.Click += buttonRename_Click;
            // 
            // flowLayoutPanelOkCancel
            // 
            flowLayoutPanelOkCancel.AutoSize = true;
            flowLayoutPanelOkCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanelOkCancel.Controls.Add(buttonOK);
            flowLayoutPanelOkCancel.Controls.Add(buttonCancel);
            flowLayoutPanelOkCancel.Dock = System.Windows.Forms.DockStyle.Bottom;
            flowLayoutPanelOkCancel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            flowLayoutPanelOkCancel.Location = new System.Drawing.Point(4, 287);
            flowLayoutPanelOkCancel.Margin = new System.Windows.Forms.Padding(0);
            flowLayoutPanelOkCancel.Name = "flowLayoutPanelOkCancel";
            flowLayoutPanelOkCancel.Size = new System.Drawing.Size(233, 27);
            flowLayoutPanelOkCancel.TabIndex = 10;
            // 
            // buttonOK
            // 
            buttonOK.AutoSize = true;
            buttonOK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonOK.Location = new System.Drawing.Point(197, 0);
            buttonOK.Margin = new System.Windows.Forms.Padding(0);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new System.Drawing.Size(36, 27);
            buttonOK.TabIndex = 0;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += buttonOK_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.AutoSize = true;
            buttonCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonCancel.Location = new System.Drawing.Point(141, 0);
            buttonCancel.Margin = new System.Windows.Forms.Padding(0);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new System.Drawing.Size(56, 27);
            buttonCancel.TabIndex = 0;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // panelManageList
            // 
            panelManageList.AutoSize = true;
            panelManageList.Controls.Add(panel2);
            panelManageList.Controls.Add(flowLayoutPanel1);
            panelManageList.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelManageList.Location = new System.Drawing.Point(4, 234);
            panelManageList.Name = "panelManageList";
            panelManageList.Size = new System.Drawing.Size(233, 53);
            panelManageList.TabIndex = 11;
            panelManageList.Visible = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(textBoxPresetName);
            panel2.Controls.Add(label1);
            panel2.Dock = System.Windows.Forms.DockStyle.Top;
            panel2.Location = new System.Drawing.Point(0, 27);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(233, 26);
            panel2.TabIndex = 11;
            // 
            // checkBoxManageList
            // 
            checkBoxManageList.AutoSize = true;
            checkBoxManageList.Dock = System.Windows.Forms.DockStyle.Bottom;
            checkBoxManageList.Location = new System.Drawing.Point(4, 213);
            checkBoxManageList.Name = "checkBoxManageList";
            checkBoxManageList.Size = new System.Drawing.Size(233, 21);
            checkBoxManageList.TabIndex = 10;
            checkBoxManageList.Text = "Manage the preset list";
            checkBoxManageList.UseVisualStyleBackColor = true;
            checkBoxManageList.CheckedChanged += checkBoxManageList_CheckedChanged;
            // 
            // FormPresets
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(241, 319);
            ControlBox = false;
            Controls.Add(listBox);
            Controls.Add(checkBoxManageList);
            Controls.Add(panelManageList);
            Controls.Add(flowLayoutPanelOkCancel);
            Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormPresets";
            Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Preset list";
            FormClosing += FormPresets_FormClosing;
            VisibleChanged += FormPresets_VisibleChanged;
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            flowLayoutPanelOkCancel.ResumeLayout(false);
            flowLayoutPanelOkCancel.PerformLayout();
            panelManageList.ResumeLayout(false);
            panelManageList.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.TextBox textBoxPresetName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        public System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelOkCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panelManageList;
        private System.Windows.Forms.CheckBox checkBoxManageList;
        private System.Windows.Forms.Button buttonReplace;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonRename;
        private System.Windows.Forms.ToolTip toolTip;
    }
}