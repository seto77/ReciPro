using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

public sealed record SiteGroup(string Name, string[] Elements);
public sealed class SiteBasedParameterization
{
    private readonly CompositionSpace space;
    public IReadOnlyList<SiteGroup> Groups { get; }

    // 占有率パラメータ: (groupIndex, elementName) -> ratio A/(A+B)
    public IReadOnlyList<(int groupIndex, string element)> OccupancyParams { get; }

    // extent パラメータ: name と、composition->value を提供
    public IReadOnlyList<ExtentParam> ExtentParams { get; }

    // サイト総数 T_g を extent の線形式で表す係数
    // T_g(y) = b0 + b^T y
    // group g ごとに (b0, bVector)
    private readonly (double b0, Vector<double> b)[] totalFits;

    public int ParameterDim => OccupancyParams.Count + ExtentParams.Count;

    public SiteBasedParameterization(
        CompositionSpace space,
        SiteGroup[] groups,
        int maxExtentSubsetSize = 2)
    {
        this.space = space;
        Groups = groups.ToArray();

        // 1) 元素インデックス辞書
        var idx = space.Elements.Select((e, i) => (e, i))
                               .ToDictionary(x => x.e, x => x.i);

        // 2) Occupancy params を決める（各グループ k元素なら k-1個）
        var occ = new List<(int, string)>();
        for (int g = 0; g < Groups.Count; g++)
        {
            var els = Groups[g].Elements;
            if (els.Length <= 1) continue;
            for (int j = 0; j < els.Length - 1; j++)
                occ.Add((g, els[j]));
        }
        OccupancyParams = occ;

        // 3) 必要な extent 次元 = AffineDim - occupancy自由度合計
        int occDim = occ.Sum(p => 1);
        int needExtent = space.AffineDim - occDim;
        if (needExtent < 0)
            throw new InvalidOperationException(
                $"Too many occupancy params ({occDim}) for AffineDim={space.AffineDim}. Reduce site group sizes or lock some ratios.");

        // 4) extent 候補生成（サイト群内の部分和を中心に）→二値性で上位を選ぶ
        var endmemberComps = Enumerable.Range(0, space.V.ColumnCount)
                                       .Select(j => space.V.Column(j))
                                       .ToArray();

        var extentCandidates = GenerateExtentCandidates(idx, endmemberComps, maxExtentSubsetSize);
        var chosenExtent = ChooseIndependentExtent(extentCandidates, endmemberComps, needExtent);

        ExtentParams = chosenExtent;

        // 5) サイト総数 T_g を extent の線形式でフィット
        totalFits = FitTotals(idx, endmemberComps);
    }

    // ---- 公開API：composition -> (occ, extent) ----

    public Dictionary<string, double> ToParameters(Vector<double> composition)
    {
        var idx = space.Elements.Select((e, i) => (e, i))
                               .ToDictionary(x => x.e, x => x.i);

        var p = new Dictionary<string, double>();

        // extent
        for (int j = 0; j < ExtentParams.Count; j++)
            p[ExtentParams[j].Name] = ExtentParams[j].Eval(composition);

        // occupancy
        for (int k = 0; k < OccupancyParams.Count; k++)
        {
            var (g, el) = OccupancyParams[k];
            double num = composition[idx[el]];
            double den = Groups[g].Elements.Sum(e => composition[idx[e]]);
            p[$"x[{Groups[g].Name}:{el}]"] = den > 0 ? num / den : double.NaN;
        }

        return p;
    }

    // ---- 公開API：parameters -> composition（解釈式で復元） ----

    public Vector<double> FromParameters(Dictionary<string, double> parameters)
    {
        int m = space.Elements.Count;
        var c = DenseVector.Create(m, 0.0);

        var idx = space.Elements.Select((e, i) => (e, i))
                               .ToDictionary(x => x.e, x => x.i);

        // y ベクトル
        var y = DenseVector.Create(ExtentParams.Count, 0.0);
        for (int j = 0; j < ExtentParams.Count; j++)
        {
            if (!parameters.TryGetValue(ExtentParams[j].Name, out double v))
                throw new ArgumentException($"Missing extent parameter: {ExtentParams[j].Name}");
            y[j] = v;
        }

        // 各サイト総数を復元
        for (int g = 0; g < Groups.Count; g++)
        {
            double Tg = totalFits[g].b0 + totalFits[g].b.DotProduct(y);

            var els = Groups[g].Elements;
            if (els.Length == 1)
            {
                c[idx[els[0]]] = Tg;
                continue;
            }

            // 占有率（k-1個 + 最後は 1-Σ）
            double sumOcc = 0.0;
            for (int j = 0; j < els.Length - 1; j++)
            {
                string key = $"x[{Groups[g].Name}:{els[j]}]";
                if (!parameters.TryGetValue(key, out double x))
                    throw new ArgumentException($"Missing occupancy parameter: {key}");
                sumOcc += x;
                c[idx[els[j]]] = x * Tg;
            }
            // 最後の元素
            double last = 1.0 - sumOcc;
            c[idx[els[^1]]] = last * Tg;
        }

        return c;
    }

    // ---- 表示用：解釈付き式（サイト総数は線形）を文字列化 ----
    public string BuildSymbolicFormula()
    {
        // extent 名を y1,y2... 的に見せたいならここで置換してもOK
        // ここでは ExtentParams[j].Name をそのまま使う
        var parts = new List<string>();

        for (int g = 0; g < Groups.Count; g++)
        {
            var els = Groups[g].Elements;

            string Tg = LinearExprToString(totalFits[g].b0, totalFits[g].b, ExtentParams.Select(e => e.Name).ToArray());

            if (els.Length == 1)
            {
                parts.Add($"{els[0]}_{{{Tg}}}");
                continue;
            }

            // 例: (A_{x}B_{1-x})_{T}
            // ここでは (e1_{x1} e2_{x2} ... ek_{1-Σ})_{T} の形
            var occSymbols = new List<string>();
            double dummy = 0; // 表示なので計算しない
            for (int j = 0; j < els.Length - 1; j++)
                occSymbols.Add($"{els[j]}_{{x[{Groups[g].Name}:{els[j]}]}}");

            string lastSym = $"1-({string.Join("+", els.Take(els.Length - 1).Select(e => $"x[{Groups[g].Name}:{e}]"))})";
            occSymbols.Add($"{els[^1]}_{{{lastSym}}}");

            parts.Add($"({string.Join("", occSymbols)})_{{{Tg}}}");
        }

        return string.Join(" ", parts);
    }

    // ================== 内部：extentの自動選択とサイト総数フィット ==================

    public sealed class ExtentParam
    {
        public string Name { get; }
        private readonly Vector<double> u;
        private readonly double minVal, maxVal;

        public ExtentParam(string name, Vector<double> u, double minVal, double maxVal)
        {
            Name = name;
            this.u = u;
            this.minVal = minVal;
            this.maxVal = maxVal;
        }

        public double Eval(Vector<double> c)
        {
            double t = u.DotProduct(c);
            if (Math.Abs(maxVal - minVal) < 1e-15) return 0.0;
            return (t - minVal) / (maxVal - minVal);
        }
    }

    private List<ExtentParam> GenerateExtentCandidates(
        Dictionary<string, int> idx,
        Vector<double>[] endmemberComps,
        int maxSubsetSize)
    {
        int m = space.Elements.Count;
        var candidates = new List<(ExtentParam param, double score)>();

        // サイト群内の部分集合（解釈を保つため）
        var elementIndices = new List<int>();
        foreach (var g in Groups)
            foreach (var e in g.Elements)
                elementIndices.Add(idx[e]);

        elementIndices = elementIndices.Distinct().ToList();

        foreach (var subset in EnumerateSubsets(elementIndices, maxSubsetSize))
        {
            var u = DenseVector.Create(m, 0.0);
            foreach (int i in subset) u[i] = 1.0;

            var t = endmemberComps.Select(c => u.DotProduct(c)).ToArray();
            double min = t.Min(), max = t.Max();
            if (Math.Abs(max - min) < 1e-12) continue;

            // 二値性スコア：端成分値が min/max にどれだけ集中？
            int nearMin = t.Count(v => Math.Abs(v - min) < 1e-9);
            int nearMax = t.Count(v => Math.Abs(v - max) < 1e-9);
            if (nearMin + nearMax < t.Length) continue; // 中間があるものは今回は捨てる

            string name = $"y[{string.Join("+", subset.Select(i => space.Elements[i]))}]";
            var p = new ExtentParam(name, u, min, max);

            double score = (max - min) + 0.1 * (nearMin + nearMax); // 適当だが効く
            candidates.Add((p, score));
        }

        // 大きい順
        return candidates.OrderByDescending(x => x.score).Select(x => x.param).ToList();
    }

    private List<ExtentParam> ChooseIndependentExtent(
        List<ExtentParam> candidates,
        Vector<double>[] endmemberComps,
        int need)
    {
        var chosen = new List<ExtentParam>();
        if (need == 0) return chosen;

        foreach (var cand in candidates)
        {
            if (chosen.Count >= need) break;

            var test = chosen.Concat(new[] { cand }).ToList();
            if (IsIndependent(test, endmemberComps))
                chosen.Add(cand);
        }

        if (chosen.Count < need)
            throw new InvalidOperationException($"Could not find enough independent extent parameters. need={need}, got={chosen.Count}");

        return chosen;
    }

    private bool IsIndependent(List<ExtentParam> extents, Vector<double>[] endmemberComps)
    {
        int nEnd = endmemberComps.Length;
        int k = extents.Count;
        if (k <= 1) return true;

        var M = DenseMatrix.Create(nEnd, k, 0.0);
        for (int i = 0; i < nEnd; i++)
            for (int j = 0; j < k; j++)
                M[i, j] = extents[j].Eval(endmemberComps[i]);

        var svd = M.Svd(false);
        var s = svd.S;
        double tol = (s.Count > 0 ? s[0] : 0.0) * 1e-12;

        int rank = 0;
        for (int i = 0; i < s.Count; i++) if (s[i] > tol) rank++;

        return rank >= k;
    }

    private (double b0, Vector<double> b)[] FitTotals(
        Dictionary<string, int> idx,
        Vector<double>[] endmemberComps)
    {
        int nEnd = endmemberComps.Length;
        int k = ExtentParams.Count;

        // デザイン行列: [1, y1, y2, ...]
        var X = DenseMatrix.Create(nEnd, 1 + k, 0.0);
        for (int i = 0; i < nEnd; i++)
        {
            X[i, 0] = 1.0;
            for (int j = 0; j < k; j++)
                X[i, 1 + j] = ExtentParams[j].Eval(endmemberComps[i]);
        }

        var fits = new (double b0, Vector<double> b)[Groups.Count];

        // 各グループごとに T_g を最小二乗（通常は厳密一致）
        for (int g = 0; g < Groups.Count; g++)
        {
            var t = DenseVector.Create(nEnd, 0.0);
            for (int i = 0; i < nEnd; i++)
            {
                double Tg = Groups[g].Elements.Sum(e => endmemberComps[i][idx[e]]);
                t[i] = Tg;
            }

            var beta = X.QR().Solve(t); // (1+k) 次元
            double b0 = beta[0];
            var b = (k > 0) ? beta.SubVector(1, k) : DenseVector.Create(0, 0.0);

            fits[g] = (b0, b);
        }

        return fits;
    }

    private IEnumerable<List<int>> EnumerateSubsets(List<int> items, int maxSize)
    {
        for (int size = 1; size <= maxSize; size++)
            foreach (var comb in Combinations(items, size, 0))
                yield return comb;
    }

    private IEnumerable<List<int>> Combinations(List<int> items, int k, int start)
    {
        if (k == 0) { yield return new List<int>(); yield break; }
        for (int i = start; i <= items.Count - k; i++)
        {
            foreach (var tail in Combinations(items, k - 1, i + 1))
            {
                tail.Insert(0, items[i]);
                yield return tail;
            }
        }
    }

    private static string LinearExprToString(double b0, Vector<double> b, string[] yNames)
    {
        // 表示用：係数は素直に小数表示（必要なら有理近似を入れてOK）
        string s = $"{b0:g}";
        for (int j = 0; j < b.Count; j++)
        {
            double a = b[j];
            if (Math.Abs(a) < 1e-12) continue;
            string sign = a >= 0 ? "+" : "-";
            s += $"{sign}{Math.Abs(a):g}*{yNames[j]}";
        }
        return s;
    }
}


public static class FormulaParser
{
    // 例:
    // "A5X2" -> {A:5, X:2}
    // "A_{5}X_{2}" -> {A:5, X:2}
    // "Fe2O3" -> {Fe:2, O:3}
    //
    // 仕様:
    // - 元素記号: [A-Z][a-z]*
    // - 係数: 省略なら1
    // - 係数は "2" でも "_{2}" でもOK
    // - 小数係数も一応対応（"0.5" や "_{0.5}"）
    public static Dictionary<string, double> ParseToElementCounts(string formula)
    {
        if (string.IsNullOrWhiteSpace(formula))
            throw new ArgumentException("Formula is empty.");

        // 空白除去
        formula = Regex.Replace(formula, @"\s+", "");

        // トークン: Element + optional coeff (digits or _{digits})
        // 例: A5, X2, Fe2, O3, A_{5}, X_{2}
        var pattern = new Regex(
            @"(?<el>[A-Z][a-z]*)(?:(?<coef>\d+(?:\.\d+)?)|_\{(?<coef2>\d+(?:\.\d+)?)\})?",
            RegexOptions.Compiled);

        var dict = new Dictionary<string, double>();
        int pos = 0;

        var matches = pattern.Matches(formula);
        if (matches.Count == 0)
            throw new FormatException($"Cannot parse formula: {formula}");

        foreach (Match m in matches)
        {
            if (!m.Success) continue;

            // 連続マッチで式全体を覆えているかを軽く検査
            if (m.Index != pos)
                throw new FormatException($"Unsupported syntax near '{formula.Substring(pos)}' in '{formula}'");
            pos += m.Length;

            var el = m.Groups["el"].Value;

            string coefStr = m.Groups["coef"].Success ? m.Groups["coef"].Value :
                             m.Groups["coef2"].Success ? m.Groups["coef2"].Value : null;

            double coef = 1.0;
            if (!string.IsNullOrEmpty(coefStr))
                coef = double.Parse(coefStr, CultureInfo.InvariantCulture);

            if (dict.ContainsKey(el)) dict[el] += coef;
            else dict[el] = coef;
        }

        if (pos != formula.Length)
            throw new FormatException($"Unsupported trailing syntax in '{formula}' at '{formula.Substring(pos)}'");

        return dict;
    }
}

public sealed class CompositionSpace
{
    public IReadOnlyList<string> EndmemberFormulas { get; }
    public IReadOnlyList<string> Elements { get; } // 元素の並び（行インデックス）

    // V: (numElements x numEndmembers)
    public Matrix<double> V { get; }

    // 参照端成分（アフィン空間の原点として使う）
    public int ReferenceIndex { get; }
    public Vector<double> Vref => V.Column(ReferenceIndex);

    // D: (numElements x (numEndmembers-1))  = [v_i - v_ref] (i != ref)
    public Matrix<double> D { get; }

    // 真の自由度（アフィン次元）
    public int AffineDim { get; }

    // D の列空間の正規直交基底 U_k（numElements x k）
    public Matrix<double> BasisUk { get; }

    // 不変量（左nullspace）: W^T D = 0 となる W（numElements x m）
    // 各列 w に対し w^T c は混合で不変（アフィン的に一定）
    public Matrix<double> InvariantW { get; }

    // 不変量の定数項: w^T v_ref（m次元）
    public Vector<double> InvariantConst { get; }

    public CompositionSpace(string[] endmemberFormulas, int referenceIndex = -1, double svdTolRel = 1e-12)
    {
        if (endmemberFormulas == null || endmemberFormulas.Length < 2)
            throw new ArgumentException("Need at least 2 endmembers.");

        EndmemberFormulas = endmemberFormulas.ToArray();

        // 1) 端成分を辞書化
        var endmemberCounts = EndmemberFormulas
            .Select(FormulaParser.ParseToElementCounts)
            .ToArray();

        // 2) 元素集合（登場順を基本、安定化のためソートしたいなら OrderBy に）
        var elementSet = new List<string>();
        var seen = new HashSet<string>();
        foreach (var d in endmemberCounts)
        {
            foreach (var el in d.Keys)
            {
                if (seen.Add(el)) elementSet.Add(el);
            }
        }
        Elements = elementSet;

        int m = Elements.Count;
        int n = EndmemberFormulas.Count;

        // 3) V 行列を構築
        var Vtmp = DenseMatrix.Create(m, n, 0.0);
        for (int j = 0; j < n; j++)
        {
            foreach (var kv in endmemberCounts[j])
            {
                int i = elementSet.IndexOf(kv.Key);
                Vtmp[i, j] = kv.Value;
            }
        }
        V = Vtmp;

        // 参照端成分
        ReferenceIndex = (referenceIndex >= 0 && referenceIndex < n) ? referenceIndex : (n - 1);

        // 4) D = [v_i - v_ref]
        var cols = new List<Vector<double>>();
        for (int j = 0; j < n; j++)
            if (j != ReferenceIndex) cols.Add(V.Column(j) - Vref);

        D = DenseMatrix.OfColumnVectors(cols);

        // 5) SVD で rank(D) = アフィン次元
        var svd = D.Svd(computeVectors: true);
        var s = svd.S;

        double tol = (s.Count > 0 ? s[0] : 0.0) * svdTolRel;
        int rank = 0;
        for (int i = 0; i < s.Count; i++)
            if (s[i] > tol) rank++;

        AffineDim = rank;

        // 6) 基底 U_k（D の列空間）
        // Math.NET の SVD は D = U * S * VT
        // U の先頭 rank 列が列空間基底
        BasisUk = (AffineDim > 0)
            ? svd.U.SubMatrix(0, m, 0, AffineDim)
            : DenseMatrix.Create(m, 0, 0.0);

        // 7) 左nullspace（不変量）: D^T の nullspace でも同じ
        // U の残り (m-rank) 列が left-nullspace を張る（U は m×m 正規直交）
        int invDim = m - AffineDim;
        InvariantW = (invDim > 0)
            ? svd.U.SubMatrix(0, m, AffineDim, invDim)
            : DenseMatrix.Create(m, 0, 0.0);

        InvariantConst = (invDim > 0)
            ? InvariantW.TransposeThisAndMultiply(Vref)
            : DenseVector.Create(0, 0.0);
    }

    // ---- 基本操作 ----

    // weights (n次元): 非負・和1を想定（チェックは呼び出し側/必要ならここで）
    public Vector<double> ComposeFromWeights(Vector<double> weights)
    {
        if (weights.Count != V.ColumnCount)
            throw new ArgumentException("weights length mismatch.");

        return V * weights; // c = V w
    }

    // 組成 c から、機械的低次元座標 z (k次元) を得る
    // z = U_k^T (c - v_ref)
    public Vector<double> ToLowDimCoordinate(Vector<double> composition)
    {
        if (composition.Count != V.RowCount)
            throw new ArgumentException("composition length mismatch.");

        var delta = composition - Vref;
        return BasisUk.TransposeThisAndMultiply(delta);
    }

    // 低次元座標 z から、アフィン空間上の点 c を復元（c = v_ref + U_k z）
    // ※ 端成分凸包（非負・和1）に入るとは限らない（あくまでアフィン空間）
    public Vector<double> FromLowDimCoordinate(Vector<double> z)
    {
        if (z.Count != AffineDim)
            throw new ArgumentException("z length mismatch.");

        return Vref + (BasisUk * z);
    }

    // 不変量チェック: w^T c が const と一致するか
    public Vector<double> InvariantsOf(Vector<double> composition)
    {
        if (composition.Count != V.RowCount)
            throw new ArgumentException("composition length mismatch.");

        return InvariantW.TransposeThisAndMultiply(composition);
    }

    // ---- オプション：組成から weights を推定（簡易版） ----
    // 注意: 厳密な「非負 + 和1」の最適化（NNLS + 等式制約）は別途QP/最適化が必要。
    // ここでは「最小二乗→クリップ→正規化」という雑だが実用的な初期値生成を提供。
    public Vector<double> EstimateWeightsLeastSquares(Vector<double> composition, bool enforceSimplex = true)
    {
        if (composition.Count != V.RowCount)
            throw new ArgumentException("composition length mismatch.");

        // 最小二乗解 w = argmin ||V w - c||_2
        // Math.NET: QR などを使う
        var qr = V.QR();
        var w = qr.Solve(composition);

        if (!enforceSimplex) return w;

        // 非負クリップ
        for (int i = 0; i < w.Count; i++)
            if (w[i] < 0) w[i] = 0;

        // 和1に正規化（全部0なら参照端成分に）
        double sum = w.Sum();
        if (sum <= 0)
        {
            w.Clear();
            w[ReferenceIndex] = 1.0;
            return w;
        }
        return w / sum;
    }

    // 表示用：元素名付きで辞書化
    public Dictionary<string, double> ToElementDictionary(Vector<double> composition)
    {
        if (composition.Count != Elements.Count)
            throw new ArgumentException("composition length mismatch.");
        var dict = new Dictionary<string, double>();
        for (int i = 0; i < Elements.Count; i++)
            dict[Elements[i]] = composition[i];
        return dict;
    }
}

public abstract class ParameterCandidate
{
    public string Name { get; }
    protected ParameterCandidate(string name) => Name = name;

    // composition -> value
    public abstract double Eval(Vector<double> c);

    // スコア（大きいほど良い）
    public double Score { get;  set; }
}

public sealed class ExtentCandidate : ParameterCandidate
{
    private readonly Vector<double> u;
    private readonly double minVal, maxVal;

    public ExtentCandidate(string name, Vector<double> u, double minVal, double maxVal) : base(name)
    {
        this.u = u;
        this.minVal = minVal;
        this.maxVal = maxVal;
    }

    public override double Eval(Vector<double> c)
    {
        double t = u.DotProduct(c);
        if (Math.Abs(maxVal - minVal) < 1e-15) return 0.0;
        return (t - minVal) / (maxVal - minVal); // 0..1
    }
}

public sealed class RatioCandidate : ParameterCandidate
{
    private readonly Vector<double> num;
    private readonly Vector<double> den;

    public RatioCandidate(string name, Vector<double> num, Vector<double> den) : base(name)
    {
        this.num = num; this.den = den;
    }

    public override double Eval(Vector<double> c)
    {
        double d = den.DotProduct(c);
        if (d <= 0) return double.NaN;
        return num.DotProduct(c) / d;
    }
}

public sealed class InterpretableParameterizer
{
    private readonly CompositionSpace space;

    public InterpretableParameterizer(CompositionSpace space)
    {
        this.space = space;
        if (space.AffineDim <= 0)
            throw new ArgumentException("AffineDim must be >= 1.");
    }

    // ---- 公開API：パラメータを自動選択して返す ----
    public IReadOnlyList<ParameterCandidate> FindParameters(
        int maxSubsetSize = 2,
        int maxCandidates = 2000)
    {
        // 端成分組成（列ベクトル）
        var endmemberComps = Enumerable.Range(0, space.V.ColumnCount)
                                       .Select(j => space.V.Column(j))
                                       .ToArray();

        // 候補生成
        var extent = GenerateExtentCandidates(endmemberComps, maxSubsetSize);
        var ratio = GenerateRatioCandidates(endmemberComps, maxSubsetSize);

        var all = extent.Cast<ParameterCandidate>().Concat(ratio).ToList();

        // スコア付け
        ScoreCandidates(all, endmemberComps);

        // 上位だけ残す
        all = all.OrderByDescending(c => c.Score).Take(maxCandidates).ToList();

        // AffineDim 個を独立になるよう貪欲に選ぶ
        var chosen = GreedySelectIndependent(all, endmemberComps, space.AffineDim);

        return chosen;
    }

    // ---- 候補生成：Extent（線形→0..1） ----
    private List<ExtentCandidate> GenerateExtentCandidates(Vector<double>[] endmemberComps, int maxSubsetSize)
    {
        int m = space.Elements.Count;
        var result = new List<ExtentCandidate>();

        foreach (var subset in EnumerateSubsets(m, maxSubsetSize))
        {
            var u = DenseVector.Create(m, 0.0);
            foreach (int idx in subset) u[idx] = 1.0;

            // 端成分での値 t_j = u^T v_j
            var t = endmemberComps.Select(c => u.DotProduct(c)).ToArray();
            double min = t.Min(), max = t.Max();
            if (Math.Abs(max - min) < 1e-12) continue;

            // 端成分で「二値っぽい」ほど良い：min側とmax側に固まるか
            // ここでは簡易：中間値が少ないほど良い
            double mid = 0.5 * (min + max);
            int nearMin = t.Count(v => Math.Abs(v - min) < 1e-9);
            int nearMax = t.Count(v => Math.Abs(v - max) < 1e-9);

            if (nearMin + nearMax < t.Length) continue; // 中間があるなら一旦捨てる（強いフィルタ）

            string name = $"extent(sum[{string.Join(",", subset.Select(i => space.Elements[i]))}])";
            result.Add(new ExtentCandidate(name, u, min, max));
        }

        return result;
    }

    // ---- 候補生成：Ratio（部分集合/部分集合） ----
    private List<RatioCandidate> GenerateRatioCandidates(Vector<double>[] endmemberComps, int maxSubsetSize)
    {
        int m = space.Elements.Count;
        var result = new List<RatioCandidate>();

        var subsets = EnumerateSubsets(m, maxSubsetSize).ToList();

        foreach (var denSet in subsets)
        {
            var den = DenseVector.Create(m, 0.0);
            foreach (int i in denSet) den[i] = 1.0;

            // 分母が端成分で常に正か確認
            var denVals = endmemberComps.Select(c => den.DotProduct(c)).ToArray();
            if (denVals.Any(v => v <= 1e-12)) continue;
            if (denVals.Max() - denVals.Min() < 1e-12) continue; // 変動ゼロは意味薄

            foreach (var numSet in subsets)
            {
                // num ⊂ den だけ許す（解釈重視）
                if (!numSet.All(i => denSet.Contains(i))) continue;
                if (numSet.Count == 0 || numSet.Count == denSet.Count) continue;

                var num = DenseVector.Create(m, 0.0);
                foreach (int i in numSet) num[i] = 1.0;

                // 端成分で 0..1 に入るか
                var x = endmemberComps.Select(c => num.DotProduct(c) / den.DotProduct(c)).ToArray();
                if (x.Any(v => double.IsNaN(v) || v < -1e-9 || v > 1 + 1e-9)) continue;

                string name = $"ratio(sum[{string.Join(",", numSet.Select(i => space.Elements[i]))}]"
                            + $"/sum[{string.Join(",", denSet.Select(i => space.Elements[i]))}])";

                result.Add(new RatioCandidate(name, num, den));
            }
        }

        return result;
    }

    // ---- スコアリング ----
    private void ScoreCandidates(List<ParameterCandidate> candidates, Vector<double>[] endmemberComps)
    {
        foreach (var cand in candidates)
        {
            var vals = endmemberComps.Select(c => cand.Eval(c)).ToArray();
            if (vals.Any(v => double.IsNaN(v) || double.IsInfinity(v)))
            {
                cand.Score = double.NegativeInfinity;
                continue;
            }

            // “良さ”の雑スコア：
            //  - 端成分値の分散が大きい（変化がある）
            //  - 端成分値が[0,1]に入る
            double mean = vals.Average();
            double var = vals.Select(v => (v - mean) * (v - mean)).Average();

            // 端成分で 0/1 に寄っているとボーナス（extent向け）
            double binaryBonus = vals.Select(v => Math.Min(v, 1 - v)).Average(); // 0なら完全二値
            binaryBonus = 1.0 - binaryBonus; // 大きいほど二値

            cand.Score = var + 0.2 * binaryBonus;
        }
    }

    // ---- 独立性を満たすように貪欲選択 ----
    private List<ParameterCandidate> GreedySelectIndependent(
        List<ParameterCandidate> sortedCandidates,
        Vector<double>[] endmemberComps,
        int k)
    {
        var chosen = new List<ParameterCandidate>();

        foreach (var cand in sortedCandidates)
        {
            if (chosen.Count >= k) break;

            var test = chosen.Concat(new[] { cand }).ToList();
            if (IsIndependentOnAffineSpace(test, endmemberComps))
                chosen.Add(cand);
        }

        return chosen;
    }

    // “独立”判定：端成分上の値行列のrankを見る（簡易）
    private bool IsIndependentOnAffineSpace(List<ParameterCandidate> cands, Vector<double>[] endmemberComps)
    {
        int nEnd = endmemberComps.Length;
        int k = cands.Count;
        if (k == 1) return true;

        var M = DenseMatrix.Create(nEnd, k, 0.0);
        for (int i = 0; i < nEnd; i++)
            for (int j = 0; j < k; j++)
                M[i, j] = cands[j].Eval(endmemberComps[i]);

        // rank(M) >= k なら独立扱い
        var svd = M.Svd(false);
        var s = svd.S;
        double tol = (s.Count > 0 ? s[0] : 0.0) * 1e-12;
        int rank = 0;
        for (int i = 0; i < s.Count; i++) if (s[i] > tol) rank++;

        return rank >= k;
    }

    // ---- 部分集合列挙（サイズ1..maxSubsetSize） ----
    private IEnumerable<List<int>> EnumerateSubsets(int n, int maxSize)
    {
        for (int size = 1; size <= maxSize; size++)
        {
            foreach (var comb in Combinations(Enumerable.Range(0, n).ToArray(), size))
                yield return comb;
        }
    }

    private IEnumerable<List<int>> Combinations(int[] items, int k, int start = 0)
    {
        if (k == 0) { yield return new List<int>(); yield break; }
        for (int i = start; i <= items.Length - k; i++)
        {
            foreach (var tail in Combinations(items, k - 1, i + 1))
            {
                tail.Insert(0, items[i]);
                yield return tail;
            }
        }
    }
}
