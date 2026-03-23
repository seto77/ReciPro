using System;
using System.Collections.Generic;
using System.ComponentModel;
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
public partial class FormCaptureGUI : CaptureFormBase
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
    private bool useCaptureExtenderMode = false; // (260323Ch) CaptureExtender が1つでも設定されているフォームでは flag 指定を最優先する

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
            // useCaptureExtenderMode = false; // 旧実装: 常に FormCaptureGUI 側の既定ルールだけでツリーを構築していた
            useCaptureExtenderMode = CaptureExtender.HasAnyCaptureTargets(targetForm); // (260323Ch) CaptureExtender が設定済みのフォームでは flag 付き対象だけをツリーに出す

            var visitedControls = new HashSet<Control> { targetForm };
            var visitedItems = new HashSet<ToolStripItem>();
            var node = CreateControlNode(targetForm, targetForm.Name, visitedControls, visitedItems, isRoot: true);
            if (node != null)
                treeViewControls.Nodes.Add(node);
        }

        treeViewControls.EndUpdate();

        if (treeViewControls.Nodes.Count > 0)
            treeViewControls.Nodes[0].Expand();
    }

    /// <summary>コントロールの TreeNode を再帰的に作成</summary>
    private TreeNode CreateControlNode(Control control, string path, HashSet<Control> visitedControls, HashSet<ToolStripItem> visitedItems, bool isRoot = false)
    {
        var childNodes = CreateChildNodes(control, path, visitedControls, visitedItems);
        var captureEnabled = IsCaptureEnabled(control);

        if (useCaptureExtenderMode && !isRoot && !captureEnabled && childNodes.Count == 0)
            return null;

        var typeName = control.GetType().Name;
        var displayText = string.IsNullOrEmpty(control.Name)
            ? $"({typeName})"
            : $"{control.Name}  [{typeName}]  {control.Width}x{control.Height}";

        var node = new TreeNode(displayText)
        {
            Tag = new CaptureTargetInfo(control, path),
            Checked = useCaptureExtenderMode ? captureEnabled : ShouldCapture(control)
        };

        foreach (var childNode in childNodes)
            AddChildNode(node, childNode);

        return node;
    }

    /// <summary>ToolStripItem の TreeNode を再帰的に作成</summary>
    private TreeNode CreateToolStripItemNode(ToolStripItem item, string path, HashSet<ToolStripItem> visitedItems)
    {
        var childNodes = CreateToolStripItemChildNodes(item, path, visitedItems);
        var captureEnabled = IsCaptureEnabled(item);

        if (!captureEnabled && childNodes.Count == 0)
            return null;

        var name = string.IsNullOrEmpty(item.Name) ? $"({item.GetType().Name})" : item.Name;
        var node = new TreeNode($"{name}  [{item.GetType().Name}]  {item.Bounds.Width}x{item.Bounds.Height}")
        {
            Tag = new CaptureTargetInfo(item, path),
            Checked = captureEnabled
        };

        foreach (var childNode in childNodes)
            AddChildNode(node, childNode);

        return node;
    }

    private List<TreeNode> CreateChildNodes(Control parent, string parentPath, HashSet<Control> visitedControls, HashSet<ToolStripItem> visitedItems)
    {
        var childNodes = new List<TreeNode>();

        // if (ShouldStopChildTraversal(parent)) return childNodes; // 旧実装: FormCaptureGUI 側の固定除外ルールだけで複合コントロール配下を止めていた
        if (!useCaptureExtenderMode && ShouldStopChildTraversal(parent)) return childNodes; // (260323Ch) CaptureExtender 未設定フォームのみ従来の固定除外ルールを使う

        foreach (Control child in parent.Controls)
        {
            if (!visitedControls.Add(child)) continue;

            if (string.IsNullOrEmpty(child.Name))
            {
                childNodes.AddRange(CreateChildNodes(child, parentPath, visitedControls, visitedItems));
            }
            else
            {
                if (!useCaptureExtenderMode && ShouldSkipStandaloneCapture(child))
                    continue;

                var childPath = $"{parentPath}.{child.Name}";
                var childNode = CreateControlNode(child, childPath, visitedControls, visitedItems);
                if (childNode != null)
                    childNodes.Add(childNode);
            }
        }

        if (useCaptureExtenderMode && parent is ToolStrip toolStrip)
        {
            for (int i = 0; i < toolStrip.Items.Count; i++)
            {
                var item = toolStrip.Items[i];
                if (!visitedItems.Add(item)) continue;

                var itemPath = $"{parentPath}.{GetToolStripItemPathSegment(item, i)}";
                var itemNode = CreateToolStripItemNode(item, itemPath, visitedItems);
                if (itemNode != null)
                    childNodes.Add(itemNode);
            }
        }

        if (useCaptureExtenderMode)
        {
            foreach (var (ownedToolStrip, pathSegment) in GetOwnedToolStrips(parent))
            {
                if (!visitedControls.Add(ownedToolStrip)) continue;

                var toolStripPath = $"{parentPath}.{pathSegment}";
                var toolStripNode = CreateControlNode(ownedToolStrip, toolStripPath, visitedControls, visitedItems);
                if (toolStripNode != null)
                    childNodes.Add(toolStripNode);
            }
        }

        if (parent is ToolStripContainer toolStripContainer)
        {
            foreach (var panel in new Control[]
                     {
                         toolStripContainer.TopToolStripPanel,
                         toolStripContainer.BottomToolStripPanel,
                         toolStripContainer.LeftToolStripPanel,
                         toolStripContainer.RightToolStripPanel,
                         toolStripContainer.ContentPanel
                     })
            {
                if (!visitedControls.Add(panel)) continue;
                childNodes.AddRange(CreateChildNodes(panel, parentPath, visitedControls, visitedItems));
            }
        }

        if (parent is SplitContainer splitContainer)
        {
            foreach (var panel in new Control[] { splitContainer.Panel1, splitContainer.Panel2 })
            {
                if (!visitedControls.Add(panel)) continue;
                childNodes.AddRange(CreateChildNodes(panel, parentPath, visitedControls, visitedItems));
            }
        }

        return childNodes;
    }

    private static List<TreeNode> CreateToolStripItemChildNodes(ToolStripItem item, string itemPath, HashSet<ToolStripItem> visitedItems)
    {
        var childNodes = new List<TreeNode>();
        if (item is not ToolStripDropDownItem dropDownItem) return childNodes;

        for (int i = 0; i < dropDownItem.DropDownItems.Count; i++)
        {
            var childItem = dropDownItem.DropDownItems[i];
            if (!visitedItems.Add(childItem)) continue;

            var childPath = $"{itemPath}.{GetToolStripItemPathSegment(childItem, i)}";
            var captureEnabled = CaptureExtender.IsCaptureEnabled(childItem);
            var grandChildren = CreateToolStripItemChildNodes(childItem, childPath, visitedItems);
            if (!captureEnabled && grandChildren.Count == 0) continue;

            var name = string.IsNullOrEmpty(childItem.Name) ? $"({childItem.GetType().Name})" : childItem.Name;
            var childNode = new TreeNode($"{name}  [{childItem.GetType().Name}]  {childItem.Bounds.Width}x{childItem.Bounds.Height}")
            {
                Tag = new CaptureTargetInfo(childItem, childPath),
                Checked = captureEnabled
            };

            foreach (var grandChild in grandChildren)
                AddChildNode(childNode, grandChild);

            childNodes.Add(childNode);
        }

        return childNodes;
    }

    private bool IsCaptureEnabled(Component component)
    {
        return useCaptureExtenderMode && CaptureExtender.IsCaptureEnabled(component);
    }

    private static void AddChildNode(TreeNode parentNode, TreeNode childNode)
    {
        parentNode.Nodes.Add(childNode); // (260323Ch) apply_patch の大きな差分を抑えるため Add を helper 化
    }

    private static string GetToolStripItemPathSegment(ToolStripItem item, int index)
    {
        return string.IsNullOrWhiteSpace(item.Name) ? $"{item.GetType().Name}{index}" : item.Name;
    }

    private static IEnumerable<(ToolStrip ToolStrip, string PathSegment)> GetOwnedToolStrips(object container)
    {
        var visitedToolStrips = new HashSet<ToolStrip>();
        for (var type = container.GetType(); type != null; type = type.BaseType)
        {
            foreach (var field in type.GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.DeclaredOnly))
            {
                if (!typeof(ToolStrip).IsAssignableFrom(field.FieldType) || field.GetValue(container) is not ToolStrip toolStrip)
                    continue;

                if (!visitedToolStrips.Add(toolStrip))
                    continue;

                var pathSegment = string.IsNullOrWhiteSpace(toolStrip.Name) ? field.Name : toolStrip.Name;
                yield return (toolStrip, pathSegment); // (260323Ch) ContextMenuStrip など Controls 配下にない ToolStrip も designer field から拾う
            }
        }
    }

    /// <summary>キャプチャ対象とすべきかの既定判定</summary>
    private static bool ShouldCapture(Control c)
    {
        // return c.Width >= MinCaptureSize && c.Height >= MinCaptureSize; // 旧実装: 最小サイズ以上はすべて個別キャプチャ対象
        return c.Width >= MinCaptureSize
            && c.Height >= MinCaptureSize
            && !ShouldSkipStandaloneCapture(c); // (260323Ch) CaptureExtender 未設定フォームでは従来の既定除外を維持
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
            || control is HorizontalAxisUserControl; // (260323Ch) CaptureExtender 未設定フォームでは、従来どおり優先候補の複合 UserControl を本体のみキャプチャする
    }

    private static bool ShouldSkipStandaloneCapture(Control control)
    {
        return control.GetType() == typeof(Label) || control.GetType() == typeof(RadioButton); // (260323Ch) CaptureExtender 未設定フォームでは単体 Label / RadioButton を既定除外する
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
            var info = (CaptureTargetInfo)node.Tag;
            var target = info.Target;
            var path = info.Path;

            // 260323Cl: CopyFromScreen のため対象フォームを前面に出す (フォームが変わったときだけ1秒待機)
            var ownerForm = GetOwnerForm(target);
            if (ownerForm != null && ownerForm != lastFrontForm)
            {
                ownerForm.BringToFront();
                Application.DoEvents();
                System.Threading.Thread.Sleep(1000);
                lastFrontForm = ownerForm;
            }

            try
            {
                // if (control is TabControl tabControl) { CaptureTabControl(...); } else { CaptureControl(...); } // 旧実装: Control だけを前提に分岐していた
                if (!useCaptureExtenderMode && target is TabControl tabControl)
                {
                    CaptureTabControl(tabControl, path, outputDir, capturedInfoList);
                }
                else if (target is Control control)
                {
                    CaptureControl(control, path, outputDir, capturedInfoList, ignoreLegacyCaptureRules: useCaptureExtenderMode); // (260323Ch) CaptureExtender 指定時は明示 flag を優先する
                }
                else if (target is ToolStripItem item)
                {
                    CaptureToolStripItem(item, path, outputDir, capturedInfoList); // (260323Ch) ToolStripItem も個別キャプチャ対象に含める
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

    private static Form GetOwnerForm(Component target)
    {
        return target switch
        {
            Control control => control.FindForm(),
            ToolStripItem item => item.Owner?.FindForm() ?? item.OwnerItem?.Owner?.FindForm(),
            _ => null
        };
    }

    /// <summary>単一コントロールをキャプチャ</summary>
    private static void CaptureControl(Control control, string path, string outputDir, List<Dictionary<string, object>> infoList, bool ignoreLegacyCaptureRules = false)
    {
        if (control.Width <= 0 || control.Height <= 0) return;
        // if (!ShouldCapture(control)) return; // 旧実装: FormCaptureGUI 側の既定ルールだけでキャプチャ可否を決めていた
        if (!ignoreLegacyCaptureRules && !ShouldCapture(control)) return; // (260323Ch) CaptureExtender 未設定フォームのみ従来の既定除外を適用する

        // 260323Cl: コントロールが TabPage の子孫なら、そのタブを選択して前面に出す
        EnsureAncestorTabsSelected(control);

        var fileName = SanitizeFileName(path) + ".png";
        // var captureTarget = control; // 旧実装: 常に対象コントロール自身の矩形だけをキャプチャしていた
        var captureTarget = GetCaptureRegionControl(control, ignoreLegacyCaptureRules); // (260323Ch) extender 優先モードの TabPage は親 TabControl のタブ見出しも含めて撮る

        // 260323Cl: CopyFromScreen で画面上の実際の表示をキャプチャする
        // DrawToBitmap (WM_PRINT) ではタブヘッダー等が正しく描画されず、GPU描画も取得できないため
        // PointToScreen(Point.Empty) はクライアント領域原点を返すため、ボーダーやタイトルバー分ずれる。
        // Form は Bounds.Location、それ以外は Parent.PointToScreen(Location) でコントロールの実際の左上を取得する。
        var screenPos = captureTarget is Form ? captureTarget.Bounds.Location
                      : captureTarget.Parent != null ? captureTarget.Parent.PointToScreen(captureTarget.Location)
                      : captureTarget.PointToScreen(System.Drawing.Point.Empty);
        var bmp = new Bitmap(captureTarget.Width, captureTarget.Height);
        using (var g = Graphics.FromImage(bmp))
        {
            g.CopyFromScreen(screenPos, System.Drawing.Point.Empty, captureTarget.Size);
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
            ["bounds"] = new { x = captureTarget.Left, y = captureTarget.Top, w = captureTarget.Width, h = captureTarget.Height }
        };

        // ToolTip テキスト取得を試みる
        var toolTipText = GetToolTipText(control);
        if (!string.IsNullOrEmpty(toolTipText))
            info["toolTip"] = toolTipText;

        infoList.Add(info);
    }

    private static Control GetCaptureRegionControl(Control control, bool ignoreLegacyCaptureRules)
    {
        return ignoreLegacyCaptureRules && control is TabPage tabPage && tabPage.Parent is TabControl tabControl
            ? tabControl // (260323Ch) TabPage 単体指定でも選択中タブ見出しを含めた見た目で残す
            : control;
    }

    private static void CaptureToolStripItem(ToolStripItem item, string path, string outputDir, List<Dictionary<string, object>> infoList)
    {
        if (item.Owner == null || item.Bounds.Width <= 0 || item.Bounds.Height <= 0) return;

        // var captureHost = item.Owner; // 旧実装: 選択項目 1 行ぶんの矩形だけを切り抜いていた
        var captureHost = EnsureToolStripCaptureHostVisible(item); // (260323Ch) 開いたメニュー全体を撮るため、対象項目に対応する DropDown / ContextMenuStrip を前面表示する
        captureHost.Refresh();
        Application.DoEvents();

        var fileName = SanitizeFileName(path) + ".png";
        var screenPos = captureHost.PointToScreen(System.Drawing.Point.Empty);
        using var bmp = new Bitmap(captureHost.Width, captureHost.Height);
        using (var g = Graphics.FromImage(bmp))
        {
            g.CopyFromScreen(screenPos, System.Drawing.Point.Empty, captureHost.Size);
        }

        if (IsSolidColor(bmp))
            return;

        SaveCompressedPng(bmp, Path.Combine(outputDir, fileName));

        var info = new Dictionary<string, object>
        {
            ["path"] = path,
            ["type"] = item.GetType().Name,
            ["text"] = item.Text ?? "",
            ["image"] = fileName,
            ["bounds"] = new { x = captureHost.Left, y = captureHost.Top, w = captureHost.Width, h = captureHost.Height }
        };

        if (!string.IsNullOrWhiteSpace(item.ToolTipText))
            info["toolTip"] = item.ToolTipText;

        infoList.Add(info);
    }

    private static ToolStrip EnsureToolStripCaptureHostVisible(ToolStripItem item)
    {
        EnsureAncestorDropDownsVisible(item);

        if (item is ToolStripDropDownItem dropDownItem && dropDownItem.HasDropDownItems)
        {
            if (!dropDownItem.DropDown.Visible)
            {
                dropDownItem.ShowDropDown(); // (260323Ch) File のような親メニュー項目はドロップダウン全体を開いてから撮る
                dropDownItem.DropDown.Refresh();
                Application.DoEvents();
                System.Threading.Thread.Sleep(200);
            }
            return dropDownItem.DropDown;
        }

        if (item.Owner is ContextMenuStrip contextMenuStrip)
        {
            if (!contextMenuStrip.Visible && contextMenuStrip.SourceControl != null)
            {
                contextMenuStrip.Show(contextMenuStrip.SourceControl, new System.Drawing.Point(0, contextMenuStrip.SourceControl.Height));
                Application.DoEvents();
                System.Threading.Thread.Sleep(200);
            }
            return contextMenuStrip;
        }

        return item.Owner is ToolStripDropDown toolStripDropDown
            ? toolStripDropDown // (260323Ch) Open のような配下項目は属している開いたメニュー全体を撮る
            : item.Owner;
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

    private static void EnsureAncestorDropDownsVisible(ToolStripItem item)
    {
        if (item.OwnerItem is not ToolStripDropDownItem ownerItem) return;

        EnsureAncestorDropDownsVisible(ownerItem);
        if (!ownerItem.DropDown.Visible)
        {
            ownerItem.ShowDropDown();
            ownerItem.DropDown.Refresh();
            Application.DoEvents();
            System.Threading.Thread.Sleep(200);
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
            if (node.Checked && node.Tag is CaptureTargetInfo)
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

    /// <summary>ツリーノードに紐づけるキャプチャ対象情報</summary>
    private record CaptureTargetInfo(Component Target, string Path);
}

