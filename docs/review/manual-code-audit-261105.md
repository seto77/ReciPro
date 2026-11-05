# マニュアルとコードの突き合わせ監査（2026-11-05）

> 本稿はコードから Claude が起こした下書き（2026-11-05）。著者未査読。

- 対象: `docs/src/{en,ja}/` の全章（Appendix A2/A3 の理論記述は対象外。A1 座標系と A4 の操作記述は対象）。基準コードは master `7ed3493`（ver4.951）とサブモジュール Crystallography `9ffcf47`、Crystallography.Controls `aadeb74`。
- このファイルは `docs_dir`（`docs/src`）の外にあるので、公開サイトには載りません。
- 本文の修正は en と ja だけに入れました。**de/fr/es/it/ru/zh-Hans/zh-Hant/ko/pt の 9 言語は未追随**です（`[pages-sync]` 運用で揃える必要があります）。
- 修正・追記した箇所には `<!-- 261105Cl draft: コードから起こした修正・著者未査読 (根拠: ...) -->` を付けてあります（`grep -rn "261105Cl draft" docs/src` で一覧できます）。
- `mkdocs build -f docs/mkdocs.yml --strict` は通過を確認済みです。

## 旧資料の棚卸し

| 資料 | 状態 |
|---|---|
| `docs/src/**`（MkDocs、11 言語・47 ページ/言語） | 現行マニュアル。ver4.951 の「Details of the chemical composition」まで反映済み。今回の監査対象 |
| `ReciPro/doc/ReciProManual(en/ja).pdf/.docx` | 2023 年版。260602Cl に出力への同梱を廃止済み（`ReciPro.csproj` のコメント）。現行版とは比較していない |
| `ReciPro/doc/bethe.pdf`, `hrtem.pdf` | 同梱は廃止済み。Appendix A3 に移行済み |
| GitHub Wiki | GitHub Pages へのリダイレクトだけ（更新停止） |
| `ReciPro/Version.cs` History | 新機能の手掛かりとして各グループで参照した |

## フォーム一覧と章の対応

| フォーム（ファイル） | 起動元 | マニュアル章 |
|---|---|---|
| FormMain | 起動時 | 0. Main window |
| FormCrystalDatabase | Functions ▸ Crystal Database | 1. Crystal database |
| FormPeriodicTable (Controls/CrystalDatabase) | データベース検索の元素選択 | 1 |
| FormCrystalSelection (Controls) | File ▸ 結晶リストの読込/保存（選択読込） | 0 |
| CrystalControl 配下: FormChemicalComposition / FormStrain / FormSuperStructure / FormAnotherSpaceGroup / FormIsotopeComposition / FormAtomDetailedInfo (Controls/Crystal) | 結晶情報パネルの右クリック、原子タブ | 0 |
| FormSymmetryInformation (Controls/Crystal) | Functions ▸ Symmetry Information | 2, A4.1 |
| FormGroupRelations (Controls/Crystal) | Symmetry Information ▸ Group Relations | A4.2 |
| FormBeamInteraction (Controls/Crystal) | Functions ▸ Beam Interaction | 3 |
| FormRotation（クラス名 FormRotationMatrix） | Functions ▸ Rotation Geometry | 4 |
| FormStructureViewer / FormAtom / FormExport3DModel / FormResolution (StructureViewer/) | Functions ▸ Structure Viewer | 5 |
| FormMovie | 構造表示・ステレオネット等の動画作成 | 5（一部 6, 12） |
| FormStereonet | Functions ▸ Stereonet | 6 |
| FormDiffractionSimulator / Geometry / Holder / DynamicCompression (DiffractionSimulator/) | Functions ▸ Diffraction Simulator | 7（ハブ） |
| FormDiffractionSimulatorCBED | 回折シミュレータの CBED モード | 7.3 |
| FormALCHEMI | 回折シミュレータ ▸ Options | 7.4 |
| FormDiffractionSpotInfo | 回折シミュレータ / HRTEM シミュレータのスポット情報 | 7 |
| FormPolycrystallineDiffractionSimulator (PolycrystallineDiffraction/) | Options ▸ (experimental) Powder diffraction function を有効にすると現れる Powder Diffraction ボタン | 7.5 で言及のみ（実験的機能） |
| FormTrajectory | Functions ▸ Trajectory Simulator | 8 |
| FormImageSimulator / FormCTF / FormPresets (ImageSimulator/) | Functions ▸ HRTEM/STEM Simulator | 9, 9.1–9.3 |
| FormSpotIDv1 / FormSpotIDv1Results (SpotID/) | Functions ▸ Spot ID v1 | 10 |
| FormSpotIDV2 / FormSpotIDv2Details (SpotID/) | Functions ▸ Spot ID v2 | 11 |
| FormEBSD | Functions ▸ EBSD Simulator | 12 |
| FormMacro (Controls/Macro) | Macro ▸ Editor | 20 |
| FormCaptureGUI (Controls) | Options ▸ (experimental) Capture GUI Components | 記載なし（開発用） |
| FormPDF (Controls) | 呼び出しはコメントアウト済み | 対象外 |
| FormCalculator (Controls/Numeric) | 260801Cl に ReciPro から廃止（`FormMain.cs` のコメント） | 対象外 |

## 集計

| グループ | 対象ページ | 食い違い | 欠落機能 | 要確認 |
|---|---|---|---|---|
| A | 0 Main window, 1 Crystal database, 21 Shortcuts, Home | 28（27 修正） | 16（11 追記） | 12 |
| B | 2 Symmetry, 3 Beam interaction, A4 | 15（13 修正） | 9（2 追記） | 6 |
| C | 4 Rotation, 5 Structure viewer, 6 Stereonet, A1 | 26（22 修正） | 23（14 追記） | 8 |
| D | 7 ハブ, 7.1 SAED, 7.2 PED, 7.3 CBED | 42（39 修正） | 13（10 追記） | 9 |
| E | 7.4 ALCHEMI, 7.5 X 線・中性子, 8 Trajectory | 14（13 修正。ALCHEMI は食い違いなし） | 9（7 追記） | 6 |
| F | 9 HRTEM/STEM, 9.1–9.3 | 22（22 修正） | 12（9 追記） | 13 |
| G | 10 Spot ID v1, 11 Spot ID v2, 12 EBSD | 20（15 修正） | 13（9 追記） | 8 |
| H | 20 Macro, 20.1–20.3 | 16（12 修正） | 6（2 追記） | 6 |
| **計** | | **約 183（163 修正）** | **101（64 追記）** | **68** |

## コード側の不具合候補（コードは未変更。著者判断待ち）

監査中に見つかった、マニュアルではなくコードの問題に見えるもの。★は親セッションが該当行を直接確認したもの。

1. ★ `ReciPro/SpotID/FormSpotIDv1.cs` 80 行目: 写真 3 の `PhotoInformation` に L3 として `inputBoxP2L3.Length`（写真 2 の値）を渡している。
2. ★ `ReciPro/ImageSimulator/FormImageSimulator.cs` `HRTEM_ObjAperRadius` の setter: 有限値の分岐でも `checkBoxOpenAperture.Checked = true` にしているため、プリセット適用時や再起動時に絞りが開放になる。マクロの `HRTEM.ApertureSemiangle` も同じ経路を通る。
3. ★ 同 `GaussianBlurFWHM` / `STEM_SourceSizeFWHM`: UI は FWHM だが、`PseudoBitmap.SetBlurImage(double hwhm, …)` に FWHM の値をそのまま渡している。実際のぼかし幅は表示値の 2 倍になっている疑い（STEM 側は呼び出し先を要確認）。
4. ★ `ReciPro/FormStereonet.cs` `Draw3D`: `checkBoxDisplay3D.Checked` のとき先頭で return するため、3D 表示中は 3D 側が更新されない疑い。
5. ★ `ReciPro/FormMain.Designer.cs` `resetRegistryToolStripMenuItem`（Reset registry settings on exit）: Designer 以外に参照が無く、チェックしても何も起きない。
6. `FormRotation.cs`: Copy は `\n` 区切りで書き、Paste は `\r\n` で分割するので、自分のコピーを貼り付けられない。
7. `FormSpotIDv1.cs`: `buttonSearchAll.Text` を英語の固定文字列で上書きしているため、ja の表記が出ない。
8. `FormMain.cs` `buttonSetVector_Click`（Plane 側）: ベクトルの組み立てで `w * cStar` と `Crystal.B_Star` が混在している。
9. FormTrajectory の「脱出時エネルギー分布」: 実装はエネルギー損失 $E_0-E$ を集計している（ラベルとの不一致）。
10. `FormStereonet.ja.resx` の `label2` が "Tilt Y:" のまま（誤訳の疑い）。Structure Viewer の Show Subcell が描画で使われていない。Save/Copy サブメニューの Legend 項目にハンドラが無い。

詳細と残りの要確認事項は、以下のグループ別報告にあります。

---


## 監査報告: A-main (0-main-window / 1-crystal-database / 21-shortcuts / index)

### 食い違い

| ページ | 節 | マニュアルの記述 | コードの実態 | 根拠 (file:method/行) | 分類 | 対応 |
|---|---|---|---|---|---|---|
| 0-main-window / 21-shortcuts (en,ja) | Keyboard & mouse shortcuts / Application-wide | Structure Viewer, Stereonet, Diffraction Simulator, **Spot ID windows** でも有効 | KeyDown を配線しているのは StructureViewer / Stereonet / DiffractionSimulator / **SpotIDv1 だけ** (v2 は対象外) | FormMain.cs:FormMain_Load (L435–467) | 不正確 | 修正済 |
| 0-main-window / 21-shortcuts (en,ja) | ショートカット表 | 方位ウィジェット "under *Current Direction*" | グループボックス名は "Current orientation" / ja「現在の結晶方位」 | FormMain.resx groupBoxCurrentDirection.Text | 古い | 修正済 |
| 0-main-window / 21-shortcuts (en,ja) | ショートカット表 | 右クリックメニュー "Rename / Duplicate / Delete / Export CIF…"、ja「名前変更／複製／削除／CIF出力」 | 順序とラベルは Rename / Export as CIF / Duplicate / Delete、ja「名前の変更／CIFで出力／複製／削除」 | FormMain.Designer.cs contextMenuStripListBox.Items, FormMain(.ja).resx | 不正確 | 修正済 |
| 0-main-window / 21-shortcuts (en,ja) | ショートカット表 / Zone axis | "Double-click the **Current Index** label" | そのラベル (labelCurrentIndex) には "[u v w]" (晶帯軸指数) が表示されるだけで、"Current Index" という文字は無い | FormMain.resx labelCurrentIndex.Text, FormMain.cs:labelCurrentIndex_DoubleClick | 不正確 | 修正済 |
| 0-main-window / 21-shortcuts (en,ja) | ショートカット表 | ドロップで読めるのは `.xml`, `.cdb2` | `.out` も受け付け、置換か追加かを Yes/No/Cancel で確認する | FormMain.cs:FormMain_DragDrop | 不正確 | 修正済 |
| 0-main-window (en,ja) | ショートカット表 | 機能ボタンをシングルクリックすると開閉 | 最小化中のウィンドウは閉じずに Normal に戻す | FormMain.cs:toolStripButtons_MouseDown | 不正確 | 修正済 |
| 0-main-window (en,ja) | File menu | Load / Save を押すとすぐファイル読込・保存 | Load (Replace/Add) と Save は FormCrystalSelection で対象結晶をチェックして選ぶ。Load のフィルタは *.xml;*.out | FormMain.cs:ReadCrystalList(showSelectionDialog), SaveCrystalDataToolStripMenuItem_Click, readCrystalDataToolStripMenuItem_Click | 不正確 | 修正済 |
| 0-main-window (en,ja) | Options | Use ionic scattering factors: 「計算に用いる原子散乱因子にイオンの電荷項を取り入れる」 | BetheMethod.ElasticIonModel を IonFull / Neutral で切り替えるだけ (動力学 Bloch 波の電子線計算と Kikuchi プロファイル)。Σq≠0 のときは単極子を落として遮蔽イオン形にし、メッセージを出す | FormMain.cs:toolStripMenuItemIonicScattering_CheckedChanged; BetheMethod.cs L181/3879; IKikuchiInelasticKernel.cs L62 | 不正確 | 修正済 |
| 0-main-window (en,ja) | Zone axis | "indices whose magnitudes stay within the limit" | 既約 (gcd=1) で \|u\|+\|v\|+\|w\| ≤ limit (L1)。limit の範囲は 1–100、既定 30 | FormMain.cs:numericBoxMaxUVW_ValueChanged; Designer numericBoxMaxUVW | 不正確 | 修正済 |
| 0-main-window (en,ja) | Arrows | "Rotate by angle Step." | Step の既定は 10°。Animation にすると Step は °/s の角速度になる。↺/↻ は画面法線まわり | FormMain.cs:ButtonDirection_Click, CheckBoxAnimation_CheckedChanged, Timer_Tick | 不正確 (省略) | 修正済 |
| 0-main-window (en,ja) | View along | Fix: 「指定軸が空間固定された状態で回転」。Axis/Plane: もう一方が設定されていればそれが上向き | Fix を入れると以後の回転軸がその方向に置き換わる。2 つの Fix は排他で、指数 0 のときは入らない。上向きに使うのは uh+vk+wl=0 (軸が面内にある) ときだけで、それ以外は自動で決まる | FormMain.cs:Rotate(Vector3DBase,double), buttonSetVector_Click, checkBoxFix*_CheckedChanged | 不正確 | 修正済 |
| 0-main-window, index (en,ja) | Crystal List / Quick start | 既定の結晶リストは ~80 結晶 | initial.xml に入っているのは 66 結晶 | ReciPro/initial.xml (`<Crystal>` 66 個) | 古い | 修正済 |
| 0-main-window (en) | Crystal edit buttons | "Delete / All clear" | ボタン名は "Clear all crystals" | FormMain.resx buttonClearAllCrystals.Text | 古い | 修正済 |
| 0-main-window (en,ja) | Atom tab / Origin shift | 「プリセットボタン (+ / −) で標準量シフト」 | + / − は符号を選ぶラジオボタン。プリセットは (⅛⅛⅛)…(0 ¼ −⅛) の 9 個のベクトルボタン | AtomControl.resx radioButtonOriginShiftPlus/Minus, buttonOriginShift1–9 | 誤り | 修正済 |
| 0-main-window (ja) | 基本ワークフロー | 「**回転情報**」「**ファンクション**」 | ja UI の表記は「回転状態表示/制御」「機能」 | FormMain.ja.resx label8 / toolStripLabel1 | ja-en不一致 | 修正済 |
| 0-main-window (ja) | 結晶リスト | 右クリックメニュー「CIF形式で書き出し」 | ja UI は「CIFで出力」 | FormMain.ja.resx exportAsCIFFormatToolStripMenuItem | ja-en不一致 | 修正済 |
| 0-main-window (ja) | 原子情報タブ | Add/Replace/Up/Down/Delete, Label/Element/Occ, Origin shift, Debye-Waller factor, Notation, Model (Isotropy/Anisotropy), Scattering factor, X-ray/Electron/Neutron を英語で表記 | ja UI は 追加/更新/上へ移動/下へ移動/削除, ラベル/元素/占有率, 原点シフト, デバイワラー因子, 形式, モデル(等方/非等方), 散乱因子, X線/電子線/中性子線 | AtomControl.ja.resx | ja-en不一致 | 修正済 |
| 0-main-window (ja) | 引用文献タブ | Authors / Journal / Title を英語で表記 | ja UI は 著者 / 雑誌名 / タイトル (Note は英語のまま) | CrystalControl.ja.resx groupBox* | ja-en不一致 | 修正済 |
| 0-main-window (en,ja) | Macro | 「Macro ウィンドウを開く」 | メニューは **Editor**(編集) と、マクロウィンドウに登録したマクロの一覧 (選ぶと実行) | FormMain.cs:editorToolStripMenuItem_Click, SetMacroToMenu | 不正確 (省略) | 修正済 |
| 0-main-window (en,ja) | Options | Reset registry settings on exit: 「次回起動時に設定をリセット」 | このメニュー項目を参照するコードが Designer 以外に**どこにも無い**。チェックしても何も起きない | grep resetRegistryToolStripMenuItem (FormMain.Designer.cs のみ) | 誤り (コード側の不具合の疑い) | 未修正 (要確認 1) |
| 1-crystal-database (en,ja) | Element | 周期表の色はグレー / 緑 / 赤 | LightYellow(中立) / LightBlue(含む) / LightCoral(除外) | FormPeriodicTable.cs L39–41, button_Click | 誤り | 修正済 |
| 1-crystal-database (en,ja) | Cell parameter | 「空欄はワイルドカード」 | 値が 0 のパラメータを無視する。許容誤差は長さが共通の ±%、角度が共通の ±° | SearchCrystalControl.cs:backgroundWorkerSearch_DoWork; resx ToolTip | 不正確 | 修正済 |
| 1-crystal-database (en,ja) | d-spacing | 最強反射 (または複数) の d と許容誤差 | d1–d3 の最大 3 値を個別にチェックして使い、それぞれ ±%。比較対象は強度上位 8 本の d 値。"Ignore scattering factor" をオンにすると格子から幾何学的な全 d を計算して比較する | SearchCrystalControl.cs:DoWork/calcDlist; CrystalMinimum.cs:d (L45) | 不正確 | 修正済 |
| 1-crystal-database (en,ja) | Density | 「許容誤差」 | 相対 ±% | SearchCrystalControl.cs:DoWork | 不正確 | 修正済 |
| 1-crystal-database (en,ja) | Name/Reference/Crystal system | (大文字小文字や "Unknown" についての記載なし) | 大文字小文字を区別しない部分一致で、条件は AND で組み合わせる。結晶系コンボの先頭 "Unknown" は絞り込みなし。Elements をオンにすると原子データの無いエントリは除外される | SearchCrystalControl.cs:DoWork; resx comboBoxSearchCrystalSystem.Items | 不正確 (省略) | 修正済 |
| 1-crystal-database (en) | Search options 表 | **Element**, **Cell Param** | UI 表記は "Elements", "Cell Params" | SearchCrystalControl.resx | 古い | 修正済 (表のみ。見出しはアンカー維持のため変えていない) |
| 21-shortcuts (en,ja) | Diffraction-pattern views | 「これらのビューにホイールズームは**無い**」、回折シミュレータは「no wheel zoom」 | 回折シミュレータには MouseWheelZoom=true で ×2 / ×0.5 のホイールズームがある (カーソル中心、Fix 中は固定中心)。ステレオネットと EBSD には無い | FormDiffractionSimulator.cs:graphicsBox_MouseWheel; Designer L1216; GraphicsBox.cs:OnMouseWheel | 誤り | 修正済 |
| index (en,ja) | Quick start | "Crystal Diffraction, HRTEM simulation" | ボタン名は Diffraction Simulator / HRTEM/STEM Simulator (ja: 回折シミュレータ / HR/STEMシミュレータ / 結晶構造) | FormMain(.ja).resx toolStripButton*.Text | 古い | 修正済 |

### マニュアルに無い機能

| フォーム | 機能/コントロール | 概要 | 根拠 | 対応 |
|---|---|---|---|---|
| FormMain | FormCrystalSelection (Load/Save 時の選択ダイアログ) | 読み込む結晶・書き出す結晶をチェックで選ぶ | FormMain.cs:ReadCrystalList, SaveCrystalDataToolStripMenuItem_Click | 追記済 |
| FormMain / FormCaptureGUI | Ctrl+Shift+Alt+C | アプリ全体でメッセージフィルタとして常時有効。アクティブウィンドウの GUI キャプチャツールを開く | FormCaptureGUI.cs:InstallShortcutFilter (FormMain.cs L328) | 追記済 (0-main, 21-shortcuts) |
| NumericBox (全フォーム) | ↑/↓ で値を増減、Enter で確定、Shift/Ctrl+Enter で式を評価 | 数値欄に共通のキー操作 (ver4.945 の改善項目) | NumericBox.cs:ProcessCmdKey, tryStepByArrowKey, textBox_KeyDown, Calculate | 追記済 (21-shortcuts) |
| FormDiffractionSimulator | ホイールズーム | ×2 / ×0.5 | FormDiffractionSimulator.cs:graphicsBox_MouseWheel | 追記済 (21-shortcuts) |
| FormEBSD | 晶帯軸ピッキング中のダブルクリック (+Ctrl/Shift) | 点の追加 / 5 px 以内の点を削除 / 選択 (12-ebsd には既に記載あり) | FormEBSD.cs:HandleZoneAxisDoubleClick | 追記済 (21-shortcuts) |
| CrystalControl | Color (描画色) | 3D 表示と回折プロファイル曲線で使う結晶色 | CrystalControl.resx colorControl | 追記済 |
| SymmetryControl | Show Errors | 格子定数の誤差欄を表示 | SymmetryControl.resx checkBoxShowError | 追記済 |
| CrystalControl | Change axes/origin setting ボタン | 右クリックメニューの同名項目と同じ動作 | CrystalControl.cs:buttonChangeAxesOriginSetting_Click | 追記済 |
| CrystalControl | Basic Info の単位 | Cell Mass 10⁻²⁴ g、Molar Volume cm³/mol (式単位あたり)、Molar Mass g/mol、Density g/cm³、Mean A g/mol | CrystalControl.resx FooterText, CrystalControl.cs L252–276 | 追記済 |
| AtomControl | 原子リストの Multi. / Wyck. Let. / Site Sym. 列 | 多重度・Wyckoff 記号・サイト対称 | AtomControl.resx *DataGridViewTextBoxColumn | 追記済 |
| FormMain | Macro メニューのマクロ一覧 | 登録マクロがメニューに並び、選ぶと直接実行 | FormMain.cs:SetMacroToMenu | 追記済 |
| CrystalControl | Atom タブ内パネルの右クリック "Set Debye-Waller Factors to Zero" | 全原子の DW を B=0 (等方) にする | CrystalControl.Designer.cs panel3.ContextMenuStrip=contextMenuStrip2, CrystalControl.cs:resetToolStripMenuItem_Click | 未対応 (要確認 9) |
| AtomControl | 位置・DW の "Show Error" チェック | 誤差欄 (±) を表示 | AtomControl.resx checkBoxDetail*Error | 未対応 (些末) |
| AtomControl / FormIsotopeComposition | 中性子 "Edit" ボタン → 同位体組成ダイアログ | Natural / Custom Abundance (%) の表を編集 | AtomControl.cs:buttonEditIsotopeAbundance_Click | 未対応 (Custom isotope abundance の記載はある) |
| FormMain | アニメーション中のステータスバー | フレームレート (frm/s) を表示 | FormMain.cs:Timer_Tick | 未対応 (些末) |
| FormCrystalDatabase | AMCSD/COD チェックを切り替えると再検索 | DataBaseChanged → buttonSearch_Click | SearchCrystalControl.cs:CrystalDatabaseControl_DataBaseChanged | 未対応 (些末) |

### 要確認事項

1. **Options ▸ Reset registry settings on exit** (resetRegistryToolStripMenuItem) を参照するコードが Designer 以外に見つからない (CheckOnClick だけでハンドラが無く、FormClosing/Registry() でも見ていない)。今は何も起きない機能になっているようです。コードを直すか、マニュアル (en「Reset settings on next restart」/ja「次回起動時に…リセット」) を直すか、判断をお願いします。ja のラベルも「レジストリを初期化 (要再起動)」で、en の "on exit" とずれています。
2. FormMain.cs:buttonSetVector_Click (Plane 側) に計算の不整合があるように見えます: `zVector = h * aStar + k * Crystal.B_Star + l * cStar` (b だけローカルの bStar ではない)、`yVector = u * aAxis + v * bAxis + w * cStar` (w の係数が cAxis ではなく cStar)。後者は u,v,w ≠ 0 かつ非直交格子だと上向きの方向が狂う可能性があります。コード側の確認をお願いします (マニュアルは変えていません)。
3. ja の機能ボタンのラベル「結晶構造\n(V)」「ステレオ\nネット(S)」は、廃止された Ctrl+Shift+V/S ショートカット (260801Cl 廃止) の名残に見えます (FormMain.ja.resx)。
4. release-notes v4.945 に "wheel and +/- zoom of the diffraction pattern" とありますが、GraphicsBox.cs のコメント (260801Cl) では +/- キーズームは「入れない (作者判断)」となっています。release-notes は担当外なので変えていません。
5. ドロップでは `.cdb2` を結晶リストとして受け付けますが、メニューの Load のフィルタは *.xml;*.out だけです。ConvertCrystalData.ConvertToCrystalList が cdb2 を正しく読めるかは確認していません。
6. Reset Rotation の「c 軸がスクリーンに垂直、b 軸が上向き」と、Euler の適用順「Ψ→Θ→Φ」(Matrix3D.Rot(phi,theta,psi)) は、座標系の規約 (別グループ/4-rotation-geometry の担当) に依存するので検証していません。
7. AMCSD「20,000 構造以上」の件数は検証していません (AMCSD.cdb3 は数えていない)。
8. 「イオン散乱因子を使用」の効く範囲として、動力学電子線 (Bethe) と Kikuchi プロファイル (IKikuchiInelasticKernel) の 2 つを書きました。HRTEM/STEM のポテンシャルなど他の経路でも ElasticIonModel を参照しているかどうか、念のため確認をお願いします。
9. Atom タブ内パネルを右クリックすると出る "Set Debye-Waller Factors to Zero" はマニュアルに無いままです。載せるかどうか判断をお願いします。
10. アプリ全体の Ctrl+Shift+矢印は SpotIDv2・HRTEM・EBSD などでは効きません (FormMain_KeyDown を配線していない)。意図どおりかを確認したいです。
11. Basic Info の「Molar Volume / Molar Mass / Density は原子情報があるときのみ表示」は、表示を切り替えるコードまで追っていません。
12. 21-shortcuts のうち、5–12・20 の各ウィンドウ固有の項目は、主なもの (StructureViewer Ctrl+Shift+C、Stereonet ダブルクリック、HRTEM Ctrl+C、Holder の矢印、Macro Ctrl+S/F10、回折パターンの左ドラッグ中心/外周) しかコードと照合していません。他の項目は各ページの担当グループの結果と突き合わせてください。

### 変更したファイル

- /home/user/ReciPro/docs/src/en/0-main-window.md
- /home/user/ReciPro/docs/src/ja/0-main-window.md
- /home/user/ReciPro/docs/src/en/1-crystal-database.md
- /home/user/ReciPro/docs/src/ja/1-crystal-database.md
- /home/user/ReciPro/docs/src/en/21-shortcuts.md
- /home/user/ReciPro/docs/src/ja/21-shortcuts.md
- /home/user/ReciPro/docs/src/en/index.md
- /home/user/ReciPro/docs/src/ja/index.md


## B-symmetry-beam 監査結果

対象: docs/src/{en,ja}/2-symmetry-information.md, 3-beam-interaction.md, appendix/a4-symmetry-space-groups/*.md
コード: Crystallography.Controls/Crystal/FormSymmetryInformation.*, FormBeamInteraction.*, FormGroupRelations.*, SymmetryDiagram*.cs

### 食い違い

| ページ | 節 | マニュアルの記述 | コードの実態 | 根拠 (file:method/行) | 分類 | 対応 |
|---|---|---|---|---|---|---|
| 2-symmetry (en/ja) | Wyckoff Positions | 多重度・記号・サイト対称性・「一般/特殊位置の別」を表示 | 一般/特殊の列は無い。列は Mult./Wyck. Let./Site Sym./Coordinates(最大4個/行、超過分は次行) | FormSymmetryInformation.cs:SetupExtraTables, SetWyckoffPosition | 不正確 | 修正済 |
| 2-symmetry (en/ja) | Geometrics | 面間隔・軸長・面間角/軸間角・晶帯軸/晶帯面 | 面1–軸1、面2–軸2 のなす角も計算・表示 (numericBoxAnglePlaneAxis1/2)。長さÅ・角度° | FormSymmetryInformation.cs:numericBox_ValueChanged | 不正確(欠落) | 修正済 |
| 2-symmetry (en/ja) | 模式図・一般位置 | 円に「分率座標」を付記 | 付記されるのは投影軸方向の高さラベル (+, −, ½+ …) | SymmetryDiagramPositions.cs:ComputeDepthLabel | 誤り | 修正済 |
| 2-symmetry (en/ja) | 模式図下のコントロール | Direction で投影軸を選択 (無条件) | a/b/c は直方晶のみ有効 (既定 c)。単斜晶はユニーク軸、他晶系は c に自動固定 | FormSymmetryInformation.cs:ChangeCrystal; SymmetryDiagramCommon.cs:ResolveProjectionAxis | 不正確 | 修正済 |
| 2-symmetry (en/ja) | 模式図下のコントロール | 「模式図の下」に Direction/Copy/Group Relations | Direction・Copy format・Group Relations はフォーム下端の Options パネル、Copy ボタンは各図の上 (Dock=Top) | FormSymmetryInformation.Designer.cs: flowLayoutPanel2/3 (Dock Top), flowLayoutPanel4 (Dock Bottom) | 不正確 | 修正済 |
| 2-symmetry (ja) | 空間群の情報 | Point Group（結晶族点群） | groupBoxPointGroup の ja は「点群」。「結晶族」は crystal family で別概念 | FormSymmetryInformation.cs:LocalizeLabels | ja-en不一致/誤り | 修正済 |
| appendix A4.1 symbols-and-diagrams (en/ja) | General-position diagram 末尾 | 「Direction の下にある x/y/z の欄」 | 試験点の欄は一般位置図の Copy ボタンの右 (図の上)、ラベルは a / b / c、範囲 −1〜1 | FormSymmetryInformation.Designer.cs: flowLayoutPanel3 (labelLaTex1-3 "a","b","c", numericBoxPositionA-C) | 誤り | 修正済 |
| 3-beam (en/ja) | Reflections / Options | BB 強度は「多重度とローレンツ偏光因子を含む」 | X線: m|F|²/V²·(1+cos²2θ)/(sin2θ sinθ)。電子線・中性子は偏光項なし (1/(sin2θ sinθ))。オン時は2つの Hide チェックを強制オン＋Disabled | FormBeamInteraction.cs:SetSortedPlanes, checkBoxBragBrentano_CheckedChanged | 不正確 | 修正済 |
| 3-beam (en/ja) | Reflections 表 | 列一覧に Condition / i 列なし。Multi. は常時表示の書きぶり | Condition 列は Hide prohibited planes オフ時のみ表示。Multi. は Hide equivalent planes オン時のみ。i 列は Miller-Bravais 有効時 | FormBeamInteraction.cs:SetSortedPlanes, MillerBravais; CrystalControl.cs:977 | 不正確(欠落) | 修正済 |
| 3-beam (en/ja) | Reflections / Options | 既定値の記載なし | Hide equivalent / Hide prohibited とも既定オン、d-Spacing Cutoff 既定 1 Å (Max 10) | FormBeamInteraction.Designer.cs (Checked=true, numericBoxCutoffD.Value=1) | 不正確(欠落) | 修正済(既定値追記) |
| 3-beam (en/ja) | Attenuations / X-ray | 光子エネルギー 1–60 keV、「対数軸」 | エネルギー軸は線形 (FixLowerXToZero, 上限 60)。点は対数間隔。縦軸が µ/ρ・µ で対数、T% で線形 | FormBeamInteraction.cs:DrawXrayAttenuationGraph; Designer: graphControlAtten.FixLowerXToZero | 誤り | 修正済 |
| 3-beam (en/ja) | Attenuations / X-ray | Thickness t (µm) 既定値なし | 既定 100 µm (0〜1e7) | FormBeamInteraction.Designer.cs: numericBoxAttenThickness | 不正確(欠落) | 修正済 |
| 3-beam (en/ja) | Attenuations / Electron | ビームエネルギー 1–30 keV | 1–50 keV で計算 (260611Cl で 30→50 に拡大)、初期表示 0–20 keV | FormBeamInteraction.cs:DrawElectronTransportGraph (kMin=1,kMax=50) | 古い | 修正済 |
| 3-beam (ja) | Scattering factors / X線 | 「f(s) を選ぶとラジオボタンの行がもう1つ現れます」 | 出典行は X線時は常に表示、F(q)+S(q) 時に Enabled=false (en の「greyed out」が正しい) | FormBeamInteraction.cs:577 付近 (flowLayoutPanelSource_Xray.Visible/Enabled) | ja-en不一致(軽微) | 未修正 |
| appendix A4.2 group-subgroup | Navigating | （確認のみ）スピナー 2–27 既定4、Search window 2–8 既定4、タブ名・バナー文言・Home(ja 現結晶)・`i: index ≤ 4 only` | すべてコードと一致 | FormGroupRelations.Designer.cs: numericIsoMax / numericReflMax; FormGroupRelations.cs:LocalizeLabels | — | 問題なし |

### マニュアルに無い機能

| フォーム | 機能/コントロール | 概要 | 根拠 | 対応 |
|---|---|---|---|---|
| FormSymmetryInformation | 試験点 a/b/c 入力 (numericBoxPositionA-C) | 一般位置図の代表点を指定 (−1〜1, 刻み0.01)。点群変化時のみ既定値 (SymmetryDiagramPositions.GetTestPoint、晶系別) にリセット | FormSymmetryInformation.cs:ChangeCrystal | 追記済 (2-symmetry 本文。appendix は位置記述を修正) |
| FormSymmetryInformation | Conditions タブの3列構成 | Indices / Condition / Element、条件なしは "No Condition" | FormSymmetryInformation.cs:SetupExtraTables, ChangeCrystal | 追記済 |
| FormSymmetryInformation | EMF コピーの背景色 | 画面と同じ配色 (ダーク時 #202020) でコピー | FormSymmetryInformation.cs:CopyAsMetafile | 未対応 (些末) |
| FormSymmetryInformation | Group Relations の追随 | 開いている群の関係ウィンドウは結晶切替で新しい空間群へ自動追随 (Home も更新) | FormSymmetryInformation.cs:ChangeCrystal | 未対応 |
| FormBeamInteraction | "test" チェックボックス (checkBoxTest) と h,k,l の Min/Max/Step パネル | 任意 (分数を含む) h,k,l を掃引して構造因子を表示するテスト機能。Reflections タブに常時見えている | FormBeamInteraction.cs:numericBoxH_min_ValueChanged, checkBoxTest_CheckedChanged | 未対応 (要確認1) |
| FormBeamInteraction | 散乱因子グラフの s 範囲 | X線 0–4 Å⁻¹ (初期 0–2)、電子線 0–2 Å⁻¹ (初期 0–1) | FormBeamInteraction.cs:scatSMaxXray/scatSMaxElectron | 未対応 |
| FormBeamInteraction | 回折ピーク図の hkl ラベル数 | 強度上位30本のみラベル | FormBeamInteraction.cs:scatMaxPeakLabels | 未対応 |
| FormBeamInteraction | 既定モデル | 電子線散乱因子 既定 Peng、X線 f(s)+Waasmaier–Kirfel、Debye-Waller 既定オフ | FormBeamInteraction.cs:513-517, Designer | 未対応 (WK既定は既述) |
| FormGroupRelations | 超群側の同型枝 | スピナーに関係なく index ≤ 4 固定 | FormGroupRelations.cs:566 | 未対応 (現記述は部分群側のみと読めるので矛盾はない) |

### 要確認事項

1. Beam Interaction の **test** チェックボックス（手動 h,k,l 掃引パネル）はユーザー向け機能としてマニュアルに載せるか、それとも開発用で非表示にすべきか。現状は Reflections タブに常時表示。
2. `numericBoxCutoffD.Minimum = (A+B+C)/20`（FormBeamInteraction.cs:crystalControl_CrystalChanged）は nm 基準の値をそのまま表示単位の Minimum に入れている。単位 Å のとき最小値が意図の 1/10 になる可能性。また nm→Å 切替時 `Value*10` が Maximum=10 を超え得る（値>1 nm のとき）。意図どおりか。
3. 電子輸送 MonteCarlo はキャッシュ構築が「固定 30 keV」とのコメント（GetAttenMc 付近）だが、グラフは 50 keV まで描く。30–50 keV 域の値の妥当性（NIST Mott は 36 keV まで、その先は screened Rutherford）をマニュアルに注記すべきか。
4. Reflections の Rel. Int. は SetSortedPlanes で全 VectorOfG（フィルタ前、消滅面含む）の最大で正規化している。フィルタ後の表で最大が 100 にならないケースがあり得るか（消滅面は F≈0 なので通常問題ないはずだが、Hide equivalent オフ時など）。
5. ja 3-beam の「f(s) を選ぶと…行がもう1つ現れます」は「X線では常に表示され、F(q)+S(q) 時は淡色」に直すべきか（en は正しい）。今回は未修正。
6. 2-symmetry の Direction 説明で ja は「直方晶（斜方晶）」と併記した。用語統一の方針があれば合わせてほしい。

### 変更したファイル

- docs/src/en/2-symmetry-information.md
- docs/src/ja/2-symmetry-information.md
- docs/src/en/3-beam-interaction.md
- docs/src/ja/3-beam-interaction.md
- docs/src/en/appendix/a4-symmetry-space-groups/symbols-and-diagrams.md
- docs/src/ja/appendix/a4-symmetry-space-groups/symbols-and-diagrams.md


## 監査報告: C-rotation-viewer-stereonet

対象: docs/src/{en,ja}/4-rotation-geometry.md, 5-structure-viewer.md, 6-stereonet.md, appendix/a1-coordinate-system/{1-orientation,2-diffraction}.md

### 座標系・符号の確認結果（問題なし）
- `Euler.ToMatrix` / `Matrix3D.Rot(phi,theta,psi)`（Matrix3D コンストラクタは列優先 e11,e21,e31,…）を展開すると、4章の行列 $R=R_Z(\Phi)R_X(\theta)R_Z(\Psi)$ と全 9 成分が一致。`Euler.FromMatrix` の逆変換（θ=acos E33, cosΦ=−E23/sinθ, cosΨ=E32/sinθ, 符号は E13/E31）も整合。
- `Matrix3D.Rot(axis,θ)` は右手系の能動回転。FormMain.Rotate は `rot * Crystal.RotationMatrix`（実験室固定軸まわり左掛け）で、$\mathbf v_{rot}=R\mathbf v_0$ の記述と整合。
- Crystal.SetAxis: c=(0,0,C), b=(0,B sinα, B cosα), a は x>0 → A1.1「c∥Z, b は YZ 面内, (100) 法線 ∥ X」と一致。
- Macro `GetRotationMatrix` は `ToArrayRowMajorOrder`、FormRotation の Copy は行順 → 4章の記述と一致。
- A1.2 の検出器傾斜: FormDiffractionSimulatorGeometry.DetectorRotation は (cosφ, sinφ, 0) まわりの τ 右手回転 → 表と一致。
- ステレオネットの上半球 = +Z（観察者側）、Wulff/Schmidt 逆変換も A1.1 と整合。

### 食い違い

| ページ | 節 | マニュアルの記述 | コードの実態 | 根拠 (file:method/行) | 分類 | 対応 |
|---|---|---|---|---|---|---|
| 4-rotation-geometry | Φ, θ, Ψ | 「数値ボックスはすべて編集可能。変更はメイン窓に反映」 | Φ/Θ/Ψ と行列 9 個はすべて ReadOnly。方位変更はメイン窓・Paste・Link 経由のみ | FormRotation.Designer.cs: numericBoxPhi/Theta/Psi, numericBox11–33 `ReadOnly = true` | 誤り | 修正済 (en/ja) |
| 4-rotation-geometry | OpenGL windows | 赤緑青の小球は Φ=θ=Ψ=0 で +X,+Y,+Z に一致 | 小球は回転後の規格化 ±a,±b,±c（ラベル +a/−a 等）。初期方位では a,b は直交格子以外 X,Y と一致しない | FormRotation.cs:createObject | 誤り | 修正済 (en/ja) |
| 4-rotation-geometry | 1st/2nd/3rd axes | 軸は ±X, ±Y, ±Z から選択 | X/Y/Z（正方向のみ）。第2軸≠第1軸、第3軸≠第2軸。既定 X–Z–Y | FormRotation.cs:getExpDirections, RadioButton_CheckedChanged; Designer の Checked 初期値 | 誤り | 修正済 (en/ja) |
| 4-rotation-geometry | Buttons | 「Isometric」 | ボタン表記は「View Isometric」 | FormRotation.resx:buttonViewIsometric.Text | 不正確 | 修正済 (en) |
| 4-rotation-geometry | Link | 両系の角度を一致させる、のみ | Link 時に $R_{base}=R_{exp}^{-1}R$ を記憶。以後メイン窓の回転は有効・非固定のゴニオ角へ数値分解され「到達可能な最近方位」に拘束される | FormRotation.cs:CheckBoxLink_CheckedChanged, SetRotation(Matrix3D); FormMain.cs:Rotate | 不正確(欠落) | 追記済 (en/ja) |
| 5-structure-viewer | File menu | Shortcut Hints が File 内 | Shortcut Hints は独立トップメニュー。File の Save/Copy は Main/Legend/Crystal Axes/Light のサブメニュー、Save Movie は Main Image/Crystal Axes のサブメニュー | FormStructureViewer.designer.cs:1870–1978 | 不正確 | 修正済 (en/ja) |
| 5-structure-viewer | Main area | Size(W×H) は保存時ピクセルサイズ | GL ビューのクライアントサイズを設定しウィンドウ自体をリサイズ（結果として保存サイズ） | FormStructureViewer.cs:sizeControl1_ValueChanged | 不正確 | 修正済 (en/ja) |
| 5-structure-viewer | Bounds by crystal planes | 閉じない場合 1 単位胞にフォールバック | 半径 Max Distance from Origin (Å) の準球多面体 (6面×7×7 法線) を追加して閉じる | FormStructureViewer.cs:initBounds; BoundControl.MaximumDistance | 誤り | 修正済 (en/ja) |
| 5-structure-viewer | Distance from origin | 単位を d / Å から選択 | d× と Å の 2 ボックスが常時連動表示。別に Translation(Å)・Max Distance・Update Immediately がある | BoundControl.resx / BoundControl.cs | 不正確 | 修正済 (en/ja) |
| 5-structure-viewer | Hide atoms | 「Hide atoms」チェックボックスで原子・結合・多面体を非表示 | 該当チェックボックスは存在しない。同等効果はツールバー「Atoms」オフ（原子生成なし→結合・多面体も消える） | FormStructureViewer.designer.cs / BoundControl.Designer.cs（該当なし）; FormStructureViewer.cs:setAtoms | 誤り | 修正済 (en/ja) |
| 5-structure-viewer | Depth fading out | Far より遠いと完全透明 | 色を背景色へ線形ブレンド（alpha は不変）。Near/Far は Å | Shaders/fragZSORT.c, fragPPLL.c, fragDDP.c:applyDepthCueing | 誤り | 修正済 (en/ja) |
| 5-structure-viewer | Transparency mode | Approximate / Perfect の 2 択 | Approximate (Z sort) / High quality (DDP, Tools▸Enable DDP 時のみ) / High quality (PPLL, GL4.3+ のみ) の 3 種 | FormStructureViewer.cs:rebuildTransparencyModeItems; resx comboBoxTransparency.Items | 古い | 修正済 (en/ja) |
| 5-structure-viewer | Symmetry Elements | 「対称心 と 回反軸」を同一クラス | 回反軸は Symmetry axes グループ（回転・らせんと同じ）。Planes=鏡映・映進、Inversion centers 単独。線幅は軸・面のみ | FormStructureViewer.resx:groupBoxAxes/Planes/Inversion | 不正確 | 修正済 (en/ja) |
| 5-structure-viewer | Toolbar | Like Vesta = 原子色・半径 | 加えて結合リストを Clear して既定結合で置換 | FormStructureViewer.cs:toolStripButtonLikeVesta_Click | 不正確 | 修正済 (en/ja) |
| 5-structure-viewer | Export 3D Model | セル枠は「チェックしたときだけ」 | 枠が表示中なら既定でチェック ON | FormExport3DModel.cs:ctor (`checkBoxCellEdges.Checked = hasLines`) | 不正確 | 修正済 (en/ja) |
| 5-structure-viewer | Rendering quality | メッシュ分割とアンチエイリアス | ハンドラはメッシュ分割数 (Cone/Cylinder slices, Sphere subdivision) と DDP パス数のみ変更。AA は変えていない | FormStructureViewer.cs:comboBoxRenderingQuality_SelectedIndexChanged | 不正確 | 未修正（要確認 5） |
| 5-structure-viewer | Bonds | 「Bond Radius」「Alpha」「Inner Bonds」等 | UI は Radius / Opacity / Show Inner Bonds / Show Center Atom 等 | BondControl.resx | 不正確(ラベル) | 未修正 |
| 5-structure-viewer (ja) | 描画範囲 | 見出し「描画範囲を結晶面で指定」「境界面で物体をクリップ」「境界外の結合原子も表示」 | ja UI は「描画範囲を格子面単位で指定」「境界面でクリップ」「境界外ではあるが結合している原子を表示」 | FormStructureViewer.ja.resx | ja-en不一致(ラベル) | 未修正 |
| 5-structure-viewer | Bounds by cell | Range = 上限/下限 | Range は Center まわりの ±半幅 | FormStructureViewer.cs:initBounds | 不正確 | 未修正（軽微） |
| 6-stereonet | Great and Small Circle | 大円と小円を描画 | 大円のみ（DrawCircles は極ベクトルの大円のみ） | FormStereonet.cs:buttonAddCircle_Click, DrawCircles | 誤り | 修正済 (en/ja, 見出しも) |
| 6-stereonet | Colour / Size | 「Change colour automatically」 | en UI は「Rotate color」 | FormStereonet.resx:checkBoxRotateColor.Text | 誤り(ラベル) | 修正済 (en) |
| 6-stereonet | Display options | 構造因子 \|F\| で重み付け | I_rel=\|F\|²/max\|F\|²。Planes: 点半径×√I_rel (∝\|F\|)、Kikuchi: 線の不透明度=I_rel | FormStereonet.cs:setVector, DrawStereoNet | 不正確 | 修正済 (en/ja) |
| 6-stereonet | Shortcuts | 「3D display」 | UI は「3D View」（ja「3D描画」） | FormStereonet.resx / ja.resx:checkBoxDisplay3D | 不正確(ラベル) | 修正済 (en/ja) |
| 6-stereonet | 3D Options | 「球の不透明度、軸インジケータなど」 | Reset / Label / Hemisphere / Stereonet / Sphere outline / Projection lines / Depth Fade | FormStereonet.resx; FormStereonet.cs:Draw3D | 不正確 | 修正済 (en/ja) |
| 6-stereonet | Wave | Planes/Kikuchi 選択時のみ「有効」 | タブは常に操作可。値が使われるのが Planes/Kikuchi のみ（Kikuchi では波長が線対間隔も決める） | FormStereonet.cs:DrawStereoNet (sinθ=λ\|g\|/2) | 不正確 | 修正済 (en/ja) |
| 6-stereonet | File menu | 保存/コピーのみ | Save Movie (Stereonet/3D)、Page Setup/Print Preview/Print もある | FormStereonet.Designer.cs:1100 | 不正確(欠落) | 追記済 (en/ja) |

### マニュアルに無い機能

| フォーム | 機能/コントロール | 概要 | 根拠 | 対応 |
|---|---|---|---|---|
| FormRotation | 2nd axis / 3rd axis チェック | ゴニオ段の有効/無効（無効で角度0、2nd 無効で 3rd も無効） | FormRotation.cs:checkBox1st_CheckedChanged | 追記済 |
| FormRotation | Fix ×3 | Link 時の分解で角度固定 | FormRotation.cs:SetRotation(Matrix3D) | 追記済 |
| FormRotation | Reset Angles | 実験角を全て 0 | buttonResetExpEuler_Click | 追記済 |
| FormRotation | 実験角の合成式・±180° 折り返し | $R_{exp}=R_{e1}R_{e2}R_{e3}$ | RotExp, NumericBoxExp_ValueChanged | 追記済 |
| FormStructureViewer | Tools ▸ I like VESTA / Enable DDP | VESTA 風、DDP 項目の表示切替 | designer:1966; initializeDdpDeveloperMenu | 追記済 |
| FormStructureViewer | 画像保存/コピー/動画のサブメニュー（結晶軸・光源ウィジェット単体） | | designer:1883–1955 | 追記済 |
| FormExport3DModel | see-through mesh（bar dia. / pitch） | v4.946 追加の透かし格子 | FormExport3DModel.cs; ExportModel | 追記済 |
| BoundControl | Translation / Max Distance from Origin / Update Immediately | | BoundControl.resx/.cs | 追記済 |
| FormStructureViewer | Unit Cell: Show Unit Cell / All・Each 色 / Edge width / Opacity | | setUnitCellPlanes | 追記済 |
| FormStructureViewer | Unit Cell 平行移動 ↔ 投影中心の連動 (0.5−x) | | numericBoxProjectionCenterX_ValueChanged | 追記済 |
| FormStructureViewer | 凡例クリックでラベル個別切替、Label の White edge / material/fixed color | | legendControl_MouseClick | 追記済 |
| FormStructureViewer | Unit Cell: Show Subcell (a,b,c 分割数 1–10) | UI はあるが描画コードで参照されていない | setUnitCellPlanes に参照なし | 未対応（要確認 3） |
| FormStructureViewer | 主ビュー外周ドラッグで視線軸まわり回転、ドラッグは結晶方位そのものを回す | | glControlMain_MouseMove/getRotation | 未対応（21-shortcuts 側の担当） |
| FormStructureViewer | Coordination: Max. distance | 配位表の最大距離 (Å) | AtomCoordinateTable.Designer.cs | 未対応（軽微） |
| FormStructureViewer | Atoms Appearance: Ambient/Diffusion/Specular/Emission/Shininess/Alpha、Apply to all elements、Up/Down | | AtomControl.resx | 未対応（AtomControl は 1章側でも扱う共通部品） |
| FormStructureViewer | Macro からの操作 (StructureViewer.SaveImage/Export3DModel) | v4.946 | FormStructureViewer.cs:SaveMainImage, ExportModel | 未対応（マクロ章の担当） |
| FormStereonet | Structure factor（トップ N）指数モード | Planes/Kikuchi で \|F\|² 上位 N 面 | setVector, radioButtonRange_CheckedChanged | 追記済 |
| FormStereonet | Anomalous dispersion | X 線 f', f'' | checkBoxAnomalousDispersion_CheckedChanged | 追記済 |
| FormStereonet | 下半球は白抜き円 | | DrawStereoNet | 追記済 |
| FormStereonet | Range モードは既約指数のみ・等価組ごとに色 | | setVector | 追記済 |
| FormStereonet | マウス位置の Tilt X/Y 表示 | | graphicsBox_MouseMove (labelXYpos) | 未対応（軽微） |
| FormStereonet | YusaGonio タブ | Load 時に TabPages.Remove されるため非表示 | FormStereonet_Load | 未対応（不要） |
| FormAtom / FormResolution | — | どこからも new されていない死にコード | grep 結果 | 未対応（不要） |

### 要確認事項
1. **FormStereonet.Draw3D の早期 return 条件**: `if (glControl == null || checkBoxDisplay3D.Checked) return;` — 3D View がオンのときに描画をスキップしており、逆条件 (`!Checked`) のバグに見える（オン中は結晶回転しても 3D が更新されない可能性）。checkBoxDisplay3D_CheckedChanged も Draw3D を呼ばない。マニュアルは「意図どおり動く」前提で書いた。
2. **Rotation Geometry の Copy → Paste 往復**: Copy は行区切り `\n`、Paste は `\r\n` で split（`separator`）。ReciPro 自身のコピーを貼ると行数<3で無視される（Excel 経由なら `\r\n` なので可）。マニュアルの「round-trip」表現のままでよいか。
3. **Structure Viewer「Show Subcell」**（a/b/c 分割数）: UI と ToolTip はあるが setUnitCellPlanes で未使用。未実装なら UI を隠すか、マニュアルに書かないままでよいか。
4. **Structure Viewer の Save/Copy ▸ Legend**: Click ハンドラ未接続で無動作。実装予定か削除か（マニュアルには「現状無効」と記載した）。
5. **Rendering quality の説明**「アンチエイリアス」: ハンドラでは AA を変更していない（FXAA は別途?）。記述を「メッシュ分割数」に限定してよいか。
6. **Link 中の SetRotation(bool renewExpEuler=true)**: メイン窓で Euler 直接入力・Reset rotation など `FormMain.SetRotation(mat)` 経由（SkipEulerChange=false）の更新が来ると、FormRotation.SetRotation() が Enable 2nd/3rd を強制 ON・Fix を全解除してから分解する。ユーザーが設定した Fix/無効軸が黙って解除される仕様でよいか（マニュアルにはこの挙動は書いていない）。
7. **FormStereonet.ja.resx の label2.Text = 'Tilt Y:'**: en は 'Zone axis / Lattice plane: '（label3 が 'Tilt X/Y:'）。ja の訳が取り違えに見える（resx は担当外のため未修正）。
8. 4章ショートカット表「Mouse wheel / Right-drag: Zoom (the large goniometer views)」: glControl*Gonio は AllowMouseScaling 既定値依存。動作は実機で未確認。

### 変更したファイル
- docs/src/en/4-rotation-geometry.md
- docs/src/ja/4-rotation-geometry.md
- docs/src/en/5-structure-viewer.md
- docs/src/ja/5-structure-viewer.md
- docs/src/en/6-stereonet.md
- docs/src/ja/6-stereonet.md
（appendix/a1-coordinate-system/*.md は検証の結果コードと一致したため変更なし）


## D-diffraction 監査報告 (7-diffraction-simulator: index / 1-saed / 2-ped / 3-cbed)

対象コード: ReciPro/DiffractionSimulator/FormDiffractionSimulator*.cs (ALCHEMI 除く), ReciPro/FormDiffractionSpotInfo.*, 参照のみ Crystallography/Diffraction/BetheMethod.cs, Crystallography.Controls/GraphicsBox/GraphicsBox.cs。
略記: FDS = FormDiffractionSimulator。修正はすべて en/ja 両方に入れ、直前（リスト項目・段落内は行末/文頭インライン）に `261105Cl draft` コメントを付けた。

### 食い違い

| ページ | 節 | マニュアルの記述 | コードの実態 | 根拠 (file:method/行) | 分類 | 対応 |
|---|---|---|---|---|---|---|
| index | Keyboard & mouse shortcuts | 「マウスホイールズームは無い」 | ホイールで ×2/×0.5 ズーム（カーソル中心、Fix 時は固定中心）。260801Cl で追加 | FDS.cs:graphicsBox_MouseWheel, FDS.Designer.cs:1216 MouseWheelZoom=true, GraphicsBox.cs:GetWheelZoomFactor | 古い | 修正済（行追加、右クリック縮小 ×1/1.2 も明記） |
| index | Mode quick reference 注記 | 入射ビーム名 "Precession (electron = PED)", "Convergence (CBED)" 等 | en UI は "Parallel beam / Precession (electron) / Convergence (electron) / Precession (X-ray, ZOLZ) / Back Laue (X-ray)"。線源変更で常に Parallel に戻る。X線/中性子では Dynamical 非表示→Kinematical に戻る。Back Laue は白色 (Monochrome=false) | FDS.resx, FDS.cs:WaveLengthControl_WaveSourceChanged, radioButtonBeamParallel_CheckedChanged | 不正確 | 修正済 (en/ja) |
| index | Mouse position | 「詳細で最近接反射の (hkl)・励起誤差・構造因子を追加表示」 | 基本: d, d⁻¹, 2θ(°, rad/mrad)。Details: 検出器座標 (mm, 垂線の足原点)・実空間座標 (mm, 結晶原点)・逆空間座標 (nm⁻¹) のみ | FDS.cs:graphicsBox_MouseMove, checkBoxMousePositionDetailes_CheckedChanged | 誤り | 修正済 |
| index | File menu | Save / Save detector area / Copy / Copy detector area の 4 項目 | 実際は Save Image (PNG/EMF), Save detector area (検出器領域表示時のみ), Save CBED disks (CBED 時のみ; 個別 PNG/TIFF・集合 PNG/TIFF), Copy, Copy detector area, Copy CBED disks, Save Movie (Diffraction pattern / Reciprocal space), Page Setup / Print Preview / Print | FDS.Designer.cs:fileToolStripMenuItem, FDS.cs:radioButtonIntensityCalculationMethod_CheckedChanged | 古い | 修正済 |
| index | Preset | 「設定一式をプリセットとして保存・呼び出し」 | 固定 7 項目 (Electron 300/200/120, X-ray 30/20 keV, MoKα₁, CuKα₁)。線源とエネルギーだけ設定。保存機能なし | FDS.cs:presetToolStripMenuItem_Click | 誤り | 修正済 |
| index | Screen: Resolution | 「1 ピクセルのサイズ (mm)」 | mm/px または nm⁻¹/px 切替。既定 0.08 mm/px (1e-5〜10) | FDS.Designer.cs:numericBoxResolution, FDS.cs:radioButtonResoUnit_CheckedChanged | 不正確 | 修正済 |
| index | Screen: Set centre / Fix | 「任意ピクセルに中心を設定」 | コンボで 垂線の足 / ダイレクトスポット / 検出器中心 から選ぶ。Fix 既定オン | FDS.cs:FixedCenter, FDS.Designer.cs:comboBoxCenter, checkBoxFixCenter | 誤り | 修正済 |
| index | Screen: Reciprocal space | 「パターン上にエワルド球と逆格子ベクトルを重ねて描画」 | パターン横の別ペイン (splitContainer1.Panel2) に 3D 表示。色・半径・しきい値・視点方向等の Options 付き | FDS.cs:checkBoxReciprocalSpace_CheckedChanged, Draw3D; FDS.Designer.cs:groupBoxOptions | 不正確 | 修正済（Options も概説） |
| index | Detector | 「Camera length: 試料〜検出器距離」 | UI は "Camera Length 2"。試料から検出器面への垂線の足までの距離 (既定 1000 mm)。τ=0 で通常のカメラ長 | FDS.cs:CameraLength2, DetectorRotation コメント | 不正確 (en) | 修正済 (en/ja) |
| index | TEM holder | 「ダブルティルト（または回転）ホルダー」 | 回転ホルダーのモードは無い。Tilt X(主)/Tilt Y(副) のみ。矢印 ←→=Tilt X, ↑↓=Tilt Y、ステップ 0.1〜2° 既定 0.2° | FormDiffractionSimulatorHolder.cs:HolderRotation, tiltByArrowKey | 不正確 | 修正済（Link/TEM 設定/180°回転も追記） |
| index | General tab | 「スポット・ラベル・菊池線・デバイリングの色を設定」 | General タブは Text size と 文字色・垂線の足・ダイレクト位置・背景の色のみ。スポット色は Appearance、菊池/デバイ/スケール色は各タブ | FDS.Designer.cs:tabPageGeneral | 誤り | 修正済 |
| index | Kikuchi tab: Threshold | 「旧来パラメータ Threshold（d で制限）が残置」 | そのようなコントロールは存在しない（選択は Structure factor Top N / 1/d Cutoff のみ） | FDS.Designer.cs:tabPageKikuchi | 古い | 修正済（存在しない旨に置換） |
| index | Kikuchi tab | 既定値記載なし | Top 既定 100 (1–1000)、1/d < 10 nm⁻¹ (0–100)、Mode 既定 Kinematical、Quality 既定 Standard、Scale 既定 Tanh | FDS.Designer.cs:1007-1036, FDS.cs:コンストラクタ | 欠落 | 修正済（追記） |
| index | Scale tab: Division | 「隣接目盛り線の角度間隔」 | Fine / Medium(既定) / Coarse の 3 択 | FDS.Designer.cs:radioButtonScaleDivision* | 不正確 | 修正済 |
| index | Misc タブ | 「Misc タブ: マウス感度」 | タブは General/Kikuchi/Debye/Scale の 4 枚のみ。回転感度は右パネルの Misc グループ（既に別節で記載） | FDS.Designer.cs:tabControl.Controls.Add | 誤り | 修正済（存在しない旨に置換） |
| index | Diffraction spot information | 「Spot Details ボタンまたは Details チェックボックスで開く」 | Details チェックボックスはマウス位置詳細の表示切替のみ。Spot Details は非動力学モードでも開け、描画中の反射の Vg, Pg, Qg を表示 | FDS.cs:ButtonDetailsOfSpots_Click, checkBoxMousePositionDetailes_CheckedChanged; FormDiffractionSpotInfo.cs:SetTable(double, Crystal) | 誤り | 修正済（Quick routes 表も修正） |
| index | Diffraction spot information | Effective digit 既定値なし | 1〜16、既定 4 | FormDiffractionSpotInfo.Designer.cs:numericBoxEffectiveDigit | 欠落 | 修正済 |
| index | Detector geometry | τ/φ の説明のみ | Camera length 2 (1–10000, 既定 1000)、τ (±200°)、φ、.prm ドロップで波長/CL2/τ/φ/foot/pixel size 読込、Detector 1024×1024、pix. size 0.1 mm、Foot fx,fy 512 px、Opacity/Gradient/Scale1/Scale2/Max-Min int./Rot 90°/Clear/Schematic diagram | FormDiffractionSimulatorGeometry.Designer.cs, .cs:FormDiffractionSimulatorGeometry_DragDrop | 欠落 | 修正済（追記） |
| index | Dynamic compression | 「.txt 圧力/時間プロファイルをドロップ、赤線ドラッグで時間を走査」 | .txt は 位置・時刻・圧力 の 3 列タブ区切り。時刻は Time スライダー (ns) で選択。グラフの縦線 2 本は Front/Back 位置。他に 2018/2019 model, Us/Ur, EOS, Shocked/Slip plane, 回転分布, 質量吸収係数, Save patterns, Simulate | FormDiffractionSimulatorDynamicCompression.cs:…_DragDrop, trackBarAdvancedTime_ValueChanged, graphControl1_LinePositionChanged | 誤り | 修正済（ショートカット表も） |
| index | 冒頭・各所 | 右パネル "Spot property" / 見出し "Screen" / "Detector (camera length)" | UI ラベルは "Simulation settings"(ja シミュレーション条件) / "Display settings"(ja 画面) / "Detector geometry & overlay image"(ja 検出器情報) | FDS.resx groupBoxSpotProperty/groupBoxMonitor/groupBoxDetectorGeometry | 不正確 | 未修正（見出し・アンカー変更を避けた） |
| SAED | Wave Length | 既定値なし | 初期 200 keV。Presets で 300/200/120 kV | FDS.Designer.cs:waveLengthControl | 欠落 | 修正済 |
| SAED/PED | 見出し "Wave Length" / "Incident beam mode" | — | UI ラベルは "Wave source"(線源) / "Optics"(入射ビーム) | FDS.resx label3, label13 | 不正確 | 未修正 |
| SAED | Intensity: Kinematical | ラベル "Kinematical" | en UI は "Kinematical && exc. error"、Dynamical は "Dynamical theory" | FDS.resx | 不正確 | 未修正（index 注記のみ "Dynamical theory" に） |
| SAED | Bloch wave settings | 既定値なし | 回折波の数 8–1000 既定 240、厚さ 0.01–10000 nm 既定 50 nm | FDS.Designer.cs:2369-2394 | 欠落 | 修正済 (SAED/PED) |
| SAED | Appearance: Solid sphere | 「円の面積が回折強度に対応」 | Kinematical は球半径 R·I^(1/3)（体積∝I、エワルド球で切断）、Excitation only は R、Dynamical は切断なしで R·I^(1/2) の円 | FDS.cs:DrawDiffractionSpots 1180-1200 | 不正確 | 修正済 |
| SAED/PED | Appearance: Color scale | 「Gray scale または Cold-warm」 | Gray / Cold-Warm / Spectrum / Fire | FDS.resx comboBoxScaleColorScale.Items* | 古い | 修正済 |
| SAED/PED | Appearance: Brightness/Log scale | Log scale は常時使える書きぶり | Brightness・Color scale・Log scale はすべて Gaussian 時のみ表示 (flowLayoutPanelGaussianOption) | FDS.cs:radioButtonPointSpread_CheckedChanged | 不正確 | 修正済 |
| SAED/PED | Appearance: Spot color | 「カラースケール不使用時のスポット色」 | Color scale = Gray のとき表示。General / Extinct (Screw or Glide) / Extinct (Lattice centring) の 3 色 | FDS.cs:comboBoxScaleColorScale_SelectedIndexChanged | 不正確 | 修正済 |
| SAED | Appearance: Radius | 単位・既定なし | "Spot radius" nm⁻¹, 0.01–1, 既定 0.2 | FDS.Designer.cs:numericBoxSpotRadius | 欠落 | 修正済 |
| SAED | Spot labels: d | 「面間隔 d」 | Å 単位で表示（他は nm） | FDS.cs:DrawDiffractionSpotsLabel | 不正確 | 修正済 |
| SAED | Spot labels: Distance | 「検出器上のスポット間距離」 | ダイレクトスポットからの距離 CL2·tan2θ (mm) | 同上 | 誤り | 修正済 |
| SAED | Spot labels: \|Fg\| | 「構造因子の絶対値 \|F\|」 | Kinematical: 相対強度 (%)。Dynamical: 相対強度 (%) + 出射面複素振幅 Ψ。Excitation only: 何も出ない | 同上 1292-1298 | 誤り | 修正済 |
| ja SAED/PED | Appearance | ja ラベル「結晶の色を使う」「カラースケール」「スポットの色」「Radius」「Brightness」「Log スケール」 | ja.resx は「結晶毎の指定色」「カラー」「描画色」「半径」「明るさ」「ログスケール」「球の断面 / ガウス関数」 | FDS.ja.resx | ja-en不一致 (ja ラベル) | 修正済 |
| ja index | File menu | 「保存」「コピー」等の日本語名 | メニュー項目は ja.resx に訳が無く日本語 UI でも英語表記 | FDS.ja.resx | ja-en不一致 (ja ラベル) | 修正済（英語表記に） |
| PED | Precession settings | 既定値なし | Semi-angle 0.1–500 mrad 既定 50（推奨欄 10–40 より大）、Step 2–1080 既定 36 | FDS.Designer.cs:2323-2346 | 欠落 | 修正済 |
| CBED | Input parameters 全般 | 既定値なし / "Convergence semi-angle", "No. of Bloch waves", "Thread count", "Draw disk outlines" | Semiangle 既定 6 mrad (0–2000)、Disk resolution 0.1 mrad/px、Number of diffracted waves 64 (1–2048)、Thickness 20–200 step 20 nm、Threads 1–128、"Draw guide circles" 既定オン（黄色円、LACBED は 000 以外十字） | FormDiffractionSimulatorCBED.Designer.cs, FDS.cs:DrawDiffractionSpots | 欠落/不正確 | 修正済 |
| CBED | Solver | Auto / Eigenproblem (MKL) / Eigenproblem (Eigen) / **Managed** | Auto / Eigenproblem with Eigen lib. / with MKL lib. / Matrix exp. with Eigen lib. / with MKL lib.。Managed は選択肢に無い。Auto=Eigen 有→MtxExp_Eigen 全コア、無→Eigen_MKL。不等間隔厚みは EVD へ。ARM64 は MKL 非表示 | FormDiffractionSimulatorCBED.cs:コンストラクタ, buttonSimulate_Click; BetheMethod.cs:cbed_DoWork 482-495 | 誤り | 修正済 |
| CBED | Run | "Start" | ボタンは "Simulate"（ja 開始） | FormDiffractionSimulatorCBED.resx | 不正確 (en) | 修正済 |
| CBED | Output: Brightness adjustment "Per disk" | 「選択した単一ディスクをフル解像度で表示」 | 各ディスクを自身の最大値で正規化するだけ（全ディスク表示のまま）。γ は 0 にリセットされ無効化 | FormDiffractionSimulatorCBED.cs:generateImage, radioButtonAllDisks_CheckedChanged | 誤り | 修正済 |
| CBED | Output: Scale / Color | "Scale"、Color「Gray など」 | en ラベルは "Gradient"（ja スケール）、Color は Gray/Cold-Warm/Spectrum/Fire | FormDiffractionSimulatorCBED.resx | 不正確 | 修正済 |
| CBED | Output: Brightness Max/Min | 上限下限 | 最大値に対する比、スライダー値の 4 乗 | generateImage | 欠落 | 修正済 |
| CBED | 冒頭 | 本体側の変化記載なし | CBED 時は本体の Bloch / Appearance パネル無効、File に Save/Copy CBED disks 出現 | FDS.cs:radioButtonBeamParallel_CheckedChanged, FormDiffractionSimulatorCBED.cs:…_VisibleChanged | 欠落 | 修正済 |

### マニュアルに無い機能

| フォーム | 機能/コントロール | 概要 | 根拠 | 対応 |
|---|---|---|---|---|
| FDS | Exclude all prohibited reflections / Exclude reflections prohibited by lattice centring / Draw all spots with the same radius | 「励起誤差のみ」時だけ表示される消滅則・同一半径オプション | FDS.cs:radioButtonIntensityCalculationMethod_CheckedChanged, CheckBoxExtinctionAll_CheckedChanged | 追記済 (SAED) |
| FDS | Options / Help メニュー | Dynamic Compression, ALCHEMI simulator..., Help > Basic concept of Bethe's method (付録 a3 を開く) | FDS.cs:basicConceptOfBethesMethodToolStripMenuItem_Click | 追記済 (index) |
| FDS | Save Movie / Print 系 / Save・Copy CBED disks | ファイルメニュー項目 | FDS.Designer.cs:fileToolStripMenuItem | 追記済 |
| FDS | 逆空間 3D 表示の Options | 色・3D スポット半径・相対強度しきい値・透明化・視点方向ボタン | FDS.Designer.cs:groupBoxOptions, FDS.cs:Draw3D | 追記済（概説のみ） |
| FDS | 解像度の単位切替 mm/px ↔ nm⁻¹/px | — | FDS.cs:radioButtonResoUnit_CheckedChanged | 追記済 |
| FDS | Anomalous dispersion (f', f'') | X線時のみ表示 (既定オン) | FDS.cs:WaveLengthControl_WaveSourceChanged, checkBoxAnomalousDispersion_CheckedChanged | 未対応（X線ページ担当範囲） |
| FDS | メインウィンドウへの .prm/画像ドロップ | 検出器ジオメトリ側へ転送される | FDS.cs:FormDiffractionSimulator_DragDrop | 未対応 |
| FDS | General タブの垂線の足/ダイレクト位置マーカー表示 ON/OFF | checkBoxShowFootPosition / checkBoxShowDirectPosition | FDS.Designer.cs:733,743 | 追記済（一言） |
| FDS | Developer code グループ | ja「開発用 (未使用)」 | FDS.Designer.cs:groupBoxDeveloperCode | 未対応（記載不要と判断） |
| Holder | Link to Current Direction / TEM-specific settings (Tilt X 方向 既定 −29°, Tilt Y 極性) / ↻ rotate 180° / Stereonet properties (描画範囲 30°, 1°線, 指数範囲, 等価指数) | — | FormDiffractionSimulatorHolder.cs/.Designer.cs | 一部追記済（Stereonet properties は未対応） |
| Geometry | Schematic diagram, Rot 90°, Scale1/Scale2/Gradient, Max/Min int., .prm 読込内容 | — | FormDiffractionSimulatorGeometry.* | 追記済 |
| DynamicCompression | 2018/2019 model, Us/Ur, EOS, Shocked/Slip plane, 回転分布, Increment Ω, Save patterns 等 | 個々のパラメータの意味は未記載 | FormDiffractionSimulatorDynamicCompression.Designer.cs | 一部追記済（列挙のみ） |
| CBED | Matrix exp. ソルバ (v4.794) | — | FormDiffractionSimulatorCBED.resx comboBoxSolver.Items3/4 | 追記済 |

### 要確認事項

1. CBED「物理的背景」の「HOLZ 線は **c 軸方向**の格子定数に敏感」は一般には「ビーム（晶帯軸）方向」の誤りでは？（コードでは確認できないため未修正）
2. ステータスバー右ダブルクリックのコピー文字列で、Intensity calculation が Dynamical 時も "Kinematical" と出る（FDS.cs:statusStrip1_MouseDown の三項演算子）。コード側のバグか。
3. CBED「for individual disks」時の Min 値が `SrcValuesGray.Max() * maxOverall * minRatio` と maxOverall を余分に掛けている（FormDiffractionSimulatorCBED.cs:generateImage）。意図通りか。
4. CBED の Solver "Auto" は GUI から BetheMethod.Solver.Auto として渡されず、文字列判定で MtxExp_Eigen（Threads 値付き）になる（buttonSimulate_Click）。結果は Auto と同等だが、Eigen 無効時の扱いも含め意図確認。マニュアルは BetheMethod 側の Auto 規則で記述した。
5. FDS.cs:FixedCenter のコメントが index 0 = "direct spot"、1 = "foot" となっているが、UI 表示 (Items: perpendicular foot / direct spot) と計算は逆（UI 側が正しい）。コメントのみの問題か。
6. Dynamic compression は X線 (XFEL) 専用の想定か。マニュアルでの位置づけ（index に置くか X線ページに移すか）と、各パラメータ (a, b, ω, σ_ω, Front/Back の物理的意味) の説明が必要か。
7. Kinematical の |Fg| ラベルの相対強度 (%) の規格化基準（000 を含むか）。Dynamical は 000 除外の最大で規格化（FDS.cs:1073）と記載した。
8. PED 半頂角の既定 50 mrad がマニュアルの推奨 10–40 mrad を外れている。推奨値か既定値のどちらを直すべきか。
9. index 冒頭の "Spot property" 等、UI グループ名 (Simulation settings / Display settings / Detector geometry & overlay image / Wave source / Optics) と節見出しの不一致を見出しごと直すか（アンカーが他ページから参照されている可能性があるため未変更）。

### 変更したファイル

- docs/src/en/7-diffraction-simulator/index.md
- docs/src/ja/7-diffraction-simulator/index.md
- docs/src/en/7-diffraction-simulator/1-saed-simulation.md
- docs/src/ja/7-diffraction-simulator/1-saed-simulation.md
- docs/src/en/7-diffraction-simulator/2-ped-simulation.md
- docs/src/ja/7-diffraction-simulator/2-ped-simulation.md
- docs/src/en/7-diffraction-simulator/3-cbed-simulation.md
- docs/src/ja/7-diffraction-simulator/3-cbed-simulation.md

（改行コードは元の CRLF を維持。コード・resx・他言語ページ・担当外ページは無変更。）


## E-alchemi-xrd-trajectory 監査報告 (261105)

担当ページ（注: 指定の `7-diffraction-simulator/8-electron-trajectory.md` は存在せず、実体は `docs/src/{en,ja}/8-electron-trajectory.md`。こちらを対象にした）
- docs/src/{en,ja}/7-diffraction-simulator/4-alchemi-simulation.md
- docs/src/{en,ja}/7-diffraction-simulator/5-x-ray-neutron-diffraction.md
- docs/src/{en,ja}/8-electron-trajectory.md

### 食い違い

| ページ | 節 | マニュアルの記述 | コードの実態 | 根拠 (file:method/行) | 分類 | 対応 |
|---|---|---|---|---|---|---|
| 5-x-ray | 特性X線 表 | Mo Kα₁ 0.7107 Å | 0.70932 Å（0.7107 は Kα 加重平均。表のエネルギー 17.479 keV は Kα₁ と整合） | Crystallography/Atom/AtomStatic.cs:CharacteristicXrayWavelength | 誤り | 修正済 (en/ja) |
| 5-x-ray | 特性X線 表 | Cr Kα₁ 2.2910 Å | 2.28973 Å | 同上 | 誤り | 修正済 (en/ja) |
| 5-x-ray | Back-Laue | 反射の幾何は Detector geometry の Tau/Phi で与える | Back-Laue のスポットは CameraLength2 と cos2θ で直接投影（ビーム垂直の平面検出器）。DetectorRotation(Tau/Phi) は投影に使われない | FormDiffractionSimulator.cs:Draw の `#region BackLaue`, ConvertReciprocalToDetector(BackLaue→(0,0)) | 誤り | 修正済 (en/ja)。index.md:295 にも同趣旨の記述あり（担当外・未修正） |
| 5-x-ray | スポットの外観 | Solid sphere: 円の面積が回折強度に対応 | 運動学では球半径 = R·I_rel^(1/3)（球の体積∝強度）、断面半径 √(r²−s²)。励起誤差のみでは半径 R 固定 | FormDiffractionSimulator.cs:Draw「円で塗りつぶすとき」 | 不正確 | 修正済 (en/ja) |
| 5-x-ray | スポットの外観 | Color scale は Gray scale / Cold-warm の2択 | Gray / Cold-Warm / Spectrum / Fire の4択 | FormDiffractionSimulator.Designer.cs:comboBoxScaleColorScale.Items, .resx | 古い | 修正済 (en/ja)。1-saed-simulation.md:82 も2択のまま（担当外） |
| 5-x-ray | スポットの外観 | Log scale: 対数表示（条件なし）/ Spot color: カラースケール非適用時の既定色 | Log scale と Color scale は Gaussian 時のみ（flowLayoutPanelGaussianOption）。Spot color 欄は Color scale=Gray のときのみ表示 | FormDiffractionSimulator.cs:radioButtonPointSpread_CheckedChanged, comboBoxScaleColorScale_SelectedIndexChanged | 不正確 | 修正済 (en/ja) |
| 5-x-ray | 中性子 | 波長 (nm) を入力 | 波長は Å/nm 切替（既定 Å） | Crystallography.Controls/Wave/WaveLengthControl.cs:LengthUnit, radioButtonUnitAngstrom_CheckedChanged | 不正確 | 修正済 (en/ja) |
| 5-x-ray (ja) | スポットの外観 | Solid sphere / Gaussian / Radius / Brightness / カラースケール / Log スケール / スポットの色 / 結晶の色を使う | ja UI: 球の断面 / ガウス関数 / 半径 / 明るさ / カラー / ログスケール / 描画色 / 結晶毎の指定色 | FormDiffractionSimulator.ja.resx | ja-en不一致(ラベル) | 修正済 (ja) |
| 5-x-ray (ja) | デバイリング | 回折強度を無視 / 指数ラベルを表示 | 強度を無視する / 指数を表示する | FormDiffractionSimulator.ja.resx:checkBoxDebyeRingIgnoreIntensity / checkBoxDebyeRingLabel | ja ラベル不一致 | 修正済 (ja) |
| 8-trajectory | 冒頭・ヒストグラム | これらの分布（ヒストグラム）が EBSD シミュレータの重み付けに使われる | FormEBSD は MonteCarlo を独自に（10M 電子）実行。FormTrajectory の結果はどこからも参照されない | FormEBSD.cs:590,3004 `new MonteCarlo(`、FormTrajectory 参照は FormMain/GuiCapture のみ | 誤り | 修正済 (en/ja) |
| 8-trajectory | 3D 表示 | 赤 = 吸収電子、橙 = 後方散乱電子 | BSE は赤（エネルギー損失で16段階に白・透明へ）、吸収電子は青。橙は「脱出後の経路」線分（長さ∝脱出エネルギー比） | FormTrajectory.cs:Draw3D (Material Color4) | 誤り | 修正済 (en/ja) |
| 8-trajectory | 脱出時エネルギー分布 | 脱出時に持っているエネルギー（エネルギー損失ではない） | ビンに入れているのは `energy - e[^1].e` = E0−E_escape（損失）。範囲 0〜E0−2 keV、ビン 0.1 keV | FormTrajectory.cs:DrawStatistics | 誤り（またはコードのバグ） | 修正済（実装通りに記述し⚠要確認を明記） |
| 8-trajectory | ヒストグラム | 面積1に規格化 | `Count / count`（BSE 総数で割るだけ、ビン幅で割らない＝ビン値の総和が1） | FormTrajectory.cs:DrawStatistics | 不正確 | 修正済 (en/ja) |
| 8-trajectory | 統計情報 | 各値の隣にモデル名 | モデル名が付くのは散乱断面積と阻止能のみ（平均自由行程・η・平均エネルギーには無し） | FormTrajectory.cs:CalcMonteCarlo | 不正確 | 修正済 (en/ja) |
| 4-alchemi | 全体 | — | UI ラベル・既定値・範囲（Range 0.01–60 / 8 mrad、Points 3–1001 / 101、Thickness 10–100 step 10、Max beams 1–1600 / 120、FWHM 0.01–20 / 1、正規化3択、X軸2択、Bragg 既定 ON、進捗5段、診断行、U<1.2 注意、線の系列既定未チェック）はすべてコードと一致。ja 版もラベル（Localization.Loc の ja）と一致 | FormALCHEMI.cs / .Designer.cs, IonizationChannel.cs:LowOvervoltage | — | 食い違いなし |

### マニュアルに無い機能

| フォーム | 機能/コントロール | 概要 | 根拠 | 対応 |
|---|---|---|---|---|
| FormDiffractionSimulator | Anomalous dispersion (f', f'') チェック | X線のみ表示・既定ON。構造因子とデバイリングに f'/f'' を反映。Back-Laue では無効。ja UI も英語表記 | FormDiffractionSimulator.cs:SetVector, WaveLengthControl_WaveSourceChanged; .resx | 追記済 (en/ja) |
| FormDiffractionSimulator | Exclude all prohibited reflections / Exclude reflections prohibited by lattice centring | 「励起誤差のみ」時のみ表示。既定は格子由来のみ非表示。非表示にしない禁制反射は別色 | FormDiffractionSimulator.cs:radioButtonIntensityCalculationMethod_CheckedChanged, SetVector | 追記済 (en/ja) |
| FormDiffractionSimulator | Draw all spots with the same radius | 「励起誤差のみ」時のみ。全スポットを S_g=0 の大きさで描画 | 同上, Draw | 追記済 (en/ja) |
| FormDiffractionSimulator | Back-Laue の仕様 | 50 keV 打切り（d_min=λ(50keV)/2）、高次反射 n·g の |F|² を最低次へ合算、最強で規格化、異常分散無効、Radius 単位なし | FormDiffractionSimulator.cs:SetVector, Draw(BackLaue) | 追記済 (en/ja) |
| FormDiffractionSimulator | Gaussian の描画カットオフ | |S_g| ≥ 3R は描かない | Draw | 追記済 (en/ja) |
| FormTrajectory | BSE の定義（2 keV しきい値） | 2 keV まで追跡、2 keV 超で脱出したものを BSE として統計 | FormTrajectory.cs:EnergyThreshold, MonteCarlo(thresholdKev) | 追記済 (en/ja) |
| FormTrajectory | 数値範囲・既定値 | 総数 100–1,000,000 / 100,000、傾き −90–0 / 0、描画数 1–100,000 / 500（計算順の先頭から） | FormTrajectory.Designer.cs, Draw3D | 追記済 (en/ja) |
| FormALCHEMI | Points のスピン刻み 2（奇数維持で θ=0 標本を保つ）、走査原点 = 回折シミュレータの現在方位 | ツールチップにはあるが本文に無い（軽微） | FormALCHEMI.Designer.cs:numericBoxPoints.UpDown_Increment, rowTip | 未対応 |
| FormDiffractionSimulator | Spot Details ボタン、X線プリセットメニュー | index.md 側の範疇 | — | 未対応（担当外） |

### 要確認事項

1. **FormTrajectory の脱出エネルギー分布**: `energies = BSEs.Select(e => energy - e[^1].e)` でエネルギー損失をヒストグラム化しているが、グループ名は「BSE energy distribution at escape／試料脱出時のエネルギー」。旧マニュアルは「損失ではない」と明記していた。コードのバグ（`e[^1].e` にすべき）か、ラベル・マニュアルの誤りか。現在はマニュアルを実装どおりに書き⚠を付けた。コード修正ならこの段落を戻す必要あり。
2. **Back-Laue と Tau/Phi**: 投影に DetectorRotation を使っていない（ビーム垂直の検出器固定）。意図した仕様か。index.md:295「Back Laue のジオメトリはここで設定」も要修正の可能性（担当外）。
3. **ALCHEMI と線源**: `Voltage => waveLengthControl.Energy` は線源を見ていない。X線（keV）や中性子（meV）のまま ALCHEMI を開くと、その数値を電子の kV として扱う。メニューも線源で無効化されない。ガードを入れるか、マニュアルの「GUI 条件: 電子線」を必須条件として強調するか。
4. **阻止能の符号**: マニュアルは「(eV/nm, 負値)」としているが、`StoppingPower * 1000` の符号は MonteCarlo.GetParameters 依存で未確認。
5. **8-electron-trajectory のパス**: 担当指定の `7-diffraction-simulator/8-electron-trajectory.md` は存在しない。実体は `docs/src/{en,ja}/8-electron-trajectory.md`（mkdocs.yml nav も同じ）なので、それを対象にした。
6. 「Kinematical & exc. error」では消滅則チェックボックスが非表示になるが、Flag1 の状態は保持される（禁制反射は F=0 なので実害なし）。仕様として問題ないかどうか。

### 変更したファイル

- docs/src/en/7-diffraction-simulator/5-x-ray-neutron-diffraction.md
- docs/src/ja/7-diffraction-simulator/5-x-ray-neutron-diffraction.md
- docs/src/en/8-electron-trajectory.md
- docs/src/ja/8-electron-trajectory.md
（4-alchemi-simulation.md は食い違いなしのため無変更。いずれも CRLF を維持）


## F-hrtem-stem 監査報告（HRTEM/STEM シミュレータ）

対象: docs/src/{en,ja}/9-hrtem-stem-simulator/{index,1-hrtem-simulation,2-stem-simulation,3-potential-simulation}.md
主な照合先: ReciPro/ImageSimulator/FormImageSimulator.{cs,Designer.cs,resx,ja.resx}, FormCTF.cs, FormPresets.cs, Crystallography/Diffraction/BetheMethod.cs (GetHRTEMImage, RunSTEM, ExtractInsideBeams), Crystallography/Images/ImageProcessor.cs (GaussianBlurFast)

### 食い違い

| ページ | 節 | マニュアルの記述 | コードの実態 | 根拠 (file:method/行) | 分類 | 対応 |
|---|---|---|---|---|---|---|
| index | Display > Label | 厚さ・デフォーカス・**指数**などのラベル | ラベルは HRTEM/STEM が `t=…\r\nf=…`、Potential が "Ug magnitude" 等のペイン名のみ。指数は出ない | FormImageSimulator.cs:BuildPseudoGrid L1338, simulatePotential L1143 | 誤り | 修正済 |
| index | File menu | Load/Save TEM parameters でファイル保存・復元 | 両項目とも resx で Enabled=False、Click ハンドラ無し（未実装） | FormImageSimulator.resx: loadTEMParameterToolStripMenuItem.Enabled / saveTEMParametersToolStripMenuItem.Enabled | 誤り | 修正済 |
| index | File menu | TIFF も他形式と同列、Overprint は保存画像のみ | TIFF は常にペインごと 1 ファイル・表示レンジでなく生値 (SrcValuesGray)・記号なし。Overprint は Copy にも効く。Save individually / Overprint は既定 ON | FormImageSimulator.cs:Save L1919-1965, Designer L2322/2348 | 不正確 | 修正済 |
| index | Help menu | Calculation library で計算ライブラリを選択 | Native フラグは GetHRTEMImage にしか渡らない（STEM は自動選択、Potential は無関係）。Load で毎回 index 0 に戻り、設定保存もされない | FormImageSimulator.cs:SimulateHRTEM L1094, FormImageSimulator_Load L659, FormPresets.cs:ImageSimulatorSetting | 不正確 | 修正済 |
| index (ja) | 強度の規格化 | 「画像ごとに個別に規格化」 | ja.resx は「画像ごとに規格化」 | FormImageSimulator.ja.resx:checkBoxNormarizeIndividually.Text | ja-en不一致(ラベル) | 修正済 |
| 1-hrtem | TEM conditions 右クリック | 条件プリセット(ARM300F, 2100F…)は左下の **Preset settings** から | 4 つの顕微鏡プリセットは TEM conditions の右クリックメニュー (contextMenuStripTEMcondition) にある。Preset settings は利用者プリセット (FormPresets) | FormImageSimulator.Designer.cs L1952, FormImageSimulator.cs:presets1〜4ToolStripMenuItem_Click | 誤り | 修正済 |
| 1-hrtem | 同上 | Set all aberrations to zero | Cs, Cc, β, ΔV に加え **デフォーカスも 0** にする | FormImageSimulator.cs:zeroAllToolStripMenuItem_Click L2107 | 不正確 | 修正済 |
| 1-hrtem | Objective aperture | 絞りで切る波数でブロッホ波計算に含めるスポット数も変わる | ブロッホ計算は常に BlochNum 本。絞りは結像に使うビームを後で選ぶだけ（判定 2sin(r/2)/λ）。2 本未満なら警告して中止 | FormImageSimulator.cs:SimulateHRTEM L1070-1079, BetheMethod.cs:GetHRTEMImage L3723-3724, ExtractInsideBeams L4572 | 誤り | 修正済 |
| 1-hrtem | Objective aperture | Shift X/Y は暗視野・**傾斜結像**に使う | 入射ベクトル k=(0,0) 固定で照射は傾かない。絞り中心をずらすだけ | BetheMethod.cs:GetHRTEMImage L3715 | 不正確 | 修正済 |
| 1-hrtem | HRTEM options | 「全入射方向からの寄与を統合する干渉モデル」 | 出射波は 1 方向のみ計算。β・δ はエンベロープ関数としてのみ入る | BetheMethod.cs:GetHRTEMImage | 誤り | 修正済 |
| 1-hrtem | HRTEM options | Linear image は WPOA 前提で PCTF にエンベロープを掛ける | 動力学出射波の各回折波に exp(-iχ)·Es·Ec を掛けて |Σ|²（準コヒーレント） | BetheMethod.cs:GetHRTEMImage L3726-3735 | 不正確 | 修正済 |
| 1-hrtem | Image properties | Resolution を小さくすると FFT 時間が比例して増える | FFT は使わず画素ごとの直接フーリエ和。時間は画素数×ビーム数で決まり Resolution には依存しない（視野 = Size×Resolution が狭まるだけ） | BetheMethod.cs:GetHRTEMImage L3780-3828 | 誤り | 修正済 |
| 1-hrtem / 2-stem / 3-potential (en) | Waves 節 | 見出し "Waves" | UI は "Diffracted waves"（ja は「波の数」で一致） | FormImageSimulator.resx:groupBoxDiffractedWaves.Text | 不正確(ラベル) | 修正済 (en) |
| 1-hrtem (ja), 2-stem (en/ja) | TEM conditions | "ΔV (FWHM)" | ラベルは "ΔV"（FWHM は Tooltip のみ） | FormImageSimulator.resx:numericBoxDeltaV.HeaderText | 不正確(ラベル) | 修正済 |
| 1-hrtem | CTF | sinχ, Es, Ec のみ記載。u 上限変更 | cosχ, PCTF, ACTF のチェック、Max u (既定 6 nm⁻¹, 0–20)、Copy (タブ区切り)、PCTF/ACTF は絞り外 0、Potential では無効 | FormCTF.cs:Renew, ButtonCopyGraph_Click, FormCTF.Designer.cs | 不正確(欠落) | 修正済(追記) |
| 2-stem | Image property / Computational cost | Size = 走査点数で計算時間に線形 | imageSize は I(Q) 計算 (Stage1–4) 後の実空間合成でのみ使用。Q リストは画像サイズと無関係 | BetheMethod.cs:RunSTEM L2755-2759, L3277-3311 | 誤り | 修正済 (2 箇所) |
| 2-stem | STEM options (simulation) | "Slice thickness for inelastic" / ja「非弾性用スライス厚」 | "Slice thickness (TDS)" / 「スライス厚 (TDS)」 | FormImageSimulator(.ja).resx:numericBoxSTEM_SliceThicknessForInelastic.HeaderText | 不正確(ラベル) | 修正済 |
| 2-stem | STEM options (optical) | "(Annular) detector inner/outer angle" / ja「(環状)検出器の内角/外角」 | グループ "Annular detector"/「環状検出器」内の "Inner radius"/"Outer radius"・「内径」「外径」(単位 mrad) | FormImageSimulator(.ja).resx:numericBoxSTEM_Detector*Angle.HeaderText, groupBox1.Text | 不正確(ラベル) | 修正済 |
| 2-stem | STEM options (optical) | σs: 単位・既定値「—」 | 単位 pm、既定 20 pm。計算後に全像へガウス畳み込み | Designer L1739, resx FooterText, BetheMethod.cs:RunSTEM L3371-3376, 3561-3564 | 不正確 | 修正済 |
| 2-stem | STEM-EDX | プローブ格子は EDX 欄の「次の行」 | labelEdxProbeGrid は STEM options (simulation) の Angular resolution 直下にあり、EDX の有無に関係なく常時表示。推奨値は EDX ON 時のみ | Designer L1333-1334, FormImageSimulator.cs:RenewProbeGridLabel | 不正確 | 修正済 |
| 2-stem | STEM options (optical) | 検出器角の適用範囲の記載なし | 検出器角は参照像専用で STEM-EDX には使われない (Tooltip に明記) | FormImageSimulator.resx:numericBoxSTEM_DetectorInnerAngle.ToolTip | 欠落 | 修正済(追記) |
| 1-hrtem / 2-stem | 既定値列 | Cs/Cc/β/ΔV/α/検出器/角度分解能/スライス厚の既定値なし | Cs 1.0 mm (±20), Cc 1.4, β 0, ΔV 0.8 eV, 絞り 12 mrad, α 25, 内径 0, 外径 20, Δα 0.4 mrad, スライス 1 nm（初回のみ。以後レジストリ復元） | FormImageSimulator.Designer.cs 各 numericBox, FormMain.cs L742 | 不正確 | 修正済(追記) |

### マニュアルに無い機能

| フォーム | 機能/コントロール | 概要 | 根拠 | 対応 |
|---|---|---|---|---|
| FormPresets | Preset list ウィンドウ全体 | Name / Add / Replace / Rename / Delete / Manage the preset list / OK / Cancel(Esc で開いた時点へ復元)。クリックで即適用。保存項目一覧。再起動後も保持 | FormPresets.cs 全体, FormMain.cs L743 | 追記済 (index #preset-list) |
| FormImageSimulator | TEM conditions 右クリックの顕微鏡プリセット 4 種 | AccVol/Cs/Cc/ΔV を設定しデフォーカスを Scherzer に | presets1〜4ToolStripMenuItem_Click | 追記済 (1-hrtem) |
| FormImageSimulator | 符号規約 | χ(u)=πλΔf u²+½πCsλ³u⁴, exp(-iχ)、Δf<0 = アンダーフォーカス、負の Cs 入力可・Scherzer 表示は +√(4/3|Cs|λ)、ΔV(FWHM)→σ, δ=Ccσ/V | FormCTF.cs:Renew L71, BetheMethod.cs:GetHRTEMImage L3733, FormImageSimulator.cs:Scherzer/DeltaVol | 追記済 (1-hrtem, 2-stem に参照) |
| FormCTF | STEM モードの Coherent / Incoherent imaging | Coherent は Es=exp(-2π²σs²u²)、Incoherent は 2 絞り重なり積分 (192²) の伝達関数 | FormCTF.cs:Renew L57-148, SetControl | 追記済 (2-stem #ctf-stem) |
| FormCTF | cosχ / PCTF / ACTF / Max u / Copy | 上表参照 | FormCTF.cs | 追記済 (1-hrtem) |
| FormImageSimulator | EDX: Common（Normalization 内） | EDX 表示中かつ複数チャネル時のみ表示。EDX は Min/Max 変換を受けず下限 0 固定 | FillEdxGrid, RadioButtonHRTEM_CheckedChanged | 追記済 (index) |
| FormImageSimulator | 対物絞り内ビーム < 2 本の警告 | "Obj. Aper. size is too small…" で中止 | SimulateHRTEM L1074 | 追記済 |
| FormImageSimulator | シリーズの Num 上限 20・1 行/1 列時の折返し | ほぼ正方グリッドに折返し | Designer numericBox*Num.Maximum, BuildPseudoGrid L1347 | 追記済 |
| FormImageSimulator | 単位胞の a/b/c 色 (赤/緑/青) | 画像中心基準 | drawSymbols | 追記済 |
| FormImageSimulator | STEM 進捗表示 (Stage 1–5、経過・残り時間、完了時ステージ別時間) | ステータスバー | stemProgressChanged / StemCompleted | 未対応 |
| FormImageSimulator | EDX 表示時のマウス値 "(model)" 表記 | 生成空孔量であることの注記 | FormImageSimulator_MouseMove2 | 未対応 |
| FormImageSimulator | 絞り開放時の nm⁻¹ 欄に "∞" 表示、Real-Time Simulation は Potential でも有効 | 細部 | NumericBoxObjAperRadius_ValueChanged, RotationChanged | 未対応 |

### 要確認事項

1. **コード不具合の疑い (HRTEM_ObjAperRadius setter)**: `else { checkBoxOpenAperture.Checked = true; numericBoxObjAperRadius.Value = value*1000; }` で、有限サイズの絞りを適用しても「絞り開放」がオンになる。プリセット適用・再起動（Setting 復元）のたびに絞りが開放される。`false` の誤記では？（FormImageSimulator.cs L184-197）
2. **ガウスぼかし幅が 2 倍の疑い**: `ImageProcess.GaussianBlurFast` の引数は HWHM（カーネル exp(-x² ln2/hwhm²)）だが、(a) 表示用 Gaussian Blur (FWHM) は `FWHM[pm]/Resolution[pm]`、(b) STEM の Effective source size は `STEM_SourceSizeFWHM/resolution` をそのまま渡している → 実際の FWHM は入力値の 2 倍。CTF の Es(STEM) は σ=FWHM/2.355 で計算しており像と不整合。（FormImageSimulator.cs:CheckBoxGaussianBlur_CheckedChanged L2093, BetheMethod.cs:RunSTEM L3375-3376/3564, ImageProcessor.cs L102-122）マニュアルはラベル通り FWHM のまま。
3. numericBoxSTEM_EffectiveSourceSize の Tooltip が「(nm)」だが Footer は pm。
4. Potential の振幅/位相モードで U'g の位相ペインのラベルが "Ug phase"（タプル (1,1,"Ug phase")）。"U'g phase" の誤記では？（simulatePotential L1143）
5. Calculation library が保存されない（ImageSimulatorSetting.Native は代入されず、Load で毎回 Native に戻る）のは意図どおりか。マニュアルにはそう書いた。
6. Load/Save TEM parameters は無効のまま残す予定か（削除 or 実装）。マニュアルは「無効（未実装）」と記載。
7. Cs の DecimalPlaces=2 のため、既存の典型値「< 0.01 mm（Cs 補正）」が入力できるか（NumericBox の丸め挙動）。
8. 既定の検出器が Inner 0 / Outer 20 mrad（α=25 より内側の BF 的設定）なのは意図どおりか。
9. Linear image（準コヒーレント）の説明文「透過波との干渉が支配的な場合（薄い試料）に正確」は Claude の表現。著者の意図する説明に合わせて要査読。
10. ja index の見出し「結果の表示・調整（左側パネル）」は明示 id が無く、他ページから `#結果の表示調整左側パネル` で参照されている（en は {#display-settings}）。pymdownx slugify で一致するか、ja にも {#display-settings} を付けて参照を揃えるか。
11. FormCTF / FormPresets は .ja.resx に Text が無く UI が英語のみ（ja マニュアルにその旨追記）。ローカライズ予定は？
12. StemCompleted の NotSupportedException 用メッセージ（スライス数が多すぎる…）は、コメントで「経路一本化で slice 数制約は消えた」とあり、文言が古い可能性。
13. HRTEM quasi モードのコード中コメントで「Ec 時間的」「Es 空間的」のラベルが式と逆（β 項に Ec、δ 項に Es と注記）。式は正しくコメントのみの問題。（BetheMethod.cs L3734-3735）

### 変更したファイル

- docs/src/en/9-hrtem-stem-simulator/index.md
- docs/src/ja/9-hrtem-stem-simulator/index.md
- docs/src/en/9-hrtem-stem-simulator/1-hrtem-simulation.md
- docs/src/ja/9-hrtem-stem-simulator/1-hrtem-simulation.md
- docs/src/en/9-hrtem-stem-simulator/2-stem-simulation.md
- docs/src/ja/9-hrtem-stem-simulator/2-stem-simulation.md
- docs/src/en/9-hrtem-stem-simulator/3-potential-simulation.md

（ja/3-potential-simulation.md は変更なし。見出しに {#tem-conditions}（1-hrtem en/ja）、{#preset-list}（index en/ja）、{#ctf-stem}（2-stem en/ja）を新設。）


## G-spotid-ebsd 監査結果（10-spot-id / 11-spot-id-v2 / 12-ebsd-simulation）

### 食い違い

| ページ | 節 | マニュアルの記述 | コードの実態 | 根拠 (file:method/行) | 分類 | 対応 |
|---|---|---|---|---|---|---|
| 12 en/ja | Workflow 1 | MC で約 250 万本の電子を追跡 | 1000 万本（260921Cl で 250 万から変更） | FormEBSD.cs: `BackscatterMonteCarloLoopCount = 10_000_000` (l.67) | 古い | 修正済 |
| 12 en/ja | BSE Distribution / Depth | 最後の非弾性散乱の深さの分布 | **Source depth (MC)** で選んだモードの深さ（既定が最後の非弾性） | FormEBSD.cs l.2467 `GetMonteCarloSourceDepth(e)` | 古い | 修正済 |
| 12 en/ja | 冒頭・中央列タブ名 / 節見出し | "Output parameters of dynamical EBSD pattern" / 「出力パラメータ」 | タブ名 "Simulated pattern" / 「シミュレーション画像」 | FormEBSD.resx / .ja.resx: tabPageOutputParameter.Text | 古い | 修正済（見出しも変更。他ページからのアンカー参照なしを確認） |
| 12 en/ja | Simulated pattern 1 行目 | "Show image with BSE angular/energy distributions" / 「…考慮して画像を合成」 | "With BSE angular/energy distributions" / 「後方散乱電子のエネルギー・方位分布を考慮」、既定 ON | FormEBSD.resx: checkBoxWithBSEDistribution.Text; Designer l.766 | 不正確 | 修正済（既定 ON も追記） |
| 12 en/ja | Non-local source | 既定値の記載なし | 既定 ON（ver4.949 History で明記） | FormEBSD.Designer.cs l.670 checkBoxTDSBackground.Checked = true | 不正確 | 修正済 |
| 12 ja | SEM/EBSD のセッティング | **Sample tilt**・**Tilt**・**Width**・**Height**（英語のまま） | 試料傾斜・傾斜・幅・高さ | FormEBSD.ja.resx HeaderText | ja-en不一致(ja resx) | 修正済 |
| 12 ja | マスターパターン / シミュレーション画像 / EBSDパターン | **Energy / Depth**、**Resolution**・**Size (W×H)** | エネルギー/深さ、解像度・サイズ (W×H) | FormEBSD.ja.resx HeaderText | ja resx 不一致 | 修正済 |
| 12 en/ja | Shortcuts | ダブルクリック＝検出器セル選択のみ | 晶帯軸ピッキング ON 時は点の追加（Ctrl=削除, Shift=選択） | FormEBSD.cs: HandleZoneAxisDoubleClick | 古い | 修正済（表の行に追記） |
| 12 en/ja | Contrast falls with energy loss | 補正なしで帯が約 12 % 広い | ToolTip は「13 % wider」 | FormEBSD.resx: checkBoxCoherenceLoss.ToolTip | 不正確(どちらか) | 未修正（要確認 1） |
| 12 en/ja | Weight by phosphor response | "applies to the detector-plane binning" | MC 分布のビニングは 260921Cl で検出器面→射出半球 (Lambert 等積ディスク 18×18) に変更 | FormEBSD.cs l.3036 付近コメント / EbsdMonteCarloDistribution | 古い可能性 | 未修正（要確認 2） |
| 12 en/ja | Calibrate geometry | 方位と「交互に」最適化 | ToolTip は "together with the orientation" | FormEBSD.resx: buttonCalibrateGeometry.ToolTip | 不正確(どちらか) | 未修正（要確認 3） |
| 10 en/ja | 冒頭・Shortcuts | v1 が画像からスポットを検出・フィッティング・指数付けし、画像を参照表示 | FormSpotIDv1 には画像表示・スポット検出のコントロールが一切無く、旧 TEM ID の数値入力のみ | FormSpotIDv1.Designer.cs（PictureBox は模式図 3 枚のみ） | 誤り | 修正済（冒頭・Shortcuts 段落） |
| 10 en/ja | Main area / Image adjustments / Optics / Spot detection and fitting / Index（ja も同） | v1 の機能として記載 | すべて FormSpotIDV2 の機能（Dust&Scratch・Gaussian blur は ScalablePictureBoxAdvanced、Re-fit all・Refine thickness 等も V2） | FormSpotIDV2.Designer.cs l.235-238 ほか | 誤り | 未修正（HTML コメントで要確認フラグ。削除/移設は要確認 4） |
| 10 en | TEM ID 冒頭 | **Search zone axes** を押す | 写真ごとの **Find Zone Axis**、複数写真は **Search zone axes from two/three patterns**（実行時に Text を上書き） | FormSpotIDv1.cs: textBox_TextChanged | 不正確 | 修正済 |
| 10 en/ja | TEM condition | 「加速電圧・カメラ長など」のみ | Acc. Voltage (kV, 既定 200)、Camera Length (mm, 既定 1000)、Wavelength (nm) は自動計算・読み取り専用 | FormSpotIDv1.Designer.cs l.400-423; FormSpotIDv1.cs: numericBoxAccVol_ValueChanged | 不正確 | 修正済 |
| 11 en/ja | 冒頭 | v2 は v1 の強化版でスポット検出・フィッティングが改善 | v1 にはスポット検出が無い（別機能） | FormSpotIDv1.Designer.cs | 誤り | 修正済 |
| 11 en/ja | File menu | 「Spot ID v1 と同じドラッグ&ドロップ読み込み」 | v1 は画像を読まない。v2 単独で DragDrop 対応 | FormSpotIDV2.Designer.cs l.1317; FormSpotIDv1.Designer.cs | 誤り | 修正済 |
| 11 en | Ignore prohibited reflections | チェック時、らせん軸・映進面の禁制反射は「必ずしも満たされない」扱い（＝残す意味） | チェック時は全消滅則の反射を候補から除外、未チェック（既定）は格子心の消滅のみ除外。電子線のみ（X 線は常に全除外） | FormSpotIDV2.cs l.1126; PolyCrystal.cs: SetGVector(removeZeroIntensity) | 誤り / ja-en不一致 | 修正済（ja も明確化） |
| 11 en/ja | Acceptable error | 面間隔と角度の許容偏差 | d の相対許容 %（0.1–10, 既定 2）。角度許容は asin(tol)/2 から導出 | FormSpotIDV2.cs: ToleranceLength / ToleranceAngle | 不正確 | 修正済 |
| 11 en/ja | Improvements over v1 | 複数プロファイル形状、重なり・サテライト反射対応 等 | v1 に比較対象の機能が無い。該当実装も見当たらない | FormSpotIDV2.cs | 誤り(推定) | 未修正（コメントで要確認フラグ、要確認 5） |

### マニュアルに無い機能

| フォーム | 機能/コントロール | 概要 | 根拠 | 対応 |
|---|---|---|---|---|
| FormEBSD | 晶帯軸ピッキング（Zone axis / [u v w] / + geometry / Find / Clear） | 実測画像上で拾った晶帯軸から方位（と検出器中心）を探索。許容 2°→5° 再試行、最大 10 候補、+geometry は 4 点以上 | FormEBSD.cs: HandleZoneAxisDoubleClick, buttonFindZoneAxis_Click; ver4.949 History "zone-axis picking" | 追記済（en/ja 新節「Zone-axis picking / 晶帯軸のピッキング」、screenshot プレースホルダ付き） |
| FormEBSD | Energy filter E > | 射出エネルギーの理想ハイパスフィルター（keV, 0=OFF, 再ビニングのみ） | FormEBSD.resx: numericBoxEnergyFilter.ToolTip; FormEBSD.cs McEnergyFilterMinKeV | 追記済 |
| FormEBSD | Dev: PCHIP compression ボタン | 開発者用（Visible=False） | FormEBSD.resx: buttonFitNistElasticSampler.Visible | 未対応（不要） |
| FormEBSD | 既定値（Number of diffracted waves 32 / 1–2048、Kikuchi Top 40、1/d < 10 nm⁻¹、Det 400×400 px・0.1 mm/px・Tilt 90°・Z 30 mm） | 数値の既定値・範囲がマニュアルに無い | FormEBSD.Designer.cs | 未対応 |
| FormSpotIDv1 | 許容誤差・ホルダー条件 (Tilt1/2)・写真2/3・同一反射チェック・複数写真検索・結晶複数選択・1000 件上限 | TEM ID の主要機能 | FormSpotIDv1.cs: textBox_TextChanged, buttonSearch_Click, buttonSearchAll_Click; FormMain.cs: multiSelectionForms_VisibleChanged | 追記済 |
| FormSpotIDV2 | Save / Copy（スポット表）、Re-fit all、Show symbols / Show numbers | スポット表の操作 | FormSpotIDV2.Designer.cs / .resx | 追記済 |
| FormSpotIDV2 | Details of the fitting function | 2D 擬 Voigt 関数の説明図 (pictureBox1) を表示、開いた時点では OFF | FormSpotIDV2.cs: FormSpotID_Load, checkBoxDetailsOfFunction_CheckedChanged | 追記済 |
| FormSpotIDV2 | Show zone axis / Show symbol / Show label（計算スポット） | 指数付け結果の重畳 | FormSpotIDV2.Designer.cs groupBoxIndex | 追記済 |
| FormSpotIDV2 | Refine thickness & direction (only for SAED)、Diffracted waves (既定 400)、Search angle range (mrad, 既定 2)、Stop | 動力学計算による厚さ・方位の最適化 | FormSpotIDV2.Designer.cs l.938-960; .resx ToolTip | 追記済 |
| FormSpotIDV2 | Show Debye rings / Show guide circles | パターン上への円の描画 | FormSpotIDV2.Designer.cs panel1 | 追記済 |
| FormSpotIDV2 | File ▸ Save / Copy ▸ as Metafile (EMF) / as Image (PNG) | 表示画像の保存・コピー | FormSpotIDV2.resx | 追記済 |
| FormSpotIDV2 | Shortcut Hints メニュー | マウス操作の一覧メニュー | FormSpotIDV2.resx shortcutHintsToolStripMenuItem | 未対応 |
| FormSpotIDV2 | スポット表の列（Direct, Range, x_0, y_0, H1, H2, θ, η, A, B_0, B_x, B_y, R(%), d(nm), hkl, Fit） | 列の意味の説明が無い | FormSpotIDV2.resx HeaderText | 未対応 |

### 要確認事項

1. EBSD「Contrast falls with energy loss」: 補正なしのバンド幅の過大評価はマニュアルの「約 12 %」と ToolTip の「13 %」のどちらが正しいか。
2. EBSD「Weight by phosphor response」: 「detector-plane binning に適用」は、MC 分布のビニングを射出半球（Lambert 等積ディスク）に変えた 260921Cl 以降も正しい表現か。
3. EBSD「Calibrate geometry」: PC/DD と方位を「交互に」最適化（マニュアル）か「同時に」（ToolTip）か。
4. Spot ID v1 ページの「Main area / Image adjustments / Optics / Spot detection and fitting / Index」節は、FormSpotIDv1 に対応するコントロールが一つも無く Spot ID v2 の内容。削除するか、v2 ページに無い情報（Image adjustments 表、Spot detail window の説明、Refine thickness）を v2 へ移すか。現状は冒頭と Shortcuts のみ修正し、節の前に HTML コメントのフラグを置いた。スクリーンショット FormSpotIDv1.png も確認推奨。
5. Spot ID v2 ページの「Improvements over v1」節（複数プロファイル形状・重なり/サテライト反射対応など）は削除でよいか。
6. コード側の不具合候補（マニュアル外・未修正）: (a) FormSpotIDv1.cs textBox_TextChanged で photo3 を作るとき L3 に `inputBoxP2L3.Length`（写真 2 の l3）を渡している。(b) 同メソッドで buttonSearchAll.Text を英語固定文字列で上書きするため、ja resx の「三枚の写真から晶帯軸を検索」が表示されない（ja マニュアルは英語表記で記述した）。
7. EBSD: 21-shortcuts.md（担当外）にも EBSD のダブルクリック（晶帯軸ピッキング時の追加/Ctrl 削除/Shift 選択）の追記が必要か。
8. EBSD 晶帯軸ピッキングの候補表で、Score / Bands / Strong bands 列が晶帯軸探索ではどういう値になるか（Assigned/picked 数, rms 等）。追記節では列の意味まで書いていない。

### 変更したファイル

- docs/src/en/10-spot-id.md
- docs/src/ja/10-spot-id.md
- docs/src/en/11-spot-id-v2.md
- docs/src/ja/11-spot-id-v2.md
- docs/src/en/12-ebsd-simulation.md
- docs/src/ja/12-ebsd-simulation.md


## H-macro 監査結果（docs/src/{en,ja}/20-macro/*）

対象コード: ReciPro/Macro/Macro.cs, ReciPro/Macro/MacroPipeListener.cs, ReciPro/FormMain.cs（CLI /m /o /x・Macro メニュー）, ReciPro/Program.cs, Crystallography.Controls/Macro/FormMacro.*（読むだけ）。

### 組み込み関数の機械的突き合わせ

スクリプト: `docs/review/tools/macro_api_extract.py`（実行: `python3 docs/review/tools/macro_api_extract.py ReciPro/Macro/Macro.cs docs/src/en/20-macro/1-built-in-functions.md`）（Macro.cs の public クラス/メンバー・[Help] 属性・引数・戻り値・get/set を抽出し、継承（ImageSimulationClass → STEMandHRTEM_class → STEM/HRTEM、ImageSimulationClass → Potential）を展開してから en 版 1-built-in-functions.md と照合）。
出力: 上記スクリプトの標準出力（リポジトリには含めていない）（公開メンバー 214 件。正規表現で拾えなかった複数行の宣言 ImageResolutionInMM/InNMinv・SpaceGroupName・Sleep は手で確認済み）

- **実装にあってマニュアルに無いメンバー: 0 件**。`SerialImage{Thickness,Defocus}{Step,Num}` は `/ Step / Num` の略記で載っている。
- **マニュアルにあって実装に無いメンバー: 0 件**。
- **[Help] 属性が無い公開メンバー: 0 件**。ただし `ReciPro.Sleep` は [Help] があるのに、`Macro` コンストラクタが `addHelp` を子クラスにしか呼ばないため、エディタの関数表と補完候補に出てこない（要確認 1）。
- **シグネチャ**: 引数名・既定値はすべて一致した（Crystal.SetCellInAng の b..gamma=NaN、AddAtom の occ=1・bIso=0、SetSpaceGroupByNumber の setting=""、Export3DModel の各既定値 80/0/true/true/true/false/2.0/true/2.4/1.2）。違いがあったのは単位と意味の記述だけで、内容は下の表のとおり。戻り値の記載漏れは Export3DModel（string）だけだった。

### 食い違い

| ページ | 節 | マニュアルの記述 | コードの実態 | 根拠 (file:method/行) | 分類 | 対応 |
|---|---|---|---|---|---|---|
| 1-built-in-functions (en/ja) | DifSim / Properties | `Wavelength` は Å | 単位は nm（[Help] も "in nm"。WaveLengthControl.WaveLength は常に nm） | Macro.cs:DifSimClass.Wavelength (L1049), Crystallography.Controls/Wave/WaveLengthControl.cs:WaveLength (L185) | 誤り | 修正済 |
| 1-built-in-functions (en/ja) | DifSim / Detector | `Foot(x, y)` は「Foot 位置のピクセル座標」 | 画像中心の検出器座標で、単位は mm。(0,0) は垂線の足（描画では Foot.X / Resolution[mm/px]） | Macro.cs:DifSimClass.Foot (L1133), FormDiffractionSimulator.cs:Foot (L315, L668) | 誤り | 修正済 |
| 1-built-in-functions (en/ja) | DifSim / Properties | `SkipRendering`: 「バッチ処理のため描画をスキップ」 | 効くのは動力学モード（bethe）のときだけで、gVector をクリアしてスポットを描かない。運動学/励起誤差モードでは何もしない。動力学モード中に SaveAsPng した画像にはスポットが写らない | FormDiffractionSimulator.cs:Draw L1091 `if (SkipRendering && bethe) gVector.Clear()` | 不正確 | 修正済 |
| 2-examples (en/ja) | Tips | 「SkipRendering=True でループの描画をスキップして高速化」 | 同上 | 同上 | 不正確 | 修正済 |
| 2-examples (en/ja) | Batch 1 | `Calc_Kinematical()` と `SkipRendering = True` を併用して PNG を保存している | 運動学モードでは SkipRendering が効かないので害は無いが、意味の無い指定になっている | 同上 | 不正確 | 未修正（例そのものは動く。Tips を直したので様子見） |
| 1-built-in-functions (en/ja) | DifSim / Properties | `Energy` は keV | 中性子は meV（[Help] にも明記） | Macro.cs:DifSimClass.Energy (L1046) | 不正確 | 修正済 |
| 1-built-in-functions (en/ja) | StructureViewer | Export3DModel の戻り値・拡張子の制約を書いていない | 戻り値は情報文字列（三角形数・出力サイズ）。filename は必須でダイアログは開かず、拡張子が .stl/.3mf 以外なら例外 | Macro.cs:StructureViewerClass.Export3DModel (L1325-) | 不正確（欠落） | 修正済（追記） |
| 1-built-in-functions (en/ja) | Global functions | `Sleep(ms)` | 実際の呼び方は `ReciPro.Sleep(ms)`（Macro の static メソッド）。関数表には出てこない | Macro.cs:Sleep (L73), Macro ctor addHelp | 不正確 | 修正済 |
| index (en/ja) | Overview / Access | 「メインウィンドウの Macro ボタンで開く」 | ボタンではなくメニュー **Macro > Editor**（ja: マクロ > 編集）。一覧の各マクロは Macro メニューに項目として並び、選ぶとそのまま実行される | FormMain.cs:editorToolStripMenuItem_Click / SetMacroToMenu / macroMenuItem_Click (L1916-1932) | 誤り | 修正済 |
| index (en/ja) | Shortcuts | `.mcr` をドロップすると「エディタに読み込む」 | エディタに読み込んだうえで、ファイル名をマクロ名として一覧に Add する（同名があれば置換するか確認） | FormMacro.cs:ReadMacroFile → buttonAddMacro_Click | 不正確 | 修正済 |
| index (ja) | 概要 / 保存先 | 「アプリケーション間で永続化」 | en は「persists across sessions」。実装も起動をまたいでレジストリに保存するだけ（FormMain.Registry の key "Macro"） | FormMain.cs L778/780, FormMacro.cs:ZippedMacros | ja-en不一致 | 修正済 |
| 3-external-control (en/ja) | Enabling | 「**Option →** …」 | メニュー名は en "Options"、ja "オプション(&O)" | FormMain.resx / FormMain.ja.resx: optionToolStripMenuItem.Text | 誤り | 修正済 |
| 2-examples (en/ja) | C | 「a 軸（x 軸）周りに回転」 | RotateAroundAxis は結晶の [uvw]（RotationMatrix·(ua+vb+wc)）周りの回転。実験室の x 軸と一致するとは限らない | Macro.cs:DirectionClass.RotateAroundAxis (L934) | 不正確 | 修正済 |
| index (en) | Editor window | ja の表には無い「CTRL+↑/↓ N/A」行がある | 機能は無いので内容は正しい | — | ja-en不一致（些末） | 未修正 |
| 3-external-control (en/ja) | Notes | 「30 秒の期限はリクエストの転送にだけ適用」 | レスポンスの書き込みにも 30 秒の CancellationToken が付いている（クライアントが読まなければ打ち切られる）。また /o の値が無いと、結果ファイルを作らずに ExitCode=1 で終わる | MacroPipeListener.cs:ServeOneRequest, FormMain.cs L563-568 | 不正確（些末） | 未修正 |
| 3-external-control (en/ja) | Notes | 「JSON の非 ASCII 文字は \uXXXX にエスケープ」 | 既定の System.Text.Json エンコーダーなので、`< > & ' +` など HTML で意味を持つ ASCII 文字もエスケープされる（パースすれば元に戻るので実害なし） | MacroPipeListener.cs:ServeOneRequest (JsonSerializer.SerializeToUtf8Bytes) | 不正確（些末） | 未修正 |

ラベル照合: Show samples / Add / Replace / Delete / Run macro / Step by step / Next step (F10) / Stop / Macro name / `Line N, Col M` / タイトル `Macro*` は FormMacro.resx と一致した。ja の "Samples" も FormMacro.ja.resx（checkBoxSamples.Text='Samples'）と一致した。F1 は FormMain.cs L428 の `HelpPage = "20-macro"` で開く。CLI の /m /o /x の挙動・終了コード 1 になる条件、パイプのプロトコル（1 MiB 上限・30 s・NUL 終端・JSON・Mutex で先着 1 プロセスだけが受け付ける・CurrentUserOnly・スコープ共有）は実装どおりだった。HRTEM/STEM/Potential の単位（Cs/Cc mm、ΔV eV、β・絞り rad、STEM の角度 mrad、光源サイズ pm、ImageResolution nm/px、GaussianBlurFWHM pm）は FormImageSimulator.cs の換算と resx の Footer 表示から確認し、すべて一致した。

### マニュアルに無い機能

| フォーム | 機能/コントロール | 概要 | 根拠 | 対応 |
|---|---|---|---|---|
| FormMacro | File メニュー (Load file / Save file) | `.mcr` を UTF-8 で読み込んで一覧に追加する／エディタの内容を `.mcr` に保存する | FormMacro.cs:loadToolStripMenuItem_Click / saveToolStripMenuItem_Click, FormMacro.resx | 追記済（index のエディタ表に 1 行） |
| FormMain | Macro メニューへの登録 | 一覧の各マクロが Macro メニューの項目になり、選ぶと直接実行される（RunMacroName） | FormMain.cs:SetMacroToMenu / macroMenuItem_Click | 追記済（Access の行） |
| FormMacro | 未保存変更の確認 | Dirty のまま別のマクロを選ぶか Show samples を切り替えると Yes/No/Cancel の保存確認が出る。Add で名前が重複していると置換するか確認する | FormMacro.cs:ConfirmSaveDirty / buttonAddMacro_Click | 未対応（些末） |
| FormMain | /x 指定時はパイプリスナーを起動しない。--capture/--diagnose でも起動しない | ワンショット実行ではリスナーを開始しない | FormMain.cs L605-607 | 未対応（些末） |
| DifSim | SpotInfo() は Back Laue モード非対応 | Back Laue のときは "Error: SpotInfo() does not support the Back Laue mode." を返す。CSV の列構成と単位（d nm、g/Sg nm⁻¹、detX/Y mm）は [Help] にあるがマニュアルには無い | Macro.cs:DifSimClass.spotInfo | 未対応 |
| File | GetDirectoryPath の戻り値は末尾 `\` 付き | 例が `folder + name` と連結できるのはこのため。キャンセルすると `"\"` が返る | Macro.cs:FileClass.getDirectoryPath | 未対応（些末） |

### 要確認事項

1. `ReciPro.Sleep` に [Help] があるのに、Macro コンストラクタは子クラスにしか `addHelp` を呼ばないため、エディタの関数表と補完候補に Sleep が出てこない。意図した挙動か（マニュアルは「表に出ない」と明記する形で直した）。
2. **コードのバグの可能性**: `FormImageSimulator.HRTEM_ObjAperRadius` の setter は、else 側（有限値）でも `checkBoxOpenAperture.Checked = true` にしている。このため `HRTEM.ApertureSemiangle = x` を設定すると絞りが「開放」になり、getter は +∞ を返す。else 側は `false` の誤りではないか（FormImageSimulator.cs L184-197）。マニュアルには書いていない。
3. サンプルマクロ 09（Macro.cs の _sampleMacros）は `print()` を使っているが、GUI エディタで実行すると print の出力は捨てられる（quiet=false）。サンプル 01 のコメントは「print() is not available」と書いており、矛盾している。またサンプル 07-01〜07-03 のコメントは "Variation of 08" となっているが、実際は 07 の変種ではないか。
4. ja 版の Samples チェックボックスは ja.resx で英語の 'Samples' のまま（en は 'Show samples'）。訳語を当てる予定はあるか（マニュアルは現行の resx に合わせてある）。
5. 例 Batch 1 は `Calc_Kinematical()` と `SkipRendering=True` を組み合わせている。運動学モードで描画を省く機能を意図していたのか（実装は動力学モード限定）。意図どおりなら例から SkipRendering を外すか、Calc_Dynamical + SpotInfo の例に差し替えるか、どちらにするか。
6. `DifSim.Tau/Phi` の setter は FormDiffractionSimulatorGeometry が null だと NullReferenceException になる（getter は null を考慮している）。DifSim.Open() の前に設定すると落ちるかどうか。

### 変更したファイル

- docs/src/en/20-macro/index.md
- docs/src/ja/20-macro/index.md
- docs/src/en/20-macro/1-built-in-functions.md
- docs/src/ja/20-macro/1-built-in-functions.md
- docs/src/en/20-macro/2-examples.md
- docs/src/ja/20-macro/2-examples.md
- docs/src/en/20-macro/3-external-control.md
- docs/src/ja/20-macro/3-external-control.md

（どの修正箇所にも `<!-- 261105Cl draft: … -->` コメントを付けた。表の行の中ではセルの先頭に入れている。）

