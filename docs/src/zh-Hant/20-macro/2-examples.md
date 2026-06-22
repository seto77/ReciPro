# 巨集範例

ReciPro 巨集的實用範例。開頭幾個是適合初學者的入門介紹；後面的範例展示批次處理操作。

> **注**：ReciPro 巨集 API 以 `ReciPro.ClassName.Member` 的形式存取。自動完成彈出視窗總是插入完整的 `ReciPro.` 前綴，因此你幾乎不需要手動輸入它。

---

## 初學者範例

### A. 基本迴圈

熟悉編輯器最簡單的方式。用 **Step by step** 執行下面的程式碼，並在偵錯面板中觀察 `i` 與 `sq` 的變化 —— 這就是 ReciPro 中「輸出」值的方式（`print()` 無法運作）。

```python
# Loop 10 times and compute the squares.
for i in range(10):
    sq = i * i
```

### B. 使用 math 模組

`math` 模組在啟動時自動匯入。可直接使用。

```python
r = 5.0
area = math.pi * r * r
circumference = 2 * math.pi * r
# Run in Step mode to see 'area' and 'circumference' in the debug panel.
```

### C. 旋轉目前的晶體

```python
# Rotate the current crystal by 30 degrees around the a-axis (x-axis).
ReciPro.Dir.RotateAroundAxisInDeg(1, 0, 0, 30)
```

### D. 對齊到晶帶軸

```python
# Align so that the [001] zone axis is normal to the screen.
ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
```

### E. 遍歷前幾個晶體

```python
# Collect the names of the first 5 crystals in the list.
names = []
for i in range(5):
    ReciPro.CrystalList.SelectedIndex = i
    names.append(ReciPro.Crystal.Name)
# Run in Step mode to see 'names' grow line by line.
```

### F. 開啟繞射圖樣

```python
# Open the diffraction simulator and display the [001] pattern
# of the first crystal in the list with 200 keV electrons.
ReciPro.CrystalList.SelectedIndex = 0
ReciPro.DifSim.Open()
ReciPro.DifSim.Source_Electron()
ReciPro.DifSim.Energy = 200
ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
```

---

## 批次處理範例

### 1. 為所有晶體儲存繞射圖樣

```python
folder = ReciPro.File.GetDirectoryPath()

ReciPro.DifSim.Open()
ReciPro.DifSim.Source_Electron()
ReciPro.DifSim.Energy = 200  # 200 keV
ReciPro.DifSim.Calc_Kinematical()
ReciPro.DifSim.SkipRendering = True

for i in range(80):  # adjust to your crystal count
    ReciPro.CrystalList.SelectedIndex = i
    name = ReciPro.Crystal.Name
    ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
    ReciPro.DifSim.SaveAsPng(folder + name + "_001.png")
    ReciPro.Dir.ProjectAlongAxis(1, 1, 0)
    ReciPro.DifSim.SaveAsPng(folder + name + "_110.png")

ReciPro.DifSim.SkipRendering = False
```

### 2. 旋轉並擷取快照

```python
folder = ReciPro.File.GetDirectoryPath()
ReciPro.DifSim.Open()
ReciPro.DifSim.Source_Electron()
ReciPro.DifSim.Energy = 200

ReciPro.Dir.ProjectAlongAxis(0, 0, 1)

for i in range(90):
    ReciPro.DifSim.SaveAsPng(folder + "rot_%03d.png" % i)
    ReciPro.Dir.RotateAroundAxisInDeg(1, 0, 0, 1)
```

### 3. 設定尤拉角

```python
# Euler angles in degrees
ReciPro.Dir.EulerInDeg(45, 30, 60)

# Same thing in radians (math is pre-imported)
ReciPro.Dir.Euler(math.pi/4, math.pi/6, math.pi/3)
```

### 4. 沿晶面與晶軸投影

```python
ReciPro.Dir.ProjectAlongPlane(1, 1, 1)  # (111) normal → screen
ReciPro.Dir.ProjectAlongAxis(1, 1, 0)   # [110] → screen
```

### 5. 批次匯入 CIF 檔案

```python
files = ReciPro.File.GetFileNames()
for f in files:
    ReciPro.File.ReadCrystal(f)
    ReciPro.CrystalList.Add()
```

### 6. 匯出繞射點資訊

```python
ReciPro.DifSim.Open()
ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
info = ReciPro.DifSim.SpotInfo()
ReciPro.File.SaveText(info, "spot_info.csv")
```

---

## 提示

- **檢視變數值**：`print()` 在此環境中無法運作（沒有主控台）。請使用 **Step by step** 執行方式，並觀察偵錯面板，它會在每一行列出區域變數。
- **`math` 已預先匯入**：`math.sqrt(x)`、`math.sin(x)`、`math.pi`、`math.radians(deg)` 無需 `import math` 即可使用。
- **批次處理加速**：在密集迴圈期間設定 `ReciPro.DifSim.SkipRendering = True`，之後再將其重設為 `False`。
- **等待算繪**：當需要讓 GUI 跟上時，呼叫 `ReciPro.Sleep(ms)` 來暫停執行。
- **自動完成**：彈出視窗顯示完整的 `ReciPro.Class.Member` 形式。輸入幾個字母，然後用 `Enter` 或 `Tab` 確認。

---

## 另請參閱

- [20. 巨集](index.md)
- [20.1. 內建函式](1-built-in-functions.md)
