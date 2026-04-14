using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Crystallography.Controls;

public partial class PyRichTextBox : RichTextBox
{
    #region "変数・定数"

    private Point _CaretPosition;// //カレットの位置(ポイント型)
    private int _CaretIndex;//カレット位置

    //private int _WordStart; //単語の開始位置
    private string _CurrentText;//単語の文字列用

    private bool _ProcessFlag = false;//入力状態のフラグ用
    private readonly ListBox listBox = new();//入力候補のポップアップリスト
    //private Timer timer = new Timer();

    private readonly ToolTip tooltip = new();

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
    // (260322Ch) WFO1000: Microsoft ??????????????????? ???????????
    // 260414Cl setter で Python 基本文法テンプレートを自動マージ (呼び出し側は無変更)
    [System.ComponentModel.Browsable(false)]
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
    public string[] AutoCompleteItems
    {
        get => _AutoCompleteItems;
        set => _AutoCompleteItems = [.. value ?? [], .. _pythonBuiltinNames];
    }

    [System.ComponentModel.Browsable(false)]
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
    public string[] ToolTipItems
    {
        get => _ToolTipItems;
        set => _ToolTipItems = [.. value ?? [], .. _pythonBuiltinTooltips];
    }

    // 260414Cl 追加 true の間は TextChanged 時の AutoComplete ポップアップ処理を抑止する。
    // プログラマティックにテキストを差し替える (Tab インデント等) 場合に使用。
    [System.ComponentModel.Browsable(false)]
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
    public bool SuppressAutoComplete { get; set; } = false;

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

    public PyRichTextBox()
    {
        InitializeComponent();

        this.TextChanged += PyRichTextBox_TextChanged;

        this.PreviewKeyDown += PyRichTextBox_PreviewKeyDown;

        this.listBox.KeyDown += Ls_KeyDown;
        this.KeyDown += PyRichTextBox_KeyDown;

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

    private void PyRichTextBox_TextChanged(object sender, EventArgs e)
    {
        // 260414Cl 追加 プログラマティックなテキスト差し替え中はポップアップを出さない
        if (SuppressAutoComplete)
        {
            listBox.Visible = false;
            return;
        }
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

    private void PyRichTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
    {
        if (e.KeyCode == Keys.ProcessKey) //日本語入力かどうか判定
            _ProcessFlag = true; //日本語入力
        else
            _ProcessFlag = false; //英語入力

        // SetFixFont(); //フォント固定化
        if (e.KeyCode == Keys.Tab) //押したキーがタブキーかどうか判定
            e.IsInputKey = true; //通常入力キーが押された事をtrueとする
    }

    private void PyRichTextBox_KeyDown(object sender, KeyEventArgs e)
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
        // 260414Cl 追加 autocomplete がキーを消費していなければインデント処理へ回す
        if (e.Handled || e.SuppressKeyPress || listBox.Visible) return;
        if (e.KeyCode == Keys.Enter && e.Modifiers == Keys.None)
        {
            handleEnterAutoIndent();
            e.Handled = e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Back && e.Modifiers == Keys.None && handleSmartBackspace())
        {
            e.Handled = e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Tab && (e.Modifiers == Keys.None || e.Modifiers == Keys.Shift))
        {
            handleTabIndent(e.Shift);
            e.Handled = e.SuppressKeyPress = true;
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

    // 260414Cl 追加 "単語の境界" 判定 (前の文字が英数字・_ でなければ境界扱い)
    private static bool IsWordBoundary(char c) => !char.IsLetterOrDigit(c) && c != '_';

    public bool GetAutoCompleteList(FindType Findtype = FindType.Including)
    {
        listBox.Items.Clear();
        if (_AutoCompleteItems == null || _CurrentText == "") return false;
        var ci = System.Globalization.CultureInfo.CurrentCulture.CompareInfo;
        var opts = System.Globalization.CompareOptions.IgnoreKanaType
                 | System.Globalization.CompareOptions.IgnoreWidth
                 | System.Globalization.CompareOptions.IgnoreCase;

        // 260414Cl 追加 優先順位付きで候補を収集し最後にソートする。
        // prio: 0=完全一致 / 1=先頭一致 / 2=単語境界一致 (例: "ReciPro.Dir" の "Dir") / 3=素の部分一致
        // 同一優先度は元の並び順を維持するため元インデックスを第 2 キーに使う。
        var candidates = new List<(string item, int prio, int idx)>();
        for (int i = 0; i < _AutoCompleteItems.Length; i++)
        {
            var s = _AutoCompleteItems[i];
            int prio = -1;
            switch (Findtype)
            {
                case FindType.Including:
                    int hit = ci.IndexOf(s, _CurrentText, opts);
                    if (hit > -1)
                    {
                        if (string.Equals(s, _CurrentText, StringComparison.OrdinalIgnoreCase)) prio = 0;
                        else if (s.StartsWith(_CurrentText, StringComparison.OrdinalIgnoreCase)) prio = 1;
                        else if (hit > 0 && IsWordBoundary(s[hit - 1])) prio = 2;
                        else prio = 3;
                    }
                    break;
                case FindType.Initials:
                    if (s.StartsWith(_CurrentText, StringComparison.OrdinalIgnoreCase)) prio = 1;
                    break;
                case FindType.Ending:
                    if (ci.IsSuffix(s, _CurrentText, opts)) prio = 3;
                    break;
            }
            if (prio >= 0) candidates.Add((s, prio, i));
        }
        candidates.Sort((a, b) =>
        {
            int c = a.prio.CompareTo(b.prio);
            return c != 0 ? c : a.idx.CompareTo(b.idx);
        });
        foreach (var (item, _, _) in candidates)
            listBox.Items.Add(item);

        if (candidates.Count == 0) return false;
        listBox.SelectedIndex = 0;
        if (_CurrentText == listBox.SelectedItem.ToString())
        {
            listBox.Items.Clear();
            return false;
        }
        return true;
    }

    #endregion "イベント"

    #region "マクロ編集機能" 260414Cl 追加
    // 行番号ガター / キャレット位置取得 / エラー行ハイライト /
    // オートインデント / スマート Backspace / Tab インデント /
    // Python 基本文法テンプレート。
    // 旧 FormMacro の局所実装を Python 専用コントロール側に集約。

    private const string IndentUnit = "    "; // 4 スペース = 1 インデント段

    // Python basic syntax templates. print / input / open / yield etc. are excluded
    // because they have no effect in the ReciPro macro environment (no stdout/stdin,
    // file I/O is out of scope).
    private static readonly (string name, string tooltip)[] _pythonBuiltins =
    {
        ("if <cond>:",                 "Conditional branch"),
        ("elif <cond>:",               "Additional condition following an if"),
        ("else:",                      "Fallback branch when no condition matches"),
        ("for <var> in <iterable>:",   "Iterate over each item of iterable"),
        ("for i in range(<n>):",       "Repeat n times (i goes 0 .. n-1)"),
        ("while <cond>:",              "Loop while condition is true"),
        ("def <name>(<args>):",        "Function definition"),
        ("return <value>",             "Return a value from a function"),
        ("class <Name>:",              "Class definition"),
        ("try:",                       "Begin an exception-handling block"),
        ("except Exception:",          "Catch an exception"),
        ("finally:",                   "Cleanup block (runs whether or not an exception occurred)"),
        ("pass",                       "Do nothing (placeholder for an empty block)"),
        ("break",                      "Exit the enclosing loop"),
        ("continue",                   "Skip to the next iteration of the loop"),
        ("lambda <args>: <expr>",      "Anonymous function"),
        ("True",                       "Boolean true"),
        ("False",                      "Boolean false"),
        ("None",                       "Null / no value"),
        ("len(<obj>)",                 "Number of elements in the object"),
        ("range(<n>)",                 "Integer sequence 0 .. n-1"),
        ("range(<start>, <stop>)",     "Integer sequence start .. stop-1"),
        ("abs(<x>)",                   "Absolute value"),
        ("min(<iter>)",                "Smallest element"),
        ("max(<iter>)",                "Largest element"),
        ("sum(<iter>)",                "Sum of elements"),
        ("sorted(<iter>)",             "Return a new sorted list"),
        ("enumerate(<iter>)",          "Iterate as (index, value) pairs"),
        ("zip(<a>, <b>)",              "Iterate two iterables in parallel"),
        ("int(<x>)",                   "Convert to integer"),
        ("float(<x>)",                 "Convert to floating-point number"),
        ("str(<x>)",                   "Convert to string"),
        ("list(<iter>)",               "Convert to list"),
        ("math.sqrt(<x>)",             "Square root"),
        ("math.sin(<x>)",              "Sine (radians)"),
        ("math.cos(<x>)",              "Cosine (radians)"),
        ("math.pi",                    "The constant pi"),
        ("math.radians(<deg>)",        "Convert degrees to radians"),
        ("math.degrees(<rad>)",        "Convert radians to degrees"),
    };
    private static readonly string[] _pythonBuiltinNames;
    private static readonly string[] _pythonBuiltinTooltips;
    static PyRichTextBox()
    {
        int n = _pythonBuiltins.Length;
        _pythonBuiltinNames = new string[n];
        _pythonBuiltinTooltips = new string[n];
        for (int i = 0; i < n; i++)
        {
            _pythonBuiltinNames[i] = _pythonBuiltins[i].name;
            _pythonBuiltinTooltips[i] = _pythonBuiltins[i].tooltip;
        }
    }

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
    private const int EM_GETFIRSTVISIBLELINE = 0xCE;

    private Panel _gutterPanel;
    private Font _gutterFont;
    private int _gutterYOffset;

    private static readonly System.Text.RegularExpressions.Regex _lineRegex
        = new(@"line (\d+)", System.Text.RegularExpressions.RegexOptions.Compiled);

    // 外部の Panel を行番号ガターとして紐付ける。Paint / TextChanged / VScroll 配線を自動で行う。
    // 2 回目以降の呼び出しは無視 (イベント二重配線防止)。
    public void AttachGutter(Panel gutterPanel)
    {
        if (_gutterPanel != null) return;
        _gutterPanel = gutterPanel;
        gutterPanel.Paint += gutter_Paint;
        this.TextChanged += (s, e) => gutterPanel.Invalidate();
        this.VScroll += (s, e) => gutterPanel.Invalidate();
    }

    private void gutter_Paint(object sender, PaintEventArgs e)
    {
        if (_gutterFont == null)
        {
            var mf = this.Font;
            _gutterFont = new Font(mf.FontFamily, mf.Size * 0.7f);
            // ベースライン揃え: 同一ファミリなので ascent/lineSpacing 比を掛けて差分から Y オフセットを算出
            var ratio = (float)mf.FontFamily.GetCellAscent(mf.Style) / mf.FontFamily.GetLineSpacing(mf.Style);
            _gutterYOffset = (int)Math.Round(ratio * (mf.GetHeight() - _gutterFont.GetHeight()));
        }
        int firstVisible = SendMessage(this.Handle, EM_GETFIRSTVISIBLELINE, 0, 0);
        int lastLine = this.GetLineFromCharIndex(this.TextLength);
        int h = _gutterPanel.Height;
        for (int i = firstVisible; i <= lastLine; i++)
        {
            int charIdx = this.GetFirstCharIndexFromLine(i);
            if (charIdx < 0) continue;
            var pos = this.GetPositionFromCharIndex(charIdx);
            if (pos.Y > h) break;
            var text = (i + 1).ToString();
            var size = TextRenderer.MeasureText(e.Graphics, text, _gutterFont);
            TextRenderer.DrawText(e.Graphics, text, _gutterFont,
                new Point(_gutterPanel.Width - size.Width - 2, pos.Y + _gutterYOffset),
                Color.LightGray);
        }
    }

    // 現在のキャレット位置を 1-based (line, col) で返す。末尾改行直後の空行も正しく扱う。
    public (int line, int col) GetLineColumn()
    {
        int idx = this.SelectionStart;
        int line = this.GetLineFromCharIndex(idx) + 1;
        int col = idx - this.GetFirstCharIndexFromLine(line - 1) + 1;
        if (idx > 0 && idx == this.TextLength && this.Text[idx - 1] == '\n')
        {
            line++;
            col = 1;
        }
        return (line, col);
    }

    // Python トレースバックから最後の "line N" (= 最内側フレーム) を抽出して該当行を選択
    public void HighlightErrorLineFromTraceback(string message)
    {
        var matches = _lineRegex.Matches(message);
        if (matches.Count == 0) return;
        if (!int.TryParse(matches[^1].Groups[1].Value, out int ln)) return;
        int idx = ln - 1;
        if (idx < 0 || idx >= this.Lines.Length) return;
        this.SelectionStart = this.GetFirstCharIndexFromLine(idx);
        this.SelectionLength = this.Lines[idx].Length;
        this.Focus();
    }

    // Enter: 現在行の先頭空白/タブを継承。キャレットまでが ':' で終わるなら 1 段インデント追加。
    private void handleEnterAutoIndent()
    {
        int caret = this.SelectionStart;
        int lineIdx = this.GetLineFromCharIndex(caret);
        var line = lineIdx < this.Lines.Length ? this.Lines[lineIdx] : "";
        int ws = 0;
        while (ws < line.Length && (line[ws] == ' ' || line[ws] == '\t')) ws++;
        var indent = line[..ws];
        int posInLine = caret - this.GetFirstCharIndexFromLine(lineIdx);
        if (posInLine <= line.Length && line[..Math.Min(posInLine, line.Length)].TrimEnd().EndsWith(':'))
            indent += IndentUnit;
        this.SelectedText = "\n" + indent;
    }

    // Backspace: カーソル位置までが全て空白/タブなら直近のインデント境界まで一気に削除
    private bool handleSmartBackspace()
    {
        if (this.SelectionLength > 0) return false;
        int caret = this.SelectionStart;
        int lineIdx = this.GetLineFromCharIndex(caret);
        int lineStart = this.GetFirstCharIndexFromLine(lineIdx);
        int posInLine = caret - lineStart;
        if (posInLine == 0) return false;
        var line = this.Lines[lineIdx];
        for (int i = 0; i < posInLine; i++)
            if (line[i] != ' ' && line[i] != '\t') return false;
        int unit = IndentUnit.Length;
        int del = posInLine % unit;
        if (del == 0) del = unit;
        if (del > posInLine) del = posInLine;
        this.SelectionStart = caret - del;
        this.SelectionLength = del;
        this.SelectedText = "";
        return true;
    }

    // Tab: 単一行ではキャレット位置にインデント挿入 (Shift+Tab は現在行を逆インデント)。
    //      複数行選択では各行の先頭で indent/outdent し、選択範囲を結果にそろえる。
    //      SuppressAutoComplete で囲み、SelectedText 差し替えによる誤ポップアップ発動を防ぐ。
    private void handleTabIndent(bool shift)
    {
        SuppressAutoComplete = true;
        try { handleTabIndentCore(shift); }
        finally { SuppressAutoComplete = false; }
    }

    private void handleTabIndentCore(bool shift)
    {
        // Lines プロパティは毎回 Text を Split して配列を再生成するので、一度だけ取得する。
        var lines = this.Lines;
        int selStart = this.SelectionStart;
        int selLen = this.SelectionLength;
        int selEnd = selStart + selLen;
        int startLine = this.GetLineFromCharIndex(selStart);
        int endLine = this.GetLineFromCharIndex(selEnd);
        if (selLen > 0 && selEnd == this.GetFirstCharIndexFromLine(endLine) && endLine > startLine)
            endLine--;

        if (startLine == endLine)
        {
            if (!shift)
            {
                this.SelectedText = IndentUnit;
                return;
            }
            int ls = this.GetFirstCharIndexFromLine(startLine);
            var ln = lines[startLine];
            int r = 0;
            while (r < IndentUnit.Length && r < ln.Length && (ln[r] == ' ' || ln[r] == '\t')) r++;
            if (r == 0) return;
            this.SelectionStart = ls;
            this.SelectionLength = r;
            this.SelectedText = "";
            this.SelectionStart = Math.Max(ls, selStart - r);
            return;
        }

        int firstLineStart = this.GetFirstCharIndexFromLine(startLine);
        int endLineStart = this.GetFirstCharIndexFromLine(endLine);
        int endLineLen = endLine < lines.Length ? lines[endLine].Length : 0;
        int rangeEnd = endLineStart + endLineLen;
        var sb = new System.Text.StringBuilder();
        for (int i = startLine; i <= endLine; i++)
        {
            var line = i < lines.Length ? lines[i] : "";
            if (shift)
            {
                int r = 0;
                while (r < IndentUnit.Length && r < line.Length && (line[r] == ' ' || line[r] == '\t')) r++;
                sb.Append(line[r..]);
            }
            else
            {
                sb.Append(IndentUnit);
                sb.Append(line);
            }
            if (i < endLine) sb.Append('\n');
        }
        this.SelectionStart = firstLineStart;
        this.SelectionLength = rangeEnd - firstLineStart;
        this.SelectedText = sb.ToString();
        this.SelectionStart = firstLineStart;
        this.SelectionLength = sb.Length;
    }
    #endregion
}