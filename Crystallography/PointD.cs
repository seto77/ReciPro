using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Crystallography;

[StructLayout(LayoutKind.Sequential)]
[Serializable()]
[TypeConverter(typeof(RectangleDConverter))]
public struct RectangleD
{
    public double X { set; get; }
    public double Y { set; get; }
    public double Width { set; get; }
    public double Height { set; get; }

    public double UpperX { get { return X + Width; } }
    public double UpperY { get { return Y + Height; } }

    public readonly bool IsInsde(PointD p)
    {
        return p.X >= X && p.X <= X + Width && p.Y >= Y && p.Y <= Y + Height;
    }

    public RectangleD(double x, double y, double width, double height) : this()
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public RectangleD(int x, int y, int width, int height) : this()
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public RectangleD(PointD pt, SizeD size) : this()
    {
        X = pt.X;
        Y = pt.Y;
        Width = size.Width;
        Height = size.Height;
    }

    public RectangleD(Point pt, Size size) : this()
    {
        X = pt.X;
        Y = pt.Y;
        Width = size.Width;
        Height = size.Height;
    }

    public RectangleD(PointD pt1, PointD pt2) : this()
    {
        X = Math.Min(pt1.X, pt2.X);
        Y = Math.Min(pt1.Y, pt2.Y);
        Width = Math.Abs(pt2.X - pt1.X);
        Height = Math.Abs(pt2.Y - pt1.Y);
    }

    public readonly RectangleF ToRectangleF() => new RectangleF((float)X, (float)Y, (float)Width, (float)Height);

    public readonly SizeD ToSizeD() => new SizeD(Width, Height);
    public readonly SizeF ToSizeF() => new SizeF((float)Width, (float)Height);

    /// <summary>
    /// 四捨五入して整数サイズに変換
    /// </summary>
    /// <returns></returns>
    public readonly Size ToSize() => new Size((int)(Width + 0.5), (int)(Height + 0.5));

}

[StructLayout(LayoutKind.Sequential)]
[Serializable]
[TypeConverter(typeof(SizeDConverter))]
public struct SizeD
{
    public double Width { set; get; }
    public double Height { set; get; }

    public SizeD(double width, double height) : this()
    {
        Width = width;
        Height = height;
    }

    public SizeD(int width, int height) : this()
    {
        Width = width;
        Height = height;
    }

    public SizeD(SizeD size) : this()
    {
        Width = size.Width;
        Height = size.Height;
    }

    public SizeD(Size size) : this()
    {
        Width = size.Width;
        Height = size.Height;
    }

    public readonly SizeF ToSizeF()
    {
        return new SizeF((float)Width, (float)Height);
    }

    #region 演算子のオーバーロード

    public override int GetHashCode()
    {
        return HashCode.Combine(Width, Height);
    }

    public override bool Equals(object obj) => obj is SizeD d && Equals(d);

    public bool Equals(in SizeD other) => Width == other.Width && Height == other.Height;

    public static SizeD operator +(in SizeD p1, in SizeD p2) => new(p1.Width + p2.Width, p1.Height + p2.Height);

    public static SizeD operator -(in SizeD p1, in SizeD p2) => new(p1.Width - p2.Width, p1.Height - p2.Height);

    public static SizeD operator -(in SizeD p) => new SizeD(-p.Width, -p.Height);

    public static SizeD operator *(in double d, in SizeD p) => new(d * p.Width, d * p.Height);

    public static SizeD operator *(in SizeD p, in double d) => new(d * p.Width, d * p.Height);

    public static SizeD operator /(in SizeD p, in double d) => new(p.Width / d, p.Height / d);

    public static bool operator ==(in SizeD left, in SizeD right) => left.Equals(right);

    public static bool operator !=(in SizeD left, in SizeD right) => !(left == right);
    #endregion
}

//PointDをクラスから構造体に変更。20190905
[StructLayout(LayoutKind.Sequential)]
[Serializable()]
[TypeConverter(typeof(PointDConverter))]
public struct PointD : IComparable, IEquatable<PointD>
{
    public double X { get; set; }
    public double Y { get; set; }
    public object Tag { get; set; }
    public readonly bool IsNaN => double.IsNaN(X) || double.IsNaN(Y);
    public readonly double Length2 => X * X + Y * Y;
    public readonly double Length => Math.Sqrt(X * X + Y * Y);

    public PointD(in double x, in double y)
    {
        X = x;
        Y = y;
        Tag = null;
    }
    public PointD(in (double x, double y) e)
    {
        X = e.x;
        Y = e.y;
        Tag = null;
    }

    public PointD(in PointD pt)
    {
        X = pt.X;
        Y = pt.Y;
        Tag = null;
    }

    public readonly PointF ToPointF() => new((float)X, (float)Y);

    public int CompareTo(object obj)
    {
        int value = X.CompareTo(((PointD)obj).X);
        if (value != 0)
            return value;
        else return Y.CompareTo(((PointD)obj).Y);
    }

    /// <summary>
    /// Get the string representation
    /// </summary>
    /// <returns></returns>
    public override readonly string ToString() => string.Format("({0}, {1})", this.X, this.Y);

    public override bool Equals(object obj) => obj is PointD d && Equals(d);

    public bool Equals(PointD other) => X == other.X && Y == other.Y;

    public override int GetHashCode() => HashCode.Combine(X, Y);


    #region 演算子のオーバーロード

    public static PointD operator +(in PointD p1, in PointD p2) => new(p1.X + p2.X, p1.Y + p2.Y);

    public static PointD operator -(in PointD p1, in PointD p2) => new(p1.X - p2.X, p1.Y - p2.Y);

    public static PointD operator -(in PointD p) => new PointD(-p.X, -p.Y);

    public static PointD operator *(in double d, in PointD p) => new(d * p.X, d * p.Y);

    public static PointD operator *(in PointD p, in double d) => new(d * p.X, d * p.Y);

    public static double operator *(in PointD p1, in PointD p2) => p1.X * p2.X + p1.Y * p2.Y;

    public static PointD operator /(in PointD p, in double d) => new(p.X / d, p.Y / d);

    public static bool operator ==(in PointD left, in PointD right) => left.Equals(right);

    public static bool operator !=(in PointD left, in PointD right) => !(left == right);

    public static PointD operator +(in PointD p1, in SizeD p2) => new(p1.X + p2.Width, p1.Y + p2.Height);
    public static PointD operator -(in PointD p1, in SizeD p2) => new(p1.X - p2.Width, p1.Y - p2.Height);

    #endregion 演算子のオーバーロード
}