using Microsoft.Scripting.Utils;
using System;
using System.Buffers;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace Crystallography.Controls;

public partial class DataSet
{
    #region 共通の静的関数
    public static Bitmap ColorImage(int argb)
    {
        var bmp = new Bitmap(40, 15);
        var g = Graphics.FromImage(bmp);
        g.Clear(Color.FromArgb(argb));
        return bmp;
    }

    public static void MoveItemBase(DataRowCollection rows, int srcIndex, int destIndex)
    {
        if (srcIndex < rows.Count && destIndex < rows.Count)
            for (int j = 0; j < rows[srcIndex].ItemArray.Length; j++)
            {
                (rows[destIndex][j], rows[srcIndex][j]) = (rows[srcIndex][j], rows[destIndex][j]);
            }
    }

    public static void ReplaceBase(DataRowCollection rows, DataRow r, int i)
    {
        for (int j = 0; j < rows[i].ItemArray.Length; j++)
            rows[i][j] = r[j];
    }
    #endregion

    partial class DataTableBoundDataTable
    {
        public Bound Get(int i) => Rows[i][BoundColumn] as Bound;
        public Bound[] GetAll() => Rows.Select(r => (r as DataTableBoundRow).Bound as Bound).ToArray();
        public void Replace(Bound bound, int i) => ReplaceBase(Rows, createRow(bound), i);
        public void Add(Bound bound) => Rows.Add(createRow(bound));
        public new void Clear() => Rows.Clear();
        public void Remove(int i) => Rows.RemoveAt(i);
        public void MoveItem(int srcIndex, int destIndex) => MoveItemBase(Rows, srcIndex, destIndex);

        private DataTableBoundRow createRow(Bound bound)
        {
            var dr = NewDataTableBoundRow();
            dr.Bound = bound;
            dr.Enabled = bound.Enabled;
            dr.h = bound.Index.H;
            dr.k = bound.Index.K;
            dr.l = bound.Index.L;
            dr.Equivalency = bound.Equivalency;
            dr.Distance = (bound.Distance * 10).ToString("f3");
            dr.MultipleOfD = bound.MultipleOfD.ToString("f3");

            dr.Color = ColorImage(bound.ColorArgb);
            return dr;
        }
    }

    partial class DataTableLatticePlaneDataTable
    {
        public LatticePlane Get(int i) => Rows[i][LatticePlaneColumn] as LatticePlane;
        public LatticePlane[] GetAll() => Rows.Select(r => (r as DataTableLatticePlaneRow).LatticePlane as LatticePlane).ToArray();
        public void Replace(LatticePlane bound, int i) => ReplaceBase(Rows, createRow(bound), i);
        public void Add(LatticePlane bound) => Rows.Add(createRow(bound));
        public new void Clear() => Rows.Clear();
        public void Remove(int i) => Rows.RemoveAt(i);
        public void MoveItem(int srcIndex, int destIndex) => MoveItemBase(Rows, srcIndex, destIndex);

        private DataTableLatticePlaneRow createRow(LatticePlane plane)
        {
            var dr = NewDataTableLatticePlaneRow();
            dr.LatticePlane = plane;
            dr.Enabled = plane.Enabled;
            dr.h = plane.Index.H;
            dr.k = plane.Index.K;
            dr.l = plane.Index.L;
            dr.Translation = plane.Translation.ToString("f3");

            dr.Color = ColorImage(plane.ColorArgb);
            return dr;
        }
    }

    partial class DataTableBondDataTable
    {
        public Bonds Get(int i) => Rows[i][BondColumn] as Bonds;
        public Bonds[] GetAll() => Rows.Select(r => (r as DataTableBondRow)[BondColumn] as Bonds).ToArray();
        public void Replace(Bonds bonds, int i) => ReplaceBase(Rows, createRow(bonds), i);
        public void Add(Bonds bonds) => Rows.Add(createRow(bonds));
        public new void Clear() => Rows.Clear();
        public void Remove(int i) => Rows.RemoveAt(i);
        public void MoveItem(int srcIndex, int destIndex) => MoveItemBase(Rows, srcIndex, destIndex);
        private DataTableBondRow createRow(Bonds bonds)
        {
            var dr = NewDataTableBondRow();
            dr[BondColumn] = bonds;
            dr[EnabledColumn] = bonds.Enabled;
            dr[CenterColumn] = bonds.Element1;
            dr[VertexColumn] = bonds.Element2;
            dr[_Max_len_Column] = (bonds.MaxLength * 10.0).ToString("f4");//表示はÅ単位
            dr[_Min_len_Column] = (bonds.MinLength * 10.0).ToString("f4");//表示はÅ単位
            dr[Show_bondsColumn] = bonds.ShowBond;
            dr[Show_PolyhedronColumn] = bonds.ShowPolyhedron;

            return dr;
        }
    }

    partial class DataTableAtomDataTable
    {
        public Atoms Get(int i) => Rows[i][AtomColumn] as Atoms;
        public Atoms[] GetAll() => Rows.Select(r => (r as DataTableAtomRow)[AtomColumn] as Atoms).ToArray();
        public void Replace(Atoms atoms, int i) => ReplaceBase(Rows, createRow(atoms), i);
        public void Add(Atoms atom) => Rows.Add(createRow(atom));
        public new void Clear() => Rows.Clear();
        public void Remove(int i) => Rows.RemoveAt(i);
        public void MoveItem(int srcIndex, int destIndex) => MoveItemBase(Rows, srcIndex, destIndex);

        private DataTableAtomRow createRow(Atoms atom)
        {
            var dr = this.NewDataTableAtomRow();
            dr[this.AtomColumn] = atom;
            dr[this.EnabledColumn] = atom.GLEnabled;
            dr[this.LabelColumn] = atom.Label;
            dr[this._Site_Sym_Column] = atom.SiteSymmetry;
            dr[this._Wyck__Let_Column] = atom.WyckoffLeter;
            dr[this.ElementColumn] = atom.ElementName;
            dr[this.XColumn] = GetStringFromDouble(atom.X, 6, true);
            dr[this.YColumn] = GetStringFromDouble(atom.Y, 6, true);
            dr[this.ZColumn] = GetStringFromDouble(atom.Z, 6, true);
            dr[this._columnMulti_] = atom.Multiplicity;
            dr[this._Occ_Column] = GetStringFromDouble(atom.Occ, 6, false);
            return dr;
        }


        public static string GetStringFromDouble(double d, int decimalPlaces, bool fraction)
        {
            #region 

            var threshold = Math.Pow(10, -decimalPlaces);

            var text = "";
            if (d != 0 && fraction) //分数で表示するとき
            {
                int j = (int)Math.Ceiling(d - 1);
                foreach (var denom in new[] { 2, 3, 4, 5, 6, 8, 9, 10, 11, 12, 16, 24 })
                    for (int i = 1; i < denom && text.Length == 0; i++)
                        if ((i == 1 || denom % i != 0) && Math.Abs(d - j - i / (double)denom) < threshold)
                            text = $"{i + (denom * j)}/{denom}";
            }

            return text != "" ? text : d.ToString($"g{decimalPlaces}");
            #endregion
        }
    }

    partial class DataTableScatteringFactorDataTable
    {
        public void Add(int h, int k, int l, int mult, double d, double twoTheta, Complex f, double relInt, string[] condition)
        {
            DataRow dr = this.NewDataTableScatteringFactorRow();
            dr[this.HColumn] = h;
            dr[this.KColumn] = k;
            dr[this.LColumn] = l;
            dr[this.MultiColumn] = mult;
            dr[this.DColumn] = d;
            dr[this.QColumn] = Math.PI * 2 / d;
            dr[this.TwoThetaColumn] = twoTheta;
            dr[this.F_realColumn] = Math.Abs(f.Real) > 1E-18 ? f.Real : 0;
            dr[this.F_invColumn] = Math.Abs(f.Imaginary) > 10E-18 ? f.Imaginary : 0;
            dr[this.FColumn] = f.Magnitude > 1E-18 ? f.Magnitude : 0;
            dr[this.F2Column] = f.Magnitude * f.Magnitude > 1E-18 ? f.Magnitude * f.Magnitude : 0;
            dr[RelIntColumn] = relInt * 100 > 1E-18 ? relInt * 100 : 0;

            var str = new System.Text.StringBuilder();
            for (int m = 0; m < condition.Length; m++)
                str.Append(m == 0 ? condition[m] : " & " + condition[m]);

            dr[columnCondition] = str.ToString();

            this.Rows.Add(dr);
        }

        public new void Clear() => Rows.Clear();
    }

    partial class DataTableCrystalDatabaseDataTable
    {
        public void SetFlag(int i, bool flag) => Rows[i][columnFlag] = flag;
        public bool GetFlag(int i) => (bool)Rows[i][columnFlag];

        /// <summary>
        /// 引数はbindingSourceMain.Currentオブジェクト. 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public Crystal2 Get(object o) => o is DataRowView drv && drv.Row is DataTableCrystalDatabaseRow r ? (Crystal2)r[Crystal2Column] : null;


        /// <summary>
        /// 引数はbindingSourceMain.Currentオブジェクト. 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public Crystal2 Get(int i) => (Crystal2)Rows[i][0];

        public void Add(Crystal2 crystal) => Add(CreateRow(crystal));
        public void Add(DataTableCrystalDatabaseRow row) => Rows.Add(row);

        public new void Clear() => Rows.Clear();

        public void Remove(int i) => Rows.RemoveAt(i);

        /// <summary>
        /// srcCrystalはbindingSourceMain.Currentオブジェクト. 
        /// </summary>
        /// <param name="srcCrystal"></param>
        /// <param name="targetcrystal"></param>
        public void Replace(object srcCrystal, Crystal2 targetcrystal)
        {
            if (srcCrystal is DataRowView drv && drv.Row is DataTableCrystalDatabaseRow src)
            {
                var target = CreateRow(targetcrystal);
                for (int j = 0; j < drv.Row.ItemArray.Length; j++)
                    src[j] = target[j];
            }
        }

        readonly object lockObj = new();
        public DataTableCrystalDatabaseRow CreateRow(Crystal2 c)
        {
            DataTableCrystalDatabaseRow dr;
            lock (lockObj)
                dr = NewDataTableCrystalDatabaseRow();

            dr.Crystal2 = c;
            dr.Name = c.name;
            dr.Formula = c.formula;
            dr.Density = c.density;
            (dr.A, dr.B, dr.C, dr.Alpha, dr.Beta, dr.Gamma) = c.CellOnlyValue;
            dr.CrystalSystem = SymmetryStatic.StrArray[c.sym][16];//s.CrystalSystemStr;
            dr.PointGroup = SymmetryStatic.StrArray[c.sym][13];
            dr.SpaceGroup = SymmetryStatic.StrArray[c.sym][3];
            dr.Authors = c.auth;
            dr.Title = c.sect;
            dr.Journal = c.jour;
            dr.Flag = true;

            return dr;
        }

    }
}

