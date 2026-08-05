// 260805Cl 新規作成: 菊池動力学バンドの投影・合成レンダラ (設計正本 = ReciPro_菊池線動力学化設計.md §4, §5)。
// 物理 (KikuchiProfileCalculator) は WinForms 非依存の Crystallography 側にあり、本クラスは
// 「符号付き float バッファへ c_total = Σ_g c_g を加算 → 一度だけ選択スケール (Linear/Log/Tanh) で圧縮 →
// E/D 色 + |m| 不透明度の ARGB 変換」(設計 §4。バンド別アルファ逐次合成は描画順依存になるため不可) だけを担う。
//
// 検出器座標 (x, y) ⇔ 方向の規約: d̂ = normalize(−x, +y, +L), L = CameraLength2。
// DiffractionSimulator は「蛍光板を試料側からのぞき込む」座標系 (作者説明。EBSD のカメラ視点と鏡像関係) のため、
// 素朴な (+x, +y, +L) に対して x が反転する。KikuchiCheck geom テストで既存 DrawKikuchiLine の双曲線と
// residual ~2e-16 で一致確認済み (等価表現: d̂ = norm(+x, −y, −L) と −sinθ_B)。
// 検出器 tilt (Tau) は呼び出し側が ĝ に Rot(axis(Phi), −Tau) を掛けて渡す (DrawKikuchiLine の vec2 と同じ扱い)。

using Crystallography;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ReciPro;

public static class KikuchiBandRenderer
{
    /// <summary>1 バンド分の入力: プロファイルと (tilt 込み) 回転済み単位法線 ĝ2</summary>
    public readonly record struct BandInput(KikuchiBandProfile Profile, Vector3DBase GHat2);

    /// <summary>
    /// 260806Cl 追加 (作者提案): 強度→不透明度の圧縮カーブ。x = |c|·contrast/scale として
    /// Linear = min(x, 1) (素直に飽和 = CCD 的ハードクリップ) / Log = log(1+9x)/ln10 を 1 で clip
    /// (x=1 で丁度 1。既存の log スケール画像表示と同思想) / Tanh = tanh(x) (設計 §4 の既定)
    /// </summary>
    public enum ScaleMode { Linear, Log, Tanh }

    /// <summary>バンドごとの前計算定数 (260806Cl /simplify: 内側ループから除算・メソッド呼び出し・参照追跡を排除)</summary>
    private struct BandData
    {
        public double Gx, Gy, GzL;     // 単位法線成分 (GzL = Gz·L)
        public double SinLo, SinHi;    // sinθ' の有効範囲 (範囲外は寄与 0 → 早期棄却)
        public double K1, K0;          // 格子座標 t = sinθ'·K1 + K0 (= (sinθ'/sinθ_B − x0)/Δx)
        public double[] C;
    }

    /// <summary>
    /// スクリーン全面のバンド合成 Bitmap (32bppArgb) を作る。
    /// det00 / detDx / detDy: スクリーン画素 (0,0) の検出器座標と、画素 +x / +y あたりの検出器座標の増分 (アフィン)。
    /// scale ≤ 0 で auto スケール (表示バンド全体の |c| の 98.5 パーセンタイル、設計 §4)。usedScale に採用値が返る。
    /// contrast: 1 が基準 (トラックバー 50/50)。
    /// gamma: 設計 §4 の m = sign(x)·|m₀|^{1/γ}。現状の呼び出し側は 1.0 固定 (素の圧縮カーブ。§9-7 作者調整枠)。
    /// </summary>
    public static Bitmap Render(IReadOnlyList<BandInput> bands, int width, int height,
        (double X, double Y) det00, (double X, double Y) detDx, (double X, double Y) detDy,
        double cameraLength, double scale, double contrast, double gamma, ScaleMode scaleMode, Color excess, Color deficient, out double usedScale)
    {
        var buf = new float[width * height];
        double L = cameraLength;

        //260806Cl /simplify: バンド定数を平坦化 (旧: ピクセル×バンドごとに Profile.Interpolate 呼び出し + SinThetaB 除算 + LINQ フィルタ)
        var bandData = new BandData[bands.Count];
        int nBands = 0;
        foreach (var b in bands)
        {
            var p = b.Profile;
            if (!p.Valid || p.X.Length < 2 || p.C.Length != p.X.Length) //260806Cl /simplify2 (F-6): C/X 長不一致は境界ガードが反転するため入口で拒否
                continue;
            var step = p.X[1] - p.X[0];
            bandData[nBands++] = new BandData
            {
                Gx = b.GHat2.X, Gy = b.GHat2.Y, GzL = b.GHat2.Z * L,
                SinLo = p.X[0] * p.SinThetaB, SinHi = p.X[^1] * p.SinThetaB,
                K1 = 1.0 / (p.SinThetaB * step), K0 = -p.X[0] / step,
                C = p.C,
            };
        }
        var nb = nBands;

        Parallel.For(0, height, py =>
        {
            double rowX = det00.X + detDy.X * py, rowY = det00.Y + detDy.Y * py;
            int o = py * width;
            for (int px = 0; px < width; px++)
            {
                double dx = rowX + detDx.X * px, dy = rowY + detDx.Y * px;
                var inv = 1.0 / Math.Sqrt(dx * dx + dy * dy + L * L);
                double sum = 0;
                for (int bi = 0; bi < nb; bi++)
                {
                    ref readonly var bd = ref bandData[bi];
                    //var sinTp = -(gh.X * dx + gh.Y * dy + gh.Z * L) * inv; //260805Cl 変更前: d̂=(+x,+y,+L) は蛍光板座標系と左右鏡像だった (作者実機指摘)
                    var sinTp = (bd.Gx * dx - bd.Gy * dy - bd.GzL) * inv; // sinθ' = −ĝ·d̂, d̂ = norm(−x, +y, +L)
                    if (sinTp <= bd.SinLo || sinTp >= bd.SinHi)
                        continue; // 帯域外 (大半のピクセル) は補間せず棄却
                    var t = sinTp * bd.K1 + bd.K0;
                    int i = (int)t;
                    if ((uint)i >= (uint)(bd.C.Length - 1))
                        continue;
                    var f = t - i;
                    sum += bd.C[i] * (1 - f) + bd.C[i + 1] * f;
                }
                buf[o + px] = (float)sum;
            }
        });

        usedScale = scale > 0 ? scale : AutoScale(buf);

        // 選択スケールで圧縮 → E/D 色 + |m| 不透明度 (背景との合成は GDI+ のアルファに任せる)。
        // 260806Cl /simplify2 (M1): 例外時は Bitmap を破棄してから再スロー (呼び出し側の using に届かないため)
        var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
        try
        {
            var data = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            try
            {
                var pixels = new int[width * height];
                int exR = excess.R, exG = excess.G, exB = excess.B, deR = deficient.R, deG = deficient.G, deB = deficient.B;
                var k = contrast / Math.Max(usedScale, 1e-30);
                var invGamma = 1.0 / Math.Max(gamma, 1e-3);
                const double invLn10 = 0.43429448190325176; // 1/ln(10)。log(1+9x)/ln10 は x=1 で丁度 1
                Parallel.For(0, height, py =>
                {
                    int o = py * width;
                    for (int px = 0; px < width; px++)
                    {
                        var v = buf[o + px];
                        if (v == 0 || !float.IsFinite(v)) continue; // 透明のまま (260805Cl 非有限値ガード追加)
                        var x = Math.Abs(v * k);
                        var m = scaleMode switch //260806Cl スケール選択 (作者提案)
                        {
                            ScaleMode.Linear => Math.Min(x, 1.0),
                            ScaleMode.Log => Math.Min(Math.Log(1 + 9 * x) * invLn10, 1.0),
                            _ => Math.Tanh(x), // 設計 §4 の既定
                        };
                        if (invGamma != 1.0)
                            m = Math.Pow(m, invGamma);
                        int a = (int)(m * 255 + 0.5);
                        if (a > 255) a = 255;
                        pixels[o + px] = v > 0
                            ? (a << 24) | (exR << 16) | (exG << 8) | exB
                            : (a << 24) | (deR << 16) | (deG << 8) | deB;
                    }
                });
                Marshal.Copy(pixels, 0, data.Scan0, pixels.Length);
            }
            finally { bmp.UnlockBits(data); }
            return bmp;
        }
        catch { bmp.Dispose(); throw; } //260806Cl /simplify2 (M1): 例外時は呼び出し側の using に届かないため自前で破棄
    }

    /// <summary>|c| の 98.5 パーセンタイル (max 正規化はスパイクに弱いため不採用。設計 §4)。標本は最大 10 万点に間引く</summary>
    private static double AutoScale(float[] buf)
    {
        int stride = Math.Max(1, buf.Length / 100_000);
        var samples = new List<float>(buf.Length / stride + 1);
        for (int i = 0; i < buf.Length; i += stride)
        {
            var v = Math.Abs(buf[i]);
            if (v > 1e-12f && float.IsFinite(v)) // 260805Cl 非有限値ガード追加 (Inf がスケールを壊すのを防ぐ)
                samples.Add(v);
        }
        if (samples.Count == 0)
            return 1;
        samples.Sort();
        var idx = Math.Min(samples.Count - 1, (int)(samples.Count * 0.985));
        return Math.Max(samples[idx], 1e-30);
    }
}
