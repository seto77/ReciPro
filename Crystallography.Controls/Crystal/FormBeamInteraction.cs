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

public partial class FormBeamInteraction : FormBase
{
    #region フィールド・プロパティ
    public Crystal Crystal => CrystalControl.Crystal;
    public CrystalControl CrystalControl;

    /// <summary>長さの単位の get/set</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public LengthUnitEnum LengthUnit => waveLengthControl.LengthUnit;

    // 260425Cl WFO1000 対策: デザイナのシリアライゼーション対象から除外
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool MillerBravais { get => dataGridViewTextBoxColumnI.Visible; set => dataGridViewTextBoxColumnI.Visible = value; }
    #endregion

    #region 起動・終了
    public FormBeamInteraction()
    {
        InitializeComponent();
    }
    private void FormBeamInteraction_Load(object sender, EventArgs e)
    {
        // (260426Ch) 古い EventHandler 明示生成とコメント typo を整理
        // CrystalControl は外部から代入される参照 (このフォームの子コントロールでない) ためデザイナ登録不可 → コードで配線。260607Cl
        CrystalControl.CrystalChanged += crystalControl_CrystalChanged;
        typeof(DataGridView).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dataGridView, true, null);
        InitializeReflectionsTab();       // 260607Cl
        InitializeScatteringFactorsTab(); // 260606Cl
        InitializeAttenuationTab();       // 260606Cl
        InitializeFluorescenceTab();      // 260606Cl
        ApplyBeamDependentVisibility();   // 260607Ch 追加: 初期表示でも中性子/X線専用表示を反映
    }

    private void FormBeamInteraction_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        Visible = false;
    }

    #endregion

    #region --capture 用 (GuiCapture の線種別モード撮影) 260608Cl 追加
    /// <summary>--capture 用: 線源を設定して全タブを再計算する (GuiCapture.CaptureBeamInteractionModeShots から呼ぶ)。
    /// 線源切替で ApplyBeamDependentVisibility が走り、蛍光タブの増減 (X線のみ) も反映される。
    /// 既定の Cu Kα (8 keV) は電子線では MonteCarlo の低エネルギー端で退化し輸送グラフが NaN になるため、
    /// マニュアル用に線種ごとの代表値へ設定する (電子線=20 keV / X線=Cu Kα / 中性子=熱中性子 ~1.8 Å)。260608Cl 追加。</summary>
    public void SetCaptureBeam(WaveSource source)
    {
        waveLengthControl.WaveSource = source;
        if (source == WaveSource.Electron)
            waveLengthControl.Energy = 20.0;            // 代表的な電子線エネルギー (MC の 1–30 keV 範囲内・輸送量が健全)
        else if (source == WaveSource.Xray)
            waveLengthControl.Energy = 8.04114721;      // Cu Kα 相当 (既定)
        else // Neutron: グラフ無し。σ_abs(1/v) 用に熱中性子の波長 (nm) を据える (電子線の短波長を引き継がせない)
            waveLengthControl.WaveLength = 0.18;
        SetSortedPlanes();
        UpdateAllTabs();
    }

    /// <summary>--capture 用: 線種×タブのクロップを撮るために TabControl を公開する。260608Cl 追加。</summary>
    public TabControl CaptureTabControl => tabControl;
    #endregion

    #region 共通: Crystal / 波長変更の追従 (両タブを更新)
    /// <summary>Scattering / Attenuation / Fluorescence の各タブを更新 (各メソッドが選択タブ以外を自己 return)。260606Cl 追加。</summary>
    private void UpdateAllTabs() { ApplyBeamDependentVisibility(); UpdateScatteringFactors(); UpdateAttenuation(); UpdateFluorescence(); }//260607Ch 変更: ビーム種依存の表示切替を先に適用 (旧: UpdateScatteringFactors(); UpdateAttenuation(); UpdateFluorescence();)

    /// <summary>ビーム種に応じて、グラフ領域や X線専用タブの表示を切り替える。260607Ch 追加。</summary>
    private void ApplyBeamDependentVisibility()
    {
        var neutron = waveLengthControl.WaveSource == WaveSource.Neutron;
        //260607Ch 中性子ではエネルギー依存グラフを出さず、元素別表だけを見せる。
        graphControlAtten.Visible = !neutron;
        graphControlScatteringFactor.Visible = !neutron;
        flowLayoutPanelAttenuationModel.Visible = !neutron;
        flowLayoutPanelScatteringFactorModel.Visible = !neutron;

        //260607Ch Fluorescence は X線専用。TabPage.Visible ではなく TabPages から外してタブヘッダ自体を消す。
        var xray = waveLengthControl.WaveSource == WaveSource.Xray;
        if (xray)
        {
            //260608Cl タブ順崩れ修正: Fluorescence は canonical 順(Reflections/ScatteringFactors/Attenuations/Fluorescence)の末尾。
            //          TabControl.TabPages.Insert はタブ順を壊す既知不具合があるため使わず、末尾 Add で復帰させる
            //          (他3タブは削除しないので Add=正位置)。
            if (!tabControl.TabPages.Contains(tabPageFluorescence))
                tabControl.TabPages.Add(tabPageFluorescence);
            //{ // 260607Ch 旧: Insert は再表示時にタブ順が崩れる
            //    var index = tabControl.TabPages.IndexOf(tabPageAttenuations);
            //    tabControl.TabPages.Insert(index >= 0 ? index + 1 : tabControl.TabPages.Count, tabPageFluorescence);
            //}
        }
        else if (tabControl.TabPages.Contains(tabPageFluorescence))
        {
            if (tabControl.SelectedTab == tabPageFluorescence)
                tabControl.SelectedTab = tabPageReflections;
            tabControl.TabPages.Remove(tabPageFluorescence);
        }
    }

    /// <summary>UIカルチャ(ja)に応じてグラフ見出し・ツールチップ・コンボ項目の日本語/英語を返す。260607Cl 追加。</summary>
    private static string Loc(string en, string ja) => System.Globalization.CultureInfo.CurrentUICulture.Name == "ja" ? ja : en;
    // 260608Cl 変更: グラフ見出し文字列を resx から「コード内テーブル」へ移行。
    // 理由: これらは FormBeamInteraction.resx に手書きで追加していたが、VS デザイナでフォームを
    //       編集すると resx 再シリアライズ時に巻き込まれて消える事故が起きたため (260607Ch の resx 化を撤回)。
    //       日本語訳は既存の Loc(en, ja) で出し分ける。EN のみのキーはリテラルを返す。
    private string R(string name) => name switch
    {
        // --- 軸ラベル(AxisLabel*) ---
        "Graph.Axis.D.Angstrom" => "d (Å)",
        "Graph.Axis.D.Nm" => "d (nm)",
        "Graph.Axis.DedsKevNm" => "dE/ds (keV/nm)",
        "Graph.Axis.EKeV" => "E (keV)",
        "Graph.Axis.ElasticMfpNm" => Loc("Elastic MFP (nm)", "弾性MFP (nm)"),
        "Graph.Axis.ElectronFeNm" => "fe (nm)",
        "Graph.Axis.FqSqElectrons" => Loc("F(q), S(q) (electrons)", "F(q), S(q) (電子数)"),
        "Graph.Axis.ImfpNm" => Loc("IMFP (nm)", "非弾性MFP (nm)"),
        "Graph.Axis.MuCmInv" => "µ (cm⁻¹)",
        "Graph.Axis.MuRhoCm2G" => "µ/ρ (cm²/g)",
        "Graph.Axis.Normalized" => Loc("Normalized", "正規化"),
        "Graph.Axis.Q.AngstromInv" => "Q = 4π·sinθ/λ (Å⁻¹)",
        "Graph.Axis.Q.NmInv" => "Q = 4π·sinθ/λ (nm⁻¹)",
        "Graph.Axis.RangeCsdaMicron" => Loc("Range CSDA (µm)", "飛程 CSDA (µm)"),
        "Graph.Axis.RelativeIntensity" => Loc("Relative intensity", "相対強度"),
        "Graph.Axis.S.AngstromInv" => "s = sinθ/λ (Å⁻¹)",
        "Graph.Axis.SigmaElasticNm2" => Loc("σ elastic (nm²)", "σ 弾性 (nm²)"),
        "Graph.Axis.TransmissionPercent" => Loc("Transmission (%)", "透過率 (%)"),
        "Graph.Axis.TwoTheta" => "2θ (°)",
        "Graph.Axis.XrayFElectrons" => Loc("f (electrons)", "f (電子数)"),
        // --- 凡例ラベル(Label*) ---
        "Graph.Label.D" => "d:",
        "Graph.Label.Deds" => "dE/ds:",
        "Graph.Label.E" => "E:",
        "Graph.Label.F" => "f:",
        "Graph.Label.Fe" => "fe:",
        "Graph.Label.FqSq" => "F,S:",
        "Graph.Label.I" => "I:",
        "Graph.Label.Imfp" => "IMFP:",
        "Graph.Label.Intensity" => "I:",
        "Graph.Label.Mfp" => "MFP:",
        "Graph.Label.Mu" => "µ:",
        "Graph.Label.MuRho" => "µ/ρ:",
        "Graph.Label.Normalized" => Loc("norm:", "正規化:"),
        "Graph.Label.Q" => "Q:",
        "Graph.Label.Range" => Loc("Range:", "飛程:"),
        "Graph.Label.S" => "s:",
        "Graph.Label.Sigma" => "σ:",
        "Graph.Label.Transmission" => "T:",
        "Graph.Label.TwoTheta" => "2θ:",
        // --- グラフタイトル(GraphTitle) ---
        "Graph.Title.EdxRequiresXraylib" => Loc("EDX requires xraylib", "EDX には xraylib が必要です"),
        "Graph.Title.EdxSticks" => Loc("EDX emission lines", "EDX 発光線"),
        "Graph.Title.ElectronTransportNormalized" => Loc("Electron transport (normalized: σ, elastic MFP, dE/ds)", "電子輸送 (正規化: σ・弾性MFP・dE/ds)"),
        "Graph.Title.FqSq" => Loc("F(q) coherent and S(q) incoherent scattering", "F(q) 干渉性・S(q) 非干渉性散乱"),
        "Graph.Title.FqSqXraylibUnavailable" => Loc("F(q)+S(q) requires xraylib", "F(q)+S(q) には xraylib が必要です"),
        "Graph.Title.MuPhotoInternal" => Loc("Linear attenuation coefficient µ (photoabsorption only)", "線吸収係数 µ (光電吸収のみ)"),
        "Graph.Title.MuRhoPhotoInternal" => Loc("Mass attenuation coefficient µ/ρ (photoabsorption only)", "質量吸収係数 µ/ρ (光電吸収のみ)"),
        "Graph.Title.MuRhoTotal" => Loc("Mass attenuation coefficient µ/ρ (total)", "質量吸収係数 µ/ρ (全)"),
        "Graph.Title.MuTotal" => Loc("Linear attenuation coefficient µ (total)", "線吸収係数 µ (全)"),
        "Graph.Title.NeutronNoEnergyGraph" => Loc("Neutron cross sections are listed in the table (no energy graph)", "中性子断面積は表に表示します (エネルギーグラフなし)"),
        "Graph.Title.NeutronScatteringNoGraph" => Loc("Neutron scattering length b does not depend on s (no curve)", "中性子散乱長 b は s に依存しません (曲線なし)"),
        "Graph.Title.QuantityVsE" => Loc("{0} vs electron energy", "{0} 対 電子エネルギー"),
        "Graph.Title.TransmissionThroughThickness" => Loc("Transmission for thickness t = {0}", "厚さ t = {0} の透過率"),
        // --- 単位(Unit*) ---
        "Graph.Unit.Angstrom" => " Å",
        "Graph.Unit.AngstromInv" => " Å⁻¹",
        "Graph.Unit.Cm2G" => " cm²/g",
        "Graph.Unit.CmInv" => " cm⁻¹",
        "Graph.Unit.Degree" => " °",
        "Graph.Unit.Electrons" => Loc(" electrons", " 電子数"),
        "Graph.Unit.KeV" => " keV",
        "Graph.Unit.KeVPerNm" => " keV/nm",
        "Graph.Unit.Micron" => " µm",
        "Graph.Unit.Nm" => " nm",
        "Graph.Unit.Nm2" => " nm²",
        "Graph.Unit.NmInv" => " nm⁻¹",
        "Graph.Unit.Percent" => " %",
        _ => "",
    };

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
    private void FormBeamInteraction_VisibleChanged(object sender, EventArgs e)
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
        var waveLength = waveLengthControl.WaveLength;
        var waveSource = waveLengthControl.WaveSource;
        var cutoffD = LengthUnit == LengthUnitEnum.NanoMeter ? numericBoxCutoffD.Value : numericBoxCutoffD.Value / 10; // (260426Ch) 1 回だけの CutoffD helper をローカル化

        c.SetVectorOfG(cutoffD, waveSource, xrayEnergyKeV: waveLengthControl.Energy);//260606Cl X線異常分散(f'/f'')を反射表へ反映 (X線時のみ有効, 他ビームでは無視)

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
            DrawReflectionsGraph([]);//260607Cl 反射なし → グラフも空に
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

        var peaks = new List<(double twoTheta, double inten, int h, int k, int l)>();//260607Cl 回折ピークグラフ用 (反射表と同じフィルタ後データ)
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
            peaks.Add((twoTheta, g.RelativeIntensity, g.Index.h, g.Index.k, g.Index.l));//260607Cl
        }
        bindingSourceScatteringFactor.DataMember = dataMember;
        DrawReflectionsGraph(peaks);//260607Cl 反射表と同じデータで graphControlReflections に回折ピーク(2θスティック+hkl)を描画
    }

    //260607Cl X軸/対数トグル変更時に再計算せず再描画するため、直近の反射リストを保持
    private List<(double twoTheta, double inten, int h, int k, int l)> lastReflPeaks;
    private bool lastReflShowLabels = true;

    /// <summary>反射リスト (2θ, 相対強度, hkl) から graphControlReflections に回折ピーク(スティック)と強度上位の hkl ラベルを描く。
    /// X軸は comboBoxReflXAxis (2θ / d / Q)、強度軸は checkBoxReflLog で線形/対数を切替。
    /// showLabels=false でラベルを付けない (テスト掃引は分数指数があり得るため)。260607Cl 追加。</summary>
    private void DrawReflectionsGraph(List<(double twoTheta, double inten, int h, int k, int l)> peaks, bool showLabels = true)
    {
        lastReflPeaks = peaks; lastReflShowLabels = showLabels;//260607Cl 軸/対数トグル時の再描画用に保持

        int axisMode = comboBoxReflXAxis?.SelectedIndex ?? 0;// 0:2θ 1:d 2:Q  260607Cl
        bool logI = checkBoxReflLog?.Checked ?? false;       //260607Cl 強度を対数表示
        bool ang = LengthUnit == LengthUnitEnum.Angstrom;
        double lambda = waveLengthControl.WaveLength;       // nm

        graphControlReflections.GraphTitle = "";
        switch (axisMode)//260607Cl X軸ラベル/単位をモードで切替
        {
            case 1:
                //260607Ch resx 化 (旧: graphControlReflections.AxisLabelX = ang ? "d (Å)" : "d (nm)")
                graphControlReflections.AxisLabelX = R(ang ? "Graph.Axis.D.Angstrom" : "Graph.Axis.D.Nm");
                graphControlReflections.LabelX = R("Graph.Label.D"); graphControlReflections.UnitX = R(ang ? "Graph.Unit.Angstrom" : "Graph.Unit.Nm"); break;
            case 2:
                //260607Ch resx 化 (旧: graphControlReflections.AxisLabelX = ang ? "Q = 4π·sinθ/λ (Å⁻¹)" : "Q = 4π·sinθ/λ (nm⁻¹)")
                graphControlReflections.AxisLabelX = R(ang ? "Graph.Axis.Q.AngstromInv" : "Graph.Axis.Q.NmInv");
                graphControlReflections.LabelX = R("Graph.Label.Q"); graphControlReflections.UnitX = R(ang ? "Graph.Unit.AngstromInv" : "Graph.Unit.NmInv"); break;
            default:
                //260607Ch resx 化 (旧: graphControlReflections.AxisLabelX = "2θ (°)")
                graphControlReflections.AxisLabelX = R("Graph.Axis.TwoTheta");
                graphControlReflections.LabelX = R("Graph.Label.TwoTheta"); graphControlReflections.UnitX = R("Graph.Unit.Degree"); break;
        }
        graphControlReflections.AxisLabelY = R("Graph.Axis.RelativeIntensity");//260607Ch resx 化
        graphControlReflections.LabelY = R("Graph.Label.I");
        graphControlReflections.UnitY = "";
        graphControlReflections.YLog = logI;//260607Cl 対数強度
        graphControlReflections.FixLowerXToZero = axisMode != 1;//260607Cl 2θ/Q は0始点、d は実データ範囲

        // 回折する反射のみ (2θ が有限。d < λ/2 は NaN→PositiveInfinity で除外)
        var valid = peaks.Where(p => !double.IsNaN(p.twoTheta) && !double.IsInfinity(p.twoTheta) && p.inten > 0).ToList();
        if (valid.Count == 0)
        {
            graphControlReflections.Annotations = [];
            graphControlReflections.ClearProfile();
            return;
        }
        double maxI = valid.Max(p => p.inten);
        if (!(maxI > 0)) maxI = 1;
        double baseY = logI ? 1e-4 : 0;//260607Cl 対数では0始点(=log−∞)が使えないので微小フロアから立てる(正規化強度0..1に対し1e-4)

        // 2θ[deg] → 選択中X軸の値 (260607Cl)
        double X(double twoTheta)
        {
            if (axisMode == 0) return twoTheta;
            double sinT = Math.Sin(twoTheta / 2 * Math.PI / 180);
            if (axisMode == 1) { double dNm = lambda / (2 * sinT); return ang ? dNm * 10 : dNm; }   // d [nm]→[Å]
            double qNm = 4 * Math.PI * sinT / lambda; return ang ? qNm / 10 : qNm;                   // Q [nm⁻¹]→[Å⁻¹]
        }

        // hkl ラベルは強度上位のみ (スティックが密でも読めるように)。AddProfiles 前に設定し内部 Draw() で一括描画。
        graphControlReflections.Annotations = showLabels
            ? [.. valid.OrderByDescending(p => p.inten).Take(scatMaxPeakLabels)
                .Select(p => new GraphControl.GraphAnnotation(X(p.twoTheta), double.NaN, HklLabel(p.h, p.k, p.l), Color.FromArgb(0x55, 0x55, 0x55), Vertical: true, GuideLine: false))]
            : [];

        // 各反射を (X,baseY)-(X,I) の縦スティックで描く (相対強度に正規化。対数時は下端をフロアに)
        graphControlReflections.AddProfiles([.. valid.Select(p =>
            new Profile(new List<PointD> { new(X(p.twoTheta), baseY), new(X(p.twoTheta), Math.Max(p.inten / maxI, baseY)) }) { Color = Color.FromArgb(0x1f, 0x77, 0xb4), LineWidth = 1f })]);
    }

    /// <summary>Reflections タブの X軸/対数トグル変更時に直近データで再描画する (再計算なし)。260607Cl 追加。</summary>
    private void reflectionsView_OptionChanged(object sender, EventArgs e)
    {
        if (lastReflPeaks != null) DrawReflectionsGraph(lastReflPeaks, lastReflShowLabels);
    }

    /// <summary>Reflections タブの X軸切替コンボ・対数チェックの初期化と配線 (Load から)。260607Cl 追加。</summary>
    private void InitializeReflectionsTab()
    {
        comboBoxReflXAxis.Items.Clear();
        comboBoxReflXAxis.Items.AddRange(["2θ", "d", "Q"]);
        comboBoxReflXAxis.SelectedIndex = 0;// 既定 2θ (旧来の表示)。260607Cl イベントはデザイナ登録 (Load 時発火も lastReflPeaks==null で無害)
        toolTip.SetToolTip(comboBoxReflXAxis, Loc(
            "Choose the horizontal axis of the diffraction-peak plot. The three options describe the SAME set of reflections; only the horizontal scale changes.\n" +
            "  • 2θ — the scattering angle in degrees, i.e. how far the beam is bent. This is what a diffractometer reads directly.\n" +
            "  • d — the spacing between the crystal lattice planes that produce the reflection (large d = low angle = planes far apart).\n" +
            "  • Q = 4π·sinθ/λ — the 'scattering vector' (momentum transfer). Common in synchrotron and pair-distribution studies; large Q = high angle.",
            "回折ピークの横軸を選びます。3つの選択肢は同じ反射群を表し、横軸の取り方だけが変わります。\n" +
            "  • 2θ — 散乱角(度)。ビームがどれだけ曲がるか。回折計が直接読む値です。\n" +
            "  • d — その反射を作る結晶格子面の間隔(d が大きい=低角=面の間隔が広い)。\n" +
            "  • Q = 4π·sinθ/λ — 「散乱ベクトル」(運動量遷移)。放射光や二体分布関数でよく使う。Q が大きい=高角。"));
        toolTip.SetToolTip(checkBoxReflLog, Loc(
            "Switch the vertical (intensity) axis between a linear scale and a logarithmic scale (…, ×0.01, ×0.1, ×1).\n" +
            "Diffraction intensities span many orders of magnitude: a few peaks are very strong and most are weak. On a linear scale the weak peaks are flattened against the baseline; a logarithmic scale stretches the bottom so you can still see them.",
            "縦軸(強度)を線形と対数(…, ×0.01, ×0.1, ×1)で切り替えます。\n" +
            "回折強度は桁が大きく異なり、強いピークは少数で大半は弱いものです。線形では弱いピークが底に潰れて見えませんが、対数にすると下側が引き伸ばされ、弱いピークも確認できます。"));
    }

    #region テストコード (手動 h,k,l 掃引)
    /// <summary>テストコード</summary>
    private void numericBoxH_min_ValueChanged(object sender, EventArgs e)
    {
        var c = (Crystal)Crystal.Clone();
        var waveLength = waveLengthControl.WaveLength;
        var waveSource = waveLengthControl.WaveSource;

        // 一旦 bindingSource を解除
        var dataMember = bindingSourceScatteringFactor.DataMember;
        dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        bindingSourceScatteringFactor.DataMember = "";

        dataSet.DataTableScatteringFactor.Clear();

        var peaks = new List<(double twoTheta, double inten, int h, int k, int l)>();//260607Cl 回折ピークグラフ用 (掃引は分数指数があり得るため hkl ラベルは付けない)
        for (double h = numericBoxH_min.Value; h <= numericBoxH_max.Value + 1e-10; h += numericBoxH_step.Value)
            for (double k = numericBoxK_min.Value; k <= numericBoxK_max.Value + 1e-10; k += numericBoxK_step.Value)
                for (double l = numericBoxL_min.Value; l <= numericBoxL_max.Value + 1e-10; l += numericBoxL_step.Value)
                {
                    var gLength = (h * c.A_Star + k * c.B_Star + l * c.C_Star).Length;
                    var d = 1 / gLength;
                    var twoTheta = 2 * Math.Asin(gLength * waveLength / 2) / Math.PI * 180;
                    var f = Crystal.GetStructureFactor(waveSource, c.Atoms, (h, k, l), 1 / d / d / 4.0, xrayEnergyKeV: waveLengthControl.Energy);//260606Cl 異常分散を本表(SetSortedPlanes)と整合させる(X線時のみ有効, 既定ON)

                    dataSet.DataTableScatteringFactor.Add(h, k, l, 1, d, twoTheta, f, f.MagnitudeSquared(), []);
                    peaks.Add((twoTheta, f.MagnitudeSquared(), 0, 0, 0));//260607Cl 強度=|F|² (指数は未使用)
                }
        dataGridView.DefaultCellStyle.Format = "g8";
        dataGridView.VirtualMode = true;
        bindingSourceScatteringFactor.DataMember = dataMember;
        DrawReflectionsGraph(peaks, showLabels: false);//260607Cl テスト掃引もグラフ更新 (ラベルなし)
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
    private const int scatMaxPeakLabels = 30; // 260607Cl 回折ピークオーバーレイで hkl ラベルを付ける本数 (強度上位)

    /// <summary>Scattering factors タブのイベント結線と初期化 (Load から)。Designer を触らずコード側で配線する。</summary>
    private void InitializeScatteringFactorsTab()
    {
        //260607Cl ラジオ/チェック/LinePositionChanged のイベントはデザイナ登録へ移動 (Form 全体でデザイナ登録に統一)
        if (!radioButtonXrayFs.Checked && !radioButtonXrayFqSq.Checked) radioButtonXrayFs.Checked = true;                  // 既定: f(s)
        if (!radioButtonElectronPeng.Checked && !radioButtonElectronKirkland.Checked && !radioButtonElectronEightGaussian.Checked) radioButtonElectronPeng.Checked = true; // 既定: Peng

        graphControlScatteringFactor.VerticalLineMarkerVisible = true; // 各曲線にカーソル交点マーカー

        //260607Cl 線種別 3 表の列はデザイナ定義 (ヘッダ翻訳は resx/.ja.resx)。ここでは整列/書式/AutoSize/非ソートだけコードで設定する。
        //          元素/モデル列は内容フィット (AllCells)、数値列は Fill で伸縮 (相対幅はデザイナの FillWeight で微調整可)。
        var R = DataGridViewContentAlignment.MiddleRight;
        ConfigCol(colSfxElem, default); ConfigCol(colSfxZ, R, "0", true); ConfigCol(colSfxFs, R, "g4", true); ConfigCol(colSfxFp, R, "g3", true); ConfigCol(colSfxFpp, R, "g3", true);
        ConfigCol(colSfeElem, default); ConfigCol(colSfeZ, R, "0", true); ConfigCol(colSfeFe, R, "g4", true); ConfigCol(colSfeModel, default);
        ConfigCol(colSfnElem, default); ConfigCol(colSfnBcoh, R, "g4", true); ConfigCol(colSfnScoh, R, "g4", true); ConfigCol(colSfnSinc, R, "g4", true);

        // 元素数×行で増減する表は縦スクロール許可 → 多元素結晶でクリップしない
        foreach (var t in new[] { miniTableScatteringFactorsXray, miniTableScatteringFactorsElectron, miniTableScatteringFactorsNeutron })
            t.AllowVerticalScroll = true;
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
        if (tabControl.SelectedTab != tabPageScatteringFactors) return; // 260606Cl 追加: 非表示タブは計算しない (Attenuation/Fluorescence と同様。タブ切替時に SelectedIndexChanged で更新)

        var src = waveLengthControl.WaveSource;

        // モード切替ラジオ (flowLayoutPanel) をビームで表示/非表示
        flowLayoutPanelModel_Xray.Visible = src == WaveSource.Xray;
        flowLayoutPanelModel_Electron.Visible = src == WaveSource.Electron;

        //260607Cl 線種別 MiniTable は選択中の線種だけ Visible (他は非表示)
        miniTableScatteringFactorsXray.Visible = src == WaveSource.Xray;
        miniTableScatteringFactorsElectron.Visible = src == WaveSource.Electron;
        miniTableScatteringFactorsNeutron.Visible = src == WaveSource.Neutron;

        bool electron = src == WaveSource.Electron;
        bool neutron = src == WaveSource.Neutron;

        //260607Cl 中性子: 散乱長は s 非依存 → 曲線は描かず「no s dependence」メッセージ。表は b/σ を出す。
        if (neutron)
        {
            scatXrayDisp = [];
            DrawNeutronScatteringInfo();
            UpdateNeutronScatteringTable();
            return;
        }

        scatElements = BuildScatElements(electron);//260607Cl 中性子経路では未使用なので早期 return の後で構築
        bool fqsq = !electron && radioButtonXrayFqSq.Checked; // F(q)+S(q) は xraylib 待ち (P4)

        //260606Cl f'/f''((Z,energy)依存・s 非依存)を1回だけ事前計算しキャッシュ。UpdateScatteringTable はこれを使い、カーソルドラッグ毎の native 呼びを回避する。
        scatXrayDisp = electron
            ? []
            : [.. scatElements.Select(el => (Xraylib.Fprime(el.Z, waveLengthControl.Energy), Xraylib.Fdoubleprime(el.Z, waveLengthControl.Energy)))];

        //260606Cl 縦線(カーソル)を AddProfiles より前に設定し、AddProfiles 内部の Draw() で曲線・縦線・交点マーカーを一括描画する(旧: AddProfiles 後に縦線設定→明示 Draw() で二重描画していた)。
        graphControlScatteringFactor.VerticalLines = [new PointD(scatCursorS, double.NaN)];
        DrawScatteringCurves(electron, fqsq);

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
        //260607Cl AxisLabel=軸上の完全表示(単位込), Label=量名, Unit=単位(先頭スペース込)に分離
        //260607Ch resx 化 (旧: 軸/単位文字列をコード内リテラルで設定)
        graphControlScatteringFactor.AxisLabelX = R("Graph.Axis.S.AngstromInv");
        graphControlScatteringFactor.LabelX = R("Graph.Label.S");
        graphControlScatteringFactor.UnitX = R("Graph.Unit.AngstromInv");
        graphControlScatteringFactor.AxisLabelY = R(electron ? "Graph.Axis.ElectronFeNm" : "Graph.Axis.XrayFElectrons");
        graphControlScatteringFactor.LabelY = R(electron ? "Graph.Label.Fe" : "Graph.Label.F");
        graphControlScatteringFactor.UnitY = R(electron ? "Graph.Unit.Nm" : "Graph.Unit.Electrons");
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
            graphControlScatteringFactor.GraphTitle = R("Graph.Title.FqSqXraylibUnavailable");//260607Ch resx 化
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

        graphControlScatteringFactor.GraphTitle = R("Graph.Title.FqSq");//260607Ch resx 化
        graphControlScatteringFactor.AxisLabelX = R("Graph.Axis.S.AngstromInv");//260607Ch AxisLabel/Label/Unit 分離値を resx 化
        graphControlScatteringFactor.LabelX = R("Graph.Label.S");
        graphControlScatteringFactor.UnitX = R("Graph.Unit.AngstromInv");
        graphControlScatteringFactor.AxisLabelY = R("Graph.Axis.FqSqElectrons");
        graphControlScatteringFactor.LabelY = R("Graph.Label.FqSq");
        graphControlScatteringFactor.UnitY = R("Graph.Unit.Electrons");
        if (profiles.Count > 0)
            graphControlScatteringFactor.AddProfiles([.. profiles]);
        else
            graphControlScatteringFactor.ClearProfile();
    }

    /// <summary>色を白へ 50% ブレンドして淡くする (S(q) を F(q) と同系色で区別するため)。260606Cl 追加。</summary>
    private static Color Pale(Color c) => Color.FromArgb((c.R + 255) / 2, (c.G + 255) / 2, (c.B + 255) / 2);

    /// <summary>カーソル位置 s での各元素 f(s) を線種別 MiniTable に書き出す (X線→Xray表 / 電子→Electron表)。260607Cl</summary>
    private void UpdateScatteringTable(double sAng)
    {
        var src = waveLengthControl.WaveSource;
        if (src != WaveSource.Xray && src != WaveSource.Electron) return;//260607Cl 中性子は UpdateNeutronScatteringTable が担当
        bool electron = src == WaveSource.Electron;
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
                //260606Cl s 非依存の f'/f'' は UpdateScatteringFactors が事前計算した scatXrayDisp を参照(ドラッグ毎の native 呼びを回避)。
                var (fp, fpp) = i < scatXrayDisp.Length ? scatXrayDisp[i] : (double.NaN, double.NaN);
                rows.Add([el.Name, el.Z, fCell, double.IsNaN(fp) ? null : fp, double.IsNaN(fpp) ? null : fpp]);
            }
        }
        (electron ? miniTableScatteringFactorsElectron : miniTableScatteringFactorsXray).SetRows(rows);
    }

    /// <summary>中性子: 元素別の散乱長 b と断面積 (s 非依存) を MiniTable に書き出す。260607Cl 追加。</summary>
    private void UpdateNeutronScatteringTable()
    {
        //260607Cl Z 重複排除は Attenuation 中性子表と同じ AggregateElements() を共用 (旧: 独自 HashSet ループ)
        miniTableScatteringFactorsNeutron.SetRows(AggregateElements().OrderBy(x => x.z).Select(x =>
        {
            double b = NeutronB(x.z), sc = AtomStatic.NeutronCoherentCrossSection(x.z), si = AtomStatic.NeutronIncoherentCrossSection(x.z);
            return new object[] { AtomStatic.AtomicName(x.z),
                double.IsNaN(b) ? null : (object)b,
                double.IsNaN(sc) ? null : (object)sc,
                double.IsNaN(si) ? null : (object)si };
        }).ToList());
    }

    /// <summary>中性子: 散乱長は s/角度/エネルギーに依存しないため曲線は描かずメッセージのみ。260607Cl 追加。</summary>
    private void DrawNeutronScatteringInfo()
    {
        graphControlScatteringFactor.ClearProfile();
        graphControlScatteringFactor.GraphTitle = R("Graph.Title.NeutronScatteringNoGraph");//260607Ch resx 化
        graphControlScatteringFactor.AxisLabelX = graphControlScatteringFactor.AxisLabelY = "";//グラフ無し状態は軸ラベル/単位を空に
        graphControlScatteringFactor.LabelX = graphControlScatteringFactor.LabelY = "";
        graphControlScatteringFactor.UnitX = graphControlScatteringFactor.UnitY = "";
        graphControlScatteringFactor.VerticalLines = [];
    }

    /// <summary>hkl を簡潔表記する (負指数はオーバーライン)。260607Cl 追加。</summary>
    private static string HklLabel(int h, int k, int l)
    {
        static string D(int v) => v < 0 ? (-v).ToString() + "̅" : v.ToString();//負はオーバーライン(U+0305)
        return D(h) + D(k) + D(l);
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
    // GUI コントロールは Designer.cs 定義。線種(ビーム)別に列ヘッダが異なる元素別表は miniTableAttenEdges/Electron/Neutron を
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
        //260607Cl numericBoxAttenThickness.ValueChanged / tabControl.SelectedIndexChanged / 係数ラジオ CheckedChanged はデザイナ登録へ移動
        if (!radioButtonAttenMassMu.Checked && !radioButtonAttenLinMu.Checked && !radioButtonAttenTrans.Checked) radioButtonAttenMassMu.Checked = true;
        toolTip.SetToolTip(radioButtonAttenMassMu, Loc(
            "Plot the MASS attenuation coefficient µ/ρ (unit: cm²/g).\n" +
            "This measures how strongly the material removes X-rays from the beam (by absorption and scattering) per gram of material, so it does not depend on how densely the material is packed. It is the value listed in reference tables. Multiply it by the density ρ to obtain the linear coefficient µ.",
            "質量吸収係数 µ/ρ(単位: cm²/g)を表示します。\n" +
            "物質1グラムあたりに X線がどれだけ減衰(吸収+散乱)されるかを表し、物質の詰まり具合(密度)によりません。データ集に載るのはこの値です。密度 ρ を掛けると線吸収係数 µ になります。"));
        toolTip.SetToolTip(radioButtonAttenLinMu, Loc(
            "Plot the LINEAR attenuation coefficient µ = (µ/ρ)·ρ (unit: cm⁻¹).\n" +
            "This is the attenuation per centimetre of the actual material at its real density. The transmitted intensity follows I = I₀·exp(−µ·t), where t is the path length, and 1/µ is the distance over which the beam intensity falls to about 37% (1/e).",
            "線吸収係数 µ = (µ/ρ)·ρ(単位: cm⁻¹)を表示します。\n" +
            "実際の密度の物質を1cm通るごとの減衰量です。透過強度は I = I₀·exp(−µ·t)(t=通過距離)に従い、1/µ は強度が約37%(1/e)になる距離に相当します。"));
        toolTip.SetToolTip(radioButtonAttenTrans, Loc(
            "Plot the TRANSMISSION T = exp(−µ·t) in percent — the fraction of the X-ray beam that passes through the sample without being absorbed or scattered, for the sample thickness t set in the 'Thickness t' box below.\n" +
            "100% = the sample is transparent to the beam; 0% = the beam is fully blocked. Use this to judge a sensible sample thickness at the current energy.",
            "透過率 T = exp(−µ·t) を%で表示します。下の「Thickness t」で設定した試料厚 t に対し、吸収も散乱もされずに通り抜ける X線の割合です。\n" +
            "100%=試料はビームに透明、0%=ビームは完全に遮られる。現在のエネルギーで適切な試料厚を決める目安になります。"));

        //260607Cl 電子の表示量セレクタ: combo を 6 ラジオ (flowLayoutPanelElecQuantity) に変更。テキストは resx、既定は radioButtonElecAll (Designer で Checked)、イベントもデザイナ登録。各ラジオに個別ツールチップを付ける。
        toolTip.SetToolTip(radioButtonElecAll, Loc(
            "Overlay the three curves below, each rescaled to its own maximum so their SHAPES can be compared on one plot (read absolute values from the table).",
            "下の3曲線を各最大で正規化して重ね、形を1枚で比較できます(絶対値は表で確認)。"));
        toolTip.SetToolTip(radioButtonElecSigma, Loc(
            "σ elastic — elastic scattering cross section: how likely a single atom is to deflect the electron (bigger = scatters more).",
            "σ 弾性 — 弾性散乱断面積: 原子1個が電子を曲げる起こりやすさ(大きいほどよく散乱)。"));
        toolTip.SetToolTip(radioButtonElecEMFP, Loc(
            "Elastic MFP — mean free path: the average distance the electron travels between elastic scattering events.",
            "弾性MFP — 平均自由行程: 弾性散乱が起きる平均間隔の距離。"));
        toolTip.SetToolTip(radioButtonElecDeds, Loc(
            "dE/ds — stopping power: the energy the electron loses per nanometre of travel.",
            "dE/ds — 阻止能: 電子が1nm進むごとに失うエネルギー。"));
        toolTip.SetToolTip(radioButtonElecIMFP, Loc(
            "IMFP — inelastic mean free path: average distance between energy-losing collisions.",
            "IMFP — 非弾性平均自由行程: エネルギーを失う衝突の平均間隔。"));
        toolTip.SetToolTip(radioButtonElecRange, Loc(
            "Range (CSDA) — the total path length the electron travels before it stops.",
            "飛程(CSDA) — 電子が止まるまでに進む総経路長。"));

        // スカラ表: ヘッダ非表示 (Quantity/Value は自明) + コード列 + 縦スクロール許可
        foreach (var t in new[] { miniTableAttenScalars, miniTableFluorScalars })
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
        colElecA.ToolTipText = Loc("Elastic cross section σ_el (NIST Mott 50 eV–36 keV; screened Rutherford above 36 keV).", "弾性散乱断面積 σ_el (NIST Mott 50 eV–36 keV、36 keV 超は遮蔽 Rutherford 近似)");// 260608Cl Loc()化。260606Cl §6.5 元素別弾性断面積・NIST範囲外は Rutherford 近似
        ConfigCol(colNeutElem, default, fill: true); ConfigCol(colNeutBcoh, R, "g4"); ConfigCol(colNeutScoh, R, "g4"); ConfigCol(colNeutAt, R, "g3");

        // 260606Cl 行数の多い元素別表 (元素数×行。Edges は最大 2×元素数) は縦スクロール許可 → 多元素結晶でもクリップしない
        foreach (var t in new[] { miniTableAttenEdges, miniTableAttenElectron, miniTableAttenNeutron })
            t.AllowVerticalScroll = true;
    }

    /// <summary>ビーム種に応じて Attenuation タブを更新する。260606Cl 追加。</summary>
    private void UpdateAttenuation()
    {
        if (!IsHandleCreated || !Visible || Crystal == null || graphControlAtten == null) return;
        if (tabControl.SelectedTab != tabPageAttenuations) return; // 非表示タブは計算しない

        var src = waveLengthControl.WaveSource;
        miniTableAttenEdges.Visible = src == WaveSource.Xray;
        miniTableAttenElectron.Visible = src == WaveSource.Electron;
        miniTableAttenNeutron.Visible = src == WaveSource.Neutron;
        flowLayoutPanelAttenCoeff.Visible = src == WaveSource.Xray;//260607Cl 係数モードラジオは X線時のみ
        flowLayoutPanelElecQuantity.Visible = src == WaveSource.Electron;//260607Cl 電子量セレクタ(ラジオ群)は電子時のみ

        var els = AggregateElements();
        double totalOcc = els.Sum(x => x.occ);
        if (els.Length == 0 || totalOcc <= 0 || !(Crystal.Density > 0)) // 退化入力ガード
        {
            graphControlAtten.VerticalLines = [];
            graphControlAtten.ClearProfile();
            miniTableAttenScalars.ClearRows(); miniTableAttenEdges.ClearRows(); miniTableAttenElectron.ClearRows(); miniTableAttenNeutron.ClearRows();
            return;
        }

        switch (src)
        {
            case WaveSource.Xray: UpdateAttenuationXray(els); break;
            case WaveSource.Electron: UpdateAttenuationElectron(els, totalOcc); break;
            case WaveSource.Neutron: UpdateAttenuationNeutron(els, totalOcc); break;
            default:
                graphControlAtten.VerticalLines = [];
                graphControlAtten.ClearProfile();
                miniTableAttenScalars.ClearRows();
                break;
        }
    }

    // ---- X線: 質量減衰 + 屈折率 + 吸収端 ----
    private void UpdateAttenuationXray((int z, double occ)[] els)
    {
        double e = waveLengthControl.Energy;          // 光子エネルギー [keV]
        double rho = Crystal.Density;                  // [g/cm³]
        double lambdaCm = waveLengthControl.WaveLength * 1e-7; // nm → cm
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
        double tCm = numericBoxAttenThickness.Value * 1e-4;   // µm → cm

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

        miniTableAttenScalars.SetRows(  //260608Cl スカラ行ラベルを Loc() 化 (記号・固有名・確立略語 SLD/HVL は据置)
        [
            [Loc("µ/ρ (total)", "µ/ρ (全)"), Q(muRhoTot, "cm²/g")],
            [Loc("µ (linear)", "µ (線)"), Q(muLin, "cm⁻¹")],
            [Loc("Attenuation length", "減衰長"), QLen(1 / muLin)],
            ["HVL", QLen(Math.Log(2) / muLin)],
            [Loc("Transmission", "透過率") + $" (t={numericBoxAttenThickness.Value:g4} µm)", Q(Math.Exp(-muLin * tCm) * 100, "%", "g4")],
            ["µ_en/ρ", xrl ? Q(muEnRho, "cm²/g") : "N/A"],
            ["δ (1−n)", dispNa ? "N/A" : Q(delta, "", "g4")],
            ["β", dispNa ? "N/A" : Q(beta, "", "g4")],
            [Loc("θc (critical)", "θc (臨界角)"), dispNa ? "N/A" : Q(thetaC * 1e3, "mrad")],
            [Loc("X-ray SLD (Re)", "X線 SLD (実部)"), dispNa ? "N/A" : Q(sldReal * 1e6, "10⁻⁶Å⁻²", "g4")],
            [Loc("source", "出典"), xrl ? "xraylib" : "internal (photo)"],
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
        miniTableAttenEdges.SetRows(rows);

        DrawXrayAttenuationGraph(els, totalMass, e);
    }

    /// <summary>X線係数モードラジオの状態 (0:µ/ρ質量 1:µ線 2:透過率)。260607Cl 追加。</summary>
    private int AttenCoeffMode() => radioButtonAttenLinMu.Checked ? 1 : radioButtonAttenTrans.Checked ? 2 : 0;

    /// <summary>係数モードラジオの変更ハンドラ (解除側は無視)。260607Cl 追加。</summary>
    private void attenCoeff_OptionChanged(object sender, EventArgs e)
    {
        if (sender is RadioButton { Checked: false }) return;
        UpdateAttenuation();
    }

    //260607Cl ラムダをデザイナ登録可能な名前付きハンドラへ (Form 全体でイベントはデザイナ登録に統一)
    private void numericBoxAttenThickness_ValueChanged(object sender, EventArgs e) => UpdateAttenuation();
    private void elecQuantity_OptionChanged(object sender, EventArgs e)
    {
        if (sender is RadioButton { Checked: false }) return;
        UpdateAttenuation();
    }
    private void tabControl_SelectedIndexChanged(object sender, EventArgs e) => UpdateAllTabs(); // 散乱タブも含めタブ切替で更新 (各 Update は非選択タブを自己 return)

    private void DrawXrayAttenuationGraph((int z, double occ)[] els, double totalMass, double currentE)
    {
        const double eMin = 1, eMax = 60;
        const int n = 300;
        bool xrl = Xraylib.Enabled;
        int mode = AttenCoeffMode();                 //260607Cl 0:µ/ρ 1:µ 2:透過率
        double rho = Crystal.Density;                // [g/cm³]
        double tCm = numericBoxAttenThickness.Value * 1e-4; // µm → cm (透過率用)
        double scale = mode == 1 ? rho : 1.0;        // 線吸収係数 µ = (µ/ρ)·ρ
        var pTot = new List<PointD>(n + 1);
        var pPho = new List<PointD>(n + 1);
        var pRay = new List<PointD>(n + 1);
        var pCom = new List<PointD>(n + 1);
        var pTrans = new List<PointD>(n + 1);
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
            if (mode == 2)//260607Cl 透過率 T = exp(−µt)·100 (total のみ)
            {
                if (t > 0) pTrans.Add(new PointD(e, Math.Exp(-t * rho * tCm) * 100));
            }
            else
            {
                if (t > 0) pTot.Add(new PointD(e, t * scale));
                if (xrl)
                {
                    if (p > 0) pPho.Add(new PointD(e, p * scale));
                    if (r > 0) pRay.Add(new PointD(e, r * scale));
                    if (c > 0) pCom.Add(new PointD(e, c * scale));
                }
            }
        }
        //260607Ch resx 化 (旧: graphControlAtten.AxisLabelX = "E (keV)"; graphControlAtten.LabelX = "E:"; graphControlAtten.UnitX = " keV")
        graphControlAtten.AxisLabelX = R("Graph.Axis.EKeV"); graphControlAtten.LabelX = R("Graph.Label.E"); graphControlAtten.UnitX = R("Graph.Unit.KeV");

        List<Profile> profiles;
        if (mode == 2)//透過率モード
        {
            graphControlAtten.YLog = false;
            graphControlAtten.AxisLabelY = R("Graph.Axis.TransmissionPercent"); graphControlAtten.LabelY = R("Graph.Label.Transmission"); graphControlAtten.UnitY = R("Graph.Unit.Percent");//260607Ch resx 化
            graphControlAtten.GraphTitle = string.Format(System.Globalization.CultureInfo.CurrentCulture, R("Graph.Title.TransmissionThroughThickness"), numericBoxAttenThickness.Value);//260607Ch resx 書式文字列化
            profiles = [new Profile(pTrans) { Color = Color.Black, LineWidth = 1.8f, text = "transmission" }];
        }
        else//260608Cl µ(mode1) と µ/ρ(mode0,既定) は Y軸ラベル・タイトルのみ異なりプロファイル構成は同一 → 1分岐に統合 (旧: mode==1/else で同じ4プロファイルを二重定義)
        {
            bool lin = mode == 1;// 線吸収係数 µ か 質量吸収係数 µ/ρ か
            graphControlAtten.YLog = true;
            graphControlAtten.AxisLabelY = R(lin ? "Graph.Axis.MuCmInv" : "Graph.Axis.MuRhoCm2G");
            graphControlAtten.LabelY = R(lin ? "Graph.Label.Mu" : "Graph.Label.MuRho");
            graphControlAtten.UnitY = R(lin ? "Graph.Unit.CmInv" : "Graph.Unit.Cm2G");
            graphControlAtten.GraphTitle = R(lin ? (xrl ? "Graph.Title.MuTotal" : "Graph.Title.MuPhotoInternal")
                                          : (xrl ? "Graph.Title.MuRhoTotal" : "Graph.Title.MuRhoPhotoInternal"));
            profiles = [new(pTot) { Color = Color.Black, LineWidth = 1.8f, text = "total" }];
            if (pPho.Count > 0) profiles.Add(new Profile(pPho) { Color = Color.FromArgb(0xd6, 0x27, 0x28), text = "photo" });
            if (pRay.Count > 0) profiles.Add(new Profile(pRay) { Color = Color.FromArgb(0x2c, 0xa0, 0x2c), text = "Rayleigh" });
            if (pCom.Count > 0) profiles.Add(new Profile(pCom) { Color = Color.FromArgb(0x1f, 0x77, 0xb4), text = "Compton" });
        }

        //260608Cl 縦線は現在エネルギーの1本のみ表示 (交点マーカー値の読取りを1本に集約)。AddProfiles より前に設定し内部 Draw() で一括描画。
        //          旧: 現在エネルギーに加えて各元素の吸収端(K/L3)も縦線で描いていた → 1本に変更
        //var vlines = new List<PointD> { new(currentE, double.NaN) };
        //foreach (var (z, _) in els)
        //    foreach (var edge in new[] { XrayLineEdge.K, XrayLineEdge.L3 })
        //    {
        //        double ee = AtomStatic.CharacteristicXrayEnergy(z, edge);
        //        if (!double.IsNaN(ee) && ee > eMin && ee < eMax) vlines.Add(new PointD(ee, double.NaN));
        //    }
        //graphControlAtten.VerticalLines = [.. vlines];
        graphControlAtten.VerticalLines = [new PointD(currentE, double.NaN)];
        graphControlAtten.AddProfiles([.. profiles]);
    }

    // ---- 電子: 弾性断面積 / MFP / dE·ds / IMFP / 飛程 ----
    private void UpdateAttenuationElectron((int z, double occ)[] els, double totalOcc)
    {
        double kev = waveLengthControl.Energy;
        double avgZ = els.Sum(x => x.occ * x.z) / totalOcc;
        double avgA = els.Sum(x => x.occ * AtomStatic.AtomicWeight(x.z)) / totalOcc;
        double rho = Crystal.Density;

        double nv = MonteCarlo.EstimateAverageValenceElectronCount(els.Select(x => (x.z, x.occ * AtomStatic.AtomicWeight(x.z))));//260606Cl 質量重み平均価電子数 Nv(plasma E/IMFP の TPP-2M 精度向上。旧: avgZ 単一推定)
        var mc = GetAttenMonteCarlo(avgZ, avgA, rho, nv);
        var (_, sigma, mfp, dEds) = mc.GetParameters(kev);
        double imfpA = mc.GetInelasticMeanFreePathAngstrom(kev * 1000); // keV → eV
        double lambdaNm = UniversalConstants.Convert.EnergyToElectronWaveLength(kev);
        double rangeKO = 0.0276 * avgA * Math.Pow(kev, 1.67) / (Math.Pow(avgZ, 0.89) * rho); // Kanaya-Okayama [µm]

        miniTableAttenScalars.SetRows(  //260608Cl ラベルを Loc() 化 (ラジオ訳 弾性MFP/非弾性MFP/飛程CSDA と整合)
        [
            [Loc("λ (electron)", "λ (電子)"), Q(lambdaNm * 1000, "pm")],
            [Loc("σ elastic", "σ 弾性"), Q(sigma, "nm²")],
            [Loc("Elastic MFP", "弾性MFP"), Q(mfp, "nm")],
            [Loc("dE/ds (loss)", "dE/ds (損失)"), Q(dEds, "keV/nm")],
            [Loc("IMFP", "非弾性MFP"), Q(imfpA / 10, "nm")],
            [Loc("Plasma E", "プラズマ E"), mc.PlasmaEnergyEv > 0 ? Q(mc.PlasmaEnergyEv, "eV") : "N/A"],
            [Loc("J (mean exc.)", "J (平均励起)"), mc.J > 0 ? Q(mc.J, "eV") : "N/A"],
            [Loc("Range (Kanaya-Okayama)", "飛程 (Kanaya-Okayama)"), Q(rangeKO, "µm")],
            [Loc("Range (CSDA path)", "飛程 (CSDA経路)"), Q(mc.GetCsdaRangeMicron(kev), "µm")],//260606Cl 追加: 阻止能を ∫dE/|dE/ds| 積分した経路長。KO(後方散乱込みの侵入深さ近似)とは別物
            [Loc("mean Z, A", "平均 Z, A"), $"{avgZ:g4}, {avgA:g4}"],
        ]);

        //260606Cl 4列目を A(原子量)→ 元素別弾性断面積 σ_el[nm²] に変更 (§6.5)。NaN は null=空欄 (§11)。NIST範囲外(>36keV)は MonteCarlo 側で Rutherford 近似
        miniTableAttenElectron.SetRows(els.OrderBy(x => x.z)
            .Select(x =>
            {
                double sigma = MonteCarlo.ElasticCrossSectionNm2(x.z, kev);
                return new object[] { AtomStatic.AtomicName(x.z), x.z, x.occ / totalOcc * 100, double.IsNaN(sigma) ? null : (object)sigma };
            }).ToList());

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
        //260607Cl 0:全正規化 1:σ 2:MFP 3:dE/ds 4:IMFP 5:Range (combo→ラジオ群に変更。既定は radioButtonElecAll=0)
        int q = radioButtonElecSigma.Checked ? 1 : radioButtonElecEMFP.Checked ? 2 : radioButtonElecDeds.Checked ? 3 : radioButtonElecIMFP.Checked ? 4 : radioButtonElecRange.Checked ? 5 : 0;
        var raw = new (double k, double s, double m, double d, double imfp, double range)[n + 1];
        for (int i = 0; i <= n; i++)
        {
            double k = kMin + (kMax - kMin) * i / n;
            var (_, s, m, d) = mc.GetParameters(k);
            //260608Cl IMFP/Range は該当量を選択中のときだけ計算 (q==4/5 以外では未使用。全点での native 呼びを省く。旧: 常に両方を収集)
            double imfp = q == 4 ? mc.GetInelasticMeanFreePathAngstrom(k * 1000) / 10 : double.NaN;//IMFP[nm]
            double range = q == 5 ? mc.GetCsdaRangeMicron(k) : double.NaN;//Range[µm]
            raw[i] = (k, s, m, d, imfp, range);
        }

        //260607Ch resx 化 (旧: graphControlAtten.AxisLabelX = "E (keV)"; graphControlAtten.LabelX = "E:"; graphControlAtten.UnitX = " keV")
        graphControlAtten.AxisLabelX = R("Graph.Axis.EKeV"); graphControlAtten.LabelX = R("Graph.Label.E"); graphControlAtten.UnitX = R("Graph.Unit.KeV");
        graphControlAtten.VerticalLines = [new PointD(currentKev, double.NaN)];//260606Cl 縦線を AddProfiles より前に設定し内部 Draw() で一括描画

        if (q == 0)//全正規化重ね描き (従来の既定)
        {
            //260608Cl 堅牢化: 低エネルギー端 (k≈1keV) で GetParameters が NaN/Inf/負値 (dE/ds は損失で符号負) を返すと、
            //          旧 Math.Max(.., NaN) が max を NaN 汚染 → `>0` ガードで全曲線が空 → 軸レンジが NaN ("NaNE-2147483648") 化していた。
            //          NaN/Inf を除外し、dE/ds は絶対値 (損失の大きさ) で正規化する。
            static bool Ok(double v) => !double.IsNaN(v) && !double.IsInfinity(v);
            double sMax = 0, mMax = 0, dMax = 0;
            foreach (var r in raw)
            {
                if (Ok(r.s)) sMax = Math.Max(sMax, Math.Abs(r.s));
                if (Ok(r.m)) mMax = Math.Max(mMax, Math.Abs(r.m));
                if (Ok(r.d)) dMax = Math.Max(dMax, Math.Abs(r.d));
            }
            var sig = new List<PointD>(n + 1); var mfp = new List<PointD>(n + 1); var des = new List<PointD>(n + 1);
            foreach (var r in raw) // 3 量はスケール差大 → 各最大で正規化して重ね描き (絶対値は表)
            {
                if (sMax > 0 && Ok(r.s)) sig.Add(new PointD(r.k, Math.Abs(r.s) / sMax));
                if (mMax > 0 && Ok(r.m)) mfp.Add(new PointD(r.k, Math.Abs(r.m) / mMax));
                if (dMax > 0 && Ok(r.d)) des.Add(new PointD(r.k, Math.Abs(r.d) / dMax));
            }
            graphControlAtten.YLog = false;
            graphControlAtten.AxisLabelY = R("Graph.Axis.Normalized"); graphControlAtten.LabelY = R("Graph.Label.Normalized"); graphControlAtten.UnitY = "";//260607Ch resx 化
            graphControlAtten.GraphTitle = R("Graph.Title.ElectronTransportNormalized");
            graphControlAtten.AddProfiles(
            [
                new Profile(sig) { Color = Color.FromArgb(0xd6, 0x27, 0x28), text = "σ elastic" },
                new Profile(mfp) { Color = Color.FromArgb(0x1f, 0x77, 0xb4), text = "elastic MFP" },
                new Profile(des) { Color = Color.FromArgb(0x2c, 0xa0, 0x2c), text = "dE/ds" },
            ]);
        }
        else//単一量を絶対値で表示
        {
            // 選択量ごとに 値セレクタ / 軸ラベル(EN,JA) / 単位 / ラベル / 色 を決める  260607Cl
            Func<int, double> sel; string axisKey, unitKey, labelKey; Color color;//260607Ch axisEn/axisJa を resx キーへ変更
            switch (q)
            {
                case 1: sel = i => raw[i].s; axisKey = "Graph.Axis.SigmaElasticNm2"; unitKey = "Graph.Unit.Nm2"; labelKey = "Graph.Label.Sigma"; color = Color.FromArgb(0xd6, 0x27, 0x28); break;
                case 2: sel = i => raw[i].m; axisKey = "Graph.Axis.ElasticMfpNm"; unitKey = "Graph.Unit.Nm"; labelKey = "Graph.Label.Mfp"; color = Color.FromArgb(0x1f, 0x77, 0xb4); break;
                case 3: sel = i => raw[i].d; axisKey = "Graph.Axis.DedsKevNm"; unitKey = "Graph.Unit.KeVPerNm"; labelKey = "Graph.Label.Deds"; color = Color.FromArgb(0x2c, 0xa0, 0x2c); break;
                case 4: sel = i => raw[i].imfp; axisKey = "Graph.Axis.ImfpNm"; unitKey = "Graph.Unit.Nm"; labelKey = "Graph.Label.Imfp"; color = Color.FromArgb(0x94, 0x67, 0xbd); break;
                default: sel = i => raw[i].range; axisKey = "Graph.Axis.RangeCsdaMicron"; unitKey = "Graph.Unit.Micron"; labelKey = "Graph.Label.Range"; color = Color.FromArgb(0x8c, 0x56, 0x4b); break;
            }
            var pts = new List<PointD>(n + 1);
            for (int i = 0; i <= n; i++) { double v = sel(i); if (!double.IsNaN(v) && !double.IsInfinity(v)) pts.Add(new PointD(raw[i].k, v)); }
            graphControlAtten.YLog = false;
            graphControlAtten.AxisLabelY = R(axisKey); graphControlAtten.LabelY = R(labelKey); graphControlAtten.UnitY = R(unitKey);//260607Ch resx 化
            graphControlAtten.GraphTitle = string.Format(System.Globalization.CultureInfo.CurrentCulture, R("Graph.Title.QuantityVsE"), graphControlAtten.AxisLabelY);
            graphControlAtten.AddProfiles([new Profile(pts) { Color = color, LineWidth = 1.6f, text = graphControlAtten.LabelY.TrimEnd(':') }]);
        }
    }

    // ---- 中性子: 散乱長 / 断面積 (グラフ無) ----
    private void UpdateAttenuationNeutron((int z, double occ)[] els, double totalOcc)
    {
        double totalMass = els.Sum(x => x.occ * AtomStatic.AtomicWeight(x.z));
        double rho = Crystal.Density;

        double lambdaAng = waveLengthControl.WaveLength * 10.0; // nm → Å (σ_abs の 1/v 評価用)。260606Cl 追加

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

        miniTableAttenScalars.SetRows(
        [
            [Loc("b̄ (coherent)", "b̄ (コヒーレント)"), Q(bMean, "fm")],  //260608Cl ラベルを Loc() 化 (σ̄/Σ 等の記号は据置)
            [Loc("Coherent SLD", "コヒーレント SLD"), Q(sld * 1e6, "10⁻⁶Å⁻²", "g4")],
            ["σ̄_coh", Q(sCoh, "barn")],                                   // 260606Cl 4π|b|² (複素対応)
            ["σ̄_incoh", Q(sInc, "barn")],                                 // 260606Cl periodictable nsf
            [$"σ̄_abs (1/v, {lambdaAng:g3}Å)" + (anyResonant ? " *" : ""), Q(sAbs, "barn")], // 260606Cl 虚部から導出
            ["σ̄_total", Q(sTot, "barn")],                                 // 260606Cl
            [Loc("Σ_total (macro)", "Σ_total (マクロ)"), Q(sigmaTotMacro, "cm⁻¹")],                 // 260606Cl
            [Loc("Atten. length (1/Σ)", "減衰長 (1/Σ)"), QLen(attenLenCm)],                     // 260606Cl
            [Loc("source", "出典"), anyResonant ? "periodictable nsf (* 1/v invalid: Cd/Sm/Eu/Gd)" : "periodictable nsf (Sears 1992)"],
        ]);

        miniTableAttenNeutron.SetRows(els.OrderBy(x => x.z).Select(x =>
        {
            double b = NeutronB(x.z);
            object bCell = double.IsNaN(b) ? null : b;
            double sc = AtomStatic.NeutronCoherentCrossSection(x.z); // 260606Cl tabulated σ_coh (旧 4π·b.Real²/100 は共鳴核 Gd/Eu で誤り)
            object sCell = double.IsNaN(sc) ? null : sc;
            return new object[] { AtomStatic.AtomicName(x.z), bCell, sCell, x.occ / totalOcc * 100 };
        }).ToList());

        graphControlAtten.VerticalLines = [];
        graphControlAtten.GraphTitle = R("Graph.Title.NeutronNoEnergyGraph");//260607Ch resx 化
        graphControlAtten.AxisLabelX = graphControlAtten.AxisLabelY = "";//260607Cl グラフ無し状態は軸ラベル/単位を空に (前線種の残留を防ぐ)
        graphControlAtten.LabelX = graphControlAtten.LabelY = graphControlAtten.UnitX = graphControlAtten.UnitY = "";
        graphControlAtten.ClearProfile();
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
        miniTableFluorLines.AllowVerticalScroll = true; // 260606Cl 特性線表 (発光線×元素数=行数多) は縦スクロール許可
    }

    /// <summary>Fluorescence タブ更新。X線時のみ内容を出し、電子/中性子では無効メッセージ。260606Cl 追加。</summary>
    private void UpdateFluorescence()
    {
        if (!IsHandleCreated || !Visible || Crystal == null || graphControlFluor == null) return;
        if (tabControl.SelectedTab != tabPageFluorescence) return;

        if (waveLengthControl.WaveSource != WaveSource.Xray) return;//260607Ch タブ自体は ApplyBeamDependentVisibility で TabControl から外す

        bool xrl = Xraylib.Enabled;
        var els = AggregateElements();
        double totalOcc = els.Sum(x => x.occ);
        if (els.Length == 0 || totalOcc <= 0)
        {
            graphControlFluor.VerticalLines = [];
            graphControlFluor.ClearProfile();
            miniTableFluorScalars.ClearRows(); miniTableFluorLines.ClearRows();
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
        miniTableFluorLines.SetRows(lineRows);

        var scalarRows = new List<object[]>();
        foreach (var (z, _) in els.OrderBy(x => x.z))
        {
            double wk = xrl ? Xraylib.FluorescenceYield(z, Xraylib.XrlShell.K) : double.NaN;
            scalarRows.Add([$"ω_K ({AtomStatic.AtomicName(z)})", double.IsNaN(wk) ? "N/A" : wk.ToString("g3")]);
        }
        scalarRows.Add([Loc("strongest line", "最強線"), strongest]);  //260608Cl ラベルを Loc() 化 (ω_K は記号で据置)
        scalarRows.Add([Loc("source", "出典"), xrl ? "internal (E) / xraylib (I, ω)" : "internal (E only)"]);
        miniTableFluorScalars.SetRows(scalarRows);

        // EDX スティック: 各線を (E,0)-(E,h) の 2 点プロファイルで描く (高さは最大で正規化)
        graphControlFluor.YLog = false;
        //260607Ch resx 化 (旧: E (keV), Relative intensity, EDX タイトルをコード内リテラル/Loc で設定)
        graphControlFluor.AxisLabelX = R("Graph.Axis.EKeV"); graphControlFluor.LabelX = R("Graph.Label.E"); graphControlFluor.UnitX = R("Graph.Unit.KeV");
        graphControlFluor.AxisLabelY = R("Graph.Axis.RelativeIntensity"); graphControlFluor.LabelY = R("Graph.Label.Intensity"); graphControlFluor.UnitY = "";
        graphControlFluor.GraphTitle = R(xrl ? "Graph.Title.EdxSticks" : "Graph.Title.EdxRequiresXraylib");
        //260608Cl 蛍光タブは縦線不要 (旧: 現在エネルギーの縦線を表示 graphControlFluor.VerticalLines = [new PointD(waveLengthControl.Energy, double.NaN)];)
        graphControlFluor.VerticalLines = [];
        if (sticks.Count > 0 && hMax > 0)
        {
            var profiles = sticks.Select(s => new Profile(new List<PointD> { new(s.e, 0), new(s.e, s.h / hMax) })
            { Color = colorOf[s.z], LineWidth = 1.4f }).ToArray();
            graphControlFluor.AddProfiles(profiles);
        }
        else
            graphControlFluor.ClearProfile();
    }

    #endregion
}
