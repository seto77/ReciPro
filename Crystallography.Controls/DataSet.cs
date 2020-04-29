using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using Crystallography;
using Microsoft.Scripting.Utils;

namespace Crystallography.Controls
{
}

namespace Crystallography.Controls
{
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
                    var obj = rows[srcIndex][j];
                    rows[srcIndex][j] = rows[destIndex][j];
                    rows[destIndex][j] = obj;
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
                //dr[EnabledColumn] = bound.Enabled;
                dr.h = bound.BaseIndex.H;
                dr.k = bound.BaseIndex.K;
                dr.l = bound.BaseIndex.L;
                dr.Equivalency = bound.Equivalency;
                dr.Distance = bound.Distance.ToString("f3");
                //dr.DistanceD = bound.Distance;

                dr.Color = ColorImage(bound.ColorArgb);
                return dr;
            }
        }

        partial class DataTableLatticePlaneDataTable
        {
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
                dr[_Max_len_Column] = bonds.MaxLength;
                dr[_Min_len_Column] = bonds.MinLength;
                dr[Bond_colorColumn] = ColorImage(bonds.ArgbBond);
                dr[Polyhedron_colorColumn] = ColorImage(bonds.ArgbPolyhedron);

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
                dr[this.LabelColumn] = atom.Label;
                dr[this._Site_Sym_Column] = atom.SiteSymmetry;
                dr[this._Wyck__Let_Column] = atom.WyckoffLeter;
                dr[this.ElementColumn] = atom.ElementName;
                dr[this.XColumn] = GetStringFromDouble(atom.X);
                dr[this.YColumn] = GetStringFromDouble(atom.Y);
                dr[this.ZColumn] = GetStringFromDouble(atom.Z);
                dr[this._columnMulti_] = atom.Multiplicity;
                dr[this._Occ_Column] = atom.Occ;
                return dr;
            }

            public static string GetStringFromDouble(double d)
            {
                #region 
                if (Math.Abs(d - 0.125) < 0.000000001) return "1/8";
                else if (Math.Abs(d - 0.375) < 0.000000001) return "3/8";
                else if (Math.Abs(d - 0.625) < 0.000000001) return "5/8";
                else if (Math.Abs(d - 0.875) < 0.000000001) return "7/8";
                else if (Math.Abs(d - 0.25) < 0.000000001) return "1/4";
                else if (Math.Abs(d - 0.75) < 0.000000001) return "3/4";
                else if (Math.Abs(d - 0.5) < 0.000000001) return "1/2";
                else if (Math.Abs(d - 1.0 / 3.0) < 0.000000001) return "1/3";
                else if (Math.Abs(d - 2.0 / 3.0) < 0.000000001) return "2/3";
                else if (Math.Abs(d - 1.0 / 6.0) < 0.000000001) return "1/6";
                else if (Math.Abs(d - 5.0 / 6.0) < 0.000000001) return "5/6";
                else if (Math.Abs(d - 1.0 / 12.0) < 0.000000001) return "1/12";
                else if (Math.Abs(d - 5.0 / 12.0) < 0.000000001) return "5/12";
                else if (Math.Abs(d - 7.0 / 12.0) < 0.000000001) return "7/12";
                else if (Math.Abs(d - 11.0 / 12.0) < 0.000000001) return "11/12";
                else if (Math.Abs(d - 1.0 / 24.0) < 0.000000001) return "1/24";
                else if (Math.Abs(d - 5.0 / 24.0) < 0.000000001) return "5/24";
                else if (Math.Abs(d - 7.0 / 24.0) < 0.000000001) return "7/24";
                else if (Math.Abs(d - 11.0 / 24.0) < 0.000000001) return "11/24";
                else if (Math.Abs(d - 13.0 / 24.0) < 0.000000001) return "13/24";
                else if (Math.Abs(d - 17.0 / 24.0) < 0.000000001) return "17/24";
                else if (Math.Abs(d - 19.0 / 24.0) < 0.000000001) return "19/24";
                else if (Math.Abs(d - 23.0 / 24.0) < 0.000000001) return "23/24";
                else return d.ToString("g6");
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
    }
}

