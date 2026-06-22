# 문제 해결

ReciPro의 일반적인 문제와 해결 방법입니다. 아래 항목 중 다수는 [GitHub 이슈 트래커](https://github.com/seto77/ReciPro/issues)에 올라온 질문과 버그 보고에서 가져온 것이며, 해당하는 경우 버그가 수정된 버전을 함께 표기했습니다.

> **대부분의 문제는 [최신 버전](https://github.com/seto77/ReciPro/releases/latest)으로 업데이트하기만 하면 해결됩니다.** ReciPro는 자주 업데이트되며, 아래 버그 중 다수는 보고된 지 며칠 안에 수정되었습니다.

---

## 시작 및 실행

### 증상: 프로세스는 실행 중인데 창이 나타나지 않음

ReciPro가 시작되지만(작업 관리자에는 보임) 창이 화면에 전혀 표시되지 않습니다.

**원인**: 창이 화면 밖에서 열렸습니다 — Windows 표시 좌표 문제로, 일반적으로 모니터를 변경하거나 표시 배율을 변경한 후에 발생합니다. (Issues [#50](https://github.com/seto77/ReciPro/issues/50), [#53](https://github.com/seto77/ReciPro/issues/53), [#55](https://github.com/seto77/ReciPro/issues/55))

**해결 방법**:

1. **작업 관리자**를 엽니다.
2. 프로세스 목록에서 **ReciPro**를 찾습니다.
3. 마우스 오른쪽 버튼으로 클릭하고 **최대화**를 선택합니다.

창이 주 디스플레이로 가져와집니다. **전환**, **앞으로 가져오기**, **최소화**는 도움이 되지 *않으며* — **최대화**만 작동한다는 점에 유의하세요.

### 증상: ReciPro가 시작되지 않거나, 충돌하거나, 시작 시 멈춤

**원인**: 대부분의 경우 OpenGL 초기화가 실패하거나, 손상된 레지스트리/설정 값이 시작을 차단합니다.

**해결 방법** (순서대로 시도):

1. **OpenGL 비활성화**: ReciPro를 실행하는 동안 **Ctrl** 키를 누르고 있으면 OpenGL이 비활성화된 상태로 시작됩니다. 최근 버전(v4.925 이상)은 OpenGL 초기화를 강화하여 OpenGL이 실패하더라도 앱이 실행되도록 합니다 — 이 경우 3D 기능은 비활성화되지만 앱의 나머지 부분은 작동합니다.
2. **설정 초기화**: 레지스트리 편집기에서 키 `HKEY_CURRENT_USER\Software\Crystallography\ReciPro`를 삭제한 다음 다시 시작합니다. (**Option → Reset registry**와 동일합니다.)
3. **클린 재설치**: ReciPro를 제거하고, 다음 폴더가 있으면 삭제한 후(`<user>`를 본인 계정 이름으로 바꾸세요) 다시 설치합니다:
   - `C:\Users\<user>\AppData\Local\Crystallography Software\ReciPro`
   - `C:\Users\<user>\AppData\Roaming\ReciPro\ReciPro`
4. 최신 버전으로 **업데이트**합니다.

이 중 어느 것도 도움이 되지 않으면 OS 환경 자체가 원인일 수 있습니다. PC 세부 정보(CPU, GPU, Windows 버전)와 함께 [이슈를 등록](https://github.com/seto77/ReciPro/issues)해 주세요.

---

## OpenGL 문제

### 증상: 시작 시 검은 화면 또는 충돌

**원인**: 호환되지 않는 GPU 또는 원격 데스크톱 환경.

**해결 방법**:

1. **Option → Disable OpenGL (needs restart)**로 이동합니다(또는 실행 중 **Ctrl**을 누르고 있습니다).
2. ReciPro를 다시 시작합니다.
3. 구조 뷰어와 일부 3D 기능은 소프트웨어 렌더링을 사용합니다.

### 증상: 내장 GPU 또는 오래된 GPU(Intel/AMD)가 렌더링되지 않음

**원인**: 일부 내장 GPU(예: AMD Radeon Vega, Intel UHD)는 오래된 빌드에서 OpenGL 초기화 문제가 있었습니다. (Issue [#2](https://github.com/seto77/ReciPro/issues/2))

**해결 방법**: 최신 버전으로 업데이트하세요. OpenGL 버전 요구 사항이 낮아졌고(v4.781), 내장 GPU 초기화가 수정되었으며(v4.785), 초기화가 제어된 방식으로 실패하도록 추가로 강화되었습니다(v4.925). GPU 드라이버를 업데이트하는 것도 도움이 됩니다.

### 증상: 렌더링 품질이 낮음

**해결 방법**: GPU 드라이버를 업데이트하세요. OpenGL 1.5를 지원하는 외장(독립형) GPU를 권장합니다.

---

## .NET Runtime

### 증상: 애플리케이션이 시작되지 않음

**원인**: 필요한 .NET Desktop Runtime이 설치되어 있지 않습니다. 현재 버전은 **.NET Desktop Runtime 10.0**이 필요합니다(오래된 빌드: v4.895–v4.91x는 9.0이 필요했습니다. 이슈 [#43](https://github.com/seto77/ReciPro/issues/43) 참조).

**해결 방법**: <https://dotnet.microsoft.com/download/dotnet/10.0>에서 다운로드하여 설치하세요(**Desktop Runtime**을 선택, 대부분의 PC는 x64).

### 증상: Microsoft 다운로드 페이지에 접속할 수 없음

**해결 방법**: 런타임 설치 프로그램을 직접 다운로드할 수 있습니다. [.NET 10.0 다운로드 페이지](https://dotnet.microsoft.com/download/dotnet/10.0)에서 사용 중인 아키텍처에 맞는 **Windows Desktop Runtime X64**를 선택하세요. (Issue [#49](https://github.com/seto77/ReciPro/issues/49))

---

## 설치

### 증상: 관리자 권한 없이 설치 또는 제거

**참고**: 관리자 권한은 필요하지 않습니다. 바로 가기와 사용자별 파일은 본인의 사용자 폴더(예: `%AppData%\Microsoft\Windows\Start Menu\Programs\Crystallography Software\`와 바탕 화면)에 배치됩니다. (Issue [#38](https://github.com/seto77/ReciPro/issues/38))

---

## 표시 및 레이아웃

### 증상: 버튼이나 패널이 잘리거나 가려짐, 또는 레이아웃이 깨져 보임

예를 들어 최근 버전에서 Spot ID v2의 **Peak Identification** 버튼이 가려지거나, 정보 페이지 및 기타 폼이 잘못 정렬됩니다. (Issues [#56](https://github.com/seto77/ReciPro/issues/56), [#59](https://github.com/seto77/ReciPro/issues/59))

**원인**: 일부 최근 빌드에서 도입된 DPI 배율 / UI 글꼴 회귀.

**해결 방법**:

- Windows **표시 배율을 100%**로 설정하세요(이렇게 하면 대부분 레이아웃이 복원됩니다).
- 빠른 임시 해결책으로, **창 크기를 조정**하여(예: 세로로 줄여서) 가려진 컨트롤을 표시하세요.
- 최신 버전으로 업데이트하세요 — 레이아웃은 점진적으로 수정되고 있습니다. 최근 빌드가 더 나빠 보이면 약간 더 오래된 버전(예: v4.915)으로 되돌리는 것이 임시 방편입니다. 남아 있는 깨진 폼이 있으면 보고해 주세요.

---

## 동역학적 계산

### 증상: 매우 느리거나 메모리 부족

**원인**: 블로흐파가 너무 많거나 이미지가 너무 큽니다.

**해결 방법**:

- **No. of Bloch waves**를 줄이세요(일상적인 계산에는 보통 50–200이면 충분합니다)
- ≤ 500파에는 **Eigen** 솔버를, > 500파에는 **MKL**을 사용하세요
- STEM 시뮬레이션의 경우 이미지 해상도를 낮추세요
- 메모리를 많이 사용하는 다른 애플리케이션을 닫으세요

### 증상: HAADF-STEM 이미지가 검게 나옴

**원인**: 원자 온도 인자(B)가 0으로 설정되어 있습니다.

**해결 방법**: 모든 원자에 대해 B ≥ 0.5 Å²로 설정하세요. TDS 강도는 0이 아닌 온도 인자를 필요로 합니다.

---

## 회절 시뮬레이터

### 증상: 회절 패턴이 비어 있음 / 아무것도 그려지지 않음

**원인**: 대개 보기가 너무 많이 확대되었거나, 입사파 에너지가 범위를 벗어났습니다. (Issue [#3](https://github.com/seto77/ReciPro/issues/3))

**해결 방법**:

- 메인 그리기 영역을 **왼쪽 클릭**하여 축소하세요.
- **Wave** 탭(왼쪽 위)에서 입사파 에너지를 확인하세요: X선 ≈ 1–100 keV, 전자 ≈ 10–1000 keV가 적절합니다.

---

## 파일 입출력

### 증상: CIF 파일이 로드되지 않음

**해결 방법**:

- CIF 파일 형식이 올바른지 확인하세요
- 파일을 **결정 정보** 영역에 끌어서 놓아 보세요
- 일부 비표준 CIF 확장은 지원되지 않을 수 있습니다

### 증상: dm3/dm4 파일이 로드되지 않거나 "unable to cast … 'System.Single' to 'System.Double'"

**원인**: DM3/DM4 형식에는 여러 변형이 있으며, 오래된 빌드는 그 전부를 읽지 못했습니다. (Issue [#15](https://github.com/seto77/ReciPro/issues/15))

**해결 방법**: 최신 버전으로 업데이트하세요 — DM3 읽기 호환성이 v4.835에서 개선되었습니다. 파일이 여전히 로드되지 않으면 지원을 추가할 수 있도록 [파일을 보내](https://github.com/seto77/ReciPro/issues)주세요.

### 증상: dm3/dm4 파일의 축척이 잘못 표시됨

**해결 방법**: 원본 Digital Micrograph 소프트웨어에서 보정을 확인하세요. ReciPro는 내장된 메타데이터를 읽습니다. 메타데이터가 잘못된 경우 Optics 패널에서 픽셀 크기와 카메라 길이를 수동으로 설정하세요.

---

## 레지스트리 초기화

설정이 손상된 경우:

1. **Option → Reset registry (after restart)**
2. ReciPro를 다시 시작하세요 — 창 위치, 파장, 카메라 길이 등이 기본값으로 초기화됩니다

---

## 자주 묻는 질문

### Mac(또는 Linux) 버전이 있나요? {#mac-linux}

공식 Mac 또는 Linux 버전은 없습니다. ReciPro는 현재 Windows에서만 실행되는 **.NET Desktop Runtime**에 의존합니다. (Issue [#12](https://github.com/seto77/ReciPro/issues/12))

다만 macOS에서 작동한다고 보고된 비공식 경로가 있습니다: **win-x64 portable ZIP** 배포판([릴리스 페이지](https://github.com/seto77/ReciPro/releases/latest)에서 제공)은 **Sikarugir** Wine 래퍼와 **Mesa3D** OpenGL 드라이버를 조합하여 macOS(Apple Silicon)에서 실행되며 — Windows 라이선스나 가상 머신이 필요하지 않습니다. 한 사용자가 게시한 단계별 설정 안내서는 <https://github.com/Ryo-fkushima/ReciPro_macOS_memo>에서 볼 수 있습니다.

이 구성은 공식적으로 지원되거나 완전히 검증된 것이 아니라는 점에 유의하세요. 알려진 제한 사항으로 일부 기호(Å, 위 첨자, 화살표)가 잘못 표시될 수 있습니다.

**깨진 기호(Å, 위 첨자, 화살표) 수정:** 원인은 ReciPro가 일반적으로 사용하는 Windows 글꼴(Segoe UI, Yu Gothic UI 등)이 Wine 환경에 없고, Wine에 내장된 대체 글꼴에 일부 과학 글리프가 빠져 있기 때문입니다. ReciPro는 **Wine 아래에서 실행 중임을 감지하면** 폭넓게 지원되는 글꼴로 자동 전환하므로, 해결책은 그러한 글꼴을 Wine prefix에서 사용할 수 있게 만드는 것뿐입니다:

1. **DejaVu Sans** / **DejaVu Serif**(Å, 위 첨자, 화살표, 분수 레이블을 지원)를 설치하고, 일본어 UI의 경우 **Noto Sans CJK JP**(또는 **Noto Sans JP**)를 설치하세요.
2. 가장 간단한 방법은 다운로드한 `.ttf`/`.otf` 파일을 prefix의 글꼴 폴더 — Sikarugir 래퍼 내부의 `…/drive_c/windows/Fonts/` — 에 복사한 다음 ReciPro를 다시 실행하는 것입니다. (`winetricks`로도 그중 일부를 설치할 수 있습니다.)
3. 다시 시작하면 ReciPro가 자동으로 이를 인식합니다. ReciPro 설정은 변경할 필요가 없습니다.

글꼴이 설치되어 있지 않으면 ReciPro는 기본 글꼴 이름을 유지하므로 상황이 더 나빠지지는 않습니다 — 기호가 그대로 깨진 채로 남을 뿐입니다.

**이 경로에 대한 전망 — 두 가지 솔직한 참고 사항:**

- 실험적인 **win-arm64** ZIP은 Apple Silicon에서도 현재의 Mac에서는 실행되지 **않습니다**: 오늘날의 macOS Wine 빌드(Sikarugir 포함)는 x86_64 Windows 바이너리를 Rosetta 2를 통해 실행하며, ARM64 Windows 바이너리를 실행할 메커니즘이 없습니다. Mac에서는 항상 **win-x64** portable ZIP을 사용하세요.
- Apple은 Rosetta 2를 단계적으로 폐지하고 있습니다. macOS 27(2026년 가을)이 완전한 Rosetta 2 지원을 제공하는 마지막 버전으로 발표되었으므로, 현재의 x64 + Rosetta 경로는 macOS 28(2027년 가을)부터 작동을 멈출 것으로 예상됩니다. macOS용 네이티브 ARM64 Wine이 상류에서 개발 중이며, 만약 실현된다면 win-arm64 ZIP이 Mac에서 후속 수단이 될 수 있지만, 아직 약속할 수는 없습니다.

### ReciPro가 Windows on ARM(ARM64)에서 실행되나요? {#windows-on-arm}

예 — 두 가지 경로가 있습니다:

- **네이티브 ARM64 패키지(실험적, 권장)**: v4.938부터 실험적인 네이티브 ARM64 portable 패키지(`ReciPro-v.X_arm64.zip`; v.4.939까지는 `ReciPro-v.X-arm64.zip`로 명명됨)가 [릴리스 페이지](https://github.com/seto77/ReciPro/releases/latest)에 게시됩니다. self-contained이므로 .NET Runtime 설치가 필요하지 않습니다 — 사용자가 쓸 수 있는 폴더에 ZIP을 압축 해제하고 `ReciPro.exe`를 실행하세요. Windows가 다운로드한 ZIP을 차단하면(Mark of the Web), 압축을 풀기 *전에* ZIP을 마우스 오른쪽 버튼으로 클릭 → **속성** → **차단 해제** 체크 → **OK**를 하세요(또는 PowerShell에서 `Unblock-File .\ReciPro-*arm64.zip`를 실행하세요). 자세한 내용은 동봉된 `README-PORTABLE.txt`에 있습니다.
- **에뮬레이션 아래의 x64 패키지**: 일반 MSI 설치 프로그램과 win-x64 portable ZIP도 .NET Desktop Runtime(x64)이 설치된 상태에서 내장 x64 에뮬레이션을 통해 ARM64 Windows에서 실행됩니다(.NET 10이 적용된 v4.913 무렵부터 확인됨). 무거운 계산은 네이티브 빌드보다 느리게 실행됩니다. (Issue [#47](https://github.com/seto77/ReciPro/issues/47))

네이티브 ARM64 패키지에 대한 참고 사항:

- Intel MKL은 ARM64용이 존재하지 않으므로 해당 솔버 옵션과 메뉴 항목이 숨겨집니다. 동역학적 계산은 동봉된 NEON 최적화 네이티브 라이브러리를 사용합니다. 대표적인 검증 사례에서 그 결과는 예상되는 부동 소수점 허용 오차 범위 내에서 x64 빌드와 일치했습니다.
- 3D 보기(구조 뷰어 및 관련 창)는 실행될 수 있지만, Windows on ARM은 Direct3D 12 변환 계층(GLOn12 / Mesa)을 통해서만 OpenGL을 제공하므로, 3D 렌더링이 네이티브 OpenGL 드라이버가 있는 PC에 비해 눈에 띄게 느립니다 — 이는 플랫폼 제한이며 버그가 아니고, 네이티브 ARM64 빌드로도 바꿀 수 없습니다. 구조 뷰어의 **High quality (Per-Pixel Linked List)** 투명도 모드는 이 드라이버 스택에서 특히 느립니다. 기본 **Approximate** 모드를 권장합니다. 3D 보기가 시작되지 않으면 Microsoft Store에서 "OpenCL, OpenGL, and Vulkan Compatibility Pack"을 설치하세요.
- ARM64 패키지는 macOS + Wine에서 실행되지 **않습니다**(이전 질문 참조). Mac에서는 win-x64 portable ZIP을 사용하세요.

### ReciPro를 어떻게 인용해야 하나요?

[GitHub 저장소 페이지](https://github.com/seto77/ReciPro)의 **Cite this repository** 링크를 사용하세요(메타데이터는 `CITATION.cff`에서 제공됩니다). 권장 인용은 다음과 같습니다:

> Seto, Y. & Ohtsuka, M. (2022). *J. Appl. Cryst.* **55**, 397–410. doi:[10.1107/S1600576722000139](https://doi.org/10.1107/S1600576722000139)

(Issue [#33](https://github.com/seto77/ReciPro/issues/33))

---

## 버그 보고

문제는 다음에서 보고하세요: <https://github.com/seto77/ReciPro/issues>

다음 내용을 포함해 주세요:

- ReciPro 버전 번호
- 문제를 재현하는 단계
- 오류 메시지 또는 스크린샷
