using System.Collections.Generic;
using System.Linq;
using C4 = OpenTK.Mathematics.Color4;
using M3d = OpenTK.Mathematics.Matrix3d;
using M3f = OpenTK.Mathematics.Matrix3;
using M4d = OpenTK.Mathematics.Matrix4d;
using M4f = OpenTK.Mathematics.Matrix4;
using V2d = OpenTK.Mathematics.Vector2d;
using V2f = OpenTK.Mathematics.Vector2;
using V3d = OpenTK.Mathematics.Vector3d;
using V3f = OpenTK.Mathematics.Vector3;
using V4d = OpenTK.Mathematics.Vector4d;
using V4f = OpenTK.Mathematics.Vector4;

namespace Crystallography.OpenGL;

public static class Extensions
{
    #region V2dに関する拡張メソッド

    /// <summary>
    /// 拡張メソッド.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static V2f ToV2f(in this V2d v) => new((float)v.X, (float)v.Y);

    #endregion V2dに関する拡張メソッド

    #region V3dに関するメソッド

    /// <summary>
    /// 拡張メソッド.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static V3f ToV3f(in this V3d v) => new((float)v.X, (float)v.Y, (float)v.Z);

    /// <summary>
    /// 拡張メソッド.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector3DBase ToVector3DBase(in this V3d v) => new(v.X, v.Y, v.Z);

    /// <summary>
    /// 拡張メソッド.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static V4d ToV4d(in this V3d v) => new(v.X, v.Y, v.Z, 1);



    #endregion V3dに関する拡張メソッド

    #region V3fに関する拡張メソッド

    /// <summary>
    /// 拡張メソッド.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static C4 ToC4(in this V3f v, in float A) => new(v.X, v.Y, v.Z, A);

    /// <summary>
    /// 拡張メソッド.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static V3d ToV3d(in this V3f v) => new(v.X, v.Y, v.Z);




    #endregion V4fに関する拡張メソッド

    #region V4fに関する拡張メソッド

    /// <summary>
    /// 拡張メソッド.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static float[] ToArray(in this V4f v) => [v.X, v.Y, v.Z, v.W];

    /// <summary>
    /// 拡張メソッド.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static C4 ToC4(in this V4f v) => new(v.X, v.Y, v.Z, v.W);


    /// <summary>
    /// 拡張メソッド.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static V3d ToV3d(in this V4f v) => new(v.X, v.Y, v.Z);

    #endregion V4fに関する拡張メソッド

    #region V4dに関する拡張メソッド

    /// <summary>
    /// 拡張メソッド.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static V4f ToV4f(in this V4d v) => new((float)v.X, (float)v.Y, (float)v.Z, (float)v.W);

    /// <summary>
    /// 拡張メソッド.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static V3f ToV3f(in this V4d v) => new((float)v.X, (float)v.Y, (float)v.Z);



    /// <summary>
    /// 拡張メソッド.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static double[] ToArray(in this V4d v) => [v.X, v.Y, v.Z, v.W];

    /// <summary>
    /// 拡張メソッド.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static float[] ToArrayF(in this V4d v) => [(float)v.X, (float)v.Y, (float)v.Z, (float)v.W];

    /// <summary>
    /// 拡張メソッド.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static C4 ToC4(in this V4d v) => new((float)v.X, (float)v.Y, (float)v.Z, (float)v.W);

    #endregion V4dに関する拡張メソッド

    #region C4に関する拡張メソッド

    /// <summary>
    /// 拡張メソッド.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static V4f ToV4f(in this C4 c) => new(c.R, c.G, c.B, c.A);

    /// <summary>
    /// 拡張メソッド. Aチャンネルを削除してVector3に変換.
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public static V3f ToV3f(in this C4 c) => new(c.R, c.G, c.B);




    #endregion C4に関する拡張メソッド

    #region M4dに関する拡張メソッド

    /// <summary>
    /// 拡張メソッド.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static M4f ToM4f(in this M4d m) => new(
        (float)m.M11, (float)m.M12, (float)m.M13, (float)m.M14,
        (float)m.M21, (float)m.M22, (float)m.M23, (float)m.M24,
        (float)m.M31, (float)m.M32, (float)m.M33, (float)m.M34,
        (float)m.M41, (float)m.M42, (float)m.M43, (float)m.M44
       );

    /// <summary>
    /// 拡張メソッド.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Matrix3D ToMatrix3D(in this M4d m) => new(
        m.M11, m.M21, m.M31,
        m.M12, m.M22, m.M32,
        m.M13, m.M23, m.M33
       );

    #endregion M4dに関する拡張メソッド

    #region M3dに関する拡張メソッド

    /// <summary>
    /// 拡張メソッド.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static M3f ToM3f(in this M3d m) => new(
        (float)m.M11, (float)m.M12, (float)m.M13,
        (float)m.M21, (float)m.M22, (float)m.M23,
        (float)m.M31, (float)m.M32, (float)m.M33
       );

    /// <summary>
    /// 拡張メソッド.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Matrix3D ToMatrix3D(in this M3d m) => new(
        m.M11, m.M21, m.M31,
        m.M12, m.M22, m.M32,
        m.M13, m.M23, m.M33
       );


    /// <summary>
    /// 拡張メソッド.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static M4d ToMatrix4d(in this M3d m) => new(m);



    #endregion M3dに関する拡張メソッド

    #region その他の静的メソッド
    public static V3f Average(IEnumerable<V3f> vectors)
    {
        float x = 0, y = 0, z = 0;
        foreach (var v in vectors)
        {
            x += v.X;
            y += v.Y;
            z += v.Z;
        }
        var count = vectors.Count();
        return new V3f(x / count, y / count, z / count);
    }

    public static V3d Average(IEnumerable<V3d> vectors)
    {
        double x = 0, y = 0, z = 0;
        foreach (var v in vectors)
        {
            x += v.X;
            y += v.Y;
            z += v.Z;
        }
        var count = vectors.Count();
        return new V3d(x / count, y / count, z / count);
    }
    #endregion
}
