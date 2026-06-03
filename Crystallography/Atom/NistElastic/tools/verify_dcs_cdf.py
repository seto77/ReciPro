#!/usr/bin/env python3
# 260603Cl: NIST DCS CSV -> 自前CDF を nist_dcs_lib で生成し、既存 E_*.TXT ブロックと高精度照合。
# codex 検証ゲート: sigma_el(raw) vs NIST, sigma_tr(implied) vs NIST(TCS別取得), CDF単調/端点,
#                   P(theta>t0), theta分位点。出力は ASCII (PowerShell cp932 文字化け回避)。
#
# 使い方:
#   python verify_dcs_cdf.py --dcs ..\DCS\DCS_Z14_E20keV.csv --etxt ..\Original\E_14.TXT --block 100

from __future__ import annotations

import argparse
import sys
from pathlib import Path

import numpy as np

sys.path.insert(0, str(Path(__file__).resolve().parent))
import nist_dcs_lib as lib  # noqa: E402


def main() -> int:
    ap = argparse.ArgumentParser(description="Verify DCS->CDF against existing E_*.TXT block.")
    ap.add_argument("--dcs", required=True)
    ap.add_argument("--etxt", required=True, help="比較対象の既存 E_NN.TXT (省略時は CDF 単体検証)")
    ap.add_argument("--block", type=int, default=100, help="0-based block index (100=20keV)")
    ap.add_argument("--sigma-tr-nist", type=float, default=None, help="NIST 公表 sigma_tr [a0^2] (TCS取得値)")
    args = ap.parse_args()

    theta, dcs, sig_hdr, e_ev = lib.read_dcs_csv(args.dcs)
    sig_raw, str_raw, phi = lib.dcs_to_cdf(theta, dcs)
    str_impl = lib.sigma_tr_implied(sig_hdr, phi)

    print(f"DCS CSV : {len(theta)} angles {theta.min():.0f}..{theta.max():.0f} deg, "
          f"E={e_ev:.0f} eV, header sigma_el={sig_hdr:.6E} a0^2")

    # --- CDF 健全性 ---
    mono = bool(np.all(np.diff(phi) >= -1e-12))
    print(f"CDF     : endpoints Phi[0]={phi[0]:.3E} Phi[-1]={phi[-1]:.6f}, monotonic={mono}")
    print()
    print("=== sigma [a0^2] ===")
    print(f"  sigma_el raw(GL32) : {sig_raw:.6E}   rel vs NIST header {abs(sig_raw/sig_hdr-1)*100:.4f}%")
    print(f"  sigma_tr raw       : {str_raw:.6E}")
    print(f"  sigma_tr implied   : {str_impl:.6E}   (= sigma_el_NIST * <1-cos>)")
    if args.sigma_tr_nist:
        print(f"  sigma_tr NIST(TCS) : {args.sigma_tr_nist:.6E}   rel implied {abs(str_impl/args.sigma_tr_nist-1)*100:.4f}%")

    eb_phi = None
    if args.etxt and Path(args.etxt).exists():
        eb_sigma, eb_phi = lib.read_etxt_block(args.etxt, args.block)
        print(f"  E_TXT block {args.block} : sigma_el={eb_sigma:.6E}  rel raw {abs(sig_raw/eb_sigma-1)*100:.4f}%")
    print()

    print("=== P(theta > t0) ===")
    print("   t0[deg]   mine" + ("       E_TXT" if eb_phi is not None else ""))
    for t0 in (10, 30, 60, 90, 120, 150):
        line = f"   {t0:5d}   {lib.p_theta_gt(phi, t0):.6f}"
        if eb_phi is not None:
            line += f"   {lib.p_theta_gt(eb_phi, t0):.6f}"
        print(line)
    print()

    print("=== theta quantile [deg] (R = cumulative prob) ===")
    print("   R        mine" + ("     E_TXT" if eb_phi is not None else ""))
    for r in (0.5, 0.9, 0.99, 0.999):
        line = f"   {r:.3f}   {lib.theta_quantile(phi, r):7.3f}"
        if eb_phi is not None:
            line += f"  {lib.theta_quantile(eb_phi, r):7.3f}"
        print(line)

    if eb_phi is not None:
        d = np.abs(phi - eb_phi)
        print()
        print(f"=== CDF diff (mine vs E_TXT) ===  max|d|={d.max():.3E} at j={int(d.argmax())}, mean|d|={d.mean():.3E}")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
