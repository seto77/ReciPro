using System.Collections.Generic;
using Crystallography.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ReciPro;

/// <summary>
/// 260521Cl 追加: GUI 統一性監査用に ReciPro の全フォームを構築して PNG 一括保存する開発者向けツール。
/// 起動: <c>ReciPro.exe --capture [出力ディレクトリ]</c>
/// 対話的な FormCaptureGUI とは別経路で、各フォームを画面外で Show → DrawToBitmap して保存する
/// (以前一時ハーネスで行っていた DrawToBitmap 方式の再現)。通常起動 (引数なし) では一切実行されない。
/// </summary>
internal static class GuiCapture
{
    /// <summary>
    /// 260522Cl 追加: --capture で言語を強制指定 (en/ja) した場合のカルチャ。
    /// FormMain ctor がレジストリ値で CurrentUICulture を上書きするため、各フォーム構築前に再設定する。
    /// </summary>
    public static System.Globalization.CultureInfo ForcedUICulture;

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
                    captureFormMain = mainForm; // (260523Ch) reflection 順の先頭で作った FormMain を後続 FormTrajectory の親情報として再利用する
                else if (form is FormTrajectory trajectory)
                    trajectory.FormMain = captureFormMain; // (260523Ch) FormTrajectory は単独生成だと Simulate 時に FormMain.Crystal を参照できない

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
        // FormScatteringFactor) を、spinel 選択済みの FormMain が持つ配線済みインスタンス経由で撮る。
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
    /// 1 つの Form を画面外に表示して撮影する。
    /// まず WinForms 全体を DrawToBitmap で取得し、その後 DrawToBitmap では空白になりやすい GLControlAlpha 領域を
    /// GL.ReadPixels 由来の画像で上書きする。closeAfterCapture=false は、後続フォームの準備に FormMain を使うための例外。
    /// </summary>
    private static void CaptureForm(Form form, string name, string outDir, Action<string> trace, bool closeAfterCapture = true)
    {
        form.StartPosition = FormStartPosition.Manual;
        form.ShowInTaskbar = false;
        form.Location = new Point(-32000, -32000); // 画面外に表示してちらつきを避ける
        // Show() で Visible=true にしないと子コントロールが描画されない (CreateControl だけだと空白になる)。
        // Load 等の例外は ThreadException ハンドラへ流れるためモーダル化せず、ハングしない。
        // ただし Show() の呼び出しスタック上で同期的に投げられる例外もあるため、try で囲んで
        // 例外が出てもハンドル/レイアウト生成済みなら DrawToBitmap を試みる (部分的にでも撮る)。
        try { form.Show(); }
        catch (Exception ex) { trace($"{name}\tWARN\tShow: {ex.GetType().Name}: {ex.Message}"); }
        Application.DoEvents();
        PrepareSpecialCaptureState(form, trace); // (260523Ch) FormTrajectory など生成直後では代表画像が空になるフォームだけ個別準備

        // 260524Cl: 自動キャプチャは GL/PictureBox を含むフォームの全体像も含め最善努力で撮る (overlay で GL を補完)。
        // 質の悪い画像はユーザーが手動キャプチャ (cap-*-manual) で上書きする運用 (md は manual 優先・無ければ auto)。
        int w = Math.Max(form.Width, 1), h = Math.Max(form.Height, 1);
        using var bmp = new Bitmap(w, h);
        form.DrawToBitmap(bmp, new Rectangle(0, 0, w, h));
        var openGlOverlayCount = OverlayOpenGlControls(form, bmp, trace); // (260523Ch) DrawToBitmap で空になる OpenGL 領域を GL.ReadPixels 画像で補完する
        bmp.Save(Path.Combine(outDir, name + ".png"), ImageFormat.Png);
        var cropCount = CaptureControlCrops(form, name, outDir, trace); // 260523Cl 追加: Capture=true のコントロール単位クロップを非対話で生成
        trace($"{name}\tOK\t{w}x{h}\tOpenGL={openGlOverlayCount}\tCrops={cropCount}"); // (260523Cl)

        // form.Close(); // 旧実装: キャプチャ直後に全フォームを閉じていた
        if (closeAfterCapture)
            form.Close(); // (260523Ch) FormMain は FormTrajectory 特例の親として最後まで保持する
    }

    /// <summary>
    /// 260523Cl 追加: Designer で <c>Capture=true</c> を付けたコントロール単位のクロップを、対話 UI を出さずに生成する。
    /// 対話ツール FormCaptureGUI は CopyFromScreen 方式で対象を前面表示する必要があり画面外キャプチャと両立しないため、
    /// キャプチャの責務はこの GuiCapture 側に置き、「フォーム全体を DrawToBitmap + OpenGL overlay した合成画像」から
    /// 各対象の矩形を切り出す。Crystallography.Controls 側へは <see cref="Crystallography.Controls.CaptureExtender.IsCaptureEnabled"/>
    /// (Capture=true 判定) だけを依存し、対象列挙・パス命名・空白判定はすべてここで行う。
    /// 命名は FormCaptureGUI の手動キャプチャと同じ規則 (form.Name 起点、SplitterPanel/ToolStripPanel/無名は除外) で、
    /// 既存の Wiki 画像 raw URL を壊さない。同じタブ選択状態の対象は合成画像を使い回し、タブ切替時だけ撮り直す。
    /// </summary>
    /// <returns>保存できたクロップ数。</returns>
    private static int CaptureControlCrops(Form form, string name, string outDir, Action<string> trace)
    {
        var count = 0;
        Bitmap formBmp = null;
        string lastTabSignature = null;
        try
        {
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
                    EnsureAncestorTabsSelected(control);
                    Application.DoEvents();

                    Bitmap crop;
                    if (!IsEffectivelyVisible(form, control))
                    {
                        // 260524Cl 追加: 既定で Visible=false の Capture=true コントロール (例: 歳差モードでのみ
                        // 表示される flowLayoutPanelPED) は合成画像に現れないため、一時的に可視化して単体で撮る。
                        crop = RenderHiddenControl(control);
                        if (crop == null)
                            continue;
                    }
                    else
                    {
                        // タブ選択が変わったときだけフォーム全体を撮り直し、同一タブ上の複数クロップで合成画像を共有する。
                        var signature = GetTabSelectionSignature(form);
                        if (formBmp == null || signature != lastTabSignature)
                        {
                            formBmp?.Dispose();
                            formBmp = new Bitmap(Math.Max(form.Width, 1), Math.Max(form.Height, 1));
                            form.DrawToBitmap(formBmp, new Rectangle(0, 0, formBmp.Width, formBmp.Height));
                            OverlayOpenGlControls(form, formBmp, trace);
                            lastTabSignature = signature;
                        }

                        // TabPage は親 TabControl 全体 (タブ見出し込み) を撮る (FormCaptureGUI と同じ見た目)。
                        var region = control is TabPage tabPage && tabPage.Parent is TabControl tabControl ? (Control)tabControl : control;
                        var rect = GetControlBoundsInFormBitmap(form, region, region.Size);
                        var clipped = Rectangle.Intersect(new Rectangle(Point.Empty, formBmp.Size), rect);
                        if (clipped.Width <= 0 || clipped.Height <= 0)
                            continue;

                        crop = new Bitmap(clipped.Width, clipped.Height);
                        using var g = Graphics.FromImage(crop);
                        g.DrawImage(formBmp, new Rectangle(Point.Empty, clipped.Size), clipped, GraphicsUnit.Pixel);
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
        }
        finally
        {
            formBmp?.Dispose();
        }
        return count;
    }

    /// <summary>260523Cl 追加: コントロールの祖先 TabPage を順に選択し、クロップ時に可視化する。</summary>
    private static void EnsureAncestorTabsSelected(Control control)
    {
        for (var c = control; c != null; c = c.Parent)
        {
            if (c is TabPage tabPage && tabPage.Parent is TabControl tabControl && tabControl.SelectedTab != tabPage)
            {
                tabControl.SelectedTab = tabPage;
                tabControl.Refresh();
            }
        }
    }

    /// <summary>260523Cl 追加: フォーム内全 TabControl の選択タブ状態の署名。変化したときだけ合成画像を撮り直す。</summary>
    private static string GetTabSelectionSignature(Form form)
        => string.Join(",", EnumerateControls(form).OfType<TabControl>().Select(t => $"{t.Name}:{t.SelectedIndex}"));

    /// <summary>260524Cl 追加: control から form まで全て Visible なら true。途中に Visible=false があれば false。</summary>
    private static bool IsEffectivelyVisible(Form form, Control control)
    {
        for (var c = control; c != null && !ReferenceEquals(c, form); c = c.Parent)
            if (!c.Visible)
                return false;
        return true;
    }

    /// <summary>
    /// 260524Cl 追加: 既定で非表示の Capture=true コントロールを撮るため、自身と非表示の祖先を一時的に
    /// Visible=true にして単体 DrawToBitmap し、撮影後に必ず元の可視状態へ戻す (後続クロップ・共有合成画像に影響させない)。
    /// 例: 歳差モードでのみ表示される FormDiffractionSimulator の flowLayoutPanelPED。GL を含む領域は対象外。
    /// </summary>
    private static Bitmap RenderHiddenControl(Control control)
    {
        var toggled = new List<Control>();
        for (var c = control; c != null && c is not Form; c = c.Parent)
            if (!c.Visible) { c.Visible = true; toggled.Add(c); }
        try
        {
            control.BringToFront();
            control.PerformLayout();
            Application.DoEvents();
            int w = Math.Max(control.Width, 1), h = Math.Max(control.Height, 1);
            var bmp = new Bitmap(w, h);
            control.DrawToBitmap(bmp, new Rectangle(0, 0, w, h));
            return bmp;
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
    /// </summary>
    private static void PrepareSpecialCaptureState(Form form, Action<string> trace)
    {
        if (form is FormMain mainForm)
        {
            var selected = mainForm.PrepareCaptureCrystalSelection();
            trace($"{form.GetType().Name}\tINFO\tcapture crystal={(selected ? mainForm.Crystal?.Name : "not found")}"); // (260523Ch)
            return;
        }

        if (form is not FormTrajectory trajectory)
            return;

        try
        {
            trajectory.PrepareCaptureForGuiAudit(); // (260523Ch) FormTrajectory は Simulate 後でないと GL 軌跡が存在しない
            Application.DoEvents();
            trace($"{form.GetType().Name}\tINFO\tprepared trajectory simulation");
        }
        catch (Exception ex)
        {
            trace($"{form.GetType().Name}\tWARN\tPrepareCapture: {ex.GetType().Name}: {ex.Message}");
        }
    }

    /// <summary>
    /// (260523Ch) DrawToBitmap 後のフォーム画像に、GLControlAlpha の実描画結果を合成する。
    /// WinForms の DrawToBitmap は OpenGL back/front buffer の内容を拾えず白紙になりやすいため、
    /// GLControlAlpha.GenerateBitmap(renderBeforeRead: true) で各 GL 領域だけ再描画・ReadPixels し、フォーム画像へ貼り戻す。
    /// </summary>
    /// <returns>合成できた GLControlAlpha の数。</returns>
    private static int OverlayOpenGlControls(Form form, Bitmap formBitmap, Action<string> trace)
    {
        var count = 0;
        using var graphics = Graphics.FromImage(formBitmap);
        foreach (var glControl in EnumerateControls(form).OfType<GLControlAlpha>())
        {
            if (glControl.IsDisposed || !glControl.Visible || glControl.Width <= 0 || glControl.Height <= 0)
                continue;

            try
            {
                glControl.Refresh(); // (260523Ch) off-screen Show 後の未描画バッファをできるだけ避ける
                Application.DoEvents();

                using var glBitmap = glControl.GenerateBitmap(renderBeforeRead: true); // (260523Ch) キャプチャ時は swap なし再描画後の back buffer を読む
                if (glBitmap == null || glBitmap.Width <= 0 || glBitmap.Height <= 0)
                    continue;

                var target = GetControlBoundsInFormBitmap(form, glControl, glBitmap.Size);
                var clippedTarget = Rectangle.Intersect(new Rectangle(Point.Empty, formBitmap.Size), target);
                if (clippedTarget.Width <= 0 || clippedTarget.Height <= 0)
                    continue;

                var source = new Rectangle(
                    clippedTarget.X - target.X,
                    clippedTarget.Y - target.Y,
                    clippedTarget.Width,
                    clippedTarget.Height);
                graphics.DrawImage(glBitmap, clippedTarget, source, GraphicsUnit.Pixel);
                count++;
            }
            catch (Exception ex)
            {
                trace($"{form.GetType().Name}\tWARN\tOpenGL overlay {glControl.Name}: {ex.GetType().Name}: {ex.Message}");
            }
        }
        return count;
    }

    /// <summary>
    /// (260523Ch) GL 画像をフォーム全体 PNG のどこへ貼るかを求める。
    /// DrawToBitmap のフォーム画像はタイトルバー等の非クライアント領域も含むため、
    /// 親子 Control の Location だけではずれる。PointToScreen と form.Bounds の差分で合成位置を決める。
    /// </summary>
    private static Rectangle GetControlBoundsInFormBitmap(Form form, Control control, Size bitmapSize)
    {
        var controlScreenLocation = control.PointToScreen(Point.Empty);
        return new Rectangle(
            controlScreenLocation.X - form.Bounds.X,
            controlScreenLocation.Y - form.Bounds.Y,
            bitmapSize.Width,
            bitmapSize.Height); // (260523Ch) Form 全体画像は非クライアント領域も含むため screen 座標差分で合成位置を求める
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
