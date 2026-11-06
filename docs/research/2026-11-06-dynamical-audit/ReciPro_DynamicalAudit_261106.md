---
doc_type: audit
status: completed
as_of: 2026-11-06
---

# ReciPro 動力学計算の横断監査と改善 (261106Ch)

## 結論

ReciPro の Bloch/Bethe コアは、完全周期結晶・高エネルギーの前方伝播・有限の反射集合という条件で、物理的な根拠を持つ。今回も独立参照、保存則、修正前後の回帰で具体的な経路を検証した。一方、全材料・全厚さ・全検出条件で定量的に正しいという結論は出せない。とくに EBSD の非干渉源と MC の接続、厚い試料の TDS 再散乱、異方性 ADP の吸収核、部分コヒーレンス、重複 CBED の合成を区別する必要がある。

実装したのは、PED/平行回折の固有解再利用の修正、弱吸収・近縮退時の積分安定化、研究用 EBSD 背景の電流重み、HRTEM TCC の数学的に同値な高速化、空の対物絞りへの対応である。既存メモの疑義 A7・B6・A4を実際に検証した部分と、新しく得た具体的な再現・高速化を分けて記録する。新たな物理模型の精度を未検証のまま既定へ組み込んではいない。

分野別の根拠、式、コード位置、文献、公開実装の固定 commit は以下にある。

- [CBED・PED・Bloch コア](ReciPro_DynamicalAudit_CBED_PED_261106.md)
- [STEM・HRTEM・EDX](ReciPro_DynamicalAudit_Imaging_261106.md)
- [EBSD・源・輸送との接続](ReciPro_DynamicalAudit_EBSD_261106.md)

## 調査範囲と証拠の扱い

正本は `C:/Users/seto/source/repos/ReciPro` 配下の submodule。開始時の公開リポは clean、Crystallography は `b2bbacaaf972ca2922387bdec16b20ed20e90f6d`、Native は `0700faa7a9ed9981240d9167c82471efc968c5ce`。別クローンは読んでいない。

`C:/Users/seto/source/repos/references/README.md` から関連原著・教科書を選び、既存の Bethe 技術基準、STEM-EDX、py_multislice 比較、EBSD、FKP、直近の実装疑義一覧（ローカル資料: `.project-guidance/ReciPro/Crystallography_計算手法の下書き_261105/ja/implementation-questions.md`）と照合した。WEB は一次論文・公式文書・実際の公開コードを使った。主な比較先は EMsoft、abTEM、py4DSTEM、diffBloch、Dr. Probe/JMultiSliceLib、Prismatic、py_multislice。

公開ソフトの出力を無条件の正解とはしない。たとえば abTEM の Bloch 波説明の経験的吸収と ReciPro の TDS 吸収は異なる模型であり、同じ入力ファイルを渡しただけでは数値一致を要求できない。[abTEM 公式解説](https://abtem.github.io/doc/user_guide/tutorials/blochwave.html)。各比較は波数・位相・単位・ADP・吸収・反射集合・厚さ・検出器の契約から揃える。

この監査は関連領域を横断したもので、139件以上のライブラリ全件を全文精読したという意味でも、すべての入力を網羅した証明でもない。既存の長時間研究・登録済み比較条件を変更せず、今回の軽量試験と区別した。

## 物理的に使える範囲

| 領域 | 支持できる部分 | 残る条件・限界 |
|---|---|---|
| 共通 Bethe | 行割り `A=D_P^-1(M+D_Q)`、相対論的波長、固有モード伝播。無吸収の保存量は `Σ(P_g/P_0)|ψ_g|²` | 前方線形化、有限 beam cutoff。単なる `Σ|ψ|²` を厳密な電流保存と扱わない。大きいNを真値と呼ぶ前に観測量ごとの収束が必要 |
| CBED | 入射方向ごとの動力学振幅と、非重複ディスク内の強度 | GUIの重なるディスクは不透明画像の上書き。局所CBEDの干渉合成でも、一般のPACBED強度和でもない |
| PED | 各歳差方位の強度の非干渉平均 | 角度の分割数とbeam数の収束が独立に必要。歳差平均で動力学が消えるわけではない |
| STEM elastic | 入射部分波の伝播・干渉と環状検出器の縮約 | Ela/TDSは既存の相対表示規格化。入射電子当たりの絶対確率ではない。EDXとは規格化が別 |
| STEM TDS/EDX | Watanabe型の吸収ポテンシャル、局所イオン化形状因子・断面積による出力 | TDS後の再散乱、強い熱散乱、低エネルギー内殻の非局所性。EDX計算値は検出X線カウントそのものではない |
| HRTEM | 出射波にレンズ収差と部分コヒーレンスを作用させる既存模型 | Es×Etは空間/時間の混合項を省略。角度が変わったときの試料内の動力学も単一exit waveでは平均できない |
| EBSD | 相反波のsource縮約、エネルギー/深さ/方向のMC重み、検出器投影 | source仮説、平均吸収の補正、経験的A(E)、表示と照合の近似差。低損失のみが菊池に寄与するとは一般化できない |
| 異方性 ADP | 外側のDebye-Waller因子のテンソル依存 | TDS内核へはG方向の有効Bを等方Bとして入れており、一般の異方性変位共分散を厳密には扱わない。これは既知疑義B1 |

`DynamicTheory.MultiSlice` は実装がコメントアウトされた廃止クラスであり、現行の独立 multislice 検証器にはならない。

## 今回のコード変更

### 1. 固有解の条件を計算別に保持

PED(A) → 通常回折(B) → PED(B) で、共有フィールドだけが B に変わり、PED 固有解は A のまま再利用された。小さな Si の再現では、旧 PED の反射数63に対し正しい新規計算は条件により76・53・85だった。Surface をその場で変更しても固有解が残り、通常回折の000強度が0.235955に対し新規計算0.296527という例も得た。

通常回折とPEDそれぞれに、実際に計算したN・電圧・方位・表面・散乱模型・beam選択設定の値を保存する。再構築失敗時は有効扱いしない。厚さだけ変更したときの固有解再利用は維持し、方位/表面だけの変更ではUを不要に破棄しない。`ClearUCache()` は依存する固有解も失効させ、公開APIでADPを変更してから明示的に破棄した場合も新規計算と一致させた。公開の可変Crystalについて、変更を自動で全追跡する改修ではない。

### 2. Bloch波の厚さ積分を安定化

`F(λ,t)=(exp(λt)-1)/λ` の旧式は小さい `λt` で桁落ちする。`|λ|` の閾値だけでは扱えない。`|λt|≤0.01` で `t·exprel(λt)` の6次級数を使い、その他は既存式を保つ。厳密な λ=0、t=0 も連続な極限になる。

managed EBSD、TDS、Kikuchi、ALCHEMI と native EBSD に適用。ALCHEMIの厚さ当たりb回のexp、native等間隔深さの指数再利用は残す。級数の数学的打切り誤差はこの範囲で2.5e-19以下だが、最終結果の誤差保証とは別。

通常程度の吸収で大きい像の誤りを見つけたという主張ではない。弱吸収/薄い深さでは旧式がゼロを返す反例があり、検査により修正を確認した。

### 3. EBSD吸収背景を電流で数える

opt-inの `ComputeAbsorbedFluxBackground` は失われた量を `1-Σ|ψ_g|²` で数えていた。表面法線成分Pが異なる場合、無吸収でもこの密度和は1でない。実際に使用するbeam集合の `P_g/P_0` を渡し、流束の損失を使う。これは源/再注入模型全体の正当性を保証する変更ではなく、保存則に関するA4の修正。GUI既定はこの背景を有効にしていない。

### 4. HRTEM TCCの厳密な集約

`I(r)=Re Σ_(g,h) c_(g,h) exp[2πi(g-h)·r]` を、同じ整数差反射 q=g-h の係数ごとに先に足す。TCC・レンズ係数はペアごとに従来どおり評価するので、和の順序以外に物理近似・cutoffを加えない。

BenchmarkDotNet、Siの傾斜方位、64×64画素、3焦点、native有効で、画素合成の平均時間は次のとおり。

| beams | ペア数 → 集約項数 | 旧 → 新 | 平均時間の比 |
|---:|---:|---:|---:|
| 50 | 1275 → 169 | 15.546 → 2.255 ms | 約6.89倍 |
| 150 | 11325 → 4190 | 147.745 → 50.785 ms | 約2.91倍 |

これは固有分解・係数構築を除いた部分の速度。3 iterationの短い計測で信頼区間が広く、精密な倍率や全HRTEMの倍率ではない。最大の像差/peakは3.86e-15。全順序対の直接和、完全コヒーレンス極限、空絞りで別途検証した。空絞りは物理的にゼロ像であり、native wrapperの0/0へ入らないよう修正した。

## PEDの角度・反射数収束を実際に調べた結果

今回作成した `tools/PedConvergenceCheck` は、生の反射別強度、角度分割差、反射数差、採用beam数、結晶CIF、native DLLのSHAを保存する診断出力である。Si/GaAs、[001]から0.5°傾斜、200 kV、歳差半角1°、N=64/128、厚さ20/100 nmを計算した。元の16/32/64方位の結果を見てから128/256へ追跡したことを明記し、初回データを保持した。

| 厚さ100 nm、2結晶×2反射数 | 反射強度ベクトルの相対L2差 |
|---|---:|
| 角度32 → 64 | 7.32–7.94% |
| 角度64 → 128 | 2.66–4.50% |
| 角度128 → 256 | 0.210–0.438% |
| 角度256固定、N64 → 128、Si | 1.056% |
| 角度256固定、N64 → 128、GaAs | 2.920% |

256方位を真値・完全な収束と認定していない。厚い試料で角度数不足の影響が大きく、その差が減ると反射数の影響が見える例である。20 nmでは32→64の差は0.0126–0.0479%と小さかった。差は一般に単調とは限らず、方位ごとのbeam選択の不連続性も含む。初回8362反射値の再計算差は最大3.24e-15だった。

生データは `tools/PedConvergenceCheck/results/261106` と `261106_refined`。相対L2の分母は大きい方の方位数/反射数の計算値のノルムで、両計算の反射の和集合上で比較し、無い反射は0として扱う。後からscale合わせをして差を縮めてはいない。

## 新しい出力と高速化の優先順位

| 優先 | 提案 | 得られる情報と採用前の条件 |
|---|---|---|
| 1 | 反射数・PED角度・深さ・照射角の収束診断 | 信号が物理変化か数値cutoffかを分離。raw強度/形状尺度を別に出し、scale最適化だけで未収束を隠さない |
| 1 | 正式な共通検出器格子とPACBED/4D-STEM | 位置分解像は複素振幅和の後に二乗。完全位置平均のPACBEDでは適切な条件で干渉項が落ちる。非重複極限と流束規格化を検証 |
| 1 | 検出器別STEMとCOM/DPC | 同じ弾性伝播を複数検出器で再利用。TDSは検出器範囲ごとのsourceが必要。厚い結晶のCOMを投影電場へ直結させない |
| 2 | EBSDのエネルギー窓・平均エネルギー | I(x,y,E)へ実測の検出器応答を畳み込む。A(E)・背景との分離、窓和の保存、既存合成への復帰を確認 |
| 2 | 厚さ/方位/U_gの感度・識別性 | 規則度、DW、厚さ、方位の相互依存を評価。解析微分/中心差分と縮退条件を照合 |
| 2 | HRTEMの焦点/角度ensemble | 空間/時間の混合項と角度依存動力学の影響を定量化。小幅極限で既存模型へ戻す |
| 研究 | 外部低次U_g/DFT電荷密度の取り込み | IAMを超える結合・電荷移動、QCBEDの電子密度差。全電子電荷・核項・U0・原点・単位・二重計上を検証 |
| 研究 | MDFF/transition potential、core-loss PACBED | 角度/損失分解の非局所励起と非弾性後の伝播。現在のEDX形状関数だけをEELSへ読み替えない |
| 研究 | physical-optics Bloch、matrix-free/Krylov、FP multislice連携 | 誤差許容を固定した比較とBDN。既存FKPを継承し、近似の異なる計算器の速度をそのまま比較しない |

根拠の例: [Mendis 2025](https://journals.iucr.org/a/issues/2025/02/00/tw5012/index.html)はBloch基底で位相格子と自由伝播を分離する方式を示す。ReciProのP_g依存・吸収・HOLZ・深さ系列を保ったまま勝るかは未測定。[Mawson et al.](https://arxiv.org/abs/2002.01595)はDPCに厚さ・方位由来の動力学的な擬似コントラストが入り、歳差で軽減できる条件を示す。ここからCOM/DPCとPEDを連携させる価値はあるが、磁性や電場の直接定量機能が現行scalar potentialに備わるわけではない。

[abTEMのDFT potential例](https://abtem.github.io/doc/user_guide/tutorials/charge_density.html)と[GPAWの全電子ポテンシャル仕様](https://gpaw.readthedocs.io/documentation/electrostatic_potential.html)は外部密度を使う設計の参照になる。[Lin et al. 2025](https://doi.org/10.1038/s41467-025-60966-0)のQCBEDは規則度と電子密度・DWを一緒に検討する先例であり、ReciProのIAM計算だけで同じ情報を既に出せるとは解釈しない。

CPU-Hermitian化、GPU置換、MKL閾値変更、float保存を無条件に再提案しない。既存Bethe技術基準（ローカル資料: `.project-guidance/ReciPro/ReciPro_BetheMethod技術基準.md`）に却下理由・再検討条件がある。FFT-Krylovは既にローカル研究実装があり、今回の新規発明ではない。R80のLUT差が小さい結果と、beam選択方式の同時間予算合格や無限半径の真値は別である。

## 検証記録

検証はIdle優先度、原則 `DOTNET_PROCESSOR_COUNT=2`, `MKL_NUM_THREADS=1`, `OMP_NUM_THREADS=1`、重い計算は1本ずつ行った。修正前はgit archiveで隔離したソースをビルドし、共有作業ツリーを巻き戻していない。

- native: Visual Studio Communityの `devenv.com ReciPro.sln /Build "Release|x64" /Project Crystallography.Native`。generic/avx2/avx512を再ビルド。初回の新helperの整数リテラルによるC++型エラーは修正し、再ビルド成功。既存Eigen由来の警告は残る。
- `tools/BlochIntegralCheck`: 独立の直接波動関数+Simpson積分432検査。旧nativeは234失敗、新3flavorは各432通過。最大 `|error|/t=1.512679e-14`。native無しを合格扱いしない。
- BetheBench quick: Si/Au/石英/黒鉛/Fe3O4 × CBED/POT/STEM/EBSD/HRTEM、40 datasets。最終.NET 11の修正前後で `--tol 1e-12` を通過、最大差/scaleは7.734e-14 (Au/EBSD)。quickのHRTEMは準コヒーレント設定で、TCCの検証は別の専用試験を使った。
- BetheBench heavy: Si、N=448、div=24、slice=0.5 nm、STEM ela/tds/bothの3 datasets。最終.NET 11で旧新 `--tol 1e-12` 通過、最大差/scaleは7.129e-16。これらのwall時間を精密な速度比較には使わない。
- 最終の `dotnet build ReciPro.sln -c Release -p:Platform=x64 -m:1`: 警告0・エラー0。全xUnit suite **6703/6703成功、skip 0**。今回の新規33件（PED等11、積分11、HRTEM8、電流背景3）を含む。新native DLLを `bin/x64/Release/net11.0-windows` へ明示コピーして実施。
- BetheDivCheck: 傾斜表面・無吸収のSi/GaAsで最大流束誤差1.78e-15。EbsdSolverCheck: RK4+Simpson独立参照にnative/managed、局所/非局所、等間隔/不等間隔で最大相対差9.55e-12、すべて通過。A4の重み付き再注入は新2波テスト、旧null規約は既存ハーネスで別に検証。

途中で別作業の.NET 11移行（csproj・global.json・CI等）が入った。これを巻き戻さず、最終suiteとquick/heavyは.NET 11で実施。旧コード隔離コピーもTargetFrameworkだけを11へ合わせ、旧/新runtimeを混ぜなかった。最初のquick照合とHRTEM BDNは.NET 10で実施した結果であり、最終suiteとは実行環境を分けて記録する。

実行ログ・旧新データ・BDN結果は `C:/tmp/recipro_dynamical_audit_261106/`。小さい検証ログとハッシュmanifestは `tools/DynamicalAudit261106/` に保存。再現ハーネスはローカル `tools`、BDNはローカル `ReciPro.Benchmarks`、説明はローカル `.project-guidance` に置く。公開リポのコミット・pushは依頼されていないため実施しない。

## 残る研究と今回認定しない事項

実測データに対する不確かさ付きの全域精度、全材料のbeam収束、異方ADP吸収の近似誤差、EBSD源の普遍模型、厚膜STEMの再散乱、局所/非局所イオン化の適用境界は別の検証が必要。ALCHEMIの傾斜表面における密度・電流・イオン化経路長の対応（既知疑義A5/A6）も、今回のA4修正とは切り分けて導出を詰める。

拡張の採否を急ぐより、同じ物理模型での数値収束、独立コードとの差、実測との差を別に記録することが、ReciProを定量的に使うための最短の改善である。
