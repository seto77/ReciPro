using System;
using System.Drawing;
using System.Threading;

namespace Crystallography.Controls
{
    // 260428Cl 追加: UI フォントの言語別解決
    // 260519Cl 変更: 自前フォールバックを廃止 (OS の font linking に委譲)。
    //                英語=Segoe UI / 日本語=Yu Gothic UI を前提とする。
    public static class FontHelper
    {
        /// <summary>現在の UICulture に応じた UI フォントファミリ名。</summary>
        // プロセス起動後に CurrentUICulture が切り替わるケースは想定しないため、初回アクセス時に解決して以降キャッシュ。
        public static string DefaultUIFamily => defaultUIFamily.Value;

        private static readonly Lazy<string> defaultUIFamily = new(() =>
            Thread.CurrentThread.CurrentUICulture.Name.StartsWith("ja", StringComparison.OrdinalIgnoreCase)
                ? "Yu Gothic UI"
                : "Segoe UI");

        public static Font GetUIFont(float size = 9f, FontStyle style = FontStyle.Regular)
            => new(DefaultUIFamily, size, style);
    }
}
