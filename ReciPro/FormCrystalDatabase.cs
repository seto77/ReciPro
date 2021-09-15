using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReciPro
{
    public partial class FormCrystalDatabase : Form
    {
        public FormMain FormMain;
        public FormCrystalDatabase()
        {
            InitializeComponent();
        }

        private void FormCrystalDatabase_Load(object sender, EventArgs e)
        {
            if (File.Exists(FormMain.UserAppDataPath + "StdDB.cdb3"))
                crystalDatabaseControl.ReadDatabase(FormMain.UserAppDataPath + "StdDB.cdb3");
        }

        private void buttonSearch_Click(object sender, EventArgs e)
            => crystalDatabaseControl.Filter = searchCrystalControl.Filter;

        private void FormCrystalDatabase_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            FormMain.toolStripButtonDatabase.Checked = false;
            this.Visible = false;
        }

        private void crystalDatabaseControl_CrystalChanged(object sender, EventArgs e)
            => FormMain.crystalControl.Crystal = crystalDatabaseControl.Crystal;
    }
}
