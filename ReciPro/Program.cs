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

    // 260612Cl 追加: CI 用の起動診断モードの引数 (attach-arm64-experimental.yml の windows-11-arm smoke が使用)
    private const string SmokeArg = "--smoke";

    // 260617Cl 追加: 多言語化のオーバーフロー診断モードの引数 (GuiCapture.Diagnose)。
    private const string DiagnoseArg = "--diagnose";

    /// <summary>アプリケーションのメイン エントリ ポイントです。</summary>
    // private static void Main() // 260521Cl 旧シグネチャ (--capture 引数対応のため string[] args を追加)
    [STAThread]
    private static void Main(string[] args)
    {
        // 260612Cl 追加: CI 用の起動 smoke。NativeWrapper/Xraylib はロード失敗時に黙って Enabled=false へ
        // フォールバックする型のため、配布前検査として明示的に検査し、結果をファイルへ書き出す
        // (OutputType=WinExe のためコンソール出力は使えない)。GUI は一切起動しない。
        //   ReciPro.exe --smoke [出力ファイル]
        // 終了コード: 0=native/xraylib とも有効、2=いずれか無効 (詳細は出力ファイル参照)
        if (args.Length >= 1 && args[0] == SmokeArg)
            RunSmoke(args.Length >= 2 ? args[1] : "smoke-result.txt");

        // 260522Cl 追加 / 260525Cl 改修: --capture の言語指定を SetDefaultFont より前に反映する。
        //   ReciPro.exe --capture [出力ディレクトリ] [カルチャ(en/ja)]   (出力先を明示)
        //   ReciPro.exe --capture [カルチャ(en/ja)]                      (出力先省略=既定の docs/src/assets/cap-*-auto)
        // GetUIFont() / 各フォームの resx ローカライズが CurrentUICulture を参照するため、ここで先に確定させる。
        string captureDir = null, captureCulture = null;  // 260525Cl 追加
        if (args.Length >= 2 && args[0] == CaptureArg)
        {
            // args[1] が対応カルチャ名なら「カルチャのみ指定 (出力先は既定)」、それ以外なら出力先ディレクトリとみなす。
            // 260617Cl 変更: en/ja 固定判定から SupportedCultures 駆動へ (Phase 0。--capture <dir> de 等が通る)。
            // 旧: if (args[1] is "en" or "ja") captureCulture = args[1];
            if (Array.Exists(Crystallography.SupportedCultures.All, c => string.Equals(c.Name, args[1], StringComparison.OrdinalIgnoreCase)))
                captureCulture = args[1];
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

        // 260617Cl 追加: 多言語化のオーバーフロー診断モード。--capture 同様 SetDefaultFont 前に言語を確定させる。
        //   ReciPro.exe --diagnose [カルチャ] [水増し%]
        //   水増し%(疑似ローカライズ): 例 140 で「文字が 40% 長くなったら切れるか」を実翻訳が無くても先出しする。
        bool doDiagnose = false; double diagnoseInflate = 1.0;
        if (args.Length >= 1 && args[0] == DiagnoseArg)
        {
            doDiagnose = true;
            string diagnoseCulture = null;
            if (args.Length >= 2 && Array.Exists(Crystallography.SupportedCultures.All, c => string.Equals(c.Name, args[1], StringComparison.OrdinalIgnoreCase)))
                diagnoseCulture = args[1];
            // 水増し% は、カルチャを伴うなら args[2]、伴わない (= args[1] が数値) なら args[1]。
            var pctArg = diagnoseCulture != null ? (args.Length >= 3 ? args[2] : null) : (args.Length >= 2 ? args[1] : null);
            if (int.TryParse(pctArg, out var pct) && pct > 0) diagnoseInflate = pct / 100.0;
            if (diagnoseCulture != null)
            {
                var ci = new System.Globalization.CultureInfo(diagnoseCulture);
                System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
                System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = ci;
                GuiCapture.ForcedUICulture = ci;
            }
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

        // 260617Cl 追加: 多言語化対応の全体レバー。全 NumericBox の数値欄に最低固定幅(論理px)を保証し、
        // ヘッダ翻訳で数値欄が縮む問題を解消する (親が Flow/Table なら全幅を伸ばしリフロー、絶対配置なら数値欄死守+ヘッダは tooltip 救済)。
        // ValueBoxWidth を明示設定した個別 NumericBox はそちらが優先。詳細は .project-guidance/ReciPro_多言語化方針.md Phase 1。
        Crystallography.Controls.NumericBox.DefaultValueBoxWidth = 54;

        // 260617Cl 追加: 多言語化のオーバーフロー診断モード本体。通常起動には一切影響しない。
        if (doDiagnose)
        {
            var cult = (GuiCapture.ForcedUICulture ?? System.Threading.Thread.CurrentThread.CurrentUICulture).Name;
            var outFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"recipro-diagnose-{cult}-x{(int)(diagnoseInflate * 100)}.tsv");
            GuiCapture.Diagnose(outFile, diagnoseInflate);
            Environment.Exit(0);
        }

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

    // 260612Cl 追加: --smoke の本体。Enabled プロパティの参照が static ctor (DLL ロード + self-test) を
    // 起動するので、これだけで「実機で native/xraylib が本当にロードできるか」の検査になる。
    private static void RunSmoke(string outPath)
    {
        // glfw3.dll は single-file exe のバンドル内にあり、3D フォームを開くまでロードされない。
        // GL コンテキストは作らず glfwInit/Terminate だけで「バンドルから抽出した実物がロード・初期化できる」
        // ことを検査する (codex レビュー指摘: リポ側 DLL の LoadLibrary では配布物の検査にならない)
        var glfwEnabled = false;
        string glfwError;
        try
        {
            glfwEnabled = OpenTK.Windowing.GraphicsLibraryFramework.GLFW.Init();
            if (glfwEnabled)
                OpenTK.Windowing.GraphicsLibraryFramework.GLFW.Terminate();
            glfwError = glfwEnabled ? "" : "glfwInit returned false";
        }
        catch (Exception ex) { glfwError = ex.Message; }

        System.IO.File.WriteAllLines(outPath,
        [
            $"arch={System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture}",
            $"baseDir={AppContext.BaseDirectory}",
            $"nativeEnabled={NativeWrapper.Enabled}",
            $"nativeLibrary={NativeWrapper.LoadedNativeLibrary}",
            $"nativeError={NativeWrapper.LastLoadError}",
            $"xraylibEnabled={Xraylib.Enabled}",
            $"xraylibError={Xraylib.LastLoadError}",
            $"glfwEnabled={glfwEnabled}",
            $"glfwError={glfwError}",
        ]);
        Environment.Exit(NativeWrapper.Enabled && Xraylib.Enabled && glfwEnabled ? 0 : 2);
    }

    // 260428Cl 追加
    public static bool ReadDarkMode()
        => Registry.GetValue(DarkModeRegPath, DarkModeRegName, 0) is 1;

    // 260428Cl 追加
    public static void WriteDarkMode(bool dark)
        => Registry.SetValue(DarkModeRegPath, DarkModeRegName, dark ? 1 : 0, RegistryValueKind.DWord);
}
