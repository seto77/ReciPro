using System;
using System.Collections.Generic;

namespace Crystallography
{
    public class Convolution
    {
        static public List<bool> BlurPixels(List<bool> srcPixels, int width, int radius)
        {
            return new List<bool>(BlurPixels(srcPixels.ToArray(), width, radius));
        }

        static public bool[] BlurPixels(bool[] srcPixels, int width, int radius)
        {
            int height = srcPixels.Length / width;
            bool[] destPixels = new bool[srcPixels.Length];
            for (int h = 0; h < height; h++)
                for (int w = 0; w < width; w++)
                {
                    if (srcPixels[h * width + w])
                    {
                        for (int j = Math.Max(0, h - radius); j < Math.Min(height, h + radius + 1); j++)
                            for (int i = Math.Max(0, w - radius); i < Math.Min(width, w + radius + 1); i++)
                                if (!destPixels[j * width + i] && (w - i) * (w - i) + (h - j) * (h - j) < radius * radius)
                                    destPixels[j * width + i] = true;
                    }
                }
            return destPixels;
        }

        /*
        double[] blur = new double[0];
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
                if (blur.Length != limit)
                {
                    blur = new double[limit];
                    double sum = 0;

                    for (int h = 0; h < limit; h++)
                    {
                        blur[h] = Math.Exp(-(h - center) * (h - center) / filmBlurPixel / filmBlurPixel * Math.Log(2));
                        sum += blur[h];
                    }
                    for (int i = 0; i < blur.Length; i++)
                        blur[i] /= sum;
                }
                double[] pixelsFilmBlur1 = new double[ImageWidth * ImageHeight];

                for (int n = 0; n < blur.Length; n++)
                    for (int h = 0; h < ImageHeight; h++)
                        for (int w = Math.Max(center - n, 0); w < Math.Min(ImageWidth, ImageWidth + center - n); w++)
                            pixelsFilmBlur1[h * ImageWidth + w] += blur[n] * pixels[h * ImageWidth + w + n - center];

                double[] pixelsFilmBlur2 = new double[ImageWidth * ImageHeight];

                for (int n = 0; n < blur.Length; n++)
                    for (int h = Math.Max(center - n, 0); h < Math.Min(ImageHeight, ImageHeight + center - n); h++)
                        for (int w = 0; w < ImageWidth; w++)
                            pixelsFilmBlur2[h * ImageWidth + w] += blur[n] * pixelsFilmBlur1[(h + n - center) * ImageWidth + w];
                return pixelsFilmBlur2;
            }
        }*/
    }
}