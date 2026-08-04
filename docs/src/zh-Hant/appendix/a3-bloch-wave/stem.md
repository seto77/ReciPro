# STEM 計算

STEM 影像計算從與 [CBED](cbed.md) 相同的會聚探針表示出發。差異在於可觀測量：CBED 顯示繞射平面中的盤強度，而 STEM 掃描探針位置，並在每個位置積分進入所選偵測器的強度。

---

## 可觀測量

設 $\mathbf R_0$ 為探針位置，$\mathbf Q$ 為繞射平面座標，$t$ 為試樣厚度。若偵測器函式 $D(\mathbf Q)$ 在偵測器角度範圍內為 1、範圍外為 0，則彈性 STEM 強度為

$$I_{\mathrm{STEM}}^{\mathrm{ela}}(\mathbf R_0)=
\int D(\mathbf Q)\,
\left|\psi(\mathbf Q,t;\mathbf R_0)\right|^2\,d\mathbf Q$$

BF、ABF、LAADF 和 HAADF 對應於 $D(\mathbf Q)$ 中內、外角度的不同選擇。因此改變 STEM 偵測器角度會改變所積分的物理量；這不僅僅是一項顯示設定。

---

## 透過傅立葉係數加速

直接的實作會對每個被掃描的探針位置 $\mathbf R_0$ 重新求解動力學問題。會聚探針表達式具有一個有用的結構：對 $\mathbf R_0$ 的依賴以相位因子的形式出現

$$\exp(-2\pi i\,\mathbf K\cdot\mathbf R_0)$$

這使得 ReciPro 可以先計算影像的二維傅立葉係數，而不必逐點計算 $I_{\mathrm{STEM}}(\mathbf R_0)$。從概念上講，

$$I_{\mathrm{STEM}}^{\mathrm{ela}}(\mathbf q)=
\sum_{\mathbf g,\mathbf h}
F_{\mathbf g,\mathbf h}(t)\,
\delta(\mathbf q-\mathbf g+\mathbf h)$$

因此一旦已知係數 $F_{\mathbf g,\mathbf h}(t)$，便可透過逆傅立葉變換高效地重建完整的掃描影像。

這是布洛赫波 STEM 對於具有小晶胞的完美晶體的主要優勢。它可以比在每個探針位置重複一次多層切片（multislice）計算快得多。

---

## 重建為實數影像 {#real-image-reconstruction}

影像由係數依下式還原：

$$I(\mathbf r)=\sum_{\mathbf q}I(\mathbf q)\,\exp(2\pi i\,\mathbf q\cdot\mathbf r),
\qquad \mathbf q=\mathbf g-\mathbf h$$

由於 $I(\mathbf r)$ 是實數強度，其係數必須嚴格滿足厄米對稱性：

$$I(-\mathbf q)=I(\mathbf q)^{*}$$

而由所有束對產生的 $\mathbf q$ 集合在 $\mathbf q\rightarrow-\mathbf q$ 下是封閉的。因此該求和在構造上即為實數，**任何殘留的虛部都是數值誤差而非物理**。

實際上確實會殘留很小的虛部，因為 $\mathbf k+\mathbf q$ 處的振幅是在有限的入射方向格點上以雙線性內插取得的（參見[探針的角度取樣](#angular-sampling)）。這使得 $I(-\mathbf q)$ 與 $I(\mathbf q)^{*}$ 相差 $h^{2}$ 量級，其中 $h$ 為角度步長。

將求和後的像素寫作 $a+ib$，把它歸約為實數影像的正確作法是取**實部** $a$。這是往實軸的正交投影，與先將係數對稱化

$$I_{\mathrm{sym}}(\mathbf q)=\tfrac12\left[I(\mathbf q)+I(-\mathbf q)^{*}\right]$$

再求和完全等價。而取絕對值 $\sqrt{a^{2}+b^{2}}\simeq a+b^{2}/2a$ **並不**等價，且在四個方面都是錯誤的：

- 額外項 $b^{2}/2a$ 恆為正，因而永不抵消——這是偏差而非雜訊；
- 在 $a$ 較小處，也就是**暗**像素處，它相對訊號最大，因此侵蝕的是影像對比而非整體亮度；
- 它破壞線性性，由於 $\lvert z_1+z_2\rvert\neq\lvert z_1\rvert+\lvert z_2\rvert$，合成影像不再等於彈性 + TDS；
- 它掩蓋負值像素，而負值正是 $\mathbf q$ 取樣不足的可見徵兆，本應作為對使用者的警示保留下來。

因此 ReciPro 的彈性、TDS 與 STEM-EDX 影像皆由實部重建，且僅在光源尺寸造成的模糊之後才截斷至零，使真正為負的像素在此之前始終可被偵出。

!!! note
    4.944 以前的版本對彈性影像與 TDS 影像取絕對值求和。在預設角度格點下，其差異遠低於任何可察覺的水準（見下表）；只有刻意採用粗格點時才會變得可測，且呈現方式始終是暗像素略微變亮。

---

## 探針的角度取樣 {#angular-sampling}

入射錐在步長為 $\Delta\alpha$（STEM 選項中的**角解析度**）的方形方向格點上取樣，以少量餘裕涵蓋會聚半角 $\alpha$。沿一軸的分割數為

$$N=\left\lceil\frac{2\alpha\times1.05}{\Delta\alpha}\right\rceil$$

因而方向數——亦即需求解的本徵值問題數——按 $N^{2}$ 增長。此格點與掃描點數無關：它離散化的是*探針內部的方向*，而非*探針的位置*。

它也是上述厄米殘差的唯一來源，因此該殘差可直接作為收斂指標。下列數值測自 SrTiO₃ [001]、200 kV、$\alpha=25$ mrad、128 束、32×32 掃描點。「殘差」為 $\max_{\mathbf q}\lvert I(\mathbf q)-I(-\mathbf q)^{*}\rvert$ 相對 $I(\mathbf 0)$ 的值，右側兩欄為取絕對值求和原本會在最亮像素上增加的變亮量。

| $N$ | 方向數 | 彈性殘差 | TDS 殘差 | 絕對值偏差（彈性） | 絕對值偏差（TDS） |
|----:|-----------:|-----------------:|-------------:|------------------------:|--------------------:|
| 16  | 256    | 1.2×10⁻³ | 6.1×10⁻³ | 2.4×10⁻⁵ | 1.1×10⁻⁴ |
| 32  | 1024   | 4.1×10⁻⁴ | 2.6×10⁻³ | 1.1×10⁻⁶ | 1.3×10⁻⁵ |
| 64  | 4096   | 5.6×10⁻⁵ | 7.2×10⁻⁴ | 5.8×10⁻⁸ | 4.3×10⁻⁷ |
| 132 | 17424  | 3.8×10⁻⁵ | 1.1×10⁻⁴ | 4.2×10⁻⁸ | 3.6×10⁻⁸ |

預設角解析度 0.4 mrad 對 $\alpha=25$ mrad 給出 $N=132$，已處於收斂區。另有兩點值得注意：

- 在任何格點下，TDS 殘差都比彈性殘差大約一個量級，因為 TDS 係數還多帶了一重偵測器選擇吸收的厚度積分。
- 殘差是對全部 $\mathbf q$ 取的最大值，因此逐格點略有起伏而非完全平滑下降；其背後的趨勢為 $O(h^{2})$。

---

## TDS 與偵測器選擇性吸收

在 HAADF-STEM 中，來自熱漫散射 (TDS) 的非彈性分量往往是影像對比的主要來源。ReciPro 將 TDS 處理為從彈性通道中移除並進入所選角度範圍的強度，並用吸收位能來表示。

對於偵測器角度範圍 $\theta_1\leq\theta\leq\theta_2$，偵測器選擇性吸收散射因子在概念上可寫為

$$f'_{\kappa}(\mathbf g;\theta_1,\theta_2)=
\int_{\theta_1}^{\theta_2}\sin\theta\,d\theta
\int_0^{2\pi}
\left|\Delta f_{e,\kappa}(\mathbf g,\theta,\phi)\right|^2\,d\phi$$

將該範圍選取為與 BF、ADF 或 HAADF 偵測器相匹配，即可計算出進入該偵測器的 TDS 貢獻。

STEM TDS 強度是偵測器選擇性吸收的厚度積分：

$$I_{\mathrm{STEM}}^{\mathrm{TDS}}(\mathbf R_0)=
\int_0^t
\langle\psi(z;\mathbf R_0)|\widehat W_{\mathrm{det}}|\psi(z;\mathbf R_0)\rangle\,dz$$

其中 $\widehat W_{\mathrm{det}}$ 表示偵測器選擇性 TDS。一旦已知布洛赫波的本徵值和本徵向量，這個 $z$ 積分便可解析處理。數值切片積分同樣可行，ReciPro 會根據計算模式採用合適的方法。

---

## 局域吸收與非局域吸收

吸收位能可以用兩種主要方式處理。

| 形式 | 含義 | 特點 |
|------|---------|---------|
| 局域近似 | 使用僅依賴於位置的吸收位能 $U'(\mathbf r)$。 | 對寬 ADF / HAADF 偵測器通常有效且快速。 |
| 非局域形式 | 使用 $U'(\mathbf r,\mathbf r')$ 或依賴於入射波與出射波成對組合的矩陣元 $U'_{\mathbf g,\mathbf h}$。 | 對窄偵測器、重元素或低加速電壓更準確，但代價高得多。 |

在局域近似中，矩陣元可由倒易向量差（如 $U'_{\mathbf g-\mathbf h}$）求得。在非局域形式中，每一對 $(\mathbf g,\mathbf h)$ 都需要各自的角度積分，因此計算代價隨束數迅速增長。

---

## 布洛赫波 STEM 的適用範圍

布洛赫波 STEM 對於高度週期性的完美晶體很快，非常適合對厚度、欠焦和偵測器角度進行系統性比較。對於缺陷、大型超胞或非週期性結構，諸如凍結聲子多層切片（frozen-phonon multislice）之類的方法可能更合適，因為它們不依賴於相同的小週期胞假設。

在 ReciPro 中，理解 STEM 最簡單的方式如下：從與 CBED 相同的會聚波出發，然後將繞射盤可觀測量替換為對繞射平面的偵測器積分。

---

## 實用參數

- **偵測器角度**：BF / ABF / ADF / HAADF 是 $D(\mathbf Q)$ 與 $f'_{\kappa}(\mathbf g;\theta_1,\theta_2)$ 的定義。
- **束數**：高頻影像分量和通道效應對所納入的束數較為敏感。
- **厚度步長**：若使用數值切片積分，請檢查將切片厚度減半時的變化。
- **角解析度**：決定探針方向格點 $N$（參見[探針的角度取樣](#angular-sampling)）。計算量按 $N^{2}$ 增長，因而是左右計算時間的最主要因素。
- **TDS 模型**：對於 HAADF $Z$ 對比，TDS 項與彈性項同等重要。

## 另請參閱

- [動力學計算（共用核心）](calculation.md)
- [附錄 A3. 用布洛赫波法處理動力學繞射](index.md)
- [9.2. STEM 模擬](../../9-hrtem-stem-simulator/2-stem-simulation.md)
