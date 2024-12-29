using MathNet.Numerics;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Crystallography;
public static class Intrinsics
{

    private static unsafe Vector128<double> FromArray128(double[] array, int index)
    {
        fixed (double* p = array)
            return Avx.LoadVector128(p + index);
    }

    private static unsafe Vector256<double> FromArray256(double[] array, int index)
    {
        fixed (double* p = array)
            return Avx.LoadVector256(p + index);
    }

    private static unsafe Vector256<double> FromArray256(Complex[] array, int index)
    {
        fixed (Complex* p = array)
            return Avx.LoadVector256((double*)p + index * 2);
    }

    public static double Sum256(double[] array)
    {
        var v = Vector256<double>.Zero;
        for (int i = 0; i < array.Length; i += 4)
            v = Avx.Add(v, FromArray256(array, i));

        return v.GetElement(0) + v.GetElement(1) + v.GetElement(2) + v.GetElement(3);
    }

    public static Complex Sum256(Complex[] array)
    {
        var v = Vector256<double>.Zero;
        for (int i = 0; i < array.Length; i += 2)
            v = Avx.Add(v, FromArray256(array, i));

        return new Complex(v.GetElement(0) + v.GetElement(2), v.GetElement(1) + v.GetElement(3));
    }

    public static unsafe Complex Blend(in Complex c0, in Complex c1, in Complex c2, in Complex c3, in double r0, in double r1, in double r2, in double r3)
    {
        var result = new Complex();
        Complex* p = &result;

        var rV0 = FromArray128([r0, r0], 0);
        var rV1 = FromArray128([r1, r1], 0);
        var rV2 = FromArray128([r2, r2], 0);
        var rV3 = FromArray128([r3, r3], 0);

        fixed (Complex* _c0 = &c0)
        fixed (Complex* _c1 = &c1)
        fixed (Complex* _c2 = &c2)
        fixed (Complex* _c3 = &c3)
        {
            var accum = Avx.Multiply(Avx.LoadVector128((double*)_c0), rV0);
            accum = Avx.Add(accum, Avx.Multiply(Avx.LoadVector128((double*)_c1), rV1));
            accum = Avx.Add(accum, Avx.Multiply(Avx.LoadVector128((double*)_c2), rV2));
            accum = Avx.Add(accum, Avx.Multiply(Avx.LoadVector128((double*)_c3), rV3));
            Avx.Store((double*)p, accum);
        }
        return result;
    }

    public static unsafe void Blend(int len, Complex[] c0, Complex[] c1, Complex[] c2, Complex[] c3, in double r0, in double r1, in double r2, in double r3, ref Complex[] result)
    {
        var rV0 = FromArray256(new double[] { r0, r0, r0, r0 }, 0);
        var rV1 = FromArray256(new double[] { r1, r1, r1, r1 }, 0);
        var rV2 = FromArray256(new double[] { r2, r2, r2, r2 }, 0);
        var rV3 = FromArray256(new double[] { r3, r3, r3, r3 }, 0);

        fixed (Complex* p1 = result)
        {
            var p2 = (double*)p1;
            for (int n = 0; n < len - 1; n += 2, p2 += 4)
            {
                var accum = Avx.Multiply(FromArray256(c0, n), rV0);
                accum = Avx.Add(accum, Avx.Multiply(FromArray256(c1, n), rV1));
                accum = Avx.Add(accum, Avx.Multiply(FromArray256(c2, n), rV2));
                accum = Avx.Add(accum, Avx.Multiply(FromArray256(c3, n), rV3));
                Avx.Store(p2, accum);
            }
        }
        if (len % 2 == 1)
            result[len - 1] = c0[len - 1] * r0 + c1[len - 1] * r1 + c2[len - 1] * r2 + c3[len - 1] * r3;
    }

    public static unsafe void BlendAndConjugate(int len, Complex[] c0, Complex[] c1, Complex[] c2, Complex[] c3, in double r0, in double r1, in double r2, in double r3, ref Complex[] result)
    {
        var rV0 = FromArray256(new double[] { r0, r0, r0, r0 }, 0);
        var rV1 = FromArray256(new double[] { r1, r1, r1, r1 }, 0);
        var rV2 = FromArray256(new double[] { r2, r2, r2, r2 }, 0);
        var rV3 = FromArray256(new double[] { r3, r3, r3, r3 }, 0);

        fixed (Complex* p1 = result)
        {
            var p2 = (double*)p1;
            for (int n = 0; n < len - 1; n += 2)
            {
                var accum = Avx.Multiply(FromArray256(c0, n), rV0);
                accum = Avx.Add(accum, Avx.Multiply(FromArray256(c1, n), rV1));
                accum = Avx.Add(accum, Avx.Multiply(FromArray256(c2, n), rV2));
                accum = Avx.Add(accum, Avx.Multiply(FromArray256(c3, n), rV3));

                p2[n * 2] = accum.GetElement(0);
                p2[n * 2 + 1] = -accum.GetElement(1);
                p2[n * 2 + 2] = accum.GetElement(2);
                p2[n * 2 + 3] = -accum.GetElement(3);
            }
        }
        if (len % 2 == 1)
            result[len - 1] = (c0[len - 1] * r0 + c1[len - 1] * r1 + c2[len - 1] * r2 + c3[len - 1] * r3).Conjugate();
    }

    public static unsafe void Blend(int len, Complex[] c0, Complex[] c1, in double r0, in double r1, ref Complex[] result)
    {
        var rV0 = FromArray256(new Complex[] { r0, r0, r0, r0 }, 0);
        var rV1 = FromArray256(new Complex[] { r1, r1, r1, r1 }, 0);
        fixed (Complex* p1 = result)
        {
            var p2 = (double*)p1;
            for (int n = 0; n < len - 1; n += 2)
            {
                var accum = Avx.Multiply(FromArray256(c0, n), rV0);
                accum = Avx.Add(accum, Avx.Multiply(FromArray256(c1, n), rV1));
                Avx.Store(p2, accum);
            }
        }
        if (result.Length % 2 == 1)
            result[len - 1] = c0[len - 1] * r0 + c1[len - 1] * r1;
    }


    static public void PointWiseMultiply(int len, Complex[] left, Complex[] right, Complex[] result)
    {
        // x = (x1, x2, x3, x4)
        // y = (y1, y2, y3, y4)
        // としたとき、
        // Shuffle(x,x,5) =>(x2,x1,x4,x3)  (良くわからんが、5 = 00000101 というビット操作が関係している。) 
        // HorizontalSubtract(x, y) => (x1-x2, y1-y2, x3-x4, y3-y4) 
        // HorizontalAdd(x, y) => (x1+x2, y1+y2, x3+x4, y3+y4) 
        // UnpackLow(x, y) => (x1, y1, x3, y3) 
        // UnpackLow(x, y) => (x2, y2, x4, y4)

        unsafe
        {
            fixed (Complex* ptr1 = left)
            fixed (Complex* ptr2 = right)
            fixed (Complex* ptr3 = result)
            {
                var p1 = (double*)ptr1;
                var p2 = (double*)ptr2;
                var p3 = (double*)ptr3;
                int i = 0;

                for (; i + 8 <= len * 2; i += 8)
                {
                    var a = Avx.LoadVector256(p1 + i);
                    var b = Avx.LoadVector256(p1 + i + 4);
                    var c = Avx.LoadVector256(p2 + i);
                    var d = Avx.LoadVector256(p2 + i + 4);

                    var foo = Avx.HorizontalSubtract(Avx.Multiply(a, c), Avx.Multiply(b, d));
                    var bar = Avx.HorizontalAdd(Avx.Multiply(a, Avx.Shuffle(c, c, 5)), Avx.Multiply(b, Avx.Shuffle(d, d, 5)));

                    Avx.Store(p3 + i, Avx.UnpackLow(foo, bar));
                    Avx.Store(p3 + i + 4, Avx.UnpackHigh(foo, bar));
                }

                for (; i < len * 2; i += 2)
                {
                    var a = p1[i + 0];
                    var b = p1[i + 1];
                    var c = p2[i + 0];
                    var d = p2[i + 1];
                    p3[i + 0] = a * c - b * d;
                    p3[i + 1] = a * d + b * c;
                }
            }
        }
    }

    static public Complex Multiply(int len, Complex[] left, Complex[] right)
    {
        unsafe
        {
            fixed (Complex* ptr1 = left)
            fixed (Complex* ptr2 = right)
            {
                var p1 = (double*)ptr1;
                var p2 = (double*)ptr2;
                int i = 0;

                var s = Vector256<double>.Zero;
                for (; i + 8 <= len * 2; i += 8)
                {
                    var a = Avx.LoadVector256(p1 + i);
                    var b = Avx.LoadVector256(p1 + i + 4);
                    var c = Avx.LoadVector256(p2 + i);
                    var d = Avx.LoadVector256(p2 + i + 4);

                    var foo = Avx.HorizontalSubtract(Avx.Multiply(a, c), Avx.Multiply(b, d));
                    var bar = Avx.HorizontalAdd(Avx.Multiply(a, Avx.Shuffle(c, c, 5)), Avx.Multiply(b, Avx.Shuffle(d, d, 5)));

                    s = Avx.Add(s, Avx.UnpackLow(foo, bar));
                    s = Avx.Add(s, Avx.UnpackHigh(foo, bar));
                }
                var real = s.GetElement(0) + s.GetElement(2);
                var imag = s.GetElement(1) + s.GetElement(3);

                for (; i < len * 2; i += 2)
                {
                    var a = p1[i + 0];
                    var b = p1[i + 1];
                    var c = p2[i + 0];
                    var d = p2[i + 1];
                    real += a * c - b * d;
                    imag += a * d + b * c;
                }
                return new Complex(real, imag);

            }
        }
    }


}
