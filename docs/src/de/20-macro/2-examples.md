# Makro-Beispiele

Praktische Beispiele für ReciPro-Makros. Die ersten paar sind einsteigerfreundliche Einführungen; spätere Beispiele zeigen Stapeloperationen.

> **Hinweis**: Die ReciPro-Makro-API wird als `ReciPro.ClassName.Member` angesprochen. Das Autovervollständigungs-Popup fügt immer das vollständige Präfix `ReciPro.` ein, sodass Sie es selten von Hand eintippen müssen.

---

## Einsteigerbeispiele

### A. Grundlegende Schleife

Der einfachste Weg, den Editor kennenzulernen. Führen Sie dies mit **Step by step** aus und beobachten Sie, wie sich `i` und `sq` im Debug-Bereich ändern — das ist die ReciPro-Art, Werte „auszugeben" (`print()` funktioniert nicht).

```python
# Loop 10 times and compute the squares.
for i in range(10):
    sq = i * i
```

### B. Das math-Modul verwenden

Das `math`-Modul wird beim Start automatisch importiert. Verwenden Sie es direkt.

```python
r = 5.0
area = math.pi * r * r
circumference = 2 * math.pi * r
# Run in Step mode to see 'area' and 'circumference' in the debug panel.
```

### C. Den aktuellen Kristall drehen

```python
# Rotate the current crystal by 30 degrees around the a-axis (x-axis).
ReciPro.Dir.RotateAroundAxisInDeg(1, 0, 0, 30)
```

### D. An einer Zonenachse ausrichten

```python
# Align so that the [001] zone axis is normal to the screen.
ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
```

### E. Über die ersten paar Kristalle iterieren

```python
# Collect the names of the first 5 crystals in the list.
names = []
for i in range(5):
    ReciPro.CrystalList.SelectedIndex = i
    names.append(ReciPro.Crystal.Name)
# Run in Step mode to see 'names' grow line by line.
```

### F. Ein Beugungsmuster öffnen

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

## Stapelbeispiele

### 1. Beugungsmuster für alle Kristalle speichern

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

### 2. Drehen und Schnappschüsse aufnehmen

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

### 3. Euler-Winkel setzen

```python
# Euler angles in degrees
ReciPro.Dir.EulerInDeg(45, 30, 60)

# Same thing in radians (math is pre-imported)
ReciPro.Dir.Euler(math.pi/4, math.pi/6, math.pi/3)
```

### 4. Entlang Ebenen und Achsen projizieren

```python
ReciPro.Dir.ProjectAlongPlane(1, 1, 1)  # (111) normal → screen
ReciPro.Dir.ProjectAlongAxis(1, 1, 0)   # [110] → screen
```

### 5. CIF-Dateien im Stapel importieren

```python
files = ReciPro.File.GetFileNames()
for f in files:
    ReciPro.File.ReadCrystal(f)
    ReciPro.CrystalList.Add()
```

### 6. Reflexinformationen exportieren

```python
ReciPro.DifSim.Open()
ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
info = ReciPro.DifSim.SpotInfo()
ReciPro.File.SaveText(info, "spot_info.csv")
```

---

## Tipps

- **Werte inspizieren**: `print()` funktioniert in dieser Umgebung nicht (keine Konsole). Verwenden Sie die Ausführung **Step by step** und beobachten Sie den Debug-Bereich, der die lokalen Variablen in jeder Zeile auflistet.
- **`math` ist vorab importiert**: `math.sqrt(x)`, `math.sin(x)`, `math.pi`, `math.radians(deg)` sind ohne `import math` verfügbar.
- **Stapel-Beschleunigung**: Setzen Sie `ReciPro.DifSim.SkipRendering = True` während enger Schleifen und setzen Sie es danach auf `False` zurück.
- **Auf das Rendering warten**: Rufen Sie `ReciPro.Sleep(ms)` auf, um die Ausführung anzuhalten, wenn die GUI nachkommen muss.
- **Autovervollständigung**: Das Popup zeigt die vollständige Form `ReciPro.Class.Member`. Tippen Sie ein paar Buchstaben und bestätigen Sie mit `Enter` oder `Tab`.

---

## Siehe auch

- [20. Makro](index.md)
- [20.1. Integrierte Funktionen](1-built-in-functions.md)
