#region using
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
using System.Drawing.Imaging; // 260724Cl 追加: 実測 EBSD 画像の透明度合成 (ImageAttributes/ColorMatrix) 用
using System.Text;
using System.Threading.Tasks;
using ZLinq;
#endregion

namespace ReciPro;

public partial class FormEBSD : FormBase
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


    //260727Cl (/simplify): 唯一の利用者だった BuildEbsdLookupTable が EbsdPatternComposer へ移設されて未参照になったので削除
    //旧: const double Inv_PI = 1 / Math.PI; / const double Half_PI = 0.5 * Math.PI;

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
    private System.Threading.CancellationTokenSource monteCarloCts; // 260406Cl 追加: MonteCarlo 計算のキャンセル用
    private void DisposeMonteCarloCts() { monteCarloCts?.Dispose(); monteCarloCts = null; } // 260406Cl 追加

    private EbsdMonteCarloDistribution mcDistribution = null; // 260325Cl 追加: MC フィッティング結果
    private MonteCarloDistributionDepthMode monteCarloDistributionDepthMode = MonteCarloDistributionDepthMode.LastInelasticEventDepth; // (260331Ch) MasterPattern 重み付けに使う z は既定で last inelastic depth

    /// <summary>飛程計算の際の打ち切りエネルギー (kev)</summary>
    private double EnergyThreshold = 2;
    public double WaveLength { get => waveLengthControl.WaveLength; set => waveLengthControl.WaveLength = value; }

    public double DetTilt => numericBoxDetTilt.RadianValue;
    // public double DetR => numericBoxDetRadius.Value; // 260723Cl 廃止: 円形検出器 (半径 mm) → 矩形検出器 (Width/Height px × Resolution mm/px) へ移行
    /// <summary>検出器の横ピクセル数。260723Cl 追加</summary>
    public int DetPixelWidth => Math.Max(1, numericBoxDetWidth.ValueInteger);
    /// <summary>検出器の縦ピクセル数。260723Cl 追加</summary>
    public int DetPixelHeight => Math.Max(1, numericBoxDetHeight.ValueInteger);
    /// <summary>検出器のピクセルサイズ (mm/px)。260723Cl 追加</summary>
    public double DetPixelSize => Math.Max(1E-6, numericBoxDetResolution.Value);
    /// <summary>検出器の物理半幅 (mm)。260723Cl 追加</summary>
    public double DetHalfWidth => DetPixelWidth * DetPixelSize * 0.5;
    /// <summary>検出器の物理半高 (mm)。260723Cl 追加</summary>
    public double DetHalfHeight => DetPixelHeight * DetPixelSize * 0.5;
    /// <summary>検出器中心の X 座標 (mm)。260723Cl 追加</summary>
    public double DetX => numericBoxXofDet.Value;
    public double DetY => numericBoxYofDet.Value;
    public double DetZ => numericBoxZofDet.Value;

    #region レジストリ保存用プロパティ (FormMain.Registry の rw() から get/set される) 260724Cl 追加
    public double DetectorTiltDegree { get => numericBoxDetTilt.Value; set => numericBoxDetTilt.Value = value; }
    public double DetectorX { get => numericBoxXofDet.Value; set => numericBoxXofDet.Value = value; }
    public double DetectorY { get => numericBoxYofDet.Value; set => numericBoxYofDet.Value = value; }
    public double DetectorZ { get => numericBoxZofDet.Value; set => numericBoxZofDet.Value = value; }
    public int DetectorPixelWidth { get => numericBoxDetWidth.ValueInteger; set => numericBoxDetWidth.Value = value; }
    public int DetectorPixelHeight { get => numericBoxDetHeight.ValueInteger; set => numericBoxDetHeight.Value = value; }
    public double DetectorPixelSize { get => numericBoxDetResolution.Value; set => numericBoxDetResolution.Value = value; }
    public double SampleTiltDegree { get => numericBoxSampleTilt.Value; set => numericBoxSampleTilt.Value = value; }
    public bool FlipDetectorLeftRight { get => checkBoxFlipDetectorLeftRight.Checked; set => checkBoxFlipDetectorLeftRight.Checked = value; }
    #endregion

    public double SmpTilt => numericBoxSampleTilt.RadianValue;

    //260726Cl 型変更: 10 要素タプル → EbsdBackscatteredElectron (Crystallography 側の record struct。メンバー名は同一なので参照側は不変)。
    //旧: (double Depth, V3 Vec, PointD Position, double Energy, double TotalEnergyLoss, bool HasLastInelasticEvent, double LastInelasticDepth, double LastInelasticEnergyBeforeLoss, double LastInelasticEnergyAfterLoss, V3 LastInelasticDirection)[] BSEs = [];
    EbsdBackscatteredElectron[] BSEs = []; // (260331Ch) 最後の非弾性散乱情報も保持する

    public Crystal Crystal => FormMain.Crystal;

    /// <summary>検出器法線を Z 軸へ合わせた座標での検出器面の符号付き Z 位置。260725Cl 追加 (/simplify):
    /// 晶帯軸ラベルの表示方向判定が同じ三角関数式を手書きで持っていたため、CameraLength2 と式を 1 箇所に統合した。
    /// 標準配置 (DetTilt=90°, DetY&lt;0) では **−CameraLength2** になる — EbsdDetectorGeometry.FromPatternCenter の
    /// 「標準配置 signed L = −DD」と同一規約 (260725Cl 訂正: 当初 +CameraLength2 と書いていたが既定値 DetY=−35/DetZ=30/δ=90° で −35 = −CameraLength2)。
    /// 利用側は相対符号のみを見る (晶帯軸の表示向き判定) ので、この符号自体が挙動を左右することはない。</summary>
    private double SignedCameraLength => DetY * Math.Sin(DetTilt) - DetZ * Math.Cos(DetTilt); // 260725Cl: public → private (外部参照なし)

    /// <summary>試料から検出器までの距離</summary>
    // public double CameraLength2 => Math.Abs(DetY * Math.Sin(DetTilt) - DetZ * Math.Cos(DetTilt)); // 260725Cl 変更前
    public double CameraLength2 => Math.Abs(SignedCameraLength); // 260725Cl 変更

    /// <summary>画像の中心。検出器(Detector)座標系(Foot原点)で表現</summary>
    public PointD Foot
    {
        get
        {
            // 260723Cl 変更: 検出器中心の X オフセット (DetX) 対応のため、YZ 平面の 2D 同次変換 + ±len 分岐を 3D ベクトル式へ書き換え。
            // 検出器中心 C=(DetX,-DetY,-DetZ)、法線 n=(0,sinΘd,-cosΘd)、面内基底 ex=(1,0,0), ey=n×ex として
            // 垂線の足 F=(n・C)n から Foot=((F-C)・ex, (F-C)・ey)=(-DetX, -(DetY cosΘd + DetZ sinΘd))。通常配置で旧実装と等価。
            // 注: ここでの ey=n×ex=(0,-cosΘd,-sinΘd) は「表示の下向き Y」基底。DrawGeometry/CalcStatistics の f1 基底
            // eyGeometry=RotX(-Θd)・(0,1,0)=(0,cosΘd,sinΘd) はその逆向き (同一面内)。セル式の Y 符号はこの差を吸収済み (Codex 検証済)。
            var (sinDetTilt, cosDetTilt) = Math.SinCos(DetTilt);
            return new PointD(-DetX, -(DetY * cosDetTilt + DetZ * sinDetTilt));
            #region 旧実装 (260723Cl 変更前)
            ////垂線の足の実空間座標座標
            //var (sinDetTilt, cosDetTilt) = Math.SinCos(DetTilt);
            //var f = new V3(-CameraLength2 * sinDetTilt, CameraLength2 * cosDetTilt, 1);
            ////検出器の中心座標
            //var c = new V3(DetY, DetZ, 1);

            //var len = (f - c).Length;
            //var (sin, cos) = Math.SinCos(-DetTilt);// double cos = Math.Cos(-DetTilt), sin = Math.Sin(-DetTilt);

            //var rot = new M3(cos, -sin, DetY - DetY * cos + DetZ * sin,
            //                 sin, cos, DetZ - DetY * sin - DetZ * cos,
            //                 0, 0, 1);

            //return (rot * f).X > c.X ? new PointD(0, len) : new PointD(0, -len);
            #endregion
        }
    }

    /// <summary>画面解像度 mm/pix</summary>
    // public double Resolution => 2.0 * numericBoxDetRadius.Value / graphicsBox.ClientRectangle.Width; // 260723Cl 変更前: 画面幅=検出器直径の固定表示
    // public double Resolution => Math.Max(1E-6, numericBoxResolution.Value); // 260723Cl 変更: 表示解像度 (ズーム) は numericBoxResolution が保持 // 260725Cl 変更前
    public double Resolution => renderResolutionOverride ?? Math.Max(1E-6, numericBoxResolution.Value); // 260725Cl 変更: Copy 用オフスクリーン描画中は上書き値を優先
    public float ResolutionF => (float)Resolution;

    #region 表示ビュー状態 (ズーム・パン) 260723Cl 追加
    // 表示パターン座標系: 垂線の足 (PC) を原点とし、X は左右反転トグル (DetectorXMirror) 適用後の mm 座標。
    // 菊池線・晶帯軸ラベルなどのオーバーレイと検出器矩形はこの座標系で描画され、SetProjection が画面へ変換する。

    /// <summary>中ドラッグによる平行移動量 (表示パターン座標 mm)。260723Cl 追加</summary>
    private PointD viewPan = new(0, 0);

    /// <summary>マウス操作から numericBoxResolution / sizeControl を書き戻すときの再入抑止。260723Cl 追加</summary>
    private bool skipViewEvent = false;

    /// <summary>検出器中心の表示パターン座標 (mm)。X は表示反転 (xm) を適用。260723Cl 追加</summary>
    private PointD DetectorCenterView => new(DetectorXMirror * DetX, -Foot.Y);

    /// <summary>画面中心に表示する表示パターン座標 (mm)。既定 (viewPan=0) は検出器中心。260723Cl 追加</summary>
    private PointD ViewCenter => new(DetectorCenterView.X + viewPan.X, DetectorCenterView.Y + viewPan.Y);

    /// <summary>Copy 用オフスクリーン描画時のキャンバスサイズ・表示解像度の一時上書き (RenderViewTo が設定)。260725Cl 追加
    /// (260725Cl 訂正: 旧 doc は改名前の RenderViewToBitmap を参照していた)</summary>
    private Size? renderCanvasOverride = null;
    private double? renderResolutionOverride = null;
    //260725Cl (/simplify): 検出器外枠の抑止は描画コンテキストではなく単発の表示オプションなので、
    //可変フィールド (set→finally で復元) をやめ DrawOverlays の引数にした。旧: private bool renderSuppressDetectorOutline = false;

    /// <summary>描画キャンバスのピクセルサイズ。通常は graphicsBox、Copy 時はオフスクリーンビットマップ。260725Cl 追加</summary>
    private Size CanvasSize => renderCanvasOverride ?? graphicsBox.ClientSize;
    #endregion

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

    /// <summary>EBSD パターン画像 (現在の視野全体をカバーするラスター)。DrawEBSDCore が更新し DrawOverlays が画面へ配置する。260723Cl 追加
    /// 実体は Pbmp.GetImage が返す PseudoBitmap 内部キャッシュ (destBmp) への参照。所有権は Pbmp 側にあり、ここで Dispose してはいけない (260724Cl)。</summary>
    private Bitmap patternBitmap = null;

    /// <summary>patternBitmap が表す表示パターン座標 (mm) の矩形 (生成時の視野)。260724Cl 追加</summary>
    private RectangleD patternBitmapRect;

    #region 実測 EBSD 画像のフィールド (D&D で読み込み、検出器矩形へ重ねて表示) 260724Cl 追加。FormDiffractionSimulatorGeometry の OverlappedImage と同じ流儀

    /// <summary>実測 EBSD 画像の生強度。所有権はこのフォーム (差し替え時に Dispose)。260724Cl 追加</summary>
    private PseudoBitmap expPbmp = null;

    /// <summary>expPbmp.GetImage() が返す表示用 Bitmap への借用参照 (内部キャッシュ destBmp。Dispose 禁止)。260724Cl 追加</summary>
    private Bitmap expImage = null;

    /// <summary>輝度 (Max intensity) トラックバー値→実強度の対数変換係数。260724Cl 追加 (FormDiffractionSimulatorGeometry と同形)</summary>
    private double expTrackbarConstantA = 0, expTrackbarConstantB = 1;

    /// <summary>expPbmp 差し替え時の不変条件 (旧インスタンス破棄と旧由来 expImage の無効化) を集約。260724Cl 追加</summary>
    private void SetExpPseudoBitmap(PseudoBitmap value)
    {
        expPbmp?.Dispose();
        expPbmp = value;
        expImage = null;
        InvalidateIndexingResults(); //260724Cl: 旧画像の方位候補を失効させる (下の指数付け region)。バンド検出廃止で引数レス化 //260727Cl: FormEBSD.Indexing.cs は本ファイルへ統合済みなので参照先を訂正
    }

    #endregion

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
        HelpPage = "12-ebsd-simulation"; //260529Cl 追加
        // 260731Cl 追加: ダークモード時、ステレオネット (PoleFigureControl) に重ねて配置したチェックボックスとラベルの
        // 背景をキャンバス背景 (CanvasBackColor: ダーク時 #202020) に合わせる (Designer は White 固定)
        if (Application.IsDarkModeEnabled)
            checkBoxDrawAxesInStereonet.BackColor = labelBseStereonetNote.BackColor = CanvasBackColor;

        buttonStop.Click += buttonStop_Click; // (260327Ch) 既存の Stop ボタンは MasterPattern build 停止に使う
        UpdateEbsdTiltCoeffs(); // 260325Cl: tilt 係数を初期値で計算
        checkBoxDrawAxesInStereonet.CheckedChanged += (_, _) => DrawGeometry(); // 260725Ch: 結晶軸表示の切替をステレオネットへ即時反映

        dataGridViewEbsdCandidates.Font = new Font(dataGridViewEbsdCandidates.Font.FontFamily, Math.Max(7f, dataGridViewEbsdCandidates.Font.Size - 1f), dataGridViewEbsdCandidates.Font.Style, dataGridViewEbsdCandidates.Font.Unit); // 260725Ch: 候補一覧を小さい文字にして表示行数を増やす
        dataGridViewEbsdCandidates.RowTemplate.Height = dataGridViewEbsdCandidates.Font.Height + 3; // 260725Ch: フォントに合わせて行高も詰める
        dataGridViewEbsdCandidates.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // 260725Ch: 候補は行全体を選択

        // 260724Cl 追加: 表示チェックが ON になったら対応する設定タブを前面に出す
        checkBoxShowDyanmicalEBSD.CheckedChanged += (_, _) => { if (checkBoxShowDyanmicalEBSD.Checked) tabControlPatternSettings.SelectedTab = tabPageOutputParameter; };
        checkBoxShowExperimentalImage.CheckedChanged += (_, _) => { if (checkBoxShowExperimentalImage.Checked) tabControlPatternSettings.SelectedTab = tabPageExperimentalImage; };
        checkBoxShowOverlays.CheckedChanged += (_, _) => { if (checkBoxShowOverlays.Checked) tabControlSettings.SelectedTab = tabPageOverlays; };
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
            // 260617Cl 変更: 日本語固定リテラルは英語(neutral)UIで未翻訳に見えるため英語化 (Phase 0)。開発者向け診断のため英語固定で十分。
            // 旧: "ReciPro.sln が見つからず、generated source の出力先を特定できませんでした。"
            MessageBox.Show(this, "ReciPro.sln was not found; could not determine the output location for the generated source.", "NIST elastic sampler compression", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

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
        toolStripStatusLabelSummary.Text = "NIST elastic compression";
        StatusBarHelper.SetProgress(toolStripProgressBar, toolStripStatusLabelProgress, 0, "", TimeSpan.Zero, showRemaining: true);// 260520Cl SetProgress化 (canonical進捗行)
        toolStripStatusLabelDetail.Text = "";

        var stopwatch = Stopwatch.StartNew();
        var progress = new Progress<NistElasticCompressionProgress>(state =>
        {
            var progressValue = Math.Clamp((int)Math.Round(100.0 * state.OverallProgress), 0, 100);
            toolStripProgressBar.Value = progressValue;
            toolStripStatusLabelSummary.Text = $"NIST elastic compression: Z={state.AtomicNumber}, block {state.BlockIndex}/{state.BlockCount}, {state.Phase}";
            StatusBarHelper.SetProgress(toolStripProgressBar, toolStripStatusLabelProgress, progressValue / 100.0, "", stopwatch.Elapsed, showRemaining: true);// 260520Cl SetProgress化
            toolStripStatusLabelDetail.Text = $"File {state.FileIndex}/{state.FileCount}: {Path.GetFileName(state.SourcePath)}";
        });

        try
        {
            var compressionResult = await Task.Run(() => NistElasticSamplerPchipGenerator.GenerateCompressedSourcesToRepository(openFileDialog.FileNames, repositoryRoot, progress)); // (260401Ch) 実処理は Crystallography.Atom.NistElastic 側の API を呼ぶ
            stopwatch.Stop();
            toolStripProgressBar.Value = 100;
            toolStripStatusLabelSummary.Text = "NIST elastic compression completed";
            StatusBarHelper.SetProgress(toolStripProgressBar, toolStripStatusLabelProgress, 1.0, "", stopwatch.Elapsed);// 260520Cl SetProgress化 (完了)
            toolStripStatusLabelDetail.Text = $"{compressionResult.SourceFileCount} file(s) processed, {compressionResult.OutputPaths.Count} output file(s).";
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
            toolStripStatusLabelSummary.Text = "NIST elastic compression failed";
            toolStripStatusLabelProgress.Text = $"Failed after {StatusBarHelper.FormatElapsed(stopwatch.Elapsed)}";
            toolStripStatusLabelDetail.Text = ex.Message;
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
        // 260612Cl z(平均原子番号)/a(平均原子量)/valenceElectronCount は MonteCarlo.GetMeanAtomicParameters (Multiplicity×Occ 加重) に集約。
        // 旧: 下記 sum1/sum2/sum3 + EstimateAverageValenceElectronCount を直書きし、Multiplicity のみで Occ 抜け (部分占有・固溶体で実組成とずれた)。
        //var sum1 = cry.Atoms.Sum(a => AtomStatic.AtomicWeight(a.AtomicNumber) * a.Multiplicity * a.AtomicNumber);
        //var sum2 = cry.Atoms.Sum(a => AtomStatic.AtomicWeight(a.AtomicNumber) * a.Multiplicity);
        //var sum3 = cry.Atoms.Sum(a => a.Multiplicity);
        //var valenceElectronCount = MonteCarlo.EstimateAverageValenceElectronCount(
        //    atoms.Select(atom => (atom.AtomicNumber, AtomStatic.AtomicWeight(atom.AtomicNumber) * atom.Multiplicity))); // (260401Ch)
        var (z, a, valenceElectronCount) = MonteCarlo.GetMeanAtomicParameters(cry.Atoms);//260612Cl
        var rho = cry.Density;
        var energy = Voltage;
        var sampleTilt = SmpTilt;
        var energyThreshold = EnergyThreshold;
        var sampleRotation = M3.CreateRotationX(sampleTilt);
        var loop = Math.Min(ElasticSamplerBenchmarkLoopCount, BackscatterMonteCarloLoopCount);
        var atoms = cry.Atoms.ToArray();
        var fileStem = $"NistElasticSamplerBenchmark_{DateTime.Now:yyyyMMdd_HHmmss}_E{energy.ToString("0.0", CultureInfo.InvariantCulture)}keV"; // (260401Ch)

        buttonFitNistElasticSampler.Enabled = false;
        buttonBenchmarkNistElasticSampler.Enabled = false;
        toolStripProgressBar.Value = 0;
        toolStripStatusLabelSummary.Text = "NIST elastic MC benchmark";
        StatusBarHelper.SetProgress(toolStripProgressBar, toolStripStatusLabelProgress, 0, "", TimeSpan.Zero, showRemaining: true);// 260520Cl SetProgress化 (canonical進捗行)
        toolStripStatusLabelDetail.Text = "";

        var originalSamplerTextDirectory = MonteCarlo.NistElasticSamplerTextDirectory;
        MonteCarlo.NistElasticSamplerTextDirectory = originalTextDirectory; // (260401Ch) benchmark 中は source tree の original TXT を使う
        var stopwatch = Stopwatch.StartNew();

        try
        {
            var progress = new Progress<(int Progress, string Message, string Detail)>(state =>
            {
                toolStripProgressBar.Value = Math.Clamp(state.Progress, 0, 100);
                toolStripStatusLabelSummary.Text = $"NIST elastic MC benchmark: {state.Message}";
                StatusBarHelper.SetProgress(toolStripProgressBar, toolStripStatusLabelProgress, state.Progress / 100.0, "", stopwatch.Elapsed, showRemaining: true);// 260520Cl SetProgress化 (コントロール値の読み戻しをやめ source=state.Progress から ratio を計算)
                toolStripStatusLabelDetail.Text = state.Detail;
            });

            var runs = await Task.Run(() => RunElasticSamplerSourceBenchmark(
                z, a, rho, energy, sampleTilt, energyThreshold, valenceElectronCount, atoms, loop, sampleRotation, progress));

            var summaryPath = WriteElasticSamplerBenchmarkSummaryCsv(outputDirectory, fileStem, runs);
            var depthPath = WriteElasticSamplerBenchmarkHistogramCsv(outputDirectory, fileStem, "depth", "DepthNm", runs, static electron => electron.Depth, 80);
            var energyPath = WriteElasticSamplerBenchmarkHistogramCsv(outputDirectory, fileStem, "energy", "ExitEnergyKev", runs, static electron => electron.Energy, 80);

            stopwatch.Stop();
            toolStripProgressBar.Value = 100;
            toolStripStatusLabelSummary.Text = "NIST elastic MC benchmark completed";
            StatusBarHelper.SetProgress(toolStripProgressBar, toolStripStatusLabelProgress, 1.0, "", stopwatch.Elapsed);// 260520Cl SetProgress化 (完了)
            toolStripStatusLabelDetail.Text = Path.GetFileName(summaryPath);
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
            toolStripStatusLabelSummary.Text = "NIST elastic MC benchmark failed";
            toolStripStatusLabelProgress.Text = $"Failed after {StatusBarHelper.FormatElapsed(stopwatch.Elapsed)}";
            toolStripStatusLabelDetail.Text = ex.Message;
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
        if (comboBoxMasterPatternGrid != null && comboBoxMasterPatternGrid.SelectedIndex < 0)
        {
            int defaultIndex = comboBoxMasterPatternGrid.FindStringExact("256");
            comboBoxMasterPatternGrid.SelectedIndex = defaultIndex >= 0 ? defaultIndex : 0; // (260322Ch) MasterPattern 分解能の既定値は 256 にする
        }
        if (comboBoxMasterPattern2DHemisphere != null && comboBoxMasterPattern2DHemisphere.SelectedIndex < 0 && comboBoxMasterPattern2DHemisphere.Items.Count > 0)
            comboBoxMasterPattern2DHemisphere.SelectedIndex = 0; // (260322Ch) MasterPattern2D の初期表示半球を +Z にそろえる
        #region MasterPattern3D control // (260322Ch)
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
        comboBoxGradient.SelectedIndex = comboBoxScale.SelectedIndex = 0;
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

    #region DrawGeometry() OpenGLで入射電子、試料、検出器の幾何学を描画し、ステレオネット上に検出器の輪郭を描画
    /// <summary>試料と電子線が交差する位置は常に(0,0,0)</summary>
    public void DrawGeometry(int i = -1, int j = -1)
    {
        // 260724Cl 追加: FormMain 未代入 (初期化中) や glControlGeo 生成前 (FormEBSD_Load 前) は描画しない
        if (FormMain == null || glControlGeo == null) return;
        #region OpenGLによる3D描画
        var glObjects = new List<GLObject>();

        //試料の傾き
        var samRot = Matrix3D.RotX(SmpTilt);
        //試料を示す直方体
        var sample = new Parallelepiped(samRot * new V3(-15, -15, -1), samRot * new V3(30, 0, 0), samRot * new V3(0, 30, 0), samRot * new V3(0, 0, 1), new Material(C4.AliceBlue), DrawingMode.SurfacesAndEdges);
        glObjects.Add(sample);

        //検出器の傾き
        var (sinDetTilt, cosDetTilt) = Math.SinCos(DetTilt);
        // var detector = new Cylinder(new V3(0, -DetY, -DetZ), new V3(0, sinDetTilt, -cosDetTilt), DetR, new Material(C4.GreenYellow, 0.7), DrawingMode.Surfaces, true, 2, 180); // 260723Cl 変更前: 円盤 (半径 DetR)
        // 260723Cl 変更: 矩形検出器 (halfW×halfH) を薄い直方体で描画。面内基底は縁描画と同じ RotX(-DetTilt) を使う
        double halfW = DetHalfWidth, halfH = DetHalfHeight;
        var detRotGeo = M3.CreateRotationX(-DetTilt);
        var detCenter = new V3(DetX, -DetY, -DetZ);
        V3 detEx = detRotGeo * new V3(1, 0, 0), detEy = detRotGeo * new V3(0, 1, 0), detEz = detRotGeo * new V3(0, 0, 1);
        var detector = new Parallelepiped(detCenter - halfW * detEx - halfH * detEy - 0.25 * detEz,
            2 * halfW * detEx, 2 * halfH * detEy, 0.5 * detEz, new Material(C4.GreenYellow, 0.7), DrawingMode.SurfacesAndEdges);
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
        // 260723Cl 変更: 円周 30 点 → 矩形周 32 点 (RectPerimeter)
        glObjects.AddRange(Enumerable.Range(0, 32).Select(e =>
        {
            var (x, y) = RectPerimeter(e / 32.0 * 4);
            var p = detRotGeo * new V3(halfW * x, halfH * y, 0);
            return new Lines([new V3(0, 0, 0), new(p.X + DetX, p.Y - DetY, p.Z - DetZ)], 1f, new Material(C4.Yellow, 0.7));
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
        // 260723Cl 変更: DetR*(x,y,0) → (halfW·x, halfH·y, 0) + 中心 X オフセット (DetX)。x,y は ±1 の検出器正規化座標
        var f1 = new Func<double, double, PointD>((x, y)
            => Stereonet.ConvertVectorToSchmidt(samRot2 * (detRot * new V3(halfW * x, halfH * y, 0) + new V3(DetX, -DetY, -DetZ))));

        if (checkBoxDrawAxesInStereonet.Checked)
        {
            var axisA = samRot * (Crystal.RotationMatrix * Crystal.A_Axis);
            var axisB = samRot * (Crystal.RotationMatrix * Crystal.B_Axis);
            var axisC = samRot * (Crystal.RotationMatrix * Crystal.C_Axis);
            poleFigureControl.Circles = [
                (Stereonet.ConvertVectorToSchmidt(axisA), 0.02, Color.Red, true, "a"),
                (Stereonet.ConvertVectorToSchmidt(-axisA), 0.02, Color.Red, true, "-a"),
                (Stereonet.ConvertVectorToSchmidt(axisB), 0.02, Color.Green, true, "b"),
                (Stereonet.ConvertVectorToSchmidt(-axisB), 0.02, Color.Green, true, "-b"),
                (Stereonet.ConvertVectorToSchmidt(axisC), 0.02, Color.Blue, true, "c"),
                (Stereonet.ConvertVectorToSchmidt(-axisC), 0.02, Color.Blue, true, "-c")
                ]; // 260725Ch: 3D幾何表示と同じ結晶a/b/c軸を上半球側の符号で重ねる
        }
        else
            poleFigureControl.Circles = []; // 260725Ch

        var step = 60;
        var range = Enumerable.Range(0, step + 1).Select(e => (double)e);
        // 260723Cl 変更: 検出器輪郭を円周 → 矩形周へ
        lines.Add((
            range.Select(n => 4.0 * n / step).Select(t => { var (x, y) = RectPerimeter(t); return f1(x, y); }).ToArray(),
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

    /// <summary>矩形周 (±1 正規化) 上の点を周回パラメータ t∈[0,4) から返す。260723Cl 追加 (OpenGL 縁線・ステレオネット輪郭・CalcStatistics で共用)</summary>
    private static (double x, double y) RectPerimeter(double t)
    {
        t = (t % 4 + 4) % 4;
        return t < 1 ? (2 * t - 1, -1) : t < 2 ? (1, 2 * (t - 1) - 1) : t < 3 ? (1 - 2 * (t - 2), 1) : (-1, 1 - 2 * (t - 3));
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
        if (glControlMasterPattern3D == null || Crystal?.A_Axis == null || Crystal.B_Axis == null || Crystal.C_Axis == null)
            return;

        var (u, v, w) = indexControl.Values;
        var zoneAxis = u * Crystal.A_Axis + v * Crystal.B_Axis + w * Crystal.C_Axis; // (260322Ch) 結晶学的 [u v w] を実空間ベクトルへ変換する
        if (zoneAxis.Length2 < 1e-12)
        {
            toolStripStatusLabelSummary.Text = "Zone axis [u v w] cannot be [0 0 0]."; // 260517Cl 旧挙動を復元: zone axis (0,0,0) を明示的に通知
            return;
        }

        glControlMasterPattern3D.WorldMatrix = GLGeometry.CreateRotationFromZ(zoneAxis.ToOpenTK()).ToMatrix4d(); // (260322Ch) zone axis が viewer の +Z 方向を向くように回転する
        toolStripStatusLabelSummary.Text = $"MasterPattern3D view: [{u} {v} {w}]"; // (260322Ch) // 260406Cl Label1→Label2: 進捗専用に整理
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
        InvalidateIndexingResults(); //260725Ch: 旧結晶または実行中探索の方位候補を新結晶へ適用させない
        composedPatternCache = default; //260725Ch: 旧結晶の MasterPattern をキャッシュ経由で保持し続けない
        // 260724Cl 追加: 結晶が変わると旧結晶の MasterPattern は無効なので、build 中なら停止したうえで破棄し、依存 UI も無効化する
        if (masterPatternEbsd.IsBuilding)
            masterPatternEbsd.CancelMasterPatternBuild();
        masterPatternEbsd.ClearMasterPattern();
        tabPageOutputParameter.Enabled = false;
        checkBoxShowDyanmicalEBSD.Enabled = false;
        UpdateMasterPatternSelectors();
        DrawMasterPattern2D(); // 3D キャッシュのクリア (ResetMasterPattern3DCache) と placeholder 描画もここで行われる

        SetVector();
        ResetMasterPattern3DAxes(); // (260322Ch) 結晶変更時は MasterPattern3D axes inset も描き直す
        Draw();
    }

    /// <summary>実測画像読み込みで DetWidth/DetHeight を連続設定する際の再入抑止。260724Cl 追加</summary>
    private bool skipDetectorGeometryEvent = false;

    /// <summary>サンプルや検出器の幾何学条件が変更されたとき</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    // private void numericBoxDetRadius_ValueChanged(object sender, EventArgs e) // 260723Cl 旧名: numericBoxDetRadius 廃止に伴い改名
    private void numericBoxDetectorGeometry_ValueChanged(object sender, EventArgs e)
    {
        if (skipDetectorGeometryEvent) return; // 260724Cl 追加
        UpdateEbsdTiltCoeffs(); // 260325Cl: tilt 係数を再計算
        RebinMcDistribution(); // 260723Cl 追加: 検出器ジオメトリ変更を BSE 重み分布 (8×8 ビン) にも反映
        InvalidateIndexingResults(); // 260724Cl 追加: 幾何が変わったら方位候補を失効させる (バンド検出廃止で引数レス化)
        // DrawGeometry(); // 260723Cl 削除: 直後の Draw() 内でも DrawGeometry() が呼ばれ二重描画だった
        Draw();
    }

    /// <summary>試料傾斜が変更されたとき。260723Cl 追加
    /// BSEs は MC 生成時の試料傾斜を織り込み済みのため再ビニングでは反映できない (厳密には MC 再実行が必要。既存挙動どおり stale のまま描画のみ更新)。</summary>
    private void numericBoxSampleTilt_ValueChanged(object sender, EventArgs e)
    {
        UpdateEbsdTiltCoeffs();
        InvalidateIndexingResults(); //260725Ch: SampleTilt は指数付けの lab↔sample 変換を変えるため、候補と実行中結果を失効させる
        Draw();
    }

    /// <summary>検出器ジオメトリ変更時に、保存済み BSE を新しい検出器へ再ビニングして mcDistribution を作り直す (MC 本体は再実行しない)。260723Cl 追加</summary>
    private void RebinMcDistribution()
    {
        //if (mcDistribution == null || BSEs == null || BSEs.Length == 0 || MasterPattern == null || MasterPattern.Energies.Length == 0) //260725Ch 変更前: 空Depthsだけがctorへ到達
        if (mcDistribution == null || BSEs == null || BSEs.Length == 0 || MasterPattern == null || MasterPattern.Energies.Length == 0 || MasterPattern.Depths.Length == 0) //260725Ch
            return;
        var bseRaw = BSEs.Select(e => (
            monteCarloDistributionDepthMode == MonteCarloDistributionDepthMode.LastInelasticEventDepth && e.HasLastInelasticEvent
                ? e.LastInelasticDepth
                : e.Depth,
                e.Vec, e.Energy)).ToArray();
        mcDistribution = new EbsdMonteCarloDistribution(bseRaw, Voltage, DetTilt, DetX, DetY, DetZ, DetHalfWidth, DetHalfHeight, MasterPattern.Energies, MasterPattern.Depths);
        composedPatternCache = default; // 260725Cl 追加 (/simplify): 旧 MC 分布と MasterPattern を掴んだままにしない (grid 512 で数百 MB を次のクリックまで保持していた)
    }

    /// <summary>BSE 重みを使う前に、mcDistribution の (energy × depth) 格子を現在の MasterPattern へ揃える。260727Cl 追加。
    /// MC は MasterPattern 構築とは別のタイミングでも走る (Calc BSE / 検出器幾何変更時の再ビニング / build 中止) ので、
    /// 両者の格子はずれ得る。ずれたまま weighted 合成へ渡すと添字 wIdx = ei*dLen + di が別スライスの重みを指し、
    /// 例外も警告も出さずに物理的に誤ったパターンを描く (dLen が増える方向なら IndexOutOfRange)。
    /// 保存済み BSE から再ビニングして揃え、揃えられなければ false を返す (呼び出し側は BSE 重みを使わない)。</summary>
    private bool EnsureMcDistributionMatchesMasterPattern()
    {
        if (mcDistribution == null || MasterPattern == null) return false;
        if (mcDistribution.MatchesGridOf(MasterPattern)) return true;
        RebinMcDistribution();
        return mcDistribution != null && mcDistribution.MatchesGridOf(MasterPattern);
    }

    private void FormEBSD_VisibleChanged(object sender, EventArgs e)
    {
        // 260723Cl 追加: sizeControl を graphicsBox の現在サイズで初期化 (FormDiffractionSimulator と同方式)
        if (Visible && graphicsBox.ClientSize.Width > 0 && graphicsBox.ClientSize.Height > 0)
        {
            skipViewEvent = true;
            try { sizeControl.Value = graphicsBox.ClientSize; }
            finally { skipViewEvent = false; }
        }
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

    private void numericBoxKikuchiThresholdOfStructureFactor_ValueChanged(object sender, EventArgs e) => SetVector();

    private void colorControlExcessLine_ColorChanged(object sender, EventArgs e) => Draw();

    //private void waveLengthControl_WavelengthChanged(object sender, EventArgs e) => SetVector(); //260725Ch 変更前
    private void waveLengthControl_WavelengthChanged(object sender, EventArgs e)
    {
        SetVector();
        InvalidateIndexingResults(); //260725Ch: 波長は反射カタログとpair-angle幅尤度を変えるため旧候補を失効
    }

    #endregion

    #region メイン描画関数
    /// <summary>描画関数</summary>
    public void Draw(Graphics g = null)
    {
        DrawEBSD();
        DrawOverlays(g);
        DrawGeometry();
    }
    #endregion

    #region MasterPattern から EBSD パターンを生成、描画

    bool skipEBSD_Rendering = false;

    /// <summary>直近に表示した EBSD 描画ステータス (経過時間を除いた部分)。260726Cl 追加:
    /// 同じ内容の再描画でステータスバーを書き換えないための鍵。パン・ズーム・回転が完了メッセージを潰さないようにする</summary>
    string lastEbsdRenderStatusKey = null;
    private double[] ebsdValues = []; // 260325Cl: EBSD パターン描画用バッファ (サイズ変更時のみ再割り当て)
    private (int Width, int Height) ebsdCachedSize = (0, 0); // 260325Cl: PseudoBitmap 再生成判定用
    private int masterPatternCombinationModel = 2; // (260325Ch) 0=current, 1=globally normalized master, 2=absolute MC x differential master

    /// <summary>MasterPattern → 表示ラスターの合成器 (ピクセルごとの参照テーブル + 3 つの合成モデル)。260726Cl 追加:
    /// ルックアップテーブル構築と 6 つの合成カーネルは Crystallography/EBSD/EbsdPatternComposer.cs へ移設した
    /// (GUI 非依存の純計算。ここに残るのは UI 値の読み取りと表示だけ)</summary>
    private readonly EbsdPatternComposer patternComposer = new();

    /// <summary>DetTilt/SmpTilt から回転係数を再計算する。260325Cl 追加 (260726Cl: 本体は EbsdPatternComposer へ移設)</summary>
    private void UpdateEbsdTiltCoeffs() => patternComposer.UpdateTiltCoefficients(DetTilt, SmpTilt, DetY, DetZ);

    /// <summary>EBSD パターン計算に使うラスターサイズ。260723Cl 追加
    /// 260724Cl 変更: 検出器ピクセル数 → graphicsBox の画面ピクセル数 (パターンを検出器矩形でなく視野全体に描くため)。
    /// メモリ・速度保護のため最大辺 MaxPatternRasterSize にアスペクト比を保ってクランプする。</summary>
    private const int MaxPatternRasterSize = 2048;
    private (int Width, int Height) PatternRasterSize
    {
        get
        {
            // double w = DetPixelWidth, h = DetPixelHeight; // 260724Cl 変更前: 検出器固有ピクセル
            // double w = graphicsBox.ClientSize.Width, h = graphicsBox.ClientSize.Height; // 260725Cl 変更前
            double w = CanvasSize.Width, h = CanvasSize.Height; // 260725Cl 変更: Copy 用オフスクリーン描画に対応 (通常は graphicsBox と同値)
            if (w <= 0 || h <= 0) return (0, 0);
            var max = Math.Max(w, h);
            if (max > MaxPatternRasterSize) { w *= MaxPatternRasterSize / max; h *= MaxPatternRasterSize / max; }
            return (Math.Max(1, (int)Math.Round(w)), Math.Max(1, (int)Math.Round(h)));
        }
    }

    /// <summary>ラスター (width×height) のピクセル中心を表示パターン座標 (mm、検出器中心基準) へ写す係数。260724Cl 追加
    /// px_view = (2w+1-width)·ScaleW + OffX (= viewPan.X)。ラスターは現在の視野 (ClientSize×Resolution、中心 ViewCenter) 全体をカバーする。
    /// EbsdPatternComposer の BuildLookupTable と Weighted 3 モデルの detNorm 計算で共用 (EbsdRasterView として渡す)。260726Cl: 旧名 BuildEbsdLookupTable</summary>
    private (double ScaleW, double ScaleH, double OffX, double OffY) GetRasterToViewParams(int width, int height)
        //=> (graphicsBox.ClientSize.Width * Resolution / (2.0 * width), // 260725Cl 変更前
        //    graphicsBox.ClientSize.Height * Resolution / (2.0 * height),
        => (CanvasSize.Width * Resolution / (2.0 * width), // 260725Cl 変更: Copy 用オフスクリーン描画に対応
            CanvasSize.Height * Resolution / (2.0 * height),
            viewPan.X, viewPan.Y);

    public void DrawEBSD()
    {
        // 260724Cl 追加: 検出器面が試料原点を通る退化配置 (CameraLength2≈0) では視線方向が定義できない (ゼロ長ベクトル→NaN)。
        // その場合は古いパターン画像も隠す (patternBitmap は Pbmp 借用参照なので Dispose しない)
        if (MasterPattern == null || CameraLength2 <= 1E-6) { patternBitmap = null; return; }
        if (skipEBSD_Rendering) return;

        // int width = graphicsBox.ClientRectangle.Width, height = graphicsBox.ClientRectangle.Height; // 260723Cl 変更前: 画面ピクセル=計算ピクセル (固定表示)
        var (width, height) = PatternRasterSize; // 260723Cl 変更: クランプ付きラスターで計算し、画面への配置は DrawOverlays が担う // 260724Cl: ラスター=現在の視野全体
        if (width <= 0 || height <= 0) return;

        //260717Cl 変更: 例外時に skipEBSD_Rendering が true のまま残り以後の描画が止まるため、本体を try/finally で保護
        skipEBSD_Rendering = true;
        try { DrawEBSDCore(width, height); }
        finally { skipEBSD_Rendering = false; }
    }

    private void DrawEBSDCore(int width, int height)
    {
        sw1.Restart();

        // Step 1: ルックアップテーブル構築 (方向計算 + Rosca-Lambert + 補間係数)
        //260726Cl 変更: 本体を EbsdPatternComposer へ移設。UI から読む値 (視野・検出器サイズ・左右反転) はここでまとめて渡す。
        //旧: BuildEbsdLookupTable(width, height);
        var (rasterScaleW, rasterScaleH, rasterOffX, rasterOffY) = GetRasterToViewParams(width, height);
        var rasterView = new EbsdRasterView(rasterScaleW, rasterScaleH, rasterOffX, rasterOffY, DetHalfWidth, DetHalfHeight, DetectorXMirror, DetX);
        patternComposer.BuildLookupTable(MasterPattern, Crystal.RotationMatrix, width, height, rasterView);

        var totalPixels = width * height;
        if (ebsdValues.Length != totalPixels)
            ebsdValues = new double[totalPixels];

        string statusText;

        // var useBseDistribution = checkBoxWithBSEDistribution.Checked && mcDistribution != null; // (260327Ch) // 260727Cl 変更前: 格子一致を見ておらず、MasterPattern を作り直すと別スライスの重みで合成し得た
        var useBseDistribution = checkBoxWithBSEDistribution.Checked && EnsureMcDistributionMatchesMasterPattern(); // 260727Cl

        // 260325Cl: BSE 分布を使う場合は加重平均、そうでなければ単一スライス
        if (useBseDistribution)
        {
            // Step 2a: 全エネルギー・深さの加重平均パターン
            switch (masterPatternCombinationModel)
            {
                case 1:
                    //260726Cl: 規格化係数の準備 (旧 EnsureMasterPatternGlobalNormalizationFactorsModel1) は合成器の内部で行う
                    patternComposer.ApplyWeightedModel1(ebsdValues, width, height, MasterPattern, mcDistribution, rasterView);
                    statusText = $"EBSD weighted pattern (model=1, globally normalized master, {MasterPattern.Energies.Length} energies × {MasterPattern.Depths.Length} depths), {StatusBarHelper.FormatElapsed(sw1.Elapsed)}"; // (260325Ch)
                    break;
                case 2:
                    patternComposer.ApplyWeightedModel2(ebsdValues, width, height, MasterPattern, mcDistribution, rasterView);
                    statusText = $"EBSD weighted pattern (model=2, absolute MC x differential master, {MasterPattern.Energies.Length} energies × {MasterPattern.Depths.Length} depths), {StatusBarHelper.FormatElapsed(sw1.Elapsed)}"; // (260325Ch)
                    break;
                default://0
                    patternComposer.ApplyWeightedModel0(ebsdValues, width, height, MasterPattern, mcDistribution, rasterView);
                    statusText = $"EBSD weighted pattern (model=0, current), {MasterPattern.Energies.Length} energies × {MasterPattern.Depths.Length} depths, {StatusBarHelper.FormatElapsed(sw1.Elapsed)}"; // (260325Ch)
                    break;
            }
        }
        else
        {
            // Step 2b: 単一スライス (従来動作)
            var energyIndex = trackBarOutputEnergy.Value;
            var depthIndex = trackBarOutputThickness.Value;
            if ((uint)energyIndex >= (uint)MasterPattern.Energies.Length || (uint)depthIndex >= (uint)MasterPattern.Depths.Length)
                return;//260717Cl: skipEBSD_Rendering の復元は DrawEBSD 側の finally に集約

            var mp = MasterPattern;
            var posPlane = mp.GetPlane(MasterPattern.Hemisphere.PositiveZ, energyIndex, depthIndex);
            var negPlane = mp.GetPlane(MasterPattern.Hemisphere.NegativeZ, energyIndex, depthIndex);
            var energy = MasterPattern.Energies[energyIndex];
            var depth = MasterPattern.Depths[depthIndex];
            switch (masterPatternCombinationModel)
            {
                case 1:
                    var planeIndex = energyIndex * mp.Depths.Length + depthIndex; // (260325Ch) model 1 の規格化係数参照用
                    //260726Cl: 係数配列の保持と範囲外ガードは合成器側へ (旧 masterPatternGlobalNormalizationFactors の直参照)
                    var planeScaleFactor = patternComposer.GetGlobalNormalizationFactorModel1(mp, planeIndex);
                    patternComposer.ApplySingleSliceModel1(ebsdValues, totalPixels, posPlane, negPlane, planeScaleFactor);
                    statusText = $"EBSD from MasterPattern (model=1): E={energy:g} keV, depth={depth:g} nm, {StatusBarHelper.FormatElapsed(sw1.Elapsed)}"; // (260325Ch)
                    break;
                case 2:
                    var posPlanePrevious = depthIndex > 0 ? mp.GetPlane(MasterPattern.Hemisphere.PositiveZ, energyIndex, depthIndex - 1) : null; // (260325Ch)
                    var negPlanePrevious = depthIndex > 0 ? mp.GetPlane(MasterPattern.Hemisphere.NegativeZ, energyIndex, depthIndex - 1) : null; // (260325Ch)
                    patternComposer.ApplySingleSliceModel2(ebsdValues, totalPixels, posPlane, negPlane, posPlanePrevious, negPlanePrevious);
                    statusText = $"EBSD from MasterPattern (model=2): E={energy:g} keV, depth slice={depth:g} nm, {StatusBarHelper.FormatElapsed(sw1.Elapsed)}"; // (260325Ch)
                    break;
                default:
                    // ApplyEbsdLookupSingleSlice(ebsdValues, totalPixels, posPlane, negPlane); // (260325Ch) 旧実装
                    patternComposer.ApplySingleSliceModel0(ebsdValues, totalPixels, posPlane, negPlane);
                    statusText = $"EBSD from MasterPattern (model=0): E={energy:g} keV, depth={depth:g} nm, {StatusBarHelper.FormatElapsed(sw1.Elapsed)}"; // (260325Ch)
                    break;
            }
        }

        // Step 3: 表示
        if (Pbmp == null || ebsdCachedSize.Width != width || ebsdCachedSize.Height != height)
        {
            Pbmp?.Dispose();
            //260717Cl 変更: Enumerable.Repeat の逐次列挙を Array.Fill + 一括コピーに
            var alpha = new byte[totalPixels];
            Array.Fill(alpha, (byte)255);
            Pbmp = new PseudoBitmap(ebsdValues, width) { AlphaEnabled = true, FilterAlfha = [.. alpha] };
            ebsdCachedSize = (width, height);
            // tabPageOutputParameter.Enabled = true; // 260724Cl 削除: 有効化は MasterPattern 構築完了時 (MasterPatternCompleted ハンドラ) に一本化
        }
        else
            Pbmp.SrcValuesGray = Pbmp.SrcValuesGrayOriginal = ebsdValues;

        #region 画像のコントラストやスケールを設定
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

        var (min, max) = Pbmp.SrcValuesGray.MinMax();//260717Cl 変更: Max()+Min() の 2 走査を 1 走査に
        var dev = max - min;

        Pbmp.MaxValue = dev * maxRatio + min;
        Pbmp.MinValue = dev * minRatio + min;

        #endregion

        // 260723Cl 変更: ここでは画面へ直接描かず、1:1 の検出器ビットマップを更新するだけにする。
        // 画面への配置 (ズーム・パン・検出器矩形位置) は DrawOverlays 側の DrawImage が担う。
        //if (checkBoxShowDyanmicalEBSD.Checked && Pbmp != null)
        //{
        //    var graphics = graphicsBox.Graphics;
        //    graphics.SmoothingMode = SmoothingMode.None;
        //    graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
        //    graphics.PixelOffsetMode = PixelOffsetMode.Half;

        //    var bmp = Pbmp.GetImage(new RectangleD(0, 0, Pbmp.Width, Pbmp.Height), graphicsBox.ClientSize);
        //    graphics.DrawImage(bmp, new RectangleD(-DetR, -DetR - Foot.Y, DetR * 2, DetR * 2));
        //}
        if (Pbmp != null)
        {
            // patternBitmap?.Dispose(); // 260724Cl 削除: GetImage は Pbmp 内部キャッシュ (destBmp) と同一インスタンスを返すため、
            //   ここで破棄すると次回 GetImage の destBmp.Width 参照が ArgumentException (Parameter is not valid) になる。
            //   所有権は PseudoBitmap 側 (Pbmp.Dispose() が解放) にあり、呼び出し側は参照を保持するだけにする。
            patternBitmap = Pbmp.GetImage(new RectangleD(0, 0, Pbmp.Width, Pbmp.Height), new Size(width, height));
            // 260724Cl 追加: このビットマップが表す表示パターン座標 (mm) の矩形 = 生成時の視野を記録。
            // パン中は再計算せず旧視野の矩形へ貼ることで、画像がマウスに追従する (露出部は背景色、確定時に再計算)。
            //260726Cl: メソッド先頭で作った rasterView と同じ値なので再計算しない (旧: GetRasterToViewParams をここでもう一度呼んでいた)
            patternBitmapRect = new RectangleD(
                DetectorCenterView.X + rasterOffX - rasterScaleW * width, DetectorCenterView.Y + rasterOffY - rasterScaleH * height,
                2 * rasterScaleW * width, 2 * rasterScaleH * height);
        }

        // toolStripStatusLabelProgress.Text = statusText; // 260406Cl Label1は進捗専用に整理。描画結果の説明はLabel2+Label3へ分割
        //260726Cl 変更 (作者報告: 較正・指数付けの結果が読めない): 旧コードは描画のたびに Summary/Detail を書いていたため、
        //パン・ズーム・リサイズ・回転で走る再描画が、完了メッセージを即座に潰していた。パターンの中身 (モデル・エネルギー・深さ) が
        //変わったときだけ書く = 表示は常に最新の「出来事」になる。
        //旧: toolStripStatusLabelSummary.Text = "EBSD rendering"; toolStripStatusLabelDetail.Text = statusText;
        int elapsedSeparator = statusText.LastIndexOf(','); //末尾の経過時間は毎回変わるので鍵から外す
        string statusKey = elapsedSeparator > 0 ? statusText[..elapsedSeparator] : statusText;
        if (statusKey != lastEbsdRenderStatusKey)
        {
            lastEbsdRenderStatusKey = statusKey;
            toolStripStatusLabelSummary.Text = "EBSD rendering";
            toolStripStatusLabelDetail.Text = statusText;
        }
    }

    #endregion

    #region プロジェクション行列の設定
    /// <summary>プロジェクション行列の設定を行う。</summary>
    public bool SetProjection(Graphics g = null)
    {
        // if (g != null && graphicsBox.ClientSize.Width != 0 && graphicsBox.ClientSize.Height != 0) // 260725Cl 変更前
        if (g != null && CanvasSize.Width != 0 && CanvasSize.Height != 0) // 260725Cl 変更: Copy 用オフスクリーン描画に対応
            try
            {
                //g.Transform = new Matrix(...); // (260611Ch) 旧: Matrix が未解放
                // 260723Cl 変更: 平行移動項を Foot 固定 (+Foot/Res) から ViewCenter (ズーム・パン対応。既定は検出器中心=旧挙動) へ
                using var transform = new Matrix( // (260611Ch)
                (float)(1 / Resolution), 0, 0, (float)(1 / Resolution),
                //(float)(graphicsBox.ClientSize.Width / 2.0 - ViewCenter.X / Resolution), // 260725Cl 変更前
                //(float)(graphicsBox.ClientSize.Height / 2.0 - ViewCenter.Y / Resolution));
                (float)(CanvasSize.Width / 2.0 - ViewCenter.X / Resolution), // 260725Cl 変更
                (float)(CanvasSize.Height / 2.0 - ViewCenter.Y / Resolution));
                g.Transform = transform; // (260611Ch)
            }
            catch { return false; }
        return true;
    }
    #endregion

    #region DrawOverlay() 補助図形をGraphicBoxに描画

    /// <summary></summary>
    /// <param name="graphics"></param>
    //260725Cl シグネチャ変更 (suppressDetectorOutline 追加: Detector 範囲コピー時は黄色い検出器外枠がコピー縁と一致するため描かない。作者指示)。
    //旧: private void DrawOverlays(Graphics graphics = null, int i = -1, int j = -1)
    private void DrawOverlays(Graphics graphics = null, int i = -1, int j = -1, bool suppressDetectorOutline = false)
    {
        // 260724Cl 追加: InitializeComponent 中 (graphicsBox.Resize 発火時) は FormMain 未代入のため描画しない (Crystal => FormMain.Crystal が NRE)
        if (FormMain == null) return;
        if (InvokeRequired)//別スレッドから呼び出されたとき Invokeして呼びなおす
        {
            Invoke(new Action(() => DrawOverlays(graphics, i, j, suppressDetectorOutline)), null); // 260725Cl: 引数を引き継ぐ
            return;
        }
        //グラフィックスボックスに描画する場合
        graphics ??= graphicsBox.Graphics;
        if (!SetProjection(graphics))
            return;

        double xm = DetectorXMirror; // 260718Cl: 左右反転。オーバーレイ (ゾーン軸ラベル・菊池線) の X 投影へパターンと一貫して掛ける
        double halfW = DetHalfWidth, halfH = DetHalfHeight; // 260723Cl 追加
        double detCx = DetectorCenterView.X, detCy = DetectorCenterView.Y; // 260723Cl 追加: 検出器矩形の表示中心

        // if (!checkBoxShowDyanmicalEBSD.Checked || Pbmp == null)
        //     graphics.Clear(colorControlBackGround.Color); // 260723Cl 変更前: EBSD 画像が全画面を覆う前提でクリアを省略していた
        // 260723Cl 変更: パン中などで画像が画面全体を覆うとは限らないため常に背景をクリアしてから配置する
        graphics.Clear(colorControlBackGround.Color);
        if (checkBoxShowDyanmicalEBSD.Checked && patternBitmap != null)
        {
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphics.PixelOffsetMode = PixelOffsetMode.Half;
            // graphics.DrawImage(patternBitmap, new RectangleD(detCx - halfW, detCy - halfH, halfW * 2, halfH * 2)); // 260724Cl 変更前: 検出器矩形にのみ描画
            graphics.DrawImage(patternBitmap, patternBitmapRect); // 260724Cl 変更: パターンは視野全体に描画 (矩形は生成時の視野)
        }

        // 260724Cl 追加: 実測 EBSD 画像を検出器矩形へ重ねる (シミュレーションの上、菊池線等オーバーレイの下)。
        // 左右反転 (xm) は実測画像のピクセル順には適用しない (実測が基準で、シミュレーション側を xm で合わせる思想)。
        // Show overlays チェックとは独立した画像レイヤーとして扱う。
        if (checkBoxShowExperimentalImage.Checked && expImage != null)
        {
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphics.PixelOffsetMode = PixelOffsetMode.Half;
            using var ia = new ImageAttributes();
            ia.SetColorMatrix(new ColorMatrix { Matrix33 = trackBarExpImageOpacity.Value / (float)trackBarExpImageOpacity.Maximum });
            var dest = new PointF[] { // 左上、右上、左下の順 (mm 座標。Graphics.Transform が画面へ写す)
                new((float)(detCx - halfW), (float)(detCy - halfH)),
                new((float)(detCx + halfW), (float)(detCy - halfH)),
                new((float)(detCx - halfW), (float)(detCy + halfH)) };
            graphics.DrawImage(expImage, dest, new RectangleF(0, 0, expImage.Width, expImage.Height), GraphicsUnit.Pixel, ia);
        }

        //DrawDetectedBands(graphics); //260724Cl 廃止: バンド検出と中心線オーバーレイの撤廃 (作者指示。方位探索は Radon テンプレート照合へ一本化)

        if (checkBoxShowOverlays.Checked)
        {
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //var penExcess = new Pen(new SolidBrush(colorControlExcessLine.Color), (float)(trackBarLineWidth.Value * Resolution / 2000f)); // (260611Ch) 旧: Pen/内部 SolidBrush が未解放
            using var penExcess = new Pen(colorControlExcessLine.Color, (float)(trackBarLineWidth.Value * Resolution / 2000f)); // (260611Ch)
            // var diag = Resolution * Math.Sqrt(graphicsBox.ClientSize.Width * graphicsBox.ClientSize.Width + graphicsBox.ClientSize.Height * graphicsBox.ClientSize.Height) / 2; // 260725Cl 変更前
            var diag = Resolution * Math.Sqrt(CanvasSize.Width * CanvasSize.Width + CanvasSize.Height * CanvasSize.Height) / 2; // 260725Cl 変更: Copy 用オフスクリーン描画に対応
            //var font = new Font(WineCompat.Resolve("Tahoma"), (float)(trackBarStrSize.Value / 8.0 * Resolution)); //260610Cl Wine時フォント切替 // (260611Ch) 旧: 未解放
            using var font = new Font(WineCompat.Resolve("Tahoma"), (float)(trackBarStrSize.Value / 8.0 * Resolution)); //260610Cl Wine時フォント切替 // (260611Ch)
            using var brush = new SolidBrush(colorControlString.Color); // (260611Ch)

            var Tau = numericBoxDetTilt.RadianValue - numericBoxSampleTilt.RadianValue;

            #region 菊池線の表示と菊池線指数の表示
            if (checkBoxShowKikuchiLines.Checked || checkBoxShowGIndices.Checked)
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
                            var pt = new PointD(xm * (cosPsi * x - sinPsi * (y - Y)), sinPsi * x + cosPsi * (y - Y)); // 260718Cl: 左右反転 xm を X に

                            if (IsScreenArea(pt))
                                pts.Add(pt);
                        }

                        if (pts.Count > 1)
                        {
                            //菊池線描画
                            if (checkBoxShowKikuchiLines.Checked)
                            {
                                if (checkBoxKikuchiLine_Kinematical.Checked)
                                    penExcess.Color = Blend(colorControlExcessLine.Color, colorControlBackGround.Color, g.RelativeIntensity);
                                graphics.DrawLines(penExcess, pts.ToArray());
                            }


                            //ラベル描画
                            if (checkBoxShowGIndices.Checked) // 260331Cl checkBoxShowGIndices で制御
                            {
                                //まず傾きをみて線のどちら側にラベルを付けるかを決める。θは -π ~ +πの範囲で調節
                                using var original = graphics.Transform;//260717Cl 追加: Transform getter が返す Matrix コピーを復元後に解放
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
            #endregion

            #region 晶帯軸ラベルをバンド交点に表示
            // if (checkBoxShowZoneAxisIndices.Checked && checkBoxShowOverlays.Checked && Crystal.VectorOfG_KikuchiLine.Count >= 2) // 260725Ch 変更前
            if (checkBoxShowZoneAxisIndices.Checked && checkBoxShowOverlays.Checked && Crystal.VectorOfG_KikuchiLine.Count >= 2 && CameraLength2 > 1E-6) // 260725Ch: 退化配置で全ラベルが原点へ重なるのを防ぐ
            {
                var rot = Crystal.RotationMatrix;
                var rotTau = Matrix3D.Rot(new Vector3DBase(1, 0, 0), -Tau);
                // double detectorPlaneZ = DetY * Math.Sin(DetTilt) - DetZ * Math.Cos(DetTilt); // 260725Ch // 260725Cl 変更前: CameraLength2 と同一式の手書き
                double detectorPlaneZ = SignedCameraLength; // 260725Ch: 検出器法線をZ軸へ合わせた座標での検出器面の符号付きZ位置 // 260725Cl: プロパティ化

                var drawnZoneAxes = new HashSet<(int, int, int)>();
                var gList = Crystal.VectorOfG_KikuchiLine;
                for (int ii = 0; ii < gList.Count - 1; ii++)
                    for (int jj = ii + 1; jj < gList.Count; jj++)
                    {
                        var g1 = gList[ii]; var g2 = gList[jj];
                        #region 260725Ch 変更前: 実数外積を最大指数12までの整数へ近似していたため、高指数の晶帯軸を誤る場合があった
                        //// g ベクトルの外積 → 晶帯軸方向（直交座標系、回転前）
                        //var cross = Vector3DBase.VectorProduct(g1, g2);
                        //if (cross.Length2 < 1e-20) continue;

                        //// 指数 [uvw] を求める: cross (直交座標) = u*A_Axis + v*B_Axis + w*C_Axis
                        //// → (u,v,w) = MatrixReal⁻¹ * cross = MatrixInverse * cross
                        //var uvwVec = Crystal.MatrixInverse * cross;
                        //double maxComp = Math.Max(Math.Max(Math.Abs(uvwVec.X), Math.Abs(uvwVec.Y)), Math.Abs(uvwVec.Z));
                        //if (maxComp < 1e-10) continue;
                        //double scale = 1.0 / maxComp;
                        //int bestU = 0, bestV = 0, bestW = 0;
                        //double bestError = double.MaxValue;
                        //for (int m = 1; m <= 12; m++)
                        //{
                        //    double su = uvwVec.X * scale * m, sv = uvwVec.Y * scale * m, sw = uvwVec.Z * scale * m;
                        //    int ru2 = (int)Math.Round(su), rv2 = (int)Math.Round(sv), rw2 = (int)Math.Round(sw);
                        //    double err = Math.Abs(su - ru2) + Math.Abs(sv - rv2) + Math.Abs(sw - rw2);
                        //    if (err < bestError) { bestError = err; bestU = ru2; bestV = rv2; bestW = rw2; }
                        //    if (err < 0.05) break;
                        //}
                        #endregion

                        var (h1, k1, l1) = g1.Index; var (h2, k2, l2) = g2.Index;
                        int bestU = k1 * l2 - l1 * k2, bestV = l1 * h2 - h1 * l2, bestW = h1 * k2 - k1 * h2; // 260725Ch: Stereonet と同じ整数外積で晶帯軸を厳密に算出
                        if (bestU == 0 && bestV == 0 && bestW == 0) continue;
                        // static int Gcd(int a, int b) { ... } / int gcd = Gcd(Gcd(|u|,|v|), |w|); // 260725Cl 変更前: 自前 gcd
                        int gcd = Algebra.Irreducible(bestU, bestV, bestW); // 260725Cl 変更 (/simplify): 約分は既存の正典ヘルパへ (FormStereonet の整数外積と同じ組み合わせ)
                        if (gcd > 1) { bestU /= gcd; bestV /= gcd; bestW /= gcd; }
                        if (bestU < 0 || (bestU == 0 && bestV < 0) || (bestU == 0 && bestV == 0 && bestW < 0))
                        { bestU = -bestU; bestV = -bestV; bestW = -bestW; }
                        if (!drawnZoneAxes.Add((bestU, bestV, bestW))) continue;//260717Cl: Contains+Add を Add 戻り値判定に一本化

                        // 晶帯軸方向を検出器座標に投影（実空間ベクトルを使用）
                        var zoneAxisReal = bestU * Crystal.A_Axis + bestV * Crystal.B_Axis + bestW * Crystal.C_Axis;
                        var dir = rotTau * (rot * zoneAxisReal);
                        if (Math.Abs(dir.Z) < 1e-15) continue;

                        // 260725Ch: 重複排除用の正規化指数 (bestU/V/W) とは別に、正の交点パラメータで検出器面へ到達する物理方向をラベルに出す
                        // 260725Cl 変更 (/simplify): displayU/V/W の 3 変数+if ブロックを符号 1 個に集約し、
                        // 続く「if (dir.Z < 0) dir = -dir;」を削除 — 下の投影式は dir.X/dir.Z と dir.Y/dir.Z のみで dir→−dir に不変 (完全な no-op だった)
                        int sgn = detectorPlaneZ * dir.Z < 0 ? -1 : 1;

                        // 中心投影: EBSD は試料側から見た図形なので X 方向を反転 (EbsdPatternComposer.BuildLookupTable の ax = -Ri.E11 と整合)
                        double detX = -xm * CameraLength2 * dir.X / dir.Z; // 260718Cl: 左右反転 xm
                        double detY2 = CameraLength2 * dir.Y / dir.Z;

                        var ptZA = new PointD(detX, detY2);
                        if (!IsScreenArea(ptZA, -20)) continue;

                        // string label = $"[{bestU} {bestV} {bestW}]"; // 260725Ch 変更前
                        string label = $"[{sgn * bestU} {sgn * bestV} {sgn * bestW}]"; // 260725Ch // 260725Cl: displayU/V/W → sgn
                        var size = graphics.MeasureString(label, font);
                        graphics.DrawString(label, font, brush, new PointF((float)ptZA.X - size.Width / 2, (float)ptZA.Y - size.Height / 2));
                    }
            }
            #endregion

            #region 検出器のアウトラインを表示
            if (checkBoxDrawDetectorOutline.Checked)
            {
                //検出器を示す外枠を描画 // 260723Cl 変更: 円 (DrawArc) → 矩形 (halfW×halfH)
                // if (checkBoxShowCircle.Checked) // 260725Cl 変更前
                if (checkBoxShowCircle.Checked && !suppressDetectorOutline) //260725Cl 変更: Detector 範囲コピーでは外枠がコピー縁と一致するため含めない
                {
                    using var outlinePen = new Pen(Color.Yellow, ResolutionF * 2); // (260611Ch)
                    // graphics.DrawArc(outlinePen, -DetR, -DetR - Foot.Y, DetR * 2, DetR * 2, 0, 360); // 260723Cl 変更前
                    graphics.DrawRectangle(outlinePen, (float)(detCx - halfW), (float)(detCy - halfH), (float)(halfW * 2), (float)(halfH * 2));
                }
                //検出器の分割線
                if (checkBoxShowMesh.Checked)
                {
                    using var meshPen = new Pen(Color.Orange, ResolutionF); // (260611Ch)
                    for (int n = 0; n < DetectorDivision; n++)
                    {
                        var x = 2.0 * n / DetectorDivision - 1;
                        // graphics.DrawLine(meshPen, -DetR, x * DetR - Foot.Y, DetR, x * DetR - Foot.Y); // 260723Cl 変更前: DetR 正方形基準
                        graphics.DrawLine(meshPen, detCx - halfW, detCy + x * halfH, detCx + halfW, detCy + x * halfH); // 260723Cl
                        graphics.DrawLine(meshPen, detCx + x * halfW, detCy - halfH, detCx + x * halfW, detCy + halfH); // 260723Cl
                    }
                    if ((uint)i < (uint)DetectorDivision && (uint)j < (uint)DetectorDivision)
                    {
                        double x = 2.0 * i / DetectorDivision - 1, y = 2.0 * j / DetectorDivision - 1;

                        using var selectedCellBrush = new SolidBrush(Color.FromArgb(32, Color.Orange)); // (260611Ch)
                        // graphics.FillRectangle(selectedCellBrush, DetR * x, DetR * y - Foot.Y, 2 * DetR / DetectorDivision, 2 * DetR / DetectorDivision); // 260723Cl 変更前
                        graphics.FillRectangle(selectedCellBrush, detCx + halfW * x, detCy + halfH * y, 2 * halfW / DetectorDivision, 2 * halfH / DetectorDivision); // 260723Cl
                    }
                }
            }
            #endregion
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
            //&& clientPt.X < graphicsBox.ClientRectangle.Width - margin // 260725Cl 変更前
            //&& clientPt.Y < graphicsBox.ClientRectangle.Height - margin;
            && clientPt.X < CanvasSize.Width - margin // 260725Cl 変更: Copy 用オフスクリーン描画に対応
            && clientPt.Y < CanvasSize.Height - margin;
    }

    /// <summary>フィルム(Src)上の位置 (mm)を座標系変換 画面(Client)上の点(pixel)に変換</summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private PointD convertDetectorToScreen(in double x, in double y)
    {
        // 260723Cl 変更: +Foot 固定 → ViewCenter (ズーム・パン対応。SetProjection の Transform と同一の変換)
        // double px = (x + Foot.X) / Resolution + graphicsBox.ClientSize.Width / 2.0; // 260723Cl 変更前
        // double py = (y + Foot.Y) / Resolution + graphicsBox.ClientSize.Height / 2.0; // 260723Cl 変更前
        // double px = (x - ViewCenter.X) / Resolution + graphicsBox.ClientSize.Width / 2.0; // 260725Cl 変更前
        // double py = (y - ViewCenter.Y) / Resolution + graphicsBox.ClientSize.Height / 2.0;
        double px = (x - ViewCenter.X) / Resolution + CanvasSize.Width / 2.0; // 260725Cl 変更: Copy 用オフスクリーン描画に対応
        double py = (y - ViewCenter.Y) / Resolution + CanvasSize.Height / 2.0;
        return new(px, py);
    }

    /// <summary>検出器(Detector)上の位置 (mm)を画面(Screen)上の点(pixel)に変換</summary>
    /// <param name="pt"></param>
    /// <returns></returns>
    private PointD convertDetectorToScreen(in PointD pt) => convertDetectorToScreen(pt.X, pt.Y);

    /// <summary>画面(Screen)上の点(pixel)を表示パターン座標 (mm) に変換 (convertDetectorToScreen の逆変換)。260723Cl 追加</summary>
    // 260725Cl 変更: 基準を graphicsBox.ClientSize → CanvasSize へ (順変換 convertDetectorToScreen 側だけがオフスクリーン対応になり規約が割れていた。
    // 呼び出しはマウスハンドラのみでオフスクリーン描画中は発火しないため挙動は不変だが、対を成す 2 メソッドの基準を揃える)
    private PointD convertScreenToDetector(in PointD pt)
        => new((pt.X - CanvasSize.Width / 2.0) * Resolution + ViewCenter.X,
               (pt.Y - CanvasSize.Height / 2.0) * Resolution + ViewCenter.Y);
    #endregion

    #region 菊池線 graphicsBoxのイベント (graphicsBox上のマウスイベントも含む)

    // 260723Cl 追加: 右ドラッグ=拡大 (ラバーバンド)・右クリック=縮小・中ドラッグ=平行移動 (ScalablePictureBox と同じ操作系) のための状態
    private bool mouseRangeMode = false;
    private Point mouseRangeStart, mouseRangeEnd;
    private Point panLastPoint;

    private void graphicsBox_MouseDown(object sender, MouseEventArgs e)
    {
        // 260723Cl 追加: 右ボタン=ズーム操作開始、中ボタン=平行移動開始
        if (e.Button == MouseButtons.Right && e.Clicks == 1)
        {
            mouseRangeMode = true;
            mouseRangeStart = mouseRangeEnd = e.Location;
            return;
        }
        if (e.Button == MouseButtons.Middle)
        {
            panLastPoint = e.Location;
            return;
        }

        if (e.Button == MouseButtons.Left && e.Clicks == 2) // 260723Cl 変更: 左ボタンに限定 (旧: ボタン不問)
        {
            // 260723Cl 変更: ズーム・パン対応のため、画面ピクセル比ではなく画面→表示パターン座標→検出器矩形内セルの逆変換で求める
            //var size = graphicsBox.ClientSize;
            //var i = e.Location.X * DetectorDivision / size.Width;
            //var j = e.Location.Y * DetectorDivision / size.Height;
            var det = convertScreenToDetector(new PointD(e.X, e.Y));
            double halfW = DetHalfWidth, halfH = DetHalfHeight;
            var i = (int)Math.Floor((det.X - DetectorCenterView.X + halfW) / (2 * halfW) * DetectorDivision);
            var j = (int)Math.Floor((det.Y - DetectorCenterView.Y + halfH) / (2 * halfH) * DetectorDivision);
            if ((uint)i < (uint)DetectorDivision && (uint)j < (uint)DetectorDivision)
            {
                // 260723Cl 追加: i は表示セル。左右反転 (xm=-1) 時、物理検出器 (DrawGeometry/CalcStatistics の f1 座標) では X が逆順になる
                var iPhysical = DetectorXMirror > 0 ? i : DetectorDivision - 1 - i;
                DrawOverlays(null, i, j);
                DrawGeometry(iPhysical, j);
                CalcStatistics(iPhysical, j);
            }
        }
    }

    private void graphicsBox_MouseUp(object sender, MouseEventArgs e)
    {
        // 260723Cl 追加: 右ボタン確定 — 微小移動なら縮小、十分な矩形なら拡大 (閾値は ScalablePictureBox と同じ)
        if (mouseRangeMode && e.Button == MouseButtons.Right)
        {
            mouseRangeMode = false;
            mouseRangeEnd = e.Location;
            int dx = Math.Abs(mouseRangeStart.X - mouseRangeEnd.X), dy = Math.Abs(mouseRangeStart.Y - mouseRangeEnd.Y);
            if (dx < 3 && dy < 3)
                //縮小: クリック点を新しい画面中心にして表示解像度 (mm/px) を 2 倍
                SetView(convertScreenToDetector(new PointD(e.X, e.Y)), Resolution * 2);
            else if (dx > 10 && dy > 10)
            {
                //拡大: 選択矩形全体が画面に収まる解像度へ (縦横比は保持)
                var center = convertScreenToDetector(new PointD((mouseRangeStart.X + mouseRangeEnd.X) / 2.0, (mouseRangeStart.Y + mouseRangeEnd.Y) / 2.0));
                SetView(center, Resolution * Math.Max((double)dx / graphicsBox.ClientSize.Width, (double)dy / graphicsBox.ClientSize.Height));
            }
            else
                graphicsBox.Invalidate(); //ラバーバンド消去のみ
            return;
        }
        // if (e.Button == MouseButtons.Middle) return; // 260723Cl 追加: パンは MouseMove で描画済み // 260724Cl 変更前
        if (e.Button == MouseButtons.Middle) { DrawEBSD(); DrawOverlays(); return; } // 260724Cl 変更: パン確定時に新しい視野でパターンを再計算 (ドラッグ中は旧視野の画像が追従)

        Draw();
    }

    /// <summary>表示中心 (表示パターン座標 mm) と表示解像度 (mm/px) を設定し、numericBoxResolution へ書き戻して再描画する。260723Cl 追加</summary>
    private void SetView(PointD centerView, double resolution)
    {
        resolution = Math.Clamp(resolution, numericBoxResolution.Minimum > 0 ? numericBoxResolution.Minimum : 1E-4, numericBoxResolution.Maximum);
        viewPan = new PointD(centerView.X - DetectorCenterView.X, centerView.Y - DetectorCenterView.Y);
        skipViewEvent = true;
        try { numericBoxResolution.Value = resolution; }
        finally { skipViewEvent = false; }
        // DrawOverlays(); // 260724Cl 変更前
        DrawEBSD(); DrawOverlays(); // 260724Cl 変更: パターンは視野依存になったため、ズーム確定時に再計算する
    }

    /// <summary>ラバーバンド矩形 (右ドラッグのズーム範囲) を描画バッファより手前に描画する。260723Cl 追加</summary>
    private void graphicsBox_PaintOverlay(object sender, PaintEventArgs e)
    {
        if (!mouseRangeMode) return;
        using var pen = new Pen(Color.Pink) { DashStyle = DashStyle.Dash };
        e.Graphics.DrawRectangle(pen, Math.Min(mouseRangeStart.X, mouseRangeEnd.X), Math.Min(mouseRangeStart.Y, mouseRangeEnd.Y),
            Math.Abs(mouseRangeStart.X - mouseRangeEnd.X), Math.Abs(mouseRangeStart.Y - mouseRangeEnd.Y));
    }

    private PointD lastMousePos = new();

    private void graphicsBox_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
    {
        var mousePos = new PointD(e.X, e.Y);

        // 260723Cl 追加: 右ドラッグ中はラバーバンド更新のみ
        if (mouseRangeMode)
        {
            mouseRangeEnd = e.Location;
            graphicsBox.Invalidate();
            lastMousePos = mousePos;
            return;
        }
        // 260723Cl 追加: 中ドラッグで平行移動 (画像内容がマウスに追従)。パターン再計算は不要なので DrawOverlays のみ
        if (e.Button == MouseButtons.Middle)
        {
            viewPan = new PointD(viewPan.X - (e.X - panLastPoint.X) * Resolution, viewPan.Y - (e.Y - panLastPoint.Y) * Resolution);
            panLastPoint = e.Location;
            DrawOverlays();
            lastMousePos = mousePos;
            return;
        }

        //左ボタンが押されながらマウスが動いたとき
        if (e.Button == MouseButtons.Left)
        {
            double xm = DetectorXMirror; // 260724Cl 追加: 左右反転表示 (Flip L-R) 時はドラッグ X 成分と回転向きも反転しないと操作が逆になる
            var center = new PointD(graphicsBox.ClientSize.Width / 2.0, graphicsBox.ClientSize.Height / 2.0);
            if ((e.X - graphicsBox.ClientSize.Width / 2) * (e.X - graphicsBox.ClientSize.Width / 2) + (e.Y - graphicsBox.ClientSize.Height / 2) * (e.Y - graphicsBox.ClientSize.Height / 2)
                < Math.Min(graphicsBox.ClientSize.Width, graphicsBox.ClientSize.Height) * Math.Min(graphicsBox.ClientSize.Width, graphicsBox.ClientSize.Height) * 0.18)
            {
                if (mousePos != lastMousePos)
                {
                    var devPos = mousePos - lastMousePos;
                    var devAngle = Math.Atan((mousePos - lastMousePos).Length * Resolution / CameraLength2);
                    // FormMain.Rotate((-1 * devPos.Y, -Math.Cos(SmpTilt - DetTilt) * devPos.X, Math.Sin(SmpTilt - DetTilt) * devPos.X), devAngle); // 260724Cl 変更前: xm 未適用
                    FormMain.Rotate((-1 * devPos.Y, -Math.Cos(SmpTilt - DetTilt) * xm * devPos.X, Math.Sin(SmpTilt - DetTilt) * xm * devPos.X), devAngle); // 260724Cl
                }
            }
            else
                // FormMain.Rotate((0, Math.Sin(SmpTilt - DetTilt), Math.Cos(SmpTilt - DetTilt)), -Math.Atan2(lastMousePos.X - center.X, lastMousePos.Y - center.Y) + Math.Atan2(mousePos.X - center.X, mousePos.Y - center.Y)); // 260724Cl 変更前: xm 未適用
                FormMain.Rotate((0, Math.Sin(SmpTilt - DetTilt), Math.Cos(SmpTilt - DetTilt)), xm * (-Math.Atan2(lastMousePos.X - center.X, lastMousePos.Y - center.Y) + Math.Atan2(mousePos.X - center.X, mousePos.Y - center.Y))); // 260724Cl
            //Draw関数は、FormMain.Rotateを呼び出した後、FormMainから呼ばれる
        }
        lastMousePos = mousePos;
    }

    // private void graphicsBox_Resize(object sender, EventArgs e) => Draw(); // 260723Cl 変更前 (Designer 未接続のデッドコードだった)
    // 260723Cl 変更: sizeControl へ書き戻し、表示のみ更新 (パターンラスターは画面サイズ非依存になったため再計算不要)
    private void graphicsBox_Resize(object sender, EventArgs e)
    {
        if (graphicsBox.ClientSize.Width <= 0 || graphicsBox.ClientSize.Height <= 0) return; //最小化時など
        skipViewEvent = true;
        try { sizeControl.Value = graphicsBox.ClientSize; }
        finally { skipViewEvent = false; }
        // DrawOverlays(); // 260724Cl 変更前
        DrawEBSD(); DrawOverlays(); // 260724Cl 変更: ラスター=視野全体のためリサイズで再計算する
    }

    /// <summary>表示解像度 (ズーム) の numericBox が変更されたとき。260723Cl 追加
    /// 260724Cl 変更: パターンは視野依存になったため再計算も行う。</summary>
    private void numericBoxResolution_ValueChanged(object sender, EventArgs e)
    {
        if (skipViewEvent) return;
        DrawEBSD(); DrawOverlays();
    }

    /// <summary>sizeControl → graphicsBox サイズ同期。Dock=Fill を維持したままフォームサイズを差分調整する (FormDiffractionSimulator と同方式)。260723Cl 追加</summary>
    private void sizeControl_ValueChanged(object sender, EventArgs e)
    {
        if (skipViewEvent) return;
        var dW = sizeControl.ImageWidth - graphicsBox.ClientSize.Width;
        var dH = sizeControl.ImageHeight - graphicsBox.ClientSize.Height;
        Size = new Size(Size.Width + dW, Size.Height + dH);
    }

    #endregion graphicsBoxのイベント

    #region 実測 EBSD 画像の読み込み (D&D) と表示調整 260724Cl 追加

    /// <summary>D&amp;D で受け付ける実測画像の拡張子 (要件: tiff/png/bmp/jpg)。260724Cl 追加</summary>
    private static bool IsExperimentalImageFile(string path)
        => Path.GetExtension(path).ToLower() is ".tif" or ".tiff" or ".png" or ".bmp" or ".jpg" or ".jpeg";

    private void FormEBSD_DragEnter(object sender, DragEventArgs e)
        => e.Effect = e.Data?.GetData(DataFormats.FileDrop) is string[] files && files.Any(IsExperimentalImageFile)
            ? DragDropEffects.Copy : DragDropEffects.None;

    private void FormEBSD_DragDrop(object sender, DragEventArgs e)
    {
        // 複数ファイル時はドロップ順で最初の対応画像だけを読む
        if (e.Data?.GetData(DataFormats.FileDrop) is string[] files && files.FirstOrDefault(IsExperimentalImageFile) is string file)
            ReadExperimentalImage(file);
    }

    /// <summary>実測 EBSD 画像を読み込み、検出器のピクセル数 (DetWidth/DetHeight) を画像サイズに合わせ、検出器全体が収まる表示にして再描画する。260724Cl 追加
    /// 読み込みは FormSpotIDV2/FormDiffractionSimulatorGeometry と同じ ImageIO.ReadImage (16bit tiff 対応)。DetResolution (mm/px) は変更しない。</summary>
    public void ReadExperimentalImage(string fileName)
    {
        //読み込み〜新 PseudoBitmap の完全な準備までを try 内で行い、成功が確定してから旧画像と交換する (途中失敗では旧画像を保持)
        PseudoBitmap loaded = null;
        double min, max;
        int width, height;
        try
        {
            // ImageIO は Ring (グローバル) へ展開するため、成功直後に UI スレッド上でローカルへスナップショットする。
            // 第 2 引数 false: tiff の正規化確認ダイアログを抑止し生強度のまま読む
            if (!ImageIO.ReadImage(fileName, false) || Ring.Intensity == null || Ring.SrcImgSize.IsEmpty)
            {
                MessageBox.Show(this, $"Failed to read the image:\r\n{fileName}", "Experimental image", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            width = Ring.SrcImgSize.Width; height = Ring.SrcImgSize.Height;
            if (width <= 0 || height <= 0 || Ring.Intensity.LongLength != (long)width * height)
            {
                MessageBox.Show(this, $"Inconsistent image data:\r\n{fileName}", "Experimental image", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (width > numericBoxDetWidth.Maximum || height > numericBoxDetHeight.Maximum)
            {
                MessageBox.Show(this, $"The image ({width} x {height}) exceeds the maximum detector size ({numericBoxDetWidth.Maximum:g0} px).", "Experimental image", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var values = (double[])Ring.Intensity.Clone();

            //有限画素だけからレンジを求め、非有限画素 (NaN/Inf) は min へ置換する
            min = double.MaxValue; max = double.MinValue;
            foreach (var v in values)
                if (double.IsFinite(v)) { if (v < min) min = v; if (v > max) max = v; }
            if (min > max) //全画素が非有限
            {
                MessageBox.Show(this, $"The image contains no finite pixel values:\r\n{fileName}", "Experimental image", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (max <= min) max = min + 1; //単色画像で PseudoBitmap の表示レンジ分母が 0 になるのを防ぐ
            for (int i = 0; i < values.Length; i++)
                if (!double.IsFinite(values[i])) values[i] = min;

            loaded = new PseudoBitmap(values, width);
            loaded.SetScaleGray();
            loaded.MinValue = min;
            loaded.MaxValue = max;
            if (loaded.GetImage() == null)
                throw new InvalidOperationException("Failed to generate the display bitmap.");
        }
        catch (Exception ex)
        {
            loaded?.Dispose();
            MessageBox.Show(this, $"Failed to read the image:\r\n{fileName}\r\n\r\n{ex.Message}", "Experimental image", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        SetExpPseudoBitmap(loaded);

        //輝度 (Max intensity) トラックバーの対数変換係数を実レンジから設定 (FormDiffractionSimulatorGeometry と同形)
        expTrackbarConstantA = min - 1;
        expTrackbarConstantB = trackBarExpImageMaxInt.Maximum / Math.Log(max - expTrackbarConstantA);
        skipViewEvent = true;
        try
        {
            trackBarExpImageMaxInt.Value = trackBarExpImageMaxInt.Maximum; //標準表示 (=データ最大値) へリセット
            trackBarExpImageMinInt.Value = trackBarExpImageMinInt.Minimum; //260724Cl: Min はデータ最小値 (v=0 → A+e⁰ = min)
        }
        finally { skipViewEvent = false; }
        expImage = expPbmp.GetImage();

        checkBoxShowExperimentalImage.Enabled = trackBarExpImageOpacity.Enabled = trackBarExpImageMaxInt.Enabled = trackBarExpImageMinInt.Enabled = true;
        tabPageExperimentalImage.Enabled = true; // 260724Cl 追加: 実測画像が読み込まれたら Experimental image タブを解禁
        tabControlPatternSettings.SelectedTab = tabPageExperimentalImage; // 260725Ch: D&Dした画像の設定をすぐ操作できるよう前面へ

        //検出器ピクセル数を画像サイズへ反映 (DetResolution は不変)。ValueChanged の二重再計算は skip フラグで束ねる
        skipDetectorGeometryEvent = true;
        try
        {
            numericBoxDetWidth.Value = width;
            numericBoxDetHeight.Value = height;
        }
        finally { skipDetectorGeometryEvent = false; }
        RebinMcDistribution();
        DrawGeometry();

        //検出器全体 (=画像全体) が graphicsBox に収まるように表示をフィットし、パターンも再計算する
        var fitResolution = Math.Max(DetHalfWidth * 2 / Math.Max(1, graphicsBox.ClientSize.Width), DetHalfHeight * 2 / Math.Max(1, graphicsBox.ClientSize.Height)) * 1.05;
        SetView(DetectorCenterView, fitResolution);
    }

    /// <summary>実測画像の表示 ON/OFF。260724Cl 追加</summary>
    private void checkBoxShowExperimentalImage_CheckedChanged(object sender, EventArgs e) => DrawOverlays();

    /// <summary>実測画像の透明度。表示合成 (ColorMatrix) のみ変わるため再配置だけ行う。260724Cl 追加</summary>
    private void trackBarExpImageOpacity_ValueChanged(object sender, EventArgs e) => DrawOverlays();

    /// <summary>実測画像の輝度 (表示下限/上限強度)。対数スケールで Min/MaxValue を変え、表示用 Bitmap を作り直す。260724Cl 追加
    /// (旧名 trackBarExpImageMaxInt_ValueChanged。Min トラックバー追加に伴い両対応の共通ハンドラへ改名。FormDiffractionSimulatorGeometry.trackBarMaxInt_ValueChanged と同形)</summary>
    private void trackBarExpImageIntensity_ValueChanged(object sender, EventArgs e)
    {
        if (skipViewEvent || expPbmp == null) return;
        expPbmp.MaxValue = expTrackbarConstantA + Math.Exp(trackBarExpImageMaxInt.Value / expTrackbarConstantB);
        expPbmp.MinValue = expTrackbarConstantA + Math.Exp(trackBarExpImageMinInt.Value / expTrackbarConstantB);
        expImage = expPbmp.GetImage();
        DrawOverlays();
    }

    #endregion 実測 EBSD 画像

    #region 菊池線を初期化。最後にDraw()も呼び出す。
    /// <summary>菊池線を初期化。最後にDraw()も呼び出す。</summary>
    /// <param name="renewCrystal"></param>
    public void SetVector(bool renewCrystal = false)
    {
        if (FormMain == null) return;
        var sw = new Stopwatch();
        sw.Start();

        var crystal = FormMain.Crystal;
        crystal.SetVectorOfG(0, 2 / WaveLength, waveLengthControl.WaveSource);

        //260717Cl 変更: 3 本の Where 走査 (ExtinctionRule の null / lattice 一致 / 不一致で結局 null 以外は全て false) を単一 foreach に。未使用の width/height も削除
        foreach (var gtemp in crystal.VectorOfG)
            gtemp.Flag1 = gtemp.ExtinctionRule is null;

        if (radioButtonKikuchiThresholdOfLength.Checked)
        {
            Crystal.VectorOfG_KikuchiLine =
            [.. Crystal.VectorOfG.Where(g => g.Length < numericBoxKikuchiThresholdOfLength.Value).OrderByDescending(g => g.Length)];
        }
        else
        {
            var list = Crystal.VectorOfG.OrderByDescending(g => g.RelativeIntensity).ToList();
            var max = Math.Min(numericBoxKikuchiThresholdOfStructureFactor.ValueInteger, Crystal.VectorOfG.Length);
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
    //260726Cl 移設: RunBackscatterMonteCarlo (大量電子の並列 MC 実行と脱出電子の収集) は GUI 非依存の計算なので
    //Crystallography/EBSD/EbsdBackscatterSimulator.cs へ移した。呼び出しは EbsdBackscatterSimulator.Run。
    /// <summary>モンテカルロによる飛程シミュレーション</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void buttonBSE_Click(object sender, EventArgs e)
    {
        if (masterPatternEbsd.IsBuilding)
        {
            // toolStripStatusLabelProgress.Text = "MasterPattern is running. Wait for it to finish or press Stop."; // 260406Cl Label1→Label2: 進捗専用に整理
            toolStripStatusLabelSummary.Text = "MasterPattern is running. Wait for it to finish or press Stop.";
            return;
        }

        buttonSimulateBSE.Enabled = false; // (260401Ch) Calc BSE の多重起動を防ぐ
        buttonCreateMasterPattern.Enabled = false; // (260401Ch) Calc BSE 実行中に MasterPattern 前段 MC を重ねて走らせない
        // buttonStop.Visible = false; // (260401Ch) Calc BSE でも MC 中はまだ停止できない // 260406Cl 旧: MC 中も Stop を表示するよう変更
        monteCarloCts = new System.Threading.CancellationTokenSource(); // 260406Cl 追加
        buttonStop.Visible = true; // 260406Cl MC 中も Stop ボタンを表示
        toolStripProgressBar.Value = 0;
        toolStripStatusLabelSummary.Text = "Calc BSE: MonteCarlo";
        StatusBarHelper.SetProgress(toolStripProgressBar, toolStripStatusLabelProgress, 0, "", TimeSpan.Zero, showRemaining: true);// 260520Cl SetProgress化 (canonical進捗行)
        toolStripStatusLabelDetail.Text = "";
        // labelMasterPatternInfo.Text = "Calculating BSE by Monte Carlo..."; // 260406Cl 廃止: Label2 "Calc BSE: MonteCarlo" で代替

        try
        {
            await RunMonteCarloAndSetRangesAsync(
                statusPrefix: "Calc BSE",
                progressInfoText: "Calculating BSE by Monte Carlo...",
                completedInfoText: "Monte Carlo finished for Calc BSE.",
                noResultInfoText: "Calc BSE was aborted because Monte Carlo returned no usable BSEs.",
                cancellationToken: monteCarloCts.Token); // (260401Ch) MasterPattern 前段と同じ MC 前処理へ統一 // 260406Cl cancellationToken 追加
        }
        finally
        {
            DisposeMonteCarloCts(); // 260406Cl
            buttonStop.Visible = false; // 260406Cl MC 完了後は Stop を隠す
            buttonSimulateBSE.Enabled = true;
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
            // 260723Cl 変更: DetR*(x,y,0) → (halfW·x, halfH·y, 0) + 中心 X オフセット (DetX)。DrawGeometry の f1 と同じ矩形検出器写像
            var f = new Func<double, double, PointD>((x, y)
                => Stereonet.ConvertVectorToSchmidt(smpRot * (detRot * new V3(DetHalfWidth * x, DetHalfHeight * y, 0) + new V3(DetX, -DetY, -DetZ))));
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
                // area = [.. ValueEnumerable.Range(0, areaStep).Select(n => 2.0 * Math.PI * n / areaStep).Select(Θ => f(Math.Sin(Θ), Math.Cos(Θ)))]; // 260723Cl 変更前: 円周
                area = [.. ValueEnumerable.Range(0, areaStep).Select(n => 4.0 * n / areaStep).Select(t => { var (x, y) = RectPerimeter(t); return f(x, y); })]; // 260723Cl 変更: 矩形周

            //ある立体角に収まるbseだけを抽出
            var bse2 = BSEs.AsParallel().Where(e =>
            Geometry.InsidePolygonalArea(area, e.Position)).ToArray();
            #endregion

            var count = bse2.Length;
            // 260723Cl 追加: 矩形化・DetX 移動で検出器 (セル) 内の BSE が 0 件になり得る (旧: 0 除算と depths.Max() で例外)
            if (count == 0)
            {
                graphControlEnergyProfile.ClearProfile();
                graphControlDepthProfile.ClearProfile();
                return;
            }
            //エネルギー分布を描画 ここから
            //if(false)
            {
                double step = 0.25, lower = 0, upper = energy - EnergyThreshold;//kev単位
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
                var depths = bse2.Select(e => e.HasLastInelasticEvent ? e.LastInelasticDepth : e.Depth); // (260401Ch) MasterPattern 重み付けと同じく、可能なら最後の非弾性散乱深さを深さ分布に使う
                double step = 1, lower = 0, upper = depths.Max();//nm単位
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

    private bool skipProgressChangedEvent = false;


    #region 画像出力パラメータのイベント

    private void TrackBarOutputThickness_Scroll(object sender, EventArgs e)
    {
        numericBoxDepth.Value = ThicknessArray[trackBarOutputThickness.Value];
        if (mcDistribution == null && MasterPattern != null) InvalidateIndexingResults(); //260725Ch: 単一スライス fallback が存在するときだけ失効
        Draw();
    }
    private void trackBarOutputEnergy_ValueChanged(object sender, EventArgs e)
    {
        numericBoxEnergy.Value = EnergyArray[trackBarOutputEnergy.Value];
        if (mcDistribution == null && MasterPattern != null) InvalidateIndexingResults(); //260725Ch: Radon-only時に無関係な候補を消さない
        Draw();
    }
    private void trackBarIntensityBrightnessMax_ValueChanged(object sender, EventArgs e) => Draw();

    #endregion

    // 260520Cl 改名: buttonSaveImage → buttonCopyImage (実体はクリップボードへコピー。Text="Copy"・兄弟の buttonCopyEnergyProfile と命名統一)
    // 260725Cl 変更: コピー範囲 (Current view / Detector) と Match detector resolution オプションに対応
    private void buttonCopyImage_Click(object sender, EventArgs e)
    {
        //if (Pbmp != null)
        //    Clipboard.SetDataObject(Pbmp.GetImage()); // 260725Cl 変更前: パターン計算ラスターをそのままコピー (範囲・解像度の指定不可)

        // コピー解像度 (mm/px): Match detector resolution 時は検出器ピクセルと 1:1
        var resolution = checkBoxMatchDetectorResolution.Checked ? DetPixelSize : Resolution;
        PointD center;
        int w, h;
        if (radioButtonDetector.Checked)
        {
            //検出器エリアのみ (Match 時は DetPixelWidth × DetPixelHeight に一致)
            center = DetectorCenterView;
            w = (int)Math.Round(DetHalfWidth * 2 / resolution);
            h = (int)Math.Round(DetHalfHeight * 2 / resolution);
        }
        else //radioButtonCopyCurrent: graphicsBox が表示している範囲
        {
            center = ViewCenter;
            w = (int)Math.Round(graphicsBox.ClientSize.Width * Resolution / resolution);
            h = (int)Math.Round(graphicsBox.ClientSize.Height * Resolution / resolution);
        }
        if (w <= 0 || h <= 0) return;

        //極端なズームアウト + Match 時の巨大ビットマップ保護: 最大辺 4096 にクランプ (mm 範囲は維持し解像度を粗くする)
        //260725Cl 注記: パターン本体のラスターは別に MaxPatternRasterSize (2048) でクランプされる (PatternRasterSize)。
        //検出器が 2048 px を超える場合、"Match detector resolution" でもパターン画像は 2048 から拡大されたものになる (オーバーレイのみ実解像度)
        const int maxCopyPixel = 4096;
        if (Math.Max(w, h) > maxCopyPixel)
        {
            var scale = (double)maxCopyPixel / Math.Max(w, h);
            resolution /= scale;
            w = Math.Max(1, (int)(w * scale));
            h = Math.Max(1, (int)(h * scale));
        }

        // 260725Cl 変更: コピー形式ラジオ (radioButtonCopyEmf / radioButtonCopyBmp) に対応
        if (radioButtonCopyEmf.Checked)
        {
            //拡張メタファイル: 菊池線・指数ラベル等のオーバーレイをベクトルのまま保持 (パターン画像のみラスター埋め込み)
            ClipboardMetafileHelper.SaveOrCopyDrawingAsEnhMetafile(Handle, g =>
            {
                g.SetClip(new Rectangle(0, 0, w, h)); //メタファイルは画面やビットマップと違い自然な境界クリップが無いため明示 (Transform 設定前=デバイス座標)
                // RenderViewTo(g, new Size(w, h), center, resolution); // 260725Cl 変更前
                RenderViewTo(g, new Size(w, h), center, resolution, suppressDetectorOutline: radioButtonDetector.Checked); //260725Cl 変更: Detector 範囲は外枠を含めない
            });
        }
        else //radioButtonCopyBmp: ビットマップ形式
        {
            using var bmp = new Bitmap(w, h);
            using (var g = Graphics.FromImage(bmp))
                // RenderViewTo(g, new Size(w, h), center, resolution); // 260725Cl 変更前
                RenderViewTo(g, new Size(w, h), center, resolution, suppressDetectorOutline: radioButtonDetector.Checked); //260725Cl 変更: Detector 範囲は外枠を含めない
            Clipboard.SetDataObject(bmp, true); //copy=true: bmp は直後に Dispose するため実体コピーで渡す
        }
    }

    // 260725Cl 追加: MasterPattern 2D / 3D プレビューのクリップボードコピー。
    // Designer にボタン (buttonMasterPattern2DCopy / 3DCopy) とツールチップ文案は元からあったが Click ハンドラが未配線で、
    // 画面に見えているのに押しても無反応だった (Copy ボタンをコピーして作った際の配線漏れ)。
    private void buttonMasterPattern2DCopy_Click(object sender, EventArgs e)
    {
        if (masterPattern2DBitmap == null) return;
        //GetImage() は PseudoBitmap 内部キャッシュの借用参照なので Dispose せず、copy=true で実体をクリップボードへ渡す
        Clipboard.SetDataObject(masterPattern2DBitmap.GetImage(), true);
        toolStripStatusLabelSummary.Text = "MasterPattern 2D copied to the clipboard";
    }

    private void buttonMasterPattern3DCopy_Click(object sender, EventArgs e)
    {
        try
        {
            //GL の back buffer へ描き直してから ReadPixels する (OpenGL 無効時・サイズ 0 では null が返る)
            using var bmp = glControlMasterPattern3D?.GenerateBitmap(renderBeforeRead: true);
            if (bmp == null) { toolStripStatusLabelSummary.Text = "MasterPattern 3D copy failed (OpenGL unavailable)"; return; }
            Clipboard.SetDataObject(bmp, true); //copy=true: bmp は直後に Dispose するため実体コピーで渡す
            toolStripStatusLabelSummary.Text = "MasterPattern 3D copied to the clipboard";
        }
        catch (Exception ex) //GL バックエンド (ARM の GLOn12 等) での ReadPixels 失敗でアプリを落とさない
        {
            toolStripStatusLabelSummary.Text = "MasterPattern 3D copy failed";
            toolStripStatusLabelDetail.Text = ex.Message;
        }
    }

    /// <summary>指定した中心 (表示パターン座標 mm)・解像度 (mm/px)・キャンバス (px) でパターン+オーバーレイを g へオフスクリーン描画する。260725Cl 追加
    /// 画面用と同じ描画パイプライン (DrawEBSD/DrawOverlays) を CanvasSize/Resolution/viewPan の一時上書きで流用し、終了後に画面用の状態を復元する。
    /// Bitmap だけでなく Metafile の Graphics へも描けるよう、描画先生成は呼び出し側が担う (旧 RenderViewToBitmap を一般化)。
    /// 260725Cl 追記: 要求ビューが画面と完全一致するとき (既定の Current view + Match 解像度なし) は高速パスへ分岐し、
    /// 上書き・パターン再計算・復元をいずれも行わず、画面の patternBitmap を再利用して DrawOverlays だけで描く。</summary>
    //260725Cl シグネチャ変更 (suppressDetectorOutline 追加)。旧: private void RenderViewTo(Graphics g, Size canvas, PointD centerView, double resolution)
    private void RenderViewTo(Graphics g, Size canvas, PointD centerView, double resolution, bool suppressDetectorOutline = false)
    {
        //260725Cl 追加 (/simplify): 要求ビューが画面と完全一致するとき (既定の Current view + Match 解像度なし) は
        //上書きも再計算も不要 — 画面の patternBitmap をそのまま使って g へ描くだけで同じ絵になる。
        //これで最頻ケースの重いパターン再計算 2 回 (往路+復路) がゼロになる
        //(canvas/resolution/center は Current view かつ Match 未チェックのとき画面の値がそのまま渡るので、比較は厳密一致で足りる)
        if (canvas == graphicsBox.ClientSize && resolution == Resolution
            && centerView.X == ViewCenter.X && centerView.Y == ViewCenter.Y
            && (patternBitmap != null || !checkBoxShowDyanmicalEBSD.Checked)) //パターンを描かない設定なら未計算でもよい
        {
            DrawOverlays(g, -1, -1, suppressDetectorOutline);
            return;
        }

        var originalPan = viewPan;
        renderCanvasOverride = canvas;
        renderResolutionOverride = resolution;
        viewPan = new PointD(centerView.X - DetectorCenterView.X, centerView.Y - DetectorCenterView.Y);
        try
        {
            DrawEBSD(); //対象視野・解像度でパターンを再計算 (Pbmp/patternBitmap を一時的に上書き)
            DrawOverlays(g, -1, -1, suppressDetectorOutline); //260725Cl: 外枠抑止はフィールドでなく引数で渡す
        }
        finally
        {
            renderCanvasOverride = null;
            renderResolutionOverride = null;
            viewPan = originalPan;
            DrawEBSD(); //画面用のパターン (Pbmp/patternBitmap/patternBitmapRect) を復元
            DrawOverlays();
        }
    }


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
    // private async Task<bool> RunMonteCarloAndSetRangesAsync(...) // 260406Cl 旧シグネチャ: CancellationToken なし
    private async Task<bool> RunMonteCarloAndSetRangesAsync(
        string statusPrefix = "MasterPattern",
        string progressInfoText = "Preparing MasterPattern by Monte Carlo...",
        string completedInfoText = "Monte Carlo finished. Starting MasterPattern build...",
        string noResultInfoText = "MasterPattern build was aborted because Monte Carlo returned no usable BSEs.",
        System.Threading.CancellationToken cancellationToken = default) // 260406Cl CancellationToken 追加
    {
        var cry = FormMain.Crystal;
        cry.GetFormulaAndDensity();
        // 260612Cl z(平均原子番号)/a(平均原子量)/valenceElectronCount は MonteCarlo.GetMeanAtomicParameters (Multiplicity×Occ 加重) に集約。
        // 旧: sum1/sum2/sum3 + EstimateAverageValenceElectronCount を直書きし、Multiplicity のみで Occ 抜け (部分占有・固溶体で実組成とずれた)。
        //var sum1 = cry.Atoms.Sum(a => AtomStatic.AtomicWeight(a.AtomicNumber) * a.Multiplicity * a.AtomicNumber);
        //var sum2 = cry.Atoms.Sum(a => AtomStatic.AtomicWeight(a.AtomicNumber) * a.Multiplicity);
        //var sum3 = cry.Atoms.Sum(a => a.Multiplicity);
        //double z = sum1 / sum2, a = sum2 / sum3;
        //var valenceElectronCount = MonteCarlo.EstimateAverageValenceElectronCount(
        //    cry.Atoms.Select(atom => (atom.AtomicNumber, AtomStatic.AtomicWeight(atom.AtomicNumber) * atom.Multiplicity))); // (260331Ch)
        var (z, a, valenceElectronCount) = MonteCarlo.GetMeanAtomicParameters(cry.Atoms);//260612Cl
        double rho = cry.Density;
        // double energy = Voltage, ..., detectorR = DetR, ...; // 260723Cl 変更前: 円形検出器 (半径)
        double energy = Voltage, sampleTilt = SmpTilt, detectorTilt = DetTilt, detectorX = DetX, detectorY = DetY, detectorZ = DetZ, detectorHalfW = DetHalfWidth, detectorHalfH = DetHalfHeight, energyThreshold = EnergyThreshold; // 260723Cl 変更: 矩形検出器 (半幅・半高) + 中心 X
        var loop = BackscatterMonteCarloLoopCount;
        var sampleRotation = M3.CreateRotationX(sampleTilt);
        var monteCarloStopwatch = Stopwatch.StartNew();
        IProgress<(int Progress, string Message)> progress = new Progress<(int Progress, string Message)>(state =>
        {
            var progressValue = Math.Clamp(state.Progress, 0, 100);
            toolStripProgressBar.Value = progressValue;
            toolStripStatusLabelSummary.Text = $"{statusPrefix}: {state.Message}"; // (260401Ch) Calc BSE も同じ MC helper を共有する
            StatusBarHelper.SetProgress(toolStripProgressBar, toolStripStatusLabelProgress, progressValue / 100.0, "", monteCarloStopwatch.Elapsed, showRemaining: true);// 260520Cl SetProgress化
            // labelMasterPatternInfo.Text = $"{progressInfoText} {progressValue}%"; // (260401Ch) // 260406Cl 廃止: Label1の進捗%とLabel2のタスク名で代替
        });

        // 260406Cl: OperationCanceledException を捕捉して MC キャンセル時も安全に return false する
        try
        {
            progress.Report((0, "MonteCarlo"));
            var result = await Task.Run(() =>
            {
                var monte = new MonteCarlo(z, a, rho, energy, sampleTilt, energyThreshold,
                    elasticScatteringModel: MonteCarlo.ElasticScatteringModels.MottNistSampler2023,
                    inelasticScatteringModel: MonteCarlo.InelasticScatteringModels.DiscreteBulkDiimfpApproximation,
                    valenceElectronCount: valenceElectronCount,
                    atoms: cry.Atoms); // (260331Ch)
                var bses = EbsdBackscatterSimulator.Run(monte, loop, energyThreshold, sampleRotation, (completed, total) => //260726Cl: 本体は Crystallography/EBSD へ移設
                    progress.Report(((int)Math.Round(90.0 * completed / total), "MonteCarlo")), cancellationToken); // (260327Ch) fitting 分を残して 90% まで使う // 260406Cl cancellationToken 追加
                if (bses.Length == 0)
                    return (Bses: bses, Distribution: (EbsdMonteCarloDistribution)null, Energies: [], Depths: [],
                        energyStart: 0.0, energyEnd: 0.0, energyStep: 0.0, depthStart: 0.0, depthEnd: 0.0, depthStep: 0.0);

                progress.Report((92, "Analyzing Monte Carlo ranges"));
                var bseRaw = bses.Select(e => (
                    monteCarloDistributionDepthMode == MonteCarloDistributionDepthMode.LastInelasticEventDepth && e.HasLastInelasticEvent
                        ? e.LastInelasticDepth
                        : e.Depth,
                        e.Vec, e.Energy)).ToArray(); // (260331Ch) P(z_last_inelastic, Ω_exit, E_exit) と P(z_last_event, Ω_exit, E_exit) を切替
                var (energyLoss80, depth99) = EbsdMonteCarloDistribution.ComputeRangesFromMC(bseRaw, energy); // (260327Ch)
                var grid = EbsdMonteCarloDistribution.ComputeGridFromRanges(energy, energyLoss80, depth99); // (260327Ch)

                progress.Report((95, "Fitting Monte Carlo distribution"));
                var distribution = new EbsdMonteCarloDistribution(
                    bseRaw, energy,
                    // detectorTilt, detectorY, detectorZ, detectorR, // 260723Cl 変更前: 円形検出器 (半径)
                    detectorTilt, detectorX, detectorY, detectorZ, detectorHalfW, detectorHalfH, // 260718Cl: smpTilt 引数を削除 (BSE Vec は既に lab 座標系で検出器写像に試料傾斜は不要) // 260723Cl: 矩形検出器 (半幅・半高) + 中心 X
                    grid.energies, grid.depths);
                return (Bses: bses, Distribution: distribution, Energies: grid.energies, Depths: grid.depths, grid.energyStart, grid.energyEnd, grid.energyStep, grid.depthStart, grid.depthEnd, grid.depthStep);
            }, cancellationToken); // 260406Cl cancellationToken を Task.Run にも渡す

            if (result.Bses == null || result.Bses.Length == 0)
            {
                masterPatternMonteCarloElapsedMilliseconds = monteCarloStopwatch.ElapsedMilliseconds; // (260327Ch)
                BSEs = [];
                mcDistribution = null;
                composedPatternCache = default; // 260725Cl 追加 (/simplify): MC 合成キャッシュも失効させる
                InvalidateIndexingResults(); //260725Ch: 旧 MC 合成で採点した候補を残さない
                toolStripProgressBar.Value = 0;
                toolStripStatusLabelSummary.Text = $"{statusPrefix}: MonteCarlo"; // (260401Ch)
                StatusBarHelper.SetProgress(toolStripProgressBar, toolStripStatusLabelProgress, 0, "", TimeSpan.FromMilliseconds(masterPatternMonteCarloElapsedMilliseconds), showRemaining: true);// 260520Cl SetProgress化
                // toolStripStatusLabelDetail.Text = ""; // 260406Cl 旧: 空文字→noResultInfoText に変更
                toolStripStatusLabelDetail.Text = noResultInfoText; // 260406Cl labelMasterPatternInfo廃止: 結果なし理由をLabel3へ移動
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
            composedPatternCache = default; // 260725Cl 追加 (/simplify): 旧 MC 分布・旧 MasterPattern を掴んだままにしない
            InvalidateIndexingResults(); //260725Ch: ZNCC 辞書の重みが変わるため候補と実行中結果を失効させる

            poleFigureControl.DrawingMode = PoleFigureControl2.DrawingModeEnum.Histogram;
            var poleFigureRotation = M3.CreateRotationX(sampleTilt);
            poleFigureControl.Vectors = [.. BSEs.Select(e => new V4(poleFigureRotation * e.Vec, e.Energy))];
            CalcStatistics();

            masterPatternMonteCarloElapsedMilliseconds = monteCarloStopwatch.ElapsedMilliseconds; // (260327Ch) MC 本体と fitting、統計更新まで含めた時間
            toolStripProgressBar.Value = 100;
            toolStripStatusLabelSummary.Text = $"{statusPrefix}: MonteCarlo finished"; // (260401Ch)
            StatusBarHelper.SetProgress(toolStripProgressBar, toolStripStatusLabelProgress, 1.0, "", TimeSpan.FromMilliseconds(masterPatternMonteCarloElapsedMilliseconds));// 260520Cl SetProgress化 (完了)
            // labelMasterPatternInfo.Text = completedInfoText; // (260401Ch) // 260406Cl 廃止
            toolStripStatusLabelDetail.Text = completedInfoText; // 260406Cl labelMasterPatternInfo廃止: 完了メッセージをLabel3へ移動
            return true;
        }
        catch (OperationCanceledException) // 260406Cl 追加: Stop ボタンで MC がキャンセルされた場合
        {
            masterPatternMonteCarloElapsedMilliseconds = monteCarloStopwatch.ElapsedMilliseconds;
            toolStripProgressBar.Value = 0;
            toolStripStatusLabelSummary.Text = $"{statusPrefix}: MonteCarlo cancelled";
            toolStripStatusLabelProgress.Text = $"Stopped after {StatusBarHelper.FormatElapsed(monteCarloStopwatch.Elapsed)}";
            toolStripStatusLabelDetail.Text = "";
            return false;
        }
    }

    /// <summary>
    /// 260524Cl 追加: --capture 用。Show しただけでは MasterPattern が無いため、Build MasterPattern を起動するだけ。
    /// build は非同期かつ重い (MonteCarlo + Bethe) が、完了判定は凝ったことをせず、GuiCapture 側が
    /// 「画面が変化しなくなったら完了」と見なす (5秒ごとの画面比較)。通常操作には影響させず、呼び出し元は GuiCapture に限定する。
    /// </summary>
    internal void PrepareCaptureForGuiAudit()
    {
        if (FormMain?.Crystal == null || masterPatternEbsd.IsBuilding)
            return;
        buttonCreateMasterPattern_Click(buttonCreateMasterPattern, EventArgs.Empty); // Build MasterPattern ボタン相当を起動 (async)。完了判定は GuiCapture の画面安定待ちに委ねる。
    }

    /// <summary>
    /// UI 上の設定値を読み取り、MasterPattern の作成を開始する。
    /// 実際の計算本体は Crystallography.EBSD に委譲し、このメソッドでは UI の状態遷移だけを扱う。
    /// 260527Cl: 直上に PrepareCaptureForGuiAudit を挿入した際に当メソッドの doc が剥がれていたので戻した。
    /// </summary>
    private async void buttonCreateMasterPattern_Click(object sender, EventArgs e)
    {
        #region お蔵入り // (260327Ch) 旧 bwEBSD 実行中チェックは ebsdNew 本命化に伴い退避
        //if (Crystal?.Bethe?.bwEBSD?.IsBusy == true)
        //{
        //    toolStripStatusLabelProgress.Text = "The regular EBSD solver is running. Wait for it to finish first.";
        //    return;
        //}
        #endregion
        if (masterPatternEbsd.IsBuilding)
            return;

        buttonCreateMasterPattern.Enabled = false; // (260327Ch) MC 前処理中の多重起動を防ぐ
        // buttonStop.Visible = false; // (260327Ch) MC 前処理はまだ停止できないため、Bethe 開始まで出さない // 260406Cl 旧: MC 中も Stop を表示するよう変更
        monteCarloCts = new System.Threading.CancellationTokenSource(); // 260406Cl 追加
        buttonStop.Visible = true; // 260406Cl MC 中も Stop ボタンを表示
        toolStripProgressBar.Value = 0;
        toolStripStatusLabelSummary.Text = "MasterPattern: MonteCarlo";
        StatusBarHelper.SetProgress(toolStripProgressBar, toolStripStatusLabelProgress, 0, "", TimeSpan.Zero, showRemaining: true);// 260520Cl SetProgress化 (canonical進捗行)
        toolStripStatusLabelDetail.Text = ""; // (260327Ch) 前回の完了時間表示をクリア
        // labelMasterPatternInfo.Text = "Preparing MasterPattern by Monte Carlo..."; // 260406Cl 廃止: Label2 "MasterPattern: MonteCarlo" で代替
        sw2.Restart(); // (260327Ch) MasterPattern 全体の経過時間
        masterPatternMonteCarloElapsedMilliseconds = 0;
        EBSD.MasterPatternBuildRequest request = null;

        try
        {
            // 260325Cl: まず MC を実行してレンジとフィッティングを決定
            if (!await RunMonteCarloAndSetRangesAsync(cancellationToken: monteCarloCts.Token)) // 260406Cl cancellationToken 追加
            {
                DisposeMonteCarloCts(); // 260406Cl
                buttonStop.Visible = false; // 260406Cl MC 失敗/キャンセル時は Stop を隠す
                buttonCreateMasterPattern.Enabled = true; // (260327Ch)
                return;
            }
            DisposeMonteCarloCts(); // 260406Cl MC 完了後は CTS を破棄 (Bethe は masterPatternEbsd が管理)

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
            DisposeMonteCarloCts(); // 260406Cl
            DetachMasterPatternBuildEvents();
            buttonCreateMasterPattern.Enabled = true; // (260327Ch)
            buttonStop.Visible = false; // (260327Ch)
            throw;
        }

        trackBarMasterPatternEnergy.Enabled = trackBarMasterPatternDepth.Enabled = false;
        // buttonStop.Visible = true; // 260406Cl 旧: MC 開始時から表示済みなのでここでは不要 (Bethe 開始時点で既に表示中)
        toolStripProgressBar.Value = 0;
        toolStripStatusLabelSummary.Text = "MasterPattern: Starting Bethe calculation";
        // labelMasterPatternInfo.Text = $"Building {GetHemisphereText(request.Hemisphere)} master grid ({request.GridSize} x {request.GridSize})..."; // (260321Ch) 旧案: 単一半球計算を前提にしていた
        // labelMasterPatternInfo.Text = $"Building full sphere master grid ({request.GridSize} x {request.GridSize})..."; // 260406Cl 廃止: Label3へ移動
        toolStripStatusLabelDetail.Text = $"Full sphere, grid {request.GridSize} x {request.GridSize}"; // 260406Cl labelMasterPatternInfo廃止: グリッド情報をLabel3へ
        StatusBarHelper.SetProgress(toolStripProgressBar, toolStripStatusLabelProgress, 0, "", TimeSpan.Zero, showRemaining: true);// 260520Cl SetProgress化 (canonical進捗行) // (260327Ch)
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
            toolStripStatusLabelSummary.Text = $"MasterPattern: {e.UserState}";
            StatusBarHelper.SetProgress(toolStripProgressBar, toolStripStatusLabelProgress, progress / 100.0, "", TimeSpan.FromSeconds(sec), showRemaining: true);// 260520Cl SetProgress化
            // labelMasterPatternInfo.Text = $"Building {GetHemisphereText(e.Request.Hemisphere)} master grid... {progress}%"; // (260321Ch) 旧案: 単一半球計算を前提にしていた
            // labelMasterPatternInfo.Text = $"Building full sphere master grid... {progress}%"; // 260406Cl 廃止: Label1の進捗%とLabel2のタスク名で代替
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
            toolStripStatusLabelSummary.Text = "MasterPattern failed";
            toolStripStatusLabelProgress.Text = $"Failed after {StatusBarHelper.FormatElapsed(sw2.Elapsed)}";
            toolStripStatusLabelDetail.Text = "";
            // labelMasterPatternInfo.Text = "MasterPattern build failed."; // 260406Cl 廃止: Label2 "MasterPattern failed" で代替
            UpdateMasterPatternSelectors();
            DrawMasterPattern2D();
            return;
        }

        if (e.Cancelled || e.MasterPattern == null)
        {
            toolStripStatusLabelSummary.Text = "MasterPattern cancelled";
            toolStripStatusLabelProgress.Text = $"Stopped after {StatusBarHelper.FormatElapsed(sw2.Elapsed)}";
            toolStripStatusLabelDetail.Text = "";
            // labelMasterPatternInfo.Text = "MasterPattern build was cancelled."; // 260406Cl 廃止: Label2 "MasterPattern cancelled" で代替
            UpdateMasterPatternSelectors();
            DrawMasterPattern2D();
            return;
        }

        composedPatternCache = default; //260725Ch: 完成前の MasterPattern を保持するキャッシュを明示的に破棄
        InvalidateIndexingResults(); //260725Ch: 新しい MasterPattern に対して旧候補・実行中探索を適用させない
        UpdateMasterPatternSelectors();
        trackBarMasterPatternDepth.Value = MasterPattern.Depths.Length / 2; // 260725Ch: build直後は低コントラストな最小depthではなく中央付近を初期表示
        DrawMasterPattern2D();
        Draw(); // (260327Ch) 描画更新で他ラベルが書き換わる前に済ませ、最後に MasterPattern 用の status を上書きする
        toolStripProgressBar.Value = 100;
        toolStripStatusLabelSummary.Text = "MasterPattern completed";
        var totalSec = sw2.ElapsedMilliseconds / 1000.0;
        var monteCarloSec = masterPatternMonteCarloElapsedMilliseconds / 1000.0;
        StatusBarHelper.SetProgress(toolStripProgressBar, toolStripStatusLabelProgress, 1.0, "", TimeSpan.FromSeconds(totalSec));// 260520Cl SetProgress化 (完了)
        // toolStripStatusLabelDetail.Text = $"Total {totalSec:f2} s (Monte Carlo {monteCarloSec:f2} s, MasterPattern {sec:f2} s, {e.Request.GridSize} x {e.Request.GridSize}, full sphere)"; // 260406Cl 旧: energies/depths 情報を統合
        toolStripStatusLabelDetail.Text = $"Total {totalSec:f2} s (MC {monteCarloSec:f2} s, Bethe {sec:f2} s), {e.Request.GridSize} x {e.Request.GridSize}, full sphere, {MasterPattern?.Energies.Length ?? 0} energies, {MasterPattern?.Depths.Length ?? 0} depths"; // 260406Cl labelMasterPatternInfo廃止: energies/depths をLabel3へ統合
        // labelMasterPatternInfo.Text = $"Ready: {GetHemisphereText(e.Request.Hemisphere)}, {MasterPattern?.Energies.Length ?? 0} energies, {MasterPattern?.Depths.Length ?? 0} depths."; // (260321Ch) 旧案
        // labelMasterPatternInfo.Text = $"Ready: full sphere, {MasterPattern?.Energies.Length ?? 0} energies, {MasterPattern?.Depths.Length ?? 0} depths."; // 260406Cl 廃止: Label3へ統合

        // 260325Cl 追加: MasterPattern 完了時に groupBoxOutput を有効化し、trackbar を同期する
        tabPageOutputParameter.Enabled = true;
        tabControlPatternSettings.SelectedTab = tabPageOutputParameter; // 260725Ch: build直後は出力設定を前面へ
        checkBoxShowDyanmicalEBSD.Enabled = true; // 260724Cl 追加: dynamical EBSD の表示切替も MasterPattern 構築後に解禁
        trackBarOutputEnergy.Maximum = Math.Max(0, MasterPattern.Energies.Length - 1);
        trackBarOutputThickness.Maximum = Math.Max(0, MasterPattern.Depths.Length - 1);
        trackBarOutputEnergy.Value = trackBarOutputThickness.Value = 0;
        numericBoxEnergy.Value = MasterPattern.Energies.Length > 0 ? MasterPattern.Energies[0] : 0;
        numericBoxDepth.Value = MasterPattern.Depths.Length > 0 ? MasterPattern.Depths[0] : 0;

    }

    /// <summary>進行中の MonteCarlo / MasterPattern build を停止する。</summary>
    private void buttonStop_Click(object sender, EventArgs e)
    {
        // 260406Cl 追加: MC 実行中のキャンセル
        // MC と Bethe は排他的に実行される (buttonSimulateBSE/buttonCreateMasterPattern が互いの Enabled を制御するため同時には走らない)
        if (monteCarloCts != null && !monteCarloCts.IsCancellationRequested)
        {
            monteCarloCts.Cancel();
            toolStripStatusLabelSummary.Text = "MonteCarlo cancel requested";
            return;
        }

        if (masterPatternEbsd.IsBuilding)
        {
            masterPatternEbsd.CancelMasterPatternBuild();
            toolStripStatusLabelSummary.Text = "MasterPattern cancel requested";
            StatusBarHelper.SetProgress(toolStripProgressBar, toolStripStatusLabelProgress, toolStripProgressBar.Value / 100.0, "", sw2.Elapsed, showRemaining: true);// 260520Cl SetProgress化
            return;
        }

        #region お蔵入り // (260327Ch) 旧 bwEBSD の停止 UI は ebsdNew 本命化に伴い退避
        //if (Crystal?.Bethe?.bwEBSD?.IsBusy == true)
        //{
        //    Crystal.Bethe.CancelEBSD();
        //    toolStripStatusLabelSummary.Text = "EBSD cancel requested";
        //    toolStripStatusLabelProgress.Text = "Stopping EBSD...";
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
            trackBarMasterPatternEnergy.Enabled = trackBarMasterPatternDepth.Enabled = false;
            numericBoxMasterPatternEnergy.Value = numericBoxMasterPatternDepth.Value = 0;
            return;
        }

        trackBarMasterPatternEnergy.Minimum = trackBarMasterPatternDepth.Minimum = 0;
        trackBarMasterPatternEnergy.Maximum = Math.Max(0, MasterPattern.Energies.Length - 1);
        trackBarMasterPatternDepth.Maximum = Math.Max(0, MasterPattern.Depths.Length - 1);
        trackBarMasterPatternEnergy.Enabled = MasterPattern.Energies.Length > 0;
        trackBarMasterPatternDepth.Enabled = MasterPattern.Depths.Length > 0;
        trackBarMasterPatternEnergy.Value = trackBarMasterPatternDepth.Value = 0;
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
            numericBoxMasterPatternEnergy.Value = numericBoxMasterPatternDepth.Value = 0;
            return;
        }

        numericBoxMasterPatternEnergy.Value = trackBarMasterPatternEnergy.Enabled && trackBarMasterPatternEnergy.Value < MasterPattern.Energies.Length
            ? MasterPattern.Energies[trackBarMasterPatternEnergy.Value] : 0;
        numericBoxMasterPatternDepth.Value = trackBarMasterPatternDepth.Enabled && trackBarMasterPatternDepth.Value < MasterPattern.Depths.Length
            ? MasterPattern.Depths[trackBarMasterPatternDepth.Value] : 0; // (260321Ch)
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
            // labelMasterPatternInfo.Text = "MasterPattern preview is empty."; // 260406Cl 廃止: Label2へ移動
            toolStripStatusLabelSummary.Text = "MasterPattern preview is empty.";
            ResetMasterPattern3DCache(); // (260321Ch) build 前は 3D 再描画用のキャッシュも空にしておく
            // SetMasterPattern2DBitmap(CreateMasterPatternPlaceholderValues(GetSelectedMasterPatternGridSize()), GetSelectedMasterPatternGridSize()); // (260322Ch) 旧実装: helper が新規配列を返していた
            SetMasterPattern2DBitmap(new double[selectedGridSize * selectedGridSize], selectedGridSize); // (260322Ch) helper を介さず空の placeholder 配列をその場で生成する
            ClearMesh(); // (260321Ch) MasterPattern 未作成時の 3D preview は空にする
            return;
        }

        var selectedHemisphere = GetSelectedMasterPattern2DHemisphere();
        int selectedEnergyIndex = trackBarMasterPatternEnergy.Enabled ? trackBarMasterPatternEnergy.Value : -1,
            selectedDepthIndex = trackBarMasterPatternDepth.Enabled ? trackBarMasterPatternDepth.Value : -1;
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
        // labelMasterPatternInfo.Text = $"Preview: {GetHemisphereText(selectedHemisphere)}, E = {energy:g} kV, depth = {depth:g} nm"; // 260406Cl 廃止: Label2+Label3へ分割
        toolStripStatusLabelSummary.Text = "MasterPattern preview";
        toolStripStatusLabelDetail.Text = $"{GetHemisphereText(selectedHemisphere)}, E = {energy:g} keV, depth = {depth:g} nm"; // 260520Cl: kV→keV (エネルギー単位)
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
        masterPattern2DValues = masterPattern3DValuesPositive = masterPattern3DValuesNegative = []; // (260322Ch)
        masterPattern3DCacheGridSize = 0; // (260322Ch)
    }

    /// <summary>
    /// キャッシュ済みの preview 値と現在の PseudoBitmap 設定を使って 3D を描き直す。
    /// 輝度レンジやカラースケール変更時に 2D 側の設定をそのまま反映するために使う。
    /// </summary>
    private void RedrawMasterPattern3DFromCache() // (260322Ch) 旧名: RedrawMasterPattern3DPreviewFromCache
    {
        if (masterPattern2DBitmap == null || masterPattern3DCacheGridSize <= 0
            || masterPattern2DValues == null || masterPattern2DValues.Length != masterPattern3DCacheGridSize * masterPattern3DCacheGridSize)
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

    private List<GLObject> CreateMasterPattern3DAxisLabelObjects()
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
    private static ColoredSurfaceMesh CreateMesh_Square(double[] values, int gridSize, MasterPattern.Hemisphere hemisphere, PseudoBitmap referenceBitmap)
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
    private static ColoredSurfaceMesh CreateMesh_Hex(double[] values, int gridSize, MasterPattern.Hemisphere hemisphere, PseudoBitmap referenceBitmap)
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
            [.. positionList], [.. argbList], [.. indexList],
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
        // DrawEBSD(); // (260327Ch) BSE 分布つき合成と単一スライス表示を即座に切り替える
        DrawEBSD(); DrawOverlays(); // 260723Cl 変更: DrawEBSD は patternBitmap 更新のみになったため、画面反映に DrawOverlays が必要
    }

    /// <summary>260718Cl 追加: 検出器を背面から見た左右反転の X 符号。未チェック(既定)=+1=現状(試料側から見た図)、チェック=-1=左右反転。
    /// パターン(EbsdPatternComposer.BuildLookupTable の ax)・輝度(detNormX)・オーバーレイ(ゾーン軸 detX・菊池線 pt.X)の全 X 投影へ一貫して掛ける。</summary>
    //260724Cl 定義反転 (作者指示): 未チェック (既定) = 検出器から試料を見る視線ベクトル (自然なカメラ画像) = 旧チェック状態 (xm=−1) と等価。
    //チェック時のみ左右反転 (試料側から見た向き、xm=+1)。⚠レジストリに旧定義のチェック状態が保存されている場合は意味が逆になるので一度チェックを外すこと。
    //旧: private double DetectorXMirror => checkBoxFlipDetectorLeftRight.Checked ? -1.0 : 1.0;
    private double DetectorXMirror => checkBoxFlipDetectorLeftRight.Checked ? 1.0 : -1.0;

    // private void checkBoxFlipDetectorLeftRight_CheckedChanged(object sender, EventArgs e) => DrawEBSD(); // 260718Cl 追加: パターン再描画→Paint 経由でオーバーレイも反転反映
    //private void checkBoxFlipDetectorLeftRight_CheckedChanged(object sender, EventArgs e) { DrawEBSD(); DrawOverlays(); } // 260723Cl 変更: 画面配置が DrawOverlays へ移ったため、ラスター再計算後に表示も更新する
    private void checkBoxFlipDetectorLeftRight_CheckedChanged(object sender, EventArgs e)
    {
        InvalidateIndexingResults(); //260725Ch: XMirror は指数付けの検出器座標系を変えるため、候補と実行中結果を失効させる
        DrawEBSD();
        DrawOverlays();
    }

    private void checkBoxDrawDetectorOutline_CheckedChanged(object sender, EventArgs e)
    {
        flowLayoutPanelDetectorOutline.Enabled = checkBoxDrawDetectorOutline.Checked;
        Draw();
    }

    private void checkBoxShowKikuchiLines_CheckedChanged(object sender, EventArgs e)
    {
        flowLayoutPanelKikuchiLines.Enabled = checkBoxShowKikuchiLines.Checked;
        Draw();
    }

    private void colorControlExcessLine_Load(object sender, EventArgs e)
    {

    }

    #region 実測 EBSD パターンの指数付け (方位候補の探索と検出器幾何較正)
    //260726Cl 統合: 独立ファイルだった FormEBSD.Indexing.cs (partial) をここへ取り込み、探索・較正のアルゴリズム本体は
    //Crystallography/EBSD/ (EbsdOrientationSearch・EbsdGeometryCalibrator) へ分離した。このリージョンに残るのは UI orchestration のみ。
    //260725Cl: コントロールの置き場 — 中央列 EBSD pattern 配下の tabControlPatternSettings → Experimental image タブ
    //(探索エンジンのラジオ・Find/Calibrate ボタン・候補 DataGridView)。設計正本 = .project-guidance/ReciPro/ReciPro_EBSD総合設計・実装・高速化・引継ぎ.md §7。
    //260724Cl 方針転換 (作者指示): バンドの離散検出 (Detect bands) と中心線表示・Optimize orientation ボタンを廃止し、
    //「Find orientation candidates」に一本化。裏で Radon 証拠マップへの運動学的テンプレート照合 (EbsdRadonIndexer) で方位を直接探索し、
    //動力学 MasterPattern が生成済みなら上位候補へ ZNCC 精密化を自動連結する。

    //260724Cl 廃止: private List<EbsdBand> detectedBands (バンド離散検出の撤廃に伴い、中心線オーバーレイ表示ごと削除)

    /// <summary>指数付け候補 (スコア降順)</summary>
    private List<EbsdOrientationCandidate> orientationCandidates = null;

    private bool candidateGridInitialized = false;
    private bool skipCandidateSelectionEvent = false;

    /// <summary>解析系ボタンの相互排他 (実行中の二重起動・stale 結果の適用を防ぐ)。260724Cl 追加</summary>
    private bool indexingBusy = false;

    /// <summary>指数付け用反射リストの d 下限 (nm)。260724Cl (/simplify) 追加: 反射生成とステータス表示に二重ハードコードされていた値を一元化 (将来 UI 化候補)</summary>
    private const double KikuchiDLimit = 0.15;

    /// <summary>指数付け結果の世代番号。InvalidateIndexingResults で進み、await 跨ぎで失効した結果の適用を弾く。260725Cl 追加:
    /// indexingBusy は Find/Calibrate ボタンしか無効化しないため、await 中の画像 D&amp;D や検出器幾何の変更で失効させたはずの
    /// 候補が、探索完了後に無条件で復活していた (誤データ表示。クラッシュはしない)</summary>
    private int indexingGeneration = 0;

    /// <summary>画像・幾何変更で不要になった探索/較正のCPU処理を停止する。260725Ch 追加</summary>
    private System.Threading.CancellationTokenSource indexingCts;

    //260724Cl シグネチャ変更: バンド検出廃止に伴い clearBands 引数を削除。旧: private void InvalidateIndexingResults(bool clearBands)
    //260725Cl シグネチャ変更: announceCancel 追加。旧: private void InvalidateIndexingResults()
    /// <summary>画像/幾何の変更で方位候補を失効させる。260724Cl 追加 (Codex 指摘: stale 結果の誤適用防止)</summary>
    /// <param name="announceCancel">実行中なら中止要求をステータスバーへ出す。較正が自分の書き戻し後に呼ぶ場合だけ false</param>
    private void InvalidateIndexingResults(bool announceCancel = true)
    {
        indexingGeneration++; // 260725Cl 追加: 実行中の探索結果を失効させる
        //260725Cl 追加 (作者実機指摘): 探索中に幾何などを変えても画面が無反応に見えたので、中止要求を出した時点で表示する
        //(実際の停止はワーカーが次の中止チェックに到達するまで数十 ms 遅れる)
        if (announceCancel && indexingBusy) toolStripStatusLabelSummary.Text = "Canceling...";
        indexingCts?.Cancel(); //260725Ch: 結果を捨てるだけでなく、辞書/Radon探索と較正の残CPU処理も停止
        orientationCandidates = null;
        if (candidateGridInitialized)
        {
            skipCandidateSelectionEvent = true;
            try { dataGridViewEbsdCandidates.Rows.Clear(); }
            finally { skipCandidateSelectionEvent = false; }
        }
    }

    /// <summary>直近に進捗行を書き換えた時刻 (探索開始からの ms)。UI 更新の間引きに使う。260725Cl 追加</summary>
    private long lastIndexingProgressMs;

    /// <summary>進捗行に添える段の名前 (較正の "start 3/40" など)。260726Cl 追加</summary>
    private string indexingStage = "";

    //260726Cl 削除 (作者指示「2 分タイマーなど必要ない。常に最新の情報が出ていればよい」):
    //一時的なピン留め (statusPinnedUntilTick / SetPinnedStatus) は廃止。代わりに DrawEBSD 側で、
    //パターンの中身が変わらない再描画ではステータスを書き換えないようにした (lastEbsdRenderStatusKey)

    /// <summary>
    /// 探索・較正の進捗と経過時間をステータスバーへ出す (MasterPattern/MC と同じ canonical 進捗行)。260725Cl 追加
    /// (作者実機指摘: 探索中にプログレスバーが動かず、経過時間も出ていなかった)。
    /// ワーカースレッドから呼ばれるが、コントロールへの反映は StatusBarHelper 側が自動 Invoke する。
    /// </summary>
    //260726Cl シグネチャ変更: stage 追加 (較正の多点開始で "start 3/40" を出す)。旧: ReportIndexingProgress(double, Stopwatch)
    private void ReportIndexingProgress(double ratio, Stopwatch sw, string stage = null)
    {
        if (!indexingBusy) return; //完了後に遅れて届いた通知で最終表示を壊さない
        if (stage != null) indexingStage = stage;
        long now = sw.ElapsedMilliseconds;
        if (stage == null && ratio < 1 && now - lastIndexingProgressMs < 200) return; //UI 更新は毎秒 5 回まで (段が変わったときは間引かない)
        lastIndexingProgressMs = now;
        StatusBarHelper.SetProgress(toolStripProgressBar, toolStripStatusLabelProgress, ratio, indexingStage, sw.Elapsed, showRemaining: true);
    }

    /// <summary>探索・較正の終了を進捗行へ書く。完了は 100%、中止・失敗はバーを戻して理由と経過時間だけ残す。260725Cl 追加</summary>
    private void FinishIndexingProgress(Stopwatch sw, string canceledOrFailed = null)
    {
        if (canceledOrFailed == null)
            StatusBarHelper.SetProgress(toolStripProgressBar, toolStripStatusLabelProgress, 1.0, "", sw.Elapsed);
        else
        {
            toolStripProgressBar.Value = 0;
            toolStripStatusLabelProgress.Text = $"{canceledOrFailed} after {StatusBarHelper.FormatElapsed(sw.Elapsed)}";
        }
    }

    private bool TryBeginIndexing()
    {
        if (indexingBusy) return false;
        indexingBusy = true;
        lastIndexingProgressMs = 0; //260725Cl
        indexingStage = ""; //260726Cl
        StatusBarHelper.SetProgress(toolStripProgressBar, toolStripStatusLabelProgress, 0, "", TimeSpan.Zero, showRemaining: true); //260725Cl
        indexingCts?.Dispose(); //260725Ch: 前回は EndIndexing で破棄するが、例外的な経路でも古い CTS を保持しない
        indexingCts = new System.Threading.CancellationTokenSource(); //260725Ch
        buttonFindOrientation.Enabled = buttonCalibrateGeometry.Enabled = false; //260724Cl: 廃止 2 ボタンを除去
        return true;
    }

    private void EndIndexing()
    {
        indexingCts?.Dispose(); //260725Ch
        indexingCts = null;
        indexingBusy = false;
        buttonFindOrientation.Enabled = buttonCalibrateGeometry.Enabled = true; //260724Cl: 廃止 2 ボタンを除去
    }

    /// <summary>現在の UI 値から、実測画像のピクセルグリッドを基準にした検出器幾何スナップショットを作る</summary>
    private EbsdDetectorGeometry BuildDetectorGeometry(int imageWidth, int imageHeight)
        => new(DetTilt, DetX, DetY, DetZ, DetHalfWidth * 2 / imageWidth, imageWidth, imageHeight, DetectorXMirror, SmpTilt);

    //260724Cl 廃止: buttonDetectBands_Click / DrawDetectedBands (バンド離散検出とその中心線・縁点オーバーレイの撤廃。
    //検出パイプライン自体は EbsdBandDetector.Detect として Crystallography 側に残置 — 検証ハーネスが使用)

    #region 方位候補の探索 (Radon テンプレート照合 + ZNCC 自動精密化)

    private async void buttonFindOrientation_Click(object sender, EventArgs e)
    {
        if (expPbmp == null)
        {
            MessageBox.Show(this, "Load an experimental image first (drag && drop an image file).", "Find orientation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        //260724Cl 追加: Dictionary indexing (Primary) は MasterPattern 由来の辞書パターンと総当たり比較するため生成済みが必須
        bool useDictionary = radioButtonIndexingDictionary.Checked;
        if (useDictionary && MasterPattern == null)
        {
            MessageBox.Show(this, "Dictionary search requires the dynamical master pattern. Build it first, or use Radon search.", "Find orientation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        if (!TryBeginIndexing()) return;
        int generation = indexingGeneration; // 260725Cl 追加: await 中に画像・幾何が変わったら結果を捨てる
        var cancel = indexingCts.Token; //260725Ch: 無効化時に実計算も停止する
        toolStripStatusLabelSummary.Text = "Searching orientation candidates...";
        var sw = Stopwatch.StartNew(); //260725Cl: 中止・失敗時にも経過時間を出すので try の外で開始する
        try
        {
            var geom = BuildDetectorGeometry(expPbmp.Width, expPbmp.Height);
            var crystal = Crystal;

            //指数付け用の反射リスト (d>1.5Å) を UI スレッド上で一時生成し、表示用 VectorOfG_KikuchiLine は退避→復元する
            //(描画スレッドと共有される crystal 状態をワーカーから書き換えないため。生成は数十 ms。例外時も finally で必ず復元)
            var backup = crystal.VectorOfG_KikuchiLine;
            Vector3D[] reflections;
            try
            {
                crystal.SetVectorOfG_KikuchiLine(KikuchiDLimit, waveLengthControl.WaveSource);
                reflections = [.. crystal.VectorOfG_KikuchiLine];
            }
            finally { crystal.VectorOfG_KikuchiLine = backup; }

            var values = expPbmp.SrcValuesGray;
            int iw = expPbmp.Width, ih = expPbmp.Height;
            double wl = WaveLength; //nm (pair-angle シードの幅尤度用)

            //動力学 MasterPattern が生成済みなら ZNCC 複合ランクを自動連結 (旧 Optimize orientation ボタン相当。260724Cl 作者指示)。
            //複合ランクの設計根拠 (生 ZNCC 再ランクが有害な理由・証拠飽和 cap・トップのみ精密化) は EbsdOrientationSearch のクラスコメント参照
            bool refineByZncc = MasterPattern != null;
            var ctx = refineByZncc ? SnapshotMatchingContext() : null;
            //260725Cl: UI スレッドで結晶状態をスナップショット。FZ 除外は proper 回転 1 個 (monoclinic C2) のみ実測検証済みなので、
            //cubic/hex 等の高対称系は pruning on/off の候補一致を検証するまで安全側で無効化する (Codex 裁定 260725。実測パターンが揃ったら解除)
            var properSyms = useDictionary && EbsdDictionaryIndexer.GetProperRotations(crystal) is { Length: 1 } syms ? syms : null;

            void Report(double r) => ReportIndexingProgress(r, sw); //260725Cl: 粗探索から進捗と経過時間を受ける
            //260726Cl: 探索本体は Crystallography/EBSD/EbsdOrientationSearch.cs へ分離 (旧はこの Task.Run の中に直書きしていた)
            var candidates = await Task.Run(() => EbsdOrientationSearch.Run(values, iw, ih, geom, reflections, wl, useDictionary, ctx,
                properSymmetries: properSyms, maxCandidates: 10, cancel: cancel, progress: Report), cancel); //260725Ch
            sw.Stop();

            //260725Cl 追加: 探索中に実測画像の差し替えや検出器幾何の変更があった場合、この結果は既に失効しているので適用しない
            if (generation != indexingGeneration)
            {
                toolStripStatusLabelSummary.Text = "Orientation search discarded (the image or geometry changed)";
                FinishIndexingProgress(sw, "Canceled"); //260725Cl
                return;
            }
            orientationCandidates = candidates;
            FillCandidateGrid();
            FinishIndexingProgress(sw); //260725Cl: 進捗行を 100% で締める
            //260724Cl: 使用モードを明示 (Codex 裁定)。旧: (refineByZncc ? " (ZNCC refined)" : "")
            toolStripStatusLabelSummary.Text = $"Orientation search: {candidates.Count} candidates" +
                (useDictionary ? " (Dictionary + ZNCC combo)" : refineByZncc ? " (Radon + ZNCC combo)" : " (Radon only)"); //260724Cl: Dictionary モード表示追加
            toolStripStatusLabelDetail.Text = $"{sw.Elapsed.TotalMilliseconds:f0} ms, {reflections.Length} reflections (d>{KikuchiDLimit * 10:0.#}A). Click a row to apply the orientation."; //260724Cl (/simplify): 表示値を定数から導出
        }
        catch (OperationCanceledException) //260725Ch: 入力変更による正常な中止を失敗表示にしない
        {
            toolStripStatusLabelSummary.Text = generation == indexingGeneration ? "Orientation search canceled" : "Orientation search discarded (the image or geometry changed)";
            toolStripStatusLabelDetail.Text = "";
            FinishIndexingProgress(sw, "Canceled"); //260725Cl
        }
        catch (Exception ex)
        {
            toolStripStatusLabelSummary.Text = "Orientation search failed";
            toolStripStatusLabelDetail.Text = ex.Message;
            FinishIndexingProgress(sw, "Failed"); //260725Cl
        }
        finally { EndIndexing(); }
    }

    private void EnsureCandidateGridColumns()
    {
        if (candidateGridInitialized) return;
        candidateGridInitialized = true;
        var g = dataGridViewEbsdCandidates;
        g.AllowUserToAddRows = false;
        g.ReadOnly = true;
        g.RowHeadersVisible = false;
        g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        g.MultiSelect = false;
        g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        //260724Cl: Radon 方位探索用に列を再構成 (Score=SNR z 値、Bands=強い証拠を持つ予測バンド/視野内予測バンド、RMS° 列は廃止)
        //260725Cl 変更: 幅を DPI 換算 + 最終列を Fill に。旧実装は 96 DPI 前提の生ピクセル固定だったため、
        //フォントだけ DPI で拡大して高 DPI でヘッダが文字切れし、かつ固定合計幅がグリッド実幅を超えて常時横スクロールになっていた
        //(BeamInteraction 4 表の [project_minitable_readonly_grid] と同型の問題)
        int Dpi(int px96) => (int)Math.Round(px96 * DeviceDpi / 96.0);
        //旧: Width = 24 / 46 / 44 / 48 / 284 (px 固定)
        g.Columns.AddRange(
            new DataGridViewTextBoxColumn { HeaderText = "#", Width = Dpi(28) },
            new DataGridViewTextBoxColumn { HeaderText = "Score", Width = Dpi(52) },
            new DataGridViewTextBoxColumn { HeaderText = "Bands", Width = Dpi(52) },
            new DataGridViewTextBoxColumn { HeaderText = "ZNCC", Width = Dpi(52) },
            //残り幅を最終列が吸収する (hkl 列は内容が可変長なので Fill が自然。横スクロールバーも出なくなる)
            new DataGridViewTextBoxColumn { HeaderText = "Strong bands (hkl)", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, MinimumWidth = Dpi(120) });
        g.SelectionChanged += dataGridViewEbsdCandidates_SelectionChanged;
    }

    private void FillCandidateGrid()
    {
        EnsureCandidateGridColumns();
        var g = dataGridViewEbsdCandidates;
        skipCandidateSelectionEvent = true;
        try
        {
            g.Rows.Clear();
            if (orientationCandidates != null)
                foreach (var (c, i) in orientationCandidates.Select((c, i) => (c, i)))
                    //260725Cl: Score も非有限ガード (辞書経路は ScoreOrientations の double.MinValue センチネル
                    //= 視野内予測バンドが 4 本未満のとき をそのまま入れるため、309 桁の数値がセルに出るのを防ぐ)
                    //g.Rows.Add(i, double.IsFinite(c.Score) ? $"{c.Score:f1}" : "-", $"{c.AssignedBands}/{c.TotalBands}", //260725Ch 変更前: double.MinValue は finite なので 309 桁表示になっていた
                    //    double.IsNaN(c.Zncc) ? "-" : $"{c.Zncc:f3}", c.HklText);
                    g.Rows.Add(i, c.Score != double.MinValue && double.IsFinite(c.Score) ? $"{c.Score:f1}" : "-", $"{c.AssignedBands}/{c.TotalBands}", //260725Ch
                        double.IsFinite(c.Zncc) ? $"{c.Zncc:f3}" : "-", c.HklText); //260724Cl: AssignmentText (band:hkl) → HklText
            g.ClearSelection();
        }
        finally { skipCandidateSelectionEvent = false; }
    }

    /// <summary>候補行の選択で方位を全アプリへ適用 (シミュレーションが実測に重なって描画される)</summary>
    private void dataGridViewEbsdCandidates_SelectionChanged(object sender, EventArgs e)
    {
        if (skipCandidateSelectionEvent || orientationCandidates == null || dataGridViewEbsdCandidates.SelectedRows.Count == 0) return;
        int idx = dataGridViewEbsdCandidates.SelectedRows[0].Index;
        if ((uint)idx < (uint)orientationCandidates.Count)
            FormMain.SetRotation(orientationCandidates[idx].Rotation);
    }

    #endregion

    #region ZNCC 用スナップショット・検出器幾何較正 (動力学 MasterPattern 必須)

    //260725Cl (/simplify): ローカル PerturbRotation は EbsdIndexer.PerturbRotation へ統合 (Crystallography 側の
    //EbsdDictionaryIndexer.Perturb・EbsdRadonIndexer.Perturb と 3 重複していた。式・演算順・規約 (試料系左摂動) は同一)

    /// <summary>MC 重み合成パターンのキャッシュ (MasterPattern と mcDistribution の組が同一なら再利用、合成は ~100ms)。260724Cl 追加</summary>
    private (MasterPattern Mp, EbsdMonteCarloDistribution Dist, float[] Pos, float[] Neg) composedPatternCache;

    /// <summary>ZNCC 系操作に必要な状態を UI スレッド上でスナップショットする (ワーカーからコントロールを読まないため)。260724Cl 追加</summary>
    //260726Cl 戻り値変更: 匿名タプル → EbsdMatchingContext (Crystallography 側の record。探索・較正の両方へそのまま渡せる)
    private EbsdMatchingContext SnapshotMatchingContext()
    {
        var geom = BuildDetectorGeometry(expPbmp.Width, expPbmp.Height);
        var mp = MasterPattern;
        float[] pos, neg;
        //260724Cl 改訂 (作者指示「エネルギー 1 点はまずい」): MC 分布があれば全ビン平均重みの微分合成パターン
        //(実稼働の表示合成 model 2 のグローバル近似) を ZNCC 比較に使う。単一スライスより実測との相関が上がることをハーネスで実証。
        //MC 未実行 (通常は MasterPattern build 前段で必ず走る) 時のみ旧来の trackBar 選択単一スライスへフォールバック
        //if (mcDistribution != null) // 260727Cl 変更前: 格子一致を見ておらず、ずれた重みで合成した参照パターンで採点し得た
        if (EnsureMcDistributionMatchesMasterPattern()) // 260727Cl
        {
            if (!ReferenceEquals(composedPatternCache.Mp, mp) || !ReferenceEquals(composedPatternCache.Dist, mcDistribution))
            {
                var (p, n) = mcDistribution.ComposeGlobalWeightedPattern(mp);
                composedPatternCache = (mp, mcDistribution, p, n);
            }
            (pos, neg) = (composedPatternCache.Pos, composedPatternCache.Neg);
        }
        else
        {
            int eIdx = Math.Clamp(trackBarOutputEnergy.Value, 0, mp.Energies.Length - 1);
            int dIdx = Math.Clamp(trackBarOutputThickness.Value, 0, mp.Depths.Length - 1);
            pos = mp.GetPlane(MasterPattern.Hemisphere.PositiveZ, eIdx, dIdx);
            neg = mp.GetPlane(MasterPattern.Hemisphere.NegativeZ, eIdx, dIdx);
        }
        var (refData, rw, rh) = EbsdPatternScorer.PrepareReference(expPbmp.SrcValuesGray, expPbmp.Width, expPbmp.Height, 160);
        return new EbsdMatchingContext(geom, mp, pos, neg, refData, rw, rh, new Matrix3D(Crystal.RotationMatrix));
    }

    private bool CheckMatchingPrerequisites(string title)
    {
        if (expPbmp == null)
        {
            MessageBox.Show(this, "Load an experimental image first (drag && drop an image file).", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }
        if (MasterPattern == null)
        {
            MessageBox.Show(this, "Build the dynamical master pattern first (this function compares simulated and experimental patterns pixel by pixel).", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }
        return true;
    }

    //260724Cl 廃止: buttonOptimizeOrientation_Click (Find orientation candidates の ZNCC 自動連結へ統合。作者指示)

    /// <summary>
    /// 検出器のパターンセンター (PC) と検出器距離 (DD) を較正する (方位も交互に微調整)。DetTilt は固定。260724Cl 追加
    /// 単一パターンでは DetTilt と方位 X 回転がゲージ自由度になるため Tilt は較正しない (設計正本 §7.2 / Codex 裁定)。
    /// 最適化の中身 (交互法 → 方位仕上げ → 6 変数同時最適化 × 多点開始) は EbsdGeometryCalibrator を参照。
    /// 結果は DetX/DetY/DetZ へ逆変換して書き戻す。
    /// </summary>
    private async void buttonCalibrateGeometry_Click(object sender, EventArgs e)
    {
        if (!CheckMatchingPrerequisites("Calibrate detector geometry")) return;
        if (!TryBeginIndexing()) return;
        int generation = indexingGeneration; // 260725Cl 追加: await 中に実測画像・幾何が変わったら較正結果を書き戻さない (旧画像に合わせた幾何の誤適用防止)
        var cancel = indexingCts.Token; //260725Ch
        toolStripStatusLabelSummary.Text = "Calibrating detector geometry (PC/DD + orientation)...";
        var sw = Stopwatch.StartNew(); //260725Cl: 中止・失敗時にも経過時間を出すので try の外で開始する
        try
        {
            var ctx = SnapshotMatchingContext();
            var (footU0, footV0) = ctx.Geometry.PatternCenterMm; //260724Cl (/simplify): PC 式の手書き重複 (-DetX, -(DetY cosδ+DetZ sinδ)) を幾何オブジェクトへ一元化
            double dd0 = ctx.Geometry.CameraLength;
            double physW = DetHalfWidth * 2, physH = DetHalfHeight * 2;
            //if (dd0 < 1E-3) { toolStripStatusLabelSummary.Text = "Invalid camera length"; EndIndexing(); return; } //260725Ch 変更前: finally でも二重に EndIndexing していた
            if (dd0 < 1E-3) { toolStripStatusLabelSummary.Text = "Invalid camera length"; return; } //260725Ch

            void Report(double r, string stage) => ReportIndexingProgress(r, sw, stage); //260726Cl: 多点開始の段名も受ける
            //260726Cl: 較正本体は Crystallography/EBSD/EbsdGeometryCalibrator.cs へ分離 (旧はこの Task.Run の中に直書きしていた)
            var result = await Task.Run(() => EbsdGeometryCalibrator.Run(ctx, physW, physH, cancel, Report), cancel); //260725Ch
            sw.Stop();

            //260725Cl 追加: 較正中に実測画像の差し替えや幾何の変更があった場合、この結果は失効しているので書き戻さない
            if (generation != indexingGeneration)
            {
                toolStripStatusLabelSummary.Text = "Geometry calibration discarded (the image or geometry changed)";
                FinishIndexingProgress(sw, "Canceled"); //260725Cl
                return;
            }

            //DetX/DetY/DetZ へ逆変換して書き戻し (DetTilt 固定)。numericBox の範囲へクランプ (260724Cl)
            var (detX, detY, detZ) = EbsdDetectorGeometry.FromPatternCenter(result.PatternCenterU, result.PatternCenterV, result.CameraLength, ctx.Geometry.DetTilt);
            skipDetectorGeometryEvent = true;
            try
            {
                numericBoxXofDet.Value = Math.Clamp(detX, numericBoxXofDet.Minimum, numericBoxXofDet.Maximum);
                numericBoxYofDet.Value = Math.Clamp(detY, numericBoxYofDet.Minimum, numericBoxYofDet.Maximum);
                numericBoxZofDet.Value = Math.Clamp(detZ, numericBoxZofDet.Minimum, numericBoxZofDet.Maximum);
            }
            finally { skipDetectorGeometryEvent = false; }
            UpdateEbsdTiltCoeffs();
            RebinMcDistribution();
            InvalidateIndexingResults(announceCancel: false); //260725Ch: 較正前の幾何で得た候補を残さず、実行世代も進める (260725Cl: これは自分の書き戻しなので "Canceling..." は出さない)
            FormMain.SetRotation(result.Rotation); //Draw は SetRotation → FormMain 経由で走る
            FinishIndexingProgress(sw); //260725Cl: 進捗行を 100% で締める (InvalidateIndexingResults の "Canceling..." より後に出す)

            //260726Cl 変更 (作者報告「最後に消える」の真因): StatusStrip はフォーム幅 (1424px) に収まらない項目を描画しないので、
            //長い文字列を入れると書いた瞬間に見えなくなる。旧 Detail は 220 文字あった。表示は 2 ラベル合計 160 文字程度に収める。
            //旧 Detail: "PC (...)→(...) mm, DD ...→... mm, best of N starts (#k, m within 1E-3, spread ...), r/R rounds (...), joint 6-var ..., E evals, T ms. Tilt is kept fixed (single-pattern gauge)."
            toolStripStatusLabelSummary.Text = $"Geometry calibrated: ZNCC {result.ZnccStart:f3} → {result.Zncc:f3}" +
                $" (best #{result.BestIndex}/{result.Starts}, {result.NearBest} within 1E-3)"; //260726Cl: 多点開始。within が少ないほど局所解が深い
            //260725Cl: 交互最適化のラウンド数と収束可否 (上限に張り付くなら未収束の疑い)。260726Cl: evals と傾斜の注記は冗長なので削除
            //260726Cl: flat = ZNCC が最良と 1E-3 以内で並ぶ解の集団における PC・DD の広がり (半値幅)。単一パターンで幾何がどこまで決まるかの実測値
            toolStripStatusLabelDetail.Text = $"ΔPC ({result.PatternCenterU - footU0:+0.00;-0.00},{result.PatternCenterV - footV0:+0.00;-0.00}), ΔDD {result.CameraLength - dd0:+0.00;-0.00} mm; " +
                $"flat ±{result.FlatU:f2}/±{result.FlatV:f2} PC, ±{result.FlatDd:f2} DD; " +
                $"{result.Rounds}/{EbsdGeometryCalibrator.MaxRounds} rounds{(result.Converged ? "" : " (limit)")}, joint {(result.JointGain > 0 ? "+" : "")}{result.JointGain:f4}, {sw.Elapsed.TotalSeconds:f1} s";
        }
        catch (OperationCanceledException) //260725Ch
        {
            toolStripStatusLabelSummary.Text = generation == indexingGeneration ? "Geometry calibration canceled" : "Geometry calibration discarded (the image or geometry changed)";
            toolStripStatusLabelDetail.Text = "";
            FinishIndexingProgress(sw, "Canceled"); //260725Cl
        }
        catch (Exception ex)
        {
            toolStripStatusLabelSummary.Text = "Geometry calibration failed";
            toolStripStatusLabelDetail.Text = ex.Message;
            FinishIndexingProgress(sw, "Failed"); //260725Cl
        }
        finally { EndIndexing(); }
    }

    #endregion

    #endregion
}


