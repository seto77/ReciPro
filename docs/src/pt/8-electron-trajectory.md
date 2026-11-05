# Trajetórias eletrônicas

O **Simulador de trajetórias (método de Monte Carlo)** calcula as trajetórias dos elétrons dentro de uma amostra pelo **método de Monte Carlo**: os elétrons incidentes sofrem espalhamento elástico e inelástico, e as distribuições resultantes dos elétrons retroespalhados (BSE) — direção, energia no escape, profundidade de penetração e espalhamento lateral — são acumuladas.
A [12. Simulação EBSD](12-ebsd-simulation.md) usa o mesmo mecanismo de Monte Carlo para sua ponderação angular/de energia/de profundidade, mas o executa internamente com suas próprias configurações; os resultados desta janela não são repassados a ela.

![Electron Trajectory](../assets/cap-pt-auto/FormTrajectory.png)

A janela tem três colunas: a **vista 3D das trajetórias** à esquerda, as **Estatísticas** e a estereonete da **Distribuição direcional dos BSE** no centro, e três **histogramas** à direita. A composição e a densidade da amostra vêm do cristal selecionado na janela principal; aqui só se definem a energia do feixe, a inclinação da amostra e o número de trajetórias.

---

## Atalhos de teclado e mouse

As trajetórias são exibidas em uma vista 3D OpenGL. Ela usa a [navegação de vista](21-shortcuts.md) padrão do ReciPro, mas **o deslocamento está desativado** — use os botões de predefinição de vista para saltar para as orientações padrão.

| Atalho | Ação |
|----------|--------|
| <kbd>F1</kbd> | Abrir esta página do manual on-line |
| Arrastar com o botão esquerdo | Girar o modelo |
| Arrastar com o botão direito para cima/baixo, ou roda do mouse | Zoom |
| <kbd>CTRL</kbd> + clique duplo com o botão direito | Alternar entre projeção ortográfica / perspectiva |

→ Consulte **[21. Atalhos de teclado e mouse](21-shortcuts.md)** para uma visão geral de todas as janelas.

---

## Condições de cálculo

Os controles na parte superior da janela definem a execução:

- **Simular trajetórias** : inicia a execução de Monte Carlo. A barra de status na parte inferior informa separadamente o tempo decorrido do cálculo das trajetórias, do desenho dos gráficos e da renderização 3D.
- **Number of trajectories** (100–1 000 000, padrão 100 000) : quantos elétrons incidentes rastrear. Mais elétrons reduzem o ruído estatístico de todas as distribuições abaixo, com um tempo de execução que cresce linearmente.
- **Inclinação da amostra** (°, −90 a 0, padrão 0) : inclinação da superfície da amostra em torno do eixo *X*. Deixe em 0 para incidência normal; use **−70°** para reproduzir a geometria do [simulador EBSD](12-ebsd-simulation.md), em que a grande inclinação aumenta o rendimento de retroespalhamento.
- **Energia** (keV) / **Compr. de onda** / **Unit** : a tensão de aceleração do feixe incidente e o comprimento de onda do elétron, com correção relativística, vinculado a ela. A energia define a energia cinética usada tanto pelo modelo elástico (NIST Mott) quanto pelo inelástico (poder de freamento / IMFP).

Alterar **Inclinação da amostra** ou **Energia** depois de uma execução não altera o resultado exibido: até que **Simular trajetórias** seja pressionado novamente, cada redesenho (vista 3D, estereonete, histogramas) usa a inclinação e a energia com que as trajetórias foram calculadas. <!-- 261105Cl 監査: FormTrajectory.cs calcTilt/calcEnergy (9890ced0) -->

Um elétron é rastreado até deixar a amostra ou até sua energia cair para **2 keV** (limiar fixo). Os elétrons que saem com mais de 2 keV são contados como elétrons retroespalhados; η (sua fração em relação a todos os elétrons incidentes), a energia média de BSE, a estereonete e os histogramas abaixo são calculados a partir desses elétrons. A seção de choque, o livre caminho médio e o poder de freamento em **Estatísticas** são valores do modelo na energia do feixe.

Os próprios modelos de espalhamento não podem ser escolhidos pelo usuário: as seções de choque elásticas vêm da tabela NIST Mott incluída (com recurso ao Rutherford blindado fora de seu intervalo), e o poder de freamento da forma modificada de Jablonski (2008). O modelo efetivamente usado é indicado ao lado da seção de choque e do poder de freamento em **Estatísticas**. Consulte [Atenuação e transporte](appendix/a2-beam-interaction/attenuation-transport.md) para saber o que são esses modelos.

### Vista 3D das trajetórias

As trajetórias dos elétrons retroespalhados são desenhadas em **vermelho**, e as dos elétrons absorvidos na amostra em **azul**; ao longo de cada caminho, a cor esmaece em direção ao branco/transparente em 16 passos à medida que o elétron perde energia. O caminho após o escape (veja abaixo) é desenhado como um curto segmento **laranja** cujo comprimento é proporcional à energia de escape. Os círculos-guia concêntricos são rotulados em nm (ou µm), e **+X**, **+Y**, **+Z (=beam)** marcam os eixos.

- **Do eixo Z (=direção do feixe)** / **Do eixo X (eixo de rotação)** / **Da normal à superfície** : alinham a vista com as direções padrão.
- **Número de trajetórias a desenhar** (1–100 000, padrão 500) : quantas das trajetórias calculadas renderizar (desenhar todas as 100 000 seria ilegível e lento). São tomadas as primeiras na ordem de cálculo, pulando os elétrons absorvidos quando estes não são desenhados.
- **Desenhar eixos** / **Desenhar círculos-guia** : as setas dos eixos e a escala de distância.
- **Desenhar trajetórias absorvidas na amostra** : inclui os elétrons que nunca escapam.
- **Desenhar o caminho após o escape** : continua desenhando o caminho de um elétron retroespalhado depois que ele deixou a superfície.

---

## Estatísticas

![Estatísticas](../assets/cap-pt-auto/FormTrajectory.panel2.groupBoxStatistics.png)

Valores para a energia do feixe atual. A seção de choque e o poder de freamento trazem entre colchetes o nome do modelo que os produziu.

- **Seção de choque de dispersão (σ_E)** (nm²) — seção de choque elástica total por átomo.
- **Livre caminho médio elástico (λ)** (nm) — distância média entre eventos de espalhamento elástico.
- **Poder de freamento (dE/ds)** (eV/nm, negativo) — energia perdida por unidade de comprimento de trajetória.
- **Coeficiente de elétrons retroespalhados, η** (%) — a fração dos elétrons incidentes que saem novamente pela superfície de entrada. É a grandeza na qual se baseia o contraste das imagens BSE.
- **Energia média de BSE** (keV) — energia média dos elétrons retroespalhados no momento em que escapam. Exibida como “-” quando nenhum elétron é retroespalhado (os histogramas ficam então vazios). <!-- 261105Cl 監査: FormTrajectory.cs DrawStatistics (count == 0) -->

---

## Distribuição direcional dos BSE

![Distribuição direcional dos BSE](../assets/cap-pt-auto/FormTrajectory.panel2.groupBoxDirectionDistribution.png)

Distribuição angular dos elétrons retroespalhados, desenhada em uma estereonete cujo centro é a direção normal à superfície.

- **Frequência** / **Energia média** / **Desvio padrão da energia** : a grandeza mapeada em cor — quantos elétrons saem em cada direção, sua energia média ou a dispersão dessa energia.
- **Desenhar eixos** : sobrepõe as direções +X / ±Y / ±Z.
- **Min** / **Max**, **Resolution**, **Color** : os limites da escala de cores, o tamanho da classe angular do histograma e o mapa de cores.

---

## Histogramas

![Histogramas](../assets/cap-pt-auto/FormTrajectory.flowLayoutPanelProfiles.png)

Três distribuições dos elétrons retroespalhados. Cada classe contém a fração de todos os elétrons retroespalhados que caem nela (os valores das classes somam 1; não são divididos pela largura da classe).

### Distribuição de energia dos BSE no escape

Histograma da **energia que os elétrons retroespalhados ainda carregam ao deixar a amostra** (keV) — não da sua perda de energia. O simulador EBSD pondera a integração em energia do master pattern com uma distribuição desse tipo, obtida da sua própria execução de Monte Carlo.

### Distância máxima de BSE paralela à superfície

Histograma de quanto cada elétron retroespalhado percorreu **lateralmente** (paralelamente à superfície, nm) antes de escapar. É o tamanho lateral do volume de interação e, portanto, o limite intrínseco de resolução espacial de uma medição BSE ou EBSD.

### Profundidade máxima de penetração de BSE

Histograma da maior profundidade **perpendicular à superfície** (nm) que cada elétron retroespalhado alcançou antes de escapar.

---

## Veja também

- [Simulação EBSD](12-ebsd-simulation.md)
- [Cálculo EBSD](appendix/a3-bloch-wave/ebsd.md)
- [Atenuação e transporte](appendix/a2-beam-interaction/attenuation-transport.md) — as seções de choque elásticas, o poder de freamento e os alcances usados aqui.
- [Difração dinâmica (onda de Bloch)](appendix/a3-bloch-wave/index.md)
- [Simulador HRTEM/STEM](9-hrtem-stem-simulator/index.md)
- [Simulador de difração](7-diffraction-simulator/index.md)
