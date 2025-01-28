#region
using Microsoft.Scripting.Utils;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Math;
#endregion

namespace ReciPro;
public partial class FormImageSimulator : Form
{
    #region プロパティ

    public bool PresetVisible { get => checkBoxPreset.Checked; set => checkBoxPreset.Checked = value; }

    public bool CTFVisible { get => checkBoxCTF.Checked; set => checkBoxCTF.Checked = value; }

    public ImageSimulatorSetting Setting { get => new("", this); set => value.Apply(this); }
    public bool Native => toolStripComboBoxCaclulationLibrary.SelectedIndex == 0;
 
    public ImageModes ImageMode
    {
        get => radioButtonHRTEM.Checked ? ImageModes.HRTEM : radioButtonProjectedPotential.Checked ? ImageModes.POTENTIAL : ImageModes.STEM;
        set
        {
            if (value == ImageModes.HRTEM)
                radioButtonHRTEM.Checked = true;
            else if (value == ImageModes.POTENTIAL)
                radioButtonProjectedPotential.Checked = true;
            else
                radioButtonSTEM.Checked = true;
        }
    }

    /// <summary>
    /// Bloch波の数
    /// </summary>
    public int BlochNum { get => numericBoxNumOfBlochWave.ValueInteger; set => numericBoxNumOfBlochWave.Value = value; }

    /// <summary>
    /// 試料の厚み (nm) (シリアルモードではないとき)
    /// </summary>
    public double Thickness { get => numericBoxThickness.Value; set => numericBoxThickness.Value = value; }

    #region 電子顕微鏡の共通パラメータ

    /// <summary>
    /// 電子の加速電圧 (kV)
    /// </summary>
    public double AccVol { get => numericBoxAccVol.Value; set => numericBoxAccVol.Value = value; }
    /// <summary>
    /// 電子の波長 (nm)
    /// </summary>
    public double Lambda => UniversalConstants.Convert.EnergyToElectronWaveLength(AccVol);

    /// <summary>
    /// デフォーカス値 (nm) (シリアルモードではないとき) 
    /// </summary>
    public double Defocus { get => numericBoxDefocus.Value; set => numericBoxDefocus.Value = value; }

    /// <summary>
    /// 球面収差 Cs (nm) numericBoxCsで表示されているのはmm単位なので、1E6/1E-6 倍変換して get/set
    /// </summary>
    public double Cs { get => numericBoxCs.Value * 1E6; set => numericBoxCs.Value = value * 1E-6; }

    /// <summary>
    /// 色収差 Cc (nm) numericBoxCcで表示されているのはmm単位なので、1E6/1E-6 倍変換して get/set
    /// </summary>
    public double Cc { get => numericBoxCc.Value * 1E6; set => numericBoxCc.Value = value * 1E-6; }

    /// <summary>
    /// 電子の加速電圧の揺らぎ (kV) numericBoxDeltaVで表示されているのはFWHMだが、2 * Sqrt(2 * Log(2)) で割って1000倍して、eV単位の標準偏差に変換する
    /// </summary>
    public double DeltaVol { get => numericBoxDeltaV.Value / 1000 / 2 / Sqrt(2 * Log(2)); set => numericBoxDeltaV.Value = value * 1000 * 2 * Sqrt(2 * Log(2)); }

    /// <summary>
    /// Δ
    /// </summary>
    public double Delta => Cc * DeltaVol / AccVol;

    /// <summary>
    /// Scherzer focus (nm) getのみ
    /// </summary>
    public double Scherzer => Cs > 0 ? -Sqrt(4.0 / 3.0 * Cs * Lambda) : Sqrt(4.0 / 3.0 * -Cs * Lambda);

    #endregion

    private BetheMethod.Beam[] Beams { get; set; }

    private BetheMethod.Beam[] BeamsInside { get; set; }

    #region 計算する画像サイズ、解像度に関するプロパティ
    /// <summary>
    /// イメージの解像度 (nm/pix)
    /// </summary>
    public double ImageResolution { get => numericBoxResolution.Value / 1000.0; set => numericBoxResolution.Value = value * 1000.0; }

    /// <summary>
    /// イメージサイズ 
    /// </summary>
    public Size ImageSize { get => new(numericBoxWidth.ValueInteger, numericBoxHeight.ValueInteger); set { numericBoxWidth.Value = value.Width; numericBoxHeight.Value = value.Height; } }
    #endregion

    # region シリアルモードのプロパティ
    public double[] ThicknessArray
    {
        get
        {
            if (radioButtonSingleMode.Checked || !checkBoxSerialThickness.Checked)
                return [numericBoxThickness.Value];
            try
            {
                return textBoxThicknessList.Text.Split(["\r\n"], StringSplitOptions.RemoveEmptyEntries).Select(str => Convert.ToDouble(str)).ToArray();
            }
            catch
            {
                MessageBox.Show("Values in Thickness list are invalid.");
                return null;
            }
        }
        set
        {
            if (value != null && value.Length > 0)
                textBoxThicknessList.Text = string.Join("\r\n", value);
        }
    }
    public double[] DefocusArray
    {
        get
        {
            if (radioButtonSingleMode.Checked || !checkBoxSerialDefocus.Checked)
                return [numericBoxDefocus.Value];
            try
            {
                return textBoxDefocusList.Text.Split(["\r\n"], StringSplitOptions.RemoveEmptyEntries).Select(str => Convert.ToDouble(str)).ToArray();
            }
            catch
            {
                MessageBox.Show("Values in Defocus list are invalid.");
                return null;
            }
        }
        set
        {
            if (value != null && value.Length > 0)
                textBoxDefocusList.Text = String.Join("\r\n", value);
        }
    }
    #endregion 

    #region 画像関連
    public bool UnitCellVisible { get => checkBoxShowUnitcell.Checked; set => checkBoxShowUnitcell.Checked = value; }
    public bool LabelVisible { get => checkBoxShowLabel.Checked; set => checkBoxShowLabel.Checked = value; }
    public int LabelSize { get => numericBoxLabelFontSize.ValueInteger; set => numericBoxLabelFontSize.Value = value; }
    public bool ScaleBarVisible { get => checkBoxShowScale.Checked; set => checkBoxShowScale.Checked = value; }
    public double ScaleBarLength { get => numericBoxScaleLength.Value; set => numericBoxScaleLength.Value = value; }
    
    public bool OverprintSymbols { get => toolStripMenuItemOverprintSymbols.Checked; set => toolStripMenuItemOverprintSymbols.Checked = value; }
    public bool SaveIndividually { get => toolStripMenuItemSaveIndividually.Checked; set => toolStripMenuItemSaveIndividually.Checked = value; }

    public bool GaussianBlurEnabled { get => checkBoxGaussianBlur.Checked; set => checkBoxGaussianBlur.Checked = value; }
    public double GaussianBlurFWHM { get => numericBoxGaussianBlurRadius.Value;set=>numericBoxGaussianBlurRadius.Value = value; }
    #endregion

    #region HRTEM固有プロパティ
    public HRTEM_Modes HRTEM_Mode
    {
        get => radioButtonModeQuasiCoherent.Checked ? HRTEM_Modes.Quasi : HRTEM_Modes.TCC;
        set
        {
            if (value == HRTEM_Modes.Quasi)
                radioButtonModeQuasiCoherent.Checked = true;
            else
                radioButtonModeTransmissionCrossCoefficient.Checked = true;
        }
    }
    /// <summary>
    /// 対物絞りのサイズ (rad)
    /// </summary>
    public double HRTEM_ObjAperRadius
    {
        get => checkBoxOpenAperture.Checked ? double.PositiveInfinity : numericBoxObjAperRadius.Value / 1000;
        set
        {
            if (double.IsPositiveInfinity(value))
                checkBoxOpenAperture.Checked = true;
            else
            {
                checkBoxOpenAperture.Checked = true;
                numericBoxObjAperRadius.Value = value * 1000;
            }
        }
    }

    /// <summary>
    /// 対物絞りの中心位置X (rad)
    /// </summary>
    public double HRTEM_ObjAperX { get => numericBoxHRTEM_ObjAperX.Value / 1000; set => numericBoxHRTEM_ObjAperX.Value = value * 1000; }
    /// <summary>
    /// 対物絞りの中心位置Y (rad)
    /// </summary>
    public double HRTEM_ObjAperY { get => numericBoxHRTEM_ObjAperY.Value / 1000; set => numericBoxHRTEM_ObjAperY.Value = value * 1000; }

    /// <summary>
    /// β (illumination semiangle) (rad) 
    /// </summary>
    public double HRTEM_Beta { get => numericBoxHRTEM_BetaAgnle.Value / 1000; set => numericBoxHRTEM_BetaAgnle.Value = value * 1000; }

    #endregion

    #region STEMモード固有

    /// <summary>
    /// STEMモードの時のみ有効.実効的光源サイズ (nm単位) 
    /// </summary>
    public double STEM_SourceSizeFWHM { get => numericBoxSTEM_EffectiveSourceSize.Value / 1000; set => numericBoxSTEM_EffectiveSourceSize.Value = value * 1000; }

    /// <summary>
    /// STEMモードの時のみ有効. 実効光源サイズ (nm) (STEM計算に必要) 2 * Sqrt(2 * Log(2)) で割って、標準偏差に変換する
    /// </summary>
    public double STEM_SourceSizeSigma { get => numericBoxSTEM_EffectiveSourceSize.Value / 1000 / 2 / Sqrt(2 * Log(2)); set => numericBoxSTEM_EffectiveSourceSize.Value = value * 1000 * 2 * Sqrt(2 * Log(2)); }

    /// <summary>
    /// STEM検出器の内径角度 (rad)
    /// </summary>
    public double STEM_DetectorInnerAngle { get => numericBoxSTEM_DetectorInnerAngle.Value / 1000; set => numericBoxSTEM_DetectorInnerAngle.Value = value * 1000; }

    /// <summary>
    /// STEM検出器の外径角度 (rad)
    /// </summary>
    public double STEM_DetectorOuterAngle { get => numericBoxSTEM_DetectorOuterAngle.Value / 1000; set => numericBoxSTEM_DetectorOuterAngle.Value = value * 1000; }

    /// <summary>
    /// STEM時の収束角(rad)
    /// </summary>
    public double STEM_ConvergenceAngle { get => numericBoxSTEM_ConvergenceAngle.Value / 1000; set => numericBoxSTEM_ConvergenceAngle.Value = value * 1000; }

    /// <summary>
    /// STEMモードの時のみ有効. 収束ビームを分解する角度. Rad単位 (表示上は mrad なので1000倍に変換される)
    /// </summary>
    public double STEM_AngularResolution { get => numericBoxSTEM_AngleResolution.Value / 1000; set => numericBoxSTEM_AngleResolution.Value = value * 1000; }

    /// <summary>
    /// STEMモードの時のみ有効. TDS計算の際の、サンプルのスライス厚み.　(nm単位)
    /// </summary>
    public double STEM_SliceThickness { get => numericBoxSTEM_SliceThicknessForInelastic.Value; set => numericBoxSTEM_SliceThicknessForInelastic.Value = value; }


    #endregion

    #endregion プロパティ

    #region フィールド、enum

    public FormMain FormMain;
    public FormDiffractionSpotInfo FormDiffractionSpotInfo;

    public FormPresets FormPresets;
    public FormCTF FormCTF;

    readonly Stopwatch sw1 = new(), sw2 = new(), sw3 = new(), sw4 = new();
    //private static readonly double Pi2 = PI * PI;

    private ScalablePictureBox[,] pictureBoxes = new ScalablePictureBox[0, 0];

    private PseudoBitmap scaleImage;
    public enum ImageModes { HRTEM, POTENTIAL, STEM }

    public enum HRTEM_Modes { Quasi, TCC }

    #endregion フィールド

    #region 起動、終了、フォームイベントの関連
    public FormImageSimulator()
    {
        InitializeComponent();

        FormDiffractionSpotInfo = new FormDiffractionSpotInfo { Visible = false, FormImageSimulator = this };

        FormPresets = new FormPresets() { Visible = false, Owner = this, TopMost = true, FormImageSimulator = this };

        FormCTF = new FormCTF() { Visible = false, Owner = this, TopMost = true, FormImageSimulator = this };

    }

    private void FormImageSimulator_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        Visible = false;
    }

    private void FormImageSimulator_Load(object sender, EventArgs e)
    {
        toolStripComboBoxCaclulationLibrary.SelectedIndex = 0;

        var width = pictureBoxPhaseScale.ClientRectangle.Width;
        var height = pictureBoxPhaseScale.ClientRectangle.Height;
        var temp = Enumerable.Range(0, width * height).ToList().Select(n => (double)(n % width) / width).ToArray();
        scaleImage = new PseudoBitmap(temp, width) { MaxValue = 1, MinValue = 0 };
        scaleImage.SetScaleRotation();
        pictureBoxPhaseScale.Image = scaleImage.GetImage();

        width = pictureBoxScaleOfIntensity.ClientRectangle.Width;
        height = pictureBoxScaleOfIntensity.ClientRectangle.Height;
        temp = Enumerable.Range(0, width * height).ToList().Select(n => (double)(n % width) / width).ToArray();
        scaleImage = new PseudoBitmap(temp, width) { MaxValue = 1, MinValue = 0 };
        scaleImage.SetScaleGray();
        pictureBoxScaleOfIntensity.Image = scaleImage.GetImage();

        comboBoxScaleColorScale.SelectedIndex = 0;

        NumericBoxAccVol_ValueChanged(sender, e);
    }

    /// <summary>
    /// このフォームのVisibleが変更されたとき。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FormImageSimulator_VisibleChanged(object sender, EventArgs e)
    {
        if (this.Visible)
        {
            CalculateInsideSpotInfo();
            if (FormCTF.Visible)
                FormCTF.Renew();
            FormMain.toolStripButtonImageSimulator.Checked = true;
        }
        else
        {
            FormDiffractionSpotInfo.Visible = false;
            FormMain.toolStripButtonImageSimulator.Checked = false;
        }
    }
    #endregion 起動、終了関連

    #region PseudoBitmapに格納する情報
    public class ImageInfo(int width, int height, double resolution, Matrix3D mat, string text, bool lockIntensity = false)
    {
        public int Width = width, Height = height;
        public double Resolution = resolution;
        public PointD A = new(mat.E11, mat.E21), B = new(mat.E12, mat.E22), C = new(mat.E13, mat.E23);
        public Matrix3D Mat = mat;
        public string Text = text;
        public bool LockIntensity = lockIntensity;
    }
    #endregion PseudoBitmapに格納する情報

    #region Simulateボタン
    /// <summary>
    /// Simulateボタンが押されたとき
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void ButtonSimulate_Click(object sender, EventArgs e)
    {
        toolStripStatusLabel1.Text = "";
        toolStripProgressBar.Value = 0;

        if (ImageMode == ImageModes.HRTEM)
            SimulateHRTEM();
        else if (ImageMode == ImageModes.POTENTIAL)
            simulatePotential();
        else if (ImageMode == ImageModes.STEM)
            simulateSTEM();
    }
    #endregion

    #region STEMシミュレーション

    int stemDirectionTotal = 0;
    private void simulateSTEM(bool realtimeMode = false)
    {
        sw1.Reset(); sw2.Reset(); sw3.Reset(); sw4.Reset();
        sw1.Restart();
        if (ThicknessArray == null || DefocusArray == null) return;

        //ローテーション配列を作る //一辺が2.の正方形の中に一辺1/Nのピクセルを詰め込み、中心ピクセルが、円の中心とちょうど一致するような問題を考える
        var directions = new List<Vector3DBase>();

        // 収束角を1.05倍にしておく
        var division = (int)Math.Ceiling(numericBoxSTEM_ConvergenceAngle.Value * 2 * 1.05 / numericBoxSTEM_AngleResolution.Value);
        var sin = Sin(numericBoxSTEM_ConvergenceAngle.Value * 1.05 / 1000);

        var radius = division / 2.0;
        for (int h = 0; h < division; h++)
            for (int w = 0; w < division; w++)
            {
                var x = (w - radius + 0.5) / (radius - 0.5) * sin;
                var y = -(h - radius + 0.5) / (radius - 0.5) * sin;//結晶の座標系は、X軸が右、Y軸が上、Z軸が手前なのでYを反転

                directions.Add(new Vector3DBase(x, y, -Sqrt(1 - x * x - y * y)));
            }

        bool inside(int i) => (i % division - radius + 0.5) * (i % division - radius + 0.5) + (i / division - radius + 0.5) * (i / division - radius + 0.5) <= radius * radius;

        stemDirectionTotal = Enumerable.Range(0, division * division).Count(i => inside(i));

        toolStripProgressBar.Maximum = stemDirectionTotal;
        FormMain.Crystal.Bethe.StemProgressChanged += stemProgressChanged;
        FormMain.Crystal.Bethe.StemCompleted += StemCompleted;

        FormMain.Crystal.Bethe.RunSTEM(
            BlochNum,
            AccVol,
            Cs,
            Delta,
            STEM_SliceThickness,
            ImageSize,
            ImageResolution,
            STEM_SourceSizeFWHM,
            FormMain.Crystal.RotationMatrix,
            ThicknessArray,
            DefocusArray,
            [.. directions],
            STEM_ConvergenceAngle,
            STEM_DetectorInnerAngle,
            STEM_DetectorOuterAngle
            );

        this.buttonSimulate.Visible = false;
        this.buttonStop.Visible = true;
        this.splitContainer1.Enabled = false;

    }

    private void buttonStop_Click(object sender, EventArgs e)
    {
        FormMain.Crystal.Bethe.CancelSTEM();
        this.buttonSimulate.Visible = true;
        this.buttonStop.Visible = false;
        this.splitContainer1.Enabled = true;
    }

    #region BackgroundWorkerからのProgressChanged
    private bool skipProgressChangedEvent = false;
    private void stemProgressChanged(object sender, ProgressChangedEventArgs e)
    {

        if (skipProgressChangedEvent) return;
        skipProgressChangedEvent = true;

        double current = e.ProgressPercentage;
        long s1 = sw1.ElapsedMilliseconds, s2 = sw2.ElapsedMilliseconds, s3 = sw3.ElapsedMilliseconds, s4 = sw4.ElapsedMilliseconds;

        var message = (string)e.UserState;
        if (message.StartsWith("Calculating I_inelastic(Q)", StringComparison.Ordinal))
        {
            if (sw1.IsRunning) sw1.Stop();
            if (sw2.IsRunning) sw2.Stop();
            if (sw3.IsRunning) sw3.Stop();
            if (!sw4.IsRunning) sw4.Restart();
            var sec = s4 / 1000.0;
            var totalsec = sec + (s1 + s2 + s3) / 1000.0;
            toolStripProgressBar.Value = (int)((current / 1E6 * 0.8 + 0.2) * toolStripProgressBar.Maximum);
            toolStripStatusLabel1.Text = $"Ellapsed time : {totalsec:f1} s.  Stage 4: Calculating I_inelastic(Q).  ";
            toolStripStatusLabel2.Text = $"{current / 1E4:f1} % completed,  wait for more {sec * (1E6 - current) / current:f1} s.";
        }
        else if (message.StartsWith("Calculating U", StringComparison.Ordinal))
        {
            if (sw1.IsRunning) sw1.Stop();
            if (sw2.IsRunning) sw2.Stop();
            if (!sw3.IsRunning) sw3.Restart();
            var sec = s3 / 1000.0;
            var totalsec = sec + (s1 + s2) / 1000.0;
            toolStripProgressBar.Value = (int)((current / 1E6 * 0.01 + 0.19) * toolStripProgressBar.Maximum);
            toolStripStatusLabel1.Text = $"Ellapsed time : {totalsec:f1} s.  Stage 3: Calculating U' matrix.  ";
            toolStripStatusLabel2.Text = $"{current / 1E4:f1} % completed,  wait for more {sec * (1E6 - current) / current:f1} s.";
        }
        else if (message.StartsWith("Calculating I_elastic(Q)", StringComparison.Ordinal))
        {
            if (sw1.IsRunning) sw1.Stop();
            if (!sw2.IsRunning) sw2.Restart();
            var sec = s2 / 1000.0;
            var totalsec = sec + s1 / 1000.0;
            toolStripProgressBar.Value = (int)((current / 1E6 * 0.01 + 0.18) * toolStripProgressBar.Maximum);
            toolStripStatusLabel1.Text = $"Ellapsed time : {totalsec:f1} s.  Stage 2: Calculating I_elastic(Q).  ";
            toolStripStatusLabel2.Text = $"{current / 1E4:f1} % completed,  wait for more {sec * (1E6 - current) / current:f1} s.";
        }
        else
        {
            var sec = s1 / 1000.0;
            toolStripProgressBar.Value = (int)((current / 1E6 * 0.18 + 0.0) * toolStripProgressBar.Maximum);
            toolStripStatusLabel1.Text = $"Ellapsed time : {sec:f1} s.  Stage 1: Calculating Tg for " + stemDirectionTotal.ToString() + " directions (" + message + ").";
            toolStripStatusLabel2.Text = $"{current / 1E4:f1} % completed,  wait for more {sec * (1E6 - current) / current:f1} s.";
        }
        Application.DoEvents();
        skipProgressChangedEvent = false;
    }
    #endregion

    #region BackgroundWorkerからのstemCompleted
    private void StemCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        FormMain.Crystal.Bethe.StemCompleted -= StemCompleted;
        FormMain.Crystal.Bethe.StemProgressChanged -= stemProgressChanged;
        long s1 = sw1.ElapsedMilliseconds, s2 = sw2.ElapsedMilliseconds, s3 = sw3.ElapsedMilliseconds, s4 = sw4.ElapsedMilliseconds;

        if (!e.Cancelled)
        {
            //SendImage(ThicknessArray.Length, DefocusArray.Length, FormMain.Crystal.Bethe.STEM_Image, ImageSize.Width, ImageSize.Height);
            GeneratePseudBitmap();
          

            toolStripProgressBar.Value = toolStripProgressBar.Maximum;
            toolStripStatusLabel1.Text = $"Completed! Total ellapsed time: {(s1 + s2 + s3 + s4) / 1000.0:f1} sec.";
            toolStripStatusLabel1.Text += $"  Stage 1: {s1 / 1000.0:f1} sec.  Stage 2: {s2 / 1000.0:f1} sec.  Stage 3: {s3 / 1000.0:f1} sec.  Stage 4: {s4 / 1000.0:f1} sec.";

        }
        else
        {
            toolStripStatusLabel1.Text = $"Interupted! Total ellapsed time: {(s1 + s2 + s3) / 1000.0:f1} sec.";
        }
        toolStripStatusLabel2.Text = "";
        this.buttonSimulate.Visible = true;
        this.buttonStop.Visible = false;
        this.splitContainer1.Enabled = true;
        sw1.Stop(); sw1.Reset(); sw2.Stop(); sw2.Reset(); sw3.Reset(); sw3.Reset();
        Application.DoEvents();
    }

    #endregion

    #endregion;

    #region HREMシミュレーション
    public void SimulateHRTEM(bool realtimeMode = false)
    {
        sw1.Restart();

        if (ThicknessArray == null || DefocusArray == null) return;

        Beams = FormMain.Crystal.Bethe.GetDifractedBeamAmpriltudes(BlochNum, AccVol, FormMain.Crystal.RotationMatrix, ThicknessArray[0]);

        //LTF(レンズ伝達関数)を計算 && apertureの外にあるbeamを除外
        BeamsInside = BetheMethod.ExtractInsideBeams(Beams, AccVol, HRTEM_ObjAperRadius, HRTEM_ObjAperX, HRTEM_ObjAperY);
        if (BeamsInside.Length < 2)//絞りに入るスポットが2未満の時は、警告を出してリターン
        {
            if (!realtimeMode)
                MessageBox.Show("Obj. Aper. size is too small. Try again after increase the value!");
            return;
        }

        FormMain.Crystal.Bethe.GetHRTEMImage(BlochNum, AccVol, FormMain.Crystal.RotationMatrix, (HRTEM_ObjAperRadius, HRTEM_ObjAperX, HRTEM_ObjAperY),
            ImageSize, ImageResolution, Cs, HRTEM_Beta, Delta, ThicknessArray, DefocusArray, HRTEM_Mode == HRTEM_Modes.Quasi, Native);

        var temp = sw1.ElapsedMilliseconds;
        toolStripStatusLabel1.Text += $"Generation of HRTEM images: {sw1.ElapsedMilliseconds} msec,   ";

        GeneratePseudBitmap();

        toolStripStatusLabel1.Text += $"Drawing: {sw1.ElapsedMilliseconds - temp} msec.";
    }
    #endregion

    #region ポテンシャルシミュレーション
    private void simulatePotential(bool realtimeMode = false)
    {
        sw1.Restart();

        if (!checkBoxPotentialUg.Checked && !checkBoxPotentialUgPrime.Checked) return;

        Beams = FormMain.Crystal.Bethe.GetDifractedBeamAmpriltudes(BlochNum, AccVol, FormMain.Crystal.RotationMatrix, ThicknessArray[0]);
        var images = FormMain.Crystal.Bethe.GetPotentialImage(Beams, ImageSize, ImageResolution, radioButtonPotentialModeMagAndPhase.Checked);

        //画像が上下左右反転 (180度回転) しているみたいなので、処理 20230304
        //なぜかまた上下左右反転 (180度回転) しているみたいなので、削除 20241101
        //for (int i = 0; i < images.Length; i++)
        //    images[i] = [.. images[i].Reverse()];


        var temp = sw1.ElapsedMilliseconds;
        toolStripStatusLabel1.Text = $"Generation of Potential images: {temp} msec,   ";

        //最大値、最小値の設定
        double max = radioButtonPotentialModeMagAndPhase.Checked ? Max(images[0].Max(), images[2].Max()) : Max(Abs(images.Max(d => d.Max())), Abs(images.Min(d => d.Min())));
        double min = radioButtonPotentialModeMagAndPhase.Checked ? 0 : -max;

        //トラックバー設定
        SkipEvent = true;
        trackBarAdvancedMax.Value = trackBarAdvancedMin.Maximum = trackBarAdvancedMax.Maximum = max;
        trackBarAdvancedMin.Value = trackBarAdvancedMin.Minimum = trackBarAdvancedMax.Minimum = min;
        trackBarAdvancedMax.UpDown_Increment = trackBarAdvancedMin.UpDown_Increment = (max - min) / 100.0;
        SkipEvent = false;

        //作成したイメージをPseudoBitmapに変換
        var mat = FormMain.Crystal.RotationMatrix * FormMain.Crystal.MatrixReal;
        int width = ImageSize.Width, height = ImageSize.Height;
        var range = Enumerable.Range(0, 2).ToList();
        var pseudo = range.Select(_ => range.Select(_ => new PseudoBitmap()).ToList()).ToList();

        //振幅と位相モードの時
        if (radioButtonPotentialModeMagAndPhase.Checked)
            foreach (var (i, j, text) in new[] { (0, 0, "Ug magnitude"), (0, 1, "Ug phase"), (1, 0, "U'g magnitude"), (1, 1, "Ug phase") })
            {
                var src = j == 0 ? images[i * 2 + j] : images[i * 2 + j].Select(d => d / Math.PI * 180).ToArray();
                pseudo[i][j] = new PseudoBitmap(src, width)
                {
                    MaxValue = j == 0 ? max : 180,
                    MinValue = j == 0 ? min : -180,
                    Tag = new ImageInfo(width, height, ImageResolution, mat, text, j == 1),
                    Scale = j == 0 ?
                    (comboBoxScaleColorScale.SelectedIndex == 0 ? PseudoBitmap.Scales.GrayLinear : PseudoBitmap.Scales.ColdWarmLinear) :
                    PseudoBitmap.Scales.RotationLinear
                };
            }
        //実数と虚数モードの時
        else
            foreach (var (i, j, text) in new[] { (0, 0, "Ug real"), (0, 1, "Ug imag"), (1, 0, "U'g real"), (1, 1, "U'g imag") })
                pseudo[i][j] = new PseudoBitmap(images[i * 2 + j], width)
                {
                    MaxValue = max,
                    MinValue = min,
                    Tag = new ImageInfo(width, height, ImageResolution, mat, text),
                    Scale = comboBoxScaleColorScale.SelectedIndex == 0 ? PseudoBitmap.Scales.GrayLinear : PseudoBitmap.Scales.ColdWarmLinear
                };

        //チェック状況に応じて、削除
        if (!checkBoxPotentialUg.Checked)
            pseudo.RemoveAt(0);
        else if (!checkBoxPotentialUgPrime.Checked)
            pseudo.RemoveAt(1);

        if ((radioButtonPotentialModeRealAndImag.Checked && radioButtonPotentialShowReal.Checked) ||
            (radioButtonPotentialModeMagAndPhase.Checked && radioButtonPotentialShowMag.Checked))
            pseudo.ForEach(p => p.RemoveAt(1));
        else if ((radioButtonPotentialModeRealAndImag.Checked && radioButtonPotentialShowImag.Checked) ||
            (radioButtonPotentialModeMagAndPhase.Checked && radioButtonPotentialShowPhase.Checked))
            pseudo.ForEach(p => p.RemoveAt(0));

        //resultに格納して、ScalablePictureboxに転送
        var result = new PseudoBitmap[pseudo.Count, pseudo[0].Count];
        for (int r = 0; r < pseudo.Count; r++)
            for (int c = 0; c < pseudo[0].Count; c++)
                result[r, c] = pseudo[r][c];

        SetPseudoBitamap(result);
        toolStripStatusLabel1.Text += $"Drawing: {sw1.ElapsedMilliseconds - temp} msec.";
        TrackBarAdvancedMin_ValueChanged(new object(), 0);
    }
    #endregion

    #region 計算結果をPictureBoxにセット

    /// <summary>
    /// PseudoBitmapを作成
    /// </summary>
    /// <param name="tLen"></param>
    /// <param name="dLen"></param>
    /// <param name="images"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public void GeneratePseudBitmap()
    {
        if (ImageMode == ImageModes.POTENTIAL)
            return;

        var bethe = FormMain.Crystal.Bethe;
        int width, height;
        double resolution;
        double[] thicknesses, defocusses;
        double[][][] images;
        Matrix3D rot;
        if (ImageMode == ImageModes.STEM)
        {
            if (bethe.ResultSTEM.ImageBoth == null)
                return;

            width = bethe.ResultSTEM.Size.Width;
            height = bethe.ResultSTEM.Size.Height;
            thicknesses = bethe.ResultSTEM.Thicknesses;
            defocusses = bethe.ResultSTEM.Defocusses;
            resolution = bethe.ResultSTEM.Resolution;
            rot = bethe.ResultSTEM.rot;

            if (radioButtonSTEM_target_both.Checked)
                images = bethe.ResultSTEM.ImageBoth;
            else if (radioButtonSTEM_target_elas.Checked)
                images = bethe.ResultSTEM.ImageEla;
            else
                images = bethe.ResultSTEM.ImageTDS;

        }
        else if (ImageMode == ImageModes.HRTEM)
        {
            if (FormMain.Crystal.Bethe.ResultHRTEM.Image == null)
                return;

            width = bethe.ResultHRTEM.Size.Width;
            height = bethe.ResultHRTEM.Size.Height;
            thicknesses = bethe.ResultHRTEM.Thicknesses;
            defocusses = bethe.ResultHRTEM.Defocusses;
            resolution = bethe.ResultHRTEM.Resolution;
            rot = bethe.ResultHRTEM.rot;

            images = bethe.ResultHRTEM.Image;
        }
        else
            return;

        int tLen = thicknesses.Length, dLen = defocusses.Length;

        var _images = new double[tLen][][];
        for (int t = 0; t < tLen; t++)
            _images[t] = new double[dLen][];

        //20241101 生成した画像が180度回転していることが発覚したため、ここで修正


        //作成したイメージをPseudoBitmapに変換
        var pseudo = radioButtonHorizontalDefocus.Checked ? new PseudoBitmap[tLen, dLen] : new PseudoBitmap[dLen, tLen];
        var mat = rot * FormMain.Crystal.MatrixReal;

        //全体でノーマライズ
        if (!checkBoxNormarizeIndividually.Checked)
            _images = Normalize(images, checkBoxIntensityMin.Checked, checkBoxIntensityMax.Checked);

        for (int t = 0; t < tLen; t++)
            for (var d = 0; d < dLen; d++)
            {
                //個別にノーマライズ
                if (checkBoxNormarizeIndividually.Checked)
                    _images[t][d] = Normalize(images[t][d], checkBoxIntensityMin.Checked, checkBoxIntensityMax.Checked);

                //PseudoBitmapを生成
                pseudo[radioButtonHorizontalDefocus.Checked ? t : d, radioButtonHorizontalDefocus.Checked ? d : t] = new PseudoBitmap(_images[t][d], width)
                {
                    Tag = new ImageInfo(width, height, resolution, mat, $"t={ThicknessArray[t]:f2}\r\nf={DefocusArray[d]:f2}"),
                    MaxValue = trackBarAdvancedMax.Value,
                    MinValue = trackBarAdvancedMin.Value,
                    Scale = comboBoxScaleColorScale.SelectedIndex == 0 ? PseudoBitmap.Scales.GrayLinear : PseudoBitmap.Scales.ColdWarmLinear
                };
            }

        //1列あるいは1行で、他の要素が多いときは適当に折り返し
        if ((dLen == 1 && tLen > 2) || (tLen == 1 && dLen > 2))
        {
            var newCol = Ceiling(Sqrt(pseudo.Length));
            var newRow = Ceiling(pseudo.Length / newCol);
            var newPseudo = new PseudoBitmap[(int)newRow, (int)newCol];
            var oldPseudo = pseudo.Cast<PseudoBitmap>().ToList();
            for (int r = 0, n = 0; r < newRow; r++)
                for (int c = 0; c < newCol; c++, n++)
                    newPseudo[r, c] = n < pseudo.Length ? oldPseudo[n] : null;
            pseudo = newPseudo;
        }

        SkipEvent = true;
        double max = checkBoxIntensityMax.Checked ? numericBoxIntensityMax.Value : _images.Max();
        double min = checkBoxIntensityMin.Checked ? numericBoxIntensityMin.Value : _images.Min();
        trackBarAdvancedMax.Value = trackBarAdvancedMin.Maximum = trackBarAdvancedMax.Maximum = max;
        trackBarAdvancedMin.Value = trackBarAdvancedMin.Minimum = trackBarAdvancedMax.Minimum = min;
        trackBarAdvancedMax.UpDown_Increment = trackBarAdvancedMin.UpDown_Increment = (trackBarAdvancedMax.Value - trackBarAdvancedMin.Value) / 100.0;
        SkipEvent = false;

        //ScalableBoxに転送
        SetPseudoBitamap(pseudo);

        TrackBarAdvancedMin_ValueChanged(new object(), 0);
    }

    #region normarize関数
    public double[] Normalize(double[] image, bool normalizeMin, bool normalizeMax)
    {
        if (!normalizeMin && !normalizeMax)
            return [.. image];

        double min = image.Min(), max = image.Max();
        double destMin = normalizeMin ? numericBoxIntensityMin.Value : min;
        double destMax = normalizeMax ? numericBoxIntensityMax.Value : max;

        return image.Select(d => (d - min) / (max - min) * (destMax - destMin) + destMin).ToArray();
    }

    public double[][][] Normalize(double[][][] image, bool normalizeMin, bool normalizeMax)
    {
        var _image = new double[image.Length][][];

        double min = image.Min(), max = image.Max();
        double destMin = normalizeMin ? numericBoxIntensityMin.Value : min;
        double destMax = normalizeMax ? numericBoxIntensityMax.Value : max;

        for (int i = 0; i < image.Length; i++)
        {
            _image[i] = new double[image[i].Length][];
            for (int j = 0; j < image[i].Length; j++)
                if (normalizeMin || normalizeMax)
                    _image[i][j] = image[i][j].Select(d => (d - min) / (max - min) * (destMax - destMin) + destMin).ToArray();
                else
                    _image[i][j] = [.. image[i][j]];
        }
        return _image;
    }
    #endregion

    //作成したPseutoBitmapをscalablePictureBoxに転送
    private void SetPseudoBitamap(PseudoBitmap[,] image)
    {
        var row = image.GetLength(0);
        var col = image.GetLength(1);

        if (pictureBoxes.GetLength(0) == row && pictureBoxes.GetLength(1) == col)
        {
            for (int r = 0; r < row; r++)
                for (int c = 0; c < col; c++)
                {
                    pictureBoxes[r, c].SkipEvent = true;
                    pictureBoxes[r, c].PseudoBitmap = image[r, c];
                    pictureBoxes[r, c].SkipEvent = false;
                }
        }
        else
        {
            tableLayoutPanel.SuspendLayout();
            pictureBoxes = new ScalablePictureBox[row, col];
            tableLayoutPanel.Controls.Clear();
            tableLayoutPanel.RowCount = row;
            tableLayoutPanel.ColumnCount = col;
            Enumerable.Range(0, row).ToList().ForEach(r => tableLayoutPanel.RowStyles[r].Height = 1f);
            Enumerable.Range(0, col).ToList().ForEach(c => tableLayoutPanel.ColumnStyles[c].Width = 1f);

            for (int r = 0; r < row; r++)
                for (int c = 0; c < col; c++)
                {
                    pictureBoxes[r, c] = new ScalablePictureBox
                    {
                        SkipEvent = true,
                        Size = new Size(1, 1),
                        MouseScaling = true,
                        MouseTranslation = true,
                        PseudoBitmap = image[r, c],
                        ZoomAndCenter = (0, new PointD(0, 0))
                    };
                    tableLayoutPanel.Controls.Add(pictureBoxes[r, c], c, r);
                    pictureBoxes[r, c].Dock = DockStyle.Fill;
                    pictureBoxes[r, c].SkipEvent = false;

                    pictureBoxes[r, c].DrawingAreaChanged += PictureBox_DrawingAreaChanged;
                    pictureBoxes[r, c].Paint2 += PictureBox_Paint2;
                    pictureBoxes[r, c].MouseMove2 += FormImageSimulator_MouseMove2;
                    pictureBoxes[r, c].MouseDown2 += FormImageSimulator_MouseDown2;
                }
            tableLayoutPanel.ResumeLayout();
        }

        pictureBoxes[0, 0].ZoomAndCenter = (0, new PointD(0, 0));
    }
    #endregion

    #region マウス操作

    private bool FormImageSimulator_MouseMove2(object sender, MouseEventArgs e, PointD pt)
    {
        var pseud = (sender as ScalablePictureBox).PseudoBitmap;
        var info = pseud.Tag as ImageInfo;
        labelMousePositionX.Text = $"X: {(pt.X - info.Width / 2.0) * info.Resolution * 1000:f2} pm";
        labelMousePositionY.Text = $"Y: {(-pt.Y + info.Height / 2.0) * info.Resolution * 1000:f2} pm";
        labelMousePositionValue.Text = $"Value: {pseud.GetPixelRawValue(pt):g6}";
        return false;
    }
    private bool FormImageSimulator_MouseDown2(object sender, MouseEventArgs e, PointD pt)
    {
        if (e.Clicks == 2 && e.Button == MouseButtons.Left)
        {
            int rows = pictureBoxes.GetLength(0), cols = pictureBoxes.GetLength(1);
            if (rows == 1 && cols == 1)
                return false;

            for (int targetR = 0; targetR < rows; targetR++)
                for (int targetC = 0; targetC < cols; targetC++)
                    if ((sender as ScalablePictureBox) == pictureBoxes[targetR, targetC])//まずターゲットを見つける
                    {
                        List<int> rowsList = Enumerable.Range(0, rows).ToList(), colsList = Enumerable.Range(0, cols).ToList();

                        tableLayoutPanel.SuspendLayout();

                        SkipEvent = true;
                        //rowsList.ForEach(row => colsList.ForEach(cos => pictureBoxes[row, cos].SkipEvent = true));

                        if (tableLayoutPanel.RowStyles[targetR].Height == 100f)
                        {
                            rowsList.ForEach(row => tableLayoutPanel.RowStyles[row].Height = 1f);
                            colsList.ForEach(col => tableLayoutPanel.ColumnStyles[col].Width = 1f);
                            pictureBoxes[0, 0].ZoomAndCenter = (0, new PointD(0, 0));
                        }
                        else
                        {
                            rowsList.ForEach(row => tableLayoutPanel.RowStyles[row].Height = targetR == row ? 100f : 0f);
                            colsList.ForEach(col => tableLayoutPanel.ColumnStyles[col].Width = targetC == col ? 100f : 0f);
                        }

                        //rowsList.ForEach(row => colsList.ForEach(col => pictureBoxes[row, col].SkipEvent = false));
                        SkipEvent = false;

                        tableLayoutPanel.ResumeLayout();
                        return false;
                    }
        }
        return false;
    }
    #endregion

    #region 電子顕微鏡の各種光学パラメータや試料パラメータのイベント

    /// <summary>
    /// 電子顕微鏡の各種光学パラメータが変更されたとき。レンズ関数を描画
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NumericBoxTEMproperty_ValueChanged(object sender, EventArgs e) => FormCTF.Renew();

    /// <summary>
    /// 加速電圧が変更されたとき。波長を変更、シェルツァーフォーカス変更、レンズ関数描画、ビームの個数計算
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NumericBoxAccVol_ValueChanged(object sender, EventArgs e)
    {
        textBoxScherzer.Text = Scherzer.ToString("f2");

        numericBoxSTEM_ConvergenceAngle_ValueChanged(sender, e);
        NumericBoxObjAperRadius_ValueChanged(sender, e);

    }
    /// <summary>
    /// 球面収差が変更されたとき。シェルツァーフォーカス変更、レンズ関数描画
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NumericBoxCs_ValueChanged(object sender, EventArgs e)
    {
        textBoxScherzer.Text = Scherzer.ToString("f2");
        FormCTF.Renew();
    }

    /// <summary>
    /// STEMの収束角、検出器範囲が変更されたとき、半径(nm^-1)の換算値を変更
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void numericBoxSTEM_ConvergenceAngle_ValueChanged(object sender, EventArgs e)
    {
        textBoxConvRadius.Text = (Sin(STEM_ConvergenceAngle) / Lambda).ToString("f3");
        textBoxInnerRadius.Text = (Sin(STEM_DetectorInnerAngle) / Lambda).ToString("f3");
        textBoxOuterRadius.Text = (Sin(STEM_DetectorOuterAngle) / Lambda).ToString("f3");
        FormCTF.Renew();
    }


    /// <summary>
    /// デフォーカスが変更されたとき。シリアルモードのデフォーカス開始値変更、レンズ関数描画
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NumericBoxDefocus_ValueChanged(object sender, EventArgs e)
    {
        numericBoxDefocusStart.Value = numericBoxDefocus.Value;
        FormCTF.Renew();
    }
    /// <summary>
    /// 試料厚みが変更されたとき。シリアルモードの試料厚み開始値変更
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NumericBoxThickness_ValueChanged(object sender, EventArgs e) => numericBoxThicknessStart.Value = numericBoxThickness.Value;

    /// <summary>
    /// 対物絞りの半径やシフトが変更されたとき。絞り半径のnm^-1換算値を設定、内側ビームの個数計算
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NumericBoxObjAperRadius_ValueChanged(object sender, EventArgs e)
    {
        FormCTF.Renew();

        numericBoxObjAperRadius.Enabled = numericBoxHRTEM_ObjAperX.Enabled = numericBoxHRTEM_ObjAperY.Enabled = !checkBoxOpenAperture.Checked;

        textBoxObjAperRadius.Text = checkBoxOpenAperture.Checked ? HRTEM_ObjAperRadius.ToString() : (Sin(HRTEM_ObjAperRadius) / Lambda).ToString("f3");

        CalculateInsideSpotInfo();
    }

    /// <summary>
    /// シリアルモードの試料厚み、ステップ、個数が変更されたとき。厚みリストを変更
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NumericBoxThicknessSerial_ValueChanged(object sender, EventArgs e)
    {
        textBoxThicknessList.Text = numericBoxThicknessStart.Value.ToString();
        for (int i = 1; i < numericBoxThicknessNum.ValueInteger; i++)
            textBoxThicknessList.Text += "\r\n" + (numericBoxThicknessStart.Value + numericBoxThicknessStep.Value * i).ToString();
    }
    /// <summary>
    /// シリアルモードのデフォーカス、ステップ、個数が変更されたとき。デフォーカスリストを変更
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NumericBoxDefocusSerial_ValueChanged(object sender, EventArgs e)
    {
        textBoxDefocusList.Text = numericBoxDefocusStart.Value.ToString();
        for (int i = 1; i < numericBoxDefocusNum.ValueInteger; i++)
            textBoxDefocusList.Text += "\r\n" + (numericBoxDefocusStart.Value + numericBoxDefocusStep.Value * i).ToString();
    }
    /// <summary>
    /// ブロッホ波の個数が変更されたとき。FormDiffractionSimulator中のブロッホ波個数を変更、スポット情報更新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NumericBoxNumOfBlochWave_ValueChanged(object sender, EventArgs e)
    {
        if (FormMain.FormDiffractionSimulator.Visible)
            FormMain.FormDiffractionSimulator.numericBoxNumOfBlochWave.Value = numericBoxNumOfBlochWave.Value;
        CalculateInsideSpotInfo();
    }

    /// <summary>
    /// 現在のパラメータに従って、対物絞り内のスポット情報を計算。SpotInfoのテーブルを更新。FormDiffractionSimulatorが表示されていれば更新
    /// </summary>
    public void CalculateInsideSpotInfo()
    {
        var beams = FormMain.Crystal.Bethe.Find_gVectors(FormMain.Crystal.RotationMatrix, new Vector3DBase(0, 0, 1 / Lambda), BlochNum);
        BeamsInside = BetheMethod.ExtractInsideBeams(beams, AccVol, HRTEM_ObjAperRadius, HRTEM_ObjAperX, HRTEM_ObjAperY);
        textBoxNumOfSpots.Text = BeamsInside.Length.ToString();

        if (FormDiffractionSpotInfo.Visible)
        {
            Beams = FormMain.Crystal.Bethe.GetDifractedBeamAmpriltudes(BlochNum, AccVol, FormMain.Crystal.RotationMatrix, ThicknessArray[0]);
            BeamsInside = BetheMethod.ExtractInsideBeams(Beams, AccVol, HRTEM_ObjAperRadius, HRTEM_ObjAperX, HRTEM_ObjAperY);
            FormDiffractionSpotInfo.SetTable(AccVol, BeamsInside);
        }

        if (FormMain.FormDiffractionSimulator.Visible)
            FormMain.FormDiffractionSimulator.Draw();
    }

    #endregion

    #region 他のフォームで結晶回転状態が変更されたとき
    public void RotationChanged()
    {
        if (checkBoxRealTimeSimulation.Checked)
        {
            if (ImageMode == ImageModes.HRTEM)
                SimulateHRTEM(true);
            else if (ImageMode == ImageModes.POTENTIAL)
                simulatePotential(true);
        }

        if (ImageMode == ImageModes.HRTEM)
            CalculateInsideSpotInfo();
    }
    #endregion

    #region スポット情報ボタン
    /// <summary>
    /// スポット情報ボタン
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ButtonDetailsOfSpots_Click(object sender, EventArgs e)
    {
        FormDiffractionSpotInfo.SetTable(AccVol, BeamsInside);
        FormDiffractionSpotInfo.Visible = true;
    }
    #endregion

    #region チェックボックス On/Offやボタン押下イベントに伴うパネル類のEnabled, visible設定

    /// <summary>
    /// 連続画像モード関連のチェックボックス
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CheckBoxSerialDefocus_CheckedChanged(object sender, EventArgs e)
    {
        panelSerial.Enabled = radioButtonSerialMode.Checked;

        panelSerialThickness.Enabled = checkBoxSerialThickness.Checked;
        panelSerialDefocus.Enabled = checkBoxSerialDefocus.Checked;
        flowLayoutPanelHorizontalDirection.Enabled = checkBoxSerialThickness.Checked && checkBoxSerialDefocus.Checked;

        groupBoxSampleProperty.Enabled = !(radioButtonSerialMode.Checked && checkBoxSerialThickness.Checked);
        numericBoxDefocus.Enabled = !(radioButtonSerialMode.Checked && checkBoxSerialDefocus.Checked);
    }

    private void CheckBoxShowLabel_CheckedChanged(object sender, EventArgs e)
    {
        colorControlLabel.Enabled = numericBoxLabelFontSize.Enabled = checkBoxShowLabel.Checked;
        colorControlScale.Enabled = numericBoxScaleLength.Enabled = checkBoxShowScale.Checked;

        foreach (var box in pictureBoxes)
            box.Refresh();
    }

    private void RadioButtonHRTEM_CheckedChanged(object sender, EventArgs e)
    {
        this.SuspendLayout();
        numericBoxDefocus.Enabled = ImageMode != ImageModes.POTENTIAL;

        numericBoxHRTEM_BetaAgnle.Enabled = ImageMode == ImageModes.HRTEM;

        numericBoxCs.Enabled = numericBoxCc.Enabled = numericBoxDeltaV.Enabled =
        groupBoxSampleProperty.Visible = groupBoxNormalization.Visible
               = groupBoxSerialImage.Visible = ImageMode != ImageModes.POTENTIAL;

        checkBoxRealTimeSimulation.Visible = ImageMode != ImageModes.STEM;

        groupBoxPotentialOption.Visible = ImageMode == ImageModes.POTENTIAL;
        groupBoxHREMoption1.Visible = groupBoxHREMoption2.Visible = ImageMode == ImageModes.HRTEM;
        groupBoxSTEMoption1.Visible = groupBoxSTEMoption2.Visible = groupBoxSTEMoption3.Visible = ImageMode == ImageModes.STEM;

        if (ImageMode == ImageModes.POTENTIAL)
            checkBoxCTF.Checked = false;
        checkBoxCTF.Enabled = ImageMode != ImageModes.POTENTIAL;

        this.ResumeLayout(true);

        FormCTF.Renew();
    }

    #endregion

    #region 画像の描画、コピー/保存関連

    private void PictureBox_Paint2(object sender, PaintEventArgs e)
    {
        var box = sender as ScalablePictureBox;
        if (box.PseudoBitmap != null && box.PseudoBitmap.Tag != null && box.PseudoBitmap.Tag is ImageInfo info)
        {
            var conv = new Func<PointD, PointD>(src => box.ConvertToClientPt(src));
            var zoom = box.Zoom;
            drawSymbols(e.Graphics, conv, zoom, info);
        }
    }

    private void drawSymbols(Graphics g, Func<PointD, PointD> conv, double zoom, ImageInfo imageInfo, bool merge = false)
    {
        var reso = imageInfo.Resolution;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

        //ユニットセル
        if (checkBoxShowUnitcell.Checked)
        {
            Pen penA = new(Color.Red, 1), penB = new(Color.Green, 1), penC = new(Color.Blue, 1);
            var zero = new PointD(0, 0);
            var a = new PointD(imageInfo.A.X, -imageInfo.A.Y) / reso * zoom;
            var b = new PointD(imageInfo.B.X, -imageInfo.B.Y) / reso * zoom;
            var c = new PointD(imageInfo.C.X, -imageInfo.C.Y) / reso * zoom;

            var ptOrigin = conv(new PointD(0.5 * imageInfo.Width, 0.5 * imageInfo.Height)) - (a + b + c) / 2;

            foreach (var t in new[] { zero, b, c, b + c })
                g.DrawLine(penA, (ptOrigin + t).ToPointF(), (ptOrigin + t + a).ToPointF());

            foreach (var t in new[] { zero, c, a, c + a })
                g.DrawLine(penB, (ptOrigin + t).ToPointF(), (ptOrigin + t + b).ToPointF());

            foreach (var t in new[] { zero, a, b, a + b })
                g.DrawLine(penC, (ptOrigin + t).ToPointF(), (ptOrigin + t + c).ToPointF());
        }

        //ラベル
        if (checkBoxShowLabel.Checked)
        {
            var font = new Font("Segoe UI Symbol", (float)numericBoxLabelFontSize.Value);
            var sb = new SolidBrush(colorControlLabel.Color);
            g.DrawString(imageInfo.Text, font, sb, merge ? conv(new PointD(4, 8)).ToPointF() : new PointF(4f, 8f));
        }

        //スケールバー

        if (checkBoxShowScale.Checked)
        {
            var pen = new Pen(colorControlScale.Color, 3);
            var pt1 = merge ? conv(new PointD(4, 4)) : new PointD(4f, 4f);
            var pt2 = new PointD(pt1.X + numericBoxScaleLength.Value / reso * zoom, pt1.Y);
            g.DrawLine(pen, (float)pt1.X, (float)pt1.Y, (float)pt2.X, (float)pt2.Y);
        }


    }
    public enum FormatEnum { Meta, PNG, TIFF }
    public enum ActionEnum { Save, Copy }
    public void Save(FormatEnum format, ActionEnum action, string _filename=null)
    {
        if (pictureBoxes != null && pictureBoxes.Length != 0)
        {
            int row = pictureBoxes.GetLength(0), col = pictureBoxes.GetLength(1);
            var pseudo = new PseudoBitmap[row, col];
            for (int r = 0; r < row; r++)
                for (int c = 0; c < col; c++)
                    pseudo[r, c] = pictureBoxes[r, c].PseudoBitmap;
            int width = pseudo[0, 0].Width, height = pseudo[0, 0].Height;

            //イメージを生成するAction. RとCが無効の場合は全画像、有効な場合は1枚画像
            var draw = new Action<Graphics, PseudoBitmap>((g, p) =>
            {
                if (p != null)
                {
                    g.DrawImage(p.GetImage(), new Point(0, 0));
                    if (toolStripMenuItemOverprintSymbols.Checked)
                        drawSymbols(g, new Func<PointD, PointD>(pt => pt), 1, (ImageInfo)p.Tag);
                }
                else
                {
                    for (int r = 0; r < row; r++)
                        for (int c = 0; c < col; c++)
                        {
                            if (pseudo[r, c].Width != 0)
                            {
                                g.DrawImage(pseudo[r, c].GetImage(), new Point(c * width, r * height));
                                if (toolStripMenuItemOverprintSymbols.Checked)
                                    drawSymbols(g, new Func<PointD, PointD>(pt => pt + new PointD(c * width, r * height)),
                                        1, (ImageInfo)pseudo[r, c].Tag, true);
                            }
                        }
                }
            });

            //メタファイルをセーブしたりコピーしたりするときのアクション
            var actionForMetafile = new Action<PseudoBitmap, string>((p, filename) =>
            {
                var grfx = CreateGraphics();
                var ipHdc = grfx.GetHdc();
                var ms = new MemoryStream();
                var mf = new Metafile(ms, ipHdc, EmfType.EmfPlusDual);
                grfx.ReleaseHdc(ipHdc);
                grfx.Dispose();
                var g = Graphics.FromImage(mf);

                draw(g, p);

                g.Dispose();

                if (filename.Length == 0)//finenameが""の時はコピー
                    ClipboardMetafileHelper.PutEnhMetafileOnClipboard(this.Handle, mf);
                else
                    using (var fsm = new FileStream(filename, FileMode.Create, FileAccess.Write))
                        fsm.Write(ms.GetBuffer(), 0, (int)ms.Length);
            });

            //ここから、実際の処理

            //先にファイルダイアログの処理をしてしまう
            var filename = _filename;

            if (_filename == null && action == ActionEnum.Save)
            {
                var dlg = new SaveFileDialog { Filter = format switch { FormatEnum.Meta => "*.emf|*.emf", FormatEnum.PNG => "*.png|*.png", _ => "*.tif|*.tif" } };
                if (dlg.ShowDialog() == DialogResult.OK)
                    filename = dlg.FileName;
                else
                    return;
            }
            //
            if(action== ActionEnum.Save)
            {
                if (!Path.Exists(Path.GetDirectoryName(filename))) 
                    return;
                if (format == FormatEnum.PNG && !filename.ToLower().EndsWith(".png"))
                    filename += ".png";
                else if (format == FormatEnum.TIFF && !filename.ToLower().EndsWith(".tif"))
                    filename += ".tif";
                else if (format == FormatEnum.Meta && !filename.ToLower().EndsWith(".emf"))
                    filename += ".emf";
            }

            //メタファイル形式の時
            if (format == FormatEnum.Meta)
            {
                //個別保存の時
                if (action == ActionEnum.Save && pseudo.Length > 1 && toolStripMenuItemSaveIndividually.Checked)//Save
                {
                    for (int r = 0; r < row; r++)
                        for (int c = 0; c < col; c++)
                            if (pseudo[r, c].Width != 0)
                            {
                                var text = (pseudo[r, c].Tag as ImageInfo).Text.Replace("\r\n", ", ");
                                var fn = filename.Replace(".emf", " (" + text + ").emf");
                                actionForMetafile(pseudo[r, c], fn);
                            }
                }
                else//全体保存 or 全体コピー
                {
                    if (action == ActionEnum.Save)
                        actionForMetafile(null, filename);
                    else
                        actionForMetafile(null, "");//filename を "" にすると、コピー
                }
            }
            //Png形式の時
            else if (format == FormatEnum.PNG)
            {
                //個別保存の時
                if (action == ActionEnum.Save && pseudo.Length > 1 && toolStripMenuItemSaveIndividually.Checked)//Save
                {
                    for (int r = 0; r < row; r++)
                        for (int c = 0; c < col; c++)
                            if (pseudo[r, c].Width != 0)
                            {
                                var bmp = new Bitmap(width, height);
                                draw(Graphics.FromImage(bmp), pseudo[r, c]);
                                var text = (pseudo[r, c].Tag as ImageInfo).Text.Replace("\r\n", ", ");
                                bmp.Save(filename.Replace(".png", " (" + text + ").png"), ImageFormat.Png);
                            }
                }
                else//全体保存 or 全体コピー
                {
                    var bmp = new Bitmap(col * width, row * height);
                    draw(Graphics.FromImage(bmp), null);
                    if (action == ActionEnum.Save)
                        bmp.Save(filename, ImageFormat.Png);
                    else
                        Clipboard.SetDataObject(bmp);
                }
            }
            else if (format == FormatEnum.TIFF)//Tiff形式 個別保存のみ
            {
                for (int r = 0; r < row; r++)
                    for (int c = 0; c < col; c++)
                        if (pseudo[r, c].Width != 0)
                        {
                            var text = (pseudo[r, c].Tag as ImageInfo).Text.Replace("\r\n", ", ");
                            var fn = pseudo.Length == 1 ? filename : filename.Replace(".tif", " (" + text + ").tif");
                            Tiff.Writer(fn, pseudo[r, c].SrcValuesGray, 3, width);
                        }
            }
        }
    }

    bool tableLayoutPanelFocused = false;
    private void TableLayoutPanel_Enter(object sender, EventArgs e) => tableLayoutPanelFocused = true;

    private void TableLayoutPanel_Leave(object sender, EventArgs e) => tableLayoutPanelFocused = false;

    private void FormImageSimulator_KeyDown(object sender, KeyEventArgs e)
    {
        if (tableLayoutPanelFocused && e.Control && e.KeyCode == Keys.C)
            ToolStripMenuItemCopyMetafile_Click(sender, new EventArgs());
    }
    private void ToolStripMenuItemSavePNG_Click(object sender, EventArgs e) => Save(FormatEnum.PNG, ActionEnum.Save);
    private void ToolStripMenuItemSaveTIFF_Click(object sender, EventArgs e) => Save(FormatEnum.TIFF, ActionEnum.Save);
    private void ToolStripMenuItemSaveMetafile_Click(object sender, EventArgs e) => Save(FormatEnum.Meta, ActionEnum.Save);
    private void ToolStripMenuItemCopyImage_Click(object sender, EventArgs e) => Save(FormatEnum.PNG, ActionEnum.Copy);
    private void ToolStripMenuItemCopyMetafile_Click(object sender, EventArgs e) => Save(FormatEnum.Meta, ActionEnum.Copy);
    #endregion 画像のコピー/保存

    #region その他イベント
    private void DetailsOfHRTEMSimulationToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        new FormPDF(appPath + @"\doc\hrtem.pdf").ShowDialog();
    }


    private void PictureBox_DrawingAreaChanged(object sender, double zoom, PointD center)
    {
        if (SkipEvent) return;

        var box = sender as ScalablePictureBox;
        if (box.PseudoBitmap.Width == 0)
            return;

        foreach (var b in pictureBoxes)
            if (b != null && b != (ScalablePictureBox)sender)
            {
                b.DrawingAreaChanged -= PictureBox_DrawingAreaChanged;
                b.ZoomAndCenter = (zoom, center);
                b.DrawingAreaChanged += PictureBox_DrawingAreaChanged;
            }
    }
    #endregion

    #region 画像表示関連、画像の輝度、カラースケール、ガウシアンぼかし

    public bool SkipEvent = false;

    private void radioButtonSTEM_target_both_CheckedChanged(object sender, EventArgs e)
    {
        GeneratePseudBitmap();
    }

    private void checkBoxIntensityMin_CheckedChanged(object sender, EventArgs e)
    {
        numericBoxIntensityMin.Enabled = checkBoxIntensityMin.Checked;
        numericBoxIntensityMax.Enabled = checkBoxIntensityMax.Checked;
        GeneratePseudBitmap();
    }
    private void RadioButtonPotentialAsMagnitudeAndPhase_CheckedChanged(object sender, EventArgs e)
    {
        flowLayoutPanelMagAndPhase.Visible = panelPhaseScale.Visible = radioButtonPotentialModeMagAndPhase.Checked;
        flowLayoutPanelRealAndImaiginary.Visible = radioButtonPotentialModeRealAndImag.Checked;
    }

    private bool TrackBarAdvancedMin_ValueChanged(object sender, double value)
    {
        if (SkipEvent) return false;
        foreach (var box in pictureBoxes)
            if (box.PseudoBitmap.Tag != null && !(box.PseudoBitmap.Tag as ImageInfo).LockIntensity)
            {
                box.PseudoBitmap.MaxValue = trackBarAdvancedMax.Value;
                box.PseudoBitmap.MinValue = trackBarAdvancedMin.Value;
                box.drawPictureBox();
            }
        return false;
    }

    private void ComboBoxScaleColorScale_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (SkipEvent) return;

        if (comboBoxScaleColorScale.SelectedIndex == 0)
            scaleImage.SetScaleGray();

        else
            scaleImage.SetScaleColdWarm();
        pictureBoxScaleOfIntensity.Image = scaleImage.GetImage();

        foreach (var box in pictureBoxes)
            if (box.PseudoBitmap.Tag != null && !(box.PseudoBitmap.Tag as ImageInfo).LockIntensity)
            {
                if (comboBoxScaleColorScale.SelectedIndex == 0)
                    box.PseudoBitmap.SetScaleGray();
                else
                    box.PseudoBitmap.SetScaleColdWarm();
                box.drawPictureBox();
            }
    }
    private void CheckBoxGaussianBlur_CheckedChanged(object sender, EventArgs e)
    {
        if (SkipEvent) return;

        numericBoxGaussianBlurRadius.Enabled = checkBoxGaussianBlur.Checked;

        foreach (var box in pictureBoxes)
            if (box.PseudoBitmap.Tag != null && !(box.PseudoBitmap.Tag as ImageInfo).LockIntensity)
            {
                if (checkBoxGaussianBlur.Checked)
                    box.PseudoBitmap.SetBlurImage(numericBoxGaussianBlurRadius.Value / numericBoxResolution.Value, PseudoBitmap.BlurModeEnum.Gaussian);
                else
                    box.PseudoBitmap.SetOriginalGray();

                box.drawPictureBox();
            }
    }
    #endregion 画像の輝度、カラースケール、ガウシアンぼかし

    #region 右クリックメニュー
    private void setZeroDefocusToolStripMenuItem_Click(object sender, EventArgs e) => numericBoxDefocus.Value = 0;

    private void setScherzerDefocusToolStripMenuItem_Click(object sender, EventArgs e) => numericBoxDefocus.Value = Scherzer;

    private void zeroAllToolStripMenuItem_Click(object sender, EventArgs e) => numericBoxCc.Value = numericBoxCs.Value = numericBoxHRTEM_BetaAgnle.Value = numericBoxDeltaV.Value = numericBoxDefocus.Value = 0;

    private void presets1ToolStripMenuItem_Click(object sender, EventArgs e)
    {//ARM300F
        AccVol = 300;
        Cs = 0 * 1000000;
        Cc = 2.8 * 1000000;
        DeltaVol = 0.3 / 1000 / 2 / Sqrt(2 * Log(2));
        Defocus = Scherzer;

    }

    private void presets2ToolStripMenuItem_Click(object sender, EventArgs e)
    {//Schottky JEM2100F UHR
        AccVol = 200;
        Cs = 0.5 * 1000000;
        Cc = 1.1 * 1000000;
        DeltaVol = 0.8 / 1000 / 2 / Sqrt(2 * Log(2));
        Defocus = Scherzer;
    }

    private void presets3ToolStripMenuItem_Click(object sender, EventArgs e)
    {//Schottky JEM2100F HR
        AccVol = 200;
        Cs = 1.0 * 1000000;
        Cc = 1.4 * 1000000;
        DeltaVol = 0.8 / 1000 / 2 / Sqrt(2 * Log(2));
        Defocus = Scherzer;
    }

    private void presets4ToolStripMenuItem_Click(object sender, EventArgs e)
    {//LAB6 JEM2010 HR
        AccVol = 200;
        Cs = 1.0 * 1000000;
        Cc = 1.4 * 1000000;
        DeltaVol = 2.0 / 1000 / 2 / Sqrt(2 * Log(2));
        Defocus = Scherzer;
    }


    private void typicalBF02MradToolStripMenuItem_Click(object sender, EventArgs e)
    {
        STEM_ConvergenceAngle = 25.0 / 1000;
        STEM_DetectorInnerAngle = 0;
        STEM_DetectorOuterAngle = 5.0 / 1000;
    }

    private void typicalABF1224MradToolStripMenuItem_Click(object sender, EventArgs e)
    {
        STEM_ConvergenceAngle = 25.0 / 1000;
        STEM_DetectorInnerAngle = 12.0 / 1000;
        STEM_DetectorOuterAngle = 24.0 / 1000;
    }

    private void typicalLAADF2560MradToolStripMenuItem_Click(object sender, EventArgs e)
    {
        STEM_ConvergenceAngle = 25.0 / 1000;
        STEM_DetectorInnerAngle = 26.0 / 1000;
        STEM_DetectorOuterAngle = 60.0 / 1000;
    }

    private void typicalHAADF80250MradToolStripMenuItem_Click(object sender, EventArgs e)
    {
        STEM_ConvergenceAngle = 25.0 / 1000;
        STEM_DetectorInnerAngle = 80.0 / 1000;
        STEM_DetectorOuterAngle = 250.0 / 1000;

    }

    #endregion

    #region プリセットフォーム、CTFグラフフォームの表示/非表示
    private void checkBoxPreset_CheckedChanged(object sender, EventArgs e)
    {
        FormPresets.Visible = checkBoxPreset.Checked;
    }

    private void checkBoxShowLensFunctionGraph_CheckedChanged(object sender, EventArgs e)
    {
        FormCTF.Visible = checkBoxCTF.Checked;
    }
    #endregion

}
