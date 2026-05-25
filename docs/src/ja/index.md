<!-- nav -->

🌐 [English](../index.md)  |  **日本語**

[0. メインウィンドウ →](0-main-window.md)

## 概要
* ReciProは、MITライセンスで配布されている無料のソフトウェアです。様々な結晶学的計算や電子顕微鏡シミュレーション機能を提供します。
* 2020年3月のGitHub公開以来、累計27,000回以上ダウンロードされ、多くの結晶学者・電子顕微鏡研究者に利用されています。

## 主な機能
* **Full GUI** — すべての操作はグラフィカルインターフェースで行います。ファイルの入出力はドラッグ＆ドロップに対応。
* **結晶リスト** — 複数の結晶をまとめて管理。結晶ごとにウィンドウを開く必要はありません。
* **空間群データベース** — International Tables Volume Aの230空間群＋530個のHall symbolを内蔵。対称要素、ワイコフ位置、消滅則を含みます。対称要素と一般位置を *International Tables* Vol. A 様式の模式図として描画できます（[対称性情報](11-symmetry-information.md) 参照）。
* **原子情報** — H (1) 〜 Cf (98) の元素について、散乱因子（X線・電子線・中性子）、特性X線エネルギー、同位体比などを内蔵。
* **フレキシブルな結晶回転** — 晶帯軸・結晶面の指数による設定、マウスドラッグによる任意方位回転。三方晶・六方晶ではミラー・ブラベー指数（4指数 *hkil*）表記に対応。回転状態は全シミュレーションウィンドウで同期。
* **回折シミュレーション** — 運動学的・動力学的（ブロッホ波法）電子回折、X線回折（歳差カメラ・後方ラウエカメラを含む）、歳差電子回折(PED)、収束電子回折(CBED)。TEMホルダーシミュレーションで回折図形とホルダー傾斜角を連動。
* **HRTEM / STEMシミュレーション** — 部分コヒーレンスモデルを含む高分解能TEM像シミュレーション、熱散漫散乱を含むSTEMシミュレーション。
* **EBSD・電子飛程** — EBSDパターンシミュレーション、モンテカルロ法による電子飛程（電子軌道）シミュレーション（[電子飛程](13-electron-trajectory.md) 参照）。
* **スポット指数付け** — 実験回折像からのスポット自動検出・フィッティング・指数付け（Spot ID v1/v2）。
* **マクロ** — Python構文のマクロによる操作の自動化（[マクロ](20-macro.md) 参照）。
* **ライト／ダークテーマ** — UIはライト／ダークのカラーモードを選択できます。

## 動作環境
| 項目 | 最低要件 | 推奨環境 |
|------|---------|---------|
| OS | [.NET Desktop Runtime 10.0](https://dotnet.microsoft.com/download/dotnet/10.0) が動作するWindows（ARM64版Windowsにも対応） | Windows 11 |
| GPU | OpenGL 1.3対応 | OpenGL 1.5対応の外付けGPU |
| メモリ | — | 16 GB以上 |
| CPU | — | 8コア以上（動力学計算時） |

## マニュアル
* **英語版 (PDF):** https://raw.githubusercontent.com/seto77/ReciPro/master/ReciPro/doc/ReciProManual(en).pdf
* **日本語版 (PDF):** https://raw.githubusercontent.com/seto77/ReciPro/master/ReciPro/doc/ReciProManual(ja).pdf

## クイックスタート
1. [Releases](https://github.com/seto77/ReciPro/releases/latest) からダウンロード・インストール。
2. 内蔵の結晶リスト（約80結晶）から結晶を選択。CIFファイルのインポートや [CSManager](https://github.com/seto77/CSManager) も利用可能。
3. 右パネルの各機能（Structure Viewer、Stereonet、Crystal Diffraction、HRTEMシミュレーション等）を呼び出す。
4. マウスドラッグや晶帯軸・面指数の入力で結晶を回転。

## 引用文献
> Y. Seto, "ReciPro: free and open-source multipurpose crystallographic software integrating a crystal operation interface and diffraction simulators," *J. Appl. Cryst.* **55**, 397–410 (2022). https://doi.org/10.1107/S1600576722000139

## ライセンス
ReciProは [MITライセンス](https://github.com/seto77/ReciPro/blob/master/LICENSE.md) の下で配布されています。

---

[0. メインウィンドウ →](0-main-window.md)
