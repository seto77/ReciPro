# ReciPro

[![Documentation](https://img.shields.io/badge/%F0%9F%93%96_Documentation-blue)](https://seto77.github.io/ReciPro/de/)
[![Latest Release](https://img.shields.io/github/v/release/seto77/ReciPro?logo=github)](https://github.com/seto77/ReciPro/releases/latest)
[![Total downloads](https://img.shields.io/github/downloads/seto77/ReciPro/total?logo=github&label=GitHub%20downloads)](https://github.com/seto77/ReciPro/releases)
[![GitHub Stars](https://img.shields.io/github/stars/seto77/ReciPro?style=social)](https://github.com/seto77/ReciPro/stargazers)
[![GitHub Forks](https://img.shields.io/github/forks/seto77/ReciPro?style=social)](https://github.com/seto77/ReciPro/forks)
[![License: MIT](https://img.shields.io/badge/License-MIT-green)](https://github.com/seto77/ReciPro/blob/master/LICENSE.md)

<!-- 260804Cl: Übersetzung von ../../README.md (Englisch). Bei Änderungen am englischen Original bitte auch diese Datei aktualisieren. -->
[English](../../README.md) | [日本語](README.ja.md) | **Deutsch** | [Français](README.fr.md) | [Español](README.es.md) | [Italiano](README.it.md) | [Русский](README.ru.md) | [简体中文](README.zh-Hans.md) | [繁體中文](README.zh-Hant.md) | [한국어](README.ko.md) | [Português](README.pt.md)

*ReciPro* ist eine kostenlose, quelloffene und GUI-basierte kristallographische Mehrzwecksoftware. Sie bietet nahtlosen Zugriff auf Funktionen zum Durchsuchen von Kristalldatenbanken, zur Visualisierung von Kristallstrukturen und Goniometereinstellungen, zur Simulation von Beugungsbildern und hochauflösenden mikroskopischen Abbildungen sowie zur Auswertung von Beugungsdaten. Diese Funktionen sind über eine benutzerfreundliche GUI miteinander verknüpft, und die Ergebnisse werden nahezu in Echtzeit synchron dargestellt. *ReciPro* unterstützt ein breites Spektrum von Kristallographinnen und Kristallographen (auch Einsteiger), die mit Röntgen-, Elektronen- und Neutronenbeugung sowie TEM arbeiten.

*ReciPro* wird seit 2002 kontinuierlich weiterentwickelt und ist seit März 2020 auf GitHub verfügbar. Es wurde über 27.000-mal von GitHub heruntergeladen und wird von Hunderten Anwenderinnen und Anwendern in mehr als einem Dutzend Laboren an Universitäten und in Unternehmen eingesetzt.

***[Im Handbuch erfahren Sie, wie Sie ReciPro verwenden!](https://seto77.github.io/ReciPro/de/)***

[Verschiedene Simulationen in Echtzeit (Beispiel: MgAl2O4)](https://github.com/user-attachments/assets/6b0234dd-f2d6-49db-b146-bb74cf6021b6)

## Autoren

*ReciPro* wird von [Seto Y.](https://yseto.net/en/home-e) und [Ohtsuka M.](https://researchmap.jp/7000002999?lang=en) entwickelt. Die Funktionen und Algorithmen werden in [der Publikation](https://github.com/seto77/ReciPro/blob/master/docs/ReciProSetoOhtsuka2022.pdf) vorgestellt.

## Zitieren

Wenn Sie *ReciPro* in wissenschaftlichen Arbeiten verwenden, nutzen Sie bitte den Link **Cite this repository** auf der GitHub-Repository-Seite. Die Zitationsmetadaten werden über `CITATION.cff` bereitgestellt; die bevorzugte Zitation ist der folgende Artikel:

  * [Seto, Y. & Ohtsuka, M. (2022). *J. Appl. Cryst.* **55**, 397-410, doi: 10.1107/S1600576722000139.](https://doi.org/10.1107/S1600576722000139)

Sie können bei Bedarf auch das Software-Repository selbst zitieren:

  * Repository: https://github.com/seto77/ReciPro
  * Releases: https://github.com/seto77/ReciPro/releases/latest

***

## Installation

* Laden Sie [*ReciPro-setup.msi*](https://github.com/seto77/ReciPro/releases/latest/download/ReciPro-setup.msi) herunter (direkter Link zur neuesten Version) und führen Sie die Datei aus. Sie finden sie auch auf der [Releases-Seite](https://github.com/seto77/ReciPro/releases/latest). (Bis v.4.939 hieß der Installer *ReciProSetup.msi*.)
* *ReciPro* läuft unter Windows mit ***.Net Desktop Runtime 10.0*** (NICHT ***.Net Runtime 10.0***); diese kann [hier](https://dotnet.microsoft.com/download/dotnet/10.0) installiert werden.
* Falls Sie keinen Installer ausführen können (z. B. auf PCs mit eingeschränkten Rechten), steht auf der Releases-Seite auch ein **portables ZIP-Paket** (*ReciPro-v.X.XXX.zip*) bereit: eigenständig, ohne Installation und ohne .NET-Runtime — einfach entpacken und starten.
* *ReciPro* wird unter der **MIT-Lizenz** vertrieben (frei für alle zur Nutzung, Änderung und Weitergabe).
* Informationen zur Codesignatur und zur Überprüfung des Installers finden Sie unter [Codesignatur-Richtlinie](../../CODE_SIGNING.md).
* Mitgelieferte oder referenzierte Komponenten und Daten Dritter sind in den [Third-party notices](../../THIRD-PARTY-NOTICES.md) aufgeführt.

### macOS (inoffiziell)

* *ReciPro* unterstützt offiziell nur Windows, es wurde jedoch berichtet, dass es unter macOS (Apple Silicon) läuft, wenn man das **portable ZIP-Paket** mit dem Wine-Wrapper **Sikarugir** und dem OpenGL-Treiber **Mesa3D** kombiniert — ohne Windows-Lizenz und ohne virtuelle Maschine.
* Siehe die Schritt-für-Schritt-Anleitung von Ryo Fukushima (JAMSTEC): https://github.com/Ryo-fkushima/ReciPro_macOS_memo
* Diese Konfiguration wird nicht offiziell unterstützt und ist nicht vollständig verifiziert. Eine bekannte Einschränkung ist, dass einige Zeichen (Å, hochgestellte Zeichen, Pfeile) fehlerhaft dargestellt werden können.
* Die fehlerhaften Zeichen lassen sich beheben, indem Schriftarten mit großem Glyphenumfang (**DejaVu Sans/Serif** sowie **Noto Sans CJK JP** für die japanische Oberfläche) im Wine-Prefix installiert werden — ReciPro erkennt die Wine-Umgebung und wechselt automatisch zu diesen Schriften. Einzelheiten siehe [Fehlerbehebung](https://seto77.github.io/ReciPro/de/troubleshooting/).

### Hinweis zu Windows-Sicherheitswarnungen

* Bitte laden Sie *ReciPro* ausschließlich von der offiziellen GitHub-Releases-Seite herunter: https://github.com/seto77/ReciPro/releases/latest
* Auf manchen Windows-Systemen zeigen Microsoft Defender SmartScreen oder Smart App Control vor dem Ausführen des Installers eine Warnung an. Das kommt bei neu erstellter oder nur eng verbreiteter Forschungssoftware vor und bedeutet nicht zwangsläufig, dass der Installer schädlich ist.
* Wenn Sie den heruntergeladenen Installer selbst prüfen möchten, können Sie ihn mit einem Mehr-Engine-Dienst wie VirusTotal scannen.

## Codesignatur-Richtlinie

[<img src="https://signpath.org/assets/favicon-50x50.png" alt="SignPath" height="20">](https://about.signpath.io/) Kostenlose Codesignatur unter Windows bereitgestellt von [SignPath.io](https://about.signpath.io/), Zertifikat von der [SignPath Foundation](https://signpath.org/).

Seit v.4.942 werden die Release-Artefakte (der Installer *ReciPro-setup.msi* und die portable *ReciPro.exe*) im Rahmen der automatisierten Release-Pipeline mit Windows Authenticode signiert; jede Signaturanforderung wird vor der Veröffentlichung vom Maintainer geprüft und manuell freigegeben. Die vollständige Richtlinie — einschließlich Signaturumfang, Überprüfung eines Installers und Meldung verdächtiger Artefakte — finden Sie in [CODE_SIGNING.md](../../CODE_SIGNING.md).

## Datenschutz

*ReciPro* ist eine lokale Desktop-Anwendung. Sie erfasst, speichert oder überträgt **keine** personenbezogenen Daten oder Nutzungsdaten und enthält weder Telemetrie noch Analysefunktionen. Nach der Installation läuft sie vollständig offline.

Die einzigen Netzwerkverbindungen von *ReciPro* sind optionale, von der Nutzerin bzw. dem Nutzer angestoßene Downloads; keine davon lädt Ihre Daten hoch:

* **Auf Updates prüfen** (Menübefehl): vergleicht Ihre installierte Version mit dem neuesten GitHub-Release und lädt auf Wunsch den neuen Installer von der offiziellen Seite [GitHub Releases](https://github.com/seto77/ReciPro/releases/latest) herunter.
* **COD-Datenbank** (Crystallography Open Database): wird bei der ersten Verwendung (~880 MB) vom GitHub-Spiegel des Autors heruntergeladen und danach offline genutzt.
* **Intel-MKL-Bibliothek** (optionale Beschleunigung): wird nur bei aktivierter Option *Use MKL* von [nuget.org](https://www.nuget.org/) heruntergeladen (~55 MB), um Berechnungen zur dynamischen Beugung zu beschleunigen.

Die mitgelieferte AMCSD-Datenbank und alle Kernfunktionen arbeiten vollständig offline.

## Handbuch
  * Online-Handbuch (Englisch / Japanisch): https://seto77.github.io/ReciPro/de/
  * Japanische Version: https://yseto.net/soft/recipro
***

## Hauptfunktionen

### Kristalldatenbank

* **AMCSD** (American Mineralogist Crystal Structure Database): Über 21.000 Kristallstrukturen sind integriert und unmittelbar nach der Installation verfügbar.
  * Die Datenbank ist stark komprimiert (~5 MB) und in der Installationsdatei enthalten, sodass sie auch in Offline-Umgebungen zur Verfügung steht.
  * Kristalle lassen sich nach Name, chemischer Zusammensetzung, Gitterparametern, Dichte, Symmetrie und enthaltenen Elementen suchen.
  * Referenz: [Downs & Hall-Wallace, 2003, *American Mineralogist* **88**, 247-250](https://www.geo.arizona.edu/xtal/group/pdf/am88_247.pdf)
* **COD** (Crystallography Open Database): ~525.000 Kristallstrukturen einschließlich organischer Kristalle sind ebenfalls verfügbar.
  * Wird bei der ersten Verwendung automatisch heruntergeladen (~880 MB) und ist danach offline nutzbar.
  * Referenzen: [Gražulis et al., 2009, *J. Appl. Cryst.* **42**, 726-729](https://doi.org/10.1107/S0021889809016690); [Gražulis et al., 2012, *Nucleic Acids Res.* **40**, D420-D427](https://doi.org/10.1093/nar/gkr900)
* Import und Export von Dateien im CIF- und AMC-Format.

### Kristallographische Berechnungen

* 530 Raumgruppen-Notationen werden unterstützt: 230 standardisierte ITA-Aufstellungen + 300 nicht standardisierte Achsenaufstellungen.
  * Allgemeine Bedingungen (Auslöschungsregeln), Wyckoff-Positionen und Multiplizitäten aller Raumgruppen.
  * Geometrische Berechnung von Periodizität und/oder Winkeln zwischen Ebenen und/oder Achsen.
  * Erzeugung äquivalenter Atompositionen.
  * Einfache Umrechnung zwischen nicht standardisierten Achsenaufstellungen (z. B. *Pbnm* nach *Pnma*) und Ursprungsverschiebungen.

### Atomare Eigenschaften

* Wellenlängen/Energien charakteristischer Röntgenstrahlung für <sup>1</sup>H bis <sup>98</sup>Cf.
* Atomare Streufaktoren für Röntgen-, Elektronen- und Neutronenstrahlung.

### Strukturbetrachter

* 3D-Visualisierung von Kristallstrukturen auf Basis von OpenGL (GLSL).
  * Darstellung von Atomen, Bindungen, Koordinationspolyedern, Elementarzellen, Netzebenen, Begrenzungsflächen und Legendenbeschriftungen.
  * Selbst komplexe Kristallstrukturen mit Zehntausenden Atomen werden flüssig in Echtzeit gezeichnet.
  * Die voreingestellten Farben und Größen der Atome sind mit VESTA kompatibel.
  * Der Darstellungsbereich lässt sich über Vielfache der Elementarzelle oder über Kristallflächenindizes und den Abstand vom Zentrum festlegen.
  * Beliebige Kristalltrachten können durch Einfärben der Begrenzungsflächen dargestellt werden.
  * Beliebige Netzebenen können angezeigt werden, was Einsteigern hilft, das Konzept der Netzebenen bei Beugungsphänomenen zu verstehen.
  * Drehen, Verschieben und Zoomen werden frei mit der Maus gesteuert.
  * Ein Klick auf ein Atom zeigt Abstände und Bindungswinkel zu den Nachbaratomen an.
  * Der Rotationszustand wird unmittelbar in anderen Funktionsfenstern (Stereogramm, Beugungssimulator usw.) übernommen.
  * Ein integrierter Video-Encoder (Windows Media Foundation) kann Rotationsanimationen (H.264/H.265 MP4) für Präsentationen erzeugen.

### Stereogramm

* Stellt Kristallflächen und Kristallachsen in stereographischer Projektion dar.
  * Sowohl winkeltreue (Wulffsches Netz) als auch flächentreue (Schmidtsches Netz) Projektionen werden unterstützt, mit Breiten- und Längenkreisen.
  * Indizes können über Zahlenbereiche oder konkrete Werte angegeben werden.
  * Großkreise lassen sich durch Angabe von Zonenachsen darstellen.
  * Zeichnungsobjekte können im Vektorformat gespeichert oder kopiert und später ohne Auflösungsverlust bearbeitet werden.
  * 3D-Visualisierung der Geometrie der stereographischen Projektion zu Lehrzwecken.

### Beugungssimulator

* Simuliert Einkristall-Beugungsbilder für Röntgen-, Elektronen- und Neutronenstrahlung.
  * Die kinetische Energie des einfallenden Strahls ist frei konfigurierbar.
  * Charakteristische Röntgenenergien von <sup>1</sup>H bis <sup>98</sup>Cf sind integriert.
  * Der Darstellungsbereich wird über die Bildauflösung (Pixelgröße) und die Kameralänge festgelegt.
  * Auch geneigte Detektorgeometrien werden unterstützt.
  * Das Überlagern experimentell aufgenommener Bilder wird unterstützt.
  * Die Kristallrotation (Beugungsbedingung) ist steuerbar und wird sofort mit anderen Fenstern synchronisiert.

* **Polykristalline Beugung**: Simulation von Debye-Ringen unter Annahme einer polykristallinen Probe.
* **Präzessionskamera** (Röntgen): Simulation von Präzessionskamera-Aufnahmen der Laue-Zone nullter Ordnung.
* **Rückstrahl-Laue-Kamera** (Röntgen): Simulation von Rückstrahl-Laue-Aufnahmen.

#### Kinematische Beugungstheorie
* Für alle Strahlquellen verfügbar (Röntgen, Elektronen, Neutronen).
* Die Beugungsintensitäten werden aus dem Betragsquadrat des Kristallstrukturfaktors und dem Anregungsfehler abgeschätzt.
* Der Einfluss des Debye-Waller-Faktors auf die Beugungsintensitäten ist berücksichtigt.

#### Dynamische Beugungstheorie (Elektronen)
* Basiert auf der **Blochwellen-Methode** (Bethe, 1928), die flexible Kristallorientierungen ohne Beschränkung auf niedrigindizierte Zonenachsen erlaubt.
* Zwei Berechnungsansätze stehen zur Verfügung:
  * **Bethe-Eigenwertmethode**: Matrixdiagonalisierung für Eigenwerte/Eigenvektoren der Bloch-Eigenzustände. Geeignet, wenn die Probendicke variiert wird.
  * **Streumatrix-Methode**: Direkte Berechnung von Matrixexponentialen mit dem Scaling-and-Squaring-Verfahren und Padé-Approximation. Geeignet für schnelle Berechnungen bei einer einzelnen Dicke.
* Der schnellste Algorithmus und die jeweils beste mathematische Bibliothek (Eigen, Intel MKL oder Math.NET) werden automatisch ausgewählt.
* Das Absorptionspotential der thermisch diffusen Streuung (TDS) wird aus Performance-Gründen analytisch berechnet.

* **SAED** (Feinbereichs-Elektronenbeugung): Simulation der Elektronenbeugung mit parallelem Strahl unter Berücksichtigung dynamischer Streueffekte.
* **PED** (Präzessions-Elektronenbeugung): Simuliert PED-Aufnahmen durch Angabe von Präzessionswinkel und azimutaler Winkelauflösung. Nützlich für die Kristallstrukturanalyse und die Optimierung quasi-kinematischer PED-Bedingungen.
* **CBED** (Konvergente Elektronenbeugung): Simuliert CBED-Aufnahmen mit frei wählbarem Konvergenz-Halbwinkel und Unterteilungszahl. Simulationen über die Dicke hinweg zur Bestimmung der Probendicke werden unterstützt.
  * Positionsgemittelte CBED-Aufnahmen (PACBED).
  * Großwinkel-CBED-Simulation (LA-CBED).

### HRTEM-Simulator

* Simulation hochauflösender transmissionselektronenmikroskopischer Bilder im selben theoretischen Rahmen der Blochwellen.
* Optische Parameter (Beschleunigungsspannung, Koeffizient der sphärischen Aberration, Defokus, Probendicke usw.) werden über die GUI eingestellt.
* Typische Voreinstellungen für TEM-Optikparameter sind integriert und per Rechtsklick abrufbar.
* Zwei Abbildungsmodelle für partielle Kohärenz:
  * **Lineare Kontrastübertragungstheorie**: geringerer Rechenaufwand; geeignet für dünne Proben, die die Näherung des schwachen Phasenobjekts erfüllen.
  * **Nichtlineare Kontrastübertragungstheorie (TCC-Modell)**: basiert auf dem Transmissionskreuzkoeffizienten erster Ordnung (Ishizuka, 1980); zuverlässig auch für dickere Proben und Materialien mit höherer Ordnungszahl.
* Die Kontrastübertragungsfunktion mit Einhüllenden kann geplottet werden.
* Bildserien über Dicke und Defokus können gleichzeitig berechnet werden.
* Unter Standardbedingungen ist die Rechnung typischerweise in weniger als einer Sekunde abgeschlossen.

### STEM-Simulator

* Simulation von Bildern der Rastertransmissionselektronenmikroskopie.
  * Abbildungsmodi Hellfeld (BF), Ringdunkelfeld (ADF) und Hochwinkel-ADF (HAADF).
  * Der konvergente Strahl wird als Überlagerung vieler ebener Wellen mit exakter Überlappungsberechnung behandelt.
  * Inelastisch gestreute Elektronen werden über das absorptive Potentialmodell berechnet.
  * Bildserien über Dicke und Defokus können erzeugt werden.

### Spot ID

* Halbautomatische Indizierung von Beugungsreflexen in experimentellen SAED-Aufnahmen.
* **Spot ID v1**: Sucht Zonenachsen anhand der geometrischen Anordnung (Abstände und Winkel) der Beugungsreflexe. Unterstützt die gleichzeitige Auswertung von 2–3 Bildern.
* **Spot ID v2**: Importiert SAED-Aufnahmen direkt.
  * Unterstützt gängige Bildformate: TIFF (.tif), Digital Micrograph 3/4 (.dm3, .dm4) und weitere.
  * Automatische Erkennung und Anpassung der Beugungsreflexe mit 2D-Pseudo-Voigt-Funktionen.
  * Erschöpfende Suche nach Kristallorientierungen, die zur Anordnung der reziproken Gittervektoren passen.
  * Präzise Bestimmung selbst hochindizierter Zonenachsen.

### Rotationsgeometrie (Goniometer)

* Verknüpft die Euler-Winkel in ReciPro mit dem Goniometer im Labor.
* Gibt an, wie das Goniometer gedreht werden muss, um die gewünschte Kristallorientierung (z. B. eine niedrigindizierte Zonenachse) zu erreichen.
* Unterstützt beliebige Goniometer-Definitionen.

### Makro

* Makro-Skripte in Python-Syntax zur Automatisierung von Arbeitsabläufen.
  * Beispiel: einen Kristall in 1°-Schritten drehen und bei jedem Schritt Beugungsbilder oder STEM-Bilder speichern.
  * ReciPro-spezifische Funktionen stehen im Namensraum "ReciPro" zur Verfügung.
  * Anwendungsbeispiele finden Sie im [Handbuch](https://seto77.github.io/ReciPro/de/20-macro/2-examples/).

### Weitere Funktionen

* **Elektronenreichweiten-Simulator**: Monte-Carlo-Simulation der Elektronenreichweite in Materialien.
* **EBSD** (Elektronenrückstreubeugung): in Entwicklung.

## Technische Details

* Geschrieben in **C++**, **C#** und **OpenGL Shading Language (GLSL)**.
* Multithreading-Parallelisierung für leistungsstarke Berechnungen auf modernen Many-Core-CPUs.
* Alle Funktionsfenster werden bei einer Änderung der Kristallorientierung synchron in Echtzeit aktualisiert.
* Verwendet ein rechtshändiges kartesisches Koordinatensystem (X: rechts, Y: oben, Z: vorne) mit der Euler-Winkel-Konvention Z–X–Z.
* Die Koordinatendefinitionen sind mit der EBSD-Software von Thermo Fisher Scientific kompatibel.

### Wissenschaftliche Wirkung

* **Begutachtete Software-Publikation:** [Seto, Y. & Ohtsuka, M. (2022), *Journal of Applied Crystallography*, **55**, 397-410](https://doi.org/10.1107/S1600576722000139).
* **Zitierende Arbeiten:** [Zitierende Artikel bei Google Scholar](https://scholar.google.jp/scholar?cites=12625594477623342627).
* **Aufmerksamkeit für den Artikel:** [Altmetric-Details](https://www.altmetric.com/details/123778746).

| Kennzahl | Wert |
| --- | --- |
| GitHub-Downloads insgesamt | 27.000+ Downloads |
| Zitationen bei Google Scholar | 170+ Zitationen |
| Zitationen bei Dimensions | 160+ Zitationen |
| Leser bei Mendeley | 90+ Leser |

## Screenshots

<img src="https://seto77.github.io/ReciPro/assets/cap-de-auto/FormMain.png" height="320px" alt="Hauptfenster">
<img src="https://seto77.github.io/ReciPro/assets/cap-de-auto/FormCrystalDatabase.png" height="320px" alt="Kristalldatenbank">
<img src="https://seto77.github.io/ReciPro/assets/cap-de-auto/FormSymmetryInformation.png" height="320px" alt="Symmetrieinformationen">
<img src="https://seto77.github.io/ReciPro/assets/cap-de-auto/FormBeamInteraction.png" height="320px" alt="Strahl-Wechselwirkung">
<img src="https://seto77.github.io/ReciPro/assets/cap-de-auto/FormStructureViewer.png" height="320px" alt="Strukturbetrachter">
<img src="https://seto77.github.io/ReciPro/assets/cap-de-auto/FormStereonet.png" height="320px" alt="Stereogramm">
<img src="https://seto77.github.io/ReciPro/assets/cap-de-auto/FormDiffractionSimulator.png" height="320px" alt="Beugungssimulator">
<img src="https://seto77.github.io/ReciPro/assets/cap-de-auto/FormImageSimulator.png" height="320px" alt="HRTEM/STEM-Simulator">
<img src="https://seto77.github.io/ReciPro/assets/cap-de-auto/FormSpotIDV2.png" height="320px" alt="Spot ID v2">
<img src="https://seto77.github.io/ReciPro/assets/cap-de-auto/FormMacro.png" height="320px" alt="Makro">
<img src="https://seto77.github.io/ReciPro/assets/cap-de-auto/FormTrajectory.png" height="320px" alt="Elektronenreichweiten-Simulator">

***
