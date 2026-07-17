using System.Collections.Generic;
using Crystallography.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ReciPro;

/// <summary>
/// 260521Cl 追加 / 260524Cl 全面改修: GUI 統一性監査用に ReciPro の全フォームを構築して PNG 一括保存する開発者向けツール。
/// 起動: <c>ReciPro.exe --capture [出力ディレクトリ] [カルチャ(en/ja)]</c>
/// 各フォームを画面内 (0,0) に最前面表示し、<see cref="Graphics.CopyFromScreen(Point, Point, Size)"/> で実描画をそのまま撮る。
/// 以前は画面外 (-32000,-32000) + <see cref="Control.DrawToBitmap(Bitmap, Rectangle)"/> 方式だったが、
/// DrawToBitmap (WM_PRINT) ではタブヘッダー・GraphicsBox の GDI 描画・GPU(OpenGL) 描画が正しく取れず、
/// 重なり合うコントロールの z-order も反転していた (FormCaptureGUI.cs:575 のコメント参照)。
/// そこで対話ツール FormCaptureGUI と同じ CopyFromScreen 方式へ統一した。通常起動 (引数なし) では一切実行されない。
/// </summary>
// 260617Cl: 多言語化のオーバーフロー診断 (GuiCapture.Diagnose.cs) と内部機構を共有するため partial 化。
internal static partial class GuiCapture
{
    /// <summary>
    /// 260522Cl 追加: --capture で言語を強制指定 (en/ja) した場合のカルチャ。
    /// FormMain ctor がレジストリ値で CurrentUICulture を上書きするため、各フォーム構築前に再設定する。
    /// </summary>
    public static System.Globalization.CultureInfo ForcedUICulture;

    // 260524Cl 追加: CopyFromScreen 方式の待機時間。--capture は Application.Run を回さず DoEvents で描画を進めるため、
    // Show / タブ切替 / 結晶選択の後に「描画が画面へ反映される」まで明示的に待ってから撮る必要がある。
    private const int FirstPaintSettleMs = 350; // 初回表示後、フォーム全体が描画されるまでの待ち
    private const int PrepareSettleMs = 450;    // 結晶選択 (spinel) や Trajectory.Simulate 後の再計算・再描画待ち
    private const int TabSwitchSettleMs = 180;  // クロップ時にタブを切り替えた後の再描画待ち

    /// <summary>260525Cl 追加: --capture の出力先を省略したときの既定ディレクトリ (docs/src/assets/cap-{en|ja}-auto)。
    /// Pages 正本化に伴い、自動キャプチャも ReciPro.wiki ではなく docs/src 側へ保存する。
    /// 実行ファイル (bin/...) からリポルート (...\ReciPro) を辿れなければ temp にフォールバックする。</summary>
    private static string DefaultAutoCaptureDir()
    {
        var culture = ForcedUICulture ?? System.Threading.Thread.CurrentThread.CurrentUICulture;
        // 260617Cl 変更: en/ja 固定から SupportedCultures 駆動へ (新言語は cap-de-auto 等。Phase 0)。
        // 旧: var langDir = culture.Name == "ja" ? "cap-ja-auto" : "cap-en-auto";
        var langDir = "cap-" + Crystallography.SupportedCultures.Resolve(culture.Name).Name + "-auto";
        var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
        while (dir != null && dir.Name != "bin")
            dir = dir.Parent;
        var repoRoot = dir?.Parent?.Parent;  // bin → ...\ReciPro\ReciPro → ...\ReciPro (docs/ を持つリポルート)
        return repoRoot != null
            ? Path.Combine(repoRoot.FullName, "docs", "src", "assets", langDir)
            : Path.Combine(Path.GetTempPath(), "recipro-capture-" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + "-" + langDir);
    }

    /// <summary>
    /// --capture の本体。ReciPro 内の parameterless ctor を持つ Form を順に構築し、フォーム単位の PNG を保存する。
    /// FormMain は他フォームの代表状態を作るため最後まで保持する。通常起動からは呼ばない開発者向け経路。
    /// </summary>
    public static void Run(string outDir)
    {
        // 260525Cl: 引数省略時の既定保存先を docs/src/assets/cap-{en|ja}-auto に変更 (Pages 正本化に伴い画像も docs/src 側へ集約)。
        // outDir ??= Path.Combine(Path.GetTempPath(), "recipro-capture-" + DateTime.Now.ToString("yyyyMMdd-HHmmss")); // 260525Cl 旧: temp フォールバック
        outDir ??= DefaultAutoCaptureDir();
        Directory.CreateDirectory(outDir);

        var log = new List<string>();
        void Trace(string s)
        {
            var line = $"{DateTime.Now:HH:mm:ss.fff}\t{s}";
            log.Add(line);
            Console.WriteLine(line);
        }

        // フォームの Load / VisibleChanged 等で投げられた例外を握りつぶす。
        // これをしないと WinForms 標準の未処理例外ダイアログ (モーダル) が出てハーネスがハングする
        // (例: FormCTF を親なしで構築すると get_ImageMode が NullReferenceException)。
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (_, e) => Trace($"\tThreadException\t{e.Exception.GetType().Name}: {e.Exception.Message}");

        Trace($"capture start -> {outDir}");

        // 260524Cl 追加: CopyFromScreen は物理画面を読むため、RDP セッションが非表示・最小化・フォーカス喪失だと
        // "ハンドルが無効です" で失敗する。毎回最初に必ず注意喚起を出す (ユーザー要望)。
        Trace("==================================================================================");
        Trace("[CAUTION] Capture uses CopyFromScreen. Keep the screen VISIBLE and FOCUSED until done.");
        Trace("          Over Remote Desktop (RDP): keep the RDP window in the foreground, and do NOT");
        Trace("          minimize or disconnect. A hidden/minimized session yields blank/failed shots.");
        Trace("[注意] 画面キャプチャ中はウィンドウを前面・表示のまま保ってください。RDP の場合は RDP ウィンドウを");
        Trace("       前面に出したまま最小化・切断しないでください (非表示だと撮影が失敗/真っ黒になります)。");
        Trace("==================================================================================");

        // 起動時に画面が取得可能か 8x8 で試し、不可ならその場で警告する (全フォーム失敗の前に気付けるように)。
        using (var probe = CaptureScreen(new Rectangle(0, 0, 8, 8), null, Trace, "screen-probe"))
        {
            if (probe == null)
                Trace("[CAUTION] Screen capture is currently UNAVAILABLE. Bring the (RDP) session to the foreground now.");
        }

        // 260602Cl 追加: 環境変数 RECIPRO_CAPTURE_ONLY (カンマ区切りの型名部分一致) が指定されたら、その対象だけを撮る。
        // 1 フォームだけ撮り直したいとき (例: FormImageSimulator のモード別全体画像) に、全フォーム再撮影による
        // 差分 churn と実行時間・途中クラッシュのリスクを避けるための開発者向け絞り込み。FormMain は後続フォームへ
        // 親結晶 (spinel) を供給するため、フィルタ対象外でも必ず構築する。RECIPRO_GLDIAG と同様の env-var トグル方式。
        var captureOnly = (Environment.GetEnvironmentVariable("RECIPRO_CAPTURE_ONLY") ?? "")
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        bool ShouldCapture(string typeName) => captureOnly.Length == 0
            || captureOnly.Any(s => typeName.Contains(s, StringComparison.OrdinalIgnoreCase));
        if (captureOnly.Length > 0)
            Trace($"RECIPRO_CAPTURE_ONLY = {string.Join(",", captureOnly)}");

        // ReciPro アセンブリ内の、パラメータレスコンストラクタを持つ Form 派生型を対象にする。
        // FormMain を先頭に構築する (他フォームが静的に FormMain を参照する場合に備える)。
        var types = typeof(FormMain).Assembly.GetTypes()
            .Where(t => typeof(Form).IsAssignableFrom(t) && !t.IsAbstract && t.GetConstructor(Type.EmptyTypes) != null)
            .OrderBy(t => t == typeof(FormMain) ? 0 : 1).ThenBy(t => t.Name)
            .ToList();
        if (captureOnly.Length > 0) // FormMain は親結晶供給に必須なので常に残す
            types = types.Where(t => t == typeof(FormMain) || ShouldCapture(t.Name)).ToList();
        Trace($"{types.Count} form types (parameterless ctor)");

        int ok = 0, fail = 0;
        FormMain captureFormMain = null; // (260523Ch) FormTrajectory の --capture 用 Simulate が Crystal を参照できるよう FormMain を保持する
        foreach (var type in types)
        {
            Form form = null;
            try
            {
                // 260522Cl: 直前のフォーム (特に FormMain) がレジストリ値でカルチャを書き換えても、強制指定があれば戻す。
                if (ForcedUICulture != null)
                    System.Threading.Thread.CurrentThread.CurrentUICulture = ForcedUICulture;
                form = (Form)Activator.CreateInstance(type);
                if (form is FormMain mainForm)
                    captureFormMain = mainForm; // (260523Ch) reflection 順の先頭で作った FormMain を後続フォームの親情報として再利用する
                else
                    WireCrystalDependencies(form, captureFormMain); // 260617Cl: 子フォームへの FormMain/親情報注入を共通化 (--capture と --diagnose で共用)

                if (ShouldCapture(type.Name))
                {
                    CaptureForm(form, type.Name, outDir, Trace, closeAfterCapture: !ReferenceEquals(form, captureFormMain));
                    ok++;

                    // 260524Cl 追加: マクロエディタ (FormMacro) は FormMain 直後 (= 反射列挙の最初) に撮る。
                    // 引数付き ctor で reflection 単独生成できず、FormMain が Load で配線済みインスタンスを保持しているので
                    // ここで撮る。末尾の結晶依存ループまで待つと、GL 多用フォームの NativeWindow finalize で稀にプロセスが
                    // 落ちて撮り損ねるため、GL ウィンドウが溜まる前のこの時点で先に保存しておく。spinel 選択は FormMain の
                    // CaptureForm 内 (PrepareSpecialCaptureState) で済んでいる。
                    if (ReferenceEquals(form, captureFormMain) && captureFormMain.FormMacro != null && ShouldCapture("FormMacro"))
                    {
                        try { CaptureForm(captureFormMain.FormMacro, "FormMacro", outDir, Trace, closeAfterCapture: true); ok++; }
                        catch (Exception ex) { fail++; Trace($"FormMacro\tFAIL\t{ex.GetType().Name}: {ex.Message}"); }
                    }
                }
                else if (ReferenceEquals(form, captureFormMain))
                {
                    // 260602Cl: FormMain がフィルタ対象外でも、後続フォーム (FormImageSimulator 等) の Simulate が
                    // FormMain.Crystal を要るため、表示して spinel 選択だけ済ませ、開いたまま保持する (撮影・保存はしない)。
                    try { form.Show(); } catch (Exception ex) { Trace($"FormMain\tWARN\tShow: {ex.GetType().Name}: {ex.Message}"); }
                    Settle(form, FirstPaintSettleMs, Trace);
                    PrepareSpecialCaptureState(form, Trace); // spinel 選択
                    Settle(form, PrepareSettleMs, Trace);
                }
            }
            catch (Exception ex)
            {
                fail++;
                Trace($"{type.Name}\tFAIL\t{ex.GetType().Name}: {ex.Message}");
            }
            finally
            {
                if (!ReferenceEquals(form, captureFormMain))
                {
                    try { form?.Dispose(); } catch { /* 破棄時例外は無視 */ }
                }
            }
        }

        // 260523Cl 追加: 親結晶が必要で reflection 列挙では撮れない子フォーム (FormSymmetryInformation /
        // FormBeamInteraction / FormStructureViewer) を、spinel 選択済みの FormMain が持つ配線済みインスタンス経由で撮る。
        if (captureFormMain != null)
        {
            foreach (var child in captureFormMain.EnumerateCaptureCrystalDependentForms())
            {
                if (!ShouldCapture(child.GetType().Name)) // 260602Cl: フィルタ対象外の結晶依存子フォームは撮らない
                    continue;
                try
                {
                    // 260717Cl 追加: FormSymmetryInformation / FormGroupRelations は spinel (Fd-3m) だと対称要素図・
                    // 一般位置図が複雑すぎて判読しづらい (ユーザー指示) ため、この 2 フォームだけ代表結晶を
                    // rutile (P4_2/mnm) に切り替えて撮る。他の結晶依存フォームは従来どおり spinel。
                    bool wantsRutile = child is Crystallography.Controls.FormSymmetryInformation or Crystallography.Controls.FormGroupRelations;
                    var selected = captureFormMain.PrepareCaptureCrystalSelection(wantsRutile ? "rutile" : "spinel");
                    Trace($"{child.GetType().Name}\tINFO\tcapture crystal={(selected ? captureFormMain.Crystal?.Name : "not found")}");
                    Application.DoEvents();
                    CaptureForm(child, child.GetType().Name, outDir, Trace, closeAfterCapture: true);
                    ok++;
                }
                catch (Exception ex)
                {
                    fail++;
                    Trace($"{child.GetType().Name}\tFAIL\t{ex.GetType().Name}: {ex.Message}");
                }
            }
            captureFormMain.PrepareCaptureCrystalSelection(); // 260717Cl: ループ後は既定の spinel に戻す
        }

        try { captureFormMain?.Close(); captureFormMain?.Dispose(); } catch { /* 破棄時例外は無視 */ } // (260523Ch)

        Trace($"done: ok={ok} fail={fail}");
        File.WriteAllLines(Path.Combine(outDir, "_capture-log.tsv"), log);
    }

    /// <summary>
    /// 1 つの Form を画面内に最前面表示して撮影する (260524Cl: DrawToBitmap から CopyFromScreen 方式へ変更)。
    /// Show → 最前面化 → 描画待ち → 代表状態準備 → 再描画待ち の後、ウィンドウ全体を CopyFromScreen で撮り、
    /// 続けて Capture=true のコントロール単位クロップを撮る。closeAfterCapture=false は後続フォームの準備に FormMain を使うための例外。
    /// </summary>
    private static void CaptureForm(Form form, string name, string outDir, Action<string> trace, bool closeAfterCapture = true)
    {
        form.StartPosition = FormStartPosition.Manual;
        form.ShowInTaskbar = false;
        form.Location = new Point(0, 0); // CopyFromScreen で実描画を撮るため画面内に表示する
        // Show() で Visible=true にしないと子コントロールが描画されない。
        // Load 等の例外は ThreadException ハンドラへ流れるためモーダル化せず、ハングしない。
        // ただし Show() の呼び出しスタック上で同期的に投げられる例外もあるため、try で囲んで
        // 例外が出てもハンドル/レイアウト生成済みなら撮影を試みる (部分的にでも撮る)。
        try { form.Show(); }
        catch (Exception ex) { trace($"{name}\tWARN\tShow: {ex.GetType().Name}: {ex.Message}"); }

        BringToFront(form);
        Settle(form, FirstPaintSettleMs, trace);
        PrepareSpecialCaptureState(form, trace); // (260523Ch) FormMain は spinel 選択、FormTrajectory は Simulate 相当を実行
        Settle(form, PrepareSettleMs, trace);

        // 260524Cl: prepare 中に子フォーム生成や DoEvents で他アプリ (IDE 等) が前面を奪い、CopyFromScreen が別ウィンドウを
        // 撮ってしまうことがある (例: FormDiffractionSimulator.Draw 後に VS Code が前面化)。撮影直前に再度最前面化する。
        BringToFront(form);
        Settle(form, TabSwitchSettleMs, trace);

        var bounds = GetWindowVisualBounds(form); // タイトルバー等の非クライアント領域も含むウィンドウ全体 (影は除く)
        var bmp = CaptureScreen(bounds, form, trace, name, retryIfSolid: true);
        var captured = bmp != null;
        if (captured)
            using (bmp) bmp.Save(Path.Combine(outDir, name + ".png"), ImageFormat.Png);
        else
            trace($"{name}\tWARN\tfull-form capture failed (RDP screen hidden/minimized?)"); // 撮れなくても次のフォームへ進む
        var cropCount = CaptureControlCrops(form, name, outDir, trace); // 260523Cl: Capture=true のコントロール単位クロップ
        var menuCount = CaptureToolStripItemCrops(form, name, outDir, trace); // 260527Cl: Capture=true の ToolStripItem (メニュー展開) クロップ
        trace($"{name}\t{(captured ? "OK" : "PARTIAL")}\t{bounds.Width}x{bounds.Height}\tCrops={cropCount}\tMenus={menuCount}");

        // 260602Cl 追加: FormImageSimulator はモード (HRTEM/STEM/POTENTIAL) ごとに右側パネル構成が変わるため、
        // 各モードの「全体フォーム画像」を追加で撮る (上で撮った全体画像は既定=HRTEM の 1 枚だけ)。
        if (form is FormImageSimulator imageSimulatorForModeShots)
            CaptureImageSimulatorModeShots(imageSimulatorForModeShots, name, outDir, trace);

        // 260602Cl 追加: FormDiffractionSimulator は波長×入射ビームの組合せでモード (SAED/PED/X線) ごとに右側パネル構成が
        // 変わるため、各モードの「全体フォーム画像」を追加で撮る (上で撮った全体画像は既定モードの 1 枚だけ)。
        if (form is FormDiffractionSimulator diffractionSimulatorForModeShots)
            CaptureDiffractionSimulatorModeShots(diffractionSimulatorForModeShots, name, outDir, trace);

        // 260608Cl 追加: FormBeamInteraction は線種 (X線/電子線/中性子線) でタブ内容が大きく変わる (減衰・輸送/散乱因子の曲線・表が
        // 別物、蛍光は X線専用) ため、線種×タブごとに TabControl 全体をクロップ撮影する。
        if (form is Crystallography.Controls.FormBeamInteraction beamInteractionForModeShots)
            CaptureBeamInteractionModeShots(beamInteractionForModeShots, name, outDir, trace);

        // 260705Cl 追加: FormGroupRelations は既定 (未選択) だとプレースホルダ表示のみなので、代表の部分群を選んだ
        // 詳細タブと Diagram タブ (Bärnighausen グラフ) を追加でクロップ撮影する。
        if (form is Crystallography.Controls.FormGroupRelations groupRelationsForModeShots)
            CaptureGroupRelationsDiagramShot(groupRelationsForModeShots, name, outDir, trace);

        if (closeAfterCapture)
        {
            form.TopMost = false; // (260524Cl) 後続フォームの最前面化を妨げないよう閉じる前に解除
            form.Close();
        }
    }

    /// <summary>
    /// 260602Cl 追加: FormImageSimulator の「全体フォーム画像」をモードごとに撮る。
    /// HRTEM / STEM / POTENTIAL を順に選び、各モードで Simulate → 画面安定待ち → ウィンドウ全体を CopyFromScreen し、
    /// <c>FormImageSimulator-{hrtem|stem|potential}.png</c> として保存する。コントロール単体クロップは
    /// <see cref="RenderHiddenControl"/> によりモード非依存で全 groupBox 分すでに撮れているため、ここでは追加しない。
    /// STEM は計算が重いので、完了判定は既定モードと同じく <see cref="WaitUntilScreenStable"/> (画面が止まったら完了) に委ねる。
    /// 既存の <c>FormImageSimulator.png</c> (既定=HRTEM の全体画像) はそのまま残す (index 等の既存参照を壊さない)。
    /// </summary>
    private static void CaptureImageSimulatorModeShots(FormImageSimulator sim, string baseName, string outDir, Action<string> trace)
    {
        var modes = new[]
        {
            (FormImageSimulator.ImageModes.HRTEM, "hrtem"),
            (FormImageSimulator.ImageModes.STEM, "stem"),
            (FormImageSimulator.ImageModes.POTENTIAL, "potential"),
        };

        foreach (var (mode, suffix) in modes)
        {
            var name = baseName + "-" + suffix; // 例: FormImageSimulator-stem
            try
            {
                sim.ImageMode = mode;                  // ラジオ切替で右側パネルの可視性 (RadioButtonHRTEM_CheckedChanged) が更新される
                Settle(sim, TabSwitchSettleMs, trace); // レイアウト反映を待つ
                BringToFront(sim);
                sim.PrepareCaptureForGuiAudit();       // 現在モードの Simulate を起動 (HRTEM/POTENTIAL は同期、STEM は非同期)
                WaitUntilScreenStable(sim, trace);     // 計算完了 (= 画面が止まる) まで待つ
                BringToFront(sim);
                Settle(sim, TabSwitchSettleMs, trace);

                var bmp = CaptureScreen(GetWindowVisualBounds(sim), sim, trace, name, retryIfSolid: true);
                if (bmp != null)
                    using (bmp) bmp.Save(Path.Combine(outDir, name + ".png"), ImageFormat.Png);
                else
                    trace($"{name}\tWARN\tmode full-form capture failed");
            }
            catch (Exception ex)
            {
                // 1 モードの失敗で残りを諦めない (GuiCapture 全体の「可能な限り次へ進む」方針)。
                trace($"{name}\tWARN\tmode shot: {ex.GetType().Name}: {ex.Message}");
            }
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
    private static void CaptureDiffractionSimulatorModeShots(FormDiffractionSimulator sim, string baseName, string outDir, Action<string> trace)
    {
        // 260623Cl 修正: 旧順 { saed, xray, ped }。SetCaptureMode("ped") は電子線+歳差を選ぶが波長/エネルギーを
        // 設定しないため、直前の "xray" が入れた 0.154 nm (Cu Kα) を引き継ぎ → 電子線 63 eV 相当の極小エワルド球になり
        // 反射がほぼ消えた空の PED 図になっていた (de 撮影で実害)。"ped" を "saed" (電子線 200 keV の正状態) の直後に
        // 並べ替え、正しい電子線エネルギー/波長を継承させる。"xray" は自前で波長を設定するので最後で問題ない。
        foreach (var suffix in new[] { "saed", "ped", "xray" })
        {
            var name = baseName + "-" + suffix; // 例: FormDiffractionSimulator-saed
            try
            {
                sim.SetCaptureMode(suffix);            // 波長・入射ビーム・強度計算を代表状態へ (CheckedChanged で右側パネルの可視性も更新)
                Settle(sim, TabSwitchSettleMs, trace); // レイアウト反映を待つ
                BringToFront(sim);
                sim.PrepareCaptureForGuiAudit();       // 現在モードで SetVector()+Draw() (PED は動力学計算で重い)
                WaitUntilScreenStable(sim, trace);     // 計算完了 (= 画面が止まる) まで待つ
                BringToFront(sim);
                Settle(sim, TabSwitchSettleMs, trace);

                var bmp = CaptureScreen(GetWindowVisualBounds(sim), sim, trace, name, retryIfSolid: true);
                if (bmp != null)
                    using (bmp) bmp.Save(Path.Combine(outDir, name + ".png"), ImageFormat.Png);
                else
                    trace($"{name}\tWARN\tmode full-form capture failed");
            }
            catch (Exception ex)
            {
                // 1 モードの失敗で残りを諦めない (GuiCapture 全体の「可能な限り次へ進む」方針)。
                trace($"{name}\tWARN\tmode shot: {ex.GetType().Name}: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 260608Cl 追加: FormBeamInteraction を「線種 (X線/電子線/中性子線) × タブ」ごとに撮る。
    /// 各タブの内容は線種で大きく変わる (減衰・輸送/散乱因子は曲線・表が別物、蛍光は X線専用で電子/中性子では消える) ため、
    /// 線源を切り替え → 表示中の各 TabPage を選択 → TabControl 全体 (タブ見出し込み) をクロップし、
    /// <c>FormBeamInteraction-{xray|electron|neutron}-{reflections|attenuations|scattering|fluorescence}.png</c> として保存する。
    /// マニュアルの各タブ節 (X線/電子線/中性子線の content-tab) から参照する。標準の全体画像・波長コントロールクロップは
    /// CaptureForm 側で既に撮れている。線種で TabPages が増減する (蛍光は X線のみ) ので ToArray でスナップショットして列挙する。
    /// </summary>
    private static void CaptureBeamInteractionModeShots(Crystallography.Controls.FormBeamInteraction form, string baseName, string outDir, Action<string> trace)
    {
        var beams = new[]
        {
            (Crystallography.WaveSource.Xray, "xray"),
            (Crystallography.WaveSource.Electron, "electron"),
            (Crystallography.WaveSource.Neutron, "neutron"),
        };
        foreach (var (src, beamSuffix) in beams)
        {
            try
            {
                form.SetCaptureBeam(src);              // 線源切替 → ApplyBeamDependentVisibility で蛍光タブの増減 (X線のみ) も反映
                Settle(form, TabSwitchSettleMs, trace);
                BringToFront(form);
                var tc = form.CaptureTabControl;
                foreach (var tab in tc.TabPages.Cast<TabPage>().ToArray()) // 線種で増減するのでスナップショット
                {
                    var name = baseName + "-" + beamSuffix + "-" + BeamInteractionTabKey(tab.Name);
                    try
                    {
                        tc.SelectedTab = tab;             // SelectedIndexChanged → UpdateAllTabs で当該タブを計算
                        Settle(form, TabSwitchSettleMs, trace);
                        BringToFront(form);
                        Settle(form, TabSwitchSettleMs, trace);
                        var bmp = CaptureScreen(new Rectangle(GetScreenLocation(tc), tc.Size), form, trace, name, retryIfSolid: true);
                        if (bmp != null)
                            using (bmp) bmp.Save(Path.Combine(outDir, name + ".png"), ImageFormat.Png);
                        else
                            trace($"{name}\tWARN\tbeam-tab capture failed");
                    }
                    catch (System.Exception ex) { trace($"{name}\tWARN\tbeam-tab shot: {ex.GetType().Name}: {ex.Message}"); }
                }
            }
            catch (System.Exception ex) { trace($"{baseName}-{beamSuffix}\tWARN\tbeam mode: {ex.GetType().Name}: {ex.Message}"); }
        }
        try { form.SetCaptureBeam(Crystallography.WaveSource.Xray); } catch { /* 撮影後 close 前に既定 (X線) へ戻す */ }
    }

    /// <summary>260705Cl 追加: FormGroupRelations の残りの詳細タブ (既定選択の Matrix は全体画像で撮れている) を
    /// クロップ撮影する。PrepareCaptureForGuiAudit (代表部分群の選択) は PrepareSpecialCaptureState 側で既に実行済み。</summary>
    private static void CaptureGroupRelationsDiagramShot(Crystallography.Controls.FormGroupRelations form, string baseName, string outDir, Action<string> trace)
    {
        try
        {
            var tc = form.CaptureTabControl;
            //foreach (var tabName in new[] { "tabOrbit", "tabDomains", "tabReflections", "tabDiagram" })
            foreach (var tabName in new[] { "tabOrbit", "tabDomains", "tabReflections", "tabDiagram", "tabPointGroups", "tabElements" }) // 260712Cl: 点群 Hasse 図タブ (③-4)、260713Cl: 対称要素タブ (③-2)
            {
                var tab = tc.TabPages.Cast<TabPage>().FirstOrDefault(t => t.Name == tabName);
                if (tab == null) { trace($"{baseName}-{tabName}\tWARN\t{tabName} not found"); continue; }
                tc.SelectedTab = tab;
                Settle(form, TabSwitchSettleMs, trace);
                BringToFront(form);
                Settle(form, TabSwitchSettleMs, trace);
                var name = baseName + "-" + tabName;
                // 260713Cl: tabElements は 2 パス重ね描き (透明ビットマップ×2 + ColorMatrix) で GDI 負荷が高く、
                // RDP の CopyFromScreen が「ハンドル作成エラー (Win32Exception)」で失敗しやすい。pictureBox の
                // Image を直接クローン保存すれば screen-capture 依存を外せて確実 (プログラム描画なので画素も厳密)。
                // 260717Cl: 既定フォームサイズだと左右分割後の各図が小さく、立方晶 (spinel) の要素/一般位置が
                // 判読不能になるため、この direct-image 撮影の間だけフォームを一時拡大する (他タブの CopyFromScreen
                // 撮影サイズは従来どおり)。
                if (tabName == "tabElements" && tab.Controls.Find("pictureBoxElements", true).FirstOrDefault() is PictureBox pbElem && pbElem.Image != null)
                {
                    var originalSize = form.Size; // 260717Cl 追加 (一時拡大)
                    try
                    {
                        form.Size = new Size(1250, 860);
                        Settle(form, TabSwitchSettleMs, trace); // SizeChanged → RenderElements 再描画を反映
                        using var clone = new Bitmap(pbElem.Image);
                        clone.Save(Path.Combine(outDir, name + ".png"), ImageFormat.Png);
                        trace($"{name}\tOK\tdirect image {pbElem.Image.Width}x{pbElem.Image.Height}");
                    }
                    finally
                    {
                        form.Size = originalSize;
                        Settle(form, TabSwitchSettleMs, trace);
                    }
                    continue;
                }
                var bmp = CaptureScreen(new Rectangle(GetScreenLocation(tc), tc.Size), form, trace, name, retryIfSolid: true);
                if (bmp != null)
                    using (bmp) bmp.Save(Path.Combine(outDir, name + ".png"), ImageFormat.Png);
                else
                    trace($"{name}\tWARN\t{tabName} capture failed");
            }
        }
        catch (System.Exception ex) { trace($"{baseName}-diagram\tWARN\tdiagram shot: {ex.GetType().Name}: {ex.Message}"); }
    }

    /// <summary>TabPage 名 → ファイル名サフィックス。260608Cl 追加。</summary>
    private static string BeamInteractionTabKey(string tabPageName) => tabPageName switch
    {
        "tabPageReflections" => "reflections",
        "tabPageAttenuations" => "attenuations",
        "tabPageScatteringFactors" => "scattering",
        "tabPageFluorescence" => "fluorescence",
        _ => tabPageName,
    };

    /// <summary>260524Cl 追加: CopyFromScreen の前に対象フォームを通常表示・最前面・アクティブ化する。</summary>
    private static void BringToFront(Form form)
    {
        try
        {
            if (form.WindowState != FormWindowState.Normal)
                form.WindowState = FormWindowState.Normal;
            form.TopMost = true; // 無人実行中に他ウィンドウが被って映り込むのを防ぐ
            form.BringToFront();
            form.Activate();
            if (form.IsHandleCreated)
                SetForegroundWindow(form.Handle); // RDP でフォーカスが他へ移っても撮影対象を前面へ取り戻す
        }
        catch { /* 表示状態変更時の例外は無視 (撮影は後段で最善努力) */ }
    }

    /// <summary>
    /// 260524Cl 追加: 指定ミリ秒のあいだ DoEvents を回して描画を画面へ反映させる。
    /// --capture は Application.Run を回さないため、CopyFromScreen の前にこの明示的な描画待ちが要る。
    /// OpenGL 領域は通常の Invalidate では更新されないことがあるので、毎回 Render() で可視バッファへ最新シーンを出す。
    /// </summary>
    private static void Settle(Form form, int ms, Action<string> trace)
    {
        try { form.Refresh(); } catch { /* Refresh 時例外は無視 */ }
        RenderOpenGlControls(form, trace);
        var until = Environment.TickCount + Math.Max(ms, 0);
        do
        {
            Application.DoEvents();
            System.Threading.Thread.Sleep(15);
        } while (Environment.TickCount < until);
    }

    // 260623Cl 追加: --capture は Application.Run を回さず DoEvents で描画を進めるため、CurrentUICulture を
    // 持つ UI スレッドに WindowsForms の SynchronizationContext が無い。すると FormEBSD/FormImageSimulator が
    // 起動する BackgroundWorker の RunWorkerCompleted が UI スレッドへマーシャリングされず ThreadPool スレッドで
    // 走り、そこで GL の MakeCurrent が一時的な WGL "要求されたリソースは使用中です" で失敗すると、OpenTK 既定の
    // GLFW エラーコールバック (GLFWProvider.DefaultErrorCallback) が GLFWException を throw し、UI スレッド外の
    // 未処理例外としてプロセスごと落ちる (cap-de-auto が FormEBSD で全滅・60/156 で停止した実害)。
    // 対策: 撮影中だけ GLFW エラーコールバックを「throw せず log する」ものに差し替えて撮影を頑健化する
    // (例外メッセージ自身が案内する GLFWProvider.SetErrorCallback の正規の使い方)。正常時はコールバック未発火＝
    // 挙動不変、GL エラー時のみ throw→log に変わるだけ。後段の RenderOpenGlControls(1007) が UI スレッドで
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

    private const int CaptureMaxAttempts = 5; // 260524Cl: CopyFromScreen 失敗時の最大試行回数

    /// <summary>
    /// 260524Cl 追加 / 堅牢化: 画面上の指定矩形を CopyFromScreen で撮ってビットマップ化する。
    /// RDP セッションが非表示・最小化・フォーカス喪失だと CopyFromScreen は <see cref="System.ComponentModel.Win32Exception"/>
    /// ("ハンドルが無効です") を投げたり全面単色を返したりする。そこで失敗時は foregroundForm を取り直して待ち、
    /// 数回まで再試行する。最終的に撮れなければ null を返し、呼び出し側は画像を保存せず次へ進む (1 枚の失敗で全体を止めない)。
    /// retryIfSolid=true (フォーム全体) では全面単色も「実描画が読めていない」とみなして再試行・null 化する
    /// (黒画像で既存 Wiki 画像を上書きしないため)。クロップ (retryIfSolid=false) の単色は呼び出し側 IsSolidColor が正規にスキップする。
    /// </summary>
    private static Bitmap CaptureScreen(Rectangle screenRect, Form foregroundForm = null, Action<string> trace = null, string label = null, bool retryIfSolid = false)
    {
        int w = Math.Max(screenRect.Width, 1), h = Math.Max(screenRect.Height, 1);
        for (int attempt = 1; attempt <= CaptureMaxAttempts; attempt++)
        {
            Bitmap bmp = null;
            try
            {
                bmp = new Bitmap(w, h);
                using (var g = Graphics.FromImage(bmp))
                    g.CopyFromScreen(screenRect.Location, Point.Empty, new Size(w, h));

                if (!retryIfSolid || !IsSolidColor(bmp))
                    return bmp; // 成功

                bmp.Dispose(); // 全面単色 = RDP で実描画が読めていない可能性。破棄して再試行。
                trace?.Invoke($"{label}\tWARN\tCopyFromScreen blank attempt {attempt}/{CaptureMaxAttempts}");
            }
            catch (Exception ex)
            {
                bmp?.Dispose();
                trace?.Invoke($"{label}\tWARN\tCopyFromScreen attempt {attempt}/{CaptureMaxAttempts}: {ex.GetType().Name}: {ex.Message}");
            }

            if (attempt == CaptureMaxAttempts)
                break;
            if (foregroundForm != null)
                BringToFront(foregroundForm); // RDP の一時的なフォーカス喪失対策にフォアグラウンドを取り直す
            System.Threading.Thread.Sleep(400 * attempt); // 線形バックオフ
            Application.DoEvents();
        }
        return null;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    private struct RECT { public int Left, Top, Right, Bottom; }

    [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
    private static extern int DwmGetWindowAttribute(IntPtr hwnd, int attr, out RECT rect, int size);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd); // 260524Cl: CopyFromScreen 前に対象ウィンドウを確実に前面へ

    private const int DWMWA_EXTENDED_FRAME_BOUNDS = 9;

    /// <summary>
    /// 260524Cl 追加: CopyFromScreen で撮るウィンドウ全体の矩形を求める。
    /// WinForms の <see cref="Control.Bounds"/> (GetWindowRect 由来) は Win10/11 の不可視リサイズ枠を含むため
    /// 実際の描画ウィンドウより一回り大きく、そのまま CopyFromScreen すると下端などに背後のデスクトップが写り込む。
    /// DWM の実視覚矩形 (DWMWA_EXTENDED_FRAME_BOUNDS、影は除く) が取れればそれを使い、失敗時のみ Bounds に戻す。
    /// </summary>
    private static Rectangle GetWindowVisualBounds(Form form)
    {
        try
        {
            if (form.IsHandleCreated
                && DwmGetWindowAttribute(form.Handle, DWMWA_EXTENDED_FRAME_BOUNDS, out var r, System.Runtime.InteropServices.Marshal.SizeOf<RECT>()) == 0
                && r.Right > r.Left && r.Bottom > r.Top)
                return new Rectangle(r.Left, r.Top, r.Right - r.Left, r.Bottom - r.Top);
        }
        catch { /* P/Invoke 失敗時は Bounds にフォールバック */ }
        return form.Bounds;
    }

    /// <summary>260524Cl 追加: コントロールの実際の左上スクリーン座標を求める (FormCaptureGUI と同一規則)。</summary>
    private static Point GetScreenLocation(Control control)
        => control is Form ? control.Bounds.Location
         : control.Parent != null ? control.Parent.PointToScreen(control.Location)
         : control.PointToScreen(Point.Empty);

    /// <summary>
    /// 260523Cl 追加 / 260524Cl 改修: Designer で <c>Capture=true</c> を付けたコントロール単位のクロップを、対話 UI を出さずに生成する。
    /// 各対象を CopyFromScreen で個別に撮る (FormCaptureGUI と同方式)。命名は手動キャプチャと同じ規則
    /// (form.Name 起点、SplitterPanel/ToolStripPanel/無名は除外) で、既存の Wiki 画像 raw URL を壊さない。
    /// Crystallography.Controls 側へは <see cref="Crystallography.Controls.CaptureExtender.IsCaptureEnabled"/>
    /// (Capture=true 判定) だけを依存し、対象列挙・パス命名・空白判定はすべてここで行う。
    /// </summary>
    /// <returns>保存できたクロップ数。</returns>
    private static int CaptureControlCrops(Form form, string name, string outDir, Action<string> trace)
    {
        var count = 0;
        // Capture=true のコントロールを列挙する。ToolStripItem (メニュードロップダウン展開等) は
        // 別ウィンドウのため非対話では撮らない (= EnumerateControls には現れないので自然に除外される)。
        foreach (var control in EnumerateControls(form))
        {
            if (control is Form || string.IsNullOrEmpty(control.Name) || control.IsDisposed || control.Width <= 0 || control.Height <= 0)
                continue;
            if (!Crystallography.Controls.CaptureExtender.IsCaptureEnabled(control))
                continue;

            try
            {
                // タブを選択し直したときだけ再描画を待つ (毎回の長い待機を避ける)。
                if (EnsureAncestorTabsSelected(control))
                    Settle(form, TabSwitchSettleMs, trace);

                Bitmap crop;
                if (!IsEffectivelyVisible(form, control))
                {
                    // 260524Cl: 既定で Visible=false の Capture=true コントロール (例: 歳差モードでのみ表示される
                    // flowLayoutPanelPED) は通常表示に現れないため、一時的に可視化・最前面化して単体で撮る。
                    crop = RenderHiddenControl(form, control, trace);
                    if (crop == null)
                        continue;
                }
                else
                {
                    // TabPage は親 TabControl 全体 (タブ見出し込み) を撮る (FormCaptureGUI と同じ見た目)。
                    var region = control is TabPage tabPage && tabPage.Parent is TabControl tabControl ? (Control)tabControl : control;
                    region.Refresh();
                    // 260527Cl: 大きい二重バッファ領域 (例 FormSymmetryInformation.tableLayoutPanel1 内の対称要素/一般位置の図) は
                    // region.Refresh() 直後の 1 回 DoEvents では描画が画面 (front buffer) へ反映されず単色で撮れることがある
                    // (全体像は撮れているのにクロップだけ空白になる)。全体像と同じく Settle で描画を反映させ、
                    // さらに retryIfSolid=true で単色フレームを掴んだら数回撮り直す (本当に単色の領域は最終的に null=スキップ)。
                    Settle(form, TabSwitchSettleMs, trace); // Refresh + RenderOpenGlControls + DoEvents ループ
                    crop = CaptureScreen(new Rectangle(GetScreenLocation(region), region.Size), form, trace, $"{name}.{control.Name}", retryIfSolid: true);
                    if (crop == null)
                        continue; // RDP 画面が一時的に取得不能 or 何度撮っても単色なら、このクロップは諦めて次へ
                }

                using (crop)
                {
                    if (IsSolidColor(crop))
                        continue; // Visible=false のパネル等で単色になったクロップは保存しない

                    var fileName = SanitizeFileName(BuildCapturePath(form, control)) + ".png";
                    crop.Save(Path.Combine(outDir, fileName), ImageFormat.Png);
                    count++;
                }
            }
            catch (Exception ex)
            {
                // 1 コントロールの失敗で残りのクロップを諦めない (GuiCapture 全体の「可能な限り次へ進む」方針)。
                trace($"{name}\tWARN\tcrop {control.Name}: {ex.GetType().Name}: {ex.Message}");
            }
        }
        return count;
    }

    /// <summary>
    /// 260527Cl 追加: Designer で <c>Capture=true</c> を付けた ToolStripItem (メニュー項目等) のドロップダウンを
    /// 非対話で撮る。対話ツール FormCaptureGUI.CaptureToolStripItem と同じ方式 (祖先含めて ShowDropDown し、
    /// 開いた DropDown / ContextMenuStrip / Owner ToolStrip を CopyFromScreen) ・同じ命名規則 (form.Name 起点で
    /// owner ToolStrip の Control パス + 項目の OwnerItem 連鎖の名前を "." 連結) で生成する。撮影後は開いた
    /// ドロップダウンを閉じ、後続フォーム/クロップの撮影を妨げないようにする。CaptureControlCrops は Control しか
    /// 列挙しない (メニュードロップダウンは別ウィンドウ) ため、ここで補完する。
    /// </summary>
    /// <returns>保存できたメニュークロップ数。</returns>
    private static int CaptureToolStripItemCrops(Form form, string name, string outDir, Action<string> trace)
    {
        var count = 0;
        foreach (var item in EnumerateToolStripItems(form))
        {
            if (string.IsNullOrEmpty(item.Name) || !Crystallography.Controls.CaptureExtender.IsCaptureEnabled(item))
                continue;

            try
            {
                var host = EnsureToolStripCaptureHostVisible(item);
                if (host == null || host.IsDisposed || host.Width <= 0 || host.Height <= 0)
                    continue;

                host.Refresh();
                Application.DoEvents();
                System.Threading.Thread.Sleep(150); // ドロップダウンが画面へ出るまで待つ

                var crop = CaptureScreen(new Rectangle(host.PointToScreen(Point.Empty), host.Size), form, trace, $"{name}.{item.Name}");
                if (crop != null)
                    using (crop)
                    {
                        if (!IsSolidColor(crop))
                        {
                            var fileName = SanitizeFileName(BuildToolStripItemCapturePath(form, item)) + ".png";
                            crop.Save(Path.Combine(outDir, fileName), ImageFormat.Png);
                            count++;
                        }
                    }
            }
            catch (Exception ex)
            {
                trace($"{name}\tWARN\tmenu-crop {item.Name}: {ex.GetType().Name}: {ex.Message}");
            }
            finally
            {
                try { CloseToolStripDropDowns(item); } catch { /* ドロップダウンのクローズ失敗は無視 */ }
            }
        }
        return count;
    }

    /// <summary>260527Cl 追加: フォーム内の全 ToolStripItem を列挙する (Controls 配下の ToolStrip + designer field の ContextMenuStrip 等。ドロップダウン項目も再帰)。</summary>
    private static IEnumerable<ToolStripItem> EnumerateToolStripItems(Form form)
    {
        var toolStrips = new HashSet<ToolStrip>();
        foreach (var toolStrip in EnumerateControls(form).OfType<ToolStrip>())
            toolStrips.Add(toolStrip);
        // Controls 配下にない ToolStrip (ContextMenuStrip 等) を designer field から拾う (FormCaptureGUI.GetOwnedToolStrips と同趣旨)
        for (var type = form.GetType(); type != null; type = type.BaseType)
            foreach (var field in type.GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.DeclaredOnly))
                if (typeof(ToolStrip).IsAssignableFrom(field.FieldType) && field.GetValue(form) is ToolStrip ownedToolStrip)
                    toolStrips.Add(ownedToolStrip);

        var visited = new HashSet<ToolStripItem>();
        foreach (var toolStrip in toolStrips)
            foreach (var item in EnumerateToolStripItems(toolStrip.Items, visited))
                yield return item;
    }

    private static IEnumerable<ToolStripItem> EnumerateToolStripItems(ToolStripItemCollection items, HashSet<ToolStripItem> visited)
    {
        foreach (ToolStripItem item in items)
        {
            if (!visited.Add(item)) continue;
            yield return item;
            if (item is ToolStripDropDownItem dropDownItem && dropDownItem.HasDropDownItems)
                foreach (var child in EnumerateToolStripItems(dropDownItem.DropDownItems, visited))
                    yield return child;
        }
    }

    /// <summary>260527Cl 追加: 対象項目を撮るためのホスト (開いた DropDown / ContextMenuStrip / Owner ToolStrip) を可視化して返す (FormCaptureGUI と同方式)。</summary>
    private static ToolStrip EnsureToolStripCaptureHostVisible(ToolStripItem item)
    {
        EnsureAncestorDropDownsVisible(item);

        if (item is ToolStripDropDownItem dropDownItem && dropDownItem.HasDropDownItems)
        {
            if (!dropDownItem.DropDown.Visible)
            {
                dropDownItem.ShowDropDown(); // File のような親メニュー項目はドロップダウン全体を開いてから撮る
                dropDownItem.DropDown.Refresh();
                Application.DoEvents();
                System.Threading.Thread.Sleep(200);
            }
            return dropDownItem.DropDown;
        }

        if (item.Owner is ContextMenuStrip contextMenuStrip)
        {
            if (!contextMenuStrip.Visible && contextMenuStrip.SourceControl != null)
            {
                contextMenuStrip.Show(contextMenuStrip.SourceControl, new Point(0, contextMenuStrip.SourceControl.Height));
                Application.DoEvents();
                System.Threading.Thread.Sleep(200);
            }
            return contextMenuStrip;
        }

        return item.Owner is ToolStripDropDown toolStripDropDown ? toolStripDropDown : item.Owner;
    }

    /// <summary>260527Cl 追加: 対象項目の祖先ドロップダウンを順に開く (ネストしたサブメニュー対応)。</summary>
    private static void EnsureAncestorDropDownsVisible(ToolStripItem item)
    {
        if (item.OwnerItem is not ToolStripDropDownItem ownerItem) return;
        EnsureAncestorDropDownsVisible(ownerItem);
        if (!ownerItem.DropDown.Visible)
        {
            ownerItem.ShowDropDown();
            ownerItem.DropDown.Refresh();
            Application.DoEvents();
            System.Threading.Thread.Sleep(200);
        }
    }

    /// <summary>260527Cl 追加: 撮影のために開いたドロップダウンを子→親の順に閉じる (後続の撮影を妨げないため)。</summary>
    private static void CloseToolStripDropDowns(ToolStripItem item)
    {
        for (var current = item; current != null; current = current.OwnerItem)
            if (current is ToolStripDropDownItem dropDownItem && dropDownItem.HasDropDownItems && dropDownItem.DropDown.Visible)
                dropDownItem.HideDropDown();
        Application.DoEvents();
    }

    /// <summary>
    /// 260527Cl 追加: ToolStripItem のキャプチャ用パス (= クロップのファイル名 stem)。owner ToolStrip までの Control パス
    /// (BuildCapturePath。ToolStripPanel 等は除外) に、項目の OwnerItem 連鎖の名前を "." 連結する。
    /// FormCaptureGUI の対話キャプチャと同じ stem になるようにし、本文の `[ここに画像 fileToolStripMenuItem]` 等の指定で解決できるようにする。
    /// </summary>
    private static string BuildToolStripItemCapturePath(Form form, ToolStripItem item)
    {
        var segments = new List<string>();
        for (var current = item; current != null; current = current.OwnerItem)
            segments.Add(string.IsNullOrEmpty(current.Name) ? current.GetType().Name : current.Name);
        segments.Reverse();

        var top = item;
        while (top.OwnerItem != null)
            top = top.OwnerItem;
        var prefix = top.Owner != null ? BuildCapturePath(form, top.Owner) : form.Name;
        return prefix + "." + string.Join(".", segments);
    }

    /// <summary>
    /// 260523Cl 追加 / 260524Cl 改修: コントロールの祖先 TabPage を順に選択し、クロップ時に可視化する。
    /// いずれかのタブ選択を実際に変更したら true (呼び出し側が再描画待ちを入れるため)。
    /// 260524Cl: TabControl を BringToFront して、重なる兄弟コントロール (例: FormStereonet/FormDiffractionSimulator の
    /// stereonet graphicsBox。これらのフォームはタブクリック時に同様の前後入れ替えを行う) より前面に出し、
    /// タブ内容がその背後描画で隠れて撮れない問題を防ぐ。全体像は crop より前に撮るので影響しない。
    /// </summary>
    private static bool EnsureAncestorTabsSelected(Control control)
    {
        var changed = false;
        for (var c = control; c != null; c = c.Parent)
        {
            if (c is TabPage tabPage && tabPage.Parent is TabControl tabControl)
            {
                if (tabControl.SelectedTab != tabPage)
                {
                    tabControl.SelectedTab = tabPage;
                    changed = true;
                }
                tabControl.BringToFront(); // 重なる graphicsBox 等より前面へ (フォーム側のタブ前後入れ替えロジックと同じ z-order 効果)
                tabControl.Refresh();
            }
        }
        return changed;
    }

    /// <summary>260524Cl 追加: control から form まで全て Visible なら true。途中に Visible=false があれば false。</summary>
    private static bool IsEffectivelyVisible(Form form, Control control)
    {
        for (var c = control; c != null && !ReferenceEquals(c, form); c = c.Parent)
            if (!c.Visible)
                return false;
        return true;
    }

    /// <summary>
    /// 260524Cl 改修: 既定で非表示の Capture=true コントロールを撮るため、自身と非表示の祖先を一時的に
    /// Visible=true・最前面にして CopyFromScreen し、撮影後に必ず元の可視状態へ戻す (後続クロップに影響させない)。
    /// 例: 歳差モードでのみ表示される FormDiffractionSimulator の flowLayoutPanelPED。
    /// </summary>
    private static Bitmap RenderHiddenControl(Form form, Control control, Action<string> trace)
    {
        var toggled = new List<Control>();
        for (var c = control; c != null && c is not Form; c = c.Parent)
            if (!c.Visible) { c.Visible = true; toggled.Add(c); }
        try
        {
            control.BringToFront();
            control.PerformLayout();
            Settle(form, TabSwitchSettleMs, trace);
            return CaptureScreen(new Rectangle(GetScreenLocation(control), control.Size), form, trace, control.Name);
        }
        finally
        {
            for (var i = toggled.Count - 1; i >= 0; i--) // 逆順 (子→親) で元の非表示へ戻す
                toggled[i].Visible = false;
            Application.DoEvents();
        }
    }

    /// <summary>
    /// 260523Cl 追加: コントロールのキャプチャ用パス (= クロップのファイル名 stem) を組み立てる。
    /// form.Name を起点に名前付き祖先の Name を "." で連結する。SplitContainer の SplitterPanel、
    /// ToolStripContainer の ToolStripPanel / ContentPanel、無名コントロールはパスに含めない。
    /// FormCaptureGUI の対話キャプチャと同じ命名規則なので、既存の Wiki 画像 raw URL を壊さない。
    /// </summary>
    private static string BuildCapturePath(Form form, Control control)
    {
        var segments = new List<string>();
        for (var c = control; c != null && !ReferenceEquals(c, form); c = c.Parent)
        {
            if (string.IsNullOrEmpty(c.Name) || c is SplitterPanel || c is ToolStripPanel || c is ToolStripContentPanel)
                continue; // SplitContainer/ToolStripContainer の入れ物パネルと無名コントロールはパスに出さない
            segments.Add(c.Name);
        }
        segments.Add(form.Name);
        segments.Reverse();
        return string.Join(".", segments);
    }

    /// <summary>260523Cl 追加: ファイル名に使えない文字を '_' へ置換する (FormCaptureGUI と同一規則)。</summary>
    private static string SanitizeFileName(string name)
    {
        foreach (var ch in Path.GetInvalidFileNameChars())
            name = name.Replace(ch, '_');
        return name;
    }

    /// <summary>
    /// 260523Cl 追加: クロップ内側 (上下左右 5px 除外) が一様色なら true。
    /// Visible=false のパネル等で灰色一色になったクロップを検出し、無意味なファイルの保存を防ぐ。
    /// </summary>
    private static bool IsSolidColor(Bitmap bmp)
    {
        const int margin = 5;
        int x0 = margin, y0 = margin, x1 = bmp.Width - margin, y1 = bmp.Height - margin;
        if (x1 <= x0 || y1 <= y0)
            return true; // margin で内側が残らない極小クロップは単色扱い

        var data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
        try
        {
            var row = new int[bmp.Width];
            System.Runtime.InteropServices.Marshal.Copy(data.Scan0 + y0 * data.Stride, row, 0, bmp.Width);
            int first = row[x0];
            for (int y = y0; y < y1; y++)
            {
                System.Runtime.InteropServices.Marshal.Copy(data.Scan0 + y * data.Stride, row, 0, bmp.Width);
                for (int x = x0; x < x1; x++)
                    if (row[x] != first)
                        return false;
            }
            return true;
        }
        finally
        {
            bmp.UnlockBits(data);
        }
    }

    /// <summary>
    /// (260523Ch) フォームを Show しただけではマニュアル用の代表状態にならない画面を、撮影直前に整える。
    /// ここは通常 UI 初期化ではなくキャプチャ用の薄い分岐置き場なので、対象フォームは必要最小限に留める。
    /// FormMain は代表結晶を Spinel にし、FormTrajectory は Simulate 相当を実行して GL 軌跡を生成する。
    /// 260524Cl: 重い計算フォーム (FormEBSD の MasterPattern build / FormImageSimulator の Simulate) は、起動だけして
    /// 完了判定は <see cref="WaitUntilScreenStable"/> (5秒ごとに撮って変化が無くなったら完了) に委ねる。
    /// </summary>
    private static void PrepareSpecialCaptureState(Form form, Action<string> trace)
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
                case FormSpotIDV2 spotID:
                    // 260524Cl: SrTiO3 SAD の dm3 を読み込み、スポット検出を起動した代表状態で撮る。検出は非同期かつ重いので画面安定待ち。
                    var sadImage = FindReferenceFile(Path.Combine("DigitalMicroGraph", "SrTiO3", "SADiff100cm_001.dm3"));
                    spotID.PrepareCaptureForGuiAudit(sadImage);
                    trace($"{form.GetType().Name}\tINFO\ttriggered spot-ID find spots ({(sadImage != null ? "image loaded" : "sample image not found")})");
                    WaitUntilScreenStable(form, trace);
                    break;
                case Crystallography.Controls.FormMacro macroForm:
                    // 260524Cl: マクロエディタはサンプルマクロを表示した代表状態で撮る。capture 責務を GuiCapture に集約する方針
                    // (別リポ Crystallography.Controls は無改変) のため、private な checkBoxSamples を reflection でトグルする。
                    TryShowMacroSamples(macroForm, trace);
                    Application.DoEvents();
                    trace($"{form.GetType().Name}\tINFO\tprepared macro editor (samples)");
                    break;
                case Crystallography.Controls.FormGroupRelations groupRelations:
                    // 260705Cl 追加: 既定 (未選択) はプレースホルダのみなので、最初の t-部分群を選んだ代表状態で撮る。
                    groupRelations.PrepareCaptureForGuiAudit();
                    Application.DoEvents();
                    trace($"{form.GetType().Name}\tINFO\tselected first t-subgroup for detail tabs");
                    break;
            }
        }
        catch (Exception ex)
        {
            trace($"{form.GetType().Name}\tWARN\tPrepareCapture: {ex.GetType().Name}: {ex.Message}");
        }
    }

    /// <summary>
    /// 260524Cl 追加: マクロエディタ (Crystallography.Controls.FormMacro) を「サンプルマクロ表示」状態にする。
    /// capture 責務を GuiCapture に集約する方針 (別リポ Crystallography.Controls は無改変) のため、private な
    /// checkBoxSamples を reflection で取得して Checked=true にする。CheckedChanged ハンドラがサンプルマクロを
    /// エディタへ流し込む (fresh load なので未保存確認ダイアログは出ない)。失敗時はユーザー保存マクロのまま撮る (最善努力)。
    /// </summary>
    private static void TryShowMacroSamples(Form macroForm, Action<string> trace)
    {
        try
        {
            var field = macroForm.GetType().GetField("checkBoxSamples",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
            if (field?.GetValue(macroForm) is CheckBox cb && cb.Visible && !cb.Checked)
                cb.Checked = true; // CheckedChanged が走り、サンプルマクロがエディタに表示される
        }
        catch (Exception ex)
        {
            trace($"{macroForm.Name}\tWARN\tmacro samples toggle: {ex.GetType().Name}: {ex.Message}");
        }
    }

    /// <summary>
    /// 260524Cl 追加: --capture 用のサンプルデータ (リポジトリの references フォルダ内) を探す。
    /// exe (bin/Debug) から見た相対パスと開発機の絶対パスを順に試し、最初に存在したフルパスを返す。無ければ null。
    /// </summary>
    private static string FindReferenceFile(string relativePath)
    {
        var candidates = new[]
        {
            Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "references", relativePath),
            Path.Combine(@"C:\Users\seto\source\repos\ReciPro\references", relativePath),
        };
        foreach (var candidate in candidates)
        {
            try { if (File.Exists(candidate)) return Path.GetFullPath(candidate); }
            catch { /* 不正パスは無視 */ }
        }
        return null;
    }

    private const int StabilizePollMs = 5000;  // 260524Cl: 重い計算フォームを撮る前に、この間隔で画面を撮り比べる
    private const int StabilizeMaxPolls = 72;  // 上限 (5秒 × 72 = 6分)。計算が終わらなくてもこの時点で撮る

    /// <summary>
    /// 260524Cl 追加: 重い計算フォーム用の単純な完了判定。<see cref="StabilizePollMs"/> ごとにウィンドウ全体を撮り、
    /// 直前の撮影と画素が完全一致したら「計算が終わって画面が止まった」とみなして戻る (凝った完了判定はしない)。
    /// 進捗バーや経過時間ラベルが動いている間は一致しないので、それらが止まる = 計算完了で抜ける。
    /// キャレット点滅で一致しないのを避けるため、待機前にアクティブコントロールを外す。上限に達したらそのまま戻る。
    /// </summary>
    private static void WaitUntilScreenStable(Form form, Action<string> trace)
    {
        try { form.ActiveControl = null; } catch { /* キャレット点滅で画面が一致しなくなるのを避ける */ }

        Bitmap previous = null;
        try
        {
            for (var poll = 1; poll <= StabilizeMaxPolls; poll++)
            {
                var until = Environment.TickCount + StabilizePollMs; // StabilizePollMs ぶん描画を進める
                do { Application.DoEvents(); System.Threading.Thread.Sleep(30); } while (Environment.TickCount < until);
                RenderOpenGlControls(form, trace); // GL 結果 (EBSD MasterPattern3D 等) を可視バッファへ反映

                var current = CaptureScreen(GetWindowVisualBounds(form));
                if (current == null)
                    continue; // 一時的に撮れなければ次のポーリングへ

                if (previous != null && BitmapsEqual(previous, current))
                {
                    current.Dispose();
                    trace($"{form.Name}\tINFO\tscreen stable after ~{poll * StabilizePollMs / 1000}s");
                    return;
                }
                previous?.Dispose();
                previous = current;
            }
            trace($"{form.Name}\tINFO\tscreen not stable within {StabilizeMaxPolls * StabilizePollMs / 1000}s; capturing as-is");
        }
        finally
        {
            previous?.Dispose();
        }
    }

    /// <summary>260524Cl 追加: 同サイズの 2 枚のビットマップが画素単位で完全一致するか。WaitUntilScreenStable の変化判定用。</summary>
    private static bool BitmapsEqual(Bitmap a, Bitmap b)
    {
        if (a.Width != b.Width || a.Height != b.Height)
            return false;

        var rect = new Rectangle(0, 0, a.Width, a.Height);
        var da = a.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
        var db = b.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
        try
        {
            var bytes = Math.Abs(da.Stride) * a.Height;
            var bufferA = new byte[bytes];
            var bufferB = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(da.Scan0, bufferA, 0, bytes);
            System.Runtime.InteropServices.Marshal.Copy(db.Scan0, bufferB, 0, bytes);
            return bufferA.AsSpan().SequenceEqual(bufferB);
        }
        finally
        {
            a.UnlockBits(da);
            b.UnlockBits(db);
        }
    }

    /// <summary>
    /// (260523Ch) フォーム配下の全コントロールを深さ優先で列挙する。
    /// GLControlAlpha は Panel / SplitContainer / TabPage などの奥に入っているため、Controls 直下だけでは拾えない。
    /// </summary>
    private static IEnumerable<Control> EnumerateControls(Control root)
    {
        yield return root;

        foreach (Control child in root.Controls)
        {
            foreach (var descendant in EnumerateControls(child))
                yield return descendant;
        }
    }
}
