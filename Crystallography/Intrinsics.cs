using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using System.Numerics;
using static System.Numerics.Complex;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using MathNet.Numerics;

namespace Crystallography;
public static class Intrinsics
{
    private static unsafe Vector256<double> FromArray(double[] array, int index)
    {
        fixed (double* p = array)
            return Avx2.LoadVector256(p + index);
    }

    private static unsafe Vector256<double> FromArray(Complex[] array, int index)
    {
        fixed (Complex* p = array)
            return Avx2.LoadVector256((double*)p + index * 2);
    }

    public static double Sum(double[] array)
    {
        var v = Vector256<double>.Zero;
        for (int i = 0; i < array.Length; i += 4)
            v = Avx2.Add(v, FromArray(array, i));

        return v.GetElement(0) + v.GetElement(1) + v.GetElement(2) + v.GetElement(3);
    }

    public static Complex Sum(Complex[] array)
    {
        var v = Vector256<double>.Zero;
        for (int i = 0; i < array.Length; i += 2)
            v = Avx2.Add(v, FromArray(array, i));

        return new Complex(v.GetElement(0) + v.GetElement(2), v.GetElement(1) + v.GetElement(3));
    }

    public static unsafe void Blend(Complex[] c0, Complex[] c1, Complex[] c2, Complex[] c3, in double r0, in double r1, in double r2, in double r3, ref Complex[] result)
    {
        var rV0 = FromArray(new double[] { r0, r0, r0, r0 }, 0);
        var rV1 = FromArray(new double[] { r1, r1, r1, r1 }, 0);
        var rV2 = FromArray(new double[] { r2, r2, r2, r2 }, 0);
        var rV3 = FromArray(new double[] { r3, r3, r3, r3 }, 0);

        fixed (Complex* p1 = result)
        {
            var p2 = (double*)p1;
            for (int n = 0; n < result.Length - 1; n += 2, p2 += 4)
            {
                var accum = Avx2.Multiply(FromArray(c0, n), rV0);
                accum = Avx2.Add(accum, Avx2.Multiply(FromArray(c1, n), rV1));
                accum = Avx2.Add(accum, Avx2.Multiply(FromArray(c2, n), rV2));
                accum = Avx2.Add(accum, Avx2.Multiply(FromArray(c3, n), rV3));
                Avx2.Store(p2, accum);
            }
        }
        if (result.Length % 2 == 1)
            result[^1] = c0[^1] * r0 + c1[^1] * r1 + c2[^1] * r2 + c3[^1] * r3;
    }

    public static unsafe void BlendAndConjugate(Complex[] c0, Complex[] c1, Complex[] c2, Complex[] c3, in double r0, in double r1, in double r2, in double r3, ref Complex[] result)
    {
        var rV0 = FromArray(new double[] { r0, r0, r0, r0 }, 0);
        var rV1 = FromArray(new double[] { r1, r1, r1, r1 }, 0);
        var rV2 = FromArray(new double[] { r2, r2, r2, r2 }, 0);
        var rV3 = FromArray(new double[] { r3, r3, r3, r3 }, 0);

        fixed (Complex* p1 = result)
        {
            var p2 = (double*)p1;
            for (int n = 0; n < result.Length - 1; n += 2)
            {
                var accum = Avx2.Multiply(FromArray(c0, n), rV0);
                accum = Avx2.Add(accum, Avx2.Multiply(FromArray(c1, n), rV1));
                accum = Avx2.Add(accum, Avx2.Multiply(FromArray(c2, n), rV2));
                accum = Avx2.Add(accum, Avx2.Multiply(FromArray(c3, n), rV3));

                p2[n * 2] = accum.GetElement(0);
                p2[n * 2 + 1] = -accum.GetElement(1);
                p2[n * 2 + 2] = accum.GetElement(2);
                p2[n * 2 + 3] = -accum.GetElement(3);
            }
        }
        if (result.Length % 2 == 1)
            result[^1] = (c0[^1] * r0 + c1[^1] * r1 + c2[^1] * r2 + c3[^1] * r3).Conjugate();
    }

    public static unsafe void Blend(Complex[] c0, Complex[] c1, in double r0, in double r1,  ref Complex[] result)
    {
        var rV0 = FromArray(new Complex[] { r0, r0, r0, r0 }, 0);
        var rV1 = FromArray(new Complex[] { r1, r1, r1, r1 }, 0);
        fixed (Complex* p1 = result)
        {
            var p2 = (double*)p1;
            for (int n = 0; n < result.Length - 1; n += 2)
            {
                var accum = Avx2.Multiply(FromArray(c0, n), rV0);
                accum = Avx2.Add(accum, Avx2.Multiply(FromArray(c1, n), rV1));
                Avx2.Store(p2, accum);
            }
        }
        if (result.Length % 2 == 1)
            result[^1] = c0[^1] * r0 + c1[^1] * r1;
    }


}
