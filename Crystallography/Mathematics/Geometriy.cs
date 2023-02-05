using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;

namespace Crystallography;

public static class Geometriy
{
    static Geometriy()
    {
        MathNet.Numerics.Control.TryUseNativeMKL();
    }

    /// <summary>
    /// 楕円をとおる5点以上の点pt[]が与えられたとき最小2乗法から中心位置を返す関数
    /// </summary>
    /// <param name="pt"></param>
    /// <param name="focus1"></param>
    /// <param name="focus2"></param>
    public static PointD GetEllipseCenter(PointD[] pt)
    {
        //まず a*x^2 + b*x*y + c*y^2 + d*x + e*y = 1 という a,b,c,d,eの5つのパラメータを最小２乗法から求める
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
        //このときの平行移動量(つまり中心位置)は
        //return new PointD(-d / 2 / a, -e / 2 / c);
        return new PointD(-(b * e - 2 * c * d) / (b * b - 4 * a * c), -(b * d - 2 * a * e) / (b * b - 4 * a * c));
    }

    /// <summary>
    /// 与えられたPointD[]にもっとも近い楕円の方程式 a*x^2 + b*x*y + c*y^2 + d*x + e*y = 1000000 の a,b,c,d,eの5つのパラメータを返す
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public static double[] GetParameterOfCurveOfSecondaryDegree(PointD[] point)
    {
        //まず a*x^2 + b*x*y + c*y^2 + d*x + e*y = 1000000 という a,b,c,d,eの5つのパラメータを最小２乗法から求める
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

        return new double[] { a, b, c, d, e };
    }

    /// <summary>
    /// 半径Rの円がカメラ長FD, 傾きphi と　tau　のときどのような矩形(offset, width, height, cos, sin)になるかを返す
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
        //座標系は実空間(mm)で考える
        //a*x^2 + b*x*y + c*y^2 + d*x + e*y = 10000
        double CosPhi = Math.Cos(phi), SinPhi = Math.Sin(phi);
        double CosTau = Math.Cos(tau), SinTau = Math.Sin(tau);
        double a = 10000 * ((CosPhi * CosPhi + CosTau * CosTau * SinPhi * SinPhi) / R / R - SinPhi * SinPhi * SinTau * SinTau / FD / FD);
        double b = 10000 * 2 * (FD * FD + R * R) * SinPhi * CosPhi * SinTau * SinTau / R / R / FD / FD;
        double c = 10000 * ((CosPhi * CosPhi * CosTau * CosTau + SinPhi * SinPhi) / R / R - CosPhi * CosPhi * SinTau * SinTau / FD / FD);
        double d = 10000 * 2 * SinPhi * SinTau / FD;
        double e = -10000 * 2 * CosPhi * SinTau / FD;

        //次に a*x^2 + b*x*y + c*y^2 + d*x + e*y = 10000 を　a * x^2 + b *x*y + c * y^2 = f という形に変換する
        //このときの平行移動量(つまり中心位置)はX= -d / 2a, Y= -e / 2c
        //OffSet = new PointD(-d / 2 / a, -e / 2 / c);
        OffSet = new PointD(-(b * e - 2 * c * d) / (b * b - 4 * a * c), -(b * d - 2 * a * e) / (b * b - 4 * a * c));
        //またfは
        double f = 10000 + OffSet.X * OffSet.X * a + OffSet.Y * OffSet.Y * c;

        if (Math.Abs(b) < 0.000000001)//b==0の時は固有値問題がとけない
        {
            Cos = 1;
            Sin = 0;
            Width = Math.Sqrt(f / a);
            Height = Math.Sqrt(f / c);
        }
        else
        {
            b *= 0.5;
            //次に{{a,b}{b,c}}の固有値,固有ベクトルを求める
            double sqrt = Math.Sqrt(4 * b * b + (a - c) * (a - c));
            //Cos = 0.5 * (1 + (c - a) / sqrt);
            Cos = (a - c - sqrt) / Math.Sqrt(2) / Math.Sqrt(4 * b * b - (a - c) * (-a + sqrt + c));
            //if ((a - sqrt - c) / 2 / b < 0) Cos = -Cos;
            Sin = (a - c + sqrt) / Math.Sqrt(2) / Math.Sqrt(4 * b * b + (a - c) * (a + sqrt - c));
            if (b < 0) Sin = -Sin;

            double k1 = (a + c - sqrt) / 2.0;
            double k2 = (a + c + sqrt) / 2.0;

            //こうすると結局 k1 X^2 + k2 Y^2 = fという方程式に書き直すことができる。
            Width = Math.Sqrt(f / k1);
            Height = Math.Sqrt(f / k2);
        }

        return (OffSet, Width, Height, Cos, Sin);
    }

    /// <summary>
    ///  楕円の中心群とそれらの半径から、真の中心のオフセット位置(offset)と傾き(tau, phi)とそれらの誤差を返す
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
        //任意の二点を選んでoffset, tau, phiを計算する
        List<double> offsetXList = new(), offsetYList = new();
        List<double> tauList = new(), phiList = new();

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
        offsetDev = new PointD(Statistics.Deviation(offsetXList.ToArray()), Statistics.Deviation(offsetYList.ToArray()));
        tauDev = Statistics.Deviation(tauList.ToArray());

        double phiDev1, phiDev2;

        phiDev1 = Statistics.Deviation(phiList.ToArray());
        for (int i = 0; i < phiList.Count; i++)
            if (phiList[i] < 0) phiList[i] += Math.PI;
        phiDev2 = Statistics.Deviation(phiList.ToArray());

        phiDev = Math.Min(phiDev1, phiDev2);

        //全EllipseCenterを用いて、offset, tau, phiを計算する
        GetTiltAndOffset(EllipseCenter, Radius, CameraLength, ref offset, ref tau, ref phi);
    }

    /// <summary>
    /// 楕円の中心群とそれらの半径から、真の中心のオフセット位置(offset)と傾き(tau, phi)を返す
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
        //まずCenterPtの回帰直線を求める
        double phi1, A;
        phi1 = A = 0;
        Statistics.LineFitting(EllipseCenter, ref phi1, ref A);
        double CosPhi1 = Math.Cos(phi1), SinPhi1 = Math.Sin(phi1);
        bool xMode = Math.Abs(CosPhi1) > 1 / Math.Sqrt(2);

        //この直線上の点B(x,y)と、各CenterPtの距離Riとしたとき
        //δ^2= ( Ri - Cameralength * Tan(2θ)^2 *Sin(ψ) / pixelSize )^2　
        //が最小になるような点Bとψをさがす

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
                        //現在のTau,X,Yから予想される半径Rの円の中心位置は
                        double IdealX, IdealY;
                        double TwoTheta = Math.Atan2(Radius[i], CameraLength);
                        //Rはtauが正のとき正、負のとき負
                        //double R = CameraLength * 2 * Math.Sin(TwoTheta) * Math.Sin(TwoTheta) * Math.Sin(tau1) / (Math.Cos(2 * TwoTheta) + Math.Cos(2 * tau1));
                        //1/2 CL tan2q { sinj [tan(2q+j) -tan(2q-j)] }
                        double R = 0.5 * CameraLength * Math.Sin(TwoTheta) * (1 / Math.Cos(TwoTheta + tau1) - 1 / Math.Cos(TwoTheta - tau1));
                        if (tau1 > 0)
                            R = Math.Abs(R);
                        else
                            R = -Math.Abs(R);

                        IdealX = R * CosPhi1;
                        IdealY = R * SinPhi1;

                        if (xMode && tau1 * IdealX <= 0)//横方向に広がっていて tau1 > 0 かつ idealX <0  あるいは tau1 < 0 かつ idealX > 0 のときは
                        {
                            IdealX = -IdealX;
                            IdealY = -IdealY;
                        }
                        if (!xMode && tau1 * IdealY <= 0)//縦方向に広がっていて tau1 > 0 かつ idealY <0  あるいは tau1 < 0 かつ idealY > 0 のときは
                        {
                            IdealX = -IdealX;
                            IdealY = -IdealY;
                        }
                        //ここまでで、Tau すなわち Rが正のときは楕円の中心もX,Yのいずれかの方向に正に振れる事になる
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
        }//最適化終了

        offset = new PointD(BestX, BestY);

        //無条件に270°引いて
        phi1 -= 9 * Math.PI / 2;

        //xModeが真かつtauが正のとき. このとき回転軸は-135°～-45°になるべき
        if (xMode && bestTau1 >= 0)
            while (phi1 < -Math.PI * 3 / 4 - 0.01)//-135°～-45°の範囲じゃないときは
                phi1 += Math.PI;
        //xModeが真かつtauが負のとき. このとき回転軸は45°～135°になるべき
        if (xMode && bestTau1 < 0)
            while (phi1 < Math.PI / 4 - 0.01)//45°以下のときは
                phi1 += Math.PI;
        //xModeが偽かつtauが正のとき. このとき回転軸は-45°～45°になるべき
        if (!xMode && bestTau1 >= 0)
            while (phi1 < -Math.PI / 4 - 0.01)//-45°～45°の範囲じゃないときは
                phi1 += Math.PI;
        //xModeが偽かつtauが負のとき.このとき回転軸は135°～215°になるべき
        if (!xMode && bestTau1 < 0)
            while (phi1 < Math.PI * 3 / 4 - 0.01)//135°～215°の範囲じゃないときは
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
    /// 楕円の方程式から、その楕円群をもっとも真円に近づけるパラメータ(PixX, PixY, Ksi)を求める
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
            //RがRになるように調節する
            ellipse[i].Coeff[0] *= ellipse[i].millimeterCalc * ellipse[i].millimeterCalc / 1000000;
            ellipse[i].Coeff[1] *= ellipse[i].millimeterCalc * ellipse[i].millimeterCalc / 1000000;
            ellipse[i].Coeff[2] *= ellipse[i].millimeterCalc * ellipse[i].millimeterCalc / 1000000;
        }

        //半径Rの円 X^2 + Y^2 = R^2　がアフィン変換 { { A, B} , { 0 , C } } という行列で変換されると
        //楕円   a * X^2 + b * X * Y + c * Y^2  = X^2 / A^2 - X * Y * 2B /A^2/C  + Y^2 * (A^2+B^2) /(A^2 C^2) = R^2 に変換される
        //このとき
        //A =  1 / √(a)
        //B = - b /√(a) /√(4ac-b^2)
        //C = 2√(a) /√(4ac-b^2)

        var tempPixX = new List<double>(ellipse.Length);
        var tempPixY = new List<double>(ellipse.Length);
        var tempKsi = new List<double>(ellipse.Length);
        double A, B, C;
        for (int i = 0; i < ellipse.Length; i++)
        {
            A = Math.Sqrt(1.0 / ellipse[i].Coeff[0]);
            B = -ellipse[i].Coeff[1] / Math.Sqrt(ellipse[i].Coeff[0] * (4 * ellipse[i].Coeff[0] * ellipse[i].Coeff[2] - ellipse[i].Coeff[1] * ellipse[i].Coeff[1]));
            C = 2 * Math.Sqrt(ellipse[i].Coeff[0] / (4 * ellipse[i].Coeff[0] * ellipse[i].Coeff[2] - ellipse[i].Coeff[1] * ellipse[i].Coeff[1]));
            //{ { A, B} , { 0 , C } } * {x, y} = { { PixX , PixY SinKsi } , { 0 , PixelY } } * {X,Y} // ただし {x,y}は真の円上の座標, {X,Y}はピクセル座標
            //だから　newPixX = PixX /A, newPixY = PixY /C, tan(newKsi) = C (tanKsi-B) /A
            tempPixX.Add(PixX / A);
            tempPixY.Add(PixY / C);
            tempKsi.Add(Math.Atan(C / A * (Math.Tan(Ksi) - B)));
        }

        PixX = tempPixX.Average();
        PixXDev = Statistics.Deviation(tempPixX.ToArray());
        PixY = tempPixY.Average();
        PixYDev = Statistics.Deviation(tempPixY.ToArray());
        if (distortion)
        {
            Ksi = tempKsi.Average();
            KsiDev = Statistics.Deviation(tempKsi.ToArray());
        }
    }


    /// <summary>
    /// 点pが、与えられた点(右回りか左回り)ptsで作られる多角形の内部に存在するかどうかを判定する。
    /// https://www.nttpc.co.jp/technology/number_algorithm.html Winding Number Algorithmの変形を参考にした。
    /// </summary>
    /// <param name="pts"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public static bool InsidePolygonalArea(List<PointD> pts, in PointD p) => InsidePolygonalArea(CollectionsMarshal.AsSpan(pts), in p);

    /// <summary>
    /// 点pが、与えられた点(右回りか左回り)ptsで作られる多角形の内部に存在するかどうかを判定する。
    /// https://www.nttpc.co.jp/technology/number_algorithm.html Winding Number Algorithmの変形を参考にした。
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
            // 上向きの辺、下向きの辺によって処理が分かれる。
            // 上向きの辺。点Pがy軸方向について、始点と終点の間にある。ただし、終点は含まない。(ルール1)
            if ((pts[i].Y <= p.Y) && (pts[i + 1].Y > p.Y))
            {
                // 辺は点pよりも右側にある。ただし、重ならない。(ルール4)
                // 辺が点pと同じ高さになる位置を特定し、その時のxの値と点pのxの値を比較する。
                var vt = (p.Y - pts[i].Y) / (pts[i + 1].Y - pts[i].Y);
                if (p.X < (pts[i].X + (vt * (pts[i + 1].X - pts[i].X))))
                    ++wn;  //ここが重要。上向きの辺と交差した場合は+1
            }
            // 下向きの辺。点Pがy軸方向について、始点と終点の間にある。ただし、始点は含まない。(ルール2)
            else if ((pts[i].Y > p.Y) && (pts[i + 1].Y <= p.Y))
            {
                // 辺は点pよりも右側にある。ただし、重ならない。(ルール4)
                // 辺が点pと同じ高さになる位置を特定し、その時のxの値と点pのxの値を比較する。
                var vt = (p.Y - pts[i].Y) / (pts[i + 1].Y - pts[i].Y);
                if (p.X < (pts[i].X + (vt * (pts[i + 1].X - pts[i].X))))
                    --wn;  //ここが重要。下向きの辺と交差した場合は-1
            }
            // ルール1,ルール2を確認することで、ルール3も確認できている。
        }

        //最後に、最後の点と最初の点を計算
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
    /// 与えられた点(右回りか左回り)で囲まれる面積を返す
    /// </summary>
    /// <param name="pt"></param>
    /// <returns></returns>
    public static double GetPolygonalArea(PointD[] pt) => GetPolygonalArea(pt.Select(p => (p.X, p.Y)).ToArray());

    /// <summary>
    /// 与えられた点(右回りか左回り)で囲まれる面積を返す
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
    /// 与えられた点pt(右回りか左回り)で構成される多角形と、ある直線(ax+by>c)で囲まれる多角形を返す  (ax+by＜c)の時は全ての係数の符号を逆転
    /// </summary>
    /// <param name="pt"></param>
    /// <returns></returns>
    public static (double X, double Y)[] GetPolygonDividedByLine((double X, double Y)[] pt, double a, double b, double c)
    {
        //ptが2点以下ならそのまま返す
        if (pt == null || pt.Length < 3) return pt;

        //まず与えられた直線の条件 (ax+by>cあるいはax+by<c)と各点を比べる

        //ptのx,y値を格納しさらにZに範囲内(1)か範囲外(0)かを記録する一時的な変数
        var ptAlpha = new List<(double X, double Y, bool Flag)>();
        bool flag1 = true, flag2 = true;
        for (int i = 0; i < pt.Length; i++)
        {
            (double X, double Y, bool Flag) v = (pt[i].X, pt[i].Y, true);
            if (a * pt[i].X + b * pt[i].Y < c)//範囲外であれば
            {
                v.Flag = false;
                flag1 = false;//一つでも範囲外のものがあればflag1はfalseになる
            }
            else
                flag2 = false;//一つでも範囲内のものがあればflag2はfalseになる

            ptAlpha.Add(v);
        }

        //範囲外点がなければそのままptを返す。
        if (flag1)
            return pt;

        //範囲外点がある場合

        //すべてが範囲外ならnullを返す
        if (flag2)
            return Array.Empty<(double X, double Y)>();

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

        return ptBeta.ToArray();
    }

    /// <summary>
    /// 平面において、直線 a x + b y = c　が 点pt1とpt2を結ぶ直線と交わる交点を返す
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    private static (double X, double Y) getCrossPoint((double X, double Y, bool Flag) p1, (double X, double Y, bool Flag) p2, double a, double b, double c)
    {
        //次の2つの方程式を満たすx, yを求めればよい
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
    /// 3次元において、平面 a x + b y + c z = d (法線ベクトル(a,b,c))と、点pt1とpt2を結ぶ直線と交わる交点を返す
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <param name="d"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    public static Vector3D GetCrossPoint(in double a, in double b, in double c, in double d, Vector3D p1, Vector3D p2) 
        => GetCrossPoint(a, b, c, d, new Vector3D(p1.X, p1.Y, p1.Z), new Vector3DBase(p2.X, p2.Y, p2.Z));

    /// <summary>
    /// 3次元において、平面 a x + b y + c z = d (法線ベクトル(a,b,c))と、点pt1とpt2を結ぶ直線と交わる交点を返す
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <param name="d"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    public static Vector3D GetCrossPoint(in double a, in double b, in double c, in double d, Vector3DBase p1, Vector3DBase p2)
    {
        //次の3つの方程式を満たすx, y, z を求めればよい (2020/02/04修正)
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

        return new Vector3D(x, y, z);
    }


    // <summary>
    /// 3次元において、平面 a x + b y + c z = d (法線ベクトル(a,b,c))と、点(0,0,0)とptを結ぶ直線と交わる交点を返す
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
        //次の3つの方程式を満たすx, y, z を求めればよい (2020/02/04修正)
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
    /// sourcePointsで指定されたラインプロファイルを、areaで指定された範囲内で切り取る。
    /// </summary>
    /// <param name="sourcePoints"></param>
    /// <param name="area"></param>
    /// <returns></returns>
    public static PointD[][] GetPointsWithinRectangle(IEnumerable<PointD> sourcePoints, RectangleD area)
    {
        var pt = sourcePoints.ToList();
        //まず、横軸の上限と下限をトリム
        var first = pt.FindIndex(p => p.X >= area.X) - 1;
        if (first > 0)
            pt.RemoveRange(0, first);
        var last = pt.FindLastIndex(p => p.X <= area.X + area.Width) + 2;
        if (last < pt.Count)
            pt.RemoveRange(last, pt.Count - last);

        if (pt.Max(p => p.Y) <= area.UpperY && pt.Min(pt => pt.Y) >= area.Y)
            return new[] { pt.ToArray() };
        else if (pt.Max(p => p.Y) <= area.Y || pt.Min(pt => pt.Y) >= area.UpperY)
            return new[] { Array.Empty<PointD>() };
        else
        {
            for (int i = 0; i < pt.Count - 1; i++)
            {
                if (!area.IsInsde(pt[i]) || !area.IsInsde(pt[i + 1])) //どちらかが範囲外の時
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
    /// GetPointsWithinRectangle()が呼び出す
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="rect"></param>
    /// <returns></returns>
    private static PointD[] getCrossPoint(PointD p1, PointD p2, RectangleD rect)
    {
        //方程式は y= a x + b
        var a = (p2.Y - p1.Y) / (p2.X - p1.X);
        var b = p2.Y - a * p2.X;

        if (rect.IsInsde(p1))//p1が範囲内にあるとき
        {
            if (rect.IsInsde(p2))//p1もp2が範囲内にあるとき
            {
                return null;
            }
            else//p1が範囲内にあってp2が範囲外のとき
            {
                if (double.IsInfinity(a))
                {
                    if (p2.Y > rect.UpperY)
                        return new[] { new PointD(p1.X, rect.UpperY) };
                    else
                        return new[] { new PointD(p1.X, rect.Y) };
                }
                //x=maxXとの交点は
                double c = a * rect.UpperX + b;

                if (c < rect.Y)
                    return new[] { new PointD((rect.Y - b) / a, rect.Y) };
                else if (c > rect.UpperY)
                    return new[] { new PointD((rect.UpperY - b) / a, rect.UpperY) };
                else
                    return new[] { new PointD(rect.UpperX, c) };
            }
        }
        else//p1が範囲外にあるとき
        {
            if (rect.IsInsde(p2))//p1が範囲外でp2が範囲内のとき
            {
                //方程式は y= a x + b
                if (double.IsInfinity(a))
                {
                    if (p1.Y > rect.UpperY)
                        return new[] { new PointD(p1.X, rect.UpperY) };
                    else
                        return new[] { new PointD(p1.X, rect.Y) };
                }
                //x=minXとの交点は
                double c = a * rect.X + b;

                if (c < rect.Y)
                    return new[] { new PointD((rect.Y - b) / a, rect.Y) };
                else if (c > rect.UpperY)
                    return new[] { new PointD((rect.UpperY - b) / a, rect.UpperY) };
                else
                    return new[] { new PointD(rect.X, c) };
            }
            else//p1もp2が範囲外のとき
            {
                if (double.IsInfinity(a)) //傾き無限大の時
                {
                    if (p1.X >= rect.X && p1.X <= rect.UpperX)//両者のXは範囲内だが、Yがそれぞれ上限と下限を超えている場合
                    {
                        if (p1.Y < rect.Y && rect.UpperY < p2.Y)
                            return new[] { new PointD(p1.X, rect.Y), new PointD(p1.X, rect.UpperY) };
                        else if (p2.Y < rect.Y && rect.UpperY < p1.Y)
                            return new[] { new PointD(p1.X, rect.UpperY), new PointD(p1.X, rect.Y) };
                    }
                    else
                        return null;
                }

                //4つの交点を求める

                var temp = new List<PointD>(new[] {
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
    ///  3次元において、平面 a x + b y + c z + d = 0 (法線ベクトル(a,b,c))と、点(x, y, z)との距離(絶対値)を返す
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
    /// 点集合から最小二乗法による平面パラメータdouble[]{a,b,c,d} (但し、平面方程式 a x + b y + c z + d = 0) を返す
    /// </summary>
    /// <param name="points"></param>
    /// <returns>double[]{a,b,c,d} (但し、平面方程式 a x + b y + c z + d = 0)</returns>
    public static double[] GetPlaneEquationFromPoints(IEnumerable<Vector3DBase> points)
    {
        //http://sysplan.nams.kyushu-u.ac.jp/gen/edu/Algorithms/PlaneFitting/index.html
        //pdfはCrystallograpy/資料フォルダ

        var ave = Vector3DBase.Average(points);
        var mtx = new DenseMatrix(points.Count(), 3);
        int n = 0;
        foreach (var p in points.Select(p => p - ave))
            mtx.SetRow(n++, p.ToDoublearray());

        var evd = (mtx.Transpose() * mtx).Evd(Symmetricity.Unknown);
        var index = evd.EigenValues.AbsoluteMinimumIndex();

        double a = evd.EigenVectors[0, index], b = evd.EigenVectors[1, index], c = evd.EigenVectors[2, index], d = -(a * ave.X + b * ave.Y + c * ave.Z);
        return new double[] { a, b, c, d };
    }

    /// <summary>
    /// 点集合から最小二乗法による平面パラメータdouble[]{a,b,c,d} (但し、平面方程式 a x + b y + c z + d = 0) を返す
    /// </summary>
    /// <param name="points"></param>
    /// <returns>double[]{a,b,c,d} (但し、平面方程式 a x + b y + c z + d = 0)</returns>
    public static double[] GetPlaneEquationFromPoints(IEnumerable<OpenTK.Vector3d> points)
    {
        //http://sysplan.nams.kyushu-u.ac.jp/gen/edu/Algorithms/PlaneFitting/index.html
        //pdfはCrystallograpy/資料フォルダ

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
        return new double[] { a, b, c, d };
    }


    /// <summary>
    /// 与えられた平面(double[4],  a x + b y + c z + d >= 0 )の集合で、空間が閉じるかどうかを判定
    /// </summary>
    /// <param name="prms"></param>
    /// <returns></returns>
    public static bool Enclosed(double[][] bounds)
    {
        var countList = new List<int>();

        for (int i = 0; i < bounds.Length; i++)
        {
            var n = GetClippedPolygon(i, bounds).Length;
            if (n >= 3)
                countList.Add(GetClippedPolygon(i, bounds).Length);
        }

        return countList.Count >= 4;
    }

    /// <summary>
    /// 境界面によって切り取られた多角形の頂点座標を求める.
    /// </summary>
    /// <param name="plane">切り取られる面 (double[4],  a x + b y + c z + d = 0) </param>
    /// <param name="bounds">境界面 (double[4],  a x + b y + c z + d >= 0 ) </param>
    /// <returns></returns>
    public static double[][] GetClippedPolygon(double[] plane, double[][] bounds)
    {
        var pts = new List<Vector3DBase>();
        for (int i = 0; i < bounds.Length; i++)
            for (int j = i + 1; j < bounds.Length; j++)
            {
                var mtx = new Matrix3D(plane[0], bounds[i][0], bounds[j][0], plane[1], bounds[i][1], bounds[j][1], plane[2], bounds[i][2], bounds[j][2]);
                if (Math.Abs(mtx.Determinant()) > 0.0000000001)
                {
                    var pt = mtx.Inverse() * new Vector3DBase(-plane[3], -bounds[i][3], -bounds[j][3]);
                    if (bounds.All(b => b[0] * pt.X + b[1] * pt.Y + b[2] * pt.Z + b[3] > -0.0000000001) && pts.All(p => (p - pt).Length2 > 0.0000000001))
                        pts.Add(pt);
                }
            }
        return pts.Select(p => p.ToDoublearray()).ToArray();
    }

    /// <summary>
    /// 境界面によって切り取られた多角形の頂点座標を求める.
    /// </summary>
    /// <param name="i">切り取られる面のインデックス</param>
    /// <param name="bounds">境界面 (ただしiは除く) (double[4],  a x + b y + c z + d >= 0 ) </param>
    /// <returns></returns>
    public static double[][] GetClippedPolygon(int i, double[][] bounds)
    {
        return GetClippedPolygon(bounds[i], bounds.Where((b, j) => i != j).ToArray());
    }

    /// <summary>
    /// ベクトルa1 => ベクトルb1 かつ ベクトルa2 => ベクトルb2に写すような回転行列を求める. a1,a2,b1,b2の長さは1でなくても構わない（関数中で規格化する）
    /// </summary>
    /// <param name="a1"></param>
    /// <param name="a2"></param>
    /// <param name="b1"></param>
    /// <param name="b2"></param>
    /// <returns></returns>
    public static Matrix3D GetRotation(Vector3DBase a1, Vector3DBase a2, Vector3DBase b1, Vector3DBase b2)
    {
        //まず規格化
        var v1 = new Vector3DBase(a1).Normarize();
        var v2 = new Vector3DBase(a2).Normarize();
        var w1 = new Vector3DBase(b1).Normarize();
        var w2 = new Vector3DBase(b2).Normarize();

        var v3 = Vector3DBase.VectorProduct(v1, v2);
        var w3 = Vector3DBase.VectorProduct(w1, w2);

        return new Matrix3D(w1, w2, w3) * new Matrix3D(v1, v2, v3).Inverse();
    }

    /// <summary>
    /// 円錐と平面との交点(断面座標系)の集合を得る。
    /// 円錐は頂点(0,0,0), 円錐半角(alpha), 円錐中心軸は(cosPhi*sinTau, -sinPhi*sinTau, cosTau)で定義される。
    /// 断面はZ=Lを満たし、左上の点がupperLeft(断面座標系)、右上の点がlowerRight(断面座標系)で定義される矩形平面である。
    /// 断面座標系とは、交点(X,Y,L)について、(X,Y)の部分のことである(断面の中心は(0,0,L)である))。
    /// </summary>
    /// <param name="alpha">円錐半角(alpha)</param>
    /// <param name="phi"> 円錐中心軸のパラメータ. 円錐中心軸方向は(cosPhi*sinTau, -sinPhi*sinTau, cosTau)で定義される</param>
    /// <param name="tau">円錐中心軸のパラメータ. 円錐中心軸方向は(cosPhi*sinTau, -sinPhi*sinTau, cosTau)で定義される</param>
    /// <param name="l">断面のパラメータ. 断面はZ=Lで定義される. </param>
    /// <param name="upperLeft">矩形平面の左上座標</param>
    /// <param name="lowerRight">矩形平面の右下座標</param>
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
            if (Math.Abs(P) < 1E-8)//放物線
            {
                var pts = new List<PointD>();
                var shift = new PointD(0, -l / Math.Tan(2 * tau));//平行移動
                for (double x = -maxWidth; x < maxWidth; x += maxWidth / 2000.0)
                    pts.Add(rot(new PointD(x, x * x * sinTau / cosTau / l / 2) + shift));
                tempResult.Add(pts);
            }
            else
            {
                var shift = new PointD(0, -l * sinTau * cosTau / (sinTau2 - cosAlpha2));//平行移動量
                if (P > 0)//楕円
                {
                    var pts = new List<PointD>();
                    for (double omega = 0; omega < Math.PI * 2.0000001; omega += Math.PI / 2000)
                        pts.Add(rot(new PointD(Math.Sin(omega + Math.PI / 2) / Psqrt, -Math.Cos(omega + Math.PI / 2) / Qsqrt) + shift));
                    tempResult.Add(pts);
                }
                else//双曲線
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

        //境界チェック
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
                if (flags[j] ^ flags[j + 1])//どちらか一方がtrueでもう一方がfalseの時
                {
                    // (Y2-Y1)x + (X1-X2)y = X1Y2-X2Y1という直線と、
                    // x = xMin, x=xMax, y=yMin, y=yMaxという４本の直線の交点を計算する
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
                    result.Add(new List<PointD>());
                    for (; j < pts.Count && flags[j]; j++)
                        result[^1].Add(pts[j]);
                }
            }
        }


        return result;
    }

}