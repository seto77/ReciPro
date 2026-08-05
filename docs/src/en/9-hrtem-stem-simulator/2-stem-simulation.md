# STEM Simulation

**STEM (Scanning Transmission Electron Microscopy)** simulation computes scanning transmission electron microscopy images using the Bloch-wave method.

![Simulator in STEM mode](../../assets/cap-en-auto/FormImageSimulator-stem.png)

> This page lists every setting that appears on the right when **Image mode = STEM**. For the result display, brightness, and normalisation controls on the left, see the [overview page](index.md). Only the STEM-specific **display target** is repeated below.

---

## Overview

A convergent electron beam is scanned across the specimen, and the transmitted and scattered electrons at each scan position are collected by annular detectors. ReciPro computes the STEM image with the Bloch-wave method (dynamical calculation).

### Calculation flow

1. At each scan position, compute the diffracted intensities with the Bloch-wave method for every incident direction of the convergent probe.
2. Integrate the scattered intensity over the detector's angular range.
3. Both elastic and thermal-diffuse scattering (TDS) contributions can be computed.

See [Appendix A3.4 — STEM calculation](../appendix/a3-bloch-wave/stem.md) for the theory.

---

## Detector types

| Detector | Angle range | Main contribution | Contrast |
|----------|-------------|-------------------|----------|
| **BF** (bright field) | 0 – convergence angle | Elastic | Phase contrast |
| **ABF** (annular bright field) | Inner part of the convergence angle | Elastic | Light-element sensitive |
| **LAADF** (low-angle annular dark field) | Just outside the convergence angle | Elastic + TDS | Strain sensitive |
| **HAADF** (high-angle annular dark field) | Well outside the convergence angle | TDS (inelastic) | Z-contrast ($\propto Z^2$) |

> **Typical detector settings** (each available with one click from the right-click menu of the STEM options, all with convergence angle α = 25 mrad):
> BF (0–5 mrad) / ABF (12–24 mrad) / LAADF (26–60 mrad) / HAADF (80–250 mrad)

---

## Specimen parameters

![Specimen parameters](../../assets/cap-en-auto/FormImageSimulator.splitContainer1.flowLayoutPanelModeSelection.groupBoxSampleProperty.png)

- **Thickness** : specimen thickness (nm). This value is ignored in **Serial image** mode.

---

## TEM conditions

![TEM conditions](../../assets/cap-en-auto/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxTEMConditions.png)

| Parameter | Description | Default / typical |
|-----------|-------------|-------------------|
| **Acc. Vol. (kV)** | Accelerating voltage. The relativistically corrected electron wavelength is shown alongside | 200 kV |
| **Defocus Δf** | Defocus of the objective (probe-forming) lens (nm) | −57.8 nm |
| **Cs** | Spherical aberration coefficient (mm). Affects the probe size | 0.5–1.0 mm |
| **Cc** | Chromatic aberration coefficient (mm) | 1.0–2.0 mm |
| **ΔV (FWHM)** | Full width at half maximum of the electron energy spread (eV) | 0.5–2.0 eV |

> **β (illumination semi-angle) is disabled in STEM mode**, because the convergence angle α takes its role.

---

## STEM options (optical)

![STEM options (optical)](../../assets/cap-en-auto/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxSTEMoption1.png)

Set the geometry of the convergent probe and the annular detector. Each angle is also shown converted to a reciprocal-space radius $\sin\theta/\lambda$ (nm⁻¹) on the right.

| Parameter | Description | Default / typical |
|-----------|-------------|-------------------|
| **α (convergence angle)** | Semi-angle of the convergent probe (mrad). Larger values give a finer probe and change the diffraction contrast | 15–25 mrad |
| **(Annular) detector inner angle** | Inner collection semi-angle of the annular detector (mrad). Signal inside this angle is excluded | BF: 0, HAADF: 80 |
| **(Annular) detector outer angle** | Outer collection semi-angle of the annular detector (mrad). Signal outside this angle is excluded | BF: 5, HAADF: 250 |
| **Effective source size σs (FWHM)** | Effective electron source size. Larger values blur the probe and reduce fine-detail contrast | — |

---

## STEM options (simulation)

![STEM options (simulation)](../../assets/cap-en-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.groupBoxSTEMoption2.png)

- **Slice thickness for inelastic** : specimen slice thickness (nm) used when computing the TDS (thermal-diffuse, inelastic) intensity. Smaller values are more accurate but slower.
- **Angular resolution** : angular sampling resolution of the incident probe directions (mrad). Smaller values sample the probe more finely but are slower. The number of directions grows as the square of this ratio, so it is the main lever on calculation time; see [Angular sampling of the probe](../appendix/a3-bloch-wave/stem.md#angular-sampling) for measured convergence.

---

## Image mode (single / serial)

![Image mode](../../assets/cap-en-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.groupBoxSerialImage.png)

- **Single image** : compute one STEM image at the current thickness.
- **Serial image** : generate a series of images with thickness / defocus stepped in stages (set by **Start / Step / Num**; the list below can also be edited directly).

---

## Image property

![Image property](../../assets/cap-en-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.panelImageProperties.groupBoxImageProperty.png)

- **Size (W×H)** : number of pixels in the scanned image (default 512×512). In STEM this equals the number of scan points and scales the computation time linearly.
- **Resolution** : sampling resolution (pm/px).

---

## Waves

![Waves](../../assets/cap-en-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.panelImageProperties.groupBoxDiffractedWaves.png)

- **Max Bloch waves** : maximum number of Bloch waves used in the Bethe method (default 80). The eigenvalue-problem cost scales as the cube of the number of waves.

---

## STEM display target (result side) {#stem-display-target}

![STEM image](../../assets/cap-en-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxSTEMoption3.png)

The display switch at the bottom-left of the window selects which scattering component of the already-computed STEM image to show (switchable without recomputing).

| Display target | Description |
|----------------|-------------|
| **Elastic** | Elastic-scattering only image |
| **TDS** | Thermal-diffuse-scattering only image |
| **Elastic & TDS** | Sum of elastic + TDS |
| **EDX** | Characteristic X-ray map. The line to show (for example `O-K`) is chosen in the combo box below, and **EDX: Common** in *Normalization* puts every channel on one shared display range so switching channel does not rescale the image |

!!! note
    All three images are reconstructed from the real part of the Fourier sum, so **Elastic & TDS** is exactly the sum of the other two. Versions up to 4.944 took the magnitude instead, which broke that identity and slightly brightened the dark pixels. See [Reconstructing a real image](../appendix/a3-bloch-wave/stem.md#real-image-reconstruction).

---

## STEM-EDX elemental maps {#stem-edx}

![STEM-EDX elemental maps](../../assets/cap-en-auto/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxSTEMoption1.groupBoxSTEMoption4.png)

Tick **Calculate EDX maps** to compute characteristic X-ray maps alongside the ADF-type image. This is not a separate mode: the elastic, TDS and EDX signals all come out of the same STEM run, and you switch between them afterwards in [STEM image](#stem-display-target) without recomputing.

There is no element selector. When the checkbox is on, **every element/shell channel that can be calculated for this crystal at this accelerating voltage** is computed, and the line under the checkbox lists them (for example `3 map(s): O-K, Mg-K, Al-K`). A channel is available when the ionisation edge lies below the accelerating voltage and the shell is covered by the shipped data — K: C–Sn (Z = 6–50), L-total: Ca–Rn (Z = 20–86). The shipped table stores fully relativistic ionisation form factors out to a scattering vector of 8 Å⁻¹ for every channel, so heavy-element L lines up to radon are simulated without extrapolation. If nothing is available the run is refused with an explanatory message rather than producing an empty map.

The next line reports the probe direction grid, for example `Grid: 132² (recommended: ≥48²)`. This grid is set by **Angular resolution** and the convergence angle; see [Angular sampling of the probe](../appendix/a3-bloch-wave/stem.md#angular-sampling). Below the recommended division the ±q Hermitian residual can exceed the tolerance and abort the run, so the value turns orange and a confirmation dialog appears before the calculation starts.

!!! warning "What the values are"
    The map is the **number of inner-shell vacancies generated per incident electron** — a model quantity, not a predicted X-ray count. Fluorescence yield, self-absorption in the specimen, detector solid angle and detector efficiency are **not** applied. Use the maps for spatial distribution and for comparing thickness or orientation, not for absolute quantification.

### Detector parameters (reserved)

**Self-absorption**, **Take-off angle** and **Detector** are laid out but disabled: they belong to the detector model that is not implemented yet. They are shown so that the panel does not move when the model lands. Their eventual effect differs in kind:

| Factor | Pixel-to-pixel contrast in one map | Ratio between element maps |
|---|---|---|
| Self-absorption (take-off angle) | **changes it** | **changes it** |
| Detector window / dead layer / efficiency | no effect | **changes it strongly** |
| Detector solid angle, beam current, dwell time | no effect | no effect |

The last row is why ReciPro does not expose beam current or dwell time at all: they multiply every pixel of every map by the same number, cancel in any ratio, and are invisible after the display normalisation.

### Accuracy and cost

STEM-EDX places no extra limit on the wave count or the slice thickness: it runs through the same calculation paths as the ADF-type image, so whatever settings work for STEM work for EDX too.

Accuracy is left to you, exactly as it is for the wave count or the angular resolution. For reference, the depth-integration error grows roughly in proportion to **Slice thickness (TDS)** — about 2–3 % at 1 nm, 4–8 % at 2 nm and 12–23 % at 4 nm (peak-relative, SrTiO₃ at 39 nm). Halving the slice thickness roughly halves the error and roughly doubles the depth-integration work.

With aberrations set (for example Cs = 1 mm with Scherzer defocus at α = 25 mrad), the aberration phase oscillates quickly across the probe direction grid, and STEM-EDX may refuse to run with a *non-Hermitian residual* error even at a fine grid — the refusal protects the map from grid artefacts of a few per cent. Reduce Cs and defocus (the scan average of an EDX map does not depend on the aberrations at all), or make **Angular resolution** substantially finer and accept the longer run.

---

## Computational cost

STEM simulation is computationally expensive, so set the following parameters appropriately.

| Factor | Impact |
|--------|--------|
| **Convergence angle** | Larger → more CBED disk overlap → higher cost |
| **Bloch waves** | Eigenvalue-problem cost scales as N³ |
| **Angular resolution** | Finer → more accurate but cost scales as N² |
| **Image pixels (Size)** | Linear scaling with the number of scan points |

---

## Importance of the temperature factor

For HAADF-STEM simulation, atoms must have a non-zero isotropic temperature factor (Debye-Waller factor). If the value is unknown, set $B \approx 0.5\ \text{Å}^2$. With a zero temperature factor the TDS intensity is zero and the HAADF image is not computed correctly.

| Detector | Range | Main contribution |
|----------|-------|-------------------|
| BF, ABF | Inside the convergence angle | Elastic |
| LAADF, HAADF | Outside the convergence angle | Inelastic (TDS) |

---

## Comparison with Dr. Probe

ReciPro's STEM simulations have been confirmed to agree closely with the widely used Dr. Probe GUI (v1.10). The figure below compares the two for BF, ABF, LAADF and HAADF detectors over a thickness series (2.96–60.05 nm), both aberration-free (left) and with Cs = 0.2 mm, defocus = −25.9 nm (right). The two codes agree across all detector types and thicknesses.

![STEM simulation comparison: Dr. Probe vs ReciPro](../../assets/references/STEM_DrProbe_comparison.png)

A more detailed report is available as a PDF: [Comparison of STEM simulations by Dr. Probe GUI (v1.10) and ReciPro (v4.854)](https://github.com/seto77/ReciPro/files/10976084/ComparisonSTEMsimulations.pdf).

---

## See also

- [HRTEM/STEM simulator (overview)](index.md)
- [HRTEM simulation](1-hrtem-simulation.md)
- [Potential simulation](3-potential-simulation.md)
- [Appendix A3.4 — STEM calculation](../appendix/a3-bloch-wave/stem.md)
