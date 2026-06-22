# STEM 계산

STEM 이미지 계산은 [CBED](cbed.md)와 동일한 수렴 프로브 표현에서 출발한다. 차이는 관측량에 있다: CBED는 회절면에서 디스크 강도를 표시하는 반면, STEM은 프로브 위치를 주사하면서 각 위치에서 선택된 검출기로 들어오는 강도를 적분한다.

---

## 관측량

$\mathbf R_0$를 프로브 위치, $\mathbf Q$를 회절면 좌표, $t$를 시료 두께라고 하자. 검출기 함수 $D(\mathbf Q)$가 검출기 각도 범위 안에서는 1이고 밖에서는 0이라면, 탄성 STEM 강도는 다음과 같다

$$I_{\mathrm{STEM}}^{\mathrm{ela}}(\mathbf R_0)=
\int D(\mathbf Q)\,
\left|\psi(\mathbf Q,t;\mathbf R_0)\right|^2\,d\mathbf Q$$

BF, ABF, LAADF, HAADF는 $D(\mathbf Q)$에서 내각과 외각을 서로 다르게 선택한 경우에 해당한다. 따라서 STEM 검출기 각도를 바꾸면 적분되는 물리량 자체가 바뀐다. 이는 단순한 표시 설정이 아니다.

---

## 푸리에 계수 가속

직접적인 구현은 주사된 모든 프로브 위치 $\mathbf R_0$에 대해 동역학적 문제를 다시 푸는 방식이 된다. 수렴 프로브 표현식은 유용한 구조를 가진다: $\mathbf R_0$ 의존성이 위상 인자로 나타난다

$$\exp(-2\pi i\,\mathbf K\cdot\mathbf R_0)$$

이 덕분에 ReciPro는 $I_{\mathrm{STEM}}(\mathbf R_0)$를 점마다 계산하는 대신, 먼저 이미지의 2차원 푸리에 계수를 계산할 수 있다. 개념적으로

$$I_{\mathrm{STEM}}^{\mathrm{ela}}(\mathbf q)=
\sum_{\mathbf g,\mathbf h}
F_{\mathbf g,\mathbf h}(t)\,
\delta(\mathbf q-\mathbf g+\mathbf h)$$

이므로, 계수 $F_{\mathbf g,\mathbf h}(t)$를 알고 나면 역 푸리에 변환을 통해 전체 주사 이미지를 효율적으로 재구성할 수 있다.

이것이 작은 단위 격자를 가진 완전 결정에 대한 블로흐파 STEM의 주요 이점이다. 모든 프로브 위치에서 멀티슬라이스 계산을 반복하는 것보다 훨씬 빠를 수 있다.

---

## TDS 및 검출기 선택 흡수

HAADF-STEM에서는 열 확산 산란(TDS)에서 오는 비탄성 성분이 이미지 대비의 주요 원천인 경우가 많다. ReciPro는 TDS를 탄성 채널에서 선택된 각도 범위로 제거되는 강도의 양으로 다루며, 이를 흡수 퍼텐셜로 표현한다.

검출기 각도 범위 $\theta_1\leq\theta\leq\theta_2$에 대해, 검출기 선택 흡수 산란 인자는 개념적으로 다음과 같이 쓸 수 있다

$$f'_{\kappa}(\mathbf g;\theta_1,\theta_2)=
\int_{\theta_1}^{\theta_2}\sin\theta\,d\theta
\int_0^{2\pi}
\left|\Delta f_{e,\kappa}(\mathbf g,\theta,\phi)\right|^2\,d\phi$$

이 범위를 BF, ADF, HAADF 검출기에 맞게 선택하면 해당 검출기로 들어오는 TDS 기여가 평가된다.

STEM TDS 강도는 검출기 선택 흡수의 두께 적분이다:

$$I_{\mathrm{STEM}}^{\mathrm{TDS}}(\mathbf R_0)=
\int_0^t
\langle\psi(z;\mathbf R_0)|\widehat W_{\mathrm{det}}|\psi(z;\mathbf R_0)\rangle\,dz$$

여기서 $\widehat W_{\mathrm{det}}$는 검출기 선택 TDS를 나타낸다. 블로흐파 고유값과 고유벡터를 알고 나면 이 $z$ 적분은 해석적으로 처리할 수 있다. 수치 슬라이스 적분도 가능하며, ReciPro는 계산 모드에 따라 적절한 방식을 사용한다.

---

## 국소 및 비국소 흡수

흡수 퍼텐셜은 크게 두 가지 방식으로 다룰 수 있다.

| 형식 | 의미 | 특징 |
|------|---------|---------|
| 국소 근사 | 위치에만 의존하는 흡수 퍼텐셜 $U'(\mathbf r)$를 사용한다. | 넓은 ADF / HAADF 검출기에 대해 대개 효과적이고 빠르다. |
| 비국소 형식 | 입사파와 출사파의 쌍에 의존하는 $U'(\mathbf r,\mathbf r')$ 또는 행렬 요소 $U'_{\mathbf g,\mathbf h}$를 사용한다. | 좁은 검출기, 무거운 원소, 또는 낮은 가속 전압에 대해 더 정확하지만, 훨씬 비용이 크다. |

국소 근사에서는 행렬 요소를 $U'_{\mathbf g-\mathbf h}$와 같은 역격자 벡터 차이로부터 평가할 수 있다. 비국소 형식에서는 각 $(\mathbf g,\mathbf h)$ 쌍마다 고유한 각도 적분이 필요하므로, 빔 수가 늘어남에 따라 비용이 빠르게 증가한다.

---

## 블로흐파 STEM의 적용 범위

블로흐파 STEM은 고도로 주기적인 완전 결정에 대해 빠르며, 두께, 디포커스, 검출기 각도의 체계적인 비교에 적합하다. 결함, 큰 슈퍼셀, 또는 비주기적 구조에 대해서는 frozen-phonon 멀티슬라이스와 같은 방법이 더 적절할 수 있는데, 이는 동일한 작은 주기 격자 가정에 의존하지 않기 때문이다.

ReciPro에서 STEM은 다음과 같이 이해하는 것이 가장 쉽다: CBED와 동일한 수렴파에서 시작한 다음, 회절 디스크 관측량을 회절면에 대한 검출기 적분으로 대체한다.

---

## 실용 매개변수

- **검출기 각도**: BF / ABF / ADF / HAADF는 $D(\mathbf Q)$와 $f'_{\kappa}(\mathbf g;\theta_1,\theta_2)$의 정의이다.
- **빔 수**: 고주파 이미지 성분과 채널링은 포함되는 빔의 수에 민감하다.
- **두께 단계**: 수치 슬라이스 적분을 사용하는 경우, 슬라이스 두께를 절반으로 줄였을 때의 변화를 확인하라.
- **TDS 모델**: HAADF $Z$-대비의 경우, TDS 항은 탄성 항만큼 중요하다.

## 함께 보기

- [동역학적 계산 (공통 코어)](calculation.md)
- [부록 A3. 블로흐파 방법에 의한 동역학적 회절](index.md)
- [9.2. STEM 시뮬레이션](../../9-hrtem-stem-simulator/2-stem-simulation.md)
