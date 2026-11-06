# cuSOLVER Xgeev：適合条件と隔離検証（261106Ch）

## 結論と現在の段階

ReciProの吸収を含む複素非Hermitian行列に対し、cuSOLVERの `cusolverDnXgeev` は必要な右固有ベクトルと複素固有値を求めるAPIを提供する。Eigenの新GPUモジュールにあるHermitian専用EVDとは別の接続である。CUDAの公式redistributableを隔離配置し、C#からホストAPIを直接呼ぶ研究用adapterを作成した。nvcc、Toolkitのシステム導入、driver変更、本番コードの変更は不要だった。

公式配布物のSHA-256、DLLの依存関係・export、API契約を確認した後、共通runnerで**9条件すべての数値ゲートを通過**した。6条件のBenchmarkDotNet比較も完了し、CPU飽和下ではSi640のEVDが同じrunのEigen基準に対して約1.96倍速かった。一方、Si128は約5.77倍の時間を要した。GPUの一律採用や無負荷時の切替閾値を決める結果ではない。前回の[GPU再検討条件](../2026-11-06-dynamical-speed-followup/research-review.md)を実行可能な比較へ進めた記録である。

## 実機と採用する配布版

| 項目 | 読取専用調査の結果 |
|---|---|
| GPU | NVIDIA GeForce RTX 4060、8188 MiB、compute capability 8.9 |
| driver / UMD | 610.88 / CUDA UMD 13.3 |
| 実行形態 | Windows x64、WDDM、表示にも使用中 |
| 既存開発環境 | nvccがPATH上に無く標準Toolkitフォルダも無し。condaのtorchは2.12.0+cpuでCUDA DLLを含まない |
| 初期比較の配布版 | CUDA 13.3.1、cuSOLVER 12.2.6.9、通常の複素double・既定算法 |

[inventory](evidence/cusolver/gpu-inventory.txt)。nvidia-smiのCUDA表示はdriver側の対応であり、Toolkitが導入済みという意味ではない。表示アプリケーションとGPUを共用しているため、GPU負荷と空きメモリの条件も測定時に残す。

実際にロードしたAPIからも `CUDA runtime=13030`、`driver API=13030`、`cuSOLVER=12206`、`CC=8.9`、`FP32/FP64=64` を取得した。cuSOLVERのこの整数版表現は12.2.6を表し、配布manifestにある12.2.6.9の末尾build番号は別に保持する。[実行ログ](evidence/cusolver/physical-validation.txt)

調査時の最新CUDAは13.5（2026-11-04公開）であるが、対応driver branchはR620。13.3の対応branchはR610。13.xのminor compatibilityは存在するものの、13.5の新機能には対応する新driverを要するという公式条件がある。今回はdriverを変えず、13.3.1の通常Xgeevを主比較に選んだ。13.5の新算法が610.88で動くことを仮定しない。[NVIDIA driver対応表](https://docs.nvidia.com/cuda/cuda-toolkit-release-notes/#cuda-driver)、[13.5公式配布manifest](https://developer.download.nvidia.com/compute/cuda/redist/redistrib_13.5.0.json)

CC 8.9はFP32に対するFP64の演算throughputが64:1の系統である。これは今回のハードウェアでFP64計算を評価する際の制約であり、その比率がEVD全体の時間比になるわけではない。CPUとのデータ移動、QR反復、GPU占有率も影響する。[NVIDIA Compute Capabilities表](https://docs.nvidia.com/cuda/cuda-programming-guide/05-appendices/compute-capabilities.html#features-and-technical-specifications)

### RTX 4060での速度をどう予想するか

公式仕様はXgeevをCPU/GPU混成算法とし、pinned host workspaceを推奨する。GPUへ行列を置いても、CPU側の仕事とhost/device間のやり取りが消える仕様ではない。したがって、RTX 4060のFP64 throughput制約に加え、host側に残る処理・同期が律速になる可能性を調べる必要がある。ただし、**今回の行列でCPU側が実際の律速になったという測定結果はまだ無い**。[NVIDIA Xgeevの仕様](https://docs.nvidia.com/cuda/cusolver/index.html#cusolverdnxgeev)

このadapterの外側の転送量だけでも1回あたり `32N²+16N+4` bytes（Aの入力、V・W・infoの出力）となる。N=448では約6.13 MiBで、ライブラリ内部の混成計算による転送は別途ある。workspaceの再利用は確保費用を減らすが、この転送や反復回数を消さない。N=128～448に対する速度倍率は、GPUの公称FLOPSから計算せず、同じ行列を用いた同期完了までのwall timeから判断する。

hostのCPU時間がwall timeに占める比率、GPU利用率、pinned/device workspace量は、遅い場合の原因を絞る補助情報とする。低いGPU利用率だけではCPU律速と断定できない。小さいkernel、driver/WDDMの待ち、データ転送、計測間隔の粗さも候補になる。

## 公式配布物と実体の確認

保存先は `C:/tmp/recipro_evd_261106/cuda/13.3.1/`。同じreleaseの[公式manifest](https://developer.download.nvidia.com/compute/cuda/redist/redistrib_13.3.1.json)から以下を取得し、zipのSHA-256を全て照合した。installerは実行していない。

| component | library version | zip bytes | 使用するDLL |
|---|---|---:|---|
| cuda_cudart | 13.3.29 | 2,589,792 | cudart64_13.dll |
| libcusolver | 12.2.6.9 | 236,630,673 | cusolver64_12.dll |
| libcublas | 13.6.0.2 | 393,706,755 | cublas64_13.dll、cublasLt64_13.dll |
| libcusparse | 12.8.2.51 | 152,461,374 | cusparse64_12.dll |
| libnvjitlink | 13.3.33 | 274,601,597 | nvJitLink_130_0.dll |

cuSOLVERのPE importにはcuSPARSEとcuBLAS/Ltがある。nvJitLinkは同じreleaseの補助依存として隔離取得した。`cusolverDnXgeev`、`cusolverDnXgeev_bufferSize` のexportを確認した。取得scriptは `tools/EvdBackendStudy261106/gpu_fetch_redist.py`。全体のPATH、レジストリ、conda環境へ変更を加えない。

- [取得URL・zip SHA-256](evidence/cusolver/download-manifest.json)
- [実DLLの絶対パス・サイズ・SHA-256](evidence/cusolver/dll-manifest.json)
- [PE依存関係](evidence/cusolver/pe-dependencies.txt)、[PE export](evidence/cusolver/pe-exports.txt)

## Xgeevで維持する数値契約

入力はcolumn-majorのcomplex doubleで、全data/compute typeを `CUDA_C_64F` とする。`jobvl=NOVECTOR`、`jobvr=VECTOR`、`lda=ldvr=N`、`ldvl=1`。得られる右固有ベクトルは `A V = V Λ` を満たす。左固有ベクトルはこのAPIでは対応しないが、現行の伝播係数は右固有ベクトル行列をLUで解いて求められる。`V†` を `V⁻¹` の代わりに使うHermitian化はしない。

`Xgeev_bufferSize` が返すdeviceとhostの両workspaceを確保し、host側はpinned memoryにする。device入力Aは上書きされるため、adapterは元のmanaged入力からコピーする。API statusとdevice上の `info` は別々に検査し、`info != 0` の固有解は採用しない。これらは固定した[CUDA 13.3.1 API仕様](https://docs.nvidia.com/cuda/archive/13.3.1/cusolver/index.html#cusolverdnxgeev)に従う。

NVIDIAの公式sampleも、complex double入力、右vector、pinned host workspace、nonblocking stream、最後のstream同期という構造を使う。今回読んだ上流commitは `8b0573b11d183d9b3a36cd2955aa31c16cc61b95`。adapterはAPIに基づく独自実装であり、sample本体をコピーしていない。[固定版example3](https://github.com/NVIDIA/CUDALibrarySamples/blob/8b0573b11d183d9b3a36cd2955aa31c16cc61b95/cuSOLVER/Xgeev/cusolver_Xgeev_example3.cu)、[取得commit](evidence/cusolver/sample-commit.json)

## 既定算法と新しいALG_1を分ける

取得した13.3.1のheaderでは `cusolverDnFunction_t` にGEEV選択項目が無い。一般的な `CUSOLVER_ALG_1` enumが存在していても、それだけでXgeevの混合精度が使えるとは判断できない。adapterはalgorithm=0のみ受け付け、既定のmath modeを明示し、FP64 emulationは有効にしない。

最新13.5のrelease notesと公式example4には、低い精度でSchur分解を求めて反復精密化するXgeev `ALG_1` がある。複素double・右固有ベクトルが対象で、大きく固有値が十分離れた場合に適し、密集スペクトルや小行列では遅くなること、workspaceが増えることが明記される。API本文の「既定算法のみ」という旧記述との差は、最新sampleによって単なる名称の推測からは進んだが、配布版とdriverを合わせた実行確認が必要である。[13.5 release notes](https://docs.nvidia.com/cuda/cuda-toolkit-release-notes/#cusolver-release-13-5)、[固定版example4](https://github.com/NVIDIA/CUDALibrarySamples/blob/8b0573b11d183d9b3a36cd2955aa31c16cc61b95/cuSOLVER/Xgeev/cusolver_Xgeev_example4.cu)

Bethe行列には対称性由来の縮退や近接固有値があるため、sampleの大規模・分離スペクトルでの利点を一般化しない。ALG_1やFP64 emulationは通常算法と独立に検証する候補として残す。今回の隔離13.3.1比較の速度に含めない。

ALG_1の「double精度を回復する」という説明から、旧EVDとビット一致することや、非正規行列の伝播量まで自動的に同じ精度になることを推定しない。近接固有値・非正規性の強い条件では、後方残差だけでなく再構成波と厚さ積分の誤差を検査する。将来対応版を試す場合も、通常算法の精度ゲートと並列に判定する。

## 研究adapterと測定境界

`tools/EvdBackendStudy261106/GpuEvdBackend.cs` は `ReciPro.EvdBackendStudy` namespaceの `IDisposable` class。`GpuEvdBackend(isolatedRoot, N, algorithm=0)` で1stream/handle/paramsと再利用workspaceを確保し、`Solve(A,W,V)` を繰り返す。同名CUDA DLLが別の場所から既にロードされていれば失敗させ、隔離先の絶対パスとprocess内だけのDLL探索登録を使う。

warm測定の1回に含める処理は次の通り。

1. managed入力からpinned stagingへコピー。
2. AをH2D転送し、Xgeevを呼ぶ。
3. W・V・infoをpinned memoryへD2H転送。
4. stream同期、API/numerical info確認、managed出力へコピー。

constructorの時間、device workspace、pinned host workspace/staging量は別に記録した。constructor時間はDLLロード・handle/context作成等の概数であり、初回EVDのlazy initialization費用は含まない。最初のSolveだけのcold時間は今回まだ計測していない。将来はこれを別途測定し、BenchmarkDotNetのwarm時間と分けて記録する。Nごとにworkspaceを使い回す境界なので、毎回の確保込み比較を行う場合は別の測定名にする。

固有値だけをGPUへ渡してCPUへ戻さない測定や、同期せずenqueue時間だけ測る値をReciProのEVD時間として使わない。現在の共通比較はCPU側へ右vectorを返す契約なので、GPUに後段のLU/GEMMまで保持する場合は別の全工程試験が必要になる。

### ABI再確認と本番化前の残件

取得headerと再照合し、Xgeevの22引数、bufferSizeの19引数について、`int64_t` はC# `long`、`size_t` は `nuint`、pointerは `nint`、statusとdevice infoは32bitとしていることを確認した。Windows x64専用guardを持ち、Complexの2個のdoubleをcolumn-majorで渡す。ソース照合に加え、以下の実行時の数値ゲートも通過した。

現adapterの `Dispose()` はstream同期後に解放を試みるが、同期・free/destroy・DLL探索登録解除の戻り値は診断へ残していない。`Solve()` 内のAPI statusとnumerical infoは検査済みだが、異常終了時の解放失敗の診断は本番化前の残件である。改善時は元の計算例外をcleanup例外で隠さず、解放結果を記録し、再利用を禁止する方針とする。実行中の検証バイナリとの対応を保つため、この段階ではソースを変更していない。

## batch・streamと判定条件

XgeevのAPIにbatch dimensionは無く、13.3.1のheader/exportでもXgeevのbatched APIは確認できない。Hermitian専用 `XsyevBatched` を流用しない。複数の独立方向を並べるなら、まず方向ごとのhandle/stream/workspaceを分離し、2streamまでの追加試験にする。これはGPU同時処理の候補であり、必ずoverlapするという保証ではない。XgeevはCPU/GPU混成なので、host側の時間とメモリも残る。[NVIDIA stream並行実行の仕様](https://docs.nvidia.com/cuda/cusolver/index.html#parallelism-with-streams)

本比較は単一streamとし、この調査で追加する重いjobは1本ずつ実行した。他者が実行中の計算で事前にCPU100%の飽和を確認し、作者指定に従って高負荷下の参考値を測った。追加jobはCPU側Idle優先度、processor count上限2、BLAS/OMPを1とした。これらの環境変数がcuSOLVER内部の全host処理を制限するとは断言しない。負荷サンプルは以下に示す。

共通fixtureではSiだけでなく非中心対称・重元素、無吸収、近縮退を含め、`||AV−VΛ||`、同じLU参照から再構成した波、厚さ0の境界、表面法線流束を比較した。固有ベクトルの順序や任意位相の直接一致を合否にしない。精度ゲートを通ったfixtureだけを速度比較へ進め、代表サイズと実用方向数で採用範囲を決める。sourceの厚さ積分までの検証は別途必要である。

## 共通runnerによる数値検証結果

Si128/256/448/640、Au448、GaAs256、無吸収のSi32B0/GaAs32B0、合成のNearDegenerate8の9条件がすべてPASS。最後の1条件は非正規2×2 blockを4個並べた検証用行列であり、実物理試料の速度表には含めない。物理fixtureは200 kV、[001]から0.5°傾斜、表面法線23°、厚さ0/35/100 nmで比較した。

比較基準は凍結した `Crystallography.Native.avx512.dll`（SHA-256 `78B2B691DE805FB7EED7E0F877561883D346B1F781303C75F906ED85EF894174`）。候補・基準の右固有ベクトルから、共通のnative PartialPivLUと共通のスカラー和で波を再構成した。

| 検査 | 9条件中の最大値（概数） | 判定上限 |
|---|---:|---:|
| 正規化EVD残差 | 5.51×10⁻¹⁶ | 10⁻¹¹ |
| 再構成波の最大差 / 基準波の共通ピーク | 1.96×10⁻¹³ | 10⁻¹⁰ |
| 厚さ0の入射境界の正規化誤差 | 6.66×10⁻¹⁶ | 10⁻¹¹ |
| 表面法線流束の基準との差 | 3.00×10⁻¹⁴ | 10⁻¹⁰ |
| 無吸収2条件の流束保存誤差 | 3.44×10⁻¹⁵ | 10⁻¹⁰ |

残差は `||AV−VΛ||F / (||A||F||V||F + ||VΛ||F)`、流束は `Σg P_g|ψ_g|²/P_0`。入力行列の非破壊性と全固有値・固有ベクトルの有限性も検査した。[全ログ](evidence/cusolver/physical-validation.txt)、[条件別の数値](evidence/cusolver/physical-metrics.json)

続いて、固有値分解を使わない `scipy.linalg.expm(2πiAt)ψ₀` ともGPUの9条件すべてを照合し、通過した。波の最大正規化差は約3.27×10⁻¹³（NearDegenerate8）、法線流束の最大差は約4.34×10⁻¹⁴（Si640）。前表の「Eigen基準との差」と参照が異なるため、数値を混ぜない。[独立expm参照の全結果](evidence/expm-oracle.json)

GPU側のLU・伝播一括APIはこのadapterに実装していない。ログの `fusedDifference=0` はその検査を行わなかった場合の値であり、GPU伝播がビット一致した意味ではない。EBSD/TDS sourceの厚さ積分も、この9条件では直接検証していない。全工程の速度は別の評価を要する。

## 実測workspaceと初期化費用

次は実APIのbufferSize queryが返した値と、adapterが別に確保したpinned staging量である。単位はbytes。これはlibrary/driver内部の確保を全て数えたピークVRAMではない。

| N | device workspace | pinned host workspace | pinned staging |
|---:|---:|---:|---:|
| 128 | 1,049,088 | 676,352 | 526,340 |
| 256 | 1,847,808 | 2,694,144 | 2,101,252 |
| 448 | 3,236,864 | 8,230,912 | 6,429,700 |
| 640 | 6,589,952 | 16,782,336 | 13,117,444 |

device側には上表のworkspaceに加え、A/V/W/info用にpinned stagingと同じbyte数を直接確保する。N=640では直接確保分だけでdevice約18.79 MiB、pinned host約28.51 MiBとなる。複数streamへ展開すると、原則としてこれらをstreamごとに持つ。[全9回の観測値](evidence/cusolver/workspace-observations.json)

同一runner内でも、最初のSi128 constructorは約9.20 s、その後は約0.31～5.82 sだった。これは各fixtureでDLL/handle/stream/workspaceを作り直した一度ずつの観測であり、Nに対する初期化時間の性能曲線や、反復EVDの速度ではない。cold/warmの違い、OS/driverのcacheと同時負荷を切り分けていない。実アプリでcontextとworkspaceを長期間保持する場合と、1回だけ計算して解放する場合では、この費用の償却が大きく異なる。

## BenchmarkDotNet：CPU飽和下の6条件

事前にCPU100%の飽和を確認し、作者指定に従い高負荷下で参考測定を続けた。GPU run開始・終了のCPUサンプルは99→87%で、測定中ずっと100%だったとは扱わない。この調査の追加jobは1本ずつで、GPUもhost側処理を含む。BDN InProcess/Monitoring、invocation=1、unroll=1、warmup=3、measurement=8、外れ値除去なし。単位はms、±は標準偏差。init/workspace確保は計測外、各EVD呼出しはhost stagingコピー、H2D/D2H、stream同期、info判定を含む。[共通測定条件](protocol.md)、[負荷サンプル](evidence/performance/load-samples.jsonl)

| 条件 | 同runのEigen平均 ± SD | GPU平均 ± SD | GPU/Eigen平均 |
|---|---:|---:|---:|
| Si128 | 8.839 ± 1.462 | 51.016 ± 3.504 | 5.772 |
| Si256 | 130.752 ± 7.598 | 123.136 ± 4.387 | 0.942 |
| Si448 | 304.185 ± 14.269 | 276.268 ± 14.425 | 0.908 |
| Si640 | 944.061 ± 65.640 | 481.041 ± 10.385 | 0.510 |
| Au448 | 283.262 ± 9.745 | 290.271 ± 13.356 | 1.025 |
| GaAs256 | 143.662 ± 13.138 | 116.197 ± 7.379 | 0.809 |

全12benchmark（6条件×2方式）を完了した。[FullJSON](evidence/performance/cuSOLVER1331-bounded/ReciPro.Benchmarks.EvdGpuBackendBenchmark-report-full.json)、[全ログ](evidence/performance/cuSOLVER1331-bounded/console.txt)、[性能一覧](evidence/performance/timings.md)

Si640は同runの平均比で約1.96倍の改善が出た。Si128の悪化も明瞭であり、GPUを小さい問題へ一律に使う根拠はない。Si256/448の小さな平均差や、Au448とSi448の違いから、反射数Nだけの固定閾値もまだ導けない。baselineとcandidateは同時・交互測定ではなく、CPU負荷の時間変動も残る。この表を無負荷時の性能や全STEM/HRTEM画像の速度へ外挿しない。

OpenBLAS/oneMKLは別runで測っているため、それらの絶対時間と上表のGPU時間を割って順位・倍率を作らない。GPUが使われたこと、精度ゲートを通ること、上記負荷下でEigen比に条件依存の改善があることが、今回直接確認した範囲である。

## この結果から進める設計候補

1. **任意選択のGPU sessionとworkspace再利用を先に評価する。** 大きい行列を多数方向で繰り返す処理なら初期化を償却できる。1stream・1workerを起点とし、最大反射数に合わせたgrow-only workspaceを持つ。今回の数秒のconstructor費用を毎方向へ加える設計は避ける。単発・小行列のCPU経路は保持する。
2. **後段までGPUに保持する案は別の実験にする。** CBED/STEMで右固有ベクトル、LU係数、全厚さの位相をdevice上に保持し、必要な出射波だけを戻せば転送を減らせる可能性がある。ただしRTX 4060のFP64 LU/GEMMがCPUより速い保証はなく、EVD＋LU＋伝播の実測と同じ精度ゲートが必要である。現在のEVD倍率をその案の倍率として扱わない。
3. **Auto判定は低負荷・方向バッチの追加測定後に決める。** Nに加えて結晶・方位・必要な出力・反復回数と初期化済みかを区別する。まず今回改善したSi640相当と遅いSi128相当を含む境界を再測定し、CPU/GPUの実用範囲を確かめる。2stream化、ALG_1、FP64 emulationはその後の独立候補である。

以上は次の比較を小さく具体化する提案であり、本番の既定backendやAuto閾値は変更していない。
