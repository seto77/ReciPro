---
title: Controllo esterno (riga di comando e named pipe)
---

# Controllo esterno (riga di comando e named pipe)

Le macro di ReciPro possono essere eseguite non solo dall'editor incorporato, ma anche **dall'esterno dell'applicazione**. Sono disponibili due meccanismi:

| Meccanismo | Stile | Uso tipico |
|-----------|-------|-------------|
| **Riga di comando** (`/m` `/o` `/x`) | Una tantum: avviare ReciPro → eseguire una macro → (facoltativamente) uscire | Elaborazioni batch, CI, operazioni pianificate |
| **Named pipe** (`ReciPro.Macro.v1`) | Interattivo: inviare macro a un ReciPro *in esecuzione* e ricevere i risultati | Script Python/Jupyter, PC di controllo strumenti, integrazione con altri software |

Entrambi eseguono la macro con lo stesso motore dell'editor, quindi è disponibile tutta l'[API integrata](1-built-in-functions.md).

---

## Esecuzione da riga di comando

```
ReciPro.exe /m <macro.mcr> [/o <result.txt>] [/x]
```

| Opzione | Significato |
|--------|---------|
| `/m` | Dopo l'avvio esegue il primo file `*.mcr` esistente trovato tra gli argomenti. |
| `/o <file>` | Modalità *quiet*: non viene mostrata alcuna finestra di dialogo; l'output di `print()` della macro ed eventuali traceback di errore vengono scritti in `<file>` (UTF-8). In caso di errore il codice di uscita del processo è **1**. |
| `/x` | Chiude ReciPro al termine della macro (consigliato per l'uso batch: il codice di uscita viene restituito al chiamante solo quando il processo termina). |

Senza `/o` gli errori compaiono come normali finestre di dialogo (utile durante lo sviluppo della macro). Con `/o` l'esecuzione è completamente non presidiata: errori di sintassi, errori di esecuzione, file di macro mancante e persino un fallimento nella scrittura del file dei risultati terminano tutti con codice di uscita 1.

### Esempio: file batch

```bat
ReciPro.exe /m C:\work\saed_series.mcr /o C:\work\result.txt /x
if errorlevel 1 (
    echo Macro FALLITA:
    type C:\work\result.txt
)
```

### Esempio: PowerShell

```powershell
$p = Start-Process ReciPro.exe -ArgumentList '/m','C:\work\job.mcr','/o','C:\work\result.txt','/x' -Wait -PassThru
if ($p.ExitCode -ne 0) { Get-Content C:\work\result.txt }
```

---

## Listener della named pipe

Mentre ReciPro è in esecuzione può accettare macro da altri programmi tramite la named pipe di Windows **`\\.\pipe\ReciPro.Macro.v1`**. Un client scrive una macro, ReciPro la esegue e il client rilegge il risultato: un semplice ciclo richiesta/risposta utilizzabile da Python, PowerShell, C# o da qualunque cosa sappia aprire un file.

### Attivazione

Il listener è **disattivato per impostazione predefinita**. Attivalo dalla finestra principale:

**Opzioni → Accept external macro commands (named pipe)**

L'impostazione viene ricordata tra le sessioni.

!!! warning "Nota sulla sicurezza"
    Quando è attivo, *qualsiasi processo in esecuzione con lo stesso utente Windows* può eseguire codice macro dentro ReciPro. Gli altri utenti (e le altre macchine) vengono rifiutati. Attivalo solo quando usi davvero il controllo esterno.

### Protocollo

| Voce | Specifica |
|------|---------------|
| Nome della pipe | `\\.\pipe\ReciPro.Macro.v1` (macchina locale, solo stesso utente) |
| Richiesta | Codice sorgente della macro in **UTF-8**, terminato da un singolo **byte NUL (0x00)**. Massimo 1 MiB, deve arrivare entro 30 s. |
| Risposta | **JSON UTF-8** `{"output":"...","error":"..."}`, poi il server chiude la connessione (leggere fino a EOF). |
| `output` | Tutto ciò che la macro ha scritto su stdout/stderr (`print()` funziona qui, a differenza dell'editor grafico). |
| `error` | Stringa vuota in caso di successo; altrimenti il traceback Python, l'errore di sintassi o un messaggio di errore di protocollo. |
| Connessioni | Una connessione = una macro. I comandi vengono eseguiti uno alla volta, in ordine. |
| Stato | Lo scope Python è **condiviso con l'editor di macro e persiste tra i comandi**: le variabili definite in un comando sono visibili in quello successivo. |
| Istanze | Se sono in esecuzione più processi ReciPro, resta in ascolto solo il primo. |

La risposta viene inviata **al termine** della macro, quindi una simulazione lunga si traduce semplicemente in una lunga attesa lato client: il rilevamento del completamento è automatico.

### Python: esempio minimo

Il lato client è normale CPython: `numpy`, `pandas`, Jupyter, tutto è possibile. Per la pipe in sé non serve alcun pacchetto:

```python
with open(r'\\.\pipe\ReciPro.Macro.v1', 'r+b', buffering=0) as f:
    f.write('print(ReciPro.CrystalList.Count)'.encode('utf-8') + b'\0')
    print(f.read().decode('utf-8'))     # {"output":"68\r\n","error":""}
```

### Python: una funzione di supporto riutilizzabile

Tutti gli esempi seguenti usano questa piccola funzione:

```python
import json

PIPE = r'\\.\pipe\ReciPro.Macro.v1'

def recipro(code):
    """Esegue una macro IronPython in ReciPro; restituisce il suo output come str."""
    with open(PIPE, 'r+b', buffering=0) as f:
        f.write(code.encode('utf-8') + b'\0')
        res = json.loads(f.read().decode('utf-8'))
    if res['error']:
        raise RuntimeError(res['error'])
    return res['output']
```

### Python: caricare un CIF e salvare un pattern SAED

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

### Python: elaborazione batch di molti file CIF

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

### Python: serie di inclinazioni

```python
for i in range(10):
    recipro(f'''
ReciPro.Dir.RotateAroundAxisInDeg(1, 0, 0, 1)
ReciPro.DifSim.SaveAsPng(r"C:\\data\\tilt_{i:02d}.png")
''')
```

### Python: dati degli spot in pandas

`ReciPro.DifSim.SpotInfo()` restituisce una stringa CSV; stampala con `print()` e analizzala lato client:

```python
import io, pandas as pd

csv_text = recipro('print(ReciPro.DifSim.SpotInfo())')
df = pd.read_csv(io.StringIO(csv_text))
print(df.head())
```

### Python: lo stato persiste tra i comandi

```python
recipro('n = ReciPro.CrystalList.Count')   # definisci una variabile...
print(recipro('print(n * 2)'))             # ...e usala in un comando successivo
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
$r.output      # output stampato
$r.error       # vuoto in caso di successo
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

### Gestione degli errori

Quando la macro fallisce, `error` contiene il consueto traceback Python e `output` quanto era stato stampato prima del fallimento:

```json
{"output":"before error\r\n",
 "error":"Traceback (most recent call last):\r\n  File \"<string>\", line 2, in <module>\r\nNameError: name 'foo' is not defined"}
```

Le violazioni del protocollo vengono segnalate allo stesso modo (`error` inizia con `Protocol error:`): terminatore NUL mancante, richiesta oltre 1 MiB, UTF-8 non valido, oppure una richiesta che ha impiegato più di 30 s ad arrivare.

### Note e limitazioni

- Il limite di 30 secondi vale **solo per il trasferimento della richiesta**: l'*esecuzione* della macro può durare quanto necessario; la risposta arriva al termine.
- I caratteri non ASCII nella risposta JSON sono codificati come `\uXXXX` (JSON standard); qualsiasi parser JSON li ripristina.
- Se ReciPro non è in esecuzione (o il listener è disattivato), la connessione del client fallisce o va in timeout: avvia prima ReciPro.
- Poiché tutto viene eseguito sul thread della GUI, i comandi vengono accodati ed eseguiti rigorosamente uno alla volta; evita di inviarne uno nuovo mentre da un altro script ne è in corso uno molto lungo.

---

## Vedi anche

- [20. Macro](index.md)
- [Funzioni integrate](1-built-in-functions.md)
- [Esempi di macro](2-examples.md)
