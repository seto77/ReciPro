# ReciPro マニュアル

<!-- 260623Ch: Shared demo movie for all localized top pages. -->
<div class="rp-demo-video" markdown="0">
  <video controls autoplay muted playsinline preload="metadata" aria-label="ReciPro demo movie">
    <source src="../assets/Recipro_Demo.mp4" type="video/mp4">
  </video>
</div>

## 概要
* ReciProは、MITライセンスで配布されている無料のソフトウェアです。様々な結晶学的計算や電子顕微鏡シミュレーション機能を提供します。
* 2020年3月のGitHub公開以来、累計27,000回以上ダウンロードされ、多くの結晶学者・電子顕微鏡研究者に利用されています。

## 目的別に探す

| 目的 | 最初に読むページ | 次に使う主な機能 |
|------|------------------|------------------|
| 結晶を読み込んで方位を合わせたい | [メインウィンドウ](0-main-window.md) | [回転ジオメトリ](4-rotation-geometry.md)、[Appendix A1. 座標系](appendix/a1-coordinate-system/1-orientation.md) |
| 結晶構造を3Dで確認したい | [結晶構造ビューア](5-structure-viewer.md) | [対称性情報](2-symmetry-information.md) |
| SAED / XRD / PED / CBED を計算したい | [回折シミュレータ](7-diffraction-simulator/index.md) | [SAED](7-diffraction-simulator/1-saed-simulation.md)、[X線回折](7-diffraction-simulator/4-x-ray-neutron-diffraction.md)、[PED](7-diffraction-simulator/2-ped-simulation.md)、[CBED](7-diffraction-simulator/3-cbed-simulation.md) |
| HRTEM / STEM 像を計算したい | [HRTEM/STEMシミュレータ](9-hrtem-stem-simulator/index.md) | [HRTEM](9-hrtem-stem-simulator/1-hrtem-simulation.md)、[STEM](9-hrtem-stem-simulator/2-stem-simulation.md) |
| EBSDパターンを計算したい | [EBSDシミュレーション](12-ebsd-simulation.md) | [電子飛程](8-electron-trajectory.md)、[Appendix A3. EBSD の計算](appendix/a3-bloch-wave/ebsd.md) |
| 実験回折像を指数付けしたい | [Spot ID v1](10-spot-id.md)、[Spot ID v2](11-spot-id-v2.md) | [回折シミュレータ](7-diffraction-simulator/index.md) |
| 動力学計算の式を確認したい | [Appendix A3. Bloch波法](appendix/a3-bloch-wave/index.md) | [動力学計算](appendix/a3-bloch-wave/calculation.md)、[CBED](appendix/a3-bloch-wave/cbed.md)、[STEM](appendix/a3-bloch-wave/stem.md)、[EBSD](appendix/a3-bloch-wave/ebsd.md) |

## 主な機能
* **Full GUI** : すべての操作はグラフィカルインターフェースで行います。ファイルの入出力はドラッグ＆ドロップに対応。
* **結晶リスト** : 複数の結晶をまとめて管理。結晶ごとにウィンドウを開く必要はありません。
* **空間群データベース** : International Tables Volume Aの230空間群＋530個のHall symbolを内蔵。対称要素、ワイコフ位置、消滅則を含みます。対称要素と一般位置を *International Tables* Vol. A 様式の模式図として描画できます（[対称性情報](2-symmetry-information.md) 参照）。
* **原子情報** : H (1) 〜 Cf (98) の元素について、散乱因子（X線・電子線・中性子）、特性X線エネルギー、同位体比などを内蔵。
* **フレキシブルな結晶回転** : 晶帯軸・結晶面の指数による設定、マウスドラッグによる任意方位回転。三方晶・六方晶ではミラー・ブラベー指数（4指数 *hkil*）表記に対応。回転状態は全シミュレーションウィンドウで同期。
* **回折シミュレーション** : 運動学的・動力学的（ブロッホ波法）電子回折、X線回折（歳差カメラ・後方ラウエカメラを含む）、歳差電子回折(PED)、収束電子回折(CBED)。TEMホルダーシミュレーションで回折図形とホルダー傾斜角を連動。
* **HRTEM / STEMシミュレーション** : 部分コヒーレンスモデルを含む高分解能TEM像シミュレーション、熱散漫散乱を含むSTEMシミュレーション。
* **EBSD・電子飛程** : EBSDパターンシミュレーション、モンテカルロ法による電子飛程（電子軌道）シミュレーション（[電子飛程](8-electron-trajectory.md) 参照）。
* **スポット指数付け** : 実験回折像からのスポット自動検出・フィッティング・指数付け（Spot ID v1/v2）。
* **マクロ** : Python構文のマクロによる操作の自動化（[マクロ](20-macro/index.md) 参照）。
* **ライト／ダークテーマ** : UIはライト／ダークのカラーモードを選択できます。

## 動作環境
| 項目 | 最低要件 | 推奨環境 |
|------|---------|---------|
| OS | [.NET Desktop Runtime 10.0](https://dotnet.microsoft.com/download/dotnet/10.0) が動作するWindows（ARM64版Windowsにも対応） | Windows 11 |
| GPU | OpenGL 1.3対応 | OpenGL 4.3対応の外付けGPU |
| メモリ | — | 16 GB以上 |
| CPU | — | 8コア以上（動力学計算時） |

**Windows on ARM（ネイティブ・実験的）** : ネイティブARM64ビルドのポータブル版（`ReciPro-v.X_arm64.zip`、self-contained のため .NET Runtime のインストール不要）を[リリースページ](https://github.com/seto77/ReciPro/releases/latest)で実験的に公開しています。通常のx64版もARM64版Windows内蔵のエミュレーションで動作します。導入手順と制限は[トラブルシューティング](troubleshooting.md#windows-on-arm)を参照してください。

**macOS（非公式）** : ReciProの公式サポートはWindowsのみですが、**win-x64版**ポータブルZIPをWineラッパーの Sikarugir と OpenGL互換ドライバ Mesa3D と組み合わせることで、macOS（Apple Silicon）上で動作したという報告があります。ユーザーによる導入手順が <https://github.com/Ryo-fkushima/ReciPro_macOS_memo> で公開されています。なお、この方法は公式サポート外で、一部の記号（Å・上付き文字・矢印など）が文字化けする既知の問題があります。ARM64版ZIPは macOS + Wine では**動作しません**。また、現行の「x64版 + Rosetta 2」方式は macOS 28（2027年秋）以降は動作しなくなる見込みです — 詳細は[トラブルシューティング](troubleshooting.md#mac-linux)を参照してください。

## マニュアルの読み方

この GitHub Pages 版が現在の正本です。画面左のナビゲーションから章を選ぶか、右上の検索で機能名やUIラベルを検索してください。旧PDF版は過去版の参照用です。

* **旧PDF（英語）:** <https://raw.githubusercontent.com/seto77/ReciPro/master/ReciPro/doc/ReciProManual(en).pdf>
* **旧PDF（日本語）:** <https://raw.githubusercontent.com/seto77/ReciPro/master/ReciPro/doc/ReciProManual(ja).pdf>

## クイックスタート
1. [Releases](https://github.com/seto77/ReciPro/releases/latest) からダウンロード・インストール。
2. 内蔵の結晶リスト（約80結晶）から結晶を選択。CIFファイルのインポートや [CSManager](https://github.com/seto77/CSManager) も利用可能。
3. 右パネルの各機能（Structure Viewer、Stereonet、Crystal Diffraction、HRTEMシミュレーション等）を呼び出す。
4. マウスドラッグや晶帯軸・面指数の入力で結晶を回転。

## 引用文献
> Y. Seto, "ReciPro: free and open-source multipurpose crystallographic software integrating a crystal operation interface and diffraction simulators," *J. Appl. Cryst.* **55**, 397–410 (2022). <https://doi.org/10.1107/S1600576722000139>

## ライセンス
ReciProは [MITライセンス](https://github.com/seto77/ReciPro/blob/master/LICENSE.md) の下で配布されています。
