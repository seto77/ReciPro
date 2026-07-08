# Anhang A4. Symmetrie und Raumgruppen

Das Kapitel zum Hauptfenster [2. Symmetrieinformationen](../../2-symmetry-information.md) ist eine Anleitung zur GUI: Es sagt Ihnen, welche Registerkarte was anzeigt und welche Schaltfläche welches Diagramm kopiert. Dieser Anhang versammelt den **kristallographischen und gruppentheoretischen Hintergrund** hinter diesen Tabellen und Bildern — was ein Hermann–Mauguin-Symbol tatsächlich kodiert, wie die Symmetrieelement- und Allgemeine-Lagen-Diagramme im Stil der *International Tables for Crystallography* (ITA) Vol. A zu lesen sind, und was die Obergruppen-/Untergruppentabellen und die Terminologie des Fensters **Gruppenrelationen...** (*translationengleiche*, *klassengleiche*, Konjugationsklasse, Domänen, Zwillingsgesetze, …) bedeuten.

![Symmetrieinformationen](../../../assets/cap-de-auto/FormSymmetryInformation.png)

Zwei Fenster werden behandelt, und die Theorie liest sich am besten in dieser Reihenfolge:

1. **[A4.1. Raumgruppensymbole und Symmetriediagramme](symbols-and-diagrams.md)** — die Hermann–Mauguin-, Schoenflies- und Hall-Symbole; die gruppentheoretische Klassifikation auf der Registerkarte **Eigenschaften** (zentrosymmetrisch, Sohncke, symmorph, polar, arithmetische Kristallklasse, Patterson-Symmetrie, …); die Beschreibung jeder Symmetrieoperation auf der Registerkarte **Operationen** als Koordinatentripel/Seitz-Symbol/geometrischer Typ; und die graphischen Konventionen der Symmetrieelement- und Allgemeine-Lagen-Diagramme am unteren Rand des Fensters [Symmetrieinformationen](../../2-symmetry-information.md).
2. **[A4.2. Gruppe-Untergruppe-Beziehungen](group-subgroup-relations.md)** — was eine *maximale Untergruppe* / *minimale Obergruppe* ist, Hermanns Unterscheidung von *t*- und *k*-Untergruppen und wie jede Registerkarte des Browsers **Gruppenrelationen...** (Diagramm, Matrix, Bahnaufspaltung, Domänen & Zwillinge, Neue Reflexe) zu lesen ist, der aus dem Bereich **Optionen** von Symmetrieinformationen geöffnet wird.

A4.1 steht vorn, weil A4.2 ständig darauf zurückgreift: Jede Untergruppen-/Obergruppenbeziehung ist selbst mit genau denselben Hermann–Mauguin-Symbolen, Seitz-Symbolen und geometrischen Typbezeichnungen (*"3-fold rotation"*, *"c-glide plane"*, *"screw axis"*, …) beschriftet, die dort eingeführt werden.

---

## Umfang und Quellen

ReciPros eingebaute Datenbank umfasst die 230 Raumgruppentypen (mit 530 tabellierten Aufstellungen/Ursprungswahlen) genau so, wie sie in den *International Tables for Crystallography*, **Volume A** (Raumgruppensymmetrie) und **Volume A1** (maximale Untergruppen der Raumgruppen), tabelliert sind. Dieser Anhang erklärt ReciPros *Darstellung* dieser Daten — die Notation, die Diagramme, das Browsing-Werkzeug — und setzt voraus, dass die Leser mit Gittern, Punktgruppen und dem Begriff der Symmetrieoperation bereits auf dem Niveau des Grundstudiums vertraut sind. Er ist kein Ersatz für die ITA selbst, die die maßgebliche Referenz für die tabellierten Daten bleibt (und die ReciPro aus urheberrechtlichen Gründen nicht wörtlich wiedergeben kann — siehe die Registerkarte **Aufstellungen** für ReciPros eigene Liste alternativer Ursprünge/Aufstellungen eines gegebenen Raumgruppentyps).

!!! note "Gruppenrelationen... ist eine aktiv weiterentwickelte Funktion"
    Der Browser **Gruppenrelationen...** (A4.2) berechnet *translationengleiche* (t-) und *klassengleiche* (k-, einschließlich *isomorphe*) Untergruppen und Obergruppen direkt aus den Symmetrieoperationen der Raumgruppe selbst (nicht aus einer vortabellierten Liste), sodass jede angezeigte Beziehung unabhängig verifiziert und nicht aus einer Tabelle abgeschrieben ist. Die verbleibenden Grenzen — z. B. wird die isomorphe Serie nur bis zum Index ≤ 4 aufgezählt — sind unter **Aktuelle Einschränkungen** in A4.2 ausgewiesen.

---

## Siehe auch

- [2. Symmetrieinformationen](../../2-symmetry-information.md) — der GUI-Leitfaden, den dieser Anhang erläutert.
- [A4.1. Raumgruppensymbole und Symmetriediagramme](symbols-and-diagrams.md) · [A4.2. Gruppe-Untergruppe-Beziehungen](group-subgroup-relations.md)
- [Anhang A1. Koordinatensysteme](../a1-coordinate-system/1-orientation.md)
- [Anhang A2. Strahl-Wechselwirkung (festkörperphysikalischer Hintergrund)](../a2-beam-interaction/index.md) — wo die Reflexionsbedingungen (systematischen Auslöschungen) der Raumgruppe in den Strukturfaktor einfließen.
