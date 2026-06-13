# Code signing policy

This document describes the code-signing policy for ReciPro release artifacts.

## Current status

ReciPro is preparing code signing for Windows installer releases.

Unless a GitHub Release explicitly states that `ReciPro-setup.msi` (named `ReciProSetup.msi` up to v.4.939) is digitally signed, users should not assume that the installer is signed.

## Official downloads

Official release artifacts are published only from the ReciPro GitHub Releases page:

- https://github.com/seto77/ReciPro/releases/latest

Users should avoid downloading ReciPro installers from unofficial mirrors or third-party redistribution sites.

## Intended signing model

The intended signing route for Windows installer packages is the **SignPath Foundation** open-source code-signing program (SignPath.io). ReciPro is currently adopting this program (application in progress).

Once signing is enabled, ReciPro will carry the following attribution:

> Free code signing provided by [SignPath.io](https://about.signpath.io), certificate by [SignPath Foundation](https://signpath.org).

Release artifacts will be signed using Windows Authenticode signing and then published from GitHub Releases. For SignPath Foundation signing, the signer shown by Windows may be `SignPath Foundation` rather than the personal name of the ReciPro maintainer.

## Scope of signing

The intended signing scope is:

- `ReciPro-setup.msi` and `ReciPro-setup_arm64.msi` (including the legacy-named copy `ReciProSetup.msi`, which is the identical x64 installer kept for auto-update compatibility with older versions)
- ReciPro executable files built from this repository, including the single-file `ReciPro.exe` inside the portable ZIP packages (win-x64; the experimental win-arm64 package may remain unsigned until it graduates from experimental status)
- ReciPro libraries built from this repository

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
