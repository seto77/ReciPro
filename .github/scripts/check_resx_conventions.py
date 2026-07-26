#!/usr/bin/env python3
# 260726Cl 追加: CI 用の resx 規約チェッカ (検出専用・リポを書き換えない)。
#
# 経緯: .github/workflows/i18n-resx-check.yml は tools/check_resx_textonly.py などを呼んでいたが、
#   tools/ はメインリポから exclude された独立ローカルリポで CI の checkout に入らない。
#   workflow は「スクリプトが無ければ notice を出して成功」だったため、**3 検査すべてが毎回
#   スキップされ、名前だけ緑になる no-op** だった (2026-07-26 に発覚)。
#   修復ツール (--fix) はローカルの tools/ に置いたまま、検出だけを追跡パスの本スクリプトが担う。
#
# 検査する不変条件 (実際に事故が起きたものだけに絞る):
#   (1) culture resx は text-only。VS デザイナで Language≠(Default) のまま保存するとレイアウト
#       (Size/Location/Font 等) が culture resx へ焼き込まれ、「言語ごとにレイアウトを保守する」
#       退行が起きる。ja だけは Font を許容する (作者方針: 日本語デザイナ表示 == 実行時)。
#   (2) neutral resx に $this.Language が焼き付いていない。これが入ると ja が凍結し、
#       フォントが動く事故になる。
#   (3) neutral resx の Font サイズが 5 段階ティア上にある (デザイナ == 実行時)。
#       ただし役割フォント (Times New Roman / Courier New / Segoe UI Symbol / Tahoma 等) は
#       実行時にティア化されないので対象外。
#
# 対象言語は Crystallography/Localization/SupportedCultures.cs から導出する (**fail-closed**:
#   読めなければ落とす。ハードコード一覧へ黙って落ちると、言語を足しても検査されない)。
#
# usage: python .github/scripts/check_resx_conventions.py [--root <dir>]
# exit:  0 = 違反なし / 1 = 違反あり / 2 = 検査を実行できない (culture 一覧が読めない等)

import argparse
import os
import re
import sys
import xml.etree.ElementTree as ET

try:
    sys.stdout.reconfigure(encoding="utf-8")
except Exception:
    pass

# tools/check_resx_textonly.py の KEEP_EXACT / KEEP_SUFFIXED_RE と一致させること。
KEEP_EXACT = {"Text", "HeaderText", "FooterText", "AccessibleName", "AccessibleDescription"}
KEEP_SUFFIXED_RE = re.compile(r"^(?:Items|ToolTip|ToolTipText)\d*$")

# UiFont.IsUiBodyFont と一致させること。これ以外は役割フォントでティア対象外。
UI_BODY_FAMILIES = {
    "Segoe UI", "Yu Gothic UI", "Microsoft YaHei UI", "Microsoft JhengHei UI", "Malgun Gothic",
}
TIERS = (7.0, 8.25, 9.0, 9.75, 13.0)
FONT_RE = re.compile(r"^\s*([^,]+?)\s*,\s*([0-9.]+)\s*pt", re.IGNORECASE)

SKIP_DIRS = {"obj", "bin", ".git", "site", "node_modules"}


def load_cultures(root: str):
    """SupportedCultures.cs の new("xx", …) から culture 名を取る (en 除外)。読めなければ None。"""
    path = os.path.join(root, "Crystallography", "Localization", "SupportedCultures.cs")
    if not os.path.isfile(path):
        return None, f"SupportedCultures.cs が見つかりません: {path}"
    try:
        text = open(path, encoding="utf-8-sig").read()
    except Exception as e:
        return None, f"SupportedCultures.cs を読めません: {e}"
    names = [n for n in re.findall(r'new\(\s*"([^"]+)"', text) if n.lower() != "en"]
    if not names:
        return None, "SupportedCultures.cs から culture を 1 つも抽出できませんでした (書式が変わった?)"
    return names, None


def prop_of(name: str) -> str:
    return name.rsplit(".", 1)[-1] if "." in name else name


def iter_resx(root: str):
    for dirpath, dirnames, filenames in os.walk(root):
        dirnames[:] = [d for d in dirnames if d not in SKIP_DIRS]
        for fn in filenames:
            if fn.endswith(".resx"):
                yield os.path.join(dirpath, fn)


def culture_of(path: str, cultures):
    """`Form.ja.resx` → 'ja' / `Form.resx` → None"""
    stem = os.path.basename(path)[: -len(".resx")]
    for c in cultures:
        if stem.endswith("." + c):
            return c
    return None


def check_culture_resx(path: str, culture: str, violations):
    try:
        root = ET.parse(path).getroot()
    except ET.ParseError as e:
        violations.append((path, f"XML パース失敗: {e}"))
        return
    for el in root:
        if el.tag not in ("data", "metadata"):
            continue
        name = el.get("name") or ""
        if el.tag == "metadata":
            violations.append((path, f"metadata '{name}' (culture resx はデザイナメタを持たない)"))
            continue
        if name.startswith(">>"):
            violations.append((path, f"デザイナ階層メタ '{name}'"))
            continue
        p = prop_of(name)
        if p in KEEP_EXACT or KEEP_SUFFIXED_RE.match(p):
            continue
        if p == "Font" and culture == "ja":
            continue  # ja のみ Font を許容 (値の正規化はローカル tools が担当)
        violations.append((path, f"レイアウト/非文字列プロパティ '{name}' (text-only 違反)"))


def check_neutral_resx(path: str, violations):
    try:
        root = ET.parse(path).getroot()
    except ET.ParseError as e:
        violations.append((path, f"XML パース失敗: {e}"))
        return
    for el in root:
        if el.tag != "data":
            continue
        name = el.get("name") or ""
        if name == "$this.Language":
            violations.append((path, "$this.Language が焼き付いています (デザイナで Language≠Default のまま保存された)"))
            continue
        if prop_of(name) != "Font":
            continue
        value_el = el.find("value")
        if value_el is None or not value_el.text:
            continue
        m = FONT_RE.match(value_el.text)
        if not m:
            continue
        family, pt = m.group(1).strip(), float(m.group(2))
        if family not in UI_BODY_FAMILIES:
            continue  # 役割フォント (Courier New 等) は実行時にティア化されない
        if not any(abs(pt - t) < 1e-6 for t in TIERS):
            violations.append((path, f"'{name}' の Font サイズ {pt}pt が 5 段階ティア {TIERS} 上にありません"))


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--root", default=None, help="ReciPro リポのルート (既定 = このスクリプトの 2 階層上)")
    args = ap.parse_args()

    root = args.root or os.path.dirname(os.path.dirname(os.path.dirname(os.path.abspath(__file__))))

    cultures, err = load_cultures(root)
    if cultures is None:
        print(f"FATAL: {err}", file=sys.stderr)
        print("       検査対象言語を確定できないため、黙って一部だけ検査せず異常終了します。", file=sys.stderr)
        return 2

    violations = []
    n_culture = n_neutral = 0
    for path in iter_resx(root):
        c = culture_of(path, cultures)
        if c:
            n_culture += 1
            check_culture_resx(path, c, violations)
        else:
            n_neutral += 1
            check_neutral_resx(path, violations)

    for path, msg in violations:
        rel = os.path.relpath(path, root).replace("\\", "/")
        print(f"::error file={rel}::{msg}")

    print(f"\ncultures: {','.join(cultures)}")
    print(f"検査: culture resx {n_culture} 件 / neutral resx {n_neutral} 件  →  違反 {len(violations)} 件")
    if violations:
        print("修復はローカルで: python tools/check_resx_textonly.py --fix / "
              "python tools/snap_neutral_font_tiers.py --fix / python tools/restore_ja_fonts.py --fix")
    return 1 if violations else 0


sys.exit(main())
