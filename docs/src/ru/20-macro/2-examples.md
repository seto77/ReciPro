# Примеры макросов

Практические примеры макросов ReciPro. Первые несколько — это вводные примеры для начинающих; более поздние примеры демонстрируют пакетные операции.

> **Примечание**: К макро-API ReciPro обращаются в виде `ReciPro.ClassName.Member`. Всплывающее окно автодополнения всегда вставляет полный префикс `ReciPro.`, поэтому вам редко придётся вводить его вручную.

---

## Примеры для начинающих

### A. Базовый цикл

Самый простой способ освоить редактор. Запустите это в режиме **Step by step** и наблюдайте, как `i` и `sq` меняются в панели отладки — это способ ReciPro «выводить» значения (`print()` не работает).

```python
# Loop 10 times and compute the squares.
for i in range(10):
    sq = i * i
```

### B. Использование модуля math

Модуль `math` импортируется автоматически при запуске. Используйте его напрямую.

```python
r = 5.0
area = math.pi * r * r
circumference = 2 * math.pi * r
# Run in Step mode to see 'area' and 'circumference' in the debug panel.
```

### C. Поворот текущего кристалла

```python
# Rotate the current crystal by 30 degrees around the a-axis (x-axis).
ReciPro.Dir.RotateAroundAxisInDeg(1, 0, 0, 30)
```

### D. Выравнивание по оси зоны

```python
# Align so that the [001] zone axis is normal to the screen.
ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
```

### E. Перебор первых нескольких кристаллов

```python
# Collect the names of the first 5 crystals in the list.
names = []
for i in range(5):
    ReciPro.CrystalList.SelectedIndex = i
    names.append(ReciPro.Crystal.Name)
# Run in Step mode to see 'names' grow line by line.
```

### F. Открытие дифракционной картины

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

## Пакетные примеры

### 1. Сохранение дифракционных картин для всех кристаллов

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

### 2. Поворот и съёмка снимков

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

### 3. Задание углов Эйлера

```python
# Euler angles in degrees
ReciPro.Dir.EulerInDeg(45, 30, 60)

# Same thing in radians (math is pre-imported)
ReciPro.Dir.Euler(math.pi/4, math.pi/6, math.pi/3)
```

### 4. Проецирование вдоль плоскостей и осей

```python
ReciPro.Dir.ProjectAlongPlane(1, 1, 1)  # (111) normal → screen
ReciPro.Dir.ProjectAlongAxis(1, 1, 0)   # [110] → screen
```

### 5. Пакетный импорт CIF-файлов

```python
files = ReciPro.File.GetFileNames()
for f in files:
    ReciPro.File.ReadCrystal(f)
    ReciPro.CrystalList.Add()
```

### 6. Экспорт информации о рефлексах

```python
ReciPro.DifSim.Open()
ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
info = ReciPro.DifSim.SpotInfo()
ReciPro.File.SaveText(info, "spot_info.csv")
```

---

## Советы

- **Просмотр значений**: `print()` не работает в этой среде (нет консоли). Используйте пошаговое выполнение **Step by step** и наблюдайте за панелью отладки, в которой перечислены локальные переменные для каждой строки.
- **`math` импортируется заранее**: `math.sqrt(x)`, `math.sin(x)`, `math.pi`, `math.radians(deg)` доступны без `import math`.
- **Ускорение пакетной обработки**: устанавливайте `ReciPro.DifSim.SkipRendering = True` во время плотных циклов и возвращайте `False` после них.
- **Ожидание отрисовки**: вызывайте `ReciPro.Sleep(ms)`, чтобы приостановить выполнение, когда нужно дать графическому интерфейсу время на обработку.
- **Автодополнение**: всплывающее окно показывает полную форму `ReciPro.Class.Member`. Введите несколько букв и подтвердите выбор клавишей `Enter` или `Tab`.

---

## См. также

- [20. Макрос](index.md)
- [20.1. Встроенные функции](1-built-in-functions.md)
