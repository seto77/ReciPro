using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class FormScatteringFactor : Form
    {
        public Crystal Crystal { get => CrystalControl.Crystal; }
        public CrystalControl CrystalControl;

        public FormScatteringFactor()
        {
            InitializeComponent();
        }

        //CrystalContorolでCystalが変更されたとき
        private void crystalControl_CrystalChanged(object sender, EventArgs e)
        {
            numericUpDownThresholdD.Minimum = (decimal)((Crystal.A + Crystal.B + Crystal.C) / 20);
            SetSortedPlanes();
        }

        private void FormCrystallographicInformation_Load(object sender, EventArgs e)
        {
            //CrystalContorolでCystalが変更されたときのイベントを登録
            CrystalControl.CrystalChanged += new EventHandler(crystalControl_CrystalChanged);
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
                this.dataGridView2.Columns[3].Visible = false; //Multiを消す
            if (checkBoxHideProhibitedPlanes.Checked)
                this.dataGridView2.Columns[12].Visible = false;

            var c = (Crystal)Crystal.Clone();

            

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
                    bool irreducible = SymmetryStatic.IsRootIndex(g.Index, c.Symmetry, ref multi);
                    if (irreducible && !double.IsNaN(twoTheta))
                    {
                        var magnitude2 = g.F.Real * g.F.Real + g.F.Imaginary * g.F.Imaginary;
                        if (waveLengthControl1.WaveSource == WaveSource.Xray)
                            c.VectorOfG[i].RawIntensity = magnitude2 * multi / c.CellVolumeSqure * (1 + Math.Cos(twoTheta) * Math.Cos(twoTheta)) / Math.Sin(twoTheta) / Math.Sin(twoTheta / 2);
                        else
                            c.VectorOfG[i].RawIntensity = magnitude2 * multi / c.CellVolumeSqure / Math.Sin(twoTheta) / Math.Sin(twoTheta / 2);

                        max = Math.Max(max, c.VectorOfG[i].RawIntensity);
                    }
                }

                for (int i = 0; i < c.VectorOfG.Count; i++)
                    c.VectorOfG[i].RelativeIntensity = c.VectorOfG[i].RawIntensity / max;
            }

            var dataMember = bindingSourceScatteringFactor.DataMember;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            bindingSourceScatteringFactor.DataMember = "";
            foreach (Vector3D g in c.VectorOfG)
            {
                int multi = 0;
                bool irreducible = SymmetryStatic.IsRootIndex(g.Index, c.Symmetry, ref multi);
                if (!checkBoxHideEquivalentPlane.Checked || irreducible)
                {
                    var condition = c.Symmetry.CheckExtinctionRule(g.Index);//SymmetryStatic.CheckExtinctionRule(g.h, g.k, g.l, c.Symmetry);
                    if (!checkBoxHideProhibitedPlanes.Checked || condition.Length == 0)
                    {
                        var d = 1 / g.Length* 10;
                        var twoTheta = 2 * Math.Asin(g.Length* waveLengthControl1.WaveLength / 2) / Math.PI * 180;
                        if (double.IsNaN(twoTheta))
                            twoTheta = double.PositiveInfinity;
                       dataSet.DataTableScatteringFactor.Add(g.Index.h, g.Index.k, g.Index.l, multi, d, twoTheta, g.F, g.RelativeIntensity, g.Extinction);
                    }
                }
            }
            bindingSourceScatteringFactor.DataMember = dataMember;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
        }

        private void FormCrystallographicInformation_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var str = new StringBuilder();
           
            for (int i = 0; i < dataGridView2.Columns.Count; i++)
                if (dataGridView2.Columns[i].Visible)
                    str.Append(dataGridView2.Columns[i].HeaderText + "\t");
            str.Append("\r\n");

            for (int j = 0; j < dataGridView2.Rows.Count; j++)
            {
                for (int i = 0; i < dataGridView2.ColumnCount; i++)
                    if (dataGridView2.Columns[i].Visible)
                        str.Append($"{dataGridView2[i, j].Value}\t");
                str.Append("\r\n");
            }
            Clipboard.SetDataObject(str.ToString());
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

        private void FormScatteringFactor_VisibleChanged(object sender, EventArgs e)
        {
            SetSortedPlanes();
        }
    }
}