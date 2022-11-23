using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Crystallography.Controls
{
    public partial class FormMacro : Form
    {
        #region フィールド、プロパティ
        private const uint IMF_DUALFONT = 0x80;// //Font抑制の為の
        private const uint WM_USER = 0x400;////Font抑制の為の
        private const uint EM_SETLANGOPTIONS = (WM_USER + 120);//Font抑制の為の
        private const uint EM_GETLANGOPTIONS = (WM_USER + 121); //Font抑制の為の
        //private Thread t;
        private Task task;
        private CancellationTokenSource _cancelSource;
        public bool stepByStepMode;

        private readonly dynamic obj;
        private readonly ScriptEngine Engine;
        private readonly ScriptScope Scope;
        private readonly string ScopeName;

        #endregion

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
                    if (temp.Length == 2)
                        toolTipItems.Add(temp[1]);
                    else
                        toolTipItems.Add("");

                    dataGridView.Rows.Add(temp);
                }
                exRichTextBox.AutoCompleteItems = autoCompleteItems.ToArray();
                exRichTextBox.ToolTipItems = toolTipItems.ToArray();
            }
        }

        public FormMacro(ScriptEngine engine, object scopeObject)
        {
            InitializeComponent();

            Engine = engine;

            obj = scopeObject;

            Scope = Engine.CreateScope();
            ScopeName = obj.ScopeName;
            Scope.SetVariable(ScopeName, scopeObject);
            HelpItems = obj.Help;

            splitContainer2.SplitterDistance = splitContainer2.Width;
        }

        private void FormMacro_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        [System.Runtime.InteropServices.DllImport("USER32.dll")]
        private static extern uint SendMessage(System.IntPtr hWnd, uint msg, uint wParam, uint lParam);

        /// <summary>
        /// リッチエディットボックスのフォントが勝手に変わるのを抑制する
        /// </summary>
        /// <param name="RichTextBoxCtrl">フォントが勝手に変わるのを抑制するリッチテキストボックス</param>
        private static void NoRichTextChange(RichTextBox RichTextBoxCtrl)
        {
            uint lParam;
            lParam = SendMessage(RichTextBoxCtrl.Handle, EM_GETLANGOPTIONS, 0, 0);
            lParam &= ~IMF_DUALFONT;
            SendMessage(RichTextBoxCtrl.Handle, EM_SETLANGOPTIONS, 0, lParam);
        }

        private IronPython.Runtime.Exceptions.TracebackDelegate OnTraceback(IronPython.Runtime.Exceptions.TraceBackFrame frame, string result, object payload)
        {
            if (stepByStepMode)
                setDebugInfo(frame, result);

            while (stepByStepMode && nextStepFlag == false)
            {
                _cancelSource.Token.ThrowIfCancellationRequested();
                try { Application.DoEvents(); }
                catch { }
                Thread.Sleep(50);
            }
            nextStepFlag = false;
            return this.OnTraceback;
        }

        private delegate void setDebugInfoCallBack(IronPython.Runtime.Exceptions.TraceBackFrame frame, string result);

        private void setDebugInfo(IronPython.Runtime.Exceptions.TraceBackFrame frame, string result)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new setDebugInfoCallBack(setDebugInfo), frame, result);
                return;
            }
            if (!stepByStepMode)
                return;

            this.Focus();
            int i = (int)frame.f_lineno;
            dataGridViewDebug.Rows.Clear();
            if (i > 0 && result != "exception")
            {
                exRichTextBox.HideSelection = false;
                int start = 0;
                for (int j = 0; j < i - 1; j++)
                    start += exRichTextBox.TextLines[j].Length + 1;
                exRichTextBox.SelectionStart = start;
                exRichTextBox.SelectionLength = exRichTextBox.TextLines[i - 1].Length;

                foreach (object o in (IronPython.Runtime.PythonDictionary)frame.f_locals)
                {
                    try
                    {
                        var kv = (KeyValuePair<object, object>)o;
                        var key = (string)kv.Key;
                        if (!(key.StartsWith("__") && key.EndsWith("__")) && key != ScopeName)
                        {
                            var value = kv.Value.ToString();
                            if (kv.Value is int[] v)
                            {
                                if (v.Length != 0)
                                {
                                    value = "";
                                    foreach (int n in v)
                                        value += n + ", ";
                                }
                            }
                            dataGridViewDebug.Rows.Add(new[] { key, value });
                        }
                    }
                    catch { }
                }
            }
        }

        private bool nextStepFlag = false;

        private void buttonNextStep_Click(object sender, EventArgs e)
        {
            nextStepFlag = true;
        }

        private void buttonCancelStep_Click(object sender, EventArgs e)
        {
            if (task != null && task.Status == TaskStatus.Running)
                _cancelSource.Cancel(true);
        }

        private void buttonRunMacro_Click(object sender, EventArgs e)
        {
            stepByStepMode = false;

            buttonCancelStep.Visible = true;
            buttonStepByStep.Visible = buttonRunMacro.Visible = false;
            RunMacro(exRichTextBox.Text);
            buttonCancelStep.Visible = false;
            buttonStepByStep.Visible = buttonRunMacro.Visible = true;
        }

        private void buttonStepByStep_Click(object sender, EventArgs e)
        {
            stepByStepMode = true;

            buttonCancelStep.Visible = buttonNextStep.Visible = true;
            buttonStepByStep.Visible = buttonRunMacro.Visible = false;
            try { RunMacro(exRichTextBox.Text); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            buttonCancelStep.Visible = buttonNextStep.Visible = false;
            buttonStepByStep.Visible = buttonRunMacro.Visible = true;
        }

        public void SelectMacro(string macroName)
        {
            if (listBoxMacro.Items.Cast<macro>().Any(m => m.Name == macroName))
                listBoxMacro.SelectedIndex = listBoxMacro.Items.IndexOf(listBoxMacro.Items.Cast<macro>().First(m => m.Name == macroName));
        }

        public void RunMacroName(string macroName, bool _stepByStepMode = false)
        {
            stepByStepMode = _stepByStepMode;
            if (!listBoxMacro.Items.Cast<macro>().Any(m => m.Name == macroName))
            {
                MessageBox.Show("The macro name is not found");
                return;
            }
            RunMacro(listBoxMacro.Items.Cast<macro>().First(m => m.Name == macroName).Body);
        }

        public void RunMacro() => RunMacro(exRichTextBox.Text);
        public void RunMacro(bool _stepByStepMode) 
        {
            stepByStepMode = _stepByStepMode;
            RunMacro(exRichTextBox.Text);
        }
        public void RunMacro(string srcCode)
        {
            try
            {
                var a = Engine.CreateScriptSourceFromString(srcCode).Compile();

                dataGridViewDebug.Rows.Clear();

                if (stepByStepMode)
                    splitContainer2.SplitterDistance = splitContainer2.Width - 220;
                IronPython.Hosting.Python.SetTrace(Engine, this.OnTraceback);

                void thread()
                {
                    try { Engine.CreateScriptSourceFromString(srcCode).Execute(Scope); }
                    catch { }
                }

                _cancelSource = new CancellationTokenSource();
                task = new Task(thread, _cancelSource.Token);

                task.RunSynchronously();
            }
            catch (Microsoft.Scripting.ArgumentTypeException ex) { MessageBox.Show(ex.Message); }
            catch (Microsoft.Scripting.SyntaxErrorException ex) { MessageBox.Show("Syntax error in line " +ex.Line.ToString()); }
            catch (MissingMemberException ex) { MessageBox.Show(ex.Message); }
            catch (Exception e) { MessageBox.Show(e.Message); }
            splitContainer2.SplitterDistance = splitContainer2.Width;
        }


        #region マクロファイルを読み込み・書き込み
        private void FormMacro_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileName = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (fileName.Length == 1 && fileName[0].EndsWith(".mcr"))
                readMacroFile(fileName[0]);
        }

        private void FormMacro_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy; //ドラッグされたデータ形式を調べ、ファイルのときはコピーとする
            else

                e.Effect = DragDropEffects.None;//ファイル以外は受け付けない
        }

        private void readToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog { Filter = "*.mcr|*.mcr" };
            if (dlg.ShowDialog() == DialogResult.OK)
                readMacroFile(dlg.FileName);
        }

        private void readMacroFile (string filename)
        {
            exRichTextBox.Text = "";
            var reader = new StreamReader(filename, Encoding.GetEncoding("UTF-8"));
            string tempstr;
            while ((tempstr = reader.ReadLine()) != null)
                exRichTextBox.AppendText(tempstr + "\n");
            textBoxMacroName.Text = Path.GetFileNameWithoutExtension(filename);
            reader.Close();
            buttonAddMacro_Click(new object(), new EventArgs());

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new SaveFileDialog
            {
                Filter = "*.mcr|*.mcr",
                FileName = textBoxMacroName.Text + ".mcr"
            };
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var writer = new StreamWriter(dlg.FileName, false, Encoding.GetEncoding("UTF-8"));
                for (int i = 0; i < exRichTextBox.TextLines.Length; i++)
                    writer.WriteLine(exRichTextBox.TextLines[i]);
                writer.Close();
            }
        }
        #endregion

        private void dataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string str = (string)dataGridView.Rows[e.RowIndex].Cells[0].Value;

                int selectionStart = exRichTextBox.SelectionStart;
                exRichTextBox.Text = exRichTextBox.Text.Remove(selectionStart, exRichTextBox.SelectionLength);
                exRichTextBox.Text = exRichTextBox.Text.Insert(selectionStart, str);
            }
        }

        private void saveAsMenuItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            obj.SaveToMenuItem(textBoxMacroName.Text, exRichTextBox.Text);
        }

        private void readFromMenuItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            obj.ReadFromMenuItem();
        }

        #region リストボックス操作
        private void buttonAddMacro_Click(object sender, EventArgs e)
        {
            var m = new macro(textBoxMacroName.Text, exRichTextBox.Text);
            var items = listBoxMacro.Items;
            if (m.Name.Length == 0)
            {
                MessageBox.Show("Please input macro name", "Alert");
                return;
            }
            else if (listBoxMacro.Items.Cast<macro>().Any(o => o.Name == m.Name))
            {
                if (MessageBox.Show("The name already exists. Do you replace the macro?", "Alert", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    items[items.IndexOf(items.Cast<macro>().First(item => item.Name == m.Name))] = m;
                setMenuItemOfMain();
            }
            else
            {
                items.Add(m);
                setMenuItemOfMain();
            }
        }

        private void buttonChangeMacro_Click(object sender, EventArgs e)
        {
            if (listBoxMacro.SelectedIndex >= 0)
            {
                listBoxMacro.Items[listBoxMacro.SelectedIndex] = new macro(textBoxMacroName.Text, exRichTextBox.Text);
                setMenuItemOfMain();
            }
        }

        private void buttonDeleteMacro_Click(object sender, EventArgs e)
        {
            int n = listBoxMacro.SelectedIndex;
            if (n >= 0)
            {
                listBoxMacro.Items.RemoveAt(n);
                if (n < listBoxMacro.Items.Count)
                    listBoxMacro.SelectedIndex = n;
                else if (n - 1 < listBoxMacro.Items.Count)
                    listBoxMacro.SelectedIndex = n - 1;
            }
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonChange.Enabled = buttonDeleteProfile.Enabled = listBoxMacro.SelectedIndex >= 0;

            buttonLower.Enabled = listBoxMacro.SelectedIndex >= 0 && listBoxMacro.SelectedIndex < listBoxMacro.Items.Count - 1;
            buttonUpper.Enabled = listBoxMacro.SelectedIndex >= 1;

            if (listBoxMacro.SelectedIndex < 0)
                return;

            var value = (macro)listBoxMacro.SelectedItem;
            if (textBoxMacroName.Text != value.Name)
                textBoxMacroName.Text = value.Name;
            if (exRichTextBox.Text != value.Body)
                exRichTextBox.Text = value.Body;

            setMenuItemOfMain();
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
            //CTRL+S 上書き
            if (e.Modifiers == Keys.Control & e.KeyCode == Keys.S)
            {
                if (listBoxMacro.SelectedIndex >= 0 && textBoxMacroName.Text == ((macro)listBoxMacro.SelectedItem).Name)
                {
                    listBoxMacro.Items[listBoxMacro.SelectedIndex] = new macro(textBoxMacroName.Text, exRichTextBox.Text);
                }
            }
            //F10 次のステップに進む
            if (e.KeyCode == Keys.F10 && buttonNextStep.Visible)
                buttonNextStep_Click(sender, new EventArgs());
        }
        #endregion

        private void setMenuItemOfMain()
        {
            var list = new List<string>();
            for (int i = 0; i < listBoxMacro.Items.Count; i++)
                list.Add(((macro)listBoxMacro.Items[i]).Name);
            obj.SetMacroToMenu(list.ToArray());
        }

        public void SetMacroList(KeyValuePair<string, string>[] list)
        {
            listBoxMacro.Items.Clear();
            for (int i = 0; i < list.Length; i++)
                listBoxMacro.Items.Add(new macro(list[i].Key, list[i].Value));
        }

        public byte[] ZippedMacros
        {
            get
            {
                var strList = new List<string>();
                for (int i = 0; i < listBoxMacro.Items.Count; i++)
                {
                    var m = (macro)listBoxMacro.Items[i];
                    strList.Add(m.Name);
                    strList.Add(m.Body);
                }

                var ms = new MemoryStream();
                var ds = new DeflateStream(ms, CompressionMode.Compress, true);

                var serializer = new XmlSerializer(typeof(List<string>));
                serializer.Serialize(ds, strList);
                ds.Close();

                var byteArray = ms.ToArray();
                ms.Close();
                return byteArray;
            }
            set
            {
                if (value == null || value.Length == 0) return;

                var ms = new MemoryStream(value);
                var ds = new DeflateStream(ms, CompressionMode.Decompress, true);
                var serializer = new XmlSerializer(typeof(List<string>));
                var strList = (List<string>)serializer.Deserialize(ds);
                ds.Close();
                ms.Close();

                listBoxMacro.Items.Clear();
                for (int i = 0; i < strList.Count; i += 2)
                    listBoxMacro.Items.Add(new macro(strList[i], strList[i + 1]));
                if (listBoxMacro.Items.Count > 0)
                    setMenuItemOfMain();
            }
        }

        private struct macro
        {
            public string Name;
            public string Body;

            public macro(string name, string body)
            {
                Name = name;
                Body = body;
            }

            public override string ToString() => Name;
        }

       
    
    }
}