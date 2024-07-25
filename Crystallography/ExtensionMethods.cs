﻿using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using Windows.Media.Audio;
using DMat = MathNet.Numerics.LinearAlgebra.Complex.DenseMatrix;
using MC = Crystallography.MathematicalConstants;

namespace Crystallography;

#region MathNet の拡張
public static class MathnetEx
{
    /// <summary>
    /// 拡張メソッド. 逆行列に変換する。逆行列の計算に失敗した場合は、nullを返す。
    /// </summary>
    /// <param name="mat"></param>
    /// <returns></returns>
    public static Matrix<double> TryInverse(this Matrix<double> mat)
    {
        if (mat.Determinant() == 0)
            return null;
        try
        {
            return mat.Inverse();
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 拡張メソッド. 逆行列に変換する。逆行列の計算に失敗した場合は、nullを返す。
    /// </summary>
    /// <param name="mat"></param>
    /// <returns></returns>
    public static Matrix TryInverse(this Matrix mat)
    {
        if (mat.Determinant() == 0)
            return null;
        try
        {
            return (Matrix)mat.Inverse();
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 拡張メソッド. 逆行列に変換する。成功した場合はtrueを返す。
    /// </summary>
    /// <param name="mat"></param>
    /// <param name="matInv"></param>
    /// <returns></returns>
    public static bool TryInverse(this Matrix<double> mat, out Matrix<double> matInv)
    {
        matInv = mat.TryInverse();
        return matInv != null;
    }

    /// <summary>
    /// 拡張メソッド. 逆行列に変換する。成功した場合はtrueを返す。
    /// </summary>
    /// <param name="mat"></param>
    /// <param name="matInv"></param>
    /// <returns></returns>
    public static bool TryInverse(this Matrix<double> mat, out Matrix matInv)
    {
        matInv = (Matrix)mat.TryInverse();
        return mat != null;
    }

    /// <summary>
    /// 拡張メソッド. 逆行列に変換する。成功した場合はtrueを返す。
    /// </summary>
    /// <param name="mat"></param>
    /// <param name="matInv"></param>
    /// <returns></returns>
    public static bool TryInverse(this Matrix mat, out Matrix matInv)
    {
        matInv = mat.TryInverse();
        return matInv != null;
    }


    public static Matrix<Complex> Exponential(this Matrix<Complex> m)
    {
        #region オリジナルコード 
        /*public static Matrix<double> Exponential(this Matrix<double> m)
        {
            if (m.RowCount != m.ColumnCount)
                throw new ArgumentException("Matrix should be square");

            Matrix<double> exp_m = null;

            // if m is diagonal, then matrix exponential is equal to pointwise exponential
            if (m.IsDiagonal())
                exp_m = DenseMatrix.OfDiagonalVector(m.Diagonal().PointwiseExp());
            else
            {
                // unfortunately m is not diagonal
                // so let's try to diagonalize it
                bool diagonalization_failed = !m.IsSymmetric();
                if (!diagonalization_failed)
                {
                    try
                    {
                        var evd = m.Evd();
                        Matrix expD = DenseMatrix.OfDiagonalVector(evd.D.Diagonal().PointwiseExp());
                        exp_m = evd.EigenVectors * expD * (evd.EigenVectors.Inverse());
                    }
                    catch
                    {
                        diagonalization_failed = true;
                    }
                }

                if (diagonalization_failed)
                {
                    // last hope: Padé approximation method
                    // details could be found in 
                    // M.Arioli, B.Codenotti, C.Fassino The Padé method for computing the matrix exponential // Linear Algebra and its Applications, 1996, V. 240, P. 111-130
                    // https://www.sciencedirect.com/science/article/pii/0024379594001901

                    int p = 5; // order of Padé 

                    // high matrix norm may result in high roundoff erroros, 
                    // so first we have to find normalizing coefficient such that || m / norm_coeff || < 0.5
                    // to reduce the following computations we set it norm_coeff = 2^k

                    double k = 0;
                    double mNorm = m.L1Norm();
                    if (mNorm > 0.5)
                    {
                        k = Math.Ceiling(Math.Log(mNorm) / Math.Log(2.0));
                        m = m / Math.Pow(2.0, k);
                    }

                    Matrix<double> N = DenseMatrix.CreateIdentity(m.RowCount);
                    Matrix<double> D = DenseMatrix.CreateIdentity(m.RowCount);
                    Matrix<double> m_pow_j = m;

                    int q = p; // here we use simmetric approximation, but in general p may not be equal to q.
                    for (int j = 1; j <= Math.Max(p, q); j++)
                    {
                        if (j > 1)
                            m_pow_j = m_pow_j * m;
                        if (j <= p)
                            N = N + SpecialFunctions.Factorial(p + q - j) * SpecialFunctions.Factorial(p) / SpecialFunctions.Factorial(p + q) / SpecialFunctions.Factorial(j) / SpecialFunctions.Factorial(p - j) * m_pow_j;
                        if (j <= q)
                            D = D + SpecialFunctions.Factorial(p + q - j) * SpecialFunctions.Factorial(q) / SpecialFunctions.Factorial(p + q) / SpecialFunctions.Factorial(j) / SpecialFunctions.Factorial(q - j) * Math.Pow(-1.0, j) * m_pow_j;
                    }

                    // calculate inv(D)*N with LU decomposition
                    exp_m = D.LU().Solve(N);

                    // denormalize if need
                    if (k > 0)
                    {
                        for (int i = 0; i < k; i++)
                        {
                            exp_m = exp_m * exp_m;
                        }
                    }
                }
            }
            return exp_m;
        }*/
        #endregion

        if (m.RowCount != m.ColumnCount)
            throw new ArgumentException("Matrix should be square");

        double k = 0;
        double mNorm = m.L1Norm();
        if (mNorm > 0.5)
        {
            k = Math.Ceiling(Math.Log(mNorm) / Math.Log(2.0));
            m = m.Divide(Math.Pow(2.0, k));
        }

        int p = m.L1Norm() switch  // order of Padé 
        {
            < 1.495585217958292e-002 => 3,
            < 2.539398330063230e-001 => 5,
            < 9.504178996162932e-001 => 7,
            < 2.097847961257068e+000 => 9,
            _ => 13
        };

        Matrix<Complex> N = DMat.CreateIdentity(m.RowCount), D = N;
        Matrix<Complex> m_pow_j = m;

        for (int j = 1; j <= p; j++)
        {
            if (j > 1)
                m_pow_j = m_pow_j.Multiply(m);

            if (j <= p)
            {
                var coeff = MC.Factorial[2 * p - j] * MC.Factorial[p] / MC.Factorial[2 * p] / MC.Factorial[j] / MC.Factorial[p - j];
                var temp = m_pow_j.Multiply(coeff);
                N = N.Add(temp);
                D = j % 2 == 0 ? D.Add(temp) : D.Subtract(temp);
            }
        }

        // calculate inv(D) * N with LU decomposition
        var exp_m = D.LU().Solve(N);

        // denormalize if need
        if (k > 0)
            for (int i = 0; i < k; i++)
                exp_m = exp_m.Multiply(exp_m);

        return exp_m;
    }

}
#endregion

#region Complexの拡張
//public static class ComplexEx
//{
//    /// <summary>
//    /// 拡張メソッド.  Real^2 + Imaginary^2を返す
//    /// </summary>
//    /// <param name="c"></param>
//    /// <returns></returns>
//    public static double Magnitude2(ref this Complex c) => c.Real * c.Real + c.Imaginary * c.Imaginary;

//    /// <summary>
//    /// 拡張メソッド. 自己共役を返す
//    /// </summary>
//    /// <param name="c"></param>
//    /// <returns></returns>
//    public static Complex Conjugate(ref this Complex c) => Complex.Conjugate(c);
//}
#endregion

#region (int h, int k, int l)の拡張
public static class HKL
{
    public static (int H, int K, int L) Plus(ref this (int H, int K, int L) x, (int H, int K, int L) y) => (x.H + y.H, x.K + y.K, x.L + y.L);
    public static (int H, int K, int L) Minus(ref this (int H, int K, int L) x, (int H, int K, int L) y) => (x.H - y.H, x.K - y.K, x.L - y.L);
    public static int Multiply(ref this (int H, int K, int L) x, (int H, int K, int L) y) => x.H * y.H + x.K * y.K + x.L * y.L;
}
#endregion

#region Stringの拡張
public static class StringEx
{
    /// <summary>
    /// 拡張メソッド.  指定したseparatorで文字を区切り、文字の配列を返す.
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public static string[] Split(this string s, string separator, bool removeEmptyEntries = true)
        => s.Split(new[] { separator }, removeEmptyEntries ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None);

    /// <summary>
    /// 拡張メソッド.  指定したseparatorで文字を区切り、文字の配列を返す.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="separator"></param>
    /// <param name="removeEmptyEntries"></param>
    /// <returns></returns>
    public static string[] Split(this string s, char separator, bool removeEmptyEntries = true)
         => s.Split(new[] { separator }, removeEmptyEntries ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None);


    /// <summary>
    /// 拡張メソッド.  スペースかカンマで文字を区切り、文字の配列を返す.
    /// </summary>
    /// <returns></returns>
    public static string[] Split(this string s, bool removeEmptyEntries = true)
        => s.Split(new[] { " ", "," }, removeEmptyEntries ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None);


    /// <summary>
    /// 拡張メソッド. ConvertToDoubleを拡張メソッドとして呼び出す. 実数と、分数に対応. 変換できない場合は例外発生
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static double ToDouble(this string s) => !s.Contains("/") ? Convert.ToDouble(s) : s.Split("/", true)[0].ToDouble() / s.Split("/", true)[1].ToDouble();

    /// <summary>
    /// 拡張メソッド.  ConvertToInt32を拡張メソッドとして呼び出す. 変換できない場合は例外発生
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static int ToInt(this string s) => Convert.ToInt32(s);


}
#endregion

#region doubleの拡張
public static class DoubleEx
{
    const double rad = 0.01745329251994329576923690768489;
    const double deg = 57.295779513082320876798154814105;

    /// <summary>
    /// 拡張メソッド. 数値をRadianに変換.
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public static double ToRadians(in this double d) => d * rad;

    /// <summary>
    /// 拡張メソッド. 数値をRadianに変換.
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public static double ToDegrees(in this double d) => d * deg;
}
#endregion

#region Graphicsクラス
/// <summary>
/// Graphics クラスの描画関数にdoubleを受けられるにようにした拡張メソッド
/// </summary>
public static class GraphicsAlpha
{
    public static void DrawArc(this Graphics g, Pen pen, double x, double y, double width, double height, double startAngle, double sweepAngle)
        => g.DrawArc(pen, (float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);

    public static void DrawLines(this Graphics g, Pen pen, PointD[] points)
        => g.DrawLines(pen, points.Select(p => p.ToPointF()).ToArray());

    public static void DrawLine(this Graphics g, Pen pen, double x1, double y1, double x2, double y2)
        => g.DrawLine(pen, (float)x1, (float)y1, (float)x2, (float)y2);

    #region ペケ (×) 印を描画
    public static void DrawCross(this Graphics g, Pen pen, double x, double y, double size)
    {
        g.DrawLine(pen, x - size / 2, y - size / 2, x + size / 2, y + size / 2);
        g.DrawLine(pen, x + size / 2, y - size / 2, x - size / 2, y + size / 2);
    }
    public static void DrawCross(this Graphics g, Pen pen, PointD p, double size) => g.DrawCross(pen, p.X, p.Y, size);
    #endregion

    public static void FillPolygon(this Graphics g, Brush brush, PointD[] points, System.Drawing.Drawing2D.FillMode fillMode)
        => g.FillPolygon(brush, points.Select(p => p.ToPointF()).ToArray(), fillMode);

    public static void FillRectangle(this Graphics g, Brush brush, double x, double y, double width, double height)
        => g.FillRectangle(brush, (float)x, (float)y, (float)width, (float)height);

    public static void FillPie(this Graphics g, Brush brush, double x, double y, double width, double height, double startAngle, double sweepAngle)
        => g.FillPie(brush, (float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);


    static Dictionary<(int Alpha, Color Color), SolidBrush> solidBrushDic = [];

    #region 円の輪郭、あるいは円の塗りつぶし
    public static void FillCircle(this Graphics graphics, in Color c, in PointD pt, in double radius, in int alpha)
    {
        if (Math.Abs(pt.X) < 1E6 && Math.Abs(pt.Y) < 1E6)
        {
            if (!solidBrushDic.TryGetValue((alpha, c), out var brush))
                solidBrushDic.Add((alpha, c), brush = new SolidBrush(Color.FromArgb(alpha, c)));
            graphics.FillEllipse(brush, (float)(pt.X - radius), (float)(pt.Y - radius), (float)(2 * radius), (float)(2 * radius));
        }
    }
    public static void DrawCircle(this Graphics graphics, in Color c, in PointD pt, in double radius)
    {
        if (Math.Abs(pt.X) < 1E6 && Math.Abs(pt.Y) < 1E6)
            graphics.DrawEllipse(new Pen(c, 0.0001f), (float)(pt.X - radius), (float)(pt.Y - radius), (float)(2 * radius), (float)(2 * radius));
    }

    /// <summary>
    /// 拡張メソッド
    /// </summary>
    /// <param name="graphics"></param>
    /// <param name="s"></param>
    /// <param name="font"></param>
    /// <param name="color"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="resetTransform"></param>
    public static void DrawString(this Graphics graphics, string s, Font font, Color color, double x, double y, bool resetTransform=false)
    {
        var transform = graphics.Transform;
        if (resetTransform)
            graphics.Transform = new System.Drawing.Drawing2D.Matrix(1, 0, 0, 1, 1, 1);

        graphics.DrawString(s, font, new SolidBrush(color), (float)x, (float)y);

        if (resetTransform)
            graphics.Transform = transform;
    }

    public static void DrawString(this Graphics graphics, string s, Font font, Color color, PointD pt, bool resetTransform = false)
        =>DrawString(graphics,s,font,color, pt.X, pt.Y, resetTransform);

    #endregion


}
#endregion

#region LINQのDistinct関数の引数にラムダ式を使えるようにする拡張メソッド https://baba-s.hatenablog.com/entry/2016/10/17/100000
public static class IEnumerableExtensions
{
    private sealed class CommonSelector<T, TKey> : IEqualityComparer<T>
    {
        private readonly Func<T, TKey> m_selector;
        public CommonSelector(Func<T, TKey> selector) => m_selector = selector;
        public bool Equals(T x, T y) => m_selector(x).Equals(m_selector(y));
        public int GetHashCode(T obj) => m_selector(obj).GetHashCode();
    }

    public static IEnumerable<T> Distinct<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
        => source.Distinct(new CommonSelector<T, TKey>(selector));
}
#endregion
