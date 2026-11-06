# 測定・数値検証の条件（261106Ch）

この追調査は、[前回の高速化](../2026-11-06-dynamical-speed-followup/README.md)後のnative Eigenを基準とし、吸収を含む一般複素行列を同じ物理条件で解くbackendを比較する。既定のHermitian化、吸収の摂動化、単精度化は行わない。

## 入力と判定

- 主測定：Siの要求128/256/448/640 beams、Au448、GaAs256。200 kV、[001]から0.5°傾斜、表面法線23°傾斜。実採択反射数・hkl・温度因子・展開原子数はfixtureのJSONに記録する。
- 精度のみ：Si/GaAsの無吸収32 beams、および8次元の近縮退・非正規合成行列。合成行列を実試料の速度表へ混ぜない。
- 深さ：0/35/100 nm。固有ベクトルの並び・任意位相は直接一致を要求せず、初期波をLUで展開して再構成した波を比べる。
- 正規化残差 `||AV−VΛ||F / (||A||F ||V||F + ||VΛ||F)` は1e-11以下。波の共通ピークによる正規化差は1e-10以下。t=0境界は1e-11以下。P重み付き流束と、B=0時の保存も検査する。全値finite、収束ステータス成功、入力非破壊を必須にする。
- 別にSciPyの行列指数 `expm(2πiAt)ψ0` で照合する。EVDを使わない参照であり、固有値分解同士だけの一致では終わらせない。固有ベクトル行列の条件数も記録し、小さい残差だけで悪条件の伝播精度を保証しない。

## 比較する区間

1. EVD：入力保持用コピーと、返却用の固有値・右固有ベクトルを含む。
2. EVD＋LU＋全3厚さ伝播：CPU adapterは同じEigen LU/GEMMを使い、backendだけの変更と後段の接続費を比較する。これは全STEM画像生成の時間ではない。
3. cuSOLVER：host staging、H2D、Xgeev、W/V/infoのD2H、同期・収束確認を含む。初期化とworkspace確保は分けて記録する。GPU上に後段をすべて保持する方式とは区別する。

CPU adapterは検証済みのAVX2版を使用し、基準は固定したproduction AVX512 DLLとする。追加AVX512ビルドはCPU飽和下で極端に待たされたため中止した。CPU EVDの主処理は選択したBLAS/LAPACKの内部dispatchに従い、後段のEigen LU/GEMMにはadapter AVX2と本番AVX512の差が残る。したがってfused時間差をEVD算法だけの効果には分解しない。solver/workspace再利用だけの寄与を論じる場合は、同じadapterのfresh/reuseを比較する必要がある。

## 環境と再現性

重い作業はIdle優先度・1ジョブずつ、`DOTNET_PROCESSOR_COUNT=2`、`MKL_NUM_THREADS=1`、`OMP_NUM_THREADS=1`、`OPENBLAS_NUM_THREADS=1`。native CPU backendは1 solver workerで測る。多数の方位をCPU全コアで並列処理したときの速度へ直接外挿しない。

精密比較にはBenchmarkDotNetを用いる。初期化・数値検証はGlobalSetupに置く。単発のsmokeやvalidationの所要時間から速度優位を主張しない。run間の絶対時間ではなく、同じrun内の固定基準との比較と誤差を使う。

固有値分解の実装方式も分ける。MathNet MKLはZGEEVではなくZGEES＋ZTREVCを使う。新oneMKLと旧MathNetの差には、版・算法・コピー・D行列・workspaceの費用が含まれる。[MathNet native source](https://github.com/mathnet/mathnet-numerics/blob/master/src/NativeProviders/Common/lapack.cpp)。LAPACKのworkspaceは最小長でなくqueryした推奨長を使う。[LAPACK ZGEEV](https://www.netlib.org/lapack/explore-html/d4/d68/group__geev_ga8656466cd9da86a0b3b6966e49a7ce40.html)。

追加runtimeは`C:/tmp/recipro_evd_261106/`へ隔離し、公式配布物のURL/SHA、実DLL、fixture、全出力を保存する。Python環境・system PATH・GPU driverを変更しない。最終の採否・実測値は、このフォルダの統合READMEへ記録する。

## 負荷条件の追記

2026-11-06、他の計算によるCPU使用率100%と、IdleのcompilerへのCPU時間供給が極端に少ない状態を確認した。作者は「現在の負荷下で測り、参考値として記録する」と指定した。このため以下のBDN実行は低負荷時の採用判定用ベンチではなく、現在の負荷下での参考比較として扱う。物理・数値ゲートは通常どおり厳守し、性能結果だけで既定backendを変更しない。

この条件では、通常のpilotによる反復数調整と既定unroll=16が長い待ち時間を増幅するため、実測は **BDN InProcess / Monitoring、invocationCount=1、unrollFactor=1、warmup=3、measurement=8、launch=1、outliers=DontRemove** に変更した。各backendは別のdotnetプロセスで起動し、同じrun内のEigen基準を保持する。1 iterationの呼出数を限定する設定であって、wall timeの上限を保証する設定ではない。

InProcessではJob環境変数だけに依存せず、host起動前にServer GC、DATAS無効、TieredCompilation/PGO/OSR有効を設定する。BDNの実行ログで`GC=Concurrent Server`を確認する。BDNは実行準備時にHighへ上げるが、その後のGlobalSetupでIdleへ戻し、数値検証・測定中には再昇格しないことを当該版のソースで確認した。実プロセスも観測する。数値検証は反復ごとに省略せず、各benchmarkのGlobalSetupで行う。

サンプル数が少なく、CPU負荷も時間とともに変わるので、MeanだけでなくMedian/Min/Max/StdDevを残す。異なるrun間の比は作らない。同じrun内でもbaselineとcandidateは同時・交互実行ではないため、負荷変動の交絡が残る。外れ値を落として都合のよい速度へ整えない。GPUもhost処理を含むので、この負荷の影響を受ける。

EVDと一括伝播も別々に測るので、両者の平均時間の差をLU/GEMM単独の所要時間とは扱わない。出力コピーの契約も異なる。BDNが警告する短いiteration、外れ値検出、CIの広さをログごと保持する。`DontRemove`では外れ値と表示されても8標本をすべて集計に含める。

完了時の負荷ログでは、成功した主runの開始・終了サンプルはoneMKL 100→83%、OpenBLAS 87→100%、GPU 99→87%だった。事前に飽和を確認した事実と、測定中ずっと100%だったという主張は区別する。追加Schur比較は97→97%、96→99%、既存MathNetは91→100%。これはrun境界のサンプルであり、全iteration中の連続計測ではない。[実測ログ](evidence/performance/load-samples.jsonl)。

最初のInProcess試行はbenchmark classがsealedであったためBDN validatorが測定開始前に拒否した。2classのみunsealedへ変更し、build成功（警告0・エラー0）後に`bounded`タグで主測定を開始した。数値fixture・solverの実装は変更していない。BDNはこのような拒否でもexit0の場合があるため、runnerにFullJSON件数と各Statistics.N=8の確認を追加した。先行runTagなしの負荷記録・計測なしログを実測値へ混ぜない。検証時と測定時のsource/binary識別は[manifest](evidence/experiment-manifest.json)の別項目に残す。
