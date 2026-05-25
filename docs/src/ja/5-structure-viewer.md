# Structure Viewer（結晶構造ビューア）

**Structure Viewer** は、選択した結晶をOpenGLを使って3次元的に描画します。

![結晶構造ビューア](../assets/cap-ja-auto/FormStructureViewer.png)

---

## メインエリア

画面上部に結晶構造が描画されます。左上に光源の方向、左下に結晶軸の方向、右側に原子の凡例が表示されます。

### マウス操作

| 操作 | 動作 |
|------|------|
| 左ドラッグ | 回転 |
| 中ドラッグ | 平行移動 |
| 右ドラッグ / ホイール | ズーム |
| 左ダブルクリック | 原子の選択/解除 |
| 右クリック | コンテキストメニュー |
| Ctrl + 右ダブルクリック | 透視投影/正射投影の切替 |
| Ctrl + 右ドラッグ | 透視の度合いを調整 |

---

## メニューバー

![メニューバー](../assets/cap-ja-auto/FormStructureViewer.panelTop.menuStrip1.png)

### ファイルメニュー


| メニュー項目 | 説明 |
|-------------|------|
| 画像を保存 | 描画画像をファイルに保存 |
| 画像をクリップボードにコピー | 画像をコピー（Ctrl+Shift+C でも可） |
| 動画を保存 | 回転アニメーションをMP4で保存（回転速度・時間・方向を設定） |
| ショートカットヒント | キーボードショートカットを表示 |

**動画を保存** を選ぶと、下記の「Movie setting」ダイアログが開きます。回転速度・録画時間・回転方向（現在の投影 / 方位指数 / 格子面）、コーデック（H.264 / H.265）、エンコード速度を設定し、**OK** で MP4 ファイルを生成します。

![動画設定ダイアログ](../assets/cap-ja-auto/FormMovie.png)

### ツールメニュー


---

## タブメニュー

### 描画範囲 (Bounds)

![Bounds タブ](../assets/cap-ja-auto/FormStructureViewer.splitContainer1.tabControl.tabPageBounds.png)

結晶の描画範囲を指定します。2つのモードがあります。

**描画範囲を単位胞単位で指定**
- a, b, c軸の単位格子を単位とした描画範囲
- **中心**: 中心の分率座標
- **範囲**: 各軸の上限/下限
- よく使う値のプリセットボタンあり

**描画範囲を格子面単位で指定**
- 結晶面で描画領域を定義
- 面が閉じた領域を形成しない場合は自動的に1単位格子に設定

### 原子 (Atoms)

![Atoms タブ](../assets/cap-ja-auto/FormStructureViewer.splitContainer1.tabControl.tabPageAtom.png)

原子の座標と外観を設定します。

- **原子リスト**: 追加・置換・削除。チェックボックスで一時的に非表示
- **元素と位置**: ラベル、元素、分率座標 (X, Y, Z)、占有率
- **Origin shift**: プリセットまたはカスタム値で原子位置をシフト
- **外観**: 半径、色、マテリアル（テクスチャ）の設定
- **同じ元素に適用**: 同じ元素のすべての原子に外観設定を適用

> **重要**: 変更を永続保存するには、メインウィンドウの**リストへ追加**または**選択結晶と入れ替え**を押してください。

### 結合と多面体 (Bonds & Polyhedra)

![Bonds タブ](../assets/cap-ja-auto/FormStructureViewer.splitContainer1.tabControl.tabPageBond.png)

結合と配位多面体を定義します。

### 単位格子 (Unit cell)

![Unit Cell タブ](../assets/cap-ja-auto/FormStructureViewer.splitContainer1.tabControl.tabPageUnitCell.png)

- **平行移動**: 空間群のデフォルト原点からの移動量
- **面を表示**: 6つの格子面の描画
- **辺を表示**: 格子の辺を描画

### 格子面 (Lattice plane)

![Lattice Plane タブ](../assets/cap-ja-auto/FormStructureViewer.splitContainer1.tabControl.tabPageLatticePlane.png)

ミラー指数で指定した格子面を描画します。

### 配位情報 (Coordinate information)

![Coordinate Information タブ](../assets/cap-ja-auto/FormStructureViewer.splitContainer1.tabControl.tabPageCoordinateInformation.png)

原子の配位情報を表示します。

### 情報

![情報 タブ](../assets/cap-ja-auto/FormStructureViewer.splitContainer1.tabControl.tabPageInformation.png)

### 投影法 (Projection)

![Projection タブ](../assets/cap-ja-auto/FormStructureViewer.splitContainer1.tabControl.tabPageProjection.png)

正射投影または透視投影。奥行きフェードアウト、描画品質、透明度モードの設定。

### その他 (Misc.)

![Misc タブ](../assets/cap-ja-auto/FormStructureViewer.splitContainer1.tabControl.tabPageMisc.png)

- **光源・結晶軸・凡例の設定**: サイズ設定。「元素でまとめる」で凡例表示を切替
- **原子ラベル**: 原子ラベルのフォントサイズと色

### 対称要素 (Symmetry Elements)

**対称要素**タブは、空間群の対称操作を3Dモデル上に直接描画します（ツールバーの **対称要素** ボタンで表示/非表示を切替）。要素の種類ごとに個別に表示できます。

- **回転軸** と **らせん軸**
- **鏡映面** と **映進面**
- **対称心** と **回反軸**

各種類について、記号サイズ・線幅・色を調整できます。

---

## ツールバー

![ツールバー](../assets/cap-ja-auto/FormStructureViewer.toolStrip1.png)

| ボタン | 説明 |
|--------|------|
| 結晶軸 | 結晶軸の方向を表示（サイズは格子定数を反映） |
| 光源 | ドラッグで光源方向を変更 |
| 凡例 | 原子の凡例を表示（ラベルまたは元素名） |
| Like Vesta | Vestaソフトウェア風の原子色・サイズ・結合設定に変更 |
| 角度をリセット | 初期方位に戻す |
| 原子 / ラベル | 原子オブジェクト／原子ラベルの表示切替 |
| 単位胞 | 単位格子の辺の表示切替 |
| 対称要素 | 対称要素オーバーレイの表示切替（上記参照） |

> ウィンドウ右上の **Size (W×H)** ボックスで、画像の保存・コピー時のピクセルサイズを指定します。
