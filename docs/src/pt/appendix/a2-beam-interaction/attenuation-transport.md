# Atenuação & Transporte

Os fatores de espalhamento descrevem um único evento de espalhamento; esta página trata do que acontece com o feixe **como um todo** à medida que ele atravessa o sólido — quão rapidamente ele é removido, quão profundamente ele penetra e (no caso dos elétrons) como ele é desacelerado. A física relevante é completamente diferente para os três feixes, razão pela qual a aba **Attenuations & Transport** altera de forma tão drástica seus gráficos e tabelas conforme a radiação.

=== "X-ray"
    ![Attenuations & Transport — X-ray](../../../assets/cap-pt-auto/FormBeamInteraction-xray-attenuations.png)

=== "Electron"
    ![Attenuations & Transport — electron](../../../assets/cap-pt-auto/FormBeamInteraction-electron-attenuations.png)

=== "Neutron"
    ![Attenuations & Transport — neutron](../../../assets/cap-pt-auto/FormBeamInteraction-neutron-attenuations.png)

---

## Raios X — absorção e refração

### Atenuação de Beer–Lambert

Um feixe de raios X monocromático é removido exponencialmente com o comprimento do caminho:

$$I(t) = I_0\, e^{-\mu t}, \qquad \mu = \rho\,(\mu/\rho).$$

- $\mu/\rho$ : o **coeficiente de atenuação mássico** (cm²/g) — a grandeza tabelada, independente da densidade.
- $\mu$ : o **coeficiente de atenuação linear** (cm⁻¹) na densidade real $\rho$ do material.
- $1/\mu$ : o **comprimento de atenuação** (a intensidade cai para $1/e$).
- $\text{HVL} = \ln 2/\mu$ : a **camada semirredutora**.
- $T = e^{-\mu t}$ : a transmissão para uma amostra de espessura $t$.

### Do que se compõe $\mu/\rho$

A atenuação mássica total é a soma de três processos, representados separadamente na aba:

$$\left(\frac{\mu}{\rho}\right)_\text{total} = \left(\frac{\tau}{\rho}\right)_\text{photo} + \left(\frac{\mu}{\rho}\right)_\text{Rayleigh} + \left(\frac{\mu}{\rho}\right)_\text{Compton}.$$

Para um composto, a atenuação mássica é a soma ponderada pela massa dos valores elementares, enquanto o coeficiente linear soma diretamente as seções de choque atômicas:

$$\left(\frac{\mu}{\rho}\right)_\text{mix} = \sum_i w_i\left(\frac{\mu}{\rho}\right)_i, \qquad \mu = \sum_i n_i\,\sigma_i,$$

com $w_i$ as frações mássicas e $n_i$ as densidades numéricas. As três componentes são:

- **Fotoabsorção** $\tau$ — um fóton é absorvido e ejeta um elétron ligado. Ela domina em baixa energia, caindo aproximadamente como $\tau/\rho \propto Z^{3\!-\!4}/E^{3}$ entre as bordas. Este é o termo que ejeta o elétron de camada interna cuja relaxação produz [fluorescência](fluorescence.md).
- **Espalhamento Rayleigh (coerente)** — espalhamento elástico por elétrons ligados, relacionado ao fator de forma coerente $F(q)$.
- **Espalhamento Compton (incoerente)** — espalhamento inelástico por elétrons fracamente ligados, relacionado à função incoerente $S(q)$; sua importância relativa cresce em alta energia. O fóton espalhado tem seu comprimento de onda deslocado em

$$\Delta\lambda = \lambda' - \lambda = \frac{h}{m_e c}\,(1-\cos\varphi),$$

  de modo que um evento Compton remove o fóton do feixe monocromático (uma perda inelástica).

As **bordas de absorção** são os aumentos abruptos de $\tau$ quando a energia do fóton ultrapassa a energia de ligação de uma camada ($K$, $L_3$, …), abrindo um novo canal de ionização. A **razão de salto** é o fator pelo qual $\mu/\rho$ aumenta ao cruzar a borda; o ReciPro lista as energias e os saltos das bordas $K$ e $L_3$. O **coeficiente mássico de absorção de energia** $\mu_\text{en}/\rho$ é a parte de $\mu/\rho$ que deposita energia localmente (excluindo a energia carregada pelos fótons espalhados e fluorescentes).

### Refração, ângulo crítico e SLD

O índice de refração de raios X de um sólido é **ligeiramente menor que 1**, escrito como

$$n = 1 - \delta + i\beta, \qquad \beta = \frac{\mu_\text{abs}\lambda}{4\pi} = \frac{r_e\lambda^2}{2\pi}\sum_i n_i\,f''_i, \qquad \delta \simeq \frac{r_e\lambda^2}{2\pi}\sum_i n_i\,(Z_i+f'_i),$$

onde $n_i$ é a densidade numérica do elemento $i$ e $r_e$ o raio clássico do elétron. Aqui $\mu_\text{abs}$ é a parte absortiva da atenuação (vinculada a $f''$); ela não precisa ser igual ao $\mu$ total acima, que também contém o espalhamento Rayleigh e Compton. Como $n<1$, os raios X sofrem **reflexão externa total** abaixo de um pequeno **ângulo crítico** rasante

$$\theta_c \simeq \sqrt{2\delta}.$$

Isto decorre da geometria de refração: para um ângulo rasante $\alpha$, o vetor de onda vertical dentro do sólido é $k_z^2 \simeq k^2(\alpha^2 - 2\delta)$, que atinge zero em $\alpha = \alpha_c = \sqrt{2\delta}$; abaixo disso a onda não consegue se propagar para dentro do material e é totalmente refletida. A parte real da **densidade de comprimento de espalhamento**, $\text{SLD} = r_e\sum_i n_i (Z_i + f'_i)$, fixa $\delta$ e é o análogo de raios X da SLD de nêutrons usada em refletometria. O ReciPro reporta $\delta$, $\beta$, $\theta_c$ e a SLD de raios X na tabela escalar.

---

## Elétrons — espalhamento, desaceleração e alcance

Um elétron rápido em um sólido tanto **espalha** (mudando de direção) quanto **perde energia** continuamente, de modo que seu transporte necessita de mais de uma escala de comprimento.

### Espalhamento elástico e livre caminho médio

A seção de choque elástica $\sigma_\text{el}$ mede com que facilidade um único átomo desvia o elétron. O ReciPro usa as seções de choque **NIST Mott** (uma solução por ondas parciais da equação relativística de Dirac no potencial atômico blindado), válidas aproximadamente no intervalo **50 eV – 36.4 keV**; fora desse intervalo, ou para elementos não presentes na tabela, ele recorre à aproximação de **Rutherford blindado**. As duas não precisam se conectar de forma perfeitamente suave na fronteira. A seção de choque total é a integral angular da diferencial,

$$\sigma_\text{el} = 2\pi\int_0^\pi \frac{d\sigma}{d\Omega}\,\sin\Theta\,d\Theta, \qquad \frac{d\sigma}{d\Omega} \propto \frac{Z^2}{E^2}\,\frac{1}{\big[\sin^2(\Theta/2)+\eta\big]^2},$$

onde o parâmetro de blindagem $\eta$ arredonda a divergência para a frente da seção de choque de Rutherford pura; o tratamento de Mott inclui adicionalmente os efeitos de spin e relativísticos que o Rutherford blindado omite. A partir da seção de choque,

$$\Sigma_\text{el} = \sum_i n_i\,\sigma_{\text{el},i}, \qquad \lambda_\text{el} = \frac{1}{\Sigma_\text{el}},$$

fornecem o coeficiente de espalhamento macroscópico e o **livre caminho médio elástico** — a distância média entre eventos elásticos.

### Poder de freamento e perdas inelásticas

A energia é perdida principalmente em excitações eletrônicas (ionização, plasmons). O **poder de freamento** é definido como uma grandeza positiva,

$$S(E) = -\frac{dE}{ds} > 0,$$

onde aqui $s$ é o **comprimento do caminho** ao longo da trajetória (a variável da curva *|dE/ds|* da aba), não a variável de espalhamento $\sin\theta/\lambda$ usada em outras partes deste apêndice. O gradiente de energia $dE/ds$ é negativo, de modo que a aba traça $S$ para cima. Em energias de keV, ele segue, conceitualmente, a forma de **Bethe**

$$S(E) \;\propto\; \frac{Z\rho}{A}\,\frac{1}{E}\,\ln\!\frac{E}{J},$$

com $J$ a **energia média de excitação** do sólido. Este esboço não relativístico mostra apenas o escalonamento; o ReciPro avalia uma forma corrigida/empírica (do tipo Joy–Luo) que permanece bem-comportada em baixa energia. A **energia de plasmon** $E_p$ na tabela escalar é uma caracterização relacionada, mas distinta, das mesmas excitações eletrônicas. O **livre caminho médio inelástico** (IMFP) é a distância média correspondente entre colisões com perda de energia; o ReciPro pode avaliá-la a partir da fórmula preditiva **TPP-2M**,

$$\lambda_\text{in}(E) = \frac{E}{E_p^2\left[\beta_\text{T}\ln(\gamma_\text{T} E) - C/E + D/E^2\right]},$$

com $E$ em eV, $\lambda_\text{in}$ em Å, e os parâmetros $\beta_\text{T},\gamma_\text{T},C,D$ construídos a partir de $E_p$, da densidade, do gap de banda e do número de elétrons de valência.

### Dois tipos de alcance

- **Alcance CSDA** — a aproximação de desaceleração contínua (continuous-slowing-down approximation) integra o poder de freamento para fornecer o comprimento total do caminho percorrido antes de o elétron parar:

$$R_\text{CSDA} = \int_{E_\text{cut}}^{E_0} \frac{dE}{S(E)}.$$

(Na prática, a integral desce até um valor de corte de baixa energia $E_\text{cut}$, abaixo do qual o esboço de Bethe acima não mais se aplica.)

- **Alcance de Kanaya–Okayama** — uma estimativa empírica amplamente usada da **profundidade de penetração** (não do comprimento do caminho), levando em conta a trajetória tortuosa e espalhada:

$$R_\text{KO}\,[\mu\text{m}] = 0.0276\,\frac{A\,E_0^{1.67}}{\rho\,Z^{0.89}}, \qquad (E_0\ \text{in keV}).$$

Os dois respondem a perguntas diferentes — distância total percorrida vs. quão fundo no sólido o elétron chega — de modo que diferem em valor, e o ReciPro reporta ambos. Esses alcances definem o volume de interação por trás das simulações de [trajetórias eletrônicas](../../8-electron-trajectory.md) e EBSD.

---

## Nêutrons — seção de choque macroscópica e a lei 1/v

Para nêutrons não há curva de atenuação dependente da energia; a interação é fixada por **seções de choque nucleares**. O feixe é atenuado pela seção de choque total macroscópica, ela própria a soma das partes coerente, incoerente e de absorção:

$$\Sigma_\text{total} = \sum_i n_i\,\sigma_{\text{total},i}, \qquad \sigma_\text{total} = \sigma_\text{coh} + \sigma_\text{inc} + \sigma_\text{abs}(\lambda), \qquad T = e^{-\Sigma_\text{total} t},$$

com comprimento de atenuação $1/\Sigma_\text{total}$. A parte de absorção depende da velocidade $v$ do nêutron (portanto do comprimento de onda): para a maioria dos nuclídeos, o tempo passado próximo ao núcleo escala como $1/v$, resultando na **lei 1/v**

$$\sigma_\text{abs}(\lambda) = \sigma_\text{abs}(\lambda_0)\,\frac{\lambda}{\lambda_0}, \qquad \lambda_0 = 1.798\ \text{Å}\ (\text{thermal}, 2200\ \text{m/s}).$$

Alguns absorvedores fortes (Cd, Sm, Eu, Gd) têm **ressonâncias** de baixa energia que violam o escalonamento 1/v simples; o ReciPro sinaliza esses nuclídeos. A **densidade de comprimento de espalhamento** coerente, $\text{SLD} = \sum_i n_i\, b_{\text{coh},i}$, é o análogo de nêutrons da SLD de raios X acima.

---

## Penetração em um relance

Os três feixes sondam profundidades muito diferentes — a razão prática pela qual respondem a perguntas diferentes:

| Feixe | Amostra típica | Penetração (ordem de grandeza) | Determinada por |
|---|---|---|---|
| Raios X (≈8 keV) | pó / monocristal | 10–100 µm | $\mu = \rho(\mu/\rho)$ |
| Elétron (≈200 keV) | folha TEM | 10–100 nm (útil) | MFP elástico + perda inelástica |
| Nêutron (térmico) | volume, tamanho de cm | 1–10 cm | $\Sigma_\text{total}$ |

As mesmas escalas de comprimento explicam por que os elétrons exigem amostras ultrafinas e teoria dinâmica, enquanto os nêutrons enxergam uma amostra volumétrica inteira sob cinemática de espalhamento simples.

---

## Veja também

- [Fatores de espalhamento atômico](scattering-factor.md) — a separação $F(q)$/$S(q)$ por trás de Rayleigh/Compton, e as seções de choque de Mott.
- [Fluorescência](fluorescence.md) — a relaxação que se segue à fotoabsorção de raios X.
- [3. Interação do feixe](../../3-beam-interaction.md) — a aba *Attenuations & Transport*.
- [8. Trajetórias eletrônicas](../../8-electron-trajectory.md) · [12. Simulação EBSD](../../12-ebsd-simulation.md) — onde os alcances dos elétrons são usados.
