# 付録 A2. Bloch波法による動力学計算の概要

この付録では、ReciProの **Crystal Diffraction**・**CBED**・**HRTEM/STEM** シミュレータが用いる動力学的電子回折理論の概要を解説します。ReciProは **Bethe / Bloch波** の定式化に従います。実際の計算手順（光学ポテンシャル・透過係数・強度）は [付録 A3. Bloch波法による計算の詳細](a3-bloch-wave-calculation.md) で説明します。

---

## 結晶中の波動方程式

結晶の周期的な静電ポテンシャル中を進む高速電子は、（高エネルギー・定常の）シュレーディンガー方程式に従い、次のように書けます。

$$\nabla^2 \Psi(\mathbf{r}) + 4\pi^2\left\{\, k_{vac}^2 + \sum_{\mathbf g} U_{\mathbf g}\, e^{2\pi i\,\mathbf g\cdot\mathbf r} \right\}\Psi(\mathbf{r}) = 0$$

- $k_{vac}$ — 真空中での電子の波数。
- $U_{\mathbf g}$ — 逆格子ベクトル $\mathbf g$ に対する結晶ポテンシャルのフーリエ係数。ポテンシャルは格子周期性をもつため、逆格子に関するフーリエ級数で表されます。

---

## Bloch の定理

ポテンシャルが結晶格子の周期性をもつため、解は **Bloch 波** になります。

$$\Psi(\mathbf{r}) = b\!\left(\mathbf{k}^{(j)}, \mathbf{r}\right) = u(\mathbf{r})\exp\!\left(2\pi i\,\mathbf{k}^{(j)}\cdot\mathbf{r}\right)$$

- $u(\mathbf r)$ — 結晶格子と同じ周期性をもつ関数。逆格子で展開でき、$u(\mathbf r)=\sum_{\mathbf g} C_{\mathbf g}^{(j)}\exp(2\pi i\,\mathbf g\cdot\mathbf r)$。
- $\mathbf{k}^{(j)}$ — $j$ 番目の Bloch 波数ベクトル。
- $C_{\mathbf g}^{(j)}$ — $j$ 番目の Bloch 波における反射 $\mathbf g$ の振幅（固有ベクトル成分）。

---

## Bethe の動力学方程式

Bloch 波展開を波動方程式に代入すると、各反射 $\mathbf g$ について 1 本ずつの連立方程式、すなわち **Bethe の動力学方程式** が得られます。

$$\left[\,k^2 - \left(\mathbf{k}^{(j)} + \mathbf{g}\right)^2 + i\,U'_{g,g}\right]C_{\mathbf g}^{(j)} + \sum_{h \neq g}\left(U^C_{g-h} + i\,U'_{g,h}\right)C_{\mathbf h}^{(j)} = 0$$

- $U^C_{\mathbf g}$ — **弾性散乱** に対する結晶ポテンシャル。
- $U'_{\mathbf g}$ — 虚（**吸収**）ポテンシャル。**熱散漫散乱（TDS）** を表します。これと Debye–Waller 因子の取り扱いは [付録 A3](a3-bloch-wave-calculation.md) で詳述します。

---

## 幾何学的な定義（エワルド球）

上式に現れるベクトル・スカラーは、エワルド球上で次のように定義されます。

![Bloch波法の計算で用いるベクトルとスカラーの定義](../../assets/references/Bloch.png)

- $\hat{\mathbf n}$ — 結晶表面の法線方向の単位ベクトル。
- $\mathbf k$ — 入射波数ベクトル（先端はエワルド球上）。$\mathbf k_{vac}$ は真空中の波数ベクトル。
- $\mathbf g$ — 逆格子ベクトル。$\mathbf k + \mathbf g$ は逆格子点を指す。
- $\mathbf k^{(j)}$ — $j$ 番目の Bloch 波数ベクトル。すべての Bloch 波数ベクトルは接線成分が共通（表面での連続性）で、法線 $\hat{\mathbf n}$ 方向にのみ異なります：$\mathbf k^{(j)} = \mathbf k + \gamma^{(j)}\hat{\mathbf n}$。
- $\gamma^{(j)}$ — $j$ 番目の固有値（$\mathbf k^{(j)}$ の $\hat{\mathbf n}$ 成分を $\mathbf k$ から測ったもの）。

幾何学的関係から、

$$P_g = 2\,\hat{\mathbf n}\cdot(\mathbf k + \mathbf g), \qquad Q_g = |\mathbf k|^2 - |\mathbf k + \mathbf g|^2 = -\,\mathbf g\cdot(2\mathbf k + \mathbf g)$$

であり、**励起誤差** $S_g$（逆格子点とエワルド球面との幾何学的距離）と、反射を順位付けする **評価関数** $R$ は

$$S_g = \frac{\sqrt{P_g^{\,2} + 4Q_g}\; -\; P_g}{2}, \qquad R = |\mathbf g|\,Q_g^{\,2}$$

となります。

---

## 固有値問題への帰着

$\mathbf{k}^{(j)} = \mathbf{k} + \gamma^{(j)}\hat{\mathbf n}$ とおき、$k^2-(\mathbf k+\mathbf g)^2 = Q_g$ と線形近似 $(\mathbf k^{(j)}+\mathbf g)^2 \approx (\mathbf k+\mathbf g)^2 + \gamma^{(j)} P_g$ を用いると、Bethe の方程式は（$P_g$ で割ることで）標準的な **行列の固有値問題** になります。

$$\mathbf{A}\,\mathbf{C} = \mathbf{C}\,\boldsymbol{\Lambda}, \qquad
A_{gh} = \frac{U^C_{\,g-h} + i\,U'_{g,h}}{P_g}\;\;(g\neq h), \qquad
A_{gg} = \frac{Q_g + i\,U'_{g,g}}{P_g}$$

- $\mathbf{C}$ の各列が固有ベクトル $C^{(j)}_*$（Bloch 波の振幅）です。
- $\boldsymbol{\Lambda}=\mathrm{diag}\!\left(\lambda^{(1)}, \lambda^{(2)}, \dots\right)$ は固有値 $\lambda^{(j)} = \gamma^{(j)}$ を並べた対角行列です。

$\mathbf{A}$ を対角化すれば、**すべての** Bloch 波数ベクトルと振幅が一度に求まります。回折波の振幅、ひいては強度は、入射面・出射面での境界条件と試料厚さから定まります。これらの手順、光学（複素）ポテンシャル、Debye–Waller 因子、透過係数 $T_{\mathbf g}$ については [付録 A3. Bloch波法による計算の詳細](a3-bloch-wave-calculation.md) を参照してください。

> **注:** 回折シミュレータの **Details** テーブルに表示される $V_{\mathbf g}$ は、相対論補正項を掛ける前の数値です。

---

## 関連項目

- [7. 回折シミュレータ](../7-diffraction-simulator/index.md) — 動力学回折パターン
- [9. HRTEM/STEMシミュレータ](../9-hrtem-stem-simulator/index.md)
- [付録 A1. 座標系の定義](a1-coordinate-system.md)
- [付録 A3. Bloch波法による計算の詳細](a3-bloch-wave-calculation.md)
