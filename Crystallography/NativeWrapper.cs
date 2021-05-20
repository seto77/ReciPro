using MathNet.Numerics.LinearAlgebra.Complex;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Buffers;

namespace Crystallography
{
    public static class NativeWrapper
    {
        #region DllImport
        public enum Library { None, Eigen, Cuda }


        [DllImport("msvcrt.dll", EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        public static extern IntPtr Memcpy(IntPtr dest, IntPtr src, UIntPtr count);


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
            string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace(".dll", ".Native.dll");
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

        public  static bool Enabled { get; }

        #endregion

        #region 変換関数
        unsafe private static (int Dim, double[] Mat) toDoubleArray(DenseMatrix mat) => toDoubleArray(mat.ToArray());

        unsafe private static (int Dim, double[] Mat) toDoubleArray(Complex[,] mat)
        {
            if (mat == null)
                return (0, null);
            var dim = mat.GetLength(0);
            var dest = new double[dim * dim * 2];
            fixed (Complex* pSrc = mat)
            fixed (double* pDest = dest)
                Memcpy((IntPtr)pDest, (IntPtr)pSrc, (UIntPtr)(dim * dim * sizeof(Complex)));
            return (dim, dest);
        }

        unsafe private static (int Dim, double[] Mat) toDoubleArray(Complex[] vec)
        {
            var dim = vec.Length;
            var dest = new double[dim * 2];
            fixed (Complex* pSrc = vec)
            fixed (double* pDest = dest)
                Memcpy((IntPtr)pDest, (IntPtr)pSrc, (UIntPtr)(dim * sizeof(Complex)));
            return (dim, dest);
        }

        unsafe private static DenseMatrix toDenseMatrix(Span<double> src)
        {
            var dim = (int)Math.Sqrt(src.Length / 2);
            var dest = new Complex[dim, dim];
            fixed (double* pSrc = src)
            fixed (Complex* pDest = dest)
                Memcpy((IntPtr)pDest, (IntPtr)pSrc, (UIntPtr)(dim * dim * sizeof(Complex)));
            return DenseMatrix.OfArray(dest);
        }

        unsafe private static DenseVector toDenseVector(Span<double> src)
        {
            var dim = src.Length / 2;
            var dest = new Complex[dim];
            fixed (double* pSrc = src)
            fixed (Complex* pDest = dest)
                Memcpy((IntPtr)pDest, (IntPtr)pSrc, (UIntPtr)(dim * sizeof(Complex)));
            return DenseVector.OfArray(dest);
        }
        #endregion

        #region 逆行列
        /// <summary>
        /// Eigenライブラリーを利用して、非対称複素行列の逆行列を求める
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        static public DenseMatrix Inverse(DenseMatrix mat)
        {
            var (_dim, _mat) = toDoubleArray(mat);
            return Inverse(_dim, _mat);
        }

        /// <summary>
        /// Eigenライブラリーを利用して、非対称複素行列の逆行列を求める
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        static public DenseMatrix Inverse(Complex[,] mat)
        {
            var (dim, inputValues) = toDoubleArray(mat);
            return Inverse(dim, inputValues);
        }

        /// <summary>
        /// Eigenライブラリーを利用して、非対称複素行列の逆行列を求める
        /// </summary>
        /// <param name="dim"></param>
        /// <param name="_mat"></param>
        /// <returns></returns>
        static public DenseMatrix Inverse(int dim, double[] _mat)
        {
            var _inv = new double[dim * dim * 2];
            _Inverse(dim, _mat, _inv);
            return toDenseMatrix(_inv);
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
            //matをdouble[]に変換
            var (dim, inputValues) = toDoubleArray(mat);

            var values = new double[dim * 2];
            var vectors = new double[dim * dim * 2];

            _EigenSolver(dim, inputValues, values, vectors);
            var result = (toDenseVector(values), toDenseMatrix(vectors));

            return result; ;
        }

        #endregion 固有値

        #region 行列指数関数
        static public DenseMatrix MatrixExponential(DenseMatrix mat)
        {
            //matをdouble[]に変換
            var (dim, inputValues) = toDoubleArray(mat);

            var vectors = new double[dim * dim * 2];

            _MatrixExponential(dim, inputValues, vectors);
            var result = toDenseMatrix(vectors);

            return result; ;
        }

        static public DenseMatrix MatrixExponential_Cuda(DenseMatrix mat)
        {
            //matをdouble[]に変換
            var (dim, inputValues) = toDoubleArray(mat);

            var vectors = new double[dim * dim * 2];

            MatrixExponential_Cuda(dim, inputValues, vectors);
            var result = toDenseMatrix(vectors);

            return result; ;
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
        unsafe static public Complex[][] CBEDSolver_Eigen(Complex[,] potential, Complex[] psi0, double[] thickness, double coeff)
        => CBEDSolver(potential, psi0, thickness, coeff, true);

        /// <summary>
        /// Eigenライブラリーを利用してMatrix exponentialを解いて、CBEDの解を求める. 
        /// </summary>
        /// <param name="potential"></param>
        /// <param name="psi0"></param>
        /// <param name="thickness"></param>
        /// <param name="coeff"></param>
        /// <returns></returns>
        unsafe static public Complex[][] CBEDSolver_MatExp(Complex[,] potential, Complex[] psi0, double[] thickness, double coeff)
            => CBEDSolver(potential, psi0, thickness, coeff, false);

        unsafe static private Complex[][] CBEDSolver(Complex[,] potential, Complex[] psi0, double[] thickness, double coeff, bool eigen)
        {
            (_, double[] _potential) = toDoubleArray(potential);

            (int dim, double[] _psi0) = toDoubleArray(psi0);

            var tempResult = new double[dim * thickness.Length * 2];

            if (eigen)
                _CBEDSolver_Eigen(dim, _potential, _psi0, thickness.Length, thickness, coeff, tempResult);
            else
            {
                var tStep = thickness.Length > 1 ? thickness[1] - thickness[0] : 0.0;
                _CBEDSolver_MtxExp(dim, _potential, _psi0, thickness.Length, thickness[0], tStep, coeff, tempResult);
            }
            var result = new Complex[thickness.Length][];
            var size = sizeof(Complex);
            fixed (double* pin = tempResult)
            {
                var tempResultP = pin;
                for (int t = 0; t < thickness.Length; t++)
                {
                    result[t] = new Complex[dim];
                    for (int g = 0; g < dim; g++, tempResultP += 2)
                        result[t][g] = new Complex(*tempResultP, *(tempResultP + 1));
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
}