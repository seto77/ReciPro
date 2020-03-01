using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Crystallography
{
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

        public bool IsInsde(PointD p)
        {
            return p.X >= X && p.X <= UpperX && p.Y >= Y && p.Y <= UpperY;
        }

        public RectangleD(double x, double y, double width, double height) : this()
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public RectangleD(int x, int y, int width, int height)
            : this()
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public RectangleD(PointD pt, SizeD size)
            : this()
        {
            X = pt.X;
            Y = pt.Y;
            Width = size.Width;
            Height = size.Height;
        }

        public RectangleD(Point pt, Size size)
            : this()
        {
            X = pt.X;
            Y = pt.Y;
            Width = size.Width;
            Height = size.Height;
        }

        public RectangleD(PointD pt1, PointD pt2)
            : this()
        {
            X = Math.Min(pt1.X, pt2.X);
            Y = Math.Min(pt1.Y, pt2.Y);
            Width = Math.Abs(pt2.X - pt1.X);
            Height = Math.Abs(pt2.Y - pt1.Y);
        }

        public RectangleF ToRectangleF()
        {
            return new RectangleF((float)X, (float)Y, (float)Width, (float)Height);
        }

        public SizeD ToSizeD()
        {
            return new SizeD(Width, Height);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    [TypeConverter(typeof(SizeDConverter))]
    public struct SizeD
    {
        public double Width { set; get; }
        public double Height { set; get; }

        public SizeD(double width, double height)
            : this()
        {
            Width = width;
            Height = height;
        }

        public SizeD(int width, int height)
            : this()
        {
            Width = width;
            Height = height;
        }

        public SizeD(SizeD size)
            : this()
        {
            Width = size.Width;
            Height = size.Height;
        }

        public SizeD(Size size)
            : this()
        {
            Width = size.Width;
            Height = size.Height;
        }

        public SizeF ToSizeF()
        {
            return new SizeF((float)Width, (float)Height);
        }
    }


    //PointDをクラスから構造体に変更。201909/05
    [StructLayout(LayoutKind.Sequential)]
    [Serializable()]
    [TypeConverter(typeof(PointDConverter))]
    public struct PointD : IComparable, IEquatable<PointD>
    {
        public double X { get; set; }
        public double Y { get; set; }
        public object Tag { get; set; }
        public bool IsNaN { get => double.IsNaN(X) || double.IsNaN(Y); }
        public double Length2 => X * X + Y * Y;
        public double Length => Math.Sqrt(X * X + Y * Y);

        /* public PointD()
         {
             X = Y = 0;
             Tag = null;
         }*/

        public PointD(double x, double y)
        //: this()
        {
            X = x;
            Y = y;
            Tag = null;
        }
        public PointD((double x, double y) e)
        //: this()
        {
            X = e.x;
            Y = e.y;
            Tag = null;
        }

        public PointD(PointD pt)
        //: this()
        {
            X = pt.X;
            Y = pt.Y;
            Tag = null;
        }

        public PointF ToPointF() => new PointF((float)X, (float)Y);

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
        public override string ToString() => String.Format("({0}, {1})", this.X, this.Y);

        public override bool Equals(object obj) => obj is PointD d && Equals(d);

        public bool Equals(PointD other) => X == other.X && Y == other.Y;

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }


        #region 演算子のオーバーロード

        public static PointD operator +(PointD p1, PointD p2) => new PointD(p1.X + p2.X, p1.Y + p2.Y);

        public static PointD operator -(PointD p1, PointD p2) => new PointD(p1.X - p2.X, p1.Y - p2.Y);

        public static PointD operator -(PointD p) => new PointD(-p.X, -p.Y);

        public static PointD operator *(double d, PointD p) => new PointD(d * p.X, d * p.Y);

        public static PointD operator *(PointD p, double d) => new PointD(d * p.X, d * p.Y);

        public static double operator *(PointD p1, PointD p2) => p1.X * p2.X + p1.Y * p2.Y;

        public static PointD operator /(PointD p, double d) => new PointD(p.X / d, p.Y / d);

        public static bool operator ==(PointD left, PointD right) => left.Equals(right);

        public static bool operator !=(PointD left, PointD right) => !(left == right);

        #endregion 演算子のオーバーロード
    }
}