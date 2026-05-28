# Spot ID v2

**Spot ID v2** is the enhanced version of [Spot ID](10-spot-id.md) with improved spot detection, fitting algorithms, and a more powerful indexing engine.

![Spot ID v2](../assets/cap-en-auto/FormSpotIDV2.png)

---

## File menu

Open / save a diffraction image. The same drag-and-drop loading as [Spot ID v1](10-spot-id.md) is supported, and Gatan DM3/DM4 metadata (camera length, wavelength, pixel size) is honoured automatically.

---

## Optics

![Optics](../assets/cap-en-auto/FormSpotIDV2.splitContainer1.panel1.groupBoxOptics.png)

### Incident source

Select the radiation type (X-ray / electron / neutron) and set the energy or wavelength.

### Camera length / Pixel size

The camera length (mm) and detector pixel size (μm). When a Gatan DM file is loaded, these values are populated from the file header.

---

## Spot information

![Spot information](../assets/cap-en-auto/FormSpotIDV2.splitContainer1.groupBoxSpot.png)

- **Find spots**: Automatic spot detection with advanced peak-finding using local maxima and background subtraction.
- **Donut filter**: Applies a donut-shaped (annular) filter to enhance ring-shaped diffraction features and suppress the central beam.
- **Delete spot / Clear spots**: Remove individual or all detected spots.
- **Reset range for all spots**: Reset the fitting range for all spots to default.
- **Copy to clipboard**: Copy spot positions and intensities to the clipboard for external analysis.

---

## Index

![Index](../assets/cap-en-auto/FormSpotIDV2.splitContainer1.groupBoxIndex.png)

- **Crystal selection**: Choose which crystals from the crystal list to use as candidates for indexing.
- **Search**: Run the indexing algorithm to find the best-matching crystal and zone axis.
- **Tolerance**: Set the acceptable deviation in d-spacing and angle for a match.
- **Results**: The best matches are displayed with crystal name, zone axis [uvw], and individual spot indices (hkl).

---

## Improvements over v1

- Better noise handling in spot detection.
- More robust fitting algorithms with multiple profile shapes.
- Faster indexing with optimized search algorithms.
- Support for overlapping spots and satellite reflections.
