# Apêndice A4. Simetria e grupos espaciais

O capítulo da janela principal [2. Informação de simetria](../../2-symmetry-information.md) é um guia para a GUI: ele indica qual aba mostra o quê e qual botão copia qual diagrama. Este apêndice reúne os **fundamentos cristalográficos e de teoria de grupos** por trás dessas tabelas e figuras — o que um símbolo de Hermann–Mauguin realmente codifica, como ler os diagramas de elementos de simetria e de posições gerais no estilo das *International Tables for Crystallography* (ITA) Vol. A, e o que significam as tabelas de supergrupos/subgrupos e a terminologia (*translationengleiche*, *klassengleiche*, classe de conjugação, domínios, leis de geminação, …) da janela **Relações de grupo...**.

![Informação de simetria](../../../assets/cap-pt-auto/FormSymmetryInformation.png)

Duas janelas são cobertas, e a teoria é mais bem lida nesta ordem:

1. **[A4.1. Símbolos de grupos espaciais e diagramas de simetria](symbols-and-diagrams.md)** — os símbolos de Hermann–Mauguin, de Schoenflies e de Hall; a classificação segundo a teoria de grupos mostrada na aba **Propriedades** (centrossimétrico, Sohncke, simórfico, polar, classe cristalina aritmética, simetria de Patterson, …); a descrição de cada operação de simetria na aba **Operações** por tripleto de coordenadas/símbolo de Seitz/tipo geométrico; e as convenções gráficas dos diagramas de elementos de simetria e de posições gerais na parte inferior da janela [Informação de simetria](../../2-symmetry-information.md).
2. **[A4.2. Relações grupo–subgrupo](group-subgroup-relations.md)** — o que é um *subgrupo maximal* / *supergrupo minimal*, a distinção *t*-/*k*- de Hermann e como ler cada aba do navegador **Relações de grupo...** (Diagrama, Matriz, Divisão de órbita, Domínios e geminações, Novas reflexões), aberto a partir do painel **Opções** da Informação de simetria.

A4.1 vem primeiro porque A4.2 remete constantemente a ela: cada relação de subgrupo/supergrupo é rotulada com exatamente os mesmos símbolos de Hermann–Mauguin, símbolos de Seitz e expressões de tipo geométrico (*"3-fold rotation"*, *"c-glide plane"*, *"screw axis"*, …) ali introduzidos.

---

## Escopo e fontes

O banco de dados interno do ReciPro cobre os 230 tipos de grupos espaciais (com 530 configurações/escolhas de origem tabuladas) exatamente como tabulados nas *International Tables for Crystallography*, **Volume A** (simetria de grupos espaciais) e **Volume A1** (subgrupos maximais dos grupos espaciais). Este apêndice explica a *apresentação* desses dados pelo ReciPro — a notação, os diagramas, a ferramenta de navegação — e pressupõe que o leitor já tenha familiaridade, em nível de graduação, com redes, grupos pontuais e a ideia de uma operação de simetria. Ele não substitui as próprias ITA, que continuam sendo a referência autoritativa para os dados tabulados (e que o ReciPro não pode reproduzir literalmente por razões de direitos autorais — veja a aba **Configurações** para a listagem própria do ReciPro das origens/configurações alternativas de um dado tipo de grupo espacial).

!!! note "Relações de grupo... é um recurso em desenvolvimento ativo"
    O navegador **Relações de grupo...** (A4.2) calcula os subgrupos e supergrupos *translationengleiche* (t-) e *klassengleiche* (k-, incluindo os *isomorfos*) diretamente a partir das operações de simetria do próprio grupo espacial (e não de uma lista pré-tabulada), de modo que cada relação mostrada é verificada de forma independente, em vez de copiada de uma tabela. Os limites remanescentes — p. ex., a série isomorfa é enumerada apenas até índice ≤ 4 — estão explicitados em **Limitações atuais** de A4.2.

---

## Veja também

- [2. Informação de simetria](../../2-symmetry-information.md) — o guia da GUI que este apêndice explica.
- [A4.1. Símbolos de grupos espaciais e diagramas de simetria](symbols-and-diagrams.md) · [A4.2. Relações grupo–subgrupo](group-subgroup-relations.md)
- [Apêndice A1. Sistemas de coordenadas](../a1-coordinate-system/1-orientation.md)
- [Apêndice A2. Interação do feixe (fundamentos de física do estado sólido)](../a2-beam-interaction/index.md) — onde as condições de reflexão do grupo espacial (ausências sistemáticas) entram no fator de estrutura.
