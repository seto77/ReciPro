# 내장 함수

ReciPro 매크로에서 사용할 수 있는 클래스와 함수의 전체 레퍼런스입니다.

---

## File 클래스

| 함수 | 설명 |
|----------|-------------|
| `File.GetDirectoryPath()` | 폴더 선택 대화 상자를 표시하고 선택한 경로를 반환 |
| `File.GetFileName()` | 파일 선택 대화 상자를 표시하고 선택한 경로를 반환 |
| `File.GetFileNames()` | 다중 파일 선택 대화 상자를 표시하고 경로 목록을 반환 |
| `File.ReadCrystalList()` | 결정 목록 파일(*.xml)을 불러오기 |
| `File.ReadCrystal()` | CIF/AMC 결정 파일을 불러오기 |
| `File.ExportAsCIF()` | 현재 결정을 CIF로 내보내기 |
| `File.SaveText()` | 텍스트 데이터를 파일에 저장 |

---

## Crystal 클래스

| 속성 | 형식 | 설명 |
|----------|------|-------------|
| `Crystal.Name` | string | 결정 이름 |
| `Crystal.ChemicalFormula` | string | 화학식 |
| `Crystal.Density` | double | 밀도 (g/cm³) |

---

## CrystalList 클래스

| 함수 / 속성 | 설명 |
|---------------------|-------------|
| `CrystalList.SelectedIndex` | 선택한 결정의 인덱스를 가져오기/설정 |
| `CrystalList.Add()` | 현재 결정을 목록에 추가 |
| `CrystalList.Replace()` | 선택한 결정을 교체 |
| `CrystalList.Delete()` | 선택한 결정을 삭제 |
| `CrystalList.ClearAll()` | 모든 결정을 지우기 |
| `CrystalList.MoveUp()` | 선택한 결정을 위로 이동 |
| `CrystalList.MoveDown()` | 선택한 결정을 아래로 이동 |

---

## Direction 클래스

| 함수 | 설명 |
|----------|-------------|
| `Direction.Euler(phi, theta, psi)` | 오일러 각으로 방위를 설정 (라디안) |
| `Direction.EulerInDegree(phi, theta, psi)` | 오일러 각으로 방위를 설정 (도) |
| `Direction.EulerInDeg(phi, theta, psi)` | `EulerInDegree`의 별칭 |
| `Direction.Rotate(ax, ay, az, angle)` | 임의의 축을 중심으로 회전 (라디안) |
| `Direction.RotateInDeg(ax, ay, az, angle)` | 임의의 축을 중심으로 회전 (도) |
| `Direction.RotateAroundAxis(u, v, w, angle)` | 정대축 [uvw]을 중심으로 회전 (라디안) |
| `Direction.RotateAroundAxisInDeg(u, v, w, angle)` | 정대축 [uvw]을 중심으로 회전 (도) |
| `Direction.RotateAroundPlane(h, k, l, angle)` | 면 법선 (hkl)을 중심으로 회전 (라디안) |
| `Direction.RotateAroundPlaneInDeg(h, k, l, angle)` | 면 법선 (hkl)을 중심으로 회전 (도) |
| `Direction.ProjectAlongPlane(h, k, l)` | 면 법선을 화면에 수직으로 설정 |
| `Direction.ProjectAlongAxis(u, v, w)` | 정대축을 화면에 수직으로 설정 |

---

## DifSim 클래스

### 창 제어

`DifSim.Open()` / `DifSim.Close()`

### 파동원

`DifSim.Source_Xray()` / `DifSim.Source_Electron()` / `DifSim.Source_Neutron()`

### 속성

| 속성 | 형식 | 설명 |
|----------|------|-------------|
| `Energy` | double | 에너지 (keV) |
| `Wavelength` | double | 파장 (Å) |
| `Thickness` | double | 시료 두께 (nm) |
| `NumberOfDiffractedWaves` | int | 블로흐파의 수 |
| `CameraLength2` | double | 카메라 길이 (mm) |
| `SkipRendering` | bool | 일괄 처리를 위해 렌더링을 건너뛰기 |

### 빔 모드

`Beam_Parallel()` / `Beam_PrecessionXray()` / `Beam_PrecessionElectron()` / `Beam_Convergence()`

### 계산 모드

`Calc_Excitation()` / `Calc_Kinematical()` / `Calc_Dynamical()`

### 이미지 설정

| 속성 / 함수 | 설명 |
|---------------------|-------------|
| `ImageResolutionInMM` | 해상도 (mm/pixel) |
| `ImageResolutionInNMinv` | 해상도 (nm⁻¹/pixel) |
| `ImageWidth` / `ImageHeight` | 이미지 크기 (pixel) |
| `ImageSize(w, h)` | 이미지 크기 설정 |

### 검출기

| 속성 | 설명 |
|----------|-------------|
| `Tau` / `TauInDeg` | 검출기 기울기 각 τ (rad / deg) |
| `Phi` / `PhiInDeg` | 검출기 회전축 φ (rad / deg) |
| `Foot(x, y)` | Foot 위치 (pixel) |

### 출력

| 함수 | 설명 |
|----------|-------------|
| `SaveAsPng()` | 현재 패턴을 PNG로 저장 |
| `SpotInfo()` | 스폿 데이터를 CSV 문자열로 가져오기 |

---

## HRTEM / STEM / Potential 클래스

이 세 가지 이미지 시뮬레이션 클래스는 많은 멤버를 공유합니다. 반복을 피하기 위해 아래 표에서는 자리 표시자를 사용합니다.

- **`#`** : **HRTEM**, **STEM**, **Potential**에 공통. `#`를 `HRTEM`, `STEM`, 또는 `Potential`로 바꿉니다 (예: `STEM.Simulate()`, `Potential.AccVol`).
- **`$`** : **HRTEM**과 **STEM**에만 공통. `$`를 `HRTEM` 또는 `STEM`으로 바꿉니다.
- 명시적인 클래스 이름으로 작성된 멤버(`STEM.…` / `HRTEM.…`)는 해당 클래스에만 속합니다. **Potential** 클래스는 자체 멤버를 추가하지 않으며, `#` 멤버만 사용합니다.

### 창 제어

| 함수 | 설명 |
|----------|-------------|
| `#.Open()` | 이미지 시뮬레이터 창을 열기 |
| `#.Close()` | 이미지 시뮬레이터 창을 닫기 |
| `#.Simulate()` | 현재 설정으로 시뮬레이션을 실행 |

### 현미경 / 광학계

| 속성 / 함수 | 설명 |
|---------------------|-------------|
| `#.AccVol` | 가속 전압 (kV) |
| `$.Thickness` | 시료 두께 (nm) |
| `$.Defocus` | 디포커스 (nm) |
| `$.Cs` | 구면 수차 Cs (mm) |
| `$.Cc` | 색 수차 Cc (mm) |
| `$.DeltaV` | 에너지 퍼짐 ΔV, FWHM (eV) |
| `$.Scherzer` | Scherzer 디포커스 (nm, 읽기 전용) |
| `STEM.ConvergenceAngle` | 수렴 반각 (mrad) |
| `STEM.DetectorInnerAngle` / `STEM.DetectorOuterAngle` | 환형 검출기의 내부/외부 반각 (mrad) |
| `STEM.EffectiveSourceSize` | 유효 전자원 크기, FWHM (pm) |
| `HRTEM.Beta` | 조명 반각 β (라디안) |
| `HRTEM.ApertureSemiangle` | 대물 조리개 반각 (라디안) |
| `HRTEM.ApertureShiftX` / `HRTEM.ApertureShiftY` | 대물 조리개 이동 (라디안) |
| `HRTEM.OpenAperture` | 대물 조리개 열림 (true/false) |

### 시뮬레이션 속성

| 속성 / 함수 | 설명 |
|---------------------|-------------|
| `#.NumberOfDiffractedWaves` | 회절된 (블로흐) 파의 최대 수 |
| `#.ImageWidth` / `#.ImageHeight` | 이미지 크기 (pixel) |
| `#.ImageSize(width, height)` | 이미지 크기 설정 (pixel) |
| `#.ImageResolution` | 이미지 해상도 (nm/pixel) |
| `STEM.AngularResolution` | 수렴빔의 각도 해상도 (mrad) |
| `STEM.SliceThickness` | TDS 계산을 위한 슬라이스 두께 (nm) |
| `HRTEM.Mode_LinearImage()` | 선형 이미지 (준-가간섭성) 모델 사용 |
| `HRTEM.Mode_TCC()` | TCC (transmission cross coefficient) 모델 사용 |

### 단일 / 연속 이미지 모드

| 속성 / 함수 | 설명 |
|---------------------|-------------|
| `$.SingleImageMode()` | 단일 이미지 모드로 전환 |
| `$.SerialImageMode(withThickness, withDefocus)` | 연속 이미지 모드로 전환 |
| `$.SerialImageThicknessStart` / `Step` / `Num` | 연속 두께: 시작 (nm) / 간격 (nm) / 개수 |
| `$.SerialImageDefocusStart` / `Step` / `Num` | 연속 디포커스: 시작 (nm) / 간격 (nm) / 개수 |

### 이미지 속성

| 속성 / 함수 | 설명 |
|---------------------|-------------|
| `#.UnitCellVisible` | 단위 격자 표시 (true/false) |
| `#.LabelVisible` | 이미지 레이블 표시 (true/false) |
| `#.LabelSize` | 레이블 글꼴 크기 |
| `#.ScaleBarVisible` | 축척 막대 표시 (true/false) |
| `#.ScaleBarLength` | 축척 막대 길이 (nm) |
| `#.GaussianBlurEnabled` | 가우시안 블러 적용 (true/false) |
| `#.GaussianBlurFWHM` | 가우시안 블러 FWHM (pm) |
| `STEM.DisplayBoth()` | 탄성 성분과 TDS 성분을 모두 표시 |
| `STEM.DisplayElastic()` | 탄성 성분만 표시 |
| `STEM.DisplayTDS()` | TDS (비탄성) 성분만 표시 |

### 이미지 저장

| 속성 / 함수 | 설명 |
|---------------------|-------------|
| `#.SaveImageAsPng(filename)` | PNG로 저장 (filename을 생략하면 대화 상자 표시) |
| `#.SaveImageAsTif(filename)` | TIFF로 저장 (filename을 생략하면 대화 상자 표시) |
| `#.SaveImageAsEmf(filename)` | EMF 메타파일로 저장 (filename을 생략하면 대화 상자 표시) |
| `#.SaveIndividually` | 연속 모드에서 각 이미지를 개별적으로 저장 (true/false) |
| `#.OverprintSymbols` | 저장된 이미지에 단위 격자 / 레이블 / 축척 막대를 겹쳐 인쇄 (true/false) |

---

## 전역 함수

| 함수 | 설명 |
|----------|-------------|
| `Sleep(ms)` | 지정한 밀리초만큼 대기 |

---

## 참고

- [20. 매크로](index.md)
- [20.2. 예제](2-examples.md)
