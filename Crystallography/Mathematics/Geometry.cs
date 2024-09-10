using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using V3d = OpenTK.Vector3d;

namespace Crystallography;

public static class Geometry
{
    static Geometry()
    {
        MathNet.Numerics.Control.TryUseNativeMKL();
    }

    /// <summary>
    /// �ȉ~���Ƃ���5�_�ȏ�̓_pt[]���^����ꂽ�Ƃ��ŏ�2��@���璆�S�ʒu��Ԃ��֐�
    /// </summary>
    /// <param name="pt"></param>
    /// <param name="focus1"></param>
    /// <param name="focus2"></param>
    public static PointD GetEllipseCenter(PointD[] pt)
    {
        //�܂� a*x^2 + b*x*y + c*y^2 + d*x + e*y = 1 �Ƃ��� a,b,c,d,e��5�̃p�����[�^���ŏ��Q��@���狁�߂�
        if (pt.Length < 8) return new PointD(double.NaN, double.NaN);
        var Q = new DenseMatrix(pt.Length, 5);
        for (int i = 0; i < pt.Length; i++)
        {
            Q[i, 0] = pt[i].X * pt[i].X;
            Q[i, 1] = pt[i].X * pt[i].Y;
            Q[i, 2] = pt[i].Y * pt[i].Y;
            Q[i, 3] = pt[i].X;
            Q[i, 4] = pt[i].Y;
        }
        var A = new DenseVector(pt.Length);
        for (int i = 0; i < pt.Length; i++)
            A[i] = 1000000;
        if (!(Q.Transpose() * Q).TryInverse(out Matrix<double> inv))
            return new PointD(double.NaN, double.NaN);
        var C = inv * Q.Transpose() * A;

        double a = C[0], b = C[1], c = C[2], d = C[3], e = C[4];
        //���̂Ƃ��̕��s�ړ���(�܂蒆�S�ʒu)��
        //return new PointD(-d / 2 / a, -e / 2 / c);
        return new PointD(-(b * e - 2 * c * d) / (b * b - 4 * a * c), -(b * d - 2 * a * e) / (b * b - 4 * a * c));
    }

    /// <summary>
    /// �^����ꂽPointD[]�ɂ����Ƃ��߂��ȉ~�̕����� a*x^2 + b*x*y + c*y^2 + d*x + e*y = 1000000 �� a,b,c,d,e��5�̃p�����[�^��Ԃ�
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public static double[] GetParameterOfCurveOfSecondaryDegree(PointD[] point)
    {
        //�܂� a*x^2 + b*x*y + c*y^2 + d*x + e*y = 1000000 �Ƃ��� a,b,c,d,e��5�̃p�����[�^���ŏ��Q��@���狁�߂�
        var pt = new List<PointD>();
        for (int i = 0; i < point.Length; i++)
            if (!double.IsNaN(point[i].X))
                pt.Add(point[i]);

        if (pt.Count < 5) return null;
        var Q = new DenseMatrix(pt.Count, 5);
        for (int i = 0; i < pt.Count; i++)
        {
            Q[i, 0] = pt[i].X * pt[i].X;
            Q[i, 1] = pt[i].X * pt[i].Y;
            Q[i, 2] = pt[i].Y * pt[i].Y;
            Q[i, 3] = pt[i].X;
            Q[i, 4] = pt[i].Y;
        }
        var A = new DenseVector(pt.Count);
        for (int i = 0; i < pt.Count; i++)
            A[i] = 1000000;

        if (!(Q.Transpose() * Q).TryInverse(out Matrix inv))
            return null;

        var C = inv * Q.Transpose() * A;

        //catch { return null; }
        double a = C[0];
        double b = C[1];
        double c = C[2];
        double d = C[3];
        double e = C[4];

        return [a, b, c, d, e];
    }

    /// <summary>
    /// ���aR�̉~���J������FD, �X��phi �Ɓ@tau�@�̂Ƃ��ǂ̂悤�ȋ�`(offset, width, height, cos, sin)�ɂȂ邩��Ԃ�
    /// </summary>
    /// <param name="center"></param>
    /// <param name="radius"></param>
    /// <param name="FilmDistance"></param>
    /// <param name="pixelSize"></param>
    /// <param name="phi"></param>
    /// <param name="tau"></param>
    /// <param name="rect"></param>
    /// <param name="Rot"></param>
    public static (PointD OffSet, double Width, double Height, double Cos, double Sin) GetEllipseRectangleAndRot(double R, double FD, double phi, double tau)
    {
        PointD OffSet;
        double Width, Height, Cos, Sin;
        //���W�n�͎����(mm)�ōl����
        //a*x^2 + b*x*y + c*y^2 + d*x + e*y = 10000
        double CosPhi = Math.Cos(phi), SinPhi = Math.Sin(phi);
        double CosTau = Math.Cos(tau), SinTau = Math.Sin(tau);
        double a = 10000 * ((CosPhi * CosPhi + CosTau * CosTau * SinPhi * SinPhi) / R / R - SinPhi * SinPhi * SinTau * SinTau / FD / FD);
        double b = 10000 * 2 * (FD * FD + R * R) * SinPhi * CosPhi * SinTau * SinTau / R / R / FD / FD;
        double c = 10000 * ((CosPhi * CosPhi * CosTau * CosTau + SinPhi * SinPhi) / R / R - CosPhi * CosPhi * SinTau * SinTau / FD / FD);
        double d = 10000 * 2 * SinPhi * SinTau / FD;
        double e = -10000 * 2 * CosPhi * SinTau / FD;

        //���� a*x^2 + b*x*y + c*y^2 + d*x + e*y = 10000 ���@a * x^2 + b *x*y + c * y^2 = f �Ƃ����`�ɕϊ�����
        //���̂Ƃ��̕��s�ړ���(�܂蒆�S�ʒu)��X= -d / 2a, Y= -e / 2c
        //OffSet = new PointD(-d / 2 / a, -e / 2 / c);
        OffSet = new PointD(-(b * e - 2 * c * d) / (b * b - 4 * a * c), -(b * d - 2 * a * e) / (b * b - 4 * a * c));
        //�܂�f��
        double f = 10000 + OffSet.X * OffSet.X * a + OffSet.Y * OffSet.Y * c;

        if (Math.Abs(b) < 0.000000001)//b==0�̎��͌ŗL�l��肪�Ƃ��Ȃ�
        {
            Cos = 1;
            Sin = 0;
            Width = Math.Sqrt(f / a);
            Height = Math.Sqrt(f / c);
        }
        else
        {
            b *= 0.5;
            //����{{a,b}{b,c}}�̌ŗL�l,�ŗL�x�N�g�������߂�
            double sqrt = Math.Sqrt(4 * b * b + (a - c) * (a - c));
            //Cos = 0.5 * (1 + (c - a) / sqrt);
            Cos = (a - c - sqrt) / Math.Sqrt(2) / Math.Sqrt(4 * b * b - (a - c) * (-a + sqrt + c));
            //if ((a - sqrt - c) / 2 / b < 0) Cos = -Cos;
            Sin = (a - c + sqrt) / Math.Sqrt(2) / Math.Sqrt(4 * b * b + (a - c) * (a + sqrt - c));
            if (b < 0) Sin = -Sin;

            double k1 = (a + c - sqrt) / 2.0;
            double k2 = (a + c + sqrt) / 2.0;

            //��������ƌ��� k1 X^2 + k2 Y^2 = f�Ƃ����������ɏ����������Ƃ��ł���B
            Width = Math.Sqrt(f / k1);
            Height = Math.Sqrt(f / k2);
        }

        return (OffSet, Width, Height, Cos, Sin);
    }

    /// <summary>
    ///  �ȉ~�̒��S�Q�Ƃ����̔��a����A�^�̒��S�̃I�t�Z�b�g�ʒu(offset)�ƌX��(tau, phi)�Ƃ����̌덷��Ԃ�
    /// </summary>
    /// <param name="EllipseCenter"></param>
    /// <param name="Radius"></param>
    /// <param name="CameraLength"></param>
    /// <param name="offset"></param>
    /// <param name="offsetDev"></param>
    /// <param name="tau"></param>
    /// <param name="tauDev"></param>
    /// <param name="phi"></param>
    /// <param name="phiDev"></param>
    public static void GetTiltAndOffset(PointD[] EllipseCenter, double[] Radius, double CameraLength, ref PointD offset, ref PointD offsetDev,
      ref double tau, ref double tauDev, ref double phi, ref double phiDev)
    {
        //�C�ӂ̓�_��I���offset, tau, phi���v�Z����
        List<double> offsetXList = [], offsetYList = [];
        List<double> tauList = [], phiList = [];

        for (int i = 0; i < EllipseCenter.Length; i++)
            for (int j = i + 1; j < EllipseCenter.Length; j++)
            {
                var ellipseCenterTemp = new PointD[] { EllipseCenter[i], EllipseCenter[j] };
                var radiusTemp = new double[] { Radius[i], Radius[j] };
                var offsetTemp = offset;
                var tauTemp = tau;
                var phiTemp = phi;

                GetTiltAndOffset(ellipseCenterTemp, radiusTemp, CameraLength, ref offsetTemp, ref tauTemp, ref phiTemp);

                offsetXList.Add(offsetTemp.X);
                offsetYList.Add(offsetTemp.Y);
                tauList.Add(tauTemp);
                phiList.Add(phiTemp);
            }
        offsetDev = new PointD(Statistics.Deviation([.. offsetXList]), Statistics.Deviation([.. offsetYList]));
        tauDev = Statistics.Deviation([.. tauList]);

        double phiDev1, phiDev2;

        phiDev1 = Statistics.Deviation([.. phiList]);
        for (int i = 0; i < phiList.Count; i++)
            if (phiList[i] < 0) phiList[i] += Math.PI;
        phiDev2 = Statistics.Deviation([.. phiList]);

        phiDev = Math.Min(phiDev1, phiDev2);

        //�SEllipseCenter��p���āAoffset, tau, phi���v�Z����
        GetTiltAndOffset(EllipseCenter, Radius, CameraLength, ref offset, ref tau, ref phi);
    }

    /// <summary>
    /// �ȉ~�̒��S�Q�Ƃ����̔��a����A�^�̒��S�̃I�t�Z�b�g�ʒu(offset)�ƌX��(tau, phi)��Ԃ�
    /// </summary>
    /// <param name="CenterPt"></param>
    /// <param name="Peaks"></param>
    /// <param name="PixelSize"></param>
    /// <param name="CameraLength"></param>
    /// <param name="offset"></param>
    /// <param name="tau"></param>
    /// <param name="phi"></param>
    public static void GetTiltAndOffset(PointD[] EllipseCenter, double[] Radius, double CameraLength, ref PointD offset,
           ref double tau, ref double phi)
    {
        //�܂�CenterPt�̉�A���������߂�
        double phi1, A;
        phi1 = A = 0;
        Statistics.LineFitting(EllipseCenter, ref phi1, ref A);
        double CosPhi1 = Math.Cos(phi1), SinPhi1 = Math.Sin(phi1);
        bool xMode = Math.Abs(CosPhi1) > 1 / Math.Sqrt(2);

        //���̒�����̓_B(x,y)�ƁA�eCenterPt�̋���Ri�Ƃ����Ƃ�
        //��^2= ( Ri - Cameralength * Tan(2��)^2 *Sin(��) / pixelSize )^2�@
        //���ŏ��ɂȂ�悤�ȓ_B�ƃՂ�������

        //double[] TheoriticalRperSinPsi = new double[EllipseCenter.Length];
        double startTau1 = -Math.PI / 180;
        double stepTau1 = Math.PI / 9000;
        double endTau1 = Math.PI / 180;
        double tau1, bestTau1;
        tau1 = 0;

        double startCenter = -5;
        double stepCenter = 0.1;
        double endCenter = 5;
        double Center, BestCenter;
        double BestY, BestX, X, Y;

        double residual, bestResidual;
        bestResidual = double.PositiveInfinity;
        BestX = BestY = bestTau1 = BestCenter = 0;
        //double temp;
        for (int n = 0; n < 40; n++)
        {
            for (Center = startCenter; Center <= endCenter; Center += stepCenter)
            {
                if (xMode)
                {
                    X = Center;
                    Y = (X * SinPhi1 - A) / CosPhi1;
                }
                else
                {
                    Y = Center;
                    X = (Y * CosPhi1 + A) / SinPhi1;
                }
                for (tau1 = startTau1; tau1 <= endTau1; tau1 += stepTau1)
                {
                    residual = 0;
                    for (int i = 0; i < EllipseCenter.Length; i++)
                    {
                        //���݂�Tau,X,Y����\�z����锼�aR�̉~�̒��S�ʒu��
                        double IdealX, IdealY;
                        double TwoTheta = Math.Atan2(Radius[i], CameraLength);
                        //R��tau�����̂Ƃ����A���̂Ƃ���
                        //double R = CameraLength * 2 * Math.Sin(TwoTheta) * Math.Sin(TwoTheta) * Math.Sin(tau1) / (Math.Cos(2 * TwoTheta) + Math.Cos(2 * tau1));
                        //1/2 CL tan2q { sinj [tan(2q+j) -tan(2q-j)] }
                        double R = 0.5 * CameraLength * Math.Sin(TwoTheta) * (1 / Math.Cos(TwoTheta + tau1) - 1 / Math.Cos(TwoTheta - tau1));
                        if (tau1 > 0)
                            R = Math.Abs(R);
                        else
                            R = -Math.Abs(R);

                        IdealX = R * CosPhi1;
                        IdealY = R * SinPhi1;

                        if (xMode && tau1 * IdealX <= 0)//�������ɍL�����Ă��� tau1 > 0 ���� idealX <0  ���邢�� tau1 < 0 ���� idealX > 0 �̂Ƃ���
                        {
                            IdealX = -IdealX;
                            IdealY = -IdealY;
                        }
                        if (!xMode && tau1 * IdealY <= 0)//�c�����ɍL�����Ă��� tau1 > 0 ���� idealY <0  ���邢�� tau1 < 0 ���� idealY > 0 �̂Ƃ���
                        {
                            IdealX = -IdealX;
                            IdealY = -IdealY;
                        }
                        //�����܂łŁATau ���Ȃ킿 R�����̂Ƃ��͑ȉ~�̒��S��X,Y�̂����ꂩ�̕����ɐ��ɐU��鎖�ɂȂ�
                        residual += (X + IdealX - EllipseCenter[i].X) * (X + IdealX - EllipseCenter[i].X) * 1000
                            + (Y + IdealY - EllipseCenter[i].Y) * (Y + IdealY - EllipseCenter[i].Y) * 1000;
                    }
                    if (residual < bestResidual)
                    {
                        bestResidual = residual;
                        BestX = X;
                        BestY = Y;
                        BestCenter = Center;
                        bestTau1 = tau1;
                    }
                }
            }
            startCenter = BestCenter - 2.4 * stepCenter;
            endCenter = BestCenter + (2.4 * stepCenter);
            stepCenter *= 0.8;

            startTau1 = bestTau1 - 2.4 * stepTau1;
            endTau1 = bestTau1 + 2.4 * stepTau1;
            stepTau1 *= 0.8;
        }//�œK���I��

        offset = new PointD(BestX, BestY);

        //��������270��������
        phi1 -= 9 * Math.PI / 2;

        //xMode���^����tau�����̂Ƃ�. ���̂Ƃ���]����-135���`-45���ɂȂ�ׂ�
        if (xMode && bestTau1 >= 0)
            while (phi1 < -Math.PI * 3 / 4 - 0.01)//-135���`-45���͈̔͂���Ȃ��Ƃ���
                phi1 += Math.PI;
        //xMode���^����tau�����̂Ƃ�. ���̂Ƃ���]����45���`135���ɂȂ�ׂ�
        if (xMode && bestTau1 < 0)
            while (phi1 < Math.PI / 4 - 0.01)//45���ȉ��̂Ƃ���
                phi1 += Math.PI;
        //xMode���U����tau�����̂Ƃ�. ���̂Ƃ���]����-45���`45���ɂȂ�ׂ�
        if (!xMode && bestTau1 >= 0)
            while (phi1 < -Math.PI / 4 - 0.01)//-45���`45���͈̔͂���Ȃ��Ƃ���
                phi1 += Math.PI;
        //xMode���U����tau�����̂Ƃ�.���̂Ƃ���]����135���`215���ɂȂ�ׂ�
        if (!xMode && bestTau1 < 0)
            while (phi1 < Math.PI * 3 / 4 - 0.01)//135���`215���͈̔͂���Ȃ��Ƃ���
                phi1 += Math.PI;

        bestTau1 = Math.Abs(bestTau1);

        double CosPhi = Math.Cos(phi);
        double SinPhi = Math.Sin(phi);
        double CosTau = Math.Cos(tau);
        double SinTau = Math.Sin(tau);

        CosPhi1 = Math.Cos(phi1);
        SinPhi1 = Math.Sin(phi1);
        double CosTau1 = Math.Cos(bestTau1);
        double SinTau1 = Math.Sin(bestTau1);

        var M = new Matrix3D(CosPhi, SinPhi, 0, -SinPhi, CosPhi, 0, 0, 0, 1);
        var P = new Matrix3D(1, 0, 0, 0, CosTau, SinTau, 0, -SinTau, CosTau);

        var M1 = new Matrix3D(CosPhi1, SinPhi1, 0, -SinPhi1, CosPhi1, 0, 0, 0, 1);
        var P1 = new Matrix3D(1, 0, 0, 0, CosTau1, SinTau1, 0, -SinTau1, CosTau1);

        var Q = M1 * P1 * M1.Transpose() * M * P * M.Transpose();

        tau = Math.Acos(Q.E33);
        if (Math.Sin(tau) == 0)
            phi = 0;
        else
            phi = Math.Atan2(-Q.E31, Q.E32);

        bestResidual = double.PositiveInfinity;
        double startPhi = phi - 0.1, endPhi = phi + 0.1, stepPhi = 0.01;
        double startTau = tau - 0.01, endTau = tau + 0.01, stepTau = 0.001;
        double bestPhi = phi, bestTau = tau;
        double temp;
        for (int n = 0; n < 30; n++)
        {
            for (phi = startPhi; phi <= endPhi; phi += stepPhi)
                for (tau = startTau; tau <= endTau; tau += stepTau)
                {
                    CosPhi = Math.Cos(phi);
                    SinPhi = Math.Sin(phi);
                    CosTau = Math.Cos(tau);
                    SinTau = Math.Sin(tau);

                    residual = 0;

                    temp = Q.E11 - (CosPhi * CosPhi + CosTau * SinPhi * SinPhi);
                    residual += temp * temp;
                    temp = Q.E12 - (CosPhi * SinPhi - CosTau * CosPhi * SinPhi);
                    residual += temp * temp;
                    temp = Q.E13 - (SinPhi * SinTau);
                    residual += temp * temp;
                    temp = Q.E21 - (CosPhi * SinPhi - CosPhi * CosTau * SinPhi);
                    residual += temp * temp;
                    temp = Q.E22 - (CosPhi * CosPhi * CosTau + SinPhi * SinPhi);
                    residual += temp * temp;
                    temp = Q.E23 - (-CosPhi * SinTau);
                    residual += temp * temp;
                    temp = Q.E31 - (-SinPhi * SinTau);
                    residual += temp * temp;
                    temp = Q.E32 - (CosPhi * SinTau);
                    residual += temp * temp;
                    temp = Q.E33 - (CosTau);
                    residual += temp * temp;
                    if (bestResidual > residual)
                    {
                        bestPhi = phi;
                        bestTau = tau;
                        bestResidual = residual;
                    }
                }
            startPhi = bestPhi - stepPhi;
            endPhi = bestPhi + stepPhi;
            stepPhi *= 0.4;
            startTau = bestTau - stepTau;
            endTau = bestTau + stepTau;
            stepTau *= 0.4;
        }

        phi = bestPhi;
        tau = bestTau;
    }

    /// <summary>
    /// �ȉ~�̕���������A���̑ȉ~�Q�������Ƃ��^�~�ɋ߂Â���p�����[�^(PixX, PixY, Ksi)�����߂�
    /// </summary>
    /// <param name="ellipse"></param>
    /// <param name="PixX"></param>
    /// <param name="PixY"></param>
    /// <param name="Ksi"></param>
    /// <param name="PixXDev"></param>
    /// <param name="PixYDev"></param>
    /// <param name="KsiDev"></param>
    /// <param name="distortion"></param>
    public static void GetPixelShape(EllipseParameter[] ellipse, ref double PixX, ref double PixY, ref double Ksi, ref double PixXDev, ref double PixYDev, ref double KsiDev, bool distortion)
    {
        for (int i = 0; i < ellipse.Length; i++)
        {
            ellipse[i].SetCoeff();
            //R��R�ɂȂ�悤�ɒ��߂���
            ellipse[i].Coeff[0] *= ellipse[i].millimeterCalc * ellipse[i].millimeterCalc / 1000000;
            ellipse[i].Coeff[1] *= ellipse[i].millimeterCalc * ellipse[i].millimeterCalc / 1000000;
            ellipse[i].Coeff[2] *= ellipse[i].millimeterCalc * ellipse[i].millimeterCalc / 1000000;
        }

        //���aR�̉~ X^2 + Y^2 = R^2�@���A�t�B���ϊ� { { A, B} , { 0 , C } } �Ƃ����s��ŕϊ�������
        //�ȉ~   a * X^2 + b * X * Y + c * Y^2  = X^2 / A^2 - X * Y * 2B /A^2/C  + Y^2 * (A^2+B^2) /(A^2 C^2) = R^2 �ɕϊ������
        //���̂Ƃ�
        //A =  1 / ��(a)
        //B = - b /��(a) /��(4ac-b^2)
        //C = 2��(a) /��(4ac-b^2)

        var tempPixX = new List<double>(ellipse.Length);
        var tempPixY = new List<double>(ellipse.Length);
        var tempKsi = new List<double>(ellipse.Length);
        double A, B, C;
        for (int i = 0; i < ellipse.Length; i++)
        {
            A = Math.Sqrt(1.0 / ellipse[i].Coeff[0]);
            B = -ellipse[i].Coeff[1] / Math.Sqrt(ellipse[i].Coeff[0] * (4 * ellipse[i].Coeff[0] * ellipse[i].Coeff[2] - ellipse[i].Coeff[1] * ellipse[i].Coeff[1]));
            C = 2 * Math.Sqrt(ellipse[i].Coeff[0] / (4 * ellipse[i].Coeff[0] * ellipse[i].Coeff[2] - ellipse[i].Coeff[1] * ellipse[i].Coeff[1]));
            //{ { A, B} , { 0 , C } } * {x, y} = { { PixX , PixY SinKsi } , { 0 , PixelY } } * {X,Y} // ������ {x,y}�͐^�̉~��̍��W, {X,Y}�̓s�N�Z�����W
            //������@newPixX = PixX /A, newPixY = PixY /C, tan(newKsi) = C (tanKsi-B) /A
            tempPixX.Add(PixX / A);
            tempPixY.Add(PixY / C);
            tempKsi.Add(Math.Atan(C / A * (Math.Tan(Ksi) - B)));
        }

        PixX = tempPixX.Average();
        PixXDev = Statistics.Deviation([.. tempPixX]);
        PixY = tempPixY.Average();
        PixYDev = Statistics.Deviation([.. tempPixY]);
        if (distortion)
        {
            Ksi = tempKsi.Average();
            KsiDev = Statistics.Deviation([.. tempKsi]);
        }
    }


    /// <summary>
    /// �_p���A�^����ꂽ�_(�E��肩�����)pts�ō���鑽�p�`�̓����ɑ��݂��邩�ǂ����𔻒肷��B
    /// https://www.nttpc.co.jp/technology/number_algorithm.html Winding Number Algorithm�̕ό`���Q�l�ɂ����B
    /// </summary>
    /// <param name="pts"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public static bool InsidePolygonalArea(List<PointD> pts, in PointD p) => InsidePolygonalArea(CollectionsMarshal.AsSpan(pts), in p);

    /// <summary>
    /// �_p���A�^����ꂽ�_(�E��肩�����)pts�ō���鑽�p�`�̓����ɑ��݂��邩�ǂ����𔻒肷��B
    /// https://www.nttpc.co.jp/technology/number_algorithm.html Winding Number Algorithm�̕ό`���Q�l�ɂ����B
    /// </summary>
    /// <param name="pts"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public static bool InsidePolygonalArea(Span<PointD> pts, in PointD p)
    {
        if (pts.Length < 3) return false;
        int wn = 0;
        for (int i = 0; i < pts.Length - 1; i++)
        {
            // ������̕ӁA�������̕ӂɂ���ď������������B
            // ������̕ӁB�_P��y�������ɂ��āA�n�_�ƏI�_�̊Ԃɂ���B�������A�I�_�͊܂܂Ȃ��B(���[��1)
            if ((pts[i].Y <= p.Y) && (pts[i + 1].Y > p.Y))
            {
                // �ӂ͓_p�����E���ɂ���B�������A�d�Ȃ�Ȃ��B(���[��4)
                // �ӂ��_p�Ɠ��������ɂȂ�ʒu����肵�A���̎���x�̒l�Ɠ_p��x�̒l���r����B
                var vt = (p.Y - pts[i].Y) / (pts[i + 1].Y - pts[i].Y);
                if (p.X < (pts[i].X + (vt * (pts[i + 1].X - pts[i].X))))
                    ++wn;  //�������d�v�B������̕ӂƌ��������ꍇ��+1
            }
            // �������̕ӁB�_P��y�������ɂ��āA�n�_�ƏI�_�̊Ԃɂ���B�������A�n�_�͊܂܂Ȃ��B(���[��2)
            else if ((pts[i].Y > p.Y) && (pts[i + 1].Y <= p.Y))
            {
                // �ӂ͓_p�����E���ɂ���B�������A�d�Ȃ�Ȃ��B(���[��4)
                // �ӂ��_p�Ɠ��������ɂȂ�ʒu����肵�A���̎���x�̒l�Ɠ_p��x�̒l���r����B
                var vt = (p.Y - pts[i].Y) / (pts[i + 1].Y - pts[i].Y);
                if (p.X < (pts[i].X + (vt * (pts[i + 1].X - pts[i].X))))
                    --wn;  //�������d�v�B�������̕ӂƌ��������ꍇ��-1
            }
            // ���[��1,���[��2���m�F���邱�ƂŁA���[��3���m�F�ł��Ă���B
        }

        //�Ō�ɁA�Ō�̓_�ƍŏ��̓_���v�Z
        if ((pts[^1].Y <= p.Y) && (pts[0].Y > p.Y))
        {
            var vt = (p.Y - pts[^1].Y) / (pts[0].Y - pts[^1].Y);
            if (p.X < (pts[^1].X + (vt * (pts[0].X - pts[^1].X))))
                ++wn;
        }
        else if ((pts[^1].Y > p.Y) && (pts[0].Y <= p.Y))
        {
            var vt = (p.Y - pts[^1].Y) / (pts[0].Y - pts[^1].Y);
            if (p.X < (pts[^1].X + (vt * (pts[0].X - pts[^1].X))))
                --wn;
        }
        return wn != 0;
    }


    /// <summary>
    /// �^����ꂽ�_(�E��肩�����)�ň͂܂��ʐς�Ԃ�
    /// </summary>
    /// <param name="pt"></param>
    /// <returns></returns>
    public static double GetPolygonalArea(PointD[] pt) => GetPolygonalArea(pt.Select(p => (p.X, p.Y)).ToArray());

    /// <summary>
    /// �^����ꂽ�_(�E��肩�����)�ň͂܂��ʐς�Ԃ�
    /// </summary>
    /// <param name="pt"></param>
    /// <returns></returns>
    public static double GetPolygonalArea((double X, double Y)[] pt)
    {
        if (pt == null || pt.Length < 3) return 0;

        double area = 0;
        for (int i = 0; i < pt.Length - 1; i++)
            area += (pt[i].X - pt[i + 1].X) * (pt[i].Y + pt[i + 1].Y);
        area += (pt[^1].X - pt[0].X) * (pt[^1].Y + pt[0].Y);
        return Math.Abs(area * 0.5);
    }

    public static PointD[] GetPolygonDividedByLine(PointD[] pt, double a, double b, double c)
    {
        var results = GetPolygonDividedByLine(pt.Select(p => (p.X, p.Y)).ToArray(), a, b, c);
        return results.Select(r => new PointD(r.X, r.Y)).ToArray();
    }

    /// <summary>
    /// �^����ꂽ�_pt(�E��肩�����)�ō\������鑽�p�`�ƁA���钼��(ax+by>c)�ň͂܂�鑽�p�`��Ԃ�  (ax+by��c)�̎��͑S�Ă̌W���̕������t�]
    /// </summary>
    /// <param name="pt"></param>
    /// <returns></returns>
    public static (double X, double Y)[] GetPolygonDividedByLine((double X, double Y)[] pt, double a, double b, double c)
    {
        //pt��2�_�ȉ��Ȃ炻�̂܂ܕԂ�
        if (pt == null || pt.Length < 3) return pt;

        //�܂��^����ꂽ�����̏��� (ax+by>c���邢��ax+by<c)�Ɗe�_���ׂ�

        //pt��x,y�l���i�[�������Z�ɔ͈͓�(1)���͈͊O(0)�����L�^����ꎞ�I�ȕϐ�
        var ptAlpha = new List<(double X, double Y, bool Flag)>();
        bool flag1 = true, flag2 = true;
        for (int i = 0; i < pt.Length; i++)
        {
            (double X, double Y, bool Flag) v = (pt[i].X, pt[i].Y, true);
            if (a * pt[i].X + b * pt[i].Y < c)//�͈͊O�ł����
            {
                v.Flag = false;
                flag1 = false;//��ł��͈͊O�̂��̂������flag1��false�ɂȂ�
            }
            else
                flag2 = false;//��ł��͈͓��̂��̂������flag2��false�ɂȂ�

            ptAlpha.Add(v);
        }

        //�͈͊O�_���Ȃ���΂��̂܂�pt��Ԃ��B
        if (flag1)
            return pt;

        //�͈͊O�_������ꍇ

        //���ׂĂ��͈͊O�Ȃ�null��Ԃ�
        if (flag2)
            return [];

        for (int i = 0; i < ptAlpha.Count; i++)
        {
            int next = i < ptAlpha.Count - 1 ? i + 1 : 0;
            if (ptAlpha[i].Flag != ptAlpha[next].Flag)
            {
                var (X, Y) = getCrossPoint(ptAlpha[i], ptAlpha[next], a, b, c);
                (double X, double Y, bool Flag) v = (X, Y, true);
                ptAlpha.Insert(i + 1, v);
                i++;
            }
        }

        var ptBeta = new List<(double X, double Y)>();
        for (int i = 0; i < ptAlpha.Count; i++)
            if (ptAlpha[i].Flag)
                ptBeta.Add((ptAlpha[i].X, ptAlpha[i].Y));

        return [.. ptBeta];
    }

    /// <summary>
    /// 2�����ɂ����āA���� a x + b y = c�@�� �_pt1��pt2�����Ԓ����ƌ�����_��Ԃ�
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    private static (double X, double Y) getCrossPoint((double X, double Y, bool Flag) p1, (double X, double Y, bool Flag) p2, double a, double b, double c)
    {
        //����2�̕������𖞂���x, y�����߂�΂悢
        //a x + b y = c
        //(y1 - y2) x - (x1 - x2) y = x1 y2 - x2 y1

        if (a * (p1.X + p2.X) - b * (p1.Y + p2.Y) == 0)
            return ((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);

        return (
            (c * (p1.X - p2.X) + b * (p2.X * p1.Y - p1.X * p2.Y)) / (a * (p1.X - p2.X) + b * (p1.Y - p2.Y)),
            (c * (p1.Y - p2.Y) + a * (p1.X * p2.Y - p2.X * p1.Y)) / (a * (p1.X - p2.X) + b * (p1.Y - p2.Y))
            );
    }

    /// <summary>
    /// 3�������� a x + b y + c z = d (�@���x�N�g��(a,b,c))���A�_pt1��pt2�����Ԓ����ƌ�����_��Ԃ�. ���ʕ�������a x + b y + c z + d = 0 �ł͂Ȃ����Ƃɒ���
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <param name="d"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    public static Vector3DBase GetCrossPoint(in double a, in double b, in double c, in double d, Vector3D p1, Vector3D p2)
        => GetCrossPoint(a, b, c, d, new Vector3D(p1.X, p1.Y, p1.Z), new Vector3DBase(p2.X, p2.Y, p2.Z));

    /// <summary>
    /// 3�������� a x + b y + c z = d (�@���x�N�g��(a,b,c))���A�_pt1��pt2�����Ԓ����ƌ�����_��Ԃ�. ���ʕ�������a x + b y + c z + d = 0 �ł͂Ȃ����Ƃɒ���
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <param name="d"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    public static Vector3DBase GetCrossPoint(in double a, in double b, in double c, in double d, Vector3DBase p1, Vector3DBase p2)
    {
        //����3�̕������𖞂���x, y, z �����߂�΂悢 (2020/02/04�C��)
        // a x + b y + c z = d
        //(y1 - y2) x - (x1 - x2) y = x2 y1 - x1 y2 
        //(z1 - z2) y - (y1 - y2) z = y2 z1 - y1 z2

        //double denom = a * (p1.X - p2.X) + b * (p1.Y - p2.Y) + c * (p1.Z - p2.Z);
        //double x = (d * (p1.X - p2.X) - b * (p1.X * p2.Y - p1.Y * p2.X) - c * (p1.X * p2.Z - p1.Z * p2.X)) / denom;
        //double y = (d * (p1.Y - p2.Y) - c * (p1.Y * p2.Z - p1.Z * p2.Y) - a * (p1.Y * p2.X - p1.X * p2.Y)) / denom;
        //double z = (d * (p1.Z - p2.Z) - a * (p1.Z * p2.X - p1.X * p2.Z) - b * (p1.Z * p2.Y - p1.Y * p2.Z)) / denom;

        double dx = p1.X - p2.X, dy = p1.Y - p2.Y, dz = p1.Z - p2.Z;

        var uz = p1.X * p2.Y - p1.Y * p2.X;
        var ux = p1.Y * p2.Z - p1.Z * p2.Y;
        var uy = p1.Z * p2.X - p1.X * p2.Z;

        var denom = a * dx + b * dy + c * dz;
        var x = (d * dx - b * uz + c * uy) / denom;
        var y = (d * dy - c * ux + a * uz) / denom;
        var z = (d * dz - a * uy + b * ux) / denom;

        return new Vector3DBase(x, y, z);
    }

    /// <summary>
    /// 3�������� a x + b y + c z = d (�@���x�N�g��(a,b,c))���A�_pt1��pt2�����Ԓ����ƌ�����_��Ԃ�. ���ʕ�������a x + b y + c z + d = 0 �ł͂Ȃ����Ƃɒ���
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <param name="d"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    public static Vector3d GetCrossPoint(in double a, in double b, in double c, in double d, in V3d p1, in V3d p2)
    {
        //����3�̕������𖞂���x, y, z �����߂�΂悢 (2020/02/04�C��)
        // a x + b y + c z = d
        //(y1 - y2) x - (x1 - x2) y = x2 y1 - x1 y2 
        //(z1 - z2) y - (y1 - y2) z = y2 z1 - y1 z2

        //double denom = a * (p1.X - p2.X) + b * (p1.Y - p2.Y) + c * (p1.Z - p2.Z);
        //double x = (d * (p1.X - p2.X) - b * (p1.X * p2.Y - p1.Y * p2.X) - c * (p1.X * p2.Z - p1.Z * p2.X)) / denom;
        //double y = (d * (p1.Y - p2.Y) - c * (p1.Y * p2.Z - p1.Z * p2.Y) - a * (p1.Y * p2.X - p1.X * p2.Y)) / denom;
        //double z = (d * (p1.Z - p2.Z) - a * (p1.Z * p2.X - p1.X * p2.Z) - b * (p1.Z * p2.Y - p1.Y * p2.Z)) / denom;

        double dx = p1.X - p2.X, dy = p1.Y - p2.Y, dz = p1.Z - p2.Z;

        var uz = p1.X * p2.Y - p1.Y * p2.X;
        var ux = p1.Y * p2.Z - p1.Z * p2.Y;
        var uy = p1.Z * p2.X - p1.X * p2.Z;

        var denom = a * dx + b * dy + c * dz;
        var x = (d * dx - b * uz + c * uy) / denom;
        var y = (d * dy - c * ux + a * uz) / denom;
        var z = (d * dz - a * uy + b * ux) / denom;

        return new V3d(x, y, z);
    }


    // <summary>
    /// 3�����ɂ����āA���� a x + b y + c z = d (�@���x�N�g��(a,b,c))�ƁA�_(0,0,0)��pt�����Ԓ����ƌ�����_��Ԃ�.  ���ʕ�������a x + b y + c z + d = 0 �ł͂Ȃ����Ƃɒ���
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <param name="d"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    public static Vector3DBase GetCrossPoint(in double a, in double b, in double c, in double d, Vector3DBase p)
    {
        //����3�̕������𖞂���x, y, z �����߂�΂悢 (2020/02/04�C��)
        // a x + b y + c z = d
        //(y1 - y2) x - (x1 - x2) y = x2 y1 - x1 y2 
        //(z1 - z2) y - (y1 - y2) z = y2 z1 - y1 z2

        //double denom = a * (p1.X - p2.X) + b * (p1.Y - p2.Y) + c * (p1.Z - p2.Z);
        //double x = (d * (p1.X - p2.X) - b * (p1.X * p2.Y - p1.Y * p2.X) - c * (p1.X * p2.Z - p1.Z * p2.X)) / denom;
        //double y = (d * (p1.Y - p2.Y) - c * (p1.Y * p2.Z - p1.Z * p2.Y) - a * (p1.Y * p2.X - p1.X * p2.Y)) / denom;
        //double z = (d * (p1.Z - p2.Z) - a * (p1.Z * p2.X - p1.X * p2.Z) - b * (p1.Z * p2.Y - p1.Y * p2.Z)) / denom;

        return p * d / (a * p.X + b * p.Y + c * p.Z);
    }

    /// <summary>
    /// sourcePoints�Ŏw�肳�ꂽ���C���v���t�@�C�����Aarea�Ŏw�肳�ꂽ�͈͓��Ő؂���B
    /// </summary>
    /// <param name="sourcePoints"></param>
    /// <param name="area"></param>
    /// <returns></returns>
    public static PointD[][] GetPointsWithinRectangle(IEnumerable<PointD> sourcePoints, RectangleD area)
    {
        var pt = sourcePoints.ToList();
        //�܂��A�����̏���Ɖ������g����
        var first = pt.FindIndex(p => p.X >= area.X) - 1;
        if (first > 0)
            pt.RemoveRange(0, first);
        var last = pt.FindLastIndex(p => p.X <= area.X + area.Width) + 2;
        if (last < pt.Count)
            pt.RemoveRange(last, pt.Count - last);

        if (pt.Max(p => p.Y) <= area.UpperY && pt.Min(pt => pt.Y) >= area.Y)
            return [[.. pt]];
        else if (pt.Max(p => p.Y) <= area.Y || pt.Min(pt => pt.Y) >= area.UpperY)
            return [[]];
        else
        {
            for (int i = 0; i < pt.Count - 1; i++)
            {
                if (!area.IsInsde(pt[i]) || !area.IsInsde(pt[i + 1])) //�ǂ��炩���͈͊O�̎�
                {
                    var pts = getCrossPoint(pt[i], pt[i + 1], area);
                    if (pts != null)
                    {
                        pt.InsertRange(i + 1, pts);
                        i += pts.Length;
                    }
                }
            }

            var results = new List<List<PointD>>();
            for (int i = 0; i < pt.Count - 1; i++)
            {
                if (!area.IsInsde(pt[i]))
                    pt.RemoveAt(i--);
                else
                {
                    var pts = new List<PointD>();
                    for (; i < pt.Count && area.IsInsde(pt[i]); i++)
                        pts.Add(new PointD(pt[i]));
                    i--;
                    results.Add(pts);
                }
            }
            return results.Select(r => r.ToArray()).ToArray();
        }
    }

    /// <summary>
    /// GetPointsWithinRectangle()���Ăяo��
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="rect"></param>
    /// <returns></returns>
    private static PointD[] getCrossPoint(PointD p1, PointD p2, RectangleD rect)
    {
        //�������� y= a x + b
        var a = (p2.Y - p1.Y) / (p2.X - p1.X);
        var b = p2.Y - a * p2.X;

        if (rect.IsInsde(p1))//p1���͈͓��ɂ���Ƃ�
        {
            if (rect.IsInsde(p2))//p1��p2���͈͓��ɂ���Ƃ�
            {
                return null;
            }
            else//p1���͈͓��ɂ�����p2���͈͊O�̂Ƃ�
            {
                if (double.IsInfinity(a))
                {
                    if (p2.Y > rect.UpperY)
                        return [new PointD(p1.X, rect.UpperY)];
                    else
                        return [new PointD(p1.X, rect.Y)];
                }
                //x=maxX�Ƃ̌�_��
                double c = a * rect.UpperX + b;

                if (c < rect.Y)
                    return [new PointD((rect.Y - b) / a, rect.Y)];
                else if (c > rect.UpperY)
                    return [new PointD((rect.UpperY - b) / a, rect.UpperY)];
                else
                    return [new PointD(rect.UpperX, c)];
            }
        }
        else//p1���͈͊O�ɂ���Ƃ�
        {
            if (rect.IsInsde(p2))//p1���͈͊O��p2���͈͓��̂Ƃ�
            {
                //�������� y= a x + b
                if (double.IsInfinity(a))
                {
                    if (p1.Y > rect.UpperY)
                        return [new PointD(p1.X, rect.UpperY)];
                    else
                        return [new PointD(p1.X, rect.Y)];
                }
                //x=minX�Ƃ̌�_��
                double c = a * rect.X + b;

                if (c < rect.Y)
                    return [new PointD((rect.Y - b) / a, rect.Y)];
                else if (c > rect.UpperY)
                    return [new PointD((rect.UpperY - b) / a, rect.UpperY)];
                else
                    return [new PointD(rect.X, c)];
            }
            else//p1��p2���͈͊O�̂Ƃ�
            {
                if (double.IsInfinity(a)) //�X��������̎�
                {
                    if (p1.X >= rect.X && p1.X <= rect.UpperX)//���҂�X�͔͈͓������AY�����ꂼ�����Ɖ����𒴂��Ă���ꍇ
                    {
                        if (p1.Y < rect.Y && rect.UpperY < p2.Y)
                            return [new PointD(p1.X, rect.Y), new PointD(p1.X, rect.UpperY)];
                        else if (p2.Y < rect.Y && rect.UpperY < p1.Y)
                            return [new PointD(p1.X, rect.UpperY), new PointD(p1.X, rect.Y)];
                    }
                    else
                        return null;
                }

                //4�̌�_�����߂�

                List<PointD> temp = new(new[] {
                    new PointD(rect.X, a * rect.X + b),
                    new PointD(rect.UpperX, a * rect.UpperX + b),
                    new PointD((rect.Y - b) / a, rect.Y),
                    new PointD((rect.UpperY - b) / a, rect.UpperY) });

                var pts = temp.Where(p => rect.IsInsde(p) && p.X >= p1.X && p.X <= p2.X && p.Y >= Math.Min(p1.Y, p2.Y) && p.Y <= Math.Max(p1.Y, p2.Y)).OrderBy(p => p.X).ToArray();
                if (pts.Length == 2)
                    return pts;
                else
                    return null;
            }
        }
    }

    /// <summary>
    ///  3�����ɂ����āA���� a x + b y + c z + d = 0 (�@���x�N�g��(a,b,c))�ƁA�_(x, y, z)�Ƃ̋���(��Βl)��Ԃ�
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <param name="d"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static double GetLengthFromPlane(double a, double b, double c, double d, double x, double y, double z)
    {
        return Math.Abs(a * x + b * y + c * z + d) / Math.Sqrt(a * a + b * b + c * c);
    }

    /// <summary>
    /// �_�W������ŏ����@�ɂ�镽�ʃp�����[�^A, B, C, D (�A���A���ʕ����� a x + b y + c z + d = 0) ��Ԃ�
    /// </summary>
    /// <param name="points"></param>
    /// <returns>double[]{a,b,c,d} (�A���A���ʕ����� a x + b y + c z + d = 0)</returns>
    public static (double A, double B, double C, double D) GetPlaneEquationFromPoints(IEnumerable<Vector3DBase> points)
    {
        //http://sysplan.nams.kyushu-u.ac.jp/gen/edu/Algorithms/PlaneFitting/index.html
        //pdf��Crystallograpy/�����t�H���_

        var ave = Vector3DBase.Average(points);
        var mtx = new DenseMatrix(points.Count(), 3);
        int n = 0;
        foreach (var p in points.Select(p => p - ave))
            mtx.SetRow(n++, p.ToDoublearray());

        var evd = (mtx.Transpose() * mtx).Evd(Symmetricity.Unknown);
        var index = evd.EigenValues.AbsoluteMinimumIndex();

        double a = evd.EigenVectors[0, index], b = evd.EigenVectors[1, index], c = evd.EigenVectors[2, index], d = -(a * ave.X + b * ave.Y + c * ave.Z);
        return (a, b, c, d);
    }

    /// <summary>
    /// �_�W������ŏ����@�ɂ�镽�ʃp�����[�^double[]{a,b,c,d} (�A���A���ʕ����� a x + b y + c z + d = 0) ��Ԃ�
    /// </summary>
    /// <param name="points"></param>
    /// <returns>double[]{a,b,c,d} (�A���A���ʕ����� a x + b y + c z + d = 0)</returns>
    public static (double A, double B, double C, double D) GetPlaneEquationFromPoints(IEnumerable<V3d> points)
    {
        //http://sysplan.nams.kyushu-u.ac.jp/gen/edu/Algorithms/PlaneFitting/index.html
        //pdf��Crystallograpy/�����t�H���_

        var ave = new OpenTK.Vector3d();
        foreach (var p in points)
            ave += p;
        ave /= points.Count();

        var mtx = new DenseMatrix(points.Count(), 3);
        int n = 0;
        foreach (var p in points.Select(p => p - ave))
            mtx.SetRow(n++, new[] { p.X, p.Y, p.Z });

        //var evd = (mtx.Transpose() * mtx).Evd(Symmetricity.Unknown);
        var evd = mtx.TransposeThisAndMultiply(mtx).Evd(Symmetricity.Symmetric);
        var index = evd.EigenValues.AbsoluteMinimumIndex();

        double a = evd.EigenVectors[0, index], b = evd.EigenVectors[1, index], c = evd.EigenVectors[2, index], d = -(a * ave.X + b * ave.Y + c * ave.Z);
        return (a, b, c, d);
    }


    /// <summary>
    /// �^����ꂽ����(double[4],  a x + b y + c z + d >= 0 )�̏W���ŁA��Ԃ����邩�ǂ����𔻒�
    /// </summary>
    /// <param name="prms"></param>
    /// <returns></returns>
    public static bool Enclosed(double[][] bounds)
    {
        if (bounds.Length < 4) return false;

        var countList = new List<int>();

        for (int i = 0; i < bounds.Length; i++)
        {
            var n = GetClippedPolygon(i, bounds).Length;
            if (n >= 3)
                countList.Add(GetClippedPolygon(i, bounds).Length);
            if (countList.Count >= 4)
                return true;
        }

        return false;
    }

    /// <summary>
    /// ���E�ʂɂ���Đ؂���ꂽ���p�`�̒��_���W�����߂�.
    /// </summary>
    /// <param name="p">�؂������ (double[4],  a x + b y + c z + d = 0) </param>
    /// <param name="bounds">���E�� (double[4],  a x + b y + c z + d >= 0 ) </param>
    /// <returns></returns>
    public static double[][] GetClippedPolygon(double[] p, double[][] bounds)
    {
        if (bounds.Length == 0) return null;

        List<Vector3d> pts;

        if (bounds.Length < 250)//bounds�����Ȃ��ꍇ�͐��U�@�ŉ���
        {
            pts = [];
            for (int i = 0; i < bounds.Length; i++)
            {
                var mtx = new Matrix3D(p[0], bounds[i][0], 0, p[1], bounds[i][1], 0, p[2], bounds[i][2], 0);
                for (int j = i + 1; j < bounds.Length; j++)
                {
                    mtx.E31 = bounds[j][0]; mtx.E32 = bounds[j][1]; mtx.E33 = bounds[j][2];
                    if (Math.Abs(mtx.Determinant()) > 0.0000000001)
                    {
                        var pt = (mtx.Inverse() * (-p[3], -bounds[i][3], -bounds[j][3])).ToOpenTK();
                        if (bounds.All(b => b[0] * pt.X + b[1] * pt.Y + b[2] * pt.Z + b[3] > -0.0000000001) && pts.All(p => (p - pt).LengthSquared > 0.0000000001))
                            pts.Add(pt);
                    }
                }
            }
        }
        else//bounds�������ꍇ�́A�傫�ȋ�`�����X�ɐ؂���悤�ȃA���S���Y��
        {
            var p2 = p[0] * p[0] + p[1] * p[1] + p[2] * p[2];

            //plane��́A�\���ɑ傫���ʐς�������4�_��p�ӂ���
            var max = bounds.Max(b => b[3]);
            Vector3DBase temp1 = new(p[0], p[1], p[2]), temp2 = new(0, 0, 1);
            var rotAngle = Vector3DBase.AngleBetVectors(temp1, temp2);
            var rotAxis = Vector3DBase.VectorProduct(temp1, temp2);
            var rot = Math.Abs(rotAngle - Math.PI) < 1E-10 ? Matrix3D.Rot((1, 0, 0), Math.PI) : Matrix3D.Rot(rotAxis, -rotAngle);

            pts = [(rot * (max, max, -p[3])).ToOpenTK(), (rot * (-max, max, -p[3])).ToOpenTK(), (rot * (-max, -max, -p[3])).ToOpenTK(), (rot * (max, -max, -p[3])).ToOpenTK()];

            //bounds�ɂ���Đ؂�������W���v�Z���A�V�����_�W��������Ă���
            foreach (var b in bounds.Where(b => !(b[0] == p[0] && b[1] == p[1] && b[2] == p[2] && b[3] == p[3])))
            {
                var b2 = b[0] * b[0] + b[1] * b[1] + b[2] * b[2];
                var pb = b[0] * p[0] + b[1] * p[1] + b[2] * p[2];

                if (pb * pb / p2 / b2 < 1 - 1E-10)// b��plane�����s�ł͂Ȃ��ꍇ
                {
                    var ptsNew = new List<V3d>(pts.Count + 1);
                    var v1 = pts[^1];
                    var r1 = v1.X * b[0] + v1.Y * b[1] + v1.Z * b[2] + b[3];
                    for (int i = 0; i < pts.Count; i++)
                    {
                        if (r1 > -1E-10)
                            ptsNew.Add(v1);

                        var v2 = pts[i];
                        var r2 = v2.X * b[0] + v2.Y * b[1] + v2.Z * b[2] + b[3];

                        if (Math.Abs(r1) > 1E-10 && Math.Abs(r2) > 1E-10 && r1 * r2 < 0) //v1��v2�̊Ԃ𕽖�b���ʂ�Ƃ��A
                        {
                            var pt = GetCrossPoint(b[0], b[1], b[2], -b[3], v1, v2);//d=b[3]�̕����ɒ���
                            if ((pt - v1).LengthSquared > 1E-10 && (pt - v2).LengthSquared > 1E-10)
                                ptsNew.Add(pt);
                        }
                        v1 = v2;
                        r1 = r2;
                    }
                    if (ptsNew.Count < 3)
                        return null;
                    pts = ptsNew;
                }
                else if ((pb > 0 && b[3] < p[3]) || (pb < 0 && b[3] < p[3]))  //b��Plane�����s�ȏꍇ�A�@���̌����ɒ��ӂ���null��Ԃ�
                    return null;
            }
        }

        return pts.Select(p => new double[] { p.X, p.Y, p.Z }).ToArray();
    }

    /// <summary>
    /// ���E�ʂɂ���Đ؂���ꂽ���p�`�̒��_���W�����߂�.
    /// </summary>
    /// <param name="i">�؂�����ʂ̃C���f�b�N�X</param>
    /// <param name="bounds">���E�� (������i�͏���) (double[4],  a x + b y + c z + d >= 0 ) </param>
    /// <returns></returns>
    public static double[][] GetClippedPolygon(int i, double[][] bounds) => GetClippedPolygon(bounds[i], bounds);

    /// <summary>
    /// �x�N�g��a1 => �x�N�g��b1 ���� �x�N�g��a2 => �x�N�g��b2�Ɏʂ��悤�ȉ�]�s������߂�. a1,a2,b1,b2�̒�����1�łȂ��Ă��\��Ȃ��i�֐����ŋK�i������j
    /// </summary>
    /// <param name="a1"></param>
    /// <param name="a2"></param>
    /// <param name="b1"></param>
    /// <param name="b2"></param>
    /// <returns></returns>
    public static Matrix3D GetRotation(Vector3DBase a1, Vector3DBase a2, Vector3DBase b1, Vector3DBase b2)
    {
        //�܂��K�i��
        var v1 = new Vector3DBase(a1).Normarize();
        var v2 = new Vector3DBase(a2).Normarize();
        var w1 = new Vector3DBase(b1).Normarize();
        var w2 = new Vector3DBase(b2).Normarize();

        var v3 = Vector3DBase.VectorProduct(v1, v2);
        var w3 = Vector3DBase.VectorProduct(w1, w2);

        return new Matrix3D(w1, w2, w3) * new Matrix3D(v1, v2, v3).Inverse();
    }

    /// <summary>
    /// �~���ƕ��ʂƂ̌�_(�f�ʍ��W�n)�̏W���𓾂�B
    /// �~���͒��_(0,0,0), �~�����p(alpha), �~�����S����(cosPhi*sinTau, -sinPhi*sinTau, cosTau)�Œ�`�����B
    /// �f�ʂ�Z=L�𖞂����A����̓_��upperLeft(�f�ʍ��W�n)�A�E��̓_��lowerRight(�f�ʍ��W�n)�Œ�`������`���ʂł���B
    /// �f�ʍ��W�n�Ƃ́A��_(X,Y,L)�ɂ��āA(X,Y)�̕����̂��Ƃł���(�f�ʂ̒��S��(0,0,L)�ł���))�B
    /// </summary>
    /// <param name="alpha">�~�����p(alpha)</param>
    /// <param name="phi"> �~�����S���̃p�����[�^. �~�����S��������(cosPhi*sinTau, -sinPhi*sinTau, cosTau)�Œ�`�����</param>
    /// <param name="tau">�~�����S���̃p�����[�^. �~�����S��������(cosPhi*sinTau, -sinPhi*sinTau, cosTau)�Œ�`�����</param>
    /// <param name="l">�f�ʂ̃p�����[�^. �f�ʂ�Z=L�Œ�`�����. </param>
    /// <param name="upperLeft">��`���ʂ̍�����W</param>
    /// <param name="lowerRight">��`���ʂ̉E�����W</param>
    /// <param name="bothCone"></param>
    /// <returns></returns>
    public static List<List<PointD>> ConicSection(in double alpha, in double phi, in double tau, in double l, in PointD upperLeft, in PointD lowerRight, bool bothCone = false)
    {
        double cosPhi = Math.Cos(phi), sinPhi = Math.Sin(phi);
        double cosTau = Math.Cos(tau), sinTau = Math.Sin(tau), sinTau2 = sinTau * sinTau;
        double cosAlpha = Math.Cos(alpha), cosAlpha2 = cosAlpha * cosAlpha;

        double P = -(sinTau2 - cosAlpha2) / (l * l * (1 - cosAlpha2)), Psqrt = Math.Sqrt(Math.Abs(P));
        double Q = -P * (sinTau2 - cosAlpha2) / cosAlpha2, Qsqrt = Math.Sqrt(Q);

        PointD rot(in PointD pt) => new PointD(cosPhi * pt.X - sinPhi * pt.Y, sinPhi * pt.X + cosPhi * pt.Y);

        var maxWidth = Math.Max(upperLeft.Length, lowerRight.Length);

        var tempResult = new List<List<PointD>>();


        if (!double.IsNaN(Psqrt) && !double.IsNaN(Qsqrt))
        {
            if (Math.Abs(P) < 1E-8)//������
            {
                var pts = new List<PointD>();
                var shift = new PointD(0, -l / Math.Tan(2 * tau));//���s�ړ�
                for (double x = -maxWidth; x < maxWidth; x += maxWidth / 2000.0)
                    pts.Add(rot(new PointD(x, x * x * sinTau / cosTau / l / 2) + shift));
                tempResult.Add(pts);
            }
            else
            {
                var shift = new PointD(0, -l * sinTau * cosTau / (sinTau2 - cosAlpha2));//���s�ړ���
                if (P > 0)//�ȉ~
                {
                    var pts = new List<PointD>();
                    for (double omega = 0; omega < Math.PI * 2.0000001; omega += Math.PI / 2000)
                        pts.Add(rot(new PointD(Math.Sin(omega + Math.PI / 2) / Psqrt, -Math.Cos(omega + Math.PI / 2) / Qsqrt) + shift));
                    tempResult.Add(pts);
                }
                else//�o�Ȑ�
                {
                    var pts1 = new List<PointD>();
                    //var pts2 = new List<PointD>();
                    var omegaMax = Math.Log(maxWidth * Psqrt + Math.Sqrt(maxWidth * Psqrt * maxWidth * Psqrt + 1)) * 2;
                    var sign = (tau > 0 && alpha < Math.PI / 2) || (tau < 0 && alpha >= Math.PI / 2) ? 1 : -1;
                    for (double omega = -omegaMax; omega < omegaMax; omega += omegaMax / 1000)
                    {
                        double x = Math.Sinh(omega) / Psqrt, y = Math.Cosh(omega) / Qsqrt;
                        pts1.Add(new PointD(x, sign * y));
                        //pts1.Add(rot(new PointD(x, sign * y) + shift));
                        //pts2.Add(rot(new PointD(x, -y) + shift));
                    }
                    tempResult.Add(pts1.Select(p => rot(p + shift)).ToList());
                    if (bothCone)
                        tempResult.Add(pts1.Select(p => rot(new PointD(p.X, -p.Y) + shift)).ToList());
                }
            }
        }

        //���E�`�F�b�N
        var result = new List<List<PointD>>();

        double xMin = upperLeft.X, yMin = upperLeft.Y, xMax = lowerRight.X, yMax = lowerRight.Y;
        bool inside(in PointD pt) => xMin <= pt.X && pt.X <= xMax && yMin <= pt.Y && pt.Y <= yMax;

        var n = tempResult.Count;
        for (int i = 0; i < n; i++)
        {
            var pts = tempResult[i];
            var flags = pts.Select(p => inside(p)).ToList();
            for (int j = 0; j < pts.Count - 1; j++)
            {
                if (flags[j] ^ flags[j + 1])//�ǂ��炩�����true�ł��������false�̎�
                {
                    // (Y2-Y1)x + (X1-X2)y = X1Y2-X2Y1�Ƃ��������ƁA
                    // x = xMin, x=xMax, y=yMin, y=yMax�Ƃ����S�{�̒����̌�_���v�Z����
                    double X1 = pts[j].X, X2 = pts[j + 1].X, Y1 = pts[j].Y, Y2 = pts[j + 1].Y;
                    var cross = new[]{
                        new PointD(xMin, (X1 * Y2 - X2 * Y1 - (Y2 - Y1) * xMin) / (X1 - X2)),
                        new PointD(xMax, (X1 * Y2 - X2 * Y1 - (Y2 - Y1) * xMax) / (X1 - X2)),
                        new PointD((X1 * Y2 - X2 * Y1 - (X1 - X2) * yMin) / (Y2 - Y1),yMin),
                        new PointD((X1 * Y2 - X2 * Y1 - (X1 - X2) * yMax) / (Y2 - Y1),yMax)
                        };
                    var lengthList = cross.Select(c => (c - (pts[j] + pts[j + 1]) / 2).Length2).ToList();
                    var index = lengthList.IndexOf(lengthList.Min());
                    flags.Insert(j + 1, true);
                    pts.Insert(j + 1, cross[index]);
                    if (flags[j])
                        j++;
                }
            }
            for (int j = 0; j < pts.Count; j++)
            {
                if (flags[j])
                {
                    result.Add([]);
                    for (; j < pts.Count && flags[j]; j++)
                        result[^1].Add(pts[j]);
                }
            }
        }


        return result;
    }

}