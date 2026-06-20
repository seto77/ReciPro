using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Crystallography.Controls;

// 260621Cl 追加 (§2.5 横展開): L10n の中央テーブル (型名→項目) に基づき、Localizable=false の
// フォーム/UC が Designer.cs に英語直書きした可視ラベルを実行時に現在の UI カルチャへ差し替える。
// 対象プロパティ: Control.Text / DataGridView 列の HeaderText / ToolStripItem(メニュー)の Text。
// FormBase.OnLoad と UserControlBase.OnLoad から Apply(this) を呼ぶ。デザイン時は何もしない。
// 詳細は .project-guidance/ReciPro_多言語化方針.md §3-B(方式②)/§12.7。
/// <summary>コード側多言語化テーブル (<see cref="L10n"/>) をコントロールツリーへ適用するヘルパー。</summary>
public static class CodeLocalizer
{
    /// <summary><paramref name="root"/> (Form / UserControl) の型に登録された訳を、配下のコントロール・
    /// メニュー項目・DataGridView 列に適用する。未登録の型・デザイン時は何もしない。</summary>
    public static void Apply(Control root)
    {
        if (root == null || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            return;
        var entries = L10n.Get(root.GetType().Name);
        if (entries == null)
            return;

        var ctrlByName = new Dictionary<string, Control>();
        var itemByName = new Dictionary<string, ToolStripItem>();
        var grids = new List<DataGridView>();
        Collect(root, ctrlByName, itemByName, grids);

        foreach (var e in entries)
        {
            if (e.Prop == "HeaderText")
            {
                foreach (var g in grids)
                {
                    var col = FindColumn(g, e.Ctrl);
                    if (col != null) { col.HeaderText = e.Resolve(); break; }
                }
            }
            else // "Text"
            {
                if (e.Ctrl == "this")
                    root.Text = e.Resolve();
                else if (ctrlByName.TryGetValue(e.Ctrl, out var c))
                    c.Text = e.Resolve();
                else if (itemByName.TryGetValue(e.Ctrl, out var it))
                    it.Text = e.Resolve();
            }
        }
    }

    // コントロールツリーを再帰し、名前→Control / 名前→ToolStripItem / DataGridView を収集する。
    // MenuStrip/StatusStrip/ToolStrip (ToolStrip 派生) の Items、各コントロールの ContextMenuStrip、
    // ToolStripDropDownItem の入れ子メニューも辿る (メニュー項目は Controls ツリー外のため別途必要)。
    private static void Collect(Control c, Dictionary<string, Control> ctrlByName,
                                Dictionary<string, ToolStripItem> itemByName, List<DataGridView> grids)
    {
        if (!string.IsNullOrEmpty(c.Name))
            ctrlByName[c.Name] = c;
        if (c is DataGridView dgv)
            grids.Add(dgv);
        if (c is ToolStrip ts)
            CollectItems(ts.Items, itemByName);
        if (c.ContextMenuStrip != null)
            CollectItems(c.ContextMenuStrip.Items, itemByName);
        foreach (Control ch in c.Controls)
            Collect(ch, ctrlByName, itemByName, grids);
    }

    private static void CollectItems(ToolStripItemCollection items, Dictionary<string, ToolStripItem> map)
    {
        foreach (ToolStripItem it in items)
        {
            if (!string.IsNullOrEmpty(it.Name))
                map[it.Name] = it;
            if (it is ToolStripDropDownItem ddi && ddi.HasDropDownItems)
                CollectItems(ddi.DropDownItems, map);
        }
    }

    private static DataGridViewColumn FindColumn(DataGridView g, string name)
    {
        foreach (DataGridViewColumn col in g.Columns)
            if (col.Name == name)
                return col;
        return null;
    }
}
