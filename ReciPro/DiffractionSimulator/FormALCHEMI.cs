// 260807Cl 新規作成: ALCHEMI シミュレータ (A4′、設計 §5.5)。FormDiffractionSimulator の子フォーム。
//
// 外殻の先例 = FormDiffractionSimulatorCBED (Owner=this・メニュー起動・専用計算経路)、
// 中身の先例 = FormDiffractionSimulatorDynamicCompression (自前の出力領域 + GraphControl)。
// ⚠CBED は「操作パネルだけで結果は親キャンバス」なので中身の先例ではない (設計 §5.5 の補足)。
//
// backend は Crystallography 側で完結している。ここがやるのは
//   ①パラメータ収集 → AlchemiRequest ②Task.Run で RunAlchemi ③AlchemiResult を曲線として描く
// の 3 つだけで、物理は一切持たない。
//
// 実曲線 (AlchemiCheck curve の β-AlCo) を見て決めたレイアウト:
//   ・**厚みセレクタを曲線の直下に置く** — サイト信号は厚みで符号すら変わる (20nm で相関 +0.74、
//     50nm で −0.65)。奥に隠すと「サイト判別できない」と誤解される
//   ・Bragg 位置の縦線を既定 ON、x 軸は mrad と θ_B の切替
//   ・曲線の下にコントラストと相関の要約を出す (どのサイトが効いているか一目で分かる)
//   ・基底診断 (beams / expanded-basis / F(s) 要求 s / fit 適格) を常時表示
#region using
using Crystallography;
using Crystallography.Controls;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace ReciPro;

public partial class FormALCHEMI : FormBase
{
    #region フィールド・プロパティ

    public FormDiffractionSimulator FormDiffractionSimulator;

    private Crystal Crystal => FormDiffractionSimulator.formMain.Crystal;
    private double Voltage => FormDiffractionSimulator.waveLengthControl.Energy;

    /// <summary>直近の run の結果 (null = 未実行)。表示切替はこれを描き直すだけ</summary>
    private AlchemiResult result;
    private CancellationTokenSource cts;
    /// <summary>チャネル一覧の照会結果 (表示順 = checkedListBoxChannels の項目順)</summary>
    private IonizationChannelInfo[] channelInfos = [];
    /// <summary>run 時の反射列と θ_B [rad]。実行後にユーザーが入力を触っても表示がずれないよう結果と一緒に固定する</summary>
    private (int H, int K, int L) resultRow;
    private double resultThetaB = double.NaN;
    private readonly System.Diagnostics.Stopwatch sw = new();

    /// <summary>曲線の色 (サイト × チャネルの順に巡回)。GraphControl の凡例色と一致する</summary>
    private static readonly Color[] SeriesColors =
    [
        Color.FromArgb(30, 90, 200), Color.FromArgb(210, 50, 40), Color.FromArgb(220, 140, 20),
        Color.FromArgb(40, 150, 70), Color.FromArgb(140, 60, 180), Color.FromArgb(0, 150, 170),
        Color.FromArgb(180, 100, 40), Color.FromArgb(120, 120, 120),
    ];

    #endregion

    #region コンストラクタ・表示切替・クローズ

    public FormALCHEMI()
    {
        InitializeComponent();
        //HelpPage = "7-diffraction-simulator"; //260807Cl 旧 (専用ページが無かったので親の概要ページ)
        //260809Cl: 専用ページ 7.4 を新設したので差し替え。⚠slug は実ページ名と一字一句一致必須 (ずれると F1 が 404)
        HelpPage = "7-diffraction-simulator/4-alchemi-simulation";

        //260807Cl: ARM64 には MKL/native Eigen の事情があるので CBED と同じ規律で選択肢を絞る
        comboBoxSolver.Items.AddRange([
            Localization.Loc(en: "Native", ja: "ネイティブ", de: "Nativ", fr: "Natif", es: "Nativo", pt: "Nativo",
                it: "Nativo", ru: "Нативный", zhHans: "本机", zhHant: "原生", ko: "네이티브"),
            Localization.Loc(en: "Managed", ja: "マネージド", de: "Verwaltet", fr: "Managé", es: "Gestionado", pt: "Gerido",
                it: "Gestito", ru: "Управляемый", zhHans: "托管", zhHant: "託管", ko: "관리형")]);
        comboBoxSolver.SelectedIndex = BetheMethod.EigenEnabled ? 0 : 1;
        comboBoxSolver.Enabled = BetheMethod.EigenEnabled;

        //§9-10 作者決定 (260807Cl): 既定は参照方位集合の加重平均 (v1 では走査全体の平均)。
        //per-maximum は**表示専用**なので、物理量そのものの raw と並べて明示的に選ばせる
        comboBoxNormalization.Items.AddRange([
            Localization.Loc(en: "Scan mean (ICP)", ja: "走査平均 (ICP)", de: "Scan-Mittel (ICP)", fr: "Moyenne du balayage (ICP)",
                es: "Media del barrido (ICP)", pt: "Média da varredura (ICP)", it: "Media della scansione (ICP)",
                ru: "Среднее по скану (ICP)", zhHans: "扫描平均 (ICP)", zhHant: "掃描平均 (ICP)", ko: "스캔 평균 (ICP)"),
            Localization.Loc(en: "Maximum = 1", ja: "最大値 = 1", de: "Maximum = 1", fr: "Maximum = 1", es: "Máximo = 1",
                pt: "Máximo = 1", it: "Massimo = 1", ru: "Максимум = 1", zhHans: "最大值 = 1", zhHant: "最大值 = 1", ko: "최댓값 = 1"),
            Localization.Loc(en: "Raw (per electron)", ja: "生値 (電子 1 個あたり)", de: "Roh (pro Elektron)",
                fr: "Brut (par électron)", es: "Bruto (por electrón)", pt: "Bruto (por elétron)", it: "Grezzo (per elettrone)",
                ru: "Сырое (на электрон)", zhHans: "原始值 (每电子)", zhHant: "原始值 (每電子)", ko: "원시값 (전자당)")]);
        comboBoxNormalization.SelectedIndex = 0;

        comboBoxXAxis.Items.AddRange(["mrad", "θ_B"]);
        comboBoxXAxis.SelectedIndex = 0;

        ApplyLocalization();

        //値の変更で θ_B 表示を更新 (レイアウトではないのでコード側で配線する)
        foreach (var nb in new[] { numericBoxAxisH, numericBoxAxisK, numericBoxAxisL, numericBoxRange, numericBoxPoints })
            nb.ValueChanged += (s, e) => UpdateScanLabel();
        VisibleChanged += (s, e) => { if (Visible) RefreshLists(); };
    }

    /// <summary>表示文字列を 11 言語へ差し替える (Designer は英語のまま = VS で開いても壊れない)。</summary>
    private void ApplyLocalization()
    {
        Text = Localization.Loc(en: "ALCHEMI simulator", ja: "ALCHEMI シミュレータ", de: "ALCHEMI-Simulator",
            fr: "Simulateur ALCHEMI", es: "Simulador ALCHEMI", pt: "Simulador ALCHEMI", it: "Simulatore ALCHEMI",
            ru: "Симулятор ALCHEMI", zhHans: "ALCHEMI 模拟器", zhHant: "ALCHEMI 模擬器", ko: "ALCHEMI 시뮬레이터");
        groupBoxScan.Text = Localization.Loc(en: "Rocking scan", ja: "ロッキング走査", de: "Rocking-Scan", fr: "Balayage d'inclinaison",
            es: "Barrido de inclinación", pt: "Varredura de inclinação", it: "Scansione di rocking", ru: "Качательное сканирование",
            zhHans: "摇摆扫描", zhHant: "搖擺掃描", ko: "로킹 스캔");
        numericBoxAxisH.HeaderText = Localization.Loc(en: "Row  (", ja: "反射列  (", de: "Reihe  (", fr: "Rangée  (", es: "Fila  (",
            pt: "Fila  (", it: "Fila  (", ru: "Ряд  (", zhHans: "反射列  (", zhHant: "反射列  (", ko: "반사열  (");
        numericBoxRange.HeaderText = Localization.Loc(en: "Range  ±", ja: "範囲  ±", de: "Bereich  ±", fr: "Plage  ±", es: "Rango  ±",
            pt: "Faixa  ±", it: "Intervallo  ±", ru: "Диапазон  ±", zhHans: "范围  ±", zhHant: "範圍  ±", ko: "범위  ±");
        numericBoxPoints.HeaderText = Localization.Loc(en: "Points", ja: "点数", de: "Punkte", fr: "Points", es: "Puntos",
            pt: "Pontos", it: "Punti", ru: "Точки", zhHans: "点数", zhHant: "點數", ko: "점 수");
        groupBoxThickness.Text = Localization.Loc(en: "Thickness", ja: "厚み", de: "Dicke", fr: "Épaisseur", es: "Espesor",
            pt: "Espessura", it: "Spessore", ru: "Толщина", zhHans: "厚度", zhHant: "厚度", ko: "두께");
        numericBoxThicknessStart.HeaderText = Localization.Loc(en: "from", ja: "開始", de: "von", fr: "de", es: "desde",
            pt: "de", it: "da", ru: "от", zhHans: "从", zhHant: "從", ko: "부터");
        numericBoxThicknessEnd.HeaderText = Localization.Loc(en: "to", ja: "終了", de: "bis", fr: "à", es: "hasta",
            pt: "até", it: "a", ru: "до", zhHans: "到", zhHant: "到", ko: "까지");
        numericBoxThicknessStep.HeaderText = Localization.Loc(en: "step", ja: "刻み", de: "Schritt", fr: "pas", es: "paso",
            pt: "passo", it: "passo", ru: "шаг", zhHans: "步长", zhHant: "步長", ko: "간격");
        groupBoxCalculation.Text = Localization.Loc(en: "Calculation", ja: "計算条件", de: "Berechnung", fr: "Calcul",
            es: "Cálculo", pt: "Cálculo", it: "Calcolo", ru: "Расчёт", zhHans: "计算", zhHant: "計算", ko: "계산");
        numericBoxMaxNumOfBloch.HeaderText = Localization.Loc(en: "Max. beams", ja: "最大波数", de: "Max. Strahlen", fr: "Faisceaux max.",
            es: "Haces máx.", pt: "Feixes máx.", it: "Fasci max.", ru: "Макс. пучков", zhHans: "最大波数", zhHant: "最大波數", ko: "최대 빔 수");
        labelSolver.Text = Localization.Loc(en: "Solver", ja: "ソルバ", de: "Löser", fr: "Solveur", es: "Solucionador",
            pt: "Solucionador", it: "Risolutore", ru: "Решатель", zhHans: "求解器", zhHant: "求解器", ko: "솔버");
        checkBoxDechannelling.Text = Localization.Loc(en: "Include the dechannelled component",
            ja: "非チャネリング成分を含める", de: "Dechannelling-Anteil einbeziehen", fr: "Inclure la composante déchenalisée",
            es: "Incluir la componente descanalizada", pt: "Incluir a componente descanalizada",
            it: "Includi la componente decanalizzata", ru: "Учитывать неканалированную составляющую",
            zhHans: "包含非沟道成分", zhHant: "包含非通道成分", ko: "비채널링 성분 포함");
        groupBoxChannels.Text = Localization.Loc(en: "Ionization channels", ja: "イオン化チャネル", de: "Ionisationskanäle",
            fr: "Canaux d'ionisation", es: "Canales de ionización", pt: "Canais de ionização", it: "Canali di ionizzazione",
            ru: "Каналы ионизации", zhHans: "电离通道", zhHant: "游離通道", ko: "이온화 채널");
        groupBoxSites.Text = Localization.Loc(en: "Site hypotheses", ja: "サイト仮説", de: "Platzhypothesen", fr: "Hypothèses de site",
            es: "Hipótesis de sitio", pt: "Hipóteses de sítio", it: "Ipotesi di sito", ru: "Гипотезы о позициях",
            zhHans: "位点假设", zhHant: "位點假設", ko: "자리 가설");
        buttonSimulate.Text = Localization.Loc(en: "Simulate", ja: "計算", de: "Simulieren", fr: "Simuler", es: "Simular",
            pt: "Simular", it: "Simula", ru: "Рассчитать", zhHans: "计算", zhHant: "計算", ko: "계산");
        buttonStop.Text = Localization.Loc(en: "Stop", ja: "中止", de: "Stopp", fr: "Arrêter", es: "Detener",
            pt: "Parar", it: "Ferma", ru: "Стоп", zhHans: "停止", zhHant: "停止", ko: "중지");
        tabPageCurve.Text = Localization.Loc(en: "Curve", ja: "曲線", de: "Kurve", fr: "Courbe", es: "Curva",
            pt: "Curva", it: "Curva", ru: "Кривая", zhHans: "曲线", zhHant: "曲線", ko: "곡선");
        labelThickness.Text = groupBoxThickness.Text;
        labelNormalization.Text = Localization.Loc(en: "Normalization", ja: "規格化", de: "Normierung", fr: "Normalisation",
            es: "Normalización", pt: "Normalização", it: "Normalizzazione", ru: "Нормировка", zhHans: "归一化",
            zhHant: "歸一化", ko: "정규화");
        labelXAxis.Text = Localization.Loc(en: "X axis", ja: "X 軸", de: "X-Achse", fr: "Axe X", es: "Eje X",
            pt: "Eixo X", it: "Asse X", ru: "Ось X", zhHans: "X 轴", zhHant: "X 軸", ko: "X 축");
        checkBoxShowBragg.Text = Localization.Loc(en: "Bragg conditions", ja: "Bragg 条件", de: "Bragg-Bedingungen",
            fr: "Conditions de Bragg", es: "Condiciones de Bragg", pt: "Condições de Bragg", it: "Condizioni di Bragg",
            ru: "Условия Брэгга", zhHans: "Bragg 条件", zhHant: "Bragg 條件", ko: "Bragg 조건");
        buttonExport.Text = Localization.Loc(en: "Export CSV", ja: "CSV 出力", de: "CSV exportieren", fr: "Exporter en CSV",
            es: "Exportar CSV", pt: "Exportar CSV", it: "Esporta CSV", ru: "Экспорт CSV", zhHans: "导出 CSV",
            zhHant: "匯出 CSV", ko: "CSV 내보내기");

        //ツールチップ (監査方針: 新規コントロールには必ず付ける)
        toolTip.SetToolTip(numericBoxAxisH, Localization.Loc(
            en: "The systematic row to sweep, as reflection indices. The tilt axis is taken perpendicular to both the beam and this g, so the scan sweeps this row through its Bragg conditions.",
            ja: "掃引する系統反射列を反射指数で指定します。傾斜軸はビームとこの g の両方に垂直に取られるので、走査はこの列を Bragg 条件を通して掃きます。"));
        toolTip.SetToolTip(numericBoxRange, Localization.Loc(
            en: "Half width of the tilt scan. Beyond about 10 mrad a fixed union basis is no longer guaranteed and the expanded-basis check becomes mandatory; beyond 30 mrad it is outside the v1 guarantee.",
            ja: "傾斜走査の半幅です。10 mrad を超えると固定 union 基底の保証が外れ expanded-basis 検証が必須になり、30 mrad を超えると v1 の保証範囲外です。"));
        //260810Cl: dataset v5 で表が s ≤ 16 Å⁻¹ になり、この上限 1600 と対になった。
        //実測の最大要求は 1600 波でも 10.54 Å⁻¹ (加速電圧に依らない) なので収録範囲は使い切れない
        toolTip.SetToolTip(numericBoxMaxNumOfBloch, Localization.Loc(
            en: "Upper bound on the number of Bloch waves per orientation. The union over the whole scan is larger. The ionization form factor is tabulated to s = 16 A^-1, and this cap of 1600 is its counterpart: even at 1600 beams the basis only reaches about 10.5 A^-1. The diagnostic line below the graph reports how far it actually reached.",
            ja: "1 方位あたりのブロッホ波数の上限です。走査全体の union はこれより大きくなります。イオン化形状因子は s = 16 Å⁻¹ まで収録されており、この上限 1600 はその対です (1600 波でも基底が要求する s は約 10.5 Å⁻¹ に留まります)。実際の到達値はグラフ下の診断行に出ます。"));
        toolTip.SetToolTip(checkBoxDechannelling, Localization.Loc(
            en: "Electrons removed from the coherent Bloch field by thermal-diffuse absorption are re-emitted as randomly directed electrons over the remaining thickness. Omitting this dilutes the site contrast by tens of percent at typical thicknesses.",
            ja: "熱散漫吸収でコヒーレントなブロッホ場から失われた電子を、方向がランダム化された電子として残りの厚みぶん走らせます。省くと典型的な厚みでサイトコントラストが数十パーセント薄まります。"));
        toolTip.SetToolTip(checkedListBoxChannels, Localization.Loc(
            en: "Element and shell to ionize. Cross sections are Bote-Salvat and the shape factors are self-generated DHFS tables; channels that cannot be excited or fall outside the tabulated range are listed with the reason and cannot be selected.",
            ja: "イオン化する元素と殻です。断面積は Bote-Salvat、形状因子は自前の DHFS テーブルです。励起できない、または収録範囲外のチャネルは理由付きで表示され選択できません。"));
        toolTip.SetToolTip(checkedListBoxSites, Localization.Loc(
            en: "Atomic sites whose yield is computed separately. In the tracer picture a channel may be paired with any site, so a dopant channel on a host site is a legitimate hypothesis.",
            ja: "収量を別々に計算する原子サイトです。トレーサ近似ではチャネルとサイトの組み合わせは自由なので、ホストサイト上のドーパントチャネルも正当な仮説です。"));
        toolTip.SetToolTip(trackBarThickness, Localization.Loc(
            en: "Thickness shown in the graph. The site contrast changes strongly - and can even reverse sign - between thin and thick specimens, so check several thicknesses before drawing conclusions.",
            ja: "グラフに表示する厚みです。サイトコントラストは薄い試料と厚い試料で大きく変わり符号すら反転しうるので、結論を出す前に複数の厚みを確認してください。"));
        toolTip.SetToolTip(comboBoxNormalization, Localization.Loc(
            en: "Display normalization only; the stored quantity is always vacancies generated per incident electron. Maximum = 1 is for display and must not be used as an ICP reference.",
            ja: "表示上の規格化だけで、保存される量は常に入射電子 1 個あたりの発生空孔数です。最大値 = 1 は表示専用で、ICP の基準には使えません。"));
        toolTip.SetToolTip(buttonExport, Localization.Loc(
            en: "Write the raw curves (dynamic, dechannelled and total, per incident electron) for every orientation, thickness, site and channel to a CSV file.",
            ja: "全方位・全厚み・全サイト・全チャネルの生の曲線 (動力学・非チャネリング・合計、入射電子 1 個あたり) を CSV に書き出します。"));
    }

    private void FormALCHEMI_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (e.CloseReason == CloseReason.UserClosing)
        {
            e.Cancel = true;
            cts?.Cancel();
            Visible = false;
        }
    }

    #endregion

    #region チャネル・サイト一覧の更新

    /// <summary>結晶や加速電圧が変わったときにチャネル候補とサイト候補を作り直す。
    /// 収録範囲 (K は Z=6-50 等) は Crystallography 側の照会に任せ、GUI にハードコードしない (設計 §5.9-3)。</summary>
    private void RefreshLists()
    {
        if (FormDiffractionSimulator == null || Crystal?.Atoms == null) return;

        var checkedChannels = checkedListBoxChannels.CheckedItems.Cast<string>().ToHashSet();
        checkedListBoxChannels.Items.Clear();
        channelInfos = IonizationDataProvider.EnumerateChannels(Crystal, Voltage);
        foreach (var info in channelInfos)
        {
            var text = info.ToListItemText();
            checkedListBoxChannels.Items.Add(text,
                info.Status == IonizationAvailability.Available && (checkedChannels.Count == 0 || checkedChannels.Contains(text)));
        }

        var checkedSites = checkedListBoxSites.CheckedItems.Cast<string>().ToHashSet();
        checkedListBoxSites.Items.Clear();
        foreach (var atoms in Crystal.Atoms)
        {
            var text = $"{atoms.Label}   {AtomStatic.AtomicName(atoms.AtomicNumber)}   "
                + $"({atoms.X:f4}, {atoms.Y:f4}, {atoms.Z:f4})   ×{atoms.Atom.Length}   Occ {atoms.Occ:f3}";
            checkedListBoxSites.Items.Add(text, checkedSites.Count == 0 || checkedSites.Contains(text));
        }
        UpdateScanLabel();
    }

    /// <summary>選んだ反射列の θ_B と走査刻みを表示する (実行前に「何 θ_B ぶん振るのか」が分かるように)。</summary>
    private void UpdateScanLabel()
    {
        if (FormDiffractionSimulator == null || Crystal == null) { labelThetaB.Text = "-"; return; }
        var (h, k, l) = (numericBoxAxisH.ValueInteger, numericBoxAxisK.ValueInteger, numericBoxAxisL.ValueInteger);
        var range = numericBoxRange.Value * 1e-3;
        var step = 2 * range / Math.Max(1, numericBoxPoints.ValueInteger - 1);
        var kvac = UniversalConstants.Convert.EnergyToElectronWaveNumber(Voltage);
        var g = Crystal.RotationMatrix * (Crystal.MatrixInverseTransposed * (h, k, l));
        if (g.Length < 1e-9)
        {
            labelThetaB.Text = Localization.Loc(en: "Specify a non-zero reflection row.", ja: "0 でない反射列を指定してください。",
                de: "Geben Sie eine Reihe ungleich null an.", fr: "Indiquez une rangée non nulle.",
                es: "Indique una fila no nula.", pt: "Indique uma fila não nula.", it: "Indicare una fila non nulla.",
                ru: "Укажите ненулевой ряд.", zhHans: "请指定非零反射列。", zhHant: "請指定非零反射列。", ko: "0 이 아닌 반사열을 지정하세요.");
            return;
        }
        var thetaB = g.Length / (2 * kvac);
        labelThetaB.Text = $"θ_B({h} {k} {l}) = {thetaB * 1e3:f3} mrad   →   ±{range / thetaB:f2} θ_B,   "
            + $"{Localization.Loc(en: "step", ja: "刻み", de: "Schritt", fr: "pas", es: "paso", pt: "passo", it: "passo", ru: "шаг", zhHans: "步长", zhHant: "步長", ko: "간격")} {step * 1e3:f3} mrad";
    }

    #endregion

    #region 実行

    /// <summary>260809Cl 追加: GUI の入力から run 1 回ぶんの要求を組む (null = チャネル/サイト未選択、または軸が縮退)。
    /// <see cref="buttonSimulate_Click"/> と <see cref="PrepareCaptureForGuiAudit"/> で共用するため切り出した。
    /// 旧: この処理は buttonSimulate_Click に inline だった。</summary>
    private AlchemiRequest BuildRequest(out (int H, int K, int L) row, out double thetaB)
    {
        row = (numericBoxAxisH.ValueInteger, numericBoxAxisK.ValueInteger, numericBoxAxisL.ValueInteger);
        thetaB = double.NaN;

        var channels = Enumerable.Range(0, checkedListBoxChannels.Items.Count)
            .Where(checkedListBoxChannels.GetItemChecked)
            .Select(i => channelInfos[i])
            .Where(c => c.Status == IonizationAvailability.Available)
            .Select(c => c.Channel).ToArray();
        var siteIndices = Enumerable.Range(0, checkedListBoxSites.Items.Count).Where(checkedListBoxSites.GetItemChecked).ToArray();
        if (channels.Length == 0 || siteIndices.Length == 0)
        {
            toolStripStatusLabel.Text = Localization.Loc(
                en: "Select at least one ionization channel and one site.", ja: "イオン化チャネルとサイトを 1 つ以上選んでください。",
                de: "Wählen Sie mindestens einen Ionisationskanal und einen Platz.", fr: "Sélectionnez au moins un canal d'ionisation et un site.",
                es: "Seleccione al menos un canal de ionización y un sitio.", pt: "Selecione ao menos um canal de ionização e um sítio.",
                it: "Selezionare almeno un canale di ionizzazione e un sito.", ru: "Выберите хотя бы один канал ионизации и одну позицию.",
                zhHans: "请至少选择一个电离通道和一个位点。", zhHant: "請至少選擇一個游離通道與一個位點。", ko: "이온화 채널과 자리를 각각 하나 이상 선택하세요.");
            return null;
        }

        var (h, k, l) = row;
        var rotation = new Matrix3D(Crystal.RotationMatrix);
        var beam = new Vector3DBase(0, 0, -1);
        //掃く反射列 g に対し、傾斜軸は「ビームと g の両方に垂直」= その軸まわりに振ると s_g が動く
        var g = rotation * (Crystal.MatrixInverseTransposed * (h, k, l));
        var axis = Vector3DBase.VectorProduct(beam, g);
        if (axis.Length < 1e-9) { UpdateScanLabel(); return null; }
        thetaB = g.Length / (2 * UniversalConstants.Convert.EnergyToElectronWaveNumber(Voltage));

        var range = numericBoxRange.Value * 1e-3;
        var scan = AlchemiScan.Rocking1D(beam, axis, -range, range, numericBoxPoints.ValueInteger);
        var thicknesses = new List<double>();
        for (var t = numericBoxThicknessStart.Value; t <= numericBoxThicknessEnd.Value + 1e-9; t += numericBoxThicknessStep.Value)
            thicknesses.Add(t);
        var sites = siteIndices.Select(i => new AlchemiSiteBasis(Crystal.Atoms[i].Label, [i])).ToArray();

        return new AlchemiRequest(Voltage, rotation, scan, [.. thicknesses], sites, channels)
        {
            MaxNumOfBloch = numericBoxMaxNumOfBloch.ValueInteger,
            IncludeDechannelledComponent = checkBoxDechannelling.Checked,
            UseNativeSolver = comboBoxSolver.SelectedIndex == 0,
        };
    }

    private async void buttonSimulate_Click(object sender, EventArgs e)
    {
        if (cts != null) return;

        var request = BuildRequest(out var row, out var thetaB);
        if (request == null) return;

        cts = new CancellationTokenSource();
        buttonStop.Visible = true;
        buttonSimulate.Enabled = false;
        toolStripProgressBar.Value = 0;
        sw.Restart();
        var progress = new Progress<AlchemiProgress>(ReportProgress);
        var bethe = Crystal.Bethe;
        try
        {
            result = await Task.Run(() => bethe.RunAlchemi(request, ((IProgress<AlchemiProgress>)progress).Report, cts.Token), cts.Token);
            sw.Stop();
            resultRow = row;
            resultThetaB = thetaB;
            toolStripStatusLabel.Text = $"{sw.ElapsedMilliseconds / 1000.0:f2} s";
            SetupThicknessSelector();
            DrawCurves();
        }
        catch (OperationCanceledException)
        {
            toolStripStatusLabel.Text = Localization.Loc(en: "Interrupted.", ja: "中断しました。", de: "Abgebrochen.",
                fr: "Interrompu.", es: "Interrumpido.", pt: "Interrompido.", it: "Interrotto.", ru: "Прервано.",
                zhHans: "已中断。", zhHant: "已中斷。", ko: "중단되었습니다.");
        }
        catch (Exception ex)
        {
            //backend は「収録範囲外を黙って外挿しない」ので拒否が起こり得る。理由をそのまま見せる
            toolStripStatusLabel.Text = ex.Message;
            labelStats.Text = ex.Message;
        }
        finally
        {
            cts.Dispose();
            cts = null;
            buttonStop.Visible = false;
            buttonSimulate.Enabled = true;
            toolStripProgressBar.Value = toolStripProgressBar.Maximum;
        }
    }

    private void buttonStop_Click(object sender, EventArgs e) => cts?.Cancel();

    /// <summary>260809Cl 追加: <c>--capture</c> 用に「曲線が出た状態」を作る。
    /// 空のフォームではマニュアルの説明図にならない (Pages 編集方針 §5) が、自動キャプチャは Show した直後に撮るので、
    /// ここで代表計算まで済ませる。撮影ループは BackgroundWorker/await の完了を待てないため、
    /// <see cref="BetheMethod.RunAlchemi"/> を **UI スレッドで同期に**呼ぶ (進捗・キャンセルは不要)。
    /// 入力値は既定のまま = マニュアルの表に載せている既定値と図が一致する。</summary>
    public void PrepareCaptureForGuiAudit()
    {
        RefreshLists(); //VisibleChanged 経由の初期化に頼らない (撮影順で Visible が前後するため)
        var request = BuildRequest(out var row, out var thetaB);
        if (request == null) return;

        sw.Restart();
        result = Crystal.Bethe.RunAlchemi(request);
        sw.Stop();
        resultRow = row;
        resultThetaB = thetaB;
        toolStripStatusLabel.Text = $"{sw.ElapsedMilliseconds / 1000.0:f2} s";
        toolStripProgressBar.Value = toolStripProgressBar.Maximum;
        SetupThicknessSelector();
        DrawCurves();
    }

    private void ReportProgress(AlchemiProgress p)
    {
        //ステージ名は backend が持たない (表示都合の文字列は GUI 側の責務)
        var stage = p.Stage switch
        {
            AlchemiStage.ResolvingIonizationData => Localization.Loc(en: "Resolving ionization data", ja: "イオン化データを解決中",
                de: "Ionisationsdaten werden aufgelöst", fr: "Résolution des données d'ionisation", es: "Resolviendo datos de ionización",
                pt: "Resolvendo dados de ionização", it: "Risoluzione dei dati di ionizzazione", ru: "Разрешение данных ионизации",
                zhHans: "正在解析电离数据", zhHant: "正在解析游離資料", ko: "이온화 데이터 해결 중"),
            AlchemiStage.BuildingUnionBasis => Localization.Loc(en: "Building the union basis", ja: "union 基底を構築中",
                de: "Vereinigungsbasis wird aufgebaut", fr: "Construction de la base union", es: "Construyendo la base unión",
                pt: "Construindo a base união", it: "Costruzione della base unione", ru: "Построение объединённого базиса",
                zhHans: "正在构建并集基组", zhHant: "正在建構聯集基組", ko: "합집합 기저 구성 중"),
            AlchemiStage.BuildingMuMatrices => Localization.Loc(en: "Building the ionization matrices", ja: "イオン化行列を構築中",
                de: "Ionisationsmatrizen werden aufgebaut", fr: "Construction des matrices d'ionisation", es: "Construyendo las matrices de ionización",
                pt: "Construindo as matrizes de ionização", it: "Costruzione delle matrici di ionizzazione", ru: "Построение матриц ионизации",
                zhHans: "正在构建电离矩阵", zhHant: "正在建構游離矩陣", ko: "이온화 행렬 구성 중"),
            AlchemiStage.SolvingOrientations => Localization.Loc(en: "Solving orientations", ja: "方位を計算中",
                de: "Orientierungen werden gelöst", fr: "Résolution des orientations", es: "Resolviendo orientaciones",
                pt: "Resolvendo orientações", it: "Risoluzione delle orientazioni", ru: "Расчёт ориентаций",
                zhHans: "正在计算取向", zhHant: "正在計算取向", ko: "방위 계산 중"),
            _ => Localization.Loc(en: "Checking the expanded basis", ja: "拡張基底を検証中",
                de: "Erweiterte Basis wird geprüft", fr: "Vérification de la base élargie", es: "Verificando la base ampliada",
                pt: "Verificando a base ampliada", it: "Verifica della base ampliata", ru: "Проверка расширенного базиса",
                zhHans: "正在检验扩展基组", zhHant: "正在檢驗擴展基組", ko: "확장 기저 검증 중"),
        };
        toolStripProgressBar.Value = Math.Clamp((int)(p.Fraction * 100), 0, 100);
        toolStripStatusLabel.Text = $"{stage}  {p.Fraction:p0}   ({sw.ElapsedMilliseconds / 1000.0:f1} s)";
    }

    #endregion

    #region 描画

    private void SetupThicknessSelector()
    {
        trackBarThickness.Maximum = Math.Max(0, result.ThicknessesNm.Length - 1);
        trackBarThickness.Value = trackBarThickness.Maximum;
    }

    private void trackBarThickness_Scroll(object sender, EventArgs e) => DrawCurves();
    private void display_Changed(object sender, EventArgs e) => DrawCurves();

    private void DrawCurves()
    {
        if (result == null) return;
        var s = result.Shape;
        int t = Math.Clamp(trackBarThickness.Value, 0, s.ThicknessCount - 1);
        labelThicknessValue.Text = $"{result.ThicknessesNm[t]:f1} nm";

        var thetaB = resultThetaB;
        var inThetaB = comboBoxXAxis.SelectedIndex == 1 && !double.IsNaN(thetaB);
        double XOf(int o) => inThetaB ? result.Orientations[o].TiltRad / thetaB : result.Orientations[o].TiltRad * 1e3;

        var profiles = new List<Profile>();
        var stats = new StringBuilder();
        double[] first = null;
        int color = 0;
        for (int si = 0; si < s.SiteCount; si++)
            for (int c = 0; c < s.ChannelCount; c++)
            {
                var y = Normalize(result.Curve(result.Total, t, si, c));
                var p = new Profile { Color = SeriesColors[color++ % SeriesColors.Length], LineWidth = 2f,
                    text = $"{result.Sites[si].Label} / {result.ChannelData[c].Target.ShortLabel}" };
                for (int o = 0; o < s.OrientationCount; o++)
                    p.Pt.Add(new PointD(XOf(o), y[o]));
                profiles.Add(p);
                first ??= y;
                var contrast = y.Max() - y.Min();
                var mean = y.Average();
                stats.Append($"{p.text}: {(mean > 0 ? contrast / mean : 0):p1}");
                if (!ReferenceEquals(y, first)) stats.Append($" (r = {Correlation(first, y):f2})");
                stats.Append("    ");
            }

        //Bragg 位置 = 掃いた反射列の整数倍 (θ = n·θ_B)。実曲線でいちばん効いた表示要素。
        //260809Cl 修正: VerticalLines の setter は**再描画しない** (呼び出し側が AddProfiles/Draw で一括描画する規約。
        //GraphControl.cs:434 と FormBeamInteraction.cs:1179 の注記) ので、AddProfiles より**前**に設定する。
        //旧: AddProfiles の後に代入していたため縦線が 1 回も描かれていなかった
        graphControl.VerticalLines = checkBoxShowBragg.Checked && !double.IsNaN(thetaB)
            ? [.. BraggPositions(thetaB, inThetaB)] : [];
        graphControl.AddProfiles([.. profiles], showLegend: true);

        labelStats.Text = Localization.Loc(en: "Contrast (max−min)/mean", ja: "コントラスト (max−min)/mean",
            de: "Kontrast (max−min)/Mittel", fr: "Contraste (max−min)/moyenne", es: "Contraste (max−min)/media",
            pt: "Contraste (max−min)/média", it: "Contrasto (max−min)/media", ru: "Контраст (max−min)/среднее",
            zhHans: "对比度 (max−min)/mean", zhHant: "對比度 (max−min)/mean", ko: "대비 (max−min)/평균") + " —  " + stats;

        var b = result.Basis;
        labelBasis.Text = $"basis {b.BeamCount} ({b.CenterOnlyBeamCount} + {b.AddedByUnion})   "
            + $"F(s) ≤ {b.MaxShapeArgumentAngstromInv:f2} Å⁻¹   expanded-basis {b.ExpandedBasisMaxRelDiff:e1}   "
            + (b.AcceptedForFit
                ? Localization.Loc(en: "fit-eligible", ja: "fit 適格", de: "fit-tauglich", fr: "apte au fit", es: "apto para ajuste",
                    pt: "apto para ajuste", it: "idoneo al fit", ru: "пригодно для подгонки", zhHans: "可用于拟合", zhHant: "可用於擬合", ko: "피팅 적격")
                : "⚠ " + Localization.Loc(en: "NOT fit-eligible", ja: "fit 不適格", de: "NICHT fit-tauglich", fr: "NON apte au fit",
                    es: "NO apto para ajuste", pt: "NÃO apto para ajuste", it: "NON idoneo al fit", ru: "НЕ пригодно для подгонки",
                    zhHans: "不可用于拟合", zhHant: "不可用於擬合", ko: "피팅 부적격"))
            + LowVoltageNotice()
            + TruncationNotice()
            + (b.Warnings.Length == 0 ? "" : "   ⚠ " + string.Join(" / ", b.Warnings));
    }

    /// <summary>260810Cl 追加: 80 kV 未満の告知。**hard gate にはしない** (作者決定) —
    /// s の要求が s_kin(E0) に収まる限り計算そのものは正しいので、電圧で弾くのは過剰。
    /// 「16 Å⁻¹ を保証できる下限が 80 kV」という事実だけを伝える。</summary>
    private string LowVoltageNotice() => Voltage >= 80 ? "" : "   ⚠ " + Localization.Loc(
        en: "below 80 kV: the form factor table cannot guarantee s up to 16 A^-1 at this voltage",
        ja: "80 kV 未満: この電圧では形状因子テーブルが s = 16 Å⁻¹ までを保証できません",
        de: "unter 80 kV: die Formfaktortabelle kann s bis 16 A^-1 bei dieser Spannung nicht garantieren",
        fr: "sous 80 kV : la table de facteurs de forme ne garantit pas s jusqu'à 16 A^-1 à cette tension",
        es: "por debajo de 80 kV: la tabla de factores de forma no garantiza s hasta 16 A^-1 a esta tensión",
        pt: "abaixo de 80 kV: a tabela de fatores de forma não garante s até 16 A^-1 nesta tensão",
        it: "sotto 80 kV: la tabella dei fattori di forma non garantisce s fino a 16 A^-1 a questa tensione",
        ru: "ниже 80 кВ: таблица форм-факторов не гарантирует s до 16 A^-1 при этом напряжении",
        zhHans: "低于 80 kV：该电压下形状因子表无法保证 s 至 16 A^-1",
        zhHant: "低於 80 kV：該電壓下形狀因子表無法保證 s 至 16 A^-1",
        ko: "80 kV 미만: 이 전압에서는 형상 인자 표가 s = 16 A^-1 까지를 보장하지 못합니다");

    /// <summary>260810Cl 追加: s &gt; s_cert を 0 で打ち切った場合の告知 (dataset v5)。
    /// **外挿と違って誤差の大きさが宣言される**ので、上界 ε を必ず数値で見せる。
    /// 打ち切りは形状オブジェクトの状態なので、run 後の <see cref="AlchemiResult.ChannelData"/> から読む。</summary>
    private string TruncationNotice()
    {
        double bound = 0;
        var hit = new List<string>();
        foreach (var d in result.ChannelData)
        {
            //BetheMethod.cs が StemSignalMap へ配線しているのと同じ場合分け
            var (truncated, eps) = d.Shape switch
            {
                IonizationTableShape ts => (ts.TruncatedBeyondSMax, ts.TruncationBound),
                IonizationLTotalShape ls => (ls.TruncatedBeyondSMax, ls.TruncationBound),
                _ => (false, 0.0),
            };
            if (!truncated) continue;
            hit.Add(d.Target.ShortLabel);
            bound = Math.Max(bound, eps);
        }
        return hit.Count == 0 ? "" : "   ⚠ " + Localization.Loc(
            en: "F(s) truncated to zero beyond the certified range for {0} (bound |F| <= {1})",
            ja: "{0} で保証範囲の外の F(s) を 0 で打ち切りました (上界 |F| ≤ {1})",
            de: "F(s) jenseits des zertifizierten Bereichs für {0} auf null gesetzt (Schranke |F| <= {1})",
            fr: "F(s) tronqué à zéro au-delà de la plage certifiée pour {0} (borne |F| <= {1})",
            es: "F(s) truncado a cero más allá del rango certificado para {0} (cota |F| <= {1})",
            pt: "F(s) truncado a zero além da faixa certificada para {0} (limite |F| <= {1})",
            it: "F(s) troncato a zero oltre l'intervallo certificato per {0} (limite |F| <= {1})",
            ru: "F(s) обнулён за пределами гарантированного диапазона для {0} (граница |F| <= {1})",
            zhHans: "{0} 在保证范围之外的 F(s) 已截断为零（上界 |F| ≤ {1}）",
            zhHant: "{0} 在保證範圍之外的 F(s) 已截斷為零（上界 |F| ≤ {1}）",
            ko: "{0} 에서 보증 범위 밖의 F(s) 를 0 으로 절단했습니다 (상계 |F| ≤ {1})")
            .Replace("{0}", string.Join(", ", hit)).Replace("{1}", bound.ToString("e1"));
    }

    private IEnumerable<PointD> BraggPositions(double thetaB, bool inThetaB)
    {
        var maxN = (int)(result.Orientations[^1].TiltRad / thetaB);
        for (int n = -maxN; n <= maxN; n++)
            yield return new PointD(inThetaB ? n : n * thetaB * 1e3, double.NaN);
    }

    private double[] Normalize(double[] y) => comboBoxNormalization.SelectedIndex switch
    {
        0 => y.Average() > 0 ? [.. y.Select(v => v / y.Average())] : y,
        1 => y.Max() > 0 ? [.. y.Select(v => v / y.Max())] : y,
        _ => y,
    };

    private static double Correlation(double[] x, double[] y)
    {
        double mx = x.Average(), my = y.Average(), sxy = 0, sxx = 0, syy = 0;
        for (int i = 0; i < x.Length; i++)
        {
            sxy += (x[i] - mx) * (y[i] - my);
            sxx += (x[i] - mx) * (x[i] - mx);
            syy += (y[i] - my) * (y[i] - my);
        }
        return sxx > 0 && syy > 0 ? sxy / Math.Sqrt(sxx * syy) : 0;
    }

    #endregion

    #region CSV 出力

    private void buttonExport_Click(object sender, EventArgs e)
    {
        if (result == null || saveFileDialog.ShowDialog() != DialogResult.OK) return;
        var s = result.Shape;
        var sb = new StringBuilder();
        //自己吸収・検出器効率は未適用 (設計 §3.7)。生値であることをヘッダに残す
        sb.AppendLine($"# ReciPro ALCHEMI, {result.IncidentEnergyKeV:f1} kV, row ({resultRow.H} {resultRow.K} {resultRow.L}), "
            + $"theta_B {resultThetaB * 1e3:f4} mrad, model {result.ModelTier}, "
            + $"quantity {result.Quantity}, normalization {result.Normalization} (self-absorption and detector efficiency are NOT applied)");
        sb.AppendLine($"# basis {result.Basis.BeamCount} beams, hash {result.Basis.BasisHash}, "
            + $"expanded-basis {result.Basis.ExpandedBasisMaxRelDiff:e3}, fit-eligible {result.Basis.AcceptedForFit}");
        sb.Append("tilt_mrad,thickness_nm,site,channel,dynamic,dechannelled,total");
        sb.AppendLine();
        for (int o = 0; o < s.OrientationCount; o++)
            for (int t = 0; t < s.ThicknessCount; t++)
                for (int si = 0; si < s.SiteCount; si++)
                    for (int c = 0; c < s.ChannelCount; c++)
                    {
                        var i = s.Index(o, t, si, c);
                        sb.Append((result.Orientations[o].TiltRad * 1e3).ToString("f6", CultureInfo.InvariantCulture)).Append(',')
                          .Append(result.ThicknessesNm[t].ToString("f4", CultureInfo.InvariantCulture)).Append(',')
                          .Append(result.Sites[si].Label).Append(',')
                          .Append(result.ChannelData[c].Target.ShortLabel).Append(',')
                          .Append(result.Dynamic[i].ToString("e8", CultureInfo.InvariantCulture)).Append(',')
                          .Append(result.Dechannelled[i].ToString("e8", CultureInfo.InvariantCulture)).Append(',')
                          .Append(result.Total[i].ToString("e8", CultureInfo.InvariantCulture)).AppendLine();
                    }
        File.WriteAllText(saveFileDialog.FileName, sb.ToString());
    }

    #endregion
}
