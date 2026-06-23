# 부록 A2. 빔 상호작용 (고체물리학적 배경)

메인 창에 관한 장 [3. Beam interaction](../../3-beam-interaction.md)은 GUI 안내서입니다. 어떤 버튼을 눌러야 하는지, 각 열이 무엇을 의미하는지 알려줍니다. 이 부록은 그 수치들 뒤에 있는 **고체물리학 및 산란 물리학**을 모읍니다 — 왜 원자가 X선, 전자, 중성자를 그토록 다르게 산란시키는지, 구조 인자와 그 허수부가 어디에서 비롯되는지, 빔이 고체 내부에서 어떻게 감쇠되고 느려지는지, 그리고 형광 미리보기가 무엇을 나타내고 무엇을 나타내지 않는지를 다룹니다.

![Beam Interaction window](../../../assets/cap-ko-auto/FormBeamInteraction.png)

이 창에는 네 개의 탭이 있으며, 이론은 한 양이 다음 양으로 이어지는 순서대로 읽는 것이 가장 좋습니다:

1. **[Atomic scattering factors](scattering-factor.md)** — *단일 원자*가 각 종류의 빔을 어떻게 산란시키는지.
2. **[Structure factor](structure-factor.md)** — *단위 격자* 안의 원자들이 어떻게 간섭하는지, 디바이-월러 인자와 소광 규칙을 포함하여.
3. **[Attenuation & transport](attenuation-transport.md)** — 빔이 물질을 통과하면서 어떻게 *제거되고 느려지는지*.
4. **[Fluorescence](fluorescence.md)** — 내각 이온화에 뒤따르는 특성 X선 방출.

---

## 산란 기하학과 변수 $s$

이 창의 모든 산란 양은 빔 방향이 얼마나 변하는지에 대한 함수입니다. 입사 및 산란 파동벡터를 $\mathbf k_i$ 와 $\mathbf k_s$ 로 쓰면 (탄성 산란이므로 $|\mathbf k_i|=|\mathbf k_s|=1/\lambda$), **산란 벡터**와 그 크기는 다음과 같습니다

$$\mathbf Q = 2\pi(\mathbf k_s - \mathbf k_i), \qquad Q = |\mathbf Q| = \frac{4\pi\sin\theta}{\lambda} = 4\pi s .$$

- $\theta$ : 브래그 각 — 전체 산란각의 *절반*. Reflections 표에는 전체 각 $2\theta$ 가 나열됩니다.
- $s = \dfrac{\sin\theta}{\lambda}$ (Å⁻¹) : **Scattering factors** 탭이 이에 대해 그려지는 변수. 모든 원자 형상 인자의 자연스러운 인수입니다.
- $d$ : 면간 거리. 브래그 조건 $\lambda = 2d\sin\theta$ 에서 $s = \dfrac{1}{2d} = \dfrac{|\mathbf g|}{2}$ 이며, 여기서 $\mathbf g$ 는 $|\mathbf g| = 1/d$ 인 역격자 벡터입니다.

이 세 가지 관례는 같은 기하학을 기술하며, 척도만 다릅니다. 창이 그중 둘 이상을 사용하므로 그 대응 관계를 명확히 해 두는 것이 좋습니다:

| 창에서 | 기호 | 관계 |
|---|---|---|
| Reflections 표 | $q = 2\pi/d$ | $q = 2\pi\lvert\mathbf g\rvert = Q = 4\pi s$ |
| Reflections 표 | $2\theta$ | 전체 산란각, $\sin\theta = \lambda s$ |
| Scattering factors 탭 | $s = \sin\theta/\lambda$ | $s = q/4\pi = 1/(2d)$ |
| 회절 피크 그림 | $Q = 4\pi\sin\theta/\lambda$ | $Q = q = 4\pi s$ |

!!! note "단위"
    공개된 형상 인자 매개변수화는 $s$ 를 Å⁻¹ 단위로 사용하지만 (따라서 $s^2$ 은 Å⁻² 단위), ReciPro는 내부적으로 $s^2$ 을 nm⁻² 단위로 다룹니다. 둘은 $s^2$ 에서 인수 $100$ 만큼 차이가 납니다. 곡선과 표는 각 표의 머리글에 명시된 단위로 제시됩니다. 한 모델 — **Kirkland** — 은 $s$ 대신 $q = 2s = 1/d$ 에 대해 표로 정리되어 있습니다. [Atomic scattering factors](scattering-factor.md)를 참조하세요.

### 브래그, 라우에, 그리고 에발트 구 {#phase-convention}

브래그 조건은 단일한 기하학적 요구의 한 측면입니다. 보강 간섭(**라우에 조건**)은 산란 벡터가 역격자 벡터와 같을 것을 요구하며,

$$\mathbf k_s = \mathbf k_i + \mathbf g, \qquad |\mathbf k_i + \mathbf g|^2 = |\mathbf k_i|^2 ,$$

이는 $|\mathbf k_i|=|\mathbf k_s|=1/\lambda$ 에서 다음으로 환원됩니다

$$2\,\mathbf k_i\cdot\mathbf g + |\mathbf g|^2 = 0 \qquad\Longleftrightarrow\qquad |\mathbf g| = \frac{1}{d} = \frac{2\sin\theta}{\lambda},$$

즉 **브래그 법칙** $\lambda = 2d\sin\theta$ 입니다. 기하학적으로 이것은 **에발트 구** 작도입니다: 반사는 그 역격자점이 반지름 $1/\lambda$ 인 구 위에 놓일 때 여기됩니다. (여기서 $\mathbf g$ 는 $1/d$ 단위이므로 $\mathbf Q = 2\pi\mathbf g$ 입니다.)

---

## 위상 관례

ReciPro는 결정학적 위상 관례로 구조 인자를 구성합니다

$$F_{\mathbf g} = \sum_j \dots \exp\!\left(-2\pi i\,\mathbf g\cdot\mathbf r_j\right),$$

즉 지수에 **마이너스** 부호가 있습니다. 이 선택은 구조 인자 허수부(Reflections 표의 `F_inv`)의 부호와, 변칙 분산이 켜졌을 때 프리델 쌍 사이의 관계를 고정합니다. 여기서 한 번 명시하고 부록 전체에 걸쳐 가정합니다. 그 결과는 [Structure factor](structure-factor.md)에서 다룹니다.

---

## 운동학적 산란 대 동역학적 산란

이 부록은 **단일 (운동학적) 산란**을 다룹니다: 입사빔이 한 번 산란하고, 회절 진폭은 다음 페이지의 구조 인자입니다. 이것은 상호작용이 약할 때 옳은 그림입니다 — 거의 모든 시료에서의 X선과 중성자, 그리고 *매우 얇은* 시료에서의 전자.

상호작용이 강할 때 — 가장 얇은 결정을 제외한 모든 결정에서의 전자 — 빔은 빠져나가기 전에 여러 번 산란하고, 세기는 반사들 사이에 재분배되며, $\lvert F\rvert^2$ 은 더 이상 측정된 세기를 주지 않습니다. 그 영역은 [Appendix A3](../a3-bloch-wave/index.md)의 **동역학적** 이론을 필요로 합니다. 여기서 유도한 산란 인자와 구조 인자는 두 그림 모두의 *입력*입니다.

운동학적 극한에서조차 회절 진폭은 구조 인자만이 아닙니다: 두께 $t$ 인 슬래브를 통과하여 산란파를 합하면 다음을 얻습니다

$$A_{\mathbf g}(t) \;\propto\; F_{\mathbf g}\int_0^t e^{\,2\pi i S_{\mathbf g} z}\,dz = F_{\mathbf g}\, t\, e^{\,\pi i S_{\mathbf g} t}\,\operatorname{sinc}(\pi S_{\mathbf g} t),$$

여기서 $S_{\mathbf g}$ 는 **여기 오차** — 역격자점이 에발트 구로부터 떨어진 거리 — 입니다. 세기는 $S_{\mathbf g}=0$ 에서 날카롭게 최대가 되고 두께에 따라 진동합니다 (두께 줄무늬의 기원). [Appendix A3](../a3-bloch-wave/index.md)의 동역학적 이론은 이 단일빔 결과를 결합빔 거동으로 대체합니다.

---

## 세 가지 탐침 한눈에 보기

| | X선 | 전자 | 중성자 |
|---|---|---|---|
| 상호작용 대상 | 전자 밀도 $\rho_e$ | 정전기 퍼텐셜 $V$ | 원자핵 (및 짝짓지 않은 스핀) |
| 상호작용 세기 | 약함 | 강함 | 매우 약함 |
| 일반적인 침투 깊이 | µm – mm | nm – µm | mm – cm |
| 단일 산란 유효? | 거의 항상 | 얇은 박편만 | 거의 항상 |
| 가벼운 원자에 대한 감도 | 나쁨 ($\propto Z$) | 보통 | 종종 매우 우수 |

이러한 대비는 다음 페이지들에 걸쳐 반복적으로 나타나며, 각각 [Atomic scattering factors](scattering-factor.md)의 산란 메커니즘으로 거슬러 올라갈 수 있습니다.

---

## 함께 보기

- [3. Beam interaction](../../3-beam-interaction.md) — 이 부록이 설명하는 GUI.
- [Atomic scattering factors](scattering-factor.md) · [Structure factor](structure-factor.md) · [Attenuation & transport](attenuation-transport.md) · [Fluorescence](fluorescence.md)
- [Appendix A1. Coordinate systems](../a1-coordinate-system/1-orientation.md)
- [Appendix A3. Dynamical diffraction (Bloch-wave method)](../a3-bloch-wave/index.md) — 이 산란 인자들을 사용하는 다중 산란 이론.
