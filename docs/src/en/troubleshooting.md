# Troubleshooting

Common issues and solutions for ReciPro. Many of the entries below come from questions and bug reports on the [GitHub issue tracker](https://github.com/seto77/ReciPro/issues); the version in which a bug was fixed is noted where applicable.

> **Most problems are resolved simply by updating to the [latest version](https://github.com/seto77/ReciPro/releases/latest).** ReciPro is updated frequently, and many of the bugs below were fixed within days of being reported.

---

## Startup and launch

### Symptom: The process is running but no window appears

ReciPro starts (it is visible in Task Manager) but its window never shows on screen.

**Cause**: The window opened off-screen — a Windows display-coordinate issue, typically after changing monitors or display scaling. (Issues [#50](https://github.com/seto77/ReciPro/issues/50), [#53](https://github.com/seto77/ReciPro/issues/53), [#55](https://github.com/seto77/ReciPro/issues/55))

**Solution**:

1. Open **Task Manager**.
2. Find **ReciPro** in the process list.
3. Right-click it and choose **Maximize**.

The window will be brought onto your main display. Note that **Switch to**, **Bring to front**, and **Minimize** do *not* help — only **Maximize** works.

### Symptom: ReciPro won't start, crashes, or hangs on startup

**Cause**: Most often OpenGL initialization fails, or a corrupted registry/setting value blocks startup.

**Solution** (try in order):

1. **Disable OpenGL**: hold the **Ctrl** key while launching ReciPro to start with OpenGL disabled. Recent versions (v4.925 and later) harden OpenGL initialization so the app launches even when OpenGL fails — in that case the 3D features are disabled but the rest of the app works.
2. **Reset the settings**: in the registry editor, delete the key `HKEY_CURRENT_USER\Software\Crystallography\ReciPro`, then restart. (Equivalent to **Option → Reset registry**.)
3. **Clean reinstall**: uninstall ReciPro, delete the following folders if present (replace `<user>` with your account name), then reinstall:
   - `C:\Users\<user>\AppData\Local\Crystallography Software\ReciPro`
   - `C:\Users\<user>\AppData\Roaming\ReciPro\ReciPro`
4. **Update** to the latest version.

If none of these help, the cause may be the OS environment itself; please [open an issue](https://github.com/seto77/ReciPro/issues) with your PC details (CPU, GPU, Windows version).

---

## OpenGL issues

### Symptom: Black screen or crash on startup

**Cause**: Incompatible GPU or remote desktop environment.

**Solution**:

1. Go to **Option → Disable OpenGL (needs restart)** (or hold **Ctrl** while launching).
2. Restart ReciPro.
3. Structure Viewer and some 3D features will use software rendering.

### Symptom: Integrated or older GPU (Intel/AMD) fails to render

**Cause**: Some integrated GPUs (e.g. AMD Radeon Vega, Intel UHD) had OpenGL initialization problems in older builds. (Issue [#2](https://github.com/seto77/ReciPro/issues/2))

**Solution**: Update to the latest version. The OpenGL version requirement was lowered (v4.781), integrated-GPU initialization was fixed (v4.785), and initialization was further hardened to fail gracefully (v4.925). Updating your GPU drivers also helps.

### Symptom: Poor rendering quality

**Solution**: Update your GPU drivers. An external (discrete) GPU with OpenGL 1.5 support is recommended.

---

## .NET Runtime

### Symptom: Application won't start

**Cause**: The required .NET Desktop Runtime is not installed. Current versions require **.NET Desktop Runtime 10.0** (older builds: v4.895–v4.91x required 9.0; see issue [#43](https://github.com/seto77/ReciPro/issues/43)).

**Solution**: Download and install from <https://dotnet.microsoft.com/download/dotnet/10.0> (choose the **Desktop Runtime**, x64 for most PCs).

### Symptom: Cannot reach the Microsoft download page

**Solution**: You can download the runtime installer directly. Pick the **Windows Desktop Runtime X64** for your architecture from the [.NET 10.0 download page](https://dotnet.microsoft.com/download/dotnet/10.0). (Issue [#49](https://github.com/seto77/ReciPro/issues/49))

---

## Installation

### Symptom: Installing or uninstalling without administrator rights

**Note**: Admin rights are not required. Shortcuts and per-user files are placed in your own user folders (e.g. `%AppData%\Microsoft\Windows\Start Menu\Programs\Crystallography Software\` and the Desktop). (Issue [#38](https://github.com/seto77/ReciPro/issues/38))

---

## Display and layout

### Symptom: Buttons or panels are cut off / hidden, or the layout looks broken

For example, the **Peak Identification** button in Spot ID v2 is hidden, or the About page and other forms are misaligned, on recent versions. (Issues [#56](https://github.com/seto77/ReciPro/issues/56), [#59](https://github.com/seto77/ReciPro/issues/59))

**Cause**: A DPI-scaling / UI-font regression introduced in some recent builds.

**Solution**:

- Set your Windows **display scaling to 100%** (this usually restores the layout).
- As a quick workaround, **resize the window** (e.g. shrink it vertically) to reveal hidden controls.
- Update to the latest version — layouts are being fixed progressively. If a recent build looks worse, rolling back to a slightly older version (e.g. v4.915) is a temporary option. Please report any remaining broken forms.

---

## Dynamical calculations

### Symptom: Very slow or out of memory

**Cause**: Too many Bloch waves or too large an image.

**Solution**:

- Reduce **No. of Bloch waves** (50–200 is usually sufficient for routine calculations)
- Use the **Eigen** solver for ≤ 500 waves; **MKL** for > 500 waves
- Reduce image resolution for STEM simulations
- Close other memory-intensive applications

### Symptom: HAADF-STEM image is black

**Cause**: Atomic temperature factors (B) are set to zero.

**Solution**: Set B ≥ 0.5 Å² for all atoms. TDS intensity requires non-zero temperature factors.

---

## Diffraction simulator

### Symptom: The diffraction pattern is blank / nothing is drawn

**Cause**: Usually the view is zoomed in too far, or the incident-wave energy is out of range. (Issue [#3](https://github.com/seto77/ReciPro/issues/3))

**Solution**:

- **Left-click** the main drawing area to zoom out.
- Check the incident-wave energy on the **Wave** tab (upper left): X-ray ≈ 1–100 keV, electron ≈ 10–1000 keV are appropriate.

---

## File I/O

### Symptom: CIF file won't load

**Solution**:

- Check that the CIF file is well-formed
- Try dragging and dropping the file onto the **Crystal Information** area
- Some non-standard CIF extensions may not be supported

### Symptom: dm3/dm4 file won't load, or "unable to cast … 'System.Single' to 'System.Double'"

**Cause**: There are several variants of the DM3/DM4 format, and older builds could not read all of them. (Issue [#15](https://github.com/seto77/ReciPro/issues/15))

**Solution**: Update to the latest version — DM3 read compatibility was improved in v4.835. If a file still fails to load, please [send it](https://github.com/seto77/ReciPro/issues) so support can be added.

### Symptom: dm3/dm4 file shows wrong scale

**Solution**: Verify calibration in the original Digital Micrograph software. ReciPro reads the embedded metadata; if the metadata is incorrect, manually set the pixel size and camera length in the Optics panel.

---

## Registry reset

If settings become corrupted:

1. **Option → Reset registry (after restart)**
2. Restart ReciPro — window positions, wavelength, camera length, etc. will be reset to defaults

---

## Frequently asked questions

### Is there a Mac (or Linux) version?

No. ReciPro depends on the **.NET Desktop Runtime**, which currently runs on Windows only, so a cross-platform version is not available at present. It may become possible if that dependency becomes cross-platform in the future. (Issue [#12](https://github.com/seto77/ReciPro/issues/12))

### Does ReciPro run on Windows on ARM (ARM64)?

Yes. Recent versions run on ARM64 Windows with the .NET Desktop Runtime (x64) installed (confirmed working from around v4.913 with .NET 10). (Issue [#47](https://github.com/seto77/ReciPro/issues/47))

### How should I cite ReciPro?

Use the **Cite this repository** link on the [GitHub repository page](https://github.com/seto77/ReciPro) (metadata is provided by `CITATION.cff`). The preferred citation is:

> Seto, Y. & Ohtsuka, M. (2022). *J. Appl. Cryst.* **55**, 397–410. doi:[10.1107/S1600576722000139](https://doi.org/10.1107/S1600576722000139)

(Issue [#33](https://github.com/seto77/ReciPro/issues/33))

---

## Reporting bugs

Report issues at: <https://github.com/seto77/ReciPro/issues>

Please include:

- ReciPro version number
- Steps to reproduce the problem
- Any error messages or screenshots
