using MathNet.Numerics.LinearAlgebra.Complex;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Collections;

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

        private static (int Dim, double[] Mat) toDoubleArray(Complex[,] mat)
        {
            var dim = mat.GetLength(0);
            var matArray = new double[dim * dim * 2];
            for (int c = 0, count = 0; c < dim; c++)
                for (int r = 0; r < dim; r++)
                {
                    matArray[count++] = mat[c, r].Real;
                    matArray[count++] = mat[c, r].Imaginary;
                }

            return (dim, matArray);
        }

        private static (int Dim, double[] Mat) toDoubleArray(Complex[] vec)
        {
            var dim = vec.Length;
            var vecArray = new double[dim * 2];
            for (int c = 0, count = 0; c < dim; c++)
            {
                vecArray[count++] = vec[c].Real;
                vecArray[count++] = vec[c].Imaginary;
            }
            return (dim, vecArray);
        }

        private static DenseMatrix toDenseMatrix(double[] mat)
        {
            var dim = (int)Math.Sqrt(mat.Length / 2);
            var complex = new DenseMatrix(dim, dim);
            for (int c = 0, count = 0; c < dim; c++)
                for (int r = 0; r < dim; r++, count += 2)
                    complex[c, r] = new Complex(mat[count], mat[count + 1]);
            return complex;
        }

        private static DenseVector toDenseVector(double[] vec)
        {
            var dim = vec.Length / 2;
            var complex = new DenseVector(dim);
            for (int c = 0, count = 0; c < dim; c++, count += 2)
                complex[c] = new Complex(vec[count], vec[count + 1]);
            return complex;
        }


        #region 逆行列
        /// <summary>
        /// Eigenライブラリーを利用して、非対称複素行列の逆行列を求める
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        static public DenseMatrix Inverse(DenseMatrix mat) => Inverse(mat.ToArray());

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

            return (toDenseVector(values), toDenseMatrix(vectors));
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
        static public Complex[][] CBEDSolver(Complex[,] potential, Complex[] psi0, double[] thickness, double coeff)
        {
            (int dim, double[] _potential) = toDoubleArray(potential);

            (_, double[] _psi0) = toDoubleArray(psi0);

            var tempResult = new double[dim * thickness.Length * 2];
            _CBEDSolver(dim, _potential, _psi0, thickness.Length, thickness, coeff, tempResult);

            int n = 0;
            var result = new Complex[thickness.Length][];
            for (int t = 0; t < thickness.Length; t++)
            {
                result[t] = new Complex[dim];
                for (int g = 0; g < dim; g++)
                {
                    result[t][g] = new Complex(tempResult[n], tempResult[n + 1]);
                    n += 2;
                }
            }
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