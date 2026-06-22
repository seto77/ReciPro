# 宏示例

ReciPro 宏的实用示例。开头几个是面向初学者的入门介绍；后面的示例展示批处理操作。

> **说明**: ReciPro 宏 API 以 `ReciPro.ClassName.Member` 的形式访问。自动补全弹出框总是插入完整的 `ReciPro.` 前缀，因此你几乎不需要手动输入它。

---

## 初学者示例

### A. 基本循环

熟悉编辑器最简单的方式。用 **Step by step** 运行下面的代码，并在调试面板中观察 `i` 和 `sq` 的变化 —— 这就是 ReciPro 中“输出”值的方式（`print()` 不起作用）。

```python
# Loop 10 times and compute the squares.
for i in range(10):
    sq = i * i
```

### B. 使用 math 模块

`math` 模块在启动时自动导入。可直接使用。

```python
r = 5.0
area = math.pi * r * r
circumference = 2 * math.pi * r
# Run in Step mode to see 'area' and 'circumference' in the debug panel.
```

### C. 旋转当前晶体

```python
# Rotate the current crystal by 30 degrees around the a-axis (x-axis).
ReciPro.Dir.RotateAroundAxisInDeg(1, 0, 0, 30)
```

### D. 对齐到晶带轴

```python
# Align so that the [001] zone axis is normal to the screen.
ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
```

### E. 遍历前几个晶体

```python
# Collect the names of the first 5 crystals in the list.
names = []
for i in range(5):
    ReciPro.CrystalList.SelectedIndex = i
    names.append(ReciPro.Crystal.Name)
# Run in Step mode to see 'names' grow line by line.
```

### F. 打开衍射图样

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

## 批处理示例

### 1. 为所有晶体保存衍射图样

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

### 2. 旋转并捕获快照

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

### 3. 设置欧拉角

```python
# Euler angles in degrees
ReciPro.Dir.EulerInDeg(45, 30, 60)

# Same thing in radians (math is pre-imported)
ReciPro.Dir.Euler(math.pi/4, math.pi/6, math.pi/3)
```

### 4. 沿晶面和晶轴投影

```python
ReciPro.Dir.ProjectAlongPlane(1, 1, 1)  # (111) normal → screen
ReciPro.Dir.ProjectAlongAxis(1, 1, 0)   # [110] → screen
```

### 5. 批量导入 CIF 文件

```python
files = ReciPro.File.GetFileNames()
for f in files:
    ReciPro.File.ReadCrystal(f)
    ReciPro.CrystalList.Add()
```

### 6. 导出衍射点信息

```python
ReciPro.DifSim.Open()
ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
info = ReciPro.DifSim.SpotInfo()
ReciPro.File.SaveText(info, "spot_info.csv")
```

---

## 提示

- **查看变量值**：`print()` 在此环境中不起作用（没有控制台）。请使用 **Step by step** 执行方式，并观察调试面板，它会在每一行列出局部变量。
- **`math` 已预先导入**：`math.sqrt(x)`、`math.sin(x)`、`math.pi`、`math.radians(deg)` 无需 `import math` 即可使用。
- **批处理加速**：在密集循环期间设置 `ReciPro.DifSim.SkipRendering = True`，之后再将其重置为 `False`。
- **等待渲染**：当需要让 GUI 跟上时，调用 `ReciPro.Sleep(ms)` 来暂停执行。
- **自动补全**：弹出框显示完整的 `ReciPro.Class.Member` 形式。输入几个字母，然后用 `Enter` 或 `Tab` 确认。

---

## 另请参阅

- [20. 宏](index.md)
- [20.1. 内置函数](1-built-in-functions.md)
