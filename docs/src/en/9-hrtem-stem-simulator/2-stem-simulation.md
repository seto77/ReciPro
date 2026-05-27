# STEM Simulation

Simulates scanning transmission electron microscopy images using the Bloch-wave method.

---

## Overview

A convergent electron beam is scanned across the specimen. Transmitted and scattered electrons are collected by annular detectors. ReciPro computes both elastic and thermal diffuse scattering (TDS) contributions.

---

## Computational cost

| Factor | Impact |
|--------|--------|
| Convergence angle | Larger → more CBED disk overlap → higher cost |
| Bloch waves | Eigenvalue problem scales as N³ |
| Angular resolution | Finer → more accurate but cost scales as N² |
| Image pixels | Linear scaling |

---

## Detector types

| Detector | Angle range | Main contribution | Contrast |
|----------|-------------|-------------------|----------|
| **BF** | 0 – convergence angle | Elastic | Phase contrast |
| **ABF** | Inner part of convergence angle | Elastic | Light-element sensitive |
| **LAADF** | Just outside convergence angle | Elastic + TDS | Strain sensitive |
| **HAADF** | Well outside convergence angle | TDS (inelastic) | Z-contrast ($\propto Z^2$) |

---

## STEM-specific parameters

![STEM-specific parameters](../../assets/cap-en-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.groupBoxSTEMoption2.png)

| Parameter | Description | Typical |
|-----------|-------------|---------|
| **Convergence angle** | Beam semiangle (mrad) | 15–25 |
| **Detector inner/outer angle** | Annular detector range (mrad) | BF: 0/15, HAADF: 50–80/200 |
| **Effective source size** | FWHM (pm) | 50–100 |
| **Slice thickness** | For TDS calculation (nm) | — |
| **Angular resolution** | Angular step (mrad) | 1–3 |

---

## Display modes

![Display modes](../../assets/cap-en-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxSTEMoption3.png)

**Elastic only** / **TDS only** / **Both**

---

## Temperature factor

For HAADF-STEM, atoms must have a non-zero isotropic temperature factor (Debye-Waller factor). If unknown, set B to about 0.5 Å². Zero B gives zero TDS intensity.

---

## Comparison with Dr. Probe

ReciPro's STEM simulations have been confirmed to agree closely with the widely used Dr. Probe GUI (v1.10). The figure below compares the two for BF, ABF, LAADF and HAADF detectors over a thickness series (2.96–60.05 nm), both aberration-free (left) and with Cs = 0.2 mm, defocus = −25.9 nm (right). The two codes agree across all detector types and thicknesses.

![STEM simulation comparison: Dr. Probe vs ReciPro](../../assets/references/STEM_DrProbe_comparison.png)

A more detailed report is available as a PDF: [Comparison of STEM simulations by Dr. Probe GUI (v1.10) and ReciPro (v4.854)](https://github.com/seto77/ReciPro/files/10976084/ComparisonSTEMsimulations.pdf).

---

## See also

- [8. HRTEM/STEM simulator](index.md)
- [8.1. HRTEM simulation](1-hrtem-simulation.md)
- [8.3. Potential simulation](3-potential-simulation.md)
