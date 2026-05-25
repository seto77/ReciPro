# Appendix A2. Detector Coordinate System

**Crystal Diffraction** simulates a diffraction pattern on a detector. The detector is a finite-sized plane of pixels placed at a fixed distance from the sample, possibly tilted with respect to the incident beam. Accurate simulation requires knowledge of the geometric relationship between detector and sample.

---

## Before rotation (detector normal to beam)

### Real coordinates (X, Y, Z)
3D Cartesian coordinates in mm with the **sample** as origin.
- **Z-axis**: always parallel to the beam direction (note: differs from Appendix A1 definition).
- **X-axis**: rightward facing Z.
- **Y-axis**: downward.

### Detector coordinates (X', Y')
2D Cartesian coordinates in mm on the detector plane with the **foot** as origin. X' and Y' are parallel to X'' and Y''.

### Pixel coordinates (X'', Y'')
2D coordinates in pixel units with the **upper-left corner** of the detector as origin.

### Key definitions

| Term | Definition |
|------|-----------|
| **Foot** | The foot of the perpendicular from the sample to the detector plane. If the detector is normal to the beam, the foot coincides with the direct spot |
| **Direct spot** | Intersection of the incident beam and the detector |
| **Camera length 2 (C2)** | Distance from sample to foot (mm) |
| **Camera length 1 (C1)** | Distance from sample to direct spot (mm) |
| **Pixel size** | Side length of one pixel (mm); square pixels only |
| **Detector width/height** | Pixel counts horizontally/vertically |

---

## After rotation (tilted detector)

Two parameters describe the detector tilt:

| Parameter | Description |
|-----------|-------------|
| **φ** | Direction of the rotation axis (angle from X-axis in the XY plane) |
| **τ** | Rotation angle around the axis defined by φ (right-hand screw) |

After rotation:
- The **direct spot** and **foot** are no longer coincident.
- **C1** = distance from sample to direct spot.
- **C2** = distance from sample to foot.
- The origin of Detector coordinates remains at the **foot**.
- The origin of Pixel coordinates remains at the **upper-left corner**.
- When tilted, X/Y directions do not coincide with X'/Y' directions.
