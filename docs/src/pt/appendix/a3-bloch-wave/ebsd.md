# Cálculo de EBSD

O EBSD (difração de elétrons retroespalhados) usa o mesmo núcleo de Bethe/ondas de Bloch que CBED e STEM, mas o problema é formulado de maneira diferente. CBED e STEM são **problemas de feixe incidente**: uma onda eletrônica entra na amostra a partir do exterior e a onda de saída é calculada. O EBSD é um **problema de direção de saída**: elétrons que sofreram espalhamento inelástico no interior da amostra emergem como elétrons retroespalhados, e o cálculo pergunta quanta intensidade sai em cada direção externa.

O ReciPro converte esse problema de direção de saída em um problema comum de feixe incidente por meio do teorema da reciprocidade. Ele primeiro calcula um **master pattern** no espaço de direções e, em seguida, combina esse master pattern com os pesos de Monte Carlo para profundidade / energia / direção e a geometria do detector para formar o detector pattern.

---

## Reformulação com o teorema da reciprocidade

Se a amplitude de um ponto-fonte interno $\mathbf r_n$ para uma direção externa $\widehat{\mathbf s}$ fosse calculada diretamente, seria necessário um problema de espalhamento separado para cada ponto-fonte. Isso não é prático.

O teorema da reciprocidade reescreve o problema da seguinte forma: a amplitude para que um elétron que parte de $\mathbf r_n$ apareça na direção de campo distante $\widehat{\mathbf s}$ é igual à amplitude, em $\mathbf r_n$, de uma onda recíproca incidente a partir da direção externa $-\widehat{\mathbf s}$. Essa onda recíproca é uma solução comum de Bethe/ondas de Bloch. Escrevendo-a como $\psi_{\widehat{\mathbf s}}^{\mathrm{rec}}(\mathbf r)$, a intensidade de EBSD na direção $\widehat{\mathbf s}$ pode ser escrita como

$$I_{\mathrm{EBSD}}(\widehat{\mathbf s};E,z)\propto
\sum_n \sigma_n(E,z)\,
\left|\psi_{\widehat{\mathbf s}}^{\mathrm{rec}}(\mathbf r_n;E,z)\right|^2$$

onde $\sigma_n(E,z)$ é o peso para o espalhamento inelástico próximo à posição atômica $\mathbf r_n$ no canal de retroespalhamento na energia $E$ e profundidade $z$. Os termos de fonte são somados como intensidades, não como uma soma de amplitudes coerente, porque se supõe que o espalhamento inelástico destrói a relação de fase entre diferentes posições de fonte.

---

## Master Pattern

O master pattern de EBSD armazena a parte de difração dinâmica específica do cristal da expressão acima em uma grade de direções. Conceitualmente,

$$M(\widehat{\mathbf s};E,z)=
\sum_n w_n(E,z)\,
\left|\psi_{\widehat{\mathbf s}}^{\mathrm{rec}}(\mathbf r_n)\right|^2$$

onde $w_n$ é o peso de fonte inelástica do lado do cristal na posição atômica $\mathbf r_n$. O ReciPro usa o peso empírico

$$w_n \propto Z_n^{1.7}\,\mathrm{occ}_n$$

com número atômico $Z_n$ e ocupação $\mathrm{occ}_n$. Isso é separado da distribuição de profundidade / energia de transporte produzida pelo Monte Carlo.

Na implementação, a onda de Bloch recíproca é avaliada em cada posição atômica:

$$\beta_n^{(j)}=
\alpha^{(j)}
\sum_{\mathbf g}C_{\mathbf g}^{(j)}
\exp\!\left[2\pi i(\mathbf k^{(j)}+\mathbf g)\cdot\mathbf r_n\right]$$

O código então forma a matriz de pares de ondas de Bloch

$$S_{jj'}=\sum_n w_n\,\beta_n^{(j)}\,\overline{\beta_n^{(j')}}$$

e a integral analítica de espessura

$$\mathcal F_{jj'}(t)=
\frac{\exp\!\left[2\pi i(\gamma^{(j)}-\overline{\gamma^{(j')}})t\right]-1}
{2\pi i(\gamma^{(j)}-\overline{\gamma^{(j')}})}$$

de modo que o master pattern é avaliado como

$$M(\widehat{\mathbf s};E,t)=
\mathrm{Re}\left\{\sum_{j,j'}S_{jj'}(E)\,\mathcal F_{jj'}(t)\right\}$$

No limite degenerado em que o denominador está próximo de zero, $\mathcal F_{jj'}(t)\to t$.

---

## Amostragem do espaço de direções

O master pattern não é a própria imagem do detector; é uma distribuição de intensidade no espaço de direções fixo ao cristal. O ReciPro amostra esse espaço de direções com uma projeção equiárea de Rosca-Lambert e armazena os hemisférios $+Z$ e $-Z$ como matrizes planas separadas. A amostragem equiárea reduz o viés de densidade entre os polos e o equador.

Nesse estágio, o master pattern depende da estrutura cristalina, da tensão de aceleração, da profundidade, da energia e do modelo de absorção. A geometria do detector, como o centro do padrão e a posição da tela, ainda não foi aplicada.

---

## Pesos de Monte Carlo e detector pattern

Para obter um detector pattern de EBSD próximo do observável experimental, o master pattern deve ser ponderado por quantos elétrons retroespalhados emergem de cada profundidade, energia e direção. Escrevendo esse peso de transporte como

$$W(E,z;\widehat{\mathbf s})$$

e usando $\widehat{\mathbf s}(\mathbf p)$ para a direção fixa ao cristal correspondente ao pixel do detector $\mathbf p$, o detector pattern final é

$$P(\mathbf p)=
\sum_{i,j}
W(E_i,z_j;\widehat{\mathbf s}(\mathbf p))\,
M(\widehat{\mathbf s}(\mathbf p);E_i,z_j)$$

como uma soma discreta sobre energia e profundidade.

A parte de Monte Carlo acompanha o espalhamento elástico, o espalhamento inelástico, a perda de energia e o escape pela superfície da amostra. Para os elétrons retroespalhados, ela constrói distribuições de profundidade, energia e direção de saída. O ReciPro distingue modelos que usam a última posição de espalhamento inelástico e a energia imediatamente posterior a ela como fonte efetiva, e modelos que usam a profundidade de escape e a energia de escape.

---

## Fundo de TDS e modelo de absorção

Os padrões de EBSD contêm não apenas a estrutura geométrica de bandas de Kikuchi, mas também um fundo suave proveniente do espalhamento térmico difuso (TDS). Quando `IncludeTDSBackground` está habilitado, o ReciPro avalia a componente de TDS espalhada para o hemisfério posterior,

$$\pi/2\leq\theta\leq\pi$$

como uma matriz de absorção $\mu_{\mathrm{back}}$ e adiciona a intensidade de fundo usando a mesma soma de pares de ondas de Bloch que o master pattern. Como a mesma autossolução é reutilizada, o fundo de TDS acrescenta relativamente pouco custo adicional.

Quando `UseNonLocalAbsorption` está habilitado, o potencial de absorção é tratado não apenas como $U'_{\mathbf g-\mathbf h}$, mas como uma forma não local que depende da direção e dos pares de feixes. Isso pode melhorar a precisão, mas também exige a reconstrução da matriz de absorção para as direções da grade do master pattern, podendo, portanto, aumentar substancialmente o tempo de cálculo.

---

## Parâmetros práticos

- **Número de feixes**: Poucos feixes perdem os detalhes das bandas de Kikuchi e a estrutura de bandas de HOLZ. Eixos de zona de baixos índices podem exigir várias centenas de feixes.
- **Matrizes de profundidade e energia**: Se forem mais grosseiras que a escala de variação do peso de Monte Carlo $W(E,z;\widehat{\mathbf s})$, a largura de banda dependente da energia e os efeitos de profundidade de canalização são suprimidos pela média.
- **Geometria do detector**: O centro do padrão, a distância da tela e a inclinação da amostra determinam o mapeamento $\widehat{\mathbf s}(\mathbf p)$, de modo que o detector pattern pode mudar mesmo quando o master pattern permanece inalterado.
- **Interpretação da reciprocidade**: O master pattern não é a imagem do detector. Ele só se torna um detector pattern após a ponderação de Monte Carlo e a projeção no detector.
- **Fundo de TDS**: Habilite-o para comparações quantitativas de contraste de bandas. Desabilite-o quando a estrutura geométrica de Kikuchi for mais fácil de inspecionar sem o fundo suave.

## Veja também

- [Cálculo dinâmico (núcleo comum)](calculation.md)
- [Apêndice A3. Difração dinâmica pelo método de ondas de Bloch](index.md)
- [12. Simulação EBSD](../../12-ebsd-simulation.md)
