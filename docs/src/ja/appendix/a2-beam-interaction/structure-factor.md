# 構造因子

原子散乱因子は1個の原子を表しますが、**構造因子** は単位胞内のすべての原子が *一緒に* どう散乱するかを表します。これは **反射** タブが集計する量(`F_real`・`F_inv`・$\lvert F\rvert$・$F^2$)であり、前ページの原子物理と回折強度を結ぶ橋渡しです。

=== "X線"
    ![反射 — X線](../../../assets/cap-ja-auto/FormBeamInteraction-xray-reflections.png)

=== "電子線"
    ![反射 — 電子線](../../../assets/cap-ja-auto/FormBeamInteraction-electron-reflections.png)

=== "中性子線"
    ![反射 — 中性子線](../../../assets/cap-ja-auto/FormBeamInteraction-neutron-reflections.png)

---

## 単位胞にわたる干渉

反射 $\mathbf g = (hkl)$ の構造因子は、各原子の分率座標 $\mathbf r_j = (x_j,y_j,z_j)$ による位相を重みとした、原子因子のコヒーレントな和です。

$$F_{\mathbf g} = \sum_{j} o_j\, f_j(s,E)\, T_j(\mathbf g)\, \exp\!\left(-2\pi i\,(h x_j + k y_j + l z_j)\right).$$

- $o_j$ : サイト **占有率**(部分占有・混合占有のための分率)。
- $f_j(s,E)$ : 現在のビームに対する原子 $j$ の散乱因子 — X線では ReciPro の [位相規約](index.md#位相規約) で $f_0+f'-if''$、電子線では $f_e$、中性子線では $b$。
- $T_j(\mathbf g)$ : Debye–Waller 因子(後述)。
- $-2\pi i$ の位相は ReciPro の [規約](index.md#位相規約) に従います。

強度はモジュラスの二乗、

$$I_{\mathbf g} \;\propto\; \lvert F_{\mathbf g}\rvert^2 = F_\text{real}^2 + F_\text{inv}^2 ,$$

で、表の $F^2$ 列です。`F_real`・`F_inv` は複素構造因子の実部・虚部です。散乱因子が実数でも、非中心対称構造(や原点の取り方)では $F_{\mathbf g}$ は一般に複素数になります。X線異常分散(複素 $f$)や複素中性子散乱長は、さらに虚部を加えます。`F_inv` がすべての反射で 0 になるのは、構造が中心対称で原点を対称心に取り、かつすべての因子が実数のときだけです。

---

## Debye–Waller 因子

原子は平衡位置のまわりで振動し、散乱密度をぼかして高角の因子を弱めます。等方的な振動では

$$T_j = \exp\!\left(-B_j\, s^2\right), \qquad B_j = 8\pi^2\langle u_j^2\rangle,$$

ここで $\langle u_j^2\rangle$ は散乱方向に沿った平均二乗変位、$B_j$ は等方性変位パラメータ(Å²)です。異方的な振動はこれを

$$T_j = \exp\!\left(-2\pi^2\,\mathbf g^{\mathsf T}\!\mathbf U_j\,\mathbf g\right)$$

に一般化します($\mathbf U_j$ は変位テンソル、$\mathbf g$ は逆格子ベクトルで $|\mathbf g|=1/d$、$Q=2\pi\lvert\mathbf g\rvert$ ではない)。Debye 固体では、平均二乗変位そのものが温度 $T$・原子質量 $M$・Debye 温度 $\Theta_D$ の関数です。

$$\langle u^2\rangle = \frac{3\hbar^2}{M k_B \Theta_D}\left[\frac14 + \left(\frac{T}{\Theta_D}\right)^2\!\int_0^{\Theta_D/T}\frac{x}{e^x-1}\,dx\right],$$

したがって $B$ は温度とともに増え、重い原子では小さくなります。ReciPro はこれを計算せず、表値または入力値の $B_j$ を直接使います。$T_j$ は散乱因子に掛かるので、**散乱因子** タブは同じ $e^{-Bs^2}$ 減衰を描画曲線に適用できます。この減衰は温度と $s$ とともに増大し、これが熱散漫散乱(コヒーレントな Bragg ビームから取り除かれ、散漫な背景へ再分配される強度)が動力学理論の吸収ポテンシャルを与える理由でもあります([付録 A3](../a3-bloch-wave/index.md))。

---

## 消滅: 系統的 vs 偶然的

反射が **消える** 理由は2つあります。

- **系統的(空間群による)消滅.** 格子の心(centering)や、並進成分を持つ対称要素(らせん軸・映進面)は、原子の中身によらずその空間群のすべての結晶で、ある反射群を *厳密に* 消します。これらが **消滅する面は非表示** の根拠です。
- **偶然的な準消滅.** ある特定の構造で原子寄与がたまたま打ち消し合うと、強度は小さいが対称性で禁制ではなく、組成や位置が変われば再び現れます。これらは消滅則では除外されません。

系統消滅は、単位胞の対称関連コピー間の位相の打ち消しです。心の並進 $\mathbf t_\alpha$ に対し、構造因子は共通因子

$$F_{\mathbf g} \propto \sum_\alpha e^{-2\pi i\,\mathbf g\cdot\mathbf t_\alpha}$$

を持ち、これが特定の $hkl$ で 0 になります。体心($\mathbf t = \tfrac12,\tfrac12,\tfrac12$)では

$$1 + e^{-\pi i (h+k+l)} = 0 \quad\Longleftrightarrow\quad h+k+l \ \text{が奇数}.$$

代表的な系統消滅は次のとおりです。

| 対称要素 | 消滅条件 | 影響する反射 |
|---|---|---|
| $I$(体心) | $h+k+l$ 奇数 | すべての $hkl$ |
| $F$(面心) | $h,k,l$ の偶奇が混在 | すべての $hkl$ |
| $C$(C 底心) | $h+k$ 奇数 | すべての $hkl$ |
| $2_1$ らせん $\parallel b$ | $k$ 奇数 | $0k0$ |
| $a$ 映進 $\perp b$ | $h$ 奇数 | $h0l$ |
| $c$ 映進 $\perp b$ | $l$ 奇数 | $h0l$ |

心(centering)条件はすべての反射に効き、らせん・映進条件は対応する軸列・帯のみに効きます。これが空間群の判別に使える理由です。

---

## Friedel 則とその破れ

散乱因子が実数(非共鳴)の構造では、和の複素共役をとり $\mathbf g$ の符号を反転すると(見やすさのため実数の重み $o_j T_j$ は省略)、直ちに

$$F_{-\mathbf g} = \sum_j f_j\, e^{+2\pi i\,\mathbf g\cdot\mathbf r_j} = \left(\sum_j f_j\, e^{-2\pi i\,\mathbf g\cdot\mathbf r_j}\right)^{*} = F_{\mathbf g}^{*}, \qquad\text{ゆえに}\qquad \lvert F_{hkl}\rvert = \lvert F_{\bar h\bar k\bar l}\rvert \quad\text{(Friedel 則)}$$

が得られます。このとき結晶が非中心対称でも回折は中心対称的に見えます。**異常分散はこれを破り得ます。** 構造因子を、きれいに共役する正常部と異常部に分け、ReciPro の $f = f_0 + f' - i f''$ 規約で $F_{\mathbf g} = A_{\mathbf g} - i B_{\mathbf g}$、$F_{-\mathbf g} = A_{\mathbf g}^{*} - i B_{\mathbf g}^{*}$ と書くと、**Bijvoet 差** は

$$\lvert F_{\mathbf g}\rvert^2 - \lvert F_{-\mathbf g}\rvert^2 = -4\,\operatorname{Im}\!\left(A_{\mathbf g}\, B_{\mathbf g}^{*}\right)$$

となり、正常部と異常部の位相が異なるとき — すなわち化学的に異なる異常散乱体が非中心対称なサイトを占めるとき — にのみ 0 でなくなります。(中心対称構造、単一元素、またはすべての原子が同じ複素因子を持つ場合は差が 0 になります。)これが、非中心対称結晶の絶対構造(掌性)の決定を可能にし、吸収端近傍の X線エネルギーを選んだとき、ReciPro が Friedel 対について 0 でない `F_inv` と異なる $\lvert F\rvert$ を報告する物理的理由です。

---

## 構造因子から粉末強度へ

**粉末回折(BB光学系)強度** を有効にすると、ランダム配向多結晶の幾何を畳み込んで $\lvert F\rvert^2$ を相対粉末強度に変換します。

$$I_{hkl} \;\propto\; m_{hkl}\, \lvert F_{hkl}\rvert^2\, L p(\theta),$$

- $m_{hkl}$ : **多重度** — 同じ $2\theta$ に重なる対称等価な面の数(表の *多重度* 列)。
- $Lp(\theta)$ : Bragg–Brentano 光学系の **ローレンツ・偏光** 因子 $Lp = \dfrac{1+\cos^2 2\theta}{\sin^2\theta\,\cos\theta}$。低角ピークを強く押し上げます。

このモードでは等価面が1本の線にまとめられるため、ReciPro は *等価な面は非表示*・*消滅する面は非表示* も自動的にオンにします。

---

## 関連項目

- [原子散乱因子](scattering-factor.md) — 和に入る $f_j$。
- [減衰・輸送](attenuation-transport.md) — 散乱イベントの合間にビームに何が起きるか。
- [3. ビーム相互作用 → 反射タブ](../../3-beam-interaction.md#反射タブ)
- [付録 A3. 動力学回折](../a3-bloch-wave/index.md) — $\lvert F\rvert^2$(運動学的)では足りなくなるとき。
