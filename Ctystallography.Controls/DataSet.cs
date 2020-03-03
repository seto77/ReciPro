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
                dr[this.F_realColumn] = Math.Abs(f.Real) > 10E-10 ? f.Real : 0;
                dr[this.F_invColumn] = Math.Abs(f.Imaginary) > 10E-10 ? f.Imaginary : 0;
                dr[this.FColumn] = f.Magnitude > 10E-10 ? f.Magnitude : 0;
                dr[this.F2Column] = f.Magnitude * f.Magnitude > 10E-10 ? f.Magnitude * f.Magnitude : 0;
                dr[RelIntColumn] = relInt * 100 > 10E-10 ? relInt * 100 : 0;

                string str = "";
                if (condition.Length == 1)
                    str = condition[0];
                else if (condition.Length > 1)
                    for (int m = 0; m < condition.Length; m++)
                        str += condition[m] + " & ";
                dr[columnCondition] = str.TrimEnd(new[] { ' ', '&' });

                this.Rows.Add(dr);
            }

            public new void Clear()
            {
                this.Rows.Clear();
            }
        }
    }
}