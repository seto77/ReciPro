# 進動電子繞射 (PED) 模擬

**PED (Precession Electron Diffraction)** 模擬會計算將入射束在繞光軸的錐面上進動時所得到的電子繞射圖樣。

> 本頁列出當您選擇 **波源 = 電子、光學 = 進動 (電子)、強度計算 = 動力學理論（自動）** 時，右側出現的所有設定。請注意，**在光學中選擇進動 (電子) 會自動將強度計算切換為動力學理論**。關於繪製與儲存等視窗整體操作，請參閱[總覽頁](index.md)。

GUI 條件：**波源 = 電子、光學 = 進動 (電子)、強度計算 = 動力學理論（自動）**

![PED 模式下的繞射模擬器](../../assets/cap-zh-Hant-auto/FormDiffractionSimulator-ped.png)

---

## 總覽

在 PED 中，電子束會在繞光軸的錐面上進動，並將進動錐面上每個束方向所得到的繞射圖樣加以積分。與傳統的 SAED 相比，這提供了以下優點：

- 動力學效應被平均掉，產生接近運動學強度比的強度資料
- 較清楚地觀察到高階勞厄區 (HOLZ) 反射
- 可獲得適用於結構分析的強度資料

---

## 波源 {#wavelength-setup}

![波源](../../assets/cap-zh-Hant-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelWaveLength.png)

由於 PED 屬於電子繞射，請在 **波源** 中選擇 **電子**。輸入電子能量 (keV) 或波長 (Å 或 nm，可切換；預設 Å) 即可計算經相對論修正的波長。

---

## 光學 {#incident-beam}

![光學](../../assets/cap-zh-Hant-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelBeamMode.png)

在 **光學**（入射束幾何）中選擇 **進動 (電子)**（僅在選擇 **電子** 時可用）。

> **注意** ：選擇 **進動 (電子)** 會**自動將強度計算切換為動力學理論**，並出現布洛赫波法設定面板與進動設定面板。**僅激發誤差** / **運動學 & 激發誤差** 將無法再選取。

---

## 進動設定

![進動參數](../../assets/cap-zh-Hant-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelPED.png)

設定進動錐面的形狀與取樣。

| 參數 | 說明 | 建議值 |
|-----------|-------------|-------------|
| **進動角** | 進動角 = 進動錐面的半頂角 (mrad；0.1–500，預設 50) <!-- 261106Cl 作者の決定 D1 --> | 10–40 mrad |
| **步進** | 在進動錐面上取樣的平行束方向數目 (2–1080，預設 36)。較大的值可得到較平滑的積分，但會使計算時間線性增加 | 36–72 |

---

## 強度計算與布洛赫波法設定

一旦選擇 **進動 (電子)**，**強度計算** 即被固定為 **動力學理論**（自動）。對於每個進動方向的平行束，會以布洛赫波法（動力學計算）計算繞射強度，並對所有方向積分以得到 PED 圖樣。

![布洛赫波參數](../../assets/cap-zh-Hant-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelBethe.png)

| 參數 | 說明 | 建議值 |
|-----------|-------------|-------------|
| **繞射波數目** | 本徵值問題中所納入的布洛赫波數目 (8–1000，預設 240)。較大的值可得到較準確的強度，但計算時間會以 $O(N^3)$ 成長 | 50–200 |
| **厚度** | 動力學計算中所用的試樣厚度 (nm；0.01–10000，預設 50) | — |

計算成本大致為「步數 × 每個方向的布洛赫波計算」。關於動力學計算的細節，請參閱[動力學計算（布洛赫波法）](../appendix/a3-bloch-wave/calculation.md)。

---

## 繞射點外觀

![外觀](../../assets/cap-zh-Hant-auto/FormDiffractionSimulator.groupBoxSpotProperty.panelSimulationOptions.flowLayoutPanelAppearance.png)

控制每個繞射點的繪製方式。

- **實心球 / 高斯** ：倒易點陣點的幾何模型。**實心球** 繪製半徑為 $R$ 的球面與厄瓦爾德球的截面，而 **高斯** 繪製 $\sigma = R$ 的 3D 高斯函數與厄瓦爾德球的截面（一個 2D 高斯函數）。
- **不透明度** ：繞射點的透明度（0 = 透明，1 = 不透明）。
- **繞射點半徑 (R)** ：倒易點陣點的半徑。對於動力學強度，高斯積分 $=$ 亮度 $\times I_\text{dyn}$，而實心球使用半徑 $R \times I_\text{dyn}^{1/2}$（使面積與動力學強度成正比）。
- **亮度**、**色階**、**對數刻度** ：僅在 **高斯** 模式下顯示。**亮度** 縮放所繪製高斯函數的積分強度；**色階** 可選 **Gray**（繞射點使用下方的繞射點顏色）、**Cold-warm**、**Spectrum** 或 **Fire**；**對數刻度** 以對數刻度顯示強度。
- **繞射點顏色** ：在 **色階** 為 **Gray**（初始設定）時顯示：**一般斑點** / **消光斑點 (螺旋或滑移)** / **消光斑點 (晶格心化)**。
- **按晶體指定顏色** ：以指派給每個晶體的顏色繪製繞射點。

---

## 與 SAED 的比較

| 特徵 | SAED | PED |
|---------|------|-----|
| 束 | 平行、固定 | 進動（錐面掃描） |
| 動力學效應 | 大 | 平均化、較小 |
| HOLZ 反射 | 弱 | 強烈出現 |
| 強度可靠度 | 對結構分析可能不足 | 適用於結構分析 |
| 計算時間 | 短 | 長 |

---

## 另請參閱

- [繞射模擬器（總覽）](index.md)
- [X 光繞射模擬](5-x-ray-neutron-diffraction.md)
- [SAED 模擬](1-saed-simulation.md)
- [動力學計算（布洛赫波法）](../appendix/a3-bloch-wave/calculation.md)
