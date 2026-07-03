# 동역학적 계산 (공통 코어)

ReciPro의 회절 및 결상 시뮬레이터는 공통 **블로흐파(Bethe) 동역학적 산란 코어**를 공유하며, 이 페이지에서 이를 설명한다 (결정 퍼텐셜, 디바이-월러 항과 흡수 항, 고유값 문제, 투과 계수, 강도). 각 방법별 프로토콜은 이 코어를 기반으로 한다:

- [평행빔 SAED](#parallel-beam-saed)
- [HRTEM 결상](hrtem.md)
- [CBED](cbed.md)
- [STEM](stem.md)
- [EBSD](ebsd.md)

기저 이론 (슈뢰딩거 방정식, 블로흐 정리, Bethe의 동역학 방정식, 고유값 문제, 에발트 구 정의)에 대해서는 [부록 A3. 블로흐파 방법에 의한 동역학적 회절](index.md)을 참조하라.

---

## 상수

$$\gamma = \frac{m}{m_0} = 1 + \frac{e_0 E}{m_0 c^2}, \qquad \beta = \frac{v}{c} = \sqrt{1 - \left(\frac{m_0}{m}\right)^2} = \sqrt{1 - \gamma^{-2}}$$

- $\gamma$ : 상대론적 보정 인자; $E$ : 가속 전압; $m_0$, $m$ : 전자의 정지 질량과 상대론적 질량.
- $\Omega$ : 단위 격자 부피.
- $k_{vac}$ : 진공 중 전자의 파수.

---

## 탄성 산란에 대한 결정 퍼텐셜

탄성 산란에 대한 결정 퍼텐셜의 푸리에 계수는, 위치 $\mathbf r_k$에 있는 원자 $k$에 대해 합산하여

$$U_{\mathbf g}^{C} = \gamma\,\frac{1}{\pi\Omega}\sum_k f_k(\mathbf g)\,\exp\!\left[2\pi i\,\mathbf g\cdot\mathbf r_k\right]T_k(\mathbf g, M_k)$$

로 주어진다. 여기서 **원자 산란 인자**는 가우스 매개변수화 $(a_i, b_i)$를 사용하며,

$$f_k(\mathbf g) = \sum_i a_i\exp\!\left[-b_i\,\frac{|\mathbf g|^2}{4}\right]$$

$T_k$는 **디바이-월러 (온도) 인자**이다. 등방성 온도 인자 $M_k$의 경우,

$$T_k(\mathbf g, M_k) = \exp\!\left[-M_k\,\frac{|\mathbf g|^2}{4}\right]$$

이고, 비등방성 원자 변위 텐서 $\mathbf U$의 경우,

$$T_k(\mathbf g) = \exp\!\left[-2\pi\,\mathbf g^{t}\mathbf U\,\mathbf g\right]$$

이며, 이차 형식은 다음과 같다:

$$\mathbf g^{t}\mathbf U\,\mathbf g = \begin{pmatrix} g_x & g_y & g_z\end{pmatrix}\begin{pmatrix} U_{11} & U_{12} & U_{13}\\ U_{12} & U_{22} & U_{23}\\ U_{13} & U_{23} & U_{33}\end{pmatrix}\begin{pmatrix} g_x\\ g_y\\ g_z\end{pmatrix} = g_x^2 U_{11} + g_y^2 U_{22} + g_z^2 U_{33} + 2\!\left(g_x g_y U_{12} + g_y g_z U_{23} + g_x g_z U_{13}\right)$$

$\mathbf g$의 직교 성분은 역격자 기저 벡터와 밀러 지수로부터 얻어진다:

$$\begin{pmatrix} g_x\\ g_y\\ g_z\end{pmatrix} = \begin{pmatrix} a_x^{*} & b_x^{*} & c_x^{*}\\ a_y^{*} & b_y^{*} & c_y^{*}\\ a_z^{*} & b_z^{*} & c_z^{*}\end{pmatrix}\begin{pmatrix} h\\ k\\ l\end{pmatrix} = \begin{pmatrix} h\,a_x^{*} + k\,b_x^{*} + l\,c_x^{*}\\ h\,a_y^{*} + k\,b_y^{*} + l\,c_y^{*}\\ h\,a_z^{*} + k\,b_z^{*} + l\,c_z^{*}\end{pmatrix}$$

!!! note
    회절 시뮬레이터의 **Details** 테이블에 표시되는 $U_{\mathbf g}$ 값은 상대론적 인자 $\gamma$가 적용되기 *이전*의 원시 값이다.

---

## 흡수 퍼텐셜 (열 확산 산란)

열 확산 산란 (TDS)을 설명하는 허수 (흡수) 퍼텐셜은

$$U'_{g,h} = \gamma\,\frac{1}{\pi\Omega}\sum_k f'_k(\mathbf g,\mathbf h)\,\exp\!\left[2\pi i(\mathbf g-\mathbf h)\cdot\mathbf r_k\right]T_k(\mathbf g-\mathbf h, M_k)$$

이고, **흡수 산란 인자**는

$$f'_k(\mathbf g,\mathbf h) = \frac{2h}{\beta\, m_0\, c}\sum_i\sum_j a_i a_j\left[\frac{1}{b_i+b_j}\exp\!\left\{-\frac{b_i b_j}{b_i+b_j}\,\frac{|\mathbf g-\mathbf h|^2}{4}\right\} - \frac{1}{b_i+b_j+2M_k}\exp\!\left\{-\frac{b_i b_j - M_k^2}{b_i+b_j+2M_k}\,\frac{|\mathbf g-\mathbf h|^2}{4}\right\}\right]$$

이다. 여기서 전인자 $2h/(\beta m_0 c)$의 $h$는 **플랑크 상수**이다 (빔 지수가 아님). 계수 $U^{C}$와 $U'$는 [부록 A3](index.md)의 구조 행렬 $\mathbf A$의 원소이다.

---

## 고유해에서 회절 강도까지

구조 행렬을 대각화하면 ([부록 A3](index.md) 참조) 고유값 $\lambda^{(j)}$와 블로흐파 진폭 $C_{\mathbf g}^{(j)}$를 얻는다. 시료 두께 $t$에서 출사면의 파동 진폭 — **투과 계수** $T_{\mathbf g}$ — 은 다음과 같다:

$$\begin{pmatrix} T_0\\ T_g\\ T_h\\ \vdots\end{pmatrix}
= e^{-2\pi i(\mathbf k_{vac}\cdot\mathbf n)\,t}
\begin{pmatrix} e^{\pi i P_0 t} & 0 & 0 & \cdots\\ 0 & e^{\pi i P_g t} & 0 & \cdots\\ 0 & 0 & e^{\pi i P_h t} & \cdots\\ \vdots & \vdots & \vdots & \ddots\end{pmatrix}
\begin{pmatrix} C_0^{(1)} & C_0^{(2)} & C_0^{(3)} & \cdots\\ C_g^{(1)} & C_g^{(2)} & C_g^{(3)} & \cdots\\ C_h^{(1)} & C_h^{(2)} & C_h^{(3)} & \cdots\\ \vdots & \vdots & \vdots & \ddots\end{pmatrix}
\begin{pmatrix} e^{2\pi i\lambda^{(1)} t} & 0 & 0 & \cdots\\ 0 & e^{2\pi i\lambda^{(2)} t} & 0 & \cdots\\ 0 & 0 & e^{2\pi i\lambda^{(3)} t} & \cdots\\ \vdots & \vdots & \vdots & \ddots\end{pmatrix}
\begin{pmatrix} \alpha^{(1)}\\ \alpha^{(2)}\\ \alpha^{(3)}\\ \vdots\end{pmatrix}$$

또는, 성분별로,

$$T_{\mathbf g} = e^{-2\pi i(\mathbf k_{vac}\cdot\mathbf n)\,t}\; e^{\pi i P_g t}\sum_j C_{\mathbf g}^{(j)}\,e^{2\pi i\lambda^{(j)} t}\,\alpha^{(j)}$$

- $\alpha^{(j)}$ : 각 블로흐파의 가중 (여기) 계수로, 입사면의 경계 조건에 의해 결정된다.
- $t$ : 시료 두께.

빔 $\mathbf g$의 회절 강도는 그러면

$$I_{\mathbf g} = \left|T_{\mathbf g}\right|^2$$

이다.

---

## 평행빔 SAED 계산 { #parallel-beam-saed }

일반적인 SAED (선택 영역 전자 회절)는 단일 입사 방향을 갖는 **평행빔 회절**로 취급된다. CBED와 달리, 수렴 조리개 내부의 많은 $\mathbf K$ 점을 주사하지 않는다. 현재의 결정 방위와 가속 전압이 하나의 입사 파동벡터 $\mathbf k_0$를 정의하며, ReciPro는 이 조건에서 각 반사 $\mathbf g$의 위치와 강도를 평가한다.

계산은 다음과 같이 구성할 수 있다.

1. 결정 방위, 가속 전압, 파장, 카메라 길이, 검출기 기하를 사용하여 진공 입사 파동벡터 $\mathbf k_{vac}$와 검출기 평면을 정의한다.
2. 평균 내부 퍼텐셜 $U_0$로부터 굴절 보정을 적용하여 결정 기준 파동벡터 $\mathbf k_0$를 얻는다.
3. 후보 역격자 벡터 $\mathbf g$를 열거하고, $Q_g=|\mathbf k_0|^2-|\mathbf k_0+\mathbf g|^2$나 여기 오차 $S_g$와 같은 양을 통해 에발트 구로부터의 거리를 평가한다.
4. 선택한 강도 모드를 사용하여 각 반사의 강도를 계산한다.
5. $\mathbf k_0+\mathbf g$의 방향을 검출기 평면에 투영하여 회절 스폿으로 그린다.

ReciPro의 SAED 모드는 주로 다음의 강도 모델을 제공한다.

| 모드 | 계산 | 일반적 용도 |
|------|-------------|-------------|
| 여기 오차만 | 반사가 에발트 구에 얼마나 가까운지만으로 강도를 추정한다. 구조 인자는 사용하지 않는다. | 스폿 위치와 정대축 기하의 빠른 점검. |
| 운동학적 + 여기 오차 | $\lvert F_{\mathbf g}\rvert^2$를 여기 오차 감쇠와 함께 사용한다. 다중 산란은 포함하지 않는다. | 얇은 시료, 약한 회절, 소광 규칙 점검. |
| 동역학적 이론 | 이 페이지의 블로흐파 코어를 사용하여 $T_{\mathbf g}(t)$를 얻고 $I_{\mathbf g}=\lvert T_{\mathbf g}\rvert^2$로 설정한다. | 두께 의존성, 다중 산란, 강한 전자 회절 반사. |

충실 구 단면이나 가우스 스폿과 같은 역격자점 표시 모드는 주로 그리기 프로파일을 제어한다. 동역학적 이론 모드에서는 물리적 반사 강도가 블로흐파 값 $|T_{\mathbf g}|^2$에 의해 결정되며, 그 강도가 이어서 선택한 표시 프로파일에 할당된다.

PED는 이 평행빔 SAED 계산을 세차 방향에 대해 적분한 것으로 볼 수 있으며, CBED는 회절 디스크 내부에 많은 입사 방향을 배열한 것으로 볼 수 있다.

---

## 평균 내부 퍼텐셜과 굴절

전자가 진공에서 결정으로 들어갈 때, 평균 내부 퍼텐셜 $U_0$가 결정 내부의 기준 파동벡터를 약간 변화시킨다. 표면에 평행한 성분은 경계 조건에 의해 고정되므로, 진공 파동벡터 $\mathbf k_{vac}$와 결정 기준 파동벡터 $\mathbf k_0$는 다음과 같이 쓸 수 있다:

$$|\mathbf k_0|^2 = k_{vac}^2 + U_0, \qquad \mathbf k_0 = \mathbf k_{vac} + x\,\hat{\mathbf n}$$

여기서 $x$는 표면 법선을 따른 보정이다. 이는 다음으로부터 얻어진다:

$$x^2 + 2(\hat{\mathbf n}\cdot\mathbf k_{vac})x - U_0 = 0$$

이 굴절된 $\mathbf k_0$는 [개요 페이지](index.md)에서 $P_g$, $Q_g$, 여기 오차, 구조 행렬 $\mathbf A$를 평가할 때 사용된다. 흡수 퍼텐셜은 또한 $\mathbf g=\mathbf 0$ 성분 $U'_0$를 가지며, 이는 결정을 통과하여 전파되는 파동에 대한 공통 평균 감쇠로 작용한다.

---

## 빔 선택

블로흐파 계산은 무한히 많은 역격자 벡터를 포함할 수 없으므로, ReciPro는 유한한 빔 집합 $\{\mathbf g\}$를 선택한다. 순위 지정 양은

$$R_{\mathbf g}=|\mathbf g|\,Q_{\mathbf g}^{\,2}$$

이며, $R_{\mathbf g}$가 더 작은 빔이 먼저 포함된다. 이는 짧은 역격자 벡터를 가지면서 동시에 에발트 구에 가까운 빔을 선호한다.

실제 계산에서는, 블로흐파의 최대 개수를 증가시킬 때 강도나 이미지가 얼마나 변하는지 점검하는 것이 중요하다. 강한 정대축 조건과 HOLZ 선의 세부가 있는 CBED 패턴은 수백 개의 빔을 필요로 할 수 있는 반면, 정대축에서 벗어난 조건은 더 적은 빔으로 수렴할 수 있다.

---

## 솔버 선택

유한한 빔 집합이 선택된 후, ReciPro는 주로 투과 계수를 얻기 위한 두 가지 동등한 방법을 사용한다.

| 방법 | 특징 | 일반적 용도 |
|--------|---------|-------------|
| 고유값 방법 | 구조 행렬 $\mathbf A$를 대각화하여 고유값 $\lambda^{(j)}$와 고유벡터 $C_{\mathbf g}^{(j)}$를 얻는다. 두께 의존성은 그 후 $e^{2\pi i\lambda^{(j)}t}$를 통해 평가된다. | 많은 깊이나 에너지를 주사하는 두께 시리즈, CBED, EBSD 계산 |
| 행렬 지수 방법 | 고유분해를 명시적으로 사용하지 않고 산란 행렬 $\exp(2\pi i\mathbf A t)$를 직접 평가한다. | 단일 두께 STEM 계산 및 슬라이스 적분 계산 |

두 방법 모두 동일한 Bethe 방정식을 푼다. 구현에서, 코드는 빔의 개수, 두께 배열, 그리고 네이티브 라이브러리의 가용 여부에 따라 고유값 방법, 행렬 지수 방법, 관리되는 .NET 루틴, 네이티브 Eigen 라이브러리 중에서 선택한다.

---

## 수렴 점검

동역학적 계산에서는, 기저가 충분히 큰지 점검하는 것이 공식 자체만큼이나 중요하다. 유용한 진단량은 빔 개수를 $N-\Delta N$에서 $N$으로 증가시켰을 때의 상대적 변화이다:

$$\Delta I_N=\frac{|I_N-I_{N-\Delta N}|}{I_N}$$

STEM의 경우, 이를 검출기 각도 설정과 함께 점검하라. CBED의 경우, 디스크 내부와 HOLZ 선을 살펴보라. EBSD의 경우, master pattern에서 키쿠치 밴드 폭과 배경을 추가로 비교하라. 이는 수치적 수렴을 시뮬레이션 결과에서 보이는 물리적 특징과 연결한다.

---

## 같이 보기

- [부록 A3. 블로흐파 방법에 의한 동역학적 회절](index.md)
- [7.2. SAED simulation](../../7-diffraction-simulator/1-saed-simulation.md)
- [7.4. CBED simulation](../../7-diffraction-simulator/3-cbed-simulation.md)
- [7. 회절 시뮬레이터](../../7-diffraction-simulator/index.md)
