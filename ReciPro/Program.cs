global using Crystallography;
global using Crystallography.Controls;
global using System;
using System.Windows.Forms;

namespace ReciPro;

internal static class Program
{
    /// <summary>アプリケーションのメイン エントリ ポイントです。</summary>
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        //Application.SetHighDpiMode(HighDpiMode.DpiUnawareGdiScaled); // 260329Cl 変更: csproj の PerMonitorV2 と統一
        Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
        Application.SetCompatibleTextRenderingDefault(true);
        Application.Run(new FormMain());
    }
}
