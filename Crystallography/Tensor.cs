namespace Crystallography
{
    public class Tensor3D
    {
        public double E11, E12, E13, E21, E22, E23, E31, E32, E33;

        public Tensor3D(Vector3D v1, Vector3D v2, Vector3D v3)
        {
            E11 = v1.X;
            E21 = v1.Y;
            E31 = v1.Z;

            E12 = v2.X;
            E22 = v2.Y;
            E32 = v2.Z;

            E13 = v3.X;
            E23 = v3.Y;
            E33 = v3.Z;
        }

        public Tensor3D(double e11, double e21, double e31, double e12, double e22, double e32, double e13, double e23, double e33)
        {
            E11 = e11;
            E21 = e21;
            E31 = e31;

            E12 = e12;
            E22 = e22;
            E32 = e32;

            E13 = e13;
            E23 = e23;
            E33 = e33;
        }

        /// <summary>
        /// オイラー回転によって変換されるテンソル量を返す
        /// </summary>
        /// <param name="tensor"></param>
        /// <param name="phi"></param>
        /// <param name="rho"></param>
        /// <param name="zi"></param>
        /// <returns></returns>
        public static Matrix3D CoordinateTransformation(Matrix3D tensor, Matrix3D rot)
        {
            //a[i,j]は変換後の座標系のi番目の軸と、変換前のj番目の軸との内積
            double[][] a = new double[][]{
                new double []{ rot.E11,rot.E12,rot.E13},
                new double []{ rot.E21,rot.E22,rot.E23},
                new double []{ rot.E31,rot.E32,rot.E33}
            };

            double[][] t = new double[][]{
                new double []{ tensor.E11,tensor.E12,tensor.E13},
                new double []{ tensor.E21,tensor.E22,tensor.E23},
                new double []{ tensor.E31,tensor.E32,tensor.E33}
            };

            double[][] sigma = new double[][] { new double[] { 0, 0, 0, }, new double[] { 0, 0, 0, }, new double[] { 0, 0, 0, } };

            for (int i = 0; i < 3; i++)
                for (int j = i; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                        for (int l = 0; l < 3; l++)
                            sigma[i][j] += a[i][k] * a[j][l] * t[k][l];
                }

            return new Matrix3D(sigma[0][0], sigma[0][1], sigma[0][2], sigma[0][1], sigma[1][1], sigma[1][2], sigma[0][2], sigma[1][2], sigma[2][2]);
        }

        /*public static Tensor3D operator *(Matrix3D m, Tensor3D t)
        {
            double e11 =
               m.E11 * (m.E11 * t.E11 + m.E12 * t.E21 + m.E13 * t.E31) +
               m.E12 * (m.E11 * t.E12 + m.E12 * t.E22 + m.E13 * t.E32) +
               m.E13 * (m.E11 * t.E13 + m.E12 * t.E23 + m.E13 * t.E33);
            double e21 =
               m.E11 * (m.E21 * t.E11 + m.E22 * t.E21 + m.E23 * t.E31) +
               m.E12 * (m.E21 * t.E12 + m.E22 * t.E22 + m.E23 * t.E32) +
               m.E13 * (m.E21 * t.E13 + m.E22 * t.E23 + m.E23 * t.E33);

            //a[i,j]は変換後の座標系のi番目の軸と、変換前のj番目の軸との内積
            double[][] a = new double[][]{
                new double []{ m.E11,m.E12,m.E13},
                new double []{ m.E21,m.E22,m.E23},
                new double []{ m.E31,m.E32,m.E33}
            };

            double[][] t = new double[][]{
                new double []{ tensor.E11,tensor.E12,tensor.E13},
                new double []{ tensor.E21,tensor.E22,tensor.E23},
                new double []{ tensor.E31,tensor.E32,tensor.E33}
            };

            double[][] sigma = new double[][] { new double[] { 0, 0, 0, }, new double[] { 0, 0, 0, }, new double[] { 0, 0, 0, } };

            for (int i = 0; i < 3; i++)
                for (int j = i; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                        for (int l = 0; l < 3; l++)
                            sigma[i][j] += a[i][k] * a[j][l] * t[k][l];
                }

            return new Matrix3D(sigma[0][0], sigma[0][1], sigma[0][2], sigma[0][1], sigma[1][1], sigma[1][2], sigma[0][2], sigma[1][2], sigma[2][2]);
         } */
    }
}