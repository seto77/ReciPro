<!-- 260603Cl 追加: NIST 弾性散乱サンプラーデータの出所・再生成手順書。
     経緯: 元データ E_*.TXT の生成手段(NIST SRD64 Web ダウンロード)が一度失われ、
     git 履歴の削除済みスクリプトから復元して特定した。再発防止のため恒久記録する。 -->

# NIST Elastic-Scattering Sampler データ — 出所と再生成手順

このフォルダは、EBSD モンテカルロ電子飛程計算（[`MonteCarlo.cs`](../../MonteCarlo.cs)）の
**弾性散乱角サンプリング**に使う NIST SRD 64 由来のデータと、その圧縮済み C# ソースを格納する。

> **重要（再発防止）**: 元データ `Original\E_*.TXT` の**生成手段は一度記録が失われた**。
> 実際には ELSEPA をローカル実行したものではなく、**NIST SRD 64 の Web サービスから
> ダウンロードした sampler 数値データ**である（下記「出所」参照）。今後データを更新・拡張する
> 際は必ずこの手順書を更新すること。

---

## 1. ファイル構成

| パス | 種別 | 説明 |
|---|---|---|
| `Original\E_01.TXT` … `E_96.TXT` | 元データ（**配布外**） | NIST SRD 64 sampler の生データ。Z=1..96。`.git/info/exclude` でローカル限定（git 非追跡） |
| `NistElasticPchip.bin` | **自動生成** | `E_*.TXT` を 51 ノット PCHIP に圧縮し、全 96 元素を 1 個の **Brotli 圧縮バイナリ**へ集約したもの。実行時に使うのはこちら（260604Cl: 旧 `PCHIP01..96.cs` + `Registry.cs` を置換） |
| `Diagnostics\PCHIP01.csv` … | **自動生成** | 圧縮誤差の診断用 CSV |

`NistElasticPchip.bin` は [`Crystallography.csproj`](../../Crystallography.csproj) で `<EmbeddedResource>`（LogicalName=`Crystallography.NistElasticPchip.bin`）として埋め込まれ、実行時に [`NistElasticPchipResource`](../NistElastic.cs) が**元素単位で lazy 展開**する（展開結果は旧静的配列とビット完全一致）。
**配布版が実行時に使うのはこの埋め込みリソースのみで、`Original\E_*.TXT` には依存しない。**

> 260604Cl 経緯: 旧形式は 96 個の `PCHIP{NN}.cs` が `ushort[][]`/`float[][]` のコレクション式を直接初期化していたため、
> ジャグ配列の内部配列ごとに `<PrivateImplementationDetails>` のハッシュ名フィールドが約 21,400 個生成され、
> `#Strings` ヒープ（約 1.4MB）と初期化 IL が肥大化していた。1 個の Brotli リソースへ集約して **Crystallography.dll を約 3.5MB 削減**。
> フォーマット: `magic "NEP3"` / `version` / `codec(1=brotli)` / `method(1=xKnot byte-plane shuffle)` / `energyCount` / `knotCount` / `count` / `index[(z,offset,length)…]` / `payload(元素別 Brotli blob)`。各 blob は展開後 `sigma(double LE) + phiKnotIndex(ushort LE) + xKnot(float, byte-plane shuffle)`。

---

## 2. 出所（provenance）

元データ `E_*.TXT` は **NIST Electron Elastic-Scattering Cross-Section Database (SRD 64)** の
Web サービスから取得した。

- 取得 URL: `https://srdata.nist.gov/srd64/ElasticSimpler/SimplerDownload/{原子番号}`
  （ページ内リンク **"Numerical data for the sampler"** をクリックして TXT をダウンロード）
- 取得手段: **Playwright（headless Chromium）** による自動ダウンロードスクリプト
- データは NIST サーバ側で **相対論的 Dirac partial-wave 法（ELSEPA 系）により事前計算済み**。
  つまり**自前で ELSEPA を走らせる必要はない**。

### 取得スクリプト（現在はリポから削除済み・git 履歴から復元可能）

| ファイル | 役割 | 復元コマンド |
|---|---|---|
| `tools/fetch_nist_elastic_sampler.py` | Playwright で Z=1..96 を DL し TXT/float32 BIN 化 | `git show 6dbf3b5e:tools/fetch_nist_elastic_sampler.py` |
| `tools/FetchNistElasticSampler.ps1` | venv + `playwright install chromium` のランチャ | `git show 6dbf3b5e:tools/FetchNistElasticSampler.ps1` |

- 初出コミット: `dd23c298` / `6dbf3b5e`（2026-03-31、ReciPro リポ）
- 削除コミット: `7cfcd649`（2026-04-05「Clean up repo root」）
- ※スクリプトは **ReciPro リポ**側の `tools/` にあった（Crystallography はジャンクション追跡）。

---

## 3. フォーマット仕様（`E_*.TXT`）

1 ファイル = 1 元素。**111 エネルギーブロック**が連続する（260603Cl 101→111 に拡張）。各ブロックは:

```
<行1>  ブロック番号 (1..111)          ← パーサは読み飛ばす（NIST Fortran 版でも未使用）
<行2>  SigmaA0Squared                 ← 全弾性散乱断面積 σ_el [a0² 単位]
<行3..行2003>  Φ(cosθ) を 2001 点      ← 累積角度分布 (CPD)。cosθ=+1→-1 を 0.001 刻み、
                                          先頭 0.0 から単調増加して末尾 1.0
```

- **エネルギー軸**: 50 eV 〜 **36.4 keV** を**対数等間隔 111 点**（260603Cl 拡張。対数刻みは不変のまま 20 keV 超へ等間隔延長）。
  `E_eV(blockIndex) = exp(log(50) + ((log(20000)-log(50))/100) · blockIndex)`、`blockIndex = 0..110`
  （刻みの基準点は 20 keV=blockIndex 100 のまま。blockIndex 110 = exp(log50+step·110) ≈ **36411 eV**）
  - blockIndex **0..100**（50 eV–20 keV）: NIST **sampler** 由来（不変）
  - blockIndex **101..110**（21.2–36.4 keV）: NIST **DCS** 由来（自前 CDF 化、260603Cl 継ぎ足し）
  （実装: [`NistElastic.cs`](../NistElastic.cs) `NistElasticPchip.EnergyEvFromBlockIndex`、L24。`EnergyCount = 111`）
- **角度座標**: PCHIP の x 軸は `sqrt(1 - cosθ)`（= √2·sin(θ/2)）で変換される
  （`NistElasticPchip.TransformErrorCoordinate`、L27）。前方ピークの分解能を確保するため。

---

## 4. 再生成手順（50 eV–20 keV の sampler 部分）

> 以下は **blockIndex 0..100（50 eV–20 keV）の sampler 部分**の再生成手順。
> 20 keV 超の DCS 継ぎ足し（blockIndex 101..110）は §5 を参照。両者を結合して 111 ブロックの `E_*.TXT` を作る。

```powershell
# (1) 取得スクリプトを git から復元
git show 6dbf3b5e:tools/fetch_nist_elastic_sampler.py > tools/fetch_nist_elastic_sampler.py
git show 6dbf3b5e:tools/FetchNistElasticSampler.ps1   > tools/FetchNistElasticSampler.ps1

# (2) NIST から Z=1..96 を再取得（venv + Playwright/Chromium を自動構築）
./tools/FetchNistElasticSampler.ps1 -AtomicNumbers (1..96)
#   → E_01.TXT .. E_96.TXT を Original\ に配置

# (3) NistElasticPchip.bin を再生成（260604Cl: 旧 PCHIP*.cs/Registry.cs ではなく 1 個の Brotli リソース）
#   FormEBSD（開発者導線）から NistElasticSamplerPchipGenerator.GenerateCompressedSources を実行
#   （ReciPro/FormEBSD.cs L180-219 参照）。出力された NistElasticPchip.bin がそのまま埋め込まれる。
```

生成ツール本体: [`NistElastic.cs`](../NistElastic.cs) `NistElasticSamplerPchipGenerator`
（`ReadTextTable` → `CompressElement`/`CompressBlock`（2001点→51ノット PCHIP）→
`WriteGeneratedResource`（全元素を `NistElasticPchip.bin` へ集約）／`WriteDiagnosticsCsv`）。

---

## 5. 20 keV を超える拡張手順（案 X）

> **✅ 2026-06-03 (260603Cl) 実装完了**: 案 X で 50 eV–**36.4 keV / 111 点**へ拡張済み。
> 全 96 元素 × blockIndex 101..110（21.2–36.4 keV）を NIST DCS から CDF 化して継ぎ足し、
> `E_*.TXT`（バックアップ `Original_backup_260603\`）・`PCHIP01..96.cs`・`Registry.cs`（260604Cl 以降は `NistElasticPchip.bin`）・ランタイム定数を更新。
> MC 検証（`c:\tmp\PchipRegen`）で 30 keV を含む全域で Mott sampler が使われ、20 keV 境界も連続することを確認。
> ⚠ 拡張時に `BuildMottElasticMixtureCache` の `new MottElasticMixtureEntry[101]` ハードコード（[`MonteCarlo.cs`](../../MonteCarlo.cs)）が
> 取り残されて `atoms` 付き 20 keV 超構築で `IndexOutOfRange` クラッシュ → `[NistElasticEnergyCount]` に修正済み。
> 以下は当時の設計手順（再拡張時の参考）。

### なぜ単純な定数変更では不可か

NIST の **"sampler"（2001 点 CDF 済み）機能は 20 keV 以下限定**である
（SRD 64 公式: sampler は 50 eV–20 keV、20 keV 以下でのみ 3 座標系で DCS を表現）。
一方 **DCS（dσ/dΩ）・全断面積・transport 断面積は 300 keV まで**提供される。
→ 20 keV 超を sampler 形式で得るには、**DCS を取得して自前で CDF 化**する必要がある。

### 推奨プラン（案 X: 境界継ぎ足し / codex 検討 2026-06-03）

既存 50 eV–20 keV の sampler データは**一切変更せず**、20 keV 超だけ DCS 由来で継ぎ足す。
既存 EBSD 結果を壊さず、20 keV 超フォールバック（従来 screened Rutherford）だけを NIST 由来に置換する。

1. **既存 50 eV–20 keV は不変**。
2. NIST DCS を取得（スクリプト [`tools\fetch_nist_elastic_dcs.py`](tools/fetch_nist_elastic_dcs.py)、
   **260603 動作確認済**）。NIST は Radzen.Blazor 製。**確定フロー**:
   `/srd64/Elastic/SelInitEnergy/{Z}` → Energy 入力欄に値・**keV** ラジオ →
   座標系 `dσ/dΩ vs θ`（20 keV 超はこれのみ）→ **Display** → **View Data**（データ展開）→
   **CSV Data File**（JS `BlazorDownloadFile` で保存）の **2 段階**。
   例: `python tools\fetch_nist_elastic_dcs.py 14 --energies 20`（複数 Z/energy 可、`--debug` でスクショ/HTML 保存）。
   取得 CSV の形式: ヘッダに `Total cross section [a0²]`、本体は `θ(deg), dσ/dΩ(a0²/sr)` を
   **θ=0..180° / 1° 刻み / 181 点**。`a0² = 2.8002852E-21 m²`。
   まず **20 keV** を取得し、自前 CDF 化が既存 sampler（`Original\E_14.TXT` の最終ブロック=20 keV）と一致するか照合（検証ゲート）。
3. 照合が通ったら 21–30 keV（必要なら 0.5 keV 刻み）× Z=1..96 の DCS を取得。
4. 各 DCS → `σ_el`・`σ_tr`・`Φ(μ)` 2001 点を生成（下記レシピ）。
5. `SigmaA0Squared` は NIST 公表の全断面積、CDF は DCS 積分で正規化。単位は `a0²` 保持。
6. ランタイム/生成側の定数を拡張（下表）。30 keV までテーブルがあれば Mott、範囲外のみ
   screened Rutherford に落とす。
7. 20 keV 境界・25 keV・30 keV で断面積/transport/角度ヒストグラムを検証。

### DCS → CDF 変換レシピ

`u = sin²(θ/2) = (1 − μ)/2`、`μ = cosθ`、`D(u) = dσ/dΩ [a0²/sr]` として:

```
σ_el = 4π ∫₀¹ D(u) du
Φ(μ_j) = (4π / σ_el) · ∫₀^{u_j} D(u) du ,   u_j = (1 − μ_j)/2 ,  μ_j = 1 − 0.001·j  (j=0..2000)
σ_tr = 8π ∫₀¹ u·D(u) du            （検証用 transport 断面積）
```

- 補間: `log(D)` を `u`（または `sin(θ/2)`）上で **shape-preserving PCHIP**。
  通常の 3 次スプラインは深い Mott 極小付近で負値・過剰振動を出すので**禁止**。
- 積分: 元の NIST 角度ノットを必ず含め、各区間を 8–16 点 Gauss-Legendre。
- 既存パイプラインの角度座標 `sqrt(1-cosθ)`（§3）と整合させること。

### 検証ゲート（最低限）

- 20 keV で既存 sampler CDF と DCS 生成 CDF を比較
- `σ_el` が NIST 全断面積と一致（目安 ≤0.1–0.2%）
- `σ_tr` が NIST transport 断面積と一致（目安 ≤0.5–1%）
- `P(θ>30°)` / `P(θ>90°)` / `P(θ>120°)` を比較
- 生成乱数ヒストグラムを `2π·D(θ)·sinθ` と比較

### 20keV 実証結果（260603Cl、[`tools\verify_dcs_cdf.py`](tools/verify_dcs_cdf.py)）

Z=14 Si の DCS から自前 CDF 化し、既存 `E_14.TXT` 最終ブロック（20keV）と照合:

- σ_el: 自前積分 0.41315 vs NIST 0.41243 a0²（rel **0.17%**、ゲート ≤0.2% 内）
- CDF Φ(μ) 2001点: **max\|Δ\|=5.9E-4**（最前方 j=1）、mean\|Δ\|=6.4E-6 ＝ ほぼ完全再現
- P(θ>30/90/120/150°): 自前と既存がほぼ一致（例 θ>90°: 0.00074 vs 0.00074）
- **σ_el は NIST 公表値（CSV ヘッダの Total cross section）を保存値に使う**（自前積分値ではなく）。
  CDF は末尾を 1 に正規化するので σ_el 差の影響を受けない。

→ **案X 実証。DCS→CDF 化は NIST sampler を再現できる**ので、20–30keV を同手順で生成してよい。

### 変更したコード定数（260603Cl 実装済み）

実装の要点は **対数刻み `LogEnergyStep` を不変のまま**（20 keV = blockIndex 100 が刻みの基準点）、
**点数だけを 101→111 に増やして等間隔延長**したこと。上限は「30 keV ちょうど」ではなく、
等間隔の都合で blockIndex 110 = `exp(log50 + step·110)` ≈ **36411 eV** になる。
ガードは `20000` リテラルを新設の `NistElasticMaxEnergyEv`（≈36411 eV）に置換した。

| ファイル | 定数 | 旧 | 新（実装済み） |
|---|---|---|---|
| [`NistElastic.cs`](../NistElastic.cs) | `NistElasticPchip.EnergyCount` | 101 | **111** |
| [`NistElastic.cs`](../NistElastic.cs) | `LogEnergyStep` | (log20000-log50)/100 | **不変**（等間隔延長のため） |
| [`MonteCarlo.cs`](../../MonteCarlo.cs) | `log50` / `log20000` / `LogNistElasticEnergyStep` | 〃 | **不変**（log20000 は刻み基準点でテーブル上限ではない） |
| [`MonteCarlo.cs`](../../MonteCarlo.cs) | `NistElasticEnergyCount` | 101 | **111** |
| [`MonteCarlo.cs`](../../MonteCarlo.cs) | `NistElasticMaxEnergyEv` | （無し） | **新設** = `Math.Exp(log50+step·110)` ≈ 36411 eV |
| [`MonteCarlo.cs`](../../MonteCarlo.cs) | `energyEv > 20000.0` ガード ×3 | 20000 | **`> NistElasticMaxEnergyEv`** |
| [`MonteCarlo.cs`](../../MonteCarlo.cs) | index clamp ×2（GetNearest/GetLower） | 0, 99 | **0, NistElasticEnergyCount-2 (=109)** |
| [`MonteCarlo.cs`](../../MonteCarlo.cs) | `BuildMottElasticMixtureCache` 配列長 | `[101]`（バグ） | **`[NistElasticEnergyCount]`**（260603Cl 修正） |
| `PCHIP01..96.cs` の各配列長 | （自動生成） | 101 | **111**（再生成で自動追従） |

> 物理的背景の調査記録（screened Rutherford フォールバックの精度問題、Browning 式 `σ∝Z^1.7`、
> 係数 3.4e-3 の妥当性など）は、開発者メモリ `project_ebsd_elastic_scattering_model` を参照。

---

## 6. 関連コード早見表

| 役割 | 場所 |
|---|---|
| ランタイム入口（テーブル取得・キャッシュ） | [`MonteCarlo.cs`](../../MonteCarlo.cs) `GetNistElasticScatteringTable` / `TryLoadGeneratedNistElasticPchipTable` |
| 20 keV ガード | [`MonteCarlo.cs`](../../MonteCarlo.cs) `TryGetMottElasticTransport`（L648）/ `TrySampleMottElasticCosTheta`（L690） |
| 角度サンプリング（逆 CDF） | `AtomStatic.TryEvaluateGeneratedNistElasticPchipCosTheta`（[`NistElastic.cs`](../NistElastic.cs)） |
| 圧縮生成ツール | [`NistElastic.cs`](../NistElastic.cs) `NistElasticSamplerPchipGenerator` |
| 生成 GUI 導線 | `ReciPro/FormEBSD.cs` L180-219 |
| フォールバック（screened Rutherford） | [`MonteCarlo.cs`](../../MonteCarlo.cs) `ComputeTransportParameters`（L604-）/ `SampleElasticScatteringCosTheta`（L668-） |
