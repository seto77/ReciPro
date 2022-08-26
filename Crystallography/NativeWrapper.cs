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
    public static extern IntPtr Memcpy(IntPtr dest, IntPtr src, UIntPtr count);

    [DllImport("Crystallography.Native.dll")]
    unsafe private static extern void _PartialPivLuSolve(int dim, double* mat, double* vec, double* result);

    [DllImport("Crystallography.Native.dll")]
    unsafe private static extern void _Blend(int dim, double* c0, double* c1, double* c2, double* c3, double r0, double r1, double r2, double r3, double* result);

    [DllImport("Crystallography.Native.dll")]
    unsafe private static extern void _BlendAndConjugate(int dim, double* c0, double* c1, double* c2, double* c3, double r0, double r1, double r2, double r3, double* result);

    [DllImport("Crystallography.Native.dll")]
    unsafe private static extern void _AdJointMul_Mul_Mul(int dim, double* mat1, double* mat2, double* mat3, double* result);

    [DllImport("Crystallography.Native.dll")]
    unsafe private static extern void _BlendAdJointMul_Mul_Mul(int dim, double* c0, double* c1, double* c2, double* c3, double r0, double r1, double r2, double r3, double* mat2, double* mat3, double* result);


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
    unsafe private static extern void _Multiply(int dim, double* mat1, double* mat2, double* result);
    [DllImport("Crystallography.Native.dll")]
    unsafe private static extern void _MultiplyVec(int dim, double* mat, double* vec, double* result);

    [DllImport("Crystallography.Native.dll")]
    private static extern void _Inverse(int dim, double[] mat, double[] matInv);

    [DllImport("Crystallography.Native.dll")]
    private unsafe static extern void _Inverse(int dim, double* mat, double* matInv);

    [DllImport("Crystallography.Native.dll")]
    private static extern void _EigenSolver(int dim, double[] mat, double[] eigenValues, double[] eigenVectors);

    [DllImport("Crystallography.Native.dll")]
    private unsafe static extern void _EigenSolver(int dim, double* mat, double* eigenValues, double* eigenVectors);

    [DllImport("Crystallography.Native.dll")]
    private static extern void _MatrixExponential(int dim,
                                           double[] mat,
                                           double[] results);


    [DllImport("Crystallography.Cuda.dll")]
    private static extern void MatrixExponential_Cuda(int dim,
                                          double[] mat,
                                          double[] results);

    [DllImport("Crystallography.Cuda.dll")]
    private unsafe static extern void _CBEDSolver_MtxExp_Cuda(int gDim,
                                         double[] potential,
                                         double[] phi0,
                                         int tDim,
                                         double tStart,
                                         double tStep,
                                         double coeff,
                                         double[] result);

    [DllImport("Crystallography.Native.dll")]
    private unsafe static extern void _CBEDSolver_Eigen(int gDim,
                                          double* potential,
                                         double* phi0,
                                         int tDim,
                                         double[] thickness,
                                         double coeff,
                                         double* result);

    [DllImport("Crystallography.Native.dll")]
    private unsafe static extern void _CBEDSolver_MtxExp(int gDim,
                                  double* potential,
                                  double* phi0,
                                  int tDim,
                                  double tStart,
                                  double tStep,
                                  double coeff,
                                  double* result);

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
            var result = Inverse(2, new[] { new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0) });
            return result[0].Real + result[3].Real > 1;
        }
        catch { return false; }
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

    unsafe static public void Blend(int dim, in Complex[] c0, in Complex[] c1, in Complex[] c2, in Complex[] c3, double r0, double r1, double r2, double r3, ref Complex[] result)
    {
        fixed (Complex* p0 = c0)
        fixed (Complex* p1 = c1)
        fixed (Complex* p2 = c2)
        fixed (Complex* p3 = c3)
        fixed (Complex* res = result)
            _Blend(dim, (double*)p0, (double*)p1, (double*)p2, (double*)p3, r0, r1, r2, r3, (double*)res);
    }
    unsafe static public void BlendAndConjugate(int dim, in Complex[] c0, in Complex[] c1, in Complex[] c2, in Complex[] c3, double r0, double r1, double r2, double r3, ref Complex[] result)
    {
        fixed (Complex* p0 = c0)
        fixed (Complex* p1 = c1)
        fixed (Complex* p2 = c2)
        fixed (Complex* p3 = c3)
        fixed (Complex* res = result)
            _BlendAndConjugate(dim, (double*)p0, (double*)p1, (double*)p2, (double*)p3, r0, r1, r2, r3, (double*)res);
    }

    #region STEMの非弾性散乱電子強度の計算用の特殊関数
    unsafe static public void AdjointMul_Mul_Mul(int dim, in Complex[] mat1, in Complex[] mat2, in Complex[] mat3, ref Complex[] result)
    {
        fixed (Complex* _mat1 = mat1)
        fixed (Complex* _mat2 = mat2)
        fixed (Complex* _mat3 = mat3)
        fixed (Complex* res = result)
            _AdJointMul_Mul_Mul(dim, (double*)_mat1, (double*)_mat2, (double*)_mat3, (double*)res);
    }

    unsafe static public void BlendAdjointMul_Mul_Mul(int dim, in Complex[] c0, in Complex[] c1, in Complex[] c2, in Complex[] c3, double r0, double r1, double r2, double r3,
        in Complex[] mat2, in Complex[] mat3, ref Complex[] result)
    {
        fixed (Complex* p0 = c0)
        fixed (Complex* p1 = c1)
        fixed (Complex* p2 = c2)
        fixed (Complex* p3 = c3)
        fixed (Complex* _mat2 = mat2)
        fixed (Complex* _mat3 = mat3)
        fixed (Complex* res = result)
            _BlendAdJointMul_Mul_Mul(dim, (double*)p0, (double*)p1, (double*)p2, (double*)p3, r0, r1, r2, r3, (double*)_mat2, (double*)_mat3, (double*)res);
    }
    #endregion

    #region Eigenライブラリーを利用して、PartialPivLuSolveを求める
    unsafe static public void PartialPivLuSolve(int dim, Complex[] mat, Complex[] vec, ref Complex[] result)
    {
        fixed (Complex* m = mat)
        fixed (Complex* v = vec)
        fixed (Complex* res = result)
            _PartialPivLuSolve(dim, (double*)m, (double*)v, (double*)res);
    }
    unsafe static public Complex[] PartialPivLuSolve(int dim, Complex[] mat, Complex[] vec)
    {
        var result = GC.AllocateUninitializedArray<Complex>(dim);// new Complex[dim];
        PartialPivLuSolve(dim, mat, vec, ref result);
        return result;
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

    unsafe static public Complex[] Multiply(int dim, Complex[] matrix1, Complex[] matrix2)
    {
        var result = GC.AllocateUninitializedArray<Complex>(dim * dim);// new Complex[dim * dim];
        Multiply(dim, matrix1, matrix2, ref result);
        return result;
    }

    unsafe static public void MultiplyVec(int dim, Complex[] matrix, Complex[] vector, ref Complex[] result)
    {
        fixed (Complex* mtx1 = matrix)
        fixed (Complex* mtx2 = vector)
        fixed (Complex* res = result)
            _MultiplyVec(dim, (double*)mtx1, (double*)mtx2, (double*)res);
    }

    unsafe static public Complex[] MultiplyVec(int dim, Complex[] matrix, Complex[] vector)
    {
        var result = GC.AllocateUninitializedArray<Complex>(dim);// new Complex[dim];
        MultiplyVec(dim, matrix, vector, ref result);
        return result;
    }
    #endregion

    #region 逆行列
    /// <summary>
    /// Eigenライブラリーを利用して、非対称複素行列の逆行列を求める
    /// </summary>
    /// <param name="mat"></param>
    /// <returns></returns>
    static unsafe public Complex[] Inverse(int dim, Complex[] mat)
    {
        var values = GC.AllocateUninitializedArray<Complex>(dim * dim);//new Complex[dim* dim];
        fixed (Complex* _values = values)
        fixed (Complex* _mat = mat)
        _Inverse(dim, (double*)_mat, (double*)_values);
        return values;
    }

    #endregion 逆行列

    #region 固有値

    static unsafe public (Complex[] eigenvalues, Complex[] eigenvectors) EigenSolver(int dim, Complex[] mat)
    {
        var values = GC.AllocateUninitializedArray<Complex>(dim);//new Complex[dim];
        var vectors = GC.AllocateUninitializedArray<Complex>(dim * dim);//new Complex[dim * dim];
        fixed (Complex* _values = values)
        fixed (Complex* _vectors = vectors)
        fixed (Complex* _mat = mat)
            _EigenSolver(dim, (double*)_mat, (double*)_values, (double*)_vectors);
        return (values, vectors);
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
    unsafe static public Complex[] CBEDSolver_Eigen(Complex[] potential, Complex[] psi0, double[] thickness, double coeff)
    => CBEDSolver(potential, psi0, thickness, coeff, true);

    /// <summary>
    /// Eigenライブラリーを利用してMatrix exponentialを解いて、CBEDの解を求める. 
    /// </summary>
    /// <param name="potential"></param>
    /// <param name="psi0"></param>
    /// <param name="thickness"></param>
    /// <param name="coeff"></param>
    /// <returns></returns>
    unsafe static public Complex[] CBEDSolver_MatExp(Complex[] potential, Complex[] psi0, double[] thickness, double coeff)
        => CBEDSolver(potential, psi0, thickness, coeff, false);

    unsafe static private Complex[] CBEDSolver(Complex[] potential, Complex[] psi0, double[] thickness, double coeff, bool eigen)
    {
        var dim = psi0.Length;
        var result = GC.AllocateUninitializedArray<Complex>(dim * thickness.Length);// new Complex[dim * thickness.Length];
        fixed (Complex* _potential = potential)
        fixed (Complex* _psi0 = psi0)
        fixed (Complex* _result = result)
        {
            if (eigen)
                _CBEDSolver_Eigen(dim, (double*)_potential, (double*)_psi0, thickness.Length, thickness, coeff, (double*)_result);
            else
            {
                var tStep = thickness.Length > 1 ? thickness[1] - thickness[0] : 0.0;
                _CBEDSolver_MtxExp(dim, (double*)_potential, (double*)_psi0, thickness.Length, thickness[0], tStep, coeff, (double*)_result);
            }
        }
        return result;
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
