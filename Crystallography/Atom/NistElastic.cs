#region using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
#endregion

namespace Crystallography;

internal static class NistElasticPchip // (260401Ch) 生成・最適化・runtime 評価で共有する PCHIP 基本処理
{
    public const int EnergyCount = 101;
    public const int SourcePhiCount = 2001;
    public const int KnotCount = 51;
    public const int EvaluationCount = 4097;

    public const double LogMinimumEnergyEv = 3.912023005428146; // (260401Ch) log(50)
    public const double LogEnergyStep = 0.059914645471079815; // (260401Ch) (log(20000)-log(50))/100
    public static readonly double[] EvaluationPhiGrid = CreateEvaluationPhiGrid(); // (260401Ch)

    public static double EnergyEvFromBlockIndex(int blockIndex)
        => Math.Exp(LogMinimumEnergyEv + LogEnergyStep * blockIndex);

    public static double TransformErrorCoordinate(double x)
        => Math.Sqrt(Math.Max(0.0, 1.0 - Math.Clamp(x, -1.0, 1.0)));

    public static double InvertPhi(double[] phiOfX, double phi)
    {
        if (phi <= phiOfX[0])
            return 1.0;
        if (phi >= phiOfX[^1])
            return -1.0;

        var upper = Array.BinarySearch(phiOfX, phi);
        if (upper >= 0)
            return 1.0 - upper * 0.001;

        upper = ~upper;
        var lower = upper - 1;
        while (upper < phiOfX.Length && phiOfX[upper] == phiOfX[lower])
            upper++;

        if (upper >= phiOfX.Length)
            return 1.0 - lower * 0.001;

        var phiLower = phiOfX[lower];
        var phiUpper = phiOfX[upper];
        if (Math.Abs(phiUpper - phiLower) <= double.Epsilon)
            return 1.0 - lower * 0.001;

        var xLower = 1.0 - lower * 0.001;
        var xUpper = 1.0 - upper * 0.001;
        var fraction = (phi - phiLower) / (phiUpper - phiLower);
        return xLower + (xUpper - xLower) * fraction;
    }

    public static double[] ComputeSlope(double[] x, double[] y)
    {
        if (x.Length != y.Length)
            throw new ArgumentException("x and y must have the same length.");
        if (x.Length < 2)
            throw new ArgumentException("At least two knots are required.");

        var n = x.Length;
        var h = new double[n - 1];
        var delta = new double[n - 1];
        for (int i = 0; i < n - 1; i++)
        {
            h[i] = x[i + 1] - x[i];
            delta[i] = (y[i + 1] - y[i]) / h[i];
        }

        var slope = new double[n];
        if (n == 2)
        {
            slope[0] = delta[0];
            slope[1] = delta[0];
            return slope;
        }

        slope[0] = ComputeEndpointSlope(h[0], h[1], delta[0], delta[1]);
        slope[^1] = ComputeEndpointSlope(h[^1], h[^2], delta[^1], delta[^2]);
        for (int i = 1; i < n - 1; i++)
        {
            if (Math.Sign(delta[i - 1]) * Math.Sign(delta[i]) <= 0)
            {
                slope[i] = 0.0;
                continue;
            }

            var w1 = 2.0 * h[i] + h[i - 1];
            var w2 = h[i] + 2.0 * h[i - 1];
            slope[i] = (w1 + w2) / (w1 / delta[i - 1] + w2 / delta[i]);
        }

        return slope;
    }

    public static double Evaluate(double[] x, double[] y, double[] slope, double value)
    {
        if (value <= x[0])
            return y[0];
        if (value >= x[^1])
            return y[^1];

        int upper = Array.BinarySearch(x, value);
        if (upper < 0)
            upper = ~upper;
        upper = Math.Clamp(upper, 1, x.Length - 1);
        return EvaluateSegment(x, y, slope, upper - 1, value);
    }

    public static double EvaluateSegment(double[] x, double[] y, double[] slope, int segmentIndex, double value)
    {
        if (value <= x[0])
            return y[0];
        if (value >= x[^1])
            return y[^1];

        var h = x[segmentIndex + 1] - x[segmentIndex];
        var t = (value - x[segmentIndex]) / h;
        var t2 = t * t;
        var t3 = t2 * t;
        var h00 = 2.0 * t3 - 3.0 * t2 + 1.0;
        var h10 = t3 - 2.0 * t2 + t;
        var h01 = -2.0 * t3 + 3.0 * t2;
        var h11 = t3 - t2;
        var yValue = h00 * y[segmentIndex]
            + h10 * h * slope[segmentIndex]
            + h01 * y[segmentIndex + 1]
            + h11 * h * slope[segmentIndex + 1];
        return Math.Clamp(yValue, -1.0, 1.0);
    }

    private static double ComputeEndpointSlope(double h0, double h1, double delta0, double delta1)
    {
        var slope = ((2.0 * h0 + h1) * delta0 - h0 * delta1) / (h0 + h1);
        if (Math.Sign(slope) != Math.Sign(delta0))
            return 0.0;
        if (Math.Sign(delta0) != Math.Sign(delta1) && Math.Abs(slope) > Math.Abs(3.0 * delta0))
            return 3.0 * delta0;
        return slope;
    }

    private static double[] CreateEvaluationPhiGrid()
    {
        var grid = new double[EvaluationCount];
        for (int i = 0; i < EvaluationCount; i++)
            grid[i] = i / (double)(EvaluationCount - 1);
        return grid;
    }
}

public sealed class NistElasticCompressionProgress // (260401Ch) FormEBSD の status strip に進捗を返す
{
    public int FileIndex { get; init; }
    public int FileCount { get; init; }
    public int AtomicNumber { get; init; }
    public string SourcePath { get; init; } = "";
    public int BlockIndex { get; init; }
    public int BlockCount { get; init; }
    public string Phase { get; init; } = "";
    public double FileProgress { get; init; }
    public double OverallProgress { get; init; }
}

public sealed class NistElasticPchipCompressedBlock // (260401Ch) 1 エネルギーブロックぶんの圧縮結果
{
    public double EnergyEv { get; init; }
    public double SigmaA0Squared { get; init; }
    public ushort[] PhiKnotIndices { get; init; } = [];
    public float[] XKnot { get; init; } = [];
    public double WeightedRootMeanSquareError { get; init; }
    public double WeightedMaximumError { get; init; }
    public TimeSpan Elapsed { get; init; }
}

public sealed class NistElasticPchipCompressedElement // (260401Ch) 元素ごとの圧縮結果
{
    public int AtomicNumber { get; init; }
    public string SourcePath { get; init; } = "";
    public NistElasticPchipCompressedBlock[] Blocks { get; init; } = [];
}

public sealed class NistElasticPchipElementData // (260401Ch) generated な圧縮データの保存形式
{
    public NistElasticPchipElementData(int atomicNumber, double[] sigmaA0Squared, ushort[][] phiKnotIndices, float[][] xKnot)
    {
        AtomicNumber = atomicNumber;
        SigmaA0Squared = sigmaA0Squared;
        PhiKnotIndices = phiKnotIndices;
        XKnot = xKnot;
    }

    public int AtomicNumber { get; }
    public double[] SigmaA0Squared { get; }
    public ushort[][] PhiKnotIndices { get; }
    public float[][] XKnot { get; }
}

internal sealed class NistElasticPchipRuntimeElement // (260401Ch) generated データから runtime 用に展開したキャッシュ
{
    // public NistElasticPchipRuntimeElement(int atomicNumber, double[] sigmaA0Squared, double[][] phiKnot, double[][] xKnot, double[][] slope) // 260401Cl 旧シグネチャ
    public NistElasticPchipRuntimeElement(int atomicNumber, double[] sigmaA0Squared, double[][] phiKnot, double[][] xKnot, double[][] slope, byte[][] guideTable) // 260401Cl GuideTable 追加
    {
        AtomicNumber = atomicNumber;
        SigmaA0Squared = sigmaA0Squared;
        PhiKnot = phiKnot;
        XKnot = xKnot;
        Slope = slope;
        GuideTable = guideTable; // 260401Cl 追加
    }

    public int AtomicNumber { get; }
    public double[] SigmaA0Squared { get; }
    public double[][] PhiKnot { get; }
    public double[][] XKnot { get; }
    public double[][] Slope { get; }
    /// <summary>260401Cl PCHIP バイナリサーチを O(1) に高速化するガイドテーブル。PhiKnot 値 [0,1] を 64 分割し、各ビンに対応するセグメント開始インデックスを格納</summary>
    public byte[][] GuideTable { get; }
}

public sealed class NistElasticCompressionRunResult // (260401Ch) developer tool から扱いやすいよう出力先と成果物をまとめる
{
    public string RepositoryRoot { get; init; } = "";
    public string OriginalDirectory { get; init; } = "";
    public string GeneratedDirectory { get; init; } = "";
    public string DiagnosticsDirectory { get; init; } = "";
    public int SourceFileCount { get; init; }
    public IReadOnlyList<string> OutputPaths { get; init; } = [];
}

public static class NistElasticSamplerPchipGenerator
{
    private const int CoordinateDescentPassCount = 3; // (260401Ch)
    private const int BasinHopAttemptCount = 2; // (260401Ch)
    private const int BasinHopRemovalCount = 3; // (260401Ch)

    public static string TryFindRepositoryRoot(string startPath)
    {
        foreach (var candidate in EnumerateCandidateDirectories(startPath))
        {
            var current = candidate;
            while (current != null)
            {
                var solutionPath = Path.Combine(current.FullName, "ReciPro.sln");
                if (File.Exists(solutionPath))
                    return current.FullName;

                current = current.Parent;
            }
        }

        return null;
    }

    public static string GetOriginalDirectory(string repositoryRoot)
    {
        ValidateRepositoryRoot(repositoryRoot);
        return Path.Combine(repositoryRoot, "Crystallography", "Atom", "NistElastic", "Original"); // (260401Ch) 元 TXT は NistElastic/Original にそろえる
    }

    public static string GetGeneratedDirectory(string repositoryRoot)
    {
        ValidateRepositoryRoot(repositoryRoot);
        return Path.Combine(repositoryRoot, "Crystallography", "Atom", "NistElastic"); // (260401Ch)
    }

    public static string GetDiagnosticsDirectory(string repositoryRoot)
    {
        ValidateRepositoryRoot(repositoryRoot);
        return Path.Combine(GetGeneratedDirectory(repositoryRoot), "Diagnostics"); // (260401Ch)
    }

    public static NistElasticCompressionRunResult GenerateCompressedSourcesToRepository(
        IEnumerable<string> sourcePaths, string repositoryRoot, IProgress<NistElasticCompressionProgress> progress = null)
    {
        ValidateRepositoryRoot(repositoryRoot);
        var normalizedSources = NormalizeSourcePaths(sourcePaths); // (260401Ch) UI 側に正規化ロジックを持ち込まない
        var outputs = GenerateCompressedSources(normalizedSources, repositoryRoot, progress); // (260401Ch) 実処理は既存 generator をそのまま使う
        return new NistElasticCompressionRunResult()
        {
            RepositoryRoot = repositoryRoot,
            OriginalDirectory = GetOriginalDirectory(repositoryRoot),
            GeneratedDirectory = GetGeneratedDirectory(repositoryRoot),
            DiagnosticsDirectory = GetDiagnosticsDirectory(repositoryRoot),
            SourceFileCount = normalizedSources.Length,
            OutputPaths = [.. outputs],
        };
    }

    public static IReadOnlyList<string> GenerateCompressedSources(IEnumerable<string> sourcePaths, string repositoryRoot, IProgress<NistElasticCompressionProgress> progress = null)
    {
        ValidateRepositoryRoot(repositoryRoot);
        var normalizedSources = NormalizeSourcePaths(sourcePaths);

        if (normalizedSources.Length == 0)
            return [];

        var generatedDirectory = GetGeneratedDirectory(repositoryRoot); // (260401Ch)
        var diagnosticsDirectory = GetDiagnosticsDirectory(repositoryRoot); // (260401Ch)
        Directory.CreateDirectory(generatedDirectory);
        Directory.CreateDirectory(diagnosticsDirectory);

        var outputs = new List<string>();
        for (int fileIndex = 0; fileIndex < normalizedSources.Length; fileIndex++)
        {
            var sourcePath = normalizedSources[fileIndex];
            var atomicNumber = ParseAtomicNumberFromPath(sourcePath);
            var elementResult = CompressElement(sourcePath, atomicNumber, fileIndex, normalizedSources.Length, progress);

            outputs.Add(WriteGeneratedElementSource(generatedDirectory, elementResult));
            outputs.Add(WriteDiagnosticsCsv(diagnosticsDirectory, elementResult));
        }

        outputs.Add(WriteGeneratedRegistrySource(generatedDirectory));
        return outputs;
    }

    private static void ValidateRepositoryRoot(string repositoryRoot)
    {
        if (string.IsNullOrWhiteSpace(repositoryRoot))
            throw new ArgumentException("Repository root must not be empty.", nameof(repositoryRoot));
    }

    private static string[] NormalizeSourcePaths(IEnumerable<string> sourcePaths)
    {
        if (sourcePaths == null)
            throw new ArgumentNullException(nameof(sourcePaths));

        return sourcePaths
            .Where(path => !string.IsNullOrWhiteSpace(path))
            .Select(Path.GetFullPath)
            .Where(File.Exists)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(ParseAtomicNumberFromPath)
            .ToArray();
    }

    internal static int ParseAtomicNumberFromPath(string path)
    {
        var fileName = Path.GetFileNameWithoutExtension(path);
        var markerIndex = fileName.IndexOf("E_", StringComparison.OrdinalIgnoreCase);
        if (markerIndex < 0)
            throw new InvalidDataException($"Atomic number could not be parsed from '{path}'.");

        var digits = new string(fileName[(markerIndex + 2)..].TakeWhile(char.IsDigit).ToArray());
        if (!int.TryParse(digits, NumberStyles.Integer, CultureInfo.InvariantCulture, out var atomicNumber))
            throw new InvalidDataException($"Atomic number could not be parsed from '{path}'.");

        return atomicNumber;
    }

    private static int ParseAtomicNumberFromGeneratedCodePath(string path)
    {
        var fileName = Path.GetFileNameWithoutExtension(path);
        const string prefix = "PCHIP";
        if (!fileName.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            throw new InvalidDataException($"Atomic number could not be parsed from generated file '{path}'.");

        var digits = new string(fileName[prefix.Length..].TakeWhile(char.IsDigit).ToArray());
        if (!int.TryParse(digits, NumberStyles.Integer, CultureInfo.InvariantCulture, out var atomicNumber))
            throw new InvalidDataException($"Atomic number could not be parsed from generated file '{path}'.");

        return atomicNumber;
    }

    private static NistElasticPchipCompressedElement CompressElement(string sourcePath, int atomicNumber, int fileIndex, int fileCount, IProgress<NistElasticCompressionProgress> progress)
    {
        var table = ReadTextTable(sourcePath);
        var blocks = new NistElasticPchipCompressedBlock[NistElasticPchip.EnergyCount];
        ushort[] previousPhiKnotIndices = null;

        for (int blockIndex = 0; blockIndex < NistElasticPchip.EnergyCount; blockIndex++)
        {
            progress?.Report(new NistElasticCompressionProgress
            {
                FileIndex = fileIndex + 1,
                FileCount = fileCount,
                AtomicNumber = atomicNumber,
                SourcePath = sourcePath,
                BlockIndex = blockIndex + 1,
                BlockCount = NistElasticPchip.EnergyCount,
                Phase = "Compressing",
                FileProgress = blockIndex / (double)NistElasticPchip.EnergyCount,
                OverallProgress = (fileIndex + blockIndex / (double)NistElasticPchip.EnergyCount) / fileCount,
            });

            blocks[blockIndex] = CompressBlock(
                table.Phi[blockIndex],
                table.SigmaA0Squared[blockIndex],
                NistElasticPchip.EnergyEvFromBlockIndex(blockIndex),
                previousPhiKnotIndices);

            previousPhiKnotIndices = [.. blocks[blockIndex].PhiKnotIndices];
        }

        progress?.Report(new NistElasticCompressionProgress
        {
            FileIndex = fileIndex + 1,
            FileCount = fileCount,
            AtomicNumber = atomicNumber,
            SourcePath = sourcePath,
            BlockIndex = NistElasticPchip.EnergyCount,
            BlockCount = NistElasticPchip.EnergyCount,
            Phase = "Completed",
            FileProgress = 1.0,
            OverallProgress = (fileIndex + 1.0) / fileCount,
        });

        return new NistElasticPchipCompressedElement()
        {
            AtomicNumber = atomicNumber,
            SourcePath = sourcePath,
            Blocks = blocks,
        };
    }

    private static NistElasticPchipCompressedBlock CompressBlock(double[] phiOfX, double sigmaA0Squared, double energyEv, ushort[] previousPhiKnotIndices)
    {
        var stopwatch = Stopwatch.StartNew();
        var exactX = new double[NistElasticPchip.EvaluationCount];
        var transformedExactX = new double[NistElasticPchip.EvaluationCount];
        for (int i = 0; i < NistElasticPchip.EvaluationCount; i++)
        {
            var x = NistElasticPchip.InvertPhi(phiOfX, NistElasticPchip.EvaluationPhiGrid[i]);
            exactX[i] = x;
            transformedExactX[i] = NistElasticPchip.TransformErrorCoordinate(x);
        }

        var phiKnotIndices = TryCreateWarmStart(previousPhiKnotIndices) ?? CreateGreedyKnotIndices(exactX, transformedExactX);
        EvaluateApproximation(phiKnotIndices, exactX, transformedExactX, out var bestRootMeanSquareError, out var bestMaximumError);
        CoordinateDescent(phiKnotIndices, exactX, transformedExactX, ref bestRootMeanSquareError, ref bestMaximumError);
        BasinHop(phiKnotIndices, exactX, transformedExactX, ref bestRootMeanSquareError, ref bestMaximumError);

        var xKnot = new float[NistElasticPchip.KnotCount];
        for (int i = 0; i < NistElasticPchip.KnotCount; i++)
            xKnot[i] = (float)exactX[phiKnotIndices[i]];

        stopwatch.Stop();
        return new NistElasticPchipCompressedBlock()
        {
            EnergyEv = energyEv,
            SigmaA0Squared = sigmaA0Squared,
            PhiKnotIndices = Array.ConvertAll(phiKnotIndices, static index => (ushort)index),
            XKnot = xKnot,
            WeightedRootMeanSquareError = bestRootMeanSquareError,
            WeightedMaximumError = bestMaximumError,
            Elapsed = stopwatch.Elapsed,
        };
    }

    private static int[] TryCreateWarmStart(ushort[] previousPhiKnotIndices)
    {
        if (previousPhiKnotIndices == null || previousPhiKnotIndices.Length != NistElasticPchip.KnotCount)
            return null;

        var warmStart = Array.ConvertAll(previousPhiKnotIndices, static index => (int)index);
        warmStart[0] = 0;
        warmStart[^1] = NistElasticPchip.EvaluationCount - 1;

        for (int i = 1; i < warmStart.Length; i++)
        {
            if (warmStart[i] <= warmStart[i - 1])
                warmStart[i] = warmStart[i - 1] + 1;
        }

        for (int i = warmStart.Length - 2; i >= 0; i--)
        {
            if (warmStart[i] >= warmStart[i + 1])
                warmStart[i] = warmStart[i + 1] - 1;
        }

        for (int i = 1; i < warmStart.Length; i++)
        {
            if (warmStart[i] <= warmStart[i - 1])
                return null;
        }

        return warmStart;
    }

    private static int[] CreateGreedyKnotIndices(double[] exactX, double[] transformedExactX)
    {
        var selected = new bool[NistElasticPchip.EvaluationCount];
        selected[0] = true;
        selected[^1] = true;
        var phiKnotIndices = new List<int>(NistElasticPchip.KnotCount) { 0, NistElasticPchip.EvaluationCount - 1 };

        while (phiKnotIndices.Count < NistElasticPchip.KnotCount)
        {
            phiKnotIndices.Sort();
            EvaluateApproximation([.. phiKnotIndices], exactX, transformedExactX, out _, out _, out var transformedApproximation);

            var nextIndex = -1;
            var nextError = double.NegativeInfinity;
            for (int i = 1; i < NistElasticPchip.EvaluationCount - 1; i++)
            {
                if (selected[i])
                    continue;

                var error = Math.Abs(transformedApproximation[i] - transformedExactX[i]);
                if (error > nextError)
                {
                    nextError = error;
                    nextIndex = i;
                }
            }

            if (nextIndex < 0)
                throw new InvalidOperationException("Failed to choose the next greedy knot.");

            selected[nextIndex] = true;
            phiKnotIndices.Add(nextIndex);
        }

        phiKnotIndices.Sort();
        return [.. phiKnotIndices];
    }

    private static void CoordinateDescent(int[] phiKnotIndices, double[] exactX, double[] transformedExactX, ref double bestRootMeanSquareError, ref double bestMaximumError)
    {
        for (int pass = 0; pass < CoordinateDescentPassCount; pass++)
        {
            var improved = false;
            for (int knotIndex = 1; knotIndex < phiKnotIndices.Length - 1; knotIndex++)
            {
                var original = phiKnotIndices[knotIndex];
                var bestCandidate = original;
                var lower = phiKnotIndices[knotIndex - 1] + 1;
                var upper = phiKnotIndices[knotIndex + 1] - 1;
                for (int candidate = lower; candidate <= upper; candidate++)
                {
                    if (candidate == original)
                        continue;

                    phiKnotIndices[knotIndex] = candidate;
                    EvaluateApproximation(phiKnotIndices, exactX, transformedExactX, out var rootMeanSquareError, out var maximumError);
                    if (IsBetterScore(rootMeanSquareError, maximumError, bestRootMeanSquareError, bestMaximumError))
                    {
                        bestCandidate = candidate;
                        bestRootMeanSquareError = rootMeanSquareError;
                        bestMaximumError = maximumError;
                        improved = true;
                    }
                }

                phiKnotIndices[knotIndex] = bestCandidate;
            }

            if (!improved)
                break;
        }
    }

    private static void BasinHop(int[] phiKnotIndices, double[] exactX, double[] transformedExactX, ref double bestRootMeanSquareError, ref double bestMaximumError)
    {
        for (int attempt = 0; attempt < BasinHopAttemptCount; attempt++)
        {
            var working = (int[])phiKnotIndices.Clone();
            var removableKnotIndices = RankRemovableKnots(working, exactX, transformedExactX);
            var removedIndices = removableKnotIndices.Take(BasinHopRemovalCount).OrderByDescending(static index => index).ToArray();
            if (removedIndices.Length == 0)
                return;

            foreach (var removeIndex in removedIndices)
                working = RemoveAt(working, removeIndex);

            working = AddGreedyKnots(working, exactX, transformedExactX);
            EvaluateApproximation(working, exactX, transformedExactX, out var candidateRootMeanSquareError, out var candidateMaximumError);
            CoordinateDescent(working, exactX, transformedExactX, ref candidateRootMeanSquareError, ref candidateMaximumError);
            if (!IsBetterScore(candidateRootMeanSquareError, candidateMaximumError, bestRootMeanSquareError, bestMaximumError))
                continue;

            Array.Copy(working, phiKnotIndices, phiKnotIndices.Length);
            bestRootMeanSquareError = candidateRootMeanSquareError;
            bestMaximumError = candidateMaximumError;
        }
    }

    private static int[] RankRemovableKnots(int[] phiKnotIndices, double[] exactX, double[] transformedExactX)
    {
        var ranking = new List<(int KnotIndex, double RootMeanSquareError, double MaximumError)>();
        for (int i = 1; i < phiKnotIndices.Length - 1; i++)
        {
            var removed = RemoveAt(phiKnotIndices, i);
            EvaluateApproximation(removed, exactX, transformedExactX, out var rootMeanSquareError, out var maximumError);
            ranking.Add((i, rootMeanSquareError, maximumError));
        }

        return [.. ranking
            .OrderBy(tuple => tuple.RootMeanSquareError)
            .ThenBy(tuple => tuple.MaximumError)
            .Select(tuple => tuple.KnotIndex)];
    }

    private static int[] AddGreedyKnots(int[] phiKnotIndices, double[] exactX, double[] transformedExactX)
    {
        var selected = new bool[NistElasticPchip.EvaluationCount];
        foreach (var phiKnotIndex in phiKnotIndices)
            selected[phiKnotIndex] = true;

        var working = phiKnotIndices.ToList();
        while (working.Count < NistElasticPchip.KnotCount)
        {
            working.Sort();
            EvaluateApproximation([.. working], exactX, transformedExactX, out _, out _, out var transformedApproximation);

            var nextIndex = -1;
            var nextError = double.NegativeInfinity;
            for (int i = 1; i < NistElasticPchip.EvaluationCount - 1; i++)
            {
                if (selected[i])
                    continue;

                var error = Math.Abs(transformedApproximation[i] - transformedExactX[i]);
                if (error > nextError)
                {
                    nextError = error;
                    nextIndex = i;
                }
            }

            if (nextIndex < 0)
                throw new InvalidOperationException("Failed to reinsert a basin-hop knot.");

            selected[nextIndex] = true;
            working.Add(nextIndex);
        }

        working.Sort();
        return [.. working];
    }

    private static int[] RemoveAt(int[] source, int index)
    {
        var result = new int[source.Length - 1];
        if (index > 0)
            Array.Copy(source, 0, result, 0, index);
        if (index < source.Length - 1)
            Array.Copy(source, index + 1, result, index, source.Length - index - 1);
        return result;
    }

    private static void EvaluateApproximation(int[] phiKnotIndices, double[] exactX, double[] transformedExactX, out double rootMeanSquareError, out double maximumError)
        => EvaluateApproximation(phiKnotIndices, exactX, transformedExactX, out rootMeanSquareError, out maximumError, out _);

    private static void EvaluateApproximation(int[] phiKnotIndices, double[] exactX, double[] transformedExactX, out double rootMeanSquareError, out double maximumError, out double[] transformedApproximation)
    {
        var phiKnot = new double[phiKnotIndices.Length];
        var xKnot = new double[phiKnotIndices.Length];
        for (int i = 0; i < phiKnotIndices.Length; i++)
        {
            var knotIndex = phiKnotIndices[i];
            phiKnot[i] = NistElasticPchip.EvaluationPhiGrid[knotIndex];
            xKnot[i] = exactX[knotIndex];
        }

        var slope = NistElasticPchip.ComputeSlope(phiKnot, xKnot); // (260401Ch) runtime と同じ PCHIP 実装を共有する
        transformedApproximation = new double[NistElasticPchip.EvaluationCount];
        double sumOfSquaredError = 0.0;
        double maxAbsError = 0.0;
        var segmentIndex = 0;

        for (int i = 0; i < NistElasticPchip.EvaluationCount; i++)
        {
            var phi = NistElasticPchip.EvaluationPhiGrid[i];
            while (segmentIndex < phiKnot.Length - 2 && phi > phiKnot[segmentIndex + 1])
                segmentIndex++;

            var xApproximation = NistElasticPchip.EvaluateSegment(phiKnot, xKnot, slope, segmentIndex, phi);
            var transformedValue = NistElasticPchip.TransformErrorCoordinate(xApproximation);
            transformedApproximation[i] = transformedValue;

            var error = transformedValue - transformedExactX[i];
            sumOfSquaredError += error * error;
            maxAbsError = Math.Max(maxAbsError, Math.Abs(error));
        }

        rootMeanSquareError = Math.Sqrt(sumOfSquaredError / NistElasticPchip.EvaluationCount);
        maximumError = maxAbsError;
    }

    private static bool IsBetterScore(double rootMeanSquareError, double maximumError, double referenceRootMeanSquareError, double referenceMaximumError)
        => rootMeanSquareError < referenceRootMeanSquareError - 1.0E-15
            || (Math.Abs(rootMeanSquareError - referenceRootMeanSquareError) <= 1.0E-15 && maximumError < referenceMaximumError - 1.0E-15);

    private static string WriteGeneratedElementSource(string generatedDirectory, NistElasticPchipCompressedElement element)
    {
        var atomicNumberText = element.AtomicNumber.ToString("D2", CultureInfo.InvariantCulture);
        var path = Path.Combine(generatedDirectory, $"PCHIP{atomicNumberText}.cs"); // (260401Ch)
        var builder = new StringBuilder();
        builder.AppendLine("// <auto-generated />");
        builder.AppendLine($"// (260401Ch) Generated from {Path.GetFileName(element.SourcePath)} by NistElasticSamplerPchipGenerator.");
        builder.AppendLine("namespace Crystallography;");
        builder.AppendLine();
        builder.AppendLine("public static partial class AtomStatic");
        builder.AppendLine("{");
        builder.AppendLine($"    private static readonly double[] NistElasticPchipSigma{atomicNumberText} = [");
        for (int i = 0; i < element.Blocks.Length; i++)
            builder.AppendLine($"        {FormatDouble(element.Blocks[i].SigmaA0Squared)},");
        builder.AppendLine("    ];");
        builder.AppendLine();
        builder.AppendLine($"    private static readonly ushort[][] NistElasticPchipPhiKnotIndex{atomicNumberText} = [");
        for (int i = 0; i < element.Blocks.Length; i++)
            builder.AppendLine($"        [{string.Join(", ", element.Blocks[i].PhiKnotIndices)}],");
        builder.AppendLine("    ];");
        builder.AppendLine();
        builder.AppendLine($"    private static readonly float[][] NistElasticPchipXKnot{atomicNumberText} = [");
        for (int i = 0; i < element.Blocks.Length; i++)
            builder.AppendLine($"        [{string.Join(", ", element.Blocks[i].XKnot.Select(FormatFloat))}],");
        builder.AppendLine("    ];");
        builder.AppendLine();
        builder.AppendLine($"    private static void RegisterGeneratedNistElasticPchipE{atomicNumberText}(global::System.Collections.Generic.Dictionary<int, NistElasticPchipElementData> registry)");
        builder.AppendLine("    {");
        builder.AppendLine($"        registry[{element.AtomicNumber}] = new NistElasticPchipElementData({element.AtomicNumber}, NistElasticPchipSigma{atomicNumberText}, NistElasticPchipPhiKnotIndex{atomicNumberText}, NistElasticPchipXKnot{atomicNumberText});");
        builder.AppendLine("    }");
        builder.AppendLine("}");
        File.WriteAllText(path, builder.ToString(), new UTF8Encoding(false));
        return path;
    }

    private static string WriteDiagnosticsCsv(string diagnosticsDirectory, NistElasticPchipCompressedElement element)
    {
        var atomicNumberText = element.AtomicNumber.ToString("D2", CultureInfo.InvariantCulture);
        var path = Path.Combine(diagnosticsDirectory, $"PCHIP{atomicNumberText}.csv"); // (260401Ch)
        var builder = new StringBuilder();
        builder.AppendLine("AtomicNumber,BlockIndex,EnergyEv,SigmaA0Squared,WeightedRmsError,WeightedMaxError,ElapsedMilliseconds,PhiKnotIndices,XKnot");
        for (int i = 0; i < element.Blocks.Length; i++)
        {
            var block = element.Blocks[i];
            builder.AppendLine(string.Join(",",
                element.AtomicNumber.ToString(CultureInfo.InvariantCulture),
                (i + 1).ToString(CultureInfo.InvariantCulture),
                FormatDouble(block.EnergyEv),
                FormatDouble(block.SigmaA0Squared),
                FormatDouble(block.WeightedRootMeanSquareError),
                FormatDouble(block.WeightedMaximumError),
                block.Elapsed.TotalMilliseconds.ToString("F3", CultureInfo.InvariantCulture),
                QuoteCsv(string.Join(";", block.PhiKnotIndices)),
                QuoteCsv(string.Join(";", block.XKnot.Select(FormatCsvFloat)))));
        }

        File.WriteAllText(path, builder.ToString(), new UTF8Encoding(false));
        return path;
    }

    private static string WriteGeneratedRegistrySource(string generatedDirectory)
    {
        var elementPaths = Directory
            .EnumerateFiles(generatedDirectory, "PCHIP*.cs", SearchOption.TopDirectoryOnly)
            .Where(static path => !string.Equals(Path.GetFileName(path), "Registry.cs", StringComparison.OrdinalIgnoreCase))
            .OrderBy(ParseAtomicNumberFromGeneratedCodePath)
            .ToArray();

        var path = Path.Combine(generatedDirectory, "Registry.cs"); // (260401Ch)
        var builder = new StringBuilder();
        builder.AppendLine("// <auto-generated />");
        builder.AppendLine("// (260401Ch) Generated registry for compressed NIST elastic sampler data.");
        builder.AppendLine("namespace Crystallography;");
        builder.AppendLine();
        builder.AppendLine("public static partial class AtomStatic");
        builder.AppendLine("{");
        builder.AppendLine("    static partial void RegisterGeneratedNistElasticPchip(global::System.Collections.Generic.Dictionary<int, NistElasticPchipElementData> registry)");
        builder.AppendLine("    {");
        foreach (var elementPath in elementPaths)
        {
            var atomicNumberText = ParseAtomicNumberFromGeneratedCodePath(elementPath).ToString("D2", CultureInfo.InvariantCulture);
            builder.AppendLine($"        RegisterGeneratedNistElasticPchipE{atomicNumberText}(registry);");
        }
        builder.AppendLine("    }");
        builder.AppendLine("}");
        File.WriteAllText(path, builder.ToString(), new UTF8Encoding(false));
        return path;
    }

    private static NistElasticTextTable ReadTextTable(string path)
    {
        using var reader = new StreamReader(path, Encoding.UTF8, true);
        var sigmaA0Squared = new double[NistElasticPchip.EnergyCount];
        var phi = new double[NistElasticPchip.EnergyCount][];
        for (int blockIndex = 0; blockIndex < NistElasticPchip.EnergyCount; blockIndex++)
        {
            var blockNumber = int.Parse(ReadRequiredLine(reader), CultureInfo.InvariantCulture);
            if (blockNumber != blockIndex + 1)
                throw new InvalidDataException($"Unexpected block number {blockNumber} in {path}.");

            sigmaA0Squared[blockIndex] = double.Parse(ReadRequiredLine(reader), CultureInfo.InvariantCulture);
            var phiArray = new double[NistElasticPchip.SourcePhiCount];
            for (int i = 0; i < NistElasticPchip.SourcePhiCount; i++)
                phiArray[i] = double.Parse(ReadRequiredLine(reader), CultureInfo.InvariantCulture);
            phi[blockIndex] = phiArray;
        }

        return new NistElasticTextTable(sigmaA0Squared, phi);
    }

    private static string ReadRequiredLine(StreamReader reader)
    {
        var line = reader.ReadLine();
        if (line == null)
            throw new EndOfStreamException("Unexpected end of sampler file.");
        return line.Trim();
    }

    private static IEnumerable<DirectoryInfo> EnumerateCandidateDirectories(string startPath)
    {
        if (!string.IsNullOrWhiteSpace(startPath))
        {
            if (Directory.Exists(startPath))
                yield return new DirectoryInfo(Path.GetFullPath(startPath));
            else if (File.Exists(startPath))
                yield return new FileInfo(Path.GetFullPath(startPath)).Directory;
        }

        if (Directory.Exists(AppContext.BaseDirectory))
            yield return new DirectoryInfo(Path.GetFullPath(AppContext.BaseDirectory));
        if (Directory.Exists(Environment.CurrentDirectory))
            yield return new DirectoryInfo(Path.GetFullPath(Environment.CurrentDirectory));
    }

    private static string QuoteCsv(string value)
        => $"\"{value.Replace("\"", "\"\"", StringComparison.Ordinal)}\"";

    private static string FormatDouble(double value)
        => value.ToString("G17", CultureInfo.InvariantCulture);

    private static string FormatFloat(float value)
        => value.ToString("G9", CultureInfo.InvariantCulture) + "f";

    private static string FormatCsvFloat(float value)
        => value.ToString("G9", CultureInfo.InvariantCulture);

    private sealed class NistElasticTextTable(double[] sigmaA0Squared, double[][] phi)
    {
        public double[] SigmaA0Squared { get; } = sigmaA0Squared;
        public double[][] Phi { get; } = phi;
    }
}

public static partial class AtomStatic
{
    private static readonly Dictionary<int, NistElasticPchipElementData> GeneratedNistElasticPchipElements = []; // (260401Ch)
    private static readonly Dictionary<int, NistElasticPchipRuntimeElement> GeneratedNistElasticPchipRuntimeElements = []; // (260401Ch)
    private static readonly object GeneratedNistElasticPchipRuntimeSync = new(); // (260401Ch)

    static partial void RegisterGeneratedNistElasticPchip(Dictionary<int, NistElasticPchipElementData> registry); // (260401Ch)

    public static bool TryGetGeneratedNistElasticPchipElement(int atomicNumber, out NistElasticPchipElementData element)
        => GeneratedNistElasticPchipElements.TryGetValue(atomicNumber, out element);

    internal static bool TryGetGeneratedNistElasticPchipRuntimeElement(int atomicNumber, out NistElasticPchipRuntimeElement runtimeElement)
    {
        lock (GeneratedNistElasticPchipRuntimeSync)
        {
            if (GeneratedNistElasticPchipRuntimeElements.TryGetValue(atomicNumber, out runtimeElement))
                return true;
        }

        if (!GeneratedNistElasticPchipElements.TryGetValue(atomicNumber, out var generatedElement))
        {
            runtimeElement = null;
            return false;
        }

        var builtRuntimeElement = BuildGeneratedNistElasticPchipRuntimeElement(generatedElement);
        if (builtRuntimeElement is null)
        {
            runtimeElement = null;
            return false;
        }

        lock (GeneratedNistElasticPchipRuntimeSync)
        {
            if (!GeneratedNistElasticPchipRuntimeElements.TryGetValue(atomicNumber, out runtimeElement))
            {
                GeneratedNistElasticPchipRuntimeElements[atomicNumber] = builtRuntimeElement;
                runtimeElement = builtRuntimeElement;
            }
        }
        return true;
    }

    internal static bool TryEvaluateGeneratedNistElasticPchipCosTheta(NistElasticPchipRuntimeElement runtimeElement, int energyIndex, double targetPhi, out double cosTheta)
    {
        cosTheta = double.NaN;
        if (runtimeElement is null || energyIndex < 0 || energyIndex >= runtimeElement.PhiKnot.Length)
            return false;

        var phiKnot = runtimeElement.PhiKnot[energyIndex];
        var xKnot = runtimeElement.XKnot[energyIndex];
        var slope = runtimeElement.Slope[energyIndex];
        if (phiKnot is null || xKnot is null || slope is null)
            return false;

        // cosTheta = NistElasticPchip.Evaluate(phiKnot, xKnot, slope, Math.Clamp(targetPhi, 0.0, 1.0)); // 260401Cl 旧: Array.BinarySearch による O(log n) 探索
        // 260401Cl ガイドテーブルで O(1) に高速化
        var clampedTarget = Math.Clamp(targetPhi, 0.0, 1.0);
        var guide = runtimeElement.GuideTable?[energyIndex];
        if (guide is not null)
        {
            int guideIndex = Math.Min((int)(clampedTarget * (guide.Length - 1)), guide.Length - 1);
            int seg = guide[guideIndex];
            while (seg < phiKnot.Length - 2 && phiKnot[seg + 1] <= clampedTarget)
                seg++;
            cosTheta = NistElasticPchip.EvaluateSegment(phiKnot, xKnot, slope, seg, clampedTarget);
        }
        else
            cosTheta = NistElasticPchip.Evaluate(phiKnot, xKnot, slope, clampedTarget); // フォールバック
        return true;
    }

    private static NistElasticPchipRuntimeElement BuildGeneratedNistElasticPchipRuntimeElement(NistElasticPchipElementData generatedElement)
    {
        if (generatedElement is null
            || generatedElement.SigmaA0Squared is null
            || generatedElement.PhiKnotIndices is null
            || generatedElement.XKnot is null
            || generatedElement.SigmaA0Squared.Length != NistElasticPchip.EnergyCount
            || generatedElement.PhiKnotIndices.Length != NistElasticPchip.EnergyCount
            || generatedElement.XKnot.Length != NistElasticPchip.EnergyCount)
            return null;

        var phiKnot = new double[NistElasticPchip.EnergyCount][];
        var xKnot = new double[NistElasticPchip.EnergyCount][];
        var slope = new double[NistElasticPchip.EnergyCount][];
        var guideTable = new byte[NistElasticPchip.EnergyCount][]; // 260401Cl 追加

        for (int i = 0; i < NistElasticPchip.EnergyCount; i++)
        {
            var phiKnotIndices = generatedElement.PhiKnotIndices[i];
            var xKnotFloat = generatedElement.XKnot[i];
            if (phiKnotIndices is null
                || xKnotFloat is null
                || phiKnotIndices.Length != NistElasticPchip.KnotCount
                || xKnotFloat.Length != NistElasticPchip.KnotCount)
                return null;

            var phiKnotArray = new double[phiKnotIndices.Length];
            var xKnotArray = new double[xKnotFloat.Length];
            for (int j = 0; j < phiKnotIndices.Length; j++)
            {
                phiKnotArray[j] = phiKnotIndices[j] / (double)(NistElasticPchip.EvaluationCount - 1);
                xKnotArray[j] = xKnotFloat[j];
                if (j > 0 && phiKnotArray[j] <= phiKnotArray[j - 1])
                    return null;
            }

            phiKnot[i] = phiKnotArray;
            xKnot[i] = xKnotArray;
            slope[i] = NistElasticPchip.ComputeSlope(phiKnotArray, xKnotArray);

            // 260401Cl 追加: PCHIP バイナリサーチを O(1) に高速化するガイドテーブル構築
            const int guideSize = 64;
            var guide = new byte[guideSize];
            int seg = 0;
            for (int k = 0; k < guideSize; k++)
            {
                double v = k / (double)(guideSize - 1);
                while (seg < phiKnotArray.Length - 2 && phiKnotArray[seg + 1] <= v)
                    seg++;
                guide[k] = (byte)seg;
            }
            guideTable[i] = guide;
        }

        return new NistElasticPchipRuntimeElement(
            generatedElement.AtomicNumber,
            generatedElement.SigmaA0Squared,
            phiKnot,
            xKnot,
            slope,
            guideTable); // 260401Cl GuideTable 追加
    }
}
