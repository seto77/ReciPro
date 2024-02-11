using System;

namespace Crystallography.Mathematics;

static public class Algebra
{
    /// <summary>
    /// 3つの整数が与えられたとき、それが既約かどうかを判定し、既約であれば1、そうでないときは最大公約数(2以上の整数)を返す。
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    static public int Irreducible(int h, int k, int l)
    {
        var max = Math.Max(Math.Abs(h), Math.Max(Math.Abs(k), Math.Abs(l)));
        if (max < 2)
            return 1;
        var min = Math.Min(Math.Abs(h), Math.Min(Math.Abs(k), Math.Abs(l)));

        bool flag = true;
        int n = min >= 2 ? min : max;
        for (; n >= 2; n--)
            if (h % n == 0 && k % n == 0 && l % n == 0)
            {
                flag = false;
                break;
            }

        return flag ?  1 : n;
    }

    /// <summary>
    /// 3つの整数が与えられたとき、それが既約かどうかを判定し、既約であれば1、そうでないときは最大公約数(2以上の整数)を返す。
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    static public int Irreducible((int h, int k, int l) index) => Irreducible(index.h, index.k, index.l);

}
