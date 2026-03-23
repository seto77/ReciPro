# -*- coding: utf-8 -*-
import os

WIKI_DIR = r"C:\Users\seto\AppData\Local\Temp\ReciPro.wiki"

def write_page(filename, content):
    path = os.path.join(WIKI_DIR, filename)
    with open(path, "w", encoding="utf-8") as f:
        f.write(content)
    print(f"Wrote: {filename}")

# ============================================================
# 7.4 CBED Simulation (EN) - without colon
# ============================================================
write_page("7.4-CBED-simulation.md", """# CBED Simulation

**CBED (Convergent-Beam Electron Diffraction)** simulation computes and displays convergent-beam patterns using the Bloch wave (Bethe) method. CBED patterns show diffraction disks instead of spots, and contain rich information about crystal symmetry, thickness, and structure.

To open this window, select **Convergence (CBED)** in the incident beam mode of the [[7. Diffraction simulator|Diffraction Simulator]], then click **Execute**.

---

## Input parameters

### Mode
- **CBED**: Standard convergent-beam pattern where each disk corresponds to one reflection. The transmitted disk (000) is at the center.
- **LACBED** (Large-Angle CBED): Large-angle convergent beam pattern where disks from different reflections overlap. Useful for observing higher-order Laue zone (HOLZ) lines and symmetry.

### Convergence semi-angle (mrad)
The half-angle of the convergent beam cone. This determines the size of each diffraction disk. Typical values: 5-30 mrad. The disk diameter in reciprocal space equals 2 * alpha.

### Disk resolution (mrad/pixel)
The angular resolution within each disk. Smaller values give higher resolution but increase computation time quadratically (total pixels = (2*alpha/resolution)^2). The total number of beam directions (pixels) is shown in the status display.

### Max Bloch waves
The maximum number of beams included in the Bloch wave calculation at each incident beam direction. More beams give more accurate results but increase the computation time (O(N^3) eigenvalue problem per pixel). Typical values: 100-500.

### Thickness range
Set the start, end, and step values for sample thickness (nm). CBED patterns depend strongly on thickness, and computing multiple thicknesses simultaneously is efficient because the eigenvalue problem is solved only once per beam direction.

### Solver
Choose the linear algebra backend for the eigenvalue problem:
- **Auto**: Automatically selects the best available solver.
- **Eigenproblem (MKL)**: Intel MKL-based eigensolver (fastest, requires MKL).
- **Eigenproblem (Eigen)**: Eigen C++ library solver.
- **Managed**: Pure .NET managed solver (slowest but always available).

### Thread count
Number of parallel threads for the calculation. Set to the number of CPU cores for best performance.

### Guide circles
When checked, draw circles indicating the boundary of each diffraction disk (Bragg angle circles).

---

## Execute / Stop

Click **Execute** to start the CBED simulation. A progress bar shows the percentage completed, elapsed time, and estimated remaining time. Click **Stop** to cancel the calculation.

---

## Output controls

After the calculation completes, the output panel becomes available:

### Display mode
- **All disks**: Show the complete CBED pattern with all disks.
- **Individual disk**: Show a single selected disk at full resolution.

### Thickness slider
Select which thickness to display (from the computed thickness range).

### Brightness (Min / Max)
Adjust the minimum and maximum intensity for display. This is useful for bringing out weak features.

### Gamma
Gamma correction for the intensity display. Values < 1 enhance weak features; values > 1 enhance strong features.

### Color scale
Choose the color map: Gray, Spectrum (rainbow), etc.

### Gradient
Set the gradient display mode for the pattern.

---

## Physical background

In CBED, the incident beam is a cone of plane waves with different directions. For each direction, the Bloch wave method solves the electron Schrodinger equation inside the crystal:

1. The crystal potential V(r) is expanded in Fourier components U_g.
2. The electron wavefunction is expanded as a sum of Bloch waves.
3. This leads to an eigenvalue problem of size N (number of beams).
4. The eigenvalues give the Bloch wave wavevectors; the eigenvectors give the Bloch wave amplitudes.
5. Boundary conditions at the entrance and exit surfaces determine the diffracted beam intensities.

The intensity at each pixel in a CBED disk is I_g(k) = |sum_j C_g^j * C_0^j * exp(2*pi*i*gamma_j*t)|^2, where j runs over Bloch waves, C are eigenvector components, gamma are eigenvalues, and t is thickness.

HOLZ (Higher-Order Laue Zone) lines appear as fine dark/bright lines within the CBED disks, arising from reflections in upper Laue zones. They are sensitive to the c-axis lattice parameter and are useful for 3D structure analysis.
""")

# ============================================================
# 7.4 CBED Simulation (JA) - without colon
# ============================================================
write_page("7.4-CBED-simulation-ja.md", """# CBEDシミュレーション

**CBED（収束電子回折）**シミュレーションは、ブロッホ波（Bethe）法を使用して収束ビームパターンを計算・表示します。CBEDパターンはスポットの代わりに回折ディスクを示し、結晶の対称性、厚さ、構造に関する豊富な情報を含みます。

このウィンドウを開くには、[[7. Diffraction simulator-ja|回折シミュレータ]]の入射ビームモードで**Convergence (CBED)**を選択し、**Execute**をクリックします。

---

## 入力パラメータ

### モード
- **CBED**: 標準的な収束ビームパターン。各ディスクは一つの反射に対応し、透過ディスク (000) が中心にあります。
- **LACBED**（大角度CBED）: 大角度収束ビームパターン。異なる反射からのディスクが重なり合います。高次ラウエゾーン (HOLZ) 線や対称性の観察に有用です。

### 収束半角 (mrad)
収束ビーム円錐の半角。各回折ディスクのサイズを決定します。典型的な値: 5-30 mrad。逆空間でのディスク直径は 2*alpha です。

### ディスク解像度 (mrad/pixel)
各ディスク内の角度分解能。値が小さいほど高解像度ですが、計算時間は二乗で増加します（総ピクセル数 = (2*alpha/resolution)^2）。ビーム方向（ピクセル）の総数はステータスに表示されます。

### 最大ブロッホ波数
各入射ビーム方向でのブロッホ波計算に含まれるビームの最大数。ビーム数が多いほど正確ですが、計算時間が増加します（ピクセルあたり O(N^3) の固有値問題）。典型的な値: 100-500。

### 厚さ範囲
試料厚さ (nm) の開始値、終了値、ステップ値を設定します。CBEDパターンは厚さに強く依存し、固有値問題はビーム方向ごとに1回だけ解けばよいため、複数の厚さを同時に計算するのが効率的です。

### ソルバー
固有値問題の線形代数バックエンド：
- **Auto**: 最適なソルバーを自動選択。
- **Eigenproblem (MKL)**: Intel MKLベースの固有値ソルバー（最速、MKL要）。
- **Eigenproblem (Eigen)**: Eigen C++ライブラリソルバー。
- **Managed**: 純粋な .NET マネージドソルバー（最も遅いが常に利用可能）。

### スレッド数
計算の並列スレッド数。最高のパフォーマンスにはCPUコア数を設定します。

### ガイド円
チェックすると、各回折ディスクの境界（ブラッグ角円）を示す円を描画します。

---

## 実行 / 停止

**Execute** をクリックしてCBEDシミュレーションを開始します。進捗バーに完了率、経過時間、推定残り時間が表示されます。**Stop** をクリックして計算をキャンセルできます。

---

## 出力コントロール

計算完了後、出力パネルが有効になります：

### 表示モード
- **All disks**: すべてのディスクを含む完全なCBEDパターンを表示。
- **Individual disk**: 選択した単一のディスクをフル解像度で表示。

### 厚さスライダー
表示する厚さを選択（計算された厚さ範囲から）。

### 明るさ (Min / Max)
表示の最小・最大強度を調整。弱い特徴を引き出すのに有用です。

### ガンマ
強度表示のガンマ補正。1未満の値は弱い特徴を強調、1を超える値は強い特徴を強調します。

### カラースケール
カラーマップを選択: Gray、Spectrum（レインボー）など。

### グラデーション
パターンのグラデーション表示モードを設定。

---

## 物理的背景

CBEDでは、入射ビームは異なる方向の平面波の円錐です。各方向について、ブロッホ波法が結晶内の電子シュレーディンガー方程式を解きます：

1. 結晶ポテンシャル V(r) がフーリエ成分 U_g で展開されます。
2. 電子波動関数がブロッホ波の和として展開されます。
3. これによりサイズ N（ビーム数）の固有値問題が導かれます。
4. 固有値がブロッホ波の波数ベクトル、固有ベクトルがブロッホ波の振幅を与えます。
5. 入射面と出射面での境界条件が回折ビーム強度を決定します。

CBEDディスク内の各ピクセルでの強度は I_g(k) = |sum_j C_g^j * C_0^j * exp(2*pi*i*gamma_j*t)|^2 です。ここで j はブロッホ波、C は固有ベクトル成分、gamma は固有値、t は厚さです。

HOLZ（高次ラウエゾーン）線は、上位ラウエゾーンの反射から生じるCBEDディスク内の細い暗/明線として現れます。c軸の格子定数に敏感で、3次元構造解析に有用です。
""")

# ============================================================
# 0. Main Window (EN)
# ============================================================
write_page("0.-Main-window.md", """# Main Window

When ReciPro launches, the main window appears. From this window you select the crystal, control its orientation, and invoke various simulation and analysis functions.

![Main Window](images/FormMain.png)

---

## File menu

![Menu bar](images/FormMain.menuStrip.png)

### File
| Menu item | Description |
|-----------|-------------|
| Read crystal list (as new list) | Load a crystal list file (*.xml), replacing the current list |
| Read crystal list (and add) | Append crystals from a file to the current list |
| Read initial crystal list | Reload the default crystal list shipped with ReciPro |
| Save crystal list | Save the current crystal list to an XML file |
| Export selected crystal to CIF | Export the currently selected crystal in CIF format |
| Clear crystal list | Remove all crystals from the list |
| Exit | Close the application |

### Option
| Menu item | Description |
|-----------|-------------|
| Tool tip | Toggle tooltip display on/off |
| Reset registry (after restart) | Clear all saved settings; takes effect on next restart |
| Disable OpenGL (needs restart) | Disables hardware-accelerated OpenGL rendering. Use this if the application crashes on startup due to GPU driver issues or when running via remote desktop |

### Help
Program updates, version history, license information, link to the GitHub page, bug report form, PDF manual download, and language switch (English/Japanese; requires restart).

---

## Toolbar

![Toolbar](images/FormMain.toolStripContainer1.toolStrip1.png)

Quick-access buttons for the most common functions:
- Crystal database, Symmetry information, Scattering factor
- Rotation geometry, Structure viewer, Stereonet
- Crystal diffraction, HRTEM simulator, TEM ID, Spot ID
- Powder diffraction

---

## Crystal List

![Crystal List](images/FormMain.toolStripContainer1.splitContainer.groupBoxCrystalList.png)

The crystal list contains approximately 80 pre-installed crystals. Select a crystal to view its details and use it for simulations.

| Button | Action |
|--------|--------|
| Up / Down | Reorder the selected crystal in the list |
| Delete | Remove the selected crystal |
| All clear | Remove all crystals from the list |
| Add | Add the crystal defined in Crystal Information as a new entry |
| Replace | Replace the selected crystal with the one defined in Crystal Information |

You can also drag-and-drop CIF or AMC files onto the crystal list to import crystals.

---

## Crystal Information

![Crystal Information](images/FormMain.toolStripContainer1.splitContainer.groupBox6.png)

Edit the crystal parameters for the selected crystal. Changes are not saved until you press **Add** or **Replace**.

### Basic Info tab

![Basic Info](images/FormMain.tabPageBasicInfo.png)

- **Crystal name**: Name identifier for the crystal.
- **Crystal system**: Select from Triclinic, Monoclinic, Orthorhombic, Tetragonal, Trigonal, Hexagonal, or Cubic.
- **Space group**: Select the space group from the list (filtered by crystal system). The space group determines all symmetry operations.
- **Lattice parameters**: a, b, c (in angstroms) and alpha, beta, gamma (in degrees). Constrained parameters are automatically linked based on the crystal system (e.g., for cubic: a = b = c, alpha = beta = gamma = 90).
- **Color**: Set a display color for this crystal (used in structure viewer, stereonet, etc.).

### Atom tab

![Atoms](images/FormMain.tabPageAtom.png)

Define the atomic positions in the asymmetric unit:

- **Element**: Select the element from the periodic table.
- **Label**: Custom label for the atom site.
- **Position (x, y, z)**: Fractional coordinates in the unit cell.
- **Occupancy**: Site occupancy (0 to 1). For mixed sites, set values less than 1.
- **Debye-Waller factor**: Isotropic (B_iso) or anisotropic (B_11, B_22, ...) temperature factors. These describe atomic thermal vibrations and are essential for accurate structure factor and HAADF-STEM intensity calculations.
- **Scattering factor**: View X-ray, electron, and neutron scattering factors for the selected element.

### Reference tab

![Reference](images/FormMain.tabPageReference.png)

Store bibliographic information (journal, authors, title, etc.) for the crystal data source.

### EOS tab

![Equation of State](images/FormMain.tabPageEOS.png)

Equation of state parameters for high-pressure studies. Supports Birch-Murnaghan, Vinet, and other EOS models.

### Elasticity tab

![Elasticity](images/FormMain.tabPageElasticity.png)

Elastic stiffness constants (C_ij) for the crystal.

---

## Rotation control

See [[0.1. Crystal orientation control]] for detailed instructions.

### Current direction
Shows the current crystal orientation as a 3D axis indicator. Drag to rotate the crystal. Axes: red = *a*, green = *b*, blue = *c*.

### Reset rotation
Resets to the initial orientation: *c*-axis perpendicular to the screen, *b*-axis upward.

### Zone axis
Displays the closest zone axis [uvw] to the screen normal (searches indices where |u|+|v|+|w| < 30).

### Euler angles (Z-X-Z convention)
- **Psi**: First rotation about the Z-axis.
- **Theta**: Second rotation about the new X-axis.
- **Phi**: Third rotation about the new Z-axis.
See [[Appendix A1. Coordinate System]] for the full coordinate system definition.

### Arrow buttons / Animation
Rotate the crystal by the angle Step (in degrees). Check **Animation** for continuous rotation.

### Project along...
Align a specific zone axis [uvw] or crystal plane (hkl) perpendicular to the screen.

---

## Functions

| Button | Description | Details |
|--------|-------------|---------|
| Symmetry information | Space group symmetry operations and Wyckoff positions | - |
| Scattering factor | Crystal planes, d-spacings, and structure factors | - |
| Rotation geometry | 3D rotation matrix and Euler angle conversion | [[3. Rotation Geometry]] |
| Structure viewer | 3D crystal structure visualization | [[5. Structure viewer]] |
| Stereonet | Stereographic projection of crystal directions | [[6. Stereonet]] |
| Crystal diffraction | Diffraction pattern simulation | [[7. Diffraction simulator]] |
| HRTEM simulation | High-resolution TEM and STEM image simulation | [[8. HRTEM/STEM simulator]] |
| TEM ID | Index SAED patterns from unknown crystals | [[9. TEM ID]] |
| Spot ID | Detect and index spots from experimental SAED images | [[10. Spot ID]] |
| Powder diffraction | Polycrystalline (powder) diffraction simulation | - |
""")

# ============================================================
# 0. Main Window (JA)
# ============================================================
write_page("0.-Main-window-ja.md", """# メインウィンドウ

ReciProを起動すると、メインウィンドウが表示されます。このウィンドウから結晶を選択し、方位を制御し、各種シミュレーション・解析機能を呼び出します。

![メインウィンドウ](images/FormMain.png)

---

## ファイルメニュー

![メニューバー](images/FormMain.menuStrip.png)

### File（ファイル）
| メニュー項目 | 説明 |
|-------------|------|
| Read crystal list (as new list) | 結晶リストファイル (*.xml) を読み込み、現在のリストを置換 |
| Read crystal list (and add) | ファイルから結晶を現在のリストに追加 |
| Read initial crystal list | ReciProに同梱されたデフォルト結晶リストを再読み込み |
| Save crystal list | 現在の結晶リストをXMLファイルに保存 |
| Export selected crystal to CIF | 選択中の結晶をCIF形式でエクスポート |
| Clear crystal list | リストからすべての結晶を削除 |
| Exit | アプリケーションを終了 |

### Option（オプション）
| メニュー項目 | 説明 |
|-------------|------|
| Tool tip | ツールチップ表示のオン/オフ切替 |
| Reset registry (after restart) | 保存された設定をすべてクリア（次回再起動時に有効） |
| Disable OpenGL (needs restart) | ハードウェアアクセラレーションOpenGLレンダリングを無効化。GPUドライバの問題でアプリケーションが起動時にクラッシュする場合やリモートデスクトップ使用時に利用 |

### Help（ヘルプ）
プログラム更新、バージョン履歴、ライセンス情報、GitHubページへのリンク、バグ報告フォーム、PDFマニュアルダウンロード、言語切替（英語/日本語、再起動が必要）。

---

## ツールバー

![ツールバー](images/FormMain.toolStripContainer1.toolStrip1.png)

よく使う機能へのクイックアクセスボタン：
- 結晶データベース、対称性情報、散乱因子
- 回転ジオメトリ、構造ビューア、ステレオネット
- 結晶回折、HRTEMシミュレータ、TEM ID、Spot ID
- 粉末回折

---

## 結晶リスト

![結晶リスト](images/FormMain.toolStripContainer1.splitContainer.groupBoxCrystalList.png)

結晶リストには約80個のプリインストール結晶が含まれています。結晶を選択すると詳細が表示され、シミュレーションに使用できます。

| ボタン | 動作 |
|--------|------|
| Up / Down | 選択した結晶のリスト内順序を変更 |
| Delete | 選択した結晶を削除 |
| All clear | リストからすべての結晶を削除 |
| Add | 結晶情報で定義した結晶を新しいエントリとして追加 |
| Replace | 選択した結晶を結晶情報で定義したものに置換 |

CIFファイルやAMCファイルを結晶リストにドラッグ＆ドロップして結晶をインポートすることもできます。

---

## 結晶情報

![結晶情報](images/FormMain.toolStripContainer1.splitContainer.groupBox6.png)

選択した結晶のパラメータを編集します。変更は **Add** または **Replace** を押すまで保存されません。

### Basic Info（基本情報）タブ

![基本情報](images/FormMain.tabPageBasicInfo.png)

- **Crystal name（結晶名）**: 結晶の名前。
- **Crystal system（結晶系）**: 三斜、単斜、斜方、正方、三方、六方、立方から選択。
- **Space group（空間群）**: リストから空間群を選択（結晶系でフィルタ）。空間群がすべての対称操作を決定します。
- **Lattice parameters（格子定数）**: a, b, c（オングストローム）および alpha, beta, gamma（度）。結晶系に基づいて制約されたパラメータは自動的に連動します（例: 立方晶: a = b = c, alpha = beta = gamma = 90）。
- **Color（色）**: この結晶の表示色を設定（構造ビューア、ステレオネットなどで使用）。

### Atom（原子）タブ

![原子](images/FormMain.tabPageAtom.png)

非対称単位内の原子位置を定義：

- **Element（元素）**: 周期表から元素を選択。
- **Label（ラベル）**: 原子サイトのカスタムラベル。
- **Position (x, y, z)（位置）**: 単位胞内の分率座標。
- **Occupancy（占有率）**: サイト占有率（0から1）。混合サイトの場合は1未満に設定。
- **Debye-Waller factor（デバイ・ワラー因子）**: 等方性 (B_iso) または異方性 (B_11, B_22, ...) 温度因子。原子の熱振動を記述し、正確な構造因子およびHAADF-STEM強度計算に不可欠です。
- **Scattering factor（散乱因子）**: 選択した元素のX線、電子線、中性子線散乱因子を表示。

### Reference（参考文献）タブ

![参考文献](images/FormMain.tabPageReference.png)

結晶データソースの文献情報（ジャーナル、著者、タイトルなど）を保存。

### EOS（状態方程式）タブ

![状態方程式](images/FormMain.tabPageEOS.png)

高圧研究用の状態方程式パラメータ。Birch-Murnaghan、Vinetなどのモデルに対応。

### Elasticity（弾性定数）タブ

![弾性定数](images/FormMain.tabPageElasticity.png)

結晶の弾性スティフネス定数 (C_ij)。

---

## 回転制御

詳細は [[0.1. Crystal orientation control]] を参照。

### 現在の方向
現在の結晶方位が3D軸インジケータとして表示されます。ドラッグして結晶を回転できます。軸: 赤 = *a*、緑 = *b*、青 = *c*。

### 回転リセット
初期方位にリセット: *c*軸が画面に垂直、*b*軸が上向き。

### 晶帯軸
画面法線に最も近い晶帯軸 [uvw] を表示（|u|+|v|+|w| < 30 の範囲で探索）。

### オイラー角（Z-X-Z 規約）
- **Psi**: Z軸周りの第1回転。
- **Theta**: 新X軸周りの第2回転。
- **Phi**: 新Z軸周りの第3回転。
座標系の完全な定義は [[Appendix A1. Coordinate System-ja|座標系の定義]] を参照。

### 矢印ボタン / アニメーション
Step（度単位）の角度で結晶を回転。**Animation** にチェックを入れると連続回転。

### 投影方向指定
特定の晶帯軸 [uvw] または結晶面 (hkl) を画面に垂直に合わせます。

---

## 機能一覧

| ボタン | 説明 | 詳細 |
|--------|------|------|
| Symmetry information | 空間群の対称操作とワイコフ位置 | - |
| Scattering factor | 結晶面、面間隔、構造因子 | - |
| Rotation geometry | 3D回転行列とオイラー角変換 | [[3. Rotation Geometry-ja|回転ジオメトリ]] |
| Structure viewer | 3D結晶構造の可視化 | [[5. Structure viewer-ja|Structure Viewer]] |
| Stereonet | 結晶方向のステレオ投影 | [[6. Stereonet-ja|ステレオネット]] |
| Crystal diffraction | 回折パターンシミュレーション | [[7. Diffraction simulator-ja|回折シミュレータ]] |
| HRTEM simulation | 高分解能TEMおよびSTEM像シミュレーション | [[8. HRTEM/STEM simulator-ja|HRTEM/STEMシミュレータ]] |
| TEM ID | 未知結晶のSAEDパターンの指数付け | [[9. TEM ID-ja|TEM ID]] |
| Spot ID | 実験SAED画像からのスポット検出と指数付け | [[10. Spot ID-ja|Spot ID]] |
| Powder diffraction | 多結晶（粉末）回折シミュレーション | - |
""")

# ============================================================
# 5. Structure Viewer (EN)
# ============================================================
write_page("5.-Structure-viewer.md", """# Structure Viewer

**Structure Viewer** displays the 3D crystal structure using OpenGL rendering. Atoms, bonds, unit cells, lattice planes, and polyhedra can be visualized interactively.

![Structure Viewer](images/FormStructureViewer.png)

---

## Mouse operations
- **Left drag**: Rotate the structure.
- **Right drag / scroll**: Zoom in and out.
- **Middle drag**: Translate the view.

---

## Toolbar

![Toolbar](images/FormStructureViewer.toolStrip1.png)

Quick access to view controls: reset view, projection mode (perspective/orthographic), axis display, and screenshot capture.

---

## Tab panels

### Bounds tab

![Bounds](images/FormStructureViewer.splitContainer1.tabControl.tabPageBounds.png)

Set the display range of the structure in fractional coordinates:
- **Range (a, b, c)**: Minimum and maximum fractional coordinates along each axis. For example, setting a = 0 to 2 shows two unit cells along *a*.
- **Show atoms outside bounds**: Toggle whether atoms outside the specified range are displayed.

### Atom tab

![Atoms](images/FormStructureViewer.splitContainer1.tabControl.tabPageAtom.png)

Control the appearance of individual atoms:
- **Visibility**: Show or hide specific atom sites.
- **Radius**: Adjust the display radius for each atom type.
- **Color**: Set the color for each atom type (default colors based on element).
- **Transparency**: Adjust the transparency of atoms.

### Bond tab

![Bonds](images/FormStructureViewer.splitContainer1.tabControl.tabPageBond.png)

Define and display interatomic bonds:
- **Add bond**: Specify two atom types and a distance range to create bonds.
- **Bond radius**: Adjust the visual thickness of bonds.
- **Polyhedra**: Generate coordination polyhedra from defined bonds.

### Unit cell tab

![Unit cell](images/FormStructureViewer.splitContainer1.tabControl.tabPageUnitCell.png)

Control the display of the unit cell frame:
- **Show unit cell**: Toggle the unit cell wireframe.
- **Line width**: Adjust the thickness of the cell edges.
- **Color**: Set the color of the unit cell lines.
- **Show axes labels**: Display a, b, c axis labels.

### Lattice plane tab

![Lattice plane](images/FormStructureViewer.splitContainer1.tabControl.tabPageLatticePlane.png)

Display crystallographic lattice planes (hkl):
- **Add plane**: Specify Miller indices to add a lattice plane.
- **Color and transparency**: Customize the appearance of each plane.
- **d-spacing**: The plane spacing is computed and displayed automatically.

### Coordinate information tab

![Coordinate info](images/FormStructureViewer.splitContainer1.tabControl.tabPageCoordinateInformation.png)

Display information about the current crystal and viewing geometry:
- Lattice parameters, volume, density.
- Rotation matrix and Euler angles.
- Atom positions and symmetry-equivalent sites.

### Misc tab

![Misc](images/FormStructureViewer.splitContainer1.tabControl.tabPageMisc.png)

Miscellaneous settings:
- **Background color**: Set the background color of the 3D view.
- **Light position**: Adjust the direction of the light source.
- **Ambient/Diffuse/Specular**: Material lighting properties.

### Projection tab

![Projection](images/FormStructureViewer.splitContainer1.tabControl.tabPageProjection.png)

- **Perspective / Orthographic**: Switch between perspective (realistic depth) and orthographic (no depth distortion) projection.
- **Field of view**: Adjust the perspective field of view angle.

### Information tab

![Information](images/FormStructureViewer.splitContainer1.tabControl.tabPageInformation.png)

Displays computed crystal properties: cell volume, density, formula, and space group information.
""")

# ============================================================
# 5. Structure Viewer (JA)
# ============================================================
write_page("5.-Structure-viewer-ja.md", """# Structure Viewer

**Structure Viewer** は、OpenGLレンダリングによる3D結晶構造の表示機能です。原子、結合、単位胞、格子面、多面体をインタラクティブに可視化できます。

![Structure Viewer](images/FormStructureViewer.png)

---

## マウス操作
- **左ドラッグ**: 構造の回転。
- **右ドラッグ / スクロール**: ズームイン・アウト。
- **中ドラッグ**: 視点の平行移動。

---

## ツールバー

![ツールバー](images/FormStructureViewer.toolStrip1.png)

ビュー制御のクイックアクセス: 表示リセット、投影モード（透視/正射影）、軸表示、スクリーンショット取得。

---

## タブパネル

### Bounds（表示範囲）タブ

![Bounds](images/FormStructureViewer.splitContainer1.tabControl.tabPageBounds.png)

分率座標での構造の表示範囲を設定：
- **Range (a, b, c)**: 各軸に沿った最小・最大分率座標。例えば a = 0 から 2 に設定すると *a* 軸方向に2単位胞を表示。
- **Show atoms outside bounds**: 指定範囲外の原子を表示するかどうか切替。

### Atom（原子）タブ

![Atoms](images/FormStructureViewer.splitContainer1.tabControl.tabPageAtom.png)

個々の原子の表示を制御：
- **Visibility（可視性）**: 特定の原子サイトの表示/非表示。
- **Radius（半径）**: 各原子タイプの表示半径を調整。
- **Color（色）**: 各原子タイプの色を設定（デフォルトは元素に基づく色）。
- **Transparency（透明度）**: 原子の透明度を調整。

### Bond（結合）タブ

![Bonds](images/FormStructureViewer.splitContainer1.tabControl.tabPageBond.png)

原子間結合の定義と表示：
- **Add bond（結合追加）**: 2つの原子タイプと距離範囲を指定して結合を作成。
- **Bond radius（結合半径）**: 結合の視覚的な太さを調整。
- **Polyhedra（多面体）**: 定義された結合から配位多面体を生成。

### Unit cell（単位胞）タブ

![Unit cell](images/FormStructureViewer.splitContainer1.tabControl.tabPageUnitCell.png)

単位胞フレームの表示制御：
- **Show unit cell**: 単位胞のワイヤーフレームの表示切替。
- **Line width（線幅）**: 胞辺の太さを調整。
- **Color（色）**: 単位胞線の色を設定。
- **Show axes labels**: a, b, c 軸ラベルの表示。

### Lattice plane（格子面）タブ

![Lattice plane](images/FormStructureViewer.splitContainer1.tabControl.tabPageLatticePlane.png)

結晶学的格子面 (hkl) の表示：
- **Add plane（面追加）**: ミラー指数を指定して格子面を追加。
- **Color and transparency（色と透明度）**: 各面の外観をカスタマイズ。
- **d-spacing（面間隔）**: 面間隔は自動的に計算・表示。

### Coordinate information（座標情報）タブ

![座標情報](images/FormStructureViewer.splitContainer1.tabControl.tabPageCoordinateInformation.png)

現在の結晶と表示ジオメトリの情報を表示：
- 格子定数、体積、密度。
- 回転行列とオイラー角。
- 原子位置と対称等価サイト。

### Misc（その他）タブ

![Misc](images/FormStructureViewer.splitContainer1.tabControl.tabPageMisc.png)

その他の設定：
- **Background color（背景色）**: 3Dビューの背景色を設定。
- **Light position（光源位置）**: 光源の方向を調整。
- **Ambient/Diffuse/Specular**: マテリアルの照明プロパティ。

### Projection（投影）タブ

![Projection](images/FormStructureViewer.splitContainer1.tabControl.tabPageProjection.png)

- **Perspective / Orthographic**: 透視投影（リアルな奥行き）と正射影（奥行き歪みなし）の切替。
- **Field of view（視野角）**: 透視投影の視野角を調整。

### Information（情報）タブ

![Information](images/FormStructureViewer.splitContainer1.tabControl.tabPageInformation.png)

計算された結晶プロパティを表示: 単位胞体積、密度、化学式、空間群情報。
""")

# ============================================================
# 6. Stereonet (EN)
# ============================================================
write_page("6.-Stereonet.md", """# Stereonet

**Stereonet** displays crystal plane and axis directions using stereographic projection. This is a standard tool in crystallography for visualizing the angular relationships between crystal planes and directions.

![Stereonet](images/FormStereonet.png)

---

## Mouse operations

| Operation | Action |
|-----------|--------|
| Left drag | Rotate the crystal |
| Right drag / click | Zoom in / out |
| Double-click | Switch between planes and axes display modes |
| Middle drag | Translate the projection |

---

## Main display area

![Main display](images/FormStereonet.splitContainer1.graphicsBox.png)

The central area shows the stereographic projection circle. Crystal directions (axes) or crystal planes are plotted as points or great circles within this projection.

---

## File menu
Save or copy the stereonet in raster (PNG, BMP) or vector (EMF) format. Vector format allows editing font/line thickness in PowerPoint and other vector graphics editors.

---

## Mode
- **Axis** or **Plane** projection: Toggle between showing crystal axes [uvw] as points or crystal planes (hkl) as great circle traces.
- **Wulff** (equal-angle) or **Schmidt** (equal-area): Choose the projection type. Wulff preserves angles; Schmidt preserves areas (useful for statistical analysis of orientation distributions).

---

## Indices

### Range
Specify the maximum range of [uvw] or {hkl} indices to display. Higher values show more directions/planes but may clutter the projection.

### Specified
Add individual indices with optional display of crystallographic equivalents (all symmetry-related directions/planes).

---

## Tab panels

### Appearance tab

![Appearance](images/FormStereonet.tabControl.tabPage1.png)

- **String size**: Font size for index labels.
- **Point size**: Size of the plotted points.
- **Color**: Color of the points and labels.
- **Outline**: Show/hide the projection circle outline.

### Great and Small Circle tab

![Circles](images/FormStereonet.tabControl.tabPage2.png)

Draw great circles and small circles on the stereonet:
- **Great circle by zone axis**: Specify [uvw] to draw the great circle of all planes containing that zone axis.
- **Great circle by crystal plane**: Specify (hkl) to draw the trace of that plane.
- **Small circle**: Draw a small circle at a specified angular distance from a pole.
""")

# ============================================================
# 6. Stereonet (JA)
# ============================================================
write_page("6.-Stereonet-ja.md", """# ステレオネット

**ステレオネット**は、ステレオ投影を使用して結晶面と軸の方向を表示します。結晶面と方向の角度関係を可視化するための結晶学の標準ツールです。

![ステレオネット](images/FormStereonet.png)

---

## マウス操作

| 操作 | 動作 |
|------|------|
| 左ドラッグ | 結晶の回転 |
| 右ドラッグ/クリック | ズームイン/アウト |
| ダブルクリック | 面/軸の表示モード切替 |
| 中ドラッグ | 投影の平行移動 |

---

## メイン表示エリア

![メイン表示](images/FormStereonet.splitContainer1.graphicsBox.png)

中央エリアにステレオ投影円が表示されます。結晶方向（軸）または結晶面が投影円内に点や大円として描画されます。

---

## ファイルメニュー
ステレオネットをラスター（PNG, BMP）またはベクター（EMF）形式で保存・コピー。ベクター形式ではPowerPointなどでフォント/線の太さを編集可能です。

---

## モード
- **Axis** または **Plane** 投影: 結晶軸 [uvw] を点として表示するか、結晶面 (hkl) を大円トレースとして表示するかを切替。
- **Wulff**（等角度）または **Schmidt**（等面積）: 投影タイプを選択。Wulffは角度を保存、Schmidtは面積を保存（方位分布の統計解析に有用）。

---

## 指数

### 範囲
表示する [uvw] または {hkl} 指数の最大範囲を指定。値が大きいほど多くの方向/面が表示されますが、投影が雑然とする場合があります。

### 指定
個別の指数を追加し、結晶学的等価方向/面（すべての対称等価な方向/面）のオプション表示も可能。

---

## タブパネル

### 外観タブ

![外観](images/FormStereonet.tabControl.tabPage1.png)

- **String size（文字サイズ）**: 指数ラベルのフォントサイズ。
- **Point size（点サイズ）**: プロットされる点のサイズ。
- **Color（色）**: 点とラベルの色。
- **Outline（アウトライン）**: 投影円の輪郭線の表示/非表示。

### 大円・小円タブ

![円](images/FormStereonet.tabControl.tabPage2.png)

ステレオネット上に大円と小円を描画：
- **晶帯軸による大円**: [uvw] を指定して、その晶帯軸を含むすべての面の大円を描画。
- **結晶面による大円**: (hkl) を指定してその面のトレースを描画。
- **小円**: 極から指定した角度距離の小円を描画。
""")

# ============================================================
# 8. HRTEM/STEM Simulator (EN)
# ============================================================
write_page("8.-HRTEM-STEM-simulator.md", """# HRTEM / STEM Simulator

**HRTEM/STEM Simulator** simulates high-resolution transmission electron microscopy (HRTEM) lattice fringe images and scanning TEM (STEM) images using the multislice or Bloch wave method.

![HRTEM/STEM Simulator](images/FormImageSimulator.png)

---

## Main area

The central area displays the simulated image. The brightness range, colour scale, and Gaussian blur can be adjusted.

---

## Image mode

Select the simulation type:
- **HRTEM**: High-resolution TEM lattice fringe image.
- **Potential**: Display the projected crystal potential (useful for understanding the relationship between structure and image).
- **STEM**: Scanning TEM image (BF, ABF, LAADF, HAADF detectors).

---

## Simulation property

![Simulation property](images/FormImageSimulator.splitContainer1.groupBox1.png)

- **Max Bloch waves**: Maximum number of Bloch waves in the calculation. More waves give better accuracy for thicker specimens.
- **Image pixels**: The resolution of the simulated image (pixels along each direction).
- **Partial coherence**:
  - **Quasi-coherent** (fast): Applies envelope functions to approximate partial coherence effects.
  - **TCC** (Transmission Cross Coefficient, accurate): Full integration over source size and defocus spread. More accurate but significantly slower.
- **Single mode**: Compute a single image at one thickness and defocus.
- **Serial mode**: Compute images at multiple thicknesses and/or defocus values to generate thickness-defocus maps.

---

## Optical property

![Optical property](images/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.png)

### TEM condition
- **Accelerating voltage**: Electron beam energy (e.g. 200 kV).
- **Defocus**: Distance from exact focus. Negative values = underfocus (Scherzer defocus is displayed as a reference).

### Inherent aberrations

![Inherent](images/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBox4.png)

- **Cs** (mm): Spherical aberration coefficient. Determines the point resolution of the microscope.
- **Cc** (mm): Chromatic aberration coefficient.
- **Beta** (mrad): Beam convergence semi-angle (source size effect).
- **Delta-E** (eV): Energy spread of the electron source.

### Lens function
Phase contrast transfer function (PCTF), spatial coherence envelope, and temporal coherence envelope are displayed graphically. The first crossover of the PCTF determines the point resolution.

### HRTEM-specific options

![HRTEM options](images/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxHREMoption1.png)

- **Objective aperture**: Size and position of the objective aperture. The number of diffracted beams included depends on the aperture size.

### STEM-specific options

![STEM options](images/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxSTEMoption1.png)

- **Convergence angle** (mrad): The semi-angle of the convergent STEM probe.
- **Detector angles**: Inner and outer collection angles for each detector type.
- **Source size**: Effective source size (affects spatial resolution).
- **Slice thickness**: Thickness of each multislice propagation step.
- **Angular resolution**: Resolution in the angular integration.

---

## STEM detector types

| Detector | Collection angle | Contribution |
|----------|-----------------|--------------|
| BF (Bright Field) | Small inner angle | Elastic scattering |
| ABF (Annular Bright Field) | Annular, inside BF disk | Elastic; sensitive to light elements |
| LAADF (Low-Angle ADF) | Medium angles | Mix of elastic and TDS |
| HAADF (High-Angle ADF) | Large angles | Predominantly thermal diffuse scattering (TDS) |

> Set temperature factors (Debye-Waller factors) to non-zero values for TDS contribution. If unknown, B = 0.5 A^2 is a reasonable starting value. HAADF intensity scales approximately as Z^2 (atomic number squared), making it useful for chemical contrast.

---

## Appearance

- **Label**: Show thickness and defocus values on each image.
- **Scale bar**: Display a scale bar with the real-space scale.
- **Unit cell overlay**: Overlay the unit cell outline on the simulated image.
""")

# ============================================================
# 8. HRTEM/STEM Simulator (JA)
# ============================================================
write_page("8.-HRTEM-STEM-simulator-ja.md", """# HRTEM / STEMシミュレータ

**HRTEM/STEMシミュレータ**は、マルチスライス法またはブロッホ波法を用いて、高分解能透過電子顕微鏡（HRTEM）格子縞像および走査TEM（STEM）像をシミュレーションします。

![HRTEM/STEMシミュレータ](images/FormImageSimulator.png)

---

## メインエリア

中央エリアにシミュレーション画像が表示されます。明るさ範囲、カラースケール、ガウシアンぼかしを調整できます。

---

## 画像モード

シミュレーションタイプを選択：
- **HRTEM**: 高分解能TEM格子縞像。
- **Potential**: 射影結晶ポテンシャルの表示（構造と画像の関係を理解するのに有用）。
- **STEM**: 走査TEM像（BF、ABF、LAADF、HAADF検出器）。

---

## シミュレーションプロパティ

![シミュレーションプロパティ](images/FormImageSimulator.splitContainer1.groupBox1.png)

- **Max Bloch waves（最大ブロッホ波数）**: 計算に含まれるブロッホ波の最大数。波数が多いほど厚い試料での精度が向上。
- **Image pixels（画像ピクセル数）**: シミュレーション画像の解像度（各方向のピクセル数）。
- **Partial coherence（部分コヒーレンス）**:
  - **Quasi-coherent（準コヒーレント、高速）**: エンベロープ関数で部分コヒーレンス効果を近似。
  - **TCC**（Transmission Cross Coefficient、正確）: 光源サイズとデフォーカススプレッドにわたる完全な積分。より正確ですが大幅に遅くなります。
- **Single mode**: 1つの厚さ・デフォーカスで1枚の画像を計算。
- **Serial mode**: 複数の厚さ・デフォーカス値で画像を計算し、厚さ-デフォーカスマップを生成。

---

## 光学プロパティ

![光学プロパティ](images/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.png)

### TEM条件
- **Accelerating voltage（加速電圧）**: 電子ビームエネルギー（例: 200 kV）。
- **Defocus（デフォーカス）**: 正焦点からの距離。負の値 = アンダーフォーカス（参考値としてシェルツァーデフォーカスが表示）。

### 固有収差

![固有収差](images/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBox4.png)

- **Cs** (mm): 球面収差係数。顕微鏡の点分解能を決定。
- **Cc** (mm): 色収差係数。
- **Beta** (mrad): ビーム収束半角（光源サイズ効果）。
- **Delta-E** (eV): 電子源のエネルギースプレッド。

### レンズ関数
位相コントラスト伝達関数（PCTF）、空間コヒーレンスエンベロープ、時間コヒーレンスエンベロープがグラフで表示されます。PCTFの最初の交差点が点分解能を決定します。

### HRTEM固有オプション

![HRTEMオプション](images/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxHREMoption1.png)

- **Objective aperture（対物絞り）**: 対物絞りのサイズと位置。含まれる回折ビーム数は絞りサイズに依存。

### STEM固有オプション

![STEMオプション](images/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxSTEMoption1.png)

- **Convergence angle（収束角）** (mrad): 収束STEMプローブの半角。
- **Detector angles（検出器角度）**: 各検出器タイプの内側・外側収集角。
- **Source size（光源サイズ）**: 有効光源サイズ（空間分解能に影響）。
- **Slice thickness（スライス厚さ）**: 各マルチスライス伝播ステップの厚さ。
- **Angular resolution（角度分解能）**: 角度積分の分解能。

---

## STEM検出器タイプ

| 検出器 | 収集角 | 寄与 |
|--------|--------|------|
| BF（明視野） | 小さい内角 | 弾性散乱 |
| ABF（環状明視野） | 環状、BFディスク内側 | 弾性；軽元素に敏感 |
| LAADF（低角度ADF） | 中程度の角度 | 弾性とTDSの混合 |
| HAADF（高角度ADF） | 大きい角度 | 主に熱散漫散乱（TDS） |

> TDS寄与のために温度因子（デバイ・ワラー因子）をゼロ以外に設定してください。不明な場合は B = 0.5 A^2 が合理的な初期値です。HAADF強度は近似的に Z^2（原子番号の2乗）にスケールし、化学コントラストに有用です。

---

## 外観

- **Label（ラベル）**: 各画像に厚さとデフォーカス値を表示。
- **Scale bar（スケールバー）**: 実空間スケールのスケールバーを表示。
- **Unit cell overlay（単位胞オーバーレイ）**: シミュレーション画像に単位胞の輪郭をオーバーレイ。
""")

# ============================================================
# 9. TEM ID (EN)
# ============================================================
write_page("9.-TEM-ID.md", """# TEM ID

**TEM ID** identifies unknown crystals from SAED (Selected Area Electron Diffraction) patterns. Given measured d-spacings and angles between reflections, it searches through the crystal database to find matching crystals and zone axes.

---

## How to use

1. Load a crystal database or use the default crystal list.
2. From an experimental SAED pattern, measure:
   - d-spacings of two or more reflections.
   - The angle between two reflections.
3. Enter these values into the input fields with appropriate tolerances.
4. Click **Search** to find matching crystals and zone axes.
5. Results are displayed in a table showing the crystal name, zone axis, and matching reflections with their indices.

---

## TEM conditions

- **Accelerating voltage**: Set the electron beam energy (e.g. 200 kV) for accurate d-spacing to scattering angle conversion.
- **Camera length**: The effective camera length of the TEM (affects the relationship between spot distance and d-spacing).

---

## Search parameters

- **d-spacing 1, 2** (nm): Measured d-spacings of two reflections.
- **Angle** (degrees): Measured angle between the two reflections.
- **Tolerance**: Acceptable deviation for d-spacings (in %) and angle (in degrees).
- **Max index**: Maximum Miller index to search (higher values search more reflections but take longer).

---

## Results

The search results table shows:
- **Crystal**: Name of the matching crystal.
- **Zone axis** [uvw]: The zone axis that produces the observed pattern.
- **Reflection 1, 2**: Miller indices (hkl) of the two matched reflections.
- **Calculated d-spacings**: d-spacings from the crystal structure for comparison.
- **Calculated angle**: Angle between the reflections from the crystal structure.

Double-click a result to set the crystal orientation in the main window.
""")

# ============================================================
# 9. TEM ID (JA)
# ============================================================
write_page("9.-TEM-ID-ja.md", """# TEM ID

**TEM ID** は、SAED（制限視野電子回折）パターンから未知結晶を同定します。測定した面間隔と反射間の角度から、結晶データベースを検索して一致する結晶と晶帯軸を見つけます。

---

## 使い方

1. 結晶データベースを読み込むか、デフォルトの結晶リストを使用。
2. 実験SAEDパターンから以下を測定：
   - 2つ以上の反射の面間隔。
   - 2つの反射間の角度。
3. これらの値を適切な許容誤差と共に入力フィールドに入力。
4. **Search** をクリックして一致する結晶と晶帯軸を検索。
5. 結果がテーブルに表示され、結晶名、晶帯軸、一致する反射とその指数が示されます。

---

## TEM条件

- **Accelerating voltage（加速電圧）**: 電子ビームエネルギー（例: 200 kV）を設定。正確な面間隔-散乱角変換に使用。
- **Camera length（カメラ長）**: TEMの有効カメラ長（スポット距離と面間隔の関係に影響）。

---

## 検索パラメータ

- **d-spacing 1, 2** (nm): 2つの反射の測定面間隔。
- **Angle（角度）** (degrees): 2つの反射間の測定角度。
- **Tolerance（許容誤差）**: 面間隔（%）と角度（度）の許容偏差。
- **Max index（最大指数）**: 検索する最大ミラー指数（大きい値はより多くの反射を検索しますが時間がかかります）。

---

## 結果

検索結果テーブルに表示される項目：
- **Crystal（結晶）**: 一致する結晶の名前。
- **Zone axis（晶帯軸）** [uvw]: 観測されたパターンを生じる晶帯軸。
- **Reflection 1, 2（反射 1, 2）**: 一致した2つの反射のミラー指数 (hkl)。
- **Calculated d-spacings（計算面間隔）**: 比較用の結晶構造からの面間隔。
- **Calculated angle（計算角度）**: 結晶構造からの反射間角度。

結果をダブルクリックすると、メインウィンドウで結晶方位が設定されます。
""")

# ============================================================
# 10. Spot ID (EN)
# ============================================================
write_page("10.-Spot-ID.md", """# Spot ID

**Spot ID** detects and indexes diffraction spots from experimental SAED (Selected Area Electron Diffraction) images. It can automatically find spots, fit their positions, and determine the crystal orientation by matching against calculated patterns.

![Spot ID](images/FormSpotIDv1.png)

---

## Workflow

1. **Load image**: Open an experimental SAED image (TIFF, PNG, BMP, etc.).
2. **Detect spots**: Automatically detect diffraction spots using peak-finding algorithms.
3. **Fit spots**: Refine spot positions by fitting 2D Gaussian profiles.
4. **Index**: Match detected spots against calculated patterns from the crystal database.

---

## Spot detection panel

![Spot detection](images/FormSpotIDv1.groupBox1.png)

- **Threshold**: Minimum intensity for spot detection. Adjust to filter out noise.
- **Min/Max radius**: Size range for detected spots (in pixels).
- **Find spots**: Run the automatic spot detection algorithm.
- **Delete spot**: Manually remove a false positive detection.
- **Clear spots**: Remove all detected spots.

---

## Fitting

After detection, spots can be refined:
- **Gaussian fit**: Fit a 2D Gaussian profile to each spot for sub-pixel accuracy.
- **Global fit**: Simultaneously refine all spot positions.

---

## Indexing

Once spots are detected and fitted, index the pattern:
- Select candidate crystals from the crystal list.
- The algorithm searches for zone axes that match the observed spot positions and relative angles.
- Results show the best-matching crystal, zone axis, and individual spot indices.
""")

# ============================================================
# 10. Spot ID (JA)
# ============================================================
write_page("10.-Spot-ID-ja.md", """# Spot ID

**Spot ID** は、実験SAED（制限視野電子回折）画像から回折スポットを検出し、指数付けを行います。自動的にスポットを検出し、位置をフィッティングし、計算パターンとのマッチングにより結晶方位を決定できます。

![Spot ID](images/FormSpotIDv1.png)

---

## ワークフロー

1. **画像読み込み**: 実験SAEDイメージ（TIFF, PNG, BMPなど）を開く。
2. **スポット検出**: ピーク検出アルゴリズムにより自動的に回折スポットを検出。
3. **スポットフィッティング**: 2Dガウシアンプロファイルのフィッティングによりスポット位置を精密化。
4. **指数付け**: 検出されたスポットを結晶データベースの計算パターンと照合。

---

## スポット検出パネル

![スポット検出](images/FormSpotIDv1.groupBox1.png)

- **Threshold（しきい値）**: スポット検出の最小強度。ノイズを除去するために調整。
- **Min/Max radius（最小/最大半径）**: 検出するスポットのサイズ範囲（ピクセル単位）。
- **Find spots（スポット検出）**: 自動スポット検出アルゴリズムを実行。
- **Delete spot（スポット削除）**: 誤検出を手動で削除。
- **Clear spots（全スポットクリア）**: 検出されたすべてのスポットを削除。

---

## フィッティング

検出後、スポットを精密化できます：
- **Gaussian fit（ガウシアンフィット）**: 各スポットに2Dガウシアンプロファイルをフィッティングしてサブピクセル精度を得る。
- **Global fit（グローバルフィット）**: すべてのスポット位置を同時に精密化。

---

## 指数付け

スポットの検出とフィッティング後、パターンを指数付け：
- 結晶リストから候補結晶を選択。
- アルゴリズムが観測されたスポット位置と相対角度に一致する晶帯軸を検索。
- 結果に最適な結晶、晶帯軸、各スポットの指数が表示されます。
""")

# ============================================================
# 10. Spot ID v2 (EN) - new page without colon
# ============================================================
write_page("10.1-Spot-ID-v2.md", """# Spot ID v2

**Spot ID v2** is the enhanced version of [[10. Spot ID|Spot ID]] with improved spot detection, fitting algorithms, and a more powerful indexing engine.

![Spot ID v2](images/FormSpotIDV2.png)

---

## Spot detection and manipulation

![Spot controls](images/FormSpotIDV2.splitContainer1.groupBox1.png)

- **Find spots**: Automatic spot detection with advanced peak-finding using local maxima and background subtraction.
- **Donut filter**: Applies a donut-shaped (annular) filter to enhance ring-shaped diffraction features and suppress the central beam.
- **Delete spot / Clear spots**: Remove individual or all detected spots.
- **Reset range for all spots**: Reset the fitting range for all spots to default.
- **Copy to clipboard**: Copy spot positions and intensities to the clipboard for external analysis.

---

## Fitting panel

![Fitting](images/FormSpotIDV2.splitContainer1.groupBox2.png)

- **Comprehensive fitting**: Performs a comprehensive fit of all spots simultaneously, optimizing positions, widths, and intensities.
- **Global fit**: Refines the overall pattern geometry (center position, scale, rotation) while keeping relative spot positions fixed.
- **Individual fit**: Fit each spot independently with a 2D Gaussian or Lorentzian profile.

---

## Indexing panel

![Indexing](images/FormSpotIDV2.splitContainer1.groupBox3.png)

- **Crystal selection**: Choose which crystals from the crystal list to use as candidates for indexing.
- **Search**: Run the indexing algorithm to find the best-matching crystal and zone axis.
- **Tolerance**: Set the acceptable deviation in d-spacing and angle for a match.
- **Results**: The best matches are displayed with crystal name, zone axis [uvw], and individual spot indices (hkl).

---

## Improvements over v1

- Better noise handling in spot detection.
- More robust fitting algorithms with multiple profile shapes.
- Faster indexing with optimized search algorithms.
- Support for overlapping spots and satellite reflections.
""")

# ============================================================
# 10. Spot ID v2 (JA)
# ============================================================
write_page("10.1-Spot-ID-v2-ja.md", """# Spot ID v2

**Spot ID v2** は [[10. Spot ID-ja|Spot ID]] の強化版で、スポット検出、フィッティングアルゴリズム、指数付けエンジンが改善されています。

![Spot ID v2](images/FormSpotIDV2.png)

---

## スポット検出と操作

![スポットコントロール](images/FormSpotIDV2.splitContainer1.groupBox1.png)

- **Find spots（スポット検出）**: 局所最大値とバックグラウンド除去を用いた高度なピーク検出による自動スポット検出。
- **Donut filter（ドーナツフィルタ）**: ドーナツ型（環状）フィルタを適用してリング状の回折特徴を強調し、中心ビームを抑制。
- **Delete spot / Clear spots**: 個別またはすべての検出スポットを削除。
- **Reset range for all spots**: すべてのスポットのフィッティング範囲をデフォルトにリセット。
- **Copy to clipboard**: スポット位置と強度をクリップボードにコピーして外部解析用に使用。

---

## フィッティングパネル

![フィッティング](images/FormSpotIDV2.splitContainer1.groupBox2.png)

- **Comprehensive fitting（包括的フィッティング）**: すべてのスポットを同時にフィッティングし、位置、幅、強度を最適化。
- **Global fit（グローバルフィット）**: 相対的なスポット位置を固定したまま、パターン全体のジオメトリ（中心位置、スケール、回転）を精密化。
- **Individual fit（個別フィット）**: 各スポットを独立に2DガウシアンまたはLorentzianプロファイルでフィッティング。

---

## 指数付けパネル

![指数付け](images/FormSpotIDV2.splitContainer1.groupBox3.png)

- **Crystal selection（結晶選択）**: 指数付けの候補として結晶リストのどの結晶を使用するか選択。
- **Search（検索）**: 指数付けアルゴリズムを実行して最適な結晶と晶帯軸を検索。
- **Tolerance（許容誤差）**: 一致判定の面間隔と角度の許容偏差を設定。
- **Results（結果）**: 最良の一致が結晶名、晶帯軸 [uvw]、各スポットの指数 (hkl) と共に表示。

---

## v1からの改善点

- スポット検出でのノイズ処理の改善。
- 複数のプロファイル形状に対応した、より堅牢なフィッティングアルゴリズム。
- 最適化された検索アルゴリズムによる高速な指数付け。
- 重なり合うスポットやサテライト反射のサポート。
""")

# ============================================================
# 1. Crystal Database (EN)
# ============================================================
write_page("1.-Crystal-database.md", """# Crystal Database

**Crystal Database** provides a searchable database of crystal structures. You can search by name, chemical formula, space group, or lattice parameters, and import crystals into the main crystal list.

![Crystal Database](images/FormCrystalDatabase.png)

---

## Database panel

![Database panel](images/FormCrystalDatabase.crystalDatabaseControl.png)

### Search options

- **Name**: Search by crystal or mineral name (partial match supported).
- **Formula**: Search by chemical formula (e.g., "SiO2", "Fe2O3").
- **Space group**: Filter by space group number or symbol.
- **Crystal system**: Filter by crystal system (Triclinic through Cubic).
- **Lattice parameters**: Search by approximate lattice parameters (a, b, c, alpha, beta, gamma) with tolerance.

### Results table

![Results table](images/FormCrystalDatabase.crystalDatabaseControl.dataGridView.png)

The results table shows matching crystals with:
- Crystal name
- Chemical formula
- Space group
- Lattice parameters (a, b, c, alpha, beta, gamma)
- Reference information

Select a crystal and click **Send to main form** to import it into the main crystal list.

---

## Data sources

The database includes data from:
- American Mineralogist Crystal Structure Database (AMCSD)
- Crystallography Open Database (COD) - optional download
- User-imported CIF files
""")

# ============================================================
# 1. Crystal Database (JA)
# ============================================================
write_page("1.-Crystal-database-ja.md", """# 結晶データベース

**結晶データベース**は、検索可能な結晶構造データベースを提供します。名前、化学式、空間群、格子定数で検索し、メインの結晶リストに結晶をインポートできます。

![結晶データベース](images/FormCrystalDatabase.png)

---

## データベースパネル

![データベースパネル](images/FormCrystalDatabase.crystalDatabaseControl.png)

### 検索オプション

- **Name（名前）**: 結晶名または鉱物名で検索（部分一致対応）。
- **Formula（化学式）**: 化学式で検索（例: "SiO2"、"Fe2O3"）。
- **Space group（空間群）**: 空間群番号またはシンボルでフィルタ。
- **Crystal system（結晶系）**: 結晶系でフィルタ（三斜晶系から立方晶系まで）。
- **Lattice parameters（格子定数）**: 許容誤差付きの概略格子定数（a, b, c, alpha, beta, gamma）で検索。

### 結果テーブル

![結果テーブル](images/FormCrystalDatabase.crystalDatabaseControl.dataGridView.png)

結果テーブルに一致する結晶が表示されます：
- 結晶名
- 化学式
- 空間群
- 格子定数（a, b, c, alpha, beta, gamma）
- 参考文献情報

結晶を選択して **Send to main form** をクリックすると、メインの結晶リストにインポートされます。

---

## データソース

データベースには以下のデータが含まれます：
- American Mineralogist Crystal Structure Database (AMCSD)
- Crystallography Open Database (COD) - オプションのダウンロード
- ユーザーがインポートしたCIFファイル
""")

# ============================================================
# 3. Rotation Geometry (EN)
# ============================================================
write_page("3.-Rotation-Geometry.md", """# Rotation Geometry

**Rotation Geometry** displays the crystal rotation matrix and provides tools for converting between different orientation representations (Euler angles, rotation matrix, axis-angle).

![Rotation Geometry](images/FormRotationMatrix.png)

---

## ReciPro coordinate system

ReciPro uses a right-handed Cartesian coordinate system:
- **X-axis**: Points to the right of the screen.
- **Y-axis**: Points upward on the screen.
- **Z-axis**: Points out of the screen (toward the viewer).

The initial crystal orientation places the *c*-axis along Z (perpendicular to screen) and the *b*-axis along Y (upward).

See [[Appendix A1. Coordinate System]] for the full definition.

---

## Rotation matrix

The 3x3 rotation matrix R transforms the initial crystal orientation to the current orientation. The matrix columns represent the directions of the crystal axes (a*, b*, c*) in the laboratory frame.

---

## Euler angles (Z-X-Z convention)

Three sequential rotations:
1. **Psi**: Rotation about Z-axis (0 to 360 degrees).
2. **Theta**: Rotation about the new X-axis (0 to 180 degrees).
3. **Phi**: Rotation about the new Z-axis (0 to 360 degrees).

---

## Link feature

The **Link** option synchronizes the crystal rotation between the main window and other simulation windows. When linked, rotating the crystal in one window automatically updates all others.

---

## Experimental coordinate system

For comparison with experimental data, ReciPro can convert between:
- Crystal coordinates (fractional or Cartesian)
- Laboratory coordinates
- Detector coordinates
- Goniometer angles (if applicable)
""")

# ============================================================
# 3. Rotation Geometry (JA)
# ============================================================
write_page("3.-Rotation-Geometry-ja.md", """# 回転ジオメトリ

**回転ジオメトリ**は、結晶の回転行列を表示し、異なる方位表現（オイラー角、回転行列、軸角度）間の変換ツールを提供します。

![回転ジオメトリ](images/FormRotationMatrix.png)

---

## ReciProの座標系

ReciProは右手系のデカルト座標系を使用します：
- **X軸**: 画面の右方向。
- **Y軸**: 画面の上方向。
- **Z軸**: 画面の手前方向（観察者に向かう）。

初期結晶方位では、*c*軸がZ方向（画面に垂直）、*b*軸がY方向（上向き）に配置されます。

完全な定義は [[Appendix A1. Coordinate System-ja|座標系の定義]] を参照。

---

## 回転行列

3x3回転行列Rは初期結晶方位を現在の方位に変換します。行列の列は実験室座標系における結晶軸（a*, b*, c*）の方向を表します。

---

## オイラー角（Z-X-Z規約）

3つの連続回転：
1. **Psi**: Z軸周りの回転（0〜360度）。
2. **Theta**: 新X軸周りの回転（0〜180度）。
3. **Phi**: 新Z軸周りの回転（0〜360度）。

---

## リンク機能

**Link** オプションは、メインウィンドウと他のシミュレーションウィンドウ間で結晶回転を同期します。リンク時は、一つのウィンドウで結晶を回転すると自動的にすべてのウィンドウが更新されます。

---

## 実験座標系

実験データとの比較のため、ReciProは以下の座標系間で変換できます：
- 結晶座標（分率またはデカルト）
- 実験室座標
- 検出器座標
- ゴニオメータ角度（該当する場合）
""")

# ============================================================
# Update _Sidebar.md to include new pages
# ============================================================
write_page("_Sidebar.md", """**English**
* [[Home]]
* [[0. Main window]]
  * [[0.1. Crystal orientation control]]
* [[1. Crystal database]]
* [[3. Rotation Geometry]]
* [[5. Structure viewer]]
* [[6. Stereonet]]
* [[7. Diffraction simulator]]
  * [[7.1. SAED simulation]]
  * [[7.2. X ray diffraction]]
  * [[7.3. PED Simulation]]
  * [[7.4 CBED simulation]]
* [[8. HRTEM/STEM simulator]]
  * [[8.1. HRTEM simulation]]
  * [[8.2. STEM simulation]]
  * [[8.3. Potential simulation]]
* [[9. TEM ID]]
* [[10. Spot ID]]
  * [[10.1 Spot ID v2]]
* [[20. Macro]]
  * [[20.1. Built-in functions]]
  * [[20.2. Examples]]
* Appendix
  * [[Appendix A1. Coordinate System]]
  * [[Appendix A2. Detector Coordinate System]]
* [[Troubleshooting]]

---

**日本語**
* [[Home-ja|ホーム]]
* [[0. Main window-ja|メインウィンドウ]]
* [[1. Crystal database-ja|結晶データベース]]
* [[3. Rotation Geometry-ja|回転ジオメトリ]]
* [[5. Structure viewer-ja|Structure Viewer]]
* [[6. Stereonet-ja|ステレオネット]]
* [[7. Diffraction simulator-ja|回折シミュレータ]]
  * [[7.4 CBED simulation-ja|CBEDシミュレーション]]
* [[8. HRTEM/STEM simulator-ja|HRTEM/STEMシミュレータ]]
* [[9. TEM ID-ja|TEM ID]]
* [[10. Spot ID-ja|Spot ID]]
  * [[10.1 Spot ID v2-ja|Spot ID v2]]
* 付録
  * [[Appendix A1. Coordinate System-ja|座標系の定義]]
  * [[Appendix A2. Detector Coordinate System-ja|検出器座標系]]
""")

print("Done: All wiki pages generated!")
