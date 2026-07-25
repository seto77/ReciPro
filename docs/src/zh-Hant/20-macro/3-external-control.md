---
title: 外部控制（命令列與具名管道）
---

# 外部控制（命令列與具名管道）

ReciPro 的巨集不僅可以在內建編輯器中執行，也可以**從應用程式外部**執行。共有兩種機制：

| 機制 | 形式 | 典型用途 |
|-----------|-------|-------------|
| **命令列** (`/m` `/o` `/x`) | 一次性：啟動 ReciPro → 執行巨集 →（可選）結束 | 批次處理、CI、排程工作 |
| **具名管道** (`ReciPro.Macro.v1`) | 互動式：向*執行中的* ReciPro 傳送巨集並接收結果 | Python/Jupyter 指令碼、儀器控制電腦、與其他軟體整合 |

兩者都使用與編輯器相同的引擎執行巨集，因此可使用全部的[內建 API](1-built-in-functions.md)。

---

## 命令列執行

```
ReciPro.exe /m <macro.mcr> [/o <result.txt>] [/x]
```

| 參數 | 意義 |
|--------|---------|
| `/m` | 啟動後執行引數中找到的第一個實際存在的 `*.mcr` 檔。 |
| `/o <檔案>` | *靜默*模式：不顯示任何對話方塊；巨集的 `print()` 輸出與錯誤追蹤會寫入 `<檔案>`（UTF-8）。失敗時處理程序結束碼為 **1**。 |
| `/x` | 巨集結束後關閉 ReciPro（批次使用時建議——結束碼要等處理程序結束才會回傳給呼叫端）。 |

未使用 `/o` 時，錯誤會以一般對話方塊顯示（開發巨集時很方便）。加上 `/o` 則完全無人值守：語法錯誤、執行期錯誤、找不到巨集檔，甚至寫入結果檔失敗，都會以結束碼 1 收場。

### 範例：批次檔

```bat
ReciPro.exe /m C:\work\saed_series.mcr /o C:\work\result.txt /x
if errorlevel 1 (
    echo 巨集執行失敗:
    type C:\work\result.txt
)
```

### 範例：PowerShell

```powershell
$p = Start-Process ReciPro.exe -ArgumentList '/m','C:\work\job.mcr','/o','C:\work\result.txt','/x' -Wait -PassThru
if ($p.ExitCode -ne 0) { Get-Content C:\work\result.txt }
```

---

## 具名管道接聽器

ReciPro 執行期間，可透過 Windows 具名管道 **`\\.\pipe\ReciPro.Macro.v1`** 接受其他程式送來的巨集。用戶端寫入巨集，ReciPro 執行後用戶端讀回結果——這是簡單的要求／回應流程，可從 Python、PowerShell、C# 或任何能開啟檔案的語言使用。

### 啟用

接聽器**預設為關閉**。請在主視窗中啟用：

**選項 → Accept external macro commands (named pipe)**

此設定會在下次啟動時保留。

!!! warning "安全性提醒"
    啟用期間，*以相同 Windows 使用者身分執行的任何處理程序*都能在 ReciPro 內執行巨集程式碼。其他使用者（以及其他電腦）的連線會被拒絕。請僅在確實使用外部控制時啟用。

### 通訊協定

| 項目 | 規格 |
|------|---------------|
| 管道名稱 | `\\.\pipe\ReciPro.Macro.v1` （本機，且僅限相同使用者） |
| 要求 | 巨集原始碼以 **UTF-8** 寫入，並以單一 **NUL 位元組 (0x00)** 結尾。最大 1 MiB，須在 30 秒內送達。 |
| 回應 | 寫入 **UTF-8 JSON** `{"output":"...","error":"..."}` 後伺服器關閉連線（讀取至 EOF）。 |
| `output` | 巨集寫入 stdout/stderr 的全部內容（與 GUI 編輯器不同，這裡可以使用 `print()`）。 |
| `error` | 成功時為空字串；失敗時為 Python 追蹤、語法錯誤或通訊協定錯誤訊息。 |
| 連線 | 一次連線 = 一個巨集。命令依抵達順序逐一執行。 |
| 狀態 | Python 範圍**與巨集編輯器共用，並在命令之間持續保留**——在某個命令中定義的變數，下一個命令仍看得到。 |
| 執行個體 | 若同時執行多個 ReciPro 處理程序，只有第一個會接聽。 |

回應會在巨集**執行完畢後**才送出，因此耗時的模擬只是讓用戶端多等一會兒——完成偵測是自動的。

### Python：最小範例

用戶端就是一般的 CPython——`numpy`、`pandas`、Jupyter 都能使用。管道本身不需要任何額外套件：

```python
with open(r'\\.\pipe\ReciPro.Macro.v1', 'r+b', buffering=0) as f:
    f.write('print(ReciPro.CrystalList.Count)'.encode('utf-8') + b'\0')
    print(f.read().decode('utf-8'))     # {"output":"68\r\n","error":""}
```

### Python：可重複使用的輔助函式

以下範例都使用這個小函式：

```python
import json

PIPE = r'\\.\pipe\ReciPro.Macro.v1'

def recipro(code):
    """在 ReciPro 中執行 IronPython 巨集，並以 str 回傳其列印輸出。"""
    with open(PIPE, 'r+b', buffering=0) as f:
        f.write(code.encode('utf-8') + b'\0')
        res = json.loads(f.read().decode('utf-8'))
    if res['error']:
        raise RuntimeError(res['error'])
    return res['output']
```

### Python：讀取 CIF 並儲存 SAED 圖樣

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

### Python：批次處理大量 CIF 檔

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

### Python：傾斜序列

```python
for i in range(10):
    recipro(f'''
ReciPro.Dir.RotateAroundAxisInDeg(1, 0, 0, 1)
ReciPro.DifSim.SaveAsPng(r"C:\\data\\tilt_{i:02d}.png")
''')
```

### Python：將繞射斑資料匯入 pandas

`ReciPro.DifSim.SpotInfo()` 會回傳 CSV 字串；以 `print()` 輸出後在用戶端解析：

```python
import io, pandas as pd

csv_text = recipro('print(ReciPro.DifSim.SpotInfo())')
df = pd.read_csv(io.StringIO(csv_text))
print(df.head())
```

### Python：狀態在命令之間持續保留

```python
recipro('n = ReciPro.CrystalList.Count')   # 先定義一個變數...
print(recipro('print(n * 2)'))             # ...之後的命令即可使用
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
$r.output      # 列印輸出
$r.error       # 成功時為空
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

### 錯誤處理

巨集失敗時，`error` 內是一般的 Python 追蹤，`output` 內則是失敗前列印的內容：

```json
{"output":"before error\r\n",
 "error":"Traceback (most recent call last):\r\n  File \"<string>\", line 2, in <module>\r\nNameError: name 'foo' is not defined"}
```

通訊協定違規也以相同形式回報（`error` 以 `Protocol error:` 開頭）：缺少 NUL 結尾、要求超過 1 MiB、非有效的 UTF-8，或要求送達耗時超過 30 秒。

### 注意事項與限制

- 30 秒的期限**僅適用於要求的傳輸**——巨集的*執行*可以任意久；回應會在執行結束時送回。
- JSON 回應中的非 ASCII 字元會逸出為 `\uXXXX`（JSON 標準）；任何 JSON 剖析器都能還原。
- 若 ReciPro 未執行（或接聽器已停用），用戶端的連線會失敗或逾時——請先啟動 ReciPro。
- 由於全部在 GUI 執行緒上執行，命令會排隊並嚴格逐一處理；請盡量避免在另一個指令碼的超長命令進行中再送出新命令。

---

## 另請參閱

- [20. 巨集](index.md)
- [內建函式](1-built-in-functions.md)
- [巨集範例](2-examples.md)
