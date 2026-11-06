# PED convergence diagnostic (261106Ch)

Archived data from the local `tools/PedConvergenceCheck` harness. The two result directories are copied here byte-for-byte. The harness source is not included in this documentation archive; the commands below require the local research checkout.

Finite conditions: Si and GaAs, 200 kV, [001] tilted by 0.5 degrees about (1, 0.3, 0), precession semiangle 1 degree, N=64/128, M=16/32/64/128/256, thickness 20/100 nm. Silicon B=0.46 Å²; Ga and As B=0.6 Å². Surface is (0,0,-1). Thicknesses share each PED eigensolution.

The initial fixed diagnostic used M=16/32/64 (`261106/`). After observing a 7–8% relative L2 difference at 100 nm for M32 vs M64, the follow-up extends only the angular sampling to M128/256 (`261106_refined/`), retaining the original data. This is an explicit follow-up diagnostic, not an independently preregistered convergence claim.

This is a numerical diagnostic, not a performance benchmark or a claim of converged physics. No acceptance threshold is inferred from the observed results. The larger M/N in each pair is only a finite reference.

```powershell
$env:DOTNET_PROCESSOR_COUNT='2'
$env:MKL_NUM_THREADS='1'
$env:OMP_NUM_THREADS='1'
dotnet build tools/PedConvergenceCheck -c Release -p:Platform=x64 -m:1
dotnet tools/PedConvergenceCheck/bin/x64/Release/net11.0-windows/PedConvergenceCheck.dll tools/PedConvergenceCheck/results/261106_refined
```

The project copies native DLLs from `ReciPro/bin/Release`; build native first. The program requires native loading, records the loaded DLL and assembly hashes, and sets its process priority to Idle. Run one calculation at a time. The declared output path above must match the build's reported path.

- `metadata.json`, `Si.cif`, `GaAs.cif`: exact fixture and execution metadata.
- `intensities.tsv`: raw `|Psi|²` for each hkl. This is wave intensity, not a P-weighted flux.
- `beam-counts.tsv`: actual beam counts per precession direction; requested N and actual beam count differ at symmetry ties.
- `angular-differences.tsv`: fixed N, adjacent M comparisons (16 vs 32, 32 vs 64, 64 vs 128, 128 vs 256).
- `beam-count-differences.tsv`: fixed M, N=64 vs 128.

Both comparisons match the hkl union, treating absent reflections as zero. `relL2 = ||I_test-I_ref||₂/||I_ref||₂`; `maxScale = max|I_test-I_ref|/max I_ref`. The 000 beam is included. Rows preserve separate crystal/thickness conditions and do not average away difficult cases. The raw intensities allow additional preregistered observables later.
