using System;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class FormResolution : Form
    {
        public FormResolution()
        {
            InitializeComponent();
        }

        public int ResolutionWidth
        {
            set { numericUpDownWidth.Value = (decimal)value; }
            get { return (int)numericUpDownWidth.Value; }
        }

        public int ResolutionHeight
        {
            set { numericUpDownHeight.Value = (decimal)value; }
            get { return (int)numericUpDownHeight.Value; }
        }

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

        private void buttonOK_Click(object sender, EventArgs e)
        {
        }
    }
}