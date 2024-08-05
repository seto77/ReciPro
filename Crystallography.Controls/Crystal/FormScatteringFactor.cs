using MathNet.Numerics;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using static IronPython.Modules._ast;

namespace Crystallography.Controls
{
    public partial class FormScatteringFactor : Form
    {
        public Crystal Crystal { get => CrystalControl.Crystal; }
        public CrystalControl CrystalControl;

        #region �N��, �I��
        public FormScatteringFactor()
        {
            InitializeComponent();
        }
        private void FormCrystallographicInformation_Load(object sender, EventArgs e)
        {
            //CrystalContorol��Cystal���ύX���ꂽ�Ƃ��̃C�x���g��o�^
            CrystalControl.CrystalChanged += new EventHandler(crystalControl_CrystalChanged);
            typeof(DataGridView).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dataGridView2, true, null);

        }

        private void FormCrystallographicInformation_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        #endregion 

        //CrystalContorol��Cystal���ύX���ꂽ�Ƃ�
        private void crystalControl_CrystalChanged(object sender, EventArgs e)
        {

            numericUpDownThresholdD.Minimum = (decimal)((Crystal.A + Crystal.B + Crystal.C) / 20);
            if (this.Visible)
                SetSortedPlanes();
        }
        //VisibleChange
        private void FormScatteringFactor_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
                SetSortedPlanes();
        }

        private void numericUpDownThresholdD_ValueChanged(object sender, EventArgs e)
        {
            SetSortedPlanes();
        }

        private void buttonCopyClipBoard_Click(object sender, EventArgs e)
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


        private void SetSortedPlanes()
        {
            if (checkBoxTest.Checked)
            {
                numericBoxH_min_ValueChanged(new object(), new EventArgs());
                return;
            }

            var c = (Crystal)Crystal.Clone();

            c.SetVectorOfG((double)numericUpDownThresholdD.Value / 10, waveLengthControl1.WaveSource);

            Array.Sort(c.VectorOfG, (g1, g2) => g2.d.CompareTo(g1.d));

            if (c.VectorOfG.Length == 0) return;

            var max = c.VectorOfG.Max(g => g.RawIntensity);
            for (int i = 0; i < c.VectorOfG.Length; i++)
                c.VectorOfG[i].RelativeIntensity = c.VectorOfG[i].RawIntensity / max;

            if (checkBoxBragBrentano.Checked)
            {
                max = double.NegativeInfinity;
                for (int i = 0; i < c.VectorOfG.Length; i++)
                {
                    Vector3D g = c.VectorOfG[i];
                    double twoTheta = 2 * Math.Asin(g.Length * waveLengthControl1.WaveLength / 2);
                    bool irreducible = SymmetryStatic.IsRootIndex(g.Index, c.Symmetry, out int multi);
                    if (irreducible && !double.IsNaN(twoTheta))
                    {
                        var magnitude2 = g.F.Real * g.F.Real + g.F.Imaginary * g.F.Imaginary;
                        if (waveLengthControl1.WaveSource == WaveSource.Xray)
                            c.VectorOfG[i].RawIntensity = magnitude2 * multi / c.CellVolumeSquare * (1 + Math.Cos(twoTheta) * Math.Cos(twoTheta)) / Math.Sin(twoTheta) / Math.Sin(twoTheta / 2);
                        else
                            c.VectorOfG[i].RawIntensity = magnitude2 * multi / c.CellVolumeSquare / Math.Sin(twoTheta) / Math.Sin(twoTheta / 2);

                        max = Math.Max(max, c.VectorOfG[i].RawIntensity);
                    }
                }

                for (int i = 0; i < c.VectorOfG.Length; i++)
                    c.VectorOfG[i].RelativeIntensity = c.VectorOfG[i].RawIntensity / max;
            }

            //��UbindingSource������
            var dataMember = bindingSourceScatteringFactor.DataMember;
            //dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            bindingSourceScatteringFactor.DataMember = "";


            dataSet.DataTableScatteringFactor.Clear();
            for (int i = 0; i < this.dataGridView2.Columns.Count; i++)
                this.dataGridView2.Columns[i].Visible = true;

            if (!checkBoxHideEquivalentPlane.Checked)
                this.dataGridView2.Columns[3].Visible = false; //Multi������
            if (checkBoxHideProhibitedPlanes.Checked)
                this.dataGridView2.Columns[12].Visible = false;

            foreach (Vector3D g in c.VectorOfG)
            {
                bool irreducible = SymmetryStatic.IsRootIndex(g.Index, c.Symmetry, out int multi);
                if (!checkBoxHideEquivalentPlane.Checked || irreducible)
                {
                    var condition = c.Symmetry.CheckExtinctionRule(g.Index);//SymmetryStatic.CheckExtinctionRule(g.h, g.k, g.l, c.Symmetry);
                    if (!checkBoxHideProhibitedPlanes.Checked || condition.Length == 0)
                    {
                        var d = 1 / g.Length * 10;
                        var twoTheta = 2 * Math.Asin(g.Length * waveLengthControl1.WaveLength / 2) / Math.PI * 180;
                        if (double.IsNaN(twoTheta))
                            twoTheta = double.PositiveInfinity;
                        dataSet.DataTableScatteringFactor.Add(g.Index.h, g.Index.k, g.Index.l, multi, d, twoTheta, g.F, g.RelativeIntensity, g.Extinction);
                    }
                }
            }
            dataGridView2.DefaultCellStyle.Format = "";
            dataGridView2.VirtualMode = true;

            bindingSourceScatteringFactor.DataMember = dataMember;
            //dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            //dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            //dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
        }

        /// <summary>
        /// �e�X�g�R�[�h
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericBoxH_min_ValueChanged(object sender, EventArgs e)
        {
            var c = (Crystal)Crystal.Clone();

            //��UbindingSource������
            var dataMember = bindingSourceScatteringFactor.DataMember;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            bindingSourceScatteringFactor.DataMember = "";

            dataSet.DataTableScatteringFactor.Clear();
            for (int i = 0; i < dataGridView2.Columns.Count; i++)
                this.dataGridView2.Columns[i].Visible = true;

            for (double h = numericBoxH_min.Value; h <= numericBoxH_max.Value + 1e-10; h += numericBoxH_step.Value)
                for (double k = numericBoxK_min.Value; k <= numericBoxK_max.Value + 1e-10; k += numericBoxK_step.Value)
                    for (double l = numericBoxL_min.Value; l <= numericBoxL_max.Value + 1e-10; l += numericBoxL_step.Value)
                    {
                        var gLength = (h * c.A_Star + k * c.B_Star + l * c.C_Star).Length;
                        var d = 1 / gLength;
                        var twoTheta = 2 * Math.Asin(gLength * waveLengthControl1.WaveLength / 2) / Math.PI * 180;
                        var F = Crystal.GetStructureFactor(waveLengthControl1.WaveSource, c.Atoms, (h, k, l), 1 / d / d / 4.0);

                        dataSet.DataTableScatteringFactor.Add(h, k, l, 1, d, twoTheta, F, F.MagnitudeSquared(), []);
                    }
            dataGridView2.DefaultCellStyle.Format = "g8";
            dataGridView2.VirtualMode = true;
            bindingSourceScatteringFactor.DataMember = dataMember;
        }

        private void checkBoxTest_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Visible=checkBoxTest.Checked;
            if (panel1.Visible)
                numericBoxH_min_ValueChanged(sender, e);
            else
                SetSortedPlanes();
        }
    }
}