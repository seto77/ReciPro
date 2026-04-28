using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Threading;

namespace Crystallography.Controls
{
    // 260428Cl 追加: UI フォントの言語別解決とフォールバック
    // 英語: Segoe UI Variable Text → Segoe UI
    // 日本語: BIZ UDPGothic → Yu Gothic UI
    public static class FontHelper
    {
        private static readonly HashSet<string> installedFamilies = BuildInstalledSet();

        private static HashSet<string> BuildInstalledSet()
        {
            var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            using var col = new InstalledFontCollection();
            foreach (var f in col.Families)
                set.Add(f.Name);
            return set;
        }

        public static bool IsInstalled(string family) => installedFamilies.Contains(family);

        /// <summary>候補リストの先頭から、インストール済みのフォント名を返す。全滅時は最後の候補を返す。</summary>
        public static string PickFamily(params string[] candidates)
        {
            foreach (var c in candidates)
                if (installedFamilies.Contains(c))
                    return c;
            return candidates[^1];
        }

        /// <summary>現在の UICulture に応じた UI フォントファミリ名 (フォールバック処理済み)。</summary>
        // プロセス起動後に CurrentUICulture が切り替わるケースは想定しないため、初回アクセス時に解決して以降キャッシュ。
        public static string DefaultUIFamily => defaultUIFamily.Value;

        private static readonly Lazy<string> defaultUIFamily = new(() =>
            Thread.CurrentThread.CurrentUICulture.Name.StartsWith("ja", StringComparison.OrdinalIgnoreCase)
                ? PickFamily("BIZ UDPGothic", "Yu Gothic UI")
                : PickFamily("Segoe UI Variable Text", "Segoe UI"));

        public static Font GetUIFont(float size = 9f, FontStyle style = FontStyle.Regular)
            => new(DefaultUIFamily, size, style);
    }
}
