# Fluorescencia

Cuando la **fotoabsorción** de rayos X expulsa un electrón de una capa interna (véase [atenuación y transporte](attenuation-transport.md)), deja una vacante en un nivel profundo. El átomo se relaja al caer un electrón externo en el hueco, y la energía liberada se emite o bien como un **fotón de rayos X característico** (fluorescencia) o bien expulsando un segundo electrón (el proceso **Auger**). La pestaña **Fluorescence** muestra una vista previa del canal de fotones característicos; solo se aplica a los rayos X y está oculta para los haces de electrones y de neutrones.

![Fluorescencia (rayos X)](../../../assets/cap-es-auto/FormBeamInteraction-xray-fluorescence.png)

---

## Líneas características

Dado que las energías de las capas están definidas con precisión, la energía del fotón emitido es la **diferencia de dos energías de enlace**,

$$E_\gamma = E_B(\text{inner shell}) - E_B(\text{outer shell}),$$

y, por lo tanto, es característica del elemento:

- **Líneas K** — vacante en la capa $K$ rellenada desde $L$ ($K\alpha$) o $M$ ($K\beta$).
- **Líneas L** — vacante en la capa $L$ rellenada desde $M$/$N$ ($L\alpha$, $L\beta$, …).

Solo aparecen las transiciones permitidas por las reglas de selección dipolar, razón por la cual el espectro consta de unas pocas líneas discretas (K$\alpha_1$, K$\alpha_2$, K$\beta_1$, L$\alpha_1$, …) en lugar de un continuo. Sus energías siguen la **ley de Moseley**; en la aproximación hidrogenoide apantallada,

$$E_{n_2\to n_1} \approx R_\infty hc\,(Z-\sigma)^2\left(\frac{1}{n_1^2} - \frac{1}{n_2^2}\right), \qquad \text{so}\qquad \sqrt{E} \propto (Z-\sigma),$$

con $\sigma$ una constante de apantallamiento. Para $K\alpha$ ($n_2{=}2\to n_1{=}1$, $\sigma\approx1$) esto se reduce a $E_{K\alpha}\approx R_\infty hc\,(Z-1)^2\left(1-\tfrac14\right)$. Esta dependencia de $Z$ monótona, gobernada por el número de electrones, es la base de la identificación elemental (EDX/WDX).

---

## Rendimiento de fluorescencia

La competencia entre la relajación radiativa y la Auger se recoge en el **rendimiento de fluorescencia**

$$\omega = \frac{\Gamma_r}{\Gamma_r + \Gamma_a},$$

la probabilidad de que una vacante dada decaiga emitiendo un fotón en lugar de un electrón Auger ($\Gamma_r$, $\Gamma_a$ son las tasas radiativa y Auger).

- Para los **elementos ligeros** domina el canal Auger, de modo que $\omega_K$ es pequeño (muy por debajo del 1 % para C, N, O) — los elementos ligeros fluorescen débilmente, razón por la cual son difíciles de detectar mediante EDX.
- Para los **elementos pesados** gana el canal radiativo y $\omega_K \to$ casi 1.

El **rendimiento Auger** complementario $a$ se queda con el resto, de modo que

$$\omega + a = 1 ,$$

y un $\omega$ pequeño significa que la mayoría de las vacantes decaen por emisión Auger. Dentro del canal radiativo, la cuota de una línea concreta $\ell$ (p. ej. $K\alpha_1$ frente a $K\beta_1$) es su **relación de ramificación**

$$p_{\ell\mid X} = \frac{\Gamma_\ell}{\sum_{\ell'\in X}\Gamma_{\ell'}},$$

la tasa radiativa relativa dentro de la capa $X$. ReciPro lista $\omega_K$ para cada elemento y la línea más intensa del espectro.

---

## Qué modela y qué no modela la vista previa

El gráfico de **líneas de emisión EDX** dibuja cada línea característica como un trazo en su energía de fotón, con una altura proporcional a

$$\text{(atomic fraction)} \times \text{(radiative rate)} \times \omega.$$

Esta es una vista previa **cualitativa** de dónde caen las líneas y de sus alturas relativas aproximadas. Omite deliberadamente los factores que un espectro EDX/XRF real y cuantitativo requiere:

- si la energía incidente está realmente **por encima del borde de absorción** necesario para crear la vacante — se dibuja una línea incluso si no puede excitarse a la energía actual;
- la **sección eficaz de excitación** (con qué eficiencia el haz incidente crea la vacante a la energía elegida);
- la **autoabsorción** de los fotones emitidos dentro de la muestra (efectos de matriz);
- la **eficiencia del detector** y la resolución.

Así pues, la vista previa sirve para la identificación de líneas y el razonamiento sobre posiciones relativas, no para la determinación cuantitativa de la composición.

---

## De la vista previa a la cuantificación

Un análisis EDX/XRF real convierte las intensidades de las líneas en concentraciones mediante una **corrección de matriz (ZAF)** — para el número atómico ($Z$), la absorción ($A$) de los fotones emitidos en su salida de la muestra y la **fluorescencia** secundaria ($F$) excitada por otras líneas — combinada con la sección eficaz de excitación y la respuesta del detector mencionadas arriba. En su forma completa, la intensidad medida de la línea $\ell$ del elemento $i$ es

$$I_\ell \;\propto\; C_i\,\Phi_0\,\sigma_{\text{ion},X,i}(E_0)\,\omega_{X,i}\,p_{\ell\mid X}\,\epsilon(E_\ell)\,A_\text{matrix}(E_0,E_\ell),$$

con $C_i$ la concentración, $\Phi_0$ el flujo incidente, $\sigma_\text{ion}$ la sección eficaz de ionización, $\omega$ el rendimiento de fluorescencia, $p_{\ell\mid X}$ la relación de ramificación, $\epsilon$ la eficiencia del detector y $A_\text{matrix}$ la corrección de absorción / fluorescencia secundaria. La vista previa de ReciPro conserva únicamente la parte $C_i\,p_{\ell\mid X}\,\omega$ (fracción atómica × tasa radiativa × rendimiento) y descarta el resto, de modo que sitúa las líneas y da sus intensidades relativas intrínsecas para que puedan reconocerse en un espectro medido.

---

## Véase también

- [Atenuación y transporte](attenuation-transport.md) — fotoabsorción, el borde que crea la vacante.
- [Factores de dispersión atómica](scattering-factor.md) — los mismos electrones ligados, vistos en la dispersión.
- [3. Interacción del haz → pestaña Fluorescence](../../3-beam-interaction.md#fluorescence-tab)
