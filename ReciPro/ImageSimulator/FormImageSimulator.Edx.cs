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

    /// <summary>項目 index → 候補。CheckedListBox の表示文字列は翻訳されるので、request はこちらから組む</summary>
    private List<EdxCandidate> edxCandidates = [];

    private bool edxSkipEvent;

    /// <summary>ToolTip を出している項目 index (同じ項目で SetToolTip を繰り返すと点滅するため)</summary>
    private int edxToolTipIndex = -1;

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

    /// <summary>1 項目の表示文。吸収端・過電圧・利用不可の理由を 1 行に畳む (CheckedListBox は列を持たないため)。</summary>
    private static string EdxItemText(EdxCandidate c)
    {
        var text = $"{AtomStatic.AtomicName(c.Spec.Z)} ({c.Spec.Z}) {EdxShellText(c.Spec.Shell)}";
        if (!double.IsNaN(c.Info.EdgeEnergyKeV)) text += $"   {c.Info.EdgeEnergyKeV:f3} keV";
        if (!double.IsNaN(c.Info.Overvoltage)) text += $"   U = {c.Info.Overvoltage.ToString(c.Info.Overvoltage < 100 ? "f2" : "f0")}";
        var status = EdxStatusText(c.Info);
        return status.Length == 0 ? text : $"{text}   ({status})";
    }

    /// <summary>候補一覧を作り直す。結晶・加速電圧の変更時に呼ぶ。</summary>
    public void RenewEdxChannelList()
    {
        if (checkedListBoxEdxChannels is null) return;
        edxSkipEvent = true;
        try
        {
            var candidates = EnumerateEdxCandidates();
            //収録外になったチャネルは選択から落とす (別結晶へプリセット適用したときの積集合。§5.9.1-6)
            edxSelected.RemoveWhere(s => !candidates.Any(c => c.Spec == s));

            edxCandidates = candidates;//項目 index → 候補 (翻訳済み表示文字列から request を復元しないため)
            checkedListBoxEdxChannels.Items.Clear();
            foreach (var c in candidates)
                checkedListBoxEdxChannels.Items.Add(EdxItemText(c), edxSelected.Contains(c.Spec));
        }
        finally { edxSkipEvent = false; }
        RenewEdxSummary();
    }

    /// <summary>選択数・チャネル要約・probe grid 警告を更新する (実行時文字列)。</summary>
    public void RenewEdxSummary()
    {
        if (labelEdxSummary is null) return;

        var names = edxCandidates.Where(c => edxSelected.Contains(c.Spec))
            .Select(c => $"{AtomStatic.AtomicName(c.Spec.Z)}-{(c.Spec.Shell == IonizationShell.LTotal ? "L" : "K")}").ToList();

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

    /// <summary>run 開始前の検証 (§5.9.1-7: 判定は GUI のモードではなく「これから投げる要求」に対して行う)。
    /// 続行してよければ true。チャネル 0 件は hard block、div 不足は確認ダイアログ (実行自体は可能)。</summary>
    private bool ValidateEdxRequest(StemIonizationRequest[] requests, int division)
    {
        if (requests is null) return true;// EDX なしの通常 STEM run

        if (requests.Length == 0)
        {
            MessageBox.Show(
                Loc(en: "STEM-EDX is enabled but no channel is selected. Select at least one element/shell, or clear the checkbox.",
                    ja: "STEM-EDX が有効ですがチャネルが 1 つも選択されていません。元素・殻を選ぶか、チェックを外してください。",
                    de: "STEM-EDX ist aktiviert, aber es ist kein Kanal ausgewählt. Wählen Sie mindestens ein Element/eine Schale oder deaktivieren Sie das Kontrollkästchen.",
                    fr: "STEM-EDX est activé mais aucun canal n'est sélectionné. Choisissez au moins un élément/couche ou décochez la case.",
                    es: "STEM-EDX está activado pero no hay ningún canal seleccionado. Elija al menos un elemento/capa o desmarque la casilla.",
                    pt: "O STEM-EDX está ativado mas não há nenhum canal selecionado. Escolha pelo menos um elemento/camada ou desmarque a caixa.",
                    it: "STEM-EDX è attivo ma non è selezionato alcun canale. Scegliere almeno un elemento/guscio oppure deselezionare la casella.",
                    ru: "STEM-EDX включён, но не выбран ни один канал. Выберите хотя бы один элемент/оболочку или снимите флажок.",
                    zhHans: "已启用 STEM-EDX，但未选择任何通道。请至少选择一个元素/壳层，或取消勾选。",
                    zhHant: "已啟用 STEM-EDX，但未選擇任何通道。請至少選擇一個元素/殼層，或取消勾選。",
                    ko: "STEM-EDX 가 활성화되어 있지만 선택된 채널이 없습니다. 원소/껍질을 하나 이상 선택하거나 체크를 해제하세요."),
                "STEM-EDX", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        if (division < EdxRecommendedDivision)
        {
            //±q Hermitian 残差は div に対し O(h²) なので、粗いグリッドでは backend が §3.4 で hard fail する。
            //ユーザーの角度分解能を黙って書き換えず、続行するかだけ尋ねる (§5.9.1-3)
            var msg = Loc(
                en: "The probe grid is {0} x {0}, below the {1} recommended for STEM-EDX.\r\nThe +/-q Hermitian residual may exceed the tolerance and abort the run.\r\nContinue anyway?",
                ja: "プローブ分割数が {0} × {0} で、STEM-EDX の推奨値 {1} を下回っています。\r\n±q の Hermitian 残差が許容値を超え、計算が中断される可能性があります。\r\nこのまま続行しますか?",
                de: "Das Sondenraster ist {0} x {0} und liegt unter den für STEM-EDX empfohlenen {1}.\r\nDas ±q-Hermitesche Residuum kann die Toleranz überschreiten und den Lauf abbrechen.\r\nTrotzdem fortfahren?",
                fr: "La grille de sonde est {0} x {0}, en dessous de {1} recommandé pour STEM-EDX.\r\nLe résidu hermitien ±q peut dépasser la tolérance et interrompre le calcul.\r\nContinuer quand même ?",
                es: "La rejilla de sonda es {0} x {0}, por debajo de {1} recomendado para STEM-EDX.\r\nEl residuo hermítico ±q puede superar la tolerancia y abortar el cálculo.\r\n¿Continuar de todos modos?",
                pt: "A grelha de sonda é {0} x {0}, abaixo dos {1} recomendados para STEM-EDX.\r\nO resíduo hermitiano ±q pode exceder a tolerância e abortar o cálculo.\r\nContinuar mesmo assim?",
                it: "La griglia della sonda è {0} x {0}, sotto i {1} consigliati per STEM-EDX.\r\nIl residuo hermitiano ±q può superare la tolleranza e interrompere il calcolo.\r\nContinuare comunque?",
                ru: "Сетка зонда {0} x {0}, меньше рекомендованных для STEM-EDX {1}.\r\nЭрмитов остаток ±q может превысить допуск и прервать расчёт.\r\nВсё равно продолжить?",
                zhHans: "探针网格为 {0} × {0}，低于 STEM-EDX 建议的 {1}。\r\n±q 厄米残差可能超出容差并中断计算。\r\n仍要继续吗？",
                zhHant: "探針網格為 {0} × {0}，低於 STEM-EDX 建議的 {1}。\r\n±q 厄米殘差可能超出容差並中斷計算。\r\n仍要繼續嗎？",
                ko: "프로브 격자가 {0} × {0} 로 STEM-EDX 권장값 {1} 보다 작습니다.\r\n±q 에르미트 잔차가 허용값을 초과하여 계산이 중단될 수 있습니다.\r\n계속하시겠습니까?")
                .Replace("{0}", division.ToString()).Replace("{1}", EdxRecommendedDivision.ToString());
            if (MessageBox.Show(msg, "STEM-EDX", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                return false;
        }
        return true;
    }

    #endregion

    /// <summary>260801Cl 追加 (§5.9.1-4): 検出器内外角は STEM 参照像 (ADF/弾性/TDS) 専用で EDX マップには使われない。
    /// EDX 時に無効化するのではなく、所属を静的に明示する (無効化すると Reference ADF を隠れた値で計算することになる)。
    /// Designer 側のツールチップに 1 度だけ追記する (Load から呼ぶ)。</summary>
    private void AppendDetectorAngleNote()
    {
        var note = Loc(
            en: "Applies to the STEM reference image (ADF / elastic / TDS) only — not used for STEM-EDX maps.",
            ja: "STEM 参照像 (ADF / 弾性 / TDS) にのみ適用されます。STEM-EDX マップには使われません。",
            de: "Gilt nur für das STEM-Referenzbild (ADF / elastisch / TDS) — nicht für STEM-EDX-Karten.",
            fr: "S'applique uniquement à l'image STEM de référence (ADF / élastique / TDS), pas aux cartes STEM-EDX.",
            es: "Se aplica solo a la imagen STEM de referencia (ADF / elástica / TDS); no se usa en los mapas STEM-EDX.",
            pt: "Aplica-se apenas à imagem STEM de referência (ADF / elástica / TDS); não é usado nos mapas STEM-EDX.",
            it: "Si applica solo all'immagine STEM di riferimento (ADF / elastica / TDS), non alle mappe STEM-EDX.",
            ru: "Относится только к опорному STEM-изображению (ADF / упругое / TDS) — для карт STEM-EDX не используется.",
            zhHans: "仅适用于 STEM 参考像 (ADF / 弹性 / TDS)，不用于 STEM-EDX 分布图。",
            zhHant: "僅適用於 STEM 參考影像 (ADF / 彈性 / TDS)，不用於 STEM-EDX 分布圖。",
            ko: "STEM 참조 이미지(ADF / 탄성 / TDS)에만 적용되며 STEM-EDX 맵에는 사용되지 않습니다.");
        foreach (var c in new System.Windows.Forms.Control[] { numericBoxSTEM_DetectorInnerAngle, numericBoxSTEM_DetectorOuterAngle })
        {
            var current = toolTip.GetToolTip(c);
            toolTip.SetToolTip(c, string.IsNullOrEmpty(current) ? note : current + "\r\n" + note);
        }
    }

    #region 結果表示

    /// <summary>表示中の EDX 信号 (ComboBox で「Reference」を選んでいる、または EDX 結果が無いときは null)。
    /// **チェック状態ではなく公開済み結果から**引く (未計算チャネルや旧 run を誤表示しない契約、§5.9.1-5)。</summary>
    private StemSignalMap SelectedEdxSignal
    {
        get
        {
            var signals = FormMain?.Crystal?.Bethe?.ResultStem?.EdxSignals;
            if (signals is null || signals.Length == 0) return null;
            var i = comboBoxEdxDisplay.SelectedIndex - 1;// index 0 = STEM reference
            return i >= 0 && i < signals.Length ? signals[i] : null;
        }
    }

    /// <summary>ComboBox を「公開済み結果に含まれる EDX 信号」で作り直す。run 完了時に呼ぶ。</summary>
    private void RenewEdxDisplayList()
    {
        var signals = FormMain?.Crystal?.Bethe?.ResultStem?.EdxSignals;
        var previous = comboBoxEdxDisplay.SelectedItem as string;
        edxSkipEvent = true;
        try
        {
            comboBoxEdxDisplay.Items.Clear();
            if (signals is null || signals.Length == 0)
            {
                comboBoxEdxDisplay.Visible = false;
                return;
            }
            //先頭は STEM 参照像 (Both/Elastic/TDS ラジオが効く方)。元素セレクタと違い、ここは表示切替なので参照像も並べる
            comboBoxEdxDisplay.Items.Add(Loc(en: "STEM reference", ja: "STEM 参照像", de: "STEM-Referenz", fr: "Référence STEM",
                es: "Referencia STEM", pt: "Referência STEM", it: "Riferimento STEM", ru: "Опорное STEM",
                zhHans: "STEM 参考像", zhHant: "STEM 參考影像", ko: "STEM 참조상"));
            foreach (var s in signals)
                comboBoxEdxDisplay.Items.Add($"{AtomStatic.AtomicName(s.Channel.Z)}-{(s.Channel.Shell == IonizationShell.LTotal ? "L" : "K")}");
            comboBoxEdxDisplay.Visible = true;
            var idx = previous is null ? -1 : comboBoxEdxDisplay.Items.IndexOf(previous);
            comboBoxEdxDisplay.SelectedIndex = idx >= 0 ? idx : 0;
        }
        finally { edxSkipEvent = false; }
    }

    private void ComboBoxEdxDisplay_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (edxSkipEvent) return;
        //参照像を選んでいるときだけ Both/Elastic/TDS が意味を持つ
        groupBoxSTEMoption3.Enabled = true;
        radioButtonSTEM_target_both.Enabled = radioButtonSTEM_target_elas.Enabled = radioButtonSTEM_target_TDS.Enabled = SelectedEdxSignal is null;
        GeneratePseudBitmap();
    }

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

    private void CheckedListBoxEdxChannels_ItemCheck(object sender, ItemCheckEventArgs e)
    {
        if (edxSkipEvent || e.Index < 0 || e.Index >= edxCandidates.Count) return;
        var c = edxCandidates[e.Index];
        if (c.Info.Status != IonizationAvailability.Available)
        {
            //利用不可のチャネルはチェックさせない (理由は項目テキストと ToolTip)
            e.NewValue = CheckState.Unchecked;
            return;
        }
        if (e.NewValue == CheckState.Checked)
            edxSelected.Add(c.Spec);
        else
            edxSelected.Remove(c.Spec);
        //ItemCheck は値が確定する前に来るので、要約更新は反映後へ回す
        BeginInvoke(RenewEdxSummary);
    }

    private void CheckedListBoxEdxChannels_MouseMove(object sender, MouseEventArgs e)
    {
        //CheckedListBox は項目ごとの ToolTip を持たないので、ホバー中の項目に合わせて差し替える
        var index = checkedListBoxEdxChannels.IndexFromPoint(e.Location);
        if (index == edxToolTipIndex) return;
        edxToolTipIndex = index;
        toolTip.SetToolTip(checkedListBoxEdxChannels,
            index >= 0 && index < edxCandidates.Count ? EdxStatusToolTip(edxCandidates[index].Info) : "");
    }

    private void ButtonEdxSelectAvailable_Click(object sender, System.EventArgs e) => SelectAvailableEdxChannels();

    /// <summary>利用可能な全チャネルを選択する (--capture / マクロからも使う)</summary>
    internal void SelectAvailableEdxChannels()
    {
        //「すべて選択」ではなく「利用可能なものをすべて選択」(below-edge・範囲外は選ばない)
        edxSkipEvent = true;
        try
        {
            for (int i = 0; i < edxCandidates.Count; i++)
                if (edxCandidates[i].Info.Status == IonizationAvailability.Available)
                {
                    edxSelected.Add(edxCandidates[i].Spec);
                    checkedListBoxEdxChannels.SetItemChecked(i, true);
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
            for (int i = 0; i < checkedListBoxEdxChannels.Items.Count; i++)
                checkedListBoxEdxChannels.SetItemChecked(i, false);
        }
        finally { edxSkipEvent = false; }
        RenewEdxSummary();
    }

    #endregion
}
