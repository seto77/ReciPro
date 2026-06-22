# HRTEM 像的形成

HRTEM 像由出射面波函数——即由[动力学核心](calculation.md)求得的透射系数 $T_{\mathbf g}$——通过物镜成像而形成。ReciPro 提供两种模型：快速的**准相干**近似，以及更严格的**透射交叉系数（TCC）**模型。另请参阅 GUI 说明页面 [HRTEM 模拟器](../../9-hrtem-stem-simulator/1-hrtem-simulation.md)。

---

## 符号

| 符号 | 含义 |
|--------|---------|
| $\mathbf R$ | 实空间（像面）中的 X–Y 分量 |
| $\mathbf K$ | 入射波矢的 X–Y 分量 |
| $\mathbf G, \mathbf H$ | 倒易点阵矢量的 X–Y 分量 |
| $\mathbf u$ | 空间频率（例如 $\mathbf K+\mathbf G$） |
| $\chi(\mathbf u)$ | 透镜像差函数 |
| $A(\mathbf u)$ | 物镜光阑函数 |
| $\Delta f$ | 欠焦值 |
| $C_s$ | 球差系数 |
| $C_c$ | 色差系数 |
| $\beta$ | 照明半角（有限光源尺寸的效应） |
| $\Delta E$ | 电子能量涨落的 $1/e$ 宽度 |
| $\Delta_0$ | 欠焦弥散的 $1/e$ 宽度（高斯型），$\Delta_0 = C_c\,\Delta E / E$ |

---

## 透镜像差函数与光阑

$$\chi(\mathbf u) = \pi\lambda\Delta f\, u^2 + \tfrac{1}{2}\pi\lambda^3 C_s\, u^4 = \pi\lambda u^2\!\left(\Delta f + \tfrac{1}{2}\lambda^2 C_s u^2\right)$$

$$A(\mathbf u) = \begin{cases} 1 & (\mathbf u\ \text{inside the objective aperture})\\[2pt] 0 & (\mathbf u\ \text{outside the objective aperture})\end{cases}$$

---

## 准相干模型

一种快速近似：每个衍射束都被透镜传递函数调制，并被相干包络衰减，然后相干叠加。

$$I(\mathbf R) = |\psi(\mathbf R)|^2$$

$$\psi(\mathbf R) = \sum_{\mathbf g} T_{\mathbf g}\,\exp\!\left[2\pi i(\mathbf K+\mathbf G)\cdot\mathbf R\right]\exp\!\left[-i\chi(\mathbf K+\mathbf G)\right]A(\mathbf K+\mathbf G)\,E_c(\mathbf K+\mathbf G)\,E_s(\mathbf K+\mathbf G)$$

其中**时间相干包络**与**空间相干包络**为

$$E_c(\mathbf u) = \exp\!\left[-\tfrac{1}{2}\left(\pi\lambda\Delta_0\, u^2\right)^2\right], \qquad E_s(\mathbf u) = \exp\!\left[-\pi^2\beta^2 u^2\!\left(\Delta f + \lambda^2 C_s u^2\right)^2\right]$$

---

## 透射交叉系数（TCC）模型

对部分相干的严格处理：每一对束 $(\mathbf g, \mathbf h)$ 都通过透射交叉系数发生干涉。

$$I(\mathbf R) = \sum_{\mathbf g}\sum_{\mathbf h} T_{\mathbf g}\,T_{\mathbf h}^{*}\,\exp\!\left[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R\right]\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)$$

$$\mathrm{TCC}(\mathbf u, \mathbf u') = A(\mathbf u)\,A(\mathbf u')\,\exp\!\left[-i\{\chi(\mathbf u)-\chi(\mathbf u')\}\right]E_c(\mathbf u, \mathbf u')\,E_s(\mathbf u, \mathbf u')$$

其中**混合**相干包络为

$$E_c(\mathbf u, \mathbf u') = \exp\!\left[-\tfrac{1}{2}\left(\pi\lambda\Delta_0\right)^2\!\left(u^2 - u'^2\right)^2\right]$$

$$E_s(\mathbf u, \mathbf u') = \exp\!\left[-\pi^2\beta^2\left\{\Delta f(\mathbf u-\mathbf u') + \lambda^2 C_s\!\left(u^2\mathbf u - u'^2\mathbf u'\right)\right\}^2\right]$$

在 $\mathbf u' \to \mathbf u$ 的极限下，TCC 退化为上述的准相干包络。

---

## 降低 TCC 模型的计算量

TCC 模型的双重求和对每一对束都要计算一次 $\mathrm{TCC}$，因此计算量很大。由于像强度 $I(\mathbf R)$ 为实数，计算量可以大致减半。

首先，物镜光阑之外（$A(\mathbf K+\mathbf G)=0$）的束没有贡献，因此只需**仅对光阑内的束（$A=1$）**求和即可。

其次，TCC 满足厄米对称性，

$$\mathrm{TCC}(\mathbf u', \mathbf u) = \mathrm{TCC}(\mathbf u, \mathbf u')^{*}$$

（$A$ 为实数；$E_c, E_s$ 是在 $\mathbf u\leftrightarrow\mathbf u'$ 互换下不变的实函数；相位项 $\exp[-i\{\chi(\mathbf u)-\chi(\mathbf u')\}]$ 取复共轭）。再结合 $\exp[2\pi i(\mathbf H-\mathbf G)\cdot\mathbf R]=\bigl(\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\bigr)^{*}$ 与 $T_{\mathbf h}T_{\mathbf g}^{*}=\bigl(T_{\mathbf g}T_{\mathbf h}^{*}\bigr)^{*}$，可知 $(\mathbf g,\mathbf h)$ 项与 $(\mathbf h,\mathbf g)$ 项互为复共轭，因此它们之和等于实部的两倍：

$$F(\mathbf g,\mathbf h) + F(\mathbf h,\mathbf g) = 2\,\mathrm{Re}\{F(\mathbf g,\mathbf h)\}, \qquad F(\mathbf g,\mathbf h) \equiv T_{\mathbf g}T_{\mathbf h}^{*}\,\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\,\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)$$

因此双重求和可化简为对角项加上三角部分（在给束指定任意次序后取其中一侧），使 $\mathrm{TCC}$ 的计算次数减半：

$$I(\mathbf R) = \sum_{\mathbf g} |T_{\mathbf g}|^2\,A(\mathbf K+\mathbf G)^2 \;+\; 2\sum_{\mathbf g}\sum_{\mathbf h > \mathbf g} \mathrm{Re}\!\left\{ T_{\mathbf g}T_{\mathbf h}^{*}\,\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\,\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)\right\}$$

对角项满足 $\mathrm{TCC}(\mathbf u,\mathbf u)=A(\mathbf u)^2$，即在光阑内退化为 $|T_{\mathbf g}|^2$。

此外，在该求和中相位因子 $\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]$ 会多次取到相同的值。存储并复用这些值可进一步加速计算。

---

## 另请参阅

- [动力学计算（公共核心）](calculation.md) — 共享的布洛赫波核心与透射系数 $T_{\mathbf g}$
- [附录 A3. 用布洛赫波法处理的动力学衍射](index.md)
- [9.1. HRTEM 模拟](../../9-hrtem-stem-simulator/1-hrtem-simulation.md)
