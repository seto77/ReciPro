# 付録 A2. ビーム相互作用(固体物理的背景)

本編 [3. ビーム相互作用](../../3-beam-interaction.md) は GUI の操作マニュアルで、どのボタンを押すか、各列が何を意味するかを説明します。この付録は、それらの数値の背後にある **固体物理・散乱理論** をまとめます — なぜ X線・電子線・中性子線で原子の散乱の仕方がこれほど違うのか、構造因子とその虚部はどこから来るのか、ビームが固体中でどのように減衰し減速するのか、そして蛍光プレビューが何を表し何を表さないのか。

![ビーム相互作用ウィンドウ](../../../assets/cap-ja-auto/FormBeamInteraction.png)

ウィンドウには4つのタブがあり、理論は「ある量が次の量を決める」依存順に読むのが自然です。

1. **[原子散乱因子](scattering-factor.md)** — *1個の原子* が各ビームをどう散乱するか。
2. **[構造因子](structure-factor.md)** — *単位胞内* の原子がどう干渉するか(Debye–Waller 因子・消滅則を含む)。
3. **[減衰・輸送](attenuation-transport.md)** — ビームが物質中を進むにつれてどう *除去・減速* されるか。
4. **[蛍光](fluorescence.md)** — 内殻電離に続いて起こる特性X線発光。

---

## 散乱の幾何と変数 $s$

このウィンドウの散乱量はすべて、ビームの向きがどれだけ変わるかの関数です。入射・散乱波数ベクトルを $\mathbf k_i$, $\mathbf k_s$(弾性散乱なので $|\mathbf k_i|=|\mathbf k_s|=1/\lambda$)とすると、**散乱ベクトル** とその大きさは

$$\mathbf Q = 2\pi(\mathbf k_s - \mathbf k_i), \qquad Q = |\mathbf Q| = \frac{4\pi\sin\theta}{\lambda} = 4\pi s .$$

- $\theta$ : Bragg 角 — 全散乱角の *半分*。反射表に並ぶのは全角 $2\theta$ です。
- $s = \dfrac{\sin\theta}{\lambda}$(Å⁻¹) : **散乱因子** タブの横軸。あらゆる原子形状因子の自然な引数です。
- $d$ : 面間隔。Bragg 条件 $\lambda = 2d\sin\theta$ より $s = \dfrac{1}{2d} = \dfrac{|\mathbf g|}{2}$。ここで $\mathbf g$ は逆格子ベクトルで $|\mathbf g| = 1/d$。

3つの表し方は同じ幾何を指し、スケールが違うだけです。ウィンドウは複数を併用するので、対応を整理しておきます。

| ウィンドウ上の量 | 記号 | 関係 |
|---|---|---|
| 反射表 | $q = 2\pi/d$ | $q = 2\pi\lvert\mathbf g\rvert = Q = 4\pi s$ |
| 反射表 | $2\theta$ | 全散乱角、$\sin\theta = \lambda s$ |
| 散乱因子タブ | $s = \sin\theta/\lambda$ | $s = q/4\pi = 1/(2d)$ |
| 回折ピーク図 | $Q = 4\pi\sin\theta/\lambda$ | $Q = q = 4\pi s$ |

!!! note "単位"
    形状因子の公表されたパラメータ化は $s$ を Å⁻¹(したがって $s^2$ を Å⁻²)で扱いますが、ReciPro は内部的に $s^2$ を nm⁻² で保持します(両者は $s^2$ で 100 倍異なる)。曲線・表は各表のヘッダに記された単位で表示されます。1つのモデル — **Kirkland** — だけは $s$ ではなく $q = 2s = 1/d$ に対して与えられています([原子散乱因子](scattering-factor.md) 参照)。

### Bragg・Laue・Ewald 球

Bragg 条件は、1つの幾何学的要請の一面にすぎません。建設的干渉(**Laue 条件**)は、散乱ベクトルが逆格子ベクトルに等しいことを要求します。

$$\mathbf k_s = \mathbf k_i + \mathbf g, \qquad |\mathbf k_i + \mathbf g|^2 = |\mathbf k_i|^2 .$$

$|\mathbf k_i|=|\mathbf k_s|=1/\lambda$ を使うと、これは

$$2\,\mathbf k_i\cdot\mathbf g + |\mathbf g|^2 = 0 \qquad\Longleftrightarrow\qquad |\mathbf g| = \frac{1}{d} = \frac{2\sin\theta}{\lambda},$$

すなわち **Bragg の法則** $\lambda = 2d\sin\theta$ に帰着します。幾何的にはこれが **Ewald 球** の構成で、逆格子点が半径 $1/\lambda$ の球面上に乗ったときに反射が励起されます。(ここで $\mathbf g$ は $1/d$ 単位なので $\mathbf Q = 2\pi\mathbf g$。)

---

## 位相規約

ReciPro は結晶学の位相規約

$$F_{\mathbf g} = \sum_j \dots \exp\!\left(-2\pi i\,\mathbf g\cdot\mathbf r_j\right)$$

すなわち指数部に **マイナス** 符号を持つ形で構造因子を組み立てます。この選択が、構造因子の虚部(反射表の `F_inv`)の符号と、異常分散を有効にしたときの Friedel 対の関係を決めます。本付録では一度ここで述べ、以降は前提とします(帰結は [構造因子](structure-factor.md) で展開します)。

---

## 運動学的散乱と動力学的散乱

この付録は **単一(運動学的)散乱** を扱います。入射ビームは1回だけ散乱し、回折振幅は次ページの構造因子です。これは相互作用が弱いとき — ほとんどの試料での X線・中性子線、そして *十分に薄い* 試料での電子線 — に正しい描像です。

相互作用が強いとき — 最も薄い場合を除く結晶中の電子線 — ビームは出るまでに何度も散乱し、強度が反射間で再分配され、$\lvert F\rvert^2$ はもはや測定強度を与えません。この領域には [付録 A3](../a3-bloch-wave/index.md) の **動力学** 理論が必要です。ここで導く散乱因子・構造因子は、どちらの描像でも *入力* になります。

運動学的極限でも、回折振幅は構造因子だけでは決まりません。厚さ $t$ のスラブを通して散乱波を足し合わせると

$$A_{\mathbf g}(t) \;\propto\; F_{\mathbf g}\int_0^t e^{\,2\pi i S_{\mathbf g} z}\,dz = F_{\mathbf g}\, t\, e^{\,\pi i S_{\mathbf g} t}\,\operatorname{sinc}(\pi S_{\mathbf g} t)$$

となります。ここで $S_{\mathbf g}$ は **励起誤差**(逆格子点と Ewald 球との距離)です。強度は $S_{\mathbf g}=0$ で鋭くピークし、厚さとともに振動します(厚さ縞の起源)。[付録 A3](../a3-bloch-wave/index.md) の動力学理論は、この単一ビームの結果を結合ビームの振る舞いに置き換えます。

---

## 3つのプローブの比較

| | X線 | 電子線 | 中性子線 |
|---|---|---|---|
| 相互作用する相手 | 電子密度 $\rho_e$ | 静電ポテンシャル $V$ | 原子核(と不対スピン) |
| 相互作用の強さ | 弱い | 強い | 非常に弱い |
| 典型的な侵入深さ | µm – mm | nm – µm | mm – cm |
| 単一散乱は有効か | ほぼ常に | 薄膜のみ | ほぼ常に |
| 軽元素への感度 | 低い($\propto Z$) | 中程度 | しばしば優れる |

これらの対比は以降の各ページに繰り返し現れ、いずれも [原子散乱因子](scattering-factor.md) の散乱機構にさかのぼれます。

---

## 関連項目

- [3. ビーム相互作用](../../3-beam-interaction.md) — この付録が解説する GUI。
- [原子散乱因子](scattering-factor.md) ・ [構造因子](structure-factor.md) ・ [減衰・輸送](attenuation-transport.md) ・ [蛍光](fluorescence.md)
- [付録 A1. 座標系の定義](../a1-coordinate-system/1-orientation.md)
- [付録 A3. 動力学回折(Bloch波法)](../a3-bloch-wave/index.md) — これらの散乱因子を用いる多重散乱理論。
