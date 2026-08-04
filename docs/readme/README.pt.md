# ReciPro

[![Documentation](https://img.shields.io/badge/%F0%9F%93%96_Documentation-blue)](https://seto77.github.io/ReciPro/pt/)
[![Latest Release](https://img.shields.io/github/v/release/seto77/ReciPro?logo=github)](https://github.com/seto77/ReciPro/releases/latest)
[![Total downloads](https://img.shields.io/github/downloads/seto77/ReciPro/total?logo=github&label=GitHub%20downloads)](https://github.com/seto77/ReciPro/releases)
[![GitHub Stars](https://img.shields.io/github/stars/seto77/ReciPro?style=social)](https://github.com/seto77/ReciPro/stargazers)
[![GitHub Forks](https://img.shields.io/github/forks/seto77/ReciPro?style=social)](https://github.com/seto77/ReciPro/forks)
[![License: MIT](https://img.shields.io/badge/License-MIT-green)](https://github.com/seto77/ReciPro/blob/master/LICENSE.md)

<!-- 260804Cl: Tradução de ../../README.md (inglês). Atualize este arquivo sempre que a versão em inglês mudar. -->
[English](../../README.md) | [日本語](README.ja.md) | [Deutsch](README.de.md) | [Français](README.fr.md) | [Español](README.es.md) | [Italiano](README.it.md) | [Русский](README.ru.md) | [简体中文](README.zh-Hans.md) | [繁體中文](README.zh-Hant.md) | [한국어](README.ko.md) | **Português**

*ReciPro* é um software cristalográfico multipropósito, gratuito, de código aberto e baseado em interface gráfica, que oferece acesso integrado a funções para explorar bases de dados cristalográficas, visualizar estruturas cristalinas e configurações de goniômetro, simular padrões de difração e imagens de microscopia de alta resolução e analisar dados de difração. Esses recursos estão interligados por uma interface gráfica intuitiva, e os resultados são exibidos de forma sincronizada, praticamente em tempo real. O *ReciPro* auxiliará um público amplo de cristalógrafos (inclusive iniciantes) que trabalham com difração de raios X, de elétrons e de nêutrons, bem como com MET.

O *ReciPro* vem sendo desenvolvido continuamente desde 2002 e está disponível no GitHub desde março de 2020. Já foi baixado mais de 27.000 vezes a partir do GitHub e é utilizado por centenas de usuários em mais de uma dezena de laboratórios de universidades e empresas.

***[Consulte o manual para aprender a usá-lo!](https://seto77.github.io/ReciPro/pt/)***

[Diversas simulações executadas em tempo real (exemplo: MgAl2O4)](https://github.com/user-attachments/assets/6b0234dd-f2d6-49db-b146-bb74cf6021b6)

## Autores

O *ReciPro* é desenvolvido por [Seto Y.](https://yseto.net/en/home-e) e [Ohtsuka M.](https://researchmap.jp/7000002999?lang=en). As funções e os algoritmos são apresentados no [artigo](https://github.com/seto77/ReciPro/blob/master/docs/ReciProSetoOhtsuka2022.pdf).

## Como citar

Se você utilizar o *ReciPro* em trabalhos acadêmicos, use o link **Cite this repository** exibido na página do repositório no GitHub. Os metadados de citação são fornecidos pelo `CITATION.cff`, e a citação preferencial é o seguinte artigo:

  * [Seto, Y. & Ohtsuka, M. (2022). *J. Appl. Cryst.* **55**, 397-410, doi: 10.1107/S1600576722000139.](https://doi.org/10.1107/S1600576722000139)

Quando apropriado, também é possível citar o próprio repositório do software:

  * Repositório: https://github.com/seto77/ReciPro
  * Versões: https://github.com/seto77/ReciPro/releases/latest

***

## Instalação

* Baixe o [*ReciPro-setup.msi*](https://github.com/seto77/ReciPro/releases/latest/download/ReciPro-setup.msi) (link direto para a versão mais recente) e execute-o. Ele também está na [página de versões](https://github.com/seto77/ReciPro/releases/latest). (Até a v.4.939, o instalador chamava-se *ReciProSetup.msi*.)
* O *ReciPro* é executado no Windows com o ***.Net Desktop Runtime 10.0*** (NÃO o ***.Net Runtime 10.0***), que pode ser instalado [aqui](https://dotnet.microsoft.com/download/dotnet/10.0).
* Se não for possível executar um instalador (por exemplo, em computadores com permissões restritas), há também um pacote **ZIP portátil** (*ReciPro-v.X.XXX.zip*) na página de versões: autocontido, sem instalação e sem necessidade do runtime .NET — basta descompactar e executar.
* O *ReciPro* é distribuído sob a **licença MIT** (qualquer pessoa pode usar, modificar e redistribuir livremente).
* Sobre o estado da assinatura de código e a verificação do instalador, consulte a [política de assinatura de código](../../CODE_SIGNING.md).
* Sobre os componentes e dados de terceiros incluídos ou referenciados, consulte os [avisos de terceiros](../../THIRD-PARTY-NOTICES.md).

### macOS (não oficial)

* O *ReciPro* oferece suporte oficial apenas ao Windows, mas há relatos de que ele funciona no macOS (Apple Silicon) combinando o pacote **ZIP portátil** com o wrapper Wine **Sikarugir** e o driver OpenGL **Mesa3D** — sem licença do Windows e sem máquina virtual.
* Consulte o guia passo a passo publicado por Ryo Fukushima (JAMSTEC): https://github.com/Ryo-fkushima/ReciPro_macOS_memo
* Essa configuração não tem suporte oficial nem foi totalmente verificada. Uma limitação conhecida é que alguns símbolos (Å, sobrescritos, setas) podem ser exibidos incorretamente.
* Os símbolos corrompidos podem ser corrigidos instalando no prefixo do Wine fontes com ampla cobertura de glifos (**DejaVu Sans/Serif** e, para a interface em japonês, **Noto Sans CJK JP**) — o ReciPro detecta o ambiente Wine e passa a usá-las automaticamente. Veja mais detalhes em [Solução de problemas](https://seto77.github.io/ReciPro/pt/troubleshooting/).

### Observação sobre avisos de segurança do Windows

* Baixe o *ReciPro* somente na página oficial do GitHub Releases: https://github.com/seto77/ReciPro/releases/latest
* Em alguns sistemas Windows, o Microsoft Defender SmartScreen ou o Smart App Control pode exibir um aviso antes da execução do instalador. Isso pode ocorrer com software de pesquisa recém-compilado ou de distribuição restrita, e o aviso por si só não significa necessariamente que o instalador seja malicioso.
* Se quiser verificar por conta própria o instalador baixado, você pode analisá-lo com um serviço de múltiplos mecanismos, como o VirusTotal.

## Política de assinatura de código

[<img src="https://signpath.org/assets/favicon-50x50.png" alt="SignPath" height="20">](https://about.signpath.io/) Assinatura de código gratuita no Windows fornecida pela [SignPath.io](https://about.signpath.io/), com certificado da [SignPath Foundation](https://signpath.org/).

Desde a v.4.942, os artefatos de lançamento (o instalador *ReciPro-setup.msi* e o *ReciPro.exe* portátil) são assinados com o Windows Authenticode como parte do pipeline automatizado de publicação, e cada solicitação de assinatura é revisada e aprovada manualmente pelo mantenedor antes da publicação. Consulte o [CODE_SIGNING.md](../../CODE_SIGNING.md) para a política completa, incluindo o escopo da assinatura, como verificar um instalador e como relatar artefatos suspeitos.

## Privacidade

O *ReciPro* é um aplicativo de desktop local. Ele **não** coleta, armazena nem transmite dados pessoais ou de uso, e não contém telemetria ou análise. Após a instalação, funciona totalmente off-line.

As únicas conexões de rede feitas pelo *ReciPro* são downloads opcionais iniciados pelo usuário, e nenhum deles envia seus dados:

* **Verificar atualizações** (comando de menu): compara a versão instalada com a última versão publicada no GitHub e, se você escolher, baixa o novo instalador da página oficial [GitHub Releases](https://github.com/seto77/ReciPro/releases/latest).
* **Base de dados COD** (Crystallography Open Database): baixada no primeiro uso (~880 MB) a partir do espelho do autor no GitHub e depois utilizada off-line.
* **Biblioteca Intel MKL** (aceleração opcional): baixada (~55 MB) do [nuget.org](https://www.nuget.org/) apenas se você ativar a opção *Use MKL*, para acelerar os cálculos de difração dinâmica.

A base de dados AMCSD incluída e todos os recursos principais funcionam inteiramente off-line.

## Manual
  * Manual on-line (inglês / japonês): https://seto77.github.io/ReciPro/pt/
  * Versão em japonês: https://yseto.net/soft/recipro
***

## Principais recursos

### Base de dados cristalográfica

* **AMCSD** (American Mineralogist Crystal Structure Database): mais de 21.000 estruturas cristalinas integradas e disponíveis imediatamente após a instalação.
  * A base de dados é altamente compactada (~5 MB) e incluída no arquivo de instalação, portanto está disponível em ambientes off-line.
  * É possível pesquisar cristais por nome, composição química, parâmetros de rede, densidade, simetria e elementos presentes.
  * Referência: [Downs & Hall-Wallace, 2003, *American Mineralogist* **88**, 247-250](https://www.geo.arizona.edu/xtal/group/pdf/am88_247.pdf)
* **COD** (Crystallography Open Database): também estão disponíveis cerca de 525.000 estruturas cristalinas, incluindo cristais orgânicos.
  * Baixada automaticamente no primeiro uso (~880 MB) e disponível off-line em seguida.
  * Referências: [Gražulis et al., 2009, *J. Appl. Cryst.* **42**, 726-729](https://doi.org/10.1107/S0021889809016690); [Gražulis et al., 2012, *Nucleic Acids Res.* **40**, D420-D427](https://doi.org/10.1093/nar/gkr900)
* Importação/exportação de arquivos nos formatos CIF e AMC.

### Cálculos cristalográficos

* São suportadas 530 notações de grupos espaciais: 230 configurações padrão das ITA + 300 configurações de eixos não padronizadas.
  * Condições gerais (regras de extinção), posições de Wyckoff e multiplicidades de todos os grupos espaciais.
  * Cálculo geométrico da periodicidade e/ou dos ângulos entre planos e/ou eixos.
  * Geração de posições atômicas equivalentes.
  * Conversão simples entre configurações de eixos não padronizadas (por exemplo, de *Pbnm* para *Pnma*) e deslocamentos de origem.

### Propriedades atômicas

* Comprimento de onda/energia dos raios X característicos de <sup>1</sup>H a <sup>98</sup>Cf.
* Fatores de espalhamento atômico para raios X, elétrons e nêutrons.

### Visualizador de estruturas

* Visualização 3D de estruturas cristalinas usando a arquitetura OpenGL (GLSL).
  * Desenha átomos, ligações, poliedros de coordenação, células unitárias, planos reticulares, superfícies de contorno e rótulos de legenda.
  * Mesmo estruturas complexas com dezenas de milhares de átomos são desenhadas suavemente em tempo real.
  * As cores e os tamanhos padrão dos átomos são compatíveis com o VESTA.
  * O intervalo de desenho pode ser definido por múltiplos da célula unitária ou por índices de plano cristalino e distância ao centro.
  * Hábitos cristalinos arbitrários podem ser representados colorindo as faces de contorno.
  * Qualquer plano reticular pode ser exibido, o que ajuda iniciantes a compreender o conceito de plano reticular nos fenômenos de difração.
  * Rotação, deslocamento e zoom são controlados livremente com o mouse.
  * Ao clicar em um átomo, são exibidas as distâncias e os ângulos de ligação com os átomos vizinhos.
  * O estado de rotação é imediatamente refletido nas demais janelas funcionais (projeção estereográfica, simulador de difração etc.).
  * O codificador de vídeo integrado (Windows Media Foundation) pode gerar vídeos de animação de rotação (MP4 H.264/H.265) para apresentações.

### Projeção estereográfica

* Representa planos e eixos cristalinos em uma projeção estereográfica.
  * São suportadas tanto a projeção equiangular (rede de Wulff) quanto a equiareal (rede de Schmidt), com linhas de latitude e longitude.
  * Os índices podem ser especificados por intervalo numérico ou por valores específicos.
  * Círculos máximos podem ser exibidos especificando eixos de zona.
  * Os objetos desenhados podem ser salvos ou copiados em formato vetorial para edição posterior sem perda de resolução.
  * Visualização 3D da geometria da projeção estereográfica para fins didáticos.

### Simulador de difração

* Simula padrões de difração de monocristal para fontes de raios X, elétrons e nêutrons.
  * A energia cinética do feixe incidente pode ser configurada livremente.
  * As energias dos raios X característicos de <sup>1</sup>H a <sup>98</sup>Cf estão integradas.
  * O intervalo representado é definido pela resolução da imagem (tamanho do pixel) e pelo comprimento de câmara.
  * Também há suporte para geometrias com detector inclinado.
  * É possível sobrepor imagens obtidas experimentalmente.
  * A rotação do cristal (condição de difração) pode ser controlada e é sincronizada imediatamente com as demais janelas.

* **Difração policristalina**: simulação de anéis de Debye considerando uma amostra policristalina.
* **Câmara de precessão** (raios X): simulação de padrões de câmara de precessão da zona de Laue de ordem zero.
* **Câmara de Laue por retrorreflexão** (raios X): simulação de padrões de Laue por retrorreflexão.

#### Teoria cinemática da difração
* Disponível para todas as fontes (raios X, elétrons, nêutrons).
* As intensidades de difração são estimadas a partir do quadrado do módulo do fator de estrutura cristalina e do erro de excitação.
* Os efeitos do fator de Debye-Waller sobre as intensidades de difração estão incorporados.

#### Teoria dinâmica da difração (elétrons)
* Baseada no **método das ondas de Bloch** (Bethe, 1928), que permite orientações cristalinas flexíveis, sem restrição a eixos de zona de baixos índices.
* Há duas abordagens de cálculo disponíveis:
  * **Método de autovalores de Bethe**: diagonalização matricial para obter autovalores/autovetores dos autoestados de Bloch. Adequado quando se varia a espessura da amostra.
  * **Método da matriz de espalhamento**: cálculo direto de exponenciais de matrizes pelo método de escalonamento e elevação ao quadrado com aproximação de Padé. Adequado para cálculos rápidos com uma única espessura.
* O algoritmo mais rápido e a melhor biblioteca matemática (Eigen, Intel MKL ou Math.NET) são selecionados automaticamente.
* O potencial de absorção do espalhamento difuso térmico (TDS) é calculado analiticamente para garantir alto desempenho.

* **SAED** (difração de elétrons de área selecionada): simulação de difração de elétrons com feixe paralelo, incluindo efeitos de espalhamento dinâmico.
* **PED** (difração de elétrons por precessão): simula padrões PED a partir do ângulo de precessão e da resolução angular azimutal. Útil para análise de estruturas cristalinas e otimização de condições PED quase cinemáticas.
* **CBED** (difração de elétrons de feixe convergente): simula padrões CBED com semiângulo de convergência e número de divisões definidos pelo usuário. Há suporte para simulação ao longo da espessura, para determinação da espessura da amostra.
  * Padrões CBED com média de posição (PACBED).
  * Simulação CBED de grande ângulo (LA-CBED).

### Simulador de HRTEM

* Simulação de imagens de microscopia eletrônica de transmissão de alta resolução no mesmo arcabouço teórico das ondas de Bloch.
* Os parâmetros ópticos (tensão de aceleração, coeficiente de aberração esférica, valor de desfoco, espessura da amostra etc.) são definidos pela interface gráfica.
* Predefinições típicas de parâmetros ópticos de MET estão integradas e podem ser chamadas com o botão direito.
* Dois modelos de formação de imagem para coerência parcial:
  * **Teoria linear de transferência de contraste**: menor custo computacional; adequada para amostras finas que satisfazem a aproximação de objeto de fase fraca.
  * **Teoria não linear de transferência de contraste (modelo TCC)**: baseada no coeficiente cruzado de transmissão de primeira ordem (Ishizuka, 1980); confiável mesmo para amostras mais espessas e materiais de número atômico elevado.
* A função de transferência de contraste com funções envelope pode ser representada graficamente.
* Séries de imagens espessura-desfoco podem ser calculadas simultaneamente.
* Em condições padrão, o cálculo normalmente termina em menos de 1 segundo.

### Simulador de STEM

* Simulação de imagens de microscopia eletrônica de transmissão por varredura.
  * Modos de imagem de campo claro (BF), campo escuro anular (ADF) e campo escuro anular de alto ângulo (HAADF).
  * O feixe convergente é tratado como superposição de muitas ondas planas, com cálculo preciso das sobreposições.
  * Os elétrons espalhados inelasticamente são calculados com o modelo de potencial absortivo.
  * Séries de imagens espessura-desfoco podem ser geradas.

### Spot ID

* Indexação semiautomática de pontos de difração em padrões SAED experimentais.
* **Spot ID v1**: busca eixos de zona a partir da configuração geométrica (distâncias e ângulos) dos pontos de difração. Permite a análise simultânea de 2 a 3 imagens.
* **Spot ID v2**: importa diretamente imagens de padrões SAED.
  * Suporta formatos de imagem comuns: TIFF (.tif), Digital Micrograph 3/4 (.dm3, .dm4), entre outros.
  * Detecção e ajuste automáticos dos pontos de difração com funções pseudo-Voigt 2D.
  * Busca exaustiva de orientações cristalinas compatíveis com o arranjo dos vetores da rede recíproca.
  * Determinação precisa mesmo de eixos de zona de ordem elevada.

### Geometria de rotação (goniômetro)

* Relaciona os ângulos de Euler do ReciPro ao goniômetro do laboratório.
* Informa como o goniômetro deve ser girado para obter a orientação cristalina desejada (por exemplo, um eixo de zona de baixos índices).
* Suporta definições arbitrárias de goniômetro.

### Macros

* Macros com sintaxe Python para automatizar tarefas.
  * Exemplo: girar um cristal em passos de 1° e salvar os padrões de difração ou as imagens STEM em cada passo.
  * As funções específicas do ReciPro estão disponíveis no espaço de nomes "ReciPro".
  * Há exemplos de uso no [manual](https://seto77.github.io/ReciPro/pt/20-macro/2-examples/).

### Outros recursos

* **Simulador de alcance eletrônico**: simulação de Monte Carlo do alcance de elétrons em materiais.
* **EBSD** (difração de elétrons retroespalhados): em desenvolvimento.

## Detalhes técnicos

* Escrito em **C++**, **C#** e **OpenGL Shading Language (GLSL)**.
* Paralelização com múltiplas threads para cálculos de alto desempenho em CPUs modernas com muitos núcleos.
* Todas as janelas funcionais são atualizadas de forma sincronizada e em tempo real quando a orientação do cristal muda.
* Usa um sistema de coordenadas cartesianas destrógiro (X: direita, Y: cima, Z: frente) com a convenção de ângulos de Euler Z–X–Z.
* As definições de coordenadas são compatíveis com o software de EBSD da Thermo Fisher Scientific.

### Impacto acadêmico

* **Artigo de software revisado por pares:** [Seto, Y. & Ohtsuka, M. (2022), *Journal of Applied Crystallography*, **55**, 397-410](https://doi.org/10.1107/S1600576722000139).
* **Artigos que o citam:** [artigos citantes no Google Scholar](https://scholar.google.jp/scholar?cites=12625594477623342627).
* **Repercussão do artigo:** [detalhes no Altmetric](https://www.altmetric.com/details/123778746).

| Indicador | Valor principal |
| --- | --- |
| Downloads totais no GitHub | mais de 27.000 downloads |
| Citações no Google Scholar | mais de 170 citações |
| Citações no Dimensions | mais de 160 citações |
| Leitores no Mendeley | mais de 90 leitores |

## Capturas de tela

<img src="https://seto77.github.io/ReciPro/assets/cap-pt-auto/FormMain.png" height="320px" alt="Janela principal">
<img src="https://seto77.github.io/ReciPro/assets/cap-pt-auto/FormCrystalDatabase.png" height="320px" alt="Base de dados cristalográfica">
<img src="https://seto77.github.io/ReciPro/assets/cap-pt-auto/FormSymmetryInformation.png" height="320px" alt="Informações de simetria">
<img src="https://seto77.github.io/ReciPro/assets/cap-pt-auto/FormBeamInteraction.png" height="320px" alt="Interação do feixe">
<img src="https://seto77.github.io/ReciPro/assets/cap-pt-auto/FormStructureViewer.png" height="320px" alt="Visualizador de estruturas">
<img src="https://seto77.github.io/ReciPro/assets/cap-pt-auto/FormStereonet.png" height="320px" alt="Projeção estereográfica">
<img src="https://seto77.github.io/ReciPro/assets/cap-pt-auto/FormDiffractionSimulator.png" height="320px" alt="Simulador de difração">
<img src="https://seto77.github.io/ReciPro/assets/cap-pt-auto/FormImageSimulator.png" height="320px" alt="Simulador de HRTEM/STEM">
<img src="https://seto77.github.io/ReciPro/assets/cap-pt-auto/FormSpotIDV2.png" height="320px" alt="Spot ID v2">
<img src="https://seto77.github.io/ReciPro/assets/cap-pt-auto/FormMacro.png" height="320px" alt="Macros">
<img src="https://seto77.github.io/ReciPro/assets/cap-pt-auto/FormTrajectory.png" height="320px" alt="Simulador de alcance eletrônico">

***
