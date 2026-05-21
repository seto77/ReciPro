using System.Collections.Generic;
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
        foreach (var type in types)
        {
            Form form = null;
            try
            {
                form = (Form)Activator.CreateInstance(type);
                CaptureForm(form, type.Name, outDir, Trace);
                ok++;
            }
            catch (Exception ex)
            {
                fail++;
                Trace($"{type.Name}\tFAIL\t{ex.GetType().Name}: {ex.Message}");
            }
            finally
            {
                try { form?.Dispose(); } catch { /* 破棄時例外は無視 */ }
            }
        }

        Trace($"done: ok={ok} fail={fail}");
        File.WriteAllLines(Path.Combine(outDir, "_capture-log.tsv"), log);
    }

    private static void CaptureForm(Form form, string name, string outDir, Action<string> trace)
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

        int w = Math.Max(form.Width, 1), h = Math.Max(form.Height, 1);
        using var bmp = new Bitmap(w, h);
        form.DrawToBitmap(bmp, new Rectangle(0, 0, w, h));
        bmp.Save(Path.Combine(outDir, name + ".png"), ImageFormat.Png);
        trace($"{name}\tOK\t{w}x{h}");

        form.Close();
    }
}
