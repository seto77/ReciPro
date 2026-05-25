// 260525Cl: ヘッダーの言語セレクタ (extra.alternate) を「現在ページの相手言語版」へ向け直す。
// extra.alternate のリンクは静的 (英語=/ReciPro/, 日本語=/ReciPro/ja/) なので、既定では言語を切り替えると
// 各言語のトップページへ戻ってしまう。URL 構造が EN=<base>en/<slug>/ ・ JA=<base>ja/<slug>/ と予測可能なため、
// 現在ページの slug から相手言語の対応ページ URL を組み立てて href を差し替える。
(function () {
  "use strict";

  // 相手言語に 1:1 の対応ページが無いケースの上書き (EN のみ存在するページ → JA 側の代替ページ slug)
  var EN_TO_JA_OVERRIDE = {
    "0-1-crystal-orientation-control/": "0-main-window/"
  };

  function retarget() {
    var links = document.querySelectorAll("a.md-select__link[hreflang]");
    if (links.length < 2) return;

    var enRoot = null, jaRoot = null;
    links.forEach(function (a) {
      var lang = a.getAttribute("hreflang");
      if (lang === "en") enRoot = a.getAttribute("href");      // 例 "/ReciPro/"
      else if (lang === "ja") jaRoot = a.getAttribute("href"); // 例 "/ReciPro/ja/"
    });
    if (!enRoot || !jaRoot) return;

    var enPrefix = enRoot + "en/";   // 例 "/ReciPro/en/"
    var path = window.location.pathname;

    // 現在ページの言語と slug (末尾スラッシュ込み) を判定する。
    var slug, fromEn;
    if (path === jaRoot) { slug = ""; fromEn = false; }                       // JA トップ
    else if (path.indexOf(jaRoot) === 0) { slug = path.slice(jaRoot.length); fromEn = false; } // JA ページ
    else if (path.indexOf(enPrefix) === 0) { slug = path.slice(enPrefix.length); fromEn = true; } // EN ページ
    else if (path === enRoot) { slug = ""; fromEn = true; }                   // EN トップ
    else return; // 想定外のパスは触らない

    var enTarget = slug === "" ? enRoot : enPrefix + slug;
    var jaTarget = slug === "" ? jaRoot : jaRoot + slug;

    // EN にしか無いページから JA へ切り替えるときの代替先。
    if (fromEn && EN_TO_JA_OVERRIDE[slug]) jaTarget = jaRoot + EN_TO_JA_OVERRIDE[slug];

    links.forEach(function (a) {
      var lang = a.getAttribute("hreflang");
      if (lang === "en") a.setAttribute("href", enTarget);
      else if (lang === "ja") a.setAttribute("href", jaTarget);
    });
  }

  // Material の instant loading が将来有効化されても動くよう document$ があれば購読する。
  if (typeof window.document$ !== "undefined" && window.document$.subscribe) {
    window.document$.subscribe(retarget);
  } else if (document.readyState !== "loading") {
    retarget();
  } else {
    document.addEventListener("DOMContentLoaded", retarget);
  }
})();
