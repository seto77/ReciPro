# ReciPro

[![Documentation](https://img.shields.io/badge/%F0%9F%93%96_Documentation-blue)](https://seto77.github.io/ReciPro/zh-Hant/)
[![Latest Release](https://img.shields.io/github/v/release/seto77/ReciPro?logo=github)](https://github.com/seto77/ReciPro/releases/latest)
[![Total downloads](https://img.shields.io/github/downloads/seto77/ReciPro/total?logo=github&label=GitHub%20downloads)](https://github.com/seto77/ReciPro/releases)
[![GitHub Stars](https://img.shields.io/github/stars/seto77/ReciPro?style=social)](https://github.com/seto77/ReciPro/stargazers)
[![GitHub Forks](https://img.shields.io/github/forks/seto77/ReciPro?style=social)](https://github.com/seto77/ReciPro/forks)
[![License: MIT](https://img.shields.io/badge/License-MIT-green)](https://github.com/seto77/ReciPro/blob/master/LICENSE.md)

<!-- 260804Cl: ../../README.md（英文版）的譯文。英文版更新時請同步更新本檔案。 -->
[English](../../README.md) | [日本語](README.ja.md) | [Deutsch](README.de.md) | [Français](README.fr.md) | [Español](README.es.md) | [Italiano](README.it.md) | [Русский](README.ru.md) | [简体中文](README.zh-Hans.md) | **繁體中文** | [한국어](README.ko.md) | [Português](README.pt.md)

*ReciPro* 是一套免費開源、以圖形介面為基礎的多用途晶體學軟體，可順暢地使用晶體資料庫檢索、晶體結構與測角儀設定的視覺化、繞射圖樣與高解析度顯微影像的模擬，以及繞射資料分析等功能。這些功能透過友善的圖形介面彼此連動，計算結果幾乎可即時同步顯示。*ReciPro* 能協助使用 X 光、電子與中子繞射晶體學以及穿透式電子顯微鏡的廣大晶體學研究者（包括初學者）。

*ReciPro* 自 2002 年起持續開發，並自 2020 年 3 月起在 GitHub 上公開。在 GitHub 上的下載次數已超過 27,000 次，並由大學與企業十餘個實驗室的數百位使用者所採用。

***[請參閱手冊了解使用方式！](https://seto77.github.io/ReciPro/zh-Hant/)***

[即時執行的各種模擬（範例：MgAl2O4）](https://github.com/user-attachments/assets/6b0234dd-f2d6-49db-b146-bb74cf6021b6)

## 作者

*ReciPro* 由 [Seto Y.](https://yseto.net/en/home-e) 與 [Ohtsuka M.](https://researchmap.jp/7000002999?lang=en) 開發。相關功能與演算法已在[論文](https://github.com/seto77/ReciPro/blob/master/docs/ReciProSetoOhtsuka2022.pdf)中介紹。

## 引用

若您在學術工作中使用 *ReciPro*，請使用 GitHub 儲存庫頁面上顯示的 **Cite this repository** 連結。引用中繼資料由 `CITATION.cff` 提供，建議引用下列文章：

  * [Seto, Y. & Ohtsuka, M. (2022). *J. Appl. Cryst.* **55**, 397-410, doi: 10.1107/S1600576722000139.](https://doi.org/10.1107/S1600576722000139)

必要時亦可引用軟體儲存庫本身：

  * 儲存庫：https://github.com/seto77/ReciPro
  * 版本發佈：https://github.com/seto77/ReciPro/releases/latest

***

## 安裝

* 下載 [*ReciPro-setup.msi*](https://github.com/seto77/ReciPro/releases/latest/download/ReciPro-setup.msi)（最新版本的直接連結）並執行。也可在[發佈頁面](https://github.com/seto77/ReciPro/releases/latest)取得。（v.4.939 之前，安裝程式名為 *ReciProSetup.msi*。）
* *ReciPro* 需在安裝 ***.Net Desktop Runtime 10.0***（不是 ***.Net Runtime 10.0***）的 Windows 系統上執行，執行階段可從[這裡](https://dotnet.microsoft.com/download/dotnet/10.0)安裝。
* 若無法執行安裝程式（例如在權限受限的電腦上），發佈頁面亦提供 **可攜式 ZIP** 套件（*ReciPro-v.X.XXX.zip*）：自我包含，不需安裝、不需 .NET 執行階段，解壓縮後即可執行。
* *ReciPro* 以 **MIT 授權**發佈（任何人皆可自由使用、修改與再散布）。
* 有關程式碼簽章狀態與安裝程式的驗證方式，請參閱[程式碼簽章政策](../../CODE_SIGNING.md)。
* 有關隨附或引用的第三方元件與資料，請參閱[第三方聲明](../../THIRD-PARTY-NOTICES.md)。

### macOS（非官方）

* *ReciPro* 官方僅支援 Windows，但有回報指出，將**可攜式 ZIP** 套件與 **Sikarugir** Wine 封裝以及 **Mesa3D** OpenGL 驅動程式搭配使用，即可在 macOS（Apple Silicon）上執行，不需 Windows 授權或虛擬機器。
* 請參閱 Ryo Fukushima（JAMSTEC）發表的逐步設定指南：https://github.com/Ryo-fkushima/ReciPro_macOS_memo
* 此組態未獲官方支援，也未經完整驗證。已知限制為部分符號（Å、上標、箭頭）可能顯示不正確。
* 只要在 Wine prefix 中安裝字元涵蓋範圍較廣的字型（**DejaVu Sans/Serif**，日文介面另需 **Noto Sans CJK JP**），即可解決亂碼問題——ReciPro 會偵測 Wine 環境並自動切換至這些字型。詳情請參閱[疑難排解](https://seto77.github.io/ReciPro/zh-Hant/troubleshooting/)。

### 關於 Windows 安全性警告

* 請僅從官方 GitHub Releases 頁面下載 *ReciPro*：https://github.com/seto77/ReciPro/releases/latest
* 在部分 Windows 系統上，Microsoft Defender SmartScreen 或 Smart App Control 可能會在執行安裝程式前顯示警告。對於剛建置或散布範圍有限的研究軟體而言，這種情況並不罕見，警告本身並不必然代表安裝程式具有惡意。
* 若您希望自行驗證下載的安裝程式，可使用 VirusTotal 等多引擎掃描服務進行檢測。

## 程式碼簽章政策

[<img src="https://signpath.org/assets/favicon-50x50.png" alt="SignPath" height="20">](https://about.signpath.io/) Windows 平台的免費程式碼簽章由 [SignPath.io](https://about.signpath.io/) 提供，憑證由 [SignPath Foundation](https://signpath.org/) 核發。

自 v.4.942 起，發佈成品（*ReciPro-setup.msi* 安裝程式與可攜式 *ReciPro.exe*）會在自動化發佈流程中以 Windows Authenticode 簽章，且每次簽章請求都由維護者於發佈前審核並手動核准。完整政策（包含簽章範圍、如何驗證安裝程式，以及如何回報可疑成品）請參閱 [CODE_SIGNING.md](../../CODE_SIGNING.md)。

## 隱私權

*ReciPro* 是在本機執行的桌面應用程式。它**不會**收集、儲存或傳送任何個人資料或使用資料，也不含遙測或分析功能。安裝後可完全離線運作。

*ReciPro* 唯一會建立的網路連線，是由使用者主動觸發的選擇性下載，且皆不會上傳您的資料：

* **檢查更新**（功能表指令）：比較已安裝版本與 GitHub 上的最新發佈版本，若您選擇更新，則從官方 [GitHub Releases](https://github.com/seto77/ReciPro/releases/latest) 頁面下載新的安裝程式。
* **COD 資料庫**（Crystallography Open Database）：首次使用時由作者的 GitHub 鏡像下載（約 880 MB），之後即可離線使用。
* **Intel MKL 函式庫**（選用的加速功能）：僅在啟用 *Use MKL* 選項時，由 [nuget.org](https://www.nuget.org/) 下載（約 55 MB），用以加速動力學繞射計算。

隨附的 AMCSD 資料庫與所有核心功能皆可完全離線運作。

## 手冊
  * 線上手冊（英文 / 日文）：https://seto77.github.io/ReciPro/zh-Hant/
  * 日文版：https://yseto.net/soft/recipro
***

## 主要功能

### 晶體資料庫

* **AMCSD**（American Mineralogist Crystal Structure Database）：內建 21,000 種以上的晶體結構，安裝後即可使用。
  * 資料庫經過高度壓縮（約 5 MB）並包含於安裝檔中，因此在離線環境亦可使用。
  * 可依名稱、化學組成、晶格參數、密度、對稱性與所含元素搜尋晶體。
  * 參考文獻：[Downs & Hall-Wallace, 2003, *American Mineralogist* **88**, 247-250](https://www.geo.arizona.edu/xtal/group/pdf/am88_247.pdf)
* **COD**（Crystallography Open Database）：另可使用約 525,000 種晶體結構，包括有機晶體。
  * 首次使用時自動下載（約 880 MB），之後即可離線使用。
  * 參考文獻：[Gražulis et al., 2009, *J. Appl. Cryst.* **42**, 726-729](https://doi.org/10.1107/S0021889809016690)；[Gražulis et al., 2012, *Nucleic Acids Res.* **40**, D420-D427](https://doi.org/10.1093/nar/gkr900)
* 支援 CIF 與 AMC 格式檔案的匯入/匯出。

### 晶體學計算

* 支援 530 種空間群表示法：230 種標準 ITA 設定 + 300 種非標準軸設定。
  * 所有空間群的一般條件（消光法則）、Wyckoff 位置與多重度。
  * 面與面、軸與軸之間週期性與/或夾角的幾何計算。
  * 產生等效原子位置。
  * 可於非標準軸設定之間（例如 *Pbnm* 轉為 *Pnma*）以及原點位移之間輕鬆轉換。

### 原子性質

* <sup>1</sup>H 至 <sup>98</sup>Cf 特性 X 光的波長/能量。
* X 光、電子與中子的原子散射因子。

### 結構檢視器

* 以 OpenGL（GLSL）架構進行三維晶體結構視覺化。
  * 可繪製原子、鍵結、配位多面體、單位晶胞、晶面、邊界面與圖例標籤。
  * 即使是包含數萬個原子的複雜晶體結構，也能即時流暢地繪製。
  * 預設的原子繪製顏色與大小與 VESTA 相容。
  * 繪製範圍可用單位晶胞倍數指定，也可用晶面指數與距中心的距離指定。
  * 為邊界面上色即可呈現任意的晶體外形。
  * 可顯示任意晶面，有助於初學者理解繞射現象中晶面的概念。
  * 旋轉、平移與縮放皆可用滑鼠自由操作。
  * 點選原子可顯示與相鄰原子之間的距離與鍵角。
  * 旋轉狀態會立即反映到其他功能視窗（極射投影、繞射模擬器等）。
  * 內建影片編碼器（Windows Media Foundation）可產生用於簡報的旋轉動畫影片（H.264/H.265 MP4）。

### 極射投影

* 在極射投影圖上標繪晶面與晶軸。
  * 同時支援等角投影（吳氏網）與等面積投影（施密特網），並可顯示經緯線。
  * 指數可用數值範圍或特定數值指定。
  * 可藉由指定晶帶軸顯示大圓。
  * 繪製物件可以向量格式儲存或複製，日後編輯不會損失解析度。
  * 供教學使用的極射投影幾何三維視覺化。

### 繞射模擬器

* 模擬 X 光、電子與中子光源的單晶繞射圖樣。
  * 入射束的動能可自由設定。
  * 內建 <sup>1</sup>H 至 <sup>98</sup>Cf 的特性 X 光能量。
  * 繪製範圍由影像解析度（像素大小）與相機長度指定。
  * 亦支援偵測器傾斜的幾何配置。
  * 支援疊加實驗取得的影像。
  * 可控制晶體旋轉（繞射條件），並立即與其他視窗同步。

* **多晶繞射**：假設多晶試樣的德拜環圖樣模擬。
* **旋進相機**（X 光）：零階勞厄帶旋進相機圖樣模擬。
* **背反射勞厄相機**（X 光）：背反射勞厄圖樣模擬。

#### 運動學繞射理論
* 適用於所有光源（X 光、電子、中子）。
* 繞射強度由晶體結構因子振幅的平方與激發誤差推估。
* 已納入德拜–沃勒因子對繞射強度的影響。

#### 動力學繞射理論（電子）
* 以**布洛赫波法**（Bethe, 1928）為基礎，可彈性設定晶體方位，不受低指數晶帶軸的限制。
* 提供兩種計算方式：
  * **Bethe 特徵值法**：以矩陣對角化求布洛赫本徵態的特徵值/特徵向量，適合改變試樣厚度的情形。
  * **散射矩陣法**：以縮放平方法搭配 Padé 近似直接計算矩陣指數，適合單一厚度的快速計算。
* 自動選擇最快的演算法與最合適的數學函式庫（Eigen、Intel MKL 或 Math.NET）。
* 熱漫散射（TDS）吸收位能採解析方式計算，以獲得高效能。

* **SAED**（選區電子繞射）：含動力學散射效應的平行束電子繞射模擬。
* **PED**（旋進電子繞射）：指定旋進角與方位角解析度即可模擬 PED 圖樣。可用於晶體結構解析以及準運動學 PED 條件的最佳化。
* **CBED**（會聚束電子繞射）：可指定會聚半角與分割數模擬 CBED 圖樣。支援沿厚度方向的模擬，用以判定試樣厚度。
  * 位置平均 CBED（PACBED）圖樣。
  * 大角度 CBED（LA-CBED）模擬。

### HRTEM 模擬器

* 在相同的布洛赫波理論架構下進行高解析度穿透式電子顯微影像模擬。
* 光學參數（加速電壓、球面像差係數、失焦量、試樣厚度等）透過圖形介面設定。
* 內建典型的 TEM 光學參數預設值，可用右鍵呼叫。
* 針對部分同調性提供兩種成像模型：
  * **線性對比傳遞理論**：計算成本較低，適用於滿足弱相位物體近似的薄試樣。
  * **非線性對比傳遞理論（TCC 模型）**：以一階穿透交叉係數（Ishizuka, 1980）為基礎，即使對較厚試樣與較高原子序材料亦具可靠性。
* 可繪製含包絡函數的對比傳遞函數。
* 可同時計算厚度–失焦系列影像。
* 在標準條件下通常可於 1 秒內完成計算。

### STEM 模擬器

* 掃描穿透式電子顯微影像模擬。
  * 明場（BF）、環狀暗場（ADF）與高角度環狀暗場（HAADF）成像模式。
  * 會聚束視為多個平面波的疊加，並精確計算重疊部分。
  * 非彈性散射電子以吸收位能模型計算。
  * 可產生厚度–失焦系列影像。

### Spot ID

* 針對實測 SAED 圖樣的半自動繞射斑點指標化。
* **Spot ID v1**：利用繞射斑點的幾何配置（距離與夾角）搜尋晶帶軸。支援同時分析 2–3 張影像。
* **Spot ID v2**：直接匯入 SAED 圖樣影像。
  * 支援常見影像格式：TIFF (.tif)、Digital Micrograph 3/4 (.dm3, .dm4) 等。
  * 自動偵測繞射斑點並以二維 pseudo-Voigt 函數擬合。
  * 徹底搜尋與倒晶格向量排列相符的晶體方位。
  * 即使是高階晶帶軸也能準確判定。

### 旋轉幾何（測角儀）

* 將 ReciPro 中的尤拉角與實驗室的測角儀連結。
* 提供為達到所需晶體方位（例如低指數晶帶軸）應如何旋轉測角儀的資訊。
* 支援任意的測角儀定義。

### 巨集

* 採用 Python 語法的巨集指令碼，可自動化各項作業。
  * 範例：以 1° 為間隔旋轉晶體，並在每一步儲存繞射圖樣或 STEM 影像。
  * ReciPro 專用函式位於「ReciPro」命名空間中。
  * 使用範例請見[手冊](https://seto77.github.io/ReciPro/zh-Hant/20-macro/2-examples/)。

### 其他功能

* **電子射程模擬器**：材料中電子射程的蒙地卡羅模擬。
* **EBSD**（電子背向散射繞射）：開發中。

## 技術細節

* 以 **C++**、**C#** 與 **OpenGL 著色語言（GLSL）** 撰寫。
* 採用多執行緒平行化，在現代多核心 CPU 上達成高效能計算。
* 晶體方位變更時，所有功能視窗都會即時同步更新。
* 採用右手笛卡兒座標系（X：右，Y：上，Z：前）與 Z–X–Z 尤拉角慣例。
* 座標定義與 Thermo Fisher Scientific 的 EBSD 軟體相容。

### 學術影響

* **同儕審查的軟體論文：** [Seto, Y. & Ohtsuka, M. (2022), *Journal of Applied Crystallography*, **55**, 397-410](https://doi.org/10.1107/S1600576722000139).
* **引用論文：** [Google Scholar 引用文獻](https://scholar.google.jp/scholar?cites=12625594477623342627).
* **論文關注度：** [Altmetric 詳細資訊](https://www.altmetric.com/details/123778746).

| 指標 | 主要數值 |
| --- | --- |
| GitHub 累計下載次數 | 27,000 次以上 |
| Google Scholar 被引用次數 | 170 次以上 |
| Dimensions 被引用次數 | 160 次以上 |
| Mendeley 讀者數 | 90 人以上 |

## 螢幕擷圖

<img src="https://seto77.github.io/ReciPro/assets/cap-zh-Hant-auto/FormMain.png" height="320px" alt="主視窗">
<img src="https://seto77.github.io/ReciPro/assets/cap-zh-Hant-auto/FormCrystalDatabase.png" height="320px" alt="晶體資料庫">
<img src="https://seto77.github.io/ReciPro/assets/cap-zh-Hant-auto/FormSymmetryInformation.png" height="320px" alt="對稱性資訊">
<img src="https://seto77.github.io/ReciPro/assets/cap-zh-Hant-auto/FormBeamInteraction.png" height="320px" alt="射束交互作用">
<img src="https://seto77.github.io/ReciPro/assets/cap-zh-Hant-auto/FormStructureViewer.png" height="320px" alt="結構檢視器">
<img src="https://seto77.github.io/ReciPro/assets/cap-zh-Hant-auto/FormStereonet.png" height="320px" alt="極射投影">
<img src="https://seto77.github.io/ReciPro/assets/cap-zh-Hant-auto/FormDiffractionSimulator.png" height="320px" alt="繞射模擬器">
<img src="https://seto77.github.io/ReciPro/assets/cap-zh-Hant-auto/FormImageSimulator.png" height="320px" alt="HRTEM/STEM 模擬器">
<img src="https://seto77.github.io/ReciPro/assets/cap-zh-Hant-auto/FormSpotIDV2.png" height="320px" alt="Spot ID v2">
<img src="https://seto77.github.io/ReciPro/assets/cap-zh-Hant-auto/FormMacro.png" height="320px" alt="巨集">
<img src="https://seto77.github.io/ReciPro/assets/cap-zh-Hant-auto/FormTrajectory.png" height="320px" alt="電子射程模擬器">

***
