#region using
using System;
using System.Collections.Generic;
#endregion

namespace Crystallography;

public sealed class NistElasticPchipElementData // (260401Ch) generated NIST elastic sampler の圧縮データ受け皿
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

internal sealed class NistElasticPchipRuntimeElement // (260401Ch) generated データから PCHIP 係数を前計算した runtime 用キャッシュ
{
    public NistElasticPchipRuntimeElement(int atomicNumber, double[] sigmaA0Squared, double[][] phiKnot, double[][] xKnot, double[][] slope)
    {
        AtomicNumber = atomicNumber;
        SigmaA0Squared = sigmaA0Squared;
        PhiKnot = phiKnot;
        XKnot = xKnot;
        Slope = slope;
    }

    public int AtomicNumber { get; }
    public double[] SigmaA0Squared { get; }
    public double[][] PhiKnot { get; }
    public double[][] XKnot { get; }
    public double[][] Slope { get; }
}

public static partial class AtomStatic
{
    private static readonly Dictionary<int, NistElasticPchipElementData> GeneratedNistElasticPchipElements = []; // (260401Ch) 生の generated elastic sampler
    private static readonly Dictionary<int, NistElasticPchipRuntimeElement> GeneratedNistElasticPchipRuntimeElements = []; // (260401Ch) PCHIP 係数前計算済みキャッシュ
    private static readonly object GeneratedNistElasticPchipRuntimeSync = new(); // (260401Ch)

    static partial void RegisterGeneratedNistElasticPchip(Dictionary<int, NistElasticPchipElementData> registry); // (260401Ch) generated partial 側で元素データを登録

    public static bool TryGetGeneratedNistElasticPchipElement(int atomicNumber, out NistElasticPchipElementData element)
        => GeneratedNistElasticPchipElements.TryGetValue(atomicNumber, out element); // (260401Ch)

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

        cosTheta = EvaluateGeneratedNistElasticPchipSegment(phiKnot, xKnot, slope, Math.Clamp(targetPhi, 0.0, 1.0));
        return true;
    }

    private static NistElasticPchipRuntimeElement BuildGeneratedNistElasticPchipRuntimeElement(NistElasticPchipElementData generatedElement)
    {
        if (generatedElement is null
            || generatedElement.SigmaA0Squared is null
            || generatedElement.PhiKnotIndices is null
            || generatedElement.XKnot is null
            || generatedElement.SigmaA0Squared.Length != NistElasticSamplerPchipGenerator.EnergyCount
            || generatedElement.PhiKnotIndices.Length != NistElasticSamplerPchipGenerator.EnergyCount
            || generatedElement.XKnot.Length != NistElasticSamplerPchipGenerator.EnergyCount)
            return null;

        var phiKnot = new double[NistElasticSamplerPchipGenerator.EnergyCount][];
        var xKnot = new double[NistElasticSamplerPchipGenerator.EnergyCount][];
        var slope = new double[NistElasticSamplerPchipGenerator.EnergyCount][];

        for (int i = 0; i < NistElasticSamplerPchipGenerator.EnergyCount; i++)
        {
            var phiKnotIndices = generatedElement.PhiKnotIndices[i];
            var xKnotFloat = generatedElement.XKnot[i];
            if (phiKnotIndices is null
                || xKnotFloat is null
                || phiKnotIndices.Length != NistElasticSamplerPchipGenerator.KnotCount
                || xKnotFloat.Length != NistElasticSamplerPchipGenerator.KnotCount)
                return null;

            var phiKnotArray = new double[phiKnotIndices.Length];
            var xKnotArray = new double[xKnotFloat.Length];
            for (int j = 0; j < phiKnotIndices.Length; j++)
            {
                phiKnotArray[j] = phiKnotIndices[j] / (double)(NistElasticSamplerPchipGenerator.EvaluationCount - 1); // (260401Ch) ushort index -> Φ in [0,1]
                xKnotArray[j] = xKnotFloat[j];
                if (j > 0 && phiKnotArray[j] <= phiKnotArray[j - 1])
                    return null;
            }

            phiKnot[i] = phiKnotArray;
            xKnot[i] = xKnotArray;
            slope[i] = ComputeGeneratedNistElasticPchipSlope(phiKnotArray, xKnotArray);
        }

        return new NistElasticPchipRuntimeElement(
            generatedElement.AtomicNumber,
            generatedElement.SigmaA0Squared,
            phiKnot,
            xKnot,
            slope);
    }

    private static double[] ComputeGeneratedNistElasticPchipSlope(double[] phiKnot, double[] xKnot)
    {
        var n = phiKnot.Length;
        var h = new double[n - 1];
        var delta = new double[n - 1];
        for (int i = 0; i < n - 1; i++)
        {
            h[i] = phiKnot[i + 1] - phiKnot[i];
            delta[i] = (xKnot[i + 1] - xKnot[i]) / h[i];
        }

        var slope = new double[n];
        if (n == 2)
        {
            slope[0] = delta[0];
            slope[1] = delta[0];
            return slope;
        }

        slope[0] = ComputeGeneratedNistElasticPchipEndpointSlope(h[0], h[1], delta[0], delta[1]);
        slope[^1] = ComputeGeneratedNistElasticPchipEndpointSlope(h[^1], h[^2], delta[^1], delta[^2]);
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

    private static double ComputeGeneratedNistElasticPchipEndpointSlope(double h0, double h1, double delta0, double delta1)
    {
        var slope = ((2.0 * h0 + h1) * delta0 - h0 * delta1) / (h0 + h1);
        if (Math.Sign(slope) != Math.Sign(delta0))
            return 0.0;
        if (Math.Sign(delta0) != Math.Sign(delta1) && Math.Abs(slope) > Math.Abs(3.0 * delta0))
            return 3.0 * delta0;
        return slope;
    }

    private static double EvaluateGeneratedNistElasticPchipSegment(double[] phiKnot, double[] xKnot, double[] slope, double targetPhi)
    {
        if (targetPhi <= phiKnot[0])
            return xKnot[0];
        if (targetPhi >= phiKnot[^1])
            return xKnot[^1];

        int upper = Array.BinarySearch(phiKnot, targetPhi);
        if (upper < 0)
            upper = ~upper;
        upper = Math.Clamp(upper, 1, phiKnot.Length - 1);
        int lower = upper - 1;

        var h = phiKnot[upper] - phiKnot[lower];
        var t = (targetPhi - phiKnot[lower]) / h;
        var t2 = t * t;
        var t3 = t2 * t;
        var h00 = 2.0 * t3 - 3.0 * t2 + 1.0;
        var h10 = t3 - 2.0 * t2 + t;
        var h01 = -2.0 * t3 + 3.0 * t2;
        var h11 = t3 - t2;
        var x = h00 * xKnot[lower]
            + h10 * h * slope[lower]
            + h01 * xKnot[upper]
            + h11 * h * slope[upper];
        return Math.Clamp(x, -1.0, 1.0);
    }
}
