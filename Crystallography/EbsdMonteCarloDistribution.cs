using System;
using System.Collections.Generic;
using System.Linq;
using V3 = OpenTK.Mathematics.Vector3d;

namespace Crystallography;

/// <summary>
/// モンテカルロ BSE シミュレーション結果から、検出器の 8×8 ビン毎に
/// エネルギー・深さの重み分布 w(E, z) = g(E) × h(z|E) をフィッティングし、
/// 任意のピクセル位置で重みを返すクラス。260325Cl 追加
/// </summary>
public sealed class EbsdMonteCarloDistribution
{
    public int BinCount { get; }
    public double[] Energies { get; }
    public double[] Depths { get; }

    /// <summary>
    /// ビンごとの正規化重み。BinWeights[binI, binJ] は double[Energies.Length * Depths.Length]。
    /// </summary>
    public double[,][] BinWeights { get; }

    /// <summary>
    /// model 2 用。detector bin の absolute 強度を保った depth-slice 重み。260325Ch 追加
    /// </summary>
    public double[,][] BinAbsoluteSliceWeights { get; }

    public EbsdMonteCarloDistribution(
        (double Depth, V3 Vec, double Energy)[] bseList,
        double beamEnergy,
        double smpTilt, double detTilt,
        double detY, double detZ, double detR,
        double[] energies, double[] depths,
        int binCount = 8)
    {
        BinCount = binCount;
        Energies = energies;
        Depths = depths;

        var (sinDet, cosDet) = Math.SinCos(detTilt);
        double dNumer = -(detY * sinDet + detZ * cosDet);

        var bins = new List<(double depth, double energy)>[binCount, binCount];
        for (int i = 0; i < binCount; i++)
            for (int j = 0; j < binCount; j++)
                bins[i, j] = new List<(double, double)>();

        foreach (var (depth, vec, energy) in bseList)
        {
            double dDenom = vec.Y * sinDet + vec.Z * cosDet;
            if (Math.Abs(dDenom) < 1e-15) continue;
            double k = dNumer / dDenom;
            if (k <= 0) continue;

            double localY = cosDet * (k * vec.Y + detY) - sinDet * (k * vec.Z + detZ);
            double px = k * vec.X / detR;
            double py = localY / detR;

            if (px < -1 || px > 1 || py < -1 || py > 1) continue;

            int bi = Math.Clamp((int)((px + 1) * 0.5 * binCount), 0, binCount - 1);
            int bj = Math.Clamp((int)((1 - py) * 0.5 * binCount), 0, binCount - 1);
            bins[bi, bj].Add((depth, energy));
        }

        // BinWeights = new double[binCount, binCount][]; // (260325Ch) 旧実装
        BinWeights = new double[binCount, binCount][];
        BinAbsoluteSliceWeights = new double[binCount, binCount][]; // (260325Ch) model 2 用
        int eLen = energies.Length, dLen = depths.Length;

        for (int bi = 0; bi < binCount; bi++)
            for (int bj = 0; bj < binCount; bj++)
            {
                var binData = bins[bi, bj];
                var weights = new double[eLen * dLen];
                var absoluteSliceWeights = new double[eLen * dLen]; // (260325Ch) model 2 用
                double binFraction = bseList.Length > 0 ? (double)binData.Count / bseList.Length : 0.0; // (260325Ch)

                if (binData.Count < 10)
                {
                    double uniform = binData.Count > 0 ? 1.0 / (eLen * dLen) : 0.0;
                    Array.Fill(weights, uniform);

                    double absoluteUniform = binData.Count > 0 ? binFraction / (eLen * dLen) : 0.0; // (260325Ch)
                    Array.Fill(absoluteSliceWeights, absoluteUniform);
                }
                else
                {
                    // FitBinDistribution(binData, beamEnergy, energies, depths, weights); // (260325Ch) 旧実装
                    FitBinDistribution(binData, beamEnergy, energies, depths, weights);
                    FitBinDistribution(binData, beamEnergy, energies, depths, absoluteSliceWeights, useSliceMass: true, totalScale: binFraction); // (260325Ch)
                }

                BinWeights[bi, bj] = weights;
                BinAbsoluteSliceWeights[bi, bj] = absoluteSliceWeights; // (260325Ch)
            }
    }

    private static void FitBinDistribution(
        List<(double depth, double energy)> data,
        double beamEnergy,
        double[] energies, double[] depths,
        double[] weights,
        bool useSliceMass = false,
        double totalScale = 1.0)
    {
        int eLen = energies.Length, dLen = depths.Length;
        int count = data.Count;
        if (count == 0) return;

        double sumE = 0;
        foreach (var d in data) sumE += d.energy;
        double meanE = sumE / count;

        double varL = 0, varR = 0;
        int nL = 0, nR = 0;
        foreach (var d in data)
        {
            if (d.energy < meanE) { varL += (d.energy - meanE) * (d.energy - meanE); nL++; }
            else { varR += (d.energy - meanE) * (d.energy - meanE); nR++; }
        }
        double sigmaL = nL > 1 ? Math.Sqrt(varL / nL) : 0.5;
        double sigmaR = nR > 1 ? Math.Sqrt(varR / nR) : 0.5;
        double Ep = meanE;
        if (sigmaL < 0.01) sigmaL = 0.5;
        if (sigmaR < 0.01) sigmaR = 0.5;

        var gE = new double[eLen];
        for (int ei = 0; ei < eLen; ei++)
        {
            double dE = energies[ei] - Ep;
            double sigma = dE < 0 ? sigmaL : sigmaR;
            gE[ei] = Math.Exp(-dE * dE / (2 * sigma * sigma));
        }

        double eStep = eLen > 1 ? Math.Abs(energies[0] - energies[^1]) / (eLen - 1) : 1.0;
        var lambdaPerEnergy = new double[eLen];

        for (int ei = 0; ei < eLen; ei++)
        {
            double eLow = energies[ei] - eStep * 0.5;
            double eHigh = energies[ei] + eStep * 0.5;

            var depthsInBin = new List<double>();
            foreach (var d in data)
                if (d.energy >= eLow && d.energy < eHigh)
                    depthsInBin.Add(d.depth);

            if (depthsInBin.Count > 3)
            {
                double sum = 0;
                foreach (var z in depthsInBin) sum += z;
                lambdaPerEnergy[ei] = Math.Max(1.0, sum / depthsInBin.Count);
            }
            else
            {
                lambdaPerEnergy[ei] = -1;
            }
        }

        FitLambdaPolynomial(energies, lambdaPerEnergy, out double la, out double lb, out double lc);

        double totalWeight = 0;
        for (int ei = 0; ei < eLen; ei++)
        {
            double lambda = la + lb * energies[ei] + lc * energies[ei] * energies[ei];
            if (lambda < 1.0) lambda = 1.0;

            for (int di = 0; di < dLen; di++)
            {
                // double w = gE[ei] * Math.Exp(-depths[di] / lambda) / lambda; // (260325Ch) 旧実装
                double w;
                if (useSliceMass)
                {
                    double lowerDepth = di == 0 ? 0.0 : depths[di - 1];
                    double upperDepth = depths[di];
                    w = gE[ei] * (Math.Exp(-lowerDepth / lambda) - Math.Exp(-upperDepth / lambda)); // (260325Ch) model 2 は depth slice 区間質量
                }
                else
                {
                    w = gE[ei] * Math.Exp(-depths[di] / lambda) / lambda;
                }

                weights[ei * dLen + di] = w;
                totalWeight += w;
            }
        }

        if (totalWeight > 0)
            for (int k = 0; k < weights.Length; k++)
                // weights[k] /= totalWeight; // (260325Ch) 旧実装
                weights[k] = weights[k] / totalWeight * totalScale; // (260325Ch)
    }

    private static void FitLambdaPolynomial(double[] energies, double[] lambdaValues, out double a, out double b, out double c)
    {
        var validE = new List<double>();
        var validL = new List<double>();
        for (int i = 0; i < energies.Length; i++)
            if (lambdaValues[i] > 0)
            {
                validE.Add(energies[i]);
                validL.Add(lambdaValues[i]);
            }

        if (validE.Count == 0)
        {
            a = 10; b = 0; c = 0;
            return;
        }
        if (validE.Count == 1)
        {
            a = validL[0]; b = 0; c = 0;
            return;
        }
        if (validE.Count == 2)
        {
            double e0 = validE[0], e1 = validE[1], l0 = validL[0], l1 = validL[1];
            b = (l1 - l0) / (e1 - e0);
            a = l0 - b * e0;
            c = 0;
            return;
        }

        int n = validE.Count;
        double s0 = n, s1 = 0, s2 = 0, s3 = 0, s4 = 0;
        double r0 = 0, r1 = 0, r2 = 0;
        for (int k = 0; k < n; k++)
        {
            double e = validE[k], l = validL[k];
            double e2 = e * e;
            s1 += e; s2 += e2; s3 += e * e2; s4 += e2 * e2;
            r0 += l; r1 += l * e; r2 += l * e2;
        }

        double det = s0 * (s2 * s4 - s3 * s3) - s1 * (s1 * s4 - s3 * s2) + s2 * (s1 * s3 - s2 * s2);
        if (Math.Abs(det) < 1e-30)
        {
            a = r0 / s0; b = 0; c = 0;
            return;
        }

        double invDet = 1.0 / det;
        a = ((s2 * s4 - s3 * s3) * r0 + (s2 * s3 - s1 * s4) * r1 + (s1 * s3 - s2 * s2) * r2) * invDet;
        b = ((s2 * s3 - s1 * s4) * r0 + (s0 * s4 - s2 * s2) * r1 + (s1 * s2 - s0 * s3) * r2) * invDet;
        c = ((s1 * s3 - s2 * s2) * r0 + (s1 * s2 - s0 * s3) * r1 + (s0 * s2 - s1 * s1) * r2) * invDet;
    }

    public double[] GetPixelWeights(double detX, double detY)
    {
        int eLen = Energies.Length, dLen = Depths.Length;
        int wLen = eLen * dLen;
        var result = new double[wLen];

        double bx = (detX + 1) * 0.5 * BinCount - 0.5;
        double by = (1 - detY) * 0.5 * BinCount - 0.5;

        int bi0 = Math.Clamp((int)Math.Floor(bx), 0, BinCount - 2);
        int bj0 = Math.Clamp((int)Math.Floor(by), 0, BinCount - 2);
        double fx = Math.Clamp(bx - bi0, 0, 1);
        double fy = Math.Clamp(by - bj0, 0, 1);

        var w00 = BinWeights[bi0, bj0];
        var w10 = BinWeights[bi0 + 1, bj0];
        var w01 = BinWeights[bi0, bj0 + 1];
        var w11 = BinWeights[bi0 + 1, bj0 + 1];

        double c00 = (1 - fx) * (1 - fy);
        double c10 = fx * (1 - fy);
        double c01 = (1 - fx) * fy;
        double c11 = fx * fy;

        for (int k = 0; k < wLen; k++)
            result[k] = c00 * w00[k] + c10 * w10[k] + c01 * w01[k] + c11 * w11[k];

        return result;
    }

    public static (double energyLoss80, double depth99) ComputeRangesFromMC(
        (double Depth, V3 Vec, double Energy)[] bseList,
        double beamEnergy)
    {
        if (bseList == null || bseList.Length == 0)
            return (5.0, 50.0);

        int n = bseList.Length;
        var losses = new double[n];
        var depths = new double[n];
        for (int i = 0; i < n; i++)
        {
            losses[i] = beamEnergy - bseList[i].Energy;
            depths[i] = bseList[i].Depth;
        }

        Array.Sort(losses);
        Array.Sort(depths);

        int idxLoss80 = Math.Min((int)(n * 0.8), n - 1);
        int idxDepth99 = Math.Min((int)(n * 0.99), n - 1); // (260326Ch)

        return (losses[idxLoss80], depths[idxDepth99]);
    }

    public static (double[] energies, double energyStart, double energyEnd, double energyStep,
                    double[] depths, double depthStart, double depthEnd, double depthStep)
        ComputeGridFromRanges(double beamEnergy, double energyLoss80, double depth99)
    {
        int numEnergyLevels = 8;
        double rawEnergyStep = energyLoss80 / (numEnergyLevels - 1);
        double energyStep = Math.Max(0.1, Math.Round(rawEnergyStep / 0.1) * 0.1);
        double energyStart = beamEnergy;
        double energyEnd = Math.Round((beamEnergy - energyStep * (numEnergyLevels - 1)) / 0.1) * 0.1;
        if (energyEnd < 1) energyEnd = 1;

        var energyList = new List<double>();
        for (double e = energyStart; e >= energyEnd - 0.001; e -= energyStep)
            energyList.Add(Math.Round(e * 10) / 10.0);
        var energies = energyList.ToArray();

        int maxDepthDivisions = 40;
        double depthStep = Math.Max(0.01, Math.Round(depth99 / maxDepthDivisions * 100) / 100.0); // (260326Ch)
        double depthStart = depthStep;
        double depthEnd = Math.Max(depthStep, Math.Round(depth99 * 100) / 100.0); // (260326Ch)
        var depthList = new List<double>();
        for (double d = depthStart; d <= depthEnd + 0.0001; d += depthStep)
            depthList.Add(Math.Round(d * 100) / 100.0);
        if (depthList.Count > maxDepthDivisions)
            depthList.RemoveRange(maxDepthDivisions, depthList.Count - maxDepthDivisions);
        var depths = depthList.ToArray();

        return (energies, energyStart, energyEnd, energyStep,
                depths, depthStart, depthEnd, depthStep);
    }
}
