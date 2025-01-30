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

public partial class FormDiffractionSimulatorCBED : Form
{
    #region フィールド、プロパティ

    public FormDiffractionSimulator FormDiffractionSimulator;

    public bool LACBED => radioButtonLACBED.Checked;

    private double Voltage => FormDiffractionSimulator.waveLengthControl.Energy;
    private Crystal Crystal => FormDiffractionSimulator.formMain.Crystal;

    public int DivisionNumber
    {
        get
        {
            int count = 0;
            double radius = Division / 2.0;
            if (radius > 3000)
                return -1;
            double radius2 = radius * radius;
            Parallel.For(0, Division, h =>
            {
                double pY = h - radius + 0.5, pY2 = pY * pY;
                for (int w = 0; w < Division; w++)
                {
                    double pX = w - radius + 0.5;
                    if (pX * pX + pY2 <= radius2)
                        Interlocked.Increment(ref count);
                }
            });
            return count;
        }
    }

    public double ResolutionInverse { get; set; }
    public double ResolutionReal { get; set; }

    //public double Defocus { get { return numericBoxDefocus.Value; } }
    //public double Cs { get { return numericBoxCs.Value; } }
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

    /// <summary>
    /// 中心付近の1ピクセル当たりの角度(radian)
    /// </summary>
    public double AngleResolution { get; set; }


    public (int H, int K, int L, PointD Center, SizeD Size, Size PixelSize, PseudoBitmap PBitmap, Bitmap Bitmap)[] Disks;


    private Vector3DBase[] Directions;

    private readonly Stopwatch sw1 = new();
    private readonly Stopwatch sw2 = new();

    private bool skipEvent { get; set; } = false;

    #endregion

    #region コンストラクタ、ロード、クローズ
    public FormDiffractionSimulatorCBED()
    {
        InitializeComponent();
        NumericBoxDivision_ValueChanged(new object(), new EventArgs());

        if (!NativeWrapper.Enabled)
        {
            comboBoxSolver.Items.RemoveAt(3);
            comboBoxSolver.Items.RemoveAt(1);
        }
        comboBoxSolver.SelectedIndex = 0;
        comboBoxScale.SelectedIndex = 0;
        comboBoxGradient.SelectedIndex = 0;
    }

    private void FormDiffractionSimulatorMultislice_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        if (this.Visible && sender == this)
            FormDiffractionSimulator.radioButtonIntensityDynamical.Checked = true;
    }
    #endregion

    #region 実行/停止ボタン
    private void buttonExecute_Click(object sender, EventArgs e)
    {
        if (Crystal.Bethe.IsCBED_Busy) return;

        buttonStop.Visible = true;
        sw2.Reset();
        sw1.Restart();
        FormDiffractionSimulator.SkipDrawing = true;
        Crystal.Bethe.CBED_Completed += Bethe_CbedCompleted;
        Crystal.Bethe.CBED_ProgressChanged += Bethe_CbedProgressChanged;

        //ローテーション配列を作る //一辺が2.の正方形の中に一辺1/Nのピクセルを詰め込み、中心ピクセルが、円の中心とちょうど一致するような問題を考える
        var directions = new List<Vector3DBase>();
        //var side = 2.0 / Division;
        //var max = (int)(1 / side) + 1;
        //var max = Division % 2 == 0 ? Division / 2 : (Division + 1) / 2;
        //for (int h = -max; h <= max; h++)
        //    for (int w = -max; w <= max; w++)
        //            rotations.Add(Matrix3D.Rot(new Vector3DBase(h, w, 0), Math.Sqrt(w * side * w * side + h * side * h * side) * AlphaMax));


        var radius = Division / 2.0;
        for (int h = 0; h < Division; h++)
            for (int w = 0; w < Division; w++)
            {
                //double x = w - radius + 0.5, y = -(h - radius + 0.5);
                //directions.Add(Matrix3D.Rot(new Vector3DBase(y, -x, 0), Math.Atan(Math.Sqrt(x * x + y * y) / radius * Math.Tan(AlphaMax)))*new Vector3DBase(0,0,-1));

                //2022/10/04 以下に変更
                var x = (w - radius + 0.5) / (radius - 0.5) * Math.Sin(AlphaMax);
                var y = -(h - radius + 0.5) / (radius - 0.5) * Math.Sin(AlphaMax);//結晶の座標系は、X軸が右、Y軸が上、Z軸が手前なのでYを反転
                directions.Add(new Vector3DBase(x, y, -Math.Sqrt(1 - x * x - y * y)));
            }

        Directions = [.. directions];


        BetheMethod.Solver solver;
        if (comboBoxSolver.Text.Contains("Eigenproblem"))
            solver = comboBoxSolver.Text.Contains("MKL") ? BetheMethod.Solver.Eigen_MKL : BetheMethod.Solver.Eigen_Eigen;
        else
            solver = comboBoxSolver.Text.Contains("MKL") ? BetheMethod.Solver.MtxExp_MKL : BetheMethod.Solver.MtxExp_Eigen;

        Crystal.Bethe.RunCBED(MaxNumOfBloch, Voltage, Crystal.RotationMatrix, ThicknessArray, Directions, LACBED, solver, numericBoxThread.ValueInteger);
    }
    private void buttonStop_Click(object sender, EventArgs e)
    {
        Crystal.Bethe.CancelCBED();
        buttonExecute.Text = "Execute";
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
            toolStripStatusLabel1.Text = $"Ellapsed time : {totalsec:f2} s.,  ";
            toolStripStatusLabel1.Text += $"{current / 10.0:f1} % completed,  wait for more {sec * (1000 - current) / current:f2} s.";
        }
        else
        {
            var sec = sw1.ElapsedMilliseconds / 1000.0;
            var progress = (int)(100.0 * current / DivisionNumber);
            if (progress <= 100)
                toolStripProgressBar.Value = (int)(100.0 * current / DivisionNumber);
            toolStripStatusLabel2.Text = message;
            toolStripStatusLabel1.Text = $"Ellapsed time : {sec:f2} s.,  time/pixel: ";
            toolStripStatusLabel1.Text += sec / current > 0.9 ? $"{sec / current:f2} s.,  " : $"{sec / current * 1000:f2} ms., ";
            toolStripStatusLabel1.Text += $"{100.0 * current / DivisionNumber:f1} % completed,  wait for {sec * (DivisionNumber - current) / current:f2} s.";
        }
        Application.DoEvents();
        skipProgressChangedEvent = false;
    }

    private void Bethe_CbedCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        buttonStop.Visible = false;
        FormDiffractionSimulator.SkipDrawing = false;
        Crystal.Bethe.CBED_Completed -= Bethe_CbedCompleted;
        Crystal.Bethe.CBED_ProgressChanged -= Bethe_CbedProgressChanged;
        sw2.Stop();
        var sec1 = sw1.ElapsedMilliseconds / 1000.0;
        var sec2 = sw2.ElapsedMilliseconds / 1000.0;

        if (!e.Cancelled)
        {
            toolStripStatusLabel2.Text = "100% completed!  ";
            toolStripStatusLabel1.Text = $"Total time: {sec1 + sec2:f2} s.   ";
            toolStripStatusLabel1.Text += $"Bloch problem: {sec1:f2} s. (";
            toolStripStatusLabel1.Text += sec1 / DivisionNumber > 1 ? $"{sec1 / DivisionNumber:f2} s " : $"{sec1 / DivisionNumber * 1000:f2} ms ";
            toolStripStatusLabel1.Text += $"/pixes).   Compiling disks: {sec2:f2} s.";

            groupBoxOutput.Enabled = true;
            generateImage();

            FormDiffractionSimulator.FormDiffractionBeamTable.SetTable(FormDiffractionSimulator.waveLengthControl.Energy);
        }
        else
        {
            toolStripStatusLabel1.Text = $"Time ellapsed: {sec1 + sec2:f2} sec.,  Manually interupted.";
            groupBoxOutput.Enabled = false;
        }
        toolStripProgressBar.Value = toolStripProgressBar.Maximum;
        FormDiffractionSimulator.Draw();
        Application.DoEvents();
    }

    #endregion

    #region 生画像や表示画像の構築
    private void setImagePixelSize()
    {
        //以下の二つのパラメータはγ値の調整のところで使う。（あまり意味のないパラメータなので、廃止したい）
        AngleResolution = Math.Acos(Vector3DBase.AngleBetVectors(Directions[Directions.Length / 2], Directions[Directions.Length / 2 + 1]) / 2);
        ImagePixelSize = FormDiffractionSimulator.CameraLength2 * Math.Tan(AngleResolution);
    }

    private void generateImage(bool resetDisks = true)
    {
        if (skipEvent || FormDiffractionSimulator == null || Crystal == null || Crystal.Bethe.Disks == null)
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
                Disks[i].PBitmap.MinValue = Disks[i].PBitmap.SrcValuesGray.Max() * maxOverall * minRatio;
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
        Application.DoEvents();
    }

    private void setDisks()
    {
        if (Crystal.Bethe.Disks == null || trackBarOutputThickness.Value >= Crystal.Bethe.Disks.Length)
        {
            Disks = null;
            return;
        }
        var disks = Crystal.Bethe.Disks[trackBarOutputThickness.Value];
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

                var realWidth = Math.Tan(AlphaMax) * FormDiffractionSimulator.CameraLength2 * 2;

                Disks[i] = (disks[i].H, disks[i].K, disks[i].L, center.ToPointD, new SizeD(realWidth, realWidth), new Size(pixWidth, pixWidth), pbmp, null);
            }
        }
        );
    }

    private void TrackBarOutputThickness_Scroll(object sender, EventArgs e)
    {
        if (Crystal.Bethe.Disks == null || trackBarOutputThickness.Value >= Crystal.Bethe.Disks.Length)
            return;
        if (trackBarOutputThickness.Value < 0)
            return;
        textBoxThickness.Text = ThicknessArray[trackBarOutputThickness.Value].ToString();
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

    private void NumericBoxDivision_ValueChanged(object sender, EventArgs e) =>
        labelDivisionNumber.Text = $"{DivisionNumber:#,0}";

    private void NumericBoxWholeThicknessStart_ValueChanged(object sender, EventArgs e)
    {
        trackBarOutputThickness.Maximum = ThicknessArray.Length - 1;
        trackBarOutputThickness.Value = 0;
    }

    private void ComboBoxSolver_SelectedIndexChanged(object sender, EventArgs e)
    {
        numericBoxThread.Enabled = comboBoxSolver.SelectedIndex != 0;
        numericBoxThread.Value = comboBoxSolver.Text.Contains("MKL") ? Math.Max(1, Environment.ProcessorCount / 4) : numericBoxThread.Value = Environment.ProcessorCount;
    }
    #endregion

    private void FormDiffractionSimulatorCBED_VisibleChanged(object sender, EventArgs e)
    {
        FormDiffractionSimulator.saveCBEDPatternToolStripMenuItem.Visible = Visible;
        FormDiffractionSimulator.copyCBEDPatternToolStripMenuItem.Visible = Visible;
    }
}
