# CBED 計算

CBED（會聚束電子繞射）將[動力學核心](calculation.md)應用於許多入射束方向，然後把結果排列進繞射盤中。SAED 只有一個入射方向；CBED 則把物鏡光闌內的每個點都視為一個**部分入射平面波**，並對其中每一個分別求解布洛赫波問題。

---

## 會聚束的表示

在入射面處，會聚探針可以用探針位置 $\mathbf R_0$、透鏡相位 $\chi(\mathbf K)$ 和光闌函式 $A(\mathbf K)$ 寫成平面波之和：

$$\psi_{\mathrm{in}}(\mathbf R,0)=\sum_{\mathbf K\in\mathrm{aperture}} A(\mathbf K)\,
\exp(-2\pi i\,\mathbf K\cdot\mathbf R_0)\,
\exp[-i\chi(\mathbf K)]\,
\exp(2\pi i\,\mathbf K\cdot\mathbf R)$$

這裡 $\mathbf K$ 是入射波向量中平行於試樣表面的分量。對於一個會聚半角為 $\alpha$、電子波長為 $\lambda$ 的理想圓形光闌，有

$$A(\mathbf K)=
\begin{cases}
1 & (|\mathbf K|\leq \sin\alpha/\lambda)\\
0 & (|\mathbf K|> \sin\alpha/\lambda)
\end{cases}$$

一個有代表性的透鏡相位，用欠焦 $\Delta f$ 和球面像差 $C_s$ 表示為

$$\chi(\mathbf K)=\pi\lambda|\mathbf K|^2\Delta f+\frac{\pi}{2}C_s\lambda^3|\mathbf K|^4+\cdots$$

在 ReciPro 中，該表達式由像差、光闌和會聚角的設定來控制。

---

## 對每個方向的動力學計算

對於 CBED，光闌內的每個 $\mathbf K$ 都被視為一束平行入射波。其概念性流程為：

1. 由 $\mathbf K$ 和試樣表面法線確定折射後的參考波向量 $\mathbf k_0(\mathbf K)$。
2. 用排序量 $R_{\mathbf g}=|\mathbf g|Q_{\mathbf g}^2$ 選取反射束。
3. 建構結構矩陣 $\mathbf A$，並計算厚度 $t$ 處的透射係數 $T_{\mathbf g}(t;\mathbf K)$。

這就是[動力學核心](calculation.md)中的透射係數計算，對每個取樣的入射方向重複進行。對於厚度系列，給定方向的本徵解可以重複使用，只需更新傳播因子即可。

---

## 繞射盤的組裝

將所有 $\mathbf K$ 方向的出射波放入繞射面中，便得到透射盤和各繞射盤內部的強度。若 $\mathbf Q$ 為繞射面座標，則位置平均的 CBED 或低同調條件可以近似為非同調強度之和：

$$I_{\mathrm{CBED}}(\mathbf Q)=
\sum_{\mathbf K\in\mathrm{aperture}}
\left|\psi_{\mathbf K}(\mathbf Q,t)\right|^2$$

對於像 LACBED 那樣、需要在更大範圍內保持相位同調的模式，則必須先把振幅相加，然後再取強度。

---

## CBED 能顯示什麼

CBED 把布洛赫波解的厚度依賴性，以繞射盤內部強度結構的形式視覺化呈現。

- 改變厚度會改變盤內振盪、HOLZ 線和 Kossel-Mollenstedt 條紋。
- 改變入射取向會改變哪些反射被強烈激發。
- 增大會聚角會展寬繞射盤，並能揭示重疊以及高階勞厄帶的資訊。

因此，CBED 是把布洛赫波結果作為繞射面上的盤狀圖樣來觀察的最直接方式。在 ReciPro 中，最好將其理解為會聚束離散化、每個方向一個動力學解，以及重新排列為盤陣列三者的組合。

---

## 實用參數

- **束數**：強晶帶軸條件和 HOLZ 線細節需要大量反射束。請檢查在增大最大布洛赫波數時盤內部如何變化。
- **角度取樣**：如果光闌內的 $\mathbf K$ 取樣過於粗糙，盤內強度就會變得顆粒狀。更大的會聚角需要更細的取樣。
- **厚度**：厚度系列得益於本徵值法，因為一個本徵解可以重複用於許多厚度。
- **同調性**：請區分非同調強度求和足夠的條件，與需要同調振幅求和的條件。

## 另請參閱

- [動力學計算（共用核心）](calculation.md)
- [附錄 A3. 用布洛赫波法處理的動力學繞射](index.md)
- [7.4. CBED 模擬](../../7-diffraction-simulator/3-cbed-simulation.md)
