using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Crystallography
{
    public enum PeakFunctionForm
    {
        Simple,
        PseudoVoigt,
        Peason,
        SplitPseudoVoigt,
        SplitPearson
    }

    [Serializable]
    public class PeakFunction : IComparable
    {
        public PeakFunctionForm Option = PeakFunctionForm.PseudoVoigt;
        public double intensity;
        public double eta;
        public double etaH;
        public double etaL;
        public double Rl;
        public double Rh;
        public double Hk;
        public double X;
        public double Xerr;
        public double Int;
        public double A;
        public double B1, B2, B3;
        public double m;
        public double range;
        public double Residual;

        public int GroupIndex;
        public Color Color;

        /// <summary>
        /// Fitting時に、PeakTop(X値)を探すか。falseの場合は、初期X値をそのまま使う
        /// </summary>
        public bool SerchPeakTop = false;

        public int CompareTo(object o)
        {
            return X.CompareTo(((PeakFunction)o).X);
        }

        public PeakFunction()
        {
        }

        public PeakFunction(double X, double Hk, double range, PeakFunctionForm Option)
        {
            this.X = X;
            this.Hk = Hk;
            this.range = range;
            this.Option = Option;
        }

        public PeakFunction Copy()
        {
            PeakFunction p = new PeakFunction();
            p.Option = this.Option;
            p.intensity = this.intensity;
            p.eta = this.eta;
            p.etaH = this.etaH;
            p.etaL = this.etaL;
            p.Rl = this.Rl;
            p.Rh = this.Rh;
            p.X = this.X;
            p.Hk = this.Hk;
            p.Int = this.Int;
            p.A = this.A;//非対称パラメータ
            p.B1 = this.B1;
            p.B2 = this.B2;
            p.B3 = this.B3;
            p.m = this.m;
            p.range = this.range;
            return p;
        }

        public int GetParamNumber()
        {
            if (Option == PeakFunctionForm.PseudoVoigt)
                return 4;
            else if (Option == PeakFunctionForm.Peason)
                return 4;
            else if (Option == PeakFunctionForm.SplitPseudoVoigt)
                return 6;
            else if (Option == PeakFunctionForm.SplitPearson)
                return 6;
            else
                return int.MinValue;
        }

        public void RenewParameter()
        {
            if (Option == PeakFunctionForm.Peason)
                RenewParamPearsonIV();
            else if (Option == PeakFunctionForm.SplitPseudoVoigt)
                RenewParamSplitPseudoVoigt();
            else if (Option == PeakFunctionForm.SplitPearson)
                RenewParamSplitPearsonIV();
        }

        public double GetValue(double x, bool IsNewParamNeeded, bool withBackground = false)
        {
            if (IsNewParamNeeded)
                RenewParameter();
            double y = 0;

            if (Option == PeakFunctionForm.PseudoVoigt)
                y = PseudoVoigt(x);
            else if (Option == PeakFunctionForm.Peason)
                y = PeasonIV(x);
            else if (Option == PeakFunctionForm.SplitPseudoVoigt)
                y = SplitPseudoVoigt(x);
            else if (Option == PeakFunctionForm.SplitPearson)
                y = SplitPearsonIV(x);
            else
                y = 0;

            if (withBackground)
                y += B1 + B2 * (x - X);
            return y;
        }

        public double[] GetDifferentialValue(double x, bool IsNewParamNeeded)
        {
            if (IsNewParamNeeded)
                RenewParameter();

            if (Option == PeakFunctionForm.PseudoVoigt)
                return differectialPseudoVoigt(x);
            else if (Option == PeakFunctionForm.Peason)
                return differectialPeasonIV(x);
            else if (Option == PeakFunctionForm.SplitPseudoVoigt)
                return differentialSplitPseudoVoigt(x);
            else if (Option == PeakFunctionForm.SplitPearson)
                return differectialSplitPeasonIV(x);
            else
                return null;
        }

        public double GetIntegral()
        {
            if (Option == PeakFunctionForm.PseudoVoigt)
                return Int;
            else if (Option == PeakFunctionForm.SplitPseudoVoigt)
                return Int * Hk * PI / 2 * (1 / (1 + Math.Exp(-A)) / (etaL + SqrtLn2PI * (1 - etaL)) + 1 / (1 + Math.Exp(A)) / (etaH + SqrtLn2PI * (1 - etaH)));
            else if (Option == PeakFunctionForm.Peason)
                return Hk * Int * Math.Sqrt(Math.PI) * GammaFunction.gamma(m - 0.5) / 2 / Math.Sqrt(Math.Pow(2, 1 / m) - 1) / GammaFunction.gamma(m);
            else if (Option == PeakFunctionForm.SplitPearson)
            {
                double gh1 = GammaFunction.gamma(Rh);
                double gh2 = GammaFunction.gamma(Rh - 0.5);
                double gl1 = GammaFunction.gamma(Rl);
                double gl2 = GammaFunction.gamma(Rl - 0.5);
                double a1 = Math.Pow(2, 1 / Rl);
                double a2 = Math.Pow(2, 1 / Rh);
                return ((gh2 / (Math.Sqrt(-1 + a2) * gh1) + (A * gl2) / (Math.Sqrt(-1 + a1) * gl1)) * Hk * Int * Math.Sqrt(Math.PI)) / (2 * (1 + A));
            }
            else
                return 0;
        }

        private static readonly double SqrtLn2PI = Math.Sqrt(Math.Log(2) * Math.PI);
        private static readonly double SqrtLn2PerPI = Math.Sqrt(Math.Log(2) / Math.PI);
        private static readonly double Ln2 = Math.Log(2);
        private static readonly double Ln2Pow15 = Math.Sqrt(Ln2) * Ln2;
        private static readonly double PI = Math.PI;

        //static double pi = 1 / PI;
        private static readonly double SqrtPI = Math.Sqrt(PI);

        private double Z1, Z2, a1, a2, a3, a4, a5, a6, a7, a8, a9,
            b, b1, b2, b3, b4, b5, b6, b7;

        //Pseudo Voigt ここから
        private double PseudoVoigt(double x)
        {
            double Fourx2PerHk2 = 4 * (x - X) * (x - X) / Hk / Hk;
            return Int * ((eta / Math.PI / (1 + Fourx2PerHk2) + (1 - eta) * SqrtLn2PerPI * Math.Exp(-Ln2 * Fourx2PerHk2)) * 2 / Hk);
        }

        private double[] differectialPseudoVoigt(double x)
        {
            double tmp3 = (X - x) * (X - x);
            double tmp2 = Hk * Hk;
            double tmp1 = tmp3 / tmp2;
            double ExpMinus4Ln2tmp1 = Math.Exp(-4 * Ln2 * tmp1);
            double[] d = new double[4];
            d[0] = 2 / Hk / PI * (eta / (1 + 4 * tmp1) + (1 - eta) * SqrtLn2PI * ExpMinus4Ln2tmp1);
            d[1] = Int * (2 / Hk / PI * (1 / (1 + 4 * tmp1) - SqrtLn2PI * ExpMinus4Ln2tmp1));
            d[2] = Int * (-2 * eta * (tmp2 - 4 * tmp3) / PI / (tmp2 + 4 * tmp3) / (tmp2 + 4 * tmp3) + 2 * (eta - 1) * SqrtLn2PerPI * (tmp2 - 8 * tmp3 * Ln2) / (tmp2 * tmp2) * ExpMinus4Ln2tmp1);
            d[3] = Int * 16 * (X - x) / (tmp2 * Hk) / PI * (-eta / (1 + 4 * tmp1) / (1 + 4 * tmp1) + (eta - 1) * SqrtLn2PI * Ln2 * ExpMinus4Ln2tmp1);
            return d;
        }

        //Pseudo Voigt ここまで

        //Split Pseudo Voigt ここから
        private void RenewParamSplitPseudoVoigt()
        {
            Z1 = 1 / Math.Exp(A) + 1;
            Z2 = Math.Exp(A) + 1;
            a1 = etaL - 1;
            a2 = etaH - 1;
            a3 = a1 * SqrtLn2PI;
            a4 = a2 * SqrtLn2PI;
            a5 = 1 / (etaL - a3);
            a6 = 1 / (etaH - a4);
            a7 = 1 / Hk;
            a8 = a1 * Ln2Pow15 * SqrtPI;
            a9 = a2 * Ln2Pow15 * SqrtPI;
        }

        private double SplitPseudoVoigt(double x)
        {
            b = x - X;

            if (b < 0)
            {
                b1 = b * b * a7 * a7 * Z1 * Z1;
                b3 = 1 / (1 + b1);
                b5 = Math.Pow(2, -b1);
                return a5 * (-a3 * b5 + b3 * etaL) * Int;
            }
            else
            {
                b2 = b * b * a7 * a7 * Z2 * Z2;
                b4 = 1 / (1 + b2);
                b6 = Math.Pow(2, -b2);
                return a6 * (-a4 * b6 + b4 * etaH) * Int;
            }
        }

        private double[] differentialSplitPseudoVoigt(double x)
        {
            b = x - X;
            double[] d = new double[6];

            if (b < 0)
            {
                b1 = b * b * a7 * a7 * Z1 * Z1;
                b3 = 1 / (1 + b1);
                b5 = Math.Pow(2, -b1);

                d[0] = a5 * (-(a3 * b5) + b3 * etaL);
                d[1] = 2 * a5 * a7 * a7 * a7 * b * b * (-(a8 * b5) + b3 * b3 * etaL) * Int * Z1 * Z1;
                d[2] = (2 * a5 * b1 * (-(a8 * b5) + b3 * b3 * etaL) * Int) / Z2;
                d[3] = -((a5 * a5 * Int * SqrtLn2PI * ((-1 + b5) * Hk * Hk + b * b * b5 * Z1 * Z1)) / (Hk * Hk + b * b * Z1 * Z1));
                d[4] = 0;
                d[5] = 2 * a5 * a7 * a7 * b * (-(a8 * b5) + b3 * b3 * etaL) * Int * Z1 * Z1;
            }
            else
            {
                b2 = b * b * a7 * a7 * Z2 * Z2;
                b4 = 1 / (1 + b2);
                b6 = Math.Pow(2, -b2);

                d[0] = a6 * (-(a4 * b6) + b4 * etaH);
                d[1] = 2 * a6 * a7 * a7 * a7 * b * b * (-(a9 * b6) + b4 * b4 * etaH) * Int * Z2 * Z2;
                d[2] = (2 * a6 * b2 * (a9 * b6 - b4 * b4 * etaH) * Int) / Z1;
                d[3] = 0;
                d[4] = -((a6 * a6 * Int * SqrtLn2PI * ((-1 + b6) * Hk * Hk + b * b * b6 * Z2 * Z2)) / (Hk * Hk + b * b * Z2 * Z2));
                d[5] = 2 * a6 * a7 * a7 * b * (-(a9 * b6) + b4 * b4 * etaH) * Int * Z2 * Z2;
            }
            return d;
        }

        //Split Pseudo Voigt ここまで

        //Pearson VII ここから
        private void RenewParamPearsonIV()
        {
            a1 = 1 / Hk;
            a2 = Math.Pow(2, 1.0 / m);
        }

        private double PeasonIV(double x)
        {
            a3 = x - X;
            a4 = a2 - 1;
            a5 = 1 + 4 * a1 * a1 * a3 * a3 * a4;
            a6 = Math.Pow(a5, -m);
            return Int * a6;
        }

        private double[] differectialPeasonIV(double x)
        {
            double[] d = new double[4];
            a3 = x - X;
            a4 = a2 - 1;
            a5 = 1 + 4 * a1 * a1 * a3 * a3 * a4;
            a6 = Math.Pow(a5, -m);
            d[0] = a6;
            d[1] = (8 * a1 * a1 * a1 * a3 * a3 * a4 * a6 * Int * m) / a5;
            d[2] = a6 * Int * ((4 * a2 * a3 * a3 * Ln2) / ((4 * a3 * a3 * a4 + Hk * Hk) * m) - Math.Log(a5));
            d[3] = (8 * a1 * a1 * a3 * a4 * a6 * Int * m) / a5;
            return d;
        }

        //Pearson VII ここまで

        //Split Pearson VII ここから
        private void RenewParamSplitPearsonIV()
        {
            Z1 = 1 / Math.Exp(A) + 1;
            Z2 = Math.Exp(A) + 1;
            a1 = Math.Pow(2, 1 / Rl);
            a2 = Math.Pow(2, 1 / Rh);
            a3 = a1 - 1;
            a4 = a2 - 1;
            a5 = 1 / Hk;
            a6 = a3 * a5 * a5;
            a7 = a4 * a5 * a5;
            a8 = Int * Z1 * Z1;
            a9 = Int * Z2 * Z2;
        }

        private double SplitPearsonIV(double x)
        {
            b1 = x - X;
            if (b1 < 0)
                return Int * Math.Pow(1 + a6 * b1 * b1 * Z1 * Z1, -Rl);
            else
                return Int * Math.Pow(1 + a7 * b1 * b1 * Z2 * Z2, -Rh);
        }

        private double[] differectialSplitPeasonIV(double x)
        {
            b1 = x - X;
            double[] d = new double[6];

            if (b1 < 0)
            {
                b2 = 1 + a6 * b1 * b1 * Z1 * Z1;
                b4 = Math.Pow(b2, -Rl);
                b6 = b1 * b1 * b4;

                d[0] = b4;
                d[1] = (2 * a5 * a6 * a8 * b6 * Rl) / b2;
                d[2] = (2 * a6 * a8 * b6 * Rl) / (b2 * Z2);
                d[3] = (a1 * a5 * a5 * a8 * b6 * Ln2) / (b2 * Rl) - b4 * Int * Math.Log(b2);
                d[4] = 0;
                d[5] = (2 * a6 * a8 * b1 * b4 * Rl) / b2;
            }
            else
            {
                b3 = 1 + a7 * b1 * b1 * Z2 * Z2;
                b5 = Math.Pow(b3, -Rh);
                b7 = b1 * b1 * b5;

                d[0] = b5;
                d[1] = (2 * a5 * a7 * a9 * b7 * Rh) / b3;
                d[2] = (-2 * a7 * a9 * b7 * Rh) / (b3 * Z1);
                d[3] = 0;
                d[4] = (a2 * a5 * a5 * a9 * b7 * Ln2) / (b3 * Rh) - b5 * Int * Math.Log(b3);
                d[5] = (2 * a7 * a9 * b1 * b5 * Rh) / b3;
            }
            return d;
        }

        //Split Pearson VII ここまで
    }

    public class FittingPeak
    {
        private static readonly double SqrtLn2PI = Math.Sqrt(Math.Log(2) * Math.PI);

        //static double SqrtLn2PerPI = Math.Sqrt(Math.Log(2) / Math.PI);
        private static readonly double Ln2 = Math.Log(2);

        private static readonly double PI = Math.PI;

        public FittingPeak()
        {
            //
            // TODO: コンストラクタ ロジックをここに追加してください。
            //
        }

        //angle から±range内でピークをサーチせよ
        public static PointD FitPeakAsSimple(double angle, double range, PointD[] pt)
        {
            double temp = 0;
            double x = 0;
            for (int i = 0; i < pt.Length; i++)
                if (Math.Abs(pt[i].X - angle) <= range)
                    if (temp < pt[i].Y)
                    {
                        temp = pt[i].Y;
                        x = pt[i].X;
                    }
            return new PointD(x, temp);
        }

        public delegate void FitPeakDelegate(PointD[] pt, bool BackgroundFitting, double RemoveBadSN, ref PeakFunction p);

        public static void FitPeakThread(PointD[] pt, bool BackgroundFitting, double RemoveBadSN, ref PeakFunction p)
        {
            PeakFunction[] param = new PeakFunction[1];
            param[0] = p;
            FitMultiPeaksThread(pt, BackgroundFitting, RemoveBadSN, ref param);
            p = param[0];
        }

        public delegate bool FitMultiPeaksDelegate(PointD[] pt, bool BackgroundFitting, double RemoveBadSN, ref PeakFunction[] p);

        /// <summary>
        /// 複数ピークをフィッティングする. 戻り値は、R値
        /// </summary>
        /// <param name="pt">フィッティング対象プロファイルのデータ配列</param>
        /// <param name="BackgroundFitting">バックグラウンドをフィッティングするかどうか</param>
        /// <param name="RemoveBadSN">SN比の悪いピークについてはNaNを返すかどうか</param>
        /// <param name="p">ピーク関数 格納されている中心値、半値幅、フィッティングRangeを設定しておく</param>
        public static double FitMultiPeaksThread(PointD[] pt, bool BackgroundFitting, double RemoveBadSN, ref PeakFunction[] p)
        {
            //マルカールの方法でPseudoVoigtをとく。高速になるはず

            if (p.Length == 0)
                return double.PositiveInfinity;
            if (pt == null || pt.Length < 3)
                return double.PositiveInfinity;

            //まずここからプロファイルをとる領域や強度ななどの情報を集める
            var PtX = new List<double>();
            var PtY = new List<double>();
            double sum = 0;
            double temp = double.NegativeInfinity;
            double tempMin = double.PositiveInfinity;

            //指定された範囲内のプロファイルをPtX,PtYに格納
            for (int i = 0; i < pt.Length; i++)
                if (pt[i].X >= Math.Min(p[0].X - p[0].range, p[^1].X - p[^1].range) && pt[i].X <= Math.Max(p[0].X + p[0].range, p[^1].X + p[^1].range))
                {
                    PtX.Add(pt[i].X);
                    PtY.Add(pt[i].Y / 1000);
                    sum += pt[i].Y / 1000;
                    if (temp < pt[i].Y / 1000)
                        temp = pt[i].Y / 1000;
                }

            //p[i].SerchPeakTop
            for (int i = 0; i < p.Length; i++)
                if (p[i].SerchPeakTop)
                {
                    double max = double.NegativeInfinity;
                    double tempX = 0;
                    for (int j = 0; j < PtX.Count; j++)
                        if (PtX[j] >= p[i].X - p[i].range && PtX[j] <= p[0].X + p[0].range)
                            if (max < PtY[j])
                            {
                                max = PtY[j];
                                tempX = PtX[j];
                            }
                    p[i].X = tempX;
                }

            //最も小さい強度を探す
            for (int i = 0; i < pt.Length; i++)
                if (pt[i].X >= Math.Min(p[0].X - p[0].range, p[^1].X - p[^1].range) && pt[i].X <= Math.Max(p[0].X + p[0].range, p[^1].X + p[^1].range))
                {
                    if (tempMin > pt[i].Y / 1000)
                        tempMin = pt[i].Y / 1000;
                }

            if (PtY.Count < 3 || /*temp <= 0 ||*/ (PtY[PtY.Count - 1] == 0 && PtY[PtY.Count - 2] == 0 && PtY[PtY.Count - 3] == 0))
            {
                for (int i = 0; i < p.Length; i++)
                {
                    p[i].X = double.NaN;
                    p[i].Int = double.NaN;
                }
                return double.PositiveInfinity;
            }

            double[] x = PtX.ToArray();
            double[] y = PtY.ToArray();
            int length = x.Length;
            //ここまででプロファイルの領域などを決定

            //ここから決めなければいけないパラメータの数を設定する
            int ParamNum = 0;
            for (int i = 0; i < p.Length; i++)
                ParamNum += p[i].GetParamNumber();
            if (ParamNum < 0)
            {
                for (int i = 0; i < p.Length; i++)
                {
                    p[i].X = double.NaN;
                    p[i].Int = double.NaN;
                }
                return double.PositiveInfinity;
            }

            var diff = new double[ParamNum + 2, length];
            var Alpha = new DenseMatrix(ParamNum + 2, ParamNum + 2);
            var Beta = new DenseMatrix(ParamNum + 2, 1);

            var pCurrent = new PeakFunction[p.Length];
            var pNew = new PeakFunction[p.Length];
            for (int i = 0; i < p.Length; i++)
            {
                pCurrent[i] = new PeakFunction();
                pNew[i] = new PeakFunction();
                pCurrent[i] = p[i].Copy();
            }
            //ここまで

            //Intの値を大雑把に決める
            var peakIntensity = new double[p.Length];
            for (var i = 0; i < p.Length; i++)
            {
                var d = double.PositiveInfinity;
                for (int j = 0; j < PtX.Count; j++)
                    if (d > Math.Abs(x[j] - p[i].X))
                    {
                        d = Math.Abs(x[j] - p[i].X);
                        peakIntensity[i] = y[j];
                    }
                peakIntensity[i] -= tempMin;
            }
            //相対値として再配分する
            peakIntensity = Statistics.Normarize(peakIntensity);

            double[] ResidualCurrent = new double[length];
            double[] ResidualNew = new double[length];
            double ResidualSquareCurrent;
            double ResidualSquareNew = 0;
            double residual;
            double centerX = (x[0] + x[x.Length - 1]) / 2;
            double B1, B2, B1_New, B2_New;
            B1 = B2 = 0;

            double bestResidual = double.PositiveInfinity;
            int bestInitial = 0;
            int startInitial = 0;
            int endInitial = 1;// 2;
            int counter = 0;

            for (int Initial = startInitial; Initial < endInitial; Initial++)
            {
                double[] c = new double[3];
                switch (Initial)
                {
                    case 00: c = new double[] { 1, 1, 1 }; break;
                    case 01: c = new double[] { 1, 1, 2 }; break;
                    case 02: c = new double[] { 1, 1, 0.5 }; break;
                    case 03: c = new double[] { 0.5, 1, 1 }; break;
                    case 04: c = new double[] { 0.5, 1, 2 }; break;
                    case 05: c = new double[] { 0.5, 1, 0.5 }; break;
                    case 06: c = new double[] { 2, 1, 1 }; break;
                    case 07: c = new double[] { 2, 1, 0.5 }; break;
                    case 08: c = new double[] { 2, 1, 2 }; break;
                    case 09: c = new double[] { 1, 2, 1 }; break;
                    case 10: c = new double[] { 1, 0.5, 1 }; break;
                    case 11: c = new double[] { 0.5, 2, 2 }; break;
                }
                //ここから初期値をきめる
                //Xはすでに代入済み
                //Int以外のの初期値を大雑把に決める
                for (int i = 0; i < p.Length; i++)
                {
                    pCurrent[i].X = p[i].X;

                    if (p[i].Hk > 0)
                        pCurrent[i].Hk = p[i].Hk * c[0];
                    else
                        pCurrent[i].Hk = p[i].range * 0.5 * c[0];

                    pCurrent[i].eta = 0.5 * c[1];
                    pCurrent[i].etaH = 0.5 * c[1];
                    pCurrent[i].etaL = 0.5 * c[1];
                    pCurrent[i].m = 2 * c[1];
                    pCurrent[i].Rl = 2 * c[1];
                    pCurrent[i].Rh = 2 * c[1];
                    pCurrent[i].A = 0;
                }
                B1 = tempMin;
                B2 = 0;
                //Int
                for (int i = 0; i < p.Length; i++)
                {
                    pCurrent[i].Int = 1;
                    pCurrent[i].Int = (sum - tempMin * x.Length) * (x[1] - x[0]) * peakIntensity[i] / pCurrent[i].GetIntegral() * c[2];
                }
                //ここまで初期値決め

                //現在の残差を計算
                ResidualSquareCurrent = 0;
                double[] IntCurrent = new double[x.Length];
                double[] IntNew = new double[x.Length];
                for (int i = 0; i < length; i++)
                    IntCurrent[i] = B1 + B2 * (x[i] - centerX);
                for (int j = 0; j < p.Length; j++)
                {
                    pCurrent[j].RenewParameter();
                    for (int i = 0; i < length; i++)
                        IntCurrent[i] += pCurrent[j].GetValue(x[i], false);
                }
                for (int i = 0; i < length; i++)
                {
                    ResidualCurrent[i] = y[i] - IntCurrent[i];
                    ResidualSquareCurrent += ResidualCurrent[i] * ResidualCurrent[i];
                }

                double ramda = 100;
                counter = 0;
                do
                {
                    counter++;
                    //偏微分を作る
                    int n = 0;
                    int m = 0;
                    for (int j = 0; j < p.Length; j++)
                    {
                        n = m;
                        pCurrent[j].RenewParameter();
                        for (int i = 0; i < length; i++)
                        {
                            m = n;
                            double[] d = pCurrent[j].GetDifferentialValue(x[i], false);
                            for (int k = 0; k < d.Length; k++)
                            {
                                diff[m, i] = d[k];
                                m++;
                            }
                        }
                    }
                    for (int i = 0; i < length; i++)
                    {//バックグラウンド変数の偏微分
                        diff[ParamNum, i] = 1;
                        diff[ParamNum + 1, i] = x[i] - centerX;
                    }
                    //偏微分を作る ここまで

                    //行列Alpha, Betaを作る
                    for (int i = 0; i < ParamNum + 2; i++)
                    {
                        for (int j = i; j < ParamNum + 2; j++)
                        {
                            Alpha[i, j] = 0;
                            for (int k = 0; k < length; k++)
                                Alpha[i, j] += diff[i, k] * diff[j, k];
                            Alpha[j, i] = Alpha[i, j];
                            if (i == j)
                                Alpha[i, j] *= (1 + ramda);
                        }
                        Beta[i, 0] = 0;
                        for (int k = 0; k < length; k++)
                            Beta[i, 0] += ResidualCurrent[k] * diff[i, k];
                    }
                    //行列Alpha、Beta、ここまで

                    var alphaInv = Alpha.TryInverse();

                    if (alphaInv == null)
                    {
                        for (int i = 0; i < p.Length; i++)
                        {
                            p[i].X = double.NaN;
                            p[i].Int = double.NaN;
                        }
                        return double.PositiveInfinity;
                    }

                    var delta = alphaInv * Beta;

                    //新しいパラメータをセットして適宜修正
                    n = 0;
                    bool flag = true;
                    for (int j = 0; j < p.Length; j++)
                    {
                        pNew[j] = pCurrent[j].Copy();
                        if (p[j].Option == PeakFunctionForm.PseudoVoigt)//PseudoVoigt:      Int, Eta, Hk, X ;
                        {
                            pNew[j].Int = pCurrent[j].Int + delta[n++, 0];
                            pNew[j].eta = pCurrent[j].eta + delta[n++, 0];
                            pNew[j].Hk = pCurrent[j].Hk + delta[n++, 0];
                            pNew[j].X = pCurrent[j].X + delta[n++, 0];

                            //if (pNew[j].eta < 0 || pNew[j].eta > 1)
                            //    flag = false;
                            if (pNew[j].eta < 0)
                                pNew[j].eta = 0;
                            if (pNew[j].eta > 1)
                                pNew[j].eta = 1;
                        }
                        else if (p[j].Option == PeakFunctionForm.Peason)//PearsonVII:       Int, Hk,  m,  X
                        {
                            pNew[j].Int = pCurrent[j].Int + delta[n++, 0];
                            pNew[j].Hk = pCurrent[j].Hk + delta[n++, 0];
                            pNew[j].m = pCurrent[j].m + delta[n++, 0];
                            pNew[j].X = pCurrent[j].X + delta[n++, 0];

                            if (pNew[j].m <= 0.5 || pNew[j].m >= 10)
                                flag = false;
                        }
                        else if (p[j].Option == PeakFunctionForm.SplitPseudoVoigt)//SplitPseudoVoigt:  Int, Hk,  A,  etaL , etaH, X
                        {
                            pNew[j].Int = pCurrent[j].Int + delta[n++, 0];
                            pNew[j].Hk = pCurrent[j].Hk + delta[n++, 0];
                            pNew[j].A = pCurrent[j].A + delta[n++, 0];
                            pNew[j].etaL = pCurrent[j].etaL + delta[n++, 0];
                            pNew[j].etaH = pCurrent[j].etaH + delta[n++, 0];
                            pNew[j].X = pCurrent[j].X + delta[n++, 0];
                            if (pNew[j].etaL < 0 || pNew[j].etaL > 1)
                                flag = false;
                            if (pNew[j].etaH < 0 || pNew[j].etaH > 1)
                                flag = false;
                            if (pNew[j].A < -2 || pNew[j].A > 2)
                                flag = false;
                        }
                        else if (p[j].Option == PeakFunctionForm.SplitPearson)//SplitPearsonVII:  Int, Hk,  A,  Rl , Rh, X
                        {
                            pNew[j].Int = pCurrent[j].Int + delta[n++, 0];
                            pNew[j].Hk = pCurrent[j].Hk + delta[n++, 0];
                            pNew[j].A = pCurrent[j].A + delta[n++, 0];
                            pNew[j].Rl = pCurrent[j].Rl + delta[n++, 0];
                            pNew[j].Rh = pCurrent[j].Rh + delta[n++, 0];
                            pNew[j].X = pCurrent[j].X + delta[n++, 0];
                            if (pNew[j].Rl <= 0.5 || pNew[j].Rl >= 10)
                                flag = false;
                            if (pNew[j].Rh <= 0.5 || pNew[j].Rh >= 10)
                                flag = false;
                            if (pNew[j].A < -2 || pNew[j].A > 2)
                                flag = false;
                        }

                        if (pNew[j].Hk < 0)
                            flag = false; //pNew[j].Hk = pCurrent[j].Hk;

                        //2本以上のフィッティングの場合、XがRangeの1/8以上動いたらもどす
                        if (p.Length > 1)
                            if (pNew[j].X - p[j].X > p[j].range / 8 || pNew[j].X - p[j].X < -p[j].range / 8)
                            { pNew[j].X = pCurrent[j].X; }
                        //    flag = false; //pNew[j].X = p[j].X + p[j].range / 4;
                    }
                    B1_New = B1 + delta[ParamNum, 0];
                    B2_New = B2 + delta[ParamNum + 1, 0];
                    //if (B1_New + B2_New * (x[0] - centerX) < 0 || B1_New + B2_New * (x[x.Length - 1] - centerX) < 0)
                    //    flag = false; //B1_New = B1;//B2_New = B2;

                    //あたらしいパラメータでの残差を計算
                    if (flag)
                    {
                        ResidualSquareNew = 0;
                        for (int i = 0; i < length; i++)
                            IntNew[i] = B1_New + B2_New * (x[i] - centerX);
                        for (int j = 0; j < p.Length; j++)
                        {
                            pNew[j].RenewParameter();
                            for (int i = 0; i < length; i++)
                                IntNew[i] += pNew[j].GetValue(x[i], false);
                        }
                        for (int i = 0; i < length; i++)
                        {
                            ResidualNew[i] = y[i] - IntNew[i];
                            ResidualSquareNew += ResidualNew[i] * ResidualNew[i];
                        }
                    }
                    //残差計算ここまで

                    //新旧の値を比較
                    if (flag && ResidualSquareCurrent >= ResidualSquareNew)
                    {
                        ramda *= 0.8;
                        for (int i = 0; i < length; i++)
                            ResidualCurrent[i] = ResidualNew[i];
                        for (int j = 0; j < p.Length; j++)
                            pCurrent[j] = pNew[j].Copy();
                        B1 = B1_New;
                        B2 = B2_New;
                        if (counter > 50 && (ResidualSquareCurrent - ResidualSquareNew) / ResidualSquareCurrent < 0.0000000001)
                            break;
                        ResidualSquareCurrent = ResidualSquareNew;
                    }
                    else
                        ramda *= 2;
                } while (ramda < 100000000000 && counter < 1000);

                for (int i = 0; i < length; i++)
                    IntNew[i] = B1_New + B2_New * (x[i] - centerX);
                for (int j = 0; j < p.Length; j++)
                {
                    pNew[j].RenewParameter();
                    for (int i = 0; i < length; i++)
                        IntNew[i] += pNew[j].GetValue(x[i], false);
                }
                residual = 0;
                for (int i = 0; i < length; i++)
                    residual += (IntNew[i] - y[i]) / IntNew[i] * (IntNew[i] - y[i]) / IntNew[i];
                residual /= length;
                if (bestResidual > residual)//最も結果のよかったカウントを格納
                {
                    bestResidual = residual;
                    bestInitial = Initial;
                }
                if (Math.Sqrt(residual) < 0.25)//平均2乗残差が5%以下だったら終了
                    break;
                if (Initial == endInitial - 1 && endInitial - startInitial > 2)//5%以下のものが見つからなかったとき
                {
                    //50%を超えるくらいひどかったら
                    if (Math.Sqrt(bestResidual) > 0.90)
                    {
                        for (int i = 0; i < p.Length; i++)
                        {
                            p[i].X = double.NaN;
                            p[i].Int = double.NaN;
                        }
                        return double.PositiveInfinity;
                    }
                    //一番ましな初期値でやり直す
                    startInitial = bestInitial - 1;
                    Initial = bestInitial - 1;
                    endInitial = bestInitial + 1;
                }
            }

            //行列アルファを決める
            //行列Alpha, Betaを作る
            for (int i = 0; i < ParamNum + 2; i++)
                for (int j = i; j < ParamNum + 2; j++)
                {
                    Alpha[i, j] = 0;
                    for (int k = 0; k < length; k++)
                        Alpha[i, j] += diff[i, k] * diff[j, k];
                    Alpha[j, i] = Alpha[i, j];
                }
            counter = 0;
            //最後にそれぞれもとまった数値を入れる。
            for (int i = 0; i < p.Length; i++)
            {
                //p[i] = pCurrent[i].Copy();
                p[i].Option = pCurrent[i].Option;
                p[i].intensity = pCurrent[i].intensity;
                p[i].eta = pCurrent[i].eta;
                p[i].etaH = pCurrent[i].etaH;
                p[i].etaL = pCurrent[i].etaL;
                p[i].Rl = pCurrent[i].Rl;
                p[i].Rh = pCurrent[i].Rh;
                p[i].X = pCurrent[i].X;
                p[i].Hk = pCurrent[i].Hk;
                p[i].Int = pCurrent[i].Int;
                p[i].A = pCurrent[i].A;
                p[i].B1 = pCurrent[i].B1;
                p[i].B2 = pCurrent[i].B2;
                p[i].B3 = pCurrent[i].B3;
                counter += p[i].GetParamNumber();
                p[i].Xerr = 1 / Math.Sqrt(Alpha[counter - 1, counter - 1]);
                p[i].m = pCurrent[i].m;
                p[i].range = pCurrent[i].range;
                p[i].Residual = bestResidual;
                p[i].Int *= 1000;
                p[i].B1 = B1 + B2 * (pCurrent[i].X - centerX);
                p[i].B2 = B2;
                p[i].B1 *= 1000;
                p[i].B2 *= 1000;
            }

            //これより下は主にIPAからだけ呼ばれる部分
            if (RemoveBadSN > 0)
            {
                //S/N比が悪いデータは除去する
                double BackGround = 0;
                double Signal = 0;
                p[0].RenewParameter();
                for (int i = 0; i < length; i++)
                {
                    Signal += p[0].GetValue(x[i], false);
                    BackGround += p[0].B1 + p[0].B2 * (p[0].X - x[i]);
                }
                if (double.IsNaN(Signal / (BackGround + Signal)) || double.IsInfinity(Signal / (BackGround + Signal)) || (Signal / (BackGround + Signal)) < RemoveBadSN)
                    p[0].X = double.NaN;

                //中心位置が外れて過ぎても失格
                if (p[0].X < x[0] || x[x.Length - 1] < p[0].X)
                {
                    p[0].X = double.NaN;
                }
            }
            /*if (counter < 1000)
                return true;
            else
                return false;*/
            return bestResidual;
        }

        public static PointD FitPeakAsPseudoVoigtByMarcal2D(double[] values, int width, PointD center, int range)
        {
            int height = values.Length / width;

            double[,] tempIntensity = new double[range * 2 + 1, range * 2 + 1];
            if (center.Y < range + 2 || center.Y > height - range - 2 || center.X < range + 2 || center.X > width - range - 2)
                return new PointD(center.X, center.Y);

            for (int i = 0; i < range * 2 + 1; i++)
                for (int j = 0; j < range * 2 + 1; j++)
                    tempIntensity[i, j] = values[((int)(center.Y + 0.5 - range) + j) * width + (int)((center.X + 0.5 - range) + i)];
            PointD offset = FittingPeak.FitPeakAsPseudoVoigtByMarcal2D(tempIntensity);
            if (double.IsNaN(offset.X))
                return center;

            return new PointD(offset.X + (int)(center.X + 0.5 - range), offset.Y + (int)(center.Y + 0.5 - range));
        }

        /// <summary>
        /// 二次元データに対してPseudoVoigt関数でフィッティングする
        /// </summary>
        /// <param name="intensity"></param>
        /// <returns></returns>
        public static PointD FitPeakAsPseudoVoigtByMarcal2D(double[,] intensity)
        {
            int xLength = intensity.GetLength(0);
            int yLength = intensity.GetLength(1);

            double A = 1, Eta = 0.5, Hk = (xLength + yLength) / 4.0, X = xLength / 2.0, Y = yLength / 2.0, B1 = 0, B2 = 0, B3 = 0, C = 1;
            double A_New, Eta_New, Hk_New, X_New, Y_New, /*B1_New, B2_New, B3_New,*/ C_New;

            //範囲内で一番高い点を探す
            // double MaxIntensity = double.NegativeInfinity;

            //intensityを規格化
            double sum = 0;
            foreach (double d in intensity)
                if (!double.IsNaN(d))
                    sum += d;

            for (int i = 0; i < xLength; i++)
                for (int j = 0; j < yLength; j++)
                    if (!double.IsNaN(intensity[i, j]))
                    {
                        intensity[i, j] /= sum;
                        //   if (MaxIntensity < intensity[i, j])
                        //   {
                        //       MaxIntensity = intensity[i, j];
                        //       X = i;
                        //       Y = j;
                        //   }
                    }
            //if (initialCenter != null)
            //{ X = initialCenter.X; Y = initialCenter.Y; }
            double[,,] diff = new double[9, xLength, yLength];
            var Alpha = new DenseMatrix(9, 9);
            var Beta = new DenseMatrix(9, 1);
            double[,] ResidualCurrent = new double[xLength, yLength];
            double[,] ResidualNew = new double[xLength, yLength];
            double ResidualSquareCurrent, ResidualSquareNew = 0;
            int count = 0;

            //関数形は
            //F= 2 A / / Hk / PI
            // ( eta  / (1 + 4 * ( (X-x)^2 + C (Y-y)^2 ) / Hk^2))
            //+  (1 - eta) * Sqrt(Ln2 * PI) * Exp(-4 * Ln2 * ( (X-x)^2 + C (Y-y)^2 ) / Hk^2) ) )
            //+ B1 + B2 * (X-x) + B3 * (Y-y)

            double Ln2 = Math.Log(2);
            double Ln2PI = Math.Sqrt(Ln2 * Math.PI);
            double tmp0, tmp1, tmp2, tmp3, tmp4, tmp5;

            //現在の残差を計算
            ResidualSquareCurrent = 0;
            for (int i = 0; i < xLength; i++)
                for (int j = 0; j < yLength; j++)
                    if (!double.IsNaN(intensity[i, j]))
                    {
                        tmp3 = ((X - i) * (X - i) + C * (Y - j) * (Y - j)) / Hk / Hk;
                        ResidualCurrent[i, j] = intensity[i, j] -
                          (
                          2 * A / Hk / PI * (Eta / (1 + 4 * tmp3) + (1 - Eta) * SqrtLn2PI * Math.Exp(-4 * Ln2 * tmp3))
                            + B1 + B2 * (X - i) + B3 * (Y - j)
                          );
                        ResidualSquareCurrent += ResidualCurrent[i, j] * ResidualCurrent[i, j];
                    }

            double lambda = 1;

            while (lambda < 1000000000 && count < 300)//lambdaが大きくなりすぎた時か、試行回数が一定以上になった時、止める
            {
                count++;
                for (int i = 0; i < xLength; i++)//偏微分を作る
                    for (int j = 0; j < yLength; j++)
                    {
                        tmp0 = 1 / Hk / Math.PI;
                        tmp1 = (X - i) * (X - i) + C * (Y - j) * (Y - j);
                        tmp2 = 1 / (Hk * Hk);
                        tmp3 = tmp1 * tmp2;
                        tmp4 = Math.Exp(-4 * Ln2 * tmp3);
                        tmp5 = 1 / (1 + 4 * tmp3);

                        //∂F/∂A
                        diff[0, i, j] = 2 * tmp0 * ((1 - Eta) * Ln2PI * tmp4 + Eta * tmp5);
                        //∂F/∂eta
                        diff[1, i, j] = 2 * A * tmp0 * (-Ln2PI * tmp4 + tmp5);
                        //∂F/∂Hk
                        diff[2, i, j] = 1 / Math.PI * 2 * A * tmp2 * ((-1 + Eta) * Ln2PI * tmp4 - Eta * tmp5 + 8 * tmp3 * ((1 - Eta) * Ln2 * Ln2PI * tmp4 + Eta * tmp5 * tmp5));
                        //∂F/∂X
                        diff[3, i, j] = B2 + 16 * A * tmp0 * tmp2 * ((-1 + Eta) * Ln2 * Ln2PI * tmp4 - Eta * tmp5 * tmp5) * (X - i);
                        //∂F/∂Y
                        diff[4, i, j] = B3 + 16 * A * C * tmp0 * tmp2 * ((-1 + Eta) * Ln2 * Ln2PI * tmp4 - Eta * tmp5 * tmp5) * (Y - j);
                        //∂F/∂C
                        diff[5, i, j] = 8 * A * tmp0 * tmp2 * ((-1 + Eta) * Ln2 * Ln2PI * tmp4 - Eta * tmp5 * tmp5) * (Y - j) * (Y - j);
                        //∂F/∂B1 = 1
                        diff[6, i, j] = 1;
                        //∂F/∂B2 = (X-x)
                        diff[7, i, j] = X - i;
                        //∂F/∂B3 = (Y-y)
                        diff[8, i, j] = Y - j;
                    }

                //行列Alpha, Betaを作る
                for (int k = 0; k < 9; k++)
                {
                    for (int l = 0; l < 9; l++)
                    {
                        Alpha[k, l] = 0;
                        for (int i = 0; i < xLength; i++)
                            for (int j = 0; j < yLength; j++)
                                Alpha[k, l] += diff[k, i, j] * diff[l, i, j];

                        if (k == l)
                            Alpha[k, l] *= (1 + lambda);
                    }

                    Beta[k, 0] = 0;
                    for (int i = 0; i < xLength; i++)
                        for (int j = 0; j < yLength; j++)
                            Beta[k, 0] += ResidualCurrent[i, j] * diff[k, i, j];
                }

                if (!Alpha.TryInverse(out Matrix<double> alphaInv))
                    return new PointD(double.NaN, double.NaN);

                var delta = alphaInv * Beta;

                A_New = A + delta[0, 0];
                if (A_New < 0) A_New = A;
                Eta_New = Eta + delta[1, 0];
                if (Eta_New > 1 || Eta_New < 0) Eta_New = Eta;
                Hk_New = Hk + delta[2, 0];
                if (Hk_New < 0 || Hk_New > (xLength + yLength) * 4) Hk_New = Hk;
                X_New = X + delta[3, 0];
                if (X_New < 0 || X_New > xLength) X_New = X;
                Y_New = Y + delta[4, 0];
                if (Y_New < 0 || Y_New > yLength) Y_New = Y;
                C_New = C + delta[5, 0];
                if (C_New < 0.1 || C_New > 10) C_New = C;
                //B1_New = B1 + delta.E[6, 0];
                //B2_New = B2 + delta.E[7, 0];
                //B3_New = B3 + delta.E[8, 0];

                //あたらしいパラメータでの残差の二乗和を計算
                ResidualSquareNew = 0;
                for (int i = 0; i < xLength; i++)
                    for (int j = 0; j < yLength; j++)
                        if (!double.IsNaN(intensity[i, j]))
                        {
                            tmp3 = ((X_New - i) * (X_New - i) + C_New * (Y_New - j) * (Y_New - j)) / Hk_New / Hk_New;
                            ResidualNew[i, j] = intensity[i, j] -
                              (
                              2 * A_New / Hk_New / PI * (Eta_New / (1 + 4 * tmp3) + (1 - Eta_New) * SqrtLn2PI * Math.Exp(-4 * Ln2 * tmp3))
                              // + B1_New + B2_New * (X_New - i) + B3_New * (Y_New - j)
                              );
                            ResidualSquareNew += ResidualNew[i, j] * ResidualNew[i, j];
                        }

                if (ResidualSquareCurrent > ResidualSquareNew)//新旧の残差の二乗和を比較
                {//改善したとき
                    if ((ResidualSquareCurrent - ResidualSquareNew) / ResidualSquareCurrent > 0.000001 || count < 15 || lambda > 1)
                    {//改善率が0.0000000001 以上 (まだまだ改善の余地がある)、あるいはcountが15以下 (回数が少ない)、あるいはlambdaが1より大きいとき (まだまだ改善の余地がある)
                        ResidualSquareCurrent = ResidualSquareNew;//残差の二乗和を書き換える
                        lambda *= 0.625;//lambdaを小さくする
                        for (int i = 0; i < xLength; i++)
                            for (int j = 0; j < yLength; j++)
                                ResidualCurrent[i, j] = ResidualNew[i, j];//残差行列を書き換える
                        A = A_New; Eta = Eta_New; Hk = Hk_New; X = X_New; Y = Y_New; // B1 = B1_New; B2 = B2_New; B3 = B3_New; C = C_New;//新旧パラメータを書き換える
                    }
                    else
                        break;
                }

                else//改善しなかったとき
                    lambda *= 1.6;//lambdaを大きくする
            }

            return new PointD(X, Y);
        }
    }
}