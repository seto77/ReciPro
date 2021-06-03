using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Crystallography
{
    [Serializable()]
    public class WyckoffPosition
    {
        /// <summary>
        /// 空間群の番号
        /// </summary>
        public int SymmetrySeriesNumber;

        /// <summary>
        /// 格子のタイプ
        /// </summary>
        public string LatticeType;

        /// <summary>
        /// 多重度 (整数)
        /// </summary>
        public int Multiplicity;

        /// <summary>
        /// ワイコフ文字
        /// </summary>
        public string WyckoffLetter;

        /// <summary>
        /// ワイコフナンバー (一般位置が0, 特殊になるほど数字が大)
        /// </summary>
        public int WyckoffNumber;

        /// <summary>
        /// サイトシンメトリ
        /// </summary>
        public string SiteSymmetry;

        /// <summary>
        /// 等価位置を生成するFuncの配列
        /// </summary>
        [XmlIgnore]
        public Func<double, double, double, (double X, double Y, double Z)>[] PositionGenerator;

        /// <summary>
        /// 等価位置の文字列(x,y,zなど)の配列
        /// </summary>
        public string[] PositionStr;

        /// <summary>
        /// 等価位置の対称操作をSymmetryOperationクラスとして格納したもの
        /// </summary>
        public SymmetryOperation[] PositionOperations;

        /// <summary>
        /// 自由度 (このワイコフ位置がx,y,zなどの変数を含む場合はtrue, 含まない場合はfalse)
        /// </summary>
        public bool FreedomX, FreedomY, FreedomZ;

        public WyckoffPosition()
        {
            PositionGenerator = null;
        }

        public WyckoffPosition(int symSeries, string latticeType, string wykLetter, int wykNum, string siteSym,
            string[] coordStr,
            Func<double, double, double, (double X, double Y, double Z)>[] generators,
            SymmetryOperation[] operations = null)
        {
            SymmetrySeriesNumber = symSeries;
            LatticeType = latticeType;

            WyckoffLetter = wykLetter;
            WyckoffNumber = wykNum;
            SiteSymmetry = siteSym;

            PositionStr = coordStr;
            PositionGenerator = generators;
            Multiplicity = generators.Length;

            PositionOperations = operations;

            if (PositionStr == null || PositionStr.Length == 0)
                FreedomX = FreedomY = FreedomZ = true;
            else
            {
                var tempStr = PositionStr[0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (tempStr.Length == 3)
                {
                    FreedomX = tempStr[0].Contains('x');
                    FreedomY = tempStr[1].Contains('y');
                    FreedomZ = tempStr[2].Contains('z');
                }
                else
                    FreedomX = FreedomY = FreedomZ = true;
            }
        }

        /// <summary>
        /// 与えられたx,y,zで、このワイコフ位置を再生
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public List<Vector3D> GeneratePositions(double x, double y, double z)
        {
            var pos = new List<Vector3D>();
            var th = SymmetryStatic.Th;

            for (int i = 0; i < PositionGenerator.Length; i++)
            {
                var (X, Y, Z) = PositionGenerator[i](x, y, z);

                //0~1の範囲に収まるかどうかチェックし、適宜修正
                var v = new Vector3D(X, Y, Z, false).InnerLattice();
                //当たり判定
                bool flag = true;
                for (int j = 0; j < pos.Count && flag; j++)
                    for (int xx = (v.X < th) ? -1 : 0; xx <= ((v.X > 1 - th) ? 1 : 0) && flag; xx++)
                        for (int yy = (v.Y < th) ? -1 : 0; yy <= ((v.Y > 1 - th) ? 1 : 0) && flag; yy++)
                            for (int zz = (v.Z < th) ? -1 : 0; zz <= ((v.Z > 1 - th) ? 1 : 0) && flag; zz++)
                                if ((v.X - xx - pos[j].X) * (v.X - xx - pos[j].X) + (v.Y - yy - pos[j].Y) * (v.Y - yy - pos[j].Y) + (v.Z - zz - pos[j].Z) * (v.Z - zz - pos[j].Z) < th * th)
                                {
                                    flag = false;
                                    if (!flag)
                                        break;
                                }
                if (flag)
                {
                    if (PositionOperations != null)
                        v.Operation = PositionOperations[i];//PositionOperatorsを格納
                    pos.Add(v);
                }
            }
            return pos;
        }

        /// <summary>
        /// 与えられたposがこのWykoffPositionかどうかを判定する
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool CheckPosition(double x, double y, double z)
        {
            static bool chk(double d1, double d2)
            {
                var d = d1 - d2;
                while (d > 0.5)
                    d--;
                while (d < -0.5)
                    d++;
                return Math.Abs(d) < SymmetryStatic.Th;
            }

            foreach (var (X, Y, Z) in PositionGenerator.Select(generator => generator(x, y, z)))
            {
                if (chk(X, x) && chk(Y, y) && chk(Z, z))
                    return true;
            }
            return false;
        }


        /// <summary>
        /// 引数の空間群による対称操作で映る原子位置(pos)の等価な原子位置をクラスAtomsでかえす
        /// </summary>
        /// <param name="Pos"></param>
        /// <param name="SymmetrySeriesNumber"></param>
        /// <returns></returns>
        public static Atoms GetEquivalentAtomsPosition((double X, double Y, double Z) Pos, int SymmetrySeriesNumber)
        {
            if (double.IsNaN(Pos.X) || double.IsNaN(Pos.Y) || double.IsNaN(Pos.Z))
                return new Atoms();

            var atoms = new Atoms();
            var wykc = SymmetryStatic.WyckoffPositions[SymmetrySeriesNumber];
            //まず、もっとも対称性の低いワイコフ位置で原子位置を再生
            atoms.Atom = wykc[0].GeneratePositions(Pos.X, Pos.Y, Pos.Z);

            //ワイコフ位置判定
            atoms.WyckoffLeter = "{";
            atoms.SiteSymmetry = "";
            atoms.Multiplicity = 0;
            string wyckLet = "";
            string siteSym = "";
            int multi = 0;
            int wyckNum = 0;

            for (int j = wykc.Length - 1; j >= 0; j--)
            {

                //2020/05/15 一部のtrigonal, hexagonalで正しい判定が出来ない問題の対応
                // (0.2, 0.1, 0)という座標は (x, -x, 0), (x, 2x, 0), (-2x, -x, 0)というワイコフ位置 (P321, 3j)
                // にもかかわらず、正しく判定することが出来ない. そのため、もっとも低対称性で再生した位置全てに対して判定を
                // おこない、一回でもOKだったらそのワイコフ位置だと判定する

                var pos = SymmetrySeriesNumber >= 438 && SymmetrySeriesNumber <= 488 ?
                    atoms.Atom.Select(a => (a.X, a.Y, a.Z)).ToArray() : new[] { (Pos.X, Pos.Y, Pos.Z) };


                foreach (var (X, Y, Z) in pos)
                    if (wykc[j].CheckPosition(X,Y,Z))
                    {
                        multi = wykc[j].Multiplicity;
                        wyckLet = wykc[j].WyckoffLetter;
                        siteSym = wykc[j].SiteSymmetry;
                        wyckNum = j;
                        break;
                    }
                if (multi != 0)
                    break;
            }

            if (atoms.WyckoffLeter.ToCharArray()[0] > wyckLet.ToCharArray()[0])
            {
                atoms.WyckoffLeter = wyckLet;
                atoms.SiteSymmetry = siteSym;
                atoms.Multiplicity = multi;
                atoms.WyckoffNumber = wyckNum;
            }
            return atoms;
        }
        public static Atoms GetEquivalentAtomsPosition(Vector3D Pos, int SymmetrySeriesNumber)
            => GetEquivalentAtomsPosition((Pos.X, Pos.Y, Pos.Z), SymmetrySeriesNumber);
    }
}