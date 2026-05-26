---
title: 回折シミュレータ
---

# 回折シミュレータ (Crystal Diffraction)

**Crystal Diffraction** は、単結晶X線回折および電子線回折パターンをシミュレーションします。

![回折シミュレータ](../../assets/cap-ja-auto/FormDiffractionSimulator.png)

---

## 目的別クイックルート

| 目的 | 操作の入口 | 参照ページ |
|------|------------|------------|
| 平行ビームの電子回折（SAED）を出す | **入射ビーム**を **平行**、**波長**を **電子線**にする | [SAEDシミュレーション](1-saed-simulation.md)、[平行ビーム SAED の計算](../appendix/a2-bloch-wave/calculation.md#parallel-beam-saed) |
| X線単結晶回折を出す | **波長**を X-ray / Synchrotron へ切り替える | [X線回折シミュレーション](2-x-ray-diffraction.md) |
| 歳差電子回折（PED）を出す | **入射ビーム**を **歳差 (電子)** にし、半頂角とステップを設定する | [PEDシミュレーション](3-ped-simulation.md) |
| 収束電子線回折（CBED）を出す | **入射ビーム**を **収束 (CBED, 電子線のみ)** にし、CBED設定ウィンドウで条件を決める | [CBEDシミュレーション](4-cbed-simulation.md)、[CBED の計算](../appendix/a2-bloch-wave/cbed.md) |
| 動力学計算の反射一覧を確認する | **動力学的効果**を選び、**スポットの詳細情報**または **詳細**を開く | [動力学計算（共通コア）](../appendix/a2-bloch-wave/calculation.md) |
| 実験像と重ねて検出器幾何を合わせる | **詳細**から検出器ジオメトリ設定を開き、重畳画像を使う | [検出器座標系](../appendix/a1-coordinate-system/2-diffraction.md) |

---

## メインエリア

画面中央に回折パターンがシミュレーションされます。

### マウス操作

| 操作 | 動作 |
|------|------|
| 左ドラッグ | 回転 |
| 中ドラッグ | 平行移動 |
| 右ドラッグ | ズームイン |
| 右クリック | ズームアウト |
| 左ダブルクリック | 選択スポットの詳細情報表示 |

**マウス位置**: カーソル位置に対応する情報が表示されます。**詳細** をチェックするとより詳細な情報を表示。

---

## ファイルメニュー


| メニュー項目 | 説明 |
|-------------|------|
| Save / Save detector area | 表示画像/検出器領域画像を保存 |
| Copy / Copy detector area | 表示画像/検出器領域画像をコピー |

### プリセット


---

## ツールバー

![ツールバー](../../assets/cap-ja-auto/FormDiffractionSimulator.toolStripContainer1.toolStrip3.png)

| ボタン | 説明 |
|--------|------|
| 回折斑点 | 回折スポットの表示/非表示 |
| 菊池線 | 菊池線の表示/非表示 |
| デバイリング | デバイリングの表示/非表示 |
| 目盛線 | スケール線の表示/非表示 |
| 面指数 / d値 / 距離 / 励起誤差 / 構造因子 | スポットラベルの選択 |

---

## 画面 / 検出器情報

### 画面

![画面](../../assets/cap-ja-auto/FormDiffractionSimulator.toolStripContainer1.flowLayoutPanel6.groupBoxMonitor.png)

- **解像度**: 1ピクセルのサイズ (mm)。表示スケールのパラメータであり、マウスズームで変化。
- **Size (W×H)**: 描画領域のピクセル数 (幅×高さ)。
- **中心をセット / 中心を固定**: パターン中心の設定・固定。
- **水平反転 / 垂直反転 / ネガティブ画像**: パターンの反転・白黒反転。
- **逆空間表示**: エワルド球と逆格子ベクトルを描画。

### その他

![その他](../../assets/cap-ja-auto/FormDiffractionSimulator.toolStripContainer1.flowLayoutPanel6.panelDetectorAndMisc.groupBoxMisc.png)

- **回転の感度**: マウスドラッグ時の回転量。
- **TEM ホルダーシミュレーション**: ホルダー連動シミュレーションウィンドウを開く（下記参照）。

### TEM ホルダーシミュレーション

回折図形をダブルティルト（または回転）の **TEMホルダー** と連動させるウィンドウを開きます。ホルダーの傾斜角を設定するとパターンと結晶方位が更新され、到達可能な方位をステレオネット上に表示できます（ver4.914 で追加）。

![TEMホルダーシミュレーション](../../assets/cap-ja-auto/FormDiffractionSimulatorHolder.png)

### 検出器情報

![検出器情報](../../assets/cap-ja-auto/FormDiffractionSimulator.toolStripContainer1.flowLayoutPanel6.panelDetectorAndMisc.groupBoxDetectorGeometry.png)

- **カメラ長2**: 試料から検出器までの距離 (mm)。
- **詳細**: 光学系設定ウィンドウを開く。

---

## タブメニュー

### 一般 (General)

![一般タブ](../../assets/cap-ja-auto/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageGeneral.png)

スポット、ラベル、菊池線等の色を設定。

### 菊池線 (Kikuchi lines)

![菊池線タブ](../../assets/cap-ja-auto/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageKikuchi.png)

ツールバーで菊池線が有効の場合にアクティブ。描画する菊池線の選定基準を **結晶構造因子**（**トップ** N 本）または **1/d の閾値**（< X nm⁻¹）で指定します。**線の太さ**・**菊池線の色**、**運動学的な回折強度に従って描画する** も設定できます。

### デバイリング (Debye rings)

![デバイリングタブ](../../assets/cap-ja-auto/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageDebye.png)

デバイリングが有効の場合にアクティブ。

### 目盛り線 (Scale)

![目盛り線タブ](../../assets/cap-ja-auto/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageScale.png)

---

## スポット特性 (Spot property)

ツールバーで **回折斑点** が有効の場合にアクティブ。

### 波長

![波長](../../assets/cap-ja-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelWaveLength.png)

### 入射ビーム

![入射ビームモード](../../assets/cap-ja-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelBeamMode.png)

| モード | 説明 |
|--------|------|
| **平行** | 平行入射ビーム |
| **歳差 (電子)** | 歳差電子回折 (PED)。自動的に動力学理論に設定 |
| **収束 (CBED, 電子線のみ)** | 収束ビーム (CBED)。自動的に動力学理論に設定、CBED設定ウィンドウが開く |

### 強度計算

![強度計算](../../assets/cap-ja-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelIntensity.png)

| 方法 | 説明 |
|------|------|
| **励起誤差のみ考慮** | エワルド球と逆格子点の幾何学的距離に基づく強度 |
| **運動学的効果** | 励起誤差に加え結晶構造因子を反映 |
| **動力学的効果** | ブロッホ波法（電子線のみ） |

### 外観

![外観](../../assets/cap-ja-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelAppearance.png)

### ブロッホ波パラメータ（動力学理論）

![ブロッホ波パラメータ](../../assets/cap-ja-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelBethe.png)

- **回折波の数**: 動力学計算に含めるブロッホ波の数
- **試料厚み**: 試料の厚さ

### 歳差パラメータ

![歳差パラメータ](../../assets/cap-ja-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelPED.png)

- **半頂角**: 歳差の半角
- **ステップ**: サンプリングするビーム方向数

---

## 検出器ジオメトリ（詳細）

### 検出器ジオメトリ設定

![検出器ジオメトリ設定](../../assets/cap-ja-auto/FormDiffractionSimulatorGeometry.panelDetectorGeometry.png)

### 検出器領域と重畳画像

![検出器領域と重畳画像](../../assets/cap-ja-auto/FormDiffractionSimulatorGeometry.panelDetectorAreaAndOverlappedImage.png)

[検出器座標系](../appendix/a1-coordinate-system/2-diffraction.md) も参照。

---

## 回折スポット情報

ベーテの動力学理論（ブロッホ波法）で計算された各反射の詳細を一覧表示します。**スポットの詳細情報**ボタン（強度計算パネル）または**詳細**チェックボックスで開きます。

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

- **Unit of potential** — ポテンシャルの表示単位を **Vg [eV]**（電位、eV）と **Ug [nm⁻²]**（ブロッホ波方程式に入る換算量 $U_g = (2 m_0/h^2)\, V_g$）で切り替えます。単位に応じて表の列見出しも *Vg / V'g* ↔ *Ug / U'g* に変わります。
- 表上部に、加速電圧・波長（$\lambda = 1/k_\text{vac}$）・相対論的質量比 $m/m_0$・速度比 $v/c$・格子体積・試料厚さ・（CBEDモードの）電子線の最大半角が表示されます。
- **Note 1:** 長さの単位は Å ではなく **nm**。**Note 2:** 波数の単位は 2π/nm ではなく **1/nm**。
- **Effective digit** — 表に表示する有効桁数。**Auto resize row width** — 列幅の自動調整。**Copy to clipboard** — 表を表計算ソフトへ貼り付け可能なテキストとして出力します（このフォームは日本語UIでも英語表示です）。
