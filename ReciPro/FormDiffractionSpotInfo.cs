using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace ReciPro;
public partial class FormDiffractionSpotInfo : FormBase
{
    public double AccVol { get; set; }
    public BetheMethod.Beam[] Beams { get; set; }
    public FormDiffractionSimulator FormDiffractionSimulator = null;
    public FormImageSimulator FormImageSimulator = null;

    public enum UnitOfPotentialEnum { Ug, Vg };
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
        //260421Cl i 列追加により Ug 系列 index が +1 ずれる (9→10, 10→11, 11→12, 12→13)
        if (radioButtonUnitEV.Checked)
        {
            coeff = 1 / gamma * 6.62606896 * 6.62606896 / 2 / 9.1093897 / 1.60217733; //coeff = 1 / gamma * UniversalConstants.h * UniversalConstants.h / 2 / UniversalConstants.m0 / UniversalConstants.e0 * 1e18;
            dataGridView.Columns[10].HeaderText = "Vg re";
            dataGridView.Columns[11].HeaderText = "Vg im";
            dataGridView.Columns[12].HeaderText = "V'g re";
            dataGridView.Columns[13].HeaderText = "V'g im";

        }
        else
        {
            dataGridView.Columns[10].HeaderText = "Ug re";
            dataGridView.Columns[11].HeaderText = "Ug im";
            dataGridView.Columns[12].HeaderText = "U'g re";
            dataGridView.Columns[13].HeaderText = "U'g im";
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
            r.i = -(beam.H + beam.K);                                                                                                                     // 260421Cl Miller-Bravais i = -(h+k) を自動計算
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
            //260421Cl h, k に加えて i もスペース区切りに含める (l が h k l グループの最後のためタブで閉じる)
            //260422Cl 列メタ情報 (visible / header / スペース区切りか) をループ外で一度だけ取得
            var colCount = dataGridView.ColumnCount;
            var visible = new bool[colCount];
            var headers = new string[colCount];
            var spaceSep = new bool[colCount];
            for (int i = 0; i < colCount; i++)
            {
                visible[i] = dataGridView.Columns[i].Visible;
                headers[i] = dataGridView.Columns[i].HeaderText;
                spaceSep[i] = headers[i] == "h" || headers[i] == "k" || headers[i] == "i";
            }

            for (int i = 0; i < colCount; i++)
                if (visible[i])
                    sb.Append(spaceSep[i] ? headers[i] : $"{headers[i]}\t");
            sb.Append("\r\n");

            for (int j = 0; j < dataGridView.Rows.Count; j++)
            {
                for (int i = 0; i < colCount; i++)
                    if (visible[i])
                        sb.Append(spaceSep[i] ? $"{dataGridView[i, j].Value} " : $"{dataGridView[i, j].Value}\t");
                sb.Append("\r\n");
            }
            Clipboard.SetDataObject(sb.ToString());
        }
    }

    //260421Cl 追加: Miller-Bravais i 列の表示/非表示を切替える (FormMain から呼ばれる)
    public void UpdatePlaneIndices(bool show) => iDataGridViewTextBoxColumn.Visible = show;



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
            //260421Cl i 列追加により l 列 index が 3 → 4 にずれる
            var (h, k, l) = ((int)dataGridView[1, j].Value, (int)dataGridView[2, j].Value, (int)dataGridView[4, j].Value);
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

            //260421Cl i 列追加により σ|Φ|² 列 index が 18 → 19 にずれる
            for (int j = 0; j < dataGridView.Rows.Count; j++)
                sb.Append($"{dataGridView[19, j].Value}\t");

            sb.Append("\r\n");
            // 260428Cl FormDiffractionSimulator.Thickness setter が UI に強く結合した同期処理を起動するため、
            // ループ全体を Task.Run へ移すには Bethe 計算の async 化が必要 (D 群以降の課題)。当面 DoEvents で UI を維持する。
            if (thickness % 10 == 0)
                Application.DoEvents();
        }
        Clipboard.SetDataObject(sb.ToString());
    }
}

