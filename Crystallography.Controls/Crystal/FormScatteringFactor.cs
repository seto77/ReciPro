using MathNet.Numerics;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Crystallography.Controls;

public partial class FormScatteringFactor : CaptureFormBase
{
    public Crystal Crystal => CrystalControl.Crystal;
    public CrystalControl CrystalControl;

    /// <summary>長さの単位の get/set</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public LengthUnitEnum LengthUnit
    {
        get => radioButtonAngstrom.Checked ? LengthUnitEnum.Angstrom : LengthUnitEnum.NanoMeter;
        set
        {
            radioButtonAngstrom.Checked = value == LengthUnitEnum.Angstrom;
            radioButtonNanoMeter.Checked = value == LengthUnitEnum.NanoMeter;
        }
    }

    // 260425Cl WFO1000 対策: デザイナのシリアライゼーション対象から除外
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool MillerBravais { get => dataGridViewTextBoxColumnI.Visible; set => dataGridViewTextBoxColumnI.Visible = value; }


    #region 起動, 終了
    public FormScatteringFactor()
    {
        InitializeComponent();
    }
    private void FormCrystallographicInformation_Load(object sender, EventArgs e)
    {
        // (260426Ch) 古い EventHandler 明示生成とコメント typo を整理
        CrystalControl.CrystalChanged += crystalControl_CrystalChanged;
        typeof(DataGridView).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dataGridView, true, null);
    }

    private void FormCrystallographicInformation_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        Visible = false;
    }

    #endregion 

    // CrystalControl で Crystal が変更されたとき
    private void crystalControl_CrystalChanged(object sender, EventArgs e)
    {
        numericBoxCutoffD.Minimum = (Crystal.A + Crystal.B + Crystal.C) / 20;
        if (Visible)
            SetSortedPlanes();
    }
    // 表示時に再計算
    private void FormScatteringFactor_VisibleChanged(object sender, EventArgs e)
    {
        if (Visible)
            SetSortedPlanes();
    }

    private void numericBoxCutoffD_ValueChanged(object sender, EventArgs e) => SetSortedPlanes();

    private void buttonCopyClipboard_Click(object sender, EventArgs e)
    {
        var sb = new StringBuilder();
        var columns = dataGridView.Columns.Cast<DataGridViewColumn>().Where(static c => c.Visible).ToArray(); // (260426Ch)

        foreach (var column in columns)
            sb.Append(column.HeaderText).Append('\t');
        sb.AppendLine();

        foreach (DataGridViewRow row in dataGridView.Rows)
        {
            foreach (var column in columns)
                sb.Append(row.Cells[column.Index].Value).Append('\t');
            sb.AppendLine();
        }
        Clipboard.SetText(sb.ToString());
    }

    private void checkBoxBragBrentano_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBoxBragBrentano.Checked) // (260426Ch) 分岐の重複を削減
        {
            checkBoxHideEquivalentPlane.Checked = true;
            checkBoxHideProhibitedPlanes.Checked = true;
        }
        checkBoxHideEquivalentPlane.Enabled = !checkBoxBragBrentano.Checked;
        checkBoxHideProhibitedPlanes.Enabled = !checkBoxBragBrentano.Checked;

        SetSortedPlanes();
    }

    private void waveLengthControl1_WavelengthChanged(object sender, EventArgs e) => SetSortedPlanes();


    private void SetSortedPlanes()
    {
        if (checkBoxTest.Checked)
        {
            numericBoxH_min_ValueChanged(this, EventArgs.Empty); // (260426Ch) 不要な object/EventArgs 生成を避ける
            return;
        }

        var c = (Crystal)Crystal.Clone();
        var waveLength = waveLengthControl1.WaveLength;
        var waveSource = waveLengthControl1.WaveSource;
        var cutoffD = LengthUnit == LengthUnitEnum.NanoMeter ? numericBoxCutoffD.Value : numericBoxCutoffD.Value / 10; // (260426Ch) 1 回だけの CutoffD helper をローカル化

        c.SetVectorOfG(cutoffD, waveSource);

        Array.Sort(c.VectorOfG, (g1, g2) => g2.d.CompareTo(g1.d));

        // 一旦 bindingSource を解除
        var dataMember = bindingSourceScatteringFactor.DataMember;
        dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        bindingSourceScatteringFactor.DataMember = "";

        dataSet.DataTableScatteringFactor.Clear();
        dataGridViewTextBoxColumnMulti.Visible = checkBoxHideEquivalentPlane.Checked;
        dataGridViewTextBoxColumnIntCondition.Visible = !checkBoxHideProhibitedPlanes.Checked;
        dataGridView.DefaultCellStyle.Format = "";
        dataGridView.VirtualMode = true;

        if (c.VectorOfG.Length == 0)
        {
            bindingSourceScatteringFactor.DataMember = dataMember;
            return;
        }

        var max = c.VectorOfG.Max(g => g.RawIntensity);
        for (int i = 0; i < c.VectorOfG.Length; i++)
            c.VectorOfG[i].RelativeIntensity = c.VectorOfG[i].RawIntensity / max;

        if (checkBoxBragBrentano.Checked)
        {
            max = double.NegativeInfinity;
            for (int i = 0; i < c.VectorOfG.Length; i++)
            {
                var g = c.VectorOfG[i];
                var twoTheta = 2 * Math.Asin(g.Length * waveLength / 2);
                if (SymmetryStatic.IsRootPlane(g.Index, c.Symmetry, out var indices) && !double.IsNaN(twoTheta))
                {
                    var magnitude2 = g.F.MagnitudeSquared(); // (260426Ch)
                    if (waveSource == WaveSource.Xray)
                        c.VectorOfG[i].RawIntensity = magnitude2 * indices.Length / c.CellVolumeSquare * (1 + Math.Cos(twoTheta) * Math.Cos(twoTheta)) / Math.Sin(twoTheta) / Math.Sin(twoTheta / 2);
                    else
                        c.VectorOfG[i].RawIntensity = magnitude2 * indices.Length / c.CellVolumeSquare / Math.Sin(twoTheta) / Math.Sin(twoTheta / 2);

                    max = Math.Max(max, c.VectorOfG[i].RawIntensity);
                }
            }

            for (int i = 0; i < c.VectorOfG.Length; i++)
                c.VectorOfG[i].RelativeIntensity = c.VectorOfG[i].RawIntensity / max;
        }

        foreach (var g in c.VectorOfG)
        {
            var root = SymmetryStatic.IsRootPlane(g.Index, c.Symmetry, out var indices);
            if (checkBoxHideEquivalentPlane.Checked && !root)
                continue;

            var condition = c.Symmetry.CheckExtinctionRule(g.Index);
            if (checkBoxHideProhibitedPlanes.Checked && condition.Length != 0)
                continue;

            var d = LengthUnit == LengthUnitEnum.NanoMeter ? 1 / g.Length : 1 / g.Length * 10;
            var twoTheta = 2 * Math.Asin(g.Length * waveLength / 2) / Math.PI * 180;
            dataSet.DataTableScatteringFactor.Add(g.Index.h, g.Index.k, g.Index.l, indices.Length, d, double.IsNaN(twoTheta) ? double.PositiveInfinity : twoTheta, g.F, g.RelativeIntensity, g.Extinction);
        }
        bindingSourceScatteringFactor.DataMember = dataMember;
    }

    /// <summary>テストコード</summary>
    private void numericBoxH_min_ValueChanged(object sender, EventArgs e)
    {
        var c = (Crystal)Crystal.Clone();
        var waveLength = waveLengthControl1.WaveLength;
        var waveSource = waveLengthControl1.WaveSource;

        // 一旦 bindingSource を解除
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
                    var twoTheta = 2 * Math.Asin(gLength * waveLength / 2) / Math.PI * 180;
                    var f = Crystal.GetStructureFactor(waveSource, c.Atoms, (h, k, l), 1 / d / d / 4.0);

                    dataSet.DataTableScatteringFactor.Add(h, k, l, 1, d, twoTheta, f, f.MagnitudeSquared(), []);
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
    }

    private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex != dataGridViewTextBoxColumnI.Index) return;
        var row = dataGridView.Rows[e.RowIndex];
        var h = Convert.ToInt32(row.Cells[dataGridViewTextBoxColumnH.Index].Value);
        var k = Convert.ToInt32(row.Cells[dataGridViewTextBoxColumnK.Index].Value);
        e.Value = (-h - k).ToString(); // (260424Ch) TextBoxCell の表示値は string にして DataGridView の型不一致を避ける
        e.FormattingApplied = true;
    }
}
