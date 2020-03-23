using Crystallography;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ReciPro
{
    public partial class DataSet
    {
        partial class DataTableBetheDataTable
        {
        }

        partial class DataTableGrainDataTable
        {
            /*   public Matrix3D[] Matrices
               {
                   get
                   {
                       var mList = new List<Matrix3D>();
                       for (int i = 0; i < this.Count; i++)
                           mList.Add((Matrix3D)this.Rows[i]["Matrix"]);
                       return mList.ToArray();
                   }
               }
               */

            public void Add(FormSpotID.Grain grain)
            {
                DataRow dr = NewDataTableGrainRow();
                dr["Grain"] = grain;
                dr["No"] = this.Count;
                dr["AssignedSpots"] = grain.Indices.Count();
                dr["CrystalName"] = grain.CrystalName;
                //dr["AssignedSpots"] = candidate.Sum(c => c.Indices.Count());
                this.Rows.Add(dr);
            }
        }

        partial class DataTableCandidateDataTable
        {
            public void Add(int num, List<FormSpotID.Grain> candidate)
            {
                DataRow dr = NewDataTableCandidateRow();
                dr["Candidate"] = candidate;
                dr["No"] = num.ToString();
                dr["AssignedSpots"] = candidate.Sum(g => g.Indices.Count()); ;
                this.Rows.Add(dr);
            }
        }

        partial class DataTableSpotDataTable
        {
            public AreaDetector AreaDetector;

            /// <summary>
            /// プロパティ ダイレクトスポットを取得する.存在しない場合はPointD(NaN,NaN)を返す.
            /// </summary>
            public PointD DirectSpotPosition
            {
                get
                {
                    for (int i = 0; i < Rows.Count; i++)
                        if (Rows[i].RowState != DataRowState.Detached && (bool)Rows[i]["Direct"])
                            return new PointD((double)Rows[i]["x0"], (double)Rows[i]["y0"]);
                    return new PointD(double.NaN, double.NaN);
                }
            }

            /// <summary>
            /// プロパティ ダイレクトスポットのIndex(No)を取得する. 存在しない場合は-1を返す.
            /// </summary>
            public int DirectSpotNo
            {
                get
                {
                    for (int i = 0; i < Rows.Count; i++)
                        if (Rows[i].RowState != DataRowState.Detached && (bool)Rows[i]["Direct"])
                            return i;
                    return -1;
                }
            }

            /// <summary>
            /// プロパティ x,yがピクセル座標、zが積分強度を格納したタプル
            /// </summary>
            public List<(int No, double X, double Y, double A)> Spots
            {
                get
                {
                    if (Rows.Count == 0) return null;

                    var spots = new List<(int No, double X, double Y, double A)>();
                    for (int i = 0; i < Rows.Count; i++)
                        if (this.Rows[i].RowState != DataRowState.Detached)
                            spots.Add(((int)Rows[i]["No"], (double)Rows[i]["x0"], (double)Rows[i]["y0"], (double)Rows[i]["a"]));
                    return spots;
                }
            }

            public List<(double X, double Y)> SpotPositions
            {
                get
                {
                    if (Rows.Count == 0)
                        return null;

                    var spotPositions = new List<(double X, double Y)>();
                    for (int i = 0; i < Rows.Count; i++)
                        if (this.Rows[i].RowState != DataRowState.Detached)
                            spotPositions.Add(((double)Rows[i]["x0"], (double)Rows[i]["y0"]));
                    return spotPositions;
                }
            }

            /// <summary>
            /// プロパティ d値のリストを返す
            /// </summary>
            public List<double> Dscacing
            {
                get
                {
                    if (Rows.Count == 0) return null;

                    List<double> dList = new List<double>();
                    for (int i = 0; i < this.Count; i++)
                        dList.Add((double)this.Rows[i]["d"]);
                    return dList;
                }
            }

            /// <summary>
            /// プロパティ　逆格子ベクトルの配列を返す
            /// </summary>
            public List<Vector3DBase> ReciprocalVectors
            {
                get
                {
                    if (Rows.Count == 0)
                        return null;

                    List<Vector3DBase> vec = new List<Vector3DBase>();
                    for (int i = 0; i < this.Rows.Count; i++)
                        vec.Add(AreaDetector.convertClientToReciprocalSpace((double)Rows[i]["x0"], (double)Rows[i]["y0"]));

                    return vec;
                }
            }

            /// <summary>
            /// スポット追加
            /// </summary>
            /// <param name="direct"></param>
            /// <param name="range"></param>
            /// <param name="prmsPv"></param>
            /// <param name="prmsBg"></param>
            /// <param name="r"></param>
            public void Add(bool direct, double range, double[] prmsPv, double[] prmsBg, double r)
                => Add(direct, range, prmsPv[0], prmsPv[1], prmsPv[2], prmsPv[3], prmsPv[4], prmsPv[5], prmsPv[6], prmsBg[0], prmsBg[1], prmsBg[2], r);

            /// <summary>
            /// スポット追加
            /// </summary>
            /// <param name="direct"></param>
            /// <param name="x0"></param>
            /// <param name="y0"></param>
            /// <param name="h1"></param>
            /// <param name="h2"></param>
            /// <param name="theta"></param>
            /// <param name="eta"></param>
            /// <param name="a"></param>
            /// <param name="r"></param>
            public void Add(bool direct, double range, double x0, double y0, double h1, double h2, double theta, double eta, double a, double b0, double bx, double by, double r)
            {
                if (direct && AreaDetector != null)
                {
                    for (int i = 0; i < Rows.Count; i++)
                        Rows[i]["Direct"] = false;

                    Rows.Add(convertToDataRow(direct, range, x0, y0, h1, h2, theta, eta, a, b0, bx, by, r));
                    ResetDspacing();
                }
                else
                    Rows.Add(convertToDataRow(direct, range, x0, y0, h1, h2, theta, eta, a, b0, bx, by, r));
                resetNumber();
            }

            /// <summary>
            /// スポット削除
            /// </summary>
            /// <param name="pt"></param>
            public void Remove(int index)
            {
                Rows.RemoveAt(index);

                if (DirectSpotPosition.IsNaN && Rows.Count >= 1)
                {
                    Rows[0]["Direct"] = true;
                    ResetDspacing();
                }

                resetNumber();
            }

            /// <summary>
            /// DaraRowに変換するメソッド. Addから呼ばれる
            /// </summary>
            /// <param name="direct"></param>
            /// <param name="x0"></param>
            /// <param name="y0"></param>
            /// <param name="h1"></param>
            /// <param name="h2"></param>
            /// <param name="theta"></param>
            /// <param name="eta"></param>
            /// <param name="a"></param>
            /// <param name="r"></param>
            /// <returns></returns>
            private DataRow convertToDataRow(bool direct, double range, double x0, double y0, double h1, double h2, double theta, double eta, double a, double b0, double bx, double by, double r)
            {
                var dr = NewDataTableSpotRow();
                setRow(dr, direct, range, x0, y0, h1, h2, theta, eta, a, b0, bx, by, r);
                if (AreaDetector != null && !DirectSpotPosition.IsNaN)
                    dr.d = 1 / AreaDetector.convertClientToReciprocalSpace(x0, y0).Length;
                return dr;
            }

            /// <summary>
            /// indexで指定されたスポットの情報を変更する
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public void SetPrms(int index, double range, double[] prmsPv, double[] prmsBg, double r)
            {
                if (index < 0 || index > Rows.Count)
                    return;
                setRow((DataTableSpotRow)Rows[index], null, range, prmsPv, prmsBg, r);
            }

            /// <summary>
            /// indexで指定されたスポットの情報を返す
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public (bool Direct, int No, double Range, double X0, double Y0, double H1, double H2, double Theta, double Eta, double A, double B0, double Bx, double By, double R) GetPrms(int index)
            {
                if (index < 0 || index > Rows.Count || Rows[index].RowState == DataRowState.Detached)
                    return (false, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

                var r = (DataTableSpotRow)Rows[index];
                var Direct = r.Direct;
                var No = r.No;

                var (Range, X0, Y0, H1, H2, Theta, Eta, A, B0, Bx, By, R) =
                    ConvertPrmsToOriginalValues(r.Range, r.x0, r.y0, r.H1, r.H2, r.θ, r.η, r.A, r.B0, r.Bx, r.By, r: r.R);

                return (Direct, No, Range, X0, Y0, H1, H2, Theta, Eta, A, B0, Bx, By, R);
            }

            private void setRow(DataTableSpotRow dr, bool? direct, double range, double[] prms1, double[] prms2, double r)
                => setRow(dr, direct, range, prms1[0], prms1[1], prms1[2], prms1[3], prms1[4], prms1[5], prms1[6], prms2[0], prms2[1], prms2[2], r);

            private void setRow(DataTableSpotRow dr, bool? direct, double range, double x0, double y0, double h1, double h2, double theta, double eta, double a, double b0, double bx, double by, double r)
            {
                var H1 = h1;
                var H2 = h2;
                if (h1 < h2)
                {
                    H1 = h2;
                    H2 = h1;
                    theta += Math.PI / 2;
                }
                while (theta > Math.PI)
                    theta -= Math.PI;
                while (theta < 0)
                    theta += Math.PI;
                var Theta = theta / Math.PI * 180.0;
                var B0 = b0 + bx * x0 + by * y0;
                var R = r * 100;

                if (direct != null)
                    dr.Direct = (bool)direct;
                dr.Range = range;
                dr.x0 = x0;
                dr.y0 = y0;
                dr.H1 = H1;
                dr.H2 = H2;
                dr.θ = Theta;
                dr.η = eta;
                dr.A = a;
                dr.B0 = B0;
                dr.Bx = bx;
                dr.By = by;
                dr.R = R;
            }

            public (double Range, double X0, double Y0, double H1, double H2, double Theta, double Eta, double A, double B0, double Bx, double By, double R)
                ConvertPrmsToOriginalValues(double range, double x0, double y0, double h1, double h2, double theta, double eta, double a, double b0, double bx, double by, double r)
                => (range, x0, y0, h1, h2, theta / 180.0 * Math.PI, eta, a, b0 - bx * x0 - by * y0, bx, by, r);


            /// <summary>
            /// d値の値を再計算する
            /// </summary>
            public void ResetDspacing()
            {
                if (AreaDetector == null || DirectSpotPosition.IsNaN)
                    return;
                AreaDetector.Center = DirectSpotPosition;
                for (int i = 0; i < Rows.Count; i++)
                    Rows[i]["d"] = 1 / AreaDetector.convertClientToReciprocalSpace((double)Rows[i]["x0"], (double)Rows[i]["y0"]).Length;
            }

            /// <summary>
            /// ダイレクトスポットを、引数の番号indexで指定する。d値の再計算も行われる
            /// </summary>
            /// <param name="index"></param>
            public void SetDirectNo(int index)
            {
                if (index >= 0 && index < Rows.Count)
                {
                    for (int i = 0; i < Rows.Count; i++)
                        Rows[i]["Direct"] = i == index;
                    ResetDspacing();
                }
            }

            /// <summary>
            ///
            /// </summary>
            private void resetNumber()
            {
                for (int i = 0; i < this.Rows.Count; i++)
                    this.Rows[i]["No"] = i;
            }

            public void setHKL(int index, string hkl)
            {
                if (index < 0 || index > Rows.Count)
                    return;
                var r = (DataTableSpotRow)Rows[index];
                r.HKL = hkl;
            }


        }
    }
}