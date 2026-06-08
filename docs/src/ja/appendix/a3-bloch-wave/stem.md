# STEM の計算

STEM 像計算は、[CBED](cbed.md) と同じ収束プローブ表現を出発点にします。ただし CBED が回折面のディスク強度をそのまま見るのに対し、STEM ではプローブ位置を走査し、各位置で検出器に入る強度を積分して像を作ります。

---

## 観測量の定義

プローブ位置を $\mathbf R_0$、回折面座標を $\mathbf Q$、試料厚さを $t$ とします。検出器関数 $D(\mathbf Q)$ を、検出器に入る角度範囲で 1、それ以外で 0 となる関数とすれば、弾性散乱による STEM 強度は

$$I_{\mathrm{STEM}}^{\mathrm{ela}}(\mathbf R_0)=
\int D(\mathbf Q)\,
\left|\psi(\mathbf Q,t;\mathbf R_0)\right|^2\,d\mathbf Q$$

と書けます。BF、ABF、LAADF、HAADF の違いは、この $D(\mathbf Q)$ の内角・外角をどう選ぶかに対応します。つまり STEM の検出器角度を変えることは、単なる表示設定ではなく、積分している物理量を変えることです。

---

## Fourier 係数による高速化

素朴には、走査する全ての $\mathbf R_0$ について動力学計算を解き直せばよいように見えます。しかし収束プローブの $\mathbf R_0$ 依存性は、入射波の位相因子

$$\exp(-2\pi i\,\mathbf K\cdot\mathbf R_0)$$

として現れます。この構造を利用すると、像そのもの $I_{\mathrm{STEM}}(\mathbf R_0)$ ではなく、像の二次元 Fourier 係数を先に計算できます。概念的には

$$I_{\mathrm{STEM}}^{\mathrm{ela}}(\mathbf q)=
\sum_{\mathbf g,\mathbf h}
F_{\mathbf g,\mathbf h}(t)\,
\delta(\mathbf q-\mathbf g+\mathbf h)$$

と整理でき、$F_{\mathbf g,\mathbf h}(t)$ を求めてから逆 Fourier 変換することで、全走査位置の像を高速に再構成できます。

この点が、Bloch 波法による STEM 計算の大きな利点です。完全結晶・小さい単位胞では、プローブ位置ごとにマルチスライス計算を繰り返すよりも効率的になる場合があります。

---

## TDS と検出器選択吸収

HAADF-STEM では、弾性散乱だけでなく熱散漫散乱（TDS）に由来する非弾性成分がコントラストの主要因になります。ReciPro では、TDS を「弾性チャネルから特定角度範囲へ抜ける成分」とみなし、吸収ポテンシャルとして扱います。

検出器の角度範囲を $\theta_1\leq\theta\leq\theta_2$ とすると、検出器選択された吸収形状因子は概念的に

$$f'_{\kappa}(\mathbf g;\theta_1,\theta_2)=
\int_{\theta_1}^{\theta_2}\sin\theta\,d\theta
\int_0^{2\pi}
\left|\Delta f_{e,\kappa}(\mathbf g,\theta,\phi)\right|^2\,d\phi$$

のような角度積分で定義されます。この範囲を BF / ADF / HAADF の検出器角度に合わせることで、その検出器へ入る TDS 成分を評価します。

STEM の TDS 強度は、厚さ方向に沿った吸収量として

$$I_{\mathrm{STEM}}^{\mathrm{TDS}}(\mathbf R_0)=
\int_0^t
\langle\psi(z;\mathbf R_0)|\widehat W_{\mathrm{det}}|\psi(z;\mathbf R_0)\rangle\,dz$$

と表せます。ここで $\widehat W_{\mathrm{det}}$ は検出器選択 TDS を表す演算子です。Bloch 波の固有値・固有ベクトルが得られていれば、この $z$ 積分は解析的に扱えます。スライスごとに数値積分する方法もあり、ReciPro は用途に応じてこれらの考え方を使い分けます。

---

## 局所近似と非局所形式

吸収ポテンシャルには、主に二つの扱いがあります。

| 形式 | 内容 | 特徴 |
|------|------|------|
| 局所近似 | $U'(\mathbf r)$ として位置だけに依存する吸収を使う。 | 広い ADF / HAADF 検出器では有効な近似になりやすく、高速。 |
| 非局所形式 | $U'(\mathbf r,\mathbf r')$ または行列要素 $U'_{\mathbf g,\mathbf h}$ として、入射・出射波の組に依存する吸収を使う。 | 狭い検出器、重元素、低加速電圧などで有効だが、計算コストが大きい。 |

局所近似では $U'_{\mathbf g-\mathbf h}$ のように逆格子差だけで行列要素を評価できます。一方、非局所形式では各 $(\mathbf g,\mathbf h)$ ペアごとに角度積分が必要になるため、ビーム数に対して計算量が大きく増えます。

---

## Bloch 波 STEM の適用範囲

Bloch 波 STEM は、周期性の高い完全結晶に対して高速で、厚さ・デフォーカス・検出器角度の系統比較に向いています。一方、欠陥、大きなスーパーセル、非周期構造を扱う場合は、周期境界を前提とする Bloch 波法よりも、frozen-phonon multislice などの方法が適することがあります。

ReciPro の STEM 計算は、「CBED と同じ収束波を使い、観測量を検出器積分に変えると STEM になる」と捉えると整理しやすくなります。

---

## パラメータ上の注意

- **検出器角度**: BF / ABF / ADF / HAADF は、$D(\mathbf Q)$ と $f'_{\kappa}(\mathbf g;\theta_1,\theta_2)$ の定義そのものです。
- **ビーム数**: 像の高周波成分やチャネリングは、採用する反射数に敏感です。
- **厚さ刻み**: 数値スライス積分を使う場合は、スライス厚みを半分にしたときの変化を確認してください。
- **TDS モデル**: HAADF の $Z$ コントラストを議論する場合は、弾性成分だけでなく TDS 成分の扱いが重要です。

## 関連項目

- [動力学計算（共通コア）](calculation.md)
- [付録 A3. Bloch波法による動力学計算の概要](index.md)
- [9.2. STEMシミュレーション](../../9-hrtem-stem-simulator/2-stem-simulation.md)
