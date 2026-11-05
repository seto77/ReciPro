# Simulación STEM

La **simulación STEM (Scanning Transmission Electron Microscopy)** calcula imágenes de microscopía electrónica de transmisión de barrido mediante el método de ondas de Bloch.

![Simulador en modo STEM](../../assets/cap-es-auto/FormImageSimulator-stem.png)

> Esta página enumera todos los ajustes que aparecen a la derecha cuando **Modo de imagen = STEM**. Para los controles de la izquierda relativos a la visualización del resultado, el brillo y la normalización, consulta la [página de introducción](index.md). Solo se repite a continuación el **objetivo de visualización** específico de STEM.

---

## Introducción

Un haz de electrones convergente se barre sobre la muestra, y los electrones transmitidos y dispersados en cada posición de barrido son recogidos por detectores anulares. ReciPro calcula la imagen STEM con el método de ondas de Bloch (cálculo dinámico).

### Flujo de cálculo

1. En cada posición de barrido, calcula las intensidades difractadas con el método de ondas de Bloch para cada dirección de incidencia de la sonda convergente.
2. Integra la intensidad dispersada sobre el rango angular del detector.
3. Se pueden calcular tanto las contribuciones de dispersión elástica como las de dispersión térmica difusa (TDS).

Consulta el [Apéndice A3.4 — Cálculo STEM](../appendix/a3-bloch-wave/stem.md) para la teoría.

---

## Tipos de detector

| Detector | Rango angular | Contribución principal | Contraste |
|----------|-------------|-------------------|----------|
| **BF** (campo claro) | 0 – ángulo de convergencia | Elástica | Contraste de fase |
| **ABF** (campo claro anular) | Parte interior del ángulo de convergencia | Elástica | Sensible a elementos ligeros |
| **LAADF** (campo oscuro anular de ángulo bajo) | Justo fuera del ángulo de convergencia | Elástica + TDS | Sensible a la deformación |
| **HAADF** (campo oscuro anular de ángulo alto) | Bastante fuera del ángulo de convergencia | TDS (inelástica) | Contraste Z ($\propto Z^2$) |

> **Ajustes típicos de detector** (cada uno disponible con un clic desde el menú contextual de las opciones STEM, todos con ángulo de convergencia α = 25 mrad):
> BF (0–5 mrad) / ABF (12–24 mrad) / LAADF (26–60 mrad) / HAADF (80–250 mrad)

---

## Parámetros de la muestra

![Parámetros de la muestra](../../assets/cap-es-auto/FormImageSimulator.splitContainer1.flowLayoutPanelModeSelection.groupBoxSampleProperty.png)

- **Espesor** : espesor de la muestra (nm). Este valor se ignora en el modo **Imagen en serie**.

---

## Condiciones del TEM

![Condiciones del TEM](../../assets/cap-es-auto/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxTEMConditions.png)

| Parámetro | Descripción | Predeterminado / típico |
|-----------|-------------|-------------------|
| **Voltaje de acel.** <!-- 261105Cl 監査: 見出しから (kV) を外した (numericBoxAccVol.HeaderText)。単位は欄の右に出る --> | Tensión de aceleración (kV). La longitud de onda del electrón corregida relativistamente se muestra al lado | 200 kV |
| **Desenfoque Δf** | Desenfoque de la lente objetivo (formadora de la sonda) (nm) | −57.8 nm |
| **Cs** | Coeficiente de aberración esférica (mm). Afecta al tamaño de la sonda | 0.5–1.0 mm |
| **Cc** | Coeficiente de aberración cromática (mm) | 1.0–2.0 mm |
| **ΔV** | Anchura a media altura de la dispersión de energía de los electrones (eV) | 0.5–2.0 eV |

> **β (semiángulo de iluminación) está deshabilitado en el modo STEM**, porque el ángulo de convergencia α asume su función. El desenfoque y Cs entran en la sonda a través de la misma función de aberración $\chi$ que en HRTEM (Δf negativo = subenfoque; véase [Simulación HRTEM](1-hrtem-simulation.md#tem-conditions)). El menú contextual de **Condiciones TEM** (aberraciones a cero, desenfoque de Scherzer, ajustes predefinidos de microscopio) también funciona aquí.

---

## Opciones STEM (óptica)

![Opciones STEM (óptica)](../../assets/cap-es-auto/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxSTEMoption1.png)

Define la geometría de la sonda convergente y del detector anular. Cada ángulo también se muestra a la derecha convertido a un radio en el espacio recíproco $\sin\theta/\lambda$ (nm⁻¹).

| Parámetro | Descripción | Predeterminado / típico |
|-----------|-------------|-------------------|
| **Semiáng. conv. α** | Semiángulo de la sonda convergente (mrad). Valores mayores dan una sonda más fina y cambian el contraste de difracción | 25 mrad (por defecto); 15–25 mrad |
| **Detector anular — Interior** | Semiángulo interior de captación del detector anular (mrad). La señal dentro de este ángulo se excluye | 0 (por defecto); BF: 0, HAADF: 80 |
| **Detector anular — Exterior** | Semiángulo exterior de captación del detector anular (mrad). La señal fuera de este ángulo se excluye | 20 (por defecto); BF: 5, HAADF: 250 |
| **Tamaño efectivo de la fuente (FWHM)** <!-- 261105Cl 監査: 見出しを numericBoxSTEM_EffectiveSourceSize.HeaderText に合わせた --> | Tamaño efectivo de la fuente de electrones (pm). Se aplica tras el cálculo como una convolución gaussiana de cada imagen Elástica / TDS / EDX. Valores mayores difuminan la imagen y reducen el contraste de los detalles finos | 20 pm (por defecto) |

Los ángulos del detector solo se aplican a la imagen STEM de referencia (Elástico / TDS / Elástico & TDS); los mapas STEM-EDX no los usan. Los valores por defecto se aplican solo en el primer inicio; después se restauran los últimos valores utilizados.

---

## Opciones STEM (simulación)

![Opciones STEM (simulación)](../../assets/cap-es-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.groupBoxSTEMoption2.png)

- **Espesor de capa (TDS)** : espesor de la rebanada de la muestra (nm, por defecto 1, intervalo 0.1–10) usado al calcular la intensidad TDS (térmica difusa, inelástica). Valores menores son más precisos pero más lentos.
- **Resolución angular** : resolución de muestreo angular de las direcciones de incidencia de la sonda (mrad, por defecto 0.4). Valores menores muestrean la sonda de forma más fina pero son más lentos. Las direcciones de la sonda forman una rejilla $N\times N$ con $N=\lceil 2\times1.05\,\alpha/\Delta\alpha\rceil$, de la que se calculan las que quedan dentro de un disco aproximadamente un 5 % mayor que el ángulo de convergencia (un margen para la interpolación); por tanto, el número crece con el cuadrado de $\alpha/\Delta\alpha$ y es la principal palanca sobre el tiempo de cálculo. La línea bajo la casilla muestra la rejilla (por ejemplo `Rejilla: 132²`); véase [Muestreo angular de la sonda](../appendix/a3-bloch-wave/stem.md#angular-sampling) para las medidas de convergencia.

---

## Modo único/en serie

<!-- 261105Cl 監査: 見出しを画面 (groupBoxSerialImage.Text) と HRTEM のページに合わせた -->
![Modo único/en serie](../../assets/cap-es-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.groupBoxSerialImage.png)

- **Imagen única** : calcula una imagen STEM al espesor actual.
- **Imagen en serie** : genera una serie de imágenes con el espesor / desenfoque escalonado por etapas (definido mediante **Inicio / Paso / Núm**; la lista de abajo también se puede editar directamente).

---

## Propiedades de la imagen

![Propiedades de la imagen](../../assets/cap-es-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.panelImageProperties.groupBoxImageProperty.png)

- **Tamaño (An×Al)** : número de píxeles de la imagen barrida (predeterminado 512×512). La solución de valores propios y el cálculo de $I(Q)$ (etapas 1–4 de la barra de estado) no dependen de él; solo interviene en la síntesis final de la imagen en el espacio real a partir de los coeficientes $I(Q)$, cuyo coste es proporcional a píxeles × número de vectores $Q$.
- **Resolución** : resolución de muestreo (pm/px).

---

## Ondas

![Ondas](../../assets/cap-es-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.panelImageProperties.groupBoxDiffractedWaves.png)

- Número máximo de ondas de Bloch usadas en el método de Bethe (predeterminado 80). El coste del problema de valores propios escala con el cubo del número de ondas.

---

## Función de transferencia de contraste (STEM) {#ctf-stem}

La casilla **Función de transferencia de contraste (CTF)** de **Condiciones TEM** abre la misma ventana de CTF que en el modo HRTEM. En el modo STEM ofrece dos opciones:

- **Imagen coherente** : representa $\sin\chi$, $\cos\chi$, $E_\text{s}$, $E_\text{c}$, PCTF y ACTF como en HRTEM, pero $E_\text{s}(u)=\exp(-2\pi^2\sigma_s^2u^2)$ se toma del tamaño efectivo de la fuente ($\sigma_s$ = FWHM / $2\sqrt{2\ln2}$), y PCTF / ACTF se cortan en el ángulo de convergencia.
- **Imagen incoherente** : representa la función de transferencia incoherente, calculada integrando la función de lente sobre el solapamiento de dos aperturas formadoras de la sonda (muestreo de la apertura de 192 × 192) y multiplicando por $\exp(-2\pi^2\sigma_s^2u^2)$.

---

## Objetivo de visualización STEM (lado del resultado) {#stem-display-target}

![Imagen STEM](../../assets/cap-es-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxSTEMoption3.png)

El conmutador de visualización situado abajo a la izquierda de la ventana selecciona qué componente de dispersión de la imagen STEM ya calculada se muestra (conmutable sin volver a calcular).

| Objetivo de visualización | Descripción |
|----------------|-------------|
| **Elástico** | Imagen solo de dispersión elástica |
| **TDS** | Imagen solo de dispersión térmica difusa |
| **Elástico & TDS** | Suma de elástica + TDS |
| **EDX** | Mapa de rayos X característicos. La línea que se muestra (por ejemplo `O-K`) se elige en el cuadro combinado situado debajo, y **EDX común** en *Normalización* pone los canales de la misma magnitud (canales de capa: vacantes; series de líneas: fotones de rayos X) en un mismo rango de visualización compartido, de modo que cambiar entre ellos no reescala la imagen |

!!! note
    Las tres imágenes se reconstruyen a partir de la parte real de la suma de Fourier, de modo que **Elástico & TDS** es exactamente la suma de las otras dos. Hasta la versión 4.944 se tomaba el módulo, lo que rompía esa identidad y aclaraba ligeramente los píxeles oscuros. Véase [Reconstrucción de una imagen real](../appendix/a3-bloch-wave/stem.md#real-image-reconstruction).

---

## Mapas elementales STEM-EDX {#stem-edx}

![Mapas elementales STEM-EDX](../../assets/cap-es-auto/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxSTEMoption1.groupBoxSTEMoption4.png)

Marca **Calcular mapas EDX** para calcular mapas de rayos X característicos junto con la imagen de tipo ADF. No es un modo separado: las señales elástica, TDS y EDX salen del mismo cálculo STEM, y después se conmuta entre ellas en [Objetivo de visualización STEM](#stem-display-target) sin volver a calcular.

No hay selector de elementos. Cuando la casilla está activada se calculan **todos los canales elemento/capa que pueden calcularse para este cristal a esta tensión de aceleración**, y la línea situada bajo la casilla los enumera (por ejemplo `3 mapa(s): O-K, Mg-K, Al-K`). Un canal está disponible cuando la tensión de aceleración se encuentra en el intervalo de 30–400 kV (el rango de la tabla incluida), el borde de ionización queda por debajo de ella y la capa está cubierta por los datos incluidos — K: C–Sn (Z = 6–50), L1–L3: Ca–Rn (Z = 20–86), M1–M3: Zn–Rn (Z = 30–86), M4–M5: As–Rn (Z = 33–86); los canales L y M suman las subcapas presentes. La tabla incluida almacena factores de forma de ionización totalmente relativistas para todos los canales hasta un vector de dispersión de 16 Å⁻¹, o menos a tensiones de aceleración bajas. No se extrapola nada: donde el cálculo necesita un vector de dispersión mayor que el que cubre una fila de la tabla, el factor de forma se pone a cero y el canal se marca con ⚠ en el cuadro combinado de EDX. Si no hay ningún canal disponible, el cálculo se rechaza con un mensaje explicativo en lugar de producir un mapa vacío.

La rejilla de direcciones de la sonda se indica bajo **Resolución angular** en *Opciones STEM (simulación)*; mientras **Calcular mapas EDX** está marcada, se añade la división recomendada, por ejemplo `Rejilla: 132² (recomendado: ≥48²)`. Esta rejilla la determinan **Resolución angular** y el ángulo de convergencia; véase [Muestreo angular de la sonda](../appendix/a3-bloch-wave/stem.md#angular-sampling). Por debajo de la división recomendada el residuo hermítico ±q puede superar la tolerancia y abortar el cálculo, por lo que el valor se muestra en naranja y aparece un diálogo de confirmación antes de iniciar el cálculo.

!!! warning "Qué representan los valores"
    Para un canal de capa (K, L, M), el mapa es el **número de vacantes de capa interna generadas por electrón incidente**; no se aplica el rendimiento de fluorescencia. Para una serie de líneas (Kα, Kβ, Lα, Lβ, Mα), que se calcula cuando **Series de líneas (Kα, Lα…)** está marcada, es el **número de fotones de rayos X generados por electrón incidente**, en todas las direcciones y antes de la detección: se aplican el rendimiento de fluorescencia, las cascadas Coster–Kronig y radiativas y la ramificación de líneas (xraylib 4.2.1), pero no la cascada Auger. Ambos son magnitudes del modelo, no recuentos previstos de rayos X: la autoabsorción en la muestra, el ángulo sólido del detector y la eficiencia del detector **no** se aplican. Usa los mapas para la distribución espacial y para comparar espesores u orientaciones, no para una cuantificación absoluta.

### Parámetros del detector (reservados)

**Autoabsorción**, **Ángulo de salida** y **Detector** están dispuestos pero deshabilitados: pertenecen al modelo de detector que aún no está implementado. Se muestran para que el panel no se desplace cuando ese modelo llegue. Su efecto futuro difiere en naturaleza:

| Factor | Contraste píxel a píxel dentro de un mapa | Cociente entre mapas de elementos |
|---|---|---|
| Autoabsorción (ángulo de salida) | **lo cambia** | **lo cambia** |
| Ventana del detector / capa muerta / eficiencia | sin efecto | **lo cambia fuertemente** |
| Ángulo sólido del detector, corriente del haz, tiempo de permanencia | sin efecto | sin efecto |

La última fila explica por qué ReciPro no expone en absoluto la corriente del haz ni el tiempo de permanencia: multiplican cada píxel de cada mapa por el mismo número, se cancelan en cualquier cociente y resultan invisibles tras la normalización de la visualización.

### Precisión y coste

STEM-EDX no impone ningún límite adicional al número de ondas ni al espesor de la rebanada: recorre las mismas rutas de cálculo que la imagen de tipo ADF, de modo que los ajustes que funcionan para STEM funcionan también para EDX.

La precisión queda en tus manos, exactamente igual que con el número de ondas o la resolución angular. Como referencia, el error de la integración en profundidad crece aproximadamente en proporción a **Espesor de capa (TDS)** — en torno al 2–3 % a 1 nm, 4–8 % a 2 nm y 12–23 % a 4 nm (relativo al pico, SrTiO₃ a 39 nm). Reducir el espesor de la rebanada a la mitad reduce el error aproximadamente a la mitad y duplica aproximadamente el trabajo de integración en profundidad.

Con aberraciones definidas (por ejemplo Cs = 1 mm con desenfoque de Scherzer a α = 25 mrad), la fase de aberración oscila rápidamente sobre la rejilla de direcciones de la sonda, y STEM-EDX puede rechazar el cálculo con un error *non-Hermitian residual* incluso con una rejilla fina; este rechazo protege el mapa de artefactos de rejilla de algunos por ciento. Reduzca Cs y el desenfoque (el promedio de barrido de un mapa EDX no depende en absoluto de las aberraciones), o haga la **Resolución angular** bastante más fina aceptando un cálculo más largo.

---

## Coste computacional

La simulación STEM es costosa computacionalmente, por lo que conviene fijar adecuadamente los siguientes parámetros.

| Factor | Impacto |
|--------|--------|
| **Ángulo de convergencia** | Mayor → más solapamiento de discos CBED → mayor coste |
| **Ondas de Bloch** | El coste del problema de valores propios escala como N³ |
| **Resolución angular** | Más fina → más precisa pero el coste escala como N² |
| **Píxeles de la imagen (Tamaño)** | Solo la síntesis final en el espacio real (píxeles × número de vectores $Q$); las etapas 1–4 no dependen de ellos |

---

## Importancia del factor de temperatura

Para la simulación HAADF-STEM, los átomos deben tener un factor de temperatura isótropo (factor de Debye-Waller) distinto de cero. Si el valor se desconoce, fija $B \approx 0.5\ \text{Å}^2$. Con un factor de temperatura nulo la intensidad TDS es cero y la imagen HAADF no se calcula correctamente.

| Detector | Rango | Contribución principal |
|----------|-------|-------------------|
| BF, ABF | Dentro del ángulo de convergencia | Elástica |
| LAADF, HAADF | Fuera del ángulo de convergencia | Inelástica (TDS) |

---

## Comparación con Dr. Probe

Se ha confirmado que las simulaciones STEM de ReciPro coinciden estrechamente con la ampliamente usada GUI de Dr. Probe (v1.10). La figura siguiente compara ambas para los detectores BF, ABF, LAADF y HAADF a lo largo de una serie de espesores (2.96–60.05 nm), tanto sin aberraciones (izquierda) como con Cs = 0.2 mm, desenfoque = −25.9 nm (derecha). Los dos códigos coinciden en todos los tipos de detector y espesores.

![Comparación de simulación STEM: Dr. Probe vs ReciPro](../../assets/references/STEM_DrProbe_comparison.png)

Hay disponible un informe más detallado en formato PDF: [Comparación de simulaciones STEM mediante la GUI de Dr. Probe (v1.10) y ReciPro (v4.854)](https://github.com/seto77/ReciPro/files/10976084/ComparisonSTEMsimulations.pdf).

---

## Comparación con py_multislice

Los mapas STEM-EDX de ReciPro también se han contrastado con [py_multislice](https://github.com/HamishGBrown/py_multislice), un código multicapa / fonón congelado independiente. La figura compara los mapas O-K, Ti-K y Sr-L de SrTiO₃ [001] a 200 kV en una serie de espesores (3,91–62,48 nm), sin aberraciones (izquierda) y con Cs = 0,2 mm, desenfoque = −25,9 nm (derecha).

![Comparación de simulaciones STEM-EDX: py_multislice frente a ReciPro](../../assets/references/STEM_EDX_pyms_comparison.png)

Las formas normalizadas de los mapas coinciden en 1–2 % para Ti-K y Sr-L en el límite delgado. Los **totales** difieren en ±10–17 % porque ambos códigos toman las secciones eficaces de ionización de fuentes distintas (Bote–Salvat en ReciPro, tablas del grupo Allen en py_multislice). La razón ReciPro / py_multislice también cae con el espesor, porque el modelo absortivo de ReciPro elimina los electrones dispersados térmicamente mientras que el fonón congelado los mantiene ionizando — lo que cuantifica el error práctico de la aproximación absortiva para EDX.

El informe completo, con las curvas cuantitativas y el análisis en frecuencia espacial, está disponible en PDF: [Comparación de simulaciones STEM-EDX por py_multislice y ReciPro (v4.945, conjunto de datos de ionización v3.0.0)](../../assets/references/STEM_EDX_pyms_comparison.pdf).

---

## Véase también

- [Simulador HRTEM/STEM (introducción)](index.md)
- [Simulación HRTEM](1-hrtem-simulation.md)
- [Simulación de potencial](3-potential-simulation.md)
- [Apéndice A3.4 — Cálculo STEM](../appendix/a3-bloch-wave/stem.md)
