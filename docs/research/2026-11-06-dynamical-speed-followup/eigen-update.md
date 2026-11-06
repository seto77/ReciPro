# Eigen更新と性能・精度の比較（261106Ch）

## 導入した版

公式サイトが示す最新の安定版は[Eigen 5.0.1](https://libeigen.gitlab.io/)（2025-11-11）である。一方、ReciProにはそれより新しい開発版 `17d8ddbd22c277c6bd20e19e5b8d128b0135a9f4`（2026-09-03）が既に入っていた。安定版へ巻き戻す変更はせず、既存の開発版追従を継続し、調査時の上流HEADを以下のコミットに固定して更新した。

- 上流: [`5a19218d5a28bfc38ca6b1475ea040c8f73257ba`](https://gitlab.com/libeigen/eigen/-/commit/5a19218d5a28bfc38ca6b1475ea040c8f73257ba)（2026-11-06）。[取得時のメタデータ](evidence/eigen/upstream-commit.json)。
- 版文字列: 旧新とも `5.0.1-dev+master`。文字列だけでは更新の有無を判断できない。
- 取得元: GitLabの公式repository archive API、コミット固定zip。SHA-256: `453b9ab5765c8752d65a39713af2afbd644c56a99df88d3ca7c578374794aede`。
- `Eigen/`、新しい `contrib/`、互換用 `unsupported/` の1,035ファイルを取得物とバイト単位で照合。独自パッチは加えていない。旧vendorは変更前zipにも保存し、git履歴でも追跡可能。
- 上流で `unsupported/` が `contrib/` に移ったため、MatrixFunctionsのincludeを新しい場所へ変更した。旧include行はコメントとして残した。
- 上流のライセンス本文を `Crystallography.Native/eigen-licenses/` に保存し、[第三者通知](../../../Crystallography.Native/THIRD-PARTY-NOTICES.md)へ出典・ハッシュ・更新履歴を記した。

## 同一条件でのBenchmarkDotNet比較

旧新のAVX-512 DLLを絶対パスで同じプロセスへ明示ロードした。通常のNativeWrapperによる自動選択やmanaged fallbackの比較ではない。旧DLLは前回の積分安定化を含む今回調査開始時の版、新DLLは**Eigenだけを更新した段階**で凍結し、後続のEBSD最適化と混ぜていない。

Si、200 kV、[001]から0.5°、表面法線23°傾斜、吸収ありの実Bethe行列。要求・実反射数とも128/256/448。C#側の行列作成は計測外。同じ入出力配列を再利用し、EVDとEVD+LU+3厚さのCBEDを別々に測った。

| 区間 | N | 旧 | 更新後 | 更新後/旧 |
|---|---:|---:|---:|---:|
| EVD | 128 | 4.896 ms | 4.825 ms | 0.99 |
| EVD | 256 | 105.738 ms | 106.580 ms | 1.01 |
| EVD | 448 | 171.375 ms | 171.665 ms | 1.00 |
| CBED（EVD+LU+厚さ0/35/100 nm） | 128 | 5.021 ms | 4.854 ms | 0.97 |
| 同上 | 256 | 107.478 ms | 106.400 ms | 0.99 |
| 同上 | 448 | 175.856 ms | 175.193 ms | 1.00 |

今回の条件では実用上ほぼ同等で、Eigen更新自体による大幅な高速化は認められない。固有値反復の費用は行列にも依存するため、Nに対する時間が単純なN³比になるとは限らない。今回の3点の差の原因は切り分けておらず、少数点から一般的な費用曲線を認定しない。

[全測定表・標準偏差・信頼区間](evidence/eigen/EigenVersionBenchmark.md)、[全ログ](evidence/eigen/benchmark-run.txt)。BenchmarkDotNet 0.16.0-preview.2、.NET 11 RC3、Idle、processor count 2、MKL/OMP 1、8 iterations×250 ms、warmup3、launch1。BLAS/MKLへの切替は行っていない。

## 精度とビルド

各Setupで以下を検査してから計測した。

- 固有ベクトルの順序・任意位相を直接比較せず、正規化Frobenius残差 `||AV−VΛ||F / (||A||F ||V||F + ||VΛ||F)` を確認。最大約 `5.98e-16`。
- 共通のmanaged LUで波を再構成し、旧新の波の最大差/共通ピークは最大約 `1.43e-13`。
- nativeのfused CBEDとの一致、t=0の入射境界、入力行列の非破壊性も確認。
- これらは固定した有限行列の検証であり、全てのEigen機能や実験との一致を保証するものではない。

更新直後に標準のVisual Studio/ClangCL経路でx64のgeneric・avx2・avx512をビルドした。[ビルドログ](evidence/eigen/native-build.txt)。`/fp:fast`下の無限大に関する警告とEigen内部の静的解析警告は残っており、警告を隠す変更はしていない。現行有限入力の数値検証と、任意のNaN/Inf入力の保証を混同しない。最終コードのx64/ARM64ビルド・回帰試験は[統合記録](README.md)にまとめる。

ベンチマークのソースはローカル専用 `ReciPro.Benchmarks/EigenVersionBenchmark.cs`、旧新DLLと取得zipは `C:/tmp/recipro_speed_261106/` に保存。公開リポジトリへのコミット・pushは行っていない。
