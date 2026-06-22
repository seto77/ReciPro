# Atalhos de teclado e mouse

O ReciPro associa muitas funções a **combinações de teclas** e a **botões do mouse combinados com teclas modificadoras** — coisas que não ficam visíveis em um botão ou em um menu. Esta página reúne todas elas em um só lugar. A página de cada janela também repete seus atalhos perto do início.

<kbd>F1</kbd> funciona em **todas** as janelas e abre a página correspondente deste manual on-line.

---

## Atalhos válidos em todo o aplicativo

Estes são instalados pela [janela principal](0-main-window.md), mas permanecem ativos enquanto as janelas Visualizador de estrutura, Estereonete, Simulador de difração, Spot ID e Calculadora estiverem em foco.

| Atalho | Ação |
|----------|--------|
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>D</kbd> | Alternar o Simulador de difração |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>V</kbd> | Alternar o Visualizador de estrutura |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>S</kbd> | Alternar a Estereonete |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>T</kbd> | Alternar o Spot ID |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd> + teclas de seta | Girar o cristal um passo nessa direção (mantenha duas setas para uma diagonal) |
| Tocar duas vezes em <kbd>CTRL</kbd> | Alternar a Calculadora |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>R</kbd> | Alternar o sinalizador *Reserved* do cristal selecionado |
| <kbd>CTRL</kbd>+<kbd>ALT</kbd>+<kbd>SHIFT</kbd>+<kbd>C</kbd> | Capturar uma imagem da tela da GUI (ferramenta de desenvolvedor; ative **Capture GUI Components** primeiro) |
| Manter <kbd>CTRL</kbd> pressionado enquanto o ReciPro inicia | Iniciar com o OpenGL desativado (recuperação para problemas gráficos) |

---

## Modelos de interação compartilhados

Quase toda visualização interativa no ReciPro pertence a uma de três famílias. Conhecer a família já indica o comportamento de arrastar/zoom sem precisar memorizar cada janela.

### Visualizações 3-D OpenGL { #3d }

Usadas pelo [Visualizador de estrutura](5-structure-viewer.md), pela [Geometria de rotação](4-rotation-geometry.md), pela esfera 3-D da [Estereonete](6-stereonet.md), pelas [Trajetórias eletrônicas](8-electron-trajectory.md) e pelas visualizações de geometria / master pattern do [EBSD](12-ebsd-simulation.md).

| Ação | Resultado |
|--------|--------|
| Arrastar com o botão esquerdo | Girar — trackball perto do centro, rolagem no plano perto da borda |
| Arrastar com o botão direito para cima/baixo, ou roda do mouse | Zoom |
| Arrastar com o botão do meio | Deslocar (somente onde estiver habilitado) |
| <kbd>CTRL</kbd> + arrastar com o botão direito para cima/baixo | Alterar a distância da câmera (somente no modo perspectiva) |
| <kbd>CTRL</kbd> + clique duplo direito | Alternar entre projeção ortográfica / perspectiva |

Janelas individuais podem desativar o deslocamento ou o zoom (por exemplo, nas Trajetórias eletrônicas e nas visualizações 3-D do EBSD o deslocamento está desativado).

### Visualizações de padrão de difração { #pattern }

Usadas pelo padrão do [Simulador de difração](7-diffraction-simulator/index.md), pelo padrão de Kikuchi do [EBSD](12-ebsd-simulation.md) e pela [Estereonete](6-stereonet.md) 2-D. A diferença essencial em relação às visualizações 3-D: **arrastar gira o próprio cristal**, não apenas a câmera, de modo que cada janela vinculada é atualizada em conjunto.

| Ação | Resultado |
|--------|--------|
| Arrastar com o botão esquerdo perto do centro | Inclinar o cristal |
| Arrastar com o botão esquerdo na área externa | Girar o cristal em torno do eixo de visão/feixe |
| Clique direito | Reduzir o zoom |
| Arrastar uma caixa com o botão direito | Ampliar o zoom na região selecionada |
| Arrastar com o botão do meio | Deslocar |

Nessas visualizações **não** há zoom pela roda do mouse.

### Visualizações de imagem { #image }

Usadas pelos painéis de resultado do [HRTEM/STEM](9-hrtem-stem-simulator/index.md), pela imagem do [Spot ID v2](11-spot-id-v2.md) e pelo master pattern 2-D do [EBSD](12-ebsd-simulation.md).

| Ação | Resultado |
|--------|--------|
| Arrastar com o botão esquerdo / Arrastar com o botão do meio | Deslocar |
| Roda do mouse para cima / baixo | Ampliar (×2) / reduzir (×0.5) o zoom no cursor |
| Arrastar uma caixa com o botão direito | Ampliar o zoom na região selecionada |
| Clique direito / clique duplo direito | Reduzir o zoom (×0.5) |

---

## Referência por janela

### 0. Janela principal
[Abrir página →](0-main-window.md) · além dos atalhos válidos em todo o aplicativo acima.

| Atalho | Ação |
|----------|--------|
| Arrastar com o botão esquerdo o widget de orientação (canto inferior esquerdo) | Girar o cristal |
| Clique duplo direito no widget de orientação | Copiar a imagem do widget para a área de transferência |
| Clique único / clique duplo em um botão de função | Alternar essa janela / forçá-la para a frente |
| Clique direito em um cristal na lista | Menu de contexto (Renomear / Duplicar / Excluir / Exportar CIF…) |
| Clique duplo no rótulo **Current Index** | Mostrar / ocultar a caixa de max-UVW |
| Soltar um arquivo | Carregar uma lista de cristais (`.xml`, `.cdb2`) ou um cristal (`.cif`, `.amc`) |

### 1. Banco de dados de cristais
[Abrir página →](1-crystal-database.md)

| Atalho | Ação |
|----------|--------|
| <kbd>ENTER</kbd> em um campo de busca | Executar a busca |
| Clique em uma linha de resultado | Carregar esse cristal |
| Clique em um elemento no pop-up da tabela periódica | Percorrer seu filtro: ignorar → deve incluir → deve excluir |

### 2. Informação de simetria · 3. Interação do feixe
A Informação de simetria não tem combinações especiais de tecla/mouse. Na Interação do feixe, além de <kbd>F1</kbd> e dos botões **Copy**, o cursor vertical no gráfico **Scattering factors** pode ser arrastado para ler o valor de cada elemento.
[Simetria →](2-symmetry-information.md) · [Interação do feixe →](3-beam-interaction.md)

### 4. Geometria de rotação
[Abrir página →](4-rotation-geometry.md) — seis [visualizações 3-D](#3d) **vinculadas**; girar qualquer uma gira todas as seis em conjunto. As pequenas visualizações *Axes* / *Objects* têm zoom e deslocamento desativados.

### 5. Visualizador de estrutura
[Abrir página →](5-structure-viewer.md) — a visualização principal é uma [visualização 3-D](#3d).

| Atalho | Ação |
|----------|--------|
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>C</kbd> | Copiar a imagem renderizada para a área de transferência |
| Clique duplo esquerdo em um átomo | Mostrar coordenadas, distâncias aos vizinhos mais próximos e ângulos de ligação |
| Arrastar com o botão esquerdo o gizmo de eixos cristalinos | Girar o modelo (sem rotação no plano) |
| Arrastar com o botão esquerdo o gizmo de luz | Alterar a direção da iluminação |

### 6. Estereonete
[Abrir página →](6-stereonet.md) — a rede 2-D é uma [visualização de padrão de difração](#pattern); a esfera 3-D opcional é uma [visualização 3-D](#3d).

| Atalho | Ação |
|----------|--------|
| Clique duplo esquerdo na rede | Alternar entre projeção **Plane** e **Axis** |
| Mover o mouse sobre a rede | Ler o (hkl)/[uvw] sob o cursor |

### 7. Simulador de difração
[Abrir página →](7-diffraction-simulator/index.md) — o padrão é uma [visualização de padrão de difração](#pattern) (sem zoom pela roda do mouse).

| Atalho | Ação |
|----------|--------|
| Clique duplo esquerdo em um ponto | Mostrar detalhes da reflexão (índice, *d*, fator de estrutura, erro de excitação) |
| <kbd>CTRL</kbd> + arrastar com o botão do meio | Mover o centro do detector (quando a área do detector é exibida) |
| Clique duplo direito na barra de status | Copiar um resumo em texto das configurações atuais |
| Clique duplo direito em um botão de camada ativo (Spots / Kikuchi / Debye / Scale) | Fazer essa camada piscar |
| Clique duplo esquerdo na estereonete — janela **TEM holder** | Definir a inclinação do suporte para esse ponto |
| Teclas de seta — janela **TEM holder** | Avançar a inclinação do suporte em passos (marque **Arrow keys** antes) |
| Soltar `.prm` / imagem — **Detector geometry**, ou `.txt` — **Dynamic compression** | Carregar esses dados |

### 8. Trajetórias eletrônicas
[Abrir página →](8-electron-trajectory.md) — uma [visualização 3-D](#3d) com o deslocamento desativado.

### 9. Simulador HRTEM / STEM
[Abrir página →](9-hrtem-stem-simulator/index.md) — os painéis de resultado são [visualizações de imagem](#image) e deslocam/dão zoom em conjunto.

| Atalho | Ação |
|----------|--------|
| <kbd>CTRL</kbd>+<kbd>C</kbd> (grade de imagens em foco) | Copiar a(s) imagem(ns) para a área de transferência como metarquivo |
| <kbd>CTRL</kbd> + arrastar uma caixa com o botão direito | Selecionar uma área retangular |
| Clique duplo esquerdo em um painel | Maximizar esse painel / restaurar a grade (layouts de múltiplos painéis) |

### 10. Spot ID v1
[Abrir página →](10-spot-id.md) — a imagem é apenas de referência (não interativa).

| Atalho | Ação |
|----------|--------|
| Clique duplo em uma linha da lista de resultados | Selecionar esse cristal e girá-lo para o eixo de zona correspondente |

### 11. Spot ID v2
[Abrir página →](11-spot-id-v2.md) — a imagem é uma [visualização de imagem](#image) com edição de pontos por cima.

| Atalho | Ação |
|----------|--------|
| Clique duplo esquerdo na imagem | Adicionar um ponto (ajustado ao pico) |
| <kbd>CTRL</kbd> + clique duplo esquerdo | Adicionar um ponto e marcá-lo como feixe direto (000) |
| Clique esquerdo em um ponto | Selecionar o ponto mais próximo |
| <kbd>CTRL</kbd> + clique direito em um ponto | Excluir o ponto mais próximo |
| <kbd>CTRL</kbd> + teclas de seta | Deslocar o ponto selecionado em um pixel |
| Clique duplo no cabeçalho da linha de um ponto | Dar zoom nesse ponto (×2) |

### 12. Simulação EBSD
[Abrir página →](12-ebsd-simulation.md) — o padrão de Kikuchi é uma [visualização de padrão de difração](#pattern); as visualizações 3-D são [visualizações 3-D](#3d) (deslocamento desativado); o master pattern 2-D é uma [visualização de imagem](#image).

| Atalho | Ação |
|----------|--------|
| Clique duplo no padrão de Kikuchi | Selecionar a subcélula do detector sob o cursor e mostrar suas estatísticas |

### 20. Macro
[Abrir página →](20-macro/index.md)

| Atalho | Ação |
|----------|--------|
| <kbd>CTRL</kbd>+<kbd>S</kbd> | Salvar o texto do editor de volta na entrada selecionada da lista de macros |
| <kbd>F10</kbd> | Avançar um passo (durante a execução passo a passo) |
| Clique duplo em uma linha da lista de ajuda de funções | Inserir a assinatura dessa função no ponto de inserção |
| Soltar um arquivo `.mcr` | Carregá-lo no editor |
