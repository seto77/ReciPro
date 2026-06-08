# HRTEM 像形成

HRTEM 像は、出射面の波動関数（[動力学コア](calculation.md) で求めた透過係数 $T_{\mathbf g}$）を対物レンズに通すことで形成されます。ReciPro は2つのモデルを用意しています：高速な **準コヒーレント**近似と、より厳密な **透過相互係数（TCC）** モデルです。GUI の説明は [HRTEM シミュレータ](../../9-hrtem-stem-simulator/1-hrtem-simulation.md) のページも参照してください。

---

## 記号

| 記号 | 意味 |
|------|------|
| $\mathbf R$ | 実空間（像面）の X–Y 成分 |
| $\mathbf K$ | 入射波数ベクトルの X–Y 成分 |
| $\mathbf G, \mathbf H$ | 逆格子ベクトルの X–Y 成分 |
| $\mathbf u$ | 空間周波数（例: $\mathbf K+\mathbf G$） |
| $\chi(\mathbf u)$ | レンズ収差関数 |
| $A(\mathbf u)$ | 対物絞り関数 |
| $\Delta f$ | デフォーカス値 |
| $C_s$ | 球面収差係数 |
| $C_c$ | 色収差係数 |
| $\beta$ | 照射半角（有限な光源サイズの効果） |
| $\Delta E$ | 電子エネルギー揺らぎの $1/e$ 幅 |
| $\Delta_0$ | デフォーカス広がりの $1/e$ 幅（ガウス分布）、$\Delta_0 = C_c\,\Delta E / E$ |

---

## レンズ収差関数と絞り

$$\chi(\mathbf u) = \pi\lambda\Delta f\, u^2 + \tfrac{1}{2}\pi\lambda^3 C_s\, u^4 = \pi\lambda u^2\!\left(\Delta f + \tfrac{1}{2}\lambda^2 C_s u^2\right)$$

$$A(\mathbf u) = \begin{cases} 1 & (\mathbf u\ \text{は対物絞りの内側})\\[2pt] 0 & (\mathbf u\ \text{は対物絞りの外側})\end{cases}$$

---

## 準コヒーレントモデル

高速な近似です。各回折波にレンズ伝達を掛け、コヒーレンスのエンベロープで減衰させてからコヒーレントに足し合わせます。

$$I(\mathbf R) = |\psi(\mathbf R)|^2$$

$$\psi(\mathbf R) = \sum_{\mathbf g} T_{\mathbf g}\,\exp\!\left[2\pi i(\mathbf K+\mathbf G)\cdot\mathbf R\right]\exp\!\left[-i\chi(\mathbf K+\mathbf G)\right]A(\mathbf K+\mathbf G)\,E_c(\mathbf K+\mathbf G)\,E_s(\mathbf K+\mathbf G)$$

**時間コヒーレンス**・**空間コヒーレンス**のエンベロープは

$$E_c(\mathbf u) = \exp\!\left[-\tfrac{1}{2}\left(\pi\lambda\Delta_0\, u^2\right)^2\right], \qquad E_s(\mathbf u) = \exp\!\left[-\pi^2\beta^2 u^2\!\left(\Delta f + \lambda^2 C_s u^2\right)^2\right]$$

です。

---

## 透過相互係数（TCC）モデル

部分コヒーレンスを厳密に扱うモデルです。すべての波の対 $(\mathbf g, \mathbf h)$ が透過相互係数を通して干渉します。

$$I(\mathbf R) = \sum_{\mathbf g}\sum_{\mathbf h} T_{\mathbf g}\,T_{\mathbf h}^{*}\,\exp\!\left[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R\right]\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)$$

$$\mathrm{TCC}(\mathbf u, \mathbf u') = A(\mathbf u)\,A(\mathbf u')\,\exp\!\left[-i\{\chi(\mathbf u)-\chi(\mathbf u')\}\right]E_c(\mathbf u, \mathbf u')\,E_s(\mathbf u, \mathbf u')$$

**混合**コヒーレンスのエンベロープは

$$E_c(\mathbf u, \mathbf u') = \exp\!\left[-\tfrac{1}{2}\left(\pi\lambda\Delta_0\right)^2\!\left(u^2 - u'^2\right)^2\right]$$

$$E_s(\mathbf u, \mathbf u') = \exp\!\left[-\pi^2\beta^2\left\{\Delta f(\mathbf u-\mathbf u') + \lambda^2 C_s\!\left(u^2\mathbf u - u'^2\mathbf u'\right)\right\}^2\right]$$

です。$\mathbf u' \to \mathbf u$ の極限で、TCC は上記の準コヒーレントのエンベロープに帰着します。

---

## TCC モデルの計算量削減

TCC モデルの二重和は波の対の数だけ $\mathrm{TCC}$ を評価するため計算量が大きくなりますが、像強度 $I(\mathbf R)$ が実数であることを利用して約半分に削減できます。

まず、対物絞りの外側（$A(\mathbf K+\mathbf G)=0$）の波は寄与しないので、和は **絞りの内側（$A=1$）の波だけ** を対象にすれば十分です。

つぎに $\mathrm{TCC}$ はエルミート対称性

$$\mathrm{TCC}(\mathbf u', \mathbf u) = \mathrm{TCC}(\mathbf u, \mathbf u')^{*}$$

を満たします（$A$ は実数、$E_c, E_s$ は $\mathbf u\leftrightarrow\mathbf u'$ の入れ替えで不変な実関数、位相項 $\exp[-i\{\chi(\mathbf u)-\chi(\mathbf u')\}]$ は複素共役になります）。あわせて $\exp[2\pi i(\mathbf H-\mathbf G)\cdot\mathbf R]=\bigl(\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\bigr)^{*}$、$T_{\mathbf h}T_{\mathbf g}^{*}=\bigl(T_{\mathbf g}T_{\mathbf h}^{*}\bigr)^{*}$ なので、対 $(\mathbf g,\mathbf h)$ と $(\mathbf h,\mathbf g)$ の項は互いに複素共役です。よってその和は実部の 2 倍になります：

$$F(\mathbf g,\mathbf h) + F(\mathbf h,\mathbf g) = 2\,\mathrm{Re}\{F(\mathbf g,\mathbf h)\}, \qquad F(\mathbf g,\mathbf h) \equiv T_{\mathbf g}T_{\mathbf h}^{*}\,\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\,\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)$$

これより二重和は対角項と上三角（波に任意の順序を付けたときの片側）だけで表せ、$\mathrm{TCC}$ の評価回数が約半分になります：

$$I(\mathbf R) = \sum_{\mathbf g} |T_{\mathbf g}|^2\,A(\mathbf K+\mathbf G)^2 \;+\; 2\sum_{\mathbf g}\sum_{\mathbf h > \mathbf g} \mathrm{Re}\!\left\{ T_{\mathbf g}T_{\mathbf h}^{*}\,\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\,\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)\right\}$$

対角項では $\mathrm{TCC}(\mathbf u,\mathbf u)=A(\mathbf u)^2$ となり、絞りの内側では $|T_{\mathbf g}|^2$ に帰着します。

さらに、この和の中では位相因子 $\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]$ が同じ値を何度も取ります。これらを保存して再利用することで、計算をいっそう高速化できます。

---

## 関連項目

- [動力学計算（共通コア）](calculation.md) — 共通の Bloch 波コアと透過係数 $T_{\mathbf g}$
- [付録 A3. Bloch波法による動力学計算の概要](index.md)
- [9.1. HRTEMシミュレーション](../../9-hrtem-stem-simulator/1-hrtem-simulation.md)
