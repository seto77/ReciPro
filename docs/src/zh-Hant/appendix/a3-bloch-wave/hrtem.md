# HRTEM 像的形成

HRTEM 像由出射面波函數——即由[動力學核心](calculation.md)求得的透射係數 $T_{\mathbf g}$——通過物鏡成像而形成。ReciPro 提供兩種模型：快速的**準同調**近似，以及更嚴格的**透射交叉係數（TCC）**模型。另請參閱 GUI 說明頁面 [HRTEM 模擬器](../../9-hrtem-stem-simulator/1-hrtem-simulation.md)。

---

## 符號

| 符號 | 含義 |
|--------|---------|
| $\mathbf R$ | 實空間（像面）中的 X–Y 分量 |
| $\mathbf K$ | 入射波向量的 X–Y 分量 |
| $\mathbf G, \mathbf H$ | 倒易點陣向量的 X–Y 分量 |
| $\mathbf u$ | 空間頻率（例如 $\mathbf K+\mathbf G$） |
| $\chi(\mathbf u)$ | 透鏡像差函式 |
| $A(\mathbf u)$ | 物鏡光闌函式 |
| $\Delta f$ | 欠焦值 |
| $C_s$ | 球差係數 |
| $C_c$ | 色差係數 |
| $\beta$ | 照明半角（有限光源尺寸的效應） |
| $\Delta E$ | 電子能量漲落的 $1/e$ 寬度 |
| $\Delta_0$ | 欠焦彌散的 $1/e$ 寬度（高斯型），$\Delta_0 = C_c\,\Delta E / E$ |

---

## 透鏡像差函式與光闌

$$\chi(\mathbf u) = \pi\lambda\Delta f\, u^2 + \tfrac{1}{2}\pi\lambda^3 C_s\, u^4 = \pi\lambda u^2\!\left(\Delta f + \tfrac{1}{2}\lambda^2 C_s u^2\right)$$

$$A(\mathbf u) = \begin{cases} 1 & (\mathbf u\ \text{inside the objective aperture})\\[2pt] 0 & (\mathbf u\ \text{outside the objective aperture})\end{cases}$$

---

## 準同調模型

一種快速近似：每個繞射束都被透鏡傳遞函式調制，並被同調包絡衰減，然後同調疊加。

$$I(\mathbf R) = |\psi(\mathbf R)|^2$$

$$\psi(\mathbf R) = \sum_{\mathbf g} T_{\mathbf g}\,\exp\!\left[2\pi i(\mathbf K+\mathbf G)\cdot\mathbf R\right]\exp\!\left[-i\chi(\mathbf K+\mathbf G)\right]A(\mathbf K+\mathbf G)\,E_c(\mathbf K+\mathbf G)\,E_s(\mathbf K+\mathbf G)$$

其中**時間同調包絡**與**空間同調包絡**為

$$E_c(\mathbf u) = \exp\!\left[-\tfrac{1}{2}\left(\pi\lambda\Delta_0\, u^2\right)^2\right], \qquad E_s(\mathbf u) = \exp\!\left[-\pi^2\beta^2 u^2\!\left(\Delta f + \lambda^2 C_s u^2\right)^2\right]$$

---

## 透射交叉係數（TCC）模型

對部分同調的嚴格處理：每一對束 $(\mathbf g, \mathbf h)$ 都通過透射交叉係數發生干涉。

$$I(\mathbf R) = \sum_{\mathbf g}\sum_{\mathbf h} T_{\mathbf g}\,T_{\mathbf h}^{*}\,\exp\!\left[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R\right]\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)$$

$$\mathrm{TCC}(\mathbf u, \mathbf u') = A(\mathbf u)\,A(\mathbf u')\,\exp\!\left[-i\{\chi(\mathbf u)-\chi(\mathbf u')\}\right]E_c(\mathbf u, \mathbf u')\,E_s(\mathbf u, \mathbf u')$$

其中**混合**同調包絡為

$$E_c(\mathbf u, \mathbf u') = \exp\!\left[-\tfrac{1}{2}\left(\pi\lambda\Delta_0\right)^2\!\left(u^2 - u'^2\right)^2\right]$$

$$E_s(\mathbf u, \mathbf u') = \exp\!\left[-\pi^2\beta^2\left\{\Delta f(\mathbf u-\mathbf u') + \lambda^2 C_s\!\left(u^2\mathbf u - u'^2\mathbf u'\right)\right\}^2\right]$$

在 $\mathbf u' \to \mathbf u$ 的極限下，TCC 退化為上述的準同調包絡。

---

## 降低 TCC 模型的計算量

TCC 模型的雙重求和對每一對束都要計算一次 $\mathrm{TCC}$，因此計算量很大。由於像強度 $I(\mathbf R)$ 為實數，計算量可以大致減半。

首先，物鏡光闌之外（$A(\mathbf K+\mathbf G)=0$）的束沒有貢獻，因此只需**僅對光闌內的束（$A=1$）**求和即可。

其次，TCC 滿足厄米對稱性，

$$\mathrm{TCC}(\mathbf u', \mathbf u) = \mathrm{TCC}(\mathbf u, \mathbf u')^{*}$$

（$A$ 為實數；$E_c, E_s$ 是在 $\mathbf u\leftrightarrow\mathbf u'$ 互換下不變的實函式；相位項 $\exp[-i\{\chi(\mathbf u)-\chi(\mathbf u')\}]$ 取複共軛）。再結合 $\exp[2\pi i(\mathbf H-\mathbf G)\cdot\mathbf R]=\bigl(\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\bigr)^{*}$ 與 $T_{\mathbf h}T_{\mathbf g}^{*}=\bigl(T_{\mathbf g}T_{\mathbf h}^{*}\bigr)^{*}$，可知 $(\mathbf g,\mathbf h)$ 項與 $(\mathbf h,\mathbf g)$ 項互為複共軛，因此它們之和等於實部的兩倍：

$$F(\mathbf g,\mathbf h) + F(\mathbf h,\mathbf g) = 2\,\mathrm{Re}\{F(\mathbf g,\mathbf h)\}, \qquad F(\mathbf g,\mathbf h) \equiv T_{\mathbf g}T_{\mathbf h}^{*}\,\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\,\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)$$

因此雙重求和可化簡為對角項加上三角部分（在給束指定任意次序後取其中一側），使 $\mathrm{TCC}$ 的計算次數減半：

$$I(\mathbf R) = \sum_{\mathbf g} |T_{\mathbf g}|^2\,A(\mathbf K+\mathbf G)^2 \;+\; 2\sum_{\mathbf g}\sum_{\mathbf h > \mathbf g} \mathrm{Re}\!\left\{ T_{\mathbf g}T_{\mathbf h}^{*}\,\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\,\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)\right\}$$

對角項滿足 $\mathrm{TCC}(\mathbf u,\mathbf u)=A(\mathbf u)^2$，即在光闌內退化為 $|T_{\mathbf g}|^2$。

此外，在該求和中相位因子 $\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]$ 會多次取到相同的值。儲存並重複使用這些值可進一步加速計算。

---

## 另請參閱

- [動力學計算（共用核心）](calculation.md) — 共享的布洛赫波核心與透射係數 $T_{\mathbf g}$
- [附錄 A3. 用布洛赫波法處理的動力學繞射](index.md)
- [9.1. HRTEM 模擬](../../9-hrtem-stem-simulator/1-hrtem-simulation.md)
