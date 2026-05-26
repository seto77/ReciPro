# SAED Simulation

**SAED (Selected Area Electron Diffraction)** simulation calculates electron diffraction patterns with a parallel beam. This is the default mode of the [diffraction simulator](index.md).

---

## Overview

Simulates the diffraction pattern when a parallel electron beam passes through a thin specimen. Spot positions and intensities are calculated from the geometrical relationship between the Ewald sphere and reciprocal lattice points.

---

## Intensity calculation modes

### Only excitation error

Fastest mode. Intensity is estimated solely from the distance (excitation error *s*) between each reciprocal lattice point and the Ewald sphere. Structure factors are not considered.

### Kinematical & excitation error

Includes the crystal structure factor |*F*(**g**)|.

- Intensity ∝ |*F*(**g**)|²
- Extinction rules are correctly reflected
- Suitable for thin specimens or weak diffraction

### Dynamical theory

Rigorous Bloch-wave (Bethe) method. Electron diffraction only.

- Accurate multi-beam scattering
- Thickness-dependent intensity
- **No. of Bloch waves**: 50–200 for routine work; 500+ for high accuracy
- **Thickness**: specimen thickness in nm

---

## Spot appearance

### Solid sphere

Reciprocal lattice points are spheres of radius *R*. The cross-section with the Ewald sphere is drawn as a circle. Points far from the Ewald sphere are hidden.

### Gaussian

Points are 3D Gaussians (σ = *R*). The cross-section appears as a 2D Gaussian. All points are visible with intensity-dependent brightness.

---

## Parameters

| Parameter | Description |
|-----------|-------------|
| **Opacity** | Spot transparency (0–1) |
| **Radius** | Virtual radius *R* of reciprocal lattice points |
| **Brightness** | Brightness correction for Gaussian mode |
| **Colour scale** | Greyscale or Cold-Warm |
| **Log scale** | Logarithmic intensity display |
| **Spot colour** | Spot drawing colour |

---

## Spot labels

Select from the toolbar:

| Label | Content |
|-------|---------|
| **Index** | Miller indices (*hkl*) |
| **d** | *d*-spacing (Å) |
| **Distance** | Spot-to-spot distance on detector |
| **Excit. Err** | Excitation error *s* |
| **\|Fg\|** | Absolute structure factor |

---

## See also

- [7. Diffraction simulator](index.md)
- [Parallel-beam SAED calculation](../appendix/a2-bloch-wave/calculation.md#parallel-beam-saed)
- [7.2. X ray diffraction](2-x-ray-diffraction.md)
- [7.3. PED Simulation](3-ped-simulation.md)
