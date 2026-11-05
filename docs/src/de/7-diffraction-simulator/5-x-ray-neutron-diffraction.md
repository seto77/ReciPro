# Simulation der Röntgen-/Neutronenbeugung

Die **Simulation der Röntgen-/Neutronenbeugung** berechnet Einkristall-Beugungsmuster für Röntgenstrahlung und Neutronen. Sie ist einer der Hauptmodi des [Beugungssimulators](index.md).

> Diese Seite listet jede Einstellung auf, die auf der rechten Seite erscheint, wenn Sie **Wave Length = X-ray** (oder Neutron) wählen. Fensterweite Vorgänge wie Zeichnen und Speichern finden Sie auf der [Übersichtsseite](index.md).

GUI-Bedingungen: Wave Length = X-ray / Neutron · Incident beam = Parallel / Precession (X-ray) / Back-Laue · Intensity calculation = Only excitation error / Kinematical

![Beugungssimulator im Röntgen-/Neutronenmodus](../../assets/cap-de-auto/FormDiffractionSimulator-xray.png)

---

## Übersicht

Röntgenstrahlen haben eine größere Wellenlänge als Elektronen (Cu Kα: 0.15406 nm = 1.5406 Å), sodass die Ewald-Kugel stärker gekrümmt ist. Dadurch erfüllen weniger Punkte des reziproken Gitters gleichzeitig die Beugungsbedingung als bei Elektronen. Da die atomare Streukraft klein und die Mehrfachstreuung schwach ist, liefert die kinematische Beugungstheorie eine ausreichende Genauigkeit für die Intensitäten (die dynamische Berechnung wird nur für Elektronen unterstützt).

---

## Wave Length

![Wave Length](../../assets/cap-de-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelWaveLength.png)

Wählen Sie **X-ray** als Strahlungsquelle. Röntgenstrahlen können auf zwei Arten angegeben werden: charakteristische Röntgenstrahlung und Synchrotronstrahlung.

### Charakteristische Röntgenstrahlung

Die Wahl eines **Elements** und eines **Übergangs** legt die charakteristische Röntgenwellenlänge fest. Der Übergang wird in Siegbahn-Notation angegeben (Kα₁ / Kα₂ / Kβ usw.). Kα₁-Wellenlängen repräsentativer Elemente:

| Element | Linie | Wellenlänge (Å) | Energie (keV) |
|---------|------|-----------------|--------------|
| Cu | Kα₁ | 1.5406 | 8.048 |
| Mo | Kα₁ | 0.7093 | 17.479 |
| Co | Kα₁ | 1.7890 | 6.930 |
| Cr | Kα₁ | 2.2897 | 5.415 |

### Synchrotronstrahlung

Setzen Sie **Element** auf **0: Custom** und geben Sie die Energie (keV) oder Wellenlänge (Å) direkt ein. Es kann jede beliebige Wellenlänge verwendet werden.

---

## Modus des einfallenden Strahls

![Beam mode](../../assets/cap-de-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelBeamMode.png)

Wählt die Geometrie des einfallenden Strahls. Für Röntgenstrahlen sind drei Modi verfügbar.

### Parallel

Die normale ebene Welle. Ein paralleler einfallender Strahl, der für SAED und Einkristall-Röntgenbeugung verwendet wird.

### Precession (X-ray) — Präzessionskamera

Simuliert eine Röntgen-Präzessionskamera. Dies ist eine Präzessionsaufnahme, die eine einzelne Schicht des reziproken Gitters erfasst.

### Back-Laue (Rückstrahl-Laue)

Simuliert ein Rückstrahl-Laue-Muster mit weißer (polychromatischer) Röntgenstrahlung. In dieser Rückstrahlgeometrie wird der Detektor auf der Quellenseite platziert und **Monochrome** wird ausgeschaltet. Die Reflexe werden auf einen ebenen Detektor **senkrecht zum Strahl** im Abstand der Kameralänge auf der Quellenseite projiziert; die Detektorkippung **Tau / Phi** der [Detektorgeometrie](index.md#detector-geometry) wird bei dieser Projektion nicht verwendet.

- Das weiße Spektrum wird bei **50 keV** abgeschnitten: Reflexe werden bis $d = \lambda(50\ \text{keV})/2$ gesammelt, und es wird keine spektrale Gewichtung angewendet.
- Harmonische $n\mathbf{g}$, die auf derselben Geraden durch den Ursprung liegen, fallen auf denselben Spot; ihre $\lvert F\rvert^2$ werden auf den Reflex niedrigster Ordnung aufsummiert, und die Spot-Intensitäten werden auf den stärksten Spot normiert.
- **Anomale Dispersion (f', f'')** ist deaktiviert (die Energie ist nicht eindeutig), und **Reflexradius** hat in diesem Modus keine Einheit.

> **Hinweis**: Die Optionen für den einfallenden Strahl richten sich nach der Wellenlänge. **Precession (electron)** und **Convergence (CBED)** erscheinen nur, wenn Elektronenstrahlung gewählt ist, während die obigen Optionen **Precession (X-ray)** und **Back-Laue** nur erscheinen, wenn Röntgenstrahlung gewählt ist. Für Neutronen ist nur **Parallel** verfügbar. Je nach Zustand zum Zeitpunkt der Aufnahme zeigt der Screenshot die röntgenspezifischen Optionen möglicherweise nicht.

---

## Intensitätsberechnung

![Intensity](../../assets/cap-de-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelIntensity.png)

Wählt die Methode zur Berechnung der Spot-Intensitäten. Für Röntgenstrahlen sind zwei Modi verfügbar.

### Nur Anregungsfehler

Die Intensität wird ausschließlich durch den geometrischen Abstand zwischen der Ewald-Kugel und dem Punkt des reziproken Gitters bestimmt (dem Anregungsfehler $S_g$). Sie ist bei $S_g = 0$ — genau auf der Kugel — am größten und nimmt mit wachsendem $\lvert S_g\rvert$ ab; sie fällt auf null, sobald $\lvert S_g\rvert$ den für den reziproken Gitterpunkt angegebenen **Radius** überschreitet. Der Strukturfaktor wird ignoriert.

Nur in diesem Modus erscheinen die folgenden Optionen:

- **Alle verbotenen Reflexe ausschließen** / **Durch Gitterzentrierung verbotene Reflexe ausschließen** (Standard: nur Gitterzentrierung) : blendet durch die Raumgruppe verbotene Reflexe aus. Nicht ausgeblendete Reflexe werden in den getrennten Farben gezeichnet, die unter **Reflexfarbe** festgelegt sind (durch Schrauben-/Gleitspiegelsymmetrie verboten bzw. durch die Gitterzentrierung verboten).
- **Alle Reflexe mit gleichem Radius zeichnen** : zeichnet jeden Spot unabhängig von seinem Anregungsfehler in der Größe für $S_g = 0$.

### Kinematisch & Anr.-Fehler

Zusätzlich zum Anregungsfehler wird der kinematische Strukturfaktor $\lvert F_{hkl} \rvert^2$ in die Intensität einbezogen. Auslöschungsregeln werden streng befolgt. Der Lorentz- und der Polarisationsfaktor werden nicht berücksichtigt (dies ist eine Simulation des geometrischen Musters).

- **Anomale Dispersion (f', f'')** (nur Röntgen, standardmäßig an) : addiert die Terme der anomalen Dispersion f′, f″ bei der aktuellen Röntgenenergie zum Strukturfaktor (auch für die Debye-Ringe). In einer nicht zentrosymmetrischen Struktur mit chemisch verschiedenen anomalen Streuern gilt das Friedelsche Gesetz dann nicht mehr, und die Intensitäten von $hkl$ / $\bar h\bar k\bar l$ unterscheiden sich (Bijvoet-Paare); der Effekt ist nahe einer Absorptionskante am stärksten. Bei Back-Laue deaktiviert. Siehe [Strukturfaktor](../appendix/a2-beam-interaction/structure-factor.md).

> **Hinweis**: Die **dynamische Theorie** ist für Röntgenstrahlen deaktiviert (nur verfügbar, wenn Elektronenstrahlung gewählt ist).

---

## Darstellung der Spots

![Appearance](../../assets/cap-de-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelAppearance.png)

Steuert, wie jeder Beugungsspot gerendert wird.

- **Vollkugel / Gauß** : geometrisches Modell des Punktes im reziproken Gitter. **Vollkugel** zeichnet den Querschnitt zwischen einer Kugel und der Ewald-Kugel; bei **Kinematisch & Anr.-Fehler** ist der Kugelradius $R \times I_\text{rel}^{1/3}$ (sodass das *Volumen* der Kugel proportional zur relativen Intensität ist), bei **Nur Anregungsfehler** ist er *R* selbst. **Gauß** verwendet den Querschnitt zwischen einer 3D-Gauß-Funktion mit σ = *R* und der Ewald-Kugel (das Integral der 2D-Gauß-Funktion entspricht der Beugungsintensität); Spots mit $\lvert S_g\rvert \ge 3R$ werden nicht gezeichnet.
- **Deckkraft** : Transparenz des Spots (0 = transparent, 1 = undurchsichtig).
- **Reflexradius (R)** : Radius des Punktes im reziproken Gitter. Die gerenderte Spot-Größe wird durch die Kombination aus **Darstellung** und **Intensitätsberechnung** bestimmt.
- **Helligkeit** : nur im Modus **Gauß** aktiv. Legt die integrierte Intensität der gerenderten Gauß-Funktion fest.
- **Farbskala** (nur Gauß) : **Gray**, **Cold-Warm**, **Spectrum** oder **Fire**.
- **Log-Skala** (nur Gauß) : Intensitäten auf einer logarithmischen Skala anzeigen.
- **Reflexfarbe** : Spot-Farben, die mit der Farbskala **Gray** verwendet werden (bei den anderen Farbskalen ist diese Gruppe ausgeblendet).
- **Farbe je Kristall** : zeichnet die Spots in der dem jeweiligen Kristall zugewiesenen Farbe, wenn aktiviert.

---

## Debye-Ringe (polykristallin)

Debye-Ringe einer polykristallinen Probe können angezeigt werden. Aktivieren Sie **Debye-Ringe** in der Symbolleiste (siehe [Symbolleiste](index.md#toolbar)).

- **Beugungsintensität ignorieren** : zeichnet alle Ringe in derselben Farbe und Intensität (verwendet für einen rein geometrischen Vergleich, der den Strukturfaktor ignoriert).
- **Index-Beschriftungen anzeigen** : zeigt den (*hkl*)-Index neben jedem Ring an.

Detaillierte Einstellungen befinden sich auf der Registerkarte Debye rings des [Registerkartenmenüs](index.md#drawing-overlay-tabs).

---

## Neutronenbeugung

Die Wahl von **Neutron** im Steuerelement Wave Length berechnet ein Neutronenbeugungsmuster. Geben Sie die Energie (meV) oder Wellenlänge (Å oder nm, umschaltbar; Standard Å) ein. Der einfallende Strahl kann nur **Parallel** sein. Die Intensitätsberechnung kann **Nur Anregungsfehler** oder **Kinematisch & Anr.-Fehler** sein (**Dynamische Theorie** ist nicht verfügbar). Die kinematische Intensität verwendet die Neutronenstreulänge anstelle des atomaren Streufaktors.

---

## Unterschiede zwischen Röntgen- und Elektronenbeugung

| Merkmal | Röntgenbeugung | Elektronenbeugung |
|---------|-------------------|----------------------|
| Wellenlänge | Lang (0.5–2.5 Å) | Kurz (0.02–0.04 Å) |
| Krümmung der Ewald-Kugel | Groß | Klein (nahezu flach) |
| Gleichzeitige Reflexe | Wenige | Viele |
| Streufaktor | Atomformfaktor $f(s)$ | Elektronenstreufaktor $f_e(s)$ |
| Dynamische Effekte | Meist klein | Groß |
| Auslöschungsregeln | Streng befolgt | Können durch Mehrfachstreuung verletzt werden |

---

## Allgemeine Vorgänge

Fensterweite Vorgänge wie Kameralänge, Detektorgeometrie, Speichern von Mustern und Farbeinstellungen finden Sie auf der [Übersichtsseite](index.md). Die detaillierte Detektorgeometrie wird im unten gezeigten Geometriefenster konfiguriert.

![Einstellungen der Detektorgeometrie](../../assets/cap-de-auto/FormDiffractionSimulatorGeometry.png)

---

## Siehe auch

- [Beugungssimulator (Übersicht)](index.md)
- [SAED-Simulation](1-saed-simulation.md)
- [Präzessions-Elektronenbeugung (PED) — Simulation](2-ped-simulation.md)
- [Konvergente Elektronenbeugung (CBED) — Simulation](3-cbed-simulation.md)
- [Koordinatensystem — Kristallorientierung](../appendix/a1-coordinate-system/1-orientation.md)
