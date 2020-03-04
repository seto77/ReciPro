using System;
using System.Data;
using System.Numerics;

namespace Crystallography.Controls
{
    public partial class DataSet
    {
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

                System.Text.StringBuilder str = new System.Text.StringBuilder();
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