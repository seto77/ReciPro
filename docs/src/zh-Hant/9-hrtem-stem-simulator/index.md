---
title: HRTEM / STEM Simulator
---

# HRTEM / STEM 模擬器

**HRTEM/STEM 模擬器**針對所選晶體與方位，模擬 TEM 晶格條紋影像 (HRTEM)、STEM 影像以及投影晶體位能。點按 **模擬** 即可執行計算。

![HRTEM/STEM 模擬器](../../assets/cap-zh-Hant-auto/FormImageSimulator.png)

視窗分為左右兩半。**左側**顯示模擬結果並控制其外觀（影像窗格、亮度、顏色、比例尺等）；**右側**則是計算條件（**光學屬性** 與 **模擬設定**）。

---

## 本頁與各模式頁面

- **本頁（總覽）**：所有模式共通的操作，以及**左側的結果顯示與調整控制項**。
- **各模式頁面**：該模式下**右側**出現的所有設定。每一頁皆可獨立閱讀（因此部分設定會在多個頁面中重複出現）。

| 模式 | 內容 | 頁面 |
|------|----------|------|
| **HRTEM** | 高解析 TEM 晶格條紋影像 | [HRTEM 模擬](1-hrtem-simulation.md) |
| **STEM** | 掃描穿透式電子顯微鏡影像 (BF / ABF / LAADF / HAADF) | [STEM 模擬](2-stem-simulation.md) |
| **Potential** | 投影晶體位能 ($U_g$ / $U'_g$) | [位能模擬](3-potential-simulation.md) |

---

## 鍵盤與滑鼠快速鍵

結果會以一個或多個影像窗格顯示。它們採用 ReciPro 的標準[影像檢視導覽](../21-shortcuts.md)，且所有窗格會一起平移與縮放。

| 快速鍵 | 動作 |
|----------|--------|
| <kbd>F1</kbd> | 開啟線上手冊的本頁 |
| <kbd>CTRL</kbd>+<kbd>C</kbd> (聚焦於影像網格時) | 將影像以中繼檔 (metafile) 複製到剪貼簿 |
| 左鍵拖曳 / 中鍵拖曳 | 平移影像 (所有窗格一起移動) |
| 滑鼠滾輪向上 / 向下 | 在游標處放大 (×2) / 縮小 (×0.5) |
| 以右鍵拖曳出方框 | 放大至所選區域 |
| 右鍵點按 / 右鍵雙擊 | 縮小 (×0.5) |
| <kbd>CTRL</kbd> + 以右鍵拖曳出方框 | 選取矩形區域 |
| 左鍵雙擊某窗格 | 將該窗格最大化 / 還原格線 (多窗格版面) |
| 移動滑鼠 (不按鍵) | 讀取游標處的位置 (pm) 與像素值 |

→ 請參閱 **[21. 鍵盤與滑鼠快速鍵](../21-shortcuts.md)**，一覽每個視窗的快速鍵。

---

## 依目標的快速路徑

| 目標 | 從何開始 | 參考 |
|------|------------|-----------|
| 計算單張 HRTEM 影像 | 將 **影像模式** 設為 **HRTEM**，然後在 **TEM 條件** 中設定加速電壓與散焦 | [HRTEM 模擬](1-hrtem-simulation.md)、[HRTEM 成像](../appendix/a3-bloch-wave/hrtem.md) |
| 計算 STEM 影像 | 將 **影像模式** 設為 **STEM**，然後在 **STEM 選項** 中設定會聚角與偵測器 | [STEM 模擬](2-stem-simulation.md)、[STEM 計算](../appendix/a3-bloch-wave/stem.md) |
| 檢視投影位能 | 將 **影像模式** 設為 **Potential** | [位能模擬](3-potential-simulation.md) |
| 產生厚度 / 散焦序列 | 在 HRTEM 模式中設定 **單張/序列模式** 與影像條件 | [HRTEM 模擬](1-hrtem-simulation.md) |
| 搭配 TDS 使用 HAADF-STEM | 將原子溫度因子設為非零值，並將 STEM 偵測器移至 LAADF / HAADF | [STEM 計算](../appendix/a3-bloch-wave/stem.md) |

---

## 基本工作流程

1. 在主視窗中選取晶體與方位，然後開啟此視窗。
2. 在 **影像模式** 中選擇 HRTEM、STEM 或 Potential。
3. 在 **光學屬性** 中設定加速電壓、散焦、像差、光圈、STEM 會聚角等（請參閱各模式頁面）。
4. 在 **模擬設定** 中設定厚度、影像尺寸、解析度、布洛赫波數量、部分同調模型等（請參閱各模式頁面）。
5. 點按 **模擬**，然後視需要以左側的 **調整**、**正規化** 與 **顯示** 調整外觀。

---

## 選擇影像模式

![影像模式](../../assets/cap-zh-Hant-auto/FormImageSimulator.splitContainer1.flowLayoutPanelModeSelection.groupBoxImageMode.png){ align=left }

右上方的 **影像模式** 用於選擇計算種類。右側的面板（**光學屬性** 與 **模擬設定**）會隨所選模式而改變。<div style="clear: both;"></div>

- **HRTEM** — 高解析 TEM 晶格條紋影像 → [HRTEM 模擬](1-hrtem-simulation.md)
- **STEM** — 掃描穿透式電子顯微鏡影像 → [STEM 模擬](2-stem-simulation.md)
- **Potential** — 投影晶體位能 → [位能模擬](3-potential-simulation.md)

---

## 影像區（左側）

視窗的左半部顯示模擬影像。頂端的狀態列會回報游標位置 (**X:**、**Y:**) 與游標下方的影像 **數值：** (強度)，旁邊還有一個反映目前色階與亮度範圍的 **低 → 高** 強度刻度。

產生多張影像時（序列影像，或位能的振幅／相位），影像會以格狀排列，且所有窗格會一起縮放與平移。

---

## 結果的顯示與調整（左側面板） {#display-settings}

左下方的面板用於調整結果的外觀——亮度、顏色、正規化與疊加顯示。這些設定適用於所有模式，且無須重新計算即可生效。

### 調整

![調整](../../assets/cap-zh-Hant-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxAdjust.png)

- **Min** / **Max** ：顯示強度範圍的下限（黑）與上限（白）。以滑桿調整對比。
- **顏色** ：影像的色階——**Gray scale** 或 **Cold-Warm**（藍至紅）。
- **高斯模糊 (FWHM)** ：勾選時，以右側指定的半高全寬 (pm) 套用高斯模糊，近似有限的解析度（點擴散函數）。

### 正規化

![正規化](../../assets/cap-zh-Hant-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxNormalization.png)

- **每張影像** ：勾選時，各影像分別正規化（未勾選時，整個序列共用同一刻度）。
- **Min** / **Max** ：將正規化的下限／上限固定為右側指定的值，而非影像的最小值／最大值。
- **EDX 共用** ：僅在顯示 [EDX 分布圖](2-stem-simulation.md#stem-edx) 且該次計算產生多於一個通道時顯示；將同一量的所有通道置於同一顯示範圍。EDX 分布圖絕不會被 **Min** / **Max** 重新縮放：其下限固定為 0，只有上限依 **每張影像** / **EDX 共用** 而定。
- 此群組在 Potential 模式下隱藏。

### STEM 影像

![STEM 影像](../../assets/cap-zh-Hant-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxSTEMoption3.png)

僅在 STEM 模式下顯示。選擇所計算 STEM 影像中要顯示的散射成分（**彈性**、**TDS** 或 **彈性 & TDS**）。若同一次計算中也計算了 [STEM-EDX 分布圖](2-stem-simulation.md#stem-edx)，則可選擇第四項 **EDX**。由於此項為 STEM 專屬，[STEM 模擬](2-stem-simulation.md)頁面也有說明。

### 顯示

![顯示](../../assets/cap-zh-Hant-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxDisplay.png)

設定疊加於影像上的項目。

- **晶胞** ：疊加投影單位晶胞的輪廓，方便將影像對比與晶格相對照。平行於 **a**、**b**、**c** 的邊分別以紅、綠、藍色繪製，並以影像中心為中心。
- **標籤** ：疊加各窗格的標籤——HRTEM / STEM 中為厚度 `t=` 與散焦 `f=`（nm），Potential 模式中則為窗格名稱（例如 *Ug magnitude*）。可指定 **大小**（字型大小）與 **顏色**。
- **比例尺** ：疊加比例尺。可指定 **長度** (nm) 與 **顏色**。

---

## 執行模擬

![Simulation actions](../../assets/cap-zh-Hant-auto/FormImageSimulator.splitContainer1.panelSimulationActions.png)

- **模擬** ：以目前的晶體、顯微鏡條件、厚度、散焦與顯示設定執行計算。
- **停止** ：中止執行中的計算（僅在計算期間顯示）。
- **即時模擬** ：勾選時，旋轉晶體會立即重新計算（STEM 模式下隱藏）。
- **預設設定** ：切換預設視窗的顯示，該視窗可儲存與叫用 TEM 成像條件（請參閱下方的[預設清單](#preset-list)）。

### 預設清單 {#preset-list}

<!-- screenshot: Preset list window (FormPresets) with a few entries -->

**Preset list** 視窗會保存模擬器設定的具名快照。快照會儲存影像模式、加速電壓、Cs、Cc、ΔV、散焦、β、物鏡光圈（大小與位移）、HRTEM 部分同調模型、厚度與序列的厚度／散焦清單、影像尺寸與解析度、布洛赫波數，以及 STEM 設定（會聚角、偵測器角度、光源尺寸、切片厚度、角解析度、**計算 EDX 分布圖** 與 **譜線系 (Kα, Lα…)**）。此清單會與 ReciPro 的設定一起儲存，重新啟動後仍會保留。

- 按一下某個項目會立即套用。
- **OK** 會關閉視窗並保留目前的設定；**取消**（或 <kbd>Esc</kbd>）會關閉視窗，並還原開啟視窗時的設定。
- **管理預設清單** ：勾選時，按一下項目只會選取它（不套用），並會出現 **名稱** 方塊與 **Add** / **取代** / **重新命名** / **刪除**。**Add** 以所給名稱附加目前的設定，**取代** 以目前的設定覆寫所選項目，**重新命名** 只變更其名稱。

顯微鏡預設組（ARM300F、2100F、…）不在此清單中；它們位於 **TEM 條件** 的右鍵選單中（請參閱 [HRTEM 模擬](1-hrtem-simulation.md#tem-conditions)）。

---

## 檔案選單

![檔案選單](../../assets/cap-zh-Hant-auto/FormImageSimulator.menuStrip1.fileToolStripMenuItem.png)

- **儲存影像** ：可儲存 **為影像 (PNG 格式)**、**為影像 (TIFF 格式)** 或 **為中繼檔 (EMF)**。PNG 與 EMF 會儲存所顯示的內容（顯示範圍與色階）；**序列影像模式下個別儲存**（預設開啟）會將每個窗格寫成各自的檔案，而非一張拼接的影像。TIFF 一律為每個窗格寫出一個浮點數檔案，且不含疊加。它儲存的是經 **正規化**（若有套用 **高斯模糊 (FWHM)**，則也經其處理）後的像素值，而非 **調整** 的顯示範圍；在預設值（**Min** 0、**Max** 1、**每張影像**）下，每張影像會縮放至 0–1，因此若要匯出計算所得的強度，請取消勾選 **Min** / **Max**。EDX 分布圖一律不經縮放寫出。
- **複製影像** ：以 **為影像** 或 **為中繼檔 (EMF)** 複製到剪貼簿。
- **疊印符號**（預設開啟）：將晶胞、標籤與比例尺燒入儲存或複製的 PNG / EMF 影像中。
- **載入 TEM 參數** / **儲存 TEM 參數** ：會顯示但已停用（未實作）。請使用[預設清單](#preset-list)儲存與叫用條件。

## 說明選單

![說明選單](../../assets/cap-zh-Hant-auto/FormImageSimulator.menuStrip1.helpToolStripMenuItem.png)

- **HRTEM 模擬的基本概念** ：開啟 HRTEM 成像的說明（[附錄 A3.2](../appendix/a3-bloch-wave/hrtem.md)）。
- **計算函式庫** ：選擇計算函式庫——**Native code**（快速的 C++/Eigen）或 **Managed code**（.NET）。通常 Native 較快。此選擇只用於 HRTEM 影像合成；STEM 會自動選擇其求解器（STEM-EDX 一律需要原生程式庫），且每次啟動 ReciPro 時此選擇都會重設為 **Native code**。

---

## 另請參閱

- [HRTEM 模擬](1-hrtem-simulation.md)
- [STEM 模擬](2-stem-simulation.md)
- [位能模擬](3-potential-simulation.md)
- [動力學繞射 (布洛赫波)](../appendix/a3-bloch-wave/index.md)
- [繞射模擬器](../7-diffraction-simulator/index.md)
- [電子軌跡](../8-electron-trajectory.md)
