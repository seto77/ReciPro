using System;
using System.Numerics;
using System.Threading.Tasks;

namespace Crystallography
{
    /*
    public class FFTW
    {
        //managed arrays
        //double[] din, dout;
        public double[] Value;

        //handles to managed arrays, keeps them pinned in memory
        //GCHandle hdin;//, hdout;
        GCHandle hd;

        //pointers to the FFTW plan objects
        IntPtr plan;
        readonly double coeff;

        /// <summary>
        ///  Initializes FFTW and all arrays. n: Logical size of the transform
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="direction"></param>
        /// <param name="values"></param>
        public FFTW(int width, int height, fftw_direction direction)
        {
            //fftw.fftw_init_threads();
            //fftw.plan_with_nthreads(Environment.ProcessorCount);
            fftw.plan_with_nthreads(Environment.ProcessorCount*2);

            // create two managed arrays, possibly misalinged.  n*2 because we are dealing with complex numbers.
            Value = new double[width * height * 2];
            //dout = new double[width * height * 2];

            // get handles and pin arrays so the GC doesn't move them
            hd = GCHandle.Alloc(Value, GCHandleType.Pinned);
            //hdout = GCHandle.Alloc(dout, GCHandleType.Pinned);

            IntPtr addr = hd.AddrOfPinnedObject();
            //output = hdout.AddrOfPinnedObject();
            if (File.Exists("fftw.wisdom"))
                fftw.import_wisdom_from_filename("fftw.wisdom");

            plan = fftw.dft_2d(width, height, addr, addr, direction, fftw_flags.Measure);

            fftw.export_wisdom_to_filename("fftw.wisdom");

            coeff = 1.0/width;
        }

        public double[] RunWithNormarize(double[] values)
        {
            Parallel.For(0, Value.Length, i => Value[i] = values[i] * coeff);
            fftwf.execute(plan);
            return Value;
        }
        public double[] Run()
        {
            //Parallel.For(0, d.Length, i => d[i] = values[i] * coeff);
            fftwf.execute(plan);
            return Value;
        }
    }
    */

    public static class Fourier
    {
        static Fourier()
        {
            if (argument.Length == 0)
                initializeArgument();
            if (sinTbl == null)
                initializeTable();
        }

        public delegate void ProgressEventDelegate();

        public static event ProgressEventDelegate ProgressEvent;

        private static readonly int ThreadTotal = 128;
        private static readonly int MaxDenominator = 8000;//必ず偶数
        private static readonly int MaxCooleyTukeyTable = 13;

        /// <summary>
        /// 複素数valuesのmodulus(長さ)を得る
        /// </summary>
        /// <param name="values"></param>
        /// <param name="normarize"></param>
        /// <returns></returns>
        public static double[][][] GetModulus(Complex[][][] values, bool normarize)
        {
            double[][][] rawModulus = new double[values.Length][][];
            double maxModulus = double.NegativeInfinity;
            for (int z = 0; z < values.Length; z++)
            {
                rawModulus[z] = new double[values[z].Length][];
                for (int y = 0; y < values[z].Length; y++)
                {
                    rawModulus[z][y] = new double[values[z][y].Length];
                    for (int x = 0; x < values[z][y].Length; x++)
                    {
                        rawModulus[z][y][x] = values[z][y][x].Magnitude;
                        maxModulus = Math.Max(rawModulus[z][y][x], maxModulus);
                    }
                }
            }

            if (normarize == false)
                return rawModulus;
            else
            {
                for (int z = 0; z < values.Length; z++)
                    for (int y = 0; y < values[z].Length; y++)
                        for (int x = 0; x < values[z][y].Length; x++)
                            rawModulus[z][y][x] /= maxModulus;
                return rawModulus;
            }
        }

        /// <summary>
        /// 複素数valuesのmodulus(長さ)を得る
        /// </summary>
        /// <param name="values"></param>
        /// <param name="normarize"></param>
        /// <returns></returns>
        public static double[][] GetModulus(Complex[][] values, bool normarize)
        {
            double[][] rawModulus = new double[values.Length][];
            double maxModulus = double.NegativeInfinity;
            for (int y = 0; y < values.Length; y++)
            {
                rawModulus[y] = new double[values[y].Length];
                for (int x = 0; x < values[y].Length; x++)
                {
                    rawModulus[y][x] = values[y][x].Magnitude;
                    maxModulus = Math.Max(rawModulus[y][x], maxModulus);
                }
            }
            if (normarize == false)
                return rawModulus;
            else
            {
                for (int y = 0; y < values.Length; y++)
                    for (int x = 0; x < values[y].Length; x++)
                        rawModulus[y][x] /= maxModulus;
                return rawModulus;
            }
        }

        /// <summary>
        /// 2次元FFT
        /// </summary>
        /// <param name="values"></param>
        /// <param name="wide"></param>
        /// <returns></returns>
        public static Complex[][] FFT(byte[][] values)
        {
            Complex[][] src = new Complex[values.Length][];
            for (int y = 0; y < src.Length; y++)
            {
                src[y] = new Complex[values[y].Length];
                for (int x = 0; x < src[y].Length; x++)
                    src[y][x] = new Complex(values[y][x], 0);
            }
            return FFT(src, FourierDirectionEnum.Forward);
        }

        /// <summary>
        /// 2次元FFT
        /// </summary>
        /// <param name="values"></param>
        /// <param name="wide"></param>
        /// <returns></returns>
        public static Complex[][] FFT(double[][] values)
        {
            Complex[][] src = new Complex[values.Length][];
            for (int y = 0; y < src.Length; y++)
            {
                src[y] = new Complex[values[y].Length];
                for (int x = 0; x < src[y].Length; x++)
                    src[y][x] = new Complex(values[y][x], 0);
            }
            return FFT(src, FourierDirectionEnum.Forward);
        }

        public static Complex[][] FFT(Complex[][] src, FourierDirectionEnum direction)
        {
            return FFT(src, null, direction);
        }

        /// <summary>
        /// 2次元FFT
        /// </summary>
        /// <param name="src"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static Complex[][] FFT(Complex[][] src, double[][] filter, FourierDirectionEnum direction)
        {
            if (src == null || src.Length == 0 || src[0] == null || src[0].Length == 0) return null;
            int centerX = src[0].Length / 2, centerY = src.Length / 2;

            //まずFilterを作用させる && ４分割して組みなおす
            Complex[][] src2 = new Complex[src.Length][];
            for (int i = 0; i < src.Length; i++)
                src2[i] = new Complex[src[i].Length];

            for (int i = 0; i < src.Length; i++)
                for (int j = 0; j < src[i].Length; j++)
                    if (direction == FourierDirectionEnum.Inverse)
                        src2[i < centerY ? i + centerY : i - centerY][j < centerX ? j + centerX : j - centerX] = filter == null ? src[i][j] : (1 - filter[i][j]) * src[i][j];
                    else
                        src2[i][j] = filter == null ? src[i][j] : (1 - filter[i][j]) * src[i][j];
            src = null;
            sign = direction == FourierDirectionEnum.Forward ? -1 : 1;

            FFTdelegate[] d = new FFTdelegate[ThreadTotal];
            IAsyncResult[] ar = new IAsyncResult[ThreadTotal];
            for (int i = 0; i < ThreadTotal; i++)
                d[i] = new FFTdelegate(FFT);

            //まず各行ごとにFFT
            Complex[][] dest1 = new Complex[src2.Length][];
            for (int i = 0; i < dest1.Length; i++)
            {
                for (int j = 0; j < ThreadTotal && i + j < dest1.Length; j++)
                    ar[j] = d[j].BeginInvoke(src2[i + j], direction, ref dest1[i + j], null, null);//スレッド起動
                for (int j = 0; j < ThreadTotal && i + j < dest1.Length; j++)
                {
                    d[j].EndInvoke(ref dest1[i + j], ar[j]);//スレッド終了待ち
                    ProgressEvent?.Invoke();
                }
                i += ThreadTotal - 1;
            }

            //次に各列ごとにFFT
            Complex[][] dest2 = new Complex[src2[0].Length][];
            for (int i = 0; i < dest2.Length; i++)
            {
                dest2[i] = new Complex[src2.Length];
                for (int j = 0; j < src2.Length; j++)
                    dest2[i][j] = dest1[j][i];
            }

            for (int i = 0; i < dest2.Length; i++)
            {
                for (int j = 0; j < ThreadTotal && i + j < dest2.Length; j++)
                    ar[j] = d[j].BeginInvoke(dest2[i + j], direction, ref dest2[i + j], null, null);//スレッド起動
                for (int j = 0; j < ThreadTotal && i + j < dest2.Length; j++)
                {
                    d[j].EndInvoke(ref dest2[i + j], ar[j]);//スレッド終了待ち
                    ProgressEvent?.Invoke();
                }
                i += ThreadTotal - 1;
            }

            //dest2の行と列を入れ替えてdest1に格納し返す
            for (int i = 0; i < src2.Length; i++)
                for (int j = 0; j < src2[0].Length; j++)
                    dest1[i][j] = dest2[j][i];
            dest2 = null;

            if (direction == FourierDirectionEnum.Inverse)
                return dest1;
            else //最後に入れ替える必要がある時は
            {
                Complex[][] dest3 = new Complex[src2.Length][];
                for (int i = 0; i < dest3.Length; i++)
                    dest3[i] = new Complex[src2[i].Length];
                for (int i = 0; i < src2.Length; i++)
                    for (int j = 0; j < src2[i].Length; j++)
                        dest3[i < centerY ? i + centerY : i - centerY][j < centerX ? j + centerX : j - centerX] = dest1[i][j];
                dest1 = null;
                return dest3;
            }
        }

        private delegate void FFTdelegate(Complex[] src, FourierDirectionEnum direction, ref Complex[] dest);

        private static void FFT(Complex[] src, FourierDirectionEnum direction, ref Complex[] dest)
        {
            dest = FFT(src, direction);
        }

        /// <summary>
        /// 一次元FFT
        /// </summary>
        /// <param name="src"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static Complex[] FFT(Complex[] src, FourierDirectionEnum direction)
        {
            p.MaxDegreeOfParallelism = System.Environment.ProcessorCount;
            Complex[] dest = fft(src, direction);
            //ComplexArray.Scale(dest, 1 / Math.Sqrt(src.Length));
            for (int i = 0; i < dest.Length; i++)
                dest[i] *= 1 / Math.Sqrt(src.Length);
            return dest;
        }

        private static Complex[] fft(Complex[] src, FourierDirectionEnum direction)
        {
            sign = direction == FourierDirectionEnum.Forward ? -1 : 1;

            return fft(src);
        }

        #region fft用サブルーチン

        private static int sign = -1;

        private static readonly double sq = Math.Sqrt(1.0 / 2.0);

        private static Complex[] fft2(Complex[] src)
        {
            return new Complex[]{
               new Complex(src[0].Real + src[1].Real, src[0].Imaginary + src[1].Imaginary),
               new Complex(src[0].Real- src[1].Imaginary, src[0].Imaginary - src[1].Imaginary)
                              };
        }

        private static Complex[] fft3(Complex[] src)
        {
            Complex[] result = new Complex[]{
               new Complex(
                   src[0].Real+ src[1].Real+ src[2].Real,
                   src[0].Imaginary + src[1].Imaginary + src[2].Imaginary),
                new Complex(
                    src[0].Real+ src[1].Real* arg13R - src[1].Imaginary * arg13I + src[2].Real* arg23R - src[2].Imaginary * arg23I,
                    src[0].Imaginary + src[1].Real* arg13I + src[1].Imaginary * arg13R + src[2].Real* arg23I + src[2].Imaginary * arg23R),
               new Complex(
                   src[0].Real+ src[1].Real* arg23R - src[1].Imaginary * arg23I + src[2].Real* arg13R - src[2].Imaginary * arg13I,
                   src[0].Imaginary + src[1].Real* arg23I + src[1].Imaginary * arg23R + src[2].Real* arg13I + src[2].Imaginary * arg13R)
                              };
            if (sign == 1) return result;
            else return new Complex[] { result[0], result[2], result[1] };
        }

        private static Complex[] fft4(Complex[] src)
        {
            Complex[] result = new Complex[] {
                new Complex(src[0].Real+ src[1].Real+ src[2].Real+ src[3].Real, src[0].Imaginary + src[1].Imaginary + src[2].Imaginary + src[3].Imaginary),
                new Complex(src[0].Real- src[1].Imaginary - src[2].Real+ src[3].Imaginary, src[0].Imaginary + src[1].Real- src[2].Imaginary - src[3].Real),
                new Complex(src[0].Real- src[1].Real+ src[2].Real- src[3].Real, src[0].Imaginary - src[1].Imaginary + src[2].Imaginary - src[3].Imaginary),
                new Complex(src[0].Real+ src[1].Imaginary - src[2].Real- src[3].Imaginary, src[0].Imaginary - src[1].Real- src[2].Imaginary + src[3].Real)                               };
            if (sign == 1) return result;
            else return new Complex[] { result[0], result[3], result[2], result[1] };
        }

        private static Complex[] fft5(Complex[] s)
        {
            Complex[] result = new Complex[]{
               new Complex(
                   s[0].Real+ s[1].Real+ s[2].Real+ s[3].Real+ s[4].Real,
                   s[0].Imaginary + s[1].Imaginary + s[2].Imaginary + s[3].Imaginary + s[4].Imaginary),
               new Complex(
                    s[0].Real+ s[1].Real* arg15R - s[1].Imaginary * arg15I + s[2].Real* arg25R - s[2].Imaginary * arg25I + s[3].Real* arg35R - s[3].Imaginary * arg35I + s[4].Real* arg45R - s[4].Imaginary * arg45I,
                    s[0].Imaginary + s[1].Real* arg15I + s[1].Imaginary * arg15R + s[2].Real* arg25I + s[2].Imaginary * arg25R + s[3].Real* arg35I + s[3].Imaginary * arg35R + s[4].Real* arg45I + s[4].Imaginary * arg45R),
                new Complex(
                    s[0].Real+ s[1].Real* arg25R - s[1].Imaginary * arg25I + s[2].Real* arg45R - s[2].Imaginary * arg45I + s[3].Real* arg15R - s[3].Imaginary * arg15I + s[4].Real* arg35R - s[4].Imaginary * arg35I,
                    s[0].Imaginary + s[1].Real* arg25I + s[1].Imaginary * arg25R + s[2].Real* arg45I + s[2].Imaginary * arg45R + s[3].Real* arg15I + s[3].Imaginary * arg15R + s[4].Real* arg35I + s[4].Imaginary * arg35R),
                 new Complex(
                    s[0].Real+ s[1].Real* arg35R - s[1].Imaginary * arg35I + s[2].Real* arg15R - s[2].Imaginary * arg15I + s[3].Real* arg45R - s[3].Imaginary * arg45I + s[4].Real* arg25R - s[4].Imaginary * arg25I,
                    s[0].Imaginary + s[1].Real* arg35I + s[1].Imaginary * arg35R + s[2].Real* arg15I + s[2].Imaginary * arg15R + s[3].Real* arg45I + s[3].Imaginary * arg45R + s[4].Real* arg25I + s[4].Imaginary * arg25R),
               new Complex(
                    s[0].Real+ s[1].Real* arg45R - s[1].Imaginary * arg45I + s[2].Real* arg35R - s[2].Imaginary * arg35I + s[3].Real* arg25R - s[3].Imaginary * arg25I + s[4].Real* arg15R - s[4].Imaginary * arg15I,
                    s[0].Imaginary + s[1].Real* arg45I + s[1].Imaginary * arg45R + s[2].Real* arg35I + s[2].Imaginary * arg35R + s[3].Real* arg25I + s[3].Imaginary * arg25R + s[4].Real* arg15I + s[4].Imaginary * arg15R)
                              };

            //result = fftAny(s);
            if (sign == 1) return result;
            else return new Complex[] { result[0], result[4], result[3], result[2], result[1] };
        }

        private static Complex[] fft8(Complex[] s)
        {
            Complex[] result = new Complex[] {
                new Complex(s[0].Real+ s[1].Real+ s[2].Real+ s[3].Real+ s[4].Real+ s[5].Real+ s[6].Real+ s[7].Real,s[0].Imaginary + s[1].Imaginary + s[2].Imaginary + s[3].Imaginary + s[4].Imaginary + s[5].Imaginary + s[6].Imaginary + s[7].Imaginary),
                new Complex( s[0].Real- s[2].Imaginary - s[4].Real+ s[6].Imaginary + sq * (-s[1].Imaginary + s[1].Real- s[3].Imaginary - s[3].Real+ s[5].Imaginary - s[5].Real+ s[7].Imaginary + s[7].Real), s[0].Imaginary + s[2].Real- s[4].Imaginary - s[6].Real+ sq * (s[1].Imaginary + s[1].Real- s[3].Imaginary + s[3].Real- s[5].Imaginary - s[5].Real+ s[7].Imaginary - s[7].Real)),
                new Complex(s[0].Real- s[1].Imaginary - s[2].Real+ s[3].Imaginary + s[4].Real- s[5].Imaginary - s[6].Real+ s[7].Imaginary,s[0].Imaginary + s[1].Real- s[2].Imaginary - s[3].Real+ s[4].Imaginary + s[5].Real- s[6].Imaginary - s[7].Real),
                new Complex( s[0].Real+ s[2].Imaginary - s[4].Real- s[6].Imaginary + sq * (-s[1].Imaginary - s[1].Real- s[3].Imaginary + s[3].Real+ s[5].Imaginary + s[5].Real+ s[7].Imaginary - s[7].Real),s[0].Imaginary - s[2].Real- s[4].Imaginary + s[6].Real+ sq * (-s[1].Imaginary + s[1].Real+ s[3].Imaginary + s[3].Real+ s[5].Imaginary - s[5].Real- s[7].Imaginary - s[7].Real)),
                new Complex( s[0].Real- s[1].Real+ s[2].Real- s[3].Real+ s[4].Real- s[5].Real+ s[6].Real- s[7].Real,s[0].Imaginary - s[1].Imaginary + s[2].Imaginary - s[3].Imaginary + s[4].Imaginary - s[5].Imaginary + s[6].Imaginary - s[7].Imaginary),
                new Complex(s[0].Real- s[2].Imaginary - s[4].Real+ s[6].Imaginary + sq * (s[1].Imaginary - s[1].Real+ s[3].Imaginary + s[3].Real- s[5].Imaginary + s[5].Real- s[7].Imaginary - s[7].Real),s[0].Imaginary + s[2].Real- s[4].Imaginary - s[6].Real+ sq * (-s[1].Imaginary - s[1].Real+ s[3].Imaginary - s[3].Real+ s[5].Imaginary + s[5].Real- s[7].Imaginary + s[7].Real)),
                new Complex(s[0].Real+ s[1].Imaginary - s[2].Real- s[3].Imaginary + s[4].Real+ s[5].Imaginary - s[6].Real- s[7].Imaginary,s[0].Imaginary - s[1].Real- s[2].Imaginary + s[3].Real+ s[4].Imaginary - s[5].Real- s[6].Imaginary + s[7].Real),
                new Complex(s[0].Real+ s[2].Imaginary - s[4].Real- s[6].Imaginary + sq * (s[1].Imaginary + s[1].Real+ s[3].Imaginary - s[3].Real- s[5].Imaginary - s[5].Real- s[7].Imaginary + s[7].Real),s[0].Imaginary - s[2].Real- s[4].Imaginary + s[6].Real+ sq * (s[1].Imaginary - s[1].Real- s[3].Imaginary - s[3].Real- s[5].Imaginary + s[5].Real+ s[7].Imaginary + s[7].Real))
            };
            if (sign == 1) return result;
            else return new Complex[] { result[0], result[7], result[6], result[5], result[4], result[3], result[2], result[1] };
        }

        #region CooleyTukey用のルーチン

        private static void initializeTable()//各種テーブル初期化
        {
            bitRev = new int[MaxCooleyTukeyTable][];
            sinTbl = new double[MaxCooleyTukeyTable][];
            for (int i = 0; i < MaxCooleyTukeyTable; i++)
            {
                bitRev[i] = new int[(int)Math.Pow(2, i)];
                make_bitrev(bitRev[i]);
                sinTbl[i] = new double[(int)Math.Pow(2, i)];
                make_sintbl(sinTbl[i]);
            }
        }

        private static int[][] bitRev;
        private static double[][] sinTbl;

        private static void make_bitrev(int[] bitrev)//バタフライ演算のビット反転テーブル作成.
        {
            int n = bitrev.Length;
            int n2 = n / 2;

            int j = 0;
            bitrev[0] = j;

            for (int i = 1; i < n; i++)
            {
                int k = n2;
                while (k <= j)
                {
                    j -= k;
                    k /= 2;
                }
                j += k;
                bitrev[i] = j;
            }
        }

        private static void make_sintbl(double[] tbl)
        {
            int n = tbl.Length;
            int n2 = n / 2;
            int n4 = n / 4;
            int n8 = n / 8;

            double sh = Math.Sin(Math.PI / n);         //sin(h)
            double dc = 2.0 * sh * sh;              //1-cos(2h)
            double ds = Math.Sqrt(dc * (2.0 - dc)); //sin(2h)

            double t = 2.0 * dc;
            double c = tbl[n4] = 1.0;
            double s = tbl[0] = 0.0;

            //0---1/8---1/4---3/8---1/2---5/8---3/4---7/8---1
            //++++++++++++
            for (int i = 1; i < n8; i++)
            {
                c -= dc;
                s += ds;
                dc += t * c;
                ds -= t * s;
                tbl[i] = s;
                tbl[n4 - i] = c;
            }

            if (n8 != 0) tbl[n8] = Math.Sqrt(0.5);

            //0---1/8---1/4---3/8---1/2---5/8---3/4---7/8---1
            //           +++++++++++++
            for (int i = 0; i < n4; i++) tbl[n2 - i] = tbl[i];

            //0---1/8---1/4---3/8---1/2---5/8---3/4---7/8---1
            //                       ++++++++++++++++++++++++
            for (int i = 0; i < n2; i++) tbl[i + n2] = -tbl[i];
        }

        private static Complex[] fftCooleyTukey(Complex[] src)//2の累乗のときのアルゴリズム
        {
            int order = (int)Math.Log(src.Length, 2), n = src.Length, n4 = n / 4, i, j, k, h = 0, k2 = 0, ik;
            double cos, sin;
            for (i = 0; i < n; i++)//バタフライ演算用ビット反転
                if (i < (j = bitRev[order][i]))
                {
                    Complex t = src[i]; src[i] = src[j]; src[j] = t;
                }
            for (k = 1; k < n; k = k + k)//Sin,Cos変換
            {
                h = 0;
                k2 = k + k;
                for (j = 0; j < k; j++)
                {
                    cos = sinTbl[order][h + n4];
                    sin = (sign == 1) ? -sinTbl[order][h] : sinTbl[order][h];
                    for (i = j; i < n; i += k2)
                    {
                        ik = i + k;
                        Complex t = new Complex(sin * src[ik].Imaginary + cos * src[ik].Real, cos * src[ik].Imaginary - sin * src[ik].Real);
                        src[ik] = src[i] - t;
                        src[i] += t;
                    }
                    h += n / k2;
                }
            }
            return src;
        }

        #endregion CooleyTukey用のルーチン

        private static Complex[] fft(Complex[] src)
        {
            int length = src.Length;
            if (length == 4096 || length == 2048 || length == 1024 || length == 512 || length == 256 || length == 128 || length == 64 || length == 32 || length == 16)
                return fftCooleyTukey(src);
            else if (length == 8) return fft8(src);
            else if (length == 5) return fft5(src);
            else if (length == 4) return fft4(src);
            else if (length == 3) return fft3(src);
            else if (length == 2) return fft2(src);
            else return fftAny(src);
        }

        private static ParallelOptions p = new ParallelOptions();

        private static Complex[] fftAny(Complex[] src)
        {
            int length = src.Length;
            Complex[] dest = new Complex[length];
            int even = 0, odd = 0, n = 0;
            for (n = 3; n < length / 2.0; n += 2)
                if (length % n == 0)
                { odd = n; break; }
            for (n = 2; n < length / 2.0; n += 2)
                if (length % n == 0)
                { even = n; break; }
            if (odd != 0 || even != 0)//因数分解できた場合
            {
                if (even == 0) n = odd;
                else if (odd == 0) n = even;
                else
                    n = odd / 2 > even ? even : odd;
                int m = length / n;

                for (int i = 0; i < n; i++)
                {
                    Complex[] dividedSrc = new Complex[m];//Srcデータをn個に分割する
                    for (int j = 0; j < m; j++)
                        dividedSrc[j] = src[i + j * n];
                    dividedSrc = fft(dividedSrc);
                    for (int j = 0; j < m; j++)
                        for (int k = 0; k < n; k++)
                            dest[k * m + j] += dividedSrc[j] * Exp(sign * i * (k * m + j), length);
                }
                return dest;
            }
            else //因数分解ができない場合
            {
                for (int j = 0; j < length; j++)
                    for (int i = 0; i < length; i++)
                        dest[i] += src[j] * Exp(sign * i * j, length);

                // var v = Exp(1, 6);
                return dest;
            }
        }

        #endregion fft用サブルーチン

        private static Complex[] argument = Array.Empty<Complex>();
        private static int[] denominatorTable = Array.Empty<int>();

        private static void initializeArgument()
        {
            argument = new Complex[(MaxDenominator - 2) * MaxDenominator / 4];
            denominatorTable = new int[MaxDenominator + 1];
            int n = 0;
            for (int denom = 1; denom <= MaxDenominator; denom++)
            {
                denominatorTable[denom] = n;
                for (int num = 1; num < (denom + 1) / 2; num++)
                    argument[n++] = new Complex(Math.Cos((double)num / denom * 2 * Math.PI), Math.Sin((double)num / denom * 2 * Math.PI));
            }
        }

        private static Complex Exp(int numerator, int denominator)
        {
            numerator = numerator % denominator;
            if (numerator == 0)
                return new Complex(1, 0);
            else if ((numerator * 2) % denominator == 0)
                return new Complex(-1, 0);
            else if (denominator <= MaxDenominator)
            {
                if (numerator < 0)
                    numerator += denominator;

                if (numerator < (denominator + 1) / 2)
                    return argument[denominatorTable[denominator] + numerator - 1];
                else
                {
                    var c = argument[denominatorTable[denominator] + (denominator - numerator) - 1];
                    return new Complex(c.Real, -c.Imaginary);
                }
            }
            else
            {
                double x = (double)numerator / denominator * 2 * Math.PI;
                return new Complex(Math.Cos(x), Math.Sin(x));
            }
        }

        #region 定数

        private static double arg13R = Complex.Exp(2 * Math.PI * Complex.ImaginaryOne * 1.0 / 3.0).Real;
        private static double arg13I = Complex.Exp(2 * Math.PI * Complex.ImaginaryOne * 1.0 / 3.0).Imaginary;
        private static double arg23R = Complex.Exp(2 * Math.PI * Complex.ImaginaryOne * 2.0 / 3.0).Real;
        private static double arg23I = Complex.Exp(2 * Math.PI * Complex.ImaginaryOne * 2.0 / 3.0).Imaginary;
        private static double arg15R = Complex.Exp(2 * Math.PI * Complex.ImaginaryOne * 1.0 / 5.0).Real;
        private static double arg15I = Complex.Exp(2 * Math.PI * Complex.ImaginaryOne * 1.0 / 5.0).Imaginary;
        private static double arg25R = Complex.Exp(2 * Math.PI * Complex.ImaginaryOne * 2.0 / 5.0).Real;
        private static double arg25I = Complex.Exp(2 * Math.PI * Complex.ImaginaryOne * 2.0 / 5.0).Imaginary;
        private static double arg35R = Complex.Exp(2 * Math.PI * Complex.ImaginaryOne * 3.0 / 5.0).Real;
        private static double arg35I = Complex.Exp(2 * Math.PI * Complex.ImaginaryOne * 3.0 / 5.0).Imaginary;
        private static double arg45R = Complex.Exp(2 * Math.PI * Complex.ImaginaryOne * 4.0 / 5.0).Real;
        private static double arg45I = Complex.Exp(2 * Math.PI * Complex.ImaginaryOne * 4.0 / 5.0).Imaginary;

        #endregion 定数
    }
}