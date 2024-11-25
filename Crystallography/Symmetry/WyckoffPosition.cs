using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Crystallography;

/// <summary>
/// ワイコフポジション構造体. SymmetryStaticで初期化される.
/// </summary>
[Serializable()]
public readonly struct WyckoffPosition
{
    #region 定数
    const double th = 0.00015;
    const double one_th = 1 - th * 2;
    #endregion

    #region フィールド、プロパティ
    /// <summary>
    /// 空間群の番号
    /// </summary>
    public ushort SymmetrySeriesNumber { get; }

    /// <summary>
    /// 格子のタイプ
    /// </summary>
    public string LatticeType { get; }

    /// <summary>
    /// 多重度 (整数)
    /// </summary>
    public byte Multiplicity { get; }

    /// <summary>
    /// ワイコフ文字
    /// </summary>
    public string WyckoffLetter { get; }

    /// <summary>
    /// ワイコフナンバー (一般位置が0, 特殊になるほど数字が大)
    /// </summary>
    public byte WyckoffNumber { get; }

    /// <summary>
    /// サイトシンメトリ
    /// </summary>
    public string SiteSymmetry { get; }

    /// <summary>
    /// 等価位置を生成するFuncの配列
    /// </summary>
    [XmlIgnore]
    public Func<double, double, double, (double X, double Y, double Z)>[] PositionGenerator { get; }

    /// <summary>
    /// 等価位置の文字列(x,y,zなど)の配列
    /// </summary>
    public string[] PositionStr { get; }

    /// <summary>
    /// 等価位置の対称操作をSymmetryOperationクラスとして格納したもの
    /// </summary>
    public SymmetryOperation[] PositionOperations { get; }

    /// <summary>
    /// 自由度 (このワイコフ位置がx,y,zなどの変数を含む場合はtrue, 含まない場合はfalse)
    /// </summary>
    public (bool X, bool Y, bool Z) Free { get; }

    private static readonly char[] separator = [','];

    #endregion

    #region コンストラクタ
    public WyckoffPosition(int symSeries, string latticeType, string wykLetter, int wykNum, string siteSym,
        string[] coordStr,
        Func<double, double, double, (double X, double Y, double Z)>[] generators,
        SymmetryOperation[] operations = null)
    {
        SymmetrySeriesNumber = (ushort)symSeries;
        LatticeType = latticeType;

        WyckoffLetter = wykLetter;
        WyckoffNumber = (byte)wykNum;
        SiteSymmetry = siteSym;

        PositionStr = coordStr;
        PositionGenerator = generators;
        Multiplicity = (byte)generators.Length;

        PositionOperations = operations;

        if (PositionStr == null || PositionStr.Length == 0)
            Free = (true, true, true);
        else
        {
            var tempStr = PositionStr[0].Split(separator, StringSplitOptions.RemoveEmptyEntries);
            if (tempStr.Length == 3)
                Free = (tempStr[0].Contains('x'), tempStr[1].Contains('y'), tempStr[2].Contains('z'));
            else
                Free = (true, true, true);
        }
    }
    #endregion

    #region メソッド
    /// <summary>
    /// 与えられたx,y,zで、このワイコフ位置を再生
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public readonly Vector3D[] GeneratePositions(in double x, in double y, in double z)
    {
        var pos = new List<Vector3D>(PositionGenerator.Length);

        for (int i = 0; i < PositionGenerator.Length; i++)
        {
            var (X, Y, Z) = PositionGenerator[i](x, y, z);

            //当たり判定
            if (pos.Count == 0 || pos.All(p => !chk(Z, p.Z) || !chk(X, p.X) || !chk(Y, p.Y)))
            {
                var v = new Vector3D(X, Y, Z, false);
                //0~1の範囲に収まるかどうかチェックし、修正
                v.InnerLatticeThis();
                if (PositionOperations != null)
                    v.Operation = new SymmetryOperation(PositionOperations[i], SymmetrySeriesNumber);//PositionOperatorsを格納
                pos.Add(v);
            }
        }
        return [.. pos];
    }
    /// <summary>
    /// 与えられたposがこのWykoffPositionかどうかを判定する
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public readonly bool CheckPosition(double x, double y, double z)
    {
        return PositionGenerator.Any(g =>
        {
            var (X, Y, Z) = g(x, y, z);
            return chk(X, x) && chk(Y, y) && chk(Z, z);
        });
    }
    static bool chk(in double d1, in double d2)
    {
        var d = Math.Abs(d1 - d2);
        if (d >= 1 || d < 0) d -= Math.Floor(d);
        if (d > one_th) d -= 1;
        return d <= th;
    }
    #endregion

    #region static メソッド
    /// <summary>
    /// 引数の空間群による対称操作で映る原子位置(pos)の等価な原子位置をクラスAtomsでかえす
    /// </summary>
    /// <param name="Pos"></param>
    /// <param name="SymmetrySeriesNumber"></param>
    /// <returns></returns>
    public static Atoms GetEquivalentAtomsPosition(in (double X, double Y, double Z) Pos, in int SymmetrySeriesNumber)
    {
        if (double.IsNaN(Pos.X) || double.IsNaN(Pos.Y) || double.IsNaN(Pos.Z))
            return new Atoms();

        var atoms = new Atoms();
        var wyck = SymmetryStatic.WyckoffPositions[SymmetrySeriesNumber];
        //まず、もっとも対称性の低いワイコフ位置で原子位置を再生
        atoms.Atom = wyck[0].GeneratePositions(Pos.X, Pos.Y, Pos.Z);

        //ワイコフ位置判定
        atoms.WyckoffLeter = "{";
        atoms.SiteSymmetry = "";
        atoms.Multiplicity = 0;
        string wyckLet = "";
        string siteSym = "";
        int multi = 0;
        int wyckNum = 0;

        var atomsTemp = atoms.Atom.Select(a => (a.X, a.Y, a.Z)).ToArray();

        for (int j = wyck.Length - 1; j >= 0; j--)
        {

            //2020/05/15 一部のtrigonal, hexagonalで正しい判定が出来ない問題の対応
            // (0.2, 0.1, 0)という座標は (x, -x, 0), (x, 2x, 0), (-2x, -x, 0)というワイコフ位置 (P321, 3j)
            // にもかかわらず、正しく判定することが出来ない.
            //2022/06/16 服部さんから、P4_2/nmc(2)でも、(3/4,y,z)が上手く判定できていないとの指摘を受ける

            // そのため、もっとも低対称性で再生した位置全てに対して判定をおこない、一回でもOKだったらそのワイコフ位置だと判定する
            if (wyck[j].Multiplicity == atoms.Atom.Length)
                foreach (var (X, Y, Z) in atomsTemp)
                    if (wyck[j].CheckPosition(X, Y, Z))
                    {
                        multi = wyck[j].Multiplicity;
                        wyckLet = wyck[j].WyckoffLetter;
                        siteSym = wyck[j].SiteSymmetry;
                        wyckNum = j;
                        break;
                    }

            if (multi != 0)
                break;
        }

        atoms.WyckoffLeter = wyckLet;
        atoms.SiteSymmetry = siteSym;
        atoms.Multiplicity = atoms.Atom.Length;
        atoms.WyckoffNumber = wyckNum;

        return atoms;
    }

    #endregion
}
