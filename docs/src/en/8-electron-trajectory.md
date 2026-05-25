# Electron Trajectory

**Trajectory Simulator** computes electron trajectories inside a sample by the **Monte-Carlo method**: incident electrons undergo elastic and inelastic scattering, and the resulting distributions of backscattered electrons (direction, energy, penetration depth) are accumulated. These distributions also feed the angular/energy/depth weighting used by the [14. EBSD simulation](12-ebsd-simulation.md).

![Electron Trajectory](../assets/cap-en-auto/FormTrajectory.png)

---

## Calculation Conditions

![Calculation Conditions](../assets/cap-en-auto/FormTrajectory.panelCalculationConditions.png)

Beam energy, number of incident electrons, sample/material, and other Monte-Carlo parameters.

---

## Stereonet Options

![Stereonet Options](../assets/cap-en-auto/FormTrajectory.panelDrawingOptions.png)

Display options for the angular distribution drawn on the stereographic projection.

---

## Statistics

![Statistics](../assets/cap-en-auto/FormTrajectory.groupBoxStatistics.png)

Summary of the run (backscatter yield, mean free path, penetration depth, etc.).

---

## BSE direction distribution

![BSE direction distribution](../assets/cap-en-auto/FormTrajectory.groupBoxDirectionDistribution.png)

Angular distribution of the backscattered electrons (the stereonet center corresponds to the surface normal direction).

---

## Profiles

![Profiles](../assets/cap-en-auto/FormTrajectory.flowLayoutPanelProfiles.png)

Depth and energy profiles of the simulated electrons.

### BSE energy distribution at escape

![BSE energy distribution at escape](../assets/cap-en-auto/FormTrajectory.flowLayoutPanelProfiles.groupBoxEnergyDistribution.png)
