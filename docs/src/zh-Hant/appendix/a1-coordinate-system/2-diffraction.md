# 附錄 A1.2. 繞射模擬所用的座標系

<!-- 260526Cl: 図(Coordinates4-5)に合わせ座標記号を数式(MathJax)化・用語色を図に一致 (.rp-*: docs/src/assets/stylesheets/extra.css)。 -->

**繞射模擬器** 功能用於模擬記錄在偵測器上的繞射圖樣。偵測器是由像素構成的有限平面，放置在與試樣保持固定距離處，並且可以相對於入射束發生傾斜。要準確再現這一點，需要偵測器與試樣之間的幾何關係，以及偵測器的像素尺寸和像素數。關於基本（取向）座標系，請參閱 [A1.1. 基本座標系與晶體取向](1-orientation.md)。

!!! note "Z 和 Y 與取向座標系不同"
    在偵測器座標系中，<span class="rp-steel">$Z$</span> 平行於束流，<span class="rp-steel">$Y$</span> 指向下方。這與取向座標系不同，在取向座標系中束流沿 <span class="rp-blue">$-Z$</span> 方向，且 <span class="rp-green">$Y$</span> 指向上方。偵測器座標系遵循常見的影像／偵測器約定（原點位於左上角，<span class="rp-steel">$Y$</span> 向下遞增）。

## 旋轉前（偵測器垂直於束流）

![偵測器垂直於束流時的偵測器座標系](../../../assets/references/Coordinates4.png){width=500px}

定義了三個座標系：

- <span class="rp-steel">**實座標** ($X$, $Y$, $Z$)</span> ：以 mm 為單位的三維笛卡兒座標，以<span class="rp-steel">**試樣**</span>為原點。<span class="rp-steel">$Z$</span> 平行於束流；沿 <span class="rp-steel">$Z$</span> 方向觀察時，<span class="rp-steel">$X$</span> 指向右，<span class="rp-steel">$Y$</span> 指向下。當偵測器垂直於束流時，<span class="rp-steel">$X$ / $Y$</span> 平行於 <span class="rp-brown">$X'$ / $Y'$</span>。
- <span class="rp-brown">**偵測器座標** ($X'$, $Y'$)</span> ：偵測器平面上以 mm 為單位的二維座標，以 <span class="rp-brown">**foot**</span> 為原點。<span class="rp-brown">$X'$ / $Y'$</span> 在偵測器上分別指向右 / 下，並平行於 <span class="rp-cyan">$X''$ / $Y''$</span>。
- <span class="rp-cyan">**像素座標** ($X''$, $Y''$)</span> ：以像素為單位的二維座標，以偵測器的<span class="rp-cyan">**左上角**</span>為原點，沿偵測器的像素行列排布。

當偵測器垂直於束流時，<span class="rp-brown">**foot**</span> 與<span class="rp-red">**direct spot**</span>重合，且 <span class="rp-red">**Camera length 1**</span> 等於 <span class="rp-brown">**Camera length 2**</span>。

## 旋轉後（偵測器傾斜）

![偵測器傾斜時的偵測器座標系](../../../assets/references/Coordinates5.png){width=500px}

偵測器的傾斜由兩個參數描述：

| 參數 | 說明 |
|-----------|-------------|
| <span class="rp-grass">$\varphi$</span> | <span class="rp-grass">旋轉軸</span>的方向——它與 <span class="rp-steel">$X$</span> 軸的夾角，在 <span class="rp-steel">$XY$</span>（<span class="rp-steel">$Z$</span> = 0）平面內測量 |
| <span class="rp-grass">$\tau$</span> | 繞該軸的旋轉角（右手螺旋） |

一旦偵測器發生傾斜：

- <span class="rp-red">**direct spot**</span> 與 <span class="rp-brown">**foot**</span> 不再重合。
- <span class="rp-red">**Camera length 1** ($C_1$)</span> = 從<span class="rp-steel">試樣</span>到 <span class="rp-red">direct spot</span> 的距離。
- <span class="rp-brown">**Camera length 2** ($C_2$)</span> = 從<span class="rp-steel">試樣</span>到 <span class="rp-brown">foot</span> 的距離。
- <span class="rp-brown">**偵測器座標**</span>的原點始終位於 <span class="rp-brown">**foot**</span>；<span class="rp-cyan">**像素座標**</span>的原點始終位於<span class="rp-cyan">**左上角**</span>。
- <span class="rp-steel">$X$ / $Y$</span> 方向不再與 <span class="rp-brown">$X'$ / $Y'$</span> 重合。

## 參數表

| 術語 | 定義 |
|------|------------|
| <span class="rp-steel">**試樣 (Sample)**</span> | 散射入射束的物質；實座標的原點 |
| <span class="rp-steel">**實座標** ($X$, $Y$, $Z$)</span> | 實驗裝置的三維座標 (mm)；原點位於試樣，<span class="rp-steel">$Z$</span> 始終平行於束流 |
| <span class="rp-red">**Direct spot**</span> | 入射束與偵測器的交點 |
| <span class="rp-brown">**Foot**</span> | 從試樣向偵測器平面所作垂線的垂足；偵測器座標的原點。僅當偵測器垂直於束流時才與 direct spot 重合。在疊加影像模式下，foot 的位置以像素座標設定 |
| <span class="rp-brown">**偵測器座標** ($X'$, $Y'$)</span> | 偵測器平面上的二維座標 (mm)；原點位於 foot |
| <span class="rp-cyan">**像素座標** ($X''$, $Y''$)</span> | 偵測器平面上的二維座標（像素）；原點位於左上角 |
| <span class="rp-red">**Camera length 1** ($C_1$)</span> | 從試樣到 direct spot 的距離 (mm) |
| <span class="rp-brown">**Camera length 2** ($C_2$)</span> | 從試樣到 foot 的距離 (mm) |
| **Pixel size** | 單個（正方形）像素的邊長 (mm)；僅支援正方形像素 |
| **Detector width / height** | 水平 / 垂直方向的像素數 |
