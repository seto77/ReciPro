// 260801Cl 新規作成: STEM-EDX 要求 UI のロジック (設計書 §5.9.1)。
// STEM-EDX は独立モードではなく「STEM run の追加出力オプション」なので、判定はすべて
// 「ImageMode==STEM かつ checkBoxCalculateEdx.Checked」であり、ImageModes enum は増やさない。
// 表・ボタン・ラベルの配置は Designer.cs 側 (作者方針: GUI 配置は Designer 内で完結)。
// ここが持つのは行データの生成と実行時文字列 (Localization.Loc で 11 言語化) のみ。
#region using
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static Crystallography.Localization;
#endregion

namespace ReciPro;

public partial class FormImageSimulator
{
    /// <summary>候補チャネル 1 行分 (表示文字列から request を復元しないよう、行 Tag に spec 実体を持たせる)</summary>
    private sealed record EdxCandidate(IonizationChannelSpec Spec, IonizationChannelInfo Info);

    /// <summary>結晶変更・E0 変更をまたいでチェック状態を保つための選択集合 ((Z,Shell) で保持、表示文字列で照合しない)</summary>
    private readonly HashSet<IonizationChannelSpec> edxSelected = [];

    private bool edxSkipEvent;

    /// <summary>STEM-EDX を計算するか (ImageMode==STEM が前提)。GUI 判定はすべてこの 1 か所を経由する</summary>
    public bool EdxEnabled
    {
        get => ImageMode == ImageModes.STEM && checkBoxCalculateEdx.Checked;
        set => checkBoxCalculateEdx.Checked = value;
    }

    /// <summary>--capture 用: 元素×殻セレクタの GroupBox (スクロール下端に来て全体像に写らないため単体で撮る)</summary>
    internal System.Windows.Forms.Control EdxOptionGroup => groupBoxSTEMoption4;

    /// <summary>選択中チャネル ((Z,Shell) 集合の防御的コピー。プリセット保存・復元に使う)</summary>
    public IonizationChannelSpec[] EdxChannels
    {
        get => [.. edxSelected.OrderBy(s => s.Z).ThenBy(s => s.Shell)];
        set
        {
            edxSelected.Clear();
            if (value is not null)
                foreach (var s in value) edxSelected.Add(s);
            RenewEdxChannelList();
        }
    }

    #region 候補列挙と表の更新

    /// <summary>現在の結晶と加速電圧から候補チャネルを列挙する。Z 範囲などのデータ収録条件は
    /// GUI に持たず Crystallography.dll の Inspect に問い合わせる (設計書 §5.9-3)。</summary>
    private List<EdxCandidate> EnumerateEdxCandidates()
    {
        var list = new List<EdxCandidate>();
        var crystal = FormMain?.Crystal;
        if (crystal?.Atoms is null) return list;
        foreach (var z in crystal.Atoms.Select(a => a.AtomicNumber).Distinct().OrderBy(z => z))
            foreach (var shell in new[] { IonizationShell.K, IonizationShell.LTotal })
            {
                var spec = new IonizationChannelSpec(z, shell);
                var info = IonizationDataProvider.Inspect(spec, AccVol);
                if (info.Status == IonizationAvailability.UnsupportedElement || info.Status == IonizationAvailability.UnsupportedShell)
                    continue;// 収録外の元素/殻は候補に出さない (E0 範囲外・below-edge は理由付きで出す)
                list.Add(new EdxCandidate(spec, info));
            }
        return list;
    }

    /// <summary>状態 enum → Status 列の短い表示文 (11 言語)。列幅が狭いので、正常 (Available かつ U 十分) は
    /// 「注記なし」= 空文字にし、注意が要る行だけ語を出す。詳細理由はセル ToolTip (<see cref="EdxStatusToolTip"/>)。
    /// 例外メッセージを UI に直接出さない契約 (§5.9-3)。</summary>
    private static string EdxStatusText(IonizationChannelInfo info) => info.Status switch
    {
        IonizationAvailability.Available when info.Overvoltage < 1.2 =>
            Loc(en: "low U", ja: "U 小", de: "U klein", fr: "U faible", es: "U baja", pt: "U baixa",
                it: "U bassa", ru: "малое U", zhHans: "U 偏低", zhHant: "U 偏低", ko: "U 낮음"),
        IonizationAvailability.Available => "",
        IonizationAvailability.BelowEdge =>
            Loc(en: "below edge", ja: "端以下", de: "unter Kante", fr: "sous seuil", es: "bajo borde",
                pt: "sob borda", it: "sotto soglia", ru: "ниже края", zhHans: "低于边", zhHant: "低於邊",
                ko: "단 이하"),
        IonizationAvailability.E0OutOfRange =>
            Loc(en: "E0 range", ja: "E0 範囲外", de: "E0-Bereich", fr: "plage E0", es: "rango E0", pt: "faixa E0",
                it: "intervallo E0", ru: "диапазон E0", zhHans: "E0 范围", zhHant: "E0 範圍", ko: "E0 범위"),
        _ => "",
    };

    /// <summary>Status 列セルの ToolTip = 状態の完全な説明 + provider 品質タグ (§5.6)。</summary>
    private static string EdxStatusToolTip(IonizationChannelInfo info)
    {
        var head = info.Status switch
        {
            IonizationAvailability.Available when info.Overvoltage < 1.2 =>
                Loc(en: "Available, but the overvoltage U = E0/E_edge is below 1.2 — the cross section is less reliable there.",
                    ja: "利用可能ですが過電圧 U = E0/E_edge が 1.2 未満です。この領域は断面積の信頼度が下がります。",
                    de: "Verfügbar, aber die Überspannung U = E0/E_Kante liegt unter 1,2 — der Wirkungsquerschnitt ist dort weniger zuverlässig.",
                    fr: "Disponible, mais le survoltage U = E0/E_seuil est inférieur à 1,2 : la section efficace y est moins fiable.",
                    es: "Disponible, pero la sobretensión U = E0/E_borde es menor que 1,2: la sección eficaz es menos fiable ahí.",
                    pt: "Disponível, mas a sobretensão U = E0/E_borda é inferior a 1,2: a secção eficaz é menos fiável aí.",
                    it: "Disponibile, ma la sovratensione U = E0/E_soglia è inferiore a 1,2: la sezione d'urto è meno affidabile.",
                    ru: "Доступно, но перенапряжение U = E0/E_края меньше 1,2 — сечение там менее надёжно.",
                    zhHans: "可用，但过电压 U = E0/E_边 低于 1.2，该区域的截面可靠性较低。",
                    zhHant: "可用，但過電壓 U = E0/E_邊 低於 1.2，該區域的截面可靠性較低。",
                    ko: "사용 가능하지만 과전압 U = E0/E_단 이 1.2 미만입니다. 이 영역은 단면적 신뢰도가 낮습니다."),
            IonizationAvailability.Available =>
                Loc(en: "Available.", ja: "利用可能です。", de: "Verfügbar.", fr: "Disponible.", es: "Disponible.",
                    pt: "Disponível.", it: "Disponibile.", ru: "Доступно.", zhHans: "可用。", zhHant: "可用。", ko: "사용 가능합니다."),
            IonizationAvailability.BelowEdge =>
                Loc(en: "The incident energy is below the absorption edge, so this shell cannot be ionized.",
                    ja: "入射エネルギーが吸収端より低いため、この殻はイオン化されません。",
                    de: "Die Primärenergie liegt unter der Absorptionskante, diese Schale kann nicht ionisiert werden.",
                    fr: "L'énergie incidente est inférieure au seuil d'absorption : cette couche ne peut pas être ionisée.",
                    es: "La energía incidente está por debajo del borde de absorción: esta capa no puede ionizarse.",
                    pt: "A energia incidente está abaixo da borda de absorção: esta camada não pode ser ionizada.",
                    it: "L'energia incidente è sotto la soglia di assorbimento: questo guscio non può essere ionizzato.",
                    ru: "Энергия пучка ниже края поглощения, поэтому эта оболочка не ионизируется.",
                    zhHans: "入射能量低于吸收边，该壳层无法被电离。",
                    zhHant: "入射能量低於吸收邊，該殼層無法被游離。",
                    ko: "입사 에너지가 흡수단보다 낮아 이 껍질은 이온화되지 않습니다."),
            IonizationAvailability.E0OutOfRange =>
                Loc(en: "STEM-EDX supports 30-400 kV only (the ionization form-factor table is not extrapolated).",
                    ja: "STEM-EDX は 30-400 kV のみ対応です (イオン化形状因子テーブルを外挿しないため)。",
                    de: "STEM-EDX unterstützt nur 30-400 kV (die Tabelle der Ionisationsformfaktoren wird nicht extrapoliert).",
                    fr: "STEM-EDX ne prend en charge que 30-400 kV (la table des facteurs de forme d'ionisation n'est pas extrapolée).",
                    es: "STEM-EDX solo admite 30-400 kV (la tabla de factores de forma de ionización no se extrapola).",
                    pt: "O STEM-EDX suporta apenas 30-400 kV (a tabela de fatores de forma de ionização não é extrapolada).",
                    it: "STEM-EDX supporta solo 30-400 kV (la tabella dei fattori di forma di ionizzazione non viene estrapolata).",
                    ru: "STEM-EDX поддерживает только 30-400 кВ (таблица форм-факторов ионизации не экстраполируется).",
                    zhHans: "STEM-EDX 仅支持 30-400 kV（电离形状因子表不做外推）。",
                    zhHant: "STEM-EDX 僅支援 30-400 kV（游離形狀因子表不做外推）。",
                    ko: "STEM-EDX 는 30-400 kV 만 지원합니다 (이온화 형상 인자 표를 외삽하지 않음)."),
            _ => "",
        };
        return info.ShapeSource is null ? head : $"{head}\r\nσ: {info.CrossSectionSource.ModelId} / F(s): {info.ShapeSource.ModelId} {info.ShapeSource.DatasetVersion}";
    }

    private static string EdxShellText(IonizationShell shell) => shell == IonizationShell.LTotal ? "L (total)" : shell.ToString();

    /// <summary>候補表を作り直す。結晶・加速電圧・収束角・角度分解能の変更時に呼ぶ。</summary>
    public void RenewEdxChannelList()
    {
        if (dataGridViewEdxChannels is null) return;
        edxSkipEvent = true;
        try
        {
            var candidates = EnumerateEdxCandidates();
            //収録外になったチャネルは選択から落とす (別結晶へプリセット適用したときの積集合。§5.9.1-6)
            edxSelected.RemoveWhere(s => !candidates.Any(c => c.Spec == s));

            dataGridViewEdxChannels.Rows.Clear();
            foreach (var c in candidates)
            {
                //260801Cl: 元素+殻は 1 列 ("O (8) K")。列数を減らして Status 列の幅を確保するため
                var idx = dataGridViewEdxChannels.Rows.Add(
                    edxSelected.Contains(c.Spec),
                    $"{AtomStatic.AtomicName(c.Spec.Z)} ({c.Spec.Z}) {EdxShellText(c.Spec.Shell)}",
                    double.IsNaN(c.Info.EdgeEnergyKeV) ? "" : c.Info.EdgeEnergyKeV.ToString("f3"),
                    double.IsNaN(c.Info.Overvoltage) ? "" : c.Info.Overvoltage.ToString(c.Info.Overvoltage < 100 ? "f2" : "f0"),
                    EdxStatusText(c.Info));
                var row = dataGridViewEdxChannels.Rows[idx];
                row.Tag = c;
                if (c.Info.Status != IonizationAvailability.Available)
                {
                    //選択不能行はチェックセルを ReadOnly + 灰色 (理由は Status 列とセル ToolTip)
                    row.Cells[0].ReadOnly = true;
                    row.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.GrayText;
                }
                else if (c.Info.Overvoltage < 1.2)
                    //U<1.2 は選択可能な警告 (断面積の信頼度が落ちる領域)
                    row.Cells[4].Style.ForeColor = System.Drawing.Color.DarkOrange;
                row.Cells[4].ToolTipText = EdxStatusToolTip(c.Info);
            }
        }
        finally { edxSkipEvent = false; }
        RenewEdxSummary();
    }

    /// <summary>選択数・チャネル要約・probe grid 警告を更新する (実行時文字列)。</summary>
    public void RenewEdxSummary()
    {
        if (labelEdxSummary is null) return;

        var names = new List<string>();
        foreach (DataGridViewRow row in dataGridViewEdxChannels.Rows)
            if (row.Tag is EdxCandidate c && edxSelected.Contains(c.Spec))
                names.Add($"{AtomStatic.AtomicName(c.Spec.Z)}-{(c.Spec.Shell == IonizationShell.LTotal ? "L" : "K")}");

        labelEdxSummary.Text = names.Count == 0
            ? Loc(en: "No channel selected", ja: "チャネル未選択", de: "Kein Kanal ausgewählt", fr: "Aucun canal sélectionné",
                  es: "Ningún canal seleccionado", pt: "Nenhum canal selecionado", it: "Nessun canale selezionato",
                  ru: "Канал не выбран", zhHans: "未选择通道", zhHant: "未選擇通道", ko: "채널이 선택되지 않음")
            : Loc(en: "{0} map(s): {1}", ja: "{0} 個のマップ: {1}", de: "{0} Karte(n): {1}", fr: "{0} carte(s) : {1}",
                  es: "{0} mapa(s): {1}", pt: "{0} mapa(s): {1}", it: "{0} mappa/e: {1}", ru: "{0} карт: {1}",
                  zhHans: "{0} 张图: {1}", zhHant: "{0} 張圖: {1}", ko: "{0} 개 맵: {1}")
              .Replace("{0}", names.Count.ToString()).Replace("{1}", string.Join(", ", names));

        //probe grid: simulateSTEM と同一式で division を出す (表示と実行の食い違いを作らない)
        var division = EdxProbeDivision();
        var recommended = Loc(en: "Recommended for STEM-EDX: division >= 48", ja: "STEM-EDX 推奨: 分割数 48 以上",
            de: "Empfohlen für STEM-EDX: Teilung >= 48", fr: "Recommandé pour STEM-EDX : division >= 48",
            es: "Recomendado para STEM-EDX: división >= 48", pt: "Recomendado para STEM-EDX: divisão >= 48",
            it: "Consigliato per STEM-EDX: divisione >= 48", ru: "Рекомендуется для STEM-EDX: деление >= 48",
            zhHans: "STEM-EDX 建议：分割数 >= 48", zhHant: "STEM-EDX 建議：分割數 >= 48", ko: "STEM-EDX 권장: 분할 수 48 이상");
        labelEdxProbeGrid.Text = $"Probe grid: {division} × {division}\r\n{recommended}";
        labelEdxProbeGrid.ForeColor = division < EdxRecommendedDivision
            ? System.Drawing.Color.DarkOrange
            : System.Drawing.SystemColors.ControlText;
    }

    /// <summary>±q Hermitian 残差が実測で許容 0.01 に十分収まる方向グリッド分割数 (設計書 §5.8-1 の実測: div=32 で 0.009、48 で 0.0017)</summary>
    private const int EdxRecommendedDivision = 48;

    /// <summary>simulateSTEM と同一の division 計算 (式を二重化しない)</summary>
    private int EdxProbeDivision()
        => (int)System.Math.Ceiling(numericBoxSTEM_ConvergenceAngle.Value * 2 * 1.05 / numericBoxSTEM_AngleResolution.Value);

    /// <summary>選択チャネルを backend 要求へ変換する。EDX OFF なら null (= EDX なし run)。</summary>
    private StemIonizationRequest[] BuildEdxRequests()
        => !EdxEnabled ? null : [.. EdxChannels.Select(spec => new StemIonizationRequest(spec))];

    #endregion

    #region イベントハンドラ

    private void NumericBoxSTEM_AngleResolution_ValueChanged(object sender, System.EventArgs e)
    {
        if (checkBoxCalculateEdx is not null && checkBoxCalculateEdx.Checked) RenewEdxSummary();
    }

    private void CheckBoxCalculateEdx_CheckedChanged(object sender, System.EventArgs e)
    {
        panelEdxDetails.Visible = checkBoxCalculateEdx.Checked;
        if (checkBoxCalculateEdx.Checked)
            RenewEdxChannelList();
        RenewEdxSummary();
    }

    private void DataGridViewEdxChannels_CurrentCellDirtyStateChanged(object sender, System.EventArgs e)
    {
        //チェックボックス列は commit しないと CellValueChanged が来ない
        if (dataGridViewEdxChannels.IsCurrentCellDirty)
            dataGridViewEdxChannels.CommitEdit(DataGridViewDataErrorContexts.Commit);
    }

    private void DataGridViewEdxChannels_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        if (edxSkipEvent || e.RowIndex < 0 || e.ColumnIndex != 0) return;
        var row = dataGridViewEdxChannels.Rows[e.RowIndex];
        if (row.Tag is not EdxCandidate c) return;
        if (row.Cells[0].Value is true)
            edxSelected.Add(c.Spec);
        else
            edxSelected.Remove(c.Spec);
        RenewEdxSummary();
    }

    private void ButtonEdxSelectAvailable_Click(object sender, System.EventArgs e) => SelectAvailableEdxChannels();

    /// <summary>利用可能な全チャネルを選択する (--capture / マクロからも使う)</summary>
    internal void SelectAvailableEdxChannels()
    {
        //「すべて選択」ではなく「利用可能なものをすべて選択」(below-edge・範囲外は選ばない)
        edxSkipEvent = true;
        try
        {
            foreach (DataGridViewRow row in dataGridViewEdxChannels.Rows)
                if (row.Tag is EdxCandidate c && c.Info.Status == IonizationAvailability.Available)
                {
                    edxSelected.Add(c.Spec);
                    row.Cells[0].Value = true;
                }
        }
        finally { edxSkipEvent = false; }
        RenewEdxSummary();
    }

    private void ButtonEdxClear_Click(object sender, System.EventArgs e)
    {
        edxSkipEvent = true;
        try
        {
            edxSelected.Clear();
            foreach (DataGridViewRow row in dataGridViewEdxChannels.Rows)
                if (!row.Cells[0].ReadOnly) row.Cells[0].Value = false;
        }
        finally { edxSkipEvent = false; }
        RenewEdxSummary();
    }

    #endregion
}
