---
title: Внешнее управление (командная строка и именованный канал)
---

# Внешнее управление (командная строка и именованный канал)

Макросы ReciPro можно запускать не только из встроенного редактора, но и **извне приложения**. Доступны два механизма:

| Механизм | Стиль | Типичное применение |
|-----------|-------|-------------|
| **Командная строка** (`/m` `/o` `/x`) | Однократно: запустить ReciPro → выполнить макрос → (при необходимости) выйти | Пакетные задания, CI, планировщик задач |
| **Именованный канал** (`ReciPro.Macro.v1`) | Интерактивно: отправлять макросы в *работающий* ReciPro и получать результаты | Скрипты Python/Jupyter, управляющие ПК приборов, интеграция с другим ПО |

Оба выполняют макрос тем же движком, что и редактор, поэтому доступен весь [встроенный API](1-built-in-functions.md).

---

## Запуск из командной строки

```
ReciPro.exe /m <macro.mcr> [/o <result.txt>] [/x]
```

| Ключ | Значение |
|--------|---------|
| `/m` | После запуска выполняет первый существующий файл `*.mcr`, найденный среди аргументов. |
| `/o <файл>` | *Тихий* режим: диалоги не показываются; вывод `print()` макроса и трассировка ошибок записываются в `<файл>` (UTF-8). При сбое код возврата процесса равен **1**. |
| `/x` | Закрывает ReciPro после завершения макроса (рекомендуется для пакетного режима — код возврата попадает вызывающей стороне только по завершении процесса). |

Без `/o` ошибки показываются обычными диалоговыми окнами (удобно при разработке макроса). С `/o` запуск полностью автоматический: синтаксические ошибки, ошибки времени выполнения, отсутствующий файл макроса и даже неудачная запись файла результата завершаются кодом возврата 1.

### Пример: командный файл

```bat
ReciPro.exe /m C:\work\saed_series.mcr /o C:\work\result.txt /x
if errorlevel 1 (
    echo Макрос завершился с ошибкой:
    type C:\work\result.txt
)
```

### Пример: PowerShell

```powershell
$p = Start-Process ReciPro.exe -ArgumentList '/m','C:\work\job.mcr','/o','C:\work\result.txt','/x' -Wait -PassThru
if ($p.ExitCode -ne 0) { Get-Content C:\work\result.txt }
```

---

## Слушатель именованного канала

Пока ReciPro работает, он может принимать макросы от других программ через именованный канал Windows **`\\.\pipe\ReciPro.Macro.v1`**. Клиент пишет макрос, ReciPro его выполняет, а клиент считывает результат — простой цикл «запрос/ответ», доступный из Python, PowerShell, C# или чего угодно, что умеет открывать файл.

### Включение

Слушатель **выключен по умолчанию**. Включите его в главном окне:

**Параметры → Accept external macro commands (named pipe)**

Настройка сохраняется между сеансами.

!!! warning "О безопасности"
    Пока слушатель включён, *любой процесс, работающий от того же пользователя Windows*, может выполнить код макроса внутри ReciPro. Другие пользователи (и другие машины) отклоняются. Включайте его только тогда, когда действительно пользуетесь внешним управлением.

### Протокол

| Пункт | Спецификация |
|------|---------------|
| Имя канала | `\\.\pipe\ReciPro.Macro.v1` (локальная машина, только тот же пользователь) |
| Запрос | Исходный код макроса в **UTF-8**, завершённый одним **байтом NUL (0x00)**. Не более 1 МиБ, должен прийти в течение 30 с. |
| Ответ | **JSON в UTF-8** `{"output":"...","error":"..."}`, после чего сервер закрывает соединение (читать до EOF). |
| `output` | Всё, что макрос вывел в stdout/stderr (здесь `print()` работает, в отличие от редактора в GUI). |
| `error` | Пустая строка при успехе; иначе — трассировка Python, синтаксическая ошибка или сообщение об ошибке протокола. |
| Соединения | Одно соединение = один макрос. Команды выполняются по очереди, по одной. |
| Состояние | Область имён Python **общая с редактором макросов и сохраняется между командами** — переменные, заданные в одной команде, видны в следующей. |
| Экземпляры | Если запущено несколько процессов ReciPro, слушает только первый. |

Ответ отправляется **после завершения** макроса, поэтому длительное моделирование означает лишь долгое ожидание на стороне клиента — определение завершения происходит автоматически.

### Python: минимальный пример

На стороне клиента — обычный CPython: `numpy`, `pandas`, Jupyter, что угодно. Для самого канала пакеты не нужны:

```python
with open(r'\\.\pipe\ReciPro.Macro.v1', 'r+b', buffering=0) as f:
    f.write('print(ReciPro.CrystalList.Count)'.encode('utf-8') + b'\0')
    print(f.read().decode('utf-8'))     # {"output":"68\r\n","error":""}
```

### Python: переиспользуемая вспомогательная функция

Все последующие примеры используют эту небольшую функцию:

```python
import json

PIPE = r'\\.\pipe\ReciPro.Macro.v1'

def recipro(code):
    """Выполняет макрос IronPython в ReciPro и возвращает его вывод в виде str."""
    with open(PIPE, 'r+b', buffering=0) as f:
        f.write(code.encode('utf-8') + b'\0')
        res = json.loads(f.read().decode('utf-8'))
    if res['error']:
        raise RuntimeError(res['error'])
    return res['output']
```

### Python: загрузить CIF и сохранить картину SAED

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

### Python: пакетная обработка множества файлов CIF

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

### Python: серия наклонов

```python
for i in range(10):
    recipro(f'''
ReciPro.Dir.RotateAroundAxisInDeg(1, 0, 0, 1)
ReciPro.DifSim.SaveAsPng(r"C:\\data\\tilt_{i:02d}.png")
''')
```

### Python: данные рефлексов в pandas

`ReciPro.DifSim.SpotInfo()` возвращает строку CSV; выведите её через `print()` и разберите на стороне клиента:

```python
import io, pandas as pd

csv_text = recipro('print(ReciPro.DifSim.SpotInfo())')
df = pd.read_csv(io.StringIO(csv_text))
print(df.head())
```

### Python: состояние сохраняется между командами

```python
recipro('n = ReciPro.CrystalList.Count')   # задаём переменную...
print(recipro('print(n * 2)'))             # ...и используем её позже
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
$r.output      # вывод print
$r.error       # пусто при успехе
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

### Обработка ошибок

Если макрос завершился с ошибкой, в `error` попадает обычная трассировка Python, а в `output` — всё, что было выведено до сбоя:

```json
{"output":"before error\r\n",
 "error":"Traceback (most recent call last):\r\n  File \"<string>\", line 2, in <module>\r\nNameError: name 'foo' is not defined"}
```

Нарушения протокола сообщаются так же (`error` начинается с `Protocol error:`): нет завершающего NUL, запрос больше 1 МиБ, некорректный UTF-8 или запрос, шедший дольше 30 с.

### Замечания и ограничения

- Ограничение в 30 секунд относится **только к передаче запроса** — *выполнение* макроса может длиться сколь угодно долго; ответ придёт по завершении.
- Символы вне ASCII в JSON-ответе экранируются как `\uXXXX` (стандарт JSON); любой JSON-парсер их восстановит.
- Если ReciPro не запущен (или слушатель отключён), подключение клиента завершится ошибкой или тайм-аутом — сначала запустите ReciPro.
- Поскольку всё выполняется в потоке GUI, команды ставятся в очередь и выполняются строго по одной; не отправляйте новую команду, пока из другого скрипта выполняется очень длинная.

---

## См. также

- [20. Макрос](index.md)
- [Встроенные функции](1-built-in-functions.md)
- [Примеры макросов](2-examples.md)
