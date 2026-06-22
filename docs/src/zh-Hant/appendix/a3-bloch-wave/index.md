# 附錄 A3. 布洛赫波法的動力學繞射

本附錄概述 ReciPro 的 **繞射模擬器**、**CBED** 與 **HRTEM/STEM** 模擬器所採用的動力學電子繞射理論。ReciPro 遵循 **Bethe / 布洛赫波** 的表述方式。逐步的計算流程（光學位能、透射係數、強度）在 [動力學計算（共通核心）](calculation.md) 中說明。

---

## 晶體中的波動方程式

在晶體的週期性靜電位能中傳播的高速電子，遵循（高能、穩態）薛丁格方程式，可寫成如下形式：

$$\nabla^2 \Psi(\mathbf{r}) + 4\pi^2\left\{\, k_{vac}^2 + \sum_{\mathbf g} U_{\mathbf g}\, e^{2\pi i\,\mathbf g\cdot\mathbf r} \right\}\Psi(\mathbf{r}) = 0$$

- $k_{vac}$ : 電子在真空中的波數。
- $U_{\mathbf g}$ : 對應倒易點陣向量 $\mathbf g$ 的晶體位能的傅立葉分量。由於位能具有點陣週期性，故將其寫為對倒易點陣的傅立葉級數。

---

## 布洛赫定理

由於位能具有晶體點陣的週期性，其解為 **布洛赫波**：

$$\Psi(\mathbf{r}) = b\!\left(\mathbf{k}^{(j)}, \mathbf{r}\right) = u(\mathbf{r})\exp\!\left(2\pi i\,\mathbf{k}^{(j)}\cdot\mathbf{r}\right)$$

- $u(\mathbf r)$ : 一個與晶體點陣具有相同週期性的函式，因此它本身也可在倒易點陣上展開：$u(\mathbf r)=\sum_{\mathbf g} C_{\mathbf g}^{(j)}\exp(2\pi i\,\mathbf g\cdot\mathbf r)$。
- $\mathbf{k}^{(j)}$ : 第 $j$ 個布洛赫波向量。
- $C_{\mathbf g}^{(j)}$ : 第 $j$ 個布洛赫波中射束 $\mathbf g$ 的振幅（本徵向量分量）。

---

## Bethe 動力學方程式

將布洛赫波展開代入波動方程式，得到 **Bethe 動力學方程式** —— 每個射束 $\mathbf g$ 對應一個耦合方程式：

$$\left[\,k^2 - \left(\mathbf{k}^{(j)} + \mathbf{g}\right)^2 + i\,U'_{g,g}\right]C_{\mathbf g}^{(j)} + \sum_{h \neq g}\left(U^C_{g-h} + i\,U'_{g,h}\right)C_{\mathbf h}^{(j)} = 0$$

- $U^C_{\mathbf g}$ : 對應 **彈性** 散射的晶體位能。
- $U'_{\mathbf g}$ : 虛（**吸收**）位能，用以計入 **熱漫散射**（TDS）。它與德拜-沃勒因子如何進入計算，在 [計算核心](calculation.md) 中詳述。

---

## 幾何定義（厄瓦爾德球）

上面出現的向量與純量均定義在厄瓦爾德球上：

![布洛赫波法計算中所用向量與純量的定義](../../../assets/references/Bloch.png){width=500px}

- $\hat{\mathbf n}$ : 垂直於晶體表面的單位向量。
- $\mathbf k$ : 入射波向量（其端點位於厄瓦爾德球上）；$\mathbf k_{vac}$ 為真空波向量。
- $\mathbf g$ : 倒易點陣向量；$\mathbf k + \mathbf g$ 指向倒易點陣點。
- $\mathbf k^{(j)}$ : 第 $j$ 個布洛赫波向量。所有布洛赫波向量具有相同的切向分量（表面處的連續性），僅沿 $\hat{\mathbf n}$ 方向不同：$\mathbf k^{(j)} = \mathbf k + \gamma^{(j)}\hat{\mathbf n}$。
- $\gamma^{(j)}$ : 第 $j$ 個本徵值（$\mathbf k^{(j)}$ 沿 $\hat{\mathbf n}$ 的分量，自 $\mathbf k$ 起量取）。

由幾何關係可得：

$$P_g = 2\,\hat{\mathbf n}\cdot(\mathbf k + \mathbf g), \qquad Q_g = |\mathbf k|^2 - |\mathbf k + \mathbf g|^2 = -\,\mathbf g\cdot(2\mathbf k + \mathbf g)$$

而 **偏離向量** $S_g$（倒易點陣點偏離厄瓦爾德球的程度）以及用於對反射排序的 **評價函式** $R$ 為：

$$S_g = \frac{\sqrt{P_g^{\,2} + 4Q_g}\; -\; P_g}{2}, \qquad R = |\mathbf g|\,Q_g^{\,2}$$

---

## 歸約為本徵值問題

令 $\mathbf{k}^{(j)} = \mathbf{k} + \gamma^{(j)}\hat{\mathbf n}$，並利用 $k^2-(\mathbf k+\mathbf g)^2 = Q_g$ 以及線性化 $(\mathbf k^{(j)}+\mathbf g)^2 \approx (\mathbf k+\mathbf g)^2 + \gamma^{(j)} P_g$，則 Bethe 方程式（在除以 $P_g$ 後）化為標準的 **矩陣本徵值問題**：

$$\mathbf{A}\,\mathbf{C} = \mathbf{C}\,\boldsymbol{\Lambda}, \qquad
A_{gh} = \frac{U^C_{\,g-h} + i\,U'_{g,h}}{P_g}\;\;(g\neq h), \qquad
A_{gg} = \frac{Q_g + i\,U'_{g,g}}{P_g}$$

- $\mathbf{C}$ 的各行即本徵向量 $C^{(j)}_*$（布洛赫波振幅）。
- $\boldsymbol{\Lambda}=\mathrm{diag}\!\left(\lambda^{(1)}, \lambda^{(2)}, \dots\right)$ 包含本徵值 $\lambda^{(j)} = \gamma^{(j)}$。

顯式寫出 —— 將射束按透射束 $0$、隨後 $g$, $h$, $\dots$ 的順序排列 —— 即為：

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

將 $\mathbf{A}$ 對角化即可一次性得到 **所有** 布洛赫波向量與振幅。繞射束的振幅 —— 進而其強度 —— 隨後由入射面與出射面處的邊界條件以及試樣厚度確定。這些步驟、光學（複）位能、德拜-沃勒因子以及透射係數 $T_{\mathbf g}$ 均在 [動力學計算（共通核心）](calculation.md) 中說明。

> **註:** 繞射模擬器的 **Details** 表中所顯示的 $V_{\mathbf g}$ 值為套用相對論修正因子*之前*的原始值。

---

## 另見

- [7. 繞射模擬器](../../7-diffraction-simulator/index.md) —— 動力學繞射圖樣
- [9. HRTEM/STEM 模擬器](../../9-hrtem-stem-simulator/index.md)
- [附錄 A1. 座標系](../a1-coordinate-system/1-orientation.md)
- [動力學計算（共通核心）](calculation.md)
