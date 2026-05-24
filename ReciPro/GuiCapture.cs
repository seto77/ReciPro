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
internal static class GuiCapture
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

    /// <summary>
    /// --capture の本体。ReciPro 内の parameterless ctor を持つ Form を順に構築し、フォーム単位の PNG を保存する。
    /// FormMain は他フォームの代表状態を作るため最後まで保持する。通常起動からは呼ばない開発者向け経路。
    /// </summary>
    public static void Run(string outDir)
    {
        outDir ??= Path.Combine(Path.GetTempPath(), "recipro-capture-" + DateTime.Now.ToString("yyyyMMdd-HHmmss"));
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

        // ReciPro アセンブリ内の、パラメータレスコンストラクタを持つ Form 派生型を対象にする。
        // FormMain を先頭に構築する (他フォームが静的に FormMain を参照する場合に備える)。
        var types = typeof(FormMain).Assembly.GetTypes()
            .Where(t => typeof(Form).IsAssignableFrom(t) && !t.IsAbstract && t.GetConstructor(Type.EmptyTypes) != null)
            .OrderBy(t => t == typeof(FormMain) ? 0 : 1).ThenBy(t => t.Name)
            .ToList();
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
                else if (form is FormTrajectory trajectory)
                    trajectory.FormMain = captureFormMain; // (260523Ch) FormTrajectory は単独生成だと Simulate 時に FormMain.Crystal を参照できない
                else if (form is FormEBSD ebsd)
                    ebsd.FormMain = captureFormMain; // 260524Cl: Build MasterPattern が FormMain.Crystal を参照するため注入 (Show 時の NRE も解消)
                else if (form is FormImageSimulator imageSimulator)
                    imageSimulator.FormMain = captureFormMain; // 260524Cl: Simulate が FormMain.Crystal を参照するため注入 (Show 時の NRE も解消)
                else if (form is FormStereonet stereonet)
                    stereonet.formMain = captureFormMain; // 260524Cl: 軸/極のプロットは formMain.Crystal が必要 (未注入だと網だけで軸が描かれない。Show 時の NRE も解消)
                else if (form is FormRotationMatrix rotation)
                    rotation.FormMain = captureFormMain; // 260524Cl: GL のトーラス/軸/球の描画 (SetRotation) は FormMain の Euler 角を参照するため注入
                else if (form is FormDiffractionSimulator diffractionSimulator)
                    diffractionSimulator.formMain = captureFormMain; // 260524Cl: 回折スポットの描画 (Draw) は formMain.Crystal が必要 (未注入だと描画ボックスが灰色のまま)

                CaptureForm(form, type.Name, outDir, Trace, closeAfterCapture: !ReferenceEquals(form, captureFormMain));
                ok++;
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
        // FormScatteringFactor / FormStructureViewer) を、spinel 選択済みの FormMain が持つ配線済みインスタンス経由で撮る。
        if (captureFormMain != null)
        {
            foreach (var child in captureFormMain.EnumerateCaptureCrystalDependentForms())
            {
                try
                {
                    CaptureForm(child, child.GetType().Name, outDir, Trace, closeAfterCapture: true);
                    ok++;
                }
                catch (Exception ex)
                {
                    fail++;
                    Trace($"{child.GetType().Name}\tFAIL\t{ex.GetType().Name}: {ex.Message}");
                }
            }
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
        trace($"{name}\t{(captured ? "OK" : "PARTIAL")}\t{bounds.Width}x{bounds.Height}\tCrops={cropCount}");

        if (closeAfterCapture)
        {
            form.TopMost = false; // (260524Cl) 後続フォームの最前面化を妨げないよう閉じる前に解除
            form.Close();
        }
    }

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

    /// <summary>
    /// 260524Cl 追加: フォーム内の GLControlAlpha を通常描画 (SwapBuffers あり) して、可視バッファへ最新シーンを出す。
    /// CopyFromScreen は画面の front buffer を読むため、撮影前に GL シーンを画面へ反映しておく必要がある。
    /// </summary>
    private static void RenderOpenGlControls(Form form, Action<string> trace)
    {
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
                    RenderOpenGlControls(form, trace); // 領域内に GL があれば最新シーンを画面へ反映
                    Application.DoEvents();
                    crop = CaptureScreen(new Rectangle(GetScreenLocation(region), region.Size), form, trace, $"{name}.{control.Name}");
                    if (crop == null)
                        continue; // RDP 画面が一時的に取得不能なら、このクロップは諦めて次へ
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
            }
        }
        catch (Exception ex)
        {
            trace($"{form.GetType().Name}\tWARN\tPrepareCapture: {ex.GetType().Name}: {ex.Message}");
        }
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
