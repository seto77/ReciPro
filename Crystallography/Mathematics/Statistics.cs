using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crystallography;

public class Statistics
{
    ///// <summary>
    ///// 単純平均を求めます
    ///// </summary>
    ///// <param name="values"></param>
    ///// <returns></returns>
    //public static double Average(params int[] values)
    //{
    //    try
    //    {
    //        int sum = 0;
    //        foreach (int value in values)
    //            sum += value;

    //        return (double)sum / values.Length;
    //    }
    //    catch { return 0; }
    //}

    ///// <summary>
    ///// 重み付き平均を求めます
    ///// </summary>
    ///// <param name="values"></param>
    ///// <returns></returns>
    //public static double Average(double[] values, double[] weight)
    //{
    //    try
    //    {
    //        double[] normarizedWeight = Normarize(weight);
    //        double weightAverage = 0;
    //        for (int i = 0; i < values.Length; i++)
    //            weightAverage += values[i] * weight[i];
    //        return weightAverage;
    //    }
    //    catch { return 0; }
    //}

    /// <summary>
    /// 標本の総和を1に規格化します
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static double[] Normarize(params double[] values)
    {
        try
        {
            double[] newValues = new double[values.Length];
            double sum = 0;
            Array.Copy(values, newValues, values.Length);
            Array.Sort(newValues);
            Array.Reverse(newValues);
            foreach (double value in newValues)
                sum += value;
            for (int i = 0; i < values.Length; i++)
                newValues[i] = values[i] / sum;
            return newValues;
        }
        catch
        {
            return new double[values.Length];
        }
    }

    /// <summary>
    /// 標本標準分散を求めます
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static double Variance(params double[] values)
    {
        try
        {
            if (values.Length == 0) return 0;
            double Sum = 0;
            double SumSquare = 0;
            foreach (double value in values)
            {
                Sum += value;
                SumSquare += value * value;
            }
            return (values.Length * SumSquare - Sum * Sum) / values.Length / (values.Length - 1);
        }
        catch { return 0; }
    }

    /// <summary>
    /// 共分散を求めます
    /// </summary>
    /// <param name="values1"></param>
    /// <param name="values2"></param>
    /// <returns></returns>
    public static double Covariance(double[] values1, double[] values2)
    {
        try
        {
            if (values1.Length != values2.Length || values2.Length < 2) return 0;
            double AverageOfValue1 = values1.Average();
            double AverageOfValue2 = values2.Average();
            double Covariance = 0;
            for (int i = 0; i < values1.Length; i++)
                Covariance += (values1[i] - AverageOfValue1) * (values2[i] - AverageOfValue2);
            return Covariance / (values1.Length - 1);
        }
        catch { return 0; }
    }

    /// <summary>
    /// 標本標準偏差を求めます
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static double Deviation(params double[] values)
    {
        return Math.Sqrt(Variance(values));
    }

    /// <summary>
    /// 二変量の相関係数を求めます
    /// </summary>
    /// <param name="values1"></param>
    /// <param name="values2"></param>
    /// <returns></returns>
    public static double Correlation(double[] values1, double[] values2)
    {
        if (values1.Length != values2.Length) return 0;

        var AverageOfValue1 = values1.Average();
        var AverageOfValue2 = values2.Average();

        double Covariance = 0;
        double Variance1 = 0;
        double Variance2 = 0;
        for (int i = 0; i < values1.Length; i++)
        {
            Covariance += (values1[i] - AverageOfValue1) * (values2[i] - AverageOfValue2);
            Variance1 += (values1[i] - AverageOfValue1) * (values1[i] - AverageOfValue1);
            Variance2 += (values2[i] - AverageOfValue2) * (values2[i] - AverageOfValue2);
        }
        return Covariance / Math.Sqrt(Variance1 * Variance2);
    }

    /// <summary>
    /// 二変量のx[],y[]の一次関数近似をおこない傾きを返します
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static double Slope(double[] x, double[] y)
    {
        if (x.Length != y.Length || x.Length != 0) return 0;
        return (x.Length * SigmaProduct(x, y) - x.Sum() * y.Sum()) / (x.Length * SigmaSquare(x) - x.Sum() * y.Sum());
    }

    /// <summary>
    /// 二変量のx[],y[]の共分散行列の主成分分析から回帰直線を求めます。返す形は x sin(theta) - y cos(theta) = A ただし0 ＜ theta ＜ PI
    /// </summary>
    /// <param name="values1"></param>
    /// <param name="values2"></param>
    /// <returns></returns>
    public static void LineFitting(double[] values1, double[] values2, ref double theta, ref double A)
    {
        if (values1 == null || values1.Length == 0 || values2 == null || values2.Length == 0 || values1.Length != values2.Length)
            return;
        double a = Variance(values1);
        double b = Covariance(values1, values2);
        double c = Variance(values2);
        theta = Math.Atan2(2 * b, a - c + Math.Sqrt(4 * b * b + (a - c) * (a - c)));

        A = Math.Sin(theta) * values1.Average() - Math.Cos(theta) * values2.Average();
        if (theta < 0)
        {
            theta += Math.PI;
            A = -A;
        }
    }

    /// <summary>
    /// 二変量のpt[].x, pt.y[]の共分散行列の主成分分析から回帰直線を求めます。返す形は x sin(theta) - y cos(theta) = A ただし0 ＜ theta ＜ PI
    /// </summary>
    /// <param name="values1"></param>
    /// <param name="values2"></param>
    /// <returns></returns>
    public static void LineFitting(PointD[] pt, ref double theta, ref double A)
    {
        double[] values1 = new double[pt.Length];
        double[] values2 = new double[pt.Length];
        for (int i = 0; i < pt.Length; i++)
        {
            values1[i] = pt[i].X;
            values2[i] = pt[i].Y;
        }
        LineFitting(values1, values2, ref theta, ref A);
    }

    /// <summary>
    /// Pt[]を受け取ってその重心座標をかえします。
    /// </summary>
    /// <param name="pt"></param>
    /// <returns></returns>
    public static PointD FindCenterPt(PointD[] pt)
    {
        double[] values1 = new double[pt.Length];
        double[] values2 = new double[pt.Length];
        for (int i = 0; i < pt.Length; i++)
        {
            values1[i] = pt[i].X;
            values2[i] = pt[i].Y;
        }
        return new PointD(values1.Average(), values2.Average());
    }

    /// <summary>
    /// 変量valuesの総和を求めます
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static double Sigma(params double[] values)
    {
        double sum = 0;
        foreach (double X in values)
            sum += X;
        return sum;
    }



    /// <summary>
    /// 変量valuesの二乗和を求めます
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static double SigmaSquare(params double[] values)
    {
        double sum = 0;
        foreach (double X in values)
            sum += X * X;
        return sum;
    }

    /// <summary>
    /// 変量valuesの二乗和を求めます
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static int SumSquare(params int[] values)
    {
        int sum = 0;
        foreach (int X in values)
            sum += X * X;
        return sum;
    }

    /// <summary>
    /// 変量values1とvalues2の積の和を求めます
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static double SigmaProduct(double[] values1, double[] values2)
    {
        if (values1.Length != values2.Length) return 0;
        double sum = 0;
        for (int i = 0; i < values1.Length; i++)
            sum += values1[i] * values2[i];
        return sum;
    }


    public static object lockObject = new();

    /// <summary>
    /// 平均mu, 標準偏差sigmaの正規分布乱数を得る。Box-Muller法による。
    /// </summary>
    /// <param name="mu">平均値</param>
    /// <param name="sigma">標準偏差</param>
    /// <returns>指定した正規分布に即した乱数</returns>
    public static double NormalDistribution(double mu, double sigma)
    {
        if (Flag)
        {
            lock (lockObject)
            {
                Alpha = Rn.NextDouble();
                Beta = Rn.NextDouble() * Math.PI * 2;
            }
            BoxMuller1 = Math.Sqrt(-2 * Math.Log(Alpha));
            BoxMuller2 = Math.Sin(Beta);
        }
        else
            BoxMuller2 = Math.Cos(Beta);
        Flag = !Flag;

        return sigma * (BoxMuller1 * BoxMuller2) + mu;
    }

    private static readonly Random Rn = new(Environment.TickCount);
    private static double Alpha, Beta, BoxMuller1, BoxMuller2;
    private static bool Flag = false;

    /// <summary>
    /// 平均mu, 標準偏差sigmaの対数正規分布乱数を得る。平均と標準偏差を変換し、Box-Muller法によるNormlDistribution関数を呼び出す。
    /// </summary>
    /// <param name="mu">平均値</param>
    /// <param name="sigma">標準偏差</param>
    /// <returns>指定した対数正規分布に即した乱数</returns>
    public static double LogNormalDistribution(double mu, double sigma)
    {
        if (mu <= 0)
            return double.NaN;
        else
            return Math.Exp(NormalDistribution(Math.Log(mu * mu) - Math.Log(mu * mu + sigma * sigma) / 2.0, Math.Sqrt(Math.Log(1 + sigma / mu * sigma / mu))));
    }

    /// <summary>
    /// 数値配列を受け取って頻度分布を返す
    /// </summary>
    /// <param name="values"></param>
    /// <param name="division">指定した分割数になるように、データを丸める (0の時は丸めなし)</param>
    /// <returns></returns>
    public static SortedList<double, int> GetFrequency(double[] values, int divisions = 1000)
    {
        if (values == null || values.Length == 0) return null;

        int partition = 100;//分割数
        int range = values.Length / partition;//一つ当たりの個数
        if (range <= 0) range = 1;

        var max = values.Max();
        var min = values.Min();

        var frequency = new SortedList<double, int>[partition];
        Parallel.For(0, partition, n =>
            {
                frequency[n] = [];
                for (int i = n * range; i < Math.Min((n + 1) * range, values.Length); i++)
                {
                    var v = values[i];
                    if (divisions > 0)
                        v = (int)((v - min) / (max - min) * divisions + 0.5) * (max - min) / divisions + min;

                    if (frequency[n].ContainsKey(v))
                        frequency[n][v]++;
                    else
                        frequency[n].Add(v, 1);
                }
            });
        var frequencyFinal = new SortedList<double, int>();
        foreach (var freq in frequency)
            foreach (var key in freq.Keys)
            {
                if (frequencyFinal.ContainsKey(key))
                    frequencyFinal[key] += freq[key];
                else
                    frequencyFinal.Add(key, freq[key]);
            }
        return frequencyFinal;
    }

    public static class Combination
    {
        /// <summary>
        /// n 個から m個の組み合わせを求める(重複あり/なし)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">items は、配列、Listなどが受け取れる</param>
        /// <param name="m">m は、選び取る数</param>
        /// <param name="withRedandant">デフォルト: withRedandant = false 重複を許さない</param>
        /// <returns></returns>
        public static IEnumerable<T[]> GetElements<T>(IEnumerable<T> items, int m, bool withRedandant = false)
        {
            if (m == 1)
            {
                foreach (var n in items)
                    yield return new T[] { n };
            }
            foreach (var atom in items)
            {
                // atom よりも前のものを除く （順列と組み合わせの違い)
                var templist = items.SkipWhile(e => !e.Equals(atom)).ToList();
                if (!withRedandant)
                    templist.Remove(atom);// 重複を許さない場合は、atom そのものも取り除く

                var elements = GetElements(templist, m - 1, withRedandant);
                foreach (var elem in elements)
                {
                    var newelem = (new T[] { atom }).Concat(elem).ToArray();
                    yield return newelem;
                }
            }
        }

    }
}