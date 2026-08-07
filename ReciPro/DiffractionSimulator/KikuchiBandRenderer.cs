// 260805Cl 新規作成: 菊池動力学バンドの投影・合成レンダラ (設計正本 = ReciPro_菊池線動力学化設計.md §4, §5)。
// 物理 (KikuchiProfileCalculator) は WinForms 非依存の Crystallography 側にあり、本クラスは
// 「符号付き float バッファへ c_total = Σ_g c_g を加算 → 一度だけ選択スケール (Linear/Log/Tanh) で圧縮 →
// E/D 色 + |m| 不透明度の ARGB 変換」(設計 §4。バンド別アルファ逐次合成は描画順依存になるため不可) だけを担う。
//
// 検出器座標 (x, y) ⇔ 方向の規約: d̂ = normalize(+x, −y, −L), L = CameraLength2。
// これはスポット投影 ConvertReciprocalToDetector (pt = L·(gX, −gY)/(k − gZ)) の逆写像 = 物理の出射方向そのもの。
// ⚠260806Cl 符号確定: 旧規約 d̂ = norm(−x, +y, +L) は物理方向の −1 倍で、バンド「位置」(±sinθ_B, 対称) は
// 完全に一致するがプロファイルの E/D 非対称だけが画面上で鏡映される (作者スクリーンショット
// 「000 側の黄線の方が明るい」= Omoto の記述と逆、で発覚。KikuchiCheck ed 診断で鏡映を数値確定)。
// KikuchiCheck geom テストで既存 DrawKikuchiLine の双曲線と residual ~2e-16 で一致確認済み
// (legacy の g 用菊池線 = g スポットを通る側 = 本規約の sinθ' = −sinθ_B, プロファイル x = −1)。
// 検出器 tilt (Tau) は呼び出し側が ĝ に Rot(axis(Phi), −Tau) を掛けて渡す (DrawKikuchiLine の vec2 と同じ扱い)。

using Crystallography;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
//using System.Runtime.InteropServices; //260807Cl 削除: Marshal.Copy 廃止 (BitmapData へ直接書くようにした) で不要
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

    // 260807Cl 追加: フレーム間バッファ再利用 (perf backlog の 1 番)。
    // BDN 実測 (ReciPro.Benchmarks の KikuchiRenderBenchmark) では 1200×1200 の 1 フレームで
    // 11.0 MB を確保し Gen0/Gen1/Gen2 がすべて回っていた (float[] buf 5.8MB + int[] pixels 5.8MB)。
    // 生産の呼び出しは UI スレッド 1 本だが、[ThreadStatic] にしておけばどのスレッドから呼ばれても
    // ロック無しで安全 (代わりに呼んだスレッドごとに 1 組保持する)。
    // ⚠キャッシュは要求サイズ**以上**の長さがあり得るので、長さは必ず width*height を明示的に使うこと。
    // ARGB 側は BitmapData へ直接書くので常駐するのはこの 2 本だけ (画素あたり float 1 個 + 標本 1/stride 個)。
    [ThreadStatic] private static float[] bufCache;
    [ThreadStatic] private static float[] sampleCache;

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
        //var buf = new float[width * height]; //260807Cl 変更前: 毎フレーム確保していた
        int nPix = width * height;
        var buf = bufCache != null && bufCache.Length >= nPix ? bufCache : (bufCache = new float[nPix]);
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
                //260807Cl /simplify: 内側ループの符号をここへ畳んだ。⚠これは最適化ではなく符号の表現替え —
                //旧 (Gx*dx − Gy*dy − GzL) は 3 項すべての符号が反転して (Gx*dx + Gy*dy + GzL) になる。
                //つまり sinθ' は旧実装のちょうど −1 倍で、これが 260806Cl の E/D 鏡映修正の実体
                //(d̂ = norm(−x,+y,+L) → norm(+x,−y,−L))。命令数は 3 乗算 + 2 加減で旧と同じ。
                //バンド位置は ±sinθ_B 対称なので不変、変わるのは E/D 非対称の向きだけ
                Gx = -b.GHat2.X, Gy = b.GHat2.Y, GzL = b.GHat2.Z * L,
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
                    //var sinTp = (bd.Gx * dx - bd.Gy * dy - bd.GzL) * inv; //260806Cl 変更前: d̂=norm(−x,+y,+L) = −(物理出射方向) で E/D 非対称が鏡映されていた (ed 診断で確定)
                    var sinTp = (bd.Gx * dx + bd.Gy * dy + bd.GzL) * inv; //sinθ' = −ĝ·d̂ (Gx は符号を畳み込み済み。上の BandData 構築を参照)
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

        usedScale = scale > 0 ? scale : AutoScale(buf, nPix); //260807Cl: 再利用バッファは長すぎ得るので有効長を渡す

        // 選択スケールで圧縮 → E/D 色 + |m| 不透明度 (背景との合成は GDI+ のアルファに任せる)。
        // 260806Cl /simplify2 (M1): 例外時は Bitmap を破棄してから再スロー (呼び出し側の using に届かないため)
        var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
        try
        {
            var data = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            try
            {
                //260807Cl 変更: 中間の int[] を廃し、ロック済み BitmapData へ直接書く。
                //  var pixels = new int[width * height];
                //  ... pixels[o + px] = ...;              //透明画素は書かずに「確保直後の 0」に頼っていた
                //  Marshal.Copy(pixels, 0, data.Scan0, pixels.Length);
                //利点は 3 つ: (a) 画面画素数ぶんの int[] を持たなくて済む (再利用キャッシュの常駐が半分になる)、
                //(b) 1 フレームあたり width*height*4 バイトの memcpy が 1 回消える、
                //(c) data.Stride を素直に使うので「Stride == width*4」の暗黙の仮定が要らなくなる。
                //⚠直接書くので「透明画素は書かない」は成立しない — 下のループで 0 を明示的に書く
                var scan0 = data.Scan0; // ポインタはラムダに捕捉できないので IntPtr で渡して内側で変換する
                int stride4 = data.Stride / 4; // 32bppArgb なので Stride は必ず 4 の倍数
                int exR = excess.R, exG = excess.G, exB = excess.B, deR = deficient.R, deG = deficient.G, deB = deficient.B;
                var k = contrast / Math.Max(usedScale, 1e-30);
                var invGamma = 1.0 / Math.Max(gamma, 1e-3);
                const double invLn10 = 0.43429448190325176; // 1/ln(10)。log(1+9x)/ln10 は x=1 で丁度 1
                Parallel.For(0, height, py =>
                {
                    int o = py * width;
                    unsafe
                    {
                        int* dst = (int*)scan0 + py * stride4;
                        for (int px = 0; px < width; px++)
                        {
                            var v = buf[o + px];
                            if (v == 0 || !float.IsFinite(v)) { dst[px] = 0; continue; } // 透明 (260805Cl 非有限値ガード追加)
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
                            dst[px] = v > 0
                                ? (a << 24) | (exR << 16) | (exG << 8) | exB
                                : (a << 24) | (deR << 16) | (deG << 8) | deB;
                        }
                    }
                });
            }
            finally { bmp.UnlockBits(data); }
            return bmp;
        }
        catch { bmp.Dispose(); throw; } //260806Cl /simplify2 (M1): 例外時は呼び出し側の using に届かないため自前で破棄
    }

    /// <summary>
    /// |c| の 98.5 パーセンタイル (max 正規化はスパイクに弱いため不採用。設計 §4)。標本は最大 10 万点に間引く。
    /// 260807Cl: length は buf の有効長 (再利用バッファは要求より長いことがある)。
    /// 標本配列も再利用し、全ソートを quickselect へ置き換えた (BDN 実測でここが auto スケール時の
    /// 追加 3.6ms の主因だった)。返す値は「全ソートして添字を引く」のと**同一** — idx 番目に小さい要素そのもの。
    /// </summary>
    //private static double AutoScale(float[] buf) //260807Cl 変更前のシグネチャ
    private static double AutoScale(float[] buf, int length)
    {
        int stride = Math.Max(1, length / 100_000);
        int cap = length / stride + 1;
        var samples = sampleCache != null && sampleCache.Length >= cap ? sampleCache : (sampleCache = new float[cap]);
        int count = 0;
        for (int i = 0; i < length; i += stride)
        {
            var v = Math.Abs(buf[i]);
            if (v > 1e-12f && float.IsFinite(v)) // 260805Cl 非有限値ガード追加 (Inf がスケールを壊すのを防ぐ)
                samples[count++] = v;
        }
        if (count == 0)
            return 1;
        //samples.Sort(); var idx = ...; return Math.Max(samples[idx], 1e-30); //260807Cl 変更前: 10 万点の全ソート (O(n log n))
        var idx = Math.Min(count - 1, (int)(count * 0.985));
        return Math.Max(NthSmallest(samples, count, idx), 1e-30);
    }

    /// <summary>
    /// a[0..count) のうち idx 番目に小さい値 (0 起点)。全ソートして a[idx] を読むのと同じ値を期待 O(n) で返す。
    /// 副作用として a[0..count) は並べ替わる (標本配列は毎回作り直すので問題ない)。260807Cl 追加。
    /// ピボットは median-of-three (整列済み・逆順入力での O(n²) 退化を防ぐ)。全要素が等しい入力でも
    /// Hoare 分割は中央付近で交差するため範囲は必ず縮み、停止する。
    /// </summary>
    private static float NthSmallest(float[] a, int count, int idx)
    {
        int lo = 0, hi = count - 1;
        while (lo < hi)
        {
            int mid = lo + ((hi - lo) >> 1);
            if (a[mid] < a[lo]) (a[lo], a[mid]) = (a[mid], a[lo]);
            if (a[hi] < a[lo]) (a[lo], a[hi]) = (a[hi], a[lo]);
            if (a[hi] < a[mid]) (a[mid], a[hi]) = (a[hi], a[mid]);
            var pivot = a[mid];
            int i = lo, j = hi;
            while (i <= j)
            {
                while (a[i] < pivot) i++;
                while (a[j] > pivot) j--;
                if (i <= j)
                {
                    (a[i], a[j]) = (a[j], a[i]);
                    i++; j--;
                }
            }
            if (idx <= j) hi = j;
            else if (idx >= i) lo = i;
            else return a[idx]; // ピボットと等しい帯の中 = そこが答え
        }
        return a[lo];
    }
}
