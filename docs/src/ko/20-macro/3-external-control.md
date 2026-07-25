---
title: 외부 제어 (명령줄 및 명명된 파이프)
---

# 외부 제어 (명령줄 및 명명된 파이프)

ReciPro 매크로는 내장 편집기뿐 아니라 **애플리케이션 외부에서도** 실행할 수 있습니다. 두 가지 방식이 있습니다.

| 방식 | 형태 | 주요 용도 |
|-----------|-------|-------------|
| **명령줄** (`/m` `/o` `/x`) | 일회성: ReciPro 시작 → 매크로 실행 → (필요하면) 종료 | 일괄 처리, CI, 작업 스케줄러 |
| **명명된 파이프** (`ReciPro.Macro.v1`) | 대화형: *실행 중인* ReciPro에 매크로를 보내고 결과를 받음 | Python/Jupyter 스크립팅, 장비 제어 PC, 다른 소프트웨어와의 연동 |

둘 다 편집기와 동일한 엔진으로 매크로를 실행하므로 [내장 API](1-built-in-functions.md) 전체를 사용할 수 있습니다.

---

## 명령줄 실행

```
ReciPro.exe /m <macro.mcr> [/o <result.txt>] [/x]
```

| 스위치 | 의미 |
|--------|---------|
| `/m` | 시작 후, 인수 중에서 처음 발견된 실제 `*.mcr` 파일을 실행합니다. |
| `/o <파일>` | *quiet* 모드: 대화 상자를 전혀 표시하지 않고, 매크로의 `print()` 출력과 오류 트레이스백을 `<파일>` 에 UTF-8로 기록합니다. 실패하면 프로세스 종료 코드가 **1** 이 됩니다. |
| `/x` | 매크로가 끝나면 ReciPro를 닫습니다(일괄 처리 시 권장 — 종료 코드는 프로세스가 끝나야 호출자에게 전달됩니다). |

`/o` 가 없으면 오류가 일반 대화 상자로 표시됩니다(매크로 개발 중에 유용). `/o` 를 붙이면 완전 무인 실행이 되며, 구문 오류·실행 시 오류·매크로 파일 없음·결과 파일 쓰기 실패 모두 종료 코드 1로 끝납니다.

### 예: 배치 파일

```bat
ReciPro.exe /m C:\work\saed_series.mcr /o C:\work\result.txt /x
if errorlevel 1 (
    echo 매크로 실패:
    type C:\work\result.txt
)
```

### 예: PowerShell

```powershell
$p = Start-Process ReciPro.exe -ArgumentList '/m','C:\work\job.mcr','/o','C:\work\result.txt','/x' -Wait -PassThru
if ($p.ExitCode -ne 0) { Get-Content C:\work\result.txt }
```

---

## 명명된 파이프 리스너

ReciPro가 실행 중일 때, Windows 명명된 파이프 **`\\.\pipe\ReciPro.Macro.v1`** 를 통해 다른 프로그램의 매크로를 받을 수 있습니다. 클라이언트가 매크로를 쓰면 ReciPro가 실행하고 클라이언트가 결과를 되읽는, 단순한 요청/응답 방식이며 Python·PowerShell·C# 등 파일을 열 수 있는 무엇에서든 사용할 수 있습니다.

### 활성화

리스너는 **기본적으로 꺼져 있습니다**. 메인 창의 메뉴에서 활성화합니다.

**옵션 → Accept external macro commands (named pipe)**

이 설정은 다음 실행에도 유지됩니다.

!!! warning "보안 주의"
    활성화되어 있는 동안에는 *같은 Windows 사용자로 실행되는 모든 프로세스* 가 ReciPro 안에서 매크로 코드를 실행할 수 있습니다. 다른 사용자(및 다른 컴퓨터)의 연결은 거부됩니다. 외부 제어를 실제로 사용할 때만 켜십시오.

### 프로토콜

| 항목 | 사양 |
|------|---------------|
| 파이프 이름 | `\\.\pipe\ReciPro.Macro.v1` (로컬 컴퓨터, 동일 사용자 전용) |
| 요청 | 매크로 소스 코드를 **UTF-8** 로 쓰고 **NUL 바이트(0x00)** 하나로 종료합니다. 최대 1 MiB, 30초 이내에 도착해야 합니다. |
| 응답 | **UTF-8 JSON** `{"output":"...","error":"..."}` 를 쓴 뒤 서버가 연결을 닫습니다(EOF까지 읽기). |
| `output` | 매크로가 stdout/stderr에 쓴 모든 내용(GUI 편집기와 달리 여기서는 `print()` 가 동작합니다). |
| `error` | 성공 시 빈 문자열, 실패 시 Python 트레이스백·구문 오류·프로토콜 오류 메시지. |
| 연결 | 연결 1개 = 매크로 1개. 명령은 도착 순서대로 하나씩 실행됩니다. |
| 상태 | Python 스코프는 **매크로 편집기와 공유되며 명령 사이에 유지**됩니다 — 한 명령에서 정의한 변수를 다음 명령에서 볼 수 있습니다. |
| 인스턴스 | ReciPro 프로세스가 여러 개 실행 중이면 첫 번째 프로세스만 대기합니다. |

응답은 매크로가 **끝난 뒤** 전송되므로, 오래 걸리는 시뮬레이션은 클라이언트에서 그만큼 기다리기만 하면 됩니다 — 완료 감지는 자동입니다.

### Python: 최소 예제

클라이언트 쪽은 평범한 CPython입니다 — `numpy`, `pandas`, Jupyter 무엇이든 쓸 수 있습니다. 파이프 자체에는 추가 패키지가 필요 없습니다.

```python
with open(r'\\.\pipe\ReciPro.Macro.v1', 'r+b', buffering=0) as f:
    f.write('print(ReciPro.CrystalList.Count)'.encode('utf-8') + b'\0')
    print(f.read().decode('utf-8'))     # {"output":"68\r\n","error":""}
```

### Python: 재사용 가능한 헬퍼 함수

이후의 예제는 모두 이 작은 함수를 사용합니다.

```python
import json

PIPE = r'\\.\pipe\ReciPro.Macro.v1'

def recipro(code):
    """ReciPro에서 IronPython 매크로를 실행하고 출력 문자열을 반환합니다."""
    with open(PIPE, 'r+b', buffering=0) as f:
        f.write(code.encode('utf-8') + b'\0')
        res = json.loads(f.read().decode('utf-8'))
    if res['error']:
        raise RuntimeError(res['error'])
    return res['output']
```

### Python: CIF를 읽어 SAED 패턴 저장

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

### Python: 다수의 CIF 파일 일괄 처리

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

### Python: 경사 시리즈

```python
for i in range(10):
    recipro(f'''
ReciPro.Dir.RotateAroundAxisInDeg(1, 0, 0, 1)
ReciPro.DifSim.SaveAsPng(r"C:\\data\\tilt_{i:02d}.png")
''')
```

### Python: 스폿 데이터를 pandas로

`ReciPro.DifSim.SpotInfo()` 는 CSV 문자열을 반환하므로 `print()` 한 뒤 클라이언트에서 파싱합니다.

```python
import io, pandas as pd

csv_text = recipro('print(ReciPro.DifSim.SpotInfo())')
df = pd.read_csv(io.StringIO(csv_text))
print(df.head())
```

### Python: 명령 사이에 상태가 유지됨

```python
recipro('n = ReciPro.CrystalList.Count')   # 변수를 정의해 두면...
print(recipro('print(n * 2)'))             # ...이후 명령에서 사용할 수 있음
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
$r.output      # print 출력
$r.error       # 성공 시 비어 있음
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

### 오류 처리

매크로가 실패하면 `error` 에 일반적인 Python 트레이스백이, `output` 에는 실패 전까지 출력된 내용이 들어갑니다.

```json
{"output":"before error\r\n",
 "error":"Traceback (most recent call last):\r\n  File \"<string>\", line 2, in <module>\r\nNameError: name 'foo' is not defined"}
```

프로토콜 위반도 같은 형식으로 보고됩니다(`error` 가 `Protocol error:` 로 시작). NUL 종료 없음, 요청이 1 MiB 초과, UTF-8이 아님, 요청 도착에 30초 이상 걸림 등입니다.

### 주의점 및 제한

- 30초 제한은 **요청 전송에만** 적용됩니다 — 매크로 *실행* 은 얼마든지 오래 걸려도 되며, 응답은 완료 시점에 돌아옵니다.
- JSON 응답의 비 ASCII 문자는 `\uXXXX` 로 이스케이프됩니다(JSON 표준). JSON 파서를 거치면 원래대로 복원됩니다.
- ReciPro가 실행 중이 아니거나 리스너가 꺼져 있으면 클라이언트의 연결이 실패하거나 시간 초과됩니다 — 먼저 ReciPro를 실행하세요.
- 모두 GUI 스레드에서 실행되므로 명령은 엄격히 하나씩 순서대로 처리됩니다. 다른 스크립트에서 아주 긴 명령이 진행 중일 때 새 명령을 보내면 대기하게 됩니다.

---

## 함께 보기

- [20. 매크로](index.md)
- [내장 함수](1-built-in-functions.md)
- [매크로 예제](2-examples.md)
