# Formação da imagem HRTEM

A imagem HRTEM é formada a partir da função de onda na superfície de saída — os coeficientes de transmissão $T_{\mathbf g}$ obtidos do [núcleo dinâmico](calculation.md) —, fazendo-a passar pela lente objetiva. O ReciPro oferece dois modelos: a rápida aproximação **quase coerente** e o modelo mais rigoroso do **coeficiente cruzado de transmissão (TCC)**. Veja também a página de GUI do [Simulador HRTEM](../../9-hrtem-stem-simulator/1-hrtem-simulation.md).

---

## Símbolos

| Símbolo | Significado |
|--------|---------|
| $\mathbf R$ | Componente X–Y no espaço real (plano da imagem) |
| $\mathbf K$ | Componente X–Y do vetor de onda incidente |
| $\mathbf G, \mathbf H$ | Componentes X–Y de vetores da rede recíproca |
| $\mathbf u$ | frequência espacial (p. ex. $\mathbf K+\mathbf G$) |
| $\chi(\mathbf u)$ | função de aberração da lente |
| $A(\mathbf u)$ | função da abertura objetiva |
| $\Delta f$ | valor de desfocagem |
| $C_s$ | coeficiente de aberração esférica |
| $C_c$ | coeficiente de aberração cromática |
| $\beta$ | semiângulo de iluminação (tamanho finito da fonte) |
| $\Delta E$ | largura $1/e$ das flutuações de energia do elétron |
| $\Delta_0$ | largura $1/e$ da dispersão de desfocagem (gaussiana), $\Delta_0 = C_c\,\Delta E / E$ |

---

## Função de aberração da lente e abertura

$$\chi(\mathbf u) = \pi\lambda\Delta f\, u^2 + \tfrac{1}{2}\pi\lambda^3 C_s\, u^4 = \pi\lambda u^2\!\left(\Delta f + \tfrac{1}{2}\lambda^2 C_s u^2\right)$$

$$A(\mathbf u) = \begin{cases} 1 & (\mathbf u\ \text{inside the objective aperture})\\[2pt] 0 & (\mathbf u\ \text{outside the objective aperture})\end{cases}$$

---

## Modelo quase coerente

Uma aproximação rápida: cada feixe difratado é modulado pela transferência da lente e amortecido por envelopes de coerência, e em seguida somado coerentemente.

$$I(\mathbf R) = |\psi(\mathbf R)|^2$$

$$\psi(\mathbf R) = \sum_{\mathbf g} T_{\mathbf g}\,\exp\!\left[2\pi i(\mathbf K+\mathbf G)\cdot\mathbf R\right]\exp\!\left[-i\chi(\mathbf K+\mathbf G)\right]A(\mathbf K+\mathbf G)\,E_c(\mathbf K+\mathbf G)\,E_s(\mathbf K+\mathbf G)$$

com os **envelopes de coerência temporal** e **espacial**

$$E_c(\mathbf u) = \exp\!\left[-\tfrac{1}{2}\left(\pi\lambda\Delta_0\, u^2\right)^2\right], \qquad E_s(\mathbf u) = \exp\!\left[-\pi^2\beta^2 u^2\!\left(\Delta f + \lambda^2 C_s u^2\right)^2\right]$$

---

## Modelo do coeficiente cruzado de transmissão (TCC)

O tratamento rigoroso da coerência parcial: cada par de feixes $(\mathbf g, \mathbf h)$ interfere por meio do coeficiente cruzado de transmissão.

$$I(\mathbf R) = \sum_{\mathbf g}\sum_{\mathbf h} T_{\mathbf g}\,T_{\mathbf h}^{*}\,\exp\!\left[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R\right]\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)$$

$$\mathrm{TCC}(\mathbf u, \mathbf u') = A(\mathbf u)\,A(\mathbf u')\,\exp\!\left[-i\{\chi(\mathbf u)-\chi(\mathbf u')\}\right]E_c(\mathbf u, \mathbf u')\,E_s(\mathbf u, \mathbf u')$$

com os envelopes de coerência **mistos**

$$E_c(\mathbf u, \mathbf u') = \exp\!\left[-\tfrac{1}{2}\left(\pi\lambda\Delta_0\right)^2\!\left(u^2 - u'^2\right)^2\right]$$

$$E_s(\mathbf u, \mathbf u') = \exp\!\left[-\pi^2\beta^2\left\{\Delta f(\mathbf u-\mathbf u') + \lambda^2 C_s\!\left(u^2\mathbf u - u'^2\mathbf u'\right)\right\}^2\right]$$

No limite $\mathbf u' \to \mathbf u$ o TCC se reduz aos envelopes quase coerentes acima.

---

## Redução do custo do modelo TCC

A soma dupla do modelo TCC avalia $\mathrm{TCC}$ uma vez por par de feixes, sendo portanto custosa. Como a intensidade da imagem $I(\mathbf R)$ é real, o custo pode ser reduzido aproximadamente à metade.

Primeiro, feixes fora da abertura objetiva ($A(\mathbf K+\mathbf G)=0$) não contribuem, de modo que basta somar **apenas sobre os feixes dentro da abertura ($A=1$)**.

Em seguida, o TCC é hermitiano,

$$\mathrm{TCC}(\mathbf u', \mathbf u) = \mathrm{TCC}(\mathbf u, \mathbf u')^{*}$$

($A$ é real; $E_c, E_s$ são funções reais, invariantes sob $\mathbf u\leftrightarrow\mathbf u'$; o termo de fase $\exp[-i\{\chi(\mathbf u)-\chi(\mathbf u')\}]$ é complexo-conjugado). Junto com $\exp[2\pi i(\mathbf H-\mathbf G)\cdot\mathbf R]=\bigl(\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\bigr)^{*}$ e $T_{\mathbf h}T_{\mathbf g}^{*}=\bigl(T_{\mathbf g}T_{\mathbf h}^{*}\bigr)^{*}$, os termos $(\mathbf g,\mathbf h)$ e $(\mathbf h,\mathbf g)$ são complexos conjugados entre si, de modo que sua soma é igual ao dobro da parte real:

$$F(\mathbf g,\mathbf h) + F(\mathbf h,\mathbf g) = 2\,\mathrm{Re}\{F(\mathbf g,\mathbf h)\}, \qquad F(\mathbf g,\mathbf h) \equiv T_{\mathbf g}T_{\mathbf h}^{*}\,\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\,\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)$$

A soma dupla se reduz, portanto, à diagonal mais o triângulo superior (um lado, uma vez atribuída uma ordenação arbitrária aos feixes), reduzindo à metade o número de avaliações de $\mathrm{TCC}$:

$$I(\mathbf R) = \sum_{\mathbf g} |T_{\mathbf g}|^2\,A(\mathbf K+\mathbf G)^2 \;+\; 2\sum_{\mathbf g}\sum_{\mathbf h > \mathbf g} \mathrm{Re}\!\left\{ T_{\mathbf g}T_{\mathbf h}^{*}\,\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\,\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)\right\}$$

Para o termo diagonal vale $\mathrm{TCC}(\mathbf u,\mathbf u)=A(\mathbf u)^2$, isto é, $|T_{\mathbf g}|^2$ dentro da abertura.

Além disso, o fator de fase $\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]$ assume o mesmo valor muitas vezes dentro dessa soma. Armazenar e reutilizar esses valores acelera ainda mais o cálculo.

---

## Veja também

- [Cálculo dinâmico (núcleo comum)](calculation.md) — o núcleo de ondas de Bloch compartilhado e os coeficientes de transmissão $T_{\mathbf g}$
- [Apêndice A3. Difração dinâmica pelo método de ondas de Bloch](index.md)
- [9.1. Simulação HRTEM](../../9-hrtem-stem-simulator/1-hrtem-simulation.md)
