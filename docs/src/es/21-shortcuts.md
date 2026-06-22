# Atajos de teclado y ratón

ReciPro asocia muchas funciones a **combinaciones de teclas** y a **botones del ratón combinados con teclas modificadoras** — cosas que no son visibles en un botón ni en un menú. Esta página las recopila todas en un solo lugar. La página de cada ventana también repite sus atajos cerca del principio.

<kbd>F1</kbd> funciona en **cada** ventana y abre la página de esa ventana en este manual en línea.

---

## Atajos para toda la aplicación

Estos se instalan desde la [ventana principal](0-main-window.md), pero permanecen activos mientras las ventanas Visor de estructura, Estereograma, Simulador de difracción, Spot ID y Calculadora tienen el foco.

| Atajo | Acción |
|----------|--------|
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>D</kbd> | Activar/desactivar el Simulador de difracción |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>V</kbd> | Activar/desactivar el Visor de estructura |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>S</kbd> | Activar/desactivar el Estereograma |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>T</kbd> | Activar/desactivar Spot ID |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd> + teclas de flecha | Girar el cristal un paso en esa dirección (mantenga dos flechas para una diagonal) |
| Doble pulsación de <kbd>CTRL</kbd> | Activar/desactivar la Calculadora |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>R</kbd> | Conmutar el indicador *Reserved* del cristal seleccionado |
| <kbd>CTRL</kbd>+<kbd>ALT</kbd>+<kbd>SHIFT</kbd>+<kbd>C</kbd> | Hacer una captura de pantalla de la GUI (herramienta para desarrolladores; active primero **Capture GUI Components**) |
| Mantener <kbd>CTRL</kbd> mientras se inicia ReciPro | Iniciar con OpenGL desactivado (recuperación ante problemas gráficos) |

---

## Modelos de interacción compartidos

Casi toda vista interactiva en ReciPro pertenece a una de tres familias. Conocer la familia indica el comportamiento de arrastre/zoom sin tener que memorizar cada ventana.

### Vistas 3-D de OpenGL { #3d }

Usadas por el [Visor de estructura](5-structure-viewer.md), la [Geometría de rotación](4-rotation-geometry.md), la esfera 3-D del [Estereograma](6-stereonet.md), las [Trayectorias electrónicas](8-electron-trajectory.md) y las vistas de geometría / master-pattern de [EBSD](12-ebsd-simulation.md).

| Acción | Resultado |
|--------|--------|
| Arrastrar con el botón izquierdo | Rotar — trackball cerca del centro, giro en el plano cerca del borde |
| Arrastrar con el botón derecho arriba/abajo, o rueda del ratón | Zoom |
| Arrastrar con el botón central | Desplazar (solo donde está habilitado) |
| <kbd>CTRL</kbd> + arrastrar con el botón derecho arriba/abajo | Cambiar la distancia de la cámara (solo en modo perspectiva) |
| <kbd>CTRL</kbd> + doble clic derecho | Alternar entre proyección ortográfica / perspectiva |

Algunas ventanas pueden desactivar el desplazamiento o el zoom (por ejemplo, en las Trayectorias electrónicas y en las vistas 3-D de EBSD el desplazamiento está desactivado).

### Vistas de patrón de difracción { #pattern }

Usadas por el patrón del [Simulador de difracción](7-diffraction-simulator/index.md), el patrón de Kikuchi de [EBSD](12-ebsd-simulation.md) y el [Estereograma](6-stereonet.md) 2-D. La diferencia clave respecto a las vistas 3-D: **arrastrar rota el propio cristal**, no solo la cámara, de modo que cada ventana vinculada se actualiza a la vez.

| Acción | Resultado |
|--------|--------|
| Arrastrar con el botón izquierdo cerca del centro | Inclinar el cristal |
| Arrastrar con el botón izquierdo en la zona exterior | Girar el cristal alrededor del eje de visión/haz |
| Clic derecho | Alejar el zoom |
| Arrastrar un recuadro con el botón derecho | Acercar el zoom a la región seleccionada |
| Arrastrar con el botón central | Desplazar |

En estas vistas **no** hay zoom con la rueda del ratón.

### Vistas de imagen { #image }

Usadas por los paneles de resultados de [HRTEM/STEM](9-hrtem-stem-simulator/index.md), la imagen de [Spot ID v2](11-spot-id-v2.md) y el master pattern 2-D de [EBSD](12-ebsd-simulation.md).

| Acción | Resultado |
|--------|--------|
| Arrastrar con el botón izquierdo / central | Desplazar |
| Rueda del ratón arriba / abajo | Acercar el zoom (×2) / alejarlo (×0.5) en el cursor |
| Arrastrar un recuadro con el botón derecho | Acercar el zoom a la región seleccionada |
| Clic derecho / doble clic derecho | Alejar el zoom (×0.5) |

---

## Referencia por ventana

### 0. Ventana principal
[Abrir página →](0-main-window.md) · además de los atajos para toda la aplicación anteriores.

| Atajo | Acción |
|----------|--------|
| Arrastrar con el botón izquierdo el widget de orientación (abajo a la izquierda) | Rotar el cristal |
| Doble clic derecho en el widget de orientación | Copiar la imagen del widget al portapapeles |
| Un clic / doble clic en un botón de función | Activar/desactivar esa ventana / forzarla al primer plano |
| Clic derecho en un cristal de la lista | Menú contextual (Renombrar / Duplicar / Eliminar / Exportar CIF…) |
| Doble clic en la etiqueta **Current Index** | Mostrar / ocultar el cuadro max-UVW |
| Soltar un archivo | Cargar una lista de cristales (`.xml`, `.cdb2`) o un cristal (`.cif`, `.amc`) |

### 1. Base de datos de cristales
[Abrir página →](1-crystal-database.md)

| Atajo | Acción |
|----------|--------|
| <kbd>ENTER</kbd> en un campo de búsqueda | Ejecutar la búsqueda |
| Clic en una fila de resultados | Cargar ese cristal |
| Clic en un elemento del popup de tabla periódica | Recorrer su filtro: ignorar → debe incluir → debe excluir |

### 2. Información de simetría · 3. Interacción del haz
La Información de simetría no tiene combinaciones especiales de teclas/ratón. En la Interacción del haz, además de <kbd>F1</kbd> y los botones **Copy**, el cursor vertical del gráfico **Scattering factors** se puede arrastrar para leer el valor de cada elemento.
[Simetría →](2-symmetry-information.md) · [Interacción del haz →](3-beam-interaction.md)

### 4. Geometría de rotación
[Abrir página →](4-rotation-geometry.md) — seis [vistas 3-D](#3d) **vinculadas**; rotar cualquiera de ellas rota las seis a la vez. Las pequeñas vistas *Axes* / *Objects* tienen el zoom y el desplazamiento desactivados.

### 5. Visor de estructura
[Abrir página →](5-structure-viewer.md) — la vista principal es una [vista 3-D](#3d).

| Atajo | Acción |
|----------|--------|
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>C</kbd> | Copiar la imagen renderizada al portapapeles |
| Doble clic izquierdo en un átomo | Mostrar coordenadas, distancias a los vecinos más próximos y ángulos de enlace |
| Arrastrar con el botón izquierdo el gizmo de ejes cristalográficos | Rotar el modelo (sin giro en el plano) |
| Arrastrar con el botón izquierdo el gizmo de luz | Cambiar la dirección de iluminación |

### 6. Estereograma
[Abrir página →](6-stereonet.md) — la red 2-D es una [vista de patrón de difracción](#pattern); la esfera 3-D opcional es una [vista 3-D](#3d).

| Atajo | Acción |
|----------|--------|
| Doble clic izquierdo en la red | Alternar entre proyección **Plane** y **Axis** |
| Mover el ratón sobre la red | Leer el (hkl)/[uvw] bajo el cursor |

### 7. Simulador de difracción
[Abrir página →](7-diffraction-simulator/index.md) — el patrón es una [vista de patrón de difracción](#pattern) (sin zoom con rueda).

| Atajo | Acción |
|----------|--------|
| Doble clic izquierdo en una reflexión | Mostrar los detalles de la reflexión (índice, *d*, factor de estructura, error de excitación) |
| <kbd>CTRL</kbd> + arrastrar con el botón central | Mover el centro del detector (cuando se muestra el área del detector) |
| Doble clic derecho en la barra de estado | Copiar un resumen de texto de los ajustes actuales |
| Doble clic derecho en un botón de capa activa (Spots / Kikuchi / Debye / Scale) | Hacer parpadear esa capa |
| Doble clic izquierdo en el estereograma — ventana **TEM holder** | Fijar la inclinación del portamuestras en ese punto |
| Teclas de flecha — ventana **TEM holder** | Avanzar la inclinación del portamuestras por pasos (marque primero **Arrow keys**) |
| Soltar `.prm` / imagen — **Detector geometry**, o `.txt` — **Dynamic compression** | Cargar esos datos |

### 8. Trayectorias electrónicas
[Abrir página →](8-electron-trajectory.md) — una [vista 3-D](#3d) con el desplazamiento desactivado.

### 9. Simulador HRTEM / STEM
[Abrir página →](9-hrtem-stem-simulator/index.md) — los paneles de resultados son [vistas de imagen](#image) y se desplazan/escalan juntos.

| Atajo | Acción |
|----------|--------|
| <kbd>CTRL</kbd>+<kbd>C</kbd> (con la cuadrícula de imágenes enfocada) | Copiar la(s) imagen(es) al portapapeles como metarchivo |
| <kbd>CTRL</kbd> + arrastrar un recuadro con el botón derecho | Seleccionar un área rectangular |
| Doble clic izquierdo en un panel | Maximizar ese panel / restaurar la cuadrícula (disposiciones de varios paneles) |

### 10. Spot ID v1
[Abrir página →](10-spot-id.md) — la imagen es solo de referencia (no interactiva).

| Atajo | Acción |
|----------|--------|
| Doble clic en una fila de la lista de resultados | Seleccionar ese cristal y rotarlo al eje de zona correspondiente |

### 11. Spot ID v2
[Abrir página →](11-spot-id-v2.md) — la imagen es una [vista de imagen](#image) con edición de spots superpuesta.

| Atajo | Acción |
|----------|--------|
| Doble clic izquierdo en la imagen | Añadir un spot (ajustado al pico) |
| <kbd>CTRL</kbd> + doble clic izquierdo | Añadir un spot y marcarlo como el haz directo (000) |
| Clic izquierdo en un spot | Seleccionar el spot más cercano |
| <kbd>CTRL</kbd> + clic derecho en un spot | Eliminar el spot más cercano |
| <kbd>CTRL</kbd> + teclas de flecha | Desplazar el spot seleccionado un píxel |
| Doble clic en el encabezado de fila de un spot | Acercar el zoom a ese spot (×2) |

### 12. Simulación EBSD
[Abrir página →](12-ebsd-simulation.md) — el patrón de Kikuchi es una [vista de patrón de difracción](#pattern); las vistas 3-D son [vistas 3-D](#3d) (desplazamiento desactivado); el master pattern 2-D es una [vista de imagen](#image).

| Atajo | Acción |
|----------|--------|
| Doble clic en el patrón de Kikuchi | Elegir la subcelda del detector bajo el cursor y mostrar sus estadísticas |

### 20. Macro
[Abrir página →](20-macro/index.md)

| Atajo | Acción |
|----------|--------|
| <kbd>CTRL</kbd>+<kbd>S</kbd> | Guardar el texto del editor de vuelta en la entrada seleccionada de la lista de macros |
| <kbd>F10</kbd> | Avanzar un paso (durante la ejecución paso a paso) |
| Doble clic en una fila de la lista de ayuda de funciones | Insertar la firma de esa función en el cursor |
| Soltar un archivo `.mcr` | Cargarlo en el editor |
