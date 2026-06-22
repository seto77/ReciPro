# Solución de problemas

Problemas habituales y sus soluciones para ReciPro. Muchas de las entradas siguientes provienen de preguntas e informes de errores en el [rastreador de incidencias de GitHub](https://github.com/seto77/ReciPro/issues); la versión en la que se corrigió un error se indica cuando corresponde.

> **La mayoría de los problemas se resuelven simplemente actualizando a la [versión más reciente](https://github.com/seto77/ReciPro/releases/latest).** ReciPro se actualiza con frecuencia, y muchos de los errores enumerados a continuación se corrigieron a los pocos días de ser notificados.

---

## Inicio y arranque

### Síntoma: El proceso se está ejecutando, pero no aparece ninguna ventana

ReciPro se inicia (es visible en el Administrador de tareas), pero su ventana nunca aparece en la pantalla.

**Causa**: La ventana se abrió fuera de la pantalla, un problema con las coordenadas de pantalla de Windows, normalmente tras cambiar de monitor o de escala de pantalla. (Incidencias [#50](https://github.com/seto77/ReciPro/issues/50), [#53](https://github.com/seto77/ReciPro/issues/53), [#55](https://github.com/seto77/ReciPro/issues/55))

**Solución**:

1. Abra el **Administrador de tareas**.
2. Busque **ReciPro** en la lista de procesos.
3. Haga clic derecho sobre él y elija **Maximizar**.

La ventana se traerá a su pantalla principal. Tenga en cuenta que **Cambiar a**, **Traer al frente** y **Minimizar** *no* ayudan; solo **Maximizar** funciona.

### Síntoma: ReciPro no se inicia, se bloquea o se cuelga al arrancar

**Causa**: Lo más frecuente es que falle la inicialización de OpenGL, o que un valor de registro/ajuste dañado bloquee el arranque.

**Solución** (pruebe en orden):

1. **Desactivar OpenGL**: mantenga pulsada la tecla **Ctrl** mientras inicia ReciPro para arrancar con OpenGL desactivado. Las versiones recientes (v4.925 y posteriores) refuerzan la inicialización de OpenGL para que la aplicación se inicie incluso cuando OpenGL falla; en ese caso las funciones 3D quedan desactivadas, pero el resto de la aplicación funciona.
2. **Restablecer los ajustes**: en el editor del registro, elimine la clave `HKEY_CURRENT_USER\Software\Crystallography\ReciPro` y reinicie. (Equivale a **Option → Reset registry**.)
3. **Reinstalación limpia**: desinstale ReciPro, elimine las siguientes carpetas si existen (sustituya `<user>` por el nombre de su cuenta) y vuelva a instalar:
   - `C:\Users\<user>\AppData\Local\Crystallography Software\ReciPro`
   - `C:\Users\<user>\AppData\Roaming\ReciPro\ReciPro`
4. **Actualice** a la versión más reciente.

Si nada de esto ayuda, la causa puede estar en el propio entorno del sistema operativo; [abra una incidencia](https://github.com/seto77/ReciPro/issues) con los datos de su PC (CPU, GPU, versión de Windows).

---

## Problemas de OpenGL

### Síntoma: Pantalla negra o bloqueo al arrancar

**Causa**: GPU incompatible o entorno de escritorio remoto.

**Solución**:

1. Vaya a **Option → Disable OpenGL (needs restart)** (o mantenga pulsada **Ctrl** al iniciar).
2. Reinicie ReciPro.
3. El Visor de estructura y algunas funciones 3D usarán entonces renderizado por software.

### Síntoma: Una GPU integrada o antigua (Intel/AMD) no renderiza

**Causa**: Algunas GPU integradas (p. ej. AMD Radeon Vega, Intel UHD) tenían problemas de inicialización de OpenGL en compilaciones antiguas. (Incidencia [#2](https://github.com/seto77/ReciPro/issues/2))

**Solución**: Actualice a la versión más reciente. El requisito de versión de OpenGL se redujo (v4.781), se corrigió la inicialización de las GPU integradas (v4.785) y la inicialización se reforzó aún más para fallar de forma controlada (v4.925). Actualizar los controladores de su GPU también ayuda.

### Síntoma: Mala calidad de renderizado

**Solución**: Actualice los controladores de su GPU. Se recomienda una GPU externa (dedicada) con soporte de OpenGL 1.5.

---

## .NET Runtime

### Síntoma: La aplicación no se inicia

**Causa**: El .NET Desktop Runtime requerido no está instalado. Las versiones actuales requieren **.NET Desktop Runtime 10.0** (compilaciones anteriores: v4.895–v4.91x requerían 9.0; véase la incidencia [#43](https://github.com/seto77/ReciPro/issues/43)).

**Solución**: Descárguelo e instálelo desde <https://dotnet.microsoft.com/download/dotnet/10.0> (elija el **Desktop Runtime**, x64 para la mayoría de los PC).

### Síntoma: No se puede acceder a la página de descargas de Microsoft

**Solución**: Puede descargar directamente el instalador del runtime. Elija el **Windows Desktop Runtime X64** para su arquitectura en la [página de descargas de .NET 10.0](https://dotnet.microsoft.com/download/dotnet/10.0). (Incidencia [#49](https://github.com/seto77/ReciPro/issues/49))

---

## Instalación

### Síntoma: Instalar o desinstalar sin derechos de administrador

**Nota**: No se requieren derechos de administrador. Los accesos directos y los archivos por usuario se colocan en sus propias carpetas de usuario (p. ej. `%AppData%\Microsoft\Windows\Start Menu\Programs\Crystallography Software\` y el Escritorio). (Incidencia [#38](https://github.com/seto77/ReciPro/issues/38))

---

## Visualización y diseño

### Síntoma: Los botones o paneles aparecen cortados / ocultos, o el diseño parece roto

Por ejemplo, el botón **Peak Identification** de Spot ID v2 queda oculto, o la página Acerca de y otros formularios aparecen mal alineados en versiones recientes. (Incidencias [#56](https://github.com/seto77/ReciPro/issues/56), [#59](https://github.com/seto77/ReciPro/issues/59))

**Causa**: Una regresión de escala DPI / fuente de la interfaz introducida en algunas compilaciones recientes.

**Solución**:

- Establezca la **escala de pantalla de Windows al 100 %** (esto suele restaurar el diseño).
- Como solución rápida, **cambie el tamaño de la ventana** (p. ej. redúzcala verticalmente) para mostrar los controles ocultos.
- Actualice a la versión más reciente: los diseños se van corrigiendo progresivamente. Si una compilación reciente se ve peor, volver a una versión algo más antigua (p. ej. v4.915) es una opción temporal. Informe de cualquier formulario que siga roto.

---

## Cálculos dinámicos

### Síntoma: Muy lento o sin memoria

**Causa**: Demasiadas ondas de Bloch o una imagen demasiado grande.

**Solución**:

- Reduzca **No. of Bloch waves** (50–200 suele bastar para cálculos rutinarios)
- Use el solucionador **Eigen** para ≤ 500 ondas; **MKL** para > 500 ondas
- Reduzca la resolución de imagen para las simulaciones STEM
- Cierre otras aplicaciones que consuman mucha memoria

### Síntoma: La imagen HAADF-STEM está negra

**Causa**: Los factores de temperatura atómicos (B) están establecidos en cero.

**Solución**: Establezca B ≥ 0.5 Å² para todos los átomos. La intensidad TDS requiere factores de temperatura distintos de cero.

---

## Simulador de difracción

### Síntoma: El patrón de difracción está en blanco / no se dibuja nada

**Causa**: Normalmente la vista está demasiado ampliada, o la energía de la onda incidente está fuera de rango. (Incidencia [#3](https://github.com/seto77/ReciPro/issues/3))

**Solución**:

- **Haga clic izquierdo** en el área de dibujo principal para alejar el zoom.
- Compruebe la energía de la onda incidente en la pestaña **Wave** (arriba a la izquierda): rayos X ≈ 1–100 keV, electrones ≈ 10–1000 keV son valores adecuados.

---

## Entrada/salida de archivos

### Síntoma: El archivo CIF no se carga

**Solución**:

- Compruebe que el archivo CIF esté bien formado
- Intente arrastrar y soltar el archivo sobre el área de **Información del cristal**
- Algunas extensiones CIF no estándar pueden no ser compatibles

### Síntoma: El archivo dm3/dm4 no se carga, o aparece "unable to cast … 'System.Single' to 'System.Double'"

**Causa**: Existen varias variantes del formato DM3/DM4, y las compilaciones antiguas no podían leerlas todas. (Incidencia [#15](https://github.com/seto77/ReciPro/issues/15))

**Solución**: Actualice a la versión más reciente: la compatibilidad de lectura de DM3 se mejoró en v4.835. Si un archivo sigue sin cargarse, [envíelo](https://github.com/seto77/ReciPro/issues) para que se pueda añadir compatibilidad.

### Síntoma: El archivo dm3/dm4 muestra una escala incorrecta

**Solución**: Verifique la calibración en el software original Digital Micrograph. ReciPro lee los metadatos incrustados; si los metadatos son incorrectos, establezca manualmente el tamaño de píxel y la longitud de cámara en el panel de óptica.

---

## Restablecer el registro

Si los ajustes se dañan:

1. **Option → Reset registry (after restart)**
2. Reinicie ReciPro: las posiciones de las ventanas, la longitud de onda, la longitud de cámara, etc. se restablecerán a los valores predeterminados

---

## Preguntas frecuentes

### ¿Existe una versión para Mac (o Linux)? {#mac-linux}

No existe una versión oficial para Mac ni para Linux. ReciPro depende del **.NET Desktop Runtime**, que actualmente solo se ejecuta en Windows. (Incidencia [#12](https://github.com/seto77/ReciPro/issues/12))

Sin embargo, se ha informado de una vía no oficial que funciona en macOS: la distribución **win-x64 portable ZIP** (disponible en la [página de versiones](https://github.com/seto77/ReciPro/releases/latest)) se ejecuta en macOS (Apple Silicon) usando el envoltorio de Wine **Sikarugir** combinado con el controlador OpenGL **Mesa3D**, sin necesidad de licencia de Windows ni máquina virtual. Una guía de configuración paso a paso publicada por un usuario está disponible en <https://github.com/Ryo-fkushima/ReciPro_macOS_memo>.

Tenga en cuenta que esta configuración no está oficialmente soportada ni completamente verificada. Una limitación conocida es que algunos símbolos (Å, superíndices, flechas) pueden mostrarse de forma incorrecta.

**Corrección de los símbolos distorsionados (Å, superíndices, flechas):** La causa es que las fuentes de Windows que ReciPro usa normalmente (Segoe UI, Yu Gothic UI, etc.) faltan en el entorno de Wine, y las fuentes sustitutas integradas de Wine carecen de algunos glifos científicos. ReciPro cambia automáticamente a fuentes de amplia cobertura **cuando detecta que se está ejecutando bajo Wine**, así que la corrección consiste simplemente en hacer que esas fuentes estén disponibles en el prefijo de Wine:

1. Instale **DejaVu Sans** / **DejaVu Serif** (cubre Å, superíndices, flechas, etiquetas de fracciones) y, para la interfaz en japonés, **Noto Sans CJK JP** (o **Noto Sans JP**).
2. La forma más sencilla es copiar los archivos `.ttf`/`.otf` descargados en la carpeta de fuentes del prefijo — `…/drive_c/windows/Fonts/` dentro del envoltorio Sikarugir — y reiniciar luego ReciPro. (`winetricks` también puede instalar algunas de estas.)
3. Al reiniciar, ReciPro las detecta automáticamente; no es necesario cambiar ningún ajuste de ReciPro.

Si las fuentes no están instaladas, ReciPro conserva sus nombres de fuente predeterminados, de modo que nada empeora: los símbolos simplemente siguen distorsionados.

**Perspectivas de esta vía — dos observaciones honestas:**

- El ZIP experimental **win-arm64** **no** se ejecuta en los Mac actuales, ni siquiera en Apple Silicon: las compilaciones de Wine para macOS actuales (incluido Sikarugir) ejecutan los binarios de Windows x86_64 a través de Rosetta 2 y no tienen ningún mecanismo para ejecutar binarios de Windows ARM64. En un Mac, use siempre el **win-x64** portable ZIP.
- Apple está retirando gradualmente Rosetta 2. macOS 27 (otoño de 2026) se ha anunciado como la última versión con soporte completo de Rosetta 2, por lo que se espera que la vía actual de x64 + Rosetta deje de funcionar a partir de macOS 28 (otoño de 2027). Hay un Wine ARM64 nativo para macOS en desarrollo upstream; si se materializa, el ZIP win-arm64 podría convertirse en el sucesor en Mac, pero todavía no se puede prometer.

### ¿Funciona ReciPro en Windows on ARM (ARM64)? {#windows-on-arm}

Sí, hay dos vías:

- **Paquete nativo ARM64 (experimental, recomendado)**: a partir de v4.938 se publica un paquete portable nativo ARM64 experimental (`ReciPro-v.X_arm64.zip`; llamado `ReciPro-v.X-arm64.zip` hasta v.4.939) en la [página de versiones](https://github.com/seto77/ReciPro/releases/latest). Es self-contained, por lo que no se requiere instalar el .NET Runtime: extraiga el ZIP en una carpeta donde el usuario tenga permiso de escritura y ejecute `ReciPro.exe`. Si Windows bloquea el ZIP descargado (Mark of the Web), haga clic derecho en el ZIP → **Propiedades** → marque **Desbloquear** → **OK**, *antes* de extraer (o ejecute `Unblock-File .\ReciPro-*arm64.zip` en PowerShell). Los detalles están en el archivo `README-PORTABLE.txt` incluido.
- **Paquete x64 bajo emulación**: el instalador MSI habitual y el win-x64 portable ZIP también se ejecutan en Windows ARM64 mediante la emulación x64 integrada, con el .NET Desktop Runtime (x64) instalado (confirmado aproximadamente desde v4.913 con .NET 10). Los cálculos pesados se ejecutan más lentamente que en la compilación nativa. (Incidencia [#47](https://github.com/seto77/ReciPro/issues/47))

Notas sobre el paquete nativo ARM64:

- Intel MKL no existe para ARM64, por lo que las opciones de solucionador y los elementos de menú correspondientes quedan ocultos. Los cálculos dinámicos usan la biblioteca nativa optimizada con NEON incluida; en casos de validación representativos sus resultados coincidieron con la compilación x64 dentro de la tolerancia de coma flotante esperada.
- Las vistas 3D (Visor de estructura y ventanas relacionadas) pueden ejecutarse, pero Windows on ARM proporciona OpenGL solo a través de una capa de traducción a Direct3D 12 (GLOn12 / Mesa), de modo que el renderizado 3D es notablemente más lento que en un PC con un controlador OpenGL nativo; esto es una limitación de la plataforma, no un error, y una compilación nativa ARM64 no puede cambiarlo. El modo de transparencia **High quality (Per-Pixel Linked List)** del Visor de estructura es especialmente lento en este conjunto de controladores; se recomienda el modo predeterminado **Approximate**. Si las vistas 3D no se inician, instale el "OpenCL, OpenGL, and Vulkan Compatibility Pack" desde la Microsoft Store.
- El paquete ARM64 **no** se ejecuta en macOS + Wine (véase la pregunta anterior). En un Mac, use el win-x64 portable ZIP.

### ¿Cómo debo citar ReciPro?

Use el enlace **Cite this repository** en la [página del repositorio de GitHub](https://github.com/seto77/ReciPro) (los metadatos los proporciona `CITATION.cff`). La cita preferida es:

> Seto, Y. & Ohtsuka, M. (2022). *J. Appl. Cryst.* **55**, 397–410. doi:[10.1107/S1600576722000139](https://doi.org/10.1107/S1600576722000139)

(Incidencia [#33](https://github.com/seto77/ReciPro/issues/33))

---

## Informar de errores

Informe de los problemas en: <https://github.com/seto77/ReciPro/issues>

Incluya:

- El número de versión de ReciPro
- Los pasos para reproducir el problema
- Cualquier mensaje de error o captura de pantalla
