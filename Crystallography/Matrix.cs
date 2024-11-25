using MathNet.Numerics.LinearAlgebra;
using OpenTK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Xml.Serialization;

namespace Crystallography;

/// <summary>
/// 3�s3��s��ƐÓI�֐���� Eij��i�sj��̗v�f���Ӗ�����
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

    #region �R���X�g���N�^

    /// <summary>
    /// �R���X�g���N�^�B���������̏ꍇ�́A�P�ʍs���Ԃ�
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

    public double[] ToArrayRight() => [E11, E12, E13, E21, E22, E23, E31, E32, E33];

    /// <summary>
    /// E11, E12, E13, E21, E22, E23, E31, E32, E33
    /// </summary>
    /// <returns></returns>
    public double[] ToArrayRowMajorOrder() => [E11, E12, E13, E21, E22, E23, E31, E32, E33];

    /// <summary>
    /// E11, E21, E31, E12, E22, E32, E13, E23, E33
    /// </summary>
    /// <returns></returns>
    public double[] ToArrayColumnMajorOrder() => [E11, E21, E31, E12, E22, E32, E13, E23, E33];

    public Matrix3d ToMatrix() => new(E11, E12, E13, E21, E22, E23, m20: E31, E32, E33);

    public Vector3DBase Column1 => new(E11, E21, E31);
    public Vector3DBase Column2 => new(E12, E22, E32);
    public Vector3DBase Column3 => new(E13, E23, E33);
    public Vector3DBase Row1 => new(E11, E12, E13);
    public Vector3DBase Row2 => new(E21, E22, E23);
    public Vector3DBase Row3 => new(E31, E32, E33);

    #region  ���Z�q�̃I�[�o�[���[�h

    public static Matrix3D operator *(Matrix3D m1, Matrix3D m2) => new()
    {
        E11 = m1.E11 * m2.E11 + m1.E12 * m2.E21 + m1.E13 * m2.E31,
        E12 = m1.E11 * m2.E12 + m1.E12 * m2.E22 + m1.E13 * m2.E32,
        E13 = m1.E11 * m2.E13 + m1.E12 * m2.E23 + m1.E13 * m2.E33,

        E21 = m1.E21 * m2.E11 + m1.E22 * m2.E21 + m1.E23 * m2.E31,
        E22 = m1.E21 * m2.E12 + m1.E22 * m2.E22 + m1.E23 * m2.E32,
        E23 = m1.E21 * m2.E13 + m1.E22 * m2.E23 + m1.E23 * m2.E33,

        E31 = m1.E31 * m2.E11 + m1.E32 * m2.E21 + m1.E33 * m2.E31,
        E32 = m1.E31 * m2.E12 + m1.E32 * m2.E22 + m1.E33 * m2.E32,
        E33 = m1.E31 * m2.E13 + m1.E32 * m2.E23 + m1.E33 * m2.E33
    };

    public static Matrix3D operator *(in double d, Matrix3D m2) => new()
    {
        E11 = d * m2.E11,
        E12 = d * m2.E12,
        E13 = d * m2.E13,

        E21 = d * m2.E21,
        E22 = d * m2.E22,
        E23 = d * m2.E23,

        E31 = d * m2.E31,
        E32 = d * m2.E32,
        E33 = d * m2.E33
    };

    public static Matrix3D operator +(Matrix3D m1, Matrix3D m2) => new()
    {
        E11 = m1.E11 + m2.E11,
        E12 = m1.E12 + m2.E12,
        E13 = m1.E13 + m2.E13,

        E21 = m1.E21 + m2.E21,
        E22 = m1.E22 + m2.E22,
        E23 = m1.E23 + m2.E23,

        E31 = m1.E31 + m2.E31,
        E32 = m1.E32 + m2.E32,
        E33 = m1.E33 + m2.E33
    };

    public static Matrix3D operator -(Matrix3D m1, Matrix3D m2) => new()
    {
        E11 = m1.E11 - m2.E11,
        E12 = m1.E12 - m2.E12,
        E13 = m1.E13 - m2.E13,

        E21 = m1.E21 - m2.E21,
        E22 = m1.E22 - m2.E22,
        E23 = m1.E23 - m2.E23,

        E31 = m1.E31 - m2.E31,
        E32 = m1.E32 - m2.E32,
        E33 = m1.E33 - m2.E33
    };

    public static Matrix3D operator -(Matrix3D m1) => new()
    {
        E11 = -m1.E11,
        E12 = -m1.E12,
        E13 = -m1.E13,

        E21 = -m1.E21,
        E22 = -m1.E22,
        E23 = -m1.E23,

        E31 = -m1.E31,
        E32 = -m1.E32,
        E33 = -m1.E33
    };

    public static Vector3D operator *(Matrix3D m, Vector3D v) => m == null || v == null
            ? null
            : new Vector3D(m.E11 * v.X + m.E12 * v.Y + m.E13 * v.Z, m.E21 * v.X + m.E22 * v.Y + m.E23 * v.Z, m.E31 * v.X + m.E32 * v.Y + m.E33 * v.Z);

    public static Vector3DBase operator *(Matrix3D m, Vector3DBase v)
        => new(m.E11 * v.X + m.E12 * v.Y + m.E13 * v.Z, m.E21 * v.X + m.E22 * v.Y + m.E23 * v.Z, m.E31 * v.X + m.E32 * v.Y + m.E33 * v.Z);


    /// <summary>
    /// Matrix3D�ƃ^�v��(x,y,z)�̏�Z. (x,y,z)���c�����̃x�N�g���Ƃ��Čv�Z����B
    /// </summary>
    /// <param name="m"></param>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector3DBase operator *(Matrix3D m, in (int X, int Y, int Z) v)
        => new(m.E11 * v.X + m.E12 * v.Y + m.E13 * v.Z, m.E21 * v.X + m.E22 * v.Y + m.E23 * v.Z, m.E31 * v.X + m.E32 * v.Y + m.E33 * v.Z);

    public static Vector3DBase operator *(Matrix3D m, in (double X, double Y, double Z) v)
        => new(m.E11 * v.X + m.E12 * v.Y + m.E13 * v.Z, m.E21 * v.X + m.E22 * v.Y + m.E23 * v.Z, m.E31 * v.X + m.E32 * v.Y + m.E33 * v.Z);

    public static Vector3d operator *(Matrix3D m, Vector3d v)
        => new(m.E11 * v.X + m.E12 * v.Y + m.E13 * v.Z, m.E21 * v.X + m.E22 * v.Y + m.E23 * v.Z, m.E31 * v.X + m.E32 * v.Y + m.E33 * v.Z);

    #endregion
    public static Matrix3D Inverse(Matrix3D m)
    {
        var det = -m.E13 * m.E22 * m.E31 + m.E12 * m.E23 * m.E31 + m.E13 * m.E21 * m.E32 - m.E11 * m.E23 * m.E32 - m.E12 * m.E21 * m.E33 + m.E11 * m.E22 * m.E33;
        return det == 0
            ? new Matrix3D()
            : new Matrix3D
            {
                E11 = (-m.E23 * m.E32 + m.E22 * m.E33) / det,
                E12 = (m.E13 * m.E32 - m.E12 * m.E33) / det,
                E13 = (-m.E13 * m.E22 + m.E12 * m.E23) / det,
                E21 = (m.E23 * m.E31 - m.E21 * m.E33) / det,
                E22 = (-m.E13 * m.E31 + m.E11 * m.E33) / det,
                E23 = (m.E13 * m.E21 - m.E11 * m.E23) / det,
                E31 = (-m.E22 * m.E31 + m.E21 * m.E32) / det,
                E32 = (m.E12 * m.E31 - m.E11 * m.E32) / det,
                E33 = (-m.E12 * m.E21 + m.E11 * m.E22) / det
            };
    }

    public Matrix3D Inverse() => Inverse(this);

    public static Matrix3D Transpose(Matrix3D m) => new(m.E11, m.E12, m.E13, m.E21, m.E22, m.E23, m.E31, m.E32, m.E33);

    public Matrix3D Transpose() => Transpose(this);

    public static double Determinant(Matrix3D m) => -m.E13 * m.E22 * m.E31 + m.E12 * m.E23 * m.E31 + m.E13 * m.E21 * m.E32 - m.E11 * m.E23 * m.E32 - m.E12 * m.E21 * m.E33 + m.E11 * m.E22 * m.E33;

    public double Determinant() => Determinant(this);

    public Matrix3D ExchangeX_Y_Z() => ExchangeX_Y_Z(this);

    public static Matrix3D ExchangeX_Y_Z(Matrix3D m) => new(m.E11, -m.E21, -m.E31, m.E12, -m.E22, -m.E32, m.E13, -m.E23, -m.E33);

    public Matrix3D ExchangeYZX() => ExchangeYZX(this);

    public static Matrix3D ExchangeYZX(Matrix3D m) => new(m.E21, m.E31, m.E11, m.E22, m.E32, m.E12, m.E23, m.E33, m.E13);

    public Matrix3D ExchangeY_Z_X() => ExchangeY_Z_X(this);

    public static Matrix3D ExchangeY_Z_X(Matrix3D m) => new(-m.E21, -m.E31, m.E11, -m.E22, -m.E32, m.E12, -m.E23, -m.E33, m.E13);

    public Matrix3D ExchangeZXY() => ExchangeZXY(this);

    public static Matrix3D ExchangeZXY(Matrix3D m) => new(m.E31, m.E11, m.E21, m.E32, m.E12, m.E22, m.E33, m.E13, m.E23);

    public Matrix3D ExchangeZ_X_Y() => ExchangeZ_X_Y(this);

    public static Matrix3D ExchangeZ_X_Y(Matrix3D m) => new(-m.E31, m.E11, -m.E21, -m.E32, m.E12, -m.E22, -m.E33, m.E13, -m.E23);


    /// <summary>
    /// �x�N�g��v�̕����̎����,theta������]������s��𐶐�����
    /// </summary>
    /// <param name="v">��]��</param>
    /// <param name="theta">��]�p�x</param>
    /// <returns></returns>
    public static Matrix3D Rot(Vector3DBase v, in double theta) => Rot((v.X, v.Y, v.Z), theta);

    /// <summary>
    /// �x�N�g��v�̕����̎����,theta������]������s��𐶐�����
    /// </summary>
    /// <param name="v">��]��</param>
    /// <param name="theta">��]�p�x</param>
    /// <returns></returns>
    public static Matrix3D Rot((double X, double Y, double Z) v, in double theta)
    {
        //Vx*Vx*(1-cos) + cos  	    Vx*Vy*(1-cos) - Vz*sin  	Vz*Vx*(1-cos) + Vy*sin
        //Vx*Vy*(1-cos) + Vz*sin 	Vy*Vy*(1-cos) + cos 	    Vy*Vz*(1-cos) - Vx*sin
        //Vz*Vx*(1-cos) - Vy*sin 	Vy*Vz*(1-cos) + Vx*sin 	    Vz*Vz*(1-cos) + cos

        if (v.X == 0 && v.Y == 0 && v.Z == 0)
            return new Matrix3D();
        v = Vector3DBase.Normarize(v);
        double X = v.X, Y = v.Y, Z = v.Z;

        var cos = Math.Cos(theta);
        var oneMinusCos = 1 - cos;
        var sin = Math.Sin(theta);

        var nxyc = X * Y * oneMinusCos;
        var nyzc = Y * Z * oneMinusCos;
        var nzxc = Z * X * oneMinusCos;
        var nxs = X * sin;
        var nys = Y * sin;
        var nzs = Z * sin;
        return new Matrix3D
        {
            E11 = oneMinusCos * X * X + cos,
            E12 = nxyc - nzs,
            E13 = nzxc + nys,

            E21 = nxyc + nzs,
            E22 = oneMinusCos * Y * Y + cos,
            E23 = nyzc - nxs,

            E31 = nzxc - nys,
            E32 = nyzc + nxs,
            E33 = oneMinusCos * Z * Z + cos
        };
    }
    public static Matrix3D Rot(Vector3d v, double theta) => Rot(new Vector3DBase(v.X, v.Y, v.Z), theta);

    /// <summary>
    /// �I�C���[�p(Z-X-Z�Z�b�e�B���O)���w�肵�ĉ�]�s��𐶐�����B
    /// </summary>
    /// <param name="phi"></param>
    /// <param name="theta"></param>
    /// <param name="psi"></param>
    /// <returns></returns>
    public static Matrix3D Rot(double phi, double theta, double psi)
    {
        double cosPhi = Math.Cos(phi), sinPhi = Math.Sin(phi);
        double cosTheta = Math.Cos(theta), sinTheta = Math.Sin(theta);
        double cosPsi = Math.Cos(psi), sinPsi = Math.Sin(psi);

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

    /// <summary>
    /// X���̉���theta��]����s��𐶐�����
    /// </summary>
    /// <param name="theta"></param>
    /// <returns></returns>
    public static Matrix3D RotX(double theta)
    {
        double cos = Math.Cos(theta), sin = Math.Sin(theta);
        return new Matrix3D()
        {
            E22 = cos,
            E23 = -sin,
            E32 = sin,
            E33 = cos
        };
    }

    /// <summary>
    /// Y���̉���theta��]����s��𐶐�����
    /// </summary>
    /// <param name="theta"></param>
    /// <returns></returns>
    public static Matrix3D RotY(double theta)
    {
        double cos = Math.Cos(theta), sin = Math.Sin(theta);
        return new Matrix3D()
        {
            E11 = cos,
            E13 = sin,
            E31 = -sin,
            E33 = cos
        };
    }

    /// <summary>
    /// Z���̉���theta��]����s��𐶐�����
    /// </summary>
    /// <param name="theta"></param>
    /// <returns></returns>
    public static Matrix3D RotZ(double theta)
    {
        double cos = Math.Cos(theta), sin = Math.Sin(theta);
        return new Matrix3D()
        {
            E11 = cos,
            E12 = -sin,
            E21 = sin,
            E22 = cos
        };
    }

    private static readonly Random Rn = new(Environment.TickCount * 2);

    public static Matrix3D GenerateRamdomRotationMatrix() => GenerateRamdomRotationMatrix(Rn);

    public static Matrix3D GenerateRamdomRotationMatrix(int seed) => GenerateRamdomRotationMatrix(new Random(seed));

    private static readonly Lock lockObj = new();
    public static Matrix3D GenerateRamdomRotationMatrix(Random rn)
    {
        double rn1, rn2, rn3;
        lock (lockObj)
        {
            rn1 = rn.NextDouble();
            rn2 = rn.NextDouble();
            rn3 = rn.NextDouble();
        }
        return GenerateRamdomRotationMatrix(rn1, rn2, rn3);
    }

    public static Matrix3D GenerateRamdomRotationMatrix(in double rn1, in double rn2, in double rn3)
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
    /// ���ʐϋ�Ԃ̍s����쐬, phi, theta, ksi��0~1�͈̔͂œ���
    /// </summary>
    /// <param name="phi"></param>
    /// <param name="theta"></param>
    /// <param name="ksi"></param>
    /// <returns></returns>
    public static Matrix3D GenerateEquiareaMatrix(in double phi, in double theta, in double ksi)
    {
        double cosPhi = Math.Cos(phi * 2 * Math.PI), sinPhi = Math.Sin(phi * 2 * Math.PI);
        double cosTheta = theta * 2 - 1, sinTheta = Math.Sqrt(1 - cosTheta * cosTheta);
        double cosKsi = Math.Cos(ksi * 2 * Math.PI), sinKsi = Math.Sin(ksi * 2 * Math.PI);

        var m1 = new Matrix3D(cosPhi, -sinPhi, 0, sinPhi, cosPhi, 0, 0, 0, 1);
        //Matrix3D m1 = new Matrix3D(cosPhi, 0, sinPhi, 0, 1, 0, -sinPhi, 0, cosPhi);
        var m2 = new Matrix3D(1, 0, 0, 0, cosTheta, sinTheta, 0, -sinTheta, cosTheta);
        var m3 = new Matrix3D(cosKsi, -sinKsi, 0, sinKsi, cosKsi, 0, 0, 0, 1);

        return m1 * m2 * m3;
    }

    /// <summary>
    /// �[���s�񂩂ǂ����𔻒�
    /// </summary>
    /// <param name="m"></param>
    /// <returns></returns>
    public static bool IsZero(Matrix3D m)
        => m.E11 == 0 && m.E12 == 0 && m.E13 == 0 && m.E21 == 0 && m.E22 == 0 && m.E23 == 0 && m.E31 == 0 && m.E32 == 0 && m.E33 == 0;

    /// <summary>
    /// �[���s�񂩂ǂ����𔻒�
    /// </summary>
    /// <returns></returns>
    public bool IsZero() => IsZero(this);

    /// <summary>
    /// �P�ʍs�񂩂ǂ����𔻒�
    /// </summary>
    /// <param name="m"></param>
    /// <returns></returns>
    public static bool IsIdentity(Matrix3D m)
    => m.E11 == 1 && m.E12 == 0 && m.E13 == 0 && m.E21 == 0 && m.E22 == 1 && m.E23 == 0 && m.E31 == 0 && m.E32 == 0 && m.E33 == 1;

    /// <summary>
    /// �P�ʍs�񂩂ǂ����𔻒�
    /// </summary>
    /// <returns></returns>
    public bool IsIdentity() => IsIdentity(this);

    /// <summary>
    /// �Ίp�����̘a�����߂�
    /// </summary>
    /// <returns></returns>
    public double SumOfDiagonalCompenent() => SumOfDiagonalCompenent(this);

    /// <summary>
    /// �Ίp�����̘a�����߂�
    /// </summary>
    /// <param name="m"></param>
    /// <returns></returns>
    public static double SumOfDiagonalCompenent(Matrix3D m) => m.E11 + m.E22 + m.E33;

    /// <summary>
    /// �[���s�� (�萔)
    /// </summary>
    public static readonly Matrix3D ZeroMatrix = new(0, 0, 0, 0, 0, 0, 0, 0, 0);

    /// <summary>
    /// �P�ʍs�� (�萔)
    /// </summary>
    public static readonly Matrix3D IdentityMatrix = new(1, 0, 0, 0, 1, 0, 0, 0, 1);

    public bool Equals(Matrix3D m)
    {
        return
            m.E11 == E11 && m.E12 == E12 && m.E13 == E13 &&
            m.E21 == E21 && m.E22 == E22 && m.E23 == E23 &&
            m.E31 == E31 && m.E32 == E32 && m.E33 == E33;
    }
}

/// <summary>
/// 3�����x�N�g���̊�{�I�ȋ@�\��񋟂���
/// </summary>
[Serializable()]
[TypeConverter(typeof(Vector3DConverter))]
public class Vector3DBase : ICloneable
{
    public static readonly Vector3DBase Zero = new(0, 0, 0);
    private object arr;

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

    public Vector3DBase((double X, double Y, double Z) v)
    {
        X = v.X; Y = v.Y; Z = v.Z;
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

    public double[] ToDoublearray() => [X, Y, Z];

    public float[] ToSingleArray() => [(float)X, (float)Y, (float)Z];

    /// <summary>
    /// X,Y���W��PointD�N���X�ɉf�� (Z�͔j��)
    /// </summary>
    /// <returns></returns>
    public PointD ToPointD => new(X, Y);

    #region ���Z�q�̃I�[�o�[���[�h

    public static bool operator <(Vector3DBase v1, Vector3DBase v2) => (v1.X < v2.X && v1.Y < v2.Y && v1.Z < v2.Z);

    public static bool operator <=(Vector3DBase v1, Vector3DBase v2) => (v1.X <= v2.X && v1.Y <= v2.Y && v1.Z <= v2.Z);

    public static bool operator >(Vector3DBase v1, Vector3DBase v2) => (v1.X > v2.X && v1.Y > v2.Y && v1.Z > v2.Z);

    public static bool operator >=(Vector3DBase v1, Vector3DBase v2) => (v1.X >= v2.X && v1.Y >= v2.Y && v1.Z >= v2.Z);

    public static Vector3DBase operator +(Vector3DBase v1, Vector3DBase v2) => new(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);

    public static Vector3DBase operator -(Vector3DBase v1, Vector3DBase v2) => new(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
    public static Vector3DBase operator -(Vector3DBase v1) => new(-v1.X, -v1.Y, -v1.Z);

    public static Vector3DBase operator *(in double d, Vector3DBase v1) => new(d * v1.X, d * v1.Y, d * v1.Z);

    public static Vector3DBase operator *(Vector3DBase v1, in double d) => new(d * v1.X, d * v1.Y, d * v1.Z);

    public static Vector3DBase operator *(in int d, Vector3DBase v1) => new(d * v1.X, d * v1.Y, d * v1.Z);

    public static Vector3DBase operator *(Vector3DBase v1, in int d) => new(d * v1.X, d * v1.Y, d * v1.Z);

    public static double operator *(Vector3DBase v1, Vector3DBase v2) => v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;

    public static double operator *(Vector3DBase v1, in (int X, int Y, int Z) v2) => v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;

    public static double operator *(Vector3DBase v1, in (double X, double Y, double Z) v2) => v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;

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

    public static Vector3DBase operator /(Vector3DBase v1, in double d) => new(v1.X / d, v1.Y / d, v1.Z / d);

    public static Vector3DBase operator /(Vector3DBase v1, in int d) => new(v1.X / d, v1.Y / d, v1.Z / d);

    #endregion ���Z�q�̃I�[�o�[���[�h

    /// <summary>
    /// ���_����̒�����Ԃ�
    /// </summary>
    /// <returns></returns>
    public double Length => Math.Sqrt(Length2);

    /// <summary>
    /// ���_����̒����̓���Ԃ�
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
    public double X2 => X * X;
    public double Y2 => Y * Y;
    public double Z2 => Z * Z;

    public (double X, double Y, double Z) Tuple => (X, Y, Z);

    public Vector3d ToOpenTK() => new(X, Y, Z);

    internal static Vector3DBase Normarize(Vector3DBase v)
    {
        double l = Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
        return l > 0 ? new Vector3DBase(v.X / l, v.Y / l, v.Z / l) : v;
    }

    internal static (double X, double Y, double Z) Normarize((double X, double Y, double Z) v)
    {
        double l = Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
        return l > 0 ? (v.X / l, v.Y / l, v.Z / l) : (v);
    }

    public Vector3DBase Normarize() => Normarize(this);

    public void NormarizeThis()
    {
        double l = Math.Sqrt(X * X + Y * Y + Z * Z);
        if (l > 0)
        {
            X /= l;
            Y /= l;
            Z /= l;
        }
    }

    public Vector3D ToVector3D() => new(X, Y, Z);


    /// <summary>
    /// 2�̃x�N�g���̊O�ς�Ԃ�
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    public static Vector3DBase VectorProduct(Vector3DBase v1, Vector3DBase v2)
        => new(v1.Y * v2.Z - v1.Z * v2.Y, v1.Z * v2.X - v1.X * v2.Z, v1.X * v2.Y - v1.Y * v2.X);


    /// <summary>
    /// 2�̃x�N�g���̊O�ς�Ԃ�
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    public static Vector3DBase VectorProduct(in (double X, double Y, double Z) v1, in (double X, double Y, double Z) v2)
        => new(v1.Y * v2.Z - v1.Z * v2.Y, v1.Z * v2.X - v1.X * v2.Z, v1.X * v2.Y - v1.Y * v2.X);

    /// <summary>
    /// 2�̃x�N�g���Ԃ̊p�x��Ԃ�
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>

    public static double AngleBetVectors(in Vector3DBase v1, in Vector3DBase v2)
    {
        var aCos = Normarize(v1) * Normarize(v2);
        if (aCos > 1)
            return 0;
        else
            return aCos < -1 ? Math.PI / 2 : Math.Acos(aCos);
    }

    /// <summary>
    /// ���ϒl��Ԃ�
    /// </summary>
    /// <param name="vectors"></param>
    /// <returns></returns>
    public static Vector3DBase Average(IEnumerable<Vector3DBase> vectors)
    {
        double x = 0, y = 0, z = 0;
        foreach (var v in vectors)
        {
            x += v.X;
            y += v.Y;
            z += v.Z;
        }
        var count = vectors.Count();
        return new Vector3DBase(x / count, y / count, z / count);
    }

    public override string ToString() => string.Format("({0}, {1}, {2})", this.X, this.Y, this.Z);
}

/// <summary>
/// 3�����x�N�g���ƐÓI�֐����
/// </summary>
[Serializable()]
public class Vector3D : Vector3DBase, IComparable<Vector3D>, ICloneable
{
    public new object Clone() => (Vector3D)this.MemberwiseClone();

    public double d { get; set; }
    public string Text { get; set; }

    /// <summary>
    /// �����l��false
    /// </summary>
    public bool Flag1 { get; set; } = false;

    /// <summary>
    /// �����l��false
    /// </summary>
    public bool Flag2 { get; set; } = false;

    [XmlIgnore]
    public string[] Extinction { get; set; }

    public int Argb { get; set; }

    public (int h, int k, int l) Index { get; set; }

    public double RelativeIntensity { get; set; } = 1;
    public double RawIntensity { get; set; }

    [XmlIgnore]
    public Complex F { get; set; }

    [XmlIgnore]
    public object Tag { get; set; }

    [XmlIgnore]
    public SymmetryOperation Operation { get; set; }

    [XmlIgnore]
    public Vector3DBase Coordinates { get => new(X, Y, Z); set { X = value.X; Y = value.Y; Z = value.Z; } }

    public int CompareTo(Vector3D v)
    {
        if (d != v.d)
            return -d.CompareTo(v.d);
        else if (X != v.X)
            return -X.CompareTo(v.X);
        else if (Y != v.Y)
            return -Y.CompareTo(v.Y);
        else if (Z != v.Z)
            return -Z.CompareTo(v.Z);
        else
            return 0;
    }

    public Vector3D()
    {
        X = 0; Y = 0; Z = 0; d = 0;
    }

    public Vector3D(in double x, in double y, in double z)
    {
        X = x; Y = y; Z = z;
        //d2 = X * X + Y * Y + Z * Z;
        d = Math.Sqrt(X * X + Y * Y + Z * Z);
    }

    public Vector3D(in double x, in double y, in double z, in bool IsCalcD = true)
    {
        X = x; Y = y; Z = z;
        if (IsCalcD)
            d = Math.Sqrt(X * X + Y * Y + Z * Z);
    }

    public Vector3D(Vector3DBase v, in bool IsCalcD = true)
    {
        X = v.X; Y = v.Y; Z = v.Z;
        if (IsCalcD)
            d = Math.Sqrt(X * X + Y * Y + Z * Z);
    }

    public Vector3D(double[] v)
    {
        if (v.Length == 3)
        {
            X = v[0]; Y = v[1]; Z = v[2];
        }
        else
            X = Y = Z = 0;
    }

    public Vector3D(float[] v)
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

    //���Z�q�̃I�[�o�[���[�h
    public static bool operator <(Vector3D v1, Vector3D v2) => v1.X < v2.X && v1.Y < v2.Y && v1.Z < v2.Z;

    public static bool operator <=(Vector3D v1, Vector3D v2) => v1.X <= v2.X && v1.Y <= v2.Y && v1.Z <= v2.Z;

    public static bool operator >(Vector3D v1, Vector3D v2) => v1.X > v2.X && v1.Y > v2.Y && v1.Z > v2.Z;

    public static bool operator >=(Vector3D v1, Vector3D v2) => v1.X >= v2.X && v1.Y >= v2.Y && v1.Z >= v2.Z;

    public static Vector3D operator +(Vector3D v1, Vector3D v2)
    {
        if (v2 is null)
            throw new ArgumentNullException(nameof(v2));
        return new Vector3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
    }

    public static Vector3D operator -(Vector3D v1, Vector3D v2) => new(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);

    public static Vector3D operator -(Vector3D v1) => new(-v1.X, -v1.Y, -v1.Z);

    public static Vector3D operator *(in double d, Vector3D v1) => new(d * v1.X, d * v1.Y, d * v1.Z);

    public static Vector3D operator *(Vector3D v1, in double d) => new(d * v1.X, d * v1.Y, d * v1.Z);

    public static Vector3D operator *(in int d, Vector3D v1) => new(d * v1.X, d * v1.Y, d * v1.Z);

    public static Vector3D operator *(Vector3D v1, in int d) => new(d * v1.X, d * v1.Y, d * v1.Z);

    public static double operator *(Vector3D v1, Vector3D v2) => v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;

    public static Vector3D operator /(Vector3D v1, in double d) => new(v1.X / d, v1.Y / d, v1.Z / d);

    public static Vector3D operator /(Vector3D v1, int d) => new(v1.X / d, v1.Y / d, v1.Z / d);

    public new void NormarizeThis()
    {
        Vector3D v = Normarize(this);
        X = v.X;
        Y = v.Y;
        Z = v.Z;
    }

    /// <summary>
    /// ��̃x�N�g���Ԃ̋�����Ԃ�
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    public static double LengthBetVectors(Vector3D v1, Vector3D v2)
    {
        return Math.Sqrt((v1.X - v2.X) * (v1.X - v2.X) + (v1.Y - v2.Y) * (v1.Y - v2.Y) + (v1.Z - v2.Z) * (v1.Z - v2.Z));
    }

    /// <summary>
    /// ��̃x�N�g���Ԃ̋�����2���Ԃ�
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    public static double LengthSquareBetVectors(Vector3D v1, Vector3D v2)
    {
        return (v1.X - v2.X) * (v1.X - v2.X) + (v1.Y - v2.Y) * (v1.Y - v2.Y) + (v1.Z - v2.Z) * (v1.Z - v2.Z);
    }

    /// <summary>
    /// v�̕����������Œ�����1�̃x�N�g����Ԃ�
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector3D Normarize(Vector3D v)
    {
        double l = Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
        return l > 0 ? new Vector3D(v.X / l, v.Y / l, v.Z / l) : v;
    }

    /// <summary>
    /// 3�����x�N�g��v�̃X�e���I�l�b�g��ł̈ʒu��PointF�ŕԂ�
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

    //�X�e���I�l�b�g��ł̈ʒu�ɑΉ�����3�����x�N�g����Ԃ�
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

    //2��3�����x�N�g���Ԃ̊p�x��Ԃ�
    public static double AngleBetVectors(Vector3D v1, Vector3D v2)
    {
        double aCos = Vector3D.Normarize(v1) * Vector3D.Normarize(v2);
        if (aCos > 1) return 0;
        if (aCos < -1) return Math.PI / 2;
        return Math.Acos(aCos);
    }

    //2�̃X�e���I�l�b�g��̓_�̊Ԃ̊p�x��Ԃ�
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

    //2�̃x�N�g���̊O�ς�Ԃ�
    public static Vector3D VectorProduct(Vector3D v1, Vector3D v2)
        => new(v1.Y * v2.Z - v1.Z * v2.Y, v1.Z * v2.X - v1.X * v2.Z, v1.X * v2.Y - v1.Y * v2.X);

    /// <summary>
    /// ���W�ꂸ�������Z���A0����1�͈͓̔��Ɏ��߂�
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector3D InnerLattice(Vector3D v)
    {
        double x = v.X, y = v.Y, z = v.Z;
        if (x >= 1 || x < 0) x -= Math.Floor(x);
        if (y >= 1 || y < 0) y -= Math.Floor(y);
        if (z >= 1 || z < 0) z -= Math.Floor(z);
        return new Vector3D(x, y, z, false);
    }

    /// <summary>
    /// ���W�ꂸ�������Z���A0����1�͈͓̔��Ɏ��߂�
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public Vector3D InnerLattice()
    {
        double x = X, y = Y, z = Z;
        if (x >= 1 || x < 0) x -= Math.Floor(x);
        if (y >= 1 || y < 0) y -= Math.Floor(y);
        if (z >= 1 || z < 0) z -= Math.Floor(z);
        return new Vector3D(x, y, z, false);
    }

    /// <summary>
    /// ���W�ꂸ�������Z���A0����1�͈͓̔��Ɏ��߂�
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public void InnerLatticeThis()
    {
        if (X >= 1 || X < 0) X -= Math.Floor(X);
        if (Y >= 1 || Y < 0) Y -= Math.Floor(Y);
        if (Z >= 1 || Z < 0) Z -= Math.Floor(Z);
    }

    public override string ToString() => Text != "" ? Text : $"{X}, {Y}, {Z}";

    public static Vector3D RandomVector(Random rn) => RandomVector(rn.NextDouble(), rn.NextDouble());

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
/// 4�����ƐÓI�֐����
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
        // ��]�s�񁨃N�H�[�^�j�I���ϊ�

        double[] elem = [ // 0:x, 1:y, 2:z, 3:w
                r.E11 - r.E22 - r.E33 + 1.0,
               -r.E11 + r.E22 - r.E33 + 1.0,
               -r.E11 - r.E22 + r.E33 + 1.0,
                r.E11 + r.E22 + r.E33 + 1.0
            ];

        // �ő听��������
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
    /// �����l���� (X,Y,Z�̕������t�]����������) �𓾂�
    /// </summary>
    /// <param name="q"></param>
    /// <returns></returns>
    public static Quaternion Conjugate(Quaternion q)
    {
        return new Quaternion(q.W, -q.X, -q.Y, -q.Z);
    }

    /// <summary>
    /// �����l���� (X,Y,Z�̕������t�]����������) �𓾂�
    /// </summary>
    /// <param name="q"></param>
    /// <returns></returns>
    public Quaternion Conjugate()
    {
        return Conjugate(this);
    }

    /// <summary>
    /// �t�l���� (W,X,Y,Z�̕������t�]����������) �𓾂�
    /// </summary>
    /// <param name="q"></param>
    /// <returns></returns>
    public static Quaternion Invert(Quaternion q)
    {
        return new Quaternion(-q.W, -q.X, -q.Y, -q.Z);
    }

    /// <summary>
    /// �t�l���� (X,Y,Z�̕������t�]����������) �𓾂�
    /// </summary>
    /// <param name="q"></param>
    /// <returns></returns>
    public Quaternion Invert()
    {
        return Invert(this);
    }

    /// <summary>
    /// �m�����̎���l�𓾂�
    /// </summary>
    /// <param name="q"></param>
    /// <returns></returns>
    public static double NormSquared(Quaternion q)
    {
        return q.W * q.W + q.X * q.X + q.Y * q.Y + q.Z * q.Z;
    }

    /// <summary>
    /// �m�����̎���l�𓾂�
    /// </summary>
    /// <param name="q"></param>
    /// <returns></returns>
    public double NormSqured()
    {
        return NormSquared(this);
    }

    /// <summary>
    /// �m����(����)�𓾂�
    /// </summary>
    /// <param name="q"></param>
    /// <returns></returns>
    public static double Norm(Quaternion q)
    {
        return Math.Sqrt(q.NormSqured());
    }

    /// <summary>
    /// �m����(����)�𓾂�
    /// </summary>
    /// <returns></returns>
    public double Norm()
    {
        return Norm(this);
    }

    /// <summary>
    /// ���K���l����(��3������])�𓾂�
    /// </summary>
    /// <param name="q"></param>
    /// <returns></returns>
    public static Quaternion Normarize(Quaternion q)
    {
        return q / q.Norm();
    }

    /// <summary>
    /// �ΐ�Quatanion (��������]���Œ�������/2�̃x�N�g��)�𓾂�.
    /// </summary>
    /// <param name="q"></param>
    /// <returns></returns>
    public static Vector3DBase ToLogQuaternion(Quaternion q)
    {
        Vector3DBase v = new(q.X, q.Y, q.Z);
        double theta = Math.Acos(q.W);
        if (q.W >= 1)
            return v;
        else
            return v / v.Length * theta;
    }

    /// <summary>
    /// �ΐ�Quatanion (��������]���Œ�������/2�̃x�N�g��)�𓾂�.
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
