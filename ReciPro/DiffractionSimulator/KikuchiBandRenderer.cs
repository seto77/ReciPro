// 260805Cl 新規作成: 菊池動力学バンドの投影・合成レンダラ (設計正本 = ReciPro_菊池線動力学化設計.md §4, §5)。
// 物理 (KikuchiProfileCalculator) は WinForms 非依存の Crystallography 側にあり、本クラスは
// 「符号付き float バッファへ c_total = Σ_g c_g を加算 → 一度だけ tanh → E/D 色 + |m| 不透明度の ARGB 変換」
// (設計 §4。バンド別アルファ逐次合成は描画順依存になるため不可) だけを担う。
//
// 検出器座標 (x, y) ⇔ 方向の規約: d̂ = normalize(x, y, +L), L = CameraLength2。
// この規約は KikuchiCheck geom テストで既存 DrawKikuchiLine の双曲線と residual ~1e-16 で一致確認済み。
// 検出器 tilt (Tau) は呼び出し側が ĝ に Rot(axis(Phi), −Tau) を掛けて渡す (DrawKikuchiLine の vec2 と同じ扱い)。

using Crystallography;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ReciPro;

public static class KikuchiBandRenderer
{
    /// <summary>1 バンド分の入力: プロファイルと (tilt 込み) 回転済み単位法線 ĝ2</summary>
    public readonly record struct BandInput(KikuchiBandProfile Profile, Vector3DBase GHat2);

    /// <summary>
    /// スクリーン全面のバンド合成 Bitmap (32bppArgb) を作る。
    /// det00 / detDx / detDy: スクリーン画素 (0,0) の検出器座標と、画素 +x / +y あたりの検出器座標の増分 (アフィン)。
    /// scale ≤ 0 で auto スケール (表示バンド全体の |c| の 98.5 パーセンタイル、設計 §4)。usedScale に採用値が返る。
    /// contrast: 1 が基準 (トラックバー 50/50)。
    /// gamma: 設計 §4 の m = sign(x)·|tanh x|^{1/γ}。菊池プロファイルはバンド端スパイクが内部の 10-60 倍
    /// あるため (KikuchiCheck smoke 実測)、γ=1 だと端だけ飽和し内部の濃淡が消える。既定 2.5 (§9-7 作者調整枠)。
    /// </summary>
    public static Bitmap Render(IReadOnlyList<BandInput> bands, int width, int height,
        (double X, double Y) det00, (double X, double Y) detDx, (double X, double Y) detDy,
        double cameraLength, double scale, double contrast, double gamma, Color excess, Color deficient, out double usedScale)
    {
        var buf = new float[width * height];
        var bandArr = bands.Where(b => b.Profile.Valid).ToArray();
        double L = cameraLength;

        Parallel.For(0, height, py =>
        {
            double rowX = det00.X + detDy.X * py, rowY = det00.Y + detDy.Y * py;
            int o = py * width;
            for (int px = 0; px < width; px++)
            {
                double dx = rowX + detDx.X * px, dy = rowY + detDx.Y * px;
                var inv = 1.0 / Math.Sqrt(dx * dx + dy * dy + L * L);
                double sum = 0;
                foreach (var band in bandArr)
                {
                    var gh = band.GHat2;
                    var sinTp = -(gh.X * dx + gh.Y * dy + gh.Z * L) * inv; // sinθ' = −ĝ·d̂
                    sum += band.Profile.Interpolate(sinTp / band.Profile.SinThetaB);
                }
                buf[o + px] = (float)sum;
            }
        });

        usedScale = scale > 0 ? scale : AutoScale(buf);

        // tanh → E/D 色 + |m| 不透明度 (背景との合成は GDI+ のアルファに任せる)
        var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
        var data = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
        try
        {
            var pixels = new int[width * height];
            int exR = excess.R, exG = excess.G, exB = excess.B, deR = deficient.R, deG = deficient.G, deB = deficient.B;
            var k = contrast / Math.Max(usedScale, 1e-30);
            var invGamma = 1.0 / Math.Max(gamma, 1e-3);
            Parallel.For(0, height, py =>
            {
                int o = py * width;
                for (int px = 0; px < width; px++)
                {
                    var v = buf[o + px];
                    if (v == 0) continue; // 透明のまま
                    var m = Math.Pow(Math.Abs(Math.Tanh(v * k)), invGamma); // 設計 §4: |tanh|^{1/γ} で端スパイクと内部濃淡を両立
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

    /// <summary>|c| の 98.5 パーセンタイル (max 正規化はスパイクに弱いため不採用。設計 §4)。標本は最大 10 万点に間引く</summary>
    private static double AutoScale(float[] buf)
    {
        int stride = Math.Max(1, buf.Length / 100_000);
        var samples = new List<float>(buf.Length / stride + 1);
        for (int i = 0; i < buf.Length; i += stride)
        {
            var v = Math.Abs(buf[i]);
            if (v > 1e-12f)
                samples.Add(v);
        }
        if (samples.Count == 0)
            return 1;
        samples.Sort();
        var idx = Math.Min(samples.Count - 1, (int)(samples.Count * 0.985));
        return Math.Max(samples[idx], 1e-30);
    }
}
