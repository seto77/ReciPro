---
title: Diffraction Simulator
---

# Crystal Diffraction (Diffraction Simulator)

**Crystal Diffraction** simulates single-crystal X-ray and electron diffraction patterns.

![Diffraction Simulator](../../assets/cap-en-auto/FormDiffractionSimulator.png)

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

## File menu


### Preset


---

## Toolbar

![Toolbar](../../assets/cap-en-auto/FormDiffractionSimulator.toolStripContainer1.toolStrip3.png)

Spots, Kikuchi Lines, Debye rings, Scale, label options (Index / d / Distance / Excit. Err. / |Fg|).

---

## Display settings / Detector geometry

### Display settings

![Display settings](../../assets/cap-en-auto/FormDiffractionSimulator.toolStripContainer1.flowLayoutPanel6.groupBoxMonitor.png)

**Resolution**, image **Size (W×H)**, **Set the center to** / **Fix**, and **Horizontal flip / Vertical flip / Negative image** of the pattern. Tick **Reciprocal space** to draw the Ewald sphere and reciprocal-lattice vectors.

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

### Kikuchi lines

![Kikuchi tab](../../assets/cap-en-auto/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageKikuchi.png)

Toggle from the toolbar; choose the reflections by **Structure factor** (Top N) or **1/d Cutoff**, and set the line width and colour.

### Debye rings

![Debye tab](../../assets/cap-en-auto/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageDebye.png)

### Scale

![Scale tab](../../assets/cap-en-auto/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageScale.png)

---

## Spot property

### Wave Length

![Wave Length](../../assets/cap-en-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelWaveLength.png)

X-ray (characteristic/synchrotron), Electron, Neutron. Set energy or wavelength.

### Incident beam mode

![Beam mode](../../assets/cap-en-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelBeamMode.png)

**Parallel beam**, **Precession (electron)** (PED), **Convergence (electron)** (CBED)

### Intensity calculation

![Intensity](../../assets/cap-en-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelIntensity.png)

- Only excitation error
- Kinematical & excitation error
- Dynamical theory (Bloch wave, electron only)

### Appearance

![Appearance](../../assets/cap-en-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelAppearance.png)

Solid sphere or Gaussian. Opacity, radius, brightness, colour scale.

### Bloch wave parameters

![Bethe parameters](../../assets/cap-en-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelBethe.png)

**Number of Diffracted Waves** and **Thickness**.

### PED parameters

![PED parameters](../../assets/cap-en-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelPED.png)

Semi-angle and step.

---

## Detector geometry (detailed)

### Detector geometry settings

![Detector geometry settings](../../assets/cap-en-auto/FormDiffractionSimulatorGeometry.panelDetectorGeometry.png)

### Detector area and overlapped image

![Detector area](../../assets/cap-en-auto/FormDiffractionSimulatorGeometry.panelDetectorAreaAndOverlappedImage.png)

See [Appendix A2. Detector Coordinate System](../appendix/a2-detector-coordinate-system.md).

---

## Diffraction spot information

Lists the per-reflection details computed by the Bethe dynamical theory (Bloch-wave method). Open it with the **Spot Details** button (intensity-calculation panel) or the **Details** check box.

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

- **Unit of potential** — switches the displayed potential between **Vg [eV]** (electrostatic potential, eV) and **Ug [nm⁻²]** (the scaled quantity $U_g = (2 m_0/h^2)\, V_g$ that enters the Bloch-wave equations). The column headers change accordingly between *Vg / V'g* and *Ug / U'g*.
- Above the table, the accelerating voltage, wavelength ($\lambda = 1/k_\text{vac}$), relativistic mass ratio $m/m_0$, speed ratio $v/c$, lattice volume, sample thickness, and (in CBED mode) the maximum semi-angle of the electron beam are shown.
- **Note 1:** the unit of length is **nm**, not Å. **Note 2:** the unit of wavenumber is **1/nm**, not 2π/nm.
- **Effective digit** — number of significant digits shown. **Auto resize row width** — auto-fit column widths. **Copy to clipboard** — exports the table as text that can be pasted into a spreadsheet.
