# Exemplos de macro

Exemplos práticos para macros do ReciPro. Os primeiros são introduções acessíveis para iniciantes; exemplos posteriores mostram operações em lote.

> **Nota**: A API de macro do ReciPro é acessada como `ReciPro.ClassName.Member`. O popup de autocompletar sempre insere o prefixo completo `ReciPro.`, então raramente é necessário digitá-lo à mão.

---

## Exemplos para iniciantes

### A. Laço básico

A maneira mais fácil de conhecer o editor. Execute isto com **Step by step** e observe `i` e `sq` mudarem no painel de depuração — esta é a forma do ReciPro de "imprimir" valores (`print()` não funciona).

```python
# Loop 10 times and compute the squares.
for i in range(10):
    sq = i * i
```

### B. Usando o módulo math

O módulo `math` é importado automaticamente na inicialização. Use-o diretamente.

```python
r = 5.0
area = math.pi * r * r
circumference = 2 * math.pi * r
# Run in Step mode to see 'area' and 'circumference' in the debug panel.
```

### C. Girar o cristal atual

```python
# Rotate the current crystal by 30 degrees around the a-axis (x-axis).
ReciPro.Dir.RotateAroundAxisInDeg(1, 0, 0, 30)
```

### D. Alinhar a um eixo de zona

```python
# Align so that the [001] zone axis is normal to the screen.
ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
```

### E. Iterar pelos primeiros cristais

```python
# Collect the names of the first 5 crystals in the list.
names = []
for i in range(5):
    ReciPro.CrystalList.SelectedIndex = i
    names.append(ReciPro.Crystal.Name)
# Run in Step mode to see 'names' grow line by line.
```

### F. Abrir um padrão de difração

```python
# Open the diffraction simulator and display the [001] pattern
# of the first crystal in the list with 200 keV electrons.
ReciPro.CrystalList.SelectedIndex = 0
ReciPro.DifSim.Open()
ReciPro.DifSim.Source_Electron()
ReciPro.DifSim.Energy = 200
ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
```

---

## Exemplos em lote

### 1. Salvar padrões de difração de todos os cristais

```python
folder = ReciPro.File.GetDirectoryPath()

ReciPro.DifSim.Open()
ReciPro.DifSim.Source_Electron()
ReciPro.DifSim.Energy = 200  # 200 keV
ReciPro.DifSim.Calc_Kinematical()
ReciPro.DifSim.SkipRendering = True

for i in range(80):  # adjust to your crystal count
    ReciPro.CrystalList.SelectedIndex = i
    name = ReciPro.Crystal.Name
    ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
    ReciPro.DifSim.SaveAsPng(folder + name + "_001.png")
    ReciPro.Dir.ProjectAlongAxis(1, 1, 0)
    ReciPro.DifSim.SaveAsPng(folder + name + "_110.png")

ReciPro.DifSim.SkipRendering = False
```

### 2. Girar e capturar instantâneos

```python
folder = ReciPro.File.GetDirectoryPath()
ReciPro.DifSim.Open()
ReciPro.DifSim.Source_Electron()
ReciPro.DifSim.Energy = 200

ReciPro.Dir.ProjectAlongAxis(0, 0, 1)

for i in range(90):
    ReciPro.DifSim.SaveAsPng(folder + "rot_%03d.png" % i)
    ReciPro.Dir.RotateAroundAxisInDeg(1, 0, 0, 1)
```

### 3. Definir ângulos de Euler

```python
# Euler angles in degrees
ReciPro.Dir.EulerInDeg(45, 30, 60)

# Same thing in radians (math is pre-imported)
ReciPro.Dir.Euler(math.pi/4, math.pi/6, math.pi/3)
```

### 4. Projetar ao longo de planos e eixos

```python
ReciPro.Dir.ProjectAlongPlane(1, 1, 1)  # (111) normal → screen
ReciPro.Dir.ProjectAlongAxis(1, 1, 0)   # [110] → screen
```

### 5. Importar arquivos CIF em lote

```python
files = ReciPro.File.GetFileNames()
for f in files:
    ReciPro.File.ReadCrystal(f)
    ReciPro.CrystalList.Add()
```

### 6. Exportar informações de reflexão

```python
ReciPro.DifSim.Open()
ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
info = ReciPro.DifSim.SpotInfo()
ReciPro.File.SaveText(info, "spot_info.csv")
```

---

## Dicas

- **Inspecionar valores**: `print()` não funciona neste ambiente (sem console). Use a execução **Step by step** e observe o painel de depuração, que lista as variáveis locais em cada linha.
- **`math` é pré-importado**: `math.sqrt(x)`, `math.sin(x)`, `math.pi`, `math.radians(deg)` estão disponíveis sem `import math`.
- **Aceleração em lote**: defina `ReciPro.DifSim.SkipRendering = True` durante laços apertados e redefina para `False` em seguida.
- **Aguardar a renderização**: chame `ReciPro.Sleep(ms)` para pausar a execução quando for necessário que a GUI acompanhe.
- **Autocompletar**: o popup mostra a forma completa `ReciPro.Class.Member`. Digite algumas letras e confirme com `Enter` ou `Tab`.

---

## Veja também

- [20. Macro](index.md)
- [20.1. Funções integradas](1-built-in-functions.md)
