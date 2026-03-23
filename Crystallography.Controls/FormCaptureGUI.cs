using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Quantization;

namespace Crystallography.Controls;

/// <summary>
/// 260323Cl 追加
/// アプリケーションの全フォーム・コントロールのスクリーンショットを一括保存するための開発者向けフォーム。
/// </summary>
public partial class FormCaptureGUI : Form
{
    /// <summary>キャプチャ対象の最小サイズ (px)</summary>
    private const int MinCaptureSize = 20;

    /// <summary>GroupBox/Panel を丸ごとキャプチャする最小子コントロール数</summary>
    private const int MinChildrenForGroupCapture = 2;

    /// <summary>
    /// 260323Cl 追加
    /// アプリケーション全体で Ctrl+Shift+Alt+C ショートカットを有効にする。
    /// 起動時に一度だけ呼び出すこと。
    /// </summary>
    public static void InstallShortcutFilter()
    {
        Application.AddMessageFilter(new CaptureGUIMessageFilter());
    }

    /// <summary>Ctrl+Shift+Alt+C を検出して FormCaptureGUI を表示するメッセージフィルタ</summary>
    private class CaptureGUIMessageFilter : IMessageFilter
    {
        private const int WM_KEYDOWN = 0x0100;
        private bool handling = false; // 260323Cl: 再入防止フラグ

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg != WM_KEYDOWN) return false;
            if (handling) return false; // 再入防止 (子フォームへの伝播ループ回避)

            var key = (Keys)(int)m.WParam & Keys.KeyCode;
            if (key != Keys.C) return false;

            var modifiers = Control.ModifierKeys;
            if (modifiers != (Keys.Control | Keys.Shift | Keys.Alt)) return false;

            handling = true;
            try
            {
                // 既に開いている場合はアクティブにする
                foreach (Form form in Application.OpenForms)
                {
                    if (form is FormCaptureGUI existing)
                    {
                        existing.Activate();
                        return true; // キーイベントを消費
                    }
                }
                // 260323Cl: ショートカット押下時のアクティブフォームを対象とする
                var activeForm = Form.ActiveForm;
                if (activeForm == null || activeForm is FormCaptureGUI) return true;
                new FormCaptureGUI { targetForm = activeForm }.Show();
            }
            finally
            {
                handling = false;
            }
            return true; // キーイベントを消費 (子フォームに伝播しない)
        }
    }

    // 260323Cl: キャプチャ対象フォーム (ショートカット押下時のアクティブフォーム)
    private Form targetForm;

    public FormCaptureGUI()
    {
        InitializeComponent();
    }

    /// <summary>フォーム表示時にコントロールツリーを構築</summary>
    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        textBoxOutputDir.Text = GetDefaultOutputDir(); // 260323Cl
        BuildTree();
    }

    /// <summary>260323Cl 追加: Select... ボタン</summary>
    private void buttonSelectDir_Click(object sender, EventArgs e)
    {
        using var dialog = new FolderBrowserDialog
        {
            Description = "Select output folder for GUI capture",
            SelectedPath = textBoxOutputDir.Text,
            UseDescriptionForTitle = true
        };
        if (dialog.ShowDialog() == DialogResult.OK)
            textBoxOutputDir.Text = dialog.SelectedPath;
    }

    /// <summary>対象フォームのコントロールツリーを構築</summary>
    private void BuildTree()
    {
        treeViewControls.BeginUpdate();
        treeViewControls.Nodes.Clear();

        // 260323Cl: targetForm のみを対象にする (全フォーム列挙をやめる)
        if (targetForm != null && !targetForm.IsDisposed)
        {
            var visited = new HashSet<Control>();
            var node = CreateNode(targetForm, targetForm.Name, visited);
            treeViewControls.Nodes.Add(node);
        }

        treeViewControls.EndUpdate();

        if (treeViewControls.Nodes.Count > 0)
            treeViewControls.Nodes[0].Expand();
    }

    /// <summary>コントロールのTreeNodeを再帰的に作成</summary>
    private TreeNode CreateNode(Control control, string path, HashSet<Control> visited)
    {
        var typeName = control.GetType().Name;
        var displayText = string.IsNullOrEmpty(control.Name)
            ? $"({typeName})"
            : $"{control.Name}  [{typeName}]  {control.Width}x{control.Height}";

        var node = new TreeNode(displayText) { Tag = new ControlInfo(control, path), Checked = ShouldCapture(control) };

        AddChildNodes(control, path, visited, node);

        return node;
    }

    /// <summary>子コントロールを再帰的にノードに追加。名前のない中間コンテナは透過的にスキップして子をたどる</summary>
    private void AddChildNodes(Control parent, string parentPath, HashSet<Control> visited, TreeNode parentNode)
    {
        // if (parent is NumericBox || parent is ColorControl) return; // 旧実装: 複合コントロールの内部 UI も個別ノード化していた
        if (ShouldStopChildTraversal(parent)) return; // (260323Ch) NumericBox / ColorControl は本体のみキャプチャし、内部 UI は列挙しない

        foreach (Control child in parent.Controls)
        {
            if (visited.Contains(child)) continue;
            visited.Add(child);

            if (string.IsNullOrEmpty(child.Name))
            {
                // 名前のないコンテナ (ToolStripContainer.ContentPanel 等) は
                // ノードを作らずに、その子を親ノードに直接追加する
                AddChildNodes(child, parentPath, visited, parentNode);
            }
            else if (ShouldSkipStandaloneCapture(child))
            {
                // (260323Ch) 単体 Label / RadioButton は個別キャプチャ不要のためノード化しない
                continue;
            }
            else
            {
                var childPath = $"{parentPath}.{child.Name}";
                parentNode.Nodes.Add(CreateNode(child, childPath, visited));
            }
        }

        // ToolStripContainer の特殊パネルも走査する
        if (parent is ToolStripContainer tsc)
        {
            foreach (var panel in new Control[] { tsc.TopToolStripPanel, tsc.BottomToolStripPanel,
                                                   tsc.LeftToolStripPanel, tsc.RightToolStripPanel, tsc.ContentPanel })
            {
                if (visited.Contains(panel)) continue;
                visited.Add(panel);
                AddChildNodes(panel, parentPath, visited, parentNode);
            }
        }

        // SplitContainer のパネルも走査する
        if (parent is SplitContainer sc)
        {
            foreach (var panel in new Control[] { sc.Panel1, sc.Panel2 })
            {
                if (visited.Contains(panel)) continue;
                visited.Add(panel);
                AddChildNodes(panel, parentPath, visited, parentNode);
            }
        }
    }

    /// <summary>キャプチャ対象とすべきかの既定判定</summary>
    private static bool ShouldCapture(Control c)
    {
        // return c.Width >= MinCaptureSize && c.Height >= MinCaptureSize; // 旧実装: 最小サイズ以上はすべて個別キャプチャ対象
        return c.Width >= MinCaptureSize
            && c.Height >= MinCaptureSize
            && !ShouldSkipStandaloneCapture(c); // (260323Ch) 単体 Label / RadioButton は既定で除外
    }

    private static bool ShouldStopChildTraversal(Control control)
    {
        // return control is NumericBox || control is ColorControl; // 旧実装: NumericBox / ColorControl のみ子 UI 列挙を止めていた
        return control is NumericBox
            || control is ColorControl
            || control is WaveLengthControl
            || control is TrackBarAdvanced
            || control is ScalablePictureBox
            || control is ScalablePictureBoxAdvanced
            || control is GraphControl
            || control is DistributionGraphControl
            || control is ChemicalFormulaInputControl
            || control is CheckedListboxAlpha
            || control is SaclaControl
            || control is HorizontalAxisUserControl; // (260323Ch) 優先候補の複合 UserControl も本体のみキャプチャし、内部 UI は列挙しない
    }

    private static bool ShouldSkipStandaloneCapture(Control control)
    {
        return control.GetType() == typeof(Label) || control.GetType() == typeof(RadioButton); // (260323Ch) 単なる Label / RadioButton は個別キャプチャ不要
    }

    /// <summary>ツリーのチェック連動: 親をチェックすると子もチェック</summary>
    private void treeViewControls_AfterCheck(object sender, TreeViewEventArgs e)
    {
        if (e.Action == TreeViewAction.Unknown) return; // プログラムからの変更は無視
        SetChildChecks(e.Node, e.Node.Checked);
    }

    private static void SetChildChecks(TreeNode node, bool isChecked)
    {
        foreach (TreeNode child in node.Nodes)
        {
            child.Checked = isChecked;
            SetChildChecks(child, isChecked);
        }
    }

    /// <summary>Select All ボタン</summary>
    private void buttonSelectAll_Click(object sender, EventArgs e)
    {
        foreach (TreeNode node in treeViewControls.Nodes)
        {
            node.Checked = true;
            SetChildChecks(node, true);
        }
    }

    /// <summary>Deselect All ボタン</summary>
    private void buttonDeselectAll_Click(object sender, EventArgs e)
    {
        foreach (TreeNode node in treeViewControls.Nodes)
        {
            node.Checked = false;
            SetChildChecks(node, false);
        }
    }

    /// <summary>Refresh ボタン</summary>
    private void buttonRefresh_Click(object sender, EventArgs e)
    {
        BuildTree();
    }

    /// <summary>デフォルトの保存先を取得 (言語に応じて doc/cap-en または doc/cap-ja)</summary>
    private static string GetDefaultOutputDir()
    {
        // 260323Cl: ReciPro/doc/capture/ をデフォルトにする
        // 260323Cl: 言語に応じて cap-en / cap-ja を切り替え
        var langDir = System.Threading.Thread.CurrentThread.CurrentUICulture.Name == "ja" ? "cap-ja" : "cap-en";
        // 実行ファイルが bin/Debug/ 等にあるので、プロジェクトルートを探す
        var exeDir = AppDomain.CurrentDomain.BaseDirectory;
        var dir = new DirectoryInfo(exeDir);
        // bin フォルダの2階層上がプロジェクトルート (bin/Debug/net10.0-windows... → ReciPro/)
        while (dir != null && dir.Name != "bin")
            dir = dir.Parent;
        var projectRoot = dir?.Parent;
        if (projectRoot != null)
            return Path.Combine(projectRoot.FullName, "doc", langDir);

        // フォールバック: 実行フォルダ直下
        return Path.Combine(exeDir, "doc", langDir);
    }

    /// <summary>Capture ボタン</summary>
    private void buttonCapture_Click(object sender, EventArgs e)
    {
        var outputDir = textBoxOutputDir.Text; // 260323Cl
        if (string.IsNullOrWhiteSpace(outputDir))
        {
            MessageBox.Show("Please select an output folder.", "Capture", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        var checkedNodes = new List<TreeNode>();
        CollectCheckedNodes(treeViewControls.Nodes, checkedNodes);

        if (checkedNodes.Count == 0)
        {
            MessageBox.Show("No controls are checked.", "Capture", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        progressBar.Minimum = 0;
        progressBar.Maximum = checkedNodes.Count;
        progressBar.Value = 0;
        labelStatus.Text = "Capturing...";
        Application.DoEvents();

        var capturedInfoList = new List<Dictionary<string, object>>();
        int successCount = 0;
        Form lastFrontForm = null; // 260323Cl: フォームが変わったときだけ BringToFront

        foreach (var node in checkedNodes)
        {
            var info = (ControlInfo)node.Tag;
            var control = info.Control;
            var path = info.Path;

            // 260323Cl: CopyFromScreen のため対象フォームを前面に出す (フォームが変わったときだけ1秒待機)
            var ownerForm = control.FindForm();
            if (ownerForm != null && ownerForm != lastFrontForm)
            {
                ownerForm.BringToFront();
                Application.DoEvents();
                System.Threading.Thread.Sleep(1000);
                lastFrontForm = ownerForm;
            }

            try
            {
                // TabControl の場合は全タブを順にキャプチャ
                if (control is TabControl tabControl)
                {
                    CaptureTabControl(tabControl, path, outputDir, capturedInfoList);
                }
                else
                {
                    CaptureControl(control, path, outputDir, capturedInfoList);
                }
                successCount++;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to capture {path}: {ex.Message}");
            }

            progressBar.Value = Math.Min(progressBar.Value + 1, progressBar.Maximum);
            Application.DoEvents();
        }

        // JSON 情報ファイルを出力
        var jsonPath = Path.Combine(outputDir, "_control_info.json");
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(jsonPath, JsonSerializer.Serialize(capturedInfoList, jsonOptions));

        labelStatus.Text = $"Done. {successCount} controls captured.";
        MessageBox.Show($"Captured {successCount} controls to:\n{outputDir}", "Capture Complete",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    /// <summary>単一コントロールをキャプチャ</summary>
    private static void CaptureControl(Control control, string path, string outputDir, List<Dictionary<string, object>> infoList)
    {
        // if (control.Width < MinCaptureSize || control.Height < MinCaptureSize) return; // 旧実装: サイズだけ見てキャプチャ可否を決めていた
        if (!ShouldCapture(control)) return; // (260323Ch) ツリー外の Label / RadioButton が直接渡されても個別キャプチャしない

        // 260323Cl: コントロールが TabPage の子孫なら、そのタブを選択して前面に出す
        EnsureAncestorTabsSelected(control);

        var fileName = SanitizeFileName(path) + ".png";

        // 260323Cl: CopyFromScreen で画面上の実際の表示をキャプチャする
        // DrawToBitmap (WM_PRINT) ではタブヘッダー等が正しく描画されず、GPU描画も取得できないため
        // PointToScreen(Point.Empty) はクライアント領域原点を返すため、ボーダーやタイトルバー分ずれる。
        // Form は Bounds.Location、それ以外は Parent.PointToScreen(Location) でコントロールの実際の左上を取得する。
        var screenPos = control is Form ? control.Bounds.Location
                      : control.Parent != null ? control.Parent.PointToScreen(control.Location)
                      : control.PointToScreen(System.Drawing.Point.Empty);
        var bmp = new Bitmap(control.Width, control.Height);
        using (var g = Graphics.FromImage(bmp))
        {
            g.CopyFromScreen(screenPos, System.Drawing.Point.Empty, control.Size);
        }

        // 260323Cl: 単色ビットマップ (Visible=false のパネル等) はファイルを生成しない
        if (IsSolidColor(bmp))
        {
            bmp.Dispose();
            return;
        }

        // 260323Cl: ImageSharp で最大圧縮 (8ビットパレット + 最高圧縮レベル)
        SaveCompressedPng(bmp, Path.Combine(outputDir, fileName));
        bmp.Dispose();

        var info = new Dictionary<string, object>
        {
            ["path"] = path,
            ["type"] = control.GetType().Name,
            ["text"] = control.Text ?? "",
            ["image"] = fileName,
            ["bounds"] = new { x = control.Left, y = control.Top, w = control.Width, h = control.Height }
        };

        // ToolTip テキスト取得を試みる
        var toolTipText = GetToolTipText(control);
        if (!string.IsNullOrEmpty(toolTipText))
            info["toolTip"] = toolTipText;

        infoList.Add(info);
    }

    /// <summary>260323Cl 追加: コントロールの祖先に TabPage があれば、そのタブを選択して TabControl を前面に出す</summary>
    private static void EnsureAncestorTabsSelected(Control control)
    {
        for (var c = control; c != null; c = c.Parent)
        {
            if (c is TabPage tabPage && tabPage.Parent is TabControl tabCtrl)
            {
                if (tabCtrl.SelectedTab != tabPage)
                {
                    tabCtrl.SelectedTab = tabPage;
                    onClickMethod?.Invoke(tabCtrl, new object[] { EventArgs.Empty });
                }
                tabCtrl.BringToFront();
                tabCtrl.Refresh();
                Application.DoEvents();
            }
        }
    }

    // 260323Cl: タブ Click イベント発火用 (OnClick はprotectedのためリフレクション)
    private static readonly System.Reflection.MethodInfo onClickMethod =
        typeof(Control).GetMethod("OnClick", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

    /// <summary>TabControl の全タブを順にキャプチャ</summary>
    private static void CaptureTabControl(TabControl tabControl, string path, string outputDir, List<Dictionary<string, object>> infoList)
    {
        // 260323Cl: キャプチャ中は tabControl を前面に出す (z-order を保存して復元)
        int originalZIndex = tabControl.Parent?.Controls.GetChildIndex(tabControl) ?? -1;
        tabControl.BringToFront();
        Application.DoEvents();

        // TabControl 全体もキャプチャ
        CaptureControl(tabControl, path, outputDir, infoList);

        int originalIndex = tabControl.SelectedIndex;
        for (int i = 0; i < tabControl.TabPages.Count; i++)
        {
            // 260323Cl: タブ選択 + Click イベント発火 (BringToFront/SendToBack 等のハンドラを実行)
            tabControl.SelectedIndex = i;
            onClickMethod?.Invoke(tabControl, new object[] { EventArgs.Empty });
            tabControl.Refresh();
            Application.DoEvents();

            var tabPage = tabControl.TabPages[i];
            var tabPath = $"{path}.{tabPage.Name}";
            CaptureControl(tabControl, $"{tabPath}._tab_view", outputDir, infoList);
            CaptureControl(tabPage, tabPath, outputDir, infoList);
        }
        // 260323Cl: 元のタブと z-order に戻す
        if (originalIndex >= 0 && originalIndex < tabControl.TabPages.Count)
        {
            tabControl.SelectedIndex = originalIndex;
            onClickMethod?.Invoke(tabControl, new object[] { EventArgs.Empty });
        }
        if (originalZIndex >= 0 && tabControl.Parent != null)
            tabControl.Parent.Controls.SetChildIndex(tabControl, originalZIndex);
        tabControl.Refresh();
        Application.DoEvents();
    }

    /// <summary>チェックされたノードを収集</summary>
    private static void CollectCheckedNodes(TreeNodeCollection nodes, List<TreeNode> result)
    {
        foreach (TreeNode node in nodes)
        {
            if (node.Checked && node.Tag is ControlInfo)
                result.Add(node);
            CollectCheckedNodes(node.Nodes, result);
        }
    }

    /// <summary>コントロールに設定されている ToolTip テキストを取得</summary>
    private static string GetToolTipText(Control control)
    {
        // 親フォームの ToolTip コンポーネントを探す
        var form = control.FindForm();
        if (form == null) return null;

        // リフレクションでフォーム上の ToolTip コンポーネントを検索
        var fields = form.GetType().GetFields(
            System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Instance);

        foreach (var field in fields)
        {
            if (field.FieldType == typeof(ToolTip) && field.GetValue(form) is ToolTip toolTip)
            {
                var text = toolTip.GetToolTip(control);
                if (!string.IsNullOrEmpty(text))
                    return text;
            }
        }
        return null;
    }

    /// <summary>
    /// <summary>
    /// 260323Cl 追加
    /// ビットマップの内側領域（上下左右5px除外）で 99% 以上が同一色であるかを判定する。
    /// Visible=false のパネルなどで灰色一色のビットマップが生成されるケースを検出し、
    /// 無意味なファイルの保存を防止する。3D枠線の影響を避けるため端を除外する。
    /// </summary>
    private static bool IsSolidColor(Bitmap bmp)
    {
        const int margin = 5;
        int x0 = margin, y0 = margin;
        int x1 = bmp.Width - margin, y1 = bmp.Height - margin;
        if (x1 <= x0 || y1 <= y0) return true; // margin で内側が残らない場合は単色扱い

        var rect = new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height);
        var data = bmp.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
        try
        {
            var row = new int[bmp.Width];
            // 基準色は内側の最初のピクセルから取得
            System.Runtime.InteropServices.Marshal.Copy(data.Scan0 + y0 * data.Stride, row, 0, bmp.Width);
            int firstPixel = row[x0];

            for (int y = y0; y < y1; y++)
            {
                System.Runtime.InteropServices.Marshal.Copy(data.Scan0 + y * data.Stride, row, 0, bmp.Width);
                for (int x = x0; x < x1; x++)
                {
                    if (row[x] != firstPixel)
                        return false;
                }
            }
            return true;
        }
        finally
        {
            bmp.UnlockBits(data);
        }
    }

    /// <summary>
    /// 260323Cl 追加
    /// Bitmap を 8ビットパレット PNG (最高圧縮) で保存する。
    /// GUIスクリーンショットは色数が少ないため、パレット化で大幅にサイズ削減できる。
    /// </summary>
    private static void SaveCompressedPng(Bitmap bmp, string filePath)
    {
        // System.Drawing.Bitmap → ImageSharp Image に変換
        using var ms = new MemoryStream();
        bmp.Save(ms, ImageFormat.Png);
        ms.Position = 0;

        using var image = SixLabors.ImageSharp.Image.Load(ms);

        // 8ビットパレット (256色) + 最高圧縮レベル
        var encoder = new PngEncoder
        {
            ColorType = PngColorType.Palette,
            CompressionLevel = PngCompressionLevel.BestCompression,
            BitDepth = PngBitDepth.Bit8,
            Quantizer = new WuQuantizer(new QuantizerOptions { MaxColors = 256 })
        };

        image.SaveAsPng(filePath, encoder);
    }

    /// <summary>ファイル名に使えない文字を置換</summary>
    private static string SanitizeFileName(string name)
    {
        var invalid = Path.GetInvalidFileNameChars();
        foreach (var c in invalid)
            name = name.Replace(c, '_');
        return name;
    }

    /// <summary>ツリーノードに紐づけるコントロール情報</summary>
    private record ControlInfo(Control Control, string Path);
}
