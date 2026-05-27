---
title: HRTEM / STEM Simulator
---

# HRTEM / STEM Simulator

The **HRTEM/STEM Simulator** simulates TEM lattice-fringe (HRTEM) images, STEM images, and projected potentials. Click **Simulate** to run.

![HRTEM/STEM Simulator](../../assets/cap-en-auto/FormImageSimulator.png)

---

## Quick Routes by Goal

| Goal | Start from | Reference |
|------|------------|-----------|
| Calculate one HRTEM image | Set **Image mode** to **HRTEM**, then set accelerating voltage and defocus in **TEM conditions** | [HRTEM simulation](1-hrtem-simulation.md), [HRTEM image formation](../appendix/a2-bloch-wave/hrtem.md) |
| Calculate a STEM image | Set **Image mode** to **STEM**, then set convergence angle and detector in **STEM options** | [STEM simulation](2-stem-simulation.md), [STEM calculation](../appendix/a2-bloch-wave/stem.md) |
| View projected potential | Set **Image mode** to **Potential** | [Potential simulation](3-potential-simulation.md) |
| Generate a thickness / defocus series | Configure **Single / Serial** and the image conditions in **HRTEM options** | [HRTEM simulation](1-hrtem-simulation.md) |
| Use HAADF-STEM with TDS | Set non-zero atomic temperature factors and use an LAADF / HAADF detector | [STEM calculation](../appendix/a2-bloch-wave/stem.md) |

---

## Basic Workflow

1. Select the crystal and orientation in the main window, then open this simulator.
2. Choose HRTEM, STEM, or Potential in **Image mode**.
3. Set accelerating voltage, defocus, aberrations, apertures, and STEM convergence settings in **Optical property**.
4. Set thickness, image size, resolution, Bloch-wave count, and partial-coherence model in **Simulation property**.
5. Click **Simulate**, then adjust brightness, normalisation, scale bar, and labels in **Display settings**.

---

## File menu


### Help menu


---

## Image mode / Sample

![Image mode](../../assets/cap-en-auto/FormImageSimulator.splitContainer1.flowLayoutPanelModeSelection.groupBoxImageMode.png){align=left}

HRTEM, Potential, or STEM.<div style="clear: both;"></div>

![Sample](../../assets/cap-en-auto/FormImageSimulator.splitContainer1.flowLayoutPanelModeSelection.groupBoxSampleProperty.png){ align=left style="clear: both" }
Sets the sample thickness.<div style="clear: both;"></div>

## Optical property { style="clear: both" }

### TEM conditions

![TEM conditions](../../assets/cap-en-auto/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxTEMConditions.png)

Acc. voltage, defocus (Scherzer shown).

### Objective aperture (HRTEM option)

![Objective aperture (HRTEM option)](../../assets/cap-en-auto/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxHREMoption1.png)

Cs, Cc, beta, delta-E, PCTF, spatial/temporal coherence envelopes, objective aperture.

### STEM options (optical)

![STEM options (optical)](../../assets/cap-en-auto/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxSTEMoption1.png)

---

## Simulation property

### HRTEM options

![HRTEM options](../../assets/cap-en-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.groupBoxHREMoption2.png)

Max Bloch waves, image pixels/resolution, partial coherence (quasi-coherent / TCC), Single/Serial mode.

### STEM options (simulation)

![STEM options (simulation)](../../assets/cap-en-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.groupBoxSTEMoption2.png)

### Potential options

![Potential option](../../assets/cap-en-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.groupBoxPotentialOption.png)

### Image properties

![Image properties](../../assets/cap-en-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.panelImageProperties.groupBoxImageProperty.png)

### Diffracted waves

![Diffracted waves](../../assets/cap-en-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.panelImageProperties.groupBoxDiffractedWaves.png)

---

## Simulate

![Simulation actions](../../assets/cap-en-auto/FormImageSimulator.splitContainer1.panelSimulationActions.png)

---

## Display settings

### Adjust

![Adjust](../../assets/cap-en-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxAdjust.png)

Min/Max brightness, colour scale, Gaussian blur.

### Normalization

![Normalization](../../assets/cap-en-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxNormalization.png)

### Display

![Display](../../assets/cap-en-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxDisplay.png)

Label (thickness/defocus), scale bar, unit cell overlay.

### STEM image

![STEM image](../../assets/cap-en-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxSTEMoption3.png)

---

## STEM simulation

Computation depends on: convergence angle, Bloch wave count, angular resolution.

| Detector | Contribution |
|----------|-------------|
| BF, ABF | Elastic |
| LAADF, HAADF | Inelastic (TDS) |

> Set temperature factors non-zero for TDS (B = 0.5 Å² if unsure). HAADF intensity $\propto Z^2$.

![STEM simulation comparison: Dr. Probe vs ReciPro](../../assets/references/STEM_DrProbe_comparison.png)

A more detailed report is available as a PDF: [Comparison of STEM simulations by Dr. Probe GUI (v1.10) and ReciPro (v4.854)](https://github.com/seto77/ReciPro/files/10976084/ComparisonSTEMsimulations.pdf). See [8.2. STEM simulation](2-stem-simulation.md) for details.
