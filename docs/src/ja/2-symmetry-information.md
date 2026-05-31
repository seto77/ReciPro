# 対称性情報 (Symmetry Information)

**Symmetry Information** は、選択した結晶の空間群対称性の詳細情報を表示し、さらに*International Tables for Crystallography* Vol. A の様式に沿った対称要素・一般位置の模式図を描画します。

![対称性情報](../assets/cap-en-auto/FormSymmetryInformation.png)

ウィンドウは、空間群の情報（左上）、計算・表のタブ領域（右上）、2つの模式図（下部）から構成されます。

> 上の画像は英語表示ですが、模式図の記号は言語に依存しません。

---

## キーボード・マウスショートカット

このウィンドウに特別なキー／マウスの組み合わせはありません。<kbd>F1</kbd> でこのマニュアルページが開き、2つの **Copy** ボタンで対称要素図・一般位置図をクリップボードへコピーします（ビットマップ、または **EMF** チェック時はベクター EMF）。

→ 全ウィンドウの一覧は **[21. キーボード・マウスショートカット](21-shortcuts.md)** を参照。

---

## 空間群の情報

左上のパネルには、現在の空間群について以下が表示されます。

- **Number**（1〜230）と設定インデックス
- **Crystal System**（結晶系）
- **Point Group**（結晶族点群） : ヘルマン・モーガン (HM) 記号とシェーンフリース (SF) 記号
- **Space Group**（空間群） : HM短縮記号・HM完全記号・SF記号・**Hall記号**

---

## Geometrics Calculation（幾何計算）

![幾何計算](../assets/cap-en-auto/FormSymmetryInformation.panel2.tabControl.tabPageGeometrics.png)

2つの結晶面 \((h_1, k_1, l_1)\),  \((h_2, k_2, l_2)\) または2つの方向指数 \([u_1, v_1, w_1]\),  \([u_2, v_2, w_2]\) を入力すると、次が得られます。

- 各面の面間隔／各軸の長さ
- 2面間（または2軸間）の角度
- **両面に垂直な方向指数** と **両軸に垂直な面指数**

これらは現在の単位胞の計量に基づいて計算されます。

---

## Wyckoff Positions（ワイコフ位置）

![ワイコフ位置](../assets/cap-en-auto/FormSymmetryInformation.panel2.tabControl.tabPageWyckoff.png)

すべてのワイコフ位置について、多重度・ワイコフ記号・サイト対称性・一般/特殊位置を一覧表示します。複合格子では 格子並進ベクトルがヘッダ行に示されます。

---

## Conditions（反射条件）

![反射条件](../assets/cap-en-auto/FormSymmetryInformation.panel2.tabControl.tabPageConditions.png)

複合格子、らせん・映進などの対称操作に由来する反射（出現則）条件を表示します。

---

## 対称要素・一般位置の模式図

![対称要素・一般位置の模式図](../assets/cap-ja-auto/FormSymmetryInformation.tableLayoutPanel1.png)

下部の2つのパネルは、*International Tables for Crystallography* Vol. A の表記に則った空間群の対称性模式図を再現します。

- **対称要素 (左)**: 回転軸・らせん軸、鏡面・映進面、反転中心・回反点を、慣用の図記号で描画します。
  - 立方晶系の\(F\)格子に関しては、単位胞の1/8の領域（Upper left quadrant only）のみを表示します。
  - このような対称要素は [Structure Viewer](5-structure-viewer.md) の3Dモデル上にも直接描画することができます。

- **一般位置 (右)**: 一般等価位置を円（コンマ付きは鏡像）で表示し、分率座標を付記します。
  - 立方晶系についてのみ、3回回転軸で結ばれる三つの円をつなぐ補助線が表示されます。


模式図の下のコントロール:

- **Direction**（`a` / `b` / `c`） : 投影する結晶軸を選択します。
- 各模式図を **EMF**（ベクター画像）または **BMP**（ラスター画像）でクリップボードにコピーできます。EMFはPowerPointでグループ解除して編集できます。
