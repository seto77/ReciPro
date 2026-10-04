# Release notes

<!-- 261005Cl 新設: GitHub の Release ページには 1 行の要約とダウンロードの表だけを載せ、各版の詳細はこの頁に書く (作者指示)。
     「Earlier versions」は ReciPro/Version.cs の History (Help ▸ Version History と同じ文) から機械で写した。
     261005Cl: ver4.00 以前の日本語の 89 項目は、この英語の頁でだけ英訳に置き換えた (日本語の頁と Version.cs は原文のまま)。 -->

This page describes what changed in each version of ReciPro. The [release page on GitHub](https://github.com/seto77/ReciPro/releases) gives only a one-line summary and the download links for each version; the details are written here.

## v.4.949 (2026-10-04)

Reworked the EBSD simulation (non-local backscatter source on by default, Monte Carlo depth and energy modelling, background flattening, zone-axis picking, rotation movie). Updated the embedded Temari ionization (dataset 7.0.0) and scattering-factor (dataset-factors v2.0.0, now also in the TDS absorption) tables, fixed an error in the native EBSD solver (v.4.918–v.4.948), and added optional X-ray line series to STEM-EDX and ALCHEMI.

- **Ionization table (STEM-EDX, ALCHEMI)**: Updated the embedded Temari ionization form factors F(s, E₀) from dataset 5.0.0 to dataset 7.0.0 (DOI 10.5281/zenodo.22643468), the first version computed with a finite nucleus throughout; the grid, the 525 channels and the file format are unchanged, and F changes by at most 1.7 × 10⁻³ of F(0) = 1. CITATION.cff, README and THIRD-PARTY-NOTICES now cite dataset 7.0.0, and the License section of the manual's top page links to its DOI. Help › License now also points to THIRD-PARTY-NOTICES.md, which carries the CC BY 4.0 attribution of both bundled Temari tables (this one and the scattering factors below).
- **Scattering factors (Beam Interaction, TDS absorption)**: Updated the embedded Temari f_x(s) and f_e(s) for neutral atoms (Z = 1–86) from dataset-factors v1.0.0 to v2.0.0 (DOI 10.5281/zenodo.22820415, now cited by CITATION.cff, README and THIRD-PARTY-NOTICES, and linked from the License section of the manual's top page); the values are unchanged except in the last stored digits for Ba and Ta. Every table is now declared computed and not certified, and the error bound that v1.0.0 stated has been withdrawn, which does not mean that the values are wrong. This f_e is now also used in the TDS absorption (see below).
- **EBSD (fix)**: Fixed a complex-conjugation error in the native EBSD solver (v.4.918–v.4.948) that affected both the local backscatter source and the optional TDS background; with the native library disabled, the TDS background had the same error. In a benchmark on the code just before the fix (nine crystals including Si, Re and Au; 20 kV; 64 diffracted waves; B = 0.5 Å²; local source), total intensities were 1.11–2.48 times those of the corrected solver and normalized master patterns differed by 7–38 % (relative L2 norm; for Au still 30 % with 128 diffracted waves); other accelerating voltages, fewer diffracted waves (the default is 32), the multi-energy calculation of the EBSD window, the TDS background and released builds were not measured, so these are not the differences between v.4.948 and v.4.949. The former 'Include TDS background' checkbox, which added to the local source, is now 'Non-local source', which replaces the local source and is on by default.
- **Absorptive potential (TDS)**: The TDS (thermal diffuse scattering) absorption now uses the Temari f_e for neutral atoms (Z = 1–86) up to s = 6 Å⁻¹, in place of the Peng fit blended into the Mott–Bethe tail at 1.5–2.5 Å⁻¹; beyond 6 Å⁻¹ that Mott–Bethe tail is kept (not a Temari value), joined without smoothing (see [appendix A3 of the manual](appendix/a3-bloch-wave/calculation.md)). In benchmark calculations (five crystals; 80–300 kV; B = 0.3–1.0 Å²; STEM and HRTEM at 10 and 20 nm, CBED at 20–100 nm), STEM, HRTEM and CBED results changed by at most 0.45 % in relative L2 norm (measured with a reference implementation of this change; 0.22 % with the implementation in this release at 200 kV and B = 0.5 Å²); thicker specimens were not tested, and these figures do not cover EBSD, STEM-EDX or ALCHEMI. Atoms assigned as ions also get the new f_e unless Options › 'Use ionic scattering factors' is on; atoms with Z = 87–98 and the real part of the potential are unchanged.
- **X-ray line series (STEM-EDX, ALCHEMI)**: Added optional Kα, Kβ, Lα, Lβ and Mα channels (off by default) whose values are X-ray photons generated per incident electron: a model quantity, in all directions, before self-absorption and detection, not a predicted X-ray count. They combine the subshell ionization signals with xraylib 4.2.1 fluorescence yields, Coster–Kronig and radiative cascades and line branching ratios; the Auger cascade is not included (compared with the full cascade at 200 kV for selected elements, Mα came out about 5 % lower). They are described in the manual ([STEM Simulation › STEM-EDX elemental maps](9-hrtem-stem-simulator/2-stem-simulation.md#stem-edx); [ALCHEMI Simulation › Output quantity](7-diffraction-simulator/4-alchemi-simulation.md#output-quantity)).

## Earlier versions

The one-line summaries below are the version history that ReciPro also shows in **Help ▸ Version History**. Entries before ver4.10 (2011) were originally written in Japanese; they are given here in English translation (the Japanese page keeps the original text).

### 2026

- **ver4.948** (2026/08/30) Fixed a long-standing error in the Bloch-wave dynamical calculation that scaled each diffracted amplitude by P\_g/P\_0. Zone-axis SAED, HRTEM and STEM are unaffected; EBSD, Kikuchi bands and HOLZ reflections were affected most.
- **ver4.947** (2026/08/20) Added an ALCHEMI simulator (preview): site-resolved ionization rocking curves along a systematic row by the Bloch-wave method, with angular-spread convolution, provenance-tagged CSV export and a manual page in 11 languages. Added dynamical Kikuchi bands to the diffraction simulator, Temari scattering factors and an integral absorptive factor to Beam Interaction, saving of EBSD patterns and a CITATION.cff, and fixed the R(%) round-trip in Spot ID.
- **ver4.946** (2026/08/05) Added STEM-EDX simulation: characteristic X-ray (inner-shell ionization) maps computed alongside STEM images by the Bloch-wave method, using original fully relativistic ionization form-factor tables (K: C-Sn, L: Ca-Rn), documented in 11 languages. Also let macros create and edit crystals and drive the Structure Viewer, and added a see-through mesh style for coordination polyhedra.
- **ver4.945** (2026/08/03) Added dark mode support, improved keyboard and mouse operability, enhanced the indexing of experimental EBSD patterns with an automatic detector-geometry calibration, and fixed many bugs including a startup crash at high DPI.
- **ver4.944** (2026/07/25) Added indexing of experimental EBSD patterns (orientation search and detector calibration), added external macro control via a named pipe and unattended command-line execution, added table (CSV) export to 'Spot ID' and diffraction spot information, and improved performance and fixed many bugs across the application.
- **ver4.943** (2026/07/15) Added a 'Group Relations' function to explore group–subgroup relations of space groups (maximal subgroups/supergroups, Bärnighausen trees, and symmetry-element diagrams), and further accelerated STEM simulations with optional Intel MKL support.
- **ver4.942** (2026/07/01) Enabled digital code signing of the installer and portable executable, provided free by SignPath Foundation.
- **ver4.941** (2026/06/23) Added multi-language UI support and fixed many bugs.
- **ver4.940** (2026/06/14) Improved the accuracy of ionic elastic scattering factor calculations, and reorganized the distribution package formats.
- **ver4.939** (2026/06/13) Enhanced support for Arm64 environments and fixed bugs in Beam Interactions.
- **ver4.938** (2026/06/11) Slightly accelerated STEM simulation and other calculations, and added experimental support for running on macOS via Wine.
- **ver4.937** (2026/06/08) Substantially overhauled 'Scattering Factor' and released it as 'Beam Interaction', now including information on absorption coefficients and fluorescence.
- **ver4.936** (2026/06/04) Further reduced the distribution size by eliminating redundant data.
- **ver4.935** (2026/06/02) Added a portable ZIP distribution, improved the manual, and fixed bugs.
- **ver4.934** (2026/05/30) Improved the video encoding engine and reduced the distribution size.
- **ver4.933** (2026/05/29) Fixed OpenGL rendering corruption on Windows on ARM (x64 emulation).
- **ver4.932** (2026/05/28) Improved the manual and enhanced stereographic projection features. (see https://github.com/seto77/ReciPro/issues/58)
- **ver4.931** (2026/05/19) Fixed GUI layout issues that occurred under high-DPI display settings. (see https://github.com/seto77/ReciPro/issues/59)
- **ver4.930** (2026/05/17) Fixed great circle rendering and added a cursor-position plane/axis index readout in the stereographic projection. (see https://github.com/seto77/ReciPro/issues/58)
- **ver4.929** (2026/05/13) Fixed bugs in symmetry element rendering in 'Structure Viewer' and improved its performance.
- **ver4.928** (2026/05/09) Added a function to render symmetry elements in 'Structure Viewer'.
- **ver4.927** (2026/05/04) Substantially expanded 'Symmetry Information' to render ITC Vol.A style schematic diagrams of symmetry elements and general positions.
- **ver4.926** (2026/04/25) Added support for Miller-Bravais index notation (hkil 4-index representation of lattice planes for trigonal/hexagonal crystal systems). (see https://github.com/seto77/ReciPro/issues/54)
- **ver4.925** (2026/04/20) Hardened the app startup so it continues even when OpenGL initialization fails. (see https://github.com/seto77/ReciPro/issues/55)
- **ver4.924** (2026/04/15) Enhanced the macro-related features.
- **ver4.923** (2026/04/05) Fixed minor bugs.
- **ver4.922** (2026/04/05) Greatly reduced the size of the installer package.
- **ver4.921** (2026/04/01) Improved the EBSD simulation, and fixed many minor bugs.
- **ver4.919** (2026/03/20) Improved the OpenGL renderings and EBSD simulations, and fixed many minor bugs.
- **ver4.918** (2026/03/13) Improved the Native Library to enable automatic switching between non-AVX, AVX2 and AVX512.
- **ver4.917** (2026/03/05) Added several 'Macro' functions (see https://github.com/seto77/ReciPro/issues/36).
- **ver4.916** (2026/01/14) Fixed an issue with loading Crystallography.Native.dll.

### 2025

- **ver4.915** (2025/12/25) Improved: Equivalent axes/planes can be color-coded in 'Stereonet'.
- **ver4.914** (2025/12/21) Added: 'TEM holder simulation' to 'Diffraction Simulator'. Miller-Bravais index option to 'Stereonet'. (see https://github.com/seto77/ReciPro/issues/52)
- **ver4.913** (2025/12/12) Fixed bugs on program update and crystal database functions.
- **ver4.912** (2025/12/10) Updated AMCSD database. Improved to run on Windows on ARM64.
- **ver4.910** (2025/11/26) Updated: .Net Desktop Runtime 9 to 10. Fixed minor bugs.
- **ver4.909** (2025/10/29) Fixed a minor bug.
- **ver4.907** (2025/10/28) Fixed a minor bug.
- **ver4.906** (2025/09/26) Fixed a minor bug. Renewed the Eigen library.
- **ver4.905** (2025/09/14) Added several 'Macro' functions.
- **ver4.904** (2025/08/04) Fixed a minor bug.
- **ver4.903** (2025/05/29) Improved the 'Crystal Database' function. COD has been available.
- **ver4.902** (2025/05/13) Fixed a minor bug.
- **ver4.901** (2025/04/10) Added several 'Macro' functions (ReciPro.Crystal.\*). See https://seto77.github.io/ReciPro/en/20-macro/.
- **ver4.900** (2025/04/08) Fixed a minor bug.
- **ver4.899** (2025/04/01) Added some built-in functions for macro (see https://github.com/seto77/ReciPro/issues/45).
- **ver4.898** (2025/03/04) Added right-click menus for the selected crystal. Fixed a bug related to https://github.com/seto77/ReciPro/issues/44.
- **ver4.897** (2025/01/30) Improved: The macro function has been enhanced. See https://seto77.github.io/ReciPro/en/20-macro/.
- **ver4.896** (2025/01/17) Fixed some bugs on OpenGL renderings.

### 2024

- **ver4.895** (2024/11/14) Updated: .Net Desktop Runtime 8.0 to 9.0. Updated the bundled crystal database.
- **ver4.894** (2024/11/01) Fixed bugs on the 'Diffraction Simulator' and 'HRTEM/STEM simulator' (thanks to lukmuk-san and Nakamura-san!).
- **ver4.892** (2024/10/04) Added several 'Macro' functions.
- **ver4.891** (2024/09/06) Added the function to simulate electron trajectories based on the Monte Carlo method.
- **ver4.890** (2024/08/10) Improved 'Macro' functions.
- **ver4.889** (2024/08/09) Fixed bugs on the 'Diffraction Simulator'.
- **ver4.888** (2024/08/07) Added the 'Kikuchi line pairs' projection mode to the 'Stereonet' simulation. (see https://github.com/seto77/ReciPro/issues/35)
- **ver4.887** (2024/07/30) Fixed a minor bug.
- **ver4.886** (2024/07/20) Added horizontal/vertical flip and color inversion functions for 'Diffraction Simulator'  (see https://github.com/seto77/ReciPro/issues/35).
- **ver4.885** (2024/06/20) Fixed a bug and typo in the 'Diffraction Spot Information' (see https://github.com/seto77/ReciPro/issues/34, thanks to tianyu-liu-san).
- **ver4.884** (2024/05/24) Fixed a bug in the calculations of dynamical theory.
- **ver4.883** (2024/04/06) Fixed minor bugs in the 'CBED setting'. Update bundled libraries.
- **ver4.882** (2024/03/16) Improved GUI in the 'Diffraction simulator'. (see https://github.com/seto77/ReciPro/issues/30, thanks to lukmuk-san)
- **ver4.881** (2024/03/11) Checked security problem (see https://github.com/seto77/ReciPro/issues/31).
- **ver4.880** (2024/03/09) Improved GUI in the 'Diffraction simulator'. (see https://github.com/seto77/ReciPro/issues/30, thanks to lukmuk-san)
- **ver4.879** (2024/03/05) Fixed GUI in the 'HRTEM/STEM simulator'. (see https://github.com/seto77/ReciPro/issues/29, thanks to JingshanDu-san)
- **ver4.878** (2024/02/13) Added options for saving movies.
- **ver4.877** (2024/02/11) Added: Back Laue camera mode (X-ray diffraction) (see https://github.com/seto77/ReciPro/issues/28). Improved registry read/write behaviour at startup.

### 2023

- **ver4.876** (2023/12/21) Fixed an issue where icon images were not displayed correctly.
- **ver4.874** (2023/12/08) Improved 'Structure Viewer': Double-clicking on an atom to display its coordination environment, etc.
- **ver4.873** (2023/12/07) Improved the bounds options on 'Structure Viewer'.
- **ver4.871** (2023/11/29) Fixed bugs on 'Structure Viewer'.
- **ver4.870** (2023/11/21) Target framework has been changed to .Net Desktop Runtime 8.0.
- **ver4.869** (2023/10/26) Added the function to simulate the Ewald sphere and the reciprocal vectors to 'Diffraction Simulator'.
- **ver4.868** (2023/10/23) Fixed an issue with text rendering using OpenGL (see https://github.com/seto77/ReciPro/issues/26).
- **ver4.867** (2023/08/04) Improved the interface of SpotID v2 (see https://github.com/seto77/ReciPro/issues/25).
- **ver4.866** (2023/08/01) AVX2 support temporarily suspended. Fixed a bug when using AMD Radeon GPUs.
- **ver4.865** (2023/06/23) Fixed: GUI issues when changing language.
- **ver4.864** (2023/06/19) Added: the length and F (structure factor) of the g vector are displayed when the spot is double-clicked (see https://github.com/seto77/ReciPro/issues/21).
- **ver4.862** (2023/05/16) Fixed minor bugs. Improved macro functions. Added command-line options.
- **ver4.861** (2023/04/12) Added macro functions to automate various tasks (mainly 'Diffraction Simulator' at the moment).
- **ver4.860** (2023/04/06) Improved CIF file loading compatibility (see https://github.com/seto77/ReciPro/issues/19).
- **ver4.859** (2023/03/31) Fixed a minor bug on HRTEM/STEM simulation.
- **ver4.858** (2023/03/30) Fixed a minor bug on HRTEM/STEM simulation.
- **ver4.857** (2023/03/30) Improved several features on HRTEM/STEM simulation.
- **ver4.856** (2023/03/23) Fixed minor GUI bugs on HRTEM/STEM simulation.
- **ver4.855** (2023/03/23) Added a feature to save simulation conditions in HRTEM/STEM simulation.
- **ver4.854** (2023/03/11) Fixed minor GUI bugs on HRTEM/STEM simulation.
- **ver4.853** (2023/03/09) Corrected errors in formulas in STEM simulations. Added LA-CBED calculation mode.
- **ver4.852** (2023/03/04) Fixed minor GUI bugs on HRTEM/STEM simulation.
- **ver4.851** (2023/03/02) Fixed minor GUI bugs on HRTEM/STEM simulation.
- **ver4.850** (2023/03/01) Improved STEM simulation. If you find anything wrong with the STEM simulation, please report anything!
- **ver4.849** (2023/02/11) Improved: Overall speedup with SIMD calculation.

### 2022

- **ver4.848** (2022/12/28) Added the function to convert the current space group to a convertible space group. Fixed minor bugs on 'Spot ID v1'.
- **ver4.847** (2022/12/23) Added functions to save/copy images for 'Spot ID v2'.
- **ver4.845** (2022/12/20) Fixed minor bugs. Improved compatibility for reading Tiff format files.
- **ver4.843** (2022/11/29) Fixed minor bugs.
- **ver4.841** (2022/11/16) Target framework has been changed to .Net Desktop Runtime 7.0.
- **ver4.840** (2022/11/10) Fixed a bug that occurred when starting 'Diffraction Simulator' (see https://github.com/seto77/ReciPro/issues/16).
- **ver4.839** (2022/11/07) Added a function to simulate X-ray precession camera.
- **ver4.838** (2022/10/21) Improved compatibility of importing CIF files.
- **ver4.837** (2022/10/20) Added a function to output superstructure.
- **ver4.836** (2022/08/30) The compiler for C\+\+ code was changed to Clang.
- **ver4.835** (2022/08/09) Improved compatibility for reading DM3 format files.
- **ver4.834** (2022/07/08) Improved the function to generate movies.
- **ver4.833** (2022/06/24) Added the function to generate movies for 'Structure Viewer'.
- **ver4.832** (2022/06/23) Added the function to render stereonet projection with OpenGL.
- **ver4.831** (2022/05/14) Fixed minor bugs on the HRTEM function.
- **ver4.830** (2022/04/14) Some libraries are updated. Improved the HRTEM function (see https://github.com/seto77/ReciPro/issues/13).
- **ver4.829** (2022/01/04) Minor update on 'Spot ID v2' (see https://github.com/seto77/ReciPro/issues/11).

### 2021

- **ver4.828** (2021/12/15) Updated the crystal database.
- **ver4.827** (2021/12/01) Fixed a CultureInfo problem. (see https://github.com/seto77/ReciPro/issues/10)
- **ver4.826** (2021/11/18) Fixed minor bugs.
- **ver4.820** (2021/11/12) Target framework has been changed to .Net Desktop Runtime 6.0.
- **ver4.819** (2021/10/27) Improved the interface of Kikuchi line simulation. Speed up & fix bug on the dynamical diffraction simulator.
- **ver4.817** (2021/09/17) Fixed minor bugs.
- **ver4.815** (2021/09/02) Improved: User interfaces and tooltips.
- **ver4.814** (2021/08/29) Fixed minor bugs: Drawing overlapping area of CBED disks (see https://github.com/seto77/ReciPro/issues/8).
- **ver4.813** (2021/08/28) Fixed minor bugs on HRTEM simulation (see https://github.com/seto77/ReciPro/issues/9).
- **ver4.812** (2021/08/17) Changed GUI. Fixed minor bugs.
- **ver4.811** (2021/08/10) Fixed minor bugs on HRTEM simulation (see https://github.com/seto77/ReciPro/issues/7).
- **ver4.810** (2021/08/07) Fixed minor bugs.
- **ver4.809** (2021/07/16) Fixed minor bugs. Renewed AMCSD database, and improved loading speed of the database.
- **ver4.808** (2021/07/08) Fixed a minor bug about a compile option for native (c\+\+) codes.
- **ver4.807** (2021/07/06) Fixed minor bugs. Improved a rendering speed of 'Structure Viewer'.
- **ver4.806** (2021/05/25) Fixed distribution failure of language resource files.
- **ver4.802** (2021/05/24) Target framework has been changed to .Net Desktop Runtime 5.0.
- **ver4.800** (2021/05/20) Fixed bugs on native (c\+\+) codes. Changed CBED interface.
- **ver4.799** (2021/05/10) Fixed bugs on native (c\+\+) codes.
- **ver4.798** (2021/05/03) Fixed bugs on the 'Diffraction simulator'.
- **ver4.797** (2021/03/24) Fixed a bug on the 'Database' function.
- **ver4.795** (2021/03/09) Fixed a bug on the CBED calculation code.
- **ver4.794** (2021/03/08) Added new algorithm for CBED calculation (matrix exponential method)
- **ver4.793** (2021/02/26) Fixed bugs in 'Diffraction simulator'.

### 2020

- **ver4.792** (2020/12/28) Fixed a bug on 'Parallels Desktop' for Mac (OpenGL drawing problem).
- **ver4.791** (2020/11/06) Fixed a bug in Kikuchi line drawing. Improved speed of 'Structure Viewer' drawing.
- **ver4.790** (2020/11/02) Improved: GUI of 'Diffraction Simulator'.
- **ver4.789** (2020/10/26) Improved: Speed up drawing of 'Diffraction Simulator'.
- **ver4.788** (2020/10/20) Fixed a bug when calculating electron diffraction for crystals in AMCSD.
- **ver4.787** (2020/10/19) Fixed bugs in 'Powder Diffraction'.
- **ver4.786** (2020/10/10) Fixed bugs in 'Crystal Database' and improved the ’Find spots' function in 'Spot ID'.
- **ver4.785** (2020/10/06) Fixed a problem on OpenGL with Radeon Vega graphics.
- **ver4.784** (2020/10/01) Updated the manuals (both English and Japanese).
- **ver4.783** (2020/09/08) Fixed a bug on GUI.
- **ver4.782** (2020/08/19) Fixed a bug on OpenGL.
- **ver4.781** (2020/08/19) Loosen the restrictions on OpenGL requirements. (OpenGL 1.3 or higher)
- **ver4.780** (2020/08/18) Fixed a bug when exporting face-centered symmetry to CIF format.
- **ver4.779** (2020/07/08) Added a crystal database function, which manages 20698 crystals from AMCSD database. Fixed a bug on a dll file.
- **ver4.778** (2020/06/07) Fixed a bug on importing CIF file.
- **ver4.777** (2020/06/06) Improved GUI of the main window and 'structure viewer'.
- **ver4.776** (2020/05/30) Improved: Speed up rendering of 'Structure viewer'.
- **ver4.775** (2020/05/19) Improved: Rendering of text label in OpenGL windows. Fixed: Stereonet drawing.
- **ver4.774** (2020/05/15) Fixed bugs for Wyckoff position discriminator for trigonal and hexagonal symmetries.
- **ver4.773** (2020/05/12) Improved importing CIF file.
- **ver4.772** (2020/05/12) Changed: Open GL 1.5 (or higher) is required for 'Structure Viewer'.
- **ver4.771** (2020/05/10) Changed: Open GL 3.3 (or higher) is required for 'Structure Viewer'.
- **ver4.770** (2020/05/09) Improved rendering quality of 'Structure Viewer'.
- **ver4.769** (2020/05/06) Improved GUI on 'Structure Viewer'.
- **ver4.768** (2020/05/06) Improved GUI on 'Structure Viewer'.
- **ver4.767** (2020/05/05) Improved rendering speed of 'Structure Viewer' and fixed some bugs.
- **ver4.766** (2020/05/02) Improved GUIs on 'Structure Viewer' and fixed bugs on 'Spot ID'.
- **ver4.765** (2020/04/26) Improved 'Rotation geometry' and fixed 'Stereonet'.
- **ver4.764** (2020/04/12) Improved GUI, and fixed minor bugs.
- **ver4.763** (2020/03/31) Minor bugs fixed.
- **ver4.762** (2020/03/19) Minor bugs fixed.
- **ver4.761** (2020/03/14) Minor bugs fixed.
- **ver4.760** (2020/03/03) Minor bugs fixed.
- **ver4.756** (2020/03/02) Minor bugs fixed.
- **ver4.755** (2020/03/01) Changed: Distribution site is changed to GitHub.
- **ver4.747** (2020/02/29) Improved: Diffraction simulator.
- **ver4.746** (2020/02/28) Improved: Diffraction simulator.
- **ver4.745** (2020/02/16) Fixed a minor bug of Diffraction simulator.
- **ver4.744** (2020/02/05) Improved interfaces of Diffraction simulator.
- **ver4.743** (2020/02/02) Improved interfaces of Diffraction simulator.
- **ver4.742** (2020/01/06) A minor improvement on SpotID.

### 2019

- **ver4.741** (2019/12/12) Fixed a minor bug on SpotID.
- **ver4.740** (2019/12/07) Fixed a minor bug on TDS calculation.
- **ver4.739** (2019/10/24) Fixed a minor bug on 'Diffraction Simulator'.
- **ver4.733** (2019/10/17) Fixed a minor bug on 'Diffraction Simulator'.
- **ver4.731** (2019/09/24) Fixed minor bugs on HRTEM image simulation and Spot ID.
- **ver4.729** (2019/09/16) Improved interfaces of HRTEM image simulation.
- **ver4.728** (2019/09/15) Improved interfaces of HRTEM image simulation.
- **ver4.725** (2019/09/11) Improved calculation speed of HRTEM image simulation.
- **ver4.720** (2019/09/09) Improved calculation speed of HRTEM image simulation.
- **ver4.718** (2019/09/08) Fixed minor bugs on HRTEM image simulation.
- **ver4.715** (2019/09/08) Fixed minor bugs on HRTEM image simulation.
- **ver4.714** (2019/09/07) Improved calculation speed of HRTEM image simulation.
- **ver4.713** (2019/09/06) Fixed minor bugs on HRTEM image simulation.
- **ver4.711** (2019/09/04) Improved: HRTEM image simulation. Transmission cross coefficient model is added.
- **ver4.704** (2019/09/03) Improved: HRTEM image simulation. Through-focus/defocus mode is now available.
- **ver4.703** (2019/08/26) Added: HRTEM image simulation is now available. Many thanks to Dr. Ohtsuka.
- **ver4.694** (2019/08/18) Fixed minor bugs on 'Diffraction Simulator'. Changed .Net framework version to 4.8
- **ver4.693** (2019/08/06) Fixed minor bugs on 'Spot ID'
- **ver4.692** (2019/08/05) Improved function on 'Spot ID'
- **ver4.687** (2019/08/02) Improved calculation speed for the PED simulation
- **ver4.686** (2019/07/24) Fixed minor bugs in PED simulation
- **ver4.683** (2019/07/20) Added a function: In 'Diffraction Simulator', precession electron diffraction (PED) mode is now available.
- **ver4.682** (2019/07/18) Fixed a minor bug (eigen solver did not properly work).
- **ver4.681** (2019/07/08) Fixed minor bugs on 'Spot ID'
- **ver4.680** (2019/07/06) Added 'Rotation Geometry' form.
- **ver4.670** (2019/06/12) Improved functions on 'Spot ID'.
- **ver4.669** (2019/05/17) Fixed minor bugs.
- **ver4.668** (2019/04/25) Fixed minor bugs.
- **ver4.667** (2019/04/21) Fixed minor bugs.
- **ver4.664** (2019/04/19) Fixed minor bugs.
- **ver4.663** (2019/04/16) Fixed minor bugs.
- **ver4.662** (2019/04/12) Fixed minor bugs.
- **ver4.661** (2019/04/11) Fixed minor bugs.
- **ver4.660** (2019/04/10) Changed the installer. ClickOnce version will not be maintained in the future.
- **ver4.654** (2019/04/09) Improved the update function for zip version.
- **ver4.653** (2019/04/08) Fixed minor bugs.
- **ver4.652** (2019/04/04) Fixed minor bugs.
- **ver4.651** (2019/03/27) Corrected typos of Wyckoff positions and site symmetries in some space groups.
- **ver4.650** (2019/03/25) Minor bugs fixed.
- **ver4.649** (2019/03/18) Minor bugs fixed & Improved calculation speed of dynamic diffraction intensity.
- **ver4.648** (2019/03/11) Fixed minor bugs and improved a calculation speed on 'Spot ID'
- **ver4.647** (2019/03/10) Fixed minor bugs on 'Spot ID'
- **ver4.646** (2019/03/08) Fixed minor bugs on 'Spot ID'
- **ver4.645** (2019/03/07) Fixed minor bugs on 'Spot ID'
- **ver4.643** (2019/03/06) Fixed minor bugs on 'Spot ID'
- **ver4.642** (2019/03/05) Improved calculation speed of 'Spot ID'
- **ver4.641** (2019/03/04) Improved calculation speed of 'Spot ID'
- **ver4.64** (2019/03/03) Changed Visual Studio version to 2019.
- **ver4.636** (2019/03/01) Improved some functions in 'Spot ID'.
- **ver4.635** (2019/02/28) Improved some functions in 'Spot ID'.
- **ver4.634** (2019/02/27) Improved some functions in 'Spot ID'.
- **ver4.633** (2019/02/26) Improved some functions in 'Spot ID'.
- **ver4.632** (2019/02/22) Improved some functions in 'Spot ID'.
- **ver4.631** (2019/02/21) Fixed minor bugs. (copy functions in 'Structure Viewer' and 'Spot ID')
- **ver4.630** (2019/02/20) Fixed a minor bug. Changed .Net framework version to 4.7.2.
- **ver4.629** (2019/02/20) Added a function: OpenGL can be manually disabled by pressing 'CTRL' key on startup.
- **ver4.628** (2019/02/19) Fixed a bug of calculations of anisotropic Debye-Waller effects.
- **ver4.627** (2019/02/17) Minor bug fixed.
- **ver4.625** (2019/02/13) Fixed minor bugs.
- **ver4.624** (2019/02/09) Added: Check routine of OpenGL version.
- **ver4.622** (2019/02/06) Minor improvements.
- **ver4.621** (2019/02/05) Minor bug on the 'Spot ID' function fixed.
- **ver4.620** (2019/02/05) Minor bug on the 'Spot ID' function fixed.
- **ver4.619** (2019/02/03) Minor bug (in Bethe method) fixed.
- **ver4.618** (2019/01/28) Minor bug fixed.
- **ver4.617** (2019/01/26) Minor bug fixed.
- **ver4.616** (2019/01/25) Minor bug fixed.
- **ver4.615** (2019/01/22) Improved: A simulated CBED pattern can be saved as Tiff (32-bit float) format.
- **ver4.614** (2019/01/22) Improved: Detailed results of the Bethe method calculation can be displayed.
- **ver4.613** (2019/01/19) Minor improvements on dynamic compression mode.
- **ver4.612** (2019/01/11) Minor improvements on dynamic compression mode.
- **ver4.611** (2019/01/08) Fixed a minor bug on a TDS calculation.
- **ver4.61** (2019/01/05) Improved a dynamic simulation of electron diffraction. A TDS (thermal diffuse scattering) effect is now calculated properly

### 2018

- **ver4.602** (2018/12/23) Improved 'Structure viewer'.
- **ver4.601** (2018/12/20) Improved 'Structure viewer'.
- **ver4.6** (2018/12/17) Replaced OpenGL libraries. From this version, Open GL 4.3 (or higher) is required.
- **ver4.515** (2018/11/20) Modified some inconsistencies.
- **ver4.514** (2018/10/25) Minor bug fixed.
- **ver4.513** (2018/10/22) Improved calculation speed for CBED.
- **ver4.512** (2018/10/19) Added a solver library for CBED calculation.
- **ver4.511** (2018/10/18) Minor improvements to CBED calculation.
- **ver4.51** (2018/10/16) Minor improvements to CBED calculation.
- **ver4.50** (2018/10/16) Added a dynamic simulation mode (CBED pattern) by the Bethe method (beta). Many thanks to Dr. Ohtsuka & Dr. Igami!
- **ver4.42** (2018/10/11) Minor improvements.
- **ver4.41** (2018/10/05) Fixed minor bugs about the Bethe method.
- **ver4.40** (2018/09/23) Added a dynamic simulation mode (SAED pattern) by the Bethe method (beta).
- **ver4.372** (2018/09/10) Minor bug fixed.
- **ver4.371** (2018/08/27) Fixed bugs on 'Single Crystal Diffraction' form. (thx Dr.Sakamoto)
- **ver4.362** (2018/03/30) Minor improvements.
- **ver4.361** (2018/03/23) Minor improvements.
- **ver4.36** (2018/03/19) Improved: 'TEMID' is capable of selection of multiple crystals. (need Ctrl \+ Click).
- **ver4.35** (2018/03/01) Improved an algorithm of 'Diffraction Simulator'.
- **ver4.346** (2018/02/23) Minor bug fixed.
- **ver4.345** (2018/02/22) Minor bug fixed.
- **ver4.344** (2018/02/22) Minor bug fixed.
- **ver4.343** (2018/02/22) Minor bug fixed.
- **ver4.342** (2018/02/21) Added some options on 'Diffraction Simulator' to enable copying the detector area.
- **ver4.341** (2018/02/21) Fixed a minor bug.
- **ver4.34** (2018/02/20) Improved. 'Diffraction Simulator' is now capable of selection of multiple crystals. (need Ctrl \+ Click)
- **ver4.334** (2018/02/19) Fixed minor bugs.
- **ver4.333** (2018/02/19) Fixed minor bugs.
- **ver4.332** (2018/02/13) Fixed minor bugs.
- **ver4.331** (2018/02/08) Fixed minor bugs.
- **ver4.33** (2018/02/07) Improved: Rotation state is individually preserved for each crystal.
- **ver4.32** (2018/02/05) Added: The nearest zone axis can be shown in the main form.
- **ver4.317** (2018/02/03) Fixed minor bugs on 'Single crystal diffraction' form.
- **ver4.316** (2018/01/26) Fixed minor bugs on 'Single crystal diffraction' form.
- **ver4.312** (2018/01/25) Improved 'Single crystal diffraction' form.
- **ver4.311** (2018/01/20) Improved the 'Overlap picture' function on 'Single crystal diffraction' form.
- **ver4.31** (2018/01/19) Changed graphics interface for 'Single crystal diffraction' form from OpenGL to GDI\+, and then the metafile (vector object) of diffraction patterns can be exported to your clipboard. The 'Overlap picture' function is now under construction

### 2017

- **ver4.30** (2017/12/24) Changed graphics interface for 'Stereonet' form from OpenGL to GDI\+, and then the metafile (vector object) of stereonet can be exported to your clipboard.
- **ver4.29** (2017/09/01) Added 'Point Spread' mode on 'Single Crystal Diffraction'.
- **ver4.283** (2017/05/28) Fixed a small bug on 'Strain control' function.
- **ver4.282** (2017/05/26) Added 'Strain control' function.
- **ver4.281** (2017/04/26) Improved SACLA simulation on 'Single Crystal Diffraction'.

### 2016

- **ver4.280** (2016/12/31) Improved a compatibility for CIF format.
- **ver4.279** (2016/12/18) Fixed minor bugs.
- **ver4.278** (2016/05/17) Improved 'Powder Diffraction' and fixed minor bugs.
- **ver4.277** (2016/01/14) Changed .Net Framework version to 4.6.

### 2015

- **ver4.276** (2015/12/24) Fixed a minor bug on initial loading.
- **ver4.275** (2015/12/23) Fixed a minor bug on initial loading.
- **ver4.273** (2015/12/22) Fixed a minor bug on initial loading.
- **ver4.272** (2015/12/18) Fixed a minor bug on input form for rhombohedral settings.
- **ver4.271** (2015/12/11) Fixed a minor bug on Wyckoff positions
- **ver4.270** (2015/09/25) Fixed a minor bug on 'Structure Viewer'.(thx Dr. Fukui)
- **ver4.269** (2015/06/30) Added: Back Laue camera simulation.
- **ver4.268** (2015/05/13) Fixed a minor bug on reading \*.ipa files.
- **ver4.267** (2015/03/25) Fixed a minor bug on single diffraction simulation
- **ver4.266** (2015/03/18) Fixed a bug on Debye-Waller factor calculations (thx Dr. Koga)
- **ver4.265** (2015/03/13) Fixed a bug about the calculation of the Wyckoff position of P63/mmc. (thx Dr. Nagasako)
- **ver4.264** (2015/03/07) Improved 'Spot ID' function.
- **ver4.263** (2015/01/28) Improved 'Spot ID' function.
- **ver4.262** (2015/01/26) Updated help files.
- **ver4.261** (2015/01/24) Improved: a support of DM4 file on 'Spot ID'.
- **ver4.26** (2015/01/23) Added a new function, 'Spot ID', where diffraction spots could be semi-automatically identified.

### 2014

- **ver4.252** (2014/11/11) Fixed: minor bugs.
- **ver4.251** (2014/11/10) Fixed: minor bugs.
- **ver4.25** (2014/11/06) Added: SACLA EH5 optics for single crystal diffraction mode.
- **ver4.242** (2014/10/27) Fixed a bug on scattering factor information.
- **ver4.241** (2014/10/21) Fixed minor bugs on OpenGL calculations.
- **ver4.24** (2014/07/14) Improved 'Powder Diffraction'. (but not all functions are implemented yet)

### 2013

- **ver4.234** (2013/12/17) Improved language option
- **ver4.233** (2013/10/28) Improved appearance for >100% DPI scaling
- **ver4.232** (2013/10/15) Improved appearance for >100% DPI scaling
- **ver4.231** (2013/08/10) Fixed minor bugs on OpenGL.
- **ver4.23** (2013/03/28) Improved structure viewer.
- **ver4.221** (2013/02/26) Changed address of help page.
- **ver4.22** (2013/02/25) Added: Update check function
- **ver4.21** (2013/02/21) Added: CIF file export function

### 2012

- **ver4.202** (2012/12/20) Fixed a small bug.
- **ver4.201** (2012/12/19) Fixed a small bug.
- **ver4.20** (2012/12/17) Fixed OpenGL library.
- **ver4.191** (2012/12/05) Fixed minor bugs.
- **ver4.19** (2012/08/11) Improved: appearance in TEMID window.
- **ver4.184** (2012/06/22) Added: 'Reset registry keys' function was added in the 'Option' menu
- **ver4.183** (2012/06/03) Bug Fix
- **ver4.182** (2012/06/01) Bug Fix
- **ver4.181** (2012/05/31) Bug Fix
- **ver4.18** (2012/05/23) Improved: speed up of calculation of Debye ring simulation.

### 2011

- **ver4.17** (2011/12/28) Improved: Space groups A1, B1, C1, and F1 were added.
- **ver4.161** (2011/12/05) Fixed: a small bug on Debye ring simulation was fixed.
- **ver4.16** (2011/12/04) Improved: speed up of calculation of Debye ring simulation.
- **ver4.15** (2011/11/24) Improved: Stricter polycrystalline diffraction pattern can be calculated considering beam convergence and monochromaticity.
- **ver4.142** (2011/11/20) Improved: Stereonet simulator can draw vectors of specified indices selected by users.
- **ver4.141** (2011/11/07) Fixed: PolycrystallineDiffractionSimulation
- **ver4.14** (2011/11/01) Fixed the critical mistake on polycrystalline diffraction simulation: Intensity calculation was corrected.
- **ver4.131** (2011/11/01) Fixed: Y axis direction on polycrystalline diffraction simulation form was corrected.
- **ver4.13** (2011/10/31) Improved: Polycrystalline diffraction simulation; Fixed: File->Close function.
- **ver4.12** (2011/10/31) Improved: Polycrystalline diffraction simulation
- **ver4.113** (2011/10/30) Fixed a bug: projection buttons on main form in Japanese mode
- **ver4.112** (2011/10/21) Fixed a bug when sending crystal data.
- **ver4.111** (2011/10/17) Fixed problems on Single Crystal Diffraction form.
- **ver4.11** (2011/10/12) Fixed problems on import CIF format.
- **ver4.10** (2011/10/12) Added language option. English and Japanese are available.
- **ver4.00** (2011/07/19) Added input and output of isotope compositions and the intensity calculation for neutron diffraction.
- **ver3.922** (2011/07/05) Fixed a bug where Crystal Information overflowed its area.
- **ver3.921** (2011/07/05) Minor fix to yesterday's change. Space-group information (Symmetry info.) and structure factors (Scattering factor) are now shown separately.
- **ver3.92** (2011/07/04) Added "Detailed Information" to the main toolbar. It shows space-group information and structure factors.
- **ver3.91** (2011/05/10) Fixed a mistake in TEMID in judging equivalent axes.
- **ver3.90** (2011/04/21) Improved the Diffraction Simulator. Not quite finished yet, but released for now.
- **ver3.811** (2011/02/29) Improved the Diffraction Simulator (work in progress). Still unfinished, but released for now because it was requested.

### 2010

- **ver3.81** (2010/11/18) Changed the link target of the help pages. The content is being written.
- **ver3.80** (2010/11/08) Native code is now generated in the background at the first start, so the second and later starts are faster.
- **ver3.701** (2010/11/08) Fixed a coding error in the symmetry of the triclinic system.
- **ver3.70** (2010/11/07) Faster start-up, probably several times faster.
- **ver3.62** (2010/07/21) Stereonet projection now supports the Schmidt net (equal-area projection).
- **ver3.61** (2010/05/09) Moved the development environment to Visual Studio 2010.
- **ver3.60** (2010/01/07) Added the display of Debye-ring patterns for randomly oriented crystals.

### 2009

- **ver3.59** (2009/12/24) Fixed some errors in the calculation of atomic positions (especially the symmetry of centred lattices).
- **ver3.58** (2009/10/20) Fixed a bug that prevented crystal data from being sent between applications.
- **ver3.57** (2009/09/26) The Diffraction Simulator can now show a precession camera pattern (ZOLZ); added the intensity calculation for X-rays.
- **ver3.56** (2009/09/24) Images can now be overlaid in Electron Diffraction.
- **ver3.55** (2009/09/03) Added support for 64-bit operating systems.
- **ver3.54** (2009/06/01) Fixed a bug where colors could not be changed in the stereonet drawing.
- **ver3.53** (2009/03/11) Excitation errors and crystal structure factors of diffraction spots can now be shown. The display gets crowded, so a way to choose what is shown is being considered.
- **ver3.52** (2009/03/10) Fixed a bug in reading CIF files.

### 2008

- **ver3.51** (2008/10/30) Fixed a bug in the 'Apply to same elements' function.
- **ver3.50** (2008/08/31) Fixed a bug in saving images.
- **ver3.49** (2008/08/27) Structure Viewer can now also save the legend and crystal-axis images.
- **ver3.48** (2008/08/27) Added support for the irregular space groups A-1, B-1, C-1, I-1 and F-1; fixed Structure Viewer images that could not be saved.
- **ver3.47** (2008/08/26) Structure Viewer: the background and text colors can now be changed, and the colors from the last session are restored.
- **ver3.46** (2008/08/20) Fixed a problem in reading CIF files.
- **ver3.45** (2008/07/10) Added (or rather restored) the great-circle drawing function.
- **ver3.44** (2008/06/20) Changed the e-mail address and other contact details because the author moved to a new position.
- **ver3.43** (2008/04/29) Corrected the lattice constants of SiO2 (CaCl2 structure) in the initial crystal file.
- **ver3.42** (2008/04/22) The crystals to be read or written can now be selected.
- **ver3.41** (2008/04/13) Fixed a bug that made SMAP output (.out) files unreadable.
- **ver3.40** (2008/02/28) Structure Viewer now shows the coordination of atoms.
- **ver3.39** (2008/02/24) Slightly faster calculation of electron diffraction intensities.
- **ver3.38** (2008/02/23) Added the kinematical calculation of electron diffraction intensities. The calculation speed will be improved.
- **ver3.37** (2008/02/12) Drag and drop of crystal files; better cooperation with external applications; design changes.
- **ver3.36** (2008/02/07) Bug fixes; design changes.
- **ver3.35** (2008/01/29) Fixed a bug in the calculation of Wyckoff positions (how long will they keep turning up...).
- **ver3.34** (2008/01/28) Fixed a bug in the calculation of Wyckoff positions.
- **ver3.33** (2008/01/28) Fixed a wrong resolution setting when FormElectron (electron diffraction) is first shown (thanks to Nagata).
- **ver3.32** (2008/01/25) Tips can now be shown at start-up.
- **ver3.31** (2008/01/21) Changed the distribution site.
- **ver3.30** (2008/01/21) Double-clicking a TEMID result now applies it to the rotation angles.
- **ver3.29** (2008/01/18) Added button images.
- **ver3.28** (2008/01/16) Fixed parts of the design that looked wrong.
- **ver3.27** (2008/01/14) Changed the design.
- **ver3.26** (2008/01/10) Fixed a bug where the legend was not shown properly when there was only one atom.
- **ver3.26** (2008/01/08) Fixed malfunctions of forms.
- **ver3.25** (2008/01/07) Changed the internal format; added support for the errors of lattice constants and atomic positions.

### 2007

- **ver3.24** (2007/12/26) In Structure Viewer, right-clicking after selecting an atom now shows its coordination environment (to be extended to other places).
- **ver3.23** (2007/12/26) Electron Diffraction can now show d-spacings and distances from the reciprocal-lattice origin.
- **ver3.22** (2007/12/21) Structure Viewer can now show a legend of the atoms.
- **ver3.21** (2007/11/12) Fixed a bug where some bonds were sometimes not shown in Structure Viewer.
- **ver3.20** (2007/11/12) Added an animation (automatic rotation) function to Structure Viewer.
- **ver3.19** (2007/11/09) The main window now shows the directions of the crystal axes.
- **ver3.18** (2007/11/07) Added a print function.
- **ver3.17** (2007/11/07) Fixed a bug where one edge of the unit cell was not drawn in Structure Viewer; more tooltips.
- **ver3.16** (2007/11/03) Added saving and copying of images in Stereonet and Electron Diffraction; fixed a bug where colors could not be changed in Electron Diffraction.
- **ver3.15** (2007/10/27) Separated the common forms and controls into a DLL.
- **ver3.14** (2007/10/26) Fixed a bug where the camera length could not be changed.
- **ver3.13** (2007/09/26) Structure-analysis data of SMAP (http://www.sci.hokudai.ac.jp/~hiro/) can now be read directly.
- **ver3.12** (2007/08/21) Fixed a bug in the Lattice Plane display of Structure Viewer; faster overall.
- **ver3.11** (2007/08/21) Further improvements against bugs such as sudden crashes. This time for sure?
- **ver3.10** (2007/08/15) Further improved the bug below. Drawing text is hard...
- **ver3.09** (2007/08/14) Fixed a bug where Stereonet and Electron Diffraction sometimes stopped.
- **ver3.08** (2007/08/08) Fixed bugs related to OpenGL.
- **ver3.07** (2007/08/07) Stereonet and Electron Diffraction are now drawn with OpenGL. Faster, but there may still be bugs...
- **ver3.06** (2007/07/05) Fixed a bug in the calculation of crystal axes.
- **ver3.05** (2007/07/05) Added functions to TEMID (symmetry check, extraction from multiple patterns, etc.).
- **ver3.04** (2007/06/22) Changed the home page address.
- **ver3.03** (2007/06/22) Information on the selected symmetry can now be shown.
- **ver3.02** (2007/06/21) Crystal data can now be listed and read from an XML file in the same format as PDIndexer.
- **ver3.01** (2007/06/10) Added the Electron Diffraction and TEMID forms.
- **ver3.00** (2007/05/30) Beta version, extensively rebuilt from ver2.40. Still a work in progress.

### 2004

- **ver2.40** (2004/05/13) Added great-circle drawing to StereoNet; added Wyckoff-position information to StereoNet and Geometrics.

### 2003

- **ver2.31** (2003/11/12) Bug fixes; improved StereoNet and Diffraction.
- **ver2.30** (2003/11/12) Added a database function; changed the format of the settings file; added image output to StereoNet and Diffraction.
- **ver2.20** (2003/10/28) Added the display of Kikuchi lines; partial design changes.
- **ver2.12** (2003/10/23) Bug fixes.
- **ver2.11** (2003/10/13) Partial design changes; faster drawing and bug fixes.
- **ver2.10** (2003/10/04) Improved the design of TemID (pattern input); expanded the Help file.
- **ver2.00** (2003/09/28) Moved the development environment to the .NET Framework; added image analysis; TemID results can now be applied to the stereonet and the reciprocal space.

### 2002

- **ver1.05** (2002/05/01) Added a dialog explaining Tilt, Azimuth and Rotation; added a slider to adjust the radius of the Ewald sphere; faster display of reciprocal-lattice points; bug fixes.
- **ver1.04** (2002/04/22) Added the display of the reciprocal space; bug fixes.
- **ver1.03** (2002/03/30) Added zooming of the stereonet; corrected errors in space groups; bug fixes.
- **ver1.02** (2002/03/28) Added stereonet functions; bug fixes.
- **ver1.01** (2002/03/14) Corrected errors in space groups; added link buttons between PHOTOs; added a mode for analysis from three diffraction spots.
- **ver1.00** (2002/03/03) Created a provisional working version.
