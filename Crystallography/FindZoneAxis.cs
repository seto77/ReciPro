using System;
using System.Collections.Generic;

namespace Crystallography;

//構造体ZoneAxis
public struct ZoneAxis
{
    public Plane plane1, plane2, plane3;
    public int u, v, w;
    public double tilt1, tilt2;
    public double Theta;

    public Crystal Phase;

    public ZoneAxis(Crystal cry, Plane plane1, Plane plane2, double tilt1, double tilt2)
    {
        Phase = cry;

        this.plane1 = plane1;
        this.plane2 = plane2;
        Theta = cry.GetAnglePlanes(plane1.h, plane1.k, plane1.l, plane2.h, plane2.k, plane2.l);

        u = plane1.l * plane2.k - plane1.k * plane2.l;
        v = plane1.h * plane2.l - plane1.l * plane2.h;
        w = plane1.k * plane2.h - plane1.h * plane2.k;

        plane3 = new Plane();
        plane3.h = plane1.h - plane2.h;
        plane3.k = plane1.k - plane2.k;
        plane3.l = plane1.l - plane2.l;
        plane3.d = cry.GetLengthPlane(plane3.h, plane3.k, plane3.l);
        this.tilt1 = tilt1;
        this.tilt2 = tilt2;
    }

    public static bool operator ==(ZoneAxis za1, ZoneAxis za2)
    {
        if (za1.u == za2.u && za1.v == za2.v && za1.w == za2.w &&
            za1.plane1.h == za2.plane1.h && za1.plane1.k == za2.plane1.k && za1.plane1.l == za2.plane1.l &&
            za1.plane2.h == za2.plane2.h && za1.plane2.k == za2.plane2.k && za1.plane2.l == za2.plane2.l)
            return true;
        else
            return false;
    }

    public static bool operator !=(ZoneAxis za1, ZoneAxis za2)
    {
        if (za1.u == za2.u && za1.v == za2.v && za1.w == za2.w &&
            za1.plane1.h == za2.plane1.h && za1.plane1.k == za2.plane1.k && za1.plane1.l == za2.plane1.l &&
            za1.plane2.h == za2.plane2.h && za1.plane2.k == za2.plane2.k && za1.plane2.l == za2.plane2.l)
            return false;
        else
            return true;
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

//構造体
public struct ZoneAxes
{
    public ZoneAxis Za1, Za2, Za3;
    public double AngleBet12, AngleBet23, AngleBet31;
    public bool IsTwoPhoho;
}

public class FindZoneAxis
{
    //回折斑点のパターンから晶帯軸を探す 1枚の写真から Mode1　２辺と間の角度

    public static ZoneAxis[] GetZoneAxis(Crystal cry, PhotoInformation photo, bool excludeEquivalence)
    {
        List<ZoneAxis> za = new List<ZoneAxis>();
        if (!photo.Paintable) return za.ToArray();

        cry.SetPlanes(
           double.MaxValue, photo.IsTriangleMode ? Math.Min(Math.Min(photo.d1min, photo.d2min), photo.d3min) : Math.Min(photo.d1min, photo.d2min),
            false, false, false, false, 0, 0, 0);

        Plane[] plane = cry.Plane.ToArray();
        for (int n1 = 0; n1 < plane.Length; n1++)
            if (!excludeEquivalence || plane[n1].IsRootIndex)
                if (plane[n1].d > photo.d1min && plane[n1].d < photo.d1max)
                    for (int n2 = 0; n2 < plane.Length; n2++)
                        if (plane[n2].d > photo.d2min && plane[n2].d < photo.d2max)
                        {
                            ZoneAxis tempZoneAxis = new ZoneAxis(cry, plane[n1], plane[n2], photo.Tilt1, photo.Tilt2);
                            if ((photo.IsTriangleMode && tempZoneAxis.plane3.d > photo.d3min && tempZoneAxis.plane3.d < photo.d3max)
                                || (!photo.IsTriangleMode && tempZoneAxis.Theta < photo.Theta + photo.theta_err && tempZoneAxis.Theta > photo.Theta - photo.theta_err))
                            {
                                for (int z = 2; z <= Math.Abs(tempZoneAxis.u) || z <= Math.Abs(tempZoneAxis.v) || z <= Math.Abs(tempZoneAxis.w); z++)//最大公約数で割る
                                    if ((tempZoneAxis.u % z == 0) && (tempZoneAxis.v % z == 0) && (tempZoneAxis.w % z == 0))
                                    {
                                        tempZoneAxis.u /= z; tempZoneAxis.v /= z; tempZoneAxis.w /= z; z = 1;
                                    }
                                za.Add(tempZoneAxis);
                            }
                        }
        //excludeEquivalence==true のとき、zaの中で等価なものを削除する
        if (excludeEquivalence)
            for (int i = 0; i < za.Count; i++)
                for (int j = i + 1; j < za.Count; j++)
                    if (SymmetryStatic.CheckEquivalentAxes(za[i].u, za[i].v, za[i].w, za[j].u, za[j].v, za[j].w, cry.Symmetry))
                        if (za[i].plane1.h == za[j].plane1.h && za[i].plane1.k == za[j].plane1.k && za[i].plane1.l == za[j].plane1.l)
                            if (SymmetryStatic.CheckEquivalentPlanes((za[i].plane2.h, za[i].plane2.k, za[i].plane2.l), (za[j].plane2.h, za[j].plane2.k, za[j].plane2.l), cry.Symmetry))
                            {
                                za.RemoveAt(j);
                                j--;
                            }
        return za.ToArray();
    }

    public static ZoneAxes[] ZoneAxisFromTwoZoneAxis(Crystal cry, PhotoInformation photo1, PhotoInformation photo2, bool excludeEquivalence)
    {
        //ここからホルダー間の角度上限下限をきめる
        double angleMax = 0, angleMin = 0;
        int sign1 = Math.Sign(photo1.Tilt1 - photo2.Tilt1);
        int sign2 = Math.Sign(photo1.Tilt2 - photo2.Tilt2);
        if (sign1 == 0) sign1 = 1;
        if (sign2 == 0) sign2 = 1;

        angleMax = GetAngleBetweenHolders(
            photo1.Tilt1 + sign1 * photo1.Tilt1Err, photo1.Tilt2 + sign2 * photo1.Tilt2Err,
            photo2.Tilt1 - sign1 * photo2.Tilt1Err, photo2.Tilt2 - sign2 * photo2.Tilt2Err);

        double p1t1, p1t2, p2t1, p2t2;
        if (Math.Sign((photo1.Tilt1 - sign1 * photo1.Tilt1Err) - (photo2.Tilt1 + sign1 * photo2.Tilt1Err)) != sign1)
            p1t1 = p2t1 = (photo1.Tilt1 + photo2.Tilt1) / 2;
        else
        {
            p1t1 = photo1.Tilt1 - sign1 * photo1.Tilt1Err;
            p2t1 = photo2.Tilt1 + sign1 * photo2.Tilt1Err;
        }
        if (Math.Sign((photo1.Tilt2 - sign2 * photo1.Tilt2Err) - (photo2.Tilt2 + sign2 * photo2.Tilt2Err)) != sign2)
            p1t2 = p2t2 = (photo1.Tilt2 + photo2.Tilt2) / 2;
        else
        {
            p1t2 = photo1.Tilt2 - sign2 * photo1.Tilt2Err;
            p2t2 = photo2.Tilt2 + sign2 * photo2.Tilt2Err;
        }
        angleMin = GetAngleBetweenHolders(p1t1, p1t2, p2t1, p2t2);

        //ここから条件にある軸のペアを検索
        List<ZoneAxes> zoneAxes = new List<ZoneAxes>();
        ZoneAxis[] zoneAxis1 = GetZoneAxis(cry, photo1, excludeEquivalence);
        ZoneAxis[] zoneAxis2 = GetZoneAxis(cry, photo2, false);
        for (int n = 0; n < zoneAxis1.Length; n++)
            //if (zoneAxis1[n].plane1.IsRootIndex)
            for (int m = 0; m < zoneAxis2.Length; m++)
            {
                var v1 = zoneAxis1[n].u * cry.A_Axis + zoneAxis1[n].v * cry.B_Axis + zoneAxis1[n].w * cry.C_Axis;
                var v2 = zoneAxis2[m].u * cry.A_Axis + zoneAxis2[m].v * cry.B_Axis + zoneAxis2[m].w * cry.C_Axis;
                double calcAngle = Vector3D.AngleBetVectors(v1, v2);
                if (angleMax >= calcAngle && angleMin <= calcAngle)
                {
                    ZoneAxes temp = new ZoneAxes();
                    temp.Za1 = zoneAxis1[n];
                    temp.Za2 = zoneAxis2[m];
                    temp.AngleBet12 = calcAngle;
                    temp.IsTwoPhoho = true;
                    zoneAxes.Add(temp);
                }
            }
        //excludeEquivalence==true のとき、zoneAxesの中で等価なものを削除する
        if (excludeEquivalence)
            for (int i = 0; i < zoneAxes.Count; i++)
                for (int j = i + 1; j < zoneAxes.Count; j++)
                    if (zoneAxes[i].Za1 == zoneAxes[j].Za1)
                    {
                        var plane1 = zoneAxes[i].Za2.plane1;
                        var plane2 = zoneAxes[i].Za2.plane2;
                        var result1 =  SymmetryStatic.IsRootPlane((plane1.h, plane1.k, plane1.l), cry.Symmetry, out var indices1);
                        var result2 = SymmetryStatic.IsRootPlane((plane2.h, plane2.k, plane2.l), cry.Symmetry, out var indices2);

                        if (indices1.Contains((zoneAxes[j].Za2.plane1.h, zoneAxes[j].Za2.plane1.k, zoneAxes[j].Za2.plane1.l))
                          && indices2.Contains((zoneAxes[j].Za2.plane2.h, zoneAxes[j].Za2.plane2.k, zoneAxes[j].Za2.plane2.l)))
                        {
                            zoneAxes.RemoveAt(j);
                            j--;
                        }
                    }

        return [.. zoneAxes];
    }

    public static ZoneAxes[] ZoneAxisFromThreeZoneAxis(Crystal cry, PhotoInformation photo1, PhotoInformation photo2, PhotoInformation photo3, bool excludeEquivalence)
    {
        var zoneAxes = new List<ZoneAxes>();
        var z12 = ZoneAxisFromTwoZoneAxis(cry, photo1, photo2, true);
        var z23 = ZoneAxisFromTwoZoneAxis(cry, photo2, photo3, false);
        var z31 = ZoneAxisFromTwoZoneAxis(cry, photo3, photo1, false);
        for (int i = 0; i < z12.Length; i++)
            for (int j = 0; j < z23.Length; j++)
                if (z12[i].Za2 == z23[j].Za1)
                    for (int k = 0; k < z31.Length; k++)
                        if (z23[j].Za2 == z31[k].Za1 && z31[k].Za2 == z12[i].Za1)
                        {
                            var temp = new ZoneAxes();
                            temp.Za1 = z12[i].Za1;
                            temp.Za2 = z23[j].Za1;
                            temp.Za3 = z31[k].Za1;
                            temp.AngleBet12 = z12[i].AngleBet12;
                            temp.AngleBet23 = z23[j].AngleBet12;
                            temp.AngleBet31 = z31[k].AngleBet12;
                            temp.IsTwoPhoho = false;
                            zoneAxes.Add(temp);
                        }
        for (int i = 0; i < zoneAxes.Count; i++)
            for (int j = i + 1; j < zoneAxes.Count; j++)
                if (zoneAxes[i].Za1 == zoneAxes[j].Za1 && zoneAxes[i].Za2 == zoneAxes[j].Za2)
                {
                    var plane1 = zoneAxes[i].Za3.plane1;
                    var plane2 = zoneAxes[i].Za3.plane2;
                   var result1  = SymmetryStatic.IsRootPlane((plane1.h, plane1.k, plane1.l), cry.Symmetry, out var indices1);
                    var result2 = SymmetryStatic.IsRootPlane((plane2.h, plane2.k, plane2.l), cry.Symmetry, out var indices2);

                    if (indices1.Contains((zoneAxes[j].Za3.plane1.h, zoneAxes[j].Za3.plane1.k, zoneAxes[j].Za3.plane1.l))
                      && indices2.Contains((zoneAxes[j].Za3.plane2.h, zoneAxes[j].Za3.plane2.k, zoneAxes[j].Za3.plane2.l)))
                    {
                        zoneAxes.RemoveAt(j);
                        j--;
                    }
                }
        return zoneAxes.ToArray();
    }

    public static double GetAngleBetweenHolders(double p1t1, double p1t2, double p2t1, double p2t2)
    {
        var v1 = new Vector3D(Math.Sin(p1t1), Math.Cos(p1t1) * Math.Sin(p1t2), Math.Cos(p1t1) * Math.Cos(p1t2));
        var v2 = new Vector3D(Math.Sin(p2t1), Math.Cos(p2t1) * Math.Sin(p2t2), Math.Cos(p2t1) * Math.Cos(p2t2));
        return Vector3D.AngleBetVectors(v1, v2);
    }
}