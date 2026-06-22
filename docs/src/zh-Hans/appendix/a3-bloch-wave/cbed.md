# CBED 计算

CBED（会聚束电子衍射）将[动力学核心](calculation.md)应用于许多入射束方向，然后把结果排列进衍射盘中。SAED 只有一个入射方向；CBED 则把物镜光阑内的每个点都视为一个**部分入射平面波**，并对其中每一个分别求解布洛赫波问题。

---

## 会聚束的表示

在入射面处，会聚探针可以用探针位置 $\mathbf R_0$、透镜相位 $\chi(\mathbf K)$ 和光阑函数 $A(\mathbf K)$ 写成平面波之和：

$$\psi_{\mathrm{in}}(\mathbf R,0)=\sum_{\mathbf K\in\mathrm{aperture}} A(\mathbf K)\,
\exp(-2\pi i\,\mathbf K\cdot\mathbf R_0)\,
\exp[-i\chi(\mathbf K)]\,
\exp(2\pi i\,\mathbf K\cdot\mathbf R)$$

这里 $\mathbf K$ 是入射波矢中平行于样品表面的分量。对于一个会聚半角为 $\alpha$、电子波长为 $\lambda$ 的理想圆形光阑，有

$$A(\mathbf K)=
\begin{cases}
1 & (|\mathbf K|\leq \sin\alpha/\lambda)\\
0 & (|\mathbf K|> \sin\alpha/\lambda)
\end{cases}$$

一个有代表性的透镜相位，用欠焦 $\Delta f$ 和球差 $C_s$ 表示为

$$\chi(\mathbf K)=\pi\lambda|\mathbf K|^2\Delta f+\frac{\pi}{2}C_s\lambda^3|\mathbf K|^4+\cdots$$

在 ReciPro 中，该表达式由像差、光阑和会聚角的设置来控制。

---

## 对每个方向的动力学计算

对于 CBED，光阑内的每个 $\mathbf K$ 都被视为一束平行入射波。其概念性流程为：

1. 由 $\mathbf K$ 和样品表面法线确定折射后的参考波矢 $\mathbf k_0(\mathbf K)$。
2. 用排序量 $R_{\mathbf g}=|\mathbf g|Q_{\mathbf g}^2$ 选取反射束。
3. 构建结构矩阵 $\mathbf A$，并计算厚度 $t$ 处的透射系数 $T_{\mathbf g}(t;\mathbf K)$。

这就是[动力学核心](calculation.md)中的透射系数计算，对每个采样的入射方向重复进行。对于厚度系列，给定方向的本征解可以重复使用，只需更新传播因子即可。

---

## 衍射盘的组装

将所有 $\mathbf K$ 方向的出射波放入衍射面中，便得到透射盘和各衍射盘内部的强度。若 $\mathbf Q$ 为衍射面坐标，则位置平均的 CBED 或低相干条件可以近似为非相干强度之和：

$$I_{\mathrm{CBED}}(\mathbf Q)=
\sum_{\mathbf K\in\mathrm{aperture}}
\left|\psi_{\mathbf K}(\mathbf Q,t)\right|^2$$

对于像 LACBED 那样、需要在更大范围内保持相位相干的模式，则必须先把振幅相加，然后再取强度。

---

## CBED 能显示什么

CBED 把布洛赫波解的厚度依赖性，以衍射盘内部强度结构的形式可视化呈现。

- 改变厚度会改变盘内振荡、HOLZ 线和 Kossel-Mollenstedt 条纹。
- 改变入射取向会改变哪些反射被强烈激发。
- 增大会聚角会展宽衍射盘，并能揭示重叠以及高阶劳厄带的信息。

因此，CBED 是把布洛赫波结果作为衍射面上的盘状图样来观察的最直接方式。在 ReciPro 中，最好将其理解为会聚束离散化、每个方向一个动力学解，以及重新排列为盘阵列三者的组合。

---

## 实用参数

- **束数**：强晶带轴条件和 HOLZ 线细节需要大量反射束。请检查在增大最大布洛赫波数时盘内部如何变化。
- **角度采样**：如果光阑内的 $\mathbf K$ 采样过于粗糙，盘内强度就会变得颗粒状。更大的会聚角需要更细的采样。
- **厚度**：厚度系列得益于本征值法，因为一个本征解可以重复用于许多厚度。
- **相干性**：请区分非相干强度求和足够的条件，与需要相干振幅求和的条件。

## 另请参阅

- [动力学计算（共用核心）](calculation.md)
- [附录 A3. 用布洛赫波法处理的动力学衍射](index.md)
- [7.4. CBED 模拟](../../7-diffraction-simulator/3-cbed-simulation.md)
