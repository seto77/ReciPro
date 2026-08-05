# Funciones integradas

Referencia completa de las clases y funciones disponibles en las macros de ReciPro.

---

## Clase File

| Función | Descripción |
|----------|-------------|
| `File.GetDirectoryPath(filename)` | Mostrar el diálogo de selección de carpeta y devolver la ruta elegida; pasa `filename` para obtener en su lugar la carpeta que lo contiene |
| `File.GetFileName()` | Mostrar el diálogo de selección de archivo y devolver la ruta elegida |
| `File.GetFileNames()` | Mostrar el diálogo de selección de múltiples archivos y devolver la lista de rutas |
| `File.ReadCrystalList(filename)` | Cargar un archivo de lista de cristales (*.xml); omite `filename` para abrir un diálogo |
| `File.ReadCrystal(filename)` | Cargar un archivo de cristal CIF/AMC; omite `filename` para abrir un diálogo |
| `File.ExportAsCIF(filename)` | Exportar el cristal actual como CIF; omite `filename` para abrir un diálogo |
| `File.SaveText(textData, filename)` | Guardar datos de texto en un archivo; escribe `textData` en UTF-8 y, si se omite `filename`, abre un diálogo de guardado |

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
| `CrystalList.Count` | Número de cristales que hay en la lista |
| `CrystalList.Add()` | Añadir el cristal actual a la lista |
| `CrystalList.Replace()` | Reemplazar el cristal seleccionado |
| `CrystalList.Delete()` | Eliminar el cristal seleccionado |
| `CrystalList.ClearAll()` | Vaciar todos los cristales |
| `CrystalList.MoveUp()` | Mover el cristal seleccionado hacia arriba |
| `CrystalList.MoveDown()` | Mover el cristal seleccionado hacia abajo |

---

## Clase Dir

| Función | Descripción |
|----------|-------------|
| `Dir.Euler(phi, theta, psi)` | Establecer la orientación mediante ángulos de Euler (radianes) |
| `Dir.EulerInDegree(phi, theta, psi)` | Establecer la orientación mediante ángulos de Euler (grados) |
| `Dir.EulerInDeg(phi, theta, psi)` | Alias de `EulerInDegree` |
| `Dir.Rotate(ax, ay, az, angle)` | Rotar alrededor de un eje arbitrario (radianes) |
| `Dir.RotateInDeg(ax, ay, az, angle)` | Rotar alrededor de un eje arbitrario (grados) |
| `Dir.RotateAroundAxis(u, v, w, angle)` | Rotar alrededor del eje de zona [uvw] (radianes) |
| `Dir.RotateAroundAxisInDeg(u, v, w, angle)` | Rotar alrededor del eje de zona [uvw] (grados) |
| `Dir.RotateAroundPlane(h, k, l, angle)` | Rotar alrededor de la normal del plano (hkl) (radianes) |
| `Dir.RotateAroundPlaneInDeg(h, k, l, angle)` | Rotar alrededor de la normal del plano (hkl) (grados) |
| `Dir.ProjectAlongPlane(h, k, l)` | Situar la normal del plano perpendicular a la pantalla |
| `Dir.ProjectAlongAxis(u, v, w)` | Situar el eje de zona perpendicular a la pantalla |

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
| `SaveAsPng(filename)` | Guardar el patrón actual como PNG; omite `filename` para abrir un diálogo |
| `SpotInfo()` | Obtener los datos de reflexiones como cadena CSV |

---

## Clase SpotID

Controla [Spot ID v2](../11-spot-id-v2.md) desde una macro: cargar una imagen o una lista de puntos, detectar los puntos, buscar orientaciones y recuperar los candidatos, sin tocar la ventana. `FindSpots()` e `Identify()` solo regresan cuando el trabajo ha terminado, así que pueden encadenarse directamente.

### Control de la ventana

`SpotID.Open()` / `SpotID.Close()`

### Fuente de onda

`SpotID.Source_Xray()` / `SpotID.Source_Electron()` / `SpotID.Source_Neutron()`

### Flujo de trabajo

| Función | Descripción |
|---------|-------------|
| `SpotID.LoadFile(filename)` | Cargar un archivo igual que **File > Load**: `.csv` se lee como lista de puntos (antes debe haberse cargado una imagen) y cualquier otra extensión como imagen de patrón de difracción (dm3, dm4, mrc, ipa, tif y otros formatos admitidos). Omite `filename` para abrir un diálogo de archivo |
| `SpotID.FindSpots()` | Detectar los puntos de la imagen cargada y ajustarlos, como hace el botón **Find spots** |
| `SpotID.Identify()` | Buscar orientaciones que expliquen los puntos detectados, como hace el botón **Identify spots**, y devolver el número de candidatos. Se prueban los cristales seleccionados en la lista de cristales de la ventana principal |
| `SpotID.CandidateList()` | Devolver la lista de orientaciones candidatas como texto CSV |
| `SpotID.SpotList()` | Devolver los puntos observados como texto CSV, con las mismas columnas que **File > Save**. Combínalo con `File.SaveText()` para escribir un archivo que `LoadFile()` pueda volver a leer |

`CandidateList()` da, por cada candidato: nombre del cristal, los ángulos de Euler Z-X-Z (grados), los nueve elementos R11–R33 de la matriz de rotación (del sistema del cristal al del laboratorio, aplicada a vectores columna), el residuo cuadrático medio (nm⁻²) y la asignación de los puntos observados a índices *hkl*. Los candidatos vienen ordenados por número de puntos asignados (descendente) y luego por el residuo (ascendente). Los números se escriben en la cultura invariante, así que el separador decimal es siempre un punto.

### Propiedades

| Propiedad | Tipo | Descripción |
|-----------|------|-------------|
| `Energy` | double | Energía del haz (keV para rayos X y electrones, meV para neutrones) |
| `CameraLength` | double | Longitud de cámara (mm) |
| `PixelSizeInMM` | double | Tamaño de píxel (mm); leerlo o escribirlo cambia también la unidad del tamaño de píxel a mm |
| `PixelSizeInNMinv` | double | Tamaño de píxel (nm⁻¹); leerlo o escribirlo cambia también la unidad a nm⁻¹ |
| `MaxNumberOfSpots` | int | Número máximo de puntos que `FindSpots()` puede detectar |
| `NearestNeighbor` | int | Separación mínima permitida entre puntos detectados (píxeles) |
| `FittingRange` | double | Radio de la región alrededor de cada punto empleada en el ajuste del pico (píxeles) |
| `AcceptableError` | double | Tolerancia de la diferencia relativa de espaciado *d* al emparejar puntos con reflexiones (%) |
| `IgnoreProhibitedReflections` | bool | Ignorar las reflexiones prohibidas cinemáticamente, que aun así pueden aparecer por difracción múltiple |
| `MultiGrain` | bool | Buscar varios granos; `False` significa un solo grano |
| `MaxNumberOfGrains` | int | Número máximo de orientaciones de grano buscadas cuando `MultiGrain` es `True` |
| `NumberOfDetectedSpots` | int | Número de puntos detectados (solo lectura) |
| `NumberOfCandidates` | int | Número de candidatos hallados por el último `Identify()` (solo lectura) |

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
