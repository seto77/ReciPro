using System;
using System.Collections.Generic;
using System.Linq;

namespace Crystallography;

/// <summary>
/// Symmetry の概要の説明です。
/// </summary>
[Serializable()]
public readonly struct Symmetry
{
    #region プロパティ
    //public enum CrystalSytem { Unknown, Triclinic, Monoclinic, Orthorhombic, Tetragonal, Trigonal, Hexagonal, Cubic }
    //public enum LatticeType { P, A, B, C, I, F, R }

    //sub,SF,Hall,HM,HM_full,1軸p,1軸v,2軸p,2軸v,3軸p,3軸v,点群、ラウエ群、結晶系
    public string SpaceGroupHMsubStr { get; }
    public string SpaceGroupSFStr { get; }
    public string SpaceGroupHallStr { get; }
    public string SpaceGroupHMStr { get; }
    public string SpaceGroupHMfullStr { get; }
    public string MainAxis { get; }
    public string LatticeTypeStr { get; }
    public string StrSE1p { get; }
    public string StrSE1v { get; }
    public string StrSE2p { get; }
    public string StrSE2v { get; }
    public string StrSE3p { get; }
    public string StrSE3v { get; }
    public string PointGroupHMStr { get; }
    public string PointGroupSFStr { get; }
    public string LaueGroupStr { get; }
    public string CrystalSystemStr { get; }

    //Unknown;530空間群の番号(通し番号		空間群番号	空間群のSub番号		点群番号	ラウエ群番号	結晶系番号)
    public ushort SeriesNumber { get; }
    public byte SpaceGroupNumber { get; }
    public byte SpaceGroupSubNumber { get; }
    public byte PointGroupNumber { get; }
    public byte LaueGroupNumber { get; }
    public byte CrystalSystemNumber { get; }

    //[XmlIgnoreAttribute]
    public string[] ExtinctionRuleStr { get; }

    public List<Func<int, int, int, string>> CheckExtinctionFunc { get; }
    #endregion

    #region コンストラクタ
    public Symmetry(in int seriesNumber)
    {
        if (seriesNumber >= 0 && seriesNumber < SymmetryStatic.TotalSpaceGroupNumber)
        {
            var str = SymmetryStatic.StrArray[seriesNumber];
            var num = SymmetryStatic.NumArray[seriesNumber];

            SpaceGroupHMsubStr = str[0];
            SpaceGroupSFStr = str[1];
            SpaceGroupHallStr = str[2];
            SpaceGroupHMStr = str[3];
            SpaceGroupHMfullStr = str[4];
            MainAxis = str[5];
            LatticeTypeStr = str[6];
            StrSE1p = str[7];
            StrSE1v = str[8];
            StrSE2p = str[9];
            StrSE2v = str[10];
            StrSE3p = str[11];
            StrSE3v = str[12];
            PointGroupHMStr = str[13];
            PointGroupSFStr = str[14];
            LaueGroupStr = str[15];
            CrystalSystemStr = str[16];

            SeriesNumber = num[0];
            SpaceGroupNumber = (byte)num[1];
            SpaceGroupSubNumber = (byte)num[2];
            PointGroupNumber = (byte)num[3];
            LaueGroupNumber = (byte)num[4];
            CrystalSystemNumber = (byte)num[5];

            ExtinctionRuleStr = null;
            CheckExtinctionFunc = null;

            ExtinctionRuleStr = ExtinctionRule(this);
            CheckExtinctionFunc = SetExtinctionFunc(this);
        }
        else
        {
            SpaceGroupHMsubStr = SpaceGroupSFStr = SpaceGroupHallStr = SpaceGroupHMStr = SpaceGroupHMfullStr = MainAxis =
            LatticeTypeStr = StrSE1p = StrSE1v = StrSE2p = StrSE2v = StrSE3p = StrSE3v = PointGroupHMStr =
            PointGroupSFStr = LaueGroupStr = CrystalSystemStr = "";

            SeriesNumber = 0;
            SpaceGroupNumber = SpaceGroupSubNumber = PointGroupNumber = LaueGroupNumber = CrystalSystemNumber = 0;

            ExtinctionRuleStr = null;
            CheckExtinctionFunc = null;
        }
    }
    #endregion

    #region メソッド
    public readonly bool IsPlaneRootIndex(int h, int k, int l) => SymmetryStatic.IsRootIndex((h, k, l), this);

    public readonly bool IsPlaneRootIndex((int h, int k, int l) index) => SymmetryStatic.IsRootIndex(index, this);

    public readonly string[] CheckExtinctionRule((int h, int k, int l) index)
        => CheckExtinctionFunc.Select(check => check(index.h, index.k, index.l)).Where(str => str != null).ToArray();

    public readonly string[] CheckExtinctionRule(int h, int k, int l)
        => CheckExtinctionFunc.Select(check => check(h, k, l)).Where(str => str != null).ToArray();
    #endregion

    #region 静的メソッド
    /// <summary>
    /// 禁制則に抵触する指数を判定するFuncのリストを返す
    /// </summary>
    /// <param name="sym"></param>
    /// <returns></returns>
    public static List<Func<int, int, int, string>> SetExtinctionFunc(Symmetry sym)
    {
        #region
        var func = new List<Func<int, int, int, string>>();

        switch (sym.LatticeTypeStr)
        {
            case "A":
                func.Add((h, k, l) => (k + l) % 2 != 0 ? "A" : null); break;
            case "B":
                func.Add((h, k, l) => (l + h) % 2 != 0 ? "B" : null); break;
            case "C":
                func.Add((h, k, l) => (h + k) % 2 != 0 ? "C" : null); break;
            case "I":
                func.Add((h, k, l) => (h + k + l) % 2 != 0 ? "I" : null); break;
            case "F":
                func.Add((h, k, l) => (h + k) % 2 != 0 || (k + l) % 2 != 0 || (l + h) % 2 != 0 ? "F" : null); break;
            case "R" when sym.SpaceGroupHMStr.Contains("Hex"):
                func.Add((h, k, l) => (-h + k + l) % 3 != 0 ? "R" : null); break;
        }

        switch (sym.CrystalSystemNumber)
        {
            case 0://	Unknown
                break;

            case 1://	triclinic
                break;

            case 2://	monoclinic
                if (sym.StrSE1p == "2s1")
                    func.Add((h, k, l) => h % 2 != 0 && k == 0 && l == 0 ? "2sub1//[100]" : null);

                if (sym.StrSE2p == "2s1")
                    func.Add((h, k, l) => k % 2 != 0 && l == 0 && h == 0 ? "2sub1//[010]" : null);

                if (sym.StrSE3p == "2s1")
                    func.Add((h, k, l) => l % 2 != 0 && h == 0 && k == 0 ? "2sub1//[001]" : null);

                if (sym.StrSE1v == "b")
                    func.Add((h, k, l) => k % 2 != 0 && h == 0 ? "b⊥[100]" : null);
                else if (sym.StrSE1v == "c")
                    func.Add((h, k, l) => l % 2 != 0 && h == 0 ? "c⊥[100]" : null);
                else if (sym.StrSE1v == "n")
                    func.Add((h, k, l) => (k + l) % 2 != 0 && h == 0 ? "n⊥[100]" : null);

                if (sym.StrSE2v == "a")
                    func.Add((h, k, l) => h % 2 != 0 && k == 0 ? "a⊥[010]" : null);
                else if (sym.StrSE2v == "c")
                    func.Add((h, k, l) => l % 2 != 0 && k == 0 ? "c⊥[010]" : null);
                else if (sym.StrSE2v == "n")
                    func.Add((h, k, l) => (h + l) % 2 != 0 && k == 0 ? "n⊥[010]" : null);

                if (sym.StrSE3v == "a")
                    func.Add((h, k, l) => h % 2 != 0 && l == 0 ? "a⊥[001]" : null);
                else if (sym.StrSE3v == "b")
                    func.Add((h, k, l) => k % 2 != 0 && l == 0 ? "b⊥[001]" : null);
                else if (sym.StrSE3v == "n")
                    func.Add((h, k, l) => (h + k) % 2 != 0 && l == 0 ? "n⊥[001]" : null);

                break;

            case 3://	orthorhombic
                if (sym.StrSE1p == "2s1")
                    func.Add((h, k, l) => h % 2 != 0 && k == 0 && l == 0 ? "2sub1//[100]" : null);

                if (sym.StrSE2p == "2s1")
                    func.Add((h, k, l) => k % 2 != 0 && l == 0 && h == 0 ? "2sub1//[010]" : null);

                if (sym.StrSE3p == "2s1")
                    func.Add((h, k, l) => l % 2 != 0 && h == 0 && k == 0 ? "2sub1//[001]" : null);

                if (sym.StrSE1v == "b")
                    func.Add((h, k, l) => k % 2 != 0 && h == 0 ? "b⊥[100]" : null);
                else if (sym.StrSE1v == "c")
                    func.Add((h, k, l) => l % 2 != 0 && h == 0 ? "c⊥[100]" : null);
                else if (sym.StrSE1v == "n")
                    func.Add((h, k, l) => (k + l) % 2 != 0 && h == 0 ? "n⊥[100]" : null);
                else if (sym.StrSE1v == "d")
                    func.Add((h, k, l) => (k + l) % 4 != 0 && h == 0 ? "d⊥[100]" : null);

                if (sym.StrSE2v == "a")
                    func.Add((h, k, l) => h % 2 != 0 && k == 0 ? "a⊥[010]" : null);
                else if (sym.StrSE2v == "c")
                    func.Add((h, k, l) => l % 2 != 0 && k == 0 ? "c⊥[010]" : null);
                else if (sym.StrSE2v == "n")
                    func.Add((h, k, l) => (h + l) % 2 != 0 && k == 0 ? "n⊥[010]" : null);
                else if (sym.StrSE2v == "d")
                    func.Add((h, k, l) => (h + l) % 4 != 0 && k == 0 ? "d⊥[010]" : null);

                if (sym.StrSE3v == "a")
                    func.Add((h, k, l) => h % 2 != 0 && l == 0 ? "a⊥[001]" : null);
                else if (sym.StrSE3v == "b")
                    func.Add((h, k, l) => k % 2 != 0 && l == 0 ? "b⊥[001]" : null);
                else if (sym.StrSE3v == "n")
                    func.Add((h, k, l) => (h + k) % 2 != 0 && l == 0 ? "n⊥[001]" : null);
                else if (sym.StrSE3v == "d")
                    func.Add((h, k, l) => (h + k) % 4 != 0 && l == 0 ? "d⊥[001]" : null);

                break;

            case 4://	tetragonal
                if (sym.StrSE1p == "4s1")
                    func.Add((h, k, l) => h == 0 && k == 0 && l % 4 != 0 ? "4sub1//[001]" : null);
                else if (sym.StrSE1p == "4s2")
                    func.Add((h, k, l) => h == 0 && k == 0 && l % 2 != 0 ? "4sub2//[001]" : null);
                else if (sym.StrSE1p == "4s3")
                    func.Add((h, k, l) => h == 0 && k == 0 && l % 4 != 0 ? "4sub3//[001]" : null);

                if (sym.StrSE2p == "2s1")
                {
                    func.Add((h, k, l) => h % 2 != 0 && k == 0 && l == 0 ? "2sub1//[100]" : null);
                    func.Add((h, k, l) => h == 0 && k % 2 != 0 && l == 0 ? "2sub1//[010]" : null);
                }

                if (sym.StrSE1v == "a")
                {
                    func.Add((h, k, l) => h % 2 != 0 && l == 0 ? "a⊥[001]" : null);
                    func.Add((h, k, l) => k % 2 != 0 && l == 0 ? "b⊥[001]" : null);
                }
                else if (sym.StrSE1v == "n")
                {
                    func.Add((h, k, l) => (h + k) % 2 != 0 && l == 0 ? "n⊥[001]" : null);
                }

                if (sym.StrSE2v == "b")
                {
                    func.Add((h, k, l) => h % 2 != 0 && k == 0 ? "a⊥[010]" : null);
                }
                else if (sym.StrSE2v == "b")
                {
                    func.Add((h, k, l) => k % 2 != 0 && h == 0 ? "b⊥[100]" : null);
                }
                else if (sym.StrSE2v == "c")
                {
                    func.Add((h, k, l) => l % 2 != 0 && k == 0 ? "c⊥[010]" : null);
                    func.Add((h, k, l) => l % 2 != 0 && h == 0 ? "c⊥[100]" : null);
                }
                else if (sym.StrSE2v == "n")
                {
                    func.Add((h, k, l) => (h + l) % 2 != 0 && k == 0 ? "n⊥[010]" : null);
                    func.Add((h, k, l) => (k + l) % 2 != 0 && h == 0 ? "n⊥[100]" : null);
                }

                if (sym.StrSE3v == "c")
                {
                    func.Add((h, k, l) => l % 2 != 0 && h == k ? "c⊥[1-10]" : null);
                    func.Add((h, k, l) => l % 2 != 0 && h == -k ? "c⊥[110]" : null);
                }
                else if (sym.StrSE3v == "d")
                {
                    func.Add((h, k, l) => (2 * h + l) % 4 != 0 && h == k ? "d⊥[1-10]" : null);
                    func.Add((h, k, l) => (2 * h + l) % 4 != 0 && h == -k ? "d⊥[110]" : null);
                }
                break;

            case 5://	trigonal
                if (sym.StrSE1p == "3s1")
                    func.Add((h, k, l) => h == 0 && k == 0 && l % 3 != 0 ? "3sub1//[001]" : null);
                else if (sym.StrSE1p == "3s2")
                    func.Add((h, k, l) => h == 0 && k == 0 && l % 3 != 0 ? "3sub2//[001]" : null);

                if (sym.SpaceGroupHMsubStr != "R")//Hexセルの場合
                {
                    if (sym.StrSE2v == "c")
                    {
                        func.Add((h, k, l) => l % 2 != 0 && h == -k ? "c⊥[-1-10]" : null);
                        func.Add((h, k, l) => l % 2 != 0 && h == 0 ? "c⊥[100]" : null);
                        func.Add((h, k, l) => l % 2 != 0 && k == 0 ? "c⊥[010]" : null);
                    }

                    if (sym.StrSE3v == "c")
                    {
                        func.Add((h, k, l) => l % 2 != 0 && h == k ? "c⊥[1-10]" : null);
                        func.Add((h, k, l) => l % 2 != 0 && h == -2 * k ? "c⊥[120]" : null);
                        func.Add((h, k, l) => l % 2 != 0 && -2 * h == k ? "c⊥[-2-10]" : null);
                    }
                }
                else//Rhomboセルの場合
                {
                    if (sym.StrSE2v == "c")
                    {
                        func.Add((h, k, l) => l % 2 != 0 && h == k ? "c⊥[111]" : null);
                        func.Add((h, k, l) => l % 2 != 0 && h == k && k == l ? "c⊥[111]" : null);//要チェック。なんか変だ。
                    }
                }
                break;

            case 6://	hexagonal
                if (sym.StrSE1p == "6s1")
                    func.Add((h, k, l) => h == 0 && k == 0 && l % 6 != 0 ? "6sub1//[001]" : null);
                else if (sym.StrSE1p == "6s2")
                    func.Add((h, k, l) => h == 0 && k == 0 && l % 3 != 0 ? "6sub2//[001]" : null);
                else if (sym.StrSE1p == "6s3")
                    func.Add((h, k, l) => h == 0 && k == 0 && l % 2 != 0 ? "6sub3//[001]" : null);
                else if (sym.StrSE1p == "6s4")
                    func.Add((h, k, l) => h == 0 && k == 0 && l % 3 != 0 ? "6sub4//[001]" : null);
                else if (sym.StrSE1p == "6s5")
                    func.Add((h, k, l) => h == 0 && k == 0 && l % 6 != 0 ? "6sub5//[001]" : null);

                if (sym.StrSE2v == "c")
                    func.Add((h, k, l) => l % 2 != 0 && h == -k ? "c⊥[-1-10]" : null);
                else if (sym.StrSE2v == "c")
                    func.Add((h, k, l) => l % 2 != 0 && h == 0 ? "c⊥[100]" : null);
                else if (sym.StrSE2v == "c")
                    func.Add((h, k, l) => l % 2 != 0 && k == 0 ? "c⊥[010]" : null);

                if (sym.StrSE3v == "c")
                    func.Add((h, k, l) => l % 2 != 0 && h == k ? "c⊥[1-10]" : null);
                else if (sym.StrSE3v == "c")
                    func.Add((h, k, l) => l % 2 != 0 && h == -2 * k ? "c⊥[120]" : null);
                else if (sym.StrSE3v == "c")
                    func.Add((h, k, l) => l % 2 != 0 && -2 * h == k ? "c⊥[-2-10]" : null);

                break;

            case 7://	cubic
                if (sym.StrSE1p == "2s1")
                {
                    func.Add((h, k, l) => h % 2 != 0 && k == 0 && l == 0 ? "2sub1//[100]" : null);
                    func.Add((h, k, l) => h == 0 && k % 2 != 0 && l == 0 ? "2sub1//[010]" : null);
                    func.Add((h, k, l) => h == 0 && k == 0 && l % 2 != 0 ? "2sub1//[001]" : null);
                }
                else if (sym.StrSE1p == "4s1")
                {
                    func.Add((h, k, l) => h % 4 != 0 && k == 0 && l == 0 ? "4sub1//[100]" : null);
                    func.Add((h, k, l) => h == 0 && k % 4 != 0 && l == 0 ? "4sub1//[010]" : null);
                    func.Add((h, k, l) => h == 0 && k == 0 && l % 4 != 0 ? "4sub1//[001]" : null);
                }
                else if (sym.StrSE1p == "4s2")
                {
                    func.Add((h, k, l) => h % 2 != 0 && k == 0 && l == 0 ? "4sub2//[100]" : null);
                    func.Add((h, k, l) => h == 0 && k % 2 != 0 && l == 0 ? "4sub2//[010]" : null);
                    func.Add((h, k, l) => h == 0 && k == 0 && l % 2 != 0 ? "4sub2//[001]" : null);
                }
                else if (sym.StrSE1p == "4s3")
                {
                    func.Add((h, k, l) => h % 4 != 0 && k == 0 && l == 0 ? "4sub3//[100]" : null);
                    func.Add((h, k, l) => h == 0 && k % 4 != 0 && l == 0 ? "4sub3//[010]" : null);
                    func.Add((h, k, l) => h == 0 && k == 0 && l % 4 != 0 ? "4sub3//[001]" : null);
                }
                if (sym.StrSE1v == "a")
                {
                    func.Add((h, k, l) => h == 0 && k % 2 != 0 ? "b⊥[100]" : null);
                    func.Add((h, k, l) => k == 0 && l % 2 != 0 ? "c⊥[010]" : null);
                    func.Add((h, k, l) => l == 0 && h % 2 != 0 ? "a⊥[001]" : null);

                    if (sym.LaueGroupStr == "m3m")
                    {//Ia3のときはどうなるんだろう・・・
                        func.Add((h, k, l) => h == 0 && l % 2 != 0 ? "c⊥[100]" : null);
                        func.Add((h, k, l) => k == 0 && h % 2 != 0 ? "a⊥[010]" : null);
                        func.Add((h, k, l) => l == 0 && k % 2 != 0 ? "b⊥[001]" : null);
                    }
                }

                if (sym.StrSE1v == "n")
                {
                    func.Add((h, k, l) => h == 0 && (k + l) % 2 != 0 ? "n⊥[100]" : null);
                    func.Add((h, k, l) => k == 0 && (l + h) % 2 != 0 ? "n⊥[010]" : null);
                    func.Add((h, k, l) => l == 0 && (h + k) % 2 != 0 ? "n⊥[001]" : null);
                }
                else if (sym.StrSE1v == "d")
                {
                    func.Add((h, k, l) => h == 0 && (k + l) % 4 != 0 ? "d⊥[100]" : null);
                    func.Add((h, k, l) => k == 0 && (l + h) % 4 != 0 ? "d⊥[010]" : null);
                    func.Add((h, k, l) => l == 0 && (h + k) % 4 != 0 ? "d⊥[001]" : null);
                }

                if (sym.StrSE3v == "c")
                {
                    func.Add((h, k, l) => h == -k && l % 2 != 0 ? "c⊥[110]" : null);
                    func.Add((h, k, l) => h == k && l % 2 != 0 ? "c⊥[1-10]" : null);
                    func.Add((h, k, l) => k == -l && h % 2 != 0 ? "a⊥[011]" : null);
                    func.Add((h, k, l) => k == l && h % 2 != 0 ? "a⊥[01-1]" : null);
                    func.Add((h, k, l) => h == -l && k % 2 != 0 ? "b⊥[101]" : null);
                    func.Add((h, k, l) => h == l && k % 2 != 0 ? "b⊥[-101]" : null);
                }
                else if (sym.StrSE3v == "n")
                {
                    func.Add((h, k, l) => k == -l && (h - 2 * k) % 2 != 0 ? "n⊥[011]" : null);
                    func.Add((h, k, l) => k == l && (h + 2 * k) % 2 != 0 ? "n⊥[01-1]" : null);
                    func.Add((h, k, l) => h == -l && (2 * h - k) % 2 != 0 ? "n⊥[101]" : null);
                    func.Add((h, k, l) => h == l && (2 * h + k) % 2 != 0 ? "n⊥[-101]" : null);
                    func.Add((h, k, l) => h == -k && (2 * h - l) % 2 != 0 ? "n⊥[110]" : null);
                    func.Add((h, k, l) => h == k && (2 * h + l) % 2 != 0 ? "n⊥[1-10]" : null);
                }
                else if (sym.StrSE3v == "d")
                {
                    func.Add((h, k, l) => k == -l && (h - 2 * k) % 4 != 0 ? "d⊥[011]" : null);
                    func.Add((h, k, l) => k == l && (h + 2 * k) % 4 != 0 ? "d⊥[01-1]" : null);
                    func.Add((h, k, l) => h == -l && (2 * h - k) % 4 != 0 ? "d⊥[101]" : null);
                    func.Add((h, k, l) => h == l && (2 * h + k) % 4 != 0 ? "d⊥[-101]" : null);
                    func.Add((h, k, l) => h == -k && (2 * h - l) % 4 != 0 ? "d⊥[110]" : null);
                    func.Add((h, k, l) => h == k && (2 * h + l) % 4 != 0 ? "d⊥[1-10]" : null);
                }
                break;
        }
        return func;
        #endregion
    }

    public static string[] ExtinctionRule(Symmetry sym)
    {
        #region
        var str = new List<string>();

        if (sym.LatticeTypeStr != "P")
        {
            str.Add(sym.LatticeTypeStr switch
            {
                "A" => "hkl: k+l=2n: A",
                "B" => "hkl: h+l=2n: B",
                "C" => "hkl: h+k=2n: C",
                "I" => "hkl: h+k+l=2n: I",
                "F" => "hkl: h+k=2n k+l=2n: F",
                "R" => sym.SpaceGroupHMStr.Contains("Hex") ? "hkl: -h+k+l=3n: R" : "",
                _ => ""
            });
        }

        switch (sym.CrystalSystemNumber)
        {
            case 0://	Unknown
                break;

            case 1://	triclinic
                break;

            case 2://	monoclinic
                if (sym.StrSE1p == "2s1")
                    str.Add("h00: h=2n: 2sub1//[100]");

                if (sym.StrSE2p == "2s1")
                    str.Add("0k0: k=2n: 2sub1//[010]");

                if (sym.StrSE3p == "2s1")
                    str.Add("00l: l=2n: 2sub1//[001]");

                if (sym.StrSE1v == "b")
                    str.Add("0kl: k=2n: b⊥[100]");

                if (sym.StrSE1v == "c")
                    str.Add("0kl: l=2n: c⊥[100]");

                if (sym.StrSE1v == "n")
                    str.Add("0kl: k+l=2n: n⊥[100]");

                if (sym.StrSE2v == "a")
                    str.Add("h0l: h=2n: a⊥[010]");

                if (sym.StrSE2v == "c")
                    str.Add("h0l: l=2n: c⊥[010]");

                if (sym.StrSE2v == "n")
                    str.Add("h0l: l+h=2n: n⊥[010]");

                if (sym.StrSE3v == "a")
                    str.Add("hk0: h=2n: a⊥[001]");

                if (sym.StrSE3v == "b")
                    str.Add("hk0: k=2n: b⊥[001]");

                if (sym.StrSE3v == "n")
                    str.Add("hk0: h+k=2n: n⊥[001]");

                break;

            case 3://	orthorhombic
                if (sym.StrSE1p == "2s1")
                    str.Add("h00: h=2n: 2sub1//[100]");

                if (sym.StrSE2p == "2s1")
                    str.Add("0k0: k=2n: 2sub1//[010]");

                if (sym.StrSE3p == "2s1")
                    str.Add("00l: l=2n: 2sub1//[001]");

                if (sym.StrSE1v == "b")
                    str.Add("0kl: k=2n: b⊥[100]");

                if (sym.StrSE1v == "c")
                    str.Add("0kl: l=2n: c⊥[100]");

                if (sym.StrSE1v == "n")
                    str.Add("0kl: k+l=2n: n⊥[100]");

                if (sym.StrSE1v == "d")
                    str.Add("0kl: k+l=4n: d⊥[100]");

                if (sym.StrSE2v == "a")
                    str.Add("h0l: h=2n: a⊥[010]");

                if (sym.StrSE2v == "c")
                    str.Add("h0l: l=2n: c⊥[010]");

                if (sym.StrSE2v == "n")
                    str.Add("h0l: l+h=2n: n⊥[010]");

                if (sym.StrSE2v == "d")
                    str.Add("h0l: l+h=4n: d⊥[010]");

                if (sym.StrSE3v == "a")
                    str.Add("hk0: h=2n: a⊥[001]");

                if (sym.StrSE3v == "b")
                    str.Add("hk0: k=2n: b⊥[001]");

                if (sym.StrSE3v == "n")
                    str.Add("hk0: h+k=2n: n⊥[001]");

                if (sym.StrSE3v == "d")
                    str.Add("hk0: h+k=4n: d⊥[001]");

                break;

            case 4://	tetragonal
                if (sym.StrSE1p == "4s1")
                    str.Add("00l: l=4n: 4sub1//[001]");

                if (sym.StrSE1p == "4s2")
                    str.Add("00l: l=2n: 4sub2//[001]");

                if (sym.StrSE1p == "4s3")
                    str.Add("00l: l=4n: 4sub3//[001]");

                if (sym.StrSE2p == "2s1")
                    str.Add("h00: h=2n: 2sub1//[100]");

                if (sym.StrSE2p == "2s1")
                    str.Add("0k0: k=2n: 2sub1//[010]");

                if (sym.StrSE1v == "a")
                    str.Add("hk0: h=2n: a⊥[001]");

                if (sym.StrSE1v == "a")
                    str.Add("hk0: k=2n: b⊥[001]");

                if (sym.StrSE1v == "n")
                    str.Add("hk0: h+k=2n: n⊥[001]");

                if (sym.StrSE2v == "b")
                    str.Add("h0l: h=2n: a⊥[010]");

                if (sym.StrSE2v == "b")
                    str.Add("0kl: k=2n: b⊥[100]");

                if (sym.StrSE2v == "c")
                    str.Add("h0l: l=2n: c⊥[010]");

                if (sym.StrSE2v == "c")
                    str.Add("h0l: l=2n: c⊥[100]");

                if (sym.StrSE2v == "n")
                    str.Add("h0l: h+l=2n: n⊥[010]");

                if (sym.StrSE2v == "n")
                    str.Add("0kl: k+l=2n: n⊥[100]");

                if (sym.StrSE3v == "c")
                    str.Add("hhl: l=2n: c⊥[1-10]");

                if (sym.StrSE3v == "c")
                    str.Add("h-hl: l=2n: c⊥[110]");

                if (sym.StrSE3v == "d")
                    str.Add("hhl: 2h+l=4n: d⊥[1-10]");

                if (sym.StrSE3v == "d")
                    str.Add("h-hl: 2h+l=4n: d⊥[110]");

                break;

            case 5://	trigonal

                if (sym.StrSE1p == "3s1")
                    str.Add("00l: l=3n: 3sub1//[001]");

                if (sym.StrSE1p == "3s2")
                    str.Add("00l: l=3n: 3sub2//[001]");

                if (sym.StrSE2v == "c")
                    str.Add("h-hl: l=2n: c⊥[-1-10]");

                if (sym.StrSE2v == "c")
                    str.Add("0kl: l=2n: c⊥[100]");

                if (sym.StrSE2v == "c")
                    str.Add("h0l: l=2n: c⊥[010]");

                if (sym.StrSE3v == "c")
                    str.Add("hhl: l=2n: c⊥[1-10]");

                if (sym.StrSE3v == "c")
                    str.Add("-2hhl: l=2n: c⊥[120]");

                if (sym.StrSE3v == "c")
                    str.Add("h-2hl: l=2n: c⊥[-2-10]");

                break;

            case 6://	hexagonal
                if (sym.StrSE1p == "6s1")
                    str.Add("00l: l=6n: 6sub1//[001]");

                if (sym.StrSE1p == "6s2")
                    str.Add("00l: l=3n: 6sub2//[001]");

                if (sym.StrSE1p == "6s3")
                    str.Add("00l: l=2n: 6sub3//[001]");

                if (sym.StrSE1p == "6s4")
                    str.Add("00l: l=3n: 6sub4//[001]");

                if (sym.StrSE1p == "6s5")
                    str.Add("00l: l=6n: 6sub5//[001]");

                if (sym.StrSE2v == "c")
                    str.Add("h-hl: l=2n: c⊥[-1-10]");

                if (sym.StrSE2v == "c")
                    str.Add("0kl: l=2n: c⊥[100]");

                if (sym.StrSE2v == "c")
                    str.Add("h0l: l=2n: c⊥[010]");

                if (sym.StrSE3v == "c")
                    str.Add("hhl: l=2n: c⊥[1-10]");

                if (sym.StrSE3v == "c")
                    str.Add("-2hhl: l=2n: c⊥[120]");

                if (sym.StrSE3v == "c")
                    str.Add("h-2hl: l=2n: c⊥[-2-10]");


                break;

            case 7://	cubic
                if (sym.StrSE1p == "2s1")
                    str.Add("h00: h=2n: 2sub1//[100]");

                if (sym.StrSE1p == "2s1")
                    str.Add("0k0: k=2n: 2sub1//[010]");

                if (sym.StrSE1p == "2s1")
                    str.Add("00l: l=2n: 2sub1//[001]");

                if (sym.StrSE1p == "4s1")
                    str.Add("h00: h=4n: 4sub1//[100]");

                if (sym.StrSE1p == "4s1")
                    str.Add("0k0: k=4n: 4sub1//[010]");

                if (sym.StrSE1p == "4s1")
                    str.Add("00l: l=4n: 4sub1//[001]");

                if (sym.StrSE1p == "4s2")
                    str.Add("h00: h=2n: 4sub2//[100]");

                if (sym.StrSE1p == "4s2")
                    str.Add("0k0: k=2n: 4sub2//[010]");

                if (sym.StrSE1p == "4s2")
                    str.Add("00l: l=2n: 4sub2//[001]");

                if (sym.StrSE1p == "4s3")
                    str.Add("h00: h=4n: 4sub3//[100]");

                if (sym.StrSE1p == "4s3")
                    str.Add("0k0: k=4n: 4sub3//[010]");

                if (sym.StrSE1p == "4s3")
                    str.Add("00l: l=4n: 4sub3//[001]");

                if (sym.StrSE1v == "a")
                    str.Add("hk0: h=2n: a⊥[001]");

                if (sym.StrSE1v == "a")
                    str.Add("0kl: k=2n: b⊥[100]");

                if (sym.StrSE1v == "a")
                    str.Add("h0l: l=2n: c⊥[010]");

                if (sym.LaueGroupStr == "m3m")
                {
                    if (sym.StrSE1v == "a")
                        str.Add("hk0: k=2n: b⊥[001]");

                    if (sym.StrSE1v == "a")
                        str.Add("0kl: l=2n: c⊥[100]");

                    if (sym.StrSE1v == "a")
                        str.Add("h0l: h=2n: a⊥[010]");
                }

                if (sym.StrSE1v == "n")
                    str.Add("0kl: k+l=2n: n⊥[100]");

                if (sym.StrSE1v == "n")
                    str.Add("h0l: h+l=2n: n⊥[010]");

                if (sym.StrSE1v == "n")
                    str.Add("hk0: h+k=2n: n⊥[001]");

                if (sym.StrSE1v == "d")
                    str.Add("0kl: k+l=4n: d⊥[100]");

                if (sym.StrSE1v == "d")
                    str.Add("h0l: h+l=4n: d⊥[010]");

                if (sym.StrSE1v == "d")
                    str.Add("hk0: h+k=4n: d⊥[001]");

                if (sym.StrSE3v == "c")
                    str.Add("h-hl: l=2n: c⊥[110]");

                if (sym.StrSE3v == "c")
                    str.Add("hhl: l=2n: c⊥[1-10]");

                if (sym.StrSE3v == "c")
                    str.Add("hk-k: h=2n: a⊥[011]");

                if (sym.StrSE3v == "c")
                    str.Add("hkk: h=2n: a⊥[01-1]");

                if (sym.StrSE3v == "c")
                    str.Add("hk-h: k=2n: b⊥[101]");

                if (sym.StrSE3v == "c")
                    str.Add("hkh: k=2n: b⊥[10-1]");

                if (sym.StrSE3v == "n")
                    str.Add("hk-k: h-2k=2n: n⊥[011]");

                if (sym.StrSE3v == "n")
                    str.Add("hkk: h+2k=2n: n⊥[01-1]");

                if (sym.StrSE3v == "n")
                    str.Add("hk-h: 2h-k=2n: n⊥[101]");

                if (sym.StrSE3v == "n")
                    str.Add("hkh: 2h+k=2n: n⊥[-101]");

                if (sym.StrSE3v == "n")
                    str.Add("h-hl: 2h-l=2n: n⊥[110]");

                if (sym.StrSE3v == "n")
                    str.Add("hhl: 2h+l=2n: n⊥[1-10]");

                if (sym.StrSE3v == "d")
                    str.Add("hk-k: h-2k=4n: d⊥[011]");

                if (sym.StrSE3v == "d")
                    str.Add("hkk: h+2k=4n: d⊥[01-1]");

                if (sym.StrSE3v == "d")
                    str.Add("hk-h: 2h-k=4n: d⊥[101]");

                if (sym.StrSE3v == "d")
                    str.Add("hkh: 2h+k=4n: d⊥[-101]");

                if (sym.StrSE3v == "d")
                    str.Add("h-hl: 2h-l=4n: d⊥[110]");

                if (sym.StrSE3v == "d")
                    str.Add("hhl: 2h+l=4n: d⊥[1-10]");

                break;
        }
        return str.ToArray();
        #endregion
    }
    #endregion
}
