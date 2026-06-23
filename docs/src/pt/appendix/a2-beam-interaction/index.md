# Apêndice A2. Interação do feixe (fundamentos de física do estado sólido)

O capítulo da janela principal [3. Beam interaction](../../3-beam-interaction.md) é um guia para a GUI: ele indica quais botões pressionar e o que cada coluna significa. Este apêndice reúne a **física do estado sólido e do espalhamento** por trás desses números — por que um átomo espalha raios X, elétrons e nêutrons de maneira tão diferente, de onde vêm o fator de estrutura e sua parte imaginária, como um feixe é atenuado e desacelerado dentro de um sólido, e o que a prévia de fluorescência representa e o que não representa.

![Beam Interaction window](../../../assets/cap-pt-auto/FormBeamInteraction.png)

A janela tem quatro abas, e a teoria é mais bem lida na ordem em que uma grandeza alimenta a seguinte:

1. **[Atomic scattering factors](scattering-factor.md)** — como um *único átomo* espalha cada tipo de feixe.
2. **[Structure factor](structure-factor.md)** — como os átomos em uma *célula unitária* interferem, incluindo o fator de Debye–Waller e as regras de extinção.
3. **[Attenuation & transport](attenuation-transport.md)** — como o feixe é *removido e desacelerado* ao atravessar o material.
4. **[Fluorescence](fluorescence.md)** — a emissão de raios X característicos que segue a ionização de uma camada interna.

---

## Geometria de espalhamento e a variável $s$

Toda grandeza de espalhamento nesta janela é uma função de quanto a direção do feixe muda. Escrevendo $\mathbf k_i$ e $\mathbf k_s$ para os vetores de onda incidente e espalhado (elástico, portanto $|\mathbf k_i|=|\mathbf k_s|=1/\lambda$), o **vetor de espalhamento** e seu módulo são

$$\mathbf Q = 2\pi(\mathbf k_s - \mathbf k_i), \qquad Q = |\mathbf Q| = \frac{4\pi\sin\theta}{\lambda} = 4\pi s .$$

- $\theta$ : o ângulo de Bragg — a *metade* do ângulo total de espalhamento. A tabela Reflections lista o ângulo completo $2\theta$.
- $s = \dfrac{\sin\theta}{\lambda}$ (Å⁻¹) : a variável em função da qual a aba **Scattering factors** é traçada. É o argumento natural de todo fator de forma atômico.
- $d$ : o espaçamento interplanar. Na condição de Bragg $\lambda = 2d\sin\theta$, de modo que $s = \dfrac{1}{2d} = \dfrac{|\mathbf g|}{2}$, onde $\mathbf g$ é o vetor da rede recíproca com $|\mathbf g| = 1/d$.

Essas três convenções descrevem a mesma geometria; apenas a escala difere. Vale a pena manter a correspondência clara, pois a janela usa mais de uma delas:

| Na janela | Símbolo | Relação |
|---|---|---|
| Tabela Reflections | $q = 2\pi/d$ | $q = 2\pi\lvert\mathbf g\rvert = Q = 4\pi s$ |
| Tabela Reflections | $2\theta$ | ângulo total de espalhamento, $\sin\theta = \lambda s$ |
| Aba Scattering factors | $s = \sin\theta/\lambda$ | $s = q/4\pi = 1/(2d)$ |
| Gráfico do pico de difração | $Q = 4\pi\sin\theta/\lambda$ | $Q = q = 4\pi s$ |

!!! note "Units"
    As parametrizações publicadas dos fatores de forma usam $s$ em Å⁻¹ (logo $s^2$ em Å⁻²), enquanto o ReciPro carrega $s^2$ internamente em nm⁻². As duas diferem por um fator $100$ em $s^2$; as curvas e tabelas são apresentadas nas unidades indicadas no cabeçalho de cada tabela. Um modelo — **Kirkland** — é tabelado em função de $q = 2s = 1/d$ em vez de $s$; veja [Atomic scattering factors](scattering-factor.md).

### Bragg, Laue e a esfera de Ewald {#phase-convention}

A condição de Bragg é uma face de um único requisito geométrico. A interferência construtiva (a **condição de Laue**) exige que o vetor de espalhamento seja igual a um vetor da rede recíproca,

$$\mathbf k_s = \mathbf k_i + \mathbf g, \qquad |\mathbf k_i + \mathbf g|^2 = |\mathbf k_i|^2 ,$$

que, com $|\mathbf k_i|=|\mathbf k_s|=1/\lambda$, reduz-se a

$$2\,\mathbf k_i\cdot\mathbf g + |\mathbf g|^2 = 0 \qquad\Longleftrightarrow\qquad |\mathbf g| = \frac{1}{d} = \frac{2\sin\theta}{\lambda},$$

isto é, a **lei de Bragg** $\lambda = 2d\sin\theta$. Geometricamente, esta é a construção da **esfera de Ewald**: uma reflexão é excitada quando seu ponto da rede recíproca se encontra sobre a esfera de raio $1/\lambda$. (Aqui $\mathbf g$ está em unidades de $1/d$, portanto $\mathbf Q = 2\pi\mathbf g$.)

---

## Convenção de fase

O ReciPro constrói os fatores de estrutura com a convenção de fase cristalográfica

$$F_{\mathbf g} = \sum_j \dots \exp\!\left(-2\pi i\,\mathbf g\cdot\mathbf r_j\right),$$

isto é, um sinal de **menos** no expoente. Essa escolha fixa o sinal da parte imaginária do fator de estrutura (`F_inv` na tabela Reflections) e a relação entre pares de Friedel uma vez que a dispersão anômala é ativada. Ela é enunciada aqui uma vez e pressuposta em todo o apêndice; as consequências são desenvolvidas em [Structure factor](structure-factor.md).

---

## Espalhamento cinemático vs. dinâmico

Este apêndice trata do **espalhamento simples (cinemático)**: o feixe incidente espalha uma vez, e a amplitude difratada é o fator de estrutura da próxima página. Essa é a imagem correta quando a interação é fraca — raios X e nêutrons em quase todas as amostras, e elétrons em espécimes *muito finos*.

Quando a interação é forte — elétrons em qualquer cristal que não seja o mais fino — o feixe espalha muitas vezes antes de sair, a intensidade é redistribuída entre as reflexões, e $\lvert F\rvert^2$ não fornece mais a intensidade medida. Esse regime requer a teoria **dinâmica** do [Appendix A3](../a3-bloch-wave/index.md). Os fatores de espalhamento e os fatores de estrutura aqui deduzidos são a *entrada* para ambas as imagens.

Mesmo no limite cinemático, a amplitude difratada não é apenas o fator de estrutura: somar a onda espalhada ao longo de uma lâmina de espessura $t$ resulta em

$$A_{\mathbf g}(t) \;\propto\; F_{\mathbf g}\int_0^t e^{\,2\pi i S_{\mathbf g} z}\,dz = F_{\mathbf g}\, t\, e^{\,\pi i S_{\mathbf g} t}\,\operatorname{sinc}(\pi S_{\mathbf g} t),$$

onde $S_{\mathbf g}$ é o **erro de excitação** — a distância do ponto da rede recíproca à esfera de Ewald. A intensidade atinge um pico acentuado em $S_{\mathbf g}=0$ e oscila com a espessura (a origem das franjas de espessura); a teoria dinâmica do [Appendix A3](../a3-bloch-wave/index.md) substitui esse resultado de feixe único pelo comportamento de feixes acoplados.

---

## As três sondas em resumo

| | Raio X | Elétron | Nêutron |
|---|---|---|---|
| Interage com | densidade eletrônica $\rho_e$ | potencial eletrostático $V$ | núcleos (e spins desemparelhados) |
| Intensidade da interação | fraca | forte | muito fraca |
| Penetração típica | µm – mm | nm – µm | mm – cm |
| Espalhamento simples válido? | quase sempre | apenas lâminas finas | quase sempre |
| Sensibilidade a átomos leves | baixa ($\propto Z$) | moderada | frequentemente excelente |

Esses contrastes reaparecem ao longo das páginas seguintes, cada um rastreável ao mecanismo de espalhamento em [Atomic scattering factors](scattering-factor.md).

---

## Veja também

- [3. Beam interaction](../../3-beam-interaction.md) — a GUI que este apêndice explica.
- [Atomic scattering factors](scattering-factor.md) · [Structure factor](structure-factor.md) · [Attenuation & transport](attenuation-transport.md) · [Fluorescence](fluorescence.md)
- [Appendix A1. Coordinate systems](../a1-coordinate-system/1-orientation.md)
- [Appendix A3. Dynamical diffraction (Bloch-wave method)](../a3-bloch-wave/index.md) — a teoria de espalhamento múltiplo que usa esses fatores de espalhamento.
