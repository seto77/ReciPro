# Apéndice A4. Simetría y grupos espaciales

El capítulo de la ventana principal [2. Información de simetría](../../2-symmetry-information.md) es una guía de la GUI: indica qué pestaña muestra qué cosa y qué botón copia qué diagrama. Este apéndice reúne los **fundamentos cristalográficos y de teoría de grupos** que hay detrás de esas tablas e imágenes: qué codifica realmente un símbolo de Hermann–Mauguin, cómo leer los diagramas de elementos de simetría y de posiciones generales al estilo de las *International Tables for Crystallography* (ITA) Vol. A, y qué significan las tablas de supergrupos/subgrupos y la terminología (*translationengleiche*, *klassengleiche*, clase de conjugación, dominios, leyes de macla, …) de la ventana **Relaciones de grupo…**.

![Información de simetría](../../../assets/cap-es-auto/FormSymmetryInformation.png)

Se tratan dos ventanas, y la teoría se lee mejor en este orden:

1. **[A4.1. Símbolos de grupos espaciales y diagramas de simetría](symbols-and-diagrams.md)** — los símbolos de Hermann–Mauguin, Schoenflies y Hall; la clasificación según la teoría de grupos que muestra la pestaña **Propiedades** (centrosimétrico, Sohncke, simórfico, polar, clase cristalina aritmética, simetría de Patterson, …); la descripción de cada operación de simetría de la pestaña **Operaciones** mediante triplete de coordenadas/símbolo de Seitz/tipo geométrico; y las convenciones gráficas de los diagramas de elementos de simetría y de posiciones generales de la parte inferior de la ventana [Información de simetría](../../2-symmetry-information.md).
2. **[A4.2. Relaciones grupo-subgrupo](group-subgroup-relations.md)** — qué es un *subgrupo maximal* / *supergrupo minimal*, la distinción *t*-/*k*- de Hermann, y cómo leer cada pestaña del explorador **Relaciones de grupo…** (Diagrama, Matriz, División de órbita, Dominios y maclas, Nuevas reflexiones) que se abre desde el panel **Opciones** de Información de simetría.

A4.1 va primero porque A4.2 se remite a él constantemente: cada relación de subgrupo/supergrupo se etiqueta con los mismos símbolos de Hermann–Mauguin, símbolos de Seitz y expresiones de tipo geométrico (*"3-fold rotation"*, *"c-glide plane"*, *"screw axis"*, …) que allí se introducen.

---

## Alcance y fuentes

La base de datos integrada de ReciPro cubre los 230 tipos de grupos espaciales (con 530 configuraciones/elecciones de origen tabuladas) exactamente como están tabulados en las *International Tables for Crystallography*, **Volume A** (simetría de los grupos espaciales) y **Volume A1** (subgrupos maximales de los grupos espaciales). Este apéndice explica la *presentación* de esos datos en ReciPro — la notación, los diagramas, la herramienta de exploración — y da por sentado que el lector ya posee una familiaridad de nivel de grado con las redes, los grupos puntuales y la idea de operación de simetría. No sustituye a las propias ITA, que siguen siendo la referencia autorizada para los datos tabulados (y que ReciPro no puede reproducir literalmente por motivos de copyright; véase la pestaña **Configuraciones** para el listado propio de ReciPro de orígenes/configuraciones alternativos de un tipo de grupo espacial dado).

!!! note "Relaciones de grupo… es una función en desarrollo activo"
    El explorador **Relaciones de grupo…** (A4.2) calcula los subgrupos y supergrupos *translationengleiche* (t-) y *klassengleiche* (k-, incluidos los *isomorfos*) directamente a partir de las propias operaciones de simetría del grupo espacial (no a partir de una lista pretabulada), de modo que cada relación mostrada está verificada de forma independiente en lugar de copiada de una tabla. Los límites que quedan — p. ej. la serie isomorfa solo se enumera hasta índice ≤ 4 — se detallan en las **Limitaciones actuales** de A4.2.

---

## Véase también

- [2. Información de simetría](../../2-symmetry-information.md) — la guía de la GUI que este apéndice explica.
- [A4.1. Símbolos de grupos espaciales y diagramas de simetría](symbols-and-diagrams.md) · [A4.2. Relaciones grupo-subgrupo](group-subgroup-relations.md)
- [Apéndice A1. Sistemas de coordenadas](../a1-coordinate-system/1-orientation.md)
- [Apéndice A2. Interacción del haz (fundamentos de física del estado sólido)](../a2-beam-interaction/index.md) — donde las condiciones de reflexión del grupo espacial (ausencias sistemáticas) entran en el factor de estructura.
