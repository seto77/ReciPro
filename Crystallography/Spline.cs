using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;

namespace Crystallography
{
    public class Spline2nd
    {
        public List<double> A, B;
        public List<PointD> Pt;

        public Spline2nd(List<PointD> pt)
        {
            //���ёւ�
            Pt = sort(pt);

            //�[�_�̏���
            Pt.Insert(0, EdgePoint(Pt[0], Pt[1], Pt[2]));
            Pt.Add(EdgePoint(Pt[^1], Pt[^2], Pt[^3]));

            //A,B�̌v�Z
            A = new List<double>(); B = new List<double>();
            for (int i = 1; i < Pt.Count - 2; i++)
            {
                double dX = Pt[i + 1].X - Pt[i - 1].X;
                double dY = Pt[i + 1].Y - Pt[i - 1].Y;

                double P = (Pt[i + 2].X - Pt[i].X) * (Pt[i + 1].Y - Pt[i - 1].Y);
                double Q = (Pt[i + 1].X - Pt[i - 1].X) * (Pt[i + 2].Y - Pt[i].Y);
                double R = (Pt[i + 1].X - Pt[i - 1].X) * (Pt[i + 2].X - Pt[i].X);
                double S = (Pt[i + 1].Y - Pt[i - 1].Y) * (Pt[i + 2].Y - Pt[i].Y);
                A.Add((P * Pt[i].X - Q * Pt[i + 1].X - R * (Pt[i].Y - Pt[i + 1].Y)) / (P - Q));
                B.Add((P * Pt[i + 1].Y - Q * Pt[i].Y + S * (Pt[i].X - Pt[i + 1].X)) / (P - Q));
            }
            Pt.RemoveAt(0);
            Pt.RemoveAt(Pt.Count - 1);
        }

        public PointD GetValue(double t)
        {
            int n = (int)t;
            if (t < 0) n = 0;
            if (n > Pt.Count - 2) n = Pt.Count - 2;
            t = t - n;

            return new PointD(
                 (1 - t) * (1 - t) * Pt[n].X + 2 * (1 - t) * t * A[n] + t * t * Pt[n + 1].X,
                 (1 - t) * (1 - t) * Pt[n].Y + 2 * (1 - t) * t * B[n] + t * t * Pt[n + 1].Y
                 );
        }

        /// <summary>
        /// �����̋߂����̂��Ȃ��Ă����ĕ��ёւ�
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public List<PointD> sort(List<PointD> pt)
        {
            //���ёւ�
            List<PointD> tempPt = new([.. pt]);
            List<PointD> destPt = [];

            double minDistance = double.MaxValue;
            int lower = 0, upper = 0;
            for (int i = 0; i < pt.Count - 1; i++)
                for (int j = i + 1; j < pt.Count; j++)
                    if ((tempPt[i] - tempPt[j]).Length < minDistance)
                    {
                        minDistance = (tempPt[i] - tempPt[j]).Length;
                        lower = i;
                        upper = j;
                    }
            destPt.Add(tempPt[lower]); destPt.Add(tempPt[upper]);
            tempPt.Remove(destPt[0]); tempPt.Remove(destPt[1]);

            while (tempPt.Count > 0)
            {
                double minDistanceUpper = double.MaxValue;
                double minDistanceLower = double.MaxValue;
                for (int i = 0; i < tempPt.Count; i++)
                {
                    if (minDistanceLower > (destPt[0] - tempPt[i]).Length)
                    {
                        minDistanceLower = (destPt[0] - tempPt[i]).Length;
                        lower = i;
                    }
                    if (minDistanceUpper > (destPt[^1] - tempPt[i]).Length)
                    {
                        minDistanceUpper = (destPt[^1] - tempPt[i]).Length;
                        upper = i;
                    }
                }
                if (minDistanceLower < minDistanceUpper)
                {
                    destPt.Insert(0, tempPt[lower]);
                    tempPt.RemoveAt(lower);
                }
                else
                {
                    destPt.Add(tempPt[upper]);
                    tempPt.RemoveAt(upper);
                }
            }
            return destPt;
        }

        /// <summary>
        /// �Ō�̎O�_�ipt1, pt2, pt3)����[�_(pt0)���v�Z����
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <param name="pt3"></param>
        /// <returns></returns>
        public static PointD EdgePoint(PointD pt1, PointD pt2, PointD pt3)
        {
            double X = pt1.X - pt2.X;
            double Y = pt1.Y - pt2.Y;
            double coeff = (X * (pt1.X + pt2.X - 2 * pt3.X) + Y * (pt1.Y + pt2.Y - 2 * pt3.Y)) / (X * X + Y * Y);
            return new PointD(pt3.X + X * coeff, pt3.Y + Y * coeff);
        }
    }

    /// <summary>
    /// Spline�֐����������߂̐ÓI���\�b�h��񋟂��܂��B
    /// </summary>
    public class Spline
    {
        public Spline()
        {
            //
            // TODO: �R���X�g���N�^ ���W�b�N�������ɒǉ����Ă��������B
            //
        }

        public Spline(double[] p, double[] c)
        {
            this.p = p;
            this.c = c;
        }

        public Spline(double[] p, double[] c, double T)
        {
            this.p = p;
            this.c = c;
            this.T = T;
        }

        public double[] p;
        public double[] c;
        public double T = 0;

        public double GetValue(double x)
        {
            if (c == null || c.Length < 8) return 0;

            if (x <= p[0])
                return c[0] * x * x * x + c[1] * x * x + c[2] * x + c[3];
            else if (x >= p[^1])
                return c[^4] * x * x * x + c[^3] * x * x + c[^2] * x + c[^1];
            else
                for (int j = 0; j < p.Length - 1; j++)
                    if (x >= p[j] && x <= p[j + 1])
                        return c[j * 4 + 4] * x * x * x + c[j * 4 + 5] * x * x + c[j * 4 + 6] * x + c[j * 4 + 7];
            return 0;
        }

        //����_��n���ƃv���t�@�C����Ԃ�
        public static Profile GetSpline(PointD[] BgControlPoint, Profile destProfile)
        {
            double[] x = new double[destProfile.Pt.Count];
            for (int i = 0; i < x.Length; i++)
                x[i] = destProfile.Pt[i].X;

            if (BgControlPoint == null || BgControlPoint.Length < 3)
            {
                Profile p = new();
                for (int i = 0; i < destProfile.Pt.Count; i++)
                    p.Pt.Add(new PointD(x[i], 0));
                return p;
            }

            double[,] m = new double[BgControlPoint.Length * 4 + 4, BgControlPoint.Length * 4 + 4];
            PointD[] pt = new PointD[BgControlPoint.Length];
            for (int i = 0; i < pt.Length; i++)
                pt[i] = BgControlPoint[i];
            Array.Sort(pt);
            int length = pt.Length;
            for (int i = 0; i < pt.Length * 4; i++)
                for (int j = 0; j < pt.Length * 4; j++)
                    m[i, j] = 0;

            double X, X2, X3;
            for (int i = 0; i < length; i++)
            {
                X = pt[i].X;
                X2 = X * X;
                X3 = X * X * X;

                //����_��ʂ�Ƃ����������݂����s�񕔕�
                m[i * 2, i * 4 + 0] = m[i * 2 + 1, i * 4 + 4] = X3;
                m[i * 2, i * 4 + 1] = m[i * 2 + 1, i * 4 + 5] = X2;
                m[i * 2, i * 4 + 2] = m[i * 2 + 1, i * 4 + 6] = X;
                m[i * 2, i * 4 + 3] = m[i * 2 + 1, i * 4 + 7] = 1;

                //1�K��������v����Ƃ�����������
                m[i + 2 * length, i * 4 + 0] = 3 * X2;
                m[i + 2 * length, i * 4 + 1] = 2 * X;
                m[i + 2 * length, i * 4 + 2] = 1;
                m[i + 2 * length, i * 4 + 4] = -3 * X2;
                m[i + 2 * length, i * 4 + 5] = -2 * X;
                m[i + 2 * length, i * 4 + 6] = -1;

                //2�K��������v����Ƃ�����������
                m[i + 3 * length, i * 4 + 0] = 6 * X;
                m[i + 3 * length, i * 4 + 1] = 2;
                m[i + 3 * length, i * 4 + 4] = -6 * X;
                m[i + 3 * length, i * 4 + 5] = -2;
            }

            //�[�_�̏���
            m[4 * length + 0, 0] = 1;
            m[4 * length + 1, 1] = 1;
            m[4 * length + 2, 4 * length + 0] = 1;
            m[4 * length + 3, 4 * length + 1] = 1;

            if (!DenseMatrix.OfArray(m).TryInverse(out Matrix<double> mInverse))//�t�s����Ƃ߂�
                return new Profile();

            double[] c = new double[length * 4 + 4];//�W���̔z��
            for (int i = 0; i < c.Length; i++)
                c[i] = 0;

            for (int i = 0; i < c.Length; i++)							//�W�����v�Z
                for (int j = 0; j < 2 * length; j++)
                {
                    c[i] += mInverse[i, j] * pt[j / 2].Y;
                }

            Profile pr = new();
            double y = 0;
            for (int i = 0; i < x.Length; i++)
            {
                X = x[i];
                X2 = X * X;
                X3 = X * X * X;

                if (X <= pt[0].X)
                    y = c[0] * X3 + c[1] * X2 + c[2] * X + c[3];
                else if (X >= pt[length - 1].X)
                    y = c[^4] * X3 + c[^3] * X2 + c[^2] * X + c[^1];
                else
                    for (int j = 0; j < length - 1; j++)
                        if (x[i] >= pt[j].X && x[i] <= pt[j + 1].X)
                        {
                            y = c[j * 4 + 4] * X3 + c[j * 4 + 5] * X2 + c[j * 4 + 6] * X + c[j * 4 + 7];
                            break;
                        }
                pr.Pt.Add(new PointD(x[i], y));
            }
            return pr;
        }

        //����_��n����Spline�N���X�̒萔��Ԃ�
        public static Spline GetSpline(Profile BgControlPoint)
        {
            double[,] m = new double[BgControlPoint.Pt.Count * 4 + 4, BgControlPoint.Pt.Count * 4 + 4];
            PointD[] pt = new PointD[BgControlPoint.Pt.Count];
            for (int i = 0; i < pt.Length; i++)
                pt[i] = BgControlPoint.Pt[i];
            Array.Sort(pt);
            int length = pt.Length;
            for (int i = 0; i < pt.Length * 4; i++)
                for (int j = 0; j < pt.Length * 4; j++)
                    m[i, j] = 0;

            double X, X2, X3;
            for (int i = 0; i < length; i++)
            {
                X = pt[i].X;
                X2 = X * X;
                X3 = X * X * X;

                //����_��ʂ�Ƃ����������݂����s�񕔕�
                m[i * 2, i * 4 + 0] = m[i * 2 + 1, i * 4 + 4] = X3;
                m[i * 2, i * 4 + 1] = m[i * 2 + 1, i * 4 + 5] = X2;
                m[i * 2, i * 4 + 2] = m[i * 2 + 1, i * 4 + 6] = X;
                m[i * 2, i * 4 + 3] = m[i * 2 + 1, i * 4 + 7] = 1;

                //1�K��������v����Ƃ�����������
                m[i + 2 * length, i * 4 + 0] = 3 * X2;
                m[i + 2 * length, i * 4 + 1] = 2 * X;
                m[i + 2 * length, i * 4 + 2] = 1;
                m[i + 2 * length, i * 4 + 4] = -3 * X2;
                m[i + 2 * length, i * 4 + 5] = -2 * X;
                m[i + 2 * length, i * 4 + 6] = -1;

                //2�K��������v����Ƃ�����������
                m[i + 3 * length, i * 4 + 0] = 6 * X;
                m[i + 3 * length, i * 4 + 1] = 2;
                m[i + 3 * length, i * 4 + 4] = -6 * X;
                m[i + 3 * length, i * 4 + 5] = -2;
            }

            //�[�_�̏���
            m[4 * length + 0, 0] = 1;
            m[4 * length + 1, 1] = 1;
            m[4 * length + 2, 4 * length + 0] = 1;
            m[4 * length + 3, 4 * length + 1] = 1;

            if (!DenseMatrix.OfArray(m).TryInverse(out Matrix mInverse))//�t�s����Ƃ߂�
                return new Spline(null, null);

            var c = new double[length * 4 + 4];//�W���̔z��
            for (int i = 0; i < c.Length; i++)
            {
                c[i] = 0;
                //�W�����v�Z
                for (int j = 0; j < 2 * length; j++)
                    c[i] += mInverse[i, j] * pt[j / 2].Y;
            }

            var p = new double[pt.Length];
            for (int i = 0; i < pt.Length; i++)
                p[i] = pt[i].X;

            return new Spline(p, c);
        }
    }
}