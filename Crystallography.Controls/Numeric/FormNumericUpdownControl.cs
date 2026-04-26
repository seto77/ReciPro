using System;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class FormNumericUpdownControl : FormBase
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
            // 260426Cl 修正: buttonOK_Click と整合させ、Control が null でも NullReferenceException にならないようガード
            if (Control == null) return;
            numericBoxDecimalPlace.Value = Control.DecimalPlaces;
            numericBoxDecimalIncrement.Value = (double)Control.Increment;
        }
    }
}
