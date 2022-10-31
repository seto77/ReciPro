using System.IO;
using System.Windows.Forms;

namespace ReciPro;

public partial class FormCrystalDatabase : Form
{
    public FormMain FormMain;
    public FormCrystalDatabase()
    {
        InitializeComponent();

        searchCrystalControl.CrystalDatabaseControl = crystalDatabaseControl;

        this.AcceptButton = searchCrystalControl.buttonSearch;
    }

    private void FormCrystalDatabase_Load(object sender, EventArgs e)
    {
        if (File.Exists(FormMain.UserAppDataPath + "StdDB.cdb3"))
            crystalDatabaseControl.ReadDatabase(FormMain.UserAppDataPath + "StdDB.cdb3");
    }

    private void FormCrystalDatabase_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        FormMain.toolStripButtonDatabase.Checked = false;
        Visible = false;
    }

    private void CrystalDatabaseControl_CrystalChanged(object sender, EventArgs e)
        => FormMain.crystalControl.Crystal = crystalDatabaseControl.Crystal;
}
