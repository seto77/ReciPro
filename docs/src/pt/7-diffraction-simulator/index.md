---
title: Diffraction Simulator
---

# Crystal Diffraction (Simulador de difração)

**Crystal Diffraction (Simulador de difração)** simula padrões de difração de raios X, de nêutrons e de elétrons de monocristal.

![Simulador de difração](../../assets/cap-pt-auto/FormDiffractionSimulator.png)

A janela tem uma área de desenho do padrão de difração à **esquerda** e, à **direita**, os painéis de configuração das propriedades dos reflexos (comprimento de onda, feixe incidente, cálculo de intensidade, aparência, etc.). A combinação de comprimento de onda e feixe incidente determina o modo de aquisição (difração de raios X, SAED, PED, CBED), e os painéis à direita são reconfigurados de acordo.

---

## Como esta página e as páginas de modo dividem o trabalho

- **Esta página (hub)**: reúne as operações comuns a todos os modos (atalhos, menus, barra de ferramentas, informações de tela/detector, abas de overlay, informações dos reflexos, geometria do detector, compressão dinâmica).
- **Cada página de modo**: cobre **todas as configurações que aparecem à direita** quando esse modo é selecionado (comprimento de onda, feixe incidente, cálculo de intensidade, aparência, configurações de ondas de Bloch, configurações de precessão, etc.), de modo que cada página seja autossuficiente (há alguma sobreposição entre os modos).

| Modo | Conteúdo | Página |
|------|----------|------|
| **Difração de raios X (e de nêutrons)** | Padrão de difração de raios X / nêutrons de monocristal (paralelo, raios X em precessão, Back Laue) | [Simulação de difração de raios X](5-x-ray-neutron-diffraction.md) |
| **SAED** | Difração de elétrons com feixe paralelo (selected-area electron diffraction) | [Simulação SAED](1-saed-simulation.md) |
| **PED** | Difração de elétrons em precessão | [Simulação PED](2-ped-simulation.md) |
| **CBED** | Difração de elétrons com feixe convergente | [Simulação CBED](3-cbed-simulation.md) |

> **ALCHEMI não é um modo.** Ao contrário de raios X / SAED / PED / CBED, determinados pela combinação de comprimento de onda e feixe incidente, o simulador ALCHEMI é uma **janela filha** aberta pelo menu **Opções**, por isso é listado aqui separadamente.

| Janela filha | Conteúdo | Página |
|------|------|------|
| **Simulador ALCHEMI** | Curvas de rocking de ionização resolvidas por sítio (microanálise realçada por canalização) | [Simulação ALCHEMI](4-alchemi-simulation.md) |

---

## Referência rápida de modos

Localize a página de que precisa a partir da combinação de **comprimento de onda (fonte)** e **feixe incidente**.

| Comprimento de onda | Feixe incidente | Modo | Página |
|------------|--------------------|------|------|
| Elétron | Feixe paralelo | SAED | [Simulação SAED](1-saed-simulation.md) |
| Elétron | Precessão (elétrons) | PED | [Simulação PED](2-ped-simulation.md) |
| Elétron | Convergência (elétrons) | CBED | [Simulação CBED](3-cbed-simulation.md) |
| Raios X | Feixe paralelo | Difração de raios X | [Simulação de difração de raios X](5-x-ray-neutron-diffraction.md) |
| Raios X | Precessão (raios X, ZOLZ) | Raios X em precessão (câmera de precessão) | [Simulação de difração de raios X](5-x-ray-neutron-diffraction.md) |
| Raios X | Retro-Laue (raios X) | Laue de retroreflexão | [Simulação de difração de raios X](5-x-ray-neutron-diffraction.md) |
| Nêutron | Feixe paralelo | Difração de nêutrons | [seção de nêutrons da Simulação de difração de raios X](5-x-ray-neutron-diffraction.md) |

> **Nota**: As opções de feixe incidente (**Óptica**) mudam com o comprimento de onda. Para elétrons: **Feixe paralelo, Precessão (elétrons), Convergência (elétrons)**; para raios X: **Feixe paralelo, Precessão (raios X, ZOLZ), Retro-Laue (raios X)**; para nêutrons: apenas **Feixe paralelo**. Mudar a fonte de ondas sempre redefine a óptica para **Feixe paralelo**. Selecionar **Precessão (elétrons)** ou **Convergência (elétrons)** alterna automaticamente o cálculo de intensidade para **Teoria dinâmica**; a **Teoria dinâmica** só é oferecida para elétrons (ao selecionar raios X ou nêutrons, o cálculo volta para **Cinemático & erro exc.**). O Retro-Laue (Back Laue) usa um feixe branco (policromático).

---

## Atalhos de teclado e mouse

Estes se aplicam à janela do padrão de difração compartilhada pelas simulações de raios X, SAED e PED. Arrastar sobre o padrão gira o **cristal**. Use o zoom com a roda do mouse ou com clique direito / arrastar com o botão direito.

| Atalho | Ação |
|----------|--------|
| <kbd>F1</kbd> | Abrir esta página do manual online |
| Arrastar com o botão esquerdo perto do centro | Inclinar o cristal |
| Arrastar com o botão esquerdo na área externa | Girar o cristal em torno do eixo do feixe |
| Clique duplo esquerdo em um reflexo | Mostrar detalhes do reflexo (índice, *d*, fator de estrutura, erro de excitação) |
| Arrastar com o botão do meio | Deslocar o padrão |
| <kbd>CTRL</kbd> + Arrastar com o botão do meio | Mover o centro do detector (quando a área do detector está sendo exibida) |
| Roda do mouse | Ampliar / reduzir o zoom por ×2 a cada passo da roda; o ponto sob o cursor é levado ao centro da vista (com **Fixar** marcado, o centro fixado é mantido) |
| Clique direito | Reduzir o zoom (×1/1.2) |
| Arrastar com o botão direito um retângulo | Ampliar o zoom na região selecionada |
| Clique duplo direito na barra de status | Copiar um resumo em texto das configurações atuais |
| Clique duplo direito em um botão de camada ativo (Spots / Kikuchi / Debye / Scale) | Piscar essa camada ligando e desligando |

As janelas auxiliares abertas a partir daqui adicionam mais alguns:

| Atalho | Ação |
|----------|--------|
| Clique duplo esquerdo na estereonete — **TEM holder** | Ajustar a inclinação do suporte para esse ponto |
| Teclas de seta — **TEM holder** | Avançar a inclinação do suporte passo a passo (marque **Ativar teclas de seta** primeiro; funciona com qualquer janela do ReciPro em primeiro plano) |
| Soltar um arquivo `.prm` ou uma imagem — **Detector geometry** | Carregar geometria do detector / imagem de overlay |
| Soltar um perfil `.txt` — **Dynamic compression** | Carregar perfis de posição/tempo/pressão (escolha o tempo com o controle deslizante **Tempo**; arraste as linhas verticais no gráfico para definir a frente / a traseira) |

Os atalhos <kbd>CTRL</kbd>+<kbd>SHIFT</kbd> de toda a aplicação, da janela principal, também funcionam enquanto esta janela está em foco (consulte [janela principal](../0-main-window.md)).

→ Consulte **[21. Atalhos de teclado e mouse](../21-shortcuts.md)** para todas as janelas de uma só vez.

---

## Rotas rápidas por objetivo

| Objetivo | Começar por | Referência |
|------|------------|-----------|
| Produzir difração de elétrons com feixe paralelo (SAED) | Definir **Óptica** como **Feixe paralelo** e a **Fonte de ondas** como **Elétrons** | [Simulação SAED](1-saed-simulation.md), [cálculo de SAED com feixe paralelo](../appendix/a3-bloch-wave/calculation.md) |
| Produzir difração de raios X de monocristal | Alternar a **Fonte de ondas** para **Raios X** / Síncrotron | [Simulação de difração de raios X](5-x-ray-neutron-diffraction.md) |
| Produzir difração de elétrons em precessão (PED) | Definir **Óptica** como **Precessão (elétrons)** e, em seguida, definir o semiângulo e o passo | [Simulação PED](2-ped-simulation.md) |
| Produzir difração de elétrons com feixe convergente (CBED) | Definir **Óptica** como **Convergência (elétrons)** e definir as condições na janela CBED | [Simulação CBED](3-cbed-simulation.md), [cálculo CBED](../appendix/a3-bloch-wave/cbed.md) |
| Inspecionar a lista de reflexos do cálculo dinâmico | Selecionar **Teoria dinâmica** e clicar em **Detalhes dos pontos** | [Cálculo dinâmico (núcleo compartilhado)](../appendix/a3-bloch-wave/calculation.md) |
| Comparar a geometria do detector com uma imagem experimental | Abrir as configurações de geometria do detector em **Details**, no grupo **Geometria detector & imagem sobreposta**, e usar a imagem de overlay | [Sistema de coordenadas do detector](../appendix/a1-coordinate-system/2-diffraction.md) |

---

## Área principal

O padrão de difração é simulado no centro da tela.

### Operação do mouse

Consulte "Atalhos de teclado e mouse" no topo desta página.

### Posição do mouse

A linha **Posição do mouse:** acima do padrão mostra, para a posição do cursor, *d* (nm), *d*⁻¹ (nm⁻¹) e 2θ (em graus e em rad, ou em mrad abaixo de 0.1 rad). Marcar **Details** adiciona três leituras de coordenadas: as coordenadas do detector (mm, origem = pé da perpendicular a partir da amostra), as coordenadas no espaço real (mm, origem = cristal) e as coordenadas no espaço recíproco (nm⁻¹). No modo Retro-Laue, os valores de *d* e do espaço recíproco são exibidos como "-".

---

## Menu File

![Menu Arquivo](../../assets/cap-pt-auto/FormDiffractionSimulator.toolStripContainer1.menuStrip1.fileToolStripMenuItem.png)

| Item de menu | Descrição |
|-----------|-------------|
| **Salvar imagem** | Salvar o padrão de difração exibido, **como Imagem (formato PNG)** ou **como metarquivo (EMF)**. |
| **Salvar área do detector** | Salvar apenas o recorte da área do detector (PNG / EMF). Exibido apenas enquanto a área do detector estiver sendo mostrada. |
| **Salvar discos CBED** | Apenas no modo CBED: salvar os discos calculados **como Imagens individuais (formato PNG / TIFF)** ou **como imagem coletiva (formato PNG / TIFF)**. |
| **Copiar** | Copiar a imagem exibida para a área de transferência (**como Imagem** / **como metarquivo (EMF)**). |
| **Copiar área do detector** | Copiar apenas o recorte da área do detector. Exibido apenas enquanto a área do detector estiver sendo mostrada. |
| **Copiar discos CBED** | Apenas no modo CBED: copiar os discos **como imagem coletiva**. |
| **Salvar vídeo** | Gravar um vídeo do **Padrão de difração** ou da vista do **Espaço recíproco**. |
| **Configurar página / Visualizar impressão / Imprimir** | Imprimir o padrão exibido. |

### Preset

![Menu Predefinições](../../assets/cap-pt-auto/FormDiffractionSimulator.toolStripContainer1.menuStrip1.presetToolStripMenuItem.png)

Predefinições fixas de fonte de ondas que definem apenas a radiação e sua energia: **Elétrons 300 kV**, **Elétrons 200 kV**, **Elétrons 120 keV**, **Raios X 30 keV**, **Raios X 20 keV**, **Raios X 17.48 keV (MoKα₁)** e **Raios X 8.041 keV (CuKα₁)** (as duas últimas selecionam a linha característica Kα₁ do Mo / Cu). As demais configurações (geometria do detector, abas, propriedades dos reflexos) não são armazenadas nem alteradas.

### Menus Opções e Ajuda

| Item de menu | Descrição |
|-----------|-------------|
| **Opções > Compressão dinâmica** | Abre a janela de [Compressão dinâmica](#dynamic-compression) (junto com a janela de geometria do detector). |
| **Opções > Simulador ALCHEMI...** | Abre o [Simulador ALCHEMI](4-alchemi-simulation.md). |
| **Ajuda > Conceito básico do método de Bethe** | Abre no navegador o [apêndice sobre ondas de Bloch](../appendix/a3-bloch-wave/index.md) do manual online. |

---

## Barra de ferramentas {#toolbar}

![Barra de ferramentas](../../assets/cap-pt-auto/FormDiffractionSimulator.toolStripContainer1.toolStrip3.png)

| Botão | Descrição |
|--------|-------------|
| Pontos | Mostrar / ocultar a camada de reflexos de difração |
| Linhas de Kikuchi | Mostrar / ocultar a camada de linhas de Kikuchi |
| Anéis de Debye | Mostrar / ocultar a camada de anéis de Debye |
| Escala | Mostrar / ocultar a camada de linhas de escala |
| Index / d / 1/d / Distância / 2θ / χ / Erro excit. / \|Fg\| | Escolha do rótulo anexado a cada reflexo |

---

## Informações de tela e detector

### Tela

![Tela](../../assets/cap-pt-auto/FormDiffractionSimulator.toolStripContainer1.flowLayoutPanel6.groupBoxMonitor.png)

| Item | Descrição |
|------|-------------|
| **Resolução** | O tamanho de um pixel da tela, em **mm/px** (no plano do detector) ou **nm⁻¹/px** (selecionável; padrão 0.08 mm/px). Não precisa corresponder ao tamanho real do pixel do detector; é tratado como uma escala de exibição e atualizado automaticamente quando você aplica zoom com o mouse. |
| **Tamanho (L×A)** | Largura e altura em pixels da área de desenho. Dependendo da resolução do seu monitor, valores muito grandes podem não ser ajustáveis. |
| **Definir o centro em / Fixar** | Coloca o centro da área de desenho no **perpendicular foot** (pé da perpendicular), no **direct spot** (reflexo direto) ou no centro do **detector** (este último apenas quando uma área de detector está definida). Com **Fixar** marcado (padrão), o centro permanece ali: o deslocamento com o mouse é desativado e o zoom mantém esse centro. |
| **Espelhar horizontal / Espelhar vertical / Imagem negativa** | Espelhamentos geométricos (horizontal / vertical) e inversão de contraste do padrão exibido. Use-os para corresponder à orientação ou ao contraste de uma imagem experimental. |
| **Espaço recíproco** | Abre, ao lado do padrão, uma vista 3D da esfera de Ewald e dos pontos da rede recíproca, visualizando quais reflexos estão excitados. Seu painel **Opções** define as cores (pontos perto / longe da esfera de Ewald, esfera de Ewald, origem, guia de direção), o raio 3D dos pontos (nm⁻¹), o limiar de intensidade relativa (%), a transparência dos pontos longe da esfera de Ewald, os rótulos e a direção de visualização (botões de seta com um **Passo** angular, **Redefinir**). |

### Detector (comprimento de câmera)

![Detector (comprimento de câmera)](../../assets/cap-pt-auto/FormDiffractionSimulator.toolStripContainer1.flowLayoutPanel6.panelDetectorAndMisc.groupBoxDetectorGeometry.png)

- **Comprimento de câmara 2** : Distância (mm) da amostra ao pé da perpendicular baixada sobre o plano do detector (padrão 1000 mm). Coincide com o comprimento de câmera usual quando o detector não está inclinado (τ = 0).
- **Details** : Abre a janela de configurações de geometria do detector (consulte [Geometria do detector](#detector-geometry) abaixo).

### Diversos {#misc}

![Diversos](../../assets/cap-pt-auto/FormDiffractionSimulator.toolStripContainer1.flowLayoutPanel6.panelDetectorAndMisc.groupBoxMisc.png)

- **Sensib. de rotação** : Quantidade de rotação do cristal por pixel de arrasto do mouse.
- **Simulação do suporte TEM** : Abre a janela de simulação vinculada ao suporte (veja abaixo).

---

## Simulação do suporte TEM

![Simulação do suporte TEM](../../assets/cap-pt-auto/FormDiffractionSimulatorHolder.png)

Abre uma janela que vincula o padrão de difração a um **suporte TEM** de dupla inclinação (**Inclinação X** = rotação primária, **Inclinação Y** = rotação secundária). Definir os ângulos de inclinação do suporte atualiza o padrão e a orientação do cristal, e as orientações alcançáveis podem ser exibidas em uma estereonete (adicionado na v4.914). **Vincular à Direção Atual** toma a orientação atual do cristal como referência do suporte (nos valores dados de Inclinação X / Inclinação Y); **Configurações de TEM** definem a direção do eixo de Inclinação X na tela (padrão −29°) e a polaridade da Inclinação Y, e **↻ girar 180°** gira o cristal 180° em torno do feixe. Um clique duplo esquerdo na estereonete ajusta a inclinação do suporte para esse ponto, e marcar **Ativar teclas de seta** permite que ←/→ avancem a Inclinação X e ↑/↓ a Inclinação Y passo a passo (passo 0.1–2°, padrão 0.2°). Enquanto esta janela estiver aberta, as teclas de seta funcionam seja qual for a janela do ReciPro em primeiro plano.

---

## Abas de overlay de desenho {#drawing-overlay-tabs}

### General

![Aba Geral](../../assets/cap-pt-auto/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageGeneral.png)

Define o **Tam. texto** dos rótulos e as cores do **Texto** dos rótulos, do marcador **Pé da perpendicular a partir da amostra**, do marcador **Posição direta** e do **Fundo** (os dois marcadores podem ser desligados individualmente). As cores dos pontos são definidas em **Aparência** (consulte as páginas de modo); as cores das linhas de Kikuchi, dos anéis de Debye e das linhas de escala, nas respectivas abas. As configurações feitas aqui se aplicam a todos os modos de renderização.

### Linhas de Kikuchi

![Aba Kikuchi](../../assets/cap-pt-auto/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageKikuchi.png)

![Separador Kikuchi (modo Dinâmico)](../../assets/cap-pt-auto/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageKikuchi-dynamical.png)

Ativa quando as linhas de Kikuchi estão habilitadas na barra de ferramentas.

- **Reflection selection** : Escolhe quais reflexos geram as linhas de Kikuchi. Ou **Fator de estrutura** (os *N* reflexos principais por $\lvert F_{hkl}\rvert$, com *N* definido em **Máx.**; 1–1000, padrão 100) ou **Corte de 1/d** (todos os reflexos cujo 1/d está abaixo do limiar; 0–100 nm⁻¹, padrão 10).
- **Modo** : Como as linhas de Kikuchi são desenhadas (padrão **Cinemático**). Os ajustes abaixo da caixa de combinação mudam com o modo.
    - **Geométrico** : todas as linhas selecionadas em uma cor fixa.
    - **Cinemático** : a escuridão da linha segue a intensidade relativa cinemática de cada reflexão.
    - **Dinâmico** : perfis de banda excesso/deficiente dinâmicos de dois feixes alimentados por espalhamento térmico difuso, calculados em segundo plano. As bandas de excesso aparecem no lado de ângulos altos e os traços deficientes na direção do feixe direto; a espessura da amostra é compartilhada com os ajustes de intensidade. As linhas geométricas são desenhadas enquanto o cálculo decorre.
- **Ajustes Geométrico / Cinemático** : **Cor das linhas de Kikuchi** e **Largura da linha**.
- **Ajustes Dinâmico** :
    - **Excesso** / **Deficiente** : cores do lado claro (contraste positivo) e escuro (contraste negativo) das bandas.
    - **Bandas** : Número de bandas desenhadas com perfis dinâmicos (as *N* melhores por contraste dinâmico; 1–50, padrão 30).
    - **Qualidade** : Pontos de amostragem através de cada banda (Rápido 65 / Padrão 129 / Alta 257; padrão **Padrão**).
    - **Contraste** : Ganho de exibição aplicado ao contraste da banda antes da compressão.
    - **Escala** : Curva de compressão de brilho (Linear / Log / Tanh; padrão Tanh). São transformações de visualização, não respostas físicas de câmera.
    - **Brilho fixo** : Congela a normalização de brilho, p. ex. para comparar orientações ou espessuras diferentes.
    - A linha de aviso cinza indica a marca de desequilíbrio fonte/perda, o número de bandas desenhadas e diagnósticos (p. ex. fator *B* padrão substituído em átomos com *B* = 0).
- (O antigo parâmetro separado **Threshold** não existe mais; os reflexos são limitados apenas pela seleção acima.)

### Anéis de Debye

![Aba Debye](../../assets/cap-pt-auto/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageDebye.png)

Ativa quando os anéis de Debye estão habilitados na barra de ferramentas.

- **Ignorar intensidade de difração** : Se marcado, todos os anéis de Debye são desenhados com a mesma cor e intensidade (ignorando o fator de estrutura do cristal). Use-o para uma comparação puramente geométrica.
- **Mostrar rótulos de índices** : Se marcado, o (*hkl*) aparece próximo de cada anel.

### Scale

![Aba Escala](../../assets/cap-pt-auto/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageScale.png)

Ativa quando as linhas de escala estão habilitadas na barra de ferramentas.

- **Linha de escala 2θ / Linha de escala azimutal** : a linha **2θ** representa ângulo de espalhamento constante (círculos concêntricos), a linha **azimutal** representa ângulo de azimute constante (linhas radiais a partir do centro). As cores são configuráveis de forma independente.
- **Largura da linha** : Espessura das linhas de escala.
- **Divisão** : Densidade das linhas de escala, **Fina** / **Média** (padrão) / **Grossa**.
- **Mostrar rótulos de escala** : Se rótulos numéricos devem ser desenhados nas linhas de escala.

(Não há uma aba *Misc* separada; a **Sensib. de rotação** do mouse fica no grupo **Diversos** do painel à direita, veja [Diversos](#misc) acima.)

---

## Informações dos reflexos de difração {#diffraction-spot-information}

Lista os detalhes por reflexo calculados pelo método de ondas de Bloch (cálculo Dynamical). Abra-o com o botão **Detalhes dos pontos** no painel de cálculo de intensidade. Nos modos **Apenas erro de exc.** / **Cinemático & erro exc.**, o botão lista os reflexos desenhados no momento com seus potenciais eletrônicos e $P_g$, $Q_g$ (sem amplitudes dinâmicas); no modo CBED, a tabela segue a espessura selecionada na janela CBED.

![Informações dos reflexos de difração](../../assets/cap-pt-auto/FormDiffractionSpotInfo.png)

### Esquema e definições

O esquema (canto superior esquerdo) mostra os vetores na esfera de Ewald e define as grandezas usadas na tabela ($\hat{\mathbf{n}}$ é o vetor unitário normal à superfície da amostra, $\mathbf{k}$ é o vetor de onda incidente, $\mathbf{g}$ é o vetor da rede recíproca).

- $P_g = 2\,\hat{\mathbf{n}} \cdot (\mathbf{k} + \mathbf{g})$
- $Q_g = |\mathbf{k}|^2 - |\mathbf{k} + \mathbf{g}|^2 = -\mathbf{g} \cdot (2\mathbf{k} + \mathbf{g})$
- **Erro de excitação:** $S_g = \dfrac{\sqrt{P_g^2 + 4 Q_g} - P_g}{2}$
- **Função de avaliação:** $R = |\mathbf{g}|\, Q_g^2$ — ordena os reflexos de acordo com a intensidade com que são excitados (menor = mais próximo da esfera de Ewald = excitado mais fortemente; o feixe transmitido $g=0$ tem $R=0$ e vem primeiro). A tabela é ordenada por $R$ crescente.

### Colunas da tabela

| Coluna | Significado |
|--------|---------|
| **R** | função de avaliação $R = \lvert\mathbf{g}\rvert\, Q_g^2$ (acima; usada para selecionar / ordenar reflexos) |
| **h, k, (i,) l** | índices de Miller (*i* é o índice hexagonal redundante, mostrado apenas para cristais hexagonais) |
| **d** | distância interplanar (nm) |
| **gX, gY, gZ** | componentes do vetor da rede recíproca *g* (1/nm) |
| **\|g\|** | magnitude de *g* (1/nm) |
| **Vg re / Vg im** | coeficiente de Fourier do potencial do cristal para espalhamento elástico, $V_g$ (real / imaginário) |
| **V'g re / V'g im** | potencial imaginário (de absorção) para espalhamento térmico difuso (TDS), $V'_g$ (real / imaginário) |
| **Sg** | erro de excitação $S_g$ (acima; 1/nm) |
| **Pg** | grandeza auxiliar $P_g = 2\,\hat{\mathbf{n}}\cdot(\mathbf{k}+\mathbf{g})$ (acima) |
| **Qg** | grandeza auxiliar $Q_g = -\mathbf{g}\cdot(2\mathbf{k}+\mathbf{g})$ (acima) |
| **Φ re / Φ im** | amplitude complexa $\Phi$ da onda difratada dinâmica na superfície de saída (real / imaginário) |
| **\|Φ\|^2** | intensidade difratada $\lvert\Phi\rvert^2$ desse reflexo |
| **Σ\|Φ\|^2** | soma cumulativa de $\lvert\Phi\rvert^2$ (total sobre os reflexos; útil como verificação de conservação de intensidade) |

### Unidades de potencial e outros controles

- **Unit of potential** : Alterna o potencial exibido entre **Vg [eV]** (potencial eletrostático, eV) e **Ug [nm⁻²]** (a grandeza escalonada $U_g = (2 m_0/h^2)\, V_g$ que entra nas equações de ondas de Bloch). Os cabeçalhos das colunas mudam de acordo entre *Vg / V'g* e *Ug / U'g*.
- Acima da tabela são exibidos a tensão de aceleração, o comprimento de onda ($\lambda = 1/k_\text{vac}$), a razão de massa relativística $m/m_0$, a razão de velocidade $v/c$, o volume da rede, a espessura da amostra e (no modo CBED) o semiângulo máximo do feixe de elétrons.
- **Note 1:** a unidade de comprimento é **nm**, não Å. **Note 2:** a unidade de número de onda é **1/nm**, não 2π/nm.
- **Effective digit** : número de algarismos significativos mostrados na tabela (1–16, padrão 4). **Auto resize row width** : ajustar automaticamente as larguras das colunas. **Copy to clipboard** : exporta a tabela como texto que pode ser colado em uma planilha. **Save as CSV** : grava a mesma tabela (as colunas visíveis) em um arquivo CSV; o separador decimal é sempre o ponto, de modo que o arquivo continua válido em locales com vírgula decimal. (Este formulário é exibido em inglês mesmo em uma interface em japonês.)

---

## Geometria do detector {#detector-geometry}

Uma janela para a configuração detalhada da geometria do detector (comprimento de câmera, inclinação, rotação) e a sobreposição de uma imagem experimental. Abra-a a partir de **Details** no painel **Geometria detector & imagem sobreposta**.

![Geometria do detector](../../assets/cap-pt-auto/FormDiffractionSimulatorGeometry.png)

### Configurações de geometria do detector

![Configurações de geometria do detector](../../assets/cap-pt-auto/FormDiffractionSimulatorGeometry.panelDetectorGeometry.png)

Especifique a geometria de reflexão: **Comprimento da câmera 2** (mm, 1–10000, padrão 1000) e a inclinação do detector **τ** (−200 a 200°) em torno de um eixo no plano com azimute **φ**. Para Back Laue (Laue de retroreflexão), apenas o **Comprimento da câmera 2** é usado: o padrão é sempre projetado sobre um detector perpendicular ao feixe, no lado da fonte, e τ / φ não são aplicados a essa projeção. Soltar um arquivo `.prm` carrega dele o comprimento de onda, o comprimento de câmera 2, τ, φ, a posição do pé e o tamanho do pixel (a fonte é alterada para raios X com comprimento de onda personalizado e a área do detector é ativada).

### Área do detector e imagem sobreposta

![Área do detector e imagem sobreposta](../../assets/cap-pt-auto/FormDiffractionSimulatorGeometry.panelDetectorAreaAndOverlappedImage.png)

Marque **Definir área do detector & imagem sobreposta.** para especificar o detector: tamanho em pixels (padrão 1024 × 1024), **tam. pix.** (mm, padrão 0.1) e a posição do pé **Pé: fx, fy** (px, padrão 512, 512). Uma imagem experimental pode ser carregada (**Carregar** ou soltando o arquivo) e sobreposta com **Opacidade**, **Gradiente** (filme positivo/negativo), **Escala 1** (log/linear), **Escala 2** (Gray / Cold-Warm / Spectrum / Fire), **Int. máx./Int. mín.**, **Girar 90°** e **Limpar**. **Diagrama esquemático** mostra um esboço da geometria. Use isto para sobrepor o padrão simulado e uma imagem experimental e ajustar finamente a geometria do detector.

Consulte também [Sistema de coordenadas do detector](../appendix/a1-coordinate-system/2-diffraction.md) para as definições do sistema de coordenadas.

---

## Compressão dinâmica {#dynamic-compression}

![Compressão dinâmica](../../assets/cap-pt-auto/FormDiffractionSimulatorDynamicCompression.png)

Uma janela para simular a difração de uma amostra comprimida por choque (compressão dinâmica). Solte um arquivo `.txt` separado por tabulações com três colunas (posição, tempo, pressão) para carregar perfis de pressão para cada tempo; o controle deslizante **Tempo** (ns) seleciona o perfil mostrado no gráfico. As duas linhas verticais no gráfico (**Frente** / **Trás**) podem ser arrastadas para definir a frente e a traseira da compressão. A janela também reúne o modelo de compressão (**2018 / 2019 model**), as velocidades de choque (**Us**, **Ur**, km/s), a **EOS** (K0, K'0), o **Plano de choque** / **Plano deslizam.**, as distribuições de rotação das áreas comprimida e liberada, o coeficiente de absorção de massa, o passo em Ω e **Salvar padrões** (`#.tif`), e é executada com **Simular**.

---

## Simulador ALCHEMI

Abre uma janela filha que calcula **curvas de rocking de ionização resolvidas por sítio**: o rendimento de um canal de ionização escolhido enquanto o cristal é inclinado através das condições de Bragg de uma fila sistemática, separado por sítio atômico. Abre-se pelo menu **Opções**. Ao contrário da janela CBED, ela tem sua própria área de saída e não desenha na tela da janela principal.

→ Veja **[Simulação ALCHEMI](4-alchemi-simulation.md)** para todas as definições e para o âmbito e as limitações conhecidas.

---

## Tópicos relacionados

- [Simulação de difração de raios X](5-x-ray-neutron-diffraction.md)
- [Simulação SAED](1-saed-simulation.md)
- [Simulação PED](2-ped-simulation.md)
- [Simulação CBED](3-cbed-simulation.md)
- [Simulação ALCHEMI](4-alchemi-simulation.md)
- [Cálculo dinâmico (núcleo compartilhado)](../appendix/a3-bloch-wave/calculation.md)
- [Sistema de coordenadas do detector](../appendix/a1-coordinate-system/2-diffraction.md)
