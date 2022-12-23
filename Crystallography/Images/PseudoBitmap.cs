using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Crystallography;

[Serializable]
public class PseudoBitmap : IDisposable
{
    public void Dispose()
    {
        if (SrcBitmap != null)
            SrcBitmap.Dispose();
        if (destBmp != null)
            destBmp.Dispose();
    }

    #region static なコンストラクタ、フィールド

    //public static byte[] BrightnessScaleR = new byte[65536];//明るさスケール　16bit長に固定
    //public static byte[] BrightnessScaleG = new byte[65536];//明るさスケール　16bit長に固定
    //public static byte[] BrightnessScaleB = new byte[65536];//明るさスケール　16bit長に固定

    //public static byte[] BrightnessScaleLog = new byte[65536];//ログ用の明るさスケール　16bit長に固定
    //public static byte[] BrightnessScaleLiner = new byte[65536];//ログ用の明るさスケール　16bit長に固定

    public static (byte R, byte G, byte B)[] ColorScaleGrayLiner = new (byte R, byte G, byte B)[65536];
    public static (byte R, byte G, byte B)[] ColorScaleGrayLog = new (byte R, byte G, byte B)[65536];


    public static (byte R, byte G, byte B)[] ColorScaleColdWarmLiner = new (byte R, byte G, byte B)[65536];
    public static (byte R, byte G, byte B)[] ColorScaleColdWarmLog = new (byte R, byte G, byte B)[65536];

    public static (byte R, byte G, byte B)[] ColorScaleFireLiner = new (byte R, byte G, byte B)[65536];
    public static (byte R, byte G, byte B)[] ColorScaleFireLog = new (byte R, byte G, byte B)[65536];

    public static (byte R, byte G, byte B)[] ColorScaleSpectrumLiner = new (byte R, byte G, byte B)[65536];
    public static (byte R, byte G, byte B)[] ColorScaleSpectrumLog = new (byte R, byte G, byte B)[65536];

    public static (byte R, byte G, byte B)[] ColorScaleRotationLiner = new (byte R, byte G, byte B)[65536];
    public static (byte R, byte G, byte B)[] ColorScaleRotationLog = new (byte R, byte G, byte B)[65536];

    static PseudoBitmap()
    {
        #region スケールの初期化

        var gray = new (int I, byte R, byte G, byte B)[]{
            (0,0,0,0),
            (65536,255,255,255)
        };

        var cold_warm = new (int I, byte R, byte G, byte B)[]{
            (0,0,0,0),
            (5376,0,0,255),
            (16256,0,191,191),
            (27136,0,255,0),
            (38016,255,255,0),
            (48896,255,0,0),
            (59904,255,0,255),
            (65536,255,255,255)
        };

        var fire = new (int I, byte R, byte G, byte B)[]{
            (0,0,0,0),
            (3840, 0,0,91),
            (7936,46,0,160),
            (12032,94,0,216),
            (14336,122,0,227),
            (16128,143,0, 212),
            (20224,171,0,154),
            (24320,193,0,96),
            (24576,195,0,93),
            (24832,196,1,89),
            (27392,210,21,53),
            (32512,238,76,0),
            (36608,254,115,0),
            (36864,255,117,0),
            (40704,255, 145,0),
            (45312,255,176, 0 ),
            (48896,255,203, 0 ),
            (53248,255,234, 0 ),
            (57088,255,254,90 ),
            (57344,255,255,98 ),
            (61184,255,255, 215 ),
            (65536,255,255,255)
        };

        var spectrum = new (int I, byte R, byte G, byte B)[]{
            (0,255,0,0),
            (10752,255,252,0),
            (11008,252,255,0),
            (21760,0,255,0),
            (32512,0,255,252),
            (32768,0,252,255),
            (43520,0,0,255),
            (54272,252,0,255),
            (54528,255,0,252),
            (65536,255, 0, 0),
        };

        var rotation = new (int I, byte R, byte G, byte B)[]{
            (0,     80, 80,    255),       //0
            (10922, 80,    255,    255),           //60°
            (21845, 80,    255,    80),            //120°
            (32767, 255, 255,    80),     //180°
            (43690, 255, 80,    80),         //240°
            (54613, 255, 80,    255),         //300°
            (65536, 80, 80,    255),               //360°

        };

        (byte R, byte G, byte B)[] setScaleLinear((int I, byte R, byte G, byte B)[] src)
        {
            var result = new (byte R, byte G, byte B)[65536];
            for (int j = 0; j < src.Length - 1; j++)
            {
                (int I, byte R, byte G, byte B) a = src[j], b = src[j + 1];
                for (int k = src[j].I; k < src[j + 1].I; k++)
                {

                    var x = (double)(b.I - k) / (b.I - a.I);
                    result[k] = ((byte)(a.R * x + b.R * (1 - x)), (byte)(a.G * x + b.G * (1 - x)), (byte)(a.B * x + b.B * (1 - x)));
                }
            }
            return result;
        }

        (byte R, byte G, byte B)[] setScaleLog((int I, byte R, byte G, byte B)[] src)
        {
            var result = new (byte R, byte G, byte B)[65536];
            for (int i = 0; i < 65536; i++)
            {
                var k = (int)(Math.Log(i, 65536) * 65536);
                for (int j = 0; j < src.Length - 1; j++)
                    if (src[j].I < k && src[j + 1].I >= k)
                    {
                        (int I, byte R, byte G, byte B) a = src[j], b = src[j + 1];
                        double x = (double)(b.I - k) / (b.I - a.I);
                        result[i] = ((byte)(a.R * x + b.R * (1 - x)), (byte)(a.G * x + b.G * (1 - x)), (byte)(a.B * x + b.B * (1 - x)));
                        break;
                    }
            }
            return result;
        }

        ColorScaleGrayLiner = setScaleLinear(gray);
        ColorScaleColdWarmLiner = setScaleLinear(cold_warm);
        ColorScaleFireLiner = setScaleLinear(fire);
        ColorScaleRotationLiner = setScaleLinear(rotation);
        ColorScaleSpectrumLiner = setScaleLinear(spectrum);

        ColorScaleGrayLog = setScaleLog(gray);
        ColorScaleColdWarmLog = setScaleLog(cold_warm);
        ColorScaleFireLog = setScaleLog(fire);
        ColorScaleRotationLog = setScaleLog(rotation);
        ColorScaleSpectrumLog = setScaleLog(spectrum);

        #endregion スケールの初期化
    }
    #endregion 


    #region コンストラクタ

    public PseudoBitmap()
    {
        ColorScale = ColorScaleGrayLiner;
    }

    /// <summary>
    /// List<uint>型がソースデータであるグレー実画像のコンストラクタ
    /// </summary>
    /// <param name="valueGray"></param>
    /// <param name="bitsPerPixels"></param>
    /// <param name="width"></param>
    /// <param name="scaleR"></param>
    /// <param name="scaleG"></param>
    /// <param name="scaleB"></param>
    /// <param name="normarize"></param>
    /// <param name="realImage"></param>
    public PseudoBitmap(List<uint> valueGray, int width, byte[] scaleR = null, byte[] scaleG = null, byte[] scaleB = null, bool realImage = true)
        : this(valueGray.Select(a => (double)a).ToArray(), width, scaleR, scaleG, scaleB, realImage)
    { }

    /// <summary>
    /// List<uint>型がソースデータであるグレー実画像のコンストラクタ
    /// </summary>
    /// <param name="valueGray"></param>
    /// <param name="scale"></param>
    /// <param name="width"></param>
    /// <param name="scaleR"></param>
    /// <param name="scaleG"></param>
    /// <param name="scaleB"></param>
    /// <param name="realImage"></param>
    public PseudoBitmap(List<uint> valueGray, double scale, int width, byte[] scaleR = null, byte[] scaleG = null, byte[] scaleB = null, bool realImage = true)
      : this(valueGray.Select(a => a * scale).ToArray(), width, scaleR, scaleG, scaleB, realImage)
    {
    }

    /// <summary>
    /// グレーの実画像を描画する.
    /// </summary>
    /// <param name="valueR"></param>
    /// <param name="valueG"></param>
    /// <param name="valueB"></param>
    /// <param name="width"></param>
    public PseudoBitmap(uint[] value, int width, byte[] scaleR = null, byte[] scaleG = null, byte[] scaleB = null, bool realImage = true)
        : this(value.Select(a => (double)a).ToArray(), width, scaleR, scaleG, scaleB, realImage)
    { }

    /// <summary>
    /// double型がソースデータであるグレー実画像のコンストラクタ
    /// </summary>
    /// <param name="values"></param>
    /// <param name="width"></param>
    /// <param name="scaleR"></param>
    /// <param name="scaleG"></param>
    /// <param name="scaleB"></param>
    /// <param name="realImage"></param>
    public PseudoBitmap(double[] values, int width, byte[] scaleR, byte[] scaleG, byte[] scaleB, bool realImage = true)
    {
        if (scaleR == null || scaleG == null || scaleB == null || scaleR.Length != scaleG.Length || scaleG.Length != scaleB.Length || scaleR.Length < 2)
            constructor(values, width, null, realImage);
        else
        {
            var scale = new (byte R, byte G, byte B)[scaleR.Length];
            for (int i = 0; i < scale.Length; i++)
                scale[i] = (scaleR[i], scaleG[i], scaleB[i]);
            constructor(values, width, scale, realImage);
        }
    }

    /// <summary>
    /// double型がソースデータであるグレー実画像のコンストラクタ
    /// </summary>
    /// <param name="values"></param>
    /// <param name="width"></param>
    /// <param name="scaleR"></param>
    /// <param name="scaleG"></param>
    /// <param name="scaleB"></param>
    /// <param name="realImage"></param>
    public PseudoBitmap(double[] values, int width, (byte R, byte G, byte B)[] scale = null, bool realImage = true)
    {
        constructor(values, width, scale, realImage);
    }

    private void constructor(double[] values, int width, (byte R, byte G, byte B)[] scale = null, bool realImage = true)
    {
        if (values.Length == 0 || values.Length % width != 0) throw new ArgumentNullException("Invalid input");
        Width = width;
        Height = values.Length / width;

        GrayScale = true;
        RealImage = true;

        SrcValuesGrayOriginal = SrcValuesGray = values;

        IsSrcGray = true;

        SrcValuesR.Clear();
        SrcValuesG = SrcValuesB = SrcValuesR;

        if (scale == null || scale.Length < 2)
            ColorScale = ColorScaleGrayLiner;
        else
        {
            ColorScale = scale;
        }


        initFilter();
    }


    #endregion コンストラクタ

    #region 初期化関連

    private static Complex[][] initComplex(int width, int height)
    {
        Complex[][] complex = new Complex[height][];
        for (int i = 0; i < height; i++)
        {
            complex[i] = new Complex[width];
            for (int j = 0; j < width; j++)
                complex[i][j] = new Complex(0, 0);
        }
        return complex;
    }

    public void initFilter()
    {
        //if (Filter1.Count != Height * Width)
        //{
        //    Filter1 = Enumerable.Repeat(false, Height * Width).ToList();
        //    Filter2 = Enumerable.Repeat(false, Height * Width).ToList();
        //    Filter3 = Enumerable.Repeat(false, Height * Width).ToList();
        //    Filter4 = Enumerable.Repeat(false, Height * Width).ToList();
        //    Filter5 = Enumerable.Repeat(false, Height * Width).ToList();
        //    FilterTemporary = Enumerable.Repeat(false, Height * Width).ToList();
        //    if (Height * Width < 3001 * 3001)
        //        FFT_Filter= Enumerable.Repeat(0f, Height * Width).ToList();
        //}
        //else
        //{
        //    for (int i = 0; i < Height * Width; i++)
        //    {
        //        if (Height * Width < 3001 * 3001)
        //            FFT_Filter[i] = 0;
        //        FilterTemporary[i] = false;
        //        Filter1[i] = false;
        //        Filter2[i] = false;
        //        Filter3[i] = false;
        //        Filter4[i] = false;
        //        Filter5[i] = false;
        //    }
        //}
        Filter1.Clear();
        Filter1.AddRange(new bool[Height * Width]);

        Filter2.Clear();
        Filter2.AddRange(new bool[Height * Width]);

        Filter3.Clear();
        Filter3.AddRange(new bool[Height * Width]);

        Filter4.Clear();
        Filter4.AddRange(new bool[Height * Width]);

        Filter5.Clear();
        Filter5.AddRange(new bool[Height * Width]);

        FilterTemporary.Clear();
        FilterTemporary.AddRange(new bool[Height * Width]);

        if (Height * Width < 3001 * 3001)
            FFT_Filter = Enumerable.Repeat(0f, Height * Width).ToList();

    }

    #endregion 初期化関連

    #region プロパティ

    /// <summary>
    /// グレイスケールモードの時、SrcValuesGrayOriginalをSrcValuesGrayとは異なる配列として確保する。newの直後に設定しないといけない。
    /// </summary>
    public bool ReserveSrcValuesGrayOriginal
    {
        set
        {
            if (value)
            {
                SrcValuesGrayOriginal = new double[SrcValuesGray.Length];
                Array.Copy(SrcValuesGray, SrcValuesGrayOriginal, SrcValuesGray.Length);
            }
            else
            {
                SrcValuesGrayOriginal = SrcValuesGray;
            }
        }
    }

    /// <summary>
    /// アルファチャンネルが有効かどうか. FilterAlphaもセットしないと、機能しない.
    /// </summary>
    public bool AlphaEnabled { set; get; } = false;

    /// <summary>
    /// 上下方向の反転をするかどうか
    /// </summary>
    public bool VerticalFlip { set; get; } = false;

    /// <summary>
    /// 左右方向の反転をするかどうか
    /// </summary>
    public bool HorizontalFlip { set; get; } = false;

    public enum Scales { GrayLinear, ColdWarmLinear, RotationLinear, SpectrumLinear, FireLinear, GrayLog, ColdWarmLog, RotationLog, SpectrumLog, FireLog, }

    private Scales scale = Scales.GrayLinear;
    public Scales Scale
    {
        get => scale;
        set
        {
            scale = value;
            if (scale == Scales.GrayLinear)
                SetScaleGray(true);
            else if (scale == Scales.GrayLog)
                SetScaleGray(false);
            else if (scale == Scales.ColdWarmLinear)
                SetScaleColdWarm(true);
            else if (scale == Scales.ColdWarmLog)
                SetScaleColdWarm(false);
            else if (scale == Scales.RotationLinear)
                SetScaleRotation();
            else if (scale == Scales.SpectrumLinear)
                SetScaleSpectrum(true);
            else if (scale == Scales.SpectrumLog)
                SetScaleSpectrum(false);
            else if (scale == Scales.FireLinear)
                SetScaleFire(true);
            else if (scale == Scales.FireLog)
                SetScaleFire(false);
        }
    }




    public bool RealImage = true;

    public PointF Center;//虚画像のときの中心ピクセル位置

    /// <summary>
    /// Imageの幅
    /// </summary>
    public int Width { get; set; }

    public int Height { get; set; }//高さ

    public Complex[][] ComplexGray;//元画像の複素数;
    public Complex[][] ComplexR;//元画像Rの複素数;
    public Complex[][] ComplexG;//元画像Gの複素数;
    public Complex[][] ComplexB;//元画像Bの複素数;

    public List<float> FFT_Filter = new();
    public List<bool> FilterTemporary = new();
    public List<bool> Filter1 = new();
    public List<bool> Filter2 = new();
    public List<bool> Filter3 = new();
    public List<bool> Filter4 = new();
    public List<bool> Filter5 = new();
    public bool Filter1Visible = true;
    public bool Filter2Visible = true;
    public bool Filter3Visible = true;
    public bool Filter4Visible = true;
    public bool Filter5Visible = true;

    /// <summary>
    /// 透明度フィルター. 0が透明、255が不透明. AlphaEnabledがTrueの時だけ使われる.
    /// </summary>
    public List<byte> FilterAlfha { get; set; } = new List<byte>();

    public (byte R, byte G, byte B)[] ColorScale;

    public List<uint> SrcValuesR = new();
    public List<uint> SrcValuesG = new();
    public List<uint> SrcValuesB = new();

    /// <summary>
    /// グレースケールの画像データ (BlurModeが有効の時は、フィルター後のデータ)
    /// </summary>
    public double[] SrcValuesGray;

    /// <summary>
    /// グレースケールのもとの画像データ
    /// </summary>
    public double[] SrcValuesGrayOriginal;

    /// <summary>
    /// Blurモードの列挙体
    /// </summary>
    public enum BlurModeEnum { Gaussian, Lorentzian, None }

    private enum ImageSouceEnum { GrayDouble, GrayUint, ColorDouble, ColorUint }

    /// <summary>
    /// グレイスケールとして表示するかどうか　(ソースがグレースケールかどうかは無関係)
    /// </summary>
    public bool GrayScale;

    /// <summary>
    /// ソースがグレースケールかどうか (グレースケールとして表示するかどうかは無関係)
    /// </summary>
    public bool IsSrcGray = false;

    public UniversalConstants.LengthUnit Unit = UniversalConstants.LengthUnit.mm;
    public double PixelSizeX = 1;//単位はunit
    public double PixelSizeY = 1;

    /// <summary>
    /// 表示する強度の上限 (画像中の最大強度という意味ではない）
    /// </summary>
    public double MaxValue = 255;

    /// <summary>
    /// 表示する強度の下限 (画像中の最大強度という意味ではない）
    /// </summary>
    public double MinValue = 0;

    //public int BitsPerPixels = 8;

    public bool IsNegative = false;

    public Bitmap SrcBitmap;//実画像のときの元画像

    /// <summary>
    /// 画像に関連付けた情報 (なんでも)
    /// </summary>
    public object Tag { get; set; }

    public Profile FrequencyProfile { get; set; }

    /// <summary>
    /// 1ピクセルが相当する数値 (単位は PixelUnitで指定)
    /// </summary>
    public double PixelScale;

    /// <summary>
    /// ピクセルの単位
    /// </summary>
    public PixelUnitEnum PixelUnit;

    #endregion プロパティ

    #region 操作

    /// <summary>
    /// オリジナルのピクセル強度データにリセットする。Grayスケールモードの時だけ有効。
    /// </summary>
    public void SetOriginalGray()
    {
        if (IsSrcGray)
            SrcValuesGray = SrcValuesGrayOriginal;
    }

    /// <summary>
    /// ダスト＆スクラッチを施す。Grayスケールモードの時だけ有効。
    /// </summary>
    /// <param name="radius"></param>
    /// <param name="threshold"></param>
    /// <param name="applyToOriginal"></param>
    public void SetDustAndScratches(double radius, double threshold, bool applyToOriginal = true)
    {
        if (IsSrcGray)
        {
            if (applyToOriginal)
                SrcValuesGray = ImageProcess.DustAndScratches(SrcValuesGrayOriginal, Width, radius, threshold);
            else
                SrcValuesGray = ImageProcess.DustAndScratches(SrcValuesGray, Width, radius, threshold);
        }
    }

    /// <summary>
    /// ガウシアンBlurを施す。Grayスケールモードの時だけ有効。
    /// </summary>
    /// <param name="hwhm">半値半幅</param>
    /// <param name="mode"></param>
    /// <param name="applyToOriginal"></param>
    public void SetBlurImage(double hwhm, BlurModeEnum mode, bool applyToOriginal = true)
    {
        if (IsSrcGray)
        {
            if (mode == BlurModeEnum.Gaussian)
            {
                if (applyToOriginal)
                    SrcValuesGray = ImageProcess.GaussianBlurFast(SrcValuesGrayOriginal, Width, hwhm);
                else
                    SrcValuesGray = ImageProcess.GaussianBlurFast(SrcValuesGray, Width, hwhm);
            }
            else
                SrcValuesGray = SrcValuesGrayOriginal;
        }
    }

    /// <summary>
    /// 指定されたピクセルのカラー値を返す
    /// </summary>
    /// <param name="x">xピクセル値</param>
    /// <param name="y">yピクセル値</param>
    /// <returns></returns>
    public Color GetPixelColor(int x, int y)
    {
        if (x < 0 || Width <= x || y < 0 || Height <= y || this.RealImage == false) return Color.FromArgb(0, 0, 0);

        var offset = x + (y * Width);
        var index = Math.Max(Math.Min((int)((double)(SrcValuesR[offset] - MinValue) / (MaxValue - MinValue) * ColorScale.Length + 0.5), ColorScale.Length - 1), 0);
        return Color.FromArgb(ColorScale[index].R, ColorScale[index].G, ColorScale[index].B);
    }

    public Color GetPixelColor(double x, double y) => GetPixelColor((int)(x + 0.5), (int)(y + 0.5));

    public double GetPixelRawValue(PointD pt) => GetPixelRawValue(pt.X, pt.Y);


    public void SetScaleColdWarm(bool linear = true)
    {
        if (linear)
            ColorScale = ColorScaleColdWarmLiner;
        else
            ColorScale = ColorScaleColdWarmLog;

        GrayScale = false;
    }

    public void SetScaleSpectrum(bool linear = true)
    {
        if (linear)
            ColorScale = ColorScaleSpectrumLiner;
        else
            ColorScale = ColorScaleSpectrumLog;

        GrayScale = false;
    }
    public void SetScaleFire(bool linear = true)
    {
        if (linear)
            ColorScale = ColorScaleFireLiner;
        else
            ColorScale = ColorScaleFireLog;

        GrayScale = false;
    }

    public void SetScaleGray(bool linear = true)
    {
        if (linear)
            ColorScale = ColorScaleGrayLiner;
        else
            ColorScale = ColorScaleGrayLog;
        GrayScale = false;
    }

    public void SetScaleRotation()
    {
        ColorScale = ColorScaleRotationLiner;
        GrayScale = false;
    }



    /// <summary>
    /// グレースケールの時のx, y位置の生の強度を返す. グレースケールではなかったら常に0を返す.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public double GetPixelRawValue(double x, double y) => GetPixelRawValue((int)(x + 0.5), (int)(y + 0.5));

    /// <summary>
    /// グレースケールの時のx, y位置の生の強度を返す グレースケールではなかったら常に0を返す.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public double GetPixelRawValue(int x, int y)
    {
        int offset = x + y * Width;
        if (IsSrcGray == true && SrcValuesGray != null && offset < SrcValuesGray.Length && 0 <= offset)
            return SrcValuesGray[offset];
        else
            return 0;
    }

    /// <summary>
    /// 指定されたピクセルの複素数値を返す
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Complex[] GetPixelComplex(int x, int y)
    {
        if (x < 0 || Width <= x || y < 0 || Height <= y || this.RealImage == true) return new Complex[] { new Complex(), new Complex(), new Complex() };

        if (this.GrayScale == true)
            return new Complex[] { ComplexGray[y][x], ComplexGray[y][x], ComplexGray[y][x] };
        else
            return new Complex[] { ComplexR[y][x], ComplexG[y][x], ComplexB[y][x] };
    }

    public Complex[] GetPixelComplex(double x, double y) => GetPixelComplex((int)Math.Round(x), (int)Math.Round(y));

    /// <summary>
    /// 等倍のイメージを取得する
    /// </summary>
    /// <param name="srcCenter"></param>
    /// <param name="zoom"></param>
    /// <param name="destSize"></param>
    /// <returns></returns>
    public Bitmap GetImage()
    {
        var bmp = GetImage(new RectangleD(0, 0, Width, Height), new Size(Width, Height));
        return bmp;
    }

    /// <summary>
    /// 指定された範囲のイメージを取得する
    /// </summary>
    /// <param name="srcCenter"></param>
    /// <param name="zoom"></param>
    /// <param name="destSize"></param>
    /// <returns></returns>
    public Bitmap GetImage(PointD srcCenter, double zoom, Size destSize) => srcCenter.IsNaN
        ? null
        : GetImage(GetDrawingArea(srcCenter, zoom, destSize), destSize);

    /// <summary>
    /// 描画範囲を得る
    /// </summary>
    /// <param name="srcCenter"></param>
    /// <param name="zoom"></param>
    /// <param name="destSize"></param>
    /// <returns></returns>
    public static RectangleD GetDrawingArea(PointD srcCenter, double zoom, Size destSize)
        => new RectangleD(srcCenter.X - destSize.Width / zoom / 2.0, srcCenter.Y - destSize.Height / zoom / 2.0, destSize.Width / zoom, destSize.Height / zoom);

    private Bitmap destBmp;

    public delegate Point GetSrcPosition(int x, int y);

    public delegate byte GetValue(double value, byte[] scale);

    private RectangleD justBeforeSrcRect = new RectangleD();
    private Size justBeforeDestSize = new Size();

    /// <summary>
    /// 指定された範囲のイメージを取得する
    /// </summary>
    /// <param name="srcRect"></param>
    /// <param name="destSize"></param>
    /// <returns></returns>
    public unsafe Bitmap GetImage(RectangleD srcRect, Size destSize)
    {
        if (Height == 0 || Width == 0 || srcRect.Width == 0 || srcRect.Height == 0 || destSize.Width == 0 || destSize.Height == 0) return null;

        int width = destSize.Width, height = destSize.Height;

        //まずbmpが前回作られていなかったら作成
        if (destBmp == null || destBmp.Width != width || destBmp.Height != height || destBmp.PixelFormat == (AlphaEnabled ? PixelFormat.Format32bppPArgb : PixelFormat.Format24bppRgb))
        {
            destBmp = new Bitmap(width, height, AlphaEnabled ? PixelFormat.Format32bppPArgb : PixelFormat.Format24bppRgb);
        }
        //bmpをロック
        BitmapData bmpData;
        try
        {
            bmpData = destBmp.LockBits(new Rectangle(0, 0, destBmp.Width, destBmp.Height), ImageLockMode.ReadWrite, destBmp.PixelFormat);
        }
        catch
        {
            return null;
        }
        double zoom = (width / srcRect.Width + height / srcRect.Height) / 2.0;

        # region 描画する画素位置をソース画像位置に変換するローカル関数
        double srcX = srcRect.X + 0.5, srcY = srcRect.Y + 0.5, w = srcRect.Width / width, h = srcRect.Height / height;
        Func<int, int, (int X, int Y)> getSrcPosition;
        if (HorizontalFlip & VerticalFlip)
            getSrcPosition = (x, y) => ((int)(srcX + (width - x) * w), (int)(srcY + (height - y) * h));
        else if (!HorizontalFlip & VerticalFlip)
            getSrcPosition = (x, y) => ((int)(srcX + x * w), (int)(srcY + (height - y) * h));
        else if (HorizontalFlip & !VerticalFlip)
            getSrcPosition = (x, y) => ((int)(srcX + (width - x) * w), (int)(srcY + y * h));
        else
            getSrcPosition = (x, y) => ((int)(srcX + x * w), (int)(srcY + y * h));
        #endregion

        #region  入力値doubleを表示する値に変換するローカル関数
        var length = ColorScale.Length;
        var coeff = length / (MaxValue - MinValue);
        byte getValueR(double rawValue)
        {
            var rawIndex = (int)((rawValue - MinValue) * coeff + 0.5);
            return ColorScale[Math.Min(Math.Max(0, rawIndex), length - 1)].R;
        }

        byte getValueG(double rawValue)
        {
            var rawIndex = (int)((rawValue - MinValue) * coeff + 0.5);
            return ColorScale[Math.Min(Math.Max(0, rawIndex), length - 1)].G;
        }

        byte getValueB(double rawValue)
        {
            var rawIndex = (int)((rawValue - MinValue) * coeff + 0.5);
            return ColorScale[Math.Min(Math.Max(0, rawIndex), length - 1)].B;
        }
        #endregion

        int range = (int)(1 / zoom / 2 + 0.5);
        int increase = (zoom >= 1) ? 0 : Math.Max(range / 3, 1);

        int step = AlphaEnabled ? 4 : 3;
        int nResidual = bmpData.Stride - destBmp.Width * step;
        double filter;

        int thread = Environment.ProcessorCount;


#if DEBUG
        //thread = 1;
#endif
        int yStep = height / thread + 1;
        Parallel.For(0, thread, t =>
        {
            byte* p = (byte*)(void*)bmpData.Scan0;
            p += t * yStep * bmpData.Stride;

            byte r = 0, g = 0, b = 0;

            (int X, int Y) beforePt = (int.MinValue, int.MinValue);
            for (int y = t * yStep; y < Math.Min((t + 1) * yStep, height); y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var pt = getSrcPosition(x, y);
                    if (pt.X == beforePt.X && pt.Y == beforePt.Y)
                    {
                        for (int i = 0; i < step; i++)
                            p[i] = p[i - step];
                    }
                    else if ((uint)pt.X < (uint)Width && (uint)pt.Y < (uint)Height)//描画域内のとき
                    {
                        var srcPosition = pt.X + pt.Y * Width;
                        if (increase > 0)//補完モード
                        {
                            double rSum = 0, gSum = 0, bSum = 0, count = 0;

                            for (int j = Math.Max(0, pt.Y - range); j <= Math.Min(pt.Y + range, Height - 1); j += increase)
                                for (int i = Math.Max(0, pt.X - range); i <= Math.Min(pt.X + range, Width - 1); i += increase)
                                {
                                    var tempPosition = i + j * Width;

                                    if (IsSrcGray)
                                        bSum += SrcValuesGray[tempPosition];
                                    else
                                    {
                                        bSum += SrcValuesB[tempPosition];
                                        if (!GrayScale)
                                        {
                                            gSum += SrcValuesG[tempPosition];
                                            rSum += SrcValuesR[tempPosition];
                                        }
                                    }
                                    count++;
                                }
                            if (count > 0)
                            {
                                b = getValueB(bSum / count);
                                if (GrayScale)
                                    g = r = b;
                                else
                                {
                                    if (IsSrcGray)
                                        gSum = rSum = bSum;
                                    g = getValueG(gSum / count);
                                    r = getValueR(rSum / count);
                                }
                            }
                        }
                        else
                        {
                            if (IsSrcGray)
                            {
                                b = getValueB(SrcValuesGray[srcPosition]);
                                if (GrayScale)
                                    g = r = b;
                                else
                                {
                                    g = getValueG(SrcValuesGray[srcPosition]);
                                    r = getValueR(SrcValuesGray[srcPosition]);
                                }
                            }
                            else
                            {
                                b = getValueB(SrcValuesB[srcPosition]);
                                if (GrayScale)
                                    g = r = b;
                                else
                                {
                                    g = getValueG(SrcValuesG[srcPosition]);
                                    r = getValueR(SrcValuesR[srcPosition]);
                                }
                            }
                        }

                        if (IsNegative)
                        {
                            b = (byte)(byte.MaxValue - b); g = (byte)(byte.MaxValue - g); r = (byte)(byte.MaxValue - r);
                        }

                        p[0] = b;
                        p[1] = g;
                        p[2] = r;

                        if (AlphaEnabled)
                            p[3] = (srcPosition < FilterAlfha.Count) ? FilterAlfha[srcPosition] : (byte)255;

                        #region 各種フィルター
                        if (FFT_Filter != null && FFT_Filter.Count > 0 && (filter = FFT_Filter[srcPosition]) != 0)//もしフィルターされていたら全体を1/2にして(0,127,127)をたす
                        { //BG
                            p[0] = (byte)(p[0] * (2 - filter) * 0.5 + 127 * filter);
                            p[1] = (byte)(p[1] * (2 - filter) * 0.5 + 127 * filter);
                            p[2] = (byte)(p[2] * (2 - filter) * 0.5);
                        }

                        if (FilterTemporary != null && FilterTemporary.Count > 0 && FilterTemporary[srcPosition])//もし一時フィルターされていたら全体を1/2にして(127,127,0)をたす
                        {//GR
                            p[0] = (byte)(p[0] * 0.8);
                            p[1] = (byte)(p[1] * 0.8 + 51);
                            p[2] = (byte)(p[2] * 0.8 + 51);
                        }
                        if (Filter1Visible && Filter1 != null && Filter1[srcPosition])//もし飽和点にいろをつけるなら
                        {//R
                            p[0] = (byte)(p[0] * 0.8);
                            p[1] = (byte)(p[1] * 0.8);
                            p[2] = (byte)(p[2] * 0.8 + 51);
                        }
                        if (Filter2Visible && Filter2 != null && Filter2[srcPosition])
                        {//B
                            p[0] = (byte)(p[0] * 0.8 + 51);
                            p[1] = (byte)(p[1] * 0.8);
                            p[2] = (byte)(p[2] * 0.8);
                        }
                        if (Filter3Visible && Filter3 != null && Filter3[srcPosition])//もしスポットだったら全体を1/2にして(0,127,127)をたす
                        {//BG
                            p[0] = (byte)(p[0] * 0.8 + 51);
                            p[1] = (byte)(p[1] * 0.8 + 51);
                            p[2] = (byte)(p[2] * 0.8);
                        }
                        if (Filter4Visible && Filter4 != null && Filter4[srcPosition])//もし範囲外だったら全体を2/3にして(0,85,0)をたす
                        {//G
                            p[0] = (byte)(p[0] * 0.8);
                            p[1] = (byte)(p[1] * 0.8 + 51);
                            p[2] = (byte)(p[2] * 0.8);
                        }
                        if (Filter5Visible && Filter5 != null && Filter5[srcPosition])//もし範囲外だったら全体を2/3にして(0,85,0)をたす
                        {//BR
                            p[0] = (byte)(p[0] * 0.8 + 51);
                            p[1] = (byte)(p[2] * 0.8);
                            p[2] = (byte)(p[1] * 0.8 + 51);
                        }
                        #endregion

                    }
                    else//描画域がはみ出したとき
                    {
                        p[0] = 0; p[1] = 127; p[2] = 0;
                        if (AlphaEnabled)
                            p[3] = (byte)255;
                    }

                    p += step;

                    beforePt = pt;
                }
                p += nResidual;
            }
        });

        FilterTemporary.Clear();
        FilterTemporary.AddRange(new bool[Filter1.Count]);

        destBmp.UnlockBits(bmpData);

        justBeforeDestSize = destSize;
        justBeforeSrcRect = srcRect;
        return destBmp;
    }

    /// <summary>
    /// 実画像モードのとき回転操作を施す
    /// </summary>
    /// <param name="bmp"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public void Rotate(double angle)
    {
        Bitmap destBitmap = new Bitmap(SrcBitmap.Width, SrcBitmap.Height, PixelFormat.Format24bppRgb);
        Graphics g = Graphics.FromImage(destBitmap);
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        g.TranslateTransform((float)SrcBitmap.Width / 2, (float)SrcBitmap.Height / 2);
        g.RotateTransform((float)(angle * 180 / Math.PI));
        g.TranslateTransform(-(float)SrcBitmap.Width / 2, -(float)SrcBitmap.Height / 2);
        g.Clear(Color.White);
        g.DrawImage(SrcBitmap, new RectangleF(0, 0, SrcBitmap.Width, SrcBitmap.Height), new RectangleF(0, 0, SrcBitmap.Width, SrcBitmap.Height), GraphicsUnit.Pixel);

        //SourceBitmapをロックする
        BitmapData bitmapData = destBitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, SrcBitmap.PixelFormat);
        //SouceStrideを設定しておく
        int BitmapStride = bitmapData.Stride;
        //次にその情報をargbValuesに転送
        byte[] bitmapByteArray = new byte[bitmapData.Stride * SrcBitmap.Height];
        Marshal.Copy(bitmapData.Scan0, bitmapByteArray, 0, bitmapByteArray.Length);
        //SourceBitmapをアンロックして終了
        destBitmap.UnlockBits(bitmapData);

        if (GrayScale)
        {
            SrcValuesR.Clear();

            for (int yInit = 0; yInit < Height * bitmapData.Stride; yInit += BitmapStride)
                for (int x = 0; x < Width; x++)
                    SrcValuesR.Add(bitmapByteArray[yInit + x * 3]);
            SrcValuesG = SrcValuesR;
            SrcValuesB = SrcValuesR;
        }
        else
        {
            SrcValuesR.Clear(); ;
            SrcValuesG.Clear();
            SrcValuesB.Clear();
            for (int yInit = 0; yInit < Height * bitmapData.Stride; yInit += BitmapStride)
                for (int x = 0; x < Width; x++)
                {
                    SrcValuesR.Add(bitmapByteArray[yInit + x * 3 + 2]);
                    SrcValuesG.Add(bitmapByteArray[yInit + x * 3 + 1]);
                    SrcValuesB.Add(bitmapByteArray[yInit + x * 3 + 0]);
                }
        }
        bitmapByteArray = null;
    }



    #endregion 操作
}