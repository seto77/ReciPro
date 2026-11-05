# HRTEM シミュレーション

**HRTEM (High-Resolution Transmission Electron Microscopy)** シミュレーションは、高分解能TEM格子縞像を計算します。[HRTEM/STEMシミュレータ](index.md) のメインモードです。

![HRTEMモードのシミュレータ](../../assets/cap-ja-auto/FormImageSimulator-hrtem.png)

> このページは、**イメージモード = HRTEM** を選んだときに右側に現れる設定項目をすべて掲載します。結果の表示・明るさ調整など左側の操作は [まとめページ](index.md#結果の表示調整左側パネル) を参照してください。

---

## 概要

HRTEM像は、試料を透過した電子波が対物レンズの収差の影響を受けて結像することで形成されます。ReciProでは、ブロッホ波法（Dynamical 計算）で試料中の電子波伝搬を計算し、位相コントラスト伝達関数 (PCTF) を通してHRTEM像を生成します。

### 計算の流れ

1. **ブロッホ波法**: 結晶ポテンシャル中の電子波伝搬を計算し、出射波の振幅と位相を取得
2. **レンズ関数**: 対物レンズの収差（球面収差 $C_s$、デフォーカス $\Delta f$）を適用
3. **部分コヒーレンス**: 光源の有限サイズ（空間コヒーレンス）とエネルギー揺らぎ（時間コヒーレンス）を考慮
4. **像の形成**: 強度分布 $|\psi(\mathbf{r})|^2$ を計算

理論の詳細は [Appendix A3.2 — HRTEM 像形成](../appendix/a3-bloch-wave/hrtem.md) を参照してください。

---

## 試料情報

![試料情報](../../assets/cap-ja-auto/FormImageSimulator.splitContainer1.flowLayoutPanelModeSelection.groupBoxSampleProperty.png)

- **厚み** : 試料の厚さ (nm)。HRTEM像は厚さに強く依存します。**シリーズ画像** モードのときはこの値は無視され、後述の厚さリストが使われます。

---

## TEMの条件 {#tem-conditions}

![TEMの条件](../../assets/cap-ja-auto/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxTEMConditions.png)

対物レンズの結像条件を設定します。

| パラメータ | 説明 | 既定値 / 典型値 |
|-----------|------|-----------------|
| **加速電圧 (kV)** | 加速電圧。相対論補正された電子波長が右に表示されます | 200 kV |
| **デフォーカス Δf** | 対物レンズのデフォーカス (nm)。下に **シェルツァーデフォーカス** の参考値が表示されます | −57.8 nm |
| **Cs** | 球面収差係数 (mm)。CTFとシェルツァーデフォーカスに影響します。負の値（−20〜20 mm）も入力できます | 1.0 mm（既定）。0.5–1.0（通常）、< 0.01（Cs補正） <!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormImageSimulator.Designer.cs / FormImageSimulator.ja.resx:numericBoxDeltaV.HeaderText) --> |
| **Cc** | 色収差係数 (mm)。エネルギー幅による像のぼけを決めます | 1.4 mm（既定）。1.0–2.0 mm |
| **β** | 照射半角 (mrad)。有限光源サイズ効果（空間コヒーレンス）を表します | 0 mrad（既定）。0.1–1.0 mrad |
| **ΔV** | 電子線のエネルギー幅の半値全幅 (eV)。Cc とともに色収差による焦点広がりを決めます | 0.8 eV（既定）。0.5–2.0 eV |

<!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormImageSimulator.cs:Cs/DeltaVol/Scherzer, FormCTF.cs:Renew, BetheMethod.cs:GetHRTEMImage) -->
上の既定値は初回起動時のもので、以後は前回の値が復元されます。内部では ΔV (FWHM) を標準偏差 $\sigma_V = \Delta V / (2\sqrt{2\ln 2})$ に換算し、焦点広がりを $\delta = C_c\,\sigma_V / V$ とします。収差関数は

$$\chi(u) = \pi\lambda\,\Delta f\,u^2 + \tfrac{\pi}{2}\,C_s\lambda^3 u^4$$

で、各回折波に $\exp[-i\chi(u)]$ を掛けます。したがって **Δf が負のときがアンダーフォーカス** です（シェルツァーデフォーカスが負になる流儀）。

<!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormImageSimulator.cs:zeroAllToolStripMenuItem_Click / presets1ToolStripMenuItem_Click〜presets4) -->
> **右クリックメニュー**: TEMの条件パネルでは、**デフォーカスを0 nmに設定** / **デフォーカスをシェルツァー値に設定** / **収差をすべて0に設定** をワンクリックで行えます（最後のものは Cs・Cc・β・ΔV に加えて **デフォーカスも** 0 にします）。同じメニューに顕微鏡プリセットが 4 つあります — *300kV Cold-FEG Cs-free (Cs 0, Cc 2.8, ΔE 0.3) ARM300F*、*200kV Schottky-FEG UHR (Cs 0.5, Cc 1.1, ΔE 0.8) 2100F*、*200kV Schottky-FEG HR (Cs 1.0, Cc 1.4, ΔE 0.8) 2100F*、*200kV Lab6 HR (Cs 1.0 , Cc 1.4, ΔE 2.0) 2100*（項目名は英語のみ）。項目名に書かれた加速電圧・Cs・Cc・ΔV を設定し、デフォーカスをシェルツァー値にします（β は変更しません）。右下の **条件プリセット** で開くウィンドウは、利用者が自分の条件を保存するためのものです（[プリセットリスト](index.md#preset-list) 参照）。

### シェルツァーデフォーカス

現在の波長と球面収差 $C_s$ から計算される、位相コントラストが最適となる付近のデフォーカス値（参考表示）です。<!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormImageSimulator.cs:Scherzer) -->$C_s$ が負のときは $+\sqrt{\tfrac{4}{3}|C_s|\lambda}$ を表示します。

$$\Delta f_{\text{Scherzer}} = -\sqrt{\tfrac{4}{3}\,C_s \lambda}\quad\left(\approx -1.155\,\sqrt{C_s \lambda}\right)$$

この条件では、PCTFが広い空間周波数範囲で負になり、原子位置が暗いコントラストとして得られます。ReciPro はこの原典の Scherzer 値（収差位相 $\chi$ の極小値を $-2\pi/3$ とする条件から導かれる）を採用しており、GUI に表示される値もこの式によります。なお、帯域をさらに広げた *extended Scherzer* 値 $-1.2\sqrt{C_s\lambda}$ を用いる流儀もあります。

---

## レンズ関数 / コントラスト伝達関数 (CTF)

**コントラスト伝達関数 (CTF)** をチェックすると、レンズ収差とデフォーカスが空間周波数ごとに像コントラストをどう伝達するかをプロットするウィンドウが開きます。

![コントラスト伝達関数 (CTF)](../../assets/cap-ja-auto/FormCTF.png)

- $\sin\chi(u)$ : 位相コントラスト伝達関数（$\chi(u)$ はレンズの収差関数）
- $E_\text{s}(u)$ : 空間コヒーレンスのエンベロープ関数。光源の有限サイズ（$\beta$）による減衰
- $E_\text{c}(u)$ : 時間コヒーレンスのエンベロープ関数。エネルギー揺らぎ（$C_c$, $\Delta V$）による減衰

<!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormCTF.cs:Renew / ButtonCopyGraph_Click) -->
曲線ごとにチェックボックスがあります: $\sin\chi$、$\cos\chi$、$E_\text{s}$、$E_\text{c}$、**PCTF** $=\sin\chi\,E_\text{s}E_\text{c}$、**ACTF** $=\cos\chi\,E_\text{s}E_\text{c}$。PCTF と ACTF は対物絞りの外側で 0 になります。HRTEM モードでは

$$E_\text{s}(u)=\exp\!\left[-\pi^2\beta^2u^2\left(\Delta f+C_s\lambda^2u^2\right)^2\right],\qquad E_\text{c}(u)=\exp\!\left[-\tfrac{1}{2}\pi^2\lambda^2\delta^2u^4\right]$$

です。**Max u**（nm⁻¹、既定 6）で横軸の上限を設定します。**Copy** は描画中の曲線をタブ区切りテキストでクリップボードへコピーします（ウィンドウの UI は英語のみ）。このウィンドウは STEM モードでも使えます（[STEMシミュレーション](2-stem-simulation.md#ctf-stem) 参照）。ポテンシャルモードでは無効です。

---

## 対物絞り (HRTEM オプション)

![対物絞り (HRTEM オプション)](../../assets/cap-ja-auto/FormImageSimulator.splitContainer1.groupBoxOpticalProperty.groupBoxHREMoption1.png)

<!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormImageSimulator.cs:SimulateHRTEM, BetheMethod.cs:GetHRTEMImage / ExtractInsideBeams) -->
対物絞りを通過させる回折波を制限します。ブロッホ波計算そのものは常に **波の数** で設定した波数で行い、そのうちどのビームを結像に使うかを絞りで選びます（絞り中心を $\mathbf{c}$ として $|\mathbf{g}-\mathbf{c}| < 2\sin(\alpha_\text{obj}/2)/\lambda$ のビームが通過）。通過するビームが 2 本未満のときは、絞りを大きくするよう促すメッセージを出して計算を止めます。

- **サイズ** : 対物絞りの半角 (mrad、既定 12)。小さいほど高角度の回折波を切り、高分解能の細部がなだらかになります。逆空間半径 $\sin\theta/\lambda$ への換算値 (nm⁻¹) が表示されます。
- **Shift X / Y** : 対物絞り中心のシフト量 (mrad)。絞りをずらした暗視野結像に使います。照射そのものは傾けません。 <!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormImageSimulator.cs:SimulateHRTEM, BetheMethod.cs:GetHRTEMImage (入射ベクトル k=(0,0) 固定)) -->
- **絞り開放** : 対物絞りを開放（無限大）にし、すべての回折波を結像に使います。
- **スポット数** : 絞り内に入る回折ビーム（スポット）の本数（読み取り専用）。
- **スポット情報** : 絞り内に入る回折ビームの一覧テーブル（強度・複素振幅など）を開きます。

> 対物絞りの大きさは **回折シミュレータ** 上にも表示されます。

---

## HREM オプション（部分コヒーレンスモデル）

![HREM オプション](../../assets/cap-ja-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.groupBoxHREMoption2.png)

<!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: BetheMethod.cs:GetHRTEMImage (入射方向は k=(0,0) の 1 方向のみ)) -->
部分コヒーレンス（β と焦点広がり δ）の取り込み方を選びます。出射波は 1 つの入射方向（光軸方向）についてだけ計算し、照射角の広がりとエネルギー幅は下記のエンベロープ関数としてのみ入ります。

<!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: BetheMethod.cs:GetHRTEMImage (quasiMode)) -->
- **線形イメージ** : 準コヒーレント近似のモデルで計算コストが低い。動力学計算で得た出射波の各回折波 $\psi_g$ に $\exp[-i\chi(g)]\,E_\text{s}(g)\,E_\text{c}(g)$ を掛け、$\left|\sum_g\right|^2$ を像とします。透過波との干渉が像を支配する場合（薄い試料）に正確です。
- **透過交差係数** (TCC) : 計算コストが高いがより正確。すべてのビーム対 $(g,h)$ を相互のエンベロープ（完全な透過交差係数）付きで足し合わせます。多くの強い回折波を励起する強散乱体に使うべきモデルです。計算量は絞り内のビーム数の 2 乗で増えます。

詳細は [Appendix A3.2 — HRTEM 像形成](../appendix/a3-bloch-wave/hrtem.md) を参照。

---

## 画像モード（単一 / シリーズ画像）

![画像モード](../../assets/cap-ja-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.groupBoxSerialImage.png)

- **単一画像** : 現在の厚さとデフォーカスで1枚のHRTEM像を計算します。
- **シリーズ画像** : 厚さ・デフォーカスを段階的に変えた一連の画像（through-thickness / through-focus シリーズ）を生成します。実験像とのベストマッチ条件探索に有用です。

シリーズ画像では次を設定します。

| 項目 | 説明 |
|------|------|
| **試料厚み (nm)** / **デフォーカス (nm)** | どちらをスイープするか（両方可） |
| **Start / Step / Num** | 開始値・刻み幅・枚数。下のリスト欄に展開され、直接編集も可能 |
| **横軸の方向** | 厚さとデフォーカスの両方をスイープしたとき、グリッドの横方向に並べる量（厚さ / デフォーカス） |

厚さとデフォーカスの両方をスイープすると、行 × 列のマトリクス画像が生成されます。<!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: FormImageSimulator.Designer.cs:numericBoxThicknessNum / BuildPseudoGrid) -->**Num** はそれぞれ最大 20 です。一方だけをスイープして 3 枚以上になるときは、1 列ではなく正方形に近いグリッドに折り返して並べます。

---

## 生成画像

![生成画像](../../assets/cap-ja-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.panelImageProperties.groupBoxImageProperty.png)

- **Size (W×H)** : シミュレートする画像のピクセル数（既定 512×512）。
- **解像度** : サンプリング分解能 (pm/px、既定 2)。視野は Size × 解像度 なので、値を小さくすると狭い範囲を細かくサンプリングします。像は各画素でビームについての直接のフーリエ和で合成する（FFT は使わない）ため、計算時間は画素数とビーム数で決まり、この値には依存しません。 <!-- 261105Cl コードから起こした修正。別担当がコードと再照合済み (誤りは訂正) (根拠: BetheMethod.cs:GetHRTEMImage) -->

---

## 波の数

![波の数](../../assets/cap-ja-auto/FormImageSimulator.splitContainer1.groupBoxSimulation.panelModeOptions.panelImageProperties.groupBoxDiffractedWaves.png)

- **最大ブロッホ波数** : ベーテ法（Dynamical 計算）で使用するブロッホ波の最大数（既定 80）。多くするほど精度が上がりますが、固有値問題の解法時間が $O(N^3)$ で増加します。

---

## 関連項目

- [HRTEM/STEMシミュレータ（まとめ）](index.md)
- [STEMシミュレーション](2-stem-simulation.md)
- [ポテンシャルシミュレーション](3-potential-simulation.md)
- [Appendix A3.2 — HRTEM 像形成](../appendix/a3-bloch-wave/hrtem.md)
