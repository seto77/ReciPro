using MathNet.Numerics.LinearAlgebra.Complex;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Buffers;

namespace Crystallography;

public static class NativeWrapper
{
    #region DllImport
    public enum Library { None, Eigen, Cuda }


    [DllImport("msvcrt.dll", EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
    private static extern IntPtr Memcpy(IntPtr dest, IntPtr src, UIntPtr count);


    [DllImport("Crystallography.Native.dll")]
    unsafe private static extern void _STEM_TDS2(int dim,
                                     double* U,
                                     double* C_k,
                                     double* C_kq,
                                     double* result);


    [DllImport("Crystallography.Native.dll")]
    unsafe private static extern void _STEM_TDS1(int dim,
                                        double* B,
                                        double* U,
                                        double* C_k,
                                        double* C_kq,
                                        double* result);


    [DllImport("Crystallography.Native.dll")]
    unsafe private static extern void _PointwiseMultiply(int dim,
                                     double* mat1,
                                     double* mat2,
                                     double* result);

    [DllImport("Crystallography.Native.dll")]
    unsafe private static extern void _AdjointAndMultiply(int dim,
                                      double* mat1,
                                      double* mat2,
                                      double* result);


    [DllImport("Crystallography.Native.dll")]
    unsafe private static extern void _Multiply(int dim,
                                       double* mat1,
                                       double* mat2,
                                       double* result);

    [DllImport("Crystallography.Native.dll")]
    private static extern void _Inverse(int dim,
                                        double[] mat,
                                        double[] matInv);

    [DllImport("Crystallography.Native.dll")]
    private static extern void _EigenSolver(int dim,
                                            double[] mat,
                                            double[] eigenValues,
                                            double[] eigenVectors);

    [DllImport("Crystallography.Native.dll")]
    private static extern void _MatrixExponential(int dim,
                                           double[] mat,
                                           double[] results);


    [DllImport("Crystallography.Cuda.dll")]
    private static extern void MatrixExponential_Cuda(int dim,
                                          double[] mat,
                                          double[] results);
    [DllImport("Crystallography.Cuda.dll")]
    private static extern void _CBEDSolver_MtxExp_Cuda(int gDim,
                                         double[] potential,
                                         double[] phi0,
                                         int tDim,
                                         double tStart,
                                         double tStep,
                                         double coeff,
                                         double[] result);


    [DllImport("Crystallography.Native.dll")]
    private static extern void _CBEDSolver_Eigen(int gDim,
                                           double[] potential,
                                           double[] phi0,
                                           int tDim,
                                           double[] thickness,
                                           double coeff,
                                           double[] result);

    [DllImport("Crystallography.Native.dll")]
    private static extern void _CBEDSolver_MtxExp(int gDim,
                                          double[] potential,
                                          double[] phi0,
                                          int tDim,
                                          double tStart,
                                          double tStep,
                                          double coeff,
                                          double[] result);

    [DllImport("Crystallography.Native.dll")]
    private static unsafe extern void _HRTEMSolverQuasi(int gDim,
                                            int lDim,
                                            int rDim,
                                            double[] gPsi,
                                            double[] gVec,
                                            double[] gLenz,
                                            double[] rVec,
                                            double[] results);

    [DllImport("Crystallography.Native.dll")]
    private static unsafe extern void _HRTEMSolverTcc(
                                            int gDim,
                                            int lDim,
                                            int rDim,
                                            double[] gPsi,
                                            double[] gVec,
                                            double[] gLenz,
                                            double[] rVec,
                                            double[] results);


    [DllImport("Crystallography.Native.dll")]
    private static extern void _Histogram(
                                            int width, int height,
                                            double centerX, double centerY,
                                            double pixSizeX, double pixSizeY,
                                            double fd,
                                            double ksi, double tau, double phi,
                                            double SpericalRadiusInverse,
                                            double[] Intensity, byte[] IsValid,
                                            int yMin, int yMax,
                                            double startAngle, double stepAngle,
                                            double[] r2, int r2len,
                                            double[] profile, double[] pixels
    );
    #endregion

    #region Nativeライブラリが有効かどうか
    static NativeWrapper()
    {
        Enabled = enabled();
    }

    [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
    static bool enabled()
    {
        var appPath = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace(".dll", ".Native.dll");
        if (!System.IO.File.Exists(appPath))
            return false;
        else if (System.IO.File.GetCreationTime(appPath).Ticks < new DateTime(2019, 08, 06, 19, 45, 00).Ticks)
            return false;
        try
        {
            var result = Inverse(new[,] { { new Complex(1, 0), new Complex(0, 0) }, { new Complex(0, 0), new Complex(1, 0) } });
            return result[0, 0].Real + result[1, 1].Real > 1;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Native ライブラリが有効かどうか
    /// </summary>

    public static bool Enabled { get; }

    #endregion

    #region 変換関数
    unsafe readonly static int sizeOfComplex = sizeof(Complex);

    unsafe public static void toDoubleArray(int dim, Complex[,] mat, ref double[] dest)
    {
        //fixed (Complex* pSrc = mat)
        //fixed (double* pDest = dest)
        //    Memcpy((IntPtr)pDest, (IntPtr)pSrc, (UIntPtr)(dim * dim * sizeOfComplex));

        //　[,]の配列については、格納順の問題から、全体に対するMemcpyは使えない
        int n = 0;
        for (int j = 0; j < dim; j++)
            for (int i = 0; i < dim; i++)
            {
                dest[n++] = mat[i, j].Real;
                dest[n++] = mat[i, j].Imaginary;
            }
    }

    unsafe public static double[] toDoubleArray(int dim, Complex[,] mat)
    {
        double[] dest = new double[dim * dim * 2];
        int n = 0;
        for (int j = 0; j < dim; j++)
            for (int i = 0; i < dim; i++)
            {
                dest[n++] = mat[i, j].Real;
                dest[n++] = mat[i, j].Imaginary;
            }
        return dest;
    }

    unsafe public static void toDoubleArray(int dim, Complex[] vec, ref double[] dest)
    {
        fixed (Complex* pSrc = vec)
        fixed (double* pDest = dest)
            Memcpy((IntPtr)pDest, (IntPtr)pSrc, (UIntPtr)(dim * sizeOfComplex));
    }

    unsafe public static double[] toDoubleArray(int dim, Complex[] vec)
    {
        double[] dest = new double[dim * 2];
        toDoubleArray(dim, vec, ref dest);
        return dest;
    }

    unsafe private static DenseMatrix toDenseMatrix(int dim, in double[] src)
    {
        var dest = new Complex[dim * dim];
        fixed (double* pSrc = src)
        fixed (Complex* pDest = dest)
            Memcpy((IntPtr)pDest, (IntPtr)pSrc, (UIntPtr)(dim * dim * sizeOfComplex));

        return new DenseMatrix(dim, dim, dest);
    }


    unsafe public static DenseVector toDenseVector(int dim, in double[] src)
    {
        var dest = new Complex[dim];
        fixed (double* pSrc = src)
        fixed (Complex* pDest = dest)
            Memcpy((IntPtr)pDest, (IntPtr)pSrc, (UIntPtr)(dim * sizeOfComplex));

        return new DenseVector(dest);
    }

    unsafe public static Complex[] toComplexArray(int dim, in double[] src)
    {
        var dest = new Complex[dim];
        fixed (double* pSrc = src)
        fixed (Complex* pDest = dest)
            Memcpy((IntPtr)pDest, (IntPtr)pSrc, (UIntPtr)(dim * sizeOfComplex));

        return dest;
    }
    #endregion


    #region STEMの非弾性散乱電子強度の計算用の特殊関数
    /// <summary>
    ///　Eigenライブラリーを利用して、非対称複素行列の乗算を求める. 
    /// </summary>
    /// <param name="dim"></param>
    /// <param name="matrix1"></param>
    /// <param name="matrix2"></param>
    /// <param name="result"></param>
    unsafe static public Complex STEM_TDS1(int dim, in Complex[] B, in Complex[] U, in Complex[] C_k, in Complex[] C_kq)
    {
        var result = new double[2];
        fixed (Complex* b = B)
        fixed (Complex* c_kq = C_kq)
        fixed (Complex* u = U)
        fixed (Complex* c_k = C_k)
        fixed (double* res = result)
            _STEM_TDS1(dim, (double*)b, (double*)u, (double*)c_k, (double*)c_kq, res);

        return new Complex(result[0], result[1]);
    }

    unsafe static public void STEM_TDS2(int dim, in Complex[] U, in Complex[] C_k, in Complex[] C_kq, ref Complex[] result)
    {
        fixed (Complex* c_kq = C_kq)
        fixed (Complex* u = U)
        fixed (Complex* c_k = C_k)
        fixed (Complex* res = result)
            _STEM_TDS2(dim, (double*)u, (double*)c_k, (double*)c_kq, (double*)res);
    }
    #endregion



    #region Eigenライブラリーを利用して、非対称複素行列の要素ごとの掛算(アダマール積)を求める
    /// <summary>
    ///　Eigenライブラリーを利用して、非対称複素行列の乗算を求める. 
    /// </summary>
    /// <param name="dim"></param>
    /// <param name="matrix1"></param>
    /// <param name="matrix2"></param>
    /// <param name="result"></param>
    unsafe static public void PointwiseMultiply(int dim, Complex[] matrix1, Complex[] matrix2, ref Complex[] result)
    {
        fixed (Complex* mtx1 = matrix1)
        fixed (Complex* mtx2 = matrix2)
        fixed (Complex* res = result)
            _PointwiseMultiply(dim, (double*)mtx1, (double*)mtx2, (double*)res);
    }
    #endregion

    #region Eigenライブラリーを利用して、非対称複素行列の乗算を求める
    /// <summary>
    ///　Eigenライブラリーを利用して、非対称複素行列の乗算を求める. matrix1の形状は崩れるかもしれない
    /// </summary>
    /// <param name="dim"></param>
    /// <param name="matrix1"></param>
    /// <param name="matrix2"></param>
    /// <param name="result"></param>
    unsafe static public void AdjointAndMultiply(int dim, Complex[] matrix1, Complex[] matrix2, ref Complex[] result)
    {
        fixed (Complex* mtx1 = matrix1)
        fixed (Complex* mtx2 = matrix2)
        fixed (Complex* res = result)
            _AdjointAndMultiply(dim, (double*)mtx1, (double*)mtx2, (double*)res);
    }
    #endregion

    #region Eigenライブラリーを利用して、非対称複素行列の乗算を求める
    /// <summary>
    ///　Eigenライブラリーを利用して、非対称複素行列の乗算を求める
    /// </summary>
    /// <param name="dim"></param>
    /// <param name="matrix1"></param>
    /// <param name="matrix2"></param>
    /// <param name="result"></param>
    unsafe static public void Multiply(int dim, Complex[] matrix1, Complex[] matrix2, ref Complex[] result)
    {
        fixed (Complex* mtx1 = matrix1)
        fixed (Complex* mtx2 = matrix2)
        fixed (Complex* res = result)
            _Multiply(dim, (double*)mtx1, (double*)mtx2, (double*)res);
    }
    #endregion


    #region 逆行列
    /// <summary>
    /// Eigenライブラリーを利用して、非対称複素行列の逆行列を求める
    /// </summary>
    /// <param name="mat"></param>
    /// <returns></returns>
    static public DenseMatrix Inverse(DenseMatrix mat) => Inverse(mat.Storage.ToArray());

    /// <summary>
    /// Eigenライブラリーを利用して、非対称複素行列の逆行列を求める
    /// </summary>
    /// <param name="mat"></param>
    /// <returns></returns>
    static public DenseMatrix Inverse(Complex[,] mat)
    {
        var dim = mat.GetLength(0);
        var _mat = ArrayPool<double>.Shared.Rent(dim * dim * 2);
        var _inv = ArrayPool<double>.Shared.Rent(dim * dim * 2);
        try
        {
            toDoubleArray(dim, mat, ref _mat);
            _Inverse(dim, _mat, _inv);
            return toDenseMatrix(dim, in _inv);
        }
        finally
        {
            ArrayPool<double>.Shared.Return(_mat);
            ArrayPool<double>.Shared.Return(_inv);
        }
    }

    /// <summary>
    /// Eigenライブラリーを利用して、非対称複素行列の逆行列を求める
    /// </summary>
    /// <param name="mat1"></param>
    /// <returns></returns>
    static public DenseMatrix Inverse(int dim, Complex[] mat)
    {
        var _mat = ArrayPool<double>.Shared.Rent(dim * dim * 2);
        var _inv = ArrayPool<double>.Shared.Rent(dim * dim * 2);
        try
        {
            toDoubleArray(dim * dim, mat, ref _mat);
            _Inverse(dim, _mat, _inv);
            return toDenseMatrix(dim, in _inv);
        }
        finally
        {
            ArrayPool<double>.Shared.Return(_mat);
            ArrayPool<double>.Shared.Return(_inv);
        }
    }
    #endregion 逆行列

    #region 固有値
    /// <summary>
    /// Eigenライブラリーを利用して、非対称複素行列の固有値、固有ベクトルを求める
    /// </summary>
    /// <param name="mat"></param>
    /// <returns></returns>
    static public (DenseVector eigenvalues, DenseMatrix eigenvectors) EigenSolver(Complex[,] mat)
    {
        var dim = mat.GetLength(0);
        var _mat = ArrayPool<double>.Shared.Rent(dim * dim * 2);
        var values = ArrayPool<double>.Shared.Rent(dim * 2);
        var vectors = ArrayPool<double>.Shared.Rent(dim * dim * 2);
        try
        {
            toDoubleArray(dim, mat, ref _mat);//matをdouble[]に変換
            _EigenSolver(dim, _mat, values, vectors);
            return (toDenseVector(dim, in values), toDenseMatrix(dim, in vectors));
        }
        finally
        {
            ArrayPool<double>.Shared.Return(_mat);
            ArrayPool<double>.Shared.Return(values);
            ArrayPool<double>.Shared.Return(vectors);
        }
    }

    static public (DenseVector eigenvalues, DenseMatrix eigenvectors) EigenSolver(int dim, Complex[] mat)
    {
        var _mat = ArrayPool<double>.Shared.Rent(dim * dim * 2);
        var values = ArrayPool<double>.Shared.Rent(dim * 2);
        var vectors = ArrayPool<double>.Shared.Rent(dim * dim * 2);
        try
        {
            toDoubleArray(dim * dim, mat, ref _mat);//matをdouble[]に変換
            _EigenSolver(dim, _mat, values, vectors);
            return (toDenseVector(dim, in values), toDenseMatrix(dim, in vectors));
        }
        finally
        {
            ArrayPool<double>.Shared.Return(_mat);
            ArrayPool<double>.Shared.Return(values);
            ArrayPool<double>.Shared.Return(vectors);
        }
    }

    #endregion 固有値

    #region 行列指数関数
    static public DenseMatrix MatrixExponential(DenseMatrix mat)
    {
        var dim = mat.RowCount;
        var _mat = ArrayPool<double>.Shared.Rent(dim * dim * 2);
        var vectors = ArrayPool<double>.Shared.Rent(dim * dim * 2);
        try
        {
            toDoubleArray(dim, mat.ToArray(), ref _mat);//matをdouble[]に変換
            _MatrixExponential(dim, _mat, vectors);
            return toDenseMatrix(dim, in vectors);
        }
        finally
        {
            ArrayPool<double>.Shared.Return(_mat);
            ArrayPool<double>.Shared.Return(vectors);
        }
    }

    #endregion

    #region CBED
    /// <summary>
    /// Eigenライブラリーを利用して固有値解を求めて、CBEDの解を求める
    /// </summary>
    /// <param name="potential"></param>
    /// <param name="psi0"></param>
    /// <param name="thickness"></param>
    /// <param name="coeff"></param>
    /// <returns></returns>
    unsafe static public Complex[][] CBEDSolver_Eigen(Complex[] potential, Complex[] psi0, double[] thickness, double coeff)
    => CBEDSolver(potential, psi0, thickness, coeff, true);

    /// <summary>
    /// Eigenライブラリーを利用してMatrix exponentialを解いて、CBEDの解を求める. 
    /// </summary>
    /// <param name="potential"></param>
    /// <param name="psi0"></param>
    /// <param name="thickness"></param>
    /// <param name="coeff"></param>
    /// <returns></returns>
    unsafe static public Complex[][] CBEDSolver_MatExp(Complex[] potential, Complex[] psi0, double[] thickness, double coeff)
        => CBEDSolver(potential, psi0, thickness, coeff, false);

    unsafe static private Complex[][] CBEDSolver(Complex[] potential, Complex[] psi0, double[] thickness, double coeff, bool eigen)
    {
        var dim = psi0.Length;
        var _potential = ArrayPool<double>.Shared.Rent(dim * dim * 2);
        var _psi0 = ArrayPool<double>.Shared.Rent(dim * 2);
        var tempResult = ArrayPool<double>.Shared.Rent(dim * thickness.Length * 2);
        try
        {
            toDoubleArray(dim * dim, potential, ref _potential);
            toDoubleArray(dim, psi0, ref _psi0);

            if (eigen)
                _CBEDSolver_Eigen(dim, _potential, _psi0, thickness.Length, thickness, coeff, tempResult);
            else
            {
                var tStep = thickness.Length > 1 ? thickness[1] - thickness[0] : 0.0;
                _CBEDSolver_MtxExp(dim, _potential, _psi0, thickness.Length, thickness[0], tStep, coeff, tempResult);
            }

            var result = new Complex[thickness.Length][];
            fixed (double* pSrc = tempResult)
                for (int t = 0; t < thickness.Length; t++)
                {
                    result[t] = new Complex[dim];
                    fixed (Complex* pDest = result[t])
                        Memcpy((IntPtr)pDest, (IntPtr)(pSrc + t * dim * 2), (UIntPtr)(dim * sizeOfComplex));
                }

            return result;
        }
        finally
        {
            ArrayPool<double>.Shared.Return(_potential);
            ArrayPool<double>.Shared.Return(_psi0);
            ArrayPool<double>.Shared.Return(tempResult);
        }
    }

    #endregion

    #region HRTEM simulation
    static public double[] HRTEM_Solver(double[] gPsi, double[] gVec, double[] gLenz, double[] rVec, bool quasiMode)
    {
        var gDim = gPsi.Length / 2;
        var lDim = gLenz.Length / gPsi.Length;
        var rDim = rVec.Length / 2;

        var results = new double[lDim * rDim];

        if (quasiMode)
            _HRTEMSolverQuasi(gDim, lDim, rDim, gPsi, gVec, gLenz, rVec, results);
        else
            _HRTEMSolverTcc(gDim, lDim, rDim, gPsi, gVec, gLenz, rVec, results);

        return results;
    }

    static public (double[] gPsi, double[] gVec, double[] gLenz) HRTEM_Helper(IEnumerable<(Complex Psi, PointD Vec, Complex[] Lenz)> g)
    {
        var gP = g.AsParallel();
        return (
            gP.SelectMany(e => new[] { e.Psi.Real, e.Psi.Imaginary }).ToArray(),
            gP.SelectMany(e => new[] { e.Vec.X, e.Vec.Y }).ToArray(),
            gP.SelectMany(e => e.Lenz.SelectMany(l => new[] { l.Real, l.Imaginary }).ToArray()).ToArray()
            );
    }
    #endregion

    #region Histogram
    static public (double[] profile, double[] pixels) Histogram(
        int width, int height,
        double centerX, double centerY,
        double pixSizeX, double pixSizeY,
        double fd,
        double ksi, double tau, double phi,
        double SpericalRadiusInverse,
        double[] Intensity, byte[] IsValid,
        int yMin, int yMax,
        double startAngle, double stepAngle,
        double[] r2
        )
    {
        var profile = new double[r2.Length];
        var pixels = new double[r2.Length];
        _Histogram(
            width, height,
            centerX, centerY,
            pixSizeX, pixSizeY,
            fd,
            ksi, tau, phi,
            SpericalRadiusInverse,
            Intensity, IsValid,
            yMin, yMax,
            startAngle, stepAngle,
            r2, r2.Length,
            profile, pixels);

        return (profile, pixels);
    }
    #endregion
}
