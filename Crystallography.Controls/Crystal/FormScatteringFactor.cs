using MathNet.Numerics;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Crystallography.Controls;

public partial class FormScatteringFactor : CaptureFormBase
{
    public Crystal Crystal { get => CrystalControl.Crystal; }
    public CrystalControl CrystalControl;

    /// <summary>長さの単位の get/set</summary>
    [System.ComponentModel.Browsable(false)]
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
    public LengthUnitEnum LengthUnit
    {
        get => radioButtonAngstrom.Checked ? LengthUnitEnum.Angstrom : LengthUnitEnum.NanoMeter;
        set
        {
            radioButtonAngstrom.Checked = value == LengthUnitEnum.Angstrom;
            radioButtonNanoMeter.Checked = value == LengthUnitEnum.NanoMeter;
        }
    }

    /// <summary>
    /// d値のカットオフ (常にnm単位)
    /// </summary>
    public double CutoffD => LengthUnit== LengthUnitEnum.NanoMeter ? numericBoxCutoffD.Value : numericBoxCutoffD.Value/10;


    public bool MillerBravais { get => dataGridViewTextBoxColumnI.Visible; set => dataGridViewTextBoxColumnI.Visible = value; }


    #region 起動, 終了
    public FormScatteringFactor()
    {
        InitializeComponent();
    }
    private void FormCrystallographicInformation_Load(object sender, EventArgs e)
    {
        //CrystalContorolでCystalが変更されたときのイベントを登録
        CrystalControl.CrystalChanged += new EventHandler(crystalControl_CrystalChanged);
        typeof(DataGridView).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dataGridView, true, null);

    }

    private void FormCrystallographicInformation_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        this.Visible = false;
    }

    #endregion 

    //CrystalContorolでCystalが変更されたとき
    private void crystalControl_CrystalChanged(object sender, EventArgs e)
    {
        numericBoxCutoffD.Minimum = (Crystal.A + Crystal.B + Crystal.C) / 20;
        if (this.Visible)
            SetSortedPlanes();
    }
    //VisibleChange
    private void FormScatteringFactor_VisibleChanged(object sender, EventArgs e)
    {
        if (this.Visible)
            SetSortedPlanes();
    }

    private void numericBoxCutoffD_ValueChanged(object sender, EventArgs e)
    {
        SetSortedPlanes();
    }

    private void buttonCopyClipBoard_Click(object sender, EventArgs e)
    {
        var str = new StringBuilder();

        for (int i = 0; i < dataGridView.Columns.Count; i++)
            if (dataGridView.Columns[i].Visible)
                str.Append(dataGridView.Columns[i].HeaderText + "\t");
        str.Append("\r\n");

        for (int j = 0; j < dataGridView.Rows.Count; j++)
        {
            for (int i = 0; i < dataGridView.ColumnCount; i++)
                if (dataGridView.Columns[i].Visible)
                    str.Append($"{dataGridView[i, j].Value}\t");
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

        c.SetVectorOfG(CutoffD, waveLengthControl1.WaveSource);

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
                if (SymmetryStatic.IsRootPlane(g.Index, c.Symmetry, out var indices) && !double.IsNaN(twoTheta))
                {
                    var magnitude2 = g.F.Real * g.F.Real + g.F.Imaginary * g.F.Imaginary;
                    if (waveLengthControl1.WaveSource == WaveSource.Xray)
                        c.VectorOfG[i].RawIntensity = magnitude2 * indices.Length / c.CellVolumeSquare * (1 + Math.Cos(twoTheta) * Math.Cos(twoTheta)) / Math.Sin(twoTheta) / Math.Sin(twoTheta / 2);
                    else
                        c.VectorOfG[i].RawIntensity = magnitude2 * indices.Length / c.CellVolumeSquare / Math.Sin(twoTheta) / Math.Sin(twoTheta / 2);

                    max = Math.Max(max, c.VectorOfG[i].RawIntensity);
                }
            }

            for (int i = 0; i < c.VectorOfG.Length; i++)
                c.VectorOfG[i].RelativeIntensity = c.VectorOfG[i].RawIntensity / max;
        }

        //一旦bindingSourceを解除
        var dataMember = bindingSourceScatteringFactor.DataMember;
        dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        bindingSourceScatteringFactor.DataMember = "";


        dataSet.DataTableScatteringFactor.Clear();

        this.dataGridView.Columns[nameof(dataGridViewTextBoxColumnMulti)].Visible = checkBoxHideEquivalentPlane.Checked; 
        this.dataGridView.Columns[nameof(dataGridViewTextBoxColumnIntCondition)].Visible = !checkBoxHideProhibitedPlanes.Checked; 

        foreach (Vector3D g in c.VectorOfG)
        {
            var root = SymmetryStatic.IsRootPlane(g.Index, c.Symmetry, out var indices);
            if (!checkBoxHideEquivalentPlane.Checked || root)
            {
                int i = indices.Length;
                var condition = c.Symmetry.CheckExtinctionRule(g.Index);
                if (!checkBoxHideProhibitedPlanes.Checked || condition.Length == 0)
                {
                    var d = LengthUnit == LengthUnitEnum.NanoMeter ? 1 / g.Length : 1 / g.Length * 10;
                    var twoTheta = 2 * Math.Asin(g.Length * waveLengthControl1.WaveLength / 2) / Math.PI * 180;
                    if (double.IsNaN(twoTheta))
                        twoTheta = double.PositiveInfinity;
                    dataSet.DataTableScatteringFactor.Add(g.Index.h, g.Index.k, g.Index.l, indices.Length, d, twoTheta, g.F, g.RelativeIntensity, g.Extinction);
                }
            }
        }
        dataGridView.DefaultCellStyle.Format = "";
        dataGridView.VirtualMode = true;

        bindingSourceScatteringFactor.DataMember = dataMember;
    }

    /// <summary>テストコード</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void numericBoxH_min_ValueChanged(object sender, EventArgs e)
    {
        var c = (Crystal)Crystal.Clone();

        //一旦bindingSourceを解除
        var dataMember = bindingSourceScatteringFactor.DataMember;
        dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        bindingSourceScatteringFactor.DataMember = "";

        dataSet.DataTableScatteringFactor.Clear();

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
        dataGridView.DefaultCellStyle.Format = "g8";
        dataGridView.VirtualMode = true;
        bindingSourceScatteringFactor.DataMember = dataMember;
    }

    private void checkBoxTest_CheckedChanged(object sender, EventArgs e)
    {
        panel1.Visible = checkBoxTest.Checked;
        if (panel1.Visible)
            numericBoxH_min_ValueChanged(sender, e);
        else
            SetSortedPlanes();
    }

    private void radioButtonNanoMeter_CheckedChanged(object sender, EventArgs e)
    {
        dataGridViewTextBoxColumnD.HeaderText = LengthUnit == LengthUnitEnum.Angstrom ? "d (Å)" : "d (nm)";
        numericBoxCutoffD.FooterText = LengthUnit == LengthUnitEnum.Angstrom ? "Å" : "nm";
        numericBoxCutoffD.Value = LengthUnit == LengthUnitEnum.Angstrom ? numericBoxCutoffD.Value * 10 : numericBoxCutoffD.Value / 10;

        //SetSortedPlanes();
    }

    private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || dataGridView.Columns[e.ColumnIndex] != dataGridViewTextBoxColumnI) return;
        var row = dataGridView.Rows[e.RowIndex];
        var h = Convert.ToInt32(row.Cells[dataGridViewTextBoxColumnH.Index].Value);
        var k = Convert.ToInt32(row.Cells[dataGridViewTextBoxColumnK.Index].Value);
        e.Value = (-h - k).ToString(); // (260424Ch) TextBoxCell の表示値は string にして DataGridView の型不一致を避ける
        e.FormattingApplied = true;
    }
}
