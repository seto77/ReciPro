# ReciPro 動力学計算監査: EBSD (261106Ch)

2026-11-06。対象は ReciPro 配下の submodule。別クローンは参照していない。これは調査時点の所見と今回の変更の記録であり、既存の EBSD 正本や進行中の席占有率研究の結果を置き換えない。

## 1. 結論と判定範囲

現在の EBSD は、相反定理による Bloch 波の出射応答、局所または非局所の後方散乱源、MC のエネルギー・深さ・方向分布、検出器投影を組み合わせる構成である。この構成自体は標準的な動力学 EBSD の系譜に入る。ただし「方位・バンド形状をよく再現する」ことと「任意材料・損失窓の絶対強度を予測できる」ことは別である。特に源、MC との接続、A(E)、検出器応答には近似が残る。

今回、11/5 の実装疑義一覧 B6 に記録されていた厚さ積分の弱吸収・近縮退での桁落ちを独立に再現し、managed/native 両経路を修正した。既知疑義の検証・解消であり、新規発見として数えない。これは数値計算の堅牢性改善であって、通常条件の EBSD 像が大幅に誤っていたという主張ではない。通常に近い例 λ=-1e-4 nm^-1、t=0.1 nm の旧式誤差は約 5.7e-13。一方、λ=-1e-12、t=0.1 では 0.031 %、λ=-1e-14、t=0.001 では積分が 0 になる。

新しい物理出力としては、まずエネルギー窓ごとの I(x,y,E) と、検出器応答を含めた平均エネルギー・コントラストの出力が有望。最初から未知の密度行列模型を既定にする必要はない。既存の E×t マスターから現行模型の予測を出し、実測と食い違う量を分離できる。

## 2. 調べた実装と既存検証

| 箇所 | 実装・確認した内容 | 判定 |
|---|---|---|
| `Crystallography/EBSD/EBSD.cs` | マスター構築、正方/六方 Rosca–Lambert、両半球、累積深さ plane | 設計は整合。現行は不等間隔の `DepthIntervals` を持つ |
| `Crystallography/Diffraction/BetheMethod.cs`, `ebsdNew_DoWork` | 方位ごとの beam 選択、固有分解、source σ、非局所 `muBack` | 近似と数値計算を分けて読む必要あり |
| 同 `EBSDSolverManaged` / `ComputeTDSMatrixBackground` | modal 厚さ積分、source の縮約 | 共役・転置は現行で整合。積分を今回安定化 |
| `Crystallography.Native/EigenFuncs.cpp` | `_EBSDSolver` / `_EBSDSolverWithTDS` | 9/22 の共役転置修正と全深さの等間隔判定を確認。今回積分を安定化 |
| `Crystallography/EBSD/EbsdMonteCarloDistribution.cs` | 射出半球上の E/深さ分布、μ による経路長換算、G_A | 深さ平均化・裾・正規化に模型依存性が残る |
| `Crystallography/EBSD/EbsdPatternComposer.cs` | ΔM/Δt、方向別 MC 重み、solid angle、A(E)、背景 | 経験補正であり、正値性を含む一般的な輸送演算子ではない |
| `Crystallography/EBSD/MonteCarlo.cs` | NIST/Mott、離散損失、Drude+内殻、最新阻止能 | 各模型の範囲を区別。11/6 の阻止能改訂は既存作業であり今回の成果に数えない |
| `ReciPro/FormEBSD.cs` | source 深さ、分布・A(E) の UI、表示/照合の接続 | Gaussian と histogram の E_c 既定が別。表示と照合の重みの近似差は残る |
| `tools/EbsdSolverCheck` | RK4/Simpson による独立参照 | 既存の強い検証資産。今回も活用する |
| `tools/EbsdOccupancy` | beam/grid/源の差、直接方向参照、占有率感度 | 新しい研究として提案し直さない。既存の研究と連携する |

既知修正を新発見に数えない:

- 2026-09-22: native の S と λ の共役転置不整合は修正済み。
- `ApplyWeightedModel2` と `ComposeGlobalWeightedPattern` は実際には ΔM/Δt を使う。旧総合文書の「区間幅で割らない」は現行コードに当てはまらない。
- 2026-10-14: 電圧変更時 U0 を旧電圧から再利用した不具合は `uDictionary.Clear()` を U0 評価前へ移して修正済み。
- 2026-11-05: 異方性 ADP の等方化 `BisoEffective` の改訂も現行に存在する。

## 3. 物理的に妥当な部分と限界

### 3.1 出射波と source の縮約

波動関数を ψ_g(z)=Σ_j C_gj α_j exp(2πiγ_j z) とすれば、局所源の強度は Σ_n σ_n|Σ_g P_ng ψ_g|²、非局所源は ψ†U_backψ。現行 managed/native は対応する Hermitian の縮約を実装している。F_jj′ は λ_jj′=2πi(γ_j−conj γ_j′) と組み合わせる必要があり、native の `sigB.transpose()*B.conjugate()`、非局所側の `C.transpose()*U.transpose()*C.conjugate()` はその添字規約に沿う。単なる C†UC へ戻すのは誤り。

伝播の吸収と検出器範囲の source は異なる役割を持つ。現在の非局所 source は後方半球の annular kernel であり、局所 source に上乗せする項ではない。コードは選択・置換する経路になっている。吸収された電子を全部背景へ戻す操作は、電子励起/検出器範囲/再散乱を区別しないまま一般化できない。ALCHEMI が `AbsorptionSource` を検査する構造は維持すべきである。

### 3.2 MC と Bloch をつなぐところが最大の模型依存

`FillPathLengthWeights` の μ=cosχ による λ_t=λ_d/μ は幾何として整合し、垂直深さと出射経路長の混同は修正されている。ただし出口の電子の条件付き分布から、結晶内部の非干渉源密度を完全には決められない。最後の非弾性・最後の弾性・最後のコヒーレンス破壊は別の源仮説である。

同関数の `alpha = max(alpha−1/lambda_abs,0.1*alpha)` は二重吸収を避ける実用補正だが、下限を含む経験的な処理。確率輸送と量子場の sink/gain を導出したものではない。source と sink の同じ断面積からの台帳を閉じるまで、絶対収率への一般化を認定しない。

局所 σ 計算は B_eff に 1e-3 nm² (=0.1 Å²) の floor を置く一方、ポテンシャル側は B=0 で TDS=0 とする。B が未設定の場合に局所 source が消えないようにする意図は記されているが、B=0 の厳密な物理極限を再現する挙動ではない。GUI の非局所 source と研究ハーネスの局所 source で既定が異なる点も比較条件に必ず記録する。

### 3.3 A(E) は物理定数ではない

`CoherenceFactors` は A(E)=exp[−max(0,E0−E)/E_c]。表示合成はさらに各画素で正規化し、検出器平均の Ā でコントラストを揃える。角度によるコントラストの差の一部を意図的に除いている。G_A は電子ごとの A を三角核へ積み、節点近似の誤差を減らすが、源の量子物理を導出する変更ではない。

`ComposeGlobalWeightedPattern` は節点 A と検出器平均の深さ重みを使う近似。表示は位置依存の G_A/G を使うため、一般には表示模型と照合模型は厳密に一致しない。これは既存の設計書でも既知。定量構造解析では最終候補だけでも表示と同じ画素ごとの forward model で再評価すべきである。

追加の数理的注意: `CohAccum.Combine` は正値保存を保証しない。V=Dc+Ā W/SA (sum−MbarA) で、たとえば局所重み W_e=(1,1)、A=(1,0)、plane 平均=(2,.01)、観測方向の I=(0,0)、検出器平均 Ā=.8 なら V=2.01−3.2=−1.19。これは公開式の許容入力に対する反例であり、実結晶の現在の既定像で負値を確認したという意味ではない。安易に clamp を追加せず、負値・正規化係数・モデル間差を診断出力するのが先である。

### 3.4 異方性 ADP と TDS 内核: 既知疑義 B1

11/5 の計算手法下書き `ja/implementation-questions.md:66` (B1)、`ja/scattering-factors.md:244` (Q4)、`ja/bloch-wave.md:344` にすでに記録された未解決の模型近似である。

`getU` は G=g−h の方向の ADP を m=M(G)/s_G² に縮約し、外側の exp[−m s_G²] を作る。この外側 DW は正しい。一方、`FactorImaginary` / `FactorImaginaryAnnular` はその m を積分点の q1=K−g、q2=K−h にも等方 B として用いる。一般の異方性では q1,q2 は G と平行でないため、この置換は厳密ではない。G=0 の B_eq も、異方共分散の全情報の代わりにはならない。

Gaussian 変位 u の共分散を U とすると、M(q)=2π² qᵀUq (q は cycles/nm)。熱平均した散乱振幅の共分散は

`C(q1,q2)=exp[−M(q1−q2)]−exp[−M(q1)−M(q2)]`。

これが外側DW×TDS内核に相当する。現在の scalar m は最初の指数を合わせるが、後半の2つの指数は一般には合わせない。変更するなら ADP tensor を各原子の対称操作と結晶回転で積分座標系へ移し、全積分点で2次形式を評価する必要がある。φ の偶対称による折り畳みとDWのφループ外への移動も、一般tensorでは再検討が必要。

例として M(q)=qᵀdiag(.001,.009,.001)q/4、q1=(1,1,0)、q2=(−1,1,0) を取る。G方向から得るm=.001では現行scalarの共分散は0だが、tensor式は exp(−.001)−exp(−.005)≈.003988 になる。これは局所被積分関数が一致しないという代数的反例で、積分強度や実結晶像が何%ずれるかを示すものではない。

正しい熱共分散には半正定値の構造があるが、現在の方向別 scalar 置換で実際の行列が非正定値になる事例は今回検証していない。PSD喪失を確定事項として主張しない。異方性の強い結晶について、等方極限・結晶回転共変性・source/sink整合・行列固有値と実像の収束を調べるべき優先課題である。

### 3.5 再注入が密度を流束とみなす: 既知疑義 A4 を修正

変更前の `ComputeAbsorbedFluxBackground` は N=Σ_g|ψ_g|² を使っていた。行割りされた一般化Bloch方程式で無吸収時に保存する量は J=Σ_g(P_g/P_0)|ψ_g|² であるため、非等PのHOLZ/傾斜面では吸収なしでも偽の再注入が生じ得る。ALCHEMI の `CoherentPathLengthNm` には P_g/P_0 が導入済みだったが、この関数には未導入だった。

これは 11/5 の実装疑義 A4 として既知。`FormEBSD.cs` は `includeAbsorbedFluxBackground=false` を固定し、公開APIの既定も false なので、通常GUIの既定像には影響しない。今回、opt-in 本番呼び出しに選択後 beam の P_g/P_0 を渡して T 行列へ組み込んだ。関数の追加引数は default null で、既存の密度規約を使う独立ハーネスは明示的 null により維持した。GUI既定は変更していない。ALCHEMI の density/flux と発生率全体の規格化（既知疑義A5/A6）は別問題であり、今回の修正で一般的に解消したとはしない。

### 3.6 文献との照合

- Winkelmann et al. (2019), PRB 99, 064115 は Si の実効菊池スペクトルと全 BSE スペクトルを区別し、CSDA 出口スペクトルをそのまま菊池重みにすることへの制約を示す。局所PDFの本文・結論を確認。これは現在の E_c の普遍性を支持する文献ではない。[原著](https://journals.aps.org/prb/abstract/10.1103/PhysRevB.99.064115)
- Winkelmann et al. (2021), J. Microsc. 284, 157–184 は coherent point emitter と incoherent diffuse intensity の両者を扱う。局所PDF pp.1–10 の構成・導出を確認。位相関係が無い成分でも個々の波は回折し、背景の勾配から excess/deficiency が出る。したがって失った可干渉性を全て方向一様な pedestal とする模型には限界がある。[著者版](https://strathprints.strath.ac.uk/78647/1/Winkelmann_etal_JM_2021_Kikuchi_pattern_simulations_of_backscattered_and_transmitted.pdf)
- Della Ventura et al., DOI 10.1016/j.ultramic.2025.114301 / arXiv:2507.20105 は、12 keV Si で高損失の 2–8 keV 窓にも回折情報を観測する。本文では約1 keVの検出器エネルギー分解能と校正、エネルギー別背景補正を扱う。低損失だけに寄与を限定する説明は避けるべきである。この論文の MC 比較は背景の分布で、動力学強度の独立検証ではない。[原著全文](https://pmc.ncbi.nlm.nih.gov/articles/PMC13379735/)

2019 と新しい測定を「片方が誤り」と片付けず、検出器応答・測定量・背景補正・角度・試料条件を揃えて比較する必要がある。

## 4. 公開コードの比較

EMsoft `develop` を 2026-11-06 に取得し、tree SHA `bb0a30f106f5e60216a81833e511038e8482c01f` を記録した。参照ファイルは `Source/EMsoftLib/MBmodule.f90` と `Source/SEM/EMEBSDmaster.f90`。外部コードはコピー実装していない。

- `preCalcSgh` / `CalcSgh` は Z²×占有率×Debye–Waller×位相を構成する。
- `CalcLgh` は一般複素行列の固有分解、逆行列の入射列、MC 深さ重みの数値積分から Lgh を作る。
- `EMEBSDmaster` は site ごとの Sgh と Lgh の Hadamard 積を足し、結晶対称性を使う。

[固定commitの実装](https://github.com/EMsoft-org/EMsoft/blob/bb0a30f106f5e60216a81833e511038e8482c01f/Source/EMsoftLib/MBmodule.f90)

ReciPro の local σ (annular TDS による物理スケール) と EMsoft の Z² source は同じではない。画像が違うことだけを solver の不具合と判定できない。同じ結晶・格子・源・深さ核・beam 集合で小さい比較を作る必要がある。EMsoft も別の近似模型であり無条件の正解にはしない。

高速な方位探索では [EMSphInx](https://github.com/EMsoft-org/EMSphInx/blob/master/documentation/emsphinxebsd.rst) の球面相関が比較対象になる。ただしこれは master の構築自体を高速化する方法ではない。EMsoft は BSD3、EMSphInx は GPL という相違をリポジトリが明記しているため、ソース転用と算法参照を区別する。

最近の [EBSDmagus (2026)](https://arxiv.org/abs/2609.08334) は互換な中間結果の再利用、入力・出力の整合検査、中断後の未完了計算だけの再実行を扱う。ReciPro の研究ハーネスにも既存の分割・鍵管理があるので、新規計算エンジンとして重複導入するより、現行成果物の来歴・互換性キーを製品側へ統一する方が有効である。

## 5. 今回の実装: 安定な厚さ積分

F(λ,t)=t exprel(λt)。旧実装は |λ|<1e-15 のみを判定し、それ以外で exp(λt)−1 を直接引いていた。小ささを決めるのは無次元の λt なので、薄い t や弱い吸収で桁落ちする。また |λ|だけで t と置くと、非常に長い t で不正になる。

新実装は |λt|≤0.01 で

`F=t*(1+z*(1/2+z*(1/6+z*(1/24+z*(1/120+z*(1/720+z/5040))))))`, z=λt

を使う。最初の省略項の絶対値は exprel に対して約2.5e-19以下。範囲外は従来式。符号、source、総和順序は変えていない。

変更ファイル:

- `Crystallography/Diffraction/BlochWaveIntegral.cs`: managed 共通実装。
- `Crystallography/Diffraction/BetheMethod.cs`: EBSD local/nonlocal の2箇所。
- `Crystallography/Diffraction/Alchemi/AlchemiReduction.cs`: λを保持し、小引数だけ級数。通常の b 個/厚さの exp、1/λ の事前計算を維持。
- `Crystallography/Diffraction/Kikuchi/KikuchiProfileCalculator.cs`: Fjj。
- `Crystallography.Native/BlochWaveIntegral.h`, `EigenFuncs.cpp`, `.vcxproj`: native EBSD local/nonlocal、等間隔の累積expを維持。
- `Crystallography/tests/Crystallography.Tests/BlochWaveIntegralTests.cs`: 単一減衰8条件×4厚さ×各managed経路、複素2mode干渉3条件を直接波動関数のSimpson積分と比較。
- `Crystallography/tests/Crystallography.Tests/EbsdAbsorbedFluxTests.cs`: 非等Pの2波系、P比2の無吸収およびP比2/.5の減衰を明示的な波動関数の直接求積で検証。旧密度規約が無吸収でも偽の背景を作る陰性対照を含む。
- `tools/EbsdSolverCheck/Program.cs`: reflection呼び出しへ明示的nullを追加し、元の独立密度参照との比較を保持。

`ComputeAbsorbedFluxBackground` は t−F の小引数級数をすでに持つため、その積分式は変更していない。後続監査で既知疑義A4を確認し、§3.5の生存流束の重みを追加した。ALCHEMI の `t−Lcoh` 自体の差の相対精度は別課題で、今回の F 安定化だけで任意に小さい dechannelling が高相対精度になるわけではない。

検証状況 (親担当の実行ログを読み戻し確認):

- native generic / avx2 / avx512 のビルド成功。
- `tools/BlochIntegralCheck` は native 必須 (`NativeWrapper.Enabled=True`)。独立な Simpson 参照との432検査で、旧DLLは234失敗。新DLLは3 flavorとも432/432通過、最大絶対誤差/t = 1.512679e-14。
- ログ: `C:/tmp/recipro_dynamical_audit_261106/integral-native-old.log`, `integral-native-generic.log`, `integral-native-avx2.log`, `integral-native-avx512.log`。
- 最終検証は、別作業による .NET 11 移行を保存した現行ツリーで実施。`dotnet build ReciPro.sln -c Release -p:Platform=x64 -m:1` は警告0・エラー0。
- managed xUnit 全suite: `net11.0-windows`、6703/6703通過、失敗0・skip0。今回追加したA4の非等P・無吸収/減衰の検証を含む。
- 新native DLLをtest binへ明示的にコピーして実行した。`BetheDivCheck` は最大流束誤差1.78e-15、`EbsdSolverCheck` は最大相対誤差9.55e-12で全通過。後者の再注入チェックはnull引数の旧密度規約との互換性を検証し、新しいP重みの物理検証はA4のxUnitが担当する。
- 最終 `BlochIntegralCheck`: native有効、432/432通過、最大絶対誤差/t=1.512679e-14。
- 最終ログ: `C:/tmp/recipro_dynamical_audit_261106/tests-final.log`, `BetheDivCheck-final.log`, `EbsdSolverCheck-final.log`, `BlochIntegralCheck-final.log`。上記4ログはこの追記時に読み戻し確認した。

速度向上は計測していないので主張しない。

## 6. 優先する拡張・高速化

| 優先度 | 案 | 成立を判断する検証 |
|---|---|---|
| 高 | I(x,y,E) と R(Emeas|E) の畳み込み、平均エネルギー、窓ごとのバンドコントラスト | 独立な検出器校正、窓積分が通常像へ戻ること、energy-grid収束 |
| 高 | 源別/原子席別の応答、吸収・背景・A補正の分解出力 | 成分和と全体の一致、既存占有率ハーネスとの数値一致、negative値検出 |
| 高 | 最終候補だけ full pixel-wise forward model で再採点 | 節点A/大域平均の近似による方位・占有率のbiasを直接参照で測る |
| 中 | source/kernel/屈折/電圧/ADP/beam集合を含むキャッシュキーと中断再開 | 物理入力を1つずつ変え、必要な段だけ無効化するテスト |
| 中 | 決定的なMC乱数列、共通乱数で差分・感度の分散を削減 | thread数に依存しないelectron-IDベースのstream、複数独立seedで分散評価 |
| 中 | マップの粗い球面探索と近傍seed伝播 | 擬対称・粒界・多相に対する誤index率を保ち、全工程の時間を測る |
| 研究 | CPE+IDI/密度行列に基づく E/D、損失ごとのsourceとsink | source正値性、flux台帳、既知極限、低損失/高損失/角度/反射の留保データ |

`MonteCarlo.Rnd=Random.Shared` と並列実行では、計算条件を同じにするだけでは比較用標本が再現されない。共通乱数は有力だが、1個の Random を各 thread に共有して seed を付けるだけでは順序が変わる。electron ID ごとの stream が必要。今回、本番 MC の統計や既定値を変える実装はしていない。

GPU化やKrylovで全面マスターが何倍速くなるとは断定しない。現行には結晶対称性、代表方向共有、native BLAS、pool、非一様深さがあり、既存 BeamSelStudy/FFT-Krylov の検討を参照すべき。最終像・占有率biasを固定して、beam/格子/深さ/energy discretizationを合わせた費用比較が必要である。

## 7. 未完・実測が必要なこと

今回、実測データを新たに取得したわけではなく、温度・材料・損失窓を跨ぐ絶対強度の妥当性は認定しない。進行中のEBSD席占有率/DFT研究を停止・変更・再実行していない。文献にある高損失の回折情報と E/D の再現を、現在の経験補正で達成したとも言わない。

新しい大規模模型を既定化する前に、既存研究で未達とされた角度移送・深さ/energy刻み・MC分散の検証を明示して扱う。既存計算の完了や相関係数の高さを、物理模型の完成と同一視しない。
