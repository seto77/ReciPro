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
        InitializeAttenuationTab();       // 260606Cl
        InitializeFluorescenceTab();      // 260606Cl
    }

    private void FormCrystallographicInformation_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        Visible = false;
    }

    #endregion 

    #region 共通: Crystal / 波長変更の追従 (両タブを更新)
    /// <summary>Scattering / Attenuation / Fluorescence の各タブを更新 (各メソッドが選択タブ以外を自己 return)。260606Cl 追加。</summary>
    private void UpdateAllTabs() { UpdateScatteringFactors(); UpdateAttenuation(); UpdateFluorescence(); }
    // CrystalControl で Crystal が変更されたとき
    private void crystalControl_CrystalChanged(object sender, EventArgs e)
    {
        numericBoxCutoffD.Minimum = (Crystal.A + Crystal.B + Crystal.C) / 20;
        attenMc = null;//260606Cl 結晶変更で電子輸送 MonteCarlo を無効化(組成変化で ElasticComponents が古くなるため)
        if (Visible)
        {
            SetSortedPlanes();
            UpdateAllTabs(); // 260606Cl
        }
    }
    // 表示時に再計算
    private void FormScatteringFactor_VisibleChanged(object sender, EventArgs e)
    {
        if (Visible)
        {
            SetSortedPlanes();
            UpdateAllTabs(); // 260606Cl
        }
    }
    // 上部帯の波長 / ビーム変更 → 両タブを再計算
    private void waveLengthControl1_WavelengthChanged(object sender, EventArgs e)
    {
        SetSortedPlanes();
        UpdateAllTabs(); // 260606Cl
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

        c.SetVectorOfG(cutoffD, waveSource, xrayEnergyKeV: waveLengthControl1.Energy);//260606Cl X線異常分散(f'/f'')を反射表へ反映 (X線時のみ有効, 他ビームでは無視)

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
                    var f = Crystal.GetStructureFactor(waveSource, c.Atoms, (h, k, l), 1 / d / d / 4.0, xrayEnergyKeV: waveLengthControl1.Energy);//260606Cl 異常分散を本表(SetSortedPlanes)と整合させる(X線時のみ有効, 既定ON)

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
    //260606Cl X線異常分散 f'/f''((Z,energy)依存=カーソル s 非依存)を scatElements と同順で事前計算したキャッシュ。電子線時は空。カーソルドラッグ毎の native 呼びを避ける。
    private (double fp, double fpp)[] scatXrayDisp = [];
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
        miniTable1.AllowVerticalScroll = true; // 260606Cl 元素別散乱因子表 (元素数×行) は縦スクロール許可 → 多元素結晶でクリップしない
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
        if (tabControl1.SelectedTab != tabPageScatteringFactors) return; // 260606Cl 追加: 非表示タブは計算しない (Attenuation/Fluorescence と同様。タブ切替時に SelectedIndexChanged で更新)

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
        //260606Cl f'/f''((Z,energy)依存・s 非依存)を1回だけ事前計算しキャッシュ。UpdateScatteringTable はこれを使い、カーソルドラッグ毎の native 呼びを回避する。
        scatXrayDisp = electron
            ? []
            : [.. scatElements.Select(el => (Xraylib.Fprime(el.Z, waveLengthControl1.Energy), Xraylib.Fdoubleprime(el.Z, waveLengthControl1.Energy)))];

        //260606Cl 縦線(カーソル)を AddProfiles より前に設定し、AddProfiles 内部の Draw() で曲線・縦線・交点マーカーを一括描画する(旧: AddProfiles 後に縦線設定→明示 Draw() で二重描画していた)。
        graphControlScatteringFactor.VerticalLines = [new PointD(scatCursorS, double.NaN)];
        DrawScatteringCurves(electron, fqsq);

        SetupScatColumns(electron);
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

        var rows = new List<object[]>(scatElements.Length);
        for (int i = 0; i < scatElements.Length; i++)
        {
            var el = scatElements[i];
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
                //260606Cl s 非依存の f'/f'' は UpdateScatteringFactors が事前計算した scatXrayDisp を参照(ドラッグ毎の native 呼びを回避)。旧: double fp = Xraylib.Fprime(el.Z, energyKeV), fpp = Xraylib.Fdoubleprime(el.Z, energyKeV);
                var (fp, fpp) = i < scatXrayDisp.Length ? scatXrayDisp[i] : (double.NaN, double.NaN);
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
                    var k = AtomStatic.ElectronScatteringKirkland;//260606Cl 綴り修正 Kirkrand→Kirkland
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

    #region Attenuations & Transport タブ (X線減衰・屈折率 / 電子輸送 / 中性子断面積) 260606Cl 追加
    // GUI コントロールは Designer.cs 定義。線種(ビーム)別に列ヘッダが異なる元素別表は dgvAttenEdges/Electron/Neutron を
    // FlowLayoutPanel(flowAttenDetail)に置き Visible で切替 (ヘッダをデザイナ固定列にして resx 翻訳可能にするため)。
    // スカラ表(Quantity/Value)はヘッダ非表示 + コード SetColumns。

    private const double ClassicalElectronRadiusCm = 2.8179403262e-13; // r_e [cm]
    private MonteCarlo attenMc;                       // 電子輸送用 (物性タプルでキャッシュ)
    private (double z, double a, double rho, double nv)? attenMcKey;//260606Cl nv(質量重み平均価電子数)もキャッシュキーに含める

    /// <summary>構成元素を (Z, 占有数合計) で集約 (Occ 込み)。260606Cl 追加。</summary>
    private (int z, double occ)[] AggregateElements()
    {
        var d = new Dictionary<int, double>();
        foreach (var a in Crystal.Atoms)
        {
            int z = a.AtomicNumber;
            if (z < 1 || z > 99) continue;
            d[z] = d.TryGetValue(z, out var v) ? v + a.Occ : a.Occ;
        }
        return d.Select(kv => (kv.Key, kv.Value)).ToArray();
    }

    /// <summary>値+単位を整形 (NaN/Inf は "N/A")。260606Cl 追加。</summary>
    private static string Q(double v, string unit, string fmt = "g4")
        => double.IsNaN(v) || double.IsInfinity(v) ? "N/A" : $"{v.ToString(fmt)} {unit}".TrimEnd();

    /// <summary>長さ [cm] を桁に応じ自動スケールして整形。260606Cl 追加。</summary>
    private static string QLen(double cm)
    {
        if (double.IsNaN(cm) || double.IsInfinity(cm)) return "N/A";
        double a = Math.Abs(cm);
        if (a < 1e-4) return $"{cm * 1e7:g4} nm";
        if (a < 1e-1) return $"{cm * 1e4:g4} µm";
        if (a < 1e2) return $"{cm * 1e1:g4} mm";
        return $"{cm * 1e-2:g4} m";
    }

    /// <summary>デザイナ定義列の表示属性 (整列/書式/AutoSize/非ソート) をコードで設定 (ヘッダ文字は resx)。260606Cl 追加。</summary>
    private static void ConfigCol(DataGridViewColumn c, DataGridViewContentAlignment align, string fmt = null, bool fill = false)
    {
        c.DefaultCellStyle.Alignment = align;
        if (fmt != null) c.DefaultCellStyle.Format = fmt;
        c.AutoSizeMode = fill ? DataGridViewAutoSizeColumnMode.Fill : DataGridViewAutoSizeColumnMode.AllCells;
        c.SortMode = DataGridViewColumnSortMode.NotSortable;
    }

    /// <summary>Attenuation タブのイベント配線と列設定 (Load から)。260606Cl 追加。</summary>
    private void InitializeAttenuationTab()
    {
        numAttenThickness.ValueChanged += (_, _) => UpdateAttenuation();
        tabControl1.SelectedIndexChanged += (_, _) => UpdateAllTabs(); // 260606Cl 散乱タブも含めタブ切替で更新 (各メソッドが選択タブ以外を自己 return)

        // スカラ表: ヘッダ非表示 (Quantity/Value は自明) + コード列 + 縦スクロール許可
        foreach (var t in new[] { dgvAttenScalars, dgvFluorScalars })
        {
            t.ColumnHeadersVisible = false;
            t.AllowVerticalScroll = true;
            t.SetColumns(new MiniTable.Col("Quantity", Fill: true), new MiniTable.Col("Value", DataGridViewContentAlignment.MiddleRight));
        }

        // デザイナ定義列の整列/書式 (ヘッダ翻訳は resx)
        var R = DataGridViewContentAlignment.MiddleRight;
        var C = DataGridViewContentAlignment.MiddleCenter;
        ConfigCol(colEdgeElem, default, fill: true); ConfigCol(colEdgeZ, R, "0"); ConfigCol(colEdgeEdge, C); ConfigCol(colEdgeKeV, R, "g4"); ConfigCol(colEdgeJump, R, "g3");
        ConfigCol(colElecElem, default, fill: true); ConfigCol(colElecZ, R, "0"); ConfigCol(colElecAt, R, "g3"); ConfigCol(colElecA, R, "g4");
        ConfigCol(colNeutElem, default, fill: true); ConfigCol(colNeutBcoh, R, "g4"); ConfigCol(colNeutScoh, R, "g4"); ConfigCol(colNeutAt, R, "g3");

        // 260606Cl 行数の多い元素別表 (元素数×行。Edges は最大 2×元素数) は縦スクロール許可 → 多元素結晶でもクリップしない
        foreach (var t in new[] { dgvAttenEdges, dgvAttenElectron, dgvAttenNeutron })
            t.AllowVerticalScroll = true;
    }

    /// <summary>ビーム種に応じて Attenuation タブを更新する。260606Cl 追加。</summary>
    private void UpdateAttenuation()
    {
        if (!IsHandleCreated || !Visible || Crystal == null || graphAtten == null) return;
        if (tabControl1.SelectedTab != tabPageAttenuations) return; // 非表示タブは計算しない

        var src = waveLengthControl1.WaveSource;
        dgvAttenEdges.Visible = src == WaveSource.Xray;
        dgvAttenElectron.Visible = src == WaveSource.Electron;
        dgvAttenNeutron.Visible = src == WaveSource.Neutron;
        numAttenThickness.Visible = src == WaveSource.Xray;

        var els = AggregateElements();
        double totalOcc = els.Sum(x => x.occ);
        if (els.Length == 0 || totalOcc <= 0 || !(Crystal.Density > 0)) // 退化入力ガード
        {
            graphAtten.VerticalLines = [];
            graphAtten.ClearProfile();
            dgvAttenScalars.ClearRows(); dgvAttenEdges.ClearRows(); dgvAttenElectron.ClearRows(); dgvAttenNeutron.ClearRows();
            return;
        }

        switch (src)
        {
            case WaveSource.Xray: UpdateAttenuationXray(els); break;
            case WaveSource.Electron: UpdateAttenuationElectron(els, totalOcc); break;
            case WaveSource.Neutron: UpdateAttenuationNeutron(els, totalOcc); break;
            default:
                graphAtten.VerticalLines = [];
                graphAtten.ClearProfile();
                dgvAttenScalars.ClearRows();
                break;
        }
    }

    // ---- X線: 質量減衰 + 屈折率 + 吸収端 ----
    private void UpdateAttenuationXray((int z, double occ)[] els)
    {
        double e = waveLengthControl1.Energy;          // 光子エネルギー [keV]
        double rho = Crystal.Density;                  // [g/cm³]
        double lambdaCm = waveLengthControl1.WaveLength * 1e-7; // nm → cm
        double totalMass = els.Sum(x => x.occ * AtomStatic.AtomicWeight(x.z));
        bool xrl = Xraylib.Enabled;

        double muRhoTot = 0, muEnRho = 0;
        foreach (var (z, occ) in els)
        {
            double w = occ * AtomStatic.AtomicWeight(z) / totalMass;
            if (xrl) { muRhoTot += w * Xraylib.MassAttenuationTotal(z, e); muEnRho += w * Xraylib.MassEnergyAbsorption(z, e); }
            else muRhoTot += w * AtomStatic.MassAbsorption(e, z); // 光電のみ (fallback)
        }
        double muLin = muRhoTot * rho;                 // [1/cm]
        double tCm = numAttenThickness.Value * 1e-4;   // µm → cm

        // 屈折率 δ, β, θc, SLD。f'/f'' は xraylib 必須・一部 NaN なら吸収側不明 → N/A。
        bool dispNa = !xrl;
        double sumReal = 0, sumImag = 0;
        foreach (var (z, occ) in els)
        {
            double n = rho * UniversalConstants.A * occ / totalMass; // atoms/cm³
            double fp = xrl ? Xraylib.Fprime(z, e) : 0, fpp = xrl ? Xraylib.Fdoubleprime(z, e) : 0;
            if (xrl && (double.IsNaN(fp) || double.IsNaN(fpp))) dispNa = true;
            sumReal += n * (z + (double.IsNaN(fp) ? 0 : fp));
            sumImag += n * (double.IsNaN(fpp) ? 0 : fpp);
        }
        double pref = ClassicalElectronRadiusCm * lambdaCm * lambdaCm / (2 * Math.PI);
        double delta = pref * sumReal, beta = pref * sumImag;
        double thetaC = delta > 0 ? Math.Sqrt(2 * delta) : double.NaN; // [rad]
        double sldReal = ClassicalElectronRadiusCm * sumReal * 1e-16;  // cm⁻² → Å⁻²

        dgvAttenScalars.SetRows(
        [
            ["µ/ρ (total)", Q(muRhoTot, "cm²/g")],
            ["µ (linear)", Q(muLin, "cm⁻¹")],
            ["Attenuation length", QLen(1 / muLin)],
            ["HVL", QLen(Math.Log(2) / muLin)],
            [$"Transmission (t={numAttenThickness.Value:g4} µm)", Q(Math.Exp(-muLin * tCm) * 100, "%", "g4")],
            ["µ_en/ρ", xrl ? Q(muEnRho, "cm²/g") : "N/A"],
            ["δ (1−n)", dispNa ? "N/A" : Q(delta, "", "g4")],
            ["β", dispNa ? "N/A" : Q(beta, "", "g4")],
            ["θc (critical)", dispNa ? "N/A" : Q(thetaC * 1e3, "mrad")],
            ["X-ray SLD (Re)", dispNa ? "N/A" : Q(sldReal * 1e6, "10⁻⁶Å⁻²", "g4")],
            ["source", xrl ? "xraylib" : "internal (photo)"],
        ]);

        // 吸収端 (K/L3): xrl 有効時はエネルギーも xraylib から (ジャンプ比と整合)
        var edges = new[] { (Xraylib.XrlShell.K, XrayLineEdge.K, "K"), (Xraylib.XrlShell.L3, XrayLineEdge.L3, "L3") };
        var rows = new List<object[]>();
        foreach (var (z, _) in els.OrderBy(x => x.z))
            foreach (var (shell, edge, name) in edges)
            {
                double ee = xrl ? Xraylib.EdgeEnergyKeV(z, shell) : AtomStatic.CharacteristicXrayEnergy(z, edge);
                if (double.IsNaN(ee) || ee <= 0) continue;
                double jf = xrl ? Xraylib.EdgeJumpFactor(z, shell) : AtomStatic.AbsorptionJumpRatio(z, edge);//260606Cl xraylib 無効時は内蔵 FFAST テーブルからジャンプ比をフォールバック(未収載は NaN→空欄)
                rows.Add([AtomStatic.AtomicName(z), z, name, ee, double.IsNaN(jf) ? (object)null : jf]);
            }
        dgvAttenEdges.SetRows(rows);

        DrawXrayAttenuationGraph(els, totalMass, e);
    }

    private void DrawXrayAttenuationGraph((int z, double occ)[] els, double totalMass, double currentE)
    {
        const double eMin = 1, eMax = 60;
        const int n = 300;
        bool xrl = Xraylib.Enabled;
        var pTot = new List<PointD>(n + 1);
        var pPho = new List<PointD>(n + 1);
        var pRay = new List<PointD>(n + 1);
        var pCom = new List<PointD>(n + 1);
        for (int i = 0; i <= n; i++)
        {
            double e = eMin * Math.Pow(eMax / eMin, (double)i / n); // log 等間隔
            double t = 0, p = 0, r = 0, c = 0;
            foreach (var (z, occ) in els)
            {
                double w = occ * AtomStatic.AtomicWeight(z) / totalMass;
                if (xrl)
                {
                    t += w * Xraylib.MassAttenuationTotal(z, e);
                    p += w * Xraylib.MassAttenuationPhoto(z, e);
                    r += w * Xraylib.MassAttenuationRayleigh(z, e);
                    c += w * Xraylib.MassAttenuationCompton(z, e);
                }
                else
                    t += w * AtomStatic.MassAbsorption(e, z);
            }
            if (t > 0) pTot.Add(new PointD(e, t));
            if (xrl)
            {
                if (p > 0) pPho.Add(new PointD(e, p));
                if (r > 0) pRay.Add(new PointD(e, r));
                if (c > 0) pCom.Add(new PointD(e, c));
            }
        }
        graphAtten.YLog = true;
        graphAtten.LabelX = "E (keV)";
        graphAtten.LabelY = "µ/ρ (cm²/g)";
        graphAtten.GraphTitle = xrl ? "µ/ρ total (bold) / photo / Rayleigh / Compton" : "µ/ρ photoabsorption (internal)";
        var profiles = new List<Profile> { new(pTot) { Color = Color.Black, LineWidth = 1.8f, text = "total" } };
        if (pPho.Count > 0) profiles.Add(new Profile(pPho) { Color = Color.FromArgb(0xd6, 0x27, 0x28), text = "photo" });
        if (pRay.Count > 0) profiles.Add(new Profile(pRay) { Color = Color.FromArgb(0x2c, 0xa0, 0x2c), text = "Rayleigh" });
        if (pCom.Count > 0) profiles.Add(new Profile(pCom) { Color = Color.FromArgb(0x1f, 0x77, 0xb4), text = "Compton" });

        //260606Cl 縦線(現在エネルギー + 各元素の吸収端)を AddProfiles より前に設定し、AddProfiles 内部の Draw() で一括描画(旧: AddProfiles 後に設定→明示 Draw() で二重描画していた)。
        var vlines = new List<PointD> { new(currentE, double.NaN) };
        foreach (var (z, _) in els)
            foreach (var edge in new[] { XrayLineEdge.K, XrayLineEdge.L3 })
            {
                double ee = AtomStatic.CharacteristicXrayEnergy(z, edge);
                if (!double.IsNaN(ee) && ee > eMin && ee < eMax) vlines.Add(new PointD(ee, double.NaN));
            }
        graphAtten.VerticalLines = [.. vlines];
        graphAtten.AddProfiles([.. profiles]);
    }

    // ---- 電子: 弾性断面積 / MFP / dE·ds / IMFP / 飛程 ----
    private void UpdateAttenuationElectron((int z, double occ)[] els, double totalOcc)
    {
        double kev = waveLengthControl1.Energy;
        double avgZ = els.Sum(x => x.occ * x.z) / totalOcc;
        double avgA = els.Sum(x => x.occ * AtomStatic.AtomicWeight(x.z)) / totalOcc;
        double rho = Crystal.Density;

        double nv = MonteCarlo.EstimateAverageValenceElectronCount(els.Select(x => (x.z, x.occ * AtomStatic.AtomicWeight(x.z))));//260606Cl 質量重み平均価電子数 Nv(plasma E/IMFP の TPP-2M 精度向上。旧: avgZ 単一推定)
        var mc = GetAttenMonteCarlo(avgZ, avgA, rho, nv);
        var (_, sigma, mfp, dEds) = mc.GetParameters(kev);
        double imfpA = mc.GetInelasticMeanFreePathAngstrom(kev * 1000); // keV → eV
        double lambdaNm = UniversalConstants.Convert.EnergyToElectronWaveLength(kev);
        double rangeKO = 0.0276 * avgA * Math.Pow(kev, 1.67) / (Math.Pow(avgZ, 0.89) * rho); // Kanaya-Okayama [µm]

        dgvAttenScalars.SetRows(
        [
            ["λ (electron)", Q(lambdaNm * 1000, "pm")],
            ["σ elastic", Q(sigma, "nm²")],
            ["Elastic MFP", Q(mfp, "nm")],
            ["dE/ds (loss)", Q(dEds, "keV/nm")],
            ["IMFP", Q(imfpA / 10, "nm")],
            ["Plasma E", mc.PlasmaEnergyEv > 0 ? Q(mc.PlasmaEnergyEv, "eV") : "N/A"],
            ["J (mean exc.)", mc.J > 0 ? Q(mc.J, "eV") : "N/A"],
            ["Range (Kanaya-Okayama)", Q(rangeKO, "µm")],
            ["Range (CSDA path)", Q(mc.GetCsdaRangeMicron(kev), "µm")],//260606Cl 追加: 阻止能を ∫dE/|dE/ds| 積分した経路長。KO(後方散乱込みの侵入深さ近似)とは別物
            ["mean Z, A", $"{avgZ:g4}, {avgA:g4}"],
        ]);

        dgvAttenElectron.SetRows(els.OrderBy(x => x.z)
            .Select(x => new object[] { AtomStatic.AtomicName(x.z), x.z, x.occ / totalOcc * 100, AtomStatic.AtomicWeight(x.z) }).ToList());

        DrawElectronTransportGraph(mc, kev);
    }

    /// <summary>物性タプル (avgZ,avgA,ρ) でキャッシュした MonteCarlo (固定 30keV 構築・混合系 atoms 渡し)。260606Cl 追加。</summary>
    private MonteCarlo GetAttenMonteCarlo(double avgZ, double avgA, double rho, double valenceElectronCount)//260606Cl valenceElectronCount 追加(質量重み Nv で plasma/IMFP 精度向上)
    {
        var key = (avgZ, avgA, rho, valenceElectronCount);
        if (attenMc == null || attenMcKey != key)
        {
            attenMc = new MonteCarlo(avgZ, avgA, rho, 30, 0, valenceElectronCount: valenceElectronCount, atoms: Crystal.Atoms);//260606Cl 旧: valenceElectronCount 未指定(=avgZ 単一推定)。質量重み Nv を渡す
            attenMcKey = key;
        }
        return attenMc;
    }

    private void DrawElectronTransportGraph(MonteCarlo mc, double currentKev)
    {
        const double kMin = 1, kMax = 30;
        const int n = 200;
        var sig = new List<PointD>(n + 1);
        var mfp = new List<PointD>(n + 1);
        var des = new List<PointD>(n + 1);
        double sMax = 0, mMax = 0, dMax = 0;
        var raw = new (double k, double s, double m, double d)[n + 1];
        for (int i = 0; i <= n; i++)
        {
            double k = kMin + (kMax - kMin) * i / n;
            var (_, s, m, d) = mc.GetParameters(k);
            raw[i] = (k, s, m, d);
            sMax = Math.Max(sMax, s); mMax = Math.Max(mMax, m); dMax = Math.Max(dMax, d);
        }
        foreach (var (k, s, m, d) in raw) // 3 量はスケール差大 → 各最大で正規化して重ね描き (絶対値は表)
        {
            if (sMax > 0) sig.Add(new PointD(k, s / sMax));
            if (mMax > 0) mfp.Add(new PointD(k, m / mMax));
            if (dMax > 0) des.Add(new PointD(k, d / dMax));
        }
        graphAtten.YLog = false;
        graphAtten.LabelX = "E (keV)";
        graphAtten.LabelY = "normalized";
        graphAtten.GraphTitle = "σ / MFP / dE·ds vs E (each normalized; absolute in table)";
        graphAtten.VerticalLines = [new PointD(currentKev, double.NaN)];//260606Cl 縦線を AddProfiles より前に設定し内部 Draw() で一括描画(旧: AddProfiles 後に設定→明示 Draw() で二重描画)
        graphAtten.AddProfiles(
        [
            new Profile(sig) { Color = Color.FromArgb(0xd6, 0x27, 0x28), text = "σ elastic" },
            new Profile(mfp) { Color = Color.FromArgb(0x1f, 0x77, 0xb4), text = "elastic MFP" },
            new Profile(des) { Color = Color.FromArgb(0x2c, 0xa0, 0x2c), text = "dE/ds" },
        ]);
    }

    // ---- 中性子: 散乱長 / 断面積 (グラフ無) ----
    private void UpdateAttenuationNeutron((int z, double occ)[] els, double totalOcc)
    {
        double totalMass = els.Sum(x => x.occ * AtomStatic.AtomicWeight(x.z));
        double rho = Crystal.Density;

        double lambdaAng = waveLengthControl1.WaveLength * 10.0; // nm → Å (σ_abs の 1/v 評価用)。260606Cl 追加

        double bMean = 0, sld = 0;
        double sCoh = 0, sInc = 0, sAbs = 0; // 260606Cl 原子分率加重の平均断面積 [barn/atom] (Σ x_i σ_i)
        bool anyResonant = false;            // 260606Cl 1/v 破れの共鳴吸収核(Cd/Sm/Eu/Gd)を含むか
        foreach (var (z, occ) in els)
        {
            double x = occ / totalOcc;
            double bRe = NeutronB(z);
            if (!double.IsNaN(bRe))
            {
                bMean += x * bRe;
                double nA = rho * UniversalConstants.A * occ / totalMass * 1e-24; // atoms/Å³
                sld += nA * bRe * 1e-5; // b[fm]→Å ; SLD[Å⁻²]
            }
            sCoh += x * AtomStatic.NeutronCoherentCrossSection(z);          // 260606Cl
            sInc += x * AtomStatic.NeutronIncoherentCrossSection(z);        // 260606Cl
            sAbs += x * AtomStatic.NeutronAbsorptionCrossSection(z, lambdaAng); // 260606Cl
            if (AtomStatic.NeutronIsResonantAbsorber(z)) anyResonant = true;
        }
        double sTot = sCoh + sInc + sAbs; // σ_tot/atom [barn] (欠落元素を含むと NaN 伝播=§11)。260606Cl
        // 260606Cl マクロ断面積 Σ [cm⁻¹] = N_atoms·σ̄·1e-24, N_atoms[atoms/cm³] = ρ·N_A/Ā (Ā=平均原子量 g/mol)。透過の removal 近似 (Bragg/前方/小角/多重散乱は無視)。
        double aBar = totalMass / totalOcc;
        double nAtomsCm3 = rho > 0 ? rho * UniversalConstants.A / aBar : double.NaN;
        double sigmaTotMacro = nAtomsCm3 * sTot * 1e-24; // cm⁻¹
        double attenLenCm = sigmaTotMacro > 0 ? 1.0 / sigmaTotMacro : double.NaN;

        dgvAttenScalars.SetRows(
        [
            ["b̄ (coherent)", Q(bMean, "fm")],
            ["Coherent SLD", Q(sld * 1e6, "10⁻⁶Å⁻²", "g4")],
            ["σ̄_coh", Q(sCoh, "barn")],                                   // 260606Cl 4π|b|² (複素対応)
            ["σ̄_incoh", Q(sInc, "barn")],                                 // 260606Cl periodictable nsf
            [$"σ̄_abs (1/v, {lambdaAng:g3}Å)" + (anyResonant ? " *" : ""), Q(sAbs, "barn")], // 260606Cl 虚部から導出
            ["σ̄_total", Q(sTot, "barn")],                                 // 260606Cl
            ["Σ_total (macro)", Q(sigmaTotMacro, "cm⁻¹")],                 // 260606Cl
            ["Atten. length (1/Σ)", QLen(attenLenCm)],                     // 260606Cl
            ["source", anyResonant ? "periodictable nsf (* 1/v invalid: Cd/Sm/Eu/Gd)" : "periodictable nsf (Sears 1992)"],
        ]);

        dgvAttenNeutron.SetRows(els.OrderBy(x => x.z).Select(x =>
        {
            double b = NeutronB(x.z);
            object bCell = double.IsNaN(b) ? null : b;
            double sc = AtomStatic.NeutronCoherentCrossSection(x.z); // 260606Cl tabulated σ_coh (旧 4π·b.Real²/100 は共鳴核 Gd/Eu で誤り)
            object sCell = double.IsNaN(sc) ? null : sc;
            return new object[] { AtomStatic.AtomicName(x.z), bCell, sCell, x.occ / totalOcc * 100 };
        }).ToList());

        graphAtten.VerticalLines = [];
        graphAtten.GraphTitle = "Neutron: no energy-dependent graph";
        graphAtten.ClearProfile();
    }

    /// <summary>元素 z の束縛コヒーレント散乱長 b の実部 [fm] (自然存在比, [z][0])。無ければ NaN。260606Cl 追加。</summary>
    private static double NeutronB(int z)
    {
        var t = AtomStatic.NeutronCoherentScattering;
        if (z < 1 || z >= t.Length || t[z] == null || t[z].Length == 0) return double.NaN;
        return t[z][0].Real;
    }
    #endregion

    #region Fluorescence タブ (特性X線・蛍光収率・EDX スティック, X線専用) 260606Cl 追加
    // 表示する特性線: (内蔵エネルギー用 XrayLine, xraylib 線マクロ, その線が空孔を作る殻, 表示名)。codex: Kα/Kβ→K, Lα→L3, Lβ1→L2。
    private static readonly (XrayLine eng, Xraylib.XrlLine xrl, Xraylib.XrlShell shell, string name)[] FluorLines =
    [
        (XrayLine.Ka1, Xraylib.XrlLine.Ka1, Xraylib.XrlShell.K, "Kα1"),
        (XrayLine.Ka2, Xraylib.XrlLine.Ka2, Xraylib.XrlShell.K, "Kα2"),
        (XrayLine.Kb1, Xraylib.XrlLine.Kb1, Xraylib.XrlShell.K, "Kβ1"),
        (XrayLine.La1, Xraylib.XrlLine.La1, Xraylib.XrlShell.L3, "Lα1"),
        (XrayLine.La2, Xraylib.XrlLine.La2, Xraylib.XrlShell.L3, "Lα2"),
        (XrayLine.Lb1, Xraylib.XrlLine.Lb1, Xraylib.XrlShell.L2, "Lβ1"),
    ];

    /// <summary>Fluorescence タブ初期化 (Load から)。列設定は InitializeAttenuationTab に集約。260606Cl 追加。</summary>
    private void InitializeFluorescenceTab()
    {
        var R = DataGridViewContentAlignment.MiddleRight;
        ConfigCol(colFlElem, default, fill: true);
        ConfigCol(colFlLine, DataGridViewContentAlignment.MiddleCenter);
        ConfigCol(colFlE, R, "g4"); ConfigCol(colFlRelI, R, "g3"); ConfigCol(colFlOmega, R, "g3");
        dgvFluorLines.AllowVerticalScroll = true; // 260606Cl 特性線表 (発光線×元素数=行数多) は縦スクロール許可
    }

    /// <summary>Fluorescence タブ更新。X線時のみ内容を出し、電子/中性子では無効メッセージ。260606Cl 追加。</summary>
    private void UpdateFluorescence()
    {
        if (!IsHandleCreated || !Visible || Crystal == null || graphFluor == null) return;
        if (tabControl1.SelectedTab != tabPageFluorescence) return;

        if (waveLengthControl1.WaveSource != WaveSource.Xray)
        {
            tlpFluor.Visible = false;
            labelFluorNA.Visible = true;
            return;
        }
        tlpFluor.Visible = true;
        labelFluorNA.Visible = false;

        bool xrl = Xraylib.Enabled;
        var els = AggregateElements();
        double totalOcc = els.Sum(x => x.occ);
        if (els.Length == 0 || totalOcc <= 0)
        {
            graphFluor.VerticalLines = [];
            graphFluor.ClearProfile();
            dgvFluorScalars.ClearRows(); dgvFluorLines.ClearRows();
            return;
        }

        // 特性線テーブル + EDX スティック (強度 ∝ 原子分率 x · RadRate · ω。励起断面積/検出効率は無視: 定性表示)
        var lineRows = new List<object[]>();
        var sticks = new List<(double e, double h, int z)>();
        double hMax = 0;
        string strongest = "—";
        double strongestI = 0;
        var colorOf = new Dictionary<int, Color>();
        int ci = 0;
        foreach (var (z, occ) in els.OrderBy(x => x.z))
        {
            colorOf[z] = scatPalette[ci++ % scatPalette.Length];
            double x = occ / totalOcc;
            foreach (var (eng, xline, shell, name) in FluorLines)
            {
                double ekeV = xrl ? Xraylib.LineEnergyKeV(z, xline) : AtomStatic.CharacteristicXrayEnergy(z, eng);//260606Cl xrl 有効時は線エネルギーも xraylib(RadRate/端と出所統一)
                if (double.IsNaN(ekeV) || ekeV <= 0) continue;
                double rate = xrl ? Xraylib.LineRadRate(z, xline) : double.NaN;
                double yield = xrl ? Xraylib.FluorescenceYield(z, shell) : double.NaN;
                lineRows.Add([AtomStatic.AtomicName(z), name, ekeV,
                    double.IsNaN(rate) ? (object)null : rate * 100,
                    double.IsNaN(yield) ? (object)null : yield]);
                if (!double.IsNaN(rate) && !double.IsNaN(yield))
                {
                    double inten = x * rate * yield;
                    sticks.Add((ekeV, inten, z));
                    hMax = Math.Max(hMax, inten);
                    if (inten > strongestI) { strongestI = inten; strongest = $"{AtomStatic.AtomicName(z)} {name} ({ekeV:g4} keV)"; }
                }
            }
        }
        dgvFluorLines.SetRows(lineRows);

        var scalarRows = new List<object[]>();
        foreach (var (z, _) in els.OrderBy(x => x.z))
        {
            double wk = xrl ? Xraylib.FluorescenceYield(z, Xraylib.XrlShell.K) : double.NaN;
            scalarRows.Add([$"ω_K ({AtomStatic.AtomicName(z)})", double.IsNaN(wk) ? "N/A" : wk.ToString("g3")]);
        }
        scalarRows.Add(["strongest line", strongest]);
        scalarRows.Add(["source", xrl ? "internal (E) / xraylib (I, ω)" : "internal (E only)"]);
        dgvFluorScalars.SetRows(scalarRows);

        // EDX スティック: 各線を (E,0)-(E,h) の 2 点プロファイルで描く (高さは最大で正規化)
        graphFluor.YLog = false;
        graphFluor.LabelX = "E (keV)";
        graphFluor.LabelY = "relative intensity";
        graphFluor.GraphTitle = xrl ? "EDX sticks (qualitative: x·RadRate·ω)" : "EDX: requires xraylib";
        //260606Cl 縦線(現在エネルギー)を AddProfiles/ClearProfile より前に設定し内部 Draw() で一括描画(旧: 後に設定→明示 Draw() で二重描画)
        graphFluor.VerticalLines = [new PointD(waveLengthControl1.Energy, double.NaN)];
        if (sticks.Count > 0 && hMax > 0)
        {
            var profiles = sticks.Select(s => new Profile(new List<PointD> { new(s.e, 0), new(s.e, s.h / hMax) })
            { Color = colorOf[s.z], LineWidth = 1.4f }).ToArray();
            graphFluor.AddProfiles(profiles);
        }
        else
            graphFluor.ClearProfile();
    }
    #endregion
}
