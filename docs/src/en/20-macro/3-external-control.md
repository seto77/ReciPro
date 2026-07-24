---
title: External control (command line & named pipe)
---

# External control (command line & named pipe)

ReciPro macros can be executed not only from the built-in editor but also **from outside the application**. Two mechanisms are available:

| Mechanism | Style | Typical use |
|-----------|-------|-------------|
| **Command line** (`/m` `/o` `/x`) | One-shot: start ReciPro → run a macro → (optionally) exit | Batch jobs, CI, scheduled tasks |
| **Named pipe** (`ReciPro.Macro.v1`) | Interactive: send macros to a *running* ReciPro and receive the results | Python/Jupyter scripting, instrument-control PCs, integration with other software |

Both run the macro on the same engine as the editor, so the entire [built-in API](1-built-in-functions.md) is available.

---

## Command-line execution

```
ReciPro.exe /m <macro.mcr> [/o <result.txt>] [/x]
```

| Switch | Meaning |
|--------|---------|
| `/m` | Execute the first existing `*.mcr` file found among the arguments after start-up. |
| `/o <file>` | *Quiet* mode: no dialogs are shown; the macro's `print()` output and any error traceback are written to `<file>` (UTF-8). On failure the process exit code is set to **1**. |
| `/x` | Close ReciPro after the macro finishes (recommended for batch use — the exit code is only returned to the caller when the process ends). |

Without `/o`, errors appear as ordinary dialog boxes (useful while developing the macro). With `/o`, the run is fully unattended: syntax errors, runtime errors, a missing macro file, and even a failure to write the result file all end with exit code 1.

### Example: batch file

```bat
ReciPro.exe /m C:\work\saed_series.mcr /o C:\work\result.txt /x
if errorlevel 1 (
    echo Macro FAILED:
    type C:\work\result.txt
)
```

### Example: PowerShell

```powershell
$p = Start-Process ReciPro.exe -ArgumentList '/m','C:\work\job.mcr','/o','C:\work\result.txt','/x' -Wait -PassThru
if ($p.ExitCode -ne 0) { Get-Content C:\work\result.txt }
```

---

## Named-pipe listener

While ReciPro is running, it can accept macros from other programs through the Windows named pipe **`\\.\pipe\ReciPro.Macro.v1`**. A client writes a macro, ReciPro executes it, and the client reads back the result — a simple request/response cycle that works from Python, PowerShell, C#, or anything that can open a file.

### Enabling

The listener is **off by default**. Enable it from the main window:

**Option → Accept external macro commands (named pipe)**

The setting is remembered across sessions.

!!! warning "Security note"
    While enabled, *any process running under the same Windows user* can execute macro code inside ReciPro. Other users (and other machines) are rejected. Enable it only when you actually use external control.

### Protocol

| Item | Specification |
|------|---------------|
| Pipe name | `\\.\pipe\ReciPro.Macro.v1` (local machine, same user only) |
| Request | Macro source code as **UTF-8**, terminated by a single **NUL byte (0x00)**. Max 1 MiB, must arrive within 30 s. |
| Response | **UTF-8 JSON** `{"output":"...","error":"..."}`, then the server closes the connection (read until EOF). |
| `output` | Everything the macro wrote to stdout/stderr (`print()` works here, unlike in the GUI editor). |
| `error` | Empty string on success; otherwise the Python traceback, syntax error, or a protocol error message. |
| Connections | One connection = one macro. Commands are executed one at a time, in order. |
| State | The Python scope is **shared with the macro editor and persists between commands** — variables defined in one command are visible in the next. |
| Instances | If several ReciPro processes are running, only the first one listens. |

The response is sent after the macro **finishes**, so a long simulation simply means a long wait on the client side — completion detection is automatic.

### Python: minimal example

The client side is ordinary CPython — `numpy`, `pandas`, Jupyter, anything goes. No packages are needed for the pipe itself:

```python
with open(r'\\.\pipe\ReciPro.Macro.v1', 'r+b', buffering=0) as f:
    f.write('print(ReciPro.CrystalList.Count)'.encode('utf-8') + b'\0')
    print(f.read().decode('utf-8'))     # {"output":"68\r\n","error":""}
```

### Python: a reusable helper

All the following examples use this small function:

```python
import json

PIPE = r'\\.\pipe\ReciPro.Macro.v1'

def recipro(code):
    """Run an IronPython macro in ReciPro; return its printed output as str."""
    with open(PIPE, 'r+b', buffering=0) as f:
        f.write(code.encode('utf-8') + b'\0')
        res = json.loads(f.read().decode('utf-8'))
    if res['error']:
        raise RuntimeError(res['error'])
    return res['output']
```

### Python: load a CIF and save a SAED pattern

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

### Python: batch over many CIF files

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

### Python: tilt series

```python
for i in range(10):
    recipro(f'''
ReciPro.Dir.RotateAroundAxisInDeg(1, 0, 0, 1)
ReciPro.DifSim.SaveAsPng(r"C:\\data\\tilt_{i:02d}.png")
''')
```

### Python: spot data into pandas

`ReciPro.DifSim.SpotInfo()` returns a CSV string; `print()` it and parse on the client:

```python
import io, pandas as pd

csv_text = recipro('print(ReciPro.DifSim.SpotInfo())')
df = pd.read_csv(io.StringIO(csv_text))
print(df.head())
```

### Python: state persists between commands

```python
recipro('n = ReciPro.CrystalList.Count')   # define a variable...
print(recipro('print(n * 2)'))             # ...and use it in a later command
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
$r.output      # printed output
$r.error       # empty on success
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

### Error handling

When the macro fails, `error` contains the ordinary Python traceback and `output` contains whatever was printed before the failure:

```json
{"output":"before error\r\n",
 "error":"Traceback (most recent call last):\r\n  File \"<string>\", line 2, in <module>\r\nNameError: name 'foo' is not defined"}
```

Protocol violations are reported the same way (`error` starts with `Protocol error:`): missing NUL terminator, request over 1 MiB, not valid UTF-8, or a request that took longer than 30 s to arrive.

### Notes & limitations

- The 30-second deadline applies to **transferring the request only** — macro *execution* may take arbitrarily long; the response arrives when it finishes.
- Non-ASCII characters in the JSON response are escaped as `\uXXXX` (standard JSON); any JSON parser restores them.
- If ReciPro is not running (or the listener is disabled), the client's connect/open call fails or times out — start ReciPro first.
- Because everything runs on the GUI thread, avoid sending a new command while a very long one is in flight from another script; commands are queued and executed strictly one at a time.

---

## See also

- [20. Macro](index.md)
- [20.1. Built-in functions](1-built-in-functions.md)
- [20.2. Examples](2-examples.md)
