# Spot ID v2

**Spot ID v2** is the enhanced version of [Spot ID](10-spot-id.md) with improved spot detection, fitting algorithms, and a more powerful indexing engine.

![Spot ID v2](../assets/cap-en-auto/FormSpotIDV2.png)

---

## Keyboard & mouse shortcuts

You build the spot list directly on the loaded image. The image pane uses ReciPro's standard [image-view navigation](21-shortcuts.md) for pan/zoom; spot editing adds the combinations below.

| Shortcut | Action |
|----------|--------|
| <kbd>F1</kbd> | Open this page of the online manual |
| Left double-click the image | Add a spot at that point (peak-fitted) |
| <kbd>CTRL</kbd> + Left double-click | Add a spot and mark it as the direct (000) beam |
| Left-click a spot | Select the nearest spot |
| <kbd>CTRL</kbd> + Right-click a spot | Delete the nearest spot |
| <kbd>CTRL</kbd> + arrow keys | Nudge the selected spot by one pixel |
| Left-drag / Middle-drag (empty area) | Pan the image |
| Mouse wheel | Zoom in / out at the cursor |
| Right-drag a box | Zoom in to the selected region |
| Right double-click | Zoom out |
| Double-click a spot's row header (table) | Zoom to that spot (×2) |

The main-window <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>T</kbd> opens/closes this window.

→ See **[21. Keyboard & mouse shortcuts](21-shortcuts.md)** for every window at a glance.

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
