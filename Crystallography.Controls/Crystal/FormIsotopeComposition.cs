using System;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class FormIsotopeComposition : Form
    {
        public FormIsotopeComposition()
        {
            InitializeComponent();
        }

        private int atomNumber = 0;

        public int AtomNumber
        {
            set { atomNumber = value; }
            get { return atomNumber; }
        }

        private double[] isotopicComposition = null;

        public double[] IsotopicComposition
        {
            set
            {
                isotopicComposition = value;
                if (isotopicComposition == null || isotopicComposition.Length != AtomStatic.IsotopeAbundance[atomNumber].Count)
                {
                    dataGridView.Rows.Clear();
                    foreach (int z in AtomStatic.IsotopeAbundance[atomNumber].Keys)
                        dataGridView.Rows.Add([z.ToString(), AtomStatic.IsotopeAbundance[atomNumber][z], AtomStatic.IsotopeAbundance[atomNumber][z]]);
                }
                else
                {
                    int n = 0;
                    foreach (int z in AtomStatic.IsotopeAbundance[atomNumber].Keys)
                        dataGridView.Rows.Add([z, AtomStatic.IsotopeAbundance[atomNumber][z], isotopicComposition[n++]]);
                }
            }
            get
            {
                isotopicComposition = new double[AtomStatic.IsotopeAbundance[atomNumber].Count];
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    try
                    {
                        isotopicComposition[i] = Convert.ToDouble(dataGridView[2, i].Value);
                    }
                    catch
                    {
                        isotopicComposition[i] = 0;
                    }
                }
                return isotopicComposition;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 1) return;
            try
            {
                double d = Convert.ToDouble(e.FormattedValue);
                if (d > 100) throw new ArgumentException();
            }
            catch
            {
                MessageBox.Show("Incorrect Value!");
                e.Cancel = true;
            }
        }
    }
}