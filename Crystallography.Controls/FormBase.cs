using System;
using System.Diagnostics;
using System.Drawing;                  //260604Cl 追加: タイトルバー右寄せのテキスト計測用
using System.Runtime.InteropServices;  //260604Cl 追加: WM_GETTITLEBARINFOEX 取得用
using System.Windows.Forms;

namespace Crystallography.Controls;

[System.ComponentModel.ToolboxItem(false)]
public partial class FormBase : Form
{
    //260529Cl 追加: F1 キーで該当オンラインマニュアルを開く仕組み。
    //「どのアプリのどのマニュアルを開くか」はホストアプリ固有の知識なので、Controls には持たせない。
    //ホストアプリが起動時に HelpUrlResolver を 1 回登録し、各フォームには HelpPage(ページ識別子) を設定する。

    /// <summary>
    /// F1 押下時に開くマニュアル URL を解決するデリゲート。ホストアプリが起動時に 1 回設定する。
    /// 引数の FormBase (HelpPage 等) を見て URL 文字列を返す。null や空文字を返すと何も開かない。
    /// </summary>
    public static Func<FormBase, string> HelpUrlResolver { get; set; } //260529Cl 追加

    /// <summary>
    /// このフォームに対応するマニュアルのページ識別子。HelpUrlResolver が URL を組み立てる際に使う。
    /// 値の意味 (スラッグ/番号など) はホストアプリの解決ロジックに依存する。
    /// </summary>
    //260530Cl 追加: コードからのみ設定するプロパティなのでデザイナのシリアライズ対象外にする (WFO1000 回避)。
    //         Crystallography.Controls は WFO1000 をプロジェクト単位で抑止しない方針(260322Ch)のため、CommonDialog.cs と同じく個別属性で対応する。
    [System.ComponentModel.Browsable(false)]
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
    public string HelpPage { get; set; } = ""; //260529Cl 追加

    //260604Cl 追加: F1 でヘルプを開けるフォームのタイトル右端に出す案内文字列 (UI 言語で切替)。
    //【真因と対策 (260605Cl 調査)】キャプション(タイトルバー)文字は Windows/DWM が "255 文字までしか描画しない"
    //(256 文字バッファ=255文字+NUL。実測で Text.Length>=256 で末尾が右から欠ける。Web 傍証: RC4206 "truncated at 256" 等)。
    //通常スペースで右寄せすると幅広フォームでは詰めるスペースが多く 256 文字を超え、末尾の案内が右から消えていた。
    //→ 全角スペース(U+3000, 通常の約4倍幅)で詰めれば約1/4の文字数で同じ右寄せができ 255 文字上限に達しにくい。
    //  さらに総文字数を HelpCharLimit でクランプし、極端に広い窓 (4K 最大化等) でも案内が消えない (右寄せ位置は少し内側で妥協)。
    private const char HelpPadChar = '　';        //260605Cl 追加: 詰めに使う全角スペース
    private const string HelpCoreEn = "(F1: Help)";   //260604Cl 追加
    private const string HelpCoreJa = "(F1: ヘルプ)";  //260604Cl 追加
    private const int HelpCharLimit = 250;            //260605Cl 追加: 255文字描画上限に対する安全な総文字数クランプ
    private const int HelpFallbackPad = 6;            //260605Cl 追加: 右寄せ量を計算できない時の固定全角スペース数
    private const int HelpMinPad = 2;                 //260605Cl 追加: タイトルと案内が接触しないための最小詰め数
    private bool isUpdatingHelpSuffix = false;        //260604Cl 追加: 自分の Text 書換で再入する TextChanged を弾くフラグ

    //260604Cl 追加: タイトルに F1 ヘルプ案内を付けるか。出したくない派生フォーム (CommonDialog 等) は override で false にする。
    protected virtual bool ShowHelpSuffix => true;

    protected FormBase()
    {
        InitializeComponent();
        HelpRequested += FormBase_HelpRequested; //260529Cl 追加: F1 キー (HelpRequested) を購読

        //260604Cl 追加: F1 でヘルプが開けるフォームは、タイトル右端に案内 ("(F1: Help)" / 日本語 UI なら "(F1: ヘルプ)") を出して存在を知らせる。
        //表示時点では HelpPage / HelpUrlResolver が確定しているので Shown で初回付与し、タイトル変更や幅変更でも付け直す。
        Shown += (s, e) => AppendHelpSuffix();
        TextChanged += (s, e) => AppendHelpSuffix();
        Resize += (s, e) => AppendHelpSuffix();
    }

    //260604Cl 追加: タイトル(Text)の右端に現在の UI 言語の案内文字列を配置する (本文との間を全角スペースで詰めて右寄せ)。
    //HelpUrlResolver が未登録/空 URL を返すフォーム (他ホストアプリや設計時) には付けない。
    private void AppendHelpSuffix()
    {
        if (isUpdatingHelpSuffix)
            return; //自分が起こした TextChanged の再入を弾く (無限ループ回避)
        if (!ShowHelpSuffix)
            return; //この派生フォームは案内を出さない (CommonDialog 等)
        if (string.IsNullOrEmpty(HelpUrlResolver?.Invoke(this)))
            return; //F1 で開くヘルプが無ければ付けない

        var core = System.Globalization.CultureInfo.CurrentUICulture.Name == "ja" ? HelpCoreJa : HelpCoreEn;
        var baseText = StripHelpSuffix(Text); //既存の案内を一旦取り除いた素のタイトル

        int pad = ComputeRightAlignPad(baseText, core); //右寄せに必要な全角スペース数
        if (pad < 0)
            pad = HelpFallbackPad; //計測できない時 (ハンドル未生成・枠無し等) は固定数でフォールバック

        var desired = baseText + new string(HelpPadChar, pad) + core;
        if (Text == desired)
            return; //既に正しい状態 (再表示・連続更新・同幅 Resize 時のちらつき防止)

        isUpdatingHelpSuffix = true;
        try { Text = desired; }
        finally { isUpdatingHelpSuffix = false; }
    }

    //260604Cl 追加: 既存の案内 (両言語) と直前の詰め文字 (全角/半角スペース) を取り除いた素のタイトルを返す。
    //言語切替で逆言語の案内が残っていても二重化しないよう、core 一致で末尾を剥がす。
    private static string StripHelpSuffix(string text)
    {
        string core = text.EndsWith(HelpCoreEn, StringComparison.Ordinal) ? HelpCoreEn
                    : text.EndsWith(HelpCoreJa, StringComparison.Ordinal) ? HelpCoreJa : null;
        return core == null ? text : text.Substring(0, text.Length - core.Length).TrimEnd(HelpPadChar, ' ');
    }

    //260605Cl 追加: baseText の後ろに全角スペースを何個入れれば core がキャプション右端 (ボタン手前) に来るかを算出する。
    //・位置はピクセルで右寄せ (WM_GETTITLEBARINFOEX のボタン左 + GenericTypographic の MeasureString は実描画と一致)。
    //・総文字数を HelpCharLimit でクランプし、255 文字描画上限による末尾欠けを防ぐ (超広幅では右寄せ位置が少し内側になる)。
    //計測不能 (ハンドル未生成・枠無し・最小化・キャプション無し等) なら -1 を返し、呼び出し側で固定数にフォールバックする。
    private int ComputeRightAlignPad(string baseText, string core)
    {
        try
        {
            if (!IsHandleCreated || FormBorderStyle == FormBorderStyle.None || WindowState == FormWindowState.Minimized)
                return -1;

            //タイトルバーと各ボタンの矩形 (スクリーン座標, 物理px) を取得
            var info = new TITLEBARINFOEX { cbSize = Marshal.SizeOf<TITLEBARINFOEX>(), rgstate = new int[6], rgrect = new RECT[6] };
            SendMessage(Handle, WM_GETTITLEBARINFOEX, IntPtr.Zero, ref info);

            int titleLeft = info.rcTitleBar.Left, titleRight = info.rcTitleBar.Right;
            if (titleRight <= titleLeft)
                return -1; //キャプション無し

            //可視ボタン (index 2..5 = 最小化/最大化/ヘルプ/閉じる) のうち最も左の Left を、本文を置ける右端境界にする
            int rightBoundary = titleRight;
            for (int i = 2; i <= 5; i++)
            {
                if ((info.rgstate[i] & STATE_SYSTEM_INVISIBLE) != 0) continue;
                var r = info.rgrect[i];
                if (r.Right > r.Left && r.Left < rightBoundary)
                    rightBoundary = r.Left;
            }

            float scale = DeviceDpi / 96f;
            bool toolWindow = FormBorderStyle is FormBorderStyle.FixedToolWindow or FormBorderStyle.SizableToolWindow;
            bool hasIcon = ShowIcon && ControlBox && !toolWindow;
            int margin = (int)(12 * scale);                                             //ボタンとの間に確保する余白
            int iconOffset = hasIcon ? (int)(SystemInformation.SmallIconSize.Width + 12 * scale) : (int)(6 * scale);
            int textStart = titleLeft + iconOffset;                                      //本文の左端 (アイコン分のおおよその推定)
            int availRight = rightBoundary - margin;
            if (availRight <= textStart)
                return -1; //幅が無い

            using var font = toolWindow ? SystemFonts.SmallCaptionFont : SystemFonts.CaptionFont;
            if (font == null)
                return -1;

            //実描画(DWM)と一致する GenericTypographic の MeasureString で計測 (計測単位はフォームの DPI = 物理px で右端境界と整合)。
            using var g = CreateGraphics();
            float Measure(string s) => g.MeasureString(s, font, int.MaxValue, StringFormat.GenericTypographic).Width;

            float padW = (Measure("x" + new string(HelpPadChar, 50) + "x") - Measure("xx")) / 50f; //全角スペース1個の幅
            if (padW <= 0.1f)
                return -1;
            padW *= 1.02f; //安全側: わずかに左に余らせて右のボタンに被らせない

            float baseW = Measure(baseText);
            float coreW = Measure(core);
            int pad = (int)((availRight - (textStart + baseW) - coreW) / padW);

            //255 文字描画上限ガード: 総文字数 (baseText + pad + core) を HelpCharLimit 以下にクランプ
            int maxByChars = Math.Max(0, HelpCharLimit - baseText.Length - core.Length);
            if (pad > maxByChars) pad = maxByChars;
            if (pad < HelpMinPad) pad = Math.Min(HelpMinPad, maxByChars);
            return pad;
        }
        catch { return -1; }
    }

    #region 260604Cl 追加: タイトルバー右寄せ用の WM_GETTITLEBARINFOEX 取得
    private const int WM_GETTITLEBARINFOEX = 0x033F;
    private const int STATE_SYSTEM_INVISIBLE = 0x00008000;

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT { public int Left, Top, Right, Bottom; }

    [StructLayout(LayoutKind.Sequential)]
    private struct TITLEBARINFOEX
    {
        public int cbSize;
        public RECT rcTitleBar;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public int[] rgstate;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public RECT[] rgrect;
    }

    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref TITLEBARINFOEX lParam);
    #endregion

    //260529Cl 追加: F1 押下時に HelpUrlResolver が返す URL を既定ブラウザで開く
    private void FormBase_HelpRequested(object sender, HelpEventArgs hlpevent)
    {
        var url = HelpUrlResolver?.Invoke(this);
        if (!string.IsNullOrEmpty(url))
        {
            try { Process.Start(new ProcessStartInfo(url) { UseShellExecute = true }); }
            catch { }
        }

        if (hlpevent != null)
            hlpevent.Handled = true;
    }
}
