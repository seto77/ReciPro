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
        crystalDatabaseControl.AMCSD_Checked = true;
    }

    private void FormCrystalDatabase_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        FormMain.toolStripButtonDatabase.Checked = false;
        Visible = false;
    }

    private void CrystalDatabaseControl_CrystalChanged(object sender, EventArgs e)
        => FormMain.crystalControl.Crystal = crystalDatabaseControl.Crystal;

    private void searchCrystalControl_VisibleChanged(object sender, EventArgs e)
    {
        if (Visible)
            FormMain.toolStripButtonDatabase.Checked = true;
    }

    private void crystalDatabaseControl_DataBaseChanged(object sender, EventArgs e)
    {

    }

    private void crystalDatabaseControl_ProgressChanged(object sender, double progress, string message)
    {
        toolStripStatusLabel1.Text= message;
        toolStripProgressBar1.Value = (int)(progress * 100);
    }

    private void searchCrystalControl_ProgressChanged(object sender, double progress, string message)
    {
        toolStripStatusLabel1.Text = message;
        toolStripProgressBar1.Value = (int)(progress * 100);
    }
}
