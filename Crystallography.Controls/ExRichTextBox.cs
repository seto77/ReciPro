using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Crystallography.Controls;

public partial class ExRichTextBox : System.Windows.Forms.RichTextBox
{
    #region "変数・定数"

    private Point _CaretPosition;// //カレットの位置(ポイント型)
    private int _CaretIndex;//カレット位置

    //private int _WordStart; //単語の開始位置
    private string _CurrentText;//単語の文字列用

    private bool _ProcessFlag = false;//入力状態のフラグ用
    private readonly ListBox listBox = new();//入力候補のポップアップリスト
    //private Timer timer = new Timer();

    private ToolTip tooltip = new();

    //private Label toolTipLabel = new Label();
    private string[] _AutoCompleteItems;//入力候補事前登録用

    private string[] _ToolTipItems;//入力候補事前登録用
                                   // private System.ComponentModel.IContainer components;//コンテナーコンポーネント

    #endregion "変数・定数"

    #region "プロパティ"

    //    <system.componentmodel.editorbrowsable(system.componentmodel.editorbrowsablestate.never)> _
    //    <system.componentmodel.browsable(false)> _
    //    <system.componentmodel.category("カスタム")> _
    //    <system.componentmodel.description("現在のカレット位置を取得します。")> _
    public Point CaretPosition { get { return _CaretPosition; } }

    //<system.componentmodel.editorbrowsable(system.componentmodel.editorbrowsablestate.always)> _
    //<system.componentmodel.browsable(true)> _
    // <system.componentmodel.category("カスタム")> _
    // <system.componentmodel.description("入力候補を設定、または取得します。")> _
    public string[] AutoCompleteItems { get { return _AutoCompleteItems; } set { _AutoCompleteItems = value; } }

    public string[] ToolTipItems { get { return _ToolTipItems; } set { _ToolTipItems = value; } }

    public string[] TextLines { get { return this.Text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None); } }

    #endregion "プロパティ"

    #region "列挙型"

    ////// <summary>
    ////// マッチング方式の種類
    ////// </summary>
    ////// <remarks></remarks>
    public enum FindType
    {
        Including = 1,
        Match = 2,
        Initials = 3,
        Ending = 4
    }

    #endregion "列挙型"

    #region "コンストラクタ"

    public ExRichTextBox()
    {
        InitializeComponent();

        this.TextChanged += ExRichTextBox_TextChanged;

        this.PreviewKeyDown += ExRichTextBox_PreviewKeyDown;

        this.listBox.KeyDown += Ls_KeyDown;
        this.KeyDown += ExRichTextBox_KeyDown;

        listBox.VisibleChanged += listBox_VisibleChanged;

        listBox.IntegralHeight = true;

        listBox.DrawMode = DrawMode.OwnerDrawVariable;
        listBox.MeasureItem += Ls_MeasureItem;
        listBox.DrawItem += Ls_DrawItem;

        this.LanguageOption = RichTextBoxLanguageOptions.UIFonts;
    }

    private void listBox_VisibleChanged(object sender, EventArgs e)
    {
        if (!listBox.Visible)
            tooltip.Hide(this);
    }

    private void Ls_MeasureItem(object sender, MeasureItemEventArgs e)
    {
        e.ItemHeight = (int)listBox.Font.GetHeight();
    }

    private void Ls_DrawItem(object sender, DrawItemEventArgs e)
    {
        if (e.Index < 0) return;
        //背景を描画する
        //項目が選択されている時は強調表示される
        e.DrawBackground();
        Brush b = new SolidBrush(e.ForeColor);
        //描画する文字列の取得
        string txt = ((ListBox)sender).Items[e.Index].ToString();
        //文字列の描画
        e.Graphics.DrawString(txt, e.Font, b, e.Bounds);
        //後始末
        b.Dispose();

        if (listBox.ClientSize.Width < (int)e.Graphics.MeasureString(txt, e.Font).Width + 20)
            listBox.Width = (int)e.Graphics.MeasureString(txt, e.Font).Width + 40;

        if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
        {
            int n = new List<string>(AutoCompleteItems).IndexOf(txt);
            if (n >= 0)
                tooltip.Show(ToolTipItems[n], this, listBox.Right, e.Bounds.Y + listBox.Top, 30000);
        }
    }

    #endregion "コンストラクタ"

    #region "メッセージ"

    /*[DllImport("user32")]
    public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
    private void SetFixFont()
    {
        //メッセージを送り、RichTextのFontが勝手に変わるのを抑制する
        int lPar;
        lPar = SendMessage(this.Handle, EM_GETLANGOPTIONS, 0, 0);
        lPar = lPar & (~IMF_DUALFONT);
        SendMessage(this.Handle, EM_SETLANGOPTIONS, 0, lPar);
    }*/

    // private const uint IMF_DUALFONT = 0x80;
    // private const uint WM_USER = 0x0400;
    // private const uint EM_SETLANGOPTIONS = WM_USER + 120;
    // private const uint EM_GETLANGOPTIONS = WM_USER + 121;

    #endregion "メッセージ"

    #region "イベント"

    private void ExRichTextBox_TextChanged(object sender, EventArgs e)
    {
        int i = this.SelectionStart; //カレット位置
        string s = Text[..i]; //カレット位置までの文字列取得
        int c = -1; //区切り開始位置取得用
        //単語の区切り位置を取得する処理

        if (c < s.LastIndexOf(' ')) c = s.LastIndexOf(' '); //半角空白位置取得
        if (c < s.LastIndexOf('　')) c = s.LastIndexOf('　'); //全角空白位置取得
        if (c < s.LastIndexOf('\t')) c = s.LastIndexOf('\t'); //空白タブ位置取得
        if (c < s.LastIndexOf('\n')) c = s.LastIndexOf('\n'); //改行(ラインフィールド)位置取得

        if (c < i)
        { //カレットの位置が前回の区切り位置より後なら
            _CurrentText = s.Substring(c + 1, i - c - 1);// Mid(s, c + 1, i - c); //現在編集中の単語取得
            if (GetAutoCompleteList(FindType.Including)) //入力候補と入力中単語が一致するか判定
                listBox.Visible = true; //一致したら、ポップアップ表示
            else
                listBox.Visible = false; //不一致なら、ポップアップ非表示
        }
        else
        { //区切り位置とカレット位置が同じ、もしくは前なら
            _CurrentText = ""; //編集中単語を空文字に
            listBox.Visible = false; //ポップアップ非表示
        }

        if (listBox.Visible)
        {
            if (_ProcessFlag == false)
                _CaretPosition = this.GetPositionFromCharIndex(this.SelectionStart); //カレットの現在位置ポイント取得

            listBox.BringToFront();
            listBox.Top = _CaretPosition.Y + 15;
            listBox.Left = _CaretPosition.X + 5;
            listBox.IntegralHeight = false;

            int fullHeight = listBox.Items.Count * listBox.GetItemHeight(0);

            listBox.ClientSize = new Size(1, Math.Min(this.Height / 2, fullHeight));
            listBox.ColumnWidth = 0;
            listBox.Top = Math.Min(this.Height - listBox.Height - 5, _CaretPosition.Y + 15);
        }
    }

    private void ExRichTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
    {
        if (e.KeyCode == Keys.ProcessKey) //日本語入力かどうか判定
            _ProcessFlag = true; //日本語入力
        else
            _ProcessFlag = false; //英語入力

        // SetFixFont(); //フォント固定化
        if (e.KeyCode == Keys.Tab) //押したキーがタブキーかどうか判定
            e.IsInputKey = true; //通常入力キーが押された事をtrueとする
    }

    private void ExRichTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        _CaretIndex = this.SelectionStart; //カレットの現在位置取得
        if (_ProcessFlag == false)
            _CaretPosition = this.GetPositionFromCharIndex(this.SelectionStart); //カレットの現在位置ポイント取得

        switch (e.KeyCode)
        {
            case Keys.Down:
                if (listBox.Visible)
                {
                    e.SuppressKeyPress = true;
                    if (listBox.SelectedIndex < listBox.Items.Count - 1)
                        listBox.SelectedIndex += 1;
                }
                break;

            case Keys.Up:
                if (listBox.Visible)
                {
                    e.SuppressKeyPress = true;
                    if (listBox.SelectedIndex > 0)
                        listBox.SelectedIndex -= 1;
                }
                break;

            case Keys.Enter:
                _ProcessFlag = false;
                if (listBox.Visible)
                {
                    if (listBox.SelectedIndex < 0)
                        listBox.SelectedIndex = 0;
                    e.SuppressKeyPress = true;
                    SetAutoCompleteText();
                    listBox.Visible = false;
                }
                break;

            case Keys.Right:
                if (listBox.Visible)
                    e.SuppressKeyPress = true;
                break;

            case Keys.Left:
                if (listBox.Visible)
                    e.SuppressKeyPress = true;
                break;

            case Keys.Escape:
                if (listBox.Visible)
                {
                    e.SuppressKeyPress = true;
                    listBox.Visible = false;
                }
                break;
            /* case Keys.Space:
                 if (listBox.Visible)
                 {
                     if (listBox.SelectedIndex > -1)
                     {
                         e.SuppressKeyPress = true;
                         SetAutoCompleteText();
                         listBox.Visible = false;
                     }
                 }
                 break;*/
            case Keys.Tab:
                if (listBox.Visible)
                {
                    if (listBox.SelectedIndex > -1)
                    {
                        e.SuppressKeyPress = true;
                        SetAutoCompleteText();
                        listBox.Visible = false;
                    }
                }
                break;
        }
    }

    private void Ls_KeyDown(Object sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.Enter: //エンター
                SetAutoCompleteText(); //選択された入力候補を適用する
                this.Focus(); //リッチテキスト本体にフォーカスを移す
                break;

            case Keys.Escape: //エスケープキー
                this.Focus(); //リッチテキスト本体にフォーカスを移す
                listBox.Visible = false; //ポップアップを非表示にする
                break;
        }
    }

    private void SetAutoCompleteText()
    {
        int i = this.SelectionStart; //カレット位置
        string s = Text[..i]; //カレット位置までの文字を取得
        int c = 0; //区切り開始位置取得用
        if (c < s.LastIndexOf(' ')) c = s.LastIndexOf(' '); //半角空白位置取得
        if (c < s.LastIndexOf('　')) c = s.LastIndexOf('　');//全角空白位置取得
        if (c < s.LastIndexOf('\t')) c = s.LastIndexOf('\t'); //空白タブ位置取得
        if (c < s.LastIndexOf('\n')) c = s.LastIndexOf('\n'); //改行(ラインフィールド)位置取得
        this.SelectionStart = c == 0 ? c : c + 1;
        this.SelectionLength = c == 0 ? i - c : i - c - 1;
        this.SelectedText = listBox.SelectedItem.ToString();

        if (listBox.SelectedItem != null && listBox.SelectedItem.ToString().Contains('(') && listBox.SelectedItem.ToString().Contains(')'))
        {
            string str = listBox.SelectedItem.ToString();
            int first = str.IndexOf('(') + 1;
            int end = str.IndexOf(')');
            this.SelectionStart -= end - first + 1;
            this.SelectionLength = end - first;
        }
    }

    public bool GetAutoCompleteList(FindType Findtype = FindType.Including)
    {
        bool flg = false;
        bool flg1 = false;
        listBox.Items.Clear();
        //文字列比較に現在のカルチャを使用する
        var ci = System.Globalization.CultureInfo.CurrentCulture.CompareInfo;
        if (_AutoCompleteItems != null)
        {
            if (_CurrentText != "")
            {
                foreach (String s in _AutoCompleteItems)
                {
                    switch (Findtype)
                    {
                        case FindType.Including:
                            if (ci.IndexOf(s, _CurrentText, System.Globalization.CompareOptions.IgnoreKanaType | System.Globalization.CompareOptions.IgnoreWidth | System.Globalization.CompareOptions.IgnoreCase) > -1)
                                flg = true;
                            break;
                        //case FindType.Match:
                        //    if (StrConv(_CurrentText, VbStrConv.Wide) = StrConv(s, VbStrConv.Wide))
                        //        flg = true;
                        //    break;
                        case FindType.Initials:
                            //if (ci.IsPrefix(s, _CurrentText, System.Globalization.CompareOptions.None))//, System.Globalization.CompareOptions.IgnoreKanaType | System.Globalization.CompareOptions.IgnoreWidth | System.Globalization.CompareOptions.IgnoreCase))
                            if (s.StartsWith(_CurrentText, StringComparison.OrdinalIgnoreCase))
                                flg = true;
                            break;

                        case FindType.Ending:
                            if (ci.IsSuffix(s, _CurrentText, System.Globalization.CompareOptions.IgnoreKanaType | System.Globalization.CompareOptions.IgnoreWidth | System.Globalization.CompareOptions.IgnoreCase))
                                flg = true;
                            break;
                    }
                    if (flg)
                    {
                        listBox.Items.Add(s);
                        flg = false;
                        flg1 = true;
                    }
                }
            }
        }

        if (flg1)
        {
            listBox.SelectedIndex = 0;
            if (_CurrentText == listBox.SelectedItem.ToString())
            {
                listBox.Items.Clear();
                return false;
            }
            return true;
        }
        else
            return false;
    }

    #endregion "イベント"
}