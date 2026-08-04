# ReciPro

[![Documentation](https://img.shields.io/badge/%F0%9F%93%96_Documentation-blue)](https://seto77.github.io/ReciPro/zh-Hans/)
[![Latest Release](https://img.shields.io/github/v/release/seto77/ReciPro?logo=github)](https://github.com/seto77/ReciPro/releases/latest)
[![Total downloads](https://img.shields.io/github/downloads/seto77/ReciPro/total?logo=github&label=GitHub%20downloads)](https://github.com/seto77/ReciPro/releases)
[![GitHub Stars](https://img.shields.io/github/stars/seto77/ReciPro?style=social)](https://github.com/seto77/ReciPro/stargazers)
[![GitHub Forks](https://img.shields.io/github/forks/seto77/ReciPro?style=social)](https://github.com/seto77/ReciPro/forks)
[![License: MIT](https://img.shields.io/badge/License-MIT-green)](https://github.com/seto77/ReciPro/blob/master/LICENSE.md)

<!-- 260804Cl: ../../README.md（英文版）的译文。英文版更新时请同步更新本文件。 -->
[English](../../README.md) | [日本語](README.ja.md) | [Deutsch](README.de.md) | [Français](README.fr.md) | [Español](README.es.md) | [Italiano](README.it.md) | [Русский](README.ru.md) | **简体中文** | [繁體中文](README.zh-Hant.md) | [한국어](README.ko.md) | [Português](README.pt.md)

*ReciPro* 是一款免费开源、基于图形界面的多用途晶体学软件，可无缝调用晶体数据库检索、晶体结构与测角台设置的可视化、衍射图样与高分辨显微图像的模拟以及衍射数据分析等功能。这些功能通过友好的图形界面相互关联，计算结果几乎可以实时同步显示。*ReciPro* 可为使用 X 射线、电子和中子衍射晶体学以及透射电子显微镜的广大晶体学工作者（包括初学者）提供帮助。

*ReciPro* 自 2002 年起持续开发，并于 2020 年 3 月起在 GitHub 上公开。它在 GitHub 上的下载量已超过 27,000 次，被高校和企业十余个实验室的数百位用户使用。

***[请查阅手册了解使用方法！](https://seto77.github.io/ReciPro/zh-Hans/)***

[实时执行的各种模拟（示例：MgAl2O4）](https://github.com/user-attachments/assets/6b0234dd-f2d6-49db-b146-bb74cf6021b6)

## 作者

*ReciPro* 由 [Seto Y.](https://yseto.net/en/home-e) 与 [Ohtsuka M.](https://researchmap.jp/7000002999?lang=en) 开发。相关功能与算法在[论文](https://github.com/seto77/ReciPro/blob/master/docs/ReciProSetoOhtsuka2022.pdf)中作了介绍。

## 引用

若在学术工作中使用 *ReciPro*，请使用 GitHub 仓库页面上显示的 **Cite this repository** 链接。引用元数据由 `CITATION.cff` 提供，推荐引用下列文章：

  * [Seto, Y. & Ohtsuka, M. (2022). *J. Appl. Cryst.* **55**, 397-410, doi: 10.1107/S1600576722000139.](https://doi.org/10.1107/S1600576722000139)

在适当情况下，也可以引用软件仓库本身：

  * 仓库：https://github.com/seto77/ReciPro
  * 版本发布：https://github.com/seto77/ReciPro/releases/latest

***

## 安装

* 下载 [*ReciPro-setup.msi*](https://github.com/seto77/ReciPro/releases/latest/download/ReciPro-setup.msi)（最新版本的直接链接）并运行。也可在[发布页面](https://github.com/seto77/ReciPro/releases/latest)找到。（v.4.939 之前，安装程序名为 *ReciProSetup.msi*。）
* *ReciPro* 需要在安装了 ***.Net Desktop Runtime 10.0***（不是 ***.Net Runtime 10.0***）的 Windows 系统上运行，运行时可从[此处](https://dotnet.microsoft.com/download/dotnet/10.0)安装。
* 如果无法运行安装程序（例如在权限受限的电脑上），发布页面还提供 **便携版 ZIP** 包（*ReciPro-v.X.XXX.zip*）：自包含，无需安装、无需 .NET 运行时，解压即可运行。
* *ReciPro* 以 **MIT 许可证**发布（任何人都可自由使用、修改和再分发）。
* 有关代码签名状态与安装程序验证方法，请参阅[代码签名策略](../../CODE_SIGNING.md)。
* 有关随附或引用的第三方组件与数据，请参阅[第三方声明](../../THIRD-PARTY-NOTICES.md)。

### macOS（非官方）

* *ReciPro* 官方仅支持 Windows，但有报告称，将**便携版 ZIP** 包与 **Sikarugir** Wine 封装以及 **Mesa3D** OpenGL 驱动组合使用，可在 macOS（Apple Silicon）上运行，无需 Windows 许可证或虚拟机。
* 请参阅 Ryo Fukushima（JAMSTEC）发布的分步指南：https://github.com/Ryo-fkushima/ReciPro_macOS_memo
* 该配置未获官方支持，也未经过充分验证。已知的限制是部分符号（Å、上标、箭头）可能显示不正确。
* 在 Wine 前缀中安装字形覆盖范围较广的字体（**DejaVu Sans/Serif**，日语界面还需 **Noto Sans CJK JP**）即可解决乱码问题——ReciPro 会检测 Wine 环境并自动切换到这些字体。详情请参阅[疑难解答](https://seto77.github.io/ReciPro/zh-Hans/troubleshooting/)。

### 关于 Windows 安全警告

* 请仅从官方 GitHub Releases 页面下载 *ReciPro*：https://github.com/seto77/ReciPro/releases/latest
* 在部分 Windows 系统上，Microsoft Defender SmartScreen 或 Smart App Control 可能在运行安装程序前显示警告。对于新构建或传播范围有限的科研软件，这种情况时有发生，警告本身并不一定意味着安装程序是恶意的。
* 如果希望自行验证下载的安装程序，可使用 VirusTotal 等多引擎扫描服务进行检测。

## 代码签名策略

[<img src="https://signpath.org/assets/favicon-50x50.png" alt="SignPath" height="20">](https://about.signpath.io/) Windows 平台的免费代码签名由 [SignPath.io](https://about.signpath.io/) 提供，证书由 [SignPath Foundation](https://signpath.org/) 颁发。

自 v.4.942 起，发布产物（*ReciPro-setup.msi* 安装程序与便携版 *ReciPro.exe*）会在自动化发布流程中使用 Windows Authenticode 签名，且每次签名请求都由维护者在发布前审核并手动批准。完整策略（包括签名范围、如何验证安装程序以及如何报告可疑产物）请参阅 [CODE_SIGNING.md](../../CODE_SIGNING.md)。

## 隐私

*ReciPro* 是一款本地桌面应用程序。它**不会**收集、存储或传输任何个人信息或使用数据，也不包含遥测或分析功能。安装后可完全离线运行。

*ReciPro* 建立的唯一网络连接是用户主动发起的可选下载，其中没有任何操作会上传您的数据：

* **检查更新**（菜单命令）：将已安装版本与最新的 GitHub 发布版本进行比较，若您选择更新，则从官方 [GitHub Releases](https://github.com/seto77/ReciPro/releases/latest) 页面下载新的安装程序。
* **COD 数据库**（Crystallography Open Database）：首次使用时从作者的 GitHub 镜像下载（约 880 MB），之后即可离线使用。
* **Intel MKL 库**（可选加速）：仅在启用 *Use MKL* 选项时，从 [nuget.org](https://www.nuget.org/) 下载（约 55 MB），用于加速动力学衍射计算。

随附的 AMCSD 数据库以及所有核心功能均可完全离线工作。

## 手册
  * 在线手册（英文 / 日文）：https://seto77.github.io/ReciPro/zh-Hans/
  * 日文版：https://yseto.net/soft/recipro
***

## 主要功能

### 晶体数据库

* **AMCSD**（American Mineralogist Crystal Structure Database）：内置 21,000 余种晶体结构，安装后即可使用。
  * 数据库经过高度压缩（约 5 MB）并包含在安装文件中，因此在离线环境下也可使用。
  * 可按名称、化学组成、点阵参数、密度、对称性以及所含元素检索晶体。
  * 参考文献：[Downs & Hall-Wallace, 2003, *American Mineralogist* **88**, 247-250](https://www.geo.arizona.edu/xtal/group/pdf/am88_247.pdf)
* **COD**（Crystallography Open Database）：另可使用约 525,000 种晶体结构，包括有机晶体。
  * 首次使用时自动下载（约 880 MB），之后可离线使用。
  * 参考文献：[Gražulis et al., 2009, *J. Appl. Cryst.* **42**, 726-729](https://doi.org/10.1107/S0021889809016690)；[Gražulis et al., 2012, *Nucleic Acids Res.* **40**, D420-D427](https://doi.org/10.1093/nar/gkr900)
* 支持 CIF 与 AMC 格式文件的导入/导出。

### 晶体学计算

* 支持 530 种空间群表示：230 种标准 ITA 设置 + 300 种非标准轴设置。
  * 所有空间群的一般条件（消光规则）、Wyckoff 位置与多重性。
  * 面与面、轴与轴之间周期性和/或夹角的几何计算。
  * 生成等效原子位置。
  * 可在非标准轴设置之间（例如 *Pbnm* 到 *Pnma*）以及原点平移之间轻松转换。

### 原子性质

* <sup>1</sup>H 至 <sup>98</sup>Cf 特征 X 射线的波长/能量。
* X 射线、电子与中子的原子散射因子。

### 结构查看器

* 基于 OpenGL（GLSL）架构的三维晶体结构可视化。
  * 可绘制原子、化学键、配位多面体、晶胞、晶面、边界面以及图例标签。
  * 即使是包含数万个原子的复杂晶体结构，也能实时流畅绘制。
  * 默认的原子绘制颜色与大小与 VESTA 兼容。
  * 绘制范围可按晶胞倍数指定，也可通过晶面指数与到中心的距离指定。
  * 通过为边界面着色，可表现任意的晶体形态。
  * 可显示任意晶面，有助于初学者理解衍射现象中晶面的概念。
  * 可用鼠标自由控制旋转、平移与缩放。
  * 点击原子可显示与相邻原子之间的距离与键角。
  * 旋转状态会立即反映到其他功能窗口（极射赤面投影、衍射模拟器等）。
  * 内置视频编码器（Windows Media Foundation）可生成用于演示的旋转动画视频（H.264/H.265 MP4）。

### 极射赤面投影

* 在极射赤面投影图上绘制晶面与晶轴。
  * 同时支持等角投影（吴氏网）与等积投影（施密特网），并可显示经纬线。
  * 指数可按数值范围或具体数值指定。
  * 可通过指定晶带轴显示大圆。
  * 绘制对象可保存或复制为矢量格式，便于日后编辑而不损失分辨率。
  * 面向教学的极射赤面投影几何关系三维可视化。

### 衍射模拟器

* 模拟 X 射线、电子与中子源的单晶衍射图样。
  * 可自由设置入射束的动能。
  * 内置 <sup>1</sup>H 至 <sup>98</sup>Cf 的特征 X 射线能量。
  * 绘制范围由图像分辨率（像素尺寸）与相机长度指定。
  * 也支持探测器倾斜的几何配置。
  * 支持叠加实验获取的图像。
  * 可控制晶体旋转（衍射条件），并立即与其他窗口同步。

* **多晶衍射**：假定多晶试样的德拜环图样模拟。
* **进动照相机**（X 射线）：零阶劳厄带进动照相机图样模拟。
* **背反射劳厄照相机**（X 射线）：背反射劳厄图样模拟。

#### 运动学衍射理论
* 适用于所有束源（X 射线、电子、中子）。
* 衍射强度由晶体结构因子振幅的平方与激发误差估算。
* 已考虑德拜–沃勒因子对衍射强度的影响。

#### 动力学衍射理论（电子）
* 基于**布洛赫波法**（Bethe, 1928），可灵活设定晶体取向，不受低指数晶带轴的限制。
* 提供两种计算方法：
  * **Bethe 本征值法**：通过矩阵对角化求布洛赫本征态的本征值/本征矢，适合改变试样厚度的情形。
  * **散射矩阵法**：采用缩放平方法结合 Padé 近似直接计算矩阵指数，适合单一厚度的快速计算。
* 自动选择最快的算法与最合适的数学库（Eigen、Intel MKL 或 Math.NET）。
* 热漫散射（TDS）吸收势采用解析方法计算，以获得高性能。

* **SAED**（选区电子衍射）：包含动力学散射效应的平行束电子衍射模拟。
* **PED**（进动电子衍射）：通过指定进动角与方位角分辨率模拟 PED 图样。可用于晶体结构分析以及准运动学 PED 条件的优化。
* **CBED**（会聚束电子衍射）：可指定会聚半角与分割数模拟 CBED 图样。支持沿厚度方向的模拟，用于确定试样厚度。
  * 位置平均 CBED（PACBED）图样。
  * 大角度 CBED（LA-CBED）模拟。

### HRTEM 模拟器

* 在相同的布洛赫波理论框架下进行高分辨透射电子显微图像模拟。
* 光学参数（加速电压、球差系数、欠焦量、试样厚度等）通过图形界面设置。
* 内置典型的 TEM 光学参数预设，可通过右键调用。
* 针对部分相干性提供两种成像模型：
  * **线性衬度传递理论**：计算成本较低，适用于满足弱相位物体近似的薄试样。
  * **非线性衬度传递理论（TCC 模型）**：基于一阶透射交叉系数（Ishizuka, 1980），即使对较厚试样和较高原子序数的材料也可靠。
* 可绘制带包络函数的衬度传递函数。
* 可同时计算厚度–欠焦系列图像。
* 在常规条件下通常可在 1 秒内完成计算。

### STEM 模拟器

* 扫描透射电子显微图像模拟。
  * 明场（BF）、环形暗场（ADF）与高角环形暗场（HAADF）成像模式。
  * 会聚束按多个平面波的叠加处理，并精确计算重叠部分。
  * 非弹性散射电子采用吸收势模型计算。
  * 可生成厚度–欠焦系列图像。

### Spot ID

* 针对实测 SAED 图样的半自动衍射斑点标定。
* **Spot ID v1**：利用衍射斑点的几何配置（距离与夹角）搜索晶带轴。支持同时分析 2–3 幅图像。
* **Spot ID v2**：直接导入 SAED 图样图像。
  * 支持常见图像格式：TIFF (.tif)、Digital Micrograph 3/4 (.dm3, .dm4) 等。
  * 自动检测衍射斑点并用二维 pseudo-Voigt 函数拟合。
  * 穷举搜索与倒易点阵矢量排列相匹配的晶体取向。
  * 即使是高指数晶带轴也能准确确定。

### 旋转几何（测角台）

* 将 ReciPro 中的欧拉角与实验室的测角台关联起来。
* 给出为获得所需晶体取向（例如低指数晶带轴）应如何旋转测角台的信息。
* 支持任意的测角台定义。

### 宏

* 采用 Python 语法的宏脚本，可实现任务自动化。
  * 示例：以 1° 为步长旋转晶体，并在每一步保存衍射图样或 STEM 图像。
  * ReciPro 专用函数位于 “ReciPro” 命名空间中。
  * 使用示例见[手册](https://seto77.github.io/ReciPro/zh-Hans/20-macro/2-examples/)。

### 其他功能

* **电子射程模拟器**：材料中电子射程的蒙特卡罗模拟。
* **EBSD**（电子背散射衍射）：开发中。

## 技术细节

* 使用 **C++**、**C#** 与 **OpenGL 着色语言（GLSL）** 编写。
* 采用多线程并行化，在现代多核 CPU 上实现高性能计算。
* 晶体取向变化时，所有功能窗口都会实时同步更新。
* 采用右手笛卡尔坐标系（X：右，Y：上，Z：前）与 Z–X–Z 欧拉角约定。
* 坐标定义与 Thermo Fisher Scientific 的 EBSD 软件兼容。

### 学术影响

* **同行评审的软件论文：** [Seto, Y. & Ohtsuka, M. (2022), *Journal of Applied Crystallography*, **55**, 397-410](https://doi.org/10.1107/S1600576722000139).
* **引用论文：** [Google Scholar 引用文献](https://scholar.google.jp/scholar?cites=12625594477623342627).
* **论文关注度：** [Altmetric 详情](https://www.altmetric.com/details/123778746).

| 指标 | 主要数值 |
| --- | --- |
| GitHub 累计下载量 | 27,000 次以上 |
| Google Scholar 被引次数 | 170 次以上 |
| Dimensions 被引次数 | 160 次以上 |
| Mendeley 读者数 | 90 人以上 |

## 屏幕截图

<img src="https://seto77.github.io/ReciPro/assets/cap-zh-Hans-auto/FormMain.png" height="320px" alt="主窗口">
<img src="https://seto77.github.io/ReciPro/assets/cap-zh-Hans-auto/FormCrystalDatabase.png" height="320px" alt="晶体数据库">
<img src="https://seto77.github.io/ReciPro/assets/cap-zh-Hans-auto/FormSymmetryInformation.png" height="320px" alt="对称性信息">
<img src="https://seto77.github.io/ReciPro/assets/cap-zh-Hans-auto/FormBeamInteraction.png" height="320px" alt="射束相互作用">
<img src="https://seto77.github.io/ReciPro/assets/cap-zh-Hans-auto/FormStructureViewer.png" height="320px" alt="结构查看器">
<img src="https://seto77.github.io/ReciPro/assets/cap-zh-Hans-auto/FormStereonet.png" height="320px" alt="极射赤面投影">
<img src="https://seto77.github.io/ReciPro/assets/cap-zh-Hans-auto/FormDiffractionSimulator.png" height="320px" alt="衍射模拟器">
<img src="https://seto77.github.io/ReciPro/assets/cap-zh-Hans-auto/FormImageSimulator.png" height="320px" alt="HRTEM/STEM 模拟器">
<img src="https://seto77.github.io/ReciPro/assets/cap-zh-Hans-auto/FormSpotIDV2.png" height="320px" alt="Spot ID v2">
<img src="https://seto77.github.io/ReciPro/assets/cap-zh-Hans-auto/FormMacro.png" height="320px" alt="宏">
<img src="https://seto77.github.io/ReciPro/assets/cap-zh-Hans-auto/FormTrajectory.png" height="320px" alt="电子射程模拟器">

***
