# X-ray / Neutron Diffraction Simulation

**X-ray / neutron diffraction simulation** computes single-crystal X-ray and neutron diffraction patterns. It is one of the main modes of the [diffraction simulator](index.md).

> This page lists every setting that appears on the right-hand side when you select **Wave Length = X-ray** (or neutron). For window-wide operations such as drawing and saving, see the [overview page](index.md).

GUI conditions: Wave Length = X-ray / Neutron · Incident beam = Parallel / Precession (X-ray) / Back-Laue · Intensity calculation = Only excitation error / Kinematical

![Diffraction simulator in X-ray / neutron mode](../../assets/cap-en-auto/FormDiffractionSimulator-xray.png)

---

## Overview

X-rays have a longer wavelength than electrons (Cu Kα: 0.15406 nm = 1.5406 Å), so the Ewald sphere is more strongly curved. As a result, fewer reciprocal-lattice points satisfy the diffraction condition simultaneously than for electrons. Because the atomic scattering power is small and multiple scattering is weak, the Kinematical theory of diffraction gives sufficient accuracy for the intensities (the Dynamical calculation is supported for electrons only).

---

## Wave Length

![Wave Length](../../assets/cap-en-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelWaveLength.png)

Select **X-ray** as the radiation source. X-rays can be specified in two ways: characteristic X-rays and synchrotron radiation.

### Characteristic X-rays

Choosing an **element** and a **transition** fixes the characteristic X-ray wavelength. The transition is specified in Siegbahn notation (Kα₁ / Kα₂ / Kβ, etc.). Kα₁ wavelengths of representative elements:

<!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: Crystallography/Atom/AtomStatic.cs:CharacteristicXrayWavelength (Mo Kα1 0.70931715 Å, Cr Kα1 2.2897260 Å)) -->
| Element | Line | Wavelength (Å) | Energy (keV) |
|---------|------|-----------------|--------------|
| Cu | Kα₁ | 1.5406 | 8.048 |
| Mo | Kα₁ | 0.7093 | 17.479 |
| Co | Kα₁ | 1.7890 | 6.930 |
| Cr | Kα₁ | 2.2897 | 5.415 |

### Synchrotron radiation

Set **Element** to **0: Custom** and enter the energy (keV) or wavelength (Å) directly. Any wavelength can be used.

---

## Incident beam mode

![Beam mode](../../assets/cap-en-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelBeamMode.png)

Selects the geometry of the incident beam. Three modes are available for X-rays.

### Parallel

The standard plane wave. A parallel incident beam used for SAED and single-crystal X-ray diffraction.

### Precession (X-ray) — precession camera

Simulates an X-ray precession camera. This is a precession photograph that captures a single layer of the reciprocal lattice.

### Back-Laue (back-reflection Laue)

<!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormDiffractionSimulator.cs:radioButtonBeamParallel_CheckedChanged / SetVector / Draw (BackLaue region)) -->
Simulates a back-reflection Laue pattern with white (polychromatic) X-rays. In this back-reflection geometry the detector is placed on the source side, and **Monochrome** is turned off. The spots are projected onto a flat detector **perpendicular to the beam** at the camera length on the source side; the detector tilt **Tau / Phi** of [Detector geometry](index.md#detector-geometry) is not used in this projection.

- The white spectrum is cut at **50 keV**: reflections are collected down to $d = \lambda(50\ \text{keV})/2$, and no spectral weighting is applied.
- Harmonics $n\mathbf{g}$ lying on the same line through the origin fall on the same spot; their $\lvert F\rvert^2$ are summed onto the lowest-order reflection and the spot intensities are normalised to the strongest spot.
- **Anomalous dispersion (f', f'')** is disabled (the energy is not single-valued), and **Spot radius** has no unit in this mode.

> **Note**: The incident-beam options follow the wavelength. **Precession (electron)** and **Convergence (CBED)** appear only when electron radiation is selected, whereas the **Precession (X-ray)** and **Back-Laue** options above appear only when X-ray radiation is selected. For neutrons, only **Parallel** is available. Depending on the state at capture time, the screenshot may not show the X-ray-specific options.

---

## Intensity calculation

![Intensity](../../assets/cap-en-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelIntensity.png)

Selects the method used to compute spot intensities. Two modes are available for X-rays.

### Only excitation error

Intensity is determined solely by the geometric distance between the Ewald sphere and the reciprocal-lattice point (the excitation error $S_g$). It is largest at $S_g = 0$ — exactly on the sphere — and falls off as $\lvert S_g\rvert$ grows, reaching zero once $\lvert S_g\rvert$ exceeds the **Radius** given for the reciprocal-lattice point. The structure factor is ignored.

<!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormDiffractionSimulator.cs:radioButtonIntensityCalculationMethod_CheckedChanged / SetVector) -->
Only in this mode, the following options appear:

- **Exclude all prohibited reflections** / **Exclude reflections prohibited by lattice centring** (default: lattice-centring only) : hide reflections forbidden by the space group. Reflections that are not hidden are drawn in the separate colours set under **Spot color** (screw/glide-forbidden and lattice-forbidden).
- **Draw all spots with the same radius** : draw every spot at $S_g = 0$ size regardless of its excitation error.

### Kinematical & exc. error

In addition to the excitation error, the Kinematical structure factor $\lvert F_{hkl} \rvert^2$ is folded into the intensity. Extinction rules are strictly obeyed. The Lorentz and polarization factors are not included (this is a simulation of the geometric pattern).

<!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormDiffractionSimulator.cs:WaveLengthControl_WaveSourceChanged / SetVector, Crystal.cs:SetVectorOfG) -->
- **Anomalous dispersion (f', f'')** (X-ray only, default on) : adds the anomalous-dispersion terms f′, f″ at the current X-ray energy to the structure factor (also to the Debye rings). In a non-centrosymmetric structure containing chemically distinct anomalous scatterers, Friedel's law then breaks and $hkl$ / $\bar h\bar k\bar l$ intensities differ (Bijvoet pairs); the effect is strongest near an absorption edge. Disabled for Back-Laue. See [Structure factor](../appendix/a2-beam-interaction/structure-factor.md).

> **Note**: **Dynamical theory** is disabled for X-rays (available only when electron radiation is selected).

---

## Spot appearance

![Appearance](../../assets/cap-en-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelAppearance.png)

Controls how each diffraction spot is rendered.

<!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormDiffractionSimulator.cs:Draw (円で塗りつぶすとき / 点広がり関数で描画するとき)) -->
- **Solid sphere / Gaussian** : geometric model of the reciprocal-lattice point. **Solid sphere** draws the cross-section between a sphere and the Ewald sphere; with **Kinematical & exc. error** the sphere radius is $R \times I_\text{rel}^{1/3}$ (so the sphere *volume* is proportional to the relative intensity), with **Only excitation error** it is *R* itself. **Gaussian** uses the cross-section between a 3-D Gaussian with σ = *R* and the Ewald sphere (the integral of the 2-D Gaussian corresponds to the diffraction intensity); spots with $\lvert S_g\rvert \ge 3R$ are not drawn.
- **Opacity** : transparency of the spot (0 = transparent, 1 = opaque).
- **Spot radius (R)** : radius of the reciprocal-lattice point. The rendered spot size is determined by the combination of **Appearance** and **Intensity calculation**.
- **Brightness** : active only in **Gaussian** mode. Sets the integrated intensity of the rendered Gaussian. <!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormDiffractionSimulator.Designer.cs:comboBoxScaleColorScale / FormDiffractionSimulator.cs:comboBoxScaleColorScale_SelectedIndexChanged, radioButtonPointSpread_CheckedChanged) -->
- **Color scale** (Gaussian only) : **Gray**, **Cold-Warm**, **Spectrum** or **Fire**.
- **Log scale** (Gaussian only) : display intensities on a logarithmic scale.
- **Spot color** : spot colours used with the **Gray** colour scale (this group is hidden for the other colour scales).
- **Color per crystal** : when checked, draws spots in the colour assigned to each crystal.

---

## Debye rings (polycrystalline)

Debye rings of a polycrystalline specimen can be displayed. Enable **Debye rings** on the toolbar (see [Toolbar](index.md#toolbar)).

- **Ignore diffraction intensity** : draws all rings in the same colour and intensity (used for a purely geometric comparison that ignores the structure factor).
- **Show Index Labels** : displays the (*hkl*) index near each ring.

Detailed settings are on the Debye rings tab of the [tab menu](index.md#drawing-overlay-tabs).

---

## Neutron diffraction

Selecting **Neutron** in the Wave Length control computes a neutron diffraction pattern. <!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: Crystallography.Controls/Wave/WaveLengthControl.cs:radioButtonWaveSource_CheckedChanged / LengthUnit) -->
Enter the energy (meV) or wavelength (Å or nm, switchable; default Å). The incident beam can only be **Parallel**. The intensity calculation can be **Only excitation error** or **Kinematical & exc. error** (**Dynamical theory** is not available). The kinematical intensity uses the neutron scattering length instead of the atomic scattering factor.

---

## Differences between X-ray and electron diffraction

| Feature | X-ray diffraction | Electron diffraction |
|---------|-------------------|----------------------|
| Wavelength | Long (0.5–2.5 Å) | Short (0.02–0.04 Å) |
| Ewald sphere curvature | Large | Small (nearly flat) |
| Simultaneous reflections | Few | Many |
| Scattering factor | Atomic scattering factor $f(s)$ | Electron scattering factor $f_e(s)$ |
| Dynamical effects | Usually small | Large |
| Extinction rules | Strictly obeyed | May be violated by multiple scattering |

---

## Common operations

For window-wide operations such as camera length, detector geometry, saving patterns, and colour settings, see the [overview page](index.md). Detailed detector geometry is configured in the geometry window below.

![Detector geometry settings](../../assets/cap-en-auto/FormDiffractionSimulatorGeometry.png)

---

## See also

- [Diffraction simulator (overview)](index.md)
- [SAED simulation](1-saed-simulation.md)
- [Precession electron diffraction (PED) simulation](2-ped-simulation.md)
- [Convergent-beam electron diffraction (CBED) simulation](3-cbed-simulation.md)
- [Coordinate system — crystal orientation](../appendix/a1-coordinate-system/1-orientation.md)
