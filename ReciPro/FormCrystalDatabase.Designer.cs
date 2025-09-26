namespace ReciPro
{
    partial class FormCrystalDatabase
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
            panel1 = new System.Windows.Forms.Panel();
            searchCrystalControl = new SearchCrystalControl();
            crystalDatabaseControl = new CrystalDatabaseControl();
            toolTip = new System.Windows.Forms.ToolTip(components);
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            panel1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(searchCrystalControl);
            panel1.Dock = System.Windows.Forms.DockStyle.Right;
            panel1.Location = new System.Drawing.Point(727, 0);
            panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(188, 378);
            panel1.TabIndex = 3;
            // 
            // searchCrystalControl
            // 
            searchCrystalControl.CrystalDatabaseControl = null;
            searchCrystalControl.Dock = System.Windows.Forms.DockStyle.Fill;
            searchCrystalControl.Location = new System.Drawing.Point(0, 0);
            searchCrystalControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            searchCrystalControl.Name = "searchCrystalControl";
            searchCrystalControl.Size = new System.Drawing.Size(188, 378);
            searchCrystalControl.TabIndex = 0;
            searchCrystalControl.ProgressChanged += searchCrystalControl_ProgressChanged;
            searchCrystalControl.VisibleChanged += searchCrystalControl_VisibleChanged;
            // 
            // crystalDatabaseControl
            // 
            crystalDatabaseControl.AMCSD_Checked = false;
            crystalDatabaseControl.AMCSD_Has_Read = false;
            crystalDatabaseControl.COD_Checked = false;
            crystalDatabaseControl.COD_Has_Read = false;
            crystalDatabaseControl.DatabaseSelection = true;
            crystalDatabaseControl.Dock = System.Windows.Forms.DockStyle.Fill;
            crystalDatabaseControl.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            crystalDatabaseControl.FontSize = 9.75F;
            crystalDatabaseControl.Location = new System.Drawing.Point(0, 0);
            crystalDatabaseControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            crystalDatabaseControl.Name = "crystalDatabaseControl";
            crystalDatabaseControl.Padding = new System.Windows.Forms.Padding(0, 0, 6, 0);
            crystalDatabaseControl.SearchFilter = null;
            crystalDatabaseControl.Size = new System.Drawing.Size(727, 378);
            crystalDatabaseControl.TabIndex = 1;
            crystalDatabaseControl.CrystalChanged += CrystalDatabaseControl_CrystalChanged;
            crystalDatabaseControl.ProgressChanged += crystalDatabaseControl_ProgressChanged;
            // 
            // statusStrip1
            // 
            statusStrip1.Font = new System.Drawing.Font("Yu Gothic UI", 8F);
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripProgressBar1, toolStripStatusLabel1 });
            statusStrip1.Location = new System.Drawing.Point(0, 378);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new System.Drawing.Size(915, 22);
            statusStrip1.TabIndex = 4;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            toolStripProgressBar1.Name = "toolStripProgressBar1";
            toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new System.Drawing.Size(13, 17);
            toolStripStatusLabel1.Text = "  ";
            // 
            // FormCrystalDatabase
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(915, 400);
            Controls.Add(crystalDatabaseControl);
            Controls.Add(panel1);
            Controls.Add(statusStrip1);
            Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "FormCrystalDatabase";
            ShowIcon = false;
            Text = "Crystal Database";
            FormClosing += FormCrystalDatabase_FormClosing;
            Load += FormCrystalDatabase_Load;
            panel1.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Crystallography.Controls.SearchCrystalControl searchCrystalControl;
        private Crystallography.Controls.CrystalDatabaseControl crystalDatabaseControl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}