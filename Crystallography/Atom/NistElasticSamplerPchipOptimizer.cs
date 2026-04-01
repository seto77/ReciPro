#region using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
#endregion

namespace Crystallography;

internal static class NistElasticSamplerPchipOptimizer
{
    private const int CoordinateDescentPassCount = 3; // (260401Ch)
    private const int BasinHopAttemptCount = 2; // (260401Ch)
    private const int BasinHopRemovalCount = 3; // (260401Ch)

    public static NistElasticPchipCompressedBlock CompressBlock(double[] phiOfX, double sigmaA0Squared, double energyEv, ushort[] previousPhiKnotIndices)
    {
        var stopwatch = Stopwatch.StartNew();
        var exactX = new double[NistElasticSamplerPchipGenerator.EvaluationCount];
        var transformedExactX = new double[NistElasticSamplerPchipGenerator.EvaluationCount];
        for (int i = 0; i < NistElasticSamplerPchipGenerator.EvaluationCount; i++)
        {
            var x = NistElasticSamplerPchipGenerator.InvertPhi(phiOfX, NistElasticSamplerPchipGenerator.EvaluationPhiGrid[i]);
            exactX[i] = x;
            transformedExactX[i] = NistElasticSamplerPchipGenerator.TransformErrorCoordinate(x);
        }

        var phiKnotIndices = TryCreateWarmStart(previousPhiKnotIndices) ?? CreateGreedyKnotIndices(exactX, transformedExactX);
        EvaluateApproximation(phiKnotIndices, exactX, transformedExactX, out var bestRootMeanSquareError, out var bestMaximumError);
        CoordinateDescent(phiKnotIndices, exactX, transformedExactX, ref bestRootMeanSquareError, ref bestMaximumError);
        BasinHop(phiKnotIndices, exactX, transformedExactX, ref bestRootMeanSquareError, ref bestMaximumError);

        var xKnot = new float[NistElasticSamplerPchipGenerator.KnotCount];
        for (int i = 0; i < NistElasticSamplerPchipGenerator.KnotCount; i++)
            xKnot[i] = (float)exactX[phiKnotIndices[i]];

        stopwatch.Stop();
        return new NistElasticPchipCompressedBlock
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
        if (previousPhiKnotIndices == null || previousPhiKnotIndices.Length != NistElasticSamplerPchipGenerator.KnotCount)
            return null;

        var warmStart = Array.ConvertAll(previousPhiKnotIndices, static index => (int)index);
        warmStart[0] = 0;
        warmStart[^1] = NistElasticSamplerPchipGenerator.EvaluationCount - 1;

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
        var selected = new bool[NistElasticSamplerPchipGenerator.EvaluationCount];
        selected[0] = true;
        selected[^1] = true;
        var phiKnotIndices = new List<int>(NistElasticSamplerPchipGenerator.KnotCount) { 0, NistElasticSamplerPchipGenerator.EvaluationCount - 1 };

        while (phiKnotIndices.Count < NistElasticSamplerPchipGenerator.KnotCount)
        {
            phiKnotIndices.Sort();
            EvaluateApproximation([.. phiKnotIndices], exactX, transformedExactX, out _, out _, out var transformedApproximation);

            var nextIndex = -1;
            var nextError = double.NegativeInfinity;
            for (int i = 1; i < NistElasticSamplerPchipGenerator.EvaluationCount - 1; i++)
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
        var selected = new bool[NistElasticSamplerPchipGenerator.EvaluationCount];
        foreach (var phiKnotIndex in phiKnotIndices)
            selected[phiKnotIndex] = true;

        var working = phiKnotIndices.ToList();
        while (working.Count < NistElasticSamplerPchipGenerator.KnotCount)
        {
            working.Sort();
            EvaluateApproximation([.. working], exactX, transformedExactX, out _, out _, out var transformedApproximation);

            var nextIndex = -1;
            var nextError = double.NegativeInfinity;
            for (int i = 1; i < NistElasticSamplerPchipGenerator.EvaluationCount - 1; i++)
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
            phiKnot[i] = NistElasticSamplerPchipGenerator.EvaluationPhiGrid[knotIndex];
            xKnot[i] = exactX[knotIndex];
        }

        var slope = ComputePchipSlope(phiKnot, xKnot);
        transformedApproximation = new double[NistElasticSamplerPchipGenerator.EvaluationCount];
        double sumOfSquaredError = 0.0;
        double maxAbsError = 0.0;
        var segmentIndex = 0;

        for (int i = 0; i < NistElasticSamplerPchipGenerator.EvaluationCount; i++)
        {
            var phi = NistElasticSamplerPchipGenerator.EvaluationPhiGrid[i];
            while (segmentIndex < phiKnot.Length - 2 && phi > phiKnot[segmentIndex + 1])
                segmentIndex++;

            var xApproximation = EvaluatePchipSegment(phiKnot, xKnot, slope, segmentIndex, phi);
            var transformedValue = NistElasticSamplerPchipGenerator.TransformErrorCoordinate(xApproximation);
            transformedApproximation[i] = transformedValue;

            var error = transformedValue - transformedExactX[i];
            sumOfSquaredError += error * error;
            maxAbsError = Math.Max(maxAbsError, Math.Abs(error));
        }

        rootMeanSquareError = Math.Sqrt(sumOfSquaredError / NistElasticSamplerPchipGenerator.EvaluationCount);
        maximumError = maxAbsError;
    }

    private static double[] ComputePchipSlope(double[] x, double[] y)
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

    private static double ComputeEndpointSlope(double h0, double h1, double delta0, double delta1)
    {
        var slope = ((2.0 * h0 + h1) * delta0 - h0 * delta1) / (h0 + h1);
        if (Math.Sign(slope) != Math.Sign(delta0))
            return 0.0;
        if (Math.Sign(delta0) != Math.Sign(delta1) && Math.Abs(slope) > Math.Abs(3.0 * delta0))
            return 3.0 * delta0;
        return slope;
    }

    private static double EvaluatePchipSegment(double[] x, double[] y, double[] slope, int segmentIndex, double value)
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

    private static bool IsBetterScore(double rootMeanSquareError, double maximumError, double referenceRootMeanSquareError, double referenceMaximumError)
        => rootMeanSquareError < referenceRootMeanSquareError - 1.0E-15
            || (Math.Abs(rootMeanSquareError - referenceRootMeanSquareError) <= 1.0E-15 && maximumError < referenceMaximumError - 1.0E-15);
}
