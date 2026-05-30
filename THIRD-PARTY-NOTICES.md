# Third-party notices

This file summarizes third-party components, libraries, tools, and data sources that are bundled with or referenced by ReciPro. It is intended to support transparent redistribution and future code-signing review.

This file is not a substitute for the original license texts. For each dependency, refer to the upstream project or data provider for the authoritative license terms.

## Project license

ReciPro itself is distributed under the MIT License. See `LICENSE.md`.

## Status of this document

Last reviewed: 2026-05-30. This is a detailed inventory derived from the actual `.csproj` references, the installer (`ReciProSetup.vdproj`) bundle, the native build (`Crystallography.Native.vcxproj`), and the bundled/downloaded data. Items still requiring external verification are marked **`TODO: confirm`**.

The most important open item is **ffmpeg**: the bundled build is **GPL** (it links libx264 and libx265), which imposes source-availability obligations on the combined distribution. See the [ffmpeg](#ffmpeg--gpl-build-important-source-availability-obligation) section before the next signed release. (The COD citation has since been added to the README and both manuals, and AMCSD bulk redistribution is permitted — permission obtained from one of the AMCSD maintainers.)

## NuGet / managed libraries

These ship as managed assemblies inside the MSI (packaged through the ReciPro publish output) and are built from their respective upstream NuGet packages. No central package management is in use (no `Directory.Packages.props` / `Directory.Build.props` / `nuget.config`); versions are pinned inline per project and are consistent across projects for shared packages.

| Name | Version | Purpose | Bundled or downloaded | Upstream URL | License | Signed by ReciPro? | Notes |
|------|---------|---------|-----------------------|--------------|---------|--------------------|-------|
| BitMiracle.LibTiff.NET | 2.4.660 | Managed TIFF read/write | Bundled (DLL) | https://github.com/BitMiracle/libtiff.net | BSD-3-Clause (New BSD) — `TODO: confirm` exact text | No (third-party) | Used by ReciPro only. Not MIT. |
| FFMediaToolkit | 4.8.1 | Managed .NET wrapper over FFmpeg (rotation-animation video encoding) | Bundled (DLL) | https://github.com/radek-k/FFMediaToolkit | MIT | No (third-party) | Wrapper only. The native FFmpeg DLLs it loads are **GPL** — see the ffmpeg section. Ships with `FFmpeg.AutoGen.dll`. |
| IronPython | 3.4.2 | Python implementation for .NET (macro engine) | Bundled (DLL) | https://github.com/IronLanguages/ironpython3 | Apache-2.0 | No (third-party) | Used by ReciPro and Crystallography.Controls. 3.x is Apache-2.0 (older 2.x was MS-PL); include the `NOTICE` + Apache-2.0 text. |
| MathNet.Numerics | 6.0.0-beta2 | Numerical / scientific computing | Bundled (DLL) | https://numerics.mathdotnet.com/ | MIT | No (third-party) | Pre-release (beta) version. Used across all four shipping projects. |
| MathNet.Numerics.Providers.MKL | 6.0.0-beta2 | Math.NET MKL provider glue/shim | Bundled (managed shim DLL) | https://numerics.mathdotnet.com/ | MIT (glue only) | No (third-party) | Glue is MIT. The Intel MKL native package (`MathNet.Numerics.MKL.Win-x64`) was removed from the build (260405Cl); no native `mkl_rt.dll` is vendored or present in the build output. `TODO: confirm` whether an Intel Simplified Software License (ISSL) notice is required for any runtime-downloaded MKL native binaries. |
| OpenTK | 4.9.4 | OpenGL/OpenAL/OpenCL bindings | Bundled (DLL) | https://github.com/opentk/opentk | MIT | No (third-party) | Used across all four shipping projects. Ships `glfw3.dll` (zlib/libpng license) as a runtime native asset. |
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

The in-house native library `Crystallography.Native.dll` (and its `*.avx2.dll` / `*.avx512.dll` variants) is built from this repository (`Crystallography.Native.vcxproj`, C++/Eigen) and statically embeds the vendored Eigen headers.

| Name | Version | Purpose | Bundled or downloaded | Upstream URL | License | Signed by ReciPro? | Notes |
|------|---------|---------|-----------------------|--------------|---------|--------------------|-------|
| Eigen | 3.5.0.1 (post-3.4 dev/master snapshot) | Linear algebra (compiled into `Crystallography.Native.dll`) | Bundled as vendored header-only source; compiled in, not shipped as a separate library | https://eigen.tuxfamily.org/ | MPL-2.0 | N/A (compiled into the ReciPro-built native DLL, which **is** signed by ReciPro) | Version from `Eigen/Version` (WORLD 3, MAJOR 5, MINOR 0, PATCH 1). Preserve the MPL-2.0 notice. Optional LAPACKE/MKL Eigen backends in the header tree are inert (`EIGEN_USE_MKL` / `EIGEN_USE_BLAS` / `EIGEN_USE_LAPACKE` are never defined in the project sources). |
| Intel MKL | — | (not used) | Not bundled | — | — | — | **Not used and not redistributed.** `Crystallography.Native.vcxproj` sets `UseIntelMKL=No` (both configs), `UseInteloneMKL=No`, `UseIntelIPP/DAAL/TBB=false`; no MKL `.lib` linker inputs; no `mkl_rt.dll` in the build output. The managed `MathNet.Numerics.Providers.MKL.dll` shim is present but no native MKL runtime is loaded or shipped. No MKL obligation. |

Other native runtime DLLs that ship (detailed in the ffmpeg section, listed here for completeness): `zlib1.dll` (zlib license), `libgcc_s_seh-1.dll` and `libstdc++-6.dll` (GCC runtime, GPLv3 + GCC Runtime Library Exception), `libwinpthread-1.dll` (mingw-w64 winpthreads, permissive MIT/BSD/zlib-style), and `glfw3.dll` (zlib/libpng, via OpenTK).

## ffmpeg — GPL build (IMPORTANT: source-availability obligation)

> **GPL ALERT.** The bundled ffmpeg is a **GPL** build. It links **libx264** and **libx265**, both GPL-2.0-or-later, which forces ffmpeg to be configured `--enable-gpl`. Distributing these DLLs makes the **combined ReciPro distribution** subject to the GPL for the ffmpeg components. ReciPro's own code remains MIT, but bundling a GPL ffmpeg imposes GPL obligations on the aggregate distribution.
>
> **Required actions before the next signed release:**
> 1. **Include the GPL/COPYING license texts and copyright notices** for ffmpeg, x264, and x265 with the distribution (currently **absent** next to the DLLs and not mentioned in `LICENSE.md`).
> 2. **Convey or offer the complete corresponding source** for the ffmpeg/x264/x265 components (upstream source plus the exact build/configure scripts), e.g. include the source or a valid written offer under GPL terms.
> 3. **Do NOT re-sign, rename, or relicense the ffmpeg binaries** as ReciPro's own MIT code. They must remain identifiable, unmodified GPL ffmpeg. ReciPro's Authenticode signing must not treat them as ReciPro-authored.
> 4. **State in the docs/installer** that the bundled ffmpeg is GPL.
>
> **Lower-friction alternative:** switch to an **LGPL** ffmpeg build that drops libx264/libx265 (omit those encoders or use an LGPL-compatible H.264/H.265 path). That removes the GPL source-offer obligation while keeping ReciPro's distribution clean. `TODO: decide` between GPL-compliance vs. switching to an LGPL build.

The native FFmpeg DLLs are bundled via `ReciPro.csproj` (`<Content Include="ffmpeg\*.dll" CopyToOutputDirectory=PreserveNewest>`), copied to `bin\Release\ffmpeg\`, and packaged into the MSI's `ffmpeg\` folder through the ReciPro publish output group (not enumerated individually in the vdproj). They are loaded in-process via `NativeLibrary.Load` / `FFmpegLoader.FFmpegPath` from `FormMovie.cs` through the FFMediaToolkit wrapper (no `ffmpeg.exe` is shipped). Build origin: a MinGW/GCC ("gyan.dev / BtbN"-style) full GPL-enabled Windows shared build, ~FFmpeg 7.x. No COPYING/LICENSE/README currently accompanies the DLLs.

| File | Version (SONAME) | Component | License | Signed by ReciPro? | Notes |
|------|------------------|-----------|---------|--------------------|-------|
| avcodec-61.dll | libavcodec 61 (FFmpeg ~7.x) | FFmpeg codec library | GPL-2.0-or-later (this build) | No (upstream GPL binary) | `TODO: confirm` exact FFmpeg version + build provenance/configure flags. |
| avformat-61.dll | libavformat 61 | FFmpeg muxing/demuxing | GPL-2.0-or-later | No | `TODO: confirm` exact version. |
| avutil-59.dll | libavutil 59 | FFmpeg utility library | GPL-2.0-or-later | No | `TODO: confirm` exact version. |
| swresample-5.dll | libswresample 5 | FFmpeg audio resampling | GPL-2.0-or-later | No | `TODO: confirm` exact version. |
| swscale-8.dll | libswscale 8 | FFmpeg image scaling/conversion | GPL-2.0-or-later | No | `TODO: confirm` exact version. |
| libx264-165.dll | x264 build 165 | H.264 encoder (**GPL trigger**) | GPL-2.0-or-later | No | Explicitly loaded by name in `FormMovie.cs`. Forces `--enable-gpl`. |
| libx265.dll | x265 | H.265/HEVC encoder (**GPL trigger**) | GPL-2.0-or-later | No | Explicitly loaded by name in `FormMovie.cs`. Forces `--enable-gpl`. `TODO: confirm` exact x265 version. |
| zlib1.dll | — | Compression (used by ffmpeg) | zlib license | No | `TODO: confirm` version. |
| libgcc_s_seh-1.dll | — | GCC runtime (MinGW-w64) | GPLv3 + GCC Runtime Library Exception | No | Permissive in practice under the Runtime Library Exception. |
| libstdc++-6.dll | — | GCC C++ standard library (MinGW-w64) | GPLv3 + GCC Runtime Library Exception | No | MinGW toolchain runtime. |
| libwinpthread-1.dll | — | mingw-w64 winpthreads | Permissive (MIT/BSD/zlib-style) | No | `TODO: confirm` exact license text. |

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
| Neutron coherent scattering lengths | `TODO: confirm` source (standard source is Sears (1992) *Neutron News* **3**, 26-37); not explicitly cited in code. | `TODO: add` citation. |
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

- The **ffmpeg / x264 / x265 / zlib / MinGW-runtime DLLs** (`ffmpeg\*.dll`) are upstream third-party binaries — and the ffmpeg/x264/x265 ones are **GPL**. They must keep their own provenance, must not be relicensed or represented as ReciPro's MIT code, and the GPL source-availability obligation above must be satisfied. (Authenticode will treat them as foreign/unsigned unless re-signed; do not re-sign them as ReciPro-authored.)
- The redistributed **NuGet runtime DLLs** (OpenTK, MathNet.Numerics, IronPython, BitMiracle.LibTiff.NET, FFMediaToolkit, SimdLinq, ZLinq, MemoryPack, PureHDF, DynamicExpresso.Core, WpfMath, System.Management, etc.) keep their own upstream provenance and licenses as listed above.

The MSI itself is not signed by Visual Studio (`SignOutput=FALSE` in the vdproj); the RID is pinned to `win-x64`, so no non-Windows/x86 runtime assets are emitted. See `CODE_SIGNING.md` for the release-artifact signing policy. This notice is not a substitute for the upstream license texts — refer to each upstream project or data provider for authoritative terms.
