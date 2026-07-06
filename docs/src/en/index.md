# ReciPro Manual

<!-- 260623Ch: Shared demo movie for all localized top pages. -->
<div class="rp-demo-video" markdown="0">
  <video controls autoplay muted playsinline preload="metadata" aria-label="ReciPro demo movie">
    <source src="assets/Recipro_Demo.mp4" type="video/mp4">
  </video>
</div>

## Brief introduction
* ReciPro is MIT-licensed free software that provides a variety of crystallographic calculations and electron microscopy simulations.
* ReciPro has been downloaded more than 27,000 times in total since its release on github (Mar 2020) and is used by many crystallographers and electron microscopists.

## Find by Goal

| Goal | Start here | Main next steps |
|------|------------|-----------------|
| Load a crystal and set its orientation | [Main window](0-main-window.md) | [Rotation Geometry](4-rotation-geometry.md), [Appendix A1. Coordinate systems](appendix/a1-coordinate-system/1-orientation.md) |
| Inspect a crystal structure in 3D | [Structure Viewer](5-structure-viewer.md) | [Symmetry information](2-symmetry-information.md) |
| Calculate SAED / XRD / PED / CBED patterns | [Diffraction simulator](7-diffraction-simulator/index.md) | [SAED](7-diffraction-simulator/1-saed-simulation.md), [X-ray diffraction](7-diffraction-simulator/4-x-ray-neutron-diffraction.md), [PED](7-diffraction-simulator/2-ped-simulation.md), [CBED](7-diffraction-simulator/3-cbed-simulation.md) |
| Calculate HRTEM / STEM images | [HRTEM/STEM simulator](9-hrtem-stem-simulator/index.md) | [HRTEM](9-hrtem-stem-simulator/1-hrtem-simulation.md), [STEM](9-hrtem-stem-simulator/2-stem-simulation.md) |
| Simulate EBSD patterns | [EBSD simulation](12-ebsd-simulation.md) | [Electron trajectory](8-electron-trajectory.md), [Appendix A3. EBSD calculation](appendix/a3-bloch-wave/ebsd.md) |
| Index experimental diffraction spots | [Spot ID v1](10-spot-id.md), [Spot ID v2](11-spot-id-v2.md) | [Diffraction simulator](7-diffraction-simulator/index.md) |
| Understand the dynamical-diffraction equations | [Appendix A3. Bloch-wave method](appendix/a3-bloch-wave/index.md) | [Dynamical calculation](appendix/a3-bloch-wave/calculation.md), [CBED](appendix/a3-bloch-wave/cbed.md), [STEM](appendix/a3-bloch-wave/stem.md), [EBSD](appendix/a3-bloch-wave/ebsd.md) |
| Understand space-group symbols and group–subgroup relations | [2. Symmetry information](2-symmetry-information.md) | [Appendix A4. Symmetry and Space Groups](appendix/a4-symmetry-space-groups/index.md), [Space-group symbols and diagrams](appendix/a4-symmetry-space-groups/symbols-and-diagrams.md), [Group–subgroup relations](appendix/a4-symmetry-space-groups/group-subgroup-relations.md) |

## Features
* **Full GUI** : All operations are performed through a graphical interface. Most file I/O supports drag & drop.
* **Crystal list** : Handle multiple crystals at once; no need to open separate windows for each crystal.
* **Space group database** : Built-in database covering 230 space groups from International Tables Volume A, plus 530 Hall symbols, with symmetry elements, Wyckoff positions, and extinction rules. Symmetry elements and general positions can be drawn as *International Tables* Vol. A-style schematic diagrams (see [2. Symmetry information](2-symmetry-information.md)).
* **Atomic information** : Scattering factors (X-ray, electron, neutron), characteristic X-ray energies, isotope ratios, etc. for elements H (1) - Cf (98).
* **Flexible crystal rotation** : Set orientation by zone-axis/crystal-plane indices or by mouse drag. Miller–Bravais (4-index *hkil*) notation is supported for trigonal/hexagonal systems. Rotation state is synchronised across all simulation windows.
* **Diffraction simulation** : Kinematical and dynamical (Bloch wave) electron diffraction, X-ray diffraction (including precession and back-Laue cameras), precession electron diffraction (PED), and convergent-beam electron diffraction (CBED). A TEM-holder simulation links the diffraction pattern to holder tilt angles.
* **HRTEM / STEM simulation** : High-resolution TEM image simulation with partial-coherence models; STEM with thermal diffuse scattering.
* **EBSD & electron trajectory** : EBSD pattern simulation and Monte-Carlo electron-trajectory simulation (see [8. Electron trajectory](8-electron-trajectory.md)).
* **Spot indexing** : Automatic detection, fitting, and indexing of diffraction spots from experimental images (Spot ID v1/v2).
* **Macro** : Python-syntax macro for automating operations (see [20. Macro](20-macro/index.md)).
* **Light / dark theme** : The interface follows a selectable light or dark colour mode.

## System requirements
| Item | Minimum | Recommended |
|------|---------|-------------|
| OS | Windows with [.NET Desktop Runtime 10.0](https://dotnet.microsoft.com/download/dotnet/10.0) (Windows on ARM64 supported) | Windows 11 |
| GPU | OpenGL 1.3 | External GPU with OpenGL 4.3 |
| Memory | - | 16 GB or more |
| CPU | - | 8+ cores (for dynamical calculations) |

**Windows on ARM (native, experimental)** : An experimental native ARM64 portable package (`ReciPro-v.X_arm64.zip`, self-contained — no .NET Runtime installation required) is available on the [releases page](https://github.com/seto77/ReciPro/releases/latest). The regular x64 packages also run on ARM64 Windows under the built-in emulation. See [Troubleshooting](troubleshooting.md#windows-on-arm) for setup notes and limitations.

**macOS (unofficial)** : ReciPro officially supports Windows only, but the **win-x64** portable ZIP package has been reported to run on macOS (Apple Silicon) using the Sikarugir Wine wrapper combined with the Mesa3D OpenGL driver. A user-published setup guide is available at <https://github.com/Ryo-fkushima/ReciPro_macOS_memo>. Note that this route is not officially supported, and some symbols (Å, superscripts, arrows) may be displayed incorrectly. The ARM64 ZIP does **not** run on macOS + Wine, and the current x64 + Rosetta 2 route is expected to stop working from macOS 28 (autumn 2027) — see [Troubleshooting](troubleshooting.md#mac-linux) for details.

## How to Use This Manual

This GitHub Pages manual is the current source of truth. Use the left navigation to browse by chapter, or use search in the header to find a function name or UI label. The old PDF manuals are kept for archival reference.

* **Archived PDF (English):** <https://raw.githubusercontent.com/seto77/ReciPro/master/ReciPro/doc/ReciProManual(en).pdf>
* **Archived PDF (Japanese):** <https://raw.githubusercontent.com/seto77/ReciPro/master/ReciPro/doc/ReciProManual(ja).pdf>

## Quick start
1. Download and install from [Releases](https://github.com/seto77/ReciPro/releases/latest).
2. Select a crystal from the built-in list (~80 crystals). You can also import CIF files or use [CSManager](https://github.com/seto77/CSManager).
3. Call functions from the right-hand panel: Structure Viewer, Stereonet, Crystal Diffraction, HRTEM simulation, etc.
4. Rotate the crystal by mouse drag or by entering zone-axis/plane indices.

## Reference
> Y. Seto, "ReciPro: free and open-source multipurpose crystallographic software integrating a crystal operation interface and diffraction simulators," *J. Appl. Cryst.* **55**, 397–410 (2022). <https://doi.org/10.1107/S1600576722000139>

## License
ReciPro is distributed under the [MIT License](https://github.com/seto77/ReciPro/blob/master/LICENSE.md).
