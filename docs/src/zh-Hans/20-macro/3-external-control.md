---
title: 外部控制（命令行与命名管道）
---

# 外部控制（命令行与命名管道）

ReciPro 的宏不仅可以在内置编辑器中运行，也可以**从应用程序外部**运行。共有两种机制：

| 机制 | 形式 | 典型用途 |
|-----------|-------|-------------|
| **命令行** (`/m` `/o` `/x`) | 一次性：启动 ReciPro → 运行宏 →（可选）退出 | 批处理、CI、计划任务 |
| **命名管道** (`ReciPro.Macro.v1`) | 交互式：向*正在运行的* ReciPro 发送宏并接收结果 | Python/Jupyter 脚本、仪器控制计算机、与其他软件集成 |

两者都使用与编辑器相同的引擎运行宏，因此可以使用全部[内置 API](1-built-in-functions.md)。

---

## 命令行执行

```
ReciPro.exe /m <macro.mcr> [/o <result.txt>] [/x]
```

| 开关 | 含义 |
|--------|---------|
| `/m` | 启动后执行参数中找到的第一个存在的 `*.mcr` 文件。 |
| `/o <文件>` | *静默*模式：不显示任何对话框；宏的 `print()` 输出和错误回溯写入 `<文件>`（UTF-8）。失败时进程退出码为 **1**。 |
| `/x` | 宏结束后关闭 ReciPro（批处理时推荐——退出码只有在进程结束后才会返回给调用方）。 |

不使用 `/o` 时，错误会以普通对话框显示（便于开发宏）。使用 `/o` 则完全无人值守：语法错误、运行时错误、找不到宏文件，甚至写入结果文件失败，都会以退出码 1 结束。

### 示例：批处理文件

```bat
ReciPro.exe /m C:\work\saed_series.mcr /o C:\work\result.txt /x
if errorlevel 1 (
    echo 宏执行失败:
    type C:\work\result.txt
)
```

### 示例：PowerShell

```powershell
$p = Start-Process ReciPro.exe -ArgumentList '/m','C:\work\job.mcr','/o','C:\work\result.txt','/x' -Wait -PassThru
if ($p.ExitCode -ne 0) { Get-Content C:\work\result.txt }
```

---

## 命名管道侦听器

ReciPro 运行期间，可通过 Windows 命名管道 **`\\.\pipe\ReciPro.Macro.v1`** 接受来自其他程序的宏。客户端写入宏，ReciPro 执行后客户端读回结果——这是一个简单的请求/响应流程，可从 Python、PowerShell、C# 或任何能打开文件的语言使用。

### 启用

侦听器**默认关闭**。请在主窗口中启用：

**选项 → Accept external macro commands (named pipe)**

该设置会在下次启动时保留。

!!! warning "安全提示"
    启用期间，*以同一 Windows 用户身份运行的任何进程*都可以在 ReciPro 内执行宏代码。其他用户（以及其他计算机）的连接会被拒绝。请仅在确实使用外部控制时启用。

### 协议

| 项目 | 规格 |
|------|---------------|
| 管道名称 | `\\.\pipe\ReciPro.Macro.v1` （本机，且仅限同一用户） |
| 请求 | 宏的源代码以 **UTF-8** 写入，并以单个 **NUL 字节 (0x00)** 结尾。最大 1 MiB，须在 30 秒内到达。 |
| 响应 | 写入 **UTF-8 JSON** `{"output":"...","error":"..."}` 后服务器断开连接（读取至 EOF）。 |
| `output` | 宏写入 stdout/stderr 的全部内容（与 GUI 编辑器不同，这里 `print()` 可用）。 |
| `error` | 成功时为空字符串；失败时为 Python 回溯、语法错误或协议错误消息。 |
| 连接 | 一次连接 = 一个宏。命令按到达顺序逐个执行。 |
| 状态 | Python 作用域**与宏编辑器共享，并在命令之间保持**——在一条命令中定义的变量在下一条命令中仍然可见。 |
| 实例 | 若同时运行多个 ReciPro 进程，只有第一个进程侦听。 |

响应在宏**执行完毕后**才返回，因此长时间的模拟只是让客户端多等一会儿——完成检测是自动的。

### Python：最小示例

客户端就是普通的 CPython——`numpy`、`pandas`、Jupyter 都可以使用。管道本身不需要任何额外的包：

```python
with open(r'\\.\pipe\ReciPro.Macro.v1', 'r+b', buffering=0) as f:
    f.write('print(ReciPro.CrystalList.Count)'.encode('utf-8') + b'\0')
    print(f.read().decode('utf-8'))     # {"output":"68\r\n","error":""}
```

### Python：可复用的辅助函数

下面的示例都使用这个小函数：

```python
import json

PIPE = r'\\.\pipe\ReciPro.Macro.v1'

def recipro(code):
    """在 ReciPro 中运行 IronPython 宏，并以 str 返回其打印输出。"""
    with open(PIPE, 'r+b', buffering=0) as f:
        f.write(code.encode('utf-8') + b'\0')
        res = json.loads(f.read().decode('utf-8'))
    if res['error']:
        raise RuntimeError(res['error'])
    return res['output']
```

### Python：读取 CIF 并保存 SAED 花样

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

### Python：批量处理大量 CIF 文件

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

### Python：倾斜序列

```python
for i in range(10):
    recipro(f'''
ReciPro.Dir.RotateAroundAxisInDeg(1, 0, 0, 1)
ReciPro.DifSim.SaveAsPng(r"C:\\data\\tilt_{i:02d}.png")
''')
```

### Python：将衍射斑数据导入 pandas

`ReciPro.DifSim.SpotInfo()` 返回 CSV 字符串；用 `print()` 输出后在客户端解析：

```python
import io, pandas as pd

csv_text = recipro('print(ReciPro.DifSim.SpotInfo())')
df = pd.read_csv(io.StringIO(csv_text))
print(df.head())
```

### Python：状态在命令之间保持

```python
recipro('n = ReciPro.CrystalList.Count')   # 先定义一个变量...
print(recipro('print(n * 2)'))             # ...随后的命令即可使用
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
$r.output      # 打印输出
$r.error       # 成功时为空
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

### 错误处理

宏失败时，`error` 中是通常的 Python 回溯，`output` 中是失败之前打印的内容：

```json
{"output":"before error\r\n",
 "error":"Traceback (most recent call last):\r\n  File \"<string>\", line 2, in <module>\r\nNameError: name 'foo' is not defined"}
```

协议违规也以同样的形式报告（`error` 以 `Protocol error:` 开头）：缺少 NUL 结束符、请求超过 1 MiB、不是有效的 UTF-8，或请求到达耗时超过 30 秒。

### 注意事项与限制

- 30 秒的期限**仅适用于请求的传输**——宏的*执行*可以任意长；响应会在执行结束时返回。
- JSON 响应中的非 ASCII 字符会转义为 `\uXXXX`（JSON 标准）；任何 JSON 解析器都能还原。
- 若 ReciPro 未运行（或侦听器已禁用），客户端的连接会失败或超时——请先启动 ReciPro。
- 由于全部在 GUI 线程上执行，命令会排队并严格逐个处理；请尽量避免在另一脚本的超长命令进行中再发送新命令。

---

## 另请参见

- [20. 宏](index.md)
- [内置函数](1-built-in-functions.md)
- [宏示例](2-examples.md)
