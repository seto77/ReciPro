---
title: 外部制御 (コマンドライン / Named Pipe)
---

# 外部制御 (コマンドライン / Named Pipe)

ReciPro のマクロは、エディタからだけでなく**アプリの外から**実行することもできます。仕組みは2つあります:

| 仕組み | スタイル | 主な用途 |
|--------|----------|----------|
| **コマンドライン** (`/m` `/o` `/x`) | ワンショット: ReciPro を起動 → マクロ実行 → (必要なら) 終了 | バッチ処理、CI、タスクスケジューラ |
| **Named Pipe** (`ReciPro.Macro.v1`) | 対話型: **起動中の** ReciPro にマクロを送り、結果を受け取る | Python/Jupyter からのスクリプティング、装置制御 PC、他ソフトとの連携 |

どちらもエディタと同じエンジンでマクロを実行するので、[組み込み API](1-built-in-functions.md) の全機能が使えます。

---

## コマンドライン実行

```
ReciPro.exe /m <macro.mcr> [/o <result.txt>] [/x]
```

| スイッチ | 意味 |
|----------|------|
| `/m` | 起動後、引数中で最初に見つかった実在する `*.mcr` ファイルを実行します。 |
| `/o <ファイル>` | *quiet* モード: ダイアログを一切出さず、マクロの `print()` 出力とエラーのトレースバックを `<ファイル>` に書き出します (UTF-8)。失敗時はプロセス終了コードが **1** になります。 |
| `/x` | マクロ終了後に ReciPro を閉じます (バッチ利用では推奨 — 終了コードはプロセスが終わって初めて呼び出し元に返ります)。 |

`/o` なしの場合、エラーは通常のダイアログで表示されます (マクロ開発中に便利)。`/o` を付けると完全に無人で実行でき、構文エラー・実行時エラー・マクロファイル未発見・結果ファイルの書き込み失敗のいずれも終了コード 1 になります。

### 例: バッチファイル

```bat
ReciPro.exe /m C:\work\saed_series.mcr /o C:\work\result.txt /x
if errorlevel 1 (
    echo マクロ失敗:
    type C:\work\result.txt
)
```

### 例: PowerShell

```powershell
$p = Start-Process ReciPro.exe -ArgumentList '/m','C:\work\job.mcr','/o','C:\work\result.txt','/x' -Wait -PassThru
if ($p.ExitCode -ne 0) { Get-Content C:\work\result.txt }
```

---

## Named Pipe リスナー

起動中の ReciPro は、Windows の Named Pipe **`\\.\pipe\ReciPro.Macro.v1`** を通じて他のプログラムからマクロを受け付けられます。クライアントがマクロを書き込むと ReciPro が実行し、結果を読み返す — Python・PowerShell・C# など「ファイルを開ける」言語なら何からでも使える、単純なリクエスト/レスポンス方式です。

### 有効化

リスナーは**既定で OFF** です。メインウィンドウのメニューから有効化します:

**Option → 外部からのマクロ命令を受け付ける (Named Pipe)**

設定は次回起動時も保持されます。

!!! warning "セキュリティ上の注意"
    有効にしている間は、*同じ Windows ユーザーで動作する任意のプロセス*が ReciPro 内でマクロコードを実行できます。他ユーザー (および他マシン) からの接続は拒否されます。外部制御を実際に使うときだけ有効にしてください。

### プロトコル

| 項目 | 仕様 |
|------|------|
| パイプ名 | `\\.\pipe\ReciPro.Macro.v1` (ローカルマシン・同一ユーザーのみ) |
| リクエスト | マクロのソースコードを **UTF-8** で書き、**NUL バイト (0x00)** 1個で終端。最大 1 MiB、30 秒以内。 |
| レスポンス | **UTF-8 JSON** `{"output":"...","error":"..."}` を書いた後、サーバー側が切断 (EOF まで読む)。 |
| `output` | マクロが stdout/stderr に書いた内容 (GUI エディタと違い、ここでは `print()` が使えます)。 |
| `error` | 成功時は空文字。失敗時は Python トレースバック・構文エラー・プロトコルエラーのメッセージ。 |
| 接続 | 1 接続 = 1 マクロ。コマンドは到着順に 1 つずつ実行されます。 |
| 状態 | Python スコープは**マクロエディタと共有され、コマンド間で持続**します — あるコマンドで定義した変数は次のコマンドからも見えます。 |
| 多重起動 | ReciPro を複数起動した場合、待ち受けるのは最初の 1 プロセスだけです。 |

レスポンスはマクロが**完了してから**返るので、長いシミュレーションはクライアント側で待つだけで完了検知になります。

### Python: 最小の例

クライアント側は普通の CPython です — `numpy` も `pandas` も Jupyter も自由に使えます。パイプ自体に追加パッケージは不要です:

```python
with open(r'\\.\pipe\ReciPro.Macro.v1', 'r+b', buffering=0) as f:
    f.write('print(ReciPro.CrystalList.Count)'.encode('utf-8') + b'\0')
    print(f.read().decode('utf-8'))     # {"output":"68\r\n","error":""}
```

### Python: 再利用できるヘルパー関数

以降の例はすべてこの小さな関数を使います:

```python
import json

PIPE = r'\\.\pipe\ReciPro.Macro.v1'

def recipro(code):
    """ReciPro で IronPython マクロを実行し、print 出力を str で返す。"""
    with open(PIPE, 'r+b', buffering=0) as f:
        f.write(code.encode('utf-8') + b'\0')
        res = json.loads(f.read().decode('utf-8'))
    if res['error']:
        raise RuntimeError(res['error'])
    return res['output']
```

### Python: CIF を読み込んで SAED パターンを保存

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

### Python: 多数の CIF ファイルをバッチ処理

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

### Python: 傾斜シリーズ

```python
for i in range(10):
    recipro(f'''
ReciPro.Dir.RotateAroundAxisInDeg(1, 0, 0, 1)
ReciPro.DifSim.SaveAsPng(r"C:\\data\\tilt_{i:02d}.png")
''')
```

### Python: スポット情報を pandas へ

`ReciPro.DifSim.SpotInfo()` は CSV 文字列を返すので、`print()` してクライアント側で解析します:

```python
import io, pandas as pd

csv_text = recipro('print(ReciPro.DifSim.SpotInfo())')
df = pd.read_csv(io.StringIO(csv_text))
print(df.head())
```

### Python: コマンド間で状態が持続する

```python
recipro('n = ReciPro.CrystalList.Count')   # 変数を定義しておくと...
print(recipro('print(n * 2)'))             # ...後のコマンドから使える
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
$r.output      # print 出力
$r.error       # 成功時は空
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

### エラー処理

マクロが失敗すると `error` に通常の Python トレースバックが入り、`output` には失敗までに print された内容が入ります:

```json
{"output":"before error\r\n",
 "error":"Traceback (most recent call last):\r\n  File \"<string>\", line 2, in <module>\r\nNameError: name 'foo' is not defined"}
```

プロトコル違反も同じ形式で報告されます (`error` が `Protocol error:` で始まります): NUL 終端がない、リクエストが 1 MiB 超、UTF-8 として不正、リクエストの到着に 30 秒以上かかった、の各ケースです。

### 注意点・制限

- 30 秒の期限は**リクエストの転送**にだけ適用されます — マクロの*実行*はいくらでも長くて構いません。レスポンスは完了時に返ります。
- JSON レスポンス中の非 ASCII 文字は `\uXXXX` にエスケープされます (JSON の標準仕様)。JSON パーサーを通せば元に戻ります。
- ReciPro が起動していない (またはリスナーが無効の) 場合、クライアントの接続はタイムアウトします — 先に ReciPro を起動してください。
- すべて GUI スレッド上で実行されるため、コマンドは厳密に 1 つずつ順番に処理されます。別のスクリプトから同時に送っても壊れませんが、順番待ちになります。

---

## 関連項目

- [20. マクロ](index.md)
- [組み込み関数一覧](1-built-in-functions.md)
- [マクロの使用例](2-examples.md)
