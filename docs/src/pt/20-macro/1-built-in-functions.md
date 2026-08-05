# Funções integradas

Referência completa das classes e funções disponíveis nas macros do ReciPro.

---

## Classe File

| Função | Descrição |
|----------|-------------|
| `File.GetDirectoryPath(filename)` | Exibir diálogo de seleção de pasta, retornar o caminho selecionado; informe `filename` para obter, em vez disso, a pasta que o contém |
| `File.GetFileName()` | Exibir diálogo de seleção de arquivo, retornar o caminho selecionado |
| `File.GetFileNames()` | Exibir diálogo de seleção de múltiplos arquivos, retornar a lista de caminhos |
| `File.ReadCrystalList(filename)` | Carregar um arquivo de lista de cristais (*.xml); omita `filename` para abrir um diálogo |
| `File.ReadCrystal(filename)` | Carregar um arquivo de cristal CIF/AMC; omita `filename` para abrir um diálogo |
| `File.ExportAsCIF(filename)` | Exportar o cristal atual como CIF; omita `filename` para abrir um diálogo |
| `File.ReadText(filename)` | Ler um arquivo de texto como UTF-8 e devolvê-lo como string; omita `filename` para abrir um diálogo. Combina com `Crystal.LoadCifText()` / `SaveText()` |
| `File.SaveText(textData, filename)` | Salvar dados de texto em um arquivo; grava `textData` em UTF-8 e, se `filename` for omitido, abre um diálogo de salvamento |

---

## Classe Crystal

Lê o cristal selecionado e, por meio de um rascunho pendente, cria e edita cristais.

### Leitura

| Propriedade / Função | Descrição |
|---|---|
| `Crystal.Name` | Nome do cristal |
| `Crystal.ChemicalFormula` | Fórmula química |
| `Crystal.Density` | Densidade (g/cm³) |
| `Crystal.GetCellInAng()` | Constantes de célula como `[a, b, c, alpha, beta, gamma]` (Å, graus) |
| `Crystal.SpaceGroupName` | Símbolo Hermann–Mauguin do grupo espacial, com o sufixo de configuração (`:2`, `:H`, …) quando aplicável |
| `Crystal.SpaceGroupNumber` | Número do grupo espacial das International Tables (1–230) |
| `Crystal.HasPending` | Se há um rascunho aberto |

### Criação e edição (rascunho → Commit)

Um cristal é construído em um **rascunho pendente**: inicie-o, preencha-o com os setters, e `Commit()` valida tudo, constrói o cristal e o aplica como cristal atual em um único passo (a GUI e todos os simuladores abertos se atualizam, como ao carregar um arquivo CIF). Um `Commit()` que falha relata todos os erros de validação juntos, não muda nada e mantém o rascunho, que pode então ser corrigido e recommitado.

| Função | Descrição |
|---|---|
| `Crystal.BeginCreate(name)` | Iniciar um rascunho para um cristal novo |
| `Crystal.BeginEdit()` | Iniciar um rascunho a partir do cristal atual (célula, grupo espacial, átomos e orientação são mantidos) |
| `Crystal.LoadCifText(cifText)` | Iniciar um rascunho a partir de texto CIF (o conteúdo de um arquivo .cif, não um caminho) |
| `Crystal.SetName(name)` | Renomear o rascunho |
| `Crystal.SetCellInAng(a, b, c, alpha, beta, gamma)` | Constantes de célula em **Å e graus**. Cada chamada substitui a célula inteira; argumentos omitidos são derivados das restrições do grupo espacial (para um cristal cúbico basta `a`), e valores explícitos que as contradizem geram erro |
| `Crystal.SetSpaceGroup(symbol)` | Grupo espacial por símbolo (HM curto/completo ou Hall; espaços e `_` são ignorados). Acrescente a configuração (`'Fd-3m:2'`, `'R-3c:H'`, `'P21/c:b1'`) quando o grupo tiver várias — símbolos ambíguos geram um erro listando os candidatos |
| `Crystal.SetSpaceGroupByNumber(itNumber, setting)` | Grupo espacial por número IT (1–230); `setting` (`'1'`, `'2'`, `'H'`, `'R'`, `'b1'`, …) escolhe entre várias configurações |
| `Crystal.AddAtom(label, element, x, y, z, occ, bIso)` | Adicionar um átomo da unidade assimétrica: símbolo do elemento, coordenadas fracionárias, ocupação (0 < occ ≤ 1, padrão 1) e B isotrópico em Å² (padrão 0). Posições equivalentes, letras de Wyckoff e multiplicidades são derivadas automaticamente |
| `Crystal.ClearAtoms()` | Remover todos os átomos do rascunho |
| `Crystal.Commit()` | Validar, construir e aplicar o rascunho |
| `Crystal.Cancel()` | Descartar o rascunho |

```python
ReciPro.Crystal.BeginCreate('NaCl')
ReciPro.Crystal.SetSpaceGroup('Fm-3m')
ReciPro.Crystal.SetCellInAng(5.6402)
ReciPro.Crystal.AddAtom('Na', 'Na', 0, 0, 0)
ReciPro.Crystal.AddAtom('Cl', 'Cl', 0.5, 0.5, 0.5)
ReciPro.Crystal.Commit()

base = ReciPro.Crystal.GetCellInAng()
for k in range(-2, 3):
    ReciPro.Crystal.BeginEdit()
    ReciPro.Crystal.SetCellInAng(base[0] * (1 + 0.01 * k))
    ReciPro.Crystal.Commit()
```

Após um `Commit()` bem-sucedido, o próximo `BeginEdit()` parte do cristal **atualizado**: as mudanças se acumulam — para varreduras em valores absolutos, leia os valores base antes do laço, como acima. Para registrar o cristal na lista de cristais, chame `CrystalList.Add()`.

---

## Classe CrystalList

| Função / Propriedade | Descrição |
|---------------------|-------------|
| `CrystalList.SelectedIndex` | Obter/definir o índice do cristal selecionado |
| `CrystalList.Count` | Número de cristais presentes na lista |
| `CrystalList.Add()` | Anexar o cristal atual à lista |
| `CrystalList.Replace()` | Substituir o cristal selecionado |
| `CrystalList.Delete()` | Excluir o cristal selecionado |
| `CrystalList.ClearAll()` | Limpar todos os cristais |
| `CrystalList.MoveUp()` | Mover o cristal selecionado para cima |
| `CrystalList.MoveDown()` | Mover o cristal selecionado para baixo |

---

## Classe Dir

| Função | Descrição |
|----------|-------------|
| `Dir.Euler(phi, theta, psi)` | Definir a orientação por ângulos de Euler (radianos) |
| `Dir.EulerInDegree(phi, theta, psi)` | Definir a orientação por ângulos de Euler (graus) |
| `Dir.EulerInDeg(phi, theta, psi)` | Alias para `EulerInDegree` |
| `Dir.Rotate(ax, ay, az, angle)` | Girar em torno de um eixo arbitrário (radianos) |
| `Dir.RotateInDeg(ax, ay, az, angle)` | Girar em torno de um eixo arbitrário (graus) |
| `Dir.RotateAroundAxis(u, v, w, angle)` | Girar em torno do eixo de zona [uvw] (radianos) |
| `Dir.RotateAroundAxisInDeg(u, v, w, angle)` | Girar em torno do eixo de zona [uvw] (graus) |
| `Dir.RotateAroundPlane(h, k, l, angle)` | Girar em torno da normal ao plano (hkl) (radianos) |
| `Dir.RotateAroundPlaneInDeg(h, k, l, angle)` | Girar em torno da normal ao plano (hkl) (graus) |
| `Dir.ProjectAlongPlane(h, k, l)` | Definir a normal ao plano perpendicular à tela |
| `Dir.ProjectAlongAxis(u, v, w)` | Definir o eixo de zona perpendicular à tela |
| `Dir.GetEuler()` | Obter a orientação atual como ângulos de Euler Z-X-Z `[phi, theta, psi]` (radianos) |
| `Dir.GetEulerInDeg()` | Obter a orientação atual como ângulos de Euler Z-X-Z `[phi, theta, psi]` (graus) |
| `Dir.GetRotationMatrix()` | Obter a matriz de rotação atual como um array de nove elementos `[R11, R12, R13, R21, R22, R23, R31, R32, R33]` — a mesma convenção de `SpotID.CandidateList()` |
| `Dir.SetRotationMatrix(r11, r12, r13, r21, r22, r23, r31, r32, r33)` | Definir a orientação a partir de nove elementos da matriz de rotação (validados e reortonormalizados antes da aplicação) |

Os ângulos de Euler não são únicos nas posições de bloqueio do cardan (θ = 0 ou 180°): `GetEuler()` após `Euler()` reproduz a mesma atitude, mas não necessariamente os mesmos números. Para salvar e restaurar a orientação com exatidão, use `Dir.GetRotationMatrix()` / `Dir.SetRotationMatrix()`. A convenção completa está descrita em [Geometria de rotação](../4-rotation-geometry.md).

---

## Classe DifSim

### Controle da janela

`DifSim.Open()` / `DifSim.Close()`

### Fonte de onda

`DifSim.Source_Xray()` / `DifSim.Source_Electron()` / `DifSim.Source_Neutron()`

### Propriedades

| Propriedade | Tipo | Descrição |
|----------|------|-------------|
| `Energy` | double | Energia (keV) |
| `Wavelength` | double | Comprimento de onda (Å) |
| `Thickness` | double | Espessura da amostra (nm) |
| `NumberOfDiffractedWaves` | int | Número de ondas de Bloch |
| `CameraLength2` | double | Comprimento de câmera (mm) |
| `SkipRendering` | bool | Ignorar a renderização no processamento em lote |

### Modo de feixe

`Beam_Parallel()` / `Beam_PrecessionXray()` / `Beam_PrecessionElectron()` / `Beam_Convergence()`

### Modo de cálculo

`Calc_Excitation()` / `Calc_Kinematical()` / `Calc_Dynamical()`

### Configurações de imagem

| Propriedade / Função | Descrição |
|---------------------|-------------|
| `ImageResolutionInMM` | Resolução (mm/pixel) |
| `ImageResolutionInNMinv` | Resolução (nm⁻¹/pixel) |
| `ImageWidth` / `ImageHeight` | Tamanho da imagem (pixels) |
| `ImageSize(w, h)` | Definir o tamanho da imagem |

### Detector

| Propriedade | Descrição |
|----------|-------------|
| `Tau` / `TauInDeg` | Ângulo de inclinação do detector τ (rad / graus) |
| `Phi` / `PhiInDeg` | Eixo de rotação do detector φ (rad / graus) |
| `Foot(x, y)` | Posição do foot em pixels |

### Saída

| Função | Descrição |
|----------|-------------|
| `SaveAsPng(filename)` | Salvar o padrão atual como PNG; omita `filename` para abrir um diálogo |
| `SpotInfo()` | Obter os dados dos pontos como string CSV |

---

## Classe SpotID

Comanda o [Spot ID v2](../11-spot-id-v2.md) a partir de uma macro: carregar uma imagem ou uma lista de pontos, detectar os pontos, procurar orientações e ler os candidatos de volta, sem tocar na janela. `FindSpots()` e `Identify()` só retornam depois que o trabalho termina, portanto podem ser encadeados diretamente.

### Controle da janela

`SpotID.Open()` / `SpotID.Close()`

### Fonte de onda

`SpotID.Source_Xray()` / `SpotID.Source_Electron()` / `SpotID.Source_Neutron()`

### Fluxo de trabalho

| Função | Descrição |
|--------|-----------|
| `SpotID.LoadFile(filename)` | Carregar um arquivo como **File > Load** faz: `.csv` é lido como lista de pontos (uma imagem precisa ter sido carregada antes) e qualquer outra extensão como imagem de padrão de difração (dm3, dm4, mrc, ipa, tif e outros formatos suportados). Omita `filename` para abrir um diálogo de arquivo |
| `SpotID.FindSpots()` | Detectar os pontos da imagem carregada e ajustá-los, como faz o botão **Find spots** |
| `SpotID.Identify()` | Procurar orientações que expliquem os pontos detectados, como faz o botão **Identify spots**, e retornar o número de candidatos. Os cristais testados são os selecionados na lista de cristais da janela principal |
| `SpotID.CandidateList()` | Retornar a lista de orientações candidatas como texto CSV |
| `SpotID.SpotList()` | Retornar os pontos observados como texto CSV, com as mesmas colunas de **File > Save**. Combinado com `File.SaveText()`, gera um arquivo que `LoadFile()` consegue reler |

`CandidateList()` fornece, para cada candidato: nome do cristal, os ângulos de Euler Z-X-Z (graus), os nove elementos R11–R33 da matriz de rotação (do referencial do cristal para o do laboratório, aplicada a vetores coluna), o resíduo quadrático médio (nm⁻²) e a atribuição dos pontos observados a índices *hkl*. Os candidatos vêm ordenados pelo número de pontos atribuídos (decrescente) e depois pelo resíduo (crescente). Os números são escritos na cultura invariante, então o separador decimal é sempre um ponto.

### Propriedades

| Propriedade | Tipo | Descrição |
|-------------|------|-----------|
| `Energy` | double | Energia do feixe (keV para raios X e elétrons, meV para nêutrons) |
| `CameraLength` | double | Comprimento de câmara (mm) |
| `PixelSizeInMM` | double | Tamanho do pixel (mm); lê-lo ou escrevê-lo também muda a unidade do tamanho do pixel para mm |
| `PixelSizeInNMinv` | double | Tamanho do pixel (nm⁻¹); lê-lo ou escrevê-lo também muda a unidade para nm⁻¹ |
| `MaxNumberOfSpots` | int | Número máximo de pontos que `FindSpots()` pode detectar |
| `NearestNeighbor` | int | Separação mínima permitida entre pontos detectados (pixels) |
| `FittingRange` | double | Raio da região em torno de cada ponto usada no ajuste do pico (pixels) |
| `AcceptableError` | double | Tolerância da diferença relativa de espaçamento *d* ao associar pontos a reflexões (%) |
| `IgnoreProhibitedReflections` | bool | Ignorar reflexões cinematicamente proibidas, que ainda assim podem aparecer por difração múltipla |
| `MultiGrain` | bool | Procurar vários grãos; `False` significa um único grão |
| `MaxNumberOfGrains` | int | Número máximo de orientações de grão procuradas quando `MultiGrain` é `True` |
| `NumberOfDetectedSpots` | int | Número de pontos detectados (somente leitura) |
| `NumberOfCandidates` | int | Número de candidatos encontrados pelo último `Identify()` (somente leitura) |

---

## Classes HRTEM / STEM / Potential

Essas três classes de simulação de imagem compartilham muitos membros. Para evitar repetições, as tabelas abaixo usam marcadores de posição:

- **`#`** : comum a **HRTEM**, **STEM** e **Potential**. Substitua `#` por `HRTEM`, `STEM` ou `Potential` (por exemplo, `STEM.Simulate()`, `Potential.AccVol`).
- **`$`** : comum apenas a **HRTEM** e **STEM**. Substitua `$` por `HRTEM` ou `STEM`.
- Os membros escritos com um nome de classe explícito (`STEM.…` / `HRTEM.…`) pertencem somente a essa classe. A classe **Potential** não adiciona membros próprios; ela usa apenas os membros `#`.

### Controle da janela

| Função | Descrição |
|----------|-------------|
| `#.Open()` | Abrir a janela do Simulador de imagem |
| `#.Close()` | Fechar a janela do Simulador de imagem |
| `#.Simulate()` | Executar a simulação com as configurações atuais |

### Microscópio / óptica

| Propriedade / Função | Descrição |
|---------------------|-------------|
| `#.AccVol` | Tensão de aceleração (kV) |
| `$.Thickness` | Espessura da amostra (nm) |
| `$.Defocus` | Desfocagem (nm) |
| `$.Cs` | Aberração esférica Cs (mm) |
| `$.Cc` | Aberração cromática Cc (mm) |
| `$.DeltaV` | Dispersão de energia ΔV, FWHM (eV) |
| `$.Scherzer` | Desfocagem de Scherzer (nm, somente leitura) |
| `STEM.ConvergenceAngle` | Semiângulo de convergência (mrad) |
| `STEM.DetectorInnerAngle` / `STEM.DetectorOuterAngle` | Semiângulo interno/externo do detector anular (mrad) |
| `STEM.EffectiveSourceSize` | Tamanho efetivo da fonte, FWHM (pm) |
| `HRTEM.Beta` | Semiângulo de iluminação β (radianos) |
| `HRTEM.ApertureSemiangle` | Semiângulo da abertura objetiva (radianos) |
| `HRTEM.ApertureShiftX` / `HRTEM.ApertureShiftY` | Deslocamento da abertura objetiva (radianos) |
| `HRTEM.OpenAperture` | Abertura objetiva aberta (true/false) |

### Propriedades da simulação

| Propriedade / Função | Descrição |
|---------------------|-------------|
| `#.NumberOfDiffractedWaves` | Número máximo de ondas difratadas (de Bloch) |
| `#.ImageWidth` / `#.ImageHeight` | Tamanho da imagem (pixels) |
| `#.ImageSize(width, height)` | Definir o tamanho da imagem (pixels) |
| `#.ImageResolution` | Resolução da imagem (nm/pixel) |
| `STEM.AngularResolution` | Resolução angular do feixe convergente (mrad) |
| `STEM.SliceThickness` | Espessura da fatia para o cálculo de TDS (nm) |
| `HRTEM.Mode_LinearImage()` | Usar o modelo de imagem linear (quase coerente) |
| `HRTEM.Mode_TCC()` | Usar o modelo TCC (coeficiente cruzado de transmissão) |

### Modo de imagem única / serial

| Propriedade / Função | Descrição |
|---------------------|-------------|
| `$.SingleImageMode()` | Alternar para o modo de imagem única |
| `$.SerialImageMode(withThickness, withDefocus)` | Alternar para o modo de imagem serial |
| `$.SerialImageThicknessStart` / `Step` / `Num` | Espessura serial: início (nm) / passo (nm) / quantidade |
| `$.SerialImageDefocusStart` / `Step` / `Num` | Desfocagem serial: início (nm) / passo (nm) / quantidade |

### Propriedades da imagem

| Propriedade / Função | Descrição |
|---------------------|-------------|
| `#.UnitCellVisible` | Exibir a célula unitária (true/false) |
| `#.LabelVisible` | Exibir o rótulo da imagem (true/false) |
| `#.LabelSize` | Tamanho da fonte do rótulo |
| `#.ScaleBarVisible` | Exibir a barra de escala (true/false) |
| `#.ScaleBarLength` | Comprimento da barra de escala (nm) |
| `#.GaussianBlurEnabled` | Aplicar desfoque gaussiano (true/false) |
| `#.GaussianBlurFWHM` | FWHM do desfoque gaussiano (pm) |
| `STEM.DisplayBoth()` | Exibir as componentes elástica e TDS |
| `STEM.DisplayElastic()` | Exibir apenas a componente elástica |
| `STEM.DisplayTDS()` | Exibir apenas a componente TDS (inelástica) |

### Salvar imagem

| Propriedade / Função | Descrição |
|---------------------|-------------|
| `#.SaveImageAsPng(filename)` | Salvar como PNG (diálogo se filename for omitido) |
| `#.SaveImageAsTif(filename)` | Salvar como TIFF (diálogo se filename for omitido) |
| `#.SaveImageAsEmf(filename)` | Salvar como metarquivo EMF (diálogo se filename for omitido) |
| `#.SaveIndividually` | No modo serial, salvar cada imagem separadamente (true/false) |
| `#.OverprintSymbols` | Sobrepor célula unitária / rótulos / barra de escala nas imagens salvas (true/false) |

---

## Funções globais

| Função | Descrição |
|----------|-------------|
| `Sleep(ms)` | Aguardar o número de milissegundos especificado |

---

## Veja também

- [20. Macro](index.md)
- [20.2. Exemplos](2-examples.md)
