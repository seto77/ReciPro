---
title: Externe Steuerung (Kommandozeile & Named Pipe)
---

# Externe Steuerung (Kommandozeile & Named Pipe)

ReciPro-Makros lassen sich nicht nur aus dem eingebauten Editor ausführen, sondern auch **von außerhalb der Anwendung**. Dafür gibt es zwei Mechanismen:

| Mechanismus | Stil | Typischer Einsatz |
|-----------|-------|-------------|
| **Kommandozeile** (`/m` `/o` `/x`) | Einmalig: ReciPro starten → Makro ausführen → (optional) beenden | Stapelverarbeitung, CI, geplante Aufgaben |
| **Named Pipe** (`ReciPro.Macro.v1`) | Interaktiv: Makros an ein *laufendes* ReciPro senden und die Ergebnisse empfangen | Python-/Jupyter-Skripte, Steuerrechner von Messgeräten, Integration mit anderer Software |

Beide führen das Makro mit derselben Engine aus wie der Editor, sodass die gesamte [integrierte API](1-built-in-functions.md) zur Verfügung steht.

---

## Ausführung über die Kommandozeile

```
ReciPro.exe /m <macro.mcr> [/o <result.txt>] [/x]
```

| Schalter | Bedeutung |
|--------|---------|
| `/m` | Führt nach dem Start die erste unter den Argumenten gefundene vorhandene `*.mcr`-Datei aus. |
| `/o <Datei>` | *Quiet*-Modus: Es werden keine Dialoge angezeigt; die `print()`-Ausgabe des Makros und ein etwaiger Fehler-Traceback werden in `<Datei>` geschrieben (UTF-8). Im Fehlerfall wird der Exit-Code des Prozesses auf **1** gesetzt. |
| `/x` | Schließt ReciPro nach dem Ende des Makros (für den Stapelbetrieb empfohlen — der Exit-Code wird dem Aufrufer erst zurückgegeben, wenn der Prozess endet). |

Ohne `/o` erscheinen Fehler als gewöhnliche Dialogfenster (nützlich beim Entwickeln des Makros). Mit `/o` läuft alles vollständig unbeaufsichtigt: Syntaxfehler, Laufzeitfehler, eine fehlende Makrodatei und sogar ein Fehlschlag beim Schreiben der Ergebnisdatei enden mit Exit-Code 1.

### Beispiel: Batchdatei

```bat
ReciPro.exe /m C:\work\saed_series.mcr /o C:\work\result.txt /x
if errorlevel 1 (
    echo Makro FEHLGESCHLAGEN:
    type C:\work\result.txt
)
```

### Beispiel: PowerShell

```powershell
$p = Start-Process ReciPro.exe -ArgumentList '/m','C:\work\job.mcr','/o','C:\work\result.txt','/x' -Wait -PassThru
if ($p.ExitCode -ne 0) { Get-Content C:\work\result.txt }
```

---

## Named-Pipe-Listener

Während ReciPro läuft, kann es über die Windows-Named-Pipe **`\\.\pipe\ReciPro.Macro.v1`** Makros von anderen Programmen entgegennehmen. Ein Client schreibt ein Makro, ReciPro führt es aus, und der Client liest das Ergebnis zurück — ein einfacher Request/Response-Zyklus, der aus Python, PowerShell, C# oder allem funktioniert, was eine Datei öffnen kann.

### Aktivieren

Der Listener ist **standardmäßig aus**. Aktivieren Sie ihn im Hauptfenster:

**Optionen → Accept external macro commands (named pipe)**

Die Einstellung bleibt über Sitzungen hinweg erhalten.

!!! warning "Sicherheitshinweis"
    Solange der Listener aktiv ist, kann *jeder Prozess desselben Windows-Benutzers* Makrocode innerhalb von ReciPro ausführen. Andere Benutzer (und andere Rechner) werden abgewiesen. Aktivieren Sie ihn nur, wenn Sie die externe Steuerung tatsächlich nutzen.

### Protokoll

| Element | Spezifikation |
|------|---------------|
| Pipe-Name | `\\.\pipe\ReciPro.Macro.v1` (lokaler Rechner, nur derselbe Benutzer) |
| Anfrage | Makro-Quelltext als **UTF-8**, abgeschlossen durch ein einzelnes **NUL-Byte (0x00)**. Maximal 1 MiB, muss innerhalb von 30 s eintreffen. |
| Antwort | **UTF-8-JSON** `{"output":"...","error":"..."}`, danach schließt der Server die Verbindung (bis EOF lesen). |
| `output` | Alles, was das Makro nach stdout/stderr geschrieben hat (`print()` funktioniert hier, anders als im GUI-Editor). |
| `error` | Bei Erfolg leerer String; andernfalls der Python-Traceback, ein Syntaxfehler oder eine Protokollfehlermeldung. |
| Verbindungen | Eine Verbindung = ein Makro. Befehle werden nacheinander in ihrer Reihenfolge ausgeführt. |
| Zustand | Der Python-Namensraum wird **mit dem Makro-Editor geteilt und bleibt zwischen Befehlen erhalten** — in einem Befehl definierte Variablen sind im nächsten sichtbar. |
| Instanzen | Laufen mehrere ReciPro-Prozesse, lauscht nur der erste. |

Die Antwort wird gesendet, **nachdem** das Makro fertig ist; eine lange Simulation bedeutet daher lediglich eine lange Wartezeit auf Client-Seite — die Fertigstellung wird automatisch erkannt.

### Python: minimales Beispiel

Die Client-Seite ist ganz normales CPython — `numpy`, `pandas`, Jupyter, alles ist möglich. Für die Pipe selbst sind keine Pakete nötig:

```python
with open(r'\\.\pipe\ReciPro.Macro.v1', 'r+b', buffering=0) as f:
    f.write('print(ReciPro.CrystalList.Count)'.encode('utf-8') + b'\0')
    print(f.read().decode('utf-8'))     # {"output":"68\r\n","error":""}
```

### Python: eine wiederverwendbare Hilfsfunktion

Alle folgenden Beispiele verwenden diese kleine Funktion:

```python
import json

PIPE = r'\\.\pipe\ReciPro.Macro.v1'

def recipro(code):
    """Führt ein IronPython-Makro in ReciPro aus und gibt dessen Ausgabe als str zurück."""
    with open(PIPE, 'r+b', buffering=0) as f:
        f.write(code.encode('utf-8') + b'\0')
        res = json.loads(f.read().decode('utf-8'))
    if res['error']:
        raise RuntimeError(res['error'])
    return res['output']
```

### Python: eine CIF laden und ein SAED-Muster speichern

```python
recipro('''
ReciPro.File.ReadCrystal(r"C:\\data\\rutile.cif")
ReciPro.DifSim.Open()
ReciPro.DifSim.Source_Electron()
ReciPro.DifSim.Energy = 200
ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
ReciPro.DifSim.SaveAsPng(r"C:\\data\\rutile_001.png")
''')
```

### Python: Stapelverarbeitung vieler CIF-Dateien

```python
import glob, os

recipro('ReciPro.DifSim.Open(); ReciPro.DifSim.Source_Electron(); ReciPro.DifSim.Energy = 200')

for cif in glob.glob(r'C:\data\*.cif'):
    png = os.path.splitext(cif)[0] + '_SAED.png'
    recipro(f'''
ReciPro.File.ReadCrystal(r"{cif}")
ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
ReciPro.DifSim.SaveAsPng(r"{png}")
''')
    print('done:', cif)
```

### Python: Kippserie

```python
for i in range(10):
    recipro(f'''
ReciPro.Dir.RotateAroundAxisInDeg(1, 0, 0, 1)
ReciPro.DifSim.SaveAsPng(r"C:\\data\\tilt_{i:02d}.png")
''')
```

### Python: Reflexdaten nach pandas

`ReciPro.DifSim.SpotInfo()` liefert eine CSV-Zeichenkette; geben Sie sie mit `print()` aus und parsen Sie sie auf der Client-Seite:

```python
import io, pandas as pd

csv_text = recipro('print(ReciPro.DifSim.SpotInfo())')
df = pd.read_csv(io.StringIO(csv_text))
print(df.head())
```

### Python: der Zustand bleibt zwischen Befehlen erhalten

```python
recipro('n = ReciPro.CrystalList.Count')   # eine Variable definieren...
print(recipro('print(n * 2)'))             # ...und in einem späteren Befehl verwenden
```

### PowerShell

```powershell
function Invoke-ReciProMacro([string]$Code) {
    $pipe = [System.IO.Pipes.NamedPipeClientStream]::new('.', 'ReciPro.Macro.v1', [System.IO.Pipes.PipeDirection]::InOut)
    $pipe.Connect(5000)
    $bytes = [System.Text.Encoding]::UTF8.GetBytes($Code) + [byte[]]@(0)
    $pipe.Write($bytes, 0, $bytes.Length); $pipe.Flush()
    $ms = [System.IO.MemoryStream]::new(); $buf = New-Object byte[] 4096
    while (($n = $pipe.Read($buf, 0, $buf.Length)) -gt 0) { $ms.Write($buf, 0, $n) }
    $pipe.Dispose()
    [System.Text.Encoding]::UTF8.GetString($ms.ToArray()) | ConvertFrom-Json
}

$r = Invoke-ReciProMacro 'print(ReciPro.Crystal.Name)'
$r.output      # ausgegebener Text
$r.error       # bei Erfolg leer
```

### C#

```csharp
using System.IO.Pipes;
using System.Text;
using System.Text.Json;

static string ReciPro(string code)
{
    using var pipe = new NamedPipeClientStream(".", "ReciPro.Macro.v1", PipeDirection.InOut);
    pipe.Connect(5000);
    var req = Encoding.UTF8.GetBytes(code + "\0");
    pipe.Write(req, 0, req.Length);
    using var ms = new MemoryStream();
    pipe.CopyTo(ms);
    var res = JsonSerializer.Deserialize<JsonElement>(ms.ToArray());
    var error = res.GetProperty("error").GetString();
    if (error!.Length > 0) throw new InvalidOperationException(error);
    return res.GetProperty("output").GetString()!;
}

Console.WriteLine(ReciPro("print(ReciPro.CrystalList.Count)"));
```

### Fehlerbehandlung

Schlägt das Makro fehl, enthält `error` den gewöhnlichen Python-Traceback und `output` das, was vor dem Fehler ausgegeben wurde:

```json
{"output":"before error\r\n",
 "error":"Traceback (most recent call last):\r\n  File \"<string>\", line 2, in <module>\r\nNameError: name 'foo' is not defined"}
```

Protokollverstöße werden auf dieselbe Weise gemeldet (`error` beginnt mit `Protocol error:`): fehlender NUL-Abschluss, Anfrage über 1 MiB, kein gültiges UTF-8 oder eine Anfrage, deren Übertragung länger als 30 s gedauert hat.

### Hinweise & Einschränkungen

- Die 30-Sekunden-Frist gilt **nur für die Übertragung der Anfrage** — die *Ausführung* des Makros darf beliebig lange dauern; die Antwort kommt, wenn sie beendet ist.
- Nicht-ASCII-Zeichen in der JSON-Antwort werden als `\uXXXX` maskiert (JSON-Standard); jeder JSON-Parser stellt sie wieder her.
- Läuft ReciPro nicht (oder ist der Listener deaktiviert), schlägt der Verbindungsaufbau des Clients fehl oder läuft in einen Timeout — starten Sie zuerst ReciPro.
- Da alles im GUI-Thread abläuft, werden Befehle strikt nacheinander abgearbeitet; senden Sie möglichst keinen neuen Befehl, während aus einem anderen Skript noch ein sehr langer unterwegs ist.

---

## Siehe auch

- [20. Makro](index.md)
- [Integrierte Funktionen](1-built-in-functions.md)
- [Makro-Beispiele](2-examples.md)
