# Cálculo de CBED

CBED (difracción de electrones de haz convergente) aplica el [núcleo dinámico](calculation.md) a muchas direcciones del haz incidente y, a continuación, coloca los resultados en discos de difracción. SAED tiene una sola dirección de incidencia; CBED trata cada punto dentro del diafragma objetivo como una **onda plana incidente parcial** y resuelve el problema de ondas de Bloch para cada uno de ellos.

---

## Representación del haz convergente

En la superficie de entrada, la sonda convergente puede escribirse como una suma de ondas planas utilizando la posición de la sonda $\mathbf R_0$, la fase de la lente $\chi(\mathbf K)$ y la función de diafragma $A(\mathbf K)$:

$$\psi_{\mathrm{in}}(\mathbf R,0)=\sum_{\mathbf K\in\mathrm{aperture}} A(\mathbf K)\,
\exp(-2\pi i\,\mathbf K\cdot\mathbf R_0)\,
\exp[-i\chi(\mathbf K)]\,
\exp(2\pi i\,\mathbf K\cdot\mathbf R)$$

Aquí $\mathbf K$ es la componente del vector de onda incidente paralela a la superficie de la muestra. Para un diafragma circular ideal con semiángulo de convergencia $\alpha$ y longitud de onda del electrón $\lambda$,

$$A(\mathbf K)=
\begin{cases}
1 & (|\mathbf K|\leq \sin\alpha/\lambda)\\
0 & (|\mathbf K|> \sin\alpha/\lambda)
\end{cases}$$

Una fase de lente representativa, utilizando el desenfoque $\Delta f$ y la aberración esférica $C_s$, es

$$\chi(\mathbf K)=\pi\lambda|\mathbf K|^2\Delta f+\frac{\pi}{2}C_s\lambda^3|\mathbf K|^4+\cdots$$

En ReciPro esta expresión se controla mediante los ajustes de aberración, diafragma y ángulo de convergencia.

---

## Cálculo dinámico para cada dirección

En CBED, cada $\mathbf K$ dentro del diafragma se trata como un haz incidente paralelo. El flujo de trabajo conceptual es:

1. Determinar el vector de onda de referencia refractado $\mathbf k_0(\mathbf K)$ a partir de $\mathbf K$ y la normal a la superficie de la muestra.
2. Seleccionar los haces reflejados mediante la magnitud de ordenación $R_{\mathbf g}=|\mathbf g|Q_{\mathbf g}^2$.
3. Construir la matriz de estructura $\mathbf A$ y calcular los coeficientes de transmisión $T_{\mathbf g}(t;\mathbf K)$ al espesor $t$.

Este es el cálculo de coeficientes de transmisión del [núcleo dinámico](calculation.md), repetido para cada dirección incidente muestreada. Para una serie de espesores, la solución propia de una dirección dada puede reutilizarse y solo es necesario actualizar los factores de propagación.

---

## Ensamblaje de los discos de difracción

Al colocar las ondas de salida de todas las direcciones $\mathbf K$ en el plano de difracción se obtiene la intensidad dentro del disco transmitido y de los discos difractados. Si $\mathbf Q$ es la coordenada del plano de difracción, el CBED promediado en posición o las condiciones de baja coherencia pueden aproximarse como una suma incoherente de intensidades:

$$I_{\mathrm{CBED}}(\mathbf Q)=
\sum_{\mathbf K\in\mathrm{aperture}}
\left|\psi_{\mathbf K}(\mathbf Q,t)\right|^2$$

Para los modos de tipo LACBED, donde importa la coherencia de fase a lo largo de una región más amplia, primero deben sumarse las amplitudes y después tomar la intensidad.

---

## Qué muestra CBED

CBED hace visible la dependencia del espesor de la solución de ondas de Bloch como estructura de intensidad dentro de los discos de difracción.

- Cambiar el espesor modifica las oscilaciones en el interior de los discos, las líneas HOLZ y las franjas de Kossel-Mollenstedt.
- Cambiar la orientación de incidencia modifica qué reflexiones se excitan con intensidad.
- Aumentar el ángulo de convergencia ensancha los discos y puede revelar solapamientos e información de zonas de Laue de orden superior.

CBED es, por tanto, la forma más directa de visualizar el resultado de ondas de Bloch como un patrón de discos en el plano de difracción. En ReciPro se entiende mejor como la combinación de la discretización del haz convergente, una solución dinámica por dirección y la reorganización en matrices de discos.

---

## Parámetros prácticos

- **Número de haces**: Las condiciones de fuerte eje de zona y el detalle de las líneas HOLZ requieren muchos haces reflejados. Compruebe cómo cambia el interior de los discos al aumentar el número máximo de ondas de Bloch.
- **Muestreo angular**: Si el muestreo de $\mathbf K$ dentro del diafragma es demasiado grueso, la intensidad de los discos se vuelve granular. Los ángulos de convergencia mayores requieren un muestreo más fino.
- **Espesor**: Las series de espesores se benefician del método de valores propios, ya que una solución propia puede reutilizarse para muchos espesores.
- **Coherencia**: Distinga las condiciones en las que basta una suma incoherente de intensidades de aquellas en las que se necesita una suma coherente de amplitudes.

## Véase también

- [Cálculo dinámico (núcleo común)](calculation.md)
- [Apéndice A3. Difracción dinámica por el método de ondas de Bloch](index.md)
- [7.4. Simulación CBED](../../7-diffraction-simulator/3-cbed-simulation.md)
