using System;

namespace Crystallography
{
    [Serializable()]
    /// <summary>
    /// Seitz表記によるSymmetry operationを表現するクラス
    /// </summary>
    public class SymmetryOperation
    {
        public int SeriesNumber { get; set; }

        /// <summary>
        /// 回転の次数. 1, 2, 3, 4, 6, -1, -2(=m), -3, -4, -6のいずれか
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 回転の方向. +1か-1のいずれか
        /// </summary>
        public int Sense { get; set; }

        /// <summary>
        /// 回転の軸.
        /// </summary>
        public (int U, int V, int W) Direction { get; set; }

        /// <summary>
        /// 並進ベクトル
        /// </summary>
        public (double U, double V, double W) Translation { get; set; }

        public SymmetryOperation(int seriesNumber, int order, int sense, (int U, int V, int W) direction, (double U, double V, double W) translation)
            : this(order, sense, direction, translation)
        {
            SeriesNumber = seriesNumber;
        }

        public SymmetryOperation(int order, int sense, (int U, int V, int W) direction, (double U, double V, double W) translation)
        {
            Order = order;
            Sense = sense;
            Direction = direction;
            Translation = translation;
        }

        public SymmetryOperation(SymmetryOperation so, double shiftU, double shiftV, double shiftW)
        {
            SeriesNumber = so.SeriesNumber;
            Order = so.Order;
            Sense = so.Sense;
            Direction = so.Direction;
            Translation = (so.Translation.U + shiftU, so.Translation.V + shiftV, so.Translation.W + shiftW);
        }

        public (int H, int K, int L) ConvertPlaneIndex((int H, int K, int L) index) => ConvertPlaneIndex(index.H, index.K, index.L);

        public (int H, int K, int L) ConvertPlaneIndex(int h, int k, int l)
        {
            if (Order == 1)
                return (h, k, l);
            else if (Order == -1)
                return (-h, -k, -l);
            else
            {
                (int H, int K, int L) p = (int.MaxValue, int.MaxValue, int.MaxValue);

                if (SymmetryStatic.NumArray[SeriesNumber][5] == 5 || SymmetryStatic.NumArray[SeriesNumber][5] == 6)
                {//trigonalかHexagonalの場合
                    if (SymmetryStatic.NumArray[SeriesNumber][2] == 1)
                    {//Hexaセッティングの時
                        if (Order == 2 || Order == -2)
                        {
                            if (Direction == (0, 0, 1)) p = (-h, -k, l);
                            else if (Direction == (1, 0, 0)) p = (h, -h - k, -l);
                            else if (Direction == (0, 1, 0)) p = (-h - k, k, -l);
                            else if (Direction == (1, 1, 0)) p = (k, h, -l);
                            else if (Direction == (1, -1, 0)) p = (-k, -h, -l);
                            else if (Direction == (2, 1, 0)) p = (-h, h + k, -l);
                            else if (Direction == (1, 2, 0)) p = (h + k, -k, -l);
                        }
                        else if (Order == 3 || Order == -3)
                        {
                            if (Direction == (0, 0, 1) && Sense == +1) p = (k, -h - k, l);
                            else if (Direction == (0, 0, 1) && Sense == -1) p = (-h - k, h, l);
                        }
                        else if (Order == 6 || Order == -6)
                        {
                            if (Direction == (0, 0, 1) && Sense == +1) p = (h + k, -h, l);
                            else if (Direction == (0, 0, 1) && Sense == -1) p = (-k, h + k, l);
                        }
                    }
                    else
                    {//Rhomboセッティングの時
                        if (Order == 2 || Order == -2)
                        {
                            if (Direction == (0, 1, -1)) p = (-h, -l, -k);
                            else if (Direction == (-1, 0, 1)) p = (-l, -k, -h);
                            else if (Direction == (1, -1, 0)) p = (-k, -h, -l);
                        }
                        else if (Order == 3 || Order == -3)
                        {
                            if (Direction == (1, 1, 1) && Sense == +1) p = (k, l, h);
                            else if (Direction == (1, 1, 1) && Sense == -1) p = (l, h, k);
                        }
                    }
                }
                else
                {//trigonalでもHexagonalでもない場合
                    if (Order == 2 || Order == -2)
                    {
                        if (Direction == (1, 0, 0)) p = (h, -k, -l);
                        else if (Direction == (0, 1, 0)) p = (-h, k, -l);
                        else if (Direction == (0, 0, 1)) p = (-h, -k, l);
                        else if (Direction == (0, 1, 1)) p = (-h, l, k);
                        else if (Direction == (1, 0, 1)) p = (l, -k, h);
                        else if (Direction == (1, 1, 0)) p = (k, h, -l);
                        else if (Direction == (0, 1, -1)) p = (-h, -l, -k);
                        else if (Direction == (-1, 0, 1)) p = (-l, -k, -h);
                        else if (Direction == (1, -1, 0)) p = (-k, -h, -l);
                    }
                    else if (Order == 3 || Order == -3)
                    {
                        if (Direction == (+1, +1, +1) && Sense == +1) p = (+k, +l, +h);
                        else if (Direction == (+1, +1, +1) && Sense == -1) p = (+l, +h, +k);
                        else if (Direction == (+1, -1, -1) && Sense == +1) p = (-k, +l, -h);
                        else if (Direction == (+1, -1, -1) && Sense == -1) p = (-l, -h, +k);
                        else if (Direction == (-1, +1, -1) && Sense == +1) p = (-k, -l, +h);
                        else if (Direction == (-1, +1, -1) && Sense == -1) p = (+l, -h, -k);
                        else if (Direction == (-1, -1, +1) && Sense == +1) p = (+k, -l, -h);
                        else if (Direction == (-1, -1, +1) && Sense == -1) p = (-l, +h, -k);
                    }
                    else if (Order == 4 || Order == -4)
                    {
                        if (Direction == (1, 0, 0) && Sense == +1) p = (h, l, -k);
                        else if (Direction == (1, 0, 0) && Sense == -1) p = (h, -l, k);
                        else if (Direction == (0, 1, 0) && Sense == +1) p = (-l, k, h);
                        else if (Direction == (0, 1, 0) && Sense == -1) p = (l, k, -h);
                        else if (Direction == (0, 0, 1) && Sense == +1) p = (k, -h, l);
                        else if (Direction == (0, 0, 1) && Sense == -1) p = (-k, h, l);
                    }
                }
                return Order > 0 ? p : (-p.H, -p.K, -p.L);
            }
        }
    }
}