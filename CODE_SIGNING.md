# Code signing policy

This document describes the code-signing policy for ReciPro release artifacts.

## Current status

ReciPro has been accepted into the **SignPath Foundation** free code-signing program for open-source projects, and a production signing certificate was issued to the ReciPro SignPath organization on 2026-06-29. Since **v.4.942** (released 2026-07-01), the x64 installer and the x64 portable executable are signed as part of the automated release pipeline. The experimental Arm64 assets remain unsigned. <!-- 260715Ch: match the active release.yml signing scope -->

Releases up to v.4.941 predate code signing: unless a GitHub Release explicitly states that `ReciPro-setup.msi` (named `ReciProSetup.msi` up to v.4.939) is digitally signed, users should not assume that the installer is signed.

## Official downloads

Official release artifacts are published only from the ReciPro GitHub Releases page:

- https://github.com/seto77/ReciPro/releases/latest

Users should avoid downloading ReciPro installers from unofficial mirrors or third-party redistribution sites.

## Active signing model

Free code signing on Windows provided by [SignPath.io](https://about.signpath.io), certificate by [SignPath Foundation](https://signpath.org).

The x64 installer package and the x64 portable `ReciPro.exe` are signed using Windows Authenticode before they are published to GitHub Releases. For SignPath Foundation signing, the signer shown by Windows may be `SignPath Foundation` rather than the personal name of the ReciPro maintainer. Because SignPath Foundation signing requires a maintainer to manually approve each signing request, there may be a delay between a new version being pushed and the corresponding GitHub Release appearing, while the signing request awaits approval. <!-- 260715Ch -->

## Scope of signing

The current signing scope is: <!-- 260715Ch: verified against release.yml and the v.4.943 artifacts -->

- `ReciPro-setup.msi` and its identical legacy-named copy `ReciProSetup.msi` (x64)
- the single-file `ReciPro.exe` inside the x64 portable ZIP

The framework-dependent `ReciPro.exe` and DLLs stored inside the signed MSI are not individually Authenticode-signed; their integrity is covered by the signed MSI container. The loose `Crystallography.Native*.dll` files in the x64 portable ZIP are also not individually signed. The experimental Arm64 MSI, portable executable, and native DLLs remain unsigned until Arm64 graduates into the production signing scope.

Third-party binaries should not be re-signed as if they were maintained by ReciPro unless their provenance and redistribution terms explicitly permit that use. See `THIRD-PARTY-NOTICES.md` for bundled or referenced third-party components and data.

## Lightweight repository protection policy

ReciPro is primarily a personal research-software project. The repository policy is intentionally lightweight: routine development should remain possible without mandatory pull requests, external approvals, required signed commits, or required status checks.

For release integrity, the intended minimum protection is limited to release-critical history:

- the default branch, `master`, should not be force-pushed or deleted;
- release tags matching `v*` should not be deleted or moved after creation;
- release builds should be produced from the public GitHub repository and published as GitHub Releases.

This keeps day-to-day development simple while preserving the correspondence between a released installer, the release tag, and the public source tree.

## Maintainer and signing roles

ReciPro is maintained by a single maintainer, Yusuke Seto (Osaka Metropolitan University), who is the sole committer and owner of the source repository. For code signing, the same person holds all roles:

- **Author** — Yusuke Seto: develops and maintains the source code.
- **Approver** — Yusuke Seto: reviews and approves each signing request before a release is signed.

Because there are no external committers, every change is authored by the maintainer, and each release is manually approved before signing.

## Privacy

ReciPro is a local desktop application and does not collect or transmit any personal or usage data. See the Privacy section of the project README: https://github.com/seto77/ReciPro#privacy

## Verifying an installer

### Digital-signature check after signing is enabled

After code signing is enabled for a release, users can inspect the installer from Windows Explorer:

1. Right-click `ReciPro-setup.msi`.
2. Open **Properties**.
3. Open the **Digital Signatures** tab.
4. Confirm that the signature is valid and that the signer matches the signer documented for that release.

Advanced users can also verify the installer with the Windows SDK `signtool` utility:

```powershell
signtool verify /pa /all ReciPro-setup.msi
```

## Reporting suspicious artifacts

If you find a suspicious ReciPro installer or a download link that does not match the official GitHub Releases page, please report it through the GitHub issue tracker or contact the maintainer through the project website.
