# 매크로 예제

ReciPro 매크로의 실용 예제입니다. 처음 몇 개는 초보자를 위한 입문용이고, 이후 예제는 일괄 처리를 보여줍니다.

> **참고**: ReciPro 매크로 API는 `ReciPro.ClassName.Member` 형식으로 접근합니다. 자동 완성 팝업은 항상 전체 `ReciPro.` 접두사를 삽입하므로, 직접 입력할 일은 거의 없습니다.

---

## 초보자 예제

### A. 기본 루프

에디터를 익히는 가장 쉬운 방법입니다. **Step by step**으로 실행하고 디버그 패널에서 `i`와 `sq`가 변하는 것을 관찰하세요 — 이것이 값을 "출력"하는 ReciPro 방식입니다 (`print()`는 동작하지 않습니다).

```python
# Loop 10 times and compute the squares.
for i in range(10):
    sq = i * i
```

### B. math 모듈 사용하기

`math` 모듈은 시작 시 자동으로 임포트됩니다. 바로 사용하세요.

```python
r = 5.0
area = math.pi * r * r
circumference = 2 * math.pi * r
# Run in Step mode to see 'area' and 'circumference' in the debug panel.
```

### C. 현재 결정 회전하기

```python
# Rotate the current crystal by 30 degrees around the a-axis (x-axis).
ReciPro.Dir.RotateAroundAxisInDeg(1, 0, 0, 30)
```

### D. 정대축에 정렬하기

```python
# Align so that the [001] zone axis is normal to the screen.
ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
```

### E. 처음 몇 개 결정을 순회하기

```python
# Collect the names of the first 5 crystals in the list.
names = []
for i in range(5):
    ReciPro.CrystalList.SelectedIndex = i
    names.append(ReciPro.Crystal.Name)
# Run in Step mode to see 'names' grow line by line.
```

### F. 회절 패턴 열기

```python
# Open the diffraction simulator and display the [001] pattern
# of the first crystal in the list with 200 keV electrons.
ReciPro.CrystalList.SelectedIndex = 0
ReciPro.DifSim.Open()
ReciPro.DifSim.Source_Electron()
ReciPro.DifSim.Energy = 200
ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
```

---

## 일괄 처리 예제

### 1. 모든 결정의 회절 패턴 저장하기

```python
folder = ReciPro.File.GetDirectoryPath()

ReciPro.DifSim.Open()
ReciPro.DifSim.Source_Electron()
ReciPro.DifSim.Energy = 200  # 200 keV
ReciPro.DifSim.Calc_Kinematical()
ReciPro.DifSim.SkipRendering = True

for i in range(80):  # adjust to your crystal count
    ReciPro.CrystalList.SelectedIndex = i
    name = ReciPro.Crystal.Name
    ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
    ReciPro.DifSim.SaveAsPng(folder + name + "_001.png")
    ReciPro.Dir.ProjectAlongAxis(1, 1, 0)
    ReciPro.DifSim.SaveAsPng(folder + name + "_110.png")

ReciPro.DifSim.SkipRendering = False
```

### 2. 회전하며 스냅샷 캡처하기

```python
folder = ReciPro.File.GetDirectoryPath()
ReciPro.DifSim.Open()
ReciPro.DifSim.Source_Electron()
ReciPro.DifSim.Energy = 200

ReciPro.Dir.ProjectAlongAxis(0, 0, 1)

for i in range(90):
    ReciPro.DifSim.SaveAsPng(folder + "rot_%03d.png" % i)
    ReciPro.Dir.RotateAroundAxisInDeg(1, 0, 0, 1)
```

### 3. 오일러 각 설정하기

```python
# Euler angles in degrees
ReciPro.Dir.EulerInDeg(45, 30, 60)

# Same thing in radians (math is pre-imported)
ReciPro.Dir.Euler(math.pi/4, math.pi/6, math.pi/3)
```

### 4. 면과 축을 따라 투영하기

```python
ReciPro.Dir.ProjectAlongPlane(1, 1, 1)  # (111) normal → screen
ReciPro.Dir.ProjectAlongAxis(1, 1, 0)   # [110] → screen
```

### 5. CIF 파일 일괄 가져오기

```python
files = ReciPro.File.GetFileNames()
for f in files:
    ReciPro.File.ReadCrystal(f)
    ReciPro.CrystalList.Add()
```

### 6. 반사 정보 내보내기

```python
ReciPro.DifSim.Open()
ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
info = ReciPro.DifSim.SpotInfo()
ReciPro.File.SaveText(info, "spot_info.csv")
```

---

## 팁

- **값 확인하기**: 이 환경에서는 `print()`가 동작하지 않습니다 (콘솔 없음). **Step by step** 실행을 사용하고, 각 줄의 지역 변수를 나열하는 디버그 패널을 관찰하세요.
- **`math`는 미리 임포트됨**: `math.sqrt(x)`, `math.sin(x)`, `math.pi`, `math.radians(deg)`는 `import math` 없이 사용할 수 있습니다.
- **일괄 처리 가속**: 반복 횟수가 많은 루프에서는 `ReciPro.DifSim.SkipRendering = True`로 설정하고, 이후에 `False`로 되돌리세요.
- **렌더링 대기**: GUI가 따라잡아야 할 때는 `ReciPro.Sleep(ms)`를 호출하여 실행을 일시 정지하세요.
- **자동 완성**: 팝업은 전체 `ReciPro.Class.Member` 형식을 표시합니다. 몇 글자를 입력하고 `Enter` 또는 `Tab`으로 확정하세요.

---

## 참고

- [20. 매크로](index.md)
- [20.1. 내장 함수](1-built-in-functions.md)
