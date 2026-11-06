# C#側の再調査と採用した高速化（261106Ch）

前回のTCC差反射集約を終えた状態を出発点として、`BetheMethod.cs` の係数構築・固有解再利用・STEM縮約・EBSD境界を再調査した。C++や固有値分解だけが高速化対象ではなく、その前後に残った重複計算にも測定可能な余地があった。

## 1. HRTEMの光学位相を再利用する

現行TCCは厚さごと、反射対 `(g,h)` ごと、焦点ごとに `exp(-iχ_g)` と `exp(+iχ_h)` を再計算し、対ごとの一時 `Complex[defocusCount]` を確保していた。しかしχは反射と焦点だけで決まり、厚さに依存するのは波動関数の振幅である。

採用した変更は次のとおり。

- 方位・絞り・光学条件を固定した1回のHRTEM呼び出しで、`exp(-iχ_g)` を反射×焦点の配列に一度だけ保存する。各反射の面内ベクトルとその長さ二乗も一度だけ保存する。
- 反射対では保存した位相と他方の共役を使う。時間包絡は焦点ループの外で一度計算する。
- 一時のレンズ係数配列を作らず、差反射qの係数へ直接加算する。
- 厚さごとの画像合成順、各qへの反射対の加算順、光学因子の乗算順を保つ。全反射対のTCCや全厚さのq係数を保持する変更は採用しない。

追加保持量は配列ヘッダを除いて `16×N×D + 24×N` bytes、複素指数の回数は `O(TDN²)` から `O(DN)` へ減る。空間包絡と反射対の縮約は引き続き `O(TDN²)` なので、計算全体の次数が線形になるわけではない。物理近似や反射の打切りは増やしていない。

BenchmarkDotNetで**係数構築部分だけ**を比較した結果は以下。Nは反射数、Tは厚さ数、Dは焦点数。Si、200 kV、[001]から0.5°傾斜、Cs=0.8 mm、β=0.15 mrad、δ=3 nm。固有値分解と画素合成は計測区間に含めない。

| N / T / D | 修正前 | 位相再利用案 | 平均時間の比（前/後） | 確保量：前 → 後 |
|---|---:|---:|---:|---:|
| 50 / 1 / 1 | 117.30 μs | 28.26 μs | 4.15倍 | 348.76 → 52.34 KB |
| 150 / 1 / 3 | 3.695 ms | 2.001 ms | 1.85倍 | 4801.70 → 1805.33 KB |
| 150 / 5 / 3 | 19.348 ms | 10.066 ms | 1.92倍 | 24007.62 → 8979.06 KB |
| 300 / 3 / 7 | 82.685 ms | 26.332 ms | 3.14倍 | 58824.33 → 14423.41 KB |

[測定表と誤差・標準偏差](evidence/managed/HrtemCoefficientBenchmark.md)。8 iterations、各250 ms、warmup 3、launch 1。特にN=150の候補はGCを含む変動が大きく、精密な速度比や全HRTEMの倍率としては使わない。ベンチマークは固定した厚さ別振幅から両案の係数を構築する。実装そのものの画像比較は別に行う。

さらに、反射対を外側、厚さを内側へ移す案も比較した。150/5/3は5.394 ms、300/3/7は17.256 msだったが、元の本番コードが1厚さずつ破棄していたq係数を全T保持するため、ピークメモリが増え得る。今回は採用せず、比較用実装として残した。BDNの確保量は総量であり、このピーク保持量の違いを表していない。

両候補を各4条件で検証したところ、現在版から構築したq係数と24×19画素のnative画像はいずれもビット一致した。これは試した有限条件の事実であり、浮動小数点の全入力での一致保証ではない。実際の `GetHRTEMImage` では、既存の独立な全順序対の和に加え、偏心絞り・負/大きいCs・複数厚さ/焦点を組み合わせた4検査を追加した。現APIの入射面内ベクトルkは内部で0に固定されており、任意の非ゼロkを受け取るAPIを追加したわけではない。

## 2. 入射波e0との行列積を省く

通常回折とPEDの厚さ更新では、毎回 `alpha = V_inverse * e0` をdense matrix-vector productで計算していた。e0は第一成分だけが1なので、alphaはcolumn-major逆行列の第一列そのものである。

公開している逆行列の型・内容・構築は維持し、伝播係数を作る箇所で第一列を直接参照するようにした。逆行列をLU solveへ置換する改修ではない。

以下は取得部分だけの比較。第一列側は配列コピーまで含む保守的な測定で、採用コードはそのコピーも不要である。

| N | dense積 | 第一列コピー | 確保量：前 → 後 |
|---|---:|---:|---:|
| 8 | 120.07 ns | 8.04 ns | 616 → 152 B |
| 32 | 3.638 μs | 25.59 ns | 21,312 → 536 B |
| 128 | 23.740 μs | 103.49 ns | 274,753 → 2,072 B |
| 400 | 185.848 μs | 285.34 ns | 2,594,375 → 6,424 B |

[測定表と誤差・標準偏差](evidence/managed/DiffractionAlphaBenchmark.md)。有限な複素行列で各Nの両結果が一致することをSetupで確認した。これは既定MathNet providerの当該区間の比較であり、MKLを明示的に初期化した測定でも、回折全体の倍率でもない。厚さを変えて固有解を再利用する経路ほど相対的な効果が大きい。

## 3. ほかの候補と見送った理由

| 候補 | 調査結果・次の判定条件 |
|---|---|
| STEM弾性のq外側並列化 | 現行はk外側並列とqごとのlock。その後に作るq-entryを前へ移せばlockを省ける。既存の並行加算順が変わるので、数値ゲートと工程別計測が必要。今回は変更していない |
| STEMのUq生成で同じ差hklをまとめる | `Q×N²` 回のConcurrentDictionary lookupを減らせる候補。元の浮動小数ベクトル `vq+vi-vj` とcacheの最初の勝者が変わるため、単に「キャッシュなのでビット不変」とは言えない。検証前の採用はしない |
| STEM q-entryの二重保持をなくす | k単位の小配列からq-major flat配列へコピーする構造が残る。ただし2026-07の第4次資料に既出。現在のピークメモリへの寄与を測ってから進める |
| EBSDでgMapが恒等写像ならフィルタコピーを省く | phaseNGとmuBackBaseをそのままread-onlyで渡せる候補。EVD支配時の全体効果は小さくなり得る。native融合案と重複する費用を分けて測る |
| P/Invokeを一律に高速化する | 主経路は既にfixed pointerで渡す。短時間のleafだけGC transition省略を適用済み。長時間のEVDや内部確保を伴う関数へ一律適用する案は採らない |

## 再現と来歴

実装は [BetheMethod.cs](../../../Crystallography/Diffraction/BetheMethod.cs)、独立画像検査は [HrtemTccTests.cs](../../../Crystallography/tests/Crystallography.Tests/HrtemTccTests.cs)。ベンチマークはローカル履歴リポジトリの `ReciPro.Benchmarks/HrtemCoefficientBenchmark.cs` にあり、公開クローンには含まれない。

```powershell
# 別の重い計算と重ねず、Idle優先度で実行する。
$env:DOTNET_PROCESSOR_COUNT='2'
$env:MKL_NUM_THREADS='1'
$env:OMP_NUM_THREADS='1'
dotnet build ReciPro.Benchmarks/ReciPro.Benchmarks.csproj -c Release -p:Platform=x64 -m:1
dotnet ReciPro.Benchmarks/bin/x64/Release/net11.0-windows/ReciPro.Benchmarks.dll --filter '*HrtemCoefficientBenchmark*' '*DiffractionAlphaBenchmark*' --warmupCount 3 --iterationCount 8 --iterationTime 250 --launchCount 1
```

.NET 11 RC3、BenchmarkDotNet 0.16.0-preview.2、Server GC・DATAS無効・既定Tiered Compilation/PGO/OSR、ネイティブ有効で実施。全ログと修正前のmanagedソースは `C:/tmp/recipro_speed_261106/` に保存した。追加テストを含む本番コードの統合検証結果は、この追調査の総合記録で管理する。

[BenchmarkDotNet全ログ](evidence/managed/BenchmarkDotNet-run.txt)に各条件の係数・画像一致確認と全20ケースの実行結果、[buildログ](evidence/managed/build.txt)にベンチマーク本体のビルド結果を保存した。
