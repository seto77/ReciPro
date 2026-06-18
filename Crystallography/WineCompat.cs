using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Crystallography;

//260610Cl 追加: Wine (macOS/Linux 上の互換レイヤー) の検知と、収録グリフの広い代替フォントへの優先切替。
//
// 背景: Wine 環境には Segoe UI / Yu Gothic UI / Tahoma 等が無く、Wine 同梱の簡易代替フォントは
//       上付き文字(⁻¹ 等)・回転矢印(↺↻)・結合マクロン(σ̄) などの記号グリフを欠くため文字化け(豆腐)が起きる。
//       Windows の GDI フォントリンク(自動フォールバック)も Wine では不完全で、特に GraphicsPath.AddString
//       経路はフォールバックが一切働かない。そこでアプリ側で「実際にインストールされている
//       広カバレッジフォント」へ家族ごと選び直すのが最も確実な対処となる。
//
// 方針: Wine でなければ全 API が完全 no-op (Windows での挙動は不変)。Wine の場合のみ、
//       英語 UI 系(Segoe UI/Tahoma/Arial)・日本語 UI 系(Yu Gothic UI 等)・セリフ系(Times New Roman)を、
//       prefix にインストール済みの DejaVu / Noto / Liberation 系へ置き換える。
//       候補が 1 つも無ければ元のフォント名のまま (従来より悪化はしない)。
//       日本語 UI (.ja.resx) は和文グリフと記号の両方を持つ Noto CJK 系を優先する。
public static class WineCompat
{
    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    private static extern IntPtr GetModuleHandle(string moduleName);

    [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false)]
    private static extern IntPtr GetProcAddress(IntPtr module, string procName);

    /// <summary>Wine 上で実行されているか。ntdll.dll の wine_get_version エクスポートの有無で判定する (定番の確実な方法)。</summary>
    public static bool IsWine { get; } = DetectWine();

    private static bool DetectWine()
    {
        try
        {
            if (Environment.GetEnvironmentVariable("RECIPRO_FORCE_WINE") == "1")
                return true; // Windows 上での動作確認用 (RECIPRO_GLDIAG と同様の診断スイッチ)
            var ntdll = GetModuleHandle("ntdll.dll");
            return ntdll != IntPtr.Zero && GetProcAddress(ntdll, "wine_get_version") != IntPtr.Zero;
        }
        catch { return false; }
    }

    // 置換候補 (収録グリフの広い順)。先頭から「実際にインストールされている」最初のものを採用する。
    private static readonly Dictionary<string, string[]> candidates = new(StringComparer.OrdinalIgnoreCase)
    {
        // 英語 UI / オーバーレイ描画系 (sans)
        ["Segoe UI"] = ["DejaVu Sans", "Noto Sans", "Liberation Sans"],
        ["Tahoma"] = ["DejaVu Sans", "Noto Sans", "Liberation Sans"],
        ["Arial"] = ["DejaVu Sans", "Liberation Sans", "Noto Sans"],
        ["Segoe UI Symbol"] = ["DejaVu Sans", "Noto Sans Symbols 2", "Noto Sans"],
        // 日本語 UI (.ja.resx) 系: 和文グリフ + 記号の両方を収録する CJK フォントを優先
        ["Yu Gothic UI"] = ["Noto Sans CJK JP", "Noto Sans JP", "Source Han Sans JP", "Meiryo UI", "MS UI Gothic"],
        ["Yu Gothic"] = ["Noto Sans CJK JP", "Noto Sans JP", "Source Han Sans JP", "Meiryo", "MS Gothic"],
        ["Meiryo UI"] = ["Noto Sans CJK JP", "Noto Sans JP", "Source Han Sans JP"],
        // セリフ系 (空間群図・ステレオネットのラベル)。AddString 経路はフォールバック皆無なのでここが効く
        ["Times New Roman"] = ["DejaVu Serif", "Noto Serif", "Liberation Serif"],
        // 等幅系。260618Cl 追加: 旧候補に欠落していた (アプリ内で Courier New を使う箇所が Wine で豆腐化しうる)
        ["Courier New"] = ["DejaVu Sans Mono", "Liberation Mono", "Noto Sans Mono"],
    };

    private static readonly Dictionary<string, string> cache = new(StringComparer.OrdinalIgnoreCase);
    private static HashSet<string> installedFamilies;

    /// <summary>
    /// フォントファミリ名を解決する。Wine 上でのみ、候補のうちインストール済みの代替名を返す。
    /// Windows 上、または代替が見つからない場合は引数をそのまま返す。
    /// </summary>
    public static string Resolve(string familyName)
    {
        if (!IsWine || familyName == null)
            return familyName;
        lock (cache)
        {
            if (cache.TryGetValue(familyName, out var hit))
                return hit;
            var result = familyName;
            if (candidates.TryGetValue(familyName, out var cands))
            {
                installedFamilies ??= BuildInstalledFamilies();
                foreach (var c in cands)
                    if (installedFamilies.Contains(c)) { result = c; break; }
            }
            cache[familyName] = result;
            return result;
        }
    }

    private static HashSet<string> BuildInstalledFamilies()
    {
        var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        try
        {
            using var col = new InstalledFontCollection();
            foreach (var f in col.Families)
                set.Add(f.Name);
        }
        catch { }
        return set;
    }

    /// <summary>
    /// Font オブジェクトを解決する。ファミリが置換対象ならサイズ・スタイル・単位を保った新しい Font を返し、
    /// それ以外は同じインスタンスをそのまま返す (参照比較で変更の有無を判定できる)。
    /// </summary>
    public static Font Resolve(Font font)
    {
        if (!IsWine || font == null)
            return font;
        var original = font.OriginalFontName ?? font.Name; // resx 由来でファミリ欠落時も要求名で判定する
        var mapped = Resolve(original);
        if (string.Equals(mapped, original, StringComparison.OrdinalIgnoreCase))
            return font;
        try { return new Font(mapped, font.Size, font.Style, font.Unit); }
        catch { return font; }
    }

    /// <summary>
    /// コントロールツリー全体 (DataGridView の各 CellStyle、ToolStrip/ContextMenuStrip の項目を含む) に
    /// フォント置換を適用する。Wine でなければ何もしない。FormBase.Load から呼ばれる。
    /// </summary>
    public static void ApplyTo(Control control)
    {
        if (!IsWine || control == null)
            return;

        var mapped = Resolve(control.Font);
        if (!ReferenceEquals(mapped, control.Font))
            control.Font = mapped;

        if (control is DataGridView dgv)
        {
            ApplyTo(dgv.ColumnHeadersDefaultCellStyle);
            ApplyTo(dgv.RowHeadersDefaultCellStyle);
            ApplyTo(dgv.DefaultCellStyle);
            foreach (DataGridViewColumn col in dgv.Columns)
                if (col.HasDefaultCellStyle)
                    ApplyTo(col.DefaultCellStyle);
        }
        else if (control is ToolStrip ts) // MenuStrip / StatusStrip / ContextMenuStrip を含む
        {
            foreach (ToolStripItem item in ts.Items)
                ApplyTo(item);
        }

        if (control.ContextMenuStrip != null)
            ApplyTo(control.ContextMenuStrip);

        foreach (Control child in control.Controls)
            ApplyTo(child);
    }

    private static void ApplyTo(DataGridViewCellStyle style)
    {
        if (style?.Font == null) // null は親スタイル継承なので触らない
            return;
        var mapped = Resolve(style.Font);
        if (!ReferenceEquals(mapped, style.Font))
            style.Font = mapped;
    }

    private static void ApplyTo(ToolStripItem item)
    {
        var mapped = Resolve(item.Font);
        if (!ReferenceEquals(mapped, item.Font))
            item.Font = mapped;
        if (item is ToolStripDropDownItem dd)
            foreach (ToolStripItem sub in dd.DropDownItems)
                ApplyTo(sub);
    }
}
