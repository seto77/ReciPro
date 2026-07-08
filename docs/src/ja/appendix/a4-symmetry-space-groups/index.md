# 付録 A4. 対称性と空間群

メインウィンドウの章 [2. 対称性情報](../../2-symmetry-information.md) はGUIの操作案内です。どのタブに何が表示され、どのボタンでどの模式図がコピーされるかを説明します。本付録は、その表や図の背後にある **結晶学・群論の理論的背景** をまとめます。ヘルマン・モーガン記号が実際に何を表しているか、*International Tables for Crystallography*（ITA）Vol. A 様式の対称要素・一般位置模式図の読み方、そして **Group Relations...**（群の関係）ウィンドウの超群/部分群表と用語（*translationengleiche*、*klassengleiche*、共役類、ドメイン、双晶則、…）の意味です。

![Symmetry Information](../../../assets/cap-ja-auto/FormSymmetryInformation.png)

> 上の画像は英語表示ですが、模式図の記号は言語に依存しません。

2つのウィンドウを扱い、次の順で読むのが最適です。

1. **[A4.1. 空間群記号と対称性模式図](symbols-and-diagrams.md)** — ヘルマン・モーガン記号・シェーンフリース記号・Hall記号、**Properties**（群の性質）タブに表示される群論的分類（中心対称・Sohncke・Symmorphic・極性・算術結晶類・Patterson 対称、…）、**Operations**（対称操作）タブの各対称操作の座標トリプレット/Seitz記号/幾何学的種類による表現、そして [対称性情報](../../2-symmetry-information.md) ウィンドウ下部の対称要素・一般位置模式図の図記号の約束事。
2. **[A4.2. 群・部分群の関係](group-subgroup-relations.md)** — *極大部分群* / *極小超群* とは何か、Hermann の *t*-/*k*- の区別、そして対称性情報の **Options**（オプション）パネルから開く **Group Relations...**（群の関係）ブラウザの各タブ（Diagram/Matrix/Orbit splitting/Domains & Twins/New reflections）の読み方。

A4.1 を先に置くのは、A4.2 がそこで導入した表記（同じヘルマン・モーガン記号、Seitz記号、"3-fold rotation"・"c-glide plane"・"screw axis" のような幾何学的種類の言い回し）を、部分群/超群の関係を説明する際に絶えず参照するためです。

---

## 対象範囲と出典

ReciProの内蔵データベースは、*International Tables for Crystallography* の **Volume A**（空間群対称性）と **Volume A1**（空間群の極大部分群）に収録された通りの230空間群タイプ（530通りの設定・原点選択を含む）を収録しています。本付録が説明するのはReciProによるその「見せ方」——記法・模式図・閲覧ツール——であり、読者が格子・点群・対称操作という概念について学部レベルの素養をすでに持っていることを前提とします。ITA そのものに代わるものではなく、収録データの権威ある典拠は引き続きITAです（著作権の都合上ReciProはITAの表をそのまま複製できません。ある空間群タイプに対する原点・設定の一覧はReciPro独自の一覧として **Settings**（設定一覧）タブに表示されます）。

!!! note "Group Relations... は現在も開発が進行中の機能です"
    **Group Relations...**（群の関係）ブラウザ（A4.2）は、*translationengleiche*（t-）と *klassengleiche*（k-、*isomorphic*（同型）を含む）の部分群・超群を、あらかじめ用意された表からではなく空間群自身の対称操作から直接計算します。そのため表示される各関係は、表からの転記ではなく独立に検証可能です。残る制限——同型系列の列挙は index ≤ 4 まで、系統図 (Diagram) タブの描画は *t*-関係のみ——は A4.2 の「現在の制限」に明記されています。

---

## 関連項目

- [2. 対称性情報](../../2-symmetry-information.md) — 本付録が解説するGUIガイド。
- [A4.1. 空間群記号と対称性模式図](symbols-and-diagrams.md) · [A4.2. 群・部分群の関係](group-subgroup-relations.md)
- [Appendix A1. 座標系](../a1-coordinate-system/1-orientation.md)
- [Appendix A2. ビーム相互作用（固体物理的背景）](../a2-beam-interaction/index.md) — 空間群の反射条件（消滅則）が構造因子へどう反映されるか。
