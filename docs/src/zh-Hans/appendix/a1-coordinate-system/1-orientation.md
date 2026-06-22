# 附录 A1.1. 基本坐标系与晶体取向

<!-- 260526Cl: 図(Coordinates1-3)に合わせ座標記号を数式(MathJax)化・用語色を図に一致 (.rp-*: docs/src/assets/stylesheets/extra.css)。 -->

本页定义 ReciPro 的**基本（取向）坐标系**，它用于一切涉及晶体旋转的场合（主窗口、结构查看器、极射赤平投影、旋转几何以及衍射模拟），并说明如何表示晶体的初始取向和欧拉角旋转。在**衍射模拟器**中用于放置探测器的另一套坐标系，在 [A1.2. 衍射模拟的坐标系](2-diffraction.md) 中描述。

---

## 取向的定义

ReciPro 采用固定在显示器上的**右手坐标系**：

| 轴 | 方向 |
|------|-----------|
| <span class="rp-red">$X$</span> | 显示器的右方 |
| <span class="rp-green">$Y$</span> | 显示器上的向上方向 |
| <span class="rp-blue">$Z$</span> | 垂直于显示器向外、朝向观察者 |

![显示器上显示的 ReciPro 坐标轴](../../../assets/references/Coordinates1.png){width=400px}

**射束方向**对应于观察方向（朝显示器内看），即 <span class="rp-blue">$-Z$</span> 轴。

ReciPro 中的大多数操作只涉及*方向*（以 3×3 旋转矩阵表示），并不需要明确的原点。唯一的例外是**衍射模拟器**功能，它需要一个明确的原点——参见 [A1.2. 衍射模拟的坐标系](2-diffraction.md)。

## 晶体的初始取向

初始取向（首次启动时，或在**重置旋转**之后）定义如下：

1. <span class="rp-blue">$c$</span> 轴与 <span class="rp-blue">$Z$</span> 轴对齐。
2. <span class="rp-green">$b$</span> 轴位于 <span class="rp-green">$Y$</span><span class="rp-blue">$Z$</span> 平面内，靠近 <span class="rp-green">$Y$</span> 轴。
3. <span class="rp-red">$a$</span> 轴随后由 <span class="rp-green">$b$</span> 轴和 <span class="rp-blue">$c$</span> 轴确定（右手定则）。

![初始取向：晶体 a / b / c 轴相对于 X / Y / Z 的关系，入射束沿 −Z 方向](../../../assets/references/Coordinates2.png){width=300px}

等价地：

- 从显示器向外（朝向观察者）的方向是 **[001]** 晶带轴。
- 显示器上向右的方向是 **(100)** 晶面的法线。

> **注意：** <span class="rp-blue">$c$</span> 轴（= [001]）始终与 <span class="rp-blue">$Z$</span> 重合，但在某些晶系中 <span class="rp-red">$a$</span> 轴和 <span class="rp-green">$b$</span> 轴**不一定**与 <span class="rp-red">$X$</span> 和 <span class="rp-green">$Y$</span> 重合。

## 欧拉角

晶体取向用三个欧拉角 <span class="rp-olive">$\Phi$</span>、<span class="rp-cyan">$\theta$</span>、<span class="rp-magenta">$\Psi$</span> 表示，按 <span class="rp-blue">$Z$</span>–<span class="rp-red">$X$</span>–<span class="rp-blue">$Z$</span> 顺序施加（先 <span class="rp-magenta">$\Psi$</span>，再 <span class="rp-cyan">$\theta$</span>，最后 <span class="rp-olive">$\Phi$</span>）。当三个角度全为零时，对应的旋转轴为：

| 角度 | 轴（当所有角度 = 0 时） | 等级 |
|-------|----------------------------|------|
| <span class="rp-olive">$\Phi$</span> | <span class="rp-blue">$Z$</span> | 第 1（最高） |
| <span class="rp-cyan">$\theta$</span> | <span class="rp-red">$X$</span> | 第 2（中间） |
| <span class="rp-magenta">$\Psi$</span> | <span class="rp-blue">$Z$</span> | 第 3（最低） |

![欧拉角旋转轴 — Φ（黄）、θ（青）、Ψ（品红）— 在 0°（上）和 15°（下）时所示](../../../assets/references/Coordinates3.png){width=400px}

这三个角度构成一个**层级**：<span class="rp-olive">$\Phi$</span> 是最高级的旋转，其次是 <span class="rp-cyan">$\theta$</span>，再次是 <span class="rp-magenta">$\Psi$</span>。较低级轴的方向取决于较高级旋转的状态。例如，当 <span class="rp-olive">$\Phi$</span> = <span class="rp-cyan">$\theta$</span> = <span class="rp-magenta">$\Psi$</span> = 15° 时，<span class="rp-olive">$\Phi$</span> 轴仍与 <span class="rp-blue">$Z$</span> 重合，但 <span class="rp-cyan">$\theta$</span> 轴和 <span class="rp-magenta">$\Psi$</span> 轴通常与 <span class="rp-red">$X$</span>、<span class="rp-green">$Y$</span>、<span class="rp-blue">$Z$</span> 中的任何一个都不一致。

> **旋转几何**窗口可以将此取向用任意的、特定于实验的欧拉角约定重新表示（例如，以匹配实验室的测角仪）。参见 [4. 旋转几何](../../4-rotation-geometry.md)。
