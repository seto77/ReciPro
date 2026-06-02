# 動力学計算（共通コア）

ReciPro の回折・結像シミュレータは、共通の **Bloch 波（Bethe）動力学散乱コア**を土台にしています（結晶ポテンシャル、Debye–Waller・吸収項、固有値問題、透過係数、強度）。このページでその共通コアを説明します。各手法固有のプロトコルは、この土台の上に構築されます。

- [平行ビーム SAED](#parallel-beam-saed)
- [HRTEM 像形成](hrtem.md)
- [CBED](cbed.md)
- [STEM](stem.md)
- [EBSD](ebsd.md)

基礎理論（シュレーディンガー方程式、Bloch の定理、Bethe の動力学方程式、固有値問題、エワルド球上の定義）は [付録 A2. Bloch波法による動力学計算の概要](index.md) を参照してください。

---

## 定数

$$\gamma = \frac{m}{m_0} = 1 + \frac{e_0 E}{m_0 c^2}, \qquad \beta = \frac{v}{c} = \sqrt{1 - \left(\frac{m_0}{m}\right)^2} = \sqrt{1 - \gamma^{-2}}$$

- $\gamma$ : 相対論補正係数。$E$ : 加速電圧。$m_0$, $m$ : 電子の静止質量・相対論的質量。
- $\Omega$ : 単位胞の体積。
- $k_{vac}$ : 真空中での電子の波数。

---

## 弾性散乱に対する結晶ポテンシャル

弾性散乱に対する結晶ポテンシャルのフーリエ係数は、位置 $\mathbf r_k$ の各原子 $k$ について和をとり、

$$U_{\mathbf g}^{C} = \gamma\,\frac{1}{\pi\Omega}\sum_k f_k(\mathbf g)\,\exp\!\left[2\pi i\,\mathbf g\cdot\mathbf r_k\right]T_k(\mathbf g, M_k)$$

ここで **原子散乱因子** はガウス関数のパラメータ $(a_i, b_i)$ で表され、

$$f_k(\mathbf g) = \sum_i a_i\exp\!\left[-b_i\,\frac{|\mathbf g|^2}{4}\right]$$

$T_k$ は **Debye–Waller（温度）因子** です。等方的な温度因子 $M_k$ の場合は

$$T_k(\mathbf g, M_k) = \exp\!\left[-M_k\,\frac{|\mathbf g|^2}{4}\right]$$

異方的な原子変位テンソル $\mathbf U$ の場合は

$$T_k(\mathbf g) = \exp\!\left[-2\pi\,\mathbf g^{t}\mathbf U\,\mathbf g\right]$$

であり、二次形式は

$$\mathbf g^{t}\mathbf U\,\mathbf g = \begin{pmatrix} g_x & g_y & g_z\end{pmatrix}\begin{pmatrix} U_{11} & U_{12} & U_{13}\\ U_{12} & U_{22} & U_{23}\\ U_{13} & U_{23} & U_{33}\end{pmatrix}\begin{pmatrix} g_x\\ g_y\\ g_z\end{pmatrix} = g_x^2 U_{11} + g_y^2 U_{22} + g_z^2 U_{33} + 2\!\left(g_x g_y U_{12} + g_y g_z U_{23} + g_x g_z U_{13}\right)$$

です。$\mathbf g$ のデカルト成分は、逆格子基本ベクトルとミラー指数から得られます。

$$\begin{pmatrix} g_x\\ g_y\\ g_z\end{pmatrix} = \begin{pmatrix} a_x^{*} & b_x^{*} & c_x^{*}\\ a_y^{*} & b_y^{*} & c_y^{*}\\ a_z^{*} & b_z^{*} & c_z^{*}\end{pmatrix}\begin{pmatrix} h\\ k\\ l\end{pmatrix} = \begin{pmatrix} h\,a_x^{*} + k\,b_x^{*} + l\,c_x^{*}\\ h\,a_y^{*} + k\,b_y^{*} + l\,c_y^{*}\\ h\,a_z^{*} + k\,b_z^{*} + l\,c_z^{*}\end{pmatrix}$$

!!! note
    回折シミュレータの **Details** テーブルに表示される $U_{\mathbf g}$ は、相対論補正係数 $\gamma$ を掛ける前の数値です。

---

## 吸収ポテンシャル（熱散漫散乱）

熱散漫散乱（TDS）を表す虚（吸収）ポテンシャルは

$$U'_{g,h} = \gamma\,\frac{1}{\pi\Omega}\sum_k f'_k(\mathbf g,\mathbf h)\,\exp\!\left[2\pi i(\mathbf g-\mathbf h)\cdot\mathbf r_k\right]T_k(\mathbf g-\mathbf h, M_k)$$

であり、**吸収散乱因子** は

$$f'_k(\mathbf g,\mathbf h) = \frac{2h}{\beta\, m_0\, c}\sum_i\sum_j a_i a_j\left[\frac{1}{b_i+b_j}\exp\!\left\{-\frac{b_i b_j}{b_i+b_j}\,\frac{|\mathbf g-\mathbf h|^2}{4}\right\} - \frac{1}{b_i+b_j+2M_k}\exp\!\left\{-\frac{b_i b_j - M_k^2}{b_i+b_j+2M_k}\,\frac{|\mathbf g-\mathbf h|^2}{4}\right\}\right]$$

です。前係数 $2h/(\beta m_0 c)$ の $h$ は **プランク定数**（反射の指数 $h$ とは別）です。$U^{C}$ と $U'$ の係数が、[付録 A2](index.md) の構造行列 $\mathbf A$ の要素になります。

---

## 固有解から回折強度へ

構造行列を対角化すると（[付録 A2](index.md) 参照）、固有値 $\lambda^{(j)}$ と Bloch 波振幅 $C_{\mathbf g}^{(j)}$ が得られます。試料厚さ $t$ における出射面の波の振幅、すなわち **透過係数** $T_{\mathbf g}$ は

$$\begin{pmatrix} T_0\\ T_g\\ T_h\\ \vdots\end{pmatrix}
= e^{-2\pi i(\mathbf k_{vac}\cdot\mathbf n)\,t}
\begin{pmatrix} e^{\pi i P_0 t} & 0 & 0 & \cdots\\ 0 & e^{\pi i P_g t} & 0 & \cdots\\ 0 & 0 & e^{\pi i P_h t} & \cdots\\ \vdots & \vdots & \vdots & \ddots\end{pmatrix}
\begin{pmatrix} C_0^{(1)} & C_0^{(2)} & C_0^{(3)} & \cdots\\ C_g^{(1)} & C_g^{(2)} & C_g^{(3)} & \cdots\\ C_h^{(1)} & C_h^{(2)} & C_h^{(3)} & \cdots\\ \vdots & \vdots & \vdots & \ddots\end{pmatrix}
\begin{pmatrix} e^{2\pi i\lambda^{(1)} t} & 0 & 0 & \cdots\\ 0 & e^{2\pi i\lambda^{(2)} t} & 0 & \cdots\\ 0 & 0 & e^{2\pi i\lambda^{(3)} t} & \cdots\\ \vdots & \vdots & \vdots & \ddots\end{pmatrix}
\begin{pmatrix} \alpha^{(1)}\\ \alpha^{(2)}\\ \alpha^{(3)}\\ \vdots\end{pmatrix}$$

あるいは成分ごとに書くと、

$$T_{\mathbf g} = e^{-2\pi i(\mathbf k_{vac}\cdot\mathbf n)\,t}\; e^{\pi i P_g t}\sum_j C_{\mathbf g}^{(j)}\,e^{2\pi i\lambda^{(j)} t}\,\alpha^{(j)}$$

- $\alpha^{(j)}$ : 各 Bloch 波の重み（励起）係数。入射面での境界条件から定まります。
- $t$ : 試料厚さ。

反射 $\mathbf g$ の回折強度は

$$I_{\mathbf g} = \left|T_{\mathbf g}\right|^2$$

となります。

---

## 平行ビーム SAED の計算 { #parallel-beam-saed }

通常の SAED（Selected Area Electron Diffraction）は、入射方向が 1 本だけの **平行ビーム回折**として扱います。CBED のように絞り内の多数の $\mathbf K$ を走査せず、現在の結晶方位と加速電圧から 1 つの入射波数ベクトル $\mathbf k_0$ を決め、その条件で反射 $\mathbf g$ ごとの位置と強度を評価します。

計算の流れは次のように整理できます。

1. 結晶方位、加速電圧、波長、カメラ長、検出器幾何から入射波数 $\mathbf k_{vac}$ と検出器面を決める。
2. 平均内部ポテンシャル $U_0$ による屈折を考慮し、結晶中の参照波数 $\mathbf k_0$ を求める。
3. 候補となる逆格子ベクトル $\mathbf g$ を列挙し、$Q_g=|\mathbf k_0|^2-|\mathbf k_0+\mathbf g|^2$ や励起誤差 $S_g$ でエワルド球からのずれを評価する。
4. 反射ごとの強度を、選択した強度モードで計算する。
5. $\mathbf k_0+\mathbf g$ の方向を検出器面へ投影し、スポット位置として描画する。

ReciPro の SAED には、主に次の強度モードがあります。

| モード | 計算内容 | 主な用途 |
|--------|----------|----------|
| 励起誤差のみ | 反射がエワルド球にどれだけ近いかだけで強度を見積もる。構造因子は使わない。 | スポット位置や晶帯軸の幾何を素早く確認する。 |
| 運動学的理論 + 励起誤差 | $\lvert F_{\mathbf g}\rvert^2$ と励起誤差による減衰で強度を与える。多重散乱は扱わない。 | 薄い試料、弱い回折、消滅則の確認。 |
| 動力学的理論 | このページの Bloch 波コアで透過係数 $T_{\mathbf g}(t)$ を求め、$I_{\mathbf g}=\lvert T_{\mathbf g}\rvert^2$ とする。 | 厚さ依存、多重散乱、強い反射を含む電子回折。 |

「球の断面」「ガウス関数」などの逆格子点表示モードは、主にスポットの描画プロファイルを決める設定です。動力学的理論を選んだ場合、物理的な反射強度は Bloch 波計算の $|T_{\mathbf g}|^2$ で決まり、その強度が表示プロファイルへ割り当てられます。

PED はこの平行ビーム SAED を歳差方向に沿って積分したもの、CBED は絞り内の多数の入射方向をディスク内に並べたもの、と見ると関係が分かりやすくなります。

---

## 平均内部ポテンシャルと屈折

電子が真空から結晶へ入ると、平均内部ポテンシャル $U_0$ により結晶内の参照波数がわずかに変化します。表面に平行な成分は境界条件で保存されるため、真空中の入射波数 $\mathbf k_{vac}$ と結晶中の参照波数 $\mathbf k_0$ は

$$|\mathbf k_0|^2 = k_{vac}^2 + U_0, \qquad \mathbf k_0 = \mathbf k_{vac} + x\,\hat{\mathbf n}$$

と書けます。ここで $x$ は表面法線方向の補正量で、次の二次方程式から決まります。

$$x^2 + 2(\hat{\mathbf n}\cdot\mathbf k_{vac})x - U_0 = 0$$

この屈折補正を入れた $\mathbf k_0$ が、[概要ページ](index.md) の $P_g$、$Q_g$、励起誤差、そして構造行列 $\mathbf A$ の評価に使われます。吸収ポテンシャルにも $\mathbf g=\mathbf 0$ 成分 $U'_0$ があり、これは結晶中を進む波全体に共通する平均減衰として働きます。

---

## ビーム選択

Bloch 波計算では、無限個の逆格子ベクトルをそのまま扱うことはできないため、有限個の反射 $\{\mathbf g\}$ を選びます。ReciPro では、逆格子ベクトルの長さとエワルド球からのずれを合わせた評価量

$$R_{\mathbf g}=|\mathbf g|\,Q_{\mathbf g}^{\,2}$$

を用い、$R_{\mathbf g}$ の小さい反射から採用します。これは「短い逆格子ベクトルで、かつ励起条件に近い反射」を優先する選択です。

実用上は、最大 Bloch 波数を増やしたときに強度や像がどの程度変わるかを確認することが重要です。強いゾーン軸条件や HOLZ 線を含む CBED では、数百本程度の反射が必要になることがあります。一方、オフゾーン条件ではより少ない反射数で収束する場合もあります。

---

## ソルバーの選択

有限個の反射を選んだ後、ReciPro は主に二つの方法で透過係数を求めます。

| 方法 | 特徴 | 向いている計算 |
|------|------|----------------|
| 固有値法 | 構造行列 $\mathbf A$ を対角化し、固有値 $\lambda^{(j)}$ と固有ベクトル $C_{\mathbf g}^{(j)}$ を求める。厚さ依存は $e^{2\pi i\lambda^{(j)}t}$ の評価で済む。 | 厚さシリーズ、CBED、EBSD のように多数の厚さ・エネルギーを走査する計算 |
| 行列指数関数法 | 散乱行列 $\exp(2\pi i\mathbf A t)$ を直接評価する。固有値分解を明示的に使わない。 | 単一厚さの STEM 計算、薄いスライスを積分する計算 |

どちらの方法も同じ Bethe 方程式に基づく等価な解法です。実装では、ビーム数、厚さ配列の長さ、ネイティブ Eigen ライブラリの利用可否に応じて、固有値法・行列指数関数法・マネージド実装・ネイティブ実装を使い分けます。

---

## 収束確認

動力学計算では、数式よりも「十分な基底を入れたか」の確認が結果を左右します。ビーム数を $N-\Delta N$ から $N$ に増やしたときの相対変化

$$\Delta I_N=\frac{|I_N-I_{N-\Delta N}|}{I_N}$$

を見れば、強度や像が反射数に対して安定しているかを確認できます。STEM の場合は検出器角度、CBED の場合はディスク内の HOLZ 線、EBSD の場合は master pattern の菊池バンド幅や背景も同時に確認すると、数値収束と物理的な見え方を結び付けて判断できます。

---

## 関連項目

- [付録 A2. Bloch波法による動力学計算の概要](index.md)
- [7.2 SAEDシミュレーション](../../7-diffraction-simulator/1-saed-simulation.md)
- [7.4 CBEDシミュレーション](../../7-diffraction-simulator/3-cbed-simulation.md)
- [7. 回折シミュレータ](../../7-diffraction-simulator/index.md)
