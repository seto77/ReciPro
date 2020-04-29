using System;
using System.Data;
using System.Drawing;
using System.Numerics;
using Crystallography;

namespace ReciPro
{
    public partial class DataSet
    {
        partial class DataTableBondDataTable
        {
            public Bonds Get(int i) => Rows[i][BondColumn] as Bonds;

            public void Set(Bonds bonds, int i)
            {
                var dr = createRow(bonds);

                for (int j = 0; j < Columns.Count; j++)
                    Rows[i][j] = dr[j];
            }

            public void Add(Bonds bonds) => Rows.Add(createRow(bonds));

            public new void Clear() => Rows.Clear();

            public void Remove(int i) => Rows.RemoveAt(i);

            private DataTableBondRow createRow(Bonds bonds)
            {
                var dr = NewDataTableBondRow();
                dr[BondColumn] = bonds;
                dr[EnabledColumn] = bonds.Enabled;
                dr[CenterColumn] = bonds.Element1;
                dr[VertexColumn] = bonds.Element2;
                dr[_Max_len_Column] = bonds.MaxLength;
                dr[_Min_len_Column] = bonds.MinLength;

                var bmp1 = new Bitmap(40, 15);
                var g1 = Graphics.FromImage(bmp1);
                g1.Clear(Color.FromArgb(bonds.ArgbBond));
                dr[Bond_colorColumn] = bmp1;

                var bmp2 = new Bitmap(40, 15);
                var g2 = Graphics.FromImage(bmp2);
                g2.Clear(Color.FromArgb(bonds.ArgbPolyhedron));
                dr[Polyhedron_colorColumn] = bmp2;

                return dr;
            }
        }

        partial class DataTableAtomDataTable
        {
            public Atoms Get(int i) => Rows[i][AtomColumn] as Atoms;

            public void Set(Atoms atoms, int i)
            {
                var dr = createRow(atoms);

                for (int j = 0; j < Columns.Count; j++)
                    Rows[i][j] = dr[j];
            }

            public void Add(Atoms atom) => Rows.Add(createRow(atom));

            public new void Clear() => Rows.Clear();

            public void Remove(int i) => Rows.RemoveAt(i);

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

            public void MoveItem(int srcIndex, int destIndex)
            {
                if (srcIndex < this.Rows.Count && destIndex < this.Rows.Count)
                {
                    for (int j = 0; j < Columns.Count; j++)
                    {
                        var obj = Rows[srcIndex][j];
                        this.Rows[srcIndex][j] = this.Rows[destIndex][j];
                        this.Rows[destIndex][j] = obj;
                    }
                }
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

            public new void Clear()
            {
                this.Rows.Clear();
            }
        }
    }
}