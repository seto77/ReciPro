# Appendix A1.2. Coordinate system for diffraction simulation

<!-- 260526Cl: 図(Coordinates4-5)に合わせ座標記号を数式(MathJax)化・用語色を図に一致 (.rp-*: docs/src/assets/stylesheets/extra.css)。 -->

The **Crystal Diffraction** function simulates the diffraction pattern recorded on a detector. The detector is a finite plane of pixels placed at a fixed distance from the sample, and it may be tilted with respect to the incident beam. Reproducing this accurately requires the geometric relationship between the detector and the sample, together with the detector's pixel size and pixel count. For the basic (orientation) coordinate system, see [A1.1. Basic coordinate system and crystal orientation](1-orientation.md).

!!! note "Z and Y differ from the orientation system"
    Within the detector coordinate system, <span class="rp-steel">$Z$</span> is parallel to the beam and <span class="rp-steel">$Y$</span> points downward. This differs from the orientation coordinate system, where the beam is along <span class="rp-blue">$-Z$</span> and <span class="rp-green">$Y$</span> points up. The detector system follows the usual image/detector convention (origin at the top-left, <span class="rp-steel">$Y$</span> increasing downward).

## Before rotation (detector normal to the beam)

![Detector coordinate system with the detector normal to the beam](../../../assets/references/Coordinates4.png){width=500px}

Three coordinate systems are defined:

- <span class="rp-steel">**Real coordinates** ($X$, $Y$, $Z$)</span> : 3D Cartesian coordinates in mm, with the <span class="rp-steel">**sample**</span> as origin. <span class="rp-steel">$Z$</span> is parallel to the beam; viewed along <span class="rp-steel">$Z$</span>, <span class="rp-steel">$X$</span> points right and <span class="rp-steel">$Y$</span> points down. When the detector is normal to the beam, <span class="rp-steel">$X$ / $Y$</span> are parallel to <span class="rp-brown">$X'$ / $Y'$</span>.
- <span class="rp-brown">**Detector coordinates** ($X'$, $Y'$)</span> : 2D coordinates in mm on the detector plane, with the <span class="rp-brown">**foot**</span> as origin. <span class="rp-brown">$X'$ / $Y'$</span> point right / down on the detector and are parallel to <span class="rp-cyan">$X''$ / $Y''$</span>.
- <span class="rp-cyan">**Pixel coordinates** ($X''$, $Y''$)</span> : 2D coordinates in pixel units, with the <span class="rp-cyan">**upper-left corner**</span> of the detector as origin, following the detector's pixel rows and columns.

When the detector is perpendicular to the beam, the <span class="rp-brown">**foot**</span> and the <span class="rp-red">**direct spot**</span> coincide, and <span class="rp-red">**Camera length 1**</span> equals <span class="rp-brown">**Camera length 2**</span>.

## After rotation (tilted detector)

![Detector coordinate system with a tilted detector](../../../assets/references/Coordinates5.png){width=500px}

The detector tilt is described by two parameters:

| Parameter | Description |
|-----------|-------------|
| <span class="rp-grass">$\varphi$</span> | Direction of the <span class="rp-grass">rotation axis</span> — its angle from the <span class="rp-steel">$X$</span>-axis, measured in the <span class="rp-steel">$XY$</span> (<span class="rp-steel">$Z$</span> = 0) plane |
| <span class="rp-grass">$\tau$</span> | Rotation angle about that axis (right-hand screw) |

Once the detector is tilted:

- The <span class="rp-red">**direct spot**</span> and the <span class="rp-brown">**foot**</span> no longer coincide.
- <span class="rp-red">**Camera length 1** ($C_1$)</span> = distance from the <span class="rp-steel">sample</span> to the <span class="rp-red">direct spot</span>.
- <span class="rp-brown">**Camera length 2** ($C_2$)</span> = distance from the <span class="rp-steel">sample</span> to the <span class="rp-brown">foot</span>.
- The origin of <span class="rp-brown">**Detector coordinates**</span> stays at the <span class="rp-brown">**foot**</span>; the origin of <span class="rp-cyan">**Pixel coordinates**</span> stays at the <span class="rp-cyan">**upper-left corner**</span>.
- The <span class="rp-steel">$X$ / $Y$</span> directions no longer coincide with <span class="rp-brown">$X'$ / $Y'$</span>.

## Parameter glossary

| Term | Definition |
|------|------------|
| <span class="rp-steel">**Sample**</span> | The material scattering the incident beam; the origin of the real coordinates |
| <span class="rp-steel">**Real coordinates** ($X$, $Y$, $Z$)</span> | 3D coordinates (mm) of the experimental setup; origin at the sample, <span class="rp-steel">$Z$</span> always parallel to the beam |
| <span class="rp-red">**Direct spot**</span> | Intersection of the incident beam and the detector |
| <span class="rp-brown">**Foot**</span> | The foot of the perpendicular from the sample to the detector plane; origin of the detector coordinates. Coincides with the direct spot only when the detector is normal to the beam. For overlay-image mode, set the foot position in pixel coordinates |
| <span class="rp-brown">**Detector coordinates** ($X'$, $Y'$)</span> | 2D coordinates (mm) on the detector plane; origin at the foot |
| <span class="rp-cyan">**Pixel coordinates** ($X''$, $Y''$)</span> | 2D coordinates (pixels) on the detector plane; origin at the upper-left corner |
| <span class="rp-red">**Camera length 1** ($C_1$)</span> | Distance from the sample to the direct spot (mm) |
| <span class="rp-brown">**Camera length 2** ($C_2$)</span> | Distance from the sample to the foot (mm) |
| **Pixel size** | Side length of one (square) pixel (mm); only square pixels are supported |
| **Detector width / height** | Number of pixels horizontally / vertically |
