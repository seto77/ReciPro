# 付録 A1.1. 基本座標系と結晶方位の定義

<!-- 260526Cl: 図(Coordinates1-3)に合わせ座標記号を数式(MathJax)化・用語色を図に一致 (.rp-*: docs/src/assets/stylesheets/extra.css)。 -->

このページでは、結晶の回転を扱うすべての場面（メインウィンドウ、Structure Viewer、ステレオネット、Rotation Geometry、回折シミュレーション）で使われる ReciPro の **基本（方位）座標系** と、結晶の初期方位・オイラー角による回転の表し方を定義します。**Crystal Diffraction** で検出器を配置する際に使う別の座標系は [A1.2. 回折シミュレーションにおける座標系の定義](2-diffraction.md) を参照してください。

---

## 方位の定義

ReciProはモニターに固定した**右手系座標系**を採用しています。

| 軸 | 方向 |
|----|------|
| <span class="rp-red">$X$</span> | モニター面の右方向 |
| <span class="rp-green">$Y$</span> | モニター面の上方向 |
| <span class="rp-blue">$Z$</span> | モニター面の垂直手前方向（視点に向かう方向） |

![モニター上に示したReciProの座標軸](../../../assets/references/Coordinates1.png)

**ビーム方向**は視線方向（モニターを見つめる方向）に対応し、<span class="rp-blue">$-Z$</span>軸方向です。

ReciProで行う演算のほとんどは*方向*（すなわち3×3の回転行列）だけが意味を持ち、原点の位置を意識する必要はありません。唯一の例外が **Crystal Diffraction** 機能で、ここでは原点位置を明示的に考慮する必要があります（[A1.2. 回折シミュレーションにおける座標系の定義](2-diffraction.md) を参照）。

## 結晶の初期方位

初期状態（初回起動時、または **Reset rotation** を押した後）の方位は次のように定義されます。

1. <span class="rp-blue">$c$</span>軸が<span class="rp-blue">$Z$</span>軸方向に一致
2. <span class="rp-green">$b$</span>軸が<span class="rp-green">$Y$</span><span class="rp-blue">$Z$</span>平面上にあり、<span class="rp-green">$Y$</span>軸に近い方向
3. <span class="rp-red">$a$</span>軸は<span class="rp-green">$b$</span>軸・<span class="rp-blue">$c$</span>軸から決定（右手の法則）

![初期方位：結晶の a / b / c 軸と X / Y / Z 軸の関係。入射ビームは −Z 方向](../../../assets/references/Coordinates2.png)

言い換えると：

- モニター手前方向（視点に向かう方向）= **[001]** 晶帯軸
- モニター右方向 = **(100)** 結晶面の法線方向

> **注意**: <span class="rp-blue">$c$</span>軸（= [001] 晶帯軸）は必ず<span class="rp-blue">$Z$</span>軸に一致しますが、結晶系によっては <span class="rp-red">$a$</span>軸・<span class="rp-green">$b$</span>軸 は必ずしも<span class="rp-red">$X$</span>軸・<span class="rp-green">$Y$</span>軸に一致しません。

## オイラー角

結晶方位は3つのオイラー角 <span class="rp-olive">$\Phi$</span>、<span class="rp-cyan">$\theta$</span>、<span class="rp-magenta">$\Psi$</span> で表現し、<span class="rp-blue">$Z$</span>–<span class="rp-red">$X$</span>–<span class="rp-blue">$Z$</span> の順（<span class="rp-magenta">$\Psi$</span> → <span class="rp-cyan">$\theta$</span> → <span class="rp-olive">$\Phi$</span>）に作用させます。すべての角度がゼロのとき、各回転軸は以下に対応します。

| 角度 | 回転軸（全角度=0のとき） | 順位 |
|------|--------------------------|------|
| <span class="rp-olive">$\Phi$</span> | <span class="rp-blue">$Z$</span> | 1st（最上位） |
| <span class="rp-cyan">$\theta$</span> | <span class="rp-red">$X$</span> | 2nd（中位） |
| <span class="rp-magenta">$\Psi$</span> | <span class="rp-blue">$Z$</span> | 3rd（最下位） |

![オイラー角の回転軸 — Φ（黄）・θ（水色）・Ψ（マゼンタ）。上が 0°、下が 15° の状態](../../../assets/references/Coordinates3.png)

3つのオイラー角には**主従関係（階層）**があります。<span class="rp-olive">$\Phi$</span>が最上位、次に<span class="rp-cyan">$\theta$</span>、最下位が<span class="rp-magenta">$\Psi$</span>です。下位の回転軸の方向は、上位の回転の状態によって変化します。例えば <span class="rp-olive">$\Phi$</span> = <span class="rp-cyan">$\theta$</span> = <span class="rp-magenta">$\Psi$</span> = 15° のとき、<span class="rp-olive">$\Phi$</span>の回転軸は<span class="rp-blue">$Z$</span>軸と一致しますが、<span class="rp-cyan">$\theta$</span>と<span class="rp-magenta">$\Psi$</span>の回転軸は一般に<span class="rp-red">$X$</span>, <span class="rp-green">$Y$</span>, <span class="rp-blue">$Z$</span>のいずれとも一致しません。

> **Rotation Geometry** ウィンドウを使うと、この方位を任意の（実験固有の）オイラー角定義で表現し直せます（例: 実験室のゴニオメーターに合わせる）。詳しくは [4. 回転ジオメトリ](../../4-rotation-geometry.md) を参照してください。
