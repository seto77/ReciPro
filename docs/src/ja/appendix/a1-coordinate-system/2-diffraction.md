<!-- 260526Cl: 図(Coordinates4-5)に合わせ座標記号を数式(MathJax)化・用語色を図に一致 (.rp-*: docs/src/assets/stylesheets/extra.css)。 -->
# 付録 A1.2. 回折シミュレーションにおける座標系の定義

**Crystal Diffraction** 機能は、検出器上に写る回折パターンをシミュレーションします。検出器はピクセルの集合からなる有限サイズの平面で、試料から一定の距離に置かれ、入射ビームに対して傾いている場合もあります。これを正確に再現するには、検出器と試料の幾何学的関係に加え、検出器のピクセルサイズ・ピクセル数の情報が必要です。基本（方位）座標系については [A1.1. 基本座標系と結晶方位の定義](1-orientation.md) を参照してください。

!!! note "ZとYは方位の座標系とは異なる"
    検出器の座標系では、<span class="rp-steel">$Z$</span>軸はビーム方向に平行で、<span class="rp-steel">$Y$</span>軸は下方向です。これは方位の座標系（ビーム= <span class="rp-blue">$-Z$</span>、<span class="rp-green">$Y$</span>= 上方向）とは異なることに注意してください。検出器の座標系は、画像・検出器で一般的な慣習（原点が左上、<span class="rp-steel">$Y$</span>は下向きに増加）に従います。

## 回転前（検出器がビームに垂直）

![検出器がビームに垂直な場合の座標系](../../../assets/references/Coordinates4.png)

3つの座標系を定義します。

- <span class="rp-steel">**実座標** ($X$, $Y$, $Z$)</span> — mm単位の3次元直交座標。<span class="rp-steel">**試料**</span>を原点とする。<span class="rp-steel">$Z$</span>軸はビーム方向に平行で、<span class="rp-steel">$Z$</span>軸方向を正面に見て <span class="rp-steel">$X$</span> は右、<span class="rp-steel">$Y$</span> は下を向く。検出器がビームに垂直のとき、<span class="rp-steel">$X$ / $Y$</span> は <span class="rp-brown">$X'$ / $Y'$</span> に平行。
- <span class="rp-brown">**検出器座標** ($X'$, $Y'$)</span> — 検出器平面上のmm単位の2次元座標。<span class="rp-brown">**foot**</span> を原点とする。<span class="rp-brown">$X'$ / $Y'$</span> は検出器平面上で右 / 下を向き、<span class="rp-cyan">$X''$ / $Y''$</span> に平行。
- <span class="rp-cyan">**ピクセル座標** ($X''$, $Y''$)</span> — ピクセル単位の2次元座標。検出器の<span class="rp-cyan">**左上隅**</span>を原点とし、検出器のピクセル配列に沿う。

検出器がビームに垂直なとき、<span class="rp-brown">**foot**</span> と<span class="rp-red">**透過スポット**</span>は一致し、<span class="rp-red">**Camera length 1**</span> と <span class="rp-brown">**Camera length 2**</span> は等しくなります。

## 回転後（検出器が傾いた場合）

![検出器が傾いた場合の座標系](../../../assets/references/Coordinates5.png)

検出器の傾きは2つのパラメータで表現します。

| パラメータ | 説明 |
|-----------|------|
| <span class="rp-grass">$\varphi$</span> | <span class="rp-grass">回転軸</span>の方向。<span class="rp-steel">$XY$</span>平面（<span class="rp-steel">$Z$</span> = 0 平面）上で、<span class="rp-steel">$X$</span>軸から測った角度 |
| <span class="rp-grass">$\tau$</span> | その軸まわりの回転角（右ネジの方向） |

検出器が傾くと：

- <span class="rp-red">**透過スポット**</span>と <span class="rp-brown">**foot**</span> は一致しなくなる。
- <span class="rp-red">**Camera length 1** ($C_1$)</span> = <span class="rp-steel">試料</span>から<span class="rp-red">透過スポット</span>までの距離。
- <span class="rp-brown">**Camera length 2** ($C_2$)</span> = <span class="rp-steel">試料</span>から <span class="rp-brown">foot</span> までの距離。
- <span class="rp-brown">**検出器座標**</span>の原点は常に <span class="rp-brown">**foot**</span>、<span class="rp-cyan">**ピクセル座標**</span>の原点は常に<span class="rp-cyan">**左上隅**</span>。
- <span class="rp-steel">$X$ / $Y$</span> 方向は <span class="rp-brown">$X'$ / $Y'$</span> 方向と一致しなくなる。

## パラメータ一覧

| 用語 | 定義 |
|------|------|
| <span class="rp-steel">**Sample（試料）**</span> | 入射ビームを散乱する物質。実座標の原点 |
| <span class="rp-steel">**実座標** ($X$, $Y$, $Z$)</span> | 実験系のmm単位の3次元座標。原点は試料、<span class="rp-steel">$Z$</span>軸は常にビーム方向に平行 |
| <span class="rp-red">**透過スポット (Direct spot)**</span> | 入射ビームと検出器の交点 |
| <span class="rp-brown">**Foot**</span> | 試料から検出器平面に下ろした垂線の足。検出器座標の原点。検出器がビームに垂直なときのみ透過スポットと一致する。重ね合わせ画像モードでは foot の位置をピクセル座標で設定する |
| <span class="rp-brown">**検出器座標** ($X'$, $Y'$)</span> | 検出器平面上のmm単位の2次元座標。原点は foot |
| <span class="rp-cyan">**ピクセル座標** ($X''$, $Y''$)</span> | 検出器平面上のピクセル単位の2次元座標。原点は左上隅 |
| <span class="rp-red">**Camera length 1** ($C_1$)</span> | 試料から透過スポットまでの距離 (mm) |
| <span class="rp-brown">**Camera length 2** ($C_2$)</span> | 試料から foot までの距離 (mm) |
| **Pixel size** | 1ピクセルの一辺の長さ (mm)。正方ピクセルのみ対応 |
| **Detector width / height** | 水平 / 垂直方向のピクセル数 |
