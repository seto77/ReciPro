# Cálculo de EBSD

EBSD (difracción de electrones retrodispersados) utiliza el mismo núcleo de Bethe/ondas de Bloch que CBED y STEM, pero el problema se plantea de otra manera. CBED y STEM son **problemas de haz incidente**: una onda electrónica entra desde el exterior de la muestra y se calcula la onda de salida. EBSD es un **problema de dirección de salida**: los electrones que han sufrido dispersión inelástica en el interior de la muestra emergen como electrones retrodispersados, y el cálculo pregunta cuánta intensidad sale en cada dirección externa.

ReciPro convierte este problema de dirección de salida en un problema ordinario de haz incidente mediante el teorema de reciprocidad. Primero calcula un **master pattern** en el espacio de direcciones y luego combina ese master pattern con los pesos de Monte Carlo para profundidad / energía / dirección y la geometría del detector para formar el patrón del detector.

---

## Reformulación con el teorema de reciprocidad

Si la amplitud desde un punto fuente interno $\mathbf r_n$ hacia una dirección externa $\widehat{\mathbf s}$ se calculara directamente, sería necesario un problema de dispersión separado para cada punto fuente. Eso no es práctico.

El teorema de reciprocidad reescribe el problema de la siguiente manera: la amplitud de que un electrón que parte de $\mathbf r_n$ aparezca en la dirección de campo lejano $\widehat{\mathbf s}$ es igual a la amplitud, en $\mathbf r_n$, de una onda recíproca que incide desde la dirección externa $-\widehat{\mathbf s}$. Esta onda recíproca es una solución ordinaria de Bethe/ondas de Bloch. Escribiéndola como $\psi_{\widehat{\mathbf s}}^{\mathrm{rec}}(\mathbf r)$, la intensidad EBSD en la dirección $\widehat{\mathbf s}$ puede escribirse como

$$I_{\mathrm{EBSD}}(\widehat{\mathbf s};E,z)\propto
\sum_n \sigma_n(E,z)\,
\left|\psi_{\widehat{\mathbf s}}^{\mathrm{rec}}(\mathbf r_n;E,z)\right|^2$$

donde $\sigma_n(E,z)$ es el peso para la dispersión inelástica cerca de la posición atómica $\mathbf r_n$ en el canal de retrodispersión a energía $E$ y profundidad $z$. Los términos fuente se suman como intensidades, no como una suma coherente de amplitudes, porque se supone que la dispersión inelástica destruye la relación de fase entre las distintas posiciones fuente.

---

## Master pattern

El master pattern de EBSD almacena la parte de difracción dinámica específica del cristal de la expresión anterior sobre una malla de direcciones. Conceptualmente,

$$M(\widehat{\mathbf s};E,z)=
\sum_n w_n(E,z)\,
\left|\psi_{\widehat{\mathbf s}}^{\mathrm{rec}}(\mathbf r_n)\right|^2$$

donde $w_n$ es el peso de fuente inelástica del lado del cristal en la posición atómica $\mathbf r_n$. ReciPro utiliza el peso empírico

$$w_n \propto Z_n^{1.7}\,\mathrm{occ}_n$$

con el número atómico $Z_n$ y la ocupación $\mathrm{occ}_n$. Esto es independiente de la distribución de profundidad de transporte / energía producida por Monte Carlo.

En la implementación, la onda de Bloch recíproca se evalúa en cada posición atómica:

$$\beta_n^{(j)}=
\alpha^{(j)}
\sum_{\mathbf g}C_{\mathbf g}^{(j)}
\exp\!\left[2\pi i(\mathbf k^{(j)}+\mathbf g)\cdot\mathbf r_n\right]$$

El código forma entonces la matriz de pares de ondas de Bloch

$$S_{jj'}=\sum_n w_n\,\beta_n^{(j)}\,\overline{\beta_n^{(j')}}$$

y la integral analítica sobre el espesor

$$\mathcal F_{jj'}(t)=
\frac{\exp\!\left[2\pi i(\gamma^{(j)}-\overline{\gamma^{(j')}})t\right]-1}
{2\pi i(\gamma^{(j)}-\overline{\gamma^{(j')}})}$$

de modo que el master pattern se evalúa como

$$M(\widehat{\mathbf s};E,t)=
\mathrm{Re}\left\{\sum_{j,j'}S_{jj'}(E)\,\mathcal F_{jj'}(t)\right\}$$

En el límite degenerado en el que el denominador se acerca a cero, $\mathcal F_{jj'}(t)\to t$.

---

## Muestreo del espacio de direcciones

El master pattern no es la imagen del detector en sí; es una distribución de intensidad en el espacio de direcciones fijo al cristal. ReciPro muestrea ese espacio de direcciones con una proyección equiárea de Rosca-Lambert y almacena los hemisferios $+Z$ y $-Z$ como matrices planas separadas. El muestreo equiárea reduce el sesgo de densidad entre los polos y el ecuador.

En esta etapa, el master pattern depende de la estructura cristalina, la tensión de aceleración, la profundidad, la energía y el modelo de absorción. La geometría del detector, como el centro del patrón y la posición de la pantalla, aún no se ha aplicado.

---

## Pesos de Monte Carlo y patrón del detector

Para obtener un patrón de detector EBSD cercano al observable experimental, el master pattern debe ponderarse según cuántos electrones retrodispersados emergen de cada profundidad, energía y dirección. Escribiendo este peso de transporte como

$$W(E,z;\widehat{\mathbf s})$$

y usando $\widehat{\mathbf s}(\mathbf p)$ para la dirección fija al cristal que corresponde al píxel del detector $\mathbf p$, el patrón del detector final es

$$P(\mathbf p)=
\sum_{i,j}
W(E_i,z_j;\widehat{\mathbf s}(\mathbf p))\,
M(\widehat{\mathbf s}(\mathbf p);E_i,z_j)$$

como una suma discreta sobre energía y profundidad.

La parte de Monte Carlo sigue la dispersión elástica, la dispersión inelástica, la pérdida de energía y el escape a través de la superficie de la muestra. Para los electrones retrodispersados construye distribuciones de profundidad, energía y dirección de salida. ReciPro distingue entre modelos que utilizan la última posición de dispersión inelástica y la energía inmediatamente posterior como fuente efectiva, y modelos que utilizan la profundidad de escape y la energía de escape.

---

## Fondo de TDS y modelo de absorción

Los patrones EBSD contienen no solo la estructura geométrica de bandas de Kikuchi, sino también un fondo suave procedente de la dispersión térmica difusa (TDS). Cuando `IncludeTDSBackground` está activado, ReciPro evalúa la componente de TDS dispersada hacia el hemisferio posterior,

$$\pi/2\leq\theta\leq\pi$$

como una matriz de absorción $\mu_{\mathrm{back}}$ y añade la intensidad de fondo usando la misma sumación de pares de ondas de Bloch que el master pattern. Como se reutiliza la misma solución de autovalores, el fondo de TDS añade relativamente poco coste adicional.

Cuando `UseNonLocalAbsorption` está activado, el potencial absorptivo no se trata solo como $U'_{\mathbf g-\mathbf h}$, sino como una forma no local que depende de la dirección y de los pares de haces. Esto puede mejorar la precisión, pero también requiere reconstruir la matriz de absorción para las direcciones de la malla del master pattern, por lo que puede aumentar considerablemente el tiempo de cálculo.

---

## Parámetros prácticos

- **Número de haces**: Un número demasiado bajo de haces pierde el detalle de las bandas de Kikuchi y la estructura de bandas HOLZ. Los ejes de zona de índices bajos pueden requerir varios cientos de haces.
- **Matrices de profundidad y energía**: Si son más gruesas que la escala de variación del peso de Monte Carlo $W(E,z;\widehat{\mathbf s})$, los efectos de ancho de banda dependiente de la energía y de profundidad de canalización se promedian y se pierden.
- **Geometría del detector**: El centro del patrón, la distancia a la pantalla y la inclinación de la muestra determinan la aplicación $\widehat{\mathbf s}(\mathbf p)$, de modo que el patrón del detector puede cambiar incluso cuando el master pattern permanece inalterado.
- **Interpretación de la reciprocidad**: El master pattern no es la imagen del detector. Solo se convierte en un patrón de detector tras la ponderación de Monte Carlo y la proyección al detector.
- **Fondo de TDS**: Actívelo para comparaciones cuantitativas del contraste de bandas. Desactívelo cuando la estructura geométrica de Kikuchi sea más fácil de inspeccionar sin el fondo suave.

## Véase también

- [Cálculo dinámico (núcleo común)](calculation.md)
- [Apéndice A3. Difracción dinámica por el método de ondas de Bloch](index.md)
- [12. Simulación EBSD](../../12-ebsd-simulation.md)
