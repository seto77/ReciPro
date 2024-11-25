using System;
using System.Collections.Generic;
using System.Linq;

namespace Crystallography
{
    public class FindParameter
    {
        public static double FindSphericalCorrection(double L1, double L2, double d1, double d2, double WaveLength, double filmDistance)
        {
            //真のL1/L2 = p
            double p = Math.Tan(2 * Math.Asin(WaveLength / 2 / d1)) / Math.Tan(2 * Math.Asin(WaveLength / 2 / d2));
            double R = filmDistance * (L1 - p * L2) / L1 / L2 / (L2 - L1);
            return R;
        }

        public static void FindSphericalCorrection(List<EllipseParameter> ellipses, double waveLength, double filmDistance, ref double radiusInverse, ref double radiusInverseDev)
        {
            double weightTotal = 0;
            double tempRtotal = 0;
            List<double> r = [];
            for (int i = 0; i < ellipses.Count; i++)
                for (int j = i + 1; j < ellipses.Count; j++)
                    for (int k = 0; k < ellipses[i].millimeters.Count; k++)
                        if (!double.IsNaN(ellipses[i].millimeters[k]) && !double.IsNaN(ellipses[j].millimeters[k]))
                        {
                            double obs1 = ellipses[i].millimeters[k];
                            double obs2 = ellipses[j].millimeters[k];
                            double d1 = ellipses[i].d;
                            double d2 = ellipses[j].d;

                            double tempR = FindParameter.FindSphericalCorrection(obs1, obs2, d1, d2, waveLength, filmDistance);
                            double sigma1 = FindParameter.FindSphericalCorrection(obs1, obs2 + 0.01, d1, d2, waveLength, filmDistance) - tempR;
                            if (sigma1 != 0)
                            {
                                weightTotal += 1 / (sigma1 * sigma1);
                                tempRtotal += tempR / (sigma1 * sigma1);
                                r.Add(tempR);
                            }
                        }

            if (weightTotal > 0 && !double.IsInfinity(weightTotal))
            {
                radiusInverse += tempRtotal / weightTotal;
                radiusInverseDev = Statistics.Deviation([.. r]) / Math.Sqrt(Math.Sqrt(2 * r.Count));
            }
        }

        /// <summary>
        /// 一枚の写真中の2つのピークの位置とd値からd1に対するSin(2θ)を返す関数
        /// </summary>
        /// <param name="L1"></param>
        /// <param name="L2"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="WaveLength"></param>
        /// <returns></returns>
        public static double GetSinTheta(double L1, double L2, double d1, double d2, double WaveLength)
        {
            double Y = L2 / L1;
            double X = d1 / d2;
            double Y2 = Y * Y;
            double X2 = X * X;
            double bestSinTheta, startSinTheta, endSinTheta, stepSinTheta;
            bestSinTheta = 0;
            startSinTheta = (WaveLength / 2 / d1) * (WaveLength / 2 / d1) * 0.1;
            endSinTheta = (WaveLength / 2 / d1) * (WaveLength / 2 / d1) * 4;
            stepSinTheta = (endSinTheta - startSinTheta) / 1000.0;
            double Residual, bestResidual;
            bestResidual = double.PositiveInfinity;
            double SinTheta;
            for (int n = 0; n < 25; n++)
            {
                for (SinTheta = startSinTheta; SinTheta <= endSinTheta; SinTheta += stepSinTheta)
                {
                    Residual = Math.Abs(-Y2 + X2 * ((1 - X2 * SinTheta) * (1 - 2 * SinTheta) * (1 - 2 * SinTheta) / (1 - 2 * X2 * SinTheta) / (1 - 2 * X2 * SinTheta) / (1 - SinTheta)));
                    if (Residual < bestResidual)
                    {
                        bestSinTheta = SinTheta;
                        bestResidual = Residual;
                    }
                }
                startSinTheta = bestSinTheta - stepSinTheta * 2.5;
                endSinTheta = bestSinTheta + stepSinTheta * 2.5;
                stepSinTheta *= 0.32;
            }
            return Math.Sqrt(bestSinTheta);
        }

        /// <summary>
        /// 一枚の写真中の2つのピークの位置とd値から波長を返す関数　初期値として現在の波長を渡す
        /// </summary>
        /// <param name="L1"></param>
        /// <param name="L2"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="WaveLength"></param>
        /// <returns></returns>
        public static double FindWaveLength2(double L1, double L2, double d1, double d2, double WaveLength)
        {
            return 2 * d1 * GetSinTheta(L1, L2, d1, d2, WaveLength);
        }

        /// <summary>
        /// 一枚の写真中の複数のピークを同時に渡してその加重平均から波長と誤差を計算する　(FindWaveLength2を内部的に呼び出す)
        /// </summary>
        /// <param name="p"></param>
        /// <param name="WaveLength"></param>
        /// <param name="WaveLengthDev"></param>
        public static void FindWaveLengthFromMultiPeaks(List<EllipseParameter> ellipses, ref double WaveLength, ref double WaveLengthDev)
        {
            if (ellipses == null || ellipses.Count < 1) return;
            double tempTotal = 0;
            double temp, sigma1, sigma2, weight;
            double weightTotal = 0;
            List<double> WL = [];
            for (int i = 0; i < ellipses.Count; i++)
                for (int j = i + 1; j < ellipses.Count; j++)
                    for (int k = 0; k < ellipses[i].millimeters.Count; k++)
                        if (!double.IsNaN(ellipses[i].millimeters[k]) && !double.IsNaN(ellipses[j].millimeters[k]))
                        {
                            double obs1 = ellipses[i].millimeters[k];
                            double obs2 = ellipses[j].millimeters[k];
                            double d1 = ellipses[i].d;
                            double d2 = ellipses[j].d;

                            temp = FindParameter.FindWaveLength2(obs1, obs2, d1, d2, WaveLength);
                            sigma1 = (FindParameter.FindWaveLength2(obs1 + 0.1, obs2, d1, d2, WaveLength) - temp);
                            sigma2 = (FindParameter.FindWaveLength2(obs1, obs2 + 0.1, d1, d2, WaveLength) - temp);
                            if (sigma1 != 0 || sigma2 != 0)
                            {
                                weight = 1 / (sigma1 * sigma1 + sigma2 * sigma2);
                                weightTotal += weight;
                                tempTotal += weight * temp;
                                WL.Add(temp);
                            }
                        }
            if (weightTotal > 0 && !double.IsInfinity(weightTotal))
            {
                WaveLength = tempTotal / weightTotal;
                WaveLengthDev = Statistics.Deviation([.. WL]) / Math.Sqrt(Math.Sqrt(2 * WL.Count));
            }
        }

        /// <summary>
        /// 一枚の写真中の複数のピークを同時に渡してその加重平均から波長と誤差を計算する　(FindWaveLength2を内部的に呼び出す)
        /// </summary>
        /// <param name="p"></param>
        /// <param name="WaveLength"></param>
        /// <param name="WaveLengthDev"></param>
        public static void FindWaveLengthFromMultiPeaks(List<Plane> plane, ref double WaveLength, ref double WaveLengthDev)
        {
            if (plane == null) return;
            double tempTotal = 0;
            double temp, sigma1, sigma2, weight;
            double weightTotal = 0;
            List<double> WL = [];
            for (int i = 0; i < plane.Count; i++)
                for (int j = i + 1; j < plane.Count; j++)
                    if (!double.IsNaN(plane[i].MillimeterObs) && !double.IsNaN(plane[j].MillimeterObs)
                        && plane[i].IsFittingChecked && plane[j].IsFittingChecked)
                    {
                        double obs1 = plane[i].MillimeterObs;
                        double obs2 = plane[j].MillimeterObs;
                        double d1 = plane[i].d;
                        double d2 = plane[j].d;

                        temp = FindParameter.FindWaveLength2(obs1, obs2, d1, d2, WaveLength);
                        sigma1 = (FindParameter.FindWaveLength2(obs1 + 0.1, obs2, d1, d2, WaveLength) - temp);
                        sigma2 = (FindParameter.FindWaveLength2(obs1, obs2 + 0.1, d1, d2, WaveLength) - temp);
                        if (sigma1 != 0 || sigma2 != 0)
                        {
                            weight = 1 / (sigma1 * sigma1 + sigma2 * sigma2);
                            weightTotal += weight;
                            tempTotal += weight * temp;
                            WL.Add(temp);
                        }
                    }
            if (weightTotal > 0 && !double.IsInfinity(weightTotal))
            {
                WaveLength = tempTotal / weightTotal;
                WaveLengthDev = Statistics.Deviation([.. WL]) / Math.Sqrt(Math.Sqrt(2 * WL.Count));
            }
        }

        /// <summary>
        /// 一枚の写真中の複数のピークと波長+カメラ長からピクセルサイズを計算する　(全パターンからの算出)
        /// </summary>
        /// <param name="p"></param>
        /// <param name="WaveLength"></param>
        /// <param name="WaveLengthDev"></param>
        public static void FindPixelSizeFromMultiPeaks(List<Plane> plane, double CameraLength, double WaveLength, ref double PixelSize, ref double PixelSizeDev)
        {
            if (plane == null) return;
            double tempTotal = 0;
            double temp, sigma1, sigma2, weight;
            double weightTotal = 0;
            List<double> CL = [];
            for (int i = 0; i < plane.Count; i++)
                for (int j = i + 1; j < plane.Count; j++)
                    if (!double.IsNaN(plane[i].MillimeterObs) && !double.IsNaN(plane[j].MillimeterObs)
                        && plane[i].IsFittingChecked && plane[j].IsFittingChecked)
                    {
                        double obs1 = plane[i].MillimeterObs;
                        double obs2 = plane[j].MillimeterObs;
                        double d1 = plane[i].d;
                        double d2 = plane[j].d;
                        temp = CameraLength * Math.Tan(2 * Math.Asin(GetSinTheta(obs1, obs2, d1, d2, WaveLength))) / obs1 * PixelSize;

                        sigma1 = CameraLength * Math.Tan(2 * Math.Asin(GetSinTheta(obs1 + 0.1, obs2, d1, d2, WaveLength))) / obs1 * PixelSize - temp;
                        sigma2 = CameraLength * Math.Tan(2 * Math.Asin(GetSinTheta(obs1, obs2 + 0.1, d1, d2, WaveLength))) / obs1 * PixelSize - temp;
                        if (sigma1 != 0 || sigma2 != 0)
                        {
                            weight = 1 / (sigma1 * sigma1 + sigma2 * sigma2);
                            weightTotal += weight;
                            tempTotal += weight * temp;
                            CL.Add(temp);
                        }
                    }
            if (weightTotal > 0 && !double.IsInfinity(weightTotal))
            {
                PixelSize = tempTotal / weightTotal;
                PixelSizeDev = Statistics.Deviation([.. CL]) / Math.Sqrt(Math.Sqrt(2 * CL.Count));
            }
        }

        /// <summary>
        /// 固定されたピクセルサイズとフィルム距離から波長をかえす
        /// </summary>
        /// <param name="p"></param>
        /// <param name="PixelSize"></param>
        /// <param name="FilmDistance"></param>
        /// <param name="WaveLength"></param>
        /// <param name="WaveLengthDev"></param>
        public static void FindWaveLengthFromFixedPixelSize(List<EllipseParameter> ellipse, double FilmDistance,
            ref double WaveLength, ref double WaveLengthDev)
        {
            //int n = 0;
            double tempTotal = 0;
            double weightTotal = 0;
            double weight, temp, diff;
            List<double> WL = [];
            for (int i = 0; i < ellipse.Count; i++)
            {
                temp = 2 * ellipse[i].d * Math.Sin(Math.Atan(ellipse[i].GetAverageRadius() / FilmDistance) / 2);
                diff = 2 * ellipse[i].d * Math.Sin(Math.Atan((ellipse[i].GetAverageRadius() + 0.1) / FilmDistance) / 2) - temp;
                weight = 1 / diff / diff;
                weightTotal += weight;
                tempTotal += weight * temp;
                WL.Add(temp);
            }
            if (weightTotal > 0 && !double.IsInfinity(weightTotal))
            {
                WaveLength = tempTotal / weightTotal;
                WaveLengthDev = Statistics.Deviation([.. WL]) / Math.Sqrt(WL.Count);
            }
        }

        /// <summary>
        /// １枚のイメージからフィルム距離を返す。波長と、各面の真の面間隔と実測した2θが必要
        /// </summary>
        /// <param name="p"></param>
        /// <param name="WaveLength"></param>
        /// <param name="FilmDistance"></param>
        /// <param name="FilmDistanceDev"></param>
        public static void FindFilmDistanceFromWaveLength(List<EllipseParameter> ellipses, double WaveLength, ref double FilmDistance, ref double FilmDistanceDev)
        {
            if (ellipses == null || ellipses.Count < 1) return;
            double tempTotal = 0;
            double weightTotal = 0;
            double temp, weight;
            List<double> FD = [];
            for (int i = 0; i < ellipses.Count; i++)
                for (int j = 0; j < ellipses[i].millimeters.Count; j++)
                    if (!double.IsNaN(ellipses[i].millimeters[j]))
                    {
                        temp = ellipses[i].millimeters[j] / Math.Tan(2 * Math.Asin(WaveLength / 2.0 / ellipses[i].d));
                        weight = 1;//Math.Pow(Math.Sin(Math.Atan(p[i].pixel1 * p[i].pixelSize / p[i].filmDistance1)), 2);
                        tempTotal += temp * weight;
                        weightTotal += weight;
                        FD.Add(temp);
                    }
            if (weightTotal > 0 && !double.IsInfinity(weightTotal))
            {
                FilmDistance = tempTotal / weightTotal;
                FilmDistanceDev = Statistics.Deviation([.. FD]) / Math.Sqrt(FD.Count);
            }
        }

        /// <summary>
        /// 2枚のフィルム距離の異なる画像からフィルム距離を返す。フィルム距離の差(誤差無し)が必要
        /// </summary>
        /// <param name="p"></param>
        /// <param name="FilmDistanceDiscrepancy"></param>
        /// <param name="FilmDistance"></param>
        /// <param name="FilmDistanceDev"></param>
        public static void FindFilmDistanceFromDiscrepancy(List<EllipseParameter> ellipses1, List<EllipseParameter> ellipses2, double Discrepancy, ref double FilmDistance, ref double FilmDistanceDev)
        {
            List<double> FD = [];
            double temp, dif1, dif2, weight;
            double tempTotal = 0;
            double weightTotal = 0;
            for (int i = 0; i < ellipses1.Count; i++)
                for (int j = 0; j < ellipses2.Count; j++)
                    for (int k = 0; k < ellipses1[i].millimeters.Count; k++)
                        if (ellipses1[i].strHKL == ellipses2[j].strHKL
                        && !double.IsNaN(ellipses1[i].millimeters[k]) && !double.IsNaN(ellipses2[j].millimeters[k]))
                        {
                            double m1 = ellipses1[i].millimeters[k];
                            double m2 = ellipses2[j].millimeters[k];

                            temp = FindCameraLength(m1, m2, Discrepancy);
                            dif1 = FindCameraLength(m1 + 0.1, m2, Discrepancy) - temp;
                            dif2 = FindCameraLength(m1, m2 + 0.1, Discrepancy) - temp;
                            weight = 1 / (dif1 * dif1 + dif2 * dif2) * 100;
                            weightTotal += weight;
                            tempTotal += weight * temp;
                            FD.Add(temp);
                        }
            if (weightTotal > 0 && !double.IsInfinity(weightTotal))
            {
                FilmDistance = tempTotal / weightTotal;
                FilmDistanceDev = Statistics.Deviation([.. FD]) / Math.Sqrt(FD.Count);
            }
        }

        /// <summary>
        /// 2枚のフィルム距離の異なる画像からフィルム距離を返す。フィルム距離の差(誤差無し)が必要
        /// </summary>
        /// <param name="p"></param>
        /// <param name="FilmDistanceDiscrepancy"></param>
        /// <param name="FilmDistance"></param>
        /// <param name="FilmDistanceDev"></param>
        public static void FindFilmDistanceFromDiscrepancy(List<Plane> plane1, List<Plane> plane2, double Discrepancy, ref double FilmDistance, ref double FilmDistanceDev)
        {
            List<double> FD = [];
            double temp, dif1, dif2, weight;
            double tempTotal = 0;
            double weightTotal = 0;
            for (int i = 0; i < plane1.Count; i++)
                for (int j = 0; j < plane2.Count; j++)
                    if (plane1[i].strHKL == plane2[j].strHKL
                         && !double.IsNaN(plane1[i].MillimeterObs) && !double.IsNaN(plane2[j].MillimeterObs)
                        && plane1[i].IsFittingChecked && plane2[j].IsFittingChecked)
                    {
                        double m1 = plane1[i].MillimeterObs;
                        double m2 = plane2[j].MillimeterObs;

                        temp = FindCameraLength(m1, m2, Discrepancy);
                        dif1 = FindCameraLength(m1 + 0.1, m2, Discrepancy) - temp;
                        dif2 = FindCameraLength(m1, m2 + 0.1, Discrepancy) - temp;
                        weight = 1 / (dif1 * dif1 + dif2 * dif2) * 100;
                        weightTotal += weight;
                        tempTotal += weight * temp;
                        FD.Add(temp);
                    }
            if (weightTotal > 0 && !double.IsInfinity(weightTotal))
            {
                FilmDistance = tempTotal / weightTotal;
                if (FD.Count > 1)
                    FilmDistanceDev = Statistics.Deviation([.. FD]) / Math.Sqrt(FD.Count - 1);
                else
                    FilmDistanceDev = 0;
            }
        }

        private static double FindCameraLength(double L1, double L2, double Discrepancy)
        {
            return Discrepancy / (L2 - L1) * L1;
        }

        /*
        public static void FindPixelSize(RingParameter[] p, double WaveLength, double[] FilmDistance, ref double PixelSize,ref double PixelSizeDev)
        {
            //int n = 0;
            double tempTotal = 0;
            double weightTotal = 0;
            List<double> PS = new List<double>();
            double temp,weight,sigma;
            for (int i = 0; i < p.Length; i++)
            {
                if (p[i].IsChecked)
                {
                    temp = FilmDistance[0] / p[i].millimeter1 * Math.Tan(2 * Math.Asin(WaveLength / 2 / p[i].d));
                    sigma = FilmDistance[0] / (p[i].millimeter1 + 0.1) * Math.Tan(2 * Math.Asin(WaveLength / 2 / p[i].d));
                    weight = 1 / sigma / sigma;
                    tempTotal += temp * weight;
                    weightTotal += weight;
                    PS.Add(temp);

                    if (!p[i].Is1ImageMode)
                    {
                        temp = FilmDistance[1] / p[i].millimeter2 * Math.Tan(2 * Math.Asin(WaveLength / 2 / p[i].d));
                        sigma = FilmDistance[1] / (p[i].millimeter2 + 0.1) * Math.Tan(2 * Math.Asin(WaveLength / 2 / p[i].d));
                        weight = 1 / sigma / sigma;
                        tempTotal += temp * weight;
                        weightTotal += weight;
                        PS.Add(temp);
                    }
                }
            }
            if (weightTotal > 0 && !double.IsInfinity(weightTotal))
            {
                PixelSize = tempTotal / weightTotal;
                PixelSizeDev = Statistics.Deviation(PS.ToArray()) / Math.Sqrt(PS.Count);
            }
        }*/
    }

    public class EllipseParameter
    {
        public List<PointD> points = [];
        public List<double> millimeters = [];
        public double[] Coeff = new double[5];
        public double millimeterCalc;
        public bool IsValid;
        public double d;
        public string strHKL;

        public EllipseParameter()
        {
            millimeters.Clear();
            points.Clear();
        }

        public double GetAverageRadius()
        {
            var temp = new List<double>();
            for (int i = 0; i < millimeters.Count; i++)
                if (!double.IsNaN(millimeters[i]))
                    temp.Add(millimeters[i]);
            return temp.Average();
        }

        public void Add(double MilliMeter, double Angle)
        {
            millimeters.Add(MilliMeter);
            points.Add(new PointD(MilliMeter * Math.Cos(Angle), MilliMeter * Math.Sin(Angle)));
        }

        public void Clear()
        {
            millimeters.Clear();
            points.Clear();
        }

        public void SetCoeff()
        {
            Coeff = Geometry.GetParameterOfCurveOfSecondaryDegree([.. points]);
        }

        public override string ToString()
        {
            if (millimeters.Count < 1)
                return "         ";
            else
                return millimeters[^1].ToString("0000.0000");
        }
    }

    public class PlaneParameters
    {
        public double millimeterObs;
        public double millimeterCalc;
        public PeakFunction pvp;
        public string strHKL;
        public bool isThisChecked;
        public double d;

        public bool isOneImageMode;

        //2ImageModeのときのコンストラクタ
        public PlaneParameters(string StrHKL, double millimeterObs1, double millimeterObs2, double millimeterCalc1, double millimeterCalc2, bool IsThisChecked, double D)
        {
            strHKL = StrHKL;
            //millimeterObs = new double[2];
            //millimeterCalc = new double[2];
            //pvp=new PeakFunction[2];
            pvp = new PeakFunction
            {
                X = double.NaN
            };
            //pvp = new PeakFunction();
            //pvp[1].X = double.NaN;

            d = D;
        }

        //1ImageModeのときのコンストラクタ
        public PlaneParameters(string StrHKL, double millimeterObs1, double millimeterCalc1, bool IsThisChecked)
        {
            strHKL = StrHKL;
            //millimeterObs = new double[2];
            //millimeterCalc = new double[2];

            pvp = new PeakFunction();
        }

        public override string ToString()
        {
            //string str0,str1;
            if (!double.IsNaN(millimeterObs) && millimeterObs != 0)
                return millimeterObs.ToString("000.000");
            else
                return "";
        }
    }
}