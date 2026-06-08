# 減衰・輸送

散乱因子は1回の散乱イベントを表しますが、このページはビーム **全体** が固体中を進むときに何が起こるか — どれだけ速く除去され、どこまで侵入し、(電子線では)どう減速するか — を扱います。関係する物理は3つのビームでまったく異なり、これが **減衰・輸送** タブのプロットや表が放射線によって大きく変わる理由です。

=== "X線"
    ![減衰・輸送 — X線](../../../assets/cap-ja-auto/FormBeamInteraction-xray-attenuations.png)

=== "電子線"
    ![減衰・輸送 — 電子線](../../../assets/cap-ja-auto/FormBeamInteraction-electron-attenuations.png)

=== "中性子線"
    ![減衰・輸送 — 中性子線](../../../assets/cap-ja-auto/FormBeamInteraction-neutron-attenuations.png)

---

## X線 — 吸収と屈折

### Beer–Lambert 減衰

単色 X線ビームは経路長に対して指数関数的に除去されます。

$$I(t) = I_0\, e^{-\mu t}, \qquad \mu = \rho\,(\mu/\rho).$$

- $\mu/\rho$ : **質量減衰係数**(cm²/g)— 表に載る、密度によらない量。
- $\mu$ : 実際の密度 $\rho$ における **線減衰係数**(cm⁻¹)。
- $1/\mu$ : **減衰長**(強度が $1/e$ になる)。
- $\text{HVL} = \ln 2/\mu$ : **半価層**。
- $T = e^{-\mu t}$ : 厚さ $t$ の試料の透過率。

### $\mu/\rho$ の内訳

全質量減衰は3つの過程の和で、タブには個別に描かれます。

$$\left(\frac{\mu}{\rho}\right)_\text{total} = \left(\frac{\tau}{\rho}\right)_\text{photo} + \left(\frac{\mu}{\rho}\right)_\text{Rayleigh} + \left(\frac{\mu}{\rho}\right)_\text{Compton}.$$

化合物では質量減衰は元素値の質量分率加重和になり、線減衰係数は原子断面積を直接足し合わせます。

$$\left(\frac{\mu}{\rho}\right)_\text{mix} = \sum_i w_i\left(\frac{\mu}{\rho}\right)_i, \qquad \mu = \sum_i n_i\,\sigma_i,$$

ここで $w_i$ は質量分率、$n_i$ は数密度です。3つの成分は次のとおりです。

- **光電吸収** $\tau$ — 光子が吸収され束縛電子を叩き出す。低エネルギーで支配的で、吸収端の間ではおおよそ $\tau/\rho \propto Z^{3\!-\!4}/E^{3}$ で減少します。これは [蛍光](fluorescence.md) を生む内殻電子を叩き出す項です。
- **Rayleigh(コヒーレント)** 散乱 — 束縛電子による弾性散乱で、コヒーレント形状因子 $F(q)$ に関係します。
- **Compton(インコヒーレント)** 散乱 — 弱く束縛された電子による非弾性散乱で、インコヒーレント関数 $S(q)$ に関係し、高エネルギーで相対的重要性が増します。散乱された光子は波長が

$$\Delta\lambda = \lambda' - \lambda = \frac{h}{m_e c}\,(1-\cos\varphi)$$

  だけずれるので、Compton 散乱は光子を単色ビームから除去します(非弾性損失)。

**吸収端** は、光子エネルギーがある殻($K$, $L_3$, …)の結合エネルギーを越え、新たな電離チャネルが開くときに $\tau$ が急上昇する点です。**ジャンプ比** は端をまたいで $\mu/\rho$ が増える倍率で、ReciPro は $K$・$L_3$ の端エネルギーとジャンプを示します。**質量エネルギー吸収係数** $\mu_\text{en}/\rho$ は $\mu/\rho$ のうち局所にエネルギーを付与する部分(散乱・蛍光光子が持ち去るエネルギーを除く)です。

### 屈折・臨界角・SLD

固体の X線屈折率は **1 よりわずかに小さく**、

$$n = 1 - \delta + i\beta, \qquad \beta = \frac{\mu_\text{abs}\lambda}{4\pi} = \frac{r_e\lambda^2}{2\pi}\sum_i n_i\,f''_i, \qquad \delta \simeq \frac{r_e\lambda^2}{2\pi}\sum_i n_i\,(Z_i+f'_i)$$

と書けます($n_i$ は元素 $i$ の数密度、$r_e$ は古典電子半径)。ここで $\mu_\text{abs}$ は屈折率の虚部に対応する吸収成分($f''$ に結びつく)で、Rayleigh/Compton も含む上の total $\mu$ とは一致するとは限りません。$n<1$ のため、X線は小さなかすめ **臨界角**

$$\theta_c \simeq \sqrt{2\delta}$$

以下で **全反射** を起こします。これは屈折の幾何から導けます。かすめ角 $\alpha$ に対し固体内の鉛直波数は $k_z^2 \simeq k^2(\alpha^2 - 2\delta)$ で、$\alpha = \alpha_c = \sqrt{2\delta}$ で 0 になり、それ以下では波が物質内へ伝播できず全反射されます。**散乱長密度** の実部 $\text{SLD} = r_e\sum_i n_i (Z_i + f'_i)$ が $\delta$ を決め、反射率測定で使う中性子 SLD の X線版にあたります。ReciPro はスカラ表に $\delta$・$\beta$・$\theta_c$・X線 SLD を示します。

---

## 電子線 — 散乱・減速・飛程

固体中の高速電子は **散乱**(向きを変える)と **エネルギー損失**(連続的に減速)の両方を起こすので、その輸送には複数の長さスケールが必要です。

### 弾性散乱と平均自由行程

弾性断面積 $\sigma_\text{el}$ は、1個の原子が電子をどれだけ曲げやすいかを表します。ReciPro は **NIST Mott** 断面積(遮蔽原子ポテンシャル中の相対論的 Dirac 方程式の部分波解)を用い、おおよそ **50 eV – 36.4 keV** で有効です。範囲外、または表にない元素では **遮蔽 Rutherford** 近似にフォールバックします。両者は境界で完全に滑らかにつながるとは限りません。全断面積は微分断面積の角度積分です。

$$\sigma_\text{el} = 2\pi\int_0^\pi \frac{d\sigma}{d\Omega}\,\sin\Theta\,d\Theta, \qquad \frac{d\sigma}{d\Omega} \propto \frac{Z^2}{E^2}\,\frac{1}{\big[\sin^2(\Theta/2)+\eta\big]^2},$$

ここで遮蔽パラメータ $\eta$ が、裸の Rutherford 断面積の前方発散を丸めます。Mott の扱いは、遮蔽 Rutherford が省くスピン・相対論効果も含みます。断面積から

$$\Sigma_\text{el} = \sum_i n_i\,\sigma_{\text{el},i}, \qquad \lambda_\text{el} = \frac{1}{\Sigma_\text{el}}$$

がマクロ散乱係数と **弾性平均自由行程**(弾性イベント間の平均距離)を与えます。

### 阻止能と非弾性損失

エネルギーは主に電子励起(電離・プラズモン)に失われます。**阻止能** は正の量として

$$S(E) = -\frac{dE}{ds} > 0$$

と定義されます。ここで $s$ は軌跡に沿った **経路長**(タブの *dE/ds* 曲線の変数)で、この付録の他の箇所で使う散乱変数 $\sin\theta/\lambda$ とは別物です。エネルギー勾配 $dE/ds$ は負ですが、タブは $S$ を上向きに描きます。keV 領域では概念的に **Bethe** 形

$$S(E) \;\propto\; \frac{Z\rho}{A}\,\frac{1}{E}\,\ln\!\frac{E}{J}$$

に従います($J$ は固体の **平均励起エネルギー**)。これは非相対論的な概形でスケーリングを示すだけで、ReciPro は低エネルギーでも破綻しない補正・経験式(Joy–Luo 型)を評価します。スカラ表の **プラズモンエネルギー** $E_p$ は、同じ電子励起を特徴づける関連した別パラメータです。**非弾性平均自由行程**(IMFP)は対応するエネルギー損失衝突間の平均距離で、ReciPro は **TPP-2M** の予測式

$$\lambda_\text{in}(E) = \frac{E}{E_p^2\left[\beta_\text{T}\ln(\gamma_\text{T} E) - C/E + D/E^2\right]}$$

で評価できます($E$ は eV、$\lambda_\text{in}$ は Å、パラメータ $\beta_\text{T},\gamma_\text{T},C,D$ は $E_p$・密度・バンドギャップ・価電子数から作られます)。

### 2種類の飛程

- **CSDA 飛程** — 連続減速近似で阻止能を積分し、電子が止まるまでに飛ぶ総経路長を与えます。

$$R_\text{CSDA} = \int_{E_\text{cut}}^{E_0} \frac{dE}{S(E)}.$$

(実際には積分は低エネルギーカットオフ $E_\text{cut}$ まで行います。それより下では上の Bethe 概形は成り立ちません。)

- **Kanaya–Okayama 飛程** — 蛇行した散乱軌跡を考慮した、**侵入深さ**(経路長ではない)の広く使われる経験的推定。

$$R_\text{KO}\,[\mu\text{m}] = 0.0276\,\frac{A\,E_0^{1.67}}{\rho\,Z^{0.89}}, \qquad (E_0\ \text{は keV}).$$

両者は別の問いに答えます — 飛んだ総距離 vs 固体のどれだけ奥まで届くか — ので値が異なり、ReciPro は両方を報告します。これらの飛程が [電子飛程](../../8-electron-trajectory.md) や EBSD シミュレーションの相互作用体積を決めます。

---

## 中性子線 — マクロ断面積と 1/v 則

中性子にはエネルギー依存の減衰曲線がなく、相互作用は **核断面積** で固定されます。ビームはマクロ全断面積を通じて減衰し、これはコヒーレント・インコヒーレント・吸収の各成分の和です。

$$\Sigma_\text{total} = \sum_i n_i\,\sigma_{\text{total},i}, \qquad \sigma_\text{total} = \sigma_\text{coh} + \sigma_\text{inc} + \sigma_\text{abs}(\lambda), \qquad T = e^{-\Sigma_\text{total} t}.$$

減衰長は $1/\Sigma_\text{total}$ です。吸収部は中性子速度 $v$(したがって波長)に依存します。多くの核種では核近傍に滞在する時間が $1/v$ でスケールし、**1/v 則**

$$\sigma_\text{abs}(\lambda) = \sigma_\text{abs}(\lambda_0)\,\frac{\lambda}{\lambda_0}, \qquad \lambda_0 = 1.798\ \text{Å}\ (\text{熱中性子}, 2200\ \text{m/s})$$

を与えます。一部の強吸収体(Cd・Sm・Eu・Gd)は低エネルギー **共鳴** を持ち、単純な 1/v スケーリングを破ります。ReciPro はこれらの核種に印を付けます。コヒーレント **散乱長密度** $\text{SLD} = \sum_i n_i\, b_{\text{coh},i}$ は、上の X線 SLD の中性子版です。

---

## 侵入深さの比較

3つのビームは大きく異なる深さを探り、これが別々の問いに答える実用上の理由です。

| ビーム | 典型的な試料 | 侵入深さ(桁) | 決める量 |
|---|---|---|---|
| X線(≈8 keV) | 粉末 / 単結晶 | 10–100 µm | $\mu = \rho(\mu/\rho)$ |
| 電子線(≈200 keV) | TEM 薄膜 | 10–100 nm(有効) | 弾性 MFP + 非弾性損失 |
| 中性子線(熱) | cm 級のバルク | 1–10 cm | $\Sigma_\text{total}$ |

同じ長さスケールが、電子線が極薄試料と動力学理論を要する一方で、中性子線が単一散乱の運動学でバルク全体を見る理由を説明します。

---

## 関連項目

- [原子散乱因子](scattering-factor.md) — Rayleigh/Compton の背後にある $F(q)$/$S(q)$ 分解、および Mott 断面積。
- [蛍光](fluorescence.md) — X線光電吸収に続く緩和。
- [3. ビーム相互作用](../../3-beam-interaction.md) — *減衰・輸送* タブ。
- [8. 電子飛程](../../8-electron-trajectory.md) ・ [12. EBSDシミュレーション](../../12-ebsd-simulation.md) — 電子飛程が使われる場所。
