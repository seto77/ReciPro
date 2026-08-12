# Built-in Functions

Complete reference of classes and functions available in ReciPro macros.

---

## File class

| Function | Description |
|----------|-------------|
| `File.GetDirectoryPath(filename)` | Show folder-picker dialog, return selected path; pass `filename` to get the folder that contains it instead |
| `File.GetFileName()` | Show file-picker dialog, return selected path |
| `File.GetFileNames()` | Show multi-file-picker dialog, return list of paths |
| `File.ReadCrystalList(filename)` | Load a crystal list file (*.xml); omit `filename` to open a dialog |
| `File.ReadCrystal(filename)` | Load a CIF/AMC crystal file; omit `filename` to open a dialog |
| `File.ExportAsCIF(filename)` | Export the current crystal as CIF; omit `filename` to open a dialog |
| `File.ReadText(filename)` | Read a text file (UTF-8) and return it as a string; omit `filename` to open a dialog. Pairs with `Crystal.LoadCifText()` / `SaveText()` |
| `File.SaveText(textData, filename)` | Save text data to a file; writes `textData` as UTF-8, and omitting `filename` opens a save dialog |

---

## Crystal class

Reads the currently selected crystal and, through a pending draft, creates and edits crystals.

### Reading

| Property / Function | Description |
|---|---|
| `Crystal.Name` | Crystal name |
| `Crystal.ChemicalFormula` | Chemical formula |
| `Crystal.Density` | Density (g/cm³) |
| `Crystal.GetCellInAng()` | Cell constants as `[a, b, c, alpha, beta, gamma]` (Å, degrees) |
| `Crystal.SpaceGroupName` | Hermann–Mauguin space-group symbol, with the setting suffix (`:2`, `:H`, …) where applicable |
| `Crystal.SpaceGroupNumber` | International Tables space-group number (1–230) |
| `Crystal.HasPending` | Whether a pending draft is open |

### Creating and editing (draft → Commit)

A crystal is built in a **pending draft**: start one, fill it with the setters, and `Commit()` validates everything, builds the crystal, and applies it as the current crystal in one step (the GUI and all open simulators update, as when a CIF file is loaded). A failed `Commit()` reports all validation errors together, changes nothing, and keeps the draft so it can be fixed and committed again.

| Function | Description |
|---|---|
| `Crystal.BeginCreate(name)` | Start a draft for a new crystal |
| `Crystal.BeginEdit()` | Start a draft from the current crystal (cell, space group, atoms and orientation are carried over) |
| `Crystal.LoadCifText(cifText)` | Start a draft from CIF text (the content of a .cif file, not a path) |
| `Crystal.SetName(name)` | Rename the draft |
| `Crystal.SetCellInAng(a, b, c, alpha, beta, gamma)` | Cell constants in **Å and degrees**. Each call replaces the whole cell; omitted arguments are derived from the space-group constraints (for a cubic crystal, `a` alone is enough), and explicit values that contradict them raise an error |
| `Crystal.SetSpaceGroup(symbol)` | Space group by symbol (HM short/full or Hall; spaces and `_` are ignored). Append the setting (`'Fd-3m:2'`, `'R-3c:H'`, `'P21/c:b1'`) when the group has several — ambiguous symbols raise an error listing the candidates |
| `Crystal.SetSpaceGroupByNumber(itNumber, setting)` | Space group by IT number (1–230); `setting` (`'1'`, `'2'`, `'H'`, `'R'`, `'b1'`, …) picks among multiple settings |
| `Crystal.AddAtom(label, element, x, y, z, occ, bIso)` | Add an atom of the asymmetric unit: element symbol, fractional coordinates, occupancy (0 < occ ≤ 1, default 1) and isotropic B in Å² (default 0). Equivalent positions, Wyckoff letters and multiplicities are derived automatically |
| `Crystal.ClearAtoms()` | Remove all atoms from the draft |
| `Crystal.Commit()` | Validate, build and apply the draft |
| `Crystal.Cancel()` | Discard the draft |

```python
ReciPro.Crystal.BeginCreate('NaCl')
ReciPro.Crystal.SetSpaceGroup('Fm-3m')
ReciPro.Crystal.SetCellInAng(5.6402)
ReciPro.Crystal.AddAtom('Na', 'Na', 0, 0, 0)
ReciPro.Crystal.AddAtom('Cl', 'Cl', 0.5, 0.5, 0.5)
ReciPro.Crystal.Commit()

base = ReciPro.Crystal.GetCellInAng()
for k in range(-2, 3):
    ReciPro.Crystal.BeginEdit()
    ReciPro.Crystal.SetCellInAng(base[0] * (1 + 0.01 * k))
    ReciPro.Crystal.Commit()
```

After a successful `Commit()`, the next `BeginEdit()` starts from the **updated** crystal, so changes accumulate — for absolute scans, read the base values before the loop, as above. To register the committed crystal in the crystal list, call `CrystalList.Add()`.

---

## CrystalList class

| Function / Property | Description |
|---------------------|-------------|
| `CrystalList.SelectedIndex` | Get/set selected crystal index |
| `CrystalList.Count` | Number of crystals currently in the list |
| `CrystalList.Add()` | Append current crystal to list |
| `CrystalList.Replace()` | Replace selected crystal |
| `CrystalList.Delete()` | Delete selected crystal |
| `CrystalList.ClearAll()` | Clear all crystals |
| `CrystalList.MoveUp()` | Move selected crystal up |
| `CrystalList.MoveDown()` | Move selected crystal down |

---

## Dir class

| Function | Description |
|----------|-------------|
| `Dir.Euler(phi, theta, psi)` | Set orientation by Euler angles (radians) |
| `Dir.EulerInDegree(phi, theta, psi)` | Set orientation by Euler angles (degrees) |
| `Dir.EulerInDeg(phi, theta, psi)` | Alias for `EulerInDegree` |
| `Dir.Rotate(ax, ay, az, angle)` | Rotate around arbitrary axis (radians) |
| `Dir.RotateInDeg(ax, ay, az, angle)` | Rotate around arbitrary axis (degrees) |
| `Dir.RotateAroundAxis(u, v, w, angle)` | Rotate around zone axis [uvw] (radians) |
| `Dir.RotateAroundAxisInDeg(u, v, w, angle)` | Rotate around zone axis [uvw] (degrees) |
| `Dir.RotateAroundPlane(h, k, l, angle)` | Rotate around plane normal (hkl) (radians) |
| `Dir.RotateAroundPlaneInDeg(h, k, l, angle)` | Rotate around plane normal (hkl) (degrees) |
| `Dir.ProjectAlongPlane(h, k, l)` | Set plane normal perpendicular to screen |
| `Dir.ProjectAlongAxis(u, v, w)` | Set zone axis perpendicular to screen |
| `Dir.GetEuler()` | Get the current orientation as Z-X-Z Euler angles `[phi, theta, psi]` (radians) |
| `Dir.GetEulerInDeg()` | Get the current orientation as Z-X-Z Euler angles `[phi, theta, psi]` (degrees) |
| `Dir.GetRotationMatrix()` | Get the current rotation matrix as a nine-element array `[R11, R12, R13, R21, R22, R23, R31, R32, R33]` — the same convention as `SpotID.CandidateList()` |
| `Dir.SetRotationMatrix(r11, r12, r13, r21, r22, r23, r31, r32, r33)` | Set the orientation from nine rotation-matrix elements (validated and re-orthonormalized before use) |

Euler angles are not unique at gimbal positions (θ = 0 or 180°): `GetEuler()` after `Euler()` reproduces the same attitude, but not necessarily the same numbers. To save and restore the orientation exactly, use `Dir.GetRotationMatrix()` / `Dir.SetRotationMatrix()`. The full convention is described in [4. Rotation Geometry](../4-rotation-geometry.md).

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
| `ExcitationError` | double | Spot radius (nm⁻¹): in kinematical/excitation mode, `SpotInfo()` exports reflections with \|Sg\| within this value |
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
| `SaveAsPng(filename)` | Save current pattern as PNG; omit `filename` to open a dialog |
| `SpotInfo()` | Get spot data as CSV string |

---

## SpotID class

Drives [Spot ID v2](../11-spot-id-v2.md) from a macro: load an image or a spot list, detect the spots, search for orientations and read the candidates back, without touching the window. `FindSpots()` and `Identify()` return only once the work has finished, so they can be chained directly.

### Window control

`SpotID.Open()` / `SpotID.Close()`

### Wave source

`SpotID.Source_Xray()` / `SpotID.Source_Electron()` / `SpotID.Source_Neutron()`

### Workflow

| Function | Description |
|----------|-------------|
| `SpotID.LoadFile(filename)` | Load a file as **File > Load** does: `.csv` is read as a spot list (an image must be loaded first), any other extension as a diffraction pattern image (dm3, dm4, mrc, ipa, tif and other supported formats). Omit `filename` to open a file dialog |
| `SpotID.FindSpots()` | Detect the spots in the loaded image and fit them, as the **Find spots** button does |
| `SpotID.Identify()` | Search for orientations that explain the detected spots, as the **Identify spots** button does, and return the number of candidates. The crystals tested are those selected in the crystal list of the main window |
| `SpotID.CandidateList()` | Return the candidate orientation list as CSV text |
| `SpotID.SpotList()` | Return the observed spots as CSV text, with the same columns as **File > Save**. Pair it with `File.SaveText()` to write a file that `LoadFile()` can read back |

`CandidateList()` gives, for each candidate: crystal name, the Z-X-Z Euler angles (deg), the nine rotation-matrix elements R11–R33 (crystal frame to laboratory frame, applied to column vectors), the mean-squared residual (nm⁻²), and the assignment of observed spots to *hkl* indices. Candidates come ordered by the number of assigned spots (descending), then by the residual (ascending). Numbers are written in the invariant culture, so the decimal separator is always a period.

### Properties

| Property | Type | Description |
|----------|------|-------------|
| `Energy` | double | Beam energy (keV for X-rays and electrons, meV for neutrons) |
| `CameraLength` | double | Camera length (mm) |
| `PixelSizeInMM` | double | Pixel size (mm); reading or writing it also switches the pixel-size unit to mm |
| `PixelSizeInNMinv` | double | Pixel size (nm⁻¹); reading or writing it also switches the unit to nm⁻¹ |
| `MaxNumberOfSpots` | int | Maximum number of spots `FindSpots()` may detect |
| `NearestNeighbor` | int | Minimum separation allowed between detected spots (pixels) |
| `FittingRange` | double | Radius of the region around each spot used for peak fitting (pixels) |
| `AcceptableError` | double | Tolerance of the relative *d*-spacing difference when matching spots to reflections (%) |
| `IgnoreProhibitedReflections` | bool | Ignore kinematically forbidden reflections, which can still appear via multiple diffraction |
| `MultiGrain` | bool | Search for several grains; `False` means a single grain |
| `MaxNumberOfGrains` | int | Maximum number of grain orientations searched when `MultiGrain` is `True` |
| `NumberOfDetectedSpots` | int | Number of detected spots (read-only) |
| `NumberOfCandidates` | int | Number of candidates found by the last `Identify()` (read-only) |

---

## StructureViewer class

Drives the Structure Viewer window from a macro. `SaveImage()` and `Export3DModel()` open the window first when necessary, because the 3D model is built when the window is shown.

| Function | Description |
|---|---|
| `StructureViewer.Open()` | Open the Structure Viewer window |
| `StructureViewer.Close()` | Close the Structure Viewer window |
| `StructureViewer.SaveImage(filename)` | Save the rendered main view as a PNG file, at the pixel size given by the **Size (W×H)** box; omit `filename` to open a dialog |
| `StructureViewer.Export3DModel(filename, maxSizeInMM, fixedScaleInMMperNm, includeAtoms, includeBonds, includePolyhedra, polyhedraAsEdges, polyEdgeDiaInMM, includeCellEdges, cellEdgeDiaInMM, thickenBondsToMM)` | Export the displayed structure for 3D printing, like **Export 3D Model (3MF/STL)** in the File menu. The extension picks the format (`.stl` single color / `.3mf` colored by element); only `filename` is required — the other defaults equal the dialog defaults (largest dimension 80 mm, unit-cell edges ⌀2.4 mm, bonds thickened to ⌀1.2 mm). Pass `fixedScaleInMMperNm` > 0 to build several models at the same scale |

```python
ReciPro.StructureViewer.Export3DModel('D:/print/NaCl_60mm.stl', maxSizeInMM=60)
ReciPro.StructureViewer.Export3DModel('D:/print/NaCl_edges.stl', maxSizeInMM=60, polyhedraAsEdges=True)
```

---

## HRTEM / STEM / Potential classes

These three image-simulation classes share many members. To avoid repetition, the tables below use placeholders:

- **`#`** : common to **HRTEM**, **STEM** and **Potential**. Replace `#` with `HRTEM`, `STEM`, or `Potential` (e.g. `STEM.Simulate()`, `Potential.AccVol`).
- **`$`** : common to **HRTEM** and **STEM** only. Replace `$` with `HRTEM` or `STEM`.
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

- [20. Macro](index.md)
- [20.2. Examples](2-examples.md)
