# ReciPro 手冊

## 簡介
* ReciPro 是一款採用 MIT 授權條款的免費軟體，提供多種晶體學計算與電子顯微鏡模擬功能。
* 自 2020 年 3 月在 GitHub 發布以來，ReciPro 累計下載量已超過 27,000 次，並為眾多晶體學家與電子顯微鏡研究者所使用。

## 依目標查找

| 目標 | 從這裡開始 | 主要後續步驟 |
|------|------------|-----------------|
| 載入晶體並設定其取向 | [主視窗](0-main-window.md) | [旋轉幾何](4-rotation-geometry.md)、[附錄 A1. 座標系](appendix/a1-coordinate-system/1-orientation.md) |
| 以 3D 方式檢視晶體結構 | [結構檢視器](5-structure-viewer.md) | [對稱性資訊](2-symmetry-information.md) |
| 計算 SAED / XRD / PED / CBED 圖樣 | [繞射模擬器](7-diffraction-simulator/index.md) | [SAED](7-diffraction-simulator/1-saed-simulation.md)、[X 射線繞射](7-diffraction-simulator/4-x-ray-neutron-diffraction.md)、[PED](7-diffraction-simulator/2-ped-simulation.md)、[CBED](7-diffraction-simulator/3-cbed-simulation.md) |
| 計算 HRTEM / STEM 影像 | [HRTEM/STEM 模擬器](9-hrtem-stem-simulator/index.md) | [HRTEM](9-hrtem-stem-simulator/1-hrtem-simulation.md)、[STEM](9-hrtem-stem-simulator/2-stem-simulation.md) |
| 模擬 EBSD 圖樣 | [EBSD 模擬](12-ebsd-simulation.md) | [電子軌跡](8-electron-trajectory.md)、[附錄 A3. EBSD 計算](appendix/a3-bloch-wave/ebsd.md) |
| 標定實驗繞射斑點 | [Spot ID v1](10-spot-id.md)、[Spot ID v2](11-spot-id-v2.md) | [繞射模擬器](7-diffraction-simulator/index.md) |
| 理解動力學繞射方程式 | [附錄 A3. 布洛赫波法](appendix/a3-bloch-wave/index.md) | [動力學計算](appendix/a3-bloch-wave/calculation.md)、[CBED](appendix/a3-bloch-wave/cbed.md)、[STEM](appendix/a3-bloch-wave/stem.md)、[EBSD](appendix/a3-bloch-wave/ebsd.md) |

## 功能
* **Full GUI** : 所有操作均透過圖形介面完成。大多數檔案輸入/輸出支援拖放。
* **晶體清單** : 同時管理多個晶體；無需為每個晶體個別開啟視窗。
* **空間群資料庫** : 內建資料庫涵蓋 International Tables Volume A 的 230 個空間群，以及 530 個 Hall 符號，並含對稱元素、維科夫位置與消光規則。對稱元素與一般位置可繪製為 *International Tables* Vol. A 風格的示意圖（參見 [2. 對稱性資訊](2-symmetry-information.md)）。
* **原子資訊** : 包含元素 H (1) – Cf (98) 的散射因子（X 射線、電子、中子）、特徵 X 射線能量、同位素比等。
* **靈活的晶體旋轉** : 可透過晶帶軸/晶面指數或滑鼠拖動來設定取向。三方/六方晶系支援米勒-布拉維（4 指數 *hkil*）記法。旋轉狀態會在所有模擬視窗之間同步。
* **繞射模擬** : 運動學與動力學（布洛赫波法）電子繞射、X 射線繞射（包括進動相機與後勞厄相機）、進動電子繞射 (PED) 以及會聚束電子繞射 (CBED)。TEM 試樣台模擬將繞射圖樣與試樣台傾斜角聯繫起來。
* **HRTEM / STEM 模擬** : 採用部分同調模型的高解析 TEM 影像模擬；含熱漫散射的 STEM。
* **EBSD 與電子軌跡** : EBSD 圖樣模擬以及電子軌跡的蒙地卡羅模擬（參見 [8. 電子軌跡](8-electron-trajectory.md)）。
* **斑點標定** : 自實驗影像中自動偵測、擬合並標定繞射斑點（Spot ID v1/v2）。
* **巨集** : 用於自動化操作的 Python 語法巨集（參見 [20. 巨集](20-macro/index.md)）。
* **淺色 / 深色佈景主題** : 介面支援可選的淺色或深色配色模式。

## 系統需求
| 項目 | 最低需求 | 建議配置 |
|------|---------|-------------|
| OS | 安裝有 [.NET Desktop Runtime 10.0](https://dotnet.microsoft.com/download/dotnet/10.0) 的 Windows（支援 Windows on ARM64） | Windows 11 |
| GPU | OpenGL 1.3 | 支援 OpenGL 4.3 的獨立 GPU |
| 記憶體 | - | 16 GB 或更多 |
| CPU | - | 8 核以上（用於動力學計算） |

**Windows on ARM（原生，實驗性）** : 實驗性的原生 ARM64 可攜套件（`ReciPro-v.X_arm64.zip`，自包含 — 無需安裝 .NET Runtime）可在[發布頁面](https://github.com/seto77/ReciPro/releases/latest)取得。常規的 x64 套件也可在 ARM64 Windows 上透過內建模擬執行。安裝說明與限制請參見[疑難排解](troubleshooting.md#windows-on-arm)。

**macOS（非官方）** : ReciPro 官方僅支援 Windows，但據回報，**win-x64** 可攜 ZIP 套件可在 macOS（Apple Silicon）上透過 Sikarugir Wine 封裝器結合 Mesa3D OpenGL 驅動程式執行。一份使用者發布的安裝指南位於 <https://github.com/Ryo-fkushima/ReciPro_macOS_memo>。請注意，此途徑並非官方支援，且某些符號（Å、上標、箭頭）可能顯示不正確。ARM64 ZIP **無法**在 macOS + Wine 上執行，而目前的 x64 + Rosetta 2 途徑預計自 macOS 28（2027 年秋季）起將停止運作 — 詳情請參見[疑難排解](troubleshooting.md#mac-linux)。

## 如何使用本手冊

此 GitHub Pages 手冊是目前的權威來源。可使用左側導覽依章節瀏覽，或使用頁首中的搜尋來查找某個功能名稱或 UI 標籤。舊版 PDF 手冊僅作存檔參考保留。

* **存檔 PDF（英文）：** <https://raw.githubusercontent.com/seto77/ReciPro/master/ReciPro/doc/ReciProManual(en).pdf>
* **存檔 PDF（日文）：** <https://raw.githubusercontent.com/seto77/ReciPro/master/ReciPro/doc/ReciProManual(ja).pdf>

## 快速開始
1. 從 [Releases](https://github.com/seto77/ReciPro/releases/latest) 下載並安裝。
2. 從內建清單（約 80 個晶體）中選擇一個晶體。你也可以匯入 CIF 檔案或使用 [CSManager](https://github.com/seto77/CSManager)。
3. 從右側面板呼叫各項功能：結構檢視器、極網、繞射模擬器、HRTEM 模擬等。
4. 透過滑鼠拖動或輸入晶帶軸/晶面指數來旋轉晶體。

## 引用
> Y. Seto, "ReciPro: free and open-source multipurpose crystallographic software integrating a crystal operation interface and diffraction simulators," *J. Appl. Cryst.* **55**, 397–410 (2022). <https://doi.org/10.1107/S1600576722000139>

## 授權條款
ReciPro 以 [MIT 授權條款](https://github.com/seto77/ReciPro/blob/master/LICENSE.md) 散布。
