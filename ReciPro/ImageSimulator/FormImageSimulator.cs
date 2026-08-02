#region
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using static Crystallography.Localization;//260802Cl 追加: STEM-EDX の実行時文字列を 11 言語化 (方式②)
using static System.Math;
#endregion

namespace ReciPro;

public partial class FormImageSimulator : FormBase
{
    #region プロパティ

    public bool PresetVisible { get => checkBoxPreset.Checked; set => checkBoxPreset.Checked = value; }

    public bool CTFVisible { get => checkBoxCTF.Checked; set => checkBoxCTF.Checked = value; }

    public ImageSimulatorSetting Setting { get => new("", this); set => value.Apply(this); }
    public bool Native => toolStripComboBoxCaclulationLibrary.SelectedIndex == 0;

    public ImageModes ImageMode
    {
        get => radioButtonHRTEM.Checked ? ImageModes.HRTEM : radioButtonProjectedPotential.Checked ? ImageModes.POTENTIAL : ImageModes.STEM;
        set
        {
            if (value == ImageModes.HRTEM)
                radioButtonHRTEM.Checked = true;
            else if (value == ImageModes.POTENTIAL)
                radioButtonProjectedPotential.Checked = true;
            else
                radioButtonSTEM.Checked = true;
        }
    }

    /// <summary>Bloch波の数</summary>
    public int BlochNum { get => numericBoxNumOfBlochWave.ValueInteger; set => numericBoxNumOfBlochWave.Value = value; }

    /// <summary>試料の厚み (nm) (シリアルモードではないとき)</summary>
    public double Thickness { get => numericBoxThickness.Value; set => numericBoxThickness.Value = value; }

    #region 電子顕微鏡の共通パラメータ

    /// <summary>電子の加速電圧 (kV)</summary>
    public double AccVol { get => numericBoxAccVol.Value; set => numericBoxAccVol.Value = value; }
    /// <summary>電子の波長 (nm)</summary>
    public double Lambda => UniversalConstants.Convert.EnergyToElectronWaveLength(AccVol);

    /// <summary>デフォーカス値 (nm) (シリアルモードではないとき)</summary>
    public double Defocus { get => numericBoxDefocus.Value; set => numericBoxDefocus.Value = value; }

    /// <summary>球面収差 Cs (nm) numericBoxCsで表示されているのはmm単位なので、1E6/1E-6 倍変換して get/set</summary>
    public double Cs { get => numericBoxCs.Value * 1E6; set => numericBoxCs.Value = value * 1E-6; }

    /// <summary>色収差 Cc (nm) numericBoxCcで表示されているのはmm単位なので、1E6/1E-6 倍変換して get/set</summary>
    public double Cc { get => numericBoxCc.Value * 1E6; set => numericBoxCc.Value = value * 1E-6; }

    /// <summary>電子の加速電圧の揺らぎの標準偏差 (kV)。numericBoxDeltaVの表示値は eV 単位の FWHM。DeltaVolFWHMで1000で割って eV→keV(=kV) とし、2 * Sqrt(2 * Log(2))（= 2·√(2·ln2) = 2.355、FWHM→σ の換算係数）で割って標準偏差に変換する (260521Cl コメント修正: 旧コメントは「1000倍してeV単位の標準偏差」と誤記)</summary>
    public double DeltaVol { get => DeltaVolFWHM / 2 / Sqrt(2 * Log(2)); set => DeltaVolFWHM = value * 2 * Sqrt(2 * Log(2)); }

    /// <summary>電子の加速電圧の揺らぎ FWHM (kV)</summary>
    public double DeltaVolFWHM { get => numericBoxDeltaV.Value / 1000; set => numericBoxDeltaV.Value = value * 1000; }

    /// <summary>Δ</summary>
    public double Delta => Cc * DeltaVol / AccVol;

    /// <summary>Scherzer focus (nm) getのみ</summary>
    public double Scherzer => Cs > 0 ? -Sqrt(4.0 / 3.0 * Cs * Lambda) : Sqrt(4.0 / 3.0 * -Cs * Lambda);

    #endregion

    private BetheMethod.Beam[] Beams { get; set; }

    private BetheMethod.Beam[] BeamsInside { get; set; }

    #region 計算する画像サイズ、解像度に関するプロパティ
    /// <summary>イメージの解像度 (nm/pix)</summary>
    public double ImageResolution { get => numericBoxResolution.Value / 1000.0; set => numericBoxResolution.Value = value * 1000.0; }

    /// <summary>イメージサイズ</summary>
    // 260521Cl: numericBoxWidth/Height → sizeControl1 へ置換 (Value は Size なので 1:1)
    //public Size ImageSize { get => new(numericBoxWidth.ValueInteger, numericBoxHeight.ValueInteger); set { numericBoxWidth.Value = value.Width; numericBoxHeight.Value = value.Height; } }
    public Size ImageSize { get => sizeControl1.Value; set => sizeControl1.Value = value; }
    #endregion

    # region シリアルモードのプロパティ

    public bool SingleImageMode { get => radioButtonSingleMode.Checked; set => radioButtonSingleMode.Checked = value; }
    public bool SerialImageMode { get => radioButtonSerialMode.Checked; set => radioButtonSerialMode.Checked = value; }
    public bool SerialImageWithThickness { get => checkBoxSerialThickness.Checked; set => checkBoxSerialThickness.Checked = value; }
    public bool SerialImageWithDefocus { get => checkBoxSerialDefocus.Checked; set => checkBoxSerialDefocus.Checked = value; }

    public double SerialImageThicknessStart { get => numericBoxThicknessStart.Value; set => numericBoxThicknessStart.Value = value; }
    public double SerialImageThicknessStep { get => numericBoxThicknessStep.Value; set => numericBoxThicknessStep.Value = value; }
    // public int SerialImageThicknessNum { get => numericBoxThicknessNum.ValueInteger; set => numericBoxThicknessStep.Value = value; } // (260414Ch) 旧実装: setter が個数ではなく step を書き換えていた
    public int SerialImageThicknessNum { get => numericBoxThicknessNum.ValueInteger; set => numericBoxThicknessNum.Value = value; }

    public double SerialImageDefocusStart { get => numericBoxDefocusStart.Value; set => numericBoxDefocusStart.Value = value; }
    public double SerialImageDefocusStep { get => numericBoxDefocusStep.Value; set => numericBoxDefocusStep.Value = value; }
    // public int SerialImageDefocusNum { get => numericBoxDefocusNum.ValueInteger; set => numericBoxDefocusStep.Value = value; } // (260414Ch) 旧実装: setter が個数ではなく step を書き換えていた
    public int SerialImageDefocusNum { get => numericBoxDefocusNum.ValueInteger; set => numericBoxDefocusNum.Value = value; }
    public double[] ThicknessArray
    {
        get
        {
            if (radioButtonSingleMode.Checked || !checkBoxSerialThickness.Checked)
                return [numericBoxThickness.Value];
            try
            {
                //260317Cl 変更: Convert.ToDouble → double.Parse
                return textBoxThicknessList.Text.Split(["\r\n"], StringSplitOptions.RemoveEmptyEntries).Select(str => double.Parse(str)).ToArray();
            }
            catch
            {
                MessageBox.Show("Values in Thickness list are invalid.");
                return null;
            }
        }
        set
        {
            if (value != null && value.Length > 0)
                textBoxThicknessList.Text = string.Join("\r\n", value);
        }
    }
    public double[] DefocusArray
    {
        get
        {
            if (radioButtonSingleMode.Checked || !checkBoxSerialDefocus.Checked)
                return [numericBoxDefocus.Value];
            try
            {
                return textBoxDefocusList.Text.Split(["\r\n"], StringSplitOptions.RemoveEmptyEntries).Select(str => Convert.ToDouble(str)).ToArray();
            }
            catch
            {
                MessageBox.Show("Values in Defocus list are invalid.");
                return null;
            }
        }
        set
        {
            if (value != null && value.Length > 0)
                textBoxDefocusList.Text = String.Join("\r\n", value);
        }
    }
    #endregion 

    #region 画像関連
    public bool UnitCellVisible { get => checkBoxShowUnitcell.Checked; set => checkBoxShowUnitcell.Checked = value; }
    public bool LabelVisible { get => checkBoxShowLabel.Checked; set => checkBoxShowLabel.Checked = value; }
    public int LabelSize { get => numericBoxLabelFontSize.ValueInteger; set => numericBoxLabelFontSize.Value = value; }
    public bool ScaleBarVisible { get => checkBoxShowScale.Checked; set => checkBoxShowScale.Checked = value; }
    public double ScaleBarLength { get => numericBoxScaleLength.Value; set => numericBoxScaleLength.Value = value; }

    public bool OverprintSymbols { get => toolStripMenuItemOverprintSymbols.Checked; set => toolStripMenuItemOverprintSymbols.Checked = value; }
    public bool SaveIndividually { get => toolStripMenuItemSaveIndividually.Checked; set => toolStripMenuItemSaveIndividually.Checked = value; }

    public bool GaussianBlurEnabled { get => checkBoxGaussianBlur.Checked; set => checkBoxGaussianBlur.Checked = value; }
    public double GaussianBlurFWHM { get => numericBoxGaussianBlurRadius.Value; set => numericBoxGaussianBlurRadius.Value = value; }
    #endregion

    #region HRTEM固有プロパティ
    public HRTEM_Modes HRTEM_Mode
    {
        get => radioButtonModeQuasiCoherent.Checked ? HRTEM_Modes.Quasi : HRTEM_Modes.TCC;
        set
        {
            if (value == HRTEM_Modes.Quasi)
                radioButtonModeQuasiCoherent.Checked = true;
            else
                radioButtonModeTransmissionCrossCoefficient.Checked = true;
        }
    }
    /// <summary>対物絞りのサイズ (rad)</summary>
    public double HRTEM_ObjAperRadius
    {
        get => checkBoxOpenAperture.Checked ? double.PositiveInfinity : numericBoxObjAperRadius.Value / 1000;
        set
        {
            if (double.IsPositiveInfinity(value))
                checkBoxOpenAperture.Checked = true;
            else
            {
                checkBoxOpenAperture.Checked = true;
                numericBoxObjAperRadius.Value = value * 1000;
            }
        }
    }

    /// <summary>対物絞りの中心位置X (rad)</summary>
    public double HRTEM_ObjAperX { get => numericBoxHRTEM_ObjAperX.Value / 1000; set => numericBoxHRTEM_ObjAperX.Value = value * 1000; }
    /// <summary>対物絞りの中心位置Y (rad)</summary>
    public double HRTEM_ObjAperY { get => numericBoxHRTEM_ObjAperY.Value / 1000; set => numericBoxHRTEM_ObjAperY.Value = value * 1000; }

    /// <summary>絞りの開放状態</summary>
    public bool HRTEM_OpenObjAper { get => checkBoxOpenAperture.Checked; set => checkBoxOpenAperture.Checked = value; }

    /// <summary>β (illumination semiangle) (rad)</summary>
    public double HRTEM_Beta { get => numericBoxHRTEM_BetaAgnle.Value / 1000; set => numericBoxHRTEM_BetaAgnle.Value = value * 1000; }

    #endregion

    #region STEMモード固有

    /// <summary>STEMモードの時のみ有効.実効的光源サイズ (nm単位)</summary>
    public double STEM_SourceSizeFWHM { get => numericBoxSTEM_EffectiveSourceSize.Value / 1000; set => numericBoxSTEM_EffectiveSourceSize.Value = value * 1000; }

    /// <summary>STEMモードの時のみ有効. 実効光源サイズ (nm) (STEM計算に必要) 2 * Sqrt(2 * Log(2)) で割って、標準偏差に変換する</summary>
    public double STEM_SourceSizeSigma { get => STEM_SourceSizeFWHM / 2 / Sqrt(2 * Log(2)); set => STEM_SourceSizeFWHM = value * 2 * Sqrt(2 * Log(2)); }

    /// <summary>STEM検出器の内径角度 (rad)</summary>
    public double STEM_DetectorInnerAngle { get => numericBoxSTEM_DetectorInnerAngle.Value / 1000; set => numericBoxSTEM_DetectorInnerAngle.Value = value * 1000; }

    /// <summary>STEM検出器の外径角度 (rad)</summary>
    public double STEM_DetectorOuterAngle { get => numericBoxSTEM_DetectorOuterAngle.Value / 1000; set => numericBoxSTEM_DetectorOuterAngle.Value = value * 1000; }

    /// <summary>STEM時の収束角(rad)</summary>
    public double STEM_ConvergenceAngle { get => numericBoxSTEM_ConvergenceAngle.Value / 1000; set => numericBoxSTEM_ConvergenceAngle.Value = value * 1000; }

    /// <summary>STEMモードの時のみ有効. 収束ビームを分解する角度. Rad単位 (表示上は mrad なので1000倍に変換される)</summary>
    public double STEM_AngularResolution { get => numericBoxSTEM_AngleResolution.Value / 1000; set => numericBoxSTEM_AngleResolution.Value = value * 1000; }

    /// <summary>260802Cl 追加: プローブ方向グリッドの一辺の分割数。simulateSTEM が実際に使う値であり、
    /// STEM-EDX の事前警告表示も同じここを参照する (収束角を 1.05 倍しておくのは既存仕様)。</summary>
    private int StemProbeDivision()
        => (int)Ceiling(numericBoxSTEM_ConvergenceAngle.Value * 2 * 1.05 / numericBoxSTEM_AngleResolution.Value);

    /// <summary>STEMモードの時のみ有効. TDS計算の際の、サンプルのスライス厚み.　(nm単位)</summary>
    public double STEM_SliceThickness { get => numericBoxSTEM_SliceThicknessForInelastic.Value; set => numericBoxSTEM_SliceThicknessForInelastic.Value = value; }


    /// <summary>表示する STEM 信号。260802Cl 変更 (作者指示): EDX を 4 つ目として追加 (末尾追加なので既存値は不変)。
    /// EDX を選べるのは公開済み結果が EDX 信号を含むときだけ (それ以外は setter が無視する)。</summary>
    public STEM_ModeEnum STEM_Mode
    {
        get
        {
            if (radioButtonSTEM_target_both.Checked) return STEM_ModeEnum.Both;
            else if (radioButtonSTEM_target_elas.Checked) return STEM_ModeEnum.Elastic;
            else if (radioButtonSTEM_target_TDS.Checked) return STEM_ModeEnum.TDS;
            else return STEM_ModeEnum.EDX;
        }
        set
        {
            if (value == STEM_ModeEnum.Both) radioButtonSTEM_target_both.Checked = true;
            else if (value == STEM_ModeEnum.Elastic) radioButtonSTEM_target_elas.Checked = true;
            else if (value == STEM_ModeEnum.TDS) radioButtonSTEM_target_TDS.Checked = true;
            else if (EdxDisplayAvailable) radioButtonSTEM_target_EDX.Checked = true;//Enabled は実効値なので使わない
        }
    }
    #endregion

    #region STEM-EDX (STEM モード内の追加出力オプション。設計書 §5.9.1)
    //260801Cl 追加 / 260802Cl 変更: 当初 FormImageSimulator.Edx.cs に分けていたが、他フォームに partial を分割する例が
    //無く ReciPro の慣習に合わないため本体へ統合 (作者指示)。ドメイン側 (候補列挙・11 言語の状態文・推奨分割数) は
    //Crystallography の IonizationChannelInfo / IonizationDataProvider へ移した。ここに残るのは純粋に GUI の配線のみ。
    //STEM-EDX は独立モードではないので、判定はすべて「ImageMode==STEM かつ checkBoxCalculateEdx.Checked」で行う。

    /// <summary>項目 index → 候補。CheckedListBox の表示文字列は翻訳されるので、request は必ずこちらから組む</summary>
    private IonizationChannelInfo[] edxCandidates = [];

    /// <summary>候補一覧を作り直した最後の条件 (同じ結晶・同じ電圧での再構築を省く)</summary>
    private (Crystal Crystal, double AccVol) edxListKey;

    private bool edxSkipEvent;

    /// <summary>ToolTip を出している項目 index (同じ項目で SetToolTip を繰り返すと点滅するため)</summary>
    private int edxToolTipIndex = -1;

    /// <summary>260802Cl 追加: 候補一覧をまだ作れない段階 (起動直後 = 結晶未ロード) で
    /// プリセット・レジストリから復元された選択。候補が組めた時点で 1 度だけ消費する。
    /// これが無いと、レジストリ復元が「候補 0 件 → 選択も 0 件」で必ず空振りしていた。</summary>
    private (int Z, IonizationShell Shell)[] edxPendingChannels;

    /// <summary>STEM-EDX マップを要求するチェックの状態。
    /// 260802Cl 変更: getter から <c>ImageMode == ImageModes.STEM</c> の条件を外した (旧: 両方の AND)。
    /// プリセットはこのプロパティを保存するので、モードを含めると「HRTEM 表示中に保存したプリセットは
    /// EDX 要求を落とす」ことになり、§5.9.1-6 の「EdxEnabled とチャネル一覧は分離保持」に反していた。
    /// 実際に EDX を計算するかの判定 (モードとの AND) は <see cref="BuildEdxRequests"/> 側に置く。</summary>
    public bool EdxEnabled
    {
        get => checkBoxCalculateEdx.Checked;
        set => checkBoxCalculateEdx.Checked = value;
    }

    /// <summary>--capture 用: 元素×殻セレクタの GroupBox (スクロール下端に来て全体像に写らないため単体で撮る)</summary>
    internal Control EdxOptionGroup => groupBoxSTEMoption4;

    /// <summary>260802Cl 追加: --capture 用。run 完了後に表示信号を EDX へ切り替える。
    /// EDX を選べるようになるのは結果が公開されてからなので、Simulate を投げる前には設定できない。</summary>
    internal bool CaptureSelectEdxAfterRun;

    /// <summary>チェック済みチャネル。表示文字列ではなく候補配列から引く</summary>
    private IonizationChannelSpec[] CheckedEdxChannels
        => [.. checkedListBoxEdxChannels.CheckedIndices.Cast<int>()
            .Where(i => i < edxCandidates.Length).Select(i => edxCandidates[i].Channel)];

    /// <summary>260802Cl 追加: プリセット保存用の選択チャネル (設計書 §5.9.1-6)。EdxEnabled とは分離して持つので、
    /// 一時的にチェックを外しても選択そのものは失われない。record ではなく (Z, Shell) の平坦なタプル配列にしてあるのは、
    /// Crystallography の型に MemoryPack 属性を足さずに済ませるため (ValueTuple は MemoryPack 組み込み対応)。
    /// set は「別の結晶・加速電圧へ適用したときは積集合だけ復元する」= 収録外・端以下になったものは黙って落とす
    /// (勝手に別元素を選ばない。結果 0 件になったら実行前の ValidateEdxRequest が hard block する)。</summary>
    public (int Z, IonizationShell Shell)[] EdxChannels
    {
        get => checkedListBoxEdxChannels is null ? [] : [.. CheckedEdxChannels.Select(s => (s.Z, s.Shell))];
        set
        {
            if (checkedListBoxEdxChannels is null) return;
            //復元するものも外すものも無いなら、候補列挙 (元素ごとの Inspect = リソース展開) を走らせない
            if ((value is null || value.Length == 0) && checkedListBoxEdxChannels.CheckedIndices.Count == 0) return;
            //実際の反映は RenewEdxChannelList に任せる。起動直後は結晶が未ロードで候補が 0 件になり得るので、
            //その場合は保留しておき、候補が組めた時点 (= 結晶が入った時点) で消費する
            edxPendingChannels = value ?? [];
            RenewEdxChannelList();
        }
    }

    /// <summary>候補一覧を作り直す。結晶・加速電圧が変わったときだけ実際に組み直す。</summary>
    public void RenewEdxChannelList()
    {
        if (checkedListBoxEdxChannels is null) return;
        var key = (FormMain?.Crystal, AccVol);
        //260802Cl 変更: 早期 return を条件反転に (保留していたプリセット復元は、候補を組み直さない経路でも消費する必要がある)。
        //旧: if (edxCandidates.Length > 0 && key == edxListKey) { RenewEdxSummary(); return; }
        if (edxCandidates.Length == 0 || key != edxListKey)
        {
            //チェック状態は (Z,Shell) で覚えておく (項目の並びや表示文字列ではなく実体で復元する)
            var previous = new HashSet<IonizationChannelSpec>(CheckedEdxChannels);
            edxSkipEvent = true;
            try
            {
                edxCandidates = IonizationDataProvider.EnumerateChannels(FormMain?.Crystal, AccVol);
                edxListKey = key;
                checkedListBoxEdxChannels.BeginUpdate();
                checkedListBoxEdxChannels.Items.Clear();
                foreach (var info in edxCandidates)
                    //以前チェックされていても、電圧変更などで選べなくなった候補は復元しない (ItemCheck の拒否と辻褄を合わせる)
                    checkedListBoxEdxChannels.Items.Add(info.ToListItemText(),
                        info.Status == IonizationAvailability.Available && previous.Contains(info.Channel));
                checkedListBoxEdxChannels.EndUpdate();
            }
            finally { edxSkipEvent = false; }
        }

        //260802Cl 追加: プリセット・レジストリからの復元を、候補が組めた時点で 1 度だけ反映する (設計書 §5.9.1-6)。
        //収録外・端以下になったものは復元しない = 「別結晶へ適用したら積集合だけ」。空になったら実行前に hard block される
        if (edxPendingChannels is not null && edxCandidates.Length > 0)
        {
            var wanted = new HashSet<IonizationChannelSpec>(edxPendingChannels.Select(v => new IonizationChannelSpec(v.Z, v.Shell)));
            edxPendingChannels = null;
            edxSkipEvent = true;
            try
            {
                for (int i = 0; i < edxCandidates.Length; i++)
                    checkedListBoxEdxChannels.SetItemChecked(i,
                        edxCandidates[i].Status == IonizationAvailability.Available && wanted.Contains(edxCandidates[i].Channel));
            }
            finally { edxSkipEvent = false; }
        }
        RenewEdxSummary();
    }

    /// <summary>選択数・チャネル要約・probe grid 警告を更新する (実行時文字列)。</summary>
    public void RenewEdxSummary()
    {
        if (labelEdxSummary is null) return;

        var names = CheckedEdxChannels.Select(spec => spec.ShortLabel).ToArray();
        labelEdxSummary.Text = names.Length == 0
            ? Loc(en: "No channel selected", ja: "チャネル未選択", de: "Kein Kanal ausgewählt", fr: "Aucun canal sélectionné",
                  es: "Ningún canal seleccionado", pt: "Nenhum canal selecionado", it: "Nessun canale selezionato",
                  ru: "Канал не выбран", zhHans: "未选择通道", zhHant: "未選擇通道", ko: "채널이 선택되지 않음")
            : Loc(en: "{0} map(s): {1}", ja: "{0} 個のマップ: {1}", de: "{0} Karte(n): {1}", fr: "{0} carte(s) : {1}",
                  es: "{0} mapa(s): {1}", pt: "{0} mapa(s): {1}", it: "{0} mappa/e: {1}", ru: "{0} карт: {1}",
                  zhHans: "{0} 张图: {1}", zhHant: "{0} 張圖: {1}", ko: "{0} 개 맵: {1}")
              .Replace("{0}", names.Length.ToString()).Replace("{1}", string.Join(", ", names));

        var division = StemProbeDivision();
        var recommended = Loc(en: "Recommended for STEM-EDX: division >= 48", ja: "STEM-EDX 推奨: 分割数 48 以上",
            de: "Empfohlen für STEM-EDX: Teilung >= 48", fr: "Recommandé pour STEM-EDX : division >= 48",
            es: "Recomendado para STEM-EDX: división >= 48", pt: "Recomendado para STEM-EDX: divisão >= 48",
            it: "Consigliato per STEM-EDX: divisione >= 48", ru: "Рекомендуется для STEM-EDX: деление >= 48",
            zhHans: "STEM-EDX 建议：分割数 >= 48", zhHant: "STEM-EDX 建議：分割數 >= 48", ko: "STEM-EDX 권장: 분할 수 48 이상");
        labelEdxProbeGrid.Text = $"Probe grid: {division} × {division}\r\n{recommended}";
        labelEdxProbeGrid.ForeColor = division < StemIonizationRequest.RecommendedProbeDivision
            ? Color.DarkOrange : SystemColors.ControlText;
    }

    /// <summary>選択チャネルを backend 要求へ変換する。EDX OFF なら null (= EDX なし run)。</summary>
    private StemIonizationRequest[] BuildEdxRequests()
        //260802Cl 変更: モード判定をここへ移した (EdxEnabled はチェック状態そのものになったため)
        => ImageMode != ImageModes.STEM || !EdxEnabled ? null
            : [.. CheckedEdxChannels.OrderBy(s => s.Z).ThenBy(s => s.Shell).Select(s => new StemIonizationRequest(s))];

    /// <summary>run 開始前の検証 (§5.9.1-7: 判定は GUI のモードではなく「これから投げる要求」に対して行う)。
    /// 続行してよければ true。チャネル 0 件は hard block、div 不足は確認ダイアログ (実行自体は可能)。</summary>
    private static bool ValidateEdxRequest(StemIonizationRequest[] requests, int division)
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

        if (division < StemIonizationRequest.RecommendedProbeDivision)
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
                .Replace("{0}", division.ToString()).Replace("{1}", StemIonizationRequest.RecommendedProbeDivision.ToString());
            if (MessageBox.Show(msg, "STEM-EDX", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                return false;
        }
        return true;
    }

    /// <summary>利用可能な全チャネルを選択する (--capture / マクロからも使う)</summary>
    internal void SelectAvailableEdxChannels()
    {
        //「すべて選択」ではなく「利用可能なものをすべて選択」(below-edge・範囲外は選ばない)
        edxSkipEvent = true;
        try
        {
            for (int i = 0; i < edxCandidates.Length; i++)
                if (edxCandidates[i].Status == IonizationAvailability.Available)
                    checkedListBoxEdxChannels.SetItemChecked(i, true);
        }
        finally { edxSkipEvent = false; }
        RenewEdxSummary();
    }

    #region STEM-EDX 結果表示

    /// <summary>260802Cl 追加: 表示中の結果が EDX 信号を含むか (= 表示信号として EDX を選べるか)。
    /// **`radioButtonSTEM_target_EDX.Enabled` で代用しないこと**: Enabled の getter は親を含む実効値を返すので、
    /// 計算中 (splitContainer1.Enabled=false) は必ず false になる。</summary>
    private bool EdxDisplayAvailable => displayedStemResult?.EdxSignals is { Length: > 0 };

    /// <summary>表示する EDX 信号 = ComboBox で選んでいる特性 X 線 (EDX 結果が無ければ null)。
    /// **チェック状態ではなく公開済み結果から**引く (未計算チャネルや旧 run を誤表示しない契約、§5.9.1-5)。
    /// 260802Cl 変更: 参照元を表示中 snapshot へ (旧: 都度 FormMain.Crystal.Bethe.ResultStem を見ていた)。</summary>
    private StemSignalMap SelectedEdxSignal
    {
        get
        {
            var signals = displayedStemResult?.EdxSignals;
            if (signals is null || signals.Length == 0) return null;
            var i = comboBoxEdxDisplay.SelectedIndex;
            return i >= 0 && i < signals.Length ? signals[i] : null;
        }
    }

    /// <summary>特性 X 線の ComboBox を「公開済み結果に含まれる EDX 信号」で作り直す。run 完了時に呼ぶ。
    /// 260802Cl 変更 (作者指示): EDX は Both/Elastic/TDS と並ぶ 4 つ目の表示信号になったので、ここでは
    /// **ラジオの選択可否**も決める。EDX 結果を持たない run を表示中に EDX を選べてしまうと空表示になるため。</summary>
    private void RenewEdxDisplayList()
    {
        var signals = displayedStemResult?.EdxSignals;
        var previous = comboBoxEdxDisplay.SelectedItem as string;
        edxSkipEvent = true;
        try
        {
            comboBoxEdxDisplay.Items.Clear();
            var has = signals is { Length: > 0 };
            radioButtonSTEM_target_EDX.Enabled = has;
            if (!has)
            {
                comboBoxEdxDisplay.Visible = false;
                //EDX を選んだまま EDX 無しの結果が来たら、参照像へ戻す (空表示にしない)
                if (radioButtonSTEM_target_EDX.Checked) radioButtonSTEM_target_both.Checked = true;
                return;
            }
            foreach (var sig in signals)
                comboBoxEdxDisplay.Items.Add(sig.Channel.ShortLabel);
            //前回と同じ特性 X 線が今回の結果にもあれば維持、無ければ先頭
            var idx = previous is null ? -1 : comboBoxEdxDisplay.Items.IndexOf(previous);
            comboBoxEdxDisplay.SelectedIndex = idx >= 0 ? idx : 0;
            comboBoxEdxDisplay.Visible = radioButtonSTEM_target_EDX.Checked;
        }
        finally { edxSkipEvent = false; }
    }

    #endregion

    #region STEM-EDX イベントハンドラ

    private void NumericBoxSTEM_AngleResolution_ValueChanged(object sender, EventArgs e)
    {
        if (checkBoxCalculateEdx is not null && checkBoxCalculateEdx.Checked) RenewEdxSummary();
    }

    private void CheckBoxCalculateEdx_CheckedChanged(object sender, EventArgs e)
    {
        panelEdxDetails.Visible = checkBoxCalculateEdx.Checked;
        if (checkBoxCalculateEdx.Checked)
            RenewEdxChannelList();
        else
            RenewEdxSummary();
    }

    private void CheckedListBoxEdxChannels_ItemCheck(object sender, ItemCheckEventArgs e)
    {
        if (edxSkipEvent || e.Index < 0 || e.Index >= edxCandidates.Length) return;
        //利用不可のチャネルはチェックさせない (理由は項目テキストと ToolTip)
        if (edxCandidates[e.Index].Status != IonizationAvailability.Available)
            e.NewValue = CheckState.Unchecked;
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
            index >= 0 && index < edxCandidates.Length ? edxCandidates[index].ToDescription() : "");
    }

    private void ButtonEdxSelectAvailable_Click(object sender, EventArgs e) => SelectAvailableEdxChannels();

    private void ButtonEdxClear_Click(object sender, EventArgs e)
    {
        edxSkipEvent = true;
        try
        {
            for (int i = 0; i < checkedListBoxEdxChannels.Items.Count; i++)
                checkedListBoxEdxChannels.SetItemChecked(i, false);
        }
        finally { edxSkipEvent = false; }
        RenewEdxSummary();
    }

    /// <summary>特性 X 線 (EDX チャネル) の選択が変わったとき。</summary>
    private void ComboBoxEdxDisplay_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (edxSkipEvent) return;
        GeneratePseudBitmap();
    }

    #endregion

    #endregion

    #endregion プロパティ

    #region フィールド、enum

    public FormMain FormMain;
    public FormDiffractionSpotInfo FormDiffractionSpotInfo;

    //260425Cl 追加: FormMain から呼ばれて配下フォーム (FormDiffractionSpotInfo) の i 列を切替
    public void UpdatePlaneIndices() => FormDiffractionSpotInfo?.UpdatePlaneIndices(FormMain.MillerBravaisActive); // (260426Ch) 1 行 wrapper をインライン化

    public FormPresets FormPresets;
    public FormCTF FormCTF;

    //260801Cl 変更: sw5 = STEM-EDX の別 q パス (Stage 5) 用。旧: sw1..sw4
    readonly Stopwatch sw1 = new(), sw2 = new(), sw3 = new(), sw4 = new(), sw5 = new();

    /// <summary>260801Cl 追加: 実行中の run を開始した BetheMethod (run 中に結晶が切り替わっても購読解除・結果取得が取り違えないよう snapshot)</summary>
    private BetheMethod stemBethe;

    //260802Cl 削除: private int stemEdxChannelCount (実行中の run の EDX チャネル数)。
    //StemProgressInfo.ChannelCount が全ステージに載るようになったので、GUI 側に run 状態を持たなくてよくなった
    //private static readonly double Pi2 = PI * PI;

    /// <summary>260802Cl 追加: 表示中の STEM 結果 (設計書 §5.9.1-5)。ComboBox・左像・右像・厚み/デフォーカスのラベルを
    /// **すべて同じ 1 個の snapshot から**作るための参照。都度 <c>FormMain.Crystal.Bethe.ResultStem</c> を見に行くと、
    /// 結晶を切り替えた後や失敗 run の後に「左は旧 run・右は新 run」といった食い違いが起こり得た (codex 22巡)。
    /// 失敗・cancel した run では更新しない = 次の成功 run まで前回の表示を保つ、という §5.9.1-5 の契約もこれで表現される。</summary>
    private StemSimulationResult displayedStemResult;

    /// <summary>表示セル。折返しで空くセルは null になり得る</summary>
    private ScalablePictureBox[,] pictureBoxes = new ScalablePictureBox[0, 0];

    /// <summary>PseudoBitmap を持つセルだけ (折返しの空セルを除く)</summary>
    private IEnumerable<ScalablePictureBox> Boxes
        => pictureBoxes.Cast<ScalablePictureBox>().Where(b => b?.PseudoBitmap is not null);

    private PseudoBitmap scaleImage;
    public enum ImageModes { HRTEM, POTENTIAL, STEM }

    /// <summary>260802Cl 追加: 画像の表示上の役割。輝度レンジ・カラースケールをどの操作系が支配するかを決める。
    /// 既存の <see cref="ImageInfo.LockIntensity"/> (ポテンシャルの位相像) とは別概念 — あちらは「一切変えない」で、
    /// EDX は「参照像とは別の操作系に属する」。ぼかしは両方に効かせる (同じ実空間座標の表示フィルタなので)。</summary>
    public enum ImageRole { Reference, EdxSignal }

    public enum HRTEM_Modes { Quasi, TCC }

    public enum STEM_ModeEnum { Both, Elastic, TDS, EDX }//260802Cl EDX 追加 (末尾追加)

    #endregion フィールド

    #region 起動、終了、フォームイベントの関連
    public FormImageSimulator()
    {
        InitializeComponent();
        HelpPage = "9-hrtem-stem-simulator"; //260529Cl 追加

        FormDiffractionSpotInfo = new FormDiffractionSpotInfo { Visible = false, FormImageSimulator = this };

        FormPresets = new FormPresets() { Visible = false, Owner = this, TopMost = true, FormImageSimulator = this };

        FormCTF = new FormCTF() { Visible = false, Owner = this, TopMost = true, FormImageSimulator = this };

    }

    private void FormImageSimulator_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        Visible = false;
    }

    private void FormImageSimulator_Load(object sender, EventArgs e)
    {
        toolStripComboBoxCaclulationLibrary.SelectedIndex = 0;

        //260718Cl 変更: 小さな UI スケール画像 2 枚の生成に ParallelEnumerable は過剰なため、単純 for に
        static double[] gradient(int width, int height)
        {
            var values = new double[width * height];
            for (int n = 0; n < values.Length; n++)
                values[n] = (double)(n % width) / width;
            return values;
        }
        var width = pictureBoxPhaseScale.ClientRectangle.Width;
        scaleImage = new PseudoBitmap(gradient(width, pictureBoxPhaseScale.ClientRectangle.Height), width) { MaxValue = 1, MinValue = 0 };
        scaleImage.SetScaleRotation();
        pictureBoxPhaseScale.Image = scaleImage.GetImage();

        width = pictureBoxScaleOfIntensity.ClientRectangle.Width;
        scaleImage = new PseudoBitmap(gradient(width, pictureBoxScaleOfIntensity.ClientRectangle.Height), width) { MaxValue = 1, MinValue = 0 };
        scaleImage.SetScaleGray();
        pictureBoxScaleOfIntensity.Image = scaleImage.GetImage();

        comboBoxScaleColorScale.SelectedIndex = 0;

        //260802Cl 追加: EDX はまだ計算結果が無いので選べない (run 完了時に RenewEdxDisplayList が有効化する)
        radioButtonSTEM_target_EDX.Enabled = false;
        checkBoxEdxCommonScale.Visible = false;

        //260802Cl 変更: 検出器角が STEM 参照像専用である旨 (§5.9.1-4) は、Load でツールチップへ実行時追記していたのをやめ、
        //11 言語 resx の numericBoxSTEM_Detector*Angle.ToolTip 本文へ直接書いた (Designer コントロールの静的な文は resx = 方式①。
        //コード側追記だと翻訳ツール・文字溢れ診断のどちらからも見えないうえ、ハンドル再生成で二重に付く)

        NumericBoxAccVol_ValueChanged(sender, e);
    }

    /// <summary>このフォームのVisibleが変更されたとき。</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FormImageSimulator_VisibleChanged(object sender, EventArgs e)
    {
        if (this.Visible)
        {
            CalculateInsideSpotInfo();
            if (FormCTF.Visible)
                FormCTF.Renew();
            //260802Cl 追加: 非表示の間に結晶が変わっていても RotationChanged は来ない (Visible 時のみ呼ばれる) ので、
            //表示に戻った時点で候補を突き合わせる (中身が同じなら実際の再構築は起きない)
            if (checkBoxCalculateEdx is not null && checkBoxCalculateEdx.Checked)
                RenewEdxChannelList();
            FormMain.toolStripButtonImageSimulator.Checked = true;
        }
        else
        {
            FormDiffractionSpotInfo.Visible = false;
            FormMain.toolStripButtonImageSimulator.Checked = false;
        }
    }
    #endregion 起動、終了関連

    #region PseudoBitmapに格納する情報
    public class ImageInfo(int width, int height, double resolution, Matrix3D mat, string text, bool lockIntensity = false,
        ImageRole role = ImageRole.Reference, string signalKey = null)//260802Cl role/signalKey 追加
    {
        public int Width = width, Height = height;
        public double Resolution = resolution;
        public PointD A = new(mat.E11, mat.E21), B = new(mat.E12, mat.E22), C = new(mat.E13, mat.E23);
        public Matrix3D Mat = mat;
        public string Text = text;
        public bool LockIntensity = lockIntensity;
        /// <summary>260802Cl 追加: 表示上の役割 (輝度・カラーの操作系をどちらに属させるか)</summary>
        public ImageRole Role = role;
        /// <summary>260802Cl 追加: 個別保存のファイル名に使う安定キー (翻訳文字列を使わない)。例 "Reference-Both" / "EDX-Z08-K"</summary>
        public string SignalKey = signalKey;
    }
    #endregion PseudoBitmapに格納する情報

    #region Simulateボタン
    /// <summary>Simulateボタンが押されたとき</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void ButtonSimulate_Click(object sender, EventArgs e)
    {
        Simulate();
    }

    public void Simulate(bool sync = false)
    {
        toolStripStatusLabel1.Text = "";
        toolStripProgressBar.Value = 0;

        if (ImageMode == ImageModes.HRTEM)
            SimulateHRTEM();
        else if (ImageMode == ImageModes.POTENTIAL)
            simulatePotential();
        else if (ImageMode == ImageModes.STEM)
            simulateSTEM(sync);
    }

    /// <summary>
    /// 260524Cl 追加: --capture 用。Show しただけでは画像が無いため、現在のモードの Simulate を起動するだけ。
    /// 計算完了の判定は凝ったことをせず、GuiCapture 側が「画面が変化しなくなったら完了」と見なす (5秒ごとの画面比較)。
    /// 通常操作には影響させず、呼び出し元は GuiCapture に限定する。
    /// </summary>
    internal void PrepareCaptureForGuiAudit()
    {
        if (FormMain?.Crystal == null)
            return;
        Simulate(); // Simulate ボタン相当を起動 (STEM は非同期、HRTEM/POTENTIAL は同期)。完了判定は GuiCapture の画面安定待ちに委ねる。
    }

    #endregion

    #region STEMシミュレーション

    int stemDirectionTotal = 0;
    private void simulateSTEM(bool sync = false)
    {
        //260802Cl 変更: sw5 も reset する (旧: sw1..sw4 のみ)。RunSTEM が同期 throw した run では StemCompleted が
        //呼ばれず reset が走らないため、前回の EDX 時間が次の run の表示に混ざっていた
        sw1.Reset(); sw2.Reset(); sw3.Reset(); sw4.Reset(); sw5.Reset();
        sw1.Restart();
        if (ThicknessArray == null || DefocusArray == null) return;

        //ローテーション配列を作る //一辺が2.の正方形の中に一辺1/Nのピクセルを詰め込み、中心ピクセルが、円の中心とちょうど一致するような問題を考える
        //260718Cl 変更: List+spread を固定長配列に、円内ピクセル数 (stemDirectionTotal) も同じループで数える (旧: inside ローカル関数で全 index を再走査)
        //260802Cl 変更: 式を StemProbeDivision() へ (旧: ここに直書き。EDX の警告表示が同じ式を別に持っていて、
        //片方だけ編集すると「警告に出る分割数」と「実際に走る分割数」が食い違い得た)
        var division = StemProbeDivision();
        var sin = Sin(numericBoxSTEM_ConvergenceAngle.Value * 1.05 / 1000);

        var radius = division / 2.0;
        var directions = new Vector3DBase[division * division];
        stemDirectionTotal = 0;
        for (int h = 0; h < division; h++)
            for (int w = 0; w < division; w++)
            {
                var x = (w - radius + 0.5) / (radius - 0.5) * sin;
                var y = -(h - radius + 0.5) / (radius - 0.5) * sin;//結晶の座標系は、X軸が右、Y軸が上、Z軸が手前なのでYを反転

                directions[h * division + w] = new Vector3DBase(x, y, -Sqrt(1 - x * x - y * y));
                if ((w - radius + 0.5) * (w - radius + 0.5) + (h - radius + 0.5) * (h - radius + 0.5) <= radius * radius)
                    stemDirectionTotal++;
            }

        //260801Cl 追加: EDX 要求は購読前に検証する (エラーで抜けるときに購読解除の後始末が要らない)
        var edxRequests = BuildEdxRequests();
        if (!ValidateEdxRequest(edxRequests, division)) return;

        //260801Cl 追加: run 中に別の結晶へ切り替わっても購読解除・結果取得が同じインスタンスを指すよう snapshot する (codex 20巡)
        stemBethe = FormMain.Crystal.Bethe;
        //260802Cl 削除: stemEdxChannelCount = edxRequests?.Length ?? 0; (StemProgressInfo.ChannelCount が全ステージに載るため不要)

        toolStripProgressBar.Maximum = stemDirectionTotal;
        stemBethe.StemProgressChanged += stemProgressChanged;
        stemBethe.StemCompleted += StemCompleted;

        try
        {
            stemBethe.RunSTEM(
                BlochNum,
                AccVol,
                Cs,
                Delta,
                STEM_SliceThickness,
                ImageSize,
                ImageResolution,
                STEM_SourceSizeFWHM,
                FormMain.Crystal.RotationMatrix,
                ThicknessArray,
                DefocusArray,
                directions,
                STEM_ConvergenceAngle,
                STEM_DetectorInnerAngle,
                STEM_DetectorOuterAngle,
                ionizations: edxRequests
                );
        }
        catch (Exception ex)
        {
            //260801Cl 追加: RunSTEM は worker 起動前に同期 throw し得る (未収録 Z・E0 範囲外・重複チャネル)。
            //購読したまま抜けると次の run で二重購読になるので、必ず解除して UI を戻す (codex 20巡)
            stemBethe.StemProgressChanged -= stemProgressChanged;
            stemBethe.StemCompleted -= StemCompleted;
            stemBethe = null;
            toolStripStatusLabel1.Text = ex.Message;
            MessageBox.Show(ex.Message, "STEM-EDX", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        this.buttonSimulate.Visible = false;
        this.buttonStop.Visible = true;
        this.splitContainer1.Enabled = false;

        if (sync)
            // 260428Cl 同期マクロ呼び出し用の UI ポンプ。Macro 自体が同期 API のため、ここでは DoEvents を残す (Macro の async 化時に削除予定)
            //260802Cl 変更: 待つ相手をローカルに退避する (旧: stemBethe.IsSTEM_Busy)。DoEvents 中に StemCompleted が
            //走ると stemBethe は null になるので、次のループ条件で NullReferenceException になり得た
            for (var running = stemBethe; running.IsSTEM_Busy;)
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }
    }

    private void buttonStop_Click(object sender, EventArgs e)
    {
        //260801Cl 変更: cancel 要求だけ出し、UI の復帰は StemCompleted に任せる (worker が止まる前に設定 UI を戻すと、
        //実行中の run と食い違う条件で再実行できてしまう。codex 20巡)。旧: ここで buttonSimulate/splitContainer を戻していた
        //260802Cl 変更: 旧 `stemBethe ?? FormMain.Crystal.Bethe` は「今の結晶」へ落ちるので snapshot の意味を打ち消していた
        if (stemBethe is null) return;
        stemBethe.CancelSTEM();
        buttonStop.Enabled = false;
    }

    #region BackgroundWorkerからのProgressChanged
    private bool skipProgressChangedEvent = false;
    private void stemProgressChanged(object sender, ProgressChangedEventArgs e)
    {

        if (skipProgressChangedEvent) return;
        //260802Cl 追加: StemCompleted と同じ理由 (--capture は Application.Run 無し = 通知がスレッドプールで来る)
        if (InvokeRequired) { BeginInvoke(() => stemProgressChanged(sender, e)); return; }
        //260802Cl 変更: 文字列前方一致 (旧: var message = (string)e.UserState; message.StartsWith("Calculating I_EDX(Q)") …) を廃し
        //型付き StemProgressInfo で分岐する (設計書 §5.9-8)。旧実装は Stage5 のチャネル番号を " (ch i/n)" から
        //Substring でパースしており、書式が変わると負長で落ちる経路が残っていた。
        if (e.UserState is not StemProgressInfo info) return;
        //260802Cl 追加: 以降は必ず finally でフラグを戻す (途中で例外が出るとフラグが立ちっぱなしになり、
        //以後の進捗表示が一切更新されなくなっていた。codex 22巡)
        skipProgressChangedEvent = true;
        try
        {

        long s1 = sw1.ElapsedMilliseconds, s2 = sw2.ElapsedMilliseconds, s3 = sw3.ElapsedMilliseconds, s4 = sw4.ElapsedMilliseconds;

        var current = info.Fraction;//ステージ内進捗 0-1 (旧: 0-1E6 の ProgressPercentage)
        //260801Cl 追加: Stage4 と Stage5 (EDX) の配分。EDX チャネル 1 本を Stage4 の 1/3 の重みとみなす (§5.9-8。
        //暫定係数で、実測 ETA モデルではない)。ch=0 なら Stage4 が従来どおり 20-100%
        //260802Cl 変更: チャネル数は GUI のチェック状態 (旧 stemEdxChannelCount フィールド) ではなく run の要求そのものから取る
        var edxCh = info.ChannelCount;
        var stage4Span = 0.80 * 3 / (3 + edxCh);
        var edxSpan = 0.80 / (3 + edxCh);
        //残り時間は etaFrac==0 で 0 除算になるので、進捗が出るまでは「推定中」にする。
        //260802Cl 変更: 残り時間の基準を「表示している %」と分けた。Stage5 の sw5 は全チャネル通算で進むので、
        //チャネル内 Fraction で割ると 2 本目以降の残り時間が大幅な過大評価になる (codex 22巡)
        double etaFrac = current;
        string Remaining(double sec) => etaFrac > 0 ? $"wait for more {sec * (1 - etaFrac) / etaFrac:f1} s" : "estimating…";

        //ステージ表示・進捗バー・残り時間の組み立ては全ステージ共通なので、分岐では「どの時計を動かすか」と
        //「バーの位置」と「ステージ文」だけを決める
        double sec, totalsec, bar;
        string stage;
        switch (info.Stage)
        {
            case StemStage.IonizationQ://Stage 5 (STEM-EDX の別 q パス)
                if (sw1.IsRunning) sw1.Stop();
                if (sw2.IsRunning) sw2.Stop();
                if (sw3.IsRunning) sw3.Stop();
                if (sw4.IsRunning) sw4.Stop();
                if (!sw5.IsRunning) sw5.Restart();
                sec = sw5.ElapsedMilliseconds / 1000.0;
                totalsec = (s1 + s2 + s3 + s4) / 1000.0 + sec;
                etaFrac = edxCh > 0 ? (info.ChannelIndex + current) / edxCh : current;//sw5 は全チャネル通算なので分母も全チャネル
                bar = Math.Min(0.20 + stage4Span + (info.ChannelIndex + current) * edxSpan, 1.0);
                //260802Cl: 型付きになったのでチャネル名も出せる (旧: 番号だけ)
                stage = $"Stage 5: Calculating I_EDX(Q) {info.Channel?.ShortLabel} {(edxCh > 1 ? $"({info.ChannelIndex + 1}/{edxCh}) " : "")}. ";
                break;
            case StemStage.InelasticQ:
                if (sw1.IsRunning) sw1.Stop();
                if (sw2.IsRunning) sw2.Stop();
                if (sw3.IsRunning) sw3.Stop();
                if (!sw4.IsRunning) sw4.Restart();
                sec = s4 / 1000.0;
                totalsec = sec + (s1 + s2 + s3) / 1000.0;
                bar = current * stage4Span + 0.2;
                stage = "Stage 4: Calculating I_inelastic(Q).  ";
                break;
            case StemStage.PotentialMatrix:
                if (sw1.IsRunning) sw1.Stop();
                if (sw2.IsRunning) sw2.Stop();
                if (!sw3.IsRunning) sw3.Restart();
                sec = s3 / 1000.0;
                totalsec = sec + (s1 + s2) / 1000.0;
                bar = current * 0.01 + 0.19;
                stage = "Stage 3: Calculating U' matrix.  ";
                break;
            case StemStage.ElasticQ:
                if (sw1.IsRunning) sw1.Stop();
                if (!sw2.IsRunning) sw2.Restart();
                sec = s2 / 1000.0;
                totalsec = sec + s1 / 1000.0;
                bar = current * 0.01 + 0.18;
                stage = "Stage 2: Calculating I_elastic(Q).  ";
                break;
            case StemStage.EigenSolve:
                sec = totalsec = s1 / 1000.0;
                bar = current * 0.18;
                stage = $"Stage 1: Calculating Tg for {stemDirectionTotal} directions ({info.SolverLabel}).";
                break;
            //260802Cl 変更: 未知のステージを Stage1 として表示しない (旧: default が EigenSolve 扱いだったので、
            //将来ステージが増えたとき「Stage 1」と誤表示し、しかも sw1 を動かしてしまう。codex 22巡)
            default:
                return;
        }
        toolStripProgressBar.Value = (int)(bar * toolStripProgressBar.Maximum);
        toolStripStatusLabel1.Text = $"Elapsed time : {totalsec:f1} s  {stage}";
        toolStripStatusLabel2.Text = $"{current * 100:f1} % completed,  {Remaining(sec)}";
        // 260428Cl Application.DoEvents() を削除 (BackgroundWorker の ProgressChanged は UI スレッドで動作するため不要)
        }
        finally { skipProgressChangedEvent = false; }
    }
    #endregion

    #region BackgroundWorkerからのstemCompleted
    private void StemCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        //260802Cl 追加: --capture は Application.Run を回さないため UI スレッドに SynchronizationContext が無く、
        //BackgroundWorker の完了通知がスレッドプールで来る。そのまま UI を触ると「別スレッドで作られたコントロールは
        //親にできない」で落ちたり、描画しても画面に反映されなかったりする (実機で両方踏んだ)。
        //通常起動 (Application.Run あり) では InvokeRequired=false なので、この分岐は素通りする
        if (InvokeRequired) { BeginInvoke(() => StemCompleted(sender, e)); return; }

        //260801Cl 変更: 購読解除は run を開始したインスタンスに対して行う (run 中に結晶が切り替わっても取り違えない)。
        //旧: FormMain.Crystal.Bethe を都度参照していた
        //260802Cl 変更: フォールバックを外す (run を開始したインスタンス以外から購読解除すると取り違える)
        var bethe = stemBethe;
        if (bethe is not null)
        {
            bethe.StemCompleted -= StemCompleted;
            bethe.StemProgressChanged -= stemProgressChanged;
        }
        long s1 = sw1.ElapsedMilliseconds, s2 = sw2.ElapsedMilliseconds, s3 = sw3.ElapsedMilliseconds, s4 = sw4.ElapsedMilliseconds, s5 = sw5.ElapsedMilliseconds;

        //260801Cl 追加: e.Error を e.Cancelled より先に見る (旧実装は e.Error を確認しておらず、
        //worker が例外で落ちても「完了」として旧結果を再描画していた。codex 20巡)
        if (e.Error is not null)
        {
            toolStripStatusLabel1.Text = $"Failed: {e.Error.Message}";
            MessageBox.Show(e.Error.Message, "STEM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        else if (!e.Cancelled)
        {
            //260802Cl 追加: 表示元をこの run の結果 1 個に固定する (設計書 §5.9.1-5)。
            //失敗・cancel では更新しない = 次の成功 run まで前回の表示を保つ。以後 ComboBox も左右の像も
            //厚み/デフォーカスのラベルも、すべてこの snapshot から作る
            displayedStemResult = bethe?.ResultStem;
            //260801Cl 追加: 特性 X 線の ComboBox は「今回の run が公開した EdxSignals」から作る (GeneratePseudBitmap より先に)
            RenewEdxDisplayList();
            //260802Cl 追加: --capture が元素マップのスクショを撮るための切替 (通常操作では常に false)
            //判定に radioButtonSTEM_target_EDX.Enabled は使えない: Enabled の getter は「実効値」で、
            //この時点ではまだ splitContainer1.Enabled=false (計算中の UI ロック) なので必ず false になる
            if (CaptureSelectEdxAfterRun && EdxDisplayAvailable)
                radioButtonSTEM_target_EDX.Checked = true;
            //SendImage(ThicknessArray.Length, DefocusArray.Length, FormMain.Crystal.Bethe.STEM_Image, ImageSize.Width, ImageSize.Height);
            GeneratePseudBitmap();


            toolStripProgressBar.Value = toolStripProgressBar.Maximum;
            toolStripStatusLabel1.Text = $"Completed! Total elapsed time: {(s1 + s2 + s3 + s4 + s5) / 1000.0:f1} s"; // 260520Cl: typo fix (ellapsed → elapsed)
            toolStripStatusLabel1.Text += $"  Stage 1: {s1 / 1000.0:f1} s  Stage 2: {s2 / 1000.0:f1} s  Stage 3: {s3 / 1000.0:f1} s  Stage 4: {s4 / 1000.0:f1} s";
            if (s5 > 0) toolStripStatusLabel1.Text += $"  Stage 5 (EDX): {s5 / 1000.0:f1} s";//260801Cl 追加

        }
        else
        {
            toolStripStatusLabel1.Text = $"Interrupted! Total elapsed time: {(s1 + s2 + s3 + s4 + s5) / 1000.0:f1} s"; // 260520Cl: typo fix (Interupted → Interrupted, ellapsed → elapsed)
        }
        toolStripStatusLabel2.Text = "";
        this.buttonSimulate.Visible = true;
        this.buttonStop.Visible = false;
        this.buttonStop.Enabled = true;//260801Cl 追加 (buttonStop_Click で無効化した分を戻す)
        this.splitContainer1.Enabled = true;
        stemBethe = null;//260801Cl 追加
        //260801Cl 修正: sw3 の Stop/Reset が 2 回書かれ sw4 が reset されていなかった (旧: sw3.Reset(); sw3.Reset();)
        sw1.Stop(); sw1.Reset(); sw2.Stop(); sw2.Reset(); sw3.Stop(); sw3.Reset(); sw4.Stop(); sw4.Reset(); sw5.Stop(); sw5.Reset();
        // 260428Cl Application.DoEvents() を削除 (RunWorkerCompleted は UI スレッドで動作するため不要)
    }

    #endregion

    #endregion;

    #region HREMシミュレーション
    public void SimulateHRTEM(bool realtimeMode = false)
    {
        sw1.Restart();

        if (ThicknessArray == null || DefocusArray == null) return;

        Beams = FormMain.Crystal.Bethe.GetDifractedBeamAmpriltudes(BlochNum, AccVol, FormMain.Crystal.RotationMatrix, ThicknessArray[0]);

        //LTF(レンズ伝達関数)を計算 && apertureの外にあるbeamを除外
        BeamsInside = BetheMethod.ExtractInsideBeams(Beams, AccVol, HRTEM_ObjAperRadius, HRTEM_ObjAperX, HRTEM_ObjAperY);
        if (BeamsInside.Length < 2)//絞りに入るスポットが2未満の時は、警告を出してリターン
        {
            if (!realtimeMode)
                MessageBox.Show("Obj. Aper. size is too small. Try again after increase the value!");
            return;
        }

        FormMain.Crystal.Bethe.GetHRTEMImage(
            BlochNum,
            AccVol,
            FormMain.Crystal.RotationMatrix,
            (HRTEM_ObjAperRadius, HRTEM_ObjAperX, HRTEM_ObjAperY),
            ImageSize,
            ImageResolution,
            Cs,
            HRTEM_Beta,
            Delta,
            ThicknessArray,
            DefocusArray,
            HRTEM_Mode == HRTEM_Modes.Quasi,
            Native);

        var temp = sw1.ElapsedMilliseconds;
        toolStripStatusLabel1.Text += $"Generation of HRTEM images: {sw1.ElapsedMilliseconds} ms,   ";

        GeneratePseudBitmap();

        toolStripStatusLabel1.Text += $"Drawing: {sw1.ElapsedMilliseconds - temp} ms";
    }
    #endregion

    #region ポテンシャルシミュレーション
    private void simulatePotential(bool realtimeMode = false)
    {
        sw1.Restart();

        if (!checkBoxPotentialUg.Checked && !checkBoxPotentialUgPrime.Checked) return;

        Beams = FormMain.Crystal.Bethe.GetDifractedBeamAmpriltudes(BlochNum, AccVol, FormMain.Crystal.RotationMatrix, ThicknessArray[0]);
        var images = FormMain.Crystal.Bethe.GetPotentialImage(Beams, ImageSize, ImageResolution, radioButtonPotentialModeMagAndPhase.Checked);

        //画像が上下左右反転 (180度回転) しているみたいなので、処理 20230304
        //なぜかまた上下左右反転 (180度回転) しているみたいなので、削除 20241101
        //for (int i = 0; i < images.Length; i++)
        //    images[i] = [.. images[i].Reverse()];


        var temp = sw1.ElapsedMilliseconds;
        toolStripStatusLabel1.Text = $"Generation of Potential images: {temp} ms,   ";

        //最大値、最小値の設定
        double max = radioButtonPotentialModeMagAndPhase.Checked ? Max(images[0].Max(), images[2].Max()) : Max(Abs(images.Max(d => d.Max())), Abs(images.Min(d => d.Min())));
        double min = radioButtonPotentialModeMagAndPhase.Checked ? 0 : -max;

        //トラックバー設定
        SkipEvent = true;
        trackBarAdvancedMax.Value = trackBarAdvancedMin.Maximum = trackBarAdvancedMax.Maximum = max;
        trackBarAdvancedMin.Value = trackBarAdvancedMin.Minimum = trackBarAdvancedMax.Minimum = min;
        trackBarAdvancedMax.UpDown_Increment = trackBarAdvancedMin.UpDown_Increment = (max - min) / 100.0;
        SkipEvent = false;

        //作成したイメージをPseudoBitmapに変換
        var mat = FormMain.Crystal.RotationMatrix * FormMain.Crystal.MatrixReal;
        int width = ImageSize.Width, height = ImageSize.Height;
        var range = Enumerable.Range(0, 2).ToList();
        var pseudo = range.Select(_ => range.Select(_ => new PseudoBitmap()).ToList()).ToList();

        //振幅と位相モードの時
        if (radioButtonPotentialModeMagAndPhase.Checked)
            foreach (var (i, j, text) in new[] { (0, 0, "Ug magnitude"), (0, 1, "Ug phase"), (1, 0, "U'g magnitude"), (1, 1, "Ug phase") })
            {
                var src = j == 0 ? images[i * 2 + j] : images[i * 2 + j].Select(d => d / Math.PI * 180).ToArray();
                pseudo[i][j] = new PseudoBitmap(src, width)
                {
                    MaxValue = j == 0 ? max : 180,
                    MinValue = j == 0 ? min : -180,
                    Tag = new ImageInfo(width, height, ImageResolution, mat, text, j == 1),
                    Scale = j == 0 ?
                    (comboBoxScaleColorScale.SelectedIndex == 0 ? PseudoBitmap.Scales.GrayLinear : PseudoBitmap.Scales.ColdWarmLinear) :
                    PseudoBitmap.Scales.RotationLinear
                };
            }
        //実数と虚数モードの時
        else
            foreach (var (i, j, text) in new[] { (0, 0, "Ug real"), (0, 1, "Ug imag"), (1, 0, "U'g real"), (1, 1, "U'g imag") })
                pseudo[i][j] = new PseudoBitmap(images[i * 2 + j], width)
                {
                    MaxValue = max,
                    MinValue = min,
                    Tag = new ImageInfo(width, height, ImageResolution, mat, text),
                    Scale = comboBoxScaleColorScale.SelectedIndex == 0 ? PseudoBitmap.Scales.GrayLinear : PseudoBitmap.Scales.ColdWarmLinear
                };

        //チェック状況に応じて、削除
        if (!checkBoxPotentialUg.Checked)
            pseudo.RemoveAt(0);
        else if (!checkBoxPotentialUgPrime.Checked)
            pseudo.RemoveAt(1);

        if ((radioButtonPotentialModeRealAndImag.Checked && radioButtonPotentialShowReal.Checked) ||
            (radioButtonPotentialModeMagAndPhase.Checked && radioButtonPotentialShowMag.Checked))
            pseudo.ForEach(p => p.RemoveAt(1));
        else if ((radioButtonPotentialModeRealAndImag.Checked && radioButtonPotentialShowImag.Checked) ||
            (radioButtonPotentialModeMagAndPhase.Checked && radioButtonPotentialShowPhase.Checked))
            pseudo.ForEach(p => p.RemoveAt(0));

        //resultに格納して、ScalablePictureboxに転送
        var result = new PseudoBitmap[pseudo.Count, pseudo[0].Count];
        for (int r = 0; r < pseudo.Count; r++)
            for (int c = 0; c < pseudo[0].Count; c++)
                result[r, c] = pseudo[r][c];

        SetPseudoBitamap(result);
        toolStripStatusLabel1.Text += $"Drawing: {sw1.ElapsedMilliseconds - temp} ms";
        TrackBarAdvancedMin_ValueChanged(new object(), 0);
    }
    #endregion

    #region 計算結果をPictureBoxにセット

    /// <summary>計算結果から PseudoBitmap を作り、グリッドへ転送する。
    /// 260802Cl 変更 (作者指示): 二ペイン並置は廃止し 1 ペインへ戻した。STEM の表示信号は
    /// Both / Elastic / TDS / **EDX** の 4 択で、EDX を選んだときだけ直下の ComboBox で特性 X 線を選ぶ。
    /// 表示に使う値はすべて <see cref="displayedStemResult"/> という 1 個の snapshot から取る
    /// (旧実装は厚み・デフォーカスのラベルだけ現在の GUI 入力 ThicknessArray/DefocusArray を使っており、
    /// 旧 run を表示したまま入力を変えると画像とラベルが食い違っていた)。</summary>
    public void GeneratePseudBitmap()
    {
        if (ImageMode == ImageModes.POTENTIAL)
            return;

        var bethe = FormMain.Crystal.Bethe;
        if (ImageMode == ImageModes.STEM)
        {
            var result = displayedStemResult;
            if (result?.ImageBoth is null)
                return;
            var mat = result.Rotation * FormMain.Crystal.MatrixReal;

            //EDX を選んでいる間だけ、特性 X 線の選択と EDX 専用の表示オプションを出す
            var edx = radioButtonSTEM_target_EDX.Checked ? SelectedEdxSignal : null;
            comboBoxEdxDisplay.Visible = radioButtonSTEM_target_EDX.Checked && comboBoxEdxDisplay.Items.Count > 0;
            checkBoxEdxCommonScale.Visible = edx is not null && result.EdxSignals.Length > 1;

            if (edx is not null)
                FillEdxGrid(edx, result, mat);
            else
            {
                var (planes, key) =
                    radioButtonSTEM_target_elas.Checked ? (result.ImageEla.Planes, "Elastic") :
                    radioButtonSTEM_target_TDS.Checked ? (result.ImageTDS.Planes, "TDS") :
                                                         (result.ImageBoth.Planes, "Both");
                FillReferenceGrid(planes, result.Size, result.Resolution, mat, result.Thicknesses, result.Defocusses, key);
            }
        }
        else if (ImageMode == ImageModes.HRTEM)
        {
            if (bethe.ResultHRTEM.Image == null)
                return;
            var r = bethe.ResultHRTEM;
            FillReferenceGrid(r.Image, r.Size, r.Resolution, r.rot * FormMain.Crystal.MatrixReal, r.Thicknesses, r.Defocusses, "HRTEM");
        }

        //260802Cl 追加: **レイアウトが確定してから**描く。SetPseudoBitamap の中で描くと、新規生成直後のセルは
        //まだ 1x1 で ScalablePictureBox.drawPictureBox が何もせずに戻り、画像が出ないまま残る
        foreach (var b in Boxes)
            b.drawPictureBox();
    }

    /// <summary>260802Cl 追加: 弾性・TDS・その合成 / HRTEM 像の生成。従来どおり値そのものを Normalize してから
    /// 1 本の表示レンジを与える (既存の輝度調整 UI の意味を変えない)。</summary>
    private void FillReferenceGrid(double[][][] images, Size size, double resolution, Matrix3D mat,
        double[] thicknesses, double[] defocusses, string signalKey)
    {
        int tLen = thicknesses.Length, dLen = defocusses.Length;
        var _images = new double[tLen][][];
        for (int t = 0; t < tLen; t++)
            _images[t] = new double[dLen][];

        //全体でノーマライズ
        if (!checkBoxNormarizeIndividually.Checked)
            _images = Normalize(images, checkBoxIntensityMin.Checked, checkBoxIntensityMax.Checked);
        else
            for (int t = 0; t < tLen; t++)
                for (int d = 0; d < dLen; d++)
                    _images[t][d] = Normalize(images[t][d], checkBoxIntensityMin.Checked, checkBoxIntensityMax.Checked);

        //260802Cl 変更: レンジを先に確定してからセルを作る (旧: 古いトラックバー値でセルを作り、最後に
        //TrackBarAdvancedMin_ValueChanged で上書きしていた)
        double max = checkBoxIntensityMax.Checked ? numericBoxIntensityMax.Value : _images.Max();
        double min = checkBoxIntensityMin.Checked ? numericBoxIntensityMin.Value : _images.Min();
        if (max <= min) max = min + 1;//定数画像でも表示できるようにする (0 除算・全黒回避)

        SetPseudoBitamap(BuildPseudoGrid(_images, size, resolution, mat, thicknesses, defocusses,
            ImageRole.Reference, signalKey, (_, _) => (min, max),
            comboBoxScaleColorScale.SelectedIndex == 0 ? PseudoBitmap.Scales.GrayLinear : PseudoBitmap.Scales.ColdWarmLinear));
        SetAdjustRange(min, max);
    }

    /// <summary>260802Cl 追加: EDX 元素マップの生成。**生値は変換しない** (設計書 §5.9-5・codex 22/23巡):
    /// Normalize を通すとチャネル間の振幅情報が消え、TIFF 出力も「生成空孔量」でなくなる。表示レンジだけを与える。
    /// 下限は 0 固定 (負値 clamp 済みの非負量。各マップの最小値を黒にすると背景の強弱の比較が壊れる)。
    /// 上限は t/d 軸 (checkBoxNormarizeIndividually) × チャネル軸 (checkBoxEdxCommonScale) の 2 軸直交で決める。</summary>
    private void FillEdxGrid(StemSignalMap edx, StemSimulationResult result, Matrix3D mat)
    {
        var planes = edx.Image.Planes;
        int tLen = result.Thicknesses.Length, dLen = result.Defocusses.Length;
        //チャネル軸: 共通なら「表示していないチャネルも含めた」全 EDX 信号が母集団 (§5.9-5)
        var scope = checkBoxEdxCommonScale.Checked && checkBoxEdxCommonScale.Visible ? result.EdxSignals : [edx];

        double allMax = 0;
        foreach (var s in scope)
            foreach (var byT in s.Image.Planes)
                foreach (var img in byT)
                    for (int i = 0; i < img.Length; i++)
                        if (img[i] > allMax) allMax = img[i];

        double[,] tdMax = null;
        if (checkBoxNormarizeIndividually.Checked)
        {
            tdMax = new double[tLen, dLen];
            foreach (var s in scope)
                for (int t = 0; t < tLen; t++)
                    for (int d = 0; d < dLen; d++)
                    {
                        var img = s.Image.Planes[t][d];
                        for (int i = 0; i < img.Length; i++)
                            if (img[i] > tdMax[t, d]) tdMax[t, d] = img[i];
                    }
        }

        var key = $"EDX-Z{edx.Channel.Z:d2}-{edx.Channel.Shell}";
        SetPseudoBitamap(BuildPseudoGrid(planes, result.Size, result.Resolution, mat, result.Thicknesses, result.Defocusses,
            ImageRole.EdxSignal, key,
            (t, d) => (0, Math.Max(tdMax is null ? allMax : tdMax[t, d], double.Epsilon)),
            //EDX 既定は Gray (非負量に ColdWarm は不適、§5.9-5)。ラジオを EDX へ切り替えたとき comboBoxScaleColorScale を
            //Gray へ寄せてあるので、ここは素直に ComboBox に従えばよい (ユーザーが明示的に ColdWarm を選べば従う)
            comboBoxScaleColorScale.SelectedIndex == 0 ? PseudoBitmap.Scales.GrayLinear : PseudoBitmap.Scales.ColdWarmLinear));
        SetAdjustRange(0, Math.Max(allMax, double.Epsilon));
    }

    /// <summary>260802Cl 追加: [t][d][pix] から表示用の PseudoBitmap 行列を組む (折返しを含む)。</summary>
    private PseudoBitmap[,] BuildPseudoGrid(double[][][] images, Size size, double resolution, Matrix3D mat,
        double[] thicknesses, double[] defocusses, ImageRole role, string signalKey,
        Func<int, int, (double Min, double Max)> range, PseudoBitmap.Scales scale)
    {
        int tLen = thicknesses.Length, dLen = defocusses.Length;
        var horizontalDefocus = radioButtonHorizontalDefocus.Checked;
        var pseudo = horizontalDefocus ? new PseudoBitmap[tLen, dLen] : new PseudoBitmap[dLen, tLen];

        for (int t = 0; t < tLen; t++)
            for (int d = 0; d < dLen; d++)
            {
                var (min, max) = range(t, d);
                pseudo[horizontalDefocus ? t : d, horizontalDefocus ? d : t] = new PseudoBitmap(images[t][d], size.Width)
                {
                    //ラベルは表示中 result の厚み・デフォーカス (旧: 現在の GUI 入力値 ThicknessArray/DefocusArray)
                    Tag = new ImageInfo(size.Width, size.Height, resolution, mat,
                        $"t={thicknesses[t]:f2}\r\nf={defocusses[d]:f2}", false, role, $"{signalKey} t{t:d3}-d{d:d3}"),
                    MaxValue = max,
                    MinValue = min,
                    Scale = scale
                };
            }

        //1列あるいは1行で、他の要素が多いときは適当に折り返し
        if ((dLen == 1 && tLen > 2) || (tLen == 1 && dLen > 2))
        {
            var newCol = Ceiling(Sqrt(pseudo.Length));
            var newRow = Ceiling(pseudo.Length / newCol);
            var newPseudo = new PseudoBitmap[(int)newRow, (int)newCol];
            var oldPseudo = pseudo.Cast<PseudoBitmap>().ToList();
            for (int r = 0, n = 0; r < newRow; r++)
                for (int c = 0; c < newCol; c++, n++)
                    newPseudo[r, c] = n < pseudo.Length ? oldPseudo[n] : null;
            pseudo = newPseudo;
        }
        return pseudo;
    }

    /// <summary>260802Cl 追加: 輝度トラックバーを今の表示レンジに合わせる (イベントは飛ばさない)。</summary>
    private void SetAdjustRange(double min, double max)
    {
        SkipEvent = true;
        trackBarAdvancedMax.Value = trackBarAdvancedMin.Maximum = trackBarAdvancedMax.Maximum = max;
        trackBarAdvancedMin.Value = trackBarAdvancedMin.Minimum = trackBarAdvancedMax.Minimum = min;
        trackBarAdvancedMax.UpDown_Increment = trackBarAdvancedMin.UpDown_Increment = (max - min) / 100.0;
        SkipEvent = false;
    }

    #region normarize関数
    public double[] Normalize(double[] image, bool normalizeMin, bool normalizeMax)
    {
        if (!normalizeMin && !normalizeMax)
            return [.. image];

        double min = image.Min(), max = image.Max();
        double destMin = normalizeMin ? numericBoxIntensityMin.Value : min;
        double destMax = normalizeMax ? numericBoxIntensityMax.Value : max;

        return image.Select(d => (d - min) / (max - min) * (destMax - destMin) + destMin).ToArray();
    }

    public double[][][] Normalize(double[][][] image, bool normalizeMin, bool normalizeMax)
    {
        var _image = new double[image.Length][][];

        double min = image.Min(), max = image.Max();
        double destMin = normalizeMin ? numericBoxIntensityMin.Value : min;
        double destMax = normalizeMax ? numericBoxIntensityMax.Value : max;

        for (int i = 0; i < image.Length; i++)
        {
            _image[i] = new double[image[i].Length][];
            for (int j = 0; j < image[i].Length; j++)
                if (normalizeMin || normalizeMax)
                    _image[i][j] = image[i][j].Select(d => (d - min) / (max - min) * (destMax - destMin) + destMin).ToArray();
                else
                    _image[i][j] = [.. image[i][j]];
        }
        return _image;
    }
    #endregion

    //作成したPseutoBitmapをscalablePictureBoxに転送
    //セル数・折返しが変わったときだけ作り直す。折返しで空くセル (null) も許容する。
    private void SetPseudoBitamap(PseudoBitmap[,] image)
    {
        var row = image.GetLength(0);
        var col = image.GetLength(1);

        if (pictureBoxes.GetLength(0) == row && pictureBoxes.GetLength(1) == col)
        {
            for (int r = 0; r < row; r++)
                for (int c = 0; c < col; c++)
                {
                    pictureBoxes[r, c].SkipEvent = true;
                    pictureBoxes[r, c].PseudoBitmap = image[r, c];
                    pictureBoxes[r, c].SkipEvent = false;
                }
        }
        else
        {
            tableLayoutPanel.SuspendLayout();
            pictureBoxes = new ScalablePictureBox[row, col];
            tableLayoutPanel.Controls.Clear();
            tableLayoutPanel.RowCount = row;
            tableLayoutPanel.ColumnCount = col;
            //260802Cl 追加: RowCount/ColumnCount を増やしても Row/ColumnStyles は自動では増えない。
            //Designer の LayoutSettings が持っている本数 (20) を超えると添字で落ちるので、足りない分をここで補う
            //(厚み 20 点超の run で実際に落ちる経路だった)
            while (tableLayoutPanel.RowStyles.Count < row) tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 1f));
            while (tableLayoutPanel.ColumnStyles.Count < col) tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1f));
            for (int r = 0; r < row; r++) tableLayoutPanel.RowStyles[r].Height = 1f;//260718Cl Range().ToList().ForEach → for
            for (int c = 0; c < col; c++) tableLayoutPanel.ColumnStyles[c].Width = 1f;

            for (int r = 0; r < row; r++)
                for (int c = 0; c < col; c++)
                {
                    pictureBoxes[r, c] = new ScalablePictureBox
                    {
                        SkipEvent = true,
                        Size = new Size(1, 1),
                        MouseScaling = true,
                        MouseTranslation = true,
                        PseudoBitmap = image[r, c],
                        ZoomAndCenter = (0, new PointD(0, 0))
                    };
                    tableLayoutPanel.Controls.Add(pictureBoxes[r, c], c, r);
                    pictureBoxes[r, c].Dock = DockStyle.Fill;
                    pictureBoxes[r, c].SkipEvent = false;

                    pictureBoxes[r, c].DrawingAreaChanged += PictureBox_DrawingAreaChanged;
                    pictureBoxes[r, c].Paint2 += PictureBox_Paint2;
                    pictureBoxes[r, c].MouseMove2 += FormImageSimulator_MouseMove2;
                    pictureBoxes[r, c].MouseDown2 += FormImageSimulator_MouseDown2;
                }
            tableLayoutPanel.ResumeLayout();
        }

        pictureBoxes[0, 0].ZoomAndCenter = (0, new PointD(0, 0));
    }
    #endregion

    #region マウス操作

    private bool FormImageSimulator_MouseMove2(object sender, MouseEventArgs e, PointD pt)
    {
        var pseud = (sender as ScalablePictureBox).PseudoBitmap;
        if (pseud?.Tag is not ImageInfo info) return false;//260802Cl 追加: 折返しの空セルは Tag を持たない
        labelMousePositionX.Text = $"X: {(pt.X - info.Width / 2.0) * info.Resolution * 1000:f2} pm";
        labelMousePositionY.Text = $"Y: {(-pt.Y + info.Height / 2.0) * info.Resolution * 1000:f2} pm";
        //260802Cl 変更: EDX は「モデル上の生成空孔量」であって実測 X 線カウントではないので、値だけ出すと誤読される
        labelMousePositionValue.Text = info.Role == ImageRole.EdxSignal
            ? $"Value: {pseud.GetPixelRawValue(pt):g6} (model)"
            : $"Value: {pseud.GetPixelRawValue(pt):g6}";
        return false;
    }
    private bool FormImageSimulator_MouseDown2(object sender, MouseEventArgs e, PointD pt)
    {
        if (e.Clicks == 2 && e.Button == MouseButtons.Left)
        {
            int rows = pictureBoxes.GetLength(0), cols = pictureBoxes.GetLength(1);
            if (rows == 1 && cols == 1)
                return false;

            for (int targetR = 0; targetR < rows; targetR++)
                for (int targetC = 0; targetC < cols; targetC++)
                    if ((ScalablePictureBox)sender == pictureBoxes[targetR, targetC])//まずターゲットを見つける
                    {
                        var restore = tableLayoutPanel.RowStyles[targetR].Height == 100f;
                        tableLayoutPanel.SuspendLayout();
                        SkipEvent = true;
                        for (int row = 0; row < rows; row++)
                            tableLayoutPanel.RowStyles[row].Height = restore ? 1f : (targetR == row ? 100f : 0f);
                        for (int col = 0; col < cols; col++)
                            tableLayoutPanel.ColumnStyles[col].Width = restore ? 1f : (targetC == col ? 100f : 0f);
                        if (restore && pictureBoxes[0, 0] is not null)
                            pictureBoxes[0, 0].ZoomAndCenter = (0, new PointD(0, 0));
                        SkipEvent = false;
                        tableLayoutPanel.ResumeLayout();
                        return false;
                    }
        }
        return false;
    }
    #endregion

    #region 電子顕微鏡の各種光学パラメータや試料パラメータのイベント

    /// <summary>電子顕微鏡の各種光学パラメータが変更されたとき。レンズ関数を描画</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NumericBoxTEMproperty_ValueChanged(object sender, EventArgs e) => FormCTF.Renew();

    /// <summary>加速電圧が変更されたとき。波長を変更、シェルツァーフォーカス変更、レンズ関数描画、ビームの個数計算</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NumericBoxAccVol_ValueChanged(object sender, EventArgs e)
    {
        textBoxScherzer.Text = Scherzer.ToString("f2");

        numericBoxSTEM_ConvergenceAngle_ValueChanged(sender, e);
        NumericBoxObjAperRadius_ValueChanged(sender, e);

        //260801Cl 追加: E0 は EDX の吸収端判定・過電圧・データ範囲 (30-400 kV) を左右するので候補表を作り直す
        if (checkBoxCalculateEdx is not null && checkBoxCalculateEdx.Checked)
            RenewEdxChannelList();
    }
    /// <summary>球面収差が変更されたとき。シェルツァーフォーカス変更、レンズ関数描画</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NumericBoxCs_ValueChanged(object sender, EventArgs e)
    {
        textBoxScherzer.Text = Scherzer.ToString("f2");
        FormCTF.Renew();
    }

    /// <summary>STEMの収束角、検出器範囲が変更されたとき、半径(nm^-1)の換算値を変更</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void numericBoxSTEM_ConvergenceAngle_ValueChanged(object sender, EventArgs e)
    {
        textBoxConvRadius.Text = (Sin(STEM_ConvergenceAngle) / Lambda).ToString("f3");
        textBoxInnerRadius.Text = (Sin(STEM_DetectorInnerAngle) / Lambda).ToString("f3");
        textBoxOuterRadius.Text = (Sin(STEM_DetectorOuterAngle) / Lambda).ToString("f3");
        FormCTF.Renew();

        //260801Cl 追加: 収束角・角度分解能は probe grid の division を決めるので EDX の警告表示を更新
        //(numericBoxSTEM_AngleResolution の ValueChanged も同じハンドラを購読している)
        if (checkBoxCalculateEdx is not null && checkBoxCalculateEdx.Checked)
            RenewEdxSummary();
    }


    /// <summary>デフォーカスが変更されたとき。シリアルモードのデフォーカス開始値変更、レンズ関数描画</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NumericBoxDefocus_ValueChanged(object sender, EventArgs e)
    {
        numericBoxDefocusStart.Value = numericBoxDefocus.Value;
        FormCTF.Renew();
    }
    /// <summary>試料厚みが変更されたとき。シリアルモードの試料厚み開始値変更</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NumericBoxThickness_ValueChanged(object sender, EventArgs e) => numericBoxThicknessStart.Value = numericBoxThickness.Value;

    /// <summary>対物絞りの半径やシフトが変更されたとき。絞り半径のnm^-1換算値を設定、内側ビームの個数計算</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NumericBoxObjAperRadius_ValueChanged(object sender, EventArgs e)
    {
        FormCTF.Renew();

        numericBoxObjAperRadius.Enabled = numericBoxHRTEM_ObjAperX.Enabled = numericBoxHRTEM_ObjAperY.Enabled = !checkBoxOpenAperture.Checked;

        textBoxObjAperRadius.Text = checkBoxOpenAperture.Checked ? HRTEM_ObjAperRadius.ToString() : (Sin(HRTEM_ObjAperRadius) / Lambda).ToString("f3");

        CalculateInsideSpotInfo();
    }

    /// <summary>シリアルモードの試料厚み、ステップ、個数が変更されたとき。厚みリストを変更</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NumericBoxThicknessSerial_ValueChanged(object sender, EventArgs e)
    {
        textBoxThicknessList.Text = numericBoxThicknessStart.Value.ToString();
        for (int i = 1; i < numericBoxThicknessNum.ValueInteger; i++)
            textBoxThicknessList.Text += "\r\n" + (numericBoxThicknessStart.Value + numericBoxThicknessStep.Value * i).ToString();
    }
    /// <summary>シリアルモードのデフォーカス、ステップ、個数が変更されたとき。デフォーカスリストを変更</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NumericBoxDefocusSerial_ValueChanged(object sender, EventArgs e)
    {
        textBoxDefocusList.Text = numericBoxDefocusStart.Value.ToString();
        for (int i = 1; i < numericBoxDefocusNum.ValueInteger; i++)
            textBoxDefocusList.Text += "\r\n" + (numericBoxDefocusStart.Value + numericBoxDefocusStep.Value * i).ToString();
    }
    /// <summary>ブロッホ波の個数が変更されたとき。FormDiffractionSimulator中のブロッホ波個数を変更、スポット情報更新</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NumericBoxNumOfBlochWave_ValueChanged(object sender, EventArgs e)
    {
        if (FormMain.FormDiffractionSimulator.Visible)
            FormMain.FormDiffractionSimulator.numericBoxNumOfBlochWave.Value = numericBoxNumOfBlochWave.Value;
        CalculateInsideSpotInfo();
    }

    /// <summary>現在のパラメータに従って、対物絞り内のスポット情報を計算。SpotInfoのテーブルを更新。FormDiffractionSimulatorが表示されていれば更新</summary>
    public void CalculateInsideSpotInfo()
    {
        if (!this.Visible)
            return;
        var beams = FormMain.Crystal.Bethe.Find_gVectors(FormMain.Crystal.RotationMatrix, new Vector3DBase(0, 0, -1 / Lambda), BlochNum);
        BeamsInside = BetheMethod.ExtractInsideBeams(beams, AccVol, HRTEM_ObjAperRadius, HRTEM_ObjAperX, HRTEM_ObjAperY);
        textBoxNumOfSpots.Text = BeamsInside.Length.ToString();

        if (FormDiffractionSpotInfo.Visible)
        {
            Beams = FormMain.Crystal.Bethe.GetDifractedBeamAmpriltudes(BlochNum, AccVol, FormMain.Crystal.RotationMatrix, ThicknessArray[0]);
            BeamsInside = BetheMethod.ExtractInsideBeams(Beams, AccVol, HRTEM_ObjAperRadius, HRTEM_ObjAperX, HRTEM_ObjAperY);
            FormDiffractionSpotInfo.SetTable(AccVol, BeamsInside);
        }

        if (FormMain.FormDiffractionSimulator.Visible)
            FormMain.FormDiffractionSimulator.Draw();
    }

    #endregion

    #region 他のフォームで結晶回転状態が変更されたとき
    public void RotationChanged()
    {
        if (checkBoxRealTimeSimulation.Checked)
        {
            if (ImageMode == ImageModes.HRTEM)
                SimulateHRTEM(true);
            else if (ImageMode == ImageModes.POTENTIAL)
                simulatePotential(true);
        }

        if (ImageMode == ImageModes.HRTEM)
            CalculateInsideSpotInfo();

        //260802Cl 追加: FormMain はこのメソッドを結晶切替 (crystalControl_CrystalChanged) でも呼ぶ。
        //EDX 候補は構成元素で決まるので、ここで作り直さないと前の結晶の元素が並んだままになり、
        //その状態で Simulate すると「今の結晶に居ない元素」の要求が backend へ行ってしまう。
        //候補の作り直しは (結晶, 加速電圧) が変わったときだけ実際に走るので、回転ドラッグ中の連打は素通りする
        if (checkBoxCalculateEdx is not null && checkBoxCalculateEdx.Checked)
            RenewEdxChannelList();
    }
    #endregion

    #region スポット情報ボタン
    /// <summary>スポット情報ボタン</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ButtonDetailsOfSpots_Click(object sender, EventArgs e)
    {
        FormDiffractionSpotInfo.SetTable(AccVol, BeamsInside);
        FormDiffractionSpotInfo.Visible = true;
    }
    #endregion

    #region チェックボックス On/Offやボタン押下イベントに伴うパネル類のEnabled, visible設定

    /// <summary>連続画像モード関連のチェックボックス</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CheckBoxSerialDefocus_CheckedChanged(object sender, EventArgs e)
    {
        panelSerial.Enabled = radioButtonSerialMode.Checked;

        panelSerialThickness.Enabled = checkBoxSerialThickness.Checked;
        panelSerialDefocus.Enabled = checkBoxSerialDefocus.Checked;
        flowLayoutPanelHorizontalDirection.Enabled = checkBoxSerialThickness.Checked && checkBoxSerialDefocus.Checked;

        groupBoxSampleProperty.Enabled = !(radioButtonSerialMode.Checked && checkBoxSerialThickness.Checked);
        numericBoxDefocus.Enabled = !(radioButtonSerialMode.Checked && checkBoxSerialDefocus.Checked);
    }

    private void CheckBoxShowLabel_CheckedChanged(object sender, EventArgs e)
    {
        colorControlLabel.Enabled = numericBoxLabelFontSize.Enabled = checkBoxShowLabel.Checked;
        colorControlScale.Enabled = numericBoxScaleLength.Enabled = checkBoxShowScale.Checked;

        foreach (var box in Boxes)
            box.Refresh();
    }

    private void RadioButtonHRTEM_CheckedChanged(object sender, EventArgs e)
    {
        //260801Cl 追加: RadioButton の CheckedChanged は「外れた側」でも発火するので、外れた側の分を捨てて二重更新を避ける
        if (sender is RadioButton { Checked: false }) return;

        this.SuspendLayout();
        //260801Cl 追加: モード判定をローカルへ (STEM-EDX は独立モードでなく STEM 内オプション。設計書 §5.9.1-1)
        var mode = ImageMode;
        var isStem = mode == ImageModes.STEM;

        numericBoxDefocus.Enabled = mode != ImageModes.POTENTIAL;

        numericBoxHRTEM_BetaAgnle.Enabled = mode == ImageModes.HRTEM;

        numericBoxCs.Enabled = numericBoxCc.Enabled = numericBoxDeltaV.Enabled =
        groupBoxSampleProperty.Visible = groupBoxNormalization.Visible
               = groupBoxSerialImage.Visible = mode != ImageModes.POTENTIAL;

        checkBoxRealTimeSimulation.Visible = !isStem;

        groupBoxPotentialOption.Visible = mode == ImageModes.POTENTIAL;
        groupBoxHREMoption1.Visible = groupBoxHREMoption2.Visible = mode == ImageModes.HRTEM;
        //260801Cl 変更: option4 (EDX 要求) を STEM 系 GroupBox に追加。表の中身はチェック時のみ展開 (progressive disclosure)
        groupBoxSTEMoption1.Visible = groupBoxSTEMoption2.Visible = groupBoxSTEMoption3.Visible = groupBoxSTEMoption4.Visible = isStem;
        panelEdxDetails.Visible = checkBoxCalculateEdx.Checked;
        //260802Cl 追加: EDX チャネル間の共通スケールは groupBoxNormalization (HRTEM でも見える) の中にあるので、
        //STEM で EDX を表示しているときだけに絞る (groupBoxSTEMoption3 側は GroupBox ごと消えるので不要)
        checkBoxEdxCommonScale.Visible = isStem && radioButtonSTEM_target_EDX.Checked
            && (displayedStemResult?.EdxSignals.Length ?? 0) > 1;
        if (isStem && checkBoxCalculateEdx.Checked)
            RenewEdxChannelList();

        if (mode == ImageModes.POTENTIAL)
            checkBoxCTF.Checked = false;
        checkBoxCTF.Enabled = mode != ImageModes.POTENTIAL;

        this.ResumeLayout(true);

        FormCTF.Renew();
    }

    #endregion

    #region 画像の描画、コピー/保存関連

    private void PictureBox_Paint2(object sender, PaintEventArgs e)
    {
        var box = sender as ScalablePictureBox;
        if (box.PseudoBitmap != null && box.PseudoBitmap.Tag != null && box.PseudoBitmap.Tag is ImageInfo info)
        {
            var conv = new Func<PointD, PointD>(src => box.ConvertToClientPt(src));
            var zoom = box.Zoom;
            drawSymbols(e.Graphics, conv, zoom, info);
        }
    }

    private void drawSymbols(Graphics g, Func<PointD, PointD> conv, double zoom, ImageInfo imageInfo, bool merge = false)
    {
        var reso = imageInfo.Resolution;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

        //ユニットセル
        if (checkBoxShowUnitcell.Checked)
        {
            using Pen penA = new(Color.Red, 1), penB = new(Color.Green, 1), penC = new(Color.Blue, 1);//260718Cl using 化 (描画ごとにリークしていた)
            var zero = new PointD(0, 0);
            var a = new PointD(imageInfo.A.X, -imageInfo.A.Y) / reso * zoom;
            var b = new PointD(imageInfo.B.X, -imageInfo.B.Y) / reso * zoom;
            var c = new PointD(imageInfo.C.X, -imageInfo.C.Y) / reso * zoom;

            var ptOrigin = conv(new PointD(0.5 * imageInfo.Width, 0.5 * imageInfo.Height)) - (a + b + c) / 2;

            foreach (var t in new[] { zero, b, c, b + c })
                g.DrawLine(penA, (ptOrigin + t).ToPointF(), (ptOrigin + t + a).ToPointF());

            foreach (var t in new[] { zero, c, a, c + a })
                g.DrawLine(penB, (ptOrigin + t).ToPointF(), (ptOrigin + t + b).ToPointF());

            foreach (var t in new[] { zero, a, b, a + b })
                g.DrawLine(penC, (ptOrigin + t).ToPointF(), (ptOrigin + t + c).ToPointF());
        }

        //ラベル
        if (checkBoxShowLabel.Checked)
        {
            //var font = new Font(WineCompat.Resolve("Segoe UI Symbol"), (float)numericBoxLabelFontSize.Value); //260610Cl Wine時フォント切替 // (260611Ch) 旧: 未解放
            using var font = new Font(WineCompat.Resolve("Segoe UI Symbol"), (float)numericBoxLabelFontSize.Value); //260610Cl Wine時フォント切替 // (260611Ch)
            using var sb = new SolidBrush(colorControlLabel.Color); // (260611Ch)
            g.DrawString(imageInfo.Text, font, sb, merge ? conv(new PointD(4, 8)).ToPointF() : new PointF(4f, 8f));
        }

        //スケールバー

        if (checkBoxShowScale.Checked)
        {
            //var pen = new Pen(colorControlScale.Color, 3); // (260611Ch) 旧: Pen が未解放
            using var pen = new Pen(colorControlScale.Color, 3); // (260611Ch)
            var pt1 = merge ? conv(new PointD(4, 4)) : new PointD(4f, 4f);
            var pt2 = new PointD(pt1.X + numericBoxScaleLength.Value / reso * zoom, pt1.Y);
            g.DrawLine(pen, (float)pt1.X, (float)pt1.Y, (float)pt2.X, (float)pt2.Y);
        }


    }
    public enum FormatEnum { Meta, PNG, TIFF }
    public enum ActionEnum { Save, Copy }
    public void Save(FormatEnum format, ActionEnum action, string _filename = null)
    {
        //260802Cl 変更: 表示中のグリッド 1 枚分を保存する (1 ペイン構成)。旧: 二ペインを横に連結していた。
        //null 安全と個別保存名は残す (折返しで空くセルがあり得るため / 同じ t/d を別信号で保存したときの上書き防止)
        var row = pictureBoxes.GetLength(0);
        var col = pictureBoxes.GetLength(1);
        if (row == 0 || col == 0)
            return;

        var pseudo = new PseudoBitmap[row, col];
        int width = 0, height = 0;
        for (int r = 0; r < row; r++)
            for (int c = 0; c < col; c++)
            {
                pseudo[r, c] = pictureBoxes[r, c]?.PseudoBitmap;
                if (width == 0 && pseudo[r, c] is { Width: > 0 } first)
                    (width, height) = (first.Width, first.Height);
            }
        if (width == 0 || height == 0)
            return;

        var cells = pseudo.Cast<PseudoBitmap>().Where(c => c is { Width: > 0 }).ToArray();

        //イメージを生成するAction. p が null の場合は全画像、非 null の場合は 1 枚画像
        var draw = new Action<Graphics, PseudoBitmap>((g, p) =>
        {
            if (p != null)
            {
                g.DrawImage(p.GetImage(), new Point(0, 0));
                if (toolStripMenuItemOverprintSymbols.Checked && p.Tag is ImageInfo one)
                    drawSymbols(g, new Func<PointD, PointD>(pt => pt), 1, one);
                return;
            }
            for (int r = 0; r < row; r++)
                for (int c = 0; c < col; c++)
                    if (pseudo[r, c] is { Width: > 0 } cell)
                    {
                        var origin = new Point(c * width, r * height);
                        g.DrawImage(cell.GetImage(), origin);
                        if (toolStripMenuItemOverprintSymbols.Checked && cell.Tag is ImageInfo info)
                            drawSymbols(g, new Func<PointD, PointD>(pt => pt + new PointD(origin.X, origin.Y)), 1, info, true);
                    }
        });

        //メタファイルをセーブしたりコピーしたりするときのアクション (filename が "" の時はコピー)
        // 260716Cl 旧: using var grfx = CreateGraphics(); ipHdc = grfx.GetHdc(); using var ms = new MemoryStream();
        //   try { mf = new Metafile(ms, ipHdc, EmfType.EmfPlusDual); } finally { grfx.ReleaseHdc(ipHdc); } using (mf) { draw → PutEnhMetafileOnClipboard or FileStream 書き出し }
        //   と HDC→Metafile 定型を自前実装していた (260715Ch)。同型が 3 箇所に複製されていたため ClipboardMetafileHelper.SaveOrCopyDrawingAsEnhMetafile へ集約。
        var actionForMetafile = new Action<PseudoBitmap, string>((p, filename) =>
            ClipboardMetafileHelper.SaveOrCopyDrawingAsEnhMetafile(this.Handle, g => draw(g, p), filename)); // 260716Cl

        //260802Cl 追加: 個別保存の 1 枚分のファイル名。ImageInfo.SignalKey (翻訳されない安定キー + t/d index) を含める。
        //旧は "t=..., f=..." だけだったので、同じ t/d を別の信号で保存すると同名になり後者が前者を上書きしていた
        static string CellName(PseudoBitmap cell)
        {
            if (cell.Tag is not ImageInfo info) return "image";
            var td = info.Text.Replace("\r\n", ", ");
            return info.SignalKey is null ? td : $"{info.SignalKey}, {td}";
        }

        //ここから、実際の処理

        //先にファイルダイアログの処理をしてしまう
        var filename = _filename;

        if (_filename == null && action == ActionEnum.Save)
        {
            //var dlg = new SaveFileDialog { Filter = ... }; // 旧: ダイアログが未解放
            using var dlg = new SaveFileDialog { Filter = format switch { FormatEnum.Meta => "*.emf|*.emf", FormatEnum.PNG => "*.png|*.png", _ => "*.tif|*.tif" } }; // (260715Ch)
            if (dlg.ShowDialog() == DialogResult.OK)
                filename = dlg.FileName;
            else
                return;
        }
        //
        if (action == ActionEnum.Save)
        {
            if (string.IsNullOrWhiteSpace(filename))
                return; // (260715Ch) プログラム呼出しで空名が渡された場合も GetFullPath 例外にしない
            //if (!Path.Exists(Path.GetDirectoryName(filename))) // 旧: 相対ファイル名は DirectoryName が空になり、保存せず無言 return
            filename = Path.GetFullPath(filename); // (260715Ch) 相対指定も現在ディレクトリ基準の保存先として扱う
            if (!Directory.Exists(Path.GetDirectoryName(filename))) // (260715Ch)
                return;
            //if (format == FormatEnum.PNG && !filename.ToLower().EndsWith(".png")) // 旧: CurrentCulture 依存
            if (format == FormatEnum.PNG && !filename.EndsWith(".png", StringComparison.OrdinalIgnoreCase)) // (260715Ch)
                filename += ".png";
            else if (format == FormatEnum.TIFF && !filename.EndsWith(".tif", StringComparison.OrdinalIgnoreCase)
                && !filename.EndsWith(".tiff", StringComparison.OrdinalIgnoreCase)) // (260715Ch)
                filename += ".tif";
            else if (format == FormatEnum.Meta && !filename.EndsWith(".emf", StringComparison.OrdinalIgnoreCase)) // (260715Ch)
                filename += ".emf";
        }

        var individually = action == ActionEnum.Save && cells.Length > 1 && toolStripMenuItemSaveIndividually.Checked;
        var (dir, stem) = (Path.GetDirectoryName(filename ?? ""), Path.GetFileNameWithoutExtension(filename ?? "")); // 260716Cl ループ不変のパス演算を巻き上げ

        //メタファイル形式の時
        if (format == FormatEnum.Meta)
        {
            if (individually)
                foreach (var cell in cells)
                    actionForMetafile(cell, Path.Combine(dir, $"{stem} ({CellName(cell)}).emf"));
            else//全体保存 or 全体コピー
                actionForMetafile(null, action == ActionEnum.Save ? filename : "");//filename を "" にすると、コピー
        }
        //Png形式の時
        else if (format == FormatEnum.PNG)
        {
            if (individually)
                foreach (var cell in cells)
                {
                    //var bmp = new Bitmap(width, height); // (260611Ch) 旧: 保存後も Bitmap/Graphics が未解放
                    using var bmp = new Bitmap(width, height); // (260611Ch)
                    using (var g = Graphics.FromImage(bmp)) // (260611Ch)
                        draw(g, cell);
                    bmp.Save(Path.Combine(dir, $"{stem} ({CellName(cell)}).png"), ImageFormat.Png); // (260715Ch)
                }
            else//全体保存 or 全体コピー
            {
                //var bmp = new Bitmap(col * width, row * height); // 旧: Clipboard コピー時に Bitmap が未解放
                using var bmp = new Bitmap(col * width, row * height); // (260715Ch)
                //draw(Graphics.FromImage(bmp), null); // (260611Ch) 旧: Graphics が未解放
                using (var g = Graphics.FromImage(bmp)) // (260611Ch) Bitmap は Clipboard に渡す場合があるため Graphics だけ先に解放
                {
                    g.Clear(Color.White);//260802Cl 追加: 折返しで空くセルを白で埋める (旧は全面を画像が覆う前提だった)
                    draw(g, null);
                }
                if (action == ActionEnum.Save)
                    bmp.Save(filename, ImageFormat.Png);
                else
                    //Clipboard.SetDataObject(bmp); // 旧: 非永続参照のため Bitmap を解放できなかった
                    Clipboard.SetDataObject(bmp, true); // (260715Ch) 永続コピー完了後に using で解放
            }
        }
        else if (format == FormatEnum.TIFF)//Tiff形式 個別保存のみ。**表示レンジではなく生値**を書き出す契約は従来どおり
        {
            foreach (var cell in cells)
                Tiff.Writer(cells.Length == 1 ? filename : Path.Combine(dir, $"{stem} ({CellName(cell)}).tif"),
                    cell.SrcValuesGray, 3, width);
        }
    }

    bool tableLayoutPanelFocused = false;
    private void TableLayoutPanel_Enter(object sender, EventArgs e) => tableLayoutPanelFocused = true;

    private void TableLayoutPanel_Leave(object sender, EventArgs e) => tableLayoutPanelFocused = false;

    private void FormImageSimulator_KeyDown(object sender, KeyEventArgs e)
    {
        if (tableLayoutPanelFocused && e.Control && e.KeyCode == Keys.C)
            ToolStripMenuItemCopyMetafile_Click(sender, new EventArgs());
    }
    private void ToolStripMenuItemSavePNG_Click(object sender, EventArgs e) => Save(FormatEnum.PNG, ActionEnum.Save);
    private void ToolStripMenuItemSaveTIFF_Click(object sender, EventArgs e) => Save(FormatEnum.TIFF, ActionEnum.Save);
    private void ToolStripMenuItemSaveMetafile_Click(object sender, EventArgs e) => Save(FormatEnum.Meta, ActionEnum.Save);
    private void ToolStripMenuItemCopyImage_Click(object sender, EventArgs e) => Save(FormatEnum.PNG, ActionEnum.Copy);
    private void ToolStripMenuItemCopyMetafile_Click(object sender, EventArgs e) => Save(FormatEnum.Meta, ActionEnum.Copy);
    #endregion 画像のコピー/保存

    #region その他イベント
    private void DetailsOfHRTEMSimulationToolStripMenuItem_Click(object sender, EventArgs e)
    {
        // 260604Cl hrtem.pdf の同梱・表示を廃止。GitHub Pages の HRTEM像形成 付録を既定ブラウザで開く。
        //var appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        //new FormPDF(appPath + @"\doc\hrtem.pdf").ShowDialog();
        // 260622Cl: ja/en 二値＋旧 a2-bloch-wave 名を、HelpBaseUrl() (HelpCulture 駆動) ＋現行 a3-bloch-wave へ統一。
        Process.Start(new ProcessStartInfo($"{FormMain.HelpBaseUrl()}appendix/a3-bloch-wave/hrtem/") { UseShellExecute = true });
        //var lang = Thread.CurrentThread.CurrentUICulture.Name == "ja" ? "ja" : "en"; // 260622Cl 旧
        //Process.Start(new ProcessStartInfo($"https://seto77.github.io/ReciPro/{lang}/appendix/a2-bloch-wave/hrtem/") { UseShellExecute = true });
    }


    private void PictureBox_DrawingAreaChanged(object sender, double zoom, PointD center)
    {
        if (SkipEvent) return;

        var box = sender as ScalablePictureBox;
        if (box.PseudoBitmap is null || box.PseudoBitmap.Width == 0)
            return;

        foreach (var b in Boxes)
            if (b != (ScalablePictureBox)sender)
            {
                b.DrawingAreaChanged -= PictureBox_DrawingAreaChanged;
                b.ZoomAndCenter = (zoom, center);
                b.DrawingAreaChanged += PictureBox_DrawingAreaChanged;
            }
    }
    #endregion

    #region 画像表示関連、画像の輝度、カラースケール、ガウシアンぼかし

    public bool SkipEvent = false;

    /// <summary>表示信号 (Both / Elastic / TDS / EDX) が変わったとき。
    /// 260802Cl 変更 (作者指示): EDX を 4 つ目の選択肢にしたので、EDX を選んだときだけ特性 X 線の ComboBox を出し、
    /// カラースケールを Gray へ寄せる (非負量に ColdWarm は不適。設計書 §5.9-5 の「EDX 既定 Gray」)。</summary>
    private void radioButtonSTEM_target_both_CheckedChanged(object sender, EventArgs e)
    {
        //RadioButton の CheckedChanged は「外れた側」でも発火するので、外れた分は捨てて二重更新を避ける
        if (sender is RadioButton { Checked: false }) return;

        if (radioButtonSTEM_target_EDX.Checked && comboBoxScaleColorScale.SelectedIndex != 0)
        {
            SkipEvent = true;
            comboBoxScaleColorScale.SelectedIndex = 0;
            SkipEvent = false;
        }
        GeneratePseudBitmap();
    }

    private void checkBoxIntensityMin_CheckedChanged(object sender, EventArgs e)
    {
        numericBoxIntensityMin.Enabled = checkBoxIntensityMin.Checked;
        numericBoxIntensityMax.Enabled = checkBoxIntensityMax.Checked;
        GeneratePseudBitmap();
    }
    private void RadioButtonPotentialAsMagnitudeAndPhase_CheckedChanged(object sender, EventArgs e)
    {
        flowLayoutPanelMagAndPhase.Visible = panelPhaseScale.Visible = radioButtonPotentialModeMagAndPhase.Checked;
        flowLayoutPanelRealAndImaiginary.Visible = radioButtonPotentialModeRealAndImag.Checked;
    }

    private bool TrackBarAdvancedMin_ValueChanged(object sender, double value)
    {
        if (SkipEvent) return false;
        foreach (var box in Boxes)
            if (box.PseudoBitmap.Tag is ImageInfo { LockIntensity: false })
            {
                box.PseudoBitmap.MaxValue = trackBarAdvancedMax.Value;
                box.PseudoBitmap.MinValue = trackBarAdvancedMin.Value;
                box.drawPictureBox();
            }
        return false;
    }

    private void ComboBoxScaleColorScale_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (SkipEvent) return;

        if (comboBoxScaleColorScale.SelectedIndex == 0)
            scaleImage.SetScaleGray();

        else
            scaleImage.SetScaleColdWarm();
        pictureBoxScaleOfIntensity.Image = scaleImage.GetImage();

        foreach (var box in Boxes)
            if (box.PseudoBitmap.Tag is ImageInfo { LockIntensity: false })
            {
                if (comboBoxScaleColorScale.SelectedIndex == 0)
                    box.PseudoBitmap.SetScaleGray();
                else
                    box.PseudoBitmap.SetScaleColdWarm();
                box.drawPictureBox();
            }
    }
    private void CheckBoxGaussianBlur_CheckedChanged(object sender, EventArgs e)
    {
        if (SkipEvent) return;

        numericBoxGaussianBlurRadius.Enabled = checkBoxGaussianBlur.Checked;

        foreach (var box in Boxes)
            if (box.PseudoBitmap.Tag is ImageInfo { LockIntensity: false })
            {
                if (checkBoxGaussianBlur.Checked)
                    box.PseudoBitmap.SetBlurImage(numericBoxGaussianBlurRadius.Value / numericBoxResolution.Value, PseudoBitmap.BlurModeEnum.Gaussian);
                else
                    box.PseudoBitmap.SetOriginalGray();

                box.drawPictureBox();
            }
    }
    #endregion 画像の輝度、カラースケール、ガウシアンぼかし

    #region 右クリックメニュー
    private void setZeroDefocusToolStripMenuItem_Click(object sender, EventArgs e) => numericBoxDefocus.Value = 0;

    private void setScherzerDefocusToolStripMenuItem_Click(object sender, EventArgs e) => numericBoxDefocus.Value = Scherzer;

    private void zeroAllToolStripMenuItem_Click(object sender, EventArgs e) => numericBoxCc.Value = numericBoxCs.Value = numericBoxHRTEM_BetaAgnle.Value = numericBoxDeltaV.Value = numericBoxDefocus.Value = 0;

    private void presets1ToolStripMenuItem_Click(object sender, EventArgs e)
    {//ARM300F
        AccVol = 300;
        Cs = 0 * 1000000;
        Cc = 2.8 * 1000000;
        DeltaVol = 0.3 / 1000 / 2 / Sqrt(2 * Log(2));
        Defocus = Scherzer;

    }

    private void presets2ToolStripMenuItem_Click(object sender, EventArgs e)
    {//Schottky JEM2100F UHR
        AccVol = 200;
        Cs = 0.5 * 1000000;
        Cc = 1.1 * 1000000;
        DeltaVol = 0.8 / 1000 / 2 / Sqrt(2 * Log(2));
        Defocus = Scherzer;
    }

    private void presets3ToolStripMenuItem_Click(object sender, EventArgs e)
    {//Schottky JEM2100F HR
        AccVol = 200;
        Cs = 1.0 * 1000000;
        Cc = 1.4 * 1000000;
        DeltaVol = 0.8 / 1000 / 2 / Sqrt(2 * Log(2));
        Defocus = Scherzer;
    }

    private void presets4ToolStripMenuItem_Click(object sender, EventArgs e)
    {//LAB6 JEM2010 HR
        AccVol = 200;
        Cs = 1.0 * 1000000;
        Cc = 1.4 * 1000000;
        DeltaVol = 2.0 / 1000 / 2 / Sqrt(2 * Log(2));
        Defocus = Scherzer;
    }


    private void typicalBF02MradToolStripMenuItem_Click(object sender, EventArgs e)
    {
        STEM_ConvergenceAngle = 25.0 / 1000;
        STEM_DetectorInnerAngle = 0;
        STEM_DetectorOuterAngle = 5.0 / 1000;
    }

    private void typicalABF1224MradToolStripMenuItem_Click(object sender, EventArgs e)
    {
        STEM_ConvergenceAngle = 25.0 / 1000;
        STEM_DetectorInnerAngle = 12.0 / 1000;
        STEM_DetectorOuterAngle = 24.0 / 1000;
    }

    private void typicalLAADF2560MradToolStripMenuItem_Click(object sender, EventArgs e)
    {
        STEM_ConvergenceAngle = 25.0 / 1000;
        STEM_DetectorInnerAngle = 26.0 / 1000;
        STEM_DetectorOuterAngle = 60.0 / 1000;
    }

    private void typicalHAADF80250MradToolStripMenuItem_Click(object sender, EventArgs e)
    {
        STEM_ConvergenceAngle = 25.0 / 1000;
        STEM_DetectorInnerAngle = 80.0 / 1000;
        STEM_DetectorOuterAngle = 250.0 / 1000;

    }

    #endregion

    #region プリセットフォーム、CTFグラフフォームの表示/非表示
    private void checkBoxPreset_CheckedChanged(object sender, EventArgs e)
    {
        FormPresets.Visible = checkBoxPreset.Checked;
    }

    private void checkBoxShowLensFunctionGraph_CheckedChanged(object sender, EventArgs e)
    {
        FormCTF.Visible = checkBoxCTF.Checked;
    }
    #endregion

    private void radioButtonPotentialShowPhase_CheckedChanged(object sender, EventArgs e)
    {

    }
}

