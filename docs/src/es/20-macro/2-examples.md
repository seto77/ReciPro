# Ejemplos de macros

Ejemplos prácticos para las macros de ReciPro. Los primeros son introducciones aptas para principiantes; los ejemplos posteriores muestran operaciones por lotes.

> **Nota**: La API de macros de ReciPro se accede como `ReciPro.ClassName.Member`. El menú emergente de autocompletado siempre inserta el prefijo completo `ReciPro.`, por lo que rara vez necesita escribirlo a mano.

---

## Ejemplos para principiantes

### A. Bucle básico

La forma más sencilla de aprender el editor. Ejecute esto con **Step by step** y observe cómo cambian `i` y `sq` en el panel de depuración; esta es la manera de "imprimir" valores en ReciPro (`print()` no funciona).

```python
# Loop 10 times and compute the squares.
for i in range(10):
    sq = i * i
```

### B. Usar el módulo math

El módulo `math` se importa automáticamente al iniciar. Úselo directamente.

```python
r = 5.0
area = math.pi * r * r
circumference = 2 * math.pi * r
# Run in Step mode to see 'area' and 'circumference' in the debug panel.
```

### C. Rotar el cristal actual

```python
# Rotate the current crystal by 30 degrees around the a-axis (x-axis).
ReciPro.Dir.RotateAroundAxisInDeg(1, 0, 0, 30)
```

### D. Alinear con un eje de zona

```python
# Align so that the [001] zone axis is normal to the screen.
ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
```

### E. Iterar sobre los primeros cristales

```python
# Collect the names of the first 5 crystals in the list.
names = []
for i in range(5):
    ReciPro.CrystalList.SelectedIndex = i
    names.append(ReciPro.Crystal.Name)
# Run in Step mode to see 'names' grow line by line.
```

### F. Abrir un patrón de difracción

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

## Ejemplos por lotes

### 1. Guardar los patrones de difracción de todos los cristales

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

### 2. Rotar y capturar instantáneas

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

### 3. Establecer los ángulos de Euler

```python
# Euler angles in degrees
ReciPro.Dir.EulerInDeg(45, 30, 60)

# Same thing in radians (math is pre-imported)
ReciPro.Dir.Euler(math.pi/4, math.pi/6, math.pi/3)
```

### 4. Proyectar a lo largo de planos y ejes

```python
ReciPro.Dir.ProjectAlongPlane(1, 1, 1)  # (111) normal → screen
ReciPro.Dir.ProjectAlongAxis(1, 1, 0)   # [110] → screen
```

### 5. Importar archivos CIF por lotes

```python
files = ReciPro.File.GetFileNames()
for f in files:
    ReciPro.File.ReadCrystal(f)
    ReciPro.CrystalList.Add()
```

### 6. Exportar información de reflexiones

```python
ReciPro.DifSim.Open()
ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
info = ReciPro.DifSim.SpotInfo()
ReciPro.File.SaveText(info, "spot_info.csv")
```

---

## Consejos

- **Inspeccionar valores**: `print()` no funciona en este entorno (no hay consola). Use la ejecución **Step by step** y observe el panel de depuración, que enumera las variables locales en cada línea.
- **`math` está preimportado**: `math.sqrt(x)`, `math.sin(x)`, `math.pi`, `math.radians(deg)` están disponibles sin `import math`.
- **Aceleración por lotes**: establezca `ReciPro.DifSim.SkipRendering = True` durante bucles intensivos y restablézcalo a `False` después.
- **Esperar al renderizado**: llame a `ReciPro.Sleep(ms)` para pausar la ejecución cuando necesite que la GUI se ponga al día.
- **Autocompletado**: el menú emergente muestra la forma completa `ReciPro.Class.Member`. Escriba unas pocas letras y confirme con `Enter` o `Tab`.

---

## Véase también

- [20. Macro](index.md)
- [20.1. Funciones integradas](1-built-in-functions.md)
