using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Crystallography.Controls;

public partial class FormIsotopeComposition : CaptureFormBase
{
    public FormIsotopeComposition() => InitializeComponent();

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int AtomNumber { get; set; } = 0;

    private double[] isotopicComposition = null;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public double[] IsotopicComposition
    {
        set
        {
            isotopicComposition = value;
            var abundance = AtomStatic.IsotopeAbundance[AtomNumber];
            dataGridView.Rows.Clear();
            int n = 0;
            bool keep = isotopicComposition != null && isotopicComposition.Length == abundance.Count;
            foreach (int z in abundance.Keys)
                dataGridView.Rows.Add([z.ToString(), abundance[z], keep ? isotopicComposition[n++] : abundance[z]]);
        }
        get
        {
            isotopicComposition = new double[AtomStatic.IsotopeAbundance[AtomNumber].Count];
            for (int i = 0; i < dataGridView.Rows.Count; i++)
                isotopicComposition[i] = double.TryParse(dataGridView[2, i].Value?.ToString(), out var d) ? d : 0;
            return isotopicComposition;
        }
    }

    private void button1_Click(object sender, EventArgs e) { }
    private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e) { }

    private void dataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex < 1) return;
        if (!double.TryParse(e.FormattedValue?.ToString(), out var d) || d > 100)
        {
            MessageBox.Show("Incorrect Value!");
            e.Cancel = true;
        }
    }
}
