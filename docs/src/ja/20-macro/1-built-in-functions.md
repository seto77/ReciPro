# 組み込み関数一覧

ReciProマクロで使用可能な組み込みクラスと関数の一覧です。

---

## File クラス

ファイルの読み書きを行います。

| 関数 | 説明 |
|------|------|
| `File.GetDirectoryPath()` | フォルダ選択ダイアログを表示し、選択されたフォルダのパスを返す |
| `File.GetFileName()` | ファイル選択ダイアログを表示し、選択されたファイルのパスを返す |
| `File.GetFileNames()` | 複数ファイル選択ダイアログを表示し、選択されたファイルパスのリストを返す |
| `File.ReadCrystalList()` | 結晶リストファイル (*.xml) を読み込み |
| `File.ReadCrystal()` | CIF/AMC形式の結晶ファイルを読み込み |
| `File.ExportAsCIF()` | 現在選択中の結晶をCIF形式で保存 |
| `File.SaveText()` | テキストデータをファイルに保存 |

---

## Crystal クラス

現在選択中の結晶のプロパティを取得します。

| プロパティ | 型 | 説明 |
|-----------|-----|------|
| `Crystal.Name` | string | 結晶名 |
| `Crystal.ChemicalFormula` | string | 化学式 |
| `Crystal.Density` | double | 密度 (g/cm³) |

---

## CrystalList クラス

結晶リストの管理を行います。

| 関数/プロパティ | 説明 |
|----------------|------|
| `CrystalList.SelectedIndex` | 選択中の結晶のインデックス（取得/設定） |
| `CrystalList.Add()` | 現在の結晶をリスト末尾に追加 |
| `CrystalList.Replace()` | 選択中の結晶を現在の結晶で置換 |
| `CrystalList.Delete()` | 選択中の結晶を削除 |
| `CrystalList.ClearAll()` | すべての結晶を削除 |
| `CrystalList.MoveUp()` | 選択中の結晶を上に移動 |
| `CrystalList.MoveDown()` | 選択中の結晶を下に移動 |

---

## Direction クラス

結晶方位の設定・回転を行います。角度の単位に注意してください（ラジアン版とデグリー版があります）。

| 関数 | 説明 |
|------|------|
| `Direction.Euler(phi, theta, psi)` | オイラー角で方位を設定（ラジアン） |
| `Direction.EulerInDegree(phi, theta, psi)` | オイラー角で方位を設定（度） |
| `Direction.EulerInDeg(phi, theta, psi)` | `EulerInDegree` の別名 |
| `Direction.Rotate(axis_x, axis_y, axis_z, angle)` | 任意軸周りに回転（ラジアン） |
| `Direction.RotateInDeg(axis_x, axis_y, axis_z, angle)` | 任意軸周りに回転（度） |
| `Direction.RotateAroundAxis(u, v, w, angle)` | 晶帯軸 [uvw] 周りに回転（ラジアン） |
| `Direction.RotateAroundAxisInDeg(u, v, w, angle)` | 晶帯軸 [uvw] 周りに回転（度） |
| `Direction.RotateAroundPlane(h, k, l, angle)` | 結晶面 (hkl) 法線周りに回転（ラジアン） |
| `Direction.RotateAroundPlaneInDeg(h, k, l, angle)` | 結晶面 (hkl) 法線周りに回転（度） |
| `Direction.ProjectAlongPlane(h, k, l)` | 結晶面 (hkl) の法線をスクリーン垂直方向に設定 |
| `Direction.ProjectAlongAxis(u, v, w)` | 晶帯軸 [uvw] をスクリーン垂直方向に設定 |

---

## DifSim クラス

回折シミュレータを操作します。

### ウィンドウ制御

| 関数 | 説明 |
|------|------|
| `DifSim.Open()` | 回折シミュレータウィンドウを開く |
| `DifSim.Close()` | 回折シミュレータウィンドウを閉じる |

### 波源の設定

| 関数 | 説明 |
|------|------|
| `DifSim.Source_Xray()` | X線源に設定 |
| `DifSim.Source_Electron()` | 電子線源に設定 |
| `DifSim.Source_Neutron()` | 中性子線源に設定 |

### プロパティ

| プロパティ | 型 | 説明 |
|-----------|-----|------|
| `DifSim.Energy` | double | エネルギー (keV) |
| `DifSim.Wavelength` | double | 波長 (Å) |
| `DifSim.Thickness` | double | 試料厚さ (nm) |
| `DifSim.NumberOfDiffractedWaves` | int | ブロッホ波の数 |
| `DifSim.CameraLength2` | double | カメラ長 (mm) |
| `DifSim.SkipRendering` | bool | 描画のスキップ（バッチ処理で高速化） |

### 入射ビームモード

| 関数 | 説明 |
|------|------|
| `DifSim.Beam_Parallel()` | 平行ビーム |
| `DifSim.Beam_PrecessionXray()` | X線歳差 |
| `DifSim.Beam_PrecessionElectron()` | 電子線歳差 (PED) |
| `DifSim.Beam_Convergence()` | 収束ビーム (CBED) |

### 計算モード

| 関数 | 説明 |
|------|------|
| `DifSim.Calc_Excitation()` | 励起誤差のみ |
| `DifSim.Calc_Kinematical()` | 運動学的理論 |
| `DifSim.Calc_Dynamical()` | 動力学的理論 |

### 画像設定

| プロパティ/関数 | 説明 |
|----------------|------|
| `DifSim.ImageResolutionInMM` | 解像度 (mm/pixel) |
| `DifSim.ImageResolutionInNMinv` | 解像度 (nm⁻¹/pixel) |
| `DifSim.ImageWidth` / `ImageHeight` | 画像サイズ (pixel) |
| `DifSim.ImageSize(w, h)` | 画像サイズを設定 |

### 検出器パラメータ

| プロパティ | 説明 |
|-----------|------|
| `DifSim.Tau` / `TauInDeg` | 検出器傾斜角 τ（ラジアン/度） |
| `DifSim.Phi` / `PhiInDeg` | 検出器回転軸方向 φ（ラジアン/度） |
| `DifSim.Foot(x, y)` | Foot位置のピクセル座標 |

### 出力

| 関数 | 説明 |
|------|------|
| `DifSim.SaveAsPng()` | 現在の回折パターンをPNGファイルとして保存 |
| `DifSim.SpotInfo()` | スポット情報をCSV形式で取得 |

---

## HRTEM / STEM / Potential クラス

この3つの画像シミュレーションクラスは多くのメンバーを共有します。重複を避けるため、下表ではプレースホルダを使います。

- **`#`** : **HRTEM**・**STEM**・**Potential** に共通。`#` を `HRTEM` / `STEM` / `Potential` に置き換えて使います（例: `STEM.Simulate()`、`Potential.AccVol`）。
- **`$`** : **HRTEM** と **STEM** にのみ共通。`$` を `HRTEM` または `STEM` に置き換えます。
- クラス名を明示したメンバー（`STEM.…` / `HRTEM.…`）はそのクラス専用です。**Potential** クラスは固有メンバーを持たず、`#` のメンバーのみを使います。

### ウィンドウ制御

| 関数 | 説明 |
|------|------|
| `#.Open()` | 画像シミュレータウィンドウを開く |
| `#.Close()` | 画像シミュレータウィンドウを閉じる |
| `#.Simulate()` | 現在の設定でシミュレーションを実行 |

### 顕微鏡・光学条件

| プロパティ/関数 | 説明 |
|----------------|------|
| `#.AccVol` | 加速電圧 (kV) |
| `$.Thickness` | 試料厚さ (nm) |
| `$.Defocus` | デフォーカス (nm) |
| `$.Cs` | 球面収差 Cs (mm) |
| `$.Cc` | 色収差 Cc (mm) |
| `$.DeltaV` | エネルギー幅 ΔV、FWHM (eV) |
| `$.Scherzer` | シェルツァーデフォーカス (nm、取得のみ) |
| `STEM.ConvergenceAngle` | 収束半角 (mrad) |
| `STEM.DetectorInnerAngle` / `STEM.DetectorOuterAngle` | 環状検出器の内/外半角 (mrad) |
| `STEM.EffectiveSourceSize` | 実効光源サイズ、FWHM (pm) |
| `HRTEM.Beta` | 照射半角 β (ラジアン) |
| `HRTEM.ApertureSemiangle` | 対物絞り半角 (ラジアン) |
| `HRTEM.ApertureShiftX` / `HRTEM.ApertureShiftY` | 対物絞りシフト (ラジアン) |
| `HRTEM.OpenAperture` | 対物絞りの開放 (true/false) |

### シミュレーション設定

| プロパティ/関数 | 説明 |
|----------------|------|
| `#.NumberOfDiffractedWaves` | 計算に取り入れる回折波(ブロッホ波)の最大数 |
| `#.ImageWidth` / `#.ImageHeight` | 画像サイズ (pixel) |
| `#.ImageSize(width, height)` | 画像サイズを設定 (pixel) |
| `#.ImageResolution` | 画像解像度 (nm/pixel) |
| `STEM.AngularResolution` | 収束ビームの角度分解能 (mrad) |
| `STEM.SliceThickness` | TDS計算用のスライス厚さ (nm) |
| `HRTEM.Mode_LinearImage()` | 線形像（準コヒーレント）モデルを使用 |
| `HRTEM.Mode_TCC()` | TCC（透過相互係数）モデルを使用 |

### 単一/シリーズ画像モード

| プロパティ/関数 | 説明 |
|----------------|------|
| `$.SingleImageMode()` | 単一画像モードに切替 |
| `$.SerialImageMode(withThickness, withDefocus)` | シリーズ画像モードに切替 |
| `$.SerialImageThicknessStart` / `Step` / `Num` | シリーズ厚さ: 開始 (nm) / ステップ (nm) / 枚数 |
| `$.SerialImageDefocusStart` / `Step` / `Num` | シリーズデフォーカス: 開始 (nm) / ステップ (nm) / 枚数 |

### 画像プロパティ

| プロパティ/関数 | 説明 |
|----------------|------|
| `#.UnitCellVisible` | 単位格子の表示 (true/false) |
| `#.LabelVisible` | 画像ラベルの表示 (true/false) |
| `#.LabelSize` | ラベルのフォントサイズ |
| `#.ScaleBarVisible` | スケールバーの表示 (true/false) |
| `#.ScaleBarLength` | スケールバーの長さ (nm) |
| `#.GaussianBlurEnabled` | ガウシアンぼかしの適用 (true/false) |
| `#.GaussianBlurFWHM` | ガウシアンぼかしの FWHM (pm) |
| `STEM.DisplayBoth()` | 弾性 + TDS を表示 |
| `STEM.DisplayElastic()` | 弾性のみ表示 |
| `STEM.DisplayTDS()` | TDS（非弾性）のみ表示 |

### 画像の保存

| プロパティ/関数 | 説明 |
|----------------|------|
| `#.SaveImageAsPng(filename)` | PNG形式で保存（filename 省略時はダイアログ） |
| `#.SaveImageAsTif(filename)` | TIFF形式で保存（filename 省略時はダイアログ） |
| `#.SaveImageAsEmf(filename)` | EMFメタファイルで保存（filename 省略時はダイアログ） |
| `#.SaveIndividually` | シリーズモード時に各画像を個別保存 (true/false) |
| `#.OverprintSymbols` | 保存画像に単位格子・ラベル・スケールバーを焼き込む (true/false) |

---

## グローバル関数

| 関数 | 説明 |
|------|------|
| `Sleep(ms)` | 指定ミリ秒だけ待機 |

---

## 関連項目

- [マクロ](index.md)
- [マクロの使用例](2-examples.md)
