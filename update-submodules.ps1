#requires -Version 5.1
<#
.SYNOPSIS
  Pull the latest of each shared-library submodule and record the new pointers.
.DESCRIPTION
  The Crystallography* libraries are git submodules (migrated from junctions on
  2026-06-24). Each consuming app pins a specific lib commit and does NOT auto-follow,
  so changes pushed from another app are not visible here until you sync.

  This script handles the PULL direction: fast-forward every submodule to the latest
  of its tracked branch (main) and commit the pointer bump in this repository.

  To push YOUR OWN lib edits instead, commit & push inside the submodule first, then
  `git add <lib>` here (do NOT run this script for that — it follows the remote branch).
.PARAMETER NoCommit
  Update the submodule working trees and stage the pointer changes, but do not commit.
.EXAMPLE
  .\update-submodules.ps1
.EXAMPLE
  .\update-submodules.ps1 -NoCommit
#>
param([switch]$NoCommit)
$ErrorActionPreference = 'Stop'
Set-Location -LiteralPath $PSScriptRoot

if (-not (Test-Path .gitmodules)) { Write-Host 'No .gitmodules in this repo; nothing to do.'; return }

Write-Host 'Updating submodules to the latest of their tracked branch (main)...'
git submodule update --init --recursive --remote --merge
if ($LASTEXITCODE -ne 0) { throw "git submodule update failed ($LASTEXITCODE)" }

# Stage pointer changes for every submodule path declared in .gitmodules.
$paths = git config --file .gitmodules --get-regexp '\.path$' | ForEach-Object { ($_ -split '\s+', 2)[1] }
git add -- $paths

if (-not (git diff --cached --name-only)) {
  Write-Host 'Submodules already up to date; nothing to commit.'
  return
}

Write-Host "`nSubmodule pointers now at:"
git submodule status | ForEach-Object { Write-Host "  $_" }

if ($NoCommit) {
  Write-Host "`n-NoCommit specified: pointers staged but not committed."
  return
}

git commit -m 'Update crystallography submodules'
if ($LASTEXITCODE -ne 0) { throw "commit failed ($LASTEXITCODE)" }
Write-Host "`nCommitted submodule pointer update. Run 'git push' to publish."
