using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Crystallography.Controls;

public partial class FormAtomDetailedInfo : FormBase
{
    private Atoms atoms = new();

    public FormAtomDetailedInfo()
    {
        InitializeComponent();
        listBox1.Items.Add("No.\tx\t y\t  z\r\n");
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Atoms Atoms
    {
        get => atoms;
        set { atoms = value; SetAtomDetailedInfo(); }
    }

    private void SetAtomDetailedInfo()
    {
        if (atoms == null) return;
        for (int i = 0; i < atoms.Atom.Length; i++)
        {
            var a = atoms.Atom[i];
            listBox.Items.Add($"{i + 1}\t{Atoms.GetStringFromDouble(a.X)}\t {Atoms.GetStringFromDouble(a.Y)}\t  {Atoms.GetStringFromDouble(a.Z)}");
        }
    }

    private void listBox_SelectedIndexChanged(object sender, EventArgs e) { }
    private void FormAtomDetailedInfo_Load(object sender, EventArgs e) { }
}
