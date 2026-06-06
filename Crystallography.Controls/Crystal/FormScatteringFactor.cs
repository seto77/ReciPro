using MathNet.Numerics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Crystallography.Controls;

public partial class FormScatteringFactor : FormBase
{
    #region フィールド・プロパティ
    public Crystal Crystal => CrystalControl.Crystal;
    public CrystalControl CrystalControl;

    /// <summary>長さの単位の get/set</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public LengthUnitEnum LengthUnit => waveLengthControl1.LengthUnit;

    // 260425Cl WFO1000 対策: デザイナのシリアライゼーション対象から除外
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool MillerBravais { get => dataGridViewTextBoxColumnI.Visible; set => dataGridViewTextBoxColumnI.Visible = value; }
    #endregion

    #region 起動・終了
    public FormScatteringFactor()
    {
        InitializeComponent();
    }
    private void FormCrystallographicInformation_Load(object sender, EventArgs e)
    {
        // (260426Ch) 古い EventHandler 明示生成とコメント typo を整理
        CrystalControl.CrystalChanged += crystalControl_CrystalChanged;
        typeof(DataGridView).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dataGridView, true, null);
        InitializeScatteringFactorsTab(); // 260606Cl
    }

    private void FormCrystallographicInformation_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        Visible = false;
    }

    #endregion 

    #region 共通: Crystal / 波長変更の追従 (両タブを更新)
    // CrystalControl で Crystal が変更されたとき
    private void crystalControl_CrystalChanged(object sender, EventArgs e)
    {
        numericBoxCutoffD.Minimum = (Crystal.A + Crystal.B + Crystal.C) / 20;
        if (Visible)
        {
            SetSortedPlanes();
            UpdateScatteringFactors(); // 260606Cl
        }
    }
    // 表示時に再計算
    private void FormScatteringFactor_VisibleChanged(object sender, EventArgs e)
    {
        if (Visible)
        {
            SetSortedPlanes();
            UpdateScatteringFactors(); // 260606Cl
        }
    }
    // 上部帯の波長 / ビーム変更 → 両タブを再計算
    private void waveLengthControl1_WavelengthChanged(object sender, EventArgs e)
    {
        SetSortedPlanes();
        UpdateScatteringFactors(); // 260606Cl
    }
    #endregion

    #region Reflections タブ (反射・構造因子テーブル)
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

    #region テストコード (手動 h,k,l 掃引)
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
    #endregion

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
    #endregion

    #region Scattering factors タブ (X線 / 電子線) 260606Cl 追加

    // 曲線描画の s 範囲 (s = sinθ/λ, 単位 Å⁻¹) と分割数
    private const double scatSMaxAng = 2.0;
    private const int scatPoints = 200;

    // 元素ごとの曲線色 (matplotlib tab10 相当)
    private static readonly Color[] scatPalette =
    [
        Color.FromArgb(0x1f, 0x77, 0xb4), Color.FromArgb(0xff, 0x7f, 0x0e), Color.FromArgb(0x2c, 0xa0, 0x2c),
        Color.FromArgb(0xd6, 0x27, 0x28), Color.FromArgb(0x94, 0x67, 0xbd), Color.FromArgb(0x8c, 0x56, 0x4b),
        Color.FromArgb(0xe3, 0x77, 0xc2), Color.FromArgb(0x7f, 0x7f, 0x7f), Color.FromArgb(0xbc, 0xbd, 0x22),
        Color.FromArgb(0x17, 0xbe, 0xcf),
    ];

    private enum ElectronModel { Peng, Kirkland, EightGaussian }

    // MiniTable のカーソル再計算用に現在の構成元素を保持
    private readonly record struct ScatElement(string Name, int Z, int Sub, double Biso, Color Color);
    private ScatElement[] scatElements = [];
    private double scatCursorS = 0.5; // カーソル縦線位置 (s, Å⁻¹)。ドラッグで更新 (0 だと Y 軸に重なり掴みにくいので 0.5 始点)
    private bool scatColsElectron, scatColsReady; // MiniTable 列構成のキャッシュ

    /// <summary>Scattering factors タブのイベント結線と初期化 (Load から)。Designer を触らずコード側で配線する。</summary>
    private void InitializeScatteringFactorsTab()
    {
        foreach (var rb in new[] { radioButtonXrayFs, radioButtonXrayFqSq, radioButtonElectronPeng, radioButtonElectronKirkland, radioButtonElectronEightGaussian })
            rb.CheckedChanged += scattering_OptionChanged;
        checkBoxDebyeWaller.CheckedChanged += scattering_OptionChanged;
        graphControlScatteringFactor.LinePositionChanged += scattering_LinePositionChanged;

        if (!radioButtonXrayFs.Checked && !radioButtonXrayFqSq.Checked) radioButtonXrayFs.Checked = true;                  // 既定: f(s)
        if (!radioButtonElectronPeng.Checked && !radioButtonElectronKirkland.Checked && !radioButtonElectronEightGaussian.Checked) radioButtonElectronPeng.Checked = true; // 既定: Peng

        graphControlScatteringFactor.VerticalLineMarkerVisible = true; // 各曲線にカーソル交点マーカー
    }

    private ElectronModel CurrentElectronModel()
        => radioButtonElectronKirkland.Checked ? ElectronModel.Kirkland : radioButtonElectronEightGaussian.Checked ? ElectronModel.EightGaussian : ElectronModel.Peng;

    private void scattering_OptionChanged(object sender, EventArgs e)
    {
        if (sender is RadioButton { Checked: false }) return; // ラジオの解除側イベントは無視
        UpdateScatteringFactors();
    }

    private void scattering_LinePositionChanged()
    {
        var lines = graphControlScatteringFactor.VerticalLines;
        if (lines.Length > 0) scatCursorS = lines[0].X;
        UpdateScatteringTable(scatCursorS); // 各元素の f(s) を MiniTable へ読み出し
    }

    /// <summary>ビーム種 (X線 / 電子線) に応じて散乱因子の曲線と表を更新する。</summary>
    private void UpdateScatteringFactors()
    {
        if (!IsHandleCreated || Crystal == null) return;

        var src = waveLengthControl1.WaveSource;

        // モード切替ラジオ (flowLayoutPanel) をビームで表示/非表示
        flowLayoutPanelModel_Xray.Visible = src == WaveSource.Xray;
        flowLayoutPanelModel_Electron.Visible = src == WaveSource.Electron;

        // 今回の実装対象は X線 / 電子線のみ (中性子は別フェーズ)
        if (src != WaveSource.Xray && src != WaveSource.Electron)
        {
            graphControlScatteringFactor.ClearProfile();
            miniTable1.ClearRows();
            return;
        }

        bool electron = src == WaveSource.Electron;
        bool fqsq = !electron && radioButtonXrayFqSq.Checked; // F(q)+S(q) は xraylib 待ち (P4)

        scatElements = BuildScatElements(electron);
        DrawScatteringCurves(electron, fqsq);

        SetupScatColumns(electron);
        graphControlScatteringFactor.VerticalLines = [new PointD(scatCursorS, double.NaN)];
        graphControlScatteringFactor.Draw(); // 縦線 + 交点マーカーを反映
        UpdateScatteringTable(scatCursorS);
    }

    /// <summary>構成元素を (Z, sub) で重複排除して取り出す。</summary>
    private ScatElement[] BuildScatElements(bool electron)
    {
        var list = new List<ScatElement>();
        var seen = new HashSet<(int z, int sub)>();
        int ci = 0;
        foreach (var a in Crystal.Atoms)
        {
            int z = a.AtomicNumber;
            int sub = electron ? a.SubNumberElectron : a.SubNumberXray;
            if (z <= 0 || !seen.Add((z, sub))) continue;
            var name = string.IsNullOrEmpty(a.ElementName) ? AtomStatic.AtomicName(z) : a.ElementName;
            list.Add(new ScatElement(name, z, sub, a.Dsf?.Biso ?? 0, scatPalette[ci++ % scatPalette.Length]));
        }
        return [.. list];
    }

    /// <summary>各元素の f(s) / fₑ(s) 曲線を graphControl に描く。</summary>
    private void DrawScatteringCurves(bool electron, bool fqsq)
    {
        if (scatElements.Length == 0)
        {
            graphControlScatteringFactor.ClearProfile();
            graphControlScatteringFactor.GraphTitle = "";
            return;
        }
        if (fqsq) // 260606Cl: X線 F(q)+S(q) モード (xraylib) は専用描画へ
        {
            DrawFqSqCurves();
            return;
        }

        var model = electron ? CurrentElectronModel() : default;
        var profiles = new List<Profile>(scatElements.Length);
        foreach (var el in scatElements)
        {
            var factor = GetFactor(el.Z, el.Sub, electron, model);
            if (factor == null) continue;
            var pts = new List<PointD>(scatPoints + 1);
            for (int i = 0; i <= scatPoints; i++)
            {
                double sAng = scatSMaxAng * i / scatPoints;          // s = sinθ/λ [Å⁻¹]
                double s2 = sAng * sAng * 100.0;                     // [nm⁻²] (1 Å⁻¹ = 10 nm⁻¹)
                double y = factor(s2) * (electron ? 1.0 : 10.0);     // X線は電子単位へ ×10、電子は nm そのまま
                if (checkBoxDebyeWaller.Checked) y *= Math.Exp(-el.Biso * s2); // Debye-Waller exp(-B·s²)
                pts.Add(new PointD(sAng, y));
            }
            profiles.Add(new Profile(pts) { Color = el.Color });
        }

        graphControlScatteringFactor.GraphTitle = "";
        graphControlScatteringFactor.LabelX = "s = sinθ/λ (Å⁻¹)";
        graphControlScatteringFactor.LabelY = electron ? "fe (nm)" : "f (electrons)";
        if (profiles.Count > 0)
            graphControlScatteringFactor.AddProfiles([.. profiles]);
        else
            graphControlScatteringFactor.ClearProfile();
    }

    /// <summary>X線 F(q)+S(q) モードの曲線を描く (xraylib)。260606Cl 追加。
    /// F(q)=Rayleigh コヒーレント形状因子 (太線, q→0 で Z→高 q で下降)、S(q)=Compton 非弾性散乱関数 (淡色, 0→Z で上昇)。
    /// 横軸 q=sinθ/λ [Å⁻¹] は f(s) と同一量。Debye-Waller は弾性 F(q) のみに掛ける。</summary>
    private void DrawFqSqCurves()
    {
        if (!Xraylib.Enabled)
        {
            graphControlScatteringFactor.ClearProfile();
            graphControlScatteringFactor.GraphTitle = "F(q)+S(q): xraylib unavailable";
            return;
        }

        var profiles = new List<Profile>(scatElements.Length * 2);
        foreach (var el in scatElements)
        {
            var fPts = new List<PointD>(scatPoints + 1);
            var sPts = new List<PointD>(scatPoints + 1);
            for (int i = 0; i <= scatPoints; i++)
            {
                double q = scatSMaxAng * i / scatPoints; // q = sinθ/λ [Å⁻¹] (xraylib の q と同一)
                double f = Xraylib.FormFactorRayl(el.Z, q);
                double s = Xraylib.IncoherentSF(el.Z, q);
                if (checkBoxDebyeWaller.Checked && !double.IsNaN(f))
                    f *= Math.Exp(-el.Biso * q * q * 100.0); // Debye-Waller (弾性のみ。s²[nm⁻²]=q²·100)
                if (!double.IsNaN(f)) fPts.Add(new PointD(q, f));
                if (!double.IsNaN(s)) sPts.Add(new PointD(q, s));
            }
            if (fPts.Count > 0) profiles.Add(new Profile(fPts) { Color = el.Color, LineWidth = 1.6f, text = el.Name + " F(q)" });
            if (sPts.Count > 0) profiles.Add(new Profile(sPts) { Color = Pale(el.Color), LineWidth = 1f, text = el.Name + " S(q)" });
        }

        graphControlScatteringFactor.GraphTitle = "F(q) bold (coherent) / S(q) pale (incoherent)";
        graphControlScatteringFactor.LabelX = "s = sinθ/λ (Å⁻¹)";
        graphControlScatteringFactor.LabelY = "F(q), S(q) (electrons)";
        if (profiles.Count > 0)
            graphControlScatteringFactor.AddProfiles([.. profiles]);
        else
            graphControlScatteringFactor.ClearProfile();
    }

    /// <summary>色を白へ 50% ブレンドして淡くする (S(q) を F(q) と同系色で区別するため)。260606Cl 追加。</summary>
    private static Color Pale(Color c) => Color.FromArgb((c.R + 255) / 2, (c.G + 255) / 2, (c.B + 255) / 2);

    /// <summary>ビームに応じて MiniTable の列を構成する (構成が変わったときのみ再生成)。</summary>
    private void SetupScatColumns(bool electron)
    {
        if (scatColsReady && scatColsElectron == electron) return;
        if (electron)
            miniTable1.SetColumns(
                new MiniTable.Col("Element", Fill: true),
                new MiniTable.Col("Z", DataGridViewContentAlignment.MiddleRight, "0"),
                new MiniTable.Col("fe(s) nm", DataGridViewContentAlignment.MiddleRight, "g4"),
                new MiniTable.Col("model"));
        else
            // 260606Cl: X線は f(s) に加え、現在ビームエネルギーでの異常分散 f′(E)/f″(E) (xraylib) を併記。
            // f′/f″ は E と Z のみに依存し s(カーソル)では変わらない。xraylib 無効時はセル空欄 (N/A)。
            miniTable1.SetColumns(
                new MiniTable.Col("Element", Fill: true),
                new MiniTable.Col("Z", DataGridViewContentAlignment.MiddleRight, "0"),
                new MiniTable.Col("f(s)", DataGridViewContentAlignment.MiddleRight, "g4"),
                new MiniTable.Col("f'(E)", DataGridViewContentAlignment.MiddleRight, "g3"),
                new MiniTable.Col("f''(E)", DataGridViewContentAlignment.MiddleRight, "g3"));
        scatColsElectron = electron;
        scatColsReady = true;
    }

    /// <summary>カーソル位置 s での各元素 f(s) を MiniTable に書き出す。</summary>
    private void UpdateScatteringTable(double sAng)
    {
        if (!scatColsReady) return;
        bool electron = waveLengthControl1.WaveSource == WaveSource.Electron;
        var model = electron ? CurrentElectronModel() : default;
        var modelName = model == ElectronModel.EightGaussian ? "8-Gauss" : model.ToString();
        double s2 = sAng * sAng * 100.0;
        // X線時のみ: 現在ビームの光子エネルギー [keV]。異常分散 f′(E)/f″(E) (xraylib) に使う (s 非依存)。260606Cl
        double energyKeV = electron ? double.NaN : waveLengthControl1.Energy;

        var rows = new List<object[]>(scatElements.Length);
        foreach (var el in scatElements)
        {
            var factor = GetFactor(el.Z, el.Sub, electron, model);
            double f = double.NaN;
            if (factor != null)
            {
                f = factor(s2) * (electron ? 1.0 : 10.0);
                if (checkBoxDebyeWaller.Checked) f *= Math.Exp(-el.Biso * s2);
            }
            object fCell = double.IsNaN(f) ? null : f;
            if (electron)
                rows.Add([el.Name, el.Z, fCell, modelName]);
            else
            {
                // f′ = Fi、慣用 f″ = −Fii (Xraylib 内で符号反転済)。利用不可は NaN → 空欄。260606Cl
                double fp = Xraylib.Fprime(el.Z, energyKeV), fpp = Xraylib.Fdoubleprime(el.Z, energyKeV);
                rows.Add([el.Name, el.Z, fCell, double.IsNaN(fp) ? null : fp, double.IsNaN(fpp) ? null : fpp]);
            }
        }
        miniTable1.SetRows(rows);
    }

    /// <summary>原子散乱因子 Factor(s²[nm⁻²]) を返す (なければ null)。ES 型名に依存しないよう var で受ける。</summary>
    private static Func<double, double> GetFactor(int z, int sub, bool electron, ElectronModel model)
    {
        try
        {
            if (!electron)
            {
                var a = AtomStatic.XrayScatteringWK;
                return z < a.Length && a[z] != null && sub < a[z].Length ? a[z][sub]?.Factor : null;
            }
            switch (model)
            {
                case ElectronModel.Kirkland:
                    var k = AtomStatic.ElectronScatteringKirkrand;
                    return z < k.Length ? k[z]?.Factor : null;
                case ElectronModel.EightGaussian:
                    var g = AtomStatic.ElectronScatteringEightGaussian;
                    return z < g.Length ? g[z]?.Factor : null;
                default:
                    var p = AtomStatic.ElectronScatteringPeng;
                    return z < p.Length && p[z] != null && sub < p[z].Length ? p[z][sub]?.Factor : null;
            }
        }
        catch { return null; }
    }

    #endregion
}
