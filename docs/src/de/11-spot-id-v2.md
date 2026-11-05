# Spot ID v2

**Spot ID v2** erkennt, fittet und indiziert Beugungsreflexe auf einem experimentellen Beugungsbild. [Spot ID v1](10-spot-id.md) (das frühere TEM ID) nimmt die Reflexgeometrie dagegen als numerische Eingabe entgegen.

![Spot ID v2](../assets/cap-de-auto/FormSpotIDV2.png)

---

## Tastatur- & Maus-Kurzbefehle

Die Reflexliste erstellen Sie direkt auf dem geladenen Bild. Der Bildbereich nutzt die standardmäßige [Bildansicht-Navigation](21-shortcuts.md) von ReciPro zum Verschieben/Zoomen; für die Reflexbearbeitung kommen die folgenden Kombinationen hinzu.

| Kurzbefehl | Aktion |
|----------|--------|
| <kbd>F1</kbd> | Diese Seite des Online-Handbuchs öffnen |
| Linker Doppelklick auf das Bild | Einen Reflex an diesem Punkt hinzufügen (Peak-gefittet) |
| <kbd>CTRL</kbd> + linker Doppelklick | Einen Reflex hinzufügen und als direkten (000) Strahl markieren |
| Linksklick auf einen Reflex | Den nächstgelegenen Reflex auswählen |
| <kbd>CTRL</kbd> + Rechtsklick auf einen Reflex | Den nächstgelegenen Reflex löschen |
| <kbd>CTRL</kbd> + Pfeiltasten | Den ausgewählten Reflex um ein Pixel verschieben |
| Linksziehen / Mittelziehen (leerer Bereich) | Das Bild verschieben |
| Mausrad | Am Cursor hinein-/herauszoomen |
| Rechtsziehen eines Rahmens | In den ausgewählten Bereich hineinzoomen |
| Rechter Doppelklick | Herauszoomen |
| Doppelklick auf den Zeilenkopf eines Reflexes (Tabelle) | Auf diesen Reflex zoomen (×2) |

→ Siehe **[21. Tastatur- & Maus-Kurzbefehle](21-shortcuts.md)** für jedes Fenster auf einen Blick.

---

## Menü „Datei“

Ein Beugungsbild öffnen; **Speichern** / **Kopieren** schreiben das angezeigte Bild als Metadatei (EMF) oder als Bild (PNG). Bilder lassen sich auch per Drag & Drop laden. Bei einer in nm⁻¹ kalibrierten Gatan-DM3/DM4-Datei wird die Strahlung auf Elektronen gesetzt, und Beschleunigungsspannung sowie Pixelgröße (nm⁻¹) werden aus der Datei gelesen (die Kameralänge wird in diesem Fall nicht benötigt).

**Speichern ▸ Candidate list (CSV)...** exportiert die durch die Indizierung gefundenen Orientierungskandidaten, **Kopieren ▸ Candidate list (TSV)** legt dieselbe Tabelle in die Zwischenablage: eine Zeile je Kandidat × Korn mit Rang, Kornindex, Kristallname, ZXZ-Eulerwinkeln (°), den neun Elementen der Rotationsmatrix, dem mittleren quadratischen Residuum (nm⁻²), der Zahl der zugeordneten Reflexe und den Zuordnungen Reflex → (hkl). Die CSV-Datei verwendet stets den Punkt als Dezimaltrennzeichen; die TSV-Fassung lässt sich direkt in eine Tabellenkalkulation einfügen.

---

## Optik

![Optik](../assets/cap-de-auto/FormSpotIDV2.splitContainer1.panel1.groupBoxOptics.png)

### Strahlungsquelle

Wählen Sie den Strahlungstyp (Röntgen / Elektron / Neutron) und stellen Sie die Energie oder Wellenlänge ein.

### Kameralänge / Pixelgröße

Die Kameralänge (mm) und die Detektor-Pixelgröße (mm oder nm⁻¹). Wird eine in nm⁻¹ kalibrierte Gatan-DM-Datei geladen, wird die Pixelgröße (nm⁻¹) aus dem Dateikopf gelesen, und die Kameralänge wird nicht verwendet.

**Debye-Ringe anzeigen** (standardmäßig aus) und **Hilfskreise anzeigen** (standardmäßig an) zeichnen die Debye-Ringe des Kristalls bzw. Hilfskreise in das Muster.

---

## Reflexinformation

![Reflexinformation](../assets/cap-de-auto/FormSpotIDV2.splitContainer1.groupBoxSpot.png)

- **Reflexe finden & anpassen**: Automatische Reflexerkennung mittels lokaler Maxima und Untergrundabzug.
- **Anzahl**: Die maximale Anzahl der zu erkennenden Reflexe.
- **Nächster Nachbar**: Der minimale Abstand (px), der zwischen erkannten Reflexen zulässig ist. Peaks, die enger beieinander liegen, werden zusammengeführt, um eine Doppelerkennung desselben Reflexes zu verhindern.
- **Anpassungsradius**: Der Radius (px) des kreisförmigen Bereichs, der zum Fitten des Peaks jedes Reflexes verwendet wird. Pixel innerhalb dieses Kreises werden mit einer Pseudo-Voigt-Funktion gefittet.
- **Auf alle anwenden**: Setzt den Fit-Radius jedes Reflexes auf den aktuellen Wert von **Anpassungsradius**.
- **Löschen / Alle leeren**: Den ausgewählten Reflex oder alle erkannten Reflexe entfernen.
- **Speichern** / **Kopieren**: Die Reflextabelle in eine Datei speichern bzw. in die Zwischenablage kopieren.
- **Alle anpassen**: Positionen und Intensitäten aller erkannten Reflexe neu fitten.
- **Symbole** / **Nummern**: Markierungssymbole / Nummern auf den beobachteten Reflexen anzeigen (beide standardmäßig an).
- **Globale Anp.**: Führt einen globalen Fit aller Reflexpositionen auf einmal durch (experimentell).
- **Donut**: Wendet einen donutförmigen Untergrundabzug an (experimentell); das Feld daneben legt die Breite (px) des Rings um jeden Reflex fest, dessen Mittelwert als lokaler Untergrund abgezogen wird.
- **Reflex-Details**: Wenn aktiviert, öffnet sich ein Fenster mit detaillierten Informationen zum aktuell ausgewählten Reflex.
- **Funktions-Details** (beim Öffnen des Fensters aus): Zeigt eine Abbildung der zweidimensionalen Pseudo-Voigt-Funktion und ihrer in der Reflextabelle verwendeten Parameter (x_0, y_0, H1, H2, θ, η, A, B_0, B_x, B_y).

![Details of the spot](../assets/cap-de-auto/FormSpotIDv2Details.png)

---

## Index

![Index](../assets/cap-de-auto/FormSpotIDV2.splitContainer1.groupBoxIndex.png)

- **Reflexe identifizieren**: Führt den Indizierungsalgorithmus aus, um den am besten passenden Kristall und die Zonenachse zu finden.
- **Zulässiger Fehler**: Relative Toleranz des Netzebenenabstands in % (0,1–10, Standard 2). Die Winkeltoleranz zwischen Reflexen wird daraus als asin(Toleranz)/2 abgeleitet.
- **Verbotene Reflexe ignorieren** (standardmäßig aus; nur Elektronenbeugung): Wenn aktiviert, werden alle systematisch ausgelöschten Reflexe, einschließlich der durch Schraubenachsen und Gleitspiegelebenen verbotenen, von den Kandidatenreflexen ausgeschlossen. Wenn deaktiviert, werden nur die Auslöschungen aufgrund der Gitterzentrierung (I, F, A, B, C, R) ausgeschlossen, sodass durch Schrauben-/Gleitspiegelsymmetrie verbotene Reflexe, die durch Mehrfachbeugung erscheinen können, Kandidaten bleiben. Für Röntgenstrahlung werden stets alle ausgelöschten Reflexe ausgeschlossen.
- **Einzelkorn / Mehrere Körner**: Suche nach einer einzelnen Orientierung (Einkristall) oder nach mehreren Orientierungen (ein polykristalliner / Mehrkorn-Bereich). Für mehrere Körner legt **Max. Anzahl Körner** die Obergrenze für die Anzahl der zu suchenden Körner fest.
- **Zonenachse anzeigen** / **Symbol anzeigen** / **Beschriftung anzeigen**: Die Zonenachse sowie die Symbole / Indexbeschriftungen der berechneten Reflexe einblenden.
- **Dicke & Richtung verfeinern (nur für SAED)**: Verfeinert die Probendicke (nm) und die Strahlrichtung des ausgewählten Korns, indem die beobachteten Reflexintensitäten mit dynamischen Rechnungen (Bloch-Wellen-Methode) abgeglichen werden. **Gebeugte Wellen** (1–2048, Standard 400) ist die Anzahl der Strahlen, und **Suchwinkelbereich** (mrad, 1–10, Standard 2) ist der halbe Winkelbereich der durchsuchten Einfallsrichtungen. **Stopp** bricht die Berechnung ab.
- **Results**: Die besten Übereinstimmungen werden mit Kristallname, Zonenachse [uvw] und den einzelnen Reflexindizes (hkl) angezeigt.

---

## Siehe auch

- [Spot ID v1](10-spot-id.md)
- [Beugungssimulator](7-diffraction-simulator/index.md)
- [Hauptfenster](0-main-window.md)
- [Tastatur- & Maus-Kurzbefehle](21-shortcuts.md)
