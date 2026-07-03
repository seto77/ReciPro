# 动力学计算（公共内核）

ReciPro 的衍射模拟器与成像模拟器共用一个公共的**布洛赫波（Bethe）动力学散射内核**，本页对其进行说明（晶体势、Debye–Waller 因子与吸收项、本征值问题、透射系数以及强度）。各方法专用的流程均建立在该内核之上：

- [平行束 SAED](#parallel-beam-saed)
- [HRTEM 成像](hrtem.md)
- [CBED](cbed.md)
- [STEM](stem.md)
- [EBSD](ebsd.md)

关于其底层理论（薛定谔方程、布洛赫定理、Bethe 动力学方程、本征值问题以及埃瓦尔德球的定义），请参见[附录 A3. 布洛赫波法动力学衍射](index.md)。

---

## 常数

$$\gamma = \frac{m}{m_0} = 1 + \frac{e_0 E}{m_0 c^2}, \qquad \beta = \frac{v}{c} = \sqrt{1 - \left(\frac{m_0}{m}\right)^2} = \sqrt{1 - \gamma^{-2}}$$

- $\gamma$ ：相对论修正因子；$E$ ：加速电压；$m_0$、$m$ ：电子的静止质量与相对论质量。
- $\Omega$ ：晶胞体积。
- $k_{vac}$ ：电子在真空中的波数。

---

## 弹性散射的晶体势

对位于位置 $\mathbf r_k$ 的各原子 $k$ 求和，得到弹性散射晶体势的傅里叶系数为

$$U_{\mathbf g}^{C} = \gamma\,\frac{1}{\pi\Omega}\sum_k f_k(\mathbf g)\,\exp\!\left[2\pi i\,\mathbf g\cdot\mathbf r_k\right]T_k(\mathbf g, M_k)$$

其中**原子散射因子**采用高斯参数化 $(a_i, b_i)$，

$$f_k(\mathbf g) = \sum_i a_i\exp\!\left[-b_i\,\frac{|\mathbf g|^2}{4}\right]$$

而 $T_k$ 为 **Debye–Waller（温度）因子**。对于各向同性温度因子 $M_k$，

$$T_k(\mathbf g, M_k) = \exp\!\left[-M_k\,\frac{|\mathbf g|^2}{4}\right]$$

而对于各向异性的原子位移张量 $\mathbf U$，

$$T_k(\mathbf g) = \exp\!\left[-2\pi\,\mathbf g^{t}\mathbf U\,\mathbf g\right]$$

其二次型为

$$\mathbf g^{t}\mathbf U\,\mathbf g = \begin{pmatrix} g_x & g_y & g_z\end{pmatrix}\begin{pmatrix} U_{11} & U_{12} & U_{13}\\ U_{12} & U_{22} & U_{23}\\ U_{13} & U_{23} & U_{33}\end{pmatrix}\begin{pmatrix} g_x\\ g_y\\ g_z\end{pmatrix} = g_x^2 U_{11} + g_y^2 U_{22} + g_z^2 U_{33} + 2\!\left(g_x g_y U_{12} + g_y g_z U_{23} + g_x g_z U_{13}\right)$$

$\mathbf g$ 的笛卡尔分量由倒易基矢和米勒指数得到：

$$\begin{pmatrix} g_x\\ g_y\\ g_z\end{pmatrix} = \begin{pmatrix} a_x^{*} & b_x^{*} & c_x^{*}\\ a_y^{*} & b_y^{*} & c_y^{*}\\ a_z^{*} & b_z^{*} & c_z^{*}\end{pmatrix}\begin{pmatrix} h\\ k\\ l\end{pmatrix} = \begin{pmatrix} h\,a_x^{*} + k\,b_x^{*} + l\,c_x^{*}\\ h\,a_y^{*} + k\,b_y^{*} + l\,c_y^{*}\\ h\,a_z^{*} + k\,b_z^{*} + l\,c_z^{*}\end{pmatrix}$$

!!! note
    衍射模拟器的 **Details** 表中显示的 $U_{\mathbf g}$ 值是在应用相对论因子 $\gamma$ *之前*的原始值。

---

## 吸收势（热漫散射）

考虑热漫散射（TDS）的虚部（吸收）势为

$$U'_{g,h} = \gamma\,\frac{1}{\pi\Omega}\sum_k f'_k(\mathbf g,\mathbf h)\,\exp\!\left[2\pi i(\mathbf g-\mathbf h)\cdot\mathbf r_k\right]T_k(\mathbf g-\mathbf h, M_k)$$

其中**吸收散射因子**为

$$f'_k(\mathbf g,\mathbf h) = \frac{2h}{\beta\, m_0\, c}\sum_i\sum_j a_i a_j\left[\frac{1}{b_i+b_j}\exp\!\left\{-\frac{b_i b_j}{b_i+b_j}\,\frac{|\mathbf g-\mathbf h|^2}{4}\right\} - \frac{1}{b_i+b_j+2M_k}\exp\!\left\{-\frac{b_i b_j - M_k^2}{b_i+b_j+2M_k}\,\frac{|\mathbf g-\mathbf h|^2}{4}\right\}\right]$$

此处前置因子 $2h/(\beta m_0 c)$ 中的 $h$ 是**普朗克常数**（并非束指数）。系数 $U^{C}$ 与 $U'$ 是[附录 A3](index.md)中结构矩阵 $\mathbf A$ 的元素。

---

## 从本征解到衍射强度

将结构矩阵对角化（参见[附录 A3](index.md)）可得到本征值 $\lambda^{(j)}$ 和布洛赫波振幅 $C_{\mathbf g}^{(j)}$。出射面上的波振幅——即**透射系数** $T_{\mathbf g}$——在样品厚度 $t$ 处为

$$\begin{pmatrix} T_0\\ T_g\\ T_h\\ \vdots\end{pmatrix}
= e^{-2\pi i(\mathbf k_{vac}\cdot\mathbf n)\,t}
\begin{pmatrix} e^{\pi i P_0 t} & 0 & 0 & \cdots\\ 0 & e^{\pi i P_g t} & 0 & \cdots\\ 0 & 0 & e^{\pi i P_h t} & \cdots\\ \vdots & \vdots & \vdots & \ddots\end{pmatrix}
\begin{pmatrix} C_0^{(1)} & C_0^{(2)} & C_0^{(3)} & \cdots\\ C_g^{(1)} & C_g^{(2)} & C_g^{(3)} & \cdots\\ C_h^{(1)} & C_h^{(2)} & C_h^{(3)} & \cdots\\ \vdots & \vdots & \vdots & \ddots\end{pmatrix}
\begin{pmatrix} e^{2\pi i\lambda^{(1)} t} & 0 & 0 & \cdots\\ 0 & e^{2\pi i\lambda^{(2)} t} & 0 & \cdots\\ 0 & 0 & e^{2\pi i\lambda^{(3)} t} & \cdots\\ \vdots & \vdots & \vdots & \ddots\end{pmatrix}
\begin{pmatrix} \alpha^{(1)}\\ \alpha^{(2)}\\ \alpha^{(3)}\\ \vdots\end{pmatrix}$$

或者，逐分量地写为

$$T_{\mathbf g} = e^{-2\pi i(\mathbf k_{vac}\cdot\mathbf n)\,t}\; e^{\pi i P_g t}\sum_j C_{\mathbf g}^{(j)}\,e^{2\pi i\lambda^{(j)} t}\,\alpha^{(j)}$$

- $\alpha^{(j)}$ ：各布洛赫波的权重（激发）系数，由入射面处的边界条件确定。
- $t$ ：样品厚度。

于是束 $\mathbf g$ 的衍射强度为

$$I_{\mathbf g} = \left|T_{\mathbf g}\right|^2$$

---

## 平行束 SAED 计算 { #parallel-beam-saed }

普通 SAED（选区电子衍射）被当作单一入射方向的**平行束衍射**来处理。与 CBED 不同，它不会在会聚孔径内扫描众多 $\mathbf K$ 点。当前的晶体取向和加速电压定义了一个入射波矢 $\mathbf k_0$，ReciPro 在该条件下计算每个反射 $\mathbf g$ 的位置和强度。

该计算可按如下方式组织。

1. 利用晶体取向、加速电压、波长、相机长度和探测器几何来定义入射真空波矢 $\mathbf k_{vac}$ 和探测器平面。
2. 应用来自平均内势 $U_0$ 的折射修正，得到晶体参考波矢 $\mathbf k_0$。
3. 枚举候选倒易点阵矢量 $\mathbf g$，并通过 $Q_g=|\mathbf k_0|^2-|\mathbf k_0+\mathbf g|^2$ 和偏离矢量 $S_g$ 等量来评估其与埃瓦尔德球的距离。
4. 使用所选的强度模式计算每个反射的强度。
5. 将 $\mathbf k_0+\mathbf g$ 的方向投影到探测器平面上，并将其绘制为衍射斑点。

ReciPro 的 SAED 模式主要提供以下强度模型。

| 模式 | 计算 | 典型用途 |
|------|-------------|-------------|
| 仅偏离矢量 | 仅根据反射与埃瓦尔德球的接近程度来估计强度。不使用结构因子。 | 快速检查斑点位置和晶带轴几何。 |
| 运动学 + 偏离矢量 | 将 $\lvert F_{\mathbf g}\rvert^2$ 与偏离矢量阻尼结合使用。不包含多次散射。 | 薄样品、弱衍射以及消光规则检查。 |
| 动力学理论 | 使用本页的布洛赫波内核得到 $T_{\mathbf g}(t)$，并令 $I_{\mathbf g}=\lvert T_{\mathbf g}\rvert^2$。 | 厚度依赖性、多次散射以及强电子衍射反射。 |

倒易点阵点的显示模式，例如实心球截面和高斯斑点，主要控制绘制轮廓。在动力学理论模式中，物理反射强度由布洛赫波值 $|T_{\mathbf g}|^2$ 决定，随后该强度被赋予所选的显示轮廓。

PED 可视为将此平行束 SAED 计算对进动方向积分，而 CBED 可视为在衍射盘内排布众多入射方向。

---

## 平均内势与折射

当电子从真空进入晶体时，平均内势 $U_0$ 会使晶体内部的参考波矢发生轻微变化。平行于表面的分量由边界条件确定，因此真空波矢 $\mathbf k_{vac}$ 与晶体参考波矢 $\mathbf k_0$ 可写为

$$|\mathbf k_0|^2 = k_{vac}^2 + U_0, \qquad \mathbf k_0 = \mathbf k_{vac} + x\,\hat{\mathbf n}$$

其中 $x$ 是沿表面法线方向的修正。它由下式求得

$$x^2 + 2(\hat{\mathbf n}\cdot\mathbf k_{vac})x - U_0 = 0$$

这个经过折射的 $\mathbf k_0$ 在[概览页面](index.md)中评估 $P_g$、$Q_g$、偏离矢量以及结构矩阵 $\mathbf A$ 时使用。吸收势还具有一个 $\mathbf g=\mathbf 0$ 分量 $U'_0$，它对在晶体中传播的波起到公共的平均衰减作用。

---

## 束选取

布洛赫波计算无法包含无限多的倒易点阵矢量，因此 ReciPro 选取一个有限的束集合 $\{\mathbf g\}$。其排序量为

$$R_{\mathbf g}=|\mathbf g|\,Q_{\mathbf g}^{\,2}$$

$R_{\mathbf g}$ 较小的束会被优先纳入。这倾向于选取倒易点阵矢量较短且同时靠近埃瓦尔德球的束。

在实际计算中，重要的是检查当布洛赫波的最大数目增加时强度或图像变化了多少。强晶带轴条件以及具有 HOLZ 线细节的 CBED 花样可能需要数百条束，而偏离晶带轴的条件可能用较少的束即可收敛。

---

## 求解器选择

在选定有限束集合之后，ReciPro 主要使用两种等价的方式来获得透射系数。

| 方法 | 特点 | 典型用途 |
|--------|---------|-------------|
| 本征值法 | 将结构矩阵 $\mathbf A$ 对角化，得到本征值 $\lambda^{(j)}$ 和本征矢量 $C_{\mathbf g}^{(j)}$。随后通过 $e^{2\pi i\lambda^{(j)}t}$ 评估厚度依赖性。 | 扫描众多深度或能量的厚度系列、CBED 和 EBSD 计算 |
| 矩阵指数法 | 直接评估散射矩阵 $\exp(2\pi i\mathbf A t)$，而不显式使用本征分解。 | 单一厚度的 STEM 计算和分层积分计算 |

两种方法求解的是同一个 Bethe 方程。在实现中，代码根据束数、厚度数组以及原生库是否可用，在本征值法、矩阵指数法、托管 .NET 例程和原生 Eigen 库之间进行选择。

---

## 收敛性检查

对于动力学计算，检查基组是否足够大与公式本身同等重要。一个有用的诊断量是当束数从 $N-\Delta N$ 增加到 $N$ 时的相对变化：

$$\Delta I_N=\frac{|I_N-I_{N-\Delta N}|}{I_N}$$

对于 STEM，请连同探测器角度设置一起检查。对于 CBED，请检视衍射盘内部和 HOLZ 线。对于 EBSD，还应比较 master pattern 中的菊池带宽度和背景。这将数值收敛与模拟结果中可见的物理特征联系起来。

---

## 参见

- [附录 A3. 布洛赫波法动力学衍射](index.md)
- [7.2. SAED 模拟](../../7-diffraction-simulator/1-saed-simulation.md)
- [7.4. CBED 模拟](../../7-diffraction-simulator/3-cbed-simulation.md)
- [7. 衍射模拟器](../../7-diffraction-simulator/index.md)
