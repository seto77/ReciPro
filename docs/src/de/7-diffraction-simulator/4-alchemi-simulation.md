# ALCHEMI-Simulation

**ALCHEMI (Atom Location by CHannelling-Enhanced MIcroanalysis)** bestimmt, **welchen Platz ein Dotierungsatom besetzt**, indem die Ausbeute charakteristischer Röntgenstrahlung gemessen wird, während der Kristall entlang einer systematischen Reihe verkippt wird, und indem die Orientierungsabhängigkeit ausgewertet wird. Der ALCHEMI-Simulator von ReciPro berechnet aus einer Kristallstruktur und einem Satz von Platzhypothesen die **Rocking-Kurve (Ionisationsausbeute in Abhängigkeit von der Orientierung) in Vorwärtsrichtung**.

> **Dies ist eine Preview-Funktion.** v1 führt **ausschließlich eindimensionale Vorwärtsrechnungen** durch; die Anpassung an experimentelle Daten und die 2D-Karte (2D-HARECXS) sind nicht implementiert (diese Registerkarten sind ausgeblendet). **Nach bestem Wissen der Autoren existiert kein anderer öffentlich verfügbarer ALCHEMI-Vorwärtssimulator.** Da es keine Implementierung zum Gegenprüfen gibt, lesen Sie [Geltungsbereich und bekannte Grenzen](#geltungsbereich-und-bekannte-grenzen), bevor Sie die Ergebnisse quantitativ verwenden.

Öffnen über das Menü **Optionen** des [Beugungssimulators](index.md) → **ALCHEMI-Simulator...**

GUI-Bedingungen: Wave Length = Electron (Kristall, Beschleunigungsspannung und Orientierung werden vom übergeordneten Beugungssimulator übernommen)

![ALCHEMI-Simulator](../../assets/cap-de-auto/FormALCHEMI.png)

Das Fenster hat **links die Einstellungen** (Scan, Dicke, Berechnung, Ionisationskanäle, Platzhypothesen) und **rechts das Ergebnis** (Registerkarte Kurve).

---

## Was berechnet wird

Für jede einfallende Orientierung wird das Wellenfeld im Kristall mit der Bloch-Wellen-Methode gelöst, und für jedes Paar aus Platz $s$ und Ionisationskanal $c$ wird die Ionisationsausbeute analytisch bis zur Dicke $t$ integriert.

$$
Y_\text{dyn} = \mathrm{Re} \sum_{jj'} \alpha_j^{*}\,\bigl(C^{\dagger} \mu_{s,c} C\bigr)_{jj'}\, \alpha_{j'}\, F_{jj'}(t),
\qquad F_{jj'}(t) = \frac{e^{\lambda t} - 1}{\lambda}
$$

Die Ionisationsmatrix $\mu$ hängt nur von der Differenz zweier Reflexe ab, $G = \mathbf{g}_h - \mathbf{g}_g$.

$$
\mu_{hg} = \sum_a \mathrm{Occ}_a\, e^{-M_a(G)}\, \sigma_c\, F_c(|G|/2)\, e^{-2\pi i\,G \cdot \mathbf{r}_a}
$$

- $\sigma_c$ : totaler Ionisationsquerschnitt aus dem **Bote–Salvat**-Modell
- $F_c(s)$ : normierter Ionisationsformfaktor aus selbst erzeugten **DHFS**-Tabellen (dieselbe Datenbasis wie [Strahl-Wechselwirkung](../3-beam-interaction.md) und [STEM-EDX](../9-hrtem-stem-simulator/2-stem-simulation.md))
- $e^{-M_a(G)}$ : Debye-Waller-Faktor (anisotrope ADPs werden unterstützt)

Das entspricht der **lokalen Formfaktor-Näherung** von ICSC (Oxley & Allen 2003). Die Zwei-Impuls-MDFF wird nicht verwendet.

### Dechannelling-Anteil

Elektronen, die durch thermisch-diffuse Absorption aus dem kohärenten Bloch-Feld entfernt werden, durchlaufen die restliche Dicke als richtungsmäßig randomisierte Elektronen und ionisieren auch dort.

$$
Y_\text{dech} = \frac{\mu_{00}}{V_c}\,\bigl(t - L_\text{coh}(t)\bigr),
\qquad L_\text{coh}(t) = \int_0^t \sum_g |\psi_g(z)|^2\,dz
$$

Das Abwählen von **Dechannelling-Anteil einbeziehen** im Feld **Berechnung** lässt diesen Term entfallen. Er macht bei typischen Dicken mehrere zehn Prozent der Gesamtausbeute aus; wird er weggelassen, erscheint der Platzkontrast stärker, als er ist.

### Ausgabegröße

Die primäre Größe ist die **Anzahl der pro einfallendem Elektron erzeugten Innerschalen-Löcher**. **Die Umrechnung in Röntgenphotonen (Fluoreszenzausbeute und Linienverzweigung), die Röntgen-Selbstabsorption in der Probe sowie Detektoreffizienz und Raumwinkel sind NICHT berücksichtigt.**

---

## Linker Bereich: Einstellungen

### Rocking-Scan

| Eintrag | Beschreibung | Standard |
|---------|--------------|----------|
| **Reihe ( h k l )** | Die abzutastende systematische Reihe, als Reflexindizes angegeben. Die Kippachse steht senkrecht sowohl auf dem Strahl als auch auf diesem $\mathbf{g}$, sodass der Scan diese Reihe durch ihre Bragg-Bedingungen führt | (1 0 0) |
| **Bereich ±** | Halbe Breite des Kippscans (mrad). Oberhalb von etwa 10 mrad ist eine feste Vereinigungsbasis nicht mehr garantiert, oberhalb von 30 mrad liegt es außerhalb der v1-Zusicherung | 8 mrad |
| **Punkte** | Anzahl der Scanpunkte (3–1001) | 101 |

Die Zeile darunter zeigt den Bragg-Winkel $\theta_B$ der gewählten Reihe, wie vielen $\theta_B$ die Scanbreite entspricht, und die Kippschrittweite — so sehen Sie schon vor dem Start, wie weit der Scan tatsächlich reicht.

### Dicke

Geben Sie Anfang, Ende und Schritt (nm) an. **Alle Dicken werden in einem Lauf gemeinsam berechnet**; das Ergebnis wird mit dem Schieberegler unter der Kurve umgeschaltet.

Der Platzkontrast ändert sich zwischen dünnen und dicken Proben stark und kann sogar das Vorzeichen wechseln. Prüfen Sie daher mehrere Dicken, bevor Sie Schlüsse ziehen. Deshalb sitzt der Dickenwähler direkt unter der Kurve.

### Berechnung

| Eintrag | Beschreibung | Standard |
|---------|--------------|----------|
| **Max. Strahlen** | Obergrenze der Zahl der Bloch-Wellen pro Orientierung (1–1600). Die Vereinigung über den gesamten Scan ist größer | 120 |
| **Löser** | Rechenkern für das Eigenwertproblem: **Nativ** (Eigen C++) oder **Verwaltet** (.NET). Wo der native Löser nicht verfügbar ist, wird die Auswahl auf Verwaltet festgelegt | Nativ |
| **Dechannelling-Anteil einbeziehen** | Ob $Y_\text{dech}$ (oben) addiert wird | ein |

**Die Obergrenze von 1600 Strahlen ist das Gegenstück zum tabellierten Bereich $s \le 16\ \text{Å}^{-1}$ des Ionisationsformfaktors.** In der Praxis erfordern selbst 1600 Strahlen nur etwa 10,5 Å⁻¹, sodass der tabellierte Bereich bei Einhaltung der Obergrenze nie ausgeschöpft wird. Der tatsächlich erreichte Wert steht in der Zeile [Basisdiagnose](#basisdiagnose) unter dem Diagramm.

### Ionisationskanäle

Die Liste von Element und Schale, die ionisiert werden sollen. Jede Zeile lautet `Element (Z) Schale   Kantenenergie   U = Überspannung`, mit einer Kennzeichnung in Klammern, wo Vorsicht geboten ist.

- Kanäle, die **nicht angeregt werden können** (die Primärenergie liegt unter der Absorptionskante) oder die **außerhalb des tabellierten Bereichs** liegen, werden mit Begründung aufgeführt und lassen sich nicht anwählen
- Kanäle mit einer Überspannung $U = E_0/E_\text{Kante}$ unter 1,2 tragen einen Warnhinweis, weil der Querschnitt dort weniger zuverlässig ist

### Platzhypothesen

Die Liste der Atomplätze, deren Ausbeute getrennt berechnet wird, dargestellt als `Bezeichnung Element (x, y, z) ×Multiplizität Occ Besetzung`.

⚠ **Im Tracer-Bild darf ein Kanal mit jedem beliebigen Platz kombiniert werden.** Der Ionisationskanal eines Dotierungsatoms mit der Geometrie eines Wirtsplatzes (Position, ADP, Besetzung) zu paaren, ist die vorgesehene Verwendung; eine Beschränkung auf übereinstimmende Elemente wäre falsch. Es werden **alle Kombinationen** der angehakten Kanäle und Plätze berechnet.

### Simulieren / Stopp

**Simulieren** startet den Scan. Der Fortschritt wird in der Statusleiste in fünf Stufen gemeldet (Ionisationsdaten werden aufgelöst → Vereinigungsbasis wird aufgebaut → Ionisationsmatrizen werden aufgebaut → Orientierungen werden gelöst → erweiterte Basis wird geprüft); **Stopp** bricht jederzeit ab.

---

## Rechter Bereich: Registerkarte Kurve

Nach Abschluss der Rechnung wird pro Paar aus Platz × Kanal eine Kurve gezeichnet. Die Legende lautet `Platzbezeichnung / Kanal`.

| Eintrag | Beschreibung |
|---------|--------------|
| **Dicke** | Wählt mit einem Schieberegler die angezeigte Dicke (es wird nichts neu berechnet) |
| **Normierung** | **Scan-Mittel (ICP)** = durch das Mittel über den gesamten Scan teilen (die in ALCHEMI übliche Größe) / **Maximum = 1** / **Roh (pro Elektron)** |
| **X-Achse** | Schaltet zwischen **mrad** und **θ_B** (in Einheiten des Bragg-Winkels der abgetasteten Reihe) um |
| **Bragg-Bedingungen** | Zeichnet senkrechte Linien bei $\theta = n\,\theta_B$ |
| **CSV exportieren** | Schreibt die rohen Kurven für jede Orientierung, Dicke, jeden Platz und Kanal in eine CSV-Datei ([unten](#csv-export)) |

⚠ **Die Normierung ist nur eine Anzeigetransformation.** Die gespeicherte Größe sind stets die pro einfallendem Elektron erzeugten Löcher, und **Maximum = 1 dient nur der Anzeige** — es darf nicht als ICP-Bezug verwendet werden.

### Kontrast und Korrelation

Die erste Zeile unter der Kurve nennt je Serie den **Kontrast** $(\max-\min)/\text{Mittel}$ und den **Korrelationskoeffizienten** $r$ gegenüber der ersten Serie. Sie ist eine Zusammenfassung, um auf einen Blick zu beurteilen, welcher Platz wirkt: Zwei Serien mit $r$ nahe $+1$ haben dieselbe Orientierungsabhängigkeit, das heißt, diese Daten können jene Plätze nicht trennen.

### Basisdiagnose

Die zweite Zeile meldet den Zustand der Basis.

```text
basis 347 (184 + 163)   F(s) ≤ 6.20 Å⁻¹   expanded-basis 6.7e-3   ⚠ NICHT fit-tauglich
```

- **basis N (nur Zentrum + durch Vereinigung ergänzt)** : Größe der echten Vereinigung der Reflexe über alle Orientierungen des Scans
- **F(s) ≤ … Å⁻¹** : das größte Formfaktor-Argument, das die Basis tatsächlich benötigt hat
- **expanded-basis** : die maximale relative Abweichung, wenn Zentrum und beide Enden des Scans mit einer 1,25-fachen Basis erneut gelöst werden. Das ist ein **Stellvertreter für den Konvergenzfehler**
- **fit-tauglich / NICHT fit-tauglich** : das Ergebnis wird **nicht tauglich**, wenn der expanded-basis-Wert die Schwelle $3\times10^{-3}$ überschreitet

⚠ **Verwenden Sie ein als nicht fit-tauglich gekennzeichnetes Ergebnis nicht für eine quantitative Besetzungsanpassung.** Das ist eine Freigabebedingung von v1. Beachten Sie außerdem: Die Diagnose ist auf der **absoluten Ausbeute** definiert und fällt daher konservativ aus, wenn Sie nur das ICP betrachten (das durch das Scan-Mittel teilt).

In den folgenden Situationen werden weitere Warnungen angehängt.

- **Beschleunigungsspannung unter 80 kV** : Bei dieser Spannung kann die Formfaktortabelle $s$ bis $16\ \text{Å}^{-1}$ nicht garantieren. Die Rechnung selbst bleibt korrekt, solange das von der Basis benötigte $s$ im zertifizierten Bereich bleibt — daher ist dies ein **Hinweis, keine Ablehnung**
- **Abschneiden des Formfaktors** : Wo $F(s)$ jenseits des zertifizierten Bereichs auf null gesetzt wurde, **wird die resultierende Fehlerschranke $|F| \le \varepsilon$ numerisch angezeigt**. Es wird nichts stillschweigend extrapoliert

---

## CSV-Export {#csv-export}

**CSV exportieren** schreibt eine Tabelle im Long-Format, der die beiden folgenden Kopfzeilen vorangestellt sind. Der Kopf ist so gestaltet, dass die Datei allein die zur Reproduktion nötigen Bedingungen nennt.

```text
# ReciPro ALCHEMI, 250.0 kV, row (1 0 0), theta_B 3.8424 mrad, model LocalFormFactor,
#   quantity ..., normalization PerIncidentElectron (self-absorption and detector efficiency are NOT applied)
# basis 347 beams, hash ..., expanded-basis 6.658e-003, fit-eligible False
tilt_mrad,thickness_nm,site,channel,dynamic,dechannelled,total
```

`dynamic` / `dechannelled` / `total` werden getrennt gespeichert, sodass **der Beitrag des Dechannelling-Anteils nachträglich beurteilt werden kann**. Die Werte sind roh (pro einfallendem Elektron) und durchlaufen nicht die Anzeigenormierung; das Dezimaltrennzeichen ist immer ein Punkt.

---

## Geltungsbereich und bekannte Grenzen

„Berechenbar" und „quantitativ verifiziert" sind zweierlei. Dieser Abschnitt benennt Letzteres.

### Quantitativ verifizierter Bereich

**β-AlCo [001] bei 250 keV, Kanäle Al-K / Co-K / Co-L** — und sonst nichts. Verglichen mit einer Multislice-Rechnung mit eingefrorenen Phononen (py_multislice), deren dynamische Formulierung vollständig unabhängig ist:

- **Al-Platz (leichte Säule)** : RMS-Residuum bezogen auf die ICP-Modulation ≤3,2 % bei allen Dicken, ≤0,6 % für $t \ge 10$ nm
- **Co-Platz (schwere Säule)** : ≤3 % für $t \le 4$ nm, aber **6–17 % für $t \gtrsim 10$ nm**

Jedes andere System, Element, jede andere Schale oder Spannung ist „berechenbar", aber nicht „quantitativ verifiziert".

### Bekannter systematischer Fehler — der Dechannelling-Term hat keine Platzkorrelation

Der Dechannelling-Term von v1 ist eine von der Orientierung unabhängige Konstante; seine einzige Wirkung auf das ICP ist, es in Richtung 1 zu ziehen. Tatsächlich kanalisiert ein Teil der thermisch gestreuten Elektronen erneut in die Säulen und kehrt, da starke Streuer, **bevorzugt zu den schweren Säulen** zurück. Im obigen Vergleich wurde die effektive Größe dieses Beitrags **an den schweren Säulen um 10–19 Punkte unterschätzt**.

→ **Für leichte oder schwach streuende Plätze oder für $t \lesssim 5$ nm beträgt die Übereinstimmung mit einer unabhängigen Implementierung 1–3 %. Für schwere Säulen mit $t \gtrsim 10$ nm besteht ein systematischer Fehler von 6–17 % der ICP-Modulation.** Ein Wiedereinspeisungsmodell mit Platzkorrelation ist auf v1.1 oder später verschoben.

### Nicht im Vorwärtsmodell enthalten

**Eine Faltung mit der Winkelverbreiterung allein reproduziert kein Experiment.** Nichts davon ist enthalten:

- **Dickenverteilung** und **Verbiegung** der Probe
- Röntgen-**Selbstabsorption**
- **Detektoreffizienz und Raumwinkel**
- **Untergrund** (Bremsstrahlung, überlappende Linien)
- Faltung mit der **Winkelverbreiterung des einfallenden Strahls** (Konvergenzhalbwinkel, Drift) — in v1 nicht implementiert

### Modellannahmen

- **Nur Tracer-Näherung** : Die lineare Überlagerung von Platzantworten gilt nur im verdünnten Grenzfall, in dem das Dotierungsatom das elastische Wellenfeld nicht stört. VCA bei endlicher Konzentration liegt außerhalb des Umfangs von v1
- **Lokale Formfaktor-Näherung** : $\mu$ ist allein eine Funktion von $G = \mathbf{g}_h - \mathbf{g}_g$, nicht die Zwei-Impuls-MDFF (Modell A von OAR 1999). Die Näherung versagt bei K-Schalen leichter Elemente und bei niederenergetischen Kanten
- **Löcher, keine Röntgenphotonen** : Fluoreszenzausbeute und Linienverzweigung werden nicht angewendet
- **Die untere Grenze der Beschleunigungsspannung liegt bei 80 kV** : Das ist die niedrigste Spannung, bei der $s = 16\ \text{Å}^{-1}$ garantiert werden kann, keine Ablehnungsschwelle

---

## Siehe auch

- [Beugungssimulator (Übersicht)](index.md)
- [CBED-Simulation](3-cbed-simulation.md)
- [Dynamische Berechnung (gemeinsamer Kern)](../appendix/a3-bloch-wave/calculation.md)
- [STEM-Simulation](../9-hrtem-stem-simulator/2-stem-simulation.md) — STEM-EDX, das dieselbe Ionisationsdatenbasis verwendet
- [Strahl-Wechselwirkung](../3-beam-interaction.md) — Daten zu Querschnitten und Absorptionskanten
