using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Crystallography
{
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

        /// <summary>
        /// 上下方向の反転をするかどうか
        /// </summary>
        public bool VerticalFlip { set; get; } = false;

        /// <summary>
        /// 左右方向の反転をするかどうか
        /// </summary>
        public bool HorizontalFlip { set; get; } = false;

        static PseudoBitmap()
        {
            #region スケールの初期化

            int[][] cold_warm = new int[][]{
                new int[]{0,0,0,0},
                new int[]{5376,0,0,255},
                new int[]{16256,0,191,191},
                new int[]{27136,0,255,0},
                new int[]{38016,255,255,0},
                new int[]{48896,255,0,0},
                new int[]{59904,255,0,255},
                new int[]{65535,255,255,255}
            };

            int[][] rotation = new int[][]{
                new int[]{0,            80, 80,    255},       //0
                new int[]{10922, 80,    255,    255},       //60°
                new int[]{21845, 80,    255,    80},   //120°
                new int[]{32767,        255,    255,    80},     //180°
                new int[]{43690,        255, 80,    80},         //240°
                new int[]{54613,        255, 80,    255},         //300°
                new int[]{65535, 80, 80,    255},       //360°

            };

            for (int i = 0; i < 65536; i++)
            {
                if (i == 0)
                {
                    BrightnessScaleLog[0] = 0;
                    BrightnessScaleLiner[0] = 0;
                }
                else
                {
                    BrightnessScaleLiner[i] = (byte)(i / 256);//リニア
                    BrightnessScaleLog[i] = (byte)(Math.Log(i, 65536) * 256);//ログ用 65536が255になるように調節
                }
                int k = i;
                for (int j = 0; j < cold_warm.Length-1; j++)
                    if (cold_warm[j][0] < k && cold_warm[j + 1][0] >= k)
                    {
                        double a1 = (double)(cold_warm[j + 1][0] - k) / (cold_warm[j + 1][0] - cold_warm[j][0]), a2 = (double)(k - cold_warm[j][0]) / (cold_warm[j + 1][0] - cold_warm[j][0]);
                        BrightnessScaleLinerColorR[i] = (byte)(cold_warm[j][1] * a1 + cold_warm[j + 1][1] * a2);
                        BrightnessScaleLinerColorG[i] = (byte)(cold_warm[j][2] * a1 + cold_warm[j + 1][2] * a2);
                        BrightnessScaleLinerColorB[i] = (byte)(cold_warm[j][3] * a1 + cold_warm[j + 1][3] * a2);
                    }

                for (int j = 0; j < rotation.Length - 1; j++)
                    if (rotation[j][0] == k)
                    {
                        BrightnessScaleLinerRotationR[i] = (byte)(rotation[j][1]);
                        BrightnessScaleLinerRotationG[i] = (byte)(rotation[j][2]);
                        BrightnessScaleLinerRotationB[i] = (byte)(rotation[j][3]);
                    }
                    else if (rotation[j][0] < k && rotation[j + 1][0] >= k)
                    {
                        double a1 = (double)(rotation[j + 1][0] - k) / (rotation[j + 1][0] - rotation[j][0]),
                            a2 = (double)(k - rotation[j][0]) / (rotation[j + 1][0] - rotation[j][0]);
                        BrightnessScaleLinerRotationR[i] = (byte)(rotation[j][1] * a1 + rotation[j + 1][1] * a2);
                        BrightnessScaleLinerRotationG[i] = (byte)(rotation[j][2] * a1 + rotation[j + 1][2] * a2);
                        BrightnessScaleLinerRotationB[i] = (byte)(rotation[j][3] * a1 + rotation[j + 1][3] * a2);
                    }


                k = (int)(Math.Log(i, 65536) * 65536);
                for (int j = 0; j < cold_warm.Length - 1; j++)
                    if (cold_warm[j][0] < k && cold_warm[j + 1][0] >= k)
                    {
                        double a1 = (double)(cold_warm[j + 1][0] - k) / (cold_warm[j + 1][0] - cold_warm[j][0]), a2 = (double)(k - cold_warm[j][0]) / (cold_warm[j + 1][0] - cold_warm[j][0]);
                        BrightnessScaleLogColorR[i] = (byte)(cold_warm[j][1] * a1 + cold_warm[j + 1][1] * a2);
                        BrightnessScaleLogColorG[i] = (byte)(cold_warm[j][2] * a1 + cold_warm[j + 1][2] * a2);
                        BrightnessScaleLogColorB[i] = (byte)(cold_warm[j][3] * a1 + cold_warm[j + 1][3] * a2);
                    }
            }

            #endregion スケールの初期化
        }

        #region コンストラクタ

        public PseudoBitmap()
        {
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
        //  :this(valueGray.Select(a => a * scale).ToArray(),width,scaleR,scaleG,scaleB,realImage)
        {
            double[] values = new double[valueGray.Count];
            for (int i = 0; i < values.Length; i++)
                values[i] = valueGray[i] * scale;

            constructor(values, width, scaleR, scaleB, scaleG, realImage);
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
        public PseudoBitmap(double[] values, int width, byte[] scaleR = null, byte[] scaleG = null, byte[] scaleB = null, bool realImage = true)
        {
            constructor(values, width, scaleR, scaleB, scaleG, realImage);
            /*  if (values.Length == 0 || values.Length % width != 0) throw new ArgumentNullException("Invalid input");
              Width = width;
              Height = values.Length / width;

              GrayScale = true;
              RealImage = true;

              SrcValuesGrayOriginal = SrcValuesGray = values;

              IsSrcGray = true;

              SrcValuesR.Clear();
              SrcValuesG = SrcValuesB = SrcValuesR;

              ScaleR = scaleR != null ? scaleR : BrightnessScaleLiner;
              ScaleG = scaleG != null ? scaleG : BrightnessScaleLiner;
              ScaleB = scaleB != null ? scaleB : BrightnessScaleLiner;
              initFilter();
             * */
        }

        private void constructor(double[] values, int width, byte[] scaleR = null, byte[] scaleG = null, byte[] scaleB = null, bool realImage = true)
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

            ScaleR = scaleR ?? BrightnessScaleLiner;
            ScaleG = scaleG ?? BrightnessScaleLiner;
            ScaleB = scaleB ?? BrightnessScaleLiner;
            initFilter();
        }

        /// <summary>
        /// 32bitまでのカラーの実画像のコンストラクタ
        /// </summary>
        /// <param name="valueR"></param>
        /// <param name="valueG"></param>
        /// <param name="valueB"></param>
        /// <param name="width"></param>
        public PseudoBitmap(uint[] valueR, uint[] valueG, uint[] valueB, int width)
        {
            if (valueR.Length == 0 || valueR.Length % width != 0) return;
            Width = width;
            Height = valueR.Length / width;

            GrayScale = false;
            RealImage = true;

            IsSrcGray = false;

            SrcValuesR = new List<uint>(valueR);
            SrcValuesG = new List<uint>(valueG);
            SrcValuesB = new List<uint>(valueB);

            ScaleR = BrightnessScaleLiner;
            ScaleG = BrightnessScaleLiner;
            ScaleB = BrightnessScaleLiner;
            initFilter();
        }

        #region complex用のコンストラクタ (どこで使ってるんだっけ?)

        /// <summary>
        /// カラーケールcomplexを引数に取るコンストラクタ
        /// </summary>
        /// <param name="complex"></param>
        public PseudoBitmap(Complex[][] complexR, Complex[][] complexG, Complex[][] complexB, byte[] scaleR, byte[] scaleG, byte[] scaleB, bool normarize, bool realImage)
        {
            //すべてのcomplexがnullだったら例外をとばす
            if (complexR == null && complexG == null && complexB == null) throw new ArgumentNullException("Invalid input");

            //高さ、幅を設定
            if (complexR != null && complexR.Length > 0 && complexR[0].Length > 0)
            {
                Width = complexR[0].Length; Height = complexR.Length;
            }
            else if (complexG != null && complexG.Length > 0 && complexG[0].Length > 0)
            {
                Width = complexG[0].Length; Height = complexG.Length;
            }
            else if (complexB != null && complexB.Length > 0 && complexB[0].Length > 0)
            {
                Width = complexB[0].Length; Height = complexB.Length;
            }
            else throw new ArgumentNullException("Invalid input");
            Center = new PointF(Width / 2.0f, Height / 2.0f);

            GrayScale = false;
            RealImage = realImage;

            //null値が入っていた場合は0で初期化
            if (complexR == null) complexR = initComplex(Width, Height);
            if (complexG == null) complexG = initComplex(Width, Height);
            if (complexB == null) complexB = initComplex(Width, Height);

            ComplexR = complexR;
            ComplexG = complexG;
            ComplexB = complexB;

            //三個をまとめたModulas
            double[][][] modulas = Fourier.GetModulus(new Complex[][][] { complexR, complexG, complexB }, normarize);

            SrcValuesR.Clear();
            SrcValuesG.Clear();
            SrcValuesB.Clear();
            int n = 0;
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                {
                    if (!normarize)
                    {
                        SrcValuesR.Add(Math.Max(Math.Min((ushort)modulas[0][y][x], (ushort)255), (ushort)0));
                        SrcValuesG.Add(Math.Max(Math.Min((ushort)modulas[1][y][x], (ushort)255), (ushort)0));
                        SrcValuesB.Add(Math.Max(Math.Min((ushort)modulas[2][y][x], (ushort)255), (ushort)0));
                    }
                    else
                    {
                        SrcValuesR.Add(Math.Max(Math.Min((ushort)(modulas[0][y][x] * scaleR.Length), (ushort)(scaleR.Length - 1)), (ushort)0));
                        SrcValuesG.Add(Math.Max(Math.Min((ushort)(modulas[1][y][x] * scaleG.Length), (ushort)(scaleG.Length - 1)), (ushort)0));
                        SrcValuesB.Add(Math.Max(Math.Min((ushort)(modulas[2][y][x] * scaleB.Length), (ushort)(scaleB.Length - 1)), (ushort)0));
                    }
                    n++;
                }
            ScaleR = scaleR;
            ScaleG = scaleG;
            ScaleB = scaleB;

            MaxValue = (uint)(scaleR.Length - 1);
            MinValue = 0;

            initFilter();
        }

        /// <summary>
        /// グレイスケールcomplexを引数に取るコンストラクタ
        /// </summary>
        /// <param name="complex"></param>
        public PseudoBitmap(Complex[][] complex, byte[] scaleR, byte[] scaleG, byte[] scaleB, bool normarize, bool isRealImage)
        {
            //適正なサイズでなければ例外をとばす
            if (complex == null || complex.Length == 0 || complex[0].Length == 0) throw new ArgumentNullException("Invalid input");

            GrayScale = true;
            RealImage = isRealImage;

            ComplexGray = complex;
            double[][] modulas = Fourier.GetModulus(complex, normarize);

            //高さ、幅を設定
            Width = complex[0].Length;
            Height = complex.Length;
            Center = new PointF(Width / 2.0f, Height / 2.0f);

            SrcValuesR.Clear();
            SrcValuesR.AddRange(new uint[Width * Height]);
            int n = 0;
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                {
                    if (!normarize)
                        SrcValuesR[n++] = (Math.Max(Math.Min((byte)modulas[y][x], (byte)255), (byte)0));
                    else
                        SrcValuesR[n++] = (Math.Max(Math.Min((byte)(modulas[y][x] * 255), (byte)255), (byte)0));
                }
            SrcValuesG = SrcValuesR;
            SrcValuesB = SrcValuesR;

            ScaleR = scaleR;
            ScaleG = scaleG;
            ScaleB = scaleB;

            MaxValue = (scaleR.Length - 1);
            MinValue = 0;

            initFilter();
        }

        #endregion complex用のコンストラクタ (どこで使ってるんだっけ?)

        /// <summary>
        /// 実画像用コンストラクタ グレーorカラーは自動判別
        /// </summary>
        /// <param name="bitmap"></param>
        public PseudoBitmap(Bitmap bitmap)
        {
            RealImage = true;
            //null値だったら例外をとばす
            if (bitmap == null) throw new ArgumentNullException("null bitmap");

            //高さ、幅を設定
            Width = bitmap.Width;
            Height = bitmap.Height;

            //画像はすべてPixelFormat.Format24bppRgbに変換する
            if (bitmap.PixelFormat != PixelFormat.Format24bppRgb)
            {
                if (bitmap.PixelFormat == PixelFormat.Format32bppArgb)
                    bitmap = bitmap.Clone(new Rectangle(0, 0, Width, Height), PixelFormat.Format24bppRgb);

                if (bitmap.PixelFormat != PixelFormat.Format24bppRgb)
                    bitmap = new Bitmap(bitmap).Clone(new Rectangle(0, 0, Width, Height), PixelFormat.Format24bppRgb);
            }

            //変換がうまくいかなければ例外をとばす
            if (bitmap.PixelFormat != PixelFormat.Format24bppRgb)
                throw new ArgumentNullException("Invalid bitmap");

            SrcBitmap = bitmap;

            //SourceBitmapをロックする
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

            //SouceStrideを設定しておく
            int BitmapStride = bitmapData.Stride;

            //次にその情報をargbValuesに転送
            byte[] bitmapByteArray = new byte[bitmapData.Stride * bitmap.Height];
            Marshal.Copy(bitmapData.Scan0, bitmapByteArray, 0, bitmapByteArray.Length);
            //SourceBitmapをアンロックして終了
            bitmap.UnlockBits(bitmapData);

            //BitsPerPixels = 8;

            //グレイスケールかどうかを判断
            GrayScale = true;
            for (int i = 0; i < bitmapByteArray.Length && GrayScale; i += 3)
                if (bitmapByteArray[i] != bitmapByteArray[i + 1] || bitmapByteArray[i + 1] != bitmapByteArray[i + 2])
                    GrayScale = false;

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
                SrcValuesR.Clear();
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

            byte[] scale = new byte[256];
            for (int i = 0; i < 256; i++)
                scale[i] = (byte)i;
            ScaleR = ScaleG = ScaleB = scale;

            initFilter();
        }

        #endregion コンストラクタ

        #region 初期化関連

        private Complex[][] initComplex(int width, int height)
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
            if (Filter1.Count != Height * Width)
            {
                Filter1 = Enumerable.Repeat(false, Height * Width).ToList();
                Filter2 = Enumerable.Repeat(false, Height * Width).ToList();
                Filter3 = Enumerable.Repeat(false, Height * Width).ToList();
                Filter4 = Enumerable.Repeat(false, Height * Width).ToList();
                Filter5 = Enumerable.Repeat(false, Height * Width).ToList();
                FilterTemporary = Enumerable.Repeat(false, Height * Width).ToList();
                if (Height * Width < 3001 * 3001)
                    FFT_Filter= Enumerable.Repeat(0f, Height * Width).ToList();
            }
            else
            {
                for (int i = 0; i < Height * Width; i++)
                {
                    if (Height * Width < 3001 * 3001)
                        FFT_Filter[i] = 0;
                    FilterTemporary[i] = false;
                    Filter1[i] = false;
                    Filter2[i] = false;
                    Filter3[i] = false;
                    Filter4[i] = false;
                    Filter5[i] = false;
                }
            }
        }

        #endregion 初期化関連

        #region プロパティ

        public enum Scales { LinearGray, LinearColdWarm, LinearRotation, LogGray, LogColdWarm }

        private Scales scale = Scales.LinearGray;
        public Scales Scale { get => scale;
            set 
                {
                scale = value;
                if (scale == Scales.LinearGray)
                    SetScaleGray(true);
                else if (scale == Scales.LogGray)
                    SetScaleGray(false);
                else if (scale == Scales.LinearColdWarm)
                    SetScaleColdWarm(true);
                else if (scale == Scales.LogColdWarm)
                    SetScaleColdWarm(false);
                else if (scale == Scales.LinearRotation)
                    SetScaleRotation();


            }
        }



        public static byte[] BrightnessScaleR = new byte[65536];//明るさスケール　16bit長に固定
        public static byte[] BrightnessScaleG = new byte[65536];//明るさスケール　16bit長に固定
        public static byte[] BrightnessScaleB = new byte[65536];//明るさスケール　16bit長に固定
        public static byte[] BrightnessScaleLog = new byte[65536];//ログ用の明るさスケール　16bit長に固定
        public static byte[] BrightnessScaleLiner = new byte[65536];//ログ用の明るさスケール　16bit長に固定
        public static byte[] BrightnessScaleLinerColorR = new byte[65536];
        public static byte[] BrightnessScaleLinerColorG = new byte[65536];
        public static byte[] BrightnessScaleLinerColorB = new byte[65536];
        public static byte[] BrightnessScaleLogColorR = new byte[65536];
        public static byte[] BrightnessScaleLogColorG = new byte[65536];
        public static byte[] BrightnessScaleLogColorB = new byte[65536];

        public static byte[] BrightnessScaleLinerRotationR = new byte[65536];
        public static byte[] BrightnessScaleLinerRotationG = new byte[65536];
        public static byte[] BrightnessScaleLinerRotationB = new byte[65536];


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

        public List<float> FFT_Filter = new List<float>();
        public List<bool> FilterTemporary = new List<bool>();
        public List<bool> Filter1 = new List<bool>();
        public List<bool> Filter2 = new List<bool>();
        public List<bool> Filter3 = new List<bool>();
        public List<bool> Filter4 = new List<bool>();
        public List<bool> Filter5 = new List<bool>();
        public bool Filter1Visible = true;
        public bool Filter2Visible = true;
        public bool Filter3Visible = true;
        public bool Filter4Visible = true;
        public bool Filter5Visible = true;

        public byte[] ScaleR;
        public byte[] ScaleG;
        public byte[] ScaleB;

        public List<uint> SrcValuesR = new List<uint>();
        public List<uint> SrcValuesG = new List<uint>();
        public List<uint> SrcValuesB = new List<uint>();

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

        /*private BlurModeEnum blurMode = BlurModeEnum.None;
        /// <summary>
        /// BlurMode
        /// </summary>
        public BlurModeEnum BlurMode
        {
            get { return blurMode; }
            set { blurMode = value; }
        }

        private double blurRadius = 0;
        /// <summary>
        /// Blurモードの時の半径
        /// </summary>
        public double BlurRadius
        {
            get { return blurRadius; }
            set { blurRadius = value; }
        }
    */

        private enum ImageSouceEnum
        { GrayDouble, GrayUint, ColorDouble, ColorUint }

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

            int offset = x + y * Width;
            return Color.FromArgb(
                ScaleR[Math.Max(Math.Min((int)((double)(SrcValuesR[offset] - MinValue) / (MaxValue - MinValue) * ScaleR.Length + 0.5), ScaleR.Length - 1), 0)],
                ScaleG[Math.Max(Math.Min((int)((double)(SrcValuesG[offset] - MinValue) / (MaxValue - MinValue) * ScaleG.Length + 0.5), ScaleG.Length - 1), 0)],
                ScaleB[Math.Max(Math.Min((int)((double)(SrcValuesB[offset] - MinValue) / (MaxValue - MinValue) * ScaleB.Length + 0.5), ScaleB.Length - 1), 0)]
                );
        }

        public Color GetPixelColor(double x, double y)
        {
            return GetPixelColor((int)(x + 0.5), (int)(y + 0.5));
        }

        public double GetPixelRawValue(PointD pt)
        {
            return GetPixelRawValue(pt.X, pt.Y);
        }


        public void SetScaleColdWarm(bool linear=true)
        {
            if (linear)
            {
                ScaleR = BrightnessScaleLinerColorR;
                ScaleG = BrightnessScaleLinerColorG;
                ScaleB = BrightnessScaleLinerColorB;
            }
            else
            {
                ScaleR = BrightnessScaleLogColorR;
                ScaleG = BrightnessScaleLogColorG;
                ScaleB = BrightnessScaleLogColorB;
            }

            GrayScale = false;
        }

        public void SetScaleGray(bool linear = true)
        {
            if (linear)
            {
                ScaleR = BrightnessScaleLiner;
                ScaleG = BrightnessScaleLiner;
                ScaleB = BrightnessScaleLiner;
            }
            else
            {
                ScaleR = BrightnessScaleLog;
                ScaleG = BrightnessScaleLog;
                ScaleB = BrightnessScaleLog;
            }

            GrayScale = false;
        }

        public void SetScaleRotation()
        {
            ScaleR = BrightnessScaleLinerRotationR;
            ScaleG = BrightnessScaleLinerRotationG;
            ScaleB = BrightnessScaleLinerRotationB;
            GrayScale = false;
        }



        /// <summary>
        /// グレースケールの時のx, y位置の生の強度を返す. グレースケールではなかったら常に0を返す.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public double GetPixelRawValue(double x, double y)
        {
            return GetPixelRawValue((int)(x + 0.5), (int)(y + 0.5));
        }

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

        public Complex[] GetPixelComplex(double x, double y)
        {
            return GetPixelComplex((int)Math.Round(x), (int)Math.Round(y));
        }

        /// <summary>
        /// 等倍のイメージを取得する
        /// </summary>
        /// <param name="srcCenter"></param>
        /// <param name="zoom"></param>
        /// <param name="destSize"></param>
        /// <returns></returns>
        public Bitmap GetImage()
        {
            return GetImage(new RectangleD(0, 0, Width, Height), new Size(Width, Height));
        }

        /// <summary>
        /// 指定された範囲のイメージを取得する
        /// </summary>
        /// <param name="srcCenter"></param>
        /// <param name="zoom"></param>
        /// <param name="destSize"></param>
        /// <returns></returns>
        public Bitmap GetImage(PointD srcCenter, double zoom, Size destSize)
        {
            if (srcCenter.IsNaN) return null;
            return GetImage(GetDrawingArea(srcCenter, zoom, destSize), destSize);
        }

        /// <summary>
        /// 描画範囲を得る
        /// </summary>
        /// <param name="srcCenter"></param>
        /// <param name="zoom"></param>
        /// <param name="destSize"></param>
        /// <returns></returns>
        public RectangleD GetDrawingArea(PointD srcCenter, double zoom, Size destSize)
        {
            return new RectangleD(srcCenter.X - destSize.Width / zoom / 2.0, srcCenter.Y - destSize.Height / zoom / 2.0, destSize.Width / zoom, destSize.Height / zoom);
        }

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

            //まずbmpが前回作られていなかったら作成
            if (destBmp == null || destBmp.Width != destSize.Width || destBmp.Height != destSize.Height)
                destBmp = new Bitmap(destSize.Width, destSize.Height, PixelFormat.Format24bppRgb);

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
            double zoom = (destSize.Width / srcRect.Width + (double)destSize.Height / srcRect.Height) / 2.0;

            //描画する画素位置をソース画像位置に変換するdelegateを作成
            Point getSrcPosition(int x, int y)
            {
                int _x, _y;
                if (!HorizontalFlip)
                    _x = (int)(srcRect.X + (double)x / destSize.Width * srcRect.Width + 0.5);
                else
                    _x = (int)(srcRect.X + (double)(destSize.Width - x) / destSize.Width * srcRect.Width + 0.5);
                if (!VerticalFlip)
                    _y = (int)(srcRect.Y + (double)y / destSize.Height * srcRect.Height + 0.5);
                else
                    _y = (int)(srcRect.Y + (double)(destSize.Height - y) / destSize.Height * srcRect.Height + 0.5);

                return new Point(_x, _y);
            }

            //入力値doubleを表示する値に変換するdelegate
            byte getValue(double rawValue, byte[] scale)
            {
                double rawIndex = (double)(rawValue - MinValue) / (MaxValue - MinValue) * scale.Length;
                int minIndex = (int)(Math.Min(rawIndex, scale.Length - 1) + 0.5);
                int index = Math.Max(minIndex, 0);

                return scale[index];
            }

            int range = (int)(1 / zoom / 2 + 0.5);
            int increase = (zoom >= 1) ? 0 : Math.Max(range / 3, 1);

            int width = destBmp.Width;
            int height = destBmp.Height;

            byte* p = (byte*)(void*)bmpData.Scan0;
            int nResidual = bmpData.Stride - destBmp.Width * 3;

            double filter;
            byte r = 0, g = 0, b = 0;
            Point beforePt = new Point(int.MinValue, int.MinValue);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Point pt = getSrcPosition(x, y);
                    if (pt.X == beforePt.X && pt.Y == beforePt.Y)
                    {
                        p[0] = p[-3];
                        p[1] = p[-2];
                        p[2] = p[-1];
                    }
                    else if (pt.X >= 0 && pt.X < Width && pt.Y >= 0 && pt.Y < Height)//描画域内のとき
                    {
                        int srcPosition = pt.X + pt.Y * Width;
                        {
                            if (increase > 0)//補完モード
                            {
                                double rSum = 0, gSum = 0, bSum = 0, count = 0;

                                for (int j = Math.Max(pt.Y - range, 0); j <= Math.Min(pt.Y + range, Height - 1); j += increase)
                                    for (int i = Math.Max(pt.X - range, 0); i <= Math.Min(pt.X, Width - 1); i += increase)
                                    {
                                        int tempPosition = i + j * Width;

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
                                    b = getValue(bSum / count, ScaleB);
                                    if (GrayScale)
                                        g = r = b;
                                    else
                                    {
                                        if (IsSrcGray)
                                            gSum = rSum = bSum;
                                        g = getValue(gSum / count, ScaleG);
                                        r = getValue(rSum / count, ScaleR);
                                    }
                                }
                            }
                            else
                            {
                                if (IsSrcGray)
                                {
                                    b = getValue(SrcValuesGray[srcPosition], ScaleB);
                                    if (GrayScale)
                                        g = r = b;
                                    else
                                    {
                                        g = getValue(SrcValuesGray[srcPosition], ScaleG);
                                        r = getValue(SrcValuesGray[srcPosition], ScaleR);
                                    }
                                }
                                else
                                {
                                    b = getValue(SrcValuesB[srcPosition], ScaleB);
                                    if (GrayScale)
                                        g = r = b;
                                    else
                                    {
                                        g = getValue(SrcValuesG[srcPosition], ScaleG);
                                        r = getValue(SrcValuesR[srcPosition], ScaleR);
                                    }
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
                            //p[0] = 255;
                            //p[1] = 255;
                            //p[2] = 255-  51;
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
                        if (Filter5Visible && Filter5 != null /*&& Filter5.Count !=0 */&& Filter5[srcPosition])//もし範囲外だったら全体を2/3にして(0,85,0)をたす
                        {//BR
                            p[0] = (byte)(p[0] * 0.8 + 51);
                            p[1] = (byte)(p[2] * 0.8);
                            p[2] = (byte)(p[1] * 0.8 + 51);
                        }
                    }
                    else//描画域がはみ出したとき
                    {
                        p[0] = 0; p[1] = 127; p[2] = 0;
                    }
                    p += 3;
                    beforePt.X = pt.X;
                    beforePt.Y = pt.Y;
                }
                p += nResidual;
            }

            for (int i = 0; i < FilterTemporary.Count; i++)
                FilterTemporary[i] = false;

            destBmp.UnlockBits(bmpData);

            justBeforeDestSize = destSize;
            justBeforeSrcRect = srcRect;
            return destBmp;
        }

        /// <summary>
        /// 実画像から虚画像に変換
        /// </summary>
        /// <returns></returns>
        public PseudoBitmap ToInverseImage(byte[] scale, bool normarize)
        {
            if (this.RealImage == false) return null;

            if (this.GrayScale)//グレースケールのとき
            {
                //strideを考慮してbyte配列を抽出
                double[][] valueGray = new double[Height][];
                int n = 0;
                for (int y = 0; y < Height; y++)
                {
                    valueGray[y] = new double[Width];
                    for (int x = 0; x < Width; x++)
                        valueGray[y][x] = SrcValuesR[n++];
                }
                return new PseudoBitmap(Fourier.FFT(valueGray), scale, scale, scale, normarize, false);
            }
            else //カラースケールのとき
            {
                double[][] valueR = new double[Height][];
                double[][] valueG = new double[Height][];
                double[][] valueB = new double[Height][];
                int n = 0;
                for (int y = 0; y < Height; y++)
                {
                    valueR[y] = new double[Width];
                    valueG[y] = new double[Width];
                    valueB[y] = new double[Width];
                    for (int x = 0; x < Width; x++)
                    {
                        valueB[y][x] = SrcValuesB[n];
                        valueG[y][x] = SrcValuesG[n];
                        valueR[y][x] = SrcValuesR[n];
                        n++;
                    }
                }
                return new PseudoBitmap(Fourier.FFT(valueR), Fourier.FFT(valueG), Fourier.FFT(valueB), scale, scale, scale, normarize, false);
            }
        }

        /// <summary>
        /// 虚画像から実画像に変換
        /// </summary>
        /// <returns></returns>
        public PseudoBitmap ToRealImage(bool normarize)
        {
            if (this.RealImage) return null;

            //フィルターの中心を抜く
            if (FFT_Filter != null)
            {
                FFT_Filter[(int)Center.Y * Width + (int)Center.X] = 0;
                if (Width % 2 == 1)
                    if (Height % 2 == 1)
                    {//両方奇数のとき
                        FFT_Filter[(int)Center.Y * Width + (int)Center.X + 1] = 0;
                        FFT_Filter[((int)Center.Y + 1) * Width + (int)Center.X] = 0;
                        FFT_Filter[((int)Center.Y + 1) * Width + (int)Center.X + 1] = 0;
                    }
                    else
                        FFT_Filter[(int)Center.Y * Width + (int)Center.X + 1] = 0;
                else if (Height % 2 == 1)
                    FFT_Filter[((int)Center.Y + 1) * Width + (int)Center.X] = 0;
            }

            int n = 0;
            double[][] filter = new double[Height][];
            for (int i = 0; i < filter.Length; i++)
            {
                filter[i] = new double[Width];
                for (int j = 0; j < Width; j++)
                    filter[i][j] = (double)FFT_Filter[n++];
            }

            byte[] scale = new byte[256];
            for (int i = 0; i < 256; i++)
                scale[i] = (byte)i;

            if (GrayScale)
            {
                Complex[][] complex = Fourier.FFT(ComplexGray, filter, FourierDirectionEnum.Inverse);
                return new PseudoBitmap(complex, scale, scale, scale, normarize, true);
            }
            else
            {
                Complex[][] complexR = Fourier.FFT(ComplexR, filter, FourierDirectionEnum.Inverse);
                Complex[][] complexG = Fourier.FFT(ComplexG, filter, FourierDirectionEnum.Inverse);
                Complex[][] complexB = Fourier.FFT(ComplexB, filter, FourierDirectionEnum.Inverse);
                return new PseudoBitmap(complexR, complexG, complexB, scale, scale, scale, normarize, true);
            }
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
}