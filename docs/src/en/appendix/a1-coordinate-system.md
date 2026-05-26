# Appendix A1. Coordinate Systems

ReciPro uses two related but distinct coordinate conventions:

1. The **orientation coordinate system**, used everywhere crystal rotation is involved (Main window, Structure Viewer, Stereonet, Rotation Geometry, and diffraction simulation).
2. The **detector coordinate system**, used by the **Crystal Diffraction** function to place a finite — and possibly tilted — detector relative to the sample.

This appendix defines both.

---

## Orientation coordinate system

### Definition of orientation

ReciPro uses a **right-handed coordinate system** fixed to the monitor:

| Axis | Direction |
|------|-----------|
| **X** | Right of the monitor |
| **Y** | Upward on the monitor |
| **Z** | Vertically out of the monitor, toward the viewer |

![ReciPro coordinate axes shown on the monitor](../../assets/references/Coordinates1.png)

The **beam direction** corresponds to the viewing direction (looking into the monitor), i.e. the **−Z** axis.

Most operations in ReciPro involve only *directions* (expressed as 3×3 rotation matrices) and do not require an explicit origin. The one exception is the **Crystal Diffraction** function, which needs an explicit origin — see [Detector coordinate system](#detector) below.

### Initial crystal direction

The initial orientation (at first launch, or after **Reset rotation**) is defined as:

1. The **c-axis** is aligned with the **Z-axis**.
2. The **b-axis** lies in the **YZ plane**, close to the Y-axis.
3. The **a-axis** is then fixed by the b- and c-axes (right-hand rule).

![Initial orientation: the crystal a / b / c axes relative to X / Y / Z, with the incident beam along −Z](../../assets/references/Coordinates2.png)

Equivalently:

- The direction out of the monitor (toward the viewer) is the **[001]** zone axis.
- The rightward direction on the monitor is the normal of the **(100)** plane.

> **Note:** The c-axis (= [001]) always coincides with Z, but in some crystal systems the a- and b-axes do **not** necessarily coincide with X and Y.

### Euler angles

Crystal orientation is expressed with three Euler angles **Φ**, **θ**, **Ψ**, applied in **Z–X–Z** order (Ψ, then θ, then Φ). When all three angles are zero, the corresponding rotation axes are:

| Angle | Axis (when all angles = 0) | Rank |
|-------|----------------------------|------|
| **Φ** | Z | 1st (highest) |
| **θ** | X | 2nd (middle) |
| **Ψ** | Z | 3rd (lowest) |

![Euler-angle rotation axes — Φ (yellow), θ (cyan), Ψ (magenta) — shown at 0° (top) and at 15° (bottom)](../../assets/references/Coordinates3.png)

The three angles form a **hierarchy**: Φ is the highest rotation, followed by θ, then Ψ. The direction of a lower axis depends on the state of the higher rotations. For example, when Φ = θ = Ψ = 15°, the Φ axis still coincides with Z, but the θ and Ψ axes generally align with none of X, Y, or Z.

> The **Rotation Geometry** window can re-express this orientation in an arbitrary, experiment-specific Euler-angle convention (e.g. to match a laboratory goniometer). See [4. Rotation Geometry](../4-rotation-geometry.md).

---

## Detector coordinate system (Crystal Diffraction) {#detector}

The **Crystal Diffraction** function simulates the diffraction pattern recorded on a detector. The detector is a finite plane of pixels placed at a fixed distance from the sample, and it may be tilted with respect to the incident beam. Reproducing this accurately requires the geometric relationship between the detector and the sample, together with the detector's pixel size and pixel count.

!!! note "Z and Y differ from the orientation system"
    Within the detector coordinate system, **Z is parallel to the beam** and **Y points downward**. This differs from the orientation coordinate system above, where the beam is along **−Z** and **Y points up**. The detector system follows the usual image/detector convention (origin at the top-left, Y increasing downward).

### Before rotation (detector normal to the beam)

![Detector coordinate system with the detector normal to the beam](../../assets/references/Coordinates4.png)

Three coordinate systems are defined:

- **Real coordinates (X, Y, Z)** — 3D Cartesian coordinates in mm, with the **sample** as origin. Z is parallel to the beam; viewed along Z, X points right and Y points down. When the detector is normal to the beam, X / Y are parallel to X′ / Y′.
- **Detector coordinates (X′, Y′)** — 2D coordinates in mm on the detector plane, with the **foot** as origin. X′ / Y′ point right / down on the detector and are parallel to X″ / Y″.
- **Pixel coordinates (X″, Y″)** — 2D coordinates in pixel units, with the **upper-left corner** of the detector as origin, following the detector's pixel rows and columns.

When the detector is perpendicular to the beam, the **foot** and the **direct spot** coincide, and Camera length 1 equals Camera length 2.

### After rotation (tilted detector)

![Detector coordinate system with a tilted detector](../../assets/references/Coordinates5.png)

The detector tilt is described by two parameters:

| Parameter | Description |
|-----------|-------------|
| **φ** | Direction of the rotation axis — its angle from the X-axis, measured in the XY (Z = 0) plane |
| **τ** | Rotation angle about that axis (right-hand screw) |

Once the detector is tilted:

- The **direct spot** and the **foot** no longer coincide.
- **Camera length 1 (C1)** = distance from the sample to the direct spot.
- **Camera length 2 (C2)** = distance from the sample to the foot.
- The origin of **Detector coordinates** stays at the **foot**; the origin of **Pixel coordinates** stays at the **upper-left corner**.
- The X / Y directions no longer coincide with X′ / Y′.

### Parameter glossary

| Term | Definition |
|------|------------|
| **Sample** | The material scattering the incident beam; the origin of the real coordinates |
| **Real coordinates (X, Y, Z)** | 3D coordinates (mm) of the experimental setup; origin at the sample, Z always parallel to the beam |
| **Direct spot** | Intersection of the incident beam and the detector |
| **Foot** | The foot of the perpendicular from the sample to the detector plane; origin of the detector coordinates. Coincides with the direct spot only when the detector is normal to the beam. For overlay-image mode, set the foot position in pixel coordinates |
| **Detector coordinates (X′, Y′)** | 2D coordinates (mm) on the detector plane; origin at the foot |
| **Pixel coordinates (X″, Y″)** | 2D coordinates (pixels) on the detector plane; origin at the upper-left corner |
| **Camera length 1 (C1)** | Distance from the sample to the direct spot (mm) |
| **Camera length 2 (C2)** | Distance from the sample to the foot (mm) |
| **Pixel size** | Side length of one (square) pixel (mm); only square pixels are supported |
| **Detector width / height** | Number of pixels horizontally / vertically |
