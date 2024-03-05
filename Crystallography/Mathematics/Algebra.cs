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
        //絶対値を小さい順に並び変え
        var vals = new[] { Math.Abs(h), Math.Abs(k), Math.Abs(l) };
        Array.Sort(vals);

        if (vals[0] == 1 || vals[1] == 1 || vals[2] < 2)
            return 1;

        for (int n = vals[0] > 1 ? vals[0] : (vals[1] > 1 ? vals[1] : vals[2]); n >= 2; n--)
            if (h % n == 0 && k % n == 0 && l % n == 0)
                return n;

        return 1;
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
