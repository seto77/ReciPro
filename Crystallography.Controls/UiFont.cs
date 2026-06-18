using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    // 260618Cl 追加: UI フォントの中央集約リゾルバ。FormBase.Load から全コントロールツリーへ適用し、
    // 各フォントを次の 3 軸で解決する (bold/italic = Style は常に保持):
    //   (1) 言語軸  : UI 本文フォントを現在カルチャの書体へ (Segoe UI ↔ Yu Gothic UI ↔ Microsoft YaHei UI …)
    //   (2) サイズ  : 現 pt を 5 段階ティア(SS/S/M/L/LL)へ分類し、ティア→カルチャ別 pt で解決 (サイズ正規化)
    //   (3) ﾌﾟﾗｯﾄﾌｫｰﾑ軸: Wine では実在の広カバレッジ書体へ (WineCompat 経由。Windows では no-op)
    //
    // 旧構成は FontHelper.GetUIFont(ambient 既定) + WineCompat.ApplyTo(Wine のみツリー walk) の 2 系統。
    // 本クラスが言語軸+ティアを Windows でも効かせ、内部で WineCompat をプラットフォーム軸として呼ぶ。
    // Times New Roman / Courier New / Segoe UI Symbol 等の「役割フォント」は言語軸では触らず、
    // Wine のときだけ WineCompat が代替する (科学表記の Times は Windows では不変)。
    //
    // ティアは resx/Designer に保存されない。デザイナで選んだ pt を実行時にバケット分類して導出する
    // (= 既存コントロールを 1 つも改修せずに乗せられる)。境界は TierOf を参照。
    public static class UiFont
    {
        /// <summary>UI フォントの 5 段階サイズ。SS=最小 … LL=最大(タイトル)。</summary>
        public enum Tier { SS, S, M, L, LL }

        // ティア→pt。当面すべてのカルチャ共通 (family だけ差し替える)。将来 zh/ja で光学的に微調整したく
        // なったら、ここをカルチャ別に分岐する「唯一のテーブル」。既定値は実測 pt 分布から
        // 「主要サイズ(9pt=M / 9.75pt=L, 計 ~1700 コントロール)を無変化に保つ」保守値。
        public static float PtOf(Tier t) => t switch
        {
            Tier.SS => 7f,
            Tier.S => 8.25f,
            Tier.M => 9f,
            Tier.L => 9.75f,
            Tier.LL => 13f,
            _ => 9f,
        };

        /// <summary>現 pt を最近接ティアへ分類する。デザイナで選んだ pt がこの境界で 5 段階に丸められる。</summary>
        public static Tier TierOf(float pt) =>
              pt < 7.6f ? Tier.SS
            : pt < 8.6f ? Tier.S
            : pt < 9.5f ? Tier.M
            : pt < 11.6f ? Tier.L
            : Tier.LL;

        // UI 本文フォント集合 = SupportedCultures の各 FontFamily。Times/Courier/Segoe UI Symbol/Tahoma 等は
        // 含まれない (= 役割フォント扱いで言語軸では触らない)。
        private static readonly HashSet<string> uiBodyFamilies = BuildUiBodyFamilies();

        private static HashSet<string> BuildUiBodyFamilies()
        {
            var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var c in SupportedCultures.All)
                set.Add(c.FontFamily);
            return set;
        }

        public static bool IsUiBodyFont(string family) => family != null && uiBodyFamilies.Contains(family);

        // 現在カルチャ (プロセス起動後に切り替わらない前提。FontHelper.DefaultUIFamily と同じ扱い)。
        private static readonly Lazy<UiCulture> culture = new(() => SupportedCultures.Current);

        /// <summary>1 つの Font を解決する。変更不要なら同一インスタンスを返す (参照比較で適用要否を判定し、
        /// ambient 継承しているコントロールを無駄に explicit 化しない)。</summary>
        public static Font Resolve(Font font)
        {
            if (font == null)
                return font;

            var family = font.OriginalFontName ?? font.Name; // resx 由来でファミリ欠落時も要求名で判定
            if (IsUiBodyFont(family) && font.Unit == GraphicsUnit.Point)
            {
                var newFamily = culture.Value.FontFamily;          // 言語軸
                var newPt = PtOf(TierOf(font.Size));                // サイズ(ティア)軸
                if (WineCompat.IsWine)
                    newFamily = WineCompat.Resolve(newFamily);      // プラットフォーム軸

                if (string.Equals(newFamily, family, StringComparison.OrdinalIgnoreCase) && newPt == font.Size)
                    return font;                                    // 既に一致 → ambient 継承を壊さない
                try { return new Font(newFamily, newPt, font.Style); } // Style(太字/斜体)保持・単位 Point
                catch { return font; }
            }

            // 役割フォント (Times/Courier/Symbol …): 言語軸では触らない。Wine のみ広カバレッジへ代替。
            return WineCompat.IsWine ? WineCompat.Resolve(font) : font;
        }

        /// <summary>コントロールツリー全体へ適用 (DataGridView の各 CellStyle、ToolStrip/ContextMenuStrip の
        /// 項目を含む)。FormBase.Load から呼ばれる。</summary>
        public static void Apply(Control control)
        {
            if (control == null)
                return;

            var mapped = Resolve(control.Font);
            if (!ReferenceEquals(mapped, control.Font))
                control.Font = mapped;

            if (control is DataGridView dgv)
            {
                // 260618Cl 拡充: フォント family 差替を DataGridView の全 CellStyle 経路へ広げる
                //   (多言語化: CJK で Segoe UI のまま残ると Han グリフが font-linking で不揃いになるため)。
                //   各 getter は null のとき空スタイルを遅延生成するが、Apply は Font==null を素通しするので無害。
                Apply(dgv.ColumnHeadersDefaultCellStyle);
                Apply(dgv.RowHeadersDefaultCellStyle);
                Apply(dgv.DefaultCellStyle);
                Apply(dgv.RowsDefaultCellStyle);              // 260618Cl 追加
                Apply(dgv.AlternatingRowsDefaultCellStyle);   // 260618Cl 追加
                Apply(dgv.RowTemplate.DefaultCellStyle);      // 260618Cl 追加
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (col.HasDefaultCellStyle)
                        Apply(col.DefaultCellStyle);
                    if (col.HeaderCell != null)
                        Apply(col.HeaderCell.Style);          // 260618Cl 追加: 列見出しセルの個別スタイル
                }
            }
            else if (control is ToolStrip ts) // MenuStrip / StatusStrip / ContextMenuStrip を含む
            {
                foreach (ToolStripItem item in ts.Items)
                    Apply(item);
            }

            if (control.ContextMenuStrip != null)
                Apply(control.ContextMenuStrip);

            foreach (Control child in control.Controls)
                Apply(child);
        }

        private static void Apply(DataGridViewCellStyle style)
        {
            if (style?.Font == null) // null は親スタイル継承なので触らない
                return;
            var mapped = Resolve(style.Font);
            if (!ReferenceEquals(mapped, style.Font))
                style.Font = mapped;
        }

        private static void Apply(ToolStripItem item)
        {
            var mapped = Resolve(item.Font);
            if (!ReferenceEquals(mapped, item.Font))
                item.Font = mapped;
            // 260618Cl 追加: ToolStripComboBox / ToolStripTextBox 等のホストされた実コントロールにも適用する
            //   (item.Font だけでは内側の ComboBox/TextBox の描画フォントが変わらないため)。
            if (item is ToolStripControlHost host && host.Control != null)
                Apply(host.Control);
            if (item is ToolStripDropDownItem dd)
                foreach (ToolStripItem sub in dd.DropDownItems)
                    Apply(sub);
        }
    }
}
