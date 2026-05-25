# X-ray Diffraction Simulation

Simulates single-crystal X-ray diffraction patterns. Activated by selecting X-ray in the Wave tab of the [diffraction simulator](index.md).

---

## X-ray source setup

### Characteristic X-rays

Select an element and transition line:

| Element | Line | Wavelength (Å) | Energy (keV) |
|---------|------|-----------------|--------------|
| Cu | Kα₁ | 1.5406 | 8.048 |
| Mo | Kα₁ | 0.7107 | 17.479 |
| Co | Kα₁ | 1.7890 | 6.930 |
| Cr | Kα₁ | 2.2910 | 5.415 |

### Synchrotron radiation

Set Element to **0** and enter energy or wavelength directly.

---

## Camera geometries

In addition to a flat detector, the detector-geometry settings provide dedicated X-ray camera modes:

- **Precession camera** — simulates a precession photograph (single reciprocal-lattice layer).
- **Back-Laue camera** — simulates a back-reflection Laue pattern (detector on the source side of the sample).

---

## Differences from electron diffraction

| Feature | X-ray | Electron |
|---------|-------|----------|
| Wavelength | Long (0.5–2.5 Å) | Short (0.02–0.04 Å) |
| Ewald sphere curvature | Large | Small (nearly flat) |
| Simultaneous reflections | Few | Many |
| Scattering factor | Atomic f(s) | Electron fe(s) |
| Dynamical effects | Usually small | Large |
| Extinction rules | Strictly obeyed | May be violated by multiple scattering |

---

## Intensity calculation

Kinematical theory is generally sufficient for X-ray diffraction (dynamical theory is available for electrons only).

---

## Debye rings

Enable **Debye rings** on the toolbar for polycrystalline patterns.

- **Ignore diffraction intensity**: uniform ring colour
- **Show index label**: display indices near rings

---

## See also

- [7. Diffraction simulator](index.md)
- [7.1. SAED simulation](1-saed-simulation.md)
- [7.3. PED Simulation](3-ped-simulation.md)
