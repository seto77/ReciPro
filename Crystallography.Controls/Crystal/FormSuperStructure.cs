using System;
using System.Windows.Forms;

namespace Crystallography.Controls;
public partial class FormSuperStructure : Form
{
    public int A => numericBoxA.ValueInteger;
    public int B => numericBoxB.ValueInteger;
    public int C => numericBoxC.ValueInteger;

    public FormSuperStructure()
    {
        InitializeComponent();
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        Close();
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
