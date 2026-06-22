# Fehlerbehebung

Häufige Probleme und Lösungen für ReciPro. Viele der folgenden Einträge stammen aus Fragen und Fehlerberichten im [GitHub-Issue-Tracker](https://github.com/seto77/ReciPro/issues); die Version, in der ein Fehler behoben wurde, ist – wo zutreffend – angegeben.

> **Die meisten Probleme lassen sich einfach durch ein Update auf die [neueste Version](https://github.com/seto77/ReciPro/releases/latest) beheben.** ReciPro wird häufig aktualisiert, und viele der nachstehenden Fehler wurden innerhalb weniger Tage nach ihrer Meldung behoben.

---

## Start und Programmstart

### Symptom: Der Prozess läuft, aber es erscheint kein Fenster

ReciPro startet (es ist im Task-Manager sichtbar), aber sein Fenster wird nie auf dem Bildschirm angezeigt.

**Ursache**: Das Fenster hat sich außerhalb des Bildschirms geöffnet – ein Problem mit den Windows-Anzeigekoordinaten, typischerweise nach einem Monitorwechsel oder einer Änderung der Anzeigeskalierung. (Issues [#50](https://github.com/seto77/ReciPro/issues/50), [#53](https://github.com/seto77/ReciPro/issues/53), [#55](https://github.com/seto77/ReciPro/issues/55))

**Lösung**:

1. Öffnen Sie den **Task-Manager**.
2. Suchen Sie **ReciPro** in der Prozessliste.
3. Klicken Sie mit der rechten Maustaste darauf und wählen Sie **Maximieren**.

Das Fenster wird auf Ihren Hauptbildschirm geholt. Beachten Sie, dass **Wechseln zu**, **In den Vordergrund** und **Minimieren** *nicht* helfen – nur **Maximieren** funktioniert.

### Symptom: ReciPro startet nicht, stürzt ab oder hängt beim Start

**Ursache**: Meist schlägt die OpenGL-Initialisierung fehl, oder ein beschädigter Registry-/Einstellungswert blockiert den Start.

**Lösung** (in dieser Reihenfolge versuchen):

1. **OpenGL deaktivieren**: Halten Sie die **Ctrl**-Taste beim Start von ReciPro gedrückt, um mit deaktiviertem OpenGL zu starten. Neuere Versionen (v4.925 und später) härten die OpenGL-Initialisierung ab, sodass die App auch dann startet, wenn OpenGL fehlschlägt – in diesem Fall sind die 3D-Funktionen deaktiviert, der Rest der App funktioniert aber.
2. **Einstellungen zurücksetzen**: Löschen Sie im Registrierungs-Editor den Schlüssel `HKEY_CURRENT_USER\Software\Crystallography\ReciPro` und starten Sie neu. (Entspricht **Optionen → Registrierung zurücksetzen**.)
3. **Saubere Neuinstallation**: Deinstallieren Sie ReciPro, löschen Sie die folgenden Ordner, falls vorhanden (ersetzen Sie `<user>` durch Ihren Kontonamen), und installieren Sie dann neu:
   - `C:\Users\<user>\AppData\Local\Crystallography Software\ReciPro`
   - `C:\Users\<user>\AppData\Roaming\ReciPro\ReciPro`
4. **Aktualisieren** Sie auf die neueste Version.

Wenn nichts davon hilft, kann die Ursache in der Betriebssystemumgebung selbst liegen; bitte [öffnen Sie ein Issue](https://github.com/seto77/ReciPro/issues) mit Ihren PC-Details (CPU, GPU, Windows-Version).

---

## OpenGL-Probleme

### Symptom: Schwarzer Bildschirm oder Absturz beim Start

**Ursache**: Inkompatible GPU oder Remotedesktop-Umgebung.

**Lösung**:

1. Gehen Sie zu **Optionen → OpenGL deaktivieren (Neustart erforderlich)** (oder halten Sie beim Start **Ctrl** gedrückt).
2. Starten Sie ReciPro neu.
3. Die Strukturansicht und einige 3D-Funktionen verwenden dann Software-Rendering.

### Symptom: Integrierte oder ältere GPU (Intel/AMD) rendert nicht

**Ursache**: Einige integrierte GPUs (z. B. AMD Radeon Vega, Intel UHD) hatten in älteren Builds Probleme mit der OpenGL-Initialisierung. (Issue [#2](https://github.com/seto77/ReciPro/issues/2))

**Lösung**: Aktualisieren Sie auf die neueste Version. Die Anforderung an die OpenGL-Version wurde gesenkt (v4.781), die Initialisierung integrierter GPUs wurde behoben (v4.785), und die Initialisierung wurde weiter abgehärtet, um kontrolliert fehlzuschlagen (v4.925). Auch ein Update Ihrer GPU-Treiber hilft.

### Symptom: Schlechte Renderqualität

**Lösung**: Aktualisieren Sie Ihre GPU-Treiber. Empfohlen wird eine externe (dedizierte) GPU mit OpenGL-1.5-Unterstützung.

---

## .NET-Runtime

### Symptom: Anwendung startet nicht

**Ursache**: Die erforderliche .NET Desktop Runtime ist nicht installiert. Aktuelle Versionen erfordern die **.NET Desktop Runtime 10.0** (ältere Builds: v4.895–v4.91x erforderten 9.0; siehe Issue [#43](https://github.com/seto77/ReciPro/issues/43)).

**Lösung**: Laden Sie sie herunter und installieren Sie sie von <https://dotnet.microsoft.com/download/dotnet/10.0> (wählen Sie die **Desktop Runtime**, x64 für die meisten PCs).

### Symptom: Die Microsoft-Downloadseite ist nicht erreichbar

**Lösung**: Sie können das Runtime-Installationsprogramm direkt herunterladen. Wählen Sie auf der [.NET-10.0-Downloadseite](https://dotnet.microsoft.com/download/dotnet/10.0) die **Windows Desktop Runtime X64** für Ihre Architektur. (Issue [#49](https://github.com/seto77/ReciPro/issues/49))

---

## Installation

### Symptom: Installieren oder Deinstallieren ohne Administratorrechte

**Hinweis**: Administratorrechte sind nicht erforderlich. Verknüpfungen und benutzerspezifische Dateien werden in Ihren eigenen Benutzerordnern abgelegt (z. B. `%AppData%\Microsoft\Windows\Start Menu\Programs\Crystallography Software\` und dem Desktop). (Issue [#38](https://github.com/seto77/ReciPro/issues/38))

---

## Anzeige und Layout

### Symptom: Schaltflächen oder Bereiche sind abgeschnitten / verdeckt, oder das Layout wirkt zerstört

Zum Beispiel ist die Schaltfläche **Peak Identification** in Spot ID v2 verdeckt, oder die Über-Seite und andere Formulare sind in neueren Versionen falsch ausgerichtet. (Issues [#56](https://github.com/seto77/ReciPro/issues/56), [#59](https://github.com/seto77/ReciPro/issues/59))

**Ursache**: Eine Regression bei DPI-Skalierung / UI-Schriftart, die in einigen neueren Builds eingeführt wurde.

**Lösung**:

- Stellen Sie die Windows-**Anzeigeskalierung auf 100 %** (dies stellt das Layout meist wieder her).
- Als schnelle Behelfslösung **ändern Sie die Fenstergröße** (z. B. verkleinern Sie es vertikal), um verdeckte Steuerelemente sichtbar zu machen.
- Aktualisieren Sie auf die neueste Version – Layouts werden schrittweise korrigiert. Sieht ein neuerer Build schlechter aus, ist ein Zurücksetzen auf eine etwas ältere Version (z. B. v4.915) eine vorübergehende Option. Bitte melden Sie verbleibende defekte Formulare.

---

## Dynamische Berechnungen

### Symptom: Sehr langsam oder kein Speicher mehr

**Ursache**: Zu viele Bloch-Wellen oder ein zu großes Bild.

**Lösung**:

- Verringern Sie **No. of Bloch waves** (50–200 reichen für Routineberechnungen meist aus)
- Verwenden Sie den **Eigen**-Löser für ≤ 500 Wellen; **MKL** für > 500 Wellen
- Verringern Sie die Bildauflösung für STEM-Simulationen
- Schließen Sie andere speicherintensive Anwendungen

### Symptom: HAADF-STEM-Bild ist schwarz

**Ursache**: Die atomaren Temperaturfaktoren (B) sind auf null gesetzt.

**Lösung**: Setzen Sie B ≥ 0.5 Å² für alle Atome. Die TDS-Intensität erfordert von null verschiedene Temperaturfaktoren.

---

## Beugungssimulator

### Symptom: Das Beugungsmuster ist leer / es wird nichts gezeichnet

**Ursache**: Meist ist die Ansicht zu weit hineingezoomt, oder die Energie der einfallenden Welle liegt außerhalb des Bereichs. (Issue [#3](https://github.com/seto77/ReciPro/issues/3))

**Lösung**:

- **Klicken Sie mit der linken Maustaste** in den Hauptzeichenbereich, um herauszuzoomen.
- Prüfen Sie die Energie der einfallenden Welle auf der Registerkarte **Wave** (oben links): Röntgen ≈ 1–100 keV, Elektron ≈ 10–1000 keV sind angemessen.

---

## Datei-Ein-/Ausgabe

### Symptom: CIF-Datei lädt nicht

**Lösung**:

- Prüfen Sie, ob die CIF-Datei wohlgeformt ist
- Versuchen Sie, die Datei per Drag & Drop auf den Bereich **Kristallinformation** zu ziehen
- Manche nicht standardkonformen CIF-Erweiterungen werden möglicherweise nicht unterstützt

### Symptom: dm3/dm4-Datei lädt nicht, oder "unable to cast … 'System.Single' to 'System.Double'"

**Ursache**: Es gibt mehrere Varianten des DM3/DM4-Formats, und ältere Builds konnten nicht alle lesen. (Issue [#15](https://github.com/seto77/ReciPro/issues/15))

**Lösung**: Aktualisieren Sie auf die neueste Version – die DM3-Lesekompatibilität wurde in v4.835 verbessert. Lässt sich eine Datei weiterhin nicht laden, [senden Sie sie bitte ein](https://github.com/seto77/ReciPro/issues), damit Unterstützung dafür ergänzt werden kann.

### Symptom: dm3/dm4-Datei zeigt falschen Maßstab

**Lösung**: Überprüfen Sie die Kalibrierung in der originalen Digital-Micrograph-Software. ReciPro liest die eingebetteten Metadaten; sind die Metadaten falsch, setzen Sie Pixelgröße und Kameralänge im Optik-Bereich manuell.

---

## Registry zurücksetzen

Falls Einstellungen beschädigt werden:

1. **Optionen → Registrierung zurücksetzen (nach Neustart)**
2. Starten Sie ReciPro neu – Fensterpositionen, Wellenlänge, Kameralänge usw. werden auf die Standardwerte zurückgesetzt

---

## Häufig gestellte Fragen

### Gibt es eine Mac- (oder Linux-)Version? {#mac-linux}

Es gibt keine offizielle Mac- oder Linux-Version. ReciPro hängt von der **.NET Desktop Runtime** ab, die derzeit nur unter Windows läuft. (Issue [#12](https://github.com/seto77/ReciPro/issues/12))

Es wurde jedoch ein inoffizieller Weg gemeldet, der unter macOS funktioniert: Die Distribution **win-x64 Portable ZIP** (verfügbar auf der [Releases-Seite](https://github.com/seto77/ReciPro/releases/latest)) läuft unter macOS (Apple Silicon) mit dem Wine-Wrapper **Sikarugir** in Kombination mit dem OpenGL-Treiber **Mesa3D** – ohne Windows-Lizenz oder virtuelle Maschine. Eine von einem Nutzer veröffentlichte Schritt-für-Schritt-Anleitung ist unter <https://github.com/Ryo-fkushima/ReciPro_macOS_memo> verfügbar.

Beachten Sie, dass diese Konfiguration nicht offiziell unterstützt oder vollständig verifiziert ist. Eine bekannte Einschränkung ist, dass einige Zeichen (Å, hochgestellte Zeichen, Pfeile) möglicherweise falsch dargestellt werden.

**Behebung der fehlerhaften Zeichen (Å, hochgestellte Zeichen, Pfeile):** Die Ursache ist, dass die Windows-Schriftarten, die ReciPro normalerweise verwendet (Segoe UI, Yu Gothic UI usw.), in der Wine-Umgebung fehlen und Wines integrierte Ersatzschriftarten einige wissenschaftliche Glyphen nicht enthalten. ReciPro wechselt automatisch zu breit abdeckenden Schriftarten, **wenn es erkennt, dass es unter Wine läuft**, sodass die Behebung einfach darin besteht, diese Schriftarten im Wine-Prefix verfügbar zu machen:

1. Installieren Sie **DejaVu Sans** / **DejaVu Serif** (deckt Å, hochgestellte Zeichen, Pfeile, Bruch-Beschriftungen ab) und, für die japanische Oberfläche, **Noto Sans CJK JP** (oder **Noto Sans JP**).
2. Am einfachsten kopieren Sie die heruntergeladenen `.ttf`/`.otf`-Dateien in den Schriftartenordner des Prefix – `…/drive_c/windows/Fonts/` innerhalb des Sikarugir-Wrappers – und starten ReciPro dann neu. (`winetricks` kann einige davon ebenfalls installieren.)
3. Beim Neustart erkennt ReciPro sie automatisch; an ReciPro muss keine Einstellung geändert werden.

Sind die Schriftarten nicht installiert, behält ReciPro seine Standard-Schriftartnamen bei, sodass nichts schlimmer wird – die Zeichen bleiben einfach fehlerhaft.

**Ausblick für diesen Weg – zwei ehrliche Hinweise:**

- Das experimentelle **win-arm64**-ZIP läuft auf aktuellen Macs **nicht**, auch nicht auf Apple Silicon: Die heutigen macOS-Wine-Builds (einschließlich Sikarugir) führen x86_64-Windows-Binärdateien über Rosetta 2 aus und haben keinen Mechanismus, um ARM64-Windows-Binärdateien auszuführen. Verwenden Sie auf einem Mac immer das **win-x64** Portable ZIP.
- Apple stellt Rosetta 2 schrittweise ein. macOS 27 (Herbst 2026) ist als letzte Version mit voller Rosetta-2-Unterstützung angekündigt, sodass der derzeitige Weg über x64 + Rosetta voraussichtlich ab macOS 28 (Herbst 2027) nicht mehr funktioniert. Ein natives ARM64-Wine für macOS wird stromaufwärts entwickelt; falls es zustande kommt, könnte das win-arm64-ZIP der Nachfolger auf dem Mac werden, das lässt sich aber noch nicht versprechen.

### Läuft ReciPro unter Windows on ARM (ARM64)? {#windows-on-arm}

Ja – es gibt zwei Wege:

- **Natives ARM64-Paket (experimentell, empfohlen)**: Ab v4.938 wird ein experimentelles natives ARM64-Portable-Paket (`ReciPro-v.X_arm64.zip`; bis v.4.939 `ReciPro-v.X-arm64.zip` genannt) auf der [Releases-Seite](https://github.com/seto77/ReciPro/releases/latest) veröffentlicht. Es ist self-contained, sodass keine Installation der .NET-Runtime erforderlich ist – entpacken Sie das ZIP in einen für den Benutzer beschreibbaren Ordner und führen Sie `ReciPro.exe` aus. Wenn Windows das heruntergeladene ZIP blockiert (Mark of the Web), klicken Sie mit der rechten Maustaste auf das ZIP → **Eigenschaften** → setzen Sie das Häkchen bei **Zulassen** → **OK**, *bevor* Sie entpacken (oder führen Sie `Unblock-File .\ReciPro-*arm64.zip` in PowerShell aus). Details stehen in der mitgelieferten `README-PORTABLE.txt`.
- **x64-Paket unter Emulation**: Das reguläre MSI-Installationsprogramm und das win-x64 Portable ZIP laufen mit installierter .NET Desktop Runtime (x64) über die integrierte x64-Emulation auch auf ARM64-Windows (bestätigt etwa ab v4.913 mit .NET 10). Aufwendige Berechnungen laufen langsamer als beim nativen Build. (Issue [#47](https://github.com/seto77/ReciPro/issues/47))

Hinweise zum nativen ARM64-Paket:

- Intel MKL existiert nicht für ARM64, daher werden die entsprechenden Löser-Optionen und Menüpunkte ausgeblendet. Dynamische Berechnungen verwenden die mitgelieferte NEON-optimierte native Bibliothek; in repräsentativen Validierungsfällen stimmten ihre Ergebnisse innerhalb der erwarteten Gleitkomma-Toleranz mit dem x64-Build überein.
- 3D-Ansichten (Strukturansicht und verwandte Fenster) können laufen, aber Windows on ARM stellt OpenGL nur über eine Direct3D-12-Übersetzungsschicht (GLOn12 / Mesa) bereit, sodass das 3D-Rendering merklich langsamer ist als auf einem PC mit nativem OpenGL-Treiber – das ist eine Plattformbeschränkung, kein Fehler, und ein nativer ARM64-Build kann daran nichts ändern. Der Transparenzmodus **High quality (Per-Pixel Linked List)** in der Strukturansicht ist auf diesem Treiber-Stack besonders langsam; der Standardmodus **Approximate** wird empfohlen. Falls 3D-Ansichten nicht starten, installieren Sie das "OpenCL, OpenGL, and Vulkan Compatibility Pack" aus dem Microsoft Store.
- Das ARM64-Paket läuft **nicht** unter macOS + Wine (siehe die vorherige Frage). Verwenden Sie auf einem Mac das win-x64 Portable ZIP.

### Wie soll ich ReciPro zitieren?

Verwenden Sie den Link **Cite this repository** auf der [GitHub-Repository-Seite](https://github.com/seto77/ReciPro) (die Metadaten werden von `CITATION.cff` bereitgestellt). Die bevorzugte Zitierung lautet:

> Seto, Y. & Ohtsuka, M. (2022). *J. Appl. Cryst.* **55**, 397–410. doi:[10.1107/S1600576722000139](https://doi.org/10.1107/S1600576722000139)

(Issue [#33](https://github.com/seto77/ReciPro/issues/33))

---

## Fehler melden

Melden Sie Probleme unter: <https://github.com/seto77/ReciPro/issues>

Bitte geben Sie an:

- ReciPro-Versionsnummer
- Schritte zur Reproduktion des Problems
- Etwaige Fehlermeldungen oder Screenshots
