using System;

namespace Crystallography;

[Serializable()]
/// <summary>Seitz表記によるSymmetry operationを表現するクラス SymmatryStaticで初期化される</summary>
public readonly struct SymmetryOperation
{
    #region フィールド、プロパティ
    public ushort SeriesNumber { get; }

    /// <summary>回転・回反の次数. 1, 2, 3, 4, 6, -1, -2(=m), -3, -4, -6のいずれか</summary>
    public short Order { get; }

    /// <summary>回転・回反の方向. trueは +1か falseは -1のいずれか</summary>
    public bool Sense { get; }

    /// <summary>回転・回反の軸.</summary>
    public (int U, int V, int W) Direction { get; }

    /// <summary>単位胞内での対称要素の代表点。軸上点/面上点/対称心の不動点を表す。Order==1 (恒等) では未使用。</summary>
    public (double U, double V, double W) Position { get; }

    /// <summary>内在並進ベクトル。螺旋軸と映進面の並進成分を保持する。回転・鏡面・対称心では (0,0,0)。</summary>
    public (double U, double V, double W) IntrinsicTranslation { get; }

    /// <summary>対称操作 (R, t)(x,y,z) = R·(x,y,z) + t における並進 t ベクトル。
    /// (I − R)·Position + IntrinsicTranslation で計算される。
    /// ここで R は (Order, Sense, Direction) で定まる 3×3 直接空間の対称操作行列 (回転または回反、<see cref="ApplyMatrix"/> が実装)、
    /// I は 3×3 単位行列。Position/IntrinsicTranslation が未設定のときは (0,0,0) を返す。</summary>
    public (double U, double V, double W) SeitzTranslation
    {
        get
        {
            var (rx, ry, rz) = ApplyMatrix(Position.U, Position.V, Position.W);
            return (Position.U - rx + IntrinsicTranslation.U,
                    Position.V - ry + IntrinsicTranslation.V,
                    Position.W - rz + IntrinsicTranslation.W);
        }
    }

    #endregion

    #region コンストラクタ
    /// <summary>(Order, Sense, Direction, Position, IntrinsicTranslation) を直接指定するコンストラクタ。
    /// SeriesNumber は 0 (=未指定) で初期化される。OperationList の static literals および
    /// Phase 3 のデータ生成スクリプトが使用する。</summary>
    public SymmetryOperation(int order, int sense, in (int U, int V, int W) direction,
                             in (double U, double V, double W) position,
                             in (double U, double V, double W) intrinsicTranslation)
    {
        SeriesNumber = 0;
        Order = (short)order;
        Sense = sense == 1;
        Direction = direction;
        Position = position;
        IntrinsicTranslation = intrinsicTranslation;
    }

    /// <summary>中心化シフトを伴うコピー。SeitzTranslation を T だけ加算するために Position をシフトするが、
    /// 軸/鏡面の平行成分は本来 IntrinsicTranslation に振り分ける必要があり、現状は近似実装
    /// (`SeitzTranslation` の派生値が中心化された軸については T 倍ずれる可能性)。SymmetryStatic の
    /// 中心化展開でのみ使用され、外部から SeitzTranslation を読む caller がない間は影響なし。</summary>
    public SymmetryOperation(SymmetryOperation so, int seriesNumber, double shiftU, double shiftV, double shiftW)
    {
        SeriesNumber = (ushort)seriesNumber;
        Order = so.Order;
        Sense = so.Sense;
        Direction = so.Direction;
        Position = (so.Position.U + shiftU, so.Position.V + shiftV, so.Position.W + shiftW);
        IntrinsicTranslation = so.IntrinsicTranslation;
    }

    public SymmetryOperation(SymmetryOperation so, int seriesNumber)
    {
        SeriesNumber = (ushort)seriesNumber;
        Order = so.Order;
        Sense = so.Sense;
        Direction = so.Direction;
        Position = so.Position;
        IntrinsicTranslation = so.IntrinsicTranslation;
    }

    #endregion

    #region ApplyMatrix (直接空間の対称操作の線形部適用)
    /// <summary>260429Cl: 対称操作の線形部行列 R を直接空間ベクトルに適用して R·(x,y,z) を返す。
    /// 回転と回反 (-2=mirror, -3, -4, -6) の両方を扱う (回反は R(-n) = -R(+n) で実装)。
    /// trigonal/hex の hex 設定は非直交基底行列、それ以外 (cubic/tetragonal/orthorhombic および rho 設定) は直交基底。
    /// 注意: <see cref="ConvertPlaneIndex(int, int, int)"/> は逆格子用 R^{-T} なので 3/4/6 回回転で行列が異なる。</summary>
    public (double X, double Y, double Z) ApplyMatrix(double x, double y, double z)
    {
        if (Order == 1) return (x, y, z);
        if (Order == -1) return (-x, -y, -z);

        bool isHex = SeriesNumber > 0 && SymmetryStatic.IsHexBySeries[SeriesNumber];
        int o = Math.Abs(Order), u = Direction.U, v = Direction.V, w = Direction.W;
        (double X, double Y, double Z) p;

        if (!isHex && o == 2)
        {   // 2 回回転は R = 2 n n^T / |n|² − I の代数式で 9 方向を一括処理
            double s = 2.0 / (u * u + v * v + w * w), d = u * x + v * y + w * z;
            p = (s * u * d - x, s * v * d - y, s * w * d - z);
        }
        else if (!isHex && o == 3)
            // 立方系 {111} 系 3 回回転: uvw=+1 が正準なので符号積で 4 方向を一括処理
            p = Sense ? (u * w * z, u * v * x, v * w * y) : (u * v * y, v * w * z, u * w * x);
        else if (!isHex && o == 4)
            p = (u, v, w) switch
            {
                (1, 0, 0) => Sense ? (x, -z, y) : (x, z, -y),
                (0, 1, 0) => Sense ? (z, y, -x) : (-z, y, x),
                (0, 0, 1) => Sense ? (-y, x, z) : (y, -x, z),
                _ => (0.0, 0.0, 0.0)
            };
        else if (isHex && o == 2)
            p = (u, v, w) switch
            {
                (0, 0, 1) => (-x, -y, z),
                (1, 0, 0) => (x - y, -y, -z),
                (0, 1, 0) => (-x, -x + y, -z),
                (1, 1, 0) => (y, x, -z),
                (1, -1, 0) => (-y, -x, -z),
                (2, 1, 0) => (x, x - y, -z),
                (1, 2, 0) => (-x + y, y, -z),
                _ => (0.0, 0.0, 0.0)
            };
        else if (isHex && o == 3 && (u, v, w) == (0, 0, 1))
            p = Sense ? (-y, x - y, z) : (-x + y, -x, z);
        else if (isHex && o == 6 && (u, v, w) == (0, 0, 1))
            p = Sense ? (x - y, x, z) : (y, -x + y, z);
        else
            p = (0, 0, 0);

        return Order > 0 ? p : (-p.X, -p.Y, -p.Z);
    }

    /// <summary>(260502Cl) 線形部 R を Vector3DBase に作用させる: R · v を返す。</summary>
    public Vector3DBase ApplyMatrix(Vector3DBase v)
    {
        var (x, y, z) = ApplyMatrix(v.X, v.Y, v.Z);
        return new Vector3DBase(x, y, z);
    }

    /// <summary>(260502Cl) Seitz 操作 (R, t) を作用させる: R·v + SeitzTranslation を返す。</summary>
    public Vector3DBase ApplyAffine(Vector3DBase v)
    {
        var (x, y, z) = ApplyMatrix(v.X, v.Y, v.Z);
        var t = SeitzTranslation;
        return new Vector3DBase(x + t.U, y + t.V, z + t.W);
    }
    #endregion

    #region ConvertPlaneIndex
    public (int H, int K, int L) ConvertPlaneIndex((int H, int K, int L) index)
        => ConvertPlaneIndex(index.H, index.K, index.L);



    public (int H, int K, int L) ConvertPlaneIndex(int h, int k, int l)
    {
        if (Order == 1) return (h, k, l);
        if (Order == -1) return (-h, -k, -l);

        bool isHex = SymmetryStatic.IsHexBySeries[SeriesNumber];
        bool isTrigOrHex = SymmetryStatic.NumArray[SeriesNumber][5] == 5 || SymmetryStatic.NumArray[SeriesNumber][5] == 6;
        int o = Math.Abs(Order);
        (int H, int K, int L) p = (0, 0, 0);

        if (isHex)
        {
            if (o == 2)
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
            else if (o == 3 && Direction == (0, 0, 1))
                p = Sense ? (k, -h - k, l) : (-h - k, h, l);
            else if (o == 6 && Direction == (0, 0, 1))
                p = Sense ? (h + k, -h, l) : (-k, h + k, l);
        }
        else if (isTrigOrHex)
        {//Rhomboセッティング
            if (o == 2)
                p = Direction switch
                {
                    (0, 1, -1) => (-h, -l, -k),
                    (-1, 0, 1) => (-l, -k, -h),
                    (1, -1, 0) => (-k, -h, -l),
                    _ => (0, 0, 0)
                };
            else if (o == 3 && Direction == (1, 1, 1))
                p = Sense ? (k, l, h) : (l, h, k);
        }
        else
        {//trigonal でも hex でもない場合
            if (o == 2)
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
            else if (o == 3)
                p = Direction switch
                {
                    (+1, +1, +1) => Sense ? (+k, +l, +h) : (+l, +h, +k),
                    (+1, -1, -1) => Sense ? (-k, +l, -h) : (-l, -h, +k),
                    (-1, +1, -1) => Sense ? (-k, -l, +h) : (+l, -h, -k),
                    (-1, -1, +1) => Sense ? (+k, -l, -h) : (-l, +h, -k),
                    _ => (0, 0, 0)
                };
            else if (o == 4)
                p = Direction switch
                {
                    (1, 0, 0) => Sense ? (h, l, -k) : (h, -l, k),
                    (0, 1, 0) => Sense ? (-l, k, h) : (l, k, -h),
                    (0, 0, 1) => Sense ? (k, -h, l) : (-k, h, l),
                    _ => (0, 0, 0)
                };
        }
        return Order > 0 ? p : (-p.H, -p.K, -p.L);
    }

    #region h,k,lを整数から実数に拡張したテストコード
    public (double H, double K, double L) ConvertPlaneIndex((double H, double K, double L) index)
      => ConvertPlaneIndex(index.H, index.K, index.L);

    public (double H, double K, double L) ConvertPlaneIndex(double h, double k, double l)
    {
        if (Order == 1) return (h, k, l);
        if (Order == -1) return (-h, -k, -l);

        bool isHex = SymmetryStatic.IsHexBySeries[SeriesNumber];
        bool isTrigOrHex = SymmetryStatic.NumArray[SeriesNumber][5] == 5 || SymmetryStatic.NumArray[SeriesNumber][5] == 6;
        int o = Math.Abs(Order);
        (double H, double K, double L) p = (0, 0, 0);

        if (isHex)
        {
            if (o == 2)
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
            else if (o == 3 && Direction == (0, 0, 1))
                p = Sense ? (k, -h - k, l) : (-h - k, h, l);
            else if (o == 6 && Direction == (0, 0, 1))
                p = Sense ? (h + k, -h, l) : (-k, h + k, l);
        }
        else if (isTrigOrHex)
        {//Rhomboセッティング
            if (o == 2)
                p = Direction switch
                {
                    (0, 1, -1) => (-h, -l, -k),
                    (-1, 0, 1) => (-l, -k, -h),
                    (1, -1, 0) => (-k, -h, -l),
                    _ => (0, 0, 0)
                };
            else if (o == 3 && Direction == (1, 1, 1))
                p = Sense ? (k, l, h) : (l, h, k);
        }
        else
        {//trigonal でも hex でもない場合
            if (o == 2)
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
            else if (o == 3)
                p = Direction switch
                {
                    (+1, +1, +1) => Sense ? (+k, +l, +h) : (+l, +h, +k),
                    (+1, -1, -1) => Sense ? (-k, +l, -h) : (-l, -h, +k),
                    (-1, +1, -1) => Sense ? (-k, -l, +h) : (+l, -h, -k),
                    (-1, -1, +1) => Sense ? (+k, -l, -h) : (-l, +h, -k),
                    _ => (0, 0, 0)
                };
            else if (o == 4)
                p = Direction switch
                {
                    (1, 0, 0) => Sense ? (h, l, -k) : (h, -l, k),
                    (0, 1, 0) => Sense ? (-l, k, h) : (l, k, -h),
                    (0, 0, 1) => Sense ? (k, -h, l) : (-k, h, l),
                    _ => (0, 0, 0)
                };
        }
        return Order > 0 ? p : (-p.H, -p.K, -p.L);
    }
    #endregion

    #endregion
}
