using System;

namespace Crystallography;

[Serializable()]
/// <summary>
/// Seitz表記によるSymmetry operationを表現するクラス SymmatryStaticで初期化される
/// </summary>
public readonly struct SymmetryOperation
{
    public ushort SeriesNumber { get;}

    /// <summary>
    /// 回転の次数. 1, 2, 3, 4, 6, -1, -2(=m), -3, -4, -6のいずれか
    /// </summary>
    public short Order { get;}

    /// <summary>
    /// 回転の方向. trueは +1か falseは -1のいずれか
    /// </summary>
    public bool Sense { get;}

    /// <summary>
    /// 回転の軸.
    /// </summary>
    public (int U, int V, int W) Direction {get;}

    /// <summary>
    /// 並進ベクトル
    /// </summary>
    public (double U, double V, double W) Translation { get; }

    public SymmetryOperation(int order, int sense,in (int U, int V, int W) direction,in (double U, double V, double W) translation)
    {
        SeriesNumber = 0;
        Order = (short)order;
        Sense = sense == 1;
        Direction = direction;
        Translation = translation;
    }

    public SymmetryOperation(SymmetryOperation so, int seriesNumber, double shiftU, double shiftV, double shiftW)
    {
        SeriesNumber = (ushort)seriesNumber;
        Order = so.Order;
        Sense = so.Sense;
        Direction = so.Direction;
        Translation = (so.Translation.U + shiftU, so.Translation.V + shiftV, so.Translation.W + shiftW);
    }

    public SymmetryOperation(SymmetryOperation so, int seriesNumber)
    {
        SeriesNumber = (ushort)seriesNumber;
        Order = so.Order;
        Sense = so.Sense;
        Direction = so.Direction;
        Translation = so.Translation;
    }

    public (int H, int K, int L) ConvertPlaneIndex((int H, int K, int L) index) 
        => ConvertPlaneIndex(index.H, index.K, index.L);

    public (int H, int K, int L) ConvertPlaneIndex(int h, int k, int l)
    {
        if (Order == 1)
            return (h, k, l);
        else if (Order == -1)
            return (-h, -k, -l);
        else
        {
            (int H, int K, int L) p = (0, 0, 0);

            if (SymmetryStatic.NumArray[SeriesNumber][5] == 5 || SymmetryStatic.NumArray[SeriesNumber][5] == 6)
            {//trigonalかHexagonalの場合
                if (SymmetryStatic.NumArray[SeriesNumber][2] == 1)
                {//Hexaセッティングの時
                    if (Math.Abs(Order) == 2)
                    {
                        p = Direction switch
                        {
                            (0, 0, 1) => (-h, -k, l),
                            (1, 0, 0) => (h, -h - k, -l),
                            (0, 1, 0) => (-h - k, k, -l),
                            (1, 1, 0) => (k, h, -l),
                            (1, -1, 0) => (-k, -h, -l),
                            (2, 1, 0) => (-h, h + k, -l),
                            (1, 2, 0) => (h + k, -k, -l),
                            _ => (0, 0, 0)
                        };
                    }
                    else if (Math.Abs(Order) == 3)
                    {
                        if (Direction == (0, 0, 1))
                            p = Sense ? (k, -h - k, l) : (-h - k, h, l);
                    }
                    else if (Math.Abs(Order) == 6)
                    {
                        if (Direction == (0, 0, 1))
                            p = Sense ? (h + k, -h, l) : (-k, h + k, l);
                    }
                }
                else
                {//Rhomboセッティングの時
                    if (Math.Abs(Order) == 2)
                    {
                        p = Direction switch
                        {
                            (0, 1, -1) => (-h, -l, -k),
                            (-1, 0, 1) => (-l, -k, -h),
                            (1, -1, 0) => (-k, -h, -l),
                            _ => (0, 0, 0)
                        };
                    }
                    else if (Math.Abs(Order) == 3)
                    {
                        if (Direction == (1, 1, 1))
                            p = Sense ? (k, l, h) : (l, h, k);
                    }
                }
            }
            else
            {//trigonalでもHexagonalでもない場合
                if (Math.Abs(Order) == 2)
                {
                    p = Direction switch
                    {
                        (1, 0, 0) => (h, -k, -l),
                        (0, 1, 0) => (-h, k, -l),
                        (0, 0, 1) => (-h, -k, l),
                        (0, 1, 1) => (-h, l, k),
                        (1, 0, 1) => (l, -k, h),
                        (1, 1, 0) => (k, h, -l),
                        (0, 1, -1) => (-h, -l, -k),
                        (-1, 0, 1) => (-l, -k, -h),
                        (1, -1, 0) => (-k, -h, -l),
                        _ => (0, 0, 0)
                    };
                }
                else if (Math.Abs(Order) == 3)
                {
                    p = Direction switch
                    {
                        (+1, +1, +1) => Sense ? (+k, +l, +h) : (+l, +h, +k),
                        (+1, -1, -1) => Sense ? (-k, +l, -h) : (-l, -h, +k),
                        (-1, +1, -1) => Sense ? (-k, -l, +h) : (+l, -h, -k),
                        (-1, -1, +1) => Sense ? (+k, -l, -h) : (-l, +h, -k),
                        _ => (0, 0, 0)
                    };
                }
                else if (Math.Abs(Order) == 4)
                {
                    p = Direction switch
                    {
                        (1, 0, 0) => Sense ? (h, l, -k) : (h, -l, k),
                        (0, 1, 0) => Sense ? (-l, k, h) : (l, k, -h),
                        (0, 0, 1) => Sense ? (k, -h, l) : (-k, h, l),
                        _ => (0, 0, 0)
                    };
                }
            }
            return Order > 0 ? p : (-p.H, -p.K, -p.L);
        }
    }
}