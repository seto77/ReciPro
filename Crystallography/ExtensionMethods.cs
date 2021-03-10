using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Providers.Common.Mkl;
using System;
using System.Numerics;

using MC = Crystallography.MathematicalConstants;

namespace Crystallography
{

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
        public static Matrix<Complex> Exponential(this Matrix<Complex> m)
        {
            if (m.RowCount != m.ColumnCount)
                throw new ArgumentException("Matrix should be square");

            int p = 7; // order of Padé 

            double k = 0;
            double mNorm = m.L1Norm();
            if (mNorm > 0.5)
            {
                k = Math.Ceiling(Math.Log(mNorm) / Math.Log(2.0));
                m = m.Divide(Math.Pow(2.0, k));
            }

            Matrix<Complex> N = MathNet.Numerics.LinearAlgebra.Complex.DenseMatrix.CreateIdentity(m.RowCount), D = N;
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
                exp_m = exp_m.Power((int)Math.Pow(2.0, k));

            return exp_m;
        }

    }
    #endregion

    #region Complexの拡張
    public static class ComplexEx
    {
        /// <summary>
        /// 拡張メソッド.  Real^2 + Imaginary^2を返す
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static double Magnitude2(this Complex c) => c.Real * c.Real + c.Imaginary * c.Imaginary;

        /// <summary>
        /// 拡張メソッド. 自己共役を返す
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Complex Conjugate(this Complex c) => Complex.Conjugate(c);
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
            => s.Split(new[] { separator }, removeEmptyEntries ? StringSplitOptions.RemoveEmptyEntries: StringSplitOptions.None);

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
        public static double ToDouble(this string s) => !s.Contains("/") ? Convert.ToDouble(s) : s.Split("/")[0].ToDouble() / s.Split("/")[1].ToDouble();

        /// <summary>
        /// 拡張メソッド.  ConvertToInt32を拡張メソッドとして呼び出す. 変換できない場合は例外発生
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ToInt(this string s) => Convert.ToInt32(s);


    }
    #endregion
}