# HRTEM 상 형성

HRTEM 상은 출사면 파동함수 — [동역학적 코어](calculation.md)에서 얻은 투과 계수 $T_{\mathbf g}$ — 를 대물렌즈를 통과시켜 형성된다. ReciPro는 두 가지 모델을 제공한다: 빠른 **준가간섭(quasi-coherent)** 근사와 보다 엄밀한 **투과 교차 계수(transmission cross coefficient, TCC)** 모델이다. [HRTEM 시뮬레이터](../../9-hrtem-stem-simulator/1-hrtem-simulation.md) GUI 페이지도 참조하라.

---

## 기호

| 기호 | 의미 |
|--------|---------|
| $\mathbf R$ | 실공간(상면)에서의 X–Y 성분 |
| $\mathbf K$ | 입사 파동벡터의 X–Y 성분 |
| $\mathbf G, \mathbf H$ | 역격자 벡터의 X–Y 성분 |
| $\mathbf u$ | 공간 주파수 (예: $\mathbf K+\mathbf G$) |
| $\chi(\mathbf u)$ | 렌즈 수차 함수 |
| $A(\mathbf u)$ | 대물 조리개 함수 |
| $\Delta f$ | 디포커스 값 |
| $C_s$ | 구면 수차 계수 |
| $C_c$ | 색 수차 계수 |
| $\beta$ | 조명 반각 (유한 광원 크기) |
| $\Delta E$ | 전자 에너지 요동의 $1/e$ 폭 |
| $\Delta_0$ | 디포커스 분포의 $1/e$ 폭 (가우시안), $\Delta_0 = C_c\,\Delta E / E$ |

---

## 렌즈 수차 함수와 조리개

$$\chi(\mathbf u) = \pi\lambda\Delta f\, u^2 + \tfrac{1}{2}\pi\lambda^3 C_s\, u^4 = \pi\lambda u^2\!\left(\Delta f + \tfrac{1}{2}\lambda^2 C_s u^2\right)$$

$$A(\mathbf u) = \begin{cases} 1 & (\mathbf u\ \text{inside the objective aperture})\\[2pt] 0 & (\mathbf u\ \text{outside the objective aperture})\end{cases}$$

---

## 준가간섭 모델

빠른 근사: 각 회절빔은 렌즈 전달에 의해 변조되고 가간섭성 포락선에 의해 감쇠된 다음, 가간섭적으로 합산된다.

$$I(\mathbf R) = |\psi(\mathbf R)|^2$$

$$\psi(\mathbf R) = \sum_{\mathbf g} T_{\mathbf g}\,\exp\!\left[2\pi i(\mathbf K+\mathbf G)\cdot\mathbf R\right]\exp\!\left[-i\chi(\mathbf K+\mathbf G)\right]A(\mathbf K+\mathbf G)\,E_c(\mathbf K+\mathbf G)\,E_s(\mathbf K+\mathbf G)$$

여기서 **시간적** 및 **공간적 가간섭성 포락선**은

$$E_c(\mathbf u) = \exp\!\left[-\tfrac{1}{2}\left(\pi\lambda\Delta_0\, u^2\right)^2\right], \qquad E_s(\mathbf u) = \exp\!\left[-\pi^2\beta^2 u^2\!\left(\Delta f + \lambda^2 C_s u^2\right)^2\right]$$

---

## 투과 교차 계수(TCC) 모델

부분 가간섭성의 엄밀한 처리: 모든 빔 쌍 $(\mathbf g, \mathbf h)$ 이 투과 교차 계수를 통해 간섭한다.

$$I(\mathbf R) = \sum_{\mathbf g}\sum_{\mathbf h} T_{\mathbf g}\,T_{\mathbf h}^{*}\,\exp\!\left[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R\right]\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)$$

$$\mathrm{TCC}(\mathbf u, \mathbf u') = A(\mathbf u)\,A(\mathbf u')\,\exp\!\left[-i\{\chi(\mathbf u)-\chi(\mathbf u')\}\right]E_c(\mathbf u, \mathbf u')\,E_s(\mathbf u, \mathbf u')$$

여기서 **혼합** 가간섭성 포락선은

$$E_c(\mathbf u, \mathbf u') = \exp\!\left[-\tfrac{1}{2}\left(\pi\lambda\Delta_0\right)^2\!\left(u^2 - u'^2\right)^2\right]$$

$$E_s(\mathbf u, \mathbf u') = \exp\!\left[-\pi^2\beta^2\left\{\Delta f(\mathbf u-\mathbf u') + \lambda^2 C_s\!\left(u^2\mathbf u - u'^2\mathbf u'\right)\right\}^2\right]$$

극한 $\mathbf u' \to \mathbf u$ 에서 TCC는 위의 준가간섭 포락선으로 환원된다.

---

## TCC 모델의 계산 비용 절감

TCC 모델의 이중합은 빔 쌍마다 $\mathrm{TCC}$ 를 한 번씩 평가하므로 비용이 크다. 상 강도 $I(\mathbf R)$ 가 실수이므로, 비용을 대략 절반으로 줄일 수 있다.

첫째, 대물 조리개 바깥의 빔($A(\mathbf K+\mathbf G)=0$)은 기여하지 않으므로, **조리개 안쪽의 빔($A=1$)에 대해서만** 합산하면 충분하다.

둘째, TCC는 에르미트(Hermitian)이다,

$$\mathrm{TCC}(\mathbf u', \mathbf u) = \mathrm{TCC}(\mathbf u, \mathbf u')^{*}$$

($A$ 는 실수이고; $E_c, E_s$ 는 $\mathbf u\leftrightarrow\mathbf u'$ 교환에 대해 불변인 실함수이며; 위상항 $\exp[-i\{\chi(\mathbf u)-\chi(\mathbf u')\}]$ 은 복소 켤레가 된다). 이와 함께 $\exp[2\pi i(\mathbf H-\mathbf G)\cdot\mathbf R]=\bigl(\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\bigr)^{*}$ 및 $T_{\mathbf h}T_{\mathbf g}^{*}=\bigl(T_{\mathbf g}T_{\mathbf h}^{*}\bigr)^{*}$ 를 고려하면, $(\mathbf g,\mathbf h)$ 항과 $(\mathbf h,\mathbf g)$ 항은 서로 복소 켤레이므로, 그 합은 실수부의 두 배와 같다:

$$F(\mathbf g,\mathbf h) + F(\mathbf h,\mathbf g) = 2\,\mathrm{Re}\{F(\mathbf g,\mathbf h)\}, \qquad F(\mathbf g,\mathbf h) \equiv T_{\mathbf g}T_{\mathbf h}^{*}\,\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\,\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)$$

따라서 이중합은 대각 성분에 상삼각(빔에 임의의 순서를 부여하면 한쪽 면)을 더한 것으로 환원되어, $\mathrm{TCC}$ 평가 횟수를 절반으로 줄인다:

$$I(\mathbf R) = \sum_{\mathbf g} |T_{\mathbf g}|^2\,A(\mathbf K+\mathbf G)^2 \;+\; 2\sum_{\mathbf g}\sum_{\mathbf h > \mathbf g} \mathrm{Re}\!\left\{ T_{\mathbf g}T_{\mathbf h}^{*}\,\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\,\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)\right\}$$

대각 항의 경우 $\mathrm{TCC}(\mathbf u,\mathbf u)=A(\mathbf u)^2$, 즉 조리개 안쪽에서 $|T_{\mathbf g}|^2$ 이다.

나아가, 위상 인자 $\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]$ 는 이 합 안에서 동일한 값을 여러 번 가진다. 이 값들을 저장해 두고 재사용하면 계산이 더욱 빨라진다.

---

## 참고

- [동역학적 계산 (공통 코어)](calculation.md) — 공유 블로흐파 코어와 투과 계수 $T_{\mathbf g}$
- [부록 A3. 블로흐파 방법에 의한 동역학적 회절](index.md)
- [9.1. HRTEM 시뮬레이션](../../9-hrtem-stem-simulator/1-hrtem-simulation.md)
