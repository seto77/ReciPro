# Rotation Geometry

This window represents the rotational state of a crystal as a 3×3 matrix and converts between different Eulerian coordinate systems.

![Rotation Geometry](../assets/cap-en-auto/FormRotationMatrix.png)

ReciPro uses three Euler angles — **Ψ**, **θ**, and **Φ** — applied in **Z–X–Z** order. However, this convention does not necessarily match the goniometer axes of your actual instrument. The **Rotation Geometry** window lets you convert ReciPro's Euler angles to an arbitrarily defined coordinate system, supporting goniometer adjustment in the laboratory.

---

## Keyboard & mouse shortcuts

All six 3-D views (the ReciPro and experimental goniometer / axes / objects panels) are **linked** — rotating any one rotates all six together. They share ReciPro's standard [OpenGL view navigation](21-shortcuts.md).

| Shortcut | Action |
|----------|--------|
| <kbd>F1</kbd> | Open this page of the online manual |
| Left-drag a view | Rotate the model (all six views rotate together) |
| Mouse wheel, or Right-drag up/down | Zoom (the large goniometer views) |
| <kbd>CTRL</kbd> + Right-drag up/down | Change the camera distance (perspective mode only) |
| <kbd>CTRL</kbd> + Right double-click | Toggle orthographic / perspective projection |

The small *Axes* and *Objects* views have zoom and pan disabled. There are no keyboard shortcuts other than <kbd>F1</kbd>.

---

## ReciPro coordinate system (ZXZ)

The upper half of the window shows the rotation state in the "ReciPro coordinate system".

- **Φ, θ, Ψ** values are synchronised with the Euler angles set in the Main window.
- **Rotation matrix** displays the 3×3 matrix corresponding to the current rotation state.

### Φ, θ, Ψ (Z–X–Z Euler angles)

The crystal orientation is parametrised by three rotations applied in this order:

1. **Φ** — first rotation about the **Z** axis.
2. **θ** — rotation about the **X** axis of the once-rotated frame.
3. **Ψ** — second rotation about the **Z** axis of the twice-rotated frame.

Every numeric box is editable; changing a value here updates the Main window and every linked simulator.

### Rotation matrix

The 3 × 3 matrix produced from the current (Φ, θ, Ψ). Use **Copy to Excel** / **Paste from Excel** to round-trip the matrix through a spreadsheet.

The matrix follows one fixed convention everywhere it appears in ReciPro:

$$
\mathbf{v}_{\mathrm{rot}} = R\,\mathbf{v}_{0}
$$

- \(R\) is the **active** rotation of the crystal, expressed in the monitor-fixed laboratory frame (\(X\) right, \(Y\) up, \(Z\) toward the viewer — see [Appendix A1.1](appendix/a1-coordinate-system/1-orientation.md)).
- \(\mathbf{v}_{0}\) : Cartesian coordinates (a column vector) of a crystal-fixed direction in the **initial orientation** (\(c \parallel Z\), \(b\) in the \(YZ\) plane).
- \(\mathbf{v}_{\mathrm{rot}}\) : coordinates of the same direction after the rotation, still in the laboratory frame.
- At \(\Phi=\theta=\Psi=0\), \(R\) is the identity. \(R\) is orthonormal with determinant +1, so the inverse transform is simply the transpose: \(R^{-1}=R^{\mathsf T}\).

In terms of the Euler angles, \(R = R_Z(\Phi)\,R_X(\theta)\,R_Z(\Psi)\) (right-handed rotations about the fixed axes; \(\Psi\) acts on the vector first):

$$
R=\begin{pmatrix}
\cos\Phi\cos\Psi-\cos\theta\,\sin\Phi\sin\Psi & -\cos\Phi\sin\Psi-\cos\theta\,\sin\Phi\cos\Psi & \sin\theta\,\sin\Phi\\
\sin\Phi\cos\Psi+\cos\theta\,\cos\Phi\sin\Psi & -\sin\Phi\sin\Psi+\cos\theta\,\cos\Phi\cos\Psi & -\sin\theta\,\cos\Phi\\
\sin\theta\,\sin\Psi & \sin\theta\,\cos\Psi & \cos\theta
\end{pmatrix}
$$

To apply it to crystallographic indices, first convert them to Cartesian coordinates in the initial orientation: a direction \([uvw]\) becomes \(\mathbf{v}_0 = u\mathbf{a}+v\mathbf{b}+w\mathbf{c}\), and a plane normal \((hkl)\) becomes \(\mathbf{v}_0 = h\mathbf{a}^{*}+k\mathbf{b}^{*}+l\mathbf{c}^{*}\) (reciprocal axes).

The same matrix, with \(R_{ij}\) = row \(i\), column \(j\), appears as:

- **Copy to Excel** / **Paste from Excel** in this window (3 × 3, tab-separated, rows from top to bottom);
- `Dir.GetRotationMatrix()` / `Dir.SetRotationMatrix()` and `Dir.GetEuler()` in the [macro API](20-macro/1-built-in-functions.md) (nine elements in row order \(R_{11}, R_{12}, R_{13}, R_{21}, \ldots, R_{33}\));
- the columns **R11**–**R33** of the candidate list saved by [Spot ID v2](11-spot-id-v2.md).

### OpenGL windows

The 3D view shows the current rotation using three coloured toruses (doughnuts):

| Colour | Euler angle | Goniometer level |
|--------|------------|-----------------|
| **Yellow** | Φ | 1st (upper) axis |
| **Light blue** | θ | 2nd (middle) axis |
| **Pink** | Ψ | 3rd (lower) axis |

The **red**, **green**, and **blue** arrows represent the X, Y, Z axes in real-space Cartesian coordinates. These are *not* the same as the crystal axes shown in the Main window.

The grey sphere at the centre represents the sample; red/green/blue spheres show how the object has rotated from its initial orientation (when Φ = θ = Ψ = 0, they align with +X, +Y, +Z respectively).

> **Note**: Dragging in the OpenGL window changes only the *projection direction* of this view, not the crystal orientation itself. To rotate the crystal, use the Main window.

### Buttons

| Button | Action |
|--------|--------|
| Copy to Excel | Copy the 3×3 rotation matrix in tab-separated format |
| Paste from Excel | Set rotation matrix from clipboard (tab-separated 3×3) |
| View along beam | Match the Main window projection (Z-axis perpendicular to screen) |
| Isometric | Switch to isometric projection |

---

## Experimental coordinate system

The lower half defines Euler angles on an arbitrary set of rotation axes and gets/sets the goniometer state. This is called the **Experimental coordinate system**.

### 1st, 2nd, 3rd axes

Select the rotation axes of the goniometer from **±X**, **±Y**, and **±Z** for each level (upper, middle, lower). The graphics update accordingly.

The Euler angles for each axis are displayed in the corresponding coloured text boxes (yellow, light blue, pink). You can also enter values directly.

---

## Link

When **Link** is checked, the ReciPro coordinate system and the Experimental coordinate system are coupled: their Euler angles are adjusted so that the object orientation is consistent between the two systems.

### Example workflow

1. In the laboratory, set a goniometer so that the *a*-axis of a crystal is aligned with the X-ray incidence direction and the *b*-axis is horizontal.
2. Enter the laboratory goniometer's Euler angles in the Experimental coordinate system.
3. In the Main window, rotate the crystal so that the *a*-axis faces the screen normal and the *b*-axis faces horizontal.
4. Check **Link** — now, whenever you point the crystal to a different orientation in the Main window, the required goniometer angles are automatically displayed.

---

## See also

- [Main window](0-main-window.md)
- [Stereonet](6-stereonet.md)
- [Basic coordinate system & crystal orientation](appendix/a1-coordinate-system/1-orientation.md)
- [Keyboard & mouse shortcuts](21-shortcuts.md)
