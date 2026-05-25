<!-- nav -->

[← 8. Electron trajectory](13-electron-trajectory.md)  |  [🏠 Home](../index.md)  |  [9.1. HRTEM simulation →](8-1-hrtem-simulation.md)

# HRTEM / STEM Simulator

The **HRTEM/STEM Simulator** simulates TEM lattice-fringe (HRTEM) images, STEM images, and projected potentials. Click **Simulate** to run.

![HRTEM/STEM Simulator](../assets/cap-en-auto/FormImageSimulator.png)

---

## File menu


### Help menu


---

## Image mode / Sample property

![Image mode](../assets/cap-en-auto/FormImageSimulator.splitContainer1.flowLayoutPanelModeSelection.groupBoxImageMode.png)

HRTEM, Potential, or STEM.

![Sample property](../assets/cap-en-auto/FormImageSimulator.splitContainer1.flowLayoutPanelModeSelection.groupBoxSampleProperty.png)

---

## Optical property

### TEM conditions

![TEM conditions](../assets/cap-en-auto/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxTEMConditions.png)

Acc. voltage, defocus (Scherzer shown).

### HRTEM option (Inherent / Lens function / Aperture)

![HRTEM option 1](../assets/cap-en-auto/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxHREMoption1.png)

Cs, Cc, beta, delta-E, PCTF, spatial/temporal coherence envelopes, objective aperture.

### STEM option (Convergence / Detector)

![STEM option 1](../assets/cap-en-auto/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxSTEMoption1.png)

---

## Simulation property

### HRTEM simulation options

![HRTEM option 2](../assets/cap-en-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.groupBoxHREMoption2.png)

Max Bloch waves, image pixels/resolution, partial coherence (quasi-coherent / TCC), Single/Serial mode.

### STEM simulation options

![STEM option 2](../assets/cap-en-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.groupBoxSTEMoption2.png)

### Potential options

![Potential option](../assets/cap-en-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.groupBoxPotentialOption.png)

### Image property

![Image property](../assets/cap-en-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.panelImageProperties.groupBoxImageProperty.png)

### Diffracted waves

![Diffracted waves](../assets/cap-en-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.panelImageProperties.groupBoxDiffractedWaves.png)

---

## Simulate

![Simulation actions](../assets/cap-en-auto/FormImageSimulator.splitContainer1.panelSimulationActions.png)

---

## Display settings

### Adjust

![Adjust](../assets/cap-en-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxAdjust.png)

Min/Max brightness, colour scale, Gaussian blur.

### Normalization

![Normalization](../assets/cap-en-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxNormalization.png)

### Display options

![Display options](../assets/cap-en-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxDisplay.png)

Label (thickness/defocus), scale bar, unit cell overlay.

### STEM display option

![STEM option 3](../assets/cap-en-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxSTEMoption3.png)

---

## STEM simulation

Computation depends on: convergence angle, Bloch wave count, angular resolution.

| Detector | Contribution |
|----------|-------------|
| BF, ABF | Elastic |
| LAADF, HAADF | Inelastic (TDS) |

> Set temperature factors non-zero for TDS (B = 0.5 A^2 if unsure). HAADF intensity ~ Z^2.

![STEM simulation comparison: Dr. Probe vs ReciPro](../assets/references/STEM_DrProbe_comparison.png)

A more detailed report is available as a PDF: [Comparison of STEM simulations by Dr. Probe GUI (v1.10) and ReciPro (v4.854)](https://github.com/seto77/ReciPro/files/10976084/ComparisonSTEMsimulations.pdf). See [8.2. STEM simulation](8-2-stem-simulation.md) for details.

---

[← 8. Electron trajectory](13-electron-trajectory.md)  |  [🏠 Home](../index.md)  |  [9.1. HRTEM simulation →](8-1-hrtem-simulation.md)
