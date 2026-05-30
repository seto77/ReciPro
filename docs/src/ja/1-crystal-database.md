# 結晶データベース

**結晶データベース** は、2つのデータソースから結晶構造を検索・インポートできます。**AMCSD** と **COD** のチェックボックスで切り替えます。

- **AMCSD** : 内蔵の [American Mineralogist Crystal Structure Database](http://rruff.geo.arizona.edu/AMS/amcsd.php)（約21,000件）。
- **COD** : [Crystallography Open Database](https://www.crystallography.net/cod/)。初回利用時にデータベースファイルが自動でダウンロードされ、更新もできます。

これらのデータベースを使用する際は、以下の文献を引用してください。

**AMCSD** を使用する場合：

> Downs, R.T. and Hall-Wallace, M. (2003) The American Mineralogist Crystal Structure Database. *American Mineralogist* **88**, 247–250.

**COD** を使用する場合：

> Gražulis, S. et al. (2009) Crystallography Open Database – an open-access collection of crystal structures. *Journal of Applied Crystallography* **42**, 726–729.
>
> Gražulis, S. et al. (2012) Crystallography Open Database (COD): an open-access collection of crystal structures and platform for world-wide collaboration. *Nucleic Acids Research* **40**, D420–D427.

![結晶データベース](../assets/cap-ja-auto/FormCrystalDatabase.png)

---

## キーボード・マウスショートカット

このウィンドウに修飾キーの組み合わせはなく、操作は通常のクリックが中心です。分かりにくい入力は以下のみです。

| ショートカット | 動作 |
|----------------|------|
| <kbd>F1</kbd> | このページのオンラインマニュアルを開く |
| 検索欄で <kbd>ENTER</kbd> | データベース検索を実行（**Search** ボタンと同じ） |
| 結果テーブルの行をクリック | その結晶をメインウィンドウへ読込 |
| **周期表** ポップアップで元素をクリック | フィルタを循環: *無視* → *含む* → *除外* |

→ 全ウィンドウの一覧は **[21. キーボード・マウスショートカット](21-shortcuts.md)** を参照。

---

## テーブル

![結晶テーブル](../assets/cap-ja-auto/FormCrystalDatabase.crystalDatabaseControl.png)

データベースに含まれる結晶が表示されます。検索条件を入力している場合は、条件に合う結晶のみが表示されます。

テーブル中の結晶を選択すると、メインウィンドウの **結晶情報** に情報が転送されます。**結晶リスト** に追加するには **リストへ追加** または **選択結晶と入れ替え** ボタンを押してください。

---

## 検索オプション

![検索オプション](../assets/cap-ja-auto/FormCrystalDatabase.panelSearch.searchCrystalControl.png)

検索条件を入力後、**検索**ボタンまたはEnterキーを押してください。

| 条件 | 説明 |
|------|------|
| **名前** | 結晶の名称 |
| **元素** | **周期表**ボタンで元素選択ウィンドウを開く。各元素のボタンは「含んでもよい」「必ず含む」「必ず含まない」を切替 |
| **文献** | 論文名、雑誌名、著者名 |
| **結晶系** | 結晶系を選択 |
| **格子定数** | 格子定数と許容誤差 |
| **d値** | 強い回折のd-spacingと許容誤差 |
| **密度** | 密度と許容誤差 |

### 名前

結晶名のテキスト検索。部分一致可。

### 元素

**周期表**ボタンで元素選択ウィンドウを開きます。各元素ボタンは押すたびに以下の3状態を切り替えます。

- **含んでもよい**（デフォルト・グレー）
- **必ず含む**（緑）
- **必ず含まない**（赤）

ウィンドウ上部の 3 つのボタンで、すべての元素を一括で 3 状態のいずれかにリセットできます。

### 文献

論文タイトル・雑誌名・著者リストを対象としたテキスト検索。

### 結晶系

特定の結晶系（立方・正方・斜方・六方・三方・単斜・三斜）に絞り込みます。

### 格子定数検索

![格子定数検索](../assets/cap-ja-auto/FormCrystalDatabase.panelSearch.searchCrystalControl.flowLayoutPanel1.groupBoxCellParameter.png)

目標の格子定数 *a*, *b*, *c*, *α*, *β*, *γ* と許容誤差を入力します。空欄はワイルドカード扱い。

### d値

最強反射（または複数の強反射）の *d*-spacing と許容誤差を入力します。実験で回折ピーク位置のみが分かっている場合に有用。

### 密度

質量密度 (g/cm³) と許容誤差で絞り込みます。
