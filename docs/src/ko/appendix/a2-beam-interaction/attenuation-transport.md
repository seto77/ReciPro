# 감쇠 및 수송

산란 인자는 단일 산란 사건을 기술하지만, 이 페이지는 빔이 고체를 통과하면서 **전체로서** 어떤 일이 일어나는지를 다룹니다 — 얼마나 빠르게 제거되는지, 얼마나 깊이 침투하는지, 그리고 (전자의 경우) 어떻게 감속되는지입니다. 관련 물리는 세 가지 빔에 대해 완전히 다르며, 그래서 **감쇠 & 수송** 탭은 복사선에 따라 그래프와 표를 그토록 크게 바꿉니다.

=== "X-ray"
    ![감쇠 & 수송 — X-ray](../../../assets/cap-ko-auto/FormBeamInteraction-xray-attenuations.png)

=== "Electron"
    ![감쇠 & 수송 — electron](../../../assets/cap-ko-auto/FormBeamInteraction-electron-attenuations.png)

=== "Neutron"
    ![감쇠 & 수송 — neutron](../../../assets/cap-ko-auto/FormBeamInteraction-neutron-attenuations.png)

---

## X선 — 흡수와 굴절

### Beer–Lambert 감쇠

단색 X선 빔은 경로 길이에 따라 지수적으로 제거됩니다:

$$I(t) = I_0\, e^{-\mu t}, \qquad \mu = \rho\,(\mu/\rho).$$

- $\mu/\rho$ : **질량 감쇠 계수** (cm²/g) — 표로 정리된, 밀도에 무관한 양.
- $\mu$ : 재료의 실제 밀도 $\rho$ 에서의 **선형 감쇠 계수** (cm⁻¹).
- $1/\mu$ : **감쇠 길이** (세기가 $1/e$ 로 떨어짐).
- $\text{HVL} = \ln 2/\mu$ : **반가층**.
- $T = e^{-\mu t}$ : 두께 $t$ 인 시료의 투과율.

### $\mu/\rho$ 를 구성하는 것

총 질량 감쇠는 세 가지 과정의 합으로, 탭에서 각각 따로 표시됩니다:

$$\left(\frac{\mu}{\rho}\right)_\text{total} = \left(\frac{\tau}{\rho}\right)_\text{photo} + \left(\frac{\mu}{\rho}\right)_\text{Rayleigh} + \left(\frac{\mu}{\rho}\right)_\text{Compton}.$$

화합물의 경우 질량 감쇠는 원소 값들의 질량 가중 합이며, 선형 계수는 원자 단면적을 직접 더합니다:

$$\left(\frac{\mu}{\rho}\right)_\text{mix} = \sum_i w_i\left(\frac{\mu}{\rho}\right)_i, \qquad \mu = \sum_i n_i\,\sigma_i,$$

여기서 $w_i$ 는 질량 분율, $n_i$ 는 수밀도입니다. 세 성분은 다음과 같습니다:

- **광흡수** $\tau$ — 광자가 흡수되어 속박 전자를 방출합니다. 낮은 에너지에서 지배적이며, 흡수단 사이에서 대략 $\tau/\rho \propto Z^{3\!-\!4}/E^{3}$ 로 감소합니다. 이는 내각 전자를 방출하는 항이며, 그 완화가 [형광](fluorescence.md)을 만들어냅니다.
- **Rayleigh (가간섭) 산란** — 속박 전자에 의한 탄성 산란으로, 가간섭 형상 인자 $F(q)$ 와 관련됩니다.
- **Compton (비가간섭) 산란** — 약하게 속박된 전자에 의한 비탄성 산란으로, 비가간섭 함수 $S(q)$ 와 관련됩니다. 높은 에너지에서 상대적 중요도가 커집니다. 산란된 광자는 파장이 다음만큼 이동합니다

$$\Delta\lambda = \lambda' - \lambda = \frac{h}{m_e c}\,(1-\cos\varphi),$$

  따라서 Compton 사건은 광자를 단색 빔에서 제거합니다 (비탄성 손실).

**흡수단**은 광자 에너지가 어떤 전자각 ($K$, $L_3$, …)의 결합 에너지를 넘어 새로운 이온화 채널을 열 때 나타나는 $\tau$ 의 급격한 상승입니다. **점프비**는 흡수단을 가로질러 $\mu/\rho$ 가 증가하는 배수이며, ReciPro 는 $K$ 및 $L_3$ 흡수단 에너지와 점프를 나열합니다. **질량 에너지 흡수 계수** $\mu_\text{en}/\rho$ 는 $\mu/\rho$ 중에서 에너지를 국소적으로 침착시키는 부분입니다 (산란 및 형광 광자가 운반해 가는 에너지는 제외).

### 굴절, 임계각, 그리고 SLD

고체의 X선 굴절률은 **1보다 약간 작으며**, 다음과 같이 표현됩니다

$$n = 1 - \delta + i\beta, \qquad \beta = \frac{\mu_\text{abs}\lambda}{4\pi} = \frac{r_e\lambda^2}{2\pi}\sum_i n_i\,f''_i, \qquad \delta \simeq \frac{r_e\lambda^2}{2\pi}\sum_i n_i\,(Z_i+f'_i),$$

여기서 $n_i$ 는 원소 $i$ 의 수밀도, $r_e$ 는 고전 전자 반경입니다. 여기서 $\mu_\text{abs}$ 는 감쇠의 흡수성 부분 ($f''$ 에 연결됨)이며, 위의 총 $\mu$ 와 같을 필요는 없습니다. 후자는 Rayleigh 및 Compton 산란도 포함하기 때문입니다. $n<1$ 이므로 X선은 작은 빗각의 **임계각** 아래에서 **전외부반사**를 겪습니다

$$\theta_c \simeq \sqrt{2\delta}.$$

이는 굴절 기하학에서 따라옵니다: 빗각 $\alpha$ 에 대해 고체 내부의 수직 파수 벡터는 $k_z^2 \simeq k^2(\alpha^2 - 2\delta)$ 이며, $\alpha = \alpha_c = \sqrt{2\delta}$ 에서 0에 도달합니다. 그 아래에서는 파동이 재료 안으로 전파할 수 없어 완전히 반사됩니다. **산란 길이 밀도**의 실수부 $\text{SLD} = r_e\sum_i n_i (Z_i + f'_i)$ 는 $\delta$ 를 결정하며, 반사율 측정에서 사용되는 중성자 SLD의 X선 대응물입니다. ReciPro 는 스칼라 표에 $\delta$, $\beta$, $\theta_c$, 그리고 X선 SLD를 보고합니다.

---

## 전자 — 산란, 감속, 그리고 비정거리

고체 내의 빠른 전자는 **산란**(방향 변경)하는 동시에 연속적으로 **에너지를 잃으므로**, 그 수송에는 하나 이상의 길이 척도가 필요합니다.

### 탄성 산란과 평균 자유 행로

탄성 단면적 $\sigma_\text{el}$ 은 단일 원자가 전자를 얼마나 쉽게 편향시키는지를 측정합니다. ReciPro 는 **NIST Mott** 단면적 (차폐된 원자 퍼텐셜에서 상대론적 Dirac 방정식의 부분파 해)을 사용하며, 대략 **50 eV – 36.4 keV** 범위에서 유효합니다. 이 범위 밖이거나 표에 없는 원소의 경우 **차폐 Rutherford** 근사로 되돌아갑니다. 둘은 경계에서 완벽히 매끄럽게 이어질 필요는 없습니다. 총 단면적은 미분 단면적의 각도 적분입니다,

$$\sigma_\text{el} = 2\pi\int_0^\pi \frac{d\sigma}{d\Omega}\,\sin\Theta\,d\Theta, \qquad \frac{d\sigma}{d\Omega} \propto \frac{Z^2}{E^2}\,\frac{1}{\big[\sin^2(\Theta/2)+\eta\big]^2},$$

여기서 차폐 매개변수 $\eta$ 는 순수 Rutherford 단면적의 전방 발산을 둥글게 깎아냅니다. Mott 처리는 차폐 Rutherford가 생략하는 스핀 및 상대론적 효과를 추가로 포함합니다. 단면적으로부터,

$$\Sigma_\text{el} = \sum_i n_i\,\sigma_{\text{el},i}, \qquad \lambda_\text{el} = \frac{1}{\Sigma_\text{el}},$$

거시적 산란 계수와 **탄성 평균 자유 행로** — 탄성 사건 사이의 평균 거리 — 가 주어집니다.

### 저지능과 비탄성 손실

에너지는 주로 전자적 들뜸(이온화, 플라스몬)으로 손실됩니다. **저지능**은 양의 양으로 정의됩니다,

$$S(E) = -\frac{dE}{ds} > 0,$$

여기서 $s$ 는 궤적을 따른 **경로 길이** (탭의 *|dE/ds|* 곡선의 변수)이며, 이 부록의 다른 곳에서 사용되는 산란 변수 $\sin\theta/\lambda$ 가 아닙니다. 에너지 기울기 $dE/ds$ 는 음수이므로 탭은 $S$ 를 위쪽으로 그립니다. keV 에너지에서는 개념적으로 **Bethe** 형태를 따릅니다

$$S(E) \;\propto\; \frac{Z\rho}{A}\,\frac{1}{E}\,\ln\!\frac{E}{J},$$

여기서 $J$ 는 고체의 **평균 들뜸 에너지**입니다. 이 비상대론적 스케치는 스케일링만을 보여줍니다. ReciPro 는 낮은 에너지에서도 양호하게 유지되는 보정/경험적 형태(Joy–Luo 유형)를 평가합니다. 스칼라 표의 **플라스몬 에너지** $E_p$ 는 동일한 전자적 들뜸에 대한 관련되지만 별개인 특성화입니다. **비탄성 평균 자유 행로** (IMFP)는 에너지를 잃는 충돌 사이의 대응되는 평균 거리이며, ReciPro 는 이를 **TPP-2M** 예측 공식으로부터 평가할 수 있습니다,

$$\lambda_\text{in}(E) = \frac{E}{E_p^2\left[\beta_\text{T}\ln(\gamma_\text{T} E) - C/E + D/E^2\right]},$$

여기서 $E$ 는 eV, $\lambda_\text{in}$ 는 Å 단위이며, 매개변수 $\beta_\text{T},\gamma_\text{T},C,D$ 는 $E_p$, 밀도, 띠 간격, 그리고 원자가 전자 수로부터 구성됩니다.

### 두 종류의 비정거리

- **CSDA 비정거리** — 연속 감속 근사(continuous-slowing-down approximation)는 저지능을 적분하여 전자가 멈추기 전까지 이동한 총 경로 길이를 줍니다:

$$R_\text{CSDA} = \int_{E_\text{cut}}^{E_0} \frac{dE}{S(E)}.$$

(실제로는 적분이 저에너지 차단값 $E_\text{cut}$ 까지 내려가며, 그 아래에서는 위의 Bethe 스케치가 더 이상 성립하지 않습니다.)

- **Kanaya–Okayama 비정거리** — 굴곡지고 산란된 궤적을 고려하여 **침투 깊이**(경로 길이가 아님)를 추정하는 널리 사용되는 경험식:

$$R_\text{KO}\,[\mu\text{m}] = 0.0276\,\frac{A\,E_0^{1.67}}{\rho\,Z^{0.89}}, \qquad (E_0\ \text{in keV}).$$

둘은 서로 다른 질문에 답합니다 — 날아간 총 거리 대 전자가 고체 안으로 얼마나 깊이 도달하는지 — 따라서 값이 다르며, ReciPro 는 둘 다 보고합니다. 이 비정거리들은 [전자 궤적](../../8-electron-trajectory.md) 및 EBSD 시뮬레이션 뒤의 상호작용 부피를 설정합니다.

---

## 중성자 — 거시적 단면적과 1/v 법칙

중성자의 경우 에너지 의존적 감쇠 곡선이 없으며, 상호작용은 **핵 단면적**에 의해 고정됩니다. 빔은 거시적 총 단면적을 통해 감쇠되며, 이 자체는 가간섭, 비가간섭, 흡수 부분의 합입니다:

$$\Sigma_\text{total} = \sum_i n_i\,\sigma_{\text{total},i}, \qquad \sigma_\text{total} = \sigma_\text{coh} + \sigma_\text{inc} + \sigma_\text{abs}(\lambda), \qquad T = e^{-\Sigma_\text{total} t},$$

감쇠 길이는 $1/\Sigma_\text{total}$ 입니다. 흡수 부분은 중성자 속도 $v$ (따라서 파장)에 의존합니다: 대부분의 핵종에서 핵 근처에 머무는 시간은 $1/v$ 로 스케일하여 **1/v 법칙**을 줍니다

$$\sigma_\text{abs}(\lambda) = \sigma_\text{abs}(\lambda_0)\,\frac{\lambda}{\lambda_0}, \qquad \lambda_0 = 1.798\ \text{Å}\ (\text{thermal}, 2200\ \text{m/s}).$$

몇몇 강한 흡수체(Cd, Sm, Eu, Gd)는 단순한 1/v 스케일링을 위반하는 저에너지 **공명**을 가지며, ReciPro 는 이 핵종들을 표시합니다. 가간섭 **산란 길이 밀도** $\text{SLD} = \sum_i n_i\, b_{\text{coh},i}$ 는 위의 X선 SLD의 중성자 대응물입니다.

---

## 한눈에 보는 침투

세 가지 빔은 매우 다른 깊이를 탐침합니다 — 이것이 서로 다른 질문에 답하는 실질적 이유입니다:

| 빔 | 전형적 시료 | 침투 (자릿수) | 결정 요인 |
|---|---|---|---|
| X선 (≈8 keV) | 분말 / 단결정 | 10–100 µm | $\mu = \rho(\mu/\rho)$ |
| 전자 (≈200 keV) | TEM 박막 | 10–100 nm (유용) | 탄성 MFP + 비탄성 손실 |
| 중성자 (열) | 벌크, cm 크기 | 1–10 cm | $\Sigma_\text{total}$ |

동일한 길이 척도가 전자가 왜 극박 시료와 동역학적 이론을 요구하는지, 반면 중성자는 단일 산란 운동학 하에서 벌크 시료 전체를 보는지를 설명합니다.

---

## 함께 보기

- [원자 산란 인자](scattering-factor.md) — Rayleigh/Compton 뒤의 $F(q)$/$S(q)$ 분할, 그리고 Mott 단면적.
- [형광](fluorescence.md) — X선 광흡수 뒤에 이어지는 완화.
- [3. 빔 상호작용](../../3-beam-interaction.md) — *감쇠 & 수송* 탭.
- [8. 전자 궤적](../../8-electron-trajectory.md) · [12. EBSD 시뮬레이션](../../12-ebsd-simulation.md) — 전자 비정거리가 사용되는 곳.
