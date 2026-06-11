ReciPro portable ZIP package for Windows on ARM (experimental) (260611Cl)
==========================================================================

This package is an EXPERIMENTAL native ARM64 build of ReciPro for
Windows on ARM devices (for example, Snapdragon-based Surface PCs).
On ordinary x64 PCs, use the MSI installer or the win-x64 portable ZIP
instead. In this document, "portable" means "no installer required";
ReciPro still uses the current user's AppData folder for settings and
copied default data.

How to run
----------

1. Extract the ZIP file to a user-writable folder.
   Example: Documents\ReciPro or Desktop\ReciPro

2. If Windows blocks the downloaded file (Mark of the Web), right-click
   the downloaded ZIP -> Properties -> check "Unblock" -> OK, before
   extracting. In PowerShell the same can be done with:

     Unblock-File .\ReciPro-*-win-arm64-experimental-portable.zip

   If you already extracted a blocked ZIP, unblock the extracted files:

     Get-ChildItem .\ReciPro -Recurse | Unblock-File

3. Run ReciPro.exe from the extracted ReciPro folder.
   Do not run ReciPro.exe directly from inside the ZIP viewer; extract
   the full folder first so that the bundled DLLs, the crystal database
   (AMCSD.cdb3), localization files, and manuals remain next to
   ReciPro.exe.

Runtime
-------

This portable package is self-contained for Windows ARM64. A separate
.NET Desktop Runtime installation is not required. When Microsoft
releases .NET runtime security updates, this package should be rebuilt
and redistributed so that the bundled runtime is also updated.

The bundled native compute library ships as a single NEON-optimized
binary (Crystallography.Native.dll). Unlike the x64 package there are
no AVX2 / AVX512 flavors; this is expected and not an error.

Intel MKL is not available on ARM64; the corresponding solver options
and menu items are hidden in this build.

3D views (Structure Viewer and related windows) use OpenGL, which
Windows on ARM provides through a Direct3D 12 translation layer
(GLOn12 / Mesa). If 3D views fail to start, install "OpenCL, OpenGL,
and Vulkan Compatibility Pack" from the Microsoft Store. The
"High quality (Per-Pixel Linked List)" transparency mode in Structure
Viewer is known to be slow on this driver stack; the default
"Approximate" mode is recommended.

Notes
-----

- This ARM64 build does NOT run on macOS + Wine (current macOS Wine
  builds run x86_64 Windows binaries only). On Mac, use the win-x64
  portable ZIP.
- Administrator privileges are not required by ReciPro itself.
- ReciPro stores user settings and copied default data under the
  current user's application data folder, and per-user options under
  HKCU (HKEY_CURRENT_USER\Software\Crystallography\ReciPro). These
  registry settings are shared with x64 builds of ReciPro running on
  the same PC (under emulation).
- The in-app "Check Updates" menu is hidden in the portable package.
  To update, download the latest ARM64 package from the official
  ReciPro GitHub repository.
- Windows Defender SmartScreen or institutional security software may
  warn about newly downloaded unsigned research software. Download
  ReciPro only from the official GitHub repository (the Releases page,
  or its Actions artifacts while this ARM64 package is experimental):
  https://github.com/seto77/ReciPro

Verification
------------

If SHA256SUMS-win-arm64-experimental.txt is provided alongside this
package, you can verify the downloaded ZIP file in PowerShell:

  Get-FileHash .\ReciPro-*-win-arm64-experimental-portable.zip -Algorithm SHA256
