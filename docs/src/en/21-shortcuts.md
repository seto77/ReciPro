# Keyboard & Mouse Shortcuts

ReciPro wires many functions to **key combinations** and to **mouse buttons combined with modifier keys** — things that are not visible on a button or a menu. This page collects them all in one place. Each window's own page also repeats its shortcuts near the top.

<kbd>F1</kbd> works in **every** window and opens that window's page of this online manual.

---

## Application-wide shortcuts

<!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormMain.cs:FormMain_Load (KeyDown += FormMain_KeyDown), FormCaptureGUI.cs:InstallShortcutFilter, NumericBox.cs:ProcessCmdKey/tryStepByArrowKey/textBox_KeyDown) -->
These are installed by the [main window](0-main-window.md) but stay active while the Structure Viewer, Stereonet, Diffraction Simulator, and Spot ID v1 windows are focused.

| Shortcut | Action |
|----------|--------|
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd> + arrow keys | Rotate the crystal one step in that direction (hold two arrows for a diagonal) |
| Hold <kbd>CTRL</kbd> while ReciPro starts | Start with OpenGL disabled (recovery for graphics problems; this launch only, not saved) |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>ALT</kbd>+<kbd>C</kbd> (any window) | Open the (experimental) GUI-capture tool for the active window |

### Numeric input boxes

Most numeric fields in every window behave the same way.

| Shortcut | Action |
|----------|--------|
| <kbd>↑</kbd> / <kbd>↓</kbd> (field focused, spin buttons shown) | Step the value up / down, exactly like the spin buttons |
| <kbd>ENTER</kbd> | Commit the typed value |
| <kbd>SHIFT</kbd>+<kbd>ENTER</kbd> or <kbd>CTRL</kbd>+<kbd>ENTER</kbd> | Evaluate the typed arithmetic expression and replace it with the result |

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

<!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormDiffractionSimulator.cs:graphicsBox_MouseWheel (MouseWheelZoom=true は回折シミュレータのみ), GraphicsBox.cs:OnMouseWheel) -->
Only the Diffraction Simulator pattern also zooms with the mouse wheel (×2 / ×0.5, re-centred on the cursor); the Stereonet and the EBSD Kikuchi pattern have **no** mouse-wheel zoom. There is no <kbd>+</kbd> / <kbd>-</kbd> key zoom.

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
| Right-click a crystal in the list | Context menu (Rename / Export as CIF / Duplicate / Delete) |
| Double-click the zone-axis index label (`[u v w]`) | Show / hide the max-UVW box |
| Drop a file | Load a crystal list (`.xml`, `.out`, `.cdb2`) or a crystal (`.cif`, `.amc`) <!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormMain.cs:FormMain_DragDrop, contextMenuStripListBox) --> |

### 1. Crystal database
[Open page →](1-crystal-database.md)

| Shortcut | Action |
|----------|--------|
| <kbd>ENTER</kbd> in a search field | Run the search |
| Click a result row | Load that crystal |
| Click an element in the **Periodic Table** popup | Cycle its filter: *may or not include* → *must include* → *must exclude* |

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
| Left double-click the net | Switch between **Planes** and **Axes** projection |
| Move the mouse over the net | Read off the (hkl)/[uvw] under the cursor |

### 7. Diffraction simulator
[Open page →](7-diffraction-simulator/index.md) — the pattern is a [diffraction-pattern view](#pattern), plus wheel zoom.

| Shortcut | Action |
|----------|--------|
| Mouse wheel up / down | Zoom in (×2) / out (×0.5), re-centring the view on the cursor position (on the fixed centre when the centre **Fix** box is on) <!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormDiffractionSimulator.cs:graphicsBox_MouseWheel) --> |
| Left double-click a spot | Show reflection details (index, *d*, structure factor, excitation error) |
| <kbd>CTRL</kbd> + Middle-drag | Move the detector centre (when the detector area is shown) |
| Right double-click the status bar | Copy a text summary of the current settings |
| Right double-click a lit layer button (Spots / Kikuchi / Debye / Scale) | Blink that layer on and off |
| Left double-click the stereonet — **TEM holder** window | Set the holder tilt to that point |
| Arrow keys — **TEM holder** window | Step the holder tilt (tick **Enable arrow keys** first; works with any ReciPro window in front) |
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
[Open page →](10-spot-id.md) — there is no image view (the spot geometry is entered as numbers).

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
| With **Zone axis** picking on: left double-click / <kbd>CTRL</kbd> + left double-click / <kbd>SHIFT</kbd> + left double-click the pattern | Add a zone-axis point / remove the point within 5 screen pixels / select that point <!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormEBSD.cs:HandleZoneAxisDoubleClick) --> |

### 20. Macro
[Open page →](20-macro/index.md)

| Shortcut | Action |
|----------|--------|
| <kbd>CTRL</kbd>+<kbd>S</kbd> | Save the editor text back into the selected macro-list entry |
| <kbd>F10</kbd> | Advance one step (during step-by-step execution) |
| Double-click a row in the function-help list | Insert that function's signature at the caret |
| Drop a `.mcr` file on the window | Load it into the editor and add it to the macro list (the file name becomes the macro name) |
