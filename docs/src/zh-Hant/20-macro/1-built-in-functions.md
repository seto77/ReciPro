# 內建函式

ReciPro 巨集中可用的類別與函式完整參考。

---

## File 類別

| 函式 | 說明 |
|----------|-------------|
| `File.GetDirectoryPath()` | 顯示資料夾選擇對話方塊，傳回所選路徑 |
| `File.GetFileName()` | 顯示檔案選擇對話方塊，傳回所選路徑 |
| `File.GetFileNames()` | 顯示多檔案選擇對話方塊，傳回路徑清單 |
| `File.ReadCrystalList()` | 載入晶體清單檔 (*.xml) |
| `File.ReadCrystal()` | 載入 CIF/AMC 晶體檔 |
| `File.ExportAsCIF()` | 將目前晶體匯出為 CIF |
| `File.SaveText()` | 將文字資料儲存到檔案 |

---

## Crystal 類別

| 屬性 | 型別 | 說明 |
|----------|------|-------------|
| `Crystal.Name` | string | 晶體名稱 |
| `Crystal.ChemicalFormula` | string | 化學式 |
| `Crystal.Density` | double | 密度 (g/cm³) |

---

## CrystalList 類別

| 函式 / 屬性 | 說明 |
|---------------------|-------------|
| `CrystalList.SelectedIndex` | 取得/設定所選晶體的索引 |
| `CrystalList.Add()` | 將目前晶體附加到清單 |
| `CrystalList.Replace()` | 取代所選晶體 |
| `CrystalList.Delete()` | 刪除所選晶體 |
| `CrystalList.ClearAll()` | 清空所有晶體 |
| `CrystalList.MoveUp()` | 將所選晶體上移 |
| `CrystalList.MoveDown()` | 將所選晶體下移 |

---

## Direction 類別

| 函式 | 說明 |
|----------|-------------|
| `Direction.Euler(phi, theta, psi)` | 以歐拉角設定取向（弧度） |
| `Direction.EulerInDegree(phi, theta, psi)` | 以歐拉角設定取向（度） |
| `Direction.EulerInDeg(phi, theta, psi)` | `EulerInDegree` 的別名 |
| `Direction.Rotate(ax, ay, az, angle)` | 繞任意軸旋轉（弧度） |
| `Direction.RotateInDeg(ax, ay, az, angle)` | 繞任意軸旋轉（度） |
| `Direction.RotateAroundAxis(u, v, w, angle)` | 繞晶帶軸 [uvw] 旋轉（弧度） |
| `Direction.RotateAroundAxisInDeg(u, v, w, angle)` | 繞晶帶軸 [uvw] 旋轉（度） |
| `Direction.RotateAroundPlane(h, k, l, angle)` | 繞晶面法線 (hkl) 旋轉（弧度） |
| `Direction.RotateAroundPlaneInDeg(h, k, l, angle)` | 繞晶面法線 (hkl) 旋轉（度） |
| `Direction.ProjectAlongPlane(h, k, l)` | 將晶面法線設為垂直於螢幕 |
| `Direction.ProjectAlongAxis(u, v, w)` | 將晶帶軸設為垂直於螢幕 |

---

## DifSim 類別

### 視窗控制

`DifSim.Open()` / `DifSim.Close()`

### 波源

`DifSim.Source_Xray()` / `DifSim.Source_Electron()` / `DifSim.Source_Neutron()`

### 屬性

| 屬性 | 型別 | 說明 |
|----------|------|-------------|
| `Energy` | double | 能量 (keV) |
| `Wavelength` | double | 波長 (Å) |
| `Thickness` | double | 試樣厚度 (nm) |
| `NumberOfDiffractedWaves` | int | 布洛赫波的數目 |
| `CameraLength2` | double | 相機長度 (mm) |
| `SkipRendering` | bool | 跳過算繪以進行批次處理 |

### 束模式

`Beam_Parallel()` / `Beam_PrecessionXray()` / `Beam_PrecessionElectron()` / `Beam_Convergence()`

### 計算模式

`Calc_Excitation()` / `Calc_Kinematical()` / `Calc_Dynamical()`

### 影像設定

| 屬性 / 函式 | 說明 |
|---------------------|-------------|
| `ImageResolutionInMM` | 解析度 (mm/pixel) |
| `ImageResolutionInNMinv` | 解析度 (nm⁻¹/pixel) |
| `ImageWidth` / `ImageHeight` | 影像尺寸（像素） |
| `ImageSize(w, h)` | 設定影像尺寸 |

### 偵測器

| 屬性 | 說明 |
|----------|-------------|
| `Tau` / `TauInDeg` | 偵測器傾斜角 τ（rad / deg） |
| `Phi` / `PhiInDeg` | 偵測器旋轉軸 φ（rad / deg） |
| `Foot(x, y)` | foot 位置（以像素計） |

### 輸出

| 函式 | 說明 |
|----------|-------------|
| `SaveAsPng()` | 將目前圖樣儲存為 PNG |
| `SpotInfo()` | 以 CSV 字串取得繞射點資料 |

---

## HRTEM / STEM / Potential 類別

這三個影像模擬類別共用許多成員。為避免重複，下表使用佔位符：

- **`#`** ：**HRTEM**、**STEM** 與 **Potential** 共用。將 `#` 替換為 `HRTEM`、`STEM` 或 `Potential`（例如 `STEM.Simulate()`、`Potential.AccVol`）。
- **`$`** ：僅 **HRTEM** 與 **STEM** 共用。將 `$` 替換為 `HRTEM` 或 `STEM`。
- 以明確類別名稱書寫的成員（`STEM.…` / `HRTEM.…`）僅屬於該類別。**Potential** 類別不新增自有成員；它只使用 `#` 成員。

### 視窗控制

| 函式 | 說明 |
|----------|-------------|
| `#.Open()` | 開啟 HRTEM/STEM 模擬器視窗 |
| `#.Close()` | 關閉 HRTEM/STEM 模擬器視窗 |
| `#.Simulate()` | 以目前設定執行模擬 |

### 顯微鏡 / 光學

| 屬性 / 函式 | 說明 |
|---------------------|-------------|
| `#.AccVol` | 加速電壓 (kV) |
| `$.Thickness` | 試樣厚度 (nm) |
| `$.Defocus` | 欠焦 (nm) |
| `$.Cs` | 球面像差 Cs (mm) |
| `$.Cc` | 色像差 Cc (mm) |
| `$.DeltaV` | 能量展寬 ΔV，FWHM (eV) |
| `$.Scherzer` | Scherzer 欠焦（nm，僅讀取） |
| `STEM.ConvergenceAngle` | 會聚半角 (mrad) |
| `STEM.DetectorInnerAngle` / `STEM.DetectorOuterAngle` | 環形偵測器的內/外半角 (mrad) |
| `STEM.EffectiveSourceSize` | 有效源尺寸，FWHM (pm) |
| `HRTEM.Beta` | 照明半角 β（弧度） |
| `HRTEM.ApertureSemiangle` | 物鏡光闌半角（弧度） |
| `HRTEM.ApertureShiftX` / `HRTEM.ApertureShiftY` | 物鏡光闌位移（弧度） |
| `HRTEM.OpenAperture` | 物鏡光闌開啟 (true/false) |

### 模擬屬性

| 屬性 / 函式 | 說明 |
|---------------------|-------------|
| `#.NumberOfDiffractedWaves` | 繞射（布洛赫）波的最大數目 |
| `#.ImageWidth` / `#.ImageHeight` | 影像尺寸（像素） |
| `#.ImageSize(width, height)` | 設定影像尺寸（像素） |
| `#.ImageResolution` | 影像解析度 (nm/pixel) |
| `STEM.AngularResolution` | 會聚束的角解析度 (mrad) |
| `STEM.SliceThickness` | TDS 計算的切片厚度 (nm) |
| `HRTEM.Mode_LinearImage()` | 使用線性成像（準同調）模型 |
| `HRTEM.Mode_TCC()` | 使用 TCC（透射交叉係數）模型 |

### 單幅 / 系列影像模式

| 屬性 / 函式 | 說明 |
|---------------------|-------------|
| `$.SingleImageMode()` | 切換到單幅影像模式 |
| `$.SerialImageMode(withThickness, withDefocus)` | 切換到系列影像模式 |
| `$.SerialImageThicknessStart` / `Step` / `Num` | 系列厚度：起始 (nm) / 步長 (nm) / 數目 |
| `$.SerialImageDefocusStart` / `Step` / `Num` | 系列欠焦：起始 (nm) / 步長 (nm) / 數目 |

### 影像屬性

| 屬性 / 函式 | 說明 |
|---------------------|-------------|
| `#.UnitCellVisible` | 顯示晶胞 (true/false) |
| `#.LabelVisible` | 顯示影像標籤 (true/false) |
| `#.LabelSize` | 標籤字型大小 |
| `#.ScaleBarVisible` | 顯示比例尺 (true/false) |
| `#.ScaleBarLength` | 比例尺長度 (nm) |
| `#.GaussianBlurEnabled` | 套用高斯模糊 (true/false) |
| `#.GaussianBlurFWHM` | 高斯模糊的 FWHM (pm) |
| `STEM.DisplayBoth()` | 同時顯示彈性與 TDS 分量 |
| `STEM.DisplayElastic()` | 僅顯示彈性分量 |
| `STEM.DisplayTDS()` | 僅顯示 TDS（非彈性）分量 |

### 儲存影像

| 屬性 / 函式 | 說明 |
|---------------------|-------------|
| `#.SaveImageAsPng(filename)` | 儲存為 PNG（省略 filename 時彈出對話方塊） |
| `#.SaveImageAsTif(filename)` | 儲存為 TIFF（省略 filename 時彈出對話方塊） |
| `#.SaveImageAsEmf(filename)` | 儲存為 EMF 中繼檔（省略 filename 時彈出對話方塊） |
| `#.SaveIndividually` | 在系列模式下，分別儲存每幅影像 (true/false) |
| `#.OverprintSymbols` | 在儲存的影像上疊印晶胞 / 標籤 / 比例尺 (true/false) |

---

## 全域函式

| 函式 | 說明 |
|----------|-------------|
| `Sleep(ms)` | 等待指定的毫秒數 |

---

## 另請參閱

- [20. 巨集](index.md)
- [20.2. 範例](2-examples.md)
