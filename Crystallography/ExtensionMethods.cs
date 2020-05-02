using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Numerics;

namespace Crystallography
{
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
    }

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
        /// 拡張メソッド.  ConvertToDoubleを拡張メソッドとして呼び出す. 変換できない場合は例外発生
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static double ToDouble(this string s) => Convert.ToDouble(s);

        /// <summary>
        /// 拡張メソッド.  ConvertToInt32を拡張メソッドとして呼び出す. 変換できない場合は例外発生
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ToInt(this string s) => Convert.ToInt32(s);


    }
}