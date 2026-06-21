# 260622Cl: 旧英語マニュアル URL (/ReciPro/en/<slug>/) を、static-i18n 移行後の
# 新ルート URL (/ReciPro/<slug>/) へ逃がす post-build フック。
#
# 背景: 多言語化で mkdocs-static-i18n を導入し、default locale (en) をサイトルートへ出すようにした。
#   これにより英語ページの公開 URL が /ReciPro/en/<slug>/ → /ReciPro/<slug>/ へ移動する。
#   旧 /en/ URL は約1ヶ月公開済みで、アプリ F1 ヘルプ・ブックマーク・検索エンジン索引が指している。
#
# なぜ mkdocs-redirects でなくフックか (実測で確定, 2026-06-22):
#   mkdocs-redirects の redirect target URL 算出は static-i18n の「再ホーム前」の名前空間に固定されており、
#   value `en/foo.md` は死 URL /en/foo/ に解決される (KEY==VALUE では url=./ の自己参照ループになる)。
#   → 英語の /en/→ルート 互換は mkdocs-redirects では張れない。JA リネーム履歴 (/ja/旧→/ja/新) は
#     JA が /ja/ のままなので従来どおり mkdocs-redirects が担当する (mkdocs.yml の redirect_maps)。
#
# 本フックの責務は「過去の英語 URL 互換の静的 meta-refresh stub 生成」だけ。i18n/fallback/build は
# プラグインに任せる。詳細は .project-guidance/ReciPro_Pages多言語化計画.md §3.2。

from pathlib import Path
import html
import os

import mkdocs.plugins

# 旧英語リネーム履歴: /ReciPro/en/<旧slug>/ → /ReciPro/<新slug>/ (新 slug はディレクトリ形・拡張子なし)。
# 旧 mkdocs.yml の redirect_maps の en/ 系エントリを最終ターゲットへ解決したもの。
# (現行ページの /en/<slug>/ → /<slug>/ は下の自動生成が全ページぶん面倒を見るので、ここには「現存しない旧 slug」だけ。)
LEGACY_EN_RENAMES = {
    "0-1-crystal-orientation-control": "0-main-window",
    "0-main-window/1-crystal-orientation-control": "0-main-window",
    "11-symmetry-information": "2-symmetry-information",
    "12-scattering-factor": "3-beam-interaction",
    "3-scattering-factor": "3-beam-interaction",
    "3-rotation-geometry": "4-rotation-geometry",
    "7-1-saed-simulation": "7-diffraction-simulator/1-saed-simulation",
    "7-2-x-ray-diffraction": "7-diffraction-simulator/4-x-ray-neutron-diffraction",
    "7-3-ped-simulation": "7-diffraction-simulator/2-ped-simulation",
    "7-4-cbed-simulation": "7-diffraction-simulator/3-cbed-simulation",
    "7-diffraction-simulator/2-x-ray-diffraction": "7-diffraction-simulator/4-x-ray-neutron-diffraction",
    "7-diffraction-simulator/1-x-ray-diffraction": "7-diffraction-simulator/4-x-ray-neutron-diffraction",
    "7-diffraction-simulator/2-saed-simulation": "7-diffraction-simulator/1-saed-simulation",
    "7-diffraction-simulator/3-ped-simulation": "7-diffraction-simulator/2-ped-simulation",
    "7-diffraction-simulator/4-cbed-simulation": "7-diffraction-simulator/3-cbed-simulation",
    "8-1-hrtem-simulation": "9-hrtem-stem-simulator/1-hrtem-simulation",
    "8-2-stem-simulation": "9-hrtem-stem-simulator/2-stem-simulation",
    "8-3-potential-simulation": "9-hrtem-stem-simulator/3-potential-simulation",
    "8-hrtem-stem-simulator": "9-hrtem-stem-simulator",
    "13-electron-trajectory": "8-electron-trajectory",
    "10-1-spot-id-v2": "11-spot-id-v2",
    "14-ebsd-simulation": "12-ebsd-simulation",
    "20-1-built-in-functions": "20-macro/1-built-in-functions",
    "20-2-examples": "20-macro/2-examples",
    "appendix-a0-how-to-use": "appendix/a0-how-to-use",
    "appendix-a1-coordinate-system": "appendix/a1-coordinate-system/1-orientation",
    "appendix/a1-coordinate-system": "appendix/a1-coordinate-system/1-orientation",
    "appendix-a2-detector-coordinate-system": "appendix/a1-coordinate-system/2-diffraction",
    "appendix/a2-detector-coordinate-system": "appendix/a1-coordinate-system/2-diffraction",
    "appendix/a2-bloch-wave-method": "appendix/a3-bloch-wave",
    "appendix/a3-bloch-wave-calculation": "appendix/a3-bloch-wave/calculation",
    "appendix/a2-bloch-wave": "appendix/a3-bloch-wave",
    "appendix/a2-bloch-wave/calculation": "appendix/a3-bloch-wave/calculation",
    "appendix/a2-bloch-wave/hrtem": "appendix/a3-bloch-wave/hrtem",
    "appendix/a2-bloch-wave/cbed": "appendix/a3-bloch-wave/cbed",
    "appendix/a2-bloch-wave/stem": "appendix/a3-bloch-wave/stem",
    "appendix/a2-bloch-wave/ebsd": "appendix/a3-bloch-wave/ebsd",
}

# サイトルート直下で、言語ディレクトリや生成物として扱う (= 英語ページではない) トップ階層。
_RESERVED_TOP = {"en", "ja", "assets", "search"}

_STUB = (
    "<!doctype html>\n"
    '<html lang="en"><head><meta charset="utf-8">\n'
    '<meta http-equiv="refresh" content="0; url={url}">\n'
    '<link rel="canonical" href="{url}">\n'
    "<script>location.replace({url_js}+location.search+location.hash)</script>\n"
    '<title>Redirecting…</title></head><body>\n'
    '<a href="{url}">This page has moved. Redirecting…</a>\n'
    "</body></html>\n"
)


@mkdocs.plugins.event_priority(-100)  # 他プラグイン (mkdocs-redirects 等) の on_post_build の後に走らせる
def on_post_build(config):
    site = Path(config.site_dir).resolve()
    en_dir = site / "en"

    def write_redirect(source_slug, target_slug):
        dest_dir = (en_dir / source_slug) if source_slug else en_dir
        target_dir = (site / target_slug) if target_slug else site
        target_index = target_dir / "index.html"
        if not target_index.exists():
            raise RuntimeError(f"legacy_en_redirects: target missing for /en/{source_slug}/ -> /{target_slug}/")

        dest_index = dest_dir / "index.html"
        rel = os.path.relpath(target_dir, dest_dir).replace("\\", "/")
        if rel == ".":
            # 自己参照 (無限リロード) を絶対に作らない。方針 §5 の foo.md→foo/index.md 罠の一般化。
            raise RuntimeError(f"legacy_en_redirects: self-loop for /en/{source_slug}/")
        url = (rel.rstrip("/") + "/") if rel != ".." else "../"
        dest_dir.mkdir(parents=True, exist_ok=True)
        dest_index.write_text(
            _STUB.format(url=html.escape(url), url_js=repr(url)), encoding="utf-8"
        )

    # (1) 現行の全英語ルートページ: /en/<slug>/ → /<slug>/ (将来追加ページも自動で互換 alias を持つ)。
    written = 0
    for index in sorted(site.rglob("index.html")):
        rel = index.relative_to(site)
        if rel.parts[0] in _RESERVED_TOP:
            continue
        slug = "" if rel.parts == ("index.html",) else "/".join(rel.parts[:-1])
        write_redirect(slug, slug)
        written += 1

    # (2) 旧英語リネーム履歴: /en/<旧slug>/ → /<新slug>/。
    for old_slug, new_slug in LEGACY_EN_RENAMES.items():
        write_redirect(old_slug, new_slug)

    mkdocs.plugins.log.info(
        "legacy_en_redirects: wrote %d current + %d legacy /en/ redirect stubs",
        written, len(LEGACY_EN_RENAMES),
    )
