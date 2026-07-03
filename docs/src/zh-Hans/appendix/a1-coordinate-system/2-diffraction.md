# 附录 A1.2. 衍射模拟所用的坐标系

<!-- 260526Cl: 図(Coordinates4-5)に合わせ座標記号を数式(MathJax)化・用語色を図に一致 (.rp-*: docs/src/assets/stylesheets/extra.css)。 -->

**衍射模拟器** 功能用于模拟记录在探测器上的衍射图样。探测器是由像素构成的有限平面，放置在与样品保持固定距离处，并且可以相对于入射束发生倾斜。要准确再现这一点，需要探测器与样品之间的几何关系，以及探测器的像素尺寸和像素数。关于基本（取向）坐标系，请参阅 [A1.1. 基本坐标系与晶体取向](1-orientation.md)。

!!! note "Z 和 Y 与取向坐标系不同"
    在探测器坐标系中，<span class="rp-steel">$Z$</span> 平行于束流，<span class="rp-steel">$Y$</span> 指向下方。这与取向坐标系不同，在取向坐标系中束流沿 <span class="rp-blue">$-Z$</span> 方向，且 <span class="rp-green">$Y$</span> 指向上方。探测器坐标系遵循常见的图像／探测器约定（原点位于左上角，<span class="rp-steel">$Y$</span> 向下递增）。

## 旋转前（探测器垂直于束流）

![探测器垂直于束流时的探测器坐标系](../../../assets/references/Coordinates4.png){width=500px}

定义了三个坐标系：

- <span class="rp-steel">**实坐标** ($X$, $Y$, $Z$)</span> ：以 mm 为单位的三维笛卡尔坐标，以<span class="rp-steel">**样品**</span>为原点。<span class="rp-steel">$Z$</span> 平行于束流；沿 <span class="rp-steel">$Z$</span> 方向观察时，<span class="rp-steel">$X$</span> 指向右，<span class="rp-steel">$Y$</span> 指向下。当探测器垂直于束流时，<span class="rp-steel">$X$ / $Y$</span> 平行于 <span class="rp-brown">$X'$ / $Y'$</span>。
- <span class="rp-brown">**探测器坐标** ($X'$, $Y'$)</span> ：探测器平面上以 mm 为单位的二维坐标，以 <span class="rp-brown">**foot**</span> 为原点。<span class="rp-brown">$X'$ / $Y'$</span> 在探测器上分别指向右 / 下，并平行于 <span class="rp-cyan">$X''$ / $Y''$</span>。
- <span class="rp-cyan">**像素坐标** ($X''$, $Y''$)</span> ：以像素为单位的二维坐标，以探测器的<span class="rp-cyan">**左上角**</span>为原点，沿探测器的像素行列排布。

当探测器垂直于束流时，<span class="rp-brown">**foot**</span> 与<span class="rp-red">**direct spot**</span>重合，且 <span class="rp-red">**Camera length 1**</span> 等于 <span class="rp-brown">**Camera length 2**</span>。

## 旋转后（探测器倾斜）

![探测器倾斜时的探测器坐标系](../../../assets/references/Coordinates5.png){width=500px}

探测器的倾斜由两个参数描述：

| 参数 | 说明 |
|-----------|-------------|
| <span class="rp-grass">$\varphi$</span> | <span class="rp-grass">旋转轴</span>的方向——它与 <span class="rp-steel">$X$</span> 轴的夹角，在 <span class="rp-steel">$XY$</span>（<span class="rp-steel">$Z$</span> = 0）平面内测量 |
| <span class="rp-grass">$\tau$</span> | 绕该轴的旋转角（右手螺旋） |

一旦探测器发生倾斜：

- <span class="rp-red">**direct spot**</span> 与 <span class="rp-brown">**foot**</span> 不再重合。
- <span class="rp-red">**Camera length 1** ($C_1$)</span> = 从<span class="rp-steel">样品</span>到 <span class="rp-red">direct spot</span> 的距离。
- <span class="rp-brown">**Camera length 2** ($C_2$)</span> = 从<span class="rp-steel">样品</span>到 <span class="rp-brown">foot</span> 的距离。
- <span class="rp-brown">**探测器坐标**</span>的原点始终位于 <span class="rp-brown">**foot**</span>；<span class="rp-cyan">**像素坐标**</span>的原点始终位于<span class="rp-cyan">**左上角**</span>。
- <span class="rp-steel">$X$ / $Y$</span> 方向不再与 <span class="rp-brown">$X'$ / $Y'$</span> 重合。

## 参数表

| 术语 | 定义 |
|------|------------|
| <span class="rp-steel">**样品 (Sample)**</span> | 散射入射束的物质；实坐标的原点 |
| <span class="rp-steel">**实坐标** ($X$, $Y$, $Z$)</span> | 实验装置的三维坐标 (mm)；原点位于样品，<span class="rp-steel">$Z$</span> 始终平行于束流 |
| <span class="rp-red">**Direct spot**</span> | 入射束与探测器的交点 |
| <span class="rp-brown">**Foot**</span> | 从样品向探测器平面所作垂线的垂足；探测器坐标的原点。仅当探测器垂直于束流时才与 direct spot 重合。在叠加图像模式下，foot 的位置以像素坐标设定 |
| <span class="rp-brown">**探测器坐标** ($X'$, $Y'$)</span> | 探测器平面上的二维坐标 (mm)；原点位于 foot |
| <span class="rp-cyan">**像素坐标** ($X''$, $Y''$)</span> | 探测器平面上的二维坐标（像素）；原点位于左上角 |
| <span class="rp-red">**Camera length 1** ($C_1$)</span> | 从样品到 direct spot 的距离 (mm) |
| <span class="rp-brown">**Camera length 2** ($C_2$)</span> | 从样品到 foot 的距离 (mm) |
| **Pixel size** | 单个（正方形）像素的边长 (mm)；仅支持正方形像素 |
| **Detector width / height** | 水平 / 垂直方向的像素数 |
