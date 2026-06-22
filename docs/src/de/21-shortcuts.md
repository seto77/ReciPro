# Tastatur- & Maus-Kurzbefehle

ReciPro verknüpft viele Funktionen mit **Tastenkombinationen** und mit **Maustasten in Verbindung mit Modifikatortasten** — Dinge, die auf einer Schaltfläche oder in einem Menü nicht sichtbar sind. Diese Seite fasst sie alle an einem Ort zusammen. Die Seite jedes Fensters wiederholt seine Kurzbefehle zusätzlich nahe dem Anfang.

<kbd>F1</kbd> funktioniert in **jedem** Fenster und öffnet die zugehörige Seite dieses Online-Handbuchs.

---

## Anwendungsweite Kurzbefehle

Diese werden vom [Hauptfenster](0-main-window.md) eingerichtet, bleiben aber aktiv, während die Fenster Strukturansicht, Stereonetz, Beugungssimulator, Spot ID und Rechner den Fokus haben.

| Kurzbefehl | Aktion |
|----------|--------|
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>D</kbd> | Beugungssimulator ein-/ausschalten |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>V</kbd> | Strukturansicht ein-/ausschalten |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>S</kbd> | Stereonetz ein-/ausschalten |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>T</kbd> | Spot ID ein-/ausschalten |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd> + Pfeiltasten | Den Kristall einen Schritt in diese Richtung drehen (zwei Pfeile gleichzeitig für eine Diagonale) |
| Doppeltippen auf <kbd>CTRL</kbd> | Rechner ein-/ausschalten |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>R</kbd> | Das *Reserved*-Kennzeichen des gewählten Kristalls umschalten |
| <kbd>CTRL</kbd>+<kbd>ALT</kbd>+<kbd>SHIFT</kbd>+<kbd>C</kbd> | Einen Screenshot der GUI aufnehmen (Entwicklerwerkzeug; zuerst **Capture GUI Components** aktivieren) |
| <kbd>CTRL</kbd> beim Start von ReciPro gedrückt halten | Mit deaktiviertem OpenGL starten (Wiederherstellung bei Grafikproblemen) |

---

## Gemeinsame Interaktionsmodelle

Fast jede interaktive Ansicht in ReciPro gehört zu einer von drei Familien. Wenn Sie die Familie kennen, kennen Sie das Drag-/Zoom-Verhalten, ohne sich jedes Fenster zu merken.

### 3-D-OpenGL-Ansichten { #3d }

Verwendet von der [Strukturansicht](5-structure-viewer.md), der [Rotationsgeometrie](4-rotation-geometry.md), der 3-D-Kugel des [Stereonetzes](6-stereonet.md), den [Elektronenbahnen](8-electron-trajectory.md) und den Geometrie-/Master-Pattern-Ansichten von [EBSD](12-ebsd-simulation.md).

| Aktion | Ergebnis |
|--------|--------|
| Linksziehen | Drehen — Trackball nahe der Mitte, in-plane-Rollen nahe dem Rand |
| Rechtsziehen auf/ab oder Mausrad | Zoomen |
| Mittelziehen | Verschieben (nur wo aktiviert) |
| <kbd>CTRL</kbd> + Rechtsziehen auf/ab | Kameradistanz ändern (nur im Perspektivmodus) |
| <kbd>CTRL</kbd> + Rechts-Doppelklick | Zwischen orthografischer / perspektivischer Projektion umschalten |

Einzelne Fenster können Verschieben oder Zoomen abschalten (zum Beispiel ist bei den Elektronenbahnen und den EBSD-3-D-Ansichten das Verschieben deaktiviert).

### Beugungsmuster-Ansichten { #pattern }

Verwendet vom Muster des [Beugungssimulators](7-diffraction-simulator/index.md), dem Kikuchi-Muster von [EBSD](12-ebsd-simulation.md) und dem 2-D-[Stereonetz](6-stereonet.md). Der entscheidende Unterschied zu den 3-D-Ansichten: **Ziehen dreht den Kristall selbst**, nicht nur die Kamera, sodass jedes verknüpfte Fenster zugleich aktualisiert wird.

| Aktion | Ergebnis |
|--------|--------|
| Linksziehen nahe der Mitte | Den Kristall kippen |
| Linksziehen im äußeren Bereich | Den Kristall um die Blick-/Strahlachse drehen |
| Rechtsklick | Herauszoomen |
| Rechtsziehen eines Rahmens | In den gewählten Bereich hineinzoomen |
| Mittelziehen | Verschieben |

Auf diesen Ansichten gibt es **kein** Zoomen per Mausrad.

### Bildansichten { #image }

Verwendet von den Ergebnisbereichen von [HRTEM/STEM](9-hrtem-stem-simulator/index.md), dem Bild von [Spot ID v2](11-spot-id-v2.md) und dem 2-D-Master-Pattern von [EBSD](12-ebsd-simulation.md).

| Aktion | Ergebnis |
|--------|--------|
| Linksziehen / Mittelziehen | Verschieben |
| Mausrad auf / ab | Hineinzoomen (×2) / Herauszoomen (×0.5) am Cursor |
| Rechtsziehen eines Rahmens | In den gewählten Bereich hineinzoomen |
| Rechtsklick / Rechts-Doppelklick | Herauszoomen (×0.5) |

---

## Referenz je Fenster

### 0. Hauptfenster
[Seite öffnen →](0-main-window.md) · zuzüglich der anwendungsweiten Kurzbefehle oben.

| Kurzbefehl | Aktion |
|----------|--------|
| Linksziehen des Orientierungs-Widgets (unten links) | Den Kristall drehen |
| Rechts-Doppelklick auf das Orientierungs-Widget | Das Widget-Bild in die Zwischenablage kopieren |
| Einfachklick / Doppelklick auf eine Funktionsschaltfläche | Dieses Fenster ein-/ausschalten / in den Vordergrund zwingen |
| Rechtsklick auf einen Kristall in der Liste | Kontextmenü (Umbenennen / Duplizieren / Löschen / CIF exportieren…) |
| Doppelklick auf die Beschriftung **Current Index** | Das Max-UVW-Feld ein-/ausblenden |
| Eine Datei ablegen | Eine Kristallliste (`.xml`, `.cdb2`) oder einen Kristall (`.cif`, `.amc`) laden |

### 1. Kristalldatenbank
[Seite öffnen →](1-crystal-database.md)

| Kurzbefehl | Aktion |
|----------|--------|
| <kbd>ENTER</kbd> in einem Suchfeld | Die Suche ausführen |
| Klick auf eine Ergebniszeile | Diesen Kristall laden |
| Klick auf ein Element im Periodensystem-Popup | Seinen Filter durchschalten: ignorieren → muss enthalten → muss ausschließen |

### 2. Symmetrieinformationen · 3. Strahl-Wechselwirkung
Symmetrieinformationen haben keine besonderen Tasten-/Mauskombinationen. In der Strahl-Wechselwirkung lässt sich neben <kbd>F1</kbd> und den **Copy**-Schaltflächen der vertikale Cursor im Diagramm **Scattering factors** ziehen, um den Wert jedes Elements abzulesen.
[Symmetrie →](2-symmetry-information.md) · [Strahl-Wechselwirkung →](3-beam-interaction.md)

### 4. Rotationsgeometrie
[Seite öffnen →](4-rotation-geometry.md) — sechs **verknüpfte** [3-D-Ansichten](#3d); das Drehen einer beliebigen dreht alle sechs zugleich. Bei den kleinen *Axes*-/*Objects*-Ansichten sind Zoom und Verschieben deaktiviert.

### 5. Strukturansicht
[Seite öffnen →](5-structure-viewer.md) — die Hauptansicht ist eine [3-D-Ansicht](#3d).

| Kurzbefehl | Aktion |
|----------|--------|
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>C</kbd> | Das gerenderte Bild in die Zwischenablage kopieren |
| Links-Doppelklick auf ein Atom | Koordinaten, Abstände zu nächsten Nachbarn und Bindungswinkel anzeigen |
| Linksziehen des Kristallachsen-Gizmos | Das Modell drehen (kein in-plane-Drehen) |
| Linksziehen des Licht-Gizmos | Die Beleuchtungsrichtung ändern |

### 6. Stereonetz
[Seite öffnen →](6-stereonet.md) — das 2-D-Netz ist eine [Beugungsmuster-Ansicht](#pattern); die optionale 3-D-Kugel ist eine [3-D-Ansicht](#3d).

| Kurzbefehl | Aktion |
|----------|--------|
| Links-Doppelklick auf das Netz | Zwischen **Plane**- und **Axis**-Projektion umschalten |
| Maus über das Netz bewegen | Die (hkl)/[uvw] unter dem Cursor ablesen |

### 7. Beugungssimulator
[Seite öffnen →](7-diffraction-simulator/index.md) — das Muster ist eine [Beugungsmuster-Ansicht](#pattern) (kein Mausrad-Zoom).

| Kurzbefehl | Aktion |
|----------|--------|
| Links-Doppelklick auf einen Reflex | Reflexdetails anzeigen (Index, *d*, Strukturfaktor, Anregungsfehler) |
| <kbd>CTRL</kbd> + Mittelziehen | Das Detektorzentrum verschieben (wenn der Detektorbereich angezeigt wird) |
| Rechts-Doppelklick auf die Statusleiste | Eine Textzusammenfassung der aktuellen Einstellungen kopieren |
| Rechts-Doppelklick auf eine aktive Ebenen-Schaltfläche (Spots / Kikuchi / Debye / Scale) | Diese Ebene blinken lassen |
| Links-Doppelklick auf das Stereonetz — Fenster **TEM holder** | Die Halterkippung auf diesen Punkt setzen |
| Pfeiltasten — Fenster **TEM holder** | Die Halterkippung schrittweise ändern (zuvor **Arrow keys** anhaken) |
| `.prm`/Bild ablegen — **Detector geometry**, oder `.txt` — **Dynamic compression** | Diese Daten laden |

### 8. Elektronenbahnen
[Seite öffnen →](8-electron-trajectory.md) — eine [3-D-Ansicht](#3d) mit deaktiviertem Verschieben.

### 9. HRTEM-/STEM-Simulator
[Seite öffnen →](9-hrtem-stem-simulator/index.md) — die Ergebnisbereiche sind [Bildansichten](#image) und verschieben/zoomen gemeinsam.

| Kurzbefehl | Aktion |
|----------|--------|
| <kbd>CTRL</kbd>+<kbd>C</kbd> (Bildraster fokussiert) | Das/die Bild(er) als Metadatei in die Zwischenablage kopieren |
| <kbd>CTRL</kbd> + Rechtsziehen eines Rahmens | Einen rechteckigen Bereich auswählen |
| Links-Doppelklick auf einen Bereich | Diesen Bereich maximieren / das Raster wiederherstellen (Mehrbereich-Layouts) |

### 10. Spot ID v1
[Seite öffnen →](10-spot-id.md) — das Bild dient nur als Referenz (nicht interaktiv).

| Kurzbefehl | Aktion |
|----------|--------|
| Doppelklick auf eine Zeile in der Ergebnisliste | Diesen Kristall auswählen und in die passende Zonenachse drehen |

### 11. Spot ID v2
[Seite öffnen →](11-spot-id-v2.md) — das Bild ist eine [Bildansicht](#image) mit darüberliegender Reflexbearbeitung.

| Kurzbefehl | Aktion |
|----------|--------|
| Links-Doppelklick auf das Bild | Einen Reflex hinzufügen (Peak-angepasst) |
| <kbd>CTRL</kbd> + Links-Doppelklick | Einen Reflex hinzufügen und als direkten (000) Strahl markieren |
| Linksklick auf einen Reflex | Den nächstgelegenen Reflex auswählen |
| <kbd>CTRL</kbd> + Rechtsklick auf einen Reflex | Den nächstgelegenen Reflex löschen |
| <kbd>CTRL</kbd> + Pfeiltasten | Den gewählten Reflex um ein Pixel verschieben |
| Doppelklick auf den Zeilenkopf eines Reflexes | Zu diesem Reflex zoomen (×2) |

### 12. EBSD-Simulation
[Seite öffnen →](12-ebsd-simulation.md) — das Kikuchi-Muster ist eine [Beugungsmuster-Ansicht](#pattern); die 3-D-Ansichten sind [3-D-Ansichten](#3d) (Verschieben aus); das 2-D-Master-Pattern ist eine [Bildansicht](#image).

| Kurzbefehl | Aktion |
|----------|--------|
| Doppelklick auf das Kikuchi-Muster | Die Detektor-Teilzelle unter dem Cursor auswählen und ihre Statistik anzeigen |

### 20. Makro
[Seite öffnen →](20-macro/index.md)

| Kurzbefehl | Aktion |
|----------|--------|
| <kbd>CTRL</kbd>+<kbd>S</kbd> | Den Editortext zurück in den gewählten Makrolisten-Eintrag speichern |
| <kbd>F10</kbd> | Einen Schritt weiter (während der schrittweisen Ausführung) |
| Doppelklick auf eine Zeile in der Funktionshilfe-Liste | Die Signatur dieser Funktion an der Einfügemarke einfügen |
| Eine `.mcr`-Datei ablegen | Sie in den Editor laden |
