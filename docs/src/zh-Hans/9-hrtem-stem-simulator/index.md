---
title: HRTEM / STEM Simulator
---

# HRTEM / STEM Simulator

**HRTEM/STEM 模拟器**针对所选晶体及其取向，模拟 TEM 晶格条纹 (HRTEM) 图像、STEM 图像以及投影晶体势。点击 **模拟** 即可运行计算。

![HRTEM/STEM 模拟器](../../assets/cap-zh-Hans-auto/FormImageSimulator.png)

窗口分为左右两半。**左侧** 显示模拟结果并控制其外观（图像窗格、亮度、颜色、标尺等）；**右侧** 为计算条件（**光学属性** 和 **模拟设置**）。

---

## 本页与各模式页面

- **本页（概述）**：说明所有模式通用的操作，以及 **左侧的结果显示与调整控件**。
- **各模式页面**：完整说明该模式下 **右侧** 出现的所有设置，使每个页面都能独立阅读（因此部分设置会在多个页面中重复出现）。

| 模式 | 内容 | 页面 |
|------|----------|------|
| **HRTEM** | 高分辨 TEM 晶格条纹像 | [HRTEM 模拟](1-hrtem-simulation.md) |
| **STEM** | 扫描透射电子显微镜像 (BF / ABF / LAADF / HAADF) | [STEM 模拟](2-stem-simulation.md) |
| **Potential** | 投影晶体势 ($U_g$ / $U'_g$) | [势模拟](3-potential-simulation.md) |

---

## 键盘和鼠标快捷键

结果以一个或多个图像窗格的形式显示。它们使用 ReciPro 的标准[图像视图导航](../21-shortcuts.md)，所有窗格会一起平移和缩放。

| 快捷键 | 操作 |
|----------|--------|
| <kbd>F1</kbd> | 打开在线手册的此页面 |
| <kbd>CTRL</kbd>+<kbd>C</kbd>（图像网格获得焦点时） | 将图像作为图元文件复制到剪贴板 |
| 左键拖动 / 中键拖动 | 平移图像（所有窗格一起移动） |
| 鼠标滚轮向上 / 向下 | 在光标处放大 (×2) / 缩小 (×0.5) |
| 右键拖出一个矩形框 | 放大到所选区域 |
| 右键单击 / 右键双击 | 缩小 (×0.5) |
| <kbd>CTRL</kbd> + 右键拖出一个矩形框 | 选择一个矩形区域 |
| 在窗格上左键双击 | 最大化该窗格 / 恢复网格（多窗格布局） |
| 移动鼠标（不按键） | 读取光标处的位置 (pm) 和像素值 |

→ 请参阅 **[21. 键盘和鼠标快捷键](../21-shortcuts.md)** 以一览每个窗口。

---

## 按目标快速导航

| 目标 | 起点 | 参考 |
|------|------------|-----------|
| 计算单张 HRTEM 图像 | 将 **图像模式** 设为 **HRTEM**，然后在 **TEM 条件** 中设置加速电压和欠焦 | [HRTEM 模拟](1-hrtem-simulation.md)、[HRTEM 成像](../appendix/a3-bloch-wave/hrtem.md) |
| 计算 STEM 图像 | 将 **图像模式** 设为 **STEM**，然后在 **STEM 选项** 中设置会聚角和探测器 | [STEM 模拟](2-stem-simulation.md)、[STEM 计算](../appendix/a3-bloch-wave/stem.md) |
| 查看投影势 | 将 **图像模式** 设为 **Potential** | [势模拟](3-potential-simulation.md) |
| 生成厚度 / 欠焦序列 | 在 HRTEM 中配置 **单幅/序列模式** 和图像条件 | [HRTEM 模拟](1-hrtem-simulation.md) |
| 使用带 TDS 的 HAADF-STEM | 将原子温度因子设为非零值，并将 STEM 探测器移至 LAADF / HAADF | [STEM 计算](../appendix/a3-bloch-wave/stem.md) |

---

## 基本工作流程

1. 在主窗口中选择晶体和取向，然后打开此窗口。
2. 在 **图像模式** 中选择 HRTEM、STEM 或 Potential。
3. 在 **光学属性** 中设置加速电压、欠焦、像差、光阑、STEM 会聚角等（参见各模式页面）。
4. 在 **模拟设置** 中设置厚度、图像尺寸、分辨率、布洛赫波数量、部分相干模型等（参见各模式页面）。
5. 点击 **模拟**，然后根据需要在左侧的 **调整**、**归一化** 和 **显示** 中调整外观。

---

## 选择图像模式

![图像模式](../../assets/cap-zh-Hans-auto/FormImageSimulator.splitContainer1.flowLayoutPanelModeSelection.groupBoxImageMode.png){ align=left }

右上方的 **图像模式** 用于选择计算类型。右侧的面板（**光学属性** 和 **模拟设置**）会随所选模式而变化。<div style="clear: both;"></div>

- **HRTEM** — 高分辨 TEM 晶格条纹像 → [HRTEM 模拟](1-hrtem-simulation.md)
- **STEM** — 扫描透射电子显微镜像 → [STEM 模拟](2-stem-simulation.md)
- **Potential** — 投影晶体势 → [势模拟](3-potential-simulation.md)

---

## 图像区域（左侧）

窗口左半部分显示模拟图像。顶部的状态栏显示光标位置 (**X:**、**Y:**) 以及光标下的图像 **值:**（强度），旁边是一个 **低 → 高** 强度刻度，反映当前的颜色映射和亮度范围。

当生成多幅图像时（序列图像，或势的幅值/相位），它们会以网格形式平铺显示，所有窗格一起缩放和平移。

---

## 结果的显示与调整（左侧面板） {#display-settings}

左下方的面板用于调整结果的外观——亮度、颜色、归一化和叠加显示。这些设置适用于所有模式，无需重新计算即可生效。

### 调整

![调整](../../assets/cap-zh-Hans-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxAdjust.png)

- **Min** / **Max** ：显示强度范围的下限（黑）和上限（白）。可用滑块调整衬度。
- **颜色** ：图像的色阶——**Gray scale** 或 **Cold-warm**（蓝到红）。
- **高斯模糊 (FWHM)** ：勾选后，按右侧给定的半高全宽 (pm) 施加高斯模糊，以近似有限的分辨率（点扩展函数）。勾选期间，新计算出的图像也会被模糊处理。FWHM 按各图像计算时所用的 **分辨率** (pm/px) 换算为像素。<!-- 261105Cl 監査: FormImageSimulator.cs:ApplyGaussianBlur -->

### 归一化

![归一化](../../assets/cap-zh-Hans-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxNormalization.png)

- **逐幅图像** ：勾选后，对每幅图像分别归一化（不勾选时，整个序列共用同一标度）。
- **Min** / **Max** ：将归一化的下限 / 上限固定为右侧给定的值，而不是图像的最小值 / 最大值。
- **EDX 共用** ：仅在显示 [EDX 分布图](2-stem-simulation.md#stem-edx) 且计算产生了多个通道时显示；将同一物理量的所有通道置于同一显示范围内。EDX 分布图从不按 **Min** / **Max** 重新定标：其下限固定为 0，只有上限遵循 **逐幅图像** / **EDX 共用**。
- 在势模式下此组被隐藏。

### STEM 图像

![STEM 图像](../../assets/cap-zh-Hans-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxSTEMoption3.png)

仅在 STEM 模式下显示。选择显示计算所得 STEM 图像中的哪种散射成分（**弹性**、**TDS** 或 **弹性 &TDS**）。若同一次计算中还计算了 [STEM-EDX 分布图](2-stem-simulation.md#stem-edx)，则可选择第四项 **EDX**。由于此项为 STEM 专用，[STEM 模拟](2-stem-simulation.md) 页面中也有说明。

### 显示

![显示](../../assets/cap-zh-Hans-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxDisplay.png)

设置叠加在图像上的项目。

- **晶胞** ：叠加显示投影晶胞的轮廓，便于将图像衬度与晶格对应起来。平行于 **a**、**b**、**c** 的棱分别以红、绿、蓝绘制，并以图像中心为中心。
- **标签** ：叠加显示各窗格的标签——HRTEM / STEM 中为厚度 `t=` 和欠焦 `f=`（nm），势模式中为窗格名称（例如 *Ug magnitude*）。可指定 **大小**（字号）和 **颜色**。
- **标尺** ：叠加显示比例尺。可指定 **长度** (nm) 和 **颜色**。

---

## 运行模拟

![模拟操作](../../assets/cap-zh-Hans-auto/FormImageSimulator.splitContainer1.panelSimulationActions.png)

- **模拟** ：使用当前的晶体、显微镜条件、厚度、欠焦和显示设置运行计算。
- **停止** ：中止正在进行的计算（仅在计算过程中显示）。
- **实时模拟** ：勾选后，旋转晶体时立即重新计算（STEM 模式下隐藏）。
- **预设设置** ：切换预设窗口的显示，该窗口用于保存和调用 TEM 成像条件（见下文[预设列表](#preset-list)）。

### 预设列表 {#preset-list}

**预设列表** 窗口保存模拟器设置的命名快照。快照保存的内容包括：图像模式、加速电压、Cs、Cc、ΔV、欠焦、β、物镜光阑（大小与偏移）、HRTEM 部分相干模型、厚度、单幅/序列模式、序列扫描哪些量（厚度 / 欠焦）及其 **起始 / 步长 / 数量**、序列厚度 / 欠焦列表、图像尺寸与分辨率、布洛赫波数，以及 STEM 设置（会聚角、探测器角度、光源尺寸、切片厚度、角分辨率、**计算 EDX 分布图** 和 **谱线系 (Kα, Lα…)**）。该列表随 ReciPro 的设置一起保存，重启后仍然保留。早期版本保存的预设不含单幅/序列设置；应用这些预设时，这些设置保持不变。<!-- 261105Cl 監査: FormPresets.cs ImageSimulatorSetting (SerialImageMode ほか) -->

- 单击某个条目会立即应用它。
- **确定** 关闭窗口并保留当前设置；**取消**（或 <kbd>Esc</kbd>）关闭窗口并恢复打开窗口时生效的设置。
- **管理预设列表** ：勾选后，单击条目只会选中它（不会应用），并出现 **名称** 框以及 **添加** / **替换** / **重命名** / **删除**。**添加** 以给定名称追加当前设置，**替换** 用当前设置和 **名称** 框中的名称就地覆盖所选条目，**重命名** 只更改其名称。

显微镜预设（ARM300F、2100F 等）不在此列表中；它们位于 **TEM 条件** 的右键菜单中（见 [HRTEM 模拟](1-hrtem-simulation.md#tem-conditions)）。

---

## 文件菜单

![文件菜单](../../assets/cap-zh-Hans-auto/FormImageSimulator.menuStrip1.fileToolStripMenuItem.png)

- **保存图像** ：可选择 **为图像 (PNG 格式)**、**为图像 (TIFF 格式)** 或 **为图元文件 (EMF)** 保存。PNG 和 EMF 保存显示的内容（显示范围和色阶）；**序列图像模式下单独保存**（默认开启）将各窗格分别写入单独的文件，而不是一幅拼接图像。TIFF 总是每个窗格写出一个浮点文件，不含叠加内容。它保存的是 **归一化** 之后（若应用了 **高斯模糊 (FWHM)** 则为其之后）的像素值，而不是 **调整** 的显示范围；在默认设置下（**Min** 0、**Max** 1、**逐幅图像**），每幅图像被缩放到 0–1，因此要导出计算得到的强度，请取消勾选 **Min** / **Max**。EDX 分布图总是不经缩放写出。
- **复制图像** ：**为图像** 或 **为图元文件 (EMF)** 复制到剪贴板。
- **叠印符号**（默认开启） ：将晶胞、标签和标尺烧录到保存或复制的 PNG / EMF 图像中。
- **加载 TEM 参数** / **保存 TEM 参数** ：以 XML 文件（`*.rpis`）读取或写出模拟器设置。该文件包含与[预设](#preset-list)相同的项目——图像模式、加速电压、像差、欠焦、物镜光阑、厚度和单幅/序列设置、图像尺寸与分辨率、STEM 设置等——因此可以保存条件，或在计算机之间共享。文件中缺少的项目（例如由旧版本写出的文件）会被设为窗口的初始值，未知的项目会被忽略；无法读取的文件会以消息提示。加载不会重新计算图像。<!-- 261106Cl 作者の決定 E7: FormPresets.cs ImageSimulatorSetting.SaveXml/LoadXml -->

## 帮助菜单

![帮助菜单](../../assets/cap-zh-Hans-auto/FormImageSimulator.menuStrip1.helpToolStripMenuItem.png)

- **HRTEM 模拟基本概念** ：打开 HRTEM 成像的说明（[附录 A3.2](../appendix/a3-bloch-wave/hrtem.md)）。

计算库不再在此窗口中选择：原生库（C++/Eigen）可用时，HRTEM 图像合成会自动使用它。若要停止使用，请在[主窗口](../0-main-window.md)中开启 **选项 ▸ 禁用 Crystallography.Native 库（需要重启）**。<!-- 261106Cl 作者の決定 E12: 計算ライブラリの切替を廃止 -->

---

## 另请参阅

- [HRTEM 模拟](1-hrtem-simulation.md)
- [STEM 模拟](2-stem-simulation.md)
- [势模拟](3-potential-simulation.md)
- [动力学衍射（布洛赫波）](../appendix/a3-bloch-wave/index.md)
- [衍射模拟器](../7-diffraction-simulator/index.md)
- [电子轨迹](../8-electron-trajectory.md)
