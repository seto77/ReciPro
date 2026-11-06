# CPU 固有値分解 backend の再調査 (2026-11-06)

261106Ch。対象は ReciPro 配下の正本と、`tools/EvdBackendStudy261106/cpu_*`・`CpuEvdBackend.cs` の隔離 prototype。本番 backend の切替は本書作成時点で未実施。既存の最新 Eigen snapshot は維持する。

## 調べるべき差

従来の「Eigen と MKL の比較」だけでは、ライブラリの差、固有値分解アルゴリズムの差、余分な行列出力、workspace の確保が混ざる。これらを分けて測る。

| 方式 | 実際に呼ぶ処理 | workspace / 行列出力 |
|---|---|---|
| 現行相当 Eigen | fresh `ComplexEigenSolver<MatrixXcd>` | 各呼出で solver を生成 |
| 再利用 Eigen | 同じ `ComplexEigenSolver` の `compute` | context ごとに solver を保持 |
| direct LAPACKE | `LAPACKE_zgeev_work('N','V')` | 最適長を一度 query、以後再利用。入力複製を測定に含む |
| LAPACKE Schur | `LAPACKE_zgees_work('V','N')` → `LAPACKE_ztrevc_work('R','B')` | 最適長を query、以後再利用。Schur ベクトルを右固有ベクトルへ戻す |
| 既存 MathNet MKL | export `z_eigen(false,...)` | 内部で行列複製・workspace 確保、さらに対角行列 D を出力 |

MathNet の complex nonsymmetric EVD は **ZGEEV ではない**。公式 `eigen_complex_factor` は ZGEES → ZTREVC である。したがって direct ZGEEV と既存 MKL の差を全て「C# overhead」や「workspace 再利用」の効果とは解釈できない。[MathNet native source](https://raw.githubusercontent.com/mathnet/mathnet-numerics/master/src/NativeProviders/Common/lapack.cpp)

ZGEEV は複素非対称行列の固有値と右固有ベクトルを求める。ZGEES が出す Schur ベクトルだけを Bloch 固有ベクトルとして使うのは誤りで、ZTREVC による back transform が必要。ZGEEV と ZGEES の balancing、固有ベクトルの正規化、順序は同じとは限らない。比較対象は生のベクトル要素ではなく、残差と再構成波動である。[LAPACK ZGEEV](https://www.netlib.org/lapack/explore-html/d4/d68/group__geev_ga8656466cd9da86a0b3b6966e49a7ce40.html)、[LAPACKE 宣言](https://raw.githubusercontent.com/Reference-LAPACK/lapack/master/LAPACKE/include/lapacke.h)

## 源コードから確認した算法の差

ここでは正本の Eigen commit `5a19218d5a28bfc38ca6b1475ea040c8f73257ba` と、OpenBLAS **v0.3.34 固定 tag** に含まれる LAPACK ソースを比較した。最新 oneMKL の内部実装は公開 source から確認できない。LAPACK API を公開しているという理由だけで、MKL 内部が下表の reference LAPACK と同じ分岐・block size・AED 条件を使うとは断定しない。

| 段階 | 正本 Eigen complex EVD | OpenBLAS v0.3.34 の ZGEEV |
|---|---|---|
| スケーリング | 行列全体の安全な共通スケール変換。一般の対角 balancing はない | 共通スケール保護に加えて `ZGEBAL('B')` による置換と対角 balancing、最後に `ZGEBAK('B','R')` で復元 |
| Hessenberg 化 | Householder を 1 列ずつ左右に適用。Q の生成には別途 Householder sequence を使う | `ZGEHRD` は条件に応じて blocked `ZLAHR2`、`ZGEMM`、`ZLARFB`。末尾または小 workspace は `ZGEHD2` |
| Schur 化 | 末尾 2×2 から 1 個の shift を選び、Givens で 1 個の bulge を追跡する QR | `ZHSEQR` が次元等により `ZLAHQR` / `ZLAQR0` を選択。後者は AED と small-bulge multishift QR を持つ |
| 固有ベクトル | 三角行列への逐次 back-substitution、最後に `U*X` の GEMM、列を正規化・固有値の絶対値で整列 | `ZTREVC3`。十分な workspace では back transform を block 化して GEMM、少ない場合は GEMV。最後に balancing を戻し、norm と位相を正規化 |

Eigen の実装根拠は `HessenbergDecomposition.h:300`、`ComplexSchur.h:312,472`、`ComplexEigenSolver.h:289`。最新 snapshot は Hessenberg の in-place storage と共通スケール保護を改善しているが、complex Schur の主反復は single-shift Givens のままである。これを「Eigen 全体が BLAS3 を使わない」と一般化してはならない。固有ベクトルの最後の積と Q の組立てには別の計算構造がある。[固定版 ComplexSchur](https://gitlab.com/libeigen/eigen/-/blob/5a19218d5a28bfc38ca6b1475ea040c8f73257ba/Eigen/src/Eigenvalues/ComplexSchur.h)、[固定版 ComplexEigenSolver](https://gitlab.com/libeigen/eigen/-/blob/5a19218d5a28bfc38ca6b1475ea040c8f73257ba/Eigen/src/Eigenvalues/ComplexEigenSolver.h)

ZGEEV の実際の呼出列は `ZGEBAL → ZGEHRD → ZUNGHR → ZHSEQR → ZTREVC3 → ZGEBAK`。吸収を含む非正規行列でも、右固有ベクトルの定義は同じである。balancing は物理近似ではなく相似変換だが、有限精度での残差・固有ベクトル条件・伝播誤差は別途照合する必要がある。[v0.3.34 ZGEEV](https://raw.githubusercontent.com/OpenMathLib/OpenBLAS/v0.3.34/lapack-netlib/SRC/zgeev.f)、[blocked Hessenberg ZGEHRD](https://raw.githubusercontent.com/OpenMathLib/OpenBLAS/v0.3.34/lapack-netlib/SRC/zgehrd.f)

`ZHSEQR` は `ILAENV(12,...)` の境界を用いるため、全 N で multishift 経路とは限らない。`ZLAQR0` は末尾 window で収束した固有値を早期に切り離す `ZLAQR3` を呼び、残りに `ZLAQR5` の複数 shift sweep を適用する。後者は reflector を蓄積し、対角から離れた更新を GEMM にまとめられる。したがって EVD の速度差には、反復数とメモリアクセスの差があり得る。個別の ReciPro 行列で何回の AED / sweep が発生したかは今回計測していない。[ZHSEQR](https://raw.githubusercontent.com/OpenMathLib/OpenBLAS/v0.3.34/lapack-netlib/SRC/zhseqr.f)、[ZLAQR0](https://raw.githubusercontent.com/OpenMathLib/OpenBLAS/v0.3.34/lapack-netlib/SRC/zlaqr0.f)、[ZLAQR3](https://raw.githubusercontent.com/OpenMathLib/OpenBLAS/v0.3.34/lapack-netlib/SRC/zlaqr3.f)、[ZLAQR5](https://raw.githubusercontent.com/OpenMathLib/OpenBLAS/v0.3.34/lapack-netlib/SRC/zlaqr5.f)

旧 MathNet の公開 wrapper は `ZGEES('V','N') → ZTREVC('R','B')` であり、この違いは allocation だけではない。reference ZGEES の balancing は `'P'` (置換のみ)、ZTREVC は各列の back transform を GEMV で行う。reference ZGEEV の ZTREVC3 は workspace が十分なら複数列をまとめて GEMM にする。今回の algorithm 2 は旧 wrapper と同じ driver 構成を新 runtime の work API で呼ぶ比較用で、algorithm 1 との比較にも balancing と back transform の差が残る。[v0.3.34 ZGEES](https://raw.githubusercontent.com/OpenMathLib/OpenBLAS/v0.3.34/lapack-netlib/SRC/zgees.f)、[ZTREVC](https://raw.githubusercontent.com/OpenMathLib/OpenBLAS/v0.3.34/lapack-netlib/SRC/ztrevc.f)、[ZTREVC3](https://raw.githubusercontent.com/OpenMathLib/OpenBLAS/v0.3.34/lapack-netlib/SRC/ztrevc3.f)

MathNet DLL の ABI は export と実動作で確認したが、その旧バイナリを生成した厳密な upstream commit は特定していない。公開 wrapper source の関数名・引数と今回の呼出は一致し、入力非破壊も実測した。内部 MKL kernel の正確な実装を wrapper source だけから復元したという意味ではない。

一次資料の取得元 URL、取得時刻、SHA256 と snapshot は `tools/EvdBackendStudy261106/cpu_sources/` の `cpu_sources_manifest.json` に保存した。書誌一覧 `C:/Users/seto/source/repos/references/README.md` には今回の LAPACK driver / AED に直接対応する項目を見つけられなかったため、ライブラリ自身の source を主根拠とした。

### Eigen 公式の October 2026 benchmark が示す範囲

[公式 Zen 5 比較](https://libeigen.gitlab.io/benchmarks/2026-09-zen5-blas-lapack/) は Ryzen AI MAX+ 395、WSL2、GCC 15.2、1 thread。公表 GEEV 表は **実数 float/double の random dense 行列**で、complex128 物理行列ではない。N=192–1024 の実 double における Eigen/library rate 比の幾何平均は OpenBLAS 0.67、oneMKL 0.81、AOCL 0.58。大きめの EVD で backend 比較を続ける動機にはなるが、この倍率を ReciPro の複素 EVD や STEM 全時間へ転用しない。

[公式 MR !2903](https://gitlab.com/libeigen/eigen/-/merge_requests/2903) の harness は complex 型も実装している一方、本文は complex の本測定を今後の作業として区別している。[GEEV harness source](https://gitlab.com/rmlarsen1/eigen-new/-/raw/benchmark-comparison-harness/benchmarks/comparison/bench_geev_compare.cpp) では、complex 型に `ComplexEigenSolver`、LAPACK 側に ZGEEV を選び、両側とも入力コピーを含め、solver と query 済み workspace は再利用する。この測定設計は今回の比較にも参考になるが、公表ページの数値と未公表 complex 条件を混同しない。

## ローカル runtime の実体

2026-11-06 の export / version 確認結果。

| ファイル | 結果 |
|---|---|
| `ReciPro/bin/Release/libMathNetNumericsMKL.dll` | FileVersion 3.0.0.0、160,656,384 bytes。`z_eigen` は公開、`LAPACKE_zgeev_work` / `zgeev_` 等は公開されていない |
| 同ディレクトリ `libiomp5md.dll` | FileVersion 20211013。同梱OpenMP runtimeの版であり、MKL計算本体の版を示す値ではない |
| `C:/ProgramData/Miniconda3/Library/bin/openblas.dll` | `openblas_get_config` = `OpenBLAS 0.3.9 DYNAMIC_ARCH NO_AFFINITY Barcelona MAX_THREADS=128`、`openblas_get_parallel` = 1 (pthreads)。LP64、上表の LAPACKE work API が存在 |
| `C:/tmp/recipro_evd_261106/openblas0334/bin/libopenblas.dll` | 公式 OpenBLAS 0.3.34 Windows LP64 版を隔離取得。config は `DYNAMIC_ARCH NO_AFFINITY Cooperlake MAX_THREADS=64`、parallel=1。必要な 3 work API を確認済。BDN の主比較はこちら |
| `C:/tmp/recipro_evd_261106/mkl/runtime/mkl_rt.3.dll` | Intel 配布 oneMKL 2026.2.0 を隔離取得。`LAPACKE_zgeev_work` / `zgees_work` / `ztrevc_work` を公開。system PATH / Python 環境へインストールしていない |

OpenBLAS 0.3.9 は古い既存在庫の測定であり、最新 OpenBLAS の性能上限を示さない。MKL wrapper に direct LAPACK export が無いため、現存 DLL への P/Invoke 変更だけでは direct ZGEEV を呼べない。別 runtime を配布する場合のサイズ、ライセンス、ローダーの分離も採用判断に含める。

### Eigen の LAPACKE hook と Fortran 直接呼出

vendor 内 `Eigen/src/Eigenvalues/ComplexSchur_LAPACKE.h` は、`ComplexSchur::compute` を `LAPACKE_zgees` に置き換える公式 hook を持つ。その後の三角行列からの固有ベクトル構築と正規化・整列は Eigen が担当する。今回の ZGEES+ZTREVC prototype は同じ Schur driver を比較するが、後半は LAPACK なので **この Eigen hook 自体を実測したことにはならない**。

`EIGEN_USE_LAPACKE` の全体有効化は LU 等の別処理も置換し得る。EVD のみの効果を調べるなら専用 translation unit で隔離する必要がある。現在は explicit runtime load の方が backend の選択と failure を観測しやすいため、まず direct driver を比較する。

Fortran `zgeev_` export も既存在庫で確認したが、直接呼出は保留する。column-major `LAPACKE_zgeev_work` は同じ Fortran driver への薄い C ABI であり、入力 transpose や workspace 自動確保を要求しない。Fortran CHARACTER 引数の hidden length、compiler ごとの mangling、LP64/ILP64 の曖昧さを追加してまで短縮できる処理は小さい。もし direct driver が有効なら、同じ最新 runtime の高水準 `LAPACKE_zgeev` と work API の少数条件比較で workspace 再利用単独の寄与を分離できる。

oneMKL wheel と依存 runtime の[取得記録](evidence/cpu/mkl-download-manifest.json)、[OpenBLAS公式release metadata](evidence/cpu/openblas-release.json)を保存した。ローカル原本は `C:/tmp/recipro_evd_261106/mkl/download-manifest.json`。smoke の JSON に、実際にロードした backend の絶対パスと SHA256 を残す。

## prototype の契約

`cpu_adapter.cpp` は現行と同じ Eigen / ClangCL と `/O2 /fp:fast` で別 DLL にした。**実測 adapter は AVX2、比較基準の frozen production DLL は AVX512**。adapter SHA256 は `EE18CBF60EEE82E28E8962E22510EB466232F1D5AE1D95AB9BED99F4F11FCA27`。この比較は運用中の基準に対する差であり、ISA を揃えた算法単独の比較ではない。特に後段の共通 Eigen LU/GEMM と fresh/reused Eigen の結果には ISA 差が残る。AVX512 を引数に取れる build script が存在することは、AVX512 adapter の実測を意味しない。

C API は column-major `complex<double>` (double 2 個)、`int32` LP64、cdecl。公開された本番 ABI は変更しない。

`CpuCreate(n,algorithm,path,&info)` が一つの worker 専用 context を作る。`CpuSolve` は入力 A を破壊せず、右固有ベクトル V と固有値 λ を出す。algorithm 0=fresh Eigen、1=ZGEEV work、2=ZGEES+ZTREVC work、3=MathNet MKL、4=reused Eigen。1 と 2 は OpenBLAS / 最新 oneMKL の同じ C ABI を使える。非零 info は失敗として伝播し、別 backend へ黙って fallback しない。

algorithm 3 は **既存 DLL の native `z_eigen` を直接呼ぶ測定**であり、MathNet の C# `DenseMatrix.Evd()` 全体ではない。D の出力 buffer は context 作成時に確保して再利用する。一方、wrapper 内部の入力 clone と LAPACKE 高水準呼出が行う workspace 確保は計測に含まれる。この行の値を「既存 managed MKL 経路の総時間」や「C# overhead の計測値」と呼ばない。

`CpuPropagate` は全 backend の EVD の後に、共通の Eigen partial-pivot LU で `V α = ψ₀` を解き、`V diag(α)` と全厚さの指数行列との GEMM を行う。EVD のみと、EVD+LU+全厚さ伝播を別々に測る。LAPACK で必要な入力コピーは timed call 内。query、context 作成、DLL load は setup。これは同じ数式の比較用 adapter であり、本番へ直接貼り付ける設計ではない。

workspace query は `lwork=-1`。ZGEEV の complex workspace 最小は 2N、real workspace は 2N。ZGEES の query 結果は ZTREVC の必要な complex 2N を包含する。最適値は実装・N に依存するため、固定値の推測に置換しない。[LAPACK workspace query](https://www.netlib.org/lapack/lug/node118.html)

latest oneMKL SDL は最初に sequential layer と LP64 を明示する。MathNet の旧 OpenMP と新 runtime の OpenMP を同時ロードしないためであり、今回の測定対象は内側 1 thread。local thread setter が存在する場合は呼出側 thread の旧設定を保存・復元する。これを worker ごとの設定復元なしにアプリへ持ち込まない。[Intel threading layer](https://www.intel.com/content/www/us/en/docs/onemkl/developer-reference-c/2026-2/mkl-set-threading-layer.html)、[Intel local thread count](https://www.intel.com/content/www/us/en/docs/onemkl/developer-reference-c/2025-0/mkl-set-num-threads-local.html)

## 正しさゲートと測定

小 N smoke は複素吸収非 Hermitian、実非対称、近縮退の 3 系。EVD 残差 `||AV−VΛ|| / (||A||||V||+||VΛ||) < 1e-11`、全出力 finite、入力非破壊を確認する。厚さ 0 / 0.001 / 35 / 100 の波動を独立な SciPy 行列指数 `expm(2πiAt) ψ₀` と比較し、peak normalized error < 1e-10。大 N は workspace query のみで、性能値とはしない。

本測定は `ReciPro.Benchmarks/EvdBackendBenchmark.cs` の共通物理行列 (Si / Au / GaAs、傾斜面・吸収を含む) を使い、BenchmarkDotNet で行う。小 N smoke の成功だけでは本番採用しない。実際の requested beam count と採用 N、処理中の DLL hash、MKL / OMP の thread 設定、条件数または再構成誤差を併記する。Hermitian solver への置換は対象外。

2026-11-06 21:18 の smoke は 7 方式 × 3 次元 (N=4/16/48) × 3 系 = 63 件を全通過した。最大 EVD 残差は **6.45e-16**、独立行列指数との最大波動差は **1.09e-13**。この初回 smoke は方式ごとに別の seeded random 行列を使ったが、全件を独立行列指数と比較した。保存スクリプトはその後、方式ごとに seed を初期化して共通入力へ揃えた。本測定の物理行列は最初から共通である。

workspace の実測 query 結果 (complex128 要素数) は次の通り。MathNet の内部分は公開 API から取得できない。

| N | OpenBLAS ZGEEV | OpenBLAS ZGEES | oneMKL 2026 ZGEEV | oneMKL 2026 ZGEES |
|---:|---:|---:|---:|---:|
|128|16,640|4,523|33,152|4,224|
|256|33,280|8,448|99,008|8,448|
|448|58,240|14,784|173,120|14,784|
|640|83,200|21,120|247,232|21,120|

例えば N=640 の latest MKL ZGEEV は complex workspace だけで約 3.77 MiB。context 再利用は allocation を減らす一方、worker 数に比例する保持メモリを増やす。本番採用時は thread-local の無制限保持ではなく、worker 所有と破棄の境界を明示する。prototype は比較のため全方式に共通 buffer を置いているので、そのままの常駐量を本番の必要量と見なさない。

初回 adapter SHA256: `ee18cbf60eee82e28e8962e22510eb466232f1d5ae1d95ab9bed99f4f11fca27`。記録 `C:/tmp/recipro_evd_261106/cpu-smoke.json`。

## 共通物理行列とBDNの結果

CPU7方式は9条件すべてで数値ゲートを通過し、独立したSciPyの行列指数とも一致した。ZGEEV主比較2run各24件、Schur/既存MathNet追加3run各12件のCPU計84benchmarkを完了し、GPU12件を合わせ計96件。詳細と全8標本は[統合記録](README.md)と[性能一覧](evidence/performance/timings.md)へ保存した。

高負荷下・1workerの参考値ではOpenBLASのSi256/448/640、Au448、GaAs256が同runのEigenより短く、Si128は遅かった。最新oneMKLも256/640で改善した一方、448は概ね同等だった。既存MathNet native exportは追加したSi128/448/640でEigenより短くならなかった。これをmanaged MathNet全体の速度や、低負荷・多方向並列時の順位へ外挿しない。

Schur方式にも大行列で改善があるため、差をZGEEV化またはworkspace再利用の一因に帰さない。Eigen再利用版の精度は確認したが、同じadapter内でfresh/reuseだけを分離したBDN測定と、同一LAPACK runtimeでのhigh-level/work API比較は未実施。再利用だけの高速化率は未確定である。本番への接続はnative一括伝播の構造を保つ選択可能backendを優先し、Auto閾値は全工程の比較まで維持する。
