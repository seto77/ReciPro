# Crystal orientation control

ReciPro lets you rotate the selected crystal interactively in the [Main window](index.md). The same orientation is shared by the diffraction simulator, Structure Viewer, Stereonet, HRTEM/STEM simulator, EBSD simulator, and other windows.

> A short video tutorial is available on the [How to use](../appendix/a0-how-to-use.md) page.

---

## Basic Ways to Set the Orientation

| Method | Use when | Where |
|--------|----------|-------|
| Mouse drag | You want to rotate freely while watching the crystal axes. | **Current orientation** panel |
| Arrow buttons | You want small, repeatable rotations. | **Arrows** panel |
| Zone axis | You know the viewing direction, such as \([001]\) or \([110]\). | **View along** / zone-axis input |
| Plane normal | You want a crystal plane \((hkl)\) normal to the screen. | **View along** / plane input |
| Euler angles | You need a reproducible numeric orientation. | **Euler angles (Z-X-Z)** |

---

## Recommended Workflow

1. Select the crystal in **Crystal List**.
2. Use **View along** to place the desired zone axis or plane normal perpendicular to the screen.
3. If the in-plane direction matters, set both the zone-axis direction and the plane direction so the screen-up direction is fixed.
4. Open the target tool, such as [Diffraction simulator](../7-diffraction-simulator/index.md), [Structure Viewer](../5-structure-viewer.md), or [HRTEM/STEM simulator](../9-hrtem-stem-simulator/index.md).

The orientation displayed here is not just a view setting. It defines the incident beam direction and the crystal coordinate relationship used by simulations.

---

## Related Reference

- [Main window](index.md) — full description of the rotation-control panels.
- [Rotation Geometry](../4-rotation-geometry.md) — rotation matrices and Euler-angle definitions.
- [Appendix A1. Coordinate systems](../appendix/a1-coordinate-system/1-orientation.md) — coordinate conventions used throughout ReciPro.
