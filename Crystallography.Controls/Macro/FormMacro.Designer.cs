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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMacro));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            splitContainer3 = new System.Windows.Forms.SplitContainer();
            listBoxMacro = new System.Windows.Forms.ListBox();
            flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            buttonDeleteProfile = new System.Windows.Forms.Button();
            buttonAdd = new System.Windows.Forms.Button();
            buttonChange = new System.Windows.Forms.Button();
            flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            buttonUpper = new System.Windows.Forms.Button();
            buttonLower = new System.Windows.Forms.Button();
            checkBoxSamples = new System.Windows.Forms.CheckBox();
            pyRichTextBox = new PyRichTextBox();
            panelGutter = new System.Windows.Forms.Panel();
            flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            label1 = new System.Windows.Forms.Label();
            textBoxMacroName = new System.Windows.Forms.TextBox();
            flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            label2 = new System.Windows.Forms.Label();
            splitContainer2 = new System.Windows.Forms.SplitContainer();
            dataGridViewDebug = new System.Windows.Forms.DataGridView();
            ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            buttonCancelStep = new System.Windows.Forms.Button();
            buttonRunMacro = new System.Windows.Forms.Button();
            buttonNextStep = new System.Windows.Forms.Button();
            buttonStepByStep = new System.Windows.Forms.Button();
            dataGridView = new System.Windows.Forms.DataGridView();
            Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            readToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            statusLabelPos = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDebug).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer3
            // 
            resources.ApplyResources(splitContainer3, "splitContainer3");
            splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.Controls.Add(listBoxMacro);
            splitContainer3.Panel1.Controls.Add(flowLayoutPanel3);
            splitContainer3.Panel1.Controls.Add(flowLayoutPanel2);
            resources.ApplyResources(splitContainer3.Panel1, "splitContainer3.Panel1");
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.Controls.Add(pyRichTextBox);
            splitContainer3.Panel2.Controls.Add(panelGutter);
            splitContainer3.Panel2.Controls.Add(flowLayoutPanel4);
            splitContainer3.Panel2.Controls.Add(flowLayoutPanel5);
            // 
            // listBoxMacro
            // 
            resources.ApplyResources(listBoxMacro, "listBoxMacro");
            listBoxMacro.FormattingEnabled = true;
            listBoxMacro.Name = "listBoxMacro";
            listBoxMacro.SelectedIndexChanged += listBox_SelectedIndexChanged;
            // 
            // flowLayoutPanel3
            // 
            resources.ApplyResources(flowLayoutPanel3, "flowLayoutPanel3");
            flowLayoutPanel3.Controls.Add(buttonDeleteProfile);
            flowLayoutPanel3.Controls.Add(buttonAdd);
            flowLayoutPanel3.Controls.Add(buttonChange);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            // 
            // buttonDeleteProfile
            // 
            resources.ApplyResources(buttonDeleteProfile, "buttonDeleteProfile");
            buttonDeleteProfile.BackColor = System.Drawing.Color.IndianRed;
            buttonDeleteProfile.ForeColor = System.Drawing.Color.White;
            buttonDeleteProfile.Name = "buttonDeleteProfile";
            buttonDeleteProfile.UseVisualStyleBackColor = false;
            buttonDeleteProfile.Click += buttonDeleteMacro_Click;
            // 
            // buttonAdd
            // 
            resources.ApplyResources(buttonAdd, "buttonAdd");
            buttonAdd.BackColor = System.Drawing.Color.SteelBlue;
            buttonAdd.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            buttonAdd.Name = "buttonAdd";
            buttonAdd.UseVisualStyleBackColor = false;
            buttonAdd.Click += buttonAddMacro_Click;
            // 
            // buttonChange
            // 
            resources.ApplyResources(buttonChange, "buttonChange");
            buttonChange.BackColor = System.Drawing.Color.SteelBlue;
            buttonChange.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            buttonChange.Name = "buttonChange";
            buttonChange.UseVisualStyleBackColor = false;
            buttonChange.Click += buttonChangeMacro_Click;
            // 
            // flowLayoutPanel2
            // 
            resources.ApplyResources(flowLayoutPanel2, "flowLayoutPanel2");
            flowLayoutPanel2.Controls.Add(buttonUpper);
            flowLayoutPanel2.Controls.Add(buttonLower);
            flowLayoutPanel2.Controls.Add(checkBoxSamples);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            // 
            // buttonUpper
            // 
            resources.ApplyResources(buttonUpper, "buttonUpper");
            buttonUpper.Name = "buttonUpper";
            buttonUpper.Click += buttonUpper_Click;
            // 
            // buttonLower
            // 
            resources.ApplyResources(buttonLower, "buttonLower");
            buttonLower.Name = "buttonLower";
            buttonLower.Click += buttonLower_Click;
            // 
            // checkBoxSamples
            // 
            resources.ApplyResources(checkBoxSamples, "checkBoxSamples");
            checkBoxSamples.Name = "checkBoxSamples";
            checkBoxSamples.UseVisualStyleBackColor = true;
            checkBoxSamples.CheckedChanged += toggleSamplesMode;
            // 
            // pyRichTextBox
            // 
            resources.ApplyResources(pyRichTextBox, "pyRichTextBox");
            pyRichTextBox.Name = "pyRichTextBox";
            pyRichTextBox.TextChanged += markDirtyFromEdit;
            // 
            // panelGutter
            // 
            panelGutter.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            panelGutter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(panelGutter, "panelGutter");
            panelGutter.Name = "panelGutter";
            // 
            // flowLayoutPanel4
            // 
            resources.ApplyResources(flowLayoutPanel4, "flowLayoutPanel4");
            flowLayoutPanel4.Controls.Add(label1);
            flowLayoutPanel4.Controls.Add(textBoxMacroName);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // textBoxMacroName
            // 
            resources.ApplyResources(textBoxMacroName, "textBoxMacroName");
            textBoxMacroName.Name = "textBoxMacroName";
            textBoxMacroName.TextChanged += markDirtyFromEdit;
            // 
            // flowLayoutPanel5
            // 
            resources.ApplyResources(flowLayoutPanel5, "flowLayoutPanel5");
            flowLayoutPanel5.Controls.Add(label2);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // splitContainer2
            // 
            resources.ApplyResources(splitContainer2, "splitContainer2");
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(dataGridViewDebug);
            // 
            // dataGridViewDebug
            // 
            dataGridViewDebug.AllowUserToAddRows = false;
            dataGridViewDebug.AllowUserToDeleteRows = false;
            dataGridViewDebug.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(255, 225, 220);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(255, 225, 220);
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewDebug.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewDebug.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewDebug.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { ColumnName, Column4 });
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("メイリオ", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(255, 192, 192);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dataGridViewDebug.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(dataGridViewDebug, "dataGridViewDebug");
            dataGridViewDebug.MultiSelect = false;
            dataGridViewDebug.Name = "dataGridViewDebug";
            dataGridViewDebug.ReadOnly = true;
            dataGridViewDebug.RowHeadersVisible = false;
            dataGridViewDebug.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            // 
            // ColumnName
            // 
            resources.ApplyResources(ColumnName, "ColumnName");
            ColumnName.Name = "ColumnName";
            ColumnName.ReadOnly = true;
            // 
            // Column4
            // 
            resources.ApplyResources(Column4, "Column4");
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            // 
            // splitContainer1
            // 
            resources.ApplyResources(splitContainer1, "splitContainer1");
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(dataGridView);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(tableLayoutPanel1, "tableLayoutPanel1");
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 1);
            tableLayoutPanel1.Controls.Add(splitContainer2, 0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(buttonCancelStep);
            flowLayoutPanel1.Controls.Add(buttonRunMacro);
            flowLayoutPanel1.Controls.Add(buttonNextStep);
            flowLayoutPanel1.Controls.Add(buttonStepByStep);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // buttonCancelStep
            // 
            resources.ApplyResources(buttonCancelStep, "buttonCancelStep");
            buttonCancelStep.BackColor = System.Drawing.Color.IndianRed;
            buttonCancelStep.ForeColor = System.Drawing.Color.White;
            buttonCancelStep.Name = "buttonCancelStep";
            buttonCancelStep.UseVisualStyleBackColor = false;
            buttonCancelStep.Click += buttonCancelStep_Click;
            // 
            // buttonRunMacro
            // 
            resources.ApplyResources(buttonRunMacro, "buttonRunMacro");
            buttonRunMacro.Name = "buttonRunMacro";
            buttonRunMacro.UseVisualStyleBackColor = true;
            buttonRunMacro.Click += buttonRunMacro_Click;
            // 
            // buttonNextStep
            // 
            resources.ApplyResources(buttonNextStep, "buttonNextStep");
            buttonNextStep.Name = "buttonNextStep";
            buttonNextStep.UseVisualStyleBackColor = true;
            buttonNextStep.Click += buttonNextStep_Click;
            // 
            // buttonStepByStep
            // 
            resources.ApplyResources(buttonStepByStep, "buttonStepByStep");
            buttonStepByStep.Name = "buttonStepByStep";
            buttonStepByStep.UseVisualStyleBackColor = true;
            buttonStepByStep.Click += buttonStepByStep_Click;
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(220, 220, 255);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(20, 20, 255);
            dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { Column1, Column2 });
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(255, 220, 255);
            dataGridViewCellStyle4.Font = new System.Drawing.Font("メイリオ", 9F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Purple;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dataGridView.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(dataGridView, "dataGridView");
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridView.CellContentDoubleClick += dataGridView_CellContentDoubleClick;
            // 
            // Column1
            // 
            resources.ApplyResources(Column1, "Column1");
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            // 
            // Column2
            // 
            Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(Column2, "Column2");
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem });
            resources.ApplyResources(menuStrip1, "menuStrip1");
            menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { readToolStripMenuItem, saveToolStripMenuItem });
            resources.ApplyResources(fileToolStripMenuItem, "fileToolStripMenuItem");
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            // 
            // readToolStripMenuItem
            // 
            readToolStripMenuItem.Name = "readToolStripMenuItem";
            resources.ApplyResources(readToolStripMenuItem, "readToolStripMenuItem");
            readToolStripMenuItem.Click += readToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            resources.ApplyResources(saveToolStripMenuItem, "saveToolStripMenuItem");
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { statusLabelPos });
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Name = "statusStrip1";
            // 
            // statusLabelPos
            // 
            statusLabelPos.Name = "statusLabelPos";
            resources.ApplyResources(statusLabelPos, "statusLabelPos");
            // 
            // FormMacro
            // 
            AllowDrop = true;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            Name = "FormMacro";
            FormClosing += FormMacro_FormClosing;
            DragDrop += FormMacro_DragDrop;
            DragEnter += FormMacro_DragEnter;
            KeyDown += FormMacro_KeyDown;
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
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewDebug).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
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
        private System.Windows.Forms.CheckBox checkBoxSamples;
    }
}