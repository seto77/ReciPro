# Third-party notices

This file summarizes third-party components, libraries, tools, and data sources that are bundled with or referenced by ReciPro. It is intended to support transparent redistribution and future code-signing review.

This file is not a substitute for the original license texts. For each dependency, refer to the upstream project or data provider for the authoritative license terms.

## Project license

ReciPro itself is distributed under the MIT License. See `LICENSE.md`.

## Status of this document

Last reviewed: 2026-05-30 (win-arm64 additions: 2026-06-12). This is a detailed inventory derived from the actual `.csproj` references, the installer (`ReciProSetup.vdproj`) bundle, the native build (`Crystallography.Native.vcxproj`), and the bundled/downloaded data. Items still requiring external verification are marked **`TODO: confirm`**.

Video export was migrated from a bundled GPL ffmpeg to the OS-provided **Windows Media Foundation** encoder (2026-05-30). As a result, **no ffmpeg / libx264 / libx265 / GPL binaries are bundled or distributed**, and the former GPL source-availability obligation no longer applies (see the [video encoding](#video-encoding-via-media-foundation) section). The COD citation has been added to the README and both manuals, the neutron scattering-length source has been updated to periodictable / Rauch data, and AMCSD bulk redistribution is permitted (permission obtained from one of the AMCSD maintainers). The remaining items are the minor **`TODO: confirm`** notes below (exact license texts, and the NIST SRD-64 / ITA citations).

**Update 2026-06-06:** xraylib 4.2.1 (BSD-3-Clause) was bundled as the native DLL `libxrl-11.dll` to provide X-ray physics reference data (anomalous-dispersion factors, photon attenuation / interaction cross sections, and fluorescence parameters) for the "Beam Interaction" window. It is a permissively-licensed third-party binary; see the [xraylib](#xraylib-bundled-third-party-native-data-library) entry under Native libraries and the not-re-signed note in [Code-signing note](#code-signing-note).

**Update 2026-06-12 (win-arm64):** an experimental native ARM64 portable package (`ReciPro-v.X-win-arm64-experimental-portable.zip`) is now published on the GitHub Releases page alongside the unchanged x64 artifacts. Because no upstream win-arm64 binaries exist for GLFW (the `OpenTK.redist.glfw` NuGet package has no win-arm64 asset; GLFW publishes no official arm64 Windows binaries) or for xraylib (conda-forge / vcpkg / MSYS2 all lack win-arm64), the win-arm64 `glfw3.dll` and `libxrl-11.dll` are **built from unmodified upstream release sources by this repository's CI** (`.github/workflows/build-native-deps-arm64.yml`, windows-11-arm runner, static CRT `/MT`) and committed under `ReciPro\native\win-arm64\` together with provenance files (`glfw3.arm64.PROVENANCE.txt`, `libxrl-11.arm64.PROVENANCE.txt`: source tarball URL + SHA256, build commands, output SHA256, Actions run URL). They remain third-party components under their upstream licenses. Both portable ZIPs (win-x64 and win-arm64) are single-file publishes: the managed third-party assemblies and the bundled `libxrl-11.dll` / `glfw3.dll` are embedded inside `ReciPro.exe` (extracted to a per-user cache at first run) rather than shipped as loose files; the MSI layout is unchanged.

## NuGet / managed libraries

These ship as managed assemblies inside the MSI (packaged through the ReciPro publish output) and are built from their respective upstream NuGet packages. No central package management is in use (no `Directory.Packages.props` / `Directory.Build.props` / `nuget.config`); versions are pinned inline per project and are consistent across projects for shared packages.

| Name | Version | Purpose | Bundled or downloaded | Upstream URL | License | Signed by ReciPro? | Notes |
|------|---------|---------|-----------------------|--------------|---------|--------------------|-------|
| BitMiracle.LibTiff.NET | 2.4.660 | Managed TIFF read/write | Bundled (DLL) | https://github.com/BitMiracle/libtiff.net | BSD-3-Clause (New BSD) — `TODO: confirm` exact text | No (third-party) | Used by ReciPro only. Not MIT. |
| IronPython | 3.4.2 | Python implementation for .NET (macro engine) | Bundled (DLL) | https://github.com/IronLanguages/ironpython3 | Apache-2.0 | No (third-party) | Used by ReciPro and Crystallography.Controls. 3.x is Apache-2.0 (older 2.x was MS-PL); include the `NOTICE` + Apache-2.0 text. |
| MathNet.Numerics | 6.0.0-beta2 | Numerical / scientific computing | Bundled (DLL) | https://numerics.mathdotnet.com/ | MIT | No (third-party) | Pre-release (beta) version. Used across all four shipping projects. |
| MathNet.Numerics.Providers.MKL | 6.0.0-beta2 | Math.NET MKL provider glue/shim | Bundled (managed shim DLL) | https://numerics.mathdotnet.com/ | MIT (glue only) | No (third-party) | Glue is MIT. The Intel MKL native package (`MathNet.Numerics.MKL.Win-x64`) was removed from the build (260405Cl); no native `mkl_rt.dll` is vendored or present in the build output. `TODO: confirm` whether an Intel Simplified Software License (ISSL) notice is required for any runtime-downloaded MKL native binaries. |
| OpenTK | 4.9.4 | OpenGL/OpenAL/OpenCL bindings | Bundled (DLL) | https://github.com/opentk/opentk | MIT | No (third-party) | Used across all four shipping projects. Ships `glfw3.dll` (zlib/libpng license) as a runtime native asset **for win-x64 only** (via `OpenTK.redist.glfw`, restored version 3.4.0.44); the win-arm64 build instead bundles a self-built `glfw3.dll` — see [GLFW](#glfw-bundled-native-windowing-library) under Native libraries. |
| SimdLinq | 1.3.2 | SIMD-accelerated LINQ (Cysharp) | Bundled (DLL) | https://github.com/Cysharp/SimdLinq | MIT | No (third-party) | Used across all four shipping projects. |
| ZLinq | 1.5.6 | Zero-allocation LINQ (Cysharp) | Bundled (DLL) | https://github.com/Cysharp/ZLinq | MIT | No (third-party) | Used across all four shipping projects. |
| MemoryPack | 1.21.4 | Zero-encoding binary serializer (Cysharp) | Bundled (DLL) | https://github.com/Cysharp/MemoryPack | MIT | No (third-party) | Used by Crystallography (`.cdb3` crystal-database serialization). |
| PureHDF | 2.1.2 | Pure C# HDF5 reader/writer | Bundled (DLL) | https://github.com/Apollo3zehn/PureHDF | MIT | No (third-party) | Used by Crystallography. |
| DynamicExpresso.Core | 2.19.3 | Runtime C#-like expression interpreter (symmetry-operation parsing) | Bundled (DLL) | https://github.com/dynamicexpresso/DynamicExpresso | MIT | No (third-party) | Used by Crystallography. Replaced `System.Linq.Dynamic.Core` (260427Cl). |
| WpfMath | 2.1.0 | WPF LaTeX math rendering (LabelTex/LabelLaTeX) | Bundled (DLL) | https://github.com/ForNeVeR/wpf-math | MIT | No (third-party) | Used by Crystallography.Controls. |
| System.Management | 10.0.8 | WMI access (Microsoft official package) | Bundled (DLL) | https://github.com/dotnet/runtime | MIT | No (third-party) | Used by Crystallography.OpenGL. |

Target framework: `net10.0-windows` for all four shipping projects (ReciPro, Crystallography, Crystallography.Controls, Crystallography.OpenGL).

Notes on inventory completeness:

- The vendored `OpenTK.GLControl` subproject (under `Crystallography.OpenGL\OpenTK.GLControl\`) is OpenTK's `GLControl` WinForms control source (https://github.com/opentk/GLControl, MIT, © Team OpenTK), copied into the repo. It references `OpenTK.Graphics 4.8.2` and `OpenTK.Windowing.Desktop 4.8.2`, but the subproject is **not** referenced by any shipping project and is not in the shipping build graph. It is covered by the OpenTK (MIT) attribution above.
- Removed / commented-out packages that are **not** active dependencies and must not be listed: `MathNet.Numerics.MKL.Win-x64` (removed 260405Cl), `NativeMemoryArray` 1.2.2 (removed 260407Cl), `SixLabors.ImageSharp` 4.0.0 (removed 260513Cl), `System.Linq.Dynamic.Core` (replaced 260427Cl), and a legacy `ImagingSolution.Control.GraphicsBox` direct DLL reference (commented out).
- No SQLite dependency: there is no `Community.CsharpSqlite` or other SQLite reference in any `.csproj`/`.cs` (the crystal database uses the Brotli + MemoryPack `.cdb3` format).

## Native libraries

The in-house native library `Crystallography.Native.dll` (and its `*.avx2.dll` / `*.avx512.dll` variants; on win-arm64 a single NEON-optimized build with no AVX flavors) is built from this repository (`Crystallography.Native.vcxproj`, C++/Eigen) and statically embeds the vendored Eigen headers.

| Name | Version | Purpose | Bundled or downloaded | Upstream URL | License | Signed by ReciPro? | Notes |
|------|---------|---------|-----------------------|--------------|---------|--------------------|-------|
| Eigen | 3.5.0.1 (post-3.4 dev/master snapshot) | Linear algebra (compiled into `Crystallography.Native.dll`) | Bundled as vendored header-only source; compiled in, not shipped as a separate library | https://eigen.tuxfamily.org/ | MPL-2.0 | N/A (compiled into the ReciPro-built native DLL, which **is** signed by ReciPro) | Version from `Eigen/Version` (WORLD 3, MAJOR 5, MINOR 0, PATCH 1). Preserve the MPL-2.0 notice. Optional LAPACKE/MKL Eigen backends in the header tree are inert (`EIGEN_USE_MKL` / `EIGEN_USE_BLAS` / `EIGEN_USE_LAPACKE` are never defined in the project sources). |
| Intel MKL | — | (not used) | Not bundled | — | — | — | **Not used and not redistributed.** `Crystallography.Native.vcxproj` sets `UseIntelMKL=No` (both configs), `UseInteloneMKL=No`, `UseIntelIPP/DAAL/TBB=false`; no MKL `.lib` linker inputs; no `mkl_rt.dll` in the build output. The managed `MathNet.Numerics.Providers.MKL.dll` shim is present but no native MKL runtime is loaded or shipped. No MKL obligation. |

Other native runtime DLL that ships: `glfw3.dll` (zlib/libpng license) — see the next section for its per-architecture provenance. (The previously-bundled ffmpeg / x264 / x265 / zlib / MinGW-runtime DLLs were removed on 2026-05-30 — see the [video encoding](#video-encoding-via-media-foundation) section.)

### GLFW (bundled native windowing library)

`glfw3.dll` provides window/context creation for the OpenGL-based 3D views (via OpenTK). Its provenance differs per architecture:

| Architecture | Origin | Notes |
|---|---|---|
| win-x64 | `OpenTK.redist.glfw` NuGet runtime asset (restored version 3.4.0.44) | Upstream prebuilt binary, redistributed unchanged. |
| win-arm64 | **Built from unmodified upstream GLFW 3.4 release source by this repository's CI** (`.github/workflows/build-native-deps-arm64.yml`, windows-11-arm runner, CMake, static CRT `/MT`), because `OpenTK.redist.glfw` has no win-arm64 asset and GLFW publishes no official arm64 Windows binaries (glfw/glfw#2163). Committed at `ReciPro\native\win-arm64\glfw3.dll`. | Full provenance (source tarball URL + SHA256, build command, output SHA256, Actions run URL) in `ReciPro\native\win-arm64\glfw3.arm64.PROVENANCE.txt`; the zlib/libpng license text ships as `glfw3.LICENSE.txt`. |

In both cases GLFW (https://www.glfw.org/, zlib/libpng license) remains a third-party component and is not re-signed or relicensed as ReciPro's MIT code; building it from unmodified upstream source in this repository's CI does not make it ReciPro-maintained code (see [Code-signing note](#code-signing-note)).

### xraylib (bundled, third-party native data library)

A standalone, permissively-licensed third-party native DLL that embeds X-ray physics reference-data tables. The win-x64 binary is **not** built from this repository (it comes from conda-forge); the win-arm64 binary is built from unmodified upstream source by this repository's CI. In both cases it remains a third-party component and is **not** re-signed by ReciPro.

| Name | Version | Purpose | Bundled or downloaded | Upstream URL | License | Signed by ReciPro? | Notes |
|------|---------|---------|-----------------------|--------------|---------|--------------------|-------|
| xraylib | 4.2.1 (soname `libxrl-11`) | X-ray physics reference data for the "Beam Interaction" window: anomalous-dispersion factors (f′, f″), photon mass attenuation / interaction cross sections (total / photoelectric / Rayleigh / Compton / energy-absorption), atomic form and incoherent-scattering functions, fluorescence yields, characteristic-line energies and radiative rates, and absorption-edge energies and jump ratios. | **Bundled** — `libxrl-11.dll` (~14.8 MB; physical data tables embedded in the DLL). Shipped via RID-conditional `ReciPro.csproj` `<Content>` items with `CopyToOutputDirectory` + `CopyToPublishDirectory=PreserveNewest` (self-contained publish does not otherwise pick up native DLLs): the repo-root `libxrl-11.dll` for win-x64, `native\win-arm64\libxrl-11.dll` for win-arm64. In the single-file portable ZIPs it is embedded inside `ReciPro.exe`. | https://github.com/tschoonj/xraylib | BSD-3-Clause | No (third-party) | Added 260606. **win-x64**: obtained from the conda-forge win-64 package (`xraylib-4.2.1`) and renamed from `xrl-11.dll` to `libxrl-11.dll` to match the upstream soname convention (the DLL does not reference its own name); runtime dependencies are `VCRUNTIME140.dll` + UCRT + `KERNEL32` only (no conda-specific dependencies). **win-arm64**: no prebuilt binary exists (conda-forge / vcpkg / MSYS2 all lack win-arm64), so the DLL is built from the unmodified upstream 4.2.1 release tarball by this repository's CI (`build-native-deps-arm64.yml`, meson, static CRT `/MT`, `meson test` passing) with the same rename; committed at `ReciPro\native\win-arm64\libxrl-11.dll`, full provenance in `ReciPro\native\win-arm64\libxrl-11.arm64.PROVENANCE.txt`. The BSD-3-Clause license text ships alongside as `libxrl-11.LICENSE.txt`. Called from `Crystallography/Xraylib.cs` (P/Invoke, `ExactSpelling`). Recommended citation: T. Schoonjans et al. (2011) *Spectrochim. Acta Part B* **66**, 776-784, https://doi.org/10.1016/j.sab.2011.09.011. |

## Video encoding via Media Foundation

ReciPro previously bundled a **GPL** ffmpeg build (linking libx264 / libx265) for rotation-animation video export, loaded through the FFMediaToolkit wrapper. **This was removed on 2026-05-30.** Video export now uses the operating system's built-in **Windows Media Foundation** H.264 / H.265 encoder (`MediaFoundationVideoEncoder` in `Crystallography.Controls`, via direct `mfplat.dll` / `mfreadwrite.dll` P/Invoke — no third-party library).

Consequences:

- **No ffmpeg, libx264, libx265, zlib, or MinGW-runtime DLLs are bundled or distributed.** The `ReciPro\ffmpeg\` folder (11 DLLs) and the `FFMediaToolkit` NuGet reference were deleted from the project.
- **No GPL obligation.** The former source-availability requirement (from the GPL x264/x265 build) no longer applies.
- **H.264/H.265 codec patent licensing** is carried by the operating system (the OS-provided Media Foundation encoder is invoked, rather than a codec binary that ReciPro distributes).
- The Media Foundation interop code is ReciPro-authored (MIT) and built from this repository, so it is in the normal ReciPro signing scope — there is no longer any foreign codec binary to keep unsigned.

## Bundled and downloaded crystallographic data

### AMCSD (bundled)

| Item | Detail |
|------|--------|
| Name | American Mineralogist Crystal Structure Database (AMCSD) |
| Bundled or downloaded | **Bundled** — `AMCSD.cdb3` (~5.3 MB; single-file Brotli + MemoryPack compressed; proprietary `.cdb3` format, not raw AMC/CIF). Shipped via `ReciPro.csproj` `<Content Include="AMCSD.cdb3">` and copied to the user app-data folder at startup. |
| Content | "Over 21,000 crystal structures" (README); AMCSD DB last updated 2025/12/10 (per `Version.cs`). |
| Upstream URL | http://rruff.geo.arizona.edu/AMS/amcsd.php |
| Required citation | Downs, R. T. & Hall-Wallace, M. (2003). The American Mineralogist Crystal Structure Database. *American Mineralogist*, **88**, 247-250. (Already cited in README, both manuals, and this file.) |
| Signed by ReciPro? | N/A (data file, not code) — ships inside the ReciPro-signed installer. |
| Redistribution status | **Permitted.** Redistribution permission was obtained from one of the AMCSD maintainers. The underlying crystal-structure data are scientific facts published in peer-reviewed journals and are openly distributed by AMCSD/RRUFF (e.g. https://www.rruff.net/). Attribution is provided via the Downs & Hall-Wallace (2003) citation. The `.cdb3` carries no embedded attribution; attribution is satisfied via README/manuals/this file (an in-app citation on the Crystal Database window would further strengthen it). |

### COD (downloaded on first use)

| Item | Detail |
|------|--------|
| Name | Crystallography Open Database (COD) |
| Bundled or downloaded | **Downloaded on first use**, NOT bundled. ~880 MB (~525,000 structures), fetched on first COD use into the user app-data folder. Same proprietary `.cdb3` (Brotli + MemoryPack) format as AMCSD, re-packaged by the author. |
| Download source | The **author's own GitHub mirror**: `https://github.com/seto77/CSManager/raw/master/COD/` (downloads `COD.cdb3` manifest, then parallel-downloads split parts `COD/COD.000 … COD.NNN`). Not streamed live from crystallography.net. |
| Upstream URL | https://www.crystallography.net/cod/ |
| License | COD data are released to the **public domain** / open terms; redistribution (including the author's CSManager mirror) is **permitted**. |
| Required citation | Added (2026-05-30) to README and both manuals: Gražulis et al. (2009) *J. Appl. Cryst.* **42**, 726-729 (https://doi.org/10.1107/S0021889809016690); Gražulis et al. (2012) *Nucleic Acids Research* **40**, D420-D427 (https://doi.org/10.1093/nar/gkr900). COD lists further papers (e.g. Vaitkus et al. 2021) on its citation page; `TODO: confirm` whether to include the full recommended set. |
| Signed by ReciPro? | N/A (downloaded data, not part of the signed installer). |
| Redistribution status | Permitted (public domain). `TODO: confirm` the redistribution/license note for the specific repackaged CSManager mirror snapshot (date/version), that it preserves COD's public-domain status, and that no individual COD entries carry non-redistributable depositor-specified licenses. |

### Hardcoded scientific data (compiled into the Crystallography core source — factual/public-domain, attribution by citation)

| Data | Source / citation | Status |
|------|-------------------|--------|
| X-ray atomic scattering factors | Waasmaier & Kirfel (1995) *Acta Cryst.* **A51**, 416-431 (also RHF/HF analytic forms, International Tables-style) | Cited in `AtomStatic.cs`. |
| Electron scattering factors | Peng et al. (1996, 1998) | Cited in `AtomStatic.cs`. |
| Neutron coherent scattering lengths | Updated (2026-05-31) from the `periodictable` 2.1.0 neutron scattering table (`b_c`, fm), which is reproduced from Rauch, H. & Waschkowski, W. (2003). "Neutron Scattering Lengths" in *ILL Neutron Data Booklet*, 2nd ed., A.-J. Dianoux & G. Lander (eds.), pp. 1.1-1 to 1.1-17, with newer corrections/measurements noted by the `periodictable` project. Complex imaginary parts are derived in the same way as `periodictable`: `b_c - i sigma_a/(2000 lambda)` at lambda = 1.798 Å. See https://periodictable.readthedocs.io/en/latest/api/nsf.html. | Compiled into `AtomStatic.cs`; no runtime dependency on `periodictable`. Previous Sears (1992) / NIST NCNR values were replaced to avoid mixing old and newer recommended tables. |
| X-ray mass / linear absorption coefficients | NIST FFAST (Chantler), http://www.nist.gov/pml/data/ffast/index.cfm | Public-domain US-gov data; attribution recommended. |
| Elastic electron-scattering sampler | NIST Electron Elastic-Scattering Cross-Section Database (SRD 64) — `TODO: confirm` exact NIST SRD citation. | Compiled-in PCHIP form only; raw `E_*.TXT/E_*.BIN` are dev-only and NOT shipped. |
| Space-group / symmetry tables (530 settings: 230 ITA + 300 non-standard) | Derived from International Tables for Crystallography (ITA) — factual data; attribution to ITA recommended. `TODO: confirm`. | Hardcoded in core library source. |

## Fonts, icons, images, and sample data

| Category | Detail | Third-party attribution |
|----------|--------|--------------------------|
| Fonts | **No fonts (.ttf/.otf) are bundled or shipped** with the app. UI relies on OS-installed fonts (English = Segoe UI, Japanese = Yu Gothic UI). | None required. The only `.ttf` files in the repo tree (Font Awesome, SIL OFL 1.1) live in the MkDocs docs build virtualenv (`docs/.venv-docs`) and are used for the GitHub Pages website only — not in the installer. `TODO: confirm` `docs/.venv-docs` is gitignored and never packaged into the MSI. |
| Icons | App and per-window icons (`App.ico`, `Kaeru.ico`, `Stereo.ico`, `Structure.ico`, `electron.ico`, `EBSD.ico`) appear to be **ReciPro's own original artwork**. `App.ico` is embedded in the exe; no standalone `.ico` is shipped. No third-party icon sets are embedded in the app. | None found. |
| Images | UI / equation / diagram PNGs in `ReciPro\image\` appear to be **ReciPro's own**, generated from `image\Equations.pptx` / `アイコン.cvx`. | None found. `TODO: confirm` none of the equation/diagram PNGs were copied from a third-party publication. |
| Sample data | `initial.xml` (~66 default crystals seeded on first run) — ReciPro's own format/content, author-created. Shipped via `ReciPro.csproj`. | None required. |
| Demo media | `img\recipro_demo.mp4` (~10 MB) — ReciPro's own. `TODO: confirm` whether it ships (not referenced in csproj `Content`; appears not to be packaged). | None required. |
| Manuals / PDFs | `doc\ReciProManual(en).pdf`, `doc\ReciProManual(ja).pdf`, `doc\Bethe.pdf`, `doc\Hrtem.pdf` — ReciPro's own content, shipped. | None required. |

## Code-signing note

The intended code-signing scope is limited to ReciPro release artifacts and binaries **built from this repository**. ReciPro signs:

- `ReciPro.exe` and `apphost.exe`;
- the in-house managed assemblies `Crystallography.dll`, `Crystallography.Controls.dll`, `Crystallography.OpenGL.dll` (and their satellite resource DLLs);
- the in-house native DLLs `Crystallography.Native.dll`, `Crystallography.Native.avx2.dll`, `Crystallography.Native.avx512.dll` (these compile in MPL-2.0 Eigen, which remains compatible).

Third-party binaries must **not** be re-signed as if they were maintained by ReciPro. In particular:

- The redistributed **NuGet runtime DLLs** (OpenTK and its win-x64 `glfw3.dll`, MathNet.Numerics, IronPython, BitMiracle.LibTiff.NET, SimdLinq, ZLinq, MemoryPack, PureHDF, DynamicExpresso.Core, WpfMath, System.Management, etc.) keep their own upstream provenance and licenses as listed above and must not be re-signed or relicensed as ReciPro's MIT code.
- The bundled **xraylib** native DLL (`libxrl-11.dll`, BSD-3-Clause) is a third-party component and must not be re-signed or relicensed as ReciPro's MIT code. This applies to both the win-x64 binary (obtained from conda-forge) and the win-arm64 binary (compiled from unmodified upstream source by this repository's CI — compiling third-party source in CI does not make it ReciPro-maintained code; see the PROVENANCE file).
- The win-arm64 **GLFW** DLL (`glfw3.dll`, zlib/libpng), likewise compiled from unmodified upstream source by this repository's CI, is a third-party component under the same rule.

The MSI itself is not signed by Visual Studio (`SignOutput=FALSE` in the vdproj); the MSI is x64-only and its build pipeline is pinned to `win-x64`, so no non-Windows/x86 runtime assets are emitted there. Release artifacts now also include single-file portable ZIPs (win-x64, and the **experimental** win-arm64 attached post-release after an on-runner smoke test): the in-house single-file `ReciPro.exe` of the win-x64 package is in the normal ReciPro signing scope, the experimental win-arm64 package may remain unsigned until it graduates from experimental status (after which it enters the same scope), and in both packages the third-party libraries embedded inside the single-file bundle retain their own provenance and are not individually re-signed. See `CODE_SIGNING.md` for the release-artifact signing policy. This notice is not a substitute for the upstream license texts — refer to each upstream project or data provider for authoritative terms.
