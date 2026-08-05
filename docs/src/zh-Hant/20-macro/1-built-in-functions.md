# 內建函式

ReciPro 巨集中可用的類別與函式完整參考。

---

## File 類別

| 函式 | 說明 |
|----------|-------------|
| `File.GetDirectoryPath(filename)` | 顯示資料夾選擇對話方塊，傳回所選路徑；傳入 `filename` 時改為傳回包含該檔案的資料夾 |
| `File.GetFileName()` | 顯示檔案選擇對話方塊，傳回所選路徑 |
| `File.GetFileNames()` | 顯示多檔案選擇對話方塊，傳回路徑清單 |
| `File.ReadCrystalList(filename)` | 載入晶體清單檔 (*.xml)；省略 `filename` 則開啟對話方塊 |
| `File.ReadCrystal(filename)` | 載入 CIF/AMC 晶體檔；省略 `filename` 則開啟對話方塊 |
| `File.ExportAsCIF(filename)` | 將目前晶體匯出為 CIF；省略 `filename` 則開啟對話方塊 |
| `File.ReadText(filename)` | 以 UTF-8 讀取文字檔並以字串傳回；省略 `filename` 則開啟對話方塊。與 `Crystal.LoadCifText()` / `SaveText()` 搭配使用 |
| `File.SaveText(textData, filename)` | 將文字資料儲存到檔案；以 UTF-8 寫出 `textData`，省略 `filename` 則開啟儲存對話方塊 |

---

## Crystal 類別

讀取目前選取的晶體，並透過 pending 草稿建立和編輯晶體。

### 讀取

| 屬性 / 函式 | 說明 |
|---|---|
| `Crystal.Name` | 晶體名稱 |
| `Crystal.ChemicalFormula` | 化學式 |
| `Crystal.Density` | 密度（g/cm³） |
| `Crystal.GetCellInAng()` | 以 `[a, b, c, alpha, beta, gamma]`（Å、度）取得晶胞參數 |
| `Crystal.SpaceGroupName` | 空間群的 Hermann–Mauguin 符號（有多個設定的群帶 `:2`、`:H` 等設定後綴） |
| `Crystal.SpaceGroupNumber` | International Tables 空間群編號（1–230） |
| `Crystal.HasPending` | 是否有開啟的 pending 草稿 |

### 建立與編輯（草稿 → Commit）

晶體在 **pending 草稿**中組裝：先開始草稿，用 setter 填入數值，`Commit()` 會一次完成全部驗證 → 建構晶體 → 套用為目前晶體（與讀入 CIF 檔案時一樣，GUI 和所有開啟的模擬器都會更新）。`Commit()` 失敗時會把全部驗證錯誤彙總回報，不改變目前晶體，草稿也會保留，修正後即可再次 Commit。

| 函式 | 說明 |
|---|---|
| `Crystal.BeginCreate(name)` | 為新晶體開始草稿 |
| `Crystal.BeginEdit()` | 從目前晶體開始草稿（晶胞、空間群、原子、取向被繼承） |
| `Crystal.LoadCifText(cifText)` | 從 CIF 文字（.cif 檔案的內容，而非路徑）開始草稿 |
| `Crystal.SetName(name)` | 重新命名草稿 |
| `Crystal.SetCellInAng(a, b, c, alpha, beta, gamma)` | 以 **Å 和度**設定晶胞參數。每次呼叫都重新指定整個晶胞；省略的引數由空間群約束導出（立方晶只需 `a`），與約束矛盾的明示值會報錯 |
| `Crystal.SetSpaceGroup(symbol)` | 按符號設定空間群（HM 短/全符號或 Hall；空格與 `_` 被忽略）。群有多個設定時附加設定（`'Fd-3m:2'`、`'R-3c:H'`、`'P21/c:b1'`）— 有歧義的符號會報錯並列出候選 |
| `Crystal.SetSpaceGroupByNumber(itNumber, setting)` | 按 IT 編號（1–230）設定空間群；有多個設定時用 `setting`（`'1'`、`'2'`、`'H'`、`'R'`、`'b1'` 等）選擇 |
| `Crystal.AddAtom(label, element, x, y, z, occ, bIso)` | 新增非對稱單元的原子：元素符號、分數座標、佔有率（0 < occ ≤ 1，預設 1）、等向性 B（Å²，預設 0）。等效位置、Wyckoff 符號與多重度自動導出 |
| `Crystal.ClearAtoms()` | 刪除草稿中的全部原子 |
| `Crystal.Commit()` | 驗證、建構並套用草稿 |
| `Crystal.Cancel()` | 捨棄草稿 |

```python
ReciPro.Crystal.BeginCreate('NaCl')
ReciPro.Crystal.SetSpaceGroup('Fm-3m')
ReciPro.Crystal.SetCellInAng(5.6402)
ReciPro.Crystal.AddAtom('Na', 'Na', 0, 0, 0)
ReciPro.Crystal.AddAtom('Cl', 'Cl', 0.5, 0.5, 0.5)
ReciPro.Crystal.Commit()

base = ReciPro.Crystal.GetCellInAng()
for k in range(-2, 3):
    ReciPro.Crystal.BeginEdit()
    ReciPro.Crystal.SetCellInAng(base[0] * (1 + 0.01 * k))
    ReciPro.Crystal.Commit()
```

`Commit()` 成功後，下一次 `BeginEdit()` 以**更新後的**晶體為起點，因此變更會累積 — 以絕對值掃描時，請像上例那樣在迴圈前讀取基準值。要把 Commit 的晶體登錄到晶體清單，呼叫 `CrystalList.Add()`。

---

## CrystalList 類別

| 函式 / 屬性 | 說明 |
|---------------------|-------------|
| `CrystalList.SelectedIndex` | 取得/設定所選晶體的索引 |
| `CrystalList.Count` | 晶體清單中登錄的晶體數量 |
| `CrystalList.Add()` | 將目前晶體附加到清單 |
| `CrystalList.Replace()` | 取代所選晶體 |
| `CrystalList.Delete()` | 刪除所選晶體 |
| `CrystalList.ClearAll()` | 清空所有晶體 |
| `CrystalList.MoveUp()` | 將所選晶體上移 |
| `CrystalList.MoveDown()` | 將所選晶體下移 |

---

## Dir 類別

| 函式 | 說明 |
|----------|-------------|
| `Dir.Euler(phi, theta, psi)` | 以歐拉角設定取向（弧度） |
| `Dir.EulerInDegree(phi, theta, psi)` | 以歐拉角設定取向（度） |
| `Dir.EulerInDeg(phi, theta, psi)` | `EulerInDegree` 的別名 |
| `Dir.Rotate(ax, ay, az, angle)` | 繞任意軸旋轉（弧度） |
| `Dir.RotateInDeg(ax, ay, az, angle)` | 繞任意軸旋轉（度） |
| `Dir.RotateAroundAxis(u, v, w, angle)` | 繞晶帶軸 [uvw] 旋轉（弧度） |
| `Dir.RotateAroundAxisInDeg(u, v, w, angle)` | 繞晶帶軸 [uvw] 旋轉（度） |
| `Dir.RotateAroundPlane(h, k, l, angle)` | 繞晶面法線 (hkl) 旋轉（弧度） |
| `Dir.RotateAroundPlaneInDeg(h, k, l, angle)` | 繞晶面法線 (hkl) 旋轉（度） |
| `Dir.ProjectAlongPlane(h, k, l)` | 將晶面法線設為垂直於螢幕 |
| `Dir.ProjectAlongAxis(u, v, w)` | 將晶帶軸設為垂直於螢幕 |
| `Dir.GetEuler()` | 取得目前取向的 Z-X-Z 尤拉角 `[phi, theta, psi]`（弧度） |
| `Dir.GetEulerInDeg()` | 取得目前取向的 Z-X-Z 尤拉角 `[phi, theta, psi]`（度） |
| `Dir.GetRotationMatrix()` | 以 9 元素陣列 `[R11, R12, R13, R21, R22, R23, R31, R32, R33]` 取得目前旋轉矩陣（與 `SpotID.CandidateList()` 相同的約定） |
| `Dir.SetRotationMatrix(r11, r12, r13, r21, r22, r23, r31, r32, r33)` | 由旋轉矩陣的 9 個元素設定取向（套用前經過驗證與再正交化） |

尤拉角在萬向節位置（θ = 0 或 180°）並不唯一：`Euler()` 之後的 `GetEuler()` 會重現相同的姿態，但不一定是相同的數值。要精確儲存與還原取向，請使用 `Dir.GetRotationMatrix()` / `Dir.SetRotationMatrix()`。完整約定見[旋轉幾何](../4-rotation-geometry.md)。

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
| `SaveAsPng(filename)` | 將目前圖樣儲存為 PNG；省略 `filename` 則開啟對話方塊 |
| `SpotInfo()` | 以 CSV 字串取得繞射點資料 |

---

## SpotID 類別

從巨集驅動 [Spot ID v2](../11-spot-id-v2.md)：讀入影像或斑點清單 → 偵測斑點 → 標定取向 → 取回候選清單，全程無須操作視窗。`FindSpots()` 與 `Identify()` 會等處理結束後才返回，因此可以直接接續呼叫。

### 視窗操作

`SpotID.Open()` / `SpotID.Close()`

### 入射波種類

`SpotID.Source_Xray()` / `SpotID.Source_Electron()` / `SpotID.Source_Neutron()`

### 處理流程

| 函式 | 說明 |
|------|------|
| `SpotID.LoadFile(filename)` | 以 **File > Load** 的方式讀入檔案：`.csv` 視為斑點清單讀取（須先讀入影像），其他副檔名則視為繞射圖樣影像讀取（dm3、dm4、mrc、ipa、tif 等支援格式）。省略 `filename` 則開啟檔案選擇對話方塊 |
| `SpotID.FindSpots()` | 在讀入的影像中偵測斑點並擬合（等同 **Find spots** 按鈕） |
| `SpotID.Identify()` | 搜尋能解釋所偵測斑點的取向（等同 **Identify spots** 按鈕），並回傳候選數。參與檢驗的晶體為主視窗晶體清單中選取的那些 |
| `SpotID.CandidateList()` | 以 CSV 文字回傳候選取向清單 |
| `SpotID.SpotList()` | 以 CSV 文字回傳觀測斑點清單（欄位與 **File > Save** 相同）。與 `File.SaveText()` 搭配儲存後，可用 `LoadFile()` 再次讀入 |

`CandidateList()` 對每個候選回傳：晶體名稱、Z-X-Z 尤拉角（度）、旋轉矩陣的九個元素 R11–R33（晶體座標系→實驗室座標系，作用於行向量）、殘差的均方（nm⁻²），以及觀測斑點與 *hkl* 指數的對應。候選依已指派斑點數遞減、其次依殘差遞增排列。數值以 invariant culture 寫出，因此小數點一律為句點。

### 屬性

| 屬性 | 型別 | 說明 |
|------|------|------|
| `Energy` | double | 入射線能量（X 光與電子束為 keV，中子束為 meV） |
| `CameraLength` | double | 相機長度（mm） |
| `PixelSizeInMM` | double | 影像的像素尺寸（mm）。讀寫此屬性時也會把像素尺寸單位切換為 mm |
| `PixelSizeInNMinv` | double | 影像的像素尺寸（nm⁻¹）。讀寫此屬性時也會把單位切換為 nm⁻¹ |
| `MaxNumberOfSpots` | int | `FindSpots()` 可偵測的斑點數上限 |
| `NearestNeighbor` | int | 所偵測斑點之間允許的最小間隔（像素） |
| `FittingRange` | double | 峰形擬合所用、每個斑點周圍區域的半徑（像素） |
| `AcceptableError` | double | 將觀測斑點對應到候選繞射時允許的面間距相對差（%） |
| `IgnoreProhibitedReflections` | bool | 是否忽略運動學消光但可經多重繞射出現的繞射 |
| `MultiGrain` | bool | 是否搜尋多個晶粒；`False` 表示單晶 |
| `MaxNumberOfGrains` | int | `MultiGrain` 為 `True` 時搜尋的晶粒取向數上限 |
| `NumberOfDetectedSpots` | int | 已偵測的斑點數（唯讀） |
| `NumberOfCandidates` | int | 上次 `Identify()` 找到的候選數（唯讀） |

---

## StructureViewer 類別

從巨集驅動結構檢視器。3D 模型在視窗顯示時建構，因此 `SaveImage()` 和 `Export3DModel()` 會在必要時先開啟視窗。

| 函式 | 說明 |
|---|---|
| `StructureViewer.Open()` | 開啟結構檢視器視窗 |
| `StructureViewer.Close()` | 關閉結構檢視器視窗 |
| `StructureViewer.SaveImage(filename)` | 將主檢視的算繪影像儲存為 PNG（像素尺寸取視窗的 **Size (W×H)** 方塊）。省略 `filename` 則開啟儲存對話方塊 |
| `StructureViewer.Export3DModel(filename, maxSizeInMM, fixedScaleInMMperNm, includeAtoms, includeBonds, includePolyhedra, polyhedraAsEdges, polyEdgeDiaInMM, includeCellEdges, cellEdgeDiaInMM, thickenBondsToMM)` | 將顯示中的結構匯出用於 3D 列印（與 File 選單的 **Export 3D Model (3MF/STL)** 相同）。格式由副檔名決定（`.stl` = 單色 / `.3mf` = 依元素著色）。僅 `filename` 為必要，其餘預設值與對話方塊相同（最長邊 80 mm、晶胞外框 ⌀2.4 mm、鍵增粗至 ⌀1.2 mm）。傳入 `fixedScaleInMMperNm` > 0 可以同一比例製作多個模型 |

```python
ReciPro.StructureViewer.Export3DModel('D:/print/NaCl_60mm.stl', maxSizeInMM=60)
ReciPro.StructureViewer.Export3DModel('D:/print/NaCl_edges.stl', maxSizeInMM=60, polyhedraAsEdges=True)
```

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
