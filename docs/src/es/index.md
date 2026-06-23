# Manual de ReciPro

<!-- 260623Ch: Shared demo movie for all localized top pages. -->
<div class="rp-demo-video" markdown="0">
  <video controls autoplay muted playsinline preload="metadata" aria-label="ReciPro demo movie">
    <source src="../assets/Recipro_Demo.mp4" type="video/mp4">
  </video>
</div>

## Breve introducción
* ReciPro es un software libre con licencia MIT que ofrece una gran variedad de cálculos cristalográficos y simulaciones de microscopía electrónica.
* ReciPro se ha descargado más de 27.000 veces en total desde su publicación en GitHub (marzo de 2020) y es utilizado por numerosos cristalógrafos y microscopistas electrónicos.

## Encontrar por objetivo

| Objetivo | Empezar aquí | Pasos siguientes principales |
|------|------------|-----------------|
| Cargar un cristal y establecer su orientación | [Ventana principal](0-main-window.md) | [Geometría de rotación](4-rotation-geometry.md), [Apéndice A1. Sistemas de coordenadas](appendix/a1-coordinate-system/1-orientation.md) |
| Examinar una estructura cristalina en 3D | [Visor de estructura](5-structure-viewer.md) | [Información de simetría](2-symmetry-information.md) |
| Calcular patrones SAED / XRD / PED / CBED | [Simulador de difracción](7-diffraction-simulator/index.md) | [SAED](7-diffraction-simulator/1-saed-simulation.md), [Difracción de rayos X](7-diffraction-simulator/4-x-ray-neutron-diffraction.md), [PED](7-diffraction-simulator/2-ped-simulation.md), [CBED](7-diffraction-simulator/3-cbed-simulation.md) |
| Calcular imágenes HRTEM / STEM | [Simulador HRTEM/STEM](9-hrtem-stem-simulator/index.md) | [HRTEM](9-hrtem-stem-simulator/1-hrtem-simulation.md), [STEM](9-hrtem-stem-simulator/2-stem-simulation.md) |
| Simular patrones EBSD | [Simulación EBSD](12-ebsd-simulation.md) | [Trayectorias electrónicas](8-electron-trajectory.md), [Apéndice A3. Cálculo de EBSD](appendix/a3-bloch-wave/ebsd.md) |
| Indexar reflexiones de difracción experimentales | [Spot ID v1](10-spot-id.md), [Spot ID v2](11-spot-id-v2.md) | [Simulador de difracción](7-diffraction-simulator/index.md) |
| Comprender las ecuaciones de la difracción dinámica | [Apéndice A3. Método de ondas de Bloch](appendix/a3-bloch-wave/index.md) | [Cálculo dinámico](appendix/a3-bloch-wave/calculation.md), [CBED](appendix/a3-bloch-wave/cbed.md), [STEM](appendix/a3-bloch-wave/stem.md), [EBSD](appendix/a3-bloch-wave/ebsd.md) |

## Funciones
* **Full GUI** : Todas las operaciones se realizan a través de una interfaz gráfica. La mayoría de las entradas/salidas de archivos admiten arrastrar y soltar.
* **Lista de cristales** : Gestiona varios cristales a la vez; no es necesario abrir una ventana distinta para cada cristal.
* **Base de datos de grupos espaciales** : Base de datos integrada que abarca los 230 grupos espaciales de las International Tables Volume A, además de 530 símbolos de Hall, con elementos de simetría, posiciones de Wyckoff y reglas de extinción. Los elementos de simetría y las posiciones generales pueden dibujarse como diagramas esquemáticos al estilo de las *International Tables* Vol. A (véase [2. Información de simetría](2-symmetry-information.md)).
* **Información atómica** : Factores de dispersión (rayos X, electrón, neutrón), energías de rayos X característicos, proporciones isotópicas, etc. para los elementos H (1) – Cf (98).
* **Rotación flexible del cristal** : Establece la orientación mediante índices de eje de zona/plano cristalino o arrastrando el ratón. Se admite la notación de Miller-Bravais (4 índices *hkil*) para los sistemas trigonal/hexagonal. El estado de rotación se sincroniza en todas las ventanas de simulación.
* **Simulación de difracción** : Difracción electrónica cinemática y dinámica (método de ondas de Bloch), difracción de rayos X (incluidas las cámaras de precesión y de Laue por reflexión), difracción electrónica de precesión (PED) y difracción electrónica de haz convergente (CBED). Una simulación de portamuestras TEM vincula el patrón de difracción con los ángulos de inclinación del portamuestras.
* **Simulación HRTEM / STEM** : Simulación de imágenes TEM de alta resolución con modelos de coherencia parcial; STEM con dispersión térmica difusa.
* **EBSD y trayectorias electrónicas** : Simulación de patrones EBSD y simulación Monte-Carlo de trayectorias electrónicas (véase [8. Trayectorias electrónicas](8-electron-trajectory.md)).
* **Indexación de reflexiones** : Detección, ajuste e indexación automáticos de reflexiones de difracción a partir de imágenes experimentales (Spot ID v1/v2).
* **Macro** : Macro con sintaxis de Python para automatizar operaciones (véase [20. Macro](20-macro/index.md)).
* **Tema claro / oscuro** : La interfaz sigue un modo de color claro u oscuro seleccionable.

## Requisitos del sistema
| Elemento | Mínimo | Recomendado |
|------|---------|-------------|
| SO | Windows con [.NET Desktop Runtime 10.0](https://dotnet.microsoft.com/download/dotnet/10.0) (Windows on ARM64 compatible) | Windows 11 |
| GPU | OpenGL 1.3 | GPU externa con OpenGL 4.3 |
| Memoria | – | 16 GB o más |
| CPU | – | 8+ núcleos (para cálculos dinámicos) |

**Windows on ARM (nativo, experimental)** : En la [página de versiones](https://github.com/seto77/ReciPro/releases/latest) está disponible un paquete portable nativo ARM64 experimental (`ReciPro-v.X_arm64.zip`, self-contained — no requiere la instalación del .NET Runtime). Los paquetes x64 habituales también se ejecutan en Windows ARM64 mediante la emulación integrada. Consulte [Solución de problemas](troubleshooting.md#windows-on-arm) para ver notas de configuración y limitaciones.

**macOS (no oficial)** : ReciPro solo es compatible oficialmente con Windows, pero se ha informado de que el paquete portable ZIP **win-x64** se ejecuta en macOS (Apple Silicon) utilizando el contenedor de Wine Sikarugir combinado con el controlador OpenGL Mesa3D. En <https://github.com/Ryo-fkushima/ReciPro_macOS_memo> hay disponible una guía de configuración publicada por un usuario. Tenga en cuenta que esta vía no tiene soporte oficial y que algunos símbolos (Å, superíndices, flechas) pueden mostrarse incorrectamente. El ZIP ARM64 **no** se ejecuta en macOS + Wine, y se prevé que la vía actual de x64 + Rosetta 2 deje de funcionar a partir de macOS 28 (otoño de 2027) — consulte [Solución de problemas](troubleshooting.md#mac-linux) para más detalles.

## Cómo usar este manual

Este manual de GitHub Pages es actualmente la fuente de referencia. Use la navegación de la izquierda para explorar por capítulos, o use la búsqueda del encabezado para encontrar el nombre de una función o una etiqueta de la interfaz. Los antiguos manuales en PDF se conservan como referencia de archivo.

* **PDF archivado (inglés):** <https://raw.githubusercontent.com/seto77/ReciPro/master/ReciPro/doc/ReciProManual(en).pdf>
* **PDF archivado (japonés):** <https://raw.githubusercontent.com/seto77/ReciPro/master/ReciPro/doc/ReciProManual(ja).pdf>

## Inicio rápido
1. Descargue e instale desde [Releases](https://github.com/seto77/ReciPro/releases/latest).
2. Seleccione un cristal de la lista integrada (~80 cristales). También puede importar archivos CIF o usar [CSManager](https://github.com/seto77/CSManager).
3. Llame a las funciones desde el panel de la derecha: Visor de estructura, Estereograma, Simulador de difracción, Simulación HRTEM, etc.
4. Gire el cristal arrastrando el ratón o introduciendo índices de eje de zona/plano.

## Referencia
> Y. Seto, "ReciPro: free and open-source multipurpose crystallographic software integrating a crystal operation interface and diffraction simulators," *J. Appl. Cryst.* **55**, 397–410 (2022). <https://doi.org/10.1107/S1600576722000139>

## Licencia
ReciPro se distribuye bajo la [Licencia MIT](https://github.com/seto77/ReciPro/blob/master/LICENSE.md).
