global using Crystallography;
global using Crystallography.Controls;
global using System;
using Microsoft.Win32; // 260428Cl 追加: ダークモード設定の読込/書込のため
using System.Windows.Forms;

namespace ReciPro;

internal static class Program
{
    // 260428Cl 追加: ダークモード設定は Application.SetColorMode を Application.Run より前に呼ぶ必要があるため、
    // FormMain.Registry() の Reg.RW (MemoryPack+Brotli) 経路ではなく、独立した DWORD 値として保持する。
    private const string DarkModeRegPath = @"HKEY_CURRENT_USER\Software\Crystallography\ReciPro";
    private const string DarkModeRegName = "DarkMode";

    // 260523Cl 追加: GUI 監査用スクショ一括取得モードの起動引数 (Main 内で 2 箇所判定するため定数化)
    private const string CaptureArg = "--capture";

    /// <summary>アプリケーションのメイン エントリ ポイントです。</summary>
    // private static void Main() // 260521Cl 旧シグネチャ (--capture 引数対応のため string[] args を追加)
    [STAThread]
    private static void Main(string[] args)
    {
        // 260522Cl 追加 / 260525Cl 改修: --capture の言語指定を SetDefaultFont より前に反映する。
        //   ReciPro.exe --capture [出力ディレクトリ] [カルチャ(en/ja)]   (出力先を明示)
        //   ReciPro.exe --capture [カルチャ(en/ja)]                      (出力先省略=既定の docs/src/assets/cap-*-auto)
        // GetUIFont() / 各フォームの resx ローカライズが CurrentUICulture を参照するため、ここで先に確定させる。
        string captureDir = null, captureCulture = null;  // 260525Cl 追加
        if (args.Length >= 2 && args[0] == CaptureArg)
        {
            // args[1] が en/ja なら「カルチャのみ指定 (出力先は既定)」、それ以外なら出力先ディレクトリとみなす。
            if (args[1] is "en" or "ja") captureCulture = args[1];
            else { captureDir = args[1]; captureCulture = args.Length >= 3 ? args[2] : null; }
        }
        // if (args.Length >= 3 && args[0] == CaptureArg) { var ci = new System.Globalization.CultureInfo(args[2]); ... } // 260525Cl 旧: dir 必須だった
        if (captureCulture != null)
        {
            var ci = new System.Globalization.CultureInfo(captureCulture);
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = ci;
            GuiCapture.ForcedUICulture = ci;
        }

        Application.EnableVisualStyles();
        //Application.SetHighDpiMode(HighDpiMode.DpiUnawareGdiScaled); // 260329Cl 変更: csproj の PerMonitorV2 と統一
        Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
        Application.SetCompatibleTextRenderingDefault(true);
        // 260428Cl 変更: OS追従ではなくレジストリ保存値による手動切替
        //Application.SetColorMode(SystemColorMode.System);
        Application.SetColorMode(ReadDarkMode() ? SystemColorMode.Dark : SystemColorMode.Classic);
        // 260428Cl 追加: 言語別 UI フォント (Designer 未指定コントロール用のデフォルト)。
        // Designer/resx で明示指定されたコントロールには適用されない (それらは文字列置換で対応済み)。
        Application.SetDefaultFont(Crystallography.Controls.FontHelper.GetUIFont());

        // 260521Cl 追加: GUI 監査用スクショ一括取得モード。通常起動 (引数なし) には一切影響しない。
        //   ReciPro.exe --capture [出力ディレクトリ] [カルチャ(en/ja)]
        if (args.Length >= 1 && args[0] == CaptureArg)
        {
            GuiCapture.Run(captureDir);  // 260525Cl: captureDir が null なら docs/src/assets/cap-{en|ja}-auto が既定
            // GuiCapture.Run(args.Length >= 2 ? args[1] : null); // 260525Cl 旧
            // return; // 旧実装: Main の return だけでは OpenTK/WinForms 周辺スレッドが残り DLL を掴むことがあった
            Environment.Exit(0); // (260523Ch) --capture 完了後は開発者ツールとしてプロセスを確実に終了させる (この後に到達しないため旧 return; は削除)
        }

        Application.Run(new FormMain());
    }

    // 260428Cl 追加
    public static bool ReadDarkMode()
        => Registry.GetValue(DarkModeRegPath, DarkModeRegName, 0) is 1;

    // 260428Cl 追加
    public static void WriteDarkMode(bool dark)
        => Registry.SetValue(DarkModeRegPath, DarkModeRegName, dark ? 1 : 0, RegistryValueKind.DWord);
}
