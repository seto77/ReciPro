using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReciPro;

public partial class FormEBSD : Form
{
    public FormMain FormMain;

    #region コンストラクタ、Load, Closing
    public FormEBSD()
    {
        InitializeComponent();
    }

    private void FormEBSD_Load(object sender, EventArgs e)
    {

    }

    private void FormEBSD_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        FormMain.toolStripButtonEBSD.Checked = false;
        this.Visible = false;
    }
    #endregion

}
