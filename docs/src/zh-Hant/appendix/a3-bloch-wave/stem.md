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
- **TDS 模型**：對於 HAADF $Z$ 對比，TDS 項與彈性項同等重要。

## 另請參閱

- [動力學計算（共用核心）](calculation.md)
- [附錄 A3. 用布洛赫波法處理動力學繞射](index.md)
- [9.2. STEM 模擬](../../9-hrtem-stem-simulator/2-stem-simulation.md)
