<!-- nav -->

[← 7.2. X ray diffraction](7-2-x-ray-diffraction.md)  |  [🏠 Home](../index.md)  |  [7.4 CBED simulation →](7-4-cbed-simulation.md)

# Precession Electron Diffraction (PED) Simulation

Simulates diffraction patterns obtained by precessing the incident electron beam around the optic axis.

---

## Overview

In PED the electron beam traces a cone around the optic axis. The diffraction patterns from all beam directions are integrated, offering several advantages over conventional SAED:

- Dynamical effects are averaged out, yielding intensities closer to kinematical values
- Higher-order Laue zone (HOLZ) reflections become more visible
- Better intensity data for structure determination

---

## Settings

Set the beam mode to **Precession** in **Spot property**. Dynamical theory is enabled automatically.

| Parameter | Description | Typical |
|-----------|-------------|---------|
| **Semi-angle** | Precession half-angle (mrad) | 10–40 |
| **Step** | Number of beam directions sampled | 36–72 |
| **No. of Bloch waves** | Bloch waves for dynamical calculation | 50–200 |
| **Thickness** | Specimen thickness (nm) | — |

---

## Calculation method

1. For each sampled beam direction at precession angle α, run a full Bloch-wave calculation
2. Integrate diffraction patterns over all directions
3. Project the result onto the detector

Computation time scales linearly with **Step**.

---

## SAED vs PED

| Feature | SAED | PED |
|---------|------|-----|
| Beam | Parallel, fixed | Precessing (cone scan) |
| Dynamical effects | Large | Averaged, smaller |
| HOLZ reflections | Weak | Stronger |
| Intensity reliability | May be insufficient for structure analysis | Suitable for structure analysis |
| Computation time | Short | Long |

---

## See also

- [7. Diffraction simulator](7-diffraction-simulator/index.md)
- [7.1. SAED simulation](7-1-saed-simulation.md)
- [7.2. X ray diffraction](7-2-x-ray-diffraction.md)

---

[← 7.2. X ray diffraction](7-2-x-ray-diffraction.md)  |  [🏠 Home](../index.md)  |  [7.4 CBED simulation →](7-4-cbed-simulation.md)
