<!-- nav -->

[← 8. 電子飛程](13-electron-trajectory.md)  |  [🏠 ホーム](index.md)  |  [9.1. HRTEMシミュレーション →](8-1-hrtem-simulation.md)

# HRTEM / STEMシミュレータ

**HRTEM/STEM シミュレータ** は、選択した結晶と方位に対するTEM格子縞像（HRTEM）・STEM像・投影ポテンシャルをシミュレーションします。**シミュレート** ボタンで実行します。

![HRTEM/STEMシミュレータ](../assets/cap-ja-auto/FormImageSimulator.png)

---

## ファイルメニュー


### ヘルプメニュー


---

## 画像モード / 試料特性

![画像モード](../assets/cap-ja-auto/FormImageSimulator.splitContainer1.flowLayoutPanelModeSelection.groupBoxImageMode.png)

**HRTEM**、**ポテンシャル**、**STEM** モードから選択。

![試料特性](../assets/cap-ja-auto/FormImageSimulator.splitContainer1.flowLayoutPanelModeSelection.groupBoxSampleProperty.png)

---

## 光学特性 (Optical property)

### TEM条件

![TEM条件](../assets/cap-ja-auto/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxTEMConditions.png)

- **加速電圧 (kV)**: 加速電圧。相対論補正波長が右に表示。
- **デフォーカス Δf**: デフォーカス値。シェルツァーデフォーカスが下に表示。

### HRTEM固有パラメータ

![HRTEM固有パラメータ](../assets/cap-ja-auto/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxHREMoption1.png)

Cs, Cc, beta, delta-E。レンズ関数（PCTF、空間・時間コヒーレンスエンベロープ）。対物絞り。

### STEM固有パラメータ

![STEM固有パラメータ](../assets/cap-ja-auto/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxSTEMoption1.png)

---

## シミュレーション特性

### HRTEMシミュレーションオプション

![HRTEMオプション](../assets/cap-ja-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.groupBoxHREMoption2.png)

- **Max Bloch waves**: 計算に使用するブロッホ波の最大数
- 部分コヒーレンスモデル: 準コヒーレント（高速）またはTCC（正確）
- Single / Serial モード

### STEMシミュレーションオプション

![STEMオプション](../assets/cap-ja-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.groupBoxSTEMoption2.png)

### ポテンシャルオプション

![ポテンシャルオプション](../assets/cap-ja-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.groupBoxPotentialOption.png)

### 画像特性

![画像特性](../assets/cap-ja-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.panelImageProperties.groupBoxImageProperty.png)

### 回折波

![回折波](../assets/cap-ja-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.panelImageProperties.groupBoxDiffractedWaves.png)

---

## シミュレーション実行

![シミュレーション実行](../assets/cap-ja-auto/FormImageSimulator.splitContainer1.panelSimulationActions.png)

---

## 表示設定

### 調整

![調整](../assets/cap-ja-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxAdjust.png)

### 正規化

![正規化](../assets/cap-ja-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxNormalization.png)

### 表示オプション

![表示オプション](../assets/cap-ja-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxDisplay.png)

ラベル（厚さ・デフォーカス）、スケールバー、単位格子グリッドの設定。

### STEM表示オプション

![STEM表示オプション](../assets/cap-ja-auto/FormImageSimulator.splitContainer1.panelDisplaySettings.groupBoxSTEMoption3.png)

---

## STEMシミュレーション

| 検出器 | 範囲 | 主な寄与 |
|--------|------|---------|
| BF, ABF | 収束角内 | 弾性散乱 |
| LAADF, HAADF | 収束角外 | 非弾性散乱 (TDS) |

> **重要**: 非弾性散乱強度の計算には、原子の温度因子をゼロ以外に設定する必要があります。不明な場合は B = 0.5 Å² に設定してください。

![STEMシミュレーション比較: Dr. Probe vs ReciPro](../assets/references/STEM_DrProbe_comparison.png)

より詳細な比較は PDF で参照できます: [Dr. Probe GUI (v.1.10) と ReciPro (v.4.854) のSTEMシミュレーション比較](https://github.com/seto77/ReciPro/files/10976084/ComparisonSTEMsimulations.pdf)

詳細は [STEMシミュレーション](8-2-stem-simulation.md) を参照してください。

---

[← 8. 電子飛程](13-electron-trajectory.md)  |  [🏠 ホーム](index.md)  |  [9.1. HRTEMシミュレーション →](8-1-hrtem-simulation.md)
