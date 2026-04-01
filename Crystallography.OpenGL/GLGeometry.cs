using System;
using System.Collections.Generic;
using M3d = OpenTK.Mathematics.Matrix3d;
using V2d = OpenTK.Mathematics.Vector2d;
using V3d = OpenTK.Mathematics.Vector3d;

namespace Crystallography.OpenGL;

public static class GLGeometry
{
    /// <summary>
    /// 平面上に分布する点集合が与えられたとき、その点集合が作る多角形の情報を返す
    /// 1. 頂点index (originから見て反時計回り)
    /// 2. 法線ベクトル
    /// 3. 中心座標
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public static (List<uint> Indices, V3d Center, V3d Norm) PolygonInfo(IEnumerable<V3d> _points, in V3d origin)
    {
        ArgumentNullException.ThrowIfNull(_points);

        // var points = _points.AsValueEnumerable();
        var points = _points as V3d[] ?? [.. _points]; // (260320Ch) IEnumerable の多重列挙を避けるため最初に一度だけ配列化する
        if (points.Length == 3)
            return (new List<uint>([0, 1, 2, 0]), (points[0] + points[1] + points[2]) / 3, V3d.Cross(points[1] - points[0], points[2] - points[1]));

        var center = V3d.Zero;
        foreach (var point in points)
            center += point;
        center /= points.Length; // (260320Ch) 追加列挙を避けながら中心座標を計算する

        //var prm = Geometriy.GetPlaneEquationFromPoints(points.Select(p => p.ToVector3DBase()));
        var (A, B, C, _) = Geometry.GetPlaneEquationFromPoints(points);
        var norm = new V3d(A, B, C);
        if (V3d.Dot(norm, center - origin) < 0)
            norm = -norm;

        //座標変換 (XY平面に投影)
        var rot = CreateRotationToZ(norm);
        var projectedPoints = new V2d[points.Length];
        var maxLengthSquared = double.NegativeInfinity;
        var i = 0;
        for (var index = 0; index < points.Length; index++)
        {
            var point = points[index] - center;
            var projectedPoint = new V2d(
                rot.M11 * point.X + rot.M12 * point.Y + rot.M13 * point.Z,
                rot.M21 * point.X + rot.M22 * point.Y + rot.M23 * point.Z);
            projectedPoints[index] = projectedPoint;

            var lengthSquared = projectedPoint.LengthSquared;
            if (lengthSquared > maxLengthSquared)
            {
                maxLengthSquared = lengthSquared;
                i = index;
            }
        }

        //もう一つ点を選び、直線の方程式を産出
        var iList = new List<uint>(points.Length + 1) { (uint)i };
        do
        {
            var nextIndex = -1;
            for (var j = 0; j < projectedPoints.Length; j++)
                if (j != i)
                {
                    var v = new V2d(projectedPoints[j].Y - projectedPoints[i].Y, projectedPoints[i].X - projectedPoints[j].X);
                    var c = projectedPoints[j].X * projectedPoints[i].Y - projectedPoints[i].X * projectedPoints[j].Y;

                    var isHullEdge = true;
                    foreach (var point in projectedPoints)
                    {
                        if (V2d.Dot(point, v) + c > Th)
                        {
                            isHullEdge = false;
                            break;
                        }
                    }

                    if (isHullEdge)
                    {
                        nextIndex = j;
                        break;
                    }
                }

            if (nextIndex < 0)
                break; // (260320Ch) 退化ケースで候補が見つからないときに無限ループへ入らないよう打ち切る

            iList.Add((uint)nextIndex);
            i = nextIndex;
        } while (i != iList[0] && iList.Count <= points.Length);

        return (iList, center, norm);
    }

    /// <summary>Z軸(001)を引数のベクトルvに回転させる行列を生成する</summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static M3d CreateRotationFromZ(V3d v)
    {
        v.Normalize();
        if (Math.Abs(v.Z - 1) < Th)
            return M3d.Identity;
        else if (Math.Abs(v.Z + 1) < Th)
            return M3d.CreateRotationX(Math.PI);
        else
            return M3d.CreateFromAxisAngle(V3d.Cross(v, Z), V3d.CalculateAngle(Z, v));
    }

    /// <summary>Z軸(001)を引数のベクトルvに回転させる行列を生成する</summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static M3d CreateRotationToZ(V3d v)
    {
        v.Normalize();
        if (Math.Abs(v.Z - 1) < Th)
            return M3d.Identity;
        else if (Math.Abs(v.Z + 1) < Th)
            return M3d.CreateRotationX(Math.PI);
        else
            return M3d.CreateFromAxisAngle(V3d.Cross(Z, v), V3d.CalculateAngle(Z, v));
    }

    public static readonly V3d Z = new(0, 0, 1);
    public const double Th = 0.0000001;
}
