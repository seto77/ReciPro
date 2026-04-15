using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Crystallography.Controls;

public partial class FormMacro : CaptureFormBase
{
    #region フィールド、プロパティ

    // 260414Cl Task / RunSynchronously は不要だったので削除。
    // CancellationTokenSource は OnTraceback の待機解除用に保持する
    // (IronPython 自体はトークンを尊重しないので Cancel ボタンはステップ実行時のみ有効)。
    private CancellationTokenSource _cancelSource;
    public bool stepByStepMode;
    private readonly MacroBase obj; // 260414Cl 旧: dynamic obj
    private readonly ScriptEngine Engine;
    private readonly ScriptScope Scope;
    private readonly string ScopeName;

    // 260414Cl OnTraceback で生成しないようキャッシュ。
    private readonly IronPython.Runtime.Exceptions.TracebackDelegate _tracebackDelegate;

    // 260414Cl 追加 未保存編集を検知するためのダーティフラグ
    private bool _isDirty = false;
    // 260414Cl 追加 選択戻し・プログラマティック代入時の再入防止 (リポジトリ共通の命名)
    private bool skipEvent = false;
    // 260414Cl 追加 SelectedIndexChanged は変更後に発火するため、直前の選択を保持する
    private int _previousSelectedIndex = -1;
    // 260414Cl 追加 ダーティ時に "*" を付与する基準タイトル。コンストラクタで Text から取得する
    private readonly string _titleBase;

    // 260415Cl 追加 サンプルマクロ表示トグル用フィールド。表示状態は checkBoxSamples.Checked が真実の情報源。
    private MacroEntry[] _userMacroSnapshot = [];
    private int _userSelectedIndex = -1;
    // Cancel 時に checkBoxSamples.Checked を戻す際の CheckedChanged 再入防止ガード
    private bool _suppressSamplesToggleEvent = false;

    #endregion

    [System.ComponentModel.Browsable(false)]
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
    public string[] HelpItems
    {
        set
        {
            var autoCompleteItems = new List<string>();
            var toolTipItems = new List<string>();
            for (int i = 0; i < value.Length; i++)
            {
                string[] temp = value[i].Split('#', true);

                for (int j = 0; j < temp.Length; j++)
                    temp[j] = temp[j].Trim().TrimEnd();

                autoCompleteItems.Add(temp[0]);
                toolTipItems.Add(temp.Length == 2 ? temp[1] : "");

                dataGridView.Rows.Add(temp);
            }
            pyRichTextBox.AutoCompleteItems = [.. autoCompleteItems];
            pyRichTextBox.ToolTipItems = [.. toolTipItems];
        }
    }

    public FormMacro(ScriptEngine engine, object scopeObject)
    {
        InitializeComponent();

        Engine = engine;
        // 260414Cl scopeObject を MacroBase 型へ。3 アプリすべて MacroBase 派生を渡している。
        obj = (MacroBase)scopeObject;

        Scope = Engine.CreateScope();
        ScopeName = obj.ScopeName;
        Scope.SetVariable(ScopeName, scopeObject);
        // 260414Cl 追加 math モジュールを事前 import (ユーザーマクロで math.sqrt 等をそのまま使えるように)
        try { Engine.CreateScriptSourceFromString("import math").Execute(Scope); }
        catch { /* math モジュール未搭載の環境でも起動は続行 */ }
        HelpItems = obj.Help;

        _tracebackDelegate = OnTraceback;

        // 260414Cl 追加 Designer で設定済みのタイトルを基準値として保持 (ダーティ時に "*" を付与する)
        _titleBase = Text;

        // 260414Cl 追加 PyRichTextBox へ行番号ガターとステータスバーを紐付け。
        // 行番号描画・スクロール同期・オートインデント等は PyRichTextBox 側に集約済み。
        pyRichTextBox.AttachGutter(panelGutter);
        pyRichTextBox.SelectionChanged += updateStatusPos;
        updateStatusPos(null, null);

        // 260415Cl 削除 Shown 時の自動サンプル挿入を撤去 (旧: this.Shown += loadSampleMacrosIfEmpty;)
        // 理由: 初回起動時の言語でユーザーのマクロリストに英語サンプルが永続保存され、
        // 後から日本語 UI に切り替えても英語のまま残ってしまう問題があったため。
        // 代替: Designer 配置の checkBoxSamples から常時・現行言語で参照できる。

        // 260415Cl 追加 派生クラスが SampleMacros を上書きしていなければチェックボックスは非表示
        var _samples = obj?.SampleMacros;
        if (_samples == null || _samples.Length == 0)
            checkBoxSamples.Visible = false;

        splitContainer2.SplitterDistance = splitContainer2.Width;
    }

    // 260415Cl 追加 listBoxMacro を items で置き換え、selectIdx を選択してエディタへ反映する共通処理。
    // skipEvent ガード + BeginUpdate/EndUpdate で SelectedIndexChanged / TextChanged / 再描画を抑止する。
    private void replaceListBoxAndSelect(MacroEntry[] items, int selectIdx)
    {
        skipEvent = true;
        try
        {
            listBoxMacro.BeginUpdate();
            try
            {
                listBoxMacro.Items.Clear();
                foreach (var m in items)
                    listBoxMacro.Items.Add(m);
            }
            finally { listBoxMacro.EndUpdate(); }

            int idx = selectIdx >= 0 && selectIdx < listBoxMacro.Items.Count ? selectIdx
                    : listBoxMacro.Items.Count > 0 ? 0 : -1;
            if (idx >= 0)
            {
                listBoxMacro.SelectedIndex = idx;
                var v = (MacroEntry)listBoxMacro.SelectedItem;
                textBoxMacroName.Text = v.Name;
                pyRichTextBox.Text = v.Body;
            }
            else
            {
                textBoxMacroName.Text = "";
                pyRichTextBox.Text = "";
            }
        }
        finally { skipEvent = false; }
    }

    // 260415Cl 改修 checkBoxSamples.CheckedChanged でサンプル表示とユーザーマクロをトグル切り替え (旧: samplesToolStripMenuItem.Click)
    private void toggleSamplesMode(object sender, EventArgs e)
    {
        if (_suppressSamplesToggleEvent) return;

        if (checkBoxSamples.Checked)
        {
            // 未保存変更の確認
            if (_isDirty && _previousSelectedIndex >= 0 && _previousSelectedIndex < listBoxMacro.Items.Count)
            {
                var dr = MessageBox.Show(
                    "Save changes to the current macro?",
                    "Unsaved changes",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);
                if (dr == DialogResult.Cancel || (dr == DialogResult.Yes && !saveCurrentToIndex(_previousSelectedIndex)))
                {
                    // Cancel / 保存失敗 → checkBoxSamples を元に戻す (CheckedChanged 再入は _suppress で無効化)
                    _suppressSamplesToggleEvent = true;
                    try { checkBoxSamples.Checked = false; }
                    finally { _suppressSamplesToggleEvent = false; }
                    return;
                }
            }

            // 現在のユーザーマクロを退避してサンプルを読み込む
            _userMacroSnapshot = listBoxMacro.Items.Cast<MacroEntry>().ToArray();
            _userSelectedIndex = listBoxMacro.SelectedIndex;

            var samples = obj.SampleMacros;
            var sampleEntries = samples == null ? []
                : Array.ConvertAll(samples, s => new MacroEntry(s.name, s.body));
            replaceListBoxAndSelect(sampleEntries, 0);
            setDirty(false);
            pyRichTextBox.ReadOnly = textBoxMacroName.ReadOnly = true;
        }
        else
        {
            // ユーザーマクロへ復元
            replaceListBoxAndSelect(_userMacroSnapshot, _userSelectedIndex);
            setDirty(false);
            pyRichTextBox.ReadOnly = textBoxMacroName.ReadOnly = false;
            _previousSelectedIndex = listBoxMacro.SelectedIndex;
            setMenuItemOfMain();
        }
        // 260415Cl 両分岐共通の再描画 (updateListButtons が checkBoxSamples.Checked を見て編集系を一括制御)
        updateListButtons();
    }

    private void FormMacro_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        this.Visible = false;
    }

    // 260414Cl NoRichTextChange / SendMessage P/Invoke / IMF_DUALFONT 等の定数を削除。
    // 元コードはどこからも呼ばれていない死コードだった。

    private IronPython.Runtime.Exceptions.TracebackDelegate OnTraceback(IronPython.Runtime.Exceptions.TraceBackFrame frame, string result, object payload)
    {
        if (stepByStepMode)
            setDebugInfo(frame, result);

        // スクリプトは UI スレッド上で同期実行されているため、Wait + Sleep だけだと
        // UI が固まってボタンが押せない。Application.DoEvents で短いポンプを行いつつ
        // 50ms ごとにキャンセル要求をチェックする。260414Cl Cancel 要求時は
        // OperationCanceledException を握り潰さず投げ上げ、IronPython の Execute から
        // そのまま RunMacro へ伝播させることで、次の行に進む前にスクリプトを中断する。
        while (stepByStepMode && nextStepFlag == false)
        {
            _cancelSource?.Token.ThrowIfCancellationRequested();
            Application.DoEvents();
            Thread.Sleep(50);
        }
        nextStepFlag = false;
        return _tracebackDelegate;
    }

    private void setDebugInfo(IronPython.Runtime.Exceptions.TraceBackFrame frame, string result)
    {
        if (this.InvokeRequired)
        {
            // 260414Cl 旧: setDebugInfoCallBack delegate を経由していた。
            this.Invoke((Action)(() => setDebugInfo(frame, result)));
            return;
        }
        if (!stepByStepMode)
            return;

        this.Focus();
        int i = (int)frame.f_lineno;
        dataGridViewDebug.Rows.Clear();
        if (i > 0 && result != "exception")
        {
            pyRichTextBox.HideSelection = false;
            // 260414Cl 旧: TextLines[j].Length+1 を手で総和して O(n) 計算 + Split 配列再生成。
            // GetFirstCharIndexFromLine / Lines に置き換え (highlightErrorLine と同じ作法)。
            pyRichTextBox.SelectionStart = pyRichTextBox.GetFirstCharIndexFromLine(i - 1);
            pyRichTextBox.SelectionLength = pyRichTextBox.Lines[i - 1].Length;

            foreach (object o in (IronPython.Runtime.PythonDictionary)frame.f_locals)
            {
                try
                {
                    var kv = (KeyValuePair<object, object>)o;
                    var key = (string)kv.Key;
                    if (!(key.StartsWith("__") && key.EndsWith("__")) && key != ScopeName)
                    {
                        var value = kv.Value.ToString();
                        if (kv.Value is int[] v && v.Length != 0)
                        {
                            value = "";
                            foreach (int n in v)
                                value += n + ", ";
                        }
                        dataGridViewDebug.Rows.Add([key, value]);
                    }
                }
                catch { }
            }
        }
    }

    private bool nextStepFlag = false;

    private void buttonNextStep_Click(object sender, EventArgs e) => nextStepFlag = true;

    private void buttonCancelStep_Click(object sender, EventArgs e)
    {
        // 260414Cl 旧: task != null && task.Status == TaskStatus.Running を見ていた。
        // Task を撤去したので _cancelSource の有無だけで判定する。
        // 260414Cl Step モード限定でキャンセル可能。OnTraceback で次行実行前に
        // ThrowIfCancellationRequested が発火し、OperationCanceledException が
        // Execute から RunMacro まで伝播してスクリプトが中断される。
        _cancelSource?.Cancel();
    }

    private void buttonRunMacro_Click(object sender, EventArgs e)
    {
        stepByStepMode = false;

        buttonStepByStep.Visible = buttonRunMacro.Visible = false;
        RunMacro(pyRichTextBox.Text);
        buttonCancelStep.Visible = false;
        buttonStepByStep.Visible = buttonRunMacro.Visible = true;
    }

    private void buttonStepByStep_Click(object sender, EventArgs e)
    {
        stepByStepMode = true;

        // 260414Cl 追加 buttonCancelStep を Step 実行中のみ可視化
        // 260414Cl 旧: try/catch (Exception) で囲んでいたが RunMacro 内で全例外を捕捉する
        // 構造に変えたので不到達。削除。
        buttonCancelStep.Visible = buttonNextStep.Visible = true;
        buttonStepByStep.Visible = buttonRunMacro.Visible = false;
        RunMacro(pyRichTextBox.Text);
        buttonCancelStep.Visible = buttonNextStep.Visible = false;
        buttonStepByStep.Visible = buttonRunMacro.Visible = true;
    }

    public void SelectMacro(string macroName)
    {
        var match = listBoxMacro.Items.Cast<MacroEntry>().FirstOrDefault(m => m.Name == macroName);
        if (match.Name != null)
            listBoxMacro.SelectedIndex = listBoxMacro.Items.IndexOf(match);
    }

    public void RunMacroName(string macroName, bool _stepByStepMode = false)
    {
        stepByStepMode = _stepByStepMode;
        var match = listBoxMacro.Items.Cast<MacroEntry>().FirstOrDefault(m => m.Name == macroName);
        if (match.Name == null)
        {
            MessageBox.Show("The macro name is not found");
            return;
        }
        RunMacro(match.Body);
    }

    public void RunMacro() => RunMacro(pyRichTextBox.Text);
    public void RunMacro(bool _stepByStepMode)
    {
        stepByStepMode = _stepByStepMode;
        RunMacro(pyRichTextBox.Text);
    }
    public void RunMacro(string srcCode)
    {
        // 260414Cl 旧: Compile() を 2 回呼んでいたが ErrorListener 方式に差し替え。
        // IronPython は文法エラー時に例外を投げず黙って null を返すケースがあるため、
        // ErrorListener でエラーを収集し、行番号付きでまとめて表示する。
        var source = Engine.CreateScriptSourceFromString(srcCode);
        var errorListener = new CollectingErrorListener();
        Microsoft.Scripting.Hosting.CompiledCode compiled = null;
        try { compiled = source.Compile(errorListener); }
        catch (Microsoft.Scripting.SyntaxErrorException ex)
        {
            // ErrorListener で拾えなかったフォールバック
            errorListener.Errors.Add($"Line {ex.Line}: {ex.Message}");
        }

        if (compiled == null || errorListener.Errors.Count > 0)
        {
            var msg = errorListener.Errors.Count > 0
                ? string.Join(Environment.NewLine, errorListener.Errors)
                : "Syntax error (no details available)";
            MessageBox.Show(msg, "Syntax error");
            return;
        }

        try
        {
            dataGridViewDebug.Rows.Clear();

            if (stepByStepMode)
            {
                splitContainer2.SplitterDistance = splitContainer2.Width - 220;
                IronPython.Hosting.Python.SetTrace(Engine, _tracebackDelegate);
            }

            _cancelSource = new CancellationTokenSource();
            // 260414Cl 旧: new Task(...).RunSynchronously() で同期実行していたが、
            // RunSynchronously は呼び出しスレッド (UI スレッド) で動くだけで Task の
            // 意味が無く、CancellationToken も IronPython 側で尊重されないので撤去。
            compiled.Execute(Scope);
        }
        // 260414Cl Step 実行の Cancel による中断は通知不要 (ユーザー操作)
        catch (OperationCanceledException) { }
        // 260414Cl ArgumentTypeException / MissingMemberException / 一般例外を統合。
        // ExceptionOperations.FormatException は Python スタイルのトレースバック
        // (行番号・呼び出しチェーン付き) を返すので "素の ex.Message" より情報量が多い。
        catch (Exception ex)
        {
            var ops = Engine.GetService<Microsoft.Scripting.Hosting.ExceptionOperations>();
            var msg = ops.FormatException(ex);
            pyRichTextBox.HighlightErrorLineFromTraceback(msg); // 260414Cl 該当行を選択状態にしてから表示
            MessageBox.Show(msg, "Runtime error");
        }
        splitContainer2.SplitterDistance = splitContainer2.Width;
    }

    // 260414Cl 追加 Compile 時の文法エラーを行番号付きで収集する ErrorListener
    private sealed class CollectingErrorListener : Microsoft.Scripting.Hosting.ErrorListener
    {
        public readonly List<string> Errors = [];
        public override void ErrorReported(
            Microsoft.Scripting.Hosting.ScriptSource source,
            string message,
            Microsoft.Scripting.SourceSpan span,
            int errorCode,
            Microsoft.Scripting.Severity severity)
        {
            if (severity == Microsoft.Scripting.Severity.Warning || severity == Microsoft.Scripting.Severity.Ignore)
                return;
            var line = span.Start.Line;
            Errors.Add(line > 0 ? $"Line {line}: {message}" : message);
        }
    }

    #region マクロファイルを読み込み・書き込み
    private void FormMacro_DragDrop(object sender, DragEventArgs e)
    {
        // 260414Cl null チェックを追加 (旧: 直接キャストで NRE 可能性)。
        if (e.Data.GetData(DataFormats.FileDrop) is string[] files
            && files.Length == 1
            && files[0].EndsWith(".mcr"))
        {
            ReadMacroFile(files[0]);
        }
    }

    private void FormMacro_DragEnter(object sender, DragEventArgs e)
        => e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;

    private void readToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var dlg = new OpenFileDialog { Filter = "*.mcr|*.mcr" };
        if (dlg.ShowDialog() == DialogResult.OK)
            ReadMacroFile(dlg.FileName);
    }

    public void ReadMacroFile(string filename)
    {
        // 260414Cl 旧: StreamReader を手で Close。File.ReadLines は遅延読み込みで
        // \r\n / \n 両方の改行を吸収するので、元の StreamReader.ReadLine と同等。
        // 260414Cl テキスト一括ロード中は TextChanged によるダーティ更新を抑止する
        skipEvent = true;
        try
        {
            pyRichTextBox.Text = "";
            foreach (var line in File.ReadLines(filename, Encoding.UTF8))
                pyRichTextBox.AppendText(line + "\n");
            textBoxMacroName.Text = Path.GetFileNameWithoutExtension(filename);
        }
        finally { skipEvent = false; }
        buttonAddMacro_Click(new object(), new EventArgs());
    }

    private void saveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var dlg = new SaveFileDialog
        {
            Filter = "*.mcr|*.mcr",
            FileName = textBoxMacroName.Text + ".mcr"
        };
        if (dlg.ShowDialog() != DialogResult.OK)
            return;
        // 260414Cl 旧: StreamWriter を手で Close していた。
        File.WriteAllLines(dlg.FileName, pyRichTextBox.TextLines, Encoding.UTF8);
    }
    #endregion

    private void dataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            string str = (string)dataGridView.Rows[e.RowIndex].Cells[0].Value;

            int selectionStart = pyRichTextBox.SelectionStart;
            pyRichTextBox.Text = pyRichTextBox.Text.Remove(selectionStart, pyRichTextBox.SelectionLength);
            pyRichTextBox.Text = pyRichTextBox.Text.Insert(selectionStart, str);
        }
    }

    // 260414Cl 削除: saveAsMenuItemToolStripMenuItem_Click / readFromMenuItemToolStripMenuItem_Click
    // どちらも Designer から配線されていない死コード。obj.SaveToMenuItem / obj.ReadFromMenuItem も
    // どのリポジトリにも実装が無く、呼ぶと RuntimeBinderException で必ず落ちる潜在バグだった。

    // 260414Cl ステータスバー (Line/Col) 表示 — PyRichTextBox.GetLineColumn() に委譲
    private void updateStatusPos(object sender, EventArgs e)
    {
        var (line, col) = pyRichTextBox.GetLineColumn();
        statusLabelPos.Text = $"Line {line}, Col {col}";
    }

    #region ダーティ状態管理 260414Cl 追加
    // 260414Cl 追加 ダーティフラグを更新しタイトルバーに "*" を反映する
    private void setDirty(bool dirty)
    {
        if (_isDirty == dirty) return;
        _isDirty = dirty;
        this.Text = dirty ? _titleBase + "*" : _titleBase;
    }

    // 260414Cl 追加 現在のテキストボックス内容を指定インデックスへ書き戻す (Replace 相当)
    private bool saveCurrentToIndex(int index)
    {
        if (index < 0 || index >= listBoxMacro.Items.Count) return false;
        if (textBoxMacroName.Text.Length == 0)
        {
            MessageBox.Show("Please input macro name", "Alert");
            return false;
        }
        // 260415Cl Items[index] への代入は ListBox 内部で SelectedIndexChanged を発火させるため、
        // skipEvent で再入ガードし、事前に dirty を解除して未保存確認ダイアログの多重表示を防ぐ。
        setDirty(false);
        skipEvent = true;
        try { listBoxMacro.Items[index] = new MacroEntry(textBoxMacroName.Text, pyRichTextBox.Text); }
        finally { skipEvent = false; }
        _previousSelectedIndex = listBoxMacro.SelectedIndex;
        setMenuItemOfMain();
        return true;
    }

    // 260414Cl 追加 pyRichTextBox / textBoxMacroName の TextChanged から呼ばれる共通ハンドラ。
    // 編集結果が選択項目と完全一致に戻ったら自動でクリーン扱いに戻す (revert-to-clean)。
    // RichTextBox.Text 全文の比較を伴うが、マクロは数十行程度なので実測で問題無し。
    private void markDirtyFromEdit(object sender, EventArgs e)
    {
        if (skipEvent) return;
        if (listBoxMacro.SelectedIndex >= 0)
        {
            var cur = (MacroEntry)listBoxMacro.SelectedItem;
            if (cur.Name == textBoxMacroName.Text && cur.Body == pyRichTextBox.Text)
            {
                setDirty(false);
                return;
            }
        }
        setDirty(true);
    }
    #endregion

    #region リストボックス操作
    private void buttonAddMacro_Click(object sender, EventArgs e)
    {
        var m = new MacroEntry(textBoxMacroName.Text, pyRichTextBox.Text);
        if (m.Name.Length == 0)
        {
            MessageBox.Show("Please input macro name", "Alert");
            return;
        }
        // 260415Cl 旧: Any + First + IndexOf の三重走査 → 単一ループで一発検索に簡素化
        var items = listBoxMacro.Items;
        int existing = -1;
        for (int i = 0; i < items.Count; i++)
            if (((MacroEntry)items[i]).Name == m.Name) { existing = i; break; }
        if (existing >= 0 && MessageBox.Show("The name already exists. Do you replace the macro?", "Alert", MessageBoxButtons.YesNo) != DialogResult.Yes)
            return;

        // 260415Cl Items[i]=x / Items.Add は SelectedIndexChanged を発火させ得るため、
        // dirty を先に解除 + skipEvent ガードで未保存確認ダイアログの多重表示を防ぐ。
        setDirty(false);
        skipEvent = true;
        try
        {
            if (existing >= 0) items[existing] = m;
            else items.Add(m);
        }
        finally { skipEvent = false; }
        setMenuItemOfMain();
        _previousSelectedIndex = listBoxMacro.SelectedIndex;
    }

    private void buttonChangeMacro_Click(object sender, EventArgs e)
    {
        // 260414Cl 旧: Items[...] への直接代入 + setMenuItemOfMain() をインライン展開していたが、
        // saveCurrentToIndex に集約 (名前未入力チェックも共通化)。
        // 260415Cl saveCurrentToIndex が setDirty / _previousSelectedIndex 更新を内包するため、呼び出し側での重複処理を削除。
        saveCurrentToIndex(listBoxMacro.SelectedIndex);
    }

    private void buttonDeleteMacro_Click(object sender, EventArgs e)
    {
        int n = listBoxMacro.SelectedIndex;
        if (n >= 0)
        {
            // 260414Cl 追加 削除操作で SelectedIndexChanged が走るので、事前にダーティを
            // クリアして確認ダイアログが出ないようにする (削除時は編集内容は捨てる前提)
            setDirty(false);
            _previousSelectedIndex = -1;
            listBoxMacro.Items.RemoveAt(n);
            if (n < listBoxMacro.Items.Count)
                listBoxMacro.SelectedIndex = n;
            else if (n - 1 < listBoxMacro.Items.Count)
                listBoxMacro.SelectedIndex = n - 1;
        }
    }

    private void listBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        // 260414Cl 旧ロジックは未保存編集を黙って上書きしていたため、
        // Yes/No/Cancel 確認ダイアログを挟む新ロジックに差し替え。
        //buttonChange.Enabled = buttonDeleteProfile.Enabled = listBoxMacro.SelectedIndex >= 0;
        //buttonLower.Enabled = listBoxMacro.SelectedIndex >= 0 && listBoxMacro.SelectedIndex < listBoxMacro.Items.Count - 1;
        //buttonUpper.Enabled = listBoxMacro.SelectedIndex >= 1;
        //if (listBoxMacro.SelectedIndex < 0)
        //    return;
        //var value = (MacroEntry)listBoxMacro.SelectedItem;
        //if (textBoxMacroName.Text != value.Name)
        //    textBoxMacroName.Text = value.Name;
        //if (pyRichTextBox.Text != value.Body)
        //    pyRichTextBox.Text = value.Body;
        //setMenuItemOfMain();

        // 260414Cl 追加 プログラマティックな選択変更 (Cancel 時の戻し等) は素通しする
        if (skipEvent)
        {
            if (listBoxMacro.SelectedIndex >= 0)
            {
                var v = (MacroEntry)listBoxMacro.SelectedItem;
                textBoxMacroName.Text = v.Name;
                pyRichTextBox.Text = v.Body;
            }
            _previousSelectedIndex = listBoxMacro.SelectedIndex;
            updateListButtons();
            return;
        }

        // 260415Cl 追加 サンプル表示モードではダーティチェック不要・読み取り専用で表示するだけ
        if (checkBoxSamples.Checked)
        {
            if (listBoxMacro.SelectedIndex >= 0)
            {
                var v = (MacroEntry)listBoxMacro.SelectedItem;
                skipEvent = true;
                try { textBoxMacroName.Text = v.Name; pyRichTextBox.Text = v.Body; }
                finally { skipEvent = false; }
            }
            return;
        }

        // 260414Cl 追加 未保存編集がある場合は確認ダイアログ
        if (_isDirty && _previousSelectedIndex >= 0 && _previousSelectedIndex < listBoxMacro.Items.Count)
        {
            var dr = MessageBox.Show(
                "Save changes to the current macro?",
                "Unsaved changes",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning);
            // Yes: 旧選択に書き戻して続行。保存失敗 (名前空) は Cancel 扱いで巻き戻し
            // No:  破棄してそのまま新選択を読み込む
            // Cancel: 新選択を無視して旧選択に戻す
            if (dr == DialogResult.Cancel || (dr == DialogResult.Yes && !saveCurrentToIndex(_previousSelectedIndex)))
            {
                skipEvent = true;
                try { listBoxMacro.SelectedIndex = _previousSelectedIndex; }
                finally { skipEvent = false; }
                return;
            }
        }

        if (listBoxMacro.SelectedIndex >= 0)
        {
            var value = (MacroEntry)listBoxMacro.SelectedItem;
            skipEvent = true;
            try
            {
                if (textBoxMacroName.Text != value.Name)
                    textBoxMacroName.Text = value.Name;
                if (pyRichTextBox.Text != value.Body)
                    pyRichTextBox.Text = value.Body;
            }
            finally { skipEvent = false; }
        }
        setDirty(false);
        _previousSelectedIndex = listBoxMacro.SelectedIndex;
        updateListButtons();
        setMenuItemOfMain();
    }

    // 260414Cl 追加 選択状態に応じてボタンの Enabled を設定 (旧 listBox_SelectedIndexChanged から抽出)
    // 260415Cl サンプル表示モードでは一括で編集系を無効化 (旧: toggleSamplesMode 側で手動 disable)
    private void updateListButtons()
    {
        bool editable = !checkBoxSamples.Checked;
        int idx = listBoxMacro.SelectedIndex;
        buttonAdd.Enabled = editable;
        buttonChange.Enabled = buttonDeleteProfile.Enabled = editable && idx >= 0;
        buttonLower.Enabled = editable && idx >= 0 && idx < listBoxMacro.Items.Count - 1;
        buttonUpper.Enabled = editable && idx >= 1;
    }

    private void buttonUpper_Click(object sender, EventArgs e)
    {
        int n = listBoxMacro.SelectedIndex;
        if (n < 1) return;
        var item = listBoxMacro.SelectedItem;
        listBoxMacro.Items.RemoveAt(n);
        listBoxMacro.Items.Insert(n - 1, item);
        listBoxMacro.SelectedIndex = n - 1;
        setMenuItemOfMain();
    }

    private void buttonLower_Click(object sender, EventArgs e)
    {
        int n = listBoxMacro.SelectedIndex;
        if (n < 0 || n >= listBoxMacro.Items.Count - 1) return;
        var item = listBoxMacro.SelectedItem;
        listBoxMacro.Items.RemoveAt(n);
        listBoxMacro.Items.Insert(n + 1, item);
        listBoxMacro.SelectedIndex = n + 1;
        setMenuItemOfMain();
    }

    #endregion

    #region KeyDownイベント
    private void FormMacro_KeyDown(object sender, KeyEventArgs e)
    {
        // 260414Cl 旧: e.Modifiers == Keys.Control & e.KeyCode == Keys.S (ビット AND)
        if (e.Modifiers == Keys.Control && e.KeyCode == Keys.S)
        {
            // 260414Cl 名前一致条件を維持しつつ saveCurrentToIndex に委譲 (重複削除)
            // 260415Cl saveCurrentToIndex が setDirty / _previousSelectedIndex を内包するため後処理を削除。
            if (listBoxMacro.SelectedIndex >= 0
                && textBoxMacroName.Text == ((MacroEntry)listBoxMacro.SelectedItem).Name)
                saveCurrentToIndex(listBoxMacro.SelectedIndex);
        }
        // F10 次のステップに進む
        if (e.KeyCode == Keys.F10 && buttonNextStep.Visible)
            buttonNextStep_Click(sender, new EventArgs());
    }
    #endregion

    // 260415Cl 簡素化 旧: List<string> + for ループ
    private void setMenuItemOfMain()
        => obj.SetMacroToMenu([.. listBoxMacro.Items.Cast<MacroEntry>().Select(m => m.Name)]);

    public void SetMacroList(KeyValuePair<string, string>[] list)
    {
        // 260414Cl 一括ロード中は SelectedIndexChanged / TextChanged による確認ダイアログを抑止
        skipEvent = true;
        try
        {
            listBoxMacro.Items.Clear();
            for (int i = 0; i < list.Length; i++)
                listBoxMacro.Items.Add(new MacroEntry(list[i].Key, list[i].Value));
        }
        finally { skipEvent = false; }
        setDirty(false);
        _previousSelectedIndex = listBoxMacro.SelectedIndex;
    }

    [System.ComponentModel.Browsable(false)]
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
    public byte[] ZippedMacros
    {
        get
        {
            // 260415Cl サンプル表示中は listBoxMacro にサンプルが入っているため、退避済みユーザーマクロを参照する。
            // これを怠るとプログラム終了時にサンプルがユーザー保存領域 (レジストリ) に書き込まれてしまう。
            var entries = checkBoxSamples.Checked
                ? _userMacroSnapshot
                : listBoxMacro.Items.Cast<MacroEntry>().ToArray();

            var strList = new List<string>();
            foreach (var m in entries)
            {
                strList.Add(m.Name);
                strList.Add(m.Body);
            }

            // 260414Cl using で確実に Dispose。
            using var ms = new MemoryStream();
            using (var ds = new DeflateStream(ms, CompressionMode.Compress, true))
            {
                var serializer = new XmlSerializer(typeof(List<string>));
                serializer.Serialize(ds, strList);
            }
            return ms.ToArray();
        }
        set
        {
            if (value == null || value.Length == 0) return;

            using var ms = new MemoryStream(value);
            using var ds = new DeflateStream(ms, CompressionMode.Decompress, true);
            var serializer = new XmlSerializer(typeof(List<string>));
            var strList = (List<string>)serializer.Deserialize(ds);

            // 260414Cl 一括ロード中は SelectedIndexChanged / TextChanged による確認ダイアログを抑止
            skipEvent = true;
            try
            {
                listBoxMacro.Items.Clear();
                for (int i = 0; i < strList.Count; i += 2)
                    listBoxMacro.Items.Add(new MacroEntry(strList[i], strList[i + 1]));
            }
            finally { skipEvent = false; }
            if (listBoxMacro.Items.Count > 0)
                setMenuItemOfMain();
            setDirty(false);
            _previousSelectedIndex = listBoxMacro.SelectedIndex;
        }
    }

    // 260414Cl 旧名 "Macro" を "MacroEntry" に改名 (private struct なので外部影響なし)。
    // public な Macro 派生クラスと紛らわしかったため。
    private struct MacroEntry(string name, string body)
    {
        public string Name = name, Body = body;
        public override readonly string ToString() => Name;
    }
}
