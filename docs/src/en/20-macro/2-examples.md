# Macro Examples

Practical examples for ReciPro macros. The first few are beginner-friendly introductions; later examples show batch operations.

> **Note**: The ReciPro macro API is accessed as `ReciPro.ClassName.Member`. The autocomplete popup always inserts the full `ReciPro.` prefix, so you rarely need to type it by hand.

---

## Beginner examples

### A. Basic loop (no side effects)

The easiest way to learn the editor. Run this with **Step by step** and watch `i` and `sq` change in the debug panel — this is the ReciPro way of "printing" values (`print()` does not work).

```python
# Loop 10 times and compute the squares.
for i in range(10):
    sq = i * i
```

### B. Using the math module

The `math` module is imported automatically at startup. Use it directly.

```python
r = 5.0
area = math.pi * r * r
circumference = 2 * math.pi * r
# Run in Step mode to see 'area' and 'circumference' in the debug panel.
```

### C. Rotate the current crystal

```python
# Rotate the current crystal by 30 degrees around the a-axis (x-axis).
ReciPro.Dir.RotateAroundAxisInDeg(1, 0, 0, 30)
```

### D. Align to a zone axis

```python
# Align so that the [001] zone axis is normal to the screen.
ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
```

### E. Loop through the first few crystals

```python
# Collect the names of the first 5 crystals in the list.
names = []
for i in range(5):
    ReciPro.CrystalList.SelectedIndex = i
    names.append(ReciPro.Crystal.Name)
# Run in Step mode to see 'names' grow line by line.
```

### F. Open a diffraction pattern

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

## Batch examples

### 1. Save diffraction patterns for all crystals

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

### 2. Rotate and capture snapshots

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

### 3. Set Euler angles

```python
# Euler angles in degrees
ReciPro.Dir.EulerInDeg(45, 30, 60)

# Same thing in radians (math is pre-imported)
ReciPro.Dir.Euler(math.pi/4, math.pi/6, math.pi/3)
```

### 4. Project along planes and axes

```python
ReciPro.Dir.ProjectAlongPlane(1, 1, 1)  # (111) normal → screen
ReciPro.Dir.ProjectAlongAxis(1, 1, 0)   # [110] → screen
```

### 5. Batch-import CIF files

```python
files = ReciPro.File.GetFileNames()
for f in files:
    ReciPro.File.ReadCrystal(f)
    ReciPro.CrystalList.Add()
```

### 6. Export spot information

```python
ReciPro.DifSim.Open()
ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
info = ReciPro.DifSim.SpotInfo()
ReciPro.File.SaveText(info, "spot_info.csv")
```

---

## Tips

- **Inspecting values**: `print()` does not work in this environment (no console). Use **Step by step** execution and watch the debug panel, which lists the local variables at each line.
- **`math` is pre-imported**: `math.sqrt(x)`, `math.sin(x)`, `math.pi`, `math.radians(deg)` are available without `import math`.
- **Batch speedup**: set `ReciPro.DifSim.SkipRendering = True` during tight loops and reset to `False` afterwards.
- **Waiting for rendering**: call `ReciPro.Sleep(ms)` to pause execution when you need the GUI to catch up.
- **Autocomplete**: the popup shows the full `ReciPro.Class.Member` form. Type a few letters and accept with `Enter` or `Tab`.

---

## See also

- [20. Macro](index.md)
- [20.1. Built-in functions](1-built-in-functions.md)
