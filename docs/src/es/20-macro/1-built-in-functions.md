# Funciones integradas

Referencia completa de las clases y funciones disponibles en las macros de ReciPro.

---

## Clase File

| Función | Descripción |
|----------|-------------|
| `File.GetDirectoryPath()` | Mostrar el diálogo de selección de carpeta y devolver la ruta elegida |
| `File.GetFileName()` | Mostrar el diálogo de selección de archivo y devolver la ruta elegida |
| `File.GetFileNames()` | Mostrar el diálogo de selección de múltiples archivos y devolver la lista de rutas |
| `File.ReadCrystalList()` | Cargar un archivo de lista de cristales (*.xml) |
| `File.ReadCrystal()` | Cargar un archivo de cristal CIF/AMC |
| `File.ExportAsCIF()` | Exportar el cristal actual como CIF |
| `File.SaveText()` | Guardar datos de texto en un archivo |

---

## Clase Crystal

| Propiedad | Tipo | Descripción |
|----------|------|-------------|
| `Crystal.Name` | string | Nombre del cristal |
| `Crystal.ChemicalFormula` | string | Fórmula química |
| `Crystal.Density` | double | Densidad (g/cm³) |

---

## Clase CrystalList

| Función / Propiedad | Descripción |
|---------------------|-------------|
| `CrystalList.SelectedIndex` | Obtener/establecer el índice del cristal seleccionado |
| `CrystalList.Add()` | Añadir el cristal actual a la lista |
| `CrystalList.Replace()` | Reemplazar el cristal seleccionado |
| `CrystalList.Delete()` | Eliminar el cristal seleccionado |
| `CrystalList.ClearAll()` | Vaciar todos los cristales |
| `CrystalList.MoveUp()` | Mover el cristal seleccionado hacia arriba |
| `CrystalList.MoveDown()` | Mover el cristal seleccionado hacia abajo |

---

## Clase Direction

| Función | Descripción |
|----------|-------------|
| `Direction.Euler(phi, theta, psi)` | Establecer la orientación mediante ángulos de Euler (radianes) |
| `Direction.EulerInDegree(phi, theta, psi)` | Establecer la orientación mediante ángulos de Euler (grados) |
| `Direction.EulerInDeg(phi, theta, psi)` | Alias de `EulerInDegree` |
| `Direction.Rotate(ax, ay, az, angle)` | Rotar alrededor de un eje arbitrario (radianes) |
| `Direction.RotateInDeg(ax, ay, az, angle)` | Rotar alrededor de un eje arbitrario (grados) |
| `Direction.RotateAroundAxis(u, v, w, angle)` | Rotar alrededor del eje de zona [uvw] (radianes) |
| `Direction.RotateAroundAxisInDeg(u, v, w, angle)` | Rotar alrededor del eje de zona [uvw] (grados) |
| `Direction.RotateAroundPlane(h, k, l, angle)` | Rotar alrededor de la normal del plano (hkl) (radianes) |
| `Direction.RotateAroundPlaneInDeg(h, k, l, angle)` | Rotar alrededor de la normal del plano (hkl) (grados) |
| `Direction.ProjectAlongPlane(h, k, l)` | Situar la normal del plano perpendicular a la pantalla |
| `Direction.ProjectAlongAxis(u, v, w)` | Situar el eje de zona perpendicular a la pantalla |

---

## Clase DifSim

### Control de ventana

`DifSim.Open()` / `DifSim.Close()`

### Fuente de ondas

`DifSim.Source_Xray()` / `DifSim.Source_Electron()` / `DifSim.Source_Neutron()`

### Propiedades

| Propiedad | Tipo | Descripción |
|----------|------|-------------|
| `Energy` | double | Energía (keV) |
| `Wavelength` | double | Longitud de onda (Å) |
| `Thickness` | double | Espesor de la muestra (nm) |
| `NumberOfDiffractedWaves` | int | Número de ondas de Bloch |
| `CameraLength2` | double | Longitud de cámara (mm) |
| `SkipRendering` | bool | Omitir el renderizado para el procesamiento por lotes |

### Modo de haz

`Beam_Parallel()` / `Beam_PrecessionXray()` / `Beam_PrecessionElectron()` / `Beam_Convergence()`

### Modo de cálculo

`Calc_Excitation()` / `Calc_Kinematical()` / `Calc_Dynamical()`

### Ajustes de imagen

| Propiedad / Función | Descripción |
|---------------------|-------------|
| `ImageResolutionInMM` | Resolución (mm/píxel) |
| `ImageResolutionInNMinv` | Resolución (nm⁻¹/píxel) |
| `ImageWidth` / `ImageHeight` | Tamaño de imagen (píxeles) |
| `ImageSize(w, h)` | Establecer el tamaño de imagen |

### Detector

| Propiedad | Descripción |
|----------|-------------|
| `Tau` / `TauInDeg` | Ángulo de inclinación del detector τ (rad / grados) |
| `Phi` / `PhiInDeg` | Eje de rotación del detector φ (rad / grados) |
| `Foot(x, y)` | Posición de foot en píxeles |

### Salida

| Función | Descripción |
|----------|-------------|
| `SaveAsPng()` | Guardar el patrón actual como PNG |
| `SpotInfo()` | Obtener los datos de reflexiones como cadena CSV |

---

## Clases HRTEM / STEM / Potential

Estas tres clases de simulación de imágenes comparten muchos miembros. Para evitar repeticiones, las tablas siguientes utilizan marcadores de posición:

- **`#`** : común a **HRTEM**, **STEM** y **Potential**. Reemplace `#` por `HRTEM`, `STEM` o `Potential` (p. ej. `STEM.Simulate()`, `Potential.AccVol`).
- **`$`** : común únicamente a **HRTEM** y **STEM**. Reemplace `$` por `HRTEM` o `STEM`.
- Los miembros escritos con un nombre de clase explícito (`STEM.…` / `HRTEM.…`) pertenecen solo a esa clase. La clase **Potential** no añade miembros propios; utiliza únicamente los miembros `#`.

### Control de ventana

| Función | Descripción |
|----------|-------------|
| `#.Open()` | Abrir la ventana del Simulador de imágenes |
| `#.Close()` | Cerrar la ventana del Simulador de imágenes |
| `#.Simulate()` | Ejecutar la simulación con los ajustes actuales |

### Microscopio / óptica

| Propiedad / Función | Descripción |
|---------------------|-------------|
| `#.AccVol` | Voltaje de aceleración (kV) |
| `$.Thickness` | Espesor de la muestra (nm) |
| `$.Defocus` | Desenfoque (nm) |
| `$.Cs` | Aberración esférica Cs (mm) |
| `$.Cc` | Aberración cromática Cc (mm) |
| `$.DeltaV` | Dispersión de energía ΔV, FWHM (eV) |
| `$.Scherzer` | Desenfoque de Scherzer (nm, solo lectura) |
| `STEM.ConvergenceAngle` | Semiángulo de convergencia (mrad) |
| `STEM.DetectorInnerAngle` / `STEM.DetectorOuterAngle` | Semiángulo interior/exterior del detector anular (mrad) |
| `STEM.EffectiveSourceSize` | Tamaño efectivo de la fuente, FWHM (pm) |
| `HRTEM.Beta` | Semiángulo de iluminación β (radianes) |
| `HRTEM.ApertureSemiangle` | Semiángulo del diafragma objetivo (radianes) |
| `HRTEM.ApertureShiftX` / `HRTEM.ApertureShiftY` | Desplazamiento del diafragma objetivo (radianes) |
| `HRTEM.OpenAperture` | Diafragma objetivo abierto (true/false) |

### Propiedades de simulación

| Propiedad / Función | Descripción |
|---------------------|-------------|
| `#.NumberOfDiffractedWaves` | Número máximo de ondas difractadas (de Bloch) |
| `#.ImageWidth` / `#.ImageHeight` | Tamaño de imagen (píxeles) |
| `#.ImageSize(width, height)` | Establecer el tamaño de imagen (píxeles) |
| `#.ImageResolution` | Resolución de imagen (nm/píxel) |
| `STEM.AngularResolution` | Resolución angular del haz convergente (mrad) |
| `STEM.SliceThickness` | Espesor de capa para el cálculo de TDS (nm) |
| `HRTEM.Mode_LinearImage()` | Usar el modelo de imagen lineal (cuasi-coherente) |
| `HRTEM.Mode_TCC()` | Usar el modelo TCC (coeficiente cruzado de transmisión) |

### Modo de imagen única / en serie

| Propiedad / Función | Descripción |
|---------------------|-------------|
| `$.SingleImageMode()` | Cambiar al modo de imagen única |
| `$.SerialImageMode(withThickness, withDefocus)` | Cambiar al modo de imagen en serie |
| `$.SerialImageThicknessStart` / `Step` / `Num` | Espesor en serie: inicio (nm) / paso (nm) / cantidad |
| `$.SerialImageDefocusStart` / `Step` / `Num` | Desenfoque en serie: inicio (nm) / paso (nm) / cantidad |

### Propiedades de imagen

| Propiedad / Función | Descripción |
|---------------------|-------------|
| `#.UnitCellVisible` | Mostrar la celda elemental (true/false) |
| `#.LabelVisible` | Mostrar la etiqueta de la imagen (true/false) |
| `#.LabelSize` | Tamaño de fuente de la etiqueta |
| `#.ScaleBarVisible` | Mostrar la barra de escala (true/false) |
| `#.ScaleBarLength` | Longitud de la barra de escala (nm) |
| `#.GaussianBlurEnabled` | Aplicar desenfoque gaussiano (true/false) |
| `#.GaussianBlurFWHM` | FWHM del desenfoque gaussiano (pm) |
| `STEM.DisplayBoth()` | Mostrar las componentes elástica y de TDS |
| `STEM.DisplayElastic()` | Mostrar solo la componente elástica |
| `STEM.DisplayTDS()` | Mostrar solo la componente de TDS (inelástica) |

### Guardar imagen

| Propiedad / Función | Descripción |
|---------------------|-------------|
| `#.SaveImageAsPng(filename)` | Guardar como PNG (diálogo si se omite filename) |
| `#.SaveImageAsTif(filename)` | Guardar como TIFF (diálogo si se omite filename) |
| `#.SaveImageAsEmf(filename)` | Guardar como metarchivo EMF (diálogo si se omite filename) |
| `#.SaveIndividually` | En modo serie, guardar cada imagen por separado (true/false) |
| `#.OverprintSymbols` | Sobreimprimir celda elemental / etiquetas / barra de escala en las imágenes guardadas (true/false) |

---

## Funciones globales

| Función | Descripción |
|----------|-------------|
| `Sleep(ms)` | Esperar el número de milisegundos especificado |

---

## Véase también

- [20. Macro](index.md)
- [20.2. Ejemplos](2-examples.md)
