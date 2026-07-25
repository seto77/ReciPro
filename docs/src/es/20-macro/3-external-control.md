---
title: Control externo (línea de comandos y canalización con nombre)
---

# Control externo (línea de comandos y canalización con nombre)

Las macros de ReciPro pueden ejecutarse no sólo desde el editor integrado, sino también **desde fuera de la aplicación**. Hay dos mecanismos disponibles:

| Mecanismo | Estilo | Uso típico |
|-----------|-------|-------------|
| **Línea de comandos** (`/m` `/o` `/x`) | De una sola vez: iniciar ReciPro → ejecutar una macro → (opcionalmente) salir | Procesos por lotes, CI, tareas programadas |
| **Canalización con nombre** (`ReciPro.Macro.v1`) | Interactivo: enviar macros a un ReciPro *en ejecución* y recibir los resultados | Scripts de Python/Jupyter, PC de control de instrumentos, integración con otro software |

Ambos ejecutan la macro con el mismo motor que el editor, de modo que toda la [API integrada](1-built-in-functions.md) está disponible.

---

## Ejecución desde la línea de comandos

```
ReciPro.exe /m <macro.mcr> [/o <result.txt>] [/x]
```

| Opción | Significado |
|--------|---------|
| `/m` | Tras el inicio, ejecuta el primer archivo `*.mcr` existente encontrado entre los argumentos. |
| `/o <archivo>` | Modo *silencioso*: no se muestran diálogos; la salida de `print()` de la macro y cualquier traza de error se escriben en `<archivo>` (UTF-8). Si falla, el código de salida del proceso se fija en **1**. |
| `/x` | Cierra ReciPro cuando termina la macro (recomendado para uso por lotes: el código de salida sólo se devuelve al llamador cuando el proceso termina). |

Sin `/o`, los errores aparecen como cuadros de diálogo normales (útil mientras se desarrolla la macro). Con `/o`, la ejecución es totalmente desatendida: errores de sintaxis, errores en tiempo de ejecución, un archivo de macro inexistente e incluso un fallo al escribir el archivo de resultados terminan con código de salida 1.

### Ejemplo: archivo por lotes

```bat
ReciPro.exe /m C:\work\saed_series.mcr /o C:\work\result.txt /x
if errorlevel 1 (
    echo La macro ha FALLADO:
    type C:\work\result.txt
)
```

### Ejemplo: PowerShell

```powershell
$p = Start-Process ReciPro.exe -ArgumentList '/m','C:\work\job.mcr','/o','C:\work\result.txt','/x' -Wait -PassThru
if ($p.ExitCode -ne 0) { Get-Content C:\work\result.txt }
```

---

## Escucha en canalización con nombre

Mientras ReciPro está en ejecución, puede aceptar macros de otros programas a través de la canalización con nombre de Windows **`\\.\pipe\ReciPro.Macro.v1`**. Un cliente escribe una macro, ReciPro la ejecuta y el cliente lee el resultado: un sencillo ciclo petición/respuesta que funciona desde Python, PowerShell, C# o cualquier cosa capaz de abrir un archivo.

### Activación

La escucha está **desactivada de forma predeterminada**. Actívela desde la ventana principal:

**Opciones → Accept external macro commands (named pipe)**

El ajuste se conserva entre sesiones.

!!! warning "Nota de seguridad"
    Mientras esté activada, *cualquier proceso que se ejecute con el mismo usuario de Windows* puede ejecutar código de macro dentro de ReciPro. Se rechazan otros usuarios (y otras máquinas). Actívela sólo cuando realmente utilice el control externo.

### Protocolo

| Elemento | Especificación |
|------|---------------|
| Nombre de la canalización | `\\.\pipe\ReciPro.Macro.v1` (máquina local, sólo el mismo usuario) |
| Petición | Código fuente de la macro en **UTF-8**, terminado por un único **byte NUL (0x00)**. Máximo 1 MiB, debe llegar en menos de 30 s. |
| Respuesta | **JSON UTF-8** `{"output":"...","error":"..."}`; después el servidor cierra la conexión (leer hasta EOF). |
| `output` | Todo lo que la macro escribió en stdout/stderr (aquí `print()` funciona, a diferencia del editor gráfico). |
| `error` | Cadena vacía si tuvo éxito; en caso contrario, la traza de Python, el error de sintaxis o un mensaje de error de protocolo. |
| Conexiones | Una conexión = una macro. Las órdenes se ejecutan de una en una, en orden. |
| Estado | El ámbito de Python se **comparte con el editor de macros y persiste entre órdenes**: las variables definidas en una orden son visibles en la siguiente. |
| Instancias | Si hay varios procesos de ReciPro en ejecución, sólo el primero escucha. |

La respuesta se envía **cuando la macro termina**, de modo que una simulación larga simplemente supone una espera larga en el cliente: la detección de finalización es automática.

### Python: ejemplo mínimo

El lado cliente es CPython normal: `numpy`, `pandas`, Jupyter, lo que sea. Para la canalización en sí no se necesita ningún paquete:

```python
with open(r'\\.\pipe\ReciPro.Macro.v1', 'r+b', buffering=0) as f:
    f.write('print(ReciPro.CrystalList.Count)'.encode('utf-8') + b'\0')
    print(f.read().decode('utf-8'))     # {"output":"68\r\n","error":""}
```

### Python: una función auxiliar reutilizable

Todos los ejemplos siguientes usan esta pequeña función:

```python
import json

PIPE = r'\\.\pipe\ReciPro.Macro.v1'

def recipro(code):
    """Ejecuta una macro de IronPython en ReciPro; devuelve su salida impresa como str."""
    with open(PIPE, 'r+b', buffering=0) as f:
        f.write(code.encode('utf-8') + b'\0')
        res = json.loads(f.read().decode('utf-8'))
    if res['error']:
        raise RuntimeError(res['error'])
    return res['output']
```

### Python: cargar un CIF y guardar un patrón SAED

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

### Python: procesar por lotes muchos archivos CIF

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

### Python: serie de inclinaciones

```python
for i in range(10):
    recipro(f'''
ReciPro.Dir.RotateAroundAxisInDeg(1, 0, 0, 1)
ReciPro.DifSim.SaveAsPng(r"C:\\data\\tilt_{i:02d}.png")
''')
```

### Python: datos de puntos hacia pandas

`ReciPro.DifSim.SpotInfo()` devuelve una cadena CSV; imprímala con `print()` y analícela en el cliente:

```python
import io, pandas as pd

csv_text = recipro('print(ReciPro.DifSim.SpotInfo())')
df = pd.read_csv(io.StringIO(csv_text))
print(df.head())
```

### Python: el estado persiste entre órdenes

```python
recipro('n = ReciPro.CrystalList.Count')   # definir una variable...
print(recipro('print(n * 2)'))             # ...y usarla en una orden posterior
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
$r.output      # salida impresa
$r.error       # vacío si tuvo éxito
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

### Manejo de errores

Cuando la macro falla, `error` contiene la traza habitual de Python y `output` lo que se imprimió antes del fallo:

```json
{"output":"before error\r\n",
 "error":"Traceback (most recent call last):\r\n  File \"<string>\", line 2, in <module>\r\nNameError: name 'foo' is not defined"}
```

Las violaciones del protocolo se informan del mismo modo (`error` empieza por `Protocol error:`): falta el terminador NUL, petición de más de 1 MiB, UTF-8 no válido, o una petición que tardó más de 30 s en llegar.

### Notas y limitaciones

- El plazo de 30 segundos se aplica **sólo a la transferencia de la petición**: la *ejecución* de la macro puede durar lo que haga falta; la respuesta llega cuando termina.
- Los caracteres no ASCII de la respuesta JSON se escapan como `\uXXXX` (JSON estándar); cualquier analizador JSON los restaura.
- Si ReciPro no está en ejecución (o la escucha está desactivada), la conexión del cliente falla o expira: inicie primero ReciPro.
- Como todo se ejecuta en el hilo de la interfaz, las órdenes se encolan y se ejecutan estrictamente de una en una; evite enviar una orden nueva mientras otra muy larga sigue en curso desde otro script.

---

## Véase también

- [20. Macro](index.md)
- [Funciones integradas](1-built-in-functions.md)
- [Ejemplos de macros](2-examples.md)
