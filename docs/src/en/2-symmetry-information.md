# Symmetry Information

**Symmetry Information** displays the space-group symmetry of the selected crystal and renders *International Tables for Crystallography* Vol. A-style schematic diagrams of the symmetry elements and general positions.

![Symmetry Information](../assets/cap-en-auto/FormSymmetryInformation.png)

The window is divided into a space-group identity area (top left), a calculation/table area with tabs (top right), and two schematic diagrams (bottom).

---

## Keyboard & mouse shortcuts

This window has no special key or mouse combinations. <kbd>F1</kbd> opens this manual page, and the two **Copy** buttons place the symmetry-element diagram and the general-position diagram on the clipboard (as a bitmap, or as a vector EMF when **EMF** is ticked).

→ See **[21. Keyboard & mouse shortcuts](21-shortcuts.md)** for every window at a glance.

---

## Space-group identity

The top-left panel lists, for the current space group:

- **Number** (1–230) and the setting index
- **Crystal System**
- **Point Group** : Hermann–Mauguin (HM) and Schoenflies (SF) symbols
- **Space Group** : HM short symbol, HM full symbol, SF symbol, and **Hall symbol**

---

## Geometrics Calculation

![Geometrics Calculation](../assets/cap-en-auto/FormSymmetryInformation.panel2.tabControl.tabPageGeometrics.png)

Enter two planes (*h₁k₁l₁*, *h₂k₂l₂*) or two axes (*u₁v₁w₁*, *u₂v₂w₂*) to obtain:

- the d-spacing of each plane / the length of each axis,
- the angle between the two planes (or two axes),
- **the axis normal to both planes** and **the plane normal to both axes**.

These calculations respect the metric of the current unit cell.

---

## Wyckoff Positions

![Wyckoff Positions](../assets/cap-en-auto/FormSymmetryInformation.panel2.tabControl.tabPageWyckoff.png)

Lists every Wyckoff position with its multiplicity, Wyckoff letter, site symmetry, and representative coordinates (coset representatives for centred lattices are shown in the header row).

---

## Conditions

![Conditions](../assets/cap-en-auto/FormSymmetryInformation.panel2.tabControl.tabPageConditions.png)

Reflection (extinction) conditions arising from the lattice centring and from the glide/screw symmetry operators.

---

## Symmetry-element & general-position diagrams

![Symmetry-element & general-position diagrams](../assets/cap-en-auto/FormSymmetryInformation.tableLayoutPanel1.png)

The two panels at the bottom reproduce the schematic diagrams used in *International Tables for Crystallography* Vol. A.

- **Left — symmetry elements**: rotation/screw axes, mirror/glide planes, inversion centres and rotoinversion points are drawn with the conventional graphical symbols.
- **Right — general positions**: the general equivalent positions are plotted as `+`/`−` circles (filled comma marks denote opposite handedness), with their fractional coordinates.

Controls below the diagrams:

- **Direction** (`a` / `b` / `c`) : choose the projection axis.
- The displayed range can be limited (e.g. *Upper left quadrant only*), and the *a*, *b*, *c* scale factors of the general-position plot can be adjusted.
- **Copy** each diagram to the clipboard as a vector image (**EMF**) or raster image (**BMP**); EMF can be ungrouped and edited in PowerPoint.

> The same symmetry elements can also be drawn directly onto the 3D model in the [5. Structure viewer](5-structure-viewer.md).
