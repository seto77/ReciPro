#region using, namespace
using MathNet.Numerics.LinearAlgebra.Complex;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
namespace Crystallography;
#endregion

public static partial class NativeWrapper
{
    #region LibraryImport
    public enum Library { None, Eigen, Cuda }
    //[DllImport("msvcrt.dll", EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
    //public static extern IntPtr Memcpy(IntPtr dest, IntPtr src, UIntPtr count);

    [LibraryImport("Crystallography.Native.dll")]
    private static unsafe partial void _PartialPivLuSolve(int dim, double* mat, double* vec, double* result);

    [LibraryImport("Crystallography.Native.dll")]
    private static unsafe partial void _Blend(int dim, double* c0, double* c1, double* c2, double* c3, double r0, double r1, double r2, double r3, double* result);

    [LibraryImport("Crystallography.Native.dll")]
    private static unsafe partial void _BlendAndConjugate(int dim, double* c0, double* c1, double* c2, double* c3, double r0, double r1, double r2, double r3, double* result);

    [LibraryImport("Crystallography.Native.dll")]
    private static unsafe partial void _AdJointMul_Mul_Mul(int dim, double* mat1, double* mat2, double* mat3, double* result);

    [LibraryImport("Crystallography.Native.dll")]
    private static unsafe partial void _BlendAdJointMul_Mul_Mul(int dim, double* c0, double* c1, double* c2, double* c3, double r0, double r1, double r2, double r3, double* mat2, double* mat3, double* result);


    [LibraryImport("Crystallography.Native.dll")]
    private static unsafe partial void _PointwiseMultiply(int dim, double* mat1, double* mat2, double* result);

    [LibraryImport("Crystallography.Native.dll")]
    private static unsafe partial void _AdjointAndMultiply(int dim, double* mat1, double* mat2, double* result);
    [LibraryImport("Crystallography.Native.dll")]
    private static unsafe partial void _MultiplyMM(int dim, double* mat1, double* mat2, double* result);

    [LibraryImport("Crystallography.Native.dll")]
    private static unsafe partial void _MultiplyMMM(int dim, double* mat1, double* mat2, double* mat3, double* result);

    [LibraryImport("Crystallography.Native.dll")]
    private static unsafe partial void _MultiplyMV(int dim, double* mat, double* vec, double* result);

    [LibraryImport("Crystallography.Native.dll")]
    private static unsafe partial void _MultiplyVV(int dim, double* vec1, double* vec2, double* result);

    [LibraryImport("Crystallography.Native.dll")]
    private static unsafe partial void _MultiplySV(int dim, double real, double imag, double* vec, double* result);


    [LibraryImport("Crystallography.Native.dll")]
    private static unsafe partial void _DivideVV(int dim, double* vec1, double* vec2, double* result);

    [LibraryImport("Crystallography.Native.dll")]
    private static unsafe partial void _AddVV(int dim, double* vec1, double* vec2, double* result);

    [LibraryImport("Crystallography.Native.dll")]
    private static unsafe partial void _SubtractVV(int dim, double* vec1, double* vec2, double* result);


    //[LibraryImport("Crystallography.Native.dll")]
    //private static unsafe partial void _Inverse(int dim, double[] mat, double[] matInv);

    [LibraryImport("Crystallography.Native.dll")]
    private static unsafe partial void _Inverse(int dim, double* mat, double* matInv);

    [LibraryImport("Crystallography.Native.dll")]
    private static unsafe partial void _Inverse_Real(int dim, double* mat, double* matInv);

    //[LibraryImport("Crystallography.Native.dll")]
    //private static unsafe partial void _EigenSolver(int dim, in double[] mat, in double[] eigenValues, double[] eigenVectors);

    [LibraryImport("Crystallography.Native.dll")]
    private static unsafe partial void _EigenSolver(int dim, double* mat, double* eigenValues, double* eigenVectors);

    [LibraryImport("Crystallography.Native.dll")]
    private static unsafe partial void _MatrixExponential(int dim, double* mat, double* results);

    [LibraryImport("Crystallography.Cuda.dll")]
    private static unsafe partial void MatrixExponential_Cuda(int dim, double[] mat, double[] results);
    [LibraryImport("Crystallography.Cuda.dll")]
    private static unsafe partial void _CBEDSolver_MtxExp_Cuda(int gDim, double[] potential, double[] phi0, int tDim, double tStart, double tStep, double[] result);

    [LibraryImport("Crystallography.Native.dll")]
    private static unsafe partial void _CBEDSolver_Eigen(int gDim, double* potential, double* phi0, int tDim, double[] thickness, double* result);

    [LibraryImport("Crystallography.Native.dll")]
    private static unsafe partial void _CBEDSolver_Eigen2(int gDim,
                                        double* potential,
                                        double* phi0,
                                        int tDim,
                                        double[] thickness,
                                        double* values,
                                        double* vectors,
                                        double* alphas,
                                        double* result);

    [LibraryImport("Crystallography.Native.dll")]
    private static unsafe partial void _CBEDSolver_MtxExp(int gDim,
                                  double* potential,
                                  double* phi0,
                                  int tDim,
                                  double tStart,
                                  double tStep,
                                  double* result);

    [LibraryImport("Crystallography.Native.dll")]
    private static unsafe partial void _HRTEMSolverQuasi(int gDim,
                                            int lDim,
                                            int rDim,
                                            double[] gPsi,
                                            double[] gVec,
                                            double[] gLenz,
                                            double[] rVec,
                                            double[] results);

    [LibraryImport("Crystallography.Native.dll")]
    private static unsafe partial void _HRTEMSolverTcc(
                                            int gDim,
                                            int lDim,
                                            int rDim,
                                            double[] gPsi,
                                            double[] gVec,
                                            double[] gLenz,
                                            double[] rVec,
                                            double[] results);


    [LibraryImport("Crystallography.Native.dll")]
    private static unsafe partial void _Histogram(
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

    [LibraryImport("Crystallography.Native.dll")]
    private static unsafe partial void _GenerateTC1(int dim, double thickness, double* _kg_z, double* _val, double* _vec, double* _tc_k);

    [LibraryImport("Crystallography.Native.dll")]
    private static unsafe partial void _GenerateTC2(int dim, double thickness, double* _kg_z, double* _val, double* _vec, double* _tc_k, double* _tc_kq);

    [LibraryImport("Crystallography.Native.dll")]
    private static unsafe partial void _RowVec_SqMat_ColVec(int dim, double* _rowVec, double* _sqMat, double* _colVec, double* _result);

    [LibraryImport("Crystallography.Native.dll")]
    private static unsafe partial void _SqMat_ColVec(int dim, double* _sqMat, double* _colVec, double* _result);

    [LibraryImport("Crystallography.Native.dll")]
    private static unsafe partial void _STEM_INEL1(int dim, double* rowVec, int* n, double* r, double* sqMat, double* colVec, double* _result);

    #endregion

    #region Nativeライブラリが有効かどうか

    /// <summary>
    /// Native ライブラリが有効かどうか
    /// </summary>
    public static bool Enabled { get; }

    static NativeWrapper()
    {

        var appPath = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace(".dll", ".Native.dll");
        if (!System.IO.File.Exists(appPath))
            Enabled = false;
        else if (System.IO.File.GetCreationTime(appPath).Ticks < new DateTime(2019, 08, 06, 19, 45, 00).Ticks)
            Enabled = false;
        try
        {
            var result = Inverse(2, [new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0)]);
            Enabled = result[0].Real + result[3].Real > 1;
        }
        catch { Enabled = false; }
    }
    #endregion

    #region 変換関数
    unsafe readonly static int sizeOfComplex = sizeof(Complex);

    //unsafe public static void toDoubleArray(int dim, Complex[,] mat, ref double[] dest)
    //{
    //    //fixed (Complex* pSrc = mat)
    //    //fixed (double* pDest = dest)
    //    //    Memcpy((IntPtr)pDest, (IntPtr)pSrc, (UIntPtr)(dim * dim * sizeOfComplex));

    //    //　[,]の配列については、格納順の問題から、全体に対するMemcpyは使えない
    //    int n = 0;
    //    for (int j = 0; j < dim; j++)
    //        for (int i = 0; i < dim; i++)
    //        {
    //            dest[n++] = mat[i, j].Real;
    //            dest[n++] = mat[i, j].Imaginary;
    //        }
    //}

    //unsafe public static double[] toDoubleArray(int dim, Complex[,] mat)
    //{
    //    double[] dest = new double[dim * dim * 2];
    //    int n = 0;
    //    for (int j = 0; j < dim; j++)
    //        for (int i = 0; i < dim; i++)
    //        {
    //            dest[n++] = mat[i, j].Real;
    //            dest[n++] = mat[i, j].Imaginary;
    //        }
    //    return dest;
    //}

    //unsafe public static void toDoubleArray(int dim, Complex[] vec, ref double[] dest)
    //{
    //    fixed (Complex* pSrc = vec)
    //    fixed (double* pDest = dest)
    //        Memcpy((IntPtr)pDest, (IntPtr)pSrc, (UIntPtr)(dim * sizeOfComplex));
    //}

    //unsafe public static double[] toDoubleArray(int dim, Complex[] vec)
    //{
    //    double[] dest = new double[dim * 2];
    //    toDoubleArray(dim, vec, ref dest);
    //    return dest;
    //}

    //unsafe private static DenseMatrix toDenseMatrix(int dim, in double[] src)
    //{
    //    var dest = new Complex[dim * dim];
    //    fixed (double* pSrc = src)
    //    fixed (Complex* pDest = dest)
    //        Memcpy((IntPtr)pDest, (IntPtr)pSrc, (UIntPtr)(dim * dim * sizeOfComplex));

    //    return new DenseMatrix(dim, dim, dest);
    //}


    //unsafe public static DenseVector toDenseVector(int dim, in double[] src)
    //{
    //    var dest = new Complex[dim];
    //    fixed (double* pSrc = src)
    //    fixed (Complex* pDest = dest)
    //        Memcpy((IntPtr)pDest, (IntPtr)pSrc, (UIntPtr)(dim * sizeOfComplex));

    //    return new DenseVector(dest);
    //}

    //unsafe public static Complex[] toComplexArray(int dim, in double[] src)
    //{
    //    var dest = new Complex[dim];
    //    fixed (double* pSrc = src)
    //    fixed (Complex* pDest = dest)
    //        Memcpy((IntPtr)pDest, (IntPtr)pSrc, (UIntPtr)(dim * sizeOfComplex));

    //    return dest;
    //}
    #endregion

    #region 単純な四則演算

    #region 行列×行列
    /// <summary>
    ///　Eigenライブラリーを利用して、非対称複素行列の乗算を求める
    /// </summary>
    /// <param name="dim"></param>
    /// <param name="matrix1"></param>
    /// <param name="matrix2"></param>
    /// <param name="result"></param>
    unsafe static public void MultiplyMxM(int dim, Complex[] matrix1, Complex[] matrix2, ref Complex[] result)
    {
        fixed (Complex* mtx1 = matrix1)
        fixed (Complex* mtx2 = matrix2)
        fixed (Complex* res = result)
            _MultiplyMM(dim, (double*)mtx1, (double*)mtx2, (double*)res);
    }
    unsafe static public Complex[] MultiplyMxM(int dim, Complex[] matrix1, Complex[] matrix2)
    {
        var result = GC.AllocateUninitializedArray<Complex>(dim * dim);
        MultiplyMxM(dim, matrix1, matrix2, ref result);
        return result;
    }

    unsafe static public void MultiplyMxMxM(int dim, Complex[] matrix1, Complex[] matrix2, Complex[] matrix3, ref Complex[] result)
    {
        fixed (Complex* mtx1 = matrix1)
        fixed (Complex* mtx2 = matrix2)
        fixed (Complex* mtx3 = matrix3)
        fixed (Complex* res = result)
            _MultiplyMMM(dim, (double*)mtx1, (double*)mtx2, (double*)mtx3, (double*)res);
    }
    #endregion

    #region 行列×ベクトル
    unsafe static public void MultiplyMxV(int dim, Complex[] matrix, Complex[] vector, ref Complex[] result)
    {
        fixed (Complex* mtx1 = matrix)
        fixed (Complex* mtx2 = vector)
        fixed (Complex* res = result)
            _MultiplyMV(dim, (double*)mtx1, (double*)mtx2, (double*)res);
    }

    unsafe static public Complex[] MultiplyMxV(int dim, Complex[] matrix, Complex[] vector)
    {
        var result = GC.AllocateUninitializedArray<Complex>(dim);
        MultiplyMxV(dim, matrix, vector, ref result);
        return result;
    }
    #endregion

    #region ベクトル×ベクトル
    unsafe static public void MultiplyVxV(int dim, Complex[] vector1, Complex[] vector2, ref Complex result)
    {
        fixed (Complex* vec1 = vector1)
        fixed (Complex* vec2 = vector2)
        fixed (Complex* res = &result)
            _MultiplyVV(dim, (double*)vec1, (double*)vec2, (double*)res);
    }

    unsafe static public Complex MultiplyVxV(int dim, Complex* vec1, Complex* vec2)
    {
        var result = new Complex();
        _MultiplyVV(dim, (double*)vec1, (double*)vec2, (double*)&result);
        return result;
    }

    unsafe static public Complex MultiplyVxV(int dim, Complex[] vector1, Complex[] vector2)
    {
        var result = new Complex();
        MultiplyVxV(dim, vector1, vector2, ref result);
        return result;
    }


    #endregion

    #region 数値×行列
    unsafe static public void MultiplySxV(int dim, in Complex s, in Complex[] v, ref Complex[] result)
    {
        fixed (Complex* p = v)
        fixed (Complex* res = result)
            _MultiplySV(dim, s.Real, s.Imaginary, (double*)p, (double*)res);
    }

    unsafe static public Complex[] MultiplySxV(int dim, in Complex s, in Complex[] v)
    {
        var result = GC.AllocateUninitializedArray<Complex>(dim);
        MultiplySxV(dim, s, v, ref result);
        return result;
    }

    #endregion

    unsafe static public void Add(int dim, in Complex[] v1, in Complex[] v2, ref Complex[] result)
    {
        fixed (Complex* p1 = v1)
        fixed (Complex* p2 = v2)
        fixed (Complex* res = result)
            _AddVV(dim * 2, (double*)p1, (double*)p2, (double*)res);
    }
    unsafe static public void Add(int dim, in double[] v1, in double[] v2, ref double[] result)
    {
        fixed (double* p1 = v1)
        fixed (double* p2 = v2)
        fixed (double* res = result)
            _AddVV(dim, p1, p2, res);
    }

    unsafe static public void Subtract(int dim, in Complex[] v1, in Complex[] v2, ref Complex[] result)
    {
        fixed (Complex* p1 = v1)
        fixed (Complex* p2 = v2)
        fixed (Complex* res = result)
            _SubtractVV(dim * 2, (double*)p1, (double*)p2, (double*)res);
    }

    unsafe static public void Subtract(int dim, in double[] v1, in double[] v2, ref double[] result)
    {
        fixed (double* p1 = v1)
        fixed (double* p2 = v2)
        fixed (double* res = result)
            _SubtractVV(dim, p1, p2, res);
    }
    unsafe static public void Divide(int dim, in Complex[] v1, in Complex[] v2, ref Complex[] result)
    {
        fixed (Complex* p1 = v1)
        fixed (Complex* p2 = v2)
        fixed (Complex* res = result)
            _DivideVV(dim, (double*)p1, (double*)p2, (double*)res);
    }

    #endregion

    #region Blend関数
    unsafe static public void Blend(in int dim, in Complex[] c0, in Complex[] c1, in Complex[] c2, in Complex[] c3, in double r0, in double r1, in double r2, in double r3, ref Complex[] result)
    {
        fixed (Complex* p0 = c0, p1 = c1, p2 = c2, p3 = c3, res = result)
            _Blend(dim * 2, (double*)p0, (double*)p1, (double*)p2, (double*)p3, r0, r1, r2, r3, (double*)res);
    }

    unsafe static public void Blend(in int dim, Complex* c0, Complex* c1, Complex* c2, Complex* c3, in double r0, in double r1, in double r2, in double r3, Complex* res)
    {
        //fixed (Complex* p0 = c0, p1 = c1, p2 = c2, p3 = c3, res = result)
        _Blend(dim * 2, (double*)c0, (double*)c1, (double*)c2, (double*)c3, r0, r1, r2, r3, (double*)res);
    }

    unsafe static public void Blend(in int dim, in double[] c0, in double[] c1, in double[] c2, in double[] c3, in double r0, in double r1, in double r2, in double r3, ref double[] result)
    {
        fixed (double* p0 = c0, p1 = c1, p2 = c2, p3 = c3, res = result)
            _Blend(dim, p0, p1, p2, p3, r0, r1, r2, r3, res);
    }

    unsafe static public void BlendAndConjugate(in int dim, in Complex[] c0, in Complex[] c1, in Complex[] c2, in Complex[] c3, in double r0, in double r1, in double r2, in double r3, ref Complex[] result)
    {
        fixed (Complex* p0 = c0, p1 = c1, p2 = c2, p3 = c3, res = result)
            _BlendAndConjugate(dim, (double*)p0, (double*)p1, (double*)p2, (double*)p3, r0, r1, r2, r3, (double*)res);
    }

    unsafe static public void BlendAndConjugate(in int dim, Complex* c0, in Complex* c1, in Complex* c2, in Complex* c3, in double r0, in double r1, in double r2, in double r3, Complex* res)
    {
        _BlendAndConjugate(dim, (double*)c0, (double*)c1, (double*)c2, (double*)c3, r0, r1, r2, r3, (double*)res);
    }
    #endregion 

    #region Eigenライブラリーを利用して、PartialPivLuSolveを求める
    unsafe static public void PartialPivLuSolve(in int dim, Complex[] mat, Complex[] vec, ref Complex[] result)
    {
        fixed (Complex* m = mat)
        fixed (Complex* v = vec)
        fixed (Complex* res = result)
            _PartialPivLuSolve(dim, (double*)m, (double*)v, (double*)res);
    }
    unsafe static public Complex[] PartialPivLuSolve(in int dim, Complex[] mat, Complex[] vec)
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



    #endregion

    #region 複素共役、転置

    unsafe static public void Adjoint(int dim, Complex[] matrix1, Complex[] matrix2, ref Complex[] result)
    {
        fixed (Complex* mtx1 = matrix1)
        fixed (Complex* mtx2 = matrix2)
        fixed (Complex* res = result)
            _AdjointAndMultiply(dim, (double*)mtx1, (double*)mtx2, (double*)res);
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

    /// <summary>
    /// Eigenライブラリーを利用して、非対称複素行列の逆行列を求める
    /// </summary>
    /// <param name="mat"></param>
    /// <returns></returns>
    static unsafe public Complex[] Inverse(Complex[] mat)
    {
        var dim = (int)Math.Sqrt(mat.Length);
        return dim * dim == mat.Length ? Inverse(dim, mat) : null;
    }

    /// <summary>
    /// Eigenライブラリーを利用して、実数行列の逆行列を求める
    /// </summary>
    /// <param name="mat"></param>
    /// <returns></returns>
    static unsafe public double[] Inverse(int dim, double[] mat)
    {
        var values = GC.AllocateUninitializedArray<double>(dim * dim);//new Complex[dim* dim];
        fixed (double* _values = values)
        fixed (double* _mat = mat)
            _Inverse_Real(dim, (double*)_mat, (double*)_values);
        return values;
    }

    /// <summary>
    /// Eigenライブラリーを利用して、実数行列の逆行列を求める
    /// </summary>
    /// <param name="mat"></param>
    /// <returns></returns>
    static unsafe public double[] Inverse(double[] mat)
    {
        var dim = (int)Math.Sqrt(mat.Length);
        return dim * dim == mat.Length ? Inverse(dim, mat) : null;
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
        => new(mat.ColumnCount, mat.ColumnCount, MatrixExponential(mat.ColumnCount, mat.Values));

    static unsafe public Complex[] MatrixExponential(in int dim, Complex[] mat)
    {
        var result = GC.AllocateUninitializedArray<Complex>(dim * dim);//new Complex[dim];
        MatrixExponential(dim, mat, ref result);
        return result;
    }

    static unsafe public void MatrixExponential(in int dim, Complex[] mat, ref Complex[] result)
    {
        fixed (Complex* _result = result, _mat = mat)
            _MatrixExponential(dim, (double*)_mat, (double*)_result);
    }

    #endregion

    #region STEMの非弾性散乱電子強度の計算用の特殊関数
    unsafe static public void AdjointMul_Mul_Mul(in int dim, in Complex[] mat1, in Complex[] mat2, in Complex[] mat3, ref Complex[] result)
    {
        fixed (Complex* _mat1 = mat1, _mat2 = mat2, _mat3 = mat3, res = result)
            _AdJointMul_Mul_Mul(dim, (double*)_mat1, (double*)_mat2, (double*)_mat3, (double*)res);
    }

    unsafe static public void BlendAdjointMul_Mul_Mul(in int dim, in Complex[] c0, in Complex[] c1, in Complex[] c2, in Complex[] c3, double r0, double r1, double r2, double r3,
        in Complex[] mat2, in Complex[] mat3, ref Complex[] result)
    {
        fixed (Complex* p0 = c0, p1 = c1, p2 = c2, p3 = c3, _mat2 = mat2, _mat3 = mat3, res = result)
            _BlendAdJointMul_Mul_Mul(dim, (double*)p0, (double*)p1, (double*)p2, (double*)p3, r0, r1, r2, r3, (double*)_mat2, (double*)_mat3, (double*)res);
    }

    unsafe static public void TDS(in int dim, in Complex[] mat1, in Complex[] mat2, in Complex[] mat3, ref Complex[] result)
    {
        fixed (Complex* _mat1 = mat1, _mat2 = mat2, _mat3 = mat3, res = result)
            _AdJointMul_Mul_Mul(dim, (double*)_mat1, (double*)_mat2, (double*)_mat3, (double*)res);
    }

    /// <summary>
    /// STEM用の特殊関数。透過係数を求める。
    /// </summary>
    /// <param name="dim"></param>
    /// <param name="thickness"></param>
    /// <param name="kg_z"></param>
    /// <param name="val"></param>
    /// <param name="vec"></param>
    /// <param name="result"></param>
    //unsafe static public void GenerateTC(in int dim,in double thickness, in double[] kg_z, in Complex[] val, in Complex[] vec, ref Complex[] result)
    //{
    //    fixed (double* _kg_z = kg_z)
    //    fixed (Complex* _val = val, _vec = vec, _result = result)
    //        _GenerateTC(dim, thickness, _kg_z, (double*)_val, (double*)_vec, (double*)_result);
    //}

    unsafe static public void GenerateTC1(in int dim, in double thickness, double* _kg_z, Complex* _val, Complex* _vec, Complex* _tc_k)
    {
        _GenerateTC1(dim, thickness, _kg_z, (double*)_val, (double*)_vec, (double*)_tc_k);
    }

    unsafe static public void GenerateTC2(in int dim, in double thickness, double* _kg_z, Complex* _val, Complex* _vec, Complex* _tc_k, Complex* _tc_kq)
    {
        _GenerateTC2(dim, thickness, _kg_z, (double*)_val, (double*)_vec, (double*)_tc_k, (double*)_tc_kq);
    }

    /// <summary>
    /// 横ベクトル×正方行列×縦ベクトルの掛算. STEMの非弾性散乱を求めるときに使用
    /// </summary>
    /// <param name="dim"></param>
    /// <param name="rowVec"></param>
    /// <param name="sqMtx"></param>
    /// <param name="colVec"></param>
    /// <returns></returns>
    unsafe static public Complex RowVec_SqMat_ColVec(in int dim, Complex[] rowVec, Complex[] sqMtx, Complex[] colVec)
    {
        var result = new Complex();
        fixed (Complex* _rowVec = rowVec, _sqMtx = sqMtx, _colVec = colVec)
            _RowVec_SqMat_ColVec(dim, (double*)_rowVec, (double*)_sqMtx, (double*)_colVec, (double*)&result);

        return result;
    }
    /// <summary>
    /// 横ベクトル×正方行列×縦ベクトルの掛算. STEMの非弾性散乱を求めるときに使用
    /// </summary>
    /// <param name="dim"></param>
    /// <param name="rowVec"></param>
    /// <param name="sqMtx"></param>
    /// <param name="colVec"></param>
    /// <returns></returns>
    unsafe static public Complex RowVec_SqMat_ColVec(in int dim, Complex* _rowVec, Complex* _sqMtx, Complex* _colVec)
    {
        var result = new Complex();
        _RowVec_SqMat_ColVec(dim, (double*)_rowVec, (double*)_sqMtx, (double*)_colVec, (double*)&result);
        return result;
    }

    unsafe static public void SqMat_ColVec(in int dim, Complex* _sqMtx, Complex* _colVec, Complex* result)
    {
        _SqMat_ColVec(dim, (double*)_sqMtx, (double*)_colVec, (double*)result);
    }

    unsafe static public Complex STEM_INEL1(in int dim, Complex* _rowVec, int[] n, double[] r, Complex* _sqMtx, Complex* _colVec)
    {
        var result = new Complex();
        fixed (int* _n = n)
        fixed (double* _r = r)
            _STEM_INEL1(dim, (double*)_rowVec, _n, _r, (double*)_sqMtx, (double*)_colVec, (double*)&result);
        return result;
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
    unsafe static public Complex[] CBEDSolver_Eigen(Complex[] potential, Complex[] psi0, double[] thickness)
    => CBEDSolver(potential, psi0, thickness, true);

    /// <summary>
    /// Eigenライブラリーを利用してMatrix exponentialを解いて、CBEDの解を求める. 
    /// </summary>
    /// <param name="potential"></param>
    /// <param name="psi0"></param>
    /// <param name="thickness"></param>
    /// <param name="coeff"></param>
    /// <returns></returns>
    unsafe static public Complex[] CBEDSolver_MatExp(Complex[] potential, Complex[] psi0, double[] thickness)
        => CBEDSolver(potential, psi0, thickness, false);

    unsafe static private Complex[] CBEDSolver(Complex[] potential, Complex[] psi0, double[] thickness, in bool eigen)
    {
        var dim = psi0.Length;
        var result = GC.AllocateUninitializedArray<Complex>(dim * thickness.Length);// new Complex[dim * thickness.Length];
        fixed (Complex* _potential = potential, _psi0 = psi0, _result = result)
        {
            if (eigen)
                _CBEDSolver_Eigen(dim, (double*)_potential, (double*)_psi0, thickness.Length, thickness, (double*)_result);
            else
            {
                var tStep = thickness.Length > 1 ? thickness[1] - thickness[0] : 0.0;
                _CBEDSolver_MtxExp(dim, (double*)_potential, (double*)_psi0, thickness.Length, thickness[0], tStep, (double*)_result);
            }
        }
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="potential"></param>
    /// <param name="psi0"></param>
    /// <param name="thickness"></param>
    /// <param name="coeff"></param>
    /// <param name="eigen"></param>
    /// <returns></returns>
    unsafe static public (Complex[] Values, Complex[] Vectors, Complex[] Alphas, Complex[] Tg)
        CBEDSolver2(Complex[] potential, Complex[] psi0, double[] thickness)
    {
        var dim = psi0.Length;
        var Values = GC.AllocateUninitializedArray<Complex>(dim);
        var Vectors = GC.AllocateUninitializedArray<Complex>(dim * dim);
        var Alphas = GC.AllocateUninitializedArray<Complex>(dim);
        var Tg = GC.AllocateUninitializedArray<Complex>(dim * thickness.Length);

        fixed (Complex* _potential = potential, _psi0 = psi0, _Tg = Tg, _Values = Values, _Vectors = Vectors, _Alphas = Alphas)
            _CBEDSolver_Eigen2(dim, (double*)_potential, (double*)_psi0, thickness.Length, thickness, (double*)_Values, (double*)_Vectors, (double*)_Alphas, (double*)_Tg);

        return (Values, Vectors, Alphas, Tg);
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
