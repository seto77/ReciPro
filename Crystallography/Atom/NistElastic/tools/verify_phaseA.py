#!/usr/bin/env python3
# 260603Cl 追加: Phase A 一括検証。代表元素の本番ノード(blockIndex 101-110)と中点(100.5-109.5)の
# DCS から CDF を生成し、(1) 健全性(σ_el raw vs NIST, CDF単調/端点, σ_tr_implied)、
# (2) 中点補間誤差(ランタイムの対数線形補間 vs 中点直接取得)、(3) 20keV境界連続性 を一覧する。
# codex 検証駆動: 中点誤差が大きい区間だけ後で本番ノードに昇格する判断材料。
#
#   python verify_phaseA.py 6 13 14 26 29 79

from __future__ import annotations

import argparse
import sys
from pathlib import Path

import numpy as np

sys.path.insert(0, str(Path(__file__).resolve().parent))
import nist_dcs_lib as lib  # noqa: E402


def load_cdf(dcs_dir: Path, z: int, e_kev: float):
    p = dcs_dir / f"DCS_Z{z:02d}_E{e_kev:g}keV.csv"
    if not p.exists():
        return None
    theta, dcs, sig_hdr, _ = lib.read_dcs_csv(p)
    sig_raw, str_raw, phi = lib.dcs_to_cdf(theta, dcs)
    return {
        "phi": phi, "sig_hdr": sig_hdr, "sig_raw": sig_raw,
        "str_impl": lib.sigma_tr_implied(sig_hdr, phi),
        "mono": bool(np.all(np.diff(phi) >= -1e-12)),
    }


def main() -> int:
    here = Path(__file__).resolve().parent.parent
    ap = argparse.ArgumentParser()
    ap.add_argument("atomic_numbers", nargs="+", type=int)
    ap.add_argument("--dcs-dir", default=str(here / "DCS"))
    ap.add_argument("--in-dir", default=str(here / "Original"))
    args = ap.parse_args()
    dcs_dir = Path(args.dcs_dir)

    worst_mid = 0.0
    worst_relsig = 0.0
    for z in sorted(dict.fromkeys(args.atomic_numbers)):
        print(f"\n===== Z={z} =====")
        nodes = {}
        print(" blk  E[keV]    sig_el_raw  relNIST%  sigtr_impl  mono CDF[-1]")
        for b in range(101, 111):
            e = lib.legacy_energy_ev(b) / 1000.0
            d = load_cdf(dcs_dir, z, e)
            if d is None:
                print(f" {b:3d} {e:8.3f}  MISSING")
                continue
            nodes[b] = d
            rel = abs(d["sig_raw"] / d["sig_hdr"] - 1) * 100
            worst_relsig = max(worst_relsig, rel)
            print(f" {b:3d} {e:8.3f}  {d['sig_raw']:.4E}  {rel:6.3f}  {d['str_impl']:.3E} "
                  f"{str(d['mono'])[0]}   {d['phi'][-1]:.4f}")

        # 20keV 既存 sampler CDF (block 100)
        etxt = Path(args.in_dir) / f"E_{z:02d}.TXT"
        sampler20 = lib.read_etxt_block(etxt, 100)[1] if etxt.exists() else None

        print(" mid    E[keV]   max|dCDF|   j     region")
        for b in range(100, 110):
            em = lib.legacy_energy_ev(b + 0.5) / 1000.0
            dm = load_cdf(dcs_dir, z, em)
            if dm is None:
                print(f" {b + 0.5:5.1f} {em:8.3f}  MISSING")
                continue
            lo = sampler20 if b == 100 else (nodes[b]["phi"] if b in nodes else None)
            hi = nodes[b + 1]["phi"] if (b + 1) in nodes else None
            if lo is None or hi is None:
                print(f" {b + 0.5:5.1f} {em:8.3f}  (隣接ノード欠損)")
                continue
            interp = 0.5 * (lo + hi)  # 対数等間隔の中点 -> frac=0.5 線形補間 (ランタイム模擬)
            err = np.abs(interp - dm["phi"])
            worst_mid = max(worst_mid, err.max())
            region = "sampler20->21.23" if b == 100 else f"{b}->{b + 1}"
            print(f" {b + 0.5:5.1f} {em:8.3f}  {err.max():.3E} {int(err.argmax()):4d}   {region}")

    print(f"\n==== 全体最悪値: 中点補間 max|dCDF|={worst_mid:.3E}, σ_el raw vs NIST 最大 rel={worst_relsig:.3f}% ====")
    print("   (目安: 中点 max|dCDF| が 1e-3 を大きく超える区間は exact node 追加を検討)")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
