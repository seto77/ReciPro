# Symmetrieinformationen

**Symmetrieinformationen** zeigt detaillierte Informationen über die Raumgruppensymmetrie des ausgewählten Kristalls an und stellt zusätzlich schematische Diagramme der Symmetrieelemente und der allgemeinen Lagen im Stil der *International Tables for Crystallography* Vol. A dar.

![Symmetrieinformationen](../assets/cap-de-auto/FormSymmetryInformation.png)

Das Fenster ist unterteilt in einen Bereich zur Raumgruppen-Identität (oben links), einen Berechnungs-/Tabellenbereich mit Registerkarten (oben rechts) und zwei schematische Diagramme (unten).

!!! tip "Symmetrietheorie (Anhang A4)"
    Was ein Hermann–Mauguin-/Hall-/Schoenflies-Symbol tatsächlich kodiert, die gruppentheoretischen Klassifikationen auf der Registerkarte **Eigenschaften** (zentrosymmetrisch, Sohncke, symmorph, polar, …), die Bedeutung der Symmetrieelement- und Allgemeine-Lagen-Diagramme unten sowie die von **Gruppenrelationen...** gezeigten Gruppe-Untergruppe-Beziehungen werden alle in **[Anhang A4. Symmetrie und Raumgruppen](appendix/a4-symmetry-space-groups/index.md)** erklärt.

---

## Tastatur- & Maus-Kurzbefehle

Dieses Fenster hat keine besonderen Tasten- oder Mauskombinationen. <kbd>F1</kbd> öffnet diese Handbuchseite, und die beiden **Kopieren**-Schaltflächen legen das Symmetrieelement-Diagramm und das Diagramm der allgemeinen Lagen in die Zwischenablage (als Vektor-**emf** oder Raster-**bmp**, wählbar über **Kopierformat**).

→ Siehe **[21. Tastatur- & Maus-Kurzbefehle](21-shortcuts.md)** für jedes Fenster auf einen Blick.

---

## Raumgruppen-Identität

Das Feld oben links listet für die aktuelle Raumgruppe:

- die **Nummer** (1–230) und den Aufstellungsindex
- das **Kristallsystem**
- die **Punktgruppe** : Hermann–Mauguin- (HM) und Schoenflies- (SF) Symbol
- die **Raumgruppe** : HM-Kurzsymbol, HM-Vollsymbol, SF-Symbol und **Hall-Symbol**

---

## Geometrische Berechnung

![Geometrische Berechnung](../assets/cap-de-auto/FormSymmetryInformation.splitContainer1.panel2.tabControl.tabPageGeometrics.png)

Geben Sie zwei Kristallebenen \((h_1, k_1, l_1)\), \((h_2, k_2, l_2)\) oder zwei Richtungsindizes \([u_1, v_1, w_1]\), \([u_2, v_2, w_2]\) ein, um Folgendes zu erhalten:

- den Netzebenenabstand jeder Ebene / die Länge jeder Achse,
- den Winkel zwischen den beiden Ebenen (oder den beiden Achsen),
- den Winkel zwischen der Normalen von Ebene 1 und Achse 1 sowie zwischen der Normalen von Ebene 2 und Achse 2 (90°, wenn die Achse in der Ebene liegt),
- **Achse parallel zu beiden Ebenen** (die Zonenachse [*uvw*] der beiden Ebenen) und **Ebene, die beide Achsen enthält** (die Ebene (*hkl*), die die beiden Achsen enthält). Die Vorzeichen folgen [*uvw*] = (*h*₁*k*₁*l*₁) × (*h*₂*k*₂*l*₂) und (*hkl*) = [*u*₁*v*₁*w*₁] × [*u*₂*v*₂*w*₂]. <!-- 261106Cl 作者の決定 B16 --> <!-- 261105Cl 監査: 画面のラベル (FormSymmetryInformation.cs label40/label42) と面・軸の角度のツールチップ (numericBoxAnglePlaneAxis1/2.ToolTip) に合わせた。以前の「normal to」は逆 -->

Diese Berechnungen berücksichtigen die Metrik der aktuellen Elementarzelle. Längen werden in Å und Winkel in Grad angezeigt.

---

## Wyckoff-Lagen

![Wyckoff-Lagen](../assets/cap-de-auto/FormSymmetryInformation.splitContainer1.panel2.tabControl.tabPageWyckoff.png)

Listet jede Wyckoff-Lage mit ihrer Multiplizität (**Mult.**), ihrem Wyckoff-Buchstaben (**Wyck.-Buchst.**), ihrer Lagesymmetrie (**Lagesym.**) und den **Koordinaten** (bis zu vier je Zeile; längere Listen werden in den folgenden Zeilen fortgesetzt). Der erste Wyckoff-Eintrag (höchste Multiplizität) ist die allgemeine Lage. Bei zentrierten Gittern werden die Gittertranslationsvektoren in der Kopfzeile angezeigt; bei R-Gruppen in hexagonalen Achsen sind dies die drei Zentrierungstranslationen der obversen Aufstellung, (0,0,0)+, (2/3,1/3,1/3)+ und (1/3,2/3,2/3)+. <!-- 261105Cl 監査: R 群の六方軸でも中心化の行が出る (FormSymmetryInformation.cs, Controls ead58e7) -->

---

## Bedingungen

![Bedingungen](../assets/cap-de-auto/FormSymmetryInformation.splitContainer1.panel2.tabControl.tabPageConditions.png)

Die Reflexionsbedingungen, die sich aus der Gitterzentrierung und aus den Gleitspiegel- und Schraubenoperatoren ergeben.

Jede Zeile hat drei Spalten: **Indizes** (die Reflexklasse, z. B. $hkl$, $h0l$, $00l$), **Bedingung** (z. B. $h+k=2n$) und **Element** (die verantwortliche Gitterzentrierung bzw. das verantwortliche Symmetrieelement). Gibt es keine Bedingung, wird *No Condition* angezeigt.

---

## Operationen

![Operationen](../assets/cap-de-auto/FormSymmetryInformation.splitContainer1.panel2.tabControl.tabPageOperations.png)

Listet jede Symmetrieoperation der allgemeinen Lage (Gitterzentrierungstranslationen bereits eingerechnet) als Koordinatentripel, Seitz-Symbol und geometrischen Typ in Klartext (z. B. *"3-fold rotation"*, *"c-glide plane"*, *"screw axis"*). **Kopieren (CIF)** kopiert die vollständige Liste als CIF-Schleife `_space_group_symop_operation_xyz` in die Zwischenablage.

→ Wie diese drei Notationen zu lesen sind, erklärt **[Anhang A4.1](appendix/a4-symmetry-space-groups/symbols-and-diagrams.md#symmetrieoperationen-registerkarte-operationen)**.

---

## Eigenschaften

![Eigenschaften](../assets/cap-de-auto/FormSymmetryInformation.splitContainer1.panel2.tabControl.tabPageProperties.png)

Berichtet gruppentheoretische Klassifikationen der aktuellen Raumgruppe (Zähligkeit der allgemeinen Lage, Ordnung der Punktgruppe, zentrosymmetrisch, Sohncke, symmorph, polare Richtung, enantiomorpher Partner, Kristallfamilie/Gittersystem/Bravais-Typ, arithmetische Kristallklasse, Patterson-Symmetrie) sowie, welche makroskopischen physikalischen Eigenschaften (pyroelektrisch/ferroelektrisch, piezoelektrisch, Frequenzverdopplung (SHG), optische Aktivität) diese Symmetrie zulässt.

→ Was die einzelnen Begriffe bedeuten, erklärt **[Anhang A4.1](appendix/a4-symmetry-space-groups/symbols-and-diagrams.md#gruppentheoretische-klassifikation-registerkarte-eigenschaften)**.

---

## Aufstellungen

![Aufstellungen](../assets/cap-de-auto/FormSymmetryInformation.splitContainer1.panel2.tabControl.tabPageSettings.png)

Listet zu Referenzzwecken jede tabellierte Ursprungs-/Achsenaufstellung, die die IT-Nummer der aktuellen Raumgruppe teilt, jeweils mit ihrem HM- und Hall-Symbol; die aktuell angezeigte Aufstellung ist markiert. Die Auswahl einer Zeile ändert den Kristall nicht.

---

## Diagramme der Symmetrieelemente & der allgemeinen Lagen

![Diagramme der Symmetrieelemente & der allgemeinen Lagen](../assets/cap-de-auto/FormSymmetryInformation.splitContainer1.tableLayoutPanel1.png)

Die beiden Felder unten geben die schematischen Symmetriediagramme der Raumgruppe in der Notation der *International Tables for Crystallography* Vol. A wieder.

- **Symmetrieelemente (links)**: Dreh-/Schraubenachsen, Spiegel-/Gleitspiegelebenen sowie Inversionszentren/Drehinversionspunkte werden mit den konventionellen graphischen Symbolen gezeichnet.
  - Für das \(F\)-Gitter des kubischen Systems wird nur das obere linke Viertel der projizierten Zelle gezeigt (siehe [Anhang A4.1](appendix/a4-symmetry-space-groups/symbols-and-diagrams.md#symmetry-element-diagram)).
  - Diese Symmetrieelemente können auch direkt auf das 3D-Modell in der [Strukturansicht](5-structure-viewer.md) gezeichnet werden.
- **Allgemeine Lagen (rechts)**: Die allgemeinen äquivalenten Lagen werden als Kreise dargestellt (ein Komma bezeichnet ein Spiegelbild), jeweils beschriftet mit ihrer Höhe entlang der Projektionsachse (`+`, `−`, `½+`, …; siehe [Anhang A4.1](appendix/a4-symmetry-space-groups/symbols-and-diagrams.md)).
  - Der Testpunkt $(x,y,z)$ wird mit den Feldern **a** / **b** / **c** neben der Schaltfläche **Kopieren** der allgemeinen Lage eingestellt (Bereich −1 bis 1, Schrittweite 0,01). Er wird bei jedem Wechsel der Punktgruppe auf einen Standardwert für die Punktgruppe zurückgesetzt und bleibt erhalten, wenn sich nur die Raumgruppe innerhalb derselben Punktgruppe ändert.
  - Nur für das kubische System verbinden Hilfslinien die drei Kreise, die durch eine dreizählige Drehachse miteinander verknüpft sind.

Bedienelemente (das Feld **Optionen** am unteren Rand des Fensters und die Schaltfläche **Kopieren** über jedem Diagramm):

- **Richtung** (`a` / `b` / `c`) : wählt die Kristallachse, entlang derer projiziert wird. Sie ist nur bei orthorhombischen Raumgruppen wählbar (Standard `c`); bei monoklinen Gruppen wird automatisch die ausgezeichnete Achse verwendet, bei allen anderen Kristallsystemen `c`.
- **Kopieren** : kopiert jedes Diagramm in dem mit **Kopierformat** gewählten Format (Vektor-**emf** / Raster-**bmp**) in die Zwischenablage; emf kann in PowerPoint entgruppiert und bearbeitet werden.
- **Gruppenrelationen...** öffnet einen Browser für die Beziehungen der aktuellen Raumgruppe zu ihren maximalen Untergruppen und minimalen Obergruppen. Wie er zu lesen ist, erklärt [Anhang A4.2](appendix/a4-symmetry-space-groups/group-subgroup-relations.md).

---

## Siehe auch

- [Kristalldatenbank](1-crystal-database.md)
- [Strukturansicht](5-structure-viewer.md)
- [Stereonetz](6-stereonet.md)
- [Rotationsgeometrie](4-rotation-geometry.md)
- [Hauptfenster](0-main-window.md)
- [Anhang A4. Symmetrie und Raumgruppen](appendix/a4-symmetry-space-groups/index.md) — der kristallographische und gruppentheoretische Hintergrund hinter jeder Registerkarte und jedem Diagramm dieser Seite.
