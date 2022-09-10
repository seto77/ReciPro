using System;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class FormAtomDetailedInfo : Form
    {
        public FormAtomDetailedInfo()
        {
            InitializeComponent();
            listBox1.Items.Add("No.\tx\t y\t  z\r\n");
        }

        private Atoms atoms = new Atoms();

        public Atoms Atoms
        {
            set { atoms = value; setAtomDetailedInfo(); }
            get { return atoms; }
        }

        private void setAtomDetailedInfo()
        {
            if (atoms == null)
                return;

            //string str = "No.\tx\t y\t  z\r\n";

            for (int i = 0; i < atoms.Atom.Length; i++)
                listBox.Items.Add($"{i + 1}\t{Atoms.GetStringFromDouble(atoms.Atom[i].X)}\t {Atoms.GetStringFromDouble(atoms.Atom[i].Y)}\t  {Atoms.GetStringFromDouble(atoms.Atom[i].Z)}");

            //this.toolTip.SetToolTip(this.listBoxAtoms, str); ;
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void FormAtomDetailedInfo_Load(object sender, EventArgs e)
        {
        }
    }
}