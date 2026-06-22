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
- **Modelo de TDS**: Para el contraste $Z$ en HAADF, el término de TDS es tan importante como el término elástico.

## Véase también

- [Cálculo dinámico (núcleo común)](calculation.md)
- [Apéndice A3. Difracción dinámica por el método de ondas de Bloch](index.md)
- [9.2. Simulación STEM](../../9-hrtem-stem-simulator/2-stem-simulation.md)
