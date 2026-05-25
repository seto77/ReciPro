# STEM シミュレーション

**STEM (Scanning Transmission Electron Microscopy)** シミュレーションは、走査透過電子顕微鏡像を計算します。

---

## 概要

STEM像は、収束した電子ビームを試料上で走査し、各位置での透過・散乱電子を環状検出器で検出することで形成されます。ReciProではブロッホ波法を用いてSTEM像をシミュレーションします。

---

## 計算手法

1. 各走査位置で、収束ビームの各入射方向に対してブロッホ波法で回折強度を計算
2. 検出器の角度範囲内の散乱強度を積算
3. 弾性散乱と熱散漫散乱 (TDS) の両方を計算可能

---

## 計算時間に影響する要因

STEMシミュレーションは計算コストが高いため、以下のパラメータを適切に設定してください。

| 要因 | 影響 |
|------|------|
| **収束角** | 大きいほどCBEDディスクの重なりが増え、計算コストが増大 |
| **ブロッホ波の数** | 固有値問題のコストは波数の3乗に比例 |
| **角度分解能** | 細かいほど正確だが計算時間は二乗で増大 |
| **画素数** | 画像のピクセル数に線形に比例 |

---

## 検出器の種類

| 検出器 | 角度範囲 | 主な寄与 | 像のコントラスト |
|--------|---------|---------|----------------|
| **BF** (明視野) | 0 ~ 収束角 | 弾性散乱 | 位相コントラスト |
| **ABF** (環状明視野) | 収束角の内側 | 弾性散乱 | 軽元素に感度が高い |
| **LAADF** (低角環状暗視野) | 収束角のやや外側 | 弾性 + TDS | ひずみに敏感 |
| **HAADF** (高角環状暗視野) | 収束角の十分外側 | TDS (非弾性) | Z-コントラスト（$\propto Z^2$、原子番号の約2乗に比例） |

---

## STEM固有パラメータ

| パラメータ | 説明 | 典型値 |
|-----------|------|--------|
| **Convergence angle** | 収束ビーム半角 (mrad) | 15–25 mrad |
| **Detector inner angle** | 環状検出器の内角 (mrad) | BF: 0, HAADF: 50–80 |
| **Detector outer angle** | 環状検出器の外角 (mrad) | BF: 15, HAADF: 200 |
| **Effective source size** | 有効光源サイズの半値幅 (pm) | 50–100 pm |
| **Slice thickness** | TDS計算用のスライス厚さ (nm) | — |
| **Angular resolution** | 収束ビームの角度ステップ (mrad) | 1–3 mrad |

---

## 表示モード

| モード | 説明 |
|--------|------|
| **Elastic only** | 弾性散乱のみの像 |
| **TDS only** | 熱散漫散乱のみの像 |
| **Both** | 弾性 + TDS の合計像 |

---

## 温度因子の重要性

> **重要**: HAADF-STEM像のシミュレーションには、原子の等方性温度因子 (Debye-Waller factor) をゼロ以外に設定する必要があります。温度因子が不明な場合は B = 0.5 Å² に設定してください。

温度因子がゼロの場合、TDS強度がゼロとなり、HAADF像が正しく計算されません。

---

## Dr. Probe との比較

ReciProのSTEMシミュレーション結果は、広く使われている Dr. Probe GUI (v.1.10) と良好に一致することが確認されています。下図は、BF・ABF・LAADF・HAADF 検出器について厚さシリーズ（2.96～60.05 nm）で両者を比較したものです（左: 収差なし、右: Cs = 0.2 mm, デフォーカス = −25.9 nm）。すべての検出器・厚さで両者はよく一致します。

![STEM シミュレーション比較: Dr. Probe vs ReciPro](../../assets/references/STEM_DrProbe_comparison.png)

より詳細な比較は PDF で参照できます: [Comparison of STEM simulations by Dr. Probe GUI (v.1.10) and ReciPro (v.4.854)](https://github.com/seto77/ReciPro/files/10976084/ComparisonSTEMsimulations.pdf)

---

## 関連項目

- [HRTEM/STEMシミュレータ](index.md)
- [HRTEMシミュレーション](1-hrtem-simulation.md)
- [ポテンシャルシミュレーション](3-potential-simulation.md)
