<!-- nav -->

[← 9. HRTEM/STEM simulator](8-hrtem-stem-simulator/index.md)  |  [🏠 Home](../index.md)  |  [9.2. STEM simulation →](8-2-stem-simulation.md)

# HRTEM Simulation

Simulates high-resolution TEM lattice-fringe images. The primary mode of the [8. HRTEM/STEM simulator](8-hrtem-stem-simulator/index.md).

---

## Calculation flow

1. **Bloch-wave method**: compute electron wave propagation through the crystal potential; obtain exit-wave amplitude and phase
2. **Lens function**: apply objective-lens aberrations (Cs, defocus Δf)
3. **Partial coherence**: account for finite source size (spatial coherence) and energy spread (temporal coherence)
4. **Image formation**: compute intensity |ψ(r)|²

---

## Specimen parameters

| Parameter | Description |
|-----------|-------------|
| **Thickness** | Specimen thickness (nm). HRTEM images are strongly thickness-dependent |

---

## Optical parameters

### TEM conditions

| Parameter | Description |
|-----------|-------------|
| **Acc. Vol.** | Accelerating voltage (kV). Relativistically corrected wavelength shown alongside |
| **Defocus** | Defocus value (nm). Scherzer defocus displayed as reference |

### Intrinsic parameters

| Parameter | Description | Typical |
|-----------|-------------|---------|
| **Cs** | Spherical aberration (mm) | 0.5–1.0 (conventional); < 0.01 (Cs-corrected) |
| **Cc** | Chromatic aberration (mm) | 1.0–2.0 |
| **β** | Illumination semiangle (mrad) | 0.1–1.0 |
| **ΔE** | Energy spread 1/*e* width (eV) | 0.5–2.0 |

---

## Phase Contrast Transfer Function (PCTF)

Displayed in the lens-function tab:

- **Sin[χ(u)]**: phase contrast transfer function
- **Es(u)**: spatial coherence envelope
- **Ec(u)**: temporal coherence envelope

Scherzer defocus: Δf = −1.2 (Cs λ)^(1/2), the condition giving a broad negative PCTF band (dark contrast = atom positions).

---

## Objective aperture

Set aperture size (mrad) and position. **Open aperture** removes it. The number of Bloch waves considered depends on aperture conditions.

---

## Partial coherence models

| Model | Description |
|-------|-------------|
| **Quasi-coherent (linear image)** | Fast. Valid under the weak-phase approximation |
| **TCC (Transmission Cross Coefficient)** | More accurate; longer computation |

---

## Simulation modes

| Mode | Description |
|------|-------------|
| **Single image** | One image at current thickness and defocus |
| **Serial image** | Matrix of images over thickness × defocus ranges (Start / Step / Num) |

---

## See also

- [8. HRTEM/STEM simulator](8-hrtem-stem-simulator/index.md)
- [8.2. STEM simulation](8-2-stem-simulation.md)
- [8.3. Potential simulation](8-3-potential-simulation.md)

---

[← 9. HRTEM/STEM simulator](8-hrtem-stem-simulator/index.md)  |  [🏠 Home](../index.md)  |  [9.2. STEM simulation →](8-2-stem-simulation.md)
