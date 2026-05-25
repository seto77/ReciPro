<!-- nav -->

[← 5. Structure viewer](5-structure-viewer.md)  |  [🏠 Home](../index.md)  |  [7. Diffraction simulator →](7-diffraction-simulator/index.md)

# Stereonet

**Stereonet** displays crystal plane and axis directions using stereographic projection.

![Stereonet](../assets/cap-en-auto/FormStereonet.png)

---

## Mouse operations
- Left-drag to rotate the crystal
- Right-drag/click to zoom in/out
- Double-click to switch between planes/axes modes.
- Middle-drag to translate.

The plane/axis indices at the current **cursor position** are displayed as you move the mouse over the projection.

## File menu
Save or copy in raster or vector format. Vector format allows editing font/line thickness in PowerPoint.

## Mode

![Mode](../assets/cap-en-auto/FormStereonet.panel3.groupBoxMode.png)

- **Projection target** — **Axis**, **Plane**, or **Kikuchi line** pairs
- **Projection method** — **Wulff** (equal-angle) or **Schmidt** (equal-area)
- **Hemisphere** — upper or lower
- Show index labels; optionally weight the plot by structure factor

> For trigonal/hexagonal crystals, Miller–Bravais (4-index) notation can be enabled from **Option ▸ Use Miller-Bravais (hkil) index** in the main window.

## Indices

![Indices](../assets/cap-en-auto/FormStereonet.panel3.groupBoxIndices.png)

- **Range**: specify [uvw] or {hkl} range
- **Specified**: add individual indices, with optional crystallographic equivalents
- **Colour** — set the plot colour, or tick *change colour automatically* to colour-code each set of equivalent axes/planes differently

## 3D Options

![3D Options](../assets/cap-en-auto/FormStereonet.panel3.panel3DOptions.png)

## Tab menu

### Appearance

![Appearance tab](../assets/cap-en-auto/FormStereonet.tabControl.tabPage1.png)

String size, point size, colour, outline.

### Great and Small Circle

![Great and Small Circle tab](../assets/cap-en-auto/FormStereonet.tabControl.tabPage2.png)

Specify by zone axis or crystal plane indices.

### Wave Length

![Wave Length tab](../assets/cap-en-auto/FormStereonet.tabControl.tabPage4.png)

Set the wave source (X-ray / electron / neutron) and the wavelength or energy used for the projection.

---

[← 5. Structure viewer](5-structure-viewer.md)  |  [🏠 Home](../index.md)  |  [7. Diffraction simulator →](7-diffraction-simulator/index.md)
