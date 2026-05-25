# マクロの使用例

ReciPro マクロの具体的な使用例を紹介します。最初のいくつかは Python 初心者向けの導入例で、後半はバッチ処理の例です。

> **補足**: ReciPro マクロ API は `ReciPro.クラス名.メンバ名` の形でアクセスします。入力候補ポップアップは常に `ReciPro.` 付きの完全形を挿入するので、通常は手で打つ必要はありません。

---

## 初心者向け

### A. 基本のループ (副作用なし)

エディタの使い方を学ぶのに一番簡単な例です。**Step by step** で実行して、デバッグパネルで `i` と `sq` が変わっていく様子を観察してください — これが ReciPro 環境での「値の確認方法」です (`print()` は使えません)。

```python
# 10 回ループして二乗を計算
for i in range(10):
    sq = i * i
```

### B. math モジュールを使う

`math` モジュールは起動時に自動で import 済みなので、すぐに使えます。

```python
r = 5.0
area = math.pi * r * r
circumference = 2 * math.pi * r
# Step モードでデバッグパネルに 'area' と 'circumference' が表示される
```

### C. 現在の結晶を回転する

```python
# a 軸 (x 軸) 周りに 30 度回転
ReciPro.Dir.RotateAroundAxisInDeg(1, 0, 0, 30)
```

### D. 晶帯軸に揃える

```python
# [001] 晶帯軸をスクリーンに垂直にする
ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
```

### E. 結晶リストの先頭数個をループ

```python
# リスト先頭 5 個の結晶名を集める
names = []
for i in range(5):
    ReciPro.CrystalList.SelectedIndex = i
    names.append(ReciPro.Crystal.Name)
# Step モードで 'names' が行ごとに増えていく様子を観察
```

### F. 回折パターンを表示する

```python
# リスト先頭の結晶について、200 keV 電子線で [001] 入射の回折図を表示
ReciPro.CrystalList.SelectedIndex = 0
ReciPro.DifSim.Open()
ReciPro.DifSim.Source_Electron()
ReciPro.DifSim.Energy = 200
ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
```

---

## バッチ処理

### 1. 結晶リストの全結晶の回折パターンを保存

```python
folder = ReciPro.File.GetDirectoryPath()

ReciPro.DifSim.Open()
ReciPro.DifSim.Source_Electron()
ReciPro.DifSim.Energy = 200  # 200 keV
ReciPro.DifSim.Calc_Kinematical()
ReciPro.DifSim.SkipRendering = True  # バッチ処理中は描画をスキップ

for i in range(80):  # 結晶数に合わせて変更
    ReciPro.CrystalList.SelectedIndex = i
    name = ReciPro.Crystal.Name
    ReciPro.Dir.ProjectAlongAxis(0, 0, 1)  # [001] に投影
    ReciPro.DifSim.SaveAsPng(folder + name + "_001.png")
    ReciPro.Dir.ProjectAlongAxis(1, 1, 0)  # [110] に投影
    ReciPro.DifSim.SaveAsPng(folder + name + "_110.png")

ReciPro.DifSim.SkipRendering = False
```

### 2. 結晶を段階的に回転しながらスナップショット

```python
folder = ReciPro.File.GetDirectoryPath()
ReciPro.DifSim.Open()
ReciPro.DifSim.Source_Electron()
ReciPro.DifSim.Energy = 200

# [001] から開始
ReciPro.Dir.ProjectAlongAxis(0, 0, 1)

# 1 度ずつ回転しながら 90 枚保存
for i in range(90):
    ReciPro.DifSim.SaveAsPng(folder + "rot_%03d.png" % i)
    ReciPro.Dir.RotateAroundAxisInDeg(1, 0, 0, 1)  # a 軸周りに 1 度回転
```

### 3. オイラー角の設定

```python
# オイラー角を度で指定
ReciPro.Dir.EulerInDeg(45, 30, 60)

# 同じことをラジアンで (math は事前 import 済み)
ReciPro.Dir.Euler(math.pi/4, math.pi/6, math.pi/3)
```

### 4. 結晶面の法線方向に投影

```python
# (111) 面の法線方向をスクリーンに垂直にする
ReciPro.Dir.ProjectAlongPlane(1, 1, 1)

# [110] 晶帯軸方向をスクリーンに垂直にする
ReciPro.Dir.ProjectAlongAxis(1, 1, 0)
```

### 5. CIF ファイルの一括読み込み

```python
# 複数の CIF ファイルを選択して読み込み
files = ReciPro.File.GetFileNames()
for f in files:
    ReciPro.File.ReadCrystal(f)
    ReciPro.CrystalList.Add()
```

### 6. スポット情報の取得

```python
# 回折シミュレータのスポット情報を CSV として取得
ReciPro.DifSim.Open()
ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
info = ReciPro.DifSim.SpotInfo()
ReciPro.File.SaveText(info, "spot_info.csv")
```

---

## ヒント

- **値の確認方法**: この環境では `print()` は使えません (コンソールウィンドウが無い)。**Step by step** で実行し、デバッグパネルで各行におけるローカル変数の値を確認してください。
- **`math` は事前 import 済み**: `math.sqrt(x)`、`math.sin(x)`、`math.pi`、`math.radians(deg)` が `import math` なしで使えます。
- **バッチ処理の高速化**: `ReciPro.DifSim.SkipRendering = True` を設定するとループ中の描画をスキップします。処理後は `False` に戻してください。
- **描画待ち**: `ReciPro.Sleep(ms)` でスクリプトを一時停止できます。GUI の描画が間に合わない場合に有用です。
- **入力候補**: ポップアップは `ReciPro.クラス.メンバ` の完全形を表示します。数文字打って `Enter` か `Tab` で確定。

---

## 関連項目

- [マクロ](index.md)
- [組み込み関数一覧](1-built-in-functions.md)
