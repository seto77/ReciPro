using System;
using System.Collections.Generic;
using System.Threading.Tasks; // 260602Cl: Parallel.For (64 ビンフィットの並列化)
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

        // 260602Cl 変更: 64 ビンは互いに独立 (distinct な BinWeights[bi,bj]/BinAbsoluteSliceWeights[bi,bj] へ書く) なので
        //   Parallel.For 化。bins への集約 (上の foreach) は逐次のまま、ここはフィット段だけ並列化する。
        Parallel.For(0, binCount * binCount, (int idx) =>
        {
            int bi = idx / binCount, bj = idx % binCount;
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
                // FitBinDistribution(binData, beamEnergy, energies, depths, absoluteSliceWeights, useSliceMass: true, totalScale: binFraction); // 260602Cl 変更前: 同一 binData に 2 回呼び meanE/sigma/gE/lambda を二重計算
                FitBinDistribution(binData, beamEnergy, energies, depths, weights, absoluteSliceWeights, binFraction); // 260602Cl: 共通 fit から weights と absoluteSliceWeights を両方埋める
            }

            BinWeights[bi, bj] = weights;
            BinAbsoluteSliceWeights[bi, bj] = absoluteSliceWeights; // (260325Ch)
        });
    }

    // 260602Cl 変更: weights と absoluteSliceWeights を 1 回の共通 fit から両方埋める形へ統合。
    //   旧実装は同一 binData に対して本メソッドを 2 回呼び (useSliceMass=false/true)、
    //   meanE/sigmaL,R/gE[]/lambdaPerEnergy/FitLambdaPolynomial を二重計算していた。
    //   これらは useSliceMass/totalScale に依存しない共通部 (Stage1) なので 1 回だけ計算し、
    //   weights の書き込み (Stage2) のみ 2 種類行う。物理的な値は旧 2 回呼びと厳密一致。
    // 旧シグネチャ: FitBinDistribution(data, beamEnergy, energies, depths, weights, bool useSliceMass=false, double totalScale=1.0)
    private static void FitBinDistribution(
        List<(double depth, double energy)> data,
        double beamEnergy,
        double[] energies, double[] depths,
        double[] weights,             // useSliceMass=false, totalScale=1.0 相当
        double[] absoluteSliceWeights, // useSliceMass=true,  totalScale=binFraction 相当
        double binFraction)
    {
        int eLen = energies.Length; // 260602Cl: dLen は Stage2 (FillBinWeights) に移ったのでここでは不要
        int count = data.Count;
        if (count == 0) return;

        #region Stage1: 共通 fit パラメータ (meanE, sigmaL/R, gE[], lambda 多項式 la/lb/lc)
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
        double halfStep = eStep * 0.5, e0 = energies[0];

        // 260602Cl 変更: 旧実装は ei ごとに binData 全体を energy 窓でフルスキャン (O(eLen × N_bin))。
        //   energies が ComputeGridFromRanges 由来 (beamEnergy から降順・ほぼ等間隔・0.1 丸め) の場合は、
        //   各電子を 1 パスで energy 窓へ割り当てる高速経路 (O(N_bin)) を使う。候補 index cand を算出し、
        //   現行と同じ半開窓 [energies[ei]-eStep/2, energies[ei]+eStep/2) を満たす ei (丸めズレ・窓の
        //   微小な重なりを cand±2 で吸収) すべてに加算 → 旧ループと同一集合・同一加算順で bit 一致。
        //   constructor は energies を外部受け取りなので、降順・ほぼ等間隔 (|δ'|<=eStep/2) でない、または
        //   eStep<=0 のときは安全のため旧 eLen フルスキャンへ fallback する (任意 energies で常に正しい)。
        var depthSumPerEnergy = new double[eLen];
        var depthCountPerEnergy = new int[eLen];

        bool uniformDescending = eStep > 0;
        if (uniformDescending)
            for (int ei = 0; ei < eLen; ei++)
                if (Math.Abs(energies[ei] - (e0 - ei * eStep)) > halfStep) { uniformDescending = false; break; } // cand±2 が全マッチ窓を覆う前提を保証

        if (uniformDescending)
        {
            double invEStep = 1.0 / eStep;
            foreach (var (depth, energy) in data)
            {
                int cand = (int)((e0 - energy) * invEStep); // 降順 energies に対する floor 候補 (energy<=e0)
                for (int ei = cand - 2; ei <= cand + 2; ei++)
                    if ((uint)ei < (uint)eLen && energy >= energies[ei] - halfStep && energy < energies[ei] + halfStep)
                    {
                        depthSumPerEnergy[ei] += depth;
                        depthCountPerEnergy[ei]++;
                    }
            }
        }
        else
        {
            // fallback: 旧 eLen フルスキャン (任意 energies に対し常に正しい)
            for (int ei = 0; ei < eLen; ei++)
            {
                double eLow = energies[ei] - halfStep, eHigh = energies[ei] + halfStep;
                foreach (var (depth, energy) in data)
                    if (energy >= eLow && energy < eHigh)
                    {
                        depthSumPerEnergy[ei] += depth;
                        depthCountPerEnergy[ei]++;
                    }
            }
        }

        var lambdaPerEnergy = new double[eLen];
        for (int ei = 0; ei < eLen; ei++)
            lambdaPerEnergy[ei] = depthCountPerEnergy[ei] > 3 ? Math.Max(1.0, depthSumPerEnergy[ei] / depthCountPerEnergy[ei]) : -1;

        FitLambdaPolynomial(energies, lambdaPerEnergy, out double la, out double lb, out double lc);
        #endregion

        #region Stage2: 共通パラメータから 2 種の weights を書き込み
        FillBinWeights(weights, energies, depths, gE, la, lb, lc, useSliceMass: false, totalScale: 1.0);
        FillBinWeights(absoluteSliceWeights, energies, depths, gE, la, lb, lc, useSliceMass: true, totalScale: binFraction);
        #endregion
    }

    /// <summary>
    /// 260602Cl 追加: 共通 fit パラメータ (gE, lambda 多項式 la/lb/lc) から 1 つの weights 配列を埋める。
    /// useSliceMass=false: 連続深さ重み g(E)·exp(-z/λ)/λ。
    /// useSliceMass=true : depth slice 区間質量 g(E)·(exp(-z_prev/λ) - exp(-z/λ))。
    /// 旧 FitBinDistribution の weights 書き込み部 (Stage2) をそのまま切り出したもの。
    /// </summary>
    private static void FillBinWeights(
        double[] weights, double[] energies, double[] depths, double[] gE,
        double la, double lb, double lc, bool useSliceMass, double totalScale)
    {
        int eLen = energies.Length, dLen = depths.Length;
        double totalWeight = 0;
        for (int ei = 0; ei < eLen; ei++)
        {
            double lambda = la + lb * energies[ei] + lc * energies[ei] * energies[ei];
            if (lambda < 1.0) lambda = 1.0;
            double invLambda = 1.0 / lambda; // 260327Cl: 除算を事前計算

            // 260327Cl: useSliceMass 時、隣接スライスで Exp 値を再利用
            if (useSliceMass)
            {
                double expPrev = 1.0; // di==0 の lowerDepth=0 → exp(0)=1
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

        int idxLoss80 = Math.Min((int)(n * 0.8), n - 1);
        int idxDepth99 = Math.Min((int)(n * 0.99), n - 1); // (260326Ch)

        // 260602Cl 変更: 80%/99% パーセンタイルのためのフルソート 2 回を QuickSelect (nth_element) に置換。
        //   QuickSelect.Execute(span, k, cmp) は span[k] に sorted index k の値を置く (前は <=, 後は >=) ので、
        //   Array.Sort 後の losses[idxLoss80]/depths[idxDepth99] と同一値。O(n log n) → 平均 O(n)。
        // Array.Sort(losses); Array.Sort(depths); // 260602Cl 変更前
        QuickSelect.Execute(losses.AsSpan(), idxLoss80, static (a, b) => a.CompareTo(b));
        QuickSelect.Execute(depths.AsSpan(), idxDepth99, static (a, b) => a.CompareTo(b));

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
