# Esempi di macro

Esempi pratici per le macro di ReciPro. I primi sono introduzioni adatte ai principianti; gli esempi successivi mostrano operazioni in batch.

> **Nota**: l'API delle macro di ReciPro viene richiamata come `ReciPro.ClassName.Member`. Il popup di completamento automatico inserisce sempre il prefisso completo `ReciPro.`, quindi raramente è necessario digitarlo a mano.

---

## Esempi per principianti

### A. Ciclo di base

Il modo più semplice per imparare a usare l'editor. Esegui questo codice con **Step by step** e osserva come cambiano `i` e `sq` nel pannello di debug — è il modo di ReciPro di "stampare" i valori (`print()` non funziona).

```python
# Loop 10 times and compute the squares.
for i in range(10):
    sq = i * i
```

### B. Usare il modulo math

Il modulo `math` viene importato automaticamente all'avvio. Usalo direttamente.

```python
r = 5.0
area = math.pi * r * r
circumference = 2 * math.pi * r
# Run in Step mode to see 'area' and 'circumference' in the debug panel.
```

### C. Ruotare il cristallo corrente

```python
# Rotate the current crystal by 30 degrees around the a-axis (x-axis).
ReciPro.Dir.RotateAroundAxisInDeg(1, 0, 0, 30)
```

### D. Allineare a un asse di zona

```python
# Align so that the [001] zone axis is normal to the screen.
ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
```

### E. Iterare sui primi cristalli

```python
# Collect the names of the first 5 crystals in the list.
names = []
for i in range(5):
    ReciPro.CrystalList.SelectedIndex = i
    names.append(ReciPro.Crystal.Name)
# Run in Step mode to see 'names' grow line by line.
```

### F. Aprire un pattern di diffrazione

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

## Esempi in batch

### 1. Salvare i pattern di diffrazione per tutti i cristalli

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

### 2. Ruotare e catturare istantanee

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

### 3. Impostare gli angoli di Eulero

```python
# Euler angles in degrees
ReciPro.Dir.EulerInDeg(45, 30, 60)

# Same thing in radians (math is pre-imported)
ReciPro.Dir.Euler(math.pi/4, math.pi/6, math.pi/3)
```

### 4. Proiettare lungo piani e assi

```python
ReciPro.Dir.ProjectAlongPlane(1, 1, 1)  # (111) normal → screen
ReciPro.Dir.ProjectAlongAxis(1, 1, 0)   # [110] → screen
```

### 5. Importare file CIF in batch

```python
files = ReciPro.File.GetFileNames()
for f in files:
    ReciPro.File.ReadCrystal(f)
    ReciPro.CrystalList.Add()
```

### 6. Esportare le informazioni sulle riflessioni

```python
ReciPro.DifSim.Open()
ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
info = ReciPro.DifSim.SpotInfo()
ReciPro.File.SaveText(info, "spot_info.csv")
```

---

## Suggerimenti

- **Ispezionare i valori**: `print()` non funziona in questo ambiente (nessuna console). Usa l'esecuzione **Step by step** e osserva il pannello di debug, che elenca le variabili locali a ogni riga.
- **`math` è preimportato**: `math.sqrt(x)`, `math.sin(x)`, `math.pi`, `math.radians(deg)` sono disponibili senza `import math`.
- **Accelerazione in batch**: imposta `ReciPro.DifSim.SkipRendering = True` durante i cicli serrati e riportalo a `False` al termine.
- **Attendere il rendering**: chiama `ReciPro.Sleep(ms)` per mettere in pausa l'esecuzione quando la GUI deve mettersi al passo.
- **Completamento automatico**: il popup mostra la forma completa `ReciPro.Class.Member`. Digita alcune lettere e conferma con `Enter` o `Tab`.

---

## Vedi anche

- [20. Macro](index.md)
- [20.1. Funzioni integrate](1-built-in-functions.md)
