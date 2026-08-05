# Funzioni integrate

Riferimento completo delle classi e delle funzioni disponibili nelle macro di ReciPro.

---

## Classe File

| Funzione | Descrizione |
|----------|-------------|
| `File.GetDirectoryPath(filename)` | Mostra la finestra di selezione cartella, restituisce il percorso selezionato; passando `filename` restituisce invece la cartella che lo contiene |
| `File.GetFileName()` | Mostra la finestra di selezione file, restituisce il percorso selezionato |
| `File.GetFileNames()` | Mostra la finestra di selezione multipla di file, restituisce l'elenco dei percorsi |
| `File.ReadCrystalList(filename)` | Carica un file di elenco cristalli (*.xml); omettendo `filename` si apre una finestra di dialogo |
| `File.ReadCrystal(filename)` | Carica un file di cristallo CIF/AMC; omettendo `filename` si apre una finestra di dialogo |
| `File.ExportAsCIF(filename)` | Esporta il cristallo corrente come CIF; omettendo `filename` si apre una finestra di dialogo |
| `File.ReadText(filename)` | Leggere un file di testo come UTF-8 e restituirlo come stringa; omettendo `filename` si apre una finestra di dialogo. Da abbinare a `Crystal.LoadCifText()` / `SaveText()` |
| `File.SaveText(textData, filename)` | Salva dati di testo in un file; scrive `textData` in UTF-8 e, omettendo `filename`, apre una finestra di salvataggio |

---

## Classe Crystal

Legge il cristallo selezionato e, tramite una bozza pendente, crea e modifica cristalli.

### Lettura

| Proprietà / Funzione | Descrizione |
|---|---|
| `Crystal.Name` | Nome del cristallo |
| `Crystal.ChemicalFormula` | Formula chimica |
| `Crystal.Density` | Densità (g/cm³) |
| `Crystal.GetCellInAng()` | Costanti di cella come `[a, b, c, alpha, beta, gamma]` (Å, gradi) |
| `Crystal.SpaceGroupName` | Simbolo Hermann–Mauguin del gruppo spaziale, con il suffisso di impostazione (`:2`, `:H`, …) dove applicabile |
| `Crystal.SpaceGroupNumber` | Numero del gruppo spaziale delle International Tables (1–230) |
| `Crystal.HasPending` | Se è aperta una bozza |

### Creazione e modifica (bozza → Commit)

Un cristallo si costruisce in una **bozza pendente**: la si avvia, la si riempie con i setter, e `Commit()` valida tutto, costruisce il cristallo e lo applica come cristallo corrente in un solo passo (la GUI e tutti i simulatori aperti si aggiornano, come al caricamento di un file CIF). Un `Commit()` fallito riporta tutti gli errori di validazione insieme, non cambia nulla e conserva la bozza, che può quindi essere corretta e ricommittata.

| Funzione | Descrizione |
|---|---|
| `Crystal.BeginCreate(name)` | Avviare una bozza per un nuovo cristallo |
| `Crystal.BeginEdit()` | Avviare una bozza dal cristallo corrente (cella, gruppo spaziale, atomi e orientazione vengono ereditati) |
| `Crystal.LoadCifText(cifText)` | Avviare una bozza da testo CIF (il contenuto di un file .cif, non un percorso) |
| `Crystal.SetName(name)` | Rinominare la bozza |
| `Crystal.SetCellInAng(a, b, c, alpha, beta, gamma)` | Costanti di cella in **Å e gradi**. Ogni chiamata sostituisce l'intera cella; gli argomenti omessi sono derivati dai vincoli del gruppo spaziale (per un cristallo cubico basta `a`), e i valori espliciti che li contraddicono generano un errore |
| `Crystal.SetSpaceGroup(symbol)` | Gruppo spaziale per simbolo (HM breve/completo o Hall; spazi e `_` ignorati). Aggiungete l'impostazione (`'Fd-3m:2'`, `'R-3c:H'`, `'P21/c:b1'`) quando il gruppo ne ha più d'una — i simboli ambigui generano un errore che elenca i candidati |
| `Crystal.SetSpaceGroupByNumber(itNumber, setting)` | Gruppo spaziale per numero IT (1–230); `setting` (`'1'`, `'2'`, `'H'`, `'R'`, `'b1'`, …) sceglie tra più impostazioni |
| `Crystal.AddAtom(label, element, x, y, z, occ, bIso)` | Aggiungere un atomo dell'unità asimmetrica: simbolo dell'elemento, coordinate frazionarie, occupazione (0 < occ ≤ 1, default 1) e B isotropo in Å² (default 0). Posizioni equivalenti, lettere di Wyckoff e molteplicità sono derivate automaticamente |
| `Crystal.ClearAtoms()` | Rimuovere tutti gli atomi dalla bozza |
| `Crystal.Commit()` | Validare, costruire e applicare la bozza |
| `Crystal.Cancel()` | Scartare la bozza |

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

Dopo un `Commit()` riuscito, il `BeginEdit()` successivo parte dal cristallo **aggiornato**: le modifiche si accumulano — per scansioni in valori assoluti, leggete i valori di base prima del ciclo, come sopra. Per registrare il cristallo nell'elenco dei cristalli, chiamate `CrystalList.Add()`.

---

## Classe CrystalList

| Funzione / Proprietà | Descrizione |
|---------------------|-------------|
| `CrystalList.SelectedIndex` | Ottieni/imposta l'indice del cristallo selezionato |
| `CrystalList.Count` | Numero di cristalli presenti nell'elenco |
| `CrystalList.Add()` | Aggiungi il cristallo corrente all'elenco |
| `CrystalList.Replace()` | Sostituisci il cristallo selezionato |
| `CrystalList.Delete()` | Elimina il cristallo selezionato |
| `CrystalList.ClearAll()` | Svuota tutti i cristalli |
| `CrystalList.MoveUp()` | Sposta il cristallo selezionato in alto |
| `CrystalList.MoveDown()` | Sposta il cristallo selezionato in basso |

---

## Classe Dir

| Funzione | Descrizione |
|----------|-------------|
| `Dir.Euler(phi, theta, psi)` | Imposta l'orientazione tramite angoli di Eulero (radianti) |
| `Dir.EulerInDegree(phi, theta, psi)` | Imposta l'orientazione tramite angoli di Eulero (gradi) |
| `Dir.EulerInDeg(phi, theta, psi)` | Alias di `EulerInDegree` |
| `Dir.Rotate(ax, ay, az, angle)` | Ruota attorno a un asse arbitrario (radianti) |
| `Dir.RotateInDeg(ax, ay, az, angle)` | Ruota attorno a un asse arbitrario (gradi) |
| `Dir.RotateAroundAxis(u, v, w, angle)` | Ruota attorno all'asse di zona [uvw] (radianti) |
| `Dir.RotateAroundAxisInDeg(u, v, w, angle)` | Ruota attorno all'asse di zona [uvw] (gradi) |
| `Dir.RotateAroundPlane(h, k, l, angle)` | Ruota attorno alla normale al piano (hkl) (radianti) |
| `Dir.RotateAroundPlaneInDeg(h, k, l, angle)` | Ruota attorno alla normale al piano (hkl) (gradi) |
| `Dir.ProjectAlongPlane(h, k, l)` | Imposta la normale al piano perpendicolare allo schermo |
| `Dir.ProjectAlongAxis(u, v, w)` | Imposta l'asse di zona perpendicolare allo schermo |
| `Dir.GetEuler()` | Ottieni l'orientazione corrente come angoli di Eulero Z-X-Z `[phi, theta, psi]` (radianti) |
| `Dir.GetEulerInDeg()` | Ottieni l'orientazione corrente come angoli di Eulero Z-X-Z `[phi, theta, psi]` (gradi) |
| `Dir.GetRotationMatrix()` | Ottieni la matrice di rotazione corrente come array di nove elementi `[R11, R12, R13, R21, R22, R23, R31, R32, R33]` — la stessa convenzione di `SpotID.CandidateList()` |
| `Dir.SetRotationMatrix(r11, r12, r13, r21, r22, r23, r31, r32, r33)` | Imposta l'orientazione da nove elementi della matrice di rotazione (validati e riortonormalizzati prima dell'applicazione) |

Gli angoli di Eulero non sono unici nelle posizioni di blocco cardanico (θ = 0 o 180°): `GetEuler()` dopo `Euler()` riproduce lo stesso assetto, ma non necessariamente gli stessi numeri. Per salvare e ripristinare esattamente l'orientazione, usa `Dir.GetRotationMatrix()` / `Dir.SetRotationMatrix()`. La convenzione completa è descritta in [Geometria di rotazione](../4-rotation-geometry.md).

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
| `SaveAsPng(filename)` | Salva il pattern corrente come PNG; omettendo `filename` si apre una finestra di dialogo |
| `SpotInfo()` | Ottieni i dati degli spot come stringa CSV |

---

## Classe SpotID

Pilota [Spot ID v2](../11-spot-id-v2.md) da una macro: caricare un'immagine o un elenco di spot, rilevare gli spot, cercare le orientazioni e rileggere i candidati, senza toccare la finestra. `FindSpots()` e `Identify()` tornano solo a lavoro concluso, quindi si possono concatenare direttamente.

### Controllo della finestra

`SpotID.Open()` / `SpotID.Close()`

### Sorgente dell'onda

`SpotID.Source_Xray()` / `SpotID.Source_Electron()` / `SpotID.Source_Neutron()`

### Flusso di lavoro

| Funzione | Descrizione |
|----------|-------------|
| `SpotID.LoadFile(filename)` | Caricare un file come fa **File > Load**: un `.csv` viene letto come elenco di spot (occorre aver caricato prima un'immagine), qualsiasi altra estensione come immagine di figura di diffrazione (dm3, dm4, mrc, ipa, tif e altri formati supportati). Omettendo `filename` si apre una finestra di selezione |
| `SpotID.FindSpots()` | Rilevare gli spot nell'immagine caricata e adattarli, come fa il pulsante **Find spots** |
| `SpotID.Identify()` | Cercare le orientazioni che spiegano gli spot rilevati, come fa il pulsante **Identify spots**, e restituire il numero di candidati. I cristalli provati sono quelli selezionati nell'elenco cristalli della finestra principale |
| `SpotID.CandidateList()` | Restituire l'elenco delle orientazioni candidate come testo CSV |
| `SpotID.SpotList()` | Restituire gli spot osservati come testo CSV, con le stesse colonne di **File > Save**. Insieme a `File.SaveText()` produce un file che `LoadFile()` sa rileggere |

`CandidateList()` fornisce, per ciascun candidato: nome del cristallo, gli angoli di Eulero Z-X-Z (gradi), i nove elementi R11–R33 della matrice di rotazione (dal riferimento del cristallo a quello del laboratorio, applicata a vettori colonna), il residuo quadratico medio (nm⁻²) e l'assegnazione degli spot osservati agli indici *hkl*. I candidati sono ordinati per numero di spot assegnati (decrescente) e poi per residuo (crescente). I numeri sono scritti in invariant culture, quindi il separatore decimale è sempre il punto.

### Proprietà

| Proprietà | Tipo | Descrizione |
|-----------|------|-------------|
| `Energy` | double | Energia del fascio (keV per raggi X ed elettroni, meV per neutroni) |
| `CameraLength` | double | Lunghezza di camera (mm) |
| `PixelSizeInMM` | double | Dimensione del pixel (mm); leggerla o scriverla porta anche l'unità della dimensione del pixel a mm |
| `PixelSizeInNMinv` | double | Dimensione del pixel (nm⁻¹); leggerla o scriverla porta anche l'unità a nm⁻¹ |
| `MaxNumberOfSpots` | int | Numero massimo di spot che `FindSpots()` può rilevare |
| `NearestNeighbor` | int | Distanza minima consentita tra spot rilevati (pixel) |
| `FittingRange` | double | Raggio della regione attorno a ogni spot usata per il fit del picco (pixel) |
| `AcceptableError` | double | Tolleranza della differenza relativa di distanza *d* nell'associare spot e riflessi (%) |
| `IgnoreProhibitedReflections` | bool | Ignorare i riflessi cinematicamente proibiti, che possono comunque comparire per diffrazione multipla |
| `MultiGrain` | bool | Cercare più grani; `False` significa grano singolo |
| `MaxNumberOfGrains` | int | Numero massimo di orientazioni di grano cercate quando `MultiGrain` è `True` |
| `NumberOfDetectedSpots` | int | Numero di spot rilevati (sola lettura) |
| `NumberOfCandidates` | int | Numero di candidati trovati dall'ultimo `Identify()` (sola lettura) |

---

## Classe StructureViewer

Pilota il visualizzatore di struttura da una macro. `SaveImage()` ed `Export3DModel()` aprono prima la finestra se necessario, perché il modello 3D viene costruito quando la finestra è mostrata.

| Funzione | Descrizione |
|---|---|
| `StructureViewer.Open()` | Aprire la finestra del visualizzatore di struttura |
| `StructureViewer.Close()` | Chiudere la finestra del visualizzatore di struttura |
| `StructureViewer.SaveImage(filename)` | Salvare la vista principale renderizzata come PNG, alla dimensione in pixel del campo **Size (W×H)**; omettendo `filename` si apre una finestra di dialogo |
| `StructureViewer.Export3DModel(filename, maxSizeInMM, fixedScaleInMMperNm, includeAtoms, includeBonds, includePolyhedra, polyhedraAsEdges, polyEdgeDiaInMM, includeCellEdges, cellEdgeDiaInMM, thickenBondsToMM)` | Esportare la struttura visualizzata per la stampa 3D, come **Export 3D Model (3MF/STL)** del menu File. L'estensione decide il formato (`.stl` monocolore / `.3mf` colorato per elemento); solo `filename` è obbligatorio — gli altri valori predefiniti sono quelli della finestra di dialogo (dimensione massima 80 mm, spigoli della cella ⌀2,4 mm, legami ispessiti a ⌀1,2 mm). Con `fixedScaleInMMperNm` > 0 si costruiscono più modelli alla stessa scala |

```python
ReciPro.StructureViewer.Export3DModel('D:/print/NaCl_60mm.stl', maxSizeInMM=60)
ReciPro.StructureViewer.Export3DModel('D:/print/NaCl_edges.stl', maxSizeInMM=60, polyhedraAsEdges=True)
```

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
