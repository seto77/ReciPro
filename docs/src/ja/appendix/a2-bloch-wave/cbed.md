# CBED の計算

CBED（収束電子線回折）は、[動力学コア](calculation.md) を多数の入射方向に対して解き、その結果を回折ディスクとして並べ直す計算です。平行ビームの SAED では入射方向が 1 本ですが、CBED では対物絞り内の各点を **partial incident plane wave** とみなし、それぞれについて Bloch 波解を求めます。

---

## 収束電子線の表現

試料入射面での収束プローブは、試料上のプローブ位置 $\mathbf R_0$、レンズ位相 $\chi(\mathbf K)$、絞り関数 $A(\mathbf K)$ を用いて、次のような平面波の和として表せます。

$$\psi_{\mathrm{in}}(\mathbf R,0)=\sum_{\mathbf K\in\mathrm{aperture}} A(\mathbf K)\,
\exp(-2\pi i\,\mathbf K\cdot\mathbf R_0)\,
\exp[-i\chi(\mathbf K)]\,
\exp(2\pi i\,\mathbf K\cdot\mathbf R)$$

$\mathbf K$ は入射波数ベクトルの表面平行成分です。収束半角を $\alpha$、電子波長を $\lambda$ とすると、理想的な円形絞りでは

$$A(\mathbf K)=
\begin{cases}
1 & (|\mathbf K|\leq \sin\alpha/\lambda)\\
0 & (|\mathbf K|> \sin\alpha/\lambda)
\end{cases}$$

となります。代表的なレンズ位相は、デフォーカス $\Delta f$ と球面収差 $C_s$ を用いて

$$\chi(\mathbf K)=\pi\lambda|\mathbf K|^2\Delta f+\frac{\pi}{2}C_s\lambda^3|\mathbf K|^4+\cdots$$

と書けます。ReciPro では、ここに収差・絞り・収束角の設定が反映されます。

---

## 各入射方向の動力学計算

CBED では、絞り内の各 $\mathbf K$ を 1 本の平行入射波とみなし、方向ごとに Bethe/Bloch 波問題を解きます。概念的な手順は次のとおりです。

1. $\mathbf K$ と表面法線から、結晶中の参照波数 $\mathbf k_0(\mathbf K)$ を決める。
2. $R_{\mathbf g}=|\mathbf g|Q_{\mathbf g}^2$ に基づいて、動力学計算に含める反射を選ぶ。
3. 構造行列 $\mathbf A$ を組み立て、厚さ $t$ における透過係数 $T_{\mathbf g}(t;\mathbf K)$ を求める。

この処理は、[動力学計算（共通コア）](calculation.md) の透過係数計算を、入射方向の数だけ繰り返すものです。厚さシリーズを扱う場合は、一度固有値分解を行えば、各厚さで伝播因子を掛け替えるだけで同じ方向の結果を再利用できます。

---

## 回折ディスクの組み立て

各 $\mathbf K$ で得られた出射波を回折面へ配置すると、透過ディスクと各回折ディスクの内部強度が得られます。回折面座標を $\mathbf Q$ とすると、位置平均 CBED やコヒーレンスの低い条件では、ディスク内強度は

$$I_{\mathrm{CBED}}(\mathbf Q)=
\sum_{\mathbf K\in\mathrm{aperture}}
\left|\psi_{\mathbf K}(\mathbf Q,t)\right|^2$$

のように、各 partial plane wave の強度和として近似できます。一方、LACBED のように位相を保ったまま広い領域を扱う場合は、強度ではなく振幅を足し合わせてから絶対値二乗を取る必要があります。

---

## 見える情報

CBED では、Bloch 波解の厚さ依存性がディスク内部の強度分布として直接現れます。

- 厚さを変えると、ディスク内部の振動、HOLZ 線、Kossel-Mollenstedt 縞が変化します。
- 入射方位を変えると、どの反射が強く励起されるかが変わります。
- 収束角を大きくするとディスクが広がり、反射間の重なりや高次ラウエゾーンの情報が見えやすくなります。

したがって CBED は、Bloch 波法の結果を「回折面上のディスク」として観察する最も直接的なモードです。ReciPro の CBED 計算は、この収束波の離散化、各方向の動力学解、ディスク配列への再配置の組合せとして理解できます。

---

## パラメータ上の注意

- **ビーム数**: ゾーン軸条件や HOLZ 線を詳しく見る場合は、多数の反射が必要です。最大 Bloch 波数を増やしたときのディスク内部構造の変化を確認してください。
- **角度サンプリング**: 絞り内の $\mathbf K$ 点が粗いと、ディスク内部の強度が粒状になります。収束角が大きいほど細かいサンプリングが必要です。
- **厚さ**: 厚さシリーズでは、同じ入射方向の固有解を再利用できるため、固有値法が有利です。
- **コヒーレンス**: 強度和でよい条件と、振幅和が必要な条件を区別してください。

## 関連項目

- [動力学計算（共通コア）](calculation.md)
- [付録 A2. Bloch波法による動力学計算の概要](index.md)
- [7.4 CBEDシミュレーション](../../7-diffraction-simulator/4-cbed-simulation.md)
