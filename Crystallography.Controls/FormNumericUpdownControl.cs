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
                Control.DecimalPlaces = (int)numericalTextBoxDecimalPlace.Value;
                Control.Increment = (decimal)numericalTextBoxDecimalIncrement.Value;
            }
        }

        private void FormNumericUpdownControl_Load(object sender, EventArgs e)
        {
            numericalTextBoxDecimalPlace.Value = (int)Control.DecimalPlaces;
            numericalTextBoxDecimalIncrement.Value = (double)Control.Increment;
        }
    }
}