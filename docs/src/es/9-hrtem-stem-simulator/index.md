---
title: HRTEM / STEM Simulator
---

# HRTEM / STEM Simulator

El **Simulador HRTEM/STEM** simula imágenes de franjas de red en TEM (HRTEM), imágenes STEM y potenciales cristalinos proyectados para el cristal y la orientación seleccionados. Haga clic en **Simular** para ejecutar el cálculo.

![Simulador HRTEM/STEM](../../assets/cap-es-auto/FormImageSimulator.png)

La ventana está dividida en dos mitades. El **lado izquierdo** muestra el resultado de la simulación y controla su aspecto (paneles de imagen, brillo, color, barra de escala, etc.); el **lado derecho** contiene las condiciones de cálculo (**Propiedades ópticas** y **Ajustes de simulación**).

---

## Esta página y las páginas de cada modo

- **Esta página (introducción)**: las operaciones comunes a todos los modos, junto con los **controles de visualización y ajuste del resultado del lado izquierdo**.
- **Páginas de cada modo**: todos los ajustes que aparecen en el **lado derecho** en ese modo, descritos de forma que cada página sea autónoma (por eso algunos ajustes aparecen en más de una página).

| Modo | Contenido | Página |
|------|----------|------|
| **HRTEM** | Imágenes de franjas de red de TEM de alta resolución | [Simulación HRTEM](1-hrtem-simulation.md) |
| **STEM** | Imágenes de microscopio electrónico de transmisión de barrido (BF / ABF / LAADF / HAADF) | [Simulación STEM](2-stem-simulation.md) |
| **Potential** | Potencial cristalino proyectado ($U_g$ / $U'_g$) | [Simulación de potencial](3-potential-simulation.md) |

---

## Atajos de teclado y ratón

Los resultados se muestran como uno o varios paneles de imagen. Utilizan la [navegación estándar de vista de imagen](../21-shortcuts.md) de ReciPro, y todos los paneles se desplazan y se acercan o alejan de forma conjunta.

| Atajo | Acción |
|----------|--------|
| <kbd>F1</kbd> | Abrir esta página del manual en línea |
| <kbd>CTRL</kbd>+<kbd>C</kbd> (rejilla de imágenes enfocada) | Copiar la imagen o las imágenes al portapapeles como metarchivo |
| Arrastrar con el botón izquierdo / central | Desplazar la imagen (todos los paneles se mueven juntos) |
| Rueda del ratón arriba / abajo | Acercar (×2) / alejar (×0.5) en la posición del cursor |
| Arrastrar un rectángulo con el botón derecho | Acercar a la región seleccionada |
| Clic derecho / doble clic derecho | Alejar (×0.5) |
| <kbd>CTRL</kbd> + arrastrar un rectángulo con el botón derecho | Seleccionar un área rectangular |
| Doble clic izquierdo sobre un panel | Maximizar ese panel / restaurar la rejilla (diseños de varios paneles) |
| Mover el ratón (sin botón) | Leer la posición (pm) y el valor de píxel en la posición del cursor |

→ Consulte **[21. Atajos de teclado y ratón](../21-shortcuts.md)** para tener una visión general de cada ventana.

---

## Rutas rápidas según el objetivo

| Objetivo | Punto de partida | Referencia |
|------|------------|-----------|
| Calcular una imagen HRTEM | Establezca **Modo de imagen** en **HRTEM** y, a continuación, ajuste el voltaje de aceleración y el desenfoque en **Condiciones TEM** | [Simulación HRTEM](1-hrtem-simulation.md), [Formación de la imagen HRTEM](../appendix/a3-bloch-wave/hrtem.md) |
| Calcular una imagen STEM | Establezca **Modo de imagen** en **STEM** y, a continuación, ajuste el ángulo de convergencia y el detector en **Opciones STEM** | [Simulación STEM](2-stem-simulation.md), [Cálculo STEM](../appendix/a3-bloch-wave/stem.md) |
| Ver el potencial proyectado | Establezca **Modo de imagen** en **Potential** | [Simulación de potencial](3-potential-simulation.md) |
| Generar una serie de espesor / desenfoque | En HRTEM, configure **Modo único/en serie** y las condiciones de imagen | [Simulación HRTEM](1-hrtem-simulation.md) |
| Usar HAADF-STEM con TDS | Establezca factores de temperatura atómicos distintos de cero y lleve el detector STEM a LAADF / HAADF | [Cálculo STEM](../appendix/a3-bloch-wave/stem.md) |

---

## Flujo de trabajo básico

1. Seleccione el cristal y la orientación en la ventana principal y, a continuación, abra esta ventana.
2. Elija HRTEM, STEM o Potential en **Modo de imagen**.
3. Ajuste el voltaje de aceleración, el desenfoque, las aberraciones, el diafragma, el ángulo de convergencia STEM, etc. en **Propiedades ópticas** (véanse las páginas de cada modo).
4. Ajuste el espesor, el tamaño de imagen, la resolución, el número de ondas de Bloch, el modelo de coherencia parcial, etc. en **Ajustes de simulación** (véanse las páginas de cada modo).
5. Haga clic en **Simular** y, si es necesario, ajuste el aspecto con **Ajuste**, **Normalización** y **Visualización** en el lado izquierdo.

---

## Selección del modo de imagen

![Modo de imagen](../../assets/cap-es-auto/FormImageSimulator.splitContainer1.flowLayoutPanelModeSelection.groupBoxImageMode.png){ align=left }

**Modo de imagen**, en la parte superior derecha, selecciona el tipo de cálculo. Los paneles de la derecha (**Propiedades ópticas** y **Ajustes de simulación**) cambian según el modo elegido.<div style="clear: both;"></div>

- **HRTEM**: imágenes de franjas de red de TEM de alta resolución → [Simulación HRTEM](1-hrtem-simulation.md)
- **STEM**: imágenes de microscopio electrónico de transmisión de barrido → [Simulación STEM](2-stem-simulation.md)
- **Potential**: potencial cristalino proyectado → [Simulación de potencial](3-potential-simulation.md)

---

## Área de imagen (lado izquierdo)

La mitad izquierda de la ventana muestra la imagen simulada. La barra de estado de la parte superior indica la posición del cursor (**X:**, **Y:**) y el **Valor:** (intensidad) de la imagen bajo el cursor, junto a una escala de intensidad **Bajo → Alto** que refleja el mapa de color y el rango de brillo actuales.

Cuando se generan varias imágenes (una imagen en serie, o la magnitud/fase de un potencial) se disponen en una rejilla, y todos los paneles se acercan, alejan y desplazan de forma conjunta.

---

## Visualización y ajuste de los resultados (panel izquierdo) {#display-settings}

El panel de la parte inferior izquierda ajusta el aspecto del resultado: brillo, color, normalización y superposiciones. Estos ajustes son comunes a todos los modos y se aplican sin necesidad de recalcular.

### Ajuste

![Ajuste](../../assets/cap-es-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxAdjust.png)

- **Min** / **Max** : extremos inferior (negro) y superior (blanco) del rango de intensidad mostrado. Utilice las barras deslizantes para ajustar el contraste.
- **Color** : escala de color de la imagen: **Gray scale** o **Cold-Warm** (de azul a rojo).
- **Desenfoque gaussiano (FWHM)** : si está marcado, aplica un desenfoque gaussiano con la anchura a media altura (pm) indicada a la derecha, lo que aproxima una resolución finita (función de dispersión de punto).

### Normalización

![Normalización](../../assets/cap-es-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxNormalization.png)

- **Por imagen** : si está marcado, normaliza cada imagen por separado (si no lo está, toda la serie comparte una escala común).
- **Min** / **Max** : fija el extremo inferior / superior de la normalización en el valor indicado a la derecha, en lugar del mínimo / máximo de la imagen.
- **EDX común** : solo se muestra mientras se visualiza un [mapa EDX](2-stem-simulation.md#stem-edx) y la ejecución ha producido más de un canal; pone todos los canales de la misma magnitud en un mismo rango de visualización. Los mapas EDX nunca se reescalan con **Min** / **Max**: su extremo inferior está fijo en 0 y solo el superior sigue **Por imagen** / **EDX común**.
- Este grupo se oculta en el modo Potential.

### Imagen STEM

![Imagen STEM](../../assets/cap-es-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxSTEMoption3.png)

Solo se muestra en modo STEM. Selecciona qué componente de dispersión de la imagen STEM calculada se muestra (**Elástico**, **TDS** o **Elástico & TDS**). Si en la misma ejecución también se calcularon [mapas STEM-EDX](2-stem-simulation.md#stem-edx), **EDX** aparece como cuarta opción. Como es específico de STEM, también se describe en la página [Simulación STEM](2-stem-simulation.md).

### Visualización

![Visualización](../../assets/cap-es-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxDisplay.png)

Establece los elementos superpuestos a la imagen.

- **Celda** : superpone el contorno de la celda unidad proyectada, lo que permite relacionar el contraste de la imagen con la red cristalina. Las aristas paralelas a **a**, **b** y **c** se dibujan en rojo, verde y azul, centradas en el centro de la imagen.
- **Texto** : superpone la etiqueta de cada panel: el espesor `t=` y el desenfoque `f=` (nm) en HRTEM / STEM, o el nombre del panel (por ejemplo *Ug magnitude*) en el modo Potential. Se pueden especificar el **Tamaño** (de letra) y el **Color**.
- **Escala** : superpone una barra de escala. Se pueden especificar la **Long.** (nm) y el **Color**.

---

## Ejecución de la simulación

![Acciones de simulación](../../assets/cap-es-auto/FormImageSimulator.splitContainer1.panelSimulationActions.png)

- **Simular** : ejecuta el cálculo con el cristal, las condiciones del microscopio, el espesor, el desenfoque y los ajustes de visualización actuales.
- **Detener** : interrumpe el cálculo en curso (solo se muestra durante el cálculo).
- **Tiempo real** : si está marcado, recalcula inmediatamente al girar el cristal (oculto en modo STEM).
- **Predefinidos** : muestra u oculta la ventana de ajustes predefinidos, que guarda y recupera condiciones de imagen TEM (véase [Preset list](#preset-list) más abajo).

### Preset list {#preset-list}

La ventana **Preset list** guarda instantáneas con nombre de los ajustes del simulador. Una instantánea guarda el modo de imagen, el voltaje de aceleración, Cs, Cc, ΔV, el desenfoque, β, la apertura de objetivo (tamaño y desplazamiento), el modelo de coherencia parcial de HRTEM, el espesor y las listas de espesores / desenfoques en serie, el tamaño y la resolución de la imagen, el número de ondas de Bloch y los ajustes STEM (ángulo de convergencia, ángulos del detector, tamaño de la fuente, espesor de capa, resolución angular, **Calcular mapas EDX** y **Series de líneas (Kα, Lα…)**). La lista se guarda con la configuración de ReciPro y se conserva tras reiniciar.

- Al hacer clic en una entrada, se aplica inmediatamente.
- **OK** cierra la ventana y conserva los ajustes actuales; **Cancelar** (o <kbd>Esc</kbd>) la cierra y restaura los ajustes vigentes cuando se abrió la ventana.
- **Gestionar la lista de preajustes** : si está marcado, al hacer clic en una entrada solo se selecciona (no se aplica) y aparecen la casilla **Nombre** y los botones **Add** / **Reemplazar** / **Cambiar nombre** / **Eliminar**. **Add** añade los ajustes actuales con el nombre indicado, **Reemplazar** sobrescribe la entrada seleccionada con los ajustes actuales y **Cambiar nombre** cambia solo su nombre.

Los ajustes predefinidos de microscopio (ARM300F, 2100F, …) no están en esta lista; se encuentran en el menú contextual de **Condiciones TEM** (véase [Simulación HRTEM](1-hrtem-simulation.md#tem-conditions)).

---

## Menú Archivo

![Menú Archivo](../../assets/cap-es-auto/FormImageSimulator.menuStrip1.fileToolStripMenuItem.png)

- **Guardar imagen** : guarda **como Imagen (formato PNG)**, **como Imagen (formato TIFF)** o **como metarchivo (EMF)**. PNG y EMF guardan lo que se muestra (rango de visualización y escala de color); **Guardar individualmente en modo de imagen en serie** (activado por defecto) escribe cada panel en su propio archivo en lugar de una sola imagen en mosaico. TIFF escribe siempre un archivo de coma flotante por panel, sin superposiciones. Guarda los valores de píxel después de la **Normalización** (y del **Desenfoque gaussiano (FWHM)**, si se aplica), no el rango de visualización de **Ajuste**; con los valores por defecto (**Min** 0, **Max** 1, **Por imagen**) cada imagen se escala a 0–1, así que desmarque **Min** / **Max** para exportar las intensidades calculadas. Los mapas EDX se escriben siempre sin escalar.
- **Copiar imagen** : copia al portapapeles **como Imagen** o **como metarchivo (EMF)**.
- **Sobreimprimir símbolos** (activado por defecto) : incrusta la celda unidad, las etiquetas y la barra de escala en la imagen PNG / EMF guardada o copiada.
- **Cargar parámetros TEM** / **Guardar parámetros TEM** : se muestran pero están desactivados (no implementados). Use la [Preset list](#preset-list) para guardar y recuperar condiciones.

## Menú Ayuda

![Menú Ayuda](../../assets/cap-es-auto/FormImageSimulator.menuStrip1.helpToolStripMenuItem.png)

- **Concepto básico de la simulación HRTEM** : abre la explicación de la formación de la imagen HRTEM ([Apéndice A3.2](../appendix/a3-bloch-wave/hrtem.md)).
- **Biblioteca de cálculo** : selecciona la biblioteca de cálculo: **Native code** (C++/Eigen, rápida) o **Managed code** (.NET). Native suele ser más rápida. La elección solo se usa para la síntesis de la imagen HRTEM; STEM selecciona su solver automáticamente (STEM-EDX siempre necesita la biblioteca nativa), y la elección vuelve a **Native code** cada vez que se inicia ReciPro.

---

## Véase también

- [Simulación HRTEM](1-hrtem-simulation.md)
- [Simulación STEM](2-stem-simulation.md)
- [Simulación de potencial](3-potential-simulation.md)
- [Difracción dinámica (onda de Bloch)](../appendix/a3-bloch-wave/index.md)
- [Simulador de difracción](../7-diffraction-simulator/index.md)
- [Trayectorias electrónicas](../8-electron-trajectory.md)
