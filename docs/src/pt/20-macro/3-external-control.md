---
title: Controle externo (linha de comando e named pipe)
---

# Controle externo (linha de comando e named pipe)

As macros do ReciPro podem ser executadas não apenas pelo editor integrado, mas também **de fora do aplicativo**. Há dois mecanismos disponíveis:

| Mecanismo | Estilo | Uso típico |
|-----------|-------|-------------|
| **Linha de comando** (`/m` `/o` `/x`) | Único: iniciar o ReciPro → executar uma macro → (opcionalmente) sair | Processamento em lote, CI, tarefas agendadas |
| **Named pipe** (`ReciPro.Macro.v1`) | Interativo: enviar macros a um ReciPro *em execução* e receber os resultados | Scripts Python/Jupyter, PCs de controle de instrumentos, integração com outros programas |

Ambos executam a macro no mesmo mecanismo do editor, de modo que toda a [API integrada](1-built-in-functions.md) está disponível.

---

## Execução pela linha de comando

```
ReciPro.exe /m <macro.mcr> [/o <result.txt>] [/x]
```

| Opção | Significado |
|--------|---------|
| `/m` | Após a inicialização, executa o primeiro arquivo `*.mcr` existente encontrado entre os argumentos. |
| `/o <arquivo>` | Modo *silencioso*: nenhuma caixa de diálogo é exibida; a saída de `print()` da macro e qualquer traceback de erro são gravados em `<arquivo>` (UTF-8). Em caso de falha, o código de saída do processo é **1**. |
| `/x` | Fecha o ReciPro quando a macro termina (recomendado para uso em lote — o código de saída só é devolvido ao chamador quando o processo termina). |

Sem `/o`, os erros aparecem como caixas de diálogo comuns (útil durante o desenvolvimento da macro). Com `/o`, a execução é totalmente automática: erros de sintaxe, erros em tempo de execução, arquivo de macro inexistente e até uma falha ao gravar o arquivo de resultado terminam com código de saída 1.

### Exemplo: arquivo em lote

```bat
ReciPro.exe /m C:\work\saed_series.mcr /o C:\work\result.txt /x
if errorlevel 1 (
    echo A macro FALHOU:
    type C:\work\result.txt
)
```

### Exemplo: PowerShell

```powershell
$p = Start-Process ReciPro.exe -ArgumentList '/m','C:\work\job.mcr','/o','C:\work\result.txt','/x' -Wait -PassThru
if ($p.ExitCode -ne 0) { Get-Content C:\work\result.txt }
```

---

## Ouvinte de named pipe

Enquanto o ReciPro está em execução, ele pode aceitar macros de outros programas pelo named pipe do Windows **`\\.\pipe\ReciPro.Macro.v1`**. O cliente grava uma macro, o ReciPro a executa e o cliente lê o resultado de volta — um simples ciclo de requisição/resposta que funciona a partir de Python, PowerShell, C# ou de qualquer coisa capaz de abrir um arquivo.

### Ativação

O ouvinte fica **desligado por padrão**. Ative-o na janela principal:

**Opções → Accept external macro commands (named pipe)**

A configuração é mantida entre sessões.

!!! warning "Observação de segurança"
    Enquanto estiver ativado, *qualquer processo executado sob o mesmo usuário do Windows* pode executar código de macro dentro do ReciPro. Outros usuários (e outras máquinas) são rejeitados. Ative-o apenas quando realmente usar o controle externo.

### Protocolo

| Item | Especificação |
|------|---------------|
| Nome do pipe | `\\.\pipe\ReciPro.Macro.v1` (máquina local, apenas o mesmo usuário) |
| Requisição | Código-fonte da macro em **UTF-8**, terminado por um único **byte NUL (0x00)**. Máximo de 1 MiB, deve chegar em até 30 s. |
| Resposta | **JSON UTF-8** `{"output":"...","error":"..."}`; em seguida o servidor fecha a conexão (leia até o EOF). |
| `output` | Tudo o que a macro escreveu em stdout/stderr (`print()` funciona aqui, ao contrário do editor gráfico). |
| `error` | String vazia em caso de sucesso; caso contrário, o traceback do Python, o erro de sintaxe ou uma mensagem de erro de protocolo. |
| Conexões | Uma conexão = uma macro. Os comandos são executados um de cada vez, em ordem. |
| Estado | O escopo do Python é **compartilhado com o editor de macros e persiste entre os comandos** — variáveis definidas em um comando ficam visíveis no seguinte. |
| Instâncias | Se houver vários processos do ReciPro em execução, apenas o primeiro escuta. |

A resposta é enviada **depois que a macro termina**, de modo que uma simulação longa significa apenas uma espera longa no cliente — a detecção de conclusão é automática.

### Python: exemplo mínimo

O lado cliente é CPython comum — `numpy`, `pandas`, Jupyter, o que quiser. Para o pipe em si não é necessário nenhum pacote:

```python
with open(r'\\.\pipe\ReciPro.Macro.v1', 'r+b', buffering=0) as f:
    f.write('print(ReciPro.CrystalList.Count)'.encode('utf-8') + b'\0')
    print(f.read().decode('utf-8'))     # {"output":"68\r\n","error":""}
```

### Python: uma função auxiliar reutilizável

Todos os exemplos a seguir usam esta pequena função:

```python
import json

PIPE = r'\\.\pipe\ReciPro.Macro.v1'

def recipro(code):
    """Executa uma macro IronPython no ReciPro; devolve a saída impressa como str."""
    with open(PIPE, 'r+b', buffering=0) as f:
        f.write(code.encode('utf-8') + b'\0')
        res = json.loads(f.read().decode('utf-8'))
    if res['error']:
        raise RuntimeError(res['error'])
    return res['output']
```

### Python: carregar um CIF e salvar um padrão SAED

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

### Python: processar em lote muitos arquivos CIF

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

### Python: série de inclinações

```python
for i in range(10):
    recipro(f'''
ReciPro.Dir.RotateAroundAxisInDeg(1, 0, 0, 1)
ReciPro.DifSim.SaveAsPng(r"C:\\data\\tilt_{i:02d}.png")
''')
```

### Python: dados dos pontos para o pandas

`ReciPro.DifSim.SpotInfo()` devolve uma string CSV; imprima-a com `print()` e analise-a no cliente:

```python
import io, pandas as pd

csv_text = recipro('print(ReciPro.DifSim.SpotInfo())')
df = pd.read_csv(io.StringIO(csv_text))
print(df.head())
```

### Python: o estado persiste entre comandos

```python
recipro('n = ReciPro.CrystalList.Count')   # define uma variável...
print(recipro('print(n * 2)'))             # ...e usa em um comando posterior
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
$r.output      # saída impressa
$r.error       # vazio em caso de sucesso
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

### Tratamento de erros

Quando a macro falha, `error` contém o traceback usual do Python e `output` contém o que foi impresso antes da falha:

```json
{"output":"before error\r\n",
 "error":"Traceback (most recent call last):\r\n  File \"<string>\", line 2, in <module>\r\nNameError: name 'foo' is not defined"}
```

Violações de protocolo são relatadas da mesma forma (`error` começa com `Protocol error:`): terminador NUL ausente, requisição acima de 1 MiB, UTF-8 inválido ou requisição que levou mais de 30 s para chegar.

### Notas e limitações

- O prazo de 30 segundos vale **apenas para a transferência da requisição** — a *execução* da macro pode demorar o quanto for; a resposta chega quando ela termina.
- Caracteres não ASCII na resposta JSON são escapados como `\uXXXX` (JSON padrão); qualquer analisador JSON os restaura.
- Se o ReciPro não estiver em execução (ou o ouvinte estiver desativado), a conexão do cliente falha ou expira — inicie o ReciPro primeiro.
- Como tudo roda na thread da interface, os comandos são enfileirados e executados estritamente um de cada vez; evite enviar um novo comando enquanto outro muito longo está em andamento a partir de outro script.

---

## Veja também

- [20. Macro](index.md)
- [Funções integradas](1-built-in-functions.md)
- [Exemplos de macro](2-examples.md)
