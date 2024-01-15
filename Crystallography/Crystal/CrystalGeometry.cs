using MathNet.Numerics.LinearAlgebra.Double;
using System;

namespace Crystallography;

public static class CrystalGeometry
{
    public static double[] GetErrorTriclinic(Plane[] plane)
    {
        double a, b, c, alpha, beta, gamma, v;
        double a_err, b_err, c_err, alpha_err, beta_err, gamma_err, v_err;
        a = b = c = alpha = beta = gamma = v = 0;
        a_err = b_err = c_err = alpha_err = beta_err = gamma_err = v_err = -1;

        var mQ = new DenseMatrix(plane.Length, 6);
        var mA = new DenseMatrix(plane.Length, 1);
        var mW = new DenseMatrix(plane.Length, plane.Length);
        for (int i = 0; i < plane.Length; i++)
        {
            mQ[i, 0] = plane[i].h * plane[i].h;
            mQ[i, 1] = plane[i].k * plane[i].k;
            mQ[i, 2] = plane[i].l * plane[i].l;
            mQ[i, 3] = plane[i].k * plane[i].l;
            mQ[i, 4] = plane[i].l * plane[i].h;
            mQ[i, 5] = plane[i].h * plane[i].k;
            mA[i, 0] = 1 / plane[i].dObs / plane[i].dObs;
            mW[i, i] = plane[i].Weight;
        }
        var mC = (mQ.Transpose() * mW * mQ).Inverse() * mQ.Transpose() * mW * mA;
        //if (mC.E.GetLength(0) == 0 || double.IsNaN(mC.E[0, 0]))
        //    return new double[0];

        double A = mC[0, 0], B = mC[1, 0], C = mC[2, 0], U = mC[3, 0], V = mC[4, 0], W = mC[5, 0];

        double vStar = Math.Sqrt(4 * A * B * C - A * U * U - B * V * V - C * W * W + U * V * W) / 2;
        v = 1 / vStar;

        double h = 4 * B * C - U * U, k = 4 * C * A - V * V, l = 4 * A * B - W * W;
        double p = V * W - 2 * A * U, q = W * U - 2 * B * V, r = U * V - 2 * C * W;

        double h2 = h * h, h4 = h2 * h2, k2 = k * k, k4 = k2 * k2, l2 = l * l, l4 = l2 * l2;
        double p2 = p * p, p4 = p2 * p2, q2 = q * q, q4 = q2 * q2, r2 = r * r, r4 = r2 * r2;

        a = Math.Sqrt(h) / vStar / 2;
        b = Math.Sqrt(k) / vStar / 2;
        c = Math.Sqrt(l) / vStar / 2;
        double cosAlpha = p / Math.Sqrt(k * l);
        double cosBeta = q / Math.Sqrt(l * h);
        double cosGamma = r / Math.Sqrt(h * k);
        alpha = Math.Acos(cosAlpha);
        beta = Math.Acos(cosBeta);
        gamma = Math.Acos(cosGamma);

        if (plane.Length - 6 > 0)//以下は誤差を計算する部分
        {
            var Dev = mA - mQ * mC;//偏差を縦成分にとったベクトル Devの成分の２乗和が ピーク数 - パラメータ数になるはず
            double sum = 0;
            for (int i = 0; i < plane.Length; i++)
                sum += Dev[i, 0] * Dev[i, 0] * plane[i].Weight;
            //本当の重み行列は
            for (int i = 0; i < plane.Length; i++)
                mW[i, i] = plane[i].Weight * (plane.Length - 6) / sum;
            var AlphaInverse = (mQ.Transpose() * mW * mQ).Inverse();//この行列の対角成分がパラメータCの誤差の２乗になる

            double Av = AlphaInverse[0, 0], Bv = AlphaInverse[1, 1], Cv = AlphaInverse[2, 2], Uv = AlphaInverse[3, 3], Vv = AlphaInverse[4, 4], Wv = AlphaInverse[5, 5];

            double ValAinv2 = (Av * h4 + Cv * q4 + Bv * r4 + q2 * r2 * Uv + h2 * q2 * Vv + h2 * r2 * Wv) / h4;
            double ValBinv2 = (Bv * k4 + Av * r4 + Cv * p4 + r2 * p2 * Vv + k2 * r2 * Wv + k2 * p2 * Uv) / k4;
            double ValCinv2 = (Cv * l4 + Bv * p4 + Av * q4 + p2 * q2 * Wv + l2 * p2 * Uv + l2 * q2 * Vv) / l4;
            a_err = Math.Sqrt(ValAinv2) * a * a * a / 2;
            b_err = Math.Sqrt(ValBinv2) * b * b * b / 2;
            c_err = Math.Sqrt(ValCinv2) * c * c * c / 2;

            double ValCosAlpha = (Av * (l * r * V + k * q * W) * (l * r * V + k * q * W) + 4 * A * A * ((p2 * Bv + q2 * Wv) / l2 + (r2 * Vv + p2 * Cv) / k2 + Uv) * l2 * k2) / (k * k * k * l * l * l);
            double ValCosBeta = (Bv * (h * p * W + l * r * U) * (h * p * W + l * r * U) + 4 * B * B * ((q2 * Cv + r2 * Uv) / h2 + (p2 * Wv + q2 * Av) / l2 + Vv) * h2 * l2) / (l * l * l * h * h * h);
            double ValCosGamma = (Cv * (k * q * U + h * p * V) * (k * q * U + h * p * V) + 4 * C * C * ((r2 * Av + p2 * Vv) / k2 + (q2 * Uv + r2 * Bv) / h2 + Wv) * k2 * h2) / (h * h * h * k * k * k);
            //y = acos(x) => y'=1/(1-x^2)
            alpha_err = Math.Sqrt(ValCosAlpha) / (1 - cosAlpha * cosAlpha);
            beta_err = Math.Sqrt(ValCosBeta) / (1 - cosBeta * cosBeta);
            gamma_err = Math.Sqrt(ValCosGamma) / (1 - cosGamma * cosGamma);

            double ValVstar2 = (Av * h2 + Bv * k2 + Cv * l2 + p2 * Uv + q2 * Vv + r2 * Wv) / 16;
            v_err = Math.Sqrt(ValVstar2) * v * v * v / 2;
        }

        return [a, b, c, alpha, beta, gamma, v, a_err, b_err, c_err, alpha_err, beta_err, gamma_err, v_err];
    }
}
