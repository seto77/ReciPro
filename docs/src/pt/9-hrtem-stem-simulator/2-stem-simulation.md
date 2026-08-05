# Simulação STEM

A **simulação STEM (Scanning Transmission Electron Microscopy)** calcula imagens de microscopia eletrônica de transmissão por varredura usando o método de ondas de Bloch.

![Simulador no modo STEM](../../assets/cap-pt-auto/FormImageSimulator-stem.png)

> Esta página lista todas as configurações que aparecem à direita quando **Image mode = STEM**. Para os controles de exibição do resultado, brilho e normalização à esquerda, consulte a [página de visão geral](index.md). Apenas o **alvo de exibição** específico do STEM é repetido abaixo.

---

## Visão geral

Um feixe eletrônico convergente é varrido sobre a amostra, e os elétrons transmitidos e espalhados em cada posição de varredura são coletados por detectores anulares. O ReciPro calcula a imagem STEM com o método de ondas de Bloch (cálculo dinâmico).

### Fluxo de cálculo

1. Em cada posição de varredura, calcule as intensidades difratadas com o método de ondas de Bloch para cada direção de incidência da sonda convergente.
2. Integre a intensidade espalhada sobre a faixa angular do detector.
3. Tanto as contribuições de espalhamento elástico quanto de espalhamento térmico difuso (TDS) podem ser calculadas.

Consulte o [Apêndice A3.4 — Cálculo STEM](../appendix/a3-bloch-wave/stem.md) para a teoria.

---

## Tipos de detector

| Detector | Faixa angular | Contribuição principal | Contraste |
|----------|-------------|-------------------|----------|
| **BF** (campo claro) | 0 – ângulo de convergência | Elástico | Contraste de fase |
| **ABF** (campo claro anular) | Parte interna do ângulo de convergência | Elástico | Sensível a elementos leves |
| **LAADF** (campo escuro anular de baixo ângulo) | Logo fora do ângulo de convergência | Elástico + TDS | Sensível a deformações |
| **HAADF** (campo escuro anular de alto ângulo) | Bem fora do ângulo de convergência | TDS (inelástico) | Contraste-Z ($\propto Z^2$) |

> **Configurações típicas de detector** (cada uma disponível com um clique no menu de clique direito das opções STEM, todas com ângulo de convergência α = 25 mrad):
> BF (0–5 mrad) / ABF (12–24 mrad) / LAADF (26–60 mrad) / HAADF (80–250 mrad)

---

## Parâmetros da amostra

![Parâmetros da amostra](../../assets/cap-pt-auto/FormImageSimulator.splitContainer1.flowLayoutPanelModeSelection.groupBoxSampleProperty.png)

- **Thickness** : espessura da amostra (nm). Este valor é ignorado no modo **Serial image**.

---

## Condições TEM

![Condições TEM](../../assets/cap-pt-auto/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxTEMConditions.png)

| Parâmetro | Descrição | Padrão / típico |
|-----------|-------------|-------------------|
| **Acc. Vol. (kV)** | Tensão de aceleração. O comprimento de onda do elétron corrigido relativisticamente é exibido ao lado | 200 kV |
| **Defocus Δf** | Desfocagem da lente objetiva (formadora da sonda) (nm) | −57.8 nm |
| **Cs** | Coeficiente de aberração esférica (mm). Afeta o tamanho da sonda | 0.5–1.0 mm |
| **Cc** | Coeficiente de aberração cromática (mm) | 1.0–2.0 mm |
| **ΔV (FWHM)** | Largura a meia altura da dispersão de energia dos elétrons (eV) | 0.5–2.0 eV |

> **β (semiângulo de iluminação) está desativado no modo STEM**, porque o ângulo de convergência α assume o seu papel.

---

## Opções STEM (óptica)

![Opções STEM (óptica)](../../assets/cap-pt-auto/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxSTEMoption1.png)

Defina a geometria da sonda convergente e do detector anular. Cada ângulo também é exibido convertido em um raio no espaço recíproco $\sin\theta/\lambda$ (nm⁻¹) à direita.

| Parâmetro | Descrição | Padrão / típico |
|-----------|-------------|-------------------|
| **α (convergence angle)** | Semiângulo da sonda convergente (mrad). Valores maiores geram uma sonda mais fina e alteram o contraste de difração | 15–25 mrad |
| **(Annular) detector inner angle** | Semiângulo interno de coleta do detector anular (mrad). O sinal dentro desse ângulo é excluído | BF: 0, HAADF: 80 |
| **(Annular) detector outer angle** | Semiângulo externo de coleta do detector anular (mrad). O sinal fora desse ângulo é excluído | BF: 5, HAADF: 250 |
| **Effective source size σs (FWHM)** | Tamanho efetivo da fonte de elétrons. Valores maiores borram a sonda e reduzem o contraste de detalhes finos | — |

---

## Opções STEM (simulação)

![Opções STEM (simulação)](../../assets/cap-pt-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.groupBoxSTEMoption2.png)

- **Slice thickness for inelastic** : espessura de fatia da amostra (nm) usada ao calcular a intensidade TDS (térmico-difuso, inelástico). Valores menores são mais precisos, mas mais lentos.
- **Angular resolution** : resolução de amostragem angular das direções de incidência da sonda (mrad). Valores menores amostram a sonda mais finamente, mas são mais lentos. O número de direções cresce com o quadrado dessa razão, sendo portanto a principal alavanca sobre o tempo de cálculo; veja [Amostragem angular da sonda](../appendix/a3-bloch-wave/stem.md#angular-sampling) para as medidas de convergência.

---

## Modo de imagem (single / serial)

![Modo de imagem](../../assets/cap-pt-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.groupBoxSerialImage.png)

- **Single image** : calcula uma imagem STEM na espessura atual.
- **Serial image** : gera uma série de imagens com a espessura / desfocagem variada em etapas (definidas por **Start / Step / Num**; a lista abaixo também pode ser editada diretamente).

---

## Propriedades da imagem

![Propriedades da imagem](../../assets/cap-pt-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.panelImageProperties.groupBoxImageProperty.png)

- **Size (W×H)** : número de pixels na imagem varrida (padrão 512×512). No STEM isso equivale ao número de pontos de varredura e escala o tempo de cálculo linearmente.
- **Resolution** : resolução de amostragem (pm/px).

---

## Ondas difratadas

![Ondas difratadas](../../assets/cap-pt-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.panelImageProperties.groupBoxDiffractedWaves.png)

- **Max Bloch waves** : número máximo de ondas de Bloch usadas no método de Bethe (padrão 80). O custo do problema de autovalores escala com o cubo do número de ondas.

---

## Alvo de exibição STEM (lado do resultado) {#stem-display-target}

![Imagem STEM](../../assets/cap-pt-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxSTEMoption3.png)

A chave de exibição no canto inferior esquerdo da janela seleciona qual componente de espalhamento da imagem STEM já calculada deve ser mostrado (alternável sem recalcular).

| Alvo de exibição | Descrição |
|----------------|-------------|
| **Elastic** | Imagem somente de espalhamento elástico |
| **TDS** | Imagem somente de espalhamento térmico difuso |
| **Elastic & TDS** | Soma de elástico + TDS |
| **EDX** | Mapa de raios X característicos. A linha a exibir (por exemplo `O-K`) é escolhida na caixa de combinação abaixo, e **EDX comum** em *Normalização* coloca todos os canais em uma única faixa de exibição compartilhada, de modo que trocar de canal não reescala a imagem |

!!! note
    As três imagens são reconstruídas a partir da parte real da soma de Fourier, de modo que **Elastic & TDS** é exatamente a soma das outras duas. Até a versão 4.944 tomava-se o módulo, o que quebrava essa identidade e clareava levemente os pixels escuros. Veja [Reconstrução de uma imagem real](../appendix/a3-bloch-wave/stem.md#real-image-reconstruction).

---

## Mapas elementares STEM-EDX {#stem-edx}

![Mapas elementares STEM-EDX](../../assets/cap-pt-auto/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxSTEMoption1.groupBoxSTEMoption4.png)

Marque **Calcular mapas EDX** para calcular mapas de raios X característicos junto com a imagem do tipo ADF. Não se trata de um modo separado: os sinais elástico, TDS e EDX saem todos da mesma execução STEM, e depois é possível alternar entre eles em [Imagem STEM](#stem-display-target) sem recalcular.

Não há seletor de elementos. Quando a caixa está marcada, **todos os canais elemento/camada que podem ser calculados para este cristal nesta tensão de aceleração** são computados, e a linha abaixo da caixa de seleção os lista (por exemplo `3 mapa(s): O-K, Mg-K, Al-K`). Um canal está disponível quando a borda de ionização fica abaixo da tensão de aceleração e a camada é coberta pelos dados fornecidos com o programa — K: C–Sn (Z = 6–50), L-total: Ca–Rn (Z = 20–86). A tabela fornecida armazena fatores de forma de ionização totalmente relativísticos até um vetor de espalhamento de 8 Å⁻¹ para todos os canais, de modo que as linhas L de elementos pesados até o radônio são simuladas sem extrapolação. Se nada estiver disponível, a execução é recusada com uma mensagem explicativa, em vez de produzir um mapa vazio.

A linha seguinte informa a grade de direções de incidência da sonda, por exemplo `Grade: 132² (recomendado: ≥48²)`. Essa grade é definida pela **Resolução angular** e pelo ângulo de convergência; veja [Amostragem angular da sonda](../appendix/a3-bloch-wave/stem.md#angular-sampling). Abaixo da divisão recomendada, o resíduo hermitiano ±q pode exceder a tolerância e abortar a execução; por isso o valor fica laranja e um diálogo de confirmação aparece antes do início do cálculo.

!!! warning "O que os valores representam"
    O mapa é o **número de vacâncias de camada interna geradas por elétron incidente** — uma grandeza do modelo, não uma contagem prevista de raios X. Rendimento de fluorescência, autoabsorção na amostra, ângulo sólido do detector e eficiência do detector **não** são aplicados. Use os mapas para a distribuição espacial e para comparar espessura ou orientação, não para quantificação absoluta.

### Parâmetros do detector (reservados)

**Autoabsorção**, **Ângulo de saída** e **Detector** estão presentes no painel, mas desativados: pertencem ao modelo de detector que ainda não está implementado. São exibidos para que o painel não mude de posição quando o modelo for incorporado. O efeito que terão difere em natureza:

| Fator | Contraste pixel a pixel em um mapa | Razão entre mapas de elementos |
|---|---|---|
| Autoabsorção (ângulo de saída) | **altera** | **altera** |
| Janela / camada morta / eficiência do detector | sem efeito | **altera fortemente** |
| Ângulo sólido do detector, corrente do feixe, tempo de permanência | sem efeito | sem efeito |

A última linha é o motivo pelo qual o ReciPro não expõe corrente do feixe nem tempo de permanência: eles multiplicam todos os pixels de todos os mapas pelo mesmo número, cancelam-se em qualquer razão e ficam invisíveis após a normalização de exibição.

### Precisão e custo

O STEM-EDX não impõe nenhum limite extra ao número de ondas nem à espessura da fatia: ele percorre os mesmos caminhos de cálculo da imagem do tipo ADF, de modo que as configurações que funcionam para STEM também funcionam para EDX.

A precisão fica a cargo do usuário, exatamente como no caso do número de ondas ou da resolução angular. Como referência, o erro de integração em profundidade cresce aproximadamente em proporção a **Espessura da fatia (TDS)** — cerca de 2–3 % a 1 nm, 4–8 % a 2 nm e 12–23 % a 4 nm (relativo ao pico, SrTiO₃ a 39 nm). Reduzir a espessura da fatia à metade reduz o erro aproximadamente à metade e praticamente dobra o trabalho de integração em profundidade.

---

## Custo computacional

A simulação STEM é computacionalmente cara, portanto defina os parâmetros a seguir adequadamente.

| Fator | Impacto |
|--------|--------|
| **Ângulo de convergência** | Maior → mais sobreposição dos discos CBED → custo maior |
| **Ondas de Bloch** | O custo do problema de autovalores escala com N³ |
| **Resolução angular** | Mais fina → mais precisa, mas o custo escala com N² |
| **Pixels da imagem (Size)** | Escala linear com o número de pontos de varredura |

---

## Importância do fator de temperatura

Para a simulação HAADF-STEM, os átomos devem ter um fator de temperatura isotrópico (fator de Debye-Waller) diferente de zero. Se o valor for desconhecido, defina $B \approx 0.5\ \text{Å}^2$. Com um fator de temperatura nulo, a intensidade TDS é zero e a imagem HAADF não é calculada corretamente.

| Detector | Faixa | Contribuição principal |
|----------|-------|-------------------|
| BF, ABF | Dentro do ângulo de convergência | Elástico |
| LAADF, HAADF | Fora do ângulo de convergência | Inelástico (TDS) |

---

## Comparação com o Dr. Probe

Confirmou-se que as simulações STEM do ReciPro concordam estreitamente com a amplamente utilizada GUI Dr. Probe (v1.10). A figura abaixo compara as duas para os detectores BF, ABF, LAADF e HAADF ao longo de uma série de espessuras (2.96–60.05 nm), tanto sem aberração (esquerda) quanto com Cs = 0.2 mm, desfocagem = −25.9 nm (direita). Os dois códigos concordam em todos os tipos de detector e espessuras.

![Comparação de simulação STEM: Dr. Probe vs ReciPro](../../assets/references/STEM_DrProbe_comparison.png)

Um relatório mais detalhado está disponível em PDF: [Comparação de simulações STEM pela GUI Dr. Probe (v1.10) e ReciPro (v4.854)](https://github.com/seto77/ReciPro/files/10976084/ComparisonSTEMsimulations.pdf).

---

## Veja também

- [Simulador HRTEM/STEM (visão geral)](index.md)
- [Simulação HRTEM](1-hrtem-simulation.md)
- [Simulação de potencial](3-potential-simulation.md)
- [Apêndice A3.4 — Cálculo STEM](../appendix/a3-bloch-wave/stem.md)
