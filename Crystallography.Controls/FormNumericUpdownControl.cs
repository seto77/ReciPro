using System;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class FormNumericUpdownControl : Form
    {
        private NumericUpDown Control = null;

        public FormNumericUpdownControl(NumericUpDown control)
        {
            InitializeComponent();
            Control = control;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (Control != null)
            {
                Control.DecimalPlaces = (int)numericBoxDecimalPlace.Value;
                Control.Increment = (decimal)numericBoxDecimalIncrement.Value;
            }
        }

        private void FormNumericUpdownControl_Load(object sender, EventArgs e)
        {
            numericBoxDecimalPlace.Value = (int)Control.DecimalPlaces;
            numericBoxDecimalIncrement.Value = (double)Control.Increment;
        }
    }
}