#!/usr/bin/env python3
# 260603Cl 追加: 書出し結果(Original_ext/E_*.TXT, 111ブロック)を全96元素で検証する。
# (1) 既存101ブロック(50eV-20keV)が Original と一字一句一致(既存不変) (2) ブロック数=111
# (3) 新10ブロック(101-110)の健全性: σ_el>0, CDF端点(Φ[0]=0,Φ[-1]=1), 単調非減少。
#
#   python verify_extended.py

from __future__ import annotations

import sys
from pathlib import Path

import numpy as np

sys.path.insert(0, str(Path(__file__).resolve().parent))
import nist_dcs_lib as lib  # noqa: E402


def main() -> int:
    here = Path(__file__).resolve().parent.parent
    ext_dir = here / "Original_ext"
    orig_dir = here / "Original"

    issues = 0
    checked = 0
    for z in range(1, 97):
        ext = ext_dir / f"E_{z:02d}.TXT"
        orig = orig_dir / f"E_{z:02d}.TXT"
        if not ext.exists():
            print(f"Z{z:02d}: Original_ext が無い"); issues += 1; continue
        if not orig.exists():
            print(f"Z{z:02d}: Original が無い"); issues += 1; continue
        checked += 1
        el = ext.read_text().split("\n")
        ol = orig.read_text().split("\n")

        # ブロック数
        nblk = sum(1 for i in range(0, len(el) - lib.BLOCK_LINES + 1, lib.BLOCK_LINES)
                   if el[i].strip().isdigit())
        nblk_ext = (len([x for x in el if x != ""])) // lib.BLOCK_LINES
        if nblk_ext != 111:
            print(f"Z{z:02d}: ブロック数 {nblk_ext} != 111"); issues += 1

        # 既存101ブロック一致 (先頭 101*2003 行)
        head_n = 101 * lib.BLOCK_LINES
        if el[:head_n] != ol[:head_n]:
            # 最初の不一致行を探す
            for k in range(min(head_n, len(el), len(ol))):
                if el[k] != ol[k]:
                    print(f"Z{z:02d}: 既存ブロック不一致 line {k}: ext='{el[k][:20]}' orig='{ol[k][:20]}'")
                    break
            issues += 1

        # 新ブロック 101-110 健全性
        for b in range(101, 111):
            sigma, phi = lib.read_etxt_block(ext, b)
            if not (sigma > 0):
                print(f"Z{z:02d} blk{b}: sigma={sigma} <= 0"); issues += 1
            if abs(phi[0]) > 1e-9 or abs(phi[-1] - 1.0) > 1e-6:
                print(f"Z{z:02d} blk{b}: endpoint Phi[0]={phi[0]:.2e} Phi[-1]={phi[-1]:.6f}"); issues += 1
            if not np.all(np.diff(phi) >= -1e-12):
                bad = int(np.argmin(np.diff(phi)))
                print(f"Z{z:02d} blk{b}: not monotonic at j={bad} (d={np.diff(phi)[bad]:.2e})"); issues += 1

    print(f"\ndone. {checked} elements checked, issues={issues}")
    return 1 if issues else 0


if __name__ == "__main__":
    raise SystemExit(main())
