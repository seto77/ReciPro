# Cálculo dinámico (núcleo común)

Los simuladores de difracción y de imágenes de ReciPro comparten un **núcleo común de dispersión dinámica de ondas de Bloch (Bethe)**, descrito en esta página (potencial del cristal, términos de Debye–Waller y de absorción, el problema de valores propios, los coeficientes de transmisión y las intensidades). Los protocolos específicos de cada método se construyen sobre este núcleo:

- [SAED de haz paralelo](#parallel-beam-saed)
- [Formación de la imagen HRTEM](hrtem.md)
- [CBED](cbed.md)
- [STEM](stem.md)
- [EBSD](ebsd.md)

Para la teoría subyacente (ecuación de Schrödinger, teorema de Bloch, ecuación dinámica de Bethe, el problema de valores propios y las definiciones de la esfera de Ewald), véase [Apéndice A3. Difracción dinámica por el método de ondas de Bloch](index.md).

---

## Constantes

$$\gamma = \frac{m}{m_0} = 1 + \frac{e_0 E}{m_0 c^2}, \qquad \beta = \frac{v}{c} = \sqrt{1 - \left(\frac{m_0}{m}\right)^2} = \sqrt{1 - \gamma^{-2}}$$

- $\gamma$ : factor de corrección relativista; $E$ : voltaje de aceleración; $m_0$, $m$ : masa del electrón en reposo y relativista.
- $\Omega$ : volumen de la celda elemental.
- $k_{vac}$ : número de onda del electrón en el vacío.

---

## Potencial del cristal para la dispersión elástica

El coeficiente de Fourier del potencial del cristal para la dispersión elástica, sumado sobre los átomos $k$ en las posiciones $\mathbf r_k$, es

$$U_{\mathbf g}^{C} = \gamma\,\frac{1}{\pi\Omega}\sum_k f_k(\mathbf g)\,\exp\!\left[2\pi i\,\mathbf g\cdot\mathbf r_k\right]T_k(\mathbf g, M_k)$$

donde el **factor de dispersión atómico** emplea una parametrización gaussiana $(a_i, b_i)$,

$$f_k(\mathbf g) = \sum_i a_i\exp\!\left[-b_i\,\frac{|\mathbf g|^2}{4}\right]$$

y $T_k$ es el **factor de Debye–Waller (de temperatura)**. Para un factor de temperatura isótropo $M_k$,

$$T_k(\mathbf g, M_k) = \exp\!\left[-M_k\,\frac{|\mathbf g|^2}{4}\right]$$

y para un tensor anisótropo de desplazamiento atómico $\mathbf U$,

$$T_k(\mathbf g) = \exp\!\left[-2\pi\,\mathbf g^{t}\mathbf U\,\mathbf g\right]$$

con la forma cuadrática

$$\mathbf g^{t}\mathbf U\,\mathbf g = \begin{pmatrix} g_x & g_y & g_z\end{pmatrix}\begin{pmatrix} U_{11} & U_{12} & U_{13}\\ U_{12} & U_{22} & U_{23}\\ U_{13} & U_{23} & U_{33}\end{pmatrix}\begin{pmatrix} g_x\\ g_y\\ g_z\end{pmatrix} = g_x^2 U_{11} + g_y^2 U_{22} + g_z^2 U_{33} + 2\!\left(g_x g_y U_{12} + g_y g_z U_{23} + g_x g_z U_{13}\right)$$

Las componentes cartesianas de $\mathbf g$ se obtienen a partir de los vectores de la base recíproca y los índices de Miller:

$$\begin{pmatrix} g_x\\ g_y\\ g_z\end{pmatrix} = \begin{pmatrix} a_x^{*} & b_x^{*} & c_x^{*}\\ a_y^{*} & b_y^{*} & c_y^{*}\\ a_z^{*} & b_z^{*} & c_z^{*}\end{pmatrix}\begin{pmatrix} h\\ k\\ l\end{pmatrix} = \begin{pmatrix} h\,a_x^{*} + k\,b_x^{*} + l\,c_x^{*}\\ h\,a_y^{*} + k\,b_y^{*} + l\,c_y^{*}\\ h\,a_z^{*} + k\,b_z^{*} + l\,c_z^{*}\end{pmatrix}$$

!!! note
    Los valores de $U_{\mathbf g}$ mostrados en la tabla **Details** del simulador de difracción son los valores en bruto *antes* de aplicar el factor relativista $\gamma$.

---

## Potencial de absorción (dispersión térmica difusa)

El potencial imaginario (de absorción) que da cuenta de la dispersión térmica difusa (TDS) es

$$U'_{g,h} = \gamma\,\frac{1}{\pi\Omega}\sum_k f'_k(\mathbf g,\mathbf h)\,\exp\!\left[2\pi i(\mathbf g-\mathbf h)\cdot\mathbf r_k\right]T_k(\mathbf g-\mathbf h, M_k)$$

con el **factor de dispersión de absorción**

$$f'_k(\mathbf g,\mathbf h) = \frac{2h}{\beta\, m_0\, c}\sum_i\sum_j a_i a_j\left[\frac{1}{b_i+b_j}\exp\!\left\{-\frac{b_i b_j}{b_i+b_j}\,\frac{|\mathbf g-\mathbf h|^2}{4}\right\} - \frac{1}{b_i+b_j+2M_k}\exp\!\left\{-\frac{b_i b_j - M_k^2}{b_i+b_j+2M_k}\,\frac{|\mathbf g-\mathbf h|^2}{4}\right\}\right]$$

Aquí $h$ en el prefactor $2h/(\beta m_0 c)$ es la **constante de Planck** (no un índice de haz). Los coeficientes $U^{C}$ y $U'$ son los elementos de la matriz de estructura $\mathbf A$ en el [Apéndice A3](index.md).

---

## De la solución propia a la intensidad difractada

La diagonalización de la matriz de estructura (véase el [Apéndice A3](index.md)) da los valores propios $\lambda^{(j)}$ y las amplitudes de las ondas de Bloch $C_{\mathbf g}^{(j)}$. Las amplitudes de onda en la superficie de salida —los **coeficientes de transmisión** $T_{\mathbf g}$— al espesor de la muestra $t$ son

$$\begin{pmatrix} T_0\\ T_g\\ T_h\\ \vdots\end{pmatrix}
= e^{-2\pi i(\mathbf k_{vac}\cdot\mathbf n)\,t}
\begin{pmatrix} e^{\pi i P_0 t} & 0 & 0 & \cdots\\ 0 & e^{\pi i P_g t} & 0 & \cdots\\ 0 & 0 & e^{\pi i P_h t} & \cdots\\ \vdots & \vdots & \vdots & \ddots\end{pmatrix}
\begin{pmatrix} C_0^{(1)} & C_0^{(2)} & C_0^{(3)} & \cdots\\ C_g^{(1)} & C_g^{(2)} & C_g^{(3)} & \cdots\\ C_h^{(1)} & C_h^{(2)} & C_h^{(3)} & \cdots\\ \vdots & \vdots & \vdots & \ddots\end{pmatrix}
\begin{pmatrix} e^{2\pi i\lambda^{(1)} t} & 0 & 0 & \cdots\\ 0 & e^{2\pi i\lambda^{(2)} t} & 0 & \cdots\\ 0 & 0 & e^{2\pi i\lambda^{(3)} t} & \cdots\\ \vdots & \vdots & \vdots & \ddots\end{pmatrix}
\begin{pmatrix} \alpha^{(1)}\\ \alpha^{(2)}\\ \alpha^{(3)}\\ \vdots\end{pmatrix}$$

o, componente a componente,

$$T_{\mathbf g} = e^{-2\pi i(\mathbf k_{vac}\cdot\mathbf n)\,t}\; e^{\pi i P_g t}\sum_j C_{\mathbf g}^{(j)}\,e^{2\pi i\lambda^{(j)} t}\,\alpha^{(j)}$$

- $\alpha^{(j)}$ : los coeficientes de ponderación (de excitación) de cada onda de Bloch, fijados por la condición de contorno en la superficie de entrada.
- $t$ : espesor de la muestra.

La intensidad difractada del haz $\mathbf g$ es entonces

$$I_{\mathbf g} = \left|T_{\mathbf g}\right|^2$$

---

## Cálculo SAED de haz paralelo { #parallel-beam-saed }

La SAED ordinaria (difracción de electrones de área seleccionada) se trata como **difracción de haz paralelo** con una única dirección de incidencia. A diferencia de CBED, no recorre muchos puntos $\mathbf K$ dentro de una apertura convergente. La orientación actual del cristal y el voltaje de aceleración definen un vector de onda incidente $\mathbf k_0$, y ReciPro evalúa la posición y la intensidad de cada reflexión $\mathbf g$ para esa condición.

El cálculo puede organizarse del siguiente modo.

1. Use la orientación del cristal, el voltaje de aceleración, la longitud de onda, la longitud de cámara y la geometría del detector para definir el vector de onda incidente en el vacío $\mathbf k_{vac}$ y el plano del detector.
2. Aplique la corrección por refracción a partir del potencial interno medio $U_0$ y obtenga el vector de onda de referencia del cristal $\mathbf k_0$.
3. Enumere los vectores candidatos de la red recíproca $\mathbf g$ y evalúe su distancia a la esfera de Ewald mediante cantidades como $Q_g=|\mathbf k_0|^2-|\mathbf k_0+\mathbf g|^2$ y el error de excitación $S_g$.
4. Calcule la intensidad de cada reflexión usando el modo de intensidad seleccionado.
5. Proyecte la dirección de $\mathbf k_0+\mathbf g$ sobre el plano del detector y dibújela como un punto de difracción.

El modo SAED de ReciPro ofrece principalmente los siguientes modelos de intensidad.

| Modo | Cálculo | Uso típico |
|------|-------------|-------------|
| Solo error de excitación | Estima la intensidad únicamente a partir de la cercanía de la reflexión a la esfera de Ewald. No se usan los factores de estructura. | Comprobaciones rápidas de las posiciones de los puntos y de la geometría del eje de zona. |
| Cinemática + error de excitación | Usa $\lvert F_{\mathbf g}\rvert^2$ junto con la atenuación por el error de excitación. No se incluye la dispersión múltiple. | Muestras delgadas, difracción débil y comprobación de reglas de extinción. |
| Teoría dinámica | Usa el núcleo de ondas de Bloch de esta página para obtener $T_{\mathbf g}(t)$ y fija $I_{\mathbf g}=\lvert T_{\mathbf g}\rvert^2$. | Dependencia del espesor, dispersión múltiple y reflexiones intensas de difracción de electrones. |

Los modos de visualización de los puntos de la red recíproca, como las secciones transversales de esfera sólida y los puntos gaussianos, controlan principalmente el perfil de dibujo. En el modo de teoría dinámica, la intensidad física de la reflexión se determina por el valor de la onda de Bloch $|T_{\mathbf g}|^2$, y esa intensidad se asigna después al perfil de visualización elegido.

PED puede verse como la integración de este cálculo SAED de haz paralelo sobre las direcciones de precesión, mientras que CBED puede verse como la disposición de muchas direcciones de incidencia dentro de los discos de difracción.

---

## Potencial interno medio y refracción

Cuando el electrón entra en el cristal desde el vacío, el potencial interno medio $U_0$ cambia ligeramente el vector de onda de referencia dentro del cristal. La componente paralela a la superficie está fijada por la condición de contorno, de modo que el vector de onda en el vacío $\mathbf k_{vac}$ y el vector de onda de referencia del cristal $\mathbf k_0$ pueden escribirse como

$$|\mathbf k_0|^2 = k_{vac}^2 + U_0, \qquad \mathbf k_0 = \mathbf k_{vac} + x\,\hat{\mathbf n}$$

donde $x$ es la corrección a lo largo de la normal a la superficie. Se obtiene de

$$x^2 + 2(\hat{\mathbf n}\cdot\mathbf k_{vac})x - U_0 = 0$$

Este $\mathbf k_0$ refractado se utiliza al evaluar $P_g$, $Q_g$, los errores de excitación y la matriz de estructura $\mathbf A$ en la [página general](index.md). El potencial de absorción también tiene una componente $\mathbf g=\mathbf 0$, $U'_0$, que actúa como una atenuación media común para las ondas que se propagan por el cristal.

---

## Selección de haces

El cálculo de ondas de Bloch no puede incluir infinitos vectores de la red recíproca, por lo que ReciPro selecciona un conjunto finito de haces $\{\mathbf g\}$. La cantidad de ordenación es

$$R_{\mathbf g}=|\mathbf g|\,Q_{\mathbf g}^{\,2}$$

y los haces con menor $R_{\mathbf g}$ se incluyen primero. Esto favorece los haces con vectores de la red recíproca cortos que, además, están cerca de la esfera de Ewald.

En los cálculos prácticos, es importante comprobar cuánto cambia la intensidad o la imagen al aumentar el número máximo de ondas de Bloch. Las condiciones intensas de eje de zona y los patrones CBED con detalle de líneas HOLZ pueden requerir varios cientos de haces, mientras que las condiciones fuera del eje de zona pueden converger con menos haces.

---

## Elección del solucionador

Una vez elegido el conjunto finito de haces, ReciPro emplea principalmente dos vías equivalentes para obtener los coeficientes de transmisión.

| Método | Característica | Uso típico |
|--------|---------|-------------|
| Método de valores propios | Diagonaliza la matriz de estructura $\mathbf A$ y obtiene los valores propios $\lambda^{(j)}$ y los vectores propios $C_{\mathbf g}^{(j)}$. La dependencia del espesor se evalúa después mediante $e^{2\pi i\lambda^{(j)}t}$. | Series de espesores, y cálculos CBED y EBSD que recorren muchas profundidades o energías |
| Método de la exponencial de matriz | Evalúa directamente la matriz de dispersión $\exp(2\pi i\mathbf A t)$ sin usar explícitamente una descomposición propia. | Cálculos STEM de un único espesor y cálculos integrados por láminas |

Ambos métodos resuelven la misma ecuación de Bethe. En la implementación, el código elige entre el método de valores propios, el método de la exponencial de matriz, las rutinas gestionadas de .NET y la biblioteca nativa Eigen según el número de haces, el conjunto de espesores y la disponibilidad de la biblioteca nativa.

---

## Comprobaciones de convergencia

En los cálculos dinámicos, comprobar que la base es lo bastante grande es tan importante como la propia fórmula. Un diagnóstico útil es el cambio relativo cuando el número de haces aumenta de $N-\Delta N$ a $N$:

$$\Delta I_N=\frac{|I_N-I_{N-\Delta N}|}{I_N}$$

Para STEM, compruébelo junto con el ajuste del ángulo del detector. Para CBED, inspeccione el interior de los discos y las líneas HOLZ. Para EBSD, compare además las anchuras de las bandas de Kikuchi y el fondo en el master pattern. Esto conecta la convergencia numérica con las características físicas visibles en el resultado simulado.

---

## Véase también

- [Apéndice A3. Difracción dinámica por el método de ondas de Bloch](index.md)
- [7.2. SAED simulation](../../7-diffraction-simulator/1-saed-simulation.md)
- [7.4. CBED simulation](../../7-diffraction-simulator/3-cbed-simulation.md)
- [7. Simulador de difracción](../../7-diffraction-simulator/index.md)
