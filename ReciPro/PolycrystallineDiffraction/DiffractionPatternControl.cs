using MathNet.Numerics.LinearAlgebra.Double;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ReciPro;

public partial class DiffractionPatternControl : UserControl
{
    public DiffractionPatternControl() => InitializeComponent();

    #region プロパティ, フィールド

    // public PseudoBitmap DiffractionPattern;
    private PseudoBitmap pseudoBitmap = new();

    private List<Crystal> crystals = null;

    public List<Crystal> Crystals
    {
        set { crystals = value; SetPlaneIndex(); }
        get => crystals;
    }

    #region WaveProperty関連

    public double Cameralength
    {
        set => numericBoxCameraLength.Value = value;
        get => numericBoxCameraLength.Value;
    }

    public double Wavelength
    {
        set => waveLengthControl.WaveLength = value;
        get => waveLengthControl.WaveLength;
    }

    public WaveSource WaveSource
    {
        set => waveLengthControl.WaveSource = value;
        get => waveLengthControl.WaveSource;
    }

    public WaveProperty WaveProperty
    {
        get => new(waveLengthControl.WaveSource, waveLengthControl.WaveLength, Monochromaticity, Convergence, false);
        set
        {
            waveLengthControl.WaveSource = value.Source;
            waveLengthControl.XrayWaveSourceElementNumber = value.XrayElementNumber;
            waveLengthControl.XrayWaveSourceLine = value.XrayLine;
            waveLengthControl.WaveLength = value.WaveLength;

            if (value.Convergence != 0)
                Convergence = value.Convergence;
            else
                Convergence = Math.PI / 180.0 * 0.05;

            if (value.Monochromaticity != 0)
                Monochromaticity = value.Monochromaticity;
            else
                Monochromaticity = 0.0001;
        }
    }

    public double Convergence
    {
        set => numericBoxConvergentAngle.RadianValue = value;
        get => numericBoxConvergentAngle.RadianValue;
    }

    public double ConvergenceDegree
    {
        set => numericBoxConvergentAngle.Value = value;
        get => numericBoxConvergentAngle.Value;
    }

    private double monochromaticity = 0;

    public double Monochromaticity
    {
        set
        {
            monochromaticity = value;
            try
            {
                if (numericBoxMonochromaticity.Value != value * 100.0)
                    numericBoxMonochromaticity.Value = value * 100.0;
            }
            catch { }
        }
        get => monochromaticity;
    }

    #endregion WaveProperty関連

    #region 画像のプロパティ関連

    /// <summary>
    /// イメージの横幅(ピクセル単位)
    /// </summary>
    public int ImageWidth
    {
        get => (int)numericUpDownImageWidth.Value;
        set => numericUpDownImageWidth.Value = value;
    }

    /// <summary>
    /// イメージの高さ(ピクセル単位)
    /// </summary>
    public int ImageHeight
    {
        get => (int)numericUpDownImageHeight.Value;
        set => numericUpDownImageHeight.Value = value;
    }

    /// <summary>
    /// イメージの全ピクセル数
    /// </summary>
    public int ImageLength => ImageHeight * ImageWidth;

    /// <summary>
    /// イメージの解像度 (mm/pixel)
    /// </summary>
    public double Resolution
    {
        get => numericBoxImageResolution.Value;
        set => numericBoxImageResolution.Value = value;
    }

    /// <summary>
    /// イメージの傾きPhi
    /// </summary>
    public double Phi
    {
        set => numericBoxPhi.RadianValue = value;
        get => numericBoxPhi.RadianValue;
    }

    /// <summary>
    /// イメージの傾きTau
    /// </summary>
    public double Tau
    {
        set => numericBoxTau.RadianValue = value;
        get => numericBoxTau.RadianValue;
    }

    /// <summary>
    /// イメージの傾き
    /// </summary>
    public Matrix3D Rotation => Matrix3D.Rot(new Vector3D(Math.Cos(Phi), Math.Sin(Phi), 0), Tau);

    private delegate void callBack();

    private PointD center;

    public PointD Center
    {
        set
        {
            if (this.InvokeRequired)//別スレッドから呼び出されたとき
            {
                this.Invoke(new callBack(delegate { Center = value; }));
                return;
            }

            center = value;

            numericBoxCenterX.Value = center.X;
            numericBoxCenterY.Value = center.Y;
            DetectorProperty.Center = value;
        }
        get => new(numericBoxCenterX.Value, numericBoxCenterY.Value);
    }

    private AreaDetector detectorProperty = null;

    /// <summary>
    /// 画像プロパティ
    /// </summary>
    public AreaDetector DetectorProperty
    {
        set => detectorProperty = value;
        get
        {
            if (detectorProperty == null)
                detectorProperty = new AreaDetector(ImageWidth, ImageHeight, Resolution, Center, WaveProperty, Cameralength);
            else
            {
                detectorProperty.Center = Center;
                detectorProperty.ImageHeight = ImageHeight;
                detectorProperty.ImageWidth = ImageWidth;
                detectorProperty.Resolution = Resolution;
                detectorProperty.CameraLength = Cameralength;
                detectorProperty.WaveProperty = WaveProperty;
            }
            return detectorProperty;
        }
    }

    #endregion 画像のプロパティ関連

    private bool isReferrenceImage = false;

    public bool IsReferrenceImage
    {
        set
        {
            isReferrenceImage = value;
            checkBoxInitialBackground.Enabled = value;
            buttonSaveBackGround.Enabled = value;
            if (value)
            {
                trackBarBgA.Value = Math.Min((int)(Math.Log(graphControlFrequency.Profile.Pt[^1].X, 7) * 100), trackBarBgA.Maximum);
                trackBarBgA.Value = 1;
                trackBarBgH.Maximum = ImageHeight / 3;
                trackBarBgH.Minimum = 1;
                trackBarBgH.Value = (int)(ImageHeight * 0.1);
                trackBarBgR.Maximum = ImageHeight * 2;
                trackBarBgR.Minimum = 0;
                trackBarBgR.Value = 0;
            }
        }
        get => isReferrenceImage;
    }

    public bool SimulationCheck
    {
        set => panelSimulationCheck.Enabled = value;
        get => panelSimulationCheck.Enabled;
    }

    /// <summary>
    /// フィルムにじみ量
    /// </summary>
    public double FilmBlur
    {
        set
        {
            if (this.InvokeRequired)//別スレッドから呼び出されたとき
            {
                this.Invoke(new callBack(delegate { FilmBlur = value; }));
                return;
            }
            numericBoxFilmBlur.Value = value;
        }
        get => numericBoxFilmBlur.Value;
    }

    #region ピクセル配列群

    /// <summary>
    /// フィルターされたピクセル配列
    /// </summary>
    public bool[] Filter;

    /// <summary>
    ///  リファレンス画像のピクセル配列
    /// </summary>
    public double[] SrcPixels;

    /// <summary>
    /// trueがバックグランドのみからなる領域
    /// </summary>
    public bool[] BackgroundArea;

    /// <summary>
    /// バックグラウンドのピクセル配列
    /// </summary>
    public double[] BackgroundPixels;

    /// <summary>
    /// 回折リングのピクセル配列
    /// </summary>
    public double[] DiffractionPixels;

    public double[] DiffractionPixelsTemp;

    /// <summary>
    /// バックグランド + 回折リング × スケール因子 のピクセル配列
    /// </summary>
    public double[] SimulatedPixels;

    /// <summary>
    /// 最終的に描画するピクセル配列
    /// </summary>
    public double[] DestPixels;

    #endregion ピクセル配列群

    public double[] Weight;

    public double ScaleFactor;
    #endregion プロパティ, フィールド

    public void SetImage(double[] srcPixels, int width, PointD center, double resolution, (byte R, byte G, byte B)[] scale)
    {
        if (srcPixels != null)
            scalablePictureBox.PseudoBitmap = new PseudoBitmap(srcPixels, width, scale, true);

        Center = center;
        SrcPixels = srcPixels;
        Filter = new bool[srcPixels.Length];
        DiffractionPixels = new double[srcPixels.Length];
        BackgroundPixels = new double[srcPixels.Length];
        DestPixels = new double[srcPixels.Length];
        Weight = new double[srcPixels.Length];
        ImageWidth = width;
        ImageHeight = srcPixels.Length / width;
        Resolution = resolution;
        for (int j = 0; j < Weight.Length; j++)
            Weight[j] = SrcPixels[j] > 0.1 ? 1 / SrcPixels[j] : 10;

        numericBoxImageResolution.Value = resolution;
        numericBoxCenterX.Value = center.X;
        numericBoxCenterY.Value = center.Y;

        Profile p = setFrequencyProfile(srcPixels);

        numericUpDownMaxInt.Maximum = Math.Max((decimal)p.Pt[^1].X, 2);
        numericUpDownMinInt.Maximum = Math.Max((decimal)p.Pt[^1].X - 1, 1);
        numericUpDownMaxInt.Minimum = 1;
        numericUpDownMinInt.Minimum = 0;
        scalablePictureBox.PseudoBitmap.MaxValue = (uint)((double)numericUpDownMaxInt.Maximum);
        scalablePictureBox.PseudoBitmap.MinValue = (uint)((double)numericUpDownMinInt.Minimum);

        graphControlFrequency.Profile = p;
        PointD[] pt = new PointD[] { new PointD((double)numericUpDownMinInt.Minimum, double.NaN), new PointD((double)numericUpDownMaxInt.Maximum, double.NaN) };
        graphControlFrequency.VerticalLines = pt;

        graphControlFrequency.Draw();

        scalablePictureBox_MouseUp2(new object(), new MouseEventArgs(new MouseButtons(), 0, 0, 0, 0), new PointD());
    }

    public void SetScale((byte R, byte G, byte B)[] scale, bool isNegative, bool isGray)
    {
        if (scalablePictureBox.PseudoBitmap != null)
        {
            scalablePictureBox.PseudoBitmap.ColorScale = scale;
            scalablePictureBox.PseudoBitmap.IsNegative = isNegative;
            scalablePictureBox.PseudoBitmap.GrayScale = isGray;
            scalablePictureBox.drawPictureBox();
        }
    }

    #region 頻度ダイアグラムおよび輝度調整

    private void graphControlFrequency_LinePositionChanged()
    {
        if (graphControlFrequency.VerticalLines.Length == 2)
        {
            if (graphControlFrequency.VerticalLines[1].X == graphControlFrequency.VerticalLines[0].X) return;

            decimal max = (int)Math.Max(graphControlFrequency.VerticalLines[0].X, graphControlFrequency.VerticalLines[1].X);
            if (numericUpDownMaxInt.Maximum < max)
                numericUpDownMaxInt.Value = numericUpDownMaxInt.Maximum;
            else if (numericUpDownMinInt.Minimum > max)
                numericUpDownMaxInt.Value = numericUpDownMaxInt.Minimum;
            else
                numericUpDownMaxInt.Value = max;

            decimal min = (int)Math.Min(graphControlFrequency.VerticalLines[0].X, graphControlFrequency.VerticalLines[1].X);
            if (numericUpDownMinInt.Maximum < min)
                numericUpDownMinInt.Value = numericUpDownMinInt.Maximum;
            else if (numericUpDownMinInt.Minimum > min)
                numericUpDownMinInt.Value = numericUpDownMinInt.Minimum;
            else
                numericUpDownMinInt.Value = min;
        }
    }

    //private delegate SortedList<uint, int> calcFreqDelegate(int start, int end);

    private object lockObj = new object();

    /// <summary>
    /// Frequencyを計算
    /// </summary>
    /// <param name="pixels"></param>
    /// <returns></returns>

    private Profile setFrequencyProfile(double[] pixels)
    {
        SortedList<uint, int> frequency = new SortedList<uint, int>();
        if (pixels == null || pixels.Length == 0) return null;
        frequency.Clear();
        int thread = 16;
        //var calcFreqThread = new calcFreqDelegate[thread];

        //for (int i = 0; i < thread; i++)
        //    calcFreqThread[i] = new calcFreqDelegate((start, end) =>
        SortedList<uint, int> calcFreqDelegate(int start, int end)
        {
            var freq = new SortedList<uint, int>();
            for (int j = start; j < end; j++)
            {
                if (!Filter[j])
                {
                    uint value;
                    value = (uint)(Math.Pow(1.003, Math.Round(Math.Log(pixels[j], 1.003))));

                    if (freq.ContainsKey(value))
                        freq[value] += 1;
                    else
                        freq.Add(value, 1);
                }
            }
            return freq;
        };
        //var ar = new IAsyncResult[thread];
        //for (int i = 0; i < thread; i++)
        //    ar[i] = calcFreqThread[i].BeginInvoke(pixels.Length / thread * i, Math.Min(pixels.Length / thread * (i + 1), pixels.Length), null, null);
        Parallel.For(0, thread, i =>
        {
            //var temp = calcFreqThread[i].EndInvoke(ar[i]);
            var temp = calcFreqDelegate(pixels.Length / thread * i, Math.Min(pixels.Length / thread * (i + 1), pixels.Length));
            lock (lockObj)
            {
                foreach (uint j in temp.Keys)
                    if (frequency.TryGetValue(j, out int value))
                        frequency[j] += temp[j];
                    else
                        frequency.Add(j, value);
            }
        });
        Profile p = new Profile();
        for (int i = 0; i < frequency.Count; i++)
            p.Pt.Add(new PointD(frequency.Keys[i], frequency[frequency.Keys[i]]));
        return p;
    }

    private void numericUpDownMinInt_ValueChanged(object sender, EventArgs e)
    {
        if (scalablePictureBox.PseudoBitmap != null)
        {
            scalablePictureBox.PseudoBitmap.MinValue = (uint)((double)numericUpDownMinInt.Value);
            if (scalablePictureBox.PseudoBitmap.MaxValue <= scalablePictureBox.PseudoBitmap.MinValue)
                numericUpDownMaxInt.Value = numericUpDownMinInt.Value + 1;
            if (graphControlFrequency.VerticalLines != null && graphControlFrequency.VerticalLines.Length == 2)
            {
                graphControlFrequency.VerticalLines[graphControlFrequency.VerticalLines[0].X < graphControlFrequency.VerticalLines[1].X ? 0 : 1].X = (double)numericUpDownMinInt.Value;
                graphControlFrequency.Draw();
            }
            scalablePictureBox.drawPictureBox();
        }
    }

    private void numericUpDownMaxInt_ValueChanged(object sender, EventArgs e)
    {
        if (scalablePictureBox.PseudoBitmap != null)
        {
            scalablePictureBox.PseudoBitmap.MaxValue = (uint)((double)numericUpDownMaxInt.Value);
            if (scalablePictureBox.PseudoBitmap.MaxValue <= scalablePictureBox.PseudoBitmap.MinValue)
                if (numericUpDownMinInt.Minimum <= numericUpDownMaxInt.Value - 1)
                    numericUpDownMinInt.Value = numericUpDownMaxInt.Value - 1;
            if (graphControlFrequency.VerticalLines != null && graphControlFrequency.VerticalLines.Length == 2)
            {
                graphControlFrequency.VerticalLines[graphControlFrequency.VerticalLines[0].X < graphControlFrequency.VerticalLines[1].X ? 1 : 0].X = (double)numericUpDownMaxInt.Value;
                graphControlFrequency.Draw();
            }
            scalablePictureBox.drawPictureBox();
        }
    }

    #endregion 頻度ダイアグラムおよび輝度調整

    #region Mask関連

    private void checkBoxRectangleMask_CheckedChanged(object sender, EventArgs e)
    {
        groupBoxRectangle.Enabled = checkBoxMaskRectangle.Checked;
        if (checkBoxMaskRectangle.Checked)
        {
            checkBoxMaskDonut.Checked = checkBoxMaskManual.Checked = checkBoxMaskDiffractionPeaks.Checked = false;
            setRectangleMask();
        }
        else
            scalablePictureBox.PseudoBitmap.Filter3 = new List<bool>(new bool[SrcPixels.Length]);
        scalablePictureBox.drawPictureBox();
    }

    private void checkBoxCircleMask_CheckedChanged(object sender, EventArgs e)
    {
        groupBoxCircleMask.Enabled = checkBoxMaskDonut.Checked;
        if (checkBoxMaskDonut.Checked)
        {
            checkBoxMaskRectangle.Checked = checkBoxMaskManual.Checked = checkBoxMaskDiffractionPeaks.Checked = false;
            setCircleMask();
        }
        else
            scalablePictureBox.PseudoBitmap.Filter3 = new List<bool>(new bool[SrcPixels.Length]);
        scalablePictureBox.drawPictureBox();
    }

    private void checkBoxManualMask_CheckedChanged(object sender, EventArgs e)
    {
        scalablePictureBox.ManualSpotMode = groupBoxManualSpot.Enabled = checkBoxMaskManual.Checked;

        if (checkBoxMaskManual.Checked)
            checkBoxMaskRectangle.Checked = checkBoxMaskDonut.Checked = checkBoxMaskDiffractionPeaks.Checked = false;
        else
            scalablePictureBox.PseudoBitmap.Filter3 = new List<bool>(new bool[ImageHeight * ImageWidth]);
        scalablePictureBox.drawPictureBox();
    }

    private void checkBoxDiffractionPeaks_CheckedChanged(object sender, EventArgs e)
    {
        groupBoxPeakIndices.Enabled = checkBoxMaskDiffractionPeaks.Checked;
        if (checkBoxMaskDiffractionPeaks.Checked)
            checkBoxMaskRectangle.Checked = checkBoxMaskDonut.Checked = checkBoxMaskManual.Checked = false;
        else
            scalablePictureBox.PseudoBitmap.Filter3 = new List<bool>(new bool[ImageHeight * ImageWidth]);
        scalablePictureBox.drawPictureBox();
    }

    private void comboBoxRectangleDirection_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch ((string)comboBoxRectangleDirection.SelectedItem)
        {
            case "Top":
                numericUpDownRectangleAngle.Enabled = false;
                numericUpDownRectangleBand.Enabled = true;
                checkBoxRectangleIsBothSide.Enabled = false;
                checkBoxRectangleIsBothSide.Checked = false;
                numericUpDownRectangleAngle.Value = 270;
                break;

            case "Bottom":
                numericUpDownRectangleAngle.Enabled = false;
                numericUpDownRectangleBand.Enabled = true;
                checkBoxRectangleIsBothSide.Enabled = false;
                checkBoxRectangleIsBothSide.Checked = false;
                numericUpDownRectangleAngle.Value = 90;
                break;

            case "Right":
                numericUpDownRectangleAngle.Enabled = false;
                numericUpDownRectangleBand.Enabled = true;
                checkBoxRectangleIsBothSide.Enabled = false;
                checkBoxRectangleIsBothSide.Checked = false;
                numericUpDownRectangleAngle.Value = 0;
                break;

            case "Left":
                numericUpDownRectangleAngle.Enabled = false;
                numericUpDownRectangleBand.Enabled = true;
                checkBoxRectangleIsBothSide.Enabled = false;
                checkBoxRectangleIsBothSide.Checked = false;
                numericUpDownRectangleAngle.Value = 180;
                break;

            case "Vertical":
                numericUpDownRectangleAngle.Enabled = false;
                numericUpDownRectangleBand.Enabled = true;
                checkBoxRectangleIsBothSide.Enabled = false;
                checkBoxRectangleIsBothSide.Checked = true;
                numericUpDownRectangleAngle.Value = 90;
                break;

            case "Horizontal":
                numericUpDownRectangleAngle.Enabled = false;
                numericUpDownRectangleBand.Enabled = true;
                checkBoxRectangleIsBothSide.Enabled = false;
                checkBoxRectangleIsBothSide.Checked = true;
                numericUpDownRectangleAngle.Value = 0;
                break;

            case "Free":
                numericUpDownRectangleAngle.Enabled = true;
                numericUpDownRectangleBand.Enabled = true;
                checkBoxRectangleIsBothSide.Enabled = true;
                numericUpDownRectangleAngle.Value = 0;
                break;
        }
        setRectangleMask();
    }

    private void checkBoxRectangleIsBothSide_CheckedChanged(object sender, EventArgs e) => setRectangleMask();

    private void numericUpDownRectangleBand_ValueChanged(object sender, EventArgs e) => setRectangleMask();

    private void numericUpDownRectangleAngle_ValueChanged(object sender, EventArgs e) => setRectangleMask();

    #region 矩形マスク

    private void setRectangleMask()
    {
        double CenterX = Center.X;
        double CenterY = Center.Y;
        double angle = (double)numericUpDownRectangleAngle.Value / 180.0 * Math.PI;
        double bandWidth = (double)numericUpDownRectangleBand.Value;
        bool isBothSide = checkBoxRectangleIsBothSide.Checked;
        int Width = ImageWidth;
        int Height = ImageHeight;

        scalablePictureBox.PseudoBitmap.Filter3 = new List<bool>(new bool[SrcPixels.Length]);

        bool IsXY = false;
        double tan = Math.Tan(angle);
        double sin = Math.Sin(angle);
        double cos = Math.Cos(angle);
        double wx = Math.Abs(bandWidth / sin);
        double wy = Math.Abs(bandWidth / cos);
        double cx, cy;
        if (Math.Abs(tan) > 1)
            IsXY = true;//縦方向に近い場合はTrue
        int jWidth;
        int startI, endI, midI;
        int startJ, endJ, midJ;
        if (IsXY)
        {
            double MinusCenterYPerTanPlusCenterX = -CenterY / tan + CenterX;

            if (isBothSide)
            {//IsXYがTrueで全直線モード
                for (int j = 0; j < Height; j++)
                {
                    cx = j / tan + MinusCenterYPerTanPlusCenterX;
                    startI = (int)(cx - wx + 0.5);
                    if (startI < 0) startI = 0;
                    endI = (int)(cx + wx + 1.5);
                    if (endI > Width) endI = Width;
                    jWidth = j * Width;
                    for (int i = startI; i < endI; i++)//バンドの内側のとき
                        scalablePictureBox.PseudoBitmap.Filter3[i + jWidth] = true;
                }
            }
            else
            {//IsXYがTrueで半直線モード
                double CenterXPerTanPlusCenterY = CenterX / tan + CenterY;
                if (sin > 0)//下に伸びた半直線のときは
                {
                    startJ = (int)(CenterY - bandWidth * Math.Abs(cos) + 0.5);//スタート地点
                    midJ = (int)(CenterY + bandWidth * Math.Abs(cos) + 0.5);//中間地点
                    for (int j = startJ; j < Height; j++)
                    {
                        cx = j / tan + MinusCenterYPerTanPlusCenterX;
                        startI = (int)(cx - wx + 0.5);
                        if (startI < 0) startI = 0;
                        endI = (int)(cx + wx + 1.5);
                        if (endI > Width) endI = Width;
                        jWidth = j * Width;
                        if (j > midJ)
                            for (int i = startI; i < endI; i++)//バンドの内側のとき
                                scalablePictureBox.PseudoBitmap.Filter3[i + jWidth] = true;
                        else
                            for (int i = startI; i < endI; i++)//バンドの内側のとき
                                if (j + i / tan > CenterXPerTanPlusCenterY)
                                    scalablePictureBox.PseudoBitmap.Filter3[i + jWidth] = true;
                    }
                }
                else//上に伸びた半直線のときは
                {
                    midJ = (int)(CenterY - bandWidth * Math.Abs(cos) + 0.5);
                    endJ = (int)(CenterY + bandWidth * Math.Abs(cos) + 0.5);
                    for (int j = 0; j < endJ; j++)
                    {
                        cx = j / tan + MinusCenterYPerTanPlusCenterX;
                        startI = (int)(cx - wx + 0.5);
                        if (startI < 0) startI = 0;
                        endI = (int)(cx + wx + 1.5);
                        if (endI > Width) endI = Width;
                        jWidth = j * Width;
                        if (j < midJ)
                            for (int i = startI; i < endI; i++)//バンドの内側のとき
                                scalablePictureBox.PseudoBitmap.Filter3[i + jWidth] = true;
                        else
                            for (int i = startI; i < endI; i++)//バンドの内側のとき
                                if (j + i / tan < CenterXPerTanPlusCenterY)
                                    scalablePictureBox.PseudoBitmap.Filter3[i + jWidth] = true;
                    }
                }
            }
        }
        else//IsXYがFalse
        {
            double CenterYMinusTanCenterX = CenterY - tan * CenterX;
            if (isBothSide)
            {
                for (int i = 0; i < Width; i++)
                {
                    cy = tan * i + CenterYMinusTanCenterX;
                    startJ = (int)(cy - wy + 0.5);
                    if (startJ < 0) startJ = 0;
                    endJ = (int)(cy + wy + 1.5);
                    if (endJ > Height) endJ = Height;
                    for (int j = startJ; j < endJ; j++)//バンドの内側のとき
                        scalablePictureBox.PseudoBitmap.Filter3[i + j * Width] = true;
                }
            }
            else
            {//半直線モードのとき
                double CenterYTanPlusCenterX = CenterY * tan + CenterX;

                if (cos > 0)//右に伸びた半直線のときは
                {
                    startI = (int)(CenterX - bandWidth * Math.Abs(sin) + 0.5);
                    midI = (int)(CenterX + bandWidth * Math.Abs(sin) + 0.5);
                    for (int i = startI; i < Width; i++)
                    {
                        cy = tan * i + CenterYMinusTanCenterX;
                        startJ = (int)(cy - wy + 0.5);
                        if (startJ < 0) startJ = 0;
                        endJ = (int)(cy + wy + 1.5);
                        if (endJ > Height) endJ = Height;
                        if (i > midI)
                            for (int j = startJ; j < endJ; j++)//バンドの内側のとき
                                scalablePictureBox.PseudoBitmap.Filter3[i + j * Width] = true;
                        else
                            for (int j = startJ; j < endJ; j++)
                                if (i + j * tan > CenterYTanPlusCenterX)
                                    scalablePictureBox.PseudoBitmap.Filter3[i + j * Width] = true;
                    }
                }
                else//左に伸びた半直線のときは
                {
                    midI = (int)(CenterX - bandWidth * Math.Abs(sin) + 0.5);
                    endI = (int)(CenterX + bandWidth * Math.Abs(sin) + 0.5);
                    for (int i = 0; i < endI; i++)
                    {
                        cy = tan * i + CenterYMinusTanCenterX;
                        startJ = (int)(cy - wy + 0.5);
                        if (startJ < 0) startJ = 0;
                        endJ = (int)(cy + wy + 1.5);
                        if (endJ > Height) endJ = Height;

                        if (i < midI)
                            for (int j = startJ; j < endJ; j++)//バンドの内側のとき
                                scalablePictureBox.PseudoBitmap.Filter3[i + j * Width] = true;
                        else
                            for (int j = startJ; j < endJ; j++)
                                if (i + j * tan < CenterYTanPlusCenterX)
                                    scalablePictureBox.PseudoBitmap.Filter3[i + j * Width] = true;
                    }
                }
            }
        }
        scalablePictureBox.drawPictureBox();
    }

    #endregion 矩形マスク

    private void numericUpDownCircleStart_ValueChanged(object sender, EventArgs e) => setCircleMask();

    private void setCircleMask()
    {
        scalablePictureBox.PseudoBitmap.Filter3 = new List<bool>(new bool[SrcPixels.Length]);

        double CenterX = Center.X;
        double CenterY = Center.Y;
        scalablePictureBox.PseudoBitmap.Filter3 = new List<bool>(new bool[SrcPixels.Length]);
        double start = (double)numericUpDownCircleStart.Value;
        double end = (double)numericUpDownCircleEnd.Value;

        for (int h = 0; h < ImageHeight; h++)
            for (int w = 0; w < ImageWidth; w++)
            {
                double distance = (w - CenterX) * (w - CenterX) + (h - CenterY) * (h - CenterY);

                scalablePictureBox.PseudoBitmap.Filter3[h * ImageWidth + w] = start * start < distance && distance < end * end;
            }
        scalablePictureBox.drawPictureBox();
    }

    private void buttonMask_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < scalablePictureBox.PseudoBitmap.Filter3.Count; i++)
            if (scalablePictureBox.PseudoBitmap.Filter3[i])
            {
                scalablePictureBox.PseudoBitmap.Filter1[i] = Filter[i] = true;
                scalablePictureBox.PseudoBitmap.Filter3[i] = false;
            }
        graphControlFrequency.ReplaceProfile(0, setFrequencyProfile(SrcPixels));
        scalablePictureBox.drawPictureBox();
    }

    private void buttonUnmask_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < scalablePictureBox.PseudoBitmap.Filter3.Count; i++)
            if (scalablePictureBox.PseudoBitmap.Filter3[i])
            {
                scalablePictureBox.PseudoBitmap.Filter1[i] = Filter[i] = false;
                scalablePictureBox.PseudoBitmap.Filter3[i] = false;
            }
        graphControlFrequency.ReplaceProfile(0, setFrequencyProfile(SrcPixels));
        scalablePictureBox.drawPictureBox();
    }

    private void buttonClearMask_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < scalablePictureBox.PseudoBitmap.Filter3.Count; i++)
            scalablePictureBox.PseudoBitmap.Filter1[i] = Filter[i] = false;
        scalablePictureBox.drawPictureBox();
    }

    private void buttonMaskAll_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < scalablePictureBox.PseudoBitmap.Filter3.Count; i++)
            scalablePictureBox.PseudoBitmap.Filter1[i] = Filter[i] = true;
        scalablePictureBox.drawPictureBox();
    }

    private void checkBoxShowMaskedArea_CheckedChanged(object sender, EventArgs e)
    {
        scalablePictureBox.PseudoBitmap.Filter1Visible = checkBoxShowMaskedArea.Checked;
        scalablePictureBox.drawPictureBox();
    }

    private PointD startMaskPoint = new PointD(double.NaN, double.NaN);

    private bool scalablePictureBox_MouseDown2(object sender, MouseEventArgs e, PointD pt)
    {
        if (checkBoxMaskManual.Checked)
        {
            if (radioButtonManualSpot.Checked && (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right))
            {
                int spotSize = Convert.ToInt16(comboBoxSpotSize.Text);
                for (int j = Math.Max((int)Math.Round(pt.Y - spotSize), 0); j <= Math.Min((int)Math.Round(pt.Y + spotSize), ImageHeight - 1); j++)
                    for (int i = Math.Max((int)Math.Round(pt.X - spotSize), 0); i <= Math.Min((int)Math.Round(pt.X + spotSize), ImageWidth - 1); i++)
                        if ((i - pt.X) * (i - pt.X) + (j - pt.Y) * (j - pt.Y) <= spotSize * spotSize)
                            scalablePictureBox.PseudoBitmap.Filter3[j * ImageWidth + i] = e.Button == MouseButtons.Left;
                scalablePictureBox.drawPictureBox();
            }
            else
                startMaskPoint = new PointD(pt);
        }
        return false;
    }

    private bool scalablePictureBox_MouseMove2(object sender, MouseEventArgs e, PointD pt)
    {
        if (checkBoxMaskManual.Checked)
        {
            if (radioButtonManualSpot.Checked)
            {
                var spotSize = Convert.ToInt16(comboBoxSpotSize.Text);
                for (int j = Math.Max((int)Math.Round(pt.Y - spotSize), 0); j <= Math.Min((int)Math.Round(pt.Y + spotSize), ImageHeight - 1); j++)
                    for (int i = Math.Max((int)Math.Round(pt.X - spotSize), 0); i <= Math.Min((int)Math.Round(pt.X + spotSize), ImageWidth - 1); i++)
                        if ((i - pt.X) * (i - pt.X) + (j - pt.Y) * (j - pt.Y) <= spotSize * spotSize)
                            if (e.Button == System.Windows.Forms.MouseButtons.None)
                                scalablePictureBox.PseudoBitmap.FilterTemporary[j * ImageWidth + i] = true;
                            else
                                scalablePictureBox.PseudoBitmap.Filter3[j * ImageWidth + i] = e.Button == MouseButtons.Left;
            }
            else if (radioButtonManualCircle.Checked && !double.IsNaN(startMaskPoint.X))
            {
                var r = (startMaskPoint - pt).Length;
                var top = Math.Max((int)(startMaskPoint.Y - r - 0.5), 0);
                var bottom = Math.Min((int)(startMaskPoint.Y + r + 0.5), ImageHeight - 1);
                var left = Math.Max((int)(startMaskPoint.X - r - 0.5), 0);
                var right = Math.Min((int)(startMaskPoint.X + r + 0.5), ImageWidth - 1);

                for (int j = top; j <= bottom; j++)
                    for (int i = left; i <= right; i++)
                        if ((new PointD(i, j) - startMaskPoint).Length < r)
                            scalablePictureBox.PseudoBitmap.FilterTemporary[j * ImageWidth + i] = true;
            }
            else if (radioButtonManualRectangle.Checked && !double.IsNaN(startMaskPoint.X))
            {
                var startPoint = new Point((int)Math.Round(startMaskPoint.X), (int)Math.Round(startMaskPoint.Y));
                var currentPoint = new Point((int)Math.Round(pt.X), (int)Math.Round(pt.Y));
                var top = Math.Min(startPoint.Y, currentPoint.Y);
                var bottom = Math.Max(startPoint.Y, currentPoint.Y);
                var left = Math.Min(startPoint.X, currentPoint.X);
                var right = Math.Max(startPoint.X, currentPoint.X);

                for (int j = Math.Max(top, 0); j <= Math.Min(bottom, ImageHeight - 1); j++)
                    for (int i = Math.Max(left, 0); i <= Math.Min(right, ImageWidth - 1); i++)
                        scalablePictureBox.PseudoBitmap.FilterTemporary[j * ImageWidth + i] = true;
            }
            else if (radioButtonManualDonut.Checked && !double.IsNaN(startMaskPoint.X))
            {
                var minR = Math.Min((center - pt).Length, (startMaskPoint - center).Length);
                var maxR = Math.Max((center - pt).Length, (startMaskPoint - center).Length);
                var top = (int)Math.Max(center.Y - maxR - 0.5, 0);
                var bottom = (int)Math.Min(center.Y + maxR + 0.5, ImageHeight - 1);
                var left = (int)Math.Max(center.X - maxR - 0.5, 0);
                var right = (int)Math.Min(center.X + maxR + 0.5, ImageWidth - 1);

                for (int j = top; j <= bottom; j++)
                    for (int i = left; i <= right; i++)
                    {
                        double r2 = (new PointD(i, j) - center).Length2;
                        if (r2 < maxR * maxR && r2 > minR * minR)
                            scalablePictureBox.PseudoBitmap.FilterTemporary[j * ImageWidth + i] = true;
                    }
            }
            scalablePictureBox.drawPictureBox();
        }
        return false;
    }

    private bool scalablePictureBox_MouseUp2(object sender, MouseEventArgs e, PointD pt)
    {
        if (checkBoxMaskManual.Checked)
        {
            if (radioButtonManualCircle.Checked && !double.IsNaN(startMaskPoint.X))
            {
                var r = (startMaskPoint - pt).Length;
                var top = Math.Max((int)(startMaskPoint.Y - r - 0.5), 0);
                var bottom = Math.Min((int)(startMaskPoint.Y + r + 0.5), ImageHeight - 1);
                var left = Math.Max((int)(startMaskPoint.X - r - 0.5), 0);
                var right = Math.Min((int)(startMaskPoint.X + r + 0.5), ImageWidth - 1);

                for (int j = top; j <= bottom; j++)
                    for (int i = left; i <= right; i++)
                        if ((new PointD(i, j) - startMaskPoint).Length < r)
                            scalablePictureBox.PseudoBitmap.Filter3[j * ImageWidth + i] = e.Button == System.Windows.Forms.MouseButtons.Left;
                scalablePictureBox.drawPictureBox();
            }
            else if (radioButtonManualRectangle.Checked && startMaskPoint.X != int.MinValue)
            {
                var startPoint = new Point((int)Math.Round(startMaskPoint.X), (int)Math.Round(startMaskPoint.Y));
                var currentPoint = new Point((int)Math.Round(pt.X), (int)Math.Round(pt.Y));
                int top = Math.Min(startPoint.Y, currentPoint.Y);
                int bottom = Math.Max(startPoint.Y, currentPoint.Y);
                int left = Math.Min(startPoint.X, currentPoint.X);
                int right = Math.Max(startPoint.X, currentPoint.X);

                for (int j = Math.Max(top, 0); j <= Math.Min(bottom, ImageHeight - 1); j++)
                    for (int i = Math.Max(left, 0); i <= Math.Min(right, ImageWidth - 1); i++)
                        scalablePictureBox.PseudoBitmap.Filter3[j * ImageWidth + i] = e.Button == System.Windows.Forms.MouseButtons.Left;
            }
            else if (radioButtonManualDonut.Checked && !double.IsNaN(startMaskPoint.X))
            {
                double minR = Math.Min((center - pt).Length, (startMaskPoint - center).Length);
                double maxR = Math.Max((center - pt).Length, (startMaskPoint - center).Length);
                int top = (int)Math.Max(center.Y - maxR - 0.5, 0);
                int bottom = (int)Math.Min(center.Y + maxR + 0.5, ImageHeight - 1);
                int left = (int)Math.Max(center.X - maxR - 0.5, 0);
                int right = (int)Math.Min(center.X + maxR + 0.5, ImageWidth - 1);

                for (int j = top; j <= bottom; j++)
                    for (int i = left; i <= right; i++)
                    {
                        var r2 = (new PointD(j, i) - center).Length2;
                        if (r2 < maxR * maxR && r2 > minR * minR)
                            scalablePictureBox.PseudoBitmap.Filter3[j * ImageWidth + i] = e.Button == System.Windows.Forms.MouseButtons.Left;
                    }
            }

            scalablePictureBox.drawPictureBox();
            startMaskPoint = new PointD(double.NaN, double.NaN);
        }
        else
        {
            skipEvent = true;
            numericBoxMonitorResolution.Value = scalablePictureBox.Zoom * numericBoxImageResolution.Value;
            skipEvent = false;
        }

        return false;
    }

    private void buttonUnmaskSelectedPeaks_Click(object sender, EventArgs e)
    {
        /*   if (checkedListBoxPlaneList.CheckedItems.Count <= 0)
               return;

           List<Vector3D> temporalVectorOfG = new List<Vector3D>();
           List<Vector3D> originalVectorOfG = new List<Vector3D>(polyCrystal.BaseCrystal.VectorOfG.ToArray());
           List<int> validIndex = new List<int>();
           for (int i = 0; i < checkedListBoxPlaneList.CheckedItems.Count; i++)
           {
               string[] index = ((string)(checkedListBoxPlaneList.CheckedItems[i])).Split(' ');
               double d = (Convert.ToInt32(index[0]) * this.polyCrystal.BaseCrystal.A_Star + Convert.ToInt32(index[1]) * this.polyCrystal.BaseCrystal.B_Star + Convert.ToInt32(index[2]) * this.polyCrystal.BaseCrystal.C_Star).Length();
               for (int n = 0; n < originalVectorOfG.Count; n++)
                   if (originalVectorOfG[n].Length() < d * 1.0001 && originalVectorOfG[n].Length() > d * 0.9999 && !validIndex.Contains(n))
                   {
                       validIndex.Add(n);
                       temporalVectorOfG.Add(originalVectorOfG[n]);
                   }
           }
           polyCrystal.BaseCrystal.VectorOfG = temporalVectorOfG;
           int maxIndex = validIndex[validIndex.Count - 1];
           List<int> temporalIndex = new List<int>();
           int[][] originalIndex = new int[polyCrystal.Crysatallites.Length][];
           for (int i = 0; i < polyCrystal.Crysatallites.Length; i++)
           {
               int length = polyCrystal.Crysatallites[i].index.Count;
               if (length > 0)
               {
                   originalIndex[i] = new int[length];
                   polyCrystal.Crysatallites[i].index.CopyTo(originalIndex[i], 0);
                   temporalIndex.Clear();
                   for (int j = 0; j < length && originalIndex[i][j] <= maxIndex; j++)
                   {
                       int n = validIndex.IndexOf(originalIndex[i][j]);
                       if (n >= 0) temporalIndex.Add(n);
                   }
                   polyCrystal.Crysatallites[i].index = new List<int>(temporalIndex.ToArray());
               }
               else
                   originalIndex[i] = new int[0];
           }

           double[] pixel = this.polyCrystal.GetSimulatedPattern(Rotation, ImageWidth, ImageHeight, Resolution, Center, Convergence, Monochromaticity, false, false, true, polyCrystal.Crysatallites, ReciprocalVector, ReciprocalArea);
           pixel = convoluteInstrumentsFunction(pixel, filmBlur * 1.5);
           for (int i = 0; i < scalablePictureBox.PseudoBitmap.Filter3.Count; i++)
               if (pixel[i] != 0)
                   scalablePictureBox.PseudoBitmap.Filter3[i] = true;// Filter[i] = false;
           graphControlFrequency.ReplaceProfile(0, setFrequencyProfile(SrcPixels));
           scalablePictureBox.drawPictureBox();

           polyCrystal.BaseCrystal.VectorOfG = originalVectorOfG;
           for (int i = 0; i < polyCrystal.Crysatallites.Length; i++)
               polyCrystal.Crysatallites[i].index = new List<int>(originalIndex[i].ToArray());
           */
    }

    #endregion Mask関連

    public void setSimulatedPixels()
    {
        if (SrcPixels == null)
        {
            SrcPixels = new double[DiffractionPixels.Length];
            Filter = new bool[DiffractionPixels.Length];
            BackgroundPixels = new double[DiffractionPixels.Length];
            DestPixels = new double[DiffractionPixels.Length];
            scalablePictureBox.PseudoBitmap = new PseudoBitmap(SrcPixels, ImageWidth);
        }

        if (DiffractionPixels.Length == SrcPixels.Length)
        {
            SimulatedPixels = new double[DiffractionPixels.Length];

            for (int i = 0; i < DiffractionPixels.Length; i++)
                if (!Filter[i])
                    SimulatedPixels[i] = ScaleFactor * DiffractionPixels[i] + BackgroundPixels[i];

            Profile p = setFrequencyProfile(SimulatedPixels);
            if (graphControlFrequency.ProfileList.Length == 1)
                graphControlFrequency.AddProfile(p);
            else
                graphControlFrequency.ReplaceProfile(1, p);
            graphControlFrequency.ProfileList[1].Color = Color.Green;

            if (graphControlFrequency.VerticalLines.Length == 0)
            {
                PointD[] pt = new PointD[] { new PointD((double)numericUpDownMinInt.Minimum, double.NaN), new PointD((double)numericUpDownMaxInt.Maximum, double.NaN) };
                graphControlFrequency.VerticalLines = pt;
                numericUpDownMaxInt_ValueChanged(new object(), new EventArgs());
            }

            graphControlFrequency.Draw();

            setDestPixels();
        }
    }

    private void setDestPixels()
    {
        if (SimulatedPixels != null)
        {
            if (checkBoxSimulation.Checked)//
            {
                double opacity = trackBarOpacity.Value / 100.0;

                for (int i = 0; i < SrcPixels.Length; i++)
                    DestPixels[i] = SrcPixels[i] * (1 - opacity) + SimulatedPixels[i] * opacity;
            }
            else if (checkBoxResidual.Checked)
            {
                double baseIntensity = 5000;
                if (numericUpDownMaxInt.Maximum < 10000)
                    baseIntensity = (double)numericUpDownMaxInt.Maximum / 2.0;

                for (int i = 0; i < SrcPixels.Length; i++)
                    if (!Filter[i])
                        DestPixels[i] = SrcPixels[i] - SimulatedPixels[i] + baseIntensity;
                    else
                        DestPixels[i] = 0;
            }
            else
            {
                for (int i = 0; i < SrcPixels.Length; i++)
                    DestPixels[i] = SrcPixels[i];
            }
            scalablePictureBox.PseudoBitmap.SrcValuesGray = DestPixels;
            scalablePictureBox.drawPictureBox();
        }
    }

    private void trackBarOpacity_Scroll(object sender, EventArgs e) => setDestPixels();

    private void checkBoxSimulation_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBoxSimulation.Checked)
        {
            checkBoxResidual.Checked = false;
            trackBarOpacity.Enabled = true;
        }
        else
            trackBarOpacity.Enabled = false;
        setSimulatedPixels();
    }

    private void checkBoxResidual_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBoxResidual.Checked)
            checkBoxSimulation.Checked = false;
        setSimulatedPixels();
    }

    #region Debyeリングシミュレーション関連

    public double[] GetDiffractionPixels(bool renewDiffractionPixels, bool renewRVector, bool renewGVector, double[] density = null)
    {
        if (renewRVector || detectorProperty == null)
        {
            detectorProperty = new AreaDetector(ImageWidth, ImageHeight, Resolution, Center, WaveProperty, Cameralength);
            detectorProperty.SetReciprocalSpace();
        }

        double[] diffractionPixels = new double[ImageLength];
        foreach (Crystal c in Crystals)
        {
            if (renewGVector)
            {
                if (c.Crystallites == null)
                    c.SetCrystallites();
                c.Crystallites.SetGVector(c, DetectorProperty);
                c.Crystallites.ValidIndex = null;
                //c.Crystallites.ValidSubRotNum = null;
            }
            if (renewDiffractionPixels)
            {
                c.Crystallites.ProgressChanged += ProgressChanged;
                c.Crystallites.SetDiffractedPixels(DetectorProperty);
                c.Crystallites.ProgressChanged -= ProgressChanged;

            }

            double[] temp = c.Crystallites.GetSimulatedPattern(DetectorProperty, density);

            for (int i = 0; i < ImageLength; i++)
                diffractionPixels[i] += temp[i];
        }
        return diffractionPixels;
    }

    public event ProgressChangedEventHandler ProgressChanged;

    public void Simulate(bool resetIndex, bool renewRvector, bool renewGvector, bool doFilter, YusaGonio gonio = null)
    {
        DiffractionPixels = GetDiffractionPixels(resetIndex, renewRvector, renewGvector);

        if (doFilter)
            DiffractionPixels = convoluteInstrumentsFunction(DiffractionPixels, FilmBlur);
    }

    /* public double[] GetPartialDiffractionPixels(int CrystalIndex, Dictionary<int, double> density, bool doFilter)
     {
         double[] temp = Crystals[CrystalIndex].Crystallites.GetSimulatedPattern(DetectorProperty, density);
         if (doFilter)
             temp = convoluteInstrumentsFunction(temp, FilmBlur);
         return temp;
     }
     */

    private double lpo_residual;

    /// <summary>
    /// 指定された
    /// </summary>
    /// <param name="addCrystals"></param>
    /// <param name="removeCrystals"></param>
    /// <param name="resetIndex"></param>
    /// <param name="renewRvector"></param>
    /// <param name="renewGvector"></param>
    /// <param name="doFilter"></param>
    /// <returns></returns>
    public double PartialDensity(int CrystalIndex, int[] densityIndex, double[] densityValue, double lpo_ratio, bool useBeforeParam = false)
    {
        if (!useBeforeParam)
        {
            double[] residualDiffrence2 = new double[ImageHeight];
            Parallel.For(10, ImageHeight - 10, y =>
            {
                for (int i = y * ImageWidth + 10; i < y * ImageWidth + ImageWidth - 10; i++)
                    if (!Filter[i] && DiffractionPixels[i] != 0)
                    {
                        double temp1 = DiffractionPixels[i] * ScaleFactor + BackgroundPixels[i] - SrcPixels[i];
                        double temp2 = temp1 - DiffractionPixels[i] * lpo_ratio * ScaleFactor;
                        residualDiffrence2[y] += (temp2 * temp2 - temp1 * temp1) * Weight[i];
                    }
            });
            lpo_residual = residualDiffrence2.Sum();
        }

        DiffractionPixelsTemp = Crystals[CrystalIndex].Crystallites.GetSimulatedPattern(DetectorProperty, densityIndex, densityValue);
        DiffractionPixelsTemp = convoluteInstrumentsFunction(DiffractionPixelsTemp, FilmBlur);

        double[] residualDiffrence = new double[ImageHeight];

        Parallel.For(10, ImageHeight - 10, y =>
        {
            for (int i = y * ImageWidth + 10; i < y * ImageWidth + ImageWidth - 10; i++)
                if (!Filter[i] && DiffractionPixelsTemp[i] != 0)
                {
                    double temp1 = DiffractionPixels[i] * (1 - lpo_ratio) * ScaleFactor + BackgroundPixels[i] - SrcPixels[i];
                    double temp2 = temp1 + DiffractionPixelsTemp[i] * ScaleFactor;
                    residualDiffrence[y] += (temp2 * temp2 - temp1 * temp1) * Weight[i];
                }
        });
        return residualDiffrence.Sum() + lpo_residual;
    }

    public void ApplyPartialDensity(int CrystalIndex, int[] densityIndices, double[] densityValues, double lpo_ratio)
    {
        DiffractionPixelsTemp = Crystals[CrystalIndex].Crystallites.GetSimulatedPattern(DetectorProperty, densityIndices, densityValues);

        DiffractionPixelsTemp = convoluteInstrumentsFunction(DiffractionPixelsTemp, FilmBlur);
        Parallel.For(10, ImageHeight - 10, y =>
        {
            for (int x = 10; x < ImageWidth - 10; x++)
            {
                int i = y * ImageWidth + x;
                if (!Filter[i] && DiffractionPixelsTemp[i] != 0)
                    DiffractionPixels[i] = DiffractionPixels[i] * (1 - lpo_ratio) + DiffractionPixelsTemp[i];
            }
        });
    }

    /*
    internal void ApplySimulationTempolary()
    {
        for (int i = 0; i < DiffractionPixelsTemp.Length; i++)
            if (DiffractionPixelsTemp[i] != 0)
                DiffractionPixels[i] += DiffractionPixelsTemp[i];
    }
    */

    private double[] blur = Array.Empty<double>();

    /// <summary>
    /// 画像を指定したfilmBlur量でにじませる
    /// </summary>
    /// <param name="pixels"></param>
    /// <param name="filmBlur"></param>
    /// <returns></returns>
    private double[] convoluteInstrumentsFunction(double[] pixels, double filmBlur)
    {
        double filmBlurPixel = filmBlur / Resolution / 1000;//ピクセル単位でのフィルムにじみ半値幅
        int limit = (int)(filmBlurPixel * 4) * 4 + 1;
        int center = limit / 2;
        if (limit == 1)
        {
            double[] pixelsFilmBlur2 = new double[ImageWidth * ImageHeight];
            for (int i = 0; i < pixels.Length; i++) pixelsFilmBlur2[i] = pixels[i];
            return pixelsFilmBlur2;
        }
        else
        {
            //  if (blur.Length != limit)
            {
                blur = new double[limit];
                for (int h = 0; h < limit; h++)
                    blur[h] = Math.Exp(-(h - center) * (h - center) / filmBlurPixel / filmBlurPixel * Math.Log(2));
                double sum = blur.Sum();
                for (int i = 0; i < blur.Length; i++)
                    blur[i] /= sum;
            }

            double[] pixelsFilmBlur1 = new double[ImageWidth * ImageHeight];

            Parallel.For(0, ImageWidth, w =>
           {
               for (int h = 0; h < ImageHeight; h++)
                   if (pixels[h * ImageWidth + w] != 0)
                       for (int n = Math.Max(0, center - h); n < Math.Min(blur.Length, ImageHeight - h + center); n++)
                           pixelsFilmBlur1[(h - center + n) * ImageWidth + w] += blur[n] * pixels[h * ImageWidth + w];
           });

            double[] pixelsFilmBlur2 = new double[ImageWidth * ImageHeight];
            Parallel.For(0, ImageHeight, h =>
            {
                for (int w = 0; w < ImageWidth; w++)
                    if (pixelsFilmBlur1[h * ImageWidth + w] != 0)
                        for (int n = Math.Max(0, center - w); n < Math.Min(blur.Length, ImageWidth - w + center); n++)
                            pixelsFilmBlur2[h * ImageWidth + w - center + n] += blur[n] * pixelsFilmBlur1[h * ImageWidth + w];
            });
            pixelsFilmBlur1 = null;

            return pixelsFilmBlur2;
        }
    }

    #endregion Debyeリングシミュレーション関連

    /// <summary>
    /// スケールファクター（DiffractionPixelsを何倍にするか）の最適値をセットし、その残差を得る。
    /// </summary>
    public double RefineScaleFactor()
    {
        double[] squareSum = new double[ImageHeight], product = new double[ImageHeight];
        // for (int y = 10; y < ImageHeight - 10; y++)
        Parallel.For(10, ImageHeight - 10, y =>
        {
            for (int x = 10; x < ImageWidth - 10; x++)
            {
                int i = y * ImageWidth + x;
                if (!Filter[i] && DiffractionPixels[i] != 0)
                {
                    //二乗の和と積を計算
                    squareSum[y] += DiffractionPixels[i] * DiffractionPixels[i] * Weight[i];
                    product[y] += DiffractionPixels[i] * (SrcPixels[i] - BackgroundPixels[i]) * Weight[i];
                }
            }
        });
        ScaleFactor = product.Sum() / squareSum.Sum();
        product = null;
        squareSum = null;
        return Residual();
    }

    /// <summary>
    /// 現在のシミュレーション画像と生画像の残差を得る
    /// </summary>
    /// <returns></returns>
    public double Residual()
    {
        double[] residual = new double[ImageHeight];
        Parallel.For(10, ImageHeight - 10, y =>
        {
            for (int x = 10; x < ImageWidth - 10; x++)
            {
                int i = y * ImageWidth + x;
                if (!Filter[i])
                {
                    double temp = DiffractionPixels[i] * ScaleFactor + BackgroundPixels[i] - SrcPixels[i];
                    residual[y] += temp * temp * Weight[i];
                }
            }
        });
        return residual.Sum();
    }

    /*
    /// <summary>
    /// 現在のシミュレーション画像と生画像の残差を得る
    /// </summary>
    /// <returns></returns>
    public double ResidualOfDiffractionPixels(double[] diffractionPixels = null)
    {
        if (diffractionPixels == null)
            diffractionPixels = DiffractionPixels;

        double[] residual = new double[ImageHeight];
        Parallel.For(10, ImageHeight - 10, y =>
        {
            for (int x = 10; x < ImageWidth - 10; x++)
            {
                int i = y * ImageWidth + x;
                if (!Filter[i] && diffractionPixels[i] != 0)
                {
                    double temp = diffractionPixels[i] * ScaleFactor + BackgroundPixels[i] - SrcPixels[i];
                    residual[y] += temp * temp * Weight[i];
                }
            }
        });
        return residual.Sum();
    }
     */

    /// <summary>
    /// 生画像の二乗和を得る
    /// </summary>
    /// <returns></returns>
    public double InitialResidual()
    {
        double residual = 0;
        for (int y = 10; y < ImageHeight - 10; y++)
            for (int x = 10; x < ImageWidth - 10; x++)
            {
                int i = y * ImageWidth + x;
                if (!Filter[i])
                    residual += SrcPixels[i] * SrcPixels[i] * Weight[i];
            }
        return residual;
    }

    /// <summary>
    /// バックグラウンド範囲を抽出する
    /// </summary>
    /// <returns></returns>
    public void SetBackgroundArea()
    {
        if (Crystals.Count == 0) return;
        double[] pixels = new double[DetectorProperty.ImageLength];
        for (int i = 0; i < Crystals.Count; i++)
        {
            double[] tempPixels = new double[DetectorProperty.ImageLength];//Powder.GetSimulatedPattern(Crystals[i], new Crystallite(Crystallite.GenerateRandomOrientation(3000000), 100), true, DetectorProperty);
            for (int j = 0; j < DetectorProperty.ImageLength; j++)
                pixels[j] += tempPixels[j];
        }

        pixels = convoluteInstrumentsFunction(pixels, FilmBlur);

        BackgroundArea = new bool[pixels.Length];
        for (int i = 0; i < BackgroundArea.Length; i++)
            BackgroundArea[i] = pixels[i] == 0;

        for (int y = 0; y < ImageHeight; y++)
            for (int x = 0; x < ImageWidth; x++)
            {
                int i = y * ImageWidth + x;
                if (y < 10 || y > ImageHeight - 11 || x < 10 || x > ImageWidth - 11)
                    BackgroundArea[i] = false;
                else if (Filter[i])
                    BackgroundArea[i] = false;
            }
    }

    [Serializable()]
    public struct BackGroundParameter
    {
        public double H, X, Y, A, R, B1, B2, B3;
        /* public BackGroundParameter()
         {
             H = X = Y = A = R = C = B1 = B2 = B3;
         }*/

        public BackGroundParameter(double h, double x, double y, double a, double r, double b1, double b2, double b3)
        {
            H = h; X = x; Y = y; A = a; R = r; B1 = b1; B2 = b2; B3 = b3;
        }

        public double getBg(double x, double y)
        {
            double r = Math.Sqrt((x - X) * (x - X) + (y - Y) * (y - Y));
            return A / (1 + 4 * (r - R) / H * (r - R) / H) + B1 + B2 * r / 1000;
        }
    }

    public BackGroundParameter beforeParameter = new BackGroundParameter();

    public double RefineBackGround() => RefineBackGround(beforeParameter);

    private delegate Matrix[] RefineBackGroundDelegate(int start, int end);

    /// <summary>
    /// バックグラウンドパラメータとスケールファクターを最適化し、最適化後の残差を返す
    /// </summary>
    /// <param name="prm"></param>
    /// <returns></returns>
    public double RefineBackGround(BackGroundParameter prm)
    {
        #region

        if (BackgroundArea == null)
            SetBackgroundArea();

        var prmNew = new BackGroundParameter(0, 0, 0, 0, 0, 0, 0, 0);

        int prmNum = 7;
        double[][] diff = new double[prmNum][];
        for (int i = 0; i < diff.Length; i++)
            diff[i] = new double[ImageWidth * ImageHeight];

        double[] residualCurrent = new double[ImageWidth * ImageHeight], residualNew = new double[ImageWidth * ImageHeight];
        double ResidualSquareCurrent = 0, ResidualSquareNew = 0;

        //現在の残差を計算
        for (int y = 10; y < ImageHeight - 10; y++)
            for (int x = 10, pos = y * ImageWidth + x; x < ImageWidth - 10; x++, pos++)
                if (BackgroundArea[pos])
                {
                    residualCurrent[pos] = SrcPixels[pos] - prm.getBg(x, y);// -B3 / r;
                    ResidualSquareCurrent += residualCurrent[pos] * residualCurrent[pos] * Weight[pos]; //   1/ src[y * ImageWidth + x]は重み
                }

        int threadTotal = 16;
        var thread = new RefineBackGroundDelegate[threadTotal];
        for (int t = 0; t < threadTotal; t++)
            thread[t] = new RefineBackGroundDelegate((start, end) =>
            {
                for (int y = start; y < end; y++)
                    for (int x = 10, pos = y * ImageWidth + x; x < ImageWidth - 10; x++, pos++)
                        if (BackgroundArea[pos])
                        {
                            double r = Math.Sqrt((x - prm.X) * (x - prm.X) + (y - prm.Y) * (y - prm.Y));
                            double rR = (r - prm.R), rR2 = rR * rR;
                            double h2 = prm.H * prm.H;
                            double denom = (h2 + 4 * rR2), denom2 = denom * denom;

                            diff[0][pos] = 1 / (1 + 4 * rR2 / h2);//∂F/∂A
                            diff[1][pos] = 8 * prm.A * prm.H * rR2 / denom2;//∂F/∂H
                            diff[4][pos] = 8 * prm.A * h2 * rR / denom2;//∂F/∂R
                            diff[2][pos] = diff[4][pos] * (x - prm.X) / r;//∂F/∂X
                            diff[3][pos] = diff[4][pos] * (y - prm.Y) / r;//∂F/∂Y
                            diff[5][pos] = 1;
                            diff[6][pos] = r / 1000;
                        }

                var alpha = new DenseMatrix(prmNum, prmNum);
                var beta = new DenseMatrix(prmNum, 1);

                for (int y = start; y < end; y++)
                    for (int x = 10, pos = y * ImageWidth + x; x < ImageWidth - 10; x++, pos++)
                        if (BackgroundArea[pos])
                            for (int k = 0; k < prmNum; k++)
                            {
                                beta[k, 0] += residualCurrent[pos] * diff[k][pos] * Weight[pos];
                                for (int l = k; l < prmNum; l++)
                                    alpha[k, l] += diff[k][pos] * diff[l][pos] * Weight[pos];
                            }
                return new Matrix[] { alpha, beta };
            });

        IAsyncResult[] ar = new IAsyncResult[threadTotal];
        double ramda = 1;
        int count = 0;
        var Alpha = new DenseMatrix(prmNum, prmNum);
        var Beta = new DenseMatrix(prmNum, 1);
        int eachHeight = (ImageHeight - 20) / threadTotal + 1;
        while (ramda < 1000 && count++ < 100)
        {
            for (int t = 0; t < threadTotal; t++)
                ar[t] = thread[t].BeginInvoke(Math.Max(10, eachHeight * t), Math.Min(eachHeight * (t + 1), ImageHeight - 10), null, null);

            for (int t = 0; t < threadTotal; t++)
            {
                Matrix[] m = thread[t].EndInvoke(ar[t]);
                for (int k = 0; k < prmNum; k++)
                {
                    Beta[k, 0] += m[1][k, 0];
                    for (int l = 0; l < prmNum; l++)
                        Alpha[k, l] += m[0][k, l];
                }
            }
            for (int k = 0; k < prmNum; k++)
            {
                Alpha[k, k] *= (1 + ramda);
                for (int l = k + 1; l < prmNum; l++)
                    Alpha[l, k] = Alpha[k, l];
            }

            var alphaInv = Alpha.TryInverse();
            if (alphaInv == null)
                break;

            var delta = Alpha.TryInverse() * Beta;

            prmNew.A = prm.A + delta[0, 0] > 1 ? prm.A + delta[0, 0] : prm.A;
            prmNew.H = prm.H + delta[1, 0] > 0.001 && prm.H + delta[1, 0] < ImageWidth ? prm.H + delta[1, 0] : prm.H;
            prmNew.X = Math.Abs(delta[2, 0] + prm.X - Center.X) < ImageWidth / 4.0 ? prm.X + delta[2, 0] : prm.X;
            prmNew.Y = Math.Abs(delta[3, 0] + prm.Y - Center.Y) < ImageHeight / 4.0 ? prm.Y + delta[3, 0] : prm.Y;
            prmNew.R = delta[4, 0] < ImageHeight ? prm.R + delta[4, 0] : prm.R;
            //prmNew.C = prm.C + delta[5, 0] > 0 ? prm.C + delta[5, 0] : prm.C;
            prmNew.B1 = prm.B1 + delta[5, 0];// > 0 ? C + delta[5, 0] : C;
            prmNew.B2 = prm.B2 + delta[6, 0];// > 0 ? C + delta[5, 0] : C;

            //あたらしいパラメータでの残差を計算
            ResidualSquareNew = 0;
            for (int y = 10; y < ImageHeight - 10; y++)
                for (int x = 10, pos = y * ImageWidth + x; x < ImageWidth - 10; x++, pos++)
                    if (BackgroundArea[pos])
                    {
                        residualNew[pos] = SrcPixels[y * ImageWidth + x] - prmNew.getBg(x, y);
                        ResidualSquareNew += residualNew[pos] * residualNew[pos] * Weight[pos]; //   1/ src[y * ImageWidth + x]は重み
                    }

            if (ResidualSquareCurrent > ResidualSquareNew)
            {
                prm = prmNew; //ScaleFactor = prmNew.C;
                if ((ResidualSquareCurrent - ResidualSquareNew) / ResidualSquareCurrent / 0.0000001 > 1 || count < 5 || ramda > 100)
                {
                    ResidualSquareCurrent = ResidualSquareNew;
                    ramda *= 0.4;
                    Array.Copy(residualNew, residualCurrent, residualNew.Length);
                }
                else
                    break;
            }
            else
                ramda *= 2.5;
        }

        for (int y = 0, pos = y * ImageWidth; y < ImageHeight; y++, pos = y * ImageWidth)
            for (int x = 0; x < ImageWidth; x++, pos++)
                BackgroundPixels[pos] = prm.getBg(x, y);

        beforeParameter = prm;

        return RefineScaleFactor();

        #endregion
    }

    public double filmBlurStep = 5;

    /// <summary>
    /// FilmBlurを最適化し、最適化後の残差を返す
    /// </summary>
    /// <returns></returns>
    public double RefineFilmBlur()
    {
        double[] src = GetDiffractionPixels(false, false, false); ;

        double residualMinimul = double.MaxValue;
        double residual;
        double bestFilmBlur = FilmBlur;

        for (int i = -10; i < 10; i++)
        {
            double blur = FilmBlur + i * filmBlurStep;
            if (blur > 0)
            {
                DiffractionPixels = convoluteInstrumentsFunction(src, blur);
                residual = RefineScaleFactor();
                if (residual < residualMinimul)
                {
                    residualMinimul = residual;
                    bestFilmBlur = blur;
                }
            }
        }
        if (bestFilmBlur == FilmBlur)
            filmBlurStep = Math.Max(1, filmBlurStep * 0.95);
        FilmBlur = bestFilmBlur;

        DiffractionPixels = convoluteInstrumentsFunction(src, bestFilmBlur);
        return RefineScaleFactor();
    }

    public double offsetStep = 0.2;
    public int offsetSearchRange = 2;//

    /// <summary>
    /// CenterOffsetを最適化し、最適化後の残差を返す. ScaleFactorも同時に直す
    /// </summary>
    /// <returns></returns>
    public double RefineCenterOffset()
    {
        double residualMinimul = double.MaxValue;
        PointD bestCenter = new PointD(Center.X, Center.Y);
        PointD originalCenter = new PointD(Center.X, Center.Y);

        //一旦Originalをコピー
        DiffractionPixels = GetDiffractionPixels(false, false, false);
        double[] originalDiffractionPixels = new double[DiffractionPixels.Length];
        for (int i = 0; i < originalDiffractionPixels.Length; i++)
            originalDiffractionPixels[i] = DiffractionPixels[i];

        List<double> residuals = new List<double>();
        ProgressChanged(this, new ProgressChangedEventArgs(0, new object[] { (double)0, "" }));

        for (double j = -offsetSearchRange; j <= offsetSearchRange; j++)
        {
            //まず、Y方向にズレた画像を作成
            double yDiv = j * offsetStep;
            double[] tempDiffractionPixels1 = new double[DiffractionPixels.Length];

            double a = yDiv < 0 ? 1 + yDiv : 1 - yDiv;
            int sign = yDiv < 0 ? 1 : -1;
            Parallel.For(1, ImageWidth - 1, x =>
            {
                for (int y = 1; y < ImageHeight - 1; y++)
                    if (originalDiffractionPixels[y * ImageWidth + x] != 0)
                    {
                        tempDiffractionPixels1[y * ImageWidth + x] += originalDiffractionPixels[y * ImageWidth + x] * a;
                        tempDiffractionPixels1[(y + sign) * ImageWidth + x] += originalDiffractionPixels[y * ImageWidth + x] * (1 - a);
                    }
            });

            for (int i = -offsetSearchRange; i <= offsetSearchRange; i++)
            {
                //まず、X方向にズレた画像を作成
                double xDiv = i * offsetStep;
                double[] tempDiffractionPixels2 = new double[DiffractionPixels.Length];

                a = xDiv < 0 ? 1 + xDiv : 1 - xDiv;
                sign = xDiv < 0 ? 1 : -1;
                Parallel.For(1, ImageHeight - 1, y =>
                {
                    for (int x = 1; x < ImageWidth - 1; x++)
                        if (tempDiffractionPixels1[y * ImageWidth + x] != 0)
                        {
                            tempDiffractionPixels2[y * ImageWidth + x] += tempDiffractionPixels1[y * ImageWidth + x] * a;
                            tempDiffractionPixels2[y * ImageWidth + x + sign] += tempDiffractionPixels1[y * ImageWidth + x] * (1 - a);
                        }
                });

                DiffractionPixels = convoluteInstrumentsFunction(tempDiffractionPixels2, FilmBlur);
                residuals.Add(RefineScaleFactor());

                if (residuals[^1] < residualMinimul)
                {
                    residualMinimul = residuals[^1];
                    bestCenter = new PointD(Center.X - xDiv, Center.Y - yDiv);
                }
            }
        }
        if (Center.X == bestCenter.X && Center.Y == bestCenter.Y)
        {
            offsetStep = Math.Max(0.001, 0.9 * offsetStep);
            DiffractionPixels = originalDiffractionPixels;
            DiffractionPixels = convoluteInstrumentsFunction(DiffractionPixels, FilmBlur);
            return RefineScaleFactor();
        }
        Center = bestCenter;

        for (int i = 0; i < Crystals.Count; i++)
            Crystals[i].Crystallites.SetGVector(Crystals[i], DetectorProperty);
        DiffractionPixels = GetDiffractionPixels(true, true, true);
        DiffractionPixels = convoluteInstrumentsFunction(DiffractionPixels, FilmBlur);

        return RefineScaleFactor();
    }

    #region バックグラウンド初期値関連

    private void checkBoxInitialBackground_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBoxInitialBackground.Checked)
        {
            groupBoxBackground.Enabled = true;
            setSimulatedPixels();
        }
        else
        {
            groupBoxBackground.Enabled = false;
        }
    }

    private double bgH = 0, bgR = 0, bgA = 0;

    private void trackBarBg_Scroll(object sender, EventArgs e)
    {
        bgH = trackBarBgH.Value;
        bgR = trackBarBgR.Value;
        bgA = Math.Pow(10.0, trackBarBgA.Value / 100.0);

        beforeParameter = new BackGroundParameter(bgH, center.X, center.Y, bgA, bgR, 0, 0, 0);
        for (int y = 0; y < ImageHeight; y++)
            for (int x = 0; x < ImageWidth; x++)
                BackgroundPixels[y * ImageWidth + x] = beforeParameter.getBg(x, y);// +B3 / r;

        setSimulatedPixels();
    }

    private void radioButtonBackground_CheckedChanged(object sender, EventArgs e) => setSimulatedPixels();

    #endregion

    private bool skipEvent = false;

    private void numericBoxMonitorResolution_ValueChanged(object sender, EventArgs e)
    {
        if (skipEvent) return;
        scalablePictureBox.Zoom = numericBoxMonitorResolution.Value / numericBoxImageResolution.Value;
        scalablePictureBox_MouseUp2(new object(), new MouseEventArgs(new MouseButtons(), 0, 0, 0, 0), new PointD());
    }

    #region バックグランドファイル読み書き

    private void buttonSaveBackGround_Click(object sender, EventArgs e)
    {
        var dlg = new SaveFileDialog { Filter = "Background file; *.bg|*.bg" };
        if (dlg.ShowDialog() == DialogResult.OK)
        {
            var serializer = new XmlSerializer(typeof(BackGroundParameter));
            var fs = new System.IO.FileStream(dlg.FileName, System.IO.FileMode.Create);
            serializer.Serialize(fs, beforeParameter);//シリアル化し、バイナリファイルに保存する
            fs.Close();//閉じる
        }
    }

    private void readBgFile(string fileName)
    {
        var serializer = new XmlSerializer(typeof(BackGroundParameter));
        var fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open);//ファイルを開く
        beforeParameter = (BackGroundParameter)serializer.Deserialize(fs);
        fs.Close();//閉じる
        for (int y = 0; y < ImageHeight; y++)
            for (int x = 0; x < ImageWidth; x++)
                BackgroundPixels[y * ImageWidth + x] = beforeParameter.getBg(x, y);// +B3 / r;
        setSimulatedPixels();
    }

    #endregion

    public void setBackgroundPixels()
    {
        if (BackgroundPixels != null)
            for (int y = 0; y < ImageHeight; y++)
                for (int x = 0; x < ImageWidth; x++)
                    BackgroundPixels[y * ImageWidth + x] = beforeParameter.getBg(x, y);// +B3 / r;
    }

    #region マスクファイル読み書き

    private void buttonSaveMask_Click(object sender, EventArgs e)
    {
        var dlg = new SaveFileDialog { Filter = "Mask file; *.mas|*.mas" };
        if (dlg.ShowDialog() == DialogResult.OK)
        {
            System.IO.BinaryWriter br = new System.IO.BinaryWriter(new System.IO.FileStream(dlg.FileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite));
            br.Write(Filter.Length);
            int n = 0;
            for (int i = 0; i < Filter.Length / 8; i++)
            {
                byte b = 0;
                byte m = 1;
                for (int j = 0; j < 8; j++)
                {
                    if (Filter[n++])
                        b += m;
                    m *= 2;
                }
                br.Write(b);
            }
            br.Close();
        }
    }

    private void readMaskFile(string filename)
    {
        try
        {
            System.IO.BinaryReader br = new System.IO.BinaryReader(new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite));
            bool[] mask = new bool[br.ReadInt32()];
            int n = 0;
            for (int i = 0; i < mask.Length / 8; i++)
            {
                byte b = br.ReadByte();
                for (int j = 0; j < 8; j++)
                {
                    mask[n++] = (b & 1) == 1;
                    b >>= 1;
                }
            }
            br.Close();

            if (Filter != null && mask != null && mask.Length == Filter.Length)
            {
                for (int i = 0; i < mask.Length; i++)
                    scalablePictureBox.PseudoBitmap.Filter1[i] = Filter[i] = mask[i];

                scalablePictureBox.drawPictureBox();
                graphControlFrequency.ReplaceProfile(0, setFrequencyProfile(SrcPixels));
            }
        }
        catch { }
    }

    #endregion

    #region ドラッグドロップイベント

    private void DiffractionPatternControl_DragEnter(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
            e.Effect = DragDropEffects.Copy; //ドラッグされたデータ形式を調べ、ファイルのときはコピーとする
        else

            e.Effect = DragDropEffects.None;//ファイル以外は受け付けない
    }

    public void DiffractionPatternControl_DragDrop(object sender, DragEventArgs e)
    {
        string[] fileNames = (string[])e.Data.GetData(DataFormats.FileDrop, false);
        foreach (string filename in fileNames)
        {
            if (filename.EndsWith("mas"))
                readMaskFile(filename);
            else if (filename.EndsWith("bg"))
                readBgFile(filename);
        }
    }

    #endregion

    public void ContolEnabled(bool flag) => groupBoxBackground.Enabled = groupBoxGeometry.Enabled = checkBoxInitialBackground.Enabled = flag;

    private void numericBoxCenterX_ValueChanged(object sender, EventArgs e) => center = new PointD(numericBoxCenterX.Value, numericBoxCenterY.Value);

    private delegate void SetPlaneIndexCallBack();

    public void SetPlaneIndex()
    {
        if (this.InvokeRequired)//別スレッドから呼び出されたとき
        {
            SetPlaneIndexCallBack d = new SetPlaneIndexCallBack(SetPlaneIndex);
            this.Invoke(d);
            return;
        }

        if (crystals == null || crystals.Count == 0) return;

        double maxLength = Math.Sqrt(Math.Max(center.X * center.X, (ImageWidth - center.X) * (ImageWidth - center.X)) + Math.Max(center.Y * center.Y, (ImageHeight - center.Y) * (ImageHeight - center.Y)));

        //foreach (PolyCrystal p in polyCrystals)
        // {
        //     double minD = p.WaveLength / Math.Sin(Math.Atan(maxLength * Resolution / p.CameraLength) / 2) / 2;
        //     p.BaseCrystal.SetPlanes(double.MaxValue, minD, true, true, false, false, HorizontalAxis.d, 0, 0);
        // }

        //checkedListBoxPlaneList.Items.Clear();
        //for (int i = 0; i < polyCrystal.BaseCrystal.Plane.Count; i++)
        //    if(polyCrystal.BaseCrystal.Plane[i].strCondition.Length==0)
        //    checkedListBoxPlaneList.Items.Add(polyCrystal.BaseCrystal.Plane[i].strHKL);
    }

    private void scalablePictureBox_Paint2(object sender, PaintEventArgs e)
    {
        /*
        if (checkedListBoxPlaneList.Items.Count>0 && polyCrystal.BaseCrystal!=null)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Font font = new Font("Tahoma", 10);
            Color stringColor = Color.Green, lineColor = Color.Green;
            PointD centerSrc = scalablePictureBox.ConvertToClientPt(Center);
            double phi = Math.Atan2(scalablePictureBox.Height / 2.0 - centerSrc.Y, scalablePictureBox.Width / 2.0 - centerSrc.X);

            List<PointD> lastPosition = new List<PointD>();
            for (int i = 0; i < checkedListBoxPlaneList.Items.Count; i++)
            {
                string[] index = ((string)(checkedListBoxPlaneList.Items[i])).Split(' ');
                double d = (Convert.ToInt32(index[0]) * this.polyCrystal.BaseCrystal.A_Star + Convert.ToInt32(index[1]) * this.polyCrystal.BaseCrystal.B_Star + Convert.ToInt32(index[2]) * this.polyCrystal.BaseCrystal.C_Star).Length();
                double r = polyCrystal.CameraLength * Math.Tan(2 * Math.Asin(polyCrystal.WaveLength * d / 2)) / Resolution;

                double tempPhi = phi;
                PointD stringPosiiton = scalablePictureBox.ConvertToClientPt(center + new PointD(r * Math.Cos(tempPhi), r * Math.Sin(tempPhi)));

                if (lastPosition.Count!=0)
                {
                    double step = 15 / r / scalablePictureBox.Zoom;
                    bool flag = true;
                    do
                    {
                        flag = false;
                        foreach (PointD pos in lastPosition)
                            if (Math.Abs(pos.X - stringPosiiton.X) < 30 && Math.Abs(pos.Y - stringPosiiton.Y) < 30)
                                flag = true;
                        if (flag)
                        {
                            tempPhi += step;
                            stringPosiiton = scalablePictureBox.ConvertToClientPt(center + new PointD(r * Math.Cos(tempPhi), r * Math.Sin(tempPhi)));
                        }
                    } while (flag);
                }
                e.Graphics.DrawString((string)checkedListBoxPlaneList.Items[i], font, new SolidBrush(stringColor), new PointF((float)stringPosiiton.X, (float)stringPosiiton.Y));
                e.Graphics.DrawLine(new Pen(lineColor,2f),
                    new PointF((float)(stringPosiiton.X - 10 * Math.Cos(tempPhi - Math.PI / 2)), (float)(stringPosiiton.Y - 10 * Math.Sin(tempPhi - Math.PI / 2))),
                    new PointF((float)(stringPosiiton.X + 10 * Math.Cos(tempPhi - Math.PI / 2)), (float)(stringPosiiton.Y + 10 * Math.Sin(tempPhi - Math.PI / 2))));
                lastPosition.Add(new PointD(stringPosiiton));
                if (lastPosition.Count > 5) lastPosition.RemoveAt(0);
            }
        }*/
    }

    private void buttonCheckAllIndices_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < checkedListBoxPlaneList.Items.Count; i++)
            checkedListBoxPlaneList.SetItemChecked(i, true);
    }

    private void buttonUncheckAllIndices_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < checkedListBoxPlaneList.Items.Count; i++)
            checkedListBoxPlaneList.SetItemChecked(i, false);
    }

    /// <summary>
    /// 回折に寄与した結晶数と面指数の情報を表示する
    /// </summary>
    public void DiffractionInformation()
    {
        /*   if (PolyCrystal == null) return;

           int n = 0;
           List<PlaneIndex> index= new List<PlaneIndex>();
           Dictionary<PlaneIndex, int> counter = new Dictionary<PlaneIndex, int>();
           for (int i = 0; i < PolyCrystal.BaseCrystal.VectorOfG.Count; i++)
           {
               Vector3D g = PolyCrystal.BaseCrystal.VectorOfG[i];
               PlaneIndex rootIndex = SymmetryStatic.GetRootPlaneIndex(new PlaneIndex(g.h, g.k, g.l), PolyCrystal.BaseCrystal.Symmetry);
               if (!counter.ContainsKey(rootIndex)){
                   counter.Add(rootIndex, 0);
                   index.Add(rootIndex);
           }}

           for (int i = 0; i < PolyCrystal.Crysatallites.Length; i++)
           {
               if (polyCrystal.Crysatallites[i].index != null)
               {
                   n += PolyCrystal.Crysatallites[i].index.Count;
                   for (int j = 0; j < PolyCrystal.Crysatallites[i].index.Count; j++)
                   {
                       Vector3D g = PolyCrystal.BaseCrystal.VectorOfG[PolyCrystal.Crysatallites[i].index[j]];
                       PlaneIndex rootIndex = SymmetryStatic.GetRootPlaneIndex(new PlaneIndex(g.h, g.k, g.l), PolyCrystal.BaseCrystal.Symmetry);
                       counter[rootIndex]++;
                   }
               }
           }

           string str = "";
           for (int i = 0; i < counter.Count; i++)
               str +=  index[i].h.ToString() +" " + index[i].k.ToString() + " " + index[i].l.ToString() + ": " +
                   counter[index[i]].ToString() + "(" + ((double)counter[index[i]]/PolyCrystal.Crysatallites.Length*100.0).ToString("f2")+"%)\r\n";

           textBoxDiffractionInformation.Text =
               "Crystallite number contoributing to diffraction: " + n.ToString() + "(" + ((double)n / PolyCrystal.Crysatallites.Length * 100.0).ToString("f2") + "%)\r\n"
                +str  ;
           */
    }

    /// <summary>
    /// 画像保存ボタンを押したときの動作
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void buttonSaveImage_Click(object sender, EventArgs e)
    {
        var dlg = new SaveFileDialog { Filter = "*.ipa|*.ipa" };
        if (dlg.ShowDialog() == DialogResult.OK)
            ImageIO.IPAImageWriter(dlg.FileName, SimulatedPixels, Resolution, new Size(ImageWidth, ImageHeight), Center, Cameralength, new WaveProperty(WaveSource, Wavelength, 0, 0, 0));
    }
}