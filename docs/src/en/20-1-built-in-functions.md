<!-- nav -->

[← 20. Macro](20-macro/index.md)  |  [🏠 Home](../index.md)  |  [20.2. Examples →](20-2-examples.md)

# Built-in Functions

Complete reference of classes and functions available in ReciPro macros.

---

## File class

| Function | Description |
|----------|-------------|
| `File.GetDirectoryPath()` | Show folder-picker dialog, return selected path |
| `File.GetFileName()` | Show file-picker dialog, return selected path |
| `File.GetFileNames()` | Show multi-file-picker dialog, return list of paths |
| `File.ReadCrystalList()` | Load a crystal list file (*.xml) |
| `File.ReadCrystal()` | Load a CIF/AMC crystal file |
| `File.ExportAsCIF()` | Export the current crystal as CIF |
| `File.SaveText()` | Save text data to a file |

---

## Crystal class

| Property | Type | Description |
|----------|------|-------------|
| `Crystal.Name` | string | Crystal name |
| `Crystal.ChemicalFormula` | string | Chemical formula |
| `Crystal.Density` | double | Density (g/cm³) |

---

## CrystalList class

| Function / Property | Description |
|---------------------|-------------|
| `CrystalList.SelectedIndex` | Get/set selected crystal index |
| `CrystalList.Add()` | Append current crystal to list |
| `CrystalList.Replace()` | Replace selected crystal |
| `CrystalList.Delete()` | Delete selected crystal |
| `CrystalList.ClearAll()` | Clear all crystals |
| `CrystalList.MoveUp()` | Move selected crystal up |
| `CrystalList.MoveDown()` | Move selected crystal down |

---

## Direction class

| Function | Description |
|----------|-------------|
| `Direction.Euler(phi, theta, psi)` | Set orientation by Euler angles (radians) |
| `Direction.EulerInDegree(phi, theta, psi)` | Set orientation by Euler angles (degrees) |
| `Direction.EulerInDeg(phi, theta, psi)` | Alias for `EulerInDegree` |
| `Direction.Rotate(ax, ay, az, angle)` | Rotate around arbitrary axis (radians) |
| `Direction.RotateInDeg(ax, ay, az, angle)` | Rotate around arbitrary axis (degrees) |
| `Direction.RotateAroundAxis(u, v, w, angle)` | Rotate around zone axis [uvw] (radians) |
| `Direction.RotateAroundAxisInDeg(u, v, w, angle)` | Rotate around zone axis [uvw] (degrees) |
| `Direction.RotateAroundPlane(h, k, l, angle)` | Rotate around plane normal (hkl) (radians) |
| `Direction.RotateAroundPlaneInDeg(h, k, l, angle)` | Rotate around plane normal (hkl) (degrees) |
| `Direction.ProjectAlongPlane(h, k, l)` | Set plane normal perpendicular to screen |
| `Direction.ProjectAlongAxis(u, v, w)` | Set zone axis perpendicular to screen |

---

## DifSim class

### Window control

`DifSim.Open()` / `DifSim.Close()`

### Wave source

`DifSim.Source_Xray()` / `DifSim.Source_Electron()` / `DifSim.Source_Neutron()`

### Properties

| Property | Type | Description |
|----------|------|-------------|
| `Energy` | double | Energy (keV) |
| `Wavelength` | double | Wavelength (Å) |
| `Thickness` | double | Specimen thickness (nm) |
| `NumberOfDiffractedWaves` | int | Number of Bloch waves |
| `CameraLength2` | double | Camera length (mm) |
| `SkipRendering` | bool | Skip rendering for batch processing |

### Beam mode

`Beam_Parallel()` / `Beam_PrecessionXray()` / `Beam_PrecessionElectron()` / `Beam_Convergence()`

### Calculation mode

`Calc_Excitation()` / `Calc_Kinematical()` / `Calc_Dynamical()`

### Image settings

| Property / Function | Description |
|---------------------|-------------|
| `ImageResolutionInMM` | Resolution (mm/pixel) |
| `ImageResolutionInNMinv` | Resolution (nm⁻¹/pixel) |
| `ImageWidth` / `ImageHeight` | Image size (pixels) |
| `ImageSize(w, h)` | Set image size |

### Detector

| Property | Description |
|----------|-------------|
| `Tau` / `TauInDeg` | Detector tilt angle τ (rad / deg) |
| `Phi` / `PhiInDeg` | Detector rotation axis φ (rad / deg) |
| `Foot(x, y)` | Foot position in pixels |

### Output

| Function | Description |
|----------|-------------|
| `SaveAsPng()` | Save current pattern as PNG |
| `SpotInfo()` | Get spot data as CSV string |

---

## HRTEM / STEM / Potential classes

These three image-simulation classes share many members. To avoid repetition, the tables below use placeholders:

- **`#`** — common to **HRTEM**, **STEM** and **Potential**. Replace `#` with `HRTEM`, `STEM`, or `Potential` (e.g. `STEM.Simulate()`, `Potential.AccVol`).
- **`$`** — common to **HRTEM** and **STEM** only. Replace `$` with `HRTEM` or `STEM`.
- Members written with an explicit class name (`STEM.…` / `HRTEM.…`) belong to that class only. The **Potential** class adds no members of its own; it uses only the `#` members.

### Window control

| Function | Description |
|----------|-------------|
| `#.Open()` | Open the Image Simulator window |
| `#.Close()` | Close the Image Simulator window |
| `#.Simulate()` | Run the simulation with the current settings |

### Microscope / optics

| Property / Function | Description |
|---------------------|-------------|
| `#.AccVol` | Accelerating voltage (kV) |
| `$.Thickness` | Specimen thickness (nm) |
| `$.Defocus` | Defocus (nm) |
| `$.Cs` | Spherical aberration Cs (mm) |
| `$.Cc` | Chromatic aberration Cc (mm) |
| `$.DeltaV` | Energy spread ΔV, FWHM (eV) |
| `$.Scherzer` | Scherzer defocus (nm, get only) |
| `STEM.ConvergenceAngle` | Convergence semi-angle (mrad) |
| `STEM.DetectorInnerAngle` / `STEM.DetectorOuterAngle` | Annular detector inner/outer semi-angle (mrad) |
| `STEM.EffectiveSourceSize` | Effective source size, FWHM (pm) |
| `HRTEM.Beta` | Illumination semi-angle β (radians) |
| `HRTEM.ApertureSemiangle` | Objective-aperture semi-angle (radians) |
| `HRTEM.ApertureShiftX` / `HRTEM.ApertureShiftY` | Objective-aperture shift (radians) |
| `HRTEM.OpenAperture` | Objective aperture open (true/false) |

### Simulation properties

| Property / Function | Description |
|---------------------|-------------|
| `#.NumberOfDiffractedWaves` | Max number of diffracted (Bloch) waves |
| `#.ImageWidth` / `#.ImageHeight` | Image size (pixels) |
| `#.ImageSize(width, height)` | Set the image size (pixels) |
| `#.ImageResolution` | Image resolution (nm/pixel) |
| `STEM.AngularResolution` | Angular resolution of the convergent beam (mrad) |
| `STEM.SliceThickness` | Slice thickness for TDS calculation (nm) |
| `HRTEM.Mode_LinearImage()` | Use the linear-image (quasi-coherent) model |
| `HRTEM.Mode_TCC()` | Use the TCC (transmission cross coefficient) model |

### Single / serial image mode

| Property / Function | Description |
|---------------------|-------------|
| `$.SingleImageMode()` | Switch to single-image mode |
| `$.SerialImageMode(withThickness, withDefocus)` | Switch to serial-image mode |
| `$.SerialImageThicknessStart` / `Step` / `Num` | Serial thickness: start (nm) / step (nm) / count |
| `$.SerialImageDefocusStart` / `Step` / `Num` | Serial defocus: start (nm) / step (nm) / count |

### Image properties

| Property / Function | Description |
|---------------------|-------------|
| `#.UnitCellVisible` | Show the unit cell (true/false) |
| `#.LabelVisible` | Show the image label (true/false) |
| `#.LabelSize` | Label font size |
| `#.ScaleBarVisible` | Show the scale bar (true/false) |
| `#.ScaleBarLength` | Scale-bar length (nm) |
| `#.GaussianBlurEnabled` | Apply Gaussian blur (true/false) |
| `#.GaussianBlurFWHM` | Gaussian-blur FWHM (pm) |
| `STEM.DisplayBoth()` | Show both elastic and TDS components |
| `STEM.DisplayElastic()` | Show the elastic component only |
| `STEM.DisplayTDS()` | Show the TDS (inelastic) component only |

### Save image

| Property / Function | Description |
|---------------------|-------------|
| `#.SaveImageAsPng(filename)` | Save as PNG (dialog if filename omitted) |
| `#.SaveImageAsTif(filename)` | Save as TIFF (dialog if filename omitted) |
| `#.SaveImageAsEmf(filename)` | Save as EMF metafile (dialog if filename omitted) |
| `#.SaveIndividually` | In serial mode, save each image separately (true/false) |
| `#.OverprintSymbols` | Overprint unit cell / labels / scale bar on saved images (true/false) |

---

## Global functions

| Function | Description |
|----------|-------------|
| `Sleep(ms)` | Wait for specified milliseconds |

---

## See also

- [20. Macro](20-macro/index.md)
- [20.2. Examples](20-2-examples.md)

---

[← 20. Macro](20-macro/index.md)  |  [🏠 Home](../index.md)  |  [20.2. Examples →](20-2-examples.md)
