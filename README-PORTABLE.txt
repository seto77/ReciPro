ReciPro portable ZIP package (260602Cl)
=======================================

The MSI installer is the recommended installation method for ReciPro.
This portable ZIP package is provided as an alternative for managed Windows
PCs where MSI installation, administrator approval, or separate .NET Desktop
Runtime installation is difficult. In this document, "portable" means
"no installer required"; ReciPro still uses the current user's AppData
folder for settings and copied default data. (260602Cl)

How to run
----------

1. Extract the ZIP file to a user-writable folder.
   Example: Documents\ReciPro or Desktop\ReciPro

2. Run ReciPro.exe from the extracted ReciPro folder.

3. Do not run ReciPro.exe directly from inside the ZIP viewer.
   Extract the full folder first so that the bundled DLLs (including the
   native compute libraries Crystallography.Native*.dll), the crystal
   database (AMCSD.cdb3), localization files, and manuals remain next to
   ReciPro.exe.

Runtime
-------

This portable package is self-contained for Windows x64. A separate .NET
Desktop Runtime 10 installation is not required; the required .NET runtime
files are bundled in this folder. When Microsoft releases .NET runtime
security updates, this package should be rebuilt and redistributed so that
the bundled runtime is also updated. (260602Cl)

The bundled native libraries ship in three flavors (generic / AVX2 / AVX512);
ReciPro automatically selects the fastest one supported by your CPU at
startup. Structure Viewer and related 3D views require OpenGL provided by
your graphics driver; ReciPro still starts and runs the other functions even
when OpenGL initialization fails.

Notes for managed PCs
---------------------

- Administrator privileges are not required by ReciPro itself.
- ReciPro stores user settings and copied default data under the current
  user's application data folder.
- ReciPro may also store per-user options under HKCU
  (HKEY_CURRENT_USER\Software\Crystallography\ReciPro).
- The in-app "Check Updates" menu is hidden in the portable package, because
  MSI-based updating does not apply here. To update, download the latest
  portable ZIP from the official GitHub Releases page.
- Windows Defender SmartScreen or institutional security software may still
  warn about newly downloaded unsigned research software. Download ReciPro
  only from the official GitHub Releases page:
  https://github.com/seto77/ReciPro/releases/latest

Verification
------------

If SHA256SUMS.txt is provided with the release, you can verify the downloaded
ZIP file in PowerShell:

  Get-FileHash .\ReciPro-*-win-x64-portable.zip -Algorithm SHA256
