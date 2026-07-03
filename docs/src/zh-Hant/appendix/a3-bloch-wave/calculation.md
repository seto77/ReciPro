# 動力學計算（共通核心）

ReciPro 的繞射模擬器與成像模擬器共用一個共通的**布洛赫波（Bethe）動力學散射核心**，本頁對其進行說明（晶體位能、Debye–Waller 因子與吸收項、本徵值問題、透射係數以及強度）。各方法專用的流程均建立在此內核之上：

- [平行束 SAED](#parallel-beam-saed)
- [HRTEM 成像](hrtem.md)
- [CBED](cbed.md)
- [STEM](stem.md)
- [EBSD](ebsd.md)

關於其底層理論（薛丁格方程式、布洛赫定理、Bethe 動力學方程式、本徵值問題以及厄瓦爾德球的定義），請參見[附錄 A3. 布洛赫波法動力學繞射](index.md)。

---

## 常數

$$\gamma = \frac{m}{m_0} = 1 + \frac{e_0 E}{m_0 c^2}, \qquad \beta = \frac{v}{c} = \sqrt{1 - \left(\frac{m_0}{m}\right)^2} = \sqrt{1 - \gamma^{-2}}$$

- $\gamma$ ：相對論修正因子；$E$ ：加速電壓；$m_0$、$m$ ：電子的靜止質量與相對論質量。
- $\Omega$ ：晶胞體積。
- $k_{vac}$ ：電子在真空中的波數。

---

## 彈性散射的晶體位能

對位於位置 $\mathbf r_k$ 的各原子 $k$ 求和，得到彈性散射晶體位能的傅立葉係數為

$$U_{\mathbf g}^{C} = \gamma\,\frac{1}{\pi\Omega}\sum_k f_k(\mathbf g)\,\exp\!\left[2\pi i\,\mathbf g\cdot\mathbf r_k\right]T_k(\mathbf g, M_k)$$

其中**原子散射因子**採用高斯參數化 $(a_i, b_i)$，

$$f_k(\mathbf g) = \sum_i a_i\exp\!\left[-b_i\,\frac{|\mathbf g|^2}{4}\right]$$

而 $T_k$ 為 **Debye–Waller（溫度）因子**。對於各向同性溫度因子 $M_k$，

$$T_k(\mathbf g, M_k) = \exp\!\left[-M_k\,\frac{|\mathbf g|^2}{4}\right]$$

而對於各向異性的原子位移張量 $\mathbf U$，

$$T_k(\mathbf g) = \exp\!\left[-2\pi\,\mathbf g^{t}\mathbf U\,\mathbf g\right]$$

其二次型為

$$\mathbf g^{t}\mathbf U\,\mathbf g = \begin{pmatrix} g_x & g_y & g_z\end{pmatrix}\begin{pmatrix} U_{11} & U_{12} & U_{13}\\ U_{12} & U_{22} & U_{23}\\ U_{13} & U_{23} & U_{33}\end{pmatrix}\begin{pmatrix} g_x\\ g_y\\ g_z\end{pmatrix} = g_x^2 U_{11} + g_y^2 U_{22} + g_z^2 U_{33} + 2\!\left(g_x g_y U_{12} + g_y g_z U_{23} + g_x g_z U_{13}\right)$$

$\mathbf g$ 的笛卡兒分量由倒易基向量和米勒指數得到：

$$\begin{pmatrix} g_x\\ g_y\\ g_z\end{pmatrix} = \begin{pmatrix} a_x^{*} & b_x^{*} & c_x^{*}\\ a_y^{*} & b_y^{*} & c_y^{*}\\ a_z^{*} & b_z^{*} & c_z^{*}\end{pmatrix}\begin{pmatrix} h\\ k\\ l\end{pmatrix} = \begin{pmatrix} h\,a_x^{*} + k\,b_x^{*} + l\,c_x^{*}\\ h\,a_y^{*} + k\,b_y^{*} + l\,c_y^{*}\\ h\,a_z^{*} + k\,b_z^{*} + l\,c_z^{*}\end{pmatrix}$$

!!! note
    繞射模擬器的 **Details** 表中顯示的 $U_{\mathbf g}$ 值是在套用相對論因子 $\gamma$ *之前*的原始值。

---

## 吸收位能（熱漫散射）

考慮熱漫散射（TDS）的虛部（吸收）位能為

$$U'_{g,h} = \gamma\,\frac{1}{\pi\Omega}\sum_k f'_k(\mathbf g,\mathbf h)\,\exp\!\left[2\pi i(\mathbf g-\mathbf h)\cdot\mathbf r_k\right]T_k(\mathbf g-\mathbf h, M_k)$$

其中**吸收散射因子**為

$$f'_k(\mathbf g,\mathbf h) = \frac{2h}{\beta\, m_0\, c}\sum_i\sum_j a_i a_j\left[\frac{1}{b_i+b_j}\exp\!\left\{-\frac{b_i b_j}{b_i+b_j}\,\frac{|\mathbf g-\mathbf h|^2}{4}\right\} - \frac{1}{b_i+b_j+2M_k}\exp\!\left\{-\frac{b_i b_j - M_k^2}{b_i+b_j+2M_k}\,\frac{|\mathbf g-\mathbf h|^2}{4}\right\}\right]$$

此處前置因子 $2h/(\beta m_0 c)$ 中的 $h$ 是**普朗克常數**（並非束指數）。係數 $U^{C}$ 與 $U'$ 是[附錄 A3](index.md)中結構矩陣 $\mathbf A$ 的元素。

---

## 從本徵解到繞射強度

將結構矩陣對角化（參見[附錄 A3](index.md)）可得到本徵值 $\lambda^{(j)}$ 和布洛赫波振幅 $C_{\mathbf g}^{(j)}$。出射面上的波振幅——即**透射係數** $T_{\mathbf g}$——在試樣厚度 $t$ 處為

$$\begin{pmatrix} T_0\\ T_g\\ T_h\\ \vdots\end{pmatrix}
= e^{-2\pi i(\mathbf k_{vac}\cdot\mathbf n)\,t}
\begin{pmatrix} e^{\pi i P_0 t} & 0 & 0 & \cdots\\ 0 & e^{\pi i P_g t} & 0 & \cdots\\ 0 & 0 & e^{\pi i P_h t} & \cdots\\ \vdots & \vdots & \vdots & \ddots\end{pmatrix}
\begin{pmatrix} C_0^{(1)} & C_0^{(2)} & C_0^{(3)} & \cdots\\ C_g^{(1)} & C_g^{(2)} & C_g^{(3)} & \cdots\\ C_h^{(1)} & C_h^{(2)} & C_h^{(3)} & \cdots\\ \vdots & \vdots & \vdots & \ddots\end{pmatrix}
\begin{pmatrix} e^{2\pi i\lambda^{(1)} t} & 0 & 0 & \cdots\\ 0 & e^{2\pi i\lambda^{(2)} t} & 0 & \cdots\\ 0 & 0 & e^{2\pi i\lambda^{(3)} t} & \cdots\\ \vdots & \vdots & \vdots & \ddots\end{pmatrix}
\begin{pmatrix} \alpha^{(1)}\\ \alpha^{(2)}\\ \alpha^{(3)}\\ \vdots\end{pmatrix}$$

或者，逐分量地寫為

$$T_{\mathbf g} = e^{-2\pi i(\mathbf k_{vac}\cdot\mathbf n)\,t}\; e^{\pi i P_g t}\sum_j C_{\mathbf g}^{(j)}\,e^{2\pi i\lambda^{(j)} t}\,\alpha^{(j)}$$

- $\alpha^{(j)}$ ：各布洛赫波的權重（激發）係數，由入射面處的邊界條件確定。
- $t$ ：試樣厚度。

於是束 $\mathbf g$ 的繞射強度為

$$I_{\mathbf g} = \left|T_{\mathbf g}\right|^2$$

---

## 平行束 SAED 計算 { #parallel-beam-saed }

普通 SAED（選區電子繞射）被當作單一入射方向的**平行束繞射**來處理。與 CBED 不同，它不會在會聚光闌內掃描眾多 $\mathbf K$ 點。當前的晶體取向和加速電壓定義了一個入射波向量 $\mathbf k_0$，ReciPro 在該條件下計算每個反射 $\mathbf g$ 的位置和強度。

該計算可按如下方式組織。

1. 利用晶體取向、加速電壓、波長、相機長度和偵測器幾何來定義入射真空波向量 $\mathbf k_{vac}$ 和偵測器平面。
2. 套用來自平均內位能 $U_0$ 的折射修正，得到晶體參考波向量 $\mathbf k_0$。
3. 列舉候選倒易點陣向量 $\mathbf g$，並透過 $Q_g=|\mathbf k_0|^2-|\mathbf k_0+\mathbf g|^2$ 和偏離向量 $S_g$ 等量來評估其與厄瓦爾德球的距離。
4. 使用所選的強度模式計算每個反射的強度。
5. 將 $\mathbf k_0+\mathbf g$ 的方向投影到偵測器平面上，並將其繪製為繞射斑點。

ReciPro 的 SAED 模式主要提供以下強度模型。

| 模式 | 計算 | 典型用途 |
|------|-------------|-------------|
| 僅偏離向量 | 僅根據反射與厄瓦爾德球的接近程度來估計強度。不使用結構因子。 | 快速檢查斑點位置和晶帶軸幾何。 |
| 運動學 + 偏離向量 | 將 $\lvert F_{\mathbf g}\rvert^2$ 與偏離向量阻尼結合使用。不包含多重散射。 | 薄試樣、弱繞射以及消光規則檢查。 |
| 動力學理論 | 使用本頁的布洛赫波內核得到 $T_{\mathbf g}(t)$，並令 $I_{\mathbf g}=\lvert T_{\mathbf g}\rvert^2$。 | 厚度依賴性、多重散射以及強電子繞射反射。 |

倒易點陣點的顯示模式，例如實心球截面和高斯斑點，主要控制繪製輪廓。在動力學理論模式中，物理反射強度由布洛赫波值 $|T_{\mathbf g}|^2$ 決定，隨後該強度被賦予所選的顯示輪廓。

PED 可視為將此平行束 SAED 計算對進動方向積分，而 CBED 可視為在繞射盤內排布眾多入射方向。

---

## 平均內位能與折射

當電子從真空進入晶體時，平均內位能 $U_0$ 會使晶體內部的參考波向量發生輕微變化。平行於表面的分量由邊界條件確定，因此真空波向量 $\mathbf k_{vac}$ 與晶體參考波向量 $\mathbf k_0$ 可寫為

$$|\mathbf k_0|^2 = k_{vac}^2 + U_0, \qquad \mathbf k_0 = \mathbf k_{vac} + x\,\hat{\mathbf n}$$

其中 $x$ 是沿表面法線方向的修正。它由下式求得

$$x^2 + 2(\hat{\mathbf n}\cdot\mathbf k_{vac})x - U_0 = 0$$

這個經過折射的 $\mathbf k_0$ 在[概覽頁面](index.md)中評估 $P_g$、$Q_g$、偏離向量以及結構矩陣 $\mathbf A$ 時使用。吸收位能還具有一個 $\mathbf g=\mathbf 0$ 分量 $U'_0$，它對在晶體中傳播的波起到共通的平均衰減作用。

---

## 束選取

布洛赫波計算無法包含無限多的倒易點陣向量，因此 ReciPro 選取一個有限的束集合 $\{\mathbf g\}$。其排序量為

$$R_{\mathbf g}=|\mathbf g|\,Q_{\mathbf g}^{\,2}$$

$R_{\mathbf g}$ 較小的束會被優先納入。這傾向於選取倒易點陣向量較短且同時靠近厄瓦爾德球的束。

在實際計算中，重要的是檢查當布洛赫波的最大數目增加時強度或影像變化了多少。強晶帶軸條件以及具有 HOLZ 線細節的 CBED 花樣可能需要數百條束，而偏離晶帶軸的條件可能用較少的束即可收斂。

---

## 求解器選擇

在選定有限束集合之後，ReciPro 主要使用兩種等價的方式來獲得透射係數。

| 方法 | 特點 | 典型用途 |
|--------|---------|-------------|
| 本徵值法 | 將結構矩陣 $\mathbf A$ 對角化，得到本徵值 $\lambda^{(j)}$ 和本徵向量 $C_{\mathbf g}^{(j)}$。隨後透過 $e^{2\pi i\lambda^{(j)}t}$ 評估厚度依賴性。 | 掃描眾多深度或能量的厚度系列、CBED 和 EBSD 計算 |
| 矩陣指數法 | 直接評估散射矩陣 $\exp(2\pi i\mathbf A t)$，而不顯式使用本徵分解。 | 單一厚度的 STEM 計算和分層積分計算 |

兩種方法求解的是同一個 Bethe 方程式。在實作中，程式碼根據束數、厚度陣列以及原生函式庫是否可用，在本徵值法、矩陣指數法、託管 .NET 常式和原生 Eigen 函式庫之間進行選擇。

---

## 收斂性檢查

對於動力學計算，檢查基組是否足夠大與公式本身同等重要。一個有用的診斷量是當束數從 $N-\Delta N$ 增加到 $N$ 時的相對變化：

$$\Delta I_N=\frac{|I_N-I_{N-\Delta N}|}{I_N}$$

對於 STEM，請連同偵測器角度設定一起檢查。對於 CBED，請檢視繞射盤內部和 HOLZ 線。對於 EBSD，還應比較 master pattern 中的菊池帶寬度和背景。這將數值收斂與模擬結果中可見的物理特徵聯繫起來。

---

## 參見

- [附錄 A3. 布洛赫波法動力學繞射](index.md)
- [7.2. SAED 模擬](../../7-diffraction-simulator/1-saed-simulation.md)
- [7.4. CBED 模擬](../../7-diffraction-simulator/3-cbed-simulation.md)
- [7. 繞射模擬器](../../7-diffraction-simulator/index.md)
