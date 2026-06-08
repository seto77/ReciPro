# Keyboard & Mouse Shortcuts

ReciPro wires many functions to **key combinations** and to **mouse buttons combined with modifier keys** — things that are not visible on a button or a menu. This page collects them all in one place. Each window's own page also repeats its shortcuts near the top.

<kbd>F1</kbd> works in **every** window and opens that window's page of this online manual.

---

## Application-wide shortcuts

These are installed by the [main window](0-main-window.md) but stay active while the Structure Viewer, Stereonet, Diffraction Simulator, Spot ID, and Calculator windows are focused.

| Shortcut | Action |
|----------|--------|
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>D</kbd> | Toggle the Diffraction Simulator |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>V</kbd> | Toggle the Structure Viewer |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>S</kbd> | Toggle the Stereonet |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>T</kbd> | Toggle Spot ID |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd> + arrow keys | Rotate the crystal one step in that direction (hold two arrows for a diagonal) |
| Double-tap <kbd>CTRL</kbd> | Toggle the Calculator |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>R</kbd> | Toggle the selected crystal's *Reserved* flag |
| <kbd>CTRL</kbd>+<kbd>ALT</kbd>+<kbd>SHIFT</kbd>+<kbd>C</kbd> | Capture a screenshot of the GUI (developer tool; enable **Capture GUI Components** first) |
| Hold <kbd>CTRL</kbd> while ReciPro starts | Start with OpenGL disabled (recovery for graphics problems) |

---

## Shared interaction models

Almost every interactive view in ReciPro belongs to one of three families. Knowing the family tells you the drag/zoom behaviour without memorising each window.

### 3-D OpenGL views { #3d }

Used by the [Structure Viewer](5-structure-viewer.md), [Rotation Geometry](4-rotation-geometry.md), the [Stereonet](6-stereonet.md)'s 3-D sphere, [Electron Trajectory](8-electron-trajectory.md), and the [EBSD](12-ebsd-simulation.md) geometry / master-pattern views.

| Action | Result |
|--------|--------|
| Left-drag | Rotate — trackball near the centre, in-plane roll near the edge |
| Right-drag up/down, or Mouse wheel | Zoom |
| Middle-drag | Pan (only where enabled) |
| <kbd>CTRL</kbd> + Right-drag up/down | Change the camera distance (perspective mode only) |
| <kbd>CTRL</kbd> + Right double-click | Toggle orthographic / perspective projection |

Individual windows may switch pan or zoom off (for example, Electron Trajectory and the EBSD 3-D views have panning disabled).

### Diffraction-pattern views { #pattern }

Used by the [Diffraction Simulator](7-diffraction-simulator/index.md) pattern, the [EBSD](12-ebsd-simulation.md) Kikuchi pattern, and the 2-D [Stereonet](6-stereonet.md). The key difference from the 3-D views: **dragging rotates the crystal itself**, not just the camera, so every linked window updates together.

| Action | Result |
|--------|--------|
| Left-drag near the centre | Tilt the crystal |
| Left-drag the outer area | Spin the crystal about the view/beam axis |
| Right-click | Zoom out |
| Right-drag a box | Zoom in to the selected region |
| Middle-drag | Pan |

There is **no** mouse-wheel zoom on these views.

### Image views { #image }

Used by the [HRTEM/STEM](9-hrtem-stem-simulator/index.md) result panes, the [Spot ID v2](11-spot-id-v2.md) image, and the [EBSD](12-ebsd-simulation.md) 2-D master pattern.

| Action | Result |
|--------|--------|
| Left-drag / Middle-drag | Pan |
| Mouse wheel up / down | Zoom in (×2) / out (×0.5) at the cursor |
| Right-drag a box | Zoom in to the selected region |
| Right-click / Right double-click | Zoom out (×0.5) |

---

## Per-window reference

### 0. Main window
[Open page →](0-main-window.md) · plus the application-wide shortcuts above.

| Shortcut | Action |
|----------|--------|
| Left-drag the orientation widget (bottom-left) | Rotate the crystal |
| Right double-click the orientation widget | Copy the widget image to the clipboard |
| Single-click / double-click a function button | Toggle that window / force it to the front |
| Right-click a crystal in the list | Context menu (Rename / Duplicate / Delete / Export CIF…) |
| Double-click the **Current Index** label | Show / hide the max-UVW box |
| Drop a file | Load a crystal list (`.xml`, `.cdb2`) or a crystal (`.cif`, `.amc`) |

### 1. Crystal database
[Open page →](1-crystal-database.md)

| Shortcut | Action |
|----------|--------|
| <kbd>ENTER</kbd> in a search field | Run the search |
| Click a result row | Load that crystal |
| Click an element in the periodic-table popup | Cycle its filter: ignore → must include → must exclude |

### 2. Symmetry information · 3. Beam interaction
Symmetry information has no special key/mouse combinations. In Beam Interaction, besides <kbd>F1</kbd> and the **Copy** buttons, the vertical cursor on the **Scattering factors** graph can be dragged to read off each element's value.
[Symmetry →](2-symmetry-information.md) · [Beam interaction →](3-beam-interaction.md)

### 4. Rotation geometry
[Open page →](4-rotation-geometry.md) — six **linked** [3-D views](#3d); rotating any one rotates all six together. The small *Axes* / *Objects* views have zoom and pan disabled.

### 5. Structure viewer
[Open page →](5-structure-viewer.md) — main view is a [3-D view](#3d).

| Shortcut | Action |
|----------|--------|
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>C</kbd> | Copy the rendered image to the clipboard |
| Left double-click an atom | Show coordinates, nearest-neighbour distances, and bond angles |
| Left-drag the crystal-axes gizmo | Rotate the model (no in-plane spin) |
| Left-drag the light gizmo | Change the lighting direction |

### 6. Stereonet
[Open page →](6-stereonet.md) — the 2-D net is a [diffraction-pattern view](#pattern); the optional 3-D sphere is a [3-D view](#3d).

| Shortcut | Action |
|----------|--------|
| Left double-click the net | Switch between **Plane** and **Axis** projection |
| Move the mouse over the net | Read off the (hkl)/[uvw] under the cursor |

### 7. Diffraction simulator
[Open page →](7-diffraction-simulator/index.md) — the pattern is a [diffraction-pattern view](#pattern) (no wheel zoom).

| Shortcut | Action |
|----------|--------|
| Left double-click a spot | Show reflection details (index, *d*, structure factor, excitation error) |
| <kbd>CTRL</kbd> + Middle-drag | Move the detector centre (when the detector area is shown) |
| Right double-click the status bar | Copy a text summary of the current settings |
| Right double-click a lit layer button (Spots / Kikuchi / Debye / Scale) | Blink that layer on and off |
| Left double-click the stereonet — **TEM holder** window | Set the holder tilt to that point |
| Arrow keys — **TEM holder** window | Step the holder tilt (tick **Arrow keys** first) |
| Drop `.prm` / image — **Detector geometry**, or `.txt` — **Dynamic compression** | Load that data |

### 8. Electron trajectory
[Open page →](8-electron-trajectory.md) — a [3-D view](#3d) with panning disabled.

### 9. HRTEM / STEM simulator
[Open page →](9-hrtem-stem-simulator/index.md) — result panes are [image views](#image) and pan/zoom together.

| Shortcut | Action |
|----------|--------|
| <kbd>CTRL</kbd>+<kbd>C</kbd> (image grid focused) | Copy the image(s) to the clipboard as a metafile |
| <kbd>CTRL</kbd> + Right-drag a box | Select a rectangular area |
| Left double-click a pane | Maximise that pane / restore the grid (multi-pane layouts) |

### 10. Spot ID v1
[Open page →](10-spot-id.md) — the image is reference-only (not interactive).

| Shortcut | Action |
|----------|--------|
| Double-click a row in the results list | Select that crystal and rotate it to the matching zone axis |

### 11. Spot ID v2
[Open page →](11-spot-id-v2.md) — the image is an [image view](#image) with spot editing on top.

| Shortcut | Action |
|----------|--------|
| Left double-click the image | Add a spot (peak-fitted) |
| <kbd>CTRL</kbd> + Left double-click | Add a spot and mark it as the direct (000) beam |
| Left-click a spot | Select the nearest spot |
| <kbd>CTRL</kbd> + Right-click a spot | Delete the nearest spot |
| <kbd>CTRL</kbd> + arrow keys | Nudge the selected spot by one pixel |
| Double-click a spot's row header | Zoom to that spot (×2) |

### 12. EBSD simulation
[Open page →](12-ebsd-simulation.md) — the Kikuchi pattern is a [diffraction-pattern view](#pattern); the 3-D views are [3-D views](#3d) (pan off); the 2-D master pattern is an [image view](#image).

| Shortcut | Action |
|----------|--------|
| Double-click the Kikuchi pattern | Pick the detector sub-cell under the cursor and show its statistics |

### 20. Macro
[Open page →](20-macro/index.md)

| Shortcut | Action |
|----------|--------|
| <kbd>CTRL</kbd>+<kbd>S</kbd> | Save the editor text back into the selected macro-list entry |
| <kbd>F10</kbd> | Advance one step (during step-by-step execution) |
| Double-click a row in the function-help list | Insert that function's signature at the caret |
| Drop a `.mcr` file | Load it into the editor |
