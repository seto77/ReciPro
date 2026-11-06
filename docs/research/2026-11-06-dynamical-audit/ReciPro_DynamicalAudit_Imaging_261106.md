# ReciPro 動力学計算監査: STEM・HRTEM・EDX (261106Ch)

2026-11-06。親の総合監査の撮像分担。正本は ReciPro 配下の submodule。調査開始時 Crystallography `b2bbaca`、Native `0700faa`。本書の行番号は同時進行の他領域修正で前後するため、メソッド名も併記する。

## 1. 結論と今回の変更

1. 完全結晶の弾性 STEM と HRTEM の中心式は標準的な Bloch 波像形成と対応している。ただし「物理的に完全」とは言えない。STEM の TDS 後の再散乱、低エネルギー内殻の非局在性、厚い試料の部分コヒーレンスは別の近似である。
2. HRTEM TCC の同じ差反射 q=g-h の係数を像合成前にまとめた。近似やビーム切り捨てを追加せず、有限和の計算順だけを変えた。
3. 対物絞りを通る反射がゼロのとき native HRTEM がゼロ除算する経路を修正した。両モデルともゼロ強度像を返す。
4. 回帰テスト 8 ケースは managed 描画、native 描画の双方を含んで通過。実行時 `NativeWrapper.Enabled=True; EigenEnabled=True` を確認した。
5. BenchmarkDotNet の像合成部分では 50 本で 6.89 倍、150 本で 2.91 倍になった。全 HRTEM の高速化率ではない。短い 3 iteration のため時間の信頼区間は広く、他ジョブの影響も除去していない。

### 変更ファイル

- `Crystallography/Diffraction/BetheMethod.cs`: `GetHRTEMImage` (およそ 3822 行)。`termIndex` による TCC 集約と、空の gList のゼロ像。
- `Crystallography/tests/Crystallography.Tests/HrtemTccTests.cs`: 完全コヒーレンス極限、全順序対の独立 oracle、空絞り。
- `ReciPro.Benchmarks/HrtemTccBenchmark.cs`: 旧全ペア和を残した native 合成 kernel 比較。ローカル専用履歴リポ。

本体の public signature、Quasi-coherent モデル、native ABI は変更しない。既存コードは変更箇所近傍にコメントで保存し、261106Ch を記載した。

## 2. 調べた既存資料と既に解決済みの問題

先に `C:/Users/seto/source/repos/references/README.md`、次いで以下の設計書を読んだ。

- `ReciPro_STEM-EDX設計.md`
- `ReciPro_STEM-EDX_v4精度検討.md`
- `ReciPro_STEM-EDX_v5配備指示書.md`
- `ReciPro_py_multislice比較_指示書.md`
- `ReciPro_BetheMethod技術基準.md`

既存の py_multislice 比較では形が 1–2% 程度、絶対イオン化断面積の出典差が ±10–17%、厚み依存には吸収模型と frozen phonon の差が残ると記録されている。今回はその比較一式を再実行していない。「今回再検証済み」として扱わない。

261105Cl の STEM 色収差包絡の 1/2 欠落修正、STEM worker の共有 Bethe 状態汚染修正、実効光源サイズの単位整理は既に現行コードへ入っている。今回の新発見とは区別する。また 260802Cl の実部採用・Hermitian 残差診断と EDX 規格化修正、260711Cl の all-slice 透過係数と q-major U streaming も既存である。

## 3. 物理と実装の対応

### 3.1 弾性 STEM

`StemDoWork` は入射方向 k ごとに Bloch の透過係数 tc を作り、q=g-h ごとに二つの入射部分波の干渉を積分して像の Fourier 係数にし、実空間へ合成する。これは局所文献 Watanabe et al., Phys. Rev. B 64, 115432 (2001), Eq. (10),(11) の構造と対応する。[DOI](https://doi.org/10.1103/PhysRevB.64.115432)

対応するコードは `Lenz`、`qList/g_qIndex`、`I_Elas`、最終 q 和。k+q の波動関数は bilinear 補間しており、これは理論式そのものではなく数値近似である。Hermitian 残差、プローブ角度分割、Bloch 本数、厚み積分間隔の収束は独立に必要。既存資料にも Bloch 128→200 の非単調収束と収差つき方向求積の遅い収束が記録されている。

重要な既知残件: Ela/TDS 像は `radiusPix²` で割られている (`BetheMethod` およそ 3449 行)。EDX は q=0 の有効入射方向数で割る (`edxImageNorm`, およそ 3527/3659 行)。設計書 §5.9.10 は EDX だけを修正したと明記する。したがって STEM 参照像の生の数値を「入射電子一個当たりの検出確率」とみなせない。分割数変更で共通倍率も変わる。EDX 改修時の例ではこの共通倍率は約 2.7 だったが、全条件で一定の 2.7 ではない。

次の規格化修正では真空・全検出器の強度=1、0厚みでTDS=0、検出角の区間分割に対する加法性を最終出力で試験する。単に既存 fixture を新値へ更新するだけでは不十分。

### 3.2 TDS/非弾性 STEM と EDX

現在の `fillUq` と深さ積分は吸収を受けた coherent 波動場から局所散乱源を積み上げる。`getU` の虚部は TDS 起源のみであり `ImaginaryPotentialAbsorption` にも明記される。plasmon/core-loss を同じ虚部へ足して全量を TDS として再注入する拡張はできない。

Watanabe 2001 の Eq. (18) を使う二つの吸収ポテンシャルの差分法と、現行の source integral を同じ実装だと断言しない。ただし TDS 後の再伝搬を省略する制約は共通に重要。原著はこの省略を明示し、Si の実測との整合例を示しているが、それは任意の重元素・厚み・検出角での保証ではない。

Findlay et al., Ultramicroscopy 104 (2005) 126–140 は MDFF と frozen lattice / scattering function の組合せを扱い、強い熱散乱では core-loss 像のコントラスト自体が反転する例を示す。薄い結晶で得た形の一致だけから厚い試料の EDX/EELS を認定できない。[DOI](https://doi.org/10.1016/j.ultramic.2005.03.004)

局所ライブラリの Oxley 2003 ICSC 論文に基づく「特性 X 線 3–4keV 超で局所近似が適切」という適用範囲と、低エネルギー端での非局在性は区別する。O-K 等の適合性は別途 MDFF/transition-potential の参照が必要。EDX 生成空孔、蛍光線生成、自己吸収、検出効率の段を混同しない。

### 3.3 HRTEM の部分コヒーレンス

局所文献 Kirkland, *Advanced Computing in Electron Microscopy*, 3rd ed. (2020) §5.4.3 (本文 pp125–128; PDF pp135–138) と §6.10.3 (本文 pp184–185; PDF pp193–194) を確認した。式ページは画像でも照合した。

現在の TCC は各ペアに coherent 位相差と空間包絡 Es、時間包絡 Et を掛ける。完全コヒーレンス β=δ=0 では、直接波動関数の絶対値二乗へ帰着することを今回のテストで確認した。

ただし Kirkland Eq. (5.37),(5.41),(5.45) の焦点ぶれ×照射角の混合項 δf·kβ は現行の EsEt 積では省略される。また対物絞り境界の照射角依存も積分しない。δ は ReciPro では標準偏差、教科書の Δ0 は 1/e 幅で Δ0=√2δ なので、式を移す際に混同しない。式(5.39)の印字された和/差は前式との整合を自分で微分して確認すべきで、転記だけを oracle にしない。

混合項の大きさをみる無次元量は `η=2π²β²δ²|q|²`。β=0.1mrad、δ=3nm、|q|=10nm^-1 なら η≈1.78e-4。一方 β=1mrad、δ=10nm、|q|=20nm^-1 では η≈0.790。η は像誤差そのものではなく、積近似が十分小さいと即断できるかの尺度である。

より大きな制約は、単一入射方位で得た exit wave に TCC を適用している点。厚い結晶では照射角ごとに動力学的励起が変わる。角度ごとに波動場を計算して強度を非干渉平均する方法が必要で、単に Es を詳しくしても解消しない。これは新しい「確定バグ」ではなく近似の適用範囲。

## 4. TCC 集約の根拠と検証

従来は各ペア p=(g,h) に対して、画素 r と defocus d ごとに `Re[ψ_p L_p(d) exp(2πiq_p·r)]` を加算していた。同じ q の位相は全ペアで同じなので、`C_q(d)=Σ_{p:q_p=q}ψ_p L_p(d)` を先に作れる。新コードは `Psi=1, Lenz=C_q` として既存 solver へ渡す。

辞書の鍵は整数 hkl 差。浮動小数点座標の近さではまとめない。最初に現れたペアの Vec を保持する点も旧 `vecDic` と同じ。加算順序と乗算結合が変わるため bit 同一ではないが、物理モデルは同一。

空絞りでは `NativeWrapper.HRTEM_Helper` が空配列を返し、`HRTEM_SolverGrid` 内の `gLenz.Length/gPsi.Length` が 0/0 になる。新コードは像配列を 0 で初期化した後、gList が空ならその厚みの合成を終える。

### テスト

`dotnet test Crystallography/tests/Crystallography.Tests/Crystallography.Tests.csproj -c Release -p:Platform=x64 --filter FullyQualifiedName~HrtemTccTests --logger 'console;verbosity=detailed'`

- 傾斜 Si、50 beams、厚み0/17/43nm、焦点-45/0/20nm、9×7画素: 全ての順序対(g,h)を足す独立 oracle と比較。集約コードを oracle に使わない。
- β=δ=0 の TCC と Quasi の波動強度比較。
- Quasi/TCC × native/managed の空絞りゼロ像。
- 合計8件PASS。初回はDLL無しのmanaged fallbackも含んだ。続いて `ReciPro/bin/Release/Crystallography.Native*.dll` を test の **bin/x64/Release/net10.0-windows** へ明示コピーし再実行、全ケースで `NativeWrapper.Enabled=True; EigenEnabled=True` を記録、8件PASS。テストの `native:false` はこの状態でも managed の画素合成を明示使用する。

### BenchmarkDotNet

Idle、`DOTNET_PROCESSOR_COUNT=2`、`MKL_NUM_THREADS=1`、`OMP_NUM_THREADS=1`。64×64画素、焦点3点。EVD と係数生成は Setup に置く。

| 実際のbeam本数 | 旧ペア数 | 集約後 | 旧 Mean | 新 Mean | Mean比 | 最大差 / peak |
|---:|---:|---:|---:|---:|---:|---:|
| 50 | 1275 | 169 | 15.546ms | 2.255ms | 6.89倍 | 1.93e-15 |
| 150 | 11325 | 4190 | 147.745ms | 50.785ms | 2.91倍 | 3.86e-15 |

150本で縮約比が小さくなるのは、選択基底が二次元の密な反射群だけではないため。N倍速になる等の一般則は主張しない。BDN 3 iteration/1 launch、相対標準偏差は約3–10%、99.9%信頼区間は広い。測った範囲は像合成 kernel のみ。

結果: `C:/tmp/recipro_dynamical_audit_261106/hrtem_bdn/results/ReciPro.Benchmarks.HrtemTccBenchmark-report-github.md`、同 `report-full-compressed.json`。

## 5. WEB と公開実装の確認

2026-11-06に公式リポ/APIで HEAD と license metadata を確認した。ソースの移植はしていない。ライセンス列は配布の法的判断ではなく、取得時に表示された識別子。

| ソフト | 読んだもの・固定版 | 実装との対応と判断 |
|---|---|---|
| abTEM | [repo](https://github.com/abTEM/abTEM)、dev `fd96a54d5127d21e279740284c2b041a45b65b80`、GPL-3.0。`abtem/detectors.py`、`abtem/prism/s_matrix.py` の source を確認 | pixelated/segmented/annular detector を分離し、S-matrixと位置係数を tensordot で合成。ReciProでも伝搬と検出器縮約を分離するとmulti-detector/4Dの土台になる。 |
| Prismatic | [repo](https://github.com/prism-em/prismatic)、master `d155fb931abaa22d5d2442d2e1a28a6657ac74da`、GPL-3.0。README確認 | 2026-01から積極保守を終了しabTEMを案内している。新しい独立検証基盤の第一候補はabTEM。 |
| py_multislice | [source](https://github.com/HamishGBrown/py_multislice/blob/5e5e4d81c9e1caf600cfe6ab706f98a1e2f1f90f/pyms/Premixed_routines.py)、master `5e5e4d81c9e1caf600cfe6ab706f98a1e2f1f90f` | 既存資料の frozen-phonon 断面像平均の欠落を現在の source でも確認: `STEM_crosssection_images = result[...]` の上書き、外側ではADF/PACBED/4Dのみnfph除算。既存の外側平均ドライバが必要。GitHub API license=null、root treeにLICENSEなし。自由な再配布可とは解釈しない。 |
| Dr. Probe / JMultiSliceLib | [source](https://github.com/ju-bar/JMultiSliceLib/blob/079ff30ac9b3c7414e34c1964b4295ca5ad7836c/src/JMultiSlice.cpp)、master `079ff30ac9b3c7414e34c1964b4295ca5ad7836c`、GPL-3.0 | `PhaseGratingSetup`, `PropagatorSetup`, `SetDetectionPlanes`, `DetectorSetup` のCPU/GPU・slice variantの分離を確認。[公式frozen-lattice解説](https://pgi-jcns.fz-juelich.de/barthel/drprobe/drprobe-efficientfl.html)は振動配置を扱う独立参照の位置づけに有用。 |

固定版 abTEM source:

- [detectors.py](https://github.com/abTEM/abTEM/blob/fd96a54d5127d21e279740284c2b041a45b65b80/abtem/detectors.py)
- [s_matrix.py](https://github.com/abTEM/abTEM/blob/fd96a54d5127d21e279740284c2b041a45b65b80/abtem/prism/s_matrix.py)
- [PRISMの公式説明](https://abtem.github.io/doc/user_guide/tutorials/prism.html)
- [検出器とDPCの公式説明](https://abtem.github.io/doc/user_guide/walkthrough/scan_and_detect.html)

ReciPro は既に入射方向ごとの伝搬を再利用している。PRISM の「位置ごとにmultisliceを再計算する実装」との対比で得られた速度倍率を、現行ReciProへの予測値に移してはいけない。

## 6. 次に有効な高速化・物理出力

| 優先 | 案 | 必要な検証 / 限界 |
|---|---|---|
| 高 | HRTEM TCCの今回の厳密集約 | 実装・数値検証済み。さらに FFT/NUFFT 化は像の周期性、任意方位・有限画素格子との対応が必要。 |
| 高 | STEM Ela/TDS の絶対規格化を明示 | 真空・0厚み・全検出器・検出器区間加法性、旧表示倍率の影響を確認。 |
| 高 | 伝搬結果から複数検出器を同時縮約 | 現行はDと検出器別TDSポテンシャルが一つのrunへ結合。Elasticは容易だがTDSは検出器ごとの角度積分が必要。 |
| 中 | 共通回折面での4D-STEM / PACBED | **位置分解CBED/4D-STEM** は各入射部分波を同じ検出器kへ配置し、複素振幅を足した後に二乗する。disk強度の単純和では干渉を失う。一方 **PACBEDは完全な位置平均で交差項が消える条件なら非干渉和でもよい**。有限走査・非一様重み・不完全な単位胞平均では別途交差項を検証する。透過係数・probe位相・補間・フラックス規格化と位置平均の重みを保存する。 |
| 中 | COM/DPC・任意segmented detector | 4D出力の一次モーメント/区間積分として定義できる。現在のscalar electrostatic modelから磁性を出せるとは言わない。厚い結晶のCOMをそのまま投影電場と同一視しない。 |
| 中 | 厳密なGaussian焦点/角度ensemble HRTEM | 焦点平均は同じexit waveで安価、角度平均は方向ごとの伝搬が必要。小β/δで現TCCへ戻り、混合項を含む直接求積とも一致すること。 |
| 中長期 | MDFF/transition potential によるcore-loss PACBED | EDXの局所σF(q)だけから角度分解EELSは作れない。損失エネルギー依存、非局所頂点、非弾性後の伝搬と検出器収集角が必要。 |
| 中長期 | physical-optics Bloch / matrix-free伝搬 | Mendis 2025 は構造行列からphase-gratingとfree-propagatorを分離し、1slice O(N²)化。[原著](https://journals.iucr.org/a/issues/2025/02/00/tw5012/index.html)、DOI10.1107/S2053273325000142。EVDの再利用が利く厚み系列では必ずしも勝たない。slice収束・flux・相反性・現在の3D/HOLZ基底との一致がゲート。 |

新しい物理の優先順位は「今の観測量の規格化と収束の可視化 → multi-detector/4D → 非局所core-lossと再散乱」。一度に計算器を置き換えるよりも、共通の伝搬出力と検出器縮約を分け、両者を独立に照合できるようにするのが有効。

## 7. 今回認定しなかったもの

全元素・全方位・全電圧・全厚みに対する定量物理精度、厚い試料のfrozen-phononとの一致、実測データへの不確かさ込み適合、STEMの絶対検出確率、磁性・結合電場の定量出力は認定していない。今回の実装は HRTEM の数学的同値な高速化と空絞りの修正であり、物理模型を拡張したと称さない。
