#region using
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using Crystallography.OpenGL;
using V3 = OpenTK.Mathematics.Vector3d;
using V4 = OpenTK.Mathematics.Vector4d;
using M3 = OpenTK.Mathematics.Matrix3d;
using M4 = OpenTK.Mathematics.Matrix4d;
using C4 = OpenTK.Mathematics.Color4;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using MathNet.Numerics;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Scripting.Utils;
using ZLinq;
#endregion

namespace ReciPro;

public partial class FormEBSD : CaptureFormBase
{
    private enum MonteCarloDistributionDepthMode
    {
        LastInelasticEventDepth,
        LastTransportEventDepth,
    }

    #region お蔵入り // (260401Ch) generated / external MC 比較ベンチは standalone 配布版では使わない
    /*
    private readonly record struct MonteCarloBenchmarkElectron(double Depth, double Energy, double TotalEnergyLoss, bool HasLastInelasticEvent, double LastInelasticDepth, V3 Direction); // (260401Ch) MC source benchmark 用の最小電子情報

    private sealed class ElasticSamplerBenchmarkRunResult // (260401Ch) source ごとの benchmark 結果
    {
        public string SourceName { get; init; } = "";
        public MonteCarlo.ElasticSamplerDataSources Source { get; init; }
        public int LoopCount { get; init; }
        public double ElapsedMilliseconds { get; init; }
        public double CrossSectionNm2 { get; init; }
        public double MeanFreePathNm { get; init; }
        public double StoppingPowerKevPerNm { get; init; }
        public MonteCarloBenchmarkElectron[] Electrons { get; init; } = [];
    }
    */
    #endregion

    #region フィールド、プロパティ
    public FormMain FormMain;
    public GLControlAlpha glControlGeo;
    public GLControlAlpha glControlMasterPattern3D; // (260321Ch) Rosca-Lambert 球面 preview 用の OpenGL コントロール
    private GLControlAlpha glControlMasterPattern3DAxes; // (260322Ch) MasterPattern3D と同期する結晶軸 inset
    private readonly Stopwatch sw1 = new(), sw2 = new();
    private const int BackscatterMonteCarloLoopCount = 2_500_000; // 260329Cl 変更: 500万→250万に削減（パラメトリックフィッティングには十分な統計量）
    private readonly Timer timer = new();
    #region お蔵入り // (260401Ch) generated / external MC 比較ベンチは standalone 配布版では使わない
    /*
    // private const int ElasticSamplerBenchmarkLoopCount = 250_000; // (260401Ch) generated / external の MC 比較は 25 万本で軽めに検証する
    private const int ElasticSamplerBenchmarkLoopCount = 1_000_000; // (260401Ch) MC source benchmark の統計ばらつきをさらに下げるため 100 万本に増やす
    private Button buttonBenchmarkNistElasticSampler = null; // (260401Ch) generated / external の MC ベンチ用ボタン
    */
    #endregion

    private readonly EBSD masterPatternEbsd = new(); // (260321Ch) MasterPattern build の実行ロジックは Crystallography.EBSD 側へ移す
    private MasterPattern MasterPattern => masterPatternEbsd.MasterPattern; // (260321Ch)
    private PseudoBitmap masterPattern2DBitmap = null; // (260322Ch) ScalablePictureBoxAdvanced 2D に渡す MasterPattern2D 画像
    private double[] masterPattern2DValues = []; // (260322Ch) 旧名: masterPattern2DPreviewValues。MasterPattern2D に現在表示している強度配列を保持する
    private double[] masterPattern3DValuesPositive = []; // (260322Ch) 旧名: masterPattern3DPreviewValuesPositive。MasterPattern3D 用の +Z 半球強度を保持する
    private double[] masterPattern3DValuesNegative = []; // (260322Ch) 旧名: masterPattern3DPreviewValuesNegative。MasterPattern3D 用の -Z 半球強度を保持する
    private int masterPattern3DCacheGridSize = 0; // (260322Ch) 旧名: masterPattern3DPreviewGridSize。0 は有効な MasterPattern3D キャッシュが未作成であることを示す
    private MasterPattern.Types masterPattern3DCacheGridType = MasterPattern.Types.Square; // 260331Cl
    private long masterPatternMonteCarloElapsedMilliseconds = 0; // (260327Ch) MasterPattern build 前段の MC + fitting の経過時間

    private EbsdMonteCarloDistribution mcDistribution = null; // 260325Cl 追加: MC フィッティング結果
    private MonteCarloDistributionDepthMode monteCarloDistributionDepthMode = MonteCarloDistributionDepthMode.LastInelasticEventDepth; // (260331Ch) MasterPattern 重み付けに使う z は既定で last inelastic depth

    /// <summary>飛程計算の際の打ち切りエネルギー (kev)</summary>
    private double EnergyThreshold = 2;
    public double WaveLength { get => waveLengthControl.WaveLength; set => waveLengthControl.WaveLength = value; }

    public double DetTilt => numericBoxDetTilt.RadianValue;
    public double DetR => numericBoxDetRadius.Value;
    public double DetY => numericBoxYofDet.Value;
    public double DetZ => numericBoxZofDet.Value;

    public double SmpTilt => numericBoxSampleTilt.RadianValue;

    (double Depth, V3 Vec, PointD Position, double Energy, double TotalEnergyLoss, bool HasLastInelasticEvent, double LastInelasticDepth, double LastInelasticEnergyBeforeLoss, double LastInelasticEnergyAfterLoss, V3 LastInelasticDirection)[] BSEs = []; // (260331Ch) 最後の非弾性散乱情報も保持する

    public Crystal Crystal => FormMain.Crystal;

    /// <summary>試料から検出器までの距離</summary>
    public double CameraLength2 => Math.Abs(DetY * Math.Sin(DetTilt) - DetZ * Math.Cos(DetTilt));

    /// <summary>画像の中心。検出器(Detector)座標系(Foot原点)で表現</summary>
    public PointD Foot
    {
        get
        {
            //垂線の足の実空間座標座標
            var (sinDetTilt, cosDetTilt) = Math.SinCos(DetTilt);
            var f = new V3(-CameraLength2 * sinDetTilt, CameraLength2 * cosDetTilt, 1);
            //検出器の中心座標
            var c = new V3(DetY, DetZ, 1);

            var len = (f - c).Length;
            var (sin, cos) = Math.SinCos(-DetTilt);// double cos = Math.Cos(-DetTilt), sin = Math.Sin(-DetTilt);

            var rot = new M3(cos, -sin, DetY - DetY * cos + DetZ * sin,
                             sin, cos, DetZ - DetY * sin - DetZ * cos,
                             0, 0, 1);

            return (rot * f).X > c.X ? new PointD(0, len) : new PointD(0, -len);
        }
    }

    /// <summary>画面解像度 mm/pix</summary>
    public double Resolution => 2.0 * numericBoxDetRadius.Value / graphicsBox.ClientRectangle.Width;
    public float ResolutionF => (float)Resolution;

    public int MaxNumOfBloch => numericBoxMaxNumOfG.ValueInteger;
    private double Voltage => waveLengthControl.Energy;
    public double[] ThicknessArray
    {
        get
        {
            var thicknessArray = new List<double>();
            for (double thickness = numericBoxThicknessStart.Value; thickness <= numericBoxThicknessEnd.Value; thickness += numericBoxThicknessStep.Value)
                thicknessArray.Add(thickness);
            return [.. thicknessArray];
        }
    }

    private PseudoBitmap Pbmp = null;

    private double[] EnergyArray
    {
        get
        {
            var energyArray = new List<double>();
            for (double energy = numericBoxEnergyStart.Value; energy >= numericBoxEnergyEnd.Value - 0.0000001; energy -= numericBoxEnergyStep.Value)
                energyArray.Add(energy);
            return [.. energyArray];
        }
    }

    public int DetectorDivision = 5;


    #endregion

    #region コンストラクタ、ロード、クローズ
    public FormEBSD()
    {
        InitializeComponent();

        // InitializeMasterPatternPreviewControls(); // (260321Ch) 旧案: preview UI をコード側で組み立てていた
        buttonStop.Click += buttonStop_Click; // (260327Ch) 既存の Stop ボタンは MasterPattern build 停止に使う
        // InitializeNistElasticSamplerDeveloperTool(); // (260401Ch) 旧実装: PCHIP 圧縮ツールはコード側で動的追加していた
        UpdateEbsdTiltCoeffs(); // 260325Cl: tilt 係数を初期値で計算
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        graphicsBox.Refresh();
    }

    private async void buttonFitNistElasticSampler_Click(object sender, EventArgs e)
    {
        var repositoryRoot = NistElasticSamplerPchipGenerator.TryFindRepositoryRoot(AppContext.BaseDirectory)
            ?? NistElasticSamplerPchipGenerator.TryFindRepositoryRoot(Environment.CurrentDirectory);
        if (repositoryRoot == null)
        {
            MessageBox.Show(this, "ReciPro.sln が見つからず、generated source の出力先を特定できませんでした。", "NIST elastic sampler compression", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // var initialDirectory = Path.Combine(repositoryRoot, "Crystallography", "Atom", "NistElasticSampler_Original"); // (260401Ch) 旧配置
        var initialDirectory = NistElasticSamplerPchipGenerator.GetOriginalDirectory(repositoryRoot); // (260401Ch) 圧縮元の既定フォルダ解決も Crystallography 側へ寄せる
        using var openFileDialog = new OpenFileDialog()
        {
            Multiselect = true,
            Filter = "NIST elastic sampler (E_*.TXT)|E_*.TXT|Text files (*.txt)|*.txt",
            Title = "Select NIST elastic sampler files",
            InitialDirectory = Directory.Exists(initialDirectory) ? initialDirectory : repositoryRoot,
            RestoreDirectory = true,
            CheckFileExists = true,
        };
        if (openFileDialog.ShowDialog(this) != DialogResult.OK || openFileDialog.FileNames.Length == 0)
            return;

        buttonFitNistElasticSampler.Enabled = false;
        toolStripProgressBar.Value = 0;
        toolStripStatusLabel2.Text = "NIST elastic compression";
        toolStripStatusLabel1.Text = "0% completed, elapsed 0.00 s.";
        toolStripStatusLabel3.Text = "";

        var stopwatch = Stopwatch.StartNew();
        var progress = new Progress<NistElasticCompressionProgress>(state =>
        {
            var progressValue = Math.Clamp((int)Math.Round(100.0 * state.OverallProgress), 0, 100);
            toolStripProgressBar.Value = progressValue;
            toolStripStatusLabel2.Text = $"NIST elastic compression: Z={state.AtomicNumber}, block {state.BlockIndex}/{state.BlockCount}, {state.Phase}";
            toolStripStatusLabel1.Text = $"{progressValue}% completed, elapsed {stopwatch.Elapsed.TotalSeconds:f2} s.";
            toolStripStatusLabel3.Text = $"File {state.FileIndex}/{state.FileCount}: {Path.GetFileName(state.SourcePath)}";
        });

        try
        {
            var compressionResult = await Task.Run(() => NistElasticSamplerPchipGenerator.GenerateCompressedSourcesToRepository(openFileDialog.FileNames, repositoryRoot, progress)); // (260401Ch) 実処理は Crystallography.Atom.NistElastic 側の API を呼ぶ
            stopwatch.Stop();
            toolStripProgressBar.Value = 100;
            toolStripStatusLabel2.Text = "NIST elastic compression completed";
            toolStripStatusLabel1.Text = $"100% completed, elapsed {stopwatch.Elapsed.TotalSeconds:f2} s.";
            toolStripStatusLabel3.Text = $"{compressionResult.SourceFileCount} file(s) processed, {compressionResult.OutputPaths.Count} output file(s).";
            MessageBox.Show(this,
                $"Finished compressing {compressionResult.SourceFileCount} file(s).\r\n" +
                // $"Generated source: {Path.Combine(repositoryRoot, "Crystallography", "Atom", "Generated")}\r\n" +
                $"Generated source: {compressionResult.GeneratedDirectory}\r\n" +
                // $"Generated CSV: {Path.Combine(repositoryRoot, "Crystallography", "Atom", "GeneratedDiagnostics")}",
                $"Generated CSV: {compressionResult.DiagnosticsDirectory}",
                "NIST elastic sampler compression",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            toolStripStatusLabel2.Text = "NIST elastic compression failed";
            toolStripStatusLabel1.Text = $"Failed after {stopwatch.Elapsed.TotalSeconds:f2} s.";
            toolStripStatusLabel3.Text = ex.Message;
            MessageBox.Show(this, ex.ToString(), "NIST elastic sampler compression", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            buttonFitNistElasticSampler.Enabled = true;
        }
    }

    #region お蔵入り // (260401Ch) generated / external MC 比較ベンチは standalone 配布版では使わない
    /*
    private async void buttonBenchmarkNistElasticSampler_Click(object sender, EventArgs e)
    {
        var repositoryRoot = NistElasticSamplerPchipGenerator.TryFindRepositoryRoot(AppContext.BaseDirectory)
            ?? NistElasticSamplerPchipGenerator.TryFindRepositoryRoot(Environment.CurrentDirectory);
        if (repositoryRoot == null)
        {
            MessageBox.Show(this, "ReciPro.sln が見つからず、benchmark の出力先を特定できませんでした。", "NIST elastic MC benchmark", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // var originalTextDirectory = Path.Combine(repositoryRoot, "ReciPro", "NistElasticSampler_Original"); // (260401Ch) 旧配置
        var originalTextDirectory = Path.Combine(repositoryRoot, "Crystallography", "Atom", "NistElastic", "Original");
        if (!Directory.Exists(originalTextDirectory))
        {
            MessageBox.Show(this, $"Original TXT folder was not found:\r\n{originalTextDirectory}", "NIST elastic MC benchmark", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var outputDirectory = Path.Combine(repositoryRoot, "ReciPro", "MonteCarloBenchmark");
        Directory.CreateDirectory(outputDirectory);

        var cry = FormMain.Crystal;
        cry.GetFormulaAndDensity();
        var sum1 = cry.Atoms.Sum(a => AtomStatic.AtomicWeight(a.AtomicNumber) * a.Multiplicity * a.AtomicNumber);
        var sum2 = cry.Atoms.Sum(a => AtomStatic.AtomicWeight(a.AtomicNumber) * a.Multiplicity);
        var sum3 = cry.Atoms.Sum(a => a.Multiplicity);
        var z = sum1 / sum2;
        var a = sum2 / sum3;
        var rho = cry.Density;
        var energy = Voltage;
        var sampleTilt = SmpTilt;
        var energyThreshold = EnergyThreshold;
        var sampleRotation = M3.CreateRotationX(sampleTilt);
        var loop = Math.Min(ElasticSamplerBenchmarkLoopCount, BackscatterMonteCarloLoopCount);
        var atoms = cry.Atoms.ToArray();
        var valenceElectronCount = MonteCarlo.EstimateAverageValenceElectronCount(
            atoms.Select(atom => (atom.AtomicNumber, AtomStatic.AtomicWeight(atom.AtomicNumber) * atom.Multiplicity))); // (260401Ch)
        var fileStem = $"NistElasticSamplerBenchmark_{DateTime.Now:yyyyMMdd_HHmmss}_E{energy.ToString("0.0", CultureInfo.InvariantCulture)}keV"; // (260401Ch)

        buttonFitNistElasticSampler.Enabled = false;
        buttonBenchmarkNistElasticSampler.Enabled = false;
        toolStripProgressBar.Value = 0;
        toolStripStatusLabel2.Text = "NIST elastic MC benchmark";
        toolStripStatusLabel1.Text = "0% completed, elapsed 0.00 s.";
        toolStripStatusLabel3.Text = "";

        var originalSamplerTextDirectory = MonteCarlo.NistElasticSamplerTextDirectory;
        MonteCarlo.NistElasticSamplerTextDirectory = originalTextDirectory; // (260401Ch) benchmark 中は source tree の original TXT を使う
        var stopwatch = Stopwatch.StartNew();

        try
        {
            var progress = new Progress<(int Progress, string Message, string Detail)>(state =>
            {
                toolStripProgressBar.Value = Math.Clamp(state.Progress, 0, 100);
                toolStripStatusLabel2.Text = $"NIST elastic MC benchmark: {state.Message}";
                toolStripStatusLabel1.Text = $"{toolStripProgressBar.Value}% completed, elapsed {stopwatch.Elapsed.TotalSeconds:f2} s.";
                toolStripStatusLabel3.Text = state.Detail;
            });

            var runs = await Task.Run(() => RunElasticSamplerSourceBenchmark(
                z, a, rho, energy, sampleTilt, energyThreshold, valenceElectronCount, atoms, loop, sampleRotation, progress));

            var summaryPath = WriteElasticSamplerBenchmarkSummaryCsv(outputDirectory, fileStem, runs);
            var depthPath = WriteElasticSamplerBenchmarkHistogramCsv(outputDirectory, fileStem, "depth", "DepthNm", runs, static electron => electron.Depth, 80);
            var energyPath = WriteElasticSamplerBenchmarkHistogramCsv(outputDirectory, fileStem, "energy", "ExitEnergyKev", runs, static electron => electron.Energy, 80);

            stopwatch.Stop();
            toolStripProgressBar.Value = 100;
            toolStripStatusLabel2.Text = "NIST elastic MC benchmark completed";
            toolStripStatusLabel1.Text = $"100% completed, elapsed {stopwatch.Elapsed.TotalSeconds:f2} s.";
            toolStripStatusLabel3.Text = Path.GetFileName(summaryPath);
            MessageBox.Show(this,
                $"Benchmark finished.\r\n" +
                $"Summary: {summaryPath}\r\n" +
                $"Depth histogram: {depthPath}\r\n" +
                $"Energy histogram: {energyPath}",
                "NIST elastic MC benchmark",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            toolStripStatusLabel2.Text = "NIST elastic MC benchmark failed";
            toolStripStatusLabel1.Text = $"Failed after {stopwatch.Elapsed.TotalSeconds:f2} s.";
            toolStripStatusLabel3.Text = ex.Message;
            MessageBox.Show(this, ex.ToString(), "NIST elastic MC benchmark", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            MonteCarlo.NistElasticSamplerTextDirectory = originalSamplerTextDirectory;
            buttonFitNistElasticSampler.Enabled = true;
            buttonBenchmarkNistElasticSampler.Enabled = true;
        }
    }

    private ElasticSamplerBenchmarkRunResult[] RunElasticSamplerSourceBenchmark(
        double z, double a, double rho, double energy, double sampleTilt, double energyThreshold, double valenceElectronCount,
        Atoms[] atoms, int loop, M3 sampleRotation, IProgress<(int Progress, string Message, string Detail)> progress = null)
    {
        var sourceConfigs = new (string Name, MonteCarlo.ElasticSamplerDataSources Source)[]
        {
            ("GeneratedOnly", MonteCarlo.ElasticSamplerDataSources.GeneratedOnly),
            ("ExternalTextOnly", MonteCarlo.ElasticSamplerDataSources.ExternalTextOnly),
        };

        var results = new ElasticSamplerBenchmarkRunResult[sourceConfigs.Length];
        for (int sourceIndex = 0; sourceIndex < sourceConfigs.Length; sourceIndex++)
        {
            var sourceConfig = sourceConfigs[sourceIndex];
            progress?.Report((sourceIndex * 50, sourceConfig.Name, "Preparing MonteCarlo"));
            var monte = new MonteCarlo(z, a, rho, energy, sampleTilt, energyThreshold,
                elasticScatteringModel: MonteCarlo.ElasticScatteringModels.MottNistSampler2023,
                inelasticScatteringModel: MonteCarlo.InelasticScatteringModels.DiscreteBulkDiimfpApproximation,
                valenceElectronCount: valenceElectronCount,
                elasticSamplerDataSource: sourceConfig.Source,
                atoms: atoms); // (260401Ch)

            var (_, crossSectionNm2, meanFreePathNm, stoppingPowerKevPerNm) = monte.GetParameters(energy);
            var stopwatch = Stopwatch.StartNew();
            var electrons = RunBackscatterMonteCarlo(monte, loop, energyThreshold, sampleRotation, (completed, total) =>
            {
                var localProgress = total > 0 ? completed / (double)total : 0.0;
                var progressValue = sourceIndex * 50 + (int)Math.Round(50.0 * localProgress);
                progress?.Report((progressValue, sourceConfig.Name, $"{completed:#,0} / {total:#,0} electrons"));
            })
            .Select(e => new MonteCarloBenchmarkElectron(e.Depth, e.Energy, e.TotalEnergyLoss, e.HasLastInelasticEvent, e.LastInelasticDepth, e.Vec))
            .ToArray(); // (260401Ch) benchmark は depth / energy / angle の比較に必要な列だけ保持する
            stopwatch.Stop();

            results[sourceIndex] = new ElasticSamplerBenchmarkRunResult()
            {
                SourceName = sourceConfig.Name,
                Source = sourceConfig.Source,
                LoopCount = loop,
                ElapsedMilliseconds = stopwatch.Elapsed.TotalMilliseconds,
                CrossSectionNm2 = crossSectionNm2,
                MeanFreePathNm = meanFreePathNm,
                StoppingPowerKevPerNm = stoppingPowerKevPerNm,
                Electrons = electrons,
            };
        }

        progress?.Report((100, "Completed", $"{results.Sum(result => result.Electrons.Length):#,0} accepted electrons"));
        return results;
    }

    private static string WriteElasticSamplerBenchmarkSummaryCsv(string outputDirectory, string fileStem, ElasticSamplerBenchmarkRunResult[] runs)
    {
        var path = Path.Combine(outputDirectory, $"{fileStem}.summary.csv");
        var builder = new StringBuilder();
        builder.AppendLine("Source,LoopCount,AcceptedCount,BseYieldPercent,ElapsedMilliseconds,CrossSectionNm2,MeanFreePathNm,StoppingPowerEvPerNm,MeanExitEnergyKev,MeanTotalEnergyLossKev,MeanDepthNm,DepthP50Nm,DepthP90Nm,DepthP99Nm,MeanLastInelasticDepthNm,MeanExitPolarAngleDeg");
        foreach (var run in runs)
        {
            var acceptedCount = run.Electrons.Length;
            var bseYieldPercent = run.LoopCount > 0 ? 100.0 * acceptedCount / run.LoopCount : 0.0;
            var depths = run.Electrons.Select(e => e.Depth).OrderBy(static value => value).ToArray();
            var lastInelasticDepths = run.Electrons.Where(static e => e.HasLastInelasticEvent).Select(static e => e.LastInelasticDepth).ToArray();
            var meanPolarAngleDeg = acceptedCount > 0
                ? run.Electrons.Average(static electron => Math.Acos(Math.Clamp(electron.Direction.Z, -1.0, 1.0)) * 180.0 / Math.PI)
                : 0.0;

            builder.AppendLine(string.Join(",",
                run.SourceName,
                run.LoopCount.ToString(CultureInfo.InvariantCulture),
                acceptedCount.ToString(CultureInfo.InvariantCulture),
                bseYieldPercent.ToString("G17", CultureInfo.InvariantCulture),
                run.ElapsedMilliseconds.ToString("F3", CultureInfo.InvariantCulture),
                run.CrossSectionNm2.ToString("G17", CultureInfo.InvariantCulture),
                run.MeanFreePathNm.ToString("G17", CultureInfo.InvariantCulture),
                (run.StoppingPowerKevPerNm * 1000.0).ToString("G17", CultureInfo.InvariantCulture),
                (acceptedCount > 0 ? run.Electrons.Average(static e => e.Energy) : 0.0).ToString("G17", CultureInfo.InvariantCulture),
                (acceptedCount > 0 ? run.Electrons.Average(static e => e.TotalEnergyLoss) : 0.0).ToString("G17", CultureInfo.InvariantCulture),
                (acceptedCount > 0 ? depths.Average() : 0.0).ToString("G17", CultureInfo.InvariantCulture),
                ComputeQuantile(depths, 0.50).ToString("G17", CultureInfo.InvariantCulture),
                ComputeQuantile(depths, 0.90).ToString("G17", CultureInfo.InvariantCulture),
                ComputeQuantile(depths, 0.99).ToString("G17", CultureInfo.InvariantCulture),
                (lastInelasticDepths.Length > 0 ? lastInelasticDepths.Average() : 0.0).ToString("G17", CultureInfo.InvariantCulture),
                meanPolarAngleDeg.ToString("G17", CultureInfo.InvariantCulture)));
        }

        File.WriteAllText(path, builder.ToString(), new UTF8Encoding(false));
        return path;
    }

    private static string WriteElasticSamplerBenchmarkHistogramCsv(
        string outputDirectory, string fileStem, string suffix, string quantityName, ElasticSamplerBenchmarkRunResult[] runs,
        Func<MonteCarloBenchmarkElectron, double> selector, int binCount)
    {
        var path = Path.Combine(outputDirectory, $"{fileStem}.{suffix}.csv");
        var allValues = runs.SelectMany(run => run.Electrons.Select(selector)).Where(static value => double.IsFinite(value)).ToArray();
        var min = allValues.Length > 0 ? allValues.Min() : 0.0;
        var max = allValues.Length > 0 ? allValues.Max() : min + 1.0;
        if (!(max > min))
            max = min + 1.0;

        var binWidth = (max - min) / binCount;
        var builder = new StringBuilder();
        builder.AppendLine($"Source,BinIndex,Lower{quantityName},Upper{quantityName},Center{quantityName},Count,Fraction");
        foreach (var run in runs)
        {
            var counts = new int[binCount];
            foreach (var value in run.Electrons.Select(selector))
            {
                if (!double.IsFinite(value))
                    continue;
                int index = (int)Math.Floor((value - min) / binWidth);
                index = Math.Clamp(index, 0, binCount - 1);
                counts[index]++;
            }

            for (int i = 0; i < binCount; i++)
            {
                var lower = min + i * binWidth;
                var upper = lower + binWidth;
                var center = (lower + upper) * 0.5;
                var fraction = run.Electrons.Length > 0 ? counts[i] / (double)run.Electrons.Length : 0.0;
                builder.AppendLine(string.Join(",",
                    run.SourceName,
                    i.ToString(CultureInfo.InvariantCulture),
                    lower.ToString("G17", CultureInfo.InvariantCulture),
                    upper.ToString("G17", CultureInfo.InvariantCulture),
                    center.ToString("G17", CultureInfo.InvariantCulture),
                    counts[i].ToString(CultureInfo.InvariantCulture),
                    fraction.ToString("G17", CultureInfo.InvariantCulture)));
            }
        }

        File.WriteAllText(path, builder.ToString(), new UTF8Encoding(false));
        return path;
    }

    private static double ComputeQuantile(double[] sortedValues, double probability)
    {
        if (sortedValues == null || sortedValues.Length == 0)
            return 0.0;

        if (probability <= 0)
            return sortedValues[0];
        if (probability >= 1)
            return sortedValues[^1];

        var index = probability * (sortedValues.Length - 1);
        var lower = (int)Math.Floor(index);
        var upper = (int)Math.Ceiling(index);
        if (lower == upper)
            return sortedValues[lower];

        var fraction = index - lower;
        return sortedValues[lower] + (sortedValues[upper] - sortedValues[lower]) * fraction;
    }
    */
    #endregion

    private void FormEBSD_Load(object sender, EventArgs e)
    {
        glControlGeo = new GLControlAlpha()
        {
            AllowMouseRotation = true,
            AllowMouseScaling = true,
            AllowMouseTranslating = false,
            Name = "glControlAxes",
            ProjectionMode = GLControlAlpha.ProjectionModes.Orhographic,
            ProjWidth = 120.0,
            RotationMode = GLControlAlpha.RotationModes.Object,
            Dock = DockStyle.Fill,
            LightPosition = new V3(100, 100, 100),
            BorderStyle = BorderStyle.Fixed3D,

            WorldMatrix = M4.CreateRotationZ(-Math.PI / 2 * 0.2) * M4.CreateRotationY(-Math.PI / 2 * 0.8) * M4.CreateRotationZ(-Math.PI / 2),
        };
        panelGeometry.Controls.Add(glControlGeo);
        // EnsureMasterPatternPreviewControl(); // (260322Ch) 旧実装: preview 初期化を helper へまとめていた
        if (comboBoxMasterPatternGrid != null && comboBoxMasterPatternGrid.SelectedIndex < 0)
        {
            int defaultIndex = comboBoxMasterPatternGrid.FindStringExact("256");
            comboBoxMasterPatternGrid.SelectedIndex = defaultIndex >= 0 ? defaultIndex : 0; // (260322Ch) MasterPattern 分解能の既定値は 256 にする
        }
        if (comboBoxMasterPattern2DHemisphere != null && comboBoxMasterPattern2DHemisphere.SelectedIndex < 0 && comboBoxMasterPattern2DHemisphere.Items.Count > 0)
            comboBoxMasterPattern2DHemisphere.SelectedIndex = 0; // (260322Ch) MasterPattern2D の初期表示半球を +Z にそろえる
        #region MasterPattern3D control // (260322Ch)
        // EnsureMasterPattern3DControl(); // (260322Ch) 旧実装: Rosca-Lambert 球面 preview 用の GLControl 生成を helper 化していた
        if (glControlMasterPattern3D == null && panelMasterPattern3D != null)
        {
            glControlMasterPattern3D = new GLControlAlpha()
            {
                AllowMouseRotation = true,
                AllowMouseScaling = true,
                AllowMouseTranslating = false,
                Name = "glControlMasterPattern3D",
                ProjectionMode = GLControlAlpha.ProjectionModes.Orhographic,
                ProjWidth = 2.6,
                RotationMode = GLControlAlpha.RotationModes.Object,
                Dock = DockStyle.Fill,
                LightPosition = new V3(20, 20, 60),
                BorderStyle = BorderStyle.Fixed3D,
                BackColor = Color.Black,
                WorldMatrix = M4.CreateRotationX(-Math.PI / 5.0) * M4.CreateRotationY(Math.PI / 5.0),
            };
            panelMasterPattern3D.Controls.Add(glControlMasterPattern3D);
        }
        if (glControlMasterPattern3DAxes == null && panelMasterPattern3DAxes != null)
        {
            glControlMasterPattern3DAxes = new GLControlAlpha()
            {
                AllowMouseRotation = false,
                AllowMouseScaling = false,
                AllowMouseTranslating = false,
                Name = "glControlMasterPattern3DAxes",
                ProjectionMode = GLControlAlpha.ProjectionModes.Orhographic,
                ProjWidth = 2.7,
                RotationMode = GLControlAlpha.RotationModes.Object,
                Dock = DockStyle.Fill,
                LightPosition = new V3(20, 20, 60),
                BorderStyle = BorderStyle.Fixed3D,
                BackColor = Color.Black,
                WorldMatrix = glControlMasterPattern3D?.WorldMatrix ?? (M4.CreateRotationX(-Math.PI / 5.0) * M4.CreateRotationY(Math.PI / 5.0)),
            };
            panelMasterPattern3DAxes.Controls.Add(glControlMasterPattern3DAxes);
        }
        if (glControlMasterPattern3D != null)
        {
            glControlMasterPattern3D.WorldMatrixChanged -= glControlMasterPattern3D_WorldMatrixChanged;
            glControlMasterPattern3D.WorldMatrixChanged += glControlMasterPattern3D_WorldMatrixChanged; // (260322Ch) MasterPattern 本体の回転を axes inset へそのまま反映する
        }
        panelMasterPattern3DAxes?.BringToFront(); // (260322Ch) axes inset を MasterPattern3D の右上へ重ねて表示する
        panelMasterPattern3DAxes.Visible = checkBoxMasterPattern3DAxisArrows.Checked; // (260322Ch) 既存チェックボックスで axes inset の表示可否だけ切り替える
        #endregion

        timer.Interval = 1000;
        timer.Tick += Timer_Tick;
        timer.Start();

        SetVector();
        ResetMasterPattern3DAxes(); // (260322Ch) MasterPattern3D axes inset の結晶軸オブジェクトを生成する
        DrawGeometry();
        comboBoxGradient.SelectedIndex = 0;
        comboBoxScale.SelectedIndex = 0;
        NumericBoxEnergyStart_ValueChanged(sender, e);
        NumericBoxThicknessStart_ValueChanged(sender, e);
        DrawMasterPattern2D(); // (260322Ch) 空の MasterPattern2D でも初期状態を描画しておく
    }

    private void FormEBSD_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        FormMain.toolStripButtonEBSD.Checked = false;
        Visible = false;
    }

    #endregion

    #region OpenGLで入射電子、試料、検出器の幾何学を描画し、ステレオネット上に検出器の輪郭を描画
    /// <summary>試料と電子線が交差する位置は常に(0,0,0)</summary>
    public void DrawGeometry(int i = -1, int j = -1)
    {
        #region OpenGLによる3D描画
        var glObjects = new List<GLObject>();

        //試料の傾き
        var samRot = Matrix3D.RotX(SmpTilt);
        //試料を示す直方体
        var sample = new Parallelepiped(samRot * new V3(-15, -15, -1), samRot * new V3(30, 0, 0), samRot * new V3(0, 30, 0), samRot * new V3(0, 0, 1), new Material(C4.AliceBlue), DrawingMode.SurfacesAndEdges);
        glObjects.Add(sample);

        //検出器の傾き
        var (sinDetTilt, cosDetTilt) = Math.SinCos(DetTilt);
        var detector = new Cylinder(new V3(0, -DetY, -DetZ), new V3(0, sinDetTilt, -cosDetTilt), DetR, new Material(C4.GreenYellow, 0.7), DrawingMode.Surfaces, true, 2, 180);
        glObjects.Add(detector);

        //XYZ軸
        var len = 50;
        //X軸
        glObjects.Add(new Lines([new V3(0, 0, 0), new V3(len, 0, 0)], 3f, new Material(C4.OrangeRed)));
        glObjects.Add(new TextObject("+X", 10f, new V3(len, 0, 0), 100, true, new Material(C4.OrangeRed), glControlGeo));

        //Y軸
        glObjects.Add(new Lines([new V3(0, 0, 0), new V3(0, -len, 0)], 3f, new Material(C4.YellowGreen)));
        glObjects.Add(new TextObject("+Y", 10f, new V3(0, -len, 0), 100, true, new Material(C4.YellowGreen), glControlGeo));

        //Z軸 = beam
        glObjects.Add(new Lines([new V3(0, 0, 0), new V3(0, 0, -len)], 3f, new Material(C4.MediumPurple)));
        glObjects.Add(new TextObject("+Z (=beam)", 10f, new V3(0, 0, -len), 100, true, new Material(C4.MediumPurple), glControlGeo));

        //照射点から検出器の縁への黄色線
        glObjects.AddRange(Enumerable.Range(0, 30).Select(e =>
        {
            var θ = e / 15.0 * Math.PI;
            var (sinθ, cosθ) = Math.SinCos(θ);
            var p = M3.CreateRotationX(-DetTilt) * (DetR * new V3(-sinθ, cosθ, 0));
            return new Lines([new V3(0, 0, 0), new(p.X, p.Y - DetY, p.Z - DetZ)], 1f, new Material(C4.Yellow, 0.7));
        }));

        //電子線方向を示す矢印
        glObjects.Add(new Cone(new V3(0, 0, 0), new V3(0, 0, 100), 5, new Material(C4.Yellow, 0.7), DrawingMode.Surfaces) { IgnoreNormalSides = true });

        //結晶のa, b, c軸を表す矢印
        var max = new[] { Crystal.A, Crystal.B, Crystal.C }.Max();
        var vec = new[] { Crystal.A_Axis, Crystal.B_Axis, Crystal.C_Axis };
        C4[] color = [C4.Red, C4.Green, C4.Blue];
        string[] label = ["a", "b", "c"];
        for (int n = 0; n < 3; n++)
        {
            var v = samRot * Crystal.RotationMatrix * vec[n] / max * 10;
            glObjects.Add(new Cylinder(-v, v * 2 - 2 * v.Normarize(), 0.4, new Material(color[n]), DrawingMode.Surfaces));
            glObjects.Add(new Cone(v, -2 * v.Normarize(), 0.8, new Material(color[n]), DrawingMode.Surfaces));
            glObjects.Add(new TextObject(label[n], 13f, v + 0.1 * v.Normarize(), 0.5, true, new Material(color[n]), glControlGeo));
        }
        glObjects.Add(new Sphere(new V3(0, 0, 0), 1.2, new Material(C4.Gray), DrawingMode.Surfaces));

        glControlGeo.DeleteAllObjects();
        glControlGeo.AddObjects(glObjects);
        glControlGeo.Refresh();
        #endregion OpenGL描画ここまで

        #region ステレオネット上に検出器の輪郭を描画

        var lines = new List<(PointD[], double, Color)>();
        M3 samRot2 = M3.CreateRotationX(SmpTilt), detRot = M3.CreateRotationX(-DetTilt);
        var f1 = new Func<double, double, PointD>((x, y)
            => Stereonet.ConvertVectorToSchmidt(samRot2 * (detRot * (DetR * new V3(x, y, 0)) + new V3(0, -DetY, -DetZ))));

        var step = 60;
        var range = Enumerable.Range(0, step + 1).Select(e => (double)e);
        lines.Add((
            range.Select(n => 2.0 * Math.PI * n / step).Select(Θ => f1(Math.Sin(Θ), Math.Cos(Θ))).ToArray(),
            2, Color.Yellow));

        int div = DetectorDivision;
        for (int n = 0; n < div + 1; n++)
        {
            lines.Add((range.Select(n => 1 - 2 * n / step).Select(x => f1(2.0 * n / div - 1, x)).ToArray(), 1, Color.Orange));
            lines.Add((range.Select(n => 1 - 2 * n / step).Select(x => f1(x, 2.0 * n / div - 1)).ToArray(), 1, Color.Orange));
        }

        if ((uint)i < (uint)DetectorDivision && (uint)j < (uint)DetectorDivision)
        {
            var r1 = range.Select(n => n / step);
            lines.Add((
                [
                ..r1.Select(x => f1(2.0 * i / div - 1, 2.0 * (- j - 1 + x)/ div + 1)),
                ..r1.Select(x => f1(2.0 * (i + x) / div - 1, 2.0 * (- j) / div + 1 )),
                ..r1.Select(x => f1(2.0 * (i + 1) / div - 1, 2.0 * (- j - x) / div + 1)),
                ..r1.Select(x => f1(2.0 * (i + 1 - x) / div - 1, 2.0 * (- j - 1) / div + 1 )),
                ], 3, Color.Orange));
        }

        poleFigureControl.Lines = [.. lines];

        poleFigureControl.Draw();
        #endregion ステレオネット上に検出器の輪郭を描画 ここまで

    }
    #endregion

    #region 3Dレンダリングの視点変更
    private void buttonViewFromZ_Click(object sender, EventArgs e) => glControlGeo.WorldMatrix = M4.Identity;

    private void buttonViewFromX_Click(object sender, EventArgs e) => glControlGeo.WorldMatrix = M4.CreateRotationY(-Math.PI / 2) * M4.CreateRotationZ(-Math.PI / 2);

    private void buttonFromSurfaceNormal_Click(object sender, EventArgs e) => glControlGeo.WorldMatrix = M4.CreateRotationX(-numericBoxSampleTilt.RadianValue);

    private void buttonViewQuarter_Click(object sender, EventArgs e)
        => glControlGeo.WorldMatrix = M4.CreateRotationZ(-Math.PI / 2 * 0.2) * M4.CreateRotationY(-Math.PI / 2 * 0.8) * M4.CreateRotationZ(-Math.PI / 2);

    private void buttonMasterPattern3DViewAlong_Click(object sender, EventArgs e)
    {
        if (glControlMasterPattern3D == null
            || Crystal?.A_Axis == null
            || Crystal.B_Axis == null
            || Crystal.C_Axis == null)
            return;

        // var zoneAxis = new Vector3DBase(numericBoxMasterPattern3DViewAlongU.Value, numericBoxMasterPattern3DViewAlongV.Value, numericBoxMasterPattern3DViewAlongW.Value); // (260322Ch) 旧案: [u v w] を直交座標 x,y,z と同一視していた
        var zoneAxis = numericBoxMasterPattern3DViewAlongU.Value * Crystal.A_Axis + numericBoxMasterPattern3DViewAlongV.Value * Crystal.B_Axis + numericBoxMasterPattern3DViewAlongW.Value * Crystal.C_Axis; // (260322Ch) 結晶学的 [u v w] を実空間ベクトルへ変換する
        if (zoneAxis.Length2 < 1e-12)
        {
            toolStripStatusLabel1.Text = "Zone axis [u v w] cannot be [0 0 0]."; // (260322Ch) 無効な軸指定はそのまま無視せず状態欄へ知らせる
            return;
        }

        // glControlMasterPattern3D.WorldMatrix = GLGeometry.CreateRotationToZ(zoneAxis.ToOpenTK()).ToMatrix4d(); // (260322Ch) 旧検討: Z軸を zone axis 側へ倒すと「axis 方向から眺める」向きと逆になる
        glControlMasterPattern3D.WorldMatrix = GLGeometry.CreateRotationFromZ(zoneAxis.ToOpenTK()).ToMatrix4d(); // (260322Ch) zone axis が viewer の +Z 方向を向くように回転する
        toolStripStatusLabel1.Text = $"MasterPattern3D view: [{numericBoxMasterPattern3DViewAlongU.Value:g} {numericBoxMasterPattern3DViewAlongV.Value:g} {numericBoxMasterPattern3DViewAlongW.Value:g}]"; // (260322Ch)
    }

    #endregion

    #region MasterPattern3D axes inset

    private void glControlMasterPattern3D_WorldMatrixChanged(object sender, EventArgs e)
        => SyncMasterPattern3DAxesWorldMatrix();

    private void ResetMasterPattern3DAxes()
    {
        if (glControlMasterPattern3DAxes == null || Crystal?.A_Axis == null || Crystal.B_Axis == null || Crystal.C_Axis == null)
            return;

        var max = new[] { Crystal.A, Crystal.B, Crystal.C }.Max();
        if (max <= 0)
            return;

        var vec = new[] { Crystal.A_Axis / max, Crystal.B_Axis / max, Crystal.C_Axis / max };
        C4[] color = [C4.Red, C4.Green, C4.Blue];
        var obj = new List<GLObject>(7);
        for (int i = 0; i < 3; i++)
        {
            obj.Add(new Cylinder(-vec[i], vec[i] * 2 - 0.3 * vec[i].Normarize(), 0.075, new Material(color[i]), DrawingMode.Surfaces));
            obj.Add(new Cone(vec[i], -0.3 * vec[i].Normarize(), 0.15, new Material(color[i]), DrawingMode.Surfaces));
            // obj.Add(new TextObject(label[i], 13, vec[i] + 0.1 * vec[i].Normarize(), 0, true, new Material(color[i]), glControlMasterPattern3DAxes)); // (260322Ch) MasterPattern3D axes inset は文字ラベルを表示しない
        }
        obj.Add(new Sphere(new V3(0, 0, 0), 0.2, new Material(C4.Gray), DrawingMode.Surfaces));

        glControlMasterPattern3DAxes.DeleteAllObjects();
        glControlMasterPattern3DAxes.AddObjects(obj);
        SyncMasterPattern3DAxesWorldMatrix();
        glControlMasterPattern3DAxes.Refresh();
    }

    private void SyncMasterPattern3DAxesWorldMatrix()
    {
        if (glControlMasterPattern3DAxes == null || glControlMasterPattern3D == null)
            return;

        glControlMasterPattern3DAxes.WorldMatrix = glControlMasterPattern3D.WorldMatrix; // (260322Ch) axes inset は MasterPattern3D と同じ回転状態を使う
    }

    #endregion

    #region その他のイベント

    /// <summary>FormMainから、結晶が変更されたときに呼び出される</summary>
    public void SetCrystal()
    {
        SetVector();
        ResetMasterPattern3DAxes(); // (260322Ch) 結晶変更時は MasterPattern3D axes inset も描き直す
        Draw();
    }

    /// <summary>サンプルや検出器の幾何学条件が変更されたとき</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void numericBoxDetRadius_ValueChanged(object sender, EventArgs e)
    {
        UpdateEbsdTiltCoeffs(); // 260325Cl: tilt 係数を再計算
        DrawGeometry();
        Draw();
    }

    private void FormEBSD_VisibleChanged(object sender, EventArgs e)
    {
        SetVector();
        ResetMasterPattern3DAxes(); // (260322Ch) 再表示時に MasterPattern3D axes inset も現在の結晶へ合わせる
        DrawGeometry();
        DrawMasterPattern2D(); // (260322Ch) 再表示時に MasterPattern2D も同期する
    }

    private void radioButtonKikuchiThresholdOfStructureFactor_CheckedChanged(object sender, EventArgs e)
    {
        if (sender is RadioButton b && b.Checked)
        {
            SetVector();
            Draw();
        }
    }

    private void numericBoxKikuchiThreadSholdOfStructureFactor_ValueChanged(object sender, EventArgs e) => SetVector();

    private void colorControlExcessLine_ColorChanged(object sender, EventArgs e) => Draw();

    private void waveLengthControl_WavelengthChanged(object sender, EventArgs e) => SetVector();

    #endregion

    #region 描画関数
    /// <summary>描画関数</summary>
    public void Draw(Graphics g = null)
    {
        // DrawKikuchiLine(); // (260331Ch) 旧順序: EBSD 更新前に overlay を描いていた
        // DrawEBSD(); // (260331Ch) 旧順序
        DrawEBSD(); // (260331Ch) まず Pbmp を更新してから
        DrawKikuchiLine(g); // (260331Ch) その上に Kikuchi 線と指数ラベルを重ねる
        DrawGeometry();
    }
    #endregion

    #region プロジェクション行列の設定
    /// <summary>プロジェクション行列の設定を行う。</summary>
    public bool SetProjection(Graphics g = null)
    {
        if (g != null && graphicsBox.ClientSize.Width != 0 && graphicsBox.ClientSize.Height != 0)
            try
            {
                g.Transform = new Matrix(
                (float)(1 / Resolution), 0, 0, (float)(1 / Resolution),
                (float)(graphicsBox.ClientSize.Width / 2.0 + Foot.X / Resolution),
                (float)(graphicsBox.ClientSize.Height / 2.0 + Foot.Y / Resolution));
            }
            catch { return false; }
        return true;
    }
    #endregion

    #region 菊池線(運動学的) graphicBox を描画

    /// <summary></summary>
    /// <param name="graphics"></param>
    private void DrawKikuchiLine(Graphics graphics = null, int i = -1, int j = -1)
    {
        if (InvokeRequired)//別スレッドから呼び出されたとき Invokeして呼びなおす
        {
            Invoke(new Action(() => DrawKikuchiLine(graphics, i, j)), null);
            return;
        }
        //グラフィックスボックスに描画する場合
        graphics ??= graphicsBox.Graphics;

        if (!SetProjection(graphics)) return;

        graphics.Clear(colorControlBackGround.Color);

        if (checkBoxDrawKikuchiLineDynamical.Checked && Pbmp != null)
        {
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphics.PixelOffsetMode = PixelOffsetMode.Half;

            var bmp = Pbmp.GetImage(new RectangleD(0, 0, Pbmp.Width, Pbmp.Height), graphicsBox.ClientSize);
            graphics.DrawImage(bmp, new RectangleD(-DetR, -DetR - Foot.Y, DetR * 2, DetR * 2));
        }


        if (checkBoxDrawKikuchiLinesKinematical.Checked)
        {
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            var penExcess = new Pen(new SolidBrush(colorControlExcessLine.Color), (float)(trackBarLineWidth.Value * Resolution / 2000f));
            var diag = Resolution * Math.Sqrt(graphicsBox.ClientSize.Width * graphicsBox.ClientSize.Width + graphicsBox.ClientSize.Height * graphicsBox.ClientSize.Height) / 2;
            var font = new Font("Tahoma", (float)(trackBarStrSize.Value / 8.0 * Resolution));
            var brush = new SolidBrush(colorControlString.Color);

            var Tau = numericBoxDetTilt.RadianValue - numericBoxSampleTilt.RadianValue;

            foreach (var g in Crystal.VectorOfG_KikuchiLine)
            {
                double sinTheta = WaveLength * g.Length / 2, sin2Theta = sinTheta * sinTheta;

                Vector3DBase vec1 = Crystal.RotationMatrix * g;

                //vec2は、検出器法線がZ軸と一致するようにX軸を回転軸に回転させたベクトル
                var vec2 = Matrix3D.Rot(new Vector3DBase(1, 0, 0), -Tau) * vec1;

                //vec3は、検出器法線(Z軸)を軸としてpsiだけ回転させて、(0,y,z)の形になるようにしたベクトル
                var psi = Math.Atan2(vec2.X, vec2.Y);
                var (sinPsi, cosPsi) = Math.SinCos(psi);
                var vec3 = Matrix3D.Rot(new Vector3DBase(0, 0, 1), psi) * vec2;

                //vec3normは、vec3を規格化したベクトル
                var vec3norm = vec3.Normarize();
                double sinPhi = vec3norm.Y, sin2Phi = sinPhi * sinPhi;
                double cosPhi = vec3norm.Z;

                double P = (sin2Phi - sin2Theta) / (CameraLength2 * CameraLength2 * (1 - sin2Theta)), Psqrt = Math.Sqrt(P);
                double Q = P * (sin2Phi - sin2Theta) / sin2Theta, Qsqrt = Math.Sqrt(Q);
                double Y = CameraLength2 * sinPhi * cosPhi / (sin2Phi - sin2Theta);

                if (!double.IsNaN(Psqrt) && !double.IsNaN(Qsqrt))
                {
                    // y= sinh(x) の逆関数は x = log{y+ sqrt(y*y+1)}
                    double omegaMax = Math.Log(diag * Psqrt + Math.Sqrt(diag * Psqrt * diag * Psqrt + 1)) * 2;
                    var pts = new List<PointD>();
                    for (double omega = -omegaMax; omega < omegaMax; omega += omegaMax / 500)
                    {
                        double x = Math.Sinh(omega) / Psqrt, y = -Math.Cosh(omega) / Qsqrt;
                        var pt = new PointD(cosPsi * x - sinPsi * (y - Y), sinPsi * x + cosPsi * (y - Y));

                        if (IsScreenArea(pt))
                            pts.Add(pt);
                    }

                    if (pts.Count > 1)
                    {
                        if (checkBoxKikuchiLine_Kinematical.Checked)
                            penExcess.Color = Blend(colorControlExcessLine.Color, colorControlBackGround.Color, g.RelativeIntensity);
                        graphics.DrawLines(penExcess, pts.ToArray());

                        //ラベル描画
                        if (checkBoxShowGIndices.Checked) // 260331Cl checkBoxShowGIndices で制御
                        {
                            //まず傾きをみて線のどちら側にラベルを付けるかを決める。θは -π ~ +πの範囲で調節
                            var original = graphics.Transform;
                            var θ = Math.Atan2(pts[^1].Y - pts[0].Y, pts[^1].X - pts[0].X);
                            if (-Math.PI / 2 < θ && θ < Math.PI / 2)
                            {
                                graphics.TranslateTransform(pts[0].X, pts[0].Y);
                                graphics.RotateTransform(θ);
                            }
                            else
                            {
                                graphics.TranslateTransform(pts[^1].X, pts[^1].Y);
                                graphics.RotateTransform(θ + Math.PI);
                            }
                            graphics.DrawString(g.Text, font, brush, new PointF(0, 0));
                            graphics.Transform = original;
                        }
                    }
                }
            }
        }

        // 260331Cl 追加: 晶帯軸ラベルをバンド交点に表示
        if (checkBoxShowZoneAxisIndices.Checked && checkBoxDrawKikuchiLinesKinematical.Checked && Crystal.VectorOfG_KikuchiLine.Count >= 2)
        {
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            var fontZA = new Font("Tahoma", (float)(trackBarStrSize.Value / 8.0 * Resolution * 0.85));
            var brushZA = new SolidBrush(Color.FromArgb(220, colorControlString.Color));
            var Tau = numericBoxDetTilt.RadianValue - numericBoxSampleTilt.RadianValue;
            var rot = Crystal.RotationMatrix;
            var rotTau = Matrix3D.Rot(new Vector3DBase(1, 0, 0), -Tau);

            var drawnZoneAxes = new HashSet<(int, int, int)>();
            var gList = Crystal.VectorOfG_KikuchiLine;
            for (int ii = 0; ii < gList.Count - 1; ii++)
                for (int jj = ii + 1; jj < gList.Count; jj++)
                {
                    var g1 = gList[ii]; var g2 = gList[jj];
                    // g ベクトルの外積 → 晶帯軸方向（直交座標系、回転前）
                    var cross = Vector3DBase.VectorProduct(g1, g2);
                    if (cross.Length2 < 1e-20) continue;

                    // 指数 [uvw] を求める: cross (直交座標) = u*A_Axis + v*B_Axis + w*C_Axis
                    // → (u,v,w) = MatrixReal⁻¹ * cross = MatrixInverse * cross
                    var uvwVec = Crystal.MatrixInverse * cross;
                    double maxComp = Math.Max(Math.Max(Math.Abs(uvwVec.X), Math.Abs(uvwVec.Y)), Math.Abs(uvwVec.Z));
                    if (maxComp < 1e-10) continue;
                    double scale = 1.0 / maxComp;
                    int bestU = 0, bestV = 0, bestW = 0;
                    double bestError = double.MaxValue;
                    for (int m = 1; m <= 12; m++)
                    {
                        double su = uvwVec.X * scale * m, sv = uvwVec.Y * scale * m, sw = uvwVec.Z * scale * m;
                        int ru2 = (int)Math.Round(su), rv2 = (int)Math.Round(sv), rw2 = (int)Math.Round(sw);
                        double err = Math.Abs(su - ru2) + Math.Abs(sv - rv2) + Math.Abs(sw - rw2);
                        if (err < bestError) { bestError = err; bestU = ru2; bestV = rv2; bestW = rw2; }
                        if (err < 0.05) break;
                    }
                    if (bestU == 0 && bestV == 0 && bestW == 0) continue;
                    static int Gcd(int a, int b) { a = Math.Abs(a); b = Math.Abs(b); while (b != 0) { (a, b) = (b, a % b); } return a; }
                    int gcd = Gcd(Gcd(Math.Abs(bestU), Math.Abs(bestV)), Math.Abs(bestW));
                    if (gcd > 1) { bestU /= gcd; bestV /= gcd; bestW /= gcd; }
                    if (bestU < 0 || (bestU == 0 && bestV < 0) || (bestU == 0 && bestV == 0 && bestW < 0))
                    { bestU = -bestU; bestV = -bestV; bestW = -bestW; }
                    if (drawnZoneAxes.Contains((bestU, bestV, bestW))) continue;
                    drawnZoneAxes.Add((bestU, bestV, bestW));

                    // 晶帯軸方向を検出器座標に投影（実空間ベクトルを使用）
                    var zoneAxisReal = bestU * Crystal.A_Axis + bestV * Crystal.B_Axis + bestW * Crystal.C_Axis;
                    var dir = rotTau * (rot * zoneAxisReal);
                    if (dir.Z < 0) dir = -dir; // 検出器に向く方を選択
                    if (dir.Z < 1e-15) continue;

                    // 中心投影: EBSD は試料側から見た図形なので X 方向を反転 (BuildEbsdLookupTable の ax = -Ri.E11 と整合)
                    double detX = -CameraLength2 * dir.X / dir.Z;
                    double detY2 = CameraLength2 * dir.Y / dir.Z;

                    var ptZA = new PointD(detX, detY2);
                    if (!IsScreenArea(ptZA, -20)) continue;

                    string label = $"[{bestU} {bestV} {bestW}]";
                    var size = graphics.MeasureString(label, fontZA);
                    graphics.DrawString(label, fontZA, brushZA, new PointF((float)ptZA.X - size.Width / 2, (float)ptZA.Y - size.Height / 2));
                }
        }

        if (checkBoxDrawDetectorOutline.Checked)
        {
            //検出器を示す円を描画
            graphics.DrawArc(new Pen(Color.Yellow, ResolutionF * 2), -DetR, -DetR - Foot.Y, DetR * 2, DetR * 2, 0, 360);
            //検出器の分割線
            for (int n = 0; n < DetectorDivision; n++)
            {
                var x = 2.0 * n / DetectorDivision - 1;
                graphics.DrawLine(new Pen(Color.Orange, ResolutionF), -DetR, x * DetR - Foot.Y, DetR, x * DetR - Foot.Y);
                graphics.DrawLine(new Pen(Color.Orange, ResolutionF), x * DetR, -DetR - Foot.Y, x * DetR, DetR - Foot.Y);
            }
            if ((uint)i < (uint)DetectorDivision && (uint)j < (uint)DetectorDivision)
            {
                double x = 2.0 * i / DetectorDivision - 1, y = 2.0 * j / DetectorDivision - 1;

                graphics.FillRectangle(new SolidBrush(Color.FromArgb(32, Color.Orange)), DetR * x, DetR * y - Foot.Y, 2 * DetR / DetectorDivision, 2 * DetR / DetectorDivision);
            }
        }
        graphicsBox.Refresh();
    }

    /// <summary>Blends the specified colors together.</summary>
    /// <param name="color">Color to blend onto the background color.</param>
    /// <param name="backColor">Color to blend the other color onto.</param>
    /// <param name="amount">How much of <paramref name="color"/> to keep,
    /// “on top of” <paramref name="backColor"/>.</param>
    /// <returns>The blended colors.</returns>
    public static Color Blend(Color color, Color backColor, double amount)
    {
        byte r = (byte)((color.R * amount) + backColor.R * (1 - amount));
        byte g = (byte)((color.G * amount) + backColor.G * (1 - amount));
        byte b = (byte)((color.B * amount) + backColor.B * (1 - amount));
        return Color.FromArgb(r, g, b);
    }

    #endregion

    #region 座標変換

    /// <summary>検出器座標で与えられた座標ptが、画面内に含まれるかどうかを返す</summary>
    /// <param name="pt"></param>
    /// <returns></returns>
    private bool IsScreenArea(in PointD pt, int margin = 0)
    {
        var clientPt = convertDetectorToScreen(pt);
        return clientPt.X > margin && clientPt.Y > margin
            && clientPt.X < graphicsBox.ClientRectangle.Width - margin
            && clientPt.Y < graphicsBox.ClientRectangle.Height - margin;
    }

    /// <summary>フィルム(Src)上の位置 (mm)を座標系変換 画面(Client)上の点(pixel)に変換</summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private PointD convertDetectorToScreen(in double x, in double y)
    {
        double px = (x + Foot.X) / Resolution + graphicsBox.ClientSize.Width / 2.0;
        double py = (y + Foot.Y) / Resolution + graphicsBox.ClientSize.Height / 2.0;
        return new(px, py);
    }

    /// <summary>検出器(Detector)上の位置 (mm)を画面(Screen)上の点(pixel)に変換</summary>
    /// <param name="pt"></param>
    /// <returns></returns>
    private PointD convertDetectorToScreen(in PointD pt) => convertDetectorToScreen(pt.X, pt.Y);
    #endregion

    #region 菊池線 graphicsBoxのイベント (graphicsBox上のマウスイベントも含む)

    //private bool MouseRangingMode = false;
    //private Point MouseRangeStart, MouseRangeEnd;//, startAnimation;
    private void graphicsBox_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Clicks == 2)
        {
            var size = graphicsBox.ClientSize;
            var i = e.Location.X * DetectorDivision / size.Width;
            var j = e.Location.Y * DetectorDivision / size.Height;
            if ((uint)i < (uint)DetectorDivision && (uint)j < (uint)DetectorDivision)
            {
                //
                DrawKikuchiLine(null, i, j);
                DrawGeometry(i, j);
                CalcStatistics(i, j);
            }
        }
    }

    private void graphicsBox_MouseUp(object sender, MouseEventArgs e)
    {
        Draw();
    }

    private PointD lastMousePos = new();

    private void graphicsBox_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
    {
        var mousePos = new PointD(e.X, e.Y);
        var center = new PointD(graphicsBox.ClientSize.Width / 2.0, graphicsBox.ClientSize.Height / 2.0);
        //左ボタンが押されながらマウスが動いたとき
        if (e.Button == MouseButtons.Left)
        {
            if ((e.X - graphicsBox.ClientSize.Width / 2) * (e.X - graphicsBox.ClientSize.Width / 2) + (e.Y - graphicsBox.ClientSize.Height / 2) * (e.Y - graphicsBox.ClientSize.Height / 2)
                < Math.Min(graphicsBox.ClientSize.Width, graphicsBox.ClientSize.Height) * Math.Min(graphicsBox.ClientSize.Width, graphicsBox.ClientSize.Height) * 0.18)
            {
                if (mousePos != lastMousePos)
                {
                    var devPos = mousePos - lastMousePos;
                    var devAngle = Math.Atan((mousePos - lastMousePos).Length * Resolution / CameraLength2);
                    FormMain.Rotate((-1 * devPos.Y, -Math.Cos(SmpTilt - DetTilt) * devPos.X, Math.Sin(SmpTilt - DetTilt) * devPos.X), devAngle);
                }
            }
            else
                FormMain.Rotate((0, Math.Sin(SmpTilt - DetTilt), Math.Cos(SmpTilt - DetTilt)), -Math.Atan2(lastMousePos.X - center.X, lastMousePos.Y - center.Y) + Math.Atan2(mousePos.X - center.X, mousePos.Y - center.Y));
            //Draw関数は、FormMain.Rotateを呼び出した後、FormMainから呼ばれる
        }
        lastMousePos = mousePos;
    }

    private void graphicsBox_Resize(object sender, EventArgs e) => Draw();

    #endregion graphicsBoxのイベント

    #region 菊池線を初期化。最後にDraw()も呼び出す。
    /// <summary>菊池線を初期化。最後にDraw()も呼び出す。</summary>
    /// <param name="renewCrystal"></param>
    public void SetVector(bool renewCrystal = false)
    {
        if (FormMain == null) return;
        var sw = new Stopwatch();
        sw.Start();
        int width = graphicsBox.ClientSize.Width, height = graphicsBox.ClientSize.Height;

        var crystal = FormMain.Crystal;
        crystal.SetVectorOfG(0, 2 / WaveLength, waveLengthControl.WaveSource);

        var latticeType = crystal.Symmetry.LatticeTypeStr;

        foreach (var gtemp in crystal.VectorOfG.Where(g => g.Extinction.Length == 0))
            gtemp.Flag1 = true;

        foreach (var gtemp in crystal.VectorOfG.Where(g => g.Extinction.Length > 0 && g.Extinction[0] == latticeType))
            gtemp.Flag1 = false;

        foreach (var gtemp in crystal.VectorOfG.Where(g => g.Extinction.Length > 0 && g.Extinction[0] != latticeType))
            gtemp.Flag1 = false;

        if (radioButtonKikuchiThresholdOfLength.Checked)
        {
            Crystal.VectorOfG_KikuchiLine =
            [.. Crystal.VectorOfG.Where(g => g.Length < numericBoxKikuchiThresholdOfLength.Value).OrderByDescending(g => g.Length)];
        }
        else
        {
            var list = Crystal.VectorOfG.OrderByDescending(g => g.RelativeIntensity).ToList();
            var max = Math.Min(numericBoxKikuchiThreadSholdOfStructureFactor.ValueInteger, Crystal.VectorOfG.Length);
            while (max + 1 < FormMain.Crystal.VectorOfG.Length)
            {
                if (SymmetryStatic.CheckEquivalentPlanes(list[max - 1].Index, list[max].Index, Crystal.Symmetry))
                    max++;
                else
                    break;
            }
            Crystal.VectorOfG_KikuchiLine = list[0..max];
            Crystal.VectorOfG_KikuchiLine.Reverse();
        }
        Draw();
    }
    #endregion

    #region モンテカルロ法による飛程シミュレーション
    /// <summary>モンテカルロによる飛程シミュレーション</summary>
    private static (double Depth, V3 Vec, PointD Position, double Energy, double TotalEnergyLoss, bool HasLastInelasticEvent, double LastInelasticDepth, double LastInelasticEnergyBeforeLoss, double LastInelasticEnergyAfterLoss, V3 LastInelasticDirection)[] RunBackscatterMonteCarlo(
        MonteCarlo monte, int loop, double energyThreshold, M3 sampleRotation, Action<int, int> reportProgress = null)
    {
        var bseLists = new System.Collections.Concurrent.ConcurrentBag<List<(double Depth, V3 Vec, PointD Position, double Energy, double TotalEnergyLoss, bool HasLastInelasticEvent, double LastInelasticDepth, double LastInelasticEnergyBeforeLoss, double LastInelasticEnergyAfterLoss, V3 LastInelasticDirection)>>(); // (260331Ch) 最後の非弾性散乱情報も保持する
        var reportStep = reportProgress == null ? int.MaxValue : Math.Max(1, loop / 100); // (260327Ch) UI へは 1% ごとにだけ流す
        int completed = 0;

        System.Threading.Tasks.Parallel.For(0, loop,
            () => new List<(double Depth, V3 Vec, PointD Position, double Energy, double TotalEnergyLoss, bool HasLastInelasticEvent, double LastInelasticDepth, double LastInelasticEnergyBeforeLoss, double LastInelasticEnergyAfterLoss, V3 LastInelasticDirection)>(256),
            (index, state, localList) =>
            {
                var electron = monte.GetBackscatteredElectronDetail();
                if (electron.Energy > energyThreshold)
                    localList.Add((electron.Depth, electron.Direction, Stereonet.ConvertVectorToSchmidt(sampleRotation * electron.Direction), electron.Energy,
                        electron.TotalEnergyLoss, electron.HasLastInelasticEvent, electron.LastInelasticDepth, electron.LastInelasticEnergyBeforeLoss, electron.LastInelasticEnergyAfterLoss, electron.LastInelasticDirection)); // (260331Ch)

                var current = System.Threading.Interlocked.Increment(ref completed);
                if (reportProgress != null && (current == loop || current % reportStep == 0))
                    reportProgress(current, loop); // (260327Ch) MasterPattern 前処理時だけ進捗を通知する
                return localList;
            },
            localList =>
            {
                if (localList.Count > 0)
                    bseLists.Add(localList);
            });

        return [.. bseLists.SelectMany(localList => localList)];
    }

    /// <summary>モンテカルロによる飛程シミュレーション</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void buttonBSE_Click(object sender, EventArgs e)
    {
        // sw1.Restart(); // (260401Ch) 旧 Calc BSE はここで独自に MC を起動していた
        // BSEs = RunBackscatterMonteCarlo(monte, loop, EnergyThreshold, smpRot); // (260401Ch) 旧実装: MasterPattern 前段とは別経路だった
        if (masterPatternEbsd.IsBuilding)
        {
            toolStripStatusLabel1.Text = "MasterPattern is running. Wait for it to finish or press Stop.";
            return;
        }

        button2.Enabled = false; // (260401Ch) Calc BSE の多重起動を防ぐ
        buttonCreateMasterPattern.Enabled = false; // (260401Ch) Calc BSE 実行中に MasterPattern 前段 MC を重ねて走らせない
        buttonStop.Visible = false; // (260401Ch) Calc BSE でも MC 中はまだ停止できない
        toolStripProgressBar.Value = 0;
        toolStripStatusLabel2.Text = "Calc BSE: MonteCarlo";
        toolStripStatusLabel1.Text = "0% completed, elapsed 0.00 s.";
        toolStripStatusLabel3.Text = "";
        labelMasterPatternInfo.Text = "Calculating BSE by Monte Carlo...";

        try
        {
            await RunMonteCarloAndSetRangesAsync(
                statusPrefix: "Calc BSE",
                progressInfoText: "Calculating BSE by Monte Carlo...",
                completedInfoText: "Monte Carlo finished for Calc BSE.",
                noResultInfoText: "Calc BSE was aborted because Monte Carlo returned no usable BSEs."); // (260401Ch) MasterPattern 前段と同じ MC 前処理へ統一
        }
        finally
        {
            button2.Enabled = true;
            buttonCreateMasterPattern.Enabled = true;
        }
    }
    #endregion

    #region 統計情報を計算しグラフ化
    public void CalcStatistics(int i = -1, int j = -1)
    {
        if (BSEs != null && BSEs.Length > 1 && poleFigureControl.Lines != null && poleFigureControl.Lines.Length > 0)
        {
            double energy = waveLengthControl.Energy;

            M3 smpRot = M3.CreateRotationX(SmpTilt), detRot = M3.CreateRotationX(-DetTilt);
            double cosTilt = Math.Cos(SmpTilt), sinTilt = Math.Sin(SmpTilt);

            #region 検出器の範囲内におさまるbseを抽出し、変数bseに格納
            PointD[] area = [];
            var areaStep = 120;
            var f = new Func<double, double, PointD>((x, y)
                => Stereonet.ConvertVectorToSchmidt(smpRot * (detRot * (DetR * new V3(x, y, 0)) + new V3(0, -DetY, -DetZ))));
            if ((uint)i < (uint)DetectorDivision && (uint)j < (uint)DetectorDivision)//
            {
                var div = DetectorDivision;
                var r1 = ValueEnumerable.Range(0, areaStep).Select(n => (double)n / areaStep);
                area =
                [
                    ..r1.Select(x => f(2.0 * i / div - 1, 2.0 * (- j - 1 + x)/ div + 1)),
                    ..r1.Select(x => f(2.0 * (i + x) / div - 1, 2.0 * (- j) / div + 1 )),
                    ..r1.Select(x => f(2.0 * (i + 1) / div - 1, 2.0 * (- j - x) / div + 1)),
                    ..r1.Select(x => f(2.0 * (i + 1 - x) / div - 1, 2.0 * (- j - 1) / div + 1 ))
                ];
            }
            else
                area = [.. ValueEnumerable.Range(0, areaStep).Select(n => 2.0 * Math.PI * n / areaStep).Select(Θ => f(Math.Sin(Θ), Math.Cos(Θ)))];

            //ある立体角に収まるbseだけを抽出
            var bse2 = BSEs.AsParallel().Where(e =>
            Geometry.InsidePolygonalArea(area, e.Position)).ToArray();
            #endregion
            // bse2 = bse2.Where(e => e[^1].e > energy - 2.5 && e[^1].e < energy - 1.5 && e.Length>2).ToArray();

            var count = bse2.Length;
            //エネルギー分布を描画 ここから
            //if(false)
            {
                double step = 0.25;//kev単位
                double lower = 0, upper = (energy - EnergyThreshold);
                int nBuckets = (int)((upper - lower) / step);
                var histogram = new MathNet.Numerics.Statistics.Histogram(bse2.Select(e => energy - e.Energy), nBuckets, lower, lower + nBuckets * step);
                var pts = new List<PointD>();
                for (int n = 0; n < histogram.BucketCount; n++)
                    pts.Add(new PointD((histogram[n].UpperBound + histogram[n].LowerBound) / 2, (double)histogram[n].Count / count));
                //pts.Add(new PointD(energy*1000 + step / 2, 0));
                graphControlEnergyProfile.ClearProfile();
                graphControlEnergyProfile.Profile = new Profile(pts);
                graphControlEnergyProfile.MaximalX = upper;
                graphControlEnergyProfile.UpperX = upper * 0.5;
                graphControlEnergyProfile.Draw();
            }
            //エネルギー分布を描画 ここまで

            //最大深さ分布　ここから
            {
                //var depths = bse2.Select(e1 => 1000.0 * e1.Max(e2 => sinTilt * e2.p.Y - cosTilt * e2.p.Z));
                // var depths = bse2.Select(e => e.Depth); // (260401Ch) 旧挙動: 脱出時(または停止時)の深さをそのまま集計していた
                var depths = bse2.Select(e => e.HasLastInelasticEvent ? e.LastInelasticDepth : e.Depth); // (260401Ch) MasterPattern 重み付けと同じく、可能なら最後の非弾性散乱深さを深さ分布に使う
                double lower = 0, upper = depths.Max();
                double step = 1;//nm単位
                int nBuckets = (int)((upper - lower) / step + 1);
                var histogram = new MathNet.Numerics.Statistics.Histogram(depths, nBuckets, lower, lower + nBuckets * step);
                var pts = new List<PointD>();
                for (int n = 0; n < histogram.BucketCount; n++)
                    pts.Add(new PointD((histogram[n].UpperBound + histogram[n].LowerBound) / 2, (double)histogram[n].Count / count));
                graphControlDepthProfile.ClearProfile();
                graphControlDepthProfile.Profile = new Profile(pts);
                graphControlDepthProfile.UpperX = upper * 0.5;
                graphControlDepthProfile.Draw();
            }
        }

    }
    #endregion

    #region 入力パラメータ関連
    private void NumericBoxThicknessStart_ValueChanged(object sender, EventArgs e)
    {
        trackBarOutputThickness.Maximum = ThicknessArray.Length - 1;
        trackBarOutputThickness.Value = 0;
    }

    private void NumericBoxEnergyStart_ValueChanged(object sender, EventArgs e)
    {
        trackBarOutputEnergy.Maximum = EnergyArray.Length - 1;
        trackBarOutputEnergy.Value = 0;
    }

    #endregion

    #region 現在のパラメータでEBSDを動力学計算

    #region お蔵入り
    //private void buttonSimulateEBSD_Click(object sender, EventArgs e)
    //{
    //    if (Crystal.Bethe.IsCBED_Busy) return;
    //    if (masterPatternEbsd.IsBuilding)
    //    {
    //        toolStripStatusLabel1.Text = "MasterPattern is running. Wait for it to finish or press Stop.";
    //        return;
    //    }

    //    buttonStop.Visible = true;
    //    sw1.Restart();
    //    //FormDiffractionSimulator.SkipDrawing = true;


    //    //方位配列を作る 

    //    var (sin, cos) = Math.SinCos(DetTilt);
    //    var rotDet = new M4(1, 0, 0, 0,
    //                        0, cos, -sin, DetY - DetY * cos + DetZ * sin,
    //                        0, sin, cos, DetZ - DetY * sin - DetZ * cos,
    //                        0, 0, 0, 1);

    //    var rotSmp = M4.CreateRotationX(numericBoxSampleTilt.RadianValue + Math.PI);

    //    var size = numericBoxDiskDiameter.ValueInteger;
    //    var directions = new List<Vector3DBase>();
    //    for (int h = 0; h < size; h++)
    //        for (int w = 0; w < size; w++)
    //        {
    //            //検出器の座標
    //            var vec = rotDet * new V4(DetR * (2 * w + 1) / size - DetR, DetR * (2 * h + 1) / size - DetR + DetY, DetZ, 1);
    //            vec = rotSmp * vec;

    //            directions.Add(new Vector3DBase(-vec.X, -vec.Y, -vec.Z).Normarize());
    //        }

    //    Directions = [.. directions];

    //    var solver = BetheMethod.Solver.Eigen_Eigen;
    //    Crystal.Bethe.EBSD_Completed += Bethe_EBSD_Completed;
    //    Crystal.Bethe.EBSD_ProgressChanged += Bethe_EBSD_ProgressChanged;
    //    Crystal.Bethe.RunEBSD(MaxNumOfBloch, EnergyArray, Crystal.RotationMatrix, ThicknessArray, Directions, solver, 32, checkBoxNonLocalAbsorption.Checked, checkBoxTDSBackground.Checked);
    //}
    #endregion

    #region BackgroundWorkerからのProgressChanged, Completed

    private bool skipProgressChangedEvent = false;

    #region お蔵入り
    //private void Bethe_EBSD_ProgressChanged(object sender, ProgressChangedEventArgs e)
    //{
    //    if (skipProgressChangedEvent) return;
    //    skipProgressChangedEvent = true;

    //    var current = e.ProgressPercentage;
    //    var message = (string)e.UserState;
    //    if (message.StartsWith("Compiling disks", StringComparison.Ordinal))
    //    {
    //        //if (sw1.IsRunning)
    //        //{
    //        //    sw1.Stop();
    //        //    sw2.Restart();
    //        //}
    //        //var sec = sw2.ElapsedMilliseconds / 1000.0;
    //        //var totalsec = sec + sw1.ElapsedMilliseconds / 1000.0;
    //        //toolStripProgressBar.Value = current / 10;
    //        //toolStripStatusLabel2.Text = "Compiling disks:";
    //        //toolStripStatusLabel1.Text = "Ellapsed time : " + totalsec.ToString("f2") + " s.,  ";
    //        //toolStripStatusLabel1.Text += $"{current / 10.0:f1} % completed,  wait for more {sec * (1000 - current) / current:f2} s.";
    //    }
    //    else
    //    {
    //        var sec = sw1.ElapsedMilliseconds / 1000.0;
    //        var progress = (int)(100.0 * current / DivisionNumber);
    //        if (progress <= 100)
    //            toolStripProgressBar.Value = (int)(100.0 * current / DivisionNumber);
    //        toolStripStatusLabel2.Text = message;
    //        toolStripStatusLabel1.Text = "Ellapsed time : " + sec.ToString("f2") + " s.,  time/pixel: ";
    //        toolStripStatusLabel1.Text += sec / current > 0.9 ? $"{sec / current:f2} s.,  " : $"{sec / current * 1000:f2} ms., ";
    //        toolStripStatusLabel1.Text += $"{100.0 * current / DivisionNumber:f1} % completed,  wait for {sec * (DivisionNumber - current) / current:f2} s.";
    //    }
    //    Application.DoEvents();
    //    skipProgressChangedEvent = false;
    //}
    #endregion

    #region お蔵入り
    //private void Bethe_EBSD_Completed(object sender, RunWorkerCompletedEventArgs e)
    //{
    //    buttonStop.Visible = false;
    //    Crystal.Bethe.EBSD_Completed -= Bethe_EBSD_Completed;
    //    Crystal.Bethe.EBSD_ProgressChanged -= Bethe_EBSD_ProgressChanged;
    //    sw2.Stop();
    //    var sec1 = sw1.ElapsedMilliseconds / 1000.0;
    //    var sec2 = sw2.ElapsedMilliseconds / 1000.0;

    //    if (!e.Cancelled)
    //    {
    //        toolStripStatusLabel2.Text = "100% completed!  ";
    //        toolStripStatusLabel1.Text = $"Total time: {sec1 + sec2:f2} s.   ";
    //        toolStripStatusLabel1.Text += $"Bloch problem: {sec1:f2} s. (";
    //        toolStripStatusLabel1.Text += sec1 / DivisionNumber > 1 ? $"{sec1 / DivisionNumber:f2} s " : $"{sec1 / DivisionNumber * 1000:f2} ms ";
    //        toolStripStatusLabel1.Text += $"/pixes).   Compiling disks: {sec2:f2} s.";

    //        groupBoxOutput.Enabled = true;
    //        generateImage();
    //    }
    //    else
    //    {
    //        toolStripStatusLabel1.Text = "Time ellapsed: " + (sec1 + sec2).ToString("f2") + " sec.,  Manually interupted.";
    //        groupBoxOutput.Enabled = false;
    //    }
    //    Application.DoEvents();
    //}
    #endregion

    #endregion

    #endregion

    #region EBSD計算後、画像を生成

    #region　お蔵入り
    //private void generateImage(bool resetDisks = true)
    //{
    //    if (Crystal.Bethe.Disks == null || trackBarOutputEnergy.Value >= Crystal.Bethe.Disks.Length || trackBarOutputThickness.Value >= Crystal.Bethe.Disks[0].Length)
    //        return;

    //    var disk = Crystal.Bethe.Disks[trackBarOutputEnergy.Value][trackBarOutputThickness.Value];
    //    Pbmp = new PseudoBitmap(disk.Amplitudes.Select(e => e.MagnitudeSquared()).ToArray(), numericBoxDiskDiameter.ValueInteger) { AlphaEnabled = true };

    //    Pbmp.FilterAlfha = Pbmp.SrcValuesGrayOriginal.Select(e => e == 0 ? (byte)0 : (byte)255).ToList();

    //    AdjustImage();

    //}
    #endregion

    private void AdjustImage()
    {
        if (Pbmp == null)
            return;
        var colorScale = comboBoxScale.SelectedIndex;
        //GrayかColorか
        if (colorScale == 0)
            Pbmp.SetScaleGray();
        else if (colorScale == 1)
            Pbmp.SetScaleColdWarm();
        else if (colorScale == 2)
            Pbmp.SetScaleSpectrum();
        else
            Pbmp.SetScaleFire();

        //Negativeかどうか
        Pbmp.IsNegative = comboBoxGradient.SelectedIndex == 1;

        var maxRatio = (double)trackBarIntensityBrightnessMax.Value / trackBarIntensityBrightnessMax.Maximum;
        var minRatio = (double)trackBarIntensityBrightnessMin.Value / trackBarIntensityBrightnessMin.Maximum;

        double max = Pbmp.SrcValuesGray.Max(), min = Pbmp.SrcValuesGray.Min(), dev = max - min;

        Pbmp.MaxValue = dev * maxRatio + min;
        Pbmp.MinValue = dev * minRatio + min;

        // Draw(); // (260331Ch) 旧実装: DrawEBSD -> AdjustImage -> DrawEBSD の再入を招きやすかった
        DrawKikuchiLine();
        DrawGeometry();
        Application.DoEvents();
    }


    #endregion

    #region 画像出力パラメータのイベント

    private void TrackBarOutputThickness_Scroll(object sender, EventArgs e)
    {
        textBoxThickness.Text = ThicknessArray[trackBarOutputThickness.Value].ToString();
        Draw();
    }
    private void trackBarOutputEnergy_ValueChanged(object sender, EventArgs e)
    {
        textBoxEnergy.Text = EnergyArray[trackBarOutputEnergy.Value].ToString();
        Draw();
    }
    private void trackBarIntensityBrightnessMax_ValueChanged(object sender, EventArgs e) => AdjustImage();

    #endregion

    private void buttonSaveImage_Click(object sender, EventArgs e)
    {
        if (Pbmp != null)
            Clipboard.SetDataObject(Pbmp.GetImage());
    }

    #region MasterPattern から EBSD パターンを生成 // 260325Cl 追加

    /// <summary>
    /// MasterPattern から単一 energy/depth の EBSD パターンを生成する。260325Cl 追加
    /// groupBoxOutput 内の trackBarOutputEnergy / trackBarOutputThickness で選択されたスライスを使用。
    /// </summary>
    private void buttonGenerateEBSDFromMaster_Click(object sender, EventArgs e)
    {
        DrawEBSD();
    }

    bool skipEBSD_Rendering = false;
    private double[] ebsdValues = []; // 260325Cl: EBSD パターン描画用バッファ (サイズ変更時のみ再割り当て)
    private int ebsdCachedWidth = 0, ebsdCachedHeight = 0; // 260325Cl: PseudoBitmap 再生成判定用
    private int masterPatternCombinationModel = 2; // (260325Ch) 0=current, 1=globally normalized master, 2=absolute MC x differential master
    private double[] masterPatternGlobalNormalizationFactors = []; // (260325Ch) model 1 用。各 energy/depth slice の全球積算強度を 1 にそろえる係数
    private MasterPattern masterPatternGlobalNormalizationSource = null; // (260325Ch) 現在の model 1 規格化係数が対応している MasterPattern

    // 260325Cl 追加: ピクセルごとの MasterPattern 参照テーブル (エネルギー・深さに依存しない)
    // 正方格子: idx[i] = 左上グリッドインデックス, wt[i*2] = fw, wt[i*2+1] = fh, posZ[i] = 半球
    // 六方格子: idx[i*3..i*3+2] = 3近傍インデックス, wt[i*3..i*3+2] = バリセントリック重み (260331Cl)
    private int[] ebsdLookupIdx = [];
    private float[] ebsdLookupWt = [];
    private bool[] ebsdLookupPosZ = [];
    private int ebsdLookupGridSize; // 260325Cl: Apply で idx+gridSize の復元に使用
    private MasterPattern.Types ebsdLookupGridType; // 260331Cl: 六方格子モードかどうか

    // 260325Cl 追加: DetTilt/SmpTilt 由来の回転係数キャッシュ (tilt 変更時のみ再計算)
    private double ebsdYCoeffPy, ebsdZCoeffPy, ebsdYConst, ebsdZConst;

    /// <summary>DetTilt/SmpTilt から回転係数を再計算する。260325Cl 追加</summary>
    private void UpdateEbsdTiltCoeffs()
    {
        var (sinDet, cosDet) = Math.SinCos(DetTilt);
        var (sinSmp, cosSmp) = Math.SinCos(SmpTilt);
        ebsdYCoeffPy = cosSmp * cosDet + sinSmp * sinDet;
        ebsdZCoeffPy = -sinSmp * cosDet + cosSmp * sinDet;
        ebsdYConst = cosSmp * DetY + sinSmp * DetZ;
        ebsdZConst = -sinSmp * DetY + cosSmp * DetZ;
    }

    /// <summary>
    /// 検出器ジオメトリと結晶方位から、ピクセルごとの MasterPattern 参照テーブルを構築する。260325Cl 追加
    /// エネルギー・深さに依存しないため、畳み込み時は 1 回だけ呼べばよい。
    /// </summary>
    private unsafe void BuildEbsdLookupTable(int width, int height) // 260325Cl: unsafe 化
    {
        var totalPixels = width * height;
        ebsdLookupGridType = MasterPattern.GridType; // 260331Cl
        var isHexGrid = ebsdLookupGridType == MasterPattern.Types.Hexagonal; // 260331Cl

        // 260331Cl: 六方格子は 3 idx + 3 wt/pixel、正方格子は 1 idx + 2 wt/pixel
        var idxCount = isHexGrid ? totalPixels * 3 : totalPixels;
        var wtCount = isHexGrid ? totalPixels * 3 : totalPixels * 2;
        if (ebsdLookupIdx.Length != idxCount)
        {
            ebsdLookupIdx = new int[idxCount];
            ebsdLookupWt = new float[wtCount];
            ebsdLookupPosZ = new bool[totalPixels];
        }

        // 260325Cl: tilt 係数はキャッシュ済み (UpdateEbsdTiltCoeffs で更新)
        var yCoeffPy = ebsdYCoeffPy;
        var zCoeffPy = ebsdZCoeffPy;
        var yConst = ebsdYConst;
        var zConst = ebsdZConst;

        var Ri = Crystal.RotationMatrix.Inverse();
        double ax = -Ri.E11, ay = -Ri.E21, az = -Ri.E31;
        double bx = Ri.E12 * yCoeffPy + Ri.E13 * zCoeffPy;
        double by = Ri.E22 * yCoeffPy + Ri.E23 * zCoeffPy;
        double bz = Ri.E32 * yCoeffPy + Ri.E33 * zCoeffPy;
        double cx = Ri.E12 * yConst + Ri.E13 * zConst;
        double cy = Ri.E22 * yConst + Ri.E23 * zConst;
        double cz = Ri.E32 * yConst + Ri.E33 * zConst;

        double scaleW = DetR / width, scaleH = DetR / height;
        double ax2 = ax * scaleW, ay2 = ay * scaleW, az2 = az * scaleW;
        double bx2 = bx * scaleH, by2 = by * scaleH, bz2 = bz * scaleH;

        var gridSize = MasterPattern.GridSize;
        ebsdLookupGridSize = gridSize; // 260325Cl: Apply 用にキャッシュ
        var startPxFactor = 1 - width; // (260325Ch) 列方向は等間隔なので、旧 pxFactor 再計算を増分更新へ置き換える
        var dxStep = 2.0 * ax2; // (260325Ch)
        var dyStep = 2.0 * ay2; // (260325Ch)
        var dzStep = 2.0 * az2; // (260325Ch)

        fixed (int* pIdx = ebsdLookupIdx)
        fixed (float* pWt = ebsdLookupWt)
        fixed (bool* pPosZ = ebsdLookupPosZ)
        {
            var pIdx0 = pIdx; var pWt0 = pWt; var pPosZ0 = pPosZ;

            if (isHexGrid) // 260331Cl: 六方格子パス
            {
                var gs = gridSize; // Parallel.For のキャプチャ用ローカル
                System.Threading.Tasks.Parallel.For(0, height, h =>
                {
                    double pyFactor = 2 * h + 1 - height;
                    double rowBx = bx2 * pyFactor + cx, rowBy = by2 * pyFactor + cy, rowBz = bz2 * pyFactor + cz;
                    int rowOffset = h * width;
                    double dx = ax2 * startPxFactor + rowBx, dy = ay2 * startPxFactor + rowBy, dz = az2 * startPxFactor + rowBz;

                    for (int w = 0; w < width; w++)
                    {
                        int i = rowOffset + w;
                        double invLen = 1.0 / Math.Sqrt(dx * dx + dy * dy + dz * dz);
                        double nx = dx * invLen, ny = dy * invLen, nz = dz * invLen;
                        pPosZ0[i] = nz >= 0;

                        // 球面→六方格子
                        var (hx, hy) = MasterPattern.SphereToRoscaLambertHex(nx, ny, Math.Abs(nz));
                        MasterPattern.GetHexBarycentricLookup(hx, hy, gs,
                            out int idx0, out int idx1, out int idx2,
                            out float bw0, out float bw1, out float bw2);

                        int i3 = i * 3;
                        pIdx0[i3] = idx0; pIdx0[i3 + 1] = idx1; pIdx0[i3 + 2] = idx2;
                        pWt0[i3] = bw0; pWt0[i3 + 1] = bw1; pWt0[i3 + 2] = bw2;

                        dx += dxStep; dy += dyStep; dz += dzStep;
                    }
                });
            }
            else // 正方格子パス (既存)
            {
                var sqLim = MasterPattern.SquareLimit;
                var invStep = gridSize / (2.0 * sqLim);
                var inv_PI = 1.0 / Math.PI;
                var gridMax = gridSize - 1;
                var roscaRadiusScale = Math.PI * 0.5;

                System.Threading.Tasks.Parallel.For(0, height, h =>
                {
                    double pyFactor = 2 * h + 1 - height;
                    double rowBx = bx2 * pyFactor + cx, rowBy = by2 * pyFactor + cy, rowBz = bz2 * pyFactor + cz;
                    int rowOffset = h * width;
                    double dx = ax2 * startPxFactor + rowBx, dy = ay2 * startPxFactor + rowBy, dz = az2 * startPxFactor + rowBz;

                    for (int w = 0; w < width; w++)
                    {
                        int i = rowOffset + w;
                        double absDx = Math.Abs(dx), absDy = Math.Abs(dy);
                        double invLen = 1.0 / Math.Sqrt(dx * dx + dy * dy + dz * dz);
                        double absDzNorm = Math.Abs(dz) * invLen;
                        pPosZ0[i] = dz >= 0;

                        double a, b;
                        if (absDx < 1e-15 && absDy < 1e-15) { a = 0; b = 0; }
                        else
                        {
                            double edgeRadius = Math.Sqrt(Math.Max(0.0, roscaRadiusScale * (1.0 - absDzNorm)));
                            if (absDx >= absDy)
                            {
                                a = dx >= 0 ? edgeRadius : -edgeRadius;
                                b = 4.0 * a * inv_PI * Math.Atan(dy / dx);
                            }
                            else
                            {
                                b = dy >= 0 ? edgeRadius : -edgeRadius;
                                a = 4.0 * b * inv_PI * Math.Atan(dx / dy);
                            }
                        }

                        double gw = (a + sqLim) * invStep - 0.5, gh = (sqLim - b) * invStep - 0.5;
                        int w0 = (int)Math.Floor(gw), h0 = (int)Math.Floor(gh);
                        double fw = gw - w0, fh = gh - h0;
                        if (w0 < 0) { w0 = 0; fw = 0; } else if (w0 >= gridMax) { w0 = gridMax - 1; fw = 1; }
                        if (h0 < 0) { h0 = 0; fh = 0; } else if (h0 >= gridMax) { h0 = gridMax - 1; fh = 1; }

                        pIdx0[i] = h0 * gridSize + w0;
                        int i2 = i * 2;
                        pWt0[i2] = (float)fw;
                        pWt0[i2 + 1] = (float)fh;

                        dx += dxStep; dy += dyStep; dz += dzStep;
                    }
                });
            }
        }
    }

    /// <summary>構築済みルックアップテーブルを使い、指定 energy/depth の EBSD パターンを values に書き込む。260325Cl 追加</summary>
    private unsafe void ApplyEbsdLookupSingleSlice(double[] values, int totalPixels, float[] posPlane, float[] negPlane) // 260325Cl: unsafe 化
    {
        if (posPlane == null && negPlane == null) return;

        var gs = ebsdLookupGridSize;
        var isHexGrid = ebsdLookupGridType == MasterPattern.Types.Hexagonal; // 260331Cl

        fixed (int* pIdx = ebsdLookupIdx)
        fixed (float* pWt = ebsdLookupWt)
        fixed (bool* pPosZ = ebsdLookupPosZ)
        fixed (double* pVal = values)
        fixed (float* pPos = posPlane ?? Array.Empty<float>())
        fixed (float* pNeg = negPlane ?? Array.Empty<float>())
        {
            var pIdx0 = pIdx; var pWt0 = pWt; var pPosZ0 = pPosZ;
            var pVal0 = pVal; var pPos0 = pPos; var pNeg0 = pNeg;
            var hasPos = posPlane != null && posPlane.Length > 0;
            var hasNeg = negPlane != null && negPlane.Length > 0;

            if (isHexGrid) // 260331Cl: 六方格子パス — 3 近傍バリセントリック補間
            {
                System.Threading.Tasks.Parallel.For(0, totalPixels, i =>
                {
                    float* plane = pPosZ0[i] ? pPos0 : pNeg0;
                    bool hasPlane = pPosZ0[i] ? hasPos : hasNeg;
                    if (!hasPlane) { pVal0[i] = 0; return; }
                    int i3 = i * 3;
                    pVal0[i] = pWt0[i3] * plane[pIdx0[i3]]
                             + pWt0[i3 + 1] * plane[pIdx0[i3 + 1]]
                             + pWt0[i3 + 2] * plane[pIdx0[i3 + 2]];
                });
            }
            else // 正方格子パス (既存)
            {
                System.Threading.Tasks.Parallel.For(0, totalPixels, i =>
                {
                    float* plane = pPosZ0[i] ? pPos0 : pNeg0;
                    bool hasPlane = pPosZ0[i] ? hasPos : hasNeg;
                    if (!hasPlane) { pVal0[i] = 0; return; }
                    int idx = pIdx0[i];
                    int i2 = i * 2;
                    float fw = pWt0[i2], fh = pWt0[i2 + 1];
                    float w0 = (1 - fw), w1 = fw;
                    pVal0[i] = (w0 * plane[idx] + w1 * plane[idx + 1]) * (1 - fh)
                             + (w0 * plane[idx + gs] + w1 * plane[idx + gs + 1]) * fh;
                });
            }
        }
    }

    /// <summary>model 1 用に、各 energy/depth slice の全球積算強度 ((+Z) + (-Z)) を 1 にそろえる係数を準備する。260325Ch 追加</summary>
    private void EnsureMasterPatternGlobalNormalizationFactors()
    {
        var mp = MasterPattern;
        if (mp == null)
        {
            masterPatternGlobalNormalizationFactors = [];
            masterPatternGlobalNormalizationSource = null;
            return;
        }

        if (ReferenceEquals(masterPatternGlobalNormalizationSource, mp)
            && masterPatternGlobalNormalizationFactors.Length == mp.PlaneCount)
            return;

        var factors = new double[mp.PlaneCount];
        for (int planeIndex = 0; planeIndex < mp.PlaneCount; planeIndex++)
        {
            double globalSum = 0.0;

            var positivePlane = (uint)planeIndex < (uint)mp.PositivePlanes.Length ? mp.PositivePlanes[planeIndex] : null;
            if (positivePlane != null)
                for (int i = 0; i < positivePlane.Length; i++)
                    globalSum += positivePlane[i];

            var negativePlane = (uint)planeIndex < (uint)mp.NegativePlanes.Length ? mp.NegativePlanes[planeIndex] : null;
            if (negativePlane != null)
                for (int i = 0; i < negativePlane.Length; i++)
                    globalSum += negativePlane[i];

            factors[planeIndex] = globalSum > 1e-30 ? 1.0 / globalSum : 0.0; // (260325Ch) 全球積算強度が 0 の slice は 0 扱いにする
        }

        masterPatternGlobalNormalizationFactors = factors;
        masterPatternGlobalNormalizationSource = mp;
    }

    /// <summary>model 1: 各 energy/depth slice の全球積算強度を 1 にそろえてから、単一スライスの EBSD パターンを描く。260325Ch 追加</summary>
    private unsafe void ApplyEbsdLookupSingleSliceModel1(double[] values, int totalPixels, float[] posPlane, float[] negPlane, double planeScaleFactor = 1.0)
    {
        if (posPlane == null && negPlane == null) return;

        var gs = ebsdLookupGridSize;
        var scaleFactor = planeScaleFactor;
        var isHexGrid = ebsdLookupGridType == MasterPattern.Types.Hexagonal; // 260331Cl

        fixed (int* pIdx = ebsdLookupIdx)
        fixed (float* pWt = ebsdLookupWt)
        fixed (bool* pPosZ = ebsdLookupPosZ)
        fixed (double* pVal = values)
        fixed (float* pPos = posPlane ?? Array.Empty<float>())
        fixed (float* pNeg = negPlane ?? Array.Empty<float>())
        {
            var pIdx0 = pIdx; var pWt0 = pWt; var pPosZ0 = pPosZ;
            var pVal0 = pVal; var pPos0 = pPos; var pNeg0 = pNeg;
            var hasPos = posPlane != null && posPlane.Length > 0;
            var hasNeg = negPlane != null && negPlane.Length > 0;

            if (isHexGrid) // 260331Cl
            {
                System.Threading.Tasks.Parallel.For(0, totalPixels, i =>
                {
                    float* plane = pPosZ0[i] ? pPos0 : pNeg0;
                    bool hasPlane = pPosZ0[i] ? hasPos : hasNeg;
                    if (!hasPlane) { pVal0[i] = 0; return; }
                    int i3 = i * 3;
                    pVal0[i] = scaleFactor * (pWt0[i3] * plane[pIdx0[i3]]
                             + pWt0[i3 + 1] * plane[pIdx0[i3 + 1]]
                             + pWt0[i3 + 2] * plane[pIdx0[i3 + 2]]);
                });
            }
            else
            {
                System.Threading.Tasks.Parallel.For(0, totalPixels, i =>
                {
                    float* plane = pPosZ0[i] ? pPos0 : pNeg0;
                    bool hasPlane = pPosZ0[i] ? hasPos : hasNeg;
                    if (!hasPlane) { pVal0[i] = 0; return; }
                    int idx = pIdx0[i];
                    int i2 = i * 2;
                    float fw = pWt0[i2], fh = pWt0[i2 + 1];
                    float w0 = (1 - fw), w1 = fw;
                    pVal0[i] = scaleFactor * ((w0 * plane[idx] + w1 * plane[idx + 1]) * (1 - fh)
                             + (w0 * plane[idx + gs] + w1 * plane[idx + gs + 1]) * fh);
                });
            }
        }
    }

    /// <summary>model 2: depthIndex と depthIndex-1 の差分を取り、単一 depth slice の EBSD パターンとして描く。260325Ch 追加</summary>
    private unsafe void ApplyEbsdLookupSingleSliceModel2(double[] values, int totalPixels, float[] posPlane, float[] negPlane, float[] posPlanePrevious = null, float[] negPlanePrevious = null)
    {
        if (posPlane == null && negPlane == null) return;

        var gs = ebsdLookupGridSize;
        var isHexGrid = ebsdLookupGridType == MasterPattern.Types.Hexagonal; // 260331Cl

        fixed (int* pIdx = ebsdLookupIdx)
        fixed (float* pWt = ebsdLookupWt)
        fixed (bool* pPosZ = ebsdLookupPosZ)
        fixed (double* pVal = values)
        fixed (float* pPos = posPlane ?? Array.Empty<float>())
        fixed (float* pNeg = negPlane ?? Array.Empty<float>())
        fixed (float* pPosPrev = posPlanePrevious ?? Array.Empty<float>())
        fixed (float* pNegPrev = negPlanePrevious ?? Array.Empty<float>())
        {
            var pIdx0 = pIdx; var pWt0 = pWt; var pPosZ0 = pPosZ;
            var pVal0 = pVal; var pPos0 = pPos; var pNeg0 = pNeg; var pPosPrev0 = pPosPrev; var pNegPrev0 = pNegPrev;
            var hasPos = posPlane != null && posPlane.Length > 0;
            var hasNeg = negPlane != null && negPlane.Length > 0;
            var hasPosPrev = posPlanePrevious != null && posPlanePrevious.Length > 0;
            var hasNegPrev = negPlanePrevious != null && negPlanePrevious.Length > 0;

            if (isHexGrid) // 260331Cl
            {
                System.Threading.Tasks.Parallel.For(0, totalPixels, i =>
                {
                    float* plane = pPosZ0[i] ? pPos0 : pNeg0;
                    float* planePrev = pPosZ0[i] ? pPosPrev0 : pNegPrev0;
                    bool hasPlane = pPosZ0[i] ? hasPos : hasNeg;
                    bool hasPlanePrev = pPosZ0[i] ? hasPosPrev : hasNegPrev;
                    if (!hasPlane) { pVal0[i] = 0; return; }
                    int i3 = i * 3;
                    double intensity = pWt0[i3] * plane[pIdx0[i3]]
                                     + pWt0[i3 + 1] * plane[pIdx0[i3 + 1]]
                                     + pWt0[i3 + 2] * plane[pIdx0[i3 + 2]];
                    if (hasPlanePrev)
                        intensity -= pWt0[i3] * planePrev[pIdx0[i3]]
                                   + pWt0[i3 + 1] * planePrev[pIdx0[i3 + 1]]
                                   + pWt0[i3 + 2] * planePrev[pIdx0[i3 + 2]];
                    pVal0[i] = Math.Max(0.0, intensity);
                });
            }
            else
            {
                System.Threading.Tasks.Parallel.For(0, totalPixels, i =>
                {
                    float* plane = pPosZ0[i] ? pPos0 : pNeg0;
                    float* planePrev = pPosZ0[i] ? pPosPrev0 : pNegPrev0;
                    bool hasPlane = pPosZ0[i] ? hasPos : hasNeg;
                    bool hasPlanePrev = pPosZ0[i] ? hasPosPrev : hasNegPrev;
                    if (!hasPlane) { pVal0[i] = 0; return; }
                    int idx = pIdx0[i];
                    int i2 = i * 2;
                    float fw = pWt0[i2], fh = pWt0[i2 + 1];
                    float w0 = (1 - fw), w1 = fw;
                    double intensity = (w0 * plane[idx] + w1 * plane[idx + 1]) * (1 - fh)
                                     + (w0 * plane[idx + gs] + w1 * plane[idx + gs + 1]) * fh;
                    if (hasPlanePrev)
                        intensity -= (w0 * planePrev[idx] + w1 * planePrev[idx + 1]) * (1 - fh)
                                   + (w0 * planePrev[idx + gs] + w1 * planePrev[idx + gs + 1]) * fh;
                    pVal0[i] = Math.Max(0.0, intensity);
                });
            }
        }
    }

    /// <summary>
    /// 構築済みルックアップテーブルと MC フィッティング結果を使い、
    /// 全エネルギー・深さの加重平均 EBSD パターンを計算する。260325Cl 追加
    /// </summary>
    private unsafe void ApplyEbsdLookupWeighted(double[] values, int width, int height)
    {
        var mp = MasterPattern;
        var dist = mcDistribution;
        int eLen = mp.Energies.Length, dLen = mp.Depths.Length;
        int totalPixels = width * height;
        int binCount = dist.BinCount;
        var gs = ebsdLookupGridSize;

        Array.Clear(values);

        // ピクセルごとの加重合計を並列で計算
        fixed (int* pIdx = ebsdLookupIdx)
        fixed (float* pWt = ebsdLookupWt)
        fixed (bool* pPosZ = ebsdLookupPosZ)
        fixed (double* pVal = values)
        {
            var pIdx0 = pIdx; var pWt0 = pWt; var pPosZ0 = pPosZ; var pVal0 = pVal;
            var isHexGrid = ebsdLookupGridType == MasterPattern.Types.Hexagonal; // 260331Cl

            System.Threading.Tasks.Parallel.For(0, height, h =>
            {
                // この行のピクセルの検出器 Y 座標 (ビン補間用)
                // 260325Cl: スクリーン h=0 → pyFactor≈-DetR (検出器底) → detNormY≈-1, 符号反転しない
                double detNormY = (2.0 * h + 1 - height) / (double)height;
                double by = (1 - detNormY) * 0.5 * binCount - 0.5;
                int bj0 = Math.Clamp((int)Math.Floor(by), 0, binCount - 2);
                double fy = Math.Clamp(by - bj0, 0, 1);

                for (int w = 0; w < width; w++)
                {
                    int i = h * width + w;

                    // 検出器 X 座標
                    double detNormX = -(2.0 * w + 1 - width) / (double)width; // 260325Cl: スクリーン X は検出器面 X と反転 (BuildEbsdLookupTable で -Ri.E11 を使用)
                    double bx = (detNormX + 1) * 0.5 * binCount - 0.5;
                    int bi0 = Math.Clamp((int)Math.Floor(bx), 0, binCount - 2);
                    double fx = Math.Clamp(bx - bi0, 0, 1);

                    // ビン重みのバイリニア補間係数
                    double c00 = (1 - fx) * (1 - fy);
                    double c10 = fx * (1 - fy);
                    double c01 = (1 - fx) * fy;
                    double c11 = fx * fy;

                    var bw00 = dist.BinWeights[bi0, bj0];
                    var bw10 = dist.BinWeights[bi0 + 1, bj0];
                    var bw01 = dist.BinWeights[bi0, bj0 + 1];
                    var bw11 = dist.BinWeights[bi0 + 1, bj0 + 1];

                    // ルックアップテーブルからマスターパターン補間パラメータ取得
                    bool posZ = pPosZ0[i];

                    // 全エネルギー・深さで加重合計
                    double sum = 0;

                    if (isHexGrid) // 260331Cl: 六方格子
                    {
                        int i3 = i * 3;
                        int hIdx0 = pIdx0[i3], hIdx1 = pIdx0[i3 + 1], hIdx2 = pIdx0[i3 + 2];
                        float hw0 = pWt0[i3], hw1 = pWt0[i3 + 1], hw2 = pWt0[i3 + 2];
                        for (int ei = 0; ei < eLen; ei++)
                            for (int di = 0; di < dLen; di++)
                            {
                                int wIdx = ei * dLen + di;
                                double weight = c00 * bw00[wIdx] + c10 * bw10[wIdx]
                                              + c01 * bw01[wIdx] + c11 * bw11[wIdx];
                                if (weight < 1e-15) continue;
                                var plane = posZ
                                    ? mp.GetPlane(MasterPattern.Hemisphere.PositiveZ, ei, di)
                                    : mp.GetPlane(MasterPattern.Hemisphere.NegativeZ, ei, di);
                                if (plane == null || plane.Length == 0) continue;
                                sum += weight * (hw0 * plane[hIdx0] + hw1 * plane[hIdx1] + hw2 * plane[hIdx2]);
                            }
                    }
                    else // 正方格子
                    {
                        int idx = pIdx0[i];
                        int i2 = i * 2;
                        float mpFw = pWt0[i2], mpFh = pWt0[i2 + 1];
                        float mpW0 = 1 - mpFw, mpW1 = mpFw;
                        float mpFh1 = 1 - mpFh;
                        for (int ei = 0; ei < eLen; ei++)
                            for (int di = 0; di < dLen; di++)
                            {
                                int wIdx = ei * dLen + di;
                                double weight = c00 * bw00[wIdx] + c10 * bw10[wIdx]
                                              + c01 * bw01[wIdx] + c11 * bw11[wIdx];
                                if (weight < 1e-15) continue;
                                var plane = posZ
                                    ? mp.GetPlane(MasterPattern.Hemisphere.PositiveZ, ei, di)
                                    : mp.GetPlane(MasterPattern.Hemisphere.NegativeZ, ei, di);
                                if (plane == null || plane.Length == 0) continue;
                                double intensity = (mpW0 * plane[idx] + mpW1 * plane[idx + 1]) * mpFh1
                                                 + (mpW0 * plane[idx + gs] + mpW1 * plane[idx + gs + 1]) * mpFh;
                                sum += weight * intensity;
                            }
                    }
                    pVal0[i] = sum;
                }
            });
        }
    }

    public void DrawEBSD()
    {
        if (MasterPattern == null)
            return;

        if (skipEBSD_Rendering) return;
        skipEBSD_Rendering = true;

        int width = graphicsBox.ClientRectangle.Width, height = graphicsBox.ClientRectangle.Height;
        if (width <= 0 || height <= 0) { skipEBSD_Rendering = false; return; }

        sw1.Restart();

        // Step 1: ルックアップテーブル構築 (方向計算 + Rosca-Lambert + 補間係数)
        BuildEbsdLookupTable(width, height);

        var totalPixels = width * height;
        if (ebsdValues.Length != totalPixels)
            ebsdValues = new double[totalPixels];

        string statusText;

        var useBseDistribution = checkBoxWithBSEDistribution.Checked && mcDistribution != null; // (260327Ch) チェック時だけ BSE 分布つき合成を表示する

        // 260325Cl: BSE 分布を使う場合は加重平均、そうでなければ単一スライス
        if (useBseDistribution)
        {
            // Step 2a: 全エネルギー・深さの加重平均パターン
            switch (masterPatternCombinationModel)
            {
                case 1:
                    EnsureMasterPatternGlobalNormalizationFactors();
                    ApplyEbsdLookupWeightedModel1(ebsdValues, width, height);
                    statusText = $"EBSD weighted pattern (model=1, globally normalized master, {MasterPattern.Energies.Length} energies × {MasterPattern.Depths.Length} depths), {sw1.ElapsedMilliseconds} ms."; // (260325Ch)
                    break;
                case 2:
                    ApplyEbsdLookupWeightedModel2(ebsdValues, width, height);
                    statusText = $"EBSD weighted pattern (model=2, absolute MC x differential master, {MasterPattern.Energies.Length} energies × {MasterPattern.Depths.Length} depths), {sw1.ElapsedMilliseconds} ms."; // (260325Ch)
                    break;
                default:
                    // ApplyEbsdLookupWeighted(ebsdValues, width, height); // (260325Ch) 旧実装
                    ApplyEbsdLookupWeighted(ebsdValues, width, height);
                    statusText = $"EBSD weighted pattern (model=0, current), {MasterPattern.Energies.Length} energies × {MasterPattern.Depths.Length} depths, {sw1.ElapsedMilliseconds} ms."; // (260325Ch)
                    break;
            }
        }
        else
        {
            // Step 2b: 単一スライス (従来動作)
            var energyIndex = trackBarOutputEnergy.Value;
            var depthIndex = trackBarOutputThickness.Value;
            if ((uint)energyIndex >= (uint)MasterPattern.Energies.Length || (uint)depthIndex >= (uint)MasterPattern.Depths.Length)
            {
                skipEBSD_Rendering = false;
                return;
            }

            var mp = MasterPattern;
            var posPlane = mp.GetPlane(MasterPattern.Hemisphere.PositiveZ, energyIndex, depthIndex);
            var negPlane = mp.GetPlane(MasterPattern.Hemisphere.NegativeZ, energyIndex, depthIndex);
            var energy = MasterPattern.Energies[energyIndex];
            var depth = MasterPattern.Depths[depthIndex];
            switch (masterPatternCombinationModel)
            {
                case 1:
                    EnsureMasterPatternGlobalNormalizationFactors();
                    var planeIndex = energyIndex * mp.Depths.Length + depthIndex; // (260325Ch) model 1 の規格化係数参照用
                    var planeScaleFactor = (uint)planeIndex < (uint)masterPatternGlobalNormalizationFactors.Length ? masterPatternGlobalNormalizationFactors[planeIndex] : 0.0; // (260325Ch)
                    ApplyEbsdLookupSingleSliceModel1(ebsdValues, totalPixels, posPlane, negPlane, planeScaleFactor);
                    statusText = $"EBSD from MasterPattern (model=1): E={energy:g} kV, depth={depth:g} nm, {sw1.ElapsedMilliseconds} ms."; // (260325Ch)
                    break;
                case 2:
                    var posPlanePrevious = depthIndex > 0 ? mp.GetPlane(MasterPattern.Hemisphere.PositiveZ, energyIndex, depthIndex - 1) : null; // (260325Ch)
                    var negPlanePrevious = depthIndex > 0 ? mp.GetPlane(MasterPattern.Hemisphere.NegativeZ, energyIndex, depthIndex - 1) : null; // (260325Ch)
                    ApplyEbsdLookupSingleSliceModel2(ebsdValues, totalPixels, posPlane, negPlane, posPlanePrevious, negPlanePrevious);
                    statusText = $"EBSD from MasterPattern (model=2): E={energy:g} kV, depth slice={depth:g} nm, {sw1.ElapsedMilliseconds} ms."; // (260325Ch)
                    break;
                default:
                    // ApplyEbsdLookupSingleSlice(ebsdValues, totalPixels, posPlane, negPlane); // (260325Ch) 旧実装
                    ApplyEbsdLookupSingleSlice(ebsdValues, totalPixels, posPlane, negPlane);
                    statusText = $"EBSD from MasterPattern (model=0): E={energy:g} kV, depth={depth:g} nm, {sw1.ElapsedMilliseconds} ms."; // (260325Ch)
                    break;
            }
        }

        // Step 3: 表示
        if (Pbmp == null || ebsdCachedWidth != width || ebsdCachedHeight != height)
        {
            Pbmp?.Dispose();
            Pbmp = new PseudoBitmap(ebsdValues, width) { AlphaEnabled = true };
            Pbmp.FilterAlfha = Enumerable.Repeat((byte)255, totalPixels).ToList();
            ebsdCachedWidth = width;
            ebsdCachedHeight = height;
            groupBoxOutput.Enabled = true;
        }
        else
        {
            Pbmp.SrcValuesGray = ebsdValues;
            Pbmp.SrcValuesGrayOriginal = ebsdValues;
        }

        AdjustImage();
        toolStripStatusLabel1.Text = statusText;

        skipEBSD_Rendering = false;
    }

    /// <summary>model 1: 各 energy/depth slice の全球積算強度を 1 にそろえてから weighted 合成する。260325Ch 追加</summary>
    private unsafe void ApplyEbsdLookupWeightedModel1(double[] values, int width, int height)
    {
        var mp = MasterPattern;
        var dist = mcDistribution;
        int eLen = mp.Energies.Length, dLen = mp.Depths.Length;
        int totalPixels = width * height;
        int binCount = dist.BinCount;
        var gs = ebsdLookupGridSize;
        var planeScaleFactors = masterPatternGlobalNormalizationFactors;

        Array.Clear(values);

        fixed (int* pIdx = ebsdLookupIdx)
        fixed (float* pWt = ebsdLookupWt)
        fixed (bool* pPosZ = ebsdLookupPosZ)
        fixed (double* pVal = values)
        {
            var pIdx0 = pIdx; var pWt0 = pWt; var pPosZ0 = pPosZ; var pVal0 = pVal;
            var isHexGrid = ebsdLookupGridType == MasterPattern.Types.Hexagonal; // 260331Cl

            System.Threading.Tasks.Parallel.For(0, height, h =>
            {
                double detNormY = (2.0 * h + 1 - height) / (double)height;
                double by = (1 - detNormY) * 0.5 * binCount - 0.5;
                int bj0 = Math.Clamp((int)Math.Floor(by), 0, binCount - 2);
                double fy = Math.Clamp(by - bj0, 0, 1);

                for (int w = 0; w < width; w++)
                {
                    int i = h * width + w;
                    double detNormX = -(2.0 * w + 1 - width) / (double)width;
                    double bx = (detNormX + 1) * 0.5 * binCount - 0.5;
                    int bi0 = Math.Clamp((int)Math.Floor(bx), 0, binCount - 2);
                    double fx = Math.Clamp(bx - bi0, 0, 1);

                    double c00 = (1 - fx) * (1 - fy);
                    double c10 = fx * (1 - fy);
                    double c01 = (1 - fx) * fy;
                    double c11 = fx * fy;

                    var bw00 = dist.BinWeights[bi0, bj0];
                    var bw10 = dist.BinWeights[bi0 + 1, bj0];
                    var bw01 = dist.BinWeights[bi0, bj0 + 1];
                    var bw11 = dist.BinWeights[bi0 + 1, bj0 + 1];
                    bool posZ = pPosZ0[i];

                    double sum = 0;
                    if (isHexGrid) // 260331Cl
                    {
                        int i3 = i * 3;
                        int hIdx0 = pIdx0[i3], hIdx1 = pIdx0[i3 + 1], hIdx2 = pIdx0[i3 + 2];
                        float hw0 = pWt0[i3], hw1 = pWt0[i3 + 1], hw2 = pWt0[i3 + 2];
                        for (int ei = 0; ei < eLen; ei++)
                            for (int di = 0; di < dLen; di++)
                            {
                                int wIdx = ei * dLen + di;
                                double weight = c00 * bw00[wIdx] + c10 * bw10[wIdx]
                                              + c01 * bw01[wIdx] + c11 * bw11[wIdx];
                                if (weight < 1e-15) continue;
                                double planeScaleFactor = (uint)wIdx < (uint)planeScaleFactors.Length ? planeScaleFactors[wIdx] : 0.0;
                                if (planeScaleFactor < 1e-30) continue;
                                var plane = posZ
                                    ? mp.GetPlane(MasterPattern.Hemisphere.PositiveZ, ei, di)
                                    : mp.GetPlane(MasterPattern.Hemisphere.NegativeZ, ei, di);
                                if (plane == null || plane.Length == 0) continue;
                                sum += weight * (hw0 * plane[hIdx0] + hw1 * plane[hIdx1] + hw2 * plane[hIdx2]) * planeScaleFactor;
                            }
                    }
                    else
                    {
                        int idx = pIdx0[i];
                        int i2 = i * 2;
                        float mpFw = pWt0[i2], mpFh = pWt0[i2 + 1];
                        float mpW0 = 1 - mpFw, mpW1 = mpFw;
                        float mpFh1 = 1 - mpFh;
                        for (int ei = 0; ei < eLen; ei++)
                            for (int di = 0; di < dLen; di++)
                            {
                                int wIdx = ei * dLen + di;
                                double weight = c00 * bw00[wIdx] + c10 * bw10[wIdx]
                                              + c01 * bw01[wIdx] + c11 * bw11[wIdx];
                                if (weight < 1e-15) continue;
                                double planeScaleFactor = (uint)wIdx < (uint)planeScaleFactors.Length ? planeScaleFactors[wIdx] : 0.0;
                                if (planeScaleFactor < 1e-30) continue;
                                var plane = posZ
                                    ? mp.GetPlane(MasterPattern.Hemisphere.PositiveZ, ei, di)
                                    : mp.GetPlane(MasterPattern.Hemisphere.NegativeZ, ei, di);
                                if (plane == null || plane.Length == 0) continue;
                                double intensity = (mpW0 * plane[idx] + mpW1 * plane[idx + 1]) * mpFh1
                                                 + (mpW0 * plane[idx + gs] + mpW1 * plane[idx + gs + 1]) * mpFh;
                                sum += weight * intensity * planeScaleFactor;
                            }
                    }
                    pVal0[i] = sum;
                }
            });
        }
    }

    /// <summary>model 2: absolute MC 重みと differential MasterPattern を掛け合わせて weighted 合成する。260325Ch 追加</summary>
    private unsafe void ApplyEbsdLookupWeightedModel2(double[] values, int width, int height)
    {
        var mp = MasterPattern;
        var dist = mcDistribution;
        int eLen = mp.Energies.Length, dLen = mp.Depths.Length;
        int totalPixels = width * height;
        int binCount = dist.BinCount;
        var gs = ebsdLookupGridSize;

        Array.Clear(values);

        fixed (int* pIdx = ebsdLookupIdx)
        fixed (float* pWt = ebsdLookupWt)
        fixed (bool* pPosZ = ebsdLookupPosZ)
        fixed (double* pVal = values)
        {
            var pIdx0 = pIdx; var pWt0 = pWt; var pPosZ0 = pPosZ; var pVal0 = pVal;
            var isHexGrid = ebsdLookupGridType == MasterPattern.Types.Hexagonal; // 260331Cl

            System.Threading.Tasks.Parallel.For(0, height, h =>
            {
                double detNormY = (2.0 * h + 1 - height) / (double)height;
                double by = (1 - detNormY) * 0.5 * binCount - 0.5;
                int bj0 = Math.Clamp((int)Math.Floor(by), 0, binCount - 2);
                double fy = Math.Clamp(by - bj0, 0, 1);

                for (int w = 0; w < width; w++)
                {
                    int i = h * width + w;
                    double detNormX = -(2.0 * w + 1 - width) / (double)width;
                    double bx = (detNormX + 1) * 0.5 * binCount - 0.5;
                    int bi0 = Math.Clamp((int)Math.Floor(bx), 0, binCount - 2);
                    double fx = Math.Clamp(bx - bi0, 0, 1);

                    double c00 = (1 - fx) * (1 - fy);
                    double c10 = fx * (1 - fy);
                    double c01 = (1 - fx) * fy;
                    double c11 = fx * fy;

                    var bw00 = dist.BinAbsoluteSliceWeights[bi0, bj0];
                    var bw10 = dist.BinAbsoluteSliceWeights[bi0 + 1, bj0];
                    var bw01 = dist.BinAbsoluteSliceWeights[bi0, bj0 + 1];
                    var bw11 = dist.BinAbsoluteSliceWeights[bi0 + 1, bj0 + 1];
                    bool posZ = pPosZ0[i];

                    double sum = 0;
                    if (isHexGrid) // 260331Cl
                    {
                        int i3 = i * 3;
                        int hIdx0 = pIdx0[i3], hIdx1 = pIdx0[i3 + 1], hIdx2 = pIdx0[i3 + 2];
                        float hw0 = pWt0[i3], hw1 = pWt0[i3 + 1], hw2 = pWt0[i3 + 2];
                        for (int ei = 0; ei < eLen; ei++)
                            for (int di = 0; di < dLen; di++)
                            {
                                int wIdx = ei * dLen + di;
                                double weight = c00 * bw00[wIdx] + c10 * bw10[wIdx]
                                              + c01 * bw01[wIdx] + c11 * bw11[wIdx];
                                if (weight < 1e-15) continue;
                                var plane = posZ
                                    ? mp.GetPlane(MasterPattern.Hemisphere.PositiveZ, ei, di)
                                    : mp.GetPlane(MasterPattern.Hemisphere.NegativeZ, ei, di);
                                if (plane == null || plane.Length == 0) continue;
                                var planePrevious = di > 0
                                    ? posZ
                                        ? mp.GetPlane(MasterPattern.Hemisphere.PositiveZ, ei, di - 1)
                                        : mp.GetPlane(MasterPattern.Hemisphere.NegativeZ, ei, di - 1)
                                    : null;
                                double intensity = hw0 * plane[hIdx0] + hw1 * plane[hIdx1] + hw2 * plane[hIdx2];
                                if (planePrevious != null && planePrevious.Length > 0)
                                    intensity -= hw0 * planePrevious[hIdx0] + hw1 * planePrevious[hIdx1] + hw2 * planePrevious[hIdx2];
                                sum += weight * Math.Max(0.0, intensity);
                            }
                    }
                    else
                    {
                        int idx = pIdx0[i];
                        int i2 = i * 2;
                        float mpFw = pWt0[i2], mpFh = pWt0[i2 + 1];
                        float mpW0 = 1 - mpFw, mpW1 = mpFw;
                        float mpFh1 = 1 - mpFh;
                        for (int ei = 0; ei < eLen; ei++)
                            for (int di = 0; di < dLen; di++)
                            {
                                int wIdx = ei * dLen + di;
                                double weight = c00 * bw00[wIdx] + c10 * bw10[wIdx]
                                              + c01 * bw01[wIdx] + c11 * bw11[wIdx];
                                if (weight < 1e-15) continue;
                                var plane = posZ
                                    ? mp.GetPlane(MasterPattern.Hemisphere.PositiveZ, ei, di)
                                    : mp.GetPlane(MasterPattern.Hemisphere.NegativeZ, ei, di);
                                if (plane == null || plane.Length == 0) continue;
                                var planePrevious = di > 0
                                    ? posZ
                                        ? mp.GetPlane(MasterPattern.Hemisphere.PositiveZ, ei, di - 1)
                                        : mp.GetPlane(MasterPattern.Hemisphere.NegativeZ, ei, di - 1)
                                    : null;
                                double intensity = (mpW0 * plane[idx] + mpW1 * plane[idx + 1]) * mpFh1
                                                 + (mpW0 * plane[idx + gs] + mpW1 * plane[idx + gs + 1]) * mpFh;
                                if (planePrevious != null && planePrevious.Length > 0)
                                    intensity -= (mpW0 * planePrevious[idx] + mpW1 * planePrevious[idx + 1]) * mpFh1
                                             + (mpW0 * planePrevious[idx + gs] + mpW1 * planePrevious[idx + gs + 1]) * mpFh;
                                sum += weight * Math.Max(0.0, intensity);
                            }
                    }
                    pVal0[i] = sum;
                }
            });
        }
    }

    #endregion

   

    #region グラフをコピー
    private void buttonCopyEnergyProfile_Click(object sender, EventArgs e)
    {
        if (graphControlEnergyProfile.Profile == null) return;

        var pt = graphControlEnergyProfile.Profile.Pt;
        var sb = new StringBuilder();
        for (int i = 0; i < pt.Count; i++)
            sb.AppendLine(pt[i].X + "\t" + pt[i].Y);

        Clipboard.SetDataObject(sb.ToString());
    }

    private void buttonDepthProfile_Click(object sender, EventArgs e)
    {
        if (graphControlDepthProfile.Profile == null) return;

        var pt = graphControlDepthProfile.Profile.Pt;
        var sb = new StringBuilder();
        for (int i = 0; i < pt.Count; i++)
            sb.AppendLine(pt[i].X + "\t" + pt[i].Y);

        Clipboard.SetDataObject(sb.ToString());
    }
    #endregion

    #region MasterPattern
    /// <summary>MasterPattern build 用に追加した進捗イベントを解除する。</summary>
    private void DetachMasterPatternBuildEvents()
    {
        masterPatternEbsd.MasterPatternProgressChanged -= MasterPattern_EBSD_ProgressChanged;
        masterPatternEbsd.MasterPatternCompleted -= MasterPattern_EBSD_Completed;
    }

    /// <summary>
    /// Calc BSE / MasterPattern 前段で共有する MC を実行し、エネルギー・深さ範囲を決定して
    /// numericBox を更新し、8×8 ビンのフィッティング結果を mcDistribution に保持する。260325Cl 追加
    /// </summary>
    private async Task<bool> RunMonteCarloAndSetRangesAsync(
        string statusPrefix = "MasterPattern",
        string progressInfoText = "Preparing MasterPattern by Monte Carlo...",
        string completedInfoText = "Monte Carlo finished. Starting MasterPattern build...",
        string noResultInfoText = "MasterPattern build was aborted because Monte Carlo returned no usable BSEs.")
    {
        var cry = FormMain.Crystal;
        cry.GetFormulaAndDensity();
        var sum1 = cry.Atoms.Sum(a => AtomStatic.AtomicWeight(a.AtomicNumber) * a.Multiplicity * a.AtomicNumber);
        var sum2 = cry.Atoms.Sum(a => AtomStatic.AtomicWeight(a.AtomicNumber) * a.Multiplicity);
        var sum3 = cry.Atoms.Sum(a => a.Multiplicity);
        double z = sum1 / sum2;
        double a = sum2 / sum3;
        double rho = cry.Density;
        var valenceElectronCount = MonteCarlo.EstimateAverageValenceElectronCount(
            cry.Atoms.Select(atom => (atom.AtomicNumber, AtomStatic.AtomicWeight(atom.AtomicNumber) * atom.Multiplicity))); // (260331Ch)
        double energy = Voltage;
        double sampleTilt = SmpTilt,detectorTilt = DetTilt;
        double detectorY = DetY,detectorZ = DetZ, detectorR = DetR;
        double energyThreshold = EnergyThreshold;
        var loop = BackscatterMonteCarloLoopCount;
        var sampleRotation = M3.CreateRotationX(sampleTilt);
        var monteCarloStopwatch = Stopwatch.StartNew();
        IProgress<(int Progress, string Message)> progress = new Progress<(int Progress, string Message)>(state =>
        {
            var progressValue = Math.Clamp(state.Progress, 0, 100);
            toolStripProgressBar.Value = progressValue;
            toolStripStatusLabel2.Text = $"{statusPrefix}: {state.Message}"; // (260401Ch) Calc BSE も同じ MC helper を共有する
            toolStripStatusLabel1.Text = $"{progressValue:f0}% completed, elapsed {monteCarloStopwatch.ElapsedMilliseconds / 1000.0:f2} s.";
            labelMasterPatternInfo.Text = $"{progressInfoText} {progressValue}%"; // (260401Ch)
        });

        progress.Report((0, "MonteCarlo"));
        var result = await Task.Run(() =>
        {
            var monte = new MonteCarlo(z, a, rho, energy, sampleTilt, energyThreshold,
                elasticScatteringModel: MonteCarlo.ElasticScatteringModels.MottNistSampler2023,
                inelasticScatteringModel: MonteCarlo.InelasticScatteringModels.DiscreteBulkDiimfpApproximation,
                valenceElectronCount: valenceElectronCount,
                atoms: cry.Atoms); // (260331Ch)
            var bses = RunBackscatterMonteCarlo(monte, loop, energyThreshold, sampleRotation, (completed, total) =>
                progress.Report(((int)Math.Round(90.0 * completed / total), "MonteCarlo"))); // (260327Ch) fitting 分を残して 90% まで使う
            if (bses.Length == 0)
                return (Bses: bses, Distribution: (EbsdMonteCarloDistribution)null, Energies: Array.Empty<double>(), Depths: Array.Empty<double>(),
                    energyStart: 0.0, energyEnd: 0.0, energyStep: 0.0, depthStart: 0.0, depthEnd: 0.0, depthStep: 0.0);

            progress.Report((92, "Analyzing Monte Carlo ranges"));
            var bseRaw = bses.Select(e => (
                monteCarloDistributionDepthMode == MonteCarloDistributionDepthMode.LastInelasticEventDepth && e.HasLastInelasticEvent
                    ? e.LastInelasticDepth
                    : e.Depth,
                e.Vec,
                e.Energy)).ToArray(); // (260331Ch) P(z_last_inelastic, Ω_exit, E_exit) と P(z_last_event, Ω_exit, E_exit) を切替
            var (energyLoss80, depth99) = EbsdMonteCarloDistribution.ComputeRangesFromMC(bseRaw, energy); // (260327Ch)
            var grid = EbsdMonteCarloDistribution.ComputeGridFromRanges(energy, energyLoss80, depth99); // (260327Ch)

            progress.Report((95, "Fitting Monte Carlo distribution"));
            var distribution = new EbsdMonteCarloDistribution(
                bseRaw, energy,
                sampleTilt, detectorTilt, detectorY, detectorZ, detectorR,
                grid.energies, grid.depths);
            return (Bses: bses, Distribution: distribution, Energies: grid.energies, Depths: grid.depths,
                energyStart: grid.energyStart, energyEnd: grid.energyEnd, energyStep: grid.energyStep,
                depthStart: grid.depthStart, depthEnd: grid.depthEnd, depthStep: grid.depthStep);
        });

        if (result.Bses == null || result.Bses.Length == 0)
        {
            masterPatternMonteCarloElapsedMilliseconds = monteCarloStopwatch.ElapsedMilliseconds; // (260327Ch)
            BSEs = [];
            mcDistribution = null;
            toolStripProgressBar.Value = 0;
            toolStripStatusLabel2.Text = $"{statusPrefix}: MonteCarlo"; // (260401Ch)
            toolStripStatusLabel1.Text = $"0% completed, elapsed {masterPatternMonteCarloElapsedMilliseconds / 1000.0:f2} s.";
            toolStripStatusLabel3.Text = "";
            labelMasterPatternInfo.Text = noResultInfoText; // (260401Ch)
            return false;
        }

        BSEs = result.Bses;
        numericBoxEnergyStart.Value = result.energyStart;
        numericBoxEnergyEnd.Value = result.energyEnd;
        numericBoxEnergyStep.Value = result.energyStep;
        numericBoxThicknessStart.Value = result.depthStart;
        numericBoxThicknessEnd.Value = result.depthEnd;
        numericBoxThicknessStep.Value = result.depthStep;
        mcDistribution = result.Distribution;

        poleFigureControl.DrawingMode = PoleFigureControl2.DrawingModeEnum.Histogram;
        var poleFigureRotation = M3.CreateRotationX(sampleTilt);
        poleFigureControl.Vectors = BSEs.Select(e => new V4(poleFigureRotation * e.Vec, e.Energy)).ToArray();
        CalcStatistics();

        masterPatternMonteCarloElapsedMilliseconds = monteCarloStopwatch.ElapsedMilliseconds; // (260327Ch) MC 本体と fitting、統計更新まで含めた時間
        toolStripProgressBar.Value = 100;
        toolStripStatusLabel2.Text = $"{statusPrefix}: MonteCarlo finished"; // (260401Ch)
        toolStripStatusLabel1.Text = $"100% completed, elapsed {masterPatternMonteCarloElapsedMilliseconds / 1000.0:f2} s.";
        labelMasterPatternInfo.Text = completedInfoText; // (260401Ch)
        return true;
    }

    /// <summary>
    /// UI 上の設定値を読み取り、MasterPattern の作成を開始する。
    /// 実際の計算本体は Crystallography.EBSD に委譲し、このメソッドでは UI の状態遷移だけを扱う。
    /// </summary>
    private async void buttonCreateMasterPattern_Click(object sender, EventArgs e)
    {
        #region お蔵入り // (260327Ch) 旧 bwEBSD 実行中チェックは ebsdNew 本命化に伴い退避
        //if (Crystal?.Bethe?.bwEBSD?.IsBusy == true)
        //{
        //    toolStripStatusLabel1.Text = "The regular EBSD solver is running. Wait for it to finish first.";
        //    return;
        //}
        #endregion
        if (masterPatternEbsd.IsBuilding)
            return;

        buttonCreateMasterPattern.Enabled = false; // (260327Ch) MC 前処理中の多重起動を防ぐ
        buttonStop.Visible = false; // (260327Ch) MC 前処理はまだ停止できないため、Bethe 開始まで出さない
        toolStripProgressBar.Value = 0;
        toolStripStatusLabel2.Text = "MasterPattern: MonteCarlo";
        toolStripStatusLabel1.Text = "0% completed, elapsed 0.00 s.";
        toolStripStatusLabel3.Text = ""; // (260327Ch) 前回の完了時間表示をクリア
        labelMasterPatternInfo.Text = "Preparing MasterPattern by Monte Carlo...";
        sw2.Restart(); // (260327Ch) MasterPattern 全体の経過時間
        masterPatternMonteCarloElapsedMilliseconds = 0;
        EBSD.MasterPatternBuildRequest request = null;

        try
        {
            // 260325Cl: まず MC を実行してレンジとフィッティングを決定
            if (!await RunMonteCarloAndSetRangesAsync())
            {
                buttonCreateMasterPattern.Enabled = true; // (260327Ch)
                return;
            }

            // var request = CreateMasterPatternBuildRequest(); // (260321Ch) 旧実装: request 生成を別 helper に切り出していた
            request = new EBSD.MasterPatternBuildRequest(
                Crystal,
                MaxNumOfBloch,
                EnergyArray,
                ThicknessArray,
                GetSelectedMasterPatternGridSize(),
                BetheMethod.Solver.Eigen_Eigen,
                32,
                checkBoxNonLocalAbsorption.Checked,
                checkBoxTDSBackground.Checked); // (260321Ch) UI 値をその場で request に束ねる
            masterPatternEbsd.MasterPatternProgressChanged -= MasterPattern_EBSD_ProgressChanged; // (260327Ch) 1 回しか使わない helper はインライン化
            masterPatternEbsd.MasterPatternCompleted -= MasterPattern_EBSD_Completed; // (260327Ch)
            masterPatternEbsd.MasterPatternProgressChanged += MasterPattern_EBSD_ProgressChanged; // (260327Ch)
            masterPatternEbsd.MasterPatternCompleted += MasterPattern_EBSD_Completed; // (260327Ch)
            if (!masterPatternEbsd.RunMasterPatternBuild(request))
            {
                DetachMasterPatternBuildEvents();
                buttonCreateMasterPattern.Enabled = true; // (260327Ch)
                return;
            }
        }
        catch
        {
            DetachMasterPatternBuildEvents();
            buttonCreateMasterPattern.Enabled = true; // (260327Ch)
            buttonStop.Visible = false; // (260327Ch)
            throw;
        }

        trackBarMasterPatternEnergy.Enabled = false;
        trackBarMasterPatternDepth.Enabled = false;
        buttonStop.Visible = true;
        toolStripProgressBar.Value = 0;
        toolStripStatusLabel2.Text = "MasterPattern: Starting Bethe calculation";
        // labelMasterPatternInfo.Text = $"Building {GetHemisphereText(request.Hemisphere)} master grid ({request.GridSize} x {request.GridSize})..."; // (260321Ch) 旧案: 単一半球計算を前提にしていた
        labelMasterPatternInfo.Text = $"Building full sphere master grid ({request.GridSize} x {request.GridSize})...";
        toolStripStatusLabel1.Text = "0% completed, elapsed 0.00 s."; // (260327Ch)
        sw1.Restart();
    }

    /// <summary>Crystallography.EBSD から届いた進捗を UI 表示へ反映する。</summary>
    private void MasterPattern_EBSD_ProgressChanged(object sender, EBSD.MasterPatternProgressChangedEventArgs e)
    {
        if (skipProgressChangedEvent)
            return; // (260327Ch) ProgressChanged 内の再入を止めて stack overflow を防ぐ

        skipProgressChangedEvent = true; // (260327Ch)
        try
        {
            var sec = sw1.ElapsedMilliseconds / 1000.0;
            var progress = Math.Clamp(e.ProgressPercentage, 0, 100);
            toolStripProgressBar.Value = progress;
            toolStripStatusLabel2.Text = $"MasterPattern: {e.UserState}";
            toolStripStatusLabel1.Text = $"{progress:f0}% completed, elapsed {sec:f2} s.";
            // labelMasterPatternInfo.Text = $"Building {GetHemisphereText(e.Request.Hemisphere)} master grid... {progress}%"; // (260321Ch) 旧案: 単一半球計算を前提にしていた
            labelMasterPatternInfo.Text = $"Building full sphere master grid... {progress}%";
            // Application.DoEvents(); // (260327Ch) ProgressChanged 再入の原因になるため停止
        }
        finally
        {
            skipProgressChangedEvent = false; // (260327Ch)
        }
    }

    /// <summary>Crystallography.EBSD 側の build 完了通知を受け、selector と preview を更新する。</summary>
    private void MasterPattern_EBSD_Completed(object sender, EBSD.MasterPatternCompletedEventArgs e)
    {
        DetachMasterPatternBuildEvents();
        buttonCreateMasterPattern.Enabled = true;
        buttonStop.Visible = false;
        var sec = sw1.ElapsedMilliseconds / 1000.0;

        if (e.Error != null)
        {
            toolStripStatusLabel2.Text = "MasterPattern failed";
            toolStripStatusLabel1.Text = $"Failed after {sw2.ElapsedMilliseconds / 1000.0:f2} s.";
            toolStripStatusLabel3.Text = "";
            labelMasterPatternInfo.Text = "MasterPattern build failed.";
            UpdateMasterPatternSelectors();
            DrawMasterPattern2D();
            return;
        }

        if (e.Cancelled || e.MasterPattern == null)
        {
            toolStripStatusLabel2.Text = "MasterPattern cancelled";
            toolStripStatusLabel1.Text = $"Stopped after {sw2.ElapsedMilliseconds / 1000.0:f2} s.";
            toolStripStatusLabel3.Text = "";
            labelMasterPatternInfo.Text = "MasterPattern build was cancelled.";
            UpdateMasterPatternSelectors();
            DrawMasterPattern2D();
            return;
        }

        UpdateMasterPatternSelectors();
        DrawMasterPattern2D();
        Draw(); // (260327Ch) 描画更新で他ラベルが書き換わる前に済ませ、最後に MasterPattern 用の status を上書きする
        toolStripProgressBar.Value = 100;
        toolStripStatusLabel2.Text = "MasterPattern completed";
        var totalSec = sw2.ElapsedMilliseconds / 1000.0;
        var monteCarloSec = masterPatternMonteCarloElapsedMilliseconds / 1000.0;
        toolStripStatusLabel1.Text = $"100% completed, elapsed {totalSec:f2} s.";
        toolStripStatusLabel3.Text = $"Total {totalSec:f2} s. (Monte Carlo {monteCarloSec:f2} s, MasterPattern {sec:f2} s, {e.Request.GridSize} x {e.Request.GridSize}, full sphere)";
        // labelMasterPatternInfo.Text = $"Ready: {GetHemisphereText(e.Request.Hemisphere)}, {MasterPattern?.Energies.Length ?? 0} energies, {MasterPattern?.Depths.Length ?? 0} depths."; // (260321Ch) 旧案
        labelMasterPatternInfo.Text = $"Ready: full sphere, {MasterPattern?.Energies.Length ?? 0} energies, {MasterPattern?.Depths.Length ?? 0} depths.";

        // 260325Cl 追加: MasterPattern 完了時に groupBoxOutput を有効化し、trackbar を同期する
        groupBoxOutput.Enabled = true;
        trackBarOutputEnergy.Maximum = Math.Max(0, MasterPattern.Energies.Length - 1);
        trackBarOutputThickness.Maximum = Math.Max(0, MasterPattern.Depths.Length - 1);
        trackBarOutputEnergy.Value = 0;
        trackBarOutputThickness.Value = 0;
        textBoxEnergy.Text = MasterPattern.Energies.Length > 0 ? MasterPattern.Energies[0].ToString() : "";
        textBoxThickness.Text = MasterPattern.Depths.Length > 0 ? MasterPattern.Depths[0].ToString() : "";

    }

    /// <summary>進行中の MasterPattern build を停止する。</summary>
    private void buttonStop_Click(object sender, EventArgs e)
    {
        if (masterPatternEbsd.IsBuilding)
        {
            masterPatternEbsd.CancelMasterPatternBuild();
            toolStripStatusLabel2.Text = "MasterPattern cancel requested";
            toolStripStatusLabel1.Text = $"{toolStripProgressBar.Value:f0}% completed, elapsed {sw2.ElapsedMilliseconds / 1000.0:f2} s.";
            return;
        }

        #region お蔵入り // (260327Ch) 旧 bwEBSD の停止 UI は ebsdNew 本命化に伴い退避
        //if (Crystal?.Bethe?.bwEBSD?.IsBusy == true)
        //{
        //    Crystal.Bethe.CancelEBSD();
        //    toolStripStatusLabel2.Text = "EBSD cancel requested";
        //    toolStripStatusLabel1.Text = "Stopping EBSD...";
        //    return;
        //}
        #endregion

        buttonStop.Visible = false;
    }

    /// <summary>作成済み MasterPattern の energy / depth selector を UI へ反映する。</summary>
    private void UpdateMasterPatternSelectors()
    {
        if (MasterPattern == null)
        {
            trackBarMasterPatternEnergy.Enabled = false;
            trackBarMasterPatternDepth.Enabled = false;
            textBoxMasterPatternEnergy.Text = "";
            textBoxMasterPatternDepth.Text = "";
            return;
        }

        trackBarMasterPatternEnergy.Minimum = 0;
        trackBarMasterPatternDepth.Minimum = 0;
        trackBarMasterPatternEnergy.Maximum = Math.Max(0, MasterPattern.Energies.Length - 1);
        trackBarMasterPatternDepth.Maximum = Math.Max(0, MasterPattern.Depths.Length - 1);
        trackBarMasterPatternEnergy.Enabled = MasterPattern.Energies.Length > 0;
        trackBarMasterPatternDepth.Enabled = MasterPattern.Depths.Length > 0;
        trackBarMasterPatternEnergy.Value = 0;
        trackBarMasterPatternDepth.Value = 0;
        UpdateMasterPatternSliceSelectorText(); // (260321Ch) trackbar と表示テキストを同期する
    }

    /// <summary>UI の grid selector から、MasterPattern の分割数を取得する。</summary>
    private int GetSelectedMasterPatternGridSize()
    {
        if (comboBoxMasterPatternGrid?.SelectedItem is object selectedItem
            && int.TryParse(selectedItem.ToString(), out var gridSize)
            && gridSize > 0)
            return gridSize;

        if (comboBoxMasterPatternGrid != null
            && int.TryParse(comboBoxMasterPatternGrid.Text, out gridSize)
            && gridSize > 0)
            return gridSize;

        return 256; // (260321Ch)
    }

    /// <summary>UI の hemisphere selector から、対応する enum 値を取得する。</summary>
    private MasterPattern.Hemisphere GetSelectedMasterPattern2DHemisphere()
        => comboBoxMasterPattern2DHemisphere.SelectedIndex == 1 ? MasterPattern.Hemisphere.NegativeZ : MasterPattern.Hemisphere.PositiveZ;

    /// <summary>現在の energy / depth trackbar の値を、表示用テキストへ反映する。</summary>
    private void UpdateMasterPatternSliceSelectorText()
    {
        if (MasterPattern == null)
        {
            textBoxMasterPatternEnergy.Text = "";
            textBoxMasterPatternDepth.Text = "";
            return;
        }

        textBoxMasterPatternEnergy.Text = trackBarMasterPatternEnergy.Enabled && trackBarMasterPatternEnergy.Value < MasterPattern.Energies.Length
            ? MasterPattern.Energies[trackBarMasterPatternEnergy.Value].ToString("g")
            : "";
        textBoxMasterPatternDepth.Text = trackBarMasterPatternDepth.Enabled && trackBarMasterPatternDepth.Value < MasterPattern.Depths.Length
            ? MasterPattern.Depths[trackBarMasterPatternDepth.Value].ToString("g")
            : ""; // (260321Ch)
    }

    /// <summary>hemisphere enum を UI 表示用の文字列へ変換する。</summary>
    private static string GetHemisphereText(MasterPattern.Hemisphere hemisphere)
        => hemisphere == MasterPattern.Hemisphere.PositiveZ ? "+Z hemisphere" : "-Z hemisphere";

    #endregion

    #region MasterPatternの二次元描画と3Dレンダリング
    #region MasterPattern2D

    /// <summary>preview 条件の selector が変化したときに表示を更新する。</summary>
    private void MasterPatternSelectionChanged(object sender, EventArgs e) // (260322Ch) 旧名: MasterPatternPreviewSelectionChanged
    {
        UpdateMasterPatternSliceSelectorText(); // (260321Ch) energy / depth の数値表示を先に更新する
        DrawMasterPattern2D();
    }

    /// <summary>現在選択されている MasterPattern slice を preview 画像へ変換して表示する。</summary>
    private void DrawMasterPattern2D() // (260322Ch) 旧名: DrawMasterPattern2DPreview
    {
        if (scalablePictureBoxAdvancedMasterPattern2D == null)
            return;

        if (MasterPattern == null)
        {
            int selectedGridSize = GetSelectedMasterPatternGridSize(); // (260322Ch) 未作成時も selector に合わせた正方格子サイズを使う
            labelMasterPatternInfo.Text = "MasterPattern preview is empty.";
            ResetMasterPattern3DCache(); // (260321Ch) build 前は 3D 再描画用のキャッシュも空にしておく
            // SetMasterPattern2DBitmap(CreateMasterPatternPlaceholderValues(GetSelectedMasterPatternGridSize()), GetSelectedMasterPatternGridSize()); // (260322Ch) 旧実装: helper が新規配列を返していた
            SetMasterPattern2DBitmap(new double[selectedGridSize * selectedGridSize], selectedGridSize); // (260322Ch) helper を介さず空の placeholder 配列をその場で生成する
            ClearMesh(); // (260321Ch) MasterPattern 未作成時の 3D preview は空にする
            return;
        }

        var selectedHemisphere = GetSelectedMasterPattern2DHemisphere();
        int selectedEnergyIndex = trackBarMasterPatternEnergy.Enabled ? trackBarMasterPatternEnergy.Value : -1;
        int selectedDepthIndex = trackBarMasterPatternDepth.Enabled ? trackBarMasterPatternDepth.Value : -1;
        if (selectedEnergyIndex < 0 || selectedDepthIndex < 0)
        {
            int gridSize = MasterPattern.GridSize; // (260322Ch)
            ResetMasterPattern3DCache(); // (260321Ch) selector 未選択時は古い 3D preview を残さない
            SetMasterPattern2DBitmap(new double[gridSize * gridSize], gridSize); // (260322Ch)
            ClearMesh(); // (260321Ch)
            return;
        }

        // var plane = MasterPattern.GetPlane(comboBoxMasterPatternEnergy.SelectedIndex, comboBoxMasterPatternDepth.SelectedIndex); // (260321Ch) 旧案: 単一半球前提の取得
        var plane = MasterPattern.GetPlane(selectedHemisphere, selectedEnergyIndex, selectedDepthIndex);
        if (plane == null || plane.Length == 0)
        {
            int gridSize = MasterPattern.GridSize; // (260322Ch)
            ResetMasterPattern3DCache(); // (260321Ch) plane が存在しないときは cached slice を破棄する
            SetMasterPattern2DBitmap(new double[gridSize * gridSize], gridSize); // (260322Ch)
            ClearMesh(); // (260321Ch)
            return;
        }

        // 260331Cl 2D: 六方格子座標系のまま表示、3D: 正方Lambert格子に変換して球面投影
        var gridType = MasterPattern.GridType;
        var displayPlane = gridType == MasterPattern.Types.Hexagonal ? MasterPattern.RenderHexPlaneToImage(plane, MasterPattern.GridSize) : plane;
        var posPlane = MasterPattern.GetPlane(MasterPattern.Hemisphere.PositiveZ, selectedEnergyIndex, selectedDepthIndex); // (260331Ch) enum 名の取り残しを合わせてビルドを通す
        var negPlane = MasterPattern.GetPlane(MasterPattern.Hemisphere.NegativeZ, selectedEnergyIndex, selectedDepthIndex); // (260331Ch)
        var displayValues = CreateMasterPatternDisplayValues(displayPlane, MasterPattern.GridSize); // 260331Cl 2D 表示用
        var positiveDisplayValues = CreateMasterPatternDisplayValues(posPlane, MasterPattern.GridSize); // 260331Cl 3D 球面用 (生データ)
        var negativeDisplayValues = CreateMasterPatternDisplayValues(negPlane, MasterPattern.GridSize); // 260331Cl 3D 球面用 (生データ)

        var energy = MasterPattern.Energies[selectedEnergyIndex];
        var depth = MasterPattern.Depths[selectedDepthIndex];
        labelMasterPatternInfo.Text = $"Preview: {GetHemisphereText(selectedHemisphere)}, E = {energy:g} kV, depth = {depth:g} nm";
        masterPattern2DValues = displayValues; // 260331Cl 2D 表示キャッシュ (六方格子座標)
        masterPattern3DValuesPositive = positiveDisplayValues; // 260331Cl 3D 球面キャッシュ
        masterPattern3DValuesNegative = negativeDisplayValues; // 260331Cl 3D 球面キャッシュ
        masterPattern3DCacheGridSize = MasterPattern.GridSize; // (260322Ch)
        masterPattern3DCacheGridType = gridType; // 260331Cl
        // SetMasterPattern2DBitmap(displayValues, MasterPattern.GridSize); // (260321Ch) 旧案: 2D preview のみ更新していた
        SetMasterPattern2DBitmap(displayValues, MasterPattern.GridSize); // (260322Ch)
        // DrawMasterPattern3D(displayValues, MasterPattern.GridSize, MasterPattern.Hemisphere, masterPattern2DBitmap); // (260321Ch) 旧案: 2D で選択した半球だけを 3D に描いていた
        RedrawMasterPattern3DFromCache(); // (260321Ch) 2D 用の見た目設定と同じカラースケールで 3D を描き直す
    }

    /// <summary>現在表示中の MasterPattern preview 値を破棄する。</summary>
    private void ResetMasterPattern3DCache() // (260322Ch) 旧名: ResetMasterPattern3DPreviewCache
    {
        masterPattern2DValues = []; // (260322Ch)
        masterPattern3DValuesPositive = []; // (260322Ch)
        masterPattern3DValuesNegative = []; // (260322Ch)
        masterPattern3DCacheGridSize = 0; // (260322Ch)
    }

    /// <summary>
    /// キャッシュ済みの preview 値と現在の PseudoBitmap 設定を使って 3D を描き直す。
    /// 輝度レンジやカラースケール変更時に 2D 側の設定をそのまま反映するために使う。
    /// </summary>
    private void RedrawMasterPattern3DFromCache() // (260322Ch) 旧名: RedrawMasterPattern3DPreviewFromCache
    {
        if (masterPattern2DBitmap == null || masterPattern3DCacheGridSize <= 0)
        {
            ClearMesh();
            return;
        }

        if (masterPattern2DValues == null || masterPattern2DValues.Length != masterPattern3DCacheGridSize * masterPattern3DCacheGridSize)
        {
            ClearMesh();
            return;
        }

        DrawMasterPattern3D(
            masterPattern3DValuesPositive,
            masterPattern3DValuesNegative,
            masterPattern3DCacheGridSize,
            masterPattern3DCacheGridType, // 260331Cl
            masterPattern2DBitmap); // (260322Ch) ScalablePictureBoxAdvanced 2D の見た目設定を OpenGL 側へ反映する
    }

    /// <summary>生の MasterPattern plane を、preview 用の 0-1 強度配列へ変換する。</summary>
    private static double[] CreateMasterPatternDisplayValues(float[] plane, int gridSize) // (260322Ch) 旧名: CreateMasterPatternPreviewValues
    {
        if (plane == null || plane.Length != gridSize * gridSize)
            return new double[gridSize * gridSize]; // (260322Ch) helper を廃止し、空の placeholder 配列を直接返す

        var max = plane.Max();
        return max > 0
            ? [.. plane.Select(value => Math.Sqrt(Math.Max(0f, value) / max))]
            : new double[gridSize * gridSize]; // (260322Ch) 0 除算を避けつつ 2D / 3D で同じ見え方を維持する
    }

    /// <summary>preview 用の数値配列を PseudoBitmap に変換し、ScalablePictureBoxAdvanced へ設定する。</summary>
    private void SetMasterPattern2DBitmap(double[] values, int gridSize)
    {
        if (scalablePictureBoxAdvancedMasterPattern2D == null || gridSize <= 0)
            return;

        var displayValues = values != null && values.Length == gridSize * gridSize
            ? values
            : new double[gridSize * gridSize]; // (260322Ch) helper を介さず空配列をその場で補う
        // var min = displayValues.Min(); // (260322Ch) 旧案: データ最小値をそのまま minimum intensity の下限にしていた
        var min = 0.0; // (260322Ch) MasterPattern preview の minimum intensity 下限は常に 0 にそろえる
        var max = displayValues.Max();
        if (Math.Abs(max - min) < 1e-12)
        {
            // displayValues = CreateMasterPatternPlaceholderValues(gridSize); // (260321Ch) 旧案: 単色データのとき微小勾配を入れていた
            max = min + 1.0; // (260321Ch) 真っ黒な初期表示を維持したまま、PseudoBitmap のレンジだけ確保する
        }

        var previousPreviewBitmap = masterPattern2DBitmap; // (260322Ch) polarity / scale / intensity の見た目設定を新 slice へ引き継ぐ
        var previousColorScale = previousPreviewBitmap?.ColorScale; // (260322Ch)
        var previousGrayScale = previousPreviewBitmap?.GrayScale ?? true; // (260322Ch)
        var previousIsNegative = previousPreviewBitmap?.IsNegative ?? false; // (260322Ch)
        var previousLowerIntensity = previousPreviewBitmap?.MinValue ?? min; // (260322Ch)
        var previousUpperIntensity = previousPreviewBitmap?.MaxValue ?? max; // (260322Ch)
        var preserveZoomAndCenter = scalablePictureBoxAdvancedMasterPattern2D.PseudoBitmap != null
            && scalablePictureBoxAdvancedMasterPattern2D.PseudoBitmap.Width == gridSize
            && scalablePictureBoxAdvancedMasterPattern2D.PseudoBitmap.Height == gridSize; // (260322Ch) energy / depth 切替時は表示領域を維持する
        var previousZoomAndCenter = preserveZoomAndCenter
            ? scalablePictureBoxAdvancedMasterPattern2D.ZoomAndCenter
            : default; // (260322Ch)

        masterPattern2DBitmap?.Dispose();
        // masterPattern2DBitmap = new PseudoBitmap(displayValues, gridSize, PseudoBitmap.ColorScaleFireLiner) // (260322Ch) 旧案: slice 切替のたびに polarity / scale が初期化され、見た目が negative 側へ崩れていた
        masterPattern2DBitmap = new PseudoBitmap(displayValues, gridSize)
        {
            MinValue = previousLowerIntensity,
            MaxValue = previousUpperIntensity,
            GrayScale = previousGrayScale,
            IsNegative = previousIsNegative,
        };
        if (previousColorScale != null && previousColorScale.Length > 1)
            masterPattern2DBitmap.ColorScale = previousColorScale; // (260322Ch) 現在のカラースケール設定も維持する

        // scalablePictureBoxAdvancedMasterPattern2D.Symbols = CreateMasterPatternPreviewSymbols(gridSize); // (260322Ch) MasterPattern2D の overlay 枠線・中心線描画は廃止
        scalablePictureBoxAdvancedMasterPattern2D.PseudoBitmap = masterPattern2DBitmap;
        scalablePictureBoxAdvancedMasterPattern2D.MinimumIntensity = 0; // (260322Ch) minimum intensity の下限は 0 固定
        if (preserveZoomAndCenter)
            scalablePictureBoxAdvancedMasterPattern2D.ZoomAndCenter = previousZoomAndCenter; // (260322Ch) energy / depth 切替時に表示領域をリセットしない
        scalablePictureBoxAdvancedMasterPattern2D.DrawPictureBox();
    }

    /// <summary>
    /// ScalablePictureBoxAdvanced 側で輝度レンジやカラースケールが変わったときに、
    /// 現在の MasterPattern3D を同じ見え方で再描画する。
    /// </summary>
    private void scalablePictureBoxAdvancedMasterPattern2D_BrightnessAndColorChanged(object sender, EventArgs e)
        => RedrawMasterPattern3DFromCache(); // (260322Ch)

    #endregion

    #region MasterPattern3D


    /// <summary>3D preview 上の既存オブジェクトを削除し、黒背景だけの状態へ戻す。 </summary>
    private void ClearMesh() // (260322Ch) 旧名: ClearMasterPattern3DPreview
    {
        if (glControlMasterPattern3D == null)
            return;

        glControlMasterPattern3D.DeleteAllObjects();
        glControlMasterPattern3D.Refresh();
    }

    /// <summary>
    /// 正規化済みの MasterPattern 値から、Rosca-Lambert 球面 preview を再描画する。
    /// 3D 側は +Z / -Z の両半球を同時に表示する。
    /// </summary>
    private void DrawMasterPattern3D(double[] positiveValues, double[] negativeValues, int gridSize, MasterPattern.Types gridType, PseudoBitmap referenceBitmap) // 260331Cl gridType 追加
    {
        if (glControlMasterPattern3D == null || gridSize <= 0)
            return;

        glControlMasterPattern3D.DeleteAllObjects();

        if ((positiveValues == null || positiveValues.Length != gridSize * gridSize)
            && (negativeValues == null || negativeValues.Length != gridSize * gridSize))
        {
            glControlMasterPattern3D.Refresh();
            return;
        }

        var glObjects = new List<GLObject>();
        if (positiveValues != null && positiveValues.Length == gridSize * gridSize)
        {
            var positiveObject = gridType == MasterPattern.Types.Hexagonal
                ? CreateMesh_Hex(positiveValues, gridSize, MasterPattern.Hemisphere.PositiveZ, referenceBitmap)
                : CreateMesh_Square(positiveValues, gridSize, MasterPattern.Hemisphere.PositiveZ, referenceBitmap);
            if (positiveObject != null)
                glObjects.Add(positiveObject);
        }
        if (negativeValues != null && negativeValues.Length == gridSize * gridSize)
        {
            var negativeObject = gridType == MasterPattern.Types.Hexagonal
                ? CreateMesh_Hex(negativeValues, gridSize, MasterPattern.Hemisphere.NegativeZ, referenceBitmap)
                : CreateMesh_Square(negativeValues, gridSize, MasterPattern.Hemisphere.NegativeZ, referenceBitmap);
            if (negativeObject != null)
                glObjects.Add(negativeObject);
        }
        glObjects.AddRange(CreateMasterPattern3DAxisLabelObjects()); // (260322Ch) 3D preview 上に a / b / c 軸ラベルを重ねる
        if (glObjects.Count > 0)
            glControlMasterPattern3D.AddObjects(glObjects);
        glControlMasterPattern3D.Refresh();
    }

    private IEnumerable<GLObject> CreateMasterPattern3DAxisLabelObjects()
    {
        if (glControlMasterPattern3D == null || !checkBoxMasterPattern3DAxisLabel.Checked || Crystal?.A_Axis == null || Crystal.B_Axis == null || Crystal.C_Axis == null)
            return [];

        var axisVectors = new[] { Crystal.A_Axis.Normarize(), Crystal.B_Axis.Normarize(), Crystal.C_Axis.Normarize() };
        C4[] color = [C4.Red, C4.Green, C4.Blue];
        string[] label = ["a", "b", "c"];
        var objects = new List<GLObject>(3);
        for (int i = 0; i < axisVectors.Length; i++)
        {
            if (axisVectors[i].Length2 < 1e-12)
                continue;

            var labelPosition = axisVectors[i].ToOpenTK(); // (260322Ch) 軸方向に対応する球面上の座標へそのまま配置する
            objects.Add(new TextObject(label[i], 13f, labelPosition, 0.05, true, new Material(color[i]), glControlMasterPattern3D)); // (260322Ch) Main window と同じサイズ感で白縁付きにする
        }
        return objects;
    }

    /// <summary>
    /// Rosca-Lambert 等積正方形の強度分布を、球面上の三角形メッシュへ変換する。
    /// 以前のようにセルごとに GLObject を分けず、半球ごとに 1 メッシュへまとめて描画負荷を下げる。
    /// </summary>
    private static GLObject CreateMesh_Square(double[] values, int gridSize, MasterPattern.Hemisphere hemisphere, PseudoBitmap referenceBitmap)
    {
        var previewGrid = gridSize; // (260322Ch) メッシュ描画で十分高速なので元の格子サイズをそのまま使う
        var previewValues = values; // (260322Ch)
        if (previewGrid <= 0 || previewValues.Length != previewGrid * previewGrid)
            return null; // (260321Ch)

        var squareLimit = MasterPattern.SquareLimit;
        var step = 2.0 * squareLimit / previewGrid;
        int vertexGrid = previewGrid + 1;
        var positions = GC.AllocateUninitializedArray<V3>(vertexGrid * vertexGrid);
        var argbs = GC.AllocateUninitializedArray<int>(vertexGrid * vertexGrid);
        for (int y = 0; y < vertexGrid; y++)
        {
            var b = squareLimit - y * step;
            int rowOffset = y * vertexGrid;
            for (int x = 0; x < vertexGrid; x++)
            {
                var a = -squareLimit + x * step;
                int index = rowOffset + x;
                positions[index] = MasterPattern.RoscaLambertToSphereSquare(a, b, hemisphere).ToOpenTK(); // (260321Ch)
                var value = GetMasterPattern3DVertexValue(previewValues, previewGrid, x, y); // (260321Ch) 頂点色は隣接セル平均で滑らかにつなぐ
                argbs[index] = GetMasterPattern3DColor(value, referenceBitmap).ToArgb();
            }
        }

        var indices = GC.AllocateUninitializedArray<uint>(previewGrid * previewGrid * 6);
        int cursor = 0;
        for (int y = 0; y < previewGrid; y++)
        {
            int row0 = y * vertexGrid;
            int row1 = (y + 1) * vertexGrid;
            for (int x = 0; x < previewGrid; x++)
            {
                uint i00 = (uint)(row0 + x);
                uint i10 = i00 + 1;
                uint i01 = (uint)(row1 + x);
                uint i11 = i01 + 1;
                indices[cursor++] = i00;
                indices[cursor++] = i10;
                indices[cursor++] = i11;
                indices[cursor++] = i00;
                indices[cursor++] = i11;
                indices[cursor++] = i01;
            }
        }

        // return new ColoredSurfaceMesh(positions, argbs, indices, CreateMasterPattern3DMaterial(C4.White), DrawingMode.Surfaces) { IgnoreNormalSides = true };// (260321Ch) 旧実装: material 生成を helper へ切り出していた
        return new ColoredSurfaceMesh(positions, argbs, indices, new Material(C4.White) { Emission = 1f, Ambient = 0f, Diffuse = 0f, Specular = 0f, SpecularPower = 1f, }, DrawingMode.Surfaces) { IgnoreNormalSides = true };// (260322Ch) 呼び出し元が 1 箇所だけなので material 生成はインライン展開する
    }

    /// <summary>
    /// 六方格子の plane データを六方 Lambert 座標系のまま球面メッシュに変換する。260331Cl 追加
    /// セル中心を頂点とし、隣接 3 セルで三角形を構成する。
    /// </summary>
    private static GLObject CreateMesh_Hex(double[] values, int gridSize, MasterPattern.Hemisphere hemisphere, PseudoBitmap referenceBitmap)
    {
        if (gridSize <= 1 || values == null || values.Length != gridSize * gridSize)
            return null;

        int N = (gridSize - 1) / 2;
        double spacing = MasterPattern.HexSpacing(N);

        // 頂点: 有効な六方格子セル中心 → 球面座標
        // vertexMap[linearIndex] → 頂点配列中の index (-1 なら無効セル)
        var vertexMap = new int[gridSize * gridSize];
        Array.Fill(vertexMap, -1);
        var positionList = new List<V3>();
        var argbList = new List<int>();

        for (int v = -N; v <= N; v++)
            for (int u = -N; u <= N; u++)
            {
                if (!MasterPattern.IsValidHexCell(u, v, N))
                    continue;
                int linIdx = MasterPattern.HexLinearIndex(u, v, N);
                var (hx, hy) = MasterPattern.HexAxialToCartesian(u, v, spacing);
                var spherePos = MasterPattern.RoscaLambertToSphereHexSquare(hx, hy, hemisphere);
                vertexMap[linIdx] = positionList.Count;
                positionList.Add(spherePos.ToOpenTK());
                var value = linIdx < values.Length ? values[linIdx] : 0;
                argbList.Add(GetMasterPattern3DColor(value, referenceBitmap).ToArgb());
            }

        if (positionList.Count == 0)
            return null;

        // 三角形: 菱形 (u,v)-(u+1,v)-(u,v+1)-(u+1,v+1) を 2 三角形に分割
        var indexList = new List<uint>();
        for (int v = -N; v <= N - 1; v++)
            for (int u = -N; u <= N - 1; u++)
            {
                int i00 = MasterPattern.IsValidHexCell(u, v, N) ? vertexMap[MasterPattern.HexLinearIndex(u, v, N)] : -1;
                int i10 = MasterPattern.IsValidHexCell(u + 1, v, N) ? vertexMap[MasterPattern.HexLinearIndex(u + 1, v, N)] : -1;
                int i01 = MasterPattern.IsValidHexCell(u, v + 1, N) ? vertexMap[MasterPattern.HexLinearIndex(u, v + 1, N)] : -1;
                int i11 = MasterPattern.IsValidHexCell(u + 1, v + 1, N) ? vertexMap[MasterPattern.HexLinearIndex(u + 1, v + 1, N)] : -1;

                // 上三角形: (u,v), (u+1,v), (u,v+1)
                if (i00 >= 0 && i10 >= 0 && i01 >= 0)
                {
                    indexList.Add((uint)i00);
                    indexList.Add((uint)i10);
                    indexList.Add((uint)i01);
                }
                // 下三角形: (u+1,v), (u+1,v+1), (u,v+1)
                if (i10 >= 0 && i11 >= 0 && i01 >= 0)
                {
                    indexList.Add((uint)i10);
                    indexList.Add((uint)i11);
                    indexList.Add((uint)i01);
                }
            }

        if (indexList.Count == 0)
            return null;

        return new ColoredSurfaceMesh(
            positionList.ToArray(), argbList.ToArray(), indexList.ToArray(),
            new Material(C4.White) { Emission = 1f, Ambient = 0f, Diffuse = 0f, Specular = 0f, SpecularPower = 1f },
            DrawingMode.Surfaces)
        { IgnoreNormalSides = true };
    }

    /// <summary>セル中心値から頂点色を作るため、隣接する 1～4 セルを平均する。</summary>
    private static double GetMasterPattern3DVertexValue(double[] values, int gridSize, int vertexX, int vertexY) // (260322Ch) 旧名: GetMasterPatternPreviewVertexValue
    {
        double sum = 0;
        int count = 0;
        for (int y = Math.Max(0, vertexY - 1); y <= Math.Min(gridSize - 1, vertexY); y++)
            for (int x = Math.Max(0, vertexX - 1); x <= Math.Min(gridSize - 1, vertexX); x++)
            {
                sum += values[y * gridSize + x];
                count++;
            }

        return count == 0 ? 0 : sum / count; // (260321Ch)
    }

    /// <summary>ScalablePictureBoxAdvanced と同じ PseudoBitmap の色変換で 3D polygon の色を返す。</summary>
    private static C4 GetMasterPattern3DColor(double value, PseudoBitmap referenceBitmap) // (260322Ch) 旧名: GetMasterPatternPreviewColor
    {
        if (referenceBitmap?.ColorScale == null || referenceBitmap.ColorScale.Length == 0)
            return C4.Black;

        var minValue = referenceBitmap.MinValue;
        var maxValue = referenceBitmap.MaxValue;
        if (Math.Abs(maxValue - minValue) < 1e-12)
            maxValue = minValue + 1.0; // (260321Ch) PseudoBitmap 側と同様に単色時の分母を確保する

        var colorScale = referenceBitmap.ColorScale;
        var coeff = colorScale.Length / (maxValue - minValue);
        var index = Math.Clamp((int)((value - minValue) * coeff + 0.5), 0, colorScale.Length - 1);
        var (r0, g0, b0) = colorScale[index];
        byte r, g, b;
        if (referenceBitmap.GrayScale)
            r = g = b = b0; // (260321Ch) PseudoBitmap.GetImage() は GrayScale 時に B 成分だけを全チャネルへ使う
        else
            (r, g, b) = (r0, g0, b0);

        if (referenceBitmap.IsNegative)
            (r, g, b) = ((byte)(255 - r), (byte)(255 - g), (byte)(255 - b));
        return new C4(r / 255f, g / 255f, b / 255f, 1f);
    }

    private void checkBoxMasterPattern3DAxisLabel_CheckedChanged(object sender, EventArgs e) => RedrawMasterPattern3DFromCache(); // (260322Ch) MasterPattern3D 上の a / b / c ラベル表示を即座に切り替える

    private void checkBoxMasterPattern3DAxisArrows_CheckedChanged(object sender, EventArgs e) => panelMasterPattern3DAxes.Visible = checkBoxMasterPattern3DAxisArrows.Checked; // (260322Ch) MasterPattern3D axes inset の表示可否を切り替える

    #endregion

    #endregion



    private void checkBoxWithBSEDistribution_CheckedChanged(object sender, EventArgs e)
    {
        flowLayoutPanelOutputRange.Enabled = !checkBoxWithBSEDistribution.Checked;
        DrawEBSD(); // (260327Ch) BSE 分布つき合成と単一スライス表示を即座に切り替える
    }
}


