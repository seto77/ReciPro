# 電子軌道 (Electron Trajectory)

**電子飛程シミュレータ** は、試料内部の電子の軌跡を **モンテカルロ法** で計算します。入射電子は弾性・非弾性散乱を受け、後方散乱電子の分布（方向・エネルギー・侵入深さ）が集計されます。これらの分布は [EBSDシミュレーション](12-ebsd-simulation.md) の方位・エネルギー・深さの重み付けにも利用されます。

![電子軌道](../assets/cap-ja-auto/FormTrajectory.png)

---

## 計算条件

![計算条件](../assets/cap-ja-auto/FormTrajectory.panelCalculationConditions.png)

ビームエネルギー、入射電子数、試料・物質、その他のモンテカルロパラメータ。

---

## ステレオネットオプション

![ステレオネットオプション](../assets/cap-ja-auto/FormTrajectory.panelDrawingOptions.png)

ステレオネット投影に描画する角度分布の表示オプション。

---

## 統計情報

![統計情報](../assets/cap-ja-auto/FormTrajectory.groupBoxStatistics.png)

後方散乱率・平均自由行程・侵入深さなど、計算結果の要約。

---

## 後方散乱電子の方位分布

![後方散乱電子の方位分布](../assets/cap-ja-auto/FormTrajectory.groupBoxDirectionDistribution.png)

後方散乱電子の角度分布（ステレオネット中心は表面法線方向）。

---

## プロファイル

![プロファイル](../assets/cap-ja-auto/FormTrajectory.flowLayoutPanelProfiles.png)

電子の深さ・エネルギープロファイル。
