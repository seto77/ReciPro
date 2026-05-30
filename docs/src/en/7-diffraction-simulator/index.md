---
title: Diffraction Simulator
---

# Crystal Diffraction (Diffraction Simulator)

**Crystal Diffraction** simulates single-crystal X-ray and electron diffraction patterns.

![Diffraction Simulator](../../assets/cap-en-auto/FormDiffractionSimulator.png)

---

## Keyboard & mouse shortcuts

This applies to the diffraction-pattern window shared by the X-ray, SAED, and PED simulations. Dragging on the pattern rotates the **crystal**; note there is **no mouse-wheel zoom** here — zoom with right-click / right-drag.

| Shortcut | Action |
|----------|--------|
| <kbd>F1</kbd> | Open this page of the online manual |
| Left-drag near the centre | Tilt the crystal |
| Left-drag the outer area | Spin the crystal about the beam axis |
| Left double-click a spot | Show reflection details (index, *d*, structure factor, excitation error) |
| Middle-drag | Pan the pattern |
| <kbd>CTRL</kbd> + Middle-drag | Move the detector centre (when the detector area is shown) |
| Right-click | Zoom out |
| Right-drag a box | Zoom in to the selected region |
| Right double-click the status bar | Copy a text summary of the current settings |
| Right double-click a lit layer button (Spots / Kikuchi / Debye / Scale) | Blink that layer on and off |

The auxiliary windows opened from here add a few more:

| Shortcut | Action |
|----------|--------|
| Left double-click the stereonet — **TEM holder** window | Set the holder tilt to that point |
| Arrow keys — **TEM holder** window | Step the holder tilt (tick **Arrow keys** first) |
| Drop a `.prm` file or an image — **Detector geometry** window | Load detector geometry / overlay image |
| Drop a `.txt` profile — **Dynamic compression** window | Load a pressure/time profile (drag the red line in the graph to scrub) |

The application-wide <kbd>CTRL</kbd>+<kbd>SHIFT</kbd> shortcuts from the [main window](../0-main-window.md#keyboard-mouse-shortcuts) also work while this window is focused.

→ See **[21. Keyboard & mouse shortcuts](../21-shortcuts.md)** for every window at a glance.

---

## Quick Routes by Goal

| Goal | Start from | Reference |
|------|------------|-----------|
| Simulate parallel-beam electron diffraction (SAED) | Set **Incident beam mode** to **Parallel beam** and **Wave Length** to electron | [SAED simulation](2-saed-simulation.md), [parallel-beam SAED calculation](../appendix/a2-bloch-wave/calculation.md#parallel-beam-saed) |
| Simulate single-crystal X-ray diffraction | Switch **Wave Length** to X-ray / Synchrotron | [X-ray diffraction](1-x-ray-diffraction.md) |
| Simulate precession electron diffraction (PED) | Set **Incident beam mode** to **Precession (electron)**, then set semi-angle and step | [PED simulation](3-ped-simulation.md) |
| Simulate convergent-beam electron diffraction (CBED) | Set **Incident beam mode** to **Convergence (electron)** and configure the CBED window | [CBED simulation](4-cbed-simulation.md), [CBED calculation](../appendix/a2-bloch-wave/cbed.md) |
| Inspect the reflection list from the dynamical calculation | Select **Dynamical theory**, then open **Spot Details** or tick **Details** | [Dynamical calculation](../appendix/a2-bloch-wave/calculation.md) |
| Match detector geometry against an experimental image | Open the detector-geometry settings and use the overlay image | [Detector coordinate system](../appendix/a1-coordinate-system/2-diffraction.md) |

---

## Main area

A diffraction pattern is simulated in the area displayed in the centre of the screen.

### Mouse operation

See [Keyboard & mouse shortcuts](#keyboard-mouse-shortcuts) at the top of this page.

### Mouse position

The information corresponding to the current mouse position is displayed in the readout strip above the pattern (cursor *q*, *d*, 2θ, azimuth, and so on). Ticking **More details** expands this readout with additional fields (Miller indices of the nearest reflection, excitation error, structure factor, etc.).

---

## File menu

| Menu item | Description |
|-----------|-------------|
| **Save** | Save the displayed diffraction pattern to a file. |
| **Save detector area** | Save only the detector-area crop. |
| **Copy** | Copy the displayed image to the clipboard. |
| **Copy detector area** | Copy only the detector-area crop. |

### Preset

Save and recall a complete simulator configuration — wavelength, detector geometry, tab settings, and spot-property settings — as a preset. Useful when switching between instruments / acquisition modes.


---

## Toolbar

![Toolbar](../../assets/cap-en-auto/FormDiffractionSimulator.toolStripContainer1.toolStrip3.png)

Spots, Kikuchi Lines, Debye rings, Scale, label options (Index / d / Distance / Excit. Err. / |Fg|).

---

## Display settings / Detector geometry

### Display settings

![Display settings](../../assets/cap-en-auto/FormDiffractionSimulator.toolStripContainer1.flowLayoutPanel6.groupBoxMonitor.png)

**Resolution**, image **Size (W×H)**, **Set the center to** / **Fix**, and **Horizontal flip / Vertical flip / Negative image** of the pattern. Tick **Reciprocal space** to draw the Ewald sphere and reciprocal-lattice vectors.

#### Resolution

The size of a pixel (mm). This value is a display-scale parameter and does not have to be the actual detector pixel size — it is updated automatically when you zoom with the mouse.

#### Size (W × H)

Pixel width and height of the drawing area. Depending on your display resolution, very large values may not be settable.

#### Set the centre to / Fix

Sets the centre of the pattern on the drawing area and (optionally) fixes it so that mouse panning cannot move it away from the chosen pixel.

#### Horizontal flip / Vertical flip / Negative image

Geometric and tonal inversions of the displayed pattern — useful for matching the orientation/contrast of an experimental image.

#### Reciprocal space

Draws the Ewald sphere and reciprocal-lattice vectors on top of the pattern, illustrating which reflections are excited.

### Misc

![Misc](../../assets/cap-en-auto/FormDiffractionSimulator.toolStripContainer1.flowLayoutPanel6.panelDetectorAndMisc.groupBoxMisc.png)

Includes the rotation-sensitivity slider and the **TEM holder simulation** button (see below).

### TEM holder simulation

Opens a window that links the diffraction pattern to a double-tilt (or rotation) **TEM holder**: set the holder tilt angles and the pattern/orientation updates accordingly, and the reachable orientations can be shown on a stereonet. Added in v4.914.

![TEM holder simulation](../../assets/cap-en-auto/FormDiffractionSimulatorHolder.png)

The stereonet (left) plots crystal axes/zone axes with the holder's tilt directions (Tilt-X, Tilt-Y arrows). Set the primary/secondary tilt angles under **Holder angles**; **Link to Current Direction** couples the holder to the current crystal orientation, and the TEM-specific settings define each tilt axis direction and polarity for your instrument.

### Detector geometry & overlay image

![Detector geometry & overlay image](../../assets/cap-en-auto/FormDiffractionSimulator.toolStripContainer1.flowLayoutPanel6.panelDetectorAndMisc.groupBoxDetectorGeometry.png)

---

## Tab menu

### General

![General tab](../../assets/cap-en-auto/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageGeneral.png)

Colours of spots, index labels, Kikuchi lines, Debye rings, and other overlays. Settings here apply to all rendering modes.

### Kikuchi lines

![Kikuchi tab](../../assets/cap-en-auto/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageKikuchi.png)

Active when **Kikuchi lines** is enabled on the toolbar.

#### Reflection selection

Choose which reflections produce Kikuchi lines by either:

- **Structure factor** — top *N* reflections sorted by |*F*ₕₖₗ|, or
- **1/d Cutoff** — all reflections with 1/d below the specified threshold (nm⁻¹).

#### Line appearance

Line width, line colour, and an option **Draw with kinematical intensity** that scales line intensity by the kinematical diffraction intensity rather than drawing all selected lines uniformly.

#### Threshold

The Kikuchi-line calculation is restricted to reflections with *d* greater than the specified threshold (legacy control retained for backward compatibility).

### Debye rings

![Debye tab](../../assets/cap-en-auto/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageDebye.png)

Active when **Debye rings** is enabled on the toolbar.

#### Ignore diffraction intensity

If checked, all Debye rings are drawn in the same colour and intensity, ignoring the crystal structure factor — useful for purely geometric pattern matching.

#### Show index label

If checked, the (*hkl*) index appears near each ring.

### Scale

![Scale tab](../../assets/cap-en-auto/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageScale.png)

Active when **Scale** is enabled on the toolbar.

#### 2θ / Azimuth scale lines

The **2θ** scale represents constant scattering angle (concentric rings), the **Azimuth** scale represents constant azimuth angle (radial lines from the centre). Colours are independently configurable.

#### Line width / Division / Show scale labels

- **Line width**: thickness of the scale lines.
- **Division**: angular interval between successive scale lines.
- **Show scale labels**: whether to draw numeric labels alongside the lines.

### Misc

Mouse rotation sensitivity and other miscellaneous controls.

#### Mouse sensitivity

Scales how far the crystal rotates per pixel of mouse drag.

---

## Spot property

Active when **Spots** is enabled on the toolbar.

### Wave Length

![Wave Length](../../assets/cap-en-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelWaveLength.png)

X-ray (characteristic/synchrotron), Electron, Neutron. Set energy or wavelength.

#### X-ray

Specify X-rays as the radiation source. For characteristic X-rays, set the **element** and the **transition** (Siegbahn notation — e.g. Kα₁, Kα₂, Kβ). For synchrotron X-rays, set **Element** to **0** (None) and enter the energy or wavelength directly.

#### Electron

Set the electron energy (keV) or wavelength (nm). The relativistic wavelength is computed from the energy.

#### Neutron

Set the neutron energy (meV) or wavelength (nm).

### Incident beam mode

![Beam mode](../../assets/cap-en-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelBeamMode.png)

Selects the geometry of the incident beam.

#### Parallel

A parallel incident beam — standard plane-wave geometry used for SAED and X-ray diffraction.

#### Precession (electron)

Simulates precession electron diffraction (PED). Available only with electron radiation. Selecting this mode automatically switches **Intensity calculation** to **Dynamical theory**.

#### Convergence (CBED, electron only)

Simulates a convergent electron beam (CBED). Available only with electron radiation. Selecting this mode automatically switches **Intensity calculation** to **Dynamical theory** and opens the [CBED setting](4-cbed-simulation.md) window.

### Intensity calculation

![Intensity](../../assets/cap-en-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelIntensity.png)

Selects the method used to compute spot intensities.

#### Only excitation error

Intensity is determined by the geometric excitation error $s_g$ (the distance between the Ewald sphere and the reciprocal-lattice point). Smaller $|s_g|$ gives higher intensity, peaking at the value set by **Radius**, and falling to zero when $|s_g|$ exceeds Radius. The structure factor is ignored.

#### Kinematical & excitation error

In addition to the excitation error, the kinematical structure factor |*F*ₕₖₗ|² is folded into the displayed intensity.

#### Dynamical theory

The Bloch-wave method is used to compute intensities, including multiple-scattering effects. Available only with electron radiation. See [Appendix A2. Bloch-wave method](../appendix/a2-bloch-wave/calculation.md).

### Appearance

![Appearance](../../assets/cap-en-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelAppearance.png)

Controls how each diffraction spot is rendered.

#### Solid sphere / Gaussian

The geometric model of the reciprocal-lattice point.

- **Solid sphere**: the reciprocal-lattice point is modelled as a sphere of radius *R* (see **Radius**). The intersection of the sphere with the Ewald sphere is drawn as a filled circle; its area corresponds to the diffraction intensity.
- **Gaussian**: the reciprocal-lattice point is modelled as a 3-D Gaussian with σ = *R* and a chosen integrated intensity. The cross-section with the Ewald sphere is a 2-D Gaussian and is drawn as such; the integral over the 2-D Gaussian gives the diffraction intensity.

#### Opacity

Transparency of the drawn spot (0 = transparent, 1 = opaque).

#### Radius

Radius *R* of the reciprocal-lattice point. The effective spot size depends on the combination of **Appearance** mode and **Intensity calculation**:

- **Gaussian + Only excitation error** — σ = *R*, integral = **Brightness**. Structure factor is ignored.
- **Gaussian + Kinematical** — σ = *R*, integral = Brightness × *I*ₖᵢₙ.
- **Gaussian + Dynamical** — σ = *R*, integral = Brightness × *I*ₐᵧₙ.
- **Solid sphere + Only excitation error** — sphere of radius *R*. Structure factor is ignored.
- **Solid sphere + Kinematical** — sphere of radius *R* × *I*ₖᵢₙ^(1/3).
- **Solid sphere + Dynamical** — sphere of radius *R* × *I*ₐᵧₙ^(1/2), so spot area is proportional to dynamical intensity.

#### Brightness

Active only in **Gaussian** mode. Sets the integrated intensity of the rendered Gaussian.

#### Color scale

Choose between **Gray scale** or **Cold-warm** colour map for intensity-coded spots.

#### Log scale

Display intensities on a logarithmic scale.

#### Spot color

Default colour for spots when the colour scale does not apply.

### Bloch wave settings

![Bethe parameters](../../assets/cap-en-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelBethe.png)

Active when **Dynamical theory** is selected.

#### Number of diffracted waves

Number of Bloch waves included in the eigenvalue problem. Larger values give more accurate intensities but increase computation time as *O*(*N*³).

#### Thickness

Sample thickness used in the dynamical calculation.

### PED settings (electron only)

![PED parameters](../../assets/cap-en-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelPED.png)

Active when **Precession (electron)** is selected.

#### Semi-angle

Half-angle of the precession cone in mrad.

#### Step

Number of parallel-beam directions sampled along the precession cone. The PED pattern is the sum of these parallel-beam dynamical calculations — more steps give smoother integration but increase computation time linearly.

---

## Detector geometry (detailed)

### Detector geometry settings

![Detector geometry settings](../../assets/cap-en-auto/FormDiffractionSimulatorGeometry.panelDetectorGeometry.png)

### Detector area and overlapped image

![Detector area](../../assets/cap-en-auto/FormDiffractionSimulatorGeometry.panelDetectorAreaAndOverlappedImage.png)

See [Appendix A1. Coordinate Systems](../appendix/a1-coordinate-system/2-diffraction.md).

---

## Diffraction spot information

Lists the per-reflection details computed by the Bloch-wave method. Open it with the **Spot Details** button (intensity-calculation panel) or the **Details** check box.

![Diffraction spot information](../../assets/cap-en-auto/FormDiffractionSpotInfo.png)

### Schematic and definitions

The schematic (top left) shows the vectors on the Ewald sphere and defines the quantities used in the table ($\hat{\mathbf{n}}$ is the unit vector normal to the sample surface, $\mathbf{k}$ is the incident wavevector, $\mathbf{g}$ is the reciprocal-lattice vector).

- $P_g = 2\,\hat{\mathbf{n}} \cdot (\mathbf{k} + \mathbf{g})$
- $Q_g = |\mathbf{k}|^2 - |\mathbf{k} + \mathbf{g}|^2 = -\mathbf{g} \cdot (2\mathbf{k} + \mathbf{g})$
- **Excitation error:** $S_g = \dfrac{\sqrt{P_g^2 + 4 Q_g} - P_g}{2}$
- **Evaluation function:** $R = |\mathbf{g}|\, Q_g^2$ — ranks reflections by how strongly they are excited (smaller = closer to the Ewald sphere = more strongly excited; the transmitted beam $g=0$ has $R=0$ and comes first). The table is sorted by ascending $R$.

### Table columns

| Column | Meaning |
|------|------|
| **R** | evaluation function $R = \lvert\mathbf{g}\rvert\, Q_g^2$ (above; used for selecting/ordering reflections) |
| **h, k, (i,) l** | Miller indices (*i* is the redundant hexagonal index, shown only for hexagonal crystals) |
| **d** | interplanar spacing (nm) |
| **gX, gY, gZ** | components of the reciprocal-lattice vector *g* (1/nm) |
| **\|g\|** | magnitude of *g* (1/nm) |
| **Vg re / Vg im** | Fourier coefficient of the crystal potential for elastic scattering, $V_g$ (real / imaginary) |
| **V'g re / V'g im** | imaginary (absorption) potential for thermal diffuse scattering, $V'_g$ (real / imaginary) |
| **Sg** | excitation error $S_g$ (above; 1/nm) |
| **Pg** | auxiliary quantity $P_g = 2\,\hat{\mathbf{n}}\cdot(\mathbf{k}+\mathbf{g})$ (above) |
| **Qg** | auxiliary quantity $Q_g = -\mathbf{g}\cdot(2\mathbf{k}+\mathbf{g})$ (above) |
| **Φ re / Φ im** | complex amplitude $\Phi$ of the dynamical diffracted wave on the exit surface (real / imaginary) |
| **\|Φ\|^2** | diffracted intensity $\lvert\Phi\rvert^2$ of that reflection |
| **Σ\|Φ\|^2** | cumulative sum of $\lvert\Phi\rvert^2$ (total over reflections; useful as an intensity-conservation check) |

### Potential units and other controls

- **Unit of potential** : switches the displayed potential between **Vg [eV]** (electrostatic potential, eV) and **Ug [nm⁻²]** (the scaled quantity $U_g = (2 m_0/h^2)\, V_g$ that enters the Bloch-wave equations). The column headers change accordingly between *Vg / V'g* and *Ug / U'g*.
- Above the table, the accelerating voltage, wavelength ($\lambda = 1/k_\text{vac}$), relativistic mass ratio $m/m_0$, speed ratio $v/c$, lattice volume, sample thickness, and (in CBED mode) the maximum semi-angle of the electron beam are shown.
- **Note 1:** the unit of length is **nm**, not Å. **Note 2:** the unit of wavenumber is **1/nm**, not 2π/nm.
- **Effective digit** : number of significant digits shown. **Auto resize row width** : auto-fit column widths. **Copy to clipboard** : exports the table as text that can be pasted into a spreadsheet.
