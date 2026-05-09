// 260502Cl: 空間群の対称要素 (反転中心 / 回転螺旋軸 / 鏡映映進面) を事前計算してキャッシュするテーブル。
// SymmetryStatic.WyckoffPositions と PositionOperations から空間群番号 1 つにつき 1 度だけ計算し、
// 結晶座標系 (a, b, c) のままの fractional 座標で全要素を保持する。射影方向には非依存。
// 利用側 (描画コード等) は射影方向ごとに perpendicular / in-plane を分類して用いる。
using System;
using System.Collections.Generic;
using System.Linq;
using Vec = Crystallography.Vector3DBase;
using Mat = Crystallography.Matrix3D;

namespace Crystallography;

/// <summary>反転中心の結晶座標 ([0, 1) に正規化済み)。</summary>
public readonly record struct InversionCenter(double X, double Y, double Z);

/// <summary>回転 / 螺旋 / 回反 軸の単位。
/// Position は軸が通る代表点、Direction は軸方向の (uvw) 整数指数。
/// Order は ±2, ±3, ±4, ±6 (負号は roto-inversion。ただし Order = -1 は反転中心、 Order = -2 は鏡映で別カテゴリ)。
/// IntrinsicTranslation は軸方向の screw 並進成分 (fractional)。
/// FinCount / EdgeStep は ITC 規約の pinwheel 描画用 (FinCount = 0 は螺旋なし)。</summary>
public readonly record struct SymmetryAxis(
    double X, double Y, double Z,
    (int U, int V, int W) Direction,
    int Order,
    bool Screw,
    int FinCount, int EdgeStep,
    (double U, double V, double W) IntrinsicTranslation);

/// <summary>鏡映 / 映進 面の単位。
/// Position は面上の代表点、Normal は面法線方向の (uvw) 整数指数。
/// Glide は面内 glide 並進成分 (fractional)。零ベクトルなら純鏡映 m。</summary>
public readonly record struct SymmetryPlane(
    double X, double Y, double Z,
    (int U, int V, int W) Normal,
    (double U, double V, double W) Glide);

/// <summary>空間群 1 つの対称要素を全列挙して格納するテーブル。
/// SymmetryElementsTable.Get(seriesNumber) で取得。空間群ごとに 1 度だけ計算し以降はキャッシュから返す。
/// 三方晶系の Rho 設定は同一空間群の Hex 設定に正規化される。</summary>
public sealed class SymmetryElementsTable
{
    /// <summary>正規化後の seriesNumber (Rho → Hex リダイレクト後の値)。</summary>
    public int SeriesNumber { get; }
    public InversionCenter[] InversionCenters { get; }
    public SymmetryAxis[] SymmetryAxes { get; }
    public SymmetryPlane[] SymmetryPlanes { get; }
    /// <summary>(260504Ch) この空間群の centering 並進ベクトル一覧 (整数並進 (0,0,0) を除く)。
    /// 例: F-centering なら (0,1/2,1/2), (1/2,0,1/2), (1/2,1/2,0)。
    /// 軸方向の primitive 並進長や centered cell の mirror/glide 展開に使う。</summary>
    public (double U, double V, double W)[] Centerings { get; }

    private SymmetryElementsTable(int seriesNumber, InversionCenter[] inv, SymmetryAxis[] rot, SymmetryPlane[] mir,
                                  (double U, double V, double W)[] centerings)
    {
        SeriesNumber = seriesNumber;
        InversionCenters = inv;
        SymmetryAxes = rot;
        SymmetryPlanes = mir;
        Centerings = centerings;
    }

    private static readonly Dictionary<int, SymmetryElementsTable> _cache = [];
    private static readonly object _lock = new();

    /// <summary>seriesNumber に対応する事前計算テーブルを取得。
    /// 三方晶系 Rho 設定は同一空間群の Hex 設定にリダイレクトされる。
    /// 範囲外もしくは PositionOperations が null の場合は null を返す。</summary>
    public static SymmetryElementsTable Get(int seriesNumber)
    {
        int resolved = ResolveRhoToHex(seriesNumber);
        lock (_lock)
        {
            if (_cache.TryGetValue(resolved, out var t)) return t;
            t = Compute(resolved);
            if (t != null) _cache[resolved] = t;
            return t;
        }
    }

    /// <summary>三方晶系の Rho 設定を、同一空間群番号の Hex 設定 series number にマップする。
    /// 該当しない場合は元の seriesNumber をそのまま返す。</summary>
    public static int ResolveRhoToHex(int seriesNumber)
    {
        if (seriesNumber <= 0 || seriesNumber >= SymmetryStatic.TotalSpaceGroupNumber) return seriesNumber;
        var sym = SymmetryStatic.Symmetries[seriesNumber];
        if (sym.SpaceGroupHMStr is not { } hm || !hm.EndsWith("Rho", StringComparison.Ordinal)) return seriesNumber;
        for (int i = 1; i < SymmetryStatic.TotalSpaceGroupNumber; i++)
        {
            var alt = SymmetryStatic.Symmetries[i];
            if (alt.SpaceGroupNumber == sym.SpaceGroupNumber &&
                alt.SpaceGroupHMStr is { } s && s.EndsWith("Hex", StringComparison.Ordinal))
                return i;
        }
        return seriesNumber;
    }

    private static SymmetryElementsTable Compute(int seriesNumber)
    {
        if (seriesNumber <= 0 || seriesNumber >= SymmetryStatic.TotalSpaceGroupNumber) return null;
        var baseOps = SymmetryStatic.WyckoffPositions[seriesNumber][0].PositionOperations;
        if (baseOps == null) return null;
        var ops = ExpandWithCentering(baseOps, seriesNumber);
        // (260503Cl) 中心化ベクトルは恒等 op の IntrinsicTranslation から取得 (constructor 改訂に追従)。
        var centerings = ops
            .Where(o => o.Order == 1 && Math.Abs(o.IntrinsicTranslation.U) + Math.Abs(o.IntrinsicTranslation.V) + Math.Abs(o.IntrinsicTranslation.W) > 1e-6)
            .Select(o => (o.IntrinsicTranslation.U, o.IntrinsicTranslation.V, o.IntrinsicTranslation.W))
            .Distinct()
            .ToList();

        return new SymmetryElementsTable(
            seriesNumber,
            CollectInversions(seriesNumber),
            CollectSymmetryAxes(ops, centerings),
            CollectSymmetryPlanes(ops, centerings),
            [.. centerings]);
    }

    #region 反転中心
    /// <summary>(260502Cl) Wyckoff position の site symmetry が中心対称で全 Free=false の WP を反転中心として収集。
    /// `GeneratePositions` で対称軌道全体を展開し、unit cell [0, 1)³ 内に正規化した上で重複除去する。</summary>
    private static InversionCenter[] CollectInversions(int seriesNumber)
    {
        var list = new List<InversionCenter>();
        var seen = new HashSet<(long, long, long)>();
        foreach (var wp in SymmetryStatic.WyckoffPositions[seriesNumber])
        {
            if (!HasInversionCenter(wp.SiteSymmetry)) continue;
            if (wp.Free.X || wp.Free.Y || wp.Free.Z) continue;
            foreach (var p in wp.GeneratePositions(0, 0, 0))
            {
                double x = Mod1(p.X), y = Mod1(p.Y), z = Mod1(p.Z);
                if (seen.Add((R6(x), R6(y), R6(z)))) list.Add(new InversionCenter(x, y, z));
            }
        }
        return [.. list];
    }

    /// <summary>反転中心を含む 11 個の中心対称結晶学点群 (Laue クラス) のいずれかかを判定。
    /// site symmetry の "." (ドット) は向き指定なので除いてから比較する。</summary>
    private static bool HasInversionCenter(string siteSymmetry)
        => !string.IsNullOrEmpty(siteSymmetry) && siteSymmetry.Replace(".", "") is
            "-1" or "2/m" or "mmm" or "4/m" or "4/mmm" or "-3" or "-3m" or "6/m" or "6/mmm" or "m-3" or "m-3m";
    #endregion

    #region 回転 / 螺旋 / 回反 軸
    private static SymmetryAxis[] CollectSymmetryAxes(SymmetryOperation[] ops,
        IReadOnlyList<(double U, double V, double W)> centerings)
    {
        var list = new List<SymmetryAxis>();
        var seen = new HashSet<(int, int, int, int, long, long, long, long, long, long)>();
        foreach (var op in ops)
        {
            int o = op.Order, absO = Math.Abs(o);
            if (o == -2) continue; // 鏡映は SymmetryPlane で扱う
            if (absO is not (2 or 3 or 4 or 6)) continue; // skip identity (1)、反転 (-1)、その他
            if (!op.Sense && (absO is 3 or 4 or 6)) continue; // 高次回転の逆冪は同じ軸なので skip
            foreach (var axis in EnumerateAxisInstances(op, centerings))
            {
                var key = (o, axis.Direction.U, axis.Direction.V, axis.Direction.W,
                    R6(axis.X), R6(axis.Y), R6(axis.Z),
                    R6(axis.IntrinsicTranslation.U), R6(axis.IntrinsicTranslation.V), R6(axis.IntrinsicTranslation.W));
                if (!seen.Add(key)) continue;
                list.Add(new SymmetryAxis(axis.X, axis.Y, axis.Z, axis.Direction, o,
                    axis.Screw, axis.FinCount, axis.EdgeStep, axis.IntrinsicTranslation));
            }
        }
        return [.. list];
    }

    /// <summary>op の幾何位置を格子同値性 ((lx,ly,lz) ∈ {0,1}³) から全列挙し、各並進ごとに screw 成分を保持する。
    /// (260503Cl) centerings を渡すことで、ScrewParams が中心化込みの primitive_along_d を使った k 判定を行う。</summary>
    private static IEnumerable<AxisInstance> EnumerateAxisInstances(SymmetryOperation op,
        IReadOnlyList<(double U, double V, double W)> centerings)
    {
        var seen = new HashSet<(long, long, long, long, long, long, bool)>();
        var (px, py, pz) = op.Position;
        var aMat = BuildIMinusR(op);
        bool useDecomp = aMat.Rank() == 2 && op.Order > 0;
        for (int tx = 0; tx <= 1; tx++) for (int ty = 0; ty <= 1; ty++) for (int tz = 0; tz <= 1; tz++)
        {
            Vec lattice = new(tx, ty, tz);
            Vec shift; (double U, double V, double W) rawIt;
            if (useDecomp)
            {
                if (!TryDecomposeAxisLatticeTranslation(op, lattice, out shift, out var axial)) continue;
                rawIt = (op.IntrinsicTranslation.U + axial.X, op.IntrinsicTranslation.V + axial.Y, op.IntrinsicTranslation.W + axial.Z);
            }
            else
            {
                if (!TrySolveLinear(aMat, lattice, out shift)) continue;
                rawIt = op.IntrinsicTranslation;
            }
            // (260503Cl) k=0 (= 格子周期で pure rotation 同等) のときは IT を (0,0,0) に正規化し、
            //   base 純回転と seen-key を一致させて重複排除する。
            var (fin, edge) = ScrewParams(op, rawIt, centerings);
            bool screw = fin > 0;
            (double U, double V, double W) it = screw
                ? (CenterMod1(rawIt.U), CenterMod1(rawIt.V), CenterMod1(rawIt.W))
                : (0, 0, 0);
            var axis = new AxisInstance(Mod1(px + shift.X), Mod1(py + shift.Y), Mod1(pz + shift.Z),
                op.Direction, it, screw, fin, edge);
            var key = (R6(axis.X), R6(axis.Y), R6(axis.Z),
                R6(axis.IntrinsicTranslation.U), R6(axis.IntrinsicTranslation.V), R6(axis.IntrinsicTranslation.W), axis.Screw);
            if (seen.Add(key)) yield return axis;
        }
    }

    private readonly record struct AxisInstance(double X, double Y, double Z,
                                                (int U, int V, int W) Direction,
                                                (double U, double V, double W) IntrinsicTranslation,
                                                bool Screw, int FinCount, int EdgeStep);

    /// <summary>n_k 螺旋の (FinCount, EdgeStep)。gcd(N,k) > 1 (4_2/6_2/6_3/6_4) は ITC 規約に従い fin 数を減らした特例形に。
    /// EdgeStep は「k 値そのもの」ではなく「螺旋の旋回方向 (chirality) のマーカー」として機能する。260509Cl 追記:
    ///   - 非特例 (gcd=1) では (N, N-k) なので、k &lt; N/2 (右巻) で EdgeStep &gt; N/2、k &gt; N/2 (左巻) で EdgeStep &lt; N/2 となる。
    ///   - 特例 (gcd&gt;1, fin 数を減らした形) でも同じ旋回方向グループの代表値に揃える:
    ///     6_2 (右巻, 6_1 と同方向) → EdgeStep=5 (= 6_1 と同じ)。{(3,2) と書きたくなるが、それだと左巻きと誤分類される}
    ///     6_4 (左巻, 6_5 と同方向) → EdgeStep=1 (= 6_5 と同じ)
    ///     6_3 / 4_2 (k=N/2, 中立) → EdgeStep=1 (左巻側に寄せる慣例)
    /// SymmetryDiagram.cs の switch 式はこの (FinCount, EdgeStep) で Screw[N]_[k] テンプレートを一意に判別する。</summary>
    public static (int FinCount, int EdgeStep) PinwheelFins(int N, int k) => (N, k) switch
    {
        (_, 0) => (0, 0),
        (4, 2) => (2, 1),
        (6, 2) => (3, 5),
        (6, 3) => (2, 1),
        (6, 4) => (3, 1),
        _ => (N, N - k),
    };

    /// <summary>(260503Cl) op の intrinsic translation 軸方向成分から (FinCount, EdgeStep) を導出する。
    /// along は「軸方向ベクトル d 自身を 1 単位とした axial 比」。中心化格子では d 方向の最小格子並進
    /// (= primitive_along_d) が d 自身ではなく d/2 (例: I-cubic 体対角) や 1/3 d (例: 一部の rhombohedral)
    /// になりうるので、ITA 規約 axial = (k/N) · primitive_along_d に揃えるには、along を primitive_along_d で
    /// 割って primitive units に変換してから k を取る必要がある。</summary>
    private static (int FinCount, int EdgeStep) ScrewParams(SymmetryOperation op, (double U, double V, double W) it,
        IReadOnlyList<(double U, double V, double W)> centerings)
    {
        int N = Math.Abs(op.Order);
        if (N < 2) return (0, 0);
        if (!TryGetAxisFraction(op, it, out double along)) return (0, 0);
        double primitive = PrimitiveAlongDirectionInDUnits(op.Direction, centerings);
        if (primitive < 1e-9) return (0, 0);
        // along (in d-units) を primitive_along_d で割って primitive units に変換、mod 1 で正規化。
        double alongPrimitive = along / primitive;
        alongPrimitive -= Math.Floor(alongPrimitive);
        if (alongPrimitive < 1e-3 || alongPrimitive > 1 - 1e-3) return (0, 0);
        int k = ((int)Math.Round(alongPrimitive * N)) % N;
        if (k < 0) k += N;
        return PinwheelFins(N, k);
    }

    /// <summary>(260503Cl) 軸方向 d に沿った最小の格子並進ベクトル (purely-along-d) を d-units で返す。
    /// 中心化を含む格子の整数結合から「d と平行な最小ベクトル」を探索する。
    /// P-cubic [111] では 1、I-cubic 体対角では 1/2、F-cubic 体対角では 1。</summary>
    private static double PrimitiveAlongDirectionInDUnits((int U, int V, int W) direction,
        IReadOnlyList<(double U, double V, double W)> centerings)
    {
        if (centerings.Count == 0) return 1.0; // P-lattice: 最小並進は d 自身。
        Vec d = new(direction.U, direction.V, direction.W);
        double dd = d * d;
        if (dd < 1e-12) return 1.0;
        double bestT = double.PositiveInfinity;
        const int range = 2;
        int M = centerings.Count;
        var cs = new Vec[M];
        for (int i = 0; i < M; i++) cs[i] = new Vec(centerings[i].U, centerings[i].V, centerings[i].W);
        for (int nx = -range; nx <= range; nx++)
        for (int ny = -range; ny <= range; ny++)
        for (int nz = -range; nz <= range; nz++)
        for (int mc = 0; mc < (1 << M); mc++)
        {
            Vec v = new(nx, ny, nz);
            for (int b = 0; b < M; b++)
                if ((mc & (1 << b)) != 0) v += cs[b];
            // v が d と平行 (= 純粋に軸方向) であれば、その大きさを d-units で記録。
            double vd = v * d;
            Vec proj = (vd / dd) * d;
            Vec perp = v - proj;
            if (perp * perp > 1e-9) continue;
            double t = Math.Abs(vd / dd);
            if (t < 1e-6) continue;
            if (t < bestT) bestT = t;
        }
        return double.IsPositiveInfinity(bestT) ? 1.0 : bestT;
    }

    private static bool TryGetAxisFraction(SymmetryOperation op, (double U, double V, double W) it, out double along)
    {
        along = 0;
        var a = BuildIMinusR(op);
        if (a.Rank() != 2 || !TryFindAxisCovector(a, out var n)) return false;
        Vec d = op.Direction;
        double nd = n * d;
        if (Math.Abs(nd) < 1e-12) return false;
        along = Mod1(n * (Vec)it / nd);
        return true;
    }

    /// <summary>L を (I-R) による軸位置シフトと軸方向成分へ分解する。</summary>
    private static bool TryDecomposeAxisLatticeTranslation(SymmetryOperation op, Vec lattice, out Vec shift, out Vec axial)
    {
        shift = Vec.Zero; axial = Vec.Zero;
        var a = BuildIMinusR(op);
        if (a.Rank() != 2 || !TryFindAxisCovector(a, out var n)) return false;
        Vec d = op.Direction;
        double nd = n * d;
        if (Math.Abs(nd) < 1e-12) return false;
        double beta = (n * lattice) / nd;
        axial = beta * d;
        return TrySolveRankTwo(a, lattice - axial, out shift);
    }
    #endregion

    #region 鏡映 / 映進 面
    private static SymmetryPlane[] CollectSymmetryPlanes(SymmetryOperation[] ops, IReadOnlyList<(double U, double V, double W)> centerings)
    {
        var list = new List<SymmetryPlane>();
        var seen = new HashSet<(int, int, int, long, long, long, long, long, long)>();
        foreach (var op in ops)
        {
            if (op.Order != -2) continue;
            foreach (var pl in EnumerateSymmetryPlanes(op, centerings))
                foreach (var eq in EnumerateEquivalentSymmetryPlanes(pl, ops))
                {
                    var key = (eq.Direction.U, eq.Direction.V, eq.Direction.W,
                        R6(eq.Px), R6(eq.Py), R6(eq.Pz),
                        R6(eq.GlideU), R6(eq.GlideV), R6(eq.GlideW));
                    if (seen.Add(key))
                        list.Add(new SymmetryPlane(eq.Px, eq.Py, eq.Pz, eq.Direction,
                            (eq.GlideU, eq.GlideV, eq.GlideW)));
                }
        }
        return [.. list];
    }

    /// <summary>紙面垂直 mirror/glide の代表面。T = t + L を (I-R)·p + g に分解し、斜交基底でも glide 成分を保つ。</summary>
    private readonly record struct PlaneRep(double Px, double Py, double Pz, double GlideU, double GlideV, double GlideW,
                                            (int U, int V, int W) Direction);

    /// <summary>op の鏡映面を、(0,0,0) と centering 並進すべての lattice 等価系から列挙し、
    /// 各々で glide score を最小化した代表面を返す (R-centering 等で純 mirror / a-glide 表現を見つけるため)。</summary>
    private static IEnumerable<PlaneRep> EnumerateSymmetryPlanes(SymmetryOperation op, IReadOnlyList<(double U, double V, double W)> centerings)
    {
        var R = new Mat(op.ApplyMatrix(new Vec(1, 0, 0)),
                        op.ApplyMatrix(new Vec(0, 1, 0)),
                        op.ApplyMatrix(new Vec(0, 0, 1)));
        var t0 = op.SeitzTranslation;
        bool sourceIsPureMirror = ((Vec)op.IntrinsicTranslation) * ((Vec)op.IntrinsicTranslation) < 1e-12; // 260509Ch
        var planes = new Dictionary<(long, long, long), PlaneRep>();
        var lattices = new List<(double U, double V, double W)> { (0, 0, 0) };
        if (centerings != null) lattices.AddRange(centerings);
        foreach (var lat in lattices)
            for (int lx = -2; lx <= 2; lx++) for (int ly = -2; ly <= 2; ly++) for (int lz = -2; lz <= 2; lz++)
            {
                var t = new Vec(t0.U + lat.U + lx, t0.V + lat.V + ly, t0.W + lat.W + lz);
                var rt = R * t;
                var n = (t - rt) * 0.5;     // 平面法線方向の代表 (lattice 同値類のキー)
                // 旧処理: var glide = (t + rt) * 0.5;
                // 純 mirror に格子並進を合成すると見かけの glide 成分が出るが、対称要素としては mirror のまま扱う。260509Ch
                var glide = sourceIsPureMirror ? Vec.Zero : (t + rt) * 0.5;
                var key = (R6(n.X), R6(n.Y), R6(n.Z));
                var plane = new PlaneRep(t.X / 2.0, t.Y / 2.0, t.Z / 2.0,
                    CenterMod1(glide.X), CenterMod1(glide.Y), CenterMod1(glide.Z), op.Direction);
                if (!planes.TryGetValue(key, out var current) || GlideScore(plane) < GlideScore(current))
                    planes[key] = plane;
            }
        foreach (var plane in planes.Values) yield return plane;

        static double GlideScore(PlaneRep p) => Math.Abs(p.GlideU) + Math.Abs(p.GlideV) + Math.Abs(p.GlideW);
    }

    /// <summary>代表 mirror/glide 面を proper symmetry operation で写し、点群対称で等価な面を列挙する。</summary>
    private static IEnumerable<PlaneRep> EnumerateEquivalentSymmetryPlanes(PlaneRep seed, SymmetryOperation[] ops)
    {
        var seen = new HashSet<(long, long, long, long, long, long, int, int, int)>();
        foreach (var op in ops)
        {
            if (op.Order <= 0) continue;
            var p = op.ApplyAffine(new Vec(seed.Px, seed.Py, seed.Pz));
            var g = op.ApplyMatrix(new Vec(seed.GlideU, seed.GlideV, seed.GlideW));
            var d = NormalizeDirection(op.ApplyMatrix(new Vec(seed.Direction.U, seed.Direction.V, seed.Direction.W)));
            if (d == (0, 0, 0)) continue;
            var eq = new PlaneRep(p.X, p.Y, p.Z, CenterMod1(g.X), CenterMod1(g.Y), CenterMod1(g.Z), d);
            var key = (R6(eq.Px), R6(eq.Py), R6(eq.Pz), R6(eq.GlideU), R6(eq.GlideV), R6(eq.GlideW),
                eq.Direction.U, eq.Direction.V, eq.Direction.W);
            if (seen.Add(key)) yield return eq;
        }
    }
    #endregion

    #region centering 展開
    /// <summary>R-/F-/I-centering 等を runtime で展開し、(I-R) 線形分解で全方向の等価操作を生成する。
    /// baseOps はその空間群の WyckoffPositions[s][0].PositionOperations。seriesNumber は SymmetryOperation の series-aware 化に使う。</summary>
    private static SymmetryOperation[] ExpandWithCentering(SymmetryOperation[] baseOps, int seriesNumber)
    {
        // (260504Ch) centering 付き SymmetryOperation は Seitz 操作としては正しいが、対称要素表では
        //   Position = 軸/面上の代表点、IntrinsicTranslation = 軸/面内成分、という正準分解が必要。
        //   先に全 op を正準化しておくと、元データ由来コピーと runtime 展開コピーが同じ seen-key に落ちる。
        var ops = baseOps.Select(op => Canonicalize(new SymmetryOperation(op, seriesNumber))).ToArray();
        var cents = new List<(double U, double V, double W)>();
        // 中心化ベクトルは恒等 op の IntrinsicTranslation に格納されている (識別: Order == 1 で IT が非ゼロ)。
        foreach (var op in ops)
        {
            if (op.Order != 1) continue;
            var (cu, cv, cw) = op.IntrinsicTranslation;
            if (Math.Abs(cu) + Math.Abs(cv) + Math.Abs(cw) > 1e-6) cents.Add((cu, cv, cw));
        }
        if (cents.Count == 0) return ops;

        var result = new List<SymmetryOperation>(ops.Length * (cents.Count + 1));
        var seen = new HashSet<(int, bool, int, int, int, long, long, long, long, long, long)>();

        bool TryAdd(SymmetryOperation op)
        {
            var key = (op.Order, op.Sense, op.Direction.U, op.Direction.V, op.Direction.W,
                R6(Mod1(op.Position.U)), R6(Mod1(op.Position.V)), R6(Mod1(op.Position.W)),
                R6(CenterMod1(op.IntrinsicTranslation.U)), R6(CenterMod1(op.IntrinsicTranslation.V)),
                R6(CenterMod1(op.IntrinsicTranslation.W)));
            if (!seen.Add(key)) return false;
            result.Add(op);
            return true;
        }

        foreach (var op in ops) TryAdd(op);
        // (260504Ch) TryCreateCenteredOperation 自身が SeriesNumber を保つので包み直し不要。
        foreach (var op in ops)
            foreach (var c in cents)
                if (TryCreateCenteredOperation(op, c, out var centered))
                    TryAdd(centered);
        return [.. result];
    }

    /// <summary>(260503Cl 追加) op の (Position, IntrinsicTranslation) を正準形に書き直す。SeitzTranslation は不変。
    /// 回転 (Order > 0): IT の軸-垂直成分を Position に吸収し、IT は軸方向 (= screw) 成分のみに。
    /// 鏡映 (Order = -2): IT の法線方向成分を Position に吸収し、IT は面内 (= glide) 成分のみに。
    /// 反転や -3/-4/-6: (I-R) で Position に押し込めるだけ押し込み、残差を IT に。
    /// SymmetryOperation の centering constructor が生成する「Position 不変・IT に centering shift を積んだ」
    /// 非正準表現を、軸位置を反映した正準表現へ戻すために使う。</summary>
    private static SymmetryOperation Canonicalize(SymmetryOperation op)
    {
        if (op.Order == 1) return op;
        var t = (Vec)op.IntrinsicTranslation;
        if (t * t < 1e-12) return op;
        if (!TrySplitTranslation(op, t, out var shift, out var residual)) return op;
        var p = op.Position;
        // (260504Ch) SeriesNumber を維持。落とすと ApplyMatrix が isHex=false に転落し trigonal/hex の I-R が壊れる。
        return new SymmetryOperation(op.Order, op.Sense ? 1 : -1, op.Direction,
            (Mod1(p.U + shift.X), Mod1(p.V + shift.Y), Mod1(p.W + shift.Z)),
            (CenterMod1(residual.X), CenterMod1(residual.Y), CenterMod1(residual.Z)),
            op.SeriesNumber);
    }

    private static bool TryCreateCenteredOperation(SymmetryOperation op, (double U, double V, double W) centering, out SymmetryOperation centered)
    {
        centered = default;
        if (op.Order == 1) return false;
        if (Math.Abs(centering.U) + Math.Abs(centering.V) + Math.Abs(centering.W) < 1e-9) return false;
        if (!TrySplitTranslation(op, centering, out var shift, out var residual)) return false;
        var p = op.Position;
        var it = op.IntrinsicTranslation;
        // (260504Ch) Canonicalize と同じ理由で SeriesNumber を維持する。
        centered = new SymmetryOperation(op.Order, op.Sense ? 1 : -1, op.Direction,
            (Mod1(p.U + shift.X), Mod1(p.V + shift.Y), Mod1(p.W + shift.Z)),
            (CenterMod1(it.U + residual.X), CenterMod1(it.V + residual.Y), CenterMod1(it.W + residual.Z)),
            op.SeriesNumber);
        return true;
    }

    /// <summary>(260504Ch) 追加並進を、対称要素の代表位置を動かす成分 shift と、軸/面内に残る intrinsic 成分 residual へ分解する。</summary>
    private static bool TrySplitTranslation(SymmetryOperation op, Vec translation, out Vec shift, out Vec residual)
    {
        shift = Vec.Zero;
        residual = Vec.Zero;
        if (op.Order == -2)
        {
            // mirror/glide: 法線方向は面位置のシフト (translation/2)、面内成分は glide として残る ((translation + R·translation)/2)。
            var rt = op.ApplyMatrix(translation);
            shift = translation * 0.5;
            residual = (translation + rt) * 0.5;
            return true;
        }

        var a = BuildIMinusR(op);
        if (a.Rank() == 2 && op.Order > 0)
            return TryDecomposeAxisLatticeTranslation(op, translation, out shift, out residual);
        if (!TrySolveLinear(a, translation, out shift)) return false;
        residual = translation - a * shift;
        return true;
    }
    #endregion

    #region 線形代数ヘルパー
    /// <summary>I - R を Mat で返す。R は op の線形部 (回転 / 回反)。</summary>
    private static Mat BuildIMinusR(SymmetryOperation op) => new(
        new Vec(1, 0, 0) - op.ApplyMatrix(new Vec(1, 0, 0)),
        new Vec(0, 1, 0) - op.ApplyMatrix(new Vec(0, 1, 0)),
        new Vec(0, 0, 1) - op.ApplyMatrix(new Vec(0, 0, 1)));

    /// <summary>(I-R) の null space (= 軸方向) に直交する covector を返す。Rank=2 の場合は列ベクトルの cross product のうち最大ノルムのものを採用。</summary>
    private static bool TryFindAxisCovector(Mat a, out Vec n)
    {
        var (c0, c1, c2) = (a.Column(0), a.Column(1), a.Column(2));
        n = c0.Cross(c1); double n2 = n * n;
        var cand = c0.Cross(c2); double cn2 = cand * cand;
        if (cn2 > n2) { n = cand; n2 = cn2; }
        cand = c1.Cross(c2); cn2 = cand * cand;
        if (cn2 > n2) { n = cand; n2 = cn2; }
        return n2 > 1e-12;
    }

    /// <summary>A·x = b を解く。rank に応じて Cramer / rank-2 部分行列 / rank-1 列射影 で fallback。</summary>
    private static bool TrySolveLinear(Mat a, Vec b, out Vec x)
    {
        x = Vec.Zero;
        int rank = a.Rank();
        if (rank == 0) return b * b < 1e-12;
        if (rank == 2) return TrySolveRankTwo(a, b, out x);
        if (rank == 1)
        {
            int best = -1; double bestN2 = 0;
            for (int i = 0; i < 3; i++)
            {
                double n2 = a.Column(i) * a.Column(i);
                if (n2 > bestN2) { bestN2 = n2; best = i; }
            }
            if (best < 0) return b * b < 1e-12;
            double v = a.Column(best) * b / bestN2;
            Span<double> values = stackalloc double[3]; values[best] = v;
            x = new Vec(values[0], values[1], values[2]);
            var r = a * x - b;
            return r * r < 1e-10;
        }
        // rank == 3 → Cramer
        var (cc0, cc1, cc2) = (a.Column(0), a.Column(1), a.Column(2));
        double det = a.Determinant();
        if (Math.Abs(det) < 1e-12) return false;
        x = new Vec(Mat.Determinant(b, cc1, cc2) / det,
                    Mat.Determinant(cc0, b, cc2) / det,
                    Mat.Determinant(cc0, cc1, b) / det);
        var rr = a * x - b;
        return rr * rr < 1e-10;
    }

    /// <summary>2 行 2 列 minors を全列挙して最も残差の小さい解を返す (rank=2 用)。</summary>
    private static bool TrySolveRankTwo(Mat a, Vec b, out Vec x)
    {
        x = Vec.Zero;
        double bestResidual = double.PositiveInfinity;
        Span<double> rhs = [b.X, b.Y, b.Z];
        Span<double> cand = stackalloc double[3];
        for (int fixedCol = 0; fixedCol < 3; fixedCol++)
        {
            int c0 = fixedCol == 0 ? 1 : 0;
            int c1 = fixedCol == 2 ? 1 : 2;
            for (int r0 = 0; r0 < 2; r0++) for (int r1 = r0 + 1; r1 < 3; r1++)
            {
                double det = a[r0, c0] * a[r1, c1] - a[r0, c1] * a[r1, c0];
                if (Math.Abs(det) < 1e-12) continue;
                double v0 = (rhs[r0] * a[r1, c1] - a[r0, c1] * rhs[r1]) / det;
                double v1 = (a[r0, c0] * rhs[r1] - rhs[r0] * a[r1, c0]) / det;
                cand.Clear();
                cand[c0] = v0; cand[c1] = v1;
                var cv = new Vec(cand[0], cand[1], cand[2]);
                var err = a * cv - b;
                double residual = err * err;
                if (residual >= bestResidual) continue;
                bestResidual = residual; x = cv;
            }
        }
        return bestResidual < 1e-10;
    }
    #endregion

    #region スカラー / 方向 ヘルパー
    private static double Mod1(double x) => x - Math.Floor(x);

    /// <summary>x を [-0.5, 0.5] に折り畳む。微小値は 0 に snap。</summary>
    public static double CenterMod1(double x)
    {
        x -= Math.Round(x);
        return Math.Abs(x) < 1e-9 ? 0 : x;
    }

    /// <summary>方向ベクトルを正規化 (gcd で割って sign を canonicalize)。</summary>
    private static (int U, int V, int W) NormalizeDirection(Vec d)
    {
        int u = (int)Math.Round(d.X), v = (int)Math.Round(d.Y), w = (int)Math.Round(d.Z);
        int gcd = (int)GammaFunction.Gcd(GammaFunction.Gcd(Math.Abs(u), Math.Abs(v)), Math.Abs(w));
        if (gcd > 1) { u /= gcd; v /= gcd; w /= gcd; }
        if (u < 0 || (u == 0 && v < 0) || (u == 0 && v == 0 && w < 0)) (u, v, w) = (-u, -v, -w);
        return (u, v, w);
    }

    /// <summary>Math.Round(x * 1e6) を long 化する dedup キー用ヘルパー。</summary>
    public static long R6(double x) => (long)Math.Round(x * 1e6);
    #endregion
}
