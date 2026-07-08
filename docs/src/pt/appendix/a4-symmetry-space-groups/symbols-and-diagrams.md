# A4.1. Símbolos de grupos espaciais e diagramas de simetria

Esta página explica tudo o que é mostrado na metade superior de [Informação de simetria](../../2-symmetry-information.md) (o painel de identidade do grupo espacial e as abas **Operações**/**Propriedades**/**Configurações**) e os dois diagramas esquemáticos na parte inferior da janela. Toda a notação segue as *International Tables for Crystallography* (ITA), Vol. A.

---

## Símbolos de Hermann–Mauguin (HM)

Um símbolo de Hermann–Mauguin tem duas camadas: o **símbolo do grupo pontual** (caixa superior, *Grupo pontual*) descreve apenas a simetria macroscópica do cristal, e o **símbolo do grupo espacial** (caixa inferior, *Grupo espacial*) acrescenta a centragem da rede e as eventuais componentes de parafuso/deslizamento.

### Letra de rede

O símbolo do grupo espacial começa com uma das sete letras de rede padrão:

| Letra | Significado |
|---|---|
| `P` | Primitiva |
| `A`, `B`, `C` | Centrada em uma face (centragem na face *bc*, *ac* ou *ab*, respectivamente) |
| `I` | De corpo centrado |
| `F` | Centrada em todas as faces |
| `R` | Romboédrica (uma rede trigonal própria; frequentemente descrita em *eixos hexagonais*, caso em que a célula contém três pontos de rede) |

### Direções de simetria

Depois da letra de rede, cada posição restante do símbolo corresponde a uma **direção de simetria** — uma direção do cristal ao longo da qual se situa um eixo de rotação/parafuso e/ou perpendicular à qual se situa um plano de espelho/deslizamento. A quais direções físicas essas posições se referem, e em que ordem, é fixado pelo sistema cristalino:

| Sistema cristalino | 1ª posição | 2ª posição | 3ª posição |
|---|---|---|---|
| Triclínico | *(nenhuma — apenas `1` ou `-1`)* | | |
| Monoclínico | $[010]$ (eixo único $b$, convenção do ReciPro) | | |
| Ortorrômbico | $[100]$ | $[010]$ | $[001]$ |
| Tetragonal | $[001]$ | $[100],[010]$ | $[110],[1\bar 10]$ |
| Trigonal / Hexagonal | $[001]$ | $[100],[010],[\bar 1\bar 1 0]$ | $[1\bar 10],[120],[\bar 2\bar 1 0]$ |
| Cúbico | $[100],[010],[001]$ | $[111]$ *(e as outras 3 diagonais de corpo)* | $[1\bar 10],[110]$ *(e as outras 4 diagonais de face)* |

Cada posição é preenchida de acordo com estas regras:

- Um número simples $n$ ($n=1,2,3,4,6$) : um eixo de **rotação** de ordem $n$ ao longo dessa direção.
- Um eixo de parafuso $n_p$ (p. ex. $2_1$, $4_2$, $6_3$) : uma rotação de $360°/n$ *combinada com* uma translação de $p/n$ da repetição da rede ao longo do eixo. Por exemplo, $2_1$ (um "parafuso de ordem dois") significa girar $180°$ **e** deslocar meia aresta da célula ao longo do eixo; $6_3$ significa girar $60°$ e deslocar meia aresta da célula ao longo de $c$.
- Uma letra isolada ($m,a,b,c,n,d$), sem número de rotação precedente : um **plano de espelho ou de deslizamento** perpendicular a essa direção (o significado da letra é o mesmo dos diagramas, abaixo).
- $n/m$ ou $n_p/m$ : um eixo de rotação/parafuso **com** um espelho perpendicular a ele (os dois elementos compartilham a mesma direção, um ao longo do eixo e outro transversal a ele).
- $-n$ (p. ex. $-1,-3,-4,-6$) : um eixo de **rotoinversão** (girar $360°/n$ e, em seguida, inverter através de um ponto sobre o eixo). $-1$ isolado denota um centro de inversão puro; não existe um eixo "$-2$", porque uma rotoinversão de ordem dois é idêntica a um espelho e, portanto, se escreve sempre $m$.

### Símbolo curto vs. completo

O símbolo HM **curto** (o normalmente citado) omite elementos de simetria que já estão implícitos nos que foram escritos; o símbolo **completo** explicita todas as direções. Por exemplo, o grupo espacial No. 62 é $Pnma$ na forma curta e $P\,2_1/n\,2_1/m\,2_1/a$ na forma completa — os três eixos de parafuso $2_1$ estão implícitos nos três planos de deslizamento/espelho junto com o grupo pontual $mmm$ do grupo espacial, então o símbolo curto os omite. Os campos *Símbolo HM (curto)* e *Símbolo HM (completo)* do ReciPro mostram ambos; para a maioria dos grupos espaciais eles coincidem.

### Símbolos de Schoenflies (SF) e de Hall

O **símbolo de Schoenflies** (p. ex. $D_{2h}^{16}$) nomeia o tipo de grupo pontual ($D_{2h}$) e acrescenta um sobrescrito que simplesmente enumera *qual* grupo espacial dessa família de grupo pontual este é — ao contrário do símbolo HM, o sobrescrito não carrega, por si só, nenhum significado geométrico direto; é preciso consultá-lo em uma tabela. O ReciPro mostra o símbolo de Schoenflies tanto para o grupo pontual quanto para o grupo espacial.

O **símbolo Hall** é uma notação diferente, baseada em geradores e projetada para processamento computacional sem ambiguidade: ele lista um conjunto mínimo de operações geradoras junto com uma origem explícita, de modo que um programa pode reconstruir o conjunto exato de coordenadas sem consultar uma tabela para saber "qual escolha de configuração/origem este símbolo HM implica". Um símbolo Hall não é a *única* maneira possível de codificar um dado conjunto de operações (escolhas diferentes de geradores dão símbolos Hall diferentes e igualmente válidos para o mesmo grupo), mas cada um deles é totalmente explícito e reversível por si só. O ReciPro mostra um símbolo Hall gerado sistematicamente para a configuração atual; a aba **Configurações** (abaixo) lista todas as escolhas tabuladas de origem/configuração que compartilham o número do grupo espacial atual, cada uma com seus próprios símbolos HM e Hall.

---

## Operações de simetria (aba Operações)

A aba **Operações** lista todas as operações de simetria da posição geral para a configuração atual (com as translações de centragem da rede já expandidas), em três notações paralelas:

| Coluna | Exemplo | Significado |
|---|---|---|
| Coordenadas | `-y, x-y, z+1/3` | O tripleto de coordenadas $(x,y,z)\mapsto(x',y',z')$, isto é, a aplicação afim $x'=Rx+t$ escrita algebricamente (convenção ITA/CIF). |
| Seitz | `3+ [111]` | Um símbolo compacto: ordem e sentido da rotação/parafuso (`3+`), direção do eixo (`[111]`) e — se presente — a translação da operação, p. ex. `2₁ [001] 0,0,1/2`. Um espelho puro é `m`, a identidade é `1`, a inversão é `-1`. |
| Tipo | `3-fold rotation (3+) [111]` | Uma classificação da operação em linguagem simples: `Identity` (identidade), `Inversion centre at …` (centro de inversão em …), rotação de ordem `n`, eixo de parafuso `nₚ`, plano de espelho `m`, plano de deslizamento `a/b/c/n/d` ou rotoinversão de ordem `n`, cada uma com sua direção (e, no caso do centro de inversão, sua posição). |

O botão **Copiar (CIF)** coloca a lista completa de operações na área de transferência como um loop CIF `_space_group_symop_operation_xyz`. Esse vocabulário — símbolo de Seitz e tipo geométrico — reaparece ao longo de [A4.2](group-subgroup-relations.md), onde cada gerador mantido/perdido de uma relação de subgrupo é descrito da mesma forma.

---

## Classificação segundo a teoria de grupos (aba Propriedades)

A aba **Propriedades** informa um conjunto de classificações padrão do grupo espacial atual. Algumas delas — centrossimétrico, Sohncke e polar (e, a partir dessas, as permissões de propriedades físicas abaixo) — decorrem diretamente da **parte matricial** $R$ de cada operação (a parte linear, de rotação ou de reflexão), junto com a parte de translação no caso de centrossimétrico. As demais — simórfico, par enantiomorfo, família cristalina/sistema reticular/tipo de Bravais, classe cristalina aritmética e simetria de Patterson — são propriedades do *tipo* de grupo espacial como um todo (seu número IT, tipo de rede e classe de Laue), e não de nenhuma operação individual. Nada disso requer uma métrica (a forma da célula unitária) — depende apenas do conteúdo abstrato de simetria e da classificação do tipo de grupo espacial.

**Centrossimétrico** — o conjunto de operações contém alguma operação da forma $\{-I \mid t\}$ (uma inversão através do ponto $t/2$, que não precisa ser a origem). As propriedades Sohncke e polar (abaixo) são mutuamente exclusivas com esta: um centro de inversão inverte todas as direções, de modo que um grupo centrossimétrico nunca pode ser polar, e $-I$ tem determinante $-1$, de modo que um grupo centrossimétrico nunca pode ser Sohncke.

**Grupo de Sohncke (que preserva a orientação)** — a parte matricial de *todas* as operações tem $\det R=+1$: o grupo contém apenas rotações próprias e rotações de parafuso, nunca um espelho, um deslizamento, uma inversão ou uma rotoinversão. 65 dos 230 tipos de grupos espaciais são grupos de Sohncke. Ser um grupo de Sohncke é a condição de simetria para que uma estrutura seja compatível com objetos de quiralidade definida (moléculas quirais, proteínas, quartzo, …) sem conter também as suas imagens especulares. Isso é mais amplo do que ser um dos membros de um *par* genuinamente distinto de tipos de grupo espacial em imagem especular — veja **Par enantiomorfo**, a seguir.

**Par enantiomorfo** — entre os 65 tipos de Sohncke, 11 pares (22 tipos) relacionam-se entre si *apenas* por uma transformação que inverte a orientação, e por nenhuma transformação própria (que preserva a orientação): aplicar um espelho a um cristal em um desses grupos espaciais o transforma no outro membro do par, e nunca de volta em si mesmo sob qualquer reetiquetagem dos eixos. Os 11 pares são os construídos sobre eixos de parafuso de quiralidade oposta:

$$P4_1 / P4_3,\ \ P4_122 / P4_322,\ \ P4_12_12 / P4_32_12,\ \ P3_1/P3_2,\ \ P3_112/P3_212,\ \ P3_121/P3_221,$$
$$P6_1/P6_5,\ \ P6_2/P6_4,\ \ P6_122/P6_522,\ \ P6_222/P6_422,\ \ P4_332/P4_132.$$

Os $65-22=43$ tipos de Sohncke restantes são a sua própria imagem especular (aquirais *enquanto tipos de grupo espacial*, ainda que cada estrutura individual neles continue sendo quiral).

**Simórfico** — um dos 73 tipos de grupo espacial para os quais é possível escolher uma origem tal que *todo* representante de classe lateral (módulo as translações da rede) tenha componente de translação intrínseca (de parafuso/deslizamento) nula — de forma equivalente, algum ponto da célula tem um grupo de simetria de sítio isomorfo ao grupo pontual completo. (As translações de centragem, é claro, permanecem; "simórfico" é uma afirmação sobre as partes não primitivas das operações do *grupo pontual*, não sobre a rede.) Um grupo espacial simórfico pode sempre ser gerado apenas a partir de seu grupo pontual e de sua rede, sem necessidade de eixos de parafuso ou planos de deslizamento, quando descrito naquela origem particular — que é exatamente a origem que as próprias ITA tabulam para um tipo simórfico, de modo que seu símbolo padrão curto/completo já está livre de letras de parafuso/deslizamento. (Descrever as operações do mesmo grupo em uma origem deslocada, ou transladada por uma centragem, pode fazer uma operação individual parecer carregar uma translação de parafuso/deslizamento, sem mudar a classificação simórfica do tipo — a classificação pergunta apenas se existe alguma origem livre de translações, e para esses 73 tipos ela existe.)

**Polar** — se alguma direção é deixada invariante, $Rv=v$, pela parte matricial de *todas* as operações (não $\pm v$: uma direção polar verdadeira deve ser preservada exatamente, e não meramente invertida ou deixada como um eixo de ordem dois). As condições relevantes são: **nenhuma** (nenhuma direção assim) &nbsp;/&nbsp; um único eixo $[uvw]$ &nbsp;/&nbsp; um plano inteiro (qualquer direção nele) &nbsp;/&nbsp; **qualquer** direção (apenas para o grupo pontual $1$). Um eixo polar é a direção ao longo da qual uma polarização elétrica espontânea é permitida pela simetria (veja a tabela de propriedades físicas abaixo).

**Família cristalina, sistema reticular, tipo de Bravais** — a hierarquia de classificação padrão da IUCr acima do sistema cristalino: 6 **famílias cristalinas**, 7 **sistemas cristalinos**, 7 **sistemas reticulares** e 14 **tipos de rede de Bravais** no total. A sutileza está na **família cristalina hexagonal**: como **sistemas cristalinos** ela se divide em *trigonal* e *hexagonal*, mas como **sistemas reticulares** ela se divide de outra forma, em *hexagonal* e *romboédrico* — um grupo espacial trigonal cai no sistema reticular hexagonal se sua rede é do tipo $P$, ou no sistema reticular romboédrico se ela é centrada em $R$, independentemente de a qual dos dois sistemas cristalinos ele pertença.

**Classe cristalina aritmética** — o emparelhamento de um símbolo de grupo pontual (possivelmente resolvido por direção) com uma letra de rede de Bravais, p. ex. `4mmP`; há 73 classes cristalinas aritméticas no total. Como alguns símbolos de grupo pontual (`3m1` vs. `31m`, para as duas maneiras não equivalentes de um grupo pontual $3m$ se posicionar em relação a uma rede hexagonal) já codificam a sua orientação em relação à rede, citar o símbolo de grupo pontual orientado junto com a letra de rede basta para nomear a classe sem ambiguidade.

**Simetria de Patterson** — o tipo de rede junto com a *classe de Laue* (o grupo pontual centrossimétrico obtido acrescentando $-1$ ao grupo pontual do próprio grupo espacial), com toda a informação de parafuso/deslizamento removida, p. ex. `Pmmm` para qualquer um dos 30 grupos espaciais ortorrômbicos de rede $P$, independentemente de quais deles tenham planos de deslizamento. Esta é a simetria da função de Patterson calculada a partir das *intensidades* de difração $|F|^2$ na aproximação cinemática, porque $|F|^2$ é insensível ao deslocamento de fase que uma translação de deslizamento/parafuso introduz (embora as ausências sistemáticas que ela causa, e os picos de Harker no mapa de Patterson, ainda possam trair indiretamente a sua presença). Para a difração dinâmica de elétrons, essa imagem cinemática não vale exatamente; veja o [Apêndice A3](../a3-bloch-wave/index.md).

### Propriedades físicas permitidas pela simetria

As últimas linhas da aba Propriedades informam se uma dada propriedade física macroscópica é **permitida pela simetria** para o grupo pontual atual — uma condição necessária, não uma garantia de que o efeito seja grande, ou sequer presente, em um cristal real (a convenção do "Physical Properties of Crystals" de Nye):

| Propriedade | Condição de simetria | Grupos pontuais |
|---|---|---|
| Piroelétrico / ferroelétrico | Polar (um vetor polar de posto 1 — a polarização espontânea — é permitido) | os 10 grupos pontuais polares |
| Piezoelétrico | Não centrossimétrico **e** grupo pontual $\ne 432$ | 20 dos 21 grupos pontuais não centrossimétricos |
| Geração de segundo harmónico ($\chi^{(2)}$ de dipolo elétrico de volume) | Mesma condição da piezoeletricidade (um tensor polar de posto 3) | os mesmos 20 grupos pontuais |
| Atividade óptica (girotropia natural) | Os 11 grupos pontuais que contêm apenas rotações próprias, mais 4 outros que são girotrópicos sem serem puramente Sohncke | $1,2,3,4,6,222,32,422,622,23,432$ e $m,mm2,\bar4,\bar42m$ — 15 grupos pontuais no total |

$432$ é o único grupo pontual acêntrico *sem* resposta piezoelétrica/SHG: ele tem simetria rotacional demais (todas as rotações próprias, cúbico) para que qualquer componente de tensor polar de posto 3 sobreviva, embora não seja centrossimétrico.

!!! note "Permitido pela simetria, não necessariamente observado"
    Estas linhas dizem o que o grupo pontual *permite*. Se um cristal real de fato comuta a sua polarização (ferroeletricidade verdadeira), ou exibe uma resposta piezoelétrica ou de SHG praticamente útil, depende de detalhes de química e de estrutura que vão além da simetria.

### Aba Configurações

Lista todas as escolhas tabuladas de origem/configuração de eixos que compartilham o número IT do grupo espacial atual (p. ex. as duas escolhas de origem de $Fd\bar 3m$, ou as diferentes escolhas de célula de um grupo monoclínico), cada uma com seus símbolos HM e Hall; a linha da configuração exibida no momento é marcada. Esta aba serve apenas para consultar as alternativas — selecionar uma linha não altera o cristal.

---

## Diagrama dos elementos de simetria

![Diagramas dos elementos de simetria e das posições gerais](../../../assets/cap-pt-auto/FormSymmetryInformation.splitContainer1.tableLayoutPanel1.png)

O diagrama da esquerda reproduz o diagrama esquemático de simetria das ITA Vol. A para a configuração atual, projetado ao longo do eixo escolhido com o controle **Direção** (`a`/`b`/`c`).

**Eixos perpendiculares à página** são desenhados como símbolos pontuais preenchidos cuja forma codifica a ordem da rotação, com pequenas caudas ("aletas") adicionadas para um eixo de parafuso (o número e o arranjo delas codificam tanto o passo $p$ do parafuso quanto a sua quiralidade; assim, p. ex., $3_1$ e $3_2$ — parafusos de mesma ordem e sentidos opostos — são desenhados como padrões de caudas em imagem especular, e não apenas com um número diferente de caudas):

| Símbolo | Elemento |
|---|---|
| Lente preenchida (oval pontiaguda) | Eixo de rotação de ordem 2 |
| Lente preenchida com uma aleta | Eixo de parafuso $2_1$ |
| Triângulo preenchido | Eixo de rotação de ordem 3 |
| Triângulo preenchido com caudas | Eixo de parafuso $3_1$ / $3_2$ |
| Quadrado preenchido | Eixo de rotação de ordem 4 |
| Quadrado preenchido com caudas | Eixo de parafuso $4_1$ / $4_2$ / $4_3$ |
| Hexágono preenchido | Eixo de rotação de ordem 6 |
| Hexágono preenchido com caudas | Eixo de parafuso $6_1 \ldots 6_5$ |
| Pequeno círculo vazio | Centro de inversão ($-1$) |
| Símbolo combinado vazio/preenchido | Eixo de rotoinversão ($-3,-4,-6$) |

Eixos que correm obliquamente ou dentro da página (isso ocorre apenas para direções especiais, como as diagonais de corpo $\langle 111\rangle$ ou as diagonais de face $\langle 110\rangle$ do sistema cúbico) são desenhados como uma seta com o símbolo pontual em sua base, seguindo a mesma convenção das ITA.

**Planos** são desenhados como linhas cujo estilo nomeia o tipo de deslizamento — a letra indica ao longo de qual direção da rede corre o vetor de deslizamento (ou que ele é diagonal/de um quarto de célula), enquanto o fato de essa translação estar *no* plano da página ou sair *para fora* dele depende do eixo de projeção escolhido:

| Estilo de linha | Plano |
|---|---|
| Linha contínua | Plano de espelho $m$ |
| Traços longos | Deslizamento axial $a$ ou $b$ |
| Linha pontilhada | Deslizamento axial $c$ (no caso comum em que sua translação sai do plano da página) |
| Linha traço-ponto | Deslizamento diagonal $n$ |
| Linha traço-ponto com seta | Deslizamento diamante $d$ (uma translação de um quarto de célula; ocorre apenas em células centradas) |
| Linha dupla | "Deslizamento duplo" $e$ — dois vetores de deslizamento independentes coincidem no mesmo plano (ocorre apenas em células centradas, onde tanto um deslizamento quanto o seu parceiro transladado pela centragem passam pelo mesmo plano) |

Um rótulo de altura fracionário (p. ex. `1/4`) ao lado de um símbolo dá a sua coordenada ao longo do eixo de projeção sempre que o elemento não está no plano de altura 0.

!!! note "Grupos cúbicos de rede F: apenas um octante é desenhado"
    Para os grupos espaciais cúbicos centrados em $F$, o ReciPro desenha apenas o quadrante superior esquerdo de um oitavo da célula (caso contrário, o diagrama seria denso demais para ser legível); a célula completa o repete pelas translações de centragem e pelos próprios elementos de simetria desenhados. Os mesmos elementos de simetria também podem ser sobrepostos diretamente ao modelo 3D no [Visualizador de estrutura](../../5-structure-viewer.md).

---

## Diagrama das posições gerais

O diagrama da direita plota as posições gerais equivalentes — a órbita de um ponto genérico $(x,y,z)$ sob todas as operações do grupo espacial — novamente no estilo das ITA:

- Cada **círculo** é a projeção de uma cópia do ponto equivalente por simetria.
- Uma **vírgula** dentro de um círculo marca uma cópia gerada por uma operação *de segunda espécie* (um espelho, um deslizamento, uma inversão ou uma rotoinversão) — ela tem a quiralidade oposta à de um objeto de teste quiral colocado no ponto original, exatamente como os pares de mãos, espelhada e comum, usados nas próprias ITA.
- Um **círculo dividido** (metade lisa, metade com vírgula) marca uma posição em que uma cópia por operação própria e uma cópia por operação imprópria se projetam sobre o mesmo ponto.
- Um rótulo de altura ao lado de um círculo (`+`, `−`, `½+`, …) dá a coordenada dessa cópia ao longo do eixo de projeção *em relação ao* ponto de referência — `+` significa "em $z$", `−` significa "em $-z$", `½+` significa "em $z+\tfrac12$", e assim por diante; não é uma altura absoluta.
- (Apenas nos grupos espaciais cúbicos) linhas auxiliares finas conectam três círculos relacionados por um eixo de ordem 3 ao longo da diagonal de corpo $\langle111\rangle$.
- Em geral, um círculo (ou uma metade de um círculo dividido) corresponde a uma posição equivalente, de modo que o número de círculos coincide com a **multiplicidade** da posição geral mostrada na aba [Posições de Wyckoff](../../2-symmetry-information.md) — uma verificação rápida ao ler qualquer um dos dois diagramas. Se o eixo de projeção escolhido fizer várias cópias de mesma quiralidade coincidirem exatamente, elas são sobrepostas em um único ponto (distinguidas apenas por rótulos de altura separados), em vez de desenhadas como círculos separados lado a lado; nesse caso, o número visível de círculos pode ser menor que a multiplicidade.

Os campos `numericBox` abaixo de **Direção** permitem afastar o ponto de teste $(x,y,z)$ da posição padrão do grupo pontual, o que às vezes é útil para desobstruir um diagrama em que vários círculos coincidiriam.

---

## Veja também

- [2. Informação de simetria](../../2-symmetry-information.md) — o guia da GUI que este apêndice explica.
- [A4.2. Relações grupo–subgrupo](group-subgroup-relations.md) — reutiliza o vocabulário de símbolos de Seitz/tipos geométricos introduzido aqui.
- [Apêndice A4. Simetria e grupos espaciais](index.md)
