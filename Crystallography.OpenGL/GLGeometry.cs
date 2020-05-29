using System;
using System.Collections.Generic;
using System.Linq;
using M3d = OpenTK.Matrix3d;
using V2d = OpenTK.Vector2d;
using V3d = OpenTK.Vector3d;

namespace Crystallography.OpenGL
{
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
        public static (int[] Indices, V3d Center, V3d Norm) PolygonInfo(V3d[] points, V3d origin)
        {
            var center = new V3d(points.Average(p => p.X), points.Average(p => p.Y), points.Average(p => p.Z));
            var prm = Geometriy.GetPlaneEquationFromPoints(points.Select(p => p.ToVector3DBase()));
            var norm = new V3d(prm[0], prm[1], prm[2]);
            if (V3d.Dot(norm, (center - origin)) < 0)
                norm = -norm;

            //座標変換 (XY平面に投影)
            var rot = CreateRotationToZ(norm);

            var vXY = points.Select(p => p - center).Select(p => new V2d(rot.M11 * p.X + rot.M12 * p.Y + rot.M13 * p.Z, rot.M21 * p.X + rot.M22 * p.Y + rot.M23 * p.Z)).ToList();

            int i = vXY.FindIndex(p => p.LengthSquared == vXY.Max(q => q.LengthSquared));//原点から最も距離の遠い点を選ぶ

            //もう一つ点を選び、直線の方程式を産出
            List<int> iList = new List<int>(new[] { i });
            do
            {
                for (int j = 0; j < vXY.Count; j++)
                    if (j != i)
                    {
                        var V = new V2d(vXY[j].Y - vXY[i].Y, vXY[i].X - vXY[j].X);
                        var c = vXY[j].X * vXY[i].Y - vXY[i].X * vXY[j].Y;

                        if (vXY.All(p => V2d.Dot(p, V) + c <= Th))
                        {
                            iList.Add(j);
                            i = j;
                            break;
                        }
                    }
            } while (i != iList[0]);

            return (iList.ToArray(), center, norm);
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
            M3d rot;
            v.Normalize();
            if (Math.Abs(v.Z - 1) < Th)
                rot = M3d.Identity;
            else if (Math.Abs(v.Z + 1) < Th)
                rot = M3d.CreateRotationX(Math.PI);
            else
                rot = M3d.CreateFromAxisAngle(V3d.Cross(Z, v), V3d.CalculateAngle(Z, v));
            return rot;
        }

        public static readonly V3d Z = new V3d(0, 0, 1);
        public const double Th = 0.0000001;
        public static int SerialNumber = 0;
        public static readonly object LockObj = new object();
    }
}