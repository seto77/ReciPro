# ALCHEMI の計算

<!-- 261124Cl 追加 (作者の決定 2026-11-24 = ALCHEMI の式を理論の付録に集める)。7.4 の操作の頁にあった収量の式は、共役側の添字・単位胞体積 V_c での割り算・
     流束の重み P_g/P_0 が実装 (Crystallography/Diffraction/Alchemi/AlchemiReduction.cs) と違っていた (codex2 査読 M1-22・M1-23)。式はこの頁だけに置く。 -->

[7.4. ALCHEMI シミュレーション](../../7-diffraction-simulator/4-alchemi-simulation.md) が計算する量の定義です。結晶内の波動場は [動力学計算（共通コア）](calculation.md) と同じブロッホ波法で求めます。

## 波動場

入射方位ごとに、反射 $\mathbf g$ の振幅を

$$
\psi_{\mathbf g}(z) = \sum_j \alpha_j\, C_{\mathbf g}^{(j)}\, e^{2\pi i \gamma_j z}
$$

と展開します。$\gamma_j$ はブロッホ波の固有値、$C_{\mathbf g}^{(j)}$ は固有ベクトル、$\alpha_j$ は表面の境界条件で決まる励起振幅です。以下では $D = C\,\mathrm{diag}(\alpha)$、つまり $D_{\mathbf g j} = C_{\mathbf g}^{(j)}\alpha_j$ と書きます。

## イオン化行列

サイト $s$ とイオン化チャネル $c$ の組ごとに、イオン化行列 $\mu_{s,c}$ を作ります。成分は 2 つの反射の差 $\mathbf G = \mathbf g_h - \mathbf g_g$ だけで決まります。

$$
\mu_{hg} = \sum_{a \in s} \mathrm{Occ}_a\, e^{-M_a(\mathbf G)}\, \sigma_c\, F_c(|\mathbf G|/2)\, e^{-2\pi i\,\mathbf G \cdot \mathbf r_a}
$$

- $\sigma_c$ : イオン化全断面積 (**Bote–Salvat** モデル)
- $F_c(s)$ : 規格化イオン化形状因子 (**Temari** が生成した DHFS テーブル。[STEM-EDX](../../9-hrtem-stem-simulator/2-stem-simulation.md) と同じ)
- $e^{-M_a(\mathbf G)}$ : デバイ・ワラー因子 (非等方の原子変位にも対応)

X 線の線の系列では、$\sigma_c$ は副殻 $i$ についての $\sum_i B_i\sigma_i$、$F_c$ は副殻の形状因子の $B_i\sigma_i$ による重み付き平均です ($B_i$ は Coster–Kronig と放射の連鎖の後の蛍光収率 × 線の分岐比。xraylib 4.2.1)。これは ICSC (Oxley & Allen 2003) の**局所形状因子近似**に相当し、二変数の MDFF は使っていません。

## チャネリングによる収量

厚さ $t$ までのイオン化収量 (入射電子 1 個あたり) は、

$$
Y_\text{dyn}(t) = \frac{1}{V_c}\,\mathrm{Re}\sum_{jj'} \bigl(D^{\dagger}\mu_{s,c}D\bigr)_{j'j}\,F_{jj'}(t)
= \frac{1}{V_c}\,\mathrm{Re}\sum_{jj'} \alpha_{j'}^{*}\bigl(C^{\dagger}\mu_{s,c}C\bigr)_{j'j}\,\alpha_j\,F_{jj'}(t)
$$

$$
F_{jj'}(t) = \int_0^t e^{\lambda z}\,dz = \frac{e^{\lambda t}-1}{\lambda},
\qquad \lambda = 2\pi i\,\bigl(\gamma_j - \overline{\gamma_{j'}}\bigr)
$$

です。$V_c$ は単位胞の体積で、$\mu$ (面積の次元) を単位胞あたりの量から入射電子 1 個あたりの収量へ換算します。縮退の極限 $\lambda \to 0$ では $F_{jj'}(t) \to t$ です。添字の並びは、行 (bra) が $j'$、列が $j$ です。

## 非チャネリング成分

熱散漫散乱による吸収でコヒーレントなブロッホ場から失われた電子は、方向がランダムになった電子として残りの厚さを走り、そこでもイオン化を起こします。

$$
Y_\text{dech}(t) = \frac{\mu_{00}}{V_c}\,\bigl(t - L_\text{coh}(t)\bigr),
\qquad
L_\text{coh}(t) = \int_0^t \sum_{\mathbf g} \frac{P_{\mathbf g}}{P_0}\,\bigl\lvert\psi_{\mathbf g}(z)\bigr\rvert^2\,dz
$$

$\mu_{00} = \sum_a \mathrm{Occ}_a\,\sigma_c$ は $\mu$ の対角成分です。$P_{\mathbf g}$ は波 $\mathbf g$ の波数ベクトルの表面の法線方向の成分 ($P_0$ は入射波) で、$\sum_{\mathbf g}(P_{\mathbf g}/P_0)\lvert\psi_{\mathbf g}\rvert^2$ は表面の法線方向に流れるコヒーレントな電流です。吸収が無ければこの量は厳密に 1 で、$L_\text{coh}(t) = t$、非チャネリング成分は 0 になります ($\sum_{\mathbf g}\lvert\psi_{\mathbf g}\rvert^2$ が 1 になるのは $P_{\mathbf g} \approx P_0$ のときだけです)。$L_\text{coh}$ は $\mu = \mathrm{diag}(P_{\mathbf g}/P_0)$ とした同じ縮約で計算します。

全収量は $Y = Y_\text{dyn} + Y_\text{dech}$ です。$t - L_\text{coh}(t)$ は吸収で失われた電流をすべて拾うので、再注入してよいのは熱散漫散乱による吸収だけです (それ以外の吸収が混じっているときは、ReciPro は計算を止めて知らせます)。

## 関連項目

- [7.4. ALCHEMI シミュレーション](../../7-diffraction-simulator/4-alchemi-simulation.md)
- [動力学計算（共通コア）](calculation.md)
- [付録 A3. Bloch波法による動力学計算の概要](index.md)
