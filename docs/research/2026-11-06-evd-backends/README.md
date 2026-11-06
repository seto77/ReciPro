# 固有値分解backend・cuSOLVERの実機追調査

2026-11-06、261106Ch。[前回の高速化追調査](../2026-11-06-dynamical-speed-followup/README.md)からの継続。前回更新したEigenを基準に、同じ吸収あり一般複素行列をCPU/GPUの別backendで解く。調査資料だけでなく、実行可能な隔離adapter、物理行列fixture、独立検証、BenchmarkDotNet測定を作成した。本追調査では製品の既定backend・配布DLL・driverを変更していない。

## 結論の範囲

**高速化の道はある。ただしbackendの一律置換ではない。** CPUの主比較ではOpenBLAS/oneMKLのdirect ZGEEVが一部の中～大条件で短縮を示し、小さいSi128では逆に遅かった。native一括伝播の内部EVDを `LAPACKE_zgeev_work` にする方式なら、worker単位でworkspaceを再利用し、現在のLU・全厚さGEMMとSTEMの保持構造を維持できる。単にMathNet providerを切り替えると、STEMのnative全スライス経路を失う場合がある。

cuSOLVER `Xgeev` も、このPCのRTX 4060で複素倍精度・右固有ベクトルを実際に計算できた。GPU固有の精度問題は今回の9条件では見つからなかった。CPU/GPU混成算法であり、host転送・CPU負荷・FP64性能・初期化費を含めた速度評価が必要。Hermitian専用solver、単精度、吸収を省く近似には置き換えていない。

作者指定により、他の計算でCPU飽和を確認したPCで測定を続けた。主run開始・終了時のサンプルは83～100%で、高負荷は時間とともに変動している。以下の速度は参考値として扱い、Auto閾値や全アプリの速度保証には転用しない。製品backendの既定値は維持した。

## 主比較の実測

各欄は**候補時間 / 同じrunの現行Eigen時間**（平均値の比、1未満が短い）。各backendは別プロセス・別runで測定し、各run内に同じ凍結Eigenを置いた。列をまたいだ比からbackend同士の厳密な優劣は求めない。

| 条件 | oneMKL ZGEEV | OpenBLAS ZGEEV | cuSOLVER default |
|---|---:|---:|---:|
| Si128 | 2.463 | 1.828 | 5.772 |
| Si256 | 0.627 | 0.470 | 0.942 |
| Si448 | 0.985 | 0.743 | 0.908 |
| Si640 | 0.748 | 0.609 | 0.510 |
| Au448 | 0.926 | 0.691 | 1.025 |
| GaAs256 | 0.559 | 0.423 | 0.809 |

CPU2種×24件、GPU12件、計**60 benchmarkすべてで8標本の完了を確認**した。[平均・SD・中央値比の全表](evidence/performance/timings.md)、同リンク先のFullJSONには8標本・Min/Max・CIも残す。[負荷サンプル](evidence/performance/load-samples.jsonl)の`runTag=bounded`ではoneMKL 100→83%、OpenBLAS 87→100%、GPU 99→87%。runTagなしの先行記録は測定クラスの制約で計測開始前に止まった試行なので、速度結果に含めない。1方向ずつの実験であり、多方向を全コア並列にした速度とは異なる。

- **CPU一括処理にも効果が残る条件がある。** OpenBLASのGaAs256はEVDで122.8→51.9 ms、EVD＋LU＋3厚さ伝播で127.8→56.4 ms（約2.27倍）。Si640の一括処理は947.5→630.9 ms（約1.50倍）。後段のISA差（参照AVX512、adapter AVX2）も含む実装全体の観測値で、EVDだけの寄与には分解しない。
- **GPUも大行列では検討に値する。** Si640のEVDは944.1→481.0 ms（約1.96倍）。host staging、H2D、Xgeev、固有値・右vector・infoのD2H、同期、host出力までを含む。初期化は含まない。Si128では8.84→51.02 msと約5.77倍遅く、Au448はほぼ同等だった。GPU後段までの一括処理や全STEM像の倍率ではない。
- **小行列はEigenを残す根拠がある。** 今回のSi128では全候補が遅い。要求反射数だけで閾値を定めず、実効N、材料、外側の並列数、厚さ数、メモリを含めてAutoを再測定する。

高負荷はGPUにも影響する一方、CPU側の競合が強いためGPUへの移行が有利に見える場合もある。この結果を無負荷PCへ外挿しない。InProcess/Monitoring、warmup3・測定8・1 invocation・外れ値除去なしを使った理由と限界は[protocol](protocol.md)へ記した。BDNの既定電源プランは測定時に高パフォーマンスへ切り替わり、主比較の終了後に元のバランスへ戻ったことを確認した。

### Schur方式と既存MathNetの追加比較

Si128/448/640で3方式を追加測定した。以下も各run内のEigenに対するEVD平均時間比であり、異なる列の比は計算しない。

| 条件 | oneMKL ZGEES＋ZTREVC | OpenBLAS ZGEES＋ZTREVC | 既存MathNet native z_eigen |
|---|---:|---:|---:|
| Si128 | 2.112 | 2.223 | 2.716 |
| Si448 | 0.987 | 0.757 | 1.198 |
| Si640 | 0.769 | 0.677 | 1.061 |

追加36件を含めて**合計96 benchmark、各8標本を完了**した。Schur経路でも大行列の改善があり、「ZGEEVへの切替だけが原因」「workspace再利用だけが原因」とは断定できない。既存MathNetのnative exportは今回の3条件でEigenより短くならなかった。ただしmanaged `Matrix.Evd()`全体を測った値ではなく、既存STEM Auto閾値の根拠をこの高負荷・1方向測定だけで否定するものでもない。

Eigen solverのfresh/reuseは数値検証したが、同一adapter内で再利用だけを分離したBDN速度測定は未実施。direct LAPACKのhigh-level API対work APIも同一runtime内で比較していないため、確保削減単独の倍率は主張しない。

## 数値検証

CPU 7方式（fresh/reused Eigen、oneMKLのZGEEV/Schur、既存MathNet native Schur、OpenBLASのZGEEV/Schur）とcuSOLVERの**8候補×9条件=72件が全通過**。基準Eigenを加えた**81件**を、固有値分解を使わない `scipy.linalg.expm(2πiAt)ψ₀` とも照合した。厚さ0/35/100nm、入力非破壊、全値finite、残差、境界、P重み付き法線流束、無吸収時の保存を検査した。[全oracle結果](evidence/expm-oracle.json)、[数値要約](evidence/numerical-summary.json)、[入力条件と行列hash](evidence/fixtures/)。

| backend | 条件数 | 最大正規化残差 | 独立expmとの最大波動差 | 一括伝播とexpmの最大差 |
|---|---:|---:|---:|---:|
| 現行Eigen基準 | 9 | 7.12e-16 | 3.27e-13 | 別途の一括出力保存なし |
| Eigen fresh / reuse | 各9 | 7.18e-16 | 3.27e-13 | 5.06e-13 |
| oneMKL 2026.2 ZGEEV | 9 | 4.09e-16 | 3.27e-13 | 5.46e-13 |
| oneMKL 2026.2 Schur | 9 | 3.92e-16 | 3.27e-13 | 5.06e-13 |
| 既存MathNet native Schur | 9 | 4.18e-16 | 3.27e-13 | 5.46e-13 |
| OpenBLAS 0.3.34 ZGEEV | 9 | 4.60e-16 | 3.27e-13 | 5.46e-13 |
| OpenBLAS 0.3.34 Schur | 9 | 4.64e-16 | 3.27e-13 | 5.06e-13 |
| cuSOLVER（CUDA 13.3.1）default | 9 | 5.51e-16 | 3.27e-13 | GPU後段は未実装 |

波動差は参照波全体のピークで正規化する。最大値の多くは固有ベクトル条件数κ₂≈20,000の近縮退合成例から来る。実試料はSi128/256/448/640、Au448、GaAs256と無吸収Si/GaAs32。詳細は[検証・計測条件](protocol.md)と[接続契約](integration-contract.md)。小Nの先行smokeも63/63通過したが、主比較は全backendで共通の物理行列である。

これは構築済み行列の固有値分解と伝播を検証する試験であり、ポテンシャル模型全体や全実験条件を保証する試験ではない。EBSD源積分、GPU内LU/GEMM、全STEM像のend-to-end速度は今回のGPU検証に含まれない。物理模型の監査は[前回の動力学調査](../2026-11-06-dynamical-audit/README.md)を参照。

## 算法を区別する理由

現行Eigenのcomplex Schurはsingle-shiftのGivens QR。OpenBLAS 0.3.34のZGEEVにはblocked Hessenberg reduction、条件に応じたmultishift QR・aggressive early deflation、ZTREVC3のBLAS3 back-transformがある。一方、既存MathNet nativeはZGEES＋ZTREVCであり、ZGEEVではない。workspace再利用だけでなく、balancing、算法、内部copy、D行列、版、ISAが異なる。oneMKLの非公開内部実装までOpenBLASと同じとは断定しない。[一次資料とC++監査](cpu-backends.md)。

Eigen公式の2026年10月Zen5比較は実数GEEVであり、ReciProの複素行列への倍率転用はしない。`EIGEN_USE_LAPACKE`の一括有効化もLU等へ影響するため、今回の明示的EVD adapterとは別の変更になる。

## 実機と再現用成果物

- CPU：AMD Ryzen 9 9950X、16 core/32 logical。GPU：RTX 4060 8GB、driver610.88、CUDA driver/runtime API13.3、CC8.9、照会したFP32/FP64比64。
- CPU：oneMKL2026.2.0、OpenBLAS0.3.34 LP64、既存MathNet native DLL。GPU：CUDA13.3.1 redistributables、cuSOLVER12.2.6.9。追加runtimeは`C:/tmp/recipro_evd_261106/`へ隔離し、system PATHやPython環境へインストールしていない。
- 基準native AVX512 SHA256：`78b2b691de805fb7eed7e0f877561883d346b1f781303c75f906ed85ef894174`。試作adapterはAVX2、SHA256：`ee18cbf60eee82e28e8962e22510eb466232f1d5ae1d95ab9bed99f4f11fca27`。一括伝播の差には後段EigenのISA差も含む。
- 全DLL・source識別：[manifest](evidence/experiment-manifest.json)。GPU取得元・SHA・workspace：[GPU記録](cusolver.md)。C++・C#・Pythonハーネス：[tools/EvdBackendStudy261106](../../../tools/EvdBackendStudy261106/README.md)。BDN本体：[EvdBackendBenchmark.cs](../../../ReciPro.Benchmarks/EvdBackendBenchmark.cs)。
- Validation Release x64 build成功（既存WinForms analyzer警告5、エラー0）。本追調査は研究コードのみを追加し、製品コードの全回帰試験を新たに実施したものではない。前回の6,717件成功と混同しない。

GPUのN640では、adapterが直接確保するdevice側約18.79MiB、pinned host側約28.51MiBに加え、CUDA context/libraryの内部メモリが必要。初期化は別途測る。BDNのmanaged allocation欄だけでnative/GPUの保持量を評価しない。

## 本番へ接続する際の判断

1. nativeのEVD→LU→全厚さGEMMを維持した選択可能backendを先に評価する。1 worker=1 context、サイズと寿命を管理し、既定Autoは全工程・方向並列・メモリの比較後に変更する。
2. `info`、LU失敗、非有限出力、確保・DLL探索失敗を明示的に扱う。既存void ABIへの無言fallbackで速度と精度の結果を混ぜない。
3. 新旧MKL/OpenMPの混在を防ぐstartup管理、配布runtimeのライセンス・容量・非対応CPU/ARM64のfallbackを詰める。今回のoneMKLはsequential LP64で隔離検証した。
4. GPUはまずhost入出力方式の実測に従う。将来GPU内LU/GEMMや複数方位の滞留を実装する場合も、今回未測定の速度を前提にしない。CUDA13.5 ALG_1は別driver・別算法・別精度検証の案件である。

公開リポジトリ・submoduleへのcommit/pushは行っていない。研究スクリプトのローカル履歴と、公開対象外のhandoverは別管理する。

ローカル履歴はtools `74ee8246`（本体）＋`a1828775`（取得sourceのbyte保持）、Benchmarks `1dd0d09`、設計書 `f3808cc` に保存した。これらはリモートのない履歴リポジトリで、pushしていない。[最終確認](evidence/final-verification.json)には96件×8標本と、測定前後の9行列SHA一致を記録した。
