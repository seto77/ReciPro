# トラブルシューティング

ReciProでよくある問題と解決方法です。以下の項目の多くは [GitHub の Issue](https://github.com/seto77/ReciPro/issues) に寄せられた質問やバグ報告に基づいており、修正されたバージョンが分かるものは併記しています。

> **多くの問題は[最新版](https://github.com/seto77/ReciPro/releases/latest)に更新するだけで解決します。** ReciProは頻繁に更新されており、下記のバグの多くは報告から数日で修正されています。

---

## 起動と立ち上げ

### 症状: プロセスは動いているのにウィンドウが表示されない

ReciProは起動している（タスクマネージャーに表示される）のに、ウィンドウが画面に現れない。

**原因**: ウィンドウが画面外に開いている — モニター構成や表示スケールを変更した後などに起きる、Windowsの表示座標の問題です。(Issue [#50](https://github.com/seto77/ReciPro/issues/50), [#53](https://github.com/seto77/ReciPro/issues/53), [#55](https://github.com/seto77/ReciPro/issues/55))

**解決方法**:

1. **タスクマネージャー** を開く
2. プロセス一覧から **ReciPro** を探す
3. 右クリックして **最大化** を選択する

ウィンドウがメインディスプレイ上に表示されます。なお **切り替え** / **前面に表示** / **最小化** では解決せず、**最大化** だけが有効です。

### 症状: ReciProが起動しない・クラッシュする・固まる

**原因**: 多くの場合OpenGLの初期化に失敗しているか、レジストリ／設定値の破損が起動を妨げています。

**解決方法**（上から順に試してください）:

1. **OpenGLを無効化**: **Ctrl** キーを押しながら起動するとOpenGLを無効にして立ち上がります。最近のバージョン（v4.925以降）はOpenGL初期化を堅牢化しており、OpenGLが失敗してもアプリは起動します（その場合は3D機能のみ無効になります）。
2. **設定のリセット**: レジストリエディタでキー `HKEY_CURRENT_USER\Software\Crystallography\ReciPro` を削除してから再起動します（**オプション → レジストリを初期化** と同等）。
3. **クリーン再インストール**: ReciProをアンインストールし、以下のフォルダがあれば削除（`<user>` はユーザー名に置き換え）してから再インストールします。
   - `C:\Users\<user>\AppData\Local\Crystallography Software\ReciPro`
   - `C:\Users\<user>\AppData\Roaming\ReciPro\ReciPro`
4. **最新版に更新** する。

これらで解決しない場合、OS環境自体が原因の可能性があります。PCの情報（CPU・GPU・Windowsのバージョン）を添えて [Issue を立てて](https://github.com/seto77/ReciPro/issues) ください。

---

## OpenGL関連

### 症状: 起動時に画面が真っ黒、またはクラッシュする

**原因**: GPUが非対応、またはリモートデスクトップ環境。

**解決方法**:

1. **オプション → OpenGLを無効化（要再起動）** を選択（または **Ctrl** を押しながら起動）
2. ReciProを再起動
3. Structure Viewerなどの3D機能はソフトウェアレンダリングになります

### 症状: 内蔵GPUや古いGPU（Intel/AMD）で描画に失敗する

**原因**: 一部の内蔵GPU（AMD Radeon Vega、Intel UHD など）で、古いビルドではOpenGLの初期化に問題がありました。(Issue [#2](https://github.com/seto77/ReciPro/issues/2))

**解決方法**: 最新版に更新してください。OpenGLの要求バージョンを引き下げ（v4.781）、内蔵GPUでの初期化を修正し（v4.785）、さらに失敗しても落ちないよう堅牢化しました（v4.925）。GPUドライバの更新も有効です。

### 症状: 描画品質が低い

**解決方法**: GPUドライバを最新版に更新してください。OpenGL 1.5対応の外付けGPUを推奨します。

---

## .NET ランタイム

### 症状: アプリケーションが起動しない

**原因**: 必要な .NET Desktop Runtime がインストールされていません。現行版は **.NET Desktop Runtime 10.0** が必要です（古いビルドの v4.895〜v4.91x は 9.0 が必要でした。Issue [#43](https://github.com/seto77/ReciPro/issues/43)）。

**解決方法**: <https://dotnet.microsoft.com/download/dotnet/10.0> から **Desktop Runtime**（多くのPCはx64）をダウンロード・インストールしてください。

### 症状: Microsoftのダウンロードページにアクセスできない

**解決方法**: ランタイムのインストーラを直接ダウンロードできます。[.NET 10.0 ダウンロードページ](https://dotnet.microsoft.com/download/dotnet/10.0) から、ご使用のアーキテクチャ向けの **Windows Desktop Runtime X64** を選んでください。(Issue [#49](https://github.com/seto77/ReciPro/issues/49))

---

## インストール

### 症状: 管理者権限なしでインストール／アンインストールしたい

**補足**: 管理者権限は不要です。ショートカットやユーザー固有のファイルは、各ユーザー専用のフォルダ（例: `%AppData%\Microsoft\Windows\Start Menu\Programs\Crystallography Software\` やデスクトップ）に配置されます。(Issue [#38](https://github.com/seto77/ReciPro/issues/38))

---

## 表示・レイアウト

### 症状: ボタンやパネルが見切れる・隠れる、レイアウトが崩れる

例えば、最近のバージョンで Spot ID v2 の **ピーク同定** ボタンが隠れる、About画面など各フォームの配置が崩れる、といった症状です。(Issue [#56](https://github.com/seto77/ReciPro/issues/56), [#59](https://github.com/seto77/ReciPro/issues/59))

**原因**: 一部の最近のビルドで生じた、DPIスケーリング／UIフォントに起因する不具合です。

**解決方法**:

- Windowsの**表示スケールを100%**に設定する（多くの場合これでレイアウトが戻ります）。
- 応急処置として、**ウィンドウのサイズを変える**（例: 縦に縮める）と隠れたコントロールが現れます。
- 最新版に更新する — レイアウトは順次修正中です。最新ビルドで悪化した場合は、少し古いバージョン（例: v4.915）に戻すのも一時的な手段です。崩れているフォームがあればぜひ報告してください。

---

## 動力学計算

### 症状: 計算が非常に遅い、またはメモリ不足

**原因**: ブロッホ波の数が多すぎる、または画像サイズが大きすぎる。

**解決方法**:

- **ブロッホ波の数** を減らす（通常の計算には50–200で十分）
- 500波以下なら **Eigen** ソルバー、500波以上なら **MKL** ソルバーを使用
- STEMシミュレーションでは画像解像度を下げる
- 他のメモリ集約的なアプリケーションを閉じる

### 症状: HAADF-STEM像が真っ黒になる

**原因**: 原子の温度因子 (B) がゼロに設定されている。

**解決方法**: すべての原子の温度因子を B ≥ 0.5 Å² に設定してください。TDS強度の計算にはゼロでない温度因子が必要です。

---

## 回折シミュレータ

### 症状: 回折パターンが真っ白／何も描画されない

**原因**: たいていは拡大しすぎているか、入射波のエネルギーが範囲外です。(Issue [#3](https://github.com/seto77/ReciPro/issues/3))

**解決方法**:

- メイン描画領域を**左クリック**して縮小する。
- **Wave** タブ（左上）で入射波のエネルギーを確認する。X線は約 1–100 keV、電子線は約 10–1000 keV が適切です。

---

## ファイル入出力

### 症状: CIFファイルが読み込めない

**解決方法**:

- CIFファイルの書式が正しいか確認してください
- ファイルを **Crystal Information** 領域にドラッグ＆ドロップしてみてください
- 一部の非標準的なCIF拡張はサポートされていない場合があります

### 症状: dm3/dm4ファイルが読み込めない、または「'System.Single' から 'System.Double' へキャストできません」エラーが出る

**原因**: DM3/DM4形式には複数のバリエーションがあり、古いビルドではすべてを読めませんでした。(Issue [#15](https://github.com/seto77/ReciPro/issues/15))

**解決方法**: 最新版に更新してください。DM3の読み込み互換性は v4.835 で改善されました。それでも読み込めない場合は、[ファイルを送って](https://github.com/seto77/ReciPro/issues) いただければ対応を検討します。

### 症状: dm3/dm4ファイルのスケールが正しくない

**解決方法**: Digital Micrographソフトウェアでキャリブレーションを確認してください。ReciProは埋め込みメタデータを読み取ります。メタデータが不正な場合は、Opticsパネルでピクセルサイズとカメラ長を手動設定してください。

---

## レジストリのリセット

設定が破損した場合：

1. **オプション → レジストリを初期化（再起動時）** を選択
2. ReciProを再起動 — ウィンドウ位置、波長、カメラ長などがデフォルトにリセットされます

---

## よくある質問

### Mac版（やLinux版）はありますか？ {#mac-linux}

公式版はありません。ReciProは **.NET Desktop Runtime** に依存しており、これは現在Windowsでしか動作しません。(Issue [#12](https://github.com/seto77/ReciPro/issues/12))

ただし非公式な方法として、**win-x64版ポータブルZIP**（[リリースページ](https://github.com/seto77/ReciPro/releases/latest) で入手可能）を、Wineラッパーの **Sikarugir** と OpenGL互換ドライバ **Mesa3D** と組み合わせることで、macOS（Apple Silicon）上で動作したという報告があります。Windowsライセンスや仮想マシンは不要です。ユーザーによる導入手順が <https://github.com/Ryo-fkushima/ReciPro_macOS_memo> で公開されています。

なお、この方法は公式サポート外で、動作の完全な検証はされていません。一部の記号（Å・上付き文字・矢印など）が文字化けする既知の問題があります。

**記号（Å・上付き文字・矢印）の文字化けを直すには:** 原因は、ReciProが通常使うWindowsのフォント（Segoe UI・Yu Gothic UI など）がWine環境に無く、Wine内蔵の代替フォントが一部の学術記号グリフを持たないことです。ReciProは **Wine環境を検知すると自動的に、収録範囲の広いフォントへ切り替えます**。したがって、対象フォントをWine prefixに用意するだけで解消します。

1. **DejaVu Sans** / **DejaVu Serif**（Å・上付き・矢印・分数ラベルを収録）を導入します。日本語UIでは **Noto Sans CJK JP**（または **Noto Sans JP**）も導入します。
2. 最も簡単なのは、ダウンロードした `.ttf`/`.otf` を prefix のフォントフォルダ（Sikarugirラッパー内の `…/drive_c/windows/Fonts/`）へコピーし、ReciProを再起動する方法です。（`winetricks` でも一部導入できます。）
3. 再起動するとReciProが自動的に認識します。ReciPro側の設定変更は不要です。

フォントを入れない場合、ReciProは既定のフォント名のまま動作するため、悪化することはありません（記号が文字化けしたままになるだけです）。

**この方法の今後の見通し（正直な注記を2点）:**

- 実験的に公開している **win-arm64版ZIP** は、Apple SiliconのMacでも**動作しません**。現行のmacOS用Wine（Sikarugirを含む）はx86_64版Windowsバイナリを Rosetta 2 経由で実行する構成で、ARM64版Windowsバイナリを実行する仕組みがないためです。Macでは常に **win-x64版** ポータブルZIPを使ってください。
- Appleは Rosetta 2 の段階的廃止を発表しています。完全な Rosetta 2 を備える最後のバージョンは macOS 27（2026年秋）とされており、macOS 28（2027年秋）以降は現行の「x64版 + Rosetta 2」方式は動作しなくなる見込みです。macOSネイティブのARM64版Wineの開発が上流で進行中のため、実現すればwin-arm64版ZIPがMacでの後継になる可能性がありますが、現時点では保証できません。

### Windows on ARM（ARM64）で動きますか？ {#windows-on-arm}

はい。2通りの方法があります。

- **ネイティブARM64版（実験的・推奨）**: v4.938 から、ネイティブARM64ビルドのポータブル版（`ReciPro-v.X_arm64.zip`。v.4.939 までは `ReciPro-v.X-arm64.zip`）を[リリースページ](https://github.com/seto77/ReciPro/releases/latest)で公開しています。self-contained 形式のため .NET Runtime のインストールは不要です。ZIPを書き込み可能なフォルダに展開し、`ReciPro.exe` を実行してください。ダウンロードしたZIPがWindowsにブロックされる場合（Mark of the Web）は、**展開前に**ZIPを右クリック → **プロパティ** → **許可する（Unblock）** にチェック → **OK** としてください（PowerShellでは `Unblock-File .\ReciPro-*arm64.zip`）。詳細は同梱の `README-PORTABLE.txt` を参照してください。
- **x64版をエミュレーションで実行**: 通常のMSIインストーラやwin-x64ポータブルZIPも、ARM64版Windows内蔵のx64エミュレーションで動作します（.NET Desktop Runtime (x64版) のインストールが必要。v4.913 付近・.NET 10 で動作確認済み）。重い計算はネイティブ版より遅くなります。(Issue [#47](https://github.com/seto77/ReciPro/issues/47))

ネイティブARM64版の注意点:

- Intel MKL はARM64版が存在しないため、対応するソルバー選択肢とメニュー項目は非表示になります。動力学計算は同梱のNEON最適化ネイティブライブラリで実行され、代表的な検証ケースではx64版との差が想定される浮動小数点誤差の範囲内であることを確認済みです。
- 3D表示（Structure Viewer など）は利用できますが、Windows on ARM の OpenGL は Direct3D 12 変換レイヤ（GLOn12 / Mesa）経由でのみ提供されるため、ネイティブOpenGLドライバを持つPCより明らかに低速です。これはプラットフォームの制約であり不具合ではなく、ネイティブARM64ビルドでも変わりません。Structure Viewer の透過モード **High quality (Per-Pixel Linked List)** はこのドライバ構成では特に低速のため、既定の **Approximate** を推奨します。3D表示が起動しない場合は、Microsoft Store から「OpenCL, OpenGL, and Vulkan Compatibility Pack」をインストールしてください。
- ARM64版は **macOS + Wine では動作しません**（前項参照）。Macではwin-x64ポータブルZIPを使ってください。

### ReciProを引用するには？

[GitHubリポジトリのページ](https://github.com/seto77/ReciPro) の **Cite this repository** リンクをご利用ください（メタデータは `CITATION.cff` で提供しています）。推奨される引用は次の論文です。

> Seto, Y. & Ohtsuka, M. (2022). *J. Appl. Cryst.* **55**, 397–410. doi:[10.1107/S1600576722000139](https://doi.org/10.1107/S1600576722000139)

(Issue [#33](https://github.com/seto77/ReciPro/issues/33))

---

## バグ報告

バグ報告はこちら: <https://github.com/seto77/ReciPro/issues>

以下の情報を含めてください：

- ReciProのバージョン番号
- 問題を再現する手順
- エラーメッセージやスクリーンショット
