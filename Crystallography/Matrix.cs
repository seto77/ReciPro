using MathNet.Numerics.LinearAlgebra;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Numerics;
using System.Xml.Serialization;
using OpenTK;

namespace Crystallography
{
    /// <summary>
    /// 3行3列行列と静的関数を提供 Eijはi行j列の要素を意味する
    /// </summary>
    [Serializable()]
    public class Matrix3D : ICloneable
    {
        public object Clone()
        {
            Matrix3D c = (Matrix3D)this.MemberwiseClone();
            return c;
        }

        public double E11, E12, E13, E21, E22, E23, E31, E32, E33;

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ。引数無しの場合は、単位行列を返す
        /// </summary>
        public Matrix3D()
        {
            E11 = 1; E12 = 0; E13 = 0;
            E21 = 0; E22 = 1; E23 = 0;
            E31 = 0; E32 = 0; E33 = 1;
        }

        public Matrix3D(Vector3D v1, Vector3D v2, Vector3D v3)
        {
            E11 = v1.X;
            E21 = v1.Y;
            E31 = v1.Z;

            E12 = v2.X;
            E22 = v2.Y;
            E32 = v2.Z;

            E13 = v3.X;
            E23 = v3.Y;
            E33 = v3.Z;
        }

        public Matrix3D(Vector3DBase v1, Vector3DBase v2, Vector3DBase v3)
        {
            E11 = v1.X;
            E21 = v1.Y;
            E31 = v1.Z;

            E12 = v2.X;
            E22 = v2.Y;
            E32 = v2.Z;

            E13 = v3.X;
            E23 = v3.Y;
            E33 = v3.Z;
        }

        public Matrix3D(double e11, double e21, double e31, double e12, double e22, double e32, double e13, double e23, double e33)
        {
            E11 = e11;
            E21 = e21;
            E31 = e31;

            E12 = e12;
            E22 = e22;
            E32 = e32;

            E13 = e13;
            E23 = e23;
            E33 = e33;
        }

        public Matrix3D(double[] e)
        {
            if (e.Length == 9)
            {
                E11 = e[0];
                E21 = e[1];
                E31 = e[2];

                E12 = e[3];
                E22 = e[4];
                E32 = e[5];

                E13 = e[6];
                E23 = e[7];
                E33 = e[8];
            }
            else
            {
                E11 = 1; E12 = 0; E13 = 0;
                E21 = 0; E22 = 1; E23 = 0;
                E31 = 0; E32 = 0; E33 = 1;
            }
        }

        public Matrix3D(Matrix3D m)
        {
            E11 = m.E11;
            E21 = m.E21;
            E31 = m.E31;

            E12 = m.E12;
            E22 = m.E22;
            E32 = m.E32;

            E13 = m.E13;
            E23 = m.E23;
            E33 = m.E33;
        }

        #endregion

        public double[] ToArrayRight() => new[] { E11, E12, E13, E21, E22, E23, E31, E32, E33 };

        #region  演算子のオーバーロード

        public static Matrix3D operator *(Matrix3D m1, Matrix3D m2)
        {
            Matrix3D m = new Matrix3D();
            m.E11 = m1.E11 * m2.E11 + m1.E12 * m2.E21 + m1.E13 * m2.E31;
            m.E12 = m1.E11 * m2.E12 + m1.E12 * m2.E22 + m1.E13 * m2.E32;
            m.E13 = m1.E11 * m2.E13 + m1.E12 * m2.E23 + m1.E13 * m2.E33;

            m.E21 = m1.E21 * m2.E11 + m1.E22 * m2.E21 + m1.E23 * m2.E31;
            m.E22 = m1.E21 * m2.E12 + m1.E22 * m2.E22 + m1.E23 * m2.E32;
            m.E23 = m1.E21 * m2.E13 + m1.E22 * m2.E23 + m1.E23 * m2.E33;

            m.E31 = m1.E31 * m2.E11 + m1.E32 * m2.E21 + m1.E33 * m2.E31;
            m.E32 = m1.E31 * m2.E12 + m1.E32 * m2.E22 + m1.E33 * m2.E32;
            m.E33 = m1.E31 * m2.E13 + m1.E32 * m2.E23 + m1.E33 * m2.E33;

            return m;
        }

        public static Matrix3D operator *(double d, Matrix3D m2)
        {
            Matrix3D m = new Matrix3D();
            m.E11 = d * m2.E11;
            m.E12 = d * m2.E12;
            m.E13 = d * m2.E13;

            m.E21 = d * m2.E21;
            m.E22 = d * m2.E22;
            m.E23 = d * m2.E23;

            m.E31 = d * m2.E31;
            m.E32 = d * m2.E32;
            m.E33 = d * m2.E33;

            return m;
        }

        public static Matrix3D operator +(Matrix3D m1, Matrix3D m2)
        {
            Matrix3D m = new Matrix3D();
            m.E11 = m1.E11 + m2.E11;
            m.E12 = m1.E12 + m2.E12;
            m.E13 = m1.E13 + m2.E13;

            m.E21 = m1.E21 + m2.E21;
            m.E22 = m1.E22 + m2.E22;
            m.E23 = m1.E23 + m2.E23;

            m.E31 = m1.E31 + m2.E31;
            m.E32 = m1.E32 + m2.E32;
            m.E33 = m1.E33 + m2.E33;
            return m;
        }

        public static Matrix3D operator -(Matrix3D m1, Matrix3D m2)
        {
            Matrix3D m = new Matrix3D();
            m.E11 = m1.E11 - m2.E11;
            m.E12 = m1.E12 - m2.E12;
            m.E13 = m1.E13 - m2.E13;

            m.E21 = m1.E21 - m2.E21;
            m.E22 = m1.E22 - m2.E22;
            m.E23 = m1.E23 - m2.E23;

            m.E31 = m1.E31 - m2.E31;
            m.E32 = m1.E32 - m2.E32;
            m.E33 = m1.E33 - m2.E33;
            return m;
        }

        public static Matrix3D operator -(Matrix3D m1)
        {
            Matrix3D m = new Matrix3D();
            m.E11 = -m1.E11;
            m.E12 = -m1.E12;
            m.E13 = -m1.E13;

            m.E21 = -m1.E21;
            m.E22 = -m1.E22;
            m.E23 = -m1.E23;

            m.E31 = -m1.E31;
            m.E32 = -m1.E32;
            m.E33 = -m1.E33;
            return m;
        }

        public static Vector3D operator *(Matrix3D m, Vector3D v)
        {
            if (m == null || v == null)
                return null;
            else
                return new Vector3D(m.E11 * v.X + m.E12 * v.Y + m.E13 * v.Z, m.E21 * v.X + m.E22 * v.Y + m.E23 * v.Z, m.E31 * v.X + m.E32 * v.Y + m.E33 * v.Z);
        }

        public static Vector3DBase operator *(Matrix3D m, Vector3DBase v)
        {
            return new Vector3DBase(m.E11 * v.X + m.E12 * v.Y + m.E13 * v.Z, m.E21 * v.X + m.E22 * v.Y + m.E23 * v.Z, m.E31 * v.X + m.E32 * v.Y + m.E33 * v.Z);
        }

        /// <summary>
        /// Matrix3Dとタプル(x,y,z)の乗算. (x,y,z)を縦方向のベクトルとして計算する。
        /// </summary>
        /// <param name="m"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3DBase operator *(Matrix3D m,(int X, int Y, int Z) v)
        {
            return new Vector3DBase(m.E11 * v.X + m.E12 * v.Y + m.E13 * v.Z, m.E21 * v.X + m.E22 * v.Y + m.E23 * v.Z, m.E31 * v.X + m.E32 * v.Y + m.E33 * v.Z);
        }

        public static Vector3DBase operator *(Matrix3D m, (double X, double Y, double Z) v)
        {
            return new Vector3DBase(m.E11 * v.X + m.E12 * v.Y + m.E13 * v.Z, m.E21 * v.X + m.E22 * v.Y + m.E23 * v.Z, m.E31 * v.X + m.E32 * v.Y + m.E33 * v.Z);
        }

        public static Vector3d operator *(Matrix3D m, Vector3d v)
        {
            return new Vector3d(m.E11 * v.X + m.E12 * v.Y + m.E13 * v.Z, m.E21 * v.X + m.E22 * v.Y + m.E23 * v.Z, m.E31 * v.X + m.E32 * v.Y + m.E33 * v.Z);
        }

        #endregion
        public static Matrix3D Inverse(Matrix3D m)
        {
            double det = -m.E13 * m.E22 * m.E31 + m.E12 * m.E23 * m.E31 + m.E13 * m.E21 * m.E32 - m.E11 * m.E23 * m.E32 - m.E12 * m.E21 * m.E33 + m.E11 * m.E22 * m.E33;
            if (det == 0)
                return new Matrix3D();
            Matrix3D mInv = new Matrix3D();
            mInv.E11 = (-m.E23 * m.E32 + m.E22 * m.E33) / det;
            mInv.E12 = (m.E13 * m.E32 - m.E12 * m.E33) / det;
            mInv.E13 = (-m.E13 * m.E22 + m.E12 * m.E23) / det;
            mInv.E21 = (m.E23 * m.E31 - m.E21 * m.E33) / det;
            mInv.E22 = (-m.E13 * m.E31 + m.E11 * m.E33) / det;
            mInv.E23 = (m.E13 * m.E21 - m.E11 * m.E23) / det;
            mInv.E31 = (-m.E22 * m.E31 + m.E21 * m.E32) / det;
            mInv.E32 = (m.E12 * m.E31 - m.E11 * m.E32) / det;
            mInv.E33 = (-m.E12 * m.E21 + m.E11 * m.E22) / det;
            return mInv;
        }

        
        
        public Matrix3D Inverse()
        {
            return Matrix3D.Inverse(this);
        }

        public static Matrix3D Transpose(Matrix3D m)
        {
            return new Matrix3D(m.E11, m.E12, m.E13, m.E21, m.E22, m.E23, m.E31, m.E32, m.E33);
        }

        public Matrix3D Transpose()
        {
            return Matrix3D.Transpose(this);
        }

        public static double Determinant(Matrix3D m)
        {
            return -m.E13 * m.E22 * m.E31 + m.E12 * m.E23 * m.E31 + m.E13 * m.E21 * m.E32 - m.E11 * m.E23 * m.E32 - m.E12 * m.E21 * m.E33 + m.E11 * m.E22 * m.E33;
        }

        public double Determinant()
        {
            return Determinant(this);
        }

        public Matrix3D ExchangeX_Y_Z()
        {
            return ExchangeX_Y_Z(this);
        }

        public static Matrix3D ExchangeX_Y_Z(Matrix3D m)
        {
            return new Matrix3D(m.E11, -m.E21, -m.E31, m.E12, -m.E22, -m.E32, m.E13, -m.E23, -m.E33);
        }

        public Matrix3D ExchangeYZX()
        {
            return ExchangeYZX(this);
        }

        public static Matrix3D ExchangeYZX(Matrix3D m)
        {
            return new Matrix3D(m.E21, m.E31, m.E11, m.E22, m.E32, m.E12, m.E23, m.E33, m.E13);
        }

        public Matrix3D ExchangeY_Z_X()
        {
            return ExchangeY_Z_X(this);
        }

        public static Matrix3D ExchangeY_Z_X(Matrix3D m)
        {
            return new Matrix3D(-m.E21, -m.E31, m.E11, -m.E22, -m.E32, m.E12, -m.E23, -m.E33, m.E13);
        }

        public Matrix3D ExchangeZXY()
        {
            return ExchangeZXY(this);
        }

        public static Matrix3D ExchangeZXY(Matrix3D m)
        {
            return new Matrix3D(m.E31, m.E11, m.E21, m.E32, m.E12, m.E22, m.E33, m.E13, m.E23);
        }

        public Matrix3D ExchangeZ_X_Y()
        {
            return ExchangeZ_X_Y(this);
        }

        public static Matrix3D ExchangeZ_X_Y(Matrix3D m)
        {
            return new Matrix3D(-m.E31, m.E11, -m.E21, -m.E32, m.E12, -m.E22, -m.E33, m.E13, -m.E23);
        }

        /// <summary>
        /// ベクトルvの方向の周りに,thetaだけ回転させる行列を生成する
        /// </summary>
        /// <param name="v">回転軸</param>
        /// <param name="theta">回転角度</param>
        /// <returns></returns>
        public static Matrix3D Rot(Vector3DBase v, double theta)
        {
            //Vx*Vx*(1-cos) + cos  	    Vx*Vy*(1-cos) - Vz*sin  	Vz*Vx*(1-cos) + Vy*sin
            //Vx*Vy*(1-cos) + Vz*sin 	Vy*Vy*(1-cos) + cos 	    Vy*Vz*(1-cos) - Vx*sin
            //Vz*Vx*(1-cos) - Vy*sin 	Vy*Vz*(1-cos) + Vx*sin 	    Vz*Vz*(1-cos) + cos

            if (v.X == 0 && v.Y == 0 && v.Z == 0)
                return new Matrix3D();
            v = Vector3DBase.Normarize(v);
            double X = v.X, Y = v.Y, Z = v.Z;
            var m = new Matrix3D();
            var cos = Math.Cos(theta);
            var oneMinusCos = 1 - cos;
            var sin = Math.Sin(theta);

            var nxyc = X * Y * oneMinusCos;
            var nyzc = Y * Z * oneMinusCos;
            var nzxc = Z * X * oneMinusCos;
            var nxs = X * sin;
            var nys = Y * sin;
            var nzs = Z * sin;

            m.E11 = oneMinusCos * X * X + cos;
            m.E12 = nxyc - nzs;
            m.E13 = nzxc + nys;

            m.E21 = nxyc + nzs;
            m.E22 = oneMinusCos * Y * Y + cos;
            m.E23 = nyzc - nxs;

            m.E31 = nzxc - nys;
            m.E32 = nyzc + nxs;
            m.E33 = oneMinusCos * Z * Z + cos;

            return m;
        }
        public static Matrix3D Rot(Vector3d v, double theta)
        {
            return Rot(new Vector3DBase(v.X, v.Y, v.Z), theta);
        }

        public static Matrix3D RotX(double theta)
        {
            double cos = Math.Cos(theta);
            double sin = Math.Sin(theta);
            return new Matrix3D()
            {
                E22 = cos,
                E23 = -sin,
                E32 = sin,
                E33 = cos
            };
        }
        public static Matrix3D RotY(double theta)
        {
            var cos = Math.Cos(theta);
            var sin = Math.Sin(theta);
            return new Matrix3D()
            {
                E11 = cos,
                E13 = sin,
                E31 = -sin,
                E33 = cos
            };
        }
        public static Matrix3D RotZ(double theta)
        {
            var cos = Math.Cos(theta);
            var sin = Math.Sin(theta);
            return new Matrix3D()
            {
                E11 = cos,
                E12 = -sin,
                E21 = sin,
                E22 = cos
            };
        }

        private static Random Rn = new Random(System.Environment.TickCount * 2);

        public static Matrix3D GenerateRamdomRotationMatrix()
        {
            return GenerateRamdomRotationMatrix(Rn);
        }

        public static Matrix3D GenerateRamdomRotationMatrix(int seed)
        {
            return GenerateRamdomRotationMatrix(new Random(seed));
        }

        public static Matrix3D GenerateRamdomRotationMatrix(Random rn)
        {
            double rn1, rn2, rn3;
            lock (o)
            {
                rn1 = rn.NextDouble();
                rn2 = rn.NextDouble();
                rn3 = rn.NextDouble();
            }
            return GenerateRamdomRotationMatrix(rn1, rn2, rn3);
        }

        private static object o = new object();

        public static Matrix3D GenerateRamdomRotationMatrix(double rn1, double rn2, double rn3)
        {
            double phi = rn1 * 2 * Math.PI;
            double cosPhi = Math.Cos(phi), sinPhi = Math.Sin(phi);
            double cosTheta = rn2 * 2 - 1, sinTheta = Math.Sqrt(1 - cosTheta * cosTheta);
            double ksi = rn3 * 2 * Math.PI;
            double cosKsi = Math.Cos(ksi), sinKsi = Math.Sin(ksi);

            //Matrix3D m1 = new Matrix3D(cosPhi, 0, -sinPhi, 0, 1, 0, sinPhi, 0, cosPhi);
            //Matrix3D m2 = new Matrix3D(1, 0, 0, 0, cosTheta, sinTheta, 0, -sinTheta, cosTheta);
            //Matrix3D m3 = new Matrix3D(cosKsi, 0, -sinKsi, 0, 1, 0, sinKsi, 0, cosKsi);

            return new Matrix3D(
                cosKsi * cosPhi - cosTheta * sinKsi * sinPhi,
                sinPhi * sinTheta,
                -cosPhi * sinKsi - cosKsi * cosTheta * sinPhi,
                sinKsi * sinTheta,
                cosTheta,
                cosKsi * sinTheta,
                cosPhi * cosTheta * sinKsi + cosKsi * sinPhi,
                -cosPhi * sinTheta,
                cosKsi * cosPhi * cosTheta - sinKsi * sinPhi);
        }

        /// <summary>
        /// 等面積空間の行列を作成, phi, theta, ksiは0~1の範囲で入力
        /// </summary>
        /// <param name="phi"></param>
        /// <param name="theta"></param>
        /// <param name="ksi"></param>
        /// <returns></returns>
        public static Matrix3D GenerateEquiareaMatrix(double phi, double theta, double ksi)
        {
            double cosPhi = Math.Cos(phi * 2 * Math.PI), sinPhi = Math.Sin(phi * 2 * Math.PI);
            double cosTheta = theta * 2 - 1, sinTheta = Math.Sqrt(1 - cosTheta * cosTheta);
            double cosKsi = Math.Cos(ksi * 2 * Math.PI), sinKsi = Math.Sin(ksi * 2 * Math.PI);

            Matrix3D m1 = new Matrix3D(cosPhi, -sinPhi, 0, sinPhi, cosPhi, 0, 0, 0, 1);
            //Matrix3D m1 = new Matrix3D(cosPhi, 0, sinPhi, 0, 1, 0, -sinPhi, 0, cosPhi);
            Matrix3D m2 = new Matrix3D(1, 0, 0, 0, cosTheta, sinTheta, 0, -sinTheta, cosTheta);
            Matrix3D m3 = new Matrix3D(cosKsi, -sinKsi, 0, sinKsi, cosKsi, 0, 0, 0, 1);

            return m1 * m2 * m3;
        }

        /// <summary>
        /// ゼロ行列かどうかを判定
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static bool IsZero(Matrix3D m)
        {
            return m.E11 == 0 && m.E12 == 0 && m.E13 == 0 && m.E21 == 0 && m.E22 == 0 && m.E23 == 0 && m.E31 == 0 && m.E32 == 0 && m.E33 == 0;
        }

        /// <summary>
        /// ゼロ行列かどうかを判定
        /// </summary>
        /// <returns></returns>
        public bool IsZero()
        => IsZero(this);

        /// <summary>
        /// 単位行列かどうかを判定
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static bool IsIdentity(Matrix3D m)
        => m.E11 == 1 && m.E12 == 0 && m.E13 == 0 && m.E21 == 0 && m.E22 == 1 && m.E23 == 0 && m.E31 == 0 && m.E32 == 0 && m.E33 == 1;

        /// <summary>
        /// 単位行列かどうかを判定
        /// </summary>
        /// <returns></returns>
        public bool IsIdentity()
            => IsIdentity(this);

        /// <summary>
        /// 対角成分の和を求める
        /// </summary>
        /// <returns></returns>
        public double SumOfDiagonalCompenent()        => SumOfDiagonalCompenent(this);

        /// <summary>
        /// 対角成分の和を求める
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static double SumOfDiagonalCompenent(Matrix3D m)        => m.E11 + m.E22 + m.E33;


        /// <summary>
        /// ゼロ行列 (定数)
        /// </summary>
        public static readonly Matrix3D ZeroMatrix = new Matrix3D(0, 0, 0, 0, 0, 0, 0, 0, 0);

        /// <summary>
        /// 単位行列 (定数)
        /// </summary>
        public static readonly Matrix3D IdentityMatrix = new Matrix3D(1, 0, 0, 0, 1, 0, 0, 0, 1);


        public bool Equals (Matrix3D m)
        {
            return 
                m.E11 == E11 && m.E12 == E12 && m.E13 == E13 &&
                m.E21 == E21 && m.E22 == E22 && m.E23 == E23 &&
                m.E31 == E31 && m.E32 == E32 && m.E33 == E33;
        }

    }

    /// <summary>
    /// 3次元ベクトルの基本的な機能を提供する
    /// </summary>
    [Serializable()]
    [TypeConverter(typeof(Vector3DConverter))]
    public class Vector3DBase : ICloneable
    {
        public static Vector3DBase Zero = new Vector3DBase(0,0,0);

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        //public object Tag { get; set; }

        public object Clone()
        {
            //return (Vector3DBase)this.MemberwiseClone();
            return new Vector3D(X, Y, Z);
        }

        public Vector3DBase()
        {
            X = 0; Y = 0; Z = 0;
        }

        public Vector3DBase(double x, double y, double z)
        {
            X = x; Y = y; Z = z;
        }

        public Vector3DBase(double[] v)
        {
            if (v.Length == 3)
            {
                X = v[0]; Y = v[1]; Z = v[2];
            }
            else
            {
                X = 0; Y = 0; Z = 0;
            }
        }

        public Vector3DBase(float[] v)
        {
            if (v.Length == 3)
            {
                X = v[0]; Y = v[1]; Z = v[2];
            }
            else
            {
                X = 0; Y = 0; Z = 0;
            }
        }

        public Vector3DBase(Vector3DBase v)
        {
            X = v.X; Y = v.Y; Z = v.Z;
        }

        public double[] ToDouble()
        {
            return new double[] { X, Y, Z };
        }

        public float[] ToSingle()
        {
            return new float[] { (float)X, (float)Y, (float)Z };
        }

        /// <summary>
        /// X,Y座標をPointDクラスに映す (Zは破棄)
        /// </summary>
        /// <returns></returns>
        public PointD ToPointD()
        {
            return new PointD(X, Y);
        }

        #region 演算子のオーバーロード

        public static bool operator <(Vector3DBase v1, Vector3DBase v2) => (v1.X < v2.X && v1.Y < v2.Y && v1.Z < v2.Z);

        public static bool operator <=(Vector3DBase v1, Vector3DBase v2) => (v1.X <= v2.X && v1.Y <= v2.Y && v1.Z <= v2.Z);

        public static bool operator >(Vector3DBase v1, Vector3DBase v2) => (v1.X > v2.X && v1.Y > v2.Y && v1.Z > v2.Z);

        public static bool operator >=(Vector3DBase v1, Vector3DBase v2) => (v1.X >= v2.X && v1.Y >= v2.Y && v1.Z >= v2.Z);

        public static Vector3DBase operator +(Vector3DBase v1, Vector3DBase v2) => new Vector3DBase(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);

        public static Vector3DBase operator -(Vector3DBase v1, Vector3DBase v2) => new Vector3DBase(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        public static Vector3DBase operator -(Vector3DBase v1) => new Vector3DBase(-v1.X, -v1.Y, -v1.Z);

        public static Vector3DBase operator *(double d, Vector3DBase v1) => new Vector3DBase(d * v1.X, d * v1.Y, d * v1.Z);

        public static Vector3DBase operator *(Vector3DBase v1, double d) => new Vector3DBase(d * v1.X, d * v1.Y, d * v1.Z);

        public static Vector3DBase operator *(int d, Vector3DBase v1) => new Vector3DBase(d * v1.X, d * v1.Y, d * v1.Z);

        public static Vector3DBase operator *(Vector3DBase v1, int d) => new Vector3DBase(d * v1.X, d * v1.Y, d * v1.Z);

        public static double operator *(Vector3DBase v1, Vector3DBase v2) => v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;

        public static double operator *(Vector3DBase v1, (int X, int Y, int Z) v2) => v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;

        public static double operator *(Vector3DBase v1, (double X, double Y, double Z) v2) => v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;

        public static Vector3D operator *(Matrix<double> m, Vector3DBase v)
        {
            if (m.RowCount == 3 && m.ColumnCount == 3)
                return new Vector3D(
                    m[0, 0] * v.X + m[1, 0] * v.Y + m[2, 0] * v.Z,
                    m[0, 1] * v.X + m[1, 1] * v.Y + m[2, 1] * v.Z,
                    m[0, 2] * v.X + m[1, 2] * v.Y + m[2, 2] * v.Z
                    );
            else
                return null;
        }

        public static Vector3DBase operator /(Vector3DBase v1, double d) => new Vector3DBase(v1.X / d, v1.Y / d, v1.Z / d);

        public static Vector3DBase operator /(Vector3DBase v1, int d) => new Vector3DBase(v1.X / d, v1.Y / d, v1.Z / d);

        #endregion 演算子のオーバーロード

        /// <summary>
        /// 原点からの長さを返す
        /// </summary>
        /// <returns></returns>
        public double Length => Math.Sqrt(X * X + Y * Y + Z * Z);

        /// <summary>
        /// 原点からの長さの二乗を返す
        /// </summary>
        /// <returns></returns>
        public double Length2 => X * X + Y * Y + Z * Z;

        /// <summary>
        /// X * X + Y * Y
        /// </summary>
        public double X2Y2 => X * X + Y * Y;
        /// <summary>
        /// Y * Y + Z * Z
        /// </summary>
        public double Y2Z2 => Y * Y + Z * Z;
        /// <summary>
        /// Z * Z + X * X
        /// </summary>
        public double Z2Y2 => Z * Z + X * X;

        public (double X, double Y, double Z) Tuple => (X, Y, Z);


        internal static Vector3DBase Normarize(Vector3DBase v)
        {
            double l = Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
            if (l > 0)
                return new Vector3DBase(v.X / l, v.Y / l, v.Z / l);
            else
                return v;
        }

        public Vector3DBase Normarize() => Normarize(this);

        /// <summary>
        /// 2つのベクトルの外積を返す
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>

        public static Vector3DBase VectorProduct(Vector3DBase v1, Vector3DBase v2) 
            => new Vector3DBase(v1.Y * v2.Z - v1.Z * v2.Y, v1.Z * v2.X - v1.X * v2.Z, v1.X * v2.Y - v1.Y * v2.X);

        /// <summary>
        /// 2つのベクトル間の角度を返す
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>

        public static double AngleBetVectors(Vector3DBase v1, Vector3DBase v2)
        {
            double aCos = Normarize(v1) * Normarize(v2);
            if (aCos > 1) return 0;
            if (aCos < -1) return Math.PI / 2;
            return Math.Acos(aCos);
        }

        public override string ToString() => string.Format("({0}, {1}, {2})", this.X, this.Y, this.Z);
    }

    /// <summary>
    /// 3次元ベクトルと静的関数を提供
    /// </summary>
    [Serializable()]
    public class Vector3D : Vector3DBase, System.IComparable<Vector3D>, ICloneable
    {
        public new object Clone()
        {
            return (Vector3D)this.MemberwiseClone();
        }

        public double d, d2;

        private double theta;

        [XmlIgnore]
        public double Theta
        {
            set
            {
                theta = value;
                TanTheta = Math.Tan(theta);
                Tan2Theta = Math.Tan(2 * theta);
                Sin2Theta = Math.Sin(2 * theta);
                Cos2Theta = Math.Cos(2 * theta);
                CosTheta = Math.Cos(theta);
            }
            get { return theta; }
        }

        public double TanTheta;
        public double Tan2Theta;
        public double Sin2Theta;
        public double Cos2Theta;
        public double CosTheta;
        public double SpostSize;

        public string Index;
        public string IndexInv;

        /// <summary>
        /// 初期値はfalse
        /// </summary>
        public bool Flag = false;

        [XmlIgnore]
        public string[] Extinction = new string[0];

        public string text = "";

        public int Argb;

        public short h, k, l;
        public double RelativeIntensity = 1;
        public double RawIntensity = 0;

        [XmlIgnore]
        public Complex F = new Complex();

        [XmlIgnore]
        public object Tag;

        [XmlIgnore]
        public SymmetryOperation Operation;

        public int CompareTo(Vector3D v)
        {
            if (d != v.d)
                return -d.CompareTo(v.d);
            else if (X != v.X)
                return -X.CompareTo(((Vector3D)v).X);
            else if (Y != v.Y)
                return -Y.CompareTo(v.Y);
            else if (Z != ((Vector3D)v).Z)
                return -Z.CompareTo(v.Z);
            else
                return 0;
        }

        public Vector3D()
        {
            X = 0; Y = 0; Z = 0; d = 0;
        }

        public Vector3D(double x, double y, double z)
        {
            Flag = false;
            X = x; Y = y; Z = z;
            d2 = X * X + Y * Y + Z * Z;
            d = Math.Sqrt(d2);
        }

        public Vector3D(double x, double y, double z, bool IsCalcD)
        {
            Flag = false;
            X = x; Y = y; Z = z;
            if (IsCalcD)
            {
                d2 = X * X + Y * Y + Z * Z;
                d = Math.Sqrt(d2);
            }
        }

        public Vector3D(double[] v)
        {
            if (v.Length == 3)
            {
                Flag = false;
                X = v[0]; Y = v[1]; Z = v[2];
            }
            else
                X = Y = Z = 0;
        }

        public Vector3D(float[] v)
        {
            if (v.Length == 3)
            {
                Flag = false;
                X = v[0]; Y = v[1]; Z = v[2];
            }
            else
            {
                X = 0; Y = 0; Z = 0;
            }
        }

        //演算子のオーバーロード
        public static bool operator <(Vector3D v1, Vector3D v2)
        {
            return (v1.X < v2.X && v1.Y < v2.Y && v1.Z < v2.Z)
                ? true : false;
        }

        public static bool operator <=(Vector3D v1, Vector3D v2)
        {
            return (v1.X <= v2.X && v1.Y <= v2.Y && v1.Z <= v2.Z)
                ? true : false;
        }

        public static bool operator >(Vector3D v1, Vector3D v2)
        {
            return (v1.X > v2.X && v1.Y > v2.Y && v1.Z > v2.Z)
                ? true : false;
        }

        public static bool operator >=(Vector3D v1, Vector3D v2)
        {
            return (v1.X >= v2.X && v1.Y >= v2.Y && v1.Z >= v2.Z)
                ? true : false;
        }

        public static Vector3D operator +(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector3D operator -(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static Vector3D operator -(Vector3D v1)
        {
            return new Vector3D(-v1.X, -v1.Y, -v1.Z);
        }

        public static Vector3D operator *(double d, Vector3D v1)
        {
            return new Vector3D(d * v1.X, d * v1.Y, d * v1.Z);
        }

        public static Vector3D operator *(Vector3D v1, double d)
        {
            return new Vector3D(d * v1.X, d * v1.Y, d * v1.Z);
        }

        public static Vector3D operator *(int d, Vector3D v1)
        {
            return new Vector3D(d * v1.X, d * v1.Y, d * v1.Z);
        }

        public static Vector3D operator *(Vector3D v1, int d)
        {
            return new Vector3D(d * v1.X, d * v1.Y, d * v1.Z);
        }

        public static double operator *(Vector3D v1, Vector3D v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public static Vector3D operator /(Vector3D v1, double d)
        {
            return new Vector3D(v1.X / d, v1.Y / d, v1.Z / d);
        }

        public static Vector3D operator /(Vector3D v1, int d)
        {
            return new Vector3D(v1.X / d, v1.Y / d, v1.Z / d);
        }

        public void NormarizeThis()
        {
            Vector3D v = Vector3D.Normarize(this);
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }

        /// <summary>
        /// 二つのベクトル間の距離を返す
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static double LengthBetVectors(Vector3D v1, Vector3D v2)
        {
            return Math.Sqrt((v1.X - v2.X) * (v1.X - v2.X) + (v1.Y - v2.Y) * (v1.Y - v2.Y) + (v1.Z - v2.Z) * (v1.Z - v2.Z));
        }

        /// <summary>
        /// 二つのベクトル間の距離の2乗を返す
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static double LengthSquareBetVectors(Vector3D v1, Vector3D v2)
        {
            return Math.Sqrt((v1.X - v2.X) * (v1.X - v2.X) + (v1.Y - v2.Y) * (v1.Y - v2.Y) + (v1.Z - v2.Z) * (v1.Z - v2.Z));
        }

        /// <summary>
        /// vの方向が同じで長さが1のベクトルを返す
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3D Normarize(Vector3D v)
        {
            double l = Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
            if (l > 0)
                return new Vector3D(v.X / l, v.Y / l, v.Z / l);
            else
                return v;
        }

        /// <summary>
        /// 3次元ベクトルvのステレオネット上での位置をPointFで返す
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static PointF StereoNetPoint(Vector3D v)
        {
            v = Vector3D.Normarize(v);
            if (v.Z >= -1)
                return new PointF((float)(v.X / (1 + v.Z)), (float)(v.Y / (1 + v.Z)));
            else
                return new PointF(-100, -100);
        }

        //ステレオネット上での位置に対応する3次元ベクトルを返す
        public static Vector3D SphereVector(PointF pt)
        {
            float denominator = pt.X * pt.X + pt.Y * pt.Y + 1;
            return new Vector3D(2 * pt.X / denominator, 2 * pt.Y / denominator, 2 / denominator - 1);
        }

        public static Vector3D SphereVector(PointD pt)
        {
            double denominator = pt.X * pt.X + pt.Y * pt.Y + 1;
            return new Vector3D(2 * pt.X / denominator, 2 * pt.Y / denominator, 2 / denominator - 1);
        }

        //2つの3次元ベクトル間の角度を返す
        public static double AngleBetVectors(Vector3D v1, Vector3D v2)
        {
            double aCos = Vector3D.Normarize(v1) * Vector3D.Normarize(v2);
            if (aCos > 1) return 0;
            if (aCos < -1) return Math.PI / 2;
            return Math.Acos(aCos);
        }

        //2つのステレオネット上の点の間の角度を返す
        public static double AngleBetStereoNetPoints(PointF pt1, PointF pt2)
        {
            double aCos = Vector3D.SphereVector(pt1) * Vector3D.SphereVector(pt2);
            if (aCos > 1) return 0;
            if (aCos < -1) return Math.PI / 2;
            return Math.Acos(aCos);
        }

        public static double AngleBetStereoNetPoints(PointD pt1, PointD pt2)
        {
            double aCos = Vector3D.SphereVector(pt1) * Vector3D.SphereVector(pt2);
            if (aCos > 1) return 0;
            if (aCos < -1) return Math.PI / 2;
            return Math.Acos(aCos);
        }

        //2つのベクトルの外積を返す
        public static Vector3D VectorProduct(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.Y * v2.Z - v1.Z * v2.Y, v1.Z * v2.X - v1.X * v2.Z, v1.X * v2.Y - v1.Y * v2.X);
        }

        /// <summary>
        /// 座標一ずつを加減算し、0から1の範囲内に収める
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3D InnerLattice(Vector3D v)
        {
            double[] d = new double[] { v.X, v.Y, v.Z };
            for (int j = 0; j < 3; j++)
            {
                while (d[j] < 0) { d[j] += 1; }
                while (d[j] >= 1) { d[j] -= 1; }
            }
            return new Vector3D(d[0], d[1], d[2], false);
        }

        /// <summary>
        /// 座標一ずつを加減算し、0から1の範囲内に収める
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public Vector3D InnerLattice()
        {
            double[] d = new double[] { X, Y, Z };
            for (int j = 0; j < 3; j++)
            {
                while (d[j] < 0) { d[j] += 1; }
                while (d[j] >= 1) { d[j] -= 1; }
            }
            return new Vector3D(d[0], d[1], d[2], false);
        }

        public override string ToString()
        {
            return text!="" ? text: $"{X}, {Y}, {Z}";
        }

        public static Vector3D RandomVector(Random rn)
        {
            return RandomVector(rn.NextDouble(), rn.NextDouble());
        }

        public static Vector3D RandomVector(double rn1, double rn2)
        {
            double rotZ = 2 * rn1 - 1;
            double rotTheta = rn2 * 2 * Math.PI;
            double rotX = Math.Sqrt(1 - rotZ * rotZ) * Math.Sin(rotTheta);
            double rotY = Math.Sqrt(1 - rotZ * rotZ) * Math.Cos(rotTheta);
            return new Vector3D(rotX, rotY, rotZ, false);
        }
    }

    /// <summary>
    /// 4元数と静的関数を提供
    /// </summary>
    public class Quaternion
    {
        public double W, X, Y, Z;

        public Quaternion()
        {
            W = X = Y = Z = 0;
        }

        public Quaternion(double w, double x, double y, double z)
        {
            W = w; X = x; Y = y; Z = z;
        }

        public Quaternion(Matrix3D r)
        {
            // 回転行列→クォータニオン変換

            double[] elem = new double[]{ // 0:x, 1:y, 2:z, 3:w
                r.E11 - r.E22 - r.E33 + 1.0,
               -r.E11 + r.E22 - r.E33 + 1.0,
               -r.E11 - r.E22 + r.E33 + 1.0,
                r.E11 + r.E22 + r.E33 + 1.0
            };

            // 最大成分を検索
            int biggestIndex = 0;
            for (int i = 1; i < 4; i++)
                if (elem[i] > elem[biggestIndex])
                    biggestIndex = i;

            if (elem[biggestIndex] > 0)
            {
                double v = Math.Sqrt(elem[biggestIndex]) * 0.5;
                double mult = 0.25 / v;

                switch (biggestIndex)
                {
                    case 0: // x
                        X = v;
                        Y = (r.E12 + r.E21) * mult;
                        Z = (r.E31 + r.E13) * mult;
                        W = (r.E32 - r.E23) * mult;
                        break;

                    case 1: // y
                        X = (r.E12 + r.E21) * mult;
                        Y = v;
                        Z = (r.E23 + r.E32) * mult;
                        W = (r.E13 - r.E31) * mult;
                        break;

                    case 2: // z
                        X = (r.E31 + r.E13) * mult;
                        Y = (r.E23 + r.E32) * mult;
                        Z = v;
                        W = (r.E21 - r.E12) * mult;
                        break;

                    case 3: // w
                        X = (r.E32 - r.E23) * mult;
                        Y = (r.E13 - r.E31) * mult;
                        Z = (r.E21 - r.E12) * mult;
                        W = v;
                        break;
                }
                if (W < 0)
                {
                    W = -W; X = -X; Y = -Y; Z = -Z;
                }
            }
        }

        public static Quaternion operator *(double d, Quaternion q)
        {
            return new Quaternion(d * q.W, d * q.X, d * q.Y, d * q.Z);
        }

        public static Quaternion operator *(Quaternion q, double d)
        {
            return new Quaternion(d * q.W, d * q.X, d * q.Y, d * q.Z);
        }

        public static Quaternion operator /(double d, Quaternion q)
        {
            return d * q.Invert();
        }

        public static Quaternion operator /(Quaternion q, double d)
        {
            return new Quaternion(q.W / d, q.X / d, q.Y / d, q.Z / d);
        }

        public static Quaternion operator *(Quaternion q1, Quaternion q2)
        {
            return new Quaternion(
                q1.W * q2.W - q1.X * q2.X - q1.Y * q2.Y - q1.Z * q2.Z,
                q1.W * q2.X + q1.X * q2.W + q1.Y * q2.Z - q1.Z * q2.Y,
                q1.W * q2.Y - q1.X * q2.Z + q1.Y * q2.W + q1.Z * q2.X,
                q1.W * q2.Z + q1.X * q2.Y - q1.Y * q2.X + q1.Z * q2.W);
        }

        public static Quaternion operator /(Quaternion q1, Quaternion q2)
        {
            return q1 * q2.Invert();
        }

        public static Quaternion operator +(Quaternion q1, Quaternion q2)
        {
            return new Quaternion(q1.W + q2.W, q1.X + q2.X, q1.Y + q2.Y, q1.Z + q2.Z);
        }

        public static Quaternion operator -(Quaternion q1, Quaternion q2)
        {
            return new Quaternion(q1.W - q2.W, q1.X - q2.X, q1.Y - q2.Y, q1.Z - q2.Z);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="q"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3DBase operator *(Quaternion q, Vector3DBase v)
        {
            double w1 = -q.X * v.X - q.Y * v.Y - q.Z * v.Z;
            double x1 = q.W * v.X + q.Y * v.Z - q.Z * v.Y;
            double y1 = q.W * v.Y - q.X * v.Z + q.Z * v.X;
            double z1 = q.W * v.Z + q.X * v.Y - q.Y * v.X;

            double x = -w1 * q.X + x1 * q.W - y1 * q.Z + z1 * q.Y;
            double y = -w1 * q.Y + x1 * q.Z + y1 * q.W - z1 * q.X;
            double z = -w1 * q.Z - x1 * q.Y + y1 * q.X + z1 * q.W;

            return new Vector3DBase(x, y, z);
        }

        /// <summary>
        /// 共役四元数 (X,Y,Zの符号を逆転させたもの) を得る
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public static Quaternion Conjugate(Quaternion q)
        {
            return new Quaternion(q.W, -q.X, -q.Y, -q.Z);
        }

        /// <summary>
        /// 共役四元数 (X,Y,Zの符号を逆転させたもの) を得る
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public Quaternion Conjugate()
        {
            return Conjugate(this);
        }

        /// <summary>
        /// 逆四元数 (W,X,Y,Zの符号を逆転させたもの) を得る
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public static Quaternion Invert(Quaternion q)
        {
            return new Quaternion(-q.W, -q.X, -q.Y, -q.Z);
        }

        /// <summary>
        /// 逆四元数 (X,Y,Zの符号を逆転させたもの) を得る
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public Quaternion Invert()
        {
            return Invert(this);
        }

        /// <summary>
        /// ノルムの自乗値を得る
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public static double NormSquared(Quaternion q)
        {
            return q.W * q.W + q.X * q.X + q.Y * q.Y + q.Z * q.Z;
        }

        /// <summary>
        /// ノルムの自乗値を得る
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public double NormSqured()
        {
            return NormSquared(this);
        }

        /// <summary>
        /// ノルム(長さ)を得る
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public static double Norm(Quaternion q)
        {
            return Math.Sqrt(q.NormSqured());
        }

        /// <summary>
        /// ノルム(長さ)を得る
        /// </summary>
        /// <returns></returns>
        public double Norm()
        {
            return Norm(this);
        }

        /// <summary>
        /// 正規化四元数(≒3次元回転)を得る
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public static Quaternion Normarize(Quaternion q)
        {
            return q / q.Norm();
        }

        /// <summary>
        /// 対数Quatanion (方向が回転軸で長さがθ/2のベクトル)を得る.
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public static Vector3DBase ToLogQuaternion(Quaternion q)
        {
            Vector3DBase v = new Vector3DBase(q.X, q.Y, q.Z);
            double theta = Math.Acos(q.W);
            if (q.W >= 1)
                return v;
            else
                return v / v.Length* theta;
        }

        /// <summary>
        /// 対数Quatanion (方向が回転軸で長さがθ/2のベクトル)を得る.
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public Vector3DBase ToLogQuaternion()
        {
            return ToLogQuaternion(this);
        }

        public static Quaternion FromLogQuaternion(Vector3DBase v)
        {
            double theta = v.Length;
            v = v / theta * Math.Sin(theta);
            return new Quaternion(Math.Cos(theta), v.X, v.Y, v.Z);
        }

        public static Matrix3D ToMatrix3D(Quaternion q)
        {
            return new Matrix3D(
                1.0 - 2.0 * q.Y * q.Y - 2.0 * q.Z * q.Z, //11
                2.0 * q.X * q.Y + 2.0 * q.W * q.Z,//12
                2.0 * q.X * q.Z - 2.0 * q.W * q.Y,//13

                2.0 * q.X * q.Y - 2.0 * q.W * q.Z,//21
                1.0 - 2.0 * q.X * q.X - 2.0 * q.Z * q.Z,//22
                2.0 * q.Y * q.Z + 2.0 * q.W * q.X,//23

                2.0 * q.X * q.Z + 2.0 * q.W * q.Y,//31
                2.0 * q.Y * q.Z - 2.0 * q.W * q.X,//32
                1.0 - 2.0 * q.X * q.X - 2.0 * q.Y * q.Y//33
            );
        }
    }
}