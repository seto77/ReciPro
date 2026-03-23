#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""Generate wiki pages for ReciPro GitHub wiki."""
import os

WIKI_DIR = "/tmp/ReciPro.wiki"

files = {}

files["Home.md"] = r"""## Brief introduction
* ReciPro is MIT-licensed free software that provides a variety of crystallographic calculations and electron microscopy simulations.
* ReciPro has been downloaded more than 10,000 times in total since its release on github (Mar 2020) and is used by many crystallographers and electron microscopists.

[Various simulations being performed in real time (sample: SrTiO3)](https://user-images.githubusercontent.com/44538886/123412384-7e20a980-d5ec-11eb-86fd-921c37ce460c.mp4)

## Features
* **Full GUI** — All operations are performed through a graphical interface. Most file I/O supports drag & drop.
* **Crystal list** — Handle multiple crystals at once; no need to open separate windows for each crystal.
* **Space group database** — Built-in database covering 230 space groups from International Tables Volume A, plus 530 Hall symbols, with symmetry elements, Wyckoff positions, and extinction rules.
* **Atomic information** — Scattering factors (X-ray, electron, neutron), characteristic X-ray energies, isotope ratios, etc. for elements H (1) - Cf (98).
* **Flexible crystal rotation** — Set orientation by zone-axis/crystal-plane indices or by mouse drag. Rotation state is synchronised across all simulation windows.
* **Diffraction simulation** — Kinematical and dynamical (Bloch wave) electron diffraction, X-ray diffraction, precession electron diffraction (PED), and convergent-beam electron diffraction (CBED).
* **HRTEM / STEM simulation** — High-resolution TEM image simulation with partial-coherence models; STEM with thermal diffuse scattering.
* **Spot indexing** — Automatic detection, fitting, and indexing of diffraction spots from experimental images.
* **Macro** — Python-syntax macro for automating operations (see [[20. Macro]]).

## System requirements
| Item | Minimum | Recommended |
|------|---------|-------------|
| OS | Windows with [.NET Desktop Runtime 9.0](https://dotnet.microsoft.com/download/dotnet/9.0) | Windows 11 |
| GPU | OpenGL 1.3 | External GPU with OpenGL 1.5 |
| Memory | - | 16 GB or more |
| CPU | - | 8+ cores (for dynamical calculations) |

## Manual
* **English (PDF):** https://raw.githubusercontent.com/seto77/ReciPro/master/ReciPro/doc/ReciProManual(en).pdf
* **Japanese (PDF):** https://raw.githubusercontent.com/seto77/ReciPro/master/ReciPro/doc/ReciProManual(ja).pdf

## Quick start
1. Download and install from [Releases](https://github.com/seto77/ReciPro/releases/latest).
2. Select a crystal from the built-in list (~80 crystals). You can also import CIF files or use [CSManager](https://github.com/seto77/CSManager).
3. Call functions from the right-hand panel: Structure Viewer, Stereonet, Crystal Diffraction, HRTEM simulation, etc.
4. Rotate the crystal by mouse drag or by entering zone-axis/plane indices.

## Reference
> Y. Seto, "ReciPro: free and open-source multipurpose crystallographic software integrating a crystal operation interface and diffraction simulators," *J. Appl. Cryst.* **55**, 397-410 (2022). https://doi.org/10.1107/S1600576722000139

## License
ReciPro is distributed under the [MIT License](https://github.com/seto77/ReciPro/blob/master/LICENSE.md).
"""

files["0.-Main-window.md"] = r"""# Main Window

When ReciPro launches, the main window appears. From this window you select the crystal, control its rotation, and invoke various functions.

| Area | Location | Description |
|------|----------|-------------|
| **File menu** | Top | File operations, options, help |
| **Rotation control** | Left | View/set crystal orientation |
| **Crystal List** | Upper centre | Select and manage crystals |
| **Crystal Information** | Lower centre | Edit lattice parameters, symmetry, atoms |
| **Functions** | Right | Launch simulation/analysis windows |

---

## File menu

### File
| Menu item | Description |
|-----------|-------------|
| Read crystal list (as new list) | Load a crystal list file (*.xml), replacing the current list |
| Read crystal list (and add) | Append to the current list |
| Read initial crystal list | Reload the default crystal list |
| Save crystal list | Save the current crystal list |
| Export selected crystal to CIF | Save in CIF format |
| Clear crystal list | Remove all crystals |
| Exit | Close the application |

### Option
| Menu item | Description |
|-----------|-------------|
| Tool tip | Toggle tooltip display |
| Reset registry (after restart) | Reset settings on next restart |
| Disable OpenGL (needs restart) | For older GPUs / remote desktop |

### Help
Program updates, version history, license, GitHub page, bug reports, PDF manual, language switch (English/Japanese, requires restart).

---

## Rotation control

### Current direction
Shows crystal orientation. Drag to rotate. Axes: red = *a*, green = *b*, blue = *c*.

### Reset rotation
Resets to initial: *c*-axis perpendicular to screen, *b*-axis upward.

### Zone axis
Displays closest zone axis to screen normal (e.g., *u*+*v*+*w* < 30).

### Euler angles (Z-X-Z)
- **Psi**: Z-axis rotation, **Theta**: X-axis rotation, **Phi**: Z-axis rotation
- See [[3. Rotation Geometry]] and [[Appendix A1. Coordinate System]].

### Arrow buttons / Animation
Rotate by angle Step. Check Animation for continuous rotation.

### Project along...
Align a zone axis [*uvw*] or crystal plane (*hkl*) perpendicular to the screen.

---

## Crystal List

~80 crystals in default installation. Select to view details and set for calculations.

| Button | Action |
|--------|--------|
| Up / Down | Reorder |
| Delete / All clear | Remove crystals |
| Add / Replace | Add or replace entry |

---

## Crystal Information

Edit lattice parameters, symmetry, and atoms. Drag & drop CIF/AMC files.

> **Important**: Press **Add** or **Replace** to save changes.

---

## Functions

| Button | Description | Details |
|--------|-------------|---------|
| Symmetry information | Space group info | - |
| Scattering factor | Crystal planes & structure factors | - |
| Rotation geometry | 3D rotation matrix | [[3. Rotation Geometry]] |
| Structure viewer | 3D crystal structure | [[5. Structure viewer]] |
| Stereonet | Stereonet projection | [[6. Stereonet]] |
| Crystal diffraction | Diffraction simulation | [[7. Diffraction simulator]] |
| HRTEM simulation | HRTEM image simulation | [[8. HRTEM/STEM simulator]] |
| TEM ID | SAED pattern indexing | [[9. TEM ID]] |
| Spot ID | Spot detection & indexing | [[10. Spot ID]] |
| Powder diffraction | Polycrystalline diffraction | - |
"""

files["1.-Crystal-database.md"] = r"""# Crystal Database

The **Crystal Database** provides functions to search and import more than 20,000 crystal structures.

Based on the [American Mineralogist Crystal Structure Database](http://rruff.geo.arizona.edu/AMS/amcsd.php). Please cite:

> Downs, R.T. and Hall-Wallace, M. (2003) The American Mineralogist Crystal Structure Database. *American Mineralogist* **88**, 247-250.

[How to search for crystals in the embedded database](https://user-images.githubusercontent.com/44538886/218979766-b9596f88-79d0-4b1f-b234-4288e62393c7.mp4)

---

## Table

Displays crystals matching search criteria. Select a crystal to transfer to Main window's Crystal Information. Press **Add** or **Replace** to add to Crystal List.

---

## Search options

| Criterion | Description |
|-----------|-------------|
| **Name** | Crystal name |
| **Element** | Periodic table selector (may/must/must-not include) |
| **Reference** | Title, journal, author |
| **Crystal system** | Select crystal system |
| **Cell Param** | Lattice constants and error |
| **d-spacing** | Strongest reflection d-values and error |
| **Density** | Density and error |
"""

files["5.-Structure-viewer.md"] = r"""# Structure Viewer

**Structure Viewer** draws the selected crystal as a three-dimensional image using OpenGL.

[](https://user-images.githubusercontent.com/44538886/225242717-879da50c-0e84-487f-aace-eb6ed835dcc4.mp4)

---

## Main area

3D crystal structure with light source, crystal axes, and atom legend.

| Operation | Action |
|-----------|--------|
| Left drag | Rotate |
| Centre drag | Translate |
| Right drag/wheel | Zoom |
| Left double-click | Select/deselect atom |
| Ctrl+Right double-click | Toggle perspective/orthogonal |

---

## File menu

Save image, copy to clipboard (Ctrl+Shift+C), save movie (MP4).

***
- ***File => Save movie*** allows you to save a movie. Set the rotation speed, recording duration, and rotation direction, and press OK button to generate the mp4 movie file.

<img src="https://user-images.githubusercontent.com/44538886/225556248-bbdb7c7a-1e63-4f15-8ceb-c8cbb1322169.png" width="15%" />

---

## Tab menu

### Bounds
Drawing range by unit cell or crystal planes. Bound planes, clipping, hide atoms.

### Atoms
Coordinates, element, occupancy, radius, colour, material. **Apply to same elements**.

### Bonds (& Polyhedra)
Bond length thresholds, polyhedron display, edges.

### Unit cell
Translation, cell planes, edges.

### Lattice plane
Miller index specification with crystallographic equivalents.

### Coordinate information
Coordination table and graph around target atom.

### Misc.
Accessory panel, label, projection (orthographic/perspective), depth fading, rendering quality, transparency mode.

---

## Toolbar

| Button | Description |
|--------|-------------|
| Crystal Axes | Show axis orientation (size = lattice constant) |
| Lightning ball | Set light direction |
| Legend | Atom legend |
| Like Vesta | Vesta-style appearance |
"""

files["6.-Stereonet.md"] = r"""# Stereonet

**Stereonet** displays crystal plane and axis directions using stereographic projection.

[](https://user-images.githubusercontent.com/44538886/225527569-53d4af64-e7e4-49b3-904a-e49a7ef63170.mp4)

---

## Mouse operations
- Left-drag to rotate the crystal
- Right-drag/click to zoom in/out
- Double-click to switch between planes/axes modes.
- Middle-drag to translate.

## File menu
Save or copy in raster or vector format. Vector format allows editing font/line thickness in PowerPoint.

## Mode
- **Axis** or **Plane** projection
- **Wulff** (equal-angle) or **Schmidt** (equal-area)

## Indices
- **Range**: specify [uvw] or {hkl} range
- **Specified**: add individual indices, with optional crystallographic equivalents

## Tab menu
- **Appearance**: string size, point size, colour, outline
- **Great and Small Circle**: specify by zone axis or crystal plane indices
"""

files["7.-Diffraction-simulator.md"] = r"""# Crystal Diffraction (Diffraction Simulator)

**Crystal Diffraction** simulates single-crystal X-ray and electron diffraction patterns.

[](https://user-images.githubusercontent.com/44538886/225580329-849f129b-a4c1-485f-9ae3-b64509b5b415.mp4)

---

## Main area

| Operation | Action |
|-----------|--------|
| Left drag | Rotate |
| Centre drag | Translate |
| Right drag | Zoom in |
| Right click | Zoom out |
| Left double-click | Spot details |

---

## Tab menu

### Wave
X-ray (characteristic/synchrotron), Electron, Neutron. Set energy or wavelength.

### Spot property

**Incident beam**: Parallel, Precession (PED), Convergence (CBED)

**Intensity calculation**:
- Only excitation error
- Kinematical & excitation error
- Dynamical theory (Bloch wave, electron only)

**Appearance**: Solid sphere or Gaussian. Opacity, radius, brightness, colour scale.

**Bloch wave**: number of waves, sample thickness.

### Kikuchi lines / Debye rings / Scale
Toggle from toolbar; set threshold, colour, labels.

---

## Toolbar

Spots, Kikuchi lines, Debye rings, Scale, label options (Index/d/Distance/Excit.Err/|Fg|).

---

## CBED setting

Press **Execute** to run. Max Bloch waves, convergence angle, disk resolution, thickness range, solver (Auto/MKL/Eigen/Managed).

---

## Detector geometry

Detailed detector settings, schematic diagram, overlapped image. See [[Appendix A2. Detector Coordinate System]].

---

## Diffraction spot information

Detailed Bethe dynamical theory spot data with schematic.
"""

files["8.-HRTEM-STEM-simulator.md"] = r"""# HRTEM / STEM Simulator

**HRTEM simulation** simulates TEM lattice fringe images. Click **Simulate image** to run.

[HRTEM simulation](https://user-images.githubusercontent.com/44538886/224967658-41df6d2c-1247-4c45-84e8-210b8e5bb263.mp4)

---

## Main area

Simulated image with Min/Max brightness, colour scale, Gaussian blur.

---

## Image mode

HRTEM, Potential, or STEM.

---

## Optical property

- **TEM condition**: Acc. voltage, defocus (Scherzer shown)
- **Inherent**: Cs, Cc, beta, delta-E
- **Lens function**: PCTF, spatial/temporal coherence envelopes
- **Objective aperture**: size, shift, spot count depends on aperture

---

## Simulation property

- Max Bloch waves, image pixels/resolution
- **Partial coherence**: quasi-coherent (fast) or TCC (accurate)
- **Single/Serial mode**: multiple thicknesses and defocuses

---

## Appearance

Label (thickness/defocus), scale bar, unit cell overlay.

---

## STEM simulation

[](https://user-images.githubusercontent.com/44538886/225196071-ff81d19f-ef3b-4238-a7aa-0804203b0f13.mp4)

Computation depends on: convergence angle, Bloch wave count, angular resolution.

| Detector | Contribution |
|----------|-------------|
| BF, ABF | Elastic |
| LAADF, HAADF | Inelastic (TDS) |

> Set temperature factors non-zero for TDS (B = 0.5 A^2 if unsure). HAADF intensity ~ Z^2.

STEM parameters: convergence angle, detector angles, source size, slice thickness, angular resolution.

[Comparison: Dr. Probe vs ReciPro](https://github.com/seto77/ReciPro/files/10976084/ComparisonSTEMsimulations.pdf)
![STEM comparison](https://user-images.githubusercontent.com/44538886/225209007-2c39080d-d402-4d39-8178-836bd5abee67.png)
"""

files["_Sidebar.md"] = r"""**English**
* [[Home]]
* [[0. Main window]]
  * [[0.1. Crystal orientation control]]
* [[1. Crystal database]]
* [[3. Rotation Geometry]]
* [[5. Structure viewer]]
* [[6. Stereonet]]
* [[7. Diffraction simulator]]
  * [[7.1. SAED simulation]]
  * [[7.2. X ray diffraction]]
  * [[7.3. PED Simulation]]
* [[8. HRTEM/STEM simulator]]
  * [[8.1. HRTEM simulation]]
  * [[8.2. STEM simulation]]
  * [[8.3. Potential simulation]]
* [[9. TEM ID]]
* [[10. Spot ID]]
* [[20. Macro]]
  * [[20.1. Built-in functions]]
  * [[20.2. Examples]]
* Appendix
  * [[Appendix A1. Coordinate System]]
  * [[Appendix A2. Detector Coordinate System]]
* [[Troubleshooting]]

---

**日本語**
* [[Home-ja|ホーム]]
* [[0. Main window-ja|メインウィンドウ]]
* [[1. Crystal database-ja|結晶データベース]]
* [[3. Rotation Geometry-ja|回転ジオメトリ]]
* [[5. Structure viewer-ja|Structure Viewer]]
* [[6. Stereonet-ja|ステレオネット]]
* [[7. Diffraction simulator-ja|回折シミュレータ]]
* [[8. HRTEM/STEM simulator-ja|HRTEM/STEMシミュレータ]]
* [[9. TEM ID-ja|TEM ID]]
* [[10. Spot ID-ja|Spot ID]]
* 付録
  * [[Appendix A1. Coordinate System-ja|座標系の定義]]
  * [[Appendix A2. Detector Coordinate System-ja|検出器座標系]]
"""

for fname, content in files.items():
    path = os.path.join(WIKI_DIR, fname)
    with open(path, 'w', encoding='utf-8') as f:
        f.write(content.lstrip('\n'))
    print(f"Created {fname} ({len(content.strip().splitlines())} lines)")
