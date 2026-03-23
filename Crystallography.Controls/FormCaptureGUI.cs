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
        BuildTree();
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

    /// <summary>キャプチャ対象とすべきかの既定判定 (粒度の細かい画像まで総ざらいで保存する方針)</summary>
    private static bool ShouldCapture(Control c)
    {
        // 最小サイズ未満のみ除外。それ以外は全て対象とする。
        return c.Width >= MinCaptureSize && c.Height >= MinCaptureSize;
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

    /// <summary>デフォルトの保存先を取得 (実行ファイルの親の doc/capture/)</summary>
    private static string GetDefaultOutputDir()
    {
        // 260323Cl: ReciPro/doc/capture/ をデフォルトにする
        // 実行ファイルが bin/Debug/ 等にあるので、プロジェクトルートを探す
        var exeDir = AppDomain.CurrentDomain.BaseDirectory;
        var dir = new DirectoryInfo(exeDir);
        // bin フォルダの2階層上がプロジェクトルート (bin/Debug/net10.0-windows... → ReciPro/)
        while (dir != null && dir.Name != "bin")
            dir = dir.Parent;
        var projectRoot = dir?.Parent;
        if (projectRoot != null)
            return Path.Combine(projectRoot.FullName, "doc", "capture");

        // フォールバック: 実行フォルダ直下
        return Path.Combine(exeDir, "doc", "capture");
    }

    /// <summary>Capture ボタン</summary>
    private void buttonCapture_Click(object sender, EventArgs e)
    {
        var defaultDir = GetDefaultOutputDir();
        using var dialog = new FolderBrowserDialog
        {
            Description = "Select output folder for GUI capture",
            SelectedPath = defaultDir,
            UseDescriptionForTitle = true
        };
        if (dialog.ShowDialog() != DialogResult.OK) return;

        var outputDir = dialog.SelectedPath;
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

        foreach (var node in checkedNodes)
        {
            var info = (ControlInfo)node.Tag;
            var control = info.Control;
            var path = info.Path;

            // 260323Cl: CopyFromScreen のため対象フォームを前面に出す
            control.FindForm()?.BringToFront();
            Application.DoEvents();

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
        if (control.Width < MinCaptureSize || control.Height < MinCaptureSize) return;

        var fileName = SanitizeFileName(path) + ".png";

        // 260323Cl: CopyFromScreen で画面上の実際の表示をキャプチャする
        // DrawToBitmap (WM_PRINT) ではタブヘッダー等が正しく描画されず、GPU描画も取得できないため
        var bmp = new Bitmap(control.Width, control.Height);
        using (var g = Graphics.FromImage(bmp))
        {
            var screenPos = control.PointToScreen(System.Drawing.Point.Empty);
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

    /// <summary>TabControl の全タブを順にキャプチャ</summary>
    private static void CaptureTabControl(TabControl tabControl, string path, string outputDir, List<Dictionary<string, object>> infoList)
    {
        // TabControl 全体もキャプチャ
        CaptureControl(tabControl, path, outputDir, infoList);

        int originalIndex = tabControl.SelectedIndex;
        for (int i = 0; i < tabControl.TabPages.Count; i++)
        {
            tabControl.SelectedIndex = i;
            tabControl.Refresh();
            Application.DoEvents();

            var tabPage = tabControl.TabPages[i];
            var tabPath = $"{path}.{tabPage.Name}";
            CaptureControl(tabControl, $"{tabPath}._tab_view", outputDir, infoList);
            CaptureControl(tabPage, tabPath, outputDir, infoList);
        }
        tabControl.SelectedIndex = originalIndex;
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
