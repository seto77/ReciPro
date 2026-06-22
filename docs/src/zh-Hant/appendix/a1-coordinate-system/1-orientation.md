# 附錄 A1.1. 基本座標系與晶體取向

<!-- 260526Cl: 図(Coordinates1-3)に合わせ座標記号を数式(MathJax)化・用語色を図に一致 (.rp-*: docs/src/assets/stylesheets/extra.css)。 -->

本頁定義 ReciPro 的**基本（取向）座標系**，它用於一切涉及晶體旋轉的場合（主視窗、結構檢視器、極網、旋轉幾何以及繞射模擬），並說明如何表示晶體的初始取向與歐拉角旋轉。在**繞射模擬器**中用於放置偵測器的另一套座標系，於 [A1.2. 繞射模擬的座標系](2-diffraction.md) 中描述。

---

## 取向的定義

ReciPro 採用固定於顯示器上的**右手座標系**：

| 軸 | 方向 |
|------|-----------|
| <span class="rp-red">$X$</span> | 顯示器的右方 |
| <span class="rp-green">$Y$</span> | 顯示器上的向上方向 |
| <span class="rp-blue">$Z$</span> | 垂直於顯示器向外、朝向觀察者 |

![顯示器上顯示的 ReciPro 座標軸](../../../assets/references/Coordinates1.png){width=400px}

**電子束方向**對應於觀察方向（朝顯示器內看），即 <span class="rp-blue">$-Z$</span> 軸。

ReciPro 中的大多數操作只涉及*方向*（以 3×3 旋轉矩陣表示），並不需要明確的原點。唯一的例外是**繞射模擬器**功能，它需要一個明確的原點——參見 [A1.2. 繞射模擬的座標系](2-diffraction.md)。

## 晶體的初始取向

初始取向（首次啟動時，或在**重設旋轉**之後）定義如下：

1. <span class="rp-blue">$c$</span> 軸與 <span class="rp-blue">$Z$</span> 軸對齊。
2. <span class="rp-green">$b$</span> 軸位於 <span class="rp-green">$Y$</span><span class="rp-blue">$Z$</span> 平面內，靠近 <span class="rp-green">$Y$</span> 軸。
3. <span class="rp-red">$a$</span> 軸隨後由 <span class="rp-green">$b$</span> 軸與 <span class="rp-blue">$c$</span> 軸確定（右手定則）。

![初始取向：晶體 a / b / c 軸相對於 X / Y / Z 的關係，入射束沿 −Z 方向](../../../assets/references/Coordinates2.png){width=300px}

等價地：

- 從顯示器向外（朝向觀察者）的方向是 **[001]** 晶帶軸。
- 顯示器上向右的方向是 **(100)** 晶面的法線。

> **注意：** <span class="rp-blue">$c$</span> 軸（= [001]）始終與 <span class="rp-blue">$Z$</span> 重合，但在某些晶系中 <span class="rp-red">$a$</span> 軸與 <span class="rp-green">$b$</span> 軸**不一定**與 <span class="rp-red">$X$</span> 和 <span class="rp-green">$Y$</span> 重合。

## 歐拉角

晶體取向用三個歐拉角 <span class="rp-olive">$\Phi$</span>、<span class="rp-cyan">$\theta$</span>、<span class="rp-magenta">$\Psi$</span> 表示，按 <span class="rp-blue">$Z$</span>–<span class="rp-red">$X$</span>–<span class="rp-blue">$Z$</span> 順序施加（先 <span class="rp-magenta">$\Psi$</span>，再 <span class="rp-cyan">$\theta$</span>，最後 <span class="rp-olive">$\Phi$</span>）。當三個角度全為零時，對應的旋轉軸為：

| 角度 | 軸（當所有角度 = 0 時） | 等級 |
|-------|----------------------------|------|
| <span class="rp-olive">$\Phi$</span> | <span class="rp-blue">$Z$</span> | 第 1（最高） |
| <span class="rp-cyan">$\theta$</span> | <span class="rp-red">$X$</span> | 第 2（中間） |
| <span class="rp-magenta">$\Psi$</span> | <span class="rp-blue">$Z$</span> | 第 3（最低） |

![歐拉角旋轉軸 — Φ（黃）、θ（青）、Ψ（洋紅）— 在 0°（上）和 15°（下）時所示](../../../assets/references/Coordinates3.png){width=400px}

這三個角度構成一個**層級**：<span class="rp-olive">$\Phi$</span> 是最高級的旋轉，其次是 <span class="rp-cyan">$\theta$</span>，再次是 <span class="rp-magenta">$\Psi$</span>。較低級軸的方向取決於較高級旋轉的狀態。例如，當 <span class="rp-olive">$\Phi$</span> = <span class="rp-cyan">$\theta$</span> = <span class="rp-magenta">$\Psi$</span> = 15° 時，<span class="rp-olive">$\Phi$</span> 軸仍與 <span class="rp-blue">$Z$</span> 重合，但 <span class="rp-cyan">$\theta$</span> 軸與 <span class="rp-magenta">$\Psi$</span> 軸通常與 <span class="rp-red">$X$</span>、<span class="rp-green">$Y$</span>、<span class="rp-blue">$Z$</span> 中的任何一個都不一致。

> **旋轉幾何**視窗可以將此取向用任意的、特定於實驗的歐拉角約定重新表示（例如，以匹配實驗室的測角儀）。參見 [4. 旋轉幾何](../../4-rotation-geometry.md)。
