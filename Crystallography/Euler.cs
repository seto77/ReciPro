using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using DMat = MathNet.Numerics.LinearAlgebra.Double.DenseMatrix;
using DVec = MathNet.Numerics.LinearAlgebra.Double.DenseVector;

namespace Crystallography;

/// <summary>
/// Euler の概要の説明です。
/// </summary>
public class Euler
{
    public Euler()
    {
        //
        // TODO: コンストラクタ ロジックをここに追加してください。
        //
    }

    //zaがtilt,azimuthに一致するようなオイラー角を返す
    public static Matrix3D SerchEulerAngleFromZoneAxes(ZoneAxis za, Crystal cry)
    {
        double tilt1 = za.tilt1;
        double tilt2 = za.tilt2;
        Vector3D v = Vector3D.Normarize(za.u * cry.A_Axis + za.v * cry.B_Axis + za.w * cry.C_Axis);

        Vector3D V = new(-Math.Sin(tilt1), -Math.Cos(tilt1) * Math.Sin(tilt2), Math.Cos(tilt1) * Math.Cos(tilt2));

        double Phi, phi1, phi2, PhiStart, PhiEnd, phi1Start, phi1End, phi2Start, phi2End, step, PhiBest, phi1Best, phi2Best;
        double dev, devTemp;
        PhiStart = phi1Start = phi2Start = -Math.PI;
        PhiEnd = phi1End = phi2End = Math.PI * 1.001;
        Matrix3D m;
        step = Math.PI / 36.0;
        dev = double.PositiveInfinity;
        PhiBest = double.PositiveInfinity;
        phi1Best = double.PositiveInfinity;
        phi2Best = double.PositiveInfinity;

        double cosP, sinP, cosP1, sinP1, cosP2, sinP2;

        for (int n = 0; n < 15; n++)
        {
            for (Phi = PhiStart; Phi <= PhiEnd; Phi += step)
                for (phi1 = phi1Start; phi1 <= phi1End; phi1 += step)
                    for (phi2 = phi2Start; phi2 <= phi2End; phi2 += step)
                    {
                        cosP = Math.Cos(Phi);
                        sinP = Math.Sin(Phi);
                        cosP1 = Math.Cos(phi1);
                        sinP1 = Math.Sin(phi1);
                        cosP2 = Math.Cos(phi2);
                        sinP2 = Math.Sin(phi2);
                        m = new Matrix3D(
                            cosP2 * cosP1 - cosP * sinP1 * sinP2, -sinP2 * cosP1 - cosP * sinP1 * cosP2, sinP * sinP1,
                            cosP2 * sinP1 + cosP * cosP1 * sinP2, -sinP2 * sinP1 + cosP * cosP1 * cosP2, -sinP * cosP1,
                            sinP2 * sinP, cosP2 * sinP, cosP
                            );
                        devTemp = (m * v - V).Length2;
                        if (dev > devTemp)
                        {
                            dev = devTemp;
                            PhiBest = Phi;
                            phi1Best = phi1;
                            phi2Best = phi2;
                        }
                    }
            if (double.IsInfinity(PhiBest)) break;
            PhiStart = PhiBest - 1.5 * step;
            PhiEnd = PhiBest + 1.5 * step;
            phi1Start = phi1Best - 1.5 * step;
            phi1End = phi1Best + 1.5 * step;
            phi2Start = phi2Best - 1.5 * step;
            phi2End = phi2Best + 1.5 * step;
            step *= 0.4;
        }

        if (phi1Best < -Math.PI) phi1Best += 2 * Math.PI;
        if (phi1Best > Math.PI) phi1Best -= 2 * Math.PI;

        if (phi2Best < -Math.PI) phi2Best += 2 * Math.PI;
        if (phi2Best > Math.PI) phi2Best -= 2 * Math.PI;

        if (PhiBest < -Math.PI) PhiBest += 2 * Math.PI;
        if (PhiBest > Math.PI) PhiBest -= 2 * Math.PI;

        if (PhiBest < 0)
        {
            PhiBest = -PhiBest;

            if (phi1Best < 0) phi1Best += Math.PI;
            else phi1Best -= Math.PI;

            if (phi2Best < 0) phi2Best += Math.PI;
            else phi2Best -= Math.PI;
        }

        cosP = Math.Cos(PhiBest);
        sinP = Math.Sin(PhiBest);
        cosP1 = Math.Cos(phi1Best);
        sinP1 = Math.Sin(phi1Best);
        cosP2 = Math.Cos(phi2Best);
        sinP2 = Math.Sin(phi2Best);
        return new Matrix3D(
                            cosP2 * cosP1 - cosP * sinP1 * sinP2, -sinP2 * cosP1 - cosP * sinP1 * cosP2, sinP * sinP1,
                            cosP2 * sinP1 + cosP * cosP1 * sinP2, -sinP2 * sinP1 + cosP * cosP1 * cosP2, -sinP * cosP1,
                            sinP2 * sinP, cosP2 * sinP, cosP
                            );
    }

    //za1がtilt1,azimuth1に、za2がtilt2,azimuth2になるべく一致するようなオイラー角を返す
    public static Matrix3D SerchEulerAngleFromZoneAxes(ZoneAxis za1, ZoneAxis za2, Crystal cry)
    {
        double tilt1 = za1.tilt1;
        double azimuth1 = za1.tilt2;
        double tilt2 = za2.tilt1;
        double azimuth2 = za2.tilt2;

        var v1 = Vector3D.Normarize(za1.u * cry.A_Axis + za1.v * cry.B_Axis + za1.w * cry.C_Axis);
        var  v2 = Vector3D.Normarize(za2.u * cry.A_Axis + za2.v * cry.B_Axis + za2.w * cry.C_Axis);

        var  V1 = new Vector3D(-Math.Sin(tilt1), -Math.Cos(tilt1) * Math.Sin(azimuth1), Math.Cos(tilt1) * Math.Cos(azimuth1));
        var V2 = new Vector3D(-Math.Sin(tilt2), -Math.Cos(tilt2) * Math.Sin(azimuth2), Math.Cos(tilt2) * Math.Cos(azimuth2));

        double Phi, phi1, phi2, PhiStart, PhiEnd, phi1Start, phi1End, phi2Start, phi2End, step, PhiBest, phi1Best, phi2Best;
        double dev, devTemp;
        PhiStart = phi1Start = phi2Start = -Math.PI;
        PhiEnd = phi1End = phi2End = Math.PI * 1.001;
        Matrix3D m;
        step = Math.PI / 36.0;
        dev = double.PositiveInfinity;
        PhiBest = double.PositiveInfinity;
        phi1Best = double.PositiveInfinity;
        phi2Best = double.PositiveInfinity;

        double cosP, sinP, cosP1, sinP1, cosP2, sinP2;

        for (int n = 0; n < 15; n++)
        {
            for (Phi = PhiStart; Phi <= PhiEnd; Phi += step)
                for (phi1 = phi1Start; phi1 <= phi1End; phi1 += step)
                    for (phi2 = phi2Start; phi2 <= phi2End; phi2 += step)
                    {
                        cosP = Math.Cos(Phi);
                        sinP = Math.Sin(Phi);
                        cosP1 = Math.Cos(phi1);
                        sinP1 = Math.Sin(phi1);
                        cosP2 = Math.Cos(phi2);
                        sinP2 = Math.Sin(phi2);
                        m = new Matrix3D(
                            cosP2 * cosP1 - cosP * sinP1 * sinP2, -sinP2 * cosP1 - cosP * sinP1 * cosP2, sinP * sinP1,
                            cosP2 * sinP1 + cosP * cosP1 * sinP2, -sinP2 * sinP1 + cosP * cosP1 * cosP2, -sinP * cosP1,
                            sinP2 * sinP, cosP2 * sinP, cosP
                            );
                        devTemp = (m * v1 - V1).Length2 + (m * v2 - V2).Length2;
                        if (dev > devTemp)
                        {
                            dev = devTemp;
                            PhiBest = Phi;
                            phi1Best = phi1;
                            phi2Best = phi2;
                        }
                    }
            if (double.IsInfinity(PhiBest)) break;
            PhiStart = PhiBest - 1.5 * step;
            PhiEnd = PhiBest + 1.5 * step;
            phi1Start = phi1Best - 1.5 * step;
            phi1End = phi1Best + 1.5 * step;
            phi2Start = phi2Best - 1.5 * step;
            phi2End = phi2Best + 1.5 * step;
            step *= 0.4;
        }

        if (phi1Best < -Math.PI) phi1Best += 2 * Math.PI;
        if (phi1Best > Math.PI) phi1Best -= 2 * Math.PI;

        if (phi2Best < -Math.PI) phi2Best += 2 * Math.PI;
        if (phi2Best > Math.PI) phi2Best -= 2 * Math.PI;

        if (PhiBest < -Math.PI) PhiBest += 2 * Math.PI;
        if (PhiBest > Math.PI) PhiBest -= 2 * Math.PI;

        if (PhiBest < 0)
        {
            PhiBest = -PhiBest;

            if (phi1Best < 0) phi1Best += Math.PI;
            else phi1Best -= Math.PI;

            if (phi2Best < 0) phi2Best += Math.PI;
            else phi2Best -= Math.PI;
        }

        cosP = Math.Cos(PhiBest);
        sinP = Math.Sin(PhiBest);
        cosP1 = Math.Cos(phi1Best);
        sinP1 = Math.Sin(phi1Best);
        cosP2 = Math.Cos(phi2Best);
        sinP2 = Math.Sin(phi2Best);
        return new Matrix3D(
                            cosP2 * cosP1 - cosP * sinP1 * sinP2, -sinP2 * cosP1 - cosP * sinP1 * cosP2, sinP * sinP1,
                            cosP2 * sinP1 + cosP * cosP1 * sinP2, -sinP2 * sinP1 + cosP * cosP1 * cosP2, -sinP * cosP1,
                            sinP2 * sinP, cosP2 * sinP, cosP
                            );
    }

    //za1がtilt1,azimuth1に、za2がtilt2,azimuth2に、za3がなるべくtilt3,azimuth3に一致するようなオイラー角を返す
    public static Matrix3D SerchEulerAngleFromZoneAxes(ZoneAxis za1, ZoneAxis za2, ZoneAxis za3, Crystal cry)
    {
        double tilt1 = za1.tilt1;
        double azimuth1 = za1.tilt2;
        double tilt2 = za2.tilt1;
        double azimuth2 = za2.tilt2;
        double tilt3 = za3.tilt1;
        double azimuth3 = za3.tilt2;

        Vector3D v1 = Vector3D.Normarize(za1.u * cry.A_Axis + za1.v * cry.B_Axis + za1.w * cry.C_Axis);
        Vector3D v2 = Vector3D.Normarize(za2.u * cry.A_Axis + za2.v * cry.B_Axis + za2.w * cry.C_Axis);
        Vector3D v3 = Vector3D.Normarize(za3.u * cry.A_Axis + za3.v * cry.B_Axis + za3.w * cry.C_Axis);

        Vector3D V1 = new Vector3D(-Math.Sin(tilt1), -Math.Cos(tilt1) * Math.Sin(azimuth1), Math.Cos(tilt1) * Math.Cos(azimuth1));
        Vector3D V2 = new Vector3D(-Math.Sin(tilt2), -Math.Cos(tilt2) * Math.Sin(azimuth2), Math.Cos(tilt2) * Math.Cos(azimuth2));
        Vector3D V3 = new Vector3D(-Math.Sin(tilt3), -Math.Cos(tilt3) * Math.Sin(azimuth3), Math.Cos(tilt3) * Math.Cos(azimuth3));

        double Phi, phi1, phi2, PhiStart, PhiEnd, phi1Start, phi1End, phi2Start, phi2End, step, PhiBest, phi1Best, phi2Best;
        double dev, devTemp;
        PhiStart = phi1Start = phi2Start = -Math.PI;
        PhiEnd = phi1End = phi2End = Math.PI * 1.001;
        Matrix3D m;
        step = Math.PI / 36.0;
        dev = double.PositiveInfinity;
        PhiBest = double.PositiveInfinity;
        phi1Best = double.PositiveInfinity;
        phi2Best = double.PositiveInfinity;

        double cosP, sinP, cosP1, sinP1, cosP2, sinP2;

        for (int n = 0; n < 25; n++)
        {
            for (Phi = PhiStart; Phi <= PhiEnd; Phi += step)
                for (phi1 = phi1Start; phi1 <= phi1End; phi1 += step)
                    for (phi2 = phi2Start; phi2 <= phi2End; phi2 += step)
                    {
                        cosP = Math.Cos(Phi);
                        sinP = Math.Sin(Phi);
                        cosP1 = Math.Cos(phi1);
                        sinP1 = Math.Sin(phi1);
                        cosP2 = Math.Cos(phi2);
                        sinP2 = Math.Sin(phi2);
                        m = new Matrix3D(
                            cosP2 * cosP1 - cosP * sinP1 * sinP2, -sinP2 * cosP1 - cosP * sinP1 * cosP2, sinP * sinP1,
                            cosP2 * sinP1 + cosP * cosP1 * sinP2, -sinP2 * sinP1 + cosP * cosP1 * cosP2, -sinP * cosP1,
                            sinP2 * sinP, cosP2 * sinP, cosP
                            );
                        devTemp = (m * v1 - V1).Length2 + (m * v2 - V2).Length2 + (m * v3 - V3).Length2;
                        if (dev > devTemp)
                        {
                            dev = devTemp;
                            PhiBest = Phi;
                            phi1Best = phi1;
                            phi2Best = phi2;
                        }
                    }
            if (double.IsInfinity(PhiBest)) break;
            PhiStart = PhiBest - 2.5 * step;
            PhiEnd = PhiBest + 2.5 * step;
            phi1Start = phi1Best - 2.5 * step;
            phi1End = phi1Best + 2.5 * step;
            phi2Start = phi2Best - 2.5 * step;
            phi2End = phi2Best + 2.5 * step;
            step *= 0.4;
        }

        if (phi1Best < -Math.PI) phi1Best += 2 * Math.PI;
        if (phi1Best > Math.PI) phi1Best -= 2 * Math.PI;

        if (phi2Best < -Math.PI) phi2Best += 2 * Math.PI;
        if (phi2Best > Math.PI) phi2Best -= 2 * Math.PI;

        if (PhiBest < -Math.PI) PhiBest += 2 * Math.PI;
        if (PhiBest > Math.PI) PhiBest -= 2 * Math.PI;

        if (PhiBest < 0)
        {
            PhiBest = -PhiBest;

            if (phi1Best < 0) phi1Best += Math.PI;
            else phi1Best -= Math.PI;

            if (phi2Best < 0) phi2Best += Math.PI;
            else phi2Best -= Math.PI;
        }

        cosP = Math.Cos(PhiBest);
        sinP = Math.Sin(PhiBest);
        cosP1 = Math.Cos(phi1Best);
        sinP1 = Math.Sin(phi1Best);
        cosP2 = Math.Cos(phi2Best);
        sinP2 = Math.Sin(phi2Best);
        return new Matrix3D(
                            cosP2 * cosP1 - cosP * sinP1 * sinP2, -sinP2 * cosP1 - cosP * sinP1 * cosP2, sinP * sinP1,
                            cosP2 * sinP1 + cosP * cosP1 * sinP2, -sinP2 * sinP1 + cosP * cosP1 * cosP2, -sinP * cosP1,
                            sinP2 * sinP, cosP2 * sinP, cosP
                            );
    }

    //Euler角を返すメソッド
    public static (double Phi, double Theta, double Psi) GetEulerAngle(Matrix3D EulerMatrix)
    {
        if (EulerMatrix.E11 > 1) EulerMatrix.E11 = 1; if (EulerMatrix.E11 < -1) EulerMatrix.E11 = -1;
        if (EulerMatrix.E23 > 1) EulerMatrix.E23 = 1; if (EulerMatrix.E23 < -1) EulerMatrix.E23 = -1;
        if (EulerMatrix.E32 > 1) EulerMatrix.E32 = 1; if (EulerMatrix.E32 < -1) EulerMatrix.E32 = -1;
        if (EulerMatrix.E33 > 1) EulerMatrix.E33 = 1; if (EulerMatrix.E33 < -1) EulerMatrix.E33 = -1;

        double phi = 0, theta = Math.Acos(EulerMatrix.E33), psi = 0;
        double SinTheta = Math.Sqrt(1 - EulerMatrix.E33 * EulerMatrix.E33);
        if (SinTheta == 0)
        {
            psi = 0;
            if (EulerMatrix.E11 == 0 && EulerMatrix.E12 > 0)
                phi = Math.PI / 2.0;
            else if (EulerMatrix.E11 == 0 && EulerMatrix.E12 < 0)
                phi = -Math.PI / 2.0;
            else if (EulerMatrix.E11 == 1)
                phi = 0;
            else if (EulerMatrix.E11 == -1)
                phi = Math.PI;
            else
                phi = Math.Acos(EulerMatrix.E11);
            return (phi, theta, psi);
        }

        double CosPhi = -EulerMatrix.E23 / SinTheta;
        double CosPsi = EulerMatrix.E32 / SinTheta;

        if (CosPhi > 1) CosPhi = 1; if (CosPhi < -1) CosPhi = -1;
        if (CosPsi > 1) CosPsi = 1; if (CosPsi < -1) CosPsi = -1;
        phi = EulerMatrix.E13 > 0 ? Math.Acos(CosPhi) : -Math.Acos(CosPhi);
        psi = EulerMatrix.E31 > 0 ? Math.Acos(CosPsi) : -Math.Acos(CosPsi);

        return (phi, theta, psi);
    }

    public static Matrix3D SetEulerAngle(double phi, double theta, double psi)
    {
        double cosPhi = Math.Cos(phi);
        double sinPhi = Math.Sin(phi);
        double cosTheta = Math.Cos(theta);
        double sinTheta = Math.Sin(theta);
        double cosPsi = Math.Cos(psi);
        double sinPsi = Math.Sin(psi);

        return new Matrix3D(
            cosPhi * cosPsi - cosTheta * sinPhi * sinPsi,
            sinPhi * cosPsi + cosTheta * cosPhi * sinPsi,
            sinPsi * sinTheta,

            -cosPhi * sinPsi - cosTheta * sinPhi * cosPsi,
            -sinPhi * sinPsi + cosTheta * cosPhi * cosPsi,
            cosPsi * sinTheta,

            sinTheta * sinPhi,
            -sinTheta * cosPhi,
            cosTheta

            );
    }



    //public enum Setting { XYX, XYZ, XZX, XZY, YXY, YXZ, YZX, YZY, ZXY, ZXZ, ZYX, ZYZ}

    /// <summary>
    /// 任意のセッティングのオイラー角に分解する.  => MathNet.FindMinimumの方が簡単なので、お蔵入り
    /// </summary>
    /// <param name="rot"></param>
    /// <param name="setting"></param>
    /// <returns></returns>
    //public static double[] DecomposeMatrix(Matrix3D rot, Vector3DBase v1, Vector3DBase v2, Vector3DBase v3)
    //{
    //    Matrix3D funcRot(double[] angle) => Matrix3D.Rot(v1, angle[0]) * Matrix3D.Rot(v2, angle[1]) * Matrix3D.Rot(v3, angle[2]);


    //    var func = new Marquardt.Function(
    //        new Func<double[], double[], double>((x, prm) =>
    //        {
    //            var res = funcRot(prm);
    //            if (x[0] == 0.0) return res.E11;
    //            else if (x[0] == 1.0) return res.E12;
    //            else if (x[0] == 2.0) return res.E13;
    //            else if (x[0] == 3.0) return res.E21;
    //            else if (x[0] == 4.0) return res.E22;
    //            else if (x[0] == 5.0) return res.E23;
    //            else if (x[0] == 6.0) return res.E31;
    //            else if (x[0] == 7.0) return res.E32;
    //            else if (x[0] == 8.0) return res.E33;
    //            else return 0;
    //        }),
    //        new[] { 0.1, 0.2, 0.3 });


    //    var obsValues = new[] {
    //        (new[] { 0.0 }, rot.E11, 1.0),
    //        (new[] { 1.0 }, rot.E12, 1.0),
    //        (new[] { 2.0 }, rot.E13, 1.0),
    //        (new[] { 3.0 }, rot.E21, 1.0),
    //        (new[] { 4.0 }, rot.E22, 1.0),
    //        (new[] { 5.0 }, rot.E23, 1.0),
    //        (new[] { 6.0 }, rot.E31, 1.0),
    //        (new[] { 7.0 }, rot.E32, 1.0),
    //        (new[] { 8.0 }, rot.E33, 1.0)

    //    };

    //    var (Prms, Error, R) = Marquardt.Solve(obsValues, new[] { func },Marquardt.Precision.High);

    //    return Prms[0];
    //}

    /* public static double[] DecomposeMatrix(Matrix3D rot, Vector3d v1, Vector3d v2, Vector3d v3)
     {
         //return DecomposeMatrix(rot, new Vector3DBase(v1.X, v1.Y, v1.Z), new Vector3DBase(v2.X, v2.Y, v2.Z), new Vector3DBase(v3.X, v3.Y, v3.Z));
         return DecomposeMatrix2(rot, new Vector3DBase[] { new Vector3DBase(v1.X, v1.Y, v1.Z), new Vector3DBase(v2.X, v2.Y, v2.Z), new Vector3DBase(v3.X, v3.Y, v3.Z) });
     }
    */

    /// <summary>
    /// rotに最も近い、任意のセッティングのオイラー角に分解する.
    /// </summary>
    /// <param name="rot"></param>
    /// <param name="setting">settings配列の長さは最大で3.
    /// V: 回転軸、Angle: 初期(あるいは固定)角度、Variable: Trueで変数、Falseで固定</param>
    /// <returns>戻り値は、</returns>
    public static double[] DecomposeMatrix2(Matrix3D targetRotation, params (Vector3d Vec, double Angle, bool Variable)[] settings)
    {
        if (!settings.Any(s => s.Variable))
            return settings.Select(s => s.Angle).ToArray();

        var rotations = new List<object>();
        var initialAngles = new List<double>();
        foreach (var (Vec, Angle, Variable) in settings)
        {
            if (Variable)
            {
                rotations.Add(new Func<double, Matrix3D>(angle => Matrix3D.Rot(Vec, angle)));
                initialAngles.Add(Angle);
            }
            else
                rotations.Add(Matrix3D.Rot(Vec, Angle));
        }

        var rotInv = targetRotation.Inverse();

        var func = new Func<Vector<double>, double>(angles =>
        {
            var mat = new Matrix3D();
            var n = 0;
            foreach (var o in rotations)
                if (o is Matrix3D fixedRot)
                    mat *= fixedRot;
                else if (o is Func<double, Matrix3D> functionalRot)
                    mat *= functionalRot(angles[n++]);

            return -(mat * rotInv).SumOfDiagonalCompenent();
        });

        var temp = func(new DVec(initialAngles.ToArray()));
        try
        {
            var result = FindMinimum.OfFunction(func, new DVec(initialAngles.ToArray()), 1e-12, 100000).ToList();
            for (int i = 0; i < settings.Length; i++)
                if (!settings[i].Variable)
                    result.Insert(i, settings[i].Angle);
            return result.ToArray();
        }
        catch
        {
            return initialAngles.ToArray();
        }
    }

}
