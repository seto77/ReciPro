using System;
using System.Collections.Generic;
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

    /// <summary>ビンごとの正規化重み。BinWeights[binI, binJ] は double[energyCount * depthCount]。 // (260327Ch)</summary>
    public double[,][] BinWeights { get; }

    /// <summary>model 2 用。detector bin の absolute 強度を保った depth-slice 重み。260325Ch 追加</summary>
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

    // private static void FitBinDistribution(... 旧シグネチャ同じ) // 260327Cl 最適化
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

        // 260327Cl: 2*sigma*sigma を事前計算して Exp 内の除算を軽減
        double inv2SigmaSqL = 1.0 / (2 * sigmaL * sigmaL);
        double inv2SigmaSqR = 1.0 / (2 * sigmaR * sigmaR);

        var gE = new double[eLen];
        for (int ei = 0; ei < eLen; ei++)
        {
            double dE = energies[ei] - Ep;
            double inv2SigmaSq = dE < 0 ? inv2SigmaSqL : inv2SigmaSqR;
            gE[ei] = Math.Exp(-dE * dE * inv2SigmaSq);
        }

        double eStep = eLen > 1 ? Math.Abs(energies[0] - energies[^1]) / (eLen - 1) : 1.0;
        var lambdaPerEnergy = new double[eLen];

        // 260327Cl: List<double> 割り当てを除去し、カウント＋合計を直接計算
        for (int ei = 0; ei < eLen; ei++)
        {
            double eLow = energies[ei] - eStep * 0.5;
            double eHigh = energies[ei] + eStep * 0.5;

            int depthCount = 0;
            double depthSum = 0;
            foreach (var d in data)
                if (d.energy >= eLow && d.energy < eHigh)
                {
                    depthSum += d.depth;
                    depthCount++;
                }

            if (depthCount > 3)
                lambdaPerEnergy[ei] = Math.Max(1.0, depthSum / depthCount);
            else
                lambdaPerEnergy[ei] = -1;
        }

        FitLambdaPolynomial(energies, lambdaPerEnergy, out double la, out double lb, out double lc);

        double totalWeight = 0;
        for (int ei = 0; ei < eLen; ei++)
        {
            double lambda = la + lb * energies[ei] + lc * energies[ei] * energies[ei];
            if (lambda < 1.0) lambda = 1.0;
            double invLambda = 1.0 / lambda; // 260327Cl: 除算を事前計算

            // 260327Cl: useSliceMass 時、隣接スライスで Exp 値を再利用
            if (useSliceMass)
            {
                double expPrev = Math.Exp(0); // di==0 の lowerDepth=0 → exp(0)=1
                for (int di = 0; di < dLen; di++)
                {
                    double expCur = Math.Exp(-depths[di] * invLambda);
                    double w = gE[ei] * (expPrev - expCur); // (260325Ch) model 2 は depth slice 区間質量
                    weights[ei * dLen + di] = w;
                    totalWeight += w;
                    expPrev = expCur; // 260327Cl: 次スライスの lowerDepth 用にキャッシュ
                }
            }
            else
            {
                for (int di = 0; di < dLen; di++)
                {
                    double w = gE[ei] * Math.Exp(-depths[di] * invLambda) * invLambda;
                    weights[ei * dLen + di] = w;
                    totalWeight += w;
                }
            }
        }

        if (totalWeight > 0)
            for (int k = 0; k < weights.Length; k++)
                weights[k] = weights[k] / totalWeight * totalScale; // (260325Ch)
    }

    // 260327Cl: List<double> を固定長配列に置き換えて GC 圧力を軽減
    private static void FitLambdaPolynomial(double[] energies, double[] lambdaValues, out double a, out double b, out double c)
    {
        int validCount = 0;
        for (int i = 0; i < energies.Length; i++)
            if (lambdaValues[i] > 0) validCount++;

        if (validCount == 0)
        {
            a = 10; b = 0; c = 0;
            return;
        }

        var validE = new double[validCount];
        var validL = new double[validCount];
        int idx = 0;
        for (int i = 0; i < energies.Length; i++)
            if (lambdaValues[i] > 0)
            {
                validE[idx] = energies[i];
                validL[idx] = lambdaValues[i];
                idx++;
            }

        if (validCount == 1)
        {
            a = validL[0]; b = 0; c = 0;
            return;
        }
        if (validCount == 2)
        {
            double e0 = validE[0], e1 = validE[1], l0 = validL[0], l1 = validL[1];
            b = (l1 - l0) / (e1 - e0);
            a = l0 - b * e0;
            c = 0;
            return;
        }

        int n = validCount;
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
