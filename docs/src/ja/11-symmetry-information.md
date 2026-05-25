<!-- nav -->

[← 1. 結晶データベース](1-crystal-database.md)  |  [🏠 ホーム](index.md)  |  [3. 散乱因子 →](12-scattering-factor.md)

# 対称性情報 (Symmetry Information)

**Symmetry Information** は、選択した結晶の空間群対称性を表示し、*International Tables for Crystallography* Vol. A 様式の対称要素・一般位置の模式図を描画します。

![対称性情報](../assets/cap-en-auto/FormSymmetryInformation.png)

ウィンドウは、空間群の情報（左上）、計算・表のタブ領域（右上）、2つの模式図（下部）から構成されます。

> 上の画像は英語表示ですが、模式図の記号は言語に依存しません。

---

## 空間群の情報

左上のパネルには、現在の空間群について以下が表示されます。

- **Number**（1〜230）と設定インデックス
- **Crystal System**（晶系）
- **Point Group**（点群）— ヘルマン・モーガン (HM) 記号とシェーンフリース (SF) 記号
- **Space Group**（空間群）— HM短記号・HM完全記号・SF記号・**Hall記号**

---

## Geometrics Calculation（幾何計算）

![幾何計算](../assets/cap-en-auto/FormSymmetryInformation.panel2.tabControl.tabPageGeometrics.png)

2つの面（*h₁k₁l₁*, *h₂k₂l₂*）または2つの軸（*u₁v₁w₁*, *u₂v₂w₂*）を入力すると、次が得られます。

- 各面の面間隔／各軸の長さ
- 2面間（または2軸間）の角度
- **両面に垂直な軸** と **両軸に垂直な面**

これらは現在の単位胞の計量に基づいて計算されます。

---

## Wyckoff Positions（ワイコフ位置）

![ワイコフ位置](../assets/cap-en-auto/FormSymmetryInformation.panel2.tabControl.tabPageWyckoff.png)

すべてのワイコフ位置について、多重度・ワイコフ記号・サイト対称性・代表座標を一覧表示します（複合格子では coset 代表がヘッダ行に示されます）。

---

## Conditions（反射条件）

![反射条件](../assets/cap-en-auto/FormSymmetryInformation.panel2.tabControl.tabPageConditions.png)

格子の複合（centring）と、らせん・映進などの対称操作に由来する反射（消滅）条件を表示します。

---

## 対称要素・一般位置の模式図

下部の2つのパネルは、*International Tables for Crystallography* Vol. A で使われる模式図を再現します。

- **左 — 対称要素**: 回転軸・らせん軸、鏡面・映進面、反転中心・回反点を、慣用の図記号で描画します。
- **右 — 一般位置**: 一般等価位置を `+`／`−` の円で表示し（コンマ付きは逆のキラリティ）、分率座標を付記します。

模式図の下のコントロール:

- **Direction**（`a` / `b` / `c`）— 投影軸を選択します。
- 表示範囲を限定でき（例: *Upper left quadrant only*）、一般位置図の *a*, *b*, *c* スケール係数を調整できます。
- 各模式図を **EMF**（ベクター画像）または **BMP**（ラスター画像）でクリップボードにコピーできます。EMFはPowerPointでグループ解除して編集できます。

> 同じ対称要素は [Structure Viewer](5-structure-viewer.md) の3Dモデル上にも直接描画できます。

---

[← 1. 結晶データベース](1-crystal-database.md)  |  [🏠 ホーム](index.md)  |  [3. 散乱因子 →](12-scattering-factor.md)
