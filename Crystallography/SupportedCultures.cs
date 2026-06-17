using System;
using System.Linq;
using System.Threading;

namespace Crystallography;

// 260617Cl 追加: UI がサポートする言語 (カルチャ) の中央 allow-list。
// 多言語化方針 (.project-guidance/ReciPro_多言語化方針.md, Phase 0) に基づき、
// 従来 ja/en の二値前提で各所に散っていた判定
//   - Reg.cs のカルチャ復元 (ja で始まれば ja / それ以外 en に潰していた)
//   - FontHelper の UI フォント選択
//   - FormMain の言語メニューのチェック状態と切替
//   - Program.cs / GuiCapture の --capture カルチャ指定
// を、この 1 箇所の配列駆動へ統一する。新言語を増やすときは原則ここへ 1 行足すだけにするのが目的。

/// <summary>UI がサポートする 1 言語 (カルチャ) の定義。</summary>
public sealed class UiCulture
{
    /// <summary>CultureInfo の neutral 名。サテライトアセンブリのフォルダ名にもなる。例 "en" / "ja" / "de" / "zh-Hans"。</summary>
    public string Name { get; }

    /// <summary>言語メニュー等に表示する自言語表記。例 "English" / "日本語" / "Deutsch" / "简体中文"。</summary>
    public string NativeName { get; }

    /// <summary>この言語の UI フォントファミリ。Latin 系は "Segoe UI"、日本語は "Yu Gothic UI"、中国語簡体字は "Microsoft YaHei UI"。</summary>
    public string FontFamily { get; }

    /// <summary>GitHub Pages マニュアルの言語ディレクトリ ("en" or "ja")。マニュアル未整備の言語は "en" にフォールバックする。</summary>
    public string HelpCulture { get; }

    /// <summary>UI 翻訳 (resx) が出荷可能な水準まで用意できているか。
    /// false の言語はプラミングだけ先行整備済みで、言語メニューには出さない (翻訳が揃った段階で true にする)。</summary>
    public bool Released { get; }

    public UiCulture(string name, string nativeName, string fontFamily, string helpCulture, bool released)
    {
        Name = name;
        NativeName = nativeName;
        FontFamily = fontFamily;
        HelpCulture = helpCulture;
        Released = released;
    }
}

/// <summary>対応言語の中央定義と、カルチャ名→対応言語の解決。</summary>
public static class SupportedCultures
{
    // Released=false は「プラミングは N 言語対応済だが翻訳 resx 未整備」を意味する。
    // 翻訳が揃ったら Released を true にし、言語メニュー項目 (Designer) を追加する。
    public static readonly UiCulture[] All = new UiCulture[]
    {
        new("en",      "English",   "Segoe UI",              "en", true ),
        new("ja",      "日本語",     "Yu Gothic UI",          "ja", true ),
        new("de",      "Deutsch",   "Segoe UI",              "en", false),
        new("fr",      "Français",  "Segoe UI",              "en", false),
        new("es",      "Español",   "Segoe UI",              "en", false),
        new("pt",      "Português", "Segoe UI",              "en", false),
        new("it",      "Italiano",  "Segoe UI",              "en", false), // 260617Cl 追加: Latin/Segoe UI・IUCr 用語あり
        new("ru",      "Русский",   "Segoe UI",              "en", false), // 260617Cl 追加: Cyrillic は Segoe UI が網羅・IUCr 用語あり
        new("zh-Hans", "简体中文",   "Microsoft YaHei UI",    "en", false),
        new("zh-Hant", "繁體中文",   "Microsoft JhengHei UI", "en", false), // 260617Cl 追加: 繁体字(台湾/香港)。zh-Hans より長い接頭辞として先に評価される
        new("ko",      "한국어",     "Malgun Gothic",         "en", false), // 260617Cl 追加: 韓国語フォント分岐。IUCr 未収録→別途用語典拠
    };

    /// <summary>既定言語 (英語)。未知カルチャはここへ解決する。</summary>
    public static UiCulture Default => All[0];

    /// <summary>
    /// カルチャ名を対応言語へ解決する。完全一致 → 接頭辞一致 (例 "ja-JP"→"ja"、"zh-Hans-CN"→"zh-Hans") → 既定 (en)。
    /// 接頭辞は長い順に評価して "zh-Hans" を "zh" より優先する。
    /// </summary>
    public static UiCulture Resolve(string cultureName)
    {
        if (string.IsNullOrWhiteSpace(cultureName)) return Default;
        var name = cultureName.Trim();

        var exact = All.FirstOrDefault(c => string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase));
        if (exact != null) return exact;

        foreach (var c in All.OrderByDescending(c => c.Name.Length))
            if (name.StartsWith(c.Name, StringComparison.OrdinalIgnoreCase))
                return c;

        // 260617Cl: script を含まない OS ロケール (例 "zh-TW"→親"zh-Hant"、"zh-CN"→親"zh-Hans") を
        // CultureInfo の親チェーンで再解決する (接頭辞一致では拾えないため)。
        try
        {
            for (var ci = System.Globalization.CultureInfo.GetCultureInfo(name).Parent;
                 !string.IsNullOrEmpty(ci.Name); ci = ci.Parent)
            {
                var byParent = All.FirstOrDefault(c => string.Equals(c.Name, ci.Name, StringComparison.OrdinalIgnoreCase));
                if (byParent != null) return byParent;
            }
        }
        catch { /* 未知カルチャ名は GetCultureInfo が投げる → 既定へ */ }

        return Default;
    }

    /// <summary>現在の UICulture に対応する言語。</summary>
    public static UiCulture Current => Resolve(Thread.CurrentThread.CurrentUICulture.Name);
}
