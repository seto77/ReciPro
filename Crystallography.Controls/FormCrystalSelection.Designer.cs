namespace Crystallography.Controls
{
    partial class FormCrystalSelection
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
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.buttonCheckAll = new System.Windows.Forms.Button();
            this.buttonUnchekAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonLoadOrSave = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonExpand = new System.Windows.Forms.Button();
            this.crystalControl1 = new Crystallography.Controls.CrystalControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBox1.ColumnWidth = 120;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.IntegralHeight = false;
            this.checkedListBox1.Location = new System.Drawing.Point(2, 24);
            this.checkedListBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkedListBox1.MultiColumn = true;
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(458, 193);
            this.checkedListBox1.TabIndex = 0;
            this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // buttonCheckAll
            // 
            this.buttonCheckAll.Location = new System.Drawing.Point(0, 0);
            this.buttonCheckAll.Name = "buttonCheckAll";
            this.buttonCheckAll.Size = new System.Drawing.Size(120, 23);
            this.buttonCheckAll.TabIndex = 2;
            this.buttonCheckAll.Text = "Check All Items";
            this.buttonCheckAll.UseVisualStyleBackColor = true;
            this.buttonCheckAll.Click += new System.EventHandler(this.buttonCheckAll_Click);
            // 
            // buttonUnchekAll
            // 
            this.buttonUnchekAll.Location = new System.Drawing.Point(126, 0);
            this.buttonUnchekAll.Name = "buttonUnchekAll";
            this.buttonUnchekAll.Size = new System.Drawing.Size(120, 23);
            this.buttonUnchekAll.TabIndex = 2;
            this.buttonUnchekAll.Text = "Uncheck All Items";
            this.buttonUnchekAll.UseVisualStyleBackColor = true;
            this.buttonUnchekAll.Click += new System.EventHandler(this.buttonUnchekAll_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Location = new System.Drawing.Point(6, 218);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 31);
            this.label1.TabIndex = 3;
            this.label1.Text = "The checked items are being loaded / saved.";
            // 
            // buttonLoadOrSave
            // 
            this.buttonLoadOrSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLoadOrSave.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonLoadOrSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonLoadOrSave.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLoadOrSave.ForeColor = System.Drawing.Color.White;
            this.buttonLoadOrSave.Location = new System.Drawing.Point(270, 221);
            this.buttonLoadOrSave.Name = "buttonLoadOrSave";
            this.buttonLoadOrSave.Size = new System.Drawing.Size(90, 28);
            this.buttonLoadOrSave.TabIndex = 2;
            this.buttonLoadOrSave.Text = "Load";
            this.buttonLoadOrSave.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.SteelBlue;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(360, 221);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 28);
            this.button1.TabIndex = 2;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // buttonExpand
            // 
            this.buttonExpand.BackColor = System.Drawing.Color.Orange;
            this.buttonExpand.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonExpand.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExpand.ForeColor = System.Drawing.Color.Moccasin;
            this.buttonExpand.Location = new System.Drawing.Point(460, 0);
            this.buttonExpand.Name = "buttonExpand";
            this.buttonExpand.Size = new System.Drawing.Size(22, 252);
            this.buttonExpand.TabIndex = 4;
            this.buttonExpand.Text = ">>>>>>>>>>>>>>";
            this.buttonExpand.UseVisualStyleBackColor = false;
            this.buttonExpand.Click += new System.EventHandler(this.buttonExpand_Click);
            // 
            // crystalControl1
            // 
            this.crystalControl1.AllowDrop = true;
            this.crystalControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.crystalControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.crystalControl1.Crystal = null;
            this.crystalControl1.Location = new System.Drawing.Point(482, 0);
            this.crystalControl1.Margin = new System.Windows.Forms.Padding(0);
            this.crystalControl1.Name = "crystalControl1";
            this.crystalControl1.Size = new System.Drawing.Size(486, 256);
            this.crystalControl1.TabIndex = 1;
            this.crystalControl1.VisibleAtomTab = true;
            this.crystalControl1.VisibleBasicInfoTab = true;
            this.crystalControl1.VisibleBondsPolyhedraTab = true;
            this.crystalControl1.VisibleEOSTab = true;
            this.crystalControl1.VisibleReferenceTab = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.buttonExpand);
            this.panel1.Controls.Add(this.buttonCheckAll);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.checkedListBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonUnchekAll);
            this.panel1.Controls.Add(this.buttonLoadOrSave);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(482, 252);
            this.panel1.TabIndex = 6;
            // 
            // FormCrystalSelection
            // 
            this.AcceptButton = this.buttonLoadOrSave;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(970, 254);
            this.Controls.Add(this.crystalControl1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormCrystalSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Select load / save items";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalControl crystalControl1;
        private System.Windows.Forms.Button buttonCheckAll;
        private System.Windows.Forms.Button buttonUnchekAll;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button buttonLoadOrSave;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonExpand;
        private System.Windows.Forms.Panel panel1;
    }
}