# EVD backend の接続条件・共通入力・数値判定

2026-11-06。対象は ReciPro 配下の正本 `Crystallography` と `Crystallography.Native`。本記録は接続設計と比較実験の契約であり、数値検証と主測定60 benchmarkの結果は[統合記録](README.md)・[速度表](evidence/performance/timings.md)に整理した。前回の HRTEM 位相再利用、α 列直接参照、EBSD 不等間隔積分の因数分解、不要局所源の省略は保持する。

## 1. 現行経路と切替時の注意

| 呼出元 | 現在の固有値経路 | LU・波の生成 | 保持量・切替制約 |
|---|---|---|---|
| 通常回折・HRTEM | 要求反射数 `<400` かつ native 有効なら `_EigenSolver`、それ以外は MathNet `Evd(Asymmetric)` | 固有ベクトル逆行列全体を公開プロパティに保持。入射 e₀ の α はその第1列を直接参照 | V と V⁻¹ 各 `16N²` bytes。新 backend が α だけ返す案では公開契約を維持できない |
| PED | 各方位の EVD と逆行列を保持 | 厚さ変化で固有解を再利用、α は逆行列第1列 | 方位数を K とすれば主に `32KN²` bytes。GPU 化で保持構造まで変える必要はない |
| CBED | Auto は native 有効時 `MtxExp_Eigen`、無効時 MathNet EVD | EVD 指定時 native は EVD→LU→全厚み GEMM、MathNet は EVD→LU→厚み別 GEMV | EVD の高速化は Auto の等間隔 MtxExp ケースには直結しない。不等間隔・重複厚みでは既存ガードにより EVD へ移る |
| EBSD | Auto は native EVD、利用不可なら MathNet EVD | native は `_EigenSolver`→`_PartialPivLuSolve`→強度積分 | managed 入出力は worker-local pool 再利用済み。native EVD 内部 scratch は呼出しごと。局所源省略の前回改善は独立して有効 |
| STEM | Auto は MKL 有効かつ実効 N≥500 で MathNet MKL、それ以外 native EVD | native 全スライス経路は EVD→LU→GEMM で全深さの ψ を物化。旧経路は λ/V/α を保持して後段で再構成 | **現在の全スライス経路は native `Eigen_Eigen` のみ**。単に Auto を MathNet MKL へ寄せると従来のメモリ増を再導入する |

確認箇所: `BetheMethod.cs` の `cbed_DoWork`、`ebsd_DoWork`、`GetDifractedBeamAmpriltudes`、PED、`StemDoWork`、`NativeWrapper.cs` の `EigenSolver`/`CBEDSolver`/`CBEDSolver2`、`Crystallography.Native/EigenFuncs.cpp` の同名 export。

`MathNetProviderManager.Initialize` は起動時に process-global provider を設定する。MKL の内側スレッドを 1 にしてから初期化し、その後 `Control.MaxDegreeOfParallelism` を戻す。初期化後の MKL→managed 切替や再初期化による内側スレッド変更は提供しない。実装側でこの規約を迂回しない。

Auto 時の外側並列は STEM/EBSD が `ProcessorCount`。CBED の MathNet MKL fallback は `ProcessorCount/4` が残る。また PED は native 側が `ProcessorCount`、MathNet 側が `ProcessorCount/4` で、MKL 不在の managed provider にも後者が適用される。これらを増やす判断には、大 N で同時 EVD workspace が増えることを含む全体測定が必要。今回の単一行列・内側1スレッド測定だけで変更しない。

## 2. 公開 API を変えずに接続できる箇所

1. `_EigenSolver` の内部を一般複素 EVD backend に置き換える。入力 A、出力 λ/V の column-major 契約を保てば、EBSD・回折・PED の呼出元を変更する必要はない。既存関数は `void` で収束状態を返さないため、本番接続時には内部で検出して安全な失敗または検証済み fallback を実装する。研究 adapter は非ゼロ `info` を例外として扱い、黙って Eigen に戻さない。
2. `_CBEDSolver_Eigen` 内の EVD を置き換え、同じ LU と GEMM を続ける。**これが direct LAPACK の本命**。STEM の全スライス構造を保ち、MathNet 経由で V を全方向保持するメモリ増を避けられる。API 名の `Eigen` と内部 backend の名前がずれる点は、導入時の表示・診断で明示する。
3. `_CBEDSolver_Eigen2` に同じ backend を接続し、λ/V/α/Tg の出力を維持する。全スライスを使わない極厚条件も対象になる。
4. EVD workspace の worker-local 再利用。EBSD の C# 出力配列はすでに再利用されているが、Eigen solver や LAPACK work buffer は別問題。新 adapter の fresh Eigen と reusable Eigen を分けて測り、アルゴリズム差と allocation 差を混ぜない。

GPU は最初に EVD 単独の host 入力→転送→分解→host 出力→同期を測る。λ/V を host へ戻す方式で勝つなら上記の契約に接続できる。GPU 内で LU/GEMM/EBSD 積分まで続ける方式は転送を減らせるが別の実装であり、この EVD 測定だけから速度を推定しない。

本研究コードは `tools/EvdBackendStudy261106/` と `ReciPro.Benchmarks/EvdBackendBenchmark.cs` に隔離する。production から研究 adapter を参照せず、配布 DLL の探索先や GUI の Auto をまだ変えない。

## 3. 数値とデータの契約

- A は `A = D_P⁻¹(M+D_Q)`。**行 g を P_g で割る**。吸収を含む一般複素・非正規行列であり、Hermitian ソルバーへの置換は同じ問題を解かない。
- `System.Numerics.Complex` 相当の double 実部・虚部を交互に置き、A と右固有ベクトル V は column-major。`A V = V diag(λ)`。複素単精度や実行列近似は使わない。
- 固有値の並び、各列の複素位相・規格化は backend ごとに異なってよい。λ と V の対応を保ち、`V α = ψ₀` を解く。列同士の単純な差は判定に使わない。
- 入力 A と ψ₀ は呼出し後も不変。LAPACK の破壊的入力には adapter 内部でコピーを行い、その時間を計測に含める。GPU の host staging、H2D/D2H、完了同期も含める。
- 厚みは実際の配列をそのまま使用し、`ψ(t)=V diag(exp(2πiλt)) α` を計算する。不等間隔・t=0 を等間隔近似に変換しない。
- 戻り波は `[t*N+g]`。出射面位相 `exp(2πi kg_z t)` を付加する前の ψ を比較する。P と厚みは実数なので、この位相は各 beam の流束を変えない。
- context は単一 worker 専用。LAPACK work array、Eigen solver、CUDA stream/handle を複数方向から同時使用しない。サイズが変わる場合の再確保を設計する。
- backend DLL は絶対パスで明示ロードする。adapter と backend の SHA256、実ロードパス、thread 設定、GPU/runtime/driver、workspace サイズをログへ残す。BDN 子プロセスの依存 DLL を暗黙の PATH 解決だけで選ばない。

## 4. 共通 fixture と独立参照

主速度比較は次の6条件で実施した。いずれも 200 kV、[001] から軸 (1,0.3,0) 周りに0.5°、表面法線23°傾斜、厚み `[0,35,100] nm`。要求 N と実効 N は両方記録し、実効 N の行列を全 backend へ同一に渡した。

| label | 結晶 | 要求 N | B (Å²) | 狙い |
|---|---|---:|---:|---|
| Si128 | Si diamond, a=0.54309 nm | 128 | 0.46 | 小行列・固定費 |
| Si256 | 同上 | 256 | 0.46 | 中行列 |
| Si448 | 同上 | 448 | 0.46 | 実用重条件 |
| Si640 | 同上 | 640 | 0.46 | 大行列でのクロスオーバー探索 |
| Au448 | Au fcc, a=0.40782 nm | 448 | 0.60 | 重元素・強い散乱/吸収 |
| GaAs256 | GaAs zincblende, a=0.56533 nm | 256 | 0.60 | 非中心対称・複素結合 |

検証専用で `Si32B0`、`GaAs32B0` を使う。温度因子ゼロで吸収項がゼロとなるため、流束保存が独立した条件になる。通常の吸収ありデータに流束=1を強制しない。

さらに `NearDegenerate8` は物理 fixture と区別した合成検証。4個の2×2上三角 block、固有値間隔1e-8 nm⁻¹、非対角1e-4 nm⁻¹、共通減衰虚部1e-4 nm⁻¹を持つ。固有値残差が小さくても非正規な列の相殺で波が悪化し得る例として使う。速度表や実物理の有効範囲には混ぜない。これだけで欠陥行列・任意の強非正規性を保証したとは扱わない。

fixture 構築は現行 `Find_gVectors` と private `getEigenMatrix` を研究側の reflection から呼び、製品 API にベンチ用入口を増やさない。Biso、散乱模型、beam 選択設定、hkl を JSON に記録する。

対称操作で展開された原子数を Si=8、Au=4、GaAs=4+4 と assert し、元素サイト別の展開数も metadata に保存する。空間群の別 setting によって意図した構造が変わった場合は計測前に停止する。

### 保存形式

既定出力は `C:/tmp/recipro_evd_261106/fixtures/<label>/`。すべて little-endian、complex は Float64 の real/imag 交互。Python は `np.fromfile(path,dtype='<c16')`、行列は `reshape((N,N),order='F')` で読める。

| file | 内容 |
|---|---|
| `matrix.bin` | A、N² complex、column-major |
| `initial.bin` | ψ₀、N complex |
| `p.bin` | P_g、N Float64。物理 fixture では P_g>0 |
| `thicknesses.bin` | t、T Float64、nm |
| `metadata.json` | N・構造・模型・hkl・配列規約・A の SHA256 |
| `EigenReference/values.bin`, `vectors.bin`, `waves.bin` | 現行 native Eigen と共通 LU で得た参照値 |
| `<backend>/values.bin`, `vectors.bin`, `waves.bin` | 候補 EVD と同じ共通 LU で再構成した波 |
| `<backend>/fused-waves.bin` | 候補の EVD+LU+GEMM 一体呼出し。CPU adapter で保存 |
| `<backend>/metrics.json` | 残差・波・境界・流束の誤差 |

`RECIPRO_EVD_BACKEND_LABEL` で `OpenBlas0334_Geev`、`Mkl2026_Geev`、`MathNetMkl_Schur`、`EigenAdapterReused` のように識別する。同じ algorithm 番号でも DLL が異なる測定を同じ出力へ上書きしない。大きい binary はローカルに残し、docs へは要約指標と必要なログだけを保存した。

### 判定

共通 C# 検証は以下を **すべて**確認する。旧 Eigen との一致だけを独立した物理検証とは呼ばない。

1. 配列の有限性・長さ・A の非破壊性・backend status 成功。
2. 正規化 EVD 残差 `||AV−VΛ||F/(||A||F||V||F+||VΛ||F) ≤ 1e-11`。固有列の並びと規格化に依存しない。
3. t=0 の波が ψ₀ に戻ること。ピーク正規化最大差 ≤1e-11。
4. 同じ native LU と同じ複素指数・積算で全 backend の波を再構成し、現行 Eigen に対してピーク正規化最大差 ≤1e-10。native 一体伝播と共通再構成の差も ≤1e-10。
5. `J(t)=Σ_g P_g |ψ_g(t)|²/P₀` の backend 間絶対差 ≤1e-10。B=0 の物理 fixture は `|J−1|≤1e-10` も必須。
6. 独立した SciPy `expm(2πiAt)ψ₀` と照合する。非正規性の相対 commutator norm、SVD による κ(V)、近縮退を記録し、残差が小さいのに波が悪い候補を排除する。今回は全9 fixtureの0/35/100 nmについて、8候補と基準Eigenの計81出力を照合して全通過した。expm参照は同じfixture・厚みに対して一度作成し、各backendへ共用した。[全oracle結果](evidence/expm-oracle.json)

1e-10 は backend が異なる場合の比較ゲートであり、入力ごとの既存実測誤差より緩い結果を無条件に採用する意味ではない。最終的には raw 誤差、材料・厚み依存、既存全体回帰を併記する。近縮退では個別固有値を無理に一対一対応させず、再構成した波と必要に応じて縮退部分空間を見る。

## 5. 計測の解釈とメモリ

`EvdBackendBenchmark` は現行配布 `_EigenSolver` と candidate EVD を比較する `EVD` カテゴリ、現行 `_CBEDSolver_Eigen` と candidate の EVD+共通 Eigen LU/GEMM を比較する `Propagation` カテゴリを持つ。後者は3厚さの一体処理であり、全STEM計算の速度ではない。多数スライスの処理量や外側方向並列は、この最初の比較で測ったとは言わない。

`EvdGpuBackendBenchmark` は host 入力/出力の EVD 完了時間を比較する。context 初期化と workspace 確保は Setup に分離し、その時間・メモリ量は別ログへ残す。GPU カーネルだけの非同期起動時間をCPU完了時間と比較しない。

CPU の adapter algorithm は 0=fresh Eigen、1=LAPACKE ZGEEV、2=ZGEES+ZTREVC、3=MathNet MKL の `z_eigen` ABI、4=reusable Eigen。1/2 は OpenBLAS と direct oneMKL の両方を同じ ABI で測れる。3 は MathNet managed wrapper 全体ではなく、その native export 直接呼出しであり、MathNet の配列生成費用まで測定したとは扱わない。

研究用 CPU adapter は AVX2、配布DLLの参照は既定で AVX512 であり、両者の Eigen 速度差は workspace 再利用だけを表さない。再利用そのものの効果は **同じ adapter の algorithm0 と4** を比較して分離する。production baseline との比は、利用者の現在の経路から候補へ移ったときの差として別に記録する。

EVD 入出力の最低限の managed 保持量は概ね A+V+λ=`32N²+16N` bytes。N=640 で A/V は各6.55 MB。LAPACK の入力コピー、Schur vectors、work array、Eigen solver 内部行列はこれに加わる。GPU は device と pinned host に A/V/λ と work を持つ。**BDN MemoryDiagnoser の managed allocated だけではこのメモリを測れない**。native/GPU workspace の明示値と、必要に応じて実行中 private bytes/working set のピークを合わせる。

STEM の全スライス保持は概ね `16 S K_all N` bytes、旧 V 保持は `16 K_inside N²` bytes。既存判定 `S*K_all ≤ K_inside*N` は保持量の判断であり、solver の計算速度の閾値ではない。N=500 という既存 MKL Auto 閾値も、内側1・方向並列・全体時間とメモリの実測で定められている。単一 EVD で新たなクロスオーバーが出ても、そのまま Auto に転記しない。

計測は割り当てた計算枠で1本ずつ実施した。Idle、`DOTNET_PROCESSOR_COUNT=2`、MKL/OMP/OpenBLAS の内側1、同一の .NET/GC/JIT 設定とし、今回の実行条件は次節の **InProcess/Monitoring、1 invocation、warmup3・measurement8・launch1** である。当初想定したThroughput/iteration250msの条件では測定していない。DLLまたはthread構成を変えるときは別プロセスとし、モデルを変える最適化やGPU常駐化は別の実験として扱う。

## 6. CPU高負荷下の参考測定の実施条件

事前に他者の計算によるCPU使用率100%を確認した上で、作者の指示により低負荷を待たずに測定した。実際の主run境界サンプルはMKLが100→83%、OpenBLASが87→100%、GPUが99→87%であり、全測定期間が常に100%だったと断定しない。負荷下の参考値として保存し、性能値だけによる既定 backend の変更は行わない。[負荷サンプル](evidence/performance/load-samples.jsonl)

使用中の BDN 0.16.0-preview.2 の実 binary を `--help` と `--list flat` で確認した。主測定で使用したオプションは次のとおり。

```text
--inProcess --strategy Monitoring --invocationCount 1 --unrollFactor 1
--warmupCount 3 --iterationCount 8 --launchCount 1
--outliers DontRemove --allStats --stopOnFirstError
```

`Monitoring` は pilot・overhead 調整を省き、1反復につき1回の完了した native 呼出しを測る。`iterationTime=250` は invocationCount を明示すると計測長の制約にはならないため、このモードでは併記しない。3回のwarmup＋8回の本測定に、現在のMemoryDiagnoserのextra iterationが1回加わる。回数は限定できるが、IdleへCPU時間が供給されない状況のwall時間の上限にはならない。native呼出しの途中を安全に取消せる契約もない。上側外れ値を削除せず、負荷の変動を含む全8標本と誤差・min/maxを残す。[BDN EngineStage（使用版commit）](https://github.com/dotnet/BenchmarkDotNet/blob/962da56e99ff7ab706c3c6f0aeb24e4af91a0d4f/src/BenchmarkDotNet/Engines/EngineStage.cs)、[BDN jobs](https://benchmarkdotnet.org/articles/configs/jobs.html)

主比較は MKL ZGEEV と OpenBLAS ZGEEV の6条件×4方法=各24 benchmark、およびGPUの6条件×2方法=12 benchmarkで、全60件の8標本を確認した。追加比較の対象はMKL/OpenBLAS Schur・既存MathNetで、Si128/Si448/Si640の3条件×4方法=各12 benchmarkとした。Eigen再利用単独の速度比較は今回の主60件には含まれない。同じ実行内の各fixtureにEigen基準を置き、異なるbackendの別runから絶対時間を抜き出して速度比を作らない。同じrunでも各方法は順番に測るため、CPU負荷の時系列変化が比に残る。

### InProcess の環境と優先度

子プロジェクトのビルドを避けるため `--inProcess` を使う場合、Jobに書いたGC/JIT設定を現在のhost runtimeへ後から適用することはできない。次を **dotnet起動前** に設定し、1 backendごとに新しいdotnetプロセスを起動する。

```text
DOTNET_PROCESSOR_COUNT=2
DOTNET_gcServer=1
DOTNET_GCDynamicAdaptationMode=0
DOTNET_TieredCompilation=1
DOTNET_TieredPGO=1
DOTNET_TC_OnStackReplacement=1
COMPlus_TC_OnStackReplacement=1
MKL_NUM_THREADS=1
OMP_NUM_THREADS=1
OPENBLAS_NUM_THREADS=1
```

`DOTNET_gcServer` の綴りはこのとおり。ベンチ実行ヘッダの `// GC=Concurrent Server` が実runtimeの確認箇所であり、`--info` だけではGC方式は表示されない。InProcess validatorも実GC方式とJob設定を比較する。現在のhost runtimeconfigにはServerGCの固定値がないため、起動前environmentで指定できる。[InProcess validator（使用版commit）](https://github.com/dotnet/BenchmarkDotNet/blob/962da56e99ff7ab706c3c6f0aeb24e4af91a0d4f/src/BenchmarkDotNet/Toolchains/InProcess/InProcessValidator.cs)

使用版の `InProcessEmitExecutor` はrunner開始前にprocessをHigh、実行threadをHighestへ変更する。その後、Engine冒頭のGlobalSetupが `PrepareProcess` を呼び、processをIdleへ戻す。測定engineはその後process priorityを上げず、executor終了時は元のIdleへ復元する。したがって数値Setupと測定本体はIdleだが、直前の短いBDN準備区間ではHighとなり得る。実測中の `PriorityClass` を外部monitorでも確認する。thread側のHighest設定はIdle process class内に残る。[InProcess executor（使用版commit）](https://github.com/dotnet/BenchmarkDotNet/blob/962da56e99ff7ab706c3c6f0aeb24e4af91a0d4f/src/BenchmarkDotNet/Toolchains/InProcess/Emit/InProcessEmitExecutor.cs)、[Engine（使用版commit）](https://github.com/dotnet/BenchmarkDotNet/blob/962da56e99ff7ab706c3c6f0aeb24e4af91a0d4f/src/BenchmarkDotNet/Engines/Engine.cs)

### Setup 費用と結果の読み違い防止

現実装ではGlobalSetupが各benchmark method・各fixtureごとに走る。CPUでは1 Setupで参照EVD、参照一体伝播、候補EVD、候補一体伝播の計4 EVDと、2 LU再構成・2残差GEMM・fixture生成・binary保存を行う。6条件×4方法のrunでは計96回の検証用EVDが測定外に追加される。GPUは1 Setupあたり参照2回＋GPU1回、6条件×2方法で計36回。`--inProcess` だけではこの重複は消えない。

Setup費用を減らす将来案として、同一process内で成功した数値検証をcacheする方法がある。キーにはfixture配列hash、初期波・厚み・P、参照DLL、adapter/backend DLL hash、algorithm、managed検証assembly版を含める。同名fixtureという理由だけで他DLLの検証を使い回さない。これは検証を省略するスイッチとは区別し、未検証キーは通常の全検証を行う。今回の主測定ではcacheを実装せず、各Setupで全検証を実施した。

CPUの2つのBaselineは `EVD` と `Propagation` の別categoryであり、`ByCategory` に加えてBDNがbaseline用にparameter/jobを暗黙にgroup化する。各Scenarioの正しい基準が選ばれる。フィルタには `--anyCategories EVD Propagation` を使う。`--allCategories EVD Propagation` では両方のcategoryを持つmethodがなく、対象がゼロになる。CPUとGPUのclassは別で、GPUにはこのcategoryフィルタを付けない。[DefaultOrderer（使用版commit）](https://github.com/dotnet/BenchmarkDotNet/blob/962da56e99ff7ab706c3c6f0aeb24e4af91a0d4f/src/BenchmarkDotNet/Order/DefaultOrderer.cs)

同じ `CandidateEvd` というmethod名だけではbackendを識別できない。runタグ、backend label、DLL hash、各runのartifactを対応させて保存する。再実行は既存artifactを上書きしない。BDN hostの終了codeだけを成功判定とせず、期待件数24/12、各methodの統計値、GlobalSetup例外・失敗・未測定項目の有無を出力JSONとログで確認する。
