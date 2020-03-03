using System;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class ChemicalFormulaInputControl : UserControl
    {
        private bool standardMode = true;

        public bool StandardMode
        {
            set
            {
                standardMode = value;
                flowLayoutPanelComposition.Visible = value;
            }
            get { return standardMode; }
        }

        private bool weightMode = true;

        public bool WeightMode
        {
            set
            {
                weightMode = value;
                flowLayoutPanelMolarRatio.Visible = !value;
                flowLayoutPanelWeight.Visible = value;
            }
            get { return weightMode; }
        }

        public ChemicalFormulaInputControl()
        {
            InitializeComponent();
            comboBoxCompound.SelectedIndex = 0;
            comboBoxElement.SelectedIndex = 0;
            //comboBoxLine.SelectedIndex = 0;
        }

        private void comboBoxElement_SelectedIndexChanged(object sender, EventArgs e)
        {
            int z = comboBoxElement.SelectedIndex + 1;

            if (z == 1) numericalTextBoxValence.Value = 1;
            else if (z == 5) numericalTextBoxValence.Value = 3;
            else if (z == 6 || z == 14) numericalTextBoxValence.Value = 4;
            else if (z == 7) numericalTextBoxValence.Value = 5;
            else if (z == 15) numericalTextBoxValence.Value = 5;
            else if (z == 16) numericalTextBoxValence.Value = 6;
            else if (z >= 25 && z <= 30) numericalTextBoxValence.Value = 2;
            else if (z == 33) numericalTextBoxValence.Value = 3;
            else if (z == 34) numericalTextBoxValence.Value = 4;
            else if (z >= 43 && z <= 45) numericalTextBoxValence.Value = 4;
            else if (z == 46) numericalTextBoxValence.Value = 2;
            else if (z == 47) numericalTextBoxValence.Value = 4;
            else if (z == 48) numericalTextBoxValence.Value = 2;
            else if (z == 49) numericalTextBoxValence.Value = 3;
            else if (z == 50) numericalTextBoxValence.Value = 4;
            else if (z == 51) numericalTextBoxValence.Value = 3;
            else if (z == 52) numericalTextBoxValence.Value = 4;
            else if (z == 53) numericalTextBoxValence.Value = 5;
            else if (z == 75) numericalTextBoxValence.Value = 7;
            else if (z == 76) numericalTextBoxValence.Value = 8;
            else if (z == 84) numericalTextBoxValence.Value = 4;
            else if (z == 87) numericalTextBoxValence.Value = 1;
            else if (z == 91) numericalTextBoxValence.Value = 5;
            else if (AtomConstants.XrayScattering[z][AtomConstants.XrayScattering[z].Length - 1].Valence > 0)
                numericalTextBoxValence.Value = AtomConstants.XrayScattering[z][AtomConstants.XrayScattering[z].Length - 1].Valence;
            else
                numericalTextBoxValence.Value = 0;
            numericalTextBoxValence_ValueChanged(sender, e);
        }

        private void numericalTextBoxValence_ValueChanged(object sender, EventArgs e)
        {
            int z = comboBoxElement.SelectedIndex + 1;
            if (comboBoxCompound.SelectedIndex != comboBoxCompound.Items.Count - 1)
            {
                //string[] s = comboBoxCompound.Text.Split(new char[] { ' ' });
                //double accesoryValence = Convert.ToDouble((s[1].Substring(s[1].Length - 1, 1) + s[1].Substring(0, s[1].Length - 1)));
                //ElementProperty ep = new ElementProperty(z, numericalTextBoxValence.Value, checkBoxCompound.Checked,"", s[0], accesoryValence, 0, 0, XrayLine.Ka, 0, 0);
                Molecule m = new Molecule(z, numericalTextBoxValence.Value);

                textBoxCompoundForm.Text = Molecule.CombineCationAndAnion(m, Molecule.DefinedIon[comboBoxCompound.SelectedIndex]);
            }
        }

        private void comboBoxCompound_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxCompoundForm.ReadOnly = comboBoxCompound.SelectedIndex != comboBoxCompound.Items.Count - 1;
            numericalTextBoxValence_ValueChanged(sender, e);
        }

        private void checkBoxIsCompound_CheckedChanged(object sender, EventArgs e)
        {
            flowLayoutPanelOxide.Visible = checkBoxCompound.Checked;
        }

        public Molecule GetMolecule()
        {
            int z = comboBoxElement.SelectedIndex + 1;

            string accesoryFormula = "";
            double accesoryValence = 0;

            if (comboBoxCompound.SelectedIndex != comboBoxCompound.Items.Count - 1)
            {
                string[] s = comboBoxCompound.Text.Split(new char[] { ' ' });
                accesoryFormula = s[0];
                accesoryValence = Convert.ToDouble((s[1].Substring(s[1].Length - 1, 1) + s[1].Substring(0, s[1].Length - 1)));
            }
            //ElementProperty ep = new ElementProperty(
            //    z, numericalTextBoxValence.Value, checkBoxCompound.Checked, textBoxCompoundForm.Text,
            //    accesoryFormula, accesoryValence, numericalTextBoxWeight.Value / 100.0, numericalTextBoxMolarRatio.Value,
            //    XrayLine.Ka, numericalTextBoxTotalCount.Value, numericalTextBoxCountTime.Value);
            //return ep;
            return null;
        }

        public void SetMolecule(Molecule ep)
        {
            //comboBoxElement.SelectedIndex = ep.AtomicNumber - 1;
            //numericalTextBoxValence.Value = ep.Valence;

            //numericalTextBoxCountTime.Value = ep.CountTime;
        }
    }
}