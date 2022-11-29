global using Crystallography;
global using Crystallography.Controls;
global using System;
using System.Windows.Forms;

namespace ReciPro;

internal static class Program
{
    /// <summary>
    /// アプリケーションのメイン エントリ ポイントです。
    /// </summary>
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetHighDpiMode(HighDpiMode.DpiUnawareGdiScaled);
        Application.SetCompatibleTextRenderingDefault(true);
        Application.Run(new FormMain());
    }
}
