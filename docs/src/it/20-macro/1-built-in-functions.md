# Funzioni integrate

Riferimento completo delle classi e delle funzioni disponibili nelle macro di ReciPro.

---

## Classe File

| Funzione | Descrizione |
|----------|-------------|
| `File.GetDirectoryPath()` | Mostra la finestra di selezione cartella, restituisce il percorso selezionato |
| `File.GetFileName()` | Mostra la finestra di selezione file, restituisce il percorso selezionato |
| `File.GetFileNames()` | Mostra la finestra di selezione multipla di file, restituisce l'elenco dei percorsi |
| `File.ReadCrystalList()` | Carica un file di elenco cristalli (*.xml) |
| `File.ReadCrystal()` | Carica un file di cristallo CIF/AMC |
| `File.ExportAsCIF()` | Esporta il cristallo corrente come CIF |
| `File.SaveText()` | Salva dati di testo in un file |

---

## Classe Crystal

| Proprietà | Tipo | Descrizione |
|----------|------|-------------|
| `Crystal.Name` | string | Nome del cristallo |
| `Crystal.ChemicalFormula` | string | Formula chimica |
| `Crystal.Density` | double | Densità (g/cm³) |

---

## Classe CrystalList

| Funzione / Proprietà | Descrizione |
|---------------------|-------------|
| `CrystalList.SelectedIndex` | Ottieni/imposta l'indice del cristallo selezionato |
| `CrystalList.Add()` | Aggiungi il cristallo corrente all'elenco |
| `CrystalList.Replace()` | Sostituisci il cristallo selezionato |
| `CrystalList.Delete()` | Elimina il cristallo selezionato |
| `CrystalList.ClearAll()` | Svuota tutti i cristalli |
| `CrystalList.MoveUp()` | Sposta il cristallo selezionato in alto |
| `CrystalList.MoveDown()` | Sposta il cristallo selezionato in basso |

---

## Classe Direction

| Funzione | Descrizione |
|----------|-------------|
| `Direction.Euler(phi, theta, psi)` | Imposta l'orientazione tramite angoli di Eulero (radianti) |
| `Direction.EulerInDegree(phi, theta, psi)` | Imposta l'orientazione tramite angoli di Eulero (gradi) |
| `Direction.EulerInDeg(phi, theta, psi)` | Alias di `EulerInDegree` |
| `Direction.Rotate(ax, ay, az, angle)` | Ruota attorno a un asse arbitrario (radianti) |
| `Direction.RotateInDeg(ax, ay, az, angle)` | Ruota attorno a un asse arbitrario (gradi) |
| `Direction.RotateAroundAxis(u, v, w, angle)` | Ruota attorno all'asse di zona [uvw] (radianti) |
| `Direction.RotateAroundAxisInDeg(u, v, w, angle)` | Ruota attorno all'asse di zona [uvw] (gradi) |
| `Direction.RotateAroundPlane(h, k, l, angle)` | Ruota attorno alla normale al piano (hkl) (radianti) |
| `Direction.RotateAroundPlaneInDeg(h, k, l, angle)` | Ruota attorno alla normale al piano (hkl) (gradi) |
| `Direction.ProjectAlongPlane(h, k, l)` | Imposta la normale al piano perpendicolare allo schermo |
| `Direction.ProjectAlongAxis(u, v, w)` | Imposta l'asse di zona perpendicolare allo schermo |

---

## Classe DifSim

### Controllo finestra

`DifSim.Open()` / `DifSim.Close()`

### Sorgente d'onda

`DifSim.Source_Xray()` / `DifSim.Source_Electron()` / `DifSim.Source_Neutron()`

### Proprietà

| Proprietà | Tipo | Descrizione |
|----------|------|-------------|
| `Energy` | double | Energia (keV) |
| `Wavelength` | double | Lunghezza d'onda (Å) |
| `Thickness` | double | Spessore del campione (nm) |
| `NumberOfDiffractedWaves` | int | Numero di onde di Bloch |
| `CameraLength2` | double | Lunghezza di camera (mm) |
| `SkipRendering` | bool | Salta il rendering per l'elaborazione in batch |

### Modalità del fascio

`Beam_Parallel()` / `Beam_PrecessionXray()` / `Beam_PrecessionElectron()` / `Beam_Convergence()`

### Modalità di calcolo

`Calc_Excitation()` / `Calc_Kinematical()` / `Calc_Dynamical()`

### Impostazioni dell'immagine

| Proprietà / Funzione | Descrizione |
|---------------------|-------------|
| `ImageResolutionInMM` | Risoluzione (mm/pixel) |
| `ImageResolutionInNMinv` | Risoluzione (nm⁻¹/pixel) |
| `ImageWidth` / `ImageHeight` | Dimensione dell'immagine (pixel) |
| `ImageSize(w, h)` | Imposta la dimensione dell'immagine |

### Rivelatore

| Proprietà | Descrizione |
|----------|-------------|
| `Tau` / `TauInDeg` | Angolo di inclinazione del rivelatore τ (rad / gradi) |
| `Phi` / `PhiInDeg` | Asse di rotazione del rivelatore φ (rad / gradi) |
| `Foot(x, y)` | Posizione del foot in pixel |

### Output

| Funzione | Descrizione |
|----------|-------------|
| `SaveAsPng()` | Salva il pattern corrente come PNG |
| `SpotInfo()` | Ottieni i dati degli spot come stringa CSV |

---

## Classi HRTEM / STEM / Potential

Queste tre classi di simulazione delle immagini condividono molti membri. Per evitare ripetizioni, le tabelle seguenti usano dei segnaposto:

- **`#`** : comune a **HRTEM**, **STEM** e **Potential**. Sostituisci `#` con `HRTEM`, `STEM` o `Potential` (ad es. `STEM.Simulate()`, `Potential.AccVol`).
- **`$`** : comune solo a **HRTEM** e **STEM**. Sostituisci `$` con `HRTEM` o `STEM`.
- I membri scritti con un nome di classe esplicito (`STEM.…` / `HRTEM.…`) appartengono solo a quella classe. La classe **Potential** non aggiunge membri propri; usa solo i membri `#`.

### Controllo finestra

| Funzione | Descrizione |
|----------|-------------|
| `#.Open()` | Apre la finestra del Simulatore HRTEM/STEM |
| `#.Close()` | Chiude la finestra del Simulatore HRTEM/STEM |
| `#.Simulate()` | Esegue la simulazione con le impostazioni correnti |

### Microscopio / ottica

| Proprietà / Funzione | Descrizione |
|---------------------|-------------|
| `#.AccVol` | Tensione di accelerazione (kV) |
| `$.Thickness` | Spessore del campione (nm) |
| `$.Defocus` | Defocalizzazione (nm) |
| `$.Cs` | Aberrazione sferica Cs (mm) |
| `$.Cc` | Aberrazione cromatica Cc (mm) |
| `$.DeltaV` | Dispersione energetica ΔV, FWHM (eV) |
| `$.Scherzer` | Defocalizzazione di Scherzer (nm, sola lettura) |
| `STEM.ConvergenceAngle` | Semiangolo di convergenza (mrad) |
| `STEM.DetectorInnerAngle` / `STEM.DetectorOuterAngle` | Semiangolo interno/esterno del rivelatore anulare (mrad) |
| `STEM.EffectiveSourceSize` | Dimensione effettiva della sorgente, FWHM (pm) |
| `HRTEM.Beta` | Semiangolo di illuminazione β (radianti) |
| `HRTEM.ApertureSemiangle` | Semiangolo dell'apertura obiettivo (radianti) |
| `HRTEM.ApertureShiftX` / `HRTEM.ApertureShiftY` | Spostamento dell'apertura obiettivo (radianti) |
| `HRTEM.OpenAperture` | Apertura obiettivo aperta (true/false) |

### Proprietà di simulazione

| Proprietà / Funzione | Descrizione |
|---------------------|-------------|
| `#.NumberOfDiffractedWaves` | Numero massimo di onde diffratte (di Bloch) |
| `#.ImageWidth` / `#.ImageHeight` | Dimensione dell'immagine (pixel) |
| `#.ImageSize(width, height)` | Imposta la dimensione dell'immagine (pixel) |
| `#.ImageResolution` | Risoluzione dell'immagine (nm/pixel) |
| `STEM.AngularResolution` | Risoluzione angolare del fascio convergente (mrad) |
| `STEM.SliceThickness` | Spessore della fetta per il calcolo TDS (nm) |
| `HRTEM.Mode_LinearImage()` | Usa il modello a immagine lineare (quasi coerente) |
| `HRTEM.Mode_TCC()` | Usa il modello TCC (transmission cross coefficient) |

### Modalità immagine singola / seriale

| Proprietà / Funzione | Descrizione |
|---------------------|-------------|
| `$.SingleImageMode()` | Passa alla modalità a immagine singola |
| `$.SerialImageMode(withThickness, withDefocus)` | Passa alla modalità a immagini seriali |
| `$.SerialImageThicknessStart` / `Step` / `Num` | Spessore seriale: inizio (nm) / passo (nm) / conteggio |
| `$.SerialImageDefocusStart` / `Step` / `Num` | Defocalizzazione seriale: inizio (nm) / passo (nm) / conteggio |

### Proprietà dell'immagine

| Proprietà / Funzione | Descrizione |
|---------------------|-------------|
| `#.UnitCellVisible` | Mostra la cella elementare (true/false) |
| `#.LabelVisible` | Mostra l'etichetta dell'immagine (true/false) |
| `#.LabelSize` | Dimensione del carattere dell'etichetta |
| `#.ScaleBarVisible` | Mostra la barra di scala (true/false) |
| `#.ScaleBarLength` | Lunghezza della barra di scala (nm) |
| `#.GaussianBlurEnabled` | Applica la sfocatura gaussiana (true/false) |
| `#.GaussianBlurFWHM` | FWHM della sfocatura gaussiana (pm) |
| `STEM.DisplayBoth()` | Mostra sia la componente elastica sia quella TDS |
| `STEM.DisplayElastic()` | Mostra solo la componente elastica |
| `STEM.DisplayTDS()` | Mostra solo la componente TDS (anelastica) |

### Salva immagine

| Proprietà / Funzione | Descrizione |
|---------------------|-------------|
| `#.SaveImageAsPng(filename)` | Salva come PNG (finestra di dialogo se filename è omesso) |
| `#.SaveImageAsTif(filename)` | Salva come TIFF (finestra di dialogo se filename è omesso) |
| `#.SaveImageAsEmf(filename)` | Salva come metafile EMF (finestra di dialogo se filename è omesso) |
| `#.SaveIndividually` | In modalità seriale, salva ogni immagine separatamente (true/false) |
| `#.OverprintSymbols` | Sovrastampa cella elementare / etichette / barra di scala sulle immagini salvate (true/false) |

---

## Funzioni globali

| Funzione | Descrizione |
|----------|-------------|
| `Sleep(ms)` | Attendi il numero di millisecondi specificato |

---

## Vedi anche

- [20. Macro](index.md)
- [20.2. Esempi](2-examples.md)
