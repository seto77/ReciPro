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
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(searchCrystalControl);
            panel1.Dock = System.Windows.Forms.DockStyle.Right;
            panel1.Location = new System.Drawing.Point(727, 0);
            panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(188, 352);
            panel1.TabIndex = 3;
            // 
            // searchCrystalControl
            // 
            searchCrystalControl.Dock = System.Windows.Forms.DockStyle.Fill;
            searchCrystalControl.Location = new System.Drawing.Point(0, 0);
            searchCrystalControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            searchCrystalControl.Name = "searchCrystalControl";
            searchCrystalControl.Size = new System.Drawing.Size(188, 352);
            searchCrystalControl.TabIndex = 0;
            searchCrystalControl.VisibleChanged += searchCrystalControl_VisibleChanged;
            // 
            // crystalDatabaseControl
            // 
            crystalDatabaseControl.Dock = System.Windows.Forms.DockStyle.Fill;
            crystalDatabaseControl.Filter = null;
            crystalDatabaseControl.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            crystalDatabaseControl.FontSize = 9.75F;
            crystalDatabaseControl.Location = new System.Drawing.Point(0, 0);
            crystalDatabaseControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            crystalDatabaseControl.Name = "crystalDatabaseControl";
            crystalDatabaseControl.Padding = new System.Windows.Forms.Padding(0, 0, 6, 0);
            crystalDatabaseControl.Size = new System.Drawing.Size(727, 352);
            crystalDatabaseControl.TabIndex = 1;
            crystalDatabaseControl.CrystalChanged += CrystalDatabaseControl_CrystalChanged;
            // 
            // FormCrystalDatabase
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(915, 352);
            Controls.Add(crystalDatabaseControl);
            Controls.Add(panel1);
            Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "FormCrystalDatabase";
            ShowIcon = false;
            Text = "Crystal Database";
            FormClosing += FormCrystalDatabase_FormClosing;
            Load += FormCrystalDatabase_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Crystallography.Controls.SearchCrystalControl searchCrystalControl;
        private Crystallography.Controls.CrystalDatabaseControl crystalDatabaseControl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip toolTip;
    }
}