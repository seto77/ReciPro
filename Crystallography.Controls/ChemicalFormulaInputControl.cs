using System;
using System.Windows.Forms;

namespace Crystallography.Controls;

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

        if (z == 1) numericBoxValence.Value = 1;
        else if (z == 5) numericBoxValence.Value = 3;
        else if (z == 6 || z == 14) numericBoxValence.Value = 4;
        else if (z == 7) numericBoxValence.Value = 5;
        else if (z == 15) numericBoxValence.Value = 5;
        else if (z == 16) numericBoxValence.Value = 6;
        else if (z >= 25 && z <= 30) numericBoxValence.Value = 2;
        else if (z == 33) numericBoxValence.Value = 3;
        else if (z == 34) numericBoxValence.Value = 4;
        else if (z >= 43 && z <= 45) numericBoxValence.Value = 4;
        else if (z == 46) numericBoxValence.Value = 2;
        else if (z == 47) numericBoxValence.Value = 4;
        else if (z == 48) numericBoxValence.Value = 2;
        else if (z == 49) numericBoxValence.Value = 3;
        else if (z == 50) numericBoxValence.Value = 4;
        else if (z == 51) numericBoxValence.Value = 3;
        else if (z == 52) numericBoxValence.Value = 4;
        else if (z == 53) numericBoxValence.Value = 5;
        else if (z == 75) numericBoxValence.Value = 7;
        else if (z == 76) numericBoxValence.Value = 8;
        else if (z == 84) numericBoxValence.Value = 4;
        else if (z == 87) numericBoxValence.Value = 1;
        else if (z == 91) numericBoxValence.Value = 5;
        else if (AtomStatic.XrayScatteringWK[z][^1].Valence > 0)
            numericBoxValence.Value = AtomStatic.XrayScatteringWK[z][^1].Valence;
        else
            numericBoxValence.Value = 0;
        numericBoxValence_ValueChanged(sender, e);
    }

    private void numericBoxValence_ValueChanged(object sender, EventArgs e)
    {
        int z = comboBoxElement.SelectedIndex + 1;
        if (comboBoxCompound.SelectedIndex != comboBoxCompound.Items.Count - 1)
        {
            //string[] s = comboBoxCompound.Text.Split(new char[] { ' ' });
            //double accesoryValence = Convert.ToDouble((s[1].Substring(s[1].Length - 1, 1) + s[1].Substring(0, s[1].Length - 1)));
            //ElementProperty ep = new ElementProperty(z, numericBoxValence.Value, checkBoxCompound.Checked,"", s[0], accesoryValence, 0, 0, XrayLine.Ka, 0, 0);
            var m = new Molecule(z, numericBoxValence.Value);

            textBoxCompoundForm.Text = Molecule.CombineCationAndAnion(m, Molecule.DefinedIon[comboBoxCompound.SelectedIndex]);
        }
    }

    private void comboBoxCompound_SelectedIndexChanged(object sender, EventArgs e)
    {
        textBoxCompoundForm.ReadOnly = comboBoxCompound.SelectedIndex != comboBoxCompound.Items.Count - 1;
        numericBoxValence_ValueChanged(sender, e);
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
            string[] s = comboBoxCompound.Text.Split([' ']);
            accesoryFormula = s[0];
            accesoryValence = Convert.ToDouble(string.Concat(s[1].AsSpan(s[1].Length - 1, 1), s[1].AsSpan()[0..^1]));
        }
        //ElementProperty ep = new ElementProperty(
        //    z, numericBoxValence.Value, checkBoxCompound.Checked, textBoxCompoundForm.Text,
        //    accesoryFormula, accesoryValence, numericBoxWeight.Value / 100.0, numericBoxMolarRatio.Value,
        //    XrayLine.Ka, numericBoxTotalCount.Value, numericBoxCountTime.Value);
        //return ep;
        return null;
    }

    //public void SetMolecule(Molecule ep)
    //{
    //    //comboBoxElement.SelectedIndex = ep.AtomicNumber - 1;
    //    //numericBoxValence.Value = ep.Valence;

    //    //numericBoxCountTime.Value = ep.CountTime;
    //}
}