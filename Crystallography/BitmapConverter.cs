using System;

//using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Crystallography
{
    public static class BitmapConverter
    {
        public unsafe static Bitmap FlipVertically(Bitmap src)
        {
            BitmapData srcData = src.LockBits(new Rectangle(0, 0, src.Width, src.Height), ImageLockMode.ReadWrite, src.PixelFormat);
            byte* srcP = (byte*)(void*)srcData.Scan0;

            Bitmap dest = new Bitmap(src.Width, src.Height, src.PixelFormat);
            BitmapData destData = dest.LockBits(new Rectangle(0, 0, src.Width, src.Height), ImageLockMode.ReadWrite, src.PixelFormat);
            byte* destP = (byte*)(void*)destData.Scan0;

            for (int h = 0; h < src.Height; h++)
                for (int w = 0; w < destData.Stride; w++)
                    destP[h * destData.Stride + w] = srcP[(src.Height - h - 1) * destData.Stride + w];

            dest.UnlockBits(destData);
            src.UnlockBits(srcData);

            return dest;
        }

        /// <summary>
        /// byte配列を、ビットマップ画像に変換。配列の長さによって、グレースケールか、カラーかを、自動で判別
        /// </summary>
        /// <param name="rgb"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public unsafe static Bitmap FromArrayToBitmap(byte[] rgb, int width, int height)
        {
            if (rgb.Length == width * height * 3)
            {
                Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, bmp.PixelFormat);
                byte* p = (byte*)(void*)bmpData.Scan0;
                int residual = bmpData.Stride - bmp.Width * 3;
                for (int h = 0; h < height; h++)
                {
                    for (int w = 0; w < width; w++)
                    {
                        p[0] = rgb[(w + h * width) * 3 + 2];
                        p[1] = rgb[(w + h * width) * 3 + 1];
                        p[2] = rgb[(w + h * width) * 3 + 0];
                        p += 3;
                    }
                    p += residual;
                }
                bmp.UnlockBits(bmpData);
                return bmp;
            }
            else if (rgb.Length == width * height)
            {
                Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, bmp.PixelFormat);
                byte* p = (byte*)(void*)bmpData.Scan0;
                int residual = bmpData.Stride - bmp.Width * 3;
                for (int h = 0; h < height; h++)
                {
                    for (int w = 0; w < width; w++)
                    {
                        p[0] = p[1] = p[2] = rgb[w + h * width];
                        p += 3;
                    }
                    p += residual;
                }
                bmp.UnlockBits(bmpData);
                return bmp;
            }
            else
                return new Bitmap(1, 1);
        }

        public static Byte[] ToByte(Bitmap Bmp)
        {
            BitmapData bmpData = Bmp.LockBits(new Rectangle(0, 0, Bmp.Width, Bmp.Height), ImageLockMode.ReadOnly, Bmp.PixelFormat);
            byte[] rgbValues = new byte[bmpData.Stride * Bmp.Height];
            Marshal.Copy(bmpData.Scan0, rgbValues, 0, bmpData.Stride * Bmp.Height);
            Bmp.UnlockBits(bmpData);
            return rgbValues;
        }

        public static Byte[] ToByteWithA(Bitmap Bmp, byte a)
        {
            BitmapData bmpData = Bmp.LockBits(new Rectangle(0, 0, Bmp.Width, Bmp.Height), ImageLockMode.ReadOnly, Bmp.PixelFormat);
            byte[] rgbaValues;
            if (bmpData.Stride == Bmp.Width * 3)
                rgbaValues = new byte[bmpData.Stride / 3 * 4 * Bmp.Height];
            else
                rgbaValues = new byte[bmpData.Stride * 4 * Bmp.Height];
            Marshal.Copy(bmpData.Scan0, rgbaValues, 0, bmpData.Stride * Bmp.Height);
            Bmp.UnlockBits(bmpData);

            if (bmpData.Stride == Bmp.Width * 3)
                for (int i = rgbaValues.Length - 1; i >= 0; i -= 4)
                {
                    rgbaValues[i - 3] = rgbaValues[i / 4 * 3];
                    rgbaValues[i - 2] = rgbaValues[i / 4 * 3 + 1];
                    rgbaValues[i - 1] = rgbaValues[i / 4 * 3 + 2];
                    rgbaValues[i] = a;
                }
            else
                for (int i = rgbaValues.Length - 1; i >= 0; i -= 4)
                {
                    rgbaValues[i - 3] = rgbaValues[i / 4];
                    rgbaValues[i - 2] = rgbaValues[i / 4];
                    rgbaValues[i - 1] = rgbaValues[i / 4];
                    rgbaValues[i] = a;
                }
            return rgbaValues;
        }

        public static Byte[][] ToByteRGB(Bitmap Bmp)
        {
            if (Bmp.PixelFormat != PixelFormat.Format24bppRgb)
                return null;
            BitmapData bmpData = Bmp.LockBits(new Rectangle(0, 0, Bmp.Width, Bmp.Height), ImageLockMode.ReadOnly, Bmp.PixelFormat);
            byte[] rgbValues = new byte[bmpData.Stride * Bmp.Height];
            Marshal.Copy(bmpData.Scan0, rgbValues, 0, bmpData.Stride * Bmp.Height);
            Bmp.UnlockBits(bmpData);
            byte[][] values = new byte[3][];

            for (int i = 0; i < 3; i++)
                values[i] = new byte[bmpData.Stride * Bmp.Height / 3];

            int n = 0;
            for (int y = 0; y < Bmp.Height; y++)
            {
                int initX = bmpData.Stride * y;
                for (int x = 0; x < Bmp.Width; x++)
                {
                    int ix = initX + x * 3;
                    values[0][n] = rgbValues[ix + 2];
                    values[1][n] = rgbValues[ix + 1];
                    values[2][n] = rgbValues[ix + 0];
                    n++;
                }
            }
            return values;
        }

        /// <summary>
        /// 32bitビットマップ画像から、BGRAの順で各ピクセルの情報をもつbyte配列を返す。Lengthは width * height * 4 となる。
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static byte[] ToByteBGRA(Bitmap bmp)
        {
            if (bmp.PixelFormat == PixelFormat.Format32bppArgb)
            {
                var bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, bmp.PixelFormat);
                var argbValues = new byte[bmpData.Stride * bmp.Height];
                Marshal.Copy(bmpData.Scan0, argbValues, 0, bmpData.Stride * bmp.Height);
                bmp.UnlockBits(bmpData);

                if (argbValues.Length == bmp.Width * bmp.Height * 4)
                    return argbValues;
                else
                {
                    var values = new byte[bmp.Width * bmp.Height * 4];
                    for (int y = 0; y < bmp.Height; y++)
                        Array.Copy(argbValues, bmpData.Stride * y, values, y * bmp.Width * 4, bmp.Width * 4);
                    return values;
                }
            }
            else
                return null;
        }

        public static byte[] ToByteRGBA(Bitmap bmp)
        {
            var argbValues = ToByteBGRA(bmp);
            if (argbValues != null)
            {
                for (int i = 0; i < argbValues.Length; i += 4)
                {
                    var val = argbValues[i];
                    argbValues[i] = argbValues[i + 2];
                    argbValues[i + 2] = val;
                }
            }
            return argbValues;            
        }

        public static Byte[] ToByteGray(Bitmap Bmp)
        {
            BitmapData bmpData = Bmp.LockBits(new Rectangle(0, 0, Bmp.Width, Bmp.Height), ImageLockMode.ReadOnly, Bmp.PixelFormat);
            byte[] rgbValues = new byte[bmpData.Stride * Bmp.Height];
            Marshal.Copy(bmpData.Scan0, rgbValues, 0, bmpData.Stride * Bmp.Height);
            Bmp.UnlockBits(bmpData);

            if (Bmp.PixelFormat == PixelFormat.Format24bppRgb)
            {
                byte[] values = new byte[Bmp.Width * Bmp.Height];
                int n = 0;
                for (int y = 0; y < Bmp.Height; y++)
                {
                    int initX = bmpData.Stride * y;
                    for (int x = 0; x < Bmp.Width; x++)
                    {
                        int ix = initX + x * 3;
                        values[n++] = (byte)((rgbValues[ix] + rgbValues[ix + 1] + rgbValues[ix + 2]) / 3.0);
                    }
                }
                return values;
            }
            else if (Bmp.PixelFormat == PixelFormat.Format32bppArgb)
            {
                byte[] values = new byte[Bmp.Width * Bmp.Height];
                int n = 0;
                for (int y = 0; y < Bmp.Height; y++)
                {
                    int initX = bmpData.Stride * y;
                    for (int x = 0; x < Bmp.Width; x++)
                    {
                        int ix = initX + x * 4;
                        values[n++] = (byte)((rgbValues[ix] + rgbValues[ix + 1] + rgbValues[ix + 2]) / 3.0);
                    }
                }
                return values;
            }
            else if (Bmp.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                return rgbValues;
            }
            else
                return null;
        }

        public static int[] ToIntGray(Bitmap bmp)
        {
            byte[] byteValue = ToByteGray(bmp);
            int[] intValue = new int[byteValue.Length];
            for (int i = 0; i < intValue.Length; i++)
                intValue[i] = byteValue[i];
            return intValue;
        }

        public static void FromByte(Bitmap bmp, byte[] values)
        {
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, bmp.PixelFormat);
            if (bmpData.Stride * bmp.Height == values.Length) //長さが等しいときだけコピー
                Marshal.Copy(values, 0, bmpData.Scan0, bmpData.Stride * bmp.Height);
            bmp.UnlockBits(bmpData);
        }

        public static void FromByte(Bitmap bmp, byte[] rValues, byte[] gValues, byte[] bValues)
        {
            if (bmp.PixelFormat == PixelFormat.Format24bppRgb)//24bitカラーモードのときだけ
            {
                byte[] rgbValues = new byte[bmp.Width * bmp.Height * 3];
                int n = 0;
                for (int y = 0; y < bmp.Height; y++)
                {
                    int initX = bmp.Width * y * 3;
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        int ix = initX + x * 3;
                        rgbValues[ix + 2] = rValues[n];
                        rgbValues[ix + 1] = gValues[n];
                        rgbValues[ix + 0] = bValues[n++];
                    }
                }
                BitmapConverter.FromByte(bmp, rgbValues);
            }
        }

        public static void FromByte(Bitmap bmp, byte[] values, bool[] filter, Color fiterColor)
        {
            if (bmp.PixelFormat == PixelFormat.Format24bppRgb)//24bitカラーモードのときだけ
            {
                if (values.Length / 3 == filter.Length && filter.Length == bmp.Width * bmp.Height)//values長、filter長が適正のとき
                {
                    int r = fiterColor.R / 2;
                    int g = fiterColor.G / 2;
                    int b = fiterColor.B / 2;
                    for (int y = 0; y < bmp.Height; y++)
                    {
                        int yWidth = y * bmp.Width;
                        for (int x = 0; x < bmp.Width; x++)
                        {
                            if (filter[yWidth + x])
                            {
                                values[yWidth * 3 + x + 0] = (byte)(values[yWidth * 3 + x + 0] / 2 + b);
                                values[yWidth * 3 + x + 1] = (byte)(values[yWidth * 3 + x + 1] / 2 + g);
                                values[yWidth * 3 + x + 2] = (byte)(values[yWidth * 3 + x + 2] / 2 + r);
                            }
                        }
                    }
                    FromByte(bmp, values);
                }
            }
        }

        /*public static void FromFFT(Bitmap bmp, Complex[][] complexR, Complex[][] complexG, Complex[][] complexB, byte[] scale)
        {
            if (bmp.Height != complexR.Length) return;

            double[][] r = Fourier.GetModulus(complexR, true);
            double[][] g = Fourier.GetModulus(complexG, true);
            double[][] b = Fourier.GetModulus(complexB, true);
            if (bmp.PixelFormat == PixelFormat.Format24bppRgb)//24bitカラーモードのときだけ
            {
                byte[] values = new byte[bmp.Width * bmp.Height * 3];
                for (int y = 0; y < bmp.Height; y++)
                {
                    int Y = y < bmp.Height / 2 ? y + bmp.Height / 2 : y - bmp.Height / 2;
                    int initX = y * bmp.Width * 3;
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        int X = x < bmp.Width / 2 ? x + bmp.Width / 2 : x - bmp.Width / 2;
                        values[initX + x * 3 + 0] = scale[(int)(b[Y][X] * (scale.Length - 1))];
                        values[initX + x * 3 + 1] = scale[(int)(g[Y][X] * (scale.Length - 1))];
                        values[initX + x * 3 + 2] = scale[(int)(r[Y][X] * (scale.Length - 1))];
                    }
                }
                FromByte(bmp, values);
            }
        }*/

        /*public static Bitmap FromFFT(Complex[][] complexGray, byte[] scale)
        {
            if (bmp.Height != complexGray.Length) return;

            double[][] gray = Fourier.GetModulus(complexGray, true);
            if (bmp.PixelFormat == PixelFormat.Format24bppRgb)//24bitカラーモードのときだけ
            {
                byte[] values = new byte[bmp.Width * bmp.Height * 3];
                for (int y = 0; y < bmp.Height; y++)
                {
                    int Y = y < bmp.Height / 2 ? y + bmp.Height / 2 : y - bmp.Height / 2;
                    int initX = y * bmp.Width * 3;
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        int X = x < bmp.Width / 2 ? x + bmp.Width / 2 : x - bmp.Width / 2;
                        values[initX + x * 3 + 0] = values[initX + x * 3 + 1] = values[initX + x * 3 + 2] = scale[(int)(gray[Y][X] * (scale.Length - 1))];
                    }
                }
                FromByte(bmp , values);
            }
        }*/

        private static object lockObject = new object();

        public static double detectSkewAngle(Bitmap bmp)
        {
            int[] value = BitmapConverter.ToIntGray(bmp);
            //-3°から+3°の範囲を0.3度刻みでチェックする
            int width = bmp.Width;

            double startAngle = -2.0 * Math.PI / 180, endAngle = 2.0 * Math.PI / 180, stepAngle = 0.05 * Math.PI / 180;
            double bestAngle = 0;
            int bestR = int.MinValue;
            // WaitDlg wd = new WaitDlg();
            // wd.progressBar.Maximum = 1000000;
            //wd.Show();

            for (int refineCount = 0; refineCount < 3; refineCount++)
            {
                stepAngle = (endAngle - startAngle) / 50;
                Parallel.For(0, 51, i =>
                    {
                        double angle = startAngle + i * stepAngle;
                        int r = detectSkewAngle(value, width, angle);
                        if (r > bestR)
                            lock (lockObject)
                            {
                                bestR = r;
                                bestAngle = angle;
                            }
                    });

                startAngle = bestAngle - stepAngle * 2;
                endAngle = bestAngle + stepAngle * 2;
            }
            // wd.Close();
            if (bestAngle < -2.0 * Math.PI / 180 || bestAngle > 2.0 * Math.PI / 180)
                bestAngle = 0;
            return bestAngle;
        }

        //指定されたangleでのR値をかえすでりげーと
        private static int detectSkewAngle(int[] value, int width, double angle)
        {
            int height = value.Length / width;
            int[] sigma = new int[height];
            double stairStep, nextStair;

            //まずsigmaを0にリセット
            for (int i = 0; i < sigma.Length; i++)
                sigma[i] = 0;
            //段差の幅を計算
            stairStep = Math.Abs(1 / Math.Sin(angle));
            //配列位置posをリセット
            int pos = 0;
            //ここからyのループ
            for (int y = 0; y < height; y++)
            {
                //次の階段位置nextStairをリセット
                nextStair = stairStep;
                int currentY = y;
                pos = currentY * width;
                //xのループ
                for (int x = 0; x < width; x++)
                {
                    //階段を超えたら…
                    if (x > nextStair)
                    {
                        currentY += angle < 0 ? -1 : 1;
                        if (currentY < 0 || currentY >= height)
                            break;
                        pos = currentY * width + x;
                        nextStair += stairStep;
                    }
                    sigma[y] += value[pos];
                    pos++;
                }
            }

            int R = 0;
            for (int i = 0; i < sigma.Length - 2; i++)
                R += Math.Abs(sigma[i] - sigma[i + 2]);
            return R;
        }

        public static Bitmap ToGrayScaleBitmap(Bitmap bmp)
        {
            Bitmap bmpTemp = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format8bppIndexed);
            ColorPalette pal = bmpTemp.Palette;
            for (int i = 0; i < pal.Entries.Length; i++)
            {
                int gray = (int)(255.0 * i / (pal.Entries.Length - 1));
                pal.Entries[i] = Color.FromArgb(gray, gray, gray);
            }

            if (bmp.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                bmpTemp = (Bitmap)bmp.Clone(); ;
                bmpTemp.Palette = pal;
                return bmpTemp;
            }
            else
            {
                byte[] value = ToByteGray(bmp);
                bmpTemp.Palette = pal;
                FromByte(bmpTemp, value);
                return bmpTemp;
            }
        }
    }
}