#!/usr/bin/env python3
# 260603Cl 追加: NIST SRD 64 から DCS (微分弾性散乱断面積 dσ/dΩ vs θ) を取得するスクリプト。
#
# 背景:
#   既存の fetch_nist_elastic_sampler.py は NIST の "sampler" 機能 (2001点 CDF 済み) を
#   ダウンロードしていたが、sampler は 50 eV-20 keV 限定。30 keV 拡張には DCS (300 keV まで提供)
#   を取得して自前で CDF 化する必要がある (../README.md の §5 参照)。
#   本スクリプトはその DCS 取得を担う。出力 CSV を DCS->CDF 変換器に渡す。
#
# 重要 (UI の不確実性):
#   NIST SRD64 の DCS ページは ASP.NET MVC の動的フォームで、Display/Download ボタンは
#   JS で生成される。本スクリプトのセレクタは「ラベル/テキスト/ロール」ベースの推定であり、
#   サイト更新で変わり得る。最初は必ず
#       python fetch_nist_elastic_dcs.py 14 --energies 20 --debug --headful
#   のように 1 元素 1 エネルギーで --debug --headful 実行し、保存される
#   debug_*.png / debug_*.html を見てセレクタを確定・調整すること。
#   確定後に Z=1..96 へ広げる。

from __future__ import annotations

import argparse
import re
import sys
import time
from pathlib import Path

from playwright.sync_api import Page, TimeoutError as PWTimeout, sync_playwright

# DCS 取得先。{Z} は原子番号。(WebFetch で確認: Energy 入力欄 + eV/keV ラジオ + 座標系3種)
BASE_URL = "https://srdata.nist.gov/srd64/Elastic/SelInitEnergy/{Z}"

# 座標系。20 keV 超は "dσ/dΩ vs θ" のみ選択可 (NIST 仕様)。
COORD_DSIGMA_DOMEGA = "dσ/dΩ vs θ"

DEFAULT_OUTPUT_DIR = Path(__file__).resolve().parent.parent / "DCS"  # Atom/NistElastic/DCS/


def parse_args() -> argparse.Namespace:
    p = argparse.ArgumentParser(description="Download NIST SRD64 elastic DCS (dsigma/dOmega vs theta).")
    p.add_argument("atomic_numbers", nargs="+", type=int, help="Atomic numbers (1..96).")
    p.add_argument("--energies", nargs="+", type=float, default=[20.0],
                   help="Electron energies in keV (default: 20). e.g. --energies 20 21 22 ... 30")
    p.add_argument("--output-dir", default=str(DEFAULT_OUTPUT_DIR), help="Destination for DCS_ZNN_E__keV.csv.")
    p.add_argument("--force", action="store_true", help="Re-download even if the CSV already exists.")
    p.add_argument("--retries", type=int, default=3, help="Retry count per (Z, energy).")
    p.add_argument("--headful", action="store_true", help="Show the browser (disable headless) for debugging.")
    p.add_argument("--debug", action="store_true", help="Save debug_*.png / debug_*.html at each step.")
    p.add_argument("--slowmo", type=int, default=0, help="Playwright slow_mo in ms (debugging).")
    return p.parse_args()


def _dump(page: Page, out_dir: Path, tag: str, debug: bool) -> None:
    """--debug 時に現在ページのスクショと HTML を保存し、セレクタ調整の材料にする。"""
    if not debug:
        return
    out_dir.mkdir(parents=True, exist_ok=True)
    try:
        page.screenshot(path=str(out_dir / f"debug_{tag}.png"), full_page=True)
        (out_dir / f"debug_{tag}.html").write_text(page.content(), encoding="utf-8")
        print(f"  [debug] saved debug_{tag}.png / .html")
    except Exception as exc:  # noqa: BLE001
        print(f"  [debug] dump failed ({tag}): {exc}")


def _first_locator(page: Page, candidates: list):
    """候補ロケータ群を順に試し、最初に存在するものを返す。UI 変化への保険。"""
    for make in candidates:
        try:
            loc = make()
            if loc.count() > 0:
                return loc.first
        except Exception:  # noqa: BLE001
            continue
    return None


def fetch_one(page: Page, out_dir: Path, z: int, energy_kev: float, debug: bool) -> bool:
    """1 つの (Z, energy) について DCS をダウンロードして CSV 保存。成功で True。"""
    url = BASE_URL.format(Z=z)
    page.goto(url, wait_until="domcontentloaded", timeout=60000)
    page.wait_for_timeout(2500)
    _dump(page, out_dir, f"Z{z:02d}_E{energy_kev:g}_1_initial", debug)

    # --- (1) エネルギー入力欄 ---  ラベル "Energy" / テキスト入力を推定で探す
    energy_box = _first_locator(page, [
        lambda: page.get_by_label(re.compile("energy", re.I)),
        lambda: page.locator("input[name*='nergy']"),
        lambda: page.locator("input[type='text']"),
    ])
    if energy_box is None:
        print(f"  [Z{z:02d} {energy_kev:g}keV] energy input not found")
        _dump(page, out_dir, f"Z{z:02d}_E{energy_kev:g}_ERR_no_energy", True)
        return False
    energy_box.fill(str(energy_kev))

    # --- (2) 単位ラジオ keV ---
    kev_radio = _first_locator(page, [
        lambda: page.get_by_label("keV", exact=True),
        lambda: page.locator("input[type='radio'][value='keV']"),
        lambda: page.get_by_text(re.compile(r"^keV$")),
    ])
    if kev_radio is not None:
        try:
            kev_radio.check()
        except Exception:
            try:
                kev_radio.click()
            except Exception:
                pass

    # --- (3) 座標系 dσ/dΩ vs θ (20 keV 超はこれのみ) ---
    coord = _first_locator(page, [
        lambda: page.get_by_text("dσ/dΩ", exact=False),
        lambda: page.get_by_text(re.compile(r"d\s*Ω", re.I)),
    ])
    if coord is not None:
        try:
            coord.click()
        except Exception:
            pass

    _dump(page, out_dir, f"Z{z:02d}_E{energy_kev:g}_2_filled", debug)

    # --- (4) Display / Submit ボタン ---
    display_btn = _first_locator(page, [
        lambda: page.get_by_role("button", name=re.compile("display", re.I)),
        lambda: page.get_by_role("link", name=re.compile("display", re.I)),
        lambda: page.locator("input[type='submit']"),
        lambda: page.get_by_text(re.compile(r"^\s*Display\s*$", re.I)),
    ])
    if display_btn is None:
        print(f"  [Z{z:02d} {energy_kev:g}keV] Display button not found")
        _dump(page, out_dir, f"Z{z:02d}_E{energy_kev:g}_ERR_no_display", True)
        return False
    display_btn.click()
    page.wait_for_timeout(6000)  # 260603Cl 高Z重元素は計算が重く View Data 生成が遅い -> 3500->6000
    _dump(page, out_dir, f"Z{z:02d}_E{energy_kev:g}_3_displayed", debug)

    # --- (5) View Data -> Text/CSV Data File でダウンロード ---
    # NIST SRD64 は Radzen.Blazor 製。フローは 2 段階:
    #   (5a) "View Data" クリックでデータテーブルと "Text/CSV Data File" ボタンを展開
    #        (クリック後 View Data 自体は disabled になる)
    #   (5b) 展開後に出る "CSV Data File" (or "Text Data File") をクリック
    #        -> JS BlazorDownloadFile() が blob を a.download で保存させる
    view_btn = _first_locator(page, [
        lambda: page.get_by_role("button", name=re.compile(r"view\s*data", re.I)),
        lambda: page.get_by_text(re.compile(r"view\s*data", re.I)),
    ])
    if view_btn is None:
        print(f"  [Z{z:02d} {energy_kev:g}keV] 'View Data' button not found")
        _dump(page, out_dir, f"Z{z:02d}_E{energy_kev:g}_ERR_no_viewdata", True)
        return False
    try:
        view_btn.click()
    except Exception:
        pass
    page.wait_for_timeout(5000)  # 260603Cl データテーブル/ダウンロードボタン展開待ち -> 3000->5000
    _dump(page, out_dir, f"Z{z:02d}_E{energy_kev:g}_4_viewdata", debug)

    # 参考: ページ表示の Total Cross Section [a0^2] をログ (検証ゲート用 sigma_el)
    try:
        tcs = page.get_by_text(re.compile(r"Total Cross Section", re.I)).first
        print(f"  [Z{z:02d} {energy_kev:g}keV] {tcs.inner_text().strip()[:80]}")
    except Exception:
        pass

    dest = out_dir / f"DCS_Z{z:02d}_E{energy_kev:g}keV.csv"
    try:
        with page.expect_download(timeout=60000) as dl_info:
            dl_btn = _first_locator(page, [
                lambda: page.get_by_text(re.compile(r"CSV\s*Data\s*File", re.I)),
                lambda: page.get_by_text(re.compile(r"Text\s*Data\s*File", re.I)),
                lambda: page.get_by_role("button", name=re.compile(r"csv|text\s*data", re.I)),
            ])
            if dl_btn is None:
                raise RuntimeError("'CSV/Text Data File' button not found")
            dl_btn.click()
        download = dl_info.value
        out_dir.mkdir(parents=True, exist_ok=True)
        download.save_as(str(dest))
        # 取得検証: ヘッダ Energy/Atomic number が要求と一致するか (Blazor状態引きずり/fill失敗の検出)
        txt = dest.read_text(encoding="utf-8", errors="ignore")
        hm = re.search(r"Energy:\s*([0-9.]+)\s*eV", txt)
        zm = re.search(r"Atomic number:\s*([0-9]+)", txt)
        got_ev = float(hm.group(1)) if hm else -1.0
        got_z = int(zm.group(1)) if zm else -1
        want_ev = energy_kev * 1000.0
        if abs(got_ev - want_ev) > max(1.0, want_ev * 0.005) or got_z != z:
            print(f"  [Z{z:02d} {energy_kev:g}keV] MISMATCH got Z={got_z} E={got_ev}eV "
                  f"want Z={z} E={want_ev:.0f}eV -> discard & retry")
            dest.unlink()
            return False
        print(f"  saved Z{z:02d} {energy_kev:g}keV -> {dest.name} (E={got_ev:.0f}eV OK)")
        return True
    except (PWTimeout, RuntimeError) as exc:
        print(f"  [Z{z:02d} {energy_kev:g}keV] download failed: {exc}")
        _dump(page, out_dir, f"Z{z:02d}_E{energy_kev:g}_ERR_no_download", True)
        return False


def main() -> int:
    args = parse_args()
    out_dir = Path(args.output_dir)

    atomic_numbers = sorted(dict.fromkeys(args.atomic_numbers))
    invalid = [z for z in atomic_numbers if z < 1 or z > 96]
    if invalid:
        raise SystemExit(f"Unsupported atomic numbers: {invalid}. Expected 1..96.")

    failures: list[str] = []
    with sync_playwright() as pw:
        browser = pw.chromium.launch(headless=not args.headful, slow_mo=args.slowmo)
        context = browser.new_context(accept_downloads=True)
        page = context.new_page()
        for z in atomic_numbers:
            for energy_kev in args.energies:
                dest = out_dir / f"DCS_Z{z:02d}_E{energy_kev:g}keV.csv"
                if dest.exists() and not args.force:
                    print(f"skip Z{z:02d} {energy_kev:g}keV -> {dest.name}")
                    continue
                ok = False
                for attempt in range(1, max(args.retries, 1) + 1):
                    try:
                        ok = fetch_one(page, out_dir, z, energy_kev, args.debug)
                        if ok:
                            break
                    except Exception as exc:  # noqa: BLE001
                        print(f"  retry Z{z:02d} {energy_kev:g}keV {attempt}/{args.retries}: {exc}")
                    time.sleep(2)
                if not ok:
                    failures.append(f"Z{z:02d}@{energy_kev:g}keV")
        browser.close()

    if failures:
        print(f"\nFAILED ({len(failures)}): {', '.join(failures)}", file=sys.stderr)
        return 1
    print("\nAll downloads OK.")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
