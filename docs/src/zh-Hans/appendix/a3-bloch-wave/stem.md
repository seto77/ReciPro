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
- **TDS 模型**：对于 HAADF $Z$ 衬度，TDS 项与弹性项同等重要。

## 另请参阅

- [动力学计算（公共核心）](calculation.md)
- [附录 A3. 用布洛赫波法处理动力学衍射](index.md)
- [9.2. STEM 模拟](../../9-hrtem-stem-simulator/2-stem-simulation.md)
