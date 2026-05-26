// 260525Cl: ヘッダーの言語セレクタ (extra.alternate) を「現在ページの相手言語版」へ向け直す。
// extra.alternate のリンクは静的 (英語=/ReciPro/, 日本語=/ReciPro/ja/) なので、既定では言語を切り替えると
// 各言語のトップページへ戻ってしまう。URL 構造が EN=<base>en/<slug>/ ・ JA=<base>ja/<slug>/ と予測可能なため、
// 現在ページの slug から相手言語の対応ページ URL を組み立てる。
//
// 260525Cl: 2 系統で確実に効かせる:
//   (1) ロード時に href を相手言語ページへ書き換える (中クリック/新規タブで開く場合に正しい URL になる)。
//   (2) クリックを横取りし、毎回その場で現在 location から遷移先を計算して飛ぶ。
//       href 書き換えがタイミングやヘッダー再描画の都合で間に合わなかった場合の保険。
//   ルート (/ReciPro/, /ReciPro/ja/) は書き換え前に一度だけ捕捉し、以後は保持値を使う
//   (href 書き換え後に再実行されても original ルートを失わないため)。
(function () {
  "use strict";

  // 相手言語に 1:1 の対応ページが無いケースの上書き (EN のみ存在するページ → JA 側の代替ページ slug)
  // 260526Cl: メインウィンドウは EN/JA とも単一ページ 0-main-window/ に統合され 1:1 対応。現状は上書き無し。
  var EN_TO_JA_OVERRIDE = {};

  var enRoot = "", jaRoot = "", rootsReady = false;

  // 言語セレクタの静的リンクから両言語のルート URL を一度だけ捕捉する。
  function ensureRoots() {
    if (rootsReady) return true;
    document.querySelectorAll("a.md-select__link[hreflang]").forEach(function (a) {
      var lang = a.getAttribute("hreflang");
      if (lang === "en" && !enRoot) enRoot = a.getAttribute("href");      // 例 "/ReciPro/"
      else if (lang === "ja" && !jaRoot) jaRoot = a.getAttribute("href"); // 例 "/ReciPro/ja/"
    });
    rootsReady = !!(enRoot && jaRoot);
    return rootsReady;
  }

  // 現在ページに対応する、行き先言語 lang ("en"|"ja") の URL を返す。判定不能なら null。
  function targetFor(lang) {
    if (!ensureRoots()) return null;

    var enPrefix = enRoot + "en/";   // 例 "/ReciPro/en/"
    var path = window.location.pathname;

    // 現在ページの slug (末尾スラッシュ込み) を判定する。
    var slug;
    if (path === enRoot || path === jaRoot) slug = "";                 // どちらかのトップ
    else if (path.indexOf(jaRoot) === 0) slug = path.slice(jaRoot.length); // JA ページ
    else if (path.indexOf(enPrefix) === 0) slug = path.slice(enPrefix.length); // EN ページ
    else return null; // 想定外のパスは触らない

    if (lang === "en") return slug === "" ? enRoot : enPrefix + slug;

    // 日本語側: EN にしか無いページからの切替は代替先 slug に差し替える。
    var jaSlug = (EN_TO_JA_OVERRIDE[slug] != null) ? EN_TO_JA_OVERRIDE[slug] : slug;
    return jaSlug === "" ? jaRoot : jaRoot + jaSlug;
  }

  // (1) ロード時に href を相手言語ページへ向け直す。
  function retarget() {
    var en = targetFor("en"), ja = targetFor("ja");
    document.querySelectorAll("a.md-select__link[hreflang]").forEach(function (a) {
      var lang = a.getAttribute("hreflang");
      if (lang === "en" && en) a.setAttribute("href", en);
      else if (lang === "ja" && ja) a.setAttribute("href", ja);
    });
  }

  // (2) クリックを横取りして、その場で計算した URL へ遷移する (タイミング/再描画に依存しない保険)。
  document.addEventListener("click", function (ev) {
    var link = ev.target && ev.target.closest && ev.target.closest("a.md-select__link[hreflang]");
    if (!link) return;
    var lang = link.getAttribute("hreflang");
    if (lang !== "en" && lang !== "ja") return;
    var t = targetFor(lang);
    if (!t) return; // 計算不能なら静的ルートの既定動作に任せる
    ev.preventDefault();
    window.location.href = t;
  });

  // Material の instant loading が将来有効化されても動くよう document$ があれば購読する。
  if (typeof window.document$ !== "undefined" && window.document$.subscribe) {
    window.document$.subscribe(retarget);
  } else if (document.readyState !== "loading") {
    retarget();
  } else {
    document.addEventListener("DOMContentLoaded", retarget);
  }
})();
