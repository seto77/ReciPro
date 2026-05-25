<!-- nav -->

[0. Main window →](en/0-main-window.md)

## Brief introduction
* ReciPro is MIT-licensed free software that provides a variety of crystallographic calculations and electron microscopy simulations.
* ReciPro has been downloaded more than 27,000 times in total since its release on github (Mar 2020) and is used by many crystallographers and electron microscopists.

## Features
* **Full GUI** — All operations are performed through a graphical interface. Most file I/O supports drag & drop.
* **Crystal list** — Handle multiple crystals at once; no need to open separate windows for each crystal.
* **Space group database** — Built-in database covering 230 space groups from International Tables Volume A, plus 530 Hall symbols, with symmetry elements, Wyckoff positions, and extinction rules. Symmetry elements and general positions can be drawn as *International Tables* Vol. A-style schematic diagrams (see [11. Symmetry information](en/11-symmetry-information.md)).
* **Atomic information** — Scattering factors (X-ray, electron, neutron), characteristic X-ray energies, isotope ratios, etc. for elements H (1) - Cf (98).
* **Flexible crystal rotation** — Set orientation by zone-axis/crystal-plane indices or by mouse drag. Miller–Bravais (4-index *hkil*) notation is supported for trigonal/hexagonal systems. Rotation state is synchronised across all simulation windows.
* **Diffraction simulation** — Kinematical and dynamical (Bloch wave) electron diffraction, X-ray diffraction (including precession and back-Laue cameras), precession electron diffraction (PED), and convergent-beam electron diffraction (CBED). A TEM-holder simulation links the diffraction pattern to holder tilt angles.
* **HRTEM / STEM simulation** — High-resolution TEM image simulation with partial-coherence models; STEM with thermal diffuse scattering.
* **EBSD & electron trajectory** — EBSD pattern simulation and Monte-Carlo electron-trajectory simulation (see [13. Electron trajectory](en/13-electron-trajectory.md)).
* **Spot indexing** — Automatic detection, fitting, and indexing of diffraction spots from experimental images (Spot ID v1/v2).
* **Macro** — Python-syntax macro for automating operations (see [20. Macro](en/20-macro.md)).
* **Light / dark theme** — The interface follows a selectable light or dark colour mode.

## System requirements
| Item | Minimum | Recommended |
|------|---------|-------------|
| OS | Windows with [.NET Desktop Runtime 10.0](https://dotnet.microsoft.com/download/dotnet/10.0) (Windows on ARM64 supported) | Windows 11 |
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

---

[0. Main window →](en/0-main-window.md)
