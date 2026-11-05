# HRTEM 模擬

**HRTEM（高解析穿透式電子顯微鏡）** 模擬用於計算高解析 TEM 晶格條紋影像。這是 [HRTEM/STEM 模擬器](index.md) 的主要模式。

![HRTEM 模式下的模擬器](../../assets/cap-zh-Hant-auto/FormImageSimulator-hrtem.png)

> 本頁涵蓋 **影像模式 = HRTEM** 時右側出現的所有設定。左側的控制項——結果的顯示與亮度調整——請參閱[總覽頁](index.md#display-settings)。

---

## 概觀

穿透試樣的電子波在物鏡像差的影響下成像，即形成 HRTEM 影像。ReciPro 以布洛赫波法（動力學計算）計算電子波在試樣內部的傳播，並透過相位對比轉移函式 (PCTF) 產生 HRTEM 影像。

### 計算流程

1. **布洛赫波法**：計算電子波在晶體位能中的傳播，求得出射波的振幅與相位
2. **透鏡函式**：套用物鏡像差（球面像差 $C_s$、散焦 $\Delta f$）
3. **部分同調**：考量有限的光源尺寸（空間同調）與能量起伏（時間同調）
4. **成像**：計算強度分布 $|\psi(\mathbf{r})|^2$

理論請參閱[附錄 A3.2 — HRTEM 成像](../appendix/a3-bloch-wave/hrtem.md)。

---

## 試樣

![試樣](../../assets/cap-zh-Hant-auto/FormImageSimulator.splitContainer1.flowLayoutPanelModeSelection.groupBoxSampleProperty.png)

- **厚度** ：試樣厚度 (nm)。HRTEM 影像強烈依賴於厚度。在 **序列影像** 模式下此值會被忽略，改用下述的厚度清單。

---

## TEM 條件 {#tem-conditions}

![TEM 條件](../../assets/cap-zh-Hant-auto/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxTEMConditions.png)

設定物鏡的成像條件。

| 參數 | 說明 | 預設 / 典型值 |
|-----------|-------------|-------------------|
| **加速電壓** <!-- 261105Cl 監査: 見出しから (kV) を外した (numericBoxAccVol.HeaderText)。単位は欄の右に出る --> | 加速電壓 (kV)。經相對論修正的電子波長顯示於右側 | 200 kV |
| **散焦 Δf** | 物鏡的散焦 (nm)。下方顯示參考用的 **Scherzer 散焦** 值 | −57.8 nm |
| **Cs** | 球面像差係數 (mm)。影響 CTF 與 Scherzer 散焦。也可輸入負值（−20 至 20 mm） | 1.0 mm（預設）；0.5–1.0（傳統）、< 0.01（Cs 校正） |
| **Cc** | 色像差係數 (mm)。決定能量擴展所造成的影像模糊 | 1.4 mm（預設）；1.0–2.0 mm |
| **β** | 照明半角 (mrad)。代表有限光源尺寸效應（空間同調） | 0 mrad（預設）；0.1–1.0 mrad |
| **ΔV** | 電子能量擴展的半高全寬 (eV)。與 Cc 共同決定色像差造成的焦點擴展 | 0.8 eV（預設）；0.5–2.0 eV |

上述預設值僅適用於首次啟動；之後會還原上次使用的值。內部會將 ΔV（半高全寬）換算為標準差 $\sigma_V = \Delta V / (2\sqrt{2\ln 2})$，焦點擴展為 $\delta = C_c\,\sigma_V / V$。像差函式為

$$\chi(u) = \pi\lambda\,\Delta f\,u^2 + \tfrac{\pi}{2}\,C_s\lambda^3 u^4 ,$$

每個繞射波都乘上 $\exp[-i\chi(u)]$，因此**負的 Δf 為欠焦**（在此慣例下 Scherzer 散焦為負值）。

> **右鍵選單**：在 TEM 條件面板上，可一鍵套用 **將散焦設為 0 nm** / **將散焦設為 Scherzer 值** / **理想條件 (Cs, Cc, β, ΔV, Δf = 0)**（最後一項會將 Cs、Cc、β、ΔV **以及散焦**都設為 0）。 <!-- 261106Cl 作者の決定 E19: 表示名を理想条件に -->同一選單中還有四個顯微鏡預設組——*300kV Cold-FEG Cs-free (Cs 0, Cc 2.8, ΔV 0.3) ARM300F*、*200kV Schottky-FEG UHR (Cs 0.5, Cc 1.1, ΔV 0.8) 2100F*、*200kV Schottky-FEG HR (Cs 1.0, Cc 1.4, ΔV 0.8) 2100F* 與 *200kV Lab6 HR (Cs 1.0 , Cc 1.4, ΔV 2.0) 2100*——會依項目所寫的值設定加速電壓、Cs、Cc 與 ΔV，然後將散焦設為 Scherzer 值（β 保持不變）。右下方的 **預設設定** 核取方塊會開啟儲存您自訂條件的視窗（請參閱[預設清單](index.md#preset-list)）。 <!-- 261105Cl 監査: プリセット名は ΔV (FormImageSimulator.resx presets1〜4ToolStripMenuItem.Text) -->

### Scherzer 散焦

在此散焦值附近相位對比最佳；由目前的波長與球面像差 $C_s$ 計算而得（僅供參考）。當 $C_s$ 為負值時，顯示的值為 $+\sqrt{\tfrac{4}{3}|C_s|\lambda}$。

$$\Delta f_{\text{Scherzer}} = -\sqrt{\tfrac{4}{3}\,C_s \lambda}\quad\left(\approx -1.155\,\sqrt{C_s \lambda}\right)$$

在此條件下，PCTF 在寬廣的空間頻率範圍內為負值，因此原子位置呈現暗對比。ReciPro 採用此原始 Scherzer 值（透過將像差相位 $\chi$ 的最小值設為 $-2\pi/3$ 推導而得），GUI 中顯示的值即遵循此公式。請注意，部分文獻改用 *延伸 Scherzer* 值 $-1.2\sqrt{C_s\lambda}$，可使傳遞頻帶更寬。

---

## 透鏡函式 / 對比傳遞函數 (CTF)

勾選 **對比傳遞函數 (CTF)** 會開啟一個視窗，繪出透鏡像差與散焦在各空間頻率下如何傳遞影像對比。

![對比傳遞函數 (CTF)](../../assets/cap-zh-Hant-auto/FormCTF.png)

- $\sin\chi(u)$ ：相位對比轉移函式（$\chi(u)$ 為透鏡的像差函式）
- $E_\text{s}(u)$ ：空間同調包絡函式；有限光源尺寸 ($\beta$) 所造成的衰減
- $E_\text{c}(u)$ ：時間同調包絡函式；能量起伏 ($C_c$, $\Delta V$) 所造成的衰減

每條曲線各有其核取方塊：$\sin\chi$、$\cos\chi$、$E_\text{s}$、$E_\text{c}$、**PCTF** $=\sin\chi\,E_\text{s}E_\text{c}$ 與 **ACTF** $=\cos\chi\,E_\text{s}E_\text{c}$；PCTF 與 ACTF 在物鏡光圈之外設為零。在 HRTEM 模式下

$$E_\text{s}(u)=\exp\!\left[-\pi^2\beta^2u^2\left(\Delta f+C_s\lambda^2u^2\right)^2\right],\qquad E_\text{c}(u)=\exp\!\left[-\tfrac{1}{2}\pi^2\lambda^2\delta^2u^4\right].$$

**最大 u**（nm⁻¹，預設 6）設定橫軸的上限。**複製** 會將所繪曲線以 Tab 分隔的文字放到剪貼簿。此視窗在 STEM 模式下也可使用（請參閱 [STEM 模擬](2-stem-simulation.md#ctf-stem)）；在 Potential 模式下則停用。

---

## 物鏡光圈 (HRTEM 選項)

![物鏡光圈 (HRTEM 選項)](../../assets/cap-zh-Hant-auto/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxHREMoption1.png)

限制通過物鏡光圈的繞射波。布洛赫波計算本身一律使用 **繞射波** 中設定的波數；光圈再從這些繞射束中選出對影像有貢獻者（當 $|\mathbf{g}-\mathbf{c}| < 2\sin(\alpha_\text{obj}/2)/\lambda$ 時繞射束通過，$\mathbf{c}$ 為光圈中心）。若通過的繞射束少於兩條，計算會停止，並顯示請您放大光圈的訊息。

- **大小** ：物鏡光圈的半角 (mrad，預設 12)。值越小，被截去的高角度繞射波越多，高解析細節也越平滑。同時顯示對應的倒易空間半徑 $\sin\theta/\lambda$ (nm⁻¹)。
- **位移 X** / **Y** ：物鏡光圈中心的位移 (mrad)。用於移位光圈的暗場成像；照明本身並不傾斜。
- **光圈全開** ：將物鏡光圈全開（無限大），使所有繞射波皆參與成像。
- **內部光點** ：落在光圈內的繞射束（光點）數量（唯讀）。
- **光點資訊** ：開啟一個表格，列出光圈內的繞射束（強度、複數振幅等）。

> 物鏡光圈的大小也會顯示於 **繞射模擬器** 中。

---

## HRTEM 選項（部分同調模型）

![HRTEM 選項（部分同調模型）](../../assets/cap-zh-Hant-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.groupBoxHREMoption2.png)

選擇如何納入部分同調（β 與焦點擴展 δ）。出射波只針對單一入射方向（光軸）計算；照明的發散與能量擴展只透過下列的包絡函式納入。

- **線性影像**（準同調模型）：計算成本低。動力學出射波的每個繞射波 $\psi_g$ 都乘上 $\exp[-i\chi(g)]\,E_\text{s}(g)\,E_\text{c}(g)$，各波以同調方式相加：$I(\mathbf{r}) = \left|\sum_g \psi_g\,e^{-i\chi(g)}\,E_\text{s}(g)\,E_\text{c}(g)\,e^{2\pi i\,\mathbf{g}\cdot\mathbf{r}}\right|^2$。當影像主要由與穿透束的干涉構成時（薄試樣）是準確的。
- **穿透交叉係數** ：計算成本高但較準確。每一對繞射束 $(g,h)$ 都以相互包絡（完整的穿透交叉係數）加總，適用於會激發許多強繞射波的強散射體。計算成本隨光圈內繞射束數的平方增加。

詳情請參閱[附錄 A3.2 — HRTEM 成像](../appendix/a3-bloch-wave/hrtem.md)。

---

## 單張/序列模式

![單張/序列模式](../../assets/cap-zh-Hant-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.groupBoxSerialImage.png)

- **單張影像** ：在目前的厚度與散焦下計算一張 HRTEM 影像。
- **序列影像** ：逐步改變厚度與散焦，產生一組影像（厚度序列／焦點序列）。可用於尋找與實驗影像最吻合的條件。

序列影像需設定下列項目。

| 項目 | 說明 |
|------|-------------|
| **厚度 (nm)** / **散焦 (nm)** | 要掃描的量（兩者可同時勾選） |
| **Start / 步進 / 數量** | 起始值、步進寬度與影像張數。這些值會展開至下方的清單框，清單框也可直接編輯 |
| **水平方向：** | 同時掃描厚度與散焦時，沿格狀排列水平方向配置的量（**散焦** 或 **厚度**） |

同時掃描厚度與散焦時，會產生列 × 行的影像矩陣。每個量的 **數量** 上限為 20。只掃描一個量且影像多於兩張時，該列會折返排成近似正方形的格狀。

---

## 影像屬性

![影像屬性](../../assets/cap-zh-Hant-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.panelImageProperties.groupBoxImageProperty.png)

- **尺寸 (寬×高)** ：模擬影像的像素數（預設為 512×512）。
- **解析度** ：取樣解析度 (pm/px，預設 2)。視野為 尺寸 × 解析度，因此值越小，會在越小的範圍內越細地取樣條紋。影像是在每個像素上對各繞射束直接進行傅立葉求和而合成的（不使用 FFT），因此計算時間取決於像素數與繞射束數，而與此值無關。

---

## 繞射波

![繞射波](../../assets/cap-zh-Hant-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.panelImageProperties.groupBoxDiffractedWaves.png)

- Bethe 法（動力學計算）所用布洛赫波的最大數量，預設為 80。數量越多準確度越高，但本徵值問題的求解時間為 $O(N^3)$。

---

## 另請參閱

- [HRTEM/STEM 模擬器（概觀）](index.md)
- [STEM 模擬](2-stem-simulation.md)
- [位能模擬](3-potential-simulation.md)
- [附錄 A3.2 — HRTEM 成像](../appendix/a3-bloch-wave/hrtem.md)
