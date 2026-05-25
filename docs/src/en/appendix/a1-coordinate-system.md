# Appendix A1. Coordinate System

## Definition of orientation

ReciPro uses a **right-handed coordinate system** with axes defined as:

| Axis | Direction |
|------|-----------|
| **X** | Right of the monitor |
| **Y** | Upward on the monitor |
| **Z** | Vertically in front of the monitor |

The **beam direction** corresponds to the viewing direction (looking at the monitor), which is the **−Z** axis.

Most operations in ReciPro involve only directions (3×3 rotation matrices) and do not require explicit origin positions. However, the **Crystal Diffraction** function requires explicit origin — see [Appendix A2. Detector Coordinate System](a2-detector-coordinate-system.md).

---

## Initial crystal direction

ReciPro defines the initial crystal orientation as follows:

1. The **c-axis** is aligned with the **Z-axis** direction.
2. The **b-axis** lies on the **YZ plane** and is close to the Y-axis.
3. The **a-axis** is determined by the b- and c-axes (right-hand rule).

In other words:
- The direction in front of the monitor corresponds to the **[001]** crystal axis.
- The right direction on the monitor corresponds to the normal of the **(100)** crystal plane.

> **Note**: The c-axis always corresponds to Z, but in some crystal systems the a- and b-axes do not necessarily correspond to X and Y.

---

## Euler angles

ReciPro uses Euler angles **Φ**, **θ**, and **Ψ** to represent crystal orientation.

When all angles are zero, the rotation axes correspond to:
- **Φ** → Z-axis (1st, highest rotation)
- **θ** → X-axis (2nd, middle rotation)
- **Ψ** → Z-axis (3rd, lowest rotation)

The three angles have a **hierarchy**: Φ is the highest rotation, followed by θ, then Ψ. The direction of a lower axis depends on the state of the upper rotations. When Φ = θ = Ψ = 15°, for example, the Φ axis still coincides with Z, but the θ and Ψ axes generally do not align with any of X, Y, or Z.
