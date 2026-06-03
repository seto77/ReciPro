#!/usr/bin/env python3
# 260603Cl 追加: 案X 書出し器。既存 E_ZZ.TXT (101ブロック, 50eV-20keV) はそのまま保持し、
# 20keV超の DCS 由来ブロック (blockIndex 101-110 = 21.23..36.41 keV, log継ぎ足し) を追加して
# 拡張版 E_ZZ.TXT (111ブロック) を出力する。
#
# 既存ブロック (0-100) は行を一字一句コピーするので、既存 EBSD 結果は完全に不変。
# 新ブロックは: block番号(連番) + SigmaA0Squared(NIST 公表値=CSVヘッダ Total cross section) + 2001点CDF。
#   σ_el は NIST 公表値を使う (自前積分でなく)。CDF は DCS 形状から末尾1正規化 (nist_dcs_lib)。
#
# 使い方 (Phase A 検証後の本番):
#   python build_dcs_blocks.py 6 13 14 26 29 79 --dcs-dir ..\DCS --in-dir ..\Original --out-dir ..\Original_ext

from __future__ import annotations

import argparse
import sys
from pathlib import Path

sys.path.insert(0, str(Path(__file__).resolve().parent))
import nist_dcs_lib as lib  # noqa: E402

NEW_BLOCK_START = 101  # blockIndex (0-based). 既存は 0..100
NEW_BLOCK_END = 110    # inclusive -> 10 ブロック追加, 計 111


def _fmt(x: float) -> str:
    """既存 E_*.TXT と同じ %.11E 形式 (例 4.12409706979E-01)。"""
    return f"{x:.11E}"


def build_one(z: int, dcs_dir: Path, in_dir: Path, out_dir: Path) -> str:
    src = in_dir / f"E_{z:02d}.TXT"
    if not src.exists():
        return f"Z{z:02d}: 既存 {src.name} が無い -> skip"
    lines = src.read_text().split("\n")

    # 既存 101 ブロック (0..100) を行コピー
    out_lines: list[str] = []
    n_existing = (len(lines)) // lib.BLOCK_LINES
    if n_existing < 101:
        return f"Z{z:02d}: 既存ブロック数 {n_existing} < 101 -> skip"
    for b in range(101):
        i = b * lib.BLOCK_LINES
        out_lines.extend(lines[i:i + lib.BLOCK_LINES])

    # 新ブロック 101..110 を DCS から生成
    for b in range(NEW_BLOCK_START, NEW_BLOCK_END + 1):
        e_kev = lib.legacy_energy_ev(b) / 1000.0
        # fetch スクリプトの命名 f"DCS_Z{z:02d}_E{energy_kev:g}keV.csv" に合わせる
        dcs_csv = dcs_dir / f"DCS_Z{z:02d}_E{e_kev:g}keV.csv"
        if not dcs_csv.exists():
            return f"Z{z:02d}: DCS {dcs_csv.name} (blockIndex {b}, {e_kev:.4f}keV) が無い -> skip"
        theta, dcs, sigma_hdr, _ = lib.read_dcs_csv(dcs_csv)
        _, _, phi = lib.dcs_to_cdf(theta, dcs)
        out_lines.append(str(b + 1))        # block番号 (パーサは読み飛ばす)
        out_lines.append(_fmt(sigma_hdr))   # SigmaA0Squared = NIST 公表値
        out_lines.extend(_fmt(p) for p in phi)

    out_dir.mkdir(parents=True, exist_ok=True)
    (out_dir / f"E_{z:02d}.TXT").write_text("\n".join(out_lines) + "\n")
    return f"Z{z:02d}: OK -> {NEW_BLOCK_END + 1} blocks (既存101 + 新{NEW_BLOCK_END - NEW_BLOCK_START + 1})"


def main() -> int:
    here = Path(__file__).resolve().parent.parent  # Atom/NistElastic
    ap = argparse.ArgumentParser(description="案X: 既存E_*.TXTに20keV超DCSブロックを継ぎ足す。")
    ap.add_argument("atomic_numbers", nargs="+", type=int)
    ap.add_argument("--dcs-dir", default=str(here / "DCS"))
    ap.add_argument("--in-dir", default=str(here / "Original"))
    ap.add_argument("--out-dir", default=str(here / "Original_ext"))
    args = ap.parse_args()

    for z in sorted(dict.fromkeys(args.atomic_numbers)):
        print(build_one(z, Path(args.dcs_dir), Path(args.in_dir), Path(args.out_dir)))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
