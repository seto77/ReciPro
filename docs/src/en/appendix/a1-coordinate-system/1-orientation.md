<!-- 260526Cl: 図(Coordinates1-3)に合わせ座標記号を数式(MathJax)化・用語色を図に一致 (.rp-*: docs/src/assets/stylesheets/extra.css)。 -->
# Appendix A1.1. Basic coordinate system and crystal orientation

This page defines ReciPro's **basic (orientation) coordinate system**, used everywhere crystal rotation is involved (Main window, Structure Viewer, Stereonet, Rotation Geometry, and diffraction simulation), together with how a crystal's initial orientation and Euler-angle rotation are expressed. The separate system used to place the detector in **Crystal Diffraction** is described in [A1.2. Coordinate system for diffraction simulation](2-diffraction.md).

---

## Definition of orientation

ReciPro uses a **right-handed coordinate system** fixed to the monitor:

| Axis | Direction |
|------|-----------|
| <span class="rp-red">$X$</span> | Right of the monitor |
| <span class="rp-green">$Y$</span> | Upward on the monitor |
| <span class="rp-blue">$Z$</span> | Vertically out of the monitor, toward the viewer |

![ReciPro coordinate axes shown on the monitor](../../../assets/references/Coordinates1.png)

The **beam direction** corresponds to the viewing direction (looking into the monitor), i.e. the <span class="rp-blue">$-Z$</span> axis.

Most operations in ReciPro involve only *directions* (expressed as 3×3 rotation matrices) and do not require an explicit origin. The one exception is the **Crystal Diffraction** function, which needs an explicit origin — see [A1.2. Coordinate system for diffraction simulation](2-diffraction.md).

## Initial crystal direction

The initial orientation (at first launch, or after **Reset rotation**) is defined as:

1. The <span class="rp-blue">$c$</span>-axis is aligned with the <span class="rp-blue">$Z$</span>-axis.
2. The <span class="rp-green">$b$</span>-axis lies in the <span class="rp-green">$Y$</span><span class="rp-blue">$Z$</span> plane, close to the <span class="rp-green">$Y$</span>-axis.
3. The <span class="rp-red">$a$</span>-axis is then fixed by the <span class="rp-green">$b$</span>- and <span class="rp-blue">$c$</span>-axes (right-hand rule).

![Initial orientation: the crystal a / b / c axes relative to X / Y / Z, with the incident beam along −Z](../../../assets/references/Coordinates2.png)

Equivalently:

- The direction out of the monitor (toward the viewer) is the **[001]** zone axis.
- The rightward direction on the monitor is the normal of the **(100)** plane.

> **Note:** The <span class="rp-blue">$c$</span>-axis (= [001]) always coincides with <span class="rp-blue">$Z$</span>, but in some crystal systems the <span class="rp-red">$a$</span>- and <span class="rp-green">$b$</span>-axes do **not** necessarily coincide with <span class="rp-red">$X$</span> and <span class="rp-green">$Y$</span>.

## Euler angles

Crystal orientation is expressed with three Euler angles <span class="rp-olive">$\Phi$</span>, <span class="rp-cyan">$\theta$</span>, <span class="rp-magenta">$\Psi$</span>, applied in <span class="rp-blue">$Z$</span>–<span class="rp-red">$X$</span>–<span class="rp-blue">$Z$</span> order (<span class="rp-magenta">$\Psi$</span>, then <span class="rp-cyan">$\theta$</span>, then <span class="rp-olive">$\Phi$</span>). When all three angles are zero, the corresponding rotation axes are:

| Angle | Axis (when all angles = 0) | Rank |
|-------|----------------------------|------|
| <span class="rp-olive">$\Phi$</span> | <span class="rp-blue">$Z$</span> | 1st (highest) |
| <span class="rp-cyan">$\theta$</span> | <span class="rp-red">$X$</span> | 2nd (middle) |
| <span class="rp-magenta">$\Psi$</span> | <span class="rp-blue">$Z$</span> | 3rd (lowest) |

![Euler-angle rotation axes — Φ (yellow), θ (cyan), Ψ (magenta) — shown at 0° (top) and at 15° (bottom)](../../../assets/references/Coordinates3.png)

The three angles form a **hierarchy**: <span class="rp-olive">$\Phi$</span> is the highest rotation, followed by <span class="rp-cyan">$\theta$</span>, then <span class="rp-magenta">$\Psi$</span>. The direction of a lower axis depends on the state of the higher rotations. For example, when <span class="rp-olive">$\Phi$</span> = <span class="rp-cyan">$\theta$</span> = <span class="rp-magenta">$\Psi$</span> = 15°, the <span class="rp-olive">$\Phi$</span> axis still coincides with <span class="rp-blue">$Z$</span>, but the <span class="rp-cyan">$\theta$</span> and <span class="rp-magenta">$\Psi$</span> axes generally align with none of <span class="rp-red">$X$</span>, <span class="rp-green">$Y$</span>, or <span class="rp-blue">$Z$</span>.

> The **Rotation Geometry** window can re-express this orientation in an arbitrary, experiment-specific Euler-angle convention (e.g. to match a laboratory goniometer). See [4. Rotation Geometry](../../4-rotation-geometry.md).
