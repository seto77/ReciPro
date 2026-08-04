# Cálculo STEM

El cálculo de imágenes STEM parte de la misma representación de sonda convergente que [CBED](cbed.md). La diferencia está en el observable: CBED muestra la intensidad del disco en el plano de difracción, mientras que STEM barre la posición de la sonda e integra, en cada posición, la intensidad que entra en el detector seleccionado.

---

## Observable

Sea $\mathbf R_0$ la posición de la sonda, $\mathbf Q$ la coordenada del plano de difracción y $t$ el espesor de la muestra. Si la función del detector $D(\mathbf Q)$ vale 1 dentro del intervalo angular del detector y 0 fuera de él, la intensidad STEM elástica es

$$I_{\mathrm{STEM}}^{\mathrm{ela}}(\mathbf R_0)=
\int D(\mathbf Q)\,
\left|\psi(\mathbf Q,t;\mathbf R_0)\right|^2\,d\mathbf Q$$

BF, ABF, LAADF y HAADF corresponden a distintas elecciones de los ángulos interno y externo en $D(\mathbf Q)$. Por tanto, cambiar el ángulo del detector STEM cambia la magnitud física que se integra; no es solo un ajuste de visualización.

---

## Aceleración mediante coeficientes de Fourier

Una implementación directa resolvería de nuevo el problema dinámico para cada posición de sonda barrida $\mathbf R_0$. La expresión de la sonda convergente tiene una estructura útil: la dependencia de $\mathbf R_0$ aparece como el factor de fase

$$\exp(-2\pi i\,\mathbf K\cdot\mathbf R_0)$$

Esto permite a ReciPro calcular primero los coeficientes de Fourier bidimensionales de la imagen, en lugar de calcular $I_{\mathrm{STEM}}(\mathbf R_0)$ punto por punto. Conceptualmente,

$$I_{\mathrm{STEM}}^{\mathrm{ela}}(\mathbf q)=
\sum_{\mathbf g,\mathbf h}
F_{\mathbf g,\mathbf h}(t)\,
\delta(\mathbf q-\mathbf g+\mathbf h)$$

de modo que, una vez conocidos los coeficientes $F_{\mathbf g,\mathbf h}(t)$, la imagen de barrido completa puede reconstruirse de forma eficiente mediante una transformada de Fourier inversa.

Esta es la principal ventaja del STEM por ondas de Bloch para cristales perfectos con celdas elementales pequeñas. Puede ser mucho más rápido que repetir un cálculo multislice en cada posición de la sonda.

---

## Reconstrucción de una imagen real {#real-image-reconstruction}

La imagen se recupera a partir de los coeficientes mediante

$$I(\mathbf r)=\sum_{\mathbf q}I(\mathbf q)\,\exp(2\pi i\,\mathbf q\cdot\mathbf r),
\qquad \mathbf q=\mathbf g-\mathbf h$$

Como $I(\mathbf r)$ es una intensidad real, sus coeficientes deben cumplir exactamente la simetría hermítica,

$$I(-\mathbf q)=I(\mathbf q)^{*}$$

y el conjunto de $\mathbf q$ generado por todos los pares de haces es cerrado bajo $\mathbf q\rightarrow-\mathbf q$. La suma es por tanto real por construcción, y **cualquier parte imaginaria que sobreviva es error numérico, no física**.

En la práctica sí sobrevive una pequeña parte imaginaria, porque la amplitud en $\mathbf k+\mathbf q$ se obtiene por interpolación bilineal sobre la rejilla finita de direcciones de incidencia (véase [Muestreo angular de la sonda](#angular-sampling)). Esto hace que $I(-\mathbf q)$ y $I(\mathbf q)^{*}$ difieran en una cantidad de orden $h^{2}$, donde $h$ es el paso angular.

Escribiendo un píxel sumado como $a+ib$, la forma correcta de reducirlo a una imagen real es tomar la **parte real** $a$. Esa es la proyección ortogonal sobre el eje real, y es idéntica a simetrizar primero los coeficientes,

$$I_{\mathrm{sym}}(\mathbf q)=\tfrac12\left[I(\mathbf q)+I(-\mathbf q)^{*}\right]$$

y sumar después. Tomar el módulo $\sqrt{a^{2}+b^{2}}\simeq a+b^{2}/2a$ **no** es equivalente, y falla de cuatro maneras distintas:

- el término adicional $b^{2}/2a$ es estrictamente positivo, por lo que nunca se cancela: es un sesgo, no ruido;
- es mayor respecto a la señal allí donde $a$ es pequeño, es decir en los píxeles **oscuros**, de modo que ataca el contraste de la imagen en lugar del nivel global;
- rompe la linealidad, de modo que la imagen combinada ya no es igual a elástica + TDS, porque $\lvert z_1+z_2\rvert\neq\lvert z_1\rvert+\lvert z_2\rvert$;
- oculta los píxeles negativos, que son el síntoma visible de un conjunto de $\mathbf q$ insuficiente y que de otro modo advertirían al usuario.

Por ello ReciPro reconstruye las imágenes elástica, TDS y STEM-EDX a partir de la parte real, y recorta a cero solo después del desenfoque por tamaño de fuente, de forma que un píxel genuinamente negativo sigue siendo detectable hasta ese punto.

!!! note
    Hasta la versión 4.944, las imágenes elástica y TDS se sumaban en módulo. En la rejilla angular predeterminada la diferencia queda muy por debajo de cualquier nivel perceptible (véase la tabla siguiente); solo resulta medible en una rejilla deliberadamente gruesa, y siempre como un ligero aclarado de los píxeles oscuros.

---

## Muestreo angular de la sonda {#angular-sampling}

El cono incidente se muestrea sobre una rejilla cuadrada de direcciones con paso $\Delta\alpha$ (**Resolución angular** en las opciones STEM), cubriendo el semiángulo de convergencia $\alpha$ con un pequeño margen. El número de divisiones a lo largo de un eje es

$$N=\left\lceil\frac{2\alpha\times1.05}{\Delta\alpha}\right\rceil$$

de modo que el número de direcciones, y por tanto de problemas de autovalores a resolver, crece como $N^{2}$. Esta rejilla no tiene relación con el número de puntos de barrido: discretiza las *direcciones dentro de la sonda*, no las *posiciones de la sonda*.

Es además la única fuente del residuo hermítico descrito arriba, lo que convierte a ese residuo en un cómodo indicador de convergencia. Los valores siguientes se midieron para SrTiO₃ [001] a 200 kV con $\alpha=25$ mrad, 128 haces y 32×32 puntos de barrido. El «residuo» es $\max_{\mathbf q}\lvert I(\mathbf q)-I(-\mathbf q)^{*}\rvert$ relativo a $I(\mathbf 0)$, y las dos últimas columnas dan el aclarado que la suma en módulo habría añadido al píxel más brillante.

| $N$ | Direcciones | Residuo elástico | Residuo TDS | Sesgo de módulo, elástico | Sesgo de módulo, TDS |
|----:|-----------:|-----------------:|-------------:|------------------------:|--------------------:|
| 16  | 256    | 1.2×10⁻³ | 6.1×10⁻³ | 2.4×10⁻⁵ | 1.1×10⁻⁴ |
| 32  | 1024   | 4.1×10⁻⁴ | 2.6×10⁻³ | 1.1×10⁻⁶ | 1.3×10⁻⁵ |
| 64  | 4096   | 5.6×10⁻⁵ | 7.2×10⁻⁴ | 5.8×10⁻⁸ | 4.3×10⁻⁷ |
| 132 | 17424  | 3.8×10⁻⁵ | 1.1×10⁻⁴ | 4.2×10⁻⁸ | 3.6×10⁻⁸ |

La resolución angular predeterminada de 0,4 mrad da $N=132$ para $\alpha=25$ mrad, que ya está en la región convergida. Conviene destacar dos puntos:

- El residuo TDS es aproximadamente un orden de magnitud mayor que el elástico en todas las rejillas, porque los coeficientes TDS llevan además la integral en espesor de la absorción seleccionada por el detector.
- El residuo es un máximo sobre todos los $\mathbf q$, por lo que fluctúa algo de una rejilla a otra en lugar de decrecer de forma perfectamente suave; la tendencia subyacente es $O(h^{2})$.

---

## TDS y absorción seleccionada por el detector

En HAADF-STEM, la componente inelástica procedente de la dispersión térmica difusa (TDS) es a menudo la principal fuente de contraste de la imagen. ReciPro trata la TDS como la cantidad de intensidad que se retira del canal elástico hacia un intervalo angular seleccionado, representada mediante un potencial de absorción.

Para un intervalo angular del detector $\theta_1\leq\theta\leq\theta_2$, el factor de dispersión de absorción seleccionado por el detector puede escribirse conceptualmente como

$$f'_{\kappa}(\mathbf g;\theta_1,\theta_2)=
\int_{\theta_1}^{\theta_2}\sin\theta\,d\theta
\int_0^{2\pi}
\left|\Delta f_{e,\kappa}(\mathbf g,\theta,\phi)\right|^2\,d\phi$$

Elegir este intervalo de forma que coincida con un detector BF, ADF o HAADF evalúa la contribución de la TDS que entra en ese detector.

La intensidad STEM de TDS es la integral en el espesor de la absorción seleccionada por el detector:

$$I_{\mathrm{STEM}}^{\mathrm{TDS}}(\mathbf R_0)=
\int_0^t
\langle\psi(z;\mathbf R_0)|\widehat W_{\mathrm{det}}|\psi(z;\mathbf R_0)\rangle\,dz$$

donde $\widehat W_{\mathrm{det}}$ representa la TDS seleccionada por el detector. Una vez conocidos los valores propios y vectores propios de las ondas de Bloch, esta integral en $z$ puede tratarse de forma analítica. También es posible una integración numérica por capas, y ReciPro emplea el enfoque adecuado según el modo de cálculo.

---

## Absorción local y no local

El potencial de absorción puede tratarse de dos maneras principales.

| Forma | Significado | Característica |
|------|---------|---------|
| Aproximación local | Utiliza un potencial de absorción $U'(\mathbf r)$ que depende únicamente de la posición. | Suele ser eficaz y rápida para detectores ADF / HAADF amplios. |
| Forma no local | Utiliza $U'(\mathbf r,\mathbf r')$ o elementos de matriz $U'_{\mathbf g,\mathbf h}$ que dependen de pares de ondas entrantes y salientes. | Más precisa para detectores estrechos, elementos pesados o tensiones de aceleración bajas, pero mucho más costosa. |

En la aproximación local, los elementos de matriz pueden evaluarse a partir de diferencias de vectores recíprocos como $U'_{\mathbf g-\mathbf h}$. En la forma no local, cada par $(\mathbf g,\mathbf h)$ requiere su propia integración angular, de modo que el coste crece rápidamente con el número de haces.

---

## Alcance del STEM por ondas de Bloch

El STEM por ondas de Bloch es rápido para cristales altamente periódicos y perfectos, y se adapta bien a comparaciones sistemáticas de espesor, desenfoque y ángulos de detector. Para defectos, superceldas grandes o estructuras no periódicas, métodos como el multislice de fonón congelado pueden ser más adecuados, ya que no se basan en la misma hipótesis de celda periódica pequeña.

En ReciPro, la forma más sencilla de entender el STEM es la siguiente: se parte de la misma onda convergente que en CBED y luego se reemplaza el observable del disco de difracción por una integración del detector sobre el plano de difracción.

---

## Parámetros prácticos

- **Ángulo del detector**: BF / ABF / ADF / HAADF son definiciones de $D(\mathbf Q)$ y $f'_{\kappa}(\mathbf g;\theta_1,\theta_2)$.
- **Número de haces**: Las componentes de imagen de alta frecuencia y el channeling son sensibles al número de haces incluidos.
- **Paso de espesor**: Si se utiliza integración numérica por capas, comprueba el cambio al reducir a la mitad el grosor de la capa.
- **Resolución angular**: Fija la rejilla de direcciones $N$ de la sonda (véase [Muestreo angular de la sonda](#angular-sampling)). El coste crece como $N^{2}$, por lo que es la principal palanca sobre el tiempo de cálculo.
- **Modelo de TDS**: Para el contraste $Z$ en HAADF, el término de TDS es tan importante como el término elástico.

## Véase también

- [Cálculo dinámico (núcleo común)](calculation.md)
- [Apéndice A3. Difracción dinámica por el método de ondas de Bloch](index.md)
- [9.2. Simulación STEM](../../9-hrtem-stem-simulator/2-stem-simulation.md)
