---
title: 回折シミュレータ
---

# 回折シミュレータ (Crystal Diffraction)

**Crystal Diffraction** は、単結晶X線回折および電子線回折パターンをシミュレーションします。

![回折シミュレータ](../../assets/cap-ja-auto/FormDiffractionSimulator.png)

---

## 目的別最速手順

| 目的 | 操作の入口 | 参照ページ |
|------|------------|------------|
| 平行ビームの電子回折（SAED）を出す | **入射ビーム**を **平行**、**波長**を **電子線**にする | [SAEDシミュレーション](2-saed-simulation.md)、[平行ビーム SAED の計算](../appendix/a2-bloch-wave/calculation.md#parallel-beam-saed) |
| X線単結晶回折を出す | **波長**を X-ray / Synchrotron へ切り替える | [X線回折シミュレーション](1-x-ray-diffraction.md) |
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

### マウス位置

カーソル位置に対応する情報 (カーソル位置の *q*, *d*, 2θ, 方位角など) がパターン上部のステータス行に表示されます。**詳細** をチェックすると、最近接反射の (*hkl*)・励起誤差・構造因子などのより詳細な情報が追加表示されます。

---

## ファイルメニュー

| メニュー項目 | 説明 |
|-------------|------|
| **保存** | 表示中の回折パターンをファイルに保存 |
| **検出器領域を保存** | 検出器領域のクロップのみを保存 |
| **コピー** | 表示中の画像をクリップボードへコピー |
| **検出器領域をコピー** | 検出器領域のクロップのみをコピー |

### プリセット

波長・検出器ジオメトリ・タブ設定・スポット特性などのシミュレータ設定一式をプリセットとして保存・呼び出しします。装置・取得モード間で素早く切り替えるのに便利です。


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

#### 解像度 (Resolution)

1 ピクセルあたりのサイズ (mm)。実際の検出器ピクセルサイズである必要はなく、表示スケールとして扱われます — マウスでズームすると自動的に更新されます。

#### サイズ (W × H)

描画領域のピクセル幅・高さ。ディスプレイ解像度によっては非常に大きな値は設定できません。

#### 中心をセット / 中心を固定

パターン中心を描画領域内の任意のピクセルに設定し、必要に応じて固定します。固定するとマウスパンで中心が動かなくなります。

#### 水平反転 / 垂直反転 / ネガティブ画像

表示パターンの幾何反転 (水平/垂直) およびコントラスト反転。実験像の向きやコントラストに合わせるときに使用します。

#### 逆空間表示

パターン上にエワルド球と逆格子ベクトルを重ねて描画し、どの反射が励起されているかを可視化します。

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

スポット、ラベル、菊池線、デバイリング、その他オーバーレイの色を設定します。ここでの設定はすべての描画モードに反映されます。

### 菊池線 (Kikuchi lines)

![菊池線タブ](../../assets/cap-ja-auto/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageKikuchi.png)

ツールバーで菊池線が有効の場合にアクティブ。

#### 反射の選択

描画する菊池線の元となる反射を以下から選びます。

- **結晶構造因子** — |*F*ₕₖₗ| の上位 *N* 本
- **1/d 閾値** — 1/d がしきい値 (nm⁻¹) 以下のすべての反射

#### 線の表現

線の太さ、菊池線の色、**運動学的回折強度に従って描画** (反射の運動学的回折強度で線の濃さを変える) を設定。

#### しきい値

旧来パラメータ。指定した *d* より大きい反射に対してのみ菊池線計算を実行 (互換のため残置)。

### デバイリング (Debye rings)

![デバイリングタブ](../../assets/cap-ja-auto/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageDebye.png)

ツールバーでデバイリングが有効の場合にアクティブ。

#### 回折強度を無視

チェック時はすべてのデバイリングを同じ色・強度で描画 (結晶構造因子を無視)。純粋に幾何的な比較がしたいときに使用。

#### 指数ラベルを表示

チェック時、各リングの近傍に (*hkl*) を表示。

### 目盛り線 (Scale)

![目盛り線タブ](../../assets/cap-ja-auto/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageScale.png)

ツールバーで目盛り線が有効の場合にアクティブ。

#### 2θ / 方位角 目盛り線

**2θ** は等散乱角 (同心円)、**方位角** は等方位角 (中心からの放射状直線) を表します。色はそれぞれ独立に設定可能。

#### 線の太さ / 分割 / 目盛りラベル

- **線の太さ**: 目盛り線の太さ
- **分割**: 隣接する目盛り線の角度間隔
- **目盛りラベルを表示**: 目盛り線に数値ラベルを表示するか

### その他 (Misc)

マウス回転感度などの細々した設定。

#### マウス感度

マウスドラッグ 1 ピクセルあたりの結晶回転量。

---

## スポット特性 (Spot property)

ツールバーで **回折斑点** が有効の場合にアクティブ。

### 波長

![波長](../../assets/cap-ja-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelWaveLength.png)

X線 (特性X線/シンクロトロン)・電子線・中性子線を選択し、エネルギーまたは波長を設定します。

#### X線

X線を線源として指定。特性X線の場合は**元素**と**遷移**を Siegbahn 記法 (Kα₁ / Kα₂ / Kβ など) で設定。シンクロトロンX線の場合は**元素**を **0 (None)** にしてエネルギーまたは波長を直接入力します。

#### 電子線

電子線のエネルギー (keV) または波長 (nm) を入力。相対論的補正付きの波長が計算されます。

#### 中性子線

中性子線のエネルギー (meV) または波長 (nm) を入力。

### 入射ビーム

![入射ビームモード](../../assets/cap-ja-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelBeamMode.png)

入射ビームのジオメトリを選択します。

#### 平行

平行入射ビーム — SAED や X 線回折で用いる標準的な平面波ジオメトリ。

#### 歳差 (電子)

歳差電子回折 (PED) をシミュレート。電子線選択時のみ有効。選択すると**強度計算**が自動的に**動力学的効果**に切り替わります。

#### 収束 (CBED, 電子線のみ)

収束電子線 (CBED) をシミュレート。電子線選択時のみ有効。選択すると**強度計算**が自動的に**動力学的効果**に切り替わり、[CBED設定](4-cbed-simulation.md)ウィンドウが開きます。

### 強度計算

![強度計算](../../assets/cap-ja-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelIntensity.png)

スポット強度の計算方法を選択します。

#### 励起誤差のみ考慮

エワルド球と逆格子点の幾何学的距離 (励起誤差 $s_g$) に基づいて強度を決定します。$|s_g|$ が小さいほど強度が大きく、**Radius** で設定した値で最大となり、$|s_g|$ が Radius を超えると 0 になります。結晶構造因子は無視されます。

#### 運動学的効果

励起誤差に加えて運動学的構造因子 |*F*ₕₖₗ|² を強度に反映します。

#### 動力学的効果

ブロッホ波法による動力学的効果を強度に反映します。多重散乱を含む正確な強度が得られます。電子線選択時のみ有効。詳細は[Appendix A2. Bloch波法](../appendix/a2-bloch-wave/calculation.md)を参照。

### 外観

![外観](../../assets/cap-ja-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelAppearance.png)

各回折スポットの描画方法を制御します。

#### Solid sphere / Gaussian

逆格子点の幾何モデルを選択します。

- **Solid sphere**: 逆格子点を半径 *R* (**Radius**) の球としてモデル化。球とエワルド球の断面が回折スポットとして描画され、その円の面積が回折強度に対応します。
- **Gaussian**: 逆格子点を σ = *R* の 3D ガウス関数としてモデル化し、エワルド球との断面 (2D ガウス) を描画。2D ガウスの積分が回折強度に対応します。

#### 不透明度 (Opacity)

スポットの透過率 (0=透過、1=不透過)。

#### Radius (R)

逆格子点の半径。**外観**モードと**強度計算**の組み合わせで描画スポットサイズが決まります。

- **Gaussian + 励起誤差のみ** — σ = *R*、積分 = Brightness。構造因子は無視。
- **Gaussian + 運動学的効果** — σ = *R*、積分 = Brightness × *I*ₖᵢₙ
- **Gaussian + 動力学的効果** — σ = *R*、積分 = Brightness × *I*ₐᵧₙ
- **Solid sphere + 励起誤差のみ** — 半径 *R* の球。構造因子は無視
- **Solid sphere + 運動学的効果** — 半径 *R* × *I*ₖᵢₙ^(1/3)
- **Solid sphere + 動力学的効果** — 半径 *R* × *I*ₐᵧₙ^(1/2)。スポット面積が動力学的強度に比例

#### Brightness

**Gaussian** モードでのみ有効。描画ガウスの積分強度を設定。

#### カラースケール

**Gray scale** または **Cold-warm** カラーマップを選択。

#### Log スケール

強度を対数表示。

#### スポットの色

カラースケールが適用されない場合のデフォルトスポット色。

### ブロッホ波設定（動力学理論）

![ブロッホ波パラメータ](../../assets/cap-ja-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelBethe.png)

**動力学的効果**選択時にアクティブ。

#### 回折波の数

固有値問題に含めるブロッホ波の本数。値を大きくすると強度はより正確になりますが、計算時間は *O*(*N*³) で増加します。

#### 試料厚み

動力学計算で用いる試料厚さ。

### プリセッション設定 (電子線のみ)

![歳差パラメータ](../../assets/cap-ja-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelPED.png)

**歳差 (電子)** 選択時にアクティブ。

#### 半頂角

歳差コーンの半角 (mrad)。

#### ステップ

歳差コーン上でサンプリングする平行ビーム方向の数。PED パターンはこれらの平行ビーム動力学計算の和として得られます。ステップ数を増やすと積分が滑らかになりますが、計算時間が比例して増えます。

---

## 検出器ジオメトリ（詳細）

### 検出器ジオメトリ設定

![検出器ジオメトリ設定](../../assets/cap-ja-auto/FormDiffractionSimulatorGeometry.panelDetectorGeometry.png)

### 検出器領域と重畳画像

![検出器領域と重畳画像](../../assets/cap-ja-auto/FormDiffractionSimulatorGeometry.panelDetectorAreaAndOverlappedImage.png)

[検出器座標系](../appendix/a1-coordinate-system/2-diffraction.md) も参照。

---

## 回折スポット情報

ブロッホ波法で計算された各反射の詳細を一覧表示します。**スポットの詳細情報**ボタン（強度計算パネル）または**詳細**チェックボックスで開きます。

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
- **Effective digit** : 表に表示する有効桁数。**Auto resize row width** : 列幅の自動調整。**Copy to clipboard** : 表を表計算ソフトへ貼り付け可能なテキストとして出力します（このフォームは日本語UIでも英語表示です）。
