# 故障排除

ReciPro 的常见问题与解决方法。下面的许多条目来自 [GitHub issue 跟踪器](https://github.com/seto77/ReciPro/issues) 上的提问和缺陷报告；在适用的情况下，标注了修复该缺陷的版本。

> **大多数问题只需更新到[最新版本](https://github.com/seto77/ReciPro/releases/latest)即可解决。** ReciPro 更新频繁，下面的许多缺陷在报告后数日内即已修复。

---

## 启动与运行

### 症状：进程在运行，但没有出现窗口

ReciPro 已启动（在任务管理器中可见），但其窗口始终不在屏幕上显示。

**原因**：窗口在屏幕之外打开——这是 Windows 显示坐标的问题，通常发生在更换显示器或更改显示缩放之后。（Issues [#50](https://github.com/seto77/ReciPro/issues/50)、[#53](https://github.com/seto77/ReciPro/issues/53)、[#55](https://github.com/seto77/ReciPro/issues/55)）

**解决方法**：

1. 打开**任务管理器**。
2. 在进程列表中找到 **ReciPro**。
3. 右键单击它并选择**最大化**。

窗口将被带到主显示器上。请注意，**切换到**、**前置**和**最小化**都*无济于事*——只有**最大化**有效。

### 症状：ReciPro 无法启动、崩溃或在启动时挂起

**原因**：最常见的是 OpenGL 初始化失败，或损坏的注册表/设置值阻止了启动。

**解决方法**（按顺序尝试）：

1. **禁用 OpenGL**：启动 ReciPro 时按住 **Ctrl** 键，以禁用 OpenGL 的方式启动。较新的版本（v4.925 及以后）强化了 OpenGL 初始化，因此即使 OpenGL 失败应用也能启动——在这种情况下 3D 功能被禁用，但应用的其余部分可正常工作。
2. **重置设置**：在注册表编辑器中删除键 `HKEY_CURRENT_USER\Software\Crystallography\ReciPro`，然后重新启动。（等同于 **Option → Reset registry**。）
3. **干净重装**：卸载 ReciPro，删除以下文件夹（如果存在，将 `<user>` 替换为你的账户名），然后重新安装：
   - `C:\Users\<user>\AppData\Local\Crystallography Software\ReciPro`
   - `C:\Users\<user>\AppData\Roaming\ReciPro\ReciPro`
4. **更新**到最新版本。

如果以上都无济于事，原因可能在于操作系统环境本身；请连同你的 PC 详情（CPU、GPU、Windows 版本）一起[提交一个 issue](https://github.com/seto77/ReciPro/issues)。

---

## OpenGL 问题

### 症状：启动时黑屏或崩溃

**原因**：GPU 不兼容或远程桌面环境。

**解决方法**：

1. 转到 **Option → Disable OpenGL (needs restart)**（或在启动时按住 **Ctrl**）。
2. 重新启动 ReciPro。
3. 结构查看器和部分 3D 功能将使用软件渲染。

### 症状：集成或较旧的 GPU（Intel/AMD）无法渲染

**原因**：某些集成 GPU（例如 AMD Radeon Vega、Intel UHD）在较旧的构建中存在 OpenGL 初始化问题。（Issue [#2](https://github.com/seto77/ReciPro/issues/2)）

**解决方法**：更新到最新版本。OpenGL 版本要求已降低（v4.781），集成 GPU 的初始化已修复（v4.785），初始化也进一步得到强化以实现可控失败（v4.925）。更新 GPU 驱动程序同样有帮助。

### 症状：渲染质量差

**解决方法**：更新 GPU 驱动程序。推荐使用支持 OpenGL 1.5 的外置（独立）GPU。

---

## .NET 运行时

### 症状：应用程序无法启动

**原因**：未安装所需的 .NET Desktop Runtime。当前版本需要 **.NET Desktop Runtime 10.0**（较旧的构建：v4.895–v4.91x 需要 9.0；参见 issue [#43](https://github.com/seto77/ReciPro/issues/43)）。

**解决方法**：从 <https://dotnet.microsoft.com/download/dotnet/10.0> 下载并安装（选择 **Desktop Runtime**，大多数 PC 选 x64）。

### 症状：无法访问 Microsoft 下载页面

**解决方法**：你可以直接下载运行时安装程序。在 [.NET 10.0 下载页面](https://dotnet.microsoft.com/download/dotnet/10.0) 上为你的架构选择 **Windows Desktop Runtime X64**。（Issue [#49](https://github.com/seto77/ReciPro/issues/49)）

---

## 安装

### 症状：在没有管理员权限的情况下安装或卸载

**注意**：不需要管理员权限。快捷方式和每个用户的文件被放置在你自己的用户文件夹中（例如 `%AppData%\Microsoft\Windows\Start Menu\Programs\Crystallography Software\` 和桌面）。（Issue [#38](https://github.com/seto77/ReciPro/issues/38)）

---

## 显示与布局

### 症状：按钮或面板被裁切 / 隐藏，或布局看起来已损坏

例如，在较新的版本中，Spot ID v2 中的 **Peak Identification** 按钮被隐藏，或者“关于”页面及其他窗体未对齐。（Issues [#56](https://github.com/seto77/ReciPro/issues/56)、[#59](https://github.com/seto77/ReciPro/issues/59)）

**原因**：在某些较新的构建中引入的 DPI 缩放 / UI 字体回归问题。

**解决方法**：

- 将 Windows **显示缩放设置为 100%**（这通常可恢复布局）。
- 作为快速变通办法，**调整窗口大小**（例如在垂直方向缩小），以显示被隐藏的控件。
- 更新到最新版本——布局正在逐步修复。如果某个较新的构建看起来更糟，回退到稍旧的版本（例如 v4.915）是一种临时选择。如有仍然损坏的窗体，请报告。

---

## 动力学计算

### 症状：非常慢或内存不足

**原因**：布洛赫波过多或图像过大。

**解决方法**：

- 减少 **No. of Bloch waves**（50–200 通常足以满足常规计算）
- 对 ≤ 500 个波使用 **Eigen** 求解器；对 > 500 个波使用 **MKL**
- 降低 STEM 模拟的图像分辨率
- 关闭其他占用大量内存的应用程序

### 症状：HAADF-STEM 图像为黑色

**原因**：原子温度因子（B）被设置为零。

**解决方法**：为所有原子设置 B ≥ 0.5 Å²。TDS 强度需要非零的温度因子。

---

## 衍射模拟器

### 症状：衍射图样为空白 / 什么都没有绘制

**原因**：通常是视图放大过度，或入射波能量超出范围。（Issue [#3](https://github.com/seto77/ReciPro/issues/3)）

**解决方法**：

- **左键单击**主绘图区域以缩小。
- 在 **Wave** 选项卡（左上角）检查入射波能量：X 射线 ≈ 1–100 keV、电子 ≈ 10–1000 keV 为适宜值。

---

## 文件输入/输出

### 症状：CIF 文件无法加载

**解决方法**：

- 检查 CIF 文件是否格式良好
- 尝试将文件拖放到**晶体信息**区域
- 某些非标准的 CIF 扩展可能不受支持

### 症状：dm3/dm4 文件无法加载，或出现 "unable to cast … 'System.Single' to 'System.Double'"

**原因**：DM3/DM4 格式有多种变体，较旧的构建无法读取全部变体。（Issue [#15](https://github.com/seto77/ReciPro/issues/15)）

**解决方法**：更新到最新版本——DM3 读取兼容性已在 v4.835 中改进。如果文件仍无法加载，请[发送给我们](https://github.com/seto77/ReciPro/issues)，以便添加支持。

### 症状：dm3/dm4 文件显示的比例尺错误

**解决方法**：在原始的 Digital Micrograph 软件中核实校准。ReciPro 读取嵌入的元数据；如果元数据不正确，请在光学面板中手动设置像素大小和相机长度。

---

## 重置注册表

如果设置变得损坏：

1. **Option → Reset registry (after restart)**
2. 重新启动 ReciPro——窗口位置、波长、相机长度等将被重置为默认值

---

## 常见问题

### 有 Mac（或 Linux）版本吗？ {#mac-linux}

没有官方的 Mac 或 Linux 版本。ReciPro 依赖 **.NET Desktop Runtime**，目前它仅在 Windows 上运行。（Issue [#12](https://github.com/seto77/ReciPro/issues/12)）

不过，有人报告了一条在 macOS 上可行的非官方途径：**win-x64 portable ZIP** 发行版（可在[发布页面](https://github.com/seto77/ReciPro/releases/latest)获取）借助 **Sikarugir** Wine 包装器结合 **Mesa3D** OpenGL 驱动程序，可在 macOS（Apple Silicon）上运行——无需 Windows 许可证或虚拟机。一位用户发布的分步设置指南可在 <https://github.com/Ryo-fkushima/ReciPro_macOS_memo> 获取。

请注意，此配置未获官方支持，也未经过完全验证。一项已知的限制是某些符号（Å、上标、箭头）可能显示不正确。

**修复乱码符号（Å、上标、箭头）：** 原因在于 ReciPro 通常使用的 Windows 字体（Segoe UI、Yu Gothic UI 等）在 Wine 环境中缺失，而 Wine 内置的替代字体缺少某些科学字形。ReciPro 在**检测到它运行于 Wine 之下时**会自动切换到覆盖范围广的字体，因此修复方法只是让这些字体在 Wine prefix 中可用：

1. 安装 **DejaVu Sans** / **DejaVu Serif**（覆盖 Å、上标、箭头、分数标签），对于日语界面，安装 **Noto Sans CJK JP**（或 **Noto Sans JP**）。
2. 最简单的方法是将下载的 `.ttf`/`.otf` 文件复制到 prefix 的字体文件夹中——即 Sikarugir 包装器内的 `…/drive_c/windows/Fonts/`——然后重新启动 ReciPro。（`winetricks` 也可以安装其中一些。）
3. 重新启动时 ReciPro 会自动识别它们；无需更改任何 ReciPro 设置。

如果未安装这些字体，ReciPro 会保留其默认字体名称，因此不会变得更糟——符号只是仍然保持乱码。

**关于此途径的展望——两点坦诚的说明：**

- 实验性的 **win-arm64** ZIP 在当前的 Mac 上**无法**运行，即使是 Apple Silicon 也不行：当今的 macOS Wine 构建（包括 Sikarugir）通过 Rosetta 2 执行 x86_64 Windows 二进制文件，没有任何机制来运行 ARM64 Windows 二进制文件。在 Mac 上请始终使用 **win-x64** portable ZIP。
- Apple 正在逐步淘汰 Rosetta 2。macOS 27（2026 年秋季）被宣布为最后一个完整支持 Rosetta 2 的版本，因此当前的 x64 + Rosetta 途径预计从 macOS 28（2027 年秋季）起将不再可用。一个面向 macOS 的原生 ARM64 Wine 正在上游开发中；如果它得以实现，win-arm64 ZIP 可能成为 Mac 上的后继方案，但目前还无法保证。

### ReciPro 能在 Windows on ARM（ARM64）上运行吗？ {#windows-on-arm}

可以——有两条途径：

- **原生 ARM64 软件包（实验性，推荐）**：从 v4.938 起，[发布页面](https://github.com/seto77/ReciPro/releases/latest) 上发布了一个实验性的原生 ARM64 portable 软件包（`ReciPro-v.X_arm64.zip`；在 v.4.939 之前命名为 `ReciPro-v.X-arm64.zip`）。它是 self-contained 的，因此无需安装 .NET Runtime——将 ZIP 解压到一个用户可写的文件夹并运行 `ReciPro.exe`。如果 Windows 阻止了下载的 ZIP（Mark of the Web），请在解压*之前*右键单击该 ZIP → **属性** → 勾选**解除锁定** → **确定**（或在 PowerShell 中运行 `Unblock-File .\ReciPro-*arm64.zip`）。详情见随附的 `README-PORTABLE.txt`。
- **在模拟下运行的 x64 软件包**：常规的 MSI 安装程序和 win-x64 portable ZIP 在安装了 .NET Desktop Runtime（x64）后，也可通过内置的 x64 模拟在 ARM64 Windows 上运行（约从 v4.913 配合 .NET 10 起确认可行）。繁重的计算运行起来比原生构建慢。（Issue [#47](https://github.com/seto77/ReciPro/issues/47)）

关于原生 ARM64 软件包的说明：

- Intel MKL 没有 ARM64 版本，因此相应的求解器选项和菜单项被隐藏。动力学计算使用随附的经 NEON 优化的原生库；在具有代表性的验证案例中，其结果在预期的浮点容差范围内与 x64 构建一致。
- 3D 视图（结构查看器及相关窗口）可以运行，但 Windows on ARM 仅通过 Direct3D 12 转换层（GLOn12 / Mesa）提供 OpenGL，因此 3D 渲染明显慢于配备原生 OpenGL 驱动程序的 PC——这是平台限制，并非缺陷，原生 ARM64 构建也无法改变这一点。结构查看器中的 **High quality (Per-Pixel Linked List)** 透明模式在此驱动栈上尤其缓慢；推荐使用默认的 **Approximate** 模式。如果 3D 视图无法启动，请从 Microsoft Store 安装 “OpenCL, OpenGL, and Vulkan Compatibility Pack”。
- ARM64 软件包**不能**在 macOS + Wine 下运行（参见上一个问题）。在 Mac 上请使用 win-x64 portable ZIP。

### 我应该如何引用 ReciPro？

使用 [GitHub 仓库页面](https://github.com/seto77/ReciPro) 上的 **Cite this repository** 链接（元数据由 `CITATION.cff` 提供）。首选的引用为：

> Seto, Y. & Ohtsuka, M. (2022). *J. Appl. Cryst.* **55**, 397–410. doi:[10.1107/S1600576722000139](https://doi.org/10.1107/S1600576722000139)

（Issue [#33](https://github.com/seto77/ReciPro/issues/33)）

---

## 报告缺陷

请在此报告问题：<https://github.com/seto77/ReciPro/issues>

请包含：

- ReciPro 版本号
- 重现问题的步骤
- 任何错误消息或截图
