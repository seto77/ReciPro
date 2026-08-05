# 내장 함수

ReciPro 매크로에서 사용할 수 있는 클래스와 함수의 전체 레퍼런스입니다.

---

## File 클래스

| 함수 | 설명 |
|----------|-------------|
| `File.GetDirectoryPath(filename)` | 폴더 선택 대화 상자를 표시하고 선택한 경로를 반환. `filename` 을 주면 그 파일이 들어 있는 폴더를 반환한다 |
| `File.GetFileName()` | 파일 선택 대화 상자를 표시하고 선택한 경로를 반환 |
| `File.GetFileNames()` | 다중 파일 선택 대화 상자를 표시하고 경로 목록을 반환 |
| `File.ReadCrystalList(filename)` | 결정 목록 파일(*.xml)을 불러오기. `filename` 을 생략하면 대화 상자를 연다 |
| `File.ReadCrystal(filename)` | CIF/AMC 결정 파일을 불러오기. `filename` 을 생략하면 대화 상자를 연다 |
| `File.ExportAsCIF(filename)` | 현재 결정을 CIF로 내보내기. `filename` 을 생략하면 대화 상자를 연다 |
| `File.ReadText(filename)` | 텍스트 파일을 UTF-8 로 읽어 문자열로 반환. `filename` 을 생략하면 대화 상자를 연다. `Crystal.LoadCifText()` / `SaveText()` 와 짝으로 사용 |
| `File.SaveText(textData, filename)` | 텍스트 데이터를 파일에 저장. `textData` 를 UTF-8 로 기록하며, `filename` 을 생략하면 저장 대화 상자를 연다 |

---

## Crystal 클래스

현재 선택된 결정을 읽고, pending 초안을 통해 결정을 생성·편집합니다.

### 읽기

| 속성 / 함수 | 설명 |
|---|---|
| `Crystal.Name` | 결정 이름 |
| `Crystal.ChemicalFormula` | 화학식 |
| `Crystal.Density` | 밀도(g/cm³) |
| `Crystal.GetCellInAng()` | 격자 상수를 `[a, b, c, alpha, beta, gamma]`(Å·도)로 가져오기 |
| `Crystal.SpaceGroupName` | 공간군의 Hermann–Mauguin 기호(설정이 여러 개인 군에서는 `:2`, `:H` 등의 설정 접미사 포함) |
| `Crystal.SpaceGroupNumber` | International Tables 공간군 번호(1–230) |
| `Crystal.HasPending` | pending 초안이 열려 있는지 |

### 생성과 편집 (초안 → Commit)

결정은 **pending 초안**으로 조립합니다: 초안을 시작하고 setter 로 값을 채우면, `Commit()` 이 전체 검증 → 결정 구축 → 현재 결정으로의 적용을 한 번에 수행합니다 (CIF 파일을 읽을 때처럼 GUI 와 열려 있는 모든 시뮬레이터가 갱신됩니다). `Commit()` 이 실패하면 검증 오류를 전부 모아서 보고하고, 현재 결정은 바꾸지 않으며 초안도 유지되므로 수정 후 그대로 다시 Commit 할 수 있습니다.

| 함수 | 설명 |
|---|---|
| `Crystal.BeginCreate(name)` | 새 결정의 초안을 시작 |
| `Crystal.BeginEdit()` | 현재 결정에서 초안을 시작(격자·공간군·원자·방위를 이어받음) |
| `Crystal.LoadCifText(cifText)` | CIF 텍스트(.cif 파일의 내용. 경로가 아님)에서 초안을 시작 |
| `Crystal.SetName(name)` | 초안의 이름을 변경 |
| `Crystal.SetCellInAng(a, b, c, alpha, beta, gamma)` | 격자 상수를 **Å·도**로 설정. 호출할 때마다 격자 전체를 다시 지정한다. 생략한 인수는 공간군 제약에서 도출되며(입방정이면 `a` 만으로 충분), 명시값이 제약과 모순되면 오류 |
| `Crystal.SetSpaceGroup(symbol)` | 공간군을 기호로 설정(HM 짧은/전체 표기 또는 Hall. 공백과 `_` 무시). 설정이 여러 개인 군에서는 설정을 붙인다(`'Fd-3m:2'`, `'R-3c:H'`, `'P21/c:b1'`) — 모호한 기호는 후보 목록과 함께 오류 |
| `Crystal.SetSpaceGroupByNumber(itNumber, setting)` | 공간군을 IT 번호(1–230)로 설정. 설정이 여러 개이면 `setting`(`'1'`, `'2'`, `'H'`, `'R'`, `'b1'` 등)으로 선택 |
| `Crystal.AddAtom(label, element, x, y, z, occ, bIso)` | 비대칭 단위의 원자를 추가: 원소 기호·분율 좌표·점유율(0 < occ ≤ 1, 기본 1)·등방성 B(Å², 기본 0). 등가 위치·Wyckoff 기호·다중도는 자동 도출 |
| `Crystal.ClearAtoms()` | 초안의 원자를 전부 삭제 |
| `Crystal.Commit()` | 초안을 검증·구축·적용 |
| `Crystal.Cancel()` | 초안을 파기 |

```python
ReciPro.Crystal.BeginCreate('NaCl')
ReciPro.Crystal.SetSpaceGroup('Fm-3m')
ReciPro.Crystal.SetCellInAng(5.6402)
ReciPro.Crystal.AddAtom('Na', 'Na', 0, 0, 0)
ReciPro.Crystal.AddAtom('Cl', 'Cl', 0.5, 0.5, 0.5)
ReciPro.Crystal.Commit()

base = ReciPro.Crystal.GetCellInAng()
for k in range(-2, 3):
    ReciPro.Crystal.BeginEdit()
    ReciPro.Crystal.SetCellInAng(base[0] * (1 + 0.01 * k))
    ReciPro.Crystal.Commit()
```

`Commit()` 성공 후 다음 `BeginEdit()` 은 **갱신된** 결정을 기점으로 하므로 변경이 누적됩니다 — 절대값으로 스캔할 때는 위 예처럼 루프 전에 기준값을 읽어 두십시오. Commit 한 결정을 결정 목록에 등록하려면 `CrystalList.Add()` 를 호출합니다.

---

## CrystalList 클래스

| 함수 / 속성 | 설명 |
|---------------------|-------------|
| `CrystalList.SelectedIndex` | 선택한 결정의 인덱스를 가져오기/설정 |
| `CrystalList.Count` | 결정 목록에 등록된 결정의 수 |
| `CrystalList.Add()` | 현재 결정을 목록에 추가 |
| `CrystalList.Replace()` | 선택한 결정을 교체 |
| `CrystalList.Delete()` | 선택한 결정을 삭제 |
| `CrystalList.ClearAll()` | 모든 결정을 지우기 |
| `CrystalList.MoveUp()` | 선택한 결정을 위로 이동 |
| `CrystalList.MoveDown()` | 선택한 결정을 아래로 이동 |

---

## Dir 클래스

| 함수 | 설명 |
|----------|-------------|
| `Dir.Euler(phi, theta, psi)` | 오일러 각으로 방위를 설정 (라디안) |
| `Dir.EulerInDegree(phi, theta, psi)` | 오일러 각으로 방위를 설정 (도) |
| `Dir.EulerInDeg(phi, theta, psi)` | `EulerInDegree`의 별칭 |
| `Dir.Rotate(ax, ay, az, angle)` | 임의의 축을 중심으로 회전 (라디안) |
| `Dir.RotateInDeg(ax, ay, az, angle)` | 임의의 축을 중심으로 회전 (도) |
| `Dir.RotateAroundAxis(u, v, w, angle)` | 정대축 [uvw]을 중심으로 회전 (라디안) |
| `Dir.RotateAroundAxisInDeg(u, v, w, angle)` | 정대축 [uvw]을 중심으로 회전 (도) |
| `Dir.RotateAroundPlane(h, k, l, angle)` | 면 법선 (hkl)을 중심으로 회전 (라디안) |
| `Dir.RotateAroundPlaneInDeg(h, k, l, angle)` | 면 법선 (hkl)을 중심으로 회전 (도) |
| `Dir.ProjectAlongPlane(h, k, l)` | 면 법선을 화면에 수직으로 설정 |
| `Dir.ProjectAlongAxis(u, v, w)` | 정대축을 화면에 수직으로 설정 |
| `Dir.GetEuler()` | 현재 방위를 Z-X-Z 오일러 각 `[phi, theta, psi]`(라디안)으로 가져오기 |
| `Dir.GetEulerInDeg()` | 현재 방위를 Z-X-Z 오일러 각 `[phi, theta, psi]`(도)로 가져오기 |
| `Dir.GetRotationMatrix()` | 현재 회전 행렬을 9 요소 배열 `[R11, R12, R13, R21, R22, R23, R31, R32, R33]` 로 가져오기(`SpotID.CandidateList()` 와 같은 규약) |
| `Dir.SetRotationMatrix(r11, r12, r13, r21, r22, r23, r31, r32, r33)` | 회전 행렬의 9 요소로 방위를 설정(검증과 재직교화를 거쳐 적용) |

오일러 각은 짐벌 위치(θ = 0 또는 180°)에서 유일하지 않으므로, `Euler()` 뒤의 `GetEuler()` 는 같은 자세를 재현하지만 같은 숫자가 된다고는 보장하지 않습니다. 방위를 정확히 저장·복원하려면 `Dir.GetRotationMatrix()` / `Dir.SetRotationMatrix()` 를 사용하십시오. 전체 규약은 [회전 기하학](../4-rotation-geometry.md) 에 설명되어 있습니다.

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
| `SaveAsPng(filename)` | 현재 패턴을 PNG로 저장. `filename` 을 생략하면 대화 상자를 연다 |
| `SpotInfo()` | 스폿 데이터를 CSV 문자열로 가져오기 |

---

## SpotID 클래스

[Spot ID v2](../11-spot-id-v2.md) 를 매크로에서 구동합니다. 이미지 또는 스폿 목록 읽기 → 스폿 검출 → 방위 동정 → 후보 목록 취득까지를 창을 조작하지 않고 실행할 수 있습니다. `FindSpots()` 와 `Identify()` 는 처리가 끝난 뒤에 돌아오므로 그대로 이어서 호출할 수 있습니다.

### 창 조작

`SpotID.Open()` / `SpotID.Close()`

### 입사파의 종류

`SpotID.Source_Xray()` / `SpotID.Source_Electron()` / `SpotID.Source_Neutron()`

### 처리 흐름

| 함수 | 설명 |
|------|------|
| `SpotID.LoadFile(filename)` | **File > Load** 와 같은 방식으로 파일을 읽는다. `.csv` 는 스폿 목록으로(먼저 이미지를 읽어야 함), 그 밖의 확장자는 회절 도형 이미지로 읽는다(dm3, dm4, mrc, ipa, tif 등 지원 형식). `filename` 을 생략하면 파일 선택 대화 상자를 연다 |
| `SpotID.FindSpots()` | 읽어들인 이미지에서 스폿을 검출하고 피팅한다(**Find spots** 버튼과 동일) |
| `SpotID.Identify()` | 검출된 스폿을 설명하는 방위를 탐색하고(**Identify spots** 버튼과 동일) 후보 수를 반환한다. 대상 결정은 메인 창의 결정 목록에서 선택 중인 것 |
| `SpotID.CandidateList()` | 후보 방위 목록을 CSV 텍스트로 반환한다 |
| `SpotID.SpotList()` | 관측 스폿 목록을 CSV 텍스트로 반환한다(열은 **File > Save** 와 동일). `File.SaveText()` 와 함께 저장하면 `LoadFile()` 로 다시 읽을 수 있다 |

`CandidateList()` 는 후보마다 결정 이름, Z-X-Z 오일러 각(도), 회전 행렬의 9 성분 R11–R33(결정 좌표계 → 실험실 좌표계, 열벡터에 작용), 잔차의 평균 제곱(nm⁻²), 관측 스폿과 *hkl* 지수의 대응을 반환합니다. 후보는 할당된 스폿 수의 내림차순, 그다음 잔차의 오름차순으로 정렬됩니다. 숫자는 invariant culture 로 기록되므로 소수점은 항상 마침표입니다.

### 속성

| 속성 | 형 | 설명 |
|------|----|------|
| `Energy` | double | 입사선의 에너지(X선·전자선은 keV, 중성자선은 meV) |
| `CameraLength` | double | 카메라 길이(mm) |
| `PixelSizeInMM` | double | 이미지의 픽셀 크기(mm). 읽거나 쓰면 픽셀 크기 단위도 mm 로 전환된다 |
| `PixelSizeInNMinv` | double | 이미지의 픽셀 크기(nm⁻¹). 읽거나 쓰면 단위도 nm⁻¹ 로 전환된다 |
| `MaxNumberOfSpots` | int | `FindSpots()` 가 검출하는 스폿 수의 상한 |
| `NearestNeighbor` | int | 검출되는 스폿 사이에 허용하는 최소 간격(픽셀) |
| `FittingRange` | double | 피크 피팅에 사용하는 각 스폿 주위 영역의 반지름(픽셀) |
| `AcceptableError` | double | 관측 스폿을 후보 반사에 대응시킬 때 허용하는 면간격의 상대차(%) |
| `IgnoreProhibitedReflections` | bool | 다중 회절로 나타날 수 있는 소광칙 금지 반사를 무시할지 여부 |
| `MultiGrain` | bool | 여러 결정립을 탐색할지 여부. `False` 이면 단결정 |
| `MaxNumberOfGrains` | int | `MultiGrain` 이 `True` 일 때 탐색하는 결정립 방위의 최대 수 |
| `NumberOfDetectedSpots` | int | 검출된 스폿 수(읽기 전용) |
| `NumberOfCandidates` | int | 직전 `Identify()` 가 찾은 후보 수(읽기 전용) |

---

## StructureViewer 클래스

결정 구조 뷰어를 매크로에서 구동합니다. 3D 모델은 창이 표시될 때 구축되므로, `SaveImage()` 와 `Export3DModel()` 은 필요하면 먼저 창을 엽니다.

| 함수 | 설명 |
|---|---|
| `StructureViewer.Open()` | 결정 구조 뷰어 창을 연다 |
| `StructureViewer.Close()` | 결정 구조 뷰어 창을 닫는다 |
| `StructureViewer.SaveImage(filename)` | 메인 뷰의 렌더링 이미지를 PNG 로 저장(픽셀 크기는 창의 **Size (W×H)** 상자). `filename` 을 생략하면 저장 대화 상자를 연다 |
| `StructureViewer.Export3DModel(filename, maxSizeInMM, fixedScaleInMMperNm, includeAtoms, includeBonds, includePolyhedra, polyhedraAsEdges, polyEdgeDiaInMM, includeCellEdges, cellEdgeDiaInMM, thickenBondsToMM)` | 표시 중인 구조를 3D 프린팅용으로 출력(File 메뉴의 **Export 3D Model (3MF/STL)** 과 동일). 형식은 확장자로 결정(`.stl` = 단색 / `.3mf` = 원소별 색). 필수는 `filename` 뿐이며 나머지 기본값은 대화 상자와 동일(최장변 80 mm·단위 격자 테두리 ⌀2.4 mm·결합 증경 ⌀1.2 mm). `fixedScaleInMMperNm` > 0 을 주면 여러 모형을 같은 축척으로 만들 수 있다 |

```python
ReciPro.StructureViewer.Export3DModel('D:/print/NaCl_60mm.stl', maxSizeInMM=60)
ReciPro.StructureViewer.Export3DModel('D:/print/NaCl_edges.stl', maxSizeInMM=60, polyhedraAsEdges=True)
```

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
