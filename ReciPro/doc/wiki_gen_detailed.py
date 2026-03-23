# -*- coding: utf-8 -*-
import os

WIKI_DIR = r"C:\Users\seto\AppData\Local\Temp\ReciPro.wiki"

def write_page(filename, content):
    path = os.path.join(WIKI_DIR, filename)
    with open(path, "w", encoding="utf-8") as f:
        f.write(content)
    print(f"Wrote: {filename}")

# ============================================================
# 7. Diffraction Simulator (EN)
# ============================================================
write_page("7.-Diffraction-simulator.md", """# Crystal Diffraction (Diffraction Simulator)

**Crystal Diffraction** simulates single-crystal diffraction patterns for X-rays, electrons, and neutrons. It can compute spot positions from the Ewald sphere construction, kinematical intensities from structure factors, and dynamical intensities using the Bloch wave (Bethe) method.

![FormDiffractionSimulator](images/FormDiffractionSimulator.png)

---

## Main area

The central area displays the simulated diffraction pattern. Diffraction spots, Kikuchi lines, Debye rings, and scale marks are drawn here according to the current crystal orientation and wave settings.

| Operation | Action |
|-----------|--------|
| Left drag | Rotate the crystal |
| Centre (wheel) drag | Translate the pattern |
| Right drag | Zoom in |
| Right click | Zoom out |
| Left double-click | Show detailed information for the nearest diffraction spot |

---

## Spot property panel

The left-side panel (**Spot property**) contains all settings related to the wave source, beam conditions, intensity calculation, and spot appearance.

![Spot property panel](images/FormDiffractionSimulator.groupBoxSpotProperty.png)

### Wave source

![Wave source](images/FormDiffractionSimulator.waveLengthControl.png)

Choose the radiation type and set the beam energy or wavelength:

- **X-ray**: Select characteristic X-ray (element + line, e.g. Cu Ka) or set a custom energy for synchrotron radiation. The Ewald sphere radius is 1/lambda.
- **Electron**: Set the accelerating voltage (e.g. 200 kV). The relativistically-corrected wavelength is computed automatically. Electron diffraction uses a much larger Ewald sphere radius than X-rays, making the pattern nearly a planar section of reciprocal space.
- **Neutron**: Set the neutron energy; the corresponding de Broglie wavelength is computed.

The energy and wavelength fields are linked: changing one automatically updates the other.

### Incident beam mode

![Beam mode](images/FormDiffractionSimulator.beamMode.png)

- **Parallel**: Standard parallel-beam illumination. Diffraction spots appear where reciprocal lattice points intersect the Ewald sphere (within the excitation error tolerance).
- **Precession (electron)**: Precession electron diffraction (PED). The beam is tilted by the semi-angle and precessed around the optical axis, integrating over many orientations. This reduces dynamical effects and makes intensities more kinematical. Set the semi-angle (mrad) and the number of integration steps.
- **Precession (X-ray)**: X-ray precession photography simulation (Buerger method). The crystal is nutated so that one reciprocal lattice layer is brought into diffraction condition without distortion.
- **Back Laue**: Back-reflection Laue geometry for X-rays.
- **Convergence (CBED)**: Convergent-beam electron diffraction. This opens the CBED simulation window (see [[7.4 CBED simulation]]).

### Intensity calculation

![Intensity calculation](images/FormDiffractionSimulator.intensityCalc.png)

Three calculation methods are available:

- **Only excitation error**: Spot visibility is determined solely by how close each reciprocal lattice point is to the Ewald sphere surface. The spot radius parameter defines the maximum excitation error s_g (in nm^-1) for a spot to appear. Spots with smaller |s_g| appear brighter. No structure factor calculation is performed. This is the fastest mode and useful for quick indexing.
- **Kinematical & excitation error**: In addition to the excitation error criterion, the kinematical intensity is computed as I_g = |F_g|^2 * sin^2(pi * s_g * t) / (pi * s_g)^2, where F_g is the structure factor and t is the sample thickness. This accounts for systematic absences (extinction rules) and relative intensity differences. Suitable for thin specimens where multiple scattering is weak.
- **Dynamical theory (Bloch wave)**: Full dynamical diffraction calculation using the Bethe (Bloch wave) method. Available for electrons only. The crystal potential is expanded in Fourier components U_g, and the Schrodinger equation is solved as an eigenvalue problem. The number of beams (Bloch waves) and sample thickness must be specified. This gives accurate intensities including multiple scattering, anomalous absorption, and channelling effects.

> **Extinction rules**: Check the extinction options to show/hide spots forbidden by:
> - **Lattice centering**: Extinctions due to the Bravais lattice type (e.g. F-centered: h,k,l must be all odd or all even).
> - **Screw axes / glide planes**: Systematic absences from symmetry elements with translational components.

### Bloch wave parameters

![Bethe parameters](images/FormDiffractionSimulator.bethe.png)

When **Dynamical theory** is selected:

- **Number of beams**: The number of diffracted waves (Bloch waves) included in the calculation. A larger number gives more accurate results but increases computation time (the eigenvalue problem is N x N). Typical values: 50-200 for SAED, 200-500 for CBED.
- **Thickness** (nm): The sample thickness. Dynamical intensities vary periodically with thickness (thickness fringes / pendellosung oscillation).

### Precession parameters

![PED parameters](images/FormDiffractionSimulator.ped.png)

When **Precession** mode is selected:

- **Semi-angle** (mrad): The tilt angle of the precessing beam from the optical axis. Typical values: 10-50 mrad. Larger angles give more kinematical-like intensities.
- **Step**: The number of azimuthal integration steps. More steps improve accuracy at the cost of computation time.

### Spot appearance

![Appearance](images/FormDiffractionSimulator.appearance.png)

- **Solid sphere**: Each spot is drawn as a filled circle. The spot size represents the excitation error tolerance.
- **Point spread (Gaussian)**: Each spot is drawn with a 2D Gaussian intensity profile, giving a more realistic appearance. When the pattern includes many spots, this mode produces a smoother image.
- **Opacity**: Controls the transparency of the diffraction spots (0 = transparent, 255 = fully opaque).
- **Spot radius / Excitation error** (nm^-1): The maximum excitation error for a spot to appear (in "Only excitation error" mode), or the visual spot radius.
- **Same size**: When checked, all spots are drawn with the same size regardless of their intensity.
- **Brightness**: Adjusts the overall brightness of the Gaussian point-spread spots.
- **Color scale**: Choose the color map for intensity display (Gray, Spectrum, etc.).
- **Log scale**: Display intensities on a logarithmic scale. Useful when strong and weak spots coexist.
- **Spot color**: Set colors for spots in different extinction categories:
  - No condition (general reflections)
  - Forbidden by screw/glide (should be absent)
  - Forbidden by lattice centering

### Spot details button

Click **Details of spots** to open a separate window showing a table of all visible diffraction spots with their indices (h,k,l), d-spacing, excitation error, structure factor magnitude |F_g|, and intensity.

---

## Tab menu

The tab panel below the main image controls overlay elements.

![Tab control](images/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.png)

### General tab

![General tab](images/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageGeneral.png)

- **String size**: Adjusts the font size of index labels and other text on the pattern.
- **Background color**: Set the background color of the diffraction pattern area.
- **String color**: Set the color of index labels.
- **Origin color**: Set the color of the transmitted beam position marker.
- **Show foot position**: Display the foot of the perpendicular from the specimen to the detector (relevant for tilted detectors).
- **Show direct position**: Display the transmitted beam (000) position.

### Kikuchi tab

![Kikuchi tab](images/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageKikuchi.png)

Kikuchi lines arise from inelastically scattered electrons that are subsequently Bragg-diffracted. They appear as pairs of bright (excess) and dark (deficient) lines.

- **Threshold**: Filter which Kikuchi lines to display:
  - By **structure factor**: Only show lines whose |F_g| exceeds the threshold (stronger reflections produce more visible Kikuchi lines).
  - By **length** (nm^-1): Only show lines for reflections within the specified reciprocal space range.
- **Kinematical**: When checked, Kikuchi lines are drawn using kinematical intensities (line brightness proportional to |F_g|^2).
- **Line width**: Adjusts the thickness of the drawn Kikuchi lines.
- **Excess line color**: Set the color of the excess (bright) Kikuchi lines. The deficient lines are drawn in the complementary color.

### Debye ring tab

![Debye ring tab](images/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageDebye.png)

Debye-Scherrer rings are concentric rings that appear in polycrystalline diffraction. They are useful as a reference overlay.

- **Ring width**: Adjusts the line thickness of the Debye rings.
- **Ring color**: Set the color of the Debye ring overlay.
- **Label**: When checked, show the Miller indices (hkl) next to each ring.
- **Ignore intensity**: When checked, all rings are drawn with equal intensity; otherwise, ring brightness reflects the multiplicity and |F_g|^2.

### Scale tab

![Scale tab](images/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageScale.png)

- **Line width**: Adjusts the thickness of the scale lines.
- **Label**: When checked, display numerical values on the scale marks (d-spacing or reciprocal distance).
- **Division**: Choose the density of scale divisions: Fine, Medium, or Coarse.
- **2-theta color**: Color of the 2-theta concentric circle scale.
- **Azimuth color**: Color of the azimuthal angle scale lines.

---

## 3D Reciprocal space view

![3D reciprocal space](images/FormDiffractionSimulator.toolStripContainer1.panelMain.splitContainer1.groupBox7.png)

Click the **Reciprocal space** checkbox to split the window and show a 3D OpenGL view of the reciprocal lattice and Ewald sphere. This helps visualize why certain spots appear in the diffraction pattern.

- **Spot radius**: Set the radius of the reciprocal lattice points in the 3D view.
- **Transparency**: Control the transparency of the Ewald sphere.
- **Show indices**: Display Miller indices on the reciprocal lattice points.
- **Spot color (near/far)**: Set the colors for reciprocal lattice points close to and far from the Ewald sphere surface.
- **Background color**: Set the 3D view background color.
- **Reciprocal threshold**: Maximum |g| for reciprocal lattice points to display.
- **Ewald sphere**: Toggle visibility of the Ewald sphere.
- **Direction guide**: Show/hide the axis direction indicator.
- The 3D view can be rotated by left-dragging, zoomed by right-dragging, and translated by middle-dragging.

---

## Mouse position panel

![Mouse position](images/FormDiffractionSimulator.toolStripContainer1.panelMousePosition.png)

Displays real-time information about the cursor position:

- **d** (nm): The d-spacing corresponding to the cursor position.
- **1/d** (nm^-1): The reciprocal lattice distance.
- **2-theta** (deg, rad): The scattering angle.
- **Details**: Expand to show the position in detector coordinates (mm), real-space coordinates (mm), and reciprocal-space coordinates (nm^-1).

---

## Toolbar (bottom)

### Display toggle toolbar

![Display toggles](images/FormDiffractionSimulator.toolStripContainer1.toolStrip3.png)

Toggle visibility of pattern elements:
- **Diffraction spots**: Show/hide the diffraction spots. Right-click to access spot-related options.
- **Kikuchi lines**: Show/hide Kikuchi lines.
- **Debye rings**: Show/hide Debye ring overlay.
- **Scale**: Show/hide the scale marks.

### Label toolbar

![Label options](images/FormDiffractionSimulator.toolStripContainer1.toolStrip1.png)

Toggle which labels appear on each diffraction spot:
- **Index**: Miller indices (h k l).
- **d**: d-spacing in nm.
- **1/d**: Reciprocal distance in nm^-1.
- **Distance**: Distance from the transmitted beam on the detector (mm).
- **Excit. Err**: Excitation error s_g (nm^-1).
- **|Fg|**: Structure factor magnitude.

---

## Detector geometry

Click **Detailed geometry** or go to **Option > Detector geometry** to open the detector geometry window. See [[Appendix A2. Detector Coordinate System]] for a detailed explanation.

Settings include camera length, detector tilt (tau, phi), detector pixel size, and detector area dimensions. An overlapped experimental image can be loaded for comparison.

---

## Diffraction spot information

Double-click on any diffraction spot to open the **Diffraction Spot Information** window, which shows:

- Miller indices (h, k, l)
- d-spacing and reciprocal lattice vector magnitude
- Excitation error s_g
- Structure factor F_g (real and imaginary parts)
- Bethe dynamical theory details (eigenvalues and eigenvectors, if dynamical mode is active)
- Schematic diagram of the Ewald sphere construction for the selected reflection
""")

# ============================================================
# 7. Diffraction Simulator (JA)
# ============================================================
write_page("7.-Diffraction-simulator-ja.md", """# 結晶回折 (回折シミュレータ)

**結晶回折**は、X線・電子線・中性子線による単結晶回折パターンをシミュレーションします。エワルド球構成による回折スポット位置の計算、構造因子による運動学的強度計算、ブロッホ波（Bethe）法による動力学的強度計算が可能です。

![FormDiffractionSimulator](images/FormDiffractionSimulator.png)

---

## メインエリア

中央のエリアにシミュレーションされた回折パターンが表示されます。現在の結晶方位と波源設定に応じて、回折スポット、菊池線、デバイリング、スケールマークが描画されます。

| 操作 | 動作 |
|------|------|
| 左ドラッグ | 結晶の回転 |
| 中央（ホイール）ドラッグ | パターンの平行移動 |
| 右ドラッグ | ズームイン |
| 右クリック | ズームアウト |
| 左ダブルクリック | 最寄りの回折スポットの詳細情報を表示 |

---

## スポットプロパティパネル

左側のパネル（**Spot property**）には、波源、ビーム条件、強度計算、スポット外観に関するすべての設定があります。

![スポットプロパティパネル](images/FormDiffractionSimulator.groupBoxSpotProperty.png)

### 波源

![波源設定](images/FormDiffractionSimulator.waveLengthControl.png)

放射線の種類を選択し、ビームエネルギーまたは波長を設定します：

- **X線**: 特性X線（元素 + 線種、例: Cu Ka）を選択するか、シンクロトロン放射光用にカスタムエネルギーを設定します。エワルド球の半径は 1/lambda です。
- **電子線**: 加速電圧（例: 200 kV）を設定します。相対論的補正を含む波長が自動計算されます。電子回折はX線よりもはるかに大きなエワルド球半径を持つため、パターンは逆格子空間のほぼ平面切断面になります。
- **中性子線**: 中性子エネルギーを設定し、対応するド・ブロイ波長が計算されます。

エネルギーと波長フィールドは連動しており、一方を変更すると自動的に他方が更新されます。

### 入射ビームモード

![ビームモード](images/FormDiffractionSimulator.beamMode.png)

- **Parallel（平行ビーム）**: 標準的な平行ビーム照射。逆格子点がエワルド球と交差する（励起誤差の許容範囲内の）位置に回折スポットが現れます。
- **Precession (electron)（歳差電子回折）**: 歳差電子回折（PED）。ビームは半角だけ傾斜され、光軸周りに歳差運動し、多くの方位にわたって積分されます。これにより動力学的効果が軽減され、強度がより運動学的になります。半角（mrad）と積分ステップ数を設定します。
- **Precession (X-ray)（X線歳差撮影法）**: X線歳差写真法（ビュルガー法）のシミュレーション。結晶を歳差運動させて、一つの逆格子面を歪みなく回折条件に持ち込みます。
- **Back Laue（背面ラウエ）**: X線の背面反射ラウエジオメトリ。
- **Convergence (CBED)（収束電子回折）**: 収束電子回折。CBED シミュレーションウィンドウが開きます（[[7.4 CBED simulation]] を参照）。

### 強度計算

![強度計算](images/FormDiffractionSimulator.intensityCalc.png)

3つの計算方法が使用可能です：

- **Only excitation error（励起誤差のみ）**: 各逆格子点がエワルド球面にどれだけ近いかだけでスポットの可視性が決まります。スポット半径パラメータが表示される最大励起誤差 s_g（nm^-1）を定義します。|s_g| が小さいスポットほど明るく表示されます。構造因子計算は行われません。最も高速なモードで、素早い指数付けに有用です。
- **Kinematical & excitation error（運動学的 + 励起誤差）**: 励起誤差条件に加えて、運動学的強度 I_g = |F_g|^2 * sin^2(pi*s_g*t) / (pi*s_g)^2 が計算されます。ここで F_g は構造因子、t は試料厚さです。系統的消滅（消滅則）と相対的な強度差を考慮します。多重散乱が弱い薄い試料に適しています。
- **Dynamical theory (Bloch wave)（動力学理論）**: Bethe（ブロッホ波）法を使用した完全な動力学回折計算。電子線のみ使用可能です。結晶ポテンシャルがフーリエ成分 U_g で展開され、シュレーディンガー方程式が固有値問題として解かれます。ビーム数（ブロッホ波数）と試料厚さを指定する必要があります。多重散乱、異常吸収、チャネリング効果を含む正確な強度が得られます。

> **消滅則**: 消滅オプションをチェックして、以下の条件で消滅するスポットの表示/非表示を切り替えます：
> - **格子消滅**: ブラベー格子タイプによる消滅（例: F心格子: h,k,l がすべて奇数またはすべて偶数）。
> - **らせん軸/映進面**: 並進成分を持つ対称要素による系統的消滅。

### ブロッホ波パラメータ

![Betheパラメータ](images/FormDiffractionSimulator.bethe.png)

**動力学理論**選択時：

- **Number of beams（ビーム数）**: 計算に含まれる回折波（ブロッホ波）の数。数が大きいほど正確な結果が得られますが、計算時間が増加します（固有値問題は N x N）。典型的な値: SAED で 50-200、CBED で 200-500。
- **Thickness（厚さ）** (nm): 試料の厚さ。動力学的強度は厚さに対して周期的に変化します（等厚干渉縞 / ペンデルレーズング振動）。

### 歳差パラメータ

![PEDパラメータ](images/FormDiffractionSimulator.ped.png)

**歳差**モード選択時：

- **Semi-angle（半角）** (mrad): 歳差ビームの光軸からの傾斜角。典型的な値: 10-50 mrad。角度が大きいほど運動学的に近い強度が得られます。
- **Step（ステップ数）**: 方位角方向の積分ステップ数。ステップ数が多いほど精度が向上しますが計算時間が増加します。

### スポット外観

![外観設定](images/FormDiffractionSimulator.appearance.png)

- **Solid sphere（塗りつぶし円）**: 各スポットが塗りつぶされた円として描画されます。スポットサイズは励起誤差の許容範囲を表します。
- **Point spread (Gaussian)（ガウシアン）**: 各スポットが2Dガウシアン強度プロファイルで描画され、より現実的な外観になります。
- **Opacity（不透明度）**: 回折スポットの透明度を制御します（0 = 透明、255 = 完全に不透明）。
- **Spot radius / Excitation error（スポット半径 / 励起誤差）** (nm^-1): スポットが表示される最大励起誤差、またはスポットの表示半径。
- **Same size（同一サイズ）**: チェックすると、強度に関係なくすべてのスポットが同じサイズで描画されます。
- **Brightness（明るさ）**: ガウシアンスポットの全体的な明るさを調整します。
- **Color scale（カラースケール）**: 強度表示のカラーマップを選択（Gray、Spectrum など）。
- **Log scale（対数スケール）**: 強度を対数スケールで表示。強いスポットと弱いスポットが共存する場合に有用。
- **Spot color（スポット色）**: 消滅カテゴリごとにスポットの色を設定：
  - No condition（一般的な反射）
  - Screw/Glide で消滅（消滅すべき反射）
  - 格子消滅で消滅

### スポット詳細ボタン

**Details of spots** をクリックすると、すべての可視回折スポットの指数 (h,k,l)、面間隔、励起誤差、構造因子振幅 |F_g|、強度をまとめた表が別ウィンドウで表示されます。

---

## タブメニュー

メイン画像下部のタブパネルでオーバーレイ要素を制御します。

![タブコントロール](images/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.png)

### General（一般）タブ

![Generalタブ](images/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageGeneral.png)

- **String size（文字サイズ）**: 指数ラベルなどのフォントサイズを調整。
- **Background color（背景色）**: 回折パターン領域の背景色を設定。
- **String color（文字色）**: 指数ラベルの色を設定。
- **Origin color（原点色）**: 透過ビーム位置マーカーの色を設定。
- **Show foot position（垂線の足を表示）**: 試料から検出器への垂線の足の位置を表示（傾斜検出器に関連）。
- **Show direct position（直接ビーム位置を表示）**: 透過ビーム (000) 位置を表示。

### Kikuchi（菊池線）タブ

![菊池線タブ](images/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageKikuchi.png)

菊池線は、非弾性散乱された電子がその後ブラッグ回折を受けることで生じます。明線（excess）と暗線（deficient）のペアとして現れます。

- **Threshold（しきい値）**: 表示する菊池線のフィルタ条件：
  - **Structure factor（構造因子）**: |F_g| がしきい値を超える線のみ表示。
  - **Length（長さ）** (nm^-1): 指定した逆格子空間範囲内の反射のみ表示。
- **Kinematical（運動学的）**: チェックすると、運動学的強度で描画（線の明るさが |F_g|^2 に比例）。
- **Line width（線幅）**: 菊池線の太さを調整。
- **Excess line color（明線の色）**: excess（明）菊池線の色を設定。

### Debye ring（デバイリング）タブ

![デバイリングタブ](images/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageDebye.png)

デバイ・シェラーリングは多結晶回折で現れる同心円状のリングで、参照オーバーレイとして使用できます。

- **Ring width（リング幅）**: デバイリングの線の太さを調整。
- **Ring color（リング色）**: デバイリングオーバーレイの色を設定。
- **Label（ラベル）**: チェックすると各リングの横にミラー指数 (hkl) を表示。
- **Ignore intensity（強度を無視）**: チェックするとすべてのリングが同じ強度で描画。

### Scale（スケール）タブ

![スケールタブ](images/FormDiffractionSimulator.toolStripContainer1.panelMain.tabControl.tabPageScale.png)

- **Line width（線幅）**: スケール線の太さを調整。
- **Label（ラベル）**: チェックするとスケール目盛りに数値を表示。
- **Division（目盛り密度）**: Fine（細かい）、Medium（中間）、Coarse（粗い）から選択。
- **2-theta color（2 theta 色）**: 2 theta 同心円スケールの色。
- **Azimuth color（方位角色）**: 方位角スケール線の色。

---

## 3D逆格子空間ビュー

![3D逆格子空間](images/FormDiffractionSimulator.toolStripContainer1.panelMain.splitContainer1.groupBox7.png)

**Reciprocal space** チェックボックスをクリックすると、ウィンドウが分割され、逆格子とエワルド球の3D OpenGLビューが表示されます。特定のスポットが回折パターンに現れる理由を視覚化するのに役立ちます。

- **Spot radius（スポット半径）**: 3Dビューでの逆格子点の半径。
- **Transparency（透明度）**: エワルド球の透明度。
- **Show indices（指数表示）**: 逆格子点にミラー指数を表示。
- **Spot color near/far（スポット色 近/遠）**: エワルド球面に近い/遠い逆格子点の色。
- **Background color（背景色）**: 3Dビューの背景色。
- **Reciprocal threshold（逆格子しきい値）**: 表示する逆格子点の最大 |g|。
- **Ewald sphere（エワルド球）**: エワルド球の表示切替。
- **Direction guide（方向ガイド）**: 軸方向インジケータの表示切替。

---

## マウス位置パネル

![マウス位置](images/FormDiffractionSimulator.toolStripContainer1.panelMousePosition.png)

カーソル位置のリアルタイム情報を表示：

- **d** (nm): カーソル位置に対応する面間隔。
- **1/d** (nm^-1): 逆格子距離。
- **2-theta** (deg, rad): 散乱角。
- **Details**: 展開すると、検出器座標 (mm)、実空間座標 (mm)、逆空間座標 (nm^-1) での位置を表示。

---

## ツールバー（下部）

### 表示切替ツールバー

![表示切替](images/FormDiffractionSimulator.toolStripContainer1.toolStrip3.png)

パターン要素の表示を切り替え：
- **Diffraction spots**: 回折スポットの表示/非表示。右クリックでスポット関連オプション。
- **Kikuchi lines**: 菊池線の表示/非表示。
- **Debye rings**: デバイリングオーバーレイの表示/非表示。
- **Scale**: スケールマークの表示/非表示。

### ラベルツールバー

![ラベルオプション](images/FormDiffractionSimulator.toolStripContainer1.toolStrip1.png)

各回折スポットに表示するラベルを切り替え：
- **Index**: ミラー指数 (h k l)。
- **d**: 面間隔 (nm)。
- **1/d**: 逆格子距離 (nm^-1)。
- **Distance**: 検出器上での透過ビームからの距離 (mm)。
- **Excit. Err**: 励起誤差 s_g (nm^-1)。
- **|Fg|**: 構造因子振幅。

---

## 検出器ジオメトリ

**Detailed geometry** ボタンまたは **Option > Detector geometry** から検出器ジオメトリウィンドウを開きます。詳細は [[Appendix A2. Detector Coordinate System-ja|検出器座標系]] を参照。

カメラ長、検出器傾斜（tau, phi）、検出器ピクセルサイズ、検出器面積の寸法を設定できます。比較用に実験画像をオーバーレイとして読み込むことも可能です。

---

## 回折スポット情報

任意の回折スポットをダブルクリックすると**回折スポット情報**ウィンドウが開き、以下の情報が表示されます：

- ミラー指数 (h, k, l)
- 面間隔と逆格子ベクトルの大きさ
- 励起誤差 s_g
- 構造因子 F_g（実部と虚部）
- Bethe 動力学理論の詳細（固有値と固有ベクトル、動力学モードが有効な場合）
- 選択した反射のエワルド球構成の模式図
""")

print("Done: Diffraction Simulator pages (EN + JA)")
