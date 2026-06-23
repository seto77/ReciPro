# Fatores de espalhamento atômico

O **fator de espalhamento atômico** (ou *fator de forma*) mede quão intensamente um único átomo espalha o feixe incidente em função da variável de espalhamento $s=\sin\theta/\lambda$. As três radiações interagem com partes completamente diferentes do átomo, de modo que seus fatores de espalhamento têm magnitudes, unidades e dependência angular distintas. Esta é a razão mais importante pela qual a aba **Scattering factors** parece tão diferente entre os feixes de raios X, de elétrons e de nêutrons.

=== "X-ray"
    ![Fatores de espalhamento — raios X](../../../assets/cap-pt-auto/FormBeamInteraction-xray-scattering.png)

=== "Electron"
    ![Fatores de espalhamento — elétron](../../../assets/cap-pt-auto/FormBeamInteraction-electron-scattering.png)

=== "Neutron"
    ![Fatores de espalhamento — nêutron](../../../assets/cap-pt-auto/FormBeamInteraction-neutron-scattering.png)

---

## Raios X — espalhamento pela nuvem eletrônica

Os raios X são espalhados pelos **elétrons** do átomo. Um único elétron livre espalha com a seção de choque diferencial clássica de **Thomson**, definida pelo raio clássico do elétron $r_e = e^2/(4\pi\varepsilon_0 m_e c^2) \approx 2.82\times10^{-5}\ \text{Å}$:

$$\left(\frac{d\sigma}{d\Omega}\right)_e = r_e^2\,\frac{1+\cos^2 2\theta}{2}.$$

Os elétrons do átomo estão distribuídos no espaço com densidade numérica $\rho_e(\mathbf r)$, e o fator de espalhamento atômico é a **transformada de Fourier** dessa densidade. A seção de choque atômica é então a seção de choque de um único elétron escalada por $|f_0|^2$:

$$f_0(\mathbf Q) = \int \rho_e(\mathbf r)\, e^{\,i\mathbf Q\cdot\mathbf r}\, d^3r ,
\qquad
\left(\frac{d\sigma}{d\Omega}\right)_\text{atom} = r_e^2\,\frac{1+\cos^2 2\theta}{2}\,|f_0(s)|^2 .$$

- Na direção para frente ($s\to 0$) todo elétron espalha em fase, de modo que $f_0(0) = Z$, o número atômico. O fator é expresso em **unidades de elétron** (múltiplos da amplitude de Thomson — a segunda equação acima torna isso explícito).
- À medida que $s$ aumenta, o espalhamento de diferentes partes da nuvem fica fora de fase e $f_0(s)$ decai. Uma distribuição eletrônica difusa (externa, de valência) faz $f_0$ cair rapidamente; os elétrons de caroço fortemente ligados continuam contribuindo até $s$ elevados.

Na prática, $f_0(s)$ é tabulado como uma soma de gaussianas (a forma analítica de **Waasmaier–Kirfel** que o ReciPro utiliza, uma extensão das tabelas mais antigas de Cromer–Mann),

$$f_0(s) = \sum_{i} a_i\, e^{-b_i s^2} + c ,$$

que é o que o ReciPro avalia para a curva. Os coeficientes são tabulados para $s$ em Å⁻¹, de modo que cada $b_i$ tem unidades de Å²; o ReciPro carrega $s^2$ internamente em nm⁻² e aplica a conversão pelo fator 100 mencionada no [índice](index.md).

### Dispersão anômala (ressonante)

A imagem da transformada de Fourier pressupõe que os elétrons espalham como se estivessem livres. Quando a energia do fóton se aproxima de uma **borda de absorção**, os elétrons ligados respondem de forma ressonante e surgem dois termos de correção dependentes da energia:

$$f(s,E) = f_0(s) + f'(E) + i\,f''(E) \qquad \text{(textbook, } e^{+i\phi}\ \text{convention).}$$

- $f'(E)$ : correção de dispersão real (reduz a contagem efetiva de elétrons próximo a uma borda).
- $f''(E)$ : parte imaginária, máxima logo acima de uma borda.
- As duas estão ligadas pelas relações de **Kramers–Kronig**, de modo que um pico na absorção ($f''$) é acompanhado por uma oscilação dispersiva em $f'$.

Estes não são parâmetros livres. A causalidade (Kramers–Kronig) liga $f'$ a $f''$, e o **teorema óptico** liga $f''$ diretamente à seção de choque de fotoabsorção:

$$f'(E) = \frac{2}{\pi}\,\mathcal{P}\!\!\int_0^\infty \frac{E'\,f''(E')}{E'^2 - E^2}\,dE',
\qquad
f''(E) = \frac{\sigma_\text{abs}(E)}{2\,r_e\,\lambda}.$$

Aqui $\sigma_\text{abs}$ é essencialmente a parte de **fotoabsorção** da atenuação (não os termos de Rayleigh/Compton) — a mesma estrutura de borda vista na página [Atenuação & transporte](attenuation-transport.md).

O ReciPro avalia $f'$ e $f''$ na energia atual com a biblioteca **xraylib** incluída e os lista na tabela (com $f'' > 0$). Dois pontos de sinal importam. Primeiro, a xraylib retorna $F_{ii}$ com o sinal oposto ao da convenção cristalográfica, de modo que o ReciPro o nega para reportar um **$f''$ positivo**. Segundo, sob a convenção de fase $\exp(-2\pi i\,\mathbf g\cdot\mathbf r)$ do ReciPro, o fator complexo que de fato entra no fator de estrutura é $f_0 + f' - i f''$ — o $+i f''$ escrito acima pertence à convenção oposta ($e^{+2\pi i}$). É por isso que `F_inv` (a parte imaginária do fator de estrutura) se torna não nula próximo a uma borda — veja [Fator de estrutura](structure-factor.md).

---

## Elétrons — espalhamento pelo potencial eletrostático

Um elétron rápido é carregado, portanto é espalhado pelo **potencial eletrostático** $V(\mathbf r)$ do átomo — a combinação do núcleo positivo e da nuvem eletrônica negativa. O fator de espalhamento eletrônico $f_e$ é, portanto, a transformada de Fourier do potencial, o que o vincula ao fator de raios X pela equação de Poisson. O resultado é a **relação de Mott–Bethe**:

$$f_e(s) = C_\text{MB}\,\frac{Z - f_0(s)}{s^2} \;\;\propto\; \frac{Z - f_X(Q)}{Q^2}.$$

O prefator $C_\text{MB}$ é construído a partir de constantes fundamentais e depende do sistema de unidades e de se $s$ ou $Q$ é usado. O ReciPro não avalia esta relação diretamente — ele usa as formas ajustadas de Peng / Kirkland / 8 gaussianas abaixo — de modo que ela é dada aqui para compreensão física e não para cálculo. Escrita explicitamente com as constantes (para $s$ e $f_e$ em Å),

$$f_e(s)\,[\text{Å}] = \frac{m_e e^2}{8\pi\varepsilon_0 h^2}\,\frac{Z - f_0(s)}{s^2} \simeq 0.023934\,\frac{Z - f_0(s)}{s^2}, \qquad s\ \text{in Å}^{-1},$$

com um $\times 0.1$ adicional quando o ReciPro reporta $f_e$ em nm, e um fator relativístico $\gamma$ extra (abaixo) no potencial dinâmico.

A física está no numerador $Z - f_0$: o elétron vê a **diferença** entre a carga nuclear $Z$ e a nuvem eletrônica de blindagem $f_0$, isto é, o potencial atômico líquido.

- **Magnitude.** Por causa do fator $1/s^2$, $f_e$ é fortemente concentrado em pequenos ângulos e é muito maior (em suas próprias unidades) e mais direcionado para frente do que $f_0$. É por isso que a difração de elétrons é dominada por reflexões de baixa ordem e por que o espalhamento dinâmico (múltiplo) é relevante — veja o [Apêndice A3](../a3-bloch-wave/index.md).
- **Limite de pequeno ângulo.** Para um átomo *neutro*, tanto $Z-f_0\to 0$ quanto $s^2\to 0$, de modo que $f_e(0)$ é finito (um limite $0/0$ fixado pelo raio atômico quadrático médio). Para um **íon**, a nuvem não cancela mais $Z$ e a cauda coulombiana de longo alcance faz $f_e$ divergir quando $s\to 0$; os fatores eletrônicos iônicos tabulados devem ser tratados com cuidado nos menores ângulos.
- **Correção relativística.** Nas energias de TEM, a massa e o comprimento de onda do elétron são relativísticos. O comprimento de onda usa a forma relativística $\lambda = h/\sqrt{2 m_0 e U\,(1 + e U/2 m_0 c^2)}$, e o potencial de interação carrega o fator relativístico $\gamma = 1 + eU/m_0c^2$. O ReciPro aplica essa correção ao formar o potencial dinâmico.

O ReciPro oferece três parametrizações de $f_e(s)$:

- **Peng** : um ajuste de cinco gaussianas, $f_e(s)=\sum_i a_i e^{-b_i s^2}$, conveniente e amplamente usado para o espalhamento elástico de elétrons.
- **Kirkland** : um ajuste misto Lorentziana + gaussiana, $f_e(q)=\sum_i \dfrac{a_i}{q^2+b_i} + \sum_i c_i\,e^{-d_i q^2}$. **Sua variável independente é $q = 2s = 1/d$, não $s$** — uma fonte frequente de erros de fator dois ao comparar modelos ($q$ em Å⁻¹, com os coeficientes ajustados $a_i,b_i,c_i,d_i$ nas unidades correspondentes).
- **8-Gaussians** : um ajuste de oito termos válido sobre uma faixa mais ampla de $s$.

**Escolhendo um.** Os três ajustam o mesmo $f_e(s)$ subjacente e concordam estreitamente em $s$ baixo; eles diferem principalmente na faixa e em como o caroço atômico é representado. **Peng** (átomos neutros e íons comuns, preciso até $s\approx2\text{–}6$ Å⁻¹) é o padrão usual para fatores de estrutura de SAED/CBED; **Kirkland** estende-se a $s$ mais altos com um termo de caroço Lorentziano, adequado para HRTEM/STEM (lembre-se de $q=2s$); **8-Gaussians** é para reflexões que alcançam $s$ muito elevados. Para um elemento leve, os três são quase indistinguíveis; as diferenças aparecem para elementos pesados em ângulos altos.

---

## Nêutrons — espalhamento pelo núcleo

Os nêutrons térmicos não têm carga e interagem com a matéria principalmente por meio da **força nuclear forte**, cujo alcance (femtômetros) é totalmente desprezível em comparação com o comprimento de onda do nêutron (ångströms). A interação é representada pelo **pseudopotencial de Fermi**, uma fonte pontual cuja intensidade é o comprimento de espalhamento $b$:

$$V(\mathbf r) = \frac{2\pi\hbar^2}{m_n}\,b\,\delta(\mathbf r)
\qquad\Longrightarrow\qquad
\frac{d\sigma}{d\Omega} = |b|^2 .$$

Como o centro espalhador é pontual, $b$ é **independente de $s$** — não há decaimento do fator de forma, razão pela qual a aba **Scattering factors** não desenha nenhuma curva para nêutrons e, em vez disso, exibe uma tabela de comprimentos de espalhamento.

- $b$ é uma propriedade do **nuclídeo**, não da configuração eletrônica. Varia irregularmente de elemento para elemento (e entre isótopos), pode ser **negativo** (por exemplo, ¹H, Ti, Mn) e não guarda relação monotônica com $Z$. Esta é a base do contraste de nêutrons (átomos leves próximos a pesados, marcação isotópica).
- **Coerente vs. incoerente.** Um elemento real é uma mistura de isótopos e estados de spin nuclear com diferentes $b$. A separação $b = \langle b\rangle + \delta b$ resulta em uma parte coerente (da média) e uma parte incoerente (da dispersão):

$$\sigma_\text{coh} = 4\pi\,|\langle b\rangle|^2, \qquad \sigma_\text{inc} = 4\pi\big(\langle |b|^2\rangle - |\langle b\rangle|^2\big), \qquad \sigma_s = \sigma_\text{coh} + \sigma_\text{inc}.$$

  A parte coerente produz a difração de Bragg (é o que entra no fator de estrutura); a parte incoerente é um fundo plano e isotrópico (grande para ¹H, a razão da deuteração).

!!! note "Tabulated values"
    O ReciPro lê $b_\text{coh}$ e as seções de choque de uma tabela de nuclídeos em vez de calculá-los. Para nuclídeos ressonantes, o $\sigma_\text{coh}$ listado não precisa ser igual ao $4\pi b^2$ ingênuo, de modo que os valores da tabela são os que prevalecem. O espalhamento magnético de nêutrons (por spins eletrônicos desemparelhados, que *de fato* tem um fator de forma dependente de $s$) não é modelado aqui.

---

## Em resumo

| | X-ray | Electron | Neutron |
|---|---|---|---|
| Espalhado por | nuvem eletrônica $\rho_e(\mathbf r)$ | potencial eletrostático $V(\mathbf r)$ | núcleo (ponto) |
| Dependência de $s$ | decai (FT da nuvem) | $\propto (Z-f_0)/s^2$, fortemente para frente | nenhuma ($b$ constante) |
| Valor para frente | $f_0(0)=Z$ | finito (neutro) / divergente (íon) | $b$ |
| Dependência da energia | $f',f''$ perto das bordas | relativística $\lambda,\gamma$ | $\sigma_\text{abs}\propto 1/v$ (não $b$) |
| Ordem de magnitude típica | $\propto Z$ | concentrada para frente, cresce com $Z$ | irregular, pode ser $<0$ |

---

## Veja também

- [Índice — geometria e a variável $s$](index.md)
- [Fator de estrutura](structure-factor.md) — como esses fatores se combinam ao longo de uma célula unitária.
- [3. Interação do feixe → aba Scattering factors](../../3-beam-interaction.md#scattering-factors-tab)
