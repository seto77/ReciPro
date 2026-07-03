# 附录 A2. 射束相互作用（固体物理背景）

主窗口章节 [3. Beam interaction](../../3-beam-interaction.md) 是 GUI 的操作指南：它告诉你该按哪些按钮、每一列代表什么。本附录汇集这些数值背后的**固体物理与散射物理** — 为什么一个原子对 X 射线、电子和中子的散射方式如此不同，结构因子及其虚部从何而来，射束在固体内部如何被衰减和减速，以及荧光预览表示什么、不表示什么。

![射束相互作用窗口](../../../assets/cap-zh-Hans-auto/FormBeamInteraction.png)

该窗口有四个选项卡，理论最好按照“一个量决定下一个量”的依赖顺序来阅读：

1. **[Atomic scattering factors](scattering-factor.md)** — *单个原子* 如何散射每一种射束。
2. **[Structure factor](structure-factor.md)** — *晶胞* 中的原子如何干涉，包括德拜-沃勒因子和消光规则。
3. **[Attenuation & transport](attenuation-transport.md)** — 射束在穿过材料的过程中如何被 *移除和减速*。
4. **[荧光](fluorescence.md)** — 内壳层电离之后产生的特征 X 射线发射。

---

## 散射几何与变量 $s$

本窗口中的每一个散射量都是射束方向变化大小的函数。以 $\mathbf k_i$ 和 $\mathbf k_s$ 表示入射和散射波矢（弹性散射，因此 $|\mathbf k_i|=|\mathbf k_s|=1/\lambda$），则**散射矢量**及其大小为

$$\mathbf Q = 2\pi(\mathbf k_s - \mathbf k_i), \qquad Q = |\mathbf Q| = \frac{4\pi\sin\theta}{\lambda} = 4\pi s .$$

- $\theta$ ：布拉格角 — 总散射角的 *一半*。衍射 表中列出的是全角 $2\theta$。
- $s = \dfrac{\sin\theta}{\lambda}$（Å⁻¹）：**散射因子** 选项卡所对应的横轴变量。它是每一个原子形状因子的自然变量。
- $d$ ：面间距。在布拉格条件 $\lambda = 2d\sin\theta$ 下，$s = \dfrac{1}{2d} = \dfrac{|\mathbf g|}{2}$，其中 $\mathbf g$ 是倒易点阵矢量，满足 $|\mathbf g| = 1/d$。

这三种约定描述的是同一几何关系，只是标度不同。由于本窗口同时使用了其中不止一种，因此值得把它们的对应关系理清楚：

| 窗口中的量 | 符号 | 关系 |
|---|---|---|
| 衍射 表 | $q = 2\pi/d$ | $q = 2\pi\lvert\mathbf g\rvert = Q = 4\pi s$ |
| 衍射 表 | $2\theta$ | 全散射角，$\sin\theta = \lambda s$ |
| 散射因子 选项卡 | $s = \sin\theta/\lambda$ | $s = q/4\pi = 1/(2d)$ |
| 衍射峰图 | $Q = 4\pi\sin\theta/\lambda$ | $Q = q = 4\pi s$ |

!!! note "单位"
    已发表的形状因子参数化使用以 Å⁻¹ 为单位的 $s$（因此 $s^2$ 以 Å⁻² 为单位），而 ReciPro 内部以 nm⁻² 保存 $s^2$。两者在 $s^2$ 上相差 $100$ 倍；曲线和表格以各表表头中标注的单位呈现。有一个模型 — **Kirkland** — 是对 $q = 2s = 1/d$ 而非对 $s$ 制表的；参见 [Atomic scattering factors](scattering-factor.md)。

### 布拉格、劳厄与埃瓦尔德球 {#phase-convention}

布拉格条件只是同一几何要求的一个侧面。相长干涉（**劳厄条件**）要求散射矢量等于一个倒易点阵矢量，

$$\mathbf k_s = \mathbf k_i + \mathbf g, \qquad |\mathbf k_i + \mathbf g|^2 = |\mathbf k_i|^2 ,$$

利用 $|\mathbf k_i|=|\mathbf k_s|=1/\lambda$，可化简为

$$2\,\mathbf k_i\cdot\mathbf g + |\mathbf g|^2 = 0 \qquad\Longleftrightarrow\qquad |\mathbf g| = \frac{1}{d} = \frac{2\sin\theta}{\lambda},$$

即 **布拉格定律** $\lambda = 2d\sin\theta$。在几何上这就是 **埃瓦尔德球** 构造：当某反射的倒易点阵点落在半径为 $1/\lambda$ 的球面上时，该反射被激发。（此处 $\mathbf g$ 以 $1/d$ 为单位，因此 $\mathbf Q = 2\pi\mathbf g$。）

---

## 相位约定

ReciPro 采用晶体学相位约定来构建结构因子

$$F_{\mathbf g} = \sum_j \dots \exp\!\left(-2\pi i\,\mathbf g\cdot\mathbf r_j\right),$$

即指数中带 **负** 号。这一选择确定了结构因子虚部（衍射 表中的 `F_inv`）的符号，以及开启反常色散后弗里德尔对之间的关系。这里只陈述一次，并在整个附录中作为前提；其推论将在 [Structure factor](structure-factor.md) 中展开。

---

## 运动学散射与动力学散射

本附录处理 **单次（运动学）散射**：入射束散射一次，衍射振幅就是下一页的结构因子。当相互作用较弱时这一图像是正确的 — 几乎所有样品中的 X 射线和中子，以及 *非常薄* 样品中的电子。

当相互作用较强时 — 除最薄晶体之外的任何晶体中的电子 — 射束在离开之前会多次散射，强度在各反射之间被重新分配，$\lvert F\rvert^2$ 不再给出测量强度。该区域需要 [Appendix A3](../a3-bloch-wave/index.md) 的 **动力学** 理论。这里导出的散射因子和结构因子是两种图像的 *输入*。

即使在运动学极限下，衍射振幅也不仅仅是结构因子：将散射波在厚度为 $t$ 的薄板上求和给出

$$A_{\mathbf g}(t) \;\propto\; F_{\mathbf g}\int_0^t e^{\,2\pi i S_{\mathbf g} z}\,dz = F_{\mathbf g}\, t\, e^{\,\pi i S_{\mathbf g} t}\,\operatorname{sinc}(\pi S_{\mathbf g} t),$$

其中 $S_{\mathbf g}$ 是 **偏离矢量** — 倒易点阵点到埃瓦尔德球的距离。强度在 $S_{\mathbf g}=0$ 处出现尖锐峰值，并随厚度振荡（厚度条纹的起源）；[Appendix A3](../a3-bloch-wave/index.md) 的动力学理论用耦合束行为取代了这一单束结果。

---

## 三种探针一览

| | X 射线 | 电子 | 中子 |
|---|---|---|---|
| 相互作用对象 | 电子密度 $\rho_e$ | 静电势 $V$ | 原子核（及未配对自旋） |
| 相互作用强度 | 弱 | 强 | 非常弱 |
| 典型穿透深度 | µm – mm | nm – µm | mm – cm |
| 单次散射是否成立？ | 几乎总是 | 仅薄膜 | 几乎总是 |
| 对轻原子的敏感性 | 差（$\propto Z$） | 中等 | 通常极佳 |

这些对比在后续各页中反复出现，每一项都可追溯到 [Atomic scattering factors](scattering-factor.md) 中的散射机制。

---

## 另请参阅

- [3. Beam interaction](../../3-beam-interaction.md) — 本附录所解释的 GUI。
- [Atomic scattering factors](scattering-factor.md) · [Structure factor](structure-factor.md) · [Attenuation & transport](attenuation-transport.md) · [荧光](fluorescence.md)
- [Appendix A1. Coordinate systems](../a1-coordinate-system/1-orientation.md)
- [Appendix A3. Dynamical diffraction (Bloch-wave method)](../a3-bloch-wave/index.md) — 使用这些散射因子的多重散射理论。
