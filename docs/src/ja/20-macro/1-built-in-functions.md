# 組み込み関数一覧

ReciProマクロで使用可能な組み込みクラスと関数の一覧です。

---

## File クラス

ファイルの読み書きを行います。

| 関数 | 説明 |
|------|------|
| `File.GetDirectoryPath(filename)` | フォルダ選択ダイアログを表示し、選択されたフォルダのパスを返す。`filename` を渡すと、そのファイルを含むフォルダを返す |
| `File.GetFileName()` | ファイル選択ダイアログを表示し、選択されたファイルのパスを返す |
| `File.GetFileNames()` | 複数ファイル選択ダイアログを表示し、選択されたファイルパスのリストを返す |
| `File.ReadCrystalList(filename)` | 結晶リストファイル (*.xml) を読み込み。`filename` を省略するとダイアログを開く |
| `File.ReadCrystal(filename)` | CIF/AMC形式の結晶ファイルを読み込み。`filename` を省略するとダイアログを開く |
| `File.ExportAsCIF(filename)` | 現在選択中の結晶をCIF形式で保存。`filename` を省略するとダイアログを開く |
| `File.ReadText(filename)` | テキストファイルを UTF-8 で読み、文字列として返す。`filename` 省略でダイアログを開く。`Crystal.LoadCifText()` / `SaveText()` と組で使う |
| `File.SaveText(textData, filename)` | テキストデータをファイルに保存。`textData` を UTF-8 で書き出す。`filename` を省略すると保存ダイアログを開く |

---

## Crystal クラス

現在選択中の結晶の読み取りと、pending 下書きを介した結晶の生成・編集を行います。

### 読み取り

| プロパティ / 関数 | 説明 |
|---|---|
| `Crystal.Name` | 結晶名 |
| `Crystal.ChemicalFormula` | 化学式 |
| `Crystal.Density` | 密度（g/cm³） |
| `Crystal.GetCellInAng()` | セル定数を `[a, b, c, alpha, beta, gamma]`（Å・度）で取得 |
| `Crystal.SpaceGroupName` | 空間群の Hermann–Mauguin 記号（複数設定がある群では `:2`、`:H` などの設定サフィックス付き） |
| `Crystal.SpaceGroupNumber` | International Tables の空間群番号（1–230） |
| `Crystal.HasPending` | pending 下書きが開いているか |

### 生成・編集 (下書き → Commit)

結晶は **pending 下書き**で組み立てます: 下書きを開始し、setter で値を入れ、`Commit()` が全検証 → 結晶の構築 → 現在の結晶への適用を一括で行います (CIF ファイル読み込みと同じように、GUI と表示中の全シミュレータが更新されます)。`Commit()` が失敗したときは検証エラーを全件まとめて報告し、現在の結晶は変更せず、下書きも保持されるので、修正してそのまま再 Commit できます。

| 関数 | 説明 |
|---|---|
| `Crystal.BeginCreate(name)` | 新規結晶の下書きを開始 |
| `Crystal.BeginEdit()` | 現在の結晶から下書きを開始（セル・空間群・原子・方位を引き継ぐ） |
| `Crystal.LoadCifText(cifText)` | CIF テキスト（.cif ファイルの中身。パスではない）から下書きを開始 |
| `Crystal.SetName(name)` | 下書きの名前を変更 |
| `Crystal.SetCellInAng(a, b, c, alpha, beta, gamma)` | セル定数を **Å・度**で設定。毎回セル全体を指定し直す（置換であって部分更新ではない）。省略した引数は空間群の制約から導出され（立方晶なら `a` だけでよい）、明示値が制約と矛盾するとエラー |
| `Crystal.SetSpaceGroup(symbol)` | 空間群を記号で設定（HM 短縮/full か Hall。空白と `_` は無視）。複数設定がある群では設定を付ける（`'Fd-3m:2'`、`'R-3c:H'`、`'P21/c:b1'`）— 曖昧な記号は候補列挙付きのエラーになる |
| `Crystal.SetSpaceGroupByNumber(itNumber, setting)` | 空間群を IT 番号（1–230）で設定。複数設定がある場合は `setting`（`'1'`、`'2'`、`'H'`、`'R'`、`'b1'` など）で選ぶ |
| `Crystal.AddAtom(label, element, x, y, z, occ, bIso)` | 非対称単位の原子を追加: 元素記号・分率座標・占有率（0 < occ ≤ 1、既定 1）・等方性 B（Å²、既定 0）。等価位置・Wyckoff 記号・多重度は自動導出 |
| `Crystal.ClearAtoms()` | 下書きの原子を全消去 |
| `Crystal.Commit()` | 下書きを検証・構築・適用 |
| `Crystal.Cancel()` | 下書きを破棄 |

```python
ReciPro.Crystal.BeginCreate('NaCl')
ReciPro.Crystal.SetSpaceGroup('Fm-3m')
ReciPro.Crystal.SetCellInAng(5.6402)
ReciPro.Crystal.AddAtom('Na', 'Na', 0, 0, 0)
ReciPro.Crystal.AddAtom('Cl', 'Cl', 0.5, 0.5, 0.5)
ReciPro.Crystal.Commit()

base = ReciPro.Crystal.GetCellInAng()
for k in range(-2, 3):
    ReciPro.Crystal.BeginEdit()
    ReciPro.Crystal.SetCellInAng(base[0] * (1 + 0.01 * k))
    ReciPro.Crystal.Commit()
```

`Commit()` 成功後の次の `BeginEdit()` は**更新後の**結晶が起点になるため、変更は累積します — 絶対値でスキャンするときは上の例のようにループ前に基準値を読んでください。Commit した結晶を結晶リストへ登録するには `CrystalList.Add()` を呼びます。

---

## CrystalList クラス

結晶リストの管理を行います。

| 関数/プロパティ | 説明 |
|----------------|------|
| `CrystalList.SelectedIndex` | 選択中の結晶のインデックス（取得/設定） |
| `CrystalList.Count` | 結晶リストに登録されている結晶の数 |
| `CrystalList.Add()` | 現在の結晶をリスト末尾に追加 |
| `CrystalList.Replace()` | 選択中の結晶を現在の結晶で置換 |
| `CrystalList.Delete()` | 選択中の結晶を削除 |
| `CrystalList.ClearAll()` | すべての結晶を削除 |
| `CrystalList.MoveUp()` | 選択中の結晶を上に移動 |
| `CrystalList.MoveDown()` | 選択中の結晶を下に移動 |

---

## Dir クラス

結晶方位の設定・回転を行います。角度の単位に注意してください（ラジアン版とデグリー版があります）。

| 関数 | 説明 |
|------|------|
| `Dir.Euler(phi, theta, psi)` | オイラー角で方位を設定（ラジアン） |
| `Dir.EulerInDegree(phi, theta, psi)` | オイラー角で方位を設定（度） |
| `Dir.EulerInDeg(phi, theta, psi)` | `EulerInDegree` の別名 |
| `Dir.Rotate(axis_x, axis_y, axis_z, angle)` | 任意軸周りに回転（ラジアン） |
| `Dir.RotateInDeg(axis_x, axis_y, axis_z, angle)` | 任意軸周りに回転（度） |
| `Dir.RotateAroundAxis(u, v, w, angle)` | 晶帯軸 [uvw] 周りに回転（ラジアン） |
| `Dir.RotateAroundAxisInDeg(u, v, w, angle)` | 晶帯軸 [uvw] 周りに回転（度） |
| `Dir.RotateAroundPlane(h, k, l, angle)` | 結晶面 (hkl) 法線周りに回転（ラジアン） |
| `Dir.RotateAroundPlaneInDeg(h, k, l, angle)` | 結晶面 (hkl) 法線周りに回転（度） |
| `Dir.ProjectAlongPlane(h, k, l)` | 結晶面 (hkl) の法線をスクリーン垂直方向に設定 |
| `Dir.ProjectAlongAxis(u, v, w)` | 晶帯軸 [uvw] をスクリーン垂直方向に設定 |
| `Dir.GetEuler()` | 現在の方位を Z-X-Z オイラー角 `[phi, theta, psi]`（ラジアン）で取得 |
| `Dir.GetEulerInDeg()` | 現在の方位を Z-X-Z オイラー角 `[phi, theta, psi]`（度）で取得 |
| `Dir.GetRotationMatrix()` | 現在の回転行列を 9 要素配列 `[R11, R12, R13, R21, R22, R23, R31, R32, R33]` で取得（`SpotID.CandidateList()` と同じ規約） |
| `Dir.SetRotationMatrix(r11, r12, r13, r21, r22, r23, r31, r32, r33)` | 回転行列の 9 要素から方位を設定（検証と再直交化を経て適用） |

オイラー角はジンバル位置（θ = 0, 180°）で一意に決まらないため、`Euler()` の後の `GetEuler()` は同じ姿勢を再現しますが、同じ数値列になるとは限りません。方位を正確に保存・復元するには `Dir.GetRotationMatrix()` / `Dir.SetRotationMatrix()` を使ってください。 規約の詳細は[回転ジオメトリ](../4-rotation-geometry.md)を参照してください。

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
| `DifSim.ExcitationError` | double | スポット半径 (nm⁻¹)。運動学/励起誤差モードの `SpotInfo()` は \|Sg\| がこの値以内の反射を出力する |
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
| `DifSim.SaveAsPng(filename)` | 現在の回折パターンをPNGファイルとして保存。`filename` を省略するとダイアログを開く |
| `DifSim.SpotInfo()` | スポット情報をCSV形式で取得 |

---

## SpotID クラス

[Spot ID v2](../11-spot-id-v2.md) をマクロから駆動します。画像またはスポット一覧の読み込み → スポット検出 → 方位同定 → 候補リストの取得までを、ウィンドウを操作せずに実行できます。`FindSpots()` と `Identify()` は処理の完了を待って戻るので、そのまま続けて呼べます。

### ウィンドウ操作

`SpotID.Open()` / `SpotID.Close()`

### 入射波の種類

`SpotID.Source_Xray()` / `SpotID.Source_Electron()` / `SpotID.Source_Neutron()`

### 処理の流れ

| 関数 | 説明 |
|------|------|
| `SpotID.LoadFile(filename)` | **File > Load** と同じ動作でファイルを読み込む。`.csv` はスポット一覧として（先に画像の読み込みが必要）、それ以外の拡張子は回折図形の画像として読む（dm3、dm4、mrc、ipa、tif ほか対応形式）。`filename` を省略するとファイル選択ダイアログを開く |
| `SpotID.FindSpots()` | 読み込んだ画像からスポットを検出してフィッティングする（**Find spots** ボタンと同じ） |
| `SpotID.Identify()` | 検出したスポットを説明する方位を探索し（**Identify spots** ボタンと同じ）、候補数を返す。対象となる結晶はメインウィンドウの結晶リストで選択中のもの |
| `SpotID.CandidateList()` | 候補方位の一覧を CSV テキストで返す |
| `SpotID.SpotList()` | 観測スポットの一覧を CSV テキストで返す（列は **File > Save** と同じ）。`File.SaveText()` と組み合わせて保存すれば `LoadFile()` で読み戻せる |

`CandidateList()` は候補ごとに、結晶名・Z-X-Z オイラー角（度）・回転行列の 9 成分 R11〜R33（結晶座標系→実験室座標系、列ベクトルに作用）・残差の平均二乗（nm⁻²）・観測スポットと *hkl* 指数の対応を返します。候補は割り当てられたスポット数の降順、次いで残差の昇順に並びます。数値は invariant culture で書き出されるため、小数点は常にピリオドです。

### プロパティ

| プロパティ | 型 | 説明 |
|-----------|---|------|
| `Energy` | double | 入射線のエネルギー（X線・電子線は keV、中性子線は meV） |
| `CameraLength` | double | カメラ長（mm） |
| `PixelSizeInMM` | double | 画像のピクセルサイズ（mm）。読み書きするとピクセルサイズの単位も mm に切り替わる |
| `PixelSizeInNMinv` | double | 画像のピクセルサイズ（nm⁻¹）。読み書きすると単位も nm⁻¹ に切り替わる |
| `MaxNumberOfSpots` | int | `FindSpots()` が検出するスポット数の上限 |
| `NearestNeighbor` | int | 検出するスポット同士に許す最小間隔（ピクセル） |
| `FittingRange` | double | ピークフィッティングに使う、各スポット周囲の領域の半径（ピクセル） |
| `AcceptableError` | double | 観測スポットを候補反射に対応づけるときに許す面間隔の相対差（%） |
| `IgnoreProhibitedReflections` | bool | 多重回折で現れうる消滅則禁制反射を無視するか |
| `MultiGrain` | bool | 複数の結晶粒を探索するか。`False` なら単結晶 |
| `MaxNumberOfGrains` | int | `MultiGrain` が `True` のときに探索する粒方位の最大数 |
| `NumberOfDetectedSpots` | int | 検出されたスポット数（読み取り専用） |
| `NumberOfCandidates` | int | 直前の `Identify()` が見つけた候補数（読み取り専用） |

---

## StructureViewer クラス

結晶構造ビューアをマクロから駆動します。3D モデルはウィンドウ表示時に構築されるため、`SaveImage()` と `Export3DModel()` は必要なら先にウィンドウを開きます。

| 関数 | 説明 |
|---|---|
| `StructureViewer.Open()` | 結晶構造ビューアのウィンドウを開く |
| `StructureViewer.Close()` | 結晶構造ビューアのウィンドウを閉じる |
| `StructureViewer.SaveImage(filename)` | メインビューの描画画像を PNG 保存（ピクセルサイズはウィンドウの **Size (W×H)** ボックス）。`filename` 省略で保存ダイアログ |
| `StructureViewer.Export3DModel(filename, maxSizeInMM, fixedScaleInMMperNm, includeAtoms, includeBonds, includePolyhedra, polyhedraAsEdges, polyEdgeDiaInMM, includeCellEdges, cellEdgeDiaInMM, thickenBondsToMM)` | 表示中の構造を 3D プリント用に出力（ファイルメニューの **3Dモデルを出力 (3MF/STL)** と同じ）。形式は拡張子で決まる（`.stl` = 単色 / `.3mf` = 元素色分け）。必須は `filename` のみで、他の既定値はダイアログの既定と同じ（最長辺 80 mm・単位胞枠 ⌀2.4 mm・結合の増径 ⌀1.2 mm）。`fixedScaleInMMperNm` > 0 を渡すと複数の模型を同縮尺で作れる |

```python
ReciPro.StructureViewer.Export3DModel('D:/print/NaCl_60mm.stl', maxSizeInMM=60)
ReciPro.StructureViewer.Export3DModel('D:/print/NaCl_edges.stl', maxSizeInMM=60, polyhedraAsEdges=True)
```

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
| `#.UnitCellVisible` | 単位胞の表示 (true/false) |
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
| `#.OverprintSymbols` | 保存画像に単位胞・ラベル・スケールバーを焼き込む (true/false) |

---

## グローバル関数

| 関数 | 説明 |
|------|------|
| `Sleep(ms)` | 指定ミリ秒だけ待機 |

---

## 関連項目

- [マクロ](index.md)
- [マクロの使用例](2-examples.md)
