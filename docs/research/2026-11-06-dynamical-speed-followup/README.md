# 動力学計算の高速化追調査・Eigen更新

2026-11-06 / 261106Ch。[前回監査](../2026-11-06-dynamical-audit/README.md)後のコードを出発点として、C#・C++・既存研究記録・公式ライブラリ仕様を再調査し、候補を実測して採否を決めた。**高速化の余地はあり、物理近似を増やさない4件の変更を実装した。** Eigenも、調査時点の上流開発版へコミットを固定して更新した。

## 1. 実装と測定結果

| 採用した変更 | 測定結果・適用範囲 |
|---|---|
| C++ EBSD：非等間隔深さの指数を固有モードごとに因数分解 | **カーネル部分で4.26～7.36倍**。複素指数の回数はO(TN²)→O(TN)。固有値分解・beam選択等は測定外 |
| HRTEM：光学位相を反射×焦点ごとに保存し厚さ間で共有 | **係数構築部分で1.85～4.15倍**。一時配列の確保も削減。固有値分解・画素合成は測定外 |
| 通常回折・PED：`V_inverse * e0`を第一列の直接参照に変更 | 同じ数学的結果のための行列積と一時配列を省略。取得部分のBDNを保存。回折全体の倍率は認定しない |
| 非局所EBSD：出力に使われない局所源の計算を省略 | C++・managed両経路へ反映。両出力が必要な公開APIの契約は維持。上記EBSD倍率に省略分を上乗せしない |

BenchmarkDotNetで同一run内の旧新を比較した。Ryzen 9 9950X、.NET 11 RC3、Idle優先度、.NET processor count 2、MKL/OMP 1 thread。8 measurement iterations×250 ms、warmup 3、launch 1。誤差・標準偏差・信頼区間は各保存表にある。複数チャットが使う共用環境なので、少数の平均値から精密な普遍的倍率を認定しない。

- [C++の調査・採用案・不採用案](native-speed.md)
- [C#の調査・採用案・メモリ費用](managed-speed.md)
- [文献・OSS・既存研究からの再評価](research-review.md)
- [Eigenの更新内容と旧新DLL比較](eigen-update.md)

因数分解・位相共有は数式上等価だが、浮動小数点の全入力でビット一致する保証ではない。EBSDは大きい位相・指数に旧式へのfallbackを残し、前回導入した弱吸収・近縮退の安定な積分級数を保持した。HRTEMは各qへの加算順を維持し、巨大な全反射対・全厚さキャッシュは作らない。

## 2. Eigenは何を「最新」にしたか

公式の最新安定版は[5.0.1（2025-11-11）](https://libeigen.gitlab.io/)。導入済みだったのは、それより新しい2026-09-03の開発snapshotであった。既存の開発版追従を継続し、2026-11-06に確認した上流HEAD [`5a19218d5a28bfc38ca6b1475ea040c8f73257ba`](https://gitlab.com/libeigen/eigen/-/commit/5a19218d5a28bfc38ca6b1475ea040c8f73257ba)へ更新した。版文字列は旧新とも`5.0.1-dev+master`なので、更新の識別にはコミットIDを使う。

公式コミット固定zipから`Eigen/`、`contrib/`、互換`unsupported/`の**1,035ファイルをバイト一致で導入・再検証**し、旧版だけに残るファイルを整理した。上流ライセンス本文と取得物のSHA-256も保存した。MatrixFunctionsのincludeは上流の`contrib/`へ移した。

Eigenだけを更新したDLLを別途凍結し、後続のC++最適化と効果を分離した。実Si Bethe行列N=128/256/448のEVD・EVD+LU+CBEDは旧新でほぼ同等。**今回の大きな高速化はEigenの版更新だけから生まれたものではない。** 残差と波の再構成も比較した。詳細と取得元は[Eigen更新記録](eigen-update.md)。

## 3. 最終コードの検証

最終native DLLを明示的にテスト出力へコピーし、実ロードを確認した。別の出力フォルダの古いDLLやmanaged fallbackをnative検証の代用にしていない。

| 検査 | 結果 |
|---|---|
| `dotnet build ReciPro.sln -c Release -p:Platform=x64 -m:1` | 成功、0 errors。既存のWinForms analyzer警告5件 |
| Visual Studio / ClangCLのnativeビルド | x64 generic・avx2・avx512、およびARM64で成功。PE machineもx64=8664/ARM64=AA64を確認 |
| 全xUnit | **6,717 passed、0 failed、0 skipped**。今回追加はHRTEM4件・空局所源10件 |
| 旧DLLとのAPI互換 | 空局所源の新10テストを旧nativeでも実行、10 passed / 0 skipped |
| Native EBSD候補の独立検証 | **10,540出力が通過**、最大正規化誤差1.93e-14。近縮退・弱/強吸収・空局所源等を含む |
| 最終DLLのBloch直接求積 | **各x64 flavor 432件**、全件通過。最悪の絶対誤差/厚さは約1.99e-14 |
| EBSDの独立RK4＋Simpson参照 | native/managedの局所源・非局所源・吸収背景が通過。最悪相対差9.56e-12（有限刻みの数値参照との比較） |
| 無吸収のP重み付き流束 | Si/GaAs、傾斜面を含め通過。最大`abs(flux−1)`=2.44e-15 |
| 実結晶の旧新版比較 | **129データセット、各版6,559,199個の実数値**。全値有限、欠落なし、全てpeak正規化差1e-12以内 |

文書は本文・証拠への相対リンクの実在を確認し、[MkDocs strict build](evidence/validation/mkdocs-strict.txt)も成功した。この研究記録自体は`docs/src`の外にあり、MkDocsの生成対象には含めない。

ARM64はコンパイルとPE確認までで、ARM64実機での数値検証・性能測定は行っていない。`/fp:fast`とEigen内部に由来するビルド警告は残っている。有限の試験入力での検証結果を、任意のNaN/Infinity入力や全てのEigen機能の保証とは扱わない。

実結晶の比較を細分すると以下になる。比較対象は前回監査済み・今回速度変更前のDLL。旧新とも同じ.NET 11のハーネスと依存DLLで動かし、Core/nativeだけを明示的に切り替えた。

| 条件 | データセット数 | 最大peak正規化差 |
|---|---:|---:|
| 5結晶×CBED/potential/STEM/EBSD/HRTEM、generic | 40 | 3.245e-13 |
| 同上、avx2 | 40 | 2.574e-13 |
| 同上、avx512 | 40 | 3.261e-13 |
| Si STEM、要求448/実効441 beams、division24、slice0.5 | 3 | 4.260e-14 |
| Si/Au EBSD、非等間隔9深さ、局所源 | 2 | 1.078e-14 |
| 同上、非局所源＋0.02 nmぼかし | 2 | 6.512e-15 |
| Si/Au EBSD、managed、非局所源＋ぼかし、非等間隔5深さ | 2 | 1.160e-15 |

上表のquick HRTEMはquasi-coherent経路である。今回変更したTCC経路は、別途`HrtemTccTests`の独立な全順序対参照と、BDN Setupでの係数・native画像照合によって検証した。

既存BetheBenchの通常比較は双方NaNを許し、両側で同じデータセットが欠落しても検出できない。今回専用の独立binary readerで**全値finite、期待件数、配列次元、実ロードDLL、managed/native指定**を追加確認した。[検証JSON](evidence/validation/integration-finite-check.json)に全caseの実ロード情報を残した。

[全テスト結果の要約とnative有効の出力](evidence/validation/test-summary.json)、[xUnitログ](evidence/validation/tests-final-full.txt)、[ビルドログ](evidence/validation/solution-final-build.txt)、[x64 nativeビルド](evidence/validation/native-final-x64-build.txt)、[ARM64ビルド](evidence/validation/native-final-arm64-build.txt)、[直接求積](evidence/validation/BlochIntegralCheck-avx512.txt)、[EBSD独立参照](evidence/validation/EbsdSolverCheck-avx512.txt)、[流束検査](evidence/validation/BetheDivCheck-avx512.txt)。

## 4. 見送った案と、まだ残る高速化の道

**「高速化はもう不可能」と結論づける根拠はない。同時に、演算数が減るという理由だけで全候補を採用しない。** 今回は次の負の結果も残した。

- C++の上三角pack＋逆数事前計算は、等間隔深さで34～63%遅くなった。採用せず、非等間隔の指数因数分解だけへ絞った。
- 採用後の等間隔深さの新/旧時間比は0.95～1.12で、信頼区間が重なる。等間隔での高速化や精密な無影響は認定しない。
- HRTEMで全厚さを同時処理する案はさらに速かったが、全厚さのq係数保持によるピークメモリ増を避け、位相共有だけを採用した。
- Eigen更新のEVDはほぼ同速だった。古いMKL不採用記録には、DLL不足でmanagedへ落ちていた比較もあるため、LAPACK全般の否定に使わない。

次の優先順位は、既存経路での費用と検証条件に基づく。[文献・実装再評価](research-review.md)に一次資料と詳しい条件をまとめた。

1. **固有値分解のbackendと一括処理を再評価する。** 今回の大きいSTEM例は全41.64 s中、EigenSolve区間が38.62 sだった。単発の時間で精密な倍率は出せないが、支配段がどこかは明瞭である。native LAPACK/MKL、solver/workspace再利用、EVD→LU→全厚さ伝播のコピー費用を実行時DLL確認付きで比較する。以前の実MKL測定には大Nで優位な条件がある。
2. **新しいcuSOLVER非Hermitian EVDを条件付きで試す。** Eigenの新GPUモジュールの固有値solverはHermitian用であり、現行吸収行列へ直接置換できない。一方、cuSOLVER `Xgeev` は複素倍精度の一般行列を対象にする。CUDA 13.3～13.5の変更は再評価の理由になるが、API・版・host/device workspace・FP64性能・近接固有値の精度を確かめ、方向バッチ全体で測る必要がある。**GPU版の実装・実測は今回行っていない。**
3. **STEMの前処理・縮約の残る費用を分離する。** q外側並列によるlock削減、重複差hklのlookup削減、q-entryの二重保持解消は候補。既存の全厚さGEMM・qタイルを壊さず、加算順やUの評価点が変わる影響を検証する。
4. **計算方式を変える研究は誤差予算付きで継続する。** physical-optics Bloch/SCA、既存FFT–Krylov研究、source低ランク化、整合格子でのFFT/NUFFTなど。PRISM論文の単純multislice比を、そのまま既に伝播を再利用しているReciProへ移さない。

この追調査は、現行の有限beam・吸収模型を保った高速化とその数値検証である。実験との模型誤差、beam/角度/厚さの収束、新しい物理出力の評価は[前回の横断監査](../2026-11-06-dynamical-audit/ReciPro_DynamicalAudit_261106.md)と区別して扱う。

## 5. 再現・保存場所

本番変更は正本submoduleの[BetheMethod.cs](../../../Crystallography/Diffraction/BetheMethod.cs)、[NativeWrapper.cs](../../../Crystallography/NativeWrapper.cs)、[EigenFuncs.cpp](../../../Crystallography.Native/EigenFuncs.cpp)とvendor。新テストは[HRTEM](../../../Crystallography/tests/Crystallography.Tests/HrtemTccTests.cs)・[空局所源](../../../Crystallography/tests/Crystallography.Tests/EbsdLocalSourceSkipTests.cs)に保存した。前回監査の未コミット変更も同じファイルに含まれるため、今回分だけの単純なHEAD diffとは一致しない。

ローカル履歴リポジトリには以下を保存した。これらは公開クローンには含まれない。

- `tools/NativeEbsdSpeedStudy/`：旧実装と3候補、数値検証、採用・不採用の測定証拠。
- `tools/DynamicalSpeed261106/`：旧新版の隔離手順、順次実行PowerShell、独立binary検証。
- `ReciPro.Benchmarks/`：`NativeEbsdKernelBenchmark.cs`、`HrtemCoefficientBenchmark.cs`、`EigenVersionBenchmark.cs`。
- `.project-guidance/ReciPro/ReciPro_BetheMethod技術基準.md`：今回の結果への追記。

[最終ソース・DLL・取得zipのSHA-256一覧](evidence/validation/manifest.json)を保存した。旧Core/DLL・Eigenだけ更新したDLL・最終DLL、実測binary、全TRXは`C:/tmp/recipro_speed_261106/`にある。`evidence/`にはBDN表・統計JSON・主要ログ・全比較結果・工程別単発時間を収録した。単発の`.bin.timing.md`は再現条件・支配段の確認用であり、精密な性能比較の根拠にはしない。

並行作業による.NET移行等が同じworkspaceに存在する。実行時のassembly commit文字列だけで未コミットのソースを識別せず、隔離したDLLのハッシュを識別子とする。公開リポジトリ・submoduleのcommit/pushは実施していない。

ローカル履歴への保存コミット: tools `f8d6223c`、benchmarks `27841e2`、技術基準 `e360c7a`（いずれもpushなし）。
