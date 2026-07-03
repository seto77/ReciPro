# 疑難排解

ReciPro 的常見問題與解決方法。下面的許多條目來自 [GitHub issue 追蹤器](https://github.com/seto77/ReciPro/issues) 上的提問與缺陷回報；在適用的情況下，標註了修復該缺陷的版本。

> **大多數問題只需更新到[最新版本](https://github.com/seto77/ReciPro/releases/latest)即可解決。** ReciPro 更新頻繁，下面的許多缺陷在回報後數日內即已修復。

---

## 啟動與執行

### 症狀：行程正在執行，但沒有出現視窗

ReciPro 已啟動（在工作管理員中可見），但其視窗始終不在螢幕上顯示。

**原因**：視窗在螢幕之外開啟——這是 Windows 顯示座標的問題，通常發生在更換顯示器或變更顯示縮放之後。（Issues [#50](https://github.com/seto77/ReciPro/issues/50)、[#53](https://github.com/seto77/ReciPro/issues/53)、[#55](https://github.com/seto77/ReciPro/issues/55)）

**解決方法**：

1. 開啟**工作管理員**。
2. 在行程清單中找到 **ReciPro**。
3. 右鍵點按它並選擇**最大化**。

視窗將被帶到主顯示器上。請注意，**切換到**、**移到最上層**與**最小化**都*無濟於事*——只有**最大化**有效。

### 症狀：ReciPro 無法啟動、當機或在啟動時停止回應

**原因**：最常見的是 OpenGL 初始化失敗，或損壞的登錄檔/設定值阻止了啟動。

**解決方法**（按順序嘗試）：

1. **停用 OpenGL**：啟動 ReciPro 時按住 **Ctrl** 鍵，以停用 OpenGL 的方式啟動。較新的版本（v4.925 及以後）強化了 OpenGL 初始化，因此即使 OpenGL 失敗應用程式也能啟動——在這種情況下 3D 功能被停用，但應用程式的其餘部分可正常運作。
2. **重設設定**：在登錄檔編輯器中刪除機碼 `HKEY_CURRENT_USER\Software\Crystallography\ReciPro`，然後重新啟動。（等同於 **選項 → 重設登錄檔**。）
3. **乾淨重新安裝**：解除安裝 ReciPro，刪除以下資料夾（如果存在，將 `<user>` 替換為你的帳戶名稱），然後重新安裝：
   - `C:\Users\<user>\AppData\Local\Crystallography Software\ReciPro`
   - `C:\Users\<user>\AppData\Roaming\ReciPro\ReciPro`
4. **更新**到最新版本。

如果以上都無濟於事，原因可能在於作業系統環境本身；請連同你的 PC 詳情（CPU、GPU、Windows 版本）一起[提交一個 issue](https://github.com/seto77/ReciPro/issues)。

---

## OpenGL 問題

### 症狀：啟動時黑畫面或當機

**原因**：GPU 不相容或遠端桌面環境。

**解決方法**：

1. 前往 **選項 → 停用 OpenGL（需重新啟動）**（或在啟動時按住 **Ctrl**）。
2. 重新啟動 ReciPro。
3. 結構檢視器和部分 3D 功能將使用軟體算繪。

### 症狀：內建或較舊的 GPU（Intel/AMD）無法算繪

**原因**：某些內建 GPU（例如 AMD Radeon Vega、Intel UHD）在較舊的組建中存在 OpenGL 初始化問題。（Issue [#2](https://github.com/seto77/ReciPro/issues/2)）

**解決方法**：更新到最新版本。OpenGL 版本需求已降低（v4.781），內建 GPU 的初始化已修復（v4.785），初始化也進一步得到強化以實現可控失敗（v4.925）。更新 GPU 驅動程式同樣有幫助。

### 症狀：算繪品質差

**解決方法**：更新 GPU 驅動程式。建議使用支援 OpenGL 1.5 的外接（獨立）GPU。

---

## .NET 執行階段

### 症狀：應用程式無法啟動

**原因**：未安裝所需的 .NET Desktop Runtime。目前版本需要 **.NET Desktop Runtime 10.0**（較舊的組建：v4.895–v4.91x 需要 9.0；參見 issue [#43](https://github.com/seto77/ReciPro/issues/43)）。

**解決方法**：從 <https://dotnet.microsoft.com/download/dotnet/10.0> 下載並安裝（選擇 **Desktop Runtime**，大多數 PC 選 x64）。

### 症狀：無法存取 Microsoft 下載頁面

**解決方法**：你可以直接下載執行階段安裝程式。在 [.NET 10.0 下載頁面](https://dotnet.microsoft.com/download/dotnet/10.0) 上為你的架構選擇 **Windows Desktop Runtime X64**。（Issue [#49](https://github.com/seto77/ReciPro/issues/49)）

---

## 安裝

### 症狀：在沒有系統管理員權限的情況下安裝或解除安裝

**注意**：不需要系統管理員權限。捷徑和每個使用者的檔案被放置在你自己的使用者資料夾中（例如 `%AppData%\Microsoft\Windows\Start Menu\Programs\Crystallography Software\` 和桌面）。（Issue [#38](https://github.com/seto77/ReciPro/issues/38)）

---

## 顯示與版面配置

### 症狀：按鈕或面板被裁切 / 隱藏，或版面配置看起來已損壞

例如，在較新的版本中，Spot ID v2 中的 **Peak Identification** 按鈕被隱藏，或者「關於」頁面及其他表單未對齊。（Issues [#56](https://github.com/seto77/ReciPro/issues/56)、[#59](https://github.com/seto77/ReciPro/issues/59)）

**原因**：在某些較新的組建中引入的 DPI 縮放 / UI 字型回歸問題。

**解決方法**：

- 將 Windows **顯示縮放設定為 100%**（這通常可恢復版面配置）。
- 作為快速權宜方法，**調整視窗大小**（例如在垂直方向縮小），以顯示被隱藏的控制項。
- 更新到最新版本——版面配置正在逐步修復。如果某個較新的組建看起來更糟，回復到稍舊的版本（例如 v4.915）是一種暫時選擇。如有仍然損壞的表單，請回報。

---

## 動力學計算

### 症狀：非常慢或記憶體不足

**原因**：布洛赫波過多或影像過大。

**解決方法**：

- 減少 **No. of Bloch waves**（50–200 通常足以滿足常規計算）
- 對 ≤ 500 個波使用 **Eigen** 求解器；對 > 500 個波使用 **MKL**
- 降低 STEM 模擬的影像解析度
- 關閉其他佔用大量記憶體的應用程式

### 症狀：HAADF-STEM 影像為黑色

**原因**：原子溫度因子（B）被設定為零。

**解決方法**：為所有原子設定 B ≥ 0.5 Å²。TDS 強度需要非零的溫度因子。

---

## 繞射模擬器

### 症狀：繞射圖樣為空白 / 什麼都沒有繪製

**原因**：通常是檢視放大過度，或入射波能量超出範圍。（Issue [#3](https://github.com/seto77/ReciPro/issues/3)）

**解決方法**：

- **左鍵點按**主繪圖區域以縮小。
- 在 **Wave** 索引標籤（左上角）檢查入射波能量：X 射線 ≈ 1–100 keV、電子 ≈ 10–1000 keV 為適宜值。

---

## 檔案輸入/輸出

### 症狀：CIF 檔案無法載入

**解決方法**：

- 檢查 CIF 檔案是否格式良好
- 嘗試將檔案拖放到**晶體資訊**區域
- 某些非標準的 CIF 擴充規格可能不受支援

### 症狀：dm3/dm4 檔案無法載入，或出現 "unable to cast … 'System.Single' to 'System.Double'"

**原因**：DM3/DM4 格式有多種變體，較舊的組建無法讀取全部變體。（Issue [#15](https://github.com/seto77/ReciPro/issues/15)）

**解決方法**：更新到最新版本——DM3 讀取相容性已在 v4.835 中改善。如果檔案仍無法載入，請[傳送給我們](https://github.com/seto77/ReciPro/issues)，以便新增支援。

### 症狀：dm3/dm4 檔案顯示的比例尺錯誤

**解決方法**：在原始的 Digital Micrograph 軟體中核實校正。ReciPro 讀取內嵌的中繼資料；如果中繼資料不正確，請在光學面板中手動設定像素大小和相機長度。

---

## 重設登錄檔

如果設定變得損壞：

1. **選項 → 重設登錄檔（重新啟動後）**
2. 重新啟動 ReciPro——視窗位置、波長、相機長度等將被重設為預設值

---

## 常見問題

### 有 Mac（或 Linux）版本嗎？ {#mac-linux}

沒有官方的 Mac 或 Linux 版本。ReciPro 依賴 **.NET Desktop Runtime**，目前它僅在 Windows 上執行。（Issue [#12](https://github.com/seto77/ReciPro/issues/12)）

不過，有人回報了一條在 macOS 上可行的非官方途徑：**win-x64 portable ZIP** 發行版（可在[發布頁面](https://github.com/seto77/ReciPro/releases/latest)取得）藉助 **Sikarugir** Wine 包裝器結合 **Mesa3D** OpenGL 驅動程式，可在 macOS（Apple Silicon）上執行——無需 Windows 授權或虛擬機器。一位使用者發布的逐步設定指南可在 <https://github.com/Ryo-fkushima/ReciPro_macOS_memo> 取得。

請注意，此配置未獲官方支援，也未經過完全驗證。一項已知的限制是某些符號（Å、上標、箭頭）可能顯示不正確。

**修復亂碼符號（Å、上標、箭頭）：** 原因在於 ReciPro 通常使用的 Windows 字型（Segoe UI、Yu Gothic UI 等）在 Wine 環境中缺失，而 Wine 內建的替代字型缺少某些科學字符。ReciPro 在**偵測到它執行於 Wine 之下時**會自動切換到涵蓋範圍廣的字型，因此修復方法只是讓這些字型在 Wine prefix 中可用：

1. 安裝 **DejaVu Sans** / **DejaVu Serif**（涵蓋 Å、上標、箭頭、分數標籤），對於日語介面，安裝 **Noto Sans CJK JP**（或 **Noto Sans JP**）。
2. 最簡單的方法是將下載的 `.ttf`/`.otf` 檔案複製到 prefix 的字型資料夾中——即 Sikarugir 包裝器內的 `…/drive_c/windows/Fonts/`——然後重新啟動 ReciPro。（`winetricks` 也可以安裝其中一些。）
3. 重新啟動時 ReciPro 會自動辨識它們；無需變更任何 ReciPro 設定。

如果未安裝這些字型，ReciPro 會保留其預設字型名稱，因此不會變得更糟——符號只是仍然保持亂碼。

**關於此途徑的展望——兩點坦誠的說明：**

- 實驗性的 **win-arm64** ZIP 在目前的 Mac 上**無法**執行，即使是 Apple Silicon 也不行：當今的 macOS Wine 組建（包括 Sikarugir）透過 Rosetta 2 執行 x86_64 Windows 二進位檔，沒有任何機制來執行 ARM64 Windows 二進位檔。在 Mac 上請始終使用 **win-x64** portable ZIP。
- Apple 正在逐步淘汰 Rosetta 2。macOS 27（2026 年秋季）被宣布為最後一個完整支援 Rosetta 2 的版本，因此目前的 x64 + Rosetta 途徑預計從 macOS 28（2027 年秋季）起將不再可用。一個面向 macOS 的原生 ARM64 Wine 正在上游開發中；如果它得以實現，win-arm64 ZIP 可能成為 Mac 上的後繼方案，但目前還無法保證。

### ReciPro 能在 Windows on ARM（ARM64）上執行嗎？ {#windows-on-arm}

可以——有兩條途徑：

- **原生 ARM64 套件（實驗性，建議）**：從 v4.938 起，[發布頁面](https://github.com/seto77/ReciPro/releases/latest) 上發布了一個實驗性的原生 ARM64 portable 套件（`ReciPro-v.X_arm64.zip`；在 v.4.939 之前命名為 `ReciPro-v.X-arm64.zip`）。它是 self-contained 的，因此無需安裝 .NET Runtime——將 ZIP 解壓縮到一個使用者可寫入的資料夾並執行 `ReciPro.exe`。如果 Windows 封鎖了下載的 ZIP（Mark of the Web），請在解壓縮*之前*右鍵點按該 ZIP → **內容** → 勾選**解除封鎖** → **確定**（或在 PowerShell 中執行 `Unblock-File .\ReciPro-*arm64.zip`）。詳情見隨附的 `README-PORTABLE.txt`。
- **在模擬下執行的 x64 套件**：常規的 MSI 安裝程式和 win-x64 portable ZIP 在安裝了 .NET Desktop Runtime（x64）後，也可透過內建的 x64 模擬在 ARM64 Windows 上執行（約從 v4.913 配合 .NET 10 起確認可行）。繁重的計算執行起來比原生組建慢。（Issue [#47](https://github.com/seto77/ReciPro/issues/47)）

關於原生 ARM64 套件的說明：

- Intel MKL 沒有 ARM64 版本，因此相應的求解器選項和選單項目被隱藏。動力學計算使用隨附的經 NEON 最佳化的原生程式庫；在具有代表性的驗證案例中，其結果在預期的浮點容差範圍內與 x64 組建一致。
- 3D 檢視（結構檢視器及相關視窗）可以執行，但 Windows on ARM 僅透過 Direct3D 12 轉換層（GLOn12 / Mesa）提供 OpenGL，因此 3D 算繪明顯慢於配備原生 OpenGL 驅動程式的 PC——這是平台限制，並非缺陷，原生 ARM64 組建也無法改變這一點。結構檢視器中的 **High quality (Per-Pixel Linked List)** 透明模式在此驅動程式堆疊上尤其緩慢；建議使用預設的 **Approximate** 模式。如果 3D 檢視無法啟動，請從 Microsoft Store 安裝「OpenCL, OpenGL, and Vulkan Compatibility Pack」。
- ARM64 套件**不能**在 macOS + Wine 下執行（參見上一個問題）。在 Mac 上請使用 win-x64 portable ZIP。

### 我應該如何引用 ReciPro？

使用 [GitHub 儲存庫頁面](https://github.com/seto77/ReciPro) 上的 **Cite this repository** 連結（中繼資料由 `CITATION.cff` 提供）。建議的引用為：

> Seto, Y. & Ohtsuka, M. (2022). *J. Appl. Cryst.* **55**, 397–410. doi:[10.1107/S1600576722000139](https://doi.org/10.1107/S1600576722000139)

（Issue [#33](https://github.com/seto77/ReciPro/issues/33)）

---

## 回報缺陷

請在此回報問題：<https://github.com/seto77/ReciPro/issues>

請包含：

- ReciPro 版本號
- 重現問題的步驟
- 任何錯誤訊息或螢幕擷取畫面
