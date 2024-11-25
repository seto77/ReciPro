using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static System.Math;
using DMat = MathNet.Numerics.LinearAlgebra.Double.DenseMatrix;
using DVec = MathNet.Numerics.LinearAlgebra.Double.DenseVector;

namespace Crystallography;
public class Marquardt
{
    public enum Precision { High, Medium, Low }

    public enum FuncType
    {
        #region 組み込み関数

        /// <summary>
        /// Gaussian 1次元, パラメータは 0: X0, 1: H, 2: A
        /// </summary>
        G1,

        /// <summary>
        /// Gaussian 2次元真円, パラメータは 0: X0, 1: Y0, 2: H, 3: A
        /// </summary>
        G2,

        /// <summary>
        /// Gaussian 2次元楕円, パラメータは 0: X0, 1: Y0, 2: H1, 3: H2, 4: T, 5: A
        /// </summary>
        G2E,

        /// <summary>
        /// Lorenzian 1次元, パラメータは 0: X0, 1: H, 2: A
        /// </summary>
        L1,

        /// <summary>
        /// Lorenzian 2次元真円, パラメータは 0: X0, 1: Y0, 2: H, 3: A
        /// </summary>
        L2,

        /// <summary>
        /// Lorenzian 2次元楕円, パラメータは 0: X0, 1: Y0, 2: H1, 3: H2, 4: T, 5: A
        /// </summary>
        L2E,

        /// <summary>
        /// PseudoVoigt 1次元, パラメータは 0: X0, 1: H, 2: Eta, 3: A
        /// </summary>
        PV1,

        /// <summary>
        /// PseudoVoigt 2次元真円, パラメータは 0: X0, 1: Y0, 2: H, 3: Eta, 4: A
        /// </summary>
        PV2,

        /// <summary>
        /// PseudoVoigt 2次元楕円, パラメータは 0: X0, 1: Y0, 2: H1, 3: H2, 4: T, 5: Eta, 6: A
        /// </summary>
        PV2E,

        /// <summary>
        /// 平面 (B0 + Bx X + By Y), パラメータは 0: B0, 1: Bx, 2: By
        /// </summary>
        Plane

        #endregion 組み込み関数
    }

    public class Function
    {
        /// <summary>
        /// 実質的な無限小 infinitesimal
        /// </summary>
        private const double inf = 1E-10;

        #region プロパティ

        /// <summary>
        /// 関数の表式. Func内の1番目は座標値, 2番目はパラメータ, 3番目は返り値
        /// </summary>
        public Func<double[], double[], double> Formula { get; set; }

        /// <summary>
        /// 複数の座標値に対する関数の表式. Func内の1番目は座標値の, 2番目はパラメータ, 3番目は返り値
        /// </summary>
        public Func<double[][], double[], double[]> FormulaEx { get; set; }

        /// <summary>
        /// パラメータ配列
        /// </summary>
        public double[] Prms { get; set; }

        /// <summary>
        /// パラメータ配列. AddPrmsされたときに、1世代前のパラメータとして保存される
        /// </summary>
        public double[] PrmsPrev { get; set; }

        /// <summary>
        /// パラメータの数
        /// </summary>
        public int PrmLength { get; }

        /// <summary>
        /// 制約条件. double[] をdouble[]に変換する. nullでもよい.
        /// </summary>
        public Func<double[], double[]> Constraints { get; set; }

        /// <summary>
        /// 微分値が解析的にわかっている場合のみ設定. nullでもよい.
        /// </summary>
        public Func<double[], double[], double[]> Derivatives { get; set; }

        public int Length { get => Prms.Length; }
        #endregion プロパティ

        private readonly double[] steps;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="formula"></param>
        /// <param name="parameters"></param>
        /// <param name="constraints"></param>
        /// <param name="derivatives"></param>
        public Function(Func<double[], double[], double> formula, double[] parameters, Func<double[], double[]> constraints = null, Func<double[], double[], double[]> derivatives = null)
        {
            Formula = formula;
            Prms = parameters;
            PrmsPrev = new double[Prms.Length];
            Constraints = constraints;
            Derivatives = derivatives;
            steps = Enumerable.Range(-9, 20).Select(n => Pow(10, n)).ToArray();
            PrmLength = Prms.Length;
        }

        /// <summary>
        /// 関数形を指定したコンストラクタ
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameters">
        /// Gaussian1 or Lorenzian1の場合: 0: X0, 1: H, 2: A
        /// Gaussian2 or Lorenzian2の場合: 0: X0, 1: Y0, 2: H, 3: A
        /// Gaussian2Ellipse or Lorenzian2Ellipseの場合: 0: X0, 1: Y0, 2: H1, 3: H2, 4: T, 5: A
        /// PseudoVoigt1 の場合; 0: X0, 1: H, 2: Eta, 3: A
        /// PseudoVoigt2 の場合; 0: X0, 1: Y0, 2: H, 3: Eta, 4: A
        /// PseudoVoigt2Ellipse の場合; 0: X0, 1: Y0, 2: H1, 3: H2, 4: T, 5: Eta, 6: A
        /// </param>
        public Function(FuncType type, params double[] parameters)
        {
            Prms = parameters;
            PrmsPrev = new double[Prms.Length];

            switch (type)
            {
                #region
                case FuncType.G1:
                    Formula = (x, p) => Gaussian(x[0], p[0], p[1], p[2]);
                    Constraints = p => [p[0], Max(p[1], inf), Max(p[2], inf)];
                    break;

                case FuncType.G2:
                    Formula = (x, p) => Gaussian(x[0], x[1], p[0], p[1], p[2], p[3]);
                    Constraints = p => [p[0], p[1], Max(p[2], inf), Max(p[3], inf)];
                    break;

                case FuncType.G2E:
                    Formula = (x, p) => Gaussian(x[0], x[1], p[0], p[1], p[2], p[3], p[4], p[5]);
                    Constraints = p => [p[0], p[1], Max(p[2], inf), Max(p[3], inf), p[4], Max(p[5], inf)];
                    break;

                case FuncType.L1:
                    Formula = (x, p) => Lorenzian(x[0], p[0], p[1], p[2]);
                    Constraints = p => [p[0], Max(p[1], inf), Max(p[2], inf)];
                    break;

                case FuncType.L2:
                    Formula = (x, p) => Lorenzian(x[0], x[1], p[0], p[1], p[2], p[3]);
                    Constraints = p => [p[0], p[1], Max(p[2], inf), Max(p[3], inf)];
                    break;

                case FuncType.L2E:
                    Formula = (x, p) => Gaussian(x[0], x[1], p[0], p[1], p[2], p[3], p[4], p[5]);
                    Constraints = p => [p[0], p[1], Max(p[2], inf), Max(p[3], inf), p[4], Max(p[5], inf)];
                    break;

                case FuncType.PV1:
                    Formula = (x, p) => PseudoVoigt(x[0], p[0], p[1], p[2], p[3]);
                    Constraints = p => [p[0], Max(p[1], 0.1), Min(Max(p[2], 0), 1.5), Max(p[3], inf)];
                    break;

                case FuncType.PV2:
                    Formula = (x, p) => PseudoVoigt(x[0], x[1], p[0], p[1], p[2], p[3], p[4]);
                    Constraints = p => [p[0], p[1], Max(p[2], 0.1), Min(Max(p[3], 0), 1.5), Max(p[4], inf)];
                    break;

                case FuncType.PV2E:
                    Formula = (x, p) => PseudoVoigt(x, p[0], p[1], p[2], p[3], p[4], p[5], p[6]);
                    Constraints = p => [p[0], p[1], Max(p[2], 0.1), Max(p[3], 0.1), p[4], Min(Max(p[5], 0), 1.5), Max(p[6], inf)];
                    Derivatives = (x, p) => PseudoVoigtDiff(x, p[0], p[1], p[2], p[3], p[4], p[5], p[6]);
                    break;

                case FuncType.Plane:
                    Formula = (x, p) => p[0] + p[1] * x[0] + p[2] * x[1];
                    Derivatives = (x, p) => [1.0, x[0], x[1]];
                    break;
                    #endregion
            }

            steps = Enumerable.Range(-9, 20).Select(n => Pow(10, n)).ToArray();
            PrmLength = Prms.Length;
        }

        /// <summary>
        /// 偏微分値を得る
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[] GetDerivative(double[] x)
        {
            if (Derivatives != null)
                return Derivatives(x, Prms);

            var derivative = new double[Length];
            var f0 = Formula(x, Prms);
            for (int j = 0; j < Length; j++)
                for (int i = 0; i < steps.Length; i++)
                {
                    var pTemp = Prms.ToArray();//値渡しでコピー
                    pTemp[j] += steps[i];
                    derivative[j] = (Formula(x, pTemp) - f0) / steps[i];
                    if (derivative[j] != 0) break;
                }
            return derivative;
        }

        /// <summary>
        /// 与えられた座標での値を得る
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetValue(params double[] x) => Formula(x, Prms);

        /// <summary>
        /// 与えられた座標での値を得る
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[] GetValues(params double[][] x)
        {
            return FormulaEx != null ?
                FormulaEx(x, Prms) :
                x.AsParallel().Select(x1 => Formula(x1, Prms)).ToArray();
        }

        /// <summary>
        /// 全パラメータを指定量だけ変化させる. 制約条件も課す。
        /// </summary>
        /// <param name="delta"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddPrms(double[] delta)
        {
            for (var i = 0; i < Prms.Length; i++)
            {
                PrmsPrev[i] = Prms[i];
                Prms[i] += delta[i];
            }
            ApplyConstraints();
        }

        /// <summary>
        /// 全パラメータを一世代前のものに戻す
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RevertPrms()
        {
            for (int i = 0; i < Prms.Length; i++)
                Prms[i] = PrmsPrev[i];
        }

        /// <summary>
        /// 全パラメータに制約を課す
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ApplyConstraints()
        {
            if (Constraints != null)
                Prms = Constraints(Prms);
        }
    }

    public static double RambdaCoeff1 { get; set; } = 0.1;
    public static double RambdaCoeff2 { get; set; } = 8;

    /// <summary>
    /// 静的メソッド. Marquardt法によるフィッティングを実行する.
    /// </summary>
    /// <param name="obsValues">観測値. x は座標, y は強度、 wは重み(分散の逆数)</param>
    /// <param name="functions">Functinクラスの配列.</param>
    /// <returns>Prms[i][j]はi番目のFunctionのj番目の最適パラメータ. Errorはまだ未実装. Rは残差</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (double[][] Prms, double[][] Error, double R)
        Solve(in (double[] x, double y, double w)[] obsValues, Function[] functions, in Precision precision = Precision.Medium)
    {
        #region 計算精度をセット
        int countMax = 200;
        double rambdaMax = 1E5, threshold = 1E-10;
        if (precision == Precision.High)
        {
            countMax = 400;
            rambdaMax = 1E7;
            threshold = 1E-15;
        }
        else if (precision == Precision.Low)
        {
            countMax = 50;
            rambdaMax = 1E3;
            threshold = 1E-5;
        }
        #endregion

        var obs = obsValues.AsParallel();
        int valuesLength = obs.Count(), totalPrmsLength = functions.Sum(f => f.Length), funcLength = functions.Length;
        if (valuesLength == 0) return (null, null, double.PositiveInfinity);
        var weight = new DiagonalMatrix(valuesLength, valuesLength, obs.Select(o => o.w).ToArray());
        var rambda = 10.0;
        var alpha = new DMat(totalPrmsLength, totalPrmsLength);
        var beta = new DVec(totalPrmsLength);
        var rCur = new DVec(obs.Select(v => v.y - functions.Sum(f => f.GetValue(v.x))).ToArray());//現在の残差を計算
        var r2Cur = rCur * weight * rCur;//残差の二乗和を計算

        var renewAlpha = true;
        var exclude = new bool[totalPrmsLength];
        int successCount = -1;//連続成功回数(正)、あるいは連続失敗回数(負)を記録する。

        for (int count = 0; count < countMax && rambda < rambdaMax; count++)
        {
            if (renewAlpha)
            {
                var jacob = DMat.OfRowArrays(obs.Select(o =>
                {
                    var d = new double[totalPrmsLength];
                    for (int j = 0, n = 0; j < funcLength; n += functions[j].PrmLength, j++)
                        Buffer.BlockCopy(functions[j].GetDerivative(o.x), 0, d, n * 8, functions[j].PrmLength * 8);
                    return d;
                }).ToArray());

                var jacobW = (DMat)(weight * jacob);
                alpha = (DMat)jacob.TransposeThisAndMultiply(jacobW);//alpha行列を計算
                beta = rCur * jacobW;//betaベクトルを計算

                //偏微分値がゼロになってしまう時の対策
                exclude = alpha.ToRowArrays().Select(row => row.All(e => e == 0)).ToArray();
                if (exclude.Contains(true))
                {
                    if (exclude.Count(e => e) == beta.Count)
                        return (null, null, double.PositiveInfinity);
                    var betaList = beta.ToList();
                    for (int i = exclude.Length - 1; i >= 0; i--)
                        if (exclude[i])
                        {
                            betaList.RemoveAt(i);
                            alpha = (DMat)alpha.RemoveColumn(i);
                            alpha = (DMat)alpha.RemoveRow(i);
                        }
                    beta = new DVec([.. betaList]);
                }
            }

            var alphaTemp = alpha + DMat.OfDiagonalVector(alpha.Diagonal() * rambda);//alphaの対角成分に1+rambdaを掛ける。
            if (!alphaTemp.TryInverse(out Matrix alphaInv))//逆行列を計算
                return (null, null, double.PositiveInfinity); //失敗した場合は終了

            //deltaベクトルを計算
            var delta = (beta * alphaInv).ToList();
            //偏微分値がゼロになってしまう時の対策
            if (delta.Count != totalPrmsLength)
                for (int i = 0; i < exclude.Length; i++)
                    if (exclude[i])
                        delta.Insert(i, 0);

            //パラメータを変化させる
            for (int j = 0, n = 0; j < funcLength; n += functions[j].PrmLength, j++)
                functions[j].AddPrms(delta.Skip(n).Take(functions[j].PrmLength).ToArray());

            var rNew = new DVec(obs.Select(v => v.y - functions.Sum(f => f.GetValue(v.x))).ToArray());//新しいパラメータで残差を計算
            var r2New = rNew * weight * rNew; //残差の二乗和を計算

            renewAlpha = r2New < r2Cur;
            if (renewAlpha)
            {
                successCount = successCount < 0 ? 1 : successCount + 1;
                if ((r2Cur - r2New) / r2Cur < threshold)
                {
                    r2Cur = r2New;
                    break;//十分収束が進んだ時は終了
                }
                rCur = rNew;
                r2Cur = r2New;
                rambda = successCount > 2 ? rambda * RambdaCoeff1 : rambda;//2回連続で成功したら、Rambdaを減らす.
            }
            else
            {
                successCount = successCount > 0 ? -1 : successCount - 1;
                if (successCount < -3)//4回連続で失敗したら、取り合えず現状を採用.
                {
                    successCount = 1;
                    rCur = rNew;
                    r2Cur = r2New;
                    renewAlpha = true;
                }
                else
                {
                    rambda *= RambdaCoeff2;
                    foreach (var f in functions) f.RevertPrms();
                }
            }
        }
        return (functions.Select(f => f.Prms).ToArray(), null, r2Cur / obs.Sum(v => v.w * v.y * v.y));
    }

    private const double ln2 = 0.69314718055994530941723212145818;
    private const double sqrtln2 = 0.8325546111576977563531646448952;
    private const double pi = 3.1415926535897932384626433832795;
    private const double piInv = 0.31830988618379067153776752674503;
    private const double sqrtpi = 1.7724538509055160272981674833411;
    private const double ln2_pi = ln2 / pi;
    private const double c = 0.58740105196819947475170563927231;// = 4^(1/3) - 1

    #region 組み込み関数

    /// <summary>
    /// ローレンツ関数 1次元
    /// </summary>
    /// <param name="x">xの値</param>
    /// <param name="x0">xの中心値</param>
    /// <param name="h">半値半幅 </param>
    /// <param name="a">積分強度 </param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Lorenzian(in double x, in double x0, in double h, in double a) => a / (pi * h) / (1 + (x - x0) / h * (x - x0) / h);

    /// <summary>
    /// ローレンツ関数 2次元 (真円)
    /// </summary>
    /// <param name="x">xの値</param>
    /// <param name="y">yの値</param>
    /// <param name="x0">xの中心値</param>
    /// <param name="y0">yの中心値</param>
    /// <param name="h">半値半幅</param>
    /// <param name="a">積分強度 </param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Lorenzian(in double x, in double y, in double x0, in double y0, in double h, in double a)
        => a / (2 * pi * h * h) / (1 + c * (x - x0) / h * (x - x0) / h + c * (y - y0) / h * (y - y0) / h);

    /// <summary>
    /// ローレンツ関数 2次元 (楕円)
    /// </summary>
    /// <param name="x">xの値</param>
    /// <param name="y">yの値</param>
    /// <param name="x0">xの中心値</param>
    /// <param name="y0">yの中心値</param>
    /// <param name="h1">半値半幅1</param>
    /// <param name="h2">半値半幅2</param>
    /// <param name="t">h1の方向に回転する角度</param>
    /// <param name="a">積分強度 </param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Lorenzian(in double x, in double y, in double x0, in double y0, in double h1, in double h2, in double t, in double a)
    {
        double x1 = x - x0, y1 = y - y0; //中心へシフト
        double cos = Cos(t), sin = Sin(t);
        double X = x1 * cos + y1 * sin, Y = -x1 * sin + y1 * cos;
        return a / (2 * pi * h1 * h2) / (1 + c * X / h1 * X / h1 + c * Y / h2 * Y / h2);
    }

    /// <summary>
    /// ガウス関数 1次元
    /// </summary>
    /// <param name="x">xの値</param>
    /// <param name="x0">xの中心値</param>
    /// <param name="h">半値半幅 </param>
    /// <param name="a">積分強度 </param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Gaussian(in double x, in double x0, in double h, in double a) => a * sqrtln2 / sqrtpi / h * Math.Exp(-ln2 * (x - x0) / h * (x - x0) / h);

    /// <summary>
    /// ガウス関数 2次元 (真円)
    /// </summary>
    /// <param name="x">xの値</param>
    /// <param name="y">yの値</param>
    /// <param name="x0">xの中心値</param>
    /// <param name="y0">yの中心値</param>
    /// <param name="h">半値半幅</param>
    /// <param name="a">積分強度 </param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Gaussian(in double x, in double y, in double x0, in double y0, in double h, in double a)
        => a * ln2 / pi / h / h * Exp(-ln2 * ((x - x0) * (x - x0) + (y - y0) * (y - y0)) / h / h);

    /// <summary>
    /// ガウス関数 2次元 (楕円)
    /// </summary>
    /// <param name="x">xの値</param>
    /// <param name="y">yの値</param>
    /// <param name="x0">xの中心値</param>
    /// <param name="y0">yの中心値</param>
    /// <param name="h1">半値半幅1</param>
    /// <param name="h2">半値半幅2</param>
    /// <param name="t">h1の方向に回転する角度</param>
    /// <param name="a">積分強度 </param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Gaussian(in double x, in double y, in double x0, in double y0, in double h1, in double h2, in double t, in double a)
    {
        double x1 = x - x0, y1 = y - y0; //中心へシフト
        double cos = Cos(t), sin = Sin(t);
        double X = x1 * cos + y1 * sin, Y = -x1 * sin + y1 * cos;
        return a * ln2_pi / h1 / h2 * Exp(-ln2 * (X * X / h1 / h1 + Y * Y / h2 / h2));
    }

    /// <summary>
    /// 疑似フォークト関数 1次元
    /// </summary>
    /// <param name="x">xの値</param>
    /// <param name="x0">xの中心値</param>
    /// <param name="h">半値半幅 </param>
    /// <param name="eta">ガウス関数とローレンツ関数の割合 (r=1のとき、100% Lorentzian)</param>
    /// <param name="a">積分強度 </param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double PseudoVoigt(in double x, in double x0, in double h, in double eta, in double a)
        => eta * Lorenzian(x, x0, h, a) + (1 - eta) * Gaussian(x, x0, h, a);

    /// <summary>
    /// 疑似フォークト関数 2次元 (真円)
    /// </summary>
    /// <param name="x">xの値</param>
    /// <param name="y">yの値</param>
    /// <param name="x0">xの中心値</param>
    /// <param name="y0">yの中心値</param>
    /// <param name="h1">半値半幅1</param>
    /// <param name="h2">半値半幅2</param>
    /// <param name="eta">ガウス関数とローレンツ関数の割合 (r=1のとき、100% Lorentzian)</param>
    /// <param name="a">積分強度 </param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double PseudoVoigt(in double x, in double y, in double x0, in double y0, in double h, in double eta, in double a)
        => eta * Lorenzian(x, y, x0, y0, h, a) + (1 - eta) * Gaussian(x, y, x0, y0, h, a);

    /// <summary>
    /// 疑似フォークト関数 2次元 (楕円)
    /// </summary>
    /// <param name="x">xの値</param>
    /// <param name="y">yの値</param>
    /// <param name="x0">xの中心値</param>
    /// <param name="y0">yの中心値</param>
    /// <param name="hx">半値半幅1</param>
    /// <param name="hy">半値半幅2</param>
    /// <param name="t">h1の方向に回転する角度</param>
    /// <param name="eta">ガウス関数とローレンツ関数の割合 (r=1のとき、100% Lorentzian)</param>
    /// <param name="a">積分強度 </param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double PseudoVoigt(in double[] x, in double x0, in double y0, in double hx, in double hy, in double t, in double eta, in double a)
    {
        double xShift = x[0] - x0, yShift = x[1] - y0; //中心へシフト
        double cos = Cos(t), sin = Sin(t);
        double xRot = xShift * cos + yShift * sin, yRot = -xShift * sin + yShift * cos;
        double hxInv = 1 / hx, hyInv = 1 / hy;
        var x2plusY2 = xRot * xRot * hxInv * hxInv + yRot * yRot * hyInv * hyInv;
        var g = ln2 * Exp(-ln2 * x2plusY2);
        var l = 1 / ((2 + 2 * c * x2plusY2) * Sqrt(1 + c * x2plusY2));
        return a * (eta * l + (1 - eta) * g) * piInv * hxInv * hyInv;
        /*
        //C++ネイティブで書いてもあまり速くならなかった。
        var results = new double[1];
        _PseudoVoigt(x, x0, y0, hx, hy, t, eta, a, results);
        return results[0];
        */
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] PseudoVoigtDiff(in double[] x, in double x0, in double y0, in double hx, in double hy, in double t, in double eta, in double a)
    {
        double cos = Cos(t), sin = Sin(t);
        double hxInv = 1 / hx, hyInv = 1 / hy;
        double hx2Inv = hxInv * hxInv, hy2Inv = hyInv * hyInv;

        double xShift = x[0] - x0, yShift = x[1] - y0; //中心へシフト
        double xRot = xShift * cos + yShift * sin, yRot = -xShift * sin + yShift * cos;
        double xRot2hx2Inv = xRot * xRot * hx2Inv, yRot2hy2Inv = yRot * yRot * hy2Inv;
        var x2pY2 = xRot2hx2Inv + yRot2hy2Inv;

        double lo = 1 + c * x2pY2, sqLo = Sqrt(lo);
        var l = 1 / (lo * sqLo * 2);
        var g = ln2 * Exp(-ln2 * x2pY2);

        var d1 = piInv * hxInv * hyInv;
        var d2 = d1 * (eta * l + (1 - eta) * g);
        var d3 = a * d1 * (3 * eta * l * c / lo + 2 * ln2 * (1 - eta) * g);

        return
        [
            d3 * (xRot * cos * hx2Inv - yRot * sin * hy2Inv),//x0
            d3 * (xRot * sin * hx2Inv + yRot * cos * hy2Inv),//y0
            (d3 * xRot2hx2Inv - a * d2) * hxInv,//hx
            (d3 * yRot2hy2Inv - a * d2) * hyInv,//hy
            d3 * ( hy2Inv - hx2Inv) * xRot * yRot,//theta
            a * d1 * (l - g) , //eta
            d2 //a
        ];

        /*
        //C++ネイティブで書いてもあまり速くならなかった。呼び出しのオーバヘッドが時間かかってそう。
        var results = new double[7];
        _PseudoVoigtDiff(x, x0, y0, hx, hy, t, eta, a, results);
        return results;
        */
    }

    [DllImport("Crystallography.Native.dll")]
    private static extern void _PseudoVoigtDiff(in double[] x, in double x0, in double y0, in double hx, in double hy, in double t, in double eta, in double a, in double[] results);

    [DllImport("Crystallography.Native.dll")]
    private static extern void _PseudoVoigt(in double[] x, double x0, in double y0, in double hx, in double hy, in double t, in double eta, in double a, in double[] results);

    #endregion 組み込み関数
}