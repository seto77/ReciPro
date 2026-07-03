# 結構因子

原子散射因子描述單一原子；**結構因子**則描述晶胞中所有原子如何*共同*散射。它是 **繞射** 索引標籤所列出的量（`F_real`、`F_inv`、$\lvert F\rvert$、$F^2$），也是連結上一頁原子物理與繞射強度之間的橋梁。

=== "X-ray"
    ![繞射 — X-ray](../../../assets/cap-zh-Hant-auto/FormBeamInteraction-xray-reflections.png)

=== "Electron"
    ![繞射 — electron](../../../assets/cap-zh-Hant-auto/FormBeamInteraction-electron-reflections.png)

=== "Neutron"
    ![繞射 — neutron](../../../assets/cap-zh-Hant-auto/FormBeamInteraction-neutron-reflections.png)

---

## 晶胞上的干涉

反射 $\mathbf g = (hkl)$ 的結構因子是原子因子的同調總和，每一項都以原子分數座標 $\mathbf r_j = (x_j,y_j,z_j)$ 所對應的相位加權：

$$F_{\mathbf g} = \sum_{j} o_j\, f_j(s,E)\, T_j(\mathbf g)\, \exp\!\left(-2\pi i\,(h x_j + k y_j + l z_j)\right).$$

- $o_j$ : 位置**佔有率**（occupancy，分數值，用於部分或混合佔據）。
- $f_j(s,E)$ : 原子 $j$ 對當前束流的原子散射因子 — 在 ReciPro 的[相位慣例](index.md#phase-convention)下，X 射線為 $f_0+f'-if''$，電子為 $f_e$，中子為 $b$。
- $T_j(\mathbf g)$ : 德拜-沃勒因子（見下文）。
- $-2\pi i$ 相位遵循 ReciPro 的[慣例](index.md#phase-convention)。

強度為模的平方，

$$I_{\mathbf g} \;\propto\; \lvert F_{\mathbf g}\rvert^2 = F_\text{real}^2 + F_\text{inv}^2 ,$$

即表格中的 $F^2$ 欄。`F_real` 與 `F_inv` 分別是複數結構因子的實部與虛部。即使原子因子為純實數，對於非中心對稱結構（或原點移位的情形），$F_{\mathbf g}$ 通常仍為複數；X 射線的異常色散（複數 $f$）與複數中子散射長度會再加入一項虛數貢獻。`F_inv` 只有在結構為中心對稱、原點位於對稱中心且所有因子皆為實數時，才會對*每一個*反射皆消失。

---

## 德拜-沃勒因子

原子在其平衡位置附近振動，使散射密度模糊化並降低高角度的因子。對於各向同性的運動，

$$T_j = \exp\!\left(-B_j\, s^2\right), \qquad B_j = 8\pi^2\langle u_j^2\rangle,$$

其中 $\langle u_j^2\rangle$ 是沿散射方向的均方位移，$B_j$ 是各向同性位移參數（Å²）。各向異性的運動則推廣為

$$T_j = \exp\!\left(-2\pi^2\,\mathbf g^{\mathsf T}\!\mathbf U_j\,\mathbf g\right),$$

其中 $\mathbf U_j$ 為位移張量，$\mathbf g$ 為倒易點陣向量（$|\mathbf g|=1/d$，而非 $Q=2\pi\lvert\mathbf g\rvert$）。對於德拜固體，均方位移本身是溫度 $T$、原子質量 $M$ 與德拜溫度 $\Theta_D$ 的函數，

$$\langle u^2\rangle = \frac{3\hbar^2}{M k_B \Theta_D}\left[\frac14 + \left(\frac{T}{\Theta_D}\right)^2\!\int_0^{\Theta_D/T}\frac{x}{e^x-1}\,dx\right],$$

因此 $B$ 隨溫度上升，並對重原子減小。ReciPro 直接採用表列或輸入的 $B_j$，而不計算此式。由於 $T_j$ 與散射因子相乘，**散射因子** 索引標籤可以對所繪曲線套用相同的 $e^{-Bs^2}$ 阻尼。阻尼隨溫度與 $s$ 增大，這正是熱漫散射（從同調布拉格束流中移除並重新分配至漫散背景的強度）在動力學理論中供給吸收位能的原因（[附錄 A3](../a3-bloch-wave/index.md)）。

---

## 消光：系統性與偶然性

反射可能因兩種不同的原因而**缺失**：

- **系統性（空間群）缺失。** 點陣中心化以及帶有平移分量的對稱元素（螺旋軸、滑移面）使整類反射*精確地*消失，對該空間群中的每個晶體皆然，與原子組成無關。這些就是 **隱藏禁制晶面** 背後的規則。
- **偶然性的近消光。** 當原子貢獻對某特定結構恰好抵消時，強度雖小卻非對稱性所禁止，且若組成或位置改變便可能重新出現。這些*不會*被消光規則移除。

系統性缺失是晶胞之對稱相關副本之間的相位抵消。對於中心化平移 $\mathbf t_\alpha$，結構因子帶有一個公因子

$$F_{\mathbf g} \propto \sum_\alpha e^{-2\pi i\,\mathbf g\cdot\mathbf t_\alpha},$$

對某些 $hkl$ 為零。對於體心化（$\mathbf t = \tfrac12,\tfrac12,\tfrac12$），

$$1 + e^{-\pi i (h+k+l)} = 0 \quad\Longleftrightarrow\quad h+k+l \ \text{odd}.$$

最常見的系統性缺失為：

| 對稱元素 | 缺失條件 | 受影響的反射 |
|---|---|---|
| $I$ (體心) | $h+k+l$ 為奇數 | 所有 $hkl$ |
| $F$ (面心) | $h,k,l$ 奇偶性混合 | 所有 $hkl$ |
| $C$ (C 底心) | $h+k$ 為奇數 | 所有 $hkl$ |
| $2_1$ 螺旋軸 $\parallel b$ | $k$ 為奇數 | $0k0$ |
| $a$-滑移面 $\perp b$ | $h$ 為奇數 | $h0l$ |
| $c$-滑移面 $\perp b$ | $l$ 為奇數 | $h0l$ |

中心化條件適用於每一個反射；螺旋軸與滑移面條件僅適用於對應的軸向列或晶帶，這正使它們成為診斷空間群的依據。

---

## 弗里德爾定律及其失效

對於由實數（非共振）散射因子構成的結構，將總和取共軛並翻轉 $\mathbf g$ 的符號可直接顯示（為清楚起見略去實數權重 $o_j T_j$）

$$F_{-\mathbf g} = \sum_j f_j\, e^{+2\pi i\,\mathbf g\cdot\mathbf r_j} = \left(\sum_j f_j\, e^{-2\pi i\,\mathbf g\cdot\mathbf r_j}\right)^{*} = F_{\mathbf g}^{*}, \qquad\text{hence}\qquad \lvert F_{hkl}\rvert = \lvert F_{\bar h\bar k\bar l}\rvert \quad\text{(Friedel's law).}$$

於是繞射即使在晶體並非中心對稱時仍呈現中心對稱。**異常色散可以打破這一點。** 將結構因子寫成一個正常部分（可乾淨地取共軛）加上一個異常部分，在 ReciPro 的 $f = f_0 + f' - i f''$ 慣例下為 $F_{\mathbf g} = A_{\mathbf g} - i B_{\mathbf g}$ 與 $F_{-\mathbf g} = A_{\mathbf g}^{*} - i B_{\mathbf g}^{*}$，則 **Bijvoet 差**為

$$\lvert F_{\mathbf g}\rvert^2 - \lvert F_{-\mathbf g}\rvert^2 = -4\,\operatorname{Im}\!\left(A_{\mathbf g}\, B_{\mathbf g}^{*}\right),$$

只有當正常部分與異常部分具有不同相位時才不為零 — 也就是當化學上不同的異常散射體佔據非中心對稱位置時。（對於中心對稱結構、單一元素，或每個原子皆帶有相同複數因子的任何情形，此差值皆消失。）這正是得以判定非中心對稱晶體之絕對結構（手性）的依據，也是一旦選擇接近某吸收邊的 X 射線能量後，ReciPro 對弗里德爾對報告出非零 `F_inv` 與相異 $\lvert F\rvert$ 的物理原因。

---

## 從結構因子到粉末強度

開啟 **粉末繞射強度（Bragg-Brentano 光路）** 會將隨機取向多晶體的幾何納入考量，把 $\lvert F\rvert^2$ 轉換為相對粉末強度：

$$I_{hkl} \;\propto\; m_{hkl}\, \lvert F_{hkl}\rvert^2\, L p(\theta),$$

- $m_{hkl}$ : **多重度** — 在同一 $2\theta$ 處重疊的對稱等價晶面數目（表格中的 *多重度* 欄）。
- $Lp(\theta)$ : Bragg-Brentano 光學的 **勞侖茲-偏振**因子，$Lp = \dfrac{1+\cos^2 2\theta}{\sin^2\theta\,\cos\theta}$，會強烈增強低角度的波峰。

由於在此模式下等價晶面會被合併為單一條譜線，ReciPro 也會強制開啟 *隱藏等效晶面* 與 *隱藏禁制晶面*。

---

## 另請參閱

- [原子散射因子](scattering-factor.md) — 進入總和的 $f_j$。
- [衰減與傳輸](attenuation-transport.md) — 散射事件之間束流會發生什麼。
- [3. 電子束交互作用 → 繞射 索引標籤](../../3-beam-interaction.md#reflections-tab)
- [附錄 A3. 動力學繞射](../a3-bloch-wave/index.md) — 當 $\lvert F\rvert^2$（運動學）已不再足夠時。
