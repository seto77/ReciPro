# 内置函数

ReciPro 宏中可用的类与函数完整参考。

---

## File 类

| 函数 | 说明 |
|----------|-------------|
| `File.GetDirectoryPath()` | 显示文件夹选择对话框，返回所选路径 |
| `File.GetFileName()` | 显示文件选择对话框，返回所选路径 |
| `File.GetFileNames()` | 显示多文件选择对话框，返回路径列表 |
| `File.ReadCrystalList()` | 加载晶体列表文件 (*.xml) |
| `File.ReadCrystal()` | 加载 CIF/AMC 晶体文件 |
| `File.ExportAsCIF()` | 将当前晶体导出为 CIF |
| `File.SaveText()` | 将文本数据保存到文件 |

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
| `CrystalList.Add()` | 将当前晶体追加到列表 |
| `CrystalList.Replace()` | 替换所选晶体 |
| `CrystalList.Delete()` | 删除所选晶体 |
| `CrystalList.ClearAll()` | 清空所有晶体 |
| `CrystalList.MoveUp()` | 将所选晶体上移 |
| `CrystalList.MoveDown()` | 将所选晶体下移 |

---

## Direction 类

| 函数 | 说明 |
|----------|-------------|
| `Direction.Euler(phi, theta, psi)` | 用欧拉角设置取向（弧度） |
| `Direction.EulerInDegree(phi, theta, psi)` | 用欧拉角设置取向（度） |
| `Direction.EulerInDeg(phi, theta, psi)` | `EulerInDegree` 的别名 |
| `Direction.Rotate(ax, ay, az, angle)` | 绕任意轴旋转（弧度） |
| `Direction.RotateInDeg(ax, ay, az, angle)` | 绕任意轴旋转（度） |
| `Direction.RotateAroundAxis(u, v, w, angle)` | 绕晶带轴 [uvw] 旋转（弧度） |
| `Direction.RotateAroundAxisInDeg(u, v, w, angle)` | 绕晶带轴 [uvw] 旋转（度） |
| `Direction.RotateAroundPlane(h, k, l, angle)` | 绕晶面法线 (hkl) 旋转（弧度） |
| `Direction.RotateAroundPlaneInDeg(h, k, l, angle)` | 绕晶面法线 (hkl) 旋转（度） |
| `Direction.ProjectAlongPlane(h, k, l)` | 将晶面法线设为垂直于屏幕 |
| `Direction.ProjectAlongAxis(u, v, w)` | 将晶带轴设为垂直于屏幕 |

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
| `SaveAsPng()` | 将当前图样保存为 PNG |
| `SpotInfo()` | 以 CSV 字符串获取衍射点数据 |

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
