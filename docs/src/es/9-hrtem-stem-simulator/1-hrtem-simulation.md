# Simulación HRTEM

La simulación **HRTEM (microscopía electrónica de transmisión de alta resolución)** calcula imágenes de franjas de red de TEM de alta resolución. Es el modo principal del [simulador HRTEM/STEM](index.md).

![Simulador en modo HRTEM](../../assets/cap-es-auto/FormImageSimulator-hrtem.png)

> Esta página describe todos los ajustes que aparecen en el lado derecho cuando **Modo de imagen = HRTEM**. Para los controles del lado izquierdo (visualización del resultado y ajuste de su brillo), consulte la [página de introducción](index.md#display-settings).

---

## Introducción

Una imagen HRTEM se forma cuando la onda electrónica transmitida a través de la muestra se proyecta en imagen bajo la influencia de las aberraciones de la lente objetivo. ReciPro calcula la propagación de la onda electrónica dentro de la muestra con el método de ondas de Bloch (cálculo dinámico) y genera la imagen HRTEM a través de la función de transferencia de contraste de fase (PCTF).

### Flujo de cálculo

1. **Método de ondas de Bloch**: calcula la propagación de la onda electrónica en el potencial cristalino y obtiene la amplitud y la fase de la onda de salida
2. **Función de lente**: aplica las aberraciones de la lente objetivo (aberración esférica $C_s$, desenfoque $\Delta f$)
3. **Coherencia parcial**: tiene en cuenta el tamaño finito de la fuente (coherencia espacial) y la fluctuación de energía (coherencia temporal)
4. **Formación de la imagen**: calcula la distribución de intensidad $|\psi(\mathbf{r})|^2$

Para la teoría, consulte el [Apéndice A3.2 — Formación de la imagen HRTEM](../appendix/a3-bloch-wave/hrtem.md).

---

## Muestra

![Muestra](../../assets/cap-es-auto/FormImageSimulator.splitContainer1.flowLayoutPanelModeSelection.groupBoxSampleProperty.png)

- **Espesor** : espesor de la muestra (nm). Las imágenes HRTEM dependen fuertemente del espesor. En modo **Imagen en serie** este valor se ignora y se utiliza en su lugar la lista de espesores descrita más abajo.

---

## Condiciones TEM {#tem-conditions}

![Condiciones TEM](../../assets/cap-es-auto/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxTEMConditions.png)

Establece las condiciones de formación de imagen de la lente objetivo.

| Parámetro | Descripción | Valor por defecto / típico |
|-----------|-------------|-------------------|
| **Voltaje de acel.** <!-- 261105Cl 監査: 見出しから (kV) を外した (numericBoxAccVol.HeaderText)。単位は欄の右に出る --> | Voltaje de aceleración (kV). A la derecha se muestra la longitud de onda del electrón con corrección relativista | 200 kV |
| **Desenfoque Δf** | Desenfoque de la lente objetivo (nm). Debajo se muestra como referencia el valor del **desenfoque de Scherzer** | −57.8 nm |
| **Cs** | Coeficiente de aberración esférica (mm). Afecta a la CTF y al desenfoque de Scherzer. Se admiten valores negativos (−20 a 20 mm) | 1.0 mm (por defecto); 0.5–1.0 (convencional), < 0.01 (corregida en Cs) |
| **Cc** | Coeficiente de aberración cromática (mm). Determina el emborronamiento de la imagen causado por la dispersión de energía | 1.4 mm (por defecto); 1.0–2.0 mm |
| **β** | Semiángulo de iluminación (mrad). Representa el efecto del tamaño finito de la fuente (coherencia espacial) | 0 mrad (por defecto); 0.1–1.0 mrad |
| **ΔV** | Anchura a media altura de la dispersión de energía de los electrones (eV). Junto con Cc determina la dispersión del foco debida a la aberración cromática | 0.8 eV (por defecto); 0.5–2.0 eV |

Los valores por defecto anteriores se aplican solo en el primer inicio; después se restauran los últimos valores utilizados. Internamente, ΔV (FWHM) se convierte en una desviación estándar $\sigma_V = \Delta V / (2\sqrt{2\ln 2})$ y la dispersión del foco es $\delta = C_c\,\sigma_V / V$. La función de aberración es

$$\chi(u) = \pi\lambda\,\Delta f\,u^2 + \tfrac{\pi}{2}\,C_s\lambda^3 u^4 ,$$

y cada onda difractada se multiplica por $\exp[-i\chi(u)]$, de modo que **un Δf negativo corresponde a subenfoque** (el convenio en el que el desenfoque de Scherzer es negativo).

> **Menú contextual**: en el panel de condiciones TEM puede aplicar con un solo clic **Ajustar desenfoque a 0 nm** / **Ajustar desenfoque al valor de Scherzer** / **Condiciones ideales (Cs, Cc, β, ΔV, Δf = 0)** (este último pone a 0 Cs, Cc, β, ΔV **y también el desenfoque**). <!-- 261106Cl 作者の決定 E19: 表示名を理想条件に --> El mismo menú contiene cuatro ajustes predefinidos de microscopio — *300kV Cold-FEG sin Cs (Cs 0, Cc 2.8, ΔV 0.3) ARM300F*, *200kV Schottky-FEG UHR (Cs 0.5, Cc 1.1, ΔV 0.8) 2100F*, *200kV Schottky-FEG HR (Cs 1.0, Cc 1.4, ΔV 0.8) 2100F* y *200kV Lab6 HR (Cs 1.0 , Cc 1.4, ΔV 2.0) 2100* —, que fijan el voltaje de aceleración, Cs, Cc y ΔV indicados en el elemento y a continuación ajustan el desenfoque al valor de Scherzer (β no se modifica). La casilla **Predefinidos** de la parte inferior derecha abre la ventana que guarda sus propias condiciones (véase [Lista de preajustes](index.md#preset-list)). <!-- 261105Cl 監査: プリセット名は ΔV (FormImageSimulator.resx presets1〜4ToolStripMenuItem.Text) -->

### Desenfoque de Scherzer

El valor de desenfoque en torno al cual el contraste de fase es óptimo, calculado a partir de la longitud de onda actual y de la aberración esférica $C_s$ (se muestra como referencia). Para $C_s$ negativo, el valor mostrado es $+\sqrt{\tfrac{4}{3}|C_s|\lambda}$.

$$\Delta f_{\text{Scherzer}} = -\sqrt{\tfrac{4}{3}\,C_s \lambda}\quad\left(\approx -1.155\,\sqrt{C_s \lambda}\right)$$

En esta condición la PCTF es negativa en un amplio intervalo de frecuencias espaciales, de modo que las posiciones atómicas aparecen con contraste oscuro. ReciPro adopta este valor original de Scherzer (obtenido al fijar el mínimo de la fase de aberración $\chi$ en $-2\pi/3$), y el valor mostrado en la GUI sigue esta fórmula. Tenga en cuenta que algunas referencias utilizan en su lugar el valor de *Scherzer extendido* $-1.2\sqrt{C_s\lambda}$, que ensancha aún más la banda de transferencia.

---

## Función de lente / Función de transferencia de contraste (CTF)

Al marcar **Función de transferencia de contraste (CTF)** se abre una ventana que representa cómo las aberraciones de la lente y el desenfoque transfieren el contraste de la imagen a cada frecuencia espacial.

![Función de transferencia de contraste (CTF)](../../assets/cap-es-auto/FormCTF.png)

- $\sin\chi(u)$ : función de transferencia de contraste de fase ($\chi(u)$ es la función de aberración de la lente)
- $E_\text{s}(u)$ : función envolvente de coherencia espacial; la atenuación debida al tamaño finito de la fuente ($\beta$)
- $E_\text{c}(u)$ : función envolvente de coherencia temporal; la atenuación debida a la fluctuación de energía ($C_c$, $\Delta V$)

Cada curva tiene su propia casilla: $\sin\chi$, $\cos\chi$, $E_\text{s}$, $E_\text{c}$, **PCTF** $=\sin\chi\,E_\text{s}E_\text{c}$ y **ACTF** $=\cos\chi\,E_\text{s}E_\text{c}$; PCTF y ACTF se ponen a cero fuera de la apertura de objetivo. En modo HRTEM

$$E_\text{s}(u)=\exp\!\left[-\pi^2\beta^2u^2\left(\Delta f+C_s\lambda^2u^2\right)^2\right],\qquad E_\text{c}(u)=\exp\!\left[-\tfrac{1}{2}\pi^2\lambda^2\delta^2u^4\right].$$

**u máx.** (nm⁻¹, por defecto 6) fija el límite superior del eje horizontal. **Copiar** coloca las curvas representadas en el portapapeles como texto separado por tabulaciones. La ventana también está disponible en modo STEM (véase [Simulación STEM](2-stem-simulation.md#ctf-stem)); está desactivada en el modo Potential.

---

## Apertura de objetivo (opción HRTEM)

![Apertura de objetivo (opción HRTEM)](../../assets/cap-es-auto/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxHREMoption1.png)

Limita las ondas difractadas que atraviesan el diafragma objetivo. El propio cálculo de ondas de Bloch utiliza siempre el número de ondas fijado en **Ondas**; el diafragma selecciona después cuáles de esos haces contribuyen a la imagen (un haz pasa cuando $|\mathbf{g}-\mathbf{c}| < 2\sin(\alpha_\text{obj}/2)/\lambda$, siendo $\mathbf{c}$ el centro del diafragma). Si pasan menos de dos haces, el cálculo se detiene con un mensaje que pide agrandar el diafragma.

- **Tamaño** : semiángulo del diafragma objetivo (mrad, por defecto 12). Cuanto menor es, más ondas difractadas de ángulo alto se cortan y más se suavizan los detalles de alta resolución. Se muestra el radio equivalente en el espacio recíproco $\sin\theta/\lambda$ (nm⁻¹).
- **Desplaz. X** / **Y** : desplazamiento del centro del diafragma objetivo (mrad). Se utiliza para imágenes de campo oscuro con diafragma desplazado; la iluminación en sí no se inclina.
- **Apertura abierta** : abre el diafragma objetivo (tamaño infinito), de modo que todas las ondas difractadas contribuyen a la imagen.
- **reflexiones dentro** : número de haces difractados (reflexiones) que caen dentro del diafragma (solo lectura).
- **Info reflexión** : abre una tabla con los haces difractados que hay dentro del diafragma (intensidad, amplitud compleja, etc.).

> El tamaño del diafragma objetivo también se muestra en el **Simulador de difracción**.

---

## Opciones HRTEM (modelo de coherencia parcial)

![Opciones HRTEM (modelo de coherencia parcial)](../../assets/cap-es-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.groupBoxHREMoption2.png)

Selecciona cómo se incluye la coherencia parcial (β y la dispersión del foco δ). La onda de salida se calcula para una única dirección de incidencia (el eje del haz); la dispersión de la iluminación y la dispersión de energía solo intervienen a través de las funciones envolventes siguientes.

- **Imagen lineal** (modelo cuasicoherente) : de bajo coste computacional. Cada onda difractada $\psi_g$ de la onda de salida dinámica se multiplica por $\exp[-i\chi(g)]\,E_\text{s}(g)\,E_\text{c}(g)$ y las ondas se suman coherentemente: $I(\mathbf{r}) = \left|\sum_g \psi_g\,e^{-i\chi(g)}\,E_\text{s}(g)\,E_\text{c}(g)\,e^{2\pi i\,\mathbf{g}\cdot\mathbf{r}}\right|^2$. Es precisa cuando la imagen está dominada por la interferencia con el haz transmitido (muestras delgadas).
- **Coef. cruz. de transm.** (coeficiente de transmisión cruzada) : de alto coste computacional pero más preciso. Se suma cada par de haces $(g,h)$ con la envolvente mutua (el coeficiente de transmisión cruzada completo), y es el modelo que debe usarse con dispersores fuertes que excitan muchas ondas difractadas intensas. El coste crece con el cuadrado del número de haces dentro del diafragma.

Para más detalles, consulte el [Apéndice A3.2 — Formación de la imagen HRTEM](../appendix/a3-bloch-wave/hrtem.md).

---

## Modo único/en serie

![Modo único/en serie](../../assets/cap-es-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.groupBoxSerialImage.png)

- **Imagen única** : calcula una imagen HRTEM al espesor y desenfoque actuales.
- **Imagen en serie** : genera un conjunto de imágenes variando por pasos el espesor y el desenfoque (serie en espesor / serie focal). Útil para encontrar la condición que mejor coincide con una imagen experimental.

Para una imagen en serie, establezca lo siguiente.

| Elemento | Descripción |
|------|-------------|
| **Espesor (nm)** / **Desenfoque (nm)** | Magnitud que se barre (se permiten ambas) |
| **Inicio / Paso / Núm** | Valor inicial, anchura del paso y número de imágenes. Se expanden en el cuadro de lista inferior, que también puede editarse directamente |
| **Dirección horizontal:** | Cuando se barren tanto el espesor como el desenfoque, la magnitud dispuesta en la dirección horizontal de la rejilla (**Desenfoque** o **Espesor**) |

Barrer a la vez el espesor y el desenfoque produce una matriz de imágenes de filas × columnas. **Núm** está limitado a 20 para cada magnitud. Cuando solo se barre una magnitud y hay más de dos imágenes, la fila se reparte en una rejilla casi cuadrada.

---

## Propiedades de imagen

![Propiedades de imagen](../../assets/cap-es-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.panelImageProperties.groupBoxImageProperty.png)

- **Tamaño (An×Al)** : número de píxeles de la imagen simulada (512×512 por defecto).
- **Resolución** : resolución de muestreo (pm/px, por defecto 2). El campo de visión es Tamaño × Resolución, de modo que un valor menor muestrea las franjas con más finura sobre un área menor. La imagen se sintetiza mediante una suma de Fourier directa sobre los haces en cada píxel (sin FFT), por lo que el tiempo de cálculo depende del número de píxeles y del número de haces, no de este valor.

---

## Ondas

![Ondas](../../assets/cap-es-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.panelImageProperties.groupBoxDiffractedWaves.png)

- Número máximo de ondas de Bloch utilizadas en el método de Bethe (cálculo dinámico); 80 por defecto. Un número mayor mejora la precisión, pero la resolución del problema de valores propios requiere un tiempo $O(N^3)$.

---

## Véase también

- [Simulador HRTEM/STEM (introducción)](index.md)
- [Simulación STEM](2-stem-simulation.md)
- [Simulación de potencial](3-potential-simulation.md)
- [Apéndice A3.2 — Formación de la imagen HRTEM](../appendix/a3-bloch-wave/hrtem.md)
