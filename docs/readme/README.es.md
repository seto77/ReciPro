# ReciPro

[![Documentation](https://img.shields.io/badge/%F0%9F%93%96_Documentation-blue)](https://seto77.github.io/ReciPro/es/)
[![Latest Release](https://img.shields.io/github/v/release/seto77/ReciPro?logo=github)](https://github.com/seto77/ReciPro/releases/latest)
[![Total downloads](https://img.shields.io/github/downloads/seto77/ReciPro/total?logo=github&label=GitHub%20downloads)](https://github.com/seto77/ReciPro/releases)
[![GitHub Stars](https://img.shields.io/github/stars/seto77/ReciPro?style=social)](https://github.com/seto77/ReciPro/stargazers)
[![GitHub Forks](https://img.shields.io/github/forks/seto77/ReciPro?style=social)](https://github.com/seto77/ReciPro/forks)
[![License: MIT](https://img.shields.io/badge/License-MIT-green)](https://github.com/seto77/ReciPro/blob/master/LICENSE.md)

<!-- 260804Cl: Traducción de ../../README.md (inglés). Actualice este archivo cuando cambie la versión en inglés. -->
[English](../../README.md) | [日本語](README.ja.md) | [Deutsch](README.de.md) | [Français](README.fr.md) | **Español** | [Italiano](README.it.md) | [Русский](README.ru.md) | [简体中文](README.zh-Hans.md) | [繁體中文](README.zh-Hant.md) | [한국어](README.ko.md) | [Português](README.pt.md)

*ReciPro* es un programa de cristalografía multipropósito, gratuito y de código abierto, basado en una interfaz gráfica. Ofrece acceso fluido a funciones para explorar bases de datos cristalográficas, visualizar estructuras cristalinas y ajustes de goniómetro, simular patrones de difracción e imágenes de microscopía de alta resolución, y analizar datos de difracción. Estas funciones están enlazadas mediante una interfaz sencilla de usar, y los resultados se muestran de forma sincronizada casi en tiempo real. *ReciPro* ayudará a un amplio abanico de cristalógrafos (incluidos principiantes) que trabajan con difracción de rayos X, de electrones y de neutrones, así como con TEM.

*ReciPro* se desarrolla de forma continua desde 2002 y está disponible en GitHub desde marzo de 2020. Se ha descargado más de 27 000 veces desde GitHub y lo utilizan cientos de usuarios en más de una docena de laboratorios de universidades y empresas.

***[¡Consulte el manual para aprender a usarlo!](https://seto77.github.io/ReciPro/es/)***

[Diversas simulaciones ejecutándose en tiempo real (ejemplo: MgAl2O4)](https://github.com/user-attachments/assets/6b0234dd-f2d6-49db-b146-bb74cf6021b6)

## Autores

*ReciPro* está desarrollado por [Seto Y.](https://yseto.net/en/home-e) y [Ohtsuka M.](https://researchmap.jp/7000002999?lang=en). Las funciones y los algoritmos se presentan en [el artículo](https://github.com/seto77/ReciPro/blob/master/docs/ReciProSetoOhtsuka2022.pdf).

## Cómo citar

Si utiliza *ReciPro* en trabajos académicos, emplee el enlace **Cite this repository** que aparece en la página del repositorio de GitHub. Los metadatos de citación se proporcionan mediante `CITATION.cff`, y la cita preferida es el siguiente artículo:

  * [Seto, Y. & Ohtsuka, M. (2022). *J. Appl. Cryst.* **55**, 397-410, doi: 10.1107/S1600576722000139.](https://doi.org/10.1107/S1600576722000139)

También puede citar el propio repositorio del programa cuando resulte apropiado:

  * Repositorio: https://github.com/seto77/ReciPro
  * Versiones: https://github.com/seto77/ReciPro/releases/latest

***

## Instalación

* Descargue [*ReciPro-setup.msi*](https://github.com/seto77/ReciPro/releases/latest/download/ReciPro-setup.msi) (enlace directo a la última versión) y ejecútelo. También puede encontrarlo en la [página de versiones](https://github.com/seto77/ReciPro/releases/latest). (Hasta la v.4.939, el instalador se llamaba *ReciProSetup.msi*.)
* *ReciPro* funciona en Windows con ***.Net Desktop Runtime 10.0*** (NO ***.Net Runtime 10.0***), que puede instalarse desde [aquí](https://dotnet.microsoft.com/download/dotnet/10.0).
* Si no puede ejecutar un instalador (por ejemplo, en equipos con permisos restringidos), también hay disponible un paquete **ZIP portátil** (*ReciPro-v.X.XXX.zip*) en la página de versiones: autónomo, sin instalación y sin necesidad del entorno de ejecución .NET; basta con descomprimirlo y ejecutarlo.
* *ReciPro* se distribuye bajo la **licencia MIT** (cualquiera puede usarlo, modificarlo y redistribuirlo libremente).
* Para conocer el estado de la firma de código y cómo verificar el instalador, consulte la [política de firma de código](../../CODE_SIGNING.md).
* Para los componentes y datos de terceros incluidos o referenciados, consulte los [avisos de terceros](../../THIRD-PARTY-NOTICES.md).

### macOS (no oficial)

* *ReciPro* solo admite oficialmente Windows, pero se ha informado de que funciona en macOS (Apple Silicon) combinando el paquete **ZIP portátil** con el envoltorio de Wine **Sikarugir** y el controlador OpenGL **Mesa3D**, sin necesidad de licencia de Windows ni de máquina virtual.
* Consulte la guía paso a paso publicada por Ryo Fukushima (JAMSTEC): https://github.com/Ryo-fkushima/ReciPro_macOS_memo
* Esta configuración no está oficialmente admitida ni completamente verificada. Una limitación conocida es que algunos símbolos (Å, superíndices, flechas) pueden mostrarse de forma incorrecta.
* Los símbolos mal representados pueden corregirse instalando en el prefijo de Wine fuentes con amplia cobertura de glifos (**DejaVu Sans/Serif** y **Noto Sans CJK JP** para la interfaz en japonés): ReciPro detecta el entorno Wine y cambia a ellas automáticamente. Consulte la [resolución de problemas](https://seto77.github.io/ReciPro/es/troubleshooting/) para más detalles.

### Nota sobre las advertencias de seguridad de Windows

* Descargue *ReciPro* únicamente desde la página oficial de GitHub Releases: https://github.com/seto77/ReciPro/releases/latest
* En algunos sistemas Windows, Microsoft Defender SmartScreen o Smart App Control pueden mostrar una advertencia antes de ejecutar el instalador. Esto puede ocurrir con programas de investigación recién compilados o de distribución limitada, y la advertencia en sí no significa necesariamente que el instalador sea malicioso.
* Si desea verificar por su cuenta el instalador descargado, puede analizarlo con un servicio de múltiples motores como VirusTotal.

## Política de firma de código

[<img src="https://signpath.org/assets/favicon-50x50.png" alt="SignPath" height="20">](https://about.signpath.io/) Firma de código gratuita en Windows proporcionada por [SignPath.io](https://about.signpath.io/), con certificado de la [SignPath Foundation](https://signpath.org/).

Desde la v.4.942, los artefactos de las versiones (el instalador *ReciPro-setup.msi* y el ejecutable portátil *ReciPro.exe*) se firman con Windows Authenticode como parte de la canalización de publicación automatizada, y cada solicitud de firma es revisada y aprobada manualmente por el mantenedor antes de su publicación. Consulte [CODE_SIGNING.md](../../CODE_SIGNING.md) para ver la política completa, incluidos el alcance de la firma, cómo verificar un instalador y cómo informar de artefactos sospechosos.

## Privacidad

*ReciPro* es una aplicación de escritorio local. **No** recopila, almacena ni transmite datos personales ni de uso, y no contiene telemetría ni analítica. Tras la instalación funciona totalmente sin conexión.

Las únicas conexiones de red que realiza *ReciPro* son descargas opcionales iniciadas por el usuario, y ninguna de ellas envía sus datos:

* **Buscar actualizaciones** (comando de menú): compara la versión instalada con la última publicada en GitHub y, si así lo decide, descarga el nuevo instalador desde la página oficial de [GitHub Releases](https://github.com/seto77/ReciPro/releases/latest).
* **Base de datos COD** (Crystallography Open Database): se descarga en el primer uso (~880 MB) desde el espejo de GitHub del autor y después se utiliza sin conexión.
* **Biblioteca Intel MKL** (aceleración opcional): se descarga (~55 MB) desde [nuget.org](https://www.nuget.org/) solo si activa la opción *Use MKL*, para acelerar los cálculos de difracción dinámica.

La base de datos AMCSD incluida y todas las funciones principales funcionan por completo sin conexión.

## Manual
  * Manual en línea (inglés / japonés): https://seto77.github.io/ReciPro/es/
  * Versión japonesa: https://yseto.net/soft/recipro
***

## Funciones principales

### Base de datos cristalográfica

* **AMCSD** (American Mineralogist Crystal Structure Database): más de 21 000 estructuras cristalinas integradas y disponibles inmediatamente después de la instalación.
  * La base de datos está muy comprimida (~5 MB) e incluida en el archivo de instalación, por lo que está disponible en entornos sin conexión.
  * Los cristales pueden buscarse por nombre, composición química, parámetros de red, densidad, simetría y elementos contenidos.
  * Referencia: [Downs & Hall-Wallace, 2003, *American Mineralogist* **88**, 247-250](https://www.geo.arizona.edu/xtal/group/pdf/am88_247.pdf)
* **COD** (Crystallography Open Database): también están disponibles unas 525 000 estructuras cristalinas, incluidos cristales orgánicos.
  * Se descarga automáticamente en el primer uso (~880 MB) y después está disponible sin conexión.
  * Referencias: [Gražulis et al., 2009, *J. Appl. Cryst.* **42**, 726-729](https://doi.org/10.1107/S0021889809016690); [Gražulis et al., 2012, *Nucleic Acids Res.* **40**, D420-D427](https://doi.org/10.1093/nar/gkr900)
* Importación y exportación de archivos en formato CIF y AMC.

### Cálculos cristalográficos

* Se admiten 530 notaciones de grupos espaciales: 230 asignaciones estándar de las ITA + 300 asignaciones de ejes no estándar.
  * Condiciones generales (reglas de extinción), posiciones de Wyckoff y multiplicidades de todos los grupos espaciales.
  * Cálculo geométrico de la periodicidad o de los ángulos entre planos o ejes.
  * Generación de posiciones atómicas equivalentes.
  * Conversión sencilla entre asignaciones de ejes no estándar (por ejemplo, de *Pbnm* a *Pnma*) y desplazamientos de origen.

### Propiedades atómicas

* Longitud de onda y energía de los rayos X característicos de <sup>1</sup>H a <sup>98</sup>Cf.
* Factores de dispersión atómica para rayos X, electrones y neutrones.

### Visor de estructuras

* Visualización 3D de estructuras cristalinas mediante la arquitectura OpenGL (GLSL).
  * Representa átomos, enlaces, poliedros de coordinación, celdas unidad, planos reticulares, superficies límite y etiquetas de leyenda.
  * Incluso estructuras cristalinas complejas con decenas de miles de átomos se dibujan con fluidez en tiempo real.
  * Los colores y tamaños de átomo predeterminados son compatibles con VESTA.
  * El intervalo de dibujo puede definirse por múltiplos de la celda unidad o por los índices de un plano cristalino y la distancia al centro.
  * Pueden representarse hábitos cristalinos arbitrarios coloreando las caras límite.
  * Puede mostrarse cualquier plano reticular, lo que ayuda a los principiantes a comprender el concepto de plano reticular en los fenómenos de difracción.
  * La rotación, el desplazamiento y el zoom se controlan libremente con el ratón.
  * Al hacer clic en un átomo se muestran las distancias y los ángulos de enlace con los átomos vecinos.
  * El estado de rotación se refleja de inmediato en las demás ventanas funcionales (proyección estereográfica, simulador de difracción, etc.).
  * El codificador de vídeo integrado (Windows Media Foundation) puede generar vídeos de animación de rotación (MP4 H.264/H.265) para presentaciones.

### Proyección estereográfica

* Representa planos y ejes cristalinos en una proyección estereográfica.
  * Se admiten tanto la proyección equiangular (red de Wulff) como la equiareal (red de Schmidt), con líneas de latitud y longitud.
  * Los índices pueden especificarse mediante intervalos numéricos o valores concretos.
  * Pueden mostrarse círculos máximos indicando los ejes de zona.
  * Los objetos dibujados pueden guardarse o copiarse en formato vectorial para editarlos después sin pérdida de resolución.
  * Visualización 3D de la geometría de la proyección estereográfica con fines didácticos.

### Simulador de difracción

* Simula patrones de difracción de monocristal para fuentes de rayos X, electrones y neutrones.
  * La energía cinética del haz incidente puede configurarse libremente.
  * Incluye las energías de los rayos X característicos de <sup>1</sup>H a <sup>98</sup>Cf.
  * El intervalo representado se especifica mediante la resolución de la imagen (tamaño de píxel) y la longitud de cámara.
  * También se admiten geometrías con el detector inclinado.
  * Se admite la superposición de imágenes adquiridas experimentalmente.
  * La rotación del cristal (condición de difracción) puede controlarse y se sincroniza de inmediato con las demás ventanas.

* **Difracción policristalina**: simulación de anillos de Debye suponiendo una muestra policristalina.
* **Cámara de precesión** (rayos X): simulación de patrones de cámara de precesión de la zona de Laue de orden cero.
* **Cámara de Laue por retrorreflexión** (rayos X): simulación de patrones de Laue por retrorreflexión.

#### Teoría cinemática de la difracción
* Disponible para todas las fuentes (rayos X, electrones, neutrones).
* Las intensidades de difracción se estiman a partir del cuadrado del módulo del factor de estructura cristalina y del error de excitación.
* Se incorporan los efectos del factor de Debye-Waller sobre las intensidades de difracción.

#### Teoría dinámica de la difracción (electrones)
* Basada en el **método de ondas de Bloch** (Bethe, 1928), que permite orientaciones cristalinas flexibles sin restringirse a ejes de zona de índices bajos.
* Hay dos enfoques de cálculo disponibles:
  * **Método de autovalores de Bethe**: diagonalización matricial para obtener autovalores y autovectores de los autoestados de Bloch. Adecuado cuando se varía el espesor de la muestra.
  * **Método de la matriz de dispersión**: cálculo directo de exponenciales de matrices mediante el método de escalado y elevación al cuadrado con aproximación de Padé. Adecuado para cálculos rápidos con un único espesor.
* El algoritmo más rápido y la mejor biblioteca matemática (Eigen, Intel MKL o Math.NET) se seleccionan automáticamente.
* El potencial de absorción por dispersión difusa térmica (TDS) se calcula analíticamente para lograr un alto rendimiento.

* **SAED** (difracción de electrones de área seleccionada): simulación de difracción de electrones con haz paralelo incluyendo efectos de dispersión dinámica.
* **PED** (difracción de electrones por precesión): simula patrones PED especificando el ángulo de precesión y la resolución angular acimutal. Útil para el análisis de estructuras cristalinas y la optimización de condiciones PED cuasicinemáticas.
* **CBED** (difracción de electrones de haz convergente): simula patrones CBED con el semiángulo de convergencia y el número de divisiones definidos por el usuario. Se admite la simulación a distintos espesores para determinar el espesor de la muestra.
  * Patrones CBED promediados en posición (PACBED).
  * Simulación CBED de gran ángulo (LA-CBED).

### Simulador HRTEM

* Simulación de imágenes de microscopía electrónica de transmisión de alta resolución con el mismo marco teórico de ondas de Bloch.
* Los parámetros ópticos (voltaje de aceleración, coeficiente de aberración esférica, desenfoque, espesor de la muestra, etc.) se ajustan desde la interfaz gráfica.
* Incluye ajustes predefinidos de parámetros ópticos típicos de TEM, accesibles con el botón derecho.
* Dos modelos de formación de imagen para la coherencia parcial:
  * **Teoría lineal de transferencia de contraste**: menor coste computacional; adecuada para muestras delgadas que cumplen la aproximación de objeto de fase débil.
  * **Teoría no lineal de transferencia de contraste (modelo TCC)**: basada en el coeficiente cruzado de transmisión de primer orden (Ishizuka, 1980); fiable incluso para muestras más gruesas y materiales de número atómico elevado.
* Puede representarse la función de transferencia de contraste con sus funciones envolventes.
* Las series de imágenes espesor-desenfoque pueden calcularse simultáneamente.
* En condiciones estándar suele completarse en menos de un segundo.

### Simulador STEM

* Simulación de imágenes de microscopía electrónica de transmisión de barrido.
  * Modos de imagen de campo claro (BF), campo oscuro anular (ADF) y campo oscuro anular de alto ángulo (HAADF).
  * El haz convergente se trata como la superposición de muchas ondas planas con un cálculo preciso del solapamiento.
  * Los electrones dispersados inelásticamente se calculan con el modelo de potencial absorbente.
  * Pueden generarse series de imágenes espesor-desenfoque.

### Spot ID

* Indexación semiautomática de puntos de difracción para patrones SAED experimentales.
* **Spot ID v1**: busca ejes de zona a partir de la configuración geométrica (distancias y ángulos) de los puntos de difracción. Admite el análisis simultáneo de 2 o 3 imágenes.
* **Spot ID v2**: importa directamente imágenes de patrones SAED.
  * Admite formatos de imagen habituales: TIFF (.tif), Digital Micrograph 3/4 (.dm3, .dm4) y más.
  * Detección y ajuste automáticos de los puntos de difracción con funciones pseudo-Voigt 2D.
  * Búsqueda exhaustiva de orientaciones cristalinas que se correspondan con la disposición de los vectores de la red recíproca.
  * Determinación precisa incluso de ejes de zona de orden alto.

### Geometría de rotación (goniómetro)

* Vincula los ángulos de Euler de ReciPro con el goniómetro del laboratorio.
* Indica cómo debe girarse el goniómetro para alcanzar la orientación cristalina deseada (por ejemplo, un eje de zona de índices bajos).
* Admite definiciones de goniómetro arbitrarias.

### Macros

* Macros con sintaxis de Python para automatizar tareas.
  * Ejemplo: girar un cristal en pasos de 1° y guardar los patrones de difracción o las imágenes STEM en cada paso.
  * Las funciones propias de ReciPro están disponibles en el espacio de nombres «ReciPro».
  * Hay ejemplos de uso en el [manual](https://seto77.github.io/ReciPro/es/20-macro/2-examples/).

### Otras funciones

* **Simulador de alcance electrónico**: simulación de Monte Carlo del alcance de los electrones en los materiales.
* **EBSD** (difracción de electrones retrodispersados): en desarrollo.

## Detalles técnicos

* Escrito en **C++**, **C#** y **OpenGL Shading Language (GLSL)**.
* Paralelización multihilo para cálculos de alto rendimiento en CPU modernas con muchos núcleos.
* Todas las ventanas funcionales se actualizan de forma sincronizada y en tiempo real cuando cambia la orientación del cristal.
* Utiliza un sistema de coordenadas cartesianas dextrógiro (X: derecha, Y: arriba, Z: frente) con el convenio de ángulos de Euler Z–X–Z.
* Las definiciones de coordenadas son compatibles con el software EBSD de Thermo Fisher Scientific.

### Impacto académico

* **Artículo de software revisado por pares:** [Seto, Y. & Ohtsuka, M. (2022), *Journal of Applied Crystallography*, **55**, 397-410](https://doi.org/10.1107/S1600576722000139).
* **Artículos que lo citan:** [artículos citantes en Google Scholar](https://scholar.google.jp/scholar?cites=12625594477623342627).
* **Repercusión del artículo:** [detalles en Altmetric](https://www.altmetric.com/details/123778746).

| Indicador | Valor destacado |
| --- | --- |
| Descargas totales en GitHub | más de 27 000 descargas |
| Citas en Google Scholar | más de 170 citas |
| Citas en Dimensions | más de 160 citas |
| Lectores en Mendeley | más de 90 lectores |

## Capturas de pantalla

<img src="https://seto77.github.io/ReciPro/assets/cap-es-auto/FormMain.png" height="320px" alt="Ventana principal">
<img src="https://seto77.github.io/ReciPro/assets/cap-es-auto/FormCrystalDatabase.png" height="320px" alt="Base de datos cristalográfica">
<img src="https://seto77.github.io/ReciPro/assets/cap-es-auto/FormSymmetryInformation.png" height="320px" alt="Información de simetría">
<img src="https://seto77.github.io/ReciPro/assets/cap-es-auto/FormBeamInteraction.png" height="320px" alt="Interacción del haz">
<img src="https://seto77.github.io/ReciPro/assets/cap-es-auto/FormStructureViewer.png" height="320px" alt="Visor de estructuras">
<img src="https://seto77.github.io/ReciPro/assets/cap-es-auto/FormStereonet.png" height="320px" alt="Proyección estereográfica">
<img src="https://seto77.github.io/ReciPro/assets/cap-es-auto/FormDiffractionSimulator.png" height="320px" alt="Simulador de difracción">
<img src="https://seto77.github.io/ReciPro/assets/cap-es-auto/FormImageSimulator.png" height="320px" alt="Simulador HRTEM/STEM">
<img src="https://seto77.github.io/ReciPro/assets/cap-es-auto/FormSpotIDV2.png" height="320px" alt="Spot ID v2">
<img src="https://seto77.github.io/ReciPro/assets/cap-es-auto/FormMacro.png" height="320px" alt="Macros">
<img src="https://seto77.github.io/ReciPro/assets/cap-es-auto/FormTrajectory.png" height="320px" alt="Simulador de alcance electrónico">

***
