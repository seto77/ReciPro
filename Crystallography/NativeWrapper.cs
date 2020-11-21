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
        private static extern void _CBEDSolver(int gDim,
                                               double[] potential,
                                               double[] phi0,
                                               int tDim,
                                               double[] thickness,
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

        /// <summary>
        /// Native ライブラリが有効かどうか
        /// </summary>
        static public bool Enabled
        {
            get
            {
                string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace(".dll", ".Native.dll");
                if (!System.IO.File.Exists(appPath))
                    return false;
                else if (System.IO.File.GetCreationTime(appPath).Ticks < new DateTime(2019, 08, 06, 19, 45, 00).Ticks)
                    return false;

                var values = new double[4];
                try
                {
                    _EigenSolver(2, new[] { 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0 }, values, new double[8]);
                }
                catch
                {
                    return false;
                }
                return values.Sum() > 1;
            }
        }

        unsafe private static (int Dim, double[] Mat) toDoubleArray(DenseMatrix mat)
        {
            var dim = mat.Row(0).Count;
            var matArray = new double[dim * dim * 2];
            fixed (double* pin = matArray)
            {
                var matArrayP = pin;
                for (int c = 0; c < dim; c++)
                    for (int r = 0; r < dim; r++)
                    {
                        *matArrayP++ = mat[c, r].Real;
                        *matArrayP++ = mat[c, r].Imaginary;
                    }

                return (dim, matArray);
            }
        }


        unsafe private static (int Dim, double[] Mat) toDoubleArray(Complex[,] mat)
        {
            var dim = mat.GetLength(0);
            var matArray = new double[dim * dim * 2];
            fixed (double* pin = matArray)
            {
                var matArrayP = pin;
                for (int c = 0; c < dim; c++)
                    for (int r = 0; r < dim; r++)
                    {
                        *matArrayP++ = mat[c, r].Real;
                        *matArrayP++ = mat[c, r].Imaginary;
                    }

                return (dim, matArray);
            }
        }

        unsafe private static (int Dim, double[] Mat) toDoubleArray(Complex[] vec)
        {
            var dim = vec.Length;
            var vecArray = new double[dim * 2];
            fixed (double* pin = vecArray)
            {
                var vecArrayP = pin;
                for (int c = 0; c < dim; c++)
                {
                    *vecArrayP++ = vec[c].Real;
                    *vecArrayP++ = vec[c].Imaginary;
                }
                return (dim, vecArray);
            }
        }

        unsafe private static DenseMatrix toDenseMatrix(Span<double> mat)
        {
            fixed (double* pin = mat)
            {
                var matP = pin;
                var dim = (int)Math.Sqrt(mat.Length / 2);
                var complex = new DenseMatrix(dim, dim);
                for (int c = 0; c < dim; c++)
                    for (int r = 0; r < dim; r++, matP += 2)
                        complex[c, r] = new Complex(*matP, *(matP+1));
                return complex;
            }
        }

        unsafe private static DenseVector toDenseVector(Span<double> vec)
        {
            fixed (double* pin = vec)
            {
                var vecP = pin;
                var dim = vec.Length / 2;
                var complex = new DenseVector(dim);
                for (int c = 0; c < dim; c++, vecP += 2)
                    complex[c] = new Complex(*vecP, *(vecP + 1));
                return complex;
            }
        }


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
            var _inv = ArrayPool<double>.Shared.Rent(dim * dim * 2);
            _Inverse(dim, _mat, _inv);
            var result = toDenseMatrix(_inv);
            ArrayPool<double>.Shared.Return(_inv);
            return result;
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

            var values = ArrayPool<double>.Shared.Rent(dim * 2);
            var vectors = ArrayPool<double>.Shared.Rent(dim * dim * 2);
            
            _EigenSolver(dim, inputValues, values, vectors);
            var result = (toDenseVector(values), toDenseMatrix(vectors));

            ArrayPool<double>.Shared.Return(values);
            ArrayPool<double>.Shared.Return(vectors);

            return result; ;
        }

        #endregion 固有値

        /// <summary>
        /// Eigenライブラリーを利用して、CBEDの解を求める
        /// </summary>
        /// <param name="potential"></param>
        /// <param name="psi0"></param>
        /// <param name="thickness"></param>
        /// <param name="coeff"></param>
        /// <returns></returns>
        unsafe static public Complex[][] CBEDSolver(Complex[,] potential, Complex[] psi0, double[] thickness, double coeff)
        {
            (int dim, double[] _potential) = toDoubleArray(potential);

            (_, double[] _psi0) = toDoubleArray(psi0);

            var tempResult = ArrayPool<double>.Shared.Rent(dim * thickness.Length * 2);
            _CBEDSolver(dim, _potential, _psi0, thickness.Length, thickness, coeff, tempResult);

            var result = new Complex[thickness.Length][];

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

            ArrayPool<double>.Shared.Return(tempResult);
          
            return result;
        }


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
    }
}