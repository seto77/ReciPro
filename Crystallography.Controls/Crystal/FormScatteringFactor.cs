using System;
using System.Linq;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class FormScatteringFactor : Form
    {
        public Crystal crystal;
        public CrystalControl crystalControl;

        public FormScatteringFactor()
        {
            InitializeComponent();
        }

        private void crystalControl_CrystalChanged(Crystal crystal)
        {
            ChangeCrystal(crystal);
        }

        private void FormCrystallographicInformation_Load(object sender, EventArgs e)
        {
            crystalControl.CrystalChanged += new CrystalControl.MyEventHandler(crystalControl_CrystalChanged);
            ChangeCrystal(crystal);
        }

        //åãèªÇïœçXÇ∑ÇÈ
        public void ChangeCrystal(Crystal crystal)
        {
            this.crystal = crystal;
            numericUpDownThresholdD.Minimum = (decimal)((crystal.A + crystal.B + crystal.C) / 6);
            SetSortedPlanes();
        }

        private void numericUpDownThresholdD_ValueChanged(object sender, EventArgs e)
        {
            SetSortedPlanes();
        }

        private void SetSortedPlanes()
        {
            dataSet.DataTableScatteringFactor.Clear();
            for (int i = 0; i < this.dataGridView2.Columns.Count; i++)
                this.dataGridView2.Columns[i].Visible = true;

            if (!checkBoxHideEquivalentPlane.Checked)
                this.dataGridView2.Columns[3].Visible = false; //MultiÇè¡Ç∑
            if (checkBoxHideProhibitedPlanes.Checked)
                this.dataGridView2.Columns[12].Visible = false;

            Crystal c = (Crystal)crystal.Clone();

            c.SetVectorOfG((double)numericUpDownThresholdD.Value / 10, waveLengthControl1.WaveSource, false);

            if (c.VectorOfG.Count == 0) return;

            double max = c.VectorOfG.Max(g => g.RawIntensity);
            for (int i = 0; i < c.VectorOfG.Count; i++)
                c.VectorOfG[i].RelativeIntensity = c.VectorOfG[i].RawIntensity / max;

            if (checkBoxBragBrentano.Checked)
            {
                max = double.NegativeInfinity;
                for (int i = 0; i < c.VectorOfG.Count; i++)
                {
                    Vector3D g = c.VectorOfG[i];
                    int multi = 0;
                    double twoTheta = 2 * Math.Asin(g.Length* waveLengthControl1.WaveLength / 2);
                    bool irreducible = SymmetryStatic.IsRootIndex(g.h, g.k, g.l, c.Symmetry, ref multi);
                    if (irreducible && !double.IsNaN(twoTheta))
                    {
                        if (waveLengthControl1.WaveSource == WaveSource.Xray)
                            c.VectorOfG[i].RawIntensity = g.F.Magnitude2() * multi / c.CellVolumeSqure * (1 + Math.Cos(twoTheta) * Math.Cos(twoTheta)) / Math.Sin(twoTheta) / Math.Sin(twoTheta / 2);
                        else
                            c.VectorOfG[i].RawIntensity = g.F.Magnitude2() * multi / c.CellVolumeSqure / Math.Sin(twoTheta) / Math.Sin(twoTheta / 2);

                        max = Math.Max(max, c.VectorOfG[i].RawIntensity);
                    }
                }

                for (int i = 0; i < c.VectorOfG.Count; i++)
                    c.VectorOfG[i].RelativeIntensity = c.VectorOfG[i].RawIntensity / max;
            }

            foreach (Vector3D g in c.VectorOfG)
            {
                int multi = 0;
                bool irreducible = SymmetryStatic.IsRootIndex(g.h, g.k, g.l, c.Symmetry, ref multi);
                if (!checkBoxHideEquivalentPlane.Checked || irreducible)
                {
                    var condition = c.Symmetry.CheckExtinctionRule(g.h, g.k, g.l);//SymmetryStatic.CheckExtinctionRule(g.h, g.k, g.l, c.Symmetry);
                    if (!checkBoxHideProhibitedPlanes.Checked || condition.Length == 0)
                    {
                        var d = 1 / g.Length* 10;
                        var twoTheta = 2 * Math.Asin(g.Length* waveLengthControl1.WaveLength / 2) / Math.PI * 180;
                        if (double.IsNaN(twoTheta))
                            twoTheta = double.PositiveInfinity;
                        dataSet.DataTableScatteringFactor.Add(g.h, g.k, g.l, multi, d, twoTheta, g.F, g.RelativeIntensity, g.Extinction);
                    }
                }
            }
        }

        private void FormCrystallographicInformation_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = "";
            for (int i = 0; i < dataGridView2.Columns.Count; i++)
                if (dataGridView2.Columns[i].Visible)
                    str += dataGridView2.Columns[i].HeaderText + "\t";
            str += "\r\n";

            for (int j = 0; j < dataGridView2.Rows.Count; j++)
            {
                for (int i = 0; i < dataGridView2.ColumnCount; i++)
                    if (dataGridView2.Columns[i].Visible)
                        str += (string)dataGridView2[i, j].Value.ToString() + "\t";
                str += "\r\n";
            }
            Clipboard.SetDataObject(str);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void checkBoxBragBrentano_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxBragBrentano.Checked)
            {
                checkBoxHideEquivalentPlane.Checked = true;
                checkBoxHideProhibitedPlanes.Checked = true;
                checkBoxHideEquivalentPlane.Enabled = false;
                checkBoxHideProhibitedPlanes.Enabled = false;
            }
            else
            {
                checkBoxHideEquivalentPlane.Enabled = true;
                checkBoxHideProhibitedPlanes.Enabled = true;
            }

            SetSortedPlanes();
        }

        private void waveLengthControl1_WavelengthChanged(object sender, EventArgs e)
        {
            SetSortedPlanes();
        }
    }
}