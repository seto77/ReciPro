using C4 = OpenTK.Graphics.Color4;
using M3d = OpenTK.Matrix3d;
using M3f = OpenTK.Matrix3;
using M4d = OpenTK.Matrix4d;
using M4f = OpenTK.Matrix4;
using V2d = OpenTK.Vector2d;
using V2f = OpenTK.Vector2;
using V3d = OpenTK.Vector3d;
using V3f = OpenTK.Vector3;
using V4d = OpenTK.Vector4d;
using V4f = OpenTK.Vector4;

namespace Crystallography.OpenGL
{
    public static class Extensions
    {
        #region V2dに関する拡張メソッド

        /// <summary>
        /// 拡張メソッド.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static V2f ToV2f(this V2d v) => new V2f((float)v.X, (float)v.Y);

        #endregion V2dに関する拡張メソッド

        #region V3dに関する拡張メソッド

        /// <summary>
        /// 拡張メソッド.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static V3f ToV3f(this V3d v) => new V3f((float)v.X, (float)v.Y, (float)v.Z);

        /// <summary>
        /// 拡張メソッド.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3DBase ToVector3DBase(this V3d v) => new Vector3DBase(v.X, v.Y, v.Z);

        /// <summary>
        /// 拡張メソッド.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static V4d ToV4d(this V3d v) => new V4d(v.X, v.Y, v.Z, 1);

        #endregion V3dに関する拡張メソッド


        #region V3fに関する拡張メソッド

        /// <summary>
        /// 拡張メソッド.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static C4 ToC4(this V3f v, float A) => new C4(v.X, v.Y, v.Z, A);

        /// <summary>
        /// 拡張メソッド.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static V3d ToV3d(this V3f v) => new V3d(v.X, v.Y, v.Z);

        #endregion V4fに関する拡張メソッド



        #region V4fに関する拡張メソッド

        /// <summary>
        /// 拡張メソッド.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static float[] ToArray(this V4f v) => new[] { v.X, v.Y, v.Z, v.W };

        /// <summary>
        /// 拡張メソッド.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static C4 ToC4(this V4f v) => new C4(v.X, v.Y, v.Z, v.W);


        /// <summary>
        /// 拡張メソッド.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static V3d ToV3d(this V4f v) => new V3d(v.X, v.Y, v.Z);

        #endregion V4fに関する拡張メソッド



        #region V4dに関する拡張メソッド

        /// <summary>
        /// 拡張メソッド.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static V4f ToV4f(this V4d v) => new V4f((float)v.X, (float)v.Y, (float)v.Z, (float)v.W);

        /// <summary>
        /// 拡張メソッド.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static V3f ToV3f(this V4d v) => new V3f((float)v.X, (float)v.Y, (float)v.Z);



        /// <summary>
        /// 拡張メソッド.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static double[] ToArray(this V4d v) => new[] { v.X, v.Y, v.Z, v.W };

        /// <summary>
        /// 拡張メソッド.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static float[] ToArrayF(this V4d v) => new[] { (float)v.X, (float)v.Y, (float)v.Z, (float)v.W };

        /// <summary>
        /// 拡張メソッド.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static C4 ToC4(this V4d v) => new C4((float)v.X, (float)v.Y, (float)v.Z, (float)v.W);

        #endregion V4dに関する拡張メソッド

        #region C4に関する拡張メソッド

        /// <summary>
        /// 拡張メソッド.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static V4f ToV4f(this C4 c) => new V4f(c.R, c.G, c.B, c.A);

        /// <summary>
        /// 拡張メソッド. Aチャンネルを削除してVector3に変換.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static V3f ToV3f(this C4 c) => new V3f(c.R, c.G, c.B);




        #endregion C4に関する拡張メソッド

        #region M4dに関する拡張メソッド

        /// <summary>
        /// 拡張メソッド.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static V4d Mult(this M4d m, V4d v) => new V4d(
                m.M11 * v.X + m.M12 * v.Y + m.M13 * v.Z + m.M14 * v.W,
                m.M21 * v.X + m.M22 * v.Y + m.M23 * v.Z + m.M24 * v.W,
                m.M31 * v.X + m.M32 * v.Y + m.M33 * v.Z + m.M34 * v.W,
                m.M41 * v.X + m.M42 * v.Y + m.M43 * v.Z + m.M44 * v.W
            );

        /// <summary>
        /// 拡張メソッド.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static M4d Mult(this M4d m, double c) => new M4d(
                c * m.M11, c * m.M12, c * m.M13, c * m.M14,
                c * m.M21, c * m.M22, c * m.M23, c * m.M24,
                c * m.M31, c * m.M32, c * m.M33, c * m.M34,
                c * m.M41, c * m.M42, c * m.M43, c * m.M44
            );

        /// <summary>
        /// 拡張メソッド.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static V4f Mult(this M4d m, V4f v) => new V4f(
                (float)(m.M11 * v.X + m.M12 * v.Y + m.M13 * v.Z + m.M14 * v.W),
                (float)(m.M21 * v.X + m.M22 * v.Y + m.M23 * v.Z + m.M24 * v.W),
                (float)(m.M31 * v.X + m.M32 * v.Y + m.M33 * v.Z + m.M34 * v.W),
                (float)(m.M41 * v.X + m.M42 * v.Y + m.M43 * v.Z + m.M44 * v.W)
            );

        /// <summary>
        /// 拡張メソッド.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static M4f ToM4f(this M4d m) => new M4f(
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
        public static Matrix3D ToMatrix3D(this M4d m) => new Matrix3D(
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
        public static V3d Mult(this M3d m, V3d v) => new V3d(
                m.M11 * v.X + m.M12 * v.Y + m.M13 * v.Z,
                m.M21 * v.X + m.M22 * v.Y + m.M23 * v.Z,
                m.M31 * v.X + m.M32 * v.Y + m.M33 * v.Z
            );

        /// <summary>
        /// 拡張メソッド.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static V3f Mult(this M3d m, V3f v) => new V3f(
               (float)(m.M11 * v.X + m.M12 * v.Y + m.M13 * v.Z),
               (float)(m.M21 * v.X + m.M22 * v.Y + m.M23 * v.Z),
               (float)(m.M31 * v.X + m.M32 * v.Y + m.M33 * v.Z)
            );

        /// <summary>
        /// 拡張メソッド.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static M3f ToM3f(this M3d m) => new M3f(
            (float)m.M11, (float)m.M12, (float)m.M13,
            (float)m.M21, (float)m.M22, (float)m.M23,
            (float)m.M31, (float)m.M32, (float)m.M33
           );

        /// <summary>
        /// 拡張メソッド.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Matrix3D ToMatrix3D(this M3d m) => new Matrix3D(
            m.M11, m.M21, m.M31,
            m.M12, m.M22, m.M32,
            m.M13, m.M23, m.M33
           );


        /// <summary>
        /// 拡張メソッド.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static M4d ToMatrix4D(this M3d m) => new M4d(
            m.M11, m.M21, m.M31, 0,
            m.M12, m.M22, m.M32, 0,
            m.M13, m.M23, m.M33, 0,
            0, 0, 0, 1
           );

        #endregion M3dに関する拡張メソッド
    }
}