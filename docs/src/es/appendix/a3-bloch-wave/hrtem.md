# Formación de la imagen HRTEM

La imagen HRTEM se forma a partir de la función de onda en la superficie de salida —los coeficientes de transmisión $T_{\mathbf g}$ obtenidos del [núcleo dinámico](calculation.md)— haciéndola pasar a través de la lente objetivo. ReciPro ofrece dos modelos: la rápida aproximación **cuasi-coherente** y el modelo más riguroso del **coeficiente de transmisión cruzada (TCC)**. Véase también la página de la interfaz del [simulador HRTEM](../../9-hrtem-stem-simulator/1-hrtem-simulation.md).

---

## Símbolos

| Símbolo | Significado |
|--------|---------|
| $\mathbf R$ | componente X–Y en el espacio real (plano de la imagen) |
| $\mathbf K$ | componente X–Y del vector de onda incidente |
| $\mathbf G, \mathbf H$ | componentes X–Y de los vectores de la red recíproca |
| $\mathbf u$ | frecuencia espacial (p. ej. $\mathbf K+\mathbf G$) |
| $\chi(\mathbf u)$ | función de aberración de la lente |
| $A(\mathbf u)$ | función del diafragma objetivo |
| $\Delta f$ | valor de desenfoque |
| $C_s$ | coeficiente de aberración esférica |
| $C_c$ | coeficiente de aberración cromática |
| $\beta$ | semiángulo de iluminación (tamaño finito de la fuente) |
| $\Delta E$ | anchura $1/e$ de las fluctuaciones de energía del electrón |
| $\Delta_0$ | anchura $1/e$ de la dispersión del desenfoque (gaussiana), $\Delta_0 = C_c\,\Delta E / E$ |

---

## Función de aberración de la lente y diafragma

$$\chi(\mathbf u) = \pi\lambda\Delta f\, u^2 + \tfrac{1}{2}\pi\lambda^3 C_s\, u^4 = \pi\lambda u^2\!\left(\Delta f + \tfrac{1}{2}\lambda^2 C_s u^2\right)$$

$$A(\mathbf u) = \begin{cases} 1 & (\mathbf u\ \text{inside the objective aperture})\\[2pt] 0 & (\mathbf u\ \text{outside the objective aperture})\end{cases}$$

---

## Modelo cuasi-coherente

Una aproximación rápida: cada haz difractado se modula por la transferencia de la lente y se amortigua por las envolventes de coherencia, y luego se suma coherentemente.

$$I(\mathbf R) = |\psi(\mathbf R)|^2$$

$$\psi(\mathbf R) = \sum_{\mathbf g} T_{\mathbf g}\,\exp\!\left[2\pi i(\mathbf K+\mathbf G)\cdot\mathbf R\right]\exp\!\left[-i\chi(\mathbf K+\mathbf G)\right]A(\mathbf K+\mathbf G)\,E_c(\mathbf K+\mathbf G)\,E_s(\mathbf K+\mathbf G)$$

con las **envolventes de coherencia temporal** y **espacial**

$$E_c(\mathbf u) = \exp\!\left[-\tfrac{1}{2}\left(\pi\lambda\Delta_0\, u^2\right)^2\right], \qquad E_s(\mathbf u) = \exp\!\left[-\pi^2\beta^2 u^2\!\left(\Delta f + \lambda^2 C_s u^2\right)^2\right]$$

---

## Modelo del coeficiente de transmisión cruzada (TCC)

El tratamiento riguroso de la coherencia parcial: cada par de haces $(\mathbf g, \mathbf h)$ interfiere a través del coeficiente de transmisión cruzada.

$$I(\mathbf R) = \sum_{\mathbf g}\sum_{\mathbf h} T_{\mathbf g}\,T_{\mathbf h}^{*}\,\exp\!\left[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R\right]\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)$$

$$\mathrm{TCC}(\mathbf u, \mathbf u') = A(\mathbf u)\,A(\mathbf u')\,\exp\!\left[-i\{\chi(\mathbf u)-\chi(\mathbf u')\}\right]E_c(\mathbf u, \mathbf u')\,E_s(\mathbf u, \mathbf u')$$

con las envolventes de coherencia **mixtas**

$$E_c(\mathbf u, \mathbf u') = \exp\!\left[-\tfrac{1}{2}\left(\pi\lambda\Delta_0\right)^2\!\left(u^2 - u'^2\right)^2\right]$$

$$E_s(\mathbf u, \mathbf u') = \exp\!\left[-\pi^2\beta^2\left\{\Delta f(\mathbf u-\mathbf u') + \lambda^2 C_s\!\left(u^2\mathbf u - u'^2\mathbf u'\right)\right\}^2\right]$$

En el límite $\mathbf u' \to \mathbf u$ el TCC se reduce a las envolventes cuasi-coherentes anteriores.

---

## Reducción del coste del modelo TCC

La suma doble del modelo TCC evalúa $\mathrm{TCC}$ una vez por cada par de haces, por lo que resulta costosa. Como la intensidad de la imagen $I(\mathbf R)$ es real, el coste se puede reducir aproximadamente a la mitad.

En primer lugar, los haces situados fuera del diafragma objetivo ($A(\mathbf K+\mathbf G)=0$) no contribuyen, de modo que basta con sumar **solo sobre los haces situados dentro del diafragma ($A=1$)**.

En segundo lugar, el TCC es hermítico,

$$\mathrm{TCC}(\mathbf u', \mathbf u) = \mathrm{TCC}(\mathbf u, \mathbf u')^{*}$$

($A$ es real; $E_c, E_s$ son funciones reales, invariantes bajo $\mathbf u\leftrightarrow\mathbf u'$; el término de fase $\exp[-i\{\chi(\mathbf u)-\chi(\mathbf u')\}]$ se conjuga complejamente). Junto con $\exp[2\pi i(\mathbf H-\mathbf G)\cdot\mathbf R]=\bigl(\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\bigr)^{*}$ y $T_{\mathbf h}T_{\mathbf g}^{*}=\bigl(T_{\mathbf g}T_{\mathbf h}^{*}\bigr)^{*}$, los términos $(\mathbf g,\mathbf h)$ y $(\mathbf h,\mathbf g)$ son complejos conjugados entre sí, de modo que su suma equivale al doble de la parte real:

$$F(\mathbf g,\mathbf h) + F(\mathbf h,\mathbf g) = 2\,\mathrm{Re}\{F(\mathbf g,\mathbf h)\}, \qquad F(\mathbf g,\mathbf h) \equiv T_{\mathbf g}T_{\mathbf h}^{*}\,\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\,\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)$$

Por tanto, la suma doble se reduce a la diagonal más el triángulo superior (un solo lado, una vez asignado un orden arbitrario a los haces), reduciendo a la mitad el número de evaluaciones de $\mathrm{TCC}$:

$$I(\mathbf R) = \sum_{\mathbf g} |T_{\mathbf g}|^2\,A(\mathbf K+\mathbf G)^2 \;+\; 2\sum_{\mathbf g}\sum_{\mathbf h > \mathbf g} \mathrm{Re}\!\left\{ T_{\mathbf g}T_{\mathbf h}^{*}\,\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]\,\mathrm{TCC}(\mathbf K+\mathbf G,\ \mathbf K+\mathbf H)\right\}$$

Para el término diagonal se cumple $\mathrm{TCC}(\mathbf u,\mathbf u)=A(\mathbf u)^2$, es decir, $|T_{\mathbf g}|^2$ dentro del diafragma.

Además, el factor de fase $\exp[2\pi i(\mathbf G-\mathbf H)\cdot\mathbf R]$ toma muchas veces el mismo valor dentro de esta suma. Almacenar y reutilizar estos valores acelera aún más el cálculo.

---

## Véase también

- [Cálculo dinámico (núcleo común)](calculation.md) — el núcleo común de ondas de Bloch y los coeficientes de transmisión $T_{\mathbf g}$
- [Apéndice A3. Difracción dinámica por el método de ondas de Bloch](index.md)
- [9.1. Simulación HRTEM](../../9-hrtem-stem-simulator/1-hrtem-simulation.md)
