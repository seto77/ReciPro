using System.Collections.Generic;
using Crystallography.OpenGL;
//using System.Drawing; //260820Cl 削除 (/simplify): ハーネスへの移動後は未使用
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ReciPro;

/// <summary>
/// 260521Cl 追加 / 260524Cl 全面改修: GUI 統一性監査用に ReciPro の全フォームを構築して PNG 一括保存する開発者向けツール。
/// 260820Cl: 撮影エンジン・診断ロジック・3 モード (--capture / --diagnose / --capture-form) のテンプレートは
/// Crystallography.Controls の <see cref="GuiCaptureHarness"/> へ移した (姉妹アプリ IPAnalyzer / PDIndexer と共用)。
/// ここに残るのは ReciPro 固有部のみ: フォームの配線 (<see cref="WireDependencies"/>)・代表状態づくり
/// (<see cref="PrepareCaptureState"/>)・モード別ショット (<see cref="CaptureExtraShots"/>)・結晶依存子フォームの列挙・
/// OpenGL 描画の注入 (<see cref="RenderGpuControls"/>)。
/// 起動: Program.cs から <c>new GuiCapture().Run(dir)</c> / <c>.Diagnose(file, inflate)</c> / <c>.CaptureSingleForm(type, png)</c>。
/// 通常起動 (引数なし) では一切実行されない。
/// </summary>
// 260617Cl 旧: 多言語化のオーバーフロー診断 (GuiCapture.Diagnose.cs) と内部機構を共有するため partial 化。
// 260820Cl: 3 ファイルを統合したのち、汎用部を GuiCaptureHarness (Crystallography.Controls) へ移動。
//   旧: internal static class GuiCapture (Run / Diagnose / CaptureSingleForm は static だった)
internal sealed class GuiCapture : GuiCaptureHarness
{
    protected override Type MainFormType => typeof(FormMain);

    // 260718Cl: FormSpotIDv2Details は親 (FormSpotIDV2) 未配線の単独生成だと空表示 (スポット未選択で画像・グラフ無し) に
    // なるため、標準列挙ではスキップし、FormSpotIDV2 撮影後 (AfterFormCaptured) に配線済みインスタンスを代表状態 (スポット選択済み) で撮る。
    protected override bool SkipInEnumeration(Type type) => type == typeof(FormSpotIDv2Details);

    /// <summary>--capture と --diagnose で共用: reflection 単独生成した子フォームへ FormMain / 親情報を注入する
    /// (Show 時の NRE 回避＋結晶依存描画の配線)。260617Cl に GuiCapture.Run の inline 分岐から切り出し。</summary>
    // protected override void WireDependencies(Form form, Form main) — 旧: private static void WireCrystalDependencies(Form form, FormMain captureFormMain)
    protected override void WireDependencies(Form form, Form main)
    {
        var captureFormMain = main as FormMain; // 260820Cl: ハーネスは Form で渡す
        if (form is FormTrajectory trajectory)
            trajectory.FormMain = captureFormMain; // (260523Ch) FormTrajectory は単独生成だと Simulate 時に FormMain.Crystal を参照できない
        else if (form is FormEBSD ebsd)
            ebsd.FormMain = captureFormMain; // 260524Cl: Build MasterPattern が FormMain.Crystal を参照するため注入
        else if (form is FormImageSimulator imageSimulator)
            imageSimulator.FormMain = captureFormMain; // 260524Cl: Simulate が FormMain.Crystal を参照するため注入
        else if (form is FormStereonet stereonet)
            stereonet.formMain = captureFormMain; // 260524Cl: 軸/極のプロットは formMain.Crystal が必要
        else if (form is FormRotationMatrix rotation)
            rotation.FormMain = captureFormMain; // 260524Cl: GL の描画 (SetRotation) は FormMain の Euler 角を参照
        else if (form is FormDiffractionSimulator diffractionSimulator)
            diffractionSimulator.formMain = captureFormMain; // 260524Cl: 回折スポット描画 (Draw) は formMain.Crystal が必要
        else if (form is FormDiffractionSimulatorHolder holder)
            holder.FormDiffractionSimulator = captureFormMain?.FormDiffractionSimulator; // 260524Cl: ステレオネット描画の配線
        else if (form is FormALCHEMI alchemi)
            alchemi.FormDiffractionSimulator = captureFormMain?.FormDiffractionSimulator; // 260809Cl: 結晶・加速電圧・方位の取得元
        else if (form is FormSpotIDV2 spotID)
            spotID.FormMain = captureFormMain; // 260524Cl: スポット同定が FormMain を参照
    }

    // 260524Cl 追加: マクロエディタ (FormMacro) は FormMain 直後 (= 反射列挙の最初) に撮る。
    // 引数付き ctor で reflection 単独生成できず、FormMain が Load で配線済みインスタンスを保持しているので基底に渡す。
    // spinel 選択は FormMain の CaptureForm 内 (PrepareCaptureState) で済んでいる。
    protected override FormMacro GetMacroEditor(Form main) => (main as FormMain)?.FormMacro;

    protected override void AfterFormCaptured(Form form, CaptureSession session)
    {
        base.AfterFormCaptured(form, session); // FormMain 直後に FormMacro

        // 260718Cl 追加: FormSpotIDv2Details は FormSpotIDV2 が Load で生成・配線した子フォーム。画像読込+スポット
        // 検出まで済んだこの時点で、非ダイレクトのスポットを1つ選び「画像+4方向プロファイルグラフ表示」状態にして撮る。
        if (form is FormSpotIDV2 spotIDForDetails && spotIDForDetails.FormSpotDetails != null && session.ShouldCapture("FormSpotIDv2Details"))
        {
            try
            {
                spotIDForDetails.PrepareDetailsCaptureForGuiAudit();
                Application.DoEvents();
                session.Capture(spotIDForDetails.FormSpotDetails, "FormSpotIDv2Details");
            }
            catch (Exception ex) { session.Trace($"FormSpotIDv2Details\tWARN\tprepare: {ex.GetType().Name}: {ex.Message}"); }
        }
    }

    // 260523Cl 追加: 親結晶が必要で reflection 列挙では撮れない子フォーム (FormSymmetryInformation /
    // FormBeamInteraction / FormStructureViewer) を、spinel 選択済みの FormMain が持つ配線済みインスタンス経由で撮る。
    protected override IEnumerable<Form> EnumerateDependentForms(Form main) => ((FormMain)main).EnumerateCaptureCrystalDependentForms();

    protected override void BeforeDependentFormCapture(Form child, Form main, Action<string> trace)
    {
        // 260717Cl 追加: FormSymmetryInformation / FormGroupRelations は spinel (Fd-3m) だと対称要素図・
        // 一般位置図が複雑すぎて判読しづらい (ユーザー指示) ため、この 2 フォームだけ代表結晶を
        // rutile (P4_2/mnm) に切り替えて撮る。他の結晶依存フォームは従来どおり spinel。
        var formMain = (FormMain)main;
        bool wantsRutile = child is FormSymmetryInformation or FormGroupRelations;
        var selected = formMain.PrepareCaptureCrystalSelection(wantsRutile ? "rutile" : "spinel");
        trace($"{child.GetType().Name}\tINFO\tcapture crystal={(selected ? formMain.Crystal?.Name : "not found")}");
    }

    protected override void AfterDependentForms(Form main, Action<string> trace)
        => ((FormMain)main).PrepareCaptureCrystalSelection(); // 260717Cl: ループ後は既定の spinel に戻す

    /// <summary>
    /// (260523Ch) フォームを Show しただけではマニュアル用の代表状態にならない画面を、撮影直前に整える。
    /// FormMain は代表結晶を Spinel にし、FormTrajectory は Simulate 相当を実行して GL 軌跡を生成する。
    /// 260524Cl: 重い計算フォーム (FormEBSD の MasterPattern build / FormImageSimulator の Simulate) は、起動だけして
    /// 完了判定は <see cref="GuiCaptureHarness.WaitUntilScreenStable"/> (5秒ごとに撮って変化が無くなったら完了) に委ねる。
    /// Controls 所有のフォーム (FormMacro / FormGroupRelations) は基底の既定実装が扱う。
    /// </summary>
    // 旧: private static void PrepareSpecialCaptureState(Form form, Action<string> trace)
    protected override void PrepareCaptureState(Form form, Action<string> trace)
    {
        EnsureGlfwErrorCallbackInstalled(trace); // 260623Cl: FormEBSD/FormImageSimulator の非同期 GL 起動前に必ず非 throw 化しておく
        if (form is FormMain mainForm)
        {
            var selected = mainForm.PrepareCaptureCrystalSelection();
            trace($"{form.GetType().Name}\tINFO\tcapture crystal={(selected ? mainForm.Crystal?.Name : "not found")}"); // (260523Ch)
            return;
        }

        try
        {
            switch (form)
            {
                case FormTrajectory trajectory:
                    trajectory.PrepareCaptureForGuiAudit(); // (260523Ch) FormTrajectory は Simulate 後でないと GL 軌跡が存在しない (同期・短時間)
                    Application.DoEvents();
                    trace($"{form.GetType().Name}\tINFO\tprepared trajectory simulation");
                    break;
                case FormEBSD ebsd:
                    ebsd.PrepareCaptureForGuiAudit(); // 260524Cl: Build MasterPattern を起動
                    trace($"{form.GetType().Name}\tINFO\ttriggered EBSD master pattern build");
                    WaitUntilScreenStable(form, trace); // 重く非同期なので「画面が変化しなくなったら完了」で待つ
                    // 260725Cl 追加: MasterPattern 構築 (MC/Bethe の非同期完了が ThreadPool スレッドで GL に触れ、WGL
                    // "make context current" 失敗を起こす) の後、3D 幾何ビュー (glControlGeo) が白紙のまま残ることがある。
                    // RenderOpenGlControls の Render() は既存オブジェクトを描き直すだけで復旧しないため、
                    // ここで DrawGeometry() を呼び GL オブジェクトを再送出する。
                    ebsd.DrawGeometry();
                    Application.DoEvents();
                    break;
                case FormImageSimulator imageSimulator:
                    imageSimulator.PrepareCaptureForGuiAudit(); // 260524Cl: Simulate を起動
                    trace($"{form.GetType().Name}\tINFO\ttriggered image simulation");
                    WaitUntilScreenStable(form, trace);
                    break;
                case FormStereonet stereonet:
                    stereonet.PrepareCaptureForGuiAudit(); // 260524Cl: 軸/極をプロット (同期・短時間)。VisibleChanged でも描くが念のため明示
                    Application.DoEvents();
                    trace($"{form.GetType().Name}\tINFO\tprepared stereonet plot");
                    break;
                case FormRotationMatrix rotation:
                    rotation.PrepareCaptureForGuiAudit(); // 260524Cl: SetRotation で GL のトーラス/軸/球を描く (同期・短時間)
                    Application.DoEvents();
                    trace($"{form.GetType().Name}\tINFO\tprepared rotation geometry");
                    break;
                case FormDiffractionSimulator diffractionSimulator:
                    diffractionSimulator.PrepareCaptureForGuiAudit(); // 260524Cl: 回折スポットを描画 (同期・短時間、既定はキネマティカル)
                    Application.DoEvents();
                    trace($"{form.GetType().Name}\tINFO\tprepared diffraction pattern");
                    break;
                case FormDiffractionSimulatorHolder holder:
                    holder.PrepareCaptureForGuiAudit(); // 260524Cl: ホルダーのステレオネット (傾斜方向) を描画 (同期・短時間)
                    Application.DoEvents();
                    trace($"{form.GetType().Name}\tINFO\tprepared holder stereonet");
                    break;
                case FormALCHEMI alchemi:
                    // 260809Cl 追加: 空のフォームでは説明図にならないので、既定条件でロッキングカーブまで計算しておく。
                    // RunAlchemi を同期で呼ぶ設計なので WaitUntilScreenStable は不要 (数秒で戻る)。
                    alchemi.PrepareCaptureForGuiAudit();
                    Application.DoEvents();
                    trace($"{form.GetType().Name}\tINFO\tprepared ALCHEMI rocking curves");
                    break;
                case FormSpotIDV2 spotID:
                    // 260524Cl: SrTiO3 SAD の dm3 を読み込み、スポット検出を起動した代表状態で撮る。検出は非同期かつ重いので画面安定待ち。
                    var sadImage = FindReferenceFile(Path.Combine("DigitalMicroGraph", "SrTiO3", "SADiff100cm_001.dm3"));
                    spotID.PrepareCaptureForGuiAudit(sadImage);
                    trace($"{form.GetType().Name}\tINFO\ttriggered spot-ID find spots ({(sadImage != null ? "image loaded" : "sample image not found")})");
                    WaitUntilScreenStable(form, trace);
                    break;
                default:
                    base.PrepareCaptureState(form, trace); // 260820Cl: Controls 所有フォーム (FormMacro / FormGroupRelations) は基底が扱う
                    break;
            }
        }
        catch (Exception ex)
        {
            trace($"{form.GetType().Name}\tWARN\tPrepareCapture: {ex.GetType().Name}: {ex.Message}");
        }
    }

    /// <summary>ReciPro のモード別・タブ別ショット。Controls 所有フォーム (FormBeamInteraction / FormGroupRelations) は基底が撮る。</summary>
    protected override void CaptureExtraShots(Form form, string name, string outDir, Action<string> trace)
    {
        // 260602Cl 追加: FormImageSimulator はモード (HRTEM/STEM/POTENTIAL) ごとに右側パネル構成が変わるため、
        // 各モードの「全体フォーム画像」を追加で撮る (基底で撮った全体画像は既定=HRTEM の 1 枚だけ)。
        if (form is FormImageSimulator imageSimulatorForModeShots)
            CaptureImageSimulatorModeShots(imageSimulatorForModeShots, name, outDir, trace);

        // 260602Cl 追加: FormDiffractionSimulator は波長×入射ビームの組合せでモード (SAED/PED/X線) ごとに右側パネル構成が
        // 変わるため、各モードの「全体フォーム画像」を追加で撮る (基底で撮った全体画像は既定モードの 1 枚だけ)。
        if (form is FormDiffractionSimulator diffractionSimulatorForModeShots)
        {
            //260807Cl: 菊池ショットは自分で SetCaptureMode("saed") してから撮るので順序に依存しない。
            //一方 CaptureDiffractionSimulatorModeShots は X線モードのまま抜けるため、その後に何か足すときは
            //こちらと同様にモードを明示すること
            CaptureKikuchiDynamicalShot(diffractionSimulatorForModeShots, outDir, trace);
            CaptureDiffractionSimulatorModeShots(diffractionSimulatorForModeShots, name, outDir, trace);
        }

        base.CaptureExtraShots(form, name, outDir, trace); // FormBeamInteraction (線種×タブ) / FormGroupRelations (詳細タブ)
    }

    /// <summary>
    /// 260602Cl 追加: FormImageSimulator の「全体フォーム画像」をモードごとに撮る。
    /// HRTEM / STEM / STEM+EDX / POTENTIAL を順に選び、各モードで Simulate → 画面安定待ち → ウィンドウ全体を CopyFromScreen し、
    /// <c>FormImageSimulator-{hrtem|stem|stem-edx|potential}.png</c> として保存する。コントロール単体クロップは
    /// <see cref="GuiCaptureHarness.RenderHiddenControl"/> によりモード非依存で全 groupBox 分すでに撮れているため、ここでは追加しない。
    /// STEM は計算が重いので、完了判定は既定モードと同じく <see cref="GuiCaptureHarness.WaitUntilScreenStable"/> (画面が止まったら完了) に委ねる。
    /// 既存の <c>FormImageSimulator.png</c> (既定=HRTEM の全体画像) はそのまま残す (index 等の既存参照を壊さない)。
    /// </summary>
    private void CaptureImageSimulatorModeShots(FormImageSimulator sim, string baseName, string outDir, Action<string> trace)
    {
        //260801Cl 追加: STEM-EDX は独立モードではなく STEM 内の追加出力オプション (設計書 §5.9.1) なので、
        //「STEM + EDX チェック ON」を 4 番目の見た目として撮る (元素×殻セレクタが埋まった状態がマニュアルに要る)
        var modes = new[]
        {
            (FormImageSimulator.ImageModes.HRTEM, "hrtem", false),
            (FormImageSimulator.ImageModes.STEM, "stem", false),
            (FormImageSimulator.ImageModes.STEM, "stem-edx", true),
            (FormImageSimulator.ImageModes.POTENTIAL, "potential", false),
        };

        foreach (var (mode, suffix, edx) in modes)
        {
            var name = baseName + "-" + suffix; // 例: FormImageSimulator-stem
            CaptureVariant(sim, name, outDir, trace, //260807Cl /simplify2: 定型部を CaptureVariant へ集約
                apply: () =>
                {
                    sim.ImageMode = mode;               // ラジオ切替で右側パネルの可視性 (RadioButtonHRTEM_CheckedChanged) が更新される
                    sim.EdxEnabled = edx;               // 260801Cl: EDX を計算するかのチェック
                    // 260802Cl 削除: sim.SelectAvailableEdxChannels() — 元素×殻の選択 UI が無くなり、
                    // EDX が ON なら利用可能な特性 X 線を常に全部計算するようになったため不要 (作者指示)
                    sim.CaptureSelectEdxAfterRun = edx; // 260802Cl: 計算後に表示信号を EDX へ (元素マップを撮るため)
                },
                prepare: sim.PrepareCaptureForGuiAudit, // 現在モードの Simulate を起動 (HRTEM/POTENTIAL は同期、STEM は非同期)
                waitStable: true,
                capture: () => CaptureScreen(GetWindowVisualBounds(sim), sim, trace, name, retryIfSolid: true));

            //260801Cl 追加: 元素×殻セレクタは panelModeOptions のスクロール下端に来て全体像には写らないので、
            //選択済みの状態で GroupBox 単体も撮る (既定パスの crop は EDX OFF = 折りたたみ状態のもの)
            if (!edx)
                continue;
            try
            {
                var panel = RenderHiddenControl(sim, sim.EdxOptionGroup, trace);
                if (panel != null)
                    using (panel) panel.Save(Path.Combine(outDir, name + "-selector.png"), ImageFormat.Png);
            }
            catch (Exception ex) { trace($"{name}-selector\tWARN\tselector shot: {ex.GetType().Name}: {ex.Message}"); }
        }
    }

    /// <summary>
    /// 260602Cl 追加: FormDiffractionSimulator の「全体フォーム画像」をモードごとに撮る。
    /// SAED (電子線・平行) / X線 (X線・平行) / PED (電子線・歳差) を順に設定し、各モードで Draw → 画面安定待ち →
    /// ウィンドウ全体を撮影し、<c>FormDiffractionSimulator-{saed|xray|ped}.png</c> として保存する。
    /// コントロール単体クロップは <see cref="RenderHiddenControl"/> によりモード非依存で全 flowLayoutPanel 分すでに撮れているため
    /// ここでは追加しない。PED は歳差の動力学計算で重いので最後に回し、完了判定は <see cref="WaitUntilScreenStable"/> に委ねる。
    /// 既存の <c>FormDiffractionSimulator.png</c> (既定モードの全体画像) はそのまま残す (index 等の既存参照を壊さない)。
    /// </summary>
    private void CaptureDiffractionSimulatorModeShots(FormDiffractionSimulator sim, string baseName, string outDir, Action<string> trace)
    {
        // 260623Cl 修正: 旧順 { saed, xray, ped }。SetCaptureMode("ped") は電子線+歳差を選ぶが波長/エネルギーを
        // 設定しないため、直前の "xray" が入れた 0.154 nm (Cu Kα) を引き継ぎ → 電子線 63 eV 相当の極小エワルド球になり
        // 反射がほぼ消えた空の PED 図になっていた (de 撮影で実害)。"ped" を "saed" (電子線 200 keV の正状態) の直後に
        // 並べ替え、正しい電子線エネルギー/波長を継承させる。"xray" は自前で波長を設定するので最後で問題ない。
        foreach (var suffix in new[] { "saed", "ped", "xray" })
        {
            var name = baseName + "-" + suffix; // 例: FormDiffractionSimulator-saed
            CaptureVariant(sim, name, outDir, trace, //260807Cl /simplify2: 定型部を CaptureVariant へ集約
                apply: () => sim.SetCaptureMode(suffix), // 波長・入射ビーム・強度計算を代表状態へ (CheckedChanged で右側パネルの可視性も更新)
                prepare: sim.PrepareCaptureForGuiAudit,  // 現在モードで SetVector()+Draw() (PED は動力学計算で重い)
                waitStable: true,
                capture: () => CaptureScreen(GetWindowVisualBounds(sim), sim, trace, name, retryIfSolid: true));
        }
    }

    /// <summary>
    /// 260807Cl 追加: tabPageKikuchi はモードでパネルが入れ替わる (Geometric/Kinematical = 線色 + 線幅 /
    /// Dynamical = バンド設定一式) ので、既定モード (Kinematical) の 1 枚だけでは
    /// マニュアルが説明している Dynamical 側の設定が写らない。Dynamical 状態のタブを
    /// <c>{既定のクロップパス}-dynamical.png</c> としてもう 1 枚撮り、撮影後に元の状態へ戻す。
    /// 状態づくり・対象コントロールとも他の特殊撮影と同じくフォーム側の internal フックへ委ねる
    /// (<see cref="FormDiffractionSimulator.PrepareCaptureKikuchiDynamical"/> /
    /// <see cref="FormDiffractionSimulator.CaptureKikuchiTab"/>)。
    /// ⚠タブは選択しない: tabControl_Selecting が菊池レイヤー OFF のときタブ選択を Cancel するので
    /// SelectedTab 代入は当てにならない (実測: 無視されて General が撮れた)。既定の tabPageKikuchi.png と
    /// 同じ RenderHiddenControl 経路 (タブ見出し無しの中身だけ) で撮り、2 枚の見た目を揃える。
    /// </summary>
    private void CaptureKikuchiDynamicalShot(FormDiffractionSimulator form, string outDir, Action<string> trace)
    {
        var (tab, notice) = form.CaptureKikuchiTab;
        if (tab == null)
        {
            trace("FormDiffractionSimulator-kikuchi-dynamical\tWARN\ttabPageKikuchi not found");
            return;
        }
        var name = SanitizeFileName(BuildCapturePath(form, tab)) + "-dynamical";
        (bool Layer, int Mode) previous = default;
        CaptureVariant(form, name, outDir, trace, //260807Cl /simplify2: 定型部を CaptureVariant へ集約
            // 動力学バンドは debounce 300ms + バックグラウンド計算 + 再描画を経てから注記ラベルにバンド数が出る。
            // 待ち時間は機械と Bands 数で変わるので、他のモード撮影と同じく「画面が止まったら完了」に委ねる
            apply: () => previous = form.PrepareCaptureKikuchiDynamical(),
            waitStable: true,
            capture: () =>
            {
                //260807Cl 追加: 注記ラベルの実フォントと寸法を記録する。ja で注記が空白になった事故
                //(原因: resx の Font エントリに type 属性が無く文字列扱いで無視され、9.75pt のまま
                //高さ 16px の矩形に収まらず 1 行も描かれなかった) の再発検知用。
                //⚠Visible はタブ未選択のここでは常に false なので見ない
                if (notice != null)
                {
                    //バンド数は最大 50 (2 桁) なので、1 桁で撮れたときも 2 桁の最悪ケースで採寸する
                    var need = TextRenderer.MeasureText(notice.Text.Replace("(9 ", "(99 "), notice.Font);
                    trace($"{name}\tINFO\tnotice '{notice.Text}' font={notice.Font.Name} {notice.Font.SizeInPoints}pt " +
                          $"box={notice.Width}x{notice.Height} needs={need.Width}x{need.Height} (worst case)" +
                          (need.Height > notice.Height || need.Width > notice.Width ? "\t*** DOES NOT FIT ***" : ""));
                }
                return RenderHiddenControl(form, tab, trace);
            },
            // 撮影後は元のモード・レイヤー状態へ戻す (後続の全体画像・モードショットに影響させない)
            restore: () => form.RestoreCaptureKikuchiState(previous));
    }

    /// <summary>260820Cl: ハーネスの GPU 描画フック。OpenGL (GLControlAlpha) の可視バッファ更新と GLFW エラーコールバックの非 throw 化を注入する。</summary>
    protected override void RenderGpuControls(Form form, Action<string> trace) => RenderOpenGlControls(form, trace);

    // 260623Cl 追加: --capture は Application.Run を回さず DoEvents で描画を進めるため、CurrentUICulture を
    // 持つ UI スレッドに WindowsForms の SynchronizationContext が無い。すると FormEBSD/FormImageSimulator が
    // 起動する BackgroundWorker の RunWorkerCompleted が UI スレッドへマーシャリングされず ThreadPool スレッドで
    // 走り、そこで GL の MakeCurrent が一時的な WGL "要求されたリソースは使用中です" で失敗すると、OpenTK 既定の
    // GLFW エラーコールバック (GLFWProvider.DefaultErrorCallback) が GLFWException を throw し、UI スレッド外の
    // 未処理例外としてプロセスごと落ちる (cap-de-auto が FormEBSD で全滅・60/156 で停止した実害)。
    // 対策: 撮影中だけ GLFW エラーコールバックを「throw せず log する」ものに差し替えて撮影を頑健化する
    // (例外メッセージ自身が案内する GLFWProvider.SetErrorCallback の正規の使い方)。正常時はコールバック未発火＝
    // 挙動不変、GL エラー時のみ throw→log に変わるだけ。後段の RenderOpenGlControls が UI スレッドで
    // 再描画するため、握りつぶした後も EBSD マスターパターン等は正しく撮れる見込み。プロセス終了時に復元不要 (Environment.Exit)。
    private static bool CaptureGlfwErrorCallbackInstalled;

    private static OpenTK.Windowing.GraphicsLibraryFramework.GLFWCallbacks.ErrorCallback CaptureGlfwErrorCallback; // GC 回収防止に保持

    private static void EnsureGlfwErrorCallbackInstalled(Action<string> trace)
    {
        if (CaptureGlfwErrorCallbackInstalled) return;
        try
        {
            CaptureGlfwErrorCallback = (error, description) =>
            {
                // ThreadPool スレッドから呼ばれ得るので共有 log (List) は触らず Console のみ (スレッド安全)。
                try { Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff}\tGLFW\tWARN\tsuppressed during capture: {error}: {description}"); } catch { /* ログ書き込み失敗は無視 */ }
            };
            OpenTK.Windowing.Desktop.GLFWProvider.SetErrorCallback(CaptureGlfwErrorCallback);
            CaptureGlfwErrorCallbackInstalled = true;
            trace("capture\tINFO\tinstalled non-throwing GLFW error callback (capture robustness)");
        }
        catch (Exception ex)
        {
            // GLFW 未初期化等で今は設定できなければ flag を立てず、次に GL を描く時に再試行する。
            trace($"capture\tWARN\tGLFW error callback not installed yet: {ex.GetType().Name}: {ex.Message}");
        }
    }

    /// <summary>
    /// 260524Cl 追加: フォーム内の GLControlAlpha を通常描画 (SwapBuffers あり) して、可視バッファへ最新シーンを出す。
    /// CopyFromScreen は画面の front buffer を読むため、撮影前に GL シーンを画面へ反映しておく必要がある。
    /// </summary>
    private static void RenderOpenGlControls(Form form, Action<string> trace)
    {
        EnsureGlfwErrorCallbackInstalled(trace); // 260623Cl: 初の GL 描画時に GLFW エラーコールバックを非 throw 化
        foreach (var glControl in EnumerateControls(form).OfType<GLControlAlpha>())
        {
            if (glControl.IsDisposed || !glControl.Visible || glControl.Width <= 0 || glControl.Height <= 0)
                continue;
            try { glControl.Render(); } // Render() は renderingForBitmapCapture=false なので SwapBuffers して画面へ表示する
            catch (Exception ex) { trace($"{form.Name}\tWARN\tGL render {glControl.Name}: {ex.GetType().Name}: {ex.Message}"); }
        }
    }

    // 260820Cl 削除: 以下は GuiCaptureHarness (Crystallography.Controls) へ移動した。
    //   Run / CaptureForm / CaptureVariant / ReportTextOverflow / BringToFront / Settle / CaptureScreen / GetWindowVisualBounds /
    //   GetScreenLocation / CaptureControlCrops / CaptureToolStripItemCrops / EnumerateToolStripItems / EnsureToolStripCaptureHostVisible /
    //   EnsureAncestorDropDownsVisible / CloseToolStripDropDowns / BuildToolStripItemCapturePath / EnsureAncestorTabsSelected /
    //   IsEffectivelyVisible / RenderHiddenControl / BuildCapturePath / SanitizeFileName / IsSolidColor / WaitUntilScreenStable /
    //   BitmapsEqual / EnumerateControls / Diagnose 一式 / CaptureSingleForm / DefaultAutoCaptureDir (→ DefaultOutputDir)
    //   CaptureBeamInteractionModeShots / BeamInteractionTabKey / CaptureGroupRelationsDiagramShot (Controls 所有フォームなので基底の既定実装へ)
    //   TryShowMacroSamples (→ FormMacro.PrepareCaptureForGuiAudit。private フィールドの reflection トグルを廃止)
    //   FindReferenceFile (→ 基底の FindReferenceFile。旧: AppContext.BaseDirectory/../../../references と開発機の絶対パス
    //     C:\Users\seto\source\repos\ReciPro\references の 2 候補。基底は RepoRoot()/references の 1 候補 = 前者と同じ場所)
}
