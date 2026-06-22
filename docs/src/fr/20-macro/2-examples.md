# Exemples de macros

Exemples pratiques pour les macros ReciPro. Les premiers sont des introductions accessibles aux débutants ; les exemples suivants montrent des opérations par lots.

> **Note** : L'API de macro ReciPro est accessible sous la forme `ReciPro.ClassName.Member`. Le popup d'autocomplétion insère toujours le préfixe complet `ReciPro.`, de sorte que vous avez rarement besoin de le taper à la main.

---

## Exemples pour débutants

### A. Boucle de base

La façon la plus simple d'apprendre l'éditeur. Exécutez ceci avec **Step by step** et observez `i` et `sq` changer dans le panneau de débogage — c'est la manière ReciPro d'« afficher » des valeurs (`print()` ne fonctionne pas).

```python
# Loop 10 times and compute the squares.
for i in range(10):
    sq = i * i
```

### B. Utiliser le module math

Le module `math` est importé automatiquement au démarrage. Utilisez-le directement.

```python
r = 5.0
area = math.pi * r * r
circumference = 2 * math.pi * r
# Run in Step mode to see 'area' and 'circumference' in the debug panel.
```

### C. Faire tourner le cristal courant

```python
# Rotate the current crystal by 30 degrees around the a-axis (x-axis).
ReciPro.Dir.RotateAroundAxisInDeg(1, 0, 0, 30)
```

### D. Aligner sur un axe de zone

```python
# Align so that the [001] zone axis is normal to the screen.
ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
```

### E. Parcourir les premiers cristaux

```python
# Collect the names of the first 5 crystals in the list.
names = []
for i in range(5):
    ReciPro.CrystalList.SelectedIndex = i
    names.append(ReciPro.Crystal.Name)
# Run in Step mode to see 'names' grow line by line.
```

### F. Ouvrir un cliché de diffraction

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

## Exemples par lots

### 1. Enregistrer les clichés de diffraction de tous les cristaux

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

### 2. Faire tourner et capturer des instantanés

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

### 3. Définir les angles d'Euler

```python
# Euler angles in degrees
ReciPro.Dir.EulerInDeg(45, 30, 60)

# Same thing in radians (math is pre-imported)
ReciPro.Dir.Euler(math.pi/4, math.pi/6, math.pi/3)
```

### 4. Projeter le long de plans et d'axes

```python
ReciPro.Dir.ProjectAlongPlane(1, 1, 1)  # (111) normal → screen
ReciPro.Dir.ProjectAlongAxis(1, 1, 0)   # [110] → screen
```

### 5. Importer des fichiers CIF par lots

```python
files = ReciPro.File.GetFileNames()
for f in files:
    ReciPro.File.ReadCrystal(f)
    ReciPro.CrystalList.Add()
```

### 6. Exporter les informations sur les réflexions

```python
ReciPro.DifSim.Open()
ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
info = ReciPro.DifSim.SpotInfo()
ReciPro.File.SaveText(info, "spot_info.csv")
```

---

## Astuces

- **Inspecter les valeurs** : `print()` ne fonctionne pas dans cet environnement (pas de console). Utilisez l'exécution **Step by step** et observez le panneau de débogage, qui liste les variables locales à chaque ligne.
- **`math` est pré-importé** : `math.sqrt(x)`, `math.sin(x)`, `math.pi`, `math.radians(deg)` sont disponibles sans `import math`.
- **Accélération des lots** : définissez `ReciPro.DifSim.SkipRendering = True` pendant les boucles serrées et réinitialisez-le à `False` ensuite.
- **Attendre le rendu** : appelez `ReciPro.Sleep(ms)` pour suspendre l'exécution lorsque l'interface graphique doit rattraper son retard.
- **Autocomplétion** : le popup affiche la forme complète `ReciPro.Class.Member`. Tapez quelques lettres et confirmez avec `Enter` ou `Tab`.

---

## Voir aussi

- [20. Macro](index.md)
- [20.1. Fonctions intégrées](1-built-in-functions.md)
