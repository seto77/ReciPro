# Troubleshooting

Common issues and solutions for ReciPro.

---

## OpenGL issues

### Symptom: Black screen or crash on startup

**Cause**: Incompatible GPU or remote desktop environment.

**Solution**:
1. Go to **Option → Disable OpenGL (needs restart)**
2. Restart ReciPro
3. Structure Viewer and some 3D features will use software rendering

### Symptom: Poor rendering quality

**Solution**: Update your GPU drivers. An external (discrete) GPU with OpenGL 1.5 support is recommended.

---

## .NET Runtime

### Symptom: Application won't start

**Cause**: .NET Desktop Runtime 10.0 is not installed.

**Solution**: Download and install from https://dotnet.microsoft.com/download/dotnet/10.0

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

## File I/O

### Symptom: CIF file won't load

**Solution**:
- Check that the CIF file is well-formed
- Try dragging and dropping the file onto the **Crystal Information** area
- Some non-standard CIF extensions may not be supported

### Symptom: dm3/dm4 file shows wrong scale

**Solution**: Verify calibration in the original Digital Micrograph software. ReciPro reads the embedded metadata; if the metadata is incorrect, manually set the pixel size and camera length in the Optics panel.

---

## Registry reset

If settings become corrupted:
1. **Option → Reset registry (after restart)**
2. Restart ReciPro — window positions, wavelength, camera length, etc. will be reset to defaults

---

## Reporting bugs

Report issues at: https://github.com/seto77/ReciPro/issues

Please include:
- ReciPro version number
- Steps to reproduce the problem
- Any error messages or screenshots
