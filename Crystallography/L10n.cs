namespace Crystallography;

// 260620Cl 追加: 方式② コード側多言語化ヘルパー。
// VS Designer で Localizable=false のままハードコードされたラベル (resx の ApplyResources に乗らない文字列) を、
// 実行時に現在の UI カルチャの訳へ差し替えるための N 言語版 Loc()。
// 既存の FormBeamInteraction.cs の private Loc(en, ja) を 11 言語へ一般化したもの。
// 用途・適用方針は .project-guidance/ReciPro_多言語化方針.md §3-B(方式②) を参照。
/// <summary>コード側に直書きした UI 文字列を、現在の UI カルチャの訳へ解決する localization ヘルパー。</summary>
public static class L10n
{
    /// <summary>
    /// 現在の UI カルチャ (<see cref="SupportedCultures.Current"/>) に対応する訳を返す。
    /// 名前付き引数で呼ぶこと。例:
    /// <code>label.Text = Loc(en: "Space Group", ja: "空間群", de: "Raumgruppe", ...);</code>
    /// 省略・空文字の言語は英語 (<paramref name="en"/>) にフォールバックする。
    /// 引数はカルチャ名で解決するので <see cref="SupportedCultures.All"/> の並び順には依存しない
    /// (新言語を増やすときはここに 1 ケース足し、各呼び出しに該当引数を任意で足す)。
    /// </summary>
    public static string Loc(
        string en, string ja = "", string de = "", string fr = "", string es = "",
        string pt = "", string it = "", string ru = "", string zhHans = "",
        string zhHant = "", string ko = "")
    {
        var pick = SupportedCultures.Current.Name switch
        {
            "ja" => ja,
            "de" => de,
            "fr" => fr,
            "es" => es,
            "pt" => pt,
            "it" => it,
            "ru" => ru,
            "zh-Hans" => zhHans,
            "zh-Hant" => zhHant,
            "ko" => ko,
            _ => en,
        };
        return string.IsNullOrEmpty(pick) ? en : pick;
    }
}
