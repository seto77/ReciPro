using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using V3 = OpenTK.Mathematics.Vector3d;
using ZLinq;

namespace Crystallography;

public enum MonteCarloStoppingPowerModel
{
    /// <summary>
    /// Joy &amp; Luo (1989) の経験的阻止能モデル。
    /// Bethe 式を低エネルギー側に拡張したもので、物質定数 k と平均イオン化ポテンシャル J を用いる。
    /// 計算が軽く実装も単純だが、低エネルギー領域 (&lt;5 keV) での精度はやや劣る。
    /// </summary>
    JoyLuo1989, // 260331Cl コメント追加

    /// <summary>
    /// Jablonski (2008) の修正阻止能モデル。
    /// TPP-2M で求めた非弾性平均自由行程 (IMFP) を基に阻止能を導出する。
    /// 密度 ρ やプラズマエネルギーなどの材料パラメータを反映するため、
    /// 特に低エネルギー領域や EBSD で扱う BSE エネルギー範囲での精度が JoyLuo1989 より高い。
    /// </summary>
    JablonskiModified2008, // 260331Cl コメント追加
}

public enum MonteCarloElasticScatteringModel
{
    /// <summary>
    /// 遮蔽 Rutherford 散乱モデル。
    /// 散乱角分布を解析的に計算でき非常に高速。軽元素〜中程度の原子番号では十分な精度がある。
    /// 重元素 (Au, W など) では大角散乱の確率を過小評価する傾向があり、
    /// 定量的な BSE 深さ分布の精度が必要な場合は MottNistSampler2023 が望ましい。
    /// </summary>
    ScreenedRutherford, // 260331Cl コメント追加

    /// <summary>
    /// NIST Electron Elastic-Scattering Cross-Section Database (SRD 64) に基づく Mott 散乱サンプラー。
    /// 部分波展開による正確な微分断面積テーブルから散乱角を逆関数法でサンプリングする。
    /// 重元素で Screened Rutherford との差が大きく、BSE 収率や深さ分布の精度が向上する。
    /// データファイル (E_ZZ.TXT) の読み込みが必要で、初回にやや時間がかかる。
    /// 混合物系では元素ごとの巨視的断面積で重み付けして散乱元素を選択する。
    /// </summary>
    MottNistSampler2023, // 260331Cl コメント追加
}

public enum MonteCarloInelasticScatteringModel
{
    /// <summary>
    /// 連続減速近似 (CSDA)。非弾性散乱を離散イベントとして扱わず、
    /// ステップごとに阻止能 × 飛行距離だけエネルギーを連続的に減少させる。
    /// 最も高速だが「最後の非弾性散乱」を定義できないため、
    /// EBSD の非弾性散乱深さ・エネルギー分布の解析には使用不可 (HasLastInelasticEvent = false)。
    /// </summary>
    ContinuousSlowingDownApproximation, // 260331Cl コメント追加

    /// <summary>
    /// 離散非弾性散乱モデル (平均損失)。非弾性散乱イベントごとに平均エネルギー損失を一定値として失う。
    /// イベント発生位置 (深さ) の統計は正しく再現されるが、エネルギー損失の確率的ばらつきがない。
    /// 計算速度は離散モデルの中で最速。深さ分布の概形を素早く確認するのに適する。
    /// </summary>
    DiscreteMeanLoss, // 260331Cl コメント追加

    /// <summary>
    /// 離散非弾性散乱モデル (指数分布損失)。平均損失を期待値とする指数分布からエネルギー損失をサンプリング。
    /// 平均値は保たれるが、実際の損失スペクトル (プラズモンピークが支配的) とは形が大きく異なり、
    /// 大きなエネルギー損失を過大評価する傾向がある。EBSD 用途では非推奨。
    /// </summary>
    DiscreteExponentialLoss, // 260331Cl コメント追加

    /// <summary>
    /// 離散非弾性散乱モデル (簡易バルク DIIMFP 近似)。
    /// プラズモンピーク・低損失テール・高損失テールの 3 成分を混合した損失分布からサンプリングする。
    /// エネルギー損失の確率的ばらつきを物理的に最も妥当な形で再現するため、
    /// 「最後の非弾性散乱後のエネルギー」の分布解析に最適。計算コストは DiscreteMeanLoss よりやや大きい。
    /// </summary>
    DiscreteBulkDiimfpApproximation, // 260331Cl コメント追加
}

//Electron beam-specimen interactions and simulation methods in microscopy 2018
//Eqs (2.38), (2.41), (2.42) などを参考
public class MonteCarlo
{
    private const int NistElasticBinaryMagic = 0x4253454E; // (260331Ch) 'NESB' little endian
    private const int NistElasticBinaryVersion = 1; // (260331Ch) sampler binary format version
    private const int NistElasticEnergyCount = 101; // (260331Ch) NIST sampler energies: 50 eV - 20 keV, log spaced
    private const int NistElasticPhiCount = 2001; // (260331Ch) X = cos(theta) from +1 to -1 with step 0.001
    private readonly Random Rnd = Random.Shared;
    public readonly double Z, A, ρ;
    public readonly double InitialKev, Tilt;
    public readonly double coeff0, coeff1, coeff2, coeff3;
    public readonly double k, J, tan, cos, sin;
    public readonly double ValenceElectronCount, BandGapEv; // (260331Ch) TPP-2M に使う平均価電子数 Nv / バンドギャップ Eg
    public readonly MonteCarloStoppingPowerModel StoppingPowerModel; // (260331Ch) 阻止能モデルの切替
    public readonly MonteCarloElasticScatteringModel ElasticScatteringModel; // (260331Ch) 弾性散乱モデルの切替
    public readonly MonteCarloInelasticScatteringModel InelasticScatteringModel; // (260331Ch) 非弾性散乱モデルの切替
    public readonly double ThresholdKev;
    private readonly ElasticSpecies[] ElasticComponents = []; // (260331Ch) Mott/NIST sampler 用の元素組成と数密度
    private readonly double TotalElasticNumberDensityPerNm3; // (260331Ch) 混合系の有効断面積表示用
    private readonly double TppPlasmaEnergyEv, TppBeta, TppGamma, TppC, TppD; // (260331Ch) TPP-2M の材料定数は毎回再計算せず使い回す
    private readonly TransportParameters[] TransportParameterCache = []; // (260331Ch) 1 eV 刻みの輸送パラメータ cache
    private readonly MottElasticMixtureEntry[] MottElasticMixtureCache = []; // (260331Ch) NIST sampler の混合断面積/CDF cache
    private readonly BulkLossSamplerEntry[] BulkLossSamplerCache = []; // (260331Ch) 軽量な bulk DIIMFP 近似 sampler cache

    public static MonteCarloStoppingPowerModel DefaultStoppingPowerModel { get; set; } = MonteCarloStoppingPowerModel.JablonskiModified2008; // (260331Ch)
    public static MonteCarloElasticScatteringModel DefaultElasticScatteringModel { get; set; } = MonteCarloElasticScatteringModel.ScreenedRutherford; // (260331Ch) 旧挙動を既定にして比較しやすくする
    public static MonteCarloInelasticScatteringModel DefaultInelasticScatteringModel { get; set; } = MonteCarloInelasticScatteringModel.DiscreteMeanLoss; // (260331Ch) まずは離散イベント化の影響を比較しやすい既定値にする
    // public static string NistElasticSamplerDirectory { get; set; } =
    //     Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ReciPro", "MonteCarlo", "NistElasticSampler"); // (260331Ch) 旧既定値
    public static string NistElasticSamplerDirectory { get; set; } =
        Path.Combine(AppContext.BaseDirectory, "NistElasticSampler"); // (260331Ch) 配布物に同梱した sampler を既定で使う

    private static readonly object NistElasticSamplerSync = new(); // (260331Ch)
    private static readonly Dictionary<int, NistElasticScatteringTable> NistElasticSamplerCache = [];
    private static readonly double LogNistElasticMinEnergyEv = Math.Log(50.0); // (260331Ch)
    private static readonly double LogNistElasticMaxEnergyEv = Math.Log(20000.0); // (260331Ch)
    private static readonly double LogNistElasticEnergyStep = (LogNistElasticMaxEnergyEv - LogNistElasticMinEnergyEv) / 100.0; // (260331Ch)
    private const double NistElasticCrossSectionUnitNm2 = 2.8002852E-3; // (260331Ch) 1 a0^2 = 2.8002852e-21 m^2 = 2.8002852e-3 nm^2

    private readonly record struct ElasticSpecies(int AtomicNumber, double NumberDensityPerNm3, NistElasticScatteringTable NistElasticTable); // (260331Ch) Mott/NIST sampler の毎イベント lock/dictionary lookup を避ける

    private sealed class NistElasticScatteringTable // (260331Ch)
    {
        public readonly double[] SigmaA0Squared = new double[NistElasticEnergyCount];
        public readonly double[][] Phi = new double[NistElasticEnergyCount][];
    }

    private sealed class MottElasticMixtureEntry // (260331Ch)
    {
        public readonly double TotalMacroscopicCrossSectionPerNm;
        public readonly double[] CumulativeProbabilities;

        public MottElasticMixtureEntry(double totalMacroscopicCrossSectionPerNm, double[] cumulativeProbabilities)
        {
            TotalMacroscopicCrossSectionPerNm = totalMacroscopicCrossSectionPerNm;
            CumulativeProbabilities = cumulativeProbabilities;
        }
    }

    private sealed class BulkLossSamplerEntry // (260331Ch)
    {
        public readonly double MinLossKev;
        public readonly double LossStepKev;
        public readonly double[] CumulativeProbabilities;

        public BulkLossSamplerEntry(double minLossKev, double lossStepKev, double[] cumulativeProbabilities)
        {
            MinLossKev = minLossKev;
            LossStepKev = lossStepKev;
            CumulativeProbabilities = cumulativeProbabilities;
        }
    }

    public readonly record struct BackscatteredElectronDetail( // (260331Ch) EBSD 寄与電子の最後の非弾性散乱情報を後段で解析できるようにする
        double Depth,
        V3 Direction,
        double Energy,
        double TotalEnergyLoss,
        bool HasLastInelasticEvent,
        double LastInelasticDepth,
        double LastInelasticEnergyBeforeLoss,
        double LastInelasticEnergyAfterLoss,
        V3 LastInelasticDirection);

    private readonly record struct TransportParameters( // (260331Ch) 1 ステップで使う輸送パラメータをまとめて扱う
        double ScreeningParameter,
        double ElasticCrossSectionNm2,
        double ElasticMeanFreePathNm,
        double StoppingPowerKevPerNm,
        double InelasticMeanFreePathNm,
        double MeanInelasticLossKev);

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
    /// <param name="inelasticScatteringModel">非弾性散乱モデル。null のときは DefaultInelasticScatteringModel を使う。</param>
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
        MonteCarloInelasticScatteringModel? inelasticScatteringModel = null,
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
        InelasticScatteringModel = inelasticScatteringModel ?? DefaultInelasticScatteringModel; // (260331Ch)
        ValenceElectronCount = valenceElectronCount is > 0 ? valenceElectronCount.Value : EstimateValenceElectronCount(z); // (260331Ch)
        BandGapEv = bandGapEv is >= 0 ? bandGapEv.Value : 0.0; // (260331Ch)
        ElasticComponents = atoms is null ? [] : BuildElasticSpecies(atoms, ρ); // (260331Ch) Mott/NIST は元素組成から混合断面積を作る
        for (int i = 0; i < ElasticComponents.Length; i++)
            TotalElasticNumberDensityPerNm3 += ElasticComponents[i].NumberDensityPerNm3;

        var nv = Math.Max(ValenceElectronCount, 0.1);
        var eg = Math.Max(BandGapEv, 0.0);
        var u = nv * ρ / A;
        TppPlasmaEnergyEv = 28.8 * Math.Sqrt(u); // (260331Ch)
        TppBeta = -0.10 + 0.944 / Math.Sqrt(TppPlasmaEnergyEv * TppPlasmaEnergyEv + eg * eg) + 0.069 * Math.Pow(ρ, 0.1); // (260331Ch)
        TppGamma = 0.191 * Math.Pow(ρ, -0.5); // (260331Ch)
        TppC = 1.97 - 0.91 * u; // (260331Ch)
        TppD = 53.4 - 20.8 * u; // (260331Ch)

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
        MottElasticMixtureCache = BuildMottElasticMixtureCache(); // (260331Ch)
        TransportParameterCache = BuildTransportParameterCache(); // (260331Ch)
        BulkLossSamplerCache = BuildBulkLossSamplerCache(); // (260331Ch) 簡易 bulk DIIMFP sampler
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
            // species[index++] = new ElasticSpecies(atomicNumber, numberDensityPerNm3); // (260331Ch) 旧実装: NIST table を毎回辞書から取り直していた
            species[index++] = new ElasticSpecies(atomicNumber, numberDensityPerNm3, GetNistElasticScatteringTable(atomicNumber));
        }
        return species;
    }

    private MottElasticMixtureEntry[] BuildMottElasticMixtureCache()
    {
        if (ElasticScatteringModel != MonteCarloElasticScatteringModel.MottNistSampler2023 ||
            ElasticComponents.Length == 0 || !(TotalElasticNumberDensityPerNm3 > 0))
            return [];

        var entries = new MottElasticMixtureEntry[101];
        var macroscopicCrossSections = new double[ElasticComponents.Length];
        for (int energyIndex = 0; energyIndex < entries.Length; energyIndex++)
        {
            double totalMacroscopicCrossSection = 0.0;
            for (int i = 0; i < ElasticComponents.Length; i++)
            {
                // var table = GetNistElasticScatteringTable(ElasticComponents[i].AtomicNumber); // (260331Ch) 旧実装
                var table = ElasticComponents[i].NistElasticTable;
                if (table is null)
                    return [];

                var sigmaNm2 = table.SigmaA0Squared[energyIndex] * NistElasticCrossSectionUnitNm2;
                var macroscopicCrossSection = ElasticComponents[i].NumberDensityPerNm3 * sigmaNm2;
                macroscopicCrossSections[i] = macroscopicCrossSection;
                totalMacroscopicCrossSection += macroscopicCrossSection;
            }

            if (!(totalMacroscopicCrossSection > 0))
                return [];

            var cumulativeProbabilities = new double[ElasticComponents.Length];
            double partial = 0.0;
            for (int i = 0; i < ElasticComponents.Length; i++)
            {
                partial += macroscopicCrossSections[i];
                cumulativeProbabilities[i] = partial / totalMacroscopicCrossSection;
            }
            cumulativeProbabilities[^1] = 1.0; // (260331Ch) 丸め誤差で 1 未満になるのを防ぐ
            entries[energyIndex] = new MottElasticMixtureEntry(totalMacroscopicCrossSection, cumulativeProbabilities);
        }
        return entries;
    }

    private TransportParameters[] BuildTransportParameterCache()
    {
        int maxEnergyEv = Math.Max(1, (int)Math.Ceiling(InitialKev * 1000.0));
        var cache = new TransportParameters[maxEnergyEv + 1];
        cache[0] = ComputeTransportParameters(0.001); // (260331Ch) 0 eV は log の都合で扱いづらいので 1 eV 相当を入れる
        for (int energyEv = 1; energyEv < cache.Length; energyEv++)
            cache[energyEv] = ComputeTransportParameters(energyEv * 0.001); // (260331Ch) 1 eV 刻みなら近似誤差は十分小さい
        return cache;
    }

    private BulkLossSamplerEntry[] BuildBulkLossSamplerCache()
    {
        const int energyStepEv = 10; // (260331Ch) 10 eV 刻みなら十分軽く、loss spectrum の変化も追いやすい
        int maxEnergyEv = Math.Max(energyStepEv, (int)Math.Ceiling(InitialKev * 1000.0));
        int entryCount = maxEnergyEv / energyStepEv + 1;
        var cache = new BulkLossSamplerEntry[entryCount];
        for (int entryIndex = 0; entryIndex < cache.Length; entryIndex++)
        {
            int energyEv = Math.Max(1, entryIndex * energyStepEv);
            var parameters = TransportParameterCache[Math.Min(energyEv, TransportParameterCache.Length - 1)];
            cache[entryIndex] = CreateBulkLossSamplerEntry(energyEv, parameters);
        }
        return cache;
    }

    private BulkLossSamplerEntry CreateBulkLossSamplerEntry(int energyEv, TransportParameters parameters)
    {
        const int binCount = 256;
        double minLossEv = Math.Max(BandGapEv > 0 ? BandGapEv : 1.0, 0.5);
        double meanLossEv = parameters.MeanInelasticLossKev * 1000.0;
        if (!(meanLossEv > 0) || energyEv <= minLossEv + 1.0)
            return null;

        double ep = Math.Max(TppPlasmaEnergyEv, minLossEv + 0.5);
        double maxLossEv = Math.Min(energyEv - 0.5, Math.Max(Math.Max(12.0 * ep, 6.0 * meanLossEv), 250.0));
        if (!(maxLossEv > minLossEv))
            return null;

        double lossStepEv = (maxLossEv - minLossEv) / binCount;
        if (!(lossStepEv > 0))
            return null;

        var lowLossPdf = new double[binCount];
        var plasmonPdf = new double[binCount];
        var tailPdf = new double[binCount];
        double plasmonSigmaEv = Math.Max(1.5, 0.18 * ep);
        double tailOnsetEv = Math.Min(maxLossEv, Math.Max(1.8 * ep, 30.0));
        for (int i = 0; i < binCount; i++)
        {
            double lossEv = minLossEv + (i + 0.5) * lossStepEv;
            lowLossPdf[i] = 1.0 / Math.Pow(lossEv + 0.35 * ep, 1.6);
            double z = (lossEv - ep) / plasmonSigmaEv;
            plasmonPdf[i] = Math.Exp(-0.5 * z * z);
            tailPdf[i] = lossEv >= tailOnsetEv ? 1.0 / Math.Pow(lossEv, 2.2) : 0.0;
        }

        NormalizePdf(lowLossPdf);
        NormalizePdf(plasmonPdf);
        NormalizePdf(tailPdf);

        double muLow = MeanLossEv(lowLossPdf, minLossEv, lossStepEv);
        double muPlasmon = MeanLossEv(plasmonPdf, minLossEv, lossStepEv);
        double muTail = MeanLossEv(tailPdf, minLossEv, lossStepEv);
        var combinedPdf = new double[binCount];
        if (muTail <= muPlasmon + 1e-9 || meanLossEv <= muPlasmon)
        {
            double wLow = Clamp01((muPlasmon - meanLossEv) / Math.Max(muPlasmon - muLow, 1e-9));
            double wPlasmon = 1.0 - wLow;
            CombinePdfs(combinedPdf, lowLossPdf, wLow, plasmonPdf, wPlasmon, tailPdf, 0.0);
        }
        else
        {
            double wLow = Math.Clamp(0.12 + 0.18 * muPlasmon / Math.Max(meanLossEv, muPlasmon), 0.08, 0.30);
            double remainingMean = (meanLossEv - wLow * muLow) / Math.Max(1.0 - wLow, 1e-9);
            double t = Clamp01((remainingMean - muPlasmon) / Math.Max(muTail - muPlasmon, 1e-9));
            double wTail = (1.0 - wLow) * t;
            double wPlasmon = 1.0 - wLow - wTail;
            CombinePdfs(combinedPdf, lowLossPdf, wLow, plasmonPdf, wPlasmon, tailPdf, wTail);
        }

        NormalizePdf(combinedPdf);
        var cumulativeProbabilities = new double[binCount];
        double cumulative = 0.0;
        for (int i = 0; i < binCount; i++)
        {
            cumulative += combinedPdf[i];
            cumulativeProbabilities[i] = cumulative;
        }
        cumulativeProbabilities[^1] = 1.0;
        return new BulkLossSamplerEntry(minLossEv * 0.001, lossStepEv * 0.001, cumulativeProbabilities);
    }

    private static void CombinePdfs(double[] destination, double[] pdf1, double weight1, double[] pdf2, double weight2, double[] pdf3, double weight3)
    {
        for (int i = 0; i < destination.Length; i++)
            destination[i] = weight1 * pdf1[i] + weight2 * pdf2[i] + weight3 * pdf3[i];
    }

    private static void NormalizePdf(double[] pdf)
    {
        double sum = 0.0;
        for (int i = 0; i < pdf.Length; i++)
            sum += pdf[i];
        if (!(sum > 0))
            return;
        for (int i = 0; i < pdf.Length; i++)
            pdf[i] /= sum;
    }

    private static double MeanLossEv(double[] pdf, double minLossEv, double lossStepEv)
    {
        double sum = 0.0;
        for (int i = 0; i < pdf.Length; i++)
            sum += pdf[i] * (minLossEv + (i + 0.5) * lossStepEv);
        return sum;
    }

    private static double Clamp01(double value)
        => Math.Clamp(value, 0.0, 1.0);

    public (double ScreeningParameter, double CrossSection, double MeanFreePath, double StoppingPower) GetParameters(double kev)
        => GetParameters(GetTransportParameters(kev)); // (260331Ch)

    private static (double ScreeningParameter, double CrossSection, double MeanFreePath, double StoppingPower) GetParameters(TransportParameters parameters)
        => (parameters.ScreeningParameter, parameters.ElasticCrossSectionNm2, parameters.ElasticMeanFreePathNm, parameters.StoppingPowerKevPerNm); // (260331Ch)

    private TransportParameters GetTransportParameters(double kev)
    {
        int energyEv = (int)Math.Round(kev * 1000.0);
        if ((uint)energyEv < (uint)TransportParameterCache.Length)
            return TransportParameterCache[energyEv];
        return ComputeTransportParameters(kev);
    }

    private TransportParameters ComputeTransportParameters(double kev)
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
        var λ_in = GetInelasticMeanFreePathAngstrom(kev * 1000.0) * 0.1; // (260331Ch) TPP-2M の IMFP [A] -> [nm]
        var meanLossKev = λ_in > 0 && sp < 0 ? -sp * λ_in : double.NaN; // (260331Ch) 平均的には <ΔE>/λ_in = stopping power を満たす
        return new TransportParameters(α, σ_E, λ_el, sp, λ_in, meanLossKev);
    }

    private bool TryGetMottElasticTransport(double kev, out double crossSectionNm2, out double meanFreePathNm)
    {
        crossSectionNm2 = meanFreePathNm = double.NaN;
        var energyEv = kev * 1000.0;
        if (MottElasticMixtureCache.Length == 0 || !(TotalElasticNumberDensityPerNm3 > 0) || energyEv < 50.0 || energyEv > 20000.0)
            return false;

        int lowerIndex = GetLowerNistElasticEnergyIndex(energyEv, out var fraction);
        var totalMacroscopicCrossSection = MottElasticMixtureCache[lowerIndex].TotalMacroscopicCrossSectionPerNm;
        if (fraction > 0 && lowerIndex < MottElasticMixtureCache.Length - 1)
            totalMacroscopicCrossSection += fraction * (MottElasticMixtureCache[lowerIndex + 1].TotalMacroscopicCrossSectionPerNm - totalMacroscopicCrossSection); // (260331Ch)
        if (!(totalMacroscopicCrossSection > 0))
            return false;

        crossSectionNm2 = totalMacroscopicCrossSection / TotalElasticNumberDensityPerNm3;
        meanFreePathNm = 1.0 / totalMacroscopicCrossSection;
        return true;
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
        if (ElasticComponents.Length == 0 || MottElasticMixtureCache.Length == 0)
            return false;

        var energyEv = kev * 1000.0;
        if (energyEv < 50.0 || energyEv > 20000.0)
            return false;

        int energyIndex = GetNearestNistElasticEnergyIndex(energyEv);
        var mixture = MottElasticMixtureCache[energyIndex];
        var choice = Rnd.NextDouble();
        int speciesIndex;
        if (ElasticComponents.Length == 1)
            speciesIndex = 0; // (260331Ch) 単一元素では mixture CDF の binary search を省く
        else
        {
            speciesIndex = Array.BinarySearch(mixture.CumulativeProbabilities, choice);
            if (speciesIndex < 0)
                speciesIndex = ~speciesIndex;
            if (speciesIndex >= ElasticComponents.Length)
                speciesIndex = ElasticComponents.Length - 1;
        }

        // var table = GetNistElasticScatteringTable(ElasticComponents[speciesIndex].AtomicNumber); // (260331Ch) 旧実装
        var table = ElasticComponents[speciesIndex].NistElasticTable;
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

    private static int GetLowerNistElasticEnergyIndex(double energyEv, out double fraction)
    {
        var logEnergy = Math.Log(energyEv);
        int lowerIndex = (int)Math.Floor((logEnergy - LogNistElasticMinEnergyEv) / LogNistElasticEnergyStep);
        lowerIndex = Math.Clamp(lowerIndex, 0, 99);
        var lowerEnergy = LogNistElasticMinEnergyEv + lowerIndex * LogNistElasticEnergyStep;
        var upperEnergy = lowerEnergy + LogNistElasticEnergyStep;
        fraction = (logEnergy - lowerEnergy) / (upperEnergy - lowerEnergy);
        return lowerIndex;
    }

    private static NistElasticScatteringTable GetNistElasticScatteringTable(int atomicNumber)
    {
        lock (NistElasticSamplerSync)
        {
            if (NistElasticSamplerCache.TryGetValue(atomicNumber, out var cached))
                return cached;
        }

        var pathBinary = Path.Combine(NistElasticSamplerDirectory, $"E_{atomicNumber:00}.BIN"); // (260331Ch) 配布サイズ削減のため、まず float32 binary を優先
        var pathText = Path.Combine(NistElasticSamplerDirectory, $"E_{atomicNumber:00}.TXT"); // (260331Ch) 旧 text sampler は fallback として残す
        // NistElasticScatteringTable table = null; // (260331Ch) 旧実装
        NistElasticScatteringTable table = TryLoadNistElasticBinaryTable(pathBinary) ?? TryLoadNistElasticTextTable(pathText); // (260331Ch)

        lock (NistElasticSamplerSync)
            NistElasticSamplerCache[atomicNumber] = table;
        return table;
    }

    private static NistElasticScatteringTable TryLoadNistElasticBinaryTable(string path)
    {
        if (!File.Exists(path))
            return null;

        try
        {
            using var stream = File.OpenRead(path);
            using var reader = new BinaryReader(stream);
            if (reader.ReadInt32() != NistElasticBinaryMagic || reader.ReadInt32() != NistElasticBinaryVersion)
                return null;

            if (reader.ReadInt32() != NistElasticEnergyCount || reader.ReadInt32() != NistElasticPhiCount)
                return null;

            var table = new NistElasticScatteringTable();
            for (int i = 0; i < NistElasticEnergyCount; i++)
            {
                table.SigmaA0Squared[i] = reader.ReadSingle();
                var phi = new double[NistElasticPhiCount];
                for (int j = 0; j < phi.Length; j++)
                    phi[j] = reader.ReadSingle();
                table.Phi[i] = phi;
            }
            return stream.Position == stream.Length ? table : null; // (260331Ch) 末尾に余計なデータがあれば無効とみなす
        }
        catch
        {
            return null;
        }
    }

    private static NistElasticScatteringTable TryLoadNistElasticTextTable(string path)
    {
        if (!File.Exists(path))
            return null;

        try
        {
            using var reader = new StreamReader(path);
            var table = new NistElasticScatteringTable();
            for (int i = 0; i < NistElasticEnergyCount; i++)
            {
                _ = reader.ReadLine(); // (260331Ch) NIST sampler 内のブロック番号。Fortran 版でも未使用
                table.SigmaA0Squared[i] = double.Parse(reader.ReadLine() ?? throw new InvalidDataException(path), CultureInfo.InvariantCulture);
                var phi = new double[NistElasticPhiCount];
                for (int j = 0; j < phi.Length; j++)
                    phi[j] = double.Parse(reader.ReadLine() ?? throw new InvalidDataException(path), CultureInfo.InvariantCulture);
                table.Phi[i] = phi;
            }
            return table;
        }
        catch
        {
            return null;
        }
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
            MonteCarloStoppingPowerModel.JablonskiModified2008
                => Jablonski2008D1 * Math.Pow(energyEv, Jablonski2008D2) * Math.Log(Jablonski2008D3 * energyEv) * Math.Pow(ρ + Jablonski2008D4, Jablonski2008D5) / λ_in,
            _ => throw new ArgumentOutOfRangeException(),
        };

        return -spEvPerAngstrom * 0.01; // (260331Ch) 1 eV/Å = 0.01 keV/nm
    }

    private double GetInelasticMeanFreePathAngstrom(double energyEv)
    {
        var gammaE = TppGamma * energyEv;
        if (!(gammaE > 1.0))
            return double.NaN;

        var denominator = TppPlasmaEnergyEv * TppPlasmaEnergyEv * (TppBeta * Math.Log(gammaE) - TppC / energyEv + TppD / (energyEv * energyEv));
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

    private double SampleInelasticLossKev(double currentKev, double meanLossKev)
    {
        if (!(meanLossKev > 0))
            return 0.0;

        var lossKev = InelasticScatteringModel switch
        {
            MonteCarloInelasticScatteringModel.DiscreteMeanLoss => meanLossKev,
            MonteCarloInelasticScatteringModel.DiscreteExponentialLoss => -meanLossKev * Math.Log(Math.Max(Rnd.NextDouble(), double.Epsilon)),
            MonteCarloInelasticScatteringModel.DiscreteBulkDiimfpApproximation => SampleBulkLossKev(currentKev, meanLossKev),
            _ => 0.0,
        }; // (260331Ch) まずは平均損失を保つ簡易離散モデルを入れ、低損失 tail は指数分布で試せるようにする
        return Math.Min(lossKev, currentKev);
    }

    private double SampleBulkLossKev(double currentKev, double meanLossKev)
    {
        if (BulkLossSamplerCache.Length == 0)
            return meanLossKev;

        int energyIndex = (int)Math.Round(currentKev * 100.0); // (260331Ch) 10 eV 刻み cache
        if ((uint)energyIndex >= (uint)BulkLossSamplerCache.Length)
            energyIndex = BulkLossSamplerCache.Length - 1;
        var entry = BulkLossSamplerCache[energyIndex];
        if (entry == null)
            return meanLossKev;

        var target = Rnd.NextDouble();
        int upper = Array.BinarySearch(entry.CumulativeProbabilities, target);
        if (upper < 0)
            upper = ~upper;
        upper = Math.Clamp(upper, 1, entry.CumulativeProbabilities.Length - 1);
        int lower = upper - 1;
        var p0 = entry.CumulativeProbabilities[lower];
        var p1 = entry.CumulativeProbabilities[upper];
        var l0 = entry.MinLossKev + (lower + 0.5) * entry.LossStepKev;
        var l1 = entry.MinLossKev + (upper + 0.5) * entry.LossStepKev;
        var lossKev = p1 > p0 ? l0 + (target - p0) * (l1 - l0) / (p1 - p0) : l0;
        return Math.Min(lossKev, currentKev);
    }

    /// <summary>
    /// 電子線の飛程を計算する. 電子は -Z軸に沿って入射し、試料と(0,0,0)の座標で衝突したあと、thresholdで指定したエネルギーまで減衰するか、
    /// 試料表面を脱出するまでの飛程を計算する。返り値は、座標 p (nm単位)と エネルギー e (kev単位) のタプル配列
    /// </summary>
    /// <returns>返り値は、座標 p (nm単位)と エネルギー e (kev単位) のタプル配列</returns>
    public List<(V3 p, double e)> GetTrajectories()
    {
        if (InelasticScatteringModel != MonteCarloInelasticScatteringModel.ContinuousSlowingDownApproximation)
            return GetTrajectoriesDiscreteInelastic(); // (260331Ch) 旧 CSDA 実装は残し、比較できるように分岐する

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

    private List<(V3 p, double e)> GetTrajectoriesDiscreteInelastic()
    {
        var trajectory = new List<(V3 p, double e)>(100) { (new V3(0, 0, 0), InitialKev) };
        double m11, m12, m13, m22, m23;
        double vX = 0, vY = 0, vZ = -1;
        int n = 0;

        while (trajectory[^1].e > ThresholdKev && trajectory[^1].p.Y * tan >= trajectory[^1].p.Z)
        {
            var parameters = GetTransportParameters(trajectory[^1].e);
            if (!(parameters.ElasticMeanFreePathNm > 0))
                break;

            var elasticRate = 1.0 / parameters.ElasticMeanFreePathNm;
            var inelasticRate = parameters.InelasticMeanFreePathNm > 0 && parameters.MeanInelasticLossKev > 0
                ? 1.0 / parameters.InelasticMeanFreePathNm
                : 0.0; // (260331Ch) IMFP が破綻した点では弾性散乱だけで継続する
            var totalRate = elasticRate + inelasticRate;
            if (!(totalRate > 0))
                break;

            var s = -Math.Log(Math.Max(Rnd.NextDouble(), double.Epsilon)) / totalRate;
            var nextPoint = trajectory[^1].p + s * new V3(vX, vY, vZ);
            if (nextPoint.Y * tan < nextPoint.Z)
            {
                trajectory.Add((nextPoint, trajectory[^1].e)); // (260331Ch) 表面を抜けた時点では試料内の次イベントは未発生
                break;
            }

            bool isElastic = inelasticRate <= 0 || Rnd.NextDouble() * totalRate < elasticRate;
            double nextEnergy = trajectory[^1].e;
            if (isElastic)
            {
                double rnd3 = Rnd.NextDouble();
                double cosθ = SampleElasticScatteringCosTheta(trajectory[^1].e, parameters.ScreeningParameter), sinθ = Math.Sqrt(1 - cosθ * cosθ); // (260331Ch)
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

                    if (++n % 10 == 0)
                    {
                        var len = Math.Sqrt(vX * vX + vY * vY + vZ * vZ);
                        vX /= len; vY /= len; vZ /= len;
                    }
                }
            }
            else
            {
                nextEnergy = Math.Max(trajectory[^1].e - SampleInelasticLossKev(trajectory[^1].e, parameters.MeanInelasticLossKev), 0.0); // (260331Ch)
            }

            trajectory.Add((nextPoint, nextEnergy));
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
        var electron = GetBackscatteredElectronDetail();
        return (electron.Depth, electron.Direction, electron.Energy); // (260331Ch) 既存呼び出しは壊さず、詳細版へ寄せる
    }

    public BackscatteredElectronDetail GetBackscatteredElectronDetail()
    {
        if (InelasticScatteringModel != MonteCarloInelasticScatteringModel.ContinuousSlowingDownApproximation)
            return GetBackscatteredElectronDetailDiscreteInelastic(); // (260331Ch)

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
        return new BackscatteredElectronDetail(
            d,
            new V3(vX, vY, vZ),
            e,
            InitialKev - e,
            false,
            double.NaN,
            double.NaN,
            double.NaN,
            new V3(double.NaN, double.NaN, double.NaN)); // (260331Ch) CSDA では最後の離散非弾性散乱は定義できない
    }

    private BackscatteredElectronDetail GetBackscatteredElectronDetailDiscreteInelastic()
    {
        double e = InitialKev;
        double vX = 0, vY = 0, vZ = -1;
        double d = 0;
        int n = 0;
        bool hasLastInelasticEvent = false;
        double lastInelasticDepth = double.NaN, lastInelasticEnergyBeforeLoss = double.NaN, lastInelasticEnergyAfterLoss = double.NaN;
        var lastInelasticDirection = new V3(double.NaN, double.NaN, double.NaN);

        while (e > ThresholdKev)
        {
            var parameters = GetTransportParameters(e);
            if (!(parameters.ElasticMeanFreePathNm > 0))
                break;

            var elasticRate = 1.0 / parameters.ElasticMeanFreePathNm;
            var inelasticRate = parameters.InelasticMeanFreePathNm > 0 && parameters.MeanInelasticLossKev > 0
                ? 1.0 / parameters.InelasticMeanFreePathNm
                : 0.0;
            var totalRate = elasticRate + inelasticRate;
            if (!(totalRate > 0))
                break;

            var s = -Math.Log(Math.Max(Rnd.NextDouble(), double.Epsilon)) / totalRate;
            var dtmp = d + s * (sin * vY - cos * vZ); // (260331Ch) 現在の進行方向で次イベント位置まで進む
            if (dtmp < 0)
                break;
            d = dtmp;

            bool isElastic = inelasticRate <= 0 || Rnd.NextDouble() * totalRate < elasticRate;
            if (isElastic)
            {
                double rnd3 = Rnd.NextDouble();
                double cosθ = SampleElasticScatteringCosTheta(e, parameters.ScreeningParameter), sinθ = Math.Sqrt(1 - cosθ * cosθ); // (260331Ch)
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

                    if (++n % 10 == 0)
                    {
                        var len = Math.Sqrt(vX * vX + vY * vY + vZ * vZ);
                        vX /= len; vY /= len; vZ /= len;
                    }
                }
            }
            else
            {
                hasLastInelasticEvent = true;
                lastInelasticDepth = d;
                lastInelasticEnergyBeforeLoss = e;
                lastInelasticDirection = new V3(vX, vY, vZ);
                e = Math.Max(e - SampleInelasticLossKev(e, parameters.MeanInelasticLossKev), 0.0); // (260331Ch)
                lastInelasticEnergyAfterLoss = e;
            }
        }
        return new BackscatteredElectronDetail(
            d,
            new V3(vX, vY, vZ),
            e,
            InitialKev - e,
            hasLastInelasticEvent,
            lastInelasticDepth,
            lastInelasticEnergyBeforeLoss,
            lastInelasticEnergyAfterLoss,
            lastInelasticDirection);
    }
    public const double Th = 0.0000001;

}
