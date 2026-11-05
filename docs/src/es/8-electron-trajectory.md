# Trayectorias electrónicas

El **Simulador de trayectorias (método de Monte Carlo)** calcula las trayectorias de los electrones dentro de una muestra mediante el **método de Monte Carlo**: los electrones incidentes experimentan dispersión elástica e inelástica, y se acumulan las distribuciones resultantes de los electrones retrodispersados (BSE): dirección, energía al escapar, profundidad de penetración y extensión lateral.
La [12. Simulación EBSD](12-ebsd-simulation.md) utiliza el mismo motor de Monte Carlo para su ponderación angular/de energía/de profundidad, pero lo ejecuta internamente con sus propios ajustes; los resultados de esta ventana no se le transmiten.

![Electron Trajectory](../assets/cap-es-auto/FormTrajectory.png)

La ventana tiene tres columnas: la **vista 3-D de trayectorias** a la izquierda, las **Estadísticas** y el estereograma de **Distribución de direcciones de BSE** en el centro, y tres **histogramas** a la derecha. La composición y la densidad de la muestra se toman del cristal seleccionado en la ventana principal; aquí solo se ajustan la energía del haz, la inclinación de la muestra y el número de trayectorias.

---

## Atajos de teclado y ratón

Las trayectorias se muestran en una vista 3-D de OpenGL. Utiliza la [navegación de vista](21-shortcuts.md) estándar de ReciPro, pero **el desplazamiento está deshabilitado**: utilice los botones de vista predefinida para saltar a las orientaciones estándar.

| Atajo | Acción |
|----------|--------|
| <kbd>F1</kbd> | Abrir esta página del manual en línea |
| Arrastrar con el botón izquierdo | Rotar el modelo |
| Arrastrar con el botón derecho arriba/abajo, o rueda del ratón | Zoom |
| <kbd>CTRL</kbd> + doble clic derecho | Alternar entre proyección ortográfica / en perspectiva |

→ Consulte **[21. Atajos de teclado y ratón](21-shortcuts.md)** para ver de un vistazo todas las ventanas.

---

## Condiciones de cálculo

Los controles de la parte superior de la ventana configuran la ejecución:

- **Simular trayectorias** : inicia la ejecución de Monte Carlo. La barra de estado de la parte inferior muestra por separado el tiempo empleado en el cálculo de trayectorias, en el dibujo de los gráficos y en la representación 3-D.
- **Number of trajectories** (100–1 000 000, por defecto 100 000) : cuántos electrones incidentes se siguen. Más electrones reducen el ruido estadístico de todas las distribuciones descritas más abajo, a costa de un tiempo de ejecución que crece linealmente.
- **Inclinación de la muestra** (°, de −90 a 0, por defecto 0) : inclinación de la superficie de la muestra alrededor del eje *X*. Déjela en 0 para incidencia normal; utilice **−70°** para reproducir la geometría del [simulador EBSD](12-ebsd-simulation.md), donde la gran inclinación aumenta el rendimiento de retrodispersión.
- **Energy** (keV) / **Wavelength** / **Unit** : el voltaje de aceleración del haz incidente y la longitud de onda del electrón asociada, con corrección relativista. La energía fija la energía cinética utilizada tanto por el modelo elástico (Mott del NIST) como por los inelásticos (poder de frenado / IMFP).

Cada electrón se sigue hasta que abandona la muestra o su energía cae a **2 keV** (umbral fijo). Los electrones que salen con más de 2 keV se cuentan como electrones retrodispersados; η (su fracción respecto de todos los electrones incidentes), la energía media de BSE, el estereograma y los histogramas descritos más abajo se calculan a partir de estos electrones. La sección eficaz, el recorrido libre medio y el poder de frenado de **Estadísticas** son valores del modelo a la energía del haz.

Los modelos de dispersión en sí no son seleccionables por el usuario: las secciones eficaces elásticas proceden de la tabla de Mott del NIST incluida (con Rutherford apantallado como alternativa fuera de su rango), y el poder de frenado de la forma de Jablonski modificada (2008). El modelo realmente utilizado se indica junto a la sección eficaz y al poder de frenado en **Estadísticas**. Consulte [Atenuación y transporte](appendix/a2-beam-interaction/attenuation-transport.md) para saber en qué consisten estos modelos.

### Vista 3-D de trayectorias

Las trayectorias de los electrones retrodispersados se dibujan en **rojo**, y las de los electrones absorbidos en la muestra, en **azul**; a lo largo de cada trayectoria el color se desvanece hacia blanco/transparente en 16 pasos a medida que el electrón pierde energía. La trayectoria tras el escape (véase más abajo) se dibuja como un breve segmento **naranja** cuya longitud es proporcional a la energía de escape. Los círculos guía concéntricos están rotulados en nm (o µm), y **+X**, **+Y**, **+Z (=beam)** marcan los ejes.

- **Desde el eje Z (=dirección del haz)** / **Desde el eje X (eje de rotación)** / **Normal a la superficie** : ajustan la vista a las direcciones estándar.
- **Número de trayectorias a dibujar** (1–100 000, por defecto 500) : cuántas de las trayectorias calculadas se representan (dibujar las 100 000 sería ilegible y lento). Se toman las primeras en el orden de cálculo, omitiendo los electrones absorbidos cuando estos no se dibujan.
- **Dibujar ejes** / **Dibujar círculos guía** : las flechas de los ejes y la escala de distancias.
- **Dibujar trayectorias absorbidas en la muestra** : incluye los electrones que nunca escapan.
- **Dibujar la trayectoria tras el escape** : sigue dibujando la trayectoria de un electrón retrodispersado después de que haya abandonado la superficie.

---

## Estadísticas

![Estadísticas](../assets/cap-es-auto/FormTrajectory.panel2.groupBoxStatistics.png)

Valores para la energía del haz actual. La sección eficaz y el poder de frenado llevan entre corchetes el nombre del modelo que los ha producido.

- **Sección eficaz de dispersión (σ_E)** (nm²): sección eficaz elástica total por átomo.
- **Recorrido libre medio elástico (λ)** (nm): distancia media entre eventos de dispersión elástica.
- **Poder de frenado (dE/ds)** (eV/nm, negativo): energía perdida por unidad de longitud de trayectoria.
- **Coeficiente de retrodispersión electrónica, η** (%): fracción de electrones incidentes que vuelven a salir por la superficie de entrada. Es la magnitud en la que se basa el contraste de las imágenes de BSE.
- **Energía media de BSE** (keV): energía media de los electrones retrodispersados en el momento en que escapan.

---

## Distribución de direcciones de BSE

![Distribución de direcciones de BSE](../assets/cap-es-auto/FormTrajectory.panel2.groupBoxDirectionDistribution.png)

Distribución angular de los electrones retrodispersados, dibujada sobre un estereograma cuyo centro es la dirección normal a la superficie.

- **Frecuencia** / **Energía media** / **Desviación estándar de energía** : la magnitud representada en color: cuántos electrones salen en cada dirección, su energía media o la dispersión de esa energía.
- **Dibujar ejes** : superpone las direcciones +X / ±Y / ±Z.
- **Min** / **Max**, **Resolution**, **Color** : los límites de la escala de color, el tamaño de clase angular del histograma y el mapa de color.

---

## Histogramas

![Histogramas](../assets/cap-es-auto/FormTrajectory.flowLayoutPanelProfiles.png)

Tres distribuciones de los electrones retrodispersados. Cada clase contiene la fracción de todos los electrones retrodispersados que caen en ella (los valores de las clases suman 1; no se dividen por la anchura de la clase).

### Distribución de energía de BSE al escapar

Histograma de la **energía que los electrones retrodispersados conservan todavía cuando abandonan la muestra** (keV), no de su pérdida de energía. El simulador EBSD pondera la integración en energía del master pattern con una distribución de este tipo, obtenida de su propia ejecución de Monte Carlo.

### Distancia máxima de BSE paralela a la superficie

Histograma de la distancia que cada electrón retrodispersado recorrió **lateralmente** (en paralelo a la superficie, nm) antes de escapar. Corresponde al tamaño lateral del volumen de interacción y, por tanto, al límite intrínseco de resolución espacial de una medida de BSE o EBSD.

### Profundidad máxima de penetración de BSE

Histograma de la mayor profundidad **perpendicular a la superficie** (nm) alcanzada por cada electrón retrodispersado antes de escapar.

---

## Véase también

- [Simulación EBSD](12-ebsd-simulation.md)
- [Cálculo EBSD](appendix/a3-bloch-wave/ebsd.md)
- [Atenuación y transporte](appendix/a2-beam-interaction/attenuation-transport.md): las secciones eficaces elásticas, el poder de frenado y los alcances utilizados aquí.
- [Difracción dinámica (onda de Bloch)](appendix/a3-bloch-wave/index.md)
- [Simulador HRTEM/STEM](9-hrtem-stem-simulator/index.md)
- [Simulador de difracción](7-diffraction-simulator/index.md)
