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
        public enum Library { None, Eigen, Cuda}

        [DllImport("msvcrt.dll", EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        public static extern IntPtr Memcpy(IntPtr dest, IntPtr src, UIntPtr count);

        [DllImport("Crystallography.Native.dll")]
        private unsafe static extern Complex* _Inverse(int dim, Complex* mat);

        [DllImport("Crystallography.Native.dll")]
        private unsafe static extern Complex** _EigenSolver(int dim, Complex* mat);

        [DllImport("Crystallography.Native.dll")]
        private unsafe static extern Complex* _MatrixExponential(int dim, Complex* mat);


        [DllImport("Crystallography.Native.dll")]
        private unsafe static extern Complex* _CBEDSolver_Eigen(int gDim,
                                               Complex* potential,
                                               Complex* phi0,
                                               int tDim,
                                               double[] thickness,
                                               double coeff);

        [DllImport("Crystallography.Native.dll")]
        private unsafe static extern Complex* _CBEDSolver_MtxExp(int gDim,
                                              Complex* potential,
                                              Complex* phi0,
                                              int tDim,
                                              double tStart,
                                              double tStep,
                                              double coeff);//,
                                              //double[] result);

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
        
        static unsafe public bool Enabled
        {
            get
            {
                return enabled();
                //
                //    string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace(".dll", ".Native.dll");
                //    if (!System.IO.File.Exists(appPath))
                //        return false;
                //    else if (System.IO.File.GetCreationTime(appPath).Ticks < new DateTime(2019, 08, 06, 19, 45, 00).Ticks)
                //        return false;
                //    try
                //    {
                //        var result = Inverse(new[,] { { new Complex(1, 0), new Complex(0, 0) }, { new Complex(0, 0), new Complex(1, 0) } });
                //        return result[0, 0].Real + result[1, 1].Real > 1;
                //    }
                //    catch
                //    {
                //        return false;
                //    }
            }
        }
        #endregion

        #region ComplexポインタをDenseMatrixに変換
        unsafe private static DenseMatrix toDenseMatrix(int rows,int cols, Complex* mat)
        {
            var result = new Complex[rows, cols];
            fixed (Complex* p = result)
                Memcpy((IntPtr)p, (IntPtr)mat, (UIntPtr)(rows* cols * sizeof(Complex)));

            return DenseMatrix.OfArray(result);
        }
        #endregion

        #region 逆行列
        /// <summary>
        /// Eigenライブラリーを利用して、非対称複素行列の逆行列を求める
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        static unsafe public DenseMatrix Inverse(DenseMatrix mat)
        {
            return Inverse(mat.ToArray());
        }

        /// <summary>
        /// Eigenライブラリーを利用して、非対称複素行列の逆行列を求める
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        static unsafe public DenseMatrix Inverse(Complex[,] mat)
        {
            fixed (Complex* pMat = mat)
            {
                var dim = mat.GetLength(0);
                var _inv = _Inverse(dim, pMat);// Inverse(dim, inputValues);
                return toDenseMatrix(dim,dim, _inv);
            }
        }

        #endregion 逆行列

        #region 固有値
        /// <summary>
        /// Eigenライブラリーを利用して、非対称複素行列の固有値、固有ベクトルを求める
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        static unsafe public (DenseVector eigenvalues, DenseMatrix eigenvectors) EigenSolver(Complex[,] mat)
        {
            var dim = mat.GetLength(0);
            fixed (Complex* pMat = mat)
            {
                var result = _EigenSolver(dim, pMat);

                var values = new Complex[dim];
                var vectors = new Complex[dim, dim];
                fixed (Complex* pVal= values, pVec = vectors) {
                    Memcpy((IntPtr)pVal, (IntPtr)result[0], (UIntPtr)(dim * sizeof(Complex)));
                    Memcpy((IntPtr)pVec, (IntPtr)result[1], (UIntPtr)(dim * dim * sizeof(Complex)));
                }

                return (DenseVector.OfArray(values), DenseMatrix.OfArray(vectors));
            }
        }
        #endregion 固有値

        #region 行列指数関数
        static unsafe public DenseMatrix MatrixExponential(DenseMatrix mat)
        {
            fixed (Complex* pMat = mat.ToArray())
            {
                var dim = mat.RowCount;
                var vectors = _MatrixExponential(dim, pMat);
                return toDenseMatrix(dim, dim, vectors);
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
        unsafe static public Complex[][] CBEDSolver_Eigen(Complex[,] potential, Complex[] psi0, double[] thickness, double coeff)
        {
            Complex* tempResult;
            int dim = psi0.Length;
            fixed (Complex* pPotential = potential, pPsi0 = psi0)
                tempResult = _CBEDSolver_Eigen(dim, pPotential, pPsi0, thickness.Length, thickness, coeff);

            var result = new Complex[thickness.Length][];
            for (int t = 0; t < thickness.Length; t++, tempResult += dim)
            {
                result[t] = new Complex[dim];
                fixed (Complex* pin = result[t])
                    Memcpy((IntPtr)pin, (IntPtr)tempResult, (UIntPtr)(dim * sizeof(Complex)));
            }
            return result;
        }

        /// <summary>
        /// Eigenライブラリーを利用してMatrix exponentialを解いて、CBEDの解を求める. 
        /// </summary>
        /// <param name="potential"></param>
        /// <param name="psi0"></param>
        /// <param name="thickness"></param>
        /// <param name="coeff"></param>
        /// <returns></returns>
        unsafe static public Complex[][] CBEDSolver_MatExp(Complex[,] potential, Complex[] psi0, double[] thickness, double coeff)
        {
            int dim = psi0.Length;
            Complex* tempResult;
            var tStep = thickness.Length > 1 ? thickness[1] - thickness[0] : 0.0;
            fixed (Complex* pPotential = potential, pPsi0 = psi0)
                tempResult = _CBEDSolver_MtxExp(dim, pPotential, pPsi0, thickness.Length, thickness[0], tStep, coeff);

            var result = new Complex[thickness.Length][];
            for (int t = 0; t < thickness.Length; t++, tempResult += dim)
            {
                result[t] = new Complex[dim];
                fixed (Complex* pin = result[t])
                    Memcpy((IntPtr)pin, (IntPtr)tempResult, (UIntPtr)(dim * sizeof(Complex)));
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
                r2,r2.Length,
                profile, pixels);

            return (profile, pixels);
        }
        #endregion
    }
}