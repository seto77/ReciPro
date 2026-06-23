# ReciPro 매뉴얼

<!-- 260623Ch: Shared demo movie for all localized top pages. -->
<div class="rp-demo-video" markdown="0">
  <video controls muted playsinline preload="metadata" aria-label="ReciPro demo movie">
    <source src="../assets/Recipro_Demo.mp4" type="video/mp4">
  </video>
</div>

## 간단한 소개
* ReciPro는 다양한 결정학 계산과 전자현미경 시뮬레이션을 제공하는 MIT 라이선스 무료 소프트웨어입니다.
* ReciPro는 GitHub에 공개된 이래(2020년 3월) 누적 27,000회 이상 다운로드되었으며, 많은 결정학자와 전자현미경 연구자가 사용하고 있습니다.

## 목표별로 찾기

| 목표 | 여기서 시작 | 주요 다음 단계 |
|------|------------|-----------------|
| 결정을 불러오고 방위를 설정 | [메인 창](0-main-window.md) | [회전 기하학](4-rotation-geometry.md), [부록 A1. 좌표계](appendix/a1-coordinate-system/1-orientation.md) |
| 결정 구조를 3D로 살펴보기 | [구조 뷰어](5-structure-viewer.md) | [대칭 정보](2-symmetry-information.md) |
| SAED / XRD / PED / CBED 패턴 계산 | [회절 시뮬레이터](7-diffraction-simulator/index.md) | [SAED](7-diffraction-simulator/1-saed-simulation.md), [X선 회절](7-diffraction-simulator/4-x-ray-neutron-diffraction.md), [PED](7-diffraction-simulator/2-ped-simulation.md), [CBED](7-diffraction-simulator/3-cbed-simulation.md) |
| HRTEM / STEM 이미지 계산 | [HRTEM/STEM 시뮬레이터](9-hrtem-stem-simulator/index.md) | [HRTEM](9-hrtem-stem-simulator/1-hrtem-simulation.md), [STEM](9-hrtem-stem-simulator/2-stem-simulation.md) |
| EBSD 패턴 시뮬레이션 | [EBSD 시뮬레이션](12-ebsd-simulation.md) | [전자 궤적](8-electron-trajectory.md), [부록 A3. EBSD 계산](appendix/a3-bloch-wave/ebsd.md) |
| 실험 회절 스폿 지수화 | [Spot ID v1](10-spot-id.md), [Spot ID v2](11-spot-id-v2.md) | [회절 시뮬레이터](7-diffraction-simulator/index.md) |
| 동역학적 회절 방정식 이해 | [부록 A3. 블로흐파 방법](appendix/a3-bloch-wave/index.md) | [동역학적 계산](appendix/a3-bloch-wave/calculation.md), [CBED](appendix/a3-bloch-wave/cbed.md), [STEM](appendix/a3-bloch-wave/stem.md), [EBSD](appendix/a3-bloch-wave/ebsd.md) |

## 기능
* **Full GUI** : 모든 작업은 그래픽 인터페이스를 통해 수행됩니다. 대부분의 파일 입출력은 끌어서 놓기를 지원합니다.
* **결정 목록** : 여러 결정을 한꺼번에 다룹니다. 결정마다 별도의 창을 열 필요가 없습니다.
* **공간군 데이터베이스** : International Tables Volume A의 230개 공간군과 530개 Hall 기호를 포함하는 내장 데이터베이스로, 대칭 요소, 와이코프 위치, 소광 규칙을 제공합니다. 대칭 요소와 일반 위치는 *International Tables* Vol. A 스타일의 모식도로 그릴 수 있습니다(참조: [2. 대칭 정보](2-symmetry-information.md)).
* **원자 정보** : 원소 H (1) – Cf (98)에 대한 산란 인자(X선, 전자, 중성자), 특성 X선 에너지, 동위원소 비율 등.
* **유연한 결정 회전** : 정대축/결정면 지수 또는 마우스 드래그로 방위를 설정합니다. 삼방정/육방정계에서는 밀러-브라베(4지수 *hkil*) 표기가 지원됩니다. 회전 상태는 모든 시뮬레이션 창에 걸쳐 동기화됩니다.
* **회절 시뮬레이션** : 운동학적 및 동역학적(블로흐파) 전자 회절, X선 회절(세차 및 백라우에 카메라 포함), 세차 전자 회절(PED), 수렴빔 전자 회절(CBED). TEM 홀더 시뮬레이션은 회절 패턴을 홀더 기울기 각도와 연결합니다.
* **HRTEM / STEM 시뮬레이션** : 부분 가간섭성 모델을 사용한 고분해능 TEM 이미지 시뮬레이션, 열 확산 산란을 포함한 STEM.
* **EBSD 및 전자 궤적** : EBSD 패턴 시뮬레이션과 몬테카를로 전자 궤적 시뮬레이션(참조: [8. 전자 궤적](8-electron-trajectory.md)).
* **스폿 지수화** : 실험 이미지로부터 회절 스폿의 자동 검출, 피팅, 지수화(Spot ID v1/v2).
* **매크로** : 작업 자동화를 위한 Python 구문 매크로(참조: [20. 매크로](20-macro/index.md)).
* **밝은 / 어두운 테마** : 인터페이스는 선택 가능한 밝은 또는 어두운 색상 모드를 따릅니다.

## 시스템 요구 사항
| 항목 | 최소 | 권장 |
|------|---------|-------------|
| 운영체제 | [.NET Desktop Runtime 10.0](https://dotnet.microsoft.com/download/dotnet/10.0)가 설치된 Windows (Windows on ARM64 지원) | Windows 11 |
| GPU | OpenGL 1.3 | OpenGL 4.3을 지원하는 외장 GPU |
| 메모리 | - | 16 GB 이상 |
| CPU | - | 8코어 이상 (동역학적 계산용) |

**Windows on ARM (네이티브, 실험적)** : 실험적인 네이티브 ARM64 포터블 패키지(`ReciPro-v.X_arm64.zip`, self-contained — .NET Runtime 설치 불필요)가 [릴리스 페이지](https://github.com/seto77/ReciPro/releases/latest)에서 제공됩니다. 일반 x64 패키지도 내장 에뮬레이션을 통해 ARM64 Windows에서 실행됩니다. 설정 방법과 제한 사항은 [문제 해결](troubleshooting.md#windows-on-arm)을 참조하세요.

**macOS (비공식)** : ReciPro는 공식적으로 Windows만 지원하지만, **win-x64** 포터블 ZIP 패키지가 Sikarugir Wine 래퍼와 Mesa3D OpenGL 드라이버를 결합하여 macOS(Apple Silicon)에서 실행되었다는 보고가 있습니다. 사용자가 공개한 설정 가이드는 <https://github.com/Ryo-fkushima/ReciPro_macOS_memo>에서 볼 수 있습니다. 이 경로는 공식적으로 지원되지 않으며, 일부 기호(Å, 위 첨자, 화살표)가 잘못 표시될 수 있다는 점에 유의하세요. ARM64 ZIP은 macOS + Wine에서 실행되지 **않으며**, 현재의 x64 + Rosetta 2 경로는 macOS 28(2027년 가을)부터 작동이 중단될 것으로 예상됩니다 — 자세한 내용은 [문제 해결](troubleshooting.md#mac-linux)을 참조하세요.

## 이 매뉴얼 사용법

이 GitHub Pages 매뉴얼이 현재 기준이 되는 정본입니다. 왼쪽 탐색 메뉴로 장별로 둘러보거나, 헤더의 검색을 사용하여 함수 이름이나 UI 라벨을 찾으세요. 기존 PDF 매뉴얼은 보관용으로 유지됩니다.

* **보관 PDF (영어):** <https://raw.githubusercontent.com/seto77/ReciPro/master/ReciPro/doc/ReciProManual(en).pdf>
* **보관 PDF (일본어):** <https://raw.githubusercontent.com/seto77/ReciPro/master/ReciPro/doc/ReciProManual(ja).pdf>

## 빠른 시작
1. [Releases](https://github.com/seto77/ReciPro/releases/latest)에서 다운로드하여 설치합니다.
2. 내장 목록(약 80개 결정)에서 결정을 선택합니다. CIF 파일을 가져오거나 [CSManager](https://github.com/seto77/CSManager)를 사용할 수도 있습니다.
3. 오른쪽 패널에서 기능을 호출합니다: 구조 뷰어, 스테레오넷, 회절 시뮬레이터, HRTEM 시뮬레이션 등.
4. 마우스 드래그 또는 정대축/결정면 지수 입력으로 결정을 회전합니다.

## 인용
> Y. Seto, "ReciPro: free and open-source multipurpose crystallographic software integrating a crystal operation interface and diffraction simulators," *J. Appl. Cryst.* **55**, 397–410 (2022). <https://doi.org/10.1107/S1600576722000139>

## 라이선스
ReciPro는 [MIT 라이선스](https://github.com/seto77/ReciPro/blob/master/LICENSE.md)에 따라 배포됩니다.
