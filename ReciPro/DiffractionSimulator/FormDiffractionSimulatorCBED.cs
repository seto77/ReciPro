#region using
using MathNet.Numerics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace ReciPro;

public partial class FormDiffractionSimulatorCBED : FormBase
{
    #region フィールド、プロパティ

    public FormDiffractionSimulator FormDiffractionSimulator;

    public bool LACBED => radioButtonLACBED.Checked;

    private double Voltage => FormDiffractionSimulator.waveLengthControl.Energy;
    private Crystal Crystal => FormDiffractionSimulator.formMain.Crystal;

    // 260416Cl Division が変わらない限り結果は同じなのでキャッシュ化。
    // 以前は毎呼び出しで Parallel.For が走り、ProgressChanged ハンドラ内で 4 回も参照されるため UI スレッドが周期的にフリーズしていた。
    private int _divisionNumberCache = -1;
    private int _divisionNumberCacheKey = -1;

    public int DivisionNumber
    {
        get
        {
            int div = Division;
            if (_divisionNumberCacheKey == div)
                return _divisionNumberCache;

            double radius = div / 2.0;
            if (radius > 3000)
            {
                _divisionNumberCacheKey = div;
                _divisionNumberCache = -1;
                return -1;
            }
            double radius2 = radius * radius;
            int count = 0;
            Parallel.For(0, div, h =>
            {
                double pY = h - radius + 0.5, pY2 = pY * pY;
                for (int w = 0; w < div; w++)
                {
                    double pX = w - radius + 0.5;
                    if (pX * pX + pY2 <= radius2)
                        Interlocked.Increment(ref count);
                }
            });
            _divisionNumberCacheKey = div;
            _divisionNumberCache = count;
            return count;
        }
    }

    public double ResolutionInverse { get; set; }
    public double ResolutionReal { get; set; }

    public double AlphaMax => numericBoxAlphaMax.Value / 1000.0;

    public bool DrawGuideCircles => checkBoxDrawGuideCircles.Checked;
    public int MaxNumOfBloch => numericBoxMaxNumOfG.ValueInteger;

    public int Division => (int)Math.Ceiling(numericBoxAlphaMax.Value * 2 / numericBoxDiskResolution.Value);

    public double[] ThicknessArray
    {
        get
        {
            var thicknessArray = new List<double>();
            for (double thickness = numericBoxWholeThicknessStart.Value; thickness <= numericBoxThicknessEnd.Value; thickness += numericBoxThicknessStep.Value)
                thicknessArray.Add(thickness);
            return thicknessArray.ToArray();
        }
    }

    public double ImagePixelSize { get; set; } = 0;

    /// <summary>中心付近の1ピクセル当たりの角度(radian)</summary>
    public double AngleResolution { get; set; }


    public (int H, int K, int L, PointD Center, SizeD Size, Size PixelSize, PseudoBitmap PBitmap, Bitmap Bitmap)[] Disks;


    private Vector3DBase[] Directions;

    /// <summary>261105Cl 追加 (監査 #32・D7・D8): 計算中の run の Bethe (開始時の結晶のもの)・収束半角 (rad)・厚みの列。null = 計算していない。
    /// 計算中に主窓で結晶を替えても、Stop と完了の処理はこの run に対して行う (以前は Crystal.Bethe = 新しい結晶の Bethe に対して行っていた)</summary>
    private BetheMethod runningBethe;
    private double runningAlphaMax;
    private double[] runningThicknesses = [];

    /// <summary>261105Cl 追加 (監査 #32・D7・D8): 最後に完了した run の結果 (ディスク・入射方向・収束半角 (rad)・厚みの列)。表示・厚みの切り替え・保存はこれだけを見る。
    /// Bethe.Disks は Stop・例外のときに途中の状態 (振幅の無いディスク) で上書きされ、EBSD の計算でも上書きされるので見ない。
    /// 厚み・収束半角は計算したときの値で、計算の後に入力欄を変えても表示・ファイル名は変わらない</summary>
    private BetheMethod.CBED_Disk[][] completedDisks;
    private Vector3DBase[] completedDirections;
    private double completedAlphaMax;
    /// <summary>261105Cl 追加: 最後に完了した run の厚みの列 (nm)。trackBarOutputThickness の値はこの添字</summary>
    public double[] CompletedThicknesses { get; private set; } = [];

    private readonly Stopwatch sw1 = new();
    private readonly Stopwatch sw2 = new();

    private bool skipEvent { get; set; } = false;

    #endregion

    #region コンストラクタ、ロード、クローズ
    public FormDiffractionSimulatorCBED()
    {
        InitializeComponent();
        //HelpPage = "7-diffraction-simulator/4-cbed-simulation"; //260529Cl 追加 (旧: 実ページは 3-cbed-simulation、4- は x-ray-neutron)
        HelpPage = "7-diffraction-simulator/3-cbed-simulation"; //260623Cl: 実ページ名へ修正 (HelpCulture flip で 9 言語に 404 波及するため)
        NumericBoxDivision_ValueChanged(this, EventArgs.Empty);

        //260611Cl 追加: ARM64 には MKL ネイティブが存在しない (x86/x64 専用) ため、MKL の選択肢自体を出さない
        if (System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture == System.Runtime.InteropServices.Architecture.Arm64)
            for (var i = comboBoxSolver.Items.Count - 1; i >= 0; i--)
                if ($"{comboBoxSolver.Items[i]}".Contains("MKL"))
                    comboBoxSolver.Items.RemoveAt(i);

        if (!BetheMethod.EigenEnabled)
        {
            //260611Cl 固定インデックスは ARM64 の MKL 項目除去後にずれるため、内容ベースの除去に変更
            for (var i = comboBoxSolver.Items.Count - 1; i >= 0; i--)
                if ($"{comboBoxSolver.Items[i]}".Contains("Eigen lib"))
                    comboBoxSolver.Items.RemoveAt(i);
        }
        comboBoxSolver.SelectedIndex = 0;
        comboBoxScale.SelectedIndex = 0;
        comboBoxGradient.SelectedIndex = 0;
    }

    private void FormDiffractionSimulatorMultislice_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        //if (this.Visible && sender == this) // 261105Cl 変更前
        if (this.Visible && sender == this && e.CloseReason == CloseReason.UserClosing) // 261105Cl 変更: アプリの終了時には親のモードを切り替えない
            //FormDiffractionSimulator.radioButtonIntensityDynamical.Checked = true; // 261105Cl 変更前: 収束モードでは Dynamical はすでにチェックされているので何も起きず、× で閉じられなかった
            FormDiffractionSimulator.BeamMode = FormDiffractionSimulator.BeamModes.Parallel; // 261105Cl 変更 (監査 D20・C25): この窓は収束 (CBED) モードの操作パネルなので、× は平行ビームへ戻す (親のハンドラがこの窓を隠す)
    }
    #endregion

    #region 実行/停止ボタン
    private void buttonSimulate_Click(object sender, EventArgs e)
    {
        //if (Crystal.Bethe.IsCBED_Busy) return; // 261105Cl 変更前
        if (runningBethe != null || Crystal.Bethe.IsCBED_Busy) return; // 261105Cl 変更: 別の結晶の Bethe で計算中のときも二重に始めない

        buttonStop.Visible = true;
        sw2.Reset();
        sw1.Restart();
        FormDiffractionSimulator.SkipDrawing = true;
        //Crystal.Bethe.CBED_Completed += Bethe_CbedCompleted; // 261105Cl 変更前
        //Crystal.Bethe.CBED_ProgressChanged += Bethe_CbedProgressChanged;
        runningBethe = Crystal.Bethe; // 261105Cl 追加 (監査 #32)
        runningAlphaMax = AlphaMax; // 261105Cl 追加 (監査 D7)
        runningThicknesses = ThicknessArray; // 261105Cl 追加 (監査 D7)
        runningBethe.CBED_Completed += Bethe_CbedCompleted; // 261105Cl 変更
        runningBethe.CBED_ProgressChanged += Bethe_CbedProgressChanged; // 261105Cl 変更

        //ローテーション配列を作る //一辺が2.の正方形の中に一辺1/Nのピクセルを詰め込み、中心ピクセルが、円の中心とちょうど一致するような問題を考える
        //260717Cl 変更: List+spread を固定長配列の直接構築に (要素数 = Division² で既知)
        var radius = Division / 2.0;
        Directions = new Vector3DBase[Division * Division];
        for (int h = 0; h < Division; h++)
            for (int w = 0; w < Division; w++)
            {
                var x = (w - radius + 0.5) / (radius - 0.5) * Math.Sin(AlphaMax);
                var y = -(h - radius + 0.5) / (radius - 0.5) * Math.Sin(AlphaMax);//結晶の座標系は、X軸が右、Y軸が上、Z軸が手前なのでYを反転
                Directions[h * Division + w] = new Vector3DBase(x, y, -Math.Sqrt(1 - x * x - y * y));
            }


        BetheMethod.Solver solver;
        if (comboBoxSolver.Text.Contains("Eigenproblem"))
            solver = comboBoxSolver.Text.Contains("MKL") ? BetheMethod.Solver.Eigen_MKL : BetheMethod.Solver.Eigen_Eigen;
        else
            solver = comboBoxSolver.Text.Contains("MKL") ? BetheMethod.Solver.MtxExp_MKL : BetheMethod.Solver.MtxExp_Eigen;

        // 261110Cl 追加: ビーム選択の方式。新方式では「回折波の数」の欄 = E1 の基準の本数 N (既定 300、遅い反射の繰り上げで明示は増える)
        runningBethe.CbedBeamSelection = checkBoxDressedTail.Checked ? BetheMethod.CbedBeamSelectionMethod.DressedTail : BetheMethod.CbedBeamSelectionMethod.Legacy;
        if (checkBoxDressedTail.Checked)
            runningBethe.CbedDressedTail = BetheMethod.CbedDressedTailDefaults with { N = MaxNumOfBloch };

        //Crystal.Bethe.RunCBED(MaxNumOfBloch, Voltage, Crystal.RotationMatrix, ThicknessArray, Directions, LACBED, solver, numericBoxThread.ValueInteger); // 261105Cl 変更前
        // runningBethe.RunCBED(MaxNumOfBloch, Voltage, Crystal.RotationMatrix, runningThicknesses, Directions, LACBED, solver, numericBoxThread.ValueInteger); // 261105Cl 変更 // 261110Cl 変更前
        // 261110Cl 変更 (codex2 第 37 回): 新方式では欄の値は新方式の N。従来へ戻るときの最大の Bloch 波の数は、覚えている従来の値 (切り替える前の欄の値)
        int legacyN = checkBoxDressedTail.Checked && legacyMaxNumOfG > 0 ? legacyMaxNumOfG : MaxNumOfBloch;
        runningBethe.RunCBED(legacyN, Voltage, Crystal.RotationMatrix, runningThicknesses, Directions, LACBED, solver, numericBoxThread.ValueInteger);
    }
    private void buttonStop_Click(object sender, EventArgs e)
    {
        //Crystal.Bethe.CancelCBED(); // 261105Cl 変更前
        runningBethe?.CancelCBED(); // 261105Cl 変更 (監査 #32): 計算を始めた結晶の Bethe を止める
        //buttonSimulate.Text = "Simulate"; // 260520Cl: Execute→Simulate (用語統一) // 261105Cl 削除 (監査 D23): 計算中に文言を変えていないのに、英語の "Simulate" で上書きして ja などの訳 (「開始」) を消していた
        buttonStop.Visible = false;
    }
    #endregion

    #region BackgroundWorkerからのProgressChanged, Completed

    private bool skipProgressChangedEvent = false;

    private void Bethe_CbedProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        if (skipProgressChangedEvent) return;

        skipProgressChangedEvent = true;

        var current = e.ProgressPercentage;
        var message = (string)e.UserState;
        if (message.StartsWith("Compiling disks", StringComparison.Ordinal))
        {
            if (sw1.IsRunning)
            {
                sw1.Stop();
                sw2.Restart();
            }
            var sec = sw2.ElapsedMilliseconds / 1000.0;
            var totalsec = sec + sw1.ElapsedMilliseconds / 1000.0;
            toolStripProgressBar.Value = current / 10;
            toolStripStatusLabel2.Text = "Compiling disks:";
            toolStripStatusLabel1.Text = $"Elapsed time : {totalsec:f2} s,  ";
            toolStripStatusLabel1.Text += $"{current / 10.0:f1} % completed,  wait for more {sec * (1000 - current) / current:f2} s";
        }
        else
        {
            var sec = sw1.ElapsedMilliseconds / 1000.0;
            var progress = (int)(100.0 * current / DivisionNumber);
            if (progress <= 100)
                toolStripProgressBar.Value = (int)(100.0 * current / DivisionNumber);
            toolStripStatusLabel2.Text = message;
            toolStripStatusLabel1.Text = $"Elapsed time : {sec:f2} s,  time/pixel: ";
            toolStripStatusLabel1.Text += sec / current > 0.9 ? $"{sec / current:f2} s,  " : $"{sec / current * 1000:f2} ms, ";
            toolStripStatusLabel1.Text += $"{100.0 * current / DivisionNumber:f1} % completed,  wait for {sec * (DivisionNumber - current) / current:f2} s";
        }
        // 260428Cl Application.DoEvents() を削除 (BackgroundWorker の ProgressChanged は UI スレッドで動作するため不要)
        skipProgressChangedEvent = false;
    }

    private void Bethe_CbedCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        buttonStop.Visible = false;
        FormDiffractionSimulator.SkipDrawing = false;
        //Crystal.Bethe.CBED_Completed -= Bethe_CbedCompleted; // 261105Cl 変更前
        //Crystal.Bethe.CBED_ProgressChanged -= Bethe_CbedProgressChanged;
        var bethe = runningBethe; // 261105Cl 変更 (監査 #32): 計算を始めた結晶の Bethe から外す
        runningBethe = null;
        bethe.CBED_Completed -= Bethe_CbedCompleted;
        bethe.CBED_ProgressChanged -= Bethe_CbedProgressChanged;
        sw1.Stop(); // 261105Cl 追加: Bloch の段階で中止・例外になったとき、経過時間が流れ続けないように
        sw2.Stop();
        var sec1 = sw1.ElapsedMilliseconds / 1000.0;
        var sec2 = sw2.ElapsedMilliseconds / 1000.0;

        // 261105Cl 追加 (監査 D8): 計算中の例外 (e.Error) を成功 (「100% completed!」) として扱い、途中のディスクで画像を作っていた。
        //   前回の結果 (completedDisks) はそのまま残す
        if (e.Error != null)
        {
            toolStripStatusLabel2.Text = "Failed:  ";
            toolStripStatusLabel1.Text = $"Time elapsed: {sec1 + sec2:f2} s,  {e.Error.Message}";
            groupBoxOutput.Enabled = completedDisks != null;
        }
        //if (!e.Cancelled) // 261105Cl 変更前
        else if (!e.Cancelled) // 261105Cl 変更
        {
            // 261105Cl 追加 (監査 #32・D7): 完了した run だけを結果として公開する。厚みの選択の範囲も、計算した厚みの列に合わせる
            completedDisks = bethe.Disks;
            completedDirections = Directions;
            completedAlphaMax = runningAlphaMax;
            CompletedThicknesses = runningThicknesses;
            skipEvent = true; // ここでは画像を作らない (下の generateImage で 1 回だけ作る)
            trackBarOutputThickness.Maximum = Math.Max(0, CompletedThicknesses.Length - 1);
            trackBarOutputThickness.Value = Math.Min(trackBarOutputThickness.Value, trackBarOutputThickness.Maximum);
            skipEvent = false;
            if (CompletedThicknesses.Length > 0)
                textBoxThickness.Text = CompletedThicknesses[trackBarOutputThickness.Value].ToString();

            toolStripStatusLabel2.Text = "100% completed!  ";
            toolStripStatusLabel1.Text = $"Total time: {sec1 + sec2:f2} s   ";
            toolStripStatusLabel1.Text += $"Bloch problem: {sec1:f2} s (";
            toolStripStatusLabel1.Text += sec1 / DivisionNumber > 1 ? $"{sec1 / DivisionNumber:f2} s " : $"{sec1 / DivisionNumber * 1000:f2} ms ";
            toolStripStatusLabel1.Text += $"/pixel).   Compiling disks: {sec2:f2} s"; // 260520Cl: typo fix (pixes → pixel)
            // 261110Cl 追加: ビーム選択の方式 (新方式を要求して従来へ戻ったら理由も)。内訳はステータスの説明に
            if (bethe.CbedBeamSelectionUsed == BetheMethod.CbedBeamSelectionMethod.DressedTail)
                toolStripStatusLabel1.Text += $".   Beam selection: dressed tail ({bethe.Beams.Length} disks)";
            else if (bethe.CbedBeamSelection == BetheMethod.CbedBeamSelectionMethod.DressedTail)
                toolStripStatusLabel1.Text += $".   Beam selection: conventional (the dressed-tail method was not used: {bethe.CbedBeamSelectionFallbackReason})";
            toolStripStatusLabel1.ToolTipText = bethe.CbedBeamSelectionUsed == BetheMethod.CbedBeamSelectionMethod.DressedTail ? bethe.CbedBeamSelectionDetail : "";
            if (!string.IsNullOrEmpty(bethe.CbedSymmetryDetail)) toolStripStatusLabel1.Text += $".   Zone-axis {bethe.CbedSymmetryDetail}"; // 261111Cl 追加: 晶帯軸の対称で代表の方向だけを解いたとき

            groupBoxOutput.Enabled = true;
            generateImage();

            FormDiffractionSimulator.FormDiffractionBeamTable.SetTable(FormDiffractionSimulator.waveLengthControl.Energy);
        }
        else
        {
            toolStripStatusLabel1.Text = $"Time elapsed: {sec1 + sec2:f2} s,  Manually interrupted."; // 260520Cl: typo fix (ellapsed → elapsed, interupted → interrupted)
            //groupBoxOutput.Enabled = false; // 261105Cl 変更前: 前回のディスクは表示したまま出力の欄だけ無効にしていた
            groupBoxOutput.Enabled = completedDisks != null; // 261105Cl 変更 (監査 #32): 中止した run は公開せず、前回完了した結果をそのまま使えるようにする
        }
        toolStripProgressBar.Value = toolStripProgressBar.Maximum;
        FormDiffractionSimulator.Draw();
        // 260428Cl Application.DoEvents() を削除 (RunWorkerCompleted は UI スレッドで動作するため不要)
    }

    #endregion

    #region 生画像や表示画像の構築
    private void setImagePixelSize()
    {
        //以下の二つのパラメータはγ値の調整のところで使う。（あまり意味のないパラメータなので、廃止したい）
        //AngleResolution = Math.Acos(Vector3DBase.AngleBetVectors(Directions[Directions.Length / 2], Directions[Directions.Length / 2 + 1]) / 2);// (260715Ch) 旧: 既に radian の角度へ回転行列 trace 用 acos を二重適用していた
        //AngleResolution = Vector3DBase.AngleBetVectors(Directions[Directions.Length / 2], Directions[Directions.Length / 2 + 1]);// (260715Ch) // 261105Cl 変更前
        AngleResolution = Vector3DBase.AngleBetVectors(completedDirections[completedDirections.Length / 2], completedDirections[completedDirections.Length / 2 + 1]); // 261105Cl 変更 (監査 D7): 表示中の結果を計算したときの入射方向 (次の run の Simulate で Directions が替わっても、中止すれば表示は前の結果)
        ImagePixelSize = FormDiffractionSimulator.CameraLength2 * Math.Tan(AngleResolution);
    }

    private void generateImage(bool resetDisks = true)
    {
        //if (skipEvent || FormDiffractionSimulator == null || Crystal == null || Crystal.Bethe.Disks == null) // 261105Cl 変更前
        if (skipEvent || FormDiffractionSimulator == null || completedDisks == null) // 261105Cl 変更 (監査 #32): 完了した run の結果だけを見る
            return;

        setImagePixelSize();

        if (resetDisks || Disks == null)
            setDisks();
        if (Disks == null)
            return;

        //γ値の調整
        var gamma = trackBarGamma.Value * AngleResolution;
        Parallel.For(0, Disks.Length, i =>
        {
            if (Disks[i].PBitmap != null)
            {
                var pb = Disks[i].PBitmap;
                var cPos = Disks[i].Center + Disks[i].Size / 2;
                pb.SrcValuesGray = pb.SrcValuesGrayOriginal.Select((v, n) =>
                {
                    (int Division, int Modulus) = Miscellaneous.DivMod(n, Disks[i].PixelSize.Width);
                    var pos = cPos - new PointD(Modulus, Division) * ImagePixelSize;
                    return v * Math.Exp(pos.Length * gamma);
                }).ToArray();
            }
        });

        var maxOverall = Disks.Max(d => d.PBitmap.SrcValuesGrayOriginal.Max());
        var maxRatio = Math.Pow((double)trackBarIntensityBrightnessMax.Value / trackBarIntensityBrightnessMax.Maximum, 4);
        var minRatio = Math.Pow((double)trackBarIntensityBrightnessMin.Value / trackBarIntensityBrightnessMin.Maximum, 4);
        var colorScale = comboBoxScale.SelectedIndex;
        var negative = comboBoxGradient.SelectedIndex == 1;

        Parallel.For(0, Disks.Length, i =>
        {
            //輝度Max,Min調整
            if (radioButtonAllDisks.Checked)
            {
                Disks[i].PBitmap.MaxValue = maxOverall * maxRatio;
                Disks[i].PBitmap.MinValue = maxOverall * minRatio;
            }
            else
            {
                Disks[i].PBitmap.MaxValue = Disks[i].PBitmap.SrcValuesGray.Max() * maxRatio;
                //Disks[i].PBitmap.MinValue = Disks[i].PBitmap.SrcValuesGray.Max() * maxOverall * minRatio; // 261105Cl 変更前: maxOverall が余分 (強度の 2 乗の次元)。最小値スライダーの効きが maxOverall 倍に弱まっていた
                Disks[i].PBitmap.MinValue = Disks[i].PBitmap.SrcValuesGray.Max() * minRatio; // 261105Cl 修正: Max と同じく各ディスクの最大値が基準 (全ディスク共通モードの maxOverall × ratio と対)
            }
            //GrayかColorか
            if (colorScale == 0)
                Disks[i].PBitmap.SetScaleGray();
            else if (colorScale == 1)
                Disks[i].PBitmap.SetScaleColdWarm();
            else if (colorScale == 2)
                Disks[i].PBitmap.SetScaleSpectrum();
            else
                Disks[i].PBitmap.SetScaleFire();
            //Negativeかどうか
            Disks[i].PBitmap.IsNegative = negative;
            Disks[i].Bitmap = Disks[i].PBitmap.GetImage();
        }
        );

        FormDiffractionSimulator.Draw();
        // 260428Cl Application.DoEvents() を削除 (UI スレッドでの単純な再描画後の DoEvents は不要)
    }

    private void setDisks()
    {
        //if (Crystal.Bethe.Disks == null || trackBarOutputThickness.Value >= Crystal.Bethe.Disks.Length) // 261105Cl 変更前
        if (completedDisks == null || trackBarOutputThickness.Value >= completedDisks.Length) // 261105Cl 変更 (監査 #32)
        {
            Disks = null;
            return;
        }
        //var disks = Crystal.Bethe.Disks[trackBarOutputThickness.Value]; // 261105Cl 変更前
        var disks = completedDisks[trackBarOutputThickness.Value]; // 261105Cl 変更 (監査 #32)
        Disks = new (int H, int K, int L, PointD Center, SizeD Size, Size PixelSize, PseudoBitmap PBitmap, Bitmap Bitmap)[disks.Length];
        var pixWidth = (int)Math.Sqrt(disks[0].Amplitudes.Length);
        Parallel.For(0, disks.Length, i =>
        //for (int i = 0; i < disks.Length; i++)
        {
            if (disks[i].Amplitudes != null)
            {
                var v = new { x = disks[i].G.X, y = -disks[i].G.Y, z = -disks[i].G.Z };//ここでベクトルのY,Zの符号を反転
                var center = Geometry.GetCrossPoint(0, 0, 1, FormDiffractionSimulator.CameraLength2, new Vector3D(0, 0, 0), new Vector3D(v.x, v.y, v.z + FormDiffractionSimulator.EwaldRadius));
                var pbmp = new PseudoBitmap(disks[i].Amplitudes.Select(amp => amp.MagnitudeSquared()).ToArray(), pixWidth)
                {
                    ReserveSrcValuesGrayOriginal = true,
                    AlphaEnabled = true,
                    FilterAlfha = disks[i].Amplitudes.Select(intensity => intensity == 0 ? (byte)0 : (byte)255).ToList()
                };

                //var realWidth = Math.Tan(AlphaMax) * FormDiffractionSimulator.CameraLength2 * 2; // 261105Cl 変更前
                var realWidth = Math.Tan(completedAlphaMax) * FormDiffractionSimulator.CameraLength2 * 2; // 261105Cl 変更 (監査 D7): 計算したときの収束半角 (計算後に欄を変えるとディスクの大きさが中身と合わなくなっていた)

                Disks[i] = (disks[i].H, disks[i].K, disks[i].L, center.ToPointD, new SizeD(realWidth, realWidth), new Size(pixWidth, pixWidth), pbmp, null);
            }
        }
        );
    }

    private void TrackBarOutputThickness_Scroll(object sender, EventArgs e)
    {
        //if (Crystal.Bethe.Disks == null || trackBarOutputThickness.Value >= Crystal.Bethe.Disks.Length) // 261105Cl 変更前
        if (skipEvent || completedDisks == null || trackBarOutputThickness.Value >= completedDisks.Length) // 261105Cl 変更 (監査 #32)
            return;
        if (trackBarOutputThickness.Value < 0)
            return;
        //textBoxThickness.Text = ThicknessArray[trackBarOutputThickness.Value].ToString(); // 261105Cl 変更前: 今の入力欄の厚み (計算後に範囲を変えると中身と違う値)
        textBoxThickness.Text = CompletedThicknesses[trackBarOutputThickness.Value].ToString(); // 261105Cl 変更 (監査 D7): 計算した厚み
        generateImage();
        FormDiffractionSimulator.FormDiffractionBeamTable.SetTable(FormDiffractionSimulator.waveLengthControl.Energy);
    }


    private void trackBarIntensityBrightnessMax_ValueChanged(object sender, EventArgs e) => generateImage(false);
    private void trackBarGamma_ValueChanged(object sender, EventArgs e) => generateImage(false);

    private void radioButtonAllDisks_CheckedChanged(object sender, EventArgs e)
    {
        if (radioButtonIndividualDisk.Checked)
        {
            skipEvent = true;
            trackBarGamma.Value = 0;
            skipEvent = false;
        }
        trackBarGamma.Enabled = labelGamma.Enabled = radioButtonAllDisks.Checked;
        generateImage(false);
    }
    #endregion

    #region 入力パラメータ関連のイベント

    private void numericBoxAlphaMax_ValueChanged(object sender, EventArgs e)
    {
        FormDiffractionSimulator.Draw();
        NumericBoxDivision_ValueChanged(sender, e);
    }

    private void radioButtonCBED_CheckedChanged(object sender, EventArgs e) => FormDiffractionSimulator.Draw();
    private void numericBoxMaxNumOfG_ValueChanged(object sender, EventArgs e) => FormDiffractionSimulator.Draw();

    private void CheckBoxDrawGuideCircles_CheckedChanged(object sender, EventArgs e) => FormDiffractionSimulator.Draw();

    /// <summary>261110Cl 追加: 新方式 (着衣つきの裾) の切り替え。「回折波の数」の欄は新方式では E1 の基準の本数 N (既定 300)、従来では最大の Bloch 波の数 (既定 64)。
    /// 切り替えのたびに両方の値を覚えて入れ替える (FormEBSD と同じ)</summary>
    int legacyMaxNumOfG = -1, dressedTailMaxNumOfG = BetheMethod.CbedDressedTailDefaults.N;
    private void checkBoxDressedTail_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBoxDressedTail.Checked) { legacyMaxNumOfG = numericBoxMaxNumOfG.ValueInteger; numericBoxMaxNumOfG.Value = dressedTailMaxNumOfG; }
        else { dressedTailMaxNumOfG = numericBoxMaxNumOfG.ValueInteger; if (legacyMaxNumOfG > 0) numericBoxMaxNumOfG.Value = legacyMaxNumOfG; }
    }

    private void NumericBoxDivision_ValueChanged(object sender, EventArgs e) =>
        labelDivisionNumber.Text = $"{DivisionNumber:#,0}";

    // 261105Cl 削除 (監査 D7): 厚みの範囲 (と、配線の誤りでスレッド数) の欄を変えるたびに、表示する厚みの選択を今の欄の列に作り直して 0 に戻していた。
    //   選択は計算した厚みの列の添字なので、計算の完了時 (Bethe_CbedCompleted) にだけ作る。Designer の配線 4 本も外した
    //private void NumericBoxWholeThicknessStart_ValueChanged(object sender, EventArgs e)
    //{
    //    trackBarOutputThickness.Maximum = ThicknessArray.Length - 1;
    //    trackBarOutputThickness.Value = 0;
    //}

    private void ComboBoxSolver_SelectedIndexChanged(object sender, EventArgs e)
    {
        numericBoxThread.Enabled = comboBoxSolver.SelectedIndex != 0;
        numericBoxThread.Value = comboBoxSolver.Text.Contains("MKL") ? Math.Max(1, Environment.ProcessorCount / 4) : Environment.ProcessorCount;//260717Cl: 冗長な二重代入 (…: numericBoxThread.Value = …) を整理
    }
    #endregion

    private void FormDiffractionSimulatorCBED_VisibleChanged(object sender, EventArgs e)
    {
        FormDiffractionSimulator.saveCBEDPatternToolStripMenuItem.Visible = Visible;
        FormDiffractionSimulator.copyCBEDPatternToolStripMenuItem.Visible = Visible;
    }
}

