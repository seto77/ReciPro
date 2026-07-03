# EBSD 计算

EBSD（电子背散射衍射）使用与 CBED 和 STEM 相同的 Bethe/布洛赫波内核，但问题的提法不同。CBED 和 STEM 是**入射束问题**：一束电子波从样品外部进入，并计算其出射波。EBSD 是**出射方向问题**：在样品内部经历了非弹性散射的电子作为背散射电子射出，计算所要回答的是有多少强度沿每个外部方向离开。

ReciPro 通过互易定理将该出射方向问题转化为一个普通的入射束问题。它首先计算方向空间中的 **master pattern**，然后将该 master pattern 与蒙特卡罗的深度 / 能量 / 方向权重以及探测器几何相结合，形成探测器图样。

---

## 用互易定理的重新表述

如果直接计算从内部源点 $\mathbf r_n$ 到外部方向 $\widehat{\mathbf s}$ 的振幅，那么对每一个源点都需要一个单独的散射问题。这并不现实。

互易定理将该问题重写如下：一个从 $\mathbf r_n$ 出发的电子出现在远场方向 $\widehat{\mathbf s}$ 上的振幅，等于一束从外部方向 $-\widehat{\mathbf s}$ 入射的互易波在 $\mathbf r_n$ 处的振幅。这束互易波是一个普通的 Bethe/布洛赫波解。将其记为 $\psi_{\widehat{\mathbf s}}^{\mathrm{rec}}(\mathbf r)$，则沿方向 $\widehat{\mathbf s}$ 的 EBSD 强度可以写为

$$I_{\mathrm{EBSD}}(\widehat{\mathbf s};E,z)\propto
\sum_n \sigma_n(E,z)\,
\left|\psi_{\widehat{\mathbf s}}^{\mathrm{rec}}(\mathbf r_n;E,z)\right|^2$$

其中 $\sigma_n(E,z)$ 是在原子位置 $\mathbf r_n$ 附近、于能量 $E$ 和深度 $z$ 处发生非弹性散射进入背散射通道的权重。这些源项作为强度相加，而不是作为相干振幅之和相加，因为假定非弹性散射会破坏不同源位置之间的相位关系。

---

## Master Pattern

EBSD master pattern 将上式中与晶体相关的动力学衍射部分存储在一个方向网格上。从概念上讲，

$$M(\widehat{\mathbf s};E,z)=
\sum_n w_n(E,z)\,
\left|\psi_{\widehat{\mathbf s}}^{\mathrm{rec}}(\mathbf r_n)\right|^2$$

其中 $w_n$ 是在原子位置 $\mathbf r_n$ 处的晶体侧非弹性源权重。ReciPro 使用经验权重

$$w_n \propto Z_n^{1.7}\,\mathrm{occ}_n$$

其中 $Z_n$ 为原子序数，$\mathrm{occ}_n$ 为占有率。这与蒙特卡罗产生的输运深度 / 能量分布是分开的。

在实现中，互易布洛赫波在每个原子位置处被求值：

$$\beta_n^{(j)}=
\alpha^{(j)}
\sum_{\mathbf g}C_{\mathbf g}^{(j)}
\exp\!\left[2\pi i(\mathbf k^{(j)}+\mathbf g)\cdot\mathbf r_n\right]$$

随后代码构造布洛赫波对矩阵

$$S_{jj'}=\sum_n w_n\,\beta_n^{(j)}\,\overline{\beta_n^{(j')}}$$

以及解析的厚度积分

$$\mathcal F_{jj'}(t)=
\frac{\exp\!\left[2\pi i(\gamma^{(j)}-\overline{\gamma^{(j')}})t\right]-1}
{2\pi i(\gamma^{(j)}-\overline{\gamma^{(j')}})}$$

从而 master pattern 被求值为

$$M(\widehat{\mathbf s};E,t)=
\mathrm{Re}\left\{\sum_{j,j'}S_{jj'}(E)\,\mathcal F_{jj'}(t)\right\}$$

在分母接近零的退化极限下，$\mathcal F_{jj'}(t)\to t$。

---

## 方向空间采样

master pattern 本身并不是探测器图像；它是晶体固定方向空间中的强度分布。ReciPro 用 Rosca-Lambert 等积投影对该方向空间进行采样，并将 $+Z$ 与 $-Z$ 两个半球存储为各自独立的平面阵列。等积采样减小了两极与赤道之间的密度偏差。

在此阶段，master pattern 依赖于晶体结构、加速电压、深度、能量和吸收模型。诸如图样中心和屏幕位置之类的探测器几何尚未应用。

---

## 蒙特卡罗权重与探测器图样

为了得到接近实验可观测量的 EBSD 探测器图样，必须按照从每个深度、能量和方向射出的背散射电子数量对 master pattern 加权。将该输运权重记为

$$W(E,z;\widehat{\mathbf s})$$

并以 $\widehat{\mathbf s}(\mathbf p)$ 表示与探测器像素 $\mathbf p$ 相对应的晶体固定方向，则最终的探测器图样为

$$P(\mathbf p)=
\sum_{i,j}
W(E_i,z_j;\widehat{\mathbf s}(\mathbf p))\,
M(\widehat{\mathbf s}(\mathbf p);E_i,z_j)$$

即一个对能量和深度的离散求和。

蒙特卡罗部分追踪弹性散射、非弹性散射、能量损失以及通过样品表面的逸出。对于背散射电子，它构建深度、能量和出射方向的分布。ReciPro 区分两类模型：一类使用最后一次非弹性散射位置以及其后紧接的能量作为有效源，另一类使用逸出深度和逸出能量。

---

## TDS 背景与吸收模型

EBSD 图样不仅包含几何菊池带结构，还包含来自热漫散射（TDS）的平滑背景。当启用 `IncludeTDSBackground` 时，ReciPro 将散射进入后半球的 TDS 分量，

$$\pi/2\leq\theta\leq\pi$$

作为吸收矩阵 $\mu_{\mathrm{back}}$ 求值，并使用与 master pattern 相同的布洛赫波对求和来添加背景强度。由于复用了同一个本征解，TDS 背景所增加的额外开销相对较少。

当启用 `UseNonLocalAbsorption` 时，吸收势不再仅作为 $U'_{\mathbf g-\mathbf h}$ 处理，而是作为依赖于方向和束对的非局域形式处理。这可以提高精度，但同时也需要为 master pattern 网格中的各个方向重建吸收矩阵，因此可能显著增加计算时间。

---

## 实用参数

- **束数**：束数过少会丢失菊池带细节和 HOLZ 带结构。低指数晶带轴可能需要数百束。
- **深度和能量阵列**：如果它们比蒙特卡罗权重 $W(E,z;\widehat{\mathbf s})$ 的变化尺度更粗，则与能量相关的带宽以及沟道深度效应会被平均掉。
- **探测器几何**：图样中心、屏幕距离和样品倾斜决定了映射 $\widehat{\mathbf s}(\mathbf p)$，因此即使 master pattern 不变，探测器图样也可能改变。
- **互易性解释**：master pattern 不是探测器图像。只有在经过蒙特卡罗加权和探测器投影之后，它才成为探测器图样。
- **TDS 背景**：在进行定量带衬度对比时启用它。在不带平滑背景时几何菊池结构更易于检视的情况下，则禁用它。

## 另请参阅

- [动力学计算（公共内核）](calculation.md)
- [附录 A3. 用布洛赫波法处理动力学衍射](index.md)
- [12. EBSD 模拟](../../12-ebsd-simulation.md)
