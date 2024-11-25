using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Crystallography;

public class Statistics
{
    ///// <summary>
    ///// �P�����ς����߂܂�
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
    ///// �d�ݕt�����ς����߂܂�
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
    /// �W�{�̑��a��1�ɋK�i�����܂�
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
    /// �W�{�W�����U�����߂܂�
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
    /// �����U�����߂܂�
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
    /// �W�{�W���΍������߂܂�
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static double Deviation(params double[] values)
    {
        return Math.Sqrt(Variance(values));
    }

    /// <summary>
    /// ��ϗʂ̑��֌W�������߂܂�
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
    /// ��ϗʂ�x[],y[]�̈ꎟ�֐��ߎ��������Ȃ��X����Ԃ��܂�
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
    /// ��ϗʂ�x[],y[]�̋����U�s��̎听�����͂����A���������߂܂��B�Ԃ��`�� x sin(theta) - y cos(theta) = A ������0 �� theta �� PI
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
    /// ��ϗʂ�pt[].x, pt.y[]�̋����U�s��̎听�����͂����A���������߂܂��B�Ԃ��`�� x sin(theta) - y cos(theta) = A ������0 �� theta �� PI
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
    /// Pt[]���󂯎���Ă��̏d�S���W���������܂��B
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
    /// �ϗ�values�̑��a�����߂܂�
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
    /// �ϗ�values�̓��a�����߂܂�
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
    /// �ϗ�values�̓��a�����߂܂�
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
    /// �ϗ�values1��values2�̐ς̘a�����߂܂�
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


    public static Lock lockObject = new();

    /// <summary>
    /// ����mu, �W���΍�sigma�̐��K���z�����𓾂�BBox-Muller�@�ɂ��B
    /// </summary>
    /// <param name="mu">���ϒl</param>
    /// <param name="sigma">�W���΍�</param>
    /// <returns>�w�肵�����K���z�ɑ���������</returns>
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
    /// ����mu, �W���΍�sigma�̑ΐ����K���z�����𓾂�B���ςƕW���΍���ϊ����ABox-Muller�@�ɂ��NormlDistribution�֐����Ăяo���B
    /// </summary>
    /// <param name="mu">���ϒl</param>
    /// <param name="sigma">�W���΍�</param>
    /// <returns>�w�肵���ΐ����K���z�ɑ���������</returns>
    public static double LogNormalDistribution(double mu, double sigma)
    {
        if (mu <= 0)
            return double.NaN;
        else
            return Math.Exp(NormalDistribution(Math.Log(mu * mu) - Math.Log(mu * mu + sigma * sigma) / 2.0, Math.Sqrt(Math.Log(1 + sigma / mu * sigma / mu))));
    }

    /// <summary>
    /// ���l�z����󂯎���ĕp�x���z��Ԃ�
    /// </summary>
    /// <param name="values"></param>
    /// <param name="division">�w�肵���������ɂȂ�悤�ɁA�f�[�^���ۂ߂� (0�̎��͊ۂ߂Ȃ�)</param>
    /// <returns></returns>
    public static SortedList<double, int> GetFrequency(double[] values, int divisions = 1000)
    {
        if (values == null || values.Length == 0) return null;

        int partition = 100;//������
        int range = values.Length / partition;//�������̌�
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
        /// n ���� m�̑g�ݍ��킹�����߂�(�d������/�Ȃ�)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">items �́A�z��AList�Ȃǂ��󂯎���</param>
        /// <param name="m">m �́A�I�ю�鐔</param>
        /// <param name="withRedandant">�f�t�H���g: withRedandant = false �d���������Ȃ�</param>
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
                // atom �����O�̂��̂����� �i����Ƒg�ݍ��킹�̈Ⴂ)
                var templist = items.SkipWhile(e => !e.Equals(atom)).ToList();
                if (!withRedandant)
                    templist.Remove(atom);// �d���������Ȃ��ꍇ�́Aatom ���̂��̂���菜��

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