# ReciPro 手册

## 简要介绍
* ReciPro 是一款采用 MIT 许可证的免费软件，提供多种晶体学计算和电子显微镜模拟功能。
* 自 2020 年 3 月在 GitHub 发布以来，ReciPro 累计下载量已超过 27,000 次，被众多晶体学家和电子显微镜研究者使用。

## 按目标查找

| 目标 | 从这里开始 | 主要后续步骤 |
|------|------------|-----------------|
| 加载晶体并设置其取向 | [主窗口](0-main-window.md) | [旋转几何](4-rotation-geometry.md)、[附录 A1. 坐标系](appendix/a1-coordinate-system/1-orientation.md) |
| 以 3D 方式查看晶体结构 | [结构查看器](5-structure-viewer.md) | [对称性信息](2-symmetry-information.md) |
| 计算 SAED / XRD / PED / CBED 图样 | [衍射模拟器](7-diffraction-simulator/index.md) | [SAED](7-diffraction-simulator/1-saed-simulation.md)、[X 射线衍射](7-diffraction-simulator/4-x-ray-neutron-diffraction.md)、[PED](7-diffraction-simulator/2-ped-simulation.md)、[CBED](7-diffraction-simulator/3-cbed-simulation.md) |
| 计算 HRTEM / STEM 图像 | [HRTEM/STEM 模拟器](9-hrtem-stem-simulator/index.md) | [HRTEM](9-hrtem-stem-simulator/1-hrtem-simulation.md)、[STEM](9-hrtem-stem-simulator/2-stem-simulation.md) |
| 模拟 EBSD 图样 | [EBSD 模拟](12-ebsd-simulation.md) | [电子轨迹](8-electron-trajectory.md)、[附录 A3. EBSD 计算](appendix/a3-bloch-wave/ebsd.md) |
| 标定实验衍射斑点 | [Spot ID v1](10-spot-id.md)、[Spot ID v2](11-spot-id-v2.md) | [衍射模拟器](7-diffraction-simulator/index.md) |
| 理解动力学衍射方程 | [附录 A3. 布洛赫波法](appendix/a3-bloch-wave/index.md) | [动力学计算](appendix/a3-bloch-wave/calculation.md)、[CBED](appendix/a3-bloch-wave/cbed.md)、[STEM](appendix/a3-bloch-wave/stem.md)、[EBSD](appendix/a3-bloch-wave/ebsd.md) |

## 功能
* **Full GUI** : 所有操作均通过图形界面完成。大多数文件输入/输出支持拖放。
* **晶体列表** : 同时管理多个晶体；无需为每个晶体单独打开窗口。
* **空间群数据库** : 内置数据库涵盖 International Tables Volume A 的 230 个空间群，以及 530 个 Hall 符号，包含对称元素、威科夫位置和消光规则。对称元素和一般位置可绘制为 *International Tables* Vol. A 风格的示意图（参见 [2. 对称性信息](2-symmetry-information.md)）。
* **原子信息** : 包含元素 H (1) – Cf (98) 的散射因子（X 射线、电子、中子）、特征 X 射线能量、同位素比等。
* **灵活的晶体旋转** : 可通过晶带轴/晶面指数或鼠标拖动来设置取向。三方/六方晶系支持米勒-布拉维（4 指数 *hkil*）记法。旋转状态会在所有模拟窗口之间同步。
* **衍射模拟** : 运动学和动力学（布洛赫波法）电子衍射、X 射线衍射（包括进动相机和后劳厄相机）、进动电子衍射 (PED) 以及会聚束电子衍射 (CBED)。TEM 样品台模拟将衍射图样与样品台倾斜角联系起来。
* **HRTEM / STEM 模拟** : 采用部分相干模型的高分辨 TEM 图像模拟；含热漫散射的 STEM。
* **EBSD 与电子轨迹** : EBSD 图样模拟以及电子轨迹的蒙特卡罗模拟（参见 [8. 电子轨迹](8-electron-trajectory.md)）。
* **斑点标定** : 从实验图像中自动检测、拟合并标定衍射斑点（Spot ID v1/v2）。
* **宏** : 用于自动化操作的 Python 语法宏（参见 [20. 宏](20-macro/index.md)）。
* **浅色 / 深色主题** : 界面支持可选的浅色或深色配色模式。

## 系统要求
| 项目 | 最低要求 | 推荐配置 |
|------|---------|-------------|
| OS | 安装有 [.NET Desktop Runtime 10.0](https://dotnet.microsoft.com/download/dotnet/10.0) 的 Windows（支持 Windows on ARM64） | Windows 11 |
| GPU | OpenGL 1.3 | 支持 OpenGL 4.3 的独立 GPU |
| 内存 | - | 16 GB 或更多 |
| CPU | - | 8 核以上（用于动力学计算） |

**Windows on ARM（原生，实验性）** : 实验性的原生 ARM64 便携包（`ReciPro-v.X_arm64.zip`，自包含 — 无需安装 .NET Runtime）可在[发布页面](https://github.com/seto77/ReciPro/releases/latest)获取。常规的 x64 包也可在 ARM64 Windows 上通过内置仿真运行。安装说明与限制请参见[故障排除](troubleshooting.md#windows-on-arm)。

**macOS（非官方）** : ReciPro 官方仅支持 Windows，但据报告，**win-x64** 便携 ZIP 包可在 macOS（Apple Silicon）上通过 Sikarugir Wine 封装器结合 Mesa3D OpenGL 驱动运行。一份用户发布的安装指南位于 <https://github.com/Ryo-fkushima/ReciPro_macOS_memo>。请注意，此途径并非官方支持，且某些符号（Å、上标、箭头）可能显示不正确。ARM64 ZIP **无法**在 macOS + Wine 上运行，而当前的 x64 + Rosetta 2 途径预计自 macOS 28（2027 年秋季）起将停止工作 — 详情请参见[故障排除](troubleshooting.md#mac-linux)。

## 如何使用本手册

此 GitHub Pages 手册是当前的权威来源。可使用左侧导航按章节浏览，或使用页眉中的搜索来查找某个功能名称或 UI 标签。旧的 PDF 手册仅作存档参考保留。

* **存档 PDF（英文）：** <https://raw.githubusercontent.com/seto77/ReciPro/master/ReciPro/doc/ReciProManual(en).pdf>
* **存档 PDF（日文）：** <https://raw.githubusercontent.com/seto77/ReciPro/master/ReciPro/doc/ReciProManual(ja).pdf>

## 快速开始
1. 从 [Releases](https://github.com/seto77/ReciPro/releases/latest) 下载并安装。
2. 从内置列表（约 80 个晶体）中选择一个晶体。你也可以导入 CIF 文件或使用 [CSManager](https://github.com/seto77/CSManager)。
3. 从右侧面板调用各项功能：结构查看器、极射赤平投影、衍射模拟器、HRTEM 模拟等。
4. 通过鼠标拖动或输入晶带轴/晶面指数来旋转晶体。

## 引用
> Y. Seto, "ReciPro: free and open-source multipurpose crystallographic software integrating a crystal operation interface and diffraction simulators," *J. Appl. Cryst.* **55**, 397–410 (2022). <https://doi.org/10.1107/S1600576722000139>

## 许可证
ReciPro 以 [MIT 许可证](https://github.com/seto77/ReciPro/blob/master/LICENSE.md) 分发。
