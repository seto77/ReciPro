// 260807Cl 新規作成: 単一フォームを**画面に出さずに**撮る開発者向けモード (`--capture-form`)。
//
// GuiCapture.Run (--capture) は CopyFromScreen なので対話デスクトップが必須で、RDP 切断中や
// 非対話セッションでは撮影が全滅する (新規フォームの目視確認ができずに実際に詰まった)。
// DrawToBitmap は画面なしで動くので「フォームが構築でき、レイアウトが崩れていない」ことの
// 自動確認に使える。⚠GL / GraphicsBox など WM_PRINT に応じない描画は白く抜けるため、
// その種のフォームには使わないこと (そちらは従来どおり --capture)。
//
// GuiCapture.cs 本体は CP932 で保存されているため、partial を使って別ファイルに置いている
// (エンコーディング違いのファイルを機械編集すると壊す危険がある)。
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ReciPro;

internal static partial class GuiCapture
{
    /// <summary>ReciPro.exe --capture-form &lt;FormTypeName&gt; &lt;out.png&gt; [culture]</summary>
    public static void CaptureSingleForm(string typeName, string outPath)
    {
        var type = typeof(GuiCapture).Assembly.GetTypes()
            .FirstOrDefault(t => typeof(Form).IsAssignableFrom(t) && !t.IsAbstract && t.Name == typeName
                && t.GetConstructor(Type.EmptyTypes) != null);
        if (type == null)
        {
            Console.Error.WriteLine($"--capture-form: no Form type named '{typeName}' with a parameterless constructor");
            Environment.ExitCode = 2;
            return;
        }
        //260809Cl 追加: --capture と同じ「代表状態づくり」を通す。従来はフォームを Show しただけだったので
        //FormALCHEMI のように「計算しないと中身が空」のフォームでは使えなかった。FormMain を先に作って
        //結晶 (spinel) を選び、子フォームへ親情報を注入してから PrepareSpecialCaptureState を呼ぶ。
        //⚠FormMain 自体は Close しない (FormClosing がレジストリへ UI 言語を焼き付けるため。GuiCapture.cs の 260726Cl 注記)。
        FormMain main = null;
        if (type != typeof(FormMain))
        {
            main = new FormMain { StartPosition = FormStartPosition.Manual, Location = new Point(-32000, -32000), ShowInTaskbar = false };
            main.Show();
            Application.DoEvents();
            main.PrepareCaptureCrystalSelection();
            Application.DoEvents();
        }

        using var form = (Form)Activator.CreateInstance(type);
        if (main != null) WireCrystalDependencies(form, main);
        form.StartPosition = FormStartPosition.Manual;
        form.Location = new Point(-32000, -32000);//ハンドルは作るが画面には出さない
        form.ShowInTaskbar = false;
        form.Show();
        Application.DoEvents();
        PrepareSpecialCaptureState(form, s => Console.WriteLine("--capture-form: " + s));
        Application.DoEvents();
        using var bmp = new Bitmap(Math.Max(1, form.Width), Math.Max(1, form.Height));
        form.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
        var dir = Path.GetDirectoryName(Path.GetFullPath(outPath));
        if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);
        bmp.Save(outPath, System.Drawing.Imaging.ImageFormat.Png);
        form.Hide();
        Console.WriteLine($"--capture-form: {typeName} -> {Path.GetFullPath(outPath)} ({bmp.Width}x{bmp.Height})");
    }
}
