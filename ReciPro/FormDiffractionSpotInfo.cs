﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace ReciPro;
public partial class FormDiffractionSpotInfo : Form
{
    public double AccVol { get; set; }
    public BetheMethod.Beam[] Beams { get; set; }
    public FormDiffractionSimulator FormDiffractionSimulator = null;
    public FormImageSimulator FormImageSimulator = null;
    
    public enum UnitOfPotentialEnum { Ug,Vg};
    public UnitOfPotentialEnum UnitOfPotential
    {
        get => radioButtonUnitEV.Checked ? UnitOfPotentialEnum.Vg : UnitOfPotentialEnum.Ug;
        set
        {
            radioButtonUnitEV.Checked = value == UnitOfPotentialEnum.Vg;
            radioButtonUnitNM.Checked = !radioButtonUnitEV.Checked;
        }
    }
    public FormDiffractionSpotInfo()
    {
        InitializeComponent();

        //DataGridViewの画面ちらつきをおさえるため、DoubleBufferedを有効にする
        // DataGirdViewのTypeを取得
        var dgvtype = typeof(DataGridView);
        // プロパティ設定の取得
        var dgvPropertyInfo = dgvtype.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
        // 対象のDataGridViewにtrueをセットする
        dgvPropertyInfo.SetValue(dataGridView, true, null);
        typeof(DataGridView).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dataGridView, true, null);
    }

    public void SetTable(double acc, Crystal crystal)
    {
        //波数を計算
        var kvac = UniversalConstants.Convert.EnergyToElectronWaveNumber(acc);
        //U0を計算
        var u0 = crystal.Bethe.getU(acc).Real.Real;

        crystal.Bethe.Beams = crystal.VectorOfG.Where(g => g.Flag2).Select(g =>
        {
            return new BetheMethod.Beam(
                g.Index,
                crystal.RotationMatrix * g,
                crystal.Bethe.getU(acc, new BetheMethod.Beam(g)),
                crystal.Bethe.getQP(g, kvac, u0));
        }).ToArray();

        SetTable(acc, crystal.Bethe.Beams);
    }


    public void SetTable(double acc = 0, BetheMethod.Beam[] beams = null)
    {
        var sw = new Stopwatch();
        sw.Start();
        toolStripStatusLabel1.Text = "";
        if (acc != 0)
            AccVol = acc;
        if (beams != null)
            Beams = beams;
        if (Beams == null)
            return;
        textBoxAccVoltage.Text = AccVol.ToString();
        textBoxWaveLength.Text = UniversalConstants.Convert.EnergyToElectronWaveLength(AccVol).ToString("f10");
        var gamma = 1 + UniversalConstants.e0 * AccVol * 1000 / UniversalConstants.m0 / UniversalConstants.c2;
        textBoxGamma.Text = gamma.ToString("f8");
        textBoxBeta.Text = (Math.Sqrt(gamma * gamma - 1) / gamma).ToString("f8");

        var crystal = FormDiffractionSimulator != null ?
            FormDiffractionSimulator.formMain.Crystal :
            FormImageSimulator.FormMain.Crystal;

        textBoxLatticeVolume.Text = crystal.Volume.ToString("f8");

        if (FormDiffractionSimulator != null)
        {
            if (FormDiffractionSimulator.CalcMode == FormDiffractionSimulator.CalcModes.Dynamical)
            {
                if (FormDiffractionSimulator.BeamMode == FormDiffractionSimulator.BeamModes.Convergence)
                {
                    textBoxThickness.Text = FormDiffractionSimulator.FormDiffractionSimulatorCBED.textBoxThickness.Text;
                    textBoxSemiangle.Text = (FormDiffractionSimulator.FormDiffractionSimulatorCBED.AlphaMax * 1000.0).ToString();
                }
                else
                    textBoxThickness.Text = FormDiffractionSimulator.numericBoxThickness.Value.ToString();
            }
        }

        double coeff = 1 / gamma;
        if (radioButtonUnitEV.Checked)
        {
            coeff = 1 / gamma * 6.62606896 * 6.62606896 / 2 / 9.1093897 / 1.60217733; //coeff = 1 / gamma * UniversalConstants.h * UniversalConstants.h / 2 / UniversalConstants.m0 / UniversalConstants.e0 * 1e18;
            dataGridView.Columns[9].HeaderText = "Vg re";
            dataGridView.Columns[10].HeaderText = "Vg im";
            dataGridView.Columns[11].HeaderText = "V'g re";
            dataGridView.Columns[12].HeaderText = "V'g im";

        }
        else
        {
            dataGridView.Columns[9].HeaderText = "Ug re";
            dataGridView.Columns[10].HeaderText = "Ug im";
            dataGridView.Columns[11].HeaderText = "U'g re";
            dataGridView.Columns[12].HeaderText = "U'g im";
        }

        var rows = new List<DataSetReciPro.DataTableBetheRow>(Beams.Length);
        for (int i = 0; i < Beams.Length; i++)
        {
            var beam = Beams[i];
            var g = beam.Vec;
            var k = g.Length;

            var r = dataSet.DataTableBethe.NewDataTableBetheRow();

            r.R = beam.Rating;
            r.h = beam.H;
            r.k = beam.K;
            r.l = beam.L;
            r.d = 1 / k;
            r.gX = Math.Abs(beam.Vec.X) > 1e-12 ? g.X : 0;
            r.gY = Math.Abs(beam.Vec.Y) > 1e-12 ? g.Y : 0;
            r.gZ = Math.Abs(beam.Vec.Z) > 1e-12 ? g.Z : 0;
            r.__g_ = k;
            r.Ug_re = Math.Abs(beam.Ureal.Real) > 1E-12 ? beam.Ureal.Real * coeff : 0;
            r.Ug_im = Math.Abs(beam.Ureal.Imaginary) > 1E-12 ? beam.Ureal.Imaginary * coeff : 0;
            r._U_g_re = Math.Abs(beam.Uimag.Real) > 1E-12 ? beam.Uimag.Real * coeff : 0;
            r._U_g_im = Math.Abs(beam.Uimag.Imaginary) > 1E-12 ? beam.Uimag.Imaginary * coeff : 0;
            r.Sg = beam.S;
            r.Pg = beam.P;
            r.Qg = beam.Q;
            r.Φ_re = Math.Abs(beam.Psi.Real) > 1e-12 ? beam.Psi.Real : 0;
            r.Φ_im = Math.Abs(beam.Psi.Imaginary) > 1e-12 ? beam.Psi.Imaginary : 0;
            r.__Φ__2 = Math.Abs(beam.Psi.Magnitude * beam.Psi.Magnitude) > 1e-12 ? beam.Psi.Magnitude * beam.Psi.Magnitude : 0;

            rows.Add(r);
        }
        toolStripStatusLabel1.Text += "Time for creation of rows: " + sw.ElapsedMilliseconds + "ms.  ";
        sw.Restart();

        //dataGridViewの内容の書き換えは非常に時間がかかるので、まず、更新すべきかどうかをチェック
        bool need = false;
        for (int i = 0; i < Math.Max(Beams.Length, dataSet.DataTableBethe.Rows.Count) && !need; i++)
        {
            if (i < Beams.Length && i < dataSet.DataTableBethe.Rows.Count)
            {
                for (int j = 0; j < dataSet.DataTableBethe.Rows[i].ItemArray.Length && !need; j++)
                {
                    if (dataSet.DataTableBethe.Rows[i][j] == DBNull.Value)
                        need = true;
                    else if (rows[i][j] is double d && !double.IsInfinity(d))
                        need = (double)dataSet.DataTableBethe.Rows[i][j] != d;
                    else if (rows[i][j] is int val)
                        need = (int)dataSet.DataTableBethe.Rows[i][j] != val;
                }
            }
            else
                need = true;
        }


        if (need)
        {
            dataGridView.DataMember = "";
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            dataSet.DataTableBethe.Rows.Clear();
            foreach (var r in rows)
                dataSet.DataTableBethe.AddDataTableBetheRow(r);


            dataGridView.VirtualMode = true;
            if (checkBoxAutoRowSize.Checked)
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView.DataMember = "DataTableBethe";
            toolStripStatusLabel1.Text += $"Time for displaying table: {sw.ElapsedMilliseconds} ms.  ";
        }
    }

    private void FormDiffractionSimulatorTable_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        this.Visible = false;

    }

    private void numericBoxEffectiveDigit_ValueChanged(object sender, EventArgs e)
        => dataGridView.DefaultCellStyle.Format = "g" + numericBoxEffectiveDigit.ValueInteger.ToString();

    private void buttonCopyToClipboard_Click(object sender, EventArgs e)
    {
        if (dataSet.DataTableBethe.Rows.Count > 1)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < dataGridView.Columns.Count; i++)
                if (dataGridView.Columns[i].Visible)
                {
                    if (dataGridView.Columns[i].HeaderText == "h" || dataGridView.Columns[i].HeaderText == "k")
                        sb.Append(dataGridView.Columns[i].HeaderText);
                    else
                        sb.Append($"{dataGridView.Columns[i].HeaderText}\t");
                }
            sb.Append("\r\n");

            for (int j = 0; j < dataGridView.Rows.Count; j++)
            {
                for (int i = 0; i < dataGridView.ColumnCount; i++)
                    if (dataGridView.Columns[i].Visible)
                    {
                        if (dataGridView.Columns[i].HeaderText == "h")
                            sb.Append($"{dataGridView[i, j].Value} ");
                        else if (dataGridView.Columns[i].HeaderText == "k")
                            sb.Append($"{dataGridView[i, j].Value} ");
                        else
                            sb.Append($"{dataGridView[i, j].Value}\t");
                    }
                sb.Append("\r\n");
            }
            Clipboard.SetDataObject(sb.ToString());
        }
    }



    private void radioButtonUnitEV_CheckedChanged(object sender, EventArgs e) => SetTable();

    private void CheckBoxAutoRowSize_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBoxAutoRowSize.Checked)
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        else
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

    }

    private void DataGridView_Scroll(object sender, ScrollEventArgs e)
    {
        if (checkBoxAutoRowSize.Checked)
        {
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (dataSet.DataTableBethe.Rows.Count < 1)
            return;

        var sb = new StringBuilder("\t");

        for (int j = 0; j < dataGridView.Rows.Count; j++)
        {
            var (h, k, l) = ((int)dataGridView[1, j].Value, (int)dataGridView[2, j].Value, (int)dataGridView[3, j].Value);
            if (h == 0 && k == 0 && l == 0)
                sb.Append($"000 \t");
            //sb.Append($"{h} {k} {l} 000 \t");
            else if ((h + k + l) % 2 == 0)
                sb.Append($"{h} {k} {l} even \t");
            else
                sb.Append($"{h} {k} {l} odd \t");

        }
        sb.Append("\r\n");

        for (double thickness = 1; thickness <= 300; thickness += 1)
        {
            FormDiffractionSimulator.Thickness = thickness;

            sb.Append($"{thickness}\t");

            for (int j = 0; j < dataGridView.Rows.Count; j++)
                sb.Append($"{dataGridView[18, j].Value}\t");

            sb.Append("\r\n");
            if (thickness % 10 == 0)
                Application.DoEvents();
        }
        Clipboard.SetDataObject(sb.ToString());
    }
}
