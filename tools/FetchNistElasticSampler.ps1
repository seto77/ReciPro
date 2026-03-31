param(
    [Parameter(Mandatory = $true, Position = 0)]
    [int[]]$AtomicNumbers,

    [string]$OutputDir = "$PSScriptRoot\\..\\ReciPro\\NistElasticSampler", # (260331Ch) 既定はリポジトリ配下。ビルド時に配布物へコピーする

    [switch]$Force
)

$ErrorActionPreference = "Stop"

# (260331Ch) Create a local venv with Playwright and save compact float32 sampler binaries into the repo.
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$pythonScript = Join-Path $scriptDir "fetch_nist_elastic_sampler.py"
$venvDir = Join-Path $env:LOCALAPPDATA "ReciPro\\MonteCarlo\\PlaywrightVenv"
$pythonExe = Join-Path $venvDir "Scripts\\python.exe"

if (-not (Get-Command python -ErrorAction SilentlyContinue)) {
    throw "python was not found. Please make Python available from PATH."
}

if (-not (Test-Path $pythonExe)) {
    python -m venv $venvDir
}

& $pythonExe -m pip install --upgrade pip playwright
& $pythonExe -m playwright install chromium

$arguments = @($pythonScript, "--output-dir", $OutputDir)
if ($Force) {
    $arguments += "--force"
}
$arguments += @($AtomicNumbers | ForEach-Object { "$_" })

& $pythonExe @arguments
