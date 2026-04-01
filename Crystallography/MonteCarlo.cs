using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using V3 = OpenTK.Mathematics.Vector3d;
using System.Threading;

namespace Crystallography;

//Electron beam-specimen interactions and simulation methods in microscopy 2018
//Eqs (2.38), (2.41), (2.42) などを参考
public class MonteCarlo
{
    #region モデル
    public enum StoppingPowerModels
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

    public enum ElasticScatteringModels
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

    #region お蔵入り // (260401Ch) generated / external の source flag 比較経路は配布版では使わない
    // public enum ElasticSamplerDataSources
    // {
    //     /// <summary>260401Ch generated PCHIP を優先し、存在しなければ外部 TXT にフォールバックする既定動作</summary>
    //     Auto,
    //     /// <summary>260401Ch generated PCHIP のみを使う。未生成元素は Mott sampler が使えず Screened Rutherford に落ちる</summary>
    //     GeneratedOnly,
    //     /// <summary>260401Ch 外部 TXT テーブルのみを使う。圧縮前との比較ベンチ用</summary>
    //     ExternalTextOnly,
    // }
    #endregion

    public enum InelasticScatteringModels
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

        // 260401Cl DiscreteExponentialLoss を除去 (指数分布は実際の損失スペクトルと形が大きく異なり非推奨のため)

        /// <summary>
        /// 離散非弾性散乱モデル (簡易バルク DIIMFP 近似)。
        /// プラズモンピーク・低損失テール・高損失テールの 3 成分を混合した損失分布からサンプリングする。
        /// エネルギー損失の確率的ばらつきを物理的に最も妥当な形で再現するため、
        /// 「最後の非弾性散乱後のエネルギー」の分布解析に最適。計算コストは DiscreteMeanLoss よりやや大きい。
        /// </summary>
        DiscreteBulkDiimfpApproximation, // 260331Cl コメント追加
    }
    #endregion

    #region const, static 定数

    public const double Th = 0.0000001; // 260401Cl 方向ベクトル回転時の特異点判定閾値。vZ ≈ -1 (ほぼ真下向き) のとき回転行列が退化するのを避ける

    private static readonly double log50 = Math.Log(50.0); // (260331Ch) NIST テーブルの最小エネルギー 50 eV の自然対数
    private static readonly double log20000 = Math.Log(20000.0); // (260331Ch) NIST テーブルの最大エネルギー 20 keV の自然対数
    private static readonly double LogNistElasticEnergyStep = (log20000 - log50) / 100.0; // (260331Ch) 対数エネルギー軸の 101 点間隔


    // 260401Cl Jablonski (2008) 修正阻止能モデルのフィッティング定数 D1〜D5。
    // 阻止能 S(E) = D1·E^D2·ln(D3·E)·(ρ+D4)^D5 / λ_in  [eV/Å] の形で使用。
    private const double Jablonski2008D1 = 7.89271; // (260331Ch)
    private const double Jablonski2008D2 = 0.0117088; // (260331Ch)
    private const double Jablonski2008D3 = 0.0545126; // (260331Ch)
    private const double Jablonski2008D4 = -0.0254488; // (260331Ch)
    private const double Jablonski2008D5 = 0.326907; // (260331Ch)
    private const int NistElasticEnergyCount = 101; // (260331Ch) NIST sampler energies: 50 eV - 20 keV, log spaced
    #region お蔵入り // (260401Ch) オリジナル TXT の 2001 点 CDF は配布版ランタイムでは使わない
    // private const int NistElasticPhiCount = 2001; // (260331Ch) X = cos(theta) from +1 to -1 with step 0.001
    #endregion

    #endregion



    private readonly Random Rnd = Random.Shared;
    /// <summary>平均原子番号 (混合物の場合は重み付き平均) </summary>
    public readonly double Z;
    /// <summary>平均原子量 (g/mol) </summary>
    public readonly double A;
    /// <summary>密度 (g/cm³) </summary>
    public readonly double ρ;  // 260401Cl 密度 (g/cm³)
    /// <summary>260401Cl 入射電子のエネルギー (keV)</summary>
    public readonly double InitialKev;
    /// <summary>260401Cl 試料表面の傾斜角 (rad, X軸回り)</summary>
    public readonly double Tilt;
    /// <summary>260401Cl Screened Rutherford 遮蔽パラメータ α の係数: 0.0034 * Z^(2/3)。α = coeff0 / E で遮蔽効果を表す</summary>
    public readonly double coeff0;
    /// <summary>260401Cl 弾性散乱断面積 σ_E の係数: Ze²/(8πε₀)。Rutherford 散乱の微分断面積の前因子</summary>
    public readonly double coeff1;
    /// <summary>260401Cl 弾性平均自由行程 λ_el の係数: A/(N_A·ρ) [nm³]。λ_el = coeff2 / σ_E で求まる</summary>
    public readonly double coeff2;
    /// <summary>260401Cl Joy-Luo 阻止能 dE/ds の係数 (keV/nm 単位)。Bethe 式の前因子 -Z·N_A·ρ·e⁴/(4πε₀²·A) を含む</summary>
    public readonly double coeff3;
    /// <summary>260401Cl Joy-Luo 阻止能の低エネルギー補正係数。Z 依存の経験的パラメータ (k = 0.0299·ln(Z) + 0.7307)</summary>
    public readonly double k;
    /// <summary>260401Cl 平均イオン化ポテンシャル (eV)。Bethe 阻止能式で物質のエネルギー損失特性を決める定数</summary>
    public readonly double J;
    /// <summary>260401Cl tan(tilt): 試料表面境界の判定に使用 (Y·tan ≥ Z で試料内)</summary>
    public readonly double tan;
    /// <summary>260401Cl cos(tilt): 深さ d の更新時の射影成分 (d += s·(sin·vY - cos·vZ))</summary>
    public readonly double cos;
    /// <summary>260401Cl sin(tilt): 深さ d の更新時の射影成分</summary>
    public readonly double sin;
    /// <summary>(260331Ch) TPP-2M に使う平均価電子数 Nv</summary>
    public readonly double ValenceElectronCount;
    /// <summary>(260331Ch) バンドギャップ Eg (eV)</summary>
    public readonly double BandGapEv;
    /// <summary>(260331Ch) 阻止能モデルの切替</summary>
    public readonly StoppingPowerModels StoppingPowerModel;
    /// <summary>(260331Ch) 弾性散乱モデルの切替</summary>
    public readonly ElasticScatteringModels ElasticScatteringModel;
    /// <summary>(260331Ch) 非弾性散乱モデルの切替</summary>
    public readonly InelasticScatteringModels InelasticScatteringModel;
    /// <summary>260401Cl シミュレーション打ち切りエネルギー (keV)。電子エネルギーがこれ以下になると追跡を終了する</summary>
    public readonly double ThresholdKev;
    /// <summary>(260331Ch) Mott/NIST sampler 用の元素組成と数密度</summary>
    private readonly ElasticSpecies[] ElasticComponents = [];
    /// <summary>(260331Ch) 混合系の全元素数密度合計 [1/nm³]。巨視的断面積 Σ = Σ_i(n_i·σ_i) から有効微視的断面積 σ_eff = Σ/n_total を逆算する際に使う</summary>
    private readonly double TotalElasticNumberDensityPerNm3;
    /// <summary>260401Cl TPP-2M (Tanuma-Powell-Penn, 2M パラメータ版) の材料定数。非弾性平均自由行程 λ_in(E) = E / {Ep²·[β·ln(γE) - C/E + D/E²]} で使用</summary>
    /// <summary>260401Cl 自由電子プラズマエネルギー Ep = 28.8·√(Nv·ρ/A) [eV]。価電子のプラズモン励起エネルギーに対応</summary>
    private readonly double TppPlasmaEnergyEv;
    /// <summary>260401Cl TPP-2M の β パラメータ。ln(γE) の係数で、IMFP のエネルギー依存の主項を制御</summary>
    private readonly double TppBeta;
    /// <summary>260401Cl TPP-2M の γ パラメータ [1/eV]。γE が対数項の引数で、密度依存 (0.191·ρ^(-0.5))</summary>
    private readonly double TppGamma;
    /// <summary>260401Cl TPP-2M の C パラメータ。1/E 項の係数で低エネルギー側の IMFP 補正に寄与</summary>
    private readonly double TppC;
    /// <summary>260401Cl TPP-2M の D パラメータ。1/E² 項の係数でさらに低エネルギー側の補正</summary>
    private readonly double TppD;
    /// <summary>(260331Ch) 1 eV 刻みの輸送パラメータ cache</summary>
    private readonly TransportParameters[] TransportParameterCache = [];
    /// <summary>(260331Ch) NIST sampler の混合断面積/CDF cache</summary>
    private readonly MottElasticMixtureEntry[] MottElasticMixtureCache = [];
    /// <summary>(260331Ch) 軽量な bulk DIIMFP 近似 sampler cache</summary>
    private readonly BulkLossSamplerEntry[] BulkLossSamplerCache = [];
    #region お蔵入り // (260401Ch) generated / external の source flag 比較経路は配布版では使わない
    // /// <summary>260401Ch generated / external / auto の sampler source flag。MC ベンチで圧縮版と元テーブルを比較するために使う</summary>
    // public readonly ElasticSamplerDataSources ElasticSamplerDataSource;
    //
    // /// <summary>(260331Ch) 配布物に同梱した sampler を既定で使う</summary>
    // public static string NistElasticSamplerDirectory { get; set; } = Path.Combine(AppContext.BaseDirectory, "NistElasticSampler");
    // /// <summary>260401Ch 元の TXT テーブル置き場。ベンチ時は source tree の NistElasticSampler_Original を指す</summary>
    // public static string NistElasticSamplerTextDirectory { get; set; } = Path.Combine(AppContext.BaseDirectory, "NistElasticSampler_Original");
    // /// <summary>260401Ch constructor で source 未指定時に使う既定値</summary>
    // public static ElasticSamplerDataSources DefaultElasticSamplerDataSource { get; set; } = ElasticSamplerDataSources.Auto;
    #endregion

    /// <summary>(260331Ch) NIST テーブル cache のスレッド同期用</summary>
    private static readonly Lock lockObj = new();
    /// <summary>260401Cl 原子番号→NIST弾性散乱テーブルの static cache。全 MonteCarlo インスタンスで共有</summary>
    #region お蔵入り // (260401Ch) source flag ごとの cache は配布版では使わない
    // private static readonly Dictionary<(int AtomicNumber, ElasticSamplerDataSources Source), NistElasticScatteringTable> NistElasticSamplerCache = [];
    #endregion
    private static readonly Dictionary<int, NistElasticScatteringTable> NistElasticSamplerCache = []; // (260401Ch) 配布版は generated data のみを原子番号ごとに cache する
    /// <summary>(260331Ch) Bohr 半径の二乗 a₀² から nm² への換算係数: 1 a₀² = 2.8002852×10⁻³ nm²</summary>
    private const double NistElasticCrossSectionUnitNm2 = 2.8002852E-3;

    #region class, record

    /// <summary>混合物中の各元素の原子番号・数密度・NIST散乱テーブルを保持する。Mott 散乱サンプリング時に元素選択と角度サンプリングに使用。</summary>
    private readonly record struct ElasticSpecies(int AtomicNumber, double NumberDensityPerNm3, NistElasticScatteringTable NistElasticTable); // (260331Ch) Mott/NIST sampler の毎イベント lock/dictionary lookup を避ける

    /// <summary>NIST SRD 64 の弾性散乱データを保持するテーブル。101 エネルギー点 (50 eV〜20 keV, 対数等間隔) ごとに全断面積と累積角度分布を格納。</summary>
    private sealed class NistElasticScatteringTable // (260331Ch)
    {
        public int AtomicNumber;
        /// <summary>260401Cl PCHIP 補間で生成済みの高速ルックアップデータ (存在すれば Phi[] より優先)</summary>
        public NistElasticPchipRuntimeElement GeneratedPchipRuntimeElement;
        /// <summary>260401Cl 各エネルギーでの弾性散乱全断面積 σ [a₀² 単位]</summary>
        public readonly double[] SigmaA0Squared = new double[NistElasticEnergyCount];
        #region お蔵入り // (260401Ch) オリジナル TXT の 2001 点 CDF は配布版ランタイムでは使わない
        // /// <summary>260401Cl 累積散乱角分布 Φ(cosθ)。cosθ = 1〜-1 を 0.001 刻み 2001 点で格納。逆関数法で散乱角をサンプリング</summary>
        // public readonly double[][] Phi = new double[NistElasticEnergyCount][];
        #endregion
    }

    /// <summary>混合物系の弾性散乱における各エネルギーでの巨視的断面積と元素選択 CDF。散乱イベント発生時にどの元素で散乱するかを確率的に決定する。</summary>
    private sealed class MottElasticMixtureEntry(double totalMacroscopicCrossSectionPerNm, double[] cumulativeProbabilities) // (260331Ch)
    {
        /// <summary>260401Cl 全元素の巨視的弾性散乱断面積の合計 Σ_total = Σ_i(n_i·σ_i) [1/nm]。逆数が弾性平均自由行程</summary>
        public readonly double TotalMacroscopicCrossSectionPerNm = totalMacroscopicCrossSectionPerNm;
        /// <summary>260401Cl 元素選択用の累積確率。i 番目の元素が選ばれる確率 = n_i·σ_i / Σ_total</summary>
        public readonly double[] CumulativeProbabilities = cumulativeProbabilities;
    }

    /// <summary>DiscreteBulkDiimfpApproximation 用のエネルギー損失サンプラー。低損失・プラズモン・高損失テールの 3 成分混合 PDF から CDF を構築し、逆関数法でサンプリングする。</summary>
    // private sealed class BulkLossSamplerEntry(double minLossKev, double lossStepKev, double[] cumulativeProbabilities) // 260401Cl 旧シグネチャ
    private sealed class BulkLossSamplerEntry(double minLossKev, double lossStepKev, double[] cumulativeProbabilities, byte[] guideTable) // 260401Cl GuideTable 追加
    {
        /// <summary>260401Cl 損失スペクトルの最小エネルギー損失 (keV)。バンドギャップ以上の値</summary>
        public readonly double MinLossKev = minLossKev;
        /// <summary>260401Cl CDF の 1 ビンあたりのエネルギー幅 (keV)</summary>
        public readonly double LossStepKev = lossStepKev;
        /// <summary>260401Cl エネルギー損失分布の累積確率 (256 ビン)。逆関数法でサンプリング</summary>
        public readonly double[] CumulativeProbabilities = cumulativeProbabilities;
        /// <summary>260401Cl CDF バイナリサーチを O(1) に高速化するガイドテーブル (64 エントリ)</summary>
        public readonly byte[] GuideTable = guideTable;
    }

    /// <summary>後方散乱電子の詳細情報。EBSD パターン形成に寄与する電子の最後の非弾性散乱の深さ・エネルギー・方向を記録する。</summary>
    /// <param name="Depth">260401Cl 電子が試料表面を脱出した (または停止した) 時点での表面からの深さ (nm)</param>
    /// <param name="Direction">260401Cl 脱出時の進行方向の単位ベクトル</param>
    /// <param name="Energy">260401Cl 脱出時のエネルギー (keV)</param>
    /// <param name="TotalEnergyLoss">260401Cl 入射エネルギーからの総エネルギー損失 (keV)</param>
    /// <param name="HasLastInelasticEvent">260401Cl 離散非弾性散乱イベントが 1 回以上発生したか (CSDA モードでは常に false)</param>
    /// <param name="LastInelasticDepth">260401Cl 最後の非弾性散乱が起きた深さ (nm)。EBSD の情報深さに直結</param>
    /// <param name="LastInelasticEnergyBeforeLoss">260401Cl 最後の非弾性散乱直前のエネルギー (keV)</param>
    /// <param name="LastInelasticEnergyAfterLoss">260401Cl 最後の非弾性散乱直後のエネルギー (keV)。この電子が回折に寄与する</param>
    /// <param name="LastInelasticDirection">260401Cl 最後の非弾性散乱時点での進行方向。回折条件の評価に使用</param>
    public readonly record struct BackscatteredElectronDetail( // (260331Ch) EBSD 寄与電子の最後の非弾性散乱情報を後段で解析できるようにする
        double Depth, V3 Direction, double Energy, double TotalEnergyLoss, bool HasLastInelasticEvent,
        double LastInelasticDepth, double LastInelasticEnergyBeforeLoss, double LastInelasticEnergyAfterLoss, V3 LastInelasticDirection);

    /// <summary>あるエネルギーにおける電子輸送パラメータの一式。弾性・非弾性散乱のステップ長と方向・エネルギー損失の計算に使う。</summary>
    /// <param name="ScreeningParameter">260401Cl Screened Rutherford の遮蔽パラメータ α = coeff0/E。原子核電荷の遮蔽効果を表し、散乱角分布の前方集中度を制御</param>
    /// <param name="ElasticCrossSectionNm2">260401Cl 弾性散乱全断面積 σ_el [nm²]。単一散乱イベントの確率を決める</param>
    /// <param name="ElasticMeanFreePathNm">260401Cl 弾性平均自由行程 λ_el = 1/(n·σ_el) [nm]。連続する弾性散乱間の平均飛行距離</param>
    /// <param name="StoppingPowerKevPerNm">260401Cl 阻止能 dE/ds [keV/nm] (負値)。単位飛行距離あたりのエネルギー損失率</param>
    /// <param name="InelasticMeanFreePathNm">260401Cl 非弾性平均自由行程 λ_in [nm]。TPP-2M で計算。連続する非弾性散乱間の平均飛行距離</param>
    /// <param name="MeanInelasticLossKev">260401Cl 1 回の非弾性散乱あたりの平均エネルギー損失 &lt;ΔE&gt; = |dE/ds|·λ_in [keV]</param>
    /// <param name="TotalRate">260401Cl 弾性+非弾性の全散乱レート 1/λ_el + 1/λ_in [1/nm]。ホットループ内の除算を事前計算で除去</param>
    /// <param name="ElasticProbability">260401Cl 散乱イベントが弾性である確率 = ElasticRate / TotalRate。ホットループ内の乗算を除去</param>
    /// <param name="NearestNistElasticEnergyIndex">260401Cl NIST 101 エネルギー点上の最近傍インデックス。ホットループ内の Math.Log を事前計算で除去</param>
    private readonly record struct TransportParameters( // (260331Ch) 1 ステップで使う輸送パラメータをまとめて扱う
        double ScreeningParameter, double ElasticCrossSectionNm2, double ElasticMeanFreePathNm, double StoppingPowerKevPerNm, double InelasticMeanFreePathNm, double MeanInelasticLossKev,
        double TotalRate, double ElasticProbability, int NearestNistElasticEnergyIndex); // 260401Cl 追加: ホットループの除算・Math.Log を排除
    #endregion

    /// <summary>コンストラクタ</summary>
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
        double z, double a, double _ρ, double kev, double tilt, double thresholdKev = 2,
        StoppingPowerModels stoppingPowerModel = StoppingPowerModels.JablonskiModified2008,
        ElasticScatteringModels elasticScatteringModel = ElasticScatteringModels.MottNistSampler2023,
        InelasticScatteringModels inelasticScatteringModel = InelasticScatteringModels.DiscreteBulkDiimfpApproximation,
        double? valenceElectronCount = null,
        double? bandGapEv = null,
        IEnumerable<Atoms> atoms = null)
    {
        Z = z; A = a; ρ = _ρ; 
        InitialKev = kev; Tilt = tilt; ThresholdKev = thresholdKev; 
        StoppingPowerModel = stoppingPowerModel;
        ElasticScatteringModel = elasticScatteringModel;
        InelasticScatteringModel = inelasticScatteringModel;
        #region お蔵入り // (260401Ch) generated / external の source flag 比較経路は配布版では使わない
        // ElasticSamplerDataSource = elasticSamplerDataSource ?? DefaultElasticSamplerDataSource;
        #endregion
        ValenceElectronCount = valenceElectronCount is > 0 ? valenceElectronCount.Value : EstimateValenceElectronCount(z); // (260331Ch)
        BandGapEv = bandGapEv is >= 0 ? bandGapEv.Value : 0.0; // (260331Ch)
        ElasticComponents = atoms is null ? [] : BuildElasticSpecies(atoms, ρ); // (260401Ch) 配布版は generated data だけから Mott sampler を構築する
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

    /// <summary>混合物の各元素の原子番号と質量比から、TPP-2M に使う平均価電子数 Nv を質量加重平均で推定する。</summary>
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

    /// <summary>
    /// 結晶の原子リストから Mott 散乱サンプリング用の元素別数密度 n_i [1/nm³] を構築する。
    /// n_i = ρ·N_A·(多重度×占有率)_i / (式量) で計算。
    /// </summary>
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
            #region お蔵入り // (260401Ch) source flag 比較経路
            // species[index++] = new ElasticSpecies(atomicNumber, numberDensityPerNm3, GetNistElasticScatteringTable(atomicNumber, elasticSamplerDataSource));
            #endregion
            species[index++] = new ElasticSpecies(atomicNumber, numberDensityPerNm3, GetNistElasticScatteringTable(atomicNumber)); // (260401Ch) 配布版は generated data のみを使う
        }
        return species;
    }

    /// <summary>
    /// NIST テーブルの 101 エネルギー点ごとに、混合物系の巨視的弾性散乱断面積 Σ_total と元素選択 CDF を事前計算する。
    /// シミュレーション中のイベントごとの再計算を避けるためのキャッシュ。
    /// </summary>
    private MottElasticMixtureEntry[] BuildMottElasticMixtureCache()
    {
        if (ElasticScatteringModel != ElasticScatteringModels.MottNistSampler2023 ||
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

    /// <summary>
    /// 1 eV 刻みで TransportParameters を事前計算し配列に格納する。
    /// シミュレーション中は配列インデックスアクセスだけで α, σ_el, λ_el, dE/ds, λ_in, &lt;ΔE&gt; を取得でき、毎ステップの再計算を回避する。
    /// </summary>
    private TransportParameters[] BuildTransportParameterCache()
    {
        int maxEnergyEv = Math.Max(1, (int)Math.Ceiling(InitialKev * 1000.0));
        var cache = new TransportParameters[maxEnergyEv + 1];
        cache[0] = ComputeTransportParameters(0.001); // (260331Ch) 0 eV は log の都合で扱いづらいので 1 eV 相当を入れる
        for (int energyEv = 1; energyEv < cache.Length; energyEv++)
            cache[energyEv] = ComputeTransportParameters(energyEv * 0.001); // (260331Ch) 1 eV 刻みなら近似誤差は十分小さい
        return cache;
    }

    /// <summary>
    /// DiscreteBulkDiimfpApproximation 用のエネルギー損失分布サンプラーを 10 eV 刻みで事前構築する。
    /// 各エネルギーでプラズモンピーク・低損失・高損失テールの 3 成分混合 PDF → CDF を作成。
    /// </summary>
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

    /// <summary>
    /// 指定エネルギーでの非弾性エネルギー損失分布を構築する。
    /// 低損失成分 (∝ 1/(ω+0.35Ep)^1.6)、プラズモン成分 (ガウシアン @ Ep)、高損失テール (∝ 1/ω^2.2) の
    /// 3 成分を平均損失 &lt;ΔE&gt; を再現するように重み付け混合し、逆関数法用の CDF を返す。
    /// </summary>
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
        // 260401Cl 追加: CDF バイナリサーチを O(1) に高速化するガイドテーブル構築
        const int guideSize = 64;
        var guide = new byte[guideSize];
        int bin = 0;
        for (int k = 0; k < guideSize; k++)
        {
            double v = k / (double)(guideSize - 1);
            while (bin < cumulativeProbabilities.Length - 1 && cumulativeProbabilities[bin] < v)
                bin++;
            guide[k] = (byte)Math.Max(0, bin > 0 ? bin - 1 : 0);
        }
        // return new BulkLossSamplerEntry(minLossEv * 0.001, lossStepEv * 0.001, cumulativeProbabilities); // 260401Cl 旧
        return new BulkLossSamplerEntry(minLossEv * 0.001, lossStepEv * 0.001, cumulativeProbabilities, guide); // 260401Cl GuideTable 追加
    }

    /// <summary>3 つの確率密度関数を重み付き線形結合する。destination[i] = w1·pdf1[i] + w2·pdf2[i] + w3·pdf3[i]</summary>
    private static void CombinePdfs(double[] destination, double[] pdf1, double weight1, double[] pdf2, double weight2, double[] pdf3, double weight3)
    {
        for (int i = 0; i < destination.Length; i++)
            destination[i] = weight1 * pdf1[i] + weight2 * pdf2[i] + weight3 * pdf3[i];
    }

    /// <summary>確率密度関数の配列を合計 1 に正規化する。</summary>
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

    /// <summary>離散化された PDF から期待値 Σ pdf[i]·(minLoss + (i+0.5)·step) を計算する [eV]。</summary>
    private static double MeanLossEv(double[] pdf, double minLossEv, double lossStepEv)
    {
        double sum = 0.0;
        for (int i = 0; i < pdf.Length; i++)
            sum += pdf[i] * (minLossEv + (i + 0.5) * lossStepEv);
        return sum;
    }

    private static double Clamp01(double value)
        => Math.Clamp(value, 0.0, 1.0);

    /// <summary>指定エネルギーでの弾性散乱パラメータ (遮蔽パラメータ α, 断面積 σ [nm²], 平均自由行程 λ [nm], 阻止能 dE/ds [keV/nm]) を返す。</summary>
    public (double ScreeningParameter, double CrossSection, double MeanFreePath, double StoppingPower) GetParameters(double kev)
        => GetParameters(GetTransportParameters(kev)); // (260331Ch)

    private static (double ScreeningParameter, double CrossSection, double MeanFreePath, double StoppingPower) GetParameters(TransportParameters parameters)
        => (parameters.ScreeningParameter, parameters.ElasticCrossSectionNm2, parameters.ElasticMeanFreePathNm, parameters.StoppingPowerKevPerNm); // (260331Ch)

    /// <summary>キャッシュから輸送パラメータを取得する。キャッシュ範囲外のエネルギーでは都度計算にフォールバック。</summary>
    private TransportParameters GetTransportParameters(double kev)
    {
        int energyEv = (int)Math.Round(kev * 1000.0);
        if ((uint)energyEv < (uint)TransportParameterCache.Length)
            return TransportParameterCache[energyEv];
        return ComputeTransportParameters(kev);
    }

    /// <summary>
    /// 指定エネルギーでの全輸送パラメータを計算する。
    /// 弾性散乱: Screened Rutherford (α, σ_el, λ_el) または Mott/NIST (σ_el, λ_el)。
    /// 非弾性散乱: TPP-2M で λ_in を求め、阻止能との整合から平均損失 &lt;ΔE&gt; = |dE/ds|·λ_in を導出。
    /// </summary>
    private TransportParameters ComputeTransportParameters(double kev)
    {
        //電子の質量 (kg) × 電子の速度の2乗 (m^2/s^2)
        var mv2 = UniversalConstants.Convert.EnergyToElectronMass(kev) * UniversalConstants.Convert.EnergyToElectronVelositySquared(kev);
        //散乱係数 / トータル散乱断面積 / 平均自由行程
        double α;
        if (ElasticScatteringModel == ElasticScatteringModels.MottNistSampler2023 &&
            TryGetMottElasticTransport(kev, out double σ_E, out double λ_el))
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
        // 260401Cl 追加: ホットループ内の除算・Math.Log を事前計算で排除
        var elasticRate = λ_el > 0 ? 1.0 / λ_el : 0.0;
        var inelasticRate = λ_in > 0 && meanLossKev > 0 ? 1.0 / λ_in : 0.0;
        var totalRate = elasticRate + inelasticRate;
        var elasticProbability = totalRate > 0 ? elasticRate / totalRate : 1.0;
        var energyEv = kev * 1000.0;
        var nearestNistIndex = energyEv >= 50.0 && energyEv <= 20000.0 ? GetNearestNistElasticEnergyIndex(energyEv) : 0;
        return new TransportParameters(α, σ_E, λ_el, sp, λ_in, meanLossKev, totalRate, elasticProbability, nearestNistIndex);
    }

    /// <summary>
    /// Mott/NIST テーブルから弾性散乱全断面積と平均自由行程を取得する。
    /// 対数エネルギー軸上で隣接 2 点の線形補間を行う。適用範囲は 50 eV〜20 keV。
    /// </summary>
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

    /// <summary>
    /// 弾性散乱角 cosθ をサンプリングする。Mott/NIST テーブルが利用可能ならそちらを使い、
    /// なければ Screened Rutherford の解析式 cosθ = 1 - 2αR/(1+α-R) (R: 一様乱数) でサンプリング。
    /// </summary>
    // private double SampleElasticScatteringCosTheta(double kev, double α) // 260401Cl 旧シグネチャ
    private double SampleElasticScatteringCosTheta(double kev, double α, int nistEnergyIndex) // 260401Cl nistEnergyIndex 追加
    {
        if (ElasticScatteringModel == ElasticScatteringModels.MottNistSampler2023 &&
            TrySampleMottElasticCosTheta(kev, nistEnergyIndex, out var cosTheta)) // 260401Cl
            return cosTheta;

        var rnd = Rnd.NextDouble();
        return 1 - 2 * α * rnd / (1 + α - rnd);
    }

    /// <summary>
    /// NIST SRD 64 の累積角度分布 Φ(cosθ) テーブルから弾性散乱角 cosθ を逆関数法でサンプリングする。
    /// 混合物系では巨視的断面積の比で散乱元素を確率的に選択した後、その元素の Φ テーブルを使う。
    /// </summary>
    // private bool TrySampleMottElasticCosTheta(double kev, out double cosTheta) // 260401Cl 旧シグネチャ
    private bool TrySampleMottElasticCosTheta(double kev, int nistEnergyIndex, out double cosTheta) // 260401Cl nistEnergyIndex 追加
    {
        cosTheta = double.NaN;
        if (ElasticComponents.Length == 0 || MottElasticMixtureCache.Length == 0)
            return false;

        var energyEv = kev * 1000.0;
        if (energyEv < 50.0 || energyEv > 20000.0)
            return false;

        // int energyIndex = GetNearestNistElasticEnergyIndex(energyEv); // 260401Cl 事前計算済み
        int energyIndex = nistEnergyIndex; // 260401Cl
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

        var target = Rnd.NextDouble();
        if (table.GeneratedPchipRuntimeElement is not null)
            return AtomStatic.TryEvaluateGeneratedNistElasticPchipCosTheta(table.GeneratedPchipRuntimeElement, energyIndex, target, out cosTheta); // (260401Ch) generated data の PCHIP 評価は AtomStatic 側へ集約
        #region お蔵入り // (260401Ch) オリジナル TXT の 2001 点 CDF 逆補間は配布版ランタイムでは使わない
        // var phi = table.Phi[energyIndex];
        // if (phi is null)
        //     return false;
        //
        // int upper = Array.BinarySearch(phi, target);
        // if (upper < 0)
        //     upper = ~upper;
        // upper = Math.Clamp(upper, 1, phi.Length - 1);
        // int lower = upper - 1;
        // var phi0 = phi[lower];
        // var phi1 = phi[upper];
        // var x0 = 1.0 - lower * 0.001;
        // var x1 = 1.0 - upper * 0.001;
        // cosTheta = Math.Clamp(phi1 > phi0 ? x0 + (target - phi0) * (x1 - x0) / (phi1 - phi0) : x0, -1.0, 1.0);
        // return true;
        #endregion
        return false; // (260401Ch) generated data が無ければ Mott sampler は失敗として上位で Screened Rutherford にフォールバック
    }

    /// <summary>NIST テーブルの 101 エネルギー点のうち、指定エネルギーに最も近いインデックスを返す (対数軸上で最近傍)。</summary>
    private static int GetNearestNistElasticEnergyIndex(double energyEv)
    {
        var logEnergy = Math.Log(energyEv);
        int lowerIndex = (int)Math.Floor((logEnergy - log50) / LogNistElasticEnergyStep);
        lowerIndex = Math.Clamp(lowerIndex, 0, 99);
        var lowerEnergy = log50 + lowerIndex * LogNistElasticEnergyStep;
        var upperEnergy = lowerEnergy + LogNistElasticEnergyStep;
        return logEnergy - lowerEnergy < upperEnergy - logEnergy ? lowerIndex : lowerIndex + 1;
    }

    /// <summary>NIST テーブルの対数エネルギー軸上で下側インデックスと補間比率 fraction (0〜1) を返す。線形補間に使用。</summary>
    private static int GetLowerNistElasticEnergyIndex(double energyEv, out double fraction)
    {
        var logEnergy = Math.Log(energyEv);
        int lowerIndex = (int)Math.Floor((logEnergy - log50) / LogNistElasticEnergyStep);
        lowerIndex = Math.Clamp(lowerIndex, 0, 99);
        var lowerEnergy = log50 + lowerIndex * LogNistElasticEnergyStep;
        var upperEnergy = lowerEnergy + LogNistElasticEnergyStep;
        fraction = (logEnergy - lowerEnergy) / (upperEnergy - lowerEnergy);
        return lowerIndex;
    }

    /// <summary>
    /// 指定原子番号の NIST 弾性散乱テーブルを取得する。
    /// 配布版では generated PCHIP データのみをロードし、static cache に保存する。
    /// </summary>
    private static NistElasticScatteringTable GetNistElasticScatteringTable(int atomicNumber)
    {
        lock (lockObj)
        {
            if (NistElasticSamplerCache.TryGetValue(atomicNumber, out var cached))
                return cached;
        }

        var table = TryLoadGeneratedNistElasticPchipTable(atomicNumber); // (260401Ch) standalone 配布では generated data のみを使う

        lock (lockObj)
            NistElasticSamplerCache[atomicNumber] = table;
        return table;
    }

    #region お蔵入り // (260401Ch) 配布版では外部 TXT 経路を使わない
    // // 260401Cl TryLoadNistElasticBinaryTable メソッドを除去 (BIN 形式読み取りを廃止)
    //
    // /// <summary>NIST SRD 64 オリジナルのテキスト形式 (*.TXT) から弾性散乱テーブルをロードする。PCHIP が利用不可な場合のフォールバック。</summary>
    // private static NistElasticScatteringTable TryLoadNistElasticTextTable(string path)
    // {
    //     if (!File.Exists(path))
    //         return null;
    //
    //     try
    //     {
    //         using var reader = new StreamReader(path);
    //         var table = new NistElasticScatteringTable();
    //         for (int i = 0; i < NistElasticEnergyCount; i++)
    //         {
    //             _ = reader.ReadLine(); // (260331Ch) NIST sampler 内のブロック番号。Fortran 版でも未使用
    //             table.SigmaA0Squared[i] = double.Parse(reader.ReadLine() ?? throw new InvalidDataException(path), CultureInfo.InvariantCulture);
    //             var phi = new double[NistElasticPhiCount];
    //             for (int j = 0; j < phi.Length; j++)
    //                 phi[j] = double.Parse(reader.ReadLine() ?? throw new InvalidDataException(path), CultureInfo.InvariantCulture);
    //             table.Phi[i] = phi;
    //         }
    //         return table;
    //     }
    //     catch
    //     {
    //         return null;
    //     }
    // }
    #endregion

    /// <summary>コード生成済みの PCHIP 補間データから弾性散乱テーブルを構築する。ファイル I/O 不要で最も高速。</summary>
    private static NistElasticScatteringTable TryLoadGeneratedNistElasticPchipTable(int atomicNumber)
    {
        if (!AtomStatic.TryGetGeneratedNistElasticPchipRuntimeElement(atomicNumber, out var runtimeElement) || runtimeElement is null)
            return null;

        if (runtimeElement.SigmaA0Squared is null || runtimeElement.SigmaA0Squared.Length != NistElasticEnergyCount)
            return null;

        var table = new NistElasticScatteringTable()
        {
            AtomicNumber = atomicNumber,
            GeneratedPchipRuntimeElement = runtimeElement,
        };
        Array.Copy(runtimeElement.SigmaA0Squared, table.SigmaA0Squared, NistElasticEnergyCount);
        return table;
    }

    /// <summary>
    /// 選択された阻止能モデルに基づき dE/ds [keV/nm] (負値) を返す。
    /// JoyLuo1989: 修正 Bethe 式。JablonskiModified2008: TPP-2M の IMFP を用いた経験式。
    /// </summary>
    private double GetStoppingPower(double kev, double mv2)
    {
        if (StoppingPowerModel == StoppingPowerModels.JoyLuo1989)
            return coeff3 / mv2 * Math.Log(1.166 * k + 0.583 / UniversalConstants.eV_joule / J * mv2);

        var energyEv = kev * 1000.0;
        var λ_in = GetInelasticMeanFreePathAngstrom(energyEv);
        if (!(λ_in > 0) || double.IsNaN(λ_in) || double.IsInfinity(λ_in))
            return coeff3 / mv2 * Math.Log(1.166 * k + 0.583 / UniversalConstants.eV_joule / J * mv2); // (260331Ch) TPP-2M が破綻したら Joy-Luo に戻す

        var spEvPerAngstrom = StoppingPowerModel switch
        {
            StoppingPowerModels.JablonskiModified2008
                => Jablonski2008D1 * Math.Pow(energyEv, Jablonski2008D2) * Math.Log(Jablonski2008D3 * energyEv) * Math.Pow(ρ + Jablonski2008D4, Jablonski2008D5) / λ_in,
            _ => throw new ArgumentOutOfRangeException(),
        };

        return -spEvPerAngstrom * 0.01; // (260331Ch) 1 eV/Å = 0.01 keV/nm
    }

    /// <summary>
    /// TPP-2M (Tanuma-Powell-Penn) 式で非弾性平均自由行程 λ_in [Å] を計算する。
    /// λ_in(E) = E / {Ep²·[β·ln(γE) - C/E + D/E²]}。Ep はプラズマエネルギー。
    /// </summary>
    private double GetInelasticMeanFreePathAngstrom(double energyEv)
    {
        var gammaE = TppGamma * energyEv;
        if (!(gammaE > 1.0))
            return double.NaN;

        var denominator = TppPlasmaEnergyEv * TppPlasmaEnergyEv * (TppBeta * Math.Log(gammaE) - TppC / energyEv + TppD / (energyEv * energyEv));
        return denominator > 0 ? energyEv / denominator : double.NaN;
    }

    /// <summary>
    /// 平均原子番号 (非整数可) から TPP-2M 用の価電子数 Nv を推定する。
    /// 既知元素表にあればその値を返し、なければ隣接 2 元素の線形補間。
    /// </summary>
    private static double EstimateValenceElectronCount(double atomicNumber)
    {
        if (AtomStatic.ElementInelasticParameters.TryGetValue((int)Math.Round(atomicNumber), out var exact))
            return exact.ValenceElectrons;

        int lower = (int)Math.Floor(atomicNumber), upper = (int)Math.Ceiling(atomicNumber);
        if (lower == upper)
            return EstimateElementValenceElectronCount(lower);

        var lowerValue = EstimateElementValenceElectronCount(lower);
        var upperValue = EstimateElementValenceElectronCount(upper);
        return lowerValue + (atomicNumber - lower) * (upperValue - lowerValue);
    }

    /// <summary>整数原子番号から価電子数を推定する。既知元素表→周期律に基づく簡易推定の順で決定。</summary>
    private static double EstimateElementValenceElectronCount(int atomicNumber)
    {
        if (AtomStatic.ElementInelasticParameters.TryGetValue(atomicNumber, out var exact))
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

    /// <summary>周期表の族位置から価電子数を簡易推定する。previousNobleGas は直前の希ガスの原子番号。</summary>
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
    /// 選択された非弾性散乱モデルに応じて 1 回の非弾性散乱でのエネルギー損失 ΔE [keV] をサンプリングする。
    /// DiscreteMeanLoss: 常に平均値。DiscreteBulkDiimfpApproximation: bulk DIIMFP 近似分布。
    /// </summary>
    private double SampleInelasticLossKev(double currentKev, double meanLossKev)
    {
        if (!(meanLossKev > 0))
            return 0.0;

        var lossKev = InelasticScatteringModel switch
        {
            InelasticScatteringModels.DiscreteMeanLoss => meanLossKev,
            // 260401Cl DiscreteExponentialLoss の分岐を除去
            InelasticScatteringModels.DiscreteBulkDiimfpApproximation => SampleBulkLossKev(currentKev, meanLossKev),
            _ => 0.0,
        };
        return Math.Min(lossKev, currentKev);
    }

    /// <summary>
    /// BulkLossSamplerCache の CDF テーブルから逆関数法でエネルギー損失をサンプリングする。
    /// 隣接ビン間の線形補間で連続的な損失値を生成。キャッシュ範囲外では平均損失にフォールバック。
    /// </summary>
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
        // int upper = Array.BinarySearch(entry.CumulativeProbabilities, target); // 260401Cl 旧: O(log 256) バイナリサーチ
        // 260401Cl ガイドテーブルで O(1) に高速化
        int upper;
        var guide = entry.GuideTable;
        if (guide is not null)
        {
            upper = guide[Math.Min((int)(target * (guide.Length - 1)), guide.Length - 1)];
            while (upper < entry.CumulativeProbabilities.Length - 1 && entry.CumulativeProbabilities[upper] < target)
                upper++;
        }
        else
        {
            upper = Array.BinarySearch(entry.CumulativeProbabilities, target);
            if (upper < 0) upper = ~upper;
        }
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
        if (InelasticScatteringModel != InelasticScatteringModels.ContinuousSlowingDownApproximation)
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
                double cosθ = SampleElasticScatteringCosTheta(trajectory[^1].e, α, GetNearestNistElasticEnergyIndex(trajectory[^1].e * 1000.0)), sinθ = Math.Sqrt(1 - cosθ * cosθ); // 260401Cl nistEnergyIndex 追加 (CSDA パスは性能非優先)
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
    /// 離散非弾性散乱モデルでの飛程計算。弾性・非弾性を独立なポアソン過程として扱い、
    /// 次イベントまでの距離を s = -ln(R)/Σ_total でサンプリングし、弾性/非弾性を確率的に分岐する。
    /// </summary>
    private List<(V3 p, double e)> GetTrajectoriesDiscreteInelastic()
    {
        var trajectory = new List<(V3 p, double e)>(100) { (new V3(0, 0, 0), InitialKev) };
        double m11, m12, m13, m22, m23;
        double vX = 0, vY = 0, vZ = -1;
        int n = 0;

        while (trajectory[^1].e > ThresholdKev && trajectory[^1].p.Y * tan >= trajectory[^1].p.Z)
        {
            var parameters = GetTransportParameters(trajectory[^1].e);
            // if (!(parameters.ElasticMeanFreePathNm > 0)) break; // 260401Cl TotalRate に統合
            // var elasticRate = 1.0 / parameters.ElasticMeanFreePathNm; // 260401Cl 事前計算済み
            // var inelasticRate = parameters.InelasticMeanFreePathNm > 0 && parameters.MeanInelasticLossKev > 0 // 260401Cl
            //     ? 1.0 / parameters.InelasticMeanFreePathNm : 0.0;
            // var totalRate = elasticRate + inelasticRate; // 260401Cl 事前計算済み
            if (!(parameters.TotalRate > 0)) // 260401Cl
                break;

            var s = -Math.Log(Math.Max(Rnd.NextDouble(), double.Epsilon)) / parameters.TotalRate; // 260401Cl
            var nextPoint = trajectory[^1].p + s * new V3(vX, vY, vZ);
            if (nextPoint.Y * tan < nextPoint.Z)
            {
                trajectory.Add((nextPoint, trajectory[^1].e)); // (260331Ch) 表面を抜けた時点では試料内の次イベントは未発生
                break;
            }

            bool isElastic = parameters.ElasticProbability >= 1.0 || Rnd.NextDouble() < parameters.ElasticProbability; // 260401Cl 事前計算済み
            double nextEnergy = trajectory[^1].e;
            if (isElastic)
            {
                double rnd3 = Rnd.NextDouble();
                double cosθ = SampleElasticScatteringCosTheta(trajectory[^1].e, parameters.ScreeningParameter, parameters.NearestNistElasticEnergyIndex), sinθ = Math.Sqrt(1 - cosθ * cosθ); // 260401Cl nistEnergyIndex 追加
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

    /// <summary>
    /// 後方散乱電子の詳細情報を返す。軌跡座標は保持せず、脱出深さ・方向・エネルギーと
    /// 最後の非弾性散乱の情報のみを記録する軽量版。大量の電子統計を取る EBSD シミュレーション向け。
    /// </summary>
    public BackscatteredElectronDetail GetBackscatteredElectronDetail()
    {
        if (InelasticScatteringModel != InelasticScatteringModels.ContinuousSlowingDownApproximation)
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
                double cosθ = SampleElasticScatteringCosTheta(e, α, GetNearestNistElasticEnergyIndex(e * 1000.0)), sinθ = Math.Sqrt(1 - cosθ * cosθ); // 260401Cl nistEnergyIndex 追加 (CSDA パスは性能非優先)
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

    /// <summary>
    /// 離散非弾性散乱モデルでの後方散乱電子詳細計算。弾性/非弾性をポアソン過程で分岐し、
    /// 非弾性イベント発生時に深さ・エネルギー・方向を記録して「最後の非弾性散乱」情報を更新する。
    /// </summary>
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
            // if (!(parameters.ElasticMeanFreePathNm > 0)) break; // 260401Cl TotalRate に統合
            // var elasticRate = 1.0 / parameters.ElasticMeanFreePathNm; // 260401Cl 事前計算済み
            // var inelasticRate = parameters.InelasticMeanFreePathNm > 0 && parameters.MeanInelasticLossKev > 0 // 260401Cl
            //     ? 1.0 / parameters.InelasticMeanFreePathNm : 0.0;
            // var totalRate = elasticRate + inelasticRate; // 260401Cl 事前計算済み
            if (!(parameters.TotalRate > 0)) // 260401Cl
                break;

            var s = -Math.Log(Math.Max(Rnd.NextDouble(), double.Epsilon)) / parameters.TotalRate; // 260401Cl
            var dtmp = d + s * (sin * vY - cos * vZ); // (260331Ch) 現在の進行方向で次イベント位置まで進む
            if (dtmp < 0)
                break;
            d = dtmp;

            bool isElastic = parameters.ElasticProbability >= 1.0 || Rnd.NextDouble() < parameters.ElasticProbability; // 260401Cl 事前計算済み
            if (isElastic)
            {
                double rnd3 = Rnd.NextDouble();
                double cosθ = SampleElasticScatteringCosTheta(e, parameters.ScreeningParameter, parameters.NearestNistElasticEnergyIndex), sinθ = Math.Sqrt(1 - cosθ * cosθ); // 260401Cl nistEnergyIndex 追加
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

}
