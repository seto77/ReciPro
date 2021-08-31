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
            this.components = new System.ComponentModel.Container();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.searchCrystalControl = new Crystallography.Controls.SearchCrystalControl();
            this.crystalDatabaseControl = new Crystallography.Controls.CrystalDatabaseControl();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSearch
            // 
            this.buttonSearch.AutoSize = true;
            this.buttonSearch.BackColor = System.Drawing.Color.Chocolate;
            this.buttonSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonSearch.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonSearch.Location = new System.Drawing.Point(0, 0);
            this.buttonSearch.Margin = new System.Windows.Forms.Padding(0);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(188, 33);
            this.buttonSearch.TabIndex = 2;
            this.buttonSearch.Text = "Search";
            this.toolTip.SetToolTip(this.buttonSearch, "Search crystals");
            this.buttonSearch.UseVisualStyleBackColor = false;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.searchCrystalControl);
            this.panel1.Controls.Add(this.buttonSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(727, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(188, 352);
            this.panel1.TabIndex = 3;
            // 
            // searchCrystalControl
            // 
            this.searchCrystalControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchCrystalControl.Location = new System.Drawing.Point(0, 33);
            this.searchCrystalControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.searchCrystalControl.Name = "searchCrystalControl";
            this.searchCrystalControl.Size = new System.Drawing.Size(188, 319);
            this.searchCrystalControl.TabIndex = 0;
            // 
            // crystalDatabaseControl
            // 
            this.crystalDatabaseControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalDatabaseControl.Filter = null;
            this.crystalDatabaseControl.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.crystalDatabaseControl.FontSize = 9.75F;
            this.crystalDatabaseControl.Location = new System.Drawing.Point(0, 0);
            this.crystalDatabaseControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.crystalDatabaseControl.Name = "crystalDatabaseControl";
            this.crystalDatabaseControl.Padding = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.crystalDatabaseControl.Size = new System.Drawing.Size(727, 352);
            this.crystalDatabaseControl.TabIndex = 1;
            this.crystalDatabaseControl.CrystalChanged += new System.EventHandler(this.crystalDatabaseControl_CrystalChanged);
            // 
            // FormCrystalDatabase
            // 
            this.AcceptButton = this.buttonSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 352);
            this.Controls.Add(this.crystalDatabaseControl);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormCrystalDatabase";
            this.ShowIcon = false;
            this.Text = "Crystal Database";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCrystalDatabase_FormClosing);
            this.Load += new System.EventHandler(this.FormCrystalDatabase_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Crystallography.Controls.SearchCrystalControl searchCrystalControl;
        private Crystallography.Controls.CrystalDatabaseControl crystalDatabaseControl;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip toolTip;
    }
}