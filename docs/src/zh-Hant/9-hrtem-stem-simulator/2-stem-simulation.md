# STEM 模擬

**STEM (Scanning Transmission Electron Microscopy) 模擬**使用布洛赫波法計算掃描穿透式電子顯微鏡影像。

![STEM 模式下的模擬器](../../assets/cap-zh-Hant-auto/FormImageSimulator-stem.png)

> 本頁列出當 **Image mode = STEM** 時於右側出現的所有設定。關於左側的結果顯示、亮度與正規化控制項，請參閱[總覽頁](index.md)。下方僅重複說明 STEM 專屬的**顯示目標**。

---

## 總覽

會聚電子束在試樣上掃描，於每個掃描位置由環形偵測器收集穿透與散射的電子。ReciPro 以布洛赫波法（動力學計算）計算 STEM 影像。

### 計算流程

1. 在每個掃描位置，以布洛赫波法針對會聚探針的每個入射方向計算繞射強度。
2. 將散射強度在偵測器的角度範圍上積分。
3. 可同時計算彈性與熱漫散射 (TDS) 的貢獻。

理論請參閱 [Appendix A3.4 — STEM calculation](../appendix/a3-bloch-wave/stem.md)。

---

## 偵測器類型

| 偵測器 | 角度範圍 | 主要貢獻 | 對比 |
|----------|-------------|-------------------|----------|
| **BF**（明場） | 0 – 會聚角 | 彈性 | 相位對比 |
| **ABF**（環形明場） | 會聚角的內側部分 | 彈性 | 對輕元素敏感 |
| **LAADF**（小角環形暗場） | 略在會聚角外側 | 彈性 + TDS | 對應變敏感 |
| **HAADF**（大角環形暗場） | 遠在會聚角外側 | TDS（非彈性） | Z 對比（$\propto Z^2$） |

> **典型偵測器設定**（每一項皆可從 STEM 選項的右鍵選單一鍵設定，全部使用會聚角 α = 25 mrad）：
> BF (0–5 mrad) / ABF (12–24 mrad) / LAADF (26–60 mrad) / HAADF (80–250 mrad)

---

## 試樣參數

![試樣參數](../../assets/cap-zh-Hant-auto/FormImageSimulator.splitContainer1.flowLayoutPanelModeSelection.groupBoxSampleProperty.png)

- **Thickness** : 試樣厚度 (nm)。在 **Serial image** 模式下此值會被忽略。

---

## TEM 條件

![TEM 條件](../../assets/cap-zh-Hant-auto/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxTEMConditions.png)

| 參數 | 說明 | 預設 / 典型 |
|-----------|-------------|-------------------|
| **Acc. Vol. (kV)** | 加速電壓。經相對論修正的電子波長會顯示於旁邊 | 200 kV |
| **Defocus Δf** | 物鏡（探針成形透鏡）的欠焦 (nm) | −57.8 nm |
| **Cs** | 球面像差係數 (mm)。影響探針尺寸 | 0.5–1.0 mm |
| **Cc** | 色像差係數 (mm) | 1.0–2.0 mm |
| **ΔV (FWHM)** | 電子能量分布的半高全寬 (eV) | 0.5–2.0 eV |

> **β（照明半角）在 STEM 模式下停用**，因為會聚角 α 取代了它的角色。

---

## STEM 選項（光學）

![STEM 選項（光學）](../../assets/cap-zh-Hant-auto/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxSTEMoption1.png)

設定會聚探針與環形偵測器的幾何。每個角度於右側也會換算為倒易空間半徑 $\sin\theta/\lambda$ (nm⁻¹) 顯示。

| 參數 | 說明 | 預設 / 典型 |
|-----------|-------------|-------------------|
| **α (convergence angle)** | 會聚探針的半角 (mrad)。較大的值會產生較細的探針並改變繞射對比 | 15–25 mrad |
| **(Annular) detector inner angle** | 環形偵測器的內側收集半角 (mrad)。此角度以內的訊號會被排除 | BF: 0, HAADF: 80 |
| **(Annular) detector outer angle** | 環形偵測器的外側收集半角 (mrad)。此角度以外的訊號會被排除 | BF: 5, HAADF: 250 |
| **Effective source size σs (FWHM)** | 有效電子源尺寸。較大的值會使探針模糊並降低細節對比 | — |

---

## STEM 選項（模擬）

![STEM 選項（模擬）](../../assets/cap-zh-Hant-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.groupBoxSTEMoption2.png)

- **Slice thickness for inelastic** : 計算 TDS（熱漫、非彈性）強度時所用的試樣切片厚度 (nm)。較小的值較準確但較慢。
- **Angular resolution** : 入射探針方向的角度取樣解析度 (mrad)。較小的值對探針取樣較細但較慢。 方向數按該比值的平方增長，因而是左右計算時間的最主要因素；收斂實測值參見[探針的角度取樣](../appendix/a3-bloch-wave/stem.md#angular-sampling)。

---

## 影像模式（single / serial）

![影像模式](../../assets/cap-zh-Hant-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.groupBoxSerialImage.png)

- **Single image** : 在目前厚度計算一張 STEM 影像。
- **Serial image** : 產生一系列影像，厚度 / 欠焦會分階段遞變（以 **Start / Step / Num** 設定；下方的清單也可直接編輯）。

---

## 影像內容

![影像屬性](../../assets/cap-zh-Hant-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.panelImageProperties.groupBoxImageProperty.png)

- **Size (W×H)** : 掃描影像的像素數（預設 512×512）。在 STEM 中此值等於掃描點數，並使計算時間線性增加。
- **Resolution** : 取樣解析度 (pm/px)。

---

## 繞射波

![繞射波](../../assets/cap-zh-Hant-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.panelImageProperties.groupBoxDiffractedWaves.png)

- **Max Bloch waves** : Bethe 法所用布洛赫波的最大數目（預設 80）。本徵值問題的計算成本隨波數的立方增加。

---

## STEM 顯示目標（結果側） {#stem-display-target}

![STEM 影像](../../assets/cap-zh-Hant-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxSTEMoption3.png)

視窗左下角的顯示切換可選擇顯示已計算 STEM 影像中的哪個散射分量（可在不重新計算的情況下切換）。

| 顯示目標 | 說明 |
|----------------|-------------|
| **Elastic** | 僅彈性散射的影像 |
| **TDS** | 僅熱漫散射的影像 |
| **Elastic & TDS** | 彈性 + TDS 的總和 |
| **EDX** | 特徵 X 射線分布圖。欲顯示的譜線（例如 `O-K`）由下方的下拉式選單選擇；*正規化*中的 **EDX 共用**會使所有通道採用同一顯示範圍，因此切換通道時影像不會重新縮放 |

!!! note
    三幅影像皆由傅立葉求和的實部重建，因此 **Elastic & TDS** 恰為另外兩幅之和。4.944 以前的版本改取絕對值，破壞了這一恆等關係，並使暗像素略微變亮。參見[重建為實數影像](../appendix/a3-bloch-wave/stem.md#real-image-reconstruction)。

---

## STEM-EDX 元素分布圖 {#stem-edx}

![STEM-EDX 元素分布圖](../../assets/cap-zh-Hant-auto/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxSTEMoption1.groupBoxSTEMoption4.png)

勾選**計算 EDX 分布圖**即可在計算 ADF 型影像的同時計算特徵 X 射線分布圖。這並不是一個獨立的模式：彈性、TDS 與 EDX 訊號皆出自同一次 STEM 計算，之後可在 [STEM 影像](#stem-display-target)中切換顯示，無需重新計算。

沒有元素選擇器：勾選之後，會計算**此晶體在此加速電壓下所有可計算的元素／殼層通道**，核取方塊下方的一行會列出這些通道（例如 `3 張圖: O-K, Mg-K, Al-K`）。當游離邊低於加速電壓、且該殼層在隨附資料的涵蓋範圍內時，該通道即可計算 — K 殼層為 C–Sn (Z = 6–50)、L-total 為 Ca–Rn (Z = 20–86)。隨附的資料表為每個通道儲存了散射向量直到 8 Å⁻¹ 的完全相對論性游離形狀因子，因此直到氡的重元素 L 譜線皆可在不外插的情況下模擬。若沒有任何可計算的通道，計算會被拒絕並顯示說明訊息，而不會產生一張空白的分布圖。

下一行顯示探針方向格點，例如 `格點: 132²（建議: ≥48²）`。此格點由**角解析度**與會聚角決定；參見[探針的角度取樣](../appendix/a3-bloch-wave/stem.md#angular-sampling)。低於建議的分割數時，±q 厄米殘差可能超出容許值而使計算中止，因此該數值會轉為橘色，並在開始計算前顯示確認對話方塊。

!!! warning "數值的意義"
    分布圖的數值是**每個入射電子所產生的內殼層空位數** — 這是模型量，而非預測的 X 射線計數。螢光產率、試樣內的自吸收、偵測器立體角與偵測器效率皆**未**納入。請將分布圖用於觀察空間分布、比較厚度或方位，而不要用於絕對定量。

### 偵測器參數（保留）

**自吸收**、**出射角**與**偵測器**雖已配置於面板上，但目前為停用狀態：它們屬於尚未實作的偵測器模型，先行顯示是為了在該模型完成時面板配置不致移動。它們最終的影響在性質上各不相同：

| 因素 | 單張分布圖內像素間的對比 | 元素分布圖之間的比值 |
|---|---|---|
| 自吸收（出射角） | **會改變** | **會改變** |
| 偵測器窗／死層／效率 | 無影響 | **會強烈改變** |
| 偵測器立體角、束電流、駐留時間 | 無影響 | 無影響 |

最後一列正是 ReciPro 完全不提供束電流與駐留時間設定的原因：它們只是把每張分布圖的每個像素乘上同一個數，在任何比值中都會相消，經顯示正規化之後便看不出任何差別。

### 精度與成本

STEM-EDX 對波數或切片厚度沒有額外限制：它與 ADF 型影像走相同的計算路徑，因此凡是適用於 STEM 的設定同樣適用於 EDX。

精度的拿捏交由使用者掌握，正如波數與角解析度的設定一樣。作為參考，深度積分誤差大致與**切片厚度 (TDS)** 成正比 — 1 nm 時約 2–3 %、2 nm 時約 4–8 %、4 nm 時約 12–23 %（相對於峰值；SrTiO₃、厚度 39 nm）。切片厚度減半，誤差大約減半，深度積分的計算量則大約加倍。

---

## 計算成本

STEM 模擬的計算成本很高，因此請適當設定下列參數。

| 因素 | 影響 |
|--------|--------|
| **會聚角** | 較大 → CBED 盤重疊較多 → 成本較高 |
| **布洛赫波** | 本徵值問題的成本隨 N³ 增加 |
| **角度解析度** | 較細 → 較準確，但成本隨 N² 增加 |
| **影像像素 (Size)** | 隨掃描點數線性增加 |

---

## 溫度因子的重要性

對於 HAADF-STEM 模擬，原子必須具有非零的等向性溫度因子（德拜-沃勒因子）。若該值未知，請設定 $B \approx 0.5\ \text{Å}^2$。若溫度因子為零，TDS 強度即為零，HAADF 影像便無法正確計算。

| 偵測器 | 範圍 | 主要貢獻 |
|----------|-------|-------------------|
| BF, ABF | 會聚角以內 | 彈性 |
| LAADF, HAADF | 會聚角以外 | 非彈性 (TDS) |

---

## 與 Dr. Probe 的比較

已確認 ReciPro 的 STEM 模擬與廣為使用的 Dr. Probe GUI (v1.10) 高度吻合。下圖針對 BF、ABF、LAADF 與 HAADF 偵測器，在一系列厚度（2.96–60.05 nm）下比較兩者，包含無像差（左）以及 Cs = 0.2 mm、欠焦 = −25.9 nm（右）兩種情況。兩套程式在所有偵測器類型與厚度上皆一致。

![STEM 模擬比較：Dr. Probe vs ReciPro](../../assets/references/STEM_DrProbe_comparison.png)

更詳細的報告以 PDF 形式提供：[Comparison of STEM simulations by Dr. Probe GUI (v1.10) and ReciPro (v4.854)](https://github.com/seto77/ReciPro/files/10976084/ComparisonSTEMsimulations.pdf)。

---

## 另請參閱

- [HRTEM/STEM 模擬器（總覽）](index.md)
- [HRTEM 模擬](1-hrtem-simulation.md)
- [位能模擬](3-potential-simulation.md)
- [Appendix A3.4 — STEM calculation](../appendix/a3-bloch-wave/stem.md)
