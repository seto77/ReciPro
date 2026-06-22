# Встроенные функции

Полный справочник классов и функций, доступных в макросах ReciPro.

---

## Класс File

| Функция | Описание |
|----------|-------------|
| `File.GetDirectoryPath()` | Показать диалог выбора папки, вернуть выбранный путь |
| `File.GetFileName()` | Показать диалог выбора файла, вернуть выбранный путь |
| `File.GetFileNames()` | Показать диалог выбора нескольких файлов, вернуть список путей |
| `File.ReadCrystalList()` | Загрузить файл списка кристаллов (*.xml) |
| `File.ReadCrystal()` | Загрузить файл кристалла CIF/AMC |
| `File.ExportAsCIF()` | Экспортировать текущий кристалл как CIF |
| `File.SaveText()` | Сохранить текстовые данные в файл |

---

## Класс Crystal

| Свойство | Тип | Описание |
|----------|------|-------------|
| `Crystal.Name` | string | Имя кристалла |
| `Crystal.ChemicalFormula` | string | Химическая формула |
| `Crystal.Density` | double | Плотность (g/cm³) |

---

## Класс CrystalList

| Функция / Свойство | Описание |
|---------------------|-------------|
| `CrystalList.SelectedIndex` | Получить/задать индекс выбранного кристалла |
| `CrystalList.Add()` | Добавить текущий кристалл в список |
| `CrystalList.Replace()` | Заменить выбранный кристалл |
| `CrystalList.Delete()` | Удалить выбранный кристалл |
| `CrystalList.ClearAll()` | Очистить все кристаллы |
| `CrystalList.MoveUp()` | Переместить выбранный кристалл вверх |
| `CrystalList.MoveDown()` | Переместить выбранный кристалл вниз |

---

## Класс Direction

| Функция | Описание |
|----------|-------------|
| `Direction.Euler(phi, theta, psi)` | Задать ориентацию через углы Эйлера (радианы) |
| `Direction.EulerInDegree(phi, theta, psi)` | Задать ориентацию через углы Эйлера (градусы) |
| `Direction.EulerInDeg(phi, theta, psi)` | Псевдоним для `EulerInDegree` |
| `Direction.Rotate(ax, ay, az, angle)` | Поворот вокруг произвольной оси (радианы) |
| `Direction.RotateInDeg(ax, ay, az, angle)` | Поворот вокруг произвольной оси (градусы) |
| `Direction.RotateAroundAxis(u, v, w, angle)` | Поворот вокруг оси зоны [uvw] (радианы) |
| `Direction.RotateAroundAxisInDeg(u, v, w, angle)` | Поворот вокруг оси зоны [uvw] (градусы) |
| `Direction.RotateAroundPlane(h, k, l, angle)` | Поворот вокруг нормали к плоскости (hkl) (радианы) |
| `Direction.RotateAroundPlaneInDeg(h, k, l, angle)` | Поворот вокруг нормали к плоскости (hkl) (градусы) |
| `Direction.ProjectAlongPlane(h, k, l)` | Установить нормаль к плоскости перпендикулярно экрану |
| `Direction.ProjectAlongAxis(u, v, w)` | Установить ось зоны перпендикулярно экрану |

---

## Класс DifSim

### Управление окном

`DifSim.Open()` / `DifSim.Close()`

### Источник волны

`DifSim.Source_Xray()` / `DifSim.Source_Electron()` / `DifSim.Source_Neutron()`

### Свойства

| Свойство | Тип | Описание |
|----------|------|-------------|
| `Energy` | double | Энергия (keV) |
| `Wavelength` | double | Длина волны (Å) |
| `Thickness` | double | Толщина образца (nm) |
| `NumberOfDiffractedWaves` | int | Число блоховских волн |
| `CameraLength2` | double | Длина камеры (mm) |
| `SkipRendering` | bool | Пропустить отрисовку для пакетной обработки |

### Режим пучка

`Beam_Parallel()` / `Beam_PrecessionXray()` / `Beam_PrecessionElectron()` / `Beam_Convergence()`

### Режим расчёта

`Calc_Excitation()` / `Calc_Kinematical()` / `Calc_Dynamical()`

### Настройки изображения

| Свойство / Функция | Описание |
|---------------------|-------------|
| `ImageResolutionInMM` | Разрешение (mm/пиксель) |
| `ImageResolutionInNMinv` | Разрешение (nm⁻¹/пиксель) |
| `ImageWidth` / `ImageHeight` | Размер изображения (пиксели) |
| `ImageSize(w, h)` | Задать размер изображения |

### Детектор

| Свойство | Описание |
|----------|-------------|
| `Tau` / `TauInDeg` | Угол наклона детектора τ (rad / градусы) |
| `Phi` / `PhiInDeg` | Ось поворота детектора φ (rad / градусы) |
| `Foot(x, y)` | Положение foot в пикселях |

### Вывод

| Функция | Описание |
|----------|-------------|
| `SaveAsPng()` | Сохранить текущую картину как PNG |
| `SpotInfo()` | Получить данные о рефлексах в виде строки CSV |

---

## Классы HRTEM / STEM / Potential

Эти три класса симуляции изображений имеют много общих членов. Чтобы избежать повторений, в таблицах ниже используются заполнители:

- **`#`** : общие для **HRTEM**, **STEM** и **Potential**. Замените `#` на `HRTEM`, `STEM` или `Potential` (например, `STEM.Simulate()`, `Potential.AccVol`).
- **`$`** : общие только для **HRTEM** и **STEM**. Замените `$` на `HRTEM` или `STEM`.
- Члены, записанные с явным именем класса (`STEM.…` / `HRTEM.…`), принадлежат только этому классу. Класс **Potential** не добавляет собственных членов; он использует только члены `#`.

### Управление окном

| Функция | Описание |
|----------|-------------|
| `#.Open()` | Открыть окно симулятора изображений |
| `#.Close()` | Закрыть окно симулятора изображений |
| `#.Simulate()` | Запустить симуляцию с текущими настройками |

### Микроскоп / оптика

| Свойство / Функция | Описание |
|---------------------|-------------|
| `#.AccVol` | Ускоряющее напряжение (kV) |
| `$.Thickness` | Толщина образца (nm) |
| `$.Defocus` | Дефокусировка (nm) |
| `$.Cs` | Сферическая аберрация Cs (mm) |
| `$.Cc` | Хроматическая аберрация Cc (mm) |
| `$.DeltaV` | Разброс энергии ΔV, FWHM (eV) |
| `$.Scherzer` | Дефокусировка Шерцера (nm, только чтение) |
| `STEM.ConvergenceAngle` | Полуугол сходимости (mrad) |
| `STEM.DetectorInnerAngle` / `STEM.DetectorOuterAngle` | Внутренний/внешний полуугол кольцевого детектора (mrad) |
| `STEM.EffectiveSourceSize` | Эффективный размер источника, FWHM (pm) |
| `HRTEM.Beta` | Полуугол освещения β (радианы) |
| `HRTEM.ApertureSemiangle` | Полуугол апертуры объектива (радианы) |
| `HRTEM.ApertureShiftX` / `HRTEM.ApertureShiftY` | Смещение апертуры объектива (радианы) |
| `HRTEM.OpenAperture` | Апертура объектива открыта (true/false) |

### Свойства симуляции

| Свойство / Функция | Описание |
|---------------------|-------------|
| `#.NumberOfDiffractedWaves` | Максимальное число дифрагированных (блоховских) волн |
| `#.ImageWidth` / `#.ImageHeight` | Размер изображения (пиксели) |
| `#.ImageSize(width, height)` | Задать размер изображения (пиксели) |
| `#.ImageResolution` | Разрешение изображения (nm/пиксель) |
| `STEM.AngularResolution` | Угловое разрешение сходящегося пучка (mrad) |
| `STEM.SliceThickness` | Толщина слоя для расчёта TDS (nm) |
| `HRTEM.Mode_LinearImage()` | Использовать модель линейного изображения (квазикогерентную) |
| `HRTEM.Mode_TCC()` | Использовать модель TCC (transmission cross coefficient) |

### Режим одиночного / серийного изображения

| Свойство / Функция | Описание |
|---------------------|-------------|
| `$.SingleImageMode()` | Переключиться в режим одиночного изображения |
| `$.SerialImageMode(withThickness, withDefocus)` | Переключиться в режим серии изображений |
| `$.SerialImageThicknessStart` / `Step` / `Num` | Серия по толщине: начало (nm) / шаг (nm) / количество |
| `$.SerialImageDefocusStart` / `Step` / `Num` | Серия по дефокусировке: начало (nm) / шаг (nm) / количество |

### Свойства изображения

| Свойство / Функция | Описание |
|---------------------|-------------|
| `#.UnitCellVisible` | Показать элементарную ячейку (true/false) |
| `#.LabelVisible` | Показать подпись изображения (true/false) |
| `#.LabelSize` | Размер шрифта подписи |
| `#.ScaleBarVisible` | Показать шкалу масштаба (true/false) |
| `#.ScaleBarLength` | Длина шкалы масштаба (nm) |
| `#.GaussianBlurEnabled` | Применить гауссово размытие (true/false) |
| `#.GaussianBlurFWHM` | FWHM гауссова размытия (pm) |
| `STEM.DisplayBoth()` | Показать и упругую, и TDS-компоненту |
| `STEM.DisplayElastic()` | Показать только упругую компоненту |
| `STEM.DisplayTDS()` | Показать только TDS-(неупругую) компоненту |

### Сохранение изображения

| Свойство / Функция | Описание |
|---------------------|-------------|
| `#.SaveImageAsPng(filename)` | Сохранить как PNG (диалог, если filename опущен) |
| `#.SaveImageAsTif(filename)` | Сохранить как TIFF (диалог, если filename опущен) |
| `#.SaveImageAsEmf(filename)` | Сохранить как метафайл EMF (диалог, если filename опущен) |
| `#.SaveIndividually` | В серийном режиме сохранять каждое изображение отдельно (true/false) |
| `#.OverprintSymbols` | Наносить элементарную ячейку / подписи / шкалу масштаба на сохраняемые изображения (true/false) |

---

## Глобальные функции

| Функция | Описание |
|----------|-------------|
| `Sleep(ms)` | Ожидать указанное число миллисекунд |

---

## См. также

- [20. Макрос](index.md)
- [20.2. Примеры](2-examples.md)
