#region using
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
#endregion

namespace Crystallography;

public sealed class NistElasticCompressionProgress // (260401Ch) FormEBSD の status strip へ進捗を流す
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

public static class NistElasticSamplerPchipGenerator
{
    public const int EnergyCount = 101; // (260401Ch)
    public const int SourcePhiCount = 2001; // (260401Ch)
    public const int KnotCount = 51; // (260401Ch)
    public const int EvaluationCount = 4097; // (260401Ch)

    internal const double LogMinimumEnergyEv = 3.912023005428146; // (260401Ch) log(50)
    internal const double LogEnergyStep = 0.059914645471079815; // (260401Ch) (log(20000)-log(50))/100
    internal static readonly double[] EvaluationPhiGrid = CreateEvaluationPhiGrid(); // (260401Ch)

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

    public static IReadOnlyList<string> GenerateCompressedSources(IEnumerable<string> sourcePaths, string repositoryRoot, IProgress<NistElasticCompressionProgress> progress = null)
    {
        if (sourcePaths == null)
            throw new ArgumentNullException(nameof(sourcePaths));
        if (string.IsNullOrWhiteSpace(repositoryRoot))
            throw new ArgumentException("Repository root must not be empty.", nameof(repositoryRoot));

        var normalizedSources = sourcePaths
            .Where(path => !string.IsNullOrWhiteSpace(path))
            .Select(Path.GetFullPath)
            .Where(File.Exists)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(ParseAtomicNumberFromPath)
            .ToArray();

        if (normalizedSources.Length == 0)
            return [];

        var generatedDirectory = Path.Combine(repositoryRoot, "Crystallography", "Atom", "Generated");
        var diagnosticsDirectory = Path.Combine(repositoryRoot, "Crystallography", "Atom", "GeneratedDiagnostics");
        Directory.CreateDirectory(generatedDirectory);
        Directory.CreateDirectory(diagnosticsDirectory);

        var outputs = new List<string>();
        for (int fileIndex = 0; fileIndex < normalizedSources.Length; fileIndex++)
        {
            var sourcePath = normalizedSources[fileIndex];
            var atomicNumber = ParseAtomicNumberFromPath(sourcePath);
            var elementResult = CompressElement(sourcePath, atomicNumber, fileIndex, normalizedSources.Length, progress);

            var generatedCodePath = WriteGeneratedElementSource(generatedDirectory, elementResult);
            var diagnosticsCsvPath = WriteDiagnosticsCsv(diagnosticsDirectory, elementResult);
            outputs.Add(generatedCodePath);
            outputs.Add(diagnosticsCsvPath);
        }

        outputs.Add(WriteGeneratedRegistrySource(generatedDirectory));
        return outputs;
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

    internal static double FormatEnergyEv(int blockIndex)
        => Math.Exp(LogMinimumEnergyEv + LogEnergyStep * blockIndex);

    internal static double TransformErrorCoordinate(double x)
        => Math.Sqrt(Math.Max(0.0, 1.0 - Math.Clamp(x, -1.0, 1.0)));

    internal static double InvertPhi(double[] phiOfX, double phi)
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

    private static NistElasticPchipCompressedElement CompressElement(string sourcePath, int atomicNumber, int fileIndex, int fileCount, IProgress<NistElasticCompressionProgress> progress)
    {
        var table = ReadTextTable(sourcePath);
        var blocks = new NistElasticPchipCompressedBlock[EnergyCount];
        ushort[] previousPhiKnotIndices = null;

        for (int blockIndex = 0; blockIndex < EnergyCount; blockIndex++)
        {
            progress?.Report(new NistElasticCompressionProgress
            {
                FileIndex = fileIndex + 1,
                FileCount = fileCount,
                AtomicNumber = atomicNumber,
                SourcePath = sourcePath,
                BlockIndex = blockIndex + 1,
                BlockCount = EnergyCount,
                Phase = "Compressing",
                FileProgress = blockIndex / (double)EnergyCount,
                OverallProgress = (fileIndex + blockIndex / (double)EnergyCount) / fileCount,
            });

            blocks[blockIndex] = NistElasticSamplerPchipOptimizer.CompressBlock(
                table.Phi[blockIndex],
                table.SigmaA0Squared[blockIndex],
                FormatEnergyEv(blockIndex),
                previousPhiKnotIndices);

            previousPhiKnotIndices = [.. blocks[blockIndex].PhiKnotIndices];
        }

        progress?.Report(new NistElasticCompressionProgress
        {
            FileIndex = fileIndex + 1,
            FileCount = fileCount,
            AtomicNumber = atomicNumber,
            SourcePath = sourcePath,
            BlockIndex = EnergyCount,
            BlockCount = EnergyCount,
            Phase = "Completed",
            FileProgress = 1.0,
            OverallProgress = (fileIndex + 1.0) / fileCount,
        });

        return new NistElasticPchipCompressedElement
        {
            AtomicNumber = atomicNumber,
            SourcePath = sourcePath,
            Blocks = blocks,
        };
    }

    private static string WriteGeneratedElementSource(string generatedDirectory, NistElasticPchipCompressedElement element)
    {
        var atomicNumberText = element.AtomicNumber.ToString("D2", CultureInfo.InvariantCulture);
        var path = Path.Combine(generatedDirectory, $"AtomStatic.NistElasticPchip.E_{atomicNumberText}.generated.cs");
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
        var path = Path.Combine(diagnosticsDirectory, $"AtomStatic.NistElasticPchip.E_{atomicNumberText}.csv");
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
            .EnumerateFiles(generatedDirectory, "AtomStatic.NistElasticPchip.E_*.generated.cs", SearchOption.TopDirectoryOnly)
            .OrderBy(ParseAtomicNumberFromPath)
            .ToArray();

        var path = Path.Combine(generatedDirectory, "AtomStatic.NistElasticPchip.generated.cs");
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
            var atomicNumberText = ParseAtomicNumberFromPath(elementPath).ToString("D2", CultureInfo.InvariantCulture);
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
        var sigmaA0Squared = new double[EnergyCount];
        var phi = new double[EnergyCount][];
        for (int blockIndex = 0; blockIndex < EnergyCount; blockIndex++)
        {
            var blockNumber = int.Parse(ReadRequiredLine(reader), CultureInfo.InvariantCulture);
            if (blockNumber != blockIndex + 1)
                throw new InvalidDataException($"Unexpected block number {blockNumber} in {path}.");

            sigmaA0Squared[blockIndex] = double.Parse(ReadRequiredLine(reader), CultureInfo.InvariantCulture);
            var phiArray = new double[SourcePhiCount];
            for (int i = 0; i < SourcePhiCount; i++)
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
        => value.ToString("G9", CultureInfo.InvariantCulture); // (260401Ch) CSV は Excel / Python で読みやすいよう C# の f suffix を付けない

    private static double[] CreateEvaluationPhiGrid()
    {
        var grid = new double[EvaluationCount];
        for (int i = 0; i < EvaluationCount; i++)
            grid[i] = i / (double)(EvaluationCount - 1);
        return grid;
    }

    private sealed class NistElasticTextTable(double[] sigmaA0Squared, double[][] phi)
    {
        public double[] SigmaA0Squared { get; } = sigmaA0Squared;
        public double[][] Phi { get; } = phi;
    }
}
