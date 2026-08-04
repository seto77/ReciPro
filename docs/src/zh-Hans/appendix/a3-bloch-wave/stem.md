# STEM 计算

STEM 图像计算从与 [CBED](cbed.md) 相同的会聚探针表示出发。区别在于可观测量：CBED 显示衍射平面中的盘强度，而 STEM 扫描探针位置，并在每个位置积分进入所选探测器的强度。

---

## 可观测量

设 $\mathbf R_0$ 为探针位置，$\mathbf Q$ 为衍射平面坐标，$t$ 为样品厚度。若探测器函数 $D(\mathbf Q)$ 在探测器角度范围内为 1、范围外为 0，则弹性 STEM 强度为

$$I_{\mathrm{STEM}}^{\mathrm{ela}}(\mathbf R_0)=
\int D(\mathbf Q)\,
\left|\psi(\mathbf Q,t;\mathbf R_0)\right|^2\,d\mathbf Q$$

BF、ABF、LAADF 和 HAADF 对应于 $D(\mathbf Q)$ 中内、外角度的不同选择。因此改变 STEM 探测器角度会改变所积分的物理量；这不仅仅是一项显示设置。

---

## 通过傅里叶系数加速

直接的实现会对每个被扫描的探针位置 $\mathbf R_0$ 重新求解动力学问题。会聚探针表达式具有一个有用的结构：对 $\mathbf R_0$ 的依赖以相位因子的形式出现

$$\exp(-2\pi i\,\mathbf K\cdot\mathbf R_0)$$

这使得 ReciPro 可以先计算图像的二维傅里叶系数，而不必逐点计算 $I_{\mathrm{STEM}}(\mathbf R_0)$。从概念上讲，

$$I_{\mathrm{STEM}}^{\mathrm{ela}}(\mathbf q)=
\sum_{\mathbf g,\mathbf h}
F_{\mathbf g,\mathbf h}(t)\,
\delta(\mathbf q-\mathbf g+\mathbf h)$$

因此一旦已知系数 $F_{\mathbf g,\mathbf h}(t)$，便可通过逆傅里叶变换高效地重建完整的扫描图像。

这是布洛赫波 STEM 对于具有小晶胞的完美晶体的主要优势。它可以比在每个探针位置重复一次多层切片（multislice）计算快得多。

---

## 重建为实数图像 {#real-image-reconstruction}

图像由系数按下式还原：

$$I(\mathbf r)=\sum_{\mathbf q}I(\mathbf q)\,\exp(2\pi i\,\mathbf q\cdot\mathbf r),
\qquad \mathbf q=\mathbf g-\mathbf h$$

由于 $I(\mathbf r)$ 是实数强度，其系数必须严格满足厄米对称性：

$$I(-\mathbf q)=I(\mathbf q)^{*}$$

而由所有束对生成的 $\mathbf q$ 集合在 $\mathbf q\rightarrow-\mathbf q$ 下是封闭的。因此该求和在构造上即为实数，**任何残留的虚部都是数值误差而非物理**。

实际上确实会残留很小的虚部，因为 $\mathbf k+\mathbf q$ 处的振幅是在有限的入射方向网格上通过双线性插值得到的（参见[探针的角度采样](#angular-sampling)）。这使得 $I(-\mathbf q)$ 与 $I(\mathbf q)^{*}$ 相差 $h^{2}$ 量级，其中 $h$ 为角度步长。

将求和后的像素写作 $a+ib$，把它归约为实数图像的正确做法是取**实部** $a$。这是向实轴的正交投影，与先将系数对称化

$$I_{\mathrm{sym}}(\mathbf q)=\tfrac12\left[I(\mathbf q)+I(-\mathbf q)^{*}\right]$$

再求和完全等价。而取模 $\sqrt{a^{2}+b^{2}}\simeq a+b^{2}/2a$ **并不**等价，且在四个方面都是错误的：

- 额外项 $b^{2}/2a$ 恒为正，因而永不抵消——这是偏置而非噪声；
- 在 $a$ 较小处，即**暗**像素处，它相对信号最大，因此侵蚀的是图像衬度而非整体亮度；
- 它破坏线性性，由于 $\lvert z_1+z_2\rvert\neq\lvert z_1\rvert+\lvert z_2\rvert$，合成图像不再等于弹性 + TDS；
- 它掩盖负值像素，而负值正是 $\mathbf q$ 取样不足的可见征兆，本应作为对用户的警示保留下来。

因此 ReciPro 的弹性、TDS 与 STEM-EDX 图像均由实部重建，并且仅在光源尺寸引起的模糊之后才截断到零，使真正为负的像素在此之前始终可被检出。

!!! note
    4.944 之前的版本对弹性图像与 TDS 图像取模求和。在默认角度网格下，其差异远低于任何可察觉的水平（见下表）；只有在刻意采用粗网格时才会变得可测，且表现形式始终是暗像素略微变亮。

---

## 探针的角度采样 {#angular-sampling}

入射锥在步长为 $\Delta\alpha$（STEM 选项中的**角分辨率**）的方形方向网格上采样，以少量余量覆盖会聚半角 $\alpha$。沿一个轴的分割数为

$$N=\left\lceil\frac{2\alpha\times1.05}{\Delta\alpha}\right\rceil$$

因而方向数——也即需要求解的本征值问题数——按 $N^{2}$ 增长。该网格与扫描点数无关：它离散化的是*探针内部的方向*，而非*探针的位置*。

它也是上述厄米残差的唯一来源，因此该残差可直接用作收敛指标。下列数值测自 SrTiO₃ [001]、200 kV、$\alpha=25$ mrad、128 束、32×32 扫描点。「残差」为 $\max_{\mathbf q}\lvert I(\mathbf q)-I(-\mathbf q)^{*}\rvert$ 相对 $I(\mathbf 0)$ 的值，右侧两列给出取模求和本会在最亮像素上增加的变亮量。

| $N$ | 方向数 | 弹性残差 | TDS 残差 | 取模偏置（弹性） | 取模偏置（TDS） |
|----:|-----------:|-----------------:|-------------:|------------------------:|--------------------:|
| 16  | 256    | 1.2×10⁻³ | 6.1×10⁻³ | 2.4×10⁻⁵ | 1.1×10⁻⁴ |
| 32  | 1024   | 4.1×10⁻⁴ | 2.6×10⁻³ | 1.1×10⁻⁶ | 1.3×10⁻⁵ |
| 64  | 4096   | 5.6×10⁻⁵ | 7.2×10⁻⁴ | 5.8×10⁻⁸ | 4.3×10⁻⁷ |
| 132 | 17424  | 3.8×10⁻⁵ | 1.1×10⁻⁴ | 4.2×10⁻⁸ | 3.6×10⁻⁸ |

默认角分辨率 0.4 mrad 对 $\alpha=25$ mrad 给出 $N=132$，已处于收敛区。另有两点值得注意：

- 在任何网格下，TDS 残差都比弹性残差大约一个量级，因为 TDS 系数还多带了一重探测器选择吸收的厚度积分。
- 残差是对全部 $\mathbf q$ 取的最大值，因此逐网格略有起伏而非完全平滑下降；其背后的趋势为 $O(h^{2})$。

---

## TDS 与探测器选择性吸收

在 HAADF-STEM 中，来自热漫散射 (TDS) 的非弹性分量往往是图像衬度的主要来源。ReciPro 将 TDS 处理为从弹性通道中移除并进入所选角度范围的强度，并用吸收势来表示。

对于探测器角度范围 $\theta_1\leq\theta\leq\theta_2$，探测器选择性吸收散射因子在概念上可写为

$$f'_{\kappa}(\mathbf g;\theta_1,\theta_2)=
\int_{\theta_1}^{\theta_2}\sin\theta\,d\theta
\int_0^{2\pi}
\left|\Delta f_{e,\kappa}(\mathbf g,\theta,\phi)\right|^2\,d\phi$$

将该范围选取为与 BF、ADF 或 HAADF 探测器相匹配，即可计算出进入该探测器的 TDS 贡献。

STEM TDS 强度是探测器选择性吸收的厚度积分：

$$I_{\mathrm{STEM}}^{\mathrm{TDS}}(\mathbf R_0)=
\int_0^t
\langle\psi(z;\mathbf R_0)|\widehat W_{\mathrm{det}}|\psi(z;\mathbf R_0)\rangle\,dz$$

其中 $\widehat W_{\mathrm{det}}$ 表示探测器选择性 TDS。一旦已知布洛赫波的本征值和本征矢量，这个 $z$ 积分便可解析处理。数值切片积分同样可行，ReciPro 会根据计算模式采用合适的方法。

---

## 局域吸收与非局域吸收

吸收势可以用两种主要方式处理。

| 形式 | 含义 | 特点 |
|------|---------|---------|
| 局域近似 | 使用仅依赖于位置的吸收势 $U'(\mathbf r)$。 | 对宽 ADF / HAADF 探测器通常有效且快速。 |
| 非局域形式 | 使用 $U'(\mathbf r,\mathbf r')$ 或依赖于入射波与出射波成对组合的矩阵元 $U'_{\mathbf g,\mathbf h}$。 | 对窄探测器、重元素或低加速电压更准确，但代价高得多。 |

在局域近似中，矩阵元可由倒易矢量差（如 $U'_{\mathbf g-\mathbf h}$）求得。在非局域形式中，每一对 $(\mathbf g,\mathbf h)$ 都需要各自的角度积分，因此计算代价随束数迅速增长。

---

## 布洛赫波 STEM 的适用范围

布洛赫波 STEM 对于高度周期性的完美晶体很快，非常适合对厚度、欠焦和探测器角度进行系统比较。对于缺陷、大型超胞或非周期性结构，诸如冻结声子多层切片（frozen-phonon multislice）之类的方法可能更合适，因为它们不依赖于相同的小周期胞假设。

在 ReciPro 中，理解 STEM 最简单的方式如下：从与 CBED 相同的会聚波出发，然后将衍射盘可观测量替换为对衍射平面的探测器积分。

---

## 实用参数

- **探测器角度**：BF / ABF / ADF / HAADF 是 $D(\mathbf Q)$ 与 $f'_{\kappa}(\mathbf g;\theta_1,\theta_2)$ 的定义。
- **束数**：高频图像分量和通道效应对所纳入的束数较为敏感。
- **厚度步长**：若使用数值切片积分，请检查将切片厚度减半时的变化。
- **角分辨率**：决定探针方向网格 $N$（参见[探针的角度采样](#angular-sampling)）。计算量按 $N^{2}$ 增长，因而是左右计算时间的最主要因素。
- **TDS 模型**：对于 HAADF $Z$ 衬度，TDS 项与弹性项同等重要。

## 另请参阅

- [动力学计算（公共内核）](calculation.md)
- [附录 A3. 用布洛赫波法处理动力学衍射](index.md)
- [9.2. STEM 模拟](../../9-hrtem-stem-simulator/2-stem-simulation.md)
