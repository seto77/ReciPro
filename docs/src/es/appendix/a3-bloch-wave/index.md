# Apéndice A3. Difracción dinámica por el método de ondas de Bloch

Este apéndice ofrece una visión general de la teoría de la difracción dinámica de electrones empleada por los simuladores **Simulador de difracción**, **CBED** y **HRTEM/STEM** de ReciPro. ReciPro sigue la formulación de **Bethe / ondas de Bloch**. El cálculo paso a paso (potencial óptico, coeficientes de transmisión, intensidades) se describe en [Cálculo dinámico (núcleo común)](calculation.md).

---

## La ecuación de onda en un cristal

Un electrón rápido que se desplaza a través del potencial electrostático periódico de un cristal obedece la ecuación de Schrödinger (de alta energía, estacionaria), que puede escribirse como

$$\nabla^2 \Psi(\mathbf{r}) + 4\pi^2\left\{\, k_{vac}^2 + \sum_{\mathbf g} U_{\mathbf g}\, e^{2\pi i\,\mathbf g\cdot\mathbf r} \right\}\Psi(\mathbf{r}) = 0$$

- $k_{vac}$ : número de onda del electrón en el vacío.
- $U_{\mathbf g}$ : componente de Fourier del potencial del cristal para el vector de la red recíproca $\mathbf g$. Como el potencial es periódico con la red, se escribe como una serie de Fourier sobre la red recíproca.

---

## Teorema de Bloch

Dado que el potencial posee la periodicidad de la red cristalina, las soluciones son **ondas de Bloch**:

$$\Psi(\mathbf{r}) = b\!\left(\mathbf{k}^{(j)}, \mathbf{r}\right) = u(\mathbf{r})\exp\!\left(2\pi i\,\mathbf{k}^{(j)}\cdot\mathbf{r}\right)$$

- $u(\mathbf r)$ : una función con la misma periodicidad que la red cristalina, de modo que ella misma puede desarrollarse sobre la red recíproca: $u(\mathbf r)=\sum_{\mathbf g} C_{\mathbf g}^{(j)}\exp(2\pi i\,\mathbf g\cdot\mathbf r)$.
- $\mathbf{k}^{(j)}$ : el $j$-ésimo vector de onda de Bloch.
- $C_{\mathbf g}^{(j)}$ : la amplitud (componente del vector propio) del haz $\mathbf g$ en la $j$-ésima onda de Bloch.

---

## Ecuación dinámica de Bethe

Sustituir el desarrollo en ondas de Bloch en la ecuación de onda da lugar a la **ecuación dinámica de Bethe** — una ecuación acoplada para cada haz $\mathbf g$:

$$\left[\,k^2 - \left(\mathbf{k}^{(j)} + \mathbf{g}\right)^2 + i\,U'_{g,g}\right]C_{\mathbf g}^{(j)} + \sum_{h \neq g}\left(U^C_{g-h} + i\,U'_{g,h}\right)C_{\mathbf h}^{(j)} = 0$$

- $U^C_{\mathbf g}$ : potencial del cristal para la dispersión **elástica**.
- $U'_{\mathbf g}$ : potencial imaginario (de **absorción**), que tiene en cuenta la **dispersión térmica difusa** (TDS). Cómo entran este y el factor de Debye–Waller se detalla en [el núcleo de cálculo](calculation.md).

---

## Definiciones geométricas (esfera de Ewald)

Los vectores y escalares que aparecen arriba están definidos sobre la esfera de Ewald:

![Definiciones de los vectores y escalares usados en el cálculo de ondas de Bloch](../../../assets/references/Bloch.png){width=500px}

- $\hat{\mathbf n}$ : vector unitario normal a la superficie del cristal.
- $\mathbf k$ : vector de onda incidente (su extremo se sitúa sobre la esfera de Ewald); $\mathbf k_{vac}$ es el vector de onda en el vacío.
- $\mathbf g$ : vector de la red recíproca; $\mathbf k + \mathbf g$ apunta al punto de la red recíproca.
- $\mathbf k^{(j)}$ : el $j$-ésimo vector de onda de Bloch. Todos los vectores de onda de Bloch comparten la misma componente tangencial (continuidad a través de la superficie) y difieren solo a lo largo de $\hat{\mathbf n}$: $\mathbf k^{(j)} = \mathbf k + \gamma^{(j)}\hat{\mathbf n}$.
- $\gamma^{(j)}$ : el $j$-ésimo valor propio (la componente de $\mathbf k^{(j)}$ a lo largo de $\hat{\mathbf n}$, medida desde $\mathbf k$).

A partir de la geometría,

$$P_g = 2\,\hat{\mathbf n}\cdot(\mathbf k + \mathbf g), \qquad Q_g = |\mathbf k|^2 - |\mathbf k + \mathbf g|^2 = -\,\mathbf g\cdot(2\mathbf k + \mathbf g)$$

y el **error de excitación** $S_g$ (la desviación del punto de la red recíproca respecto de la esfera de Ewald) junto con la **función de evaluación** $R$ utilizada para ordenar las reflexiones son

$$S_g = \frac{\sqrt{P_g^{\,2} + 4Q_g}\; -\; P_g}{2}, \qquad R = |\mathbf g|\,Q_g^{\,2}$$

---

## Reducción a un problema de valores propios

Escribiendo $\mathbf{k}^{(j)} = \mathbf{k} + \gamma^{(j)}\hat{\mathbf n}$ y usando $k^2-(\mathbf k+\mathbf g)^2 = Q_g$ junto con la linealización $(\mathbf k^{(j)}+\mathbf g)^2 \approx (\mathbf k+\mathbf g)^2 + \gamma^{(j)} P_g$, la ecuación de Bethe se convierte (tras dividir por $P_g$) en un **problema matricial de valores propios** estándar:

$$\mathbf{A}\,\mathbf{C} = \mathbf{C}\,\boldsymbol{\Lambda}, \qquad
A_{gh} = \frac{U^C_{\,g-h} + i\,U'_{g,h}}{P_g}\;\;(g\neq h), \qquad
A_{gg} = \frac{Q_g + i\,U'_{g,g}}{P_g}$$

- Las columnas de $\mathbf{C}$ son los vectores propios $C^{(j)}_*$ (las amplitudes de las ondas de Bloch).
- $\boldsymbol{\Lambda}=\mathrm{diag}\!\left(\lambda^{(1)}, \lambda^{(2)}, \dots\right)$ contiene los valores propios $\lambda^{(j)} = \gamma^{(j)}$.

Escrito explícitamente — ordenando los haces como el haz transmitido $0$, luego $g$, $h$, $\dots$ — esto es

$$
\begin{aligned}
&\begin{pmatrix}
(Q_0 + i\,U'_{0,0})/P_0 & (U^C_{-g} + i\,U'_{0,g})/P_0 & (U^C_{-h} + i\,U'_{0,h})/P_0 & \cdots \\
(U^C_{g} + i\,U'_{g,0})/P_g & (Q_g + i\,U'_{g,g})/P_g & (U^C_{g-h} + i\,U'_{g,h})/P_g & \cdots \\
(U^C_{h} + i\,U'_{h,0})/P_h & (U^C_{h-g} + i\,U'_{h,g})/P_h & (Q_h + i\,U'_{h,h})/P_h & \cdots \\
\vdots & \vdots & \vdots & \ddots
\end{pmatrix}
\begin{pmatrix}
C^{(1)}_0 & C^{(2)}_0 & C^{(3)}_0 & \cdots \\
C^{(1)}_g & C^{(2)}_g & C^{(3)}_g & \cdots \\
C^{(1)}_h & C^{(2)}_h & C^{(3)}_h & \cdots \\
\vdots & \vdots & \vdots & \ddots
\end{pmatrix} \\[1.2ex]
&\qquad=
\begin{pmatrix}
C^{(1)}_0 & C^{(2)}_0 & C^{(3)}_0 & \cdots \\
C^{(1)}_g & C^{(2)}_g & C^{(3)}_g & \cdots \\
C^{(1)}_h & C^{(2)}_h & C^{(3)}_h & \cdots \\
\vdots & \vdots & \vdots & \ddots
\end{pmatrix}
\begin{pmatrix}
\lambda^{(1)} & 0 & 0 & \cdots \\
0 & \lambda^{(2)} & 0 & \cdots \\
0 & 0 & \lambda^{(3)} & \cdots \\
\vdots & \vdots & \vdots & \ddots
\end{pmatrix}
\end{aligned}
$$

La diagonalización de $\mathbf{A}$ proporciona **todos** los vectores de onda de Bloch y las amplitudes de una sola vez. Las amplitudes de los haces difractados — y, por tanto, las intensidades — se obtienen entonces a partir de las condiciones de contorno en las superficies de entrada y de salida y del espesor de la muestra. Esos pasos, el potencial óptico (complejo), el factor de Debye–Waller y los coeficientes de transmisión $T_{\mathbf g}$ se describen en [Cálculo dinámico (núcleo común)](calculation.md).

> **Nota:** Los valores de $V_{\mathbf g}$ mostrados en la tabla **Details** del simulador de difracción son los valores en bruto *antes* de aplicar el factor de corrección relativista.

---

## Véase también

- [7. Simulador de difracción](../../7-diffraction-simulator/index.md) — patrones de difracción dinámica
- [9. Simulador HRTEM/STEM](../../9-hrtem-stem-simulator/index.md)
- [Apéndice A1. Sistemas de coordenadas](../a1-coordinate-system/1-orientation.md)
- [Cálculo dinámico (núcleo común)](calculation.md)
