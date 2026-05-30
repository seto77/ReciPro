# Third-party notices

This file summarizes third-party components, libraries, tools, and data sources that are bundled with or referenced by ReciPro. It is intended to support transparent redistribution and future code-signing review.

This file is not a substitute for the original license texts. For each dependency, refer to the upstream project or data provider for the authoritative license terms.

## Project license

ReciPro itself is distributed under the MIT License. See `LICENSE.md`.

## NuGet and library dependencies

ReciPro uses third-party .NET libraries through project and package references. The exact dependency graph may vary by build configuration and target framework.

Known or expected categories include:

- .NET Desktop / Windows Forms runtime components from Microsoft
- OpenGL-related libraries and controls
- numerical and scientific-computing libraries
- plotting, image, compression, and file-format support libraries where applicable

Before submitting the project for code-signing review, the dependency list should be checked against the current `.csproj` files and NuGet lock information, and this notice file should be updated with exact package names, versions, upstream URLs, and licenses.

## Built-in and downloaded crystallographic data

### AMCSD

ReciPro includes access to the American Mineralogist Crystal Structure Database (AMCSD). The README describes this as a built-in crystal database available immediately after installation.

Reference:

- Downs, R. T. & Hall-Wallace, M. (2003). The American Mineralogist Crystal Structure Database. *American Mineralogist*, **88**, 247-250.

The redistribution terms and citation requirements for the exact AMCSD data bundled with ReciPro should be confirmed and documented here before final code-signing submission.

### COD

ReciPro can download and use data from the Crystallography Open Database (COD). The README describes COD data as downloaded automatically on first use and then available offline.

The COD license and attribution requirements should be documented here with the exact terms used by the downloaded dataset.

## External tools

### ffmpeg

ReciPro includes or uses ffmpeg-related functionality for generating rotation animation videos. ffmpeg has its own licensing terms, which depend on the build configuration and enabled codecs.

Before final code-signing submission, confirm whether ffmpeg binaries are bundled in the installer or only expected to be available externally. If bundled, document:

- the exact binary filename and version;
- source or download URL;
- license mode for that build;
- whether any GPL components are enabled;
- required license text and attribution.

## Native and bundled binaries

Any bundled native DLLs, EXEs, databases, fonts, icons, or other redistributable assets should be listed here before final code-signing submission.

For each item, document:

- file name or package name;
- version, if applicable;
- upstream project or provider;
- license;
- whether it is built from this repository or redistributed as an upstream binary;
- whether it is intended to be signed by ReciPro.

## Code-signing note

The intended code-signing scope is limited to ReciPro release artifacts and binaries built from this repository. Third-party binaries should not be re-signed as if they were maintained by ReciPro unless their provenance and redistribution terms explicitly permit that use.

See `CODE_SIGNING.md` for the release-artifact signing policy.
