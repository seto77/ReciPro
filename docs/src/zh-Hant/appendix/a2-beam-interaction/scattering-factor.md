# 原子散射因子

**原子散射因子**（或稱*形狀因子*）量度單一原子對入射束的散射強度如何隨散射變數 $s=\sin\theta/\lambda$ 變化。這三種輻射與原子中完全不同的部分發生交互作用，因此它們的散射因子在量級、單位與角度相依性上各不相同。這正是 **散射因子** 索引標籤在 X 光、電子與中子束之間看起來如此不同的最主要原因。

=== "X-ray"
    ![散射因子 — X 光](../../../assets/cap-zh-Hant-auto/FormBeamInteraction-xray-scattering.png)

=== "Electron"
    ![散射因子 — 電子](../../../assets/cap-zh-Hant-auto/FormBeamInteraction-electron-scattering.png)

=== "Neutron"
    ![散射因子 — 中子](../../../assets/cap-zh-Hant-auto/FormBeamInteraction-neutron-scattering.png)

---

## X 光 — 由電子雲散射

X 光由原子的**電子**散射。單一自由電子以古典的 **Thomson** 微分截面散射，其大小由古典電子半徑 $r_e = e^2/(4\pi\varepsilon_0 m_e c^2) \approx 2.82\times10^{-5}\ \text{Å}$ 決定：

$$\left(\frac{d\sigma}{d\Omega}\right)_e = r_e^2\,\frac{1+\cos^2 2\theta}{2}.$$

原子的電子以數密度 $\rho_e(\mathbf r)$ 分布於空間中，而原子散射因子即為該密度的**傅立葉變換**。原子截面則是單電子截面乘以 $|f_0|^2$：

$$f_0(\mathbf Q) = \int \rho_e(\mathbf r)\, e^{\,i\mathbf Q\cdot\mathbf r}\, d^3r ,
\qquad
\left(\frac{d\sigma}{d\Omega}\right)_\text{atom} = r_e^2\,\frac{1+\cos^2 2\theta}{2}\,|f_0(s)|^2 .$$

- 在前向方向（$s\to 0$）每個電子皆同相散射，因此 $f_0(0) = Z$，即原子序。此因子以**電子單位**表示（為 Thomson 振幅的倍數 — 上方第二式即明確表達此點）。
- 隨著 $s$ 增大，電子雲不同部位的散射逐漸失相，$f_0(s)$ 隨之衰減。彌散的（外層、價）電子分布使 $f_0$ 快速下降；緊束縛的內層電子則持續貢獻至高 $s$ 處。

實務上 $f_0(s)$ 以一組高斯函數之和列表（ReciPro 採用的解析式 **Waasmaier–Kirfel** 形式，為較早的 Cromer–Mann 表的擴展），

$$f_0(s) = \sum_{i} a_i\, e^{-b_i s^2} + c ,$$

ReciPro 即以此式計算曲線。係數是針對以 Å⁻¹ 為單位的 $s$ 製表，因此每個 $b_i$ 的單位為 Å²；ReciPro 在內部以 nm⁻² 處理 $s^2$，並套用[索引](index.md)中所述的因子 100 換算。

### 異常（共振）色散

傅立葉變換的圖像假設電子如同自由電子般散射。當光子能量接近**吸收邊**時，束縛電子會共振響應，於是出現兩個與能量相依的修正項：

$$f(s,E) = f_0(s) + f'(E) + i\,f''(E) \qquad \text{(textbook, } e^{+i\phi}\ \text{convention).}$$

- $f'(E)$ ：實部色散修正（在邊緣附近降低有效電子數）。
- $f''(E)$ ：虛部，在邊緣正上方最大。
- 兩者透過 **Kramers–Kronig** 關係相連繫，因此吸收（$f''$）中的峰值會伴隨 $f'$ 中的色散擺動。

這些並非自由參數。因果律（Kramers–Kronig）將 $f'$ 與 $f''$ 繫結，而**光學定理**則將 $f''$ 直接與光吸收截面繫結：

$$f'(E) = \frac{2}{\pi}\,\mathcal{P}\!\!\int_0^\infty \frac{E'\,f''(E')}{E'^2 - E^2}\,dE',
\qquad
f''(E) = \frac{\sigma_\text{abs}(E)}{2\,r_e\,\lambda}.$$

此處 $\sigma_\text{abs}$ 基本上是衰減中的**光吸收**部分（並非 Rayleigh／Compton 項）— 與[衰減與傳輸](attenuation-transport.md)頁面所見的邊緣結構相同。

ReciPro 以隨附的 **xraylib** 函式庫在目前能量下計算 $f'$ 與 $f''$，並將其列於表中（以 $f'' > 0$）。有兩個正負號要點。第一，xraylib 回傳的 $F_{ii}$ 與結晶學慣例的正負號相反，因此 ReciPro 將其取負以回報**正的 $f''$**。第二，在 ReciPro 的 $\exp(-2\pi i\,\mathbf g\cdot\mathbf r)$ 相位慣例下，實際進入結構因子的複數因子為 $f_0 + f' - i f''$ — 上方所寫的 $+i f''$ 屬於相反的（$e^{+2\pi i}$）慣例。這正是 `F_inv`（結構因子的虛部）在邊緣附近變為非零的原因 — 參見[結構因子](structure-factor.md)。

---

## 電子 — 由靜電位能散射

快速電子帶電，因此由原子的**靜電位能** $V(\mathbf r)$ 散射 — 即正電原子核與負電電子雲的組合。電子散射因子 $f_e$ 因此是該位能的傅立葉變換，並透過 Poisson 方程式與 X 光因子相連繫。其結果即為 **Mott–Bethe 關係**：

$$f_e(s) = C_\text{MB}\,\frac{Z - f_0(s)}{s^2} \;\;\propto\; \frac{Z - f_X(Q)}{Q^2}.$$

前置因子 $C_\text{MB}$ 由基本常數構成，並取決於單位系統以及採用 $s$ 還是 $Q$。ReciPro 不直接計算此關係 — 它使用下方擬合的 Peng／Kirkland／8-Gaussian 形式 — 因此此處給出此式僅為提供物理見解，而非用於計算。將常數寫出後（$s$ 與 $f_e$ 以 Å 為單位），

$$f_e(s)\,[\text{Å}] = \frac{m_e e^2}{8\pi\varepsilon_0 h^2}\,\frac{Z - f_0(s)}{s^2} \simeq 0.023934\,\frac{Z - f_0(s)}{s^2}, \qquad s\ \text{in Å}^{-1},$$

當 ReciPro 以 nm 回報 $f_e$ 時還須再乘以 $\times 0.1$，並在動力學位能中附加一個相對論性 $\gamma$ 因子（見下文）。

物理意義在於分子 $Z - f_0$：電子所見的是核電荷 $Z$ 與屏蔽電子雲 $f_0$ 之間的**差**，即淨原子位能。

- **量級。** 由於 $1/s^2$ 因子，$f_e$ 在小角度處呈尖銳的峰值，並且（以其自身單位計）遠大於 $f_0$，且更偏向前向。這正是電子繞射由低階反射主導，以及動力學（多重）散射為何重要的原因 — 參見[附錄 A3](../a3-bloch-wave/index.md)。
- **小角度極限。** 對於*中性*原子，$Z-f_0\to 0$ 與 $s^2\to 0$ 兩者皆成立，因此 $f_e(0)$ 為有限值（一個 $0/0$ 極限，由均方原子半徑決定）。對於**離子**，電子雲不再抵消 $Z$，長程庫侖尾使 $f_e$ 在 $s\to 0$ 時發散；列表的離子電子因子在最小角度處必須謹慎處理。
- **相對論性修正。** 在 TEM 能量下，電子質量與波長皆具相對論性。波長採用相對論形式 $\lambda = h/\sqrt{2 m_0 e U\,(1 + e U/2 m_0 c^2)}$，而交互作用位能帶有相對論性因子 $\gamma = 1 + eU/m_0c^2$。ReciPro 在構成動力學位能時套用此修正。

ReciPro 提供 $f_e(s)$ 的三種參數化：

- **Peng** ：五高斯擬合，$f_e(s)=\sum_i a_i e^{-b_i s^2}$，方便且廣泛用於彈性電子散射。
- **Kirkland** ：混合 Lorentzian + 高斯擬合，$f_e(q)=\sum_i \dfrac{a_i}{q^2+b_i} + \sum_i c_i\,e^{-d_i q^2}$。**其自變數為 $q = 2s = 1/d$，而非 $s$** — 這是比較模型時常見的因子二錯誤來源（$q$ 以 Å⁻¹ 計，擬合係數 $a_i,b_i,c_i,d_i$ 以對應單位計）。
- **8-Gaussians** ：八項擬合，於較寬的 $s$ 範圍內有效。

**選擇其一。** 三者皆擬合同一個底層 $f_e(s)$，並在低 $s$ 處密切吻合；它們主要的差異在於適用範圍以及原子內層電子如何表示。**Peng**（中性原子與常見離子，精確至 $s\approx2\text{–}6$ Å⁻¹）是 SAED／CBED 結構因子的常用預設；**Kirkland** 以一個 Lorentzian 核項延伸至更高的 $s$，適用於 HRTEM/STEM（記得 $q=2s$）；**8-Gaussians** 則用於達到極高 $s$ 的反射。對於輕元素，三者幾乎無法區分；差異會在重元素的大角度處顯現。

---

## 中子 — 由原子核散射

熱中子不帶電，主要透過**強核力**與物質發生交互作用，其作用範圍（飛米）相較於中子波長（埃）完全可忽略。此交互作用以 **Fermi 贗位能**表示，即一個點源，其強度為散射長度 $b$：

$$V(\mathbf r) = \frac{2\pi\hbar^2}{m_n}\,b\,\delta(\mathbf r)
\qquad\Longrightarrow\qquad
\frac{d\sigma}{d\Omega} = |b|^2 .$$

由於散射體為點狀，$b$ **與 $s$ 無關** — 並無形狀因子衰減，這正是 **散射因子** 索引標籤對中子不繪製曲線，而改為顯示散射長度表的原因。

- $b$ 是**核種**的性質，而非電子組態的性質。它在元素之間（以及同位素之間）不規則地變化，可以為**負值**（例如 ¹H、Ti、Mn），且與 $Z$ 並無單調關係。這正是中子襯度的基礎（重原子旁的輕原子、同位素標記）。
- **同調與非同調。** 真實元素是具有不同 $b$ 的同位素與核自旋態的混合。將 $b = \langle b\rangle + \delta b$ 拆分後，可得一個同調部分（來自平均值）與一個非同調部分（來自分散）：

$$\sigma_\text{coh} = 4\pi\,|\langle b\rangle|^2, \qquad \sigma_\text{inc} = 4\pi\big(\langle |b|^2\rangle - |\langle b\rangle|^2\big), \qquad \sigma_s = \sigma_\text{coh} + \sigma_\text{inc}.$$

  同調部分產生 Bragg 繞射（它是進入結構因子的部分）；非同調部分則為平坦、各向同性的背景（對 ¹H 而言很大，這正是進行氘化的原因）。

!!! note "列表數值"
    ReciPro 從核種表讀取 $b_\text{coh}$ 與各截面，而非加以計算。對於共振核種，所列的 $\sigma_\text{coh}$ 不必等於簡單的 $4\pi b^2$，因此表中數值才是權威。磁性中子散射（來自未配對電子自旋，它*確實*具有與 $s$ 相依的形狀因子）在此並未建模。

---

## 一覽

| | X-ray | Electron | Neutron |
|---|---|---|---|
| 散射來源 | 電子雲 $\rho_e(\mathbf r)$ | 靜電位能 $V(\mathbf r)$ | 原子核（點） |
| $s$ 相依性 | 衰減（電子雲的 FT） | $\propto (Z-f_0)/s^2$，強烈前向 | 無（$b$ 為常數） |
| 前向值 | $f_0(0)=Z$ | 有限（中性）／發散（離子） | $b$ |
| 能量相依性 | 邊緣附近的 $f',f''$ | 相對論性 $\lambda,\gamma$ | $\sigma_\text{abs}\propto 1/v$（非 $b$） |
| 典型量級 | $\propto Z$ | 前向尖峰，隨 $Z$ 增大 | 不規則，可為 $<0$ |

---

## 另見

- [索引 — 幾何與變數 $s$](index.md)
- [結構因子](structure-factor.md) — 這些因子如何在晶胞上組合。
- [3. 電子束交互作用 → 散射因子 索引標籤](../../3-beam-interaction.md#scattering-factors-tab)
