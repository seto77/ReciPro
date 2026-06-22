# 부록 A3. 블로흐파 방법에 의한 동역학적 회절

이 부록은 ReciPro의 **회절 시뮬레이터**, **CBED**, **HRTEM/STEM** 시뮬레이터가 사용하는 동역학적 전자 회절 이론의 개요를 제공합니다. ReciPro는 **Bethe / 블로흐파** 정식화를 따릅니다. 단계별 계산(광학 퍼텐셜, 투과 계수, 강도)은 [동역학적 계산(공통 핵심)](calculation.md)에 설명되어 있습니다.

---

## 결정 내의 파동 방정식

결정의 주기적 정전기 퍼텐셜을 통과하는 고속 전자는 (고에너지, 정상 상태) 슈뢰딩거 방정식을 따르며, 이는 다음과 같이 쓸 수 있습니다.

$$\nabla^2 \Psi(\mathbf{r}) + 4\pi^2\left\{\, k_{vac}^2 + \sum_{\mathbf g} U_{\mathbf g}\, e^{2\pi i\,\mathbf g\cdot\mathbf r} \right\}\Psi(\mathbf{r}) = 0$$

- $k_{vac}$ : 진공 중 전자의 파수.
- $U_{\mathbf g}$ : 역격자 벡터 $\mathbf g$에 대한 결정 퍼텐셜의 푸리에 성분. 퍼텐셜은 격자 주기성을 가지므로, 역격자에 걸친 푸리에 급수로 표현됩니다.

---

## 블로흐 정리

퍼텐셜이 결정 격자의 주기성을 가지므로, 해는 **블로흐파**입니다.

$$\Psi(\mathbf{r}) = b\!\left(\mathbf{k}^{(j)}, \mathbf{r}\right) = u(\mathbf{r})\exp\!\left(2\pi i\,\mathbf{k}^{(j)}\cdot\mathbf{r}\right)$$

- $u(\mathbf r)$ : 결정 격자와 동일한 주기성을 가지는 함수이므로, 그 자체를 역격자에 걸쳐 전개할 수 있습니다. $u(\mathbf r)=\sum_{\mathbf g} C_{\mathbf g}^{(j)}\exp(2\pi i\,\mathbf g\cdot\mathbf r)$.
- $\mathbf{k}^{(j)}$ : $j$번째 블로흐 파동벡터.
- $C_{\mathbf g}^{(j)}$ : $j$번째 블로흐파에서 빔 $\mathbf g$의 진폭(고유벡터 성분).

---

## Bethe의 동역학적 방정식

블로흐파 전개를 파동 방정식에 대입하면 **Bethe의 동역학적 방정식**이 얻어집니다 — 각 빔 $\mathbf g$에 대한 하나의 연립 방정식입니다.

$$\left[\,k^2 - \left(\mathbf{k}^{(j)} + \mathbf{g}\right)^2 + i\,U'_{g,g}\right]C_{\mathbf g}^{(j)} + \sum_{h \neq g}\left(U^C_{g-h} + i\,U'_{g,h}\right)C_{\mathbf h}^{(j)} = 0$$

- $U^C_{\mathbf g}$ : **탄성** 산란에 대한 결정 퍼텐셜.
- $U'_{\mathbf g}$ : **열 확산 산란**(TDS)을 고려하는 허수(**흡수**) 퍼텐셜. 이것과 디바이-월러 인자가 어떻게 들어가는지는 [계산 핵심](calculation.md)에 상세히 설명되어 있습니다.

---

## 기하학적 정의 (에발트 구)

위에 나타나는 벡터와 스칼라는 에발트 구 위에서 정의됩니다.

![블로흐파 계산에 사용되는 벡터와 스칼라의 정의](../../../assets/references/Bloch.png){width=500px}

- $\hat{\mathbf n}$ : 결정 표면에 수직인 단위 벡터.
- $\mathbf k$ : 입사 파동벡터(그 끝점은 에발트 구 위에 있음); $\mathbf k_{vac}$는 진공 파동벡터.
- $\mathbf g$ : 역격자 벡터; $\mathbf k + \mathbf g$는 역격자점을 가리킴.
- $\mathbf k^{(j)}$ : $j$번째 블로흐 파동벡터. 모든 블로흐 파동벡터는 동일한 접선 성분을 가지며(표면을 가로지르는 연속성), $\hat{\mathbf n}$ 방향으로만 차이가 납니다. $\mathbf k^{(j)} = \mathbf k + \gamma^{(j)}\hat{\mathbf n}$.
- $\gamma^{(j)}$ : $j$번째 고유값($\mathbf k^{(j)}$의 $\hat{\mathbf n}$ 방향 성분으로, $\mathbf k$로부터 측정됨).

기하학으로부터,

$$P_g = 2\,\hat{\mathbf n}\cdot(\mathbf k + \mathbf g), \qquad Q_g = |\mathbf k|^2 - |\mathbf k + \mathbf g|^2 = -\,\mathbf g\cdot(2\mathbf k + \mathbf g)$$

그리고 **여기 오차** $S_g$(역격자점이 에발트 구로부터 벗어난 편차)와 반사를 순위화하는 데 사용되는 **평가 함수** $R$은 다음과 같습니다.

$$S_g = \frac{\sqrt{P_g^{\,2} + 4Q_g}\; -\; P_g}{2}, \qquad R = |\mathbf g|\,Q_g^{\,2}$$

---

## 고유값 문제로의 환원

$\mathbf{k}^{(j)} = \mathbf{k} + \gamma^{(j)}\hat{\mathbf n}$로 쓰고 $k^2-(\mathbf k+\mathbf g)^2 = Q_g$와 선형화 $(\mathbf k^{(j)}+\mathbf g)^2 \approx (\mathbf k+\mathbf g)^2 + \gamma^{(j)} P_g$를 함께 사용하면, Bethe의 방정식은 ($P_g$로 나눈 후) 표준 **행렬 고유값 문제**가 됩니다.

$$\mathbf{A}\,\mathbf{C} = \mathbf{C}\,\boldsymbol{\Lambda}, \qquad
A_{gh} = \frac{U^C_{\,g-h} + i\,U'_{g,h}}{P_g}\;\;(g\neq h), \qquad
A_{gg} = \frac{Q_g + i\,U'_{g,g}}{P_g}$$

- $\mathbf{C}$의 열은 고유벡터 $C^{(j)}_*$(블로흐파 진폭)입니다.
- $\boldsymbol{\Lambda}=\mathrm{diag}\!\left(\lambda^{(1)}, \lambda^{(2)}, \dots\right)$는 고유값 $\lambda^{(j)} = \gamma^{(j)}$를 담습니다.

명시적으로 풀어 쓰면 — 빔을 투과빔 $0$, 그다음 $g$, $h$, $\dots$ 순서로 배열하여 — 다음과 같습니다.

$$
\begin{aligned}
&\begin{pmatrix}
(Q_0 + i\,U'_{0,0})/P_0 & (U^C_{-g} + i\,U'_{0,g})/P_0 & (U^C_{-h} + i\,U'_{0,h})/P_0 & \cdots \\
(U^C_{g} + i\,U'_{g,0})/P_g & (Q_g + i\,U'_{g,g})/P_g & (U^C_{g-h} + i\,U'_{g,h})/P_g & \cdots \\
(U^C_{h} + i\,U'_{h,0})/P_h & (U^C_{h-g} + i\,U'_{h,g})/P_h & (Q_h + i\,U'_{h,h})/P_h & \cdots \\
\vdots & \vdots & \vdots & \ddots
\end{pmatrix}
\begin{pmatrix}
C^{(1)}_0 & C^{(2)}_0 & C^{(3)}_0 & \cdots \\
C^{(1)}_g & C^{(2)}_g & C^{(3)}_g & \cdots \\
C^{(1)}_h & C^{(2)}_h & C^{(3)}_h & \cdots \\
\vdots & \vdots & \vdots & \ddots
\end{pmatrix} \\[1.2ex]
&\qquad=
\begin{pmatrix}
C^{(1)}_0 & C^{(2)}_0 & C^{(3)}_0 & \cdots \\
C^{(1)}_g & C^{(2)}_g & C^{(3)}_g & \cdots \\
C^{(1)}_h & C^{(2)}_h & C^{(3)}_h & \cdots \\
\vdots & \vdots & \vdots & \ddots
\end{pmatrix}
\begin{pmatrix}
\lambda^{(1)} & 0 & 0 & \cdots \\
0 & \lambda^{(2)} & 0 & \cdots \\
0 & 0 & \lambda^{(3)} & \cdots \\
\vdots & \vdots & \vdots & \ddots
\end{pmatrix}
\end{aligned}
$$

$\mathbf{A}$를 대각화하면 **모든** 블로흐 파동벡터와 진폭을 한 번에 얻습니다. 회절빔의 진폭 — 따라서 강도 — 은 입사면과 출사면에서의 경계 조건 및 시료 두께로부터 따라옵니다. 그 단계들, 광학(복소) 퍼텐셜, 디바이-월러 인자, 투과 계수 $T_{\mathbf g}$는 [동역학적 계산(공통 핵심)](calculation.md)에 설명되어 있습니다.

> **참고:** 회절 시뮬레이터의 **Details** 표에 표시되는 $V_{\mathbf g}$ 값은 상대론적 보정 인자가 적용되기 *전*의 원시 값입니다.

---

## 함께 보기

- [7. 회절 시뮬레이터](../../7-diffraction-simulator/index.md) — 동역학적 회절 패턴
- [9. HRTEM/STEM 시뮬레이터](../../9-hrtem-stem-simulator/index.md)
- [부록 A1. 좌표계](../a1-coordinate-system/1-orientation.md)
- [동역학적 계산(공통 핵심)](calculation.md)
