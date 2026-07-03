# 附录 A3. 布洛赫波法的动力学衍射

本附录概述 ReciPro 的 **衍射模拟器**、**CBED** 与 **HRTEM/STEM** 模拟器所采用的动力学电子衍射理论。ReciPro 遵循 **Bethe / 布洛赫波** 的表述方式。逐步的计算流程（光学势、透射系数、强度）在 [动力学计算（公共内核）](calculation.md) 中说明。

---

## 晶体中的波动方程

在晶体的周期性静电势中传播的高速电子，遵循（高能、稳态）薛定谔方程，可写成如下形式：

$$\nabla^2 \Psi(\mathbf{r}) + 4\pi^2\left\{\, k_{vac}^2 + \sum_{\mathbf g} U_{\mathbf g}\, e^{2\pi i\,\mathbf g\cdot\mathbf r} \right\}\Psi(\mathbf{r}) = 0$$

- $k_{vac}$ : 电子在真空中的波数。
- $U_{\mathbf g}$ : 对应倒易点阵矢量 $\mathbf g$ 的晶体势的傅里叶分量。由于势具有点阵周期性，故将其写为对倒易点阵的傅里叶级数。

---

## 布洛赫定理

由于势具有晶体点阵的周期性，其解为 **布洛赫波**：

$$\Psi(\mathbf{r}) = b\!\left(\mathbf{k}^{(j)}, \mathbf{r}\right) = u(\mathbf{r})\exp\!\left(2\pi i\,\mathbf{k}^{(j)}\cdot\mathbf{r}\right)$$

- $u(\mathbf r)$ : 一个与晶体点阵具有相同周期性的函数，因此它本身也可在倒易点阵上展开：$u(\mathbf r)=\sum_{\mathbf g} C_{\mathbf g}^{(j)}\exp(2\pi i\,\mathbf g\cdot\mathbf r)$。
- $\mathbf{k}^{(j)}$ : 第 $j$ 个布洛赫波矢。
- $C_{\mathbf g}^{(j)}$ : 第 $j$ 个布洛赫波中射束 $\mathbf g$ 的振幅（本征矢量分量）。

---

## Bethe 动力学方程

将布洛赫波展开代入波动方程，得到 **Bethe 动力学方程** —— 每个射束 $\mathbf g$ 对应一个耦合方程：

$$\left[\,k^2 - \left(\mathbf{k}^{(j)} + \mathbf{g}\right)^2 + i\,U'_{g,g}\right]C_{\mathbf g}^{(j)} + \sum_{h \neq g}\left(U^C_{g-h} + i\,U'_{g,h}\right)C_{\mathbf h}^{(j)} = 0$$

- $U^C_{\mathbf g}$ : 对应 **弹性** 散射的晶体势。
- $U'_{\mathbf g}$ : 虚（**吸收**）势，用以计入 **热漫散射**（TDS）。它与德拜-沃勒因子如何进入计算，在 [计算核心](calculation.md) 中详述。

---

## 几何定义（埃瓦尔德球）

上面出现的矢量与标量均定义在埃瓦尔德球上：

![布洛赫波法计算中所用矢量与标量的定义](../../../assets/references/Bloch.png){width=500px}

- $\hat{\mathbf n}$ : 垂直于晶体表面的单位矢量。
- $\mathbf k$ : 入射波矢（其端点位于埃瓦尔德球上）；$\mathbf k_{vac}$ 为真空波矢。
- $\mathbf g$ : 倒易点阵矢量；$\mathbf k + \mathbf g$ 指向倒易点阵点。
- $\mathbf k^{(j)}$ : 第 $j$ 个布洛赫波矢。所有布洛赫波矢具有相同的切向分量（表面处的连续性），仅沿 $\hat{\mathbf n}$ 方向不同：$\mathbf k^{(j)} = \mathbf k + \gamma^{(j)}\hat{\mathbf n}$。
- $\gamma^{(j)}$ : 第 $j$ 个本征值（$\mathbf k^{(j)}$ 沿 $\hat{\mathbf n}$ 的分量，自 $\mathbf k$ 起量取）。

由几何关系可得：

$$P_g = 2\,\hat{\mathbf n}\cdot(\mathbf k + \mathbf g), \qquad Q_g = |\mathbf k|^2 - |\mathbf k + \mathbf g|^2 = -\,\mathbf g\cdot(2\mathbf k + \mathbf g)$$

而 **偏离矢量** $S_g$（倒易点阵点偏离埃瓦尔德球的程度）以及用于对反射排序的 **评价函数** $R$ 为：

$$S_g = \frac{\sqrt{P_g^{\,2} + 4Q_g}\; -\; P_g}{2}, \qquad R = |\mathbf g|\,Q_g^{\,2}$$

---

## 归约为本征值问题

令 $\mathbf{k}^{(j)} = \mathbf{k} + \gamma^{(j)}\hat{\mathbf n}$，并利用 $k^2-(\mathbf k+\mathbf g)^2 = Q_g$ 以及线性化 $(\mathbf k^{(j)}+\mathbf g)^2 \approx (\mathbf k+\mathbf g)^2 + \gamma^{(j)} P_g$，则 Bethe 方程（在除以 $P_g$ 后）化为标准的 **矩阵本征值问题**：

$$\mathbf{A}\,\mathbf{C} = \mathbf{C}\,\boldsymbol{\Lambda}, \qquad
A_{gh} = \frac{U^C_{\,g-h} + i\,U'_{g,h}}{P_g}\;\;(g\neq h), \qquad
A_{gg} = \frac{Q_g + i\,U'_{g,g}}{P_g}$$

- $\mathbf{C}$ 的各列即本征矢量 $C^{(j)}_*$（布洛赫波振幅）。
- $\boldsymbol{\Lambda}=\mathrm{diag}\!\left(\lambda^{(1)}, \lambda^{(2)}, \dots\right)$ 包含本征值 $\lambda^{(j)} = \gamma^{(j)}$。

显式写出 —— 将射束按透射束 $0$、随后 $g$, $h$, $\dots$ 的顺序排列 —— 即为：

$$
\begin{aligned}
&\begin{pmatrix}
(Q_0 + i\,U'_{0,0})/P_0 & (U^C_{-g} + i\,U'_{0,g})/P_0 & (U^C_{-h} + i\,U'_{0,h})/P_0 & \cdots \\
(U^C_{g} + i\,U'_{g,0})/P_g & (Q_g + i\,U'_{g,g})/P_g & (U^C_{g-h} + i\,U'_{g,h})/P_g & \cdots \\
(U^C_{h} + i\,U'_{h,0})/P_h & (U^C_{h-g} + i\,U'_{h,g})/P_h & (Q_h + i\,U'_{h,h})/P_h & \cdots \\
\vdots & \vdots & \vdots & \ddots
\end{pmatrix}
\begin{pmatrix}
C^{(1)}_0 & C^{(2)}_0 & C^{(3)}_0 & \cdots \\
C^{(1)}_g & C^{(2)}_g & C^{(3)}_g & \cdots \\
C^{(1)}_h & C^{(2)}_h & C^{(3)}_h & \cdots \\
\vdots & \vdots & \vdots & \ddots
\end{pmatrix} \\[1.2ex]
&\qquad=
\begin{pmatrix}
C^{(1)}_0 & C^{(2)}_0 & C^{(3)}_0 & \cdots \\
C^{(1)}_g & C^{(2)}_g & C^{(3)}_g & \cdots \\
C^{(1)}_h & C^{(2)}_h & C^{(3)}_h & \cdots \\
\vdots & \vdots & \vdots & \ddots
\end{pmatrix}
\begin{pmatrix}
\lambda^{(1)} & 0 & 0 & \cdots \\
0 & \lambda^{(2)} & 0 & \cdots \\
0 & 0 & \lambda^{(3)} & \cdots \\
\vdots & \vdots & \vdots & \ddots
\end{pmatrix}
\end{aligned}
$$

将 $\mathbf{A}$ 对角化即可一次性得到 **所有** 布洛赫波矢与振幅。衍射束的振幅 —— 进而其强度 —— 随后由入射面与出射面处的边界条件以及样品厚度确定。这些步骤、光学（复）势、德拜-沃勒因子以及透射系数 $T_{\mathbf g}$ 均在 [动力学计算（公共内核）](calculation.md) 中说明。

> **注:** 衍射模拟器的 **Details** 表中所显示的 $V_{\mathbf g}$ 值为应用相对论修正因子*之前*的原始值。

---

## 另请参阅

- [7. 衍射模拟器](../../7-diffraction-simulator/index.md) —— 动力学衍射图样
- [9. HRTEM/STEM 模拟器](../../9-hrtem-stem-simulator/index.md)
- [附录 A1. 坐标系](../a1-coordinate-system/1-orientation.md)
- [动力学计算（公共内核）](calculation.md)
