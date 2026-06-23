# 구조 인자

원자 산란 인자는 하나의 원자를 기술하지만, **구조 인자**는 단위 격자 안의 모든 원자가 어떻게 *함께* 산란하는지를 기술한다. 이것은 **Reflections** 탭이 표로 정리하는 양이며(`F_real`, `F_inv`, $\lvert F\rvert$, $F^2$), 앞 페이지의 원자 물리와 회절 강도를 잇는 다리이다.

=== "X-ray"
    ![Reflections — X-ray](../../../assets/cap-ko-auto/FormBeamInteraction-xray-reflections.png)

=== "Electron"
    ![Reflections — electron](../../../assets/cap-ko-auto/FormBeamInteraction-electron-reflections.png)

=== "Neutron"
    ![Reflections — neutron](../../../assets/cap-ko-auto/FormBeamInteraction-neutron-reflections.png)

---

## 단위 격자에 걸친 간섭

반사 $\mathbf g = (hkl)$ 의 구조 인자는 원자 인자들의 결맞은 합이며, 각 원자의 분율 위치 $\mathbf r_j = (x_j,y_j,z_j)$ 에서 오는 위상으로 가중된다:

$$F_{\mathbf g} = \sum_{j} o_j\, f_j(s,E)\, T_j(\mathbf g)\, \exp\!\left(-2\pi i\,(h x_j + k y_j + l z_j)\right).$$

- $o_j$ : 자리 **점유율**(occupancy, 분율값, 부분 또는 혼합 점유의 경우).
- $f_j(s,E)$ : 현재 빔에 대한 원자 $j$ 의 원자 산란 인자 — ReciPro의 [위상 규약](index.md#phase-convention)에서 X선의 경우 $f_0+f'-if''$, 전자의 경우 $f_e$, 중성자의 경우 $b$.
- $T_j(\mathbf g)$ : 디바이-월러 인자(아래 참조).
- $-2\pi i$ 위상은 ReciPro의 [규약](index.md#phase-convention)을 따른다.

강도는 모듈러스의 제곱이며,

$$I_{\mathbf g} \;\propto\; \lvert F_{\mathbf g}\rvert^2 = F_\text{real}^2 + F_\text{inv}^2 ,$$

이것이 표의 $F^2$ 열이다. `F_real` 과 `F_inv` 는 복소 구조 인자의 실수부와 허수부이다. 원자 인자가 순수 실수일지라도, 비중심대칭 구조(또는 이동된 원점)에서는 $F_{\mathbf g}$ 가 일반적으로 복소수이다. X선 이상 분산(복소 $f$)과 복소 중성자 산란 길이는 추가적인 허수 기여를 더한다. `F_inv` 가 *모든* 반사에 대해 0이 되는 것은 구조가 중심대칭이고 원점이 대칭 중심에 있으며 모든 인자가 실수일 때뿐이다.

---

## 디바이-월러 인자

원자는 평형 자리 주위로 진동하여 산란 밀도를 번지게 하고 고각 인자를 감소시킨다. 등방성 운동의 경우,

$$T_j = \exp\!\left(-B_j\, s^2\right), \qquad B_j = 8\pi^2\langle u_j^2\rangle,$$

여기서 $\langle u_j^2\rangle$ 는 산란 방향을 따른 평균 제곱 변위이고 $B_j$ 는 등방성 변위 파라미터(Å²)이다. 비등방성 운동은 이를 다음으로 일반화한다

$$T_j = \exp\!\left(-2\pi^2\,\mathbf g^{\mathsf T}\!\mathbf U_j\,\mathbf g\right),$$

여기서 $\mathbf U_j$ 는 변위 텐서이고 $\mathbf g$ 는 역격자 벡터($|\mathbf g|=1/d$, $Q=2\pi\lvert\mathbf g\rvert$ 가 아님)이다. 디바이 고체의 경우 평균 제곱 변위 자체가 온도 $T$, 원자 질량 $M$, 디바이 온도 $\Theta_D$ 의 함수이다,

$$\langle u^2\rangle = \frac{3\hbar^2}{M k_B \Theta_D}\left[\frac14 + \left(\frac{T}{\Theta_D}\right)^2\!\int_0^{\Theta_D/T}\frac{x}{e^x-1}\,dx\right],$$

그래서 $B$ 는 온도에 따라 증가하고 무거운 원자에서는 감소한다. ReciPro는 이를 계산하기보다 표로 주어진 또는 입력된 $B_j$ 를 직접 사용한다. $T_j$ 가 산란 인자에 곱해지므로 **Scattering factors** 탭은 동일한 $e^{-Bs^2}$ 감쇠를 그려진 곡선에 적용할 수 있다. 감쇠는 온도와 $s$ 에 따라 커지며, 이것이 열 확산 산란(결맞은 브래그 빔에서 제거되어 확산 배경으로 재분배되는 강도)이 동역학 이론에서 흡수 퍼텐셜을 공급하는 이유이다([부록 A3](../a3-bloch-wave/index.md)).

---

## 소광: 체계적 vs 우연적

반사는 서로 다른 두 가지 이유로 **부재**할 수 있다:

- **체계적(공간군) 부재.** 격자 중심화와 병진 성분을 갖는 대칭 요소(나선축, 미끄럼면)는 그 공간군의 모든 결정에 대해 원자 내용물과 무관하게 반사의 전체 부류를 *정확히* 사라지게 한다. 이것이 **Hide prohibited planes** 뒤에 있는 규칙이다.
- **우연적 준소광.** 특정 구조에서 원자 기여가 우연히 상쇄되면, 강도는 작지만 대칭적으로 금지된 것은 아니며, 조성이나 위치가 바뀌면 다시 나타날 수 있다. 이것들은 소광 규칙으로 *제거되지 않는다*.

체계적 부재는 격자의 대칭 관련 사본들 사이의 위상 상쇄이다. 중심화 병진 $\mathbf t_\alpha$ 에 대해 구조 인자는 공통 인자를 갖는다

$$F_{\mathbf g} \propto \sum_\alpha e^{-2\pi i\,\mathbf g\cdot\mathbf t_\alpha},$$

이것은 특정 $hkl$ 에 대해 0이다. 체심 중심화($\mathbf t = \tfrac12,\tfrac12,\tfrac12$)의 경우,

$$1 + e^{-\pi i (h+k+l)} = 0 \quad\Longleftrightarrow\quad h+k+l \ \text{odd}.$$

가장 흔한 체계적 부재는 다음과 같다:

| 대칭 요소 | 부재 조건 | 영향받는 반사 |
|---|---|---|
| $I$ (체심) | $h+k+l$ 홀수 | 모든 $hkl$ |
| $F$ (면심) | $h,k,l$ 혼합 패리티 | 모든 $hkl$ |
| $C$ (C-중심) | $h+k$ 홀수 | 모든 $hkl$ |
| $2_1$ 나선축 $\parallel b$ | $k$ 홀수 | $0k0$ |
| $a$-미끄럼면 $\perp b$ | $h$ 홀수 | $h0l$ |
| $c$-미끄럼면 $\perp b$ | $l$ 홀수 | $h0l$ |

중심화 조건은 모든 반사에 적용되지만, 나선 및 미끄럼 조건은 해당 축 행 또는 영역(zone)에만 적용되며, 이것이 바로 그것들을 공간군의 진단 지표로 만드는 점이다.

---

## 프리델 법칙과 그 붕괴

실수(비공명) 산란 인자를 갖는 구조의 경우, 합을 켤레화하고 $\mathbf g$ 의 부호를 뒤집으면 (명료함을 위해 실수 가중치 $o_j T_j$ 를 생략하면) 다음이 직접 성립함을 보일 수 있다

$$F_{-\mathbf g} = \sum_j f_j\, e^{+2\pi i\,\mathbf g\cdot\mathbf r_j} = \left(\sum_j f_j\, e^{-2\pi i\,\mathbf g\cdot\mathbf r_j}\right)^{*} = F_{\mathbf g}^{*}, \qquad\text{hence}\qquad \lvert F_{hkl}\rvert = \lvert F_{\bar h\bar k\bar l}\rvert \quad\text{(Friedel's law).}$$

그러면 결정이 그렇지 않더라도 회절은 중심대칭으로 보인다. **이상 분산이 이를 깨뜨릴 수 있다.** 구조 인자를 (깔끔하게 켤레화되는) 정상부와 이상부의 합으로 쓰면, ReciPro의 $f = f_0 + f' - i f''$ 규약에서 $F_{\mathbf g} = A_{\mathbf g} - i B_{\mathbf g}$ 그리고 $F_{-\mathbf g} = A_{\mathbf g}^{*} - i B_{\mathbf g}^{*}$ 이고, **바이푸트 차이(Bijvoet difference)** 는

$$\lvert F_{\mathbf g}\rvert^2 - \lvert F_{-\mathbf g}\rvert^2 = -4\,\operatorname{Im}\!\left(A_{\mathbf g}\, B_{\mathbf g}^{*}\right),$$

정상부와 이상부의 위상이 다를 때에만 0이 아니다 — 즉, 화학적으로 구별되는 이상 산란체가 비중심대칭 자리를 차지할 때이다. (이 차이는 중심대칭 구조, 단일 원소, 또는 모든 원자가 동일한 복소 인자를 갖는 어떤 경우에 대해서도 사라진다.) 이것이 비중심대칭 결정의 절대 구조(손대칭성)를 결정할 수 있게 해주며, 흡수단 근처의 X선 에너지를 선택하면 ReciPro가 프리델 쌍에 대해 0이 아닌 `F_inv` 와 서로 다른 $\lvert F\rvert$ 를 보고하는 물리적 이유이다.

---

## 구조 인자에서 분말 강도로

**Powder Diffraction Intensities (Bragg–Brentano)** 를 켜면 무작위로 배향된 다결정의 기하학을 접어 넣어 $\lvert F\rvert^2$ 를 상대 분말 강도로 변환한다:

$$I_{hkl} \;\propto\; m_{hkl}\, \lvert F_{hkl}\rvert^2\, L p(\theta),$$

- $m_{hkl}$ : **다중도** — 동일한 $2\theta$ 에서 겹치는 대칭 등가 면의 수(표의 *Multi.* 열).
- $Lp(\theta)$ : 브래그-브렌타노 광학계에 대한 **로런츠-편광** 인자, $Lp = \dfrac{1+\cos^2 2\theta}{\sin^2\theta\,\cos\theta}$, 저각 피크를 강하게 증폭한다.

이 모드에서는 등가 면이 하나의 선으로 병합되므로, ReciPro는 또한 *Hide equivalent planes* 와 *Hide prohibited planes* 를 강제로 켠다.

---

## 같이 보기

- [원자 산란 인자](scattering-factor.md) — 합에 들어가는 $f_j$.
- [감쇠 & 수송](attenuation-transport.md) — 산란 사건 사이에 빔에 무슨 일이 일어나는가.
- [3. 빔 상호작용 → Reflections 탭](../../3-beam-interaction.md#reflections-tab)
- [부록 A3. 동역학적 회절](../a3-bloch-wave/index.md) — $\lvert F\rvert^2$ (운동학적)으로 더 이상 충분하지 않을 때.
