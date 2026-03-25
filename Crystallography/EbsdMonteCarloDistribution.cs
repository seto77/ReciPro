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
    /// <summary>検出器ビン数 (各軸)。</summary>
    public int BinCount { get; }

    /// <summary>マスターパターンで使うエネルギー配列 (keV, 降順)。</summary>
    public double[] Energies { get; }

    /// <summary>マスターパターンで使う深さ配列 (nm, 昇順)。</summary>
    public double[] Depths { get; }

    /// <summary>
    /// ビンごとの重み配列。BinWeights[binI, binJ] は double[Energies.Length * Depths.Length]。
    /// 添字は energyIndex * Depths.Length + depthIndex。
    /// </summary>
    public double[,][] BinWeights { get; }

    /// <summary>
    /// BSE をモンテカルロで得た後、検出器ビンに割り当て、
    /// エネルギー・深さ分布をフィッティングして重みを計算する。
    /// </summary>
    /// <param name="bseList">MC 結果 (depth_nm, direction, energy_keV) の配列</param>
    /// <param name="beamEnergy">入射エネルギー (keV)</param>
    /// <param name="smpTilt">試料傾斜角 (rad)</param>
    /// <param name="detTilt">検出器傾斜角 (rad)</param>
    /// <param name="detY">検出器中心 Y (mm)</param>
    /// <param name="detZ">検出器中心 Z (mm)</param>
    /// <param name="detR">検出器半径 (mm)</param>
    /// <param name="energies">使用するエネルギー配列 (keV)</param>
    /// <param name="depths">使用する深さ配列 (nm)</param>
    /// <param name="binCount">検出器ビン分割数 (既定 8)</param>
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

        // 260325Cl: 検出器ジオメトリの前計算
        // BSE の出射方向 vec はビーム座標系で定義されており、検出器もビーム座標系に配置されている。
        // smpRot は不要 (smpRot はスクリーン表示用に Schmidt 投影するときだけ使う)。
        var (sinDet, cosDet) = Math.SinCos(detTilt);
        double dNumer = -(detY * sinDet + detZ * cosDet); // 検出器面までの法線距離

        // BSE をビンに振り分け
        var bins = new List<(double depth, double energy)>[binCount, binCount];
        for (int i = 0; i < binCount; i++)
            for (int j = 0; j < binCount; j++)
                bins[i, j] = new List<(double, double)>();

        foreach (var (depth, vec, energy) in bseList)
        {
            // 260325Cl: ビーム座標系のまま検出器面と交差計算 (smpRot は適用しない)
            double dDenom = vec.Y * sinDet + vec.Z * cosDet;
            if (Math.Abs(dDenom) < 1e-15) continue;
            double k = dNumer / dDenom;
            if (k <= 0) continue; // 検出器の反対側へ飛んだ

            // 検出器面上の正規化座標 [-1, 1]
            // 検出器 Y 軸 = RotX(-detTilt) * (0,1,0) = (0, cosDet, -sinDet)
            // hitRel = k*vec - detCenter = (k*vX, k*vY + detY, k*vZ + detZ)
            // localY = hitRel · detYaxis = (k*vY + detY)*cosDet - (k*vZ + detZ)*sinDet
            double localY = cosDet * (k * vec.Y + detY) - sinDet * (k * vec.Z + detZ);
            double px = k * vec.X / detR;
            double py = localY / detR;

            if (px < -1 || px > 1 || py < -1 || py > 1) continue;

            int bi = Math.Clamp((int)((px + 1) * 0.5 * binCount), 0, binCount - 1);
            int bj = Math.Clamp((int)((1 - py) * 0.5 * binCount), 0, binCount - 1);
            bins[bi, bj].Add((depth, energy));
        }

        // 各ビンでフィッティングして重みを計算
        BinWeights = new double[binCount, binCount][];
        int eLen = energies.Length, dLen = depths.Length;

        for (int bi = 0; bi < binCount; bi++)
            for (int bj = 0; bj < binCount; bj++)
            {
                var binData = bins[bi, bj];
                var weights = new double[eLen * dLen];

                if (binData.Count < 10)
                {
                    // データ不足の場合は一様重み
                    double uniform = binData.Count > 0 ? 1.0 / (eLen * dLen) : 0;
                    Array.Fill(weights, uniform);
                }
                else
                {
                    FitBinDistribution(binData, beamEnergy, energies, depths, weights);
                }
                BinWeights[bi, bj] = weights;
            }
    }

    /// <summary>
    /// 1 つのビンの BSE データから w(E, z) = g(E) × h(z|E) をフィッティングし、
    /// 離散グリッド上の重みを計算する。260325Cl 追加
    /// </summary>
    private static void FitBinDistribution(
        List<(double depth, double energy)> data,
        double beamEnergy,
        double[] energies, double[] depths,
        double[] weights)
    {
        int eLen = energies.Length, dLen = depths.Length;
        int count = data.Count;
        if (count == 0) return;

        // --- g(E): 非対称ガウシアンのフィッティング ---
        // エネルギーロスのヒストグラムから Ep, σL, σR を推定
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

        // g(E_i) を計算
        var gE = new double[eLen];
        for (int ei = 0; ei < eLen; ei++)
        {
            double dE = energies[ei] - Ep;
            double sigma = dE < 0 ? sigmaL : sigmaR;
            gE[ei] = Math.Exp(-dE * dE / (2 * sigma * sigma));
        }

        // --- h(z|E): エネルギー依存の指数減衰 λ(E) のフィッティング ---
        // 各エネルギービンで深さ分布を作り、指数フィットで λ を求める
        double eStep = eLen > 1 ? Math.Abs(energies[0] - energies[^1]) / (eLen - 1) : 1.0;
        var lambdaPerEnergy = new double[eLen];

        for (int ei = 0; ei < eLen; ei++)
        {
            double eLow = energies[ei] - eStep * 0.5;
            double eHigh = energies[ei] + eStep * 0.5;

            // このエネルギー範囲の深さデータを収集
            var depthsInBin = new List<double>();
            foreach (var d in data)
                if (d.energy >= eLow && d.energy < eHigh)
                    depthsInBin.Add(d.depth);

            if (depthsInBin.Count > 3)
            {
                // λ = 平均深さ (指数分布の最尤推定)
                double sum = 0;
                foreach (var z in depthsInBin) sum += z;
                lambdaPerEnergy[ei] = Math.Max(1.0, sum / depthsInBin.Count);
            }
            else
            {
                lambdaPerEnergy[ei] = -1; // データ不足マーカー
            }
        }

        // λ(E) を E の 2 次多項式でフィット: λ = a + b*E + c*E²
        // データが少ないビンは除外してフィット
        FitLambdaPolynomial(energies, lambdaPerEnergy, out double la, out double lb, out double lc);

        // 重み w(E_i, z_j) = g(E_i) × h(z_j | E_i) を計算し正規化
        double totalWeight = 0;
        for (int ei = 0; ei < eLen; ei++)
        {
            double lambda = la + lb * energies[ei] + lc * energies[ei] * energies[ei];
            if (lambda < 1.0) lambda = 1.0;

            for (int di = 0; di < dLen; di++)
            {
                double w = gE[ei] * Math.Exp(-depths[di] / lambda) / lambda;
                weights[ei * dLen + di] = w;
                totalWeight += w;
            }
        }

        // 正規化
        if (totalWeight > 0)
            for (int k = 0; k < weights.Length; k++)
                weights[k] /= totalWeight;
    }

    /// <summary>
    /// λ(E) を 2 次多項式 a + b*E + c*E² で最小二乗フィットする。260325Cl 追加
    /// lambdaValues[i] が負なら欠損として除外する。
    /// </summary>
    private static void FitLambdaPolynomial(double[] energies, double[] lambdaValues, out double a, out double b, out double c)
    {
        // 有効データだけ収集
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
            a = 10; b = 0; c = 0; // デフォルト λ=10nm
            return;
        }
        if (validE.Count == 1)
        {
            a = validL[0]; b = 0; c = 0;
            return;
        }
        if (validE.Count == 2)
        {
            // 1 次フィット
            double e0 = validE[0], e1 = validE[1], l0 = validL[0], l1 = validL[1];
            b = (l1 - l0) / (e1 - e0);
            a = l0 - b * e0;
            c = 0;
            return;
        }

        // 正規方程式で 2 次多項式フィット
        // Σ w_k (a + b*e_k + c*e_k^2 - λ_k)^2 → min
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

        // [s0 s1 s2] [a]   [r0]
        // [s1 s2 s3] [b] = [r1]
        // [s2 s3 s4] [c]   [r2]
        double det = s0 * (s2 * s4 - s3 * s3) - s1 * (s1 * s4 - s3 * s2) + s2 * (s1 * s3 - s2 * s2);
        if (Math.Abs(det) < 1e-30)
        {
            // 退化した場合は定数
            a = r0 / s0; b = 0; c = 0;
            return;
        }

        double invDet = 1.0 / det;
        a = ((s2 * s4 - s3 * s3) * r0 + (s2 * s3 - s1 * s4) * r1 + (s1 * s3 - s2 * s2) * r2) * invDet;
        b = ((s2 * s3 - s1 * s4) * r0 + (s0 * s4 - s2 * s2) * r1 + (s1 * s2 - s0 * s3) * r2) * invDet;
        c = ((s1 * s3 - s2 * s2) * r0 + (s1 * s2 - s0 * s3) * r1 + (s0 * s2 - s1 * s1) * r2) * invDet;
    }

    /// <summary>
    /// 検出器上の正規化座標 (detX, detY) ∈ [-1, 1] における重みを、
    /// 周囲 4 ビンからバイリニア補間して返す。260325Cl 追加
    /// 戻り値は double[Energies.Length * Depths.Length]。
    /// </summary>
    public double[] GetPixelWeights(double detX, double detY)
    {
        int eLen = Energies.Length, dLen = Depths.Length;
        int wLen = eLen * dLen;
        var result = new double[wLen];

        // ビン中心座標: ビン i の中心は (2*i + 1) / (2*binCount) * 2 - 1 = (2*i+1)/binCount - 1
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

    /// <summary>
    /// MC 結果から全検出器領域での累積分布を使い、
    /// 80% をカバーするエネルギーロスと深さの閾値を返す。260325Cl 追加
    /// </summary>
    /// <param name="bseList">MC 結果 (depth_nm, direction, energy_keV)</param>
    /// <param name="beamEnergy">入射エネルギー (keV)</param>
    /// <param name="cumulativeFraction">累積頻度の閾値 (既定 0.8)</param>
    /// <returns>(energyLoss80: 80% カバーするエネルギーロス keV, depth80: 80% カバーする深さ nm)</returns>
    // 260325Cl: depth は累積95%に変更、energy は累積80%のまま
    public static (double energyLoss80, double depth95) ComputeRangesFromMC(
        (double Depth, V3 Vec, double Energy)[] bseList,
        double beamEnergy)
    {
        if (bseList == null || bseList.Length == 0)
            return (5.0, 50.0); // デフォルト

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
        int idxDepth95 = Math.Min((int)(n * 0.95), n - 1);

        return (losses[idxLoss80], depths[idxDepth95]);
    }

    /// <summary>
    /// エネルギーと深さの配列を生成する。260325Cl 追加・修正
    /// エネルギー: beamEnergy から energyLoss80 分下がるまでを 8 段階 (0.1 keV 刻み)。
    /// 深さ: 累積95%深さ x に対し、ステップ y = x * 0.025、範囲 y～x。
    /// </summary>
    public static (double[] energies, double energyStart, double energyEnd, double energyStep,
                    double[] depths, double depthStart, double depthEnd, double depthStep)
        ComputeGridFromRanges(double beamEnergy, double energyLoss80, double depth95)
    {
        // エネルギー: 8 段階, ステップは 0.1 keV の倍数
        int numEnergyLevels = 8;
        double rawEnergyStep = energyLoss80 / (numEnergyLevels - 1);
        double energyStep = Math.Max(0.1, Math.Round(rawEnergyStep / 0.1) * 0.1);
        double energyStart = beamEnergy;
        double energyEnd = Math.Round((beamEnergy - energyStep * (numEnergyLevels - 1)) / 0.1) * 0.1;
        if (energyEnd < 1) energyEnd = 1;

        // 実際に使うエネルギー配列 (降順)
        var energyList = new List<double>();
        for (double e = energyStart; e >= energyEnd - 0.001; e -= energyStep)
            energyList.Add(Math.Round(e * 10) / 10.0); // 0.1 keV 精度に丸め
        var energies = energyList.ToArray();

        // 260325Cl: 深さ: 累積95%深さ x に対し、ステップ y = x * 0.025、範囲 y～x
        double depthStep = Math.Max(0.1, Math.Round(depth95 * 0.025 * 10) / 10.0); // 0.1 nm 精度に丸め
        double depthStart = depthStep;
        double depthEnd = Math.Max(depthStep, Math.Round(depth95 * 10) / 10.0);
        var depthList = new List<double>();
        for (double d = depthStart; d <= depthEnd + 0.001; d += depthStep)
            depthList.Add(Math.Round(d * 10) / 10.0);
        var depths = depthList.ToArray();

        return (energies, energyStart, energyEnd, energyStep,
                depths, depthStart, depthEnd, depthStep);
    }
}
