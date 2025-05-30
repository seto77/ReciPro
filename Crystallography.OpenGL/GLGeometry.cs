﻿using System;
using System.Collections.Generic;
using System.Linq;
using M3d = OpenTK.Mathematics.Matrix3d;
using V2d = OpenTK.Mathematics.Vector2d;
using V3d = OpenTK.Mathematics.Vector3d;
using ZLinq;
using Windows.UI.ViewManagement.Core;
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
        if (_points.Count() == 3)
        {
            var pts = _points.ToArray();
            return (new List<uint>([0, 1, 2, 0]), (pts[0] + pts[1] + pts[2]) / 3, V3d.Cross(pts[1] - pts[0], pts[2] - pts[1]));
        }

        var points = _points.AsValueEnumerable();

       
        var center = TkEx.Average(points);
        //var prm = Geometriy.GetPlaneEquationFromPoints(points.Select(p => p.ToVector3DBase()));
        var (A, B, C, _) = Geometry.GetPlaneEquationFromPoints(points);
        var norm = new V3d(A, B, C);
        if (V3d.Dot(norm, center - origin) < 0)
            norm = -norm;

        //座標変換 (XY平面に投影)
        var rot = CreateRotationToZ(norm);
        var vXY = points.Select(p => p - center).Select(p => new V2d(rot.M11 * p.X + rot.M12 * p.Y + rot.M13 * p.Z, rot.M21 * p.X + rot.M22 * p.Y + rot.M23 * p.Z));
        //原点から最も距離の遠い点を選んで、iに格納
        var lengthSquaredArray = vXY.Select(p => p.LengthSquared);//.ToList();
        var maxLength = lengthSquaredArray.Max();
        var i = lengthSquaredArray.ToList().FindIndex(len => len == maxLength);

        using var vXY_pool = vXY.ToArrayPool();
        var vXY_Span = vXY_pool.Span;

        //もう一つ点を選び、直線の方程式を産出
        var iList = new List<uint>([(uint)i]);
        do
        {
            for (int j = 0; j < vXY_Span.Length; j++)
                if (j != i)
                {
                    var V = new V2d(vXY_Span[j].Y - vXY_Span[i].Y, vXY_Span[i].X - vXY_Span[j].X);
                    var c = vXY_Span[j].X * vXY_Span[i].Y - vXY_Span[i].X * vXY_Span[j].Y;

                    if (vXY.All(p => V2d.Dot(p, V) + c <= Th))
                    {
                        iList.Add((uint)j);
                        i = j;
                        break;
                    }
                }
        } while (i != iList[0] && iList.Count <= points.Count());

        return (iList, center, norm);
    }

    /// <summary>
    /// Z軸(001)を引数のベクトルvに回転させる行列を生成する
    /// </summary>
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

    /// <summary>
    /// Z軸(001)を引数のベクトルvに回転させる行列を生成する
    /// </summary>
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
