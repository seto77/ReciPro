# ReciPro

[![Documentation](https://img.shields.io/badge/%F0%9F%93%96_Documentation-blue)](https://seto77.github.io/ReciPro/ja/)
[![Latest Release](https://img.shields.io/github/v/release/seto77/ReciPro?logo=github)](https://github.com/seto77/ReciPro/releases/latest)
[![Total downloads](https://img.shields.io/github/downloads/seto77/ReciPro/total?logo=github&label=GitHub%20downloads)](https://github.com/seto77/ReciPro/releases)
[![GitHub Stars](https://img.shields.io/github/stars/seto77/ReciPro?style=social)](https://github.com/seto77/ReciPro/stargazers)
[![GitHub Forks](https://img.shields.io/github/forks/seto77/ReciPro?style=social)](https://github.com/seto77/ReciPro/forks)
[![License: MIT](https://img.shields.io/badge/License-MIT-green)](https://github.com/seto77/ReciPro/blob/master/LICENSE.md)

<!-- 260804Cl: ../../README.md (英語) の翻訳版。英語版を更新したら本ファイルも追随させること。 -->
[English](../../README.md) | **日本語** | [Deutsch](README.de.md) | [Français](README.fr.md) | [Español](README.es.md) | [Italiano](README.it.md) | [Русский](README.ru.md) | [简体中文](README.zh-Hans.md) | [繁體中文](README.zh-Hant.md) | [한국어](README.ko.md) | [Português](README.pt.md)

*ReciPro* は、結晶データベースの検索、結晶構造やゴニオメータ設定の可視化、回折図形や高分解能顕微鏡像のシミュレーション、回折データの解析といった機能に一貫してアクセスできる、無料・オープンソースのGUIベース汎用結晶学ソフトウェアです。これらの機能は使いやすいGUIで互いに連携しており、計算結果はほぼリアルタイムに同期表示されます。*ReciPro* は、X線・電子線・中性子線を用いる結晶学やTEMに携わる幅広い結晶学者（初心者を含む）の助けとなります。

*ReciPro* は2002年から継続的に開発されており、2020年3月からGitHubで公開されています。GitHubからのダウンロード数は27,000回を超え、大学や企業の十数以上の研究室で数百人のユーザーに利用されています。

***[使い方はマニュアルをご覧ください！](https://seto77.github.io/ReciPro/ja/)***

[リアルタイムで実行される各種シミュレーション（試料: MgAl2O4）](https://github.com/user-attachments/assets/6b0234dd-f2d6-49db-b146-bb74cf6021b6)

## 著者

*ReciPro* は [瀬戸雄介](https://yseto.net/en/home-e) と [大塚将志](https://researchmap.jp/7000002999) により開発されています。機能とアルゴリズムは [論文](https://github.com/seto77/ReciPro/blob/master/docs/ReciProSetoOhtsuka2022.pdf) で紹介されています。

## 引用

学術的な成果で *ReciPro* を使用した場合は、GitHubリポジトリのページに表示される **Cite this repository** リンクをご利用ください。引用メタデータは `CITATION.cff` で提供されており、推奨される引用文献は以下の論文です。

  * [Seto, Y. & Ohtsuka, M. (2022). *J. Appl. Cryst.* **55**, 397-410, doi: 10.1107/S1600576722000139.](https://doi.org/10.1107/S1600576722000139)

必要に応じて、ソフトウェアのリポジトリ自体を引用することもできます。

  * リポジトリ: https://github.com/seto77/ReciPro
  * リリース: https://github.com/seto77/ReciPro/releases/latest

***

## インストール

* [*ReciPro-setup.msi*](https://github.com/seto77/ReciPro/releases/latest/download/ReciPro-setup.msi)（最新版への直接リンク）をダウンロードして実行してください。[リリースページ](https://github.com/seto77/ReciPro/releases/latest) からも入手できます。（v.4.939までは *ReciProSetup.msi* という名前でした。）
* *ReciPro* は ***.Net Desktop Runtime 10.0***（***.Net Runtime 10.0*** ではありません）がインストールされたWindows OSで動作します。ランタイムは [こちら](https://dotnet.microsoft.com/download/dotnet/10.0) から入手できます。
* 権限が制限されたPCなどでインストーラを実行できない場合は、**ポータブル版ZIP** パッケージ（*ReciPro-v.X.XXX.zip*）もリリースページで配布しています。自己完結型で、インストールも.NETランタイムも不要です。解凍して実行するだけで使えます。
* *ReciPro* は **MITライセンス** で配布されています（誰でも自由に使用・改変・再配布できます）。
* コード署名の状況とインストーラの検証方法については [コード署名ポリシー](../../CODE_SIGNING.md) をご覧ください。
* 同梱または参照しているサードパーティ製コンポーネント・データについては [サードパーティ通知](../../THIRD-PARTY-NOTICES.md) をご覧ください。

### macOS（非公式）

* *ReciPro* が公式にサポートするのはWindowsのみですが、**ポータブル版ZIP** パッケージと **Sikarugir** Wineラッパー、**Mesa3D** OpenGLドライバを組み合わせることで、macOS（Apple Silicon）でも動作したという報告があります。Windowsライセンスも仮想マシンも不要です。
* 福島良（JAMSTEC）氏が公開している手順書をご覧ください: https://github.com/Ryo-fkushima/ReciPro_macOS_memo
* この構成は公式にサポートされておらず、動作も十分に検証されていません。既知の制限として、一部の記号（Å、上付き文字、矢印）が正しく表示されないことがあります。
* 文字化けは、グリフの収録範囲が広いフォント（**DejaVu Sans/Serif**、日本語UIでは **Noto Sans CJK JP**）をWineプレフィックスにインストールすることで解消できます。ReciProはWine環境を検出して自動的にそれらのフォントへ切り替えます。詳しくは [トラブルシューティング](https://seto77.github.io/ReciPro/ja/troubleshooting/) をご覧ください。

### Windowsのセキュリティ警告について

* *ReciPro* は必ず公式のGitHub Releasesページからダウンロードしてください: https://github.com/seto77/ReciPro/releases/latest
* 一部のWindows環境では、インストーラの実行前に Microsoft Defender SmartScreen や Smart App Control が警告を表示することがあります。これはビルドされて間もない、あるいは配布範囲の限られた研究用ソフトウェアで起こりうることであり、警告自体はインストーラが悪意あるものであることを意味するわけではありません。
* ダウンロードしたインストーラをご自身で検証したい場合は、VirusTotalなどの複数エンジンによるスキャンサービスをご利用ください。

## コード署名ポリシー

[<img src="https://signpath.org/assets/favicon-50x50.png" alt="SignPath" height="20">](https://about.signpath.io/) Windows向けの無償のコード署名は [SignPath.io](https://about.signpath.io/) により、証明書は [SignPath Foundation](https://signpath.org/) により提供されています。

v.4.942以降、リリース成果物（*ReciPro-setup.msi* インストーラとポータブル版 *ReciPro.exe*）は、自動リリースパイプラインの一部としてWindows Authenticodeで署名されています。各署名リクエストは公開前にメンテナが確認し、手動で承認しています。署名の範囲、インストーラの検証方法、不審な成果物の報告方法を含む完全なポリシーは [CODE_SIGNING.md](../../CODE_SIGNING.md) をご覧ください。

## プライバシー

*ReciPro* はローカルで動作するデスクトップアプリケーションです。個人情報や利用状況の**収集・保存・送信は一切行いません**。テレメトリや解析機能も含まれていません。インストール後は完全にオフラインで動作します。

*ReciPro* が行うネットワーク接続は、ユーザーが自ら開始する任意のダウンロードのみであり、いずれもユーザーのデータをアップロードすることはありません。

* **更新の確認**（メニューコマンド）: インストール済みのバージョンと最新のGitHubリリースを比較し、ユーザーが選択した場合に公式の [GitHub Releases](https://github.com/seto77/ReciPro/releases/latest) ページから新しいインストーラをダウンロードします。
* **CODデータベース**（Crystallography Open Database）: 初回使用時に著者のGitHubミラーからダウンロード（約880 MB）し、以降はオフラインで使用します。
* **Intel MKLライブラリ**（任意の高速化）: *Use MKL* オプションを有効にした場合のみ [nuget.org](https://www.nuget.org/) からダウンロード（約55 MB）し、動力学的回折計算を高速化します。

同梱のAMCSDデータベースとすべての中核機能は、完全にオフラインで動作します。

## マニュアル
  * オンラインマニュアル（英語 / 日本語）: https://seto77.github.io/ReciPro/ja/
  * 日本語版サイト: https://yseto.net/soft/recipro
***

## 主な機能

### 結晶データベース

* **AMCSD**（American Mineralogist Crystal Structure Database）: 21,000件を超える結晶構造を内蔵しており、インストール直後から利用できます。
  * データベースは高度に圧縮されており（約5 MB）、インストールファイルに含まれているため、オフライン環境でも利用できます。
  * 名称、化学組成、格子定数、密度、対称性、含まれる元素で結晶を検索できます。
  * 文献: [Downs & Hall-Wallace, 2003, *American Mineralogist* **88**, 247-250](https://www.geo.arizona.edu/xtal/group/pdf/am88_247.pdf)
* **COD**（Crystallography Open Database）: 有機結晶を含む約525,000件の結晶構造も利用できます。
  * 初回使用時に自動的にダウンロードされ（約880 MB）、以降はオフラインで利用できます。
  * 文献: [Gražulis et al., 2009, *J. Appl. Cryst.* **42**, 726-729](https://doi.org/10.1107/S0021889809016690); [Gražulis et al., 2012, *Nucleic Acids Res.* **40**, D420-D427](https://doi.org/10.1093/nar/gkr900)
* CIF形式・AMC形式ファイルの読み込みと書き出しに対応。

### 結晶学的計算

* 530通りの空間群表記に対応: 230の標準ITA設定 + 300の非標準軸設定。
  * 全空間群の一般条件（消滅則）、ワイコフ位置、多重度。
  * 面や軸どうしの周期性・角度の幾何学的計算。
  * 等価な原子位置の生成。
  * 非標準軸設定（例: *Pbnm* から *Pnma*）や原点移動の簡単な変換。

### 原子の性質

* <sup>1</sup>H から <sup>98</sup>Cf までの特性X線の波長・エネルギー。
* X線・電子線・中性子線に対する原子散乱因子。

### 結晶構造ビューア

* OpenGL（GLSL）アーキテクチャによる3次元の結晶構造可視化。
  * 原子、結合、配位多面体、単位胞、格子面、境界面、凡例ラベルを描画します。
  * 数万個の原子を含む複雑な結晶構造でも、リアルタイムに滑らかに描画できます。
  * 既定の原子の描画色とサイズはVESTAと互換性があります。
  * 描画範囲は単位胞の倍数、または結晶面の指数と中心からの距離で指定できます。
  * 境界面に着色することで、任意の結晶晶癖を表現できます。
  * 任意の格子面を表示できるため、初心者が回折現象における格子面の概念を理解する助けになります。
  * 回転・移動・拡大縮小はマウス操作で自由に制御できます。
  * 原子をクリックすると、隣接する原子との距離と結合角が表示されます。
  * 回転状態は他の機能ウィンドウ（ステレオ投影、回折シミュレータなど）に即座に反映されます。
  * 内蔵の動画エンコーダ（Windows Media Foundation）により、発表用の回転アニメーション動画（H.264/H.265 MP4）を生成できます。

### ステレオ投影

* 結晶面と晶帯軸をステレオ投影図にプロットします。
  * 等角投影（ウルフネット）と等積投影（シュミットネット）の両方に対応し、緯線・経線も表示できます。
  * 指数は数値範囲または特定の値で指定できます。
  * 晶帯軸を指定して大円を表示できます。
  * 描画したオブジェクトはベクター形式で保存・コピーでき、解像度を損なわずに後から編集できます。
  * 教育用途に向けた、ステレオ投影の幾何学的関係の3次元可視化。

### 回折シミュレータ

* X線・電子線・中性子線を線源とする単結晶の回折図形をシミュレーションします。
  * 入射ビームの運動エネルギーを自由に設定できます。
  * <sup>1</sup>H から <sup>98</sup>Cf までの特性X線エネルギーを内蔵しています。
  * 描画範囲は画像の分解能（ピクセルサイズ）とカメラ長で指定します。
  * 検出器を傾けた配置にも対応しています。
  * 実験で取得した画像の重ね合わせにも対応しています。
  * 結晶の回転（回折条件）を制御でき、他のウィンドウと即座に同期します。

* **多結晶回折**: 多結晶試料を仮定したデバイリングのパターンシミュレーション。
* **歳差カメラ**（X線）: 0次ラウエゾーンの歳差カメラパターンのシミュレーション。
* **背面反射ラウエカメラ**（X線）: 背面反射ラウエパターンのシミュレーション。

#### 運動学的回折理論
* すべての線源（X線・電子線・中性子線）で利用できます。
* 回折強度は結晶構造因子の振幅の2乗と励起誤差から見積もられます。
* デバイ・ワラー因子が回折強度に及ぼす効果も取り入れられています。

#### 動力学的回折理論（電子線）
* **ブロッホ波法**（Bethe, 1928）に基づいており、低次晶帯軸に制約されず柔軟な結晶方位を扱えます。
* 2種類の計算手法が利用できます。
  * **ベーテ固有値法**: ブロッホ固有状態の固有値・固有ベクトルを行列対角化により求めます。試料厚さを変化させる場合に適しています。
  * **散乱行列法**: パデ近似を用いたスケーリング・スクエアリング法により行列指数関数を直接計算します。単一の厚さを高速に計算する場合に適しています。
* 最速のアルゴリズムと最適な数学ライブラリ（Eigen、Intel MKL、Math.NET）が自動的に選択されます。
* 熱散漫散乱（TDS）の吸収ポテンシャルは、高速化のため解析的に計算されます。

* **SAED**（制限視野電子回折）: 動力学的散乱効果を含む平行ビーム電子回折のシミュレーション。
* **PED**（歳差電子回折）: 歳差角と方位角分解能を指定してPEDパターンをシミュレーションします。結晶構造解析や、準運動学的なPED条件の最適化に有用です。
* **CBED**（収束電子回折）: 収束半角と分割数を指定してCBEDパターンをシミュレーションします。試料厚さの決定に向けて、厚さを変えた一括シミュレーションにも対応しています。
  * 位置平均CBED（PACBED）パターン。
  * 大角度CBED（LA-CBED）シミュレーション。

### HRTEMシミュレータ

* 同じブロッホ波の理論的枠組みによる高分解能透過型電子顕微鏡像のシミュレーション。
* 光学パラメータ（加速電圧、球面収差係数、デフォーカス値、試料厚さなど）はGUIから設定します。
* 代表的なTEM光学パラメータのプリセットを内蔵しており、右クリックで呼び出せます。
* 部分的可干渉性に対する2つの結像モデル:
  * **線形コントラスト伝達理論**: 計算コストが低く、弱位相物体近似が成り立つ薄い試料に適しています。
  * **非線形コントラスト伝達理論（TCCモデル）**: 1次の透過交差係数（Ishizuka, 1980）に基づいており、より厚い試料や重い元素を含む物質でも信頼できます。
* 包絡関数を含むコントラスト伝達関数をプロットできます。
* 厚さ・デフォーカスのシリーズ像を同時に計算できます。
* 標準的な条件では通常1秒以内に計算が完了します。

### STEMシミュレータ

* 走査透過型電子顕微鏡像のシミュレーション。
  * 明視野（BF）、環状暗視野（ADF）、高角度環状暗視野（HAADF）の各結像モード。
  * 収束ビームは多数の平面波の重ね合わせとして扱われ、重なりが正確に計算されます。
  * 非弾性散乱電子は吸収ポテンシャルモデルを用いて計算されます。
  * 厚さ・デフォーカスのシリーズ像を生成できます。

### Spot ID

* 実測のSAEDパターンに対する半自動の回折スポット指数付け。
* **Spot ID v1**: 回折スポットの幾何学的配置（距離と角度）から晶帯軸を探索します。2〜3枚の画像の同時解析に対応しています。
* **Spot ID v2**: SAEDパターン画像を直接読み込みます。
  * 標準的な画像形式に対応: TIFF (.tif)、Digital Micrograph 3/4 (.dm3, .dm4) ほか。
  * 回折スポットを自動検出し、2次元擬フォークト関数でフィッティングします。
  * 逆格子ベクトルの配置に一致する結晶方位を網羅的に探索します。
  * 高次の晶帯軸でも正確に決定できます。

### 回転ジオメトリ（ゴニオメータ）

* ReciPro内のオイラー角を、実験室のゴニオメータと対応付けます。
* 目的の結晶方位（低次晶帯軸など）を得るために、ゴニオメータをどのように回転させればよいかを示します。
* 任意のゴニオメータ定義に対応しています。

### マクロ

* Python構文のマクロスクリプトによる作業の自動化。
  * 例: 結晶を1°刻みで回転させ、各ステップで回折図形やSTEM像を保存する。
  * ReciPro固有の関数は "ReciPro" 名前空間で利用できます。
  * 使用例は [マニュアル](https://seto77.github.io/ReciPro/ja/20-macro/2-examples/) にあります。

### その他の機能

* **電子線飛程シミュレータ**: 物質中の電子線の飛程のモンテカルロシミュレーション。
* **EBSD**（電子線後方散乱回折）: 開発中。

## 技術的な詳細

* **C++**、**C#**、**OpenGL Shading Language (GLSL)** で記述されています。
* マルチスレッド並列化により、最新のメニーコアCPUで高性能な計算を行います。
* 結晶方位が変化すると、すべての機能ウィンドウがリアルタイムに同期して更新されます。
* 右手系の直交座標系（X: 右、Y: 上、Z: 手前）と Z–X–Z のオイラー角規約を使用しています。
* 座標系の定義は Thermo Fisher Scientific 社のEBSDソフトウェアと互換性があります。

### 学術的なインパクト

* **査読付きソフトウェア論文:** [Seto, Y. & Ohtsuka, M. (2022), *Journal of Applied Crystallography*, **55**, 397-410](https://doi.org/10.1107/S1600576722000139).
* **引用論文:** [Google Scholar の引用文献](https://scholar.google.jp/scholar?cites=12625594477623342627).
* **論文への注目度:** [Altmetric の詳細](https://www.altmetric.com/details/123778746).

| 指標 | 主な数値 |
| --- | --- |
| GitHubの総ダウンロード数 | 27,000回以上 |
| Google Scholar の被引用数 | 170件以上 |
| Dimensions の被引用数 | 160件以上 |
| Mendeley の読者数 | 90人以上 |

## スクリーンショット

<img src="https://seto77.github.io/ReciPro/assets/cap-ja-auto/FormMain.png" height="320px" alt="メインウィンドウ">
<img src="https://seto77.github.io/ReciPro/assets/cap-ja-auto/FormCrystalDatabase.png" height="320px" alt="結晶データベース">
<img src="https://seto77.github.io/ReciPro/assets/cap-ja-auto/FormSymmetryInformation.png" height="320px" alt="対称性情報">
<img src="https://seto77.github.io/ReciPro/assets/cap-ja-auto/FormBeamInteraction.png" height="320px" alt="ビーム相互作用">
<img src="https://seto77.github.io/ReciPro/assets/cap-ja-auto/FormStructureViewer.png" height="320px" alt="結晶構造ビューア">
<img src="https://seto77.github.io/ReciPro/assets/cap-ja-auto/FormStereonet.png" height="320px" alt="ステレオ投影">
<img src="https://seto77.github.io/ReciPro/assets/cap-ja-auto/FormDiffractionSimulator.png" height="320px" alt="回折シミュレータ">
<img src="https://seto77.github.io/ReciPro/assets/cap-ja-auto/FormImageSimulator.png" height="320px" alt="HRTEM/STEMシミュレータ">
<img src="https://seto77.github.io/ReciPro/assets/cap-ja-auto/FormSpotIDV2.png" height="320px" alt="Spot ID v2">
<img src="https://seto77.github.io/ReciPro/assets/cap-ja-auto/FormMacro.png" height="320px" alt="マクロ">
<img src="https://seto77.github.io/ReciPro/assets/cap-ja-auto/FormTrajectory.png" height="320px" alt="電子線飛程シミュレータ">

***
