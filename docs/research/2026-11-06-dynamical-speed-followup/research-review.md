# 動力学計算の高速化：文献・実装を踏まえた再評価

2026-11-06 / 261106Ch。前回の[横断監査](../2026-11-06-dynamical-audit/ReciPro_DynamicalAudit_261106.md)に対する追加調査。本文は候補の根拠・適用条件・測定方法を記録する。今回の実装採否と最終測定値は同じフォルダの統合記録を正本とし、本稿の計算量から実測倍率を推定しない。

## 1. 結論

**高速化の余地は残っている。** とくに、同じ複素指数を固有モード対ごとに計算するEBSDの厚さ積分と、同じレンズ係数を試料厚さごとに作り直すHRTEMには、物理近似を増やさず計算を減らせる箇所がある。一方、すでに改善済みのSTEM伝播をPRISMへ置き換えるだけで文献の倍率が得られるわけではない。固有値分解、伝播、源の縮約、画像合成、前処理を分けて判断する必要がある。

過去の不採用記録は候補を永久に否定する根拠ではない。ただし、古い条件の失敗を無視して同じ案を繰り返すのも不適切である。今回再評価すべき点は、実際にロードしたDLL、内外のスレッド数、行列の大きさ・吸収・方位、全厚さ一括処理の有無、メモリ上限である。

| 区分 | 候補 | 調査時の判断 |
|---|---|---|
| 今すぐ実装・測定する価値がある | EBSDの指数を固有モードごとに因数分解 | 数式上は厳密等価。非等間隔深さで複素指数の回数を大幅に減らせる |
| 今すぐ実装・測定する価値がある | HRTEMレンズ位相・TCCの厚さ間共有 | 方位・絞り・収差が固定なら厚さに依存しない。全ペアの巨大キャッシュを作らなくても実現可能 |
| 今すぐ実装・測定する価値がある | 非局所EBSDで捨てている局所源の縮約を省略 | 出力選択をコード追跡で確認。既存APIの0原子契約を使えばABIを変えずに省ける |
| 今すぐ検証する価値がある | Eigen更新の旧新DLL比較 | 更新だけで速くなるとは断定せず、同一物理行列・同一コンパイラ条件で測る |
| 条件付き | 固有分解→LU→EBSD縮約のnative一括処理、workspace再利用 | 小～中Nの大量方向で有望。EVDそのものの演算量は変わらない |
| 条件付き | native LAPACK/OpenBLAS/MKL、全厚さGEMMとの接続 | 過去の測定条件を整理し、backendを実ロードしたことを確認して再評価する |
| 条件付き | Eigenの新GPUモジュールとcuSOLVER非Hermitian EVDの別接続 | 周辺処理の接続費は下がった。Eigen GPUのEVD自体はHermitian専用であり、全工程の再測定が必要 |
| 条件付き | PEDのα列保存、角度倍増時の既計算方向再利用 | メモリ削減と収束診断の追加費用削減に有望。通常の固定M計算が自動的に速くなるわけではない |
| 研究 | physical-optics Bloch、SCA、Krylov、FFT作用 | 固有分解のN³を避け得るが、slice・基底・誤差制御と観測量の縮約が必要 |
| 根拠不足 | 汎用GPU置換、float保存、Hermitian化、任意視野への単純FFT | 現行模型と精度を保った全工程の勝利は示されていない |

実施後の結果は、[C++の測定・採否](native-speed.md)、[C#の測定・採否](managed-speed.md)、[Eigen更新の比較](eigen-update.md)に記録した。EBSDの非等間隔指数分解と不要局所源の省略、HRTEMのレンズ位相再利用を採用した。HRTEMの全厚さq係数保持を伴う共有案は見送った。以下では採用部分と研究候補の双方の根拠を残す。

## 2. 読んだ範囲と既存記録の扱い

先に `C:/Users/seto/source/repos/references/README.md` の書誌と、既存のBethe技術基準・第1～4次高速化記録・FKP事前登録と追記結果を確認した。今回の担当では新たに全PDFを精読したとは主張しない。対象コードはReciPro配下の `Crystallography/Diffraction/BetheMethod.cs`、`AlchemiReduction.cs`、`Crystallography.Native/EigenFuncs.cpp`。古い姉妹クローンは参照していない。

既存のローカル記録:

- `.project-guidance/ReciPro/ReciPro_BetheMethod技術基準.md`
- `.project-guidance/archive/2026/bethe/ReciPro_BetheMethod高速化提案.md`
- `.project-guidance/archive/2026/bethe/ReciPro_BetheMethod高速化_第4次方針転換.md`
- `.project-guidance/ReciPro/ReciPro_FFTKrylov伝搬参照_事前登録_261007.md`
- `.project-guidance/ReciPro/ReciPro_FKP固定g40方式の再評価_事前登録_261012.md`

上記はローカル専用資料であり、公開クローンには含まれない。前回調査で固定したOSSコミットの比較を引き継ぎ、今回は公式数値ライブラリの仕様と関連原論文を重点的に再確認した。

## 3. EBSDの厚さ積分：指数関数の回数をN²からNへ

局所源の強度は、現行の規約で

$$
I(t)=\operatorname{Re}\sum_{j,j'}S_{jj'}F_{jj'}(t),\qquad
F_{jj'}(t)=\frac{e^{\lambda_{jj'}t}-1}{\lambda_{jj'}},\qquad
\lambda_{jj'}=2\pi i(\gamma_j-\overline{\gamma_{j'}})
$$

と表せる。ここで

$$
u_j(t)=e^{2\pi i\gamma_jt},\qquad
e^{\lambda_{jj'}t}=u_j(t)\overline{u_{j'}(t)}
$$

なので、厚さごとにN個の指数を計算すれば、N(N+1)/2個の対に同じ情報を供給できる。この因数分解は新しい散乱近似ではない。`AlchemiReduction` はすでにこの構造を利用しているが、調査開始時のnative `_EBSDSolver` と `_EBSDSolverWithTDS` の非等間隔深さ分岐は対ごとに `exp(lambda * thick)` を呼んでいた。

| 処理 | 調査開始時の非等間隔分岐 | 因数分解後 |
|---|---:|---:|
| 複素指数の呼び出し | O(TN²) | O(TN) |
| 対ごとの積分と強度への加算 | O(TN²) | O(TN²) |
| 固有値分解 | O(N³) | O(N³) |

したがって「EBSD全体がN倍速くなる」とは言えない。深さの数Tが大きく、非等間隔格子の積分が時間を占める条件ほど効果が出やすい。sourceが密な非局所行列の場合、別途必要な `C†UC` の行列積は残る。

数値上の注意点:

- 近縮退・弱吸収での `(exp(z)-1)/z` の桁落ちは、前回追加した `exprel` 級数分岐を維持して避ける。指数因数分解だけでは解決しない。
- 物理的な減衰条件では指数は減衰するが、任意入力の増幅モードや負の厚さでは、個別の指数がoverflow/underflowして積が非有限になることがある。直接の `exp(lambda*t)` が有限になる場合との契約を検討し、必要なら対ごとのfallbackを残す。
- 等間隔分岐は既に対ごとの指数を再帰更新している。非等間隔分岐と同じ倍率は期待しない。モードベクトルの再帰へ変えるなら、累積丸めと再アンカーも検証する。
- SとFの共役の向きを変えない。2026-09-22の転置・共役修正を壊すと、速度とは比較にならない大きさの強度誤差を再導入する。

今回の採用実装では共通の実固有値を差し引き、全モードの指数が所定範囲に入る厚さだけ因数分解する。範囲外はその厚さの全ペアを直接指数へ戻す。[範囲条件と検証結果](native-speed.md)を参照。

## 4. HRTEM：レンズ計算を厚さごとに繰り返さない

TCC画像の係数は

$$
c_{gh}(t,d)=\psi_g(t)\overline{\psi_h(t)}\,L_g(d)\overline{L_h(d)}\,E_s(g,h,d)\,E_t(g,h)
$$

であり、試料厚さtに依存するのは先頭の波動関数だけである。方位・絞り・加速電圧・収差・焦点系列が固定なら、レンズ位相と包絡は共有できる。

調査開始時の `GetHRTEMImage` は厚さループ内でペアを列挙し、各ペア・各焦点について位相の複素指数を2回計算していた。次は物理模型を変えずに削減するため比較した、全厚さ共有の候補である。

1. 全厚さの波動関数を取得し、各反射のPsiを値として保存する。
2. `L_g(d)` を反射・焦点ごとに一度求める。
3. g,h,dを外側、tを内側にして、ペアの包絡係数を一度作り、各厚さのq係数へ足す。
4. 前回実装した同一qの集約と、既存の画像合成へ渡す。

これにより複素レンズ指数の回数はO(TDN²)からO(DN)になる。Es/Etも厚さ間で再利用できる。O(DN²)の全TCC表を常時保存することは必須ではない。ペアごとに全厚さへ適用すれば、レンズ位相O(DN)と厚さ別の出力係数を中心に処理できる。

重要な実装契約は、`GetDifractedBeamAmpriltudes` が同じBeamオブジェクトのPsiを書き換える点である。厚さ別にBeam配列の参照だけを保存すると、すべて最後の厚さになる。Psiの値コピー、絞り後の反射集合・順序の一致、各qへのペア加算順、空の絞りを確認する。

全厚さ共有案はq係数のピーク保持量が増えるため、今回の本番採用はレンズ位相・面内ベクトルの再利用と一時配列の削減までにとどめた。厚さを外側に置き、1厚さずつ画像化する処理順を保っている。上記1・3の全厚さ保存と包絡の厚さ間共有は採用していない。[比較値と採否](managed-speed.md)を参照。

この改善は前回のTCC画像合成の高速化とは別の段を対象とする。係数構築が速くなっても、巨大画像の画素合成が支配する条件では全工程の改善率は小さくなる。

## 5. Eigen・BLAS・LAPACK：版とbackendを混同しない

Eigenの更新、SIMDフレーバーの選択、BLAS/LAPACK backendの切替は別々の変更である。Eigenの[公式仕様](https://libeigen.gitlab.io/eigen/docs-nightly/TopicUsingBlasLapack.html)では、`EIGEN_USE_BLAS` は行列積、`EIGEN_USE_LAPACKE` は分解を外部ライブラリへ委譲する。ComplexEigenSolver経路の表にあるのは `?gees` であり、マクロを付けるだけで直接 `zgeev` 呼び出しと全く同じ処理になるとは限らない。

[ComplexEigenSolverの仕様](https://libeigen.gitlab.io/eigen/docs-nightly/classEigen_1_1ComplexEigenSolver.html)はSchur分解を経由し、サイズ指定の事前確保と `compute()` の再利用を提供する。thread-localなsolver/workspaceで確保・解放を減らす候補はあるが、固有分解のN³は残る。保持するworkspaceの大きさとワーカ数によってピークメモリが増えることも測定する。

### 過去のLAPACK棄却は読み直す必要がある

2026-06の記録にある「MKLがnative Eigenの0.16～0.5倍」は、その後2026-07-11の調査で、BetheBenchにMKL DLLが無くmanaged MathNetへ落ちていたことが判明している。これを「LAPACKは遅い」という一般結論として引用してはいけない。

修正後の既存測定では、実効N=253/345ではnative Eigenが優勢、Si N=441/553ではMKLのEVD段が約1.9/2.9倍速かった。ただしMKL経路のメモリ増と全厚さ一括処理の差があり、現行Auto閾値にはその判断が入っている。これらは過去の測定値で、今回のEigen更新後の性能ではない。

さらに既存FKP資料には、N=3000の乱数複素行列でnative EigenとOpenBLASの大きな差も記録されている。この規模を通常のN=100～500へ外挿せず、「ライブラリでは何も変わらない」という強い表現も避けるべきである。

今後のbackend比較では次を固定する。

- 実行時DLLの絶対パスとSHA、整数ABI、LAPACKEの有無、CPU命令セット。
- 外側の方向並列と内側BLASスレッド。単独EVDと実際の方向バッチは別々に測る。
- Schurだけ、EVDだけ、EVD+LU+全厚さ伝播、最終画像の各区間。
- N、行列の吸収・非正規性、要求ビーム数と実採択数、配列コピーと確保量。
- LAPACKのworkspace query。最小長で動作したことは最適速度で動作したことを意味しない。[LAPACK公式の性能説明](https://www.netlib.org/lapack/lug/node140.html)

今回追加したローカル `ReciPro.Benchmarks/EigenVersionBenchmark.cs` は、旧新DLLを絶対パスで同時ロードし、同一のSi物理行列を渡す。固有ベクトルの並びや任意位相を直接比較せず、AV−VΛの残差と、共通のmanaged LUから再構成した波動関数を照合する。これはEigen版の比較であり、新たなBLAS backendの採用試験ではない。

## 6. sourceの低ランク化・三角行列化：何がすでに省かれているか

局所源では `B=P C diag(alpha)`、`S=Bᵀ diag(sigma) conj(B)` とする現在の縮約が、原子数を利用した因子形をすでに使っている。局所源をいったんN×N行列Uに展開し、`C†UC`を2回の密なN³積で処理する経路へ統一すると、むしろ遅くなる場合がある。

SがHermitianなので上三角だけ作る、非負sigmaに対して平方根重みを使うrank-updateにする、といった厳密な候補はある。[Eigenの積評価の説明](https://libeigen.gitlab.io/eigen/docs-nightly/TopicWritingEfficientProductExpression.html)は、積・随伴・一時行列の式の形が評価費用を変えることを明示している。ただし、少原子の細い行列の積では汎用GEMMが既に速く、三角化の倍率は実測が必要である。sigmaの符号や共役の向きを変えず、実際に読む三角側を確認する。

非局所のぼかし源・TDS行列を低ランク近似する場合は別問題である。小さい固有値の切捨ては数値近似を加える。sourceの作用誤差だけでなく、全方向・全深さの積分強度、源の正値性、観測量の偏りを制限する必要がある。既定模型を無条件に低ランク化する根拠はまだない。

### 追加追跡：最終的に使わない局所源を省く

native担当が指摘した `_EBSDSolverWithTDS` の両出力を、C#側の全消費箇所まで独立に追跡した。`ebsdNew_DoWork` の `muBackFiltered != null` 分岐では、局所intensityと非局所backgroundを両方計算するが、Diskへ保存する値は `background ?? signal` である。吸収フラックスの再注入も `background ?? intensity` に加えるので、この分岐の局所数値は出力に使われない。物理的に同じ過程を二重加算しない、という2026-09-19の変更後に残った計算である。

ただし `ebsdIntensity[r] is not null` は計算済みセルの判定に使うため、局所配列をnullにすると非局所の正しい結果まで落ちる。局所の0配列を厚さ数分返す契約を保つ必要がある。共有の `EBSDSolverWithTDS` を無条件にTDS専用へ変更すると、両出力を利用する `BlochIntegralCheck` 等を壊す。

最小案は、productionの該当1箇所だけsigmaを空配列にし、既存wrapperから `nAtoms=0` を渡すことである。C++は0原子時のB・S構築と局所和を省略し、局所出力には0を入れる。native/managedの公開シグネチャを変える必要はない。新しいboolをC exportへ追加する案は旧DLLとのABIを壊す。C#のデフォルト引数追加もソース互換にとどまり、コンパイル済み呼出しの互換を保証しない。

この意味はSourceSmearingSigmaNmが有限の場合も同じで、ぼかし源行列の値が局所出力を置換する。sigmaArray自体はその行列構築と、任意の吸収再注入のsigmaSumに必要なので、前処理から一括削除しない。`UseNonLocalAbsorption` は伝播中の吸収模型を選ぶ別の旗であり、省略条件に使わない。

追加の `EbsdLocalSourceSkipTests` は既存2overloadについて、空原子でもTDSが不変、局所出力が0、空/0/非等間隔の厚さ、弱吸収、直接波のSimpson参照を検査する。旧DLLでも0次元行列の挙動を確認してから、API互換が検証済みと記す。

## 7. 固有値分解を避ける方法：研究を続ける価値はある

### physical-optics BlochとSCA

[Mendis 2024のSCA](https://journals.iucr.org/a/issues/2024/02/00/tw5006/)は、近傍反射との散乱を用いたスライス逐次計算により固有分解を避ける。論文は薄い試料・軽元素・大きい単位胞への適性を述べる。ReciProの重元素・厚膜・全入射角へそのまま適用できる主張ではない。

[Mendis 2025](https://journals.iucr.org/a/issues/2025/02/00/tw5012/index.html)は、構造行列を位相格子と自由伝播へ分け、固定試料の多数照射に対してO(N²)/sliceの方式を示す。現行の有限行列そのものを解くEVDと比較するには、P_gの行依存、吸収、HOLZ、slice幅を揃える必要がある。論文中のunitaryな固有ベクトルの扱いを、吸収を含むReciProの非Hermitian行列へ持ち込まない。

### FKPは既存研究を継承する

本プロジェクトにはFFT畳み込みとKrylov伝播の研究実装がすでにある。2026-10-13の固定g40再評価の追記は、指定LUT・全前方線形化模型・指定観測量についてR80の動径収束を確認した参照を使ったことを記録している。これは無限半径の真値でも、GUIの全モードでの速度優位でもない。また、既存CL2のnative時間予算の不通過を変更するものではない。

少数の出射波だけが必要な場合、`exp(A t) psi0`の作用を計算するKrylovは全固有ベクトルを不要にできる。一方、多数の厚さ・source積分・複数入射条件の再利用ではEVDの償却が効く。小規模での適応Krylov次数、restart、源積分まで含めて比較し、今回新しく発明した方式とは扱わない。

Schur形式のまま伝播・Sylvester積分へ進む設計も可能だが、Schur分解自体はN³である。弱吸収や縮退で積分方程式が悪条件になる場合があり、「固有ベクトル逆行列を消せるから必ず速く安定」とは認定できない。[LAPACK ZGEESの契約](https://netlib.org/lapack/explore-html/d5/d38/group__gees_gac426d99ddc8ac986de2a17e7ad3f2512.html)

## 8. PRISM・FFT・GPUを採用する条件

[abTEMのPRISM解説](https://abtem.github.io/doc/user_guide/tutorials/prism.html)は、部分平面波の伝播を再利用して多数の走査位置を合成する。ReciProのSTEMはすでに入射方向ごとの伝播と位置依存の合成を分離し、nativeの全厚さGEMMとq単位の縮約を持つ。単純な位置ごとのmultisliceとの比較倍率をReciProへ適用しない。PRISMの補間による追加削減を使うなら、その誤差と基底の違いを測る。

HRTEMのq係数をFFTへ渡す案は、画像格子と逆格子周波数が厳密に整合する視野では候補になる。現行APIは任意の画素間隔・回転・視野を許すため、全条件が通常の2D FFTの整数周波数になるわけではない。格子を丸める変更は物理的な像位置・位相を変える。任意格子ならNUFFT等の許容誤差を持つ経路と直接和を比較する必要がある。

GPUはFP64の演算能力、転送、方向バッチ、メモリ、配布・実行環境を含む課題である。過去に重元素で1～4%誤差を出したHermitian化と吸収摂動を、GPU利用のために既定へ戻す根拠はない。新しいbatched APIやFP64代替の名称だけで、現行の非Hermitianな問題への適合を認定しない。今回、この条件を満たすGPU実装の速度は未測定である。

### Eigen更新で変化したGPU再検討条件（261106Ch追記）

今回固定した上流 `5a19218d5a28bfc38ca6b1475ea040c8f73257ba` の新 `contrib/Eigen/GPU` は、cuBLAS/cuSOLVER/cuSPARSE/cuFFT/cuDSSへの明示的な接続を提供する。CUDA 11.8以上が必要で、cuDSSはCUDA 12.0以上と別途導入を要する。CPUのEigen型が自動でGPUへ切り替わる仕様ではない。`DeviceMatrix` と再利用可能なsolver/contextがあるため、複素doubleのGEMM・LU・FFTをGPU上で連結する実装費は以前より下がると判断する。ただし `DeviceBatchMatrix` はこの版でも将来項目である。[固定版GPU仕様](https://gitlab.com/libeigen/eigen/-/raw/5a19218d5a28bfc38ca6b1475ea040c8f73257ba/contrib/Eigen/src/GPU/README.md)

EVDの実装を確認すると `gpu::SelfAdjointEigenSolver` が `cusolverDnXsyevd` を呼び、下三角から実数固有値を求める。**このモジュールには `ComplexEigenSolver` のGPU backendはなく、吸収を含む非Hermitian Bethe行列の置換には使えない。** 複素型への対応は複素Hermitian行列を意味する。[固定版GpuEigenSolver.h](https://gitlab.com/libeigen/eigen/-/raw/5a19218d5a28bfc38ca6b1475ea040c8f73257ba/contrib/Eigen/src/GPU/GpuEigenSolver.h)

一方、cuSOLVER自体の `cusolverDnXgeev` は複素doubleの非Hermitian行列と右固有ベクトルに対応するCPU/GPU混成算法である。Eigenの当該モジュールには未接続なので、直接wrapperを追加する別候補として扱う。[NVIDIA Xgeev仕様](https://docs.nvidia.com/cuda/cusolver/index.html#cusolverdnxgeev) CUDA 13.3の固有ベクトル後処理のGPU化、13.4のworkspace削減に加え、13.5のrelease notesには低い精度でのSchur分解と反復精密化を用いる `ALG_1` が記載された。これは大きく固有値が十分離れた問題向けで、小行列・密集スペクトルでは遅く、追加workspaceも多いと明記される。API本文の「default algorithmのみ」という記述との不一致が残るため、実装時には採用版のheader・動作を確認する。[NVIDIA release notes](https://docs.nvidia.com/cuda/cuda-toolkit-release-notes/#cusolver-release-13-5)

再試験はまず通常の複素double XgeevとGPU内に保持した後段のLU/GEMMを組み合わせ、転送・初期化・全方向処理を含めて比較する。混合精度は別試験にし、縮退・弱吸収を含む残差と伝播・観測量の誤差を判定する。現在の読取専用inventoryではGPUはGeForce RTX 4060だったが、この経路のbuild・実測は今回行っていない。GPU再検討の根拠は増えた。採用判断には上記の数値検証と全工程の測定を要する。

## 9. 速度を評価する物差し

ある段が旧全時間の割合fを占め、その段がs倍速くなる場合、他の費用が変わらないという仮定では

$$
S_{\rm total}=\frac{1}{(1-f)+f/s}
$$

となる。これは実測値ではなくAmdahlの上限計算である。たとえば係数構築が10%ならそこが5倍速くても全体は約1.087倍、厚さ積分が60%ならそこが3倍速くなると全体は約1.667倍となる。同じ最適化でもモードと入力によって実用上の効果が大きく変わる。

採用時は次の三つを分けて残す。

1. **数学的等価性**：何を再利用し、何の近似を追加しないか。整数q・Pの行割り・共役・流束・厚さ格子の規約を固定する。
2. **数値精度**：近縮退、弱吸収、空入力、厚い試料、非中心対称・重元素、等間隔/非等間隔で独立参照と比較する。固有ベクトルの順序一致を合否にしない。
3. **性能**：BenchmarkDotNetで局所段を測り、現実の処理の段別割合と全工程も確認する。確保量だけ、手測り一回だけ、改善した条件だけで採用しない。

旧DLL・新DLL、Eigenの上流コミット、コンパイラ、runtime、スレッド、fixtureを記録し、同時作業によるバイナリの差替えを検出する。前回と今回の値を混ぜた速度比を作らず、未測定の候補は未測定と記す。この手順なら、精度を維持した改善を採用しながら、より大きい算法変更の研究も継続できる。
