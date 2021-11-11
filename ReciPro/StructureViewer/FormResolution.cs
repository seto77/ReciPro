using System;
using System.Windows.Forms;

namespace ReciPro;

public partial class FormResolution : Form
{
    public FormResolution() => InitializeComponent();

    private bool SkipEvent = false;

    private void numericUpDownWidth_ValueChanged(object sender, EventArgs e)
    {
        if (SkipEvent) return;
        SkipEvent = true;
        if (checkBoxKeepAspect.Checked)
            numericUpDownHeight.Value = numericUpDownWidth.Value;
        SkipEvent = false;
    }

    private void numericUpDownHeight_ValueChanged(object sender, EventArgs e)
    {
        if (SkipEvent) return;
        SkipEvent = true;
        if (checkBoxKeepAspect.Checked)
            numericUpDownWidth.Value = numericUpDownHeight.Value;
        SkipEvent = false;
    }
}
