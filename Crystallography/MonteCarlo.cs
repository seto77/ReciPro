using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using V3 = OpenTK.Mathematics.Vector3d;
using ZLinq;

namespace Crystallography;

public enum MonteCarloStoppingPowerModel
{
    JoyLuo1989,
    JablonskiPredictive2006,
    JablonskiModified2008,
}

public enum MonteCarloElasticScatteringModel
{
    ScreenedRutherford,
    MottNistSampler2023,
}

//Electron beam-specimen interactions and simulation methods in microscopy 2018
//Eqs (2.38), (2.41), (2.42) などを参考
public class MonteCarlo
{
    private readonly Random Rnd = Random.Shared;
    public readonly double Z, A, ρ;
    public readonly double InitialKev, Tilt;
    public readonly double coeff0, coeff1, coeff2, coeff3;
    public readonly double k, J, tan, cos, sin;
    public readonly double ValenceElectronCount, BandGapEv; // (260331Ch) TPP-2M に使う平均価電子数 Nv / バンドギャップ Eg
    public readonly MonteCarloStoppingPowerModel StoppingPowerModel; // (260331Ch) 阻止能モデルの切替
    public readonly MonteCarloElasticScatteringModel ElasticScatteringModel; // (260331Ch) 弾性散乱モデルの切替
    public readonly double ThresholdKev;
    private readonly ElasticSpecies[] ElasticComponents = []; // (260331Ch) Mott/NIST sampler 用の元素組成と数密度
    private readonly double TotalElasticNumberDensityPerNm3; // (260331Ch) 混合系の有効断面積表示用

    public static MonteCarloStoppingPowerModel DefaultStoppingPowerModel { get; set; } = MonteCarloStoppingPowerModel.JablonskiModified2008; // (260331Ch)
    public static MonteCarloElasticScatteringModel DefaultElasticScatteringModel { get; set; } = MonteCarloElasticScatteringModel.ScreenedRutherford; // (260331Ch) 旧挙動を既定にして比較しやすくする
    public static string NistElasticSamplerDirectory { get; set; } =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ReciPro", "MonteCarlo", "NistElasticSampler"); // (260331Ch)

    private static readonly object NistElasticSamplerSync = new(); // (260331Ch)
    private static readonly Dictionary<int, NistElasticScatteringTable> NistElasticSamplerCache = [];
    private static readonly double LogNistElasticMinEnergyEv = Math.Log(50.0); // (260331Ch)
    private static readonly double LogNistElasticMaxEnergyEv = Math.Log(20000.0); // (260331Ch)
    private static readonly double LogNistElasticEnergyStep = (LogNistElasticMaxEnergyEv - LogNistElasticMinEnergyEv) / 100.0; // (260331Ch)
    private const double NistElasticCrossSectionUnitNm2 = 2.8002852E-3; // (260331Ch) 1 a0^2 = 2.8002852e-21 m^2 = 2.8002852e-3 nm^2

    private readonly record struct ElasticSpecies(int AtomicNumber, double NumberDensityPerNm3); // (260331Ch)

    private sealed class NistElasticScatteringTable // (260331Ch)
    {
        public readonly double[] SigmaA0Squared = new double[101];
        public readonly double[][] Phi = new double[101][];
    }

    // (260331Ch) TPP-2M の平均価電子数 Nv。Jablonski / Tanuma / Powell の IMFP 論文の元素表を優先し、未収録元素は後段の簡易推定へフォールバックする
    private static readonly Dictionary<int, (double ValenceElectrons, double BandGapEv)> ElementInelasticParameters = new()
    {
        [3] = (1.0, 0.0),
        [4] = (2.0, 0.0),
        [6] = (4.0, 0.0),
        [11] = (1.0, 0.0),
        [12] = (2.0, 0.0),
        [13] = (3.0, 0.0),
        [14] = (4.0, 1.1),
        [19] = (1.0, 0.0),
        [21] = (3.0, 0.0),
        [22] = (4.0, 0.0),
        [23] = (5.0, 0.0),
        [24] = (6.0, 0.0),
        [26] = (8.0, 0.0),
        [27] = (9.0, 0.0),
        [28] = (10.0, 0.0),
        [29] = (11.0, 0.0),
        [32] = (4.0, 0.67),
        [39] = (3.0, 0.0),
        [41] = (5.0, 0.0),
        [42] = (6.0, 0.0),
        [44] = (8.0, 0.0),
        [45] = (9.0, 0.0),
        [46] = (10.0, 0.0),
        [47] = (11.0, 0.0),
        [49] = (3.0, 0.0),
        [50] = (4.0, 0.0),
        [55] = (1.0, 0.0),
        [64] = (9.0, 0.0),
        [65] = (9.0, 0.0),
        [66] = (9.0, 0.0),
        [72] = (4.0, 0.0),
        [73] = (5.0, 0.0),
        [74] = (6.0, 0.0),
        [75] = (7.0, 0.0),
        [76] = (8.0, 0.0),
        [77] = (9.0, 0.0),
        [78] = (10.0, 0.0),
        [79] = (11.0, 0.0),
        [83] = (5.0, 0.0),
    };

    private const double Jablonski2006C1 = 11.52; // (260331Ch)
    private const double Jablonski2006C2 = 0.01639; // (260331Ch)
    private const double Jablonski2006C3 = 0.03386; // (260331Ch)
    private const double Jablonski2008D1 = 7.89271; // (260331Ch)
    private const double Jablonski2008D2 = 0.0117088; // (260331Ch)
    private const double Jablonski2008D3 = 0.0545126; // (260331Ch)
    private const double Jablonski2008D4 = -0.0254488; // (260331Ch)
    private const double Jablonski2008D5 = 0.326907; // (260331Ch)

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="z">原子番号 (単位無し)</param>
    /// <param name="a">原子量 (g/mol)</param>
    /// <param name="_ρ">密度 (g/cm^3)</param>
    /// <param name="kev">入射電子エネルギー (kev)</param>
    /// <param name="tilt">試料表面の傾き (rad, X軸で回転)</param>
    /// <param name="thresholdKev">飛程計算を打ち切るエネルギー (kev)</param>
    /// <param name="stoppingPowerModel">阻止能モデル。null のときは DefaultStoppingPowerModel を使う。</param>
    /// <param name="elasticScatteringModel">弾性散乱モデル。null のときは DefaultElasticScatteringModel を使う。</param>
    /// <param name="valenceElectronCount">TPP-2M に使う平均価電子数 Nv。null のときは簡易推定。</param>
    /// <param name="bandGapEv">TPP-2M に使うバンドギャップ Eg (eV)。null のときは 0 eV。</param>
    /// <param name="atoms">Mott/NIST sampler 用の元素組成。null のときは平均 Z 近似のまま扱う。</param>
    public MonteCarlo(
        double z,
        double a,
        double _ρ,
        double kev,
        double tilt,
        double thresholdKev = 2,
        MonteCarloStoppingPowerModel? stoppingPowerModel = null,
        MonteCarloElasticScatteringModel? elasticScatteringModel = null,
        double? valenceElectronCount = null,
        double? bandGapEv = null,
        IEnumerable<Atoms> atoms = null)
    {
        Z = z;
        A = a;
        ρ = _ρ;
        InitialKev = kev;
        Tilt = tilt;
        ThresholdKev = thresholdKev;
        StoppingPowerModel = stoppingPowerModel ?? DefaultStoppingPowerModel; // (260331Ch)
        ElasticScatteringModel = elasticScatteringModel ?? DefaultElasticScatteringModel; // (260331Ch)
        ValenceElectronCount = valenceElectronCount is > 0 ? valenceElectronCount.Value : EstimateValenceElectronCount(z); // (260331Ch)
        BandGapEv = bandGapEv is >= 0 ? bandGapEv.Value : 0.0; // (260331Ch)
        ElasticComponents = atoms is null ? [] : BuildElasticSpecies(atoms, ρ); // (260331Ch) Mott/NIST は元素組成から混合断面積を作る
        for (int i = 0; i < ElasticComponents.Length; i++)
            TotalElasticNumberDensityPerNm3 += ElasticComponents[i].NumberDensityPerNm3;

        //散乱係数の計算中に出てくる定数
        coeff0 = 0.0034 * Math.Pow(Z, 2.0 / 3.0);
        //トータル散乱断面積の計算中に出てくる定数
        coeff1 = Z * UniversalConstants.e0 * UniversalConstants.e0 / (8.0 * Math.PI * UniversalConstants.ε0);
        //平均自由行程のところに出てくる定数
        coeff2 = A / UniversalConstants.A / ρ * 1E21;// / Math.PI;
        //阻止能の計算中に出てくる定数
        coeff3 = -Z * UniversalConstants.A * ρ * 1E3 / (A * 1E-3) * Math.Pow(UniversalConstants.e0, 4)
            / 4 / Math.PI / UniversalConstants.ε0 / UniversalConstants.ε0 / UniversalConstants.eV_joule * 1E-9 * 1E-3;

        //阻止能の計算中に出てくる物質依存の定数 k    Joy and Luo (1989)によれば 6C:0.77, 13Al: 0.815, 14Si: 0.822, 28Ni: 0.83, 29Cu: 0.83,  47Ag:0.852, 79Au: 0.851
        //取りあえず対数近似した値を使う
        k = 0.0299 * Math.Log(Z) + 0.7307;
        //阻止能の計算中に出てくる物質依存の定数 J (eV) Z<=12の時は J=11.5evにするらしい (Joy&Luo 1989)
        J = Z <= 12 ? 11.5 * Z : (9.76 * Z + 58.5 / Math.Pow(Z, 0.19));
        tan = Math.Tan(tilt);
        (sin, cos) = Math.SinCos(tilt);
    }

    public static double EstimateAverageValenceElectronCount(IEnumerable<(int AtomicNumber, double Weight)> components)
    {
        double sum = 0, weightSum = 0;
        foreach (var (atomicNumber, weight) in components)
        {
            if (!(weight > 0))
                continue;
            sum += EstimateElementValenceElectronCount(atomicNumber) * weight; // (260331Ch) 実組成があるときは平均 Z ではなく質量重みで Nv を作る
            weightSum += weight;
        }
        return weightSum > 0 ? sum / weightSum : double.NaN;
    }

    private static ElasticSpecies[] BuildElasticSpecies(IEnumerable<Atoms> atoms, double density)
    {
        if (!(density > 0))
            return [];

        var counts = new Dictionary<int, double>();
        double totalWeightPerFormula = 0.0;
        foreach (var atom in atoms)
        {
            var count = atom.Multiplicity * atom.Occ;
            if (!(count > 0) || atom.AtomicNumber <= 0)
                continue;

            if (!counts.TryAdd(atom.AtomicNumber, count))
                counts[atom.AtomicNumber] += count;
            totalWeightPerFormula += AtomStatic.AtomicWeight(atom.AtomicNumber) * count;
        }

        if (!(totalWeightPerFormula > 0) || counts.Count == 0)
            return [];

        var species = new ElasticSpecies[counts.Count];
        int index = 0;
        foreach (var (atomicNumber, count) in counts)
        {
            var numberDensityPerNm3 = density * UniversalConstants.A * count / totalWeightPerFormula * 1E-21; // (260331Ch) ρ[g/cm3] から元素ごとの数密度 [1/nm3] を作る
            species[index++] = new ElasticSpecies(atomicNumber, numberDensityPerNm3);
        }
        return species;
    }

    public (double ScreeningParameter, double CrossSection, double MeanFreePath, double StoppingPower) GetParameters(double kev)
    {
        //電子の質量 (kg) × 電子の速度の2乗 (m^2/s^2)
        var mv2 = UniversalConstants.Convert.EnergyToElectronMass(kev) * UniversalConstants.Convert.EnergyToElectronVelositySquared(kev);
        //散乱係数 / トータル散乱断面積 / 平均自由行程
        double α, σ_E, λ_el;
        if (ElasticScatteringModel == MonteCarloElasticScatteringModel.MottNistSampler2023 &&
            TryGetMottElasticTransport(kev, out σ_E, out λ_el))
        {
            α = double.NaN; // (260331Ch) Mott sampler では screening parameter を使わない
        }
        else
        {
            α = coeff0 / kev;
            var tmp = 2 * coeff1 / mv2;
            σ_E = tmp * tmp * Math.PI / α / (α + 1) * 1E18;
            //σ_E = 5.21E-21 * Z * Z / kev / kev * 12.56 / α / (α + 1) * Math.Pow((kev + 511) / (kev + 1022), 2);
            λ_el = coeff2 / σ_E; //λ_el = A / UniversalConstants.A / ρ / σ_E * 1E7;
        }
        //阻止能 (Joy and Luo 1989) (kev/nm単位)
        // var sp = coeff3 / mv2 * Math.Log(1.166 * k + 0.583 / UniversalConstants.eV_joule / J * mv2); // (260331Ch) 旧 Joy-Luo 直書き
        var sp = GetStoppingPower(kev, mv2); // (260331Ch)
        return (α, σ_E, λ_el, sp);
    }

    private bool TryGetMottElasticTransport(double kev, out double crossSectionNm2, out double meanFreePathNm)
    {
        crossSectionNm2 = meanFreePathNm = double.NaN;
        if (ElasticComponents.Length == 0 || !(TotalElasticNumberDensityPerNm3 > 0))
            return false;

        double[] macroscopicCrossSectionsArray = ElasticComponents.Length <= 16 ? null : new double[ElasticComponents.Length];
        Span<double> macroscopicCrossSections = macroscopicCrossSectionsArray is null ? stackalloc double[16] : macroscopicCrossSectionsArray;
        if (!TryBuildMottElasticMixture(kev, macroscopicCrossSections, out var totalMacroscopicCrossSection, out _))
            return false;

        crossSectionNm2 = totalMacroscopicCrossSection / TotalElasticNumberDensityPerNm3;
        meanFreePathNm = 1.0 / totalMacroscopicCrossSection;
        return true;
    }

    private bool TryBuildMottElasticMixture(double kev, Span<double> macroscopicCrossSections, out double totalMacroscopicCrossSection, out int nearestEnergyIndex)
    {
        totalMacroscopicCrossSection = 0.0;
        nearestEnergyIndex = -1;
        var energyEv = kev * 1000.0;
        if (ElasticComponents.Length == 0 || energyEv < 50.0 || energyEv > 20000.0)
            return false; // (260331Ch) NIST sampler の有効範囲外は旧 Rutherford に戻す

        nearestEnergyIndex = GetNearestNistElasticEnergyIndex(energyEv);
        for (int i = 0; i < ElasticComponents.Length; i++)
        {
            var table = GetNistElasticScatteringTable(ElasticComponents[i].AtomicNumber);
            if (table is null)
                return false;

            var sigmaNm2 = InterpolateNistElasticSigma(table.SigmaA0Squared, energyEv) * NistElasticCrossSectionUnitNm2;
            var macroscopicCrossSection = ElasticComponents[i].NumberDensityPerNm3 * sigmaNm2;
            macroscopicCrossSections[i] = macroscopicCrossSection;
            totalMacroscopicCrossSection += macroscopicCrossSection;
        }
        return totalMacroscopicCrossSection > 0;
    }

    private double SampleElasticScatteringCosTheta(double kev, double α)
    {
        if (ElasticScatteringModel == MonteCarloElasticScatteringModel.MottNistSampler2023 &&
            TrySampleMottElasticCosTheta(kev, out var cosTheta))
            return cosTheta;

        var rnd = Rnd.NextDouble();
        return 1 - 2 * α * rnd / (1 + α - rnd);
    }

    private bool TrySampleMottElasticCosTheta(double kev, out double cosTheta)
    {
        cosTheta = double.NaN;
        if (ElasticComponents.Length == 0)
            return false;

        double[] macroscopicCrossSectionsArray = ElasticComponents.Length <= 16 ? null : new double[ElasticComponents.Length];
        Span<double> macroscopicCrossSections = macroscopicCrossSectionsArray is null ? stackalloc double[16] : macroscopicCrossSectionsArray;
        if (!TryBuildMottElasticMixture(kev, macroscopicCrossSections, out var totalMacroscopicCrossSection, out var energyIndex))
            return false;

        var choice = Rnd.NextDouble() * totalMacroscopicCrossSection;
        int speciesIndex = 0;
        double partial = 0.0;
        for (; speciesIndex < ElasticComponents.Length; speciesIndex++)
        {
            partial += macroscopicCrossSections[speciesIndex];
            if (choice <= partial)
                break;
        }
        if (speciesIndex >= ElasticComponents.Length)
            speciesIndex = ElasticComponents.Length - 1;

        var table = GetNistElasticScatteringTable(ElasticComponents[speciesIndex].AtomicNumber);
        if (table is null)
            return false;

        var phi = table.Phi[energyIndex];
        var target = Rnd.NextDouble();
        int upper = Array.BinarySearch(phi, target);
        if (upper < 0)
            upper = ~upper;
        upper = Math.Clamp(upper, 1, phi.Length - 1);
        int lower = upper - 1;
        var phi0 = phi[lower];
        var phi1 = phi[upper];
        var x0 = 1.0 - lower * 0.001;
        var x1 = 1.0 - upper * 0.001;
        cosTheta = Math.Clamp(phi1 > phi0 ? x0 + (target - phi0) * (x1 - x0) / (phi1 - phi0) : x0, -1.0, 1.0);
        return true;
    }

    private static int GetNearestNistElasticEnergyIndex(double energyEv)
    {
        var logEnergy = Math.Log(energyEv);
        int lowerIndex = (int)Math.Floor((logEnergy - LogNistElasticMinEnergyEv) / LogNistElasticEnergyStep);
        lowerIndex = Math.Clamp(lowerIndex, 0, 99);
        var lowerEnergy = LogNistElasticMinEnergyEv + lowerIndex * LogNistElasticEnergyStep;
        var upperEnergy = lowerEnergy + LogNistElasticEnergyStep;
        return logEnergy - lowerEnergy < upperEnergy - logEnergy ? lowerIndex : lowerIndex + 1;
    }

    private static double InterpolateNistElasticSigma(double[] sigmaA0Squared, double energyEv)
    {
        var logEnergy = Math.Log(energyEv);
        int lowerIndex = (int)Math.Floor((logEnergy - LogNistElasticMinEnergyEv) / LogNistElasticEnergyStep);
        lowerIndex = Math.Clamp(lowerIndex, 0, 99);
        var lowerEnergy = LogNistElasticMinEnergyEv + lowerIndex * LogNistElasticEnergyStep;
        var upperEnergy = lowerEnergy + LogNistElasticEnergyStep;
        var y0 = sigmaA0Squared[lowerIndex];
        var y1 = sigmaA0Squared[lowerIndex + 1];
        return y0 + (logEnergy - lowerEnergy) * (y1 - y0) / (upperEnergy - lowerEnergy);
    }

    private static NistElasticScatteringTable GetNistElasticScatteringTable(int atomicNumber)
    {
        lock (NistElasticSamplerSync)
        {
            if (NistElasticSamplerCache.TryGetValue(atomicNumber, out var cached))
                return cached;
        }

        var path = Path.Combine(NistElasticSamplerDirectory, $"E_{atomicNumber:00}.TXT");
        NistElasticScatteringTable table = null;
        if (File.Exists(path))
        {
            try
            {
                using var reader = new StreamReader(path);
                table = new NistElasticScatteringTable();
                for (int i = 0; i < 101; i++)
                {
                    _ = reader.ReadLine(); // (260331Ch) NIST sampler 内のブロック番号。Fortran 版でも未使用
                    table.SigmaA0Squared[i] = double.Parse(reader.ReadLine() ?? throw new InvalidDataException(path), CultureInfo.InvariantCulture);
                    var phi = new double[2001];
                    for (int j = 0; j < phi.Length; j++)
                        phi[j] = double.Parse(reader.ReadLine() ?? throw new InvalidDataException(path), CultureInfo.InvariantCulture);
                    table.Phi[i] = phi;
                }
            }
            catch
            {
                table = null;
            }
        }

        lock (NistElasticSamplerSync)
            NistElasticSamplerCache[atomicNumber] = table;
        return table;
    }

    private double GetStoppingPower(double kev, double mv2)
    {
        if (StoppingPowerModel == MonteCarloStoppingPowerModel.JoyLuo1989)
            return coeff3 / mv2 * Math.Log(1.166 * k + 0.583 / UniversalConstants.eV_joule / J * mv2);

        var energyEv = kev * 1000.0;
        var λ_in = GetInelasticMeanFreePathAngstrom(energyEv);
        if (!(λ_in > 0) || double.IsNaN(λ_in) || double.IsInfinity(λ_in))
            return coeff3 / mv2 * Math.Log(1.166 * k + 0.583 / UniversalConstants.eV_joule / J * mv2); // (260331Ch) TPP-2M が破綻したら Joy-Luo に戻す

        var spEvPerAngstrom = StoppingPowerModel switch
        {
            MonteCarloStoppingPowerModel.JablonskiPredictive2006
                => Jablonski2006C1 * (Jablonski2006C2 * Z + 1.0) * Math.Log(Jablonski2006C3 * energyEv) / λ_in,
            MonteCarloStoppingPowerModel.JablonskiModified2008
                => Jablonski2008D1 * Math.Pow(energyEv, Jablonski2008D2) * Math.Log(Jablonski2008D3 * energyEv) * Math.Pow(ρ + Jablonski2008D4, Jablonski2008D5) / λ_in,
            _ => throw new ArgumentOutOfRangeException(),
        };

        return -spEvPerAngstrom * 0.01; // (260331Ch) 1 eV/Å = 0.01 keV/nm
    }

    private double GetInelasticMeanFreePathAngstrom(double energyEv)
    {
        var nv = Math.Max(ValenceElectronCount, 0.1);
        var eg = Math.Max(BandGapEv, 0.0);
        var ep = 28.8 * Math.Sqrt(nv * ρ / A); // (260331Ch) TPP-2M のプラズマエネルギー
        var beta = -0.10 + 0.944 / Math.Sqrt(ep * ep + eg * eg) + 0.069 * Math.Pow(ρ, 0.1);
        var gamma = 0.191 * Math.Pow(ρ, -0.5);
        var u = nv * ρ / A;
        var c = 1.97 - 0.91 * u;
        var d = 53.4 - 20.8 * u;
        var gammaE = gamma * energyEv;
        if (!(gammaE > 1.0))
            return double.NaN;

        var denominator = ep * ep * (beta * Math.Log(gammaE) - c / energyEv + d / (energyEv * energyEv));
        return denominator > 0 ? energyEv / denominator : double.NaN;
    }

    private static double EstimateValenceElectronCount(double atomicNumber)
    {
        if (ElementInelasticParameters.TryGetValue((int)Math.Round(atomicNumber), out var exact))
            return exact.ValenceElectrons;

        int lower = (int)Math.Floor(atomicNumber), upper = (int)Math.Ceiling(atomicNumber);
        if (lower == upper)
            return EstimateElementValenceElectronCount(lower);

        var lowerValue = EstimateElementValenceElectronCount(lower);
        var upperValue = EstimateElementValenceElectronCount(upper);
        return lowerValue + (atomicNumber - lower) * (upperValue - lowerValue);
    }

    private static double EstimateElementValenceElectronCount(int atomicNumber)
    {
        if (ElementInelasticParameters.TryGetValue(atomicNumber, out var exact))
            return exact.ValenceElectrons;

        return atomicNumber switch
        {
            <= 0 => 1.0,
            1 => 1.0,
            2 => 2.0,
            <= 10 => atomicNumber - 2.0,
            <= 18 => atomicNumber - 10.0,
            <= 36 => EstimatePeriodicValenceElectrons(atomicNumber, 18),
            <= 54 => EstimatePeriodicValenceElectrons(atomicNumber, 36),
            <= 86 => EstimatePeriodicValenceElectrons(atomicNumber, 54),
            _ => EstimatePeriodicValenceElectrons(atomicNumber, 86),
        };
    }

    private static double EstimatePeriodicValenceElectrons(int atomicNumber, int previousNobleGas)
    {
        int groupLike = atomicNumber - previousNobleGas;
        return groupLike switch
        {
            <= 2 => groupLike,
            <= 12 => groupLike,
            _ => Math.Min(groupLike - 10, 8),
        };
    }

    /// <summary>
    /// 電子線の飛程を計算する. 電子は -Z軸に沿って入射し、試料と(0,0,0)の座標で衝突したあと、thresholdで指定したエネルギーまで減衰するか、
    /// 試料表面を脱出するまでの飛程を計算する。返り値は、座標 p (nm単位)と エネルギー e (kev単位) のタプル配列
    /// </summary>
    /// <returns>返り値は、座標 p (nm単位)と エネルギー e (kev単位) のタプル配列</returns>
    public List<(V3 p, double e)> GetTrajectories()
    {
        var trajectory = new List<(V3 p, double e)>(100) { (new V3(0, 0, 0), InitialKev) };
        //var vec = new V3(0, 0, -1);
        double m11, m12, m13, m22, m23;
        double vX = 0, vY = 0, vZ = -1;
        int n = 0;

        //電子エネルギーがThresholdKev以下になるか、試料を脱出するまでループ
        while (trajectory[^1].e > ThresholdKev && trajectory[^1].p.Y * tan >= trajectory[^1].p.Z)
        {
            //パラメーター取得
            var (α, _, λ_el, sp) = GetParameters(trajectory[^1].e);
            //飛行距離 s
            var s = -λ_el * Math.Log(Rnd.NextDouble());
            if (n++ != 0)
            {
                double rnd3 = Rnd.NextDouble();
                double cosθ = SampleElasticScatteringCosTheta(trajectory[^1].e, α), sinθ = Math.Sqrt(1 - cosθ * cosθ); // (260331Ch) Mott/NIST sampler が使えれば cosθ を差し替える
                double φ = 2 * Math.PI * rnd3;
                var (sinφ, cosφ) = Math.SinCos(φ);
                double sinθcosφ = sinθ * cosφ, sinθsinφ = sinθ * sinφ;

                var vZ1 = vZ + 1;
                if (vZ1 < Th)
                { vX = sinθcosφ; vY = sinθsinφ; vZ = -cosθ; }
                else
                {
                    m11 = 1 - vX * vX / vZ1;
                    m22 = 1 - vY * vY / vZ1;
                    m12 = -vX * vY / vZ1;
                    m13 = vX;
                    m23 = vY;

                    vX = m11 * sinθcosφ + m12 * sinθsinφ + m13 * cosθ;
                    vY = m12 * sinθcosφ + m22 * sinθsinφ + m23 * cosθ;
                    vZ = -m13 * sinθcosφ - m23 * sinθsinφ + vZ * cosθ;

                    if (n % 10 == 0)
                    {
                        var len = Math.Sqrt(vX * vX + vY * vY + vZ * vZ);
                        vX /= len; vY /= len; vZ /= len;
                    }
                }
            }
            trajectory.Add((trajectory[^1].p + s * new V3(vX, vY, vZ), trajectory[^1].e + s * sp));
        }
        return trajectory;
    }

    /// <summary>
    /// 電子線の飛程を計算する. 電子は -Z軸に沿って入射し、試料と(0,0,0)の座標で衝突したあと、thresholdで指定したエネルギーまで減衰するか、
    /// 試料表面を脱出するまでの飛程を計算する。返り値は、座標 p (nm単位), 出射方向 v (単位ベクトル), エネルギー e (kev単位) のタプル
    /// </summary>
    /// <returns>返り値は、深さ (nm単位), 出射方向 (単位ベクトル), エネルギー e (kev単位) のタプル</returns>
    public (double d, V3 v, double e) GetBackscatteredElectrons()
    {
        double e = InitialKev;
        double vX = 0, vY = 0, vZ = -1;
        double d = 0;// 260321Ch: 表面からの深さだけを直接追跡する
        int n = 0;
        //電子エネルギーがThresholdKev以下になるか、試料を脱出するまでループ
        while (e > ThresholdKev)
        {
            //乱数発生
            double rnd1 = Rnd.NextDouble(), rnd3 = Rnd.NextDouble();
            //パラメーター取得
            var (α, _, λ_el, sp) = GetParameters(e);
            //飛行距離 s
            var s = -λ_el * Math.Log(rnd1);
            if (n++ != 0)
            {
                double cosθ = SampleElasticScatteringCosTheta(e, α), sinθ = Math.Sqrt(1 - cosθ * cosθ); // (260331Ch)
                double φ = 2 * Math.PI * rnd3;
                var (sinφ, cosφ) = Math.SinCos(φ);
                double sinθcosφ = sinθ * cosφ, sinθsinφ = sinθ * sinφ;
                var vZ1 = vZ + 1;
                if (vZ1 < Th)
                { vX = sinθcosφ; vY = sinθsinφ; vZ = -cosθ; }
                else
                {
                    double m11 = 1 - vX * vX / vZ1, m22 = 1 - vY * vY / vZ1, m12 = -vX * vY / vZ1, m13 = vX, m23 = vY;
                    vX = m11 * sinθcosφ + m12 * sinθsinφ + m13 * cosθ;
                    vY = m12 * sinθcosφ + m22 * sinθsinφ + m23 * cosθ;
                    vZ = -m13 * sinθcosφ - m23 * sinθsinφ + vZ * cosθ;

                    if (n % 10 == 0)
                    {
                        var len = Math.Sqrt(vX * vX + vY * vY + vZ * vZ);
                        vX /= len; vY /= len; vZ /= len;
                    }
                }
            }
            var dtmp = d + s * (sin * vY - cos * vZ);// 260321Ch
            if (dtmp < 0)
                break;
            d = dtmp;

            e += s * sp;
        }
        //return (sin * pY - cos * pZ, new V3(vX, vY, vZ), e);
        return (d, new V3(vX, vY, vZ), e);
    }
    public const double Th = 0.0000001;

}
