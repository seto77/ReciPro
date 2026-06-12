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

### Is there a Mac (or Linux) version? {#mac-linux}

There is no official Mac or Linux version. ReciPro depends on the **.NET Desktop Runtime**, which currently runs on Windows only. (Issue [#12](https://github.com/seto77/ReciPro/issues/12))

However, an unofficial route has been reported to work on macOS: the **win-x64 portable ZIP** distribution (available on the [releases page](https://github.com/seto77/ReciPro/releases/latest)) runs on macOS (Apple Silicon) using the **Sikarugir** Wine wrapper combined with the **Mesa3D** OpenGL driver — no Windows license or virtual machine required. A step-by-step setup guide published by a user is available at <https://github.com/Ryo-fkushima/ReciPro_macOS_memo>.

Note that this configuration is not officially supported or fully verified. A known limitation is that some symbols (Å, superscripts, arrows) may be displayed incorrectly.

**Fixing the garbled symbols (Å, superscripts, arrows):** The cause is that the Windows fonts ReciPro normally uses (Segoe UI, Yu Gothic UI, etc.) are missing from the Wine environment, and Wine's built-in substitute fonts lack some scientific glyphs. ReciPro automatically switches to widely-covered fonts **when it detects that it is running under Wine**, so the fix is simply to make those fonts available in the Wine prefix:

1. Install **DejaVu Sans** / **DejaVu Serif** (covers Å, superscripts, arrows, fraction labels) and, for the Japanese UI, **Noto Sans CJK JP** (or **Noto Sans JP**).
2. The simplest way is to copy the downloaded `.ttf`/`.otf` files into the prefix's font folder — `…/drive_c/windows/Fonts/` inside the Sikarugir wrapper — then relaunch ReciPro. (`winetricks` can also install some of these.)
3. On restart ReciPro picks them up automatically; no ReciPro setting needs to be changed.

If the fonts are not installed, ReciPro keeps its default font names, so nothing gets worse — the symbols simply remain garbled.

**Outlook for this route — two honest notes:**

- The experimental **win-arm64** ZIP does **not** run on current Macs, even on Apple Silicon: today's macOS Wine builds (including Sikarugir) execute x86_64 Windows binaries through Rosetta 2 and have no mechanism to run ARM64 Windows binaries. On a Mac, always use the **win-x64** portable ZIP.
- Apple is phasing out Rosetta 2. macOS 27 (autumn 2026) is announced as the last version with full Rosetta 2 support, so the current x64 + Rosetta route is expected to stop working from macOS 28 (autumn 2027). A native ARM64 Wine for macOS is under development upstream; if it materializes, the win-arm64 ZIP may become the successor on Mac, but that cannot be promised yet.

### Does ReciPro run on Windows on ARM (ARM64)? {#windows-on-arm}

Yes — there are two routes:

- **Native ARM64 package (experimental, recommended)**: from v4.938, an experimental native ARM64 portable package (`ReciPro-v.X-win-arm64-experimental-portable.zip`) is published on the [releases page](https://github.com/seto77/ReciPro/releases/latest). It is self-contained, so no .NET Runtime installation is required — extract the ZIP to a user-writable folder and run `ReciPro.exe`. If Windows blocks the downloaded ZIP (Mark of the Web), right-click the ZIP → **Properties** → check **Unblock** → **OK** *before* extracting (or run `Unblock-File .\ReciPro-*-win-arm64-experimental-portable.zip` in PowerShell). Details are in the bundled `README-PORTABLE.txt`.
- **x64 package under emulation**: the regular MSI installer and the win-x64 portable ZIP also run on ARM64 Windows through the built-in x64 emulation, with the .NET Desktop Runtime (x64) installed (confirmed from around v4.913 with .NET 10). Heavy computations run slower than the native build. (Issue [#47](https://github.com/seto77/ReciPro/issues/47))

Notes on the native ARM64 package:

- Intel MKL does not exist for ARM64, so the corresponding solver options and menu items are hidden. Dynamical calculations use the bundled NEON-optimized native library; in representative validation cases its results matched the x64 build within the expected floating-point tolerance.
- 3D views (Structure Viewer and related windows) can run, but Windows on ARM provides OpenGL only through a Direct3D 12 translation layer (GLOn12 / Mesa), so 3D rendering is noticeably slower than on a PC with a native OpenGL driver — this is a platform limitation, not a bug, and a native ARM64 build cannot change it. The **High quality (Per-Pixel Linked List)** transparency mode in Structure Viewer is particularly slow on this driver stack; the default **Approximate** mode is recommended. If 3D views fail to start, install "OpenCL, OpenGL, and Vulkan Compatibility Pack" from the Microsoft Store.
- The ARM64 package does **not** run on macOS + Wine (see the previous question). On a Mac, use the win-x64 portable ZIP.

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
