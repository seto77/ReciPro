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

    /// <summary>アプリケーションのメイン エントリ ポイントです。</summary>
    [STAThread]
    private static void Main()
    {
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
        Application.Run(new FormMain());
    }

    // 260428Cl 追加
    public static bool ReadDarkMode()
        => Registry.GetValue(DarkModeRegPath, DarkModeRegName, 0) is 1;

    // 260428Cl 追加
    public static void WriteDarkMode(bool dark)
        => Registry.SetValue(DarkModeRegPath, DarkModeRegName, dark ? 1 : 0, RegistryValueKind.DWord);
}
