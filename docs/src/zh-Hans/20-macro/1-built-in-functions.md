# 内置函数

ReciPro 宏中可用的类与函数完整参考。

---

## File 类

| 函数 | 说明 |
|----------|-------------|
| `File.GetDirectoryPath(filename)` | 显示文件夹选择对话框，返回所选路径；传入 `filename` 时改为返回包含该文件的文件夹 |
| `File.GetFileName()` | 显示文件选择对话框，返回所选路径 |
| `File.GetFileNames()` | 显示多文件选择对话框，返回路径列表 |
| `File.ReadCrystalList(filename)` | 加载晶体列表文件 (*.xml)；省略 `filename` 则打开对话框 |
| `File.ReadCrystal(filename)` | 加载 CIF/AMC 晶体文件；省略 `filename` 则打开对话框 |
| `File.ExportAsCIF(filename)` | 将当前晶体导出为 CIF；省略 `filename` 则打开对话框 |
| `File.SaveText(textData, filename)` | 将文本数据保存到文件；以 UTF-8 写出 `textData`，省略 `filename` 则打开保存对话框 |

---

## Crystal 类

| 属性 | 类型 | 说明 |
|----------|------|-------------|
| `Crystal.Name` | string | 晶体名称 |
| `Crystal.ChemicalFormula` | string | 化学式 |
| `Crystal.Density` | double | 密度 (g/cm³) |

---

## CrystalList 类

| 函数 / 属性 | 说明 |
|---------------------|-------------|
| `CrystalList.SelectedIndex` | 获取/设置所选晶体的索引 |
| `CrystalList.Count` | 晶体列表中登记的晶体数量 |
| `CrystalList.Add()` | 将当前晶体追加到列表 |
| `CrystalList.Replace()` | 替换所选晶体 |
| `CrystalList.Delete()` | 删除所选晶体 |
| `CrystalList.ClearAll()` | 清空所有晶体 |
| `CrystalList.MoveUp()` | 将所选晶体上移 |
| `CrystalList.MoveDown()` | 将所选晶体下移 |

---

## Dir 类

| 函数 | 说明 |
|----------|-------------|
| `Dir.Euler(phi, theta, psi)` | 用欧拉角设置取向（弧度） |
| `Dir.EulerInDegree(phi, theta, psi)` | 用欧拉角设置取向（度） |
| `Dir.EulerInDeg(phi, theta, psi)` | `EulerInDegree` 的别名 |
| `Dir.Rotate(ax, ay, az, angle)` | 绕任意轴旋转（弧度） |
| `Dir.RotateInDeg(ax, ay, az, angle)` | 绕任意轴旋转（度） |
| `Dir.RotateAroundAxis(u, v, w, angle)` | 绕晶带轴 [uvw] 旋转（弧度） |
| `Dir.RotateAroundAxisInDeg(u, v, w, angle)` | 绕晶带轴 [uvw] 旋转（度） |
| `Dir.RotateAroundPlane(h, k, l, angle)` | 绕晶面法线 (hkl) 旋转（弧度） |
| `Dir.RotateAroundPlaneInDeg(h, k, l, angle)` | 绕晶面法线 (hkl) 旋转（度） |
| `Dir.ProjectAlongPlane(h, k, l)` | 将晶面法线设为垂直于屏幕 |
| `Dir.ProjectAlongAxis(u, v, w)` | 将晶带轴设为垂直于屏幕 |
| `Dir.GetEuler()` | 获取当前取向的 Z-X-Z 欧拉角 `[phi, theta, psi]`（弧度） |
| `Dir.GetEulerInDeg()` | 获取当前取向的 Z-X-Z 欧拉角 `[phi, theta, psi]`（度） |
| `Dir.GetRotationMatrix()` | 以 9 元素数组 `[R11, R12, R13, R21, R22, R23, R31, R32, R33]` 获取当前旋转矩阵（与 `SpotID.CandidateList()` 相同的约定） |
| `Dir.SetRotationMatrix(r11, r12, r13, r21, r22, r23, r31, r32, r33)` | 由旋转矩阵的 9 个元素设置取向（应用前经过校验与再正交化） |

欧拉角在万向节位置（θ = 0 或 180°）不唯一：`Euler()` 之后的 `GetEuler()` 会再现相同的姿态，但不一定是相同的数值。要精确保存和恢复取向，请使用 `Dir.GetRotationMatrix()` / `Dir.SetRotationMatrix()`。完整约定见[旋转几何](../4-rotation-geometry.md)。

---

## DifSim 类

### 窗口控制

`DifSim.Open()` / `DifSim.Close()`

### 波源

`DifSim.Source_Xray()` / `DifSim.Source_Electron()` / `DifSim.Source_Neutron()`

### 属性

| 属性 | 类型 | 说明 |
|----------|------|-------------|
| `Energy` | double | 能量 (keV) |
| `Wavelength` | double | 波长 (Å) |
| `Thickness` | double | 样品厚度 (nm) |
| `NumberOfDiffractedWaves` | int | 布洛赫波的数目 |
| `CameraLength2` | double | 相机长度 (mm) |
| `SkipRendering` | bool | 跳过渲染以进行批处理 |

### 束模式

`Beam_Parallel()` / `Beam_PrecessionXray()` / `Beam_PrecessionElectron()` / `Beam_Convergence()`

### 计算模式

`Calc_Excitation()` / `Calc_Kinematical()` / `Calc_Dynamical()`

### 图像设置

| 属性 / 函数 | 说明 |
|---------------------|-------------|
| `ImageResolutionInMM` | 分辨率 (mm/pixel) |
| `ImageResolutionInNMinv` | 分辨率 (nm⁻¹/pixel) |
| `ImageWidth` / `ImageHeight` | 图像尺寸（像素） |
| `ImageSize(w, h)` | 设置图像尺寸 |

### 探测器

| 属性 | 说明 |
|----------|-------------|
| `Tau` / `TauInDeg` | 探测器倾斜角 τ（rad / deg） |
| `Phi` / `PhiInDeg` | 探测器旋转轴 φ（rad / deg） |
| `Foot(x, y)` | foot 位置（以像素计） |

### 输出

| 函数 | 说明 |
|----------|-------------|
| `SaveAsPng(filename)` | 将当前图样保存为 PNG；省略 `filename` 则打开对话框 |
| `SpotInfo()` | 以 CSV 字符串获取衍射点数据 |

---

## SpotID 类

从宏驱动 [Spot ID v2](../11-spot-id-v2.md)：读入图像或斑点列表 → 检测斑点 → 标定取向 → 取回候选列表，全程无需操作窗口。`FindSpots()` 与 `Identify()` 会等处理结束后才返回，因此可以直接连续调用。

### 窗口操作

`SpotID.Open()` / `SpotID.Close()`

### 入射波种类

`SpotID.Source_Xray()` / `SpotID.Source_Electron()` / `SpotID.Source_Neutron()`

### 处理流程

| 函数 | 说明 |
|------|------|
| `SpotID.LoadFile(filename)` | 按 **File > Load** 的方式读入文件：`.csv` 作为斑点列表读取（须先读入图像），其他扩展名作为衍射花样图像读取（dm3、dm4、mrc、ipa、tif 等支持的格式）。省略 `filename` 则打开文件选择对话框 |
| `SpotID.FindSpots()` | 在读入的图像中检测斑点并拟合（等同 **Find spots** 按钮） |
| `SpotID.Identify()` | 搜索能解释所检测斑点的取向（等同 **Identify spots** 按钮），并返回候选数。参与检验的晶体为主窗口晶体列表中选中的那些 |
| `SpotID.CandidateList()` | 以 CSV 文本返回候选取向列表 |
| `SpotID.SpotList()` | 以 CSV 文本返回观测斑点列表（列与 **File > Save** 相同）。与 `File.SaveText()` 配合保存后，可用 `LoadFile()` 再次读入 |

`CandidateList()` 对每个候选返回：晶体名、Z-X-Z 欧拉角（度）、旋转矩阵的九个元素 R11–R33（晶体坐标系→实验室坐标系，作用于列向量）、残差的均方（nm⁻²），以及观测斑点与 *hkl* 指数的对应。候选按已指派斑点数降序、其次按残差升序排列。数值以 invariant culture 写出，因此小数点始终为句点。

### 属性

| 属性 | 类型 | 说明 |
|------|------|------|
| `Energy` | double | 入射线能量（X 射线与电子束为 keV，中子束为 meV） |
| `CameraLength` | double | 相机长度（mm） |
| `PixelSizeInMM` | double | 图像的像素尺寸（mm）。读写该属性同时会把像素尺寸单位切换为 mm |
| `PixelSizeInNMinv` | double | 图像的像素尺寸（nm⁻¹）。读写该属性同时会把单位切换为 nm⁻¹ |
| `MaxNumberOfSpots` | int | `FindSpots()` 可检测的斑点数上限 |
| `NearestNeighbor` | int | 所检测斑点之间允许的最小间隔（像素） |
| `FittingRange` | double | 峰拟合所用的、每个斑点周围区域的半径（像素） |
| `AcceptableError` | double | 将观测斑点与候选衍射对应时允许的面间距相对偏差（%） |
| `IgnoreProhibitedReflections` | bool | 是否忽略运动学消光但可经多重衍射出现的衍射 |
| `MultiGrain` | bool | 是否搜索多个晶粒；`False` 表示单晶 |
| `MaxNumberOfGrains` | int | `MultiGrain` 为 `True` 时搜索的晶粒取向数上限 |
| `NumberOfDetectedSpots` | int | 已检测的斑点数（只读） |
| `NumberOfCandidates` | int | 上次 `Identify()` 找到的候选数（只读） |

---

## HRTEM / STEM / Potential 类

这三个图像模拟类共享许多成员。为避免重复，下表使用占位符：

- **`#`** ：**HRTEM**、**STEM** 与 **Potential** 共用。将 `#` 替换为 `HRTEM`、`STEM` 或 `Potential`（例如 `STEM.Simulate()`、`Potential.AccVol`）。
- **`$`** ：仅 **HRTEM** 与 **STEM** 共用。将 `$` 替换为 `HRTEM` 或 `STEM`。
- 以显式类名书写的成员（`STEM.…` / `HRTEM.…`）仅属于该类。**Potential** 类不添加自有成员；它只使用 `#` 成员。

### 窗口控制

| 函数 | 说明 |
|----------|-------------|
| `#.Open()` | 打开图像模拟器窗口 |
| `#.Close()` | 关闭图像模拟器窗口 |
| `#.Simulate()` | 以当前设置运行模拟 |

### 显微镜 / 光学

| 属性 / 函数 | 说明 |
|---------------------|-------------|
| `#.AccVol` | 加速电压 (kV) |
| `$.Thickness` | 样品厚度 (nm) |
| `$.Defocus` | 欠焦 (nm) |
| `$.Cs` | 球差 Cs (mm) |
| `$.Cc` | 色差 Cc (mm) |
| `$.DeltaV` | 能量展宽 ΔV，FWHM (eV) |
| `$.Scherzer` | Scherzer 欠焦（nm，仅读取） |
| `STEM.ConvergenceAngle` | 会聚半角 (mrad) |
| `STEM.DetectorInnerAngle` / `STEM.DetectorOuterAngle` | 环形探测器的内/外半角 (mrad) |
| `STEM.EffectiveSourceSize` | 有效源尺寸，FWHM (pm) |
| `HRTEM.Beta` | 照明半角 β（弧度） |
| `HRTEM.ApertureSemiangle` | 物镜光阑半角（弧度） |
| `HRTEM.ApertureShiftX` / `HRTEM.ApertureShiftY` | 物镜光阑位移（弧度） |
| `HRTEM.OpenAperture` | 物镜光阑开启 (true/false) |

### 模拟属性

| 属性 / 函数 | 说明 |
|---------------------|-------------|
| `#.NumberOfDiffractedWaves` | 衍射（布洛赫）波的最大数目 |
| `#.ImageWidth` / `#.ImageHeight` | 图像尺寸（像素） |
| `#.ImageSize(width, height)` | 设置图像尺寸（像素） |
| `#.ImageResolution` | 图像分辨率 (nm/pixel) |
| `STEM.AngularResolution` | 会聚束的角分辨率 (mrad) |
| `STEM.SliceThickness` | TDS 计算的切片厚度 (nm) |
| `HRTEM.Mode_LinearImage()` | 使用线性成像（准相干）模型 |
| `HRTEM.Mode_TCC()` | 使用 TCC（透射交叉系数）模型 |

### 单幅 / 系列图像模式

| 属性 / 函数 | 说明 |
|---------------------|-------------|
| `$.SingleImageMode()` | 切换到单幅图像模式 |
| `$.SerialImageMode(withThickness, withDefocus)` | 切换到系列图像模式 |
| `$.SerialImageThicknessStart` / `Step` / `Num` | 系列厚度：起始 (nm) / 步长 (nm) / 数目 |
| `$.SerialImageDefocusStart` / `Step` / `Num` | 系列欠焦：起始 (nm) / 步长 (nm) / 数目 |

### 图像属性

| 属性 / 函数 | 说明 |
|---------------------|-------------|
| `#.UnitCellVisible` | 显示晶胞 (true/false) |
| `#.LabelVisible` | 显示图像标签 (true/false) |
| `#.LabelSize` | 标签字体大小 |
| `#.ScaleBarVisible` | 显示比例尺 (true/false) |
| `#.ScaleBarLength` | 比例尺长度 (nm) |
| `#.GaussianBlurEnabled` | 应用高斯模糊 (true/false) |
| `#.GaussianBlurFWHM` | 高斯模糊的 FWHM (pm) |
| `STEM.DisplayBoth()` | 同时显示弹性与 TDS 分量 |
| `STEM.DisplayElastic()` | 仅显示弹性分量 |
| `STEM.DisplayTDS()` | 仅显示 TDS（非弹性）分量 |

### 保存图像

| 属性 / 函数 | 说明 |
|---------------------|-------------|
| `#.SaveImageAsPng(filename)` | 保存为 PNG（省略 filename 时弹出对话框） |
| `#.SaveImageAsTif(filename)` | 保存为 TIFF（省略 filename 时弹出对话框） |
| `#.SaveImageAsEmf(filename)` | 保存为 EMF 元文件（省略 filename 时弹出对话框） |
| `#.SaveIndividually` | 在系列模式下，分别保存每幅图像 (true/false) |
| `#.OverprintSymbols` | 在保存的图像上叠印晶胞 / 标签 / 比例尺 (true/false) |

---

## 全局函数

| 函数 | 说明 |
|----------|-------------|
| `Sleep(ms)` | 等待指定的毫秒数 |

---

## 另见

- [20. 宏](index.md)
- [20.2. 示例](2-examples.md)
