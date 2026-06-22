# EBSD 計算

EBSD（電子背向散射繞射）使用與 CBED 和 STEM 相同的 Bethe/布洛赫波內核，但問題的提法不同。CBED 和 STEM 是**入射束問題**：一束電子波從試樣外部進入，並計算其出射波。EBSD 是**出射方向問題**：在試樣內部經歷了非彈性散射的電子作為背向散射電子射出，計算所要回答的是有多少強度沿每個外部方向離開。

ReciPro 透過倒易定理將該出射方向問題轉化為一個普通的入射束問題。它首先計算方向空間中的 **master pattern**，然後將該 master pattern 與蒙地卡羅的深度 / 能量 / 方向權重以及偵測器幾何相結合，形成偵測器圖樣。

---

## 用倒易定理的重新表述

如果直接計算從內部源點 $\mathbf r_n$ 到外部方向 $\widehat{\mathbf s}$ 的振幅，那麼對每一個源點都需要一個單獨的散射問題。這並不現實。

倒易定理將該問題重寫如下：一個從 $\mathbf r_n$ 出發的電子出現在遠場方向 $\widehat{\mathbf s}$ 上的振幅，等於一束從外部方向 $-\widehat{\mathbf s}$ 入射的倒易波在 $\mathbf r_n$ 處的振幅。這束倒易波是一個普通的 Bethe/布洛赫波解。將其記為 $\psi_{\widehat{\mathbf s}}^{\mathrm{rec}}(\mathbf r)$，則沿方向 $\widehat{\mathbf s}$ 的 EBSD 強度可以寫為

$$I_{\mathrm{EBSD}}(\widehat{\mathbf s};E,z)\propto
\sum_n \sigma_n(E,z)\,
\left|\psi_{\widehat{\mathbf s}}^{\mathrm{rec}}(\mathbf r_n;E,z)\right|^2$$

其中 $\sigma_n(E,z)$ 是在原子位置 $\mathbf r_n$ 附近、於能量 $E$ 和深度 $z$ 處發生非彈性散射進入背向散射通道的權重。這些源項作為強度相加，而不是作為同調振幅之和相加，因為假定非彈性散射會破壞不同源位置之間的相位關係。

---

## Master Pattern

EBSD master pattern 將上式中與晶體相關的動力學繞射部分儲存在一個方向網格上。從概念上講，

$$M(\widehat{\mathbf s};E,z)=
\sum_n w_n(E,z)\,
\left|\psi_{\widehat{\mathbf s}}^{\mathrm{rec}}(\mathbf r_n)\right|^2$$

其中 $w_n$ 是在原子位置 $\mathbf r_n$ 處的晶體側非彈性源權重。ReciPro 使用經驗權重

$$w_n \propto Z_n^{1.7}\,\mathrm{occ}_n$$

其中 $Z_n$ 為原子序數，$\mathrm{occ}_n$ 為占有率。這與蒙地卡羅產生的傳輸深度 / 能量分布是分開的。

在實作中，倒易布洛赫波在每個原子位置處被求值：

$$\beta_n^{(j)}=
\alpha^{(j)}
\sum_{\mathbf g}C_{\mathbf g}^{(j)}
\exp\!\left[2\pi i(\mathbf k^{(j)}+\mathbf g)\cdot\mathbf r_n\right]$$

隨後程式碼構造布洛赫波對矩陣

$$S_{jj'}=\sum_n w_n\,\beta_n^{(j)}\,\overline{\beta_n^{(j')}}$$

以及解析的厚度積分

$$\mathcal F_{jj'}(t)=
\frac{\exp\!\left[2\pi i(\gamma^{(j)}-\overline{\gamma^{(j')}})t\right]-1}
{2\pi i(\gamma^{(j)}-\overline{\gamma^{(j')}})}$$

從而 master pattern 被求值為

$$M(\widehat{\mathbf s};E,t)=
\mathrm{Re}\left\{\sum_{j,j'}S_{jj'}(E)\,\mathcal F_{jj'}(t)\right\}$$

在分母接近零的退化極限下，$\mathcal F_{jj'}(t)\to t$。

---

## 方向空間取樣

master pattern 本身並不是偵測器影像；它是晶體固定方向空間中的強度分布。ReciPro 用面積守恆的 Rosca-Lambert 投影對該方向空間進行取樣，並將 $+Z$ 與 $-Z$ 兩個半球儲存為各自獨立的平面陣列。面積守恆取樣減小了兩極與赤道之間的密度偏差。

在此階段，master pattern 依賴於晶體結構、加速電壓、深度、能量和吸收模型。諸如圖樣中心和螢幕位置之類的偵測器幾何尚未套用。

---

## 蒙地卡羅權重與偵測器圖樣

為了得到接近實驗可觀測量的 EBSD 偵測器圖樣，必須按照從每個深度、能量和方向射出的背向散射電子數量對 master pattern 加權。將該傳輸權重記為

$$W(E,z;\widehat{\mathbf s})$$

並以 $\widehat{\mathbf s}(\mathbf p)$ 表示與偵測器像素 $\mathbf p$ 相對應的晶體固定方向，則最終的偵測器圖樣為

$$P(\mathbf p)=
\sum_{i,j}
W(E_i,z_j;\widehat{\mathbf s}(\mathbf p))\,
M(\widehat{\mathbf s}(\mathbf p);E_i,z_j)$$

即一個對能量和深度的離散求和。

蒙地卡羅部分追蹤彈性散射、非彈性散射、能量損失以及通過試樣表面的逸出。對於背向散射電子，它構建深度、能量和出射方向的分布。ReciPro 區分兩類模型：一類使用最後一次非彈性散射位置以及其後緊接的能量作為有效源，另一類使用逸出深度和逸出能量。

---

## TDS 背景與吸收模型

EBSD 圖樣不僅包含幾何菊池帶結構，還包含來自熱漫散射（TDS）的平滑背景。當啟用 `IncludeTDSBackground` 時，ReciPro 將散射進入後半球的 TDS 分量，

$$\pi/2\leq\theta\leq\pi$$

作為吸收矩陣 $\mu_{\mathrm{back}}$ 求值，並使用與 master pattern 相同的布洛赫波對求和來添加背景強度。由於重複使用了同一個本徵解，TDS 背景所增加的額外開銷相對較少。

當啟用 `UseNonLocalAbsorption` 時，吸收位能不再僅作為 $U'_{\mathbf g-\mathbf h}$ 處理，而是作為依賴於方向和束對的非局域形式處理。這可以提高精度，但同時也需要為 master pattern 網格中的各個方向重建吸收矩陣，因此可能顯著增加計算時間。

---

## 實用參數

- **束數**：束數過少會丟失菊池帶細節和 HOLZ 帶結構。低指數晶帶軸可能需要數百束。
- **深度和能量陣列**：如果它們比蒙地卡羅權重 $W(E,z;\widehat{\mathbf s})$ 的變化尺度更粗，則與能量相關的帶寬以及通道深度效應會被平均掉。
- **偵測器幾何**：圖樣中心、螢幕距離和試樣傾斜決定了映射 $\widehat{\mathbf s}(\mathbf p)$，因此即使 master pattern 不變，偵測器圖樣也可能改變。
- **倒易性解釋**：master pattern 不是偵測器影像。只有在經過蒙地卡羅加權和偵測器投影之後，它才成為偵測器圖樣。
- **TDS 背景**：在進行定量帶襯度對比時啟用它。在不帶平滑背景時幾何菊池結構更易於檢視的情況下，則停用它。

## 另請參閱

- [動力學計算（共用內核）](calculation.md)
- [附錄 A3. 用布洛赫波法處理動力學繞射](index.md)
- [12. EBSD 模擬](../../12-ebsd-simulation.md)
