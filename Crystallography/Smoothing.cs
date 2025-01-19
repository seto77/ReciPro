using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;

namespace Crystallography;

public class Smoothing
{
    public static Profile SavitzkyGolay(Profile profile, int pointNum, int order)
    {
        if (pointNum < 2) return Deep.Copy<Profile>(profile);
        if (pointNum < order + 1) order = pointNum - 1;
        if (order <= 0) return Deep.Copy<Profile>(profile);
        if (profile.Pt.Count < 3) return Deep.Copy<Profile>(profile);

        if (Math.Abs((profile.Pt[^1].X - profile.Pt[^2].X) / (profile.Pt[1].X - profile.Pt[0].X) - 1) < 0.000000001)
            return new Profile(SavitzkyGolaySimple([.. profile.Pt], pointNum, order));
        else
            return new Profile(SavitzkyGolayAccuracy([.. profile.Pt], pointNum, order));
    }

    /// <summary>
    /// �T�r�c�L�[�S���C�̃X���[�W���O
    /// </summary>
    /// <param name="profile">�t�B�b�e�B���O����v���t�@�C��</param>
    /// <param name="pointNum">�|�C���g��</param>
    /// <param name="order">����</param>
    /// <returns></returns>
    public static PointD[] SavitzkyGolaySimple(PointD[] pt, int pointNum, int order)
    {
        PointD[] smoothPt = new PointD[pt.Length];
        for (int i = 0; i < pt.Length; i++)
            smoothPt[i] = new PointD(pt[i].X, pt[i].Y);

        var m1 = new DenseMatrix(pointNum, order + 1);
        for (int j = 0; j < pointNum; j++)
            for (int i = 0; i < order + 1; i++)
                m1[j, i] = Math.Pow(j - pointNum / 2, i);

        if (!m1.TransposeThisAndMultiply(m1).TryInverse(out Matrix inv))
            return pt;

        var m2 = inv * m1.Transpose();

        double[] a = new double[pointNum];
        for (int i = 0; i < pointNum; i++)
            a[i] = m2[0, i];

        PointD[] tempPt = new PointD[pointNum];
        for (int n = pointNum / 2; n < pt.Length - pointNum / 2; n++)
        {
            Array.Copy(pt, n - pointNum / 2, tempPt, 0, pointNum);
            double y = 0;
            for (int j = 0; j < pointNum; j++)
                y += a[j] * tempPt[j].Y;
            smoothPt[n] = new PointD(smoothPt[n].X, y);
        }
        return smoothPt;
    }

    /// <summary>
    /// �T�r�c�L�[�S���C�̃X���[�W���O
    /// </summary>
    /// <param name="profile">�t�B�b�e�B���O����v���t�@�C��</param>
    /// <param name="pointNum">�|�C���g��</param>
    /// <param name="order">����</param>
    /// <returns></returns>
    public static PointD[] SavitzkyGolayAccuracy(PointD[] pt, int pointNum, int order)
    {
        PointD[] smoothPt = new PointD[pt.Length];
        for (int i = 0; i < pt.Length; i++)
            smoothPt[i] = new PointD(pt[i].X, pt[i].Y);

        for (int n = 0; n < pt.Length; n++)
        {
            var tempPt = new List<PointD>();
            //�܂��A���̓_����O���PointNum�����߂��_��T��
            for (int j = Math.Max(n - pointNum, 0); j < Math.Min(n + pointNum + 1, pt.Length); j++)
                tempPt.Add(pt[j]);
            while (tempPt.Count > pointNum)
                tempPt.RemoveAt(Math.Abs(tempPt[0].X - pt[n].X) > Math.Abs(tempPt[^1].X - pt[n].X) ? 0 : tempPt.Count - 1);

            //�v�Z���x�̂��߁Ax�͈̔͂�1����+2�ɕϊ����� ���� X = c1 x + c2;
            double c1 = 1.0 / (tempPt[^1].X - tempPt[0].X);
            double c2 = 1 - tempPt[0].X * c1;

            var m = new DenseMatrix(pointNum, order + 1);
            var y = new DenseMatrix(pointNum, 1);
            for (int j = 0; j < pointNum; j++)
            {
                y[j, 0] = tempPt[j].Y;
                for (int i = 0; i < order + 1; i++)
                    m[j, i] = Math.Pow(c1 * tempPt[j].X + c2, i);
            }
            if (!(m.TransposeThisAndMultiply(m)).TryInverse(out Matrix inv))
            {
                var a = inv * m.TransposeThisAndMultiply(y);

                smoothPt[n] = new PointD(smoothPt[n].X, 0);
                for (int j = 0; j < order + 1; j++)
                    smoothPt[n] += new PointD(0, a[j, 0] * Math.Pow(c1 * pt[n].X + c2, j));
            }
        }
        return smoothPt;
    }
}