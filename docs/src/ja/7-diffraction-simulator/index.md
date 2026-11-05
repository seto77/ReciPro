---
title: 回折シミュレータ
---

# 回折シミュレータ (Crystal Diffraction)

**回折シミュレータ (Crystal Diffraction)** は、単結晶のX線回折・中性子線回折・電子線回折パターンをシミュレーションします。

![回折シミュレータ](../../assets/cap-ja-auto/FormDiffractionSimulator.png)

ウィンドウは、**左側**に回折パターンの描画エリア、**右側**にスポット特性（波長・入射ビーム・強度計算・外観など）の設定パネルが並びます。波長と入射ビームの組合せで撮影モード（X線回折・SAED・PED・CBED）が決まり、それに応じて右側パネルの構成が切り替わります。

---

## このページと各モードページの分担

- **このページ（まとめ）**: 全モードで共通の操作（ショートカット・メニュー・ツールバー・画面/検出器情報・オーバーレイのタブ・スポット情報・検出器ジオメトリ・動的圧縮）をまとめます。
- **各モードページ**: そのモードを選んだときに **右側に現れる全設定項目**（波長・入射ビーム・強度計算・外観・ブロッホ波設定・プリセッション設定など）を、1ページで完結するように網羅します（モード間で一部重複します）。

| モード | 内容 | ページ |
|--------|------|--------|
| **X線（・中性子）回折** | 単結晶X線／中性子回折パターン（平行・歳差X線・Back Laue） | [X線回折シミュレーション](5-x-ray-neutron-diffraction.md) |
| **SAED** | 平行ビーム電子回折（制限視野電子回折） | [SAEDシミュレーション](1-saed-simulation.md) |
| **PED** | 歳差電子回折（プリセッション） | [PEDシミュレーション](2-ped-simulation.md) |
| **CBED** | 収束電子線回折 | [CBEDシミュレーション](3-cbed-simulation.md) |

> **ALCHEMI はモードではありません。** 波長と入射ビームの組合せで決まる X線 / SAED / PED / CBED と違い、ALCHEMI シミュレータは **オプション** メニューから開く**子ウィンドウ**なので、ここでは別枠として掲げます。

| 子ウィンドウ | 内容 | ページ |
|------|------|------|
| **ALCHEMI シミュレータ** | サイト分解のイオン化ロッキングカーブ（チャネリング増強微小分析） | [ALCHEMI シミュレーション](4-alchemi-simulation.md) |

---

## モード早見表

**波長（線源）**と**入射ビーム**の組合せから、設定すべきページを引きます。

| 波長 | 入射ビーム | モード | ページ |
|------|-----------|--------|--------|
| 電子線 | 平行 | SAED | [SAEDシミュレーション](1-saed-simulation.md) |
| 電子線 | 歳差（電子＝PED） | PED | [PEDシミュレーション](2-ped-simulation.md) |
| 電子線 | 収束（CBED） | CBED | [CBEDシミュレーション](3-cbed-simulation.md) |
| X線 | 平行 | X線回折 | [X線回折シミュレーション](5-x-ray-neutron-diffraction.md) |
| X線 | 歳差（X線） | 歳差X線（プリセッションカメラ） | [X線回折シミュレーション](5-x-ray-neutron-diffraction.md) |
| X線 | Back Laue | 後方反射ラウエ | [X線回折シミュレーション](5-x-ray-neutron-diffraction.md) |
| 中性子 | 平行 | 中性子回折 | [X線回折シミュレーションの中性子節](5-x-ray-neutron-diffraction.md) |

<!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormDiffractionSimulator.cs:WaveLengthControl_WaveSourceChanged, radioButtonBeamParallel_CheckedChanged; FormDiffractionSimulator.ja.resx) -->
> **注記**: 入射ビームの選択肢は波長で変わります。電子線では **平行・歳差 (電子)・収束 (CBED, 電子線のみ)**、X線では **平行・歳差 (X線, ZOLZ)・バックラウエ (X線)**、中性子では **平行** のみです。線源を変えると入射ビームは常に **平行** に戻ります。**歳差 (電子)** または **収束** を選ぶと、強度計算は自動的に **動力学的効果** へ切り替わります。**動力学的効果** は電子線でのみ選べ、X線・中性子にすると **運動学的効果** に戻ります。バックラウエは白色（多色）ビームで計算します。

---

## キーボード・マウスショートカット

<!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormDiffractionSimulator.cs:graphicsBox_MouseWheel / graphicsBox_MouseUp; GraphicsBox.cs:GetWheelZoomFactor) -->
X線・SAED・PED のシミュレーションで共通の回折パターンウィンドウに適用されます。パターン上のドラッグは**結晶**を回転させます。ズームはマウスホイール、または右クリック／右ドラッグで行います。

| ショートカット | 動作 |
|----------------|------|
| <kbd>F1</kbd> | このページのオンラインマニュアルを開く |
| 中心付近を左ドラッグ | 結晶を傾ける |
| 外周を左ドラッグ | ビーム軸まわりに結晶を回す |
| スポットを左ダブルクリック | 反射の詳細を表示（指数・*d*・構造因子・励起誤差） |
| 中ドラッグ | パターンを平行移動 |
| <kbd>CTRL</kbd> ＋ 中ドラッグ | 検出器中心を移動（検出器領域の表示時） |
| マウスホイール | 1 ノッチごとに ×2 でズームイン／アウト。カーソル位置の点が表示の中心に移ります（**中心を固定** 時は固定中心のまま） |
| 右クリック | ズームアウト（×1/1.2） |
| 右ドラッグで矩形選択 | 選択範囲にズームイン |
| ステータスバーを右ダブルクリック | 現在の設定のテキスト要約をコピー |
| 点灯中のレイヤーボタン（Spots / Kikuchi / Debye / Scale）を右ダブルクリック | そのレイヤーを点滅 |

ここから開く補助ウィンドウには、さらに以下があります。

| ショートカット | 動作 |
|----------------|------|
| ステレオネットを左ダブルクリック — **TEMホルダー** | ホルダー傾斜をその点に設定 |
| 矢印キー — **TEMホルダー** | ホルダー傾斜をステップ送り（先に **矢印キーを有効化** をチェック。ReciPro のどのウィンドウが前面でも有効） |
| `.prm` ファイルや画像をドロップ — **検出器ジオメトリ** | 検出器幾何／重畳画像を読込 |
| `.txt` プロファイルをドロップ — **動的圧縮** | 位置・時刻・圧力のプロファイルを読込（**Time** スライダーで時刻を選択、グラフの縦線で前端・後端を設定） |

メインウィンドウのアプリ全体ショートカット（<kbd>CTRL</kbd>+<kbd>SHIFT</kbd> 系）は、このウィンドウにフォーカスがある間も動作します（[メインウィンドウ](../0-main-window.md) 参照）。

→ 全ウィンドウの一覧は **[21. キーボード・マウスショートカット](../21-shortcuts.md)** を参照。

---

## 目的別最速手順

| 目的 | 操作の入口 | 参照ページ |
|------|------------|------------|
| 平行ビームの電子回折（SAED）を出す | **入射ビーム**を **平行**、**波長**を **電子線**にする | [SAEDシミュレーション](1-saed-simulation.md)、[平行ビーム SAED の計算](../appendix/a3-bloch-wave/calculation.md) |
| X線単結晶回折を出す | **波長**を X-ray / Synchrotron へ切り替える | [X線回折シミュレーション](5-x-ray-neutron-diffraction.md) |
| 歳差電子回折（PED）を出す | **入射ビーム**を **歳差 (電子)** にし、半頂角とステップを設定する | [PEDシミュレーション](2-ped-simulation.md) |
| 収束電子線回折（CBED）を出す | **入射ビーム**を **収束 (CBED, 電子線のみ)** にし、CBED設定ウィンドウで条件を決める | [CBEDシミュレーション](3-cbed-simulation.md)、[CBED の計算](../appendix/a3-bloch-wave/cbed.md) |
| 動力学計算の反射一覧を確認する | **動力学的効果**を選び、**スポットの詳細情報** を押す | [動力学計算（共通コア）](../appendix/a3-bloch-wave/calculation.md) |
| 実験像と重ねて検出器幾何を合わせる | **検出器情報** 欄の **詳細** から検出器ジオメトリ設定を開き、重畳画像を使う | [検出器座標系](../appendix/a1-coordinate-system/2-diffraction.md) |

---

## メインエリア

画面中央に回折パターンがシミュレーションされます。

### マウス操作

ページ冒頭の「キーボード・マウスショートカット」を参照してください。

### マウス位置

<!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormDiffractionSimulator.cs:graphicsBox_MouseMove, checkBoxMousePositionDetailes_CheckedChanged) -->
パターン上部の **マウス位置:** 行に、カーソル位置の *d* (nm)・*d*⁻¹ (nm⁻¹)・2θ（度、および rad。0.1 rad 未満は mrad）が表示されます。**詳細** をチェックすると、検出器上の座標（mm、原点は試料から下ろした垂線の足）・実空間座標（mm、原点は結晶）・逆空間座標（nm⁻¹）の 3 つが追加表示されます。バックラウエでは *d* と逆空間座標は「-」表示になります。

---

## ファイルメニュー

![ファイルメニュー](../../assets/cap-ja-auto/FormDiffractionSimulator.toolStripContainer1.menuStrip1.fileToolStripMenuItem.png)

<!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormDiffractionSimulator.Designer.cs:fileToolStripMenuItem; FormDiffractionSimulator.cs:radioButtonIntensityCalculationMethod_CheckedChanged, FormDiffractionSimulatorCBED.cs:FormDiffractionSimulatorCBED_VisibleChanged) -->
（メニュー項目は日本語 UI でも英語表記です）

| メニュー項目 | 説明 |
|-------------|------|
| **Save Image** | 表示中の回折パターンを **as Image (PNG format)** または **as Metafile (EMF)** で保存 |
| **Save detector area** | 検出器領域のクロップのみを保存 (PNG / EMF)。検出器領域の表示時のみ現れます |
| **Save CBED disks** | CBED モードのみ。計算したディスクを個別画像 (PNG / TIFF) または一枚の集合画像 (PNG / TIFF) で保存 |
| **Copy** | 表示中の画像をクリップボードへコピー (**as Image** / **as Metafile (EMF)**) |
| **Copy detector area** | 検出器領域のクロップのみをコピー。検出器領域の表示時のみ現れます |
| **Copy CBED disks** | CBED モードのみ。ディスクを集合画像としてコピー |
| **Save Movie** | **Diffraction pattern**（回折図形）または **Reciprocal space**（逆空間表示）の動画を保存 |
| **Page Setup / Print Preview / Print** | 表示中のパターンを印刷 |

### プリセット

![プリセットメニュー](../../assets/cap-ja-auto/FormDiffractionSimulator.toolStripContainer1.menuStrip1.presetToolStripMenuItem.png)

<!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormDiffractionSimulator.cs:presetToolStripMenuItem_Click; FormDiffractionSimulator.Designer.cs:presetToolStripMenuItem) -->
線源とエネルギーだけを設定する固定のプリセットです: **Electron 300 kV**、**Electron 200 kV**、**Electron 120 keV**、**X-ray 30 keV**、**X-ray 20 keV**、**X-ray 17.48 keV (MoKα₁)**、**X-ray 8.041 keV (CuKα₁)**（最後の 2 つは Mo / Cu の Kα₁ 特性線を選択）。検出器ジオメトリ・タブ・スポット特性などは保存も変更もされません。

### オプション・ヘルプメニュー

| メニュー項目 | 説明 |
|-------------|------|
| **オプション > Dynamic Compression** | [動的圧縮](#動的圧縮) ウィンドウを開きます（検出器ジオメトリのウィンドウも同時に開きます） |
| **オプション > ALCHEMI シミュレータ...** | [ALCHEMI シミュレータ](4-alchemi-simulation.md) を開きます |
| **ヘルプ > Basic concept of Bethe's method** | オンラインマニュアルの [ブロッホ波法の付録](../appendix/a3-bloch-wave/index.md) をブラウザで開きます |

---

## ツールバー

![ツールバー](../../assets/cap-ja-auto/FormDiffractionSimulator.toolStripContainer1.toolStrip3.png)

| ボタン | 説明 |
|--------|------|
| 回折斑点 (Spots) | 回折スポットレイヤーの表示／非表示 |
| 菊池線 (Kikuchi) | 菊池線レイヤーの表示／非表示 |
| デバイリング (Debye) | デバイリングレイヤーの表示／非表示 |
| 目盛線 (Scale) | スケール線レイヤーの表示／非表示 |
| 面指数 / d値 / 1/d / 距離 / 2θ / χ / 励起誤差 / 構造因子 | スポットに添えるラベルの選択 |

---

## 画面と検出器情報

### 画面

![画面](../../assets/cap-ja-auto/FormDiffractionSimulator.toolStripContainer1.flowLayoutPanel6.groupBoxMonitor.png)

<!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormDiffractionSimulator.Designer.cs:numericBoxResolution, comboBoxCenter; FormDiffractionSimulator.cs:FixedCenter, radioButtonResoUnit_CheckedChanged) -->
| 項目 | 説明 |
|------|------|
| **解像度** | 画面 1 ピクセルあたりの大きさ。**mm/px**（検出器面上）または **nm⁻¹/px** を選べます（既定 0.08 mm/px）。実際の検出器ピクセルサイズである必要はなく、表示スケールとして扱われ、マウスズームで自動更新されます |
| **Size (W×H)** | 描画領域のピクセル幅・高さ。ディスプレイ解像度によっては非常に大きな値は設定できません |
| **中心をセット / 中心を固定** | 描画領域の中心を **垂線の足**・**ダイレクトスポット**・**検出器の中心**（検出器領域を設定しているときのみ）のいずれかに合わせます。**中心を固定**（既定でオン）するとその位置に固定され、マウスパンが無効になり、ズームもその中心で行われます |
| **水平反転 / 垂直反転 / ネガティブ画像** | 表示パターンの幾何反転（水平／垂直）とコントラスト反転。実験像の向きやコントラストに合わせるときに使用します |
| **逆空間表示** | パターンの横にエワルド球と逆格子点の 3D 表示を開き、どの反射が励起されているかを可視化します。その **Options** 欄で色（エワルド球に近い／遠いスポット、エワルド球、原点、方向ガイド）、3D のスポット半径 (nm⁻¹)、相対強度のしきい値 (%)、遠いスポットの透明化、ラベル、視点の方向（矢印ボタンと角度 **ステップ**、**リセット**）を設定します |

### 検出器（カメラ長）

![検出器（カメラ長）](../../assets/cap-ja-auto/FormDiffractionSimulator.toolStripContainer1.flowLayoutPanel6.panelDetectorAndMisc.groupBoxDetectorGeometry.png)

- **カメラ長2** : 試料から検出器面へ下ろした垂線の足までの距離 (mm、既定 1000 mm)。検出器が傾いていない (τ = 0) ときは通常のカメラ長に一致します。 <!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormDiffractionSimulator.cs:CameraLength2, DetectorRotation; FormDiffractionSimulator.Designer.cs:numericBoxCameraLength2) -->
- **詳細** : 検出器ジオメトリ設定ウィンドウ（後述の[検出器ジオメトリ](#検出器ジオメトリ)節）を開きます。

### その他

![その他](../../assets/cap-ja-auto/FormDiffractionSimulator.toolStripContainer1.flowLayoutPanel6.panelDetectorAndMisc.groupBoxMisc.png)

- **回転の感度** : マウスドラッグ 1 ピクセルあたりの結晶回転量。
- **TEMホルダーシミュレーション** : ホルダー連動シミュレーションウィンドウを開きます（下記参照）。

---

## TEMホルダーシミュレーション

![TEMホルダーシミュレーション](../../assets/cap-ja-auto/FormDiffractionSimulatorHolder.png)

<!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormDiffractionSimulatorHolder.cs:tiltByArrowKey, buttonLink_Click; FormDiffractionSimulatorHolder.Designer.cs) -->
回折図形をダブルティルトの **TEMホルダー**（**Tilt X** = 主軸、**Tilt Y** = 副軸）と連動させるウィンドウを開きます。ホルダーの傾斜角を設定するとパターンと結晶方位が更新され、到達可能な方位をステレオネット上に表示できます（ver4.914 で追加）。**現在の結晶方位にリンク** は、現在の結晶方位を指定した Tilt X / Tilt Y のときの方位として基準にします。**TEM 設定** では画面上での Tilt X 軸の向き（既定 −29°）と Tilt Y の極性を与え、**↻ 180° 回転** はビーム軸まわりに結晶を 180° 回します。ステレオネットを左ダブルクリックするとホルダー傾斜をその点に設定でき、**矢印キーを有効化** をチェックしておくと ←/→ で Tilt X、↑/↓ で Tilt Y をステップ送りできます（ステップ 0.1〜2°、既定 0.2°）。このウィンドウが開いている間は、ReciPro のどのウィンドウが前面にあっても矢印キーが有効です。

---

## 描画オーバーレイのタブ

### 一般 (General)

![一般タブ](../../assets/cap-ja-auto/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageGeneral.png)

<!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormDiffractionSimulator.Designer.cs:tabPageGeneral (groupBoxStringSize, groupBoxColor)) -->
ラベルの **文字のサイズ** と、**文字色**・**試料から検出器へ下ろした垂線の足** のマーカー・**ダイレクトスポット位置** のマーカー・**背景** の色を設定します（2 つのマーカーはそれぞれ非表示にできます）。スポットの色は **スポットの形状**（各モードページ参照）で、菊池線・デバイリング・目盛り線の色はそれぞれのタブで設定します。ここでの設定はすべての描画モードに反映されます。

### 菊池線 (Kikuchi lines)

![菊池線タブ](../../assets/cap-ja-auto/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageKikuchi.png)

![菊池線タブ (Dynamical モード)](../../assets/cap-ja-auto/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageKikuchi-dynamical.png)

ツールバーで菊池線が有効のときにアクティブになります。

- **反射の選択** : 描画する菊池線の元となる反射を選びます。**結晶構造因子**（$\lvert F_{hkl}\rvert$ の **トップ** *N* 本。1〜1000、既定 100）または **1/d の閾値**（1/d がしきい値以下のすべての反射。0〜100 nm⁻¹、既定 10）。 <!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormDiffractionSimulator.Designer.cs:numericBoxKikuchiThresholdOfStructureFactor/OfLength; FormDiffractionSimulator.cs:コンストラクタ (comboBoxKikuchiMode/Quality/Scale.SelectedIndex)) -->
- **モード (Mode)** : 菊池線の描画方法を選びます（既定は **運動学的**）。コンボボックスの下の設定はモードに応じて切り替わります。
    - **幾何学的 (Geometric)** : 選択した線をすべて同じ色で描画します。
    - **運動学的 (Kinematical)** : 反射の運動学的相対強度に従って線の濃さを変えます。
    - **動力学的 (Dynamical)** : 熱散漫散乱を源とする 2 波動力学の excess/deficient バンドプロファイルをバックグラウンドで計算して描画します。excess バンドは高角側に、deficient は直接ビーム (000) 側に現れます。試料厚みは強度設定の厚みと共有です。計算中は幾何学的な線が代わりに表示されます。
- **幾何学的 / 運動学的 の設定** : **菊池線の色** と **線の太さ**。
- **Dynamical の設定** :
    - **Excess** / **Deficient** : バンドの明るい側 (コントラストが正) と暗い側 (負) の色。
    - **本数 (Bands)** : 動力学プロファイルで描画するバンド数（動力学コントラスト上位 *N* 本。1〜50、既定 30）。
    - **品質 (Quality)** : バンドを横断するサンプル点数（高速 65 / 標準 129 / 高品質 257。既定は標準）。
    - **コントラスト (Contrast)** : 圧縮前にバンドコントラストへ掛ける表示用の利得。
    - **スケール (Scale)** : 明るさの圧縮カーブ（Linear / Log / Tanh。既定は Tanh）。いずれも表示用の変換で、実機カメラの応答ではありません。
    - **明るさを固定 (Lock brightness)** : 明るさの正規化を固定します（方位や厚みを変えて明るさを比較するときに）。
    - 下の灰色の注記には、源と吸収の非整合タグ・描画バンド数・診断（*B* = 0 の原子に既定値を代用した等）が表示されます。
- （かつての独立した **しきい値** パラメータは現在はありません。反射は上記の選択だけで絞り込まれます。） <!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormDiffractionSimulator.Designer.cs:tabPageKikuchi (旧 Threshold コントロールは存在しない)) -->

### デバイリング (Debye rings)

![デバイリングタブ](../../assets/cap-ja-auto/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageDebye.png)

ツールバーでデバイリングが有効のときにアクティブになります。

- **強度を無視する** : チェック時はすべてのデバイリングを同じ色・強度で描画します（結晶構造因子を無視）。純粋に幾何的な比較をしたいときに使います。
- **指数を表示する** : チェック時、各リングの近傍に (*hkl*) を表示します。

### 目盛り線 (Scale)

![目盛り線タブ](../../assets/cap-ja-auto/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageScale.png)

ツールバーで目盛り線が有効のときにアクティブになります。

- **2θ / 方位角 目盛り線** : **2θ** は等散乱角（同心円）、**方位角** は等方位角（中心からの放射状直線）を表します。色はそれぞれ独立に設定できます。
- **線の太さ** : 目盛り線の太さ。
- **分割数** : 目盛り線の密度。**細かい** / **普通**（既定）/ **粗い**。 <!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormDiffractionSimulator.Designer.cs:radioButtonScaleDivisionFine/Medium/Coarse) -->
- **目盛りラベルを表示** : 目盛り線に数値ラベルを表示するか。

<!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormDiffractionSimulator.Designer.cs:tabControl (タブは 一般/菊池線/デバイリング/目盛り線 の 4 枚のみ)) -->
（独立した「その他」タブはありません。マウスの **回転の感度** は右側パネルの **その他** グループにあります。上の [その他](#その他) を参照。）

---

## 回折スポット情報

<!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormDiffractionSimulator.cs:ButtonDetailsOfSpots_Click; FormDiffractionSpotInfo.cs:SetTable(double, Crystal)) -->
ブロッホ波法（Dynamical 計算）で計算された各反射の詳細を一覧表示します。強度計算パネルの **スポットの詳細情報** ボタンで開きます。**励起誤差のみ考慮** / **運動学的効果** のときは、現在描画されている反射について電子線のポテンシャルと $P_g$・$Q_g$ を一覧します（動力学的振幅は無し）。CBED モードでは CBED ウィンドウで選んだ厚さに追随します。

![回折スポット情報](../../assets/cap-ja-auto/FormDiffractionSpotInfo.png)

### 模式図と定義

左上の模式図は、エワルド球上のベクトルと、表で使う量の定義を示します（$\hat{\mathbf{n}}$ は試料表面の法線方向の単位ベクトル、$\mathbf{k}$ は入射波数ベクトル、$\mathbf{g}$ は逆格子ベクトル）。

- $P_g = 2\,\hat{\mathbf{n}} \cdot (\mathbf{k} + \mathbf{g})$
- $Q_g = |\mathbf{k}|^2 - |\mathbf{k} + \mathbf{g}|^2 = -\mathbf{g} \cdot (2\mathbf{k} + \mathbf{g})$
- **励起誤差:** $S_g = \dfrac{\sqrt{P_g^2 + 4 Q_g} - P_g}{2}$
- **評価関数:** $R = |\mathbf{g}|\, Q_g^2$ — 反射を励起の強さで順位付けする量（小さいほどエワルド球に近く＝強く励起される。透過波 $g=0$ は $R=0$ で先頭）。表は $R$ の昇順に並びます。

### 表の各列

| 列 | 意味 |
|------|------|
| **R** | 評価関数 $R = \lvert\mathbf{g}\rvert\, Q_g^2$（上記。反射の選択・並べ替えに使用） |
| **h, k, (i,) l** | ミラー指数（*i* は六方晶の冗長指数で、六方晶のときのみ） |
| **d** | 面間隔（nm） |
| **gX, gY, gZ** | 逆格子ベクトル *g* の成分（1/nm） |
| **\|g\|** | *g* の大きさ（1/nm） |
| **Vg re / Vg im** | 弾性散乱に対する結晶ポテンシャルのフーリエ係数 $V_g$（実部・虚部） |
| **V'g re / V'g im** | 熱散漫散乱（TDS）に対応する虚（吸収）ポテンシャル $V'_g$（実部・虚部） |
| **Sg** | 励起誤差 $S_g$（上記。1/nm） |
| **Pg** | 補助量 $P_g = 2\,\hat{\mathbf{n}}\cdot(\mathbf{k}+\mathbf{g})$（上記） |
| **Qg** | 補助量 $Q_g = -\mathbf{g}\cdot(2\mathbf{k}+\mathbf{g})$（上記） |
| **Φ re / Φ im** | 出射面における動力学的回折波の複素振幅 $\Phi$（実部・虚部） |
| **\|Φ\|^2** | その反射の回折強度 $\lvert\Phi\rvert^2$ |
| **Σ\|Φ\|^2** | $\lvert\Phi\rvert^2$ の累積和（全反射の和。強度保存の確認に使える） |

### ポテンシャルの単位とその他のコントロール

- **Unit of potential** : ポテンシャルの表示単位を **Vg [eV]**（電位、eV）と **Ug [nm⁻²]**（ブロッホ波方程式に入る換算量 $U_g = (2 m_0/h^2)\, V_g$）で切り替えます。単位に応じて表の列見出しも *Vg / V'g* ↔ *Ug / U'g* に変わります。
- 表上部に、加速電圧・波長（$\lambda = 1/k_\text{vac}$）・相対論的質量比 $m/m_0$・速度比 $v/c$・格子体積・試料厚さ・（CBEDモードの）電子線の最大半角が表示されます。
- **Note 1:** 長さの単位は Å ではなく **nm**。**Note 2:** 波数の単位は 2π/nm ではなく **1/nm**。
- **Effective digit** : 表に表示する有効桁数（1〜16、既定 4）。**Auto resize row width** : 列幅の自動調整。**Copy to clipboard** : 表を表計算ソフトへ貼り付け可能なテキストとして出力します。**Save as CSV** : 同じ表（表示中の列）を CSV ファイルに保存します。小数点は常にピリオドなので、小数点がカンマのロケールでもファイルが壊れません。（このフォームは日本語UIでも英語表示です）。

---

## 検出器ジオメトリ

検出器の幾何（カメラ長・傾き・回転）と、実験像の重畳を詳細に設定するウィンドウです。**検出器ジオメトリ**パネルの **詳細** から開きます。

![検出器ジオメトリ](../../assets/cap-ja-auto/FormDiffractionSimulatorGeometry.png)

### 検出器ジオメトリ設定

![検出器ジオメトリ設定](../../assets/cap-ja-auto/FormDiffractionSimulatorGeometry.panelDetectorGeometry.png)

<!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormDiffractionSimulatorGeometry.Designer.cs; FormDiffractionSimulatorGeometry.cs:FormDiffractionSimulatorGeometry_DragDrop) -->
**Camera length 2**（mm、1〜10000、既定 1000）と、方位 **φ** の面内軸まわりの検出器の傾き **τ**（−200〜200°）を与えます（このウィンドウは日本語 UI でも英語表記）。Back Laue（後方反射ラウエ）では **Camera length 2** だけが使われます。パターンは常に線源側でビームに垂直な検出器へ投影され、τ / φ はこの投影に使われません。`.prm` ファイルをドロップすると、波長・カメラ長2・τ・φ・垂線の足の位置・ピクセルサイズを読み込みます（線源は X線（任意波長）に切り替わり、検出器領域の表示もオンになります）。

### 検出器領域と重畳画像

![検出器領域と重畳画像](../../assets/cap-ja-auto/FormDiffractionSimulatorGeometry.panelDetectorAreaAndOverlappedImage.png)

**Set detector area & overlapped image.** をチェックすると検出器を指定できます: 画素数（既定 1024 × 1024）、**pix. size**（mm、既定 0.1）、垂線の足の位置 **Foot: fx, fy**（px、既定 512, 512）。実験像は **Load** またはドロップで読み込み、**Opacity**・**Gradient**（ポジ／ネガ）・**Scale 1**（Log/Linear）・**Scale 2**（Gray / Cold-Warm / Spectrum / Fire）・**Max/Min int.**・**Rot 90°**・**Clear** で重ね方を調整します。**Schematic diagram** は幾何の模式図を表示します。シミュレーションパターンと実験像を重ねて検出器幾何を合わせ込むのに使います。

座標系の定義は [検出器座標系](../appendix/a1-coordinate-system/2-diffraction.md) も参照してください。

---

## 動的圧縮

![動的圧縮](../../assets/cap-ja-auto/FormDiffractionSimulatorDynamicCompression.png)

<!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormDiffractionSimulatorDynamicCompression.cs:FormDiffractionSimulatorDynamicCompression_DragDrop, buttonPaste_Click, graphControl1_LinePositionChanged) -->
衝撃圧縮（動的圧縮）された試料からの回折をシミュレーションするウィンドウです（日本語 UI でも英語表記）。位置・時刻・圧力の 3 列をタブ区切りで並べた `.txt` をドロップすると時刻ごとの圧力プロファイルを読み込み、**Time** スライダー (ns) でグラフに表示するプロファイルを選びます。グラフ上の 2 本の縦線（**Front** / **Back**）はドラッグで圧縮の前端・後端を設定します。このほか圧縮モデル（**2018 / 2019 model**）、衝撃速度（**Us**、**Ur**、km/s）、**EOS**（K0、K'0）、**Shocked plane** / **Slip plane**、圧縮域・解放域の回転分布、質量吸収係数、Ω のステップ送り、**Save patterns**（`#.tif`）を設定し、**Simulate** で実行します。

---

## ALCHEMI シミュレータ

**サイト分解のイオン化ロッキングカーブ**（系統反射列の Bragg 条件を通して結晶を傾けたときの、選んだイオン化チャネルの収量をサイトごとに分けたもの）を計算する子ウィンドウを開きます。**オプション** メニューから開きます。CBED ウィンドウと違い、自前の出力領域を持ち、親のキャンバスには描きません。

→ 全設定と適用範囲・既知の限界は **[ALCHEMI シミュレーション](4-alchemi-simulation.md)** を参照。

---

## 関連項目

- [X線回折シミュレーション](5-x-ray-neutron-diffraction.md)
- [SAEDシミュレーション](1-saed-simulation.md)
- [PEDシミュレーション](2-ped-simulation.md)
- [CBEDシミュレーション](3-cbed-simulation.md)
- [ALCHEMI シミュレーション](4-alchemi-simulation.md)
- [動力学計算（共通コア）](../appendix/a3-bloch-wave/calculation.md)
- [検出器座標系](../appendix/a1-coordinate-system/2-diffraction.md)
