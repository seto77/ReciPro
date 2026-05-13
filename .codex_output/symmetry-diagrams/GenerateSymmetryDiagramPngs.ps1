<#
.SYNOPSIS
Generates symmetry-diagram PNG files for all ReciPro symmetry entries.

.DESCRIPTION
This script is intentionally kept under .codex_output/symmetry-diagrams so it can
travel together with the generated image sets and be reused from another chat.

What it does:
  1. Finds the ReciPro repository root from this script location.
  2. Creates a small temporary C# console project.
  3. References the local Crystallography and Crystallography.Controls projects.
  4. Renders SymmetryDiagramElements.RenderSymmetryElements(...) for every
     SymmetryStatic.Symmetries entry.
  5. Saves the rendered PNG files under a timestamped output folder.
  6. Optionally compares the new PNGs with an old/baseline PNG folder
     pixel-by-pixel and copies only changed old images to old_changed/.

Typical usage from the repository root:

  powershell -ExecutionPolicy Bypass -File .\.codex_output\symmetry-diagrams\GenerateSymmetryDiagramPngs.ps1

Generate a named image set:

  powershell -ExecutionPolicy Bypass -File .\.codex_output\symmetry-diagrams\GenerateSymmetryDiagramPngs.ps1 `
    -OutputName organized_260512_235959_current

Generate and compare against a previous image folder:

  powershell -ExecutionPolicy Bypass -File .\.codex_output\symmetry-diagrams\GenerateSymmetryDiagramPngs.ps1 `
    -OutputName organized_260512_235959_no-minus3-minus6-principal `
    -BaselineDir .\.codex_output\symmetry-diagrams\organized_260512_224224_no-minus3-minus6-principal\new

Output layout:
  <OutputName>/
    new/                         New PNGs for all symmetry entries.
    old_changed/                 Only when -BaselineDir is given; old PNGs
                                 whose pixels differ from the new image.
    changed_space_groups.tsv     Only rows with pixel differences.
    pixel_compare_manifest.tsv   One row per rendered symmetry entry.
    README.txt                   Short explanation of the generated set.

Notes:
  - The comparison is strict ARGB pixel equality. Even one pixel difference is
    reported.
  - The temporary C# project is deleted by default. Use -KeepWork to inspect it.
  - The script uses the current checkout. Commit or stash code changes first if
    you need a reproducible baseline.
#>

[CmdletBinding()]
param(
    # Width of each rendered PNG in pixels.
    [int]$Width = 900,

    # Height of each rendered PNG in pixels.
    [int]$Height = 900,

    # Name of the output folder below this script's directory.
    # A timestamp is included by default so repeated runs do not overwrite each other.
    [string]$OutputName = "generated_$(Get-Date -Format 'yyMMdd_HHmmss')_current",

    # Optional folder containing old PNGs with the same filenames as new/.
    # If specified, this script writes changed_space_groups.tsv and old_changed/.
    [string]$BaselineDir = "",

    # Keep the temporary generated C# console project for debugging.
    [switch]$KeepWork
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

# The script is expected to live at:
#   <repo>\.codex_output\symmetry-diagrams\GenerateSymmetryDiagramPngs.ps1
# Therefore the repository root is two directories above $PSScriptRoot.
$scriptDir = $PSScriptRoot
$repoRoot = (Resolve-Path (Join-Path $scriptDir "..\..")).Path

# Project paths used by the temporary C# renderer.
$crystallographyProject = Join-Path $repoRoot "Crystallography\Crystallography.csproj"
$controlsProject = Join-Path $repoRoot "Crystallography.Controls\Crystallography.Controls.csproj"

if (-not (Test-Path -LiteralPath $crystallographyProject)) {
    throw "Cannot find Crystallography project: $crystallographyProject"
}
if (-not (Test-Path -LiteralPath $controlsProject)) {
    throw "Cannot find Crystallography.Controls project: $controlsProject"
}

# Resolve the baseline folder early so the generated C# code receives an absolute path.
$baselineFullPath = ""
if (-not [string]::IsNullOrWhiteSpace($BaselineDir)) {
    $baselineFullPath = (Resolve-Path -LiteralPath $BaselineDir).Path
}

# Main output folder. It is created under .codex_output/symmetry-diagrams.
$outputRoot = Join-Path $scriptDir $OutputName
New-Item -ItemType Directory -Force -Path $outputRoot | Out-Null

# Temporary project folder. Keeping it inside the output root makes cleanup safe.
$workDir = Join-Path $outputRoot "_generator_work"
if (Test-Path -LiteralPath $workDir) {
    Remove-Item -LiteralPath $workDir -Recurse -Force
}
New-Item -ItemType Directory -Force -Path $workDir | Out-Null

# Create a minimal console app. We immediately overwrite the csproj and Program.cs
# so the template content is just a convenient bootstrap.
dotnet new console --force --output $workDir | Out-Null

$csprojPath = Join-Path $workDir "SymmetryPngGenerator.csproj"
$programPath = Join-Path $workDir "Program.cs"

# The generated project targets net10.0-windows because the renderer uses
# System.Drawing and WinForms-compatible drawing APIs.
$csproj = @"
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0-windows</TargetFramework>
    <UseWindowsForms>true</UseWindowsForms>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="$crystallographyProject" />
    <ProjectReference Include="$controlsProject" />
  </ItemGroup>
</Project>
"@
Set-Content -LiteralPath $csprojPath -Value $csproj -Encoding UTF8

# The C# renderer contains the actual rendering/comparison loop.
# It is generated rather than stored as a permanent project so this script can
# remain a single reusable artifact.
$program = @'
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using Crystallography;
using Crystallography.Controls;

if (args.Length < 4)
    throw new ArgumentException("Usage: <outputRoot> <width> <height> <baselineDir-or-empty>");

string outputRoot = Path.GetFullPath(args[0]);
int width = int.Parse(args[1]);
int height = int.Parse(args[2]);
string baselineDir = args[3] == "__NO_BASELINE__" ? "" : args[3];
bool hasBaseline = !string.IsNullOrWhiteSpace(baselineDir);

// new/ always contains the images produced by the current checkout.
string newDir = Path.Combine(outputRoot, "new");
Directory.CreateDirectory(newDir);

// old_changed/ is created only when comparing against a baseline.
string oldChangedDir = Path.Combine(outputRoot, "old_changed");
if (hasBaseline) Directory.CreateDirectory(oldChangedDir);

var manifest = new StringBuilder();
manifest.AppendLine("Series\tHM\tSystem\tNewPng\tBaselinePng\tWidth\tHeight\tDifferentPixels\tError");

var changed = new StringBuilder();
changed.AppendLine("Series\tHM\tSystem\tDifferentPixels\tBaselinePng\tNewPng");

int rendered = 0;
int compared = 0;
int different = 0;
int missingBaseline = 0;
int failed = 0;

for (int series = 1; series < SymmetryStatic.Symmetries.Length; series++)
{
    // Some repositories contain alternative settings or origin choices, so the
    // number of entries can be larger than the standard 230 space groups.
    var table = SymmetryElementsTable.Get(series);
    if (table == null) continue;

    var sym = SymmetryStatic.Symmetries[series];
    string stem = $"{series:000}_{Sanitize(sym.SpaceGroupHMStr)}";
    string newPath = Path.Combine(newDir, $"{stem}.png");
    string baselinePath = hasBaseline ? Path.Combine(baselineDir, $"{stem}.png") : "";

    try
    {
        // C projection is the same default used by FormSymmetryInformation.
        using var newBmp = SymmetryDiagramElements.RenderSymmetryElements(
            series,
            new Size(width, height),
            ProjectionAxis.C);

        newBmp.Save(newPath, ImageFormat.Png);
        rendered++;

        long diffPixels = 0;
        string error = "";

        if (hasBaseline)
        {
            if (!File.Exists(baselinePath))
            {
                missingBaseline++;
                error = "Baseline image missing";
            }
            else
            {
                using var oldBmp = new Bitmap(baselinePath);
                diffPixels = CountDifferentPixels(oldBmp, newBmp);
                compared++;

                if (diffPixels > 0)
                {
                    different++;
                    File.Copy(baselinePath, Path.Combine(oldChangedDir, Path.GetFileName(baselinePath)), overwrite: true);
                    changed.AppendLine($"{series}\t{sym.SpaceGroupHMStr}\t{sym.CrystalSystemStr}\t{diffPixels}\t{baselinePath}\t{newPath}");
                }
            }
        }

        manifest.AppendLine($"{series}\t{sym.SpaceGroupHMStr}\t{sym.CrystalSystemStr}\t{newPath}\t{baselinePath}\t{newBmp.Width}\t{newBmp.Height}\t{diffPixels}\t{error}");

        if (rendered % 50 == 0)
            Console.WriteLine($"rendered={rendered} compared={compared} different={different}");
    }
    catch (Exception ex)
    {
        failed++;
        manifest.AppendLine($"{series}\t{sym.SpaceGroupHMStr}\t{sym.CrystalSystemStr}\t{newPath}\t{baselinePath}\t\t\t\t{ex.GetType().Name}: {ex.Message}");
        Console.WriteLine($"failed {series}: {ex.GetType().Name}: {ex.Message}");
    }
}

File.WriteAllText(Path.Combine(outputRoot, "pixel_compare_manifest.tsv"), manifest.ToString(), Encoding.UTF8);
if (hasBaseline)
    File.WriteAllText(Path.Combine(outputRoot, "changed_space_groups.tsv"), changed.ToString(), Encoding.UTF8);

var readme = new StringBuilder();
readme.AppendLine($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
readme.AppendLine($"Image size: {width} x {height}");
readme.AppendLine($"Rendered entries: {rendered}");
readme.AppendLine($"Compared entries: {compared}");
readme.AppendLine($"Different entries: {different}");
readme.AppendLine($"Missing baseline entries: {missingBaseline}");
readme.AppendLine($"Failed entries: {failed}");
readme.AppendLine();
readme.AppendLine("Layout:");
readme.AppendLine("- new/: PNG files generated from the current checkout.");
if (hasBaseline)
{
    readme.AppendLine("- old_changed/: baseline PNG files only for entries that differ from new/.");
    readme.AppendLine("- changed_space_groups.tsv: list of entries with at least one changed pixel.");
}
readme.AppendLine("- pixel_compare_manifest.tsv: one row per rendered entry.");
readme.AppendLine();
readme.AppendLine("Comparison rule:");
readme.AppendLine("- ARGB values are compared pixel-by-pixel with exact equality.");
File.WriteAllText(Path.Combine(outputRoot, "README.txt"), readme.ToString(), Encoding.UTF8);

Console.WriteLine($"output={outputRoot}");
Console.WriteLine($"rendered={rendered} compared={compared} different={different} missingBaseline={missingBaseline} failed={failed}");

static long CountDifferentPixels(Bitmap oldBmp, Bitmap newBmp)
{
    // If sizes differ, report a non-zero synthetic difference count. In normal
    // use this should not happen because the same Width/Height are supplied.
    if (oldBmp.Width != newBmp.Width || oldBmp.Height != newBmp.Height)
        return Math.Max(oldBmp.Width, newBmp.Width) * (long)Math.Max(oldBmp.Height, newBmp.Height);

    long count = 0;
    for (int y = 0; y < newBmp.Height; y++)
        for (int x = 0; x < newBmp.Width; x++)
            if (oldBmp.GetPixel(x, y).ToArgb() != newBmp.GetPixel(x, y).ToArgb())
                count++;
    return count;
}

static string Sanitize(string value)
{
    // Keep filenames readable while removing characters that are invalid on
    // Windows or awkward in command lines.
    var invalid = Path.GetInvalidFileNameChars()
        .Concat(new[] { '/', '\\', ':', '*', '?', '"', '<', '>', '|' })
        .ToHashSet();

    var sb = new StringBuilder(value.Length);
    foreach (char c in value)
        sb.Append(invalid.Contains(c) || char.IsWhiteSpace(c) ? '_' : c);

    return sb.ToString().Replace("__", "_").Trim('_');
}
'@
Set-Content -LiteralPath $programPath -Value $program -Encoding UTF8

try {
    # Run the generated renderer. Passing the baseline as an empty string is
    # easier than adding separate C# modes.
    # PowerShell does not reliably preserve an empty string as a native-process
    # argument in every host, so use an explicit sentinel when no baseline is
    # requested.
    $baselineArg = if ([string]::IsNullOrWhiteSpace($baselineFullPath)) { "__NO_BASELINE__" } else { $baselineFullPath }
    dotnet run --project $csprojPath -c Debug -- $outputRoot $Width $Height $baselineArg
    if ($LASTEXITCODE -ne 0) {
        throw "dotnet run failed with exit code $LASTEXITCODE"
    }
}
finally {
    if (-not $KeepWork) {
        Remove-Item -LiteralPath $workDir -Recurse -Force
    }
}

Write-Host "Done."
Write-Host "Output folder: $outputRoot"
