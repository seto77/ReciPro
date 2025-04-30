using System;
using System.Numerics;

namespace Crystallography;

/// <summary>
/// Plane の概要の説明です。
/// </summary>
[Serializable()]
public class Plane : IComparable
{
    public PeakFunctionForm SerchOption;

    public double SerchRange = 0.10;
    public double FWHM = 0.10;
    public bool IsCombined;//ほかの面と結合(＝等価)であるときTrue

    //public int multi;//多重度
    public int[] Multi = new int[1];//多重度

    public bool IsRootIndex = false;//基底の面指数を持つとき

    public double MillimeterCalc, MillimeterObs;
    public double Intensity;//Conbineされているときは強度の和
    public double RawIntensity;

    public double[] eachIntensity = new double[1];//Combineされているときは要素数が2以上になる
    public double observedIntensity;
    public PointD simpleParameter;
    public PeakFunction peakFunction = new();
    public double[] F2 = new double[1];//Combineされているときは要素数が2以上になる
    public Complex[] F = new Complex[1];
    public bool IsFittingSelected;
    public bool IsFittingChecked;
    public int DecompositionGroup;
    public int h, k, l;
    public int eH, eK, eL;
    public double d;
    public double dObs;
    public double XObs;
    public double XCalc;
    public double Weight;
    public string strHKL, strD;
    public string[] strCondition = [];
    public string str;
    public int num;

    public double CosExpFactor;
    public double SinExpFactor;

    public int CompareTo(object obj)
    {
        var _p = obj as Plane;
        if (d != _p.d)
            return -d.CompareTo(_p.d);
        else
        {
            var _h = _p.h;
            var _k = _p.k;
            var _l = _p.l;

            if (h > _h || h == _h && k > _k || h == _h && k == _k && l > _l)
                return -1;
            else if (h == _h && k == _k && l == _l)
                return 0;
            else
                return 1;
        }
    }

    public override string ToString()
    {
        //return str.ToString();
        string s = (strHKL + "                            ")[..13];//空白を挿入してから10文字目以降をカット
        if (double.IsNaN(XCalc) || XCalc == 0)
            s += " " + "##.####";
        else
            s += $" {XCalc:00.0000}";

        if (double.IsNaN(XObs) || XObs == 0)
            s += " " + "##.####";
        else
            s += $" {XObs:00.0000}";

        if (double.IsNaN(XObs) || XObs == 0 || double.IsNaN(peakFunction.Hk) || peakFunction.Hk == 0 || double.IsInfinity(peakFunction.Hk))
            s += " " + "#.####";
        else
            s += $" {peakFunction.Hk:0.0000}";

        if (double.IsNaN(XObs) || XObs == 0 || double.IsNaN(Weight) || Weight == 0 || double.IsInfinity(Weight))
            s += " " + "###.#";
        else
            s += $" {Weight:000.0}";

        if (double.IsNaN(XObs) || XObs == 0 || double.IsNaN(observedIntensity) || observedIntensity == 0)
            s += " " + "#######";
        else
            s += $" {observedIntensity:000000}";

        return s;
    }
}