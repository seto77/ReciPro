# EBSD の計算

EBSD（電子後方散乱回折）の計算は、CBED や STEM と同じ Bethe/Bloch 波コアを使いますが、問題の立て方が大きく異なります。CBED/STEM は試料外から入射した電子波の出射状態を求める **入射問題**です。一方 EBSD は、試料内部で非弾性散乱を受けた電子が、どの外部方向へ後方散乱として出てくるかを求める **出射問題**です。

ReciPro では、この出射問題を相反定理で通常の入射問題へ変換し、方向空間上の **master pattern** を先に計算します。その後、Monte Carlo で得た深さ・エネルギー・方向の重みと、検出器幾何を組み合わせて detector pattern を作ります。

---

## 相反定理による書き換え

結晶内部の点 $\mathbf r_n$ から出た電子が、試料外部の方向 $\widehat{\mathbf s}$ へ到達する振幅を直接求めようとすると、発光点ごとに散乱問題を解く必要があります。これは現実的ではありません。

相反定理を用いると、この問題は「試料外から $-\widehat{\mathbf s}$ 方向に平面波を入射したとき、その相反波が結晶内部の点 $\mathbf r_n$ でどの振幅を持つか」という通常の Bethe/Bloch 波問題へ置き換えられます。この相反波を $\psi_{\widehat{\mathbf s}}^{\mathrm{rec}}(\mathbf r)$ と書けば、方向 $\widehat{\mathbf s}$ への EBSD 強度は

$$I_{\mathrm{EBSD}}(\widehat{\mathbf s};E,z)\propto
\sum_n \sigma_n(E,z)\,
\left|\psi_{\widehat{\mathbf s}}^{\mathrm{rec}}(\mathbf r_n;E,z)\right|^2$$

と表せます。ここで $\sigma_n(E,z)$ は、エネルギー $E$、深さ $z$ において、原子位置 $\mathbf r_n$ 近傍から後方散乱チャネルへ入る非弾性散乱の重みです。発光源同士は非弾性散乱によって実質的にコヒーレンスを失うため、振幅和の絶対値二乗ではなく、各発光源の強度和として扱います。

---

## Master Pattern

EBSD の master pattern は、上式のうち結晶固有の動力学回折部分を方向空間上に保存したものです。概念的には

$$M(\widehat{\mathbf s};E,z)=
\sum_n w_n(E,z)\,
\left|\psi_{\widehat{\mathbf s}}^{\mathrm{rec}}(\mathbf r_n)\right|^2$$

です。$w_n$ は原子位置 $\mathbf r_n$ における結晶側の非弾性散乱重みで、ReciPro では経験的な

$$w_n \propto Z_n^{1.7}\,\mathrm{occ}_n$$

を用います。ここで $Z_n$ は原子番号、$\mathrm{occ}_n$ は占有率です。この重みは、Monte Carlo が与える輸送的な深さ・エネルギー分布とは別の、結晶側の重みです。

実装側では、相反波の Bloch 展開を各原子位置で評価し、

$$\beta_n^{(j)}=
\alpha^{(j)}
\sum_{\mathbf g}C_{\mathbf g}^{(j)}
\exp\!\left[2\pi i(\mathbf k^{(j)}+\mathbf g)\cdot\mathbf r_n\right]$$

を作ります。さらに Bloch 波対 $(j,j')$ ごとの行列

$$S_{jj'}=\sum_n w_n\,\beta_n^{(j)}\,\overline{\beta_n^{(j')}}$$

と、厚さ方向の解析積分

$$\mathcal F_{jj'}(t)=
\frac{\exp\!\left[2\pi i(\gamma^{(j)}-\overline{\gamma^{(j')}})t\right]-1}
{2\pi i(\gamma^{(j)}-\overline{\gamma^{(j')}})}$$

を用いて、

$$M(\widehat{\mathbf s};E,t)=
\mathrm{Re}\left\{\sum_{j,j'}S_{jj'}(E)\,\mathcal F_{jj'}(t)\right\}$$

として master pattern を評価します。分母が 0 に近い縮退極限では、$\mathcal F_{jj'}(t)\to t$ として扱います。

---

## 方向空間のサンプリング

Master pattern は、検出器画像そのものではなく、結晶に固定された方向空間上の強度分布です。ReciPro は方向空間を Rosca-Lambert 等積投影でサンプリングし、$+Z$ 半球と $-Z$ 半球を別々の plane 配列として保持します。等積投影を使うことで、極付近と赤道付近のサンプリング密度の偏りを抑えられます。

この段階で得られる master pattern は、結晶構造・加速電圧・深さ・エネルギー・吸収モデルに依存しますが、パターンセンターやスクリーン位置などの検出器幾何はまだ畳み込まれていません。

---

## Monte Carlo 重みと Detector Pattern

実測に近い EBSD detector pattern を作るには、master pattern に、後方散乱電子がどの深さ・どのエネルギー・どの方向から出てくるかという輸送重みを掛ける必要があります。この重みを

$$W(E,z;\widehat{\mathbf s})$$

と書くと、スクリーン上の画素 $\mathbf p$ に対応する結晶固定方向 $\widehat{\mathbf s}(\mathbf p)$ に対して、最終的な detector pattern は

$$P(\mathbf p)=
\sum_{i,j}
W(E_i,z_j;\widehat{\mathbf s}(\mathbf p))\,
M(\widehat{\mathbf s}(\mathbf p);E_i,z_j)$$

のように、エネルギーと深さに関する離散和で表せます。

Monte Carlo 側は、入射電子の弾性散乱、非弾性散乱、エネルギー損失、表面からの脱出を追跡し、後方散乱電子について深さ・エネルギー・脱出方向の分布を作ります。ReciPro では、最後の非弾性散乱位置とその直後のエネルギーを発光源の代表量として使うモデルと、脱出時の深さ・エネルギーを使うモデルを区別できます。

---

## TDS 背景と吸収モデル

EBSD パターンには、菊池バンドの幾何構造だけでなく、熱散漫散乱（TDS）に由来する滑らかな背景も含まれます。ReciPro では `IncludeTDSBackground` を有効にすると、後方半球

$$\pi/2\leq\theta\leq\pi$$

へ散乱される TDS 成分を吸収行列 $\mu_{\mathrm{back}}$ として評価し、master pattern と同じ Bloch 波対和の中で背景強度を加えます。弾性信号と同じ固有解を使えるため、TDS 背景は比較的少ない追加コストで計算できます。

`UseNonLocalAbsorption` を有効にすると、吸収ポテンシャルを単なる $U'_{\mathbf g-\mathbf h}$ ではなく、方向やビーム対に依存する非局所形式として扱います。これは精度を上げられる一方、方向ごとに吸収行列を組み直す必要があるため、master pattern 計算のコストを大きく増やします。

---

## パラメータ上の注意

- **ビーム数**: 少なすぎると菊池バンドの細部や HOLZ バンドが崩れます。低指数晶帯軸では数百本の反射が必要になることがあります。
- **深さ・エネルギー配列**: Monte Carlo 重み $W(E,z;\widehat{\mathbf s})$ の変化より粗い刻みにすると、バンド幅やチャネリングの厚さ依存が平均化されます。
- **検出器幾何**: パターンセンター、スクリーン距離、試料傾斜は $\widehat{\mathbf s}(\mathbf p)$ の対応を決めるため、master pattern が正しくても detector pattern は変わります。
- **相反定理の解釈**: Master pattern は検出器像そのものではありません。Monte Carlo 重みと検出器投影を重ねて初めて detector pattern になります。
- **TDS 背景**: バンドコントラストの定量比較では有効化が有用です。一方、幾何構造だけを見たい場合は、背景を外した方が視認しやすいことがあります。

## 関連項目

- [動力学計算（共通コア）](calculation.md)
- [付録 A2. Bloch波法による動力学計算の概要](index.md)
- [12. EBSDシミュレーション](../../12-ebsd-simulation.md)
