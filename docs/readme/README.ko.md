# ReciPro

[![Documentation](https://img.shields.io/badge/%F0%9F%93%96_Documentation-blue)](https://seto77.github.io/ReciPro/ko/)
[![Latest Release](https://img.shields.io/github/v/release/seto77/ReciPro?logo=github)](https://github.com/seto77/ReciPro/releases/latest)
[![Total downloads](https://img.shields.io/github/downloads/seto77/ReciPro/total?logo=github&label=GitHub%20downloads)](https://github.com/seto77/ReciPro/releases)
[![GitHub Stars](https://img.shields.io/github/stars/seto77/ReciPro?style=social)](https://github.com/seto77/ReciPro/stargazers)
[![GitHub Forks](https://img.shields.io/github/forks/seto77/ReciPro?style=social)](https://github.com/seto77/ReciPro/forks)
[![License: MIT](https://img.shields.io/badge/License-MIT-green)](https://github.com/seto77/ReciPro/blob/master/LICENSE.md)

<!-- 260804Cl: ../../README.md(영어)의 번역본. 영어판이 갱신되면 이 파일도 함께 갱신할 것. -->
[English](../../README.md) | [日本語](README.ja.md) | [Deutsch](README.de.md) | [Français](README.fr.md) | [Español](README.es.md) | [Italiano](README.it.md) | [Русский](README.ru.md) | [简体中文](README.zh-Hans.md) | [繁體中文](README.zh-Hant.md) | **한국어** | [Português](README.pt.md)

*ReciPro* 는 결정 데이터베이스 검색, 결정 구조와 고니오미터 설정의 시각화, 회절 패턴 및 고분해능 현미경 이미지의 시뮬레이션, 회절 데이터 분석 기능에 매끄럽게 접근할 수 있는 무료 오픈 소스 GUI 기반 다목적 결정학 소프트웨어입니다. 이러한 기능들은 사용하기 쉬운 GUI로 서로 연동되며, 계산 결과는 거의 실시간으로 동기화되어 표시됩니다. *ReciPro* 는 X선·전자선·중성자선 회절 결정학과 TEM을 다루는 폭넓은 결정학 연구자(초보자 포함)에게 도움이 됩니다.

*ReciPro* 는 2002년부터 지속적으로 개발되어 왔으며, 2020년 3월부터 GitHub에 공개되었습니다. GitHub에서 27,000회 이상 다운로드되었고, 대학과 기업의 십수 개 연구실에서 수백 명의 사용자가 이용하고 있습니다.

***[사용법은 매뉴얼을 참고하세요!](https://seto77.github.io/ReciPro/ko/)***

[실시간으로 수행되는 다양한 시뮬레이션(예: MgAl2O4)](https://github.com/user-attachments/assets/6b0234dd-f2d6-49db-b146-bb74cf6021b6)

## 개발자

*ReciPro* 는 [Seto Y.](https://yseto.net/en/home-e) 와 [Ohtsuka M.](https://researchmap.jp/7000002999?lang=en) 가 개발하고 있습니다. 기능과 알고리즘은 [논문](https://github.com/seto77/ReciPro/blob/master/docs/ReciProSetoOhtsuka2022.pdf)에 소개되어 있습니다.

## 인용

학술 연구에 *ReciPro* 를 사용한 경우, GitHub 저장소 페이지에 표시되는 **Cite this repository** 링크를 이용해 주세요. 인용 메타데이터는 `CITATION.cff` 로 제공되며, 권장 인용 문헌은 다음 논문입니다.

  * [Seto, Y. & Ohtsuka, M. (2022). *J. Appl. Cryst.* **55**, 397-410, doi: 10.1107/S1600576722000139.](https://doi.org/10.1107/S1600576722000139)

필요에 따라 소프트웨어 저장소 자체를 인용할 수도 있습니다.

  * 저장소: https://github.com/seto77/ReciPro
  * 릴리스: https://github.com/seto77/ReciPro/releases/latest

***

## 설치

* [*ReciPro-setup.msi*](https://github.com/seto77/ReciPro/releases/latest/download/ReciPro-setup.msi)(최신 버전 직접 링크)를 내려받아 실행하세요. [릴리스 페이지](https://github.com/seto77/ReciPro/releases/latest)에서도 찾을 수 있습니다. (v.4.939 까지는 설치 파일 이름이 *ReciProSetup.msi* 였습니다.)
* *ReciPro* 는 ***.Net Desktop Runtime 10.0***(***.Net Runtime 10.0*** 이 아님)이 설치된 Windows에서 동작하며, 런타임은 [여기](https://dotnet.microsoft.com/download/dotnet/10.0)에서 설치할 수 있습니다.
* 권한이 제한된 PC 등에서 설치 프로그램을 실행할 수 없는 경우, 릴리스 페이지에서 **포터블 ZIP** 패키지(*ReciPro-v.X.XXX.zip*)도 제공합니다. 자체 완결형이며 설치도 .NET 런타임도 필요 없이 압축을 풀고 실행하기만 하면 됩니다.
* *ReciPro* 는 **MIT 라이선스**로 배포됩니다(누구나 자유롭게 사용·수정·재배포할 수 있습니다).
* 코드 서명 현황과 설치 파일 검증 방법은 [코드 서명 정책](../../CODE_SIGNING.md)을 참고하세요.
* 포함되거나 참조된 서드파티 구성 요소 및 데이터는 [서드파티 고지](../../THIRD-PARTY-NOTICES.md)를 참고하세요.

### macOS (비공식)

* *ReciPro* 가 공식적으로 지원하는 운영체제는 Windows뿐이지만, **포터블 ZIP** 패키지와 **Sikarugir** Wine 래퍼, **Mesa3D** OpenGL 드라이버를 조합하면 macOS(Apple Silicon)에서도 동작한다는 보고가 있습니다. Windows 라이선스나 가상 머신은 필요하지 않습니다.
* Ryo Fukushima(JAMSTEC)가 공개한 단계별 설정 가이드를 참고하세요: https://github.com/Ryo-fkushima/ReciPro_macOS_memo
* 이 구성은 공식적으로 지원되지 않으며 충분히 검증되지도 않았습니다. 알려진 제한으로, 일부 기호(Å, 위첨자, 화살표)가 잘못 표시될 수 있습니다.
* 깨진 기호는 글리프 지원 범위가 넓은 글꼴(**DejaVu Sans/Serif**, 일본어 UI에는 **Noto Sans CJK JP**)을 Wine 프리픽스에 설치하면 해결됩니다. ReciPro가 Wine 환경을 감지해 자동으로 해당 글꼴로 전환합니다. 자세한 내용은 [문제 해결](https://seto77.github.io/ReciPro/ko/troubleshooting/)을 참고하세요.

### Windows 보안 경고에 대하여

* *ReciPro* 는 반드시 공식 GitHub Releases 페이지에서만 내려받으세요: https://github.com/seto77/ReciPro/releases/latest
* 일부 Windows 환경에서는 설치 프로그램을 실행하기 전에 Microsoft Defender SmartScreen 또는 Smart App Control이 경고를 표시할 수 있습니다. 새로 빌드되었거나 배포 범위가 좁은 연구용 소프트웨어에서 흔히 발생하는 일이며, 경고 자체가 설치 파일이 악성임을 의미하지는 않습니다.
* 내려받은 설치 파일을 직접 검증하고 싶다면 VirusTotal 같은 다중 엔진 검사 서비스로 스캔할 수 있습니다.

## 코드 서명 정책

[<img src="https://signpath.org/assets/favicon-50x50.png" alt="SignPath" height="20">](https://about.signpath.io/) Windows용 무료 코드 서명은 [SignPath.io](https://about.signpath.io/)가 제공하며, 인증서는 [SignPath Foundation](https://signpath.org/)에서 발급합니다.

v.4.942 부터 릴리스 산출물(*ReciPro-setup.msi* 설치 파일과 포터블 *ReciPro.exe*)은 자동화된 릴리스 파이프라인의 일부로 Windows Authenticode 서명이 적용되며, 각 서명 요청은 공개 전에 메인테이너가 검토하고 수동으로 승인합니다. 서명 범위, 설치 파일 검증 방법, 의심스러운 산출물 신고 방법을 포함한 전체 정책은 [CODE_SIGNING.md](../../CODE_SIGNING.md)를 참고하세요.

## 개인정보 보호

*ReciPro* 는 로컬에서 동작하는 데스크톱 애플리케이션입니다. 개인정보나 사용 데이터를 **수집·저장·전송하지 않으며**, 텔레메트리나 분석 기능도 포함되어 있지 않습니다. 설치 후에는 완전히 오프라인으로 동작합니다.

*ReciPro* 가 수행하는 네트워크 연결은 사용자가 직접 시작하는 선택적 다운로드뿐이며, 어느 것도 사용자의 데이터를 업로드하지 않습니다.

* **업데이트 확인**(메뉴 명령): 설치된 버전과 최신 GitHub 릴리스를 비교하고, 사용자가 선택하면 공식 [GitHub Releases](https://github.com/seto77/ReciPro/releases/latest) 페이지에서 새 설치 파일을 내려받습니다.
* **COD 데이터베이스**(Crystallography Open Database): 최초 사용 시 개발자의 GitHub 미러에서 내려받고(약 880 MB), 이후에는 오프라인으로 사용합니다.
* **Intel MKL 라이브러리**(선택적 가속): *Use MKL* 옵션을 켠 경우에만 [nuget.org](https://www.nuget.org/)에서 내려받아(약 55 MB) 동력학적 회절 계산을 가속합니다.

내장된 AMCSD 데이터베이스와 모든 핵심 기능은 완전히 오프라인으로 동작합니다.

## 매뉴얼
  * 온라인 매뉴얼(영어 / 일본어): https://seto77.github.io/ReciPro/ko/
  * 일본어판: https://yseto.net/soft/recipro
***

## 주요 기능

### 결정 데이터베이스

* **AMCSD**(American Mineralogist Crystal Structure Database): 21,000종 이상의 결정 구조가 내장되어 있어 설치 직후부터 사용할 수 있습니다.
  * 데이터베이스는 고도로 압축되어 있고(약 5 MB) 설치 파일에 포함되므로 오프라인 환경에서도 사용할 수 있습니다.
  * 이름, 화학 조성, 격자 상수, 밀도, 대칭성, 포함 원소로 결정을 검색할 수 있습니다.
  * 참고 문헌: [Downs & Hall-Wallace, 2003, *American Mineralogist* **88**, 247-250](https://www.geo.arizona.edu/xtal/group/pdf/am88_247.pdf)
* **COD**(Crystallography Open Database): 유기 결정을 포함한 약 525,000종의 결정 구조도 이용할 수 있습니다.
  * 최초 사용 시 자동으로 내려받으며(약 880 MB), 이후에는 오프라인으로 이용할 수 있습니다.
  * 참고 문헌: [Gražulis et al., 2009, *J. Appl. Cryst.* **42**, 726-729](https://doi.org/10.1107/S0021889809016690); [Gražulis et al., 2012, *Nucleic Acids Res.* **40**, D420-D427](https://doi.org/10.1093/nar/gkr900)
* CIF 및 AMC 형식 파일의 가져오기/내보내기를 지원합니다.

### 결정학적 계산

* 530가지 공간군 표기를 지원합니다: 230개의 표준 ITA 설정 + 300개의 비표준 축 설정.
  * 모든 공간군의 일반 조건(소광 법칙), 와이코프 위치, 다중도.
  * 면 및/또는 축 사이의 주기성과 각도의 기하학적 계산.
  * 등가 원자 위치 생성.
  * 비표준 축 설정(예: *Pbnm* → *Pnma*)과 원점 이동 간의 간편한 변환.

### 원자의 성질

* <sup>1</sup>H 부터 <sup>98</sup>Cf 까지의 특성 X선 파장/에너지.
* X선·전자선·중성자선에 대한 원자 산란 인자.

### 구조 뷰어

* OpenGL(GLSL) 아키텍처를 이용한 3차원 결정 구조 시각화.
  * 원자, 결합, 배위 다면체, 단위 격자, 격자면, 경계면, 범례 라벨을 그립니다.
  * 수만 개의 원자를 포함하는 복잡한 결정 구조도 실시간으로 매끄럽게 그릴 수 있습니다.
  * 기본 원자 색상과 크기는 VESTA와 호환됩니다.
  * 그리기 범위는 단위 격자의 배수 또는 결정면 지수와 중심으로부터의 거리로 지정할 수 있습니다.
  * 경계면에 색을 입혀 임의의 결정 정벽(晶癖)을 표현할 수 있습니다.
  * 임의의 격자면을 표시할 수 있어, 초보자가 회절 현상에서 격자면 개념을 이해하는 데 도움이 됩니다.
  * 회전, 이동, 확대/축소를 마우스로 자유롭게 조작할 수 있습니다.
  * 원자를 클릭하면 인접 원자와의 거리 및 결합각이 표시됩니다.
  * 회전 상태는 다른 기능 창(스테레오 투영, 회절 시뮬레이터 등)에 즉시 반영됩니다.
  * 내장 비디오 인코더(Windows Media Foundation)로 발표용 회전 애니메이션 영상(H.264/H.265 MP4)을 생성할 수 있습니다.

### 스테레오 투영

* 결정면과 결정축을 스테레오 투영도에 표시합니다.
  * 등각 투영(울프 네트)과 등적 투영(슈미트 네트)을 모두 지원하며, 위도선과 경도선도 표시할 수 있습니다.
  * 지수는 수치 범위 또는 특정 값으로 지정할 수 있습니다.
  * 정대축(zone axis)을 지정해 대원(great circle)을 표시할 수 있습니다.
  * 그린 객체는 벡터 형식으로 저장·복사할 수 있어 해상도 손실 없이 나중에 편집할 수 있습니다.
  * 교육용으로 스테레오 투영 기하를 3차원으로 시각화합니다.

### 회절 시뮬레이터

* X선·전자선·중성자선 선원에 대한 단결정 회절 패턴을 시뮬레이션합니다.
  * 입사 빔의 운동 에너지를 자유롭게 설정할 수 있습니다.
  * <sup>1</sup>H 부터 <sup>98</sup>Cf 까지의 특성 X선 에너지가 내장되어 있습니다.
  * 표시 범위는 이미지 해상도(픽셀 크기)와 카메라 길이로 지정합니다.
  * 검출기를 기울인 배치도 지원합니다.
  * 실험으로 취득한 이미지의 중첩 표시를 지원합니다.
  * 결정 회전(회절 조건)을 제어할 수 있으며 다른 창과 즉시 동기화됩니다.

* **다결정 회절**: 다결정 시료를 가정한 디바이 링 패턴 시뮬레이션.
* **세차 카메라**(X선): 0차 라우에 대역의 세차 카메라 패턴 시뮬레이션.
* **후방 반사 라우에 카메라**(X선): 후방 반사 라우에 패턴 시뮬레이션.

#### 운동학적 회절 이론
* 모든 선원(X선·전자선·중성자선)에서 사용할 수 있습니다.
* 회절 강도는 결정 구조 인자 진폭의 제곱과 여기 오차로부터 추정됩니다.
* 디바이–월러 인자가 회절 강도에 미치는 영향도 반영되어 있습니다.

#### 동력학적 회절 이론(전자선)
* **블로흐파 방법**(Bethe, 1928)에 기반하여, 저차 정대축에 제약받지 않고 유연한 결정 방위를 다룰 수 있습니다.
* 두 가지 계산 방식을 제공합니다.
  * **베테 고유값법**: 블로흐 고유 상태의 고유값·고유 벡터를 행렬 대각화로 구합니다. 시료 두께를 변화시킬 때 적합합니다.
  * **산란 행렬법**: 파데 근사를 이용한 스케일링·제곱법으로 행렬 지수를 직접 계산합니다. 단일 두께를 빠르게 계산할 때 적합합니다.
* 가장 빠른 알고리즘과 최적의 수학 라이브러리(Eigen, Intel MKL, Math.NET)가 자동으로 선택됩니다.
* 열확산 산란(TDS) 흡수 퍼텐셜은 높은 성능을 위해 해석적으로 계산됩니다.

* **SAED**(제한 시야 전자 회절): 동력학적 산란 효과를 포함한 평행 빔 전자 회절 시뮬레이션.
* **PED**(세차 전자 회절): 세차각과 방위각 분해능을 지정해 PED 패턴을 시뮬레이션합니다. 결정 구조 해석과 준운동학적 PED 조건 최적화에 유용합니다.
* **CBED**(수렴 빔 전자 회절): 사용자가 지정한 수렴 반각과 분할 수로 CBED 패턴을 시뮬레이션합니다. 시료 두께 결정을 위한 두께별 시뮬레이션을 지원합니다.
  * 위치 평균 CBED(PACBED) 패턴.
  * 대각도 CBED(LA-CBED) 시뮬레이션.

### HRTEM 시뮬레이터

* 동일한 블로흐파 이론 체계를 이용한 고분해능 투과 전자 현미경 이미지 시뮬레이션.
* 광학 파라미터(가속 전압, 구면 수차 계수, 디포커스 값, 시료 두께 등)는 GUI에서 설정합니다.
* 대표적인 TEM 광학 파라미터 프리셋이 내장되어 있어 마우스 오른쪽 버튼으로 불러올 수 있습니다.
* 부분 간섭성에 대한 두 가지 결상 모델:
  * **선형 콘트라스트 전달 이론**: 계산 비용이 낮으며, 약위상 물체 근사가 성립하는 얇은 시료에 적합합니다.
  * **비선형 콘트라스트 전달 이론(TCC 모델)**: 1차 투과 교차 계수(Ishizuka, 1980)에 기반하며, 더 두꺼운 시료나 원자 번호가 큰 물질에서도 신뢰할 수 있습니다.
* 포락 함수를 포함한 콘트라스트 전달 함수를 그릴 수 있습니다.
* 두께–디포커스 시리즈 이미지를 동시에 계산할 수 있습니다.
* 표준적인 조건에서는 보통 1초 이내에 계산이 완료됩니다.

### STEM 시뮬레이터

* 주사 투과 전자 현미경 이미지 시뮬레이션.
  * 명시야(BF), 환형 암시야(ADF), 고각 환형 암시야(HAADF) 결상 모드.
  * 수렴 빔은 다수의 평면파 중첩으로 취급하며 겹침을 정확히 계산합니다.
  * 비탄성 산란 전자는 흡수 퍼텐셜 모델로 계산합니다.
  * 두께–디포커스 시리즈 이미지를 생성할 수 있습니다.

### Spot ID

* 실측 SAED 패턴에 대한 반자동 회절 스폿 지수 부여.
* **Spot ID v1**: 회절 스폿의 기하학적 배치(거리와 각도)로부터 정대축을 탐색합니다. 2~3장의 이미지를 동시에 해석할 수 있습니다.
* **Spot ID v2**: SAED 패턴 이미지를 직접 불러옵니다.
  * 표준 이미지 형식을 지원합니다: TIFF (.tif), Digital Micrograph 3/4 (.dm3, .dm4) 등.
  * 회절 스폿을 자동으로 검출하고 2차원 pseudo-Voigt 함수로 피팅합니다.
  * 역격자 벡터 배열과 일치하는 결정 방위를 전수 탐색합니다.
  * 고차 정대축도 정확하게 결정할 수 있습니다.

### 회전 기하(고니오미터)

* ReciPro의 오일러 각을 실험실의 고니오미터와 연결합니다.
* 원하는 결정 방위(예: 저차 정대축)를 얻기 위해 고니오미터를 어떻게 회전시켜야 하는지 알려줍니다.
* 임의의 고니오미터 정의를 지원합니다.

### 매크로

* Python 문법의 매크로 스크립트로 작업을 자동화할 수 있습니다.
  * 예: 결정을 1° 간격으로 회전시키며 각 단계에서 회절 패턴이나 STEM 이미지를 저장.
  * ReciPro 고유 함수는 "ReciPro" 네임스페이스에서 사용할 수 있습니다.
  * 사용 예는 [매뉴얼](https://seto77.github.io/ReciPro/ko/20-macro/2-examples/)에 있습니다.

### 기타 기능

* **전자 비정 시뮬레이터**: 물질 내 전자 비정(飛程)의 몬테카를로 시뮬레이션.
* **EBSD**(전자 후방 산란 회절): 개발 중.

## 기술적 세부 사항

* **C++**, **C#**, **OpenGL Shading Language (GLSL)** 로 작성되었습니다.
* 멀티스레드 병렬화로 최신 다중 코어 CPU에서 고성능 계산을 수행합니다.
* 결정 방위가 바뀌면 모든 기능 창이 실시간으로 동기화되어 갱신됩니다.
* 오른손 직교 좌표계(X: 오른쪽, Y: 위, Z: 앞)와 Z–X–Z 오일러 각 규약을 사용합니다.
* 좌표 정의는 Thermo Fisher Scientific의 EBSD 소프트웨어와 호환됩니다.

### 학술적 영향

* **동료 심사를 거친 소프트웨어 논문:** [Seto, Y. & Ohtsuka, M. (2022), *Journal of Applied Crystallography*, **55**, 397-410](https://doi.org/10.1107/S1600576722000139).
* **인용 논문:** [Google Scholar 인용 문헌](https://scholar.google.jp/scholar?cites=12625594477623342627).
* **논문 주목도:** [Altmetric 상세 정보](https://www.altmetric.com/details/123778746).

| 지표 | 주요 수치 |
| --- | --- |
| GitHub 총 다운로드 수 | 27,000회 이상 |
| Google Scholar 피인용 수 | 170회 이상 |
| Dimensions 피인용 수 | 160회 이상 |
| Mendeley 독자 수 | 90명 이상 |

## 스크린샷

<img src="https://seto77.github.io/ReciPro/assets/cap-ko-auto/FormMain.png" height="320px" alt="메인 창">
<img src="https://seto77.github.io/ReciPro/assets/cap-ko-auto/FormCrystalDatabase.png" height="320px" alt="결정 데이터베이스">
<img src="https://seto77.github.io/ReciPro/assets/cap-ko-auto/FormSymmetryInformation.png" height="320px" alt="대칭성 정보">
<img src="https://seto77.github.io/ReciPro/assets/cap-ko-auto/FormBeamInteraction.png" height="320px" alt="빔 상호작용">
<img src="https://seto77.github.io/ReciPro/assets/cap-ko-auto/FormStructureViewer.png" height="320px" alt="구조 뷰어">
<img src="https://seto77.github.io/ReciPro/assets/cap-ko-auto/FormStereonet.png" height="320px" alt="스테레오 투영">
<img src="https://seto77.github.io/ReciPro/assets/cap-ko-auto/FormDiffractionSimulator.png" height="320px" alt="회절 시뮬레이터">
<img src="https://seto77.github.io/ReciPro/assets/cap-ko-auto/FormImageSimulator.png" height="320px" alt="HRTEM/STEM 시뮬레이터">
<img src="https://seto77.github.io/ReciPro/assets/cap-ko-auto/FormSpotIDV2.png" height="320px" alt="Spot ID v2">
<img src="https://seto77.github.io/ReciPro/assets/cap-ko-auto/FormMacro.png" height="320px" alt="매크로">
<img src="https://seto77.github.io/ReciPro/assets/cap-ko-auto/FormTrajectory.png" height="320px" alt="전자 비정 시뮬레이터">

***
