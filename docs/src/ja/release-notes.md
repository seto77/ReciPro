# 更新履歴

<!-- 261005Cl 新設: GitHub の Release ページには 1 行の要約とダウンロードの表だけを載せ、各版の詳細はこの頁に書く (作者指示)。
     v.4.919 以降の節は History の 1 行と git の履歴 (root と submodule の commit の題) から書いた。
     「それ以前の版」は ReciPro/Version.cs の History (ヘルプ ▸ バージョン履歴と同じ文) から機械で写した。 -->

ReciPro の各版で何が変わったかをこの頁にまとめます。GitHub の [Release ページ](https://github.com/seto77/ReciPro/releases) には各版の 1 行の要約とダウンロードのリンクだけを載せ、詳細はここに書きます。v.4.919 以降の版にはそれぞれの節があり、それより前の版は 1 行の要約を並べます。

## v.4.949 (2026-10-04)

EBSD シミュレーションを作り直しました (非局所後方散乱源を既定で ON、モンテカルロ法による深さとエネルギーの扱い、背景の平坦化、晶帯軸の選択、回転動画)。同梱の Temari のイオン化テーブル (dataset 7.0.0) と散乱因子テーブル (dataset-factors v2.0.0。TDS 吸収にも使うようになりました) を更新し、ネイティブの EBSD ソルバの誤り (v.4.918〜v.4.948) を直し、STEM-EDX と ALCHEMI に X 線の線の系列 (任意) を加えました。

- **EBSD (修正)**: ネイティブの EBSD ソルバ (v.4.918〜v.4.948) の複素共役の誤りを直しました。局所後方散乱源と、任意の TDS 背景に影響していました。修正の直前のコードで測ると (9 結晶、20 kV)、規格化したマスターパターンは修正後のソルバと 7〜38 % (相対 L2 ノルム) 違いました。これは誤りの大きさで、v.4.948 と v.4.949 の差ではありません。旧「Include TDS background」は「非局所後方散乱源」になり、局所源を置き換えるもので、既定で ON です。
- **イオン化テーブル (STEM-EDX、ALCHEMI)**: Temari の dataset 7.0.0 (DOI 10.5281/zenodo.22643468)。計算の全体で有限核を使った最初の版です。dataset 5.0.0 からの F の変化は F(0) = 1 に対して最大 1.7 × 10⁻³ で、数値の方法の更新を含みます。
- **散乱因子と TDS 吸収**: Temari の dataset-factors v2.0.0 (DOI 10.5281/zenodo.22820415)。値は Ba と Ta の最後の格納桁を除いて変わらず、すべてのテーブルが「計算値であり、認証されていない」と宣言されるようになりました。TDS 吸収は、中性原子 (Z = 1–86) の s = 6 Å⁻¹ までこの f\_e を使うようになり、ベンチマーク計算 (5 結晶、80〜300 kV、厚さは STEM と HRTEM が 10〜20 nm、CBED が 100 nm まで) では STEM・HRTEM・CBED の結果の変化は最大 0.45 % でした ([付録 A3](appendix/a3-bloch-wave/calculation.md) を参照)。
- **X 線の線の系列 (STEM-EDX、ALCHEMI)**: Kα・Kβ・Lα・Lβ・Mα のチャネル (既定は OFF) を加えました。値は自己吸収と検出の前の、入射電子 1 個あたりに発生する X 線の光子数で、xraylib 4.2.1 の蛍光収率・Coster–Kronig と放射の連鎖・線の分岐比を使い、Auger の連鎖は含みません ([STEM-EDX](9-hrtem-stem-simulator/2-stem-simulation.md#stem-edx)・[ALCHEMI](7-diffraction-simulator/4-alchemi-simulation.md#出力量) を参照)。
- **帰属表示**: CITATION.cff・README・THIRD-PARTY-NOTICES・マニュアルの最初の頁の License の節・ヘルプ › ライセンスの窓が、同梱の 2 つの Temari のテーブル (CC BY 4.0) を引用または案内するようになりました。

## v.4.948 (2026-08-30)

Bloch 波の動力学計算で、各回折波の振幅に P\_g/P\_0 が掛かっていた古くからの誤りを直しました。晶帯軸入射の SAED・HRTEM・STEM には影響せず、EBSD・菊池バンド・HOLZ 反射が最も大きく影響を受けていました。

- 固有値問題で、列ではなく行 g を P\_g で割るようにしました。使われていなかった CBED の傾斜の変数を除きました。
- マニュアル全体を 11 言語で校正し、別のウィンドウ・赤い ×・準備前の状態が写っていたスクリーンショットを撮り直しました。
- Spot ID の情報の表で累積の列が埋まるようになり、指数の入力欄と数値のスピンボタンに親のコントロールのツールチップが出るようになりました。
- PureHDF (2.2.0) と DynamicExpresso を更新しました。

## v.4.947 (2026-08-20)

ALCHEMI シミュレータ (プレビュー) を加えました。系統列に沿ったサイト別のイオン化ロッキングカーブを Bloch 波法で計算し、角度の広がりの畳み込み、出所を記録した CSV 出力、11 言語のマニュアルの頁を備えます。回折シミュレータに動力学的な菊池バンドを、ビーム相互作用に Temari の散乱因子と積分による吸収因子を加え、EBSD パターンの保存と CITATION.cff を加え、Spot ID の R(%) の往復の誤りを直しました。

- **ALCHEMI** (マニュアルの 7.4 節): 局所イオン化行列とそのモード縮約による 1 次元の順方向の方位計算、角度の広がりの畳み込み、イオン化テーブルと吸収ポテンシャルの出所を見出しに記録する CSV。
- **動力学的な菊池バンド** (回折シミュレータ): 系統列の多波のプロファイルと Einstein モデルの TDS 源、Linear/Log/Tanh の強度スケール、過剰線・欠損線の色。描画は同じ出力のまま 2.1〜6.3 倍速くなり、バンドが左右反転していた誤りを直しました。
- **ビーム相互作用**: 散乱因子の出典に Temari を加え、X 線の f(s) の出典を表に示し、吸収因子を積分で評価するようにしました。
- **STEM-EDX**: イオン化テーブルが M 殻を収録し、8 Å⁻¹ より先を外挿せずに s = 16 Å⁻¹ までデータで覆うようになりました。形状因子を打ち切った所を GUI に示します。
- EBSD パターンをコピーだけでなく保存できるようにし、マクロから Spot ID のスポット半径を設定できるようにし、ポータブル ZIP でも「更新の確認」で最新の Release ページを開くようにし、Bote–Salvat の文献を訂正しました。

## v.4.946 (2026-08-05)

STEM-EDX シミュレーションを加えました。特性 X 線 (内殻イオン化) のマップを STEM 像と並べて Bloch 波法で計算し、独自の完全相対論的なイオン化形状因子テーブル (K: C–Sn、L: Ca–Rn) を使います。11 言語で説明しています。マクロから結晶を作成・編集し Structure Viewer を操作できるようにし、配位多面体にシースルーのメッシュ表示を加えました。

- **STEM-EDX**: j で分けた相対論的な L 副殻を Z = 86 まで収録したイオン化テーブル、続いて v3 のテーブル (246 チャネル、s のグリッドを 8 Å⁻¹ まで延長)。ネイティブの補助ライブラリが無いときは、ローカライズしたメッセージで止めます。
- **マクロ**: 結晶の作成と編集 (draft/Commit の API)、結晶の方位の読み出し、Structure Viewer の操作 (画像と 3D プリント用のモデルを含む) ができるようになりました。マクロの説明に実際の引数を示します。
- **3D プリント**: Structure Viewer から binary STL と、色ごとに分けた 3MF を出力できます。単位胞の稜は印刷できる円柱にし、11 言語のオプションの窓を付けました。
- README を 10 言語で加え、STEM-EDX と py\_multislice の比較を PDF の報告として公開しました。

## v.4.945 (2026-08-03)

ダークモードに対応し、キーボードとマウスでの操作性を高め、実測 EBSD パターンの指数付けを検出器の幾何の自動較正で強化し、高 DPI での起動時のクラッシュを含む多くの不具合を直しました。

- **ダークモード**: 対称性の図・極点図・グラフ・3D 表示も含みます。ダークとライトの切り替えでアプリケーションが自動で再起動します。
- **キーボードとマウス**: メニューの標準の Alt のアクセスキー、回折図形のホイールと +/- での拡大縮小、数値欄の矢印キーでの増減。Enter で入力値が捨てられないようにしました。電卓の機能は廃止しました。
- **EBSD の指数付け**: 検出器の幾何の較正を最大 200 通りの初期値から繰り返し、6 変数の同時の仕上げで終えます。方位は 0.1° のシンプレックスで仕上げ、探索の進み・経過時間・中止を表示します。
- **修正**: 200 % DPI での起動時のクラッシュ、動力学モードで結晶ごとの色が効かない問題 (issue #65)、貼り付けた回転行列、ツールバーのダブルクリック、11 言語の監査で見つかった文字のはみ出し。xraylib のバイナリを 1 本 14.8 MB から 6.3 MB に縮めました。

## v.4.944 (2026-07-25)

実測 EBSD パターンの指数付け (方位の探索と検出器の較正)、名前付きパイプによるマクロの外部制御と無人のコマンドライン実行、Spot ID と回折スポット情報の表 (CSV) の出力を加え、アプリケーション全体で性能を改善し多くの不具合を直しました。

- **実測 EBSD パターンの指数付け**: 長方形の検出器、画像の重ね合わせ、Radon のテンプレート照合または辞書探索 (点群による絞り込みで数倍速い) による方位の探索、検出器の表示の左右反転、検出器の幾何の保存。
- **外部制御**: 名前付きパイプでマクロを送れるようにし、コマンドラインは /o で静かに実行できます。
- **Spot ID** と回折スポット情報の表を出力できます。マクロの SpotInfo() が運動学モードでも使え、検出器の座標も返します。
- 系統的な監査で、ライブラリ全体のリソースの漏れと数値の誤り (フーリエ変換・Marquardt 法・画像の入出力・並行処理)、フレームごとの GPU バッファの漏れ、プロセス間での結晶のコピーの失敗を直しました。

## v.4.943 (2026-07-15)

空間群の群–部分群関係を調べる「Group Relations」機能 (極大部分群・極小超群、Bärnighausen の木、対称要素の図) を加え、任意の Intel MKL への対応で STEM シミュレーションをさらに速くしました。

- **Group Relations**: 極大の t・k・同形部分群と極小超群、変換行列、多段の Bärnighausen の木、軌道の分裂・ドメイン・新しい反射、失われる対称要素と残る対称要素を重ねる Elements & Positions の図、32 点群の Hasse 図。
- **対称性情報**: Operations・Properties・Settings のタブを加え、Seitz 記号を LaTeX で表示します。
- **STEM**: 大きな固有値問題に Intel MKL を使えるようにし、計算で確保するメモリを減らしました。
- ライブラリの蛍光収率の誤記・検出器の単位・積分範囲を直し、翻訳した 9 言語の UI を校正しました。

## v.4.942 (2026-07-01)

インストーラとポータブル版の実行ファイルにデジタルコード署名を付けました (SignPath Foundation の無償提供)。

- 共有ライブラリ (Crystallography・Crystallography.Controls・Crystallography.Native・Crystallography.OpenGL) を git の submodule にしました。
- UI の言語を切り替えるとアプリケーションが自動で再起動し、F1 とヘルプは UI の言語のマニュアルを開きます。

## v.4.941 (2026-06-23)

UI の多言語化に対応し、多くの不具合を直しました。

- ツールチップを含む UI を 11 言語 (英語・日本語・ドイツ語・フランス語・スペイン語・イタリア語・ロシア語・簡体字中国語・繁体字中国語・韓国語・ポルトガル語) で使えるようにし、オンラインのマニュアルも同じ言語に訳しました。
- フォントを一か所で決める仕組み (大きさの段階・言語ごとのフォント・Wine 対応) にして、どの言語でも配置が収まるようにしました。
- README とマニュアルにデモ動画を加え、release の後に Arm64 のファイルを自動で添付するようにしました。

## v.4.940 (2026-06-14)

イオンの弾性散乱因子の計算の正確さを高め、配布物の形式を整理しました。

- 動力学計算でイオンの (完全な Peng の) 電子散乱因子を使えるようにしました (オプション ▸ イオン散乱因子を使用。既定は OFF)。
- 配布ファイルの名前を変え (ReciPro-setup.msi、x64 の付かないポータブル ZIP)、Arm64 の MSI を加え、古い Visual Studio のインストーラのプロジェクトを除きました。

## v.4.939 (2026-06-13)

Arm64 環境への対応を強化し、ビーム相互作用の不具合を直しました。

- **Windows on Arm**: ネイティブの Arm64 のポータブル ZIP と MSI。ネイティブのライブラリはソースからビルドしました (xraylib 4.2.1、GLFW 3.4)。Arm64 では MKL の設定を隠します。
- インストーラを WiX v7 に移し、ポータブル版を単一ファイルで発行するようにしました (292 ファイルから約 10 ファイル)。チェックサムのファイルは公開しなくなりました。
- **ビーム相互作用**: 電子の阻止能を |dE/ds| として説明し、グラフに色付きの凡例を付け、組成を多重度 × 占有率で重み付けするようにしました。
- 回折シミュレータで、高 DPI の画面でタブの見出しが空になる問題を直しました。

## v.4.938 (2026-06-11)

STEM シミュレーションなどの計算を少し速くし、Wine による macOS での実行に試験的に対応しました。

- ネイティブの行列積と位相の漸化式で、Bloch 波の STEM・HRTEM・CBED を速くしました。
- macOS と Linux の Wine で動かすためのフォントの互換層を加えました。
- Rigaku 2D-PXD (d*TREK の拡張 IMG) の画像を読めるようにし、数値の表示に書式指定を使えるようにしました。
- マニュアルにビーム相互作用の固体物理の付録を加えました (Bloch 波の付録は A3 になりました)。

## v.4.937 (2026-06-08)

「散乱因子」を大幅に作り直し、吸収係数と蛍光の情報も含む「ビーム相互作用」として公開しました。

- ビームの種類ごとのタブ: 散乱因子 (xraylib による)・減衰・蛍光と、電子の弾性散乱断面積と CSDA 飛程、中性子の非干渉性散乱と吸収の断面積。
- 回折シミュレータで X 線の異常分散を切り替えられるようにしました。
- 構造因子の計算と消滅則の判定を速くし、結晶の比較と原子の削除の不具合を直しました。
- タイトルバーに「(F1: Help)」を出し、同梱の PDF のヘルプをオンラインの付録へのリンクに替えました。

## v.4.936 (2026-06-04)

冗長なデータを除いて、配布物をさらに小さくしました。

- Crystallography.dll を小さくしました: 対称性の表を読める CSV のリソースに移し、NIST のデータを Brotli で詰め、結晶の XML から既定値の項目を省きました。
- NIST の弾性散乱の表を 20 keV から 36.4 keV に広げ、グラフのカーソルに交点の印と値を出すようにしました。

## v.4.935 (2026-06-02)

ポータブル ZIP の配布を加え、マニュアルを改善し、不具合を直しました。

- 動力学回折の計算の流れ (Bloch 波法・EBSD・ネイティブのラッパ) を最適化しました。
- マニュアルに回折シミュレータのモードごとの頁とキーボードのショートカットの章を加え、アプリケーション全体にツールチップを加えました。
- 中性子の散乱長を periodictable のデータに更新しました。
- ヘルプのメニューから重複したマニュアルの項目と同梱の PDF のマニュアルを除き、LICENSE.md を標準の MIT の文にしました。

## v.4.934 (2026-05-30)

動画のエンコードの仕組みを改善し、配布物を小さくしました。

- 動画の出力を GPL の ffmpeg から Windows Media Foundation に移しました。
- F1 で各ウィンドウのマニュアルの頁を開けるようにしました。
- 第三者の告知と、コード署名の方針を加えました。

## v.4.933 (2026-05-29)

Windows on ARM (x64 エミュレーション) で OpenGL の描画が崩れる問題を直しました。

## v.4.932 (2026-05-28)

マニュアルを改善し、ステレオ投影の機能を強化しました (https://github.com/seto77/ReciPro/issues/58 を参照)。

- **ステレオネット**: 線の太さとラベルの色を設定できるようにしました。
- **マニュアル**: GitHub Pages (MkDocs) に移し、数式を MathJax で表示し、座標系と Bloch 波法 (CBED・STEM・EBSD) の付録、GitHub の issue から集めたトラブルシューティング、GUI から生成したスクリーンショットを加えました。
- GUI 全体で用語・単位・状態の表示を揃え、残っていた NumericUpDown を NumericBox に替えました。

## v.4.931 (2026-05-19)

高 DPI の表示設定で起きていた GUI の配置の問題を直しました (https://github.com/seto77/ReciPro/issues/59 を参照)。

- 配置をフローパネルで作り直し、表と指数の入力欄を DPI に追従させ、h・k・l の別々の入力を共通の指数の入力欄に替えました。

## v.4.930 (2026-05-17)

ステレオ投影で大円の描画を直し、カーソル位置の面・軸の指数の表示を加えました (https://github.com/seto77/ReciPro/issues/58 を参照)。

- 対称性の図の描画を速くしました。

## v.4.929 (2026-05-13)

Structure Viewer での対称要素の描画の不具合を直し、性能を改善しました。

- 対称要素の設定のパネルを加え、主軸と鏡映面・映進面を正しく決めるようにしました。
- SixLabors への依存を除きました。

## v.4.928 (2026-05-09)

Structure Viewer で対称要素を描画する機能を加えました。

- 対称性の図: 試験点と投影の軸を選べるようにし、BMP または EMF でコピーできるようにしました。

## v.4.927 (2026-05-04)

「対称性情報」を大幅に拡張し、ITC Vol. A の形式で対称要素と一般位置の模式図を描くようにしました。

- 図は立方晶系の群にも対応し、らせん軸を中心化を考えて判定します。六方晶の設定の軸を直しました。
- 対称性情報の数式を LaTeX で表示し、対称操作の式の解析を DynamicExpresso に移しました。

## v.4.926 (2026-04-25)

Miller–Bravais の指数表記 (三方晶・六方晶の格子面の hkil の 4 指数表示) に対応しました (https://github.com/seto77/ReciPro/issues/54 を参照)。

- 4 指数の表記をメインウィンドウ・ステレオネット・イメージシミュレータ・Spot ID で使います。
- 保存された位置が画面の外にあるとき、メインウィンドウを画面の中に戻すようにしました。

## v.4.925 (2026-04-20)

OpenGL の初期化に失敗しても起動を続けるように、アプリケーションの起動を堅くしました (https://github.com/seto77/ReciPro/issues/55 を参照)。

- Bloch 波と HRTEM の計算を速くし、ネイティブのビルドの設定を最適化しました。マクロの窓で同梱のサンプルの表示を切り替えられます。

## v.4.924 (2026-04-15)

マクロ関連の機能を強化しました。

- マクロのエディタを作り直して同梱のサンプルを付け、マクロのヘルプを整え、連続画像の枚数の設定の不具合を直しました。
- 独自のスピンボタンで高 DPI の配置を直し、推奨する論文の引用を CITATION.cff に書きました。

## v.4.923 (2026-04-05)

小さな不具合を直しました。

- release のワークフローを作り直しました。

## v.4.922 (2026-04-05)

インストーラのパッケージを大幅に小さくしました。

- release を GitHub Actions でビルドして公開するようにしました (tag は更新の確認が期待する「v.」の接頭辞を使います)。

## v.4.921 (2026-04-01)

EBSD シミュレーションを改善し、多くの小さな不具合を直しました。

## v.4.919 (2026-03-20)

OpenGL の描画と EBSD シミュレーションを改善し、多くの小さな不具合を直しました。

- OpenGL のコントロールと描画オブジェクトを作り直し、Bloch 波の EBSD の計算とネイティブのライブラリを更新しました。

## それ以前の版

以下の 1 行の要約は、ReciPro の **ヘルプ ▸ バージョン履歴** に出るものと同じです。2011 年の ver4.10 より後は英語で書いています (原文のまま載せます)。

### 2026

- **ver4.918** (2026/03/13) Improved the Native Library to enable automatic switching between non-AVX, AVX2 and AVX512.
- **ver4.917** (2026/03/05) Added several 'Macro' functions (see https://github.com/seto77/ReciPro/issues/36).
- **ver4.916** (2026/01/14) Fixed an issue with loading Crystallography.Native.dll.

### 2025

- **ver4.915** (2025/12/25) Improved: Equivalent axes/planes can be color-coded in 'Stereonet'.
- **ver4.914** (2025/12/21) Added: 'TEM holder simulation' to 'Diffraction Simulator'. Miller-Bravais index option to 'Stereonet'. (see https://github.com/seto77/ReciPro/issues/52)
- **ver4.913** (2025/12/12) Fixed bugs on program update and crystal database functions.
- **ver4.912** (2025/12/10) Updated AMCSD database. Improved to run on Windows on ARM64.
- **ver4.910** (2025/11/26) Updated: .Net Desktop Runtime 9 to 10. Fixed minor bugs.
- **ver4.909** (2025/10/29) Fixed a minor bug.
- **ver4.907** (2025/10/28) Fixed a minor bug.
- **ver4.906** (2025/09/26) Fixed a minor bug. Renewed the Eigen library.
- **ver4.905** (2025/09/14) Added several 'Macro' functions.
- **ver4.904** (2025/08/04) Fixed a minor bug.
- **ver4.903** (2025/05/29) Improved the 'Crystal Database' function. COD has been available.
- **ver4.902** (2025/05/13) Fixed a minor bug.
- **ver4.901** (2025/04/10) Added several 'Macro' functions (ReciPro.Crystal.\*). See https://seto77.github.io/ReciPro/en/20-macro/.
- **ver4.900** (2025/04/08) Fixed a minor bug.
- **ver4.899** (2025/04/01) Added some built-in functions for macro (see https://github.com/seto77/ReciPro/issues/45).
- **ver4.898** (2025/03/04) Added right-click menus for the selected crystal. Fixed a bug related to https://github.com/seto77/ReciPro/issues/44.
- **ver4.897** (2025/01/30) Improved: The macro function has been enhanced. See https://seto77.github.io/ReciPro/en/20-macro/.
- **ver4.896** (2025/01/17) Fixed some bugs on OpenGL renderings.

### 2024

- **ver4.895** (2024/11/14) Updated: .Net Desktop Runtime 8.0 to 9.0. Updated the bundled crystal database.
- **ver4.894** (2024/11/01) Fixed bugs on the 'Diffraction Simulator' and 'HRTEM/STEM simulator' (thanks to lukmuk-san and Nakamura-san!).
- **ver4.892** (2024/10/04) Added several 'Macro' functions.
- **ver4.891** (2024/09/06) Added the function to simulate electron trajectories based on the Monte Carlo method.
- **ver4.890** (2024/08/10) Improved 'Macro' functions.
- **ver4.889** (2024/08/09) Fixed bugs on the 'Diffraction Simulator'.
- **ver4.888** (2024/08/07) Added the 'Kikuchi line pairs' projection mode to the 'Stereonet' simulation. (see https://github.com/seto77/ReciPro/issues/35)
- **ver4.887** (2024/07/30) Fixed a minor bug.
- **ver4.886** (2024/07/20) Added horizontal/vertical flip and color inversion functions for 'Diffraction Simulator'  (see https://github.com/seto77/ReciPro/issues/35).
- **ver4.885** (2024/06/20) Fixed a bug and typo in the 'Diffraction Spot Information' (see https://github.com/seto77/ReciPro/issues/34, thanks to tianyu-liu-san).
- **ver4.884** (2024/05/24) Fixed a bug in the calculations of dynamical theory.
- **ver4.883** (2024/04/06) Fixed minor bugs in the 'CBED setting'. Update bundled libraries.
- **ver4.882** (2024/03/16) Improved GUI in the 'Diffraction simulator'. (see https://github.com/seto77/ReciPro/issues/30, thanks to lukmuk-san)
- **ver4.881** (2024/03/11) Checked security problem (see https://github.com/seto77/ReciPro/issues/31).
- **ver4.880** (2024/03/09) Improved GUI in the 'Diffraction simulator'. (see https://github.com/seto77/ReciPro/issues/30, thanks to lukmuk-san)
- **ver4.879** (2024/03/05) Fixed GUI in the 'HRTEM/STEM simulator'. (see https://github.com/seto77/ReciPro/issues/29, thanks to JingshanDu-san)
- **ver4.878** (2024/02/13) Added options for saving movies.
- **ver4.877** (2024/02/11) Added: Back Laue camera mode (X-ray diffraction) (see https://github.com/seto77/ReciPro/issues/28). Improved registry read/write behaviour at startup.

### 2023

- **ver4.876** (2023/12/21) Fixed an issue where icon images were not displayed correctly.
- **ver4.874** (2023/12/08) Improved 'Structure Viewer': Double-clicking on an atom to display its coordination environment, etc.
- **ver4.873** (2023/12/07) Improved the bounds options on 'Structure Viewer'.
- **ver4.871** (2023/11/29) Fixed bugs on 'Structure Viewer'.
- **ver4.870** (2023/11/21) Target framework has been changed to .Net Desktop Runtime 8.0.
- **ver4.869** (2023/10/26) Added the function to simulate the Ewald sphere and the reciprocal vectors to 'Diffraction Simulator'.
- **ver4.868** (2023/10/23) Fixed an issue with text rendering using OpenGL (see https://github.com/seto77/ReciPro/issues/26).
- **ver4.867** (2023/08/04) Improved the interface of SpotID v2 (see https://github.com/seto77/ReciPro/issues/25).
- **ver4.866** (2023/08/01) AVX2 support temporarily suspended. Fixed a bug when using AMD Radeon GPUs.
- **ver4.865** (2023/06/23) Fixed: GUI issues when changing language.
- **ver4.864** (2023/06/19) Added: the length and F (structure factor) of the g vector are displayed when the spot is double-clicked (see https://github.com/seto77/ReciPro/issues/21).
- **ver4.862** (2023/05/16) Fixed minor bugs. Improved macro functions. Added command-line options.
- **ver4.861** (2023/04/12) Added macro functions to automate various tasks (mainly 'Diffraction Simulator' at the moment).
- **ver4.860** (2023/04/06) Improved CIF file loading compatibility (see https://github.com/seto77/ReciPro/issues/19).
- **ver4.859** (2023/03/31) Fixed a minor bug on HRTEM/STEM simulation.
- **ver4.858** (2023/03/30) Fixed a minor bug on HRTEM/STEM simulation.
- **ver4.857** (2023/03/30) Improved several features on HRTEM/STEM simulation.
- **ver4.856** (2023/03/23) Fixed minor GUI bugs on HRTEM/STEM simulation.
- **ver4.855** (2023/03/23) Added a feature to save simulation conditions in HRTEM/STEM simulation.
- **ver4.854** (2023/03/11) Fixed minor GUI bugs on HRTEM/STEM simulation.
- **ver4.853** (2023/03/09) Corrected errors in formulas in STEM simulations. Added LA-CBED calculation mode.
- **ver4.852** (2023/03/04) Fixed minor GUI bugs on HRTEM/STEM simulation.
- **ver4.851** (2023/03/02) Fixed minor GUI bugs on HRTEM/STEM simulation.
- **ver4.850** (2023/03/01) Improved STEM simulation. If you find anything wrong with the STEM simulation, please report anything!
- **ver4.849** (2023/02/11) Improved: Overall speedup with SIMD calculation.

### 2022

- **ver4.848** (2022/12/28) Added the function to convert the current space group to a convertible space group. Fixed minor bugs on 'Spot ID v1'.
- **ver4.847** (2022/12/23) Added functions to save/copy images for 'Spot ID v2'.
- **ver4.845** (2022/12/20) Fixed minor bugs. Improved compatibility for reading Tiff format files.
- **ver4.843** (2022/11/29) Fixed minor bugs.
- **ver4.841** (2022/11/16) Target framework has been changed to .Net Desktop Runtime 7.0.
- **ver4.840** (2022/11/10) Fixed a bug that occurred when starting 'Diffraction Simulator' (see https://github.com/seto77/ReciPro/issues/16).
- **ver4.839** (2022/11/07) Added a function to simulate X-ray precession camera.
- **ver4.838** (2022/10/21) Improved compatibility of importing CIF files.
- **ver4.837** (2022/10/20) Added a function to output superstructure.
- **ver4.836** (2022/08/30) The compiler for C\+\+ code was changed to Clang.
- **ver4.835** (2022/08/09) Improved compatibility for reading DM3 format files.
- **ver4.834** (2022/07/08) Improved the function to generate movies.
- **ver4.833** (2022/06/24) Added the function to generate movies for 'Structure Viewer'.
- **ver4.832** (2022/06/23) Added the function to render stereonet projection with OpenGL.
- **ver4.831** (2022/05/14) Fixed minor bugs on the HRTEM function.
- **ver4.830** (2022/04/14) Some libraries are updated. Improved the HRTEM function (see https://github.com/seto77/ReciPro/issues/13).
- **ver4.829** (2022/01/04) Minor update on 'Spot ID v2' (see https://github.com/seto77/ReciPro/issues/11).

### 2021

- **ver4.828** (2021/12/15) Updated the crystal database.
- **ver4.827** (2021/12/01) Fixed a CultureInfo problem. (see https://github.com/seto77/ReciPro/issues/10)
- **ver4.826** (2021/11/18) Fixed minor bugs.
- **ver4.820** (2021/11/12) Target framework has been changed to .Net Desktop Runtime 6.0.
- **ver4.819** (2021/10/27) Improved the interface of Kikuchi line simulation. Speed up & fix bug on the dynamical diffraction simulator.
- **ver4.817** (2021/09/17) Fixed minor bugs.
- **ver4.815** (2021/09/02) Improved: User interfaces and tooltips.
- **ver4.814** (2021/08/29) Fixed minor bugs: Drawing overlapping area of CBED disks (see https://github.com/seto77/ReciPro/issues/8).
- **ver4.813** (2021/08/28) Fixed minor bugs on HRTEM simulation (see https://github.com/seto77/ReciPro/issues/9).
- **ver4.812** (2021/08/17) Changed GUI. Fixed minor bugs.
- **ver4.811** (2021/08/10) Fixed minor bugs on HRTEM simulation (see https://github.com/seto77/ReciPro/issues/7).
- **ver4.810** (2021/08/07) Fixed minor bugs.
- **ver4.809** (2021/07/16) Fixed minor bugs. Renewed AMCSD database, and improved loading speed of the database.
- **ver4.808** (2021/07/08) Fixed a minor bug about a compile option for native (c\+\+) codes.
- **ver4.807** (2021/07/06) Fixed minor bugs. Improved a rendering speed of 'Structure Viewer'.
- **ver4.806** (2021/05/25) Fixed distribution failure of language resource files.
- **ver4.802** (2021/05/24) Target framework has been changed to .Net Desktop Runtime 5.0.
- **ver4.800** (2021/05/20) Fixed bugs on native (c\+\+) codes. Changed CBED interface.
- **ver4.799** (2021/05/10) Fixed bugs on native (c\+\+) codes.
- **ver4.798** (2021/05/03) Fixed bugs on the 'Diffraction simulator'.
- **ver4.797** (2021/03/24) Fixed a bug on the 'Database' function.
- **ver4.795** (2021/03/09) Fixed a bug on the CBED calculation code.
- **ver4.794** (2021/03/08) Added new algorithm for CBED calculation (matrix exponential method)
- **ver4.793** (2021/02/26) Fixed bugs in 'Diffraction simulator'.

### 2020

- **ver4.792** (2020/12/28) Fixed a bug on 'Parallels Desktop' for Mac (OpenGL drawing problem).
- **ver4.791** (2020/11/06) Fixed a bug in Kikuchi line drawing. Improved speed of 'Structure Viewer' drawing.
- **ver4.790** (2020/11/02) Improved: GUI of 'Diffraction Simulator'.
- **ver4.789** (2020/10/26) Improved: Speed up drawing of 'Diffraction Simulator'.
- **ver4.788** (2020/10/20) Fixed a bug when calculating electron diffraction for crystals in AMCSD.
- **ver4.787** (2020/10/19) Fixed bugs in 'Powder Diffraction'.
- **ver4.786** (2020/10/10) Fixed bugs in 'Crystal Database' and improved the ’Find spots' function in 'Spot ID'.
- **ver4.785** (2020/10/06) Fixed a problem on OpenGL with Radeon Vega graphics.
- **ver4.784** (2020/10/01) Updated the manuals (both English and Japanese).
- **ver4.783** (2020/09/08) Fixed a bug on GUI.
- **ver4.782** (2020/08/19) Fixed a bug on OpenGL.
- **ver4.781** (2020/08/19) Loosen the restrictions on OpenGL requirements. (OpenGL 1.3 or higher)
- **ver4.780** (2020/08/18) Fixed a bug when exporting face-centered symmetry to CIF format.
- **ver4.779** (2020/07/08) Added a crystal database function, which manages 20698 crystals from AMCSD database. Fixed a bug on a dll file.
- **ver4.778** (2020/06/07) Fixed a bug on importing CIF file.
- **ver4.777** (2020/06/06) Improved GUI of the main window and 'structure viewer'.
- **ver4.776** (2020/05/30) Improved: Speed up rendering of 'Structure viewer'.
- **ver4.775** (2020/05/19) Improved: Rendering of text label in OpenGL windows. Fixed: Stereonet drawing.
- **ver4.774** (2020/05/15) Fixed bugs for Wyckoff position discriminator for trigonal and hexagonal symmetries.
- **ver4.773** (2020/05/12) Improved importing CIF file.
- **ver4.772** (2020/05/12) Changed: Open GL 1.5 (or higher) is required for 'Structure Viewer'.
- **ver4.771** (2020/05/10) Changed: Open GL 3.3 (or higher) is required for 'Structure Viewer'.
- **ver4.770** (2020/05/09) Improved rendering quality of 'Structure Viewer'.
- **ver4.769** (2020/05/06) Improved GUI on 'Structure Viewer'.
- **ver4.768** (2020/05/06) Improved GUI on 'Structure Viewer'.
- **ver4.767** (2020/05/05) Improved rendering speed of 'Structure Viewer' and fixed some bugs.
- **ver4.766** (2020/05/02) Improved GUIs on 'Structure Viewer' and fixed bugs on 'Spot ID'.
- **ver4.765** (2020/04/26) Improved 'Rotation geometry' and fixed 'Stereonet'.
- **ver4.764** (2020/04/12) Improved GUI, and fixed minor bugs.
- **ver4.763** (2020/03/31) Minor bugs fixed.
- **ver4.762** (2020/03/19) Minor bugs fixed.
- **ver4.761** (2020/03/14) Minor bugs fixed.
- **ver4.760** (2020/03/03) Minor bugs fixed.
- **ver4.756** (2020/03/02) Minor bugs fixed.
- **ver4.755** (2020/03/01) Changed: Distribution site is changed to GitHub.
- **ver4.747** (2020/02/29) Improved: Diffraction simulator.
- **ver4.746** (2020/02/28) Improved: Diffraction simulator.
- **ver4.745** (2020/02/16) Fixed a minor bug of Diffraction simulator.
- **ver4.744** (2020/02/05) Improved interfaces of Diffraction simulator.
- **ver4.743** (2020/02/02) Improved interfaces of Diffraction simulator.
- **ver4.742** (2020/01/06) A minor improvement on SpotID.

### 2019

- **ver4.741** (2019/12/12) Fixed a minor bug on SpotID.
- **ver4.740** (2019/12/07) Fixed a minor bug on TDS calculation.
- **ver4.739** (2019/10/24) Fixed a minor bug on 'Diffraction Simulator'.
- **ver4.733** (2019/10/17) Fixed a minor bug on 'Diffraction Simulator'.
- **ver4.731** (2019/09/24) Fixed minor bugs on HRTEM image simulation and Spot ID.
- **ver4.729** (2019/09/16) Improved interfaces of HRTEM image simulation.
- **ver4.728** (2019/09/15) Improved interfaces of HRTEM image simulation.
- **ver4.725** (2019/09/11) Improved calculation speed of HRTEM image simulation.
- **ver4.720** (2019/09/09) Improved calculation speed of HRTEM image simulation.
- **ver4.718** (2019/09/08) Fixed minor bugs on HRTEM image simulation.
- **ver4.715** (2019/09/08) Fixed minor bugs on HRTEM image simulation.
- **ver4.714** (2019/09/07) Improved calculation speed of HRTEM image simulation.
- **ver4.713** (2019/09/06) Fixed minor bugs on HRTEM image simulation.
- **ver4.711** (2019/09/04) Improved: HRTEM image simulation. Transmission cross coefficient model is added.
- **ver4.704** (2019/09/03) Improved: HRTEM image simulation. Through-focus/defocus mode is now available.
- **ver4.703** (2019/08/26) Added: HRTEM image simulation is now available. Many thanks to Dr. Ohtsuka.
- **ver4.694** (2019/08/18) Fixed minor bugs on 'Diffraction Simulator'. Changed .Net framework version to 4.8
- **ver4.693** (2019/08/06) Fixed minor bugs on 'Spot ID'
- **ver4.692** (2019/08/05) Improved function on 'Spot ID'
- **ver4.687** (2019/08/02) Improved calculation speed for the PED simulation
- **ver4.686** (2019/07/24) Fixed minor bugs in PED simulation
- **ver4.683** (2019/07/20) Added a function: In 'Diffraction Simulator', precession electron diffraction (PED) mode is now available.
- **ver4.682** (2019/07/18) Fixed a minor bug (eigen solver did not properly work).
- **ver4.681** (2019/07/08) Fixed minor bugs on 'Spot ID'
- **ver4.680** (2019/07/06) Added 'Rotation Geometry' form.
- **ver4.670** (2019/06/12) Improved functions on 'Spot ID'.
- **ver4.669** (2019/05/17) Fixed minor bugs.
- **ver4.668** (2019/04/25) Fixed minor bugs.
- **ver4.667** (2019/04/21) Fixed minor bugs.
- **ver4.664** (2019/04/19) Fixed minor bugs.
- **ver4.663** (2019/04/16) Fixed minor bugs.
- **ver4.662** (2019/04/12) Fixed minor bugs.
- **ver4.661** (2019/04/11) Fixed minor bugs.
- **ver4.660** (2019/04/10) Changed the installer. ClickOnce version will not be maintained in the future.
- **ver4.654** (2019/04/09) Improved the update function for zip version.
- **ver4.653** (2019/04/08) Fixed minor bugs.
- **ver4.652** (2019/04/04) Fixed minor bugs.
- **ver4.651** (2019/03/27) Corrected typos of Wyckoff positions and site symmetries in some space groups.
- **ver4.650** (2019/03/25) Minor bugs fixed.
- **ver4.649** (2019/03/18) Minor bugs fixed & Improved calculation speed of dynamic diffraction intensity.
- **ver4.648** (2019/03/11) Fixed minor bugs and improved a calculation speed on 'Spot ID'
- **ver4.647** (2019/03/10) Fixed minor bugs on 'Spot ID'
- **ver4.646** (2019/03/08) Fixed minor bugs on 'Spot ID'
- **ver4.645** (2019/03/07) Fixed minor bugs on 'Spot ID'
- **ver4.643** (2019/03/06) Fixed minor bugs on 'Spot ID'
- **ver4.642** (2019/03/05) Improved calculation speed of 'Spot ID'
- **ver4.641** (2019/03/04) Improved calculation speed of 'Spot ID'
- **ver4.64** (2019/03/03) Changed Visual Studio version to 2019.
- **ver4.636** (2019/03/01) Improved some functions in 'Spot ID'.
- **ver4.635** (2019/02/28) Improved some functions in 'Spot ID'.
- **ver4.634** (2019/02/27) Improved some functions in 'Spot ID'.
- **ver4.633** (2019/02/26) Improved some functions in 'Spot ID'.
- **ver4.632** (2019/02/22) Improved some functions in 'Spot ID'.
- **ver4.631** (2019/02/21) Fixed minor bugs. (copy functions in 'Structure Viewer' and 'Spot ID')
- **ver4.630** (2019/02/20) Fixed a minor bug. Changed .Net framework version to 4.7.2.
- **ver4.629** (2019/02/20) Added a function: OpenGL can be manually disabled by pressing 'CTRL' key on startup.
- **ver4.628** (2019/02/19) Fixed a bug of calculations of anisotropic Debye-Waller effects.
- **ver4.627** (2019/02/17) Minor bug fixed.
- **ver4.625** (2019/02/13) Fixed minor bugs.
- **ver4.624** (2019/02/09) Added: Check routine of OpenGL version.
- **ver4.622** (2019/02/06) Minor improvements.
- **ver4.621** (2019/02/05) Minor bug on the 'Spot ID' function fixed.
- **ver4.620** (2019/02/05) Minor bug on the 'Spot ID' function fixed.
- **ver4.619** (2019/02/03) Minor bug (in Bethe method) fixed.
- **ver4.618** (2019/01/28) Minor bug fixed.
- **ver4.617** (2019/01/26) Minor bug fixed.
- **ver4.616** (2019/01/25) Minor bug fixed.
- **ver4.615** (2019/01/22) Improved: A simulated CBED pattern can be saved as Tiff (32-bit float) format.
- **ver4.614** (2019/01/22) Improved: Detailed results of the Bethe method calculation can be displayed.
- **ver4.613** (2019/01/19) Minor improvements on dynamic compression mode.
- **ver4.612** (2019/01/11) Minor improvements on dynamic compression mode.
- **ver4.611** (2019/01/08) Fixed a minor bug on a TDS calculation.
- **ver4.61** (2019/01/05) Improved a dynamic simulation of electron diffraction. A TDS (thermal diffuse scattering) effect is now calculated properly

### 2018

- **ver4.602** (2018/12/23) Improved 'Structure viewer'.
- **ver4.601** (2018/12/20) Improved 'Structure viewer'.
- **ver4.6** (2018/12/17) Replaced OpenGL libraries. From this version, Open GL 4.3 (or higher) is required.
- **ver4.515** (2018/11/20) Modified some inconsistencies.
- **ver4.514** (2018/10/25) Minor bug fixed.
- **ver4.513** (2018/10/22) Improved calculation speed for CBED.
- **ver4.512** (2018/10/19) Added a solver library for CBED calculation.
- **ver4.511** (2018/10/18) Minor improvements to CBED calculation.
- **ver4.51** (2018/10/16) Minor improvements to CBED calculation.
- **ver4.50** (2018/10/16) Added a dynamic simulation mode (CBED pattern) by the Bethe method (beta). Many thanks to Dr. Ohtsuka & Dr. Igami!
- **ver4.42** (2018/10/11) Minor improvements.
- **ver4.41** (2018/10/05) Fixed minor bugs about the Bethe method.
- **ver4.40** (2018/09/23) Added a dynamic simulation mode (SAED pattern) by the Bethe method (beta).
- **ver4.372** (2018/09/10) Minor bug fixed.
- **ver4.371** (2018/08/27) Fixed bugs on 'Single Crystal Diffraction' form. (thx Dr.Sakamoto)
- **ver4.362** (2018/03/30) Minor improvements.
- **ver4.361** (2018/03/23) Minor improvements.
- **ver4.36** (2018/03/19) Improved: 'TEMID' is capable of selection of multiple crystals. (need Ctrl \+ Click).
- **ver4.35** (2018/03/01) Improved an algorithm of 'Diffraction Simulator'.
- **ver4.346** (2018/02/23) Minor bug fixed.
- **ver4.345** (2018/02/22) Minor bug fixed.
- **ver4.344** (2018/02/22) Minor bug fixed.
- **ver4.343** (2018/02/22) Minor bug fixed.
- **ver4.342** (2018/02/21) Added some options on 'Diffraction Simulator' to enable copying the detector area.
- **ver4.341** (2018/02/21) Fixed a minor bug.
- **ver4.34** (2018/02/20) Improved. 'Diffraction Simulator' is now capable of selection of multiple crystals. (need Ctrl \+ Click)
- **ver4.334** (2018/02/19) Fixed minor bugs.
- **ver4.333** (2018/02/19) Fixed minor bugs.
- **ver4.332** (2018/02/13) Fixed minor bugs.
- **ver4.331** (2018/02/08) Fixed minor bugs.
- **ver4.33** (2018/02/07) Improved: Rotation state is individually preserved for each crystal.
- **ver4.32** (2018/02/05) Added: The nearest zone axis can be shown in the main form.
- **ver4.317** (2018/02/03) Fixed minor bugs on 'Single crystal diffraction' form.
- **ver4.316** (2018/01/26) Fixed minor bugs on 'Single crystal diffraction' form.
- **ver4.312** (2018/01/25) Improved 'Single crystal diffraction' form.
- **ver4.311** (2018/01/20) Improved the 'Overlap picture' function on 'Single crystal diffraction' form.
- **ver4.31** (2018/01/19) Changed graphics interface for 'Single crystal diffraction' form from OpenGL to GDI\+, and then the metafile (vector object) of diffraction patterns can be exported to your clipboard. The 'Overlap picture' function is now under construction

### 2017

- **ver4.30** (2017/12/24) Changed graphics interface for 'Stereonet' form from OpenGL to GDI\+, and then the metafile (vector object) of stereonet can be exported to your clipboard.
- **ver4.29** (2017/09/01) Added 'Point Spread' mode on 'Single Crystal Diffraction'.
- **ver4.283** (2017/05/28) Fixed a small bug on 'Strain control' function.
- **ver4.282** (2017/05/26) Added 'Strain control' function.
- **ver4.281** (2017/04/26) Improved SACLA simulation on 'Single Crystal Diffraction'.

### 2016

- **ver4.280** (2016/12/31) Improved a compatibility for CIF format.
- **ver4.279** (2016/12/18) Fixed minor bugs.
- **ver4.278** (2016/05/17) Improved 'Powder Diffraction' and fixed minor bugs.
- **ver4.277** (2016/01/14) Changed .Net Framework version to 4.6.

### 2015

- **ver4.276** (2015/12/24) Fixed a minor bug on initial loading.
- **ver4.275** (2015/12/23) Fixed a minor bug on initial loading.
- **ver4.273** (2015/12/22) Fixed a minor bug on initial loading.
- **ver4.272** (2015/12/18) Fixed a minor bug on input form for rhombohedral settings.
- **ver4.271** (2015/12/11) Fixed a minor bug on Wyckoff positions
- **ver4.270** (2015/09/25) Fixed a minor bug on 'Structure Viewer'.(thx Dr. Fukui)
- **ver4.269** (2015/06/30) Added: Back Laue camera simulation.
- **ver4.268** (2015/05/13) Fixed a minor bug on reading \*.ipa files.
- **ver4.267** (2015/03/25) Fixed a minor bug on single diffraction simulation
- **ver4.266** (2015/03/18) Fixed a bug on Debye-Waller factor calculations (thx Dr. Koga)
- **ver4.265** (2015/03/13) Fixed a bug about the calculation of the Wyckoff position of P63/mmc. (thx Dr. Nagasako)
- **ver4.264** (2015/03/07) Improved 'Spot ID' function.
- **ver4.263** (2015/01/28) Improved 'Spot ID' function.
- **ver4.262** (2015/01/26) Updated help files.
- **ver4.261** (2015/01/24) Improved: a support of DM4 file on 'Spot ID'.
- **ver4.26** (2015/01/23) Added a new function, 'Spot ID', where diffraction spots could be semi-automatically identified.

### 2014

- **ver4.252** (2014/11/11) Fixed: minor bugs.
- **ver4.251** (2014/11/10) Fixed: minor bugs.
- **ver4.25** (2014/11/06) Added: SACLA EH5 optics for single crystal diffraction mode.
- **ver4.242** (2014/10/27) Fixed a bug on scattering factor information.
- **ver4.241** (2014/10/21) Fixed minor bugs on OpenGL calculations.
- **ver4.24** (2014/07/14) Improved 'Powder Diffraction'. (but not all functions are implemented yet)

### 2013

- **ver4.234** (2013/12/17) Improved language option
- **ver4.233** (2013/10/28) Improved appearance for >100% DPI scaling
- **ver4.232** (2013/10/15) Improved appearance for >100% DPI scaling
- **ver4.231** (2013/08/10) Fixed minor bugs on OpenGL.
- **ver4.23** (2013/03/28) Improved structure viewer.
- **ver4.221** (2013/02/26) Changed address of help page.
- **ver4.22** (2013/02/25) Added: Update check function
- **ver4.21** (2013/02/21) Added: CIF file export function

### 2012

- **ver4.202** (2012/12/20) Fixed a small bug.
- **ver4.201** (2012/12/19) Fixed a small bug.
- **ver4.20** (2012/12/17) Fixed OpenGL library.
- **ver4.191** (2012/12/05) Fixed minor bugs.
- **ver4.19** (2012/08/11) Improved: appearance in TEMID window.
- **ver4.184** (2012/06/22) Added: 'Reset registry keys' function was added in the 'Option' menu
- **ver4.183** (2012/06/03) Bug Fix
- **ver4.182** (2012/06/01) Bug Fix
- **ver4.181** (2012/05/31) Bug Fix
- **ver4.18** (2012/05/23) Improved: speed up of calculation of Debye ring simulation.

### 2011

- **ver4.17** (2011/12/28) Improved: Space groups A1, B1, C1, and F1 were added.
- **ver4.161** (2011/12/05) Fixed: a small bug on Debye ring simulation was fixed.
- **ver4.16** (2011/12/04) Improved: speed up of calculation of Debye ring simulation.
- **ver4.15** (2011/11/24) Improved: Stricter polycrystalline diffraction pattern can be calculated considering beam convergence and monochromaticity.
- **ver4.142** (2011/11/20) Improved: Stereonet simulator can draw vectors of specified indices selected by users.
- **ver4.141** (2011/11/07) Fixed: PolycrystallineDiffractionSimulation
- **ver4.14** (2011/11/01) Fixed the critical mistake on polycrystalline diffraction simulation: Intensity calculation was corrected.
- **ver4.131** (2011/11/01) Fixed: Y axis direction on polycrystalline diffraction simulation form was corrected.
- **ver4.13** (2011/10/31) Improved: Polycrystalline diffraction simulation; Fixed: File->Close function.
- **ver4.12** (2011/10/31) Improved: Polycrystalline diffraction simulation
- **ver4.113** (2011/10/30) Fixed a bug: projection buttons on main form in Japanese mode
- **ver4.112** (2011/10/21) Fixed a bug when sending crystal data.
- **ver4.111** (2011/10/17) Fixed problems on Single Crystal Diffraction form.
- **ver4.11** (2011/10/12) Fixed problems on import CIF format.
- **ver4.10** (2011/10/12) Added language option. English and Japanese are available.
- **ver4.00** (2011/07/19) 同位体組成の入出力と中性子線回折の強度計算に対応しました。
- **ver3.922** (2011/07/05) CrystalInformationがはみ出していたバグを修正
- **ver3.921** (2011/07/05) 昨日の変更を微修正。空間群情報(Symmetry info.)と構造因子(Scattering factor)を分けて表示するようにしました。
- **ver3.92** (2011/07/04) メインツールバーに「Detailed Information」を付けました。空間群の情報や、構造因子を表示できます。
- **ver3.91** (2011/05/10) TEMIDで、等価な軸の判定ミスがありました。修正。
- **ver3.90** (2011/04/21) DiffractionSimulator周りを改良。なかなか完成とまではいきませんが、とりあえず。
- **ver3.811** (2011/02/29) DiffractionSimulator周りを改良(中)。まだ途中ですが、要望があったので、とりあえず公開

### 2010

- **ver3.81** (2010/11/18) ヘルプページのリンク先を変更。内容は鋭意作成中です。
- **ver3.80** (2010/11/08) 初回起動時にバックグラウンドでネイティブコードを生成するように変更。二回目以降の起動が早くなります。
- **ver3.701** (2010/11/08) 三斜晶系の対称性のコーディングミスを修正
- **ver3.70** (2010/11/07) 起動を高速化。多分数倍は速くなったと思います。
- **ver3.62** (2010/07/21) StereoNet投影でSchmidtネット(等積投影)に対応しました。
- **ver3.61** (2010/05/09) 開発環境をVS2010にしました。
- **ver3.60** (2010/01/07) 結晶がランダムに配向したときのデバイリングパターンを表示できるようにしました。

### 2009

- **ver3.59** (2009/12/24) 原子位置の計算に一部ミスがありました（特に複合格子の対称性）ので修正
- **ver3.58** (2009/10/20) アプリ間の結晶データ送信が正常に行えなかったバグを修正
- **ver3.57** (2009/09/26) Diffraction Simulatorでプリセッションカメラ(ZOLZ)を表示できるようにしました。&& X線の強度計算に対応
- **ver3.56** (2009/09/24) Electron Diffractionで画像をオーバーラップして表示できるようにしました。
- **ver3.55** (2009/09/03) 64bit OSに対応しました。
- **ver3.54** (2009/06/01) ステレオネット描画の部分で色を変更できないバグを修正
- **ver3.53** (2009/03/11) 回折スポットの励起誤差、結晶構造因子を表示できるようにしました。表示が込み合ってしまうので、選択表示できるように考え中です。
- **ver3.52** (2009/03/10) CIFファイルの読み込みバグを修正

### 2008

- **ver3.51** (2008/10/30) 'Apply to same elements'の機能にバグがあったので修正しました。
- **ver3.50** (2008/08/31) 画像保存にバグがありましたので修正しました。
- **ver3.49** (2008/08/27) StructureViewerで、凡例や結晶軸の画像も保存できるようにしました。
- **ver3.48** (2008/08/27) Irregularな空間群A-1,B-1,C-1,I-1,F-1に対応しました。& StructureViewerの画像が保存できなかったのを修正。
- **ver3.47** (2008/08/26) StructureViewerで背景色、文字色を変更できるようにしました。& 前回終了時の色を読み込むようにしました。
- **ver3.46** (2008/08/20) CIFファイルの読み込み不具合を修正しました。
- **ver3.45** (2008/07/10) 大円描画機能を追加（というか復活)
- **ver3.44** (2008/06/20) 作者異動に伴いメールアドレスなど変更
- **ver3.43** (2008/04/29) 初期結晶ファイル中のSiO2 (CaCl2構造)の格子定数が間違っていたのを修正しました。
- **ver3.42** (2008/04/22) 読み込む/書き込む結晶を選択することができるようにしました。
- **ver3.41** (2008/04/13) Smapのoutデータが読めなくなっていたバグを修正
- **ver3.40** (2008/02/28) Structure Viewerで原子の配位状況を表示するようにしました。
- **ver3.39** (2008/02/24) 電子線回折強度の計算速度を若干高速化
- **ver3.38** (2008/02/23) 電子線回折強度の運動学的理論計算に対応しました。計算速度はこれから向上させていきます。
- **ver3.37** (2008/02/12) 結晶ファイルのドラッグドロップ対応&外部連携強化&デザイン変更
- **ver3.36** (2008/02/07) バグ修正&デザイン変更
- **ver3.35** (2008/01/29) ワイコフ位置の計算部分のバグ修正(いつまで見つかるやら…)。
- **ver3.34** (2008/01/28) ワイコフ位置の計算部分のバグ修正。
- **ver3.33** (2008/01/28) FormElectron(電子回折)の初期表示時に解像度設定がおかしくなってしまうのを修正 (永田さん、ありがとうございます)
- **ver3.32** (2008/01/25) 起動時にヒントをだせるようにしました。
- **ver3.31** (2008/01/21) 配布元を変更しました
- **ver3.30** (2008/01/21) TEMIDの結果をダブルクリックすると回転角に反映する機能を追加
- **ver3.29** (2008/01/18) ボタンイメージを入れてみました。
- **ver3.28** (2008/01/16) 一部デザインがおかしかったのを変更
- **ver3.27** (2008/01/14) デザインを変更
- **ver3.26** (2008/01/10) 原子が一個だった時、凡例がうまく表示できなかったバグを修正
- **ver3.26** (2008/01/08) フォームの誤動作を修正
- **ver3.25** (2008/01/07) 内部形式を変更 \+ 格子定数、原子位置の誤差に対応

### 2007

- **ver3.24** (2007/12/26) StructureViewerで原子選択後右クリックで、原子の配位環境を表示できるようにした (ほかのところにも拡張予定)
- **ver3.23** (2007/12/26) ElectronDiffractionで面間隔、逆格子原点からの距離を表示できるようにした
- **ver3.22** (2007/12/21) StructureViewerで原子の凡例が表示できるようにしました。
- **ver3.21** (2007/11/12) StructureViewerでBondの一部が表示されないことがあったバグを修正
- **ver3.20** (2007/11/12) StructureViewerにアニメーション(自動回転)機能追加。
- **ver3.19** (2007/11/09) メインウィンドウに結晶軸の方向を表示するようにしました。
- **ver3.18** (2007/11/07) 印刷機能を付けました。
- **ver3.17** (2007/11/07) StructureViewerで単位格子の稜が一本表示されていなかったバグを修正。ToolTipを充実。
- **ver3.16** (2007/11/03) Stereonet, ElectronDiffractionで画像を保存、コピー機能追加 & 。ElectronDiffractionで色を変更できないバグを修正
- **ver3.15** (2007/10/27) 共通フォーム&コントロールの部分を分離しDLL化した
- **ver3.14** (2007/10/26) カメラ長が変更できなかったバグを修正
- **ver3.13** (2007/09/26) SMAP(http://www.sci.hokudai.ac.jp/~hiro/)の構造解析データを直接読み込めるようにしました。
- **ver3.12** (2007/08/21) Structure ViewerのLattice Plane表示がおかしいバグを修正 && 全体的に速度向上
- **ver3.11** (2007/08/21) 突如落ちるなどのバグをさらにさらに改善。今度こそ？
- **ver3.10** (2007/08/15) 下記のバグをさらに改善。文字描画が難しい・・・
- **ver3.09** (2007/08/14) StereoNet, ElectronDiffractionがたまに止まってしまうバグを改善
- **ver3.08** (2007/08/08) OpenGL関係でバグ修正
- **ver3.07** (2007/08/07) StereoNet,ElectronDiffractionをOpenGL描画に変更しました。速くなりましたがまだバグがあるかも・・・
- **ver3.06** (2007/07/05) 結晶軸計算のバグを修正
- **ver3.05** (2007/07/05) TEMID部分の機能を追加(対称性チェック、複数パターンからの抽出など)
- **ver3.04** (2007/06/22) ホームページアドレスを変更
- **ver3.03** (2007/06/22) 選択している対称性に関する情報を表示できるようにしました。
- **ver3.02** (2007/06/21) 結晶データをPDIndexerと同一形式のxmlファイルでリスト化して読み込めるようにしました。
- **ver3.01** (2007/06/10) Electron diffraction, TEMIDフォームを追加
- **ver3.00** (2007/05/30) ベータ版作成。ver2.40から大幅に作り直す。未だ道の途中

### 2004

- **ver2.40** (2004/05/13) StereoNetに大円描画機能追加&&StereoNet,Geometricsにワイコフ位置情報を追加

### 2003

- **ver2.31** (2003/11/12) バグフィックス&&StereoNet, Diffractionの改良
- **ver2.30** (2003/11/12) データベース機能を追加&&設定ファイルの形式変更&&StereoNet, Diffraction の画像出力機能追加
- **ver2.20** (2003/10/28) 菊池線表示機能追加&&一部デザイン変更
- **ver2.12** (2003/10/23) バグフィックス
- **ver2.11** (2003/10/13) 一部のデザインを変更&&描画部の高速化とバグフィックス
- **ver2.10** (2003/10/04) TemIDのデザイン(パターン入力部)を改良&&Helpファイルを充実
- **ver2.00** (2003/09/28) 開発環境を「.Net Framework」に変更&&画像解析機能追加&&TemID解析結果をステレオネット、逆格子空間に反映する機能を追加

### 2002

- **ver1.05** (2002/05/01) Tilt,Azimuth,Rotationの説明ダイアログ追加&&Ewald球の半径の調節スライドバーを追加&&逆格子点表示をスピードアップ&&バグフィックス
- **ver1.04** (2002/04/22) 逆空間を表示する機能を追加&&バグフィックス
- **ver1.03** (2002/03/30) ステレオネットの拡大縮小機能を追加&&空間群の間違いを訂正&&バグフィックス
- **ver1.02** (2002/03/28) ステレオネットの機能を追加&&バグフィックス
- **ver1.01** (2002/03/14) 空間群の間違いを訂正&PHOTO間のリンクボタン追加&3点の回折斑点から解析するモード追加
- **ver1.00** (2002/03/03) 暫定動作バージョンを作成
