# メインウィンドウ

ReciProを起動すると、メインウィンドウが表示されます。このウィンドウで計算対象の結晶を選択し、回転させ、各種機能を呼び出します。

![メインウィンドウ](../assets/cap-ja-auto/FormMain.png)

ウィンドウは以下の領域で構成されています：

| 領域 | 位置 | 説明 |
|------|------|------|
| **メニューバー** | 最上部 | ファイル操作、オプション、ヘルプ |
| **回転コントロール** | 左部 | 結晶方位の表示・設定 |
| **結晶リスト** | 中央上部 | 結晶の選択・管理 |
| **結晶情報** | 中央下部 | 格子定数・対称性・原子位置の編集 |
| **機能** | 右部 | シミュレーション・解析ウィンドウの起動 |

---

## キーボード・マウスショートカット

メインウィンドウは **アプリ全体** で有効なショートカットを登録します。これらは結晶構造ビューア・ステレオネット・回折シミュレータ・Spot ID・電卓の各ウィンドウにフォーカスがある間も動作します。

| ショートカット | 動作 |
|----------------|------|
| <kbd>F1</kbd> | このページのオンラインマニュアルを開く |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>D</kbd> | **回折シミュレータ** の開閉 |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>V</kbd> | **結晶構造ビューア** の開閉 |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>S</kbd> | **ステレオネット** の開閉 |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>T</kbd> | **Spot ID** の開閉 |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd> ＋ 矢印キー | 結晶をその方向へ1ステップ回転（2つ同時押しで斜め方向） |
| <kbd>CTRL</kbd> の素早い2回押し | **電卓** の開閉 |
| <kbd>CTRL</kbd>+<kbd>SHIFT</kbd>+<kbd>R</kbd> | 選択中の結晶の **Reserved** フラグを切替 |
| 起動中に <kbd>CTRL</kbd> を押し続ける | OpenGL を無効にして起動（描画トラブルの回避） |
| 左下の方位ウィジェット（*Current Direction* の軸表示）を左ドラッグ | 結晶を回転 |
| 方位ウィジェットを右ダブルクリック | ウィジェットの画像をクリップボードにコピー |
| 機能ボタンをシングルクリック | そのウィンドウを開閉 |
| 機能ボタンをダブルクリック | ウィンドウを強制的に表示し前面へ |
| 結晶リストを右クリック | コンテキストメニュー（名前変更／複製／削除／CIF出力…） |
| **Current Index** ラベルをダブルクリック | 最大UVWボックスの表示/非表示 |
| ウィンドウにファイルをドロップ | 結晶リスト（`.xml`, `.cdb2`）または結晶（`.cif`, `.amc`）を読込 |

→ 全ウィンドウの一覧は **[21. キーボード・マウスショートカット](21-shortcuts.md)** を参照。

---

## 基本ワークフロー

はじめてお使いの方は、次の手順を参考にしてください。

1. **結晶リスト**で計算対象の結晶を選択する。CIF/AMC ファイルを使う場合は **結晶情報** 領域へドラッグ＆ドロップします。
2. 格子定数・原子位置を編集した場合は、**リストへ追加** または **選択結晶と入れ替え** で変更を結晶リストへ反映する。
3. **回転情報**で晶帯軸、結晶面、オイラー角、またはマウスドラッグにより結晶方位を決める。
4. 右側の **ファンクション** から目的の機能を開く。回折、HRTEM/STEM、EBSD などの計算ウィンドウは、現在選択中の結晶と方位を引き継ぎます。

---

## ファイルメニュー

### ファイル


| メニュー項目 | 説明 |
|-------------|------|
| 結晶リストを読み込み（現在のリストを消去） | 結晶リストファイル(*.xml)を読み込み、現在のリストを置換 |
| 結晶リストを読み込み（現在のリストに追加） | 結晶リストを読み込み、現在のリストに追加 |
| 結晶リストを初期状態に戻す | 起動時の結晶リストを再読込 |
| 結晶リストを保存 | 現在の結晶リストを保存 |
| 選択結晶をCIF形式で書き出し | 選択中の結晶をCIFフォーマットで保存 |
| 現在のリストを消去 | すべての結晶をリストから削除 |
| 閉じる | アプリケーションを終了 |

### オプション


| メニュー項目 | 説明 |
|-------------|------|
| ツールチップを表示 | ツールチップの表示/非表示を切り替え |
| Use Miller-Bravais (hkil) index | 三方晶・六方晶系で面指数をプログラム全体で4指数  \((hkil)\) 表記にする |
| レジストリを初期化 (要再起動) | 次回起動時にウィンドウサイズ・波長・カメラ長等の設定をリセット |
| Crystallography.Native.dll を無効化 (要再起動) | ネイティブ(C++)ライブラリの読込に失敗する場合にマネージドコードで代替 |
| OpenGLによる全てのレンダリングを無効化 (要再起動) | 古いGPU/リモートデスクトップ環境向け |
| OpenGLによるテキストレンダリングを無効化 (要再起動) | 一部GPUでの文字描画不具合への対処 |
| MKL ライブラリを使用 | 数値計算にIntel MKLを使用 |
| ダークモード | ライト／ダークのカラーテーマを切り替え |
| Powder diffraction function (under development) | 多結晶（粉末）回折ウィンドウを有効化 |
| Capture GUI Components… | GUIスクリーンショットを保存する開発者向けツール |

### ヘルプ


| メニュー項目 | 説明 |
|-------------|------|
| アップデート | 新バージョンの確認・インストール |
| ヒント | 使い方のヒントを表示 |
| バージョン履歴 | バージョン履歴を表示 |
| ライセンス | MITライセンスを表示 |
| Github ページ | GitHubリポジトリを開く |
| バグ・要望報告 | GitHub Issuesを開く |
| 使い方 (WEB) | オンラインマニュアル (GitHub Pages 版) を、UI 言語に合わせて開きます。 |

言語の切り替えは別の **Language** メニューで行います（英語/日本語、要再起動）。

### Language

UI 言語を英語または日本語に切り替えます。変更は再起動後に反映されます。

### マクロ

[マクロ](20-macro/index.md) ウィンドウを開き、Python構文のスクリプトで ReciPro の操作を自動化します。よく使う処理を繰り返す場合は、[組み込み関数一覧](20-macro/1-built-in-functions.md) と [マクロの使用例](20-macro/2-examples.md) を参照してください。


---

## 回転状態の表示/制御

結晶の回転状態は、回折シミュレータ・Structure Viewer・ステレオネット・HRTEM/STEM シミュレータ・EBSD シミュレータなど他のウィンドウでも共通して使われます。単なる表示上の設定ではなく、シミュレーションで用いる入射ビーム方向と結晶座標系の関係を定義します。短い操作動画は [How to use（動画）](appendix/a0-how-to-use.md) のページにあります。

### 現在の結晶方位

![現在の結晶方位](../assets/cap-ja-auto/FormMain.toolStripContainer1.panel1.groupBoxCurrentDirection.png)

結晶の回転状態がグラフィカルに表示されます。マウスドラッグで回転できます。

結晶軸の色：
- **赤**: *a*軸
- **緑**: *b*軸
- **青**: *c*軸

### 方向リセット
結晶方位を**初期状態**にリセットします。初期状態とは、*c*軸がスクリーンに垂直、*b*軸が画面上方向を向く状態です。

### 晶帯軸
スクリーンに垂直な方向に最も近い晶帯軸指数を表示します。検索範囲（例：*u* + *v* + *w* < 30）を指定できます。

### オイラー角
**Z–X–Z**系のオイラー角で結晶方位を設定します：
- \(\Phi\): Z軸回転
- \(\Theta\): X軸回転
- \(\Psi\): Z軸回転

回転は \(\Psi \to \Theta \to  \Phi\) の順に適用されます。詳しくは [回転ジオメトリ](4-rotation-geometry.md) および [座標系](appendix/a1-coordinate-system/1-orientation.md) を参照してください。

### 回転

![回転](../assets/cap-ja-auto/FormMain.toolStripContainer1.panel1.groupBoxArrows.png)

矢印方向に角度**ステップ**分だけ回転します。**アニメーション**をチェックすると連続回転します。

### 軸/面投影

![軸/面投影](../assets/cap-ja-auto/FormMain.toolStripContainer1.panel1.groupBoxProjectAlong.png)

指定した晶帯軸や結晶面の方向に結晶方位を設定します。

- **固定**: チェックすると、指定した軸・面が空間的に固定された状態で回転します。
- **軸方向**: 指定した晶帯軸 \([uvw]\) をスクリーン垂直方向に設定します。**面方向**も設定されていれば、その方向が画面上向きになります。
- **面方向**: 指定した結晶面 \((hkl)\) の法線をスクリーン垂直方向に設定します。**軸方向**も設定されていれば、その方向が画面上向きになります。

### 結晶方位を設定する基本的な方法

| 方法 | こんなとき | 操作場所 |
|------|-----------|---------|
| マウスドラッグ | 結晶軸を見ながら自由に回転させたいとき | 「現在の結晶方位」パネル |
| 矢印ボタン | 小さな回転を繰り返し行いたいとき | 「回転」パネル |
| 晶帯軸 | \([001]\) や \([110]\) など、見たい方向が分かっているとき | 「軸/面投影」/ 晶帯軸の入力 |
| 面法線 | 結晶面 \((hkl)\) をスクリーンに垂直にしたいとき | 「軸/面投影」/ 面の入力 |
| オイラー角 | 再現可能な数値で方位を指定したいとき | 「オイラー角」 |

回転行列と座標系の規約は [回転ジオメトリ](4-rotation-geometry.md) および [Appendix A1. 座標系の定義](appendix/a1-coordinate-system/1-orientation.md) を参照してください。

---

## 結晶リスト

![結晶リスト](../assets/cap-ja-auto/FormMain.toolStripContainer1.splitContainer.groupBoxCrystalList.png)

読み込まれた結晶を表示（初期状態で約80結晶）。結晶を選択すると **結晶情報** に詳細が表示され、計算対象に設定されます。**結晶リストを右クリック**するとコンテキストメニュー（*名前の変更*・*CIF形式で書き出し*・*複製*・*削除*）が表示されます。

![結晶編集ボタン](../assets/cap-ja-auto/FormMain.toolStripContainer1.splitContainer.flowLayoutPanelCrystalEdit.png)

| ボタン | 動作 |
|--------|------|
| 上へ / 下へ | 選択した結晶の順番を移動 |
| 複製 | 選択中の結晶を複製 |
| 削除 / 全削除 | 選択した結晶またはすべての結晶を削除 |
| リストへ追加 / 選択結晶と入れ替え | 現在の結晶をリスト末尾に追加、または選択中の結晶と置換 |

---

## 結晶情報

格子定数・対称性・原子位置を設定・表示します。CIFファイルやAMCファイルをこの領域にドラッグ＆ドロップして結晶を読み込むこともできます。このコントロールは ReciPro・PDIndexer・CSmanager で共通ですが、表示されるタブや機能はソフトごとに異なります。ReciPro では「格子/対称性」「原子情報」「引用文献」の3タブを表示します（状態方程式(EOS)・弾性定数などのタブは他のソフト向けで、ReciPro では表示されません）。

> **重要**: 変更を加えた場合は必ず**リストへ追加**または**選択結晶と入れ替え**ボタンを押してください。押さないと、別の結晶を選択した際に変更内容が失われます。

![結晶コントロール](../assets/cap-ja-auto/FormMain.toolStripContainer1.splitContainer.groupBoxCrystalInformation.crystalControl.png)

上部の **Name**（結晶名）・**Formula**（化学組成式。原子情報から自動算出）と **Reset**（全項目を初期化）は常に表示されます。

### 格子/対称性 タブ

![格子/対称性](../assets/cap-ja-auto/FormMain.toolStripContainer1.splitContainer.groupBoxCrystalInformation.crystalControl.tabControl.tabPageBasicInfo.png)

格子定数・対称性と、そこから導かれる量を扱います。

| 項目 | 説明 |
|------|------|
| Cell constants | 格子定数 a, b, c（単位 Å = 10⁻¹⁰ m）と α, β, γ。対称性を指定すると拘束条件（例: 立方晶では a=b=c, α=β=γ=90°）に従って自動調整されます。 |
| Symmetry | 結晶系（Crystal system）・点群（Point group）・空間群（Space group）を選択します。**Search** ボックスに文字列を入力すると候補が一覧表示されます（大文字・小文字を区別）。 |
| Cell Volume / Cell Mass | 単位胞の体積・質量。 |
| Molar Volume / Molar Mass / Z / Density | モル体積・モル質量・単位胞中の化学式単位数 Z・密度。これらは**原子情報が入力されているときのみ**表示されます。 |
| Color of Profile | この結晶の回折プロファイルを描画する際の色。 |

### 原子情報 タブ

![原子情報](../assets/cap-ja-auto/FormMain.toolStripContainer1.splitContainer.groupBoxCrystalInformation.crystalControl.tabControl.tabPageAtom.png)

原子の種類・位置・温度因子・散乱因子を設定します。原子リストは **Add**（追加）・**Replace**（選択行を置換）・**Up/Down**（並べ替え）・**Delete**（削除）で編集します。各原子の項目は次の通りです。

| 項目 | 説明 |
|------|------|
| Label | 原子のラベル（任意の識別名）。 |
| Element | 元素（イオン価数を含む）。 |
| X, Y, Z | 分率座標（0–1）。1/2 や 2/3 などの分数表記でも入力できます。 |
| Occ | 占有率（0–1）。 |

**Origin shift**: 全原子座標の原点を移動します。プリセットボタン（**+** / **−**）で標準的な量だけシフトするか、**Apply custom shift** で任意量を指定します。

**Debye-Waller factor（温度因子）**:

| 項目 | 説明 |
|------|------|
| Notation | U か B のどちらの表記を使うか。 |
| Model | 等方性（Isotropy）か異方性（Anisotropy）か。 |
| B##, U## | 異方性の場合は各成分（B11 など）を入力。 |

**Scattering factor（散乱因子）**: 原子ごとに使用する散乱因子を選びます。

| 波 | 出典・設定 |
|----|-----------|
| X-ray | イオン価数を含む散乱因子（International Tables for Crystallography, Vol. C）。 |
| Electron | 電子線用散乱因子（Peng 1998, Acta Cryst. A54, 481–485）。 |
| Neutron | 中性子散乱長。**Natural isotope abundance**（天然同位体存在比）または **Custom isotope abundance**（任意の同位体組成）を選択。 |

### 引用文献 タブ

![引用文献](../assets/cap-ja-auto/FormMain.toolStripContainer1.splitContainer.groupBoxCrystalInformation.crystalControl.tabControl.tabPageReference.png)

結晶構造の出典を記録します：**Note**（メモ）・**Authors**（著者）・**Journal**（雑誌）・**Title**（論文タイトル）。

### コンテキストメニュー（右クリック）

コントロールの空白部分を右クリックすると、主に次の操作が行えます。

| メニュー項目 | 動作 |
|--------------|------|
| Beam Interaction | [ビーム相互作用](3-scattering-factor.md) ウィンドウを開く。 |
| Symmetry information | [対称性情報](2-symmetry-information.md) ウィンドウを開く。 |
| Import from CIF, AMC | CIF / AMC ファイルから結晶を読み込む。 |
| Export to CIF | 現在の結晶を CIF 形式で書き出す。 |
| Revert cell constants | 格子定数を最初に読み込んだ値に戻す。 |
| Convert to P1 spacegroup | 空間群を P1 に展開する。 |
| Convert to a superstructure | a, b, c を整数倍した超構造に変換する（サイズ指定ダイアログ）。 |
| Convert to an equivalent space group | 等価な空間群（軸の取り方を変えたセッティング）に変換する。 |

---

## 機能パネル {#functions}

ウィンドウ右側の縦並びボタンから、各解析・シミュレーションウィンドウを起動します（下表 [ファンクション](#functions) を参照）。

![ファンクションパネル](../assets/cap-ja-auto/FormMain.toolStripContainer1.toolStrip1.png)

| ボタン | 説明 | 詳細 |
|--------|------|------|
| 結晶データベース | 内蔵／オンラインデータベースからの結晶検索・取込 | [結晶データベース](1-crystal-database.md) |
| 対称性詳細 | 空間群の対称性情報・ITC Vol.A 様式の対称ダイアグラム | [対称性情報](2-symmetry-information.md) |
| ビーム相互作用 | ビームと結晶の相互作用: 反射・減衰・散乱因子・蛍光 | [ビーム相互作用](3-scattering-factor.md) |
| ゴニオメーター | 回転行列の3D表示・オイラー角変換 | [回転ジオメトリ](4-rotation-geometry.md) |
| 結晶構造 | OpenGLによる結晶構造の3D描画 | [結晶構造ビューア](5-structure-viewer.md) |
| ステレオネット | ステレオネット投影 | [ステレオネット](6-stereonet.md) |
| 回折シミュレータ | 電子線・X線の単結晶回折シミュレーション | [回折シミュレータ](7-diffraction-simulator/index.md) |
| 電子飛程シミュレータ | モンテカルロ法による電子飛程（電子軌道）シミュレーション | [電子飛程](8-electron-trajectory.md) |
| HRTEM/STEMシミュレータ | 高分解能TEM/STEM像シミュレーション | [HRTEM/STEMシミュレータ](9-hrtem-stem-simulator/index.md) |
| スポットID v1 | 制限視野回折パターンの指数付け（旧「TEM ID」） | [Spot ID v1](10-spot-id.md) |
| スポットID v2 | 回折スポットの検出・指数付け | [Spot ID v2](11-spot-id-v2.md) |
| EBSDシミュレータ | EBSDパターンシミュレーション | [EBSDシミュレーション](12-ebsd-simulation.md) |
| 粉末回折 | 多結晶（粉末）回折。**オプション ▸ Powder diffraction function** で有効化 | — |

---

## 関連項目

- [結晶データベース](1-crystal-database.md)
- [回転ジオメトリ](4-rotation-geometry.md)
- [結晶構造ビューア](5-structure-viewer.md)
- [回折シミュレータ](7-diffraction-simulator/index.md)
- [キーボード・マウスショートカット](21-shortcuts.md)
- [基本座標系と結晶方位](appendix/a1-coordinate-system/1-orientation.md)
