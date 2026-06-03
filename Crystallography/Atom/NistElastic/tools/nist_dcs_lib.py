#!/usr/bin/env python3
# 260603Cl 追加: NIST DCS -> sampler形式(σ_el, σ_tr, 2001点CDF) 変換の共通ライブラリ。
# verify_dcs_cdf.py (検証) と build_dcs_blocks.py (本番書出し) が共有する。
#
# codex 確定の高品質レシピ (案X / 20keV超だけ DCS 拡張):
#   u = sin^2(θ/2) = (1-μ)/2,  μ = cosθ,  D(u) = dσ/dΩ [a0^2/sr]
#   σ_el(raw) = 4π ∫_0^1 D du                        (検証用。保存値は NIST 公表値を使う)
#   σ_tr(raw) = 8π ∫_0^1 u·D du                       (検証用)
#   Φ(μ_j)    = ∫_0^{u_j} D du / ∫_0^1 D du,  u_j = 0.0005·j  (j=0..2000)
#   σ_tr(implied) = σ_el(NIST) · Σ Φ重み付き(1-cosθ)   (MC が実際に見る transport)
#   補間: log(D) を u 上で shape-preserving PCHIP。
#   積分: 区間ごと 32点 Gauss-Legendre。breakpoint に NIST角度ノット u と出力 u_j を必ず含める。

from __future__ import annotations

import math
import re
from pathlib import Path

import numpy as np
from scipy.interpolate import PchipInterpolator

PHI_COUNT = 2001
BLOCK_LINES = PHI_COUNT + 2  # block番号 + sigma + 2001点
A0_SQUARED_M2 = 2.8002852e-21  # 1 a0^2 [m^2] (NIST DCS CSV 記載値, 既存コードとも一致)

# 既存エネルギー軸 (50eV-20keV 対数101点) の定義。20keV以下は完全保持する。
LOG_MIN = math.log(50.0)
LOG_STEP = (math.log(20000.0) - math.log(50.0)) / 100.0


def legacy_energy_ev(block_index: int) -> float:
    """既存グリッド(50eV-20keV 対数101点)の blockIndex -> エネルギー[eV]。"""
    return math.exp(LOG_MIN + LOG_STEP * block_index)


# --- DCS CSV 読み込み ----------------------------------------------------------

def read_dcs_csv(path) -> tuple[np.ndarray, np.ndarray, float, float]:
    """NIST DCS CSV を読む。戻り値: theta[deg], dcs[a0^2/sr], σ_el_header[a0^2], energy_ev。"""
    sigma_hdr = math.nan
    energy_ev = math.nan
    theta, dcs = [], []
    for line in Path(path).read_text(encoding="utf-8", errors="ignore").splitlines():
        s = line.strip()
        m = re.search(r"Total cross section:\s*([0-9.Ee+-]+)", s)
        if m:
            sigma_hdr = float(m.group(1))
        m = re.search(r"Energy:\s*([0-9.Ee+-]+)\s*eV", s)
        if m:
            energy_ev = float(m.group(1))
        if "," in s:
            parts = s.split(",")
            try:
                theta.append(float(parts[0]))
                dcs.append(float(parts[1]))
            except ValueError:
                continue
    return np.array(theta), np.array(dcs), sigma_hdr, energy_ev


# --- E_*.TXT ブロック読み込み --------------------------------------------------

def read_etxt_block(path, block_index: int) -> tuple[float, np.ndarray]:
    """E_*.TXT の block_index (0-based) の σ[a0^2] と 2001点CDF を読む。"""
    ls = Path(path).read_text().split("\n")
    i = block_index * BLOCK_LINES
    sigma = float(ls[i + 1])
    phi = np.array([float(ls[i + 2 + k]) for k in range(PHI_COUNT)])
    return sigma, phi


# --- DCS -> CDF 変換 (Gauss-Legendre) -----------------------------------------

_GL_NODES, _GL_WEIGHTS = np.polynomial.legendre.leggauss(32)  # 区間32点


def build_logD_pchip(theta_deg: np.ndarray, dcs: np.ndarray):
    """log(D) を u=sin^2(θ/2) 上で shape-preserving PCHIP 補間する関数と、ノット u を返す。"""
    theta = np.radians(theta_deg)
    u = (1.0 - np.cos(theta)) / 2.0  # θ:0→180 で u:0→1 単調増加
    order = np.argsort(u)
    u, d = u[order], dcs[order]
    # 重複 u を除去 (θ=0,180 端点で稀に同値) - PCHIP は厳密単調増加を要求
    keep = np.concatenate([[True], np.diff(u) > 0])
    u, d = u[keep], d[keep]
    interp = PchipInterpolator(u, np.log(d), extrapolate=True)
    return (lambda uu: np.exp(interp(np.asarray(uu)))), u


def _segment_integrals(dfun, grid: np.ndarray):
    """grid の各小区間で ∫D du と ∫u·D du を 32点 Gauss-Legendre で積む。"""
    x0 = grid[:-1]
    x1 = grid[1:]
    mid = 0.5 * (x0 + x1)
    half = 0.5 * (x1 - x0)
    # shape (nseg, 32)
    xx = mid[:, None] + half[:, None] * _GL_NODES[None, :]
    dd = dfun(xx)
    seg_d = half * np.sum(_GL_WEIGHTS[None, :] * dd, axis=1)
    seg_ud = half * np.sum(_GL_WEIGHTS[None, :] * xx * dd, axis=1)
    return seg_d, seg_ud


def dcs_to_cdf(theta_deg: np.ndarray, dcs: np.ndarray):
    """DCS から (sigma_el_raw[a0^2], sigma_tr_raw[a0^2], phi[2001]) を生成する。

    phi は ∫_0^{u_j} D / ∫_0^1 D で末尾を 1 に正規化 (σ_el の絶対値に依らない)。
    """
    dfun, u_knots = build_logD_pchip(theta_deg, dcs)
    uj = 0.0005 * np.arange(PHI_COUNT)  # 出力ノード u_j
    # 積分グリッド = NIST角度ノット u ∪ 出力 u_j ∪ {0,1}
    grid = np.unique(np.concatenate([u_knots, uj, [0.0, 1.0]]))
    grid = grid[(grid >= 0.0) & (grid <= 1.0)]
    seg_d, seg_ud = _segment_integrals(dfun, grid)
    cum = np.concatenate([[0.0], np.cumsum(seg_d)])  # ∫_0^{grid[i]} D du
    total = cum[-1]
    sigma_el_raw = 4.0 * math.pi * total
    sigma_tr_raw = 8.0 * math.pi * float(np.sum(seg_ud))
    cdf_at_grid = cum / total
    phi = np.interp(uj, grid, cdf_at_grid)  # uj⊂grid なので実質厳密
    phi[0] = 0.0
    phi[-1] = 1.0
    return sigma_el_raw, sigma_tr_raw, phi


def sigma_tr_implied(sigma_el_value: float, phi: np.ndarray) -> float:
    """CDF が表す平均 (1-cosθ) から MC が実際に見る σ_tr を計算する。

    Φ(μ_j) は μ_j=1-0.001j の累積確率。dP_j = Φ_j - Φ_{j-1} を μ の中点で重み付け。
    σ_tr = σ_el · Σ dP_j (1 - μ_mid)。
    """
    mu = 1.0 - 0.001 * np.arange(PHI_COUNT)  # μ_0=1 .. μ_2000=-1
    dp = np.diff(phi)                          # 各区間の確率
    mu_mid = 0.5 * (mu[:-1] + mu[1:])
    mean_one_minus_cos = float(np.sum(dp * (1.0 - mu_mid)))
    return sigma_el_value * mean_one_minus_cos


def p_theta_gt(phi: np.ndarray, theta0_deg: float) -> float:
    """P(θ > θ0) = 1 - Φ(μ=cosθ0)。"""
    u0 = (1.0 - math.cos(math.radians(theta0_deg))) / 2.0
    j = u0 / 0.0005
    return 1.0 - float(np.interp(j, np.arange(PHI_COUNT), phi))


def theta_quantile(phi: np.ndarray, r: float) -> float:
    """累積確率 r に対応する散乱角 θ[deg] (R=0.5,0.9,0.99,0.999 検証用)。"""
    j = float(np.interp(r, phi, np.arange(PHI_COUNT)))
    mu = 1.0 - 0.001 * j
    return math.degrees(math.acos(max(-1.0, min(1.0, mu))))
