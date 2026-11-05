# Base de datos de cristales

La **Base de datos de cristales** ofrece funciones para buscar e importar estructuras cristalinas de dos fuentes, seleccionables mediante las casillas **AMCSD** y **COD**:

- **AMCSD** : la [American Mineralogist Crystal Structure Database](https://www.rruff.net/) incluida (más de 20.000 estructuras).
- **COD** : la [Crystallography Open Database](https://www.crystallography.net/cod/). Como el archivo es grande, no se incluye con el instalador; el archivo de la base de datos se descarga automáticamente en el primer uso. Cuando el archivo se actualiza en el servidor, se le solicita que lo descargue de nuevo.

Cite las siguientes referencias cuando utilice estas bases de datos.

Al utilizar **AMCSD**:

> Downs, R.T. and Hall-Wallace, M. (2003) The American Mineralogist Crystal Structure Database. *American Mineralogist* **88**, 247-250.

Al utilizar **COD**:

> Gražulis, S. et al. (2009) Crystallography Open Database – an open-access collection of crystal structures. *Journal of Applied Crystallography* **42**, 726-729.
>
> Gražulis, S. et al. (2012) Crystallography Open Database (COD): an open-access collection of crystal structures and platform for world-wide collaboration. *Nucleic Acids Research* **40**, D420-D427.

![Base de datos de cristales](../assets/cap-es-auto/FormCrystalDatabase.png)

---

## Atajos de teclado y ratón

Esta ventana no tiene combinaciones con teclas modificadoras; se maneja mediante clics ordinarios. Las únicas entradas no evidentes son:

| Atajo | Acción |
|----------|--------|
| <kbd>F1</kbd> | Abrir esta página del manual en línea |
| <kbd>ENTER</kbd> en cualquier campo de búsqueda | Ejecutar la búsqueda en la base de datos (equivale al botón **Buscar**) |
| Hacer clic en una fila de la tabla de resultados | Cargar ese cristal en la ventana principal |
| Hacer clic en un elemento de la ventana emergente **Tabla periódica** | Alternar su filtro: *puede incluir o no* → *debe incluir* → *debe excluir* |

→ Consulte **[21. Atajos de teclado y ratón](21-shortcuts.md)** para ver de un vistazo todas las ventanas.

---

## Tabla

![Tabla de la base de datos de cristales](../assets/cap-es-auto/FormCrystalDatabase.crystalDatabaseControl.png)

Muestra los cristales que coinciden con los criterios de búsqueda. Seleccione un cristal para transferirlo a la Información del cristal de la ventana principal. Pulse **Añadir** o **Reemplazar** para añadirlo a la Lista de cristales.

---

## Opciones de búsqueda

![Opciones de búsqueda](../assets/cap-es-auto/FormCrystalDatabase.panelSearch.searchCrystalControl.png)

Introduzca los criterios de búsqueda a continuación y pulse el botón **Buscar** o la tecla **Enter**.

| Criterio | Descripción |
|-----------|-------------|
| **Name** | Nombre del cristal |
| **Elementos** | Selector de tabla periódica (puede/debe/no debe incluir) |
| **Referencia** | Título, revista, autor |
| **Sistema cristalino** | Seleccionar sistema cristalino |
| **Parám. de celda** | Constantes reticulares y error |
| **Dist. interpl.** | Valores d de reflexiones intensas y error |
| **Densidad** | Densidad y error |

### Name

Coincidencia de texto libre con el nombre del cristal. Se permiten coincidencias parciales; no se distingue entre mayúsculas y minúsculas. Solo se aplican los criterios cuyas casillas están marcadas, y se combinan con Y lógico (AND).

### Element

Pulse el botón **Tabla periódica** para abrir el selector de elementos. Cada botón de elemento alterna entre tres estados:

- **puede incluir o no** (predeterminado: amarillo)
- **debe incluir** (azul)
- **debe excluir** (rojo)

Un cristal debe contener todos los elementos marcados como *debe incluir* y ninguno de los marcados como *debe excluir*; mientras el criterio **Elementos** está activo, se excluyen las entradas sin datos atómicos.

Los tres botones en la parte superior de la ventana restablecen cada elemento a uno de los tres estados con un solo clic.

### Reference

Coincidencia de texto libre (parcial, sin distinguir mayúsculas y minúsculas) con los metadatos de la publicación: título del artículo, nombre de la revista y lista de autores.

### Crystal system

Restringe la búsqueda a un sistema cristalino específico (cubic, tetragonal, orthorhombic, hexagonal, trigonal, monoclinic, triclinic). El primer elemento, *Unknown*, no aplica ninguna restricción.

### Búsqueda por parámetros de celda

![Búsqueda por parámetros de celda](../assets/cap-es-auto/FormCrystalDatabase.panelSearch.searchCrystalControl.flowLayoutPanel1.groupBoxCellParameter.png)

Introduzca las constantes reticulares objetivo *a*, *b*, *c* (Å) y *α*, *β*, *γ* (°) y los errores admisibles: una tolerancia relativa (±%) común a *a*, *b*, *c* y una tolerancia absoluta (±°) común a los ángulos. Un valor de 0 significa que ese parámetro se ignora.

### d-spacing

Introduzca hasta tres distancias interplanares *d* (Å; marque cada una para usarla), cada una con una tolerancia relativa (±%). Un cristal coincide cuando cada valor introducido concuerda con una de sus reflexiones más intensas almacenadas en la base de datos (hasta 16, ordenadas por intensidad de difracción de polvo de rayos X a λ = 1.54 Å). Con **Ignorar factor de dispersión** marcado, los valores se comparan en cambio con todas las distancias *d* geométricamente posibles calculadas a partir de la celda elemental (incluidas las reflexiones sistemáticamente ausentes). Útil cuando solo se conocen las posiciones de los picos de difracción de un experimento.

### Density

Filtra por densidad de masa (g/cm³) dentro de una tolerancia relativa (±%).

---

## Véase también

- [Ventana principal](0-main-window.md)
- [Información de simetría](2-symmetry-information.md)
- [Interacción del haz](3-beam-interaction.md)
- [Visor de estructura](5-structure-viewer.md)
