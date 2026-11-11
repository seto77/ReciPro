using System.Collections.Generic;
using System.Diagnostics;
using System.IO; // 260723Cl 追加 (CSV 保存用)
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

        //DataGridViewの画面ちらつきをおさえるため、DoubleBufferedを有効にする (non-public プロパティのため reflection)
        //260717Cl: 同一の SetValue が 2 回実行されていたため 1 回に
        typeof(DataGridView).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dataGridView, true, null);
    }

    public void SetTable(double acc, Crystal crystal)
    {
        // 261105Cl 変更 (監査 #7・C1): Sg・Pg・Qg (と R) を回転前の g で計算していた (同じ行の g は回転後)。また共有の crystal.Bethe.Beams を
        //  運動学の一覧で上書きしていた。回転後の g で計算し、この表だけの配列にする。
        // 261105Cl 変更 (監査 C5): X 線・中性子でも電子線として (keV を kV とみなした電子の波数と U0 で) 計算していた。
        //  X 線・中性子は実際の波長 (屈折なし) で Sg・Pg・Qg を求め、電子線の結晶ポテンシャルは計算しない (列は SetTable で隠す)。
        var electron = (FormDiffractionSimulator?.Source ?? WaveSource.Electron) == WaveSource.Electron; // 261105Cl
        //波数を計算
        //var kvac = UniversalConstants.Convert.EnergyToElectronWaveNumber(acc); // 261105Cl 変更前
        var kvac = electron ? UniversalConstants.Convert.EnergyToElectronWaveNumber(acc) : 1 / FormDiffractionSimulator.WaveLength; // 261105Cl
        //U0を計算
        //var u0 = crystal.Bethe.getU(acc).Real.Real; // 261105Cl 変更前
        var u0 = electron ? crystal.Bethe.getU(acc).Real.Real : 0; // 261105Cl

        //crystal.Bethe.Beams = crystal.VectorOfG.Where(g => g.Flag2).Select(g => // 261105Cl 変更前 (以下 SetTable の呼び出しまで)
        //{
        //    return new BetheMethod.Beam(
        //        g.Index,
        //        crystal.RotationMatrix * g,
        //        crystal.Bethe.getU(acc, new BetheMethod.Beam(g)),
        //        crystal.Bethe.getQP(g, kvac, u0));
        //}).ToArray();
        //
        //SetTable(acc, crystal.Bethe.Beams);
        var beams = crystal.VectorOfG.Where(g => g.Flag2).Select(g => // 261105Cl
        {
            var vec = crystal.RotationMatrix * g; // 261105Cl
            return new BetheMethod.Beam(g.Index, vec,
                electron ? crystal.Bethe.getU(acc, new BetheMethod.Beam(g)) : default, // 261105Cl
                crystal.Bethe.getQP(vec, kvac, u0)); // 261105Cl
        }).ToArray();
        SetTable(acc, beams); // 261105Cl
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
        var electron = (FormDiffractionSimulator?.Source ?? WaveSource.Electron) == WaveSource.Electron; // 261105Cl 追加 (監査 C5)
        //textBoxAccVoltage.Text = AccVol.ToString(); // 261105Cl 変更前: X 線 (keV)・中性子 (meV) のエネルギーも kV の加速電圧として電子線の波長・γ・β を出していた
        //textBoxWaveLength.Text = UniversalConstants.Convert.EnergyToElectronWaveLength(AccVol).ToString("f10");
        textBoxAccVoltage.Text = electron ? AccVol.ToString() : "-"; // 261105Cl
        textBoxWaveLength.Text = (electron ? UniversalConstants.Convert.EnergyToElectronWaveLength(AccVol) : FormDiffractionSimulator.WaveLength).ToString("f10"); // 261105Cl
        var gamma = 1 + UniversalConstants.e0 * AccVol * 1000 / UniversalConstants.m0 / UniversalConstants.c2;
        //textBoxGamma.Text = gamma.ToString("f8"); // 261105Cl 変更前
        //textBoxBeta.Text = (Math.Sqrt(gamma * gamma - 1) / gamma).ToString("f8");
        textBoxGamma.Text = electron ? gamma.ToString("f8") : "-"; // 261105Cl
        textBoxBeta.Text = electron ? (Math.Sqrt(gamma * gamma - 1) / gamma).ToString("f8") : "-"; // 261105Cl
        for (int c = 10; c <= 13; c++) // 261105Cl 追加: Ug/U'g (Vg/V'g) は電子線の結晶ポテンシャルなので X 線・中性子では隠す (Copy/CSV も表示中の列だけ)
            dataGridView.Columns[c].Visible = electron; // 261105Cl
        // 261105Cl 追加 (作者決定 C5 の続き): ポテンシャルの単位 (見出し「Unit of potential:」と Vg [eV] / Ug [nm⁻²] のラジオ) と、
        //  その換算式の行 (label14) も、列と同じく X 線・中性子では隠す
        flowLayoutPanelEnergyUnit.Visible = label14.Visible = electron; // 261105Cl

        var crystal = FormDiffractionSimulator != null ?
            FormDiffractionSimulator.formMain.Crystal :
            FormImageSimulator.FormMain.Crystal;

        textBoxLatticeVolume.Text = crystal.Volume.ToString("f8");

        // 261105Cl 追加 (監査 C22): 厚さ・収束半角は使うとき (動力学・CBED) だけ書き込んでいたので、運動学などに切り替えても前の値が残っていた
        textBoxThickness.Text = "-"; // 261105Cl
        textBoxSemiangle.Text = "-"; // 261105Cl
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
        var sumPhi2 = 0.0; // 260829Cl 追加: Σ|Φ|² (R 昇順の累積強度)。2021 年の DataSet 化以降、列だけ残って未代入 (常に DBNull = 空列) だった
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
            // 260829Cl 変更前: r.__Φ__2 = Math.Abs(beam.Psi.Magnitude * beam.Psi.Magnitude) > 1e-12 ? beam.Psi.Magnitude * beam.Psi.Magnitude : 0;
            var phi2 = beam.Psi.Magnitude * beam.Psi.Magnitude;
            r.__Φ__2 = phi2 > 1e-12 ? phi2 : 0;
            sumPhi2 += phi2;
            r._Σ_Φ__2 = sumPhi2; // 260829Cl 追加: 強度保存の目安 (マニュアル記載の列)。厳密な保存量は無吸収時の Σ P_g|Φ_g|²/P_0 = 1 で、この単純和は P_g ≈ P_0 の範囲で ≈1

            rows.Add(r);
        }
        toolStripStatusLabel1.Text += "Time for creation of rows: " + StatusBarHelper.FormatElapsed(sw.Elapsed) + ".  ";// 260520Cl 時間表記を統一 (旧: ms前の空白欠落も修正)
        sw.Restart();

        //dataGridViewの内容の書き換えは非常に時間がかかるので、まず、更新すべきかどうかをチェック
        bool need = false;
        var columnCount = dataSet.DataTableBethe.Columns.Count; // 261111Cl 追加: ItemArray.Length と同じ値 (ItemArray は読むたびに全列を箱詰めした object[] を作る)
        for (int i = 0; i < Math.Max(Beams.Length, dataSet.DataTableBethe.Rows.Count) && !need; i++)
        {
            if (i < Beams.Length && i < dataSet.DataTableBethe.Rows.Count)
            {
                //for (int j = 0; j < dataSet.DataTableBethe.Rows[i].ItemArray.Length && !need; j++) // 261111Cl 変更前
                for (int j = 0; j < columnCount && !need; j++) // 261111Cl 変更
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
            toolStripStatusLabel1.Text += $"Time for displaying table: {StatusBarHelper.FormatElapsed(sw.Elapsed)}.  ";
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
        //if (dataSet.DataTableBethe.Rows.Count > 1) // 261105Cl 変更前 (監査 C22): 1 行 (透過波 000 だけ) の表では Copy も CSV も何もしなかった
        // 261105Cl 変更: 表の文字列を作る部分は BuildTableText へ移した (Copy と CSV で同じ内容。画面を出さずに確かめられるように)
        var toClipboard = (sender as Button).Name.Contains("Clipboard"); // 261105Cl BuildTableText から移した
        var text = BuildTableText(toClipboard); // 261105Cl
        if (text == null) return; // 261105Cl 行が無い
        //Clipboard.SetDataObject(sb.ToString()); // 260723Cl 旧: クリップボードコピーのみ
        if (toClipboard)
            Clipboard.SetDataObject(text); // 261105Cl 旧: sb.ToString()
        else
        {
            var dlg = new SaveFileDialog { Filter = "*.csv|*.csv" };
            if (dlg.ShowDialog() == DialogResult.OK)
                using (var sw = new StreamWriter(dlg.FileName, false, Encoding.UTF8))
                    sw.Write(text.Replace('\t', ',')); // 261105Cl 旧: sb.ToString().Replace('\t', ',')
        }
    }

    /// <summary>261105Cl 追加: Copy to clipboard・Save as CSV の文字列 (列の区切りはタブ。CSV は呼び出し側でカンマに置き換える)。行が無ければ null。
    /// 先頭に計算の条件を "# key: value" の行で置き、見出しに単位を付ける (作者決定 C22)。</summary>
    /// <param name="toClipboard">true ならクリップボード用 (数値は今のカルチャ)、false なら CSV 用 (InvariantCulture)</param>
    internal string BuildTableText(bool toClipboard)
    {
        if (dataSet.DataTableBethe.Rows.Count == 0) return null;
        var sb = new StringBuilder();
        // 260723Cl 追加: buttonSaveAsFile からも共用し、sender 名でクリップボード/CSV ファイル保存に分岐 (FormSpotIDV2 と同型)。
        //   クリップボードは従来どおり現在のカルチャ (Excel 等へのペースト用)、ファイルは InvariantCulture (小数点=ピリオド) で
        //   出力し、小数点=カンマのロケールでも CSV が壊れないようにする (codex 指摘)。
        // 261105Cl 変更: toClipboard は引数にし (呼び出し側の buttonCopyToClipboard_Click で sender から判定)、inv は条件の行でも使うので表の見出しより前へ移した
        //var toClipboard = (sender as Button).Name.Contains("Clipboard"); // 261105Cl 変更前
        var inv = System.Globalization.CultureInfo.InvariantCulture;

        // 261105Cl 追加 (作者決定 C22): 表の前に計算の条件を "# key: value" の行で書く (Copy と CSV で同じ内容)。
        //   '#' で始まる行は注釈として読み飛ばせる (pandas の comment='#'、R の comment.char など)。行の中にタブは入れず、
        //   CSV では値の中のカンマ (結晶名など) を ';' にする (注釈を読み飛ばさない表計算ソフトでも 1 行が 1 つのセルに収まる)。
        //   数値はクリップボードなら今のカルチャ、CSV なら InvariantCulture (表の値と同じ)
        var culture = toClipboard ? System.Globalization.CultureInfo.CurrentCulture : inv; // 261105Cl
        var sim = FormDiffractionSimulator; // 261105Cl (null なら像シミュレータから開いた表)
        var main = sim?.formMain ?? FormImageSimulator?.FormMain; // 261105Cl
        var source = sim?.Source ?? WaveSource.Electron; // 261105Cl
        void condition(string key, string value) => sb.Append("# ").Append(key).Append(": ").Append(toClipboard ? value.Replace('\t', ' ') : value.Replace('\t', ' ').Replace(',', ';')).Append("\r\n"); // 261105Cl
        string number(double v, string format = "") => v.ToString(format, culture); // 261105Cl
        condition("generator", $"ReciPro {Version.VersionAndDate} (diffraction spot information)"); // 261105Cl
        condition("source", source.ToString()); // 261105Cl
        if (source == WaveSource.Electron) // 261105Cl
            condition("accelerating_voltage_kV", number(AccVol)); // 261105Cl
        else // 261105Cl X 線は keV、中性子は meV (WaveLengthControl.Energy の単位)
            condition(source == WaveSource.Xray ? "energy_keV" : "energy_meV", number(AccVol)); // 261105Cl
        condition("wavelength_nm", number(source == WaveSource.Electron ? UniversalConstants.Convert.EnergyToElectronWaveLength(AccVol) : sim.WaveLength, "g10")); // 261105Cl
        //  厚さ・収束半角は、表の上の欄 (SetTable が使うモードのときだけ書き、ほかは "-") に出ている値。今のカルチャの文字列なので数値に戻して書き直す
        var cur = System.Globalization.CultureInfo.CurrentCulture; // 261105Cl
        if (double.TryParse(textBoxThickness.Text, System.Globalization.NumberStyles.Float, cur, out var thickness)) // 261105Cl
            condition("thickness_nm", number(thickness)); // 261105Cl
        if (double.TryParse(textBoxSemiangle.Text, System.Globalization.NumberStyles.Float, cur, out var semiangle)) // 261105Cl
            condition("max_semiangle_mrad", number(semiangle)); // 261105Cl
        if (main?.Crystal is Crystal crystal) // 261105Cl
            condition("crystal", $"{crystal.Name} / {crystal.Symmetry.SpaceGroupHMStr}"); // 261105Cl
        if (main != null) // 261105Cl 主窓の Euler 角 (FormMain の Phi, Theta, Psi。度)
            condition("euler_angles_deg (Phi Theta Psi)", $"{number(main.Phi * 180 / Math.PI, "f4")} {number(main.Theta * 180 / Math.PI, "f4")} {number(main.Psi * 180 / Math.PI, "f4")}"); // 261105Cl
        condition("calculation", sim == null ? $"Image simulator ({FormImageSimulator.ImageMode})" // 261105Cl
            : (sim.CalcMode switch { FormDiffractionSimulator.CalcModes.Excitation => "Only excitation error", FormDiffractionSimulator.CalcModes.Kinematical => "Kinematical", _ => "Dynamical (Bloch wave)" })
            + " / " + (sim.IsBackLaue ? "Back Laue" : sim.BeamMode switch
            {
                FormDiffractionSimulator.BeamModes.Parallel => "Parallel beam",
                FormDiffractionSimulator.BeamModes.PrecessionElectron => "Precession (electron)",
                FormDiffractionSimulator.BeamModes.PrecessionXray => "Precession (X-ray)",
                _ => "Convergent beam (CBED)",
            })); // 261105Cl

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
            {
                //sb.Append(spaceSep[i] ? headers[i] : $"{headers[i]}\t"); // 261105Cl 変更前: 単位が無かった
                // 261105Cl 変更 (作者決定 C22): 見出しに単位を付ける。Ug 系の列は選んだ単位 (Vg [eV] / Ug [nm⁻²])。無次元の量 (指数・Φ・|Φ|²・Σ|Φ|²) には付けない
                var unit = dataGridView.Columns[i].DataPropertyName switch // 261105Cl
                {
                    "R" => "nm⁻⁵", // R = |g| Qg²
                    "d" => "nm",
                    "gX" or "gY" or "gZ" or "|g|" or "Sg" or "Pg" => "nm⁻¹",
                    "Qg" => "nm⁻²",
                    "Ug re" or "Ug im" or "U'g re" or "U'g im" => radioButtonUnitEV.Checked ? "eV" : "nm⁻²",
                    _ => null,
                };
                var header = unit == null ? headers[i] : $"{headers[i]} ({unit})"; // 261105Cl
                sb.Append(spaceSep[i] ? header : $"{header}\t"); // 261105Cl
            }
        sb.Append("\r\n");

        for (int j = 0; j < dataGridView.Rows.Count; j++)
        {
            for (int i = 0; i < colCount; i++)
                if (visible[i])
                {
                    //sb.Append(spaceSep[i] ? $"{dataGridView[i, j].Value} " : $"{dataGridView[i, j].Value}\t"); // 260723Cl 旧
                    var value = toClipboard ? $"{dataGridView[i, j].Value}" : Convert.ToString(dataGridView[i, j].Value, inv);
                    sb.Append(spaceSep[i] ? $"{value} " : $"{value}\t");
                }
            sb.Append("\r\n");
        }
        return sb.ToString();
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

