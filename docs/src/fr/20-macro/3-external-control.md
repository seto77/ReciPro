---
title: Contrôle externe (ligne de commande et tube nommé)
---

# Contrôle externe (ligne de commande et tube nommé)

Les macros ReciPro peuvent être exécutées non seulement depuis l'éditeur intégré, mais aussi **depuis l'extérieur de l'application**. Deux mécanismes sont disponibles :

| Mécanisme | Style | Usage typique |
|-----------|-------|-------------|
| **Ligne de commande** (`/m` `/o` `/x`) | Ponctuel : démarrer ReciPro → exécuter une macro → (éventuellement) quitter | Traitements par lots, CI, tâches planifiées |
| **Tube nommé** (`ReciPro.Macro.v1`) | Interactif : envoyer des macros à un ReciPro *en cours d'exécution* et recevoir les résultats | Scripts Python/Jupyter, PC de pilotage d'instrument, intégration avec d'autres logiciels |

Les deux exécutent la macro avec le même moteur que l'éditeur ; toute l'[API intégrée](1-built-in-functions.md) est donc disponible.

---

## Exécution en ligne de commande

```
ReciPro.exe /m <macro.mcr> [/o <result.txt>] [/x]
```

| Option | Signification |
|--------|---------|
| `/m` | Exécute au démarrage le premier fichier `*.mcr` existant trouvé parmi les arguments. |
| `/o <fichier>` | Mode *silencieux* : aucune boîte de dialogue n'est affichée ; la sortie `print()` de la macro et toute trace d'erreur sont écrites dans `<fichier>` (UTF-8). En cas d'échec, le code de sortie du processus vaut **1**. |
| `/x` | Ferme ReciPro à la fin de la macro (recommandé pour un usage par lots — le code de sortie n'est renvoyé à l'appelant qu'à la fin du processus). |

Sans `/o`, les erreurs apparaissent sous forme de boîtes de dialogue ordinaires (utile pendant le développement de la macro). Avec `/o`, l'exécution est entièrement automatique : erreurs de syntaxe, erreurs d'exécution, fichier de macro introuvable et même échec d'écriture du fichier de résultat se terminent tous par le code de sortie 1.

### Exemple : fichier batch

```bat
ReciPro.exe /m C:\work\saed_series.mcr /o C:\work\result.txt /x
if errorlevel 1 (
    echo Echec de la macro :
    type C:\work\result.txt
)
```

### Exemple : PowerShell

```powershell
$p = Start-Process ReciPro.exe -ArgumentList '/m','C:\work\job.mcr','/o','C:\work\result.txt','/x' -Wait -PassThru
if ($p.ExitCode -ne 0) { Get-Content C:\work\result.txt }
```

---

## Écouteur de tube nommé

Pendant son exécution, ReciPro peut accepter des macros provenant d'autres programmes via le tube nommé Windows **`\\.\pipe\ReciPro.Macro.v1`**. Un client écrit une macro, ReciPro l'exécute, puis le client relit le résultat — un simple cycle requête/réponse utilisable depuis Python, PowerShell, C#, ou tout ce qui sait ouvrir un fichier.

### Activation

L'écouteur est **désactivé par défaut**. Activez-le depuis la fenêtre principale :

**Options → Accept external macro commands (named pipe)**

Le réglage est conservé d'une session à l'autre.

!!! warning "Note de sécurité"
    Tant qu'il est activé, *tout processus s'exécutant sous le même utilisateur Windows* peut exécuter du code de macro dans ReciPro. Les autres utilisateurs (et les autres machines) sont rejetés. Ne l'activez que lorsque vous utilisez réellement le contrôle externe.

### Protocole

| Élément | Spécification |
|------|---------------|
| Nom du tube | `\\.\pipe\ReciPro.Macro.v1` (machine locale, même utilisateur uniquement) |
| Requête | Code source de la macro en **UTF-8**, terminé par un unique **octet NUL (0x00)**. 1 Mio maximum, doit arriver en moins de 30 s. |
| Réponse | **JSON UTF-8** `{"output":"...","error":"..."}`, puis le serveur ferme la connexion (lire jusqu'à EOF). |
| `output` | Tout ce que la macro a écrit sur stdout/stderr (`print()` fonctionne ici, contrairement à l'éditeur graphique). |
| `error` | Chaîne vide en cas de succès ; sinon la trace Python, l'erreur de syntaxe ou un message d'erreur de protocole. |
| Connexions | Une connexion = une macro. Les commandes sont exécutées une à une, dans l'ordre. |
| État | La portée Python est **partagée avec l'éditeur de macros et persiste entre les commandes** — les variables définies dans une commande sont visibles dans la suivante. |
| Instances | Si plusieurs processus ReciPro tournent, seul le premier écoute. |

La réponse est envoyée **après la fin** de la macro ; une simulation longue signifie donc simplement une longue attente côté client — la détection de fin est automatique.

### Python : exemple minimal

Le client est du CPython ordinaire — `numpy`, `pandas`, Jupyter, tout est possible. Aucun paquet n'est nécessaire pour le tube lui-même :

```python
with open(r'\\.\pipe\ReciPro.Macro.v1', 'r+b', buffering=0) as f:
    f.write('print(ReciPro.CrystalList.Count)'.encode('utf-8') + b'\0')
    print(f.read().decode('utf-8'))     # {"output":"68\r\n","error":""}
```

### Python : une fonction utilitaire réutilisable

Tous les exemples suivants utilisent cette petite fonction :

```python
import json

PIPE = r'\\.\pipe\ReciPro.Macro.v1'

def recipro(code):
    """Exécute une macro IronPython dans ReciPro ; renvoie sa sortie imprimée sous forme de str."""
    with open(PIPE, 'r+b', buffering=0) as f:
        f.write(code.encode('utf-8') + b'\0')
        res = json.loads(f.read().decode('utf-8'))
    if res['error']:
        raise RuntimeError(res['error'])
    return res['output']
```

### Python : charger un CIF et enregistrer un cliché SAED

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

### Python : traitement par lots de nombreux fichiers CIF

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

### Python : série d'inclinaisons

```python
for i in range(10):
    recipro(f'''
ReciPro.Dir.RotateAroundAxisInDeg(1, 0, 0, 1)
ReciPro.DifSim.SaveAsPng(r"C:\\data\\tilt_{i:02d}.png")
''')
```

### Python : données de taches vers pandas

`ReciPro.DifSim.SpotInfo()` renvoie une chaîne CSV ; affichez-la avec `print()` et analysez-la côté client :

```python
import io, pandas as pd

csv_text = recipro('print(ReciPro.DifSim.SpotInfo())')
df = pd.read_csv(io.StringIO(csv_text))
print(df.head())
```

### Python : l'état persiste entre les commandes

```python
recipro('n = ReciPro.CrystalList.Count')   # définir une variable...
print(recipro('print(n * 2)'))             # ...et l'utiliser plus tard
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
$r.output      # sortie imprimée
$r.error       # vide en cas de succès
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

### Gestion des erreurs

Lorsque la macro échoue, `error` contient la trace Python habituelle et `output` ce qui avait été imprimé avant l'échec :

```json
{"output":"before error\r\n",
 "error":"Traceback (most recent call last):\r\n  File \"<string>\", line 2, in <module>\r\nNameError: name 'foo' is not defined"}
```

Les violations de protocole sont signalées de la même manière (`error` commence par `Protocol error:`) : terminateur NUL manquant, requête de plus de 1 Mio, UTF-8 invalide, ou requête ayant mis plus de 30 s à arriver.

### Remarques et limites

- Le délai de 30 secondes s'applique **uniquement au transfert de la requête** — l'*exécution* de la macro peut durer indéfiniment ; la réponse arrive à la fin.
- Les caractères non ASCII de la réponse JSON sont échappés en `\uXXXX` (JSON standard) ; tout analyseur JSON les restitue.
- Si ReciPro n'est pas lancé (ou si l'écouteur est désactivé), l'ouverture/la connexion du client échoue ou expire — démarrez d'abord ReciPro.
- Comme tout s'exécute sur le thread de l'interface, les commandes sont mises en file et exécutées strictement une à une ; évitez d'envoyer une nouvelle commande pendant qu'une très longue est en cours depuis un autre script.

---

## Voir aussi

- [20. Macro](index.md)
- [Fonctions intégrées](1-built-in-functions.md)
- [Exemples de macros](2-examples.md)
