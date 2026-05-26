# ReciPro Manual

## Brief introduction
* ReciPro is MIT-licensed free software that provides a variety of crystallographic calculations and electron microscopy simulations.
* ReciPro has been downloaded more than 27,000 times in total since its release on github (Mar 2020) and is used by many crystallographers and electron microscopists.

## Find by Goal

| Goal | Start here | Main next steps |
|------|------------|-----------------|
| Load a crystal and set its orientation | [Main window](en/0-main-window/index.md) | [Rotation Geometry](en/4-rotation-geometry.md), [Appendix A1. Coordinate systems](en/appendix/a1-coordinate-system/1-orientation.md) |
| Inspect a crystal structure in 3D | [Structure Viewer](en/5-structure-viewer.md) | [Symmetry information](en/2-symmetry-information.md) |
| Calculate SAED / XRD / PED / CBED patterns | [Diffraction simulator](en/7-diffraction-simulator/index.md) | [SAED](en/7-diffraction-simulator/1-saed-simulation.md), [X-ray diffraction](en/7-diffraction-simulator/2-x-ray-diffraction.md), [PED](en/7-diffraction-simulator/3-ped-simulation.md), [CBED](en/7-diffraction-simulator/4-cbed-simulation.md) |
| Calculate HRTEM / STEM images | [HRTEM/STEM simulator](en/9-hrtem-stem-simulator/index.md) | [HRTEM](en/9-hrtem-stem-simulator/1-hrtem-simulation.md), [STEM](en/9-hrtem-stem-simulator/2-stem-simulation.md) |
| Simulate EBSD patterns | [EBSD simulation](en/12-ebsd-simulation.md) | [Electron trajectory](en/8-electron-trajectory.md), [Appendix A2. EBSD calculation](en/appendix/a2-bloch-wave/ebsd.md) |
| Index experimental diffraction spots | [Spot ID v1](en/10-spot-id.md), [Spot ID v2](en/11-spot-id-v2.md) | [Diffraction simulator](en/7-diffraction-simulator/index.md) |
| Understand the dynamical-diffraction equations | [Appendix A2. Bloch-wave method](en/appendix/a2-bloch-wave/index.md) | [Dynamical calculation](en/appendix/a2-bloch-wave/calculation.md), [CBED](en/appendix/a2-bloch-wave/cbed.md), [STEM](en/appendix/a2-bloch-wave/stem.md), [EBSD](en/appendix/a2-bloch-wave/ebsd.md) |

## Features
* **Full GUI** — All operations are performed through a graphical interface. Most file I/O supports drag & drop.
* **Crystal list** — Handle multiple crystals at once; no need to open separate windows for each crystal.
* **Space group database** — Built-in database covering 230 space groups from International Tables Volume A, plus 530 Hall symbols, with symmetry elements, Wyckoff positions, and extinction rules. Symmetry elements and general positions can be drawn as *International Tables* Vol. A-style schematic diagrams (see [2. Symmetry information](en/2-symmetry-information.md)).
* **Atomic information** — Scattering factors (X-ray, electron, neutron), characteristic X-ray energies, isotope ratios, etc. for elements H (1) - Cf (98).
* **Flexible crystal rotation** — Set orientation by zone-axis/crystal-plane indices or by mouse drag. Miller–Bravais (4-index *hkil*) notation is supported for trigonal/hexagonal systems. Rotation state is synchronised across all simulation windows.
* **Diffraction simulation** — Kinematical and dynamical (Bloch wave) electron diffraction, X-ray diffraction (including precession and back-Laue cameras), precession electron diffraction (PED), and convergent-beam electron diffraction (CBED). A TEM-holder simulation links the diffraction pattern to holder tilt angles.
* **HRTEM / STEM simulation** — High-resolution TEM image simulation with partial-coherence models; STEM with thermal diffuse scattering.
* **EBSD & electron trajectory** — EBSD pattern simulation and Monte-Carlo electron-trajectory simulation (see [8. Electron trajectory](en/8-electron-trajectory.md)).
* **Spot indexing** — Automatic detection, fitting, and indexing of diffraction spots from experimental images (Spot ID v1/v2).
* **Macro** — Python-syntax macro for automating operations (see [20. Macro](en/20-macro/index.md)).
* **Light / dark theme** — The interface follows a selectable light or dark colour mode.

## System requirements
| Item | Minimum | Recommended |
|------|---------|-------------|
| OS | Windows with [.NET Desktop Runtime 10.0](https://dotnet.microsoft.com/download/dotnet/10.0) (Windows on ARM64 supported) | Windows 11 |
| GPU | OpenGL 1.3 | External GPU with OpenGL 1.5 |
| Memory | - | 16 GB or more |
| CPU | - | 8+ cores (for dynamical calculations) |

## How to Use This Manual

This GitHub Pages manual is the current source of truth. Use the left navigation to browse by chapter, or use search in the header to find a function name or UI label. The old PDF manuals are kept for archival reference.

* **Archived PDF (English):** https://raw.githubusercontent.com/seto77/ReciPro/master/ReciPro/doc/ReciProManual(en).pdf
* **Archived PDF (Japanese):** https://raw.githubusercontent.com/seto77/ReciPro/master/ReciPro/doc/ReciProManual(ja).pdf

## Quick start
1. Download and install from [Releases](https://github.com/seto77/ReciPro/releases/latest).
2. Select a crystal from the built-in list (~80 crystals). You can also import CIF files or use [CSManager](https://github.com/seto77/CSManager).
3. Call functions from the right-hand panel: Structure Viewer, Stereonet, Crystal Diffraction, HRTEM simulation, etc.
4. Rotate the crystal by mouse drag or by entering zone-axis/plane indices.

## Reference
> Y. Seto, "ReciPro: free and open-source multipurpose crystallographic software integrating a crystal operation interface and diffraction simulators," *J. Appl. Cryst.* **55**, 397-410 (2022). https://doi.org/10.1107/S1600576722000139

## License
ReciPro is distributed under the [MIT License](https://github.com/seto77/ReciPro/blob/master/LICENSE.md).
