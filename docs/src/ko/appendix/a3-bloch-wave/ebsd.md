# EBSD 계산

EBSD(전자 후방산란 회절)는 CBED 및 STEM과 동일한 Bethe/블로흐파 코어를 사용하지만, 문제의 설정 방식이 다르다. CBED와 STEM은 **입사빔 문제**이다: 전자파가 시료 외부에서 들어와 출사파를 계산한다. EBSD는 **출사 방향 문제**이다: 시료 내부에서 비탄성 산란을 겪은 전자가 후방산란 전자로 방출되며, 계산은 각 외부 방향으로 얼마만큼의 강도가 빠져나가는지를 묻는다.

ReciPro는 상반 정리를 이용해 이 출사 방향 문제를 일반적인 입사빔 문제로 변환한다. 먼저 방향 공간의 **master pattern**을 계산한 다음, 그 master pattern을 몬테카를로 깊이 / 에너지 / 방향 가중치 및 검출기 기하학과 결합하여 검출기 패턴을 형성한다.

---

## 상반 정리에 의한 재정식화

내부 선원점 $\mathbf r_n$ 에서 외부 방향 $\widehat{\mathbf s}$ 으로의 진폭을 직접 계산한다면, 모든 선원점마다 별도의 산란 문제가 필요하게 된다. 이는 현실적이지 않다.

상반 정리는 이 문제를 다음과 같이 다시 쓴다: $\mathbf r_n$ 에서 출발한 전자가 원거리장 방향 $\widehat{\mathbf s}$ 에 나타날 진폭은, 외부 방향 $-\widehat{\mathbf s}$ 에서 입사하는 상반파의 $\mathbf r_n$ 에서의 진폭과 같다. 이 상반파는 일반적인 Bethe/블로흐파 해이다. 이를 $\psi_{\widehat{\mathbf s}}^{\mathrm{rec}}(\mathbf r)$ 로 쓰면, 방향 $\widehat{\mathbf s}$ 에서의 EBSD 강도는 다음과 같이 쓸 수 있다.

$$I_{\mathrm{EBSD}}(\widehat{\mathbf s};E,z)\propto
\sum_n \sigma_n(E,z)\,
\left|\psi_{\widehat{\mathbf s}}^{\mathrm{rec}}(\mathbf r_n;E,z)\right|^2$$

여기서 $\sigma_n(E,z)$ 는 원자 위치 $\mathbf r_n$ 근방에서 에너지 $E$, 깊이 $z$ 의 후방산란 채널로 들어가는 비탄성 산란에 대한 가중치이다. 선원항은 간섭성 진폭 합이 아니라 강도로서 더해지는데, 이는 비탄성 산란이 서로 다른 선원 위치 사이의 위상 관계를 파괴한다고 가정하기 때문이다.

---

## Master Pattern

EBSD master pattern은 위 식에서 결정에 고유한 동역학적 회절 부분을 방향 격자 위에 저장한다. 개념적으로,

$$M(\widehat{\mathbf s};E,z)=
\sum_n w_n(E,z)\,
\left|\psi_{\widehat{\mathbf s}}^{\mathrm{rec}}(\mathbf r_n)\right|^2$$

여기서 $w_n$ 은 원자 위치 $\mathbf r_n$ 에서의 결정 측 비탄성 선원 가중치이다. ReciPro는 경험적 가중치

$$w_n \propto Z_n^{1.7}\,\mathrm{occ}_n$$

를 원자 번호 $Z_n$ 과 점유율 $\mathrm{occ}_n$ 과 함께 사용한다. 이는 몬테카를로로 생성되는 수송 깊이 / 에너지 분포와는 별개이다.

구현에서는 상반 블로흐파를 각 원자 위치에서 평가한다.

$$\beta_n^{(j)}=
\alpha^{(j)}
\sum_{\mathbf g}C_{\mathbf g}^{(j)}
\exp\!\left[2\pi i(\mathbf k^{(j)}+\mathbf g)\cdot\mathbf r_n\right]$$

그런 다음 코드는 블로흐파 쌍 행렬

$$S_{jj'}=\sum_n w_n\,\beta_n^{(j)}\,\overline{\beta_n^{(j')}}$$

과 해석적 두께 적분

$$\mathcal F_{jj'}(t)=
\frac{\exp\!\left[2\pi i(\gamma^{(j)}-\overline{\gamma^{(j')}})t\right]-1}
{2\pi i(\gamma^{(j)}-\overline{\gamma^{(j')}})}$$

을 형성하며, 따라서 master pattern은 다음과 같이 평가된다.

$$M(\widehat{\mathbf s};E,t)=
\mathrm{Re}\left\{\sum_{j,j'}S_{jj'}(E)\,\mathcal F_{jj'}(t)\right\}$$

분모가 0에 가까운 축퇴 극한에서는 $\mathcal F_{jj'}(t)\to t$ 가 된다.

---

## 방향 공간 샘플링

master pattern은 검출기 영상 자체가 아니라, 결정 고정 방향 공간에서의 강도 분포이다. ReciPro는 면적 보존 Rosca-Lambert 투영으로 그 방향 공간을 샘플링하고, $+Z$ 와 $-Z$ 반구를 별도의 평면 배열로 저장한다. 면적 보존 샘플링은 극과 적도 사이의 밀도 편향을 줄인다.

이 단계에서 master pattern은 결정 구조, 가속 전압, 깊이, 에너지, 흡수 모델에 의존한다. 패턴 중심이나 스크린 위치와 같은 검출기 기하학은 아직 적용되지 않았다.

---

## 몬테카를로 가중치와 검출기 패턴

실험 관측량에 가까운 EBSD 검출기 패턴을 얻으려면, master pattern을 각 깊이, 에너지, 방향에서 얼마나 많은 후방산란 전자가 방출되는지에 따라 가중해야 한다. 이 수송 가중치를

$$W(E,z;\widehat{\mathbf s})$$

로 쓰고, 검출기 픽셀 $\mathbf p$ 에 대응하는 결정 고정 방향에 대해 $\widehat{\mathbf s}(\mathbf p)$ 를 사용하면, 최종 검출기 패턴은

$$P(\mathbf p)=
\sum_{i,j}
W(E_i,z_j;\widehat{\mathbf s}(\mathbf p))\,
M(\widehat{\mathbf s}(\mathbf p);E_i,z_j)$$

로, 에너지와 깊이에 대한 이산 합이 된다.

몬테카를로 부분은 탄성 산란, 비탄성 산란, 에너지 손실, 그리고 시료 표면을 통한 탈출을 추적한다. 후방산란 전자에 대해서는 깊이, 에너지, 출사 방향의 분포를 구축한다. ReciPro는 마지막 비탄성 산란 위치와 그 직후의 에너지를 유효 선원으로 사용하는 모델과, 탈출 깊이 및 탈출 에너지를 사용하는 모델을 구별한다.

---

## TDS 배경과 흡수 모델

EBSD 패턴은 기하학적 키쿠치 밴드 구조뿐만 아니라, 열 확산 산란(TDS)에서 비롯된 매끄러운 배경도 포함한다. `IncludeTDSBackground` 가 활성화되면, ReciPro는 후방 반구로 산란된 TDS 성분,

$$\pi/2\leq\theta\leq\pi$$

을 흡수 행렬 $\mu_{\mathrm{back}}$ 로 평가하고, master pattern과 동일한 블로흐파 쌍 합산을 사용하여 배경 강도를 더한다. 동일한 고유해를 재사용하므로, TDS 배경은 비교적 적은 추가 비용만 더한다.

`UseNonLocalAbsorption` 이 활성화되면, 흡수 퍼텐셜은 단지 $U'_{\mathbf g-\mathbf h}$ 로서가 아니라 방향과 빔 쌍에 의존하는 비국소 형태로 취급된다. 이는 정확도를 향상시킬 수 있지만, master pattern 격자의 방향들에 대해 흡수 행렬을 다시 구축해야 하므로 계산 시간을 상당히 증가시킬 수 있다.

---

## 실용적 매개변수

- **빔 수**: 빔이 너무 적으면 키쿠치 밴드의 세부와 HOLZ 밴드 구조를 잃는다. 저지수 정대축은 수백 개의 빔을 필요로 할 수 있다.
- **깊이 및 에너지 배열**: 이것이 몬테카를로 가중치 $W(E,z;\widehat{\mathbf s})$ 의 변동 스케일보다 거칠면, 에너지 의존적인 밴드 폭과 채널링 깊이 효과가 평균화되어 사라진다.
- **검출기 기하학**: 패턴 중심, 스크린 거리, 시료 기울기가 사상 $\widehat{\mathbf s}(\mathbf p)$ 를 결정하므로, master pattern이 변하지 않아도 검출기 패턴은 바뀔 수 있다.
- **상반성 해석**: master pattern은 검출기 영상이 아니다. 몬테카를로 가중과 검출기 투영을 거친 후에야 비로소 검출기 패턴이 된다.
- **TDS 배경**: 정량적인 밴드 대비 비교를 위해서는 활성화한다. 매끄러운 배경 없이 기하학적 키쿠치 구조를 더 쉽게 살펴보고 싶을 때는 비활성화한다.

## 함께 보기

- [동역학적 계산 (공통 코어)](calculation.md)
- [부록 A3. 블로흐파 방법에 의한 동역학적 회절](index.md)
- [12. EBSD 시뮬레이션](../../12-ebsd-simulation.md)
