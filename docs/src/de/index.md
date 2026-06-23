# ReciPro-Handbuch

<!-- 260623Ch: Shared demo movie for all localized top pages. -->
<div class="rp-demo-video" markdown="0">
  <video controls muted playsinline preload="metadata" aria-label="ReciPro demo movie">
    <source src="../assets/Recipro_Demo.mp4" type="video/mp4">
  </video>
</div>

## Kurze Einführung
* ReciPro ist eine MIT-lizenzierte, kostenlose Software, die vielfältige kristallographische Berechnungen und elektronenmikroskopische Simulationen bereitstellt.
* ReciPro wurde seit seiner Veröffentlichung auf GitHub (März 2020) insgesamt mehr als 27.000-mal heruntergeladen und wird von vielen Kristallographen und Elektronenmikroskopikern verwendet.

## Nach Ziel finden

| Ziel | Hier beginnen | Wichtige nächste Schritte |
|------|------------|-----------------|
| Einen Kristall laden und seine Orientierung festlegen | [Hauptfenster](0-main-window.md) | [Rotationsgeometrie](4-rotation-geometry.md), [Anhang A1. Koordinatensysteme](appendix/a1-coordinate-system/1-orientation.md) |
| Eine Kristallstruktur in 3D betrachten | [Strukturansicht](5-structure-viewer.md) | [Symmetrieinformationen](2-symmetry-information.md) |
| SAED-/XRD-/PED-/CBED-Muster berechnen | [Beugungssimulator](7-diffraction-simulator/index.md) | [SAED](7-diffraction-simulator/1-saed-simulation.md), [Röntgenbeugung](7-diffraction-simulator/4-x-ray-neutron-diffraction.md), [PED](7-diffraction-simulator/2-ped-simulation.md), [CBED](7-diffraction-simulator/3-cbed-simulation.md) |
| HRTEM-/STEM-Bilder berechnen | [HRTEM/STEM-Simulator](9-hrtem-stem-simulator/index.md) | [HRTEM](9-hrtem-stem-simulator/1-hrtem-simulation.md), [STEM](9-hrtem-stem-simulator/2-stem-simulation.md) |
| EBSD-Muster simulieren | [EBSD-Simulation](12-ebsd-simulation.md) | [Elektronenbahnen](8-electron-trajectory.md), [Anhang A3. EBSD-Berechnung](appendix/a3-bloch-wave/ebsd.md) |
| Experimentelle Beugungsreflexe indizieren | [Spot ID v1](10-spot-id.md), [Spot ID v2](11-spot-id-v2.md) | [Beugungssimulator](7-diffraction-simulator/index.md) |
| Die Gleichungen der dynamischen Beugung verstehen | [Anhang A3. Bloch-Wellen-Methode](appendix/a3-bloch-wave/index.md) | [Dynamische Berechnung](appendix/a3-bloch-wave/calculation.md), [CBED](appendix/a3-bloch-wave/cbed.md), [STEM](appendix/a3-bloch-wave/stem.md), [EBSD](appendix/a3-bloch-wave/ebsd.md) |

## Funktionen
* **Full GUI** : Alle Operationen erfolgen über eine grafische Oberfläche. Die meisten Datei-Ein-/Ausgaben unterstützen Drag & Drop.
* **Kristallliste** : Mehrere Kristalle gleichzeitig verwalten; es ist nicht nötig, für jeden Kristall ein eigenes Fenster zu öffnen.
* **Raumgruppen-Datenbank** : Integrierte Datenbank mit 230 Raumgruppen aus den International Tables Volume A sowie 530 Hall-Symbolen, mit Symmetrieelementen, Wyckoff-Positionen und Auslöschungsregeln. Symmetrieelemente und allgemeine Positionen können als schematische Diagramme im Stil der *International Tables* Vol. A gezeichnet werden (siehe [2. Symmetrieinformationen](2-symmetry-information.md)).
* **Atomare Informationen** : Streufaktoren (Röntgen, Elektron, Neutron), charakteristische Röntgenenergien, Isotopenverhältnisse usw. für die Elemente H (1) – Cf (98).
* **Flexible Kristalldrehung** : Orientierung über Zonenachsen-/Netzebenen-Indizes oder per Maus-Drag festlegen. Die Miller-Bravais-Notation (4-Index *hkil*) wird für trigonale/hexagonale Systeme unterstützt. Der Rotationszustand wird über alle Simulationsfenster synchronisiert.
* **Beugungssimulation** : Kinematische und dynamische (Bloch-Wellen-Methode) Elektronenbeugung, Röntgenbeugung (einschließlich Präzessions- und Rücklaue-Kameras), Präzessions-Elektronenbeugung (PED) und konvergente Elektronenbeugung (CBED). Eine TEM-Halter-Simulation verknüpft das Beugungsmuster mit den Kippwinkeln des Halters.
* **HRTEM-/STEM-Simulation** : Hochauflösende TEM-Bildsimulation mit Modellen partieller Kohärenz; STEM mit thermisch diffuser Streuung.
* **EBSD & Elektronenbahnen** : EBSD-Mustersimulation und Monte-Carlo-Simulation der Elektronenbahnen (siehe [8. Elektronenbahnen](8-electron-trajectory.md)).
* **Reflex-Indizierung** : Automatische Erkennung, Anpassung und Indizierung von Beugungsreflexen aus experimentellen Bildern (Spot ID v1/v2).
* **Makro** : Makro in Python-Syntax zur Automatisierung von Operationen (siehe [20. Makro](20-macro/index.md)).
* **Helles / dunkles Design** : Die Oberfläche folgt einem wählbaren hellen oder dunklen Farbmodus.

## Systemanforderungen
| Element | Minimum | Empfohlen |
|------|---------|-------------|
| Betriebssystem | Windows mit [.NET Desktop Runtime 10.0](https://dotnet.microsoft.com/download/dotnet/10.0) (Windows on ARM64 unterstützt) | Windows 11 |
| GPU | OpenGL 1.3 | Externe GPU mit OpenGL 4.3 |
| Arbeitsspeicher | – | 16 GB oder mehr |
| CPU | – | 8+ Kerne (für dynamische Berechnungen) |

**Windows on ARM (nativ, experimentell)** : Ein experimentelles natives ARM64-Portable-Paket (`ReciPro-v.X_arm64.zip`, self-contained – keine Installation der .NET-Runtime erforderlich) ist auf der [Releases-Seite](https://github.com/seto77/ReciPro/releases/latest) verfügbar. Die regulären x64-Pakete laufen unter der integrierten Emulation ebenfalls auf ARM64-Windows. Hinweise zur Einrichtung und zu Einschränkungen finden Sie unter [Fehlerbehebung](troubleshooting.md#windows-on-arm).

**macOS (inoffiziell)** : ReciPro unterstützt offiziell nur Windows, es wurde jedoch berichtet, dass das **win-x64**-Portable-ZIP-Paket auf macOS (Apple Silicon) mit dem Wine-Wrapper Sikarugir in Kombination mit dem OpenGL-Treiber Mesa3D läuft. Eine von einem Nutzer veröffentlichte Anleitung ist unter <https://github.com/Ryo-fkushima/ReciPro_macOS_memo> verfügbar. Beachten Sie, dass dieser Weg nicht offiziell unterstützt wird und einige Zeichen (Å, hochgestellte Zeichen, Pfeile) möglicherweise falsch dargestellt werden. Das ARM64-ZIP läuft **nicht** unter macOS + Wine, und der derzeitige Weg über x64 + Rosetta 2 wird voraussichtlich ab macOS 28 (Herbst 2027) nicht mehr funktionieren – Einzelheiten siehe [Fehlerbehebung](troubleshooting.md#mac-linux).

## Verwendung dieses Handbuchs

Dieses GitHub-Pages-Handbuch ist derzeit die maßgebliche Quelle. Nutzen Sie die Navigation auf der linken Seite, um nach Kapiteln zu blättern, oder die Suche in der Kopfzeile, um einen Funktionsnamen oder eine UI-Bezeichnung zu finden. Die alten PDF-Handbücher werden zu Archivzwecken aufbewahrt.

* **Archiviertes PDF (Englisch):** <https://raw.githubusercontent.com/seto77/ReciPro/master/ReciPro/doc/ReciProManual(en).pdf>
* **Archiviertes PDF (Japanisch):** <https://raw.githubusercontent.com/seto77/ReciPro/master/ReciPro/doc/ReciProManual(ja).pdf>

## Schnellstart
1. Von [Releases](https://github.com/seto77/ReciPro/releases/latest) herunterladen und installieren.
2. Wählen Sie einen Kristall aus der integrierten Liste (~80 Kristalle). Sie können auch CIF-Dateien importieren oder [CSManager](https://github.com/seto77/CSManager) verwenden.
3. Rufen Sie Funktionen über das rechte Bedienfeld auf: Strukturansicht, Stereonetz, Beugungssimulator, HRTEM-Simulation usw.
4. Drehen Sie den Kristall per Maus-Drag oder durch Eingabe von Zonenachsen-/Netzebenen-Indizes.

## Zitierung
> Y. Seto, "ReciPro: free and open-source multipurpose crystallographic software integrating a crystal operation interface and diffraction simulators," *J. Appl. Cryst.* **55**, 397–410 (2022). <https://doi.org/10.1107/S1600576722000139>

## Lizenz
ReciPro wird unter der [MIT-Lizenz](https://github.com/seto77/ReciPro/blob/master/LICENSE.md) vertrieben.
