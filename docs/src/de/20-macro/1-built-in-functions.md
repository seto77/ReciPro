# Integrierte Funktionen

Vollständige Referenz der in ReciPro-Makros verfügbaren Klassen und Funktionen.

---

## File-Klasse

| Funktion | Beschreibung |
|----------|-------------|
| `File.GetDirectoryPath(filename)` | Ordnerauswahldialog anzeigen, gewählten Pfad zurückgeben; mit `filename` wird stattdessen der Ordner zurückgegeben, der diese Datei enthält |
| `File.GetFileName()` | Dateiauswahldialog anzeigen, gewählten Pfad zurückgeben |
| `File.GetFileNames()` | Mehrfachdateiauswahldialog anzeigen, Liste der Pfade zurückgeben |
| `File.ReadCrystalList(filename)` | Eine Kristalllistendatei (*.xml) laden; ohne `filename` öffnet sich ein Dialog |
| `File.ReadCrystal(filename)` | Eine CIF-/AMC-Kristalldatei laden; ohne `filename` öffnet sich ein Dialog |
| `File.ExportAsCIF(filename)` | Den aktuellen Kristall als CIF exportieren; ohne `filename` öffnet sich ein Dialog |
| `File.ReadText(filename)` | Textdatei als UTF-8 lesen und als Zeichenkette zurückgeben; ohne `filename` öffnet sich ein Dialog. Passt zu `Crystal.LoadCifText()` / `SaveText()` |
| `File.SaveText(textData, filename)` | Textdaten in eine Datei speichern; schreibt `textData` als UTF-8, ohne `filename` öffnet sich ein Speicherdialog |

---

## Crystal-Klasse

Liest den aktuell gewählten Kristall und erzeugt bzw. bearbeitet Kristalle über einen schwebenden Entwurf.

### Lesen

| Eigenschaft / Funktion | Beschreibung |
|---|---|
| `Crystal.Name` | Kristallname |
| `Crystal.ChemicalFormula` | Chemische Formel |
| `Crystal.Density` | Dichte (g/cm³) |
| `Crystal.GetCellInAng()` | Zellkonstanten als `[a, b, c, alpha, beta, gamma]` (Å, Grad) |
| `Crystal.SpaceGroupName` | Hermann-Mauguin-Symbol der Raumgruppe, ggf. mit Einstellungssuffix (`:2`, `:H`, …) |
| `Crystal.SpaceGroupNumber` | Raumgruppennummer der International Tables (1–230) |
| `Crystal.HasPending` | Ob ein Entwurf offen ist |

### Erzeugen und Bearbeiten (Entwurf → Commit)

Ein Kristall wird in einem **schwebenden Entwurf** aufgebaut: Entwurf starten, mit den Settern füllen, dann validiert `Commit()` alles, baut den Kristall und übernimmt ihn in einem Schritt als aktuellen Kristall (GUI und alle offenen Simulatoren aktualisieren sich wie beim Laden einer CIF-Datei). Ein fehlgeschlagenes `Commit()` meldet alle Validierungsfehler gesammelt, ändert nichts und behält den Entwurf, sodass er korrigiert und erneut committet werden kann.

| Funktion | Beschreibung |
|---|---|
| `Crystal.BeginCreate(name)` | Entwurf für einen neuen Kristall starten |
| `Crystal.BeginEdit()` | Entwurf vom aktuellen Kristall starten (Zelle, Raumgruppe, Atome und Orientierung werden übernommen) |
| `Crystal.LoadCifText(cifText)` | Entwurf aus CIF-Text starten (der Inhalt einer .cif-Datei, kein Pfad) |
| `Crystal.SetName(name)` | Entwurf umbenennen |
| `Crystal.SetCellInAng(a, b, c, alpha, beta, gamma)` | Zellkonstanten in **Å und Grad**. Jeder Aufruf ersetzt die ganze Zelle; ausgelassene Argumente werden aus den Raumgruppen-Constraints abgeleitet (für kubische Kristalle genügt `a`), und explizite Werte, die ihnen widersprechen, lösen einen Fehler aus |
| `Crystal.SetSpaceGroup(symbol)` | Raumgruppe per Symbol (HM kurz/voll oder Hall; Leerzeichen und `_` werden ignoriert). Bei mehreren Einstellungen die Einstellung anhängen (`'Fd-3m:2'`, `'R-3c:H'`, `'P21/c:b1'`) — mehrdeutige Symbole lösen einen Fehler mit Kandidatenliste aus |
| `Crystal.SetSpaceGroupByNumber(itNumber, setting)` | Raumgruppe per IT-Nummer (1–230); `setting` (`'1'`, `'2'`, `'H'`, `'R'`, `'b1'`, …) wählt unter mehreren Einstellungen |
| `Crystal.AddAtom(label, element, x, y, z, occ, bIso)` | Atom der asymmetrischen Einheit hinzufügen: Elementsymbol, Bruchkoordinaten, Besetzung (0 < occ ≤ 1, Standard 1) und isotropes B in Å² (Standard 0). Äquivalente Lagen, Wyckoff-Buchstaben und Multiplizitäten werden automatisch abgeleitet |
| `Crystal.ClearAtoms()` | Alle Atome aus dem Entwurf entfernen |
| `Crystal.Commit()` | Entwurf validieren, bauen und übernehmen |
| `Crystal.Cancel()` | Entwurf verwerfen |

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

Nach einem erfolgreichen `Commit()` startet das nächste `BeginEdit()` vom **aktualisierten** Kristall — Änderungen akkumulieren sich; für absolute Scans die Basiswerte wie oben vor der Schleife lesen. Um den committeten Kristall in die Kristallliste einzutragen, `CrystalList.Add()` aufrufen.

---

## CrystalList-Klasse

| Funktion / Eigenschaft | Beschreibung |
|---------------------|-------------|
| `CrystalList.SelectedIndex` | Index des gewählten Kristalls abrufen/setzen |
| `CrystalList.Count` | Anzahl der Kristalle in der Liste |
| `CrystalList.Add()` | Aktuellen Kristall an die Liste anhängen |
| `CrystalList.Replace()` | Gewählten Kristall ersetzen |
| `CrystalList.Delete()` | Gewählten Kristall löschen |
| `CrystalList.ClearAll()` | Alle Kristalle leeren |
| `CrystalList.MoveUp()` | Gewählten Kristall nach oben verschieben |
| `CrystalList.MoveDown()` | Gewählten Kristall nach unten verschieben |

---

## Dir-Klasse

| Funktion | Beschreibung |
|----------|-------------|
| `Dir.Euler(phi, theta, psi)` | Orientierung über Euler-Winkel festlegen (Bogenmaß) |
| `Dir.EulerInDegree(phi, theta, psi)` | Orientierung über Euler-Winkel festlegen (Grad) |
| `Dir.EulerInDeg(phi, theta, psi)` | Alias für `EulerInDegree` |
| `Dir.Rotate(ax, ay, az, angle)` | Um eine beliebige Achse drehen (Bogenmaß) |
| `Dir.RotateInDeg(ax, ay, az, angle)` | Um eine beliebige Achse drehen (Grad) |
| `Dir.RotateAroundAxis(u, v, w, angle)` | Um die Zonenachse [uvw] drehen (Bogenmaß) |
| `Dir.RotateAroundAxisInDeg(u, v, w, angle)` | Um die Zonenachse [uvw] drehen (Grad) |
| `Dir.RotateAroundPlane(h, k, l, angle)` | Um die Ebenennormale (hkl) drehen (Bogenmaß) |
| `Dir.RotateAroundPlaneInDeg(h, k, l, angle)` | Um die Ebenennormale (hkl) drehen (Grad) |
| `Dir.ProjectAlongPlane(h, k, l)` | Ebenennormale senkrecht zum Bildschirm setzen |
| `Dir.ProjectAlongAxis(u, v, w)` | Zonenachse senkrecht zum Bildschirm setzen |
| `Dir.GetEuler()` | Aktuelle Orientierung als Z-X-Z-Eulerwinkel `[phi, theta, psi]` (Bogenmaß) abrufen |
| `Dir.GetEulerInDeg()` | Aktuelle Orientierung als Z-X-Z-Eulerwinkel `[phi, theta, psi]` (Grad) abrufen |
| `Dir.GetRotationMatrix()` | Aktuelle Rotationsmatrix als neunelementiges Array `[R11, R12, R13, R21, R22, R23, R31, R32, R33]` abrufen — dieselbe Konvention wie `SpotID.CandidateList()` |
| `Dir.SetRotationMatrix(r11, r12, r13, r21, r22, r23, r31, r32, r33)` | Orientierung aus neun Matrixelementen setzen (vor der Anwendung geprüft und reorthonormalisiert) |

Eulerwinkel sind an Gimbal-Positionen (θ = 0 oder 180°) nicht eindeutig: `GetEuler()` nach `Euler()` reproduziert dieselbe Lage, aber nicht unbedingt dieselben Zahlen. Zum exakten Speichern und Wiederherstellen der Orientierung `Dir.GetRotationMatrix()` / `Dir.SetRotationMatrix()` verwenden. Die vollständige Konvention ist in [Rotationsgeometrie](../4-rotation-geometry.md) beschrieben.

---

## DifSim-Klasse

### Fenstersteuerung

`DifSim.Open()` / `DifSim.Close()`

### Wellenquelle

`DifSim.Source_Xray()` / `DifSim.Source_Electron()` / `DifSim.Source_Neutron()`

### Eigenschaften

| Eigenschaft | Typ | Beschreibung |
|----------|------|-------------|
| `Energy` | double | Energie (keV) |
| `Wavelength` | double | Wellenlänge (Å) |
| `Thickness` | double | Probendicke (nm) |
| `NumberOfDiffractedWaves` | int | Anzahl der Bloch-Wellen |
| `CameraLength2` | double | Kameralänge (mm) |
| `SkipRendering` | bool | Rendering für Stapelverarbeitung überspringen |

### Strahlmodus

`Beam_Parallel()` / `Beam_PrecessionXray()` / `Beam_PrecessionElectron()` / `Beam_Convergence()`

### Berechnungsmodus

`Calc_Excitation()` / `Calc_Kinematical()` / `Calc_Dynamical()`

### Bildeinstellungen

| Eigenschaft / Funktion | Beschreibung |
|---------------------|-------------|
| `ImageResolutionInMM` | Auflösung (mm/Pixel) |
| `ImageResolutionInNMinv` | Auflösung (nm⁻¹/Pixel) |
| `ImageWidth` / `ImageHeight` | Bildgröße (Pixel) |
| `ImageSize(w, h)` | Bildgröße festlegen |

### Detektor

| Eigenschaft | Beschreibung |
|----------|-------------|
| `Tau` / `TauInDeg` | Detektor-Kippwinkel τ (rad / Grad) |
| `Phi` / `PhiInDeg` | Detektor-Rotationsachse φ (rad / Grad) |
| `Foot(x, y)` | Foot-Position in Pixeln |

### Ausgabe

| Funktion | Beschreibung |
|----------|-------------|
| `SaveAsPng(filename)` | Aktuelles Muster als PNG speichern; ohne `filename` öffnet sich ein Dialog |
| `SpotInfo()` | Reflexdaten als CSV-String abrufen |

---

## SpotID-Klasse

Steuert [Spot ID v2](../11-spot-id-v2.md) aus einem Makro heraus: ein Bild oder eine Reflexliste laden, die Reflexe detektieren, nach Orientierungen suchen und die Kandidaten auslesen — ohne das Fenster anzufassen. `FindSpots()` und `Identify()` kehren erst zurück, wenn die Arbeit erledigt ist, und lassen sich daher direkt aneinanderreihen.

### Fenstersteuerung

`SpotID.Open()` / `SpotID.Close()`

### Wellenquelle

`SpotID.Source_Xray()` / `SpotID.Source_Electron()` / `SpotID.Source_Neutron()`

### Arbeitsablauf

| Funktion | Beschreibung |
|----------|--------------|
| `SpotID.LoadFile(filename)` | Eine Datei laden, wie es **File > Load** tut: `.csv` wird als Reflexliste gelesen (ein Bild muss zuvor geladen sein), jede andere Endung als Beugungsbild (dm3, dm4, mrc, ipa, tif und weitere unterstützte Formate). Ohne `filename` öffnet sich ein Dateidialog |
| `SpotID.FindSpots()` | Die Reflexe im geladenen Bild detektieren und anpassen, wie es die Schaltfläche **Find spots** tut |
| `SpotID.Identify()` | Nach Orientierungen suchen, die die detektierten Reflexe erklären, wie es die Schaltfläche **Identify spots** tut, und die Anzahl der Kandidaten zurückgeben. Geprüft werden die in der Kristallliste des Hauptfensters ausgewählten Kristalle |
| `SpotID.CandidateList()` | Die Liste der Orientierungskandidaten als CSV-Text zurückgeben |
| `SpotID.SpotList()` | Die beobachteten Reflexe als CSV-Text zurückgeben, mit denselben Spalten wie **File > Save**. Zusammen mit `File.SaveText()` entsteht eine Datei, die `LoadFile()` wieder einlesen kann |

`CandidateList()` liefert je Kandidat: Kristallname, die Z-X-Z-Eulerwinkel (Grad), die neun Elemente R11–R33 der Rotationsmatrix (Kristall- zu Laborsystem, auf Spaltenvektoren angewandt), das mittlere Residuenquadrat (nm⁻²) und die Zuordnung der beobachteten Reflexe zu *hkl*-Indizes. Die Kandidaten sind nach der Anzahl zugeordneter Reflexe (absteigend) und danach nach dem Residuum (aufsteigend) sortiert. Zahlen werden in der Invariant Culture geschrieben, das Dezimaltrennzeichen ist also immer ein Punkt.

### Eigenschaften

| Eigenschaft | Typ | Beschreibung |
|-------------|-----|--------------|
| `Energy` | double | Strahlenergie (keV für Röntgen und Elektronen, meV für Neutronen) |
| `CameraLength` | double | Kameralänge (mm) |
| `PixelSizeInMM` | double | Pixelgröße (mm); Lesen oder Schreiben stellt die Einheit der Pixelgröße zugleich auf mm |
| `PixelSizeInNMinv` | double | Pixelgröße (nm⁻¹); Lesen oder Schreiben stellt die Einheit zugleich auf nm⁻¹ |
| `MaxNumberOfSpots` | int | Höchstzahl der Reflexe, die `FindSpots()` detektieren darf |
| `NearestNeighbor` | int | Kleinster zwischen detektierten Reflexen erlaubter Abstand (Pixel) |
| `FittingRange` | double | Radius des Bereichs um jeden Reflex, der zur Peak-Anpassung dient (Pixel) |
| `AcceptableError` | double | Toleranz der relativen Netzebenenabstandsdifferenz beim Zuordnen von Reflexen (%) |
| `IgnoreProhibitedReflections` | bool | Kinematisch verbotene Reflexe ignorieren, die über Mehrfachbeugung dennoch auftreten können |
| `MultiGrain` | bool | Nach mehreren Körnern suchen; `False` bedeutet ein einzelnes Korn |
| `MaxNumberOfGrains` | int | Höchstzahl der gesuchten Kornorientierungen, wenn `MultiGrain` `True` ist |
| `NumberOfDetectedSpots` | int | Anzahl der detektierten Reflexe (nur lesend) |
| `NumberOfCandidates` | int | Anzahl der Kandidaten des letzten `Identify()` (nur lesend) |

---

## StructureViewer-Klasse

Steuert die Strukturansicht aus einem Makro. `SaveImage()` und `Export3DModel()` öffnen das Fenster bei Bedarf zuerst, da das 3D-Modell beim Anzeigen des Fensters aufgebaut wird.

| Funktion | Beschreibung |
|---|---|
| `StructureViewer.Open()` | Das Strukturansicht-Fenster öffnen |
| `StructureViewer.Close()` | Das Strukturansicht-Fenster schließen |
| `StructureViewer.SaveImage(filename)` | Die gerenderte Hauptansicht als PNG speichern, in der Pixelgröße des Felds **Size (W×H)**; ohne `filename` öffnet sich ein Dialog |
| `StructureViewer.Export3DModel(filename, maxSizeInMM, fixedScaleInMMperNm, includeAtoms, includeBonds, includePolyhedra, polyhedraAsEdges, polyEdgeDiaInMM, includeCellEdges, cellEdgeDiaInMM, thickenBondsToMM)` | Die dargestellte Struktur für den 3D-Druck exportieren, wie **Export 3D Model (3MF/STL)** im Datei-Menü. Die Endung bestimmt das Format (`.stl` einfarbig / `.3mf` nach Element gefärbt); nur `filename` ist erforderlich — die übrigen Standardwerte entsprechen dem Dialog (größte Abmessung 80 mm, Zellkanten ⌀2,4 mm, Bindungen auf ⌀1,2 mm verdickt). Mit `fixedScaleInMMperNm` > 0 entstehen mehrere Modelle im selben Maßstab |

```python
ReciPro.StructureViewer.Export3DModel('D:/print/NaCl_60mm.stl', maxSizeInMM=60)
ReciPro.StructureViewer.Export3DModel('D:/print/NaCl_edges.stl', maxSizeInMM=60, polyhedraAsEdges=True)
```

---

## HRTEM-/STEM-/Potential-Klassen

Diese drei Bildsimulationsklassen teilen sich viele Mitglieder. Um Wiederholungen zu vermeiden, verwenden die folgenden Tabellen Platzhalter:

- **`#`** : gemeinsam für **HRTEM**, **STEM** und **Potential**. Ersetzen Sie `#` durch `HRTEM`, `STEM` oder `Potential` (z. B. `STEM.Simulate()`, `Potential.AccVol`).
- **`$`** : nur gemeinsam für **HRTEM** und **STEM**. Ersetzen Sie `$` durch `HRTEM` oder `STEM`.
- Mit einem expliziten Klassennamen geschriebene Mitglieder (`STEM.…` / `HRTEM.…`) gehören nur zu dieser Klasse. Die **Potential**-Klasse fügt keine eigenen Mitglieder hinzu; sie verwendet nur die `#`-Mitglieder.

### Fenstersteuerung

| Funktion | Beschreibung |
|----------|-------------|
| `#.Open()` | Das Fenster des Bildsimulators öffnen |
| `#.Close()` | Das Fenster des Bildsimulators schließen |
| `#.Simulate()` | Die Simulation mit den aktuellen Einstellungen ausführen |

### Mikroskop / Optik

| Eigenschaft / Funktion | Beschreibung |
|---------------------|-------------|
| `#.AccVol` | Beschleunigungsspannung (kV) |
| `$.Thickness` | Probendicke (nm) |
| `$.Defocus` | Defokus (nm) |
| `$.Cs` | Sphärische Aberration Cs (mm) |
| `$.Cc` | Chromatische Aberration Cc (mm) |
| `$.DeltaV` | Energiebreite ΔV, FWHM (eV) |
| `$.Scherzer` | Scherzer-Defokus (nm, nur lesen) |
| `STEM.ConvergenceAngle` | Konvergenz-Halbwinkel (mrad) |
| `STEM.DetectorInnerAngle` / `STEM.DetectorOuterAngle` | Innerer/äußerer Halbwinkel des Ringdetektors (mrad) |
| `STEM.EffectiveSourceSize` | Effektive Quellgröße, FWHM (pm) |
| `HRTEM.Beta` | Beleuchtungs-Halbwinkel β (Bogenmaß) |
| `HRTEM.ApertureSemiangle` | Objektivblenden-Halbwinkel (Bogenmaß) |
| `HRTEM.ApertureShiftX` / `HRTEM.ApertureShiftY` | Objektivblenden-Verschiebung (Bogenmaß) |
| `HRTEM.OpenAperture` | Objektivblende offen (true/false) |

### Simulationseigenschaften

| Eigenschaft / Funktion | Beschreibung |
|---------------------|-------------|
| `#.NumberOfDiffractedWaves` | Maximale Anzahl der gebeugten (Bloch-)Wellen |
| `#.ImageWidth` / `#.ImageHeight` | Bildgröße (Pixel) |
| `#.ImageSize(width, height)` | Die Bildgröße festlegen (Pixel) |
| `#.ImageResolution` | Bildauflösung (nm/Pixel) |
| `STEM.AngularResolution` | Winkelauflösung des konvergenten Strahls (mrad) |
| `STEM.SliceThickness` | Schichtdicke für die TDS-Berechnung (nm) |
| `HRTEM.Mode_LinearImage()` | Das Modell des linearen Bildes (quasi-kohärent) verwenden |
| `HRTEM.Mode_TCC()` | Das TCC-Modell (Transmissions-Kreuzkoeffizient) verwenden |

### Einzel-/Serienbildmodus

| Eigenschaft / Funktion | Beschreibung |
|---------------------|-------------|
| `$.SingleImageMode()` | In den Einzelbildmodus wechseln |
| `$.SerialImageMode(withThickness, withDefocus)` | In den Serienbildmodus wechseln |
| `$.SerialImageThicknessStart` / `Step` / `Num` | Serien-Dicke: Start (nm) / Schritt (nm) / Anzahl |
| `$.SerialImageDefocusStart` / `Step` / `Num` | Serien-Defokus: Start (nm) / Schritt (nm) / Anzahl |

### Bildeigenschaften

| Eigenschaft / Funktion | Beschreibung |
|---------------------|-------------|
| `#.UnitCellVisible` | Die Elementarzelle anzeigen (true/false) |
| `#.LabelVisible` | Die Bildbeschriftung anzeigen (true/false) |
| `#.LabelSize` | Schriftgröße der Beschriftung |
| `#.ScaleBarVisible` | Den Maßstabsbalken anzeigen (true/false) |
| `#.ScaleBarLength` | Länge des Maßstabsbalkens (nm) |
| `#.GaussianBlurEnabled` | Gaußsche Unschärfe anwenden (true/false) |
| `#.GaussianBlurFWHM` | FWHM der Gaußschen Unschärfe (pm) |
| `STEM.DisplayBoth()` | Sowohl elastische als auch TDS-Komponente anzeigen |
| `STEM.DisplayElastic()` | Nur die elastische Komponente anzeigen |
| `STEM.DisplayTDS()` | Nur die TDS-(inelastische) Komponente anzeigen |

### Bild speichern

| Eigenschaft / Funktion | Beschreibung |
|---------------------|-------------|
| `#.SaveImageAsPng(filename)` | Als PNG speichern (Dialog, falls filename weggelassen) |
| `#.SaveImageAsTif(filename)` | Als TIFF speichern (Dialog, falls filename weggelassen) |
| `#.SaveImageAsEmf(filename)` | Als EMF-Metadatei speichern (Dialog, falls filename weggelassen) |
| `#.SaveIndividually` | Im Serienmodus jedes Bild einzeln speichern (true/false) |
| `#.OverprintSymbols` | Elementarzelle / Beschriftungen / Maßstabsbalken auf gespeicherte Bilder aufdrucken (true/false) |

---

## Globale Funktionen

| Funktion | Beschreibung |
|----------|-------------|
| `Sleep(ms)` | Die angegebene Anzahl Millisekunden warten |

---

## Siehe auch

- [20. Makro](index.md)
- [20.2. Beispiele](2-examples.md)
