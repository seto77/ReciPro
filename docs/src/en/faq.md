# Q&A

<!-- 261124Cl added: points users often get stuck on or misread, collected from GitHub issues and e-mail inquiries.
     Start-up failures and broken layouts belong to troubleshooting.md (not duplicated here).
     Each answer was checked against the current code and resx by codex2 (round 2; .project-guidance/reports/codex2_261124_review/manual_r02_out.md, part C). -->

This page collects questions about ReciPro that come up repeatedly: points where users get stuck and things that are easy to misread. Most of them come from [GitHub issues](https://github.com/seto77/ReciPro/issues) and e-mail inquiries.

!!! tip "If ReciPro does not start or looks wrong"
    For problems such as ReciPro not starting, the window not appearing, 3D views being unavailable or buttons being hidden, see [Troubleshooting](troubleshooting.md). If a problem appeared in an old version, check whether it still occurs in the [latest version](https://github.com/seto77/ReciPro/releases/latest).

---

## Preparing crystal data

### Q. When I enter my own crystal, atoms appear at every position in the unit cell

**A.** ReciPro generates the equivalent positions automatically from the symmetry operations of the space group. In the atom list, enter **only one representative coordinate for each site (each set of positions related by the symmetry operations)**. If you also enter the equivalent positions by hand, the atoms are duplicated.

Also check that the space group matches the structure. For example, D0₃-type Cu₃Al is described in **Fm-3m** (not Fd-3c) with the following three sites (Al on 4a, Cu on 4b and 8c).

| Atom | x | y | z |
|---|---|---|---|
| Al | 0 | 0 | 0 |
| Cu | 1/2 | 0 | 0 |
| Cu | 1/4 | 1/4 | 1/4 |

See [0. Main window](0-main-window.md) for details of the input fields.

### Q. How do I import a CIF file? Can I import many at once?

**A.** Drag and drop the CIF onto the crystal information area, or use **File ▸ Import crystal from CIF or AMC...** in the main window. To use the imported crystal as a candidate in Spot ID v2, press **↑ Add ↑** to add it to the crystal list and select it there. (Issues [#19](https://github.com/seto77/ReciPro/issues/19), [#51](https://github.com/seto77/ReciPro/issues/51))

To import or export many CIF files at once, use a [macro](20-macro/index.md) (see [20.1. Built-in functions](20-macro/1-built-in-functions.md) for the list of functions). (Issues [#41](https://github.com/seto77/ReciPro/issues/41), [#45](https://github.com/seto77/ReciPro/issues/45))

### Q. A crystal I entered by hand shows all atoms with the same color and size in the Structure Viewer

**A.** Press **Like Vesta** on the Structure Viewer toolbar to redraw the atoms with element-specific colors and sizes. This button also rebuilds the bond settings, so set your own bonds again if you had defined them. (Issue [#44](https://github.com/seto77/ReciPro/issues/44))

---

## Orientations and indices

### Q. The zone axis is shown with negative indices, such as [00-1]. Is the coordinate system left-handed?

**A.** ReciPro uses a right-handed coordinate system ([A1.1. Basic coordinate system and crystal orientation](appendix/a1-coordinate-system/1-orientation.md)). [uvw] and [−u −v −w] describe the same zone axis with opposite directions; the sign tells which way the crystal faces (for example, whether the c axis points toward or away from the viewer). Double-click a result row in Spot ID v1, or select a candidate grain row in Spot ID v2, to apply that orientation to the main window and check the direction visually. (Issue [#27](https://github.com/seto77/ReciPro/issues/27))

### Q. Can I enter the Euler angles output by my EBSD software as they are?

**A.** ReciPro's Euler angles (Φ, Θ, Ψ) are rotations in Z–X–Z order ([A1.1](appendix/a1-coordinate-system/1-orientation.md)). Many EBSD programs output Bunge angles (φ₁, Φ, φ₂), which are also Z–X–Z rotations, but the reference axes (the choice of the specimen frame) and the sense of rotation differ between programs and settings. If the output is expressed in the specimen frame with the specimen tilt (about 70°) removed, the crystal is shown as seen along the normal of the polished surface. Whether you can enter the angles as they are must be checked the first time with a known specimen: check that **both the surface normal and a known in-plane direction** match. To convert between different angle definitions, use [4. Rotation Geometry](4-rotation-geometry.md). (Issue [#36](https://github.com/seto77/ReciPro/issues/36))

### Q. In a monoclinic crystal, the normal of the (hkl) plane does not coincide with the [hkl] direction

**A.** The normal of (hkl) coincides with [hkl] only in special cases such as cubic crystals; in general there is an angle between them. ReciPro treats the two separately. You can read the angle in **Geometrics calculation** of [2. Symmetry information](2-symmetry-information.md) by entering the plane and direction indices. (Issue [#64](https://github.com/seto77/ReciPro/issues/64))

### Q. Can I use four-index (Miller–Bravais) notation for hexagonal crystals?

**A.** Turn on **Options ▸ Use Miller-Bravais (hkil) index** in the main window. The indices of **planes** of hexagonal crystals, and of trigonal crystals described on hexagonal axes, are then shown as (hkil) in the figures and tables that support it (trigonal crystals on rhombohedral axes are not affected). **Directions** stay three-index [uvw]. (Issue [#54](https://github.com/seto77/ReciPro/issues/54))

---

## Diffraction simulator

### Q. The beam is parallel to a zone axis, so why do reflections of that zone appear?

**A.** With the beam exactly parallel to a zone axis, all planes of that zone are parallel to the beam and do not satisfy the Bragg condition exactly. Still, a crystal of finite thickness gives diffracted intensity slightly off the Bragg condition (with a non-zero excitation error). If the incident beam has an angular spread, other incident directions also contribute. The size of this deviation is the **excitation error** (the distance between a reciprocal-lattice point and the Ewald sphere). Within the zeroth-order Laue zone, the excitation error tends to grow for higher-angle reflections, and how far the drawn pattern extends depends on the calculation and drawing settings. See [7.1. SAED simulation](7-diffraction-simulator/1-saed-simulation.md) for details.

### Q. I want the simulation on the same scale as my experimental pattern

**A.** In the diffraction simulator, set the unit of **Resolution** to **nm⁻¹/px**, enter the reciprocal-space size of one pixel of the experimental image (its calibration), and set **Size (W×H)** to the width and height of the experimental image in pixels ([7. Diffraction simulator](7-diffraction-simulator/index.md)). (Issue [#30](https://github.com/seto77/ReciPro/issues/30))

To draw every spot as a dot of the same size, choose **Only excitation error** for the intensity and turn on **Draw all spots with the same radius**. Because of the curvature of the Ewald sphere, only reflections close to the sphere are drawn. As a reference for drawing a wide area of the reciprocal lattice as a flat plane, an extremely high accelerating voltage (e.g. 10,000 kV) makes the Ewald sphere almost flat (use the actual accelerating voltage when you compare with an experiment).

### Q. Can I label the spots with 1/d or the scattering angle?

**A.** In the spot labels of the diffraction simulator you can choose **1/d**, **2θ** (scattering angle), **χ** (azimuth: 0° at 12 o'clock before any display flip, positive clockwise; not shown for 000) and others. 1/d is shown in nm⁻¹ or Å⁻¹ following the length unit of the main window, and the angles in degrees. (Issues [#21](https://github.com/seto77/ReciPro/issues/21), [#64](https://github.com/seto77/ReciPro/issues/64))

### Q. I want to color the spots of several crystals differently

**A.** Select the crystals one by one in the main window and set **Color** in the crystal information. Select the crystals to compare and turn on **Color per crystal** in the diffraction simulator. (Issue [#65](https://github.com/seto77/ReciPro/issues/65))

### Q. The pattern looks flipped compared with my experimental photograph

**A.** The pattern is mirrored depending on whether the viewing screen is seen from the front or from the back. Use **Horizontal flip** / **Vertical flip** in the diffraction simulator to match the orientation, and **Negative image** to match the contrast. (Issue [#35](https://github.com/seto77/ReciPro/issues/35))

### Q. The diffraction pattern is completely black and nothing is shown

**A.** First check the zoom. Right-click the drawing area to zoom out, or set **Resolution** to mm/px and try a value of about 0.2 (the initial value is 0.08 mm/px). Also check the energy of the incident beam. See [Troubleshooting](troubleshooting.md#diffraction-simulator) for details. (Issues [#3](https://github.com/seto77/ReciPro/issues/3), [#61](https://github.com/seto77/ReciPro/issues/61))

### Q. Are the Kikuchi lines in the diffraction simulator dynamical?

**A.** It depends on the Kikuchi mode. **Geometric** draws only the line positions, and the default **Kinematical** shades the lines according to the reflection intensities. With electrons, **Dynamical** draws the band intensity profiles from a two-beam dynamical calculation with thermal diffuse scattering as the source. To obtain EBSD patterns from a many-beam dynamical calculation, use [12. EBSD simulation](12-ebsd-simulation.md). (Issue [#36](https://github.com/seto77/ReciPro/issues/36))

### Q. The structure factors do not match those of other software

**A.** Check the following. (Issue [#39](https://github.com/seto77/ReciPro/issues/39))

- **Units**: in the reflection table of [3. Beam interaction](3-beam-interaction.md), the electron structure factor F is given in nm and F² in nm² (switching the length unit of the main window to Å does not change this). When comparing with software that uses Å, multiply F by 10 and F² by 100.
- **Scattering factors**: check that each atom uses the same scattering factor (neutral atom or ion). Differences tend to appear in low-angle reflections.
- **Occupancies and atomic displacement parameters (B, U)**: match the occupancy of each site with the other software. If the other software ignores thermal vibration, set the corresponding B or U to zero.

The formulas and sources of the atomic scattering factors are given in [A2.1. Atomic scattering factors](appendix/a2-beam-interaction/scattering-factor.md). (Issue [#5](https://github.com/seto77/ReciPro/issues/5))

---

## Spot ID (indexing diffraction patterns)

### Q. In Spot ID v2, pressing "Identify spots" does not index the spots

**A.** Check the following. (Issues [#11](https://github.com/seto77/ReciPro/issues/11), [#51](https://github.com/seto77/ReciPro/issues/51))

1. A diffraction image is loaded and, besides the direct (000) spot, at least two diffraction spots are registered (orientation candidates are built from two reciprocal vectors that make an angle larger than 30° and smaller than 150°).
2. The candidate crystal is in **the crystal list of the main window** and is selected there (a common mistake is to import a CIF without adding it to the list).
3. The direct (000) spot is set correctly.
4. The **camera length** and **pixel size** match the recording conditions (when the pixel size is entered in nm⁻¹, the camera length is not used; see the next question).

### Q. I do not know the camera length. Can I index the FFT of an HRTEM image?

**A.** Yes. If you set the unit of the pixel size to **nm⁻¹**, the camera length is not used. (Issue [#25](https://github.com/seto77/ReciPro/issues/25))

For an FFT image, obtain the pixel size from the formula below, where the number of FFT points is the number of points used for the FFT along that axis (the pixel count of the cropped region, or the padded size if you zero-padded).

$$
\text{pixel size}\ [\text{nm}^{-1}] = \frac{1}{\text{resolution of the real-space image}\ [\text{nm/px}] \times \text{number of FFT points}}
$$

For example, with a real-space resolution of 0.02237 nm/px and 1024 FFT points, the pixel size is 1/(0.02237 × 1024) ≈ 0.0437 nm⁻¹. If an FFT image made in DigitalMicrograph (dm3/dm4) carries a calibration that ReciPro recognizes as nm⁻¹, it is read automatically and this calculation is not needed.

Spots in FFT images are sharp, so making the **Fitting range (radius)** smaller than the default of 20 px (about 15 px for a 1024-point FFT) can make the peak fitting more stable.

### Q. The direct beam is hidden behind a beam stopper. Can I set the direct spot by hand?

**A.** **Ctrl + left double-click** on the image adds a spot near that position and sets it as the direct (000) beam. To make an existing spot the direct beam, use the check box in the leftmost column of the spot list. The spot position is determined by peak fitting (a peak must be found within 10 px of the click), so if the beam is completely hidden, keep the rows of the observed diffraction spots, add a separate row for the center at a nearby peak, make it the direct beam, select that row and move it to the estimated center with **Ctrl + arrow keys** (one screen pixel per press). Moving the row of an observed spot itself would lose its observed position. See [11. Spot ID v2](11-spot-id-v2.md) for the list of operations.

### Q. Can I measure the angle between three spots?

**A.** Yes, from v.4.954. Hold **Shift** and left-click three registered spots in the order end → vertex → end; the angle in the image plane is shown on the image and in the status bar. (Issue [#74](https://github.com/seto77/ReciPro/issues/74))

---

## HRTEM and STEM simulation

### Q. Atom columns are not resolved in the HRTEM image

**A.** With the spherical aberration Cs, the chromatic aberration Cc, the energy spread of the incident electrons ΔE, the defocus and the objective aperture you set, the spatial frequency of the spacing you want to see may not be transferred well enough. To isolate the cause, calculate with Cs = 0 and Δf = 0 and see whether the image changes. Enter ΔE as the full width at half maximum (FWHM). (Issues [#9](https://github.com/seto77/ReciPro/issues/9), [#14](https://github.com/seto77/ReciPro/issues/14))

### Q. I want to compare the calculated HRTEM intensities as numbers

**A.** Turn off the **Min** / **Max** intensity normalization and **Gaussian blur (FWHM)**, and save as TIFF. Each image is saved as floating-point values. When you compare with the vacuum intensity taken as 1, use the same vacuum reference for all the images you compare. (Issue [#13](https://github.com/seto77/ReciPro/issues/13))

### Q. The STEM image becomes completely dark when I increase the inner detector angle

**A.** If an **elastic STEM image** is dark, the diffracted waves included in the calculation may be limited to low angles and may not reach the detector angles. Increase **Diffracted waves** and check that the image no longer changes. This happens especially in crystals of low symmetry. If a HAADF image including thermal diffuse scattering (TDS) is dark, check the selected signal and that the atomic displacement parameter B of each site is not zero. (Issue [#24](https://github.com/seto77/ReciPro/issues/24))

### Q. What inner and outer angles should I use for the annular detector?

**A.** The range of scattering angles collected by the detector depends on both the camera length (which changes with the projector-lens current) and the physical size of the detector. Enter the inner and outer semi-angles (mrad) calibrated for your instrument in **Inner radius** / **Outer radius**. From a macro, use `ReciPro.STEM.DetectorInnerAngle` / `ReciPro.STEM.DetectorOuterAngle` (mrad) ([20.1. Built-in functions](20-macro/1-built-in-functions.md)). (Issue [#46](https://github.com/seto77/ReciPro/issues/46))

---

## EBSD simulation

### Q. The master pattern is black after the calculation

**A.** A site without a valid atomic displacement parameter does not contribute to the **non-local backscattering source**, which comes from thermal diffuse scattering. If this is true for **all sites**, a master pattern that uses this source (the default) is black. From v.4.950, if even one site has B = 0 (sites with a valid anisotropic displacement are excluded), ReciPro tells you before building the master pattern and offers to set an isotropic B = 0.5 Å² in the crystal data of those sites. If you know the actual values, cancel and enter them in the atom fields. (Issue [#68](https://github.com/seto77/ReciPro/issues/68))

### Q. Does the EBSD calculation use the GPU?

**A.** No. The master pattern (Bloch waves), the Monte Carlo step and the projection onto the detector all run on the CPU. The GPU is used only for OpenGL drawing (the geometry view and the 3D master-pattern sphere). (Issue [#73](https://github.com/seto77/ReciPro/issues/73))

### Q. The master pattern is already built, but the pattern looks coarse while I rotate the crystal

**A.** Each time the orientation changes, the detector pattern is recomposed from the existing master pattern. While you rotate by dragging the mouse, the resolution of the detector pattern is lowered only if the previous normal drawing took long (currently more than 30 ms), and the pattern is redrawn at full resolution when you release the button. The master pattern is not recalculated. (Issue [#73](https://github.com/seto77/ReciPro/issues/73))

---

## Macros and automation

### Q. I want to save diffraction patterns automatically while changing the orientation step by step

**A.** With a [macro](20-macro/index.md) you can write loops in Python-like syntax. In [20.2. Examples](20-macro/2-examples.md), example 2 rotates by a fixed angle and saves PNG images, and example 3 sets the Euler angles directly. To specify each orientation by its angles, call `ReciPro.Dir.EulerInDeg(phi, theta, psi)` (in degrees) inside the loop. ReciPro can also be controlled from an external program ([20.3. External control](20-macro/3-external-control.md)). (Issues [#36](https://github.com/seto77/ReciPro/issues/36), [#37](https://github.com/seto77/ReciPro/issues/37))

### Q. Can the macro `ReciPro.Dir.ProjectAlongAxis(u, v, w)` change the axis setting (unit-cell setting) of a CIF?

**A.** No. `ProjectAlongAxis` only changes the **orientation** of the crystal so that [uvw] is perpendicular to the screen; it does not change the unit-cell setting, the lattice constants or the fractional coordinates. To change the setting, the lattice constants and atomic coordinates must be transformed with the space-group setting in mind. (Issue [#46](https://github.com/seto77/ReciPro/issues/46))

---

## Other

### Q. How do I draw the great circle of a zone in the stereonet?

**A.** In the **Great circle** tab of the stereonet, specify the zone axis [uvw], or enter two non-parallel crystal planes (hkl) that belong to the zone, and add it ([6. Stereonet](6-stereonet.md)). (Issue [#58](https://github.com/seto77/ReciPro/issues/58))

### Q. How far must I tilt the TEM specimen holder to reach another zone axis?

**A.** Use **TEM holder simulation** in the diffraction simulator. Set **Direction of Tilt X** and **Polarity of Tilt Y** for your instrument. Enter the current holder Tilt X and Y in the link fields and press **Link to current direction**; when you place the mouse pointer on the target zone axis, the Tilt X and Y that reach it are shown. The difference from the current values, axis by axis, is the tilt you need. The direction of the holder tilt axes relative to the diffraction pattern differs between instruments. (Issue [#52](https://github.com/seto77/ReciPro/issues/52))

### Q. Can I use ReciPro on a Mac or on Windows on ARM?

**A.** For the Mac, see [the Mac/Linux item in Troubleshooting](troubleshooting.md#mac-linux); for Windows on ARM, see [the Windows on ARM item](troubleshooting.md#windows-on-arm). With Parallels on a Mac, updating Parallels to a newer version has made ReciPro work in one reported case.

### Q. I used ReciPro in a paper. How should I cite it?

**A.** Please cite the following paper. The citation data are also available from **Cite this repository** on the [GitHub repository page](https://github.com/seto77/ReciPro). (Issue [#33](https://github.com/seto77/ReciPro/issues/33))

> Seto, Y. & Ohtsuka, M. (2022). *J. Appl. Cryst.* **55**, 397–410. doi:[10.1107/S1600576722000139](https://doi.org/10.1107/S1600576722000139)

---

For questions not covered here, or to report a bug, please open a [GitHub issue](https://github.com/seto77/ReciPro/issues). Including the ReciPro version, the steps to reproduce, and any error message or screenshot helps a lot.
