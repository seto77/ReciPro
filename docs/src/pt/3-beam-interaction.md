# Interação do feixe

A **Interação do feixe** descreve como o cristal selecionado interage com um feixe incidente de **raios X, elétrons ou nêutrons**. Para uma radiação escolhida, ela calcula as reflexões permitidas e seus fatores de estrutura, a atenuação e o transporte do feixe através do material, os fatores de espalhamento atômico de cada elemento e (para raios X) as linhas de fluorescência características. Alternar o tipo de radiação no topo recalcula tudo, de modo que o mesmo cristal pode ser comparado entre técnicas de difração e de espectroscopia.

![Interação do feixe](../assets/cap-pt-auto/FormBeamInteraction.png)

O feixe incidente é selecionado na faixa no topo da janela; as quatro abas abaixo — **Reflexões**, **Atenuação & Transporte**, **Fatores de dispersão** e **Fluorescência** — mostram os diferentes aspectos da interação. Cada seção de aba abaixo mostra a aba sob os feixes **X-ray / Electron / Neutron** (use as abas em cada figura); o conteúdo muda acentuadamente com o feixe.

!!! tip "Fundamentos de física do estado sólido (Apêndice A2)"
    O espalhamento e a física do estado sólido por trás dessas quatro abas — fatores de espalhamento atômico, o fator de estrutura, a atenuação e o transporte do feixe e a fluorescência — são explicados no **[Apêndice A2. Interação do feixe (fundamentos de física do estado sólido)](appendix/a2-beam-interaction/index.md)**.

!!! note "Dados de raios X e a biblioteca xraylib incluída"
    Muitas das grandezas de raios X (dispersão anômala $f'/f''$, a separação de espalhamento $F(q)+S(q)$, a decomposição em foto / Rayleigh / Compton da atenuação de massa, os saltos das bordas de absorção e os rendimentos de fluorescência) são avaliadas com a biblioteca **[xraylib](https://github.com/tschoonj/xraylib)** incluída. Se o xraylib não estiver disponível, o ReciPro recorre às suas tabelas internas (atenuação apenas por fotoabsorção, apenas energias de linhas características) e as células afetadas mostram **N/A**. A linha **fonte** de cada tabela informa qual conjunto de dados foi utilizado.

---

## Atalhos de teclado e mouse

Esta janela não possui combinações de teclas especiais. <kbd>F1</kbd> abre esta página do manual. Na aba **Fatores de dispersão**, a linha vertical do cursor pode ser **arrastada** para ler o fator de espalhamento de cada elemento naquela posição, e cada aba tem um botão **Copiar** que exporta sua tabela como texto colável em planilhas.

→ Consulte **[21. Atalhos de teclado e mouse](21-shortcuts.md)** para ver cada janela de relance.

---

## Feixe e comprimento de onda

A faixa superior é um **Wave Length Control** compartilhado com os outros simuladores.

![Wave Length Control](../assets/cap-pt-auto/FormBeamInteraction.waveLengthControl.png)

- **Raios X / Elétrons / Nêutron** : os fatores de espalhamento atômico e a física da interação diferem conforme o tipo de feixe incidente, por isso são alternados aqui.
- Para **Raios X**, escolher o **Element** (material do anodo) e a linha característica (Kα, etc.) define automaticamente o comprimento de onda desse raio X característico.
- **Energia** (keV) e **Compr. de onda** (Å) estão vinculados; definir um atualiza o outro, e ambos determinam o 2θ usado na tabela **Reflexões**.
- **Unit (Å / nm)** alterna a unidade de comprimento usada para o espaçamento d e grandezas semelhantes.
- Alternar a fonte restaura a energia usada por último com essa fonte (para raios X, também o elemento e a linha). Uma fonte ainda não usada começa em Cu Kα para raios X, 200 kV para elétrons e 25,3 meV (nêutrons térmicos). Esses valores não são mantidos depois que o ReciPro é fechado. <!-- 261106Cl 作者の決定 J18 -->

O feixe escolhido também decide quais abas e curvas são significativas:

| Feixe | Reflexões | Atenuação & Transporte | Fatores de dispersão | Fluorescência |
|------|------|------|------|------|
| **Raios X** | fatores de estrutura incl. dispersão anômala | µ/ρ, µ, transmissão + bordas de absorção (vs energia) | $f(s)$ (Waasmaier–Kirfel ou Temari) ou $F(q)+S(q)$ | linhas características + traços EDX |
| **Elétrons** | fatores de estrutura de elétrons | σ, MFP, \|dE/ds\|, IMFP, alcance (vs energia) | Peng / Kirkland / 8-Gaussians / Temari | — (oculto) |
| **Nêutron** | fatores de estrutura nucleares | comprimentos de espalhamento e seções de choque (sem curva de energia) | comprimentos de espalhamento (sem dependência de *s*) | — (oculto) |

A aba **Fluorescência** existe apenas para raios X e desaparece para feixes de elétrons e de nêutrons. Para nêutrons, os gráficos dependentes da energia em **Atenuação & Transporte** e **Fatores de dispersão** são substituídos por tabelas de elementos, pois o comprimento de espalhamento nuclear não depende do ângulo de espalhamento nem da energia.

---

## Aba Reflexões {#reflections-tab}

Lista os planos cristalinos permitidos (reflexões) do cristal e o **fator de estrutura** e a intensidade de difração de cada um. Para raios X, o fator de estrutura agora inclui os termos de **dispersão anômala** $f'/f''$ na energia atual, de modo que `F_inv` (a parte imaginária) é geralmente não nulo próximo a uma borda de absorção. O layout é o mesmo para cada feixe; apenas os valores do fator de estrutura e o 2θ de cada reflexão mudam.

=== "X-ray"
    ![Reflexões — X-ray](../assets/cap-pt-auto/FormBeamInteraction-xray-reflections.png)

=== "Electron"
    ![Reflexões — electron](../assets/cap-pt-auto/FormBeamInteraction-electron-reflections.png)

=== "Neutron"
    ![Reflexões — neutron](../assets/cap-pt-auto/FormBeamInteraction-neutron-reflections.png)

**Opções**

- **Intensidades de difração de pó (óptica Bragg-Brentano)** : calcula a intensidade relativa como intensidade de difração de pó (Bragg–Brentano), $I \propto m\,\lvert F\rvert^2 / V^2 \times L$, onde $m$ é a multiplicidade, $V$ o volume da célula e $L = (1+\cos^2 2\theta)/(\sin 2\theta\,\sin\theta)$ (fator de Lorentz–polarização) para raios X ou $L = 1/(\sin 2\theta\,\sin\theta)$ (apenas o fator de Lorentz, sem termo de polarização) para elétrons e nêutrons. As reflexões que o feixe não consegue alcançar (d < λ/2, 2θ exibido como ∞) recebem intensidade 0. <!-- 261105Cl 監査: J25 届かない反射の BB 強度は 0 --> Quando desativado, exibe a intensidade do fator de estrutura $\lvert F\rvert^2$. Ativá-lo também força *Ocultar planos equivalentes* e *Ocultar planos proibidos* e os deixa desabilitados (em cinza).
- **Ocultar planos equivalentes** (ativado por padrão) : agrupa planos cristalograficamente equivalentes em uma única entrada. A coluna **Mult.** só é exibida enquanto esta opção estiver ativada.
- **Ocultar planos proibidos** (ativado por padrão) : exclui os planos sistematicamente ausentes pelas regras de extinção. Enquanto estiver desativada, uma coluna **Condição** mostra a(s) condição(ões) de reflexão que proíbem cada plano ausente.
- **Corte de dist. interplanar >** (padrão 1 Å) : exclui reflexões com um espaçamento d menor que este valor (a unidade de comprimento segue a seleção em **Unit**). O menor valor aceito é (a + b + c)/20 da célula atual, na mesma unidade. <!-- 261105Cl 監査: J8 numericBoxCutoffD.Minimum を表示単位に合わせた -->

Cada linha é uma reflexão (ou um grupo de planos simetricamente equivalentes):

| Coluna | Significado |
|------|------|
| **h, k, l** | índices de Miller |
| **Mult.** | multiplicidade (número de planos simetricamente equivalentes) |
| **d (Å)** | espaçamento interplanar |
| **q (2π/d)** | módulo do vetor de espalhamento |
| **2θ (°)** | ângulo de difração para o comprimento de onda selecionado |
| **F_real** | parte real do fator de estrutura |
| **F_inv** | parte imaginária do fator de estrutura (não nula com dispersão anômala de raios X) |
| **\|F\|** | amplitude do fator de estrutura ($= \sqrt{F_\text{real}^2 + F_\text{inv}^2}$) |
| **F^2** | intensidade do fator de estrutura ($\lvert F\rvert^2$) |
| **Rel. Int. (%)** | intensidade relativa, com a reflexão mais forte definida como 100 |
| **Condição** | condição(ões) de extinção violada(s) pelo plano (exibida apenas quando *Ocultar planos proibidos* está desativado) |
| **i** | o índice de Miller–Bravais redundante $i=-(h+k)$ (exibido apenas quando **Opções ▸ Usar índice de Miller-Bravais (hkil)** da janela principal está ativado e o cristal usa eixos hexagonais — hexagonal, ou trigonal fora de uma configuração de eixos romboédricos); **Copiar** o escreve como exibido <!-- 261105Cl 監査: J9 i 列は表示している値をコピー --> |

**Gráfico de picos de difração.** Abaixo da tabela, as mesmas reflexões são desenhadas como um padrão de traços, com os picos mais fortes rotulados pelo seu *hkl*.

- O seletor do eixo horizontal escolhe entre **2θ** (ângulo de espalhamento em graus), **d** (espaçamento dos planos reticulares) e **Q** ($= 4\pi\sin\theta/\lambda$, o vetor de espalhamento / transferência de momento). As três opções descrevem as mesmas reflexões; apenas a escala horizontal muda.
- **Log I** alterna o eixo de intensidade entre linear e logarítmico. As intensidades de difração abrangem muitas ordens de grandeza, portanto uma escala logarítmica estica a parte inferior para revelar os picos fracos que uma escala linear achata contra a linha de base.
- As caixas **Intervalo** definem o intervalo horizontal e de intensidade exibidos.

---

## Aba Atenuação & Transporte

Quão profundamente o feixe penetra no material e como ele perde energia. O conteúdo depende do feixe.

=== "X-ray"
    ![Atenuação & Transporte — X-ray](../assets/cap-pt-auto/FormBeamInteraction-xray-attenuations.png)

=== "Electron"
    ![Atenuação & Transporte — electron](../assets/cap-pt-auto/FormBeamInteraction-electron-attenuations.png)

=== "Neutron"
    ![Atenuação & Transporte — neutron](../assets/cap-pt-auto/FormBeamInteraction-neutron-attenuations.png)

### X-ray

Os botões de opção escolhem o coeficiente plotado em função da energia do fóton (calculado em 1–60 keV sobre um eixo de energia linear; o eixo vertical é logarítmico para µ/ρ e µ e linear para T %; se a energia atual estiver fora dessa faixa, o título do gráfico o indica, p. ex. *(0,5 keV atuais fora do intervalo)*): <!-- 261106Cl 作者の決定 J5 -->

- **µ/ρ** — o coeficiente de atenuação de **massa** (cm²/g): quão fortemente o material remove raios X por grama, independentemente de quão densamente está empacotado (este é o valor encontrado em tabelas de referência). O gráfico mostra o **total** junto com seus componentes **photo**, **Rayleigh** e **Compton**.
- **µ** — o coeficiente de atenuação **linear** $\mu = (\mu/\rho)\cdot\rho$ (cm⁻¹): a atenuação por centímetro do material real em sua densidade real. A intensidade transmitida segue $I = I_0\,e^{-\mu t}$, e $1/\mu$ é a distância ao longo da qual a intensidade cai para cerca de 37 % (1/e).
- **T %** — a **transmissão** $T = e^{-\mu t}$ em porcentagem para a espessura da amostra **t** definida na caixa **Espessura t** (µm, padrão 100 µm). 100 % = transparente, 0 % = totalmente bloqueado; use isto para julgar uma espessura de amostra sensata na energia atual.

A linha vertical marca a energia atual. A tabela escalar à esquerda lista, na energia atual: **µ/ρ (total)**, **µ (linear)**, **Comprimento de atenuação** ($1/\mu$), **HVL** (camada semirredutora, $\ln 2/\mu$), **Transmissão** na espessura *t*, **µ_en/ρ** (coeficiente de absorção de energia de massa), os decrementos do índice de refração de raios X **δ** e **β** ($n = 1-\delta+i\beta$), o ângulo **θc (crítico)** para reflexão externa total e a **SLD de raios X (Re)** (densidade de comprimento de espalhamento). A tabela inferior lista as energias das **bordas** de absorção **K** e **L3** e suas razões **Salto** para cada elemento.

### Electron

O seletor de grandeza escolhe o que é plotado em função da energia do feixe (calculado em 1–50 keV; a visualização inicial mostra 0–20 keV; uma energia fora dessa faixa é indicada no título do gráfico, p. ex. *(200 keV atuais fora do intervalo)*): <!-- 261106Cl 作者の決定 J5 -->

- **Todos (normalizado)** — sobrepõe as três curvas abaixo, cada uma reescalonada ao seu próprio máximo para que as formas possam ser comparadas em um único gráfico (leia os valores absolutos na tabela).
- **σ elástica (nm²)** — seção de choque de espalhamento elástico: quão provável é que um único átomo desvie o elétron.
- **MFP elástico (nm)** — livre caminho médio: a distância média que o elétron percorre entre eventos de espalhamento elástico.
- **|dE/ds| (keV/nm)** — módulo do poder de freamento: a energia que o elétron perde por nanômetro percorrido.
- **IMFP (nm)** — livre caminho médio inelástico: a distância média entre colisões com perda de energia.
- **Alcance CSDA (µm)** — o comprimento total do trajeto que o elétron percorre antes de parar.

A tabela escalar lista o comprimento de onda do elétron **λ (elétron)**, **σ elástica**, **MFP elástico**, **|dE/ds| (perda)**, **IMFP**, a **E de plasma** e a energia média de excitação **J**, dois **alcances** de elétrons (a estimativa de penetração de Kanaya–Okayama e o comprimento de trajeto integrado CSDA) e o **Z, A** médio. A tabela por elemento fornece a fração atômica e a seção de choque elástica σ de cada elemento. As seções de choque elásticas usam os dados **NIST Mott** (50 eV–36 keV) e recorrem ao **screened Rutherford** acima de 36 keV.

### Neutron

A interação de nêutrons é definida por seções de choque nucleares em vez de uma curva dependente da energia, portanto esta aba mostra apenas tabelas. A tabela escalar lista o comprimento de espalhamento coerente médio **b̄**, a **SLD coerente**, as seções de choque médias coerente / incoerente / de absorção / total (**σ̄_coh**, **σ̄_incoh**, **σ̄_abs**, **σ̄_total**), a seção de choque total macroscópica **Σ_total** e o comprimento de atenuação correspondente, **Compr. de atenuação (1/Σ)**. A seção de choque de absorção é avaliada com a lei 1/v no comprimento de onda atual; nuclídeos para os quais isto é inválido (Cd, Sm, Eu, Gd como absorvedores ressonantes) são sinalizados. A tabela por elemento lista **b_coh**, **σ_coh** e a fração atômica.

---

## Aba Fatores de dispersão {#scattering-factors-tab}

O fator de espalhamento atômico de cada elemento constituinte, plotado em função de $s = \sin\theta/\lambda$ (Å⁻¹). Cada elemento é desenhado em sua própria cor, e a **linha vertical do cursor** pode ser arrastada para ler o fator de espalhamento de cada elemento naquela posição na tabela à esquerda.

=== "X-ray"
    ![Fatores de dispersão — X-ray](../assets/cap-pt-auto/FormBeamInteraction-xray-scattering.png)

=== "Electron"
    ![Fatores de dispersão — electron](../assets/cap-pt-auto/FormBeamInteraction-electron-scattering.png)

=== "Neutron"
    ![Fatores de dispersão — neutron](../assets/cap-pt-auto/FormBeamInteraction-neutron-scattering.png)

- **Raios X** oferece dois modos de **Modelo**: **f(s)** plota o fator de espalhamento atômico de raios X convencional (em unidades de elétron); **F(q)+S(q)** plota o fator de forma **coerente** de Rayleigh $F(q)$ junto com a função de espalhamento **incoerente** de Compton $S(q)$ (do xraylib). A tabela também lista os termos de dispersão anômala **f'(E)** e **f''(E)** na energia atual e uma coluna **modelo** que indica de qual fonte veio a coluna f(s). Quando **f(s)** está selecionado, uma segunda linha de botões de opção escolhe **de onde vem a curva**: **Waasmaier–Kirfel** (o ajuste padrão, que também cobre íons em Z = 1–98) ou **Temari**. Fica esmaecida enquanto **F(q)+S(q)** é exibido, porque essas duas curvas vêm do xraylib e não admitem escolha de origem.
- **Elétrons** oferece quatro fontes para o fator de espalhamento de elétrons: **Peng**, **Kirkland**, **8-Gaussians** e **Temari**. A tabela mostra $f_e(s)$ (nm) e qual **modelo** o produziu. **Kirkland** e **8-Gaussians** não têm parâmetros para íons; por isso, para íons as suas curvas são omitidas e as suas células da tabela ficam em branco (**Peng** dá valores para íons). <!-- 261106Cl 作者の決定 J12 -->
- Os comprimentos de espalhamento de **Nêutron** não dependem de $s$, portanto nenhuma curva é desenhada; a tabela lista o comprimento de espalhamento coerente **b_coh** de cada elemento e suas seções de choque coerente / incoerente.
- **Temari** não é um ajuste, mas uma tabela calculada (Temari dataset-factors v2.0.0, CC-BY-4.0, a partir de um cálculo de campo autoconsistente de Dirac completo). Cobre **apenas átomos neutros Z = 1–86**: íons e Z = 87–98 não têm entrada, portanto suas curvas são omitidas e suas células ficam em branco em vez de preenchidas com valores de átomo neutro.
- **Deslocamento (B)** multiplica cada fator pelo amortecimento térmico $e^{-B s^2}$ usando o parâmetro de deslocamento isotrópico de cada átomo.

---

## Aba Fluorescência {#fluorescence-tab}

Para um feixe de raios X, a emissão de **fluorescência** característica da amostra. (Esta aba fica oculta para feixes de elétrons e de nêutrons.)

![Fluorescência (X-ray)](../assets/cap-pt-auto/FormBeamInteraction-xray-fluorescence.png)

O gráfico **Linhas de emissão EDX (lista de referência; excitação não considerada)** desenha as linhas características (Kα1, Kα2, Kβ1, Lα1, Lα2, Lβ1) de cada elemento como traços em suas energias de fóton, com a altura proporcional à fração atômica × taxa radiativa × rendimento de fluorescência (uma prévia qualitativa no estilo EDX e uma lista de referência: a seção de choque de excitação e a eficiência do detector não são modeladas, de modo que a "linha mais intensa" não precisa ser a mais intensa num espectro medido). <!-- 261106Cl 作者の決定 J6 --> A tabela inferior lista, por linha, o elemento, o nome da linha, a energia **E keV**, a intensidade relativa **Rel.I** (a fração, em %, desta linha entre as transições radiativas que preenchem uma vacância na sua camada) e o rendimento de fluorescência **ω**. A tabela escalar reporta o rendimento da camada K **ω_K** de cada elemento, a **linha mais intensa (excit. ignorada)** no espectro e a **fonte** dos valores: *xraylib (E, I, ω)* quando o xraylib está disponível (energias das linhas, intensidades relativas e rendimentos vêm todos do xraylib); caso contrário, *internal (E only)*. <!-- 261105Cl 監査: FormBeamInteraction.cs (Rel.I = LineRadRate × 100、出典の行, Controls c6d62ed) -->

---

## Copiar para a área de transferência

Cada aba tem um botão **Copiar** que copia sua tabela para a área de transferência como texto que pode ser colado em uma planilha como o Excel.

---

## Veja também

- [Banco de dados de cristais](1-crystal-database.md) — definição do cristal cuja interação é calculada.
- [Simulador de difração](7-diffraction-simulator/index.md) — simulação de padrões de difração usando os fatores de estrutura.
- [Apêndice A2. Interação do feixe (fundamentos de física do estado sólido)](appendix/a2-beam-interaction/index.md) — o espalhamento e a física do estado sólido por trás de cada aba.
