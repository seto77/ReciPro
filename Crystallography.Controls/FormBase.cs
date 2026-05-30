using System;
using System.Diagnostics;
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

    protected FormBase()
    {
        InitializeComponent();
        HelpRequested += FormBase_HelpRequested; //260529Cl 追加: F1 キー (HelpRequested) を購読
    }

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
