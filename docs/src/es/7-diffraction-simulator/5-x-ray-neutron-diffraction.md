# Simulación de difracción de rayos X / neutrones

La **simulación de difracción de rayos X / neutrones** calcula patrones de difracción de monocristal para rayos X y neutrones. Es uno de los modos principales del [simulador de difracción](index.md).

> Esta página enumera cada ajuste que aparece en el lado derecho cuando selecciona **Wave Length = X-ray** (o neutron). Para operaciones que afectan a toda la ventana, como dibujar y guardar, consulte la [página de descripción general](index.md).

Condiciones de la GUI: Wave Length = X-ray / Neutron · Incident beam = Parallel / Precession (X-ray) / Back-Laue · Intensity calculation = Only excitation error / Kinematical

![Simulador de difracción en modo rayos X / neutrones](../../assets/cap-es-auto/FormDiffractionSimulator-xray.png)

---

## Descripción general

Los rayos X tienen una longitud de onda mayor que los electrones (Cu Kα: 0.15406 nm = 1.5406 Å), por lo que la esfera de Ewald está más fuertemente curvada. Como resultado, menos puntos de la red recíproca satisfacen simultáneamente la condición de difracción que en el caso de los electrones. Dado que el poder de dispersión atómico es pequeño y la dispersión múltiple es débil, la teoría cinemática de la difracción ofrece una precisión suficiente para las intensidades (el cálculo dinámico solo se admite para electrones).

---

## Wave Length

![Wave Length](../../assets/cap-es-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelWaveLength.png)

Seleccione **X-ray** como fuente de radiación. Los rayos X pueden especificarse de dos maneras: rayos X característicos y radiación de sincrotrón.

### Rayos X característicos

Elegir un **elemento** y una **transición** fija la longitud de onda de los rayos X característicos. La transición se especifica en notación de Siegbahn (Kα₁ / Kα₂ / Kβ, etc.). Longitudes de onda Kα₁ de elementos representativos:

| Elemento | Línea | Longitud de onda (Å) | Energía (keV) |
|---------|------|-----------------|--------------|
| Cu | Kα₁ | 1.5406 | 8.048 |
| Mo | Kα₁ | 0.7093 | 17.479 |
| Co | Kα₁ | 1.7890 | 6.930 |
| Cr | Kα₁ | 2.2897 | 5.415 |

### Radiación de sincrotrón

Establezca **Element** en **0: Custom** e introduzca directamente la energía (keV) o la longitud de onda (Å). Puede utilizarse cualquier longitud de onda.

---

## Modo del haz incidente

![Beam mode](../../assets/cap-es-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelBeamMode.png)

Selecciona la geometría del haz incidente. Hay tres modos disponibles para los rayos X.

### Parallel

La onda plana estándar. Un haz incidente paralelo utilizado para SAED y difracción de rayos X de monocristal.

### Precession (X-ray) — cámara de precesión

Simula una cámara de precesión de rayos X. Se trata de una fotografía de precesión que captura una sola capa de la red recíproca.

### Back-Laue (Laue de retrorreflexión)

Simula un patrón de Laue de retrorreflexión con rayos X blancos (policromáticos). En esta geometría de retrorreflexión, el detector se coloca en el lado de la fuente y **Monochrome** se desactiva. Los puntos se proyectan sobre un detector plano **perpendicular al haz**, situado a la longitud de cámara en el lado de la fuente; la inclinación del detector **Tau / Phi** de [Detector geometry](index.md#detector-geometry) no se usa en esta proyección.

- El espectro blanco se corta en **50 keV**: se recogen reflexiones hasta $d = \lambda(50\ \text{keV})/2$, y no se aplica ninguna ponderación espectral.
- Los armónicos $n\mathbf{g}$ situados sobre la misma recta que pasa por el origen caen en el mismo punto; sus $\lvert F\rvert^2$ se suman sobre la reflexión de orden más bajo y las intensidades de los puntos se normalizan al punto más intenso.
- **Dispersión anómala (f', f'')** está desactivada (la energía no tiene un valor único), y **Radio del punto** no tiene unidad en este modo.

> **Nota**: Las opciones del haz incidente dependen de la longitud de onda. **Precession (electron)** y **Convergence (CBED)** aparecen solo cuando se selecciona radiación de electrones, mientras que las opciones **Precession (X-ray)** y **Back-Laue** anteriores aparecen solo cuando se selecciona radiación de rayos X. Para los neutrones, solo está disponible **Parallel**. Según el estado en el momento de la captura, la captura de pantalla puede no mostrar las opciones específicas de rayos X.

---

## Cálculo de la intensidad

![Intensity](../../assets/cap-es-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelIntensity.png)

Selecciona el método utilizado para calcular las intensidades de los puntos. Hay dos modos disponibles para los rayos X.

### Only excitation error

La intensidad se determina únicamente por la distancia geométrica entre la esfera de Ewald y el punto de la red recíproca (el error de excitación $S_g$). Es máxima en $S_g = 0$ —exactamente sobre la esfera— y decae a medida que crece $\lvert S_g\rvert$, llegando a cero cuando $\lvert S_g\rvert$ supera el **Radio del punto** indicado para el punto de la red recíproca. El factor de estructura se ignora.

Solo en este modo aparecen las opciones siguientes:

- **Excluir todas las reflexiones prohibidas** / **Excluir reflexiones prohibidas por centrado de red** (por defecto: solo centrado de red) : ocultan las reflexiones prohibidas por el grupo espacial. Las reflexiones que no se ocultan se dibujan con los colores independientes fijados en **Color del punto** (prohibidas por ejes helicoidales/planos de deslizamiento y prohibidas por la red).
- **Dibujar todos los puntos con el mismo radio** : dibuja todos los puntos con el tamaño correspondiente a $S_g = 0$, independientemente de su error de excitación.

### Cinemática & err. exc.

Además del error de excitación, el factor de estructura cinemático $\lvert F_{hkl} \rvert^2$ se incorpora a la intensidad. Las reglas de extinción se obedecen estrictamente. Los factores de Lorentz y de polarización no se incluyen (se trata de una simulación del patrón geométrico).

- **Dispersión anómala (f', f'')** (solo rayos X, activada por defecto) : añade al factor de estructura (también a los anillos de Debye) los términos de dispersión anómala f′, f″ a la energía de rayos X actual. En una estructura no centrosimétrica que contenga dispersores anómalos químicamente distintos, la ley de Friedel deja entonces de cumplirse y las intensidades de $hkl$ / $\bar h\bar k\bar l$ difieren (pares de Bijvoet); el efecto es máximo cerca de un borde de absorción. Desactivada en Back-Laue. Consulte [Factor de estructura](../appendix/a2-beam-interaction/structure-factor.md).

> **Nota**: La **teoría dinámica** está deshabilitada para los rayos X (disponible solo cuando se selecciona radiación de electrones).

---

## Apariencia de los puntos

![Appearance](../../assets/cap-es-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelAppearance.png)

Controla cómo se renderiza cada punto de difracción.

- **Esfera sólida / Gaussiana** : modelo geométrico del punto de la red recíproca. **Esfera sólida** dibuja la sección transversal entre una esfera y la esfera de Ewald; con **Cinemática & err. exc.** el radio de la esfera es $R \times I_\text{rel}^{1/3}$ (de modo que el *volumen* de la esfera es proporcional a la intensidad relativa), y con **Solo error de exc.** es el propio *R*. **Gaussiana** utiliza la sección transversal entre una gaussiana 3D con σ = *R* y la esfera de Ewald (la integral de la gaussiana 2D corresponde a la intensidad de difracción); los puntos con $\lvert S_g\rvert \ge 3R$ no se dibujan.
- **Opacidad** : transparencia del punto (0 = transparente, 1 = opaco).
- **Radio del punto (R)** : radio del punto de la red recíproca. El tamaño renderizado del punto se determina por la combinación de **Apariencia** y **Cálculo de intensidad**.
- **Brillo** : activo solo en el modo **Gaussiana**. Establece la intensidad integrada de la gaussiana renderizada.
- **Escala de color** (solo Gaussiana) : **Gray**, **Cold-Warm**, **Spectrum** o **Fire**.
- **Escala log.** (solo Gaussiana) : muestra las intensidades en una escala logarítmica.
- **Color del punto** : colores de los puntos usados con la escala de color **Gray** (este grupo se oculta con las demás escalas de color).
- **Color por cristal** : cuando está marcada, dibuja los puntos con el color asignado a cada cristal.

---

## Anillos de Debye (policristalino)

Pueden mostrarse los anillos de Debye de una muestra policristalina. Active **Anillos de Debye** en la barra de herramientas (consulte [Barra de herramientas](index.md#toolbar)).

- **Ignorar la intensidad de difracción** : dibuja todos los anillos con el mismo color e intensidad (se utiliza para una comparación puramente geométrica que ignora el factor de estructura).
- **Mostrar etiquetas de índice** : muestra el índice (*hkl*) cerca de cada anillo.

Los ajustes detallados se encuentran en la pestaña Debye rings del [menú de pestañas](index.md#drawing-overlay-tabs).

---

## Difracción de neutrones

Seleccionar **Neutron** en el control Wave Length calcula un patrón de difracción de neutrones. Introduzca la energía (meV) o la longitud de onda (Å o nm, conmutable; por defecto Å). El haz incidente solo puede ser **Parallel**. El cálculo de la intensidad puede ser **Only excitation error** o **Cinemática & err. exc.** (**Teoría dinámica** no está disponible). La intensidad cinemática utiliza la longitud de dispersión de neutrones en lugar del factor de dispersión atómico.

---

## Diferencias entre la difracción de rayos X y de electrones

| Característica | Difracción de rayos X | Difracción de electrones |
|---------|-------------------|----------------------|
| Longitud de onda | Larga (0.5–2.5 Å) | Corta (0.02–0.04 Å) |
| Curvatura de la esfera de Ewald | Grande | Pequeña (casi plana) |
| Reflexiones simultáneas | Pocas | Muchas |
| Factor de dispersión | Factor de dispersión atómico $f(s)$ | Factor de dispersión de electrones $f_e(s)$ |
| Efectos dinámicos | Generalmente pequeños | Grandes |
| Reglas de extinción | Estrictamente obedecidas | Pueden ser violadas por la dispersión múltiple |

---

## Operaciones comunes

Para operaciones que afectan a toda la ventana, como la longitud de cámara, la geometría del detector, el guardado de patrones y los ajustes de color, consulte la [página de descripción general](index.md). La geometría detallada del detector se configura en la ventana de geometría que se muestra a continuación.

![Ajustes de geometría del detector](../../assets/cap-es-auto/FormDiffractionSimulatorGeometry.png)

---

## Véase también

- [Simulador de difracción (descripción general)](index.md)
- [Simulación SAED](1-saed-simulation.md)
- [Simulación de difracción de electrones por precesión (PED)](2-ped-simulation.md)
- [Simulación de difracción de electrones de haz convergente (CBED)](3-cbed-simulation.md)
- [Sistema de coordenadas — orientación del cristal](../appendix/a1-coordinate-system/1-orientation.md)
